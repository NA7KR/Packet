using System;
using System.Linq;
using System.Security.Cryptography;

namespace Renci.SshNet
{
    /// <summary>
    ///     Holds information about key size and cipher to use
    /// </summary>
    public class HashInfo
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CipherInfo" /> class.
        /// </summary>
        /// <param name="keySize">Size of the key.</param>
        /// <param name="cipher">The cipher.</param>
        public HashInfo(int keySize, Func<byte[], HashAlgorithm> hash)
        {
            KeySize = keySize;
            HashAlgorithm = key => (hash(key.Take(KeySize/8).ToArray()));
        }

        /// <summary>
        ///     Gets the size of the key.
        /// </summary>
        /// <value>
        ///     The size of the key.
        /// </value>
        public int KeySize { get; }

        /// <summary>
        ///     Gets the cipher.
        /// </summary>
        public Func<byte[], HashAlgorithm> HashAlgorithm { get; private set; }
    }
}