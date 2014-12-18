#region Using Directive

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Renci.SshNet;
using Renci.SshNet.Common;

#endregion

namespace PacketComs
{

	#region  class TerminalEmulator : Control

	public sealed class TerminalEmulator : Control
	{
		private SshClient _client;
		private ShellStream _stream;
		private readonly FileSql _myFiles = new FileSql();
		private readonly SerialPort _port = new SerialPort();

		#region TerminalEmulator

		public TerminalEmulator()
		{
			_scrollbackBufferSize = 9000;
			_scrollbackBuffer = new StringCollection();

			// set the display options
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer,
				true);
			_keyboard = new UcKeyboard(this);
			_parser = new UcParser();
			var nvtParser = new UcTelnetParser();
			_caret = new UcCaret();
			_modes = new UcMode();
			_tabStops = new UcTabStops();
			_savedCarets = new ArrayList();
			_caret.Pos = new Point(0, 0);
			_charSize = new Size();
			Font = new Font(_typeFace, TypeSize, TypeStyle);
			_fgColor = Color.FromArgb(0, 0, 0);
			BackColor = Color.FromArgb(0, 0, 160);
			_boldColor = Color.FromArgb(255, 255, 255);
			_blinkColor = Color.Red;
			_g0 = new UcChars(UcChars.Sets.ASCII);
			_g1 = new UcChars(UcChars.Sets.ASCII);
			_g2 = new UcChars(UcChars.Sets.DECSG);
			_g3 = new UcChars(UcChars.Sets.DECSG);
			_charAttribs.Gl = _g0;
			_charAttribs.GR = _g2;
			_charAttribs.Gs = null;
			GetFontInfo();
			// Create and initialize contextmenu
			var contextMenu1 = new ContextMenu();
			var mnuCopy = new MenuItem("Copy");
			var mnuPaste = new MenuItem("Paste");
			var mnuCopyPaste = new MenuItem("Copy and Paste");
			contextMenu1.MenuItems.AddRange(new[]
			{
				mnuCopyPaste,
				mnuPaste,
				mnuCopy
			});
			mnuCopy.Index = 0;
			mnuPaste.Index = 1;
			mnuCopyPaste.Index = 2;
			ContextMenu = contextMenu1;
			mnuCopy.Click += mnuCopy_Click;
			mnuPaste.Click += mnuPaste_Click;
			mnuCopyPaste.Click += mnuCopyPaste_Click;

			// Create and initialize a VScrollBar.
			_vertScrollBar = new UcVertScrollBar();
			// _vertScrollBar.Scroll += HandleScroll;

			// Dock the scroll bar to the right side of the form.
			_vertScrollBar.Dock = DockStyle.Right;

			// Add the scroll bar to the form.
			Controls.Add(_vertScrollBar);

			// create the character grid (rows by columns). This is a shadow of what's displayed
			// Set the window size to match
			SetSize(24, 80);

			_parser.ParserEvent += CommandRouter;
			_keyboard.KeyboardEvent += DispatchMessage;
			nvtParser.NvtParserEvent += TelnetInterpreter;
			RefreshEvent += ShowBuffer;

			RxdTextEvent += s => nvtParser.ParseString(s);

			_beginDrag = new Point();
			_endDrag = new Point();

			ConnectionType = ConnectionTypes.Telnet; // default
		}

		#endregion

		#region Close Connection

		public void Closeconnection()
		{
			Close = true;
			Disconnect();
		}

		#endregion

		#region StringCollection

		public StringCollection ScreenScrape()
		{
			var scrapedText = new StringCollection();
			return scrapedText;
		}

		#endregion

		#region Write

		public void Write(byte[] data, int offset, int length)
		{
			var sReceived = Encoding.ASCII.GetString(data, offset, length);
			Invoke(RxdTextEvent, String.Copy(sReceived));
			Invoke(RefreshEvent);
		}

		#endregion

		#region Connect

		public void Connect()
		{
			switch (ConnectionType)
			{
				case ConnectionTypes.Telnet:
				{
					_cType = "Telnet";
					ConnectTelnet(Hostname, Port);
					break;
				}
				case ConnectionTypes.COM:
				{
					_cType = "Com";
					ConnectCom();
					break;
				}
				case ConnectionTypes.SSH1:
				{
					break;
				}
				case ConnectionTypes.SSH2:
				{
					_cType = "SSH";
					ConnectSsh2(Hostname, Username, Password, Port);
					break;
				}
				default:
				{
					break;
				}
			}
		}

		#endregion

		#region Startforward

		public void Startforward()
		{
			var ln = Convert.ToInt32(LastNumber);
			ln = ln + 1;
			var nb = ("LR " + ln + "-999999");
			DispatchMessage(this, nb);
			DispatchMessage(this, Environment.NewLine);

			/*	 KRR
            if (_myFiles.CheckSt("ToDownLoad") == true)
            {
                nb = "R " + _myFiles.Rxst("ToDownLoad");
                
                DispatchMessage(this, nb);
                DispatchMessage(this, Environment.NewLine);
                
            }
			 */
		}

		#endregion

		#region Disccocted by remote

		public void Disconnectby()
		{
			Invoke(RxdTextEvent, String.Copy("\u001B[31m DISCONNECTED !!! \u001B[0m"));
			Invoke(RxdTextEvent, String.Copy(Environment.NewLine));
			Invoke(RefreshEvent);
			if (Disconnected != null)
			{
				Disconnected(this, new EventArgs());
			}
		}

		#endregion

		#region Com Port

		private void ConnectCom()
		{
			try
			{
				#region case baud

				switch (BaudRateType)
				{
					case
						BaudRateTypes.Baud_110:
					{
						_port.BaudRate = 110;
						break;
					}
					case
						BaudRateTypes.Baud_300:
					{
						_port.BaudRate = 300;
						break;
					}
					case
						BaudRateTypes.Baud_600:
					{
						_port.BaudRate = 600;
						break;
					}
					case
						BaudRateTypes.Baud_1200:
					{
						_port.BaudRate = 1200;
						break;
					}
					case
						BaudRateTypes.Baud_2400:
					{
						_port.BaudRate = 2400;
						break;
					}
					case
						BaudRateTypes.Baud_4800:
					{
						_port.BaudRate = 4800;
						break;
					}
					case
						BaudRateTypes.Baud_9600:
					{
						_port.BaudRate = 9600;
						break;
					}
					case
						BaudRateTypes.Baud_19200:
					{
						_port.BaudRate = 19200;
						break;
					}
					case
						BaudRateTypes.Baud_38400:
					{
						_port.BaudRate = 38400;
						break;
					}

					case
						BaudRateTypes.Baud_57600:
					{
						_port.BaudRate = 57600;
						break;
					}
				}

				#endregion

				#region case stop

				switch (StopBitsType)
				{
					case
						StopBitsTypes.None:
					{
						_port.StopBits = StopBits.None;
						break;
					}
					case
						StopBitsTypes.One:
					{
						_port.StopBits = StopBits.One;
						break;
					}
					case
						StopBitsTypes.OnePointFive:
					{
						_port.StopBits = StopBits.OnePointFive;
						break;
					}
					case
						StopBitsTypes.Two:
					{
						_port.StopBits = StopBits.Two;
						break;
					}
				}

				#endregion

				#region case databits

				switch (DataBitsType)
				{
					case
						DataBitsTypes.Data_Bits_5:
					{
						_port.DataBits = 5;
						break;
					}
					case
						DataBitsTypes.Data_Bits_6:
					{
						_port.DataBits = 6;
						break;
					}
					case
						DataBitsTypes.Data_Bits_7:
					{
						_port.DataBits = 7;
						break;
					}
					case
						DataBitsTypes.Data_Bits_8:
					{
						_port.DataBits = 8;
						break;
					}
				}

				#endregion

				#region  case flow

				switch (FlowType)
				{
					case
						FlowTypes.XOnXOff:
					{
						_port.Handshake = Handshake.XOnXOff;
						break;
					}
					case
						FlowTypes.RequestToSend:
					{
						_port.Handshake = Handshake.RequestToSend;
						break;
					}
					case
						FlowTypes.RequestToSendXOnXOff:
					{
						_port.Handshake = Handshake.RequestToSendXOnXOff;
						break;
					}
					case
						FlowTypes.None:
					{
						_port.Handshake = Handshake.None;
						break;
					}
				}

				#endregion

				#region case parity

				switch (ParityType)
				{
					case
						ParityTypes.None:
					{
						_port.Parity = Parity.None;
						break;
					}
					case
						ParityTypes.Odd:
					{
						_port.Parity = Parity.Odd;
						break;
					}
					case
						ParityTypes.Even:
					{
						_port.Parity = Parity.Even;
						break;
					}
					case
						ParityTypes.Mark:
					{
						_port.Parity = Parity.Mark;
						break;
					}
					case
						ParityTypes.Space:
					{
						_port.Parity = Parity.Space;
						break;
					}
				}

				#endregion

				_port.PortName = SerialPort;
				_port.Open();
				_port.DataReceived += port_DataReceived;
				//this.Focus();  
			}
			catch (IOException e)
			{
				MessageBox.Show(Convert.ToString(e));
			}
		}

		#endregion

		#region ReadStreamSSH

		private void ReadStreamSsh(object sender, ShellDataEventArgs e)
		{
			try
			{
				_inputData = Encoding.ASCII.GetString(e.Data);
				if (_inputData != String.Empty && _inputData != null)
				{
					Invoke(RxdTextEvent, String.Copy(_inputData));
					if (_inputData == "\r")
					{
						_inputData = Environment.NewLine;
					}
				}
				Invoke(RefreshEvent);
			}
			catch (Exception curException)
			{
				MessageBox.Show("Error ReadStreamSSH: " + curException.Message);
			}
		}

		#endregion

		#region port_DataReceived

		private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			try
			{
				_inputData = _port.ReadExisting();
				if (_inputData != String.Empty)
				{
					//_parser.ParseString(_inputData);
					//this.BeginInvoke(new SetTextCallback(SetText), new object[] { InputData });
					Invoke(RxdTextEvent, String.Copy(_inputData));
					if (_inputData == "/r")
					{
						_inputData = Environment.NewLine;
					}
					Invoke(RefreshEvent);
					if (FileActive)
					{
						_dataFile = _dataFile + _inputData;

						var lines = _dataFile.Split(new[] {Environment.NewLine},
							StringSplitOptions.RemoveEmptyEntries);

						if (_dataFile.Contains(BBSPrompt))
							//if (Regex.Match(sReceived,BBSPrompt).Success )
						{
							ForwardDone(this, new EventArgs());
							FileActive = false;

							for (var i = 1; i < lines.Length - 1; i++)
								//for (int i = lines.Length -2 ; i >= 1 ; i-- )  
							{
								_myFiles.WriteSqlPacket(lines[i]);
								//LastNumber = lines[i].Substring(0, 5);
							}
							LastNumberevt(this, new EventArgs());
							_dataFile = "";
						}
					}
				}
				// throw new NotImplementedException();
			}
			catch
			{
				MessageBox.Show("Error Com");
			}
		}

		#endregion

		#region Connect Telnet

		private void ConnectTelnet(string hostName, Int32 port)
		{
			Focus();
			var ipHost = Dns.GetHostEntry(hostName);
			var addr = ipHost.AddressList;
			try
			{
				// Create New Socket 
				_curSocket = new Socket(
					AddressFamily.InterNetwork, SocketType.Stream,
					ProtocolType.Tcp);

				// Create New EndPoint
				var iep = new IPEndPoint(addr[0], port);

				// This is a non blocking IO
				_curSocket.Blocking = false;

				// Begin Asyncronous Connection
				_curSocket.BeginConnect(iep, ConnectCallback, _curSocket);
			}
			catch (Exception curException)
			{
				MessageBox.Show("Connect: " + curException.Message);
			}
		}

		#endregion

		#region mnuCopy

		private void mnuCopy_Click(object sender, EventArgs e)
		{
			var start = new Point();
			var stop = new Point();
			var foundStart = false;
			var foundStop = false;

			// find coordinates of Highlighted Text
			for (var row = 0; row < _rows; row++)
			{
				for (var col = 0; col < _cols; col++)
				{
					if (foundStart == false && _attribGrid[row][col].IsInverse)
					{
						start.X = col;
						start.Y = row;
						foundStart = true;
					}

					// this next check will first find the first non-inverse coord with a
					// character in it. If it happens to be at the beginning of a line, 
					// then we'll back up and stop at the last char in the prev line
					if (foundStart &&
					    foundStop == false &&
					    _attribGrid[row][col].IsInverse == false &&
					    _charGrid[row][col] != '\0'
						)
					{
						stop.X = col - 1;
						stop.Y = row;
						foundStop = true;
						// here we back up if col == 0
						if (col == 0)
						{
							// make sure we don't have a null row
							// this shouldn't throw
							row--;
							while (_charGrid[row][0] == '\0')
								row--;
							for (col = 0; col < _cols; col++) // parse the row
							{
								if (_charGrid[row][col] == '\0') // we found the end
								{
									stop.X = col - 1;
									stop.Y = row;
								}
							}
						}
						break;
					}

					if (foundStop && foundStart)
						break;
				} // column parse
				// if we get to this point without finding a match, and we're on the last row
				// we should include the last row and stop here
				if (foundStart && foundStop == false && row == _rows - 1)
				{
					for (var col = 0; col < _cols; col++) // parse the row
					{
						if (_charGrid[row][col] == '\0') // we found the end
						{
							stop.X = col - 1;
							stop.Y = row;
						}
					}
				}
			} // row parse
			Console.WriteLine("start.Y " + Convert.ToString(start.Y) +
			                  " start.X " + Convert.ToString(start.X) +
			                  " stop.Y " + Convert.ToString(stop.Y) +
			                  " stop.X " + Convert.ToString(stop.X));

			var sc = ScreenScrape();
			foreach (var s in sc)
			{
				Console.WriteLine(s);
			}
		}

		#endregion

		#region mnuPaste

		private void mnuPaste_Click(object sender, EventArgs e)
		{
		}

		#endregion

		#region mnuCopyPaste

		private void mnuCopyPaste_Click(object sender, EventArgs e)
		{
		}

		#endregion

		#region HandleScroll

		private void HandleScroll(int se)
		{
			try
			{
				if (_caret.IsOff)
				{
				}
				else
				{
					_textAtCursor = "";
					for (var x = 0; x < _cols; x++)
					{
						var curChar = _charGrid[_caret.Pos.Y][x];
						if (curChar == '\0')
						{
							continue;
						}
						_textAtCursor = _textAtCursor + Convert.ToString(curChar);
					}
				}


				switch (se)
				{
					case 0: // page up
						if (_lastVisibleLine > -(_scrollbackBuffer.Count - _rows))
						{
							if (_vertScrollBar.Value == _vertScrollBar.Maximum)
							{
								_vertScrollBar.Value = _vertScrollBar.Value - _rows;
							}

							_vertScrollBar.Value -= 1;
							_vertScrollBar.Refresh();
							_lastVisibleLine -= 1;
						}
						else
						{
							_vertScrollBar.Value = 0;
						}
						break;
					case 1: // page down
						if (_vertScrollBar.Value < (_vertScrollBar.Maximum))
						{
							_vertScrollBar.Value += 1;
							_lastVisibleLine += 1;
						}
						else
						{
							_vertScrollBar.Value = _vertScrollBar.Maximum;
						}
						break;


					case 2: // up
						if (_lastVisibleLine > -(_scrollbackBuffer.Count - _rows))
						{
							if (_vertScrollBar.Value == _vertScrollBar.Maximum)
							{
								_vertScrollBar.Value = _vertScrollBar.Value - _rows;
							}
							var i = 0;
							while (i <= _rows)
							{
								i++;
								if (_lastVisibleLine > -(_scrollbackBuffer.Count - _rows))
								{
									_lastVisibleLine += -1;
									_vertScrollBar.Value += -1;
								}
							}
							if (_lastVisibleLine > -(_scrollbackBuffer.Count - _rows))
							{
							}
							else
							{
								_vertScrollBar.Value = 0;
							}
						}
						else
						{
							_vertScrollBar.Value = 0;
						}
						break;

					case 3: // down
						_lastVisibleLine += _rows;
						_vertScrollBar.Value += _rows;
						break;
					case -7864320:
						if (_vertScrollBar.Value < (_vertScrollBar.Maximum))
						{
							if (_vertScrollBar.Value + _rows > (_vertScrollBar.Maximum))
							{
								_vertScrollBar.Value = _vertScrollBar.Maximum;
							}
							else
							{
								_lastVisibleLine += _rows;
								_vertScrollBar.Value += _rows;
							}
						}
						else
						{
							_vertScrollBar.Value = _vertScrollBar.Maximum;
						}
						break;

					case 7864320:
						if (_lastVisibleLine > -(_scrollbackBuffer.Count - _rows))
						{
							if (_vertScrollBar.Value == _vertScrollBar.Maximum)
							{
								_vertScrollBar.Value = _vertScrollBar.Value - _rows;
							}
							var i = 0;
							while (i <= _rows)
							{
								i++;
								if (_lastVisibleLine > -(_scrollbackBuffer.Count - _rows))
								{
									_lastVisibleLine += -1;
									_vertScrollBar.Value += -1;
								}
							}
							if (!(_lastVisibleLine > -(_scrollbackBuffer.Count - _rows)))
							{
								_vertScrollBar.Value = 0;
							}
						}
						else
						{
							_vertScrollBar.Value = 0;
						}
						break;

					default:
						return;
				}
				if (_lastVisibleLine > 0)
				{
					_lastVisibleLine = 0;
				}

				if (_lastVisibleLine < (0 - _scrollbackBuffer.Count) + (_rows))
				{
					if (_rows - 1 <= _lastVisibleLine)
					{
						_lastVisibleLine = (0 - _scrollbackBuffer.Count) + (_rows) - 1;
					}
				}

				var columns = _cols;
				var rows = _rows;
				SetSize(rows, columns);

				var visiblebuffer = new StringCollection();
				for (var i = _scrollbackBuffer.Count - 1 + _lastVisibleLine; i >= 0; i--)
				{
					visiblebuffer.Insert(0, _scrollbackBuffer[i]);
					if (visiblebuffer.Count >= rows - 1) // rows -1 to leave line for cursor space
						break;
				}

				for (var i = 0; i < visiblebuffer.Count; i++)
				{
					for (var column = 0; column < columns; column++)
					{
						if (column > visiblebuffer[i].Length - 1)
							continue;
						_charGrid[i][column] = visiblebuffer[i].ToCharArray()[column];
					}

					if (_lastVisibleLine == 0)
					{
						CaretOn();
						for (var column = 0; column < _cols; column++)
						{
							if (column > _textAtCursor.Length - 1)
								continue;
							_charGrid[_rows - 1][column] = _textAtCursor.ToCharArray()[column];
						}
						CaretToAbs(_rows - 1, _textAtCursor.Length);
					}
					else
					{
						CaretOff();
					}
				}

				Refresh();
			}

			catch (Exception curException)
			{
				MessageBox.Show("Error HandleScroll: " + curException.Message);
			}
		}

		#endregion

		#region SetScrollBarValues

		private void SetScrollBarValues()
		{
			try
			{
				_vertScrollBar.Minimum = 0;
				// if the scrollbackbuffer is empty, there's nothing to scroll
				if (_scrollbackBuffer.Count <= _rows)
				{
					_vertScrollBar.Maximum = 0;
					return;
				}
				_vertScrollBar.Maximum = _scrollbackBuffer.Count + 1;
				_vertScrollBar.Value = _scrollbackBuffer.Count + 1;

				_vertScrollBar.LargeChange = _rows;
				_vertScrollBar.SmallChange = _vertScrollBar.Maximum/_charSize.Height;
			}
			catch (Exception curException)
			{
				MessageBox.Show("Error SetScrollBarValues: " + curException.Message);
			}
		}

		#endregion

		#region ConnectCallback

		private void ConnectCallback(IAsyncResult ar)
		{
			try
			{
				// Get The connection socket from the callback
				var sock1 = (Socket) ar.AsyncState;
				if (_cType == "Com")
				{
					Refresh();
				}

				if (sock1.Connected)
				{
					var stateObject = new UcCommsStateObject();
					stateObject.Socket = sock1;
					// Assign Callback function to read from Asyncronous Socket
					_callbackProc = OnReceivedData;
					// Begin reading data asyncronously
					sock1.BeginReceive(stateObject.Buffer, 0, stateObject.Buffer.Length,
						SocketFlags.None, _callbackProc, stateObject);
				}
			}
			catch (Exception curException)
			{
				MessageBox.Show(curException.Message);
			}
		}

		#endregion

		#region OnReceiveData

		private void OnReceivedData(IAsyncResult ar)
		{
			try
			{
				// Get The connection socket from the callback
				var stateObject = (UcCommsStateObject) ar.AsyncState;

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

						var lines = _dataFile.Split(new[] {Environment.NewLine},
							StringSplitOptions.RemoveEmptyEntries);

						if (sReceived.Contains(BBSPrompt))

						{
							ForwardDone(this, new EventArgs());
							FileActive = false;

							for (var i = 1; i < lines.Length - 1;)

							{
								_myFiles.WriteSqlPacket(lines[i]);
								LastNumber = lines[i].Substring(0, 5);
								if (lines[i + 1].Contains(BBSPrompt))
								{
									i = lines.Length;
								}
								else
								{
									i++;
								}
							}
							LastNumberevt(this, new EventArgs());
							_dataFile = "";
						}
					}
					Invoke(RxdTextEvent, String.Copy(sReceived));
					Invoke(RefreshEvent);
					// Re-Establish the next asyncronous receveived data callback as
					stateObject.Socket.BeginReceive(stateObject.Buffer, 0, stateObject.Buffer.Length,
						SocketFlags.None, OnReceivedData, stateObject);
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
		}

		#endregion

		#region  DispatchMessage

		private void DispatchMessage(Object sender, string strText)
		{
			if (_xoff)
			{
				// store the characters in the outputbuffer
				_outBuff += strText;
				return;
			}
			var i = 0;
			try
			{
				var smk = new Byte[strText.Length];

				if (_outBuff != "")
				{
					strText = _outBuff + strText;
					_outBuff = "";
				}

				for (i = 0; i < strText.Length; i++)
				{
					var ss = Convert.ToByte(strText[i]);
					smk[i] = ss;
				}

				if (_callbackEndDispatch == null)
				{
					_callbackEndDispatch = EndDispatchMessage;
				}

				if (_cType == "Com")
				{
					_port.Write(strText);
				}

				else if (_cType == "SSH")
				{
					strText = strText.Replace("\r\n", "\n");
					_stream.Write(strText);
				}
				else
				{
					if (_curSocket == null)
					{
						try
						{
							//  _reader.Pf.Transmit(smk);
						}
						catch
						{
							MessageBox.Show("error communicating with socket");
						}
					}
					else
					{
						try
						{
							_curSocket.BeginSend(
								smk,
								0,
								smk.Length,
								SocketFlags.None,
								_callbackEndDispatch,
								_curSocket);
						}
						catch
						{
							MessageBox.Show("error communicating with socket");
						}
					}
				}
			}
			catch (Exception curException)
			{
				MessageBox.Show("DispatchMessage: " + curException.Message);
				MessageBox.Show("DispatchMessage: Character is " + Convert.ToInt32(strText[i]));
				MessageBox.Show("DispatchMessage: String is " + strText);
			}
		}

		#endregion

		#region EndDispatchMessage

		private void EndDispatchMessage(IAsyncResult ar)
		{
			try
			{
				var sock = (Socket) ar.AsyncState;
				sock.EndSend(ar);
			}
			catch (Exception curException)
			{
				MessageBox.Show("EndDispatchMessage: " + curException.Message);
			}
		}

		#endregion

		#region PrintChar

		private void PrintChar(Char curChar)
		{
			if (_caret.EOL)
			{
				if ((_modes.Flags & UcMode.AutoWrap) == UcMode.AutoWrap)
				{
					LineFeed();
					CarriageReturn();
					_caret.EOL = false;
				}
			}

			var x = _caret.Pos.X;
			var y = _caret.Pos.Y;

			_attribGrid[y][x] = _charAttribs;

			if (_charAttribs.Gs != null)
			{
				curChar = UcChars.Get(curChar, _attribGrid[y][x].Gs.Set, _attribGrid[y][x].GR.Set);
				if (_charAttribs.Gs.Set == UcChars.Sets.DECSG) _attribGrid[y][x].IsDECSG = true;
				_charAttribs.Gs = null;
			}
			else
			{
				curChar = UcChars.Get(curChar, _attribGrid[y][x].Gl.Set, _attribGrid[y][x].GR.Set);
				if (_charAttribs.Gl.Set == UcChars.Sets.DECSG) _attribGrid[y][x].IsDECSG = true;
			}
			_charGrid[y][x] = curChar;
			CaretRight();
		}

		#endregion

		#region System.Drawing.Point GetDrawStringOffset

		private Point GetDrawStringOffset(Graphics curGraphics, Int32 x,
			Int32 y, Char curChar)
		{
			// DrawString doesn't actually print where you tell it to but instead consistently prints
			// with an offset. This is annoying when the other draw commands do not print with an offset
			// this method returns a point defining the offset so we can take it off the printstring command.

			CharacterRange[] characterRanges =
			{
				new CharacterRange(0, 1)
			};

			var layoutRect = new RectangleF(x, y, 100, 100);

			var stringFormat = new StringFormat();

			stringFormat.SetMeasurableCharacterRanges(characterRanges);

			Region[] stringRegions;

			stringRegions = curGraphics.MeasureCharacterRanges(
				curChar.ToString(CultureInfo.InvariantCulture),
				Font,
				layoutRect,
				stringFormat);

			var measureRect1 = stringRegions[0].GetBounds(curGraphics);

			return new Point((int) (measureRect1.X + 0.5), (int) (measureRect1.Y + 0.5));
		}

		#endregion

		#region System.Drawing.Point GetCharSize

		private Point GetCharSize(Graphics curGraphics)
		{
			// DrawString doesn't actually print where you tell it to but instead consistently prints
			// with an offset. This is annoying when the other draw commands do not print with an offset
			// this method returns a point defining the offset so we can take it off the printstring command.

			CharacterRange[] characterRanges =
			{
				new CharacterRange(0, 1)
			};

			var layoutRect = new RectangleF(0, 0, 100, 100);

			var stringFormat = new StringFormat();

			stringFormat.SetMeasurableCharacterRanges(characterRanges);

			var stringRegions = curGraphics.MeasureCharacterRanges(
				"A",
				Font,
				layoutRect,
				stringFormat);

			var measureRect1 = stringRegions[0].GetBounds(curGraphics);

			return new Point((int) (measureRect1.Width + 0.5), (int) (measureRect1.Height + 0.5));
		}

		#endregion

		#region AssignColors

		private void AssignColors(CharAttribStruct curAttribs, ref Color curFgColor, ref Color curBGColor)
		{
			curFgColor = ForeColor;
			curBGColor = BackColor;

			if (curAttribs.IsBlinking)
			{
				curFgColor = _blinkColor;
			}

			// bold takes precedence over the blink color
			if (curAttribs.IsBold)
			{
				curFgColor = _boldColor;
			}

			if (curAttribs.UseAltColor)
			{
				curFgColor = curAttribs.AltColor;
			}

			// alternate color takes precedence over the bold color
			if (curAttribs.UseAltBGColor)
			{
				curBGColor = curAttribs.AltBgColor;
			}

			if (curAttribs.IsInverse)
			{
				var tmpColor = curBGColor;

				curBGColor = curFgColor;
				curFgColor = tmpColor;
			}

			// If light background is on and we're not using alt colors
			// reverse the colors
			if ((_modes.Flags & UcMode.LightBackground) > 0 &&
			    curAttribs.UseAltColor == false && curAttribs.UseAltBGColor == false)
			{
				var tmpColor = curBGColor;

				curBGColor = curFgColor;
				curFgColor = tmpColor;
			}
		}

		#endregion

		#region ShowChar

		private void ShowChar(Graphics curGraphics, Char curChar, Int32 y, Int32 x,
			CharAttribStruct curAttribs)
		{
			if (curChar == '\0')
			{
				return;
			}

			var curFgColor = Color.White;
			var curBgColor = Color.Black;

			AssignColors(curAttribs, ref curFgColor, ref curBgColor);

			if ((curBgColor != BackColor && (_modes.Flags & UcMode.LightBackground) == 0) ||
			    (curBgColor != _fgColor && (_modes.Flags & UcMode.LightBackground) > 0))
			{
				// Erase the current Character underneath the cursor postion
				_eraseBuffer.Clear(curBgColor);

				// paint a rectangle over the cursor position in the character's BGColor
				curGraphics.DrawImageUnscaled(
					_eraseBitmap,
					x,
					y);
			}

			if (curAttribs.IsUnderscored)
			{
				curGraphics.DrawLine(new Pen(curFgColor, 1),
					x, y + _underlinePos,
					x + _charSize.Width, y + _underlinePos);
			}

			if (curAttribs.IsDECSG &&
			    (curChar == 'l' ||
			     curChar == 'q' ||
			     curChar == 'w' ||
			     curChar == 'k' ||
			     curChar == 'x' ||
			     curChar == 't' ||
			     curChar == 'n' ||
			     curChar == 'u' ||
			     curChar == 'm' ||
			     curChar == 'v' ||
			     curChar == 'j' ||
			     curChar == '`'))
			{
				ShowSpecialChar(
					curGraphics,
					curChar,
					y,
					x,
					curFgColor);

				return;
			}

			curGraphics.DrawString(
				curChar.ToString(CultureInfo.InvariantCulture),
				Font,
				new SolidBrush(curFgColor),
				x - _drawStringOffset.X,
				y - _drawStringOffset.Y);
		}

		#endregion

		#region SSH Connect

		private void ConnectSsh2(string hostname, string username, string password, Int32 port)
		{
			string ip;
			try
			{
				var ipHost = Dns.GetHostEntry(hostname);
				var addr = ipHost.AddressList;
				ip = addr[0].ToString();
			}
			catch
			{
				MessageBox.Show("Unable to resolve HostName");
				return;
			}

			_client = new SshClient(ip, port, username, password);
			_client.Connect();
			_stream = _client.CreateShellStream("xterm", 80, 24, 800, 600, 1024);
			_stream.DataReceived += ReadStreamSsh;
			Focus();
		}

		#endregion

		#region ShowSpecialChar

		private void ShowSpecialChar(Graphics curGraphics, Char curChar, Int32 y,
			Int32 x, Color curFgColor)
		{
			if (curChar == '\0')
			{
				return;
			}

			var curPoints = new Point[4];
			switch (curChar)
			{
				case '`': // diamond

					curPoints[0] = new Point(x + _charSize.Width/2, y + _charSize.Height/6);
					curPoints[1] = new Point(x + 5*_charSize.Width/6, y + _charSize.Height/2);
					curPoints[2] = new Point(x + _charSize.Width/2, y + 5*_charSize.Height/6);
					curPoints[3] = new Point(x + _charSize.Width/6, y + _charSize.Height/2);

					curGraphics.FillPolygon(
						new SolidBrush(curFgColor),
						curPoints);
					break;

				case 'l': // top left bracket
					curGraphics.DrawLine(new Pen(curFgColor, 1),
						x + _charSize.Width/2 - 1, y + _charSize.Height/2,
						x + _charSize.Width, y + _charSize.Height/2);
					curGraphics.DrawLine(new Pen(curFgColor, 1),
						x + _charSize.Width/2, y + _charSize.Height/2,
						x + _charSize.Width/2, y + _charSize.Height);
					break;

				case 'q': // horizontal line
					curGraphics.DrawLine(new Pen(curFgColor, 1),
						x, y + _charSize.Height/2,
						x + _charSize.Width, y + _charSize.Height/2);
					break;

				case 'w': // top tee-piece
					curGraphics.DrawLine(new Pen(curFgColor, 1),
						x, y + _charSize.Height/2,
						x + _charSize.Width, y + _charSize.Height/2);
					curGraphics.DrawLine(new Pen(curFgColor, 1),
						x + _charSize.Width/2, y + _charSize.Height/2,
						x + _charSize.Width/2, y + _charSize.Height);
					break;

				case 'k': // top right bracket
					curGraphics.DrawLine(new Pen(curFgColor, 1),
						x, y + _charSize.Height/2,
						x + _charSize.Width/2, y + _charSize.Height/2);
					curGraphics.DrawLine(new Pen(curFgColor, 1),
						x + _charSize.Width/2, y + _charSize.Height/2,
						x + _charSize.Width/2, y + _charSize.Height);
					break;

				case 'x': // vertical line
					curGraphics.DrawLine(new Pen(curFgColor, 1),
						x + _charSize.Width/2, y,
						x + _charSize.Width/2, y + _charSize.Height);
					break;

				case 't': // left hand tee-piece
					curGraphics.DrawLine(new Pen(curFgColor, 1),
						x + _charSize.Width/2, y,
						x + _charSize.Width/2, y + _charSize.Height);
					curGraphics.DrawLine(new Pen(curFgColor, 1),
						x + _charSize.Width/2, y + _charSize.Height/2,
						x + _charSize.Width, y + _charSize.Height/2);
					break;

				case 'n': // cross piece
					curGraphics.DrawLine(new Pen(curFgColor, 1),
						x + _charSize.Width/2, y,
						x + _charSize.Width/2, y + _charSize.Height);
					curGraphics.DrawLine(new Pen(curFgColor, 1),
						x, y + _charSize.Height/2,
						x + _charSize.Width, y + _charSize.Height/2);
					break;

				case 'u': // right hand tee-piece
					curGraphics.DrawLine(new Pen(curFgColor, 1),
						x, y + _charSize.Height/2,
						x + _charSize.Width/2, y + _charSize.Height/2);
					curGraphics.DrawLine(new Pen(curFgColor, 1),
						x + _charSize.Width/2, y,
						x + _charSize.Width/2, y + _charSize.Height);
					break;

				case 'm': // bottom left bracket
					curGraphics.DrawLine(new Pen(curFgColor, 1),
						x + _charSize.Width/2, y + _charSize.Height/2,
						x + _charSize.Width, y + _charSize.Height/2);
					curGraphics.DrawLine(new Pen(curFgColor, 1),
						x + _charSize.Width/2, y,
						x + _charSize.Width/2, y + _charSize.Height/2);
					break;

				case 'v': // bottom tee-piece
					curGraphics.DrawLine(new Pen(curFgColor, 1),
						x, y + _charSize.Height/2,
						x + _charSize.Width, y + _charSize.Height/2);
					curGraphics.DrawLine(new Pen(curFgColor, 1),
						x + _charSize.Width/2, y,
						x + _charSize.Width/2, y + _charSize.Height/2);
					break;

				case 'j': // bottom right bracket
					curGraphics.DrawLine(new Pen(curFgColor, 1),
						x, y + _charSize.Height/2,
						x + _charSize.Width/2, y + _charSize.Height/2);
					curGraphics.DrawLine(new Pen(curFgColor, 1),
						x + _charSize.Width/2, y,
						x + _charSize.Width/2, y + _charSize.Height/2);
					break;
			}
		}

		#endregion

		#region WipeScreen

		private void WipeScreen(Graphics curGraphics)
		{
			// clear the screen buffer area
			if ((_modes.Flags & UcMode.LightBackground) > 0)
			{
				curGraphics.Clear(_fgColor);
			}
			else
			{
				curGraphics.Clear(BackColor);
			}
		}

		#endregion

		#region ClearDown

		private void ClearDown(Int32 param)
		{
			var x = _caret.Pos.X;
			var y = _caret.Pos.Y;

			switch (param)
			{
				case 0: // from cursor to bottom inclusive

					Array.Clear(_charGrid[y], x, _charGrid[y].Length - x);
					Array.Clear(_attribGrid[y], x, _attribGrid[y].Length - x);

					for (var i = y + 1; i < _rows; i++)
					{
						Array.Clear(_charGrid[i], 0, _charGrid[i].Length);
						Array.Clear(_attribGrid[i], 0, _attribGrid[i].Length);
					}
					break;

				case 1: // from top to cursor inclusive

					Array.Clear(_charGrid[y], 0, x + 1);
					Array.Clear(_attribGrid[y], 0, x + 1);

					for (var i = 0; i < y; i++)
					{
						Array.Clear(_charGrid[i], 0, _charGrid[i].Length);
						Array.Clear(_attribGrid[i], 0, _attribGrid[i].Length);
					}
					break;

				case 2: // entire screen

					for (var i = 0; i < _rows; i++)
					{
						Array.Clear(_charGrid[i], 0, _charGrid[i].Length);
						Array.Clear(_attribGrid[i], 0, _attribGrid[i].Length);
					}
					break;
			}
		}

		#endregion

		#region ClearRight

		private void ClearRight(Int32 param)
		{
			var x = _caret.Pos.X;
			var y = _caret.Pos.Y;

			switch (param)
			{
				case 0: // from cursor to end of line inclusive

					Array.Clear(_charGrid[y], x, _charGrid[y].Length - x);
					Array.Clear(_attribGrid[y], x, _attribGrid[y].Length - x);
					break;

				case 1: // from beginning to cursor inclusive
					Array.Clear(_charGrid[y], 0, x + 1);
					Array.Clear(_attribGrid[y], 0, x + 1);
					break;

				case 2: // entire line
					Array.Clear(_charGrid[y], 0, _charGrid[y].Length);
					Array.Clear(_attribGrid[y], 0, _attribGrid[y].Length);
					break;
			}
		}

		#endregion

		#region ShowBuffer

		private void ShowBuffer()
		{
			Invalidate();
		}

		#endregion

		#region Redraw

		private void Redraw(Graphics curGraphics)
		{
			Char CurChar;


			// refresh the screen
			for (var y = 0; y < _rows; y++)
			{
				for (var x = 0; x < _cols; x++)
				{
					CurChar = _charGrid[y][x];

					if (CurChar == '\0')
					{
						continue;
					}

					var CurPoint = new Point(
						x*_charSize.Width,
						y*_charSize.Height);

					ShowChar(
						curGraphics,
						CurChar,
						CurPoint.Y,
						CurPoint.X,
						_attribGrid[y][x]);
				}
			}
		}

		#endregion

		#region NvtSendWill

		private void NvtSendWill(Char curChar)
		{
			DispatchMessage(this, string.Format("\xFF\xFB{0}", curChar));
		}

		#endregion

		#region NvtSendWont

		private void NvtSendWont(Char curChar)
		{
			DispatchMessage(this, string.Format("\xFF\xFC{0}", curChar));
		}

		#endregion

		#region NvtSendDont

		private void NvtSendDont(Char curChar)
		{
			DispatchMessage(this, string.Format("\xFF\xFE{0}", curChar));
		}

		#endregion

		#region NvtSendDo

		private void NvtSendDo(Char curChar)
		{
			DispatchMessage(this, string.Format("\xFF\xFD{0}", curChar));
		}

		#endregion

		#region NvtSendSubNeg

		private void NvtSendSubNeg(Char curChar, String curString)
		{
			DispatchMessage(this, string.Format("\xFF\xFA{0}\x00{1}\xFF\xF0", curChar, curString));
		}

		#endregion

		#region NvtExecuteChar

		private void NvtExecuteChar()
		{
		}

		#endregion

		#region TelnetInterpreter

		private void TelnetInterpreter(object sender, NvtParserEventArgs e)
		{
			switch (e.Action)
			{
				case NvtActions.SendUp:
					_parser.ParseString(e.CurChar.ToString(CultureInfo.InvariantCulture));
					break;
				case NvtActions.Execute:
					NvtExecuteChar();
					break;
			}

			// if the sequence is a DO message
			if (e.CurSequence.StartsWith("\xFD"))
			{
				var curCmd = Convert.ToChar(e.CurSequence.Substring(1, 1));

				switch (curCmd)
				{
					// 24 - terminal type 
					case '\x18':
						NvtSendWill(curCmd);
						break;

					default:
						NvtSendWont(curCmd);
						//System.Console.Write ("unsupported telnet DO sequence {0} happened\n", 
						//System.Convert.ToInt32 (System.Convert.ToChar (e.CurSequence.Substring (1,1))));
						break;
				}
			}

			// if the sequence is a WILL message
			if (e.CurSequence.StartsWith("\xFB"))
			{
				var curCmd = Convert.ToChar(e.CurSequence.Substring(1, 1));

				switch (curCmd)
				{
					case '\x01': // echo
						NvtSendDo(curCmd);
						break;

					default:
						NvtSendDont(curCmd);
						//System.Console.Write ("unsupported telnet WILL sequence {0} happened\n", 
						//System.Convert.ToInt32 (System.Convert.ToChar (e.CurSequence.Substring (1,1))));
						break;
				}
			}

			// if the sequence is a SUBNEGOTIATE message
			if (e.CurSequence.StartsWith("\xFA"))
			{
				if (e.CurSequence[2] != '\x01')
				{
					// not interested in data from host just yet as we don't ask for it at the moment
					return;
				}

				var curCmd = Convert.ToChar(e.CurSequence.Substring(1, 1));

				switch (curCmd)
				{
					// 24 - terminal type 
					case '\x18':
						NvtSendSubNeg(curCmd, "vt220");
						break;

					default:
						NvtSendSubNeg(curCmd, "");
						Console.Write("unsupported telnet SUBNEG sequence {0} happened\n",
							Convert.ToInt32(Convert.ToChar(e.CurSequence.Substring(1, 1))));
						break;
				}
			}
		}

		#endregion

		#region CarriageReturn

		private void CarriageReturn()
		{
			CaretToAbs(_caret.Pos.Y, 0);
		}

		#endregion

		#region Tab

		private void Tab()
		{
			for (var i = 0; i < _tabStops.Columns.Length; i++)
			{
				if (i > _caret.Pos.X && _tabStops.Columns[i])
				{
					CaretToAbs(_caret.Pos.Y, i);
					return;
				}
			}

			CaretToAbs(_caret.Pos.Y, _cols - 1);
		}

		#endregion

		#region TabSet

		private void TabSet()
		{
			_tabStops.Columns[_caret.Pos.X] = true;
		}

		#endregion

		#region ClearTabs

		private void ClearTabs(UcParams curParams) // TBC 
		{
			var param = 0;

			if (curParams.Count() > 0)
			{
				param = Convert.ToInt32(curParams.Elements[0]);
			}

			switch (param)
			{
				case 0: // Current Position
					_tabStops.Columns[_caret.Pos.X] = false;
					break;

				case 3: // All Tabs
					for (var i = 0; i < _tabStops.Columns.Length; i++)
					{
						_tabStops.Columns[i] = false;
					}
					break;
			}
		}

		#endregion

		#region ReverseLineFeed

		private void ReverseLineFeed()
		{
			// if we're at the top of the scroll region (top margin)
			if (_caret.Pos.Y == _topMargin)
			{
				// we need to add a new line at the top of the screen margin
				// so shift all the rows in the scroll region down in the array and
				// insert a new row at the top

				int i;

				for (i = _bottomMargin; i > _topMargin; i--)
				{
					_charGrid[i] = _charGrid[i - 1];
					_attribGrid[i] = _attribGrid[i - 1];
				}
				_charGrid[_topMargin] = new Char[_cols];
				_attribGrid[_topMargin] = new CharAttribStruct[_cols];
			}

			CaretUp();
		}

		#endregion

		#region Insertline

		private void InsertLine(UcParams curParams)
		{
			// if we're not in the scroll region then bail
			if (_caret.Pos.Y < _topMargin ||
			    _caret.Pos.Y > _bottomMargin)
			{
				return;
			}

			var nbrOff = 1;

			if (curParams.Count() > 0)
			{
				nbrOff = Convert.ToInt32(curParams.Elements[0]);
			}

			while (nbrOff > 0)
			{
				// Shift all the rows from the current row to the bottom margin down one place
				for (var i = _bottomMargin; i > _caret.Pos.Y; i--)
				{
					_charGrid[i] = _charGrid[i - 1];
					_attribGrid[i] = _attribGrid[i - 1];
				}


				_charGrid[_caret.Pos.Y] = new Char[_cols];
				_attribGrid[_caret.Pos.Y] = new CharAttribStruct[_cols];

				nbrOff--;
			}
		}

		#endregion

		#region DeleteLine

		private void DeleteLine(UcParams curParams)
		{
			// if we're not in the scroll region then bail
			if (_caret.Pos.Y < _topMargin ||
			    _caret.Pos.Y > _bottomMargin)
			{
				return;
			}

			var nbrOff = 1;

			if (curParams.Count() > 0)
			{
				nbrOff = Convert.ToInt32(curParams.Elements[0]);
			}

			while (nbrOff > 0)
			{
				// Shift all the rows from below the current row to the bottom margin up one place
				for (var i = _caret.Pos.Y; i < _bottomMargin; i++)
				{
					_charGrid[i] = _charGrid[i + 1];
					_attribGrid[i] = _attribGrid[i + 1];
				}

				_charGrid[_bottomMargin] = new Char[_cols];
				_attribGrid[_bottomMargin] = new CharAttribStruct[_cols];

				nbrOff--;
			}
		}

		#endregion

		#region LineFeed

		private void LineFeed()
		{
			SetScrollBarValues();

			// capture the new line into the scrollback buffer
			if (_scrollbackBuffer.Count < _scrollbackBufferSize)
			{
			}
			else
			{
				_scrollbackBuffer.RemoveAt(0);
			}

			var s = "";
			for (var x = 0; x < _cols; x++)
			{
				var curChar = _charGrid[_caret.Pos.Y][x];

				if (curChar == '\0')
				{
					continue;
				}
				s = s + Convert.ToString(curChar);
			}
			_scrollbackBuffer.Add(s);

			if (_caret.Pos.Y == _bottomMargin || _caret.Pos.Y == _rows - 1)
			{
				// we need to add a new line so shift all the rows up in the array and
				// insert a new row at the bottom
				int i;
				for (i = _topMargin; i < _bottomMargin; i++)
				{
					_charGrid[i] = _charGrid[i + 1];
					_attribGrid[i] = _attribGrid[i + 1];
				}
				_charGrid[i] = new Char[_cols];
				_attribGrid[i] = new CharAttribStruct[_cols];
			}
			CaretDown();
		}

		#endregion

		#region Index

		private void Index(Int32 param)
		{
			if (param == 0) param = 1;

			for (var i = 0; i < param; i++)
			{
				LineFeed();
			}
		}

		#endregion

		#region ReverseIndex

		private void ReverseIndex(Int32 param)
		{
			if (param == 0) param = 1;

			for (var i = 0; i < param; i++)
			{
				ReverseLineFeed();
			}
		}

		#endregion

		#region CaretOff

		private void CaretOff()
		{
			if (_caret.IsOff)
			{
				return;
			}

			_caret.IsOff = true;
		}

		#endregion

		#region CaretOn

		private void CaretOn()
		{
			if (_caret.IsOff == false)
			{
				return;
			}

			_caret.IsOff = false;
		}

		#endregion

		#region ShowCaret

		private void ShowCaret(Graphics curGraphics)
		{
			var x = _caret.Pos.X;
			var y = _caret.Pos.Y;

			if (_caret.IsOff)
			{
				return;
			}

			// paint a rectangle over the cursor position
			curGraphics.DrawImageUnscaled(
				_caret.Bitmap,
				x*_charSize.Width,
				y*_charSize.Height);

			// if we don't have a char to redraw then leave
			if (_charGrid[y][x] == '\0')
			{
				return;
			}

			var curAttribs = new CharAttribStruct();

			curAttribs.UseAltColor = true;

			curAttribs.Gl = _attribGrid[y][x].Gl;
			curAttribs.GR = _attribGrid[y][x].GR;
			curAttribs.Gs = _attribGrid[y][x].Gs;

			if (_attribGrid[y][x].UseAltBGColor == false)
			{
				curAttribs.AltColor = BackColor;
			}
			else if (_attribGrid[y][x].UseAltBGColor)
			{
				curAttribs.AltColor = _attribGrid[y][x].AltBgColor;
			}

			curAttribs.IsUnderscored = _attribGrid[y][x].IsUnderscored;
			curAttribs.IsDECSG = _attribGrid[y][x].IsDECSG;

			// redispay the current char in the background colour
			ShowChar(
				curGraphics,
				_charGrid[y][x],
				_caret.Pos.Y*_charSize.Height,
				_caret.Pos.X*_charSize.Width,
				curAttribs);
		}

		#endregion

		#region CaretUp

		private void CaretUp()
		{
			_caret.EOL = false;

			if ((_caret.Pos.Y > 0 && (_modes.Flags & UcMode.OriginRelative) == 0) ||
			    (_caret.Pos.Y > _topMargin && (_modes.Flags & UcMode.OriginRelative) > 0))
			{
				_caret.Pos.Y -= 1;
			}
		}

		#endregion

		#region CaretDown

		private void CaretDown()
		{
			_caret.EOL = false;

			if ((_caret.Pos.Y < _rows - 1 && (_modes.Flags & UcMode.OriginRelative) == 0) ||
			    (_caret.Pos.Y < _bottomMargin && (_modes.Flags & UcMode.OriginRelative) > 0))
			{
				_caret.Pos.Y += 1;
			}
		}

		#endregion

		#region CaretLeft

		private void CaretLeft()
		{
			_caret.EOL = false;

			if (_caret.Pos.X > 0)
			{
				_caret.Pos.X -= 1;
			}
		}

		#endregion

		#region CaretRight

		private void CaretRight()
		{
			if (_caret.Pos.X < _cols - 1)
			{
				_caret.Pos.X += 1;
				_caret.EOL = false;
			}
			else
			{
				_caret.EOL = true;
			}
		}

		#endregion

		#region CaretToRel

		private void CaretToRel(Int32 y, Int32 x)
		{
			_caret.EOL = false;
			/* This code is used when we get a cursor position command from
                   the host. Even if we're not in relative mode we use this as this will
                   sort that out for us. The ToAbs code is used internally by this prog 
                   but is smart enough to stay within the margins if the originrelative 
                   flagis set. */

			if ((_modes.Flags & UcMode.OriginRelative) == 0)
			{
				CaretToAbs(y, x);
				return;
			}

			/* the origin mode is relative so add the top and left margin
                   to Y and X respectively */
			y += _topMargin;

			if (x < 0)
			{
				x = 0;
			}

			if (x > _cols - 1)
			{
				x = _cols - 1;
			}

			if (y < _topMargin)
			{
				y = _topMargin;
			}

			if (y > _bottomMargin)
			{
				y = _bottomMargin;
			}

			_caret.Pos.Y = y;
			_caret.Pos.X = x;
		}

		#endregion

		#region CaretToAbs

		private void CaretToAbs(Int32 y, Int32 x)
		{
			_caret.EOL = false;

			if (x < 0)
			{
				x = 0;
			}

			if (x > _cols - 1)
			{
				x = _cols - 1;
			}

			if (y < 0 && (_modes.Flags & UcMode.OriginRelative) == 0)
			{
				y = 0;
			}

			if (y < _topMargin && (_modes.Flags & UcMode.OriginRelative) > 0)
			{
				y = _topMargin;
			}

			if (y > _rows - 1 && (_modes.Flags & UcMode.OriginRelative) == 0)
			{
				y = _rows - 1;
			}

			if (y > _bottomMargin && (_modes.Flags & UcMode.OriginRelative) > 0)
			{
				y = _bottomMargin;
			}

			_caret.Pos.Y = y;
			_caret.Pos.X = x;
		}

		#endregion

		#region CommandRouter

		private void CommandRouter(object sender, ParserEventArgs e)
		{
			switch (e.Action)
			{
				case Actions.Print:
					PrintChar(e.CurChar);
					//System.Console.Write ("{0}", e.CurChar);
					break;

				case Actions.Execute:
					ExecuteChar(e.CurChar);
					break;

				case Actions.Dispatch:
					break;
			}

			var param = 0;

			var inc = 1; // increment

			switch (e.CurSequence)
			{
				case "":
					break;

				case "\x1b" + "7": //DECSC Save Cursor position and attributes
					_savedCarets.Add(new UcCaretAttribs(
						_caret.Pos,
						_g0.Set,
						_g1.Set,
						_g2.Set,
						_g3.Set,
						_charAttribs));

					break;

				case "\x1b" + "8": //DECRC Restore Cursor position and attributes
					_caret.Pos = ((UcCaretAttribs) _savedCarets[_savedCarets.Count - 1]).Pos;
					_charAttribs = ((UcCaretAttribs) _savedCarets[_savedCarets.Count - 1]).Attribs;

					_g0.Set = ((UcCaretAttribs) _savedCarets[_savedCarets.Count - 1]).G0Set;
					_g1.Set = ((UcCaretAttribs) _savedCarets[_savedCarets.Count - 1]).G1Set;
					_g2.Set = ((UcCaretAttribs) _savedCarets[_savedCarets.Count - 1]).G2Set;
					_g3.Set = ((UcCaretAttribs) _savedCarets[_savedCarets.Count - 1]).G3Set;

					_savedCarets.RemoveAt(_savedCarets.Count - 1);

					break;

				case "\x1b~": //LS1R Locking Shift G1 -> GR
					_charAttribs.GR = _g1;
					break;

				case "\x1bn": //LS2 Locking Shift G2 -> GL
					_charAttribs.Gl = _g2;
					break;

				case "\x1b}": //LS2R Locking Shift G2 -> GR
					_charAttribs.GR = _g2;
					break;

				case "\x1bo": //LS3 Locking Shift G3 -> GL
					_charAttribs.Gl = _g3;
					break;

				case "\x1b|": //LS3R Locking Shift G3 -> GR
					_charAttribs.GR = _g3;
					break;

				case "\x1b#8": //DECALN
					e.CurParams.Elements.Add("1");
					e.CurParams.Elements.Add(_rows.ToString(CultureInfo.InvariantCulture));
					SetScrollRegion(e.CurParams);

					// put E's on the entire screen
					for (var y = 0; y < _rows; y++)
					{
						CaretToAbs(y, 0);

						for (var x = 0; x < _cols; x++)
						{
							PrintChar('E');
						}
					}
					break;

				case "\x1b=": // Keypad to Application mode
					_modes.Flags = _modes.Flags | UcMode.KeypadAppln;
					break;

				case "\x1b>": // Keypad to Numeric mode
					_modes.Flags = _modes.Flags ^ UcMode.KeypadAppln;
					break;

				case "\x1b[B": // CUD

					if (e.CurParams.Count() > 0)
					{
						inc = Convert.ToInt32(e.CurParams.Elements[0]);
					}

					if (inc == 0) inc = 1;

					CaretToAbs(_caret.Pos.Y + inc, _caret.Pos.X);
					break;

				case "\x1b[A": // CUU

					if (e.CurParams.Count() > 0)
					{
						inc = Convert.ToInt32(e.CurParams.Elements[0]);
					}

					if (inc == 0) inc = 1;

					CaretToAbs(_caret.Pos.Y - inc, _caret.Pos.X);
					break;

				case "\x1b[C": // CUF

					if (e.CurParams.Count() > 0)
					{
						inc = Convert.ToInt32(e.CurParams.Elements[0]);
					}

					if (inc == 0) inc = 1;

					CaretToAbs(_caret.Pos.Y, _caret.Pos.X + inc);
					break;

				case "\x1b[D": // CUB

					if (e.CurParams.Count() > 0)
					{
						inc = Convert.ToInt32(e.CurParams.Elements[0]);
					}

					if (inc == 0) inc = 1;

					CaretToAbs(_caret.Pos.Y, _caret.Pos.X - inc);
					break;

				case "\x1b[H": // CUP
				case "\x1b[f": // HVP

					var X = 0;
					var Y = 0;

					if (e.CurParams.Count() > 0)
					{
						Y = Convert.ToInt32(e.CurParams.Elements[0]) - 1;
					}

					if (e.CurParams.Count() > 1)
					{
						X = Convert.ToInt32(e.CurParams.Elements[1]) - 1;
					}

					CaretToRel(Y, X);
					break;

				case "\x1b[J":

					if (e.CurParams.Count() > 0)
					{
						param = Convert.ToInt32(e.CurParams.Elements[0]);
					}

					ClearDown(param);
					break;

				case "\x1b[K":

					if (e.CurParams.Count() > 0)
					{
						param = Convert.ToInt32(e.CurParams.Elements[0]);
					}

					ClearRight(param);
					break;

				case "\x1b[L": // INSERT LINE
					InsertLine(e.CurParams);
					break;

				case "\x1b[M": // DELETE LINE
					DeleteLine(e.CurParams);
					break;

				case "\x1bN": // SS2 Single Shift (G2 -> GL)
					_charAttribs.Gs = _g2;
					break;

				case "\x1bO": // SS3 Single Shift (G3 -> GL)
					_charAttribs.Gs = _g3;
					//System.Console.WriteLine ("SS3: GS = {0}", this.CharAttribs.GS);
					break;

				case "\x1b[m":
					SetCharAttribs(e.CurParams);
					break;

				case "\x1b[?h":
					SetqmhMode(e.CurParams);
					break;

				case "\x1b[?l":
					SetqmlMode(e.CurParams);
					break;

				case "\x1b[c": // DA Device Attributes
					//                    this.DispatchMessage (this, "\x1b[?64;1;2;6;7;8;9c");
					DispatchMessage(this, "\x1b[?6c");
					break;

				case "\x1b[g":
					ClearTabs(e.CurParams);
					break;

				case "\x1b[h":
					SethMode(e.CurParams);
					break;

				case "\x1b[l":
					SetlMode(e.CurParams);
					break;

				case "\x1b[r": // DECSTBM Set Top and Bottom Margins
					SetScrollRegion(e.CurParams);
					break;

				case "\x1b[t": // DECSLPP Set Lines Per Page

					if (e.CurParams.Count() > 0)
					{
						param = Convert.ToInt32(e.CurParams.Elements[0]);
					}

					if (param > 0) SetSize(param, _cols);

					break;

				case "\x1b" + "D": // IND

					if (e.CurParams.Count() > 0)
					{
						param = Convert.ToInt32(e.CurParams.Elements[0]);
					}

					Index(param);
					break;

				case "\x1b" + "E": // NEL
					LineFeed();
					CarriageReturn();
					break;

				case "\x1bH": // HTS
					TabSet();
					break;

				case "\x1bM": // RI
					if (e.CurParams.Count() > 0)
					{
						param = Convert.ToInt32(e.CurParams.Elements[0]);
					}

					ReverseIndex(param);
					break;

				//System.Console.Write ("unsupported VT sequence {0} happened\n", e.CurSequence);
			}

			if (e.CurSequence.StartsWith("\x1b("))
			{
				SelectCharSet(ref _g0.Set, e.CurSequence.Substring(2));
			}
			else if (e.CurSequence.StartsWith("\x1b-") ||
			         e.CurSequence.StartsWith("\x1b)"))
			{
				SelectCharSet(ref _g1.Set, e.CurSequence.Substring(2));
			}
			else if (e.CurSequence.StartsWith("\x1b.") ||
			         e.CurSequence.StartsWith("\x1b*"))
			{
				SelectCharSet(ref _g2.Set, e.CurSequence.Substring(2));
			}
			else if (e.CurSequence.StartsWith("\x1b/") ||
			         e.CurSequence.StartsWith("\x1b+"))
			{
				SelectCharSet(ref _g3.Set, e.CurSequence.Substring(2));
			}
		}

		#endregion

		#region SelectCharSet

		private void SelectCharSet(ref UcChars.Sets curTarget, String curString)
		{
			switch (curString)
			{
				case "B":
					curTarget = UcChars.Sets.ASCII;
					break;

				case "%5":
					curTarget = UcChars.Sets.DECS;
					break;

				case "0":
					curTarget = UcChars.Sets.DECSG;
					break;

				case ">":
					curTarget = UcChars.Sets.DECTECH;
					break;

				case "<":
					curTarget = UcChars.Sets.DECSG;
					break;

				case "A":
					if ((_modes.Flags & UcMode.National) == 0)
					{
						curTarget = UcChars.Sets.ISOLatin1S;
					}
					else
					{
						curTarget = UcChars.Sets.NRCUK;
					}
					break;

				case "4":
					//                    CurTarget = uc_Chars.Sets.NRCDutch;
					break;

				case "5":
					curTarget = UcChars.Sets.NRCFinnish;
					break;

				case "R":
					curTarget = UcChars.Sets.NRCFrench;
					break;

				case "9":
					curTarget = UcChars.Sets.NRCFrenchCanadian;
					break;

				case "K":
					curTarget = UcChars.Sets.NRCGerman;
					break;

				case "Y":
					curTarget = UcChars.Sets.NRCItalian;
					break;

				case "6":
					curTarget = UcChars.Sets.NRCNorDanish;
					break;

				case "'":
					curTarget = UcChars.Sets.NRCNorDanish;
					break;

				case "%6":
					curTarget = UcChars.Sets.NRCPortuguese;
					break;

				case "Z":
					curTarget = UcChars.Sets.NRCSpanish;
					break;

				case "7":
					curTarget = UcChars.Sets.NRCSwedish;
					break;

				case "=":
					curTarget = UcChars.Sets.NRCSwiss;
					break;
			}
		}

		#endregion

		#region SetqmhMode

		private void SetqmhMode(UcParams curParams) // set mode for ESC?h command
		{
			var optInt = 0;

			foreach (String curOption in curParams.Elements)
			{
				try
				{
					optInt = Convert.ToInt32(curOption);
				}
				catch (Exception curException)
				{
					MessageBox.Show(curException.Message);
				}

				switch (optInt)
				{
					case 1: // set cursor keys to application mode
						_modes.Flags = _modes.Flags | UcMode.CursorAppln;
						break;

					case 2: // lock the keyboard
						_modes.Flags = _modes.Flags | UcMode.Locked;
						break;

					case 3: // set terminal to 132 column mode
						SetSize(_rows, 132);
						break;

					case 5: // Light Background Mode
						_modes.Flags = _modes.Flags | UcMode.LightBackground;
						RefreshEvent();
						break;

					case 6: // Origin Mode Relative
						_modes.Flags = _modes.Flags | UcMode.OriginRelative;
						CaretToRel(0, 0);
						break;

					case 7: // Autowrap On
						_modes.Flags = _modes.Flags | UcMode.AutoWrap;
						break;

					case 8: // AutoRepeat On
						_modes.Flags = _modes.Flags | UcMode.Repeat;
						break;

					case 42: // DECNRCM Multinational Charset
						_modes.Flags = _modes.Flags | UcMode.National;
						break;

					case 66: // Numeric Keypad Application Mode On
						_modes.Flags = _modes.Flags | UcMode.KeypadAppln;
						break;
				}
			}
		}

		#endregion

		#region SetqmlMode

		private void SetqmlMode(UcParams curParams) // set mode for ESC?l command
		{
			var optInt = 0;

			foreach (String curOption in curParams.Elements)
			{
				try
				{
					optInt = Convert.ToInt32(curOption);
				}
				catch (Exception curException)
				{
					MessageBox.Show(curException.Message);
				}

				switch (optInt)
				{
					case 1: // set cursor keys to normal cursor mode
						_modes.Flags = _modes.Flags & ~UcMode.CursorAppln;
						break;

					case 2: // unlock the keyboard
						_modes.Flags = _modes.Flags & ~UcMode.Locked;
						break;

					case 3: // set terminal to 80 column mode
						SetSize(_rows, 80);
						break;

					case 5: // Dark Background Mode
						_modes.Flags = _modes.Flags & ~UcMode.LightBackground;
						RefreshEvent();
						break;

					case 6: // Origin Mode Absolute
						_modes.Flags = _modes.Flags & ~UcMode.OriginRelative;
						CaretToAbs(0, 0);
						break;

					case 7: // Autowrap Off
						_modes.Flags = _modes.Flags & ~UcMode.AutoWrap;
						break;

					case 8: // AutoRepeat Off
						_modes.Flags = _modes.Flags & ~UcMode.Repeat;
						break;

					case 42: // DECNRCM National Charset
						_modes.Flags = _modes.Flags & ~UcMode.National;
						break;

					case 66: // Numeric Keypad Application Mode On
						_modes.Flags = _modes.Flags & ~UcMode.KeypadAppln;
						break;
				}
			}
		}

		#endregion

		#region SethMode

		private void SethMode(UcParams curParams) // set mode for ESC?h command
		{
			var optInt = 0;

			foreach (String curOption in curParams.Elements)
			{
				try
				{
					optInt = Convert.ToInt32(curOption);
				}
				catch (Exception curException)
				{
					MessageBox.Show(curException.Message);
				}

				switch (optInt)
				{
					case 1: // set local echo off
						_modes.Flags = _modes.Flags | UcMode.LocalEchoOff;
						break;
				}
			}
		}

		#endregion

		#region SetlMode

		private void SetlMode(UcParams curParams) // set mode for ESC?l command
		{
			var optInt = 0;

			foreach (String curOption in curParams.Elements)
			{
				try
				{
					optInt = Convert.ToInt32(curOption);
				}
				catch (Exception curException)
				{
					MessageBox.Show(curException.Message);
				}

				switch (optInt)
				{
					case 1: // set LocalEcho on
						_modes.Flags = _modes.Flags & ~UcMode.LocalEchoOff;
						break;
				}
			}
		}

		#endregion

		#region SetScrollRegion

		private void SetScrollRegion(UcParams curParams)
		{
			if (curParams.Count() > 0)
			{
				_topMargin = Convert.ToInt32(curParams.Elements[0]) - 1;
			}

			if (curParams.Count() > 1)
			{
				_bottomMargin = Convert.ToInt32(curParams.Elements[1]) - 1;
			}

			if (_bottomMargin == 0)
			{
				_bottomMargin = _rows - 1;
			}

			if (_topMargin < 0)
			{
				_bottomMargin = 0;
			}
		}

		#endregion

		#region ClearCharAttribs

		private void ClearCharAttribs()
		{
			_charAttribs.IsBold = false;
			_charAttribs.IsDim = false;
			_charAttribs.IsUnderscored = false;
			_charAttribs.IsBlinking = false;
			_charAttribs.IsInverse = false;
			_charAttribs.IsPrimaryFont = false;
			_charAttribs.IsAlternateFont = false;
			_charAttribs.UseAltBGColor = false;
			_charAttribs.UseAltColor = false;
			_charAttribs.AltColor = Color.White;
			_charAttribs.AltBgColor = Color.Black;
		}

		#endregion

		#region SetCharAttribs

		private void SetCharAttribs(UcParams curParams)
		{
			if (curParams.Count() < 1)
			{
				ClearCharAttribs();
				return;
			}

			for (var i = 0; i < curParams.Count(); i++)
			{
				switch (Convert.ToInt32(curParams.Elements[i]))
				{
					case 0:
						ClearCharAttribs();
						break;

					case 1:
						_charAttribs.IsBold = true;
						break;

					case 4:
						_charAttribs.IsUnderscored = true;
						break;

					case 5:
						_charAttribs.IsBlinking = true;
						break;

					case 7:
						_charAttribs.IsInverse = true;
						break;

					case 22:
						_charAttribs.IsBold = false;
						break;

					case 24:
						_charAttribs.IsUnderscored = false;
						break;

					case 25:
						_charAttribs.IsBlinking = false;
						break;

					case 27:
						_charAttribs.IsInverse = false;
						break;

					case 30:
						_charAttribs.UseAltColor = true;
						_charAttribs.AltColor = Color.Black;
						break;

					case 31:
						_charAttribs.UseAltColor = true;
						_charAttribs.AltColor = Color.Red;
						break;

					case 32:
						_charAttribs.UseAltColor = true;
						_charAttribs.AltColor = Color.Green;
						break;

					case 33:
						_charAttribs.UseAltColor = true;
						_charAttribs.AltColor = Color.Yellow;
						break;

					case 34:
						_charAttribs.UseAltColor = true;
						_charAttribs.AltColor = Color.Blue;
						break;

					case 35:
						_charAttribs.UseAltColor = true;
						_charAttribs.AltColor = Color.Magenta;
						break;

					case 36:
						_charAttribs.UseAltColor = true;
						_charAttribs.AltColor = Color.Cyan;
						break;

					case 37:
						_charAttribs.UseAltColor = true;
						_charAttribs.AltColor = Color.White;
						break;

					case 40:
						_charAttribs.UseAltBGColor = true;
						_charAttribs.AltBgColor = Color.Black;
						break;

					case 41:
						_charAttribs.UseAltBGColor = true;
						_charAttribs.AltBgColor = Color.Red;
						break;

					case 42:
						_charAttribs.UseAltBGColor = true;
						_charAttribs.AltBgColor = Color.Green;
						break;

					case 43:
						_charAttribs.UseAltBGColor = true;
						_charAttribs.AltBgColor = Color.Yellow;
						break;

					case 44:
						_charAttribs.UseAltBGColor = true;
						_charAttribs.AltBgColor = Color.Blue;
						break;

					case 45:
						_charAttribs.UseAltBGColor = true;
						_charAttribs.AltBgColor = Color.Magenta;
						break;

					case 46:
						_charAttribs.UseAltBGColor = true;
						_charAttribs.AltBgColor = Color.Cyan;
						break;

					case 47:
						_charAttribs.UseAltBGColor = true;
						_charAttribs.AltBgColor = Color.White;
						break;
				}
			}
		}

		#endregion

		#region ExecuteChar

		private void ExecuteChar(Char curChar)
		{
			switch (curChar)
			{
				case '\x05': // ENQ request for the answerback message
					DispatchMessage(this, "vt220");
					break;

				case '\x07': // BEL ring my bell
					// this.BELL;
					if (Beep)
					{
						if (_count == 0)
						{
							// Beep at 5000 Hz for 1 second
							Console.Beep(2000, 500);
							_count = 1;
						}
						else
						{
							_count = 0;
						}
					}
					break;

				case '\x08': // BS back space
					CaretLeft();
					break;

				case '\x09': // HT Horizontal Tab
					Tab();
					break;

				case '\x0A': // LF  LineFeed
				case '\x0B': // VT  VerticalTab
				case '\x0C': // FF  FormFeed
				case '\x84': // IND Index
					LineFeed();
					break;

				case '\x0D': // CR CarriageReturn
					CarriageReturn();
					break;

				case '\x0E': // SO maps G1 into GL
					_charAttribs.Gl = _g1;
					break;

				case '\x0F': // SI maps G0 into GL
					_charAttribs.Gl = _g0;
					break;

				case '\x11': // DC1/XON continue sending characters
					_xoff = false;
					DispatchMessage(this, "");
					break;

				case '\x13': // DC3/XOFF stop sending characters
					_xoff = true;
					break;

				case '\x85': // NEL Next line (same as line feed and carriage return)
					LineFeed();
					CaretToAbs(_caret.Pos.Y, 0);
					break;

				case '\x88': // HTS Horizontal tab set 
					TabSet();
					break;

				case '\x8D': // RI Reverse Index 
					ReverseLineFeed();
					break;

				case '\x8E': // SS2 Single Shift (G2 -> GL)
					_charAttribs.Gs = _g2;
					break;

				case '\x8F': // SS3 Single Shift (G3 -> GL)
					_charAttribs.Gs = _g3;
					break;
			}
		}

		#endregion

		#region SetSize

		private void SetSize(Int32 rows, Int32 columns)
		{
			_rows = rows;
			_cols = columns;
			_topMargin = 0;
			_bottomMargin = rows - 1;

			// create the character grid (rows by columns) this is a shadow of what's displayed
			_charGrid = new Char[_rows][];

			for (var i = 0; i < _charGrid.Length; i++)
			{
				_charGrid[i] = new Char[_cols];
			}
			_attribGrid = new CharAttribStruct[_rows][];

			for (var i = 0; i < _attribGrid.Length; i++)
			{
				_attribGrid[i] = new CharAttribStruct[_cols];
			}
		}

		#endregion

		#region GetFontInfo

		private void GetFontInfo()
		{
			var tmpGraphics = CreateGraphics();

			// get the offset that the moron Graphics.Drawstring method adds by default
			_drawStringOffset = GetDrawStringOffset(tmpGraphics, 0, 0, 'A');

			// get the size of the character using the same type of method
			var tmpPoint = GetCharSize(tmpGraphics);

			_charSize.Width = tmpPoint.X; // make a little breathing room
			_charSize.Height = tmpPoint.Y;

			tmpGraphics.Dispose();
			_underlinePos = _charSize.Height - 2;
			_caret.Bitmap = new Bitmap(_charSize.Width, _charSize.Height);
			_caret.Buffer = Graphics.FromImage(_caret.Bitmap);
			_caret.Buffer.Clear(Color.FromArgb(255, 181, 106));
			_eraseBitmap = new Bitmap(_charSize.Width, _charSize.Height);
			_eraseBuffer = Graphics.FromImage(_eraseBitmap);
		}

		#endregion

		#region Disconnect

		private void Disconnect()
		{
			try
			{
				if (_cType == "Com")
				{
					_port.Close();
				}
				else if (_cType == "SSH")
				{
					Invoke(RxdTextEvent, String.Copy("\u001B[31m DISCONNECTED !!! \u001B[0m"));
					Invoke(RxdTextEvent, String.Copy(Environment.NewLine));
					Invoke(RefreshEvent);
					_stream.Close();
					_stream.Dispose();
				}
				else
				{
					Invoke(RxdTextEvent, String.Copy("\u001B[31m DISCONNECTED !!! \u001B[0m"));
					Invoke(RxdTextEvent, String.Copy(Environment.NewLine));
					Invoke(RefreshEvent);
					_curSocket.Shutdown(SocketShutdown.Both);
					_curSocket.Close();
				}
			}
			catch (Exception)
			{
			}
		}

		#endregion

		#region class uc_CommsStateObject

		private class UcCommsStateObject
		{
			public Socket Socket;
			public readonly Byte[] Buffer;

			#region UcCommsStateObject

			public UcCommsStateObject()
			{
				Buffer = new Byte[8192];
			}

			#endregion
		}

		#endregion

		#region class uc_TabStops

		private class UcTabStops
		{
			public readonly Boolean[] Columns;

			#region UcTabStops

			public UcTabStops()
			{
				Columns = new Boolean[256];
				Columns[8] = true;
				Columns[16] = true;
				Columns[24] = true;
				Columns[32] = true;
				Columns[40] = true;
				Columns[48] = true;
				Columns[56] = true;
				Columns[64] = true;
				Columns[72] = true;
				Columns[80] = true;
				Columns[88] = true;
				Columns[96] = true;
				Columns[104] = true;
				Columns[112] = true;
				Columns[120] = true;
				Columns[128] = true;
			}

			#endregion
		}

		#endregion

		#region class uc_CaretAttribs

		private class UcCaretAttribs
		{
			public readonly CharAttribStruct Attribs;
			public readonly UcChars.Sets G0Set;
			public readonly UcChars.Sets G1Set;
			public readonly UcChars.Sets G2Set;
			public readonly UcChars.Sets G3Set;
			public readonly Point Pos;

			#region UcCaretAttribs

			public UcCaretAttribs(Point p1, UcChars.Sets p2, UcChars.Sets p3, UcChars.Sets p4, UcChars.Sets p5,
				CharAttribStruct p6)
			{
				Pos = p1;
				G0Set = p2;
				G1Set = p3;
				G2Set = p4;
				G3Set = p5;
				Attribs = p6;
			}

			#endregion
		}

		#endregion

		#region calss uc_Chars

		private class UcChars
		{
			#region enum Sets

			public enum Sets
			{
				None,
				DECSG,
				DECTECH,
				DECS,
				ASCII,
				ISOLatin1S,
				NRCUK,
				NRCFinnish,
				NRCFrench,
				NRCFrenchCanadian,
				NRCGerman,
				NRCItalian,
				NRCNorDanish,
				NRCPortuguese,
				NRCSpanish,
				NRCSwedish,
				NRCSwiss
			}

			#endregion

			#region UcCharSet[] DECSG

			public static readonly UcCharSet[] DECSG =
			{
				new UcCharSet(0x5F, 0x0020), // Blank (I've used space here so you may want to change this)
				//            new uc_CharSet (0x60, 0x25C6), // Filled Diamond 
				new UcCharSet(0x61, 0x0000), // Pi over upsidedown Pi ?  
				new UcCharSet(0x62, 0x2409), // HT symbol 
				new UcCharSet(0x63, 0x240C), // LF Symbol  
				new UcCharSet(0x64, 0x240D), // CR Symbol  
				new UcCharSet(0x65, 0x240A), // LF Symbol  
				new UcCharSet(0x66, 0x00B0), // Degree  
				new UcCharSet(0x67, 0x00B1), // Plus over Minus  
				new UcCharSet(0x68, 0x2424), // NL Symbol  
				new UcCharSet(0x69, 0x240B), // VT Symbol 
				//            new uc_CharSet (0x6A, 0x2518), // Bottom Right Box 
				//            new uc_CharSet (0x6B, 0x2510), // Top Right Box
				//            new uc_CharSet (0x6C, 0x250C), // TopLeft Box
				//            new uc_CharSet (0x6D, 0x2514), // Bottom Left Box
				//            new uc_CharSet (0x6E, 0x253C), // Cross Piece
				new UcCharSet(0x6F, 0x23BA), // Scan Line 1
				new UcCharSet(0x70, 0x25BB), // Scan Line 3
				//            new uc_CharSet (0x71, 0x2500), // Horizontal Line (scan line 5 as well?)
				new UcCharSet(0x72, 0x23BC), // Scan Line 7 
				new UcCharSet(0x73, 0x23BD), // Scan Line 9 
				//            new uc_CharSet (0x74, 0x251C), // Left Tee Piece
				//            new uc_CharSet (0x75, 0x2524), // Right Tee Piece
				//            new uc_CharSet (0x76, 0x2534), // Bottom Tee Piece
				//            new uc_CharSet (0x77, 0x252C), // Top Tee Piece
				//            new uc_CharSet (0x78, 0x2502), // Vertical Line
				new UcCharSet(0x79, 0x2264), // Less than or equal  
				new UcCharSet(0x7A, 0x2265), // Greater than or equal 
				new UcCharSet(0x7B, 0x03A0), // Capital Pi
				new UcCharSet(0x7C, 0x2260), // Not Equal 
				new UcCharSet(0x7D, 0x00A3), // Pound Sign 
				new UcCharSet(0x7E, 0x00B7) // Middle Dot 
			};

			#endregion

			#region  UcCharSet[] DECS

			public static readonly UcCharSet[] DECS =
			{
				new UcCharSet(0xA8, 0x0020), // Currency Sign
				new UcCharSet(0xD7, 0x0152), // latin small ligature OE 
				new UcCharSet(0xDD, 0x0178), // Capital Y with diaeresis
				new UcCharSet(0xF7, 0x0153), // latin small ligature oe 
				new UcCharSet(0xFD, 0x00FF) // Lowercase y with diaeresis
			};

			#endregion

			#region UcCharSet[] ASCII

			public static readonly UcCharSet[] ASCII = // same as Basic Latin
			{
				new UcCharSet(0x00, 0x0000) //
			};

			#endregion

			#region UcCharSet[] NRCUK

			public static readonly UcCharSet[] NRCUK = // UK National Replacement
			{
				new UcCharSet(0x23, 0x00A3) //
			};

			#endregion

			#region  UcCharSet[] NRCFinnish

			public static readonly UcCharSet[] NRCFinnish = // Finnish National Replacement
			{
				new UcCharSet(0x5B, 0x00C4), // A with diearesis
				new UcCharSet(0x5C, 0x00D6), // O with diearesis
				new UcCharSet(0x5D, 0x00C5), // A with hollow dot above
				new UcCharSet(0x5E, 0x00DC), // U with diearesis
				new UcCharSet(0x60, 0x00E9), // e with accute accent
				new UcCharSet(0x7B, 0x00E4), // a with diearesis
				new UcCharSet(0x7C, 0x00F6), // o with diearesis
				new UcCharSet(0x7D, 0x00E5), // a with hollow dot above
				new UcCharSet(0x7E, 0x00FC) // u with diearesis
			};

			#endregion

			#region UcCharSet[] NRCFrench

			public static readonly UcCharSet[] NRCFrench = // French National Replacement
			{
				new UcCharSet(0x23, 0x00A3), // Pound Sign
				new UcCharSet(0x40, 0x00E0), // a with grav accent
				new UcCharSet(0x5B, 0x00B0), // Degree Symbol
				new UcCharSet(0x5C, 0x00E7), // little cedila
				new UcCharSet(0x5D, 0x00A7), // funny s (technical term)
				new UcCharSet(0x7B, 0x00E9), // e with accute accent
				new UcCharSet(0x7C, 0x00F9), // u with grav accent
				new UcCharSet(0x7D, 0x00E8), // e with grav accent
				new UcCharSet(0x7E, 0x00A8) // diearesis
			};

			#endregion

			#region UcCharSet[] NRCFrenchCanadian

			public static readonly UcCharSet[] NRCFrenchCanadian = // French Canadian National Replacement
			{
				new UcCharSet(0x40, 0x00E0), // a with grav accent
				new UcCharSet(0x5B, 0x00E2), // a with circumflex
				new UcCharSet(0x5C, 0x00E7), // little cedila
				new UcCharSet(0x5D, 0x00EA), // e with circumflex
				new UcCharSet(0x5E, 0x00CE), // i with circumflex
				new UcCharSet(0x60, 0x00F4), // o with circumflex
				new UcCharSet(0x7B, 0x00E9), // e with accute accent
				new UcCharSet(0x7C, 0x00F9), // u with grav accent
				new UcCharSet(0x7D, 0x00E8), // e with grav accent
				new UcCharSet(0x7E, 0x00FB) // u with circumflex
			};

			#endregion

			#region UcCharSet[] NRCGerman

			public static readonly UcCharSet[] NRCGerman = // German National Replacement
			{
				new UcCharSet(0x40, 0x00A7), // funny s
				new UcCharSet(0x5B, 0x00C4), // A with diearesis
				new UcCharSet(0x5C, 0x00D6), // O with diearesis
				new UcCharSet(0x5D, 0x00DC), // U with diearesis
				new UcCharSet(0x7B, 0x00E4), // a with diearesis
				new UcCharSet(0x7C, 0x00F6), // o with diearesis
				new UcCharSet(0x7D, 0x00FC), // u with diearesis
				new UcCharSet(0x7E, 0x00DF) // funny B
			};

			#endregion

			#region UcCharSet[] NRCItalian

			public static readonly UcCharSet[] NRCItalian = // Italian National Replacement
			{
				new UcCharSet(0x23, 0x00A3), // pound sign
				new UcCharSet(0x40, 0x00A7), // funny s
				new UcCharSet(0x5B, 0x00B0), // Degree Symbol
				new UcCharSet(0x5C, 0x00E7), // little cedilla
				new UcCharSet(0x5D, 0x00E9), // e with accute accent
				new UcCharSet(0x60, 0x00F9), // u with grav accent
				new UcCharSet(0x7B, 0x00E0), // a with grav accent
				new UcCharSet(0x7C, 0x00F2), // o with grav accent
				new UcCharSet(0x7D, 0x00E8), // e with grav accent
				new UcCharSet(0x7E, 0x00CC) // I with grav accent
			};

			#endregion

			#region UcCharSet[] NRCNorDanish

			public static readonly UcCharSet[] NRCNorDanish = // Norwegian Danish National Replacement
			{
				new UcCharSet(0x5B, 0x00C6), // AE ligature
				new UcCharSet(0x5C, 0x00D8), // O with strikethough
				new UcCharSet(0x5D, 0x00D8), // O with strikethough
				new UcCharSet(0x5D, 0x00C5), // A with hollow dot above
				new UcCharSet(0x7B, 0x00E6), // ae ligature
				new UcCharSet(0x7C, 0x00F8), // o with strikethough
				new UcCharSet(0x7D, 0x00F8), // o with strikethough
				new UcCharSet(0x7D, 0x00E5) // a with hollow dot above
			};

			#endregion

			#region UcCharSet[] NRCPortuguese

			public static readonly UcCharSet[] NRCPortuguese = // Portuguese National Replacement
			{
				new UcCharSet(0x5B, 0x00C3), // A with tilde
				new UcCharSet(0x5C, 0x00C7), // big cedilla
				new UcCharSet(0x5D, 0x00D5), // O with tilde
				new UcCharSet(0x7B, 0x00E3), // a with tilde
				new UcCharSet(0x7C, 0x00E7), // little cedilla
				new UcCharSet(0x7D, 0x00F6) // o with tilde
			};

			#endregion

			#region UcCharSet[] NRCSpanish

			public static readonly UcCharSet[] NRCSpanish = // Spanish National Replacement
			{
				new UcCharSet(0x23, 0x00A3), // pound sign
				new UcCharSet(0x40, 0x00A7), // funny s
				new UcCharSet(0x5B, 0x00A1), // I with dot
				new UcCharSet(0x5C, 0x00D1), // N with tilde
				new UcCharSet(0x5D, 0x00BF), // Upside down question mark
				new UcCharSet(0x7B, 0x0060), // back single quote
				new UcCharSet(0x7C, 0x00B0), // Degree Symbol
				new UcCharSet(0x7D, 0x00F1), // n with tilde
				new UcCharSet(0x7E, 0x00E7) // small cedilla
			};

			#endregion

			#region UcCharSet[] NRCSwedish

			public static readonly UcCharSet[] NRCSwedish = // Swedish National Replacement
			{
				new UcCharSet(0x40, 0x00C9), // E with acute
				new UcCharSet(0x5B, 0x00C4), // A with diearesis
				new UcCharSet(0x5C, 0x00D6), // O with diearesis
				new UcCharSet(0x5D, 0x00C5), // A with hollow dot above
				new UcCharSet(0x5E, 0x00DC), // U with diearesis
				new UcCharSet(0x60, 0x00E9), // e with accute accent
				new UcCharSet(0x7B, 0x00E4), // a with diearesis
				new UcCharSet(0x7C, 0x00F6), // o with diearesis
				new UcCharSet(0x7D, 0x00E5), // a with hollow dot above
				new UcCharSet(0x7E, 0x00FC) // u with diearesis
			};

			#endregion

			#region UcCharSet[] NRCSwiss

			public static readonly UcCharSet[] NRCSwiss = // Swiss National Replacement
			{
				new UcCharSet(0x23, 0x00F9), // u with grav accent
				new UcCharSet(0x40, 0x00E0), // a with grav accent
				new UcCharSet(0x5B, 0x00E9), // e with accute accent
				new UcCharSet(0x5C, 0x00E7), // small cedilla
				new UcCharSet(0x5D, 0x00EA), // e with circumflex
				new UcCharSet(0x5E, 0x00CE), // i with circumflex
				new UcCharSet(0x5F, 0x00E8), // e with grav accent
				new UcCharSet(0x60, 0x00F4), // o with circumflex
				new UcCharSet(0x7B, 0x00E4), // a with diearesis
				new UcCharSet(0x7C, 0x00F6), // o with diearesis
				new UcCharSet(0x7D, 0x00FC), // u with diearesis
				new UcCharSet(0x7E, 0x00FB) // u with circumflex
			};

			#endregion

			#region  UcCharSet

			public static readonly UcCharSet[] ISOLatin1S = // Same as Latin-1 Supplemental
			{
				new UcCharSet(0x00, 0x0000) //
			};

			#endregion

			public Sets Set;

			#region UcChars

			public UcChars(Sets p1)
			{
				Set = p1;
			}

			#endregion

			#region Char

			public static Char Get(Char curChar, Sets gl, Sets gr)
			{
				UcCharSet[] curSet;

				// I'm assuming the left hand in use table will always have a 00-7F char set in it
				if (Convert.ToInt32(curChar) < 128)
				{
					switch (gl)
					{
						case Sets.ASCII:
							curSet = ASCII;
							break;

						case Sets.DECSG:
							curSet = DECSG;
							break;

						case Sets.NRCUK:
							curSet = NRCUK;
							break;

						case Sets.NRCFinnish:
							curSet = NRCFinnish;
							break;

						case Sets.NRCFrench:
							curSet = NRCFrench;
							break;

						case Sets.NRCFrenchCanadian:
							curSet = NRCFrenchCanadian;
							break;

						case Sets.NRCGerman:
							curSet = NRCGerman;
							break;

						case Sets.NRCItalian:
							curSet = NRCItalian;
							break;

						case Sets.NRCNorDanish:
							curSet = NRCNorDanish;
							break;

						case Sets.NRCPortuguese:
							curSet = NRCPortuguese;
							break;

						case Sets.NRCSpanish:
							curSet = NRCSpanish;
							break;

						case Sets.NRCSwedish:
							curSet = NRCSwedish;
							break;

						case Sets.NRCSwiss:
							curSet = NRCSwiss;
							break;

						default:
							curSet = ASCII;
							break;
					}
				}
				// I'm assuming the right hand in use table will always have a 80-FF char set in it
				else
				{
					switch (gr)
					{
						case Sets.ISOLatin1S:
							curSet = ISOLatin1S;
							break;

						case Sets.DECS:
							curSet = DECS;
							break;

						default:
							curSet = DECS;
							break;
					}
				}

				for (var i = 0; i < curSet.Length; i++)
				{
					if (curSet[i].Location == Convert.ToInt32(curChar))
					{
						var curBytes = BitConverter.GetBytes(curSet[i].UnicodeNo);
						var newChars = Encoding.Unicode.GetChars(curBytes);

						return newChars[0];
					}
				}

				return curChar;
			}

			#endregion

			#region UcCharSet

			public struct UcCharSet
			{
				public readonly Int32 Location;
				public readonly Int16 UnicodeNo;

				#region UcCharSet

				public UcCharSet(Int32 p1, Int16 p2)
				{
					Location = p1;
					UnicodeNo = p2;
				}

				#endregion
			}

			#endregion
		}

		#endregion

		#region class uc_Caret

		private class UcCaret
		{
			public Bitmap Bitmap;
			public Graphics Buffer;
			public Boolean EOL;
			public Boolean IsOff;
			public Point Pos;

			#region UcCaret

			public UcCaret()
			{
				Pos = new Point(0, 0);
			}

			#endregion
		}

		#endregion

		#region class WMCodes

		private class WMCodes
		{
			public const int WM_KEYFIRST = 0x0100;
			public const int WM_KEYDOWN = 0x0100;
			public const int WM_KEYUP = 0x0101;
			public const int WM_CHAR = 0x0102;
			public const int WM_DEADCHAR = 0x0103;
			public const int WM_SYSKEYDOWN = 0x0104;
			public const int WM_SYSKEYUP = 0x0105;
			public const int WM_SYSCHAR = 0x0106;
			public const int WM_SYSDEADCHAR = 0x0107;
			public const int WM_KEYLAST = 0x0108;
			public const int MOUSEWHEEL = 0x020A;
			public const int OCM_VSCROLL = 0x0115;
		}

		#endregion

		#region class uc_Mode

		private class UcMode
		{
			public static readonly UInt32 Locked = 1; // Unlocked           = off 
			public static UInt32 BackSpace = 2; // Delete             = off 
			public static UInt32 NewLine = 4; // Line Feed          = off 
			public static readonly UInt32 Repeat = 8; // No Repeat          = off 
			public static readonly UInt32 AutoWrap = 16; // No AutoWrap        = off 
			public static readonly UInt32 CursorAppln = 32; // Std Cursor Codes   = off 
			public static readonly UInt32 KeypadAppln = 64; // Std Numeric Codes  = off 
			public static UInt32 DataProcessing = 128; // Typewriter         = off 
			public static UInt32 PositionReports = 256; // CharacterCodes     = off
			public static readonly UInt32 LocalEchoOff = 512; // LocalEchoOn        = off
			public static readonly UInt32 OriginRelative = 1024; // OriginAbsolute     = off
			public static readonly UInt32 LightBackground = 2048; // DarkBackground     = off
			public static readonly UInt32 National = 4096; // Multinational      = off
			public static readonly UInt32 Any = 0x80000000; // Any Flags
			public UInt32 Flags;

			#region UcMode

			public UcMode()
			{
				Flags = 0;
			}

			#endregion
		}

		#endregion

		#region class uc_Keyboard

		private class UcKeyboard
		{
			private bool _altIsDown;
			private bool _ctrlIsDown;
			private Boolean _lastKeyDownSent; // next WM_CHAR ignored if true 
			private bool _shiftIsDown;
			private readonly UcKeyMap _keyMap = new UcKeyMap();
			private readonly TerminalEmulator _parent;

			#region UcKeyboard

			public UcKeyboard(TerminalEmulator p1)
			{
				_parent = p1;
			}

			#endregion

			public event KeyboardEventHandler KeyboardEvent;

			#region KeyDown

			public void KeyDown(Message keyMess)
			{
				Byte[] lBytes;
				Byte[] wBytes;
				UInt16 keyValue = 0;
				Byte scanCode = 0;
				Byte AnsiChar = 0;
				Byte flags = 0;

				lBytes = BitConverter.GetBytes(keyMess.LParam.ToInt32());
				wBytes = BitConverter.GetBytes(keyMess.WParam.ToInt32());
				BitConverter.ToUInt16(lBytes, 0);
				scanCode = lBytes[2];
				flags = lBytes[3];
				//keyValue = BitConverter.ToUInt16(wBytes, 0);


				// key down messages send the scan code in wParam whereas
				// key press messages send the char and unicode values in this word
				if (keyMess.Msg == WMCodes.WM_SYSKEYDOWN || keyMess.Msg == WMCodes.WM_KEYDOWN)
				{
					keyValue = BitConverter.ToUInt16(wBytes, 0);
					switch (keyValue)
					{
						case 16: // Shift Keys
						case 160: // L Shift Key
						case 161: // R Shift Keys
							_shiftIsDown = true;
							return;

						case 17: // Ctrl Keys
						case 162: // L Ctrl Key
						case 163: // R Ctrl Key
							_ctrlIsDown = true;
							return;

						case 18: // Alt Keys (Menu)
						case 164: // L Alt Key
						case 165: // R Ctrl Key
							_altIsDown = true;
							return;
					}

					var modifier = "Key";

					if (_shiftIsDown)
					{
						modifier = "Shift";
					}
					else if (_ctrlIsDown)
					{
						modifier = "Ctrl";
					}
					else if (_altIsDown)
					{
						modifier = "Alt";
					}

					var outString = _keyMap.Find(scanCode, Convert.ToBoolean(flags & 0x01), modifier, _parent._modes.Flags);

					_lastKeyDownSent = false;

					if (outString != "")
					{
						_lastKeyDownSent = true;
						outString = Environment.NewLine;

						KeyboardEvent(this, outString);
					}
				}
				else if (keyMess.Msg == WMCodes.WM_SYSKEYUP ||
				         keyMess.Msg == WMCodes.WM_KEYUP)
				{
					keyValue = BitConverter.ToUInt16(wBytes, 0);

					switch (keyValue)
					{
						case 16: // Shift Keys
						case 160: // L Shift Key
						case 161: // R Shift Keys
							_shiftIsDown = false;
							break;

						case 17: // Ctrl Keys
						case 162: // L Ctrl Key
						case 163: // R Ctrl Key
							_ctrlIsDown = false;
							break;

						case 18: // Alt Keys (Menu)
						case 164: // L Alt Key
						case 165: // R Ctrl Key
							_altIsDown = false;
							break;

						default:
						{
							if (_parent.LocalEcho && keyValue == 13)
							{
								_parent.RxdTextEvent(Environment.NewLine);
								_parent.Refresh();
							}
						}
							break;
					}
				}
				else if (keyMess.Msg == WMCodes.OCM_VSCROLL || keyMess.Msg == WMCodes.MOUSEWHEEL)
				{
					_parent.HandleScroll(keyMess.WParam.ToInt32());
					_parent.Refresh();
					//KRR
				}

				else if (keyMess.Msg == WMCodes.WM_SYSCHAR || keyMess.Msg == WMCodes.WM_CHAR)
				{
					AnsiChar = wBytes[0];
					BitConverter.ToUInt16(wBytes, 0);
					if (_lastKeyDownSent == false)
					{
						KeyboardEvent(this, Convert.ToChar(AnsiChar).ToString(CultureInfo.InvariantCulture));
						{
							if (_parent.LocalEcho)
							{
								_parent.RxdTextEvent(Convert.ToString(Convert.ToChar(AnsiChar).ToString(CultureInfo.InvariantCulture)));
								_parent.Refresh();
							}
						}
					}
				}
			}

			#endregion

			#region class UcKeyInfo

			private class UcKeyInfo
			{
				public readonly Boolean ExtendFlag;
				public readonly UInt32 Flag;
				public readonly UInt32 FlagValue;
				public readonly String Modifier;
				public readonly String OutString;
				public readonly UInt16 ScanCode;

				#region UcKeyInfo

				public UcKeyInfo(UInt16 p1, Boolean p2, String p3, String p4, UInt32 p5, UInt32 p6)
				{
					ScanCode = p1;
					ExtendFlag = p2;
					Modifier = p3;
					OutString = p4;
					Flag = p5;
					FlagValue = p6;
				}

				#endregion
			}

			#endregion

			#region UcKeyMap

			private class UcKeyMap
			{
				public readonly ArrayList Elements = new ArrayList();

				#region UcKeyMap

				public UcKeyMap()
				{
					SetToDefault();
				}

				#endregion

				#region SetToDefault

				// set the key mapping up to emulate most keys on a vt420
				public void SetToDefault()
				{
					// add the default key mappings here
					Elements.Clear();

					// TOPNZ Customisations these should be commented out
					//Elements.Add (new uc_KeyInfo (15,  false, "Shift", "\x1B[OI~", uc_Mode.Any,       0)); //ShTab
					//Elements.Add (new uc_KeyInfo (63,  false, "Key",   "\x1BOT",   uc_Mode.Any,       0)); //F5
					//Elements.Add (new uc_KeyInfo (64,  false, "Key",   "\x1BOU",   uc_Mode.Any,       0)); //F6
					//Elements.Add (new uc_KeyInfo (65,  false, "Key",   "\x1BOV",   uc_Mode.Any,       0)); //F7
					//Elements.Add (new uc_KeyInfo (66,  false, "Key",   "\x1BOW",   uc_Mode.Any,       0)); //F8
					//Elements.Add (new uc_KeyInfo (67,  false, "Key",   "\x1BOX",   uc_Mode.Any,       0)); //F9
					//Elements.Add (new uc_KeyInfo (68,  false, "Key",   "\x1BOY",   uc_Mode.Any,       0)); //F10
					//Elements.Add (new uc_KeyInfo (59,  false, "Shift", "\x1B[25~", uc_Mode.Any,       0)); //ShF1
					//Elements.Add (new uc_KeyInfo (60,  false, "Shift", "\x1B[26~", uc_Mode.Any,       0)); //ShF2
					//Elements.Add (new uc_KeyInfo (61,  false, "Shift", "\x1B[28~", uc_Mode.Any,       0)); //ShF3
					//Elements.Add (new uc_KeyInfo (62,  false, "Shift", "\x1B[29~", uc_Mode.Any,       0)); //ShF4
					//Elements.Add (new uc_KeyInfo (63,  false, "Shift", "\x1B[31~", uc_Mode.Any,       0)); //ShF5
					//Elements.Add (new uc_KeyInfo (64,  false, "Shift", "\x1B[32~", uc_Mode.Any,       0)); //ShF6
					//Elements.Add (new uc_KeyInfo (65,  false, "Shift", "\x1B[33~", uc_Mode.Any,       0)); //ShF7
					//Elements.Add (new uc_KeyInfo (66,  false, "Shift", "\x1B[34~", uc_Mode.Any,       0)); //ShF8
					//Elements.Add (new uc_KeyInfo (67,  false, "Shift", "\x1B[36~", uc_Mode.Any,       0)); //ShF9
					//Elements.Add (new uc_KeyInfo (68,  false, "Shift", "\x1B[37~", uc_Mode.Any,       0)); //ShF10
					//Elements.Add (new uc_KeyInfo (87,  false, "Shift", "\x1B[38~", uc_Mode.Any,       0)); //ShF11
					//Elements.Add (new uc_KeyInfo (88,  false, "Shift", "\x1B[39~", uc_Mode.Any,       0)); //ShF12


					// this is the initial list of keyboard codes
					Elements.Add(new UcKeyInfo(15, false, "Shift", "\x1B[Z", UcMode.Any, 0)); //ShTab
					Elements.Add(new UcKeyInfo(28, false, "Key", "\x0D", UcMode.Any, 0)); //Return
					Elements.Add(new UcKeyInfo(59, false, "Key", "\x1BOP", UcMode.Any, 0)); //F1->PF1
					Elements.Add(new UcKeyInfo(60, false, "Key", "\x1BOQ", UcMode.Any, 0)); //F2->PF2
					Elements.Add(new UcKeyInfo(61, false, "Key", "\x1BOR", UcMode.Any, 0)); //F3->PF3
					Elements.Add(new UcKeyInfo(62, false, "Key", "\x1BOS", UcMode.Any, 0)); //F4->PF4
					Elements.Add(new UcKeyInfo(63, false, "Key", "\x1B[15~", UcMode.Any, 0)); //F5
					Elements.Add(new UcKeyInfo(64, false, "Key", "\x1B[17~", UcMode.Any, 0)); //F6
					Elements.Add(new UcKeyInfo(65, false, "Key", "\x1B[18~", UcMode.Any, 0)); //F7
					Elements.Add(new UcKeyInfo(66, false, "Key", "\x1B[19~", UcMode.Any, 0)); //F8
					Elements.Add(new UcKeyInfo(67, false, "Key", "\x1B[20~", UcMode.Any, 0)); //F9
					Elements.Add(new UcKeyInfo(68, false, "Key", "\x1B[21~", UcMode.Any, 0)); //F10
					Elements.Add(new UcKeyInfo(87, false, "Key", "\x1B[23~", UcMode.Any, 0)); //F11
					Elements.Add(new UcKeyInfo(88, false, "Key", "\x1B[24~", UcMode.Any, 0)); //F12
					Elements.Add(new UcKeyInfo(61, false, "Shift", "\x1B[25~", UcMode.Any, 0)); //ShF3 ->F13
					Elements.Add(new UcKeyInfo(62, false, "Shift", "\x1B[26~", UcMode.Any, 0)); //ShF4 ->F14
					Elements.Add(new UcKeyInfo(63, false, "Shift", "\x1B[28~", UcMode.Any, 0)); //ShF5 ->F15
					Elements.Add(new UcKeyInfo(64, false, "Shift", "\x1B[29~", UcMode.Any, 0)); //ShF6 ->F16
					Elements.Add(new UcKeyInfo(65, false, "Shift", "\x1B[31~", UcMode.Any, 0)); //ShF7 ->F17
					Elements.Add(new UcKeyInfo(66, false, "Shift", "\x1B[32~", UcMode.Any, 0)); //ShF8 ->F18
					Elements.Add(new UcKeyInfo(67, false, "Shift", "\x1B[33~", UcMode.Any, 0)); //ShF9 ->F19
					Elements.Add(new UcKeyInfo(68, false, "Shift", "\x1B[34~", UcMode.Any, 0)); //ShF10->F20
					Elements.Add(new UcKeyInfo(87, false, "Shift", "\x1B[28~", UcMode.Any, 0)); //ShF11->Help
					Elements.Add(new UcKeyInfo(88, false, "Shift", "\x1B[29~", UcMode.Any, 0)); //ShF12->Do
					Elements.Add(new UcKeyInfo(71, true, "Key", "\x1B[1~", UcMode.Any, 0)); //Home
					Elements.Add(new UcKeyInfo(82, true, "Key", "\x1B[2~", UcMode.Any, 0)); //Insert
					Elements.Add(new UcKeyInfo(83, true, "Key", "\x1B[3~", UcMode.Any, 0)); //Delete
					Elements.Add(new UcKeyInfo(79, true, "Key", "\x1B[4~", UcMode.Any, 0)); //End
					Elements.Add(new UcKeyInfo(73, true, "Key", "\x1B[5~", UcMode.Any, 0)); //PageUp
					Elements.Add(new UcKeyInfo(81, true, "Key", "\x1B[6~", UcMode.Any, 0)); //PageDown
					Elements.Add(new UcKeyInfo(72, true, "Key", "\x1B[A", UcMode.CursorAppln, 0)); //CursorUp
					Elements.Add(new UcKeyInfo(80, true, "Key", "\x1B[B", UcMode.CursorAppln, 0)); //CursorDown
					Elements.Add(new UcKeyInfo(77, true, "Key", "\x1B[C", UcMode.CursorAppln, 0)); //CursorKeyRight
					Elements.Add(new UcKeyInfo(75, true, "Key", "\x1B[D", UcMode.CursorAppln, 0)); //CursorKeyLeft
					Elements.Add(new UcKeyInfo(72, true, "Key", "\x1BOA", UcMode.CursorAppln, 1)); //CursorUp
					Elements.Add(new UcKeyInfo(80, true, "Key", "\x1BOB", UcMode.CursorAppln, 1)); //CursorDown
					Elements.Add(new UcKeyInfo(77, true, "Key", "\x1BOC", UcMode.CursorAppln, 1)); //CursorKeyRight
					Elements.Add(new UcKeyInfo(75, true, "Key", "\x1BOD", UcMode.CursorAppln, 1)); //CursorKeyLeft
					Elements.Add(new UcKeyInfo(82, false, "Key", "\x1BOp", UcMode.KeypadAppln, 1)); //Keypad0
					Elements.Add(new UcKeyInfo(79, false, "Key", "\x1BOq", UcMode.KeypadAppln, 1)); //Keypad1
					Elements.Add(new UcKeyInfo(80, false, "Key", "\x1BOr", UcMode.KeypadAppln, 1)); //Keypad2
					Elements.Add(new UcKeyInfo(81, false, "Key", "\x1BOs", UcMode.KeypadAppln, 1)); //Keypad3
					Elements.Add(new UcKeyInfo(75, false, "Key", "\x1BOt", UcMode.KeypadAppln, 1)); //Keypad4
					Elements.Add(new UcKeyInfo(76, false, "Key", "\x1BOu", UcMode.KeypadAppln, 1)); //Keypad5
					Elements.Add(new UcKeyInfo(77, false, "Key", "\x1BOv", UcMode.KeypadAppln, 1)); //Keypad6
					Elements.Add(new UcKeyInfo(71, false, "Key", "\x1BOw", UcMode.KeypadAppln, 1)); //Keypad7
					Elements.Add(new UcKeyInfo(72, false, "Key", "\x1BOx", UcMode.KeypadAppln, 1)); //Keypad8
					Elements.Add(new UcKeyInfo(73, false, "Key", "\x1BOy", UcMode.KeypadAppln, 1)); //Keypad9
					Elements.Add(new UcKeyInfo(74, false, "Key", "\x1BOm", UcMode.KeypadAppln, 1)); //Keypad-
					Elements.Add(new UcKeyInfo(78, false, "Key", "\x1BOl", UcMode.KeypadAppln, 1));
					//Keypad+ (use instead of comma)
					Elements.Add(new UcKeyInfo(83, false, "Key", "\x1BOn", UcMode.KeypadAppln, 1)); //Keypad.
					Elements.Add(new UcKeyInfo(28, true, "Key", "\x1BOM", UcMode.KeypadAppln, 1)); //Keypad Enter
					Elements.Add(new UcKeyInfo(03, false, "Ctrl", "\x00", UcMode.Any, 0)); //Ctrl2->Null
					Elements.Add(new UcKeyInfo(57, false, "Ctrl", "\x00", UcMode.Any, 0)); //CtrlSpaceBar->Null
					Elements.Add(new UcKeyInfo(04, false, "Ctrl", "\x1B", UcMode.Any, 0)); //Ctrl3->Escape
					Elements.Add(new UcKeyInfo(05, false, "Ctrl", "\x1C", UcMode.Any, 0)); //Ctrl4->FS
					Elements.Add(new UcKeyInfo(06, false, "Ctrl", "\x1D", UcMode.Any, 0)); //Ctrl5->GS
					Elements.Add(new UcKeyInfo(07, false, "Ctrl", "\x1E", UcMode.Any, 0)); //Ctrl6->RS
					Elements.Add(new UcKeyInfo(08, false, "Ctrl", "\x1F", UcMode.Any, 0)); //Ctrl7->US
					Elements.Add(new UcKeyInfo(09, false, "Ctrl", "\x7F", UcMode.Any, 0)); //Ctrl8->DEL
				}

				#endregion

				#region Find

				public String Find(UInt16 scanCode, Boolean extendFlag, String modifier, UInt32 modeFlags)
				{
					var outString = "";
					for (var i = 0; i < Elements.Count; i++)
					{
						var element = (UcKeyInfo) Elements[i];

						if (element.ScanCode == scanCode &&
						    element.ExtendFlag == extendFlag &&
						    element.Modifier == modifier &&
						    (element.Flag == UcMode.Any ||
						     ((element.Flag & modeFlags) == element.Flag && element.FlagValue == 1) ||
						     ((element.Flag & modeFlags) == 0 && element.FlagValue == 0)))
						{
							outString = element.OutString;
							return outString;
						}
					}
					return outString;
				}

				#endregion
			}

			#endregion
		}

		#endregion

		#region class uc_VertScrollBar

		private class UcVertScrollBar : VScrollBar
		{
			public UcVertScrollBar()
			{
				SetStyle(ControlStyles.Selectable, false);
				Maximum = 0;
			}
		}

		#endregion

		#region class uc_Params

		private class UcParams
		{
			public readonly ArrayList Elements = new ArrayList();

			public Int32 Count()
			{
				return Elements.Count;
			}

			public void Clear()
			{
				Elements.Clear();
			}

			public void Add(Char curChar)
			{
				if (Count() < 1)
				{
					Elements.Add("0");
				}

				if (curChar == ';')
				{
					Elements.Add("0");
				}
				else
				{
					var i = Elements.Count - 1;
					Elements[i] = string.Format("{0}{1}", Elements[i], curChar);
				}
			}
		}

		#endregion

		#region class uc_Parser

		private class UcParser
		{
			private Char _curChar = '\0';
			private String _curSequence = "";
			private ArrayList _paramList = new ArrayList();
			private States _state = States.Ground;
			private readonly UcCharEvents _charEvents = new UcCharEvents();
			private readonly UcParams _curParams = new UcParams();
			private readonly UcStateChangeEvents _stateChangeEvents = new UcStateChangeEvents();
			public event ParserEventHandler ParserEvent;
			// Every character received is treated as an event which could change the state of
			// the parser. The following section finds out which event or state change this character
			// should trigger and also finds out where we should store the incoming character.
			// The character may be a command, part of a sequence or a parameter; or it might just need
			// binning.
			// The sequence is: state change, store character, do action.

			#region ParseString

			public void ParseString(String inString)
			{
				var nextState = States.None;
				var nextAction = Actions.None;
				var stateExitAction = Actions.None;
				var StateEntryAction = Actions.None;

				foreach (var c in inString)
				{
					_curChar = c;

					// Get the next state and associated action based 
					// on the current state and char event
					_charEvents.GetStateEventAction(_state, _curChar, ref nextState, ref nextAction);

					// execute any actions arising from leaving the current state
					if (nextState != States.None && nextState != _state)
					{
						// check for state exit actions
						_stateChangeEvents.GetStateChangeAction(_state, Transitions.Exit, ref stateExitAction);

						// Process the exit action
						if (stateExitAction != Actions.None) DoAction(stateExitAction);
					}

					// process the action specified
					if (nextAction != Actions.None) DoAction(nextAction);

					// set the new parser state and execute any actions arising entering the new state
					if (nextState != States.None && nextState != _state)
					{
						// change the parsers state attribute
						_state = nextState;

						// check for state entry actions
						_stateChangeEvents.GetStateChangeAction(_state, Transitions.Entry, ref stateExitAction);

						// Process the entry action
						if (StateEntryAction != Actions.None) DoAction(StateEntryAction);
					}
				}
			}

			#endregion

			#region DoAction

			private void DoAction(Actions nextAction)
			{
				// Manage the contents of the Sequence and Param Variables
				switch (nextAction)
				{
					case Actions.Dispatch:
					case Actions.Collect:
						_curSequence += _curChar.ToString(CultureInfo.InvariantCulture);
						break;

					case Actions.NewCollect:
						_curSequence = _curChar.ToString(CultureInfo.InvariantCulture);
						_curParams.Clear();
						break;

					case Actions.Param:
						_curParams.Add(_curChar);
						break;
				}

				// send the external event requests
				switch (nextAction)
				{
					case Actions.Dispatch:
					case Actions.Execute:
					case Actions.Put:
					case Actions.OscStart:
					case Actions.OscPut:
					case Actions.OscEnd:
					case Actions.Hook:
					case Actions.Unhook:
					case Actions.Print:

						//                    System.Console.Write ("Sequence = {0}, Char = {1}, PrmCount = {2}, State = {3}, NextAction = {4}\n",
						//                        this.CurSequence, this.CurChar.ToString (), this.CurParams.Count ().ToString (), 
						//                        this.State.ToString (), NextAction.ToString ());

						ParserEvent(this, new ParserEventArgs(nextAction, _curChar, _curSequence, _curParams));
						break;
				}


				switch (nextAction)
				{
					case Actions.Dispatch:
						_curSequence = "";
						_curParams.Clear();
						break;
				}
			}

			#endregion

			#region enum States

			private enum States
			{
				None = 0,
				Ground = 1,
				EscapeIntrmdt = 2,
				Escape = 3,
				CsiEntry = 4,
				CsiIgnore = 5,
				CsiParam = 6,
				CsiIntrmdt = 7,
				OscString = 8,
				SosPmApcString = 9,
				DcsEntry = 10,
				DcsParam = 11,
				DcsIntrmdt = 12,
				DcsIgnore = 13,
				DcsPassthrough = 14,
				Anywhere = 16
			}

			#endregion

			#region Transition

			private enum Transitions
			{
				None = 0,
				Entry = 1,
				Exit = 2
			}

			#endregion

			#region struct UcCharEventInfo

			private struct UcCharEventInfo
			{
				public readonly Char CharFrom;
				public readonly Char CharTo;
				public readonly States CurState;
				public readonly Actions NextAction;
				public readonly States NextState; // the next state we are going to 

				public UcCharEventInfo(
					States p1,
					Char p2,
					Char p3,
					Actions p4,
					States p5)
				{
					CurState = p1;
					CharFrom = p2;
					CharTo = p3;
					NextAction = p4;
					NextState = p5;
				}
			}

			#endregion

			#region UcCharEvents

			private class UcCharEvents
			{
				#region UcCharEventInfo

				public static readonly UcCharEventInfo[] Elements =
				{
					new UcCharEventInfo(States.Anywhere, '\x1B', '\x1B', Actions.NewCollect, States.Escape),
					new UcCharEventInfo(States.Anywhere, '\x18', '\x18', Actions.Execute, States.Ground),
					new UcCharEventInfo(States.Anywhere, '\x1A', '\x1A', Actions.Execute, States.Ground),
					new UcCharEventInfo(States.Anywhere, '\x1A', '\x1A', Actions.Execute, States.Ground),
					new UcCharEventInfo(States.Anywhere, '\x80', '\x8F', Actions.Execute, States.Ground),
					new UcCharEventInfo(States.Anywhere, '\x91', '\x97', Actions.Execute, States.Ground),
					new UcCharEventInfo(States.Anywhere, '\x99', '\x99', Actions.Execute, States.Ground),
					new UcCharEventInfo(States.Anywhere, '\x9A', '\x9A', Actions.Execute, States.Ground),
					new UcCharEventInfo(States.Anywhere, '\x9C', '\x9C', Actions.Execute, States.Ground),
					new UcCharEventInfo(States.Anywhere, '\x98', '\x98', Actions.None, States.SosPmApcString),
					new UcCharEventInfo(States.Anywhere, '\x9E', '\x9F', Actions.None, States.SosPmApcString),
					new UcCharEventInfo(States.Anywhere, '\x90', '\x90', Actions.NewCollect, States.DcsEntry),
					new UcCharEventInfo(States.Anywhere, '\x9D', '\x9D', Actions.None, States.OscString),
					new UcCharEventInfo(States.Anywhere, '\x9B', '\x9B', Actions.NewCollect, States.CsiEntry),
					new UcCharEventInfo(States.Ground, '\x00', '\x17', Actions.Execute, States.None),
					new UcCharEventInfo(States.Ground, '\x00', '\x17', Actions.Execute, States.None),
					new UcCharEventInfo(States.Ground, '\x19', '\x19', Actions.Execute, States.None),
					new UcCharEventInfo(States.Ground, '\x1C', '\x1F', Actions.Execute, States.None),
					new UcCharEventInfo(States.Ground, '\x20', '\x7F', Actions.Print, States.None),
					new UcCharEventInfo(States.Ground, '\x80', '\x8F', Actions.Execute, States.None),
					new UcCharEventInfo(States.Ground, '\x91', '\x9A', Actions.Execute, States.None),
					new UcCharEventInfo(States.Ground, '\x9C', '\x9C', Actions.Execute, States.None),
					new UcCharEventInfo(States.EscapeIntrmdt, '\x00', '\x17', Actions.Execute, States.None),
					new UcCharEventInfo(States.EscapeIntrmdt, '\x19', '\x19', Actions.Execute, States.None),
					new UcCharEventInfo(States.EscapeIntrmdt, '\x1C', '\x1F', Actions.Execute, States.None),
					new UcCharEventInfo(States.EscapeIntrmdt, '\x20', '\x2F', Actions.Collect, States.None),
					new UcCharEventInfo(States.EscapeIntrmdt, '\x30', '\x7E', Actions.Dispatch, States.Ground),
					new UcCharEventInfo(States.Escape, '\x00', '\x17', Actions.Execute, States.None),
					new UcCharEventInfo(States.Escape, '\x19', '\x19', Actions.Execute, States.None),
					new UcCharEventInfo(States.Escape, '\x1C', '\x1F', Actions.Execute, States.None),
					new UcCharEventInfo(States.Escape, '\x58', '\x58', Actions.None, States.SosPmApcString),
					new UcCharEventInfo(States.Escape, '\x5E', '\x5F', Actions.None, States.SosPmApcString),
					new UcCharEventInfo(States.Escape, '\x50', '\x50', Actions.Collect, States.DcsEntry),
					new UcCharEventInfo(States.Escape, '\x5D', '\x5D', Actions.None, States.OscString),
					new UcCharEventInfo(States.Escape, '\x5B', '\x5B', Actions.Collect, States.CsiEntry),
					new UcCharEventInfo(States.Escape, '\x30', '\x4F', Actions.Dispatch, States.Ground),
					new UcCharEventInfo(States.Escape, '\x51', '\x57', Actions.Dispatch, States.Ground),
					new UcCharEventInfo(States.Escape, '\x59', '\x5A', Actions.Dispatch, States.Ground),
					new UcCharEventInfo(States.Escape, '\x5C', '\x5C', Actions.Dispatch, States.Ground),
					new UcCharEventInfo(States.Escape, '\x60', '\x7E', Actions.Dispatch, States.Ground),
					new UcCharEventInfo(States.Escape, '\x20', '\x2F', Actions.Collect, States.EscapeIntrmdt),
					new UcCharEventInfo(States.CsiEntry, '\x00', '\x17', Actions.Execute, States.None),
					new UcCharEventInfo(States.CsiEntry, '\x19', '\x19', Actions.Execute, States.None),
					new UcCharEventInfo(States.CsiEntry, '\x1C', '\x1F', Actions.Execute, States.None),
					new UcCharEventInfo(States.CsiEntry, '\x20', '\x2F', Actions.Collect, States.CsiIntrmdt),
					new UcCharEventInfo(States.CsiEntry, '\x3A', '\x3A', Actions.None, States.CsiIgnore),
					new UcCharEventInfo(States.CsiEntry, '\x3C', '\x3F', Actions.Collect, States.CsiParam),
					new UcCharEventInfo(States.CsiEntry, '\x3C', '\x3F', Actions.Collect, States.CsiParam),
					new UcCharEventInfo(States.CsiEntry, '\x30', '\x39', Actions.Param, States.CsiParam),
					new UcCharEventInfo(States.CsiEntry, '\x3B', '\x3B', Actions.Param, States.CsiParam),
					new UcCharEventInfo(States.CsiEntry, '\x3C', '\x3F', Actions.Collect, States.CsiParam),
					new UcCharEventInfo(States.CsiEntry, '\x40', '\x7E', Actions.Dispatch, States.Ground),
					new UcCharEventInfo(States.CsiParam, '\x00', '\x17', Actions.Execute, States.None),
					new UcCharEventInfo(States.CsiParam, '\x19', '\x19', Actions.Execute, States.None),
					new UcCharEventInfo(States.CsiParam, '\x1C', '\x1F', Actions.Execute, States.None),
					new UcCharEventInfo(States.CsiParam, '\x30', '\x39', Actions.Param, States.None),
					new UcCharEventInfo(States.CsiParam, '\x3B', '\x3B', Actions.Param, States.None),
					new UcCharEventInfo(States.CsiParam, '\x3A', '\x3A', Actions.None, States.CsiIgnore),
					new UcCharEventInfo(States.CsiParam, '\x3C', '\x3F', Actions.None, States.CsiIgnore),
					new UcCharEventInfo(States.CsiParam, '\x20', '\x2F', Actions.Collect, States.CsiIntrmdt),
					new UcCharEventInfo(States.CsiParam, '\x40', '\x7E', Actions.Dispatch, States.Ground),
					new UcCharEventInfo(States.CsiIgnore, '\x00', '\x17', Actions.Execute, States.None),
					new UcCharEventInfo(States.CsiIgnore, '\x19', '\x19', Actions.Execute, States.None),
					new UcCharEventInfo(States.CsiIgnore, '\x1C', '\x1F', Actions.Execute, States.None),
					new UcCharEventInfo(States.CsiIgnore, '\x40', '\x7E', Actions.None, States.Ground),
					new UcCharEventInfo(States.CsiIntrmdt, '\x00', '\x17', Actions.Execute, States.None),
					new UcCharEventInfo(States.CsiIntrmdt, '\x19', '\x19', Actions.Execute, States.None),
					new UcCharEventInfo(States.CsiIntrmdt, '\x1C', '\x1F', Actions.Execute, States.None),
					new UcCharEventInfo(States.CsiIntrmdt, '\x20', '\x2F', Actions.Collect, States.None),
					new UcCharEventInfo(States.CsiIntrmdt, '\x30', '\x3F', Actions.None, States.CsiIgnore),
					new UcCharEventInfo(States.CsiIntrmdt, '\x40', '\x7E', Actions.Dispatch, States.Ground),
					new UcCharEventInfo(States.SosPmApcString, '\x9C', '\x9C', Actions.None, States.Ground),
					new UcCharEventInfo(States.DcsEntry, '\x20', '\x2F', Actions.Collect, States.DcsIntrmdt),
					new UcCharEventInfo(States.DcsEntry, '\x3A', '\x3A', Actions.None, States.DcsIgnore),
					new UcCharEventInfo(States.DcsEntry, '\x30', '\x39', Actions.Param, States.DcsParam),
					new UcCharEventInfo(States.DcsEntry, '\x3B', '\x3B', Actions.Param, States.DcsParam),
					new UcCharEventInfo(States.DcsEntry, '\x3C', '\x3F', Actions.Collect, States.DcsParam),
					new UcCharEventInfo(States.DcsEntry, '\x40', '\x7E', Actions.None, States.DcsPassthrough),
					new UcCharEventInfo(States.DcsIntrmdt, '\x30', '\x3F', Actions.None, States.DcsIgnore),
					new UcCharEventInfo(States.DcsIntrmdt, '\x40', '\x7E', Actions.None, States.DcsPassthrough),
					new UcCharEventInfo(States.DcsIgnore, '\x9C', '\x9C', Actions.None, States.Ground),
					new UcCharEventInfo(States.DcsParam, '\x30', '\x39', Actions.Param, States.None),
					new UcCharEventInfo(States.DcsParam, '\x3B', '\x3B', Actions.Param, States.None),
					new UcCharEventInfo(States.DcsParam, '\x20', '\x2F', Actions.Collect, States.DcsIntrmdt),
					new UcCharEventInfo(States.DcsParam, '\x3A', '\x3A', Actions.None, States.DcsIgnore),
					new UcCharEventInfo(States.DcsParam, '\x3C', '\x3F', Actions.None, States.DcsIgnore),
					new UcCharEventInfo(States.DcsPassthrough, '\x00', '\x17', Actions.Put, States.None),
					new UcCharEventInfo(States.DcsPassthrough, '\x19', '\x19', Actions.Put, States.None),
					new UcCharEventInfo(States.DcsPassthrough, '\x1C', '\x1F', Actions.Put, States.None),
					new UcCharEventInfo(States.DcsPassthrough, '\x20', '\x7E', Actions.Put, States.None),
					new UcCharEventInfo(States.DcsPassthrough, '\x9C', '\x9C', Actions.None, States.Ground),
					new UcCharEventInfo(States.OscString, '\x20', '\x7F', Actions.OscPut, States.None),
					new UcCharEventInfo(States.OscString, '\x9C', '\x9C', Actions.None, States.Ground)
				};

				#endregion

				#region GetStateEventAction

				public Boolean GetStateEventAction(States curState, Char curChar, ref States nextState,
					ref Actions nextAction)
				{
					UcCharEventInfo Element;

					// Codes A0-FF are treated exactly the same way as 20-7F
					// so we can keep are state table smaller by converting before we look
					// up the event associated with the character

					if (curChar >= '\xA0' &&
					    curChar <= '\xFF')
					{
						curChar -= '\x80';
					}

					for (var i = 0; i < Elements.Length; i++)
					{
						Element = Elements[i];

						if (curChar >= Element.CharFrom &&
						    curChar <= Element.CharTo &&
						    (curState == Element.CurState || Element.CurState == States.Anywhere))
						{
							nextState = Element.NextState;
							nextAction = Element.NextAction;
							return true;
						}
					}

					return false;
				}

				#endregion
			}

			#endregion

			#region UcStateChangeEvents

			private class UcStateChangeEvents
			{
				#region  uc_CharEventInfo

				private readonly UcStateChangeInfo[] _elements =
				{
					new UcStateChangeInfo(States.OscString, Transitions.Entry, Actions.OscStart),
					new UcStateChangeInfo(States.OscString, Transitions.Exit, Actions.OscEnd),
					new UcStateChangeInfo(States.DcsPassthrough, Transitions.Entry, Actions.Hook),
					new UcStateChangeInfo(States.DcsPassthrough, Transitions.Exit, Actions.Unhook)
				};

				#endregion

				#region GetStateChangeAction

				public Boolean GetStateChangeAction(States state, Transitions transition, ref Actions nextAction)
				{
					UcStateChangeInfo Element;

					for (var i = 0; i < _elements.Length; i++)
					{
						Element = _elements[i];

						if (state == Element.State &&
						    transition == Element.Transition)
						{
							nextAction = Element.NextAction;
							return true;
						}
					}
					return false;
				}

				#endregion
			}

			#endregion

			#region UcStateChangeInfo

			private struct UcStateChangeInfo
			{
				public readonly Actions NextAction;
				public readonly States State;
				public readonly Transitions Transition; // the next state we are going to 

				#region UcStateChangeInfo

				public UcStateChangeInfo(States p1, Transitions p2, Actions p3)
				{
					State = p1;
					Transition = p2;
					NextAction = p3;
				}

				#endregion
			}

			#endregion
		}

		#endregion

		#region class uc_TelnetParser

		private class UcTelnetParser
		{
			private Char _curChar = '\0';
			private String _curSequence = "";
			private ArrayList _paramList = new ArrayList();
			private States _state = States.Ground;
			private readonly UcCharEvents _charEvents = new UcCharEvents();
			private readonly UcParams _curParams = new UcParams();
			private readonly UcStateChangeEvents _stateChangeEvents = new UcStateChangeEvents();
			public event NvtParserEventHandler NvtParserEvent;

			#region ParseString

			public void ParseString(String inString)
			{
				var nextState = States.None;
				var nextAction = NvtActions.None;
				var stateExitAction = NvtActions.None;
				var StateEntryAction = NvtActions.None;

				foreach (var c in inString)
				{
					_curChar = c;

					// Get the next state and associated action based 
					// on the current state and char event
					_charEvents.GetStateEventAction(_state, _curChar, ref nextState, ref nextAction);

					// execute any actions arising from leaving the current state
					if (nextState != States.None && nextState != _state)
					{
						// check for state exit actions
						_stateChangeEvents.GetStateChangeAction(_state, Transitions.Exit, ref stateExitAction);

						// Process the exit action
						if (stateExitAction != NvtActions.None) DoAction(stateExitAction);
					}

					// process the action specified
					if (nextAction != NvtActions.None) DoAction(nextAction);

					// set the new parser state and execute any actions arising entering the new state
					if (nextState != States.None && nextState != _state)
					{
						// change the parsers state attribute
						_state = nextState;

						// check for state entry actions
						_stateChangeEvents.GetStateChangeAction(_state, Transitions.Entry, ref stateExitAction);

						// Process the entry action
						if (StateEntryAction != NvtActions.None) DoAction(StateEntryAction);
					}
				}
			}

			#endregion

			#region DoAction

			private void DoAction(NvtActions nextAction)
			{
				// Manage the contents of the Sequence and Param Variables
				switch (nextAction)
				{
					case NvtActions.Dispatch:
					case NvtActions.Collect:
						_curSequence += _curChar.ToString(CultureInfo.InvariantCulture);
						break;

					case NvtActions.NewCollect:
						_curSequence = _curChar.ToString(CultureInfo.InvariantCulture);
						_curParams.Clear();
						break;

					case NvtActions.Param:
						_curParams.Add(_curChar);
						break;
				}

				// send the external event requests
				switch (nextAction)
				{
					case NvtActions.Dispatch:

						//                    System.Console.Write ("Sequence = {0}, Char = {1}, PrmCount = {2}, State = {3}, NextAction = {4}\n",
						//                        this.CurSequence, (int) this.CurChar, this.CurParams.Count (), 
						//                        this.State, NextAction);

						NvtParserEvent(this, new NvtParserEventArgs(nextAction, _curChar, _curSequence, _curParams));
						break;

					case NvtActions.Execute:
					case NvtActions.SendUp:
						NvtParserEvent(this, new NvtParserEventArgs(nextAction, _curChar, _curSequence, _curParams));

						//                    System.Console.Write ("Sequence = {0}, Char = {1}, PrmCount = {2}, State = {3}, NextAction = {4}\n",
						//                        this.CurSequence, (int) this.CurChar, this.CurParams.Count (), 
						//                        this.State, NextAction);
						break;
				}

				switch (nextAction)
				{
					case NvtActions.Dispatch:
						_curSequence = "";
						_curParams.Clear();
						break;
				}
			}

			#endregion

			#region enum States

			private enum States
			{
				None = 0,
				Ground = 1,
				Command = 2,
				Anywhere = 3,
				Synch = 4,
				Negotiate = 5,
				SynchNegotiate = 6,
				SubNegotiate = 7,
				SubNegValue = 8,
				SubNegParam = 9,
				SubNegEnd = 10,
				SynchSubNegotiate = 11
			}

			#endregion

			#region enum Transitions

			private enum Transitions
			{
				None = 0,
				Entry = 1,
				Exit = 2
			}

			#endregion

			#region UcCharEventInfo

			private struct UcCharEventInfo
			{
				public readonly Char CharFrom;
				public readonly Char CharTo;
				public readonly States CurState;
				public readonly NvtActions NextAction;
				public readonly States NextState; // the next state we are going to 

				#region UcCharEventInfo

				public UcCharEventInfo(States p1, Char p2, Char p3, NvtActions p4, States p5)
				{
					CurState = p1;
					CharFrom = p2;
					CharTo = p3;
					NextAction = p4;
					NextState = p5;
				}

				#endregion
			}

			#endregion

			#region UcCharEvents

			private class UcCharEvents
			{
				#region UcCharEventInfo

				public static readonly UcCharEventInfo[] Elements =
				{
					new UcCharEventInfo(States.Ground, (char) 000, (char) 254, NvtActions.SendUp, States.None),
					new UcCharEventInfo(States.Ground, (char) 255, (char) 255, NvtActions.None, States.Command),
					new UcCharEventInfo(States.Command, (char) 000, (char) 239, NvtActions.SendUp, States.Ground),
					new UcCharEventInfo(States.Command, (char) 240, (char) 241, NvtActions.None, States.Ground),
					new UcCharEventInfo(States.Command, (char) 242, (char) 249, NvtActions.Execute, States.Ground),
					new UcCharEventInfo(States.Command, (char) 250, (char) 250, NvtActions.NewCollect,
						States.SubNegotiate),
					new UcCharEventInfo(States.Command, (char) 251, (char) 254, NvtActions.NewCollect, States.Negotiate),
					new UcCharEventInfo(States.Command, (char) 255, (char) 255, NvtActions.SendUp, States.Ground),
					new UcCharEventInfo(States.SubNegotiate, (char) 000, (char) 255, NvtActions.Collect,
						States.SubNegValue),
					new UcCharEventInfo(States.SubNegValue, (char) 000, (char) 001, NvtActions.Collect,
						States.SubNegParam),
					new UcCharEventInfo(States.SubNegValue, (char) 002, (char) 255, NvtActions.None, States.Ground),
					new UcCharEventInfo(States.SubNegParam, (char) 000, (char) 254, NvtActions.Param, States.None),
					new UcCharEventInfo(States.SubNegParam, (char) 255, (char) 255, NvtActions.None, States.SubNegEnd),
					new UcCharEventInfo(States.SubNegEnd, (char) 000, (char) 239, NvtActions.None, States.Ground),
					new UcCharEventInfo(States.SubNegEnd, (char) 240, (char) 240, NvtActions.Dispatch, States.Ground),
					new UcCharEventInfo(States.SubNegEnd, (char) 241, (char) 254, NvtActions.None, States.Ground),
					new UcCharEventInfo(States.SubNegEnd, (char) 255, (char) 255, NvtActions.Param, States.SubNegParam),
					new UcCharEventInfo(States.Negotiate, (char) 000, (char) 255, NvtActions.Dispatch, States.Ground)
				};

				#endregion

				#region GetStateEventAction

				public Boolean GetStateEventAction(States curState, Char curChar, ref States nextState, ref NvtActions nextAction)
				{
					UcCharEventInfo Element;


					for (var i = 0; i < Elements.Length; i++)
					{
						Element = Elements[i];

						if (curChar >= Element.CharFrom &&
						    curChar <= Element.CharTo &&
						    (curState == Element.CurState || Element.CurState == States.Anywhere))
						{
							nextState = Element.NextState;
							nextAction = Element.NextAction;
							return true;
						}
					}

					return false;
				}

				#endregion
			}

			#endregion

			#region UcStateChangeEvents

			private class UcStateChangeEvents
			{
				#region UcStateChangeInfo

				private readonly UcStateChangeInfo[] _elements =
				{
					new UcStateChangeInfo(States.None, Transitions.None, NvtActions.None)
				};

				#endregion

				#region GetStateChangeAction

				public Boolean GetStateChangeAction(States state, Transitions transition, ref NvtActions nextAction)
				{
					UcStateChangeInfo Element;
					for (var i = 0; i < _elements.Length; i++)
					{
						Element = _elements[i];

						if (state == Element.State &&
						    transition == Element.Transition)
						{
							nextAction = Element.NextAction;
							return true;
						}
					}
					return false;
				}

				#endregion
			}

			#endregion

			#region  UcStateChangeInfo

			private struct UcStateChangeInfo
			{
				public readonly NvtActions NextAction;
				public readonly States State;
				public readonly Transitions Transition; // the next state we are going to 

				public UcStateChangeInfo(
					States p1,
					Transitions p2,
					NvtActions p3)
				{
					State = p1;
					Transition = p2;
					NextAction = p3;
				}
			}

			#endregion
		}

		#endregion

		#region Private Enums

		private enum Actions
		{
			None = 0,
			Dispatch = 1,
			Execute = 2,
			Ignore = 3,
			Collect = 4,
			NewCollect = 5,
			Param = 6,
			OscStart = 8,
			OscPut = 9,
			OscEnd = 10,
			Hook = 11,
			Unhook = 12,
			Put = 13,
			Print = 14
		}

		#endregion

		#region NvtActions

		private enum NvtActions
		{
			None = 0,
			SendUp = 1,
			Dispatch = 2,
			Collect = 4,
			NewCollect = 5,
			Param = 6,
			Execute = 7
		}

		#endregion

		#region Public Properties of Comonent

		public int Rows
		{
			get { return _rows; }
			set { }
		}

		public int Columns
		{
			get { return _cols; }
			set { }
		}

		public string LastNumber { get; set; }

		public ConnectionTypes ConnectionType { get; set; }

		public DataBitsTypes DataBitsType { get; set; }

		public StopBitsTypes StopBitsType { get; set; }

		public ParityTypes ParityType { get; set; }

		public FlowTypes FlowType { get; set; }

		public BaudRateTypes BaudRateType { get; set; }

		public string Hostname { get; set; }

		public int Port { get; set; }

		public Boolean Beep { get; set; }

		public bool LocalEcho { get; set; }

		public bool FileActive { get; set; }

		public bool Close { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		public string SerialPort { get; set; }

		public string BBSPrompt { get; set; }

		public string UernamePrompt { get; set; }

		public string PasswordPrompt { get; set; }

		public string Header { get; set; }

		#endregion

		#region enum BaudRateTypes

		public enum BaudRateTypes
		{
			Baud_110 = 110,
			Baud_300 = 300,
			Baud_600 = 600,
			Baud_1200 = 1200,
			Baud_2400 = 2400,
			Baud_4800 = 4800,
			Baud_9600 = 9600,
			Baud_19200 = 19200,
			Baud_38400 = 38400,
			Baud_57600 = 57600
			//Baud_115200 = 115200,
		}

		public enum ConnectionTypes
		{
			Telnet,
			COM,
			SSH1,
			SSH2
		}

		public enum DataBitsTypes
		{
			Data_Bits_5 = 5,
			Data_Bits_6 = 6,
			Data_Bits_7 = 7,
			Data_Bits_8 = 8
		}

		public enum FlowTypes
		{
			XOnXOff,
			RequestToSend,
			RequestToSendXOnXOff,
			None
		}

		public enum ParityTypes
		{
			None,
			Odd,
			Even,
			Mark,
			Space
		}

		public enum StopBitsTypes
		{
			None,
			One,
			OnePointFive,
			Two
		}

		#endregion

		#region Fields

		private static UcParser _parser;
		private readonly UcMode _modes;
		private readonly Color _blinkColor;
		private readonly Color _boldColor;
		private readonly UcCaret _caret;
		private readonly Color _fgColor;
		private readonly UcChars _g0;
		private readonly UcChars _g1;
		private readonly UcChars _g2;
		private readonly UcChars _g3;
		private readonly UcKeyboard _keyboard;
		private readonly ArrayList _savedCarets;
		private readonly StringCollection _scrollbackBuffer;
		private readonly int _scrollbackBufferSize;
		private readonly UcTabStops _tabStops;
		private readonly String _typeFace = FontFamily.GenericMonospace.GetName(0);
		private readonly UcVertScrollBar _vertScrollBar;
		private const Int32 TypeSize = 8;
		private readonly FontStyle TypeStyle = FontStyle.Regular;
		private CharAttribStruct[][] _attribGrid;
		private Point _beginDrag; // used in Mouse Selecting Text
		private Int32 _bottomMargin;
		private string _cType;
		private AsyncCallback _callbackEndDispatch;
		private AsyncCallback _callbackProc;
		private CharAttribStruct _charAttribs;
		private Char[][] _charGrid;
		private Size _charSize;
		private Int32 _cols;
		private Int16 _count;
		private Socket _curSocket;
		private string _dataFile = "";
		private Point _drawStringOffset;
		private Point _endDrag; // used in mouse selecting text
		private Bitmap _eraseBitmap;
		private Graphics _eraseBuffer;
		private string _inputData = String.Empty;
		private int _lastVisibleLine; // used for scrolling
		private String _outBuff = "";
		private Int32 _rows;
		private string _textAtCursor; // used to store Cursortext while scrolling
		private Int32 _topMargin;
		private Int32 _underlinePos;
		private Boolean _xoff;

		#endregion

		#region Delegates

		private delegate void KeyboardEventHandler(object sender, String e);

		private delegate void NvtParserEventHandler(object sender, NvtParserEventArgs e);

		private delegate void ParserEventHandler(object sender, ParserEventArgs e);

		private delegate void RefreshEventHandler();

		private delegate void RxdTextEventHandler(String sReceived);

		#endregion

		#region Events private

		private event RefreshEventHandler RefreshEvent;
		private event RxdTextEventHandler RxdTextEvent;
		//private event CaretOffEventHandler CaretOffEvent;
		//private event CaretOnEventHandler CaretOnEvent;

		#endregion

		#region Constructors

		public event EventHandler Disconnected;
		public event EventHandler ForwardDone;
		public event EventHandler LastNumberevt;

		#endregion

		#region Overrides

		#region override OnResize

		protected override void OnResize(EventArgs e)
		{
			Font = new Font(_typeFace, TypeSize, TypeStyle);
			// reset scrollbar values
			SetScrollBarValues();
			// capture text at cursor b/c it's not in the scrollback buffer yet
			var textAtCursor = "";
			for (var x = 0; x < _cols; x++)
			{
				var curChar = _charGrid[_caret.Pos.Y][x];
				if (curChar == '\0')
				{
					continue;
				}
				textAtCursor = textAtCursor + Convert.ToString(curChar);
			}
			// calculate new rows and columns
			var columns = ClientSize.Width/_charSize.Width - 1;
			var rows = ClientSize.Height/_charSize.Height;

			// make sure at least 1 row and 1 col or Control will throw
			if (rows < 5)
			{
				rows = 5;
				Height = _charSize.Height*rows;
			}

			// make sure at least 1 row and 1 col or Control will throw
			if (columns < 5)
			{
				columns = 5;
				Width = _charSize.Width*columns;
			}

			// make sure the bottom of this doesn't exceed bottom of parent client area
			// for some reason it was getting stuck like that
			if (Parent != null)
				if (Bottom > Parent.ClientSize.Height)
					Height = Parent.ClientSize.Height - Top;

			// reset the char grid
			SetSize(rows, columns);

			//Console.WriteLine(Convert.ToString(rows) + " rows. " + Convert.ToString(this.ScrollbackBuffer.Count + " buffer lines"));

			// populate char grid from ScrollbackBuffer

			// parse through ScrollbackBuffer from the end
			// ScrollbackBuffer[0] is the "oldest" string
			// chargrid[0] is the top row on the display
			var visiblebuffer = new StringCollection();
			for (var i = _scrollbackBuffer.Count - 1; i >= 0; i--)
			{
				visiblebuffer.Insert(0, _scrollbackBuffer[i]);
				// don't parse more strings than our display can show
				if (visiblebuffer.Count >= rows - 1) // rows -1 to leave line for cursor space
					break;
			}
			var lastline = 0;
			for (var i = 0; i < visiblebuffer.Count; i++)
			{
				//Console.WriteLine("Writing string to display: " + visiblebuffer[i]);
				for (var column = 0; column < columns; column++)
				{
					//this.CharGrid[i][column] = '0';
					if (column > visiblebuffer[i].Length - 1)
						continue;
					_charGrid[i][column] = visiblebuffer[i].ToCharArray()[column];
				}
				lastline = i;
			}

			// replace cursor text
			for (var column = 0; column < columns; column++)
			{
				if (column > textAtCursor.Length - 1)
					continue;
				_charGrid[lastline + 1][column] = textAtCursor.ToCharArray()[column];
			}
			CaretToAbs(lastline + 1, textAtCursor.Length);
			Refresh();
			base.OnResize(e);
		}

		#endregion

		#region override OnPaint

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
			e.Graphics.TextRenderingHint = TextRenderingHint.SystemDefault;
			e.Graphics.TextContrast = 0;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;

			WipeScreen(e.Graphics);
			Redraw(e.Graphics);
			ShowCaret(e.Graphics);
		}

		#endregion

		#region override OnPaintBackground

		protected override void OnPaintBackground(PaintEventArgs e)
		{
		}

		#endregion

		#region override WndProc

		protected override void WndProc(ref Message m)
		{
			// Listen for operating system messages and handle the key events.
			switch (m.Msg)
			{
				case WMCodes.WM_KEYDOWN:
				case WMCodes.WM_SYSKEYDOWN:
				case WMCodes.WM_KEYUP:
				case WMCodes.WM_SYSKEYUP:
				case WMCodes.WM_SYSCHAR:
				case WMCodes.WM_CHAR:
				case WMCodes.MOUSEWHEEL:
				case WMCodes.OCM_VSCROLL:
					_keyboard.KeyDown(m);
					break;

				default:
					// don't do any default handling for the aforementioned events
					// this means things like keyboard shortcut events are ignored
					base.WndProc(ref m);
					break;
			}
		}

		#endregion

		#region override OnMouseMove

		protected override void OnMouseMove(MouseEventArgs curArgs)
		{
			if (curArgs.Button != MouseButtons.Left)
				return;

			//Console.WriteLine(Convert.ToString(CurArgs.X) + "," + Convert.ToString(CurArgs.Y));
			_endDrag.X = curArgs.X;
			_endDrag.Y = curArgs.Y;

			var endCol = _endDrag.X/_charSize.Width;
			var endRow = _endDrag.Y/_charSize.Height;

			var begCol = _beginDrag.X/_charSize.Width;
			var begRow = _beginDrag.Y/_charSize.Height;


			// reset highlights
			for (var iRow = 0; iRow < _rows; iRow++)
				for (var iCol = 0; iCol < _cols; iCol++)
					_attribGrid[iRow][iCol].IsInverse = false;


			if (endRow < begRow) // we're parsing backwards
			{
				var i = endRow;
				endRow = begRow;
				begRow = i;
				for (var curRow = begRow; curRow <= endRow; curRow++)
				{
					if (curRow <= 0)
						continue;

					for (var curCol = 0; curCol < _cols; curCol++)
					{
						// don't select if nothing is there
						if (_charGrid[curRow][curCol] == '\0')
							continue;

						// on first row, make sure we start at begCol
						if (curRow == begRow && curCol < endCol)
							continue;

						// on last row, don't pass the end col
						if (curRow == endRow && curCol == begCol)
						{
							_attribGrid[curRow][curCol].IsInverse = true;
							break;
						}

						_attribGrid[curRow][curCol].IsInverse = true;
					}
				}
				Refresh();
				return;
			}

			if (endCol < begCol && begRow == endRow) // we're parsing backwards, but only on one row
			{
				var i = endCol;
				endCol = begCol;
				begCol = i;
			}

			// parse the rows affected and highlight them
			// endRow/endCol are where the mouse is now
			// begRow/begCol are where the mouse was when the button was pushed
			for (var curRow = begRow; curRow <= endRow; curRow++)
			{
				if (curRow >= _rows)
					break;

				for (var curCol = 0; curCol < _cols; curCol++)
				{
					// don't select if nothing is there
					if (_charGrid[curRow][curCol] == '\0')
						continue;

					// on first row, make sure we start at begCol
					if (curRow == begRow && curCol < begCol)
						continue;

					// on last row, don't pass the end col
					if (curRow == endRow && curCol == endCol)
					{
						_attribGrid[curRow][curCol].IsInverse = true;
						break;
					}
					_attribGrid[curRow][curCol].IsInverse = true;
				}
			}
			Refresh();
		}

		#endregion

		#region override OnMouseUp

		protected override void OnMouseUp(MouseEventArgs curArgs)
		{
			if (curArgs.Button == MouseButtons.Left)
			{
				if (_beginDrag.X == curArgs.X && _beginDrag.Y == curArgs.Y)
				{
					// reset highlights
					for (var iRow = 0; iRow < _rows; iRow++)
						for (var iCol = 0; iCol < _cols; iCol++)
							_attribGrid[iRow][iCol].IsInverse = false;
					Refresh();
				}
			}
		}

		#endregion

		#region override OnMouseDown

		protected override void OnMouseDown(MouseEventArgs curArgs)
		{
			Focus();
			if (curArgs.Button == MouseButtons.Left)
			{
				// begin select
				_beginDrag.X = curArgs.X;
				_beginDrag.Y = curArgs.Y;
			}
			base.OnMouseDown(curArgs);
		}

		#endregion

		#region

		protected override void OnFontChanged(EventArgs e)
		{
			//MessageBox.Show(this.Font.Name + " " + Convert.ToString(this.Font.Size));
		}

		#endregion

		#endregion

		#region Private Structs

		#region ParserEventArgs

		private struct ParserEventArgs
		{
			public readonly Actions Action;
			public readonly Char CurChar;
			public readonly UcParams CurParams;
			public readonly String CurSequence;

			public ParserEventArgs(
				Actions p1,
				Char p2,
				String p3,
				UcParams p4)
			{
				Action = p1;
				CurChar = p2;
				CurSequence = p3;
				CurParams = p4;
			}
		}

		#endregion

		#region CharAttribStruct

		private struct CharAttribStruct
		{
			public Color AltBgColor;
			public Color AltColor;
			public UcChars Gl;
			public UcChars GR;
			public UcChars Gs;
			public Boolean IsAlternateFont;
			public Boolean IsBlinking;
			public Boolean IsBold;
			public Boolean IsDECSG;
			public Boolean IsDim;
			public Boolean IsInverse;
			public Boolean IsPrimaryFont;
			public Boolean IsUnderscored;
			public Boolean UseAltBGColor;
			public Boolean UseAltColor;
		}

		#endregion

		#region NvtParserEventArgs

		private struct NvtParserEventArgs
		{
			public UcParams CurParams;
			public readonly NvtActions Action;
			public readonly Char CurChar;
			public readonly String CurSequence;

			#region  NvtParserEventArgs

			public NvtParserEventArgs(NvtActions p1, Char p2, String p3, UcParams p4)
			{
				Action = p1;
				CurChar = p2;
				CurSequence = p3;
				CurParams = p4;
			}

			#endregion
		}

		#endregion

		#endregion Private Structs
	}

	#endregion
}