using System;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
    internal class SftpReadDirRequest : SftpRequest
    {
        public SftpReadDirRequest(uint protocolVersion, uint requestId, byte[] handle,
            Action<SftpNameResponse> nameAction, Action<SftpStatusResponse> statusAction)
            : base(protocolVersion, requestId, statusAction)
        {
            Handle = handle;
            SetAction(nameAction);
        }

        public override SftpMessageTypes SftpMessageType
        {
            get { return SftpMessageTypes.ReadDir; }
        }

        public byte[] Handle { get; private set; }

        protected override void LoadData()
        {
            base.LoadData();
            Handle = ReadBinaryString();
        }

        protected override void SaveData()
        {
            base.SaveData();
            WriteBinaryString(Handle);
        }
    }
}