using System.Net;
using System.Net.Sockets;

namespace Renci.SshNet.Channels
{
    /// <summary>
    ///     Implements "forwarded-tcpip" SSH channel.
    /// </summary>
    internal partial class ChannelForwardedTcpip : Channel
    {
        partial void OpenSocket(IPAddress connectedHost, uint connectedPort)
        {
            var ep = new IPEndPoint(connectedHost, (int) connectedPort);
            _socket = new Socket(ep.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(ep);
            _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, 1);
        }

        partial void InternalSocketReceive(byte[] buffer, ref int read)
        {
            read = _socket.Receive(buffer);
        }

        partial void InternalSocketSend(byte[] data)
        {
            _socket.Send(data);
        }
    }
}