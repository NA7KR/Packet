using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using PacketComs.SSHC;
using PacketComs.SSHCV1;

namespace PacketComs
{
    public sealed class Ssh1Connection : SshConnection
    {
        private const int AuthNotRequired = 0;
        private const int AuthRequired = 1;

        private Ssh1ConnectionInfo _cInfo;
        private int _shellId;

        private SSH1PacketBuilder _packetBuilder;
        private bool _executingShell;


        public Ssh1Connection(SshConnectionParameter param, ISshConnectionEventReceiver er, string serverversion,
            string clientversion) : base(param, er)
        {
            _cInfo = new Ssh1ConnectionInfo();
            _cInfo._serverVersionString = serverversion;
            _cInfo.ClientVersionString = clientversion;
            _shellId = -1;
            _packetBuilder = new SSH1PacketBuilder(new SynchronizedSSH1PacketHandler());
        }

        public override SshConnectionInfo ConnectionInfo
        {
            get { return _cInfo; }
        }

        internal override IByteArrayHandler PacketBuilder
        {
            get { return _packetBuilder; }
        }

        public override int ChannelCount
        {
            get
            {
                return base.ChannelCount + 1; //'1' is for the shell
            }
        }

        internal override AuthenticationResult Connect(AbstractSocket s)
        {
            Stream = s;

            // Phase2 receives server keys
            ReceiveServerKeys();
            if (_param.KeyCheck != null && !_param.KeyCheck(_cInfo))
            {
                Stream.Close();
                return AuthenticationResult.Failure;
            }

            // Phase3 generates session key
            byte[] sessionKey = GenerateSessionKey();

            // Phase4 establishes the session key
            try
            {
                _packetBuilder.SetSignal(false);
                SendSessionKey(sessionKey);
                InitCipher(sessionKey);
            }
            finally
            {
                _packetBuilder.SetSignal(true);
            }
            ReceiveKeyConfirmation();

            // Phase5 user authentication
            SendUserName(_param.UserName);
            if (ReceiveAuthenticationRequirement() == AuthRequired)
            {
                if (_param.AuthenticationType == AuthenticationType.Password)
                {
                    SendPlainPassword();
                }
                else if (_param.AuthenticationType == AuthenticationType.PublicKey)
                {
                    DoRsaChallengeResponse();
                }
                bool auth = ReceiveAuthenticationResult();
                if (!auth) throw new SSHException(Strings.GetString("AuthenticationFailed"));
            }

            _packetBuilder.Handler = new CallbackSSH1PacketHandler(this);
            return AuthenticationResult.Success;
        }

        internal void Transmit(SSH1Packet p)
        {
            lock (this)
            {
                p.WriteTo(Stream, Cipher);
            }
        }

        public override void Disconnect(string msg)
        {
            if (Closed) return;
            SSH1DataWriter w = new SSH1DataWriter();
            w.Write(msg);
            SSH1Packet p = SSH1Packet.FromPlainPayload(PacketComs.SSHCV1.PacketType.SSH_MSG_DISCONNECT, w.ToByteArray());
            p.WriteTo(Stream, Cipher);
            Stream.Flush();
            Closed = true;
            Stream.Close();
        }

        public override void Close()
        {
            if (Closed) return;
            Closed = true;
            Stream.Close();
        }

        public override void SendIgnorableData(string msg)
        {
            SSH1DataWriter w = new SSH1DataWriter();
            w.Write(msg);
            SSH1Packet p = SSH1Packet.FromPlainPayload(PacketComs.SSHCV1.PacketType.SSH_MSG_IGNORE, w.ToByteArray());
            Transmit(p);
        }


