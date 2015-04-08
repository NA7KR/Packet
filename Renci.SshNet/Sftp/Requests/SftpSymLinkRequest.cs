using System;
using System.Text;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
    internal class SftpSymLinkRequest : SftpRequest
    {
        public SftpSymLinkRequest(uint protocolVersion, uint requestId, string newLinkPath, string existingPath,
            Encoding encoding, Action<SftpStatusResponse> statusAction)
            : base(protocolVersion, requestId, statusAction)
        {
            NewLinkPath = newLinkPath;
            ExistingPath = existingPath;
            Encoding = encoding;
        }

        public override SftpMessageTypes SftpMessageType
        {
            get { return SftpMessageTypes.SymLink; }
        }

        public string NewLinkPath { get; set; }
        public string ExistingPath { get; set; }
        public Encoding Encoding { get; set; }

        protected override void LoadData()
        {
            base.LoadData();
            NewLinkPath = ReadString(Encoding);
            ExistingPath = ReadString(Encoding);
        }

        protected override void SaveData()
        {
            base.SaveData();
            Write(NewLinkPath, Encoding);
            Write(ExistingPath, Encoding);
        }
    }
}