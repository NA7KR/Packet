﻿using System;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
    internal class SftpUnblockRequest : SftpRequest
    {
        public SftpUnblockRequest(uint protocolVersion, uint requestId, byte[] handle, ulong offset, ulong length,
            uint lockMask, Action<SftpStatusResponse> statusAction)
            : base(protocolVersion, requestId, statusAction)
        {
            Handle = handle;
            Offset = offset;
            Length = length;
        }

        public override SftpMessageTypes SftpMessageType
        {
            get { return SftpMessageTypes.Block; }
        }

        public byte[] Handle { get; private set; }
        public ulong Offset { get; private set; }
        public ulong Length { get; private set; }

        protected override void LoadData()
        {
            base.LoadData();
            Handle = ReadBinaryString();
            Offset = ReadUInt64();
            Length = ReadUInt64();
        }

        protected override void SaveData()
        {
            base.SaveData();
            WriteBinaryString(Handle);
            Write(Offset);
            Write(Length);
        }
    }
}