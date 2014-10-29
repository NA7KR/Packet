using System;
using Routrek.PKI;

namespace PacketComs
{
    public class DsaKeyPair : KeyPair, ISigner, IVerifier
    {
        private DsaPublicKey _publickey;
        private BigInteger _x;

        public DsaKeyPair(BigInteger p, BigInteger g, BigInteger q, BigInteger y, BigInteger x)
        {
            _publickey = new DsaPublicKey(p, g, q, y);
            _x = x;
        }

        public override PublicKeyAlgorithm Algorithm
        {
            get { return PublicKeyAlgorithm.DSA; }
        }

        public override PublicKey PublicKey
        {
            get { return _publickey; }
        }

        public BigInteger X
        {
            get { return _x; }
        }

        public byte[] Sign(byte[] data)
        {
            BigInteger r = _publickey._g.modPow(_x, _publickey._p)%_publickey._q;
            BigInteger s = (_x.modInverse(_publickey._q)*(new BigInteger(data) + _x*r))%_publickey._q;

            byte[] result = new byte[data.Length*2];
            byte[] br = r.getBytes();
            byte[] bs = s.getBytes();
            Array.Copy(br, 0, result, data.Length - br.Length, br.Length);
            Array.Copy(bs, 0, result, data.Length*2 - bs.Length, bs.Length);

            return result;
        }

        public void Verify(byte[] data, byte[] expecteddata)
        {
            _publickey.Verify(data, expecteddata);
        }

        public static DsaKeyPair GenerateNew(int bits, Random random)
        {
            BigInteger one = new BigInteger(1);
            BigInteger[] pq = FindRandomStrongPrime((uint) bits, 160, random);
            BigInteger p = pq[0], q = pq[1];
            BigInteger g = FindRandomGenerator(q, p, random);

            BigInteger x;
            do
            {
                x = new BigInteger();
                x.genRandomBits(q.bitCount(), random);
            } while ((x < one) || (x > q));

            BigInteger y = g.modPow(x, p);

            return new DsaKeyPair(p, g, q, y, x);
        }

        private static BigInteger[] FindRandomStrongPrime(uint primeBits, int orderBits, Random random)
        {
            BigInteger one = new BigInteger(1);
            BigInteger u, aux, aux2;
            long[] table_q, table_u, prime_table;
            PrimeSieve sieve = new PrimeSieve(16000);
            uint tableCount = sieve.AvailablePrimes() - 1;
            int i, j;
            bool flag;
            BigInteger prime = null, order;

            order = BigInteger.genPseudoPrime(orderBits, 20, random);

            prime_table = new long[tableCount];
            table_q = new long[tableCount];
            table_u = new long[tableCount];

            i = 0;
            for (int pN = 2; pN != 0; pN = sieve.getNextPrime(pN), i++)
            {
                prime_table[i] = pN;
            }

            for (i = 0; i < tableCount; i++)
            {
                table_q[i] =
                    (((order%new BigInteger(prime_table[i])).LongValue())*
                     2)%prime_table[i];
            }

            while (true)
            {
                u = new BigInteger();
                u.genRandomBits((int) primeBits, random);
                u.setBit(primeBits - 1);
                aux = order << 1;
                aux2 = u%aux;
                u = u - aux2;
                u = u + one;

                if (u.bitCount() <= (primeBits - 1))
                    continue;

                for (j = 0; j < tableCount; j++)
                {
                    table_u[j] =
                        (u%new BigInteger(prime_table[j])).LongValue();
                }

                aux2 = order << 1;

                for (i = 0; i < (1 << 24); i++)
                {
                    long cur_p;
                    long value;

                    flag = true;
                    for (j = 1; j < tableCount; j++)
                    {
                        cur_p = prime_table[j];
                        value = table_u[j];
                        if (value >= cur_p)
                            value -= cur_p;
                        if (value == 0)
                            flag = false;
                        table_u[j] = value + table_q[j];
                    }
                    if (!flag)
                        continue;

                    aux = aux2*new BigInteger(i);
                    prime = u + aux;

                    if (prime.bitCount() > primeBits)
                        continue;

                    if (prime.isProbablePrime(20))
                        break;
                }

                if (i < (1 << 24))
                    break;
            }

            return new[] {prime, order};
        }

        private static BigInteger FindRandomGenerator(BigInteger order, BigInteger modulo, Random random)
        {
            BigInteger one = new BigInteger(1);
            BigInteger aux = modulo - new BigInteger(1);
            BigInteger t = aux%order;
            BigInteger generator;

            if (t.LongValue() != 0)
            {
                return null;
            }

            t = aux/order;

            while (true)
            {
                generator = new BigInteger();
                generator.genRandomBits(modulo.bitCount(), random);
                generator = generator%modulo;
                generator = generator.modPow(t, modulo);
                if (generator != one)
                    break;
            }

            aux = generator.modPow(order, modulo);

            if (aux != one)
            {
                return null;
            }

            return generator;
        }
    }
}