using System;
using System.Globalization;
using System.Text;
using System.Threading;
using Renci.SshNet.Channels;
using Renci.SshNet.Common;

namespace Renci.SshNet.Sftp
{
    /// <summary>
    ///     Base class for SSH subsystem implementations
    /// </summary>
    public abstract class SubsystemSession : IDisposable
    {
        private readonly Session _session;
        private readonly string _subsystemName;
        private ChannelSession _channel;
        private EventWaitHandle _channelClosedWaitHandle = new ManualResetEvent(false);
        private EventWaitHandle _errorOccuredWaitHandle = new ManualResetEvent(false);
        private Exception _exception;

        /// <summary>
        ///     Specifies a timeout to wait for operation to complete
        /// </summary>
        protected TimeSpan _operationTimeout;

        /// <summary>
        ///     Initializes a new instance of the SubsystemSession class.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="subsystemName">Name of the subsystem.</param>
        /// <param name="operationTimeout">The operation timeout.</param>
        /// <exception cref="System.ArgumentNullException">session</exception>
        /// <exception cref="ArgumentNullException"><paramref name="session" /> or <paramref name="subsystemName" /> is null.</exception>
        public SubsystemSession(Session session, string subsystemName, TimeSpan operationTimeout, Encoding encoding)
        {
            if (session == null)
                throw new ArgumentNullException("session");

            if (subsystemName == null)
                throw new ArgumentNullException("subsystemName");

            _session = session;
            _subsystemName = subsystemName;
            _operationTimeout = operationTimeout;
            Encoding = encoding;
        }

        /// <summary>
        ///     Gets the channel number.
        /// </summary>
        protected uint ChannelNumber { get; private set; }

        protected Encoding Encoding { get; private set; }

        /// <summary>
        ///     Occurs when an error occurred.
        /// </summary>
        public event EventHandler<ExceptionEventArgs> ErrorOccurred;

        /// <summary>
        ///     Occurs when session has been disconnected form the server.
        /// </summary>
        public event EventHandler<EventArgs> Disconnected;

        /// <summary>
        ///     Connects subsystem on SSH channel.
        /// </summary>
        public void Connect()
        {
            _channel = _session.CreateChannel<ChannelSession>();

            _session.ErrorOccured += Session_ErrorOccured;
            _session.Disconnected += Session_Disconnected;
            _channel.DataReceived += Channel_DataReceived;
            _channel.Closed += Channel_Closed;

            _channel.Open();

            ChannelNumber = _channel.RemoteChannelNumber;

            _channel.SendSubsystemRequest(_subsystemName);

            OnChannelOpen();
        }

        /// <summary>
        ///     Disconnects subsystem channel.
        /// </summary>
        public void Disconnect()
        {
            _channel.SendEof();

            _channel.Close();
        }

        /// <summary>
        ///     Sends data to the subsystem.
        /// </summary>
        /// <param name="data">The data to be sent.</param>
        public void SendData(byte[] data)
        {
            _channel.SendData(data);
        }

        /// <summary>
        ///     Called when channel is open.
        /// </summary>
        protected abstract void OnChannelOpen();

        /// <summary>
        ///     Called when data is received.
        /// </summary>
        /// <param name="dataTypeCode">The data type code.</param>
        /// <param name="data">The data.</param>
        protected abstract void OnDataReceived(uint dataTypeCode, byte[] data);

        /// <summary>
        ///     Raises the error.
        /// </summary>
        /// <param name="error">The error.</param>
        protected void RaiseError(Exception error)
        {
            _exception = error;

            _errorOccuredWaitHandle.Set();

            if (ErrorOccurred != null)
            {
                ErrorOccurred(this, new ExceptionEventArgs(error));
            }
        }

        private void Channel_DataReceived(object sender, ChannelDataEventArgs e)
        {
            OnDataReceived(e.DataTypeCode, e.Data);
        }

        private void Channel_Closed(object sender, ChannelEventArgs e)
        {
            _channelClosedWaitHandle.Set();
        }

        internal void WaitHandle(WaitHandle waitHandle, TimeSpan operationTimeout)
        {
            var waitHandles = new[]
            {
                _errorOccuredWaitHandle,
                _channelClosedWaitHandle,
                waitHandle
            };

            switch (System.Threading.WaitHandle.WaitAny(waitHandles, operationTimeout))
            {
                case 0:
                    throw _exception;
                case 1:
                    throw new SshException("Channel was closed.");
                case System.Threading.WaitHandle.WaitTimeout:
                    throw new SshOperationTimeoutException(string.Format(CultureInfo.CurrentCulture,
                        "Operation has timed out."));
                default:
                    break;
            }
        }

        private void Session_Disconnected(object sender, EventArgs e)
        {
            if (Disconnected != null)
            {
                Disconnected(this, new EventArgs());
            }

            RaiseError(new SshException("Connection was lost"));
        }

        private void Session_ErrorOccured(object sender, ExceptionEventArgs e)
        {
            RaiseError(e.Exception);
        }

        #region IDisposable Members

        private bool _isDisposed;

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
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
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!_isDisposed)
            {
                if (_channel != null)
                {
                    _channel.DataReceived -= Channel_DataReceived;

                    _channel.Dispose();
                    _channel = null;
                }

                _session.ErrorOccured -= Session_ErrorOccured;
                _session.Disconnected -= Session_Disconnected;

                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    if (_errorOccuredWaitHandle != null)
                    {
                        _errorOccuredWaitHandle.Dispose();
                        _errorOccuredWaitHandle = null;
                    }

                    if (_channelClosedWaitHandle != null)
                    {
                        _channelClosedWaitHandle.Dispose();
                        _channelClosedWaitHandle = null;
                    }
                }

                // Note disposing has been done.
                _isDisposed = true;
            }
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="SubsystemSession" /> class.
        /// </summary>
        ~SubsystemSession()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        #endregion
    }
}