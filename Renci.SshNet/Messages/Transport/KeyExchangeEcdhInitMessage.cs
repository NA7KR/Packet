using System.Collections.Generic;
using System.Linq;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Transport
{
    /// <summary>
    ///     Represents SSH_MSG_KEXECDH_INIT message.
    /// </summary>
    [Message("SSH_MSG_KEXECDH_INIT", 30)]
    internal class KeyExchangeEcdhInitMessage : Message, IKeyExchangedAllowed
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="KeyExchangeEcdhInitMessage" /> class.
        /// </summary>
        /// <param name="clientExchangeValue">The client exchange value.</param>
        public KeyExchangeEcdhInitMessage(BigInteger d, BigInteger Q)
        {
            var a = d.ToByteArray().Reverse();
            var b = Q.ToByteArray().Reverse();
            var data = new List<byte>();
            data.Add(0x04);
            data.AddRange(d.ToByteArray().Reverse());
            data.AddRange(Q.ToByteArray().Reverse());
            QC = data.ToArray();
        }

        /// <summary>
        ///     Gets the client's ephemeral contribution to the ECDH exchange, encoded as an octet string
        /// </summary>
        public byte[] QC { get; private set; }

        /// <summary>
        ///     Called when type specific data need to be loaded.
        /// </summary>
        protected override void LoadData()
        {
            ResetReader();
            QC = ReadBinaryString();
        }

        /// <summary>
        ///     Called when type specific data need to be saved.
        /// </summary>
        protected override void SaveData()
        {
            WriteBinaryString(QC);
        }
    }
}