        private void ReceiveServerKeys()
        {
            SSH1Packet ssh1Packet = ReceivePacket();
            if (ssh1Packet.Type != PacketComs.SSHCV1.PacketType.SSH_SMSG_PUBLIC_KEY)
                throw new SSHException("unexpected SSH SSH1Packet type " + ssh1Packet.Type, ssh1Packet.Data);

            SSH1DataReader reader = new SSH1DataReader(ssh1Packet.Data);
            _cInfo.Serverinfo = new SSHServerInfo(reader);
            _cInfo.Hostkey = new RSAPublicKey(_cInfo.Serverinfo.host_key_public_exponent,
                _cInfo.Serverinfo.host_key_public_modulus);

            //read protocol support parameters
            int protocolFlags = reader.ReadInt32();
            int supportedCiphersMask = reader.ReadInt32();
            _cInfo.SetSupportedCipherAlgorithms(supportedCiphersMask);
            int supportedAuthenticationsMask = reader.ReadInt32();
            //Debug.WriteLine(String.Format("ServerOptions {0} {1} {2}", protocol_flags, supported_ciphers_mask, supported_authentications_mask));

            if (reader.Rest > 0) throw new SSHException("data length mismatch", ssh1Packet.Data);

            //Debug Info
            /*
			System.out.println("Flags="+protocol_flags);
			System.out.println("Cipher="+supported_ciphers_mask);
			System.out.println("Auth="+supported_authentications_mask);
			*/

            bool found = false;
            foreach (CipherAlgorithm a in _param.PreferableCipherAlgorithms)
            {
                if (a != CipherAlgorithm.Blowfish && a != CipherAlgorithm.TripleDes)
                    continue;
                else if (a == CipherAlgorithm.Blowfish &&
                         (supportedCiphersMask & (1 << (int) CipherAlgorithm.Blowfish)) == 0)
                    continue;
                else if (a == CipherAlgorithm.TripleDes &&
                         (supportedCiphersMask & (1 << (int) CipherAlgorithm.TripleDes)) == 0)
                    continue;

                _cInfo._algorithmForReception = _cInfo._algorithmForTransmittion = a;
                found = true;
                break;
            }

            if (!found)
                throw new SSHException(String.Format(Strings.GetString("ServerNotSupportedX"), "Blowfish/TripleDES"));

            if (_param.AuthenticationType == AuthenticationType.Password &&
                (supportedAuthenticationsMask & (1 << (int) AuthenticationType.Password)) == 0)
                throw new SSHException(String.Format(Strings.GetString("ServerNotSupportedPassword")), ssh1Packet.Data);
            if (_param.AuthenticationType == AuthenticationType.PublicKey &&
                (supportedAuthenticationsMask & (1 << (int) AuthenticationType.PublicKey)) == 0)
                throw new SSHException(String.Format(Strings.GetString("ServerNotSupportedRSA")), ssh1Packet.Data);
        }

        private byte[] GenerateSessionKey()
        {
            //session key(256bits)
            byte[] sessionKey = new byte[32];
            _param.Random.NextBytes(sessionKey);
            //for(int i=0; i<32; i++) Debug.Write(String.Format("0x{0:x}, ", session_key[i]));

            return sessionKey;
        }

        private void SendSessionKey(byte[] sessionKey)
        {
            try
            {
                //step1 XOR with session_id
                byte[] workingData = new byte[sessionKey.Length];
                byte[] sessionId = CalcSessionId();
                Array.Copy(sessionKey, 0, workingData, 0, sessionKey.Length);
                for (int i = 0; i < sessionId.Length; i++) workingData[i] ^= sessionId[i];

                //step2 decrypts with RSA
                RSAPublicKey firstEncryption;
                RSAPublicKey secondEncryption;
                SSHServerInfo si = _cInfo.Serverinfo;
                int firstKeyBytelen, secondKeyBytelen;
                if (si.server_key_bits < si.host_key_bits)
                {
                    firstEncryption = new RSAPublicKey(si.server_key_public_exponent, si.server_key_public_modulus);
                    secondEncryption = new RSAPublicKey(si.host_key_public_exponent, si.host_key_public_modulus);
                    firstKeyBytelen = (si.server_key_bits + 7)/8;
                    secondKeyBytelen = (si.host_key_bits + 7)/8;
                }
                else
                {
                    firstEncryption = new RSAPublicKey(si.host_key_public_exponent, si.host_key_public_modulus);
                    secondEncryption = new RSAPublicKey(si.server_key_public_exponent, si.server_key_public_modulus);
                    firstKeyBytelen = (si.host_key_bits + 7)/8;
                    secondKeyBytelen = (si.server_key_bits + 7)/8;
                }

                BigInteger firstResult =
                    RSAUtil.PKCS1PadType2(new BigInteger(workingData), firstKeyBytelen, _param.Random)
                        .modPow(firstEncryption.Exponent, firstEncryption.Modulus);
                BigInteger secondResult =
                    RSAUtil.PKCS1PadType2(firstResult, secondKeyBytelen, _param.Random)
                        .modPow(secondEncryption.Exponent, secondEncryption.Modulus);

                //output
                SSH1DataWriter writer = new SSH1DataWriter();
                writer.Write((byte) _cInfo._algorithmForTransmittion);
                writer.Write(si.anti_spoofing_cookie);
                writer.Write(secondResult);
                writer.Write(0); //protocol flags

                //send
                SSH1Packet ssh1Packet = SSH1Packet.FromPlainPayload(PacketComs.SSHCV1.PacketType.SSH_CMSG_SESSION_KEY,
                    writer.ToByteArray());
                ssh1Packet.WriteTo(Stream);

                SessionId = sessionId;
            }
            catch (Exception e)
            {
                if (e is IOException)
                    throw;
                else
                {
                    throw new SSHException(e.Message); //IOException�ȊO�݂͂�SSHException�ɂ��Ă��܂�
                }
            }
        }

