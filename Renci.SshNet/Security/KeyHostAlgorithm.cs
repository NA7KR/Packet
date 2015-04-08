using System.Collections.Generic;
using System.Linq;
using Renci.SshNet.Common;

namespace Renci.SshNet.Security
{
    /// <summary>
    ///     Implements key support for host algorithm.
    /// </summary>
    public class KeyHostAlgorithm : HostAlgorithm
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="KeyHostAlgorithm" /> class.
        /// </summary>
        /// <param name="name">Host key name.</param>
        /// <param name="key">Host key.</param>
        public KeyHostAlgorithm(string name, Key key)
            : base(name)
        {
            Key = key;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HostAlgorithm" /> class.
        /// </summary>
        /// <param name="name">Host key name.</param>
        /// <param name="key">Host key.</param>
        /// <param name="data">Host key encoded data.</param>
        public KeyHostAlgorithm(string name, Key key, byte[] data)
            : base(name)
        {
            Key = key;

            var sshKey = new SshKeyData();
            sshKey.Load(data);
            Key.Public = sshKey.Keys;
        }

        /// <summary>
        ///     Gets the key.
        /// </summary>
        public Key Key { get; }

        /// <summary>
        ///     Gets the public key data.
        /// </summary>
        public override byte[] Data
        {
            get { return new SshKeyData(Name, Key.Public).GetBytes(); }
        }

        /// <summary>
        ///     Signs the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>
        ///     Signed data.
        /// </returns>
        public override byte[] Sign(byte[] data)
        {
            return new SignatureKeyData(Name, Key.Sign(data)).GetBytes().ToArray();
        }

        /// <summary>
        ///     Verifies the signature.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="signature">The signature.</param>
        /// <returns>
        ///     <c>True</c> is signature was successfully verifies; otherwise <c>false</c>.
        /// </returns>
        public override bool VerifySignature(byte[] data, byte[] signature)
        {
            var signatureData = new SignatureKeyData();
            signatureData.Load(signature);

            return Key.VerifySignature(data, signatureData.Signature);
        }

        private class SshKeyData : SshData
        {
            public SshKeyData()
            {
            }

            public SshKeyData(string name, params BigInteger[] keys)
            {
                Name = name;
                Keys = keys;
            }

            public BigInteger[] Keys { get; private set; }
            public string Name { get; private set; }

            protected override void LoadData()
            {
                Name = ReadString();
                var keys = new List<BigInteger>();
                while (!IsEndOfData)
                {
                    keys.Add(ReadBigInt());
                }
                Keys = keys.ToArray();
            }

            protected override void SaveData()
            {
                Write(Name);
                foreach (var key in Keys)
                {
                    Write(key);
                }
            }
        }

        private class SignatureKeyData : SshData
        {
            public SignatureKeyData()
            {
            }

            public SignatureKeyData(string name, byte[] signature)
            {
                AlgorithmName = name;
                Signature = signature;
            }

            /// <summary>
            ///     Gets or sets the name of the algorithm.
            /// </summary>
            /// <value>
            ///     The name of the algorithm.
            /// </value>
            public string AlgorithmName { get; private set; }

            /// <summary>
            ///     Gets or sets the signature.
            /// </summary>
            /// <value>
            ///     The signature.
            /// </value>
            public byte[] Signature { get; private set; }

            /// <summary>
            ///     Called when type specific data need to be loaded.
            /// </summary>
            protected override void LoadData()
            {
                AlgorithmName = ReadString();
                Signature = ReadBinaryString();
            }

            /// <summary>
            ///     Called when type specific data need to be saved.
            /// </summary>
            protected override void SaveData()
            {
                Write(AlgorithmName);
                WriteBinaryString(Signature);
            }
        }
    }
}