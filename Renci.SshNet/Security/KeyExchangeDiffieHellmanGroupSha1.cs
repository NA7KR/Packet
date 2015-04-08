using System;
using Renci.SshNet.Common;
using Renci.SshNet.Messages;
using Renci.SshNet.Messages.Transport;

namespace Renci.SshNet.Security
{
    /// <summary>
    ///     Represents "diffie-hellman-group1-sha1" algorithm implementation.
    /// </summary>
    public abstract class KeyExchangeDiffieHellmanGroupSha1 : KeyExchangeDiffieHellman
    {
        /// <summary>
        ///     Gets the group prime.
        /// </summary>
        /// <value>
        ///     The group prime.
        /// </value>
        public abstract BigInteger GroupPrime { get; }

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

            Session.RegisterMessage("SSH_MSG_KEXDH_REPLY");

            Session.MessageReceived += Session_MessageReceived;

            _prime = GroupPrime;

            _group = new BigInteger(new byte[] {2});

            PopulateClientExchangeValue();

            SendMessage(new KeyExchangeDhInitMessage(_clientExchangeValue));
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
            var message = e.Message as KeyExchangeDhReplyMessage;
            if (message != null)
            {
                //  Unregister message once received
                Session.UnRegisterMessage("SSH_MSG_KEXDH_REPLY");

                HandleServerDhReply(message.HostKey, message.F, message.Signature);

                //  When SSH_MSG_KEXDH_REPLY received key exchange is completed
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
                Write(ClientExchangeValue);
                Write(ServerExchangeValue);
                Write(SharedKey);
            }
        }
    }
}