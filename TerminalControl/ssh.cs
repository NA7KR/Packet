#region Using Directive

using System;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;
using PacketComs;

#endregion

namespace PacketSoftware
{

    #region Routrek SSH Reader Class

    internal class Reader : Routrek.SSHC.ISSHConnectionEventReceiver, Routrek.SSHC.ISSHChannelEventReceiver
    {
        private TerminalEmulator Terminal;

        public Reader(object obj)
        {
            Terminal = (TerminalEmulator) obj;
        }

        public Routrek.SSHC.SSHConnection _conn;
        public bool _ready;

        public void OnData(byte[] data, int offset, int length)
        {
            Terminal.Write(data, offset, length);
            //rtb.AppendText(Encoding.ASCII.GetString(data, offset, length));
            //System.Console.Write(Encoding.ASCII.GetString(data, offset, length));
        }

        public void OnDebugMessage(bool always_display, byte[] data)
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
            _conn.Disconnect("");
            //_conn.AsyncReceive(this);
            Terminal.Disconnectby();
        }

        public void OnChannelEOF()
        {
            _pf.Close();
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
            _ready = true;
        }

        public void OnChannelError(Exception error, string msg)
        {
            //Debug.WriteLine("Channel ERROR: "+ msg);
        }

        public void OnMiscPacket(byte type, byte[] data, int offset, int length)
        {
        }

        public Routrek.SSHC.PortForwardingCheckResult CheckPortForwardingRequest(string host, int port,
            string originator_host, int originator_port)
        {
            Routrek.SSHC.PortForwardingCheckResult r = new Routrek.SSHC.PortForwardingCheckResult();
            r.allowed = true;
            r.channel = this;
            return r;
        }

        public void EstablishPortforwarding(Routrek.SSHC.ISSHChannelEventReceiver rec, Routrek.SSHC.SSHChannel channel)
        {
            _pf = channel;
        }

        public Routrek.SSHC.SSHChannel _pf;
    }

    #endregion
}