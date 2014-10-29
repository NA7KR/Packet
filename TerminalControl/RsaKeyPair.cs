using System;
using System.Security.Cryptography;

namespace PacketComs
{
    public class RsaKeyPair : KeyPair, ISigner, IVerifier
    {
        private RSAPublicKey _publickey;
        private BigInteger _d;
        private BigInteger _u;
        private BigInteger _p;
        private BigInteger _q;

        public RsaKeyPair(BigInteger e, BigInteger d, BigInteger n, BigInteger u, BigInteger p, BigInteger q)
        {
            _publickey = new RSAPublicKey(e, n);
            _d = d;
            _u = u;
            _p = p;
            _q = q;
        }

        public BigInteger D
        {
            get { return _d; }
        }

        public BigInteger U
        {
            get { return _u; }
        }

        public BigInteger P
        {
            get { return _p; }
        }

        public BigInteger Q
        {
            get { return _q; }
        }

        public override PublicKeyAlgorithm Algorithm
        {
            get { return PublicKeyAlgorithm.RSA; }
        }

        public byte[] Sign(byte[] data)
        {
            BigInteger pe = PrimeExponent(_d, _p);
            BigInteger qe = PrimeExponent(_d, _q);

            BigInteger result = SignCore(new BigInteger(data), pe, qe);

            return result.getBytes();
        }

        public void Verify(byte[] data, byte[] expected)
        {
            _publickey.Verify(data, expected);
        }

        public byte[] SignWithSha1(byte[] data)
        {
            byte[] hash = new SHA1CryptoServiceProvider().ComputeHash(data);

            byte[] buf = new byte[hash.Length + PKIUtil.SHA1_ASN_ID.Length];
            Array.Copy(PKIUtil.SHA1_ASN_ID, 0, buf, 0, PKIUtil.SHA1_ASN_ID.Length);
            Array.Copy(hash, 0, buf, PKIUtil.SHA1_ASN_ID.Length, hash.Length);

            BigInteger x = new BigInteger(buf);
            //Debug.WriteLine(x.ToHexString());
            int padLen = (_publickey._n.bitCount() + 7)/8;

            x = RSAUtil.PKCS1PadType1(x, padLen);
            byte[] result = Sign(x.getBytes());
            return result;
        }

        private BigInteger SignCore(BigInteger input, BigInteger pe, BigInteger qe)
        {
            BigInteger p2 = (input%_p).modPow(pe, _p);
            BigInteger q2 = (input%_q).modPow(qe, _q);

            if (p2 == q2)
                return p2;

            BigInteger k = (q2 - p2)%_q;
            if (k.IsNegative) k += _q; //in .NET, k is negative when _q is negative
            k = (k*_u)%_q;

            BigInteger result = k*_p + p2;

            return result;
        }

        public override PublicKey PublicKey
        {
            get { return _publickey; }
        }

        private static BigInteger PrimeExponent(BigInteger privateExponent, BigInteger prime)
        {
            BigInteger pe = prime - new BigInteger(1);
            return privateExponent%pe;
        }

        public RSAParameters ToRsaParameters()
        {
            RSAParameters p = new RSAParameters();
            p.D = _d.getBytes();
            p.Exponent = _publickey.Exponent.getBytes();
            p.Modulus = _publickey.Modulus.getBytes();
            p.P = _p.getBytes();
            p.Q = _q.getBytes();
            BigInteger pe = PrimeExponent(_d, _p);
            BigInteger qe = PrimeExponent(_d, _q);
            p.DP = pe.getBytes();
            p.DQ = qe.getBytes();
            p.InverseQ = _u.getBytes();
            return p;
        }

        public static RsaKeyPair GenerateNew(int bits, Random rnd)
        {
            BigInteger one = new BigInteger(1);
            BigInteger p = null;
            BigInteger q = null;
            BigInteger t = null;
            BigInteger p_1 = null;
            BigInteger q_1 = null;
            BigInteger phi = null;
            BigInteger G = null;
            BigInteger F = null;
            BigInteger e = null;
            BigInteger d = null;
            BigInteger u = null;
            BigInteger n = null;

            bool finished = false;

            while (!finished)
            {
                p = BigInteger.genPseudoPrime(bits/2, 64, rnd);
                q = BigInteger.genPseudoPrime(bits - (bits/2), 64, rnd);

                if (p == 0)
                {
                    continue;
                }
                else if (q < p)
                {
                    t = q;
                    q = p;
                    p = t;
                }

                t = p.gcd(q);
                if (t != one)
                {
                    continue;
                }

                p_1 = p - one;
                q_1 = q - one;
                phi = p_1*q_1;
                G = p_1.gcd(q_1);
                F = phi/G;

                e = one << 5;
                e = e - one;
                do
                {
                    e = e + (one + one);
                    t = e.gcd(phi);
                } while (t != one);

                // !!! d = e.modInverse(F);
                d = e.modInverse(phi);
                n = p*q;
                u = p.modInverse(q);

                finished = true;
            }

            return new RsaKeyPair(e, d, n, u, p, q);
        }
    }
}