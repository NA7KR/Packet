using System;
using Routrek.SSHC;

namespace PacketComs
{
    /// <summary>
    /// Fill the properties of ConnectionParameter object before you start the connection.
    /// </summary>
    public class SshConnectionParameter : ICloneable
    {
        //protocol
        private SSHProtocol _protocol;

        public SSHProtocol Protocol
        {
            get { return _protocol; }
            set { _protocol = value; }
        }

        //algorithm

        private CipherAlgorithm[] _cipherAlgorithms;

        public CipherAlgorithm[] PreferableCipherAlgorithms
        {
            get { return _cipherAlgorithms; }
            set { _cipherAlgorithms = value; }
        }

        private PublicKeyAlgorithm[] _hostkeyAlgorithms;

        public PublicKeyAlgorithm[] PreferableHostKeyAlgorithms
        {
            get { return _hostkeyAlgorithms; }
            set { _hostkeyAlgorithms = value; }
        }

        //account

        private AuthenticationType _authtype;

        public AuthenticationType AuthenticationType
        {
            get { return _authtype; }
            set { _authtype = value; }
        }

        private string _username;

        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _identityFile;

        public string IdentityFile
        {
            get { return _identityFile; }
            set { _identityFile = value; }
        }

        //host
        private HostKeyCheckCallback _keycheck;

        public HostKeyCheckCallback KeyCheck
        {
            get { return _keycheck; }
            set { _keycheck = value; }
        }

        //terminal

        private string _terminalname;

        public string TerminalName
        {
            get { return _terminalname; }
            set { _terminalname = value; }
        }

        private int _width;

        public int TerminalWidth
        {
            get { return _width; }
            set { _width = value; }
        }

        private int _height;

        public int TerminalHeight
        {
            get { return _height; }
            set { _height = value; }
        }

        private int _pixelWidth;

        public int TerminalPixelWidth
        {
            get { return _pixelWidth; }
            set { _pixelWidth = value; }
        }

        private int _pixelHeight;

        public int TerminalPixelHeight
        {
            get { return _pixelHeight; }
            set { _pixelHeight = value; }
        }

        private Random _random;

        public Random Random
        {
            get { return _random; }
            set { _random = value; }
        }

        private bool _checkMacError;

        public bool CheckMacError
        {
            get { return _checkMacError; }
            set { _checkMacError = value; }
        }

        //SSH2 only property
        private int _windowsize;

        public int WindowSize
        {
            get { return _windowsize; }
            set { _windowsize = value; }
        }

        //SSH2 only property
        private int _maxpacketsize;

        public int MaxPacketSize
        {
            get { return _maxpacketsize; }
            set { _maxpacketsize = value; }
        }

        public string Ssh1VersionEol { get; set; }


        public SshConnectionParameter()
        {
            _random = new Random();
            _authtype = AuthenticationType.Password;
            _terminalname = "vt100";
            _width = 80;
            _height = 25;
            _protocol = SSHProtocol.SSH2;
            _cipherAlgorithms = new[]
            {CipherAlgorithm.Aes128, CipherAlgorithm.Blowfish, CipherAlgorithm.TripleDes};
            _hostkeyAlgorithms = new[] {PublicKeyAlgorithm.DSA, PublicKeyAlgorithm.RSA};
            _windowsize = 0x1000;
            _maxpacketsize = 0x10000;
            _checkMacError = true;
            Ssh1VersionEol = "\n";
        }

        public object Clone()
        {
            SshConnectionParameter n = new SshConnectionParameter();
            n._authtype = _authtype;
            n._cipherAlgorithms = _cipherAlgorithms;
            n._height = _height;
            n._hostkeyAlgorithms = _hostkeyAlgorithms;
            n._identityFile = _identityFile;
            n._keycheck = _keycheck;
            n._maxpacketsize = _maxpacketsize;
            n._password = _password;
            n._protocol = _protocol;
            n._random = _random;
            n._terminalname = _terminalname;
            n._username = _username;
            n._width = _width;
            n._windowsize = _windowsize;
            n._checkMacError = _checkMacError;
            return n;
        }
    }
}