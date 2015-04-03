using System;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Windows.Forms;

namespace PacketComs
{
    public sealed partial class TerminalEmulator
    {
        #region OnReceiveData

        private void OnReceivedData(IAsyncResult ar)
        {
            try
            {
                // Get The connection socket from the callback
                var stateObject = (UcCommsStateObject)ar.AsyncState;
                bool plus;
                // Get The data , if any
                var nBytesRec = stateObject.Socket.EndReceive(ar);
                if (nBytesRec > 0)
                {
                    var sReceived = "";

                    for (var i = 0; i < nBytesRec; i++)
                    {
                        sReceived += Convert.ToChar(stateObject.Buffer[i]).ToString(CultureInfo.InvariantCulture);
                    }
                    if (sReceived.Contains(UernamePrompt))
                    {
                        DispatchMessage(this, Username);
                        DispatchMessage(this, Environment.NewLine);
                    }
                    if (sReceived.Contains(PasswordPrompt))
                    {
                        DispatchMessage(this, Password);
                        DispatchMessage(this, Environment.NewLine);
                    }

                    if (FileActive)
                    {
                        _dataFile = _dataFile + sReceived;

                        var lines = _dataFile.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
                        for (var i = lines.Length-1; i > 0; i--)
                        {
                            if (lines[i] == lines[i - 1])
                            {
                                lines = lines.Take(i).ToArray();
                            }
                            else
                            {
                                break;
                            }
                        }

                        sReceived = string.Join(Environment.NewLine, lines);
                        if (sReceived.Contains("There are no such messages"))
                        {
                            _msgstate = "Second";
                        }

                        if (sReceived.Contains(BBSPrompt))
                        {
                            if (_msgstate == "First")
                            {
                                fstmsg = 0;
                                for (var i = 1; i < lines.Length - 1; )
                                {
                                    string checkstring = lines[i].Substring(0, 5);
                                    int result;
                                    if (Int32.TryParse(checkstring, out result))
                                    {
                                        FileSql.WriteSqlPacket(lines[i]);
                                        LastNumber = Convert.ToInt32(lines[i].Substring(0, 5));
                                        if (lines[i + 1].Contains(BBSPrompt))
                                        {
                                            i = lines.Length;
                                        }
                                        else
                                        {
                                            i++;
                                        }

                                    }
                                    else
                                    {
                                        i++;
                                    }
                                }
                                LastNumberevt(this, new EventArgs());
                                _msgstate = "Second";
                            }

                            if (_msgstate == "Second")
                            {
                                if (sReceived.Contains(BBSPrompt))
                                {
                                    FileSql.UpdateSqlto("MSGTO");
                                    FileSql.UpdateSqlto("MSGFrom");
                                    FileSql.UpdateSqlto("MSGRoute");
                                    FileSql.UpdateSqlto("MSGSubject");
                                    RunCustom.RunSqlCustom();
                                    FileSql.SqlPacketDelete("MSGTO");
                                    FileSql.SqlPacketDelete("MSGFrom");
                                    FileSql.SqlPacketDelete("MSGRoute");
                                    FileSql.SqlPacketDelete("MSGSubject");
                                    _nb = FileSql.SqlSelectMail();
                                    _msgstate = "file";
                                    _dataFile = "";
                                }
                            }
                            if (_msgstate == "file")
                            {
                                if (_nb == null || _nb.Length == 0)
                                {
                                    _msgstate = "exit";
                                }
                                else
                                {
                                    if (sReceived.Contains(BBSPrompt))
                                    {
                                        
                                        DispatchMessage(this, "R " + _nb[_msgno].ToString());
                                        DispatchMessage(this, Environment.NewLine);
                                        _msgstate = "prompt";
                                        Invoke(RxdTextEvent, String.Copy(sReceived));
                                        Invoke(RefreshEvent);
                                        // Re-Establish the next asyncronous receveived data callback as
                                        stateObject.Socket.BeginReceive(stateObject.Buffer, 0, stateObject.Buffer.Length,
                                            SocketFlags.None, OnReceivedData, stateObject);
                                        return;
                                    }
                                }
                            }
                            //if (sReceived.Contains(BBSPrompt))
                            
                            
                             if (_msgstate == "prompt")
                                {
                                    string dfile = "";
                                    if (_nb != null)
                                    {
                                        Int32 lastNumber = _nb[_msgno] % 10;
                                        if (sReceived.Contains("go_7+."))
                                        {
                                            plus = true;
                                        }
                                        else
                                        {
                                            plus = false;
                                        }
                                       string result;
                                        result = null;
                                        for (var i = fstmsg; i < (lines.Length - 1); i++)
                                        {
                                            dfile = dfile + lines[i] + Environment.NewLine;
                                            if (lines[i].Contains("stop_7+"))
                                            {
                                                int start = lines[i].IndexOf("(", StringComparison.Ordinal) + 1;
                                                int end = lines[i].IndexOf("/", start, StringComparison.Ordinal);
                                                if (end == 0)
                                                {
                                                     end = lines[i].IndexOf(")", start, StringComparison.Ordinal);
                                                }
                                                 result = lines[i].Substring(start, end - start);
                                                
                                            }
                                        }
                                        if (plus)
                                        {
                                            FileSql.WriteSt(dfile, result, "7plus", false);
                                        }
                                         else
                                        {
                                            FileSql.WriteSt(dfile, _nb[_msgno].ToString(), lastNumber.ToString(),true);
                                        }
                                    FileSql.SqlupdateRead(_nb[_msgno]);
                                    }
                                    
                                    _dataFile = "";
                                    fstmsg = 1;
                                    _msgno++;
                                    _msgstate = "First";
                                }

                           // }
                           
                            if (_nb != null && _msgno == _nb.Length)
                            {
                                ForwardDone(this, new EventArgs());
                              
                                FileActive = false;
                                stateObject.Socket.Shutdown(SocketShutdown.Both);
                                stateObject.Socket.Close();
                                //Disconnect();
                                Disconnectby();
                                return;
                            }
                            if (_msgstate == "exit")
                            {
                                ForwardDone(this, new EventArgs());
                                FileActive = false;
                            }
                        }
                    }
                    Invoke(RxdTextEvent, String.Copy(sReceived));
                    Invoke(RefreshEvent);
                    // Re-Establish the next asyncronous receveived data callback as
                    if (stateObject.Socket.Connected)
                    {
                        stateObject.Socket.BeginReceive(stateObject.Buffer, 0, stateObject.Buffer.Length,
                            SocketFlags.None, OnReceivedData, stateObject);
                    }
                }
                else
                {
                    // If no data was recieved then the connection is probably dead
                    stateObject.Socket.Shutdown(SocketShutdown.Both);
                    stateObject.Socket.Close();
                    Disconnectby();
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("OnReceivedData");
            }
            if (_msgstate == "prompt")
            {
                DispatchMessage(this, Environment.NewLine);
            }

        }

        #endregion
    }
}
