using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace PacketComs
{
    internal enum ReceiverState
    {
        Ready,
        Closed,
        Error
    }

    internal interface IHandlerBase
    {
        void OnClosed();
        void OnError(Exception error, string msg);
    }

    internal interface IByteArrayHandler : IHandlerBase
    {
        void OnData(byte[] data, int offset, int length);
    }

    internal interface IStringHandler : IHandlerBase
    {
        void OnString(string data);
    }

    internal abstract class SynchronizedHandlerBase
    {
        public virtual void SetClosed()
        {
            _state = ReceiverState.Closed;
            Event.Set();
        }

        public virtual void SetError(string msg)
        {
            _errorMessage = msg;
            _state = ReceiverState.Error;
            Event.Set();
        }

        public virtual void SetReady()
        {
            _state = ReceiverState.Ready;
            Event.Set();
        }

        protected ManualResetEvent Event;
        protected ReceiverState _state;
        protected string _errorMessage;

        public WaitHandle WaitHandle
        {
            get { return Event; }
        }

        public ReceiverState State
        {
            get { return _state; }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
        }

        public void Wait()
        {
            Event.WaitOne();
            Event.Reset();
        }

        protected SynchronizedHandlerBase()
        {
            Event = new ManualResetEvent(false);
        }
    }

    internal class ProtocolNegotiationHandler : SynchronizedHandlerBase, IByteArrayHandler
    {
        protected string _serverVersion;
        protected SshConnectionParameter Param;
        protected string EndOfLine;

        public ProtocolNegotiationHandler(SshConnectionParameter param)
        {
            Param = param;
            _errorMessage = Strings.GetString("NotSSHServer");
        }

        public string ServerVersion
        {
            get { return _serverVersion; }
        }

        public string Eol
        {
            get { return EndOfLine; }
        }

        public void OnData(byte[] buf, int offset, int length)
        {
            try
            {
                //the specification claims the version string ends with CRLF, however some servers send LF only
                if (length <= 2 || buf[offset + length - 1] != 0x0A)
                    throw new SSHException(Strings.GetString("NotSSHServer"));
                //Debug.WriteLine(String.Format("receiveServerVersion len={0}",len));
                string sv = Encoding.ASCII.GetString(buf, offset, length);
                _serverVersion = sv.Trim();
                EndOfLine = sv.EndsWith("\r\n") ? "\r\n" : "\n"; //quick hack

                //check compatibility
                int a = _serverVersion.IndexOf('-');
                if (a == -1) throw new SSHException("Format of server version is invalid");
                int b = _serverVersion.IndexOf('-', a + 1);
                if (b == -1) throw new SSHException("Format of server version is invalid");
                int comma = _serverVersion.IndexOf('.', a, b - a);
                if (comma == -1) throw new SSHException("Format of server version is invalid");

                int major = Int32.Parse(_serverVersion.Substring(a + 1, comma - a - 1));
                int minor = Int32.Parse(_serverVersion.Substring(comma + 1, b - comma - 1));

                if (Param.Protocol == SSHProtocol.SSH1)
                {
                    if (major != 1) throw new SSHException("The protocol version of server is not compatible for SSH1");
                }
                else
                {
                    if (major >= 3 || major <= 0 || (major == 1 && minor != 99))
                        throw new SSHException("The protocol version of server is not compatible with SSH2");
                }

                SetReady();
            }
            catch (Exception ex)
            {
                OnError(ex, ex.Message);
            }
        }

        public void OnError(Exception error, string msg)
        {
            base.SetError(msg);
        }

        public void OnClosed()
        {
            base.SetClosed();
        }
    }

    //System.IO.Socket��IChannelEventReceiver�𒊏ۉ�����
    internal abstract class AbstractSocket
    {
        protected IByteArrayHandler Handler;

        protected AbstractSocket(IByteArrayHandler h)
        {
            Handler = h;
        }

        public void SetHandler(IByteArrayHandler h)
        {
            Handler = h;
        }

        internal abstract void Write(byte[] data, int offset, int length);
        internal abstract void WriteByte(byte data);
        internal abstract void Flush();
        internal abstract void Close();
        internal abstract bool DataAvailable { get; }
    }

    internal class PlainSocket : AbstractSocket
    {
        private Socket _socket;
        private byte[] _buf;
        private bool _closed;

        internal PlainSocket(Socket s, IByteArrayHandler h) : base(h)
        {
            _socket = s;
            _buf = new byte[0x1000];
            _closed = false;
        }

        internal override void Write(byte[] data, int offset, int length)
        {
            _socket.Send(data, offset, length, SocketFlags.None);
        }

        internal override void WriteByte(byte data)
        {
            byte[] t = new byte[1];
            t[0] = data;
            _socket.Send(t, 0, 1, SocketFlags.None);
        }

        internal override void Flush()
        {
        }

        internal override void Close()
        {
            _socket.Close();
            _closed = true;
        }

        internal void RepeatAsyncRead()
        {
            _socket.BeginReceive(_buf, 0, _buf.Length, SocketFlags.None, RepeatCallback, null);
        }

        internal override bool DataAvailable
        {
            get { return _socket.Available > 0; }
        }

        private void RepeatCallback(IAsyncResult result)
        {
            try
            {
                int n = _socket.EndReceive(result);
                //GUtil.WriteDebugLog(String.Format("t={0}, n={1} cr={2} cw={3}", DateTime.Now.ToString(), n, _socket.CanRead, _socket.CanWrite));
                //Debug.WriteLine(String.Format("r={0}, n={1} cr={2} cw={3}", result.IsCompleted, n, _socket.CanRead, _socket.CanWrite));
                if (n > 0)
                {
                    Handler.OnData(_buf, 0, n);
                    _socket.BeginReceive(_buf, 0, _buf.Length, SocketFlags.None, RepeatCallback, null);
                }
                else if (n < 0)
                {
                    //in case of Win9x, EndReceive() returns 0 every 288 seconds even if no data is available
                    _socket.BeginReceive(_buf, 0, _buf.Length, SocketFlags.None, RepeatCallback, null);
                }
                else
                    Handler.OnClosed();
            }
            catch (Exception ex)
            {
                if ((ex is SocketException) && ((SocketException) ex).ErrorCode == 995)
                {
                    //in case of .NET1.1 on Win9x, EndReceive() changes the behavior. it throws SocketException with an error code 995. 
                    _socket.BeginReceive(_buf, 0, _buf.Length, SocketFlags.None, RepeatCallback, null);
                }
                else if (!_closed)
                    Handler.OnError(ex, ex.Message);
                else
                    Handler.OnClosed();
            }
        }
    }

    internal class ChannelSocket : AbstractSocket, ISshChannelEventReceiver
    {
        private SshChannel _channel;
        private bool _ready;

        internal ChannelSocket(IByteArrayHandler h) : base(h)
        {
            _ready = false;
        }

        internal SshChannel SshChennal
        {
            get { return _channel; }
            set { _channel = value; }
        }

        internal override void Write(byte[] data, int offset, int length)
        {
            if (!_ready || _channel == null) throw new SSHException("channel not ready");
            _channel.Transmit(data, offset, length);
        }

        internal override void WriteByte(byte data)
        {
            if (!_ready || _channel == null) throw new SSHException("channel not ready");
            byte[] t = new byte[1];
            t[0] = data;
            _channel.Transmit(t);
        }

        internal override bool DataAvailable
        {
            get { return _channel.Connection.Available; }
        }


        internal override void Flush()
        {
        }

        internal override void Close()
        {
            if (!_ready || _channel == null) throw new SSHException("channel not ready");
            _channel.Close();
            if (_channel.Connection.ChannelCount <= 1) //close last channel
                _channel.Connection.Close();
        }

        public void OnData(byte[] data, int offset, int length)
        {
            Handler.OnData(data, offset, length);
        }

        public void OnChannelEof()
        {
            Handler.OnClosed();
        }

        public void OnChannelError(Exception error, string msg)
        {
            Handler.OnError(error, msg);
        }

        public void OnChannelClosed()
        {
            Handler.OnClosed();
        }

        public void OnChannelReady()
        {
            _ready = true;
        }

        public void OnExtendedData(int type, byte[] data)
        {
            //!!handle data
        }

        public void OnMiscPacket(byte packetType, byte[] data, int offset, int length)
        {
        }
    }
}