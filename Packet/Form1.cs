#region Using Directive

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility.ModifyFile;
using Utility.ModifyRegistry;
using Utility.Encrypting;
#endregion

namespace Packet
{
    //---------------------------------------------------------------------------------------------------------
    //  partial class Form1
    //---------------------------------------------------------------------------------------------------------

    #region partial class Form1
    public partial class Form1 : Form
    {

        //---------------------------------------------------------------------------------------------------------
        //  private TelnetConnection
        //---------------------------------------------------------------------------------------------------------
        #region private TelnetConnection

        ModifyRegistry myRegistry = new ModifyRegistry();
        ModifyRegistry myRegistryCom = new ModifyRegistry();
        ModifyRegistry myRegistryBBS = new ModifyRegistry();
        ModifyRegistry myRegistryNode = new ModifyRegistry();
        ModifyRegistry myRegistryCluster = new ModifyRegistry();
        ModifyRegistry myRegistrySSH = new ModifyRegistry();
        Encrypting myEncrypt = new Encrypting();
        //ModifyFile myFiles = new ModifyFile();
        Color textColor = Color.Yellow;
        Color backgroundColor = Color.Black;
        Boolean bBeep = true;
        //string ValidIpAddressRegex = @"^(0[0-7]{10,11}|0(x|X)[0-9a-fA-F]{8}|(\b4\d{8}[0-5]\b|\b[1-3]?\d{8}\d?\b)|((2[0-5][0-5]|1\d{2}|[1-9]\d?)|(0(x|X)[0-9a-fA-F]{2})|(0[0-7]{3}))(\.((2[0-5][0-5]|1\d{2}|\d\d?)|(0(x|X)[0-9a-fA-F]{2})|(0[0-7]{3}))){3})$";
        //string ValidHostnameRegex = @"^(([a-zA-Z]|[a-zA-Z][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z]|[A-Za-z][A-Za-z0-9\-]*[A-Za-z0-9])$";
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // Form1
        //---------------------------------------------------------------------------------------------------------
        #region Form1
        public Form1()
        {
            InitializeComponent();
            myRegistry.SubKey = "SOFTWARE\\NA7KR\\Packet";
            myRegistry.ShowError = true;
            myRegistryCom.SubKey = "SOFTWARE\\NA7KR\\Packet\\Port";
            myRegistryBBS.SubKey = "SOFTWARE\\NA7KR\\Packet\\BBS";
            myRegistryNode.SubKey = "SOFTWARE\\NA7KR\\Packet\\Node";
            myRegistryCluster.SubKey = "SOFTWARE\\NA7KR\\Packet\\Cluster";
            myRegistrySSH.SubKey = "SOFTWARE\\NA7KR\\Packet\\SSH";
            myRegistryCom.ShowError = true;
            myRegistryBBS.ShowError = true;
            myRegistryNode.ShowError = true;
            myRegistryCluster.ShowError = true;
            myRegistrySSH.ShowError = true;
            myRegistry.Write("Packet", Application.ProductVersion);
            this.bbs_button.Enabled = true;
            this.forward_button.Enabled = false;
            this.toolStripComboBox1.Items.Clear();
            this.toolStripComboBox1.Items.Add("Telnet");
            this.toolStripComboBox1.Items.Add("Com Port");
            this.toolStripComboBox2.Items.Clear();
            this.toolStripComboBox2.Items.Add("Telnet");
            this.toolStripComboBox2.Items.Add("Com Port");
            this.toolStripComboBox3.Items.Clear();
            this.toolStripComboBox3.Items.Add("Telnet");
            this.toolStripComboBox3.Items.Add("Com Port");
            this.toolStripComboBoxBeep.Items.Add("Yes");
            this.toolStripComboBoxBeep.Items.Add("No");
            this.toolStripComboBoxTXTC.Items.Add("Black");
            this.toolStripComboBoxTXTC.Items.Add("Red");
            this.toolStripComboBoxTXTC.Items.Add("Green");
            this.toolStripComboBoxTXTC.Items.Add("Yellow");
            this.toolStripComboBoxTXTC.Items.Add("Blue");
            this.toolStripComboBoxTXTC.Items.Add("Magenta");
            this.toolStripComboBoxTXTC.Items.Add("Cyan");
            this.toolStripComboBoxTXTC.Items.Add("White");

            this.toolStripComboBoxBGC.Items.Add("Black");
            this.toolStripComboBoxBGC.Items.Add("Red");
            this.toolStripComboBoxBGC.Items.Add("Green");
            this.toolStripComboBoxBGC.Items.Add("Yellow");
            this.toolStripComboBoxBGC.Items.Add("Blue");
            this.toolStripComboBoxBGC.Items.Add("Magenta");
            this.toolStripComboBoxBGC.Items.Add("Cyan");
            this.toolStripComboBoxBGC.Items.Add("White");

            this.bbs_button.Width = 90;
            this.bbs_button.Left = 20;
            this.bbs_button.Top = 40;

            this.forward_button.Width = 90;
            this.forward_button.Left = 130;
            this.forward_button.Top = 40;

            this.cluster_button.Width = 90;
            this.cluster_button.Left = 250;
            this.cluster_button.Top = 40;

            this.node_button.Width = 90;
            this.node_button.Left = 360;
            this.node_button.Top = 40;

            this.disconnect_button.Width = 90;
            this.disconnect_button.Left = 470;
            this.disconnect_button.Top = 40;
            this.disconnect_button.Enabled = false;
            this.ssh_button.Width = 90;

            this.ssh_button.Left = 580;
            this.ssh_button.Top = 40;
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // private void aboutToolStripMenuItem_Click
        //---------------------------------------------------------------------------------------------------------
        #region private void aboutToolStripMenuItem_Click
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 box = new AboutBox1();
            box.ShowDialog();
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // private void exitToolStripMenuItem1_Click
        // Progran Close
        //---------------------------------------------------------------------------------------------------------
        #region private void exitToolStripMenuItem1_Click
        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // toolStripComboBox1 BBS
        //---------------------------------------------------------------------------------------------------------
        #region
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (toolStripComboBox1.SelectedIndex)
            {
                case 0:
                    myRegistry.Write("BBS-Mode", "Telnet");
                    iPConfigToolStripMenuItem.Text = "IP BBS Config";
                    bbs_button.Enabled = true;
                    break;
                case 1:
                    myRegistry.Write("BBS-Mode", "Com");
                    iPConfigToolStripMenuItem.Text = "BBS Config";
                    bbs_button.Enabled = true;
                    break;
            }
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // toolStripComboBox2 Cluster
        //---------------------------------------------------------------------------------------------------------
        #region private void toolStripComboBox2_SelectedIndexChanged
        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (toolStripComboBox2.SelectedIndex)
            {
                case 0:
                    myRegistry.Write("Cluster-Mode", "Telnet");
                    clusterIPConfigToolStripMenuItem.Text = "IP Cluster Config";
                    cluster_button.Enabled = true;
                    break;
                case 1:
                    myRegistry.Write("Cluster-Mode", "Com");
                    clusterIPConfigToolStripMenuItem.Text = "Cluster Config";
                    cluster_button.Enabled = true;
                    break;
            }
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // toolStripComboBox3 Node
        //---------------------------------------------------------------------------------------------------------
        #region private void toolStripComboBox3_SelectedIndexChanged
        private void toolStripComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (toolStripComboBox3.SelectedIndex)
            {
                case 0:
                    myRegistry.Write("Node-Mode", "Telnet");
                    nodeIPConfigToolStripMenuItem.Text = "IP Node Config";
                    node_button.Enabled = true;
                    break;

                case 1:
                    myRegistry.Write("Node-Mode", "Com");
                    nodeIPConfigToolStripMenuItem.Text = "Node Config";
                    node_button.Enabled = true;
                    break;
            }
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // private void toolStripComboBoxBeep_SelectedIndexChanged
        //---------------------------------------------------------------------------------------------------------
        #region private void toolStripComboBoxBeep_SelectedIndexChanged
        private void toolStripComboBoxBeep_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (toolStripComboBoxBeep.SelectedIndex)
            {
                case 0:
                    myRegistry.Write("Beep", "Yes");
                    bBeep = true;
                    break;
                case 1:
                    myRegistry.Write("Beep", "No");
                    bBeep = false;
                    break;
            }
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // toolStripComboBoxTXTC_SelectedIndexChanged
        //---------------------------------------------------------------------------------------------------------
        #region toolStripComboBoxTXTC_SelectedIndexChanged
        private void toolStripComboBoxTXTC_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (toolStripComboBoxTXTC.SelectedIndex)
            {
                case 0:
                    myRegistry.Write("Color Text", "Black");
                    textColor = Color.Black;
                    break;
                case 1:
                    myRegistry.Write("Color Text", "Red");
                    textColor = Color.Red;
                    break;
                case 2:
                    myRegistry.Write("Color Text", "Green");
                    textColor = Color.Green;
                    break;
                case 3:
                    myRegistry.Write("Color Text", "Yellow");
                    textColor = Color.Yellow;
                    break;
                case 4:
                    myRegistry.Write("Color Text", "Blue");
                    textColor = Color.Blue;
                    break;
                case 5:
                    myRegistry.Write("Color Text", "Magenta");
                    textColor = Color.Magenta;
                    break;
                case 6:
                    myRegistry.Write("Color Text", "Cyan");
                    textColor = Color.Cyan;
                    break;
                case 7:
                    myRegistry.Write("Color Text", "White");
                    textColor = Color.White;
                    break;
            }
            terminalEmulator1.ForeColor = textColor;

        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // toolStripComboBoxBGC
        //---------------------------------------------------------------------------------------------------------
        #region toolStripComboBoxBGC
        private void toolStripComboBoxBGC_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (toolStripComboBoxBGC.SelectedIndex)
            {
                case 0:
                    myRegistry.Write("Color Background", "Black");
                    backgroundColor = Color.Black;
                    break;
                case 1:
                    myRegistry.Write("Color Background", "Red");
                    backgroundColor = Color.Red;
                    break;
                case 2:
                    myRegistry.Write("Color Background", "Green");
                    backgroundColor = Color.Green;
                    break;
                case 3:
                    myRegistry.Write("Color Background", "Yellow");
                    backgroundColor = Color.Yellow;
                    break;
                case 4:
                    myRegistry.Write("Color Background", "Blue");
                    backgroundColor = Color.Blue;
                    break;
                case 5:
                    myRegistry.Write("Color Background", "Magenta");
                    backgroundColor = Color.Magenta;
                    break;
                case 6:
                    myRegistry.Write("Color Background", "Cyan");
                    backgroundColor = Color.Cyan;
                    break;
                case 7:
                    myRegistry.Write("Color Background", "White");
                    backgroundColor = Color.White;
                    break;
            }
            terminalEmulator1.BackColor = backgroundColor;
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // private void iPConfigToolStripMenuItem_Click
        //---------------------------------------------------------------------------------------------------------
        #region private void iPConfigToolStripMenuItem_Click
        private void iPConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IP_Form box = new IP_Form("BBS",(myRegistry.Read("BBS-Mode")));
            box.ShowDialog();
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // private void Form1_Resize_1
        //---------------------------------------------------------------------------------------------------------
        #region private void Form1_Resize_1
        private void Form1_Resize_1(object sender, EventArgs e)
        {
            this.terminalEmulator1.Left = 20;
            this.terminalEmulator1.Top = 80;
            this.terminalEmulator1.Height = (this.Height - 20);
            this.terminalEmulator1.Width = (this.Width - 60);

        }
        #endregion
        //---------------------------------------------------------------------------------------------------------
        // forward_button_Click
        //---------------------------------------------------------------------------------------------------------
        #region forward_button_Click
        private void forward_button_Click(object sender, EventArgs e)
        {
            terminalEmulator1.FileActive = true;
            forward_button.Enabled = false;
            forward_button.Text = "Forward active";
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // private void Form1_Load
        //---------------------------------------------------------------------------------------------------------
        #region load
        private void Form1_Load(object sender, EventArgs e)
        {
            if (myRegistry.Read("BBS-Mode") == "Telnet")
            {
                this.toolStripComboBox1.SelectedIndex = 0;
                iPConfigToolStripMenuItem.Text = "IP BBS Config";
            }
            else if (myRegistry.Read("BBS-Mode") == "Com")
            {
                this.toolStripComboBox1.SelectedIndex = 1;
                iPConfigToolStripMenuItem.Text = "BBS Config";
            }
            else
            {
                bbs_button.Enabled = false;
                iPConfigToolStripMenuItem.Text = "BBS Config";
            }

            if (myRegistry.Read("Cluster-Mode") == "Telnet")
            {
                this.toolStripComboBox2.SelectedIndex = 0;
                clusterIPConfigToolStripMenuItem.Text = "IP Cluster Config";
            }
            else if (myRegistry.Read("Cluster-Mode") == "Com")
            {
                this.toolStripComboBox2.SelectedIndex = 1;
                clusterIPConfigToolStripMenuItem.Text = "Cluster Config";
            }
            else
            {
                cluster_button.Enabled = false;
                clusterIPConfigToolStripMenuItem.Text = "Cluster Config";
            }

            if (myRegistry.Read("Node-Mode") == "Telnet")
            {
                this.toolStripComboBox3.SelectedIndex = 0;

            }
            else if (myRegistry.Read("Node-Mode") == "Com")
            {
                this.toolStripComboBox3.SelectedIndex = 1;
                nodeIPConfigToolStripMenuItem.Text = "IP Node Config";
            }
            else
            {
                node_button.Enabled = false;
                nodeIPConfigToolStripMenuItem.Text = "Node Config";
            }
            if (myRegistry.Read("Beep") == "Yes")
            {
                bBeep = true;
                this.toolStripComboBoxBeep.SelectedIndex = 0;
            }
            if (myRegistry.Read("Beep") == "No")
            {
                bBeep = false;
                this.toolStripComboBoxBeep.SelectedIndex = 1;
            }
            string myREG = myRegistry.Read("Color Text");
            switch (myREG)
            {
                case "Black":
                    this.toolStripComboBoxTXTC.SelectedIndex = 0;
                    textColor = Color.Black;
                    break;
                case "Red":
                    this.toolStripComboBoxTXTC.SelectedIndex = 1;
                    textColor = Color.Red;
                    break;
                case "Green":
                    this.toolStripComboBoxTXTC.SelectedIndex = 2;
                    textColor = Color.Green;
                    break;
                case "Yellow":
                    this.toolStripComboBoxTXTC.SelectedIndex = 3;
                    textColor = Color.Yellow;
                    break;
                case "Blue":
                    this.toolStripComboBoxTXTC.SelectedIndex = 4;
                    textColor = Color.Blue;
                    break;
                case "Magenta":
                    this.toolStripComboBoxTXTC.SelectedIndex = 5;
                    textColor = Color.Magenta;
                    break;
                case "Cyan":
                    this.toolStripComboBoxTXTC.SelectedIndex = 6;
                    textColor = Color.Cyan;
                    break;
                case "White":
                    this.toolStripComboBoxTXTC.SelectedIndex = 7;
                    textColor = Color.White;
                    break;
                default:
                    textColor = Color.White;
                    break;
            }
            terminalEmulator1.ForeColor = textColor;


            string myREG2 = myRegistry.Read("Color Background");
            switch (myREG2)
            {
                case "Black":
                    this.toolStripComboBoxBGC.SelectedIndex = 0;
                    backgroundColor = Color.Black;
                    break;
                case "Red":
                    this.toolStripComboBoxBGC.SelectedIndex = 1;
                    backgroundColor = Color.Red;
                    break;
                case "Green":
                    this.toolStripComboBoxBGC.SelectedIndex = 2;
                    backgroundColor = Color.Green;
                    break;
                case "Yellow":
                    this.toolStripComboBoxBGC.SelectedIndex = 3;
                    backgroundColor = Color.Yellow;
                    break;
                case "Blue":
                    this.toolStripComboBoxBGC.SelectedIndex = 4;
                    backgroundColor = Color.Blue;
                    break;
                case "Magenta":
                    this.toolStripComboBoxBGC.SelectedIndex = 5;
                    backgroundColor = Color.Magenta;
                    break;
                case "Cyan":
                    this.toolStripComboBoxBGC.SelectedIndex = 6;
                    backgroundColor = Color.Cyan;
                    break;
                case "White":
                    this.toolStripComboBoxBGC.SelectedIndex = 7;
                    backgroundColor = Color.White;
                    break;
                default:
                    backgroundColor = Color.Black;
                    break;
            }
            terminalEmulator1.BackColor = backgroundColor;
            if (myRegistrySSH.Read("Echo") == "Yes")
            {
                ssh_button.Visible = true;
            }
            else
                ssh_button.Visible = false;



        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // connect bbs
        //---------------------------------------------------------------------------------------------------------
        #region connect bbs
        private void bbs_button_Click(object sender, EventArgs e)
        {
            try
            {

                if (myRegistry.Read("BBS-Mode") == "Telnet")
                {

                    if (myRegistryBBS.Read("Echo") == "Yes")
                    {
                        this.terminalEmulator1.LocalEcho = true;
                    }
                    else
                    {
                        this.terminalEmulator1.LocalEcho = false;
                    }
                    this.terminalEmulator1.Port = Convert.ToInt32(myRegistryBBS.Read("Port"));
                    this.terminalEmulator1.Hostname = myRegistryBBS.Read("IP");
                    this.terminalEmulator1.Username = myRegistryBBS.Read("CallSign");
                    this.terminalEmulator1.Password = myEncrypt.Decrypt(myRegistryBBS.Read("Password"));
                    this.terminalEmulator1.ConnectionType = PacketSoftware.TerminalEmulator.ConnectionTypes.Telnet;
                    this.terminalEmulator1.BBSPrompt = myRegistryBBS.BRead("Prompt");
                    this.terminalEmulator1.UernamePrompt = myRegistryBBS.BRead("UserNamePrompt");
                    this.terminalEmulator1.passwordPrompt = myRegistryBBS.BRead("PasswordPrompt");
                }
                else
                {
                    this.terminalEmulator1.BaudRateType = ParseEnum<PacketSoftware.TerminalEmulator.BaudRateTypes>("Baud_" + myRegistryCom.Read("Baud"));
                    this.terminalEmulator1.DataBitsType = ParseEnum<PacketSoftware.TerminalEmulator.DataBitsTypes>("Data_Bits_" + myRegistryCom.Read("Data Bits"));
                    this.terminalEmulator1.StopBitsType = ParseEnum<PacketSoftware.TerminalEmulator.StopBitsTypes>(myRegistryCom.Read("Stop Bits"));
                    this.terminalEmulator1.ParityType = ParseEnum<PacketSoftware.TerminalEmulator.ParityTypes>(myRegistryCom.Read("Parity"));
                    this.terminalEmulator1.FlowType = ParseEnum<PacketSoftware.TerminalEmulator.FlowTypes>(myRegistryCom.Read("Flow"));
                    this.terminalEmulator1.ConnectionType = PacketSoftware.TerminalEmulator.ConnectionTypes.COM;
                    this.terminalEmulator1.SerialPort = myRegistryCom.Read("Port");
                }
                 
                this.terminalEmulator1.Connect();
                this.disconnect_button.Enabled = true;
                bbs_button.Enabled = false;
                cluster_button.Enabled = false;
                node_button.Enabled = false;
                ssh_button.Enabled = false;
                forward_button.Enabled = true;

            }
            catch
            {
                MessageBox.Show("Com Port or Telnet Error", "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // cluster_button_Click
        //---------------------------------------------------------------------------------------------------------
        #region connect_cluster
        private void cluster_button_Click(object sender, EventArgs e)
        {
            bbs_button.Enabled = false;
            cluster_button.Enabled = false;
            node_button.Enabled = false;
            ssh_button.Enabled = false;
            if (myRegistry.Read("Cluster-Mode") == "Telnet")
            {
                if (myRegistryCluster.Read("Echo") == "Yes")
                {
                    this.terminalEmulator1.LocalEcho = true;
                }
                else
                {
                    this.terminalEmulator1.LocalEcho = false;
                }
                this.terminalEmulator1.Beep = bBeep;
                this.terminalEmulator1.Port = Convert.ToInt32(myRegistryCluster.Read("Port"));
                this.terminalEmulator1.Hostname = myRegistryCluster.Read("IP");
                this.terminalEmulator1.Username = myRegistryCluster.Read("CallSign");
                this.terminalEmulator1.Password = myEncrypt.Decrypt(myRegistryCluster.Read("Password"));
                this.terminalEmulator1.ConnectionType = PacketSoftware.TerminalEmulator.ConnectionTypes.Telnet;
                this.terminalEmulator1.BBSPrompt = myRegistryCluster.BRead("Prompt");
                this.terminalEmulator1.UernamePrompt = myRegistryCluster.BRead("UserNamePrompt");
                this.terminalEmulator1.passwordPrompt = myRegistryCluster.BRead("PasswordPrompt");
                this.terminalEmulator1.Connect();
                this.disconnect_button.Enabled = true;
            }
            else
            {
                this.terminalEmulator1.BaudRateType = ParseEnum<PacketSoftware.TerminalEmulator.BaudRateTypes>("Baud_" + myRegistryCom.Read("Baud"));
                this.terminalEmulator1.DataBitsType = ParseEnum<PacketSoftware.TerminalEmulator.DataBitsTypes>("Data_Bits_" + myRegistryCom.Read("Data Bits"));
                this.terminalEmulator1.StopBitsType = ParseEnum<PacketSoftware.TerminalEmulator.StopBitsTypes>(myRegistryCom.Read("Stop Bits"));
                this.terminalEmulator1.ParityType = ParseEnum<PacketSoftware.TerminalEmulator.ParityTypes>(myRegistryCom.Read("Parity"));
                this.terminalEmulator1.FlowType = ParseEnum<PacketSoftware.TerminalEmulator.FlowTypes>(myRegistryCom.Read("Flow"));
                this.terminalEmulator1.ConnectionType = PacketSoftware.TerminalEmulator.ConnectionTypes.COM;
                this.terminalEmulator1.SerialPort = myRegistryCom.Read("Port");
            }
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // node_button_Click
        //---------------------------------------------------------------------------------------------------------
        #region private void node_button_Click
        private void node_button_Click(object sender, EventArgs e)
        {
            bbs_button.Enabled = false;
            cluster_button.Enabled = false;
            node_button.Enabled = false;
            ssh_button.Enabled = false;
            if (myRegistry.Read("Node-Mode") == "Telnet")
            {
                if (myRegistryNode.Read("Echo") == "Yes")
                {
                    this.terminalEmulator1.LocalEcho = true;
                }
                else
                {
                    this.terminalEmulator1.LocalEcho = false;
                }
                this.terminalEmulator1.Port = Convert.ToInt32(myRegistryNode.Read("Port"));
                this.terminalEmulator1.Hostname = myRegistryNode.Read("IP");
                this.terminalEmulator1.Username = myRegistryNode.Read("CallSign");
                this.terminalEmulator1.Password = myEncrypt.Decrypt(myRegistryNode.Read("Password"));
                this.terminalEmulator1.ConnectionType = PacketSoftware.TerminalEmulator.ConnectionTypes.Telnet;
                this.terminalEmulator1.BBSPrompt = myRegistryNode.BRead("Prompt");
                this.terminalEmulator1.UernamePrompt = myRegistryNode.BRead("UserNamePrompt");
                this.terminalEmulator1.passwordPrompt = myRegistryNode.BRead("PasswordPrompt");
                this.terminalEmulator1.Connect();
                this.disconnect_button.Enabled = true;
            }
            else
            {
                this.terminalEmulator1.BaudRateType = ParseEnum<PacketSoftware.TerminalEmulator.BaudRateTypes>("Baud_" + myRegistryCom.Read("Baud"));
                this.terminalEmulator1.DataBitsType = ParseEnum<PacketSoftware.TerminalEmulator.DataBitsTypes>("Data_Bits_" + myRegistryCom.Read("Data Bits"));
                this.terminalEmulator1.StopBitsType = ParseEnum<PacketSoftware.TerminalEmulator.StopBitsTypes>(myRegistryCom.Read("Stop Bits"));
                this.terminalEmulator1.ParityType = ParseEnum<PacketSoftware.TerminalEmulator.ParityTypes>(myRegistryCom.Read("Parity"));
                this.terminalEmulator1.FlowType = ParseEnum<PacketSoftware.TerminalEmulator.FlowTypes>(myRegistryCom.Read("Flow"));
                this.terminalEmulator1.ConnectionType = PacketSoftware.TerminalEmulator.ConnectionTypes.COM;
                this.terminalEmulator1.SerialPort = myRegistryCom.Read("Port");
            }
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // private void clusterIPConfigToolStripMenuItem_Click
        //---------------------------------------------------------------------------------------------------------
        #region private void clusterIPConfigToolStripMenuItem_Click
        private void clusterIPConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IP_Form box = new IP_Form("Cluster", (myRegistry.Read("Cluster-Mode")));
            box.ShowDialog();
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // private void nodeIPConfigToolStripMenuItem_Click
        //---------------------------------------------------------------------------------------------------------
        #region private void nodeIPConfigToolStripMenuItem_Click
        private void nodeIPConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IP_Form box = new IP_Form("Node",(myRegistry.Read("Node-Mode")));
            box.ShowDialog();
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // private void disconnect_button_Click
        //---------------------------------------------------------------------------------------------------------
        #region private void disconnect_button_Click
        private void disconnect_button_Click(object sender, EventArgs e)
        {
            bbs_button.Enabled = true;
            cluster_button.Enabled = true;
            node_button.Enabled = true;
            disconnect_button.Enabled = false;
            ssh_button.Enabled = true;
            terminalEmulator1.closeconnection();
            terminalEmulator1.FileActive = false;
            forward_button.Enabled = false;
            forward_button.Text = "Forward";
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // ssh_button_Click
        //---------------------------------------------------------------------------------------------------------
        #region ssh_button_Click
        private void ssh_button_Click(object sender, EventArgs e)
        {
            ssh_button.Enabled = false;
            bbs_button.Enabled = false;
            cluster_button.Enabled = false;
            node_button.Enabled = false;
            disconnect_button.Enabled = true;
            this.terminalEmulator1.ConnectionType = PacketSoftware.TerminalEmulator.ConnectionTypes.SSH2;
            this.terminalEmulator1.Port = Convert.ToInt32(myRegistrySSH.Read("Port"));
            this.terminalEmulator1.Hostname = myRegistrySSH.Read("IP");
            this.terminalEmulator1.Username = myRegistrySSH.Read("CallSign");
            this.terminalEmulator1.Password = myEncrypt.Decrypt(myRegistrySSH.Read("Password"));
            this.terminalEmulator1.Connect();
        }
        #endregion

        #region toolStripMenuItem1_Click SSH
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            IP_Form box = new IP_Form("SSH","SSH");
            box.ShowDialog();
            if (myRegistrySSH.Read("Echo") == "No")
            {
                ssh_button.Visible = false;
            }
            else
                ssh_button.Visible = true;
        }
        #endregion

        #region disconnect
        private void disconnected(object sender, EventArgs e)
        {
            base.Invoke((Action)delegate { bbs_button.Enabled = true; });
            base.Invoke((Action)delegate { cluster_button.Enabled = true; });
            base.Invoke((Action)delegate { node_button.Enabled = true; });
            base.Invoke((Action)delegate { disconnect_button.Enabled = false; });
        }
        #endregion

        #region toolStripMenu click show Com port config
        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            Com_Form box = new Com_Form();
            box.ShowDialog();
        }
        #endregion

        #region ParseEnum
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        #endregion

        private void terminalEmulator1_ForwardDone(object sender, EventArgs e)
        {
            base.Invoke((Action)delegate { forward_button.Enabled = true; });

        }
    }
    #endregion
}
