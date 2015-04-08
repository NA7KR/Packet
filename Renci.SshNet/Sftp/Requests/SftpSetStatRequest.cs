using System;
using System.Text;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
    internal class SftpSetStatRequest : SftpRequest
    {
        public SftpSetStatRequest(uint protocolVersion, uint requestId, string path, Encoding encoding,
            SftpFileAttributes attributes, Action<SftpStatusResponse> statusAction)
            : base(protocolVersion, requestId, statusAction)
        {
            Path = path;
            Encoding = encoding;
            Attributes = attributes;
        }

        public override SftpMessageTypes SftpMessageType
        {
            get { return SftpMessageTypes.SetStat; }
        }

        public string Path { get; private set; }
        public Encoding Encoding { get; }
        public SftpFileAttributes Attributes { get; private set; }

        protected override void LoadData()
        {
            base.LoadData();
            Path = ReadString(Encoding);
            Attributes = ReadAttributes();
        }

        protected override void SaveData()
        {
            base.SaveData();
            Write(Path, Encoding);
            Write(Attributes);
        }
    }
}