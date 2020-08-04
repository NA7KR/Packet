//region Using Directive
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
//endregion
namespace Packet
{
    //---------------------------------------------------------------------------------------------------------
    //  partial class Form1
    //---------------------------------------------------------------------------------------------------------
    //region partial class Form1
    public partial class Main : Form
    {
        private StringBuilder m_Sb;
        private string m_FullPath;
        private bool _mBDirty;
        private FileSystemWatcher _fmWatcher;
        private static readonly FileSql MyFiles = new FileSql();
        private static readonly Sql Sql = new Sql();
        private readonly ModifyRegistry _myRegistryKeep = new ModifyRegistry();
        //region private void aboutToolStripMenuItem_Click
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var box = new AboutBox1();
            box.ShowDialog();
        }
        //endregion
        //region private void exitToolStripMenuItem1_Click
        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }
        //endregion
        //region toolStripComboBox1_SelectedIndexChanged
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (toolStripComboBox1.SelectedIndex)
            {
                case 0:
                    _myRegistry.Write("BBS-Mode", "Telnet");
                    iPConfigToolStripMenuItem.Text = "IP BBS Configure";
                    toolStripButton_bbs.Enabled = true;
                    break;
                case 1:
                    _myRegistry.Write("BBS-Mode", "Com");
                    iPConfigToolStripMenuItem.Text = "BBS Configure";
                    toolStripButton_bbs.Enabled = true;
                    break;
            }
        }
        //endregion
        //region private void toolStripComboBox2_SelectedIndexChanged
        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (toolStripComboBox2.SelectedIndex)
            {
                case 0:
                    _myRegistry.Write("Cluster-Mode", "Telnet");
                    clusterIPConfigToolStripMenuItem.Text = "IP Cluster Configure";
                    toolStripButton_cls.Enabled = true;
                    break;
                case 1:
                    _myRegistry.Write("Cluster-Mode", "Com");
                    clusterIPConfigToolStripMenuItem.Text = "Cluster Configure";
                    toolStripButton_cls.Enabled = true;
                    break;
            }
        }
        //endregion
        //region private void toolStripComboBox3_SelectedIndexChanged
        private void toolStripComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (toolStripComboBox3.SelectedIndex)
            {
                case 0:
                    _myRegistry.Write("Node-Mode", "Telnet");
                    nodeIPConfigToolStripMenuItem.Text = "IP Node Configure";
                    toolStripButton_Node.Enabled = true;
                    break;
                case 1:
                    _myRegistry.Write("Node-Mode", "Com");
                    nodeIPConfigToolStripMenuItem.Text = "Node Configure";
                    toolStripButton_Node.Enabled = true;
                    break;
            }
        }
        //endregion
        //region private void toolStripComboBoxBeep_SelectedIndexChanged
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
        //endregion
        //region toolStripComboBoxTXTC_SelectedIndexChanged
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
        //endregion
        //region toolStripComboBoxBGC
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
        //endregion
        //region private void iPConfigToolStripMenuItem_Click
        private void iPConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var box = new IpForm("BBS", (_myRegistry.Read("BBS-Mode")));
            box.ShowDialog();
        }
        //endregion
        //region private void Form1_Resize_1
        private void Form1_Resize_1(object sender, EventArgs e)
        {
            terminalEmulator1.Left = 10;
            terminalEmulator1.Top = 60;
            terminalEmulator1.Height = (Height - 140);
            terminalEmulator1.Width = (Width - 60);
        }
        //endregion
        //region button check
        private void button_check()
        {
            if (_myRegistry.Read("BBS-Mode") == "Telnet")
            {
                toolStripComboBox1.SelectedIndex = 0;
                iPConfigToolStripMenuItem.Text = "IP BBS Configure";
                toolStripButton_bbs.Enabled = true;
            }
            else if (_myRegistry.Read("BBS-Mode") == "Com")
            {
                toolStripComboBox1.SelectedIndex = 1;
                iPConfigToolStripMenuItem.Text = "BBS Configure";
                toolStripButton_bbs.Enabled = true;
            }
            else
            {
                toolStripButton_bbs.Enabled = false;
                iPConfigToolStripMenuItem.Text = "BBS Configure";
            }
            if (_myRegistry.Read("Cluster-Mode") == "Telnet")
            {
                toolStripComboBox2.SelectedIndex = 0;
                clusterIPConfigToolStripMenuItem.Text = "IP Cluster Configure";
                toolStripButton_cls.Enabled = true;
            }
            else if (_myRegistry.Read("Cluster-Mode") == "Com")
            {
                toolStripComboBox2.SelectedIndex = 1;
                clusterIPConfigToolStripMenuItem.Text = "Cluster Configure";
                toolStripButton_cls.Enabled = true;
            }
            else
            {
                toolStripButton_cls.Enabled = false;
                clusterIPConfigToolStripMenuItem.Text = "Cluster Configure";
            }
            if (_myRegistry.Read("Node-Mode") == "Telnet")
            {
                toolStripComboBox3.SelectedIndex = 0;
                toolStripButton_Node.Enabled = true;
            }
            else if (_myRegistry.Read("Node-Mode") == "Com")
            {
                toolStripComboBox3.SelectedIndex = 1;
                nodeIPConfigToolStripMenuItem.Text = "IP Node Configure";
                toolStripButton_Node.Enabled = true;
            }
            else
            {
                toolStripButton_Node.Enabled = false;
                nodeIPConfigToolStripMenuItem.Text = "Node Configure";
            }
            if (_myRegistrySsh.Read("Active") == "No")
            {
                toolStripButton_SSH.Enabled = false;
            }
            else
            {
                toolStripButton_SSH.Enabled = true;
            }
        }
        //endregion
        //region load
        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "";
            button_check();
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
            toolStripButton_Disconnect.Enabled = false;
            if (MyFiles.SelectMakeCustomQuery("Packet") == false)
            {
                Sql.WriteSqlCustomUpdate(1, "7+", "7+", "MSGSubject", "Y");
            }
            if (_myRegistrySsh.Read("Active") == "Yes")
            {
                toolStripButton_SSH.Visible = true;
            }
            else
                toolStripButton_SSH.Visible = false;
        }
        //endregion
        //region private void clusterIPConfigToolStripMenuItem_Click
        private void clusterIPConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var box = new IpForm("Cluster", (_myRegistry.Read("Cluster-Mode")));
            box.ShowDialog();
        }
        //endregion
        //region private void nodeIPConfigToolStripMenuItem_Click
        private void nodeIPConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var box = new IpForm("Node", (_myRegistry.Read("Node-Mode")));
            box.ShowDialog();
        }
        //endregion
        //region toolStripMenuItem1_Click SSH
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var box = new IpForm("SSH", "SSH");
            box.ShowDialog();
            if (_myRegistrySsh.Read("Active") == "No")
            {
                toolStripButton_SSH.Visible = false;
            }
            else
                toolStripButton_SSH.Visible = true;
        }
        //endregion
        //region disconnect
        private void Disconnected(object sender, EventArgs e)
        {
            // maybe called from terminiat in other threrad
            Invoke((Action)delegate { button_check(); });
        }
        //endregion
        //region toolStripMenu click show Com port configure
        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            var box = new ComForm();
            box.ShowDialog();
        }
        //endregion
        //region ParseEnum
        public static T ParseEnum<T>(string value)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
            catch (Exception)
            {
                return (T)Enum.Parse(typeof(T), null, false);
            }
        }
        //endregion
        //region terminalEmulator1_ForwardDone
        private void terminalEmulator1_ForwardDone(object sender, EventArgs e)
        {
            Invoke((Action)delegate { toolStripButton_fwd.Enabled = true; });
        }
        //endregion
        //region terminalEmulator1_LastNumberevt
        private void terminalEmulator1_LastNumberevt(object sender, EventArgs e)
        {
            var number = (terminalEmulator1.LastNumber);
            _myRegistryBbs.Write("Start Number", number);
        }
        //endregion
        //region BBS Click
        private void toolStripButton_BBS_Click(object sender, EventArgs e)
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
                    terminalEmulator1.BbsPrompt = _myRegistryBbs.BRead("Prompt");
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
                    terminalEmulator1.BbsPrompt = _myRegistryBbs.BRead("Prompt");
                    terminalEmulator1.SerialPort = _myRegistryCom.Read("Port");
                }
                terminalEmulator1.Connect();
                toolStripButton_Disconnect.Enabled = true;
                toolStripButton_bbs.Enabled = false;
                toolStripButton_cls.Enabled = false;
                toolStripButton_Node.Enabled = false;
                toolStripButton_SSH.Enabled = false;
                toolStripButton_fwd.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Configure BBS in Setup");
                toolStripButton_bbs.Enabled = true;
                toolStripButton_cls.Enabled = true;
                toolStripButton_Node.Enabled = true;
                toolStripButton_Disconnect.Enabled = false;
                if (_myRegistrySsh.Read("Active") == "No")
                {
                    toolStripButton_SSH.Enabled = false;
                }
                else
                    toolStripButton_SSH.Enabled = true;
            }
        }
        //endregion
        //region FWD
        private void toolStripButton_FWD_Click(object sender, EventArgs e)
        {
            StartThread();
            var idays = _myRegistryKeep.ReadDw("DaystoKeep");
            if (idays > 0)
            {
                Sql.Deletedays(idays);
            }
            var iqty = _myRegistryKeep.ReadDw("QTYtoKeep");
            if (iqty > 0)
            {
                Sql.DeleteCount(iqty);
            }
            terminalEmulator1.FileActive = true;
            toolStripButton_fwd.Enabled = false;
            toolStripButton_fwd.Text = "Forward active";
            terminalEmulator1.LastNumber = Convert.ToInt32(_myRegistryBbs.ReadDw("Start Number"));
            terminalEmulator1.Startforward();
        }
        //endregion
        //region Cluster Click
        private void toolStripButton_Cluster_Click(object sender, EventArgs e)
        {
            try
            {
                toolStripButton_bbs.Enabled = false;
                toolStripButton_cls.Enabled = false;
                toolStripButton_Node.Enabled = false;
                toolStripButton_SSH.Enabled = false;
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
                    terminalEmulator1.BbsPrompt = _myRegistryCluster.BRead("Prompt");
                    terminalEmulator1.UernamePrompt = _myRegistryCluster.BRead("UserNamePrompt");
                    terminalEmulator1.PasswordPrompt = _myRegistryCluster.BRead("PasswordPrompt");
                    terminalEmulator1.Connect();
                    toolStripButton_Disconnect.Enabled = true;
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
                toolStripButton_bbs.Enabled = true;
                toolStripButton_cls.Enabled = true;
                toolStripButton_Node.Enabled = true;
                toolStripButton_Disconnect.Enabled = false;
                if (_myRegistrySsh.Read("Active") == "No")
                {
                    toolStripButton_SSH.Enabled = false;
                }
                else
                    toolStripButton_SSH.Enabled = true;
            }
        }
        //endregion
        //region Node Click
        private void toolStripButton_Node_Click(object sender, EventArgs e)
        {
            try
            {
                toolStripButton_bbs.Enabled = false;
                toolStripButton_cls.Enabled = false;
                toolStripButton_Node.Enabled = false;
                toolStripButton_SSH.Enabled = false;
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
                    terminalEmulator1.BbsPrompt = _myRegistryNode.BRead("Prompt");
                    terminalEmulator1.UernamePrompt = _myRegistryNode.BRead("UserNamePrompt");
                    terminalEmulator1.PasswordPrompt = _myRegistryNode.BRead("PasswordPrompt");
                    terminalEmulator1.Connect();
                    toolStripButton_Disconnect.Enabled = true;
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
                toolStripButton_bbs.Enabled = true;
                toolStripButton_cls.Enabled = true;
                toolStripButton_Node.Enabled = true;
                toolStripButton_Disconnect.Enabled = false;
                if (_myRegistrySsh.Read("Active") == "No")
                {
                    toolStripButton_SSH.Enabled = false;
                }
                else
                    toolStripButton_SSH.Enabled = true;
            }
        }
        //endregion
        //region Disconnect Click
        private void toolStripButton_Disconnect_Click(object sender, EventArgs e)
        {
            // Cancel the asynchronous operation.
            FilewatchStop();
            button_check();
            toolStripButton_Disconnect.Enabled = false;
            terminalEmulator1.Closeconnection();
            terminalEmulator1.FileActive = false;
            toolStripButton_fwd.Enabled = false;
            toolStripButton_fwd.Text = "Forward";
        }
        //endregion
        //region SSH Click
        private void toolStripButton_SSH_Click(object sender, EventArgs e)
        {
            try
            {
                toolStripButton_SSH.Enabled = false;
                toolStripButton_bbs.Enabled = false;
                toolStripButton_cls.Enabled = false;
                toolStripButton_Node.Enabled = false;
                toolStripButton_Disconnect.Enabled = true;
                terminalEmulator1.ConnectionType = TerminalEmulator.ConnectionTypes.Ssh2;
                terminalEmulator1.Port = Convert.ToInt32(_myRegistrySsh.Read("Port"));
                terminalEmulator1.Hostname = _myRegistrySsh.Read("IP");
                terminalEmulator1.Username = _myRegistrySsh.Read("UserName");
                terminalEmulator1.Password = _myEncrypt.Decrypt(_myRegistrySsh.Read("Password"));
                terminalEmulator1.Connect();
            }
            catch (Exception)
            {
                MessageBox.Show("Configure SSH in Setup");
                toolStripButton_bbs.Enabled = true;
                toolStripButton_cls.Enabled = true;
                toolStripButton_Node.Enabled = true;
                toolStripButton_Disconnect.Enabled = false;
                if (_myRegistrySsh.Read("Active") == "No")
                {
                    toolStripButton_SSH.Enabled = false;
                }
                else
                    toolStripButton_SSH.Enabled = true;
            }
        }
        //endregion
        //region MailConfig
        private void toolStripButton_MailConfig_Click(object sender, EventArgs e)
        {
            var box = new Mail();
            box.ShowDialog();
        }
        //endregion
        //region  Read Mail
        private void toolStripButton_ReadMail_Click(object sender, EventArgs e)
        {
            var box = new Read();
            box.ShowDialog();
        }
        //endregion
        //region Personal Mail
        private void toolStripButton_PersonalMail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To come soon");
        }
        //endregion
        //region 7plus click
        private void toolStripButton_7plus_Click(object sender, EventArgs e)
        {
            var box = new PlusFrm();
            box.ShowDialog();
        }
        //endregion
        //region Form1
        public const string DsnName = "DSN=Packet";
        public Main()
        {
            InitializeComponent();
            m_Sb = new StringBuilder();
            _mBDirty = false;
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
            toolStripButton_bbs.Enabled = true;
            toolStripButton_fwd.Enabled = false;
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
            terminalEmulator1.DnsName = DsnName;
        }
        protected override sealed void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }
        //endregion
        //region private TelnetConnection
        //private static read only Sql Sql = new Sql();
        private bool _bBeep = true;
        private Color _backgroundColor = Color.Black;
        private Color _textColor = Color.Yellow;
        //endregion
        //region start thread
        private void StartThread()
        {
            // Initialize the object that the background worker calls.
            var path = Directory.GetCurrentDirectory() + @"\Data\7plus" + @"\";
            if (!Directory.Exists(path))
            {
                // Try to create the directory.
                Directory.CreateDirectory(path);
            }
            var outpath = Directory.GetCurrentDirectory() + @"\Data\Out" + @"\";
            if (!Directory.Exists(outpath))
            {
                // Try to create the directory.
                Directory.CreateDirectory(outpath);
            }
            var inpath = Directory.GetCurrentDirectory() + @"\Data\IN" + @"\";
            if (!Directory.Exists(inpath))
            {
                // Try to create the directory.
                Directory.CreateDirectory(inpath);
            }
            var logpath = Directory.GetCurrentDirectory() + @"\Data\Log" + @"\";
            if (!Directory.Exists(logpath))
            {
                // Try to create the directory.
                Directory.CreateDirectory(logpath);
            }
            var donepath = Directory.GetCurrentDirectory() + @"\Data\7plus\Done" + @"\";
            if (!Directory.Exists(donepath))
            {
                // Try to create the directory.
                Directory.CreateDirectory(donepath);
            }
            var lockpath = Directory.GetCurrentDirectory() + @"\Data\Lock" + @"\";
            if (!Directory.Exists(lockpath))
            {
                // Try to create the directory.
                Directory.CreateDirectory(lockpath);
            }
            CreateFileWatcher(path);
        }
        //endregion
        //region tool Strip Button
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            StartThread();
        }
        //endregion
    }
    //endregion
}