using System;
using System.Text;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
    internal class SftpRmDirRequest : SftpRequest
    {
        public SftpRmDirRequest(uint protocolVersion, uint requestId, string path, Encoding encoding,
            Action<SftpStatusResponse> statusAction)
            : base(protocolVersion, requestId, statusAction)
        {
            Path = path;
            Encoding = encoding;
        }

        public override SftpMessageTypes SftpMessageType
        {
            get { return SftpMessageTypes.RmDir; }
        }

        public string Path { get; private set; }
        public Encoding Encoding { get; }

        protected override void LoadData()
        {
            base.LoadData();
            Path = ReadString(Encoding);
        }

        protected override void SaveData()
        {
            base.SaveData();
            Write(Path, Encoding);
        }
    }
}