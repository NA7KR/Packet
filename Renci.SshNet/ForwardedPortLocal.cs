using System;
using System.Threading;

namespace Renci.SshNet
{
    /// <summary>
    ///     Provides functionality for local port forwarding
    /// </summary>
    public partial class ForwardedPortLocal : ForwardedPort, IDisposable
    {
        private EventWaitHandle _listenerTaskCompleted;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ForwardedPortLocal" /> class.
        /// </summary>
        /// <param name="boundPort">The bound port.</param>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        /// <example>
        ///     <code source="..\..\Renci.SshNet.Tests\Classes\ForwardedPortLocalTest.cs"
        ///         region="Example SshClient AddForwardedPort Start Stop ForwardedPortLocal" language="C#"
        ///         title="Local port forwarding" />
        /// </example>
        public ForwardedPortLocal(uint boundPort, string host, uint port)
            : this(string.Empty, boundPort, host, port)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ForwardedPortLocal" /> class.
        /// </summary>
        /// <param name="boundHost">The bound host.</param>
        /// <param name="boundPort">The bound port.</param>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        public ForwardedPortLocal(string boundHost, uint boundPort, string host, uint port)
        {
            if (boundHost == null)
                throw new ArgumentNullException("boundHost");

            if (host == null)
                throw new ArgumentNullException("host");

            if (!boundHost.IsValidHost())
                throw new ArgumentException("boundHost");

            if (!boundPort.IsValidPort())
                throw new ArgumentOutOfRangeException("boundPort");

            if (!host.IsValidHost())
                throw new ArgumentException("host");

            if (!port.IsValidPort())
                throw new ArgumentOutOfRangeException("port");

            BoundHost = boundHost;
            BoundPort = boundPort;
            Host = host;
            Port = port;
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
        ///     Gets the forwarded host.
        /// </summary>
        public string Host { get; protected set; }

        /// <summary>
        ///     Gets the forwarded port.
        /// </summary>
        public uint Port { get; protected set; }

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
        ~ForwardedPortLocal()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        #endregion
    }
}