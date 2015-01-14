#region Using Directive

using System;
using System.Drawing;
using System.Windows.Forms;
using PacketComs;

#endregion

namespace Packet
{
	//---------------------------------------------------------------------------------------------------------
	//  partial class Form1
	//---------------------------------------------------------------------------------------------------------

	#region partial class Form1

	public partial class Main : Form
	{
        ModifyRegistry _myRegistryKeep = new ModifyRegistry();

		#region Form1

        public const string DsnName = "DSN=Packet";	
	   
		public Main()
		{
			InitializeComponent();
			_myRegistry.SubKey = "SOFTWARE\\NA7KR\\Packet";
			_myRegistry.ShowError = true;
			_myRegistryCom.SubKey = "SOFTWARE\\NA7KR\\Packet\\Port";
			_myRegistryBbs.SubKey = "SOFTWARE\\NA7KR\\Packet\\BBS";
			_myRegistryNode.SubKey = "SOFTWARE\\NA7KR\\Packet\\Node";
			_myRegistryCluster.SubKey = "SOFTWARE\\NA7KR\\Packet\\Cluster";
			_myRegistrySsh.SubKey = "SOFTWARE\\NA7KR\\Packet\\SSH";
            _myRegistryKeep.SubKey = "SOFTWARE\\NA7KR\\Packet\\Keep";
            _myRegistryKeep.ShowError = true;
			_myRegistryCom.ShowError = true;
			_myRegistryBbs.ShowError = true;
			_myRegistryNode.ShowError = true;
			_myRegistryCluster.ShowError = true;
			_myRegistrySsh.ShowError = true;
			_myRegistry.Write("Packet", Application.ProductVersion);
			bbs_button.Enabled = true;
			forward_button.Enabled = false;
			toolStripComboBox1.Items.Clear();
			toolStripComboBox1.Items.Add("Telnet");
			toolStripComboBox1.Items.Add("Com Port");
			toolStripComboBox2.Items.Clear();
			toolStripComboBox2.Items.Add("Telnet");
			toolStripComboBox2.Items.Add("Com Port");
			toolStripComboBox3.Items.Clear();
			toolStripComboBox3.Items.Add("Telnet");
			toolStripComboBox3.Items.Add("Com Port");
			toolStripComboBoxBeep.Items.Add("Yes");
			toolStripComboBoxBeep.Items.Add("No");
			toolStripComboBoxTXTC.Items.Add("Black");
			toolStripComboBoxTXTC.Items.Add("Red");
			toolStripComboBoxTXTC.Items.Add("Green");
			toolStripComboBoxTXTC.Items.Add("Yellow");
			toolStripComboBoxTXTC.Items.Add("Blue");
			toolStripComboBoxTXTC.Items.Add("Magenta");
			toolStripComboBoxTXTC.Items.Add("Cyan");
			toolStripComboBoxTXTC.Items.Add("White");

			toolStripComboBoxBGC.Items.Add("Black");
			toolStripComboBoxBGC.Items.Add("Red");
			toolStripComboBoxBGC.Items.Add("Green");
			toolStripComboBoxBGC.Items.Add("Yellow");
			toolStripComboBoxBGC.Items.Add("Blue");
			toolStripComboBoxBGC.Items.Add("Magenta");
			toolStripComboBoxBGC.Items.Add("Cyan");
			toolStripComboBoxBGC.Items.Add("White");

            OnResize(EventArgs.Empty);
		    terminalEmulator1.dnsName = DsnName;
		}

	    protected override sealed void OnResize(EventArgs e)
	    {
	        base.OnResize(e);
	    }

	    #endregion

		#region private void aboutToolStripMenuItem_Click

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var box = new AboutBox1();
			box.ShowDialog();
		}

		#endregion

		#region private void exitToolStripMenuItem1_Click

		private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			Close();
			Application.Exit();
		}

		#endregion

        #region toolStripComboBox1_SelectedIndexChanged

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (toolStripComboBox1.SelectedIndex)
			{
				case 0:
					_myRegistry.Write("BBS-Mode", "Telnet");
					iPConfigToolStripMenuItem.Text = "IP BBS Config";
					bbs_button.Enabled = true;
					break;
				case 1:
					_myRegistry.Write("BBS-Mode", "Com");
					iPConfigToolStripMenuItem.Text = "BBS Config";
					bbs_button.Enabled = true;
					break;
			}
		}

		#endregion

		#region private void toolStripComboBox2_SelectedIndexChanged

		private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (toolStripComboBox2.SelectedIndex)
			{
				case 0:
					_myRegistry.Write("Cluster-Mode", "Telnet");
					clusterIPConfigToolStripMenuItem.Text = "IP Cluster Config";
					cluster_button.Enabled = true;
					break;
				case 1:
					_myRegistry.Write("Cluster-Mode", "Com");
					clusterIPConfigToolStripMenuItem.Text = "Cluster Config";
					cluster_button.Enabled = true;
					break;
			}
		}

		#endregion

		#region private void toolStripComboBox3_SelectedIndexChanged

		private void toolStripComboBox3_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (toolStripComboBox3.SelectedIndex)
			{
				case 0:
					_myRegistry.Write("Node-Mode", "Telnet");
					nodeIPConfigToolStripMenuItem.Text = "IP Node Config";
					node_button.Enabled = true;
					break;

				case 1:
					_myRegistry.Write("Node-Mode", "Com");
					nodeIPConfigToolStripMenuItem.Text = "Node Config";
					node_button.Enabled = true;
					break;
			}
		}

		#endregion

		#region private void toolStripComboBoxBeep_SelectedIndexChanged

		private void toolStripComboBoxBeep_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (toolStripComboBoxBeep.SelectedIndex)
			{
				case 0:
					_myRegistry.Write("Beep", "Yes");
					_bBeep = true;
					break;
				case 1:
					_myRegistry.Write("Beep", "No");
					_bBeep = false;
					break;
			}
		}

		#endregion

		#region toolStripComboBoxTXTC_SelectedIndexChanged

		private void toolStripComboBoxTXTC_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (toolStripComboBoxTXTC.SelectedIndex)
			{
				case 0:
					_myRegistry.Write("Color Text", "Black");
					_textColor = Color.Black;
					break;
				case 1:
					_myRegistry.Write("Color Text", "Red");
					_textColor = Color.Red;
					break;
				case 2:
					_myRegistry.Write("Color Text", "Green");
					_textColor = Color.Green;
					break;
				case 3:
					_myRegistry.Write("Color Text", "Yellow");
					_textColor = Color.Yellow;
					break;
				case 4:
					_myRegistry.Write("Color Text", "Blue");
					_textColor = Color.Blue;
					break;
				case 5:
					_myRegistry.Write("Color Text", "Magenta");
					_textColor = Color.Magenta;
					break;
				case 6:
					_myRegistry.Write("Color Text", "Cyan");
					_textColor = Color.Cyan;
					break;
				case 7:
					_myRegistry.Write("Color Text", "White");
					_textColor = Color.White;
					break;
			}
			terminalEmulator1.ForeColor = _textColor;
		}

		#endregion

		#region toolStripComboBoxBGC

		private void toolStripComboBoxBGC_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (toolStripComboBoxBGC.SelectedIndex)
			{
				case 0:
					_myRegistry.Write("Color Background", "Black");
					_backgroundColor = Color.Black;
					break;
				case 1:
					_myRegistry.Write("Color Background", "Red");
					_backgroundColor = Color.Red;
					break;
				case 2:
					_myRegistry.Write("Color Background", "Green");
					_backgroundColor = Color.Green;
					break;
				case 3:
					_myRegistry.Write("Color Background", "Yellow");
					_backgroundColor = Color.Yellow;
					break;
				case 4:
					_myRegistry.Write("Color Background", "Blue");
					_backgroundColor = Color.Blue;
					break;
				case 5:
					_myRegistry.Write("Color Background", "Magenta");
					_backgroundColor = Color.Magenta;
					break;
				case 6:
					_myRegistry.Write("Color Background", "Cyan");
					_backgroundColor = Color.Cyan;
					break;
				case 7:
					_myRegistry.Write("Color Background", "White");
					_backgroundColor = Color.White;
					break;
			}
			terminalEmulator1.BackColor = _backgroundColor;
		}

		#endregion

		#region private void iPConfigToolStripMenuItem_Click

		private void iPConfigToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var box = new IpForm("BBS", (_myRegistry.Read("BBS-Mode")));
			box.ShowDialog();
		}

		#endregion

		#region private void Form1_Resize_1

		private void Form1_Resize_1(object sender, EventArgs e)
		{
			terminalEmulator1.Left = 20;
			terminalEmulator1.Top = 80;
			terminalEmulator1.Height = (Height - 20);
			terminalEmulator1.Width = (Width - 60);

            int bwidth = ((Width - (9 * 90)) / 10);
            bbs_button.Width = 90;
            bbs_button.Left = bwidth;
            bbs_button.Top = 40;

            forward_button.Width = 90 ;
            forward_button.Left = bwidth + bbs_button.Right;
            forward_button.Top = 40;

            cluster_button.Width = 90;
            cluster_button.Left = bwidth + forward_button.Right;
            cluster_button.Top = 40;

            node_button.Width = 90;
		    node_button.Left = bwidth + cluster_button.Right;
            node_button.Top = 40;

            disconnect_button.Width = 90;
		    disconnect_button.Left = bwidth + node_button.Right;
            disconnect_button.Top = 40;
            disconnect_button.Enabled = false;


		    ssh_button.Left = bwidth + disconnect_button.Right;
            ssh_button.Top = 40;
            ssh_button.Width = 90;

		    mail_button.Left = bwidth + ssh_button.Right;
            mail_button.Top = 40;
            mail_button.Width = 90;

		    button_read.Left = bwidth + mail_button.Right;
            button_read.Top = 40;
            button_read.Width = 90;

		    button_personal.Left = bwidth + button_read.Right;
		    button_personal.Top = 40;
            button_personal.Width = 90;
		}

		#endregion

		#region forward_button_Click

		private void forward_button_Click(object sender, EventArgs e)
		{
            Sql.Deletedays(_myRegistryKeep.ReadDw("DaystoKeep"));
            Sql.DeleteCount(_myRegistryKeep.ReadDw("QTYtoKeep"));
			terminalEmulator1.FileActive = true;
			forward_button.Enabled = false;
			forward_button.Text = "Forward active";
			terminalEmulator1.LastNumber = Convert.ToInt32(_myRegistryBbs.ReadDw("Start Number"));
			terminalEmulator1.Startforward();		
		}

		#endregion

		#region load

		private void Form1_Load(object sender, EventArgs e)
		{
			if (_myRegistry.Read("BBS-Mode") == "Telnet")
			{
				toolStripComboBox1.SelectedIndex = 0;
				iPConfigToolStripMenuItem.Text = "IP BBS Config";
			}
			else if (_myRegistry.Read("BBS-Mode") == "Com")
			{
				toolStripComboBox1.SelectedIndex = 1;
				iPConfigToolStripMenuItem.Text = "BBS Config";
			}
			else
			{
				bbs_button.Enabled = false;
				iPConfigToolStripMenuItem.Text = "BBS Config";
			}

			if (_myRegistry.Read("Cluster-Mode") == "Telnet")
			{
				toolStripComboBox2.SelectedIndex = 0;
				clusterIPConfigToolStripMenuItem.Text = "IP Cluster Config";
			}
			else if (_myRegistry.Read("Cluster-Mode") == "Com")
			{
				toolStripComboBox2.SelectedIndex = 1;
				clusterIPConfigToolStripMenuItem.Text = "Cluster Config";
			}
			else
			{
				cluster_button.Enabled = false;
				clusterIPConfigToolStripMenuItem.Text = "Cluster Config";
			}

			if (_myRegistry.Read("Node-Mode") == "Telnet")
			{
				toolStripComboBox3.SelectedIndex = 0;
			}
			else if (_myRegistry.Read("Node-Mode") == "Com")
			{
				toolStripComboBox3.SelectedIndex = 1;
				nodeIPConfigToolStripMenuItem.Text = "IP Node Config";
			}
			else
			{
				node_button.Enabled = false;
				nodeIPConfigToolStripMenuItem.Text = "Node Config";
			}
			if (_myRegistry.Read("Beep") == "Yes")
			{
				_bBeep = true;
				toolStripComboBoxBeep.SelectedIndex = 0;
			}
			if (_myRegistry.Read("Beep") == "No")
			{
				_bBeep = false;
				toolStripComboBoxBeep.SelectedIndex = 1;
			}
			var myReg = _myRegistry.Read("Color Text");
			switch (myReg)
			{
				case "Black":
					toolStripComboBoxTXTC.SelectedIndex = 0;
					_textColor = Color.Black;
					break;
				case "Red":
					toolStripComboBoxTXTC.SelectedIndex = 1;
					_textColor = Color.Red;
					break;
				case "Green":
					toolStripComboBoxTXTC.SelectedIndex = 2;
					_textColor = Color.Green;
					break;
				case "Yellow":
					toolStripComboBoxTXTC.SelectedIndex = 3;
					_textColor = Color.Yellow;
					break;
				case "Blue":
					toolStripComboBoxTXTC.SelectedIndex = 4;
					_textColor = Color.Blue;
					break;
				case "Magenta":
					toolStripComboBoxTXTC.SelectedIndex = 5;
					_textColor = Color.Magenta;
					break;
				case "Cyan":
					toolStripComboBoxTXTC.SelectedIndex = 6;
					_textColor = Color.Cyan;
					break;
				case "White":
					toolStripComboBoxTXTC.SelectedIndex = 7;
					_textColor = Color.White;
					break;
				default:
					_textColor = Color.White;
					break;
			}
			terminalEmulator1.ForeColor = _textColor;


			var myReg2 = _myRegistry.Read("Color Background");
			switch (myReg2)
			{
				case "Black":
					toolStripComboBoxBGC.SelectedIndex = 0;
					_backgroundColor = Color.Black;
					break;
				case "Red":
					toolStripComboBoxBGC.SelectedIndex = 1;
					_backgroundColor = Color.Red;
					break;
				case "Green":
					toolStripComboBoxBGC.SelectedIndex = 2;
					_backgroundColor = Color.Green;
					break;
				case "Yellow":
					toolStripComboBoxBGC.SelectedIndex = 3;
					_backgroundColor = Color.Yellow;
					break;
				case "Blue":
					toolStripComboBoxBGC.SelectedIndex = 4;
					_backgroundColor = Color.Blue;
					break;
				case "Magenta":
					toolStripComboBoxBGC.SelectedIndex = 5;
					_backgroundColor = Color.Magenta;
					break;
				case "Cyan":
					toolStripComboBoxBGC.SelectedIndex = 6;
					_backgroundColor = Color.Cyan;
					break;
				case "White":
					toolStripComboBoxBGC.SelectedIndex = 7;
					_backgroundColor = Color.White;
					break;
				default:
					_backgroundColor = Color.Black;
					break;
			}
			terminalEmulator1.BackColor = _backgroundColor;
			if (_myRegistrySsh.Read("Active") == "Yes")
			{
				ssh_button.Visible = true;
			}
			else
				ssh_button.Visible = false;
		}

		#endregion

		#region connect bbs

		private void bbs_button_Click(object sender, EventArgs e)
		{
			try
			{
				if (_myRegistryBbs.BRead("Prompt") == "BlanKey!!")
				{
					MessageBox.Show("BBS may not correct as BBS Prompt cot configured", "Important Note",
						MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
				}

				if (_myRegistry.Read("BBS-Mode") == "Telnet")
				{
					if (_myRegistryBbs.Read("Echo") == "Yes")
					{
						terminalEmulator1.LocalEcho = true;
					}
					else
					{
						terminalEmulator1.LocalEcho = false;
					}
					terminalEmulator1.Port = Convert.ToInt32(_myRegistryBbs.Read("Port"));
					terminalEmulator1.Hostname = _myRegistryBbs.Read("IP");
					terminalEmulator1.Username = _myRegistryBbs.Read("CallSign");
					terminalEmulator1.Password = _myEncrypt.Decrypt(_myRegistryBbs.Read("Password"));
					terminalEmulator1.ConnectionType = TerminalEmulator.ConnectionTypes.Telnet;
					terminalEmulator1.BBSPrompt = _myRegistryBbs.BRead("Prompt");
					terminalEmulator1.UernamePrompt = _myRegistryBbs.BRead("UserNamePrompt");
					terminalEmulator1.PasswordPrompt = _myRegistryBbs.BRead("PasswordPrompt");
				}
				else
				{
					terminalEmulator1.BaudRateType =
						ParseEnum<TerminalEmulator.BaudRateTypes>("Baud_" + _myRegistryCom.Read("Baud"));
					terminalEmulator1.DataBitsType =
						ParseEnum<TerminalEmulator.DataBitsTypes>("Data_Bits_" +
						                                          _myRegistryCom.Read("Data Bits"));
					terminalEmulator1.StopBitsType =
						ParseEnum<TerminalEmulator.StopBitsTypes>(_myRegistryCom.Read("Stop Bits"));
					terminalEmulator1.ParityType =
						ParseEnum<TerminalEmulator.ParityTypes>(_myRegistryCom.Read("Parity"));
					terminalEmulator1.FlowType =
						ParseEnum<TerminalEmulator.FlowTypes>(_myRegistryCom.Read("Flow"));
					terminalEmulator1.ConnectionType = TerminalEmulator.ConnectionTypes.COM;
					terminalEmulator1.BBSPrompt = _myRegistryBbs.BRead("Prompt");
					terminalEmulator1.SerialPort = _myRegistryCom.Read("Port");
				}

				terminalEmulator1.Connect();
				disconnect_button.Enabled = true;
				bbs_button.Enabled = false;
				cluster_button.Enabled = false;
				node_button.Enabled = false;
				ssh_button.Enabled = false;
				forward_button.Enabled = true;
			}
			catch
			{
				MessageBox.Show("Configure BBS in Setup");
				bbs_button.Enabled = true;
				cluster_button.Enabled = true;
				node_button.Enabled = true;
				disconnect_button.Enabled = false;
				if (_myRegistrySsh.Read("Active") == "No")
				{
					ssh_button.Enabled = false;
				}
				else
					ssh_button.Enabled = true;
			}
		}

		#endregion

		#region connect_cluster

		private void cluster_button_Click(object sender, EventArgs e)
		{
			try
			{
				bbs_button.Enabled = false;
				cluster_button.Enabled = false;
				node_button.Enabled = false;
				ssh_button.Enabled = false;
				if (_myRegistry.Read("Cluster-Mode") == "Telnet")
				{
					if (_myRegistryCluster.Read("Echo") == "Yes")
					{
						terminalEmulator1.LocalEcho = true;
					}
					else
					{
						terminalEmulator1.LocalEcho = false;
					}
					terminalEmulator1.Beep = _bBeep;
					terminalEmulator1.Port = Convert.ToInt32(_myRegistryCluster.Read("Port"));
					terminalEmulator1.Hostname = _myRegistryCluster.Read("IP");
					terminalEmulator1.Username = _myRegistryCluster.Read("CallSign");
					terminalEmulator1.Password = _myEncrypt.Decrypt(_myRegistryCluster.Read("Password"));
					terminalEmulator1.ConnectionType = TerminalEmulator.ConnectionTypes.Telnet;
					terminalEmulator1.BBSPrompt = _myRegistryCluster.BRead("Prompt");
					terminalEmulator1.UernamePrompt = _myRegistryCluster.BRead("UserNamePrompt");
					terminalEmulator1.PasswordPrompt = _myRegistryCluster.BRead("PasswordPrompt");
					terminalEmulator1.Connect();
					disconnect_button.Enabled = true;
				}
				else
				{
					terminalEmulator1.BaudRateType =
						ParseEnum<TerminalEmulator.BaudRateTypes>("Baud_" + _myRegistryCom.Read("Baud"));
					terminalEmulator1.DataBitsType =
						ParseEnum<TerminalEmulator.DataBitsTypes>("Data_Bits_" +
						                                          _myRegistryCom.Read("Data Bits"));
					terminalEmulator1.StopBitsType =
						ParseEnum<TerminalEmulator.StopBitsTypes>(_myRegistryCom.Read("Stop Bits"));
					terminalEmulator1.ParityType =
						ParseEnum<TerminalEmulator.ParityTypes>(_myRegistryCom.Read("Parity"));
					terminalEmulator1.FlowType =
						ParseEnum<TerminalEmulator.FlowTypes>(_myRegistryCom.Read("Flow"));
					terminalEmulator1.ConnectionType = TerminalEmulator.ConnectionTypes.COM;
					terminalEmulator1.SerialPort = _myRegistryCom.Read("Port");
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Configure Cluster in Setup");
				bbs_button.Enabled = true;
				cluster_button.Enabled = true;
				node_button.Enabled = true;
				disconnect_button.Enabled = false;
				if (_myRegistrySsh.Read("Active") == "No")
				{
					ssh_button.Enabled = false;
				}
				else
					ssh_button.Enabled = true;
			}
		}

		#endregion

		#region private void node_button_Click

		private void node_button_Click(object sender, EventArgs e)
		{
			try
			{
				bbs_button.Enabled = false;
				cluster_button.Enabled = false;
				node_button.Enabled = false;
				ssh_button.Enabled = false;
				if (_myRegistry.Read("Node-Mode") == "Telnet")
				{
					if (_myRegistryNode.Read("Echo") == "Yes")
					{
						terminalEmulator1.LocalEcho = true;
					}
					else
					{
						terminalEmulator1.LocalEcho = false;
					}
					terminalEmulator1.Port = Convert.ToInt32(_myRegistryNode.Read("Port"));
					terminalEmulator1.Hostname = _myRegistryNode.Read("IP");
					terminalEmulator1.Username = _myRegistryNode.Read("CallSign");
					terminalEmulator1.Password = _myEncrypt.Decrypt(_myRegistryNode.Read("Password"));
					terminalEmulator1.ConnectionType = TerminalEmulator.ConnectionTypes.Telnet;
					terminalEmulator1.BBSPrompt = _myRegistryNode.BRead("Prompt");
					terminalEmulator1.UernamePrompt = _myRegistryNode.BRead("UserNamePrompt");
					terminalEmulator1.PasswordPrompt = _myRegistryNode.BRead("PasswordPrompt");
					terminalEmulator1.Connect();
					disconnect_button.Enabled = true;
				}
				else
				{
					terminalEmulator1.BaudRateType =
						ParseEnum<TerminalEmulator.BaudRateTypes>("Baud_" + _myRegistryCom.Read("Baud"));
					terminalEmulator1.DataBitsType =
						ParseEnum<TerminalEmulator.DataBitsTypes>("Data_Bits_" +
						                                          _myRegistryCom.Read("Data Bits"));
					terminalEmulator1.StopBitsType =
						ParseEnum<TerminalEmulator.StopBitsTypes>(_myRegistryCom.Read("Stop Bits"));
					terminalEmulator1.ParityType =
						ParseEnum<TerminalEmulator.ParityTypes>(_myRegistryCom.Read("Parity"));
					terminalEmulator1.FlowType =
						ParseEnum<TerminalEmulator.FlowTypes>(_myRegistryCom.Read("Flow"));
					terminalEmulator1.ConnectionType = TerminalEmulator.ConnectionTypes.COM;
					terminalEmulator1.SerialPort = _myRegistryCom.Read("Port");
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Configure Node in Setup");
				bbs_button.Enabled = true;
				cluster_button.Enabled = true;
				node_button.Enabled = true;
				disconnect_button.Enabled = false;
				if (_myRegistrySsh.Read("Active") == "No")
				{
					ssh_button.Enabled = false;
				}
				else
					ssh_button.Enabled = true;
			}
		}

		#endregion

		#region private void clusterIPConfigToolStripMenuItem_Click

		private void clusterIPConfigToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var box = new IpForm("Cluster", (_myRegistry.Read("Cluster-Mode")));
			box.ShowDialog();
		}

		#endregion

		#region private void nodeIPConfigToolStripMenuItem_Click

		private void nodeIPConfigToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var box = new IpForm("Node", (_myRegistry.Read("Node-Mode")));
			box.ShowDialog();
		}

		#endregion

		#region private void disconnect_button_Click

		private void disconnect_button_Click(object sender, EventArgs e)
		{
			bbs_button.Enabled = true;
			cluster_button.Enabled = true;
			node_button.Enabled = true;
			disconnect_button.Enabled = false;
			ssh_button.Enabled = true;
			terminalEmulator1.Closeconnection();
			terminalEmulator1.FileActive = false;
			forward_button.Enabled = false;
			forward_button.Text = "Forward";
			if (_myRegistrySsh.Read("Active") == "No")
			{
				ssh_button.Enabled = false;
			}
			else
				ssh_button.Enabled = true;
		}

		#endregion

		#region ssh_button_Click

		private void ssh_button_Click(object sender, EventArgs e)
		{
			try
			{
				ssh_button.Enabled = false;
				bbs_button.Enabled = false;
				cluster_button.Enabled = false;
				node_button.Enabled = false;
				disconnect_button.Enabled = true;
				terminalEmulator1.ConnectionType = TerminalEmulator.ConnectionTypes.SSH2;
				terminalEmulator1.Port = Convert.ToInt32(_myRegistrySsh.Read("Port"));
				terminalEmulator1.Hostname = _myRegistrySsh.Read("IP");
				terminalEmulator1.Username = _myRegistrySsh.Read("UserName");
				terminalEmulator1.Password = _myEncrypt.Decrypt(_myRegistrySsh.Read("Password"));
				terminalEmulator1.Connect();
			}
			catch (Exception)
			{
				MessageBox.Show("Configure SSH in Setup");
				bbs_button.Enabled = true;
				cluster_button.Enabled = true;
				node_button.Enabled = true;
				disconnect_button.Enabled = false;
				if (_myRegistrySsh.Read("Active") == "No")
				{
					ssh_button.Enabled = false;
				}
				else
					ssh_button.Enabled = true;
			}
		}

		#endregion

		#region toolStripMenuItem1_Click SSH

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			var box = new IpForm("SSH", "SSH");
			box.ShowDialog();
			if (_myRegistrySsh.Read("Active") == "No")
			{
				ssh_button.Visible = false;
			}
			else
				ssh_button.Visible = true;
		}

		#endregion

		#region disconnect

		private void Disconnected(object sender, EventArgs e)
		{
			Invoke((Action) delegate { bbs_button.Enabled = true; });
			Invoke((Action) delegate { cluster_button.Enabled = true; });
			Invoke((Action) delegate { node_button.Enabled = true; });
			Invoke((Action) delegate { disconnect_button.Enabled = false; });
		}

		#endregion

		#region toolStripMenu click show Com port config

		private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
		{
			var box = new ComForm();
			box.ShowDialog();
		}

		#endregion

		#region ParseEnum

		public static T ParseEnum<T>(string value)
		{
			return (T) Enum.Parse(typeof (T), value, true);
		}

		#endregion

		#region terminalEmulator1_ForwardDone

		private void terminalEmulator1_ForwardDone(object sender, EventArgs e)
		{
			Invoke((Action) delegate { forward_button.Enabled = true; });
		}

		#endregion

		#region terminalEmulator1_LastNumberevt

		private void terminalEmulator1_LastNumberevt(object sender, EventArgs e)
		{
			int number = (terminalEmulator1.LastNumber);
			_myRegistryBbs.Write("Start Number", number);
		}

		#endregion

		#region mail_button_Click

		private void mail_button_Click(object sender, EventArgs e)
		{
			var box = new Mail();
			box.ShowDialog();
		}

		#endregion

		#region private TelnetConnection

		//private static readonly Sql Sql = new Sql();

		private Boolean _bBeep = true;
		private Color _backgroundColor = Color.Black;
		private Color _textColor = Color.Yellow;

        private void button_read_Click(object sender, EventArgs e)
        {
            var box = new Read();
            box.ShowDialog();
        }

        private void button_personal_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To come soon");
        }
		//string ValidIpAddressRegex = @"^(0[0-7]{10,11}|0(x|X)[0-9a-fA-F]{8}|(\b4\d{8}[0-5]\b|\b[1-3]?\d{8}\d?\b)|((2[0-5][0-5]|1\d{2}|[1-9]\d?)|(0(x|X)[0-9a-fA-F]{2})|(0[0-7]{3}))(\.((2[0-5][0-5]|1\d{2}|\d\d?)|(0(x|X)[0-9a-fA-F]{2})|(0[0-7]{3}))){3})$";
		//string ValidHostnameRegex = @"^(([a-zA-Z]|[a-zA-Z][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z]|[A-Za-z][A-Za-z0-9\-]*[A-Za-z0-9])$";

		#endregion
	}

	#endregion
}