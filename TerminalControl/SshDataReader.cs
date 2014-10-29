using System;
using System.IO;

namespace PacketComs
{
    /// read/write primitive types
    /// 
    internal abstract class SshDataReader
    {
        protected byte[] Data;
        protected int _offset;

        public SshDataReader(byte[] image)
        {
            Data = image;
            _offset = 0;
        }

        public byte[] Image
        {
            get { return Data; }
        }

        public int Offset
        {
            get { return _offset; }
        }

        public int ReadInt32()
        {
            if (_offset + 3 >= Data.Length) throw new IOException(Strings.GetString("UnexpectedEOF"));

            int ret = (Data[_offset] << 24) + (Data[_offset + 1] << 16) +
                      (Data[_offset + 2] << 8) + Data[_offset + 3];

            _offset += 4;
            return ret;
        }

        public byte ReadByte()
        {
            if (_offset >= Data.Length) throw new IOException(Strings.GetString("UnexpectedEOF"));
            return Data[_offset++];
        }

        public bool ReadBool()
        {
            if (_offset >= Data.Length) throw new IOException(Strings.GetString("UnexpectedEOF"));
            return Data[_offset++] == 0 ? false : true;
        }

        /**
		* multi-precise integer
		*/
        public abstract BigInteger ReadMpInt();

        public byte[] ReadString()
        {
            int length = ReadInt32();
            return Read(length);
        }

        public byte[] Read(int length)
        {
            byte[] image = new byte[length];
            for (int i = 0; i < image.Length; i++)
            {
                if (_offset == Data.Length) throw new IOException(Strings.GetString("UnexpectedEOF"));
                image[i] = Data[_offset++];
            }
            return image;
        }

        public byte[] ReadAll()
        {
            byte[] t = new byte[Data.Length - _offset];
            Array.Copy(Data, _offset, t, 0, t.Length);
            return t;
        }

        public int Rest
        {
            get { return Data.Length - _offset; }
        }
    }
}