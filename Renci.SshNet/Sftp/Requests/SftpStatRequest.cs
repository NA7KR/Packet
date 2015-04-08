using System;
using System.Text;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
    internal class SftpStatRequest : SftpRequest
    {
        public SftpStatRequest(uint protocolVersion, uint requestId, string path, Encoding encoding,
            Action<SftpAttrsResponse> attrsAction, Action<SftpStatusResponse> statusAction)
            : base(protocolVersion, requestId, statusAction)
        {
            Path = path;
            Encoding = encoding;
            SetAction(attrsAction);
        }

        public override SftpMessageTypes SftpMessageType
        {
            get { return SftpMessageTypes.Stat; }
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