        private void ReceiveKeyConfirmation()
        {
            SSH1Packet ssh1Packet = ReceivePacket();
            if (ssh1Packet.Type != PacketComs.SSHCV1.PacketType.SSH_SMSG_SUCCESS)
                throw new SSHException("unexpected packet type [" + ssh1Packet.Type + "] at ReceiveKeyConfirmation()",
                    ssh1Packet.Data);
        }

        private int ReceiveAuthenticationRequirement()
        {
            SSH1Packet ssh1Packet = ReceivePacket();
            if (ssh1Packet.Type == PacketComs.SSHCV1.PacketType.SSH_SMSG_SUCCESS)
                return AuthNotRequired;
            else if (ssh1Packet.Type == PacketComs.SSHCV1.PacketType.SSH_SMSG_FAILURE)
                return AuthRequired;
            else
                throw new SSHException("type " + ssh1Packet.Type, ssh1Packet.Data);
        }

        private void SendUserName(string username)
        {
            SSH1DataWriter writer = new SSH1DataWriter();
            writer.Write(username);
            SSH1Packet ssh1Packet = SSH1Packet.FromPlainPayload(PacketComs.SSHCV1.PacketType.SSH_CMSG_USER, writer.ToByteArray());
            ssh1Packet.WriteTo(Stream, Cipher);
        }

        private void SendPlainPassword()
        {
            SSH1DataWriter writer = new SSH1DataWriter();
            writer.Write(_param.Password);
            SSH1Packet ssh1Packet = SSH1Packet.FromPlainPayload(PacketComs.SSHCV1.PacketType.SSH_CMSG_AUTH_PASSWORD, writer.ToByteArray());
            ssh1Packet.WriteTo(Stream, Cipher);
        }

        //RSA authentication
        private void DoRsaChallengeResponse()
        {
            //read key
            SSH1UserAuthKey key = new SSH1UserAuthKey(_param.IdentityFile, _param.Password);
            SSH1DataWriter w = new SSH1DataWriter();
            w.Write(key.PublicModulus);
            SSH1Packet p = SSH1Packet.FromPlainPayload(PacketComs.SSHCV1.PacketType.SSH_CMSG_AUTH_RSA, w.ToByteArray());
            p.WriteTo(Stream, Cipher);

            p = ReceivePacket();
            if (p.Type == PacketComs.SSHCV1.PacketType.SSH_SMSG_FAILURE)
                throw new SSHException(Strings.GetString("ServerRefusedRSA"));
            else if (p.Type != PacketComs.SSHCV1.PacketType.SSH_SMSG_AUTH_RSA_CHALLENGE)
                throw new SSHException(String.Format(Strings.GetString("UnexpectedResponse"), p.Type));

            //creating challenge
            SSH1DataReader r = new SSH1DataReader(p.Data);
            BigInteger challenge = key.decryptChallenge(r.ReadMpInt());
            byte[] rawchallenge = RSAUtil.StripPKCS1Pad(challenge, 2).getBytes();

            //building response
            MemoryStream bos = new MemoryStream();
            bos.Write(rawchallenge, 0, rawchallenge.Length); //!!mindterm�ł͓����O���ǂ����ŕςȃn���h�����O��������
            bos.Write(SessionId, 0, SessionId.Length);
            byte[] response = new MD5CryptoServiceProvider().ComputeHash(bos.ToArray());

            w = new SSH1DataWriter();
            w.Write(response);
            p = SSH1Packet.FromPlainPayload(PacketComs.SSHCV1.PacketType.SSH_CMSG_AUTH_RSA_RESPONSE, w.ToByteArray());
            p.WriteTo(Stream, Cipher);
        }

