using System;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
    internal class SftpWriteRequest : SftpRequest
    {
        public SftpWriteRequest(uint protocolVersion, uint requestId, byte[] handle, ulong offset, byte[] data,
            Action<SftpStatusResponse> statusAction)
            : base(protocolVersion, requestId, statusAction)
        {
            Handle = handle;
            Offset = offset;
            Data = data;
        }

        public override SftpMessageTypes SftpMessageType
        {
            get { return SftpMessageTypes.Write; }
        }

        public byte[] Handle { get; private set; }
        public ulong Offset { get; private set; }
        public byte[] Data { get; private set; }

        protected override void LoadData()
        {
            base.LoadData();
            Handle = ReadBinaryString();
            Offset = ReadUInt64();
            Data = ReadBinaryString();
        }

        protected override void SaveData()
        {
            base.SaveData();
            WriteBinaryString(Handle);
            Write(Offset);
            WriteBinaryString(Data);
        }
    }
}