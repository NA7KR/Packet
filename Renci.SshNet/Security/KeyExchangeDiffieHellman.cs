﻿using System;
using System.Linq;
using System.Text;
using Renci.SshNet.Common;
using Renci.SshNet.Messages.Transport;

namespace Renci.SshNet.Security
{
    /// <summary>
    ///     Represents base class for Diffie Hellman key exchange algorithm
    /// </summary>
    public abstract class KeyExchangeDiffieHellman : KeyExchange
    {
        /// <summary>
        ///     Specifies client exchange number.
        /// </summary>
        protected BigInteger _clientExchangeValue;

        /// <summary>
        ///     Specifies client payload
        /// </summary>
        protected byte[] _clientPayload;

        /// <summary>
        ///     Specifies key exchange group number.
        /// </summary>
        protected BigInteger _group;

        /// <summary>
        ///     Specifies host key data.
        /// </summary>
        protected byte[] _hostKey;

        /// <summary>
        ///     Specifies key exchange prime number.
        /// </summary>
        protected BigInteger _prime;

        /// <summary>
        ///     Specifies random generated number.
        /// </summary>
        protected BigInteger _randomValue;

        /// <summary>
        ///     Specifies server exchange number.
        /// </summary>
        protected BigInteger _serverExchangeValue;

        /// <summary>
        ///     Specifies server payload
        /// </summary>
        protected byte[] _serverPayload;

        /// <summary>
        ///     Specifies signature data.
        /// </summary>
        protected byte[] _signature;

        /// <summary>
        ///     Validates the exchange hash.
        /// </summary>
        /// <returns>
        ///     true if exchange hash is valid; otherwise false.
        /// </returns>
        protected override bool ValidateExchangeHash()
        {
            var exchangeHash = CalculateHash();

            var length = (uint) (_hostKey[0] << 24 | _hostKey[1] << 16 | _hostKey[2] << 8 | _hostKey[3]);

            var algorithmName = Encoding.UTF8.GetString(_hostKey, 4, (int) length);

            var key = Session.ConnectionInfo.HostKeyAlgorithms[algorithmName](_hostKey);

            Session.ConnectionInfo.CurrentHostKeyAlgorithm = algorithmName;

            if (CanTrustHostKey(key))
            {
                return key.VerifySignature(exchangeHash, _signature);
            }
            return false;
        }

        /// <summary>
        ///     Starts key exchange algorithm
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="message">Key exchange init message.</param>
        public override void Start(Session session, KeyExchangeInitMessage message)
        {
            base.Start(session, message);

            _serverPayload = message.GetBytes().ToArray();
            _clientPayload = Session.ClientInitMessage.GetBytes().ToArray();
        }

        /// <summary>
        ///     Populates the client exchange value.
        /// </summary>
        protected void PopulateClientExchangeValue()
        {
            if (_group.IsZero)
                throw new ArgumentNullException("_group");

            if (_prime.IsZero)
                throw new ArgumentNullException("_prime");

            var bitLength = _prime.BitLength;

            do
            {
                _randomValue = BigInteger.Random(bitLength);

                _clientExchangeValue = BigInteger.ModPow(_group, _randomValue, _prime);
            } while (_clientExchangeValue < 1 || _clientExchangeValue > ((_prime - 1)));
        }

        /// <summary>
        ///     Handles the server DH reply message.
        /// </summary>
        /// <param name="hostKey">The host key.</param>
        /// <param name="serverExchangeValue">The server exchange value.</param>
        /// <param name="signature">The signature.</param>
        protected virtual void HandleServerDhReply(byte[] hostKey, BigInteger serverExchangeValue, byte[] signature)
        {
            _serverExchangeValue = serverExchangeValue;
            _hostKey = hostKey;
            SharedKey = BigInteger.ModPow(serverExchangeValue, _randomValue, _prime);
            _signature = signature;
        }
    }
}