        private bool ReceiveAuthenticationResult()
        {
            SSH1Packet ssh1Packet = ReceivePacket();
            PacketComs.SSHCV1.PacketType type = ssh1Packet.Type;
            if (type == PacketComs.SSHCV1.PacketType.SSH_MSG_DEBUG)
            {
                new SSH1DataReader(ssh1Packet.Data);
                //Debug.WriteLine("receivedd debug message:"+Encoding.ASCII.GetString(r.ReadString()));
                return ReceiveAuthenticationResult();
            }
            else if (type == PacketComs.SSHCV1.PacketType.SSH_SMSG_SUCCESS)
                return true;
            else if (type == PacketComs.SSHCV1.PacketType.SSH_SMSG_FAILURE)
                return false;
            else
                throw new SSHException("type: " + type, ssh1Packet.Data);
        }

        public override SshChannel OpenShell(ISshChannelEventReceiver receiver)
        {
            if (_shellId != -1)
                throw new SSHException("A shell is opened already");
            _shellId = RegisterChannelEventReceiver(null, receiver).LocalId;
            SendRequestPty();
            _executingShell = true;
            return new SSH1Channel(this, ChannelType.Shell, _shellId);
        }

        private void SendRequestPty()
        {
            SSH1DataWriter writer = new SSH1DataWriter();
            writer.Write(_param.TerminalName);
            writer.Write(_param.TerminalHeight);
            writer.Write(_param.TerminalWidth);
            writer.Write(_param.TerminalPixelWidth);
            writer.Write(_param.TerminalPixelHeight);
            writer.Write(new byte[1]); //TTY_OP_END
            SSH1Packet ssh1Packet = SSH1Packet.FromPlainPayload(PacketComs.SSHCV1.PacketType.SSH_CMSG_REQUEST_PTY, writer.ToByteArray());
            ssh1Packet.WriteTo(Stream, Cipher);
        }

        private void ExecShell()
        {
            //System.out.println("EXEC SHELL");
            SSH1Packet ssh1Packet = SSH1Packet.FromPlainPayload(PacketComs.SSHCV1.PacketType.SSH_CMSG_EXEC_SHELL);
            ssh1Packet.WriteTo(Stream, Cipher);
        }

        public override SshChannel ForwardPort(ISshChannelEventReceiver receiver, string remoteHost, int remotePort,
            string originatorHost, int originatorPort)
        {
            if (_shellId == -1)
            {
                ExecShell();
                _shellId = RegisterChannelEventReceiver(null, new SSH1DummyReceiver()).LocalId;
            }

            int localId = RegisterChannelEventReceiver(null, receiver).LocalId;

            SSH1DataWriter writer = new SSH1DataWriter();
            writer.Write(localId); //channel id is fixed to 0
            writer.Write(remoteHost);
            writer.Write(remotePort);
            //originator is specified only if SSH_PROTOFLAG_HOST_IN_FWD_OPEN is specified
            //writer.Write(originator_host);
            SSH1Packet ssh1Packet = SSH1Packet.FromPlainPayload(PacketComs.SSHCV1.PacketType.SSH_MSG_PORT_OPEN, writer.ToByteArray());
            ssh1Packet.WriteTo(Stream, Cipher);

            return new SSH1Channel(this, ChannelType.ForwardedLocalToRemote, localId);
        }

