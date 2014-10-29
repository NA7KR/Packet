using System.Security.Cryptography;

namespace PacketComs
{

    internal interface ICipher
    {
        void Encrypt(byte[] data, int offset, int len, byte[] result, int resultOffset);
        void Decrypt(byte[] data, int offset, int len, byte[] result, int resultOffset);
        int BlockSize { get; }
    }

    internal class BlowfishCipher1 : ICipher
    {
        private Blowfish _bf;

        public BlowfishCipher1(byte[] key)
        {
            _bf = new Blowfish();
            _bf.InitializeKey(key);
        }

        public void Encrypt(byte[] data, int offset, int len, byte[] result, int resultOffset)
        {
            _bf.EncryptSsh1Style(data, offset, len, result, resultOffset);
        }

        public void Decrypt(byte[] data, int offset, int len, byte[] result, int resultOffset)
        {
            _bf.DecryptSsh1Style(data, offset, len, result, resultOffset);
        }

        public int BlockSize
        {
            get { return 8; }
        }
    }

    internal class BlowfishCipher2 : ICipher
    {
        private Blowfish _bf;

        public BlowfishCipher2(byte[] key)
        {
            _bf = new Blowfish();
            _bf.InitializeKey(key);
        }

        public BlowfishCipher2(byte[] key, byte[] iv)
        {
            _bf = new Blowfish();
            _bf.SetIv(iv);
            _bf.InitializeKey(key);
        }

        public void Encrypt(byte[] data, int offset, int len, byte[] result, int resultOffset)
        {
            _bf.EncryptCbc(data, offset, len, result, resultOffset);
        }

        public void Decrypt(byte[] data, int offset, int len, byte[] result, int resultOffset)
        {
            _bf.DecryptCbc(data, offset, len, result, resultOffset);
        }

        public int BlockSize
        {
            get { return 8; }
        }
    }

    internal class TripleDesCipher1 : ICipher
    {
        private Des _DESCipher1;
        private Des _DESCipher2;
        private Des _DESCipher3;

        public TripleDesCipher1(byte[] key)
        {
            _DESCipher1 = new Des();
            _DESCipher2 = new Des();
            _DESCipher3 = new Des();

            _DESCipher1.InitializeKey(key, 0);
            _DESCipher2.InitializeKey(key, 8);
            _DESCipher3.InitializeKey(key, 16);
        }

        public void Encrypt(byte[] data, int offset, int len, byte[] result, int resultOffset)
        {
            byte[] buf1 = new byte[len];
            _DESCipher1.EncryptCbc(data, offset, len, result, resultOffset);
            _DESCipher2.DecryptCbc(result, resultOffset, buf1.Length, buf1, 0);
            _DESCipher3.EncryptCbc(buf1, 0, buf1.Length, result, resultOffset);
        }

        public void Decrypt(byte[] data, int offset, int len, byte[] result, int resultOffset)
        {
            byte[] buf1 = new byte[len];
            _DESCipher3.DecryptCbc(data, offset, len, result, resultOffset);
            _DESCipher2.EncryptCbc(result, resultOffset, buf1.Length, buf1, 0);
            _DESCipher1.DecryptCbc(buf1, 0, buf1.Length, result, resultOffset);
        }

        public int BlockSize
        {
            get { return 8; }
        }
    }

    internal class TripleDesCipher2 : ICipher
    {
        private Des _DESCipher1;
        private Des _DESCipher2;
        private Des _DESCipher3;

        public TripleDesCipher2(byte[] key)
        {
            _DESCipher1 = new Des();
            _DESCipher2 = new Des();
            _DESCipher3 = new Des();

            _DESCipher1.InitializeKey(key, 0);
            _DESCipher2.InitializeKey(key, 8);
            _DESCipher3.InitializeKey(key, 16);
        }

        public TripleDesCipher2(byte[] key, byte[] iv)
        {
            _DESCipher1 = new Des();
            _DESCipher1.SetIv(iv);
            _DESCipher2 = new Des();
            _DESCipher2.SetIv(iv);
            _DESCipher3 = new Des();
            _DESCipher3.SetIv(iv);

            _DESCipher1.InitializeKey(key, 0);
            _DESCipher2.InitializeKey(key, 8);
            _DESCipher3.InitializeKey(key, 16);
        }

        public void Encrypt(byte[] data, int offset, int len, byte[] result, int resultOffset)
        {
            byte[] buf1 = new byte[8];
            int n = 0;
            while (n < len)
            {
                _DESCipher1.EncryptCbc(data, offset + n, 8, result, resultOffset + n);
                _DESCipher2.DecryptCbc(result, resultOffset + n, 8, buf1, 0);
                _DESCipher3.EncryptCbc(buf1, 0, 8, result, resultOffset + n);
                _DESCipher1.SetIv(result, resultOffset + n);
                _DESCipher2.SetIv(result, resultOffset + n);
                _DESCipher3.SetIv(result, resultOffset + n);
                n += 8;
            }
        }

        public void Decrypt(byte[] data, int offset, int len, byte[] result, int resultOffset)
        {
            byte[] buf1 = new byte[8];
            int n = 0;
            while (n < len)
            {
                _DESCipher3.DecryptCbc(data, offset + n, 8, result, resultOffset + n);
                _DESCipher2.EncryptCbc(result, resultOffset + n, 8, buf1, 0);
                _DESCipher1.DecryptCbc(buf1, 0, 8, result, resultOffset + n);
                _DESCipher3.SetIv(data, offset + n);
                _DESCipher2.SetIv(data, offset + n);
                _DESCipher1.SetIv(data, offset + n);
                n += 8;
            }
        }

        public int BlockSize
        {
            get { return 8; }
        }
    }

    internal class RijindaelCipher2 : ICipher
    {
        private Rijndael _rijindael;

        public RijindaelCipher2(byte[] key, byte[] iv)
        {
            _rijindael = new Rijndael();
            _rijindael.SetIv(iv);
            _rijindael.InitializeKey(key);
        }

        public void Encrypt(byte[] data, int offset, int len, byte[] result, int resultOffset)
        {
            _rijindael.EncryptCbc(data, offset, len, result, resultOffset);
        }

        public void Decrypt(byte[] data, int offset, int len, byte[] result, int resultOffset)
        {
            _rijindael.DecryptCbc(data, offset, len, result, resultOffset);
        }

        public int BlockSize
        {
            get { return _rijindael.GetBlockSize(); }
        }
    }

    /// <summary>
    /// Creates a cipher from given parameters
    /// </summary>
    internal class CipherFactory
    {
        public static ICipher CreateCipher(SSHProtocol protocol, CipherAlgorithm algorithm, byte[] key)
        {
            if (protocol == SSHProtocol.SSH1)
            {
                switch (algorithm)
                {
                    case CipherAlgorithm.TripleDes:
                        return new TripleDesCipher1(key);
                    case CipherAlgorithm.Blowfish:
                        return new BlowfishCipher1(key);
                    default:
                        throw new SSHException("unknown algorithm " + algorithm);
                }
            }
            else
            {
                switch (algorithm)
                {
                    case CipherAlgorithm.TripleDes:
                        return new TripleDesCipher2(key);
                    case CipherAlgorithm.Blowfish:
                        return new BlowfishCipher2(key);
                    default:
                        throw new SSHException("unknown algorithm " + algorithm);
                }
            }
        }

        public static ICipher CreateCipher(SSHProtocol protocol, CipherAlgorithm algorithm, byte[] key, byte[] iv)
        {
            if (protocol == SSHProtocol.SSH1)
            {
                return CreateCipher(protocol, algorithm, key);
            }
            else
            {
                switch (algorithm)
                {
                    case CipherAlgorithm.TripleDes:
                        return new TripleDesCipher2(key, iv);
                    case CipherAlgorithm.Blowfish:
                        return new BlowfishCipher2(key, iv);
                    case CipherAlgorithm.Aes128:
                        return new RijindaelCipher2(key, iv);
                    default:
                        throw new SSHException("unknown algorithm " + algorithm);
                }
            }
        }

        /// <summary>
        /// returns necessary key size from Algorithm in bytes
        /// </summary>
        public static int GetKeySize(CipherAlgorithm algorithm)
        {
            switch (algorithm)
            {
                case CipherAlgorithm.TripleDes:
                    return 24;
                case CipherAlgorithm.Blowfish:
                case CipherAlgorithm.Aes128:
                    return 16;
                default:
                    throw new SSHException("unknown algorithm " + algorithm);
            }
        }

        /// <summary>
        /// returns the block size from Algorithm in bytes
        /// </summary>
        public static int GetBlockSize(CipherAlgorithm algorithm)
        {
            switch (algorithm)
            {
                case CipherAlgorithm.TripleDes:
                case CipherAlgorithm.Blowfish:
                    return 8;
                case CipherAlgorithm.Aes128:
                    return 16;
                default:
                    throw new SSHException("unknown algorithm " + algorithm);
            }
        }

        public static string AlgorithmToSSH2Name(CipherAlgorithm algorithm)
        {
            switch (algorithm)
            {
                case CipherAlgorithm.TripleDes:
                    return "3des-cbc";
                case CipherAlgorithm.Blowfish:
                    return "blowfish-cbc";
                case CipherAlgorithm.Aes128:
                    return "aes128-cbc";
                default:
                    throw new SSHException("unknown algorithm " + algorithm);
            }
        }

        public static CipherAlgorithm SSH2NameToAlgorithm(string name)
        {
            if (name == "3des-cbc")
                return CipherAlgorithm.TripleDes;
            else if (name == "blowfish-cbc")
                return CipherAlgorithm.Blowfish;
            else if (name == "aes128-cbc")
                return CipherAlgorithm.Aes128;
            else
                throw new SSHException("Unknown algorithm " + name);
        }
    }


    /**********        MAC        ***********/

    internal interface IMac
    {
        byte[] Calc(byte[] data);
        int Size { get; }
    }

    internal class Macsha1 : IMac
    {
        private HMACSHA1 _algorithm;

        public Macsha1(byte[] key)
        {
            _algorithm = new HMACSHA1(key);
        }

        public byte[] Calc(byte[] data)
        {
            _algorithm.Initialize();
            return _algorithm.ComputeHash(data);
        }

        public int Size
        {
            get { return 20; }
        }
    }

    internal static class MACFactory
    {
        public static IMac CreateMac(MacAlgorithm algorithm, byte[] key)
        {
            if (algorithm == MacAlgorithm.HMACSHA1)
                return new Macsha1(key);
            else
                throw new SSHException("unknown algorithm" + algorithm);
        }

        public static int GetSize(MacAlgorithm algorithm)
        {
            if (algorithm == MacAlgorithm.HMACSHA1)
                return 20;
            else
                throw new SSHException("unknown algorithm" + algorithm);
        }
    }
}