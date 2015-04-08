using System;
using System.Text;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
    internal class SftpRenameRequest : SftpRequest
    {
        public SftpRenameRequest(uint protocolVersion, uint requestId, string oldPath, string newPath, Encoding encoding,
            Action<SftpStatusResponse> statusAction)
            : base(protocolVersion, requestId, statusAction)
        {
            OldPath = oldPath;
            NewPath = newPath;
            Encoding = encoding;
        }

        public override SftpMessageTypes SftpMessageType
        {
            get { return SftpMessageTypes.Rename; }
        }

        public string OldPath { get; private set; }
        public string NewPath { get; private set; }
        public Encoding Encoding { get; }

        protected override void LoadData()
        {
            base.LoadData();
            OldPath = ReadString(Encoding);
            NewPath = ReadString(Encoding);
        }

        protected override void SaveData()
        {
            base.SaveData();
            Write(OldPath, Encoding);
            Write(NewPath, Encoding);
        }
    }
}