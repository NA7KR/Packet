namespace Renci.SshNet.Sftp.Responses
{
    internal class SftpDataResponse : SftpResponse
    {
        public SftpDataResponse(uint protocolVersion)
            : base(protocolVersion)
        {
        }

        public override SftpMessageTypes SftpMessageType
        {
            get { return SftpMessageTypes.Data; }
        }

        public byte[] Data { get; set; }
        public bool IsEof { get; set; }

        protected override void LoadData()
        {
            base.LoadData();

            Data = ReadBinaryString();

            if (!IsEndOfData)
            {
                IsEof = ReadBoolean();
            }
        }
    }
}