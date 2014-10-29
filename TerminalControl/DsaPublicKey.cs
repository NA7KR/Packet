using System;

namespace PacketComs
{
    public class DsaPublicKey : PublicKey, IVerifier
    {
        internal BigInteger _p;
        internal BigInteger _g;
        internal BigInteger _q;
        internal BigInteger _y;

        public DsaPublicKey(BigInteger p, BigInteger g, BigInteger q, BigInteger y)
        {
            _p = p;
            _g = g;
            _q = q;
            _y = y;
        }

        public override PublicKeyAlgorithm Algorithm
        {
            get { return PublicKeyAlgorithm.DSA; }
        }

        public BigInteger P
        {
            get { return _p; }
        }

        public BigInteger Q
        {
            get { return _q; }
        }

        public BigInteger G
        {
            get { return _g; }
        }

        public BigInteger Y
        {
            get { return _y; }
        }

        public override void WriteTo(IKeyWriter writer)
        {
            writer.Write(_p);
            writer.Write(_q);
            writer.Write(_g);
            writer.Write(_y);
        }

        public void Verify(byte[] data, byte[] expecteddata)
        {
            byte[] first = new byte[data.Length/2];
            byte[] second = new byte[data.Length/2];
            Array.Copy(data, 0, first, 0, first.Length);
            Array.Copy(data, first.Length, second, 0, second.Length);
            BigInteger r = new BigInteger(first);
            BigInteger s = new BigInteger(second);

            BigInteger w = s.modInverse(_q);
            BigInteger u1 = (new BigInteger(expecteddata)*w)%_q;
            BigInteger u2 = (r*w)%_q;
            BigInteger v = ((_g.modPow(u1, _p)*_y.modPow(u2, _p))%_p)%_q;
            //Debug.WriteLine(DebugUtil.DumpByteArray(v.GetBytes()));
            //Debug.WriteLine(DebugUtil.DumpByteArray(r.GetBytes()));
            if (v != r) throw new VerifyException("Failed to verify");
        }
    }
}