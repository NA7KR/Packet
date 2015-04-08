using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Sftp.Responses
{
    internal abstract class ExtendedReplyInfo : SshData
    {
        protected override void LoadData()
        {
            //  Read Message Type
            var messageType = ReadByte();

            //  Read Response ID
            var responseId = ReadUInt32();
        }

        protected override void SaveData()
        {
            throw new NotImplementedException();
        }
    }
}