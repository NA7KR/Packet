using System;
using System.IO;
using System.Text;
using Routrek.SSHC;

namespace PacketComs
{
 
    public class SSHException : Exception
    {
        private byte[] _data;

        public SSHException(string msg, byte[] data) : base(msg)
        {
            _data = data;
        }

        public SSHException(string msg) : base(msg)
        {
        }
    }

    public enum SSHProtocol
    {
        SSH1,
        SSH2
    }

    public enum CipherAlgorithm
    {
        TripleDes = 3,
        Blowfish = 6,
        Aes128 = 10 //SSH2 ONLY
    }

    public enum AuthenticationType
    {
        PublicKey = 2, //uses identity file
        Password = 3,
        KeyboardInteractive = 4
    }

    public enum AuthenticationResult
    {
        Success,
        Failure,
        Prompt
    }


    public enum MacAlgorithm
    {
        HMACSHA1
    }


    internal class SSHUtil
    {
        public static string ClientVersionString(SSHProtocol p)
        {
            return p == SSHProtocol.SSH1 ? "SSH-1.5-Granados-1.0" : "SSH-2.0-Granados-1.0";
        }

        public static int ReadInt32(Stream input)
        {
            byte[] t = new byte[4];
            ReadAll(input, t, 0, t.Length);
            return ReadInt32(t, 0);
        }

        public static int ReadInt32(byte[] data, int offset)
        {
            int ret = 0;
            ret |= data[offset];
            ret <<= 8;
            ret |= data[offset + 1];
            ret <<= 8;
            ret |= data[offset + 2];
            ret <<= 8;
            ret |= data[offset + 3];
            return ret;
        }

        /**
		* Network-byte-orderで32ビット値を書き込む。
		*/

        public static void WriteIntToByteArray(byte[] dst, int pos, int data)
        {
            uint udata = (uint) data;
            uint a = udata & 0xFF000000;
            a >>= 24;
            dst[pos] = (byte) a;

            a = udata & 0x00FF0000;
            a >>= 16;
            dst[pos + 1] = (byte) a;

            a = udata & 0x0000FF00;
            a >>= 8;
            dst[pos + 2] = (byte) a;

            a = udata & 0x000000FF;
            dst[pos + 3] = (byte) a;
        }

        public static void WriteIntToStream(Stream input, int data)
        {
            byte[] t = new byte[4];
            WriteIntToByteArray(t, 0, data);
            input.Write(t, 0, t.Length);
        }

        public static void ReadAll(Stream input, byte[] buf, int offset, int len)
        {
            do
            {
                int fetched = input.Read(buf, offset, len);
                len -= fetched;
                offset += fetched;
            } while (len > 0);
        }

        public static bool ContainsString(string[] s, string v)
        {
            foreach (string x in s)
                if (x == v) return true;

            return false;
        }

        public static int Memcmp(byte[] d1, byte[] d2)
        {
            for (int i = 0; i < d1.Length; i++)
            {
                if (d1[i] != d2[i]) return d2[i] - d1[i];
            }
            return 0;
        }

        public static int Memcmp(byte[] d1, int o1, byte[] d2, int o2, int len)
        {
            for (int i = 0; i < len; i++)
            {
                if (d1[o1 + i] != d2[o2 + i]) return d2[o2 + i] - d1[o1 + i];
            }
            return 0;
        }
    }

    public class Strings
    {
        private static StringResources _strings;

        public static string GetString(string id)
        {
            if (_strings == null) Reload();
            return _strings.GetString(id);
        }

        //load resource corresponding to current culture
        public static void Reload()
        {
            _strings = new StringResources("PacketSoftware.strings", typeof (Strings).Assembly);
        }
    }

    internal class DebugUtil
    {
        public static string DumpByteArray(byte[] data)
        {
            return DumpByteArray(data, 0, data.Length);
        }

        public static string DumpByteArray(byte[] data, int offset, int length)
        {
            StringBuilder bld = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                bld.Append(data[offset + i].ToString("X2"));
                if ((i%4) == 3) bld.Append(' ');
            }
            return bld.ToString();
        }
    }
}