        public override void ListenForwardedPort(string allowedHost, int bindPort)
        {
            SSH1DataWriter writer = new SSH1DataWriter();
            writer.Write(bindPort);
            writer.Write(allowedHost);
            writer.Write(0);
            SSH1Packet p = SSH1Packet.FromPlainPayload(PacketComs.SSHCV1.PacketType.SSH_CMSG_PORT_FORWARD_REQUEST, writer.ToByteArray());
            p.WriteTo(Stream, Cipher);

            if (_shellId == -1)
            {
                ExecShell();
                _shellId = RegisterChannelEventReceiver(null, new SSH1DummyReceiver()).LocalId;
            }
        }

        public override void CancelForwardedPort(string host, int port)
        {
            throw new NotSupportedException("not implemented");
        }

        private void ProcessPortforwardingRequest(ISshConnectionEventReceiver receiver, SSH1Packet packet)
        {
            SSH1DataReader reader = new SSH1DataReader(packet.Data);
            int serverChannel = reader.ReadInt32();
            string host = Encoding.ASCII.GetString(reader.ReadString());
            int port = reader.ReadInt32();

            SSH1DataWriter writer = new SSH1DataWriter();
            PortForwardingCheckResult result = receiver.CheckPortForwardingRequest(host, port, "", 0);
            if (result.Allowed)
            {
                int localId = RegisterChannelEventReceiver(null, result.Channel).LocalId;
                _eventReceiver.EstablishPortforwarding(result.Channel,
                    new SSH1Channel(this, ChannelType.ForwardedRemoteToLocal, localId, serverChannel));

                writer.Write(serverChannel);
                writer.Write(localId);
                SSH1Packet p = SSH1Packet.FromPlainPayload(PacketComs.SSHCV1.PacketType.SSH_MSG_CHANNEL_OPEN_CONFIRMATION,
                    writer.ToByteArray());
                p.WriteTo(Stream, Cipher);
            }
            else
            {
                writer.Write(serverChannel);
                SSH1Packet p = SSH1Packet.FromPlainPayload(PacketComs.SSHCV1.PacketType.SSH_MSG_CHANNEL_OPEN_FAILURE, writer.ToByteArray());
                p.WriteTo(Stream, Cipher);
            }
        }

        private byte[] CalcSessionId()
        {
            MemoryStream bos = new MemoryStream();
            SSHServerInfo si = _cInfo.Serverinfo;
            byte[] h = si.host_key_public_modulus.getBytes();
            byte[] s = si.server_key_public_modulus.getBytes();
            //System.out.println("len h="+h.Length);
            //System.out.println("len s="+s.Length);

            int offH = (h[0] == 0 ? 1 : 0);
            int offS = (s[0] == 0 ? 1 : 0);
            bos.Write(h, offH, h.Length - offH);
            bos.Write(s, offS, s.Length - offS);
            bos.Write(si.anti_spoofing_cookie, 0, si.anti_spoofing_cookie.Length);

            byte[] sessionId = new MD5CryptoServiceProvider().ComputeHash(bos.ToArray());
            //System.out.println("sess-id-len=" + session_id.Length);
            return sessionId;
        }

        //init ciphers
        private void InitCipher(byte[] sessionKey)
        {
            Cipher = CipherFactory.CreateCipher(SSHProtocol.SSH1, _cInfo._algorithmForTransmittion, sessionKey);
            ICipher rc = CipherFactory.CreateCipher(SSHProtocol.SSH1, _cInfo._algorithmForReception, sessionKey);
            _packetBuilder.SetCipher(rc, _param.CheckMacError);
        }

        private SSH1Packet ReceivePacket()
        {
            while (true)
            {
                SSH1Packet p;
                SynchronizedSSH1PacketHandler handler = (SynchronizedSSH1PacketHandler) _packetBuilder.Handler;
                if (!handler.HasPacket)
                {
                    handler.Wait();
                    if (handler.State == ReceiverState.Error)
                        throw new SSHException(handler.ErrorMessage);
                    else if (handler.State == ReceiverState.Closed)
                        throw new SSHException("socket closed");
                }
                p = handler.PopPacket();

                SSH1DataReader r = new SSH1DataReader(p.Data);
                PacketComs.SSHCV1.PacketType pt = p.Type;
                if (pt == PacketComs.SSHCV1.PacketType.SSH_MSG_IGNORE)
                {
                    if (_eventReceiver != null) _eventReceiver.OnIgnoreMessage(r.ReadString());
                }
                else if (pt == PacketComs.SSHCV1.PacketType.SSH_MSG_DEBUG)
                {
                    if (_eventReceiver != null) _eventReceiver.OnDebugMessage(false, r.ReadString());
                }
                else
                    return p;
            }
        }

