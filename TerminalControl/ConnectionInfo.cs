using System.Text;
using PacketComs;

namespace PacketComs.SSHC
{
    /// <summary>
    /// ConnectionInfo describes the attributes of the host or the connection.
    /// It is available after the connection is established without any errors.
    /// </summary>
    public abstract class SshConnectionInfo
    {
        internal string _serverVersionString;
        internal string ClientVersionString;
        internal string _supportedCipherAlgorithms;
        internal PublicKey Hostkey;

        internal CipherAlgorithm _algorithmForTransmittion;
        internal CipherAlgorithm _algorithmForReception;

        public string ServerVersionString
        {
            get { return _serverVersionString; }
        }

        public string ClientVerisonString
        {
            get { return ClientVersionString; }
        }

        public string SupportedCipherAlgorithms
        {
            get { return _supportedCipherAlgorithms; }
        }

        public CipherAlgorithm AlgorithmForTransmittion
        {
            get { return _algorithmForTransmittion; }
        }

        public CipherAlgorithm AlgorithmForReception
        {
            get { return _algorithmForReception; }
        }

        public PublicKey HostKey
        {
            get { return Hostkey; }
        }

        public abstract string DumpHostKeyInKnownHostsStyle();
    }
}

namespace PacketComs.SSHCV1
{
    using SSHC;

    public class Ssh1ConnectionInfo : SshConnectionInfo
    {
        internal SSHServerInfo Serverinfo;

        public override string DumpHostKeyInKnownHostsStyle()
        {
            StringBuilder bld = new StringBuilder();
            bld.Append("ssh1 ");
            SSH1DataWriter wr = new SSH1DataWriter();
            //RSA only for SSH1
            RSAPublicKey rsa = (RSAPublicKey) Hostkey;
            wr.Write(rsa.Exponent);
            wr.Write(rsa.Modulus);
            bld.Append(Encoding.ASCII.GetString(Base64.Encode(wr.ToByteArray())));
            return bld.ToString();
        }

        public void SetSupportedCipherAlgorithms(int mask)
        {
            StringBuilder bld = new StringBuilder();
            if ((mask & 2) != 0) AppendSupportedCipher(bld, "Idea");
            if ((mask & 4) != 0) AppendSupportedCipher(bld, "DES");
            if ((mask & 8) != 0) AppendSupportedCipher(bld, "TripleDES");
            if ((mask & 16) != 0) AppendSupportedCipher(bld, "TSS");
            if ((mask & 32) != 0) AppendSupportedCipher(bld, "RC4");
            if ((mask & 64) != 0) AppendSupportedCipher(bld, "Blowfish");

            _supportedCipherAlgorithms = bld.ToString();
        }

        private static void AppendSupportedCipher(StringBuilder bld, string text)
        {
            if (bld.Length > 0) bld.Append(',');
            bld.Append(text);
        }
    }
}

namespace PacketComs.SSHCV2
{
    using SSHC;

    public class Ssh2ConnectionInfo : SshConnectionInfo
    {
        internal string _supportedHostKeyAlgorithms;
        internal PublicKeyAlgorithm _algorithmForHostKeyVerification;

        public string SupportedHostKeyAlgorithms
        {
            get { return _supportedHostKeyAlgorithms; }
        }

        public PublicKeyAlgorithm AlgorithmForHostKeyVerification
        {
            get { return _algorithmForHostKeyVerification; }
        }

        public string SupportedKexAlgorithms { get; internal set; }

        public override string DumpHostKeyInKnownHostsStyle()
        {
            StringBuilder bld = new StringBuilder();
            bld.Append(SSH2Util.PublicKeyAlgorithmName(Hostkey.Algorithm));
            bld.Append(' ');
            SSH2DataWriter wr = new SSH2DataWriter();
            wr.Write(SSH2Util.PublicKeyAlgorithmName(Hostkey.Algorithm));
            if (Hostkey.Algorithm == PublicKeyAlgorithm.RSA)
            {
                RSAPublicKey rsa = (RSAPublicKey) Hostkey;
                wr.Write(rsa.Exponent);
                wr.Write(rsa.Modulus);
            }
            else if (Hostkey.Algorithm == PublicKeyAlgorithm.DSA)
            {
                DsaPublicKey dsa = (DsaPublicKey) Hostkey;
                wr.Write(dsa.P);
                wr.Write(dsa.Q);
                wr.Write(dsa.G);
                wr.Write(dsa.Y);
            }
            else
                throw new SSHException("Host key algorithm is unsupported");

            bld.Append(Encoding.ASCII.GetString(Base64.Encode(wr.ToByteArray())));
            return bld.ToString();
        }
    }
}