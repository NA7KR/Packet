using System.Linq;
using System.Security.Cryptography;

namespace Renci.SshNet.Security.Cryptography
{
    /// <summary>
    ///     Provides HMAC algorithm implementation.
    /// </summary>
    /// <typeparam name="T">Class that implements <see cref="T:System.Security.Cryptography.HashAlgorithm" />.</typeparam>
    public class HMac<T> : KeyedHashAlgorithm where T : HashAlgorithm, new()
    {
        private HashAlgorithm _hash;
        //private bool _isHashing;
        private byte[] _innerPadding;
        private byte[] _outerPadding;

        private HMac()
        {
            // Create the hash algorithms.
            _hash = new T();
            HashSizeValue = _hash.HashSize;
        }

        /// <summary>
        ///     Rfc 2104.
        /// </summary>
        /// <param name="key">The key.</param>
        public HMac(byte[] key, int hashSizeValue)
            : this(key)
        {
            HashSizeValue = hashSizeValue;
        }

        public HMac(byte[] key)
            : this()
        {
            KeyValue = key;

            InternalInitialize();
        }

        /// <summary>
        ///     Gets the size of the block.
        /// </summary>
        /// <value>
        ///     The size of the block.
        /// </value>
        protected int BlockSize
        {
            get { return _hash.InputBlockSize; }
        }

        /// <summary>
        ///     Gets or sets the key to use in the hash algorithm.
        /// </summary>
        /// <returns>The key to use in the hash algorithm.</returns>
        public override byte[] Key
        {
            get { return (byte[]) KeyValue.Clone(); }
            set { SetKey(value); }
        }

        /// <summary>
        ///     Initializes an implementation of the <see cref="T:System.Security.Cryptography.HashAlgorithm" /> class.
        /// </summary>
        public override void Initialize()
        {
            InternalInitialize();
        }

        /// <summary>
        ///     Hashes the core.
        /// </summary>
        /// <param name="rgb">The RGB.</param>
        /// <param name="ib">The ib.</param>
        /// <param name="cb">The cb.</param>
        protected override void HashCore(byte[] rgb, int ib, int cb)
        {
            _hash.TransformBlock(rgb, ib, cb, rgb, ib);
        }

        /// <summary>
        ///     Finalizes the hash computation after the last data is processed by the cryptographic stream object.
        /// </summary>
        /// <returns>
        ///     The computed hash code.
        /// </returns>
        protected override byte[] HashFinal()
        {
            // Finalize the original hash.
            _hash.TransformFinalBlock(new byte[0], 0, 0);

            var hashValue = _hash.Hash;

            // Write the outer array.
            _hash.TransformBlock(_outerPadding, 0, BlockSize, _outerPadding, 0);

            // Write the inner hash and finalize the hash.            
            _hash.TransformFinalBlock(hashValue, 0, hashValue.Length);

            return _hash.Hash.Take(HashSize/8).ToArray();
        }

        private void InternalInitialize()
        {
            SetKey(KeyValue);
        }

        private void SetKey(byte[] value)
        {
            _hash.Initialize();

            if (value.Length > BlockSize)
            {
                KeyValue = _hash.ComputeHash(value);
                // No need to call Initialize, ComputeHash does it automatically.
            }
            else
            {
                KeyValue = value.Clone() as byte[];
            }

            _innerPadding = new byte[BlockSize];
            _outerPadding = new byte[BlockSize];

            // Compute inner and outer padding.
            var i = 0;
            for (i = 0; i < KeyValue.Length; i++)
            {
                _innerPadding[i] = (byte) (0x36 ^ KeyValue[i]);
                _outerPadding[i] = (byte) (0x5C ^ KeyValue[i]);
            }
            for (i = KeyValue.Length; i < BlockSize; i++)
            {
                _innerPadding[i] = 0x36;
                _outerPadding[i] = 0x5C;
            }

            _hash.TransformBlock(_innerPadding, 0, BlockSize, _innerPadding, 0);
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged ResourceMessages.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (_hash != null)
            {
                _hash.Clear();
                _hash = null;
            }
        }
    }
}