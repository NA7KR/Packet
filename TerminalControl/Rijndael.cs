using System;

namespace PacketComs
{
    public class Rijndael
    {
        private byte[] _IV;
        private int[][] _ke; // encryption round keys
        private int[][] _kd; // decryption round keys
        private int _rounds;

        public Rijndael()
        {
            _IV = new byte[GetBlockSize()];
        }

        public int GetBlockSize()
        {
            return BlockSize;
        }

        ///////////////////////////////////////////////
        // set _IV
        ///////////////////////////////////////////////
        public void SetIv(byte[] newiv)
        {
            Array.Copy(newiv, 0, _IV, 0, _IV.Length);
        }

        ///////////////////////////////////////////////
        // set KEY
        ///////////////////////////////////////////////
        public void InitializeKey(byte[] key)
        {
            if (key == null)
                throw new Exception("Empty key");
            //128bit or 192bit or 256bit
            if (!(key.Length == 16 || key.Length == 24 || key.Length == 32))
                throw new Exception("Incorrect key length");

            _rounds = GetRounds(key.Length, GetBlockSize());
            _ke = new int[_rounds + 1][];
            _kd = new int[_rounds + 1][];
            int i, j;
            for (i = 0; i < _rounds + 1; i++)
            {
                _ke[i] = new int[Bc];
                _kd[i] = new int[Bc];
            }

            int roundKeyCount = (_rounds + 1)*Bc;
            int kc = key.Length/4;
            int[] tk = new int[kc];

            for (i = 0, j = 0; i < kc;)
            {
                tk[i++] = (key[j++] & 0xFF) << 24 |
                          (key[j++] & 0xFF) << 16 |
                          (key[j++] & 0xFF) << 8 |
                          (key[j++] & 0xFF);
            }

            int t = 0;
            for (j = 0; (j < kc) && (t < roundKeyCount); j++, t++)
            {
                _ke[t/Bc][t%Bc] = tk[j];
                _kd[_rounds - (t/Bc)][t%Bc] = tk[j];
            }
            int tt, rconpointer = 0;
            while (t < roundKeyCount)
            {
                tt = tk[kc - 1];
                tk[0] ^= (S[(tt >> 16) & 0xFF] & 0xFF) << 24 ^
                         (S[(tt >> 8) & 0xFF] & 0xFF) << 16 ^
                         (S[tt & 0xFF] & 0xFF) << 8 ^
                         (S[(tt >> 24) & 0xFF] & 0xFF) ^
                         (Rcon[rconpointer++] & 0xFF) << 24;

                if (kc != 8)
                {
                    for (i = 1, j = 0; i < kc;)
                        tk[i++] ^= tk[j++];
                }
                else
                {
                    for (i = 1, j = 0; i < kc/2;)
                        tk[i++] ^= tk[j++];
                    tt = tk[kc/2 - 1];
                    tk[kc/2] ^= (S[tt & 0xFF] & 0xFF) ^
                                (S[(tt >> 8) & 0xFF] & 0xFF) << 8 ^
                                (S[(tt >> 16) & 0xFF] & 0xFF) << 16 ^
                                (S[(tt >> 24) & 0xFF] & 0xFF) << 24;
                    for (j = kc/2, i = j + 1; i < kc;)
                        tk[i++] ^= tk[j++];
                }
                for (j = 0; (j < kc) && (t < roundKeyCount); j++, t++)
                {
                    _ke[t/Bc][t%Bc] = tk[j];
                    _kd[_rounds - (t/Bc)][t%Bc] = tk[j];
                }
            }
            for (int r = 1; r < _rounds; r++)
            {
                for (j = 0; j < Bc; j++)
                {
                    tt = _kd[r][j];
                    _kd[r][j] = U1[(tt >> 24) & 0xFF] ^
                                U2[(tt >> 16) & 0xFF] ^
                                U3[(tt >> 8) & 0xFF] ^
                                U4[tt & 0xFF];
                }
            }
        }

        public static int GetRounds(int keySize, int blockSize)
        {
            switch (keySize)
            {
                case 16:
                    return blockSize == 16 ? 10 : (blockSize == 24 ? 12 : 14);
                case 24:
                    return blockSize != 32 ? 12 : 14;
                default:
                    return 14;
            }
        }

