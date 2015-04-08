using System;
using Renci.SshNet.Common;
using Renci.SshNet.Messages;
using Renci.SshNet.Messages.Transport;

namespace Renci.SshNet.Security
{
    /// <summary>
    ///     Represents "diffie-hellman-group-exchange-sha1" algorithm implementation.
    /// </summary>
    internal class KeyExchangeDiffieHellmanGroupExchangeSha1 : KeyExchangeDiffieHellman
    {
        /// <summary>
        ///     Gets algorithm name.
        /// </summary>
        public override string Name
        {
            get { return "diffie-hellman-group-exchange-sha1"; }
        }

        /// <summary>
        ///     Calculates key exchange hash value.
        /// </summary>
        /// <returns>
        ///     Key exchange hash.
        /// </returns>
        protected override byte[] CalculateHash()
        {
            var hashData = new _ExchangeHashData
            {
                ClientVersion = Session.ClientVersion,
                ServerVersion = Session.ServerVersion,
                ClientPayload = _clientPayload,
                ServerPayload = _serverPayload,
                HostKey = _hostKey,
                MinimumGroupSize = 1024,
                PreferredGroupSize = 1024,
                MaximumGroupSize = 1024,
                Prime = _prime,
                SubGroup = _group,
                ClientExchangeValue = _clientExchangeValue,
                ServerExchangeValue = _serverExchangeValue,
                SharedKey = SharedKey
            }.GetBytes();

            return Hash(hashData);
        }

        /// <summary>
        ///     Starts key exchange algorithm
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="message">Key exchange init message.</param>
        public override void Start(Session session, KeyExchangeInitMessage message)
        {
            base.Start(session, message);

            Session.RegisterMessage("SSH_MSG_KEX_DH_GEX_GROUP");
            Session.RegisterMessage("SSH_MSG_KEX_DH_GEX_REPLY");

            Session.MessageReceived += Session_MessageReceived;

            //  1. send SSH_MSG_KEY_DH_GEX_REQUEST
            SendMessage(new KeyExchangeDhGroupExchangeRequest(1024, 1024, 1024));
        }

        /// <summary>
        ///     Finishes key exchange algorithm.
        /// </summary>
        public override void Finish()
        {
            base.Finish();

            Session.MessageReceived -= Session_MessageReceived;
        }

        private void Session_MessageReceived(object sender, MessageEventArgs<Message> e)
        {
            var groupMessage = e.Message as KeyExchangeDhGroupExchangeGroup;

            if (groupMessage != null)
            {
                //  Unregister message once received
                Session.UnRegisterMessage("SSH_MSG_KEX_DH_GEX_GROUP");

                //  2. Receive SSH_MSG_KEX_DH_GEX_GROUP
                _prime = groupMessage.SafePrime;
                _group = groupMessage.SubGroup;

                PopulateClientExchangeValue();

                //  3. Send SSH_MSG_KEX_DH_GEX_INIT
                SendMessage(new KeyExchangeDhGroupExchangeInit(_clientExchangeValue));
            }
            var replyMessage = e.Message as KeyExchangeDhGroupExchangeReply;

            if (replyMessage != null)
            {
                //  Unregister message once received
                Session.UnRegisterMessage("SSH_MSG_KEX_DH_GEX_REPLY");

                HandleServerDhReply(replyMessage.HostKey, replyMessage.F, replyMessage.Signature);

                //  When SSH_MSG_KEX_DH_GEX_REPLY received key exchange is completed
                Finish();
            }
        }

        private class _ExchangeHashData : SshData
        {
            public string ServerVersion { get; set; }
            public string ClientVersion { get; set; }
            public byte[] ClientPayload { get; set; }
            public byte[] ServerPayload { get; set; }
            public byte[] HostKey { get; set; }
            public uint MinimumGroupSize { get; set; }
            public uint PreferredGroupSize { get; set; }
            public uint MaximumGroupSize { get; set; }
            public BigInteger Prime { get; set; }
            public BigInteger SubGroup { get; set; }
            public BigInteger ClientExchangeValue { get; set; }
            public BigInteger ServerExchangeValue { get; set; }
            public BigInteger SharedKey { get; set; }

            protected override void LoadData()
            {
                throw new NotImplementedException();
            }

            protected override void SaveData()
            {
                Write(ClientVersion);
                Write(ServerVersion);
                WriteBinaryString(ClientPayload);
                WriteBinaryString(ServerPayload);
                WriteBinaryString(HostKey);
                Write(MinimumGroupSize);
                Write(PreferredGroupSize);
                Write(MaximumGroupSize);
                Write(Prime);
                Write(SubGroup);
                Write(ClientExchangeValue);
                Write(ServerExchangeValue);
                Write(SharedKey);
            }
        }
    }
}