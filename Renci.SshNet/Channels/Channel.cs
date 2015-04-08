using System;
using System.Globalization;
using System.Threading;
using Renci.SshNet.Common;
using Renci.SshNet.Messages;
using Renci.SshNet.Messages.Connection;

namespace Renci.SshNet.Channels
{
    /// <summary>
    ///     Represents base class for SSH channel implementations.
    /// </summary>
    internal abstract class Channel : IDisposable
    {
        private readonly object _serverWindowSizeLock = new object();
        private EventWaitHandle _channelClosedWaitHandle = new ManualResetEvent(false);
        private EventWaitHandle _channelServerWindowAdjustWaitHandle = new ManualResetEvent(false);
        private bool _closeMessageSent;
        private EventWaitHandle _disconnectedWaitHandle = new ManualResetEvent(false);
        private EventWaitHandle _errorOccuredWaitHandle = new ManualResetEvent(false);
        private uint _initialWindowSize = 0x100000;
        private uint _maximumPacketSize = 0x8000;
        private Session _session;

        /// <summary>
        ///     Gets the type of the channel.
        /// </summary>
        /// <value>
        ///     The type of the channel.
        /// </value>
        public abstract ChannelTypes ChannelType { get; }

        /// <summary>
        ///     Gets the local channel number.
        /// </summary>
        public uint LocalChannelNumber { get; private set; }

        /// <summary>
        ///     Gets the remote channel number assigned by the server.
        /// </summary>
        public uint RemoteChannelNumber { get; private set; }

        /// <summary>
        ///     Gets the size of the local window.
        /// </summary>
        /// <value>
        ///     The size of the local window.
        /// </value>
        public uint LocalWindowSize { get; private set; }

        /// <summary>
        ///     Gets or sets the size of the server window.
        /// </summary>
        /// <value>
        ///     The size of the server window.
        /// </value>
        public uint ServerWindowSize { get; protected set; }

        /// <summary>
        ///     Gets the size of the packet.
        /// </summary>
        /// <value>
        ///     The size of the packet.
        /// </value>
        public uint PacketSize { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether this channel is open.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this channel is open; otherwise, <c>false</c>.
        /// </value>
        public bool IsOpen { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether the session is connected.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the session is connected; otherwise, <c>false</c>.
        /// </value>
        protected bool IsConnected
        {
            get { return _session.IsConnected; }
        }

        /// <summary>
        ///     Gets the connection info.
        /// </summary>
        /// <value>The connection info.</value>
        protected ConnectionInfo ConnectionInfo
        {
            get { return _session.ConnectionInfo; }
        }

        /// <summary>
        ///     Gets the session semaphore to control number of session channels
        /// </summary>
        /// <value>The session semaphore.</value>
        protected SemaphoreLight SessionSemaphore
        {
            get { return _session.SessionSemaphore; }
        }

        /// <summary>
        ///     Initializes the channel.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="serverChannelNumber">The server channel number.</param>
        /// <param name="windowSize">Size of the window.</param>
        /// <param name="packetSize">Size of the packet.</param>
        internal virtual void Initialize(Session session, uint serverChannelNumber, uint windowSize, uint packetSize)
        {
            _initialWindowSize = windowSize;
            _maximumPacketSize = Math.Max(packetSize, 0x8000); //  Ensure minimum maximum packet size of 0x8000 bytes

            _session = session;
            LocalWindowSize = _initialWindowSize; // Initial window size
            PacketSize = _maximumPacketSize; // Maximum packet size

            LocalChannelNumber = session.NextChannelNumber;
            RemoteChannelNumber = serverChannelNumber;

            _session.ChannelOpenReceived += OnChannelOpen;
            _session.ChannelOpenConfirmationReceived += OnChannelOpenConfirmation;
            _session.ChannelOpenFailureReceived += OnChannelOpenFailure;
            _session.ChannelWindowAdjustReceived += OnChannelWindowAdjust;
            _session.ChannelDataReceived += OnChannelData;
            _session.ChannelExtendedDataReceived += OnChannelExtendedData;
            _session.ChannelEofReceived += OnChannelEof;
            _session.ChannelCloseReceived += OnChannelClose;
            _session.ChannelRequestReceived += OnChannelRequest;
            _session.ChannelSuccessReceived += OnChannelSuccess;
            _session.ChannelFailureReceived += OnChannelFailure;
            _session.ErrorOccured += Session_ErrorOccured;
            _session.Disconnected += Session_Disconnected;
        }

        /// <summary>
        ///     Sends the SSH_MSG_CHANNEL_EOF message.
        /// </summary>
        internal void SendEof()
        {
            //  Send EOF message first when channel need to be closed
            SendMessage(new ChannelEofMessage(RemoteChannelNumber));
        }

        internal void SendData(byte[] buffer)
        {
            SendMessage(new ChannelDataMessage(RemoteChannelNumber, buffer));
        }

        /// <summary>
        ///     Closes the channel.
        /// </summary>
        public virtual void Close()
        {
            Close(true);
        }

        /// <summary>
        ///     Sends SSH message to the server.
        /// </summary>
        /// <param name="message">The message.</param>
        protected void SendMessage(Message message)
        {
            //  Send channel messages only while channel is open
            if (!IsOpen)
                return;

            _session.SendMessage(message);
        }

        protected void SendMessage(ChannelOpenConfirmationMessage message)
        {
            //  No need to check whether channel is open when trying to open a channel
            _session.SendMessage(message);

            //  Chanel consider to be open when confirmation message is sent
            IsOpen = true;
        }

        /// <summary>
        ///     Send message to open a channel.
        /// </summary>
        /// <param name="message">Message to send</param>
        protected void SendMessage(ChannelOpenMessage message)
        {
            //  No need to check whether channel is open when trying to open a channel
            _session.SendMessage(message);
        }

        /// <summary>
        ///     Sends close channel message to the server
        /// </summary>
        /// <param name="message">Message to send.</param>
        protected void SendMessage(ChannelCloseMessage message)
        {
            //  Send channel messages only while channel is open
            if (!IsOpen)
                return;

            _session.SendMessage(message);

            //  When channel close message is sent channel considred to be closed
            IsOpen = false;
        }

        /// <summary>
        ///     Sends channel data message to the servers.
        /// </summary>
        /// <remarks>This method takes care of managing the window size.</remarks>
        /// <param name="message">Channel data message.</param>
        protected void SendMessage(ChannelDataMessage message)
        {
            //  Send channel messages only while channel is open
            if (!IsOpen)
                return;

            var messageLength = message.Data.Length;
            do
            {
                lock (_serverWindowSizeLock)
                {
                    var serverWindowSize = ServerWindowSize;
                    if (serverWindowSize < messageLength)
                    {
                        //  Wait for window to be big enough for this message
                        _channelServerWindowAdjustWaitHandle.Reset();
                    }
                    else
                    {
                        ServerWindowSize -= (uint) messageLength;
                        break;
                    }
                }

                //  Wait for window to change
                WaitHandle(_channelServerWindowAdjustWaitHandle);
            } while (true);

            _session.SendMessage(message);
        }

        /// <summary>
        ///     Sends channel extended data message to the servers.
        /// </summary>
        /// <remarks>This method takes care of managing the window size.</remarks>
        /// <param name="message">Channel data message.</param>
        protected void SendMessage(ChannelExtendedDataMessage message)
        {
            //  Send channel messages only while channel is open
            if (!IsOpen)
                return;

            var messageLength = message.Data.Length;
            do
            {
                lock (_serverWindowSizeLock)
                {
                    var serverWindowSize = ServerWindowSize;
                    if (serverWindowSize < messageLength)
                    {
                        //  Wait for window to be big enough for this message
                        _channelServerWindowAdjustWaitHandle.Reset();
                    }
                    else
                    {
                        ServerWindowSize -= (uint) messageLength;
                        break;
                    }
                }

                //  Wait for window to change
                WaitHandle(_channelServerWindowAdjustWaitHandle);
            } while (true);

            _session.SendMessage(message);
        }

        /// <summary>
        ///     Waits for the handle to be signaled or for an error to occurs.
        /// </summary>
        /// <param name="waitHandle">The wait handle.</param>
        protected void WaitHandle(WaitHandle waitHandle)
        {
            _session.WaitHandle(waitHandle);
        }

        protected virtual void Close(bool wait)
        {
            //  Send message to close the channel on the server
            //  Ignore sending close message when client not connected
            if (!_closeMessageSent && IsConnected)
            {
                lock (this)
                {
                    if (!_closeMessageSent)
                    {
                        SendMessage(new ChannelCloseMessage(RemoteChannelNumber));
                        _closeMessageSent = true;
                    }
                }
            }

            //  Wait for channel to be closed
            if (wait)
            {
                _session.WaitHandle(_channelClosedWaitHandle);
            }
        }

        protected virtual void OnDisconnected()
        {
        }

        protected virtual void OnErrorOccured(Exception exp)
        {
        }

        private void Session_Disconnected(object sender, EventArgs e)
        {
            OnDisconnected();

            //  If objected is disposed or being disposed don't handle this event
            if (_isDisposed)
                return;

            _disconnectedWaitHandle.Set();
        }

        private void Session_ErrorOccured(object sender, ExceptionEventArgs e)
        {
            OnErrorOccured(e.Exception);

            //  If objected is disposed or being disposed don't handle this event
            if (_isDisposed)
                return;

            _errorOccuredWaitHandle.Set();
        }

        private void AdjustDataWindow(byte[] messageData)
        {
            LocalWindowSize -= (uint) messageData.Length;

            //  Adjust window if window size is too low
            if (LocalWindowSize < PacketSize)
            {
                SendMessage(new ChannelWindowAdjustMessage(RemoteChannelNumber, _initialWindowSize - LocalWindowSize));
                LocalWindowSize = _initialWindowSize;
            }
        }

        #region Message events

        /// <summary>
        ///     Occurs when <see cref="ChannelOpenFailureMessage" /> message received
        /// </summary>
        public event EventHandler<ChannelOpenFailedEventArgs> OpenFailed;

        /// <summary>
        ///     Occurs when <see cref="ChannelDataMessage" /> message received
        /// </summary>
        public event EventHandler<ChannelDataEventArgs> DataReceived;

        /// <summary>
        ///     Occurs when <see cref="ChannelExtendedDataMessage" /> message received
        /// </summary>
        public event EventHandler<ChannelDataEventArgs> ExtendedDataReceived;

        /// <summary>
        ///     Occurs when <see cref="ChannelEofMessage" /> message received
        /// </summary>
        public event EventHandler<ChannelEventArgs> EndOfData;

        /// <summary>
        ///     Occurs when <see cref="ChannelCloseMessage" /> message received
        /// </summary>
        public event EventHandler<ChannelEventArgs> Closed;

        /// <summary>
        ///     Occurs when <see cref="ChannelRequestMessage" /> message received
        /// </summary>
        public event EventHandler<ChannelRequestEventArgs> RequestReceived;

        /// <summary>
        ///     Occurs when <see cref="ChannelSuccessMessage" /> message received
        /// </summary>
        public event EventHandler<ChannelEventArgs> RequestSuccessed;

        /// <summary>
        ///     Occurs when <see cref="ChannelFailureMessage" /> message received
        /// </summary>
        public event EventHandler<ChannelEventArgs> RequestFailed;

        #endregion

        #region Channel virtual methods

        /// <summary>
        ///     Called when channel need to be open on the client.
        /// </summary>
        /// <param name="info">Channel open information.</param>
        protected virtual void OnOpen(ChannelOpenInfo info)
        {
        }

        /// <summary>
        ///     Called when channel is opened by the server.
        /// </summary>
        /// <param name="remoteChannelNumber">The remote channel number.</param>
        /// <param name="initialWindowSize">Initial size of the window.</param>
        /// <param name="maximumPacketSize">Maximum size of the packet.</param>
        protected virtual void OnOpenConfirmation(uint remoteChannelNumber, uint initialWindowSize,
            uint maximumPacketSize)
        {
            RemoteChannelNumber = remoteChannelNumber;
            ServerWindowSize = initialWindowSize;
            PacketSize = maximumPacketSize;

            //  Chanel consider to be open when confirmation message was received
            IsOpen = true;
        }

        /// <summary>
        ///     Called when channel failed to open.
        /// </summary>
        /// <param name="reasonCode">The reason code.</param>
        /// <param name="description">The description.</param>
        /// <param name="language">The language.</param>
        protected virtual void OnOpenFailure(uint reasonCode, string description, string language)
        {
            if (OpenFailed != null)
            {
                OpenFailed(this, new ChannelOpenFailedEventArgs(LocalChannelNumber, reasonCode, description, language));
            }
        }

        /// <summary>
        ///     Called when channel window need to be adjust.
        /// </summary>
        /// <param name="bytesToAdd">The bytes to add.</param>
        protected virtual void OnWindowAdjust(uint bytesToAdd)
        {
            lock (_serverWindowSizeLock)
            {
                ServerWindowSize += bytesToAdd;
            }
            _channelServerWindowAdjustWaitHandle.Set();
        }

        /// <summary>
        ///     Called when channel data is received.
        /// </summary>
        /// <param name="data">The data.</param>
        protected virtual void OnData(byte[] data)
        {
            AdjustDataWindow(data);

            if (DataReceived != null)
            {
                DataReceived(this, new ChannelDataEventArgs(LocalChannelNumber, data));
            }
        }

        /// <summary>
        ///     Called when channel extended data is received.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="dataTypeCode">The data type code.</param>
        protected virtual void OnExtendedData(byte[] data, uint dataTypeCode)
        {
            AdjustDataWindow(data);

            if (ExtendedDataReceived != null)
            {
                ExtendedDataReceived(this, new ChannelDataEventArgs(LocalChannelNumber, data, dataTypeCode));
            }
        }

        /// <summary>
        ///     Called when channel has no more data to receive.
        /// </summary>
        protected virtual void OnEof()
        {
            if (EndOfData != null)
            {
                EndOfData(this, new ChannelEventArgs(LocalChannelNumber));
            }
        }

        /// <summary>
        ///     Called when channel is closed by the server.
        /// </summary>
        protected virtual void OnClose()
        {
            Close(false);

            if (Closed != null)
            {
                Closed(this, new ChannelEventArgs(LocalChannelNumber));
            }
        }

        /// <summary>
        ///     Called when channel request received.
        /// </summary>
        /// <param name="info">Channel request information.</param>
        protected virtual void OnRequest(RequestInfo info)
        {
            if (RequestReceived != null)
            {
                RequestReceived(this, new ChannelRequestEventArgs(info));
            }
        }

        /// <summary>
        ///     Called when channel request was successful
        /// </summary>
        protected virtual void OnSuccess()
        {
            if (RequestSuccessed != null)
            {
                RequestSuccessed(this, new ChannelEventArgs(LocalChannelNumber));
            }
        }

        /// <summary>
        ///     Called when channel request failed.
        /// </summary>
        protected virtual void OnFailure()
        {
            if (RequestFailed != null)
            {
                RequestFailed(this, new ChannelEventArgs(LocalChannelNumber));
            }
        }

        #endregion

        #region Channel message event handlers

        private void OnChannelOpen(object sender, MessageEventArgs<ChannelOpenMessage> e)
        {
            if (e.Message.LocalChannelNumber == LocalChannelNumber)
            {
                OnOpen(e.Message.Info);
            }
        }

        private void OnChannelOpenConfirmation(object sender, MessageEventArgs<ChannelOpenConfirmationMessage> e)
        {
            if (e.Message.LocalChannelNumber == LocalChannelNumber)
            {
                OnOpenConfirmation(e.Message.RemoteChannelNumber, e.Message.InitialWindowSize,
                    e.Message.MaximumPacketSize);
            }
        }

        private void OnChannelOpenFailure(object sender, MessageEventArgs<ChannelOpenFailureMessage> e)
        {
            if (e.Message.LocalChannelNumber == LocalChannelNumber)
            {
                OnOpenFailure(e.Message.ReasonCode, e.Message.Description, e.Message.Language);
            }
        }

        private void OnChannelWindowAdjust(object sender, MessageEventArgs<ChannelWindowAdjustMessage> e)
        {
            if (e.Message.LocalChannelNumber == LocalChannelNumber)
            {
                OnWindowAdjust(e.Message.BytesToAdd);
            }
        }

        private void OnChannelData(object sender, MessageEventArgs<ChannelDataMessage> e)
        {
            if (e.Message.LocalChannelNumber == LocalChannelNumber)
            {
                OnData(e.Message.Data);
            }
        }

        private void OnChannelExtendedData(object sender, MessageEventArgs<ChannelExtendedDataMessage> e)
        {
            if (e.Message.LocalChannelNumber == LocalChannelNumber)
            {
                OnExtendedData(e.Message.Data, e.Message.DataTypeCode);
            }
        }

        private void OnChannelEof(object sender, MessageEventArgs<ChannelEofMessage> e)
        {
            if (e.Message.LocalChannelNumber == LocalChannelNumber)
            {
                OnEof();
            }
        }

        private void OnChannelClose(object sender, MessageEventArgs<ChannelCloseMessage> e)
        {
            if (e.Message.LocalChannelNumber == LocalChannelNumber)
            {
                OnClose();

                _channelClosedWaitHandle.Set();
            }
        }

        private void OnChannelRequest(object sender, MessageEventArgs<ChannelRequestMessage> e)
        {
            if (e.Message.LocalChannelNumber == LocalChannelNumber)
            {
                if (_session.ConnectionInfo.ChannelRequests.ContainsKey(e.Message.RequestName))
                {
                    //  Get request specific class
                    var requestInfo = _session.ConnectionInfo.ChannelRequests[e.Message.RequestName];

                    //  Load request specific data
                    requestInfo.Load(e.Message.RequestData);

                    //  Raise request specific event
                    OnRequest(requestInfo);
                }
                else
                {
                    throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture,
                        "Request '{0}' is not supported.", e.Message.RequestName));
                }
            }
        }

        private void OnChannelSuccess(object sender, MessageEventArgs<ChannelSuccessMessage> e)
        {
            if (e.Message.LocalChannelNumber == LocalChannelNumber)
            {
                OnSuccess();
            }
        }

        private void OnChannelFailure(object sender, MessageEventArgs<ChannelFailureMessage> e)
        {
            if (e.Message.LocalChannelNumber == LocalChannelNumber)
            {
                OnFailure();
            }
        }

        #endregion

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
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    Close(false);

                    // Dispose managed resources.
                    if (_channelClosedWaitHandle != null)
                    {
                        _channelClosedWaitHandle.Dispose();
                        _channelClosedWaitHandle = null;
                    }
                    if (_channelServerWindowAdjustWaitHandle != null)
                    {
                        _channelServerWindowAdjustWaitHandle.Dispose();
                        _channelServerWindowAdjustWaitHandle = null;
                    }
                    if (_errorOccuredWaitHandle != null)
                    {
                        _errorOccuredWaitHandle.Dispose();
                        _errorOccuredWaitHandle = null;
                    }
                    if (_disconnectedWaitHandle != null)
                    {
                        _disconnectedWaitHandle.Dispose();
                        _disconnectedWaitHandle = null;
                    }
                }

                //  Ensure that all events are detached from current instance
                _session.ChannelOpenReceived -= OnChannelOpen;
                _session.ChannelOpenConfirmationReceived -= OnChannelOpenConfirmation;
                _session.ChannelOpenFailureReceived -= OnChannelOpenFailure;
                _session.ChannelWindowAdjustReceived -= OnChannelWindowAdjust;
                _session.ChannelDataReceived -= OnChannelData;
                _session.ChannelExtendedDataReceived -= OnChannelExtendedData;
                _session.ChannelEofReceived -= OnChannelEof;
                _session.ChannelCloseReceived -= OnChannelClose;
                _session.ChannelRequestReceived -= OnChannelRequest;
                _session.ChannelSuccessReceived -= OnChannelSuccess;
                _session.ChannelFailureReceived -= OnChannelFailure;
                _session.ErrorOccured -= Session_ErrorOccured;
                _session.Disconnected -= Session_Disconnected;


                // Note disposing has been done.
                _isDisposed = true;
            }
        }

        /// <summary>
        ///     Releases unmanaged resources and performs other cleanup operations before the
        ///     <see cref="Channel" /> is reclaimed by garbage collection.
        /// </summary>
        ~Channel()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        #endregion
    }
}