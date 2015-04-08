namespace Renci.SshNet.Sftp.Responses
{
    internal class SftpStatusResponse : SftpResponse
    {
        public SftpStatusResponse(uint protocolVersion)
            : base(protocolVersion)
        {
        }

        public override SftpMessageTypes SftpMessageType
        {
            get { return SftpMessageTypes.Status; }
        }

        public StatusCodes StatusCode { get; private set; }
        public string ErrorMessage { get; private set; }
        public string Language { get; private set; }

        protected override void LoadData()
        {
            base.LoadData();

            StatusCode = (StatusCodes) ReadUInt32();

            if (ProtocolVersion < 3)
            {
                return;
            }

            if (!IsEndOfData)
            {
                ErrorMessage = ReadString();
                Language = ReadString();
            }
        }
    }
}