        internal void AsyncReceivePacket(SSH1Packet p)
        {
            try
            {
                int len = 0, channel = 0;
                switch (p.Type)
                {
                    case PacketComs.SSHCV1.PacketType.SSH_SMSG_STDOUT_DATA:
                        len = SSHUtil.ReadInt32(p.Data, 0);
                        FindChannelEntry(_shellId).Receiver.OnData(p.Data, 4, len);
                        break;
                    case PacketComs.SSHCV1.PacketType.SSH_SMSG_STDERR_DATA:
                    {
                        SSH1DataReader re = new SSH1DataReader(p.Data);
                        FindChannelEntry(_shellId)
                            .Receiver.OnExtendedData((int) PacketComs.SSHCV1.PacketType.SSH_SMSG_STDERR_DATA, re.ReadString());
                    }
                        break;
                    case PacketComs.SSHCV1.PacketType.SSH_MSG_CHANNEL_DATA:
                        channel = SSHUtil.ReadInt32(p.Data, 0);
                        len = SSHUtil.ReadInt32(p.Data, 4);
                        FindChannelEntry(channel).Receiver.OnData(p.Data, 8, len);
                        break;
                    case PacketComs.SSHCV1.PacketType.SSH_MSG_PORT_OPEN:
                        ProcessPortforwardingRequest(_eventReceiver, p);
                        break;
                    case PacketComs.SSHCV1.PacketType.SSH_MSG_CHANNEL_CLOSE:
                    {
                        channel = SSHUtil.ReadInt32(p.Data, 0);
                        ISshChannelEventReceiver r = FindChannelEntry(channel).Receiver;
                        UnregisterChannelEventReceiver(channel);
                        r.OnChannelClosed();
                    }
                        break;
                    case PacketComs.SSHCV1.PacketType.SSH_MSG_CHANNEL_CLOSE_CONFIRMATION:
                        channel = SSHUtil.ReadInt32(p.Data, 0);
                        break;
                    case PacketComs.SSHCV1.PacketType.SSH_MSG_DISCONNECT:
                        _eventReceiver.OnConnectionClosed();
                        break;
                    case PacketComs.SSHCV1.PacketType.SSH_SMSG_EXITSTATUS:
                        FindChannelEntry(_shellId).Receiver.OnChannelClosed();
                        break;
                    case PacketComs.SSHCV1.PacketType.SSH_MSG_DEBUG:
                    {
                        SSH1DataReader re = new SSH1DataReader(p.Data);
                        _eventReceiver.OnDebugMessage(false, re.ReadString());
                    }
                        break;
                    case PacketComs.SSHCV1.PacketType.SSH_MSG_IGNORE:
                    {
                        SSH1DataReader re = new SSH1DataReader(p.Data);
                        _eventReceiver.OnIgnoreMessage(re.ReadString());
                    }
                        break;
                    case PacketComs.SSHCV1.PacketType.SSH_MSG_CHANNEL_OPEN_CONFIRMATION:
                    {
                        int local = SSHUtil.ReadInt32(p.Data, 0);
                        int remote = SSHUtil.ReadInt32(p.Data, 4);
                        FindChannelEntry(local).Receiver.OnChannelReady();
                    }
                        break;
                    case PacketComs.SSHCV1.PacketType.SSH_SMSG_SUCCESS:
                        if (_executingShell)
                        {
                            ExecShell();
                            this.FindChannelEntry(_shellId).Receiver.OnChannelReady();
                            _executingShell = false;
                        }
                        break;
                    default:
                        _eventReceiver.OnUnknownMessage((byte) p.Type, p.Data);
                        break;
                }
            }
            catch (Exception ex)
            {
                if (!Closed)
                    _eventReceiver.OnError(ex, ex.Message);
            }
        }
    }

    public class SSH1Channel : SshChannel
    {
        public SSH1Channel(SshConnection con, ChannelType type, int local_id) : base(con, type, local_id)
        {
        }

        public SSH1Channel(SshConnection con, ChannelType type, int local_id, int remote_id) : base(con, type, local_id)
        {
            RemoteId = remote_id;
        }

        /**
		 * resizes the size of terminal
		 */

        public override void ResizeTerminal(int width, int height, int pixelWidth, int pixelHeight)
        {
            SSH1DataWriter writer = new SSH1DataWriter();
            writer.Write(height);
            writer.Write(width);
            writer.Write(pixelWidth);
            writer.Write(pixelHeight);
            SSH1Packet p = SSH1Packet.FromPlainPayload(PacketComs.SSHCV1.PacketType.SSH_CMSG_WINDOW_SIZE, writer.ToByteArray());
            Transmit(p);
        }

        /**
		* transmits channel data 
		*/

        public override void Transmit(byte[] data)
        {
            SSH1DataWriter wr = new SSH1DataWriter();
            if (_type == ChannelType.Shell)
            {
                wr.WriteAsString(data);
                SSH1Packet p = SSH1Packet.FromPlainPayload(PacketComs.SSHCV1.PacketType.SSH_CMSG_STDIN_DATA, wr.ToByteArray());
                Transmit(p);
            }
            else
            {
                wr.Write(RemoteId);
                wr.WriteAsString(data);
                SSH1Packet p = SSH1Packet.FromPlainPayload(PacketComs.SSHCV1.PacketType.SSH_MSG_CHANNEL_DATA, wr.ToByteArray());
                Transmit(p);
            }
        }

        /**
		* transmits channel data 
		*/

        public override void Transmit(byte[] data, int offset, int length)
        {
            SSH1DataWriter wr = new SSH1DataWriter();
            if (_type == ChannelType.Shell)
            {
                wr.WriteAsString(data, offset, length);
                SSH1Packet p = SSH1Packet.FromPlainPayload(PacketComs.SSHCV1.PacketType.SSH_CMSG_STDIN_DATA, wr.ToByteArray());
                Transmit(p);
            }
            else
            {
                wr.Write(RemoteId);
                wr.WriteAsString(data, offset, length);
                SSH1Packet p = SSH1Packet.FromPlainPayload(PacketComs.SSHCV1.PacketType.SSH_MSG_CHANNEL_DATA, wr.ToByteArray());
                Transmit(p);
            }
        }

        public override void SendEof()
        {
        }


        /**
		 * closes this channel
		 */

        public override void Close()
        {
            if (_connection.IsClosed) return;

            if (_type == ChannelType.Shell)
            {
                SSH1DataWriter wr2 = new SSH1DataWriter();
                wr2.Write(RemoteId);
                SSH1Packet p2 = SSH1Packet.FromPlainPayload(PacketComs.SSHCV1.PacketType.SSH_CMSG_EOF, wr2.ToByteArray());
                Transmit(p2);
            }

            SSH1DataWriter wr = new SSH1DataWriter();
            wr.Write(RemoteId);
            SSH1Packet p = SSH1Packet.FromPlainPayload(PacketComs.SSHCV1.PacketType.SSH_MSG_CHANNEL_CLOSE, wr.ToByteArray());
            Transmit(p);
        }

        private void Transmit(SSH1Packet p)
        {
            ((Ssh1Connection) _connection).Transmit(p);
        }
    }

    //if port forwardings are performed without a shell, we use SSH1DummyChannel to receive shell data
    internal class SSH1DummyReceiver : ISshChannelEventReceiver
    {
        public void OnData(byte[] data, int offset, int length)
        {
        }

        public void OnExtendedData(int type, byte[] data)
        {
        }

        public void OnChannelClosed()
        {
        }

        public void OnChannelEof()
        {
        }

        public void OnChannelReady()
        {
        }

        public void OnChannelError(Exception error, string msg)
        {
        }

        public void OnMiscPacket(byte packetType, byte[] data, int offset, int length)
        {
        }
    }
}