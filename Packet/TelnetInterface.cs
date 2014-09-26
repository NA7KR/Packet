#region Using Directive
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
#endregion

namespace Packet
{
    enum Verbs {
        WILL = 251,
        WONT = 252,
        DO = 253,
        DONT = 254,
        IAC = 255
    }

    enum Options
    {
        SGA = 3
    }

    class TelnetConnection
    {
        TcpClient tcpSocket;

        int TimeOutMs = 100;

        //---------------------------------------------------------------------------------------------------------
        // TelnetConnection
        //---------------------------------------------------------------------------------------------------------
        #region TelnetConnection
        public TelnetConnection(string Hostname, int Port)
        {
            tcpSocket = new TcpClient(Hostname, Port);

        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // Telnet_Close
        //---------------------------------------------------------------------------------------------------------
        #region Telnet_Close
        public void Telnet_Close()
        {
            try
            {
                if (!tcpSocket.Connected) return;
                tcpSocket.GetStream().Close();
                tcpSocket.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            }
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        //  Login
        //---------------------------------------------------------------------------------------------------------
        #region  Login
        public string Login(string Username,string Password,int LoginTimeOutMs)
        {
            try
            {
                int oldTimeOutMs = TimeOutMs;
                TimeOutMs = LoginTimeOutMs;
                string s = Read();
                //if (!s.TrimEnd().EndsWith(":"))
                //   throw new Exception("Failed to connect : no login prompt");
                //WriteLine(Username);

                s += Read();
                //if (!s.TrimEnd().EndsWith(":"))
                //    throw new Exception("Failed to connect : no password prompt");
                WriteLine(Password);

                s += Read();
                TimeOutMs = oldTimeOutMs;
                return s;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            }
            return "";
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // WriteLine
        //---------------------------------------------------------------------------------------------------------
        #region WriteLine
        public void WriteLine(string cmd)
        {
            try
            {
                Write(cmd + "\n");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            } 
            
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // Write
        //---------------------------------------------------------------------------------------------------------
        #region Write
        public void Write(string cmd)
        {
            try
            {
                if (!tcpSocket.Connected) return;
                byte[] buf = System.Text.ASCIIEncoding.ASCII.GetBytes(cmd.Replace("\0xFF", "\0xFF\0xFF"));
                tcpSocket.GetStream().Write(buf, 0, buf.Length);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            } 
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // Read
        //---------------------------------------------------------------------------------------------------------
        #region Read
        public string Read()
        {
            try
            {
                if (!tcpSocket.Connected) return null;
                StringBuilder sb = new StringBuilder();
                do
                {
                    ParseTelnet(sb);
                    System.Threading.Thread.Sleep(TimeOutMs);
                } while (tcpSocket.Available > 0);
                return sb.ToString();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            }
            return "";
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // IsConnected
        //---------------------------------------------------------------------------------------------------------
        #region IsConnected
        public bool IsConnected
        {
            get { return tcpSocket.Connected; }
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // ParseTelnet
        //---------------------------------------------------------------------------------------------------------
        #region ParseTelnet
        void ParseTelnet(StringBuilder sb)
        {
            while (tcpSocket.Available > 0)
            {
                int input = tcpSocket.GetStream().ReadByte();
                switch (input)
                {
                    case -1 :
                        break;
                    case (int)Verbs.IAC:
                        // interpret as command
                        int inputverb = tcpSocket.GetStream().ReadByte();
                        if (inputverb == -1) break;
                        switch (inputverb)
                        {
                            case (int)Verbs.IAC: 
                                //literal IAC = 255 escaped, so append char 255 to string
                                sb.Append(inputverb);
                                break;
                            case (int)Verbs.DO: 
                            case (int)Verbs.DONT:
                            case (int)Verbs.WILL:
                            case (int)Verbs.WONT:
                                // reply to all commands with "WONT", unless it is SGA (suppres go ahead)
                                int inputoption = tcpSocket.GetStream().ReadByte();
                                if (inputoption == -1) break;
                                tcpSocket.GetStream().WriteByte((byte)Verbs.IAC);
                                if (inputoption == (int)Options.SGA )
                                    tcpSocket.GetStream().WriteByte(inputverb == (int)Verbs.DO ? (byte)Verbs.WILL:(byte)Verbs.DO); 
                                else
                                    tcpSocket.GetStream().WriteByte(inputverb == (int)Verbs.DO ? (byte)Verbs.WONT : (byte)Verbs.DONT); 
                                tcpSocket.GetStream().WriteByte((byte)inputoption);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        sb.Append( (char)input );
                        break;
                }
            }
        }
        #endregion
    }
}
