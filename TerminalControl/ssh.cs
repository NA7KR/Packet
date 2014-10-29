#region Using Directive

using System;

#endregion

namespace PacketComs
{

    #region Routrek SSH Reader Class

    internal class Reader : ISshConnectionEventReceiver, ISshChannelEventReceiver
    {
        private TerminalEmulator Terminal;

        public Reader(object obj)
        {
            Terminal = (TerminalEmulator) obj;
        }

        public SshConnection Conn;
        public bool Ready;

        public void OnData(byte[] data, int offset, int length)
        {
            Terminal.Write(data, offset, length);
            //rtb.AppendText(Encoding.ASCII.GetString(data, offset, length));
            //System.Console.Write(Encoding.ASCII.GetString(data, offset, length));
        }

        public void OnDebugMessage(bool alwaysDisplay, byte[] data)
        {
            //Debug.WriteLine("DEBUG: "+ Encoding.ASCII.GetString(data));
        }

        public void OnIgnoreMessage(byte[] data)
        {
            //Debug.WriteLine("Ignore: "+ Encoding.ASCII.GetString(data));
        }

        public void OnAuthenticationPrompt(string[] msg)
        {
            //Debug.WriteLine("Auth Prompt "+msg[0]);
        }

        public void OnError(Exception error, string msg)
        {
            //Debug.WriteLine("ERROR: "+ msg);
        }

        public void OnChannelClosed()
        {
            //Debug.WriteLine("Channel closed");
            Conn.Disconnect("");
            //_conn.AsyncReceive(this);
            Terminal.Disconnectby();
        }

        public void OnChannelEof()
        {
            Pf.Close();
            //Debug.WriteLine("Channel EOF");
        }

        public void OnExtendedData(int type, byte[] data)
        {
            //Debug.WriteLine("EXTENDED DATA");
        }

        public void OnConnectionClosed()
        {
            //Debug.WriteLine("Connection closed");
        }

        public void OnUnknownMessage(byte type, byte[] data)
        {
            //Debug.WriteLine("Unknown Message " + type);
        }

        public void OnChannelReady()
        {
            Ready = true;
        }

        public void OnChannelError(Exception error, string msg)
        {
            //Debug.WriteLine("Channel ERROR: "+ msg);
        }

        public void OnMiscPacket(byte packetType, byte[] data, int offset, int length)
        {
        }

        public PortForwardingCheckResult CheckPortForwardingRequest(string remoteHost, int remotePort,
            string originatorHost, int originatorPort)
        {
            PortForwardingCheckResult r = new PortForwardingCheckResult();
            r.Allowed = true;
            r.Channel = this;
            return r;
        }

        public void EstablishPortforwarding(ISshChannelEventReceiver rec, SshChannel channel)
        {
            Pf = channel;
        }

        public SshChannel Pf;
    }

    #endregion
}