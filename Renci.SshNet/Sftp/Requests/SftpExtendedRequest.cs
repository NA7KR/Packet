using System;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
    internal abstract class SftpExtendedRequest : SftpRequest
    {
        public const string NAME = "posix-rename@openssh.com";

        public SftpExtendedRequest(uint protocolVersion, uint requestId, Action<SftpStatusResponse> statusAction)
            : base(protocolVersion, requestId, statusAction)
        {
        }

        public override SftpMessageTypes SftpMessageType
        {
            get { return SftpMessageTypes.Extended; }
        }

        public abstract string Name { get; }

        protected override void SaveData()
        {
            base.SaveData();
            Write(Name);
        }
    }
}