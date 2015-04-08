using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Renci.SshNet.Channels;
using Renci.SshNet.Common;

namespace Renci.SshNet
{
    /// <summary>
    ///     Provides functionality for local port forwarding
    /// </summary>
    public partial class ForwardedPortLocal
    {
        private readonly object _listenerLocker = new object();
        private TcpListener _listener;

        partial void InternalStart()
        {
            //  If port already started don't start it again
            if (IsStarted)
                return;

            var addr = BoundHost.GetIPAddress();
            var ep = new IPEndPoint(addr, (int) BoundPort);

            _listener = new TcpListener(ep);
            _listener.Start();

            Session.ErrorOccured += Session_ErrorOccured;
            Session.Disconnected += Session_Disconnected;

            _listenerTaskCompleted = new ManualResetEvent(false);
            ExecuteThread(() =>
            {
                try
                {
                    while (true)
                    {
                        lock (_listenerLocker)
                        {
                            if (_listener == null)
                                break;
                        }

                        var socket = _listener.AcceptSocket();

                        ExecuteThread(() =>
                        {
                            try
                            {
                                var originatorEndPoint = socket.RemoteEndPoint as IPEndPoint;

                                RaiseRequestReceived(originatorEndPoint.Address.ToString(),
                                    (uint) originatorEndPoint.Port);

                                var channel = Session.CreateChannel<ChannelDirectTcpip>();

                                channel.Open(Host, Port, socket);

                                channel.Bind();

                                channel.Close();
                            }
                            catch (Exception exp)
                            {
                                RaiseExceptionEvent(exp);
                            }
                        });
                    }
                }
                catch (SocketException exp)
                {
                    if (!(exp.SocketErrorCode == SocketError.Interrupted))
                    {
                        RaiseExceptionEvent(exp);
                    }
                }
                catch (Exception exp)
                {
                    RaiseExceptionEvent(exp);
                }
                finally
                {
                    _listenerTaskCompleted.Set();
                }
            });

            IsStarted = true;
        }

        partial void InternalStop()
        {
            //  If port not started you cant stop it
            if (!IsStarted)
                return;

            lock (_listenerLocker)
            {
                _listener.Stop();
                _listener = null;
            }
            _listenerTaskCompleted.WaitOne(Session.ConnectionInfo.Timeout);
            _listenerTaskCompleted.Dispose();
            _listenerTaskCompleted = null;

            IsStarted = false;
        }

        private void Session_ErrorOccured(object sender, ExceptionEventArgs e)
        {
            _listener.Stop();
        }

        private void Session_Disconnected(object sender, EventArgs e)
        {
            _listener.Stop();
        }
    }
}