        public void EncryptCbc(byte[] input, int inputOffset, int inputLen, byte[] output, int outputOffset)
        {
            int blockSize = GetBlockSize();
            int nBlocks = inputLen/blockSize;
            for (int bc = 0; bc < nBlocks; bc++)
            {
                CipherUtil.BlockXor(input, inputOffset, blockSize, _IV, 0);
                BlockEncrypt(_IV, 0, output, outputOffset);
                Array.Copy(output, outputOffset, _IV, 0, blockSize);
                inputOffset += blockSize;
                outputOffset += blockSize;
            }
        }

        public void DecryptCbc(byte[] input, int inputOffset, int inputLen, byte[] output, int outputOffset)
        {
            int blockSize = GetBlockSize();
            byte[] tmpBlk = new byte[blockSize];
            int nBlocks = inputLen/blockSize;
            for (int bc = 0; bc < nBlocks; bc++)
            {
                BlockDecrypt(input, inputOffset, tmpBlk, 0);
                for (int i = 0; i < blockSize; i++)
                {
                    tmpBlk[i] ^= _IV[i];
                    _IV[i] = input[inputOffset + i];
                    output[outputOffset + i] = tmpBlk[i];
                }
                inputOffset += blockSize;
                outputOffset += blockSize;
            }
        }

        public void BlockEncrypt(byte[] src, int inOffset, byte[] dst, int outOffset)
        {
            int[] ker = _ke[0];

            int t0 = ((src[inOffset++] & 0xFF) << 24 |
                      (src[inOffset++] & 0xFF) << 16 |
                      (src[inOffset++] & 0xFF) << 8 |
                      (src[inOffset++] & 0xFF)) ^ ker[0];
            int t1 = ((src[inOffset++] & 0xFF) << 24 |
                      (src[inOffset++] & 0xFF) << 16 |
                      (src[inOffset++] & 0xFF) << 8 |
                      (src[inOffset++] & 0xFF)) ^ ker[1];
            int t2 = ((src[inOffset++] & 0xFF) << 24 |
                      (src[inOffset++] & 0xFF) << 16 |
                      (src[inOffset++] & 0xFF) << 8 |
                      (src[inOffset++] & 0xFF)) ^ ker[2];
            int t3 = ((src[inOffset++] & 0xFF) << 24 |
                      (src[inOffset++] & 0xFF) << 16 |
                      (src[inOffset++] & 0xFF) << 8 |
                      (src[inOffset++] & 0xFF)) ^ ker[3];

            int a0, a1, a2, a3;
            for (int r = 1; r < _rounds; r++)
            {
                ker = _ke[r];
                a0 = (T1[(t0 >> 24) & 0xFF] ^
                      T2[(t1 >> 16) & 0xFF] ^
                      T3[(t2 >> 8) & 0xFF] ^
                      T4[t3 & 0xFF]) ^ ker[0];
                a1 = (T1[(t1 >> 24) & 0xFF] ^
                      T2[(t2 >> 16) & 0xFF] ^
                      T3[(t3 >> 8) & 0xFF] ^
                      T4[t0 & 0xFF]) ^ ker[1];
                a2 = (T1[(t2 >> 24) & 0xFF] ^
                      T2[(t3 >> 16) & 0xFF] ^
                      T3[(t0 >> 8) & 0xFF] ^
                      T4[t1 & 0xFF]) ^ ker[2];
                a3 = (T1[(t3 >> 24) & 0xFF] ^
                      T2[(t0 >> 16) & 0xFF] ^
                      T3[(t1 >> 8) & 0xFF] ^
                      T4[t2 & 0xFF]) ^ ker[3];
                t0 = a0;
                t1 = a1;
                t2 = a2;
                t3 = a3;
            }

            ker = _ke[_rounds];
            int tt = ker[0];
            dst[outOffset + 0] = (byte) (S[(t0 >> 24) & 0xFF] ^ (tt >> 24));
            dst[outOffset + 1] = (byte) (S[(t1 >> 16) & 0xFF] ^ (tt >> 16));
            dst[outOffset + 2] = (byte) (S[(t2 >> 8) & 0xFF] ^ (tt >> 8));
            dst[outOffset + 3] = (byte) (S[t3 & 0xFF] ^ tt);
            tt = ker[1];
            dst[outOffset + 4] = (byte) (S[(t1 >> 24) & 0xFF] ^ (tt >> 24));
            dst[outOffset + 5] = (byte) (S[(t2 >> 16) & 0xFF] ^ (tt >> 16));
            dst[outOffset + 6] = (byte) (S[(t3 >> 8) & 0xFF] ^ (tt >> 8));
            dst[outOffset + 7] = (byte) (S[t0 & 0xFF] ^ tt);
            tt = ker[2];
            dst[outOffset + 8] = (byte) (S[(t2 >> 24) & 0xFF] ^ (tt >> 24));
            dst[outOffset + 9] = (byte) (S[(t3 >> 16) & 0xFF] ^ (tt >> 16));
            dst[outOffset + 10] = (byte) (S[(t0 >> 8) & 0xFF] ^ (tt >> 8));
            dst[outOffset + 11] = (byte) (S[t1 & 0xFF] ^ tt);
            tt = ker[3];
            dst[outOffset + 12] = (byte) (S[(t3 >> 24) & 0xFF] ^ (tt >> 24));
            dst[outOffset + 13] = (byte) (S[(t0 >> 16) & 0xFF] ^ (tt >> 16));
            dst[outOffset + 14] = (byte) (S[(t1 >> 8) & 0xFF] ^ (tt >> 8));
            dst[outOffset + 15] = (byte) (S[t2 & 0xFF] ^ tt);
        }

        public void BlockDecrypt(byte[] src, int inOffset, byte[] dst, int outOffset)
        {
            int[] kdr = _kd[0];

            int t0 = ((src[inOffset++] & 0xFF) << 24 |
                      (src[inOffset++] & 0xFF) << 16 |
                      (src[inOffset++] & 0xFF) << 8 |
                      (src[inOffset++] & 0xFF)) ^ kdr[0];
            int t1 = ((src[inOffset++] & 0xFF) << 24 |
                      (src[inOffset++] & 0xFF) << 16 |
                      (src[inOffset++] & 0xFF) << 8 |
                      (src[inOffset++] & 0xFF)) ^ kdr[1];
            int t2 = ((src[inOffset++] & 0xFF) << 24 |
                      (src[inOffset++] & 0xFF) << 16 |
                      (src[inOffset++] & 0xFF) << 8 |
                      (src[inOffset++] & 0xFF)) ^ kdr[2];
            int t3 = ((src[inOffset++] & 0xFF) << 24 |
                      (src[inOffset++] & 0xFF) << 16 |
                      (src[inOffset++] & 0xFF) << 8 |
                      (src[inOffset++] & 0xFF)) ^ kdr[3];

            int a0, a1, a2, a3;
            for (int r = 1; r < _rounds; r++)
            {
                kdr = _kd[r];
                a0 = (T5[(t0 >> 24) & 0xFF] ^
                      T6[(t3 >> 16) & 0xFF] ^
                      T7[(t2 >> 8) & 0xFF] ^
                      T8[t1 & 0xFF]) ^ kdr[0];
                a1 = (T5[(t1 >> 24) & 0xFF] ^
                      T6[(t0 >> 16) & 0xFF] ^
                      T7[(t3 >> 8) & 0xFF] ^
                      T8[t2 & 0xFF]) ^ kdr[1];
                a2 = (T5[(t2 >> 24) & 0xFF] ^
                      T6[(t1 >> 16) & 0xFF] ^
                      T7[(t0 >> 8) & 0xFF] ^
                      T8[t3 & 0xFF]) ^ kdr[2];
                a3 = (T5[(t3 >> 24) & 0xFF] ^
                      T6[(t2 >> 16) & 0xFF] ^
                      T7[(t1 >> 8) & 0xFF] ^
                      T8[t0 & 0xFF]) ^ kdr[3];
                t0 = a0;
                t1 = a1;
                t2 = a2;
                t3 = a3;
            }

            kdr = _kd[_rounds];
            int tt = kdr[0];
            dst[outOffset + 0] = (byte) (Si[(t0 >> 24) & 0xFF] ^ (tt >> 24));
            dst[outOffset + 1] = (byte) (Si[(t3 >> 16) & 0xFF] ^ (tt >> 16));
            dst[outOffset + 2] = (byte) (Si[(t2 >> 8) & 0xFF] ^ (tt >> 8));
            dst[outOffset + 3] = (byte) (Si[t1 & 0xFF] ^ tt);
            tt = kdr[1];
            dst[outOffset + 4] = (byte) (Si[(t1 >> 24) & 0xFF] ^ (tt >> 24));
            dst[outOffset + 5] = (byte) (Si[(t0 >> 16) & 0xFF] ^ (tt >> 16));
            dst[outOffset + 6] = (byte) (Si[(t3 >> 8) & 0xFF] ^ (tt >> 8));
            dst[outOffset + 7] = (byte) (Si[t2 & 0xFF] ^ tt);
            tt = kdr[2];
            dst[outOffset + 8] = (byte) (Si[(t2 >> 24) & 0xFF] ^ (tt >> 24));
            dst[outOffset + 9] = (byte) (Si[(t1 >> 16) & 0xFF] ^ (tt >> 16));
            dst[outOffset + 10] = (byte) (Si[(t0 >> 8) & 0xFF] ^ (tt >> 8));
            dst[outOffset + 11] = (byte) (Si[t3 & 0xFF] ^ tt);
            tt = kdr[3];
            dst[outOffset + 12] = (byte) (Si[(t3 >> 24) & 0xFF] ^ (tt >> 24));
            dst[outOffset + 13] = (byte) (Si[(t2 >> 16) & 0xFF] ^ (tt >> 16));
            dst[outOffset + 14] = (byte) (Si[(t1 >> 8) & 0xFF] ^ (tt >> 8));
            dst[outOffset + 15] = (byte) (Si[t0 & 0xFF] ^ tt);
        }

        /// <summary>
        /// constants
        /// </summary>
        private const int BlockSize = 16;

        private const int Bc = 4;

        private static readonly int[] Alog = new int[256];
        private static readonly int[] Log = new int[256];

        private static readonly byte[] S = new byte[256];
        private static readonly byte[] Si = new byte[256];
        private static readonly int[] T1 = new int[256];
        private static readonly int[] T2 = new int[256];
        private static readonly int[] T3 = new int[256];
        private static readonly int[] T4 = new int[256];
        private static readonly int[] T5 = new int[256];
        private static readonly int[] T6 = new int[256];
        private static readonly int[] T7 = new int[256];
        private static readonly int[] T8 = new int[256];
        private static readonly int[] U1 = new int[256];
        private static readonly int[] U2 = new int[256];
        private static readonly int[] U3 = new int[256];
        private static readonly int[] U4 = new int[256];
        private static readonly byte[] Rcon = new byte[30];

        ///////////////////////////////////
        //class initialization
        ///////////////////////////////////
        static Rijndael()
        {
            int ROOT = 0x11B;
            int i, j = 0;

            Alog[0] = 1;
            for (i = 1; i < 256; i++)
            {
                j = (Alog[i - 1] << 1) ^ Alog[i - 1];
                if ((j & 0x100) != 0) j ^= ROOT;
                Alog[i] = j;
            }
            for (i = 1; i < 255; i++) Log[Alog[i]] = i;
            byte[,] a =
            {
                {1, 1, 1, 1, 1, 0, 0, 0},
                {0, 1, 1, 1, 1, 1, 0, 0},
                {0, 0, 1, 1, 1, 1, 1, 0},
                {0, 0, 0, 1, 1, 1, 1, 1},
                {1, 0, 0, 0, 1, 1, 1, 1},
                {1, 1, 0, 0, 0, 1, 1, 1},
                {1, 1, 1, 0, 0, 0, 1, 1},
                {1, 1, 1, 1, 0, 0, 0, 1}
            };
            byte[] b = {0, 1, 1, 0, 0, 0, 1, 1};

            int t;
            byte[,] box = new byte[256, 8];
            box[1, 7] = 1;
            for (i = 2; i < 256; i++)
            {
                j = Alog[255 - Log[i]];
                for (t = 0; t < 8; t++)
                {
                    box[i, t] = (byte) ((j >> (7 - t)) & 0x01);
                }
            }

            byte[,] cox = new byte[256, 8];
            for (i = 0; i < 256; i++)
            {
                for (t = 0; t < 8; t++)
                {
                    cox[i, t] = b[t];
                    for (j = 0; j < 8; j++)
                        cox[i, t] ^= (byte) (a[t, j]*box[i, j]);
                }
            }

            for (i = 0; i < 256; i++)
            {
                S[i] = (byte) (cox[i, 0] << 7);
                for (t = 1; t < 8; t++)
                    S[i] ^= (byte) (cox[i, t] << (7 - t));
                Si[S[i] & 0xFF] = (byte) i;
            }
            byte[][] g = new byte[4][];
            g[0] = new byte[] {2, 1, 1, 3};
            g[1] = new byte[] {3, 2, 1, 1};
            g[2] = new byte[] {1, 3, 2, 1};
            g[3] = new byte[] {1, 1, 3, 2};

            byte[,] aa = new byte[4, 8];
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++) aa[i, j] = g[i][j];
                aa[i, i + 4] = 1;
            }
            byte pivot, tmp;
            byte[][] iG = new byte[4][];
            for (i = 0; i < 4; i++)
                iG[i] = new byte[4];

            for (i = 0; i < 4; i++)
            {
                pivot = aa[i, i];
                if (pivot == 0)
                {
                    t = i + 1;
                    while ((aa[t, i] == 0) && (t < 4))
                        t++;
                    if (t != 4)
                    {
                        for (j = 0; j < 8; j++)
                        {
                            tmp = aa[i, j];
                            aa[i, j] = aa[t, j];
                            aa[t, j] = tmp;
                        }
                        pivot = aa[i, i];
                    }
                }
                for (j = 0; j < 8; j++)
                    if (aa[i, j] != 0)
                        aa[i, j] = (byte)
                            Alog[(255 + Log[aa[i, j] & 0xFF] - Log[pivot & 0xFF])%255];
                for (t = 0; t < 4; t++)
                    if (i != t)
                    {
                        for (j = i + 1; j < 8; j++)
                            aa[t, j] ^= (byte) Mul(aa[i, j], aa[t, i]);
                        aa[t, i] = 0;
                    }
            }

            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++) iG[i][j] = aa[i, j + 4];

            int s;
            for (t = 0; t < 256; t++)
            {
                s = S[t];
                T1[t] = Mul4(s, g[0]);
                T2[t] = Mul4(s, g[1]);
                T3[t] = Mul4(s, g[2]);
                T4[t] = Mul4(s, g[3]);

                s = Si[t];
                T5[t] = Mul4(s, iG[0]);
                T6[t] = Mul4(s, iG[1]);
                T7[t] = Mul4(s, iG[2]);
                T8[t] = Mul4(s, iG[3]);

                U1[t] = Mul4(t, iG[0]);
                U2[t] = Mul4(t, iG[1]);
                U3[t] = Mul4(t, iG[2]);
                U4[t] = Mul4(t, iG[3]);
            }

            Rcon[0] = 1;
            int r = 1;
            for (t = 1; t < 30;) Rcon[t++] = (byte) (r = Mul(2, r));
        }

        private static int Mul(int a, int b)
        {
            return (a != 0 && b != 0)
                ? Alog[(Log[a & 0xFF] + Log[b & 0xFF])%255]
                : 0;
        }

        private static int Mul4(int a, byte[] b)
        {
            if (a == 0) return 0;
            a = Log[a & 0xFF];
            int a0 = (b[0] != 0) ? Alog[(a + Log[b[0] & 0xFF])%255] & 0xFF : 0;
            int a1 = (b[1] != 0) ? Alog[(a + Log[b[1] & 0xFF])%255] & 0xFF : 0;
            int a2 = (b[2] != 0) ? Alog[(a + Log[b[2] & 0xFF])%255] & 0xFF : 0;
            int a3 = (b[3] != 0) ? Alog[(a + Log[b[3] & 0xFF])%255] & 0xFF : 0;
            return a0 << 24 | a1 << 16 | a2 << 8 | a3;
        }
    }
}