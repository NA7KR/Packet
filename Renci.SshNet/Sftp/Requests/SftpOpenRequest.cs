using System;
using System.Text;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
    internal class SftpOpenRequest : SftpRequest
    {
        public SftpOpenRequest(uint protocolVersion, uint requestId, string fileName, Encoding encoding, Flags flags,
            Action<SftpHandleResponse> handleAction, Action<SftpStatusResponse> statusAction)
            : this(
                protocolVersion, requestId, fileName, encoding, flags, new SftpFileAttributes(), handleAction,
                statusAction)
        {
        }

        public SftpOpenRequest(uint protocolVersion, uint requestId, string fileName, Encoding encoding, Flags flags,
            SftpFileAttributes attributes, Action<SftpHandleResponse> handleAction,
            Action<SftpStatusResponse> statusAction)
            : base(protocolVersion, requestId, statusAction)
        {
            Filename = fileName;
            Flags = flags;
            Attributes = attributes;
            Encoding = encoding;

            SetAction(handleAction);
        }

        public override SftpMessageTypes SftpMessageType
        {
            get { return SftpMessageTypes.Open; }
        }

        public string Filename { get; }
        public Flags Flags { get; }
        public SftpFileAttributes Attributes { get; }
        public Encoding Encoding { get; }

        protected override void LoadData()
        {
            base.LoadData();
            throw new NotSupportedException();
        }

        protected override void SaveData()
        {
            base.SaveData();

            Write(Filename, Encoding);
            Write((uint) Flags);
            Write(Attributes);
        }
    }
}