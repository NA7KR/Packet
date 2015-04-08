using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Renci.SshNet.Common;
using Renci.SshNet.Messages.Connection;

namespace Renci.SshNet.Channels
{
    /// <summary>
    ///     Implements "direct-tcpip" SSH channel.
    /// </summary>
    internal partial class ChannelDirectTcpip : Channel
    {
        private EventWaitHandle _channelData = new AutoResetEvent(false);
        public EventWaitHandle _channelEof = new AutoResetEvent(false);
        private EventWaitHandle _channelOpen = new AutoResetEvent(false);
        private Socket _socket;

        /// <summary>
        ///     Gets the type of the channel.
        /// </summary>
        /// <value>
        ///     The type of the channel.
        /// </value>
        public override ChannelTypes ChannelType
        {
            get { return ChannelTypes.DirectTcpip; }
        }

        public void Open(string remoteHost, uint port, Socket socket)
        {
            _socket = socket;

            var ep = socket.RemoteEndPoint as IPEndPoint;


            if (!IsConnected)
            {
                throw new SshException("Session is not connected.");
            }

            //  Open channel
            SendMessage(new ChannelOpenMessage(LocalChannelNumber, LocalWindowSize, PacketSize,
                new DirectTcpipChannelInfo(remoteHost, port, ep.Address.ToString(), (uint) ep.Port)));

            //  Wait for channel to open
            WaitHandle(_channelOpen);
        }

        /// <summary>
        ///     Binds channel to remote host.
        /// </summary>
        public void Bind()
        {
            //  Cannot bind if channel is not open
            if (!IsOpen)
                return;

            //  Start reading data from the port and send to channel
            Exception exception = null;

            try
            {
                var buffer = new byte[PacketSize - 9];

                while (_socket != null && _socket.CanRead())
                {
                    try
                    {
                        var read = 0;
                        InternalSocketReceive(buffer, ref read);
                        if (read > 0)
                        {
                            SendMessage(new ChannelDataMessage(RemoteChannelNumber, buffer.Take(read).ToArray()));
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch (SocketException exp)
                    {
                        if (exp.SocketErrorCode == SocketError.WouldBlock ||
                            exp.SocketErrorCode == SocketError.IOPending ||
                            exp.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
                        {
                            // socket buffer is probably empty, wait and try again
                            Thread.Sleep(30);
                        }
                        else if (exp.SocketErrorCode == SocketError.ConnectionAborted ||
                                 exp.SocketErrorCode == SocketError.ConnectionReset)
                        {
                            break;
                        }
                        else
                            throw; // throw any other error
                    }
                }
            }
            catch (Exception exp)
            {
                exception = exp;
            }

            //  Channel was open and we MUST receive EOF notification, 
            //  data transfer can take longer then connection specified timeout
            //  If listener thread is finished then socket was closed
            System.Threading.WaitHandle.WaitAny(new WaitHandle[] {_channelEof});

            //  Close socket if still open
            if (_socket != null)
            {
                _socket.Dispose();
                _socket = null;
            }

            if (exception != null)
                throw exception;
        }

        public override void Close()
        {
            //  Close socket if still open
            if (_socket != null)
            {
                _socket.Dispose();
                _socket = null;
            }

            //  Send EOF message first when channel need to be closed
            SendMessage(new ChannelEofMessage(RemoteChannelNumber));

            base.Close();
        }

        /// <summary>
        ///     Called when channel data is received.
        /// </summary>
        /// <param name="data">The data.</param>
        protected override void OnData(byte[] data)
        {
            base.OnData(data);

            InternalSocketSend(data);
        }

        /// <summary>
        ///     Called when channel is opened by the server.
        /// </summary>
        /// <param name="remoteChannelNumber">The remote channel number.</param>
        /// <param name="initialWindowSize">Initial size of the window.</param>
        /// <param name="maximumPacketSize">Maximum size of the packet.</param>
        protected override void OnOpenConfirmation(uint remoteChannelNumber, uint initialWindowSize,
            uint maximumPacketSize)
        {
            base.OnOpenConfirmation(remoteChannelNumber, initialWindowSize, maximumPacketSize);

            _channelOpen.Set();
        }

        protected override void OnOpenFailure(uint reasonCode, string description, string language)
        {
            base.OnOpenFailure(reasonCode, description, language);

            _channelOpen.Set();
        }

        /// <summary>
        ///     Called when channel has no more data to receive.
        /// </summary>
        protected override void OnEof()
        {
            base.OnEof();

            _channelEof.Set();
        }

        protected override void OnClose()
        {
            base.OnClose();

            _channelEof.Set();
        }

        protected override void OnErrorOccured(Exception exp)
        {
            base.OnErrorOccured(exp);

            //  If error occured, no more data can be received
            _channelEof.Set();
        }

        protected override void OnDisconnected()
        {
            base.OnDisconnected();

            //  If disconnected, no more data can be received
            _channelEof.Set();
        }

        partial void ExecuteThread(Action action);
        partial void InternalSocketReceive(byte[] buffer, ref int read);
        partial void InternalSocketSend(byte[] data);

        protected override void Dispose(bool disposing)
        {
            if (_socket != null)
            {
                _socket.Dispose();
                _socket = null;
            }

            if (_channelEof != null)
            {
                _channelEof.Dispose();
                _channelEof = null;
            }

            if (_channelOpen != null)
            {
                _channelOpen.Dispose();
                _channelOpen = null;
            }

            if (_channelData != null)
            {
                _channelData.Dispose();
                _channelData = null;
            }

            base.Dispose(disposing);
        }
    }
}