using System;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;

namespace Packet
{
    public sealed partial class TerminalEmulator
    {
        #region OnReveiveData

        public bool plus = false;

        private void OnReceivedData(IAsyncResult ar)
        {
            try
            {
                // Get The connection socket from the callback
                var stateObject = (UcCommsStateObject)ar.AsyncState;
                // Get The data , if any
                var nBytesRec = stateObject.Socket.EndReceive(ar);
                bool makefile = false;
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

                        var lines = _dataFile.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                        for (var i = lines.Length - 1; i > 0; i--)
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

                        if (sReceived.Contains(BbsPrompt))
                        {
                            if (_msgstate == "First")
                            {
                                _fstmsg = 0;
                                for (var i = 1; i < lines.Length - 1;)
                                {
                                    var checkstring = lines[i].Substring(0, 5);
                                    int result;
                                    if (int.TryParse(checkstring, out result))
                                    {
                                        FileSql.WriteSqlPacket(lines[i]);
                                        LastNumber = Convert.ToInt32(lines[i].Substring(0, 5));
                                        if (lines[i + 1].Contains(BbsPrompt))
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
                                if (sReceived.Contains(BbsPrompt))
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
                                    if (sReceived.Contains(BbsPrompt))
                                    {
                                        DispatchMessage(this, "R " + _nb[_msgno]);
                                        DispatchMessage(this, Environment.NewLine);
                                        _msgstate = "prompt";

                                        Invoke(RxdTextEvent, string.Copy(sReceived));

                                        Invoke(RefreshEvent);
                                        // Re-Establish the next asynchronous received data callback as
                                        stateObject.Socket.BeginReceive(stateObject.Buffer, 0, stateObject.Buffer.Length,
                                            SocketFlags.None, OnReceivedData, stateObject);
                                        return;
                                    }
                                }
                            }
                            //if (sReceived.Contains(BBSPrompt))

                            if (_msgstate == "prompt")
                            {
                                var dfile = "";
                                if (_nb != null)
                                {
                                    var lastNumber = _nb[_msgno] % 10;

                                    //var
                                    plus = sReceived.Contains("go_7+.");

                                    string result = null;

                                    for (var i = _fstmsg; i < (lines.Length - 1); i++)
                                    {
                                        if (lines[i].Contains("go_7+"))
                                        {
                                            var start = lines[i].IndexOf("go_7+", 0, StringComparison.Ordinal);
                                            if (start == 1)
                                            {
                                                dfile = null;
                                                makefile = true;
                                            }
                                            if (makefile)
                                            {
                                                dfile = dfile + lines[i] + Environment.NewLine;
                                            }
                                        }
                                        else
                                        {
                                            if (makefile)
                                            {
                                                dfile = dfile + lines[i] + Environment.NewLine;
                                            }
                                        }

                                        if (lines[i].Contains("stop_7+"))
                                        {
                                            var start = lines[i].IndexOf("(", StringComparison.Ordinal) + 1;
                                            var end = lines[i].IndexOf("/", start, StringComparison.Ordinal);
                                            if (end == -1)
                                            {
                                                end = lines[i].IndexOf(")", start, StringComparison.Ordinal);
                                            }
                                            result = lines[i].Substring(start, end - start);
                                            makefile = false;
                                        }
                                    }
                                    if (plus)
                                    {
                                        FileSql.WriteSt(dfile, result, "7plus", false);
                                    }
                                    else
                                    {
                                        FileSql.WriteSt(dfile, _nb[_msgno].ToString(), lastNumber.ToString(), true);
                                    }
                                    FileSql.SqlupdateRead(_nb[_msgno]);
                                }

                                _dataFile = "";
                                _fstmsg = 1;
                                _msgno++;
                                _msgstate = "Second";
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
                    Invoke(RxdTextEvent, string.Copy(sReceived));
                    Invoke(RefreshEvent);
                    // Re-Establish the next asynchronous received data callback as
                    if (stateObject.Socket.Connected)
                    {
                        stateObject.Socket.BeginReceive(stateObject.Buffer, 0, stateObject.Buffer.Length,
                            SocketFlags.None, OnReceivedData, stateObject);
                    }
                }
                else
                {
                    // If no data was received then the connection is probably dead
                    stateObject.Socket.Shutdown(SocketShutdown.Both);
                    stateObject.Socket.Close();
                    Disconnectby();
                    //}
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

        #endregion OnReveiveData
    }
}