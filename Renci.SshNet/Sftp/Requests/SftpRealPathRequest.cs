using System;
using System.Text;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
    internal class SftpRealPathRequest : SftpRequest
    {
        public SftpRealPathRequest(uint protocolVersion, uint requestId, string path, Encoding encoding,
            Action<SftpNameResponse> nameAction, Action<SftpStatusResponse> statusAction)
            : base(protocolVersion, requestId, statusAction)
        {
            if (nameAction == null)
                throw new ArgumentNullException("name");

            if (statusAction == null)
                throw new ArgumentNullException("status");

            Path = path;
            Encoding = encoding;
            SetAction(nameAction);
        }

        public override SftpMessageTypes SftpMessageType
        {
            get { return SftpMessageTypes.RealPath; }
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