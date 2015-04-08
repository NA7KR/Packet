using System;
using System.Text;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
    internal class SftpRemoveRequest : SftpRequest
    {
        public SftpRemoveRequest(uint protocolVersion, uint requestId, string filename, Encoding encoding,
            Action<SftpStatusResponse> statusAction)
            : base(protocolVersion, requestId, statusAction)
        {
            Filename = filename;
            Encoding = encoding;
        }

        public override SftpMessageTypes SftpMessageType
        {
            get { return SftpMessageTypes.Remove; }
        }

        public string Filename { get; private set; }
        public Encoding Encoding { get; }

        protected override void LoadData()
        {
            base.LoadData();
            Filename = ReadString(Encoding);
        }

        protected override void SaveData()
        {
            base.SaveData();
            Write(Filename, Encoding);
        }
    }
}