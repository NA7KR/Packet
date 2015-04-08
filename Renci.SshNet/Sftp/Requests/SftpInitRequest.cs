namespace Renci.SshNet.Sftp.Requests
{
    internal class SftpInitRequest : SftpMessage
    {
        public SftpInitRequest(uint version)
        {
            Version = version;
        }

        public override SftpMessageTypes SftpMessageType
        {
            get { return SftpMessageTypes.Init; }
        }

        public uint Version { get; private set; }

        protected override void LoadData()
        {
            base.LoadData();
            Version = ReadUInt32();
        }

        protected override void SaveData()
        {
            base.SaveData();
            Write(Version);
        }
    }
}