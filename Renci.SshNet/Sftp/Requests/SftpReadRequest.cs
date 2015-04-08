using System;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
    internal class SftpReadRequest : SftpRequest
    {
        public SftpReadRequest(uint protocolVersion, uint requestId, byte[] handle, ulong offset, uint length,
            Action<SftpDataResponse> dataAction, Action<SftpStatusResponse> statusAction)
            : base(protocolVersion, requestId, statusAction)
        {
            Handle = handle;
            Offset = offset;
            Length = length;
            SetAction(dataAction);
        }

        public override SftpMessageTypes SftpMessageType
        {
            get { return SftpMessageTypes.Read; }
        }

        public byte[] Handle { get; private set; }
        public ulong Offset { get; private set; }
        public uint Length { get; private set; }

        protected override void LoadData()
        {
            base.LoadData();
            Handle = ReadBinaryString();
            Offset = ReadUInt64();
            Length = ReadUInt32();
        }

        protected override void SaveData()
        {
            base.SaveData();
            WriteBinaryString(Handle);
            Write(Offset);
            Write(Length);
        }
    }
}