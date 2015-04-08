using System;
using System.Text;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
    internal class StatVfsRequest : SftpExtendedRequest
    {
        public StatVfsRequest(uint protocolVersion, uint requestId, string path, Encoding encoding,
            Action<SftpExtendedReplyResponse> extendedAction, Action<SftpStatusResponse> statusAction)
            : base(protocolVersion, requestId, statusAction)
        {
            Path = path;
            Encoding = encoding;
            SetAction(extendedAction);
        }

        public override SftpMessageTypes SftpMessageType
        {
            get { return SftpMessageTypes.Extended; }
        }

        public override string Name
        {
            get { return "statvfs@openssh.com"; }
        }

        public string Path { get; }
        public Encoding Encoding { get; }

        protected override void SaveData()
        {
            base.SaveData();
            Write(Path, Encoding);
        }
    }
}