namespace PacketComs
{
    public class CipherUtil
    {
        internal static uint GetIntLe(byte[] src, int offset)
        {
            return (src[offset] |
                    ((uint) (src[offset + 1]) << 8) |
                    ((uint) (src[offset + 2]) << 16) |
                    ((uint) (src[offset + 3]) << 24));
        }

        internal static void PutIntLe(uint val, byte[] dest, int offset)
        {
            dest[offset] = (byte) (val & 0xff);
            dest[offset + 1] = (byte) ((val >> 8) & 0xff);
            dest[offset + 2] = (byte) ((val >> 16) & 0xff);
            dest[offset + 3] = (byte) ((val >> 24) & 0xff);
        }

        internal static uint GetIntBe(byte[] src, int offset)
        {
            return (((uint) (src[offset]) << 24) |
                    ((uint) (src[offset + 1]) << 16) |
                    ((uint) (src[offset + 2]) << 8) |
                    src[offset + 3]);
        }

        internal static void PutIntBe(uint val, byte[] dest, int offset)
        {
            dest[offset] = (byte) ((val >> 24) & 0xff);
            dest[offset + 1] = (byte) ((val >> 16) & 0xff);
            dest[offset + 2] = (byte) ((val >> 8) & 0xff);
            dest[offset + 3] = (byte) (val & 0xff);
        }

        internal static void BlockXor(byte[] src, int sOffset, int len, byte[] dest, int dOffset)
        {
            for (; len > 0; len--)
                dest[dOffset++] ^= src[sOffset++];
        }

        public static int Memcmp(byte[] d1, int o1, byte[] d2, int o2, int len)
        {
            for (int i = 0; i < len; i++)
            {
                byte b1 = d1[o1 + i];
                byte b2 = d2[o2 + i];
                if (b1 < b2) return -1;
                else if (b1 > b2) return 1;
            }
            return 0;
        }
    }
}