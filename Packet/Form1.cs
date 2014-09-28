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
using Utility.AnsiColor;
using Packet.Extensions;
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
        private TelnetConnection tc;
        ModifyRegistry myRegistry = new ModifyRegistry();
        AnsiColor myAnsiProject = new AnsiColor();
        ModifyFile myFiles = new ModifyFile();
        bool forward = false;
        string prompt = "";
        Boolean bBeep = true;
        string ValidIpAddressRegex = @"^(0[0-7]{10,11}|0(x|X)[0-9a-fA-F]{8}|(\b4\d{8}[0-5]\b|\b[1-3]?\d{8}\d?\b)|((2[0-5][0-5]|1\d{2}|[1-9]\d?)|(0(x|X)[0-9a-fA-F]{2})|(0[0-7]{3}))(\.((2[0-5][0-5]|1\d{2}|\d\d?)|(0(x|X)[0-9a-fA-F]{2})|(0[0-7]{3}))){3})$";
        string ValidHostnameRegex = @"^(([a-zA-Z]|[a-zA-Z][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z]|[A-Za-z][A-Za-z0-9\-]*[A-Za-z0-9])$";
        #endregion

        //---------------------------------------------------------------------------------------------------------
        //  public  void
        //---------------------------------------------------------------------------------------------------------
        #region public  void connect
        public void connect(string _var1)
        {
            string Var1 = _var1;
            string strDnsAddress;
            string port;

            try
            {
                if (myRegistry.Read(Var1 + "-Mode") == "Telnet")
                {
                    this.toolStripComboBox1.SelectedIndex = 0;
                    strDnsAddress = myRegistry.Read(Var1 + "-IP");
                    if (strDnsAddress.Length <= 3)
                    {
                        IP_Form2 box = new IP_Form2(Var1);
                        box.ShowDialog();
                        bbs_button.Enabled = true;
                        cluster_button.Enabled = true;
                        node_button.Enabled = true;
                    }
                    else
                    {
                        if (Regex.IsMatch(strDnsAddress, ValidIpAddressRegex))
                        {
                            this.textBox1.Text = "IP = " + strDnsAddress;
                        }
                        else if (Regex.IsMatch(strDnsAddress, ValidHostnameRegex))
                        {
                            IPHostEntry strAddress = Dns.GetHostEntry(strDnsAddress);
                            strDnsAddress = strAddress.AddressList[0].ToString();
                            this.textBox1.Text = strDnsAddress;
                        }
                        else
                        {
                            IP_Form2 box = new IP_Form2(Var1);
                            box.ShowDialog();
                            bbs_button.Enabled = true;
                            cluster_button.Enabled = true;
                            node_button.Enabled = true;
                        }
                        port = myRegistry.Read(Var1 + "-Port");
                        if (port.Length <= 1)
                        {
                            IP_Form2 box = new IP_Form2(Var1);
                            box.ShowDialog();
                            bbs_button.Enabled = true;
                            cluster_button.Enabled = true;
                            node_button.Enabled = true;
                        }
                        else
                        {
                            try
                            {
                                tc = new TelnetConnection(strDnsAddress, Convert.ToInt32(port));
                                backgroundWorker1.RunWorkerAsync();
                            }
                            catch
                            {
                                this.textBox1.Text = "Connection Errot to IP = " + strDnsAddress;
                            }
                        }
                    }
                }
                if (myRegistry.Read(Var1 + "-Mode") == "Com")
                {
                    this.toolStripComboBox1.SelectedIndex = 1;
                }
              

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            }


        }
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
            this.richTextBox1.Left = 20;
            this.richTextBox1.Top = 80;
            this.richTextBox1.Height = ((this.Height - 160) / 2);
            this.richTextBox2.Top = (this.richTextBox1.Top + this.richTextBox1.Size.Height + 20);
            this.richTextBox2.Height = this.richTextBox1.Size.Height;
            this.richTextBox2.Left = 20;
            this.richTextBox1.Width = (this.Width - 60);
            this.richTextBox2.Width = (this.Width - 60);

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

            this.ssh_button.Width = 90;
            this.ssh_button.Left = 580;
            this.ssh_button.Top = 40;
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
        // private void backgroundWorker1_DoWork
        //---------------------------------------------------------------------------------------------------------
        #region private void backgroundWorker1_DoWork
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string rd2;
            int i = 1;
            // while connected
            while (tc.IsConnected && prompt.Trim() != "Timeout !!")
            {
                string rd = tc.Read();

                if (rd != "") // stop text on screen jump
                {
                    rd = myAnsiProject.Colorize(rd);
                    string[] delimiters = { "{" };
                    string[] parts = rd.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length > 1)
                    {
                        for (int index = 1; index < parts.Length; index++)
                        {
                            string part = parts[index];
                            string temp = rd.Substring(rd.IndexOf(part) + part.Length);
                            foreach (string delimter in delimiters)
                            {
                                if (temp.IndexOf(delimter) == 0)
                                {
                                    string[] words = parts[index].Split('}');
                                    words[1] = words[1].Replace("\r\r", System.Environment.NewLine);
                                    richTextBox1.Invoke(new MethodInvoker(delegate() { richTextBox1.AppendText(words[1], Color.FromName(words[0])); }));
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (bBeep == true)
                        {
                            if (rd.Contains("\a"))
                                Console.Beep(3000, 1000);
                        }
                        if (forward == true)
                        {
                            rd2 = rd.Replace("\r", System.Environment.NewLine);
                            rd2 = rd2.TrimEnd('\r', '\n');
                            rd2 = rd2.TrimStart('\r', '\n');
                            string[] result = rd2.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                            string str_build = "";
                            foreach (string s in result)
                                if (Regex.IsMatch(s, @"^\d"))
                                {
                                    str_build = str_build + s + System.Environment.NewLine;
                                }
                            myFiles.Write(str_build);
                        }
                        this.richTextBox1.Invoke(new MethodInvoker(delegate() { this.richTextBox1.AppendText(rd); }));
                        i = i + 1;
                        string mystring = i.ToString();
                    }
                }
                System.Threading.Thread.Sleep(300);
            }
        }
        #endregion

        //-----------------------------------------------------------------------------------------------------------------------------------------------
        // private void richTextBox2_KeyDown_1
        // ritch Text to send
        //-----------------------------------------------------------------------------------------------------------------------------------------------   
        #region private void richTextBox2_KeyDown_1
        private void richTextBox2_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                // send client input to server
                prompt = richTextBox2.Text + "\r\n";
                tc.WriteLine(prompt);
                this.textBox1.Invoke(new MethodInvoker(delegate() { this.textBox1.Text = "Enter Pressed"; }));
                richTextBox1.Invoke(new MethodInvoker(delegate() { richTextBox1.AppendText(System.Environment.NewLine); }));
                richTextBox2.ResetText();
            }
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
        // private void toolStripComboBox1_SelectedIndexChanged
        //---------------------------------------------------------------------------------------------------------
        #region
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox1.SelectedIndex == 0)
            {
                myRegistry.Write("BBS-Mode", "Telnet");
                iPConfigToolStripMenuItem.Visible = true;
            }
            if (toolStripComboBox1.SelectedIndex == 1)
            {
                myRegistry.Write("BBS-Mode", "Com");
                iPConfigToolStripMenuItem.Visible = false;
            }
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // private void toolStripComboBox2_SelectedIndexChanged
        //---------------------------------------------------------------------------------------------------------
        #region private void toolStripComboBox2_SelectedIndexChanged
        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox2.SelectedIndex == 0)
            {
                myRegistry.Write("Cluster-Mode", "Telnet");
                clusterIPConfigToolStripMenuItem.Visible = true;
            }
            if (toolStripComboBox2.SelectedIndex == 1)
            {
                myRegistry.Write("Cluster-Mode", "Com");
                clusterIPConfigToolStripMenuItem.Visible = false;
            }
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // private void toolStripComboBox3_SelectedIndexChanged
        //---------------------------------------------------------------------------------------------------------
        #region private void toolStripComboBox3_SelectedIndexChanged
        private void toolStripComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox3.SelectedIndex == 0)
            {
                myRegistry.Write("Node-Mode", "Telnet");
                nodeIPConfigToolStripMenuItem.Visible = true;

            }
            if (toolStripComboBox3.SelectedIndex == 1)
            {
                myRegistry.Write("Node-Mode", "Com");
                nodeIPConfigToolStripMenuItem.Visible = false;
            }
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // private void toolStripComboBoxBeep_SelectedIndexChanged
        //---------------------------------------------------------------------------------------------------------
        #region private void toolStripComboBoxBeep_SelectedIndexChanged
        private void toolStripComboBoxBeep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBoxBeep.SelectedIndex == 0)
            {
                myRegistry.Write("Beep", "Yes");
                bBeep = true;
            }
            if (toolStripComboBoxBeep.SelectedIndex == 1)
            {
                myRegistry.Write("Beep", "No");
                bBeep = false;
            }
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // private void iPConfigToolStripMenuItem_Click
        //---------------------------------------------------------------------------------------------------------
        #region private void iPConfigToolStripMenuItem_Click
        private void iPConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IP_Form2 box = new IP_Form2("BBS");
            box.ShowDialog();
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // private void Form1_Resize_1
        //---------------------------------------------------------------------------------------------------------
        #region private void Form1_Resize_1
        private void Form1_Resize_1(object sender, EventArgs e)
        {
            this.richTextBox1.Left = 20;
            this.richTextBox1.Top = 80;
            this.richTextBox1.Height = ((this.Height - 160) / 2);
            this.richTextBox2.Top = (this.richTextBox1.Top + this.richTextBox1.Size.Height + 20);
            this.richTextBox2.Height = this.richTextBox1.Size.Height;
            this.richTextBox2.Left = 20;
            this.richTextBox1.Width = (this.Width - 60);
            this.richTextBox2.Width = (this.Width - 60);
        }
        #endregion
        //---------------------------------------------------------------------------------------------------------
        // private void button1_Click_1
        //---------------------------------------------------------------------------------------------------------
        #region private void button1_Click_1
        private void button1_Click_1(object sender, EventArgs e)
        {
            forward = true;
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // private void richTextBox1_TextChanged
        //---------------------------------------------------------------------------------------------------------
        #region private void richTextBox1_TextChanged
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // private void Form1_Load
        //---------------------------------------------------------------------------------------------------------
        #region
        private void Form1_Load(object sender, EventArgs e)
        {
            if (myRegistry.Read("BBS-Mode") == "Telnet")
            {
                this.toolStripComboBox1.SelectedIndex = 0;
                iPConfigToolStripMenuItem.Visible = true;
            }
            if (myRegistry.Read("BBS-Mode") == "Com")
            {
                this.toolStripComboBox1.SelectedIndex = 1;
                iPConfigToolStripMenuItem.Visible = false;
            }
            if (myRegistry.Read("Cluster-Mode") == "Telnet")
            {
                this.toolStripComboBox2.SelectedIndex = 0;
                clusterIPConfigToolStripMenuItem.Visible = true;
            }
            if (myRegistry.Read("Cluster-Mode") == "Com")
            {
                this.toolStripComboBox2.SelectedIndex = 1;
                clusterIPConfigToolStripMenuItem.Visible = false;
            }
            if (myRegistry.Read("Node-Mode") == "Telnet")
            {
                this.toolStripComboBox3.SelectedIndex = 0;
                nodeIPConfigToolStripMenuItem.Visible = true;
            }
            if (myRegistry.Read("Node-Mode") == "Com")
            {
                this.toolStripComboBox3.SelectedIndex = 1;
                nodeIPConfigToolStripMenuItem.Visible = false;
            }
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // private void button1_Click
        //---------------------------------------------------------------------------------------------------------
        #region private void button1_Click
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText(System.Environment.NewLine);
            bbs_button.Enabled = false;
            cluster_button.Enabled = false;
            node_button.Enabled = false;
            connect("BBS");
            this.forward_button.Enabled = true;
            this.richTextBox2.Focus();
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // private void button1_Click_2
        //---------------------------------------------------------------------------------------------------------
        #region private void button1_Click_2
        private void button1_Click_2(object sender, EventArgs e)
        {
            richTextBox1.AppendText(System.Environment.NewLine);
            bbs_button.Enabled = false;
            cluster_button.Enabled = false;
            node_button.Enabled = false;
            connect("Cluster");
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // private void node_button_Click
        //---------------------------------------------------------------------------------------------------------
        #region private void node_button_Click
        private void node_button_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText(System.Environment.NewLine);
            bbs_button.Enabled = false;
            cluster_button.Enabled = false;
            node_button.Enabled = false;
            connect("Node");
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // private void clusterIPConfigToolStripMenuItem_Click
        //---------------------------------------------------------------------------------------------------------
        #region private void clusterIPConfigToolStripMenuItem_Click
        private void clusterIPConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IP_Form2 box = new IP_Form2("Cluster");
            box.ShowDialog();
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // private void nodeIPConfigToolStripMenuItem_Click
        //---------------------------------------------------------------------------------------------------------
        #region private void nodeIPConfigToolStripMenuItem_Click
        private void nodeIPConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IP_Form2 box = new IP_Form2("Node");
            box.ShowDialog();
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // private void disconnect_button_Click
        //---------------------------------------------------------------------------------------------------------
        #region private void disconnect_button_Click
        private void disconnect_button_Click(object sender, EventArgs e)
        {

            try
            {
                tc.Telnet_Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            }


            bbs_button.Enabled = true;
            cluster_button.Enabled = true;
            node_button.Enabled = true;

        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        // pssh_button_Click
        //---------------------------------------------------------------------------------------------------------
        #region ssh_button_Click
        private void ssh_button_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText(System.Environment.NewLine);
            this.richTextBox1.AppendText("Need SSH libary still!!!", Color.Red);
        }
        #endregion

    }
    #endregion
}
    