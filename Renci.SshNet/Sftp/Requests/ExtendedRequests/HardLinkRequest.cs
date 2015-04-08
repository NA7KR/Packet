using System;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
    internal class HardLinkRequest : SftpExtendedRequest
    {
        public HardLinkRequest(uint protocolVersion, uint requestId, string oldPath, string newPath,
            Action<SftpStatusResponse> statusAction)
            : base(protocolVersion, requestId, statusAction)
        {
            OldPath = oldPath;
            NewPath = newPath;
        }

        public override SftpMessageTypes SftpMessageType
        {
            get { return SftpMessageTypes.Extended; }
        }

        public override string Name
        {
            get { return "hardlink@openssh.com"; }
        }

        public string OldPath { get; }
        public string NewPath { get; }

        protected override void SaveData()
        {
            base.SaveData();
            Write(OldPath);
            Write(NewPath);
        }
    }
}