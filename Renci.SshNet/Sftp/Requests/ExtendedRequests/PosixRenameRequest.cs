using System;
using System.Text;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
    internal class PosixRenameRequest : SftpExtendedRequest
    {
        public PosixRenameRequest(uint protocolVersion, uint requestId, string oldPath, string newPath,
            Encoding encoding, Action<SftpStatusResponse> statusAction)
            : base(protocolVersion, requestId, statusAction)
        {
            OldPath = oldPath;
            NewPath = newPath;
            Encoding = encoding;
        }

        public override SftpMessageTypes SftpMessageType
        {
            get { return SftpMessageTypes.Extended; }
        }

        public override string Name
        {
            get { return "posix-rename@openssh.com"; }
        }

        public string OldPath { get; }
        public string NewPath { get; }
        public Encoding Encoding { get; }

        protected override void SaveData()
        {
            base.SaveData();
            Write(OldPath, Encoding);
            Write(NewPath, Encoding);
        }
    }
}