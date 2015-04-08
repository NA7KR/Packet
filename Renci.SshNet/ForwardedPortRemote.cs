using System;
using System.Globalization;
using System.Net;
using System.Threading;
using Renci.SshNet.Channels;
using Renci.SshNet.Common;
using Renci.SshNet.Messages.Connection;

namespace Renci.SshNet
{
    /// <summary>
    ///     Provides functionality for remote port forwarding
    /// </summary>
    public partial class ForwardedPortRemote : ForwardedPort, IDisposable
    {
        private EventWaitHandle _globalRequestResponse = new AutoResetEvent(false);
        private bool _requestStatus;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ForwardedPortRemote" /> class.
        /// </summary>
        /// <param name="boundHostAddress">The bound host address.</param>
        /// <param name="boundPort">The bound port.</param>
        /// <param name="hostAddress">The host address.</param>
        /// <param name="port">The port.</param>
        /// <exception cref="System.ArgumentNullException">boundHost</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">boundPort</exception>
        public ForwardedPortRemote(IPAddress boundHostAddress, uint boundPort, IPAddress hostAddress, uint port)
        {
            if (boundHostAddress == null)
                throw new ArgumentNullException("boundHost");

            if (hostAddress == null)
                throw new ArgumentNullException("host");

            if (!boundPort.IsValidPort())
                throw new ArgumentOutOfRangeException("boundPort");

            if (!port.IsValidPort())
                throw new ArgumentOutOfRangeException("port");

            BoundHostAddress = boundHostAddress;
            BoundPort = boundPort;
            HostAddress = hostAddress;
            Port = port;
        }

        /// <summary>
        ///     Gets the bound host.
        /// </summary>
        public IPAddress BoundHostAddress { get; protected set; }

        /// <summary>
        ///     Gets the bound host.
        /// </summary>
        public string BoundHost
        {
            get { return BoundHostAddress.ToString(); }
        }

        /// <summary>
        ///     Gets the bound port.
        /// </summary>
        public uint BoundPort { get; protected set; }

        /// <summary>
        ///     Gets the forwarded host.
        /// </summary>
        public IPAddress HostAddress { get; protected set; }

        /// <summary>
        ///     Gets the forwarded host.
        /// </summary>
        public string Host
        {
            get { return HostAddress.ToString(); }
        }

        /// <summary>
        ///     Gets the forwarded port.
        /// </summary>
        public uint Port { get; protected set; }

        /// <summary>
        ///     Starts remote port forwarding.
        /// </summary>
        public override void Start()
        {
            base.Start();

            //  If port already started don't start it again
            if (IsStarted)
                return;

            Session.RegisterMessage("SSH_MSG_REQUEST_FAILURE");
            Session.RegisterMessage("SSH_MSG_REQUEST_SUCCESS");
            Session.RegisterMessage("SSH_MSG_CHANNEL_OPEN");

            Session.RequestSuccessReceived += Session_RequestSuccess;
            Session.RequestFailureReceived += Session_RequestFailure;
            Session.ChannelOpenReceived += Session_ChannelOpening;

            //  Send global request to start direct tcpip
            Session.SendMessage(new GlobalRequestMessage(GlobalRequestName.TcpIpForward, true, BoundHost, BoundPort));

            Session.WaitHandle(_globalRequestResponse);

            if (!_requestStatus)
            {
                //  If request  failed don't handle channel opening for this request
                Session.ChannelOpenReceived -= Session_ChannelOpening;

                throw new SshException(string.Format(CultureInfo.CurrentCulture,
                    "Port forwarding for '{0}' port '{1}' failed to start.", Host, Port));
            }
            IsStarted = true;
        }

        /// <summary>
        ///     Stops remote port forwarding.
        /// </summary>
        public override void Stop()
        {
            base.Stop();

            //  If port not started you cant stop it
            if (!IsStarted)
                return;

            //  Send global request to cancel direct tcpip
            Session.SendMessage(new GlobalRequestMessage(GlobalRequestName.CancelTcpIpForward, true, BoundHost,
                BoundPort));

            Session.WaitHandle(_globalRequestResponse);

            Session.RequestSuccessReceived -= Session_RequestSuccess;
            Session.RequestFailureReceived -= Session_RequestFailure;
            Session.ChannelOpenReceived -= Session_ChannelOpening;

            IsStarted = false;
        }

        private void Session_ChannelOpening(object sender, MessageEventArgs<ChannelOpenMessage> e)
        {
            //  Ensure that this is corresponding request
            var info = e.Message.Info as ForwardedTcpipChannelInfo;
            if (info != null)
            {
                if (info.ConnectedAddress == BoundHost && info.ConnectedPort == BoundPort)
                {
                    ExecuteThread(() =>
                    {
                        try
                        {
                            RaiseRequestReceived(info.OriginatorAddress, info.OriginatorPort);

                            var channel = Session.CreateChannel<ChannelForwardedTcpip>(e.Message.LocalChannelNumber,
                                e.Message.InitialWindowSize, e.Message.MaximumPacketSize);
                            channel.Bind(HostAddress, Port);
                        }
                        catch (Exception exp)
                        {
                            RaiseExceptionEvent(exp);
                        }
                    });
                }
            }
        }

        private void Session_RequestFailure(object sender, EventArgs e)
        {
            _requestStatus = false;
            _globalRequestResponse.Set();
        }

        private void Session_RequestSuccess(object sender, MessageEventArgs<RequestSuccessMessage> e)
        {
            _requestStatus = true;
            if (BoundPort == 0)
            {
                BoundPort = (e.Message.BoundPort == null) ? 0 : e.Message.BoundPort.Value;
            }

            _globalRequestResponse.Set();
        }

        partial void ExecuteThread(Action action);

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
                    // Dispose managed resources.
                    if (_globalRequestResponse != null)
                    {
                        _globalRequestResponse.Dispose();
                        _globalRequestResponse = null;
                    }
                }

                // Note disposing has been done.
                _isDisposed = true;
            }
        }

        /// <summary>
        ///     Releases unmanaged resources and performs other cleanup operations before the
        ///     <see cref="ForwardedPortRemote" /> is reclaimed by garbage collection.
        /// </summary>
        ~ForwardedPortRemote()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        #endregion
    }
}