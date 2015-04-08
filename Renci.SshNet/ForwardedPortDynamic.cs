using System;
using System.Threading;

namespace Renci.SshNet
{
    /// <summary>
    ///     Provides functionality for dynamic port forwarding
    /// </summary>
    public partial class ForwardedPortDynamic : ForwardedPort, IDisposable
    {
        private EventWaitHandle _listenerTaskCompleted;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ForwardedPortDynamic" /> class.
        /// </summary>
        /// <param name="port">The port.</param>
        public ForwardedPortDynamic(uint port)
            : this(string.Empty, port)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ForwardedPortDynamic" /> class.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        public ForwardedPortDynamic(string host, uint port)
        {
            BoundHost = host;
            BoundPort = port;
        }

        /// <summary>
        ///     Gets the bound host.
        /// </summary>
        public string BoundHost { get; protected set; }

        /// <summary>
        ///     Gets the bound port.
        /// </summary>
        public uint BoundPort { get; protected set; }

        /// <summary>
        ///     Starts local port forwarding.
        /// </summary>
        public override void Start()
        {
            InternalStart();
        }

        /// <summary>
        ///     Stops local port forwarding.
        /// </summary>
        public override void Stop()
        {
            base.Stop();

            InternalStop();
        }

        partial void InternalStart();
        partial void InternalStop();
        partial void ExecuteThread(Action action);

        #region IDisposable Members

        private bool _isDisposed;

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged ResourceMessages.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged ResourceMessages.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!_isDisposed)
            {
                InternalStop();

                // If disposing equals true, dispose all managed
                // and unmanaged ResourceMessages.
                if (disposing)
                {
                    // Dispose managed ResourceMessages.
                    if (_listenerTaskCompleted != null)
                    {
                        _listenerTaskCompleted.Dispose();
                        _listenerTaskCompleted = null;
                    }
                }

                // Note disposing has been done.
                _isDisposed = true;
            }
        }

        /// <summary>
        ///     Releases unmanaged resources and performs other cleanup operations before the
        ///     <see cref="ForwardedPortLocal" /> is reclaimed by garbage collection.
        /// </summary>
        ~ForwardedPortDynamic()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        #endregion
    }
}