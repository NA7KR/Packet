using System;

namespace Renci.SshNet.Sftp.Responses
{
    internal abstract class SftpResponse : SftpMessage
    {
        public SftpResponse(uint protocolVersion)
        {
            ProtocolVersion = protocolVersion;
        }

        public uint ResponseId { get; private set; }
        public uint ProtocolVersion { get; private set; }

        protected override void LoadData()
        {
            base.LoadData();

            ResponseId = ReadUInt32();
        }

        protected override void SaveData()
        {
            throw new InvalidOperationException("Response cannot be saved.");
        }
    }
}