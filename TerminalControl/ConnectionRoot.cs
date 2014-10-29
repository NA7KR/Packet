using System;
using System.Collections;
using System.Net.Sockets;
using System.Text;
using Routrek.SSHC;
using Routrek.SSHCV1;

namespace PacketComs
{
    public abstract class SshConnection
    {
        internal AbstractSocket Stream;
        internal ISshConnectionEventReceiver _eventReceiver;

        protected byte[] SessionId;
        internal ICipher Cipher; //transmission
        //internal Cipher _rCipher; //reception
        protected SshConnectionParameter _param;

        protected object LockObject = new Object();

        protected bool Closed;

        protected bool _autoDisconnect;

        protected AuthenticationResult _authenticationResult;

        protected SshConnection(SshConnectionParameter param, ISshConnectionEventReceiver receiver)
        {
            _param = (SshConnectionParameter) param.Clone();
            _eventReceiver = receiver;
            ChannelEntries = new ArrayList(16);
            _autoDisconnect = true;
        }

        public abstract SshConnectionInfo ConnectionInfo { get; }

        /**
		* returns true if any data from server is available
		*/

        public bool Available
        {
            get
            {
                if (Closed)
                    return false;
                else
                    return Stream.DataAvailable;
            }
        }

        public SshConnectionParameter Param
        {
            get { return _param; }
        }

        public AuthenticationResult AuthenticationResult
        {
            get { return _authenticationResult; }
        }

        internal abstract IByteArrayHandler PacketBuilder { get; }

        public ISshConnectionEventReceiver EventReceiver
        {
            get { return _eventReceiver; }
        }

        public bool IsClosed
        {
            get { return Closed; }
        }

        public bool AutoDisconnect
        {
            get { return _autoDisconnect; }
            set { _autoDisconnect = value; }
        }


        internal abstract AuthenticationResult Connect(AbstractSocket target);

        /**
		* terminates this connection
		*/
        public abstract void Disconnect(string msg);

        /**
		* opens a pseudo terminal
		*/
        public abstract SshChannel OpenShell(ISshChannelEventReceiver receiver);

        /**
		 * forwards the remote end to another host
		 */

        public abstract SshChannel ForwardPort(ISshChannelEventReceiver receiver, string remoteHost, int remotePort,
            string originatorHost, int originatorPort);

        /**
		 * listens a connection on the remote end
		 */
        public abstract void ListenForwardedPort(string allowedHost, int bindPort);

        /**
		 * cancels binded port
		 */
        public abstract void CancelForwardedPort(string host, int port);

        /**
		* closes socket directly.
		*/
        public abstract void Close();


        public abstract void SendIgnorableData(string msg);


        /**
		 * opens another SSH connection via port-forwarded connection
		 */

        public SshConnection OpenPortForwardedAnotherConnection(SshConnectionParameter param,
            ISshConnectionEventReceiver receiver, string host, int port)
        {
            ProtocolNegotiationHandler pnh = new ProtocolNegotiationHandler(param);
            ChannelSocket s = new ChannelSocket(pnh);

            SshChannel ch = ForwardPort(s, host, port, "localhost", 0);
            s.SshChennal = ch;
            return Connect(param, receiver, pnh, s);
        }

        //channel id support
        protected class ChannelEntry
        {
            public int LocalId;
            public ISshChannelEventReceiver Receiver;
            public SshChannel Channel;
        }

        protected ArrayList ChannelEntries;
        protected int ChannelSequence;

        protected ChannelEntry FindChannelEntry(int id)
        {
            for (int i = 0; i < ChannelEntries.Count; i++)
            {
                ChannelEntry e = (ChannelEntry) ChannelEntries[i];
                if (e.LocalId == id) return e;
            }
            return null;
        }

        protected ChannelEntry RegisterChannelEventReceiver(SshChannel ch, ISshChannelEventReceiver r)
        {
            lock (this)
            {
                ChannelEntry e = new ChannelEntry();
                e.Channel = ch;
                e.Receiver = r;
                e.LocalId = ChannelSequence++;

                for (int i = 0; i < ChannelEntries.Count; i++)
                {
                    if (ChannelEntries[i] == null)
                    {
                        ChannelEntries[i] = e;
                        return e;
                    }
                }
                ChannelEntries.Add(e);
                return e;
            }
        }

        internal void RegisterChannel(int localId, SshChannel ch)
        {
            FindChannelEntry(localId).Channel = ch;
        }

        internal void UnregisterChannelEventReceiver(int id)
        {
            lock (this)
            {
                foreach (ChannelEntry e in ChannelEntries)
                {
                    if (e.LocalId == id)
                    {
                        ChannelEntries.Remove(e);
                        break;
                    }
                }
                if (ChannelCount == 0 && _autoDisconnect) Disconnect(""); //auto close
            }
        }

        public virtual int ChannelCount
        {
            get
            {
                int r = 0;
                for (int i = 0; i < ChannelEntries.Count; i++)
                {
                    if (ChannelEntries[i] != null) r++;
                }
                return r;
            }
        }


        //establishes a SSH connection in subject to ConnectionParameter
        public static SshConnection Connect(SshConnectionParameter param, ISshConnectionEventReceiver receiver,
            Socket underlyingSocket)
        {
            if (param.UserName == null) throw new InvalidOperationException("UserName property is not set");
            if (param.Password == null) throw new InvalidOperationException("Password property is not set");

            ProtocolNegotiationHandler pnh = new ProtocolNegotiationHandler(param);
            PlainSocket s = new PlainSocket(underlyingSocket, pnh);
            s.RepeatAsyncRead();
            return ConnectMain(param, receiver, pnh, s);
        }

        internal static SshConnection Connect(SshConnectionParameter param, ISshConnectionEventReceiver receiver,
            ProtocolNegotiationHandler pnh, AbstractSocket s)
        {
            if (param.UserName == null) throw new InvalidOperationException("UserName property is not set");
            if (param.Password == null) throw new InvalidOperationException("Password property is not set");

            return ConnectMain(param, receiver, pnh, s);
        }

        private static SshConnection ConnectMain(SshConnectionParameter param, ISshConnectionEventReceiver receiver,
            ProtocolNegotiationHandler pnh, AbstractSocket s)
        {
            pnh.Wait();

            if (pnh.State != ReceiverState.Ready) throw new SSHException(pnh.ErrorMessage);

            string sv = pnh.ServerVersion;

            SshConnection con;
            if (param.Protocol == SSHProtocol.SSH1)
                con = new Ssh1Connection(param, receiver, sv, SSHUtil.ClientVersionString(param.Protocol));
            else
                con = new SSH2Connection(param, receiver, sv, SSHUtil.ClientVersionString(param.Protocol));

            s.SetHandler(con.PacketBuilder);
            SendMyVersion(s, param);

            if (con.Connect(s) != AuthenticationResult.Failure)
            {
                return con;
            }
            else
            {
                s.Close();
                return null;
            }
        }

        private static void SendMyVersion(AbstractSocket stream, SshConnectionParameter param)
        {
            string cv = SSHUtil.ClientVersionString(param.Protocol);
            if (param.Protocol == SSHProtocol.SSH1)
                cv += param.Ssh1VersionEol;
            else
                cv += "\r\n";
            byte[] data = Encoding.ASCII.GetBytes(cv);
            stream.Write(data, 0, data.Length);
        }
    }

    public enum ChannelType
    {
        Session,
        Shell,
        ForwardedLocalToRemote,
        ForwardedRemoteToLocal
    }

    public abstract class SshChannel
    {
        protected ChannelType _type;
        protected int LocalId;
        protected int RemoteId;
        protected SshConnection _connection;

        protected SshChannel(SshConnection con, ChannelType type, int localId)
        {
            con.RegisterChannel(localId, this);
            _connection = con;
            _type = type;
            LocalId = localId;
        }

        public int LocalChannelId
        {
            get { return LocalId; }
        }

        public int RemoteChannelId
        {
            get { return RemoteId; }
        }

        public SshConnection Connection
        {
            get { return _connection; }
        }

        public ChannelType Type
        {
            get { return _type; }
        }

        /**
		 * resizes the size of terminal
		 */
        public abstract void ResizeTerminal(int width, int height, int pixelWidth, int pixelHeight);

        /**
		* transmits channel data 
		*/
        public abstract void Transmit(byte[] data);

        /**
		* transmits channel data 
		*/
        public abstract void Transmit(byte[] data, int offset, int length);

        /**
		 * sends EOF(SSH2 only)
		 */
        public abstract void SendEof();

        /**
		 * closes this channel
		 */
        public abstract void Close();
    }
}