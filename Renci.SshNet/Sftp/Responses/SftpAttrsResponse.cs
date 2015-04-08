namespace Renci.SshNet.Sftp.Responses
{
    internal class SftpAttrsResponse : SftpResponse
    {
        public SftpAttrsResponse(uint protocolVersion)
            : base(protocolVersion)
        {
        }

        public override SftpMessageTypes SftpMessageType
        {
            get { return SftpMessageTypes.Attrs; }
        }

        public SftpFileAttributes Attributes { get; private set; }

        protected override void LoadData()
        {
            base.LoadData();
            Attributes = ReadAttributes();
        }
    }
}