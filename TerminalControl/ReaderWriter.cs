using System.Text;
using System.IO;
using PacketComs;

namespace PacketComs.SSHC
{
    ////////////////////////////////////////////////////////////


    internal abstract class SSHDataWriter : IKeyWriter
    {
        protected MemoryStream _strm;

        public SSHDataWriter()
        {
            _strm = new MemoryStream(512);
        }

        public byte[] ToByteArray()
        {
            return _strm.ToArray();
        }

        public long Length
        {
            get { return _strm.Length; }
        }

        public void Write(byte[] data)
        {
            _strm.Write(data, 0, data.Length);
        }

        public void Write(byte[] data, int offset, int count)
        {
            _strm.Write(data, offset, count);
        }

        public void Write(byte data)
        {
            _strm.WriteByte(data);
        }

        public void Write(bool data)
        {
            _strm.WriteByte(data ? (byte) 1 : (byte) 0);
        }

        public void Write(int data)
        {
            uint udata = (uint) data;
            uint a = udata & 0xFF000000;
            a >>= 24;
            _strm.WriteByte((byte) a);

            a = udata & 0x00FF0000;
            a >>= 16;
            _strm.WriteByte((byte) a);

            a = udata & 0x0000FF00;
            a >>= 8;
            _strm.WriteByte((byte) a);

            a = udata & 0x000000FF;
            _strm.WriteByte((byte) a);
        }

        public abstract void Write(BigInteger data);

        public void Write(string data)
        {
            Write(data.Length);
            if (data.Length > 0) Write(Encoding.ASCII.GetBytes(data));
        }

        public void WriteAsString(byte[] data)
        {
            Write(data.Length);
            if (data.Length > 0) Write(data);
        }

        public void WriteAsString(byte[] data, int offset, int length)
        {
            Write(length);
            if (length > 0) Write(data, offset, length);
        }
    }
}

namespace PacketComs.SSHCV1
{
    internal class SSH1DataReader : SshDataReader
    {
        public SSH1DataReader(byte[] image) : base(image)
        {
        }

        public override BigInteger ReadMpInt()
        {
            //first 2 bytes describes the bit count
            int bits = (((int) Data[_offset]) << 8) + Data[_offset + 1];
            _offset += 2;

            return new BigInteger(Read((bits + 7)/8));
        }
    }

    internal class SSH1DataWriter : PacketComs.SSHC.SSHDataWriter
    {
        public override void Write(BigInteger data)
        {
            byte[] image = data.getBytes();
            int off = (image[0] == 0 ? 1 : 0);
            int len = (image.Length - off)*8;

            int a = len & 0x0000FF00;
            a >>= 8;
            _strm.WriteByte((byte) a);

            a = len & 0x000000FF;
            _strm.WriteByte((byte) a);

            _strm.Write(image, off, image.Length - off);
        }
    }

}

namespace PacketComs.SSHCV2
{
    internal class SSH2DataReader : SshDataReader
    {
        public SSH2DataReader(byte[] image) : base(image)
        {
        }

        //SSH2 Key File Only
        public BigInteger ReadBigIntWithBits()
        {
            int bits = ReadInt32();
            int bytes = (bits + 7)/8;
            return new BigInteger(Read(bytes));
        }

        public override BigInteger ReadMpInt()
        {
            return new BigInteger(ReadString());
        }

        public PacketType ReadPacketType()
        {
            return (PacketType) ReadByte();
        }
    }

    internal class SSH2DataWriter : PacketComs.SSHC.SSHDataWriter
    {
        //writes mpint in SSH2 format
        public override void Write(BigInteger data)
        {
            byte[] t = data.getBytes();
            int len = t.Length;
            if (t[0] >= 0x80)
            {
                Write(++len);
                Write((byte) 0);
            }
            else
                Write(len);
            Write(t);
        }

        public void WriteBigIntWithBits(BigInteger bi)
        {
            Write(bi.bitCount());
            Write(bi.getBytes());
        }

        public void WritePacketType(PacketType pt)
        {
            Write((byte) pt);
        }
    }
}