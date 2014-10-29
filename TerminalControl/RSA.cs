using System;

namespace PacketComs
{
    public class RSAPublicKey : PublicKey, IVerifier
    {
        internal BigInteger _e;
        internal BigInteger _n;


        public RSAPublicKey(BigInteger exp, BigInteger mod)
        {
            _e = exp;
            _n = mod;
        }

        public override PublicKeyAlgorithm Algorithm
        {
            get { return PublicKeyAlgorithm.RSA; }
        }

        public BigInteger Exponent
        {
            get { return _e; }
        }

        public BigInteger Modulus
        {
            get { return _n; }
        }

        public void Verify(byte[] data, byte[] expected)
        {
            if (VerifyBI(data) != new BigInteger(expected))
                throw new VerifyException("Failed to verify");
        }

        private BigInteger VerifyBI(byte[] data)
        {
            return new BigInteger(data).modPow(_e, _n);
        }

        public void VerifyWithSHA1(byte[] data, byte[] expected)
        {
            BigInteger result = VerifyBI(data);
            byte[] finaldata = RSAUtil.StripPKCS1Pad(result, 1).getBytes();

            if (finaldata.Length != PKIUtil.SHA1_ASN_ID.Length + expected.Length)
                throw new VerifyException("result is too short");
            else
            {
                byte[] r = new byte[finaldata.Length];
                Array.Copy(PKIUtil.SHA1_ASN_ID, 0, r, 0, PKIUtil.SHA1_ASN_ID.Length);
                Array.Copy(expected, 0, r, PKIUtil.SHA1_ASN_ID.Length, expected.Length);
                if (SSHUtil.Memcmp(r, finaldata) != 0)
                    throw new VerifyException("failed to verify");
            }
        }

        public override void WriteTo(IKeyWriter writer)
        {
            writer.Write(_e);
            writer.Write(_n);
        }
    }

    public class RSAUtil
    {
        public static BigInteger PKCS1PadType2(BigInteger input, int pad_len, Random rand)
        {
            int input_byte_length = (input.bitCount() + 7)/8;
            //System.out.println(String.valueOf(pad_len) + ":" + input_byte_length);
            byte[] pad = new byte[pad_len - input_byte_length - 3];

            for (int i = 0; i < pad.Length; i++)
            {
                byte[] b = new byte[1];
                rand.NextBytes(b);
                while (b[0] == 0) rand.NextBytes(b); //0�ł͂��߂�
                pad[i] = b[0];
            }

            BigInteger pad_int = new BigInteger(pad);
            pad_int = pad_int << ((input_byte_length + 1)*8);
            BigInteger result = new BigInteger(2);
            result = result << ((pad_len - 2)*8);
            result = result | pad_int;
            result = result | input;

            return result;
        }

        public static BigInteger PKCS1PadType1(BigInteger input, int pad_len)
        {
            int input_byte_length = (input.bitCount() + 7)/8;
            //System.out.println(String.valueOf(pad_len) + ":" + input_byte_length);
            byte[] pad = new byte[pad_len - input_byte_length - 3];

            for (int i = 0; i < pad.Length; i++)
            {
                pad[i] = (byte) 0xff;
            }

            BigInteger pad_int = new BigInteger(pad);
            pad_int = pad_int << ((input_byte_length + 1)*8);
            BigInteger result = new BigInteger(1);
            result = result << ((pad_len - 2)*8);
            result = result | pad_int;
            result = result | input;

            return result;
        }

        public static BigInteger StripPKCS1Pad(BigInteger input, int type)
        {
            byte[] strip = input.getBytes();
            int i;

            if (strip[0] != type) throw new Exception(String.Format("Invalid PKCS1 padding {0}", type));

            for (i = 1; i < strip.Length; i++)
            {
                if (strip[i] == 0) break;

                if (type == 0x01 && strip[i] != (byte) 0xff)
                    throw new Exception("Invalid PKCS1 padding, corrupt data");
            }

            if (i == strip.Length)
                throw new Exception("Invalid PKCS1 padding, corrupt data");

            byte[] val = new byte[strip.Length - i];
            Array.Copy(strip, i, val, 0, val.Length);
            return new BigInteger(val);
        }
    }
}