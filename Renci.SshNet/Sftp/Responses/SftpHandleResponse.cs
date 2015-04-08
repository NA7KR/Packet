namespace Renci.SshNet.Sftp.Responses
{
    internal class SftpHandleResponse : SftpResponse
    {
        public SftpHandleResponse(uint protocolVersion)
            : base(protocolVersion)
        {
        }

        public override SftpMessageTypes SftpMessageType
        {
            get { return SftpMessageTypes.Handle; }
        }

        public byte[] Handle { get; private set; }

        protected override void LoadData()
        {
            base.LoadData();

            Handle = ReadBinaryString();
        }
    }
}