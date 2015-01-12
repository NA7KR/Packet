using System.ComponentModel;
using System.Windows.Forms;
using PacketComs;

namespace Packet
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;
        
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.bbs_button = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.telnetComToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.iPConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clusterTelnetComToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox2 = new System.Windows.Forms.ToolStripComboBox();
            this.clusterIPConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nodeTelnetComToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox3 = new System.Windows.Forms.ToolStripComboBox();
            this.nodeIPConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCom = new System.Windows.Forms.ToolStripMenuItem();
            this.SSHConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxBeep = new System.Windows.Forms.ToolStripComboBox();
            this.colourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundColourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxBGC = new System.Windows.Forms.ToolStripComboBox();
            this.textColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxTXTC = new System.Windows.Forms.ToolStripComboBox();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forward_button = new System.Windows.Forms.Button();
            this.cluster_button = new System.Windows.Forms.Button();
            this.node_button = new System.Windows.Forms.Button();
            this.disconnect_button = new System.Windows.Forms.Button();
            this.ssh_button = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.mail_button = new System.Windows.Forms.Button();
            this.button_read = new System.Windows.Forms.Button();
            this.terminalEmulator1 = new PacketComs.TerminalEmulator();
            this.button_personal = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bbs_button
            // 
            this.bbs_button.Location = new System.Drawing.Point(55, 40);
            this.bbs_button.Name = "bbs_button";
            this.bbs_button.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.bbs_button.Size = new System.Drawing.Size(92, 23);
            this.bbs_button.TabIndex = 0;
            this.bbs_button.Text = "Connect BBS";
            this.toolTip1.SetToolTip(this.bbs_button, "Connect to BBS");
            this.bbs_button.UseVisualStyleBackColor = true;
            this.bbs_button.Click += new System.EventHandler(this.bbs_button_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.setupToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1278, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem1});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "File";
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // setupToolStripMenuItem
            // 
            this.setupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.telnetComToolStripMenuItem,
            this.iPConfigToolStripMenuItem,
            this.clusterTelnetComToolStripMenuItem,
            this.clusterIPConfigToolStripMenuItem,
            this.nodeTelnetComToolStripMenuItem,
            this.nodeIPConfigToolStripMenuItem,
            this.toolStripMenuItemCom,
            this.SSHConfigToolStripMenuItem,
            this.beepToolStripMenuItem,
            this.colourToolStripMenuItem});
            this.setupToolStripMenuItem.Name = "setupToolStripMenuItem";
            this.setupToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.setupToolStripMenuItem.Text = "Setup";
            // 
            // telnetComToolStripMenuItem
            // 
            this.telnetComToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1});
            this.telnetComToolStripMenuItem.Name = "telnetComToolStripMenuItem";
            this.telnetComToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.telnetComToolStripMenuItem.Text = "BBS Telnet/Com";
            this.telnetComToolStripMenuItem.ToolTipText = "Configure Telnet or Com Port";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "Telnet",
            "Com Port"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // iPConfigToolStripMenuItem
            // 
            this.iPConfigToolStripMenuItem.Name = "iPConfigToolStripMenuItem";
            this.iPConfigToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.iPConfigToolStripMenuItem.Text = "BBS IP Config";
            this.iPConfigToolStripMenuItem.ToolTipText = "Configure BBS";
            this.iPConfigToolStripMenuItem.Click += new System.EventHandler(this.iPConfigToolStripMenuItem_Click);
            // 
            // clusterTelnetComToolStripMenuItem
            // 
            this.clusterTelnetComToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox2});
            this.clusterTelnetComToolStripMenuItem.Name = "clusterTelnetComToolStripMenuItem";
            this.clusterTelnetComToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.clusterTelnetComToolStripMenuItem.Text = "Cluster Telnet/Com";
            this.clusterTelnetComToolStripMenuItem.ToolTipText = "Configure Telnet or Com Port";
            // 
            // toolStripComboBox2
            // 
            this.toolStripComboBox2.Name = "toolStripComboBox2";
            this.toolStripComboBox2.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBox2.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox2_SelectedIndexChanged);
            // 
            // clusterIPConfigToolStripMenuItem
            // 
            this.clusterIPConfigToolStripMenuItem.Name = "clusterIPConfigToolStripMenuItem";
            this.clusterIPConfigToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.clusterIPConfigToolStripMenuItem.Text = "Cluster IP Config";
            this.clusterIPConfigToolStripMenuItem.ToolTipText = "Configure Cluster";
            this.clusterIPConfigToolStripMenuItem.Click += new System.EventHandler(this.clusterIPConfigToolStripMenuItem_Click);
            // 
            // nodeTelnetComToolStripMenuItem
            // 
            this.nodeTelnetComToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox3});
            this.nodeTelnetComToolStripMenuItem.Name = "nodeTelnetComToolStripMenuItem";
            this.nodeTelnetComToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.nodeTelnetComToolStripMenuItem.Text = "Node Telnet/Com";
            this.nodeTelnetComToolStripMenuItem.ToolTipText = "Configure Telnet or Com Port";
            // 
            // toolStripComboBox3
            // 
            this.toolStripComboBox3.Name = "toolStripComboBox3";
            this.toolStripComboBox3.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBox3.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox3_SelectedIndexChanged);
            // 
            // nodeIPConfigToolStripMenuItem
            // 
            this.nodeIPConfigToolStripMenuItem.Name = "nodeIPConfigToolStripMenuItem";
            this.nodeIPConfigToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.nodeIPConfigToolStripMenuItem.Text = "Node IP Config";
            this.nodeIPConfigToolStripMenuItem.ToolTipText = "Configure Node";
            this.nodeIPConfigToolStripMenuItem.Click += new System.EventHandler(this.nodeIPConfigToolStripMenuItem_Click);
            // 
            // toolStripMenuItemCom
            // 
            this.toolStripMenuItemCom.Name = "toolStripMenuItemCom";
            this.toolStripMenuItemCom.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItemCom.Text = "Com Port Config";
            this.toolStripMenuItemCom.ToolTipText = "Configure Com Port";
            this.toolStripMenuItemCom.Click += new System.EventHandler(this.toolStripMenuItem1_Click_1);
            // 
            // SSHConfigToolStripMenuItem
            // 
            this.SSHConfigToolStripMenuItem.Name = "SSHConfigToolStripMenuItem";
            this.SSHConfigToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.SSHConfigToolStripMenuItem.Text = "SSH Config";
            this.SSHConfigToolStripMenuItem.ToolTipText = "Configure SSH";
            this.SSHConfigToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // beepToolStripMenuItem
            // 
            this.beepToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxBeep});
            this.beepToolStripMenuItem.Name = "beepToolStripMenuItem";
            this.beepToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.beepToolStripMenuItem.Text = "Beep";
            this.beepToolStripMenuItem.ToolTipText = "Configure Beep";
            // 
            // toolStripComboBoxBeep
            // 
            this.toolStripComboBoxBeep.Name = "toolStripComboBoxBeep";
            this.toolStripComboBoxBeep.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBoxBeep.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxBeep_SelectedIndexChanged);
            // 
            // colourToolStripMenuItem
            // 
            this.colourToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backgroundColourToolStripMenuItem,
            this.textColorToolStripMenuItem});
            this.colourToolStripMenuItem.Name = "colourToolStripMenuItem";
            this.colourToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.colourToolStripMenuItem.Text = "Color";
            this.colourToolStripMenuItem.ToolTipText = "Configure Color";
            // 
            // backgroundColourToolStripMenuItem
            // 
            this.backgroundColourToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxBGC});
            this.backgroundColourToolStripMenuItem.Name = "backgroundColourToolStripMenuItem";
            this.backgroundColourToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.backgroundColourToolStripMenuItem.Text = "Background Color";
            // 
            // toolStripComboBoxBGC
            // 
            this.toolStripComboBoxBGC.Name = "toolStripComboBoxBGC";
            this.toolStripComboBoxBGC.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBoxBGC.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxBGC_SelectedIndexChanged);
            // 
            // textColorToolStripMenuItem
            // 
            this.textColorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxTXTC});
            this.textColorToolStripMenuItem.Name = "textColorToolStripMenuItem";
            this.textColorToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.textColorToolStripMenuItem.Text = "Text Color";
            // 
            // toolStripComboBoxTXTC
            // 
            this.toolStripComboBoxTXTC.Name = "toolStripComboBoxTXTC";
            this.toolStripComboBoxTXTC.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBoxTXTC.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxTXTC_SelectedIndexChanged);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // forward_button
            // 
            this.forward_button.Location = new System.Drawing.Point(149, 41);
            this.forward_button.Name = "forward_button";
            this.forward_button.Size = new System.Drawing.Size(93, 23);
            this.forward_button.TabIndex = 6;
            this.forward_button.Text = "Forward";
            this.toolTip1.SetToolTip(this.forward_button, "Start Forwarding to BBS");
            this.forward_button.UseVisualStyleBackColor = true;
            this.forward_button.Click += new System.EventHandler(this.forward_button_Click);
            // 
            // cluster_button
            // 
            this.cluster_button.Location = new System.Drawing.Point(231, 40);
            this.cluster_button.Name = "cluster_button";
            this.cluster_button.Size = new System.Drawing.Size(94, 23);
            this.cluster_button.TabIndex = 2;
            this.cluster_button.Text = "Connect Cluster";
            this.toolTip1.SetToolTip(this.cluster_button, "Connect to Cluster");
            this.cluster_button.UseVisualStyleBackColor = true;
            this.cluster_button.Click += new System.EventHandler(this.cluster_button_Click);
            // 
            // node_button
            // 
            this.node_button.Location = new System.Drawing.Point(331, 40);
            this.node_button.Name = "node_button";
            this.node_button.Size = new System.Drawing.Size(94, 23);
            this.node_button.TabIndex = 3;
            this.node_button.Text = "Connect Node";
            this.toolTip1.SetToolTip(this.node_button, "Connect to Node");
            this.node_button.UseVisualStyleBackColor = true;
            this.node_button.Click += new System.EventHandler(this.node_button_Click);
            // 
            // disconnect_button
            // 
            this.disconnect_button.Location = new System.Drawing.Point(449, 41);
            this.disconnect_button.Name = "disconnect_button";
            this.disconnect_button.Size = new System.Drawing.Size(94, 23);
            this.disconnect_button.TabIndex = 7;
            this.disconnect_button.TabStop = false;
            this.disconnect_button.Text = "Disconnect";
            this.toolTip1.SetToolTip(this.disconnect_button, "Disconnect");
            this.disconnect_button.UseVisualStyleBackColor = true;
            this.disconnect_button.Click += new System.EventHandler(this.disconnect_button_Click);
            // 
            // ssh_button
            // 
            this.ssh_button.Location = new System.Drawing.Point(561, 40);
            this.ssh_button.Name = "ssh_button";
            this.ssh_button.Size = new System.Drawing.Size(94, 23);
            this.ssh_button.TabIndex = 4;
            this.ssh_button.Text = "SSH";
            this.toolTip1.SetToolTip(this.ssh_button, "To Connect SSH");
            this.ssh_button.UseVisualStyleBackColor = true;
            this.ssh_button.Click += new System.EventHandler(this.ssh_button_Click);
            // 
            // mail_button
            // 
            this.mail_button.Location = new System.Drawing.Point(673, 40);
            this.mail_button.Name = "mail_button";
            this.mail_button.Size = new System.Drawing.Size(94, 23);
            this.mail_button.TabIndex = 8;
            this.mail_button.TabStop = false;
            this.mail_button.Text = "Mail Config";
            this.toolTip1.SetToolTip(this.mail_button, "Mail List");
            this.mail_button.UseVisualStyleBackColor = true;
            this.mail_button.Click += new System.EventHandler(this.mail_button_Click);
            // 
            // button_read
            // 
            this.button_read.Location = new System.Drawing.Point(785, 40);
            this.button_read.Name = "button_read";
            this.button_read.Size = new System.Drawing.Size(94, 23);
            this.button_read.TabIndex = 9;
            this.button_read.TabStop = false;
            this.button_read.Text = "Mail Read";
            this.toolTip1.SetToolTip(this.button_read, "Mail List");
            this.button_read.UseVisualStyleBackColor = true;
            this.button_read.Click += new System.EventHandler(this.button_read_Click);
            // 
            // terminalEmulator1
            // 
            this.terminalEmulator1.BackColor = System.Drawing.Color.Black;
            this.terminalEmulator1.BaudRateType = PacketComs.TerminalEmulator.BaudRateTypes.Baud_4800;
            this.terminalEmulator1.BBSPrompt = null;
            this.terminalEmulator1.Beep = true;
            this.terminalEmulator1.Close = false;
            this.terminalEmulator1.Columns = 172;
            this.terminalEmulator1.ConnectionType = PacketComs.TerminalEmulator.ConnectionTypes.Telnet;
            this.terminalEmulator1.DataBitsType = PacketComs.TerminalEmulator.DataBitsTypes.Data_Bits_8;
            this.terminalEmulator1.dnsName = null;
            this.terminalEmulator1.FileActive = false;
            this.terminalEmulator1.FlowType = PacketComs.TerminalEmulator.FlowTypes.XOnXOff;
            this.terminalEmulator1.Font = new System.Drawing.Font("Courier New", 8F);
            this.terminalEmulator1.Header = null;
            this.terminalEmulator1.Hostname = null;
            this.terminalEmulator1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.terminalEmulator1.LastNumber = 0;
            this.terminalEmulator1.LocalEcho = false;
            this.terminalEmulator1.Location = new System.Drawing.Point(31, 89);
            this.terminalEmulator1.Name = "terminalEmulator1";
            this.terminalEmulator1.ParityType = PacketComs.TerminalEmulator.ParityTypes.None;
            this.terminalEmulator1.Password = null;
            this.terminalEmulator1.PasswordPrompt = null;
            this.terminalEmulator1.Port = 9000;
            this.terminalEmulator1.Rows = 40;
            this.terminalEmulator1.SerialPort = "";
            this.terminalEmulator1.Size = new System.Drawing.Size(1216, 522);
            this.terminalEmulator1.StopBitsType = PacketComs.TerminalEmulator.StopBitsTypes.One;
            this.terminalEmulator1.TabIndex = 5;
            this.terminalEmulator1.Text = "terminalEmulator1";
            this.terminalEmulator1.UernamePrompt = null;
            this.terminalEmulator1.Username = null;
            this.terminalEmulator1.Disconnected += new System.EventHandler(this.Disconnected);
            this.terminalEmulator1.ForwardDone += new System.EventHandler(this.terminalEmulator1_ForwardDone);
            this.terminalEmulator1.LastNumberevt += new System.EventHandler(this.terminalEmulator1_LastNumberevt);
            // 
            // button_personal
            // 
            this.button_personal.Location = new System.Drawing.Point(897, 40);
            this.button_personal.Name = "button_personal";
            this.button_personal.Size = new System.Drawing.Size(94, 23);
            this.button_personal.TabIndex = 10;
            this.button_personal.TabStop = false;
            this.button_personal.Text = "Personal Mail";
            this.toolTip1.SetToolTip(this.button_personal, "Mail List");
            this.button_personal.UseVisualStyleBackColor = true;
            this.button_personal.Click += new System.EventHandler(this.button_personal_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1278, 637);
            this.Controls.Add(this.button_personal);
            this.Controls.Add(this.button_read);
            this.Controls.Add(this.mail_button);
            this.Controls.Add(this.terminalEmulator1);
            this.Controls.Add(this.ssh_button);
            this.Controls.Add(this.disconnect_button);
            this.Controls.Add(this.node_button);
            this.Controls.Add(this.cluster_button);
            this.Controls.Add(this.forward_button);
            this.Controls.Add(this.bbs_button);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Packet Radio";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize_1);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button bbs_button;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem fileToolStripMenuItem1;
        private ToolStripMenuItem exitToolStripMenuItem1;
        private ToolStripMenuItem setupToolStripMenuItem;
        private ToolStripMenuItem telnetComToolStripMenuItem;
        private ToolStripComboBox toolStripComboBox1;
        private ToolStripMenuItem iPConfigToolStripMenuItem;
        private Button forward_button;
        private ToolStripMenuItem clusterTelnetComToolStripMenuItem;
        private ToolStripMenuItem clusterIPConfigToolStripMenuItem;
        private ToolStripMenuItem nodeTelnetComToolStripMenuItem;
        private ToolStripMenuItem nodeIPConfigToolStripMenuItem;
        private Button cluster_button;
        private Button node_button;
        private ToolStripComboBox toolStripComboBox2;
        private ToolStripComboBox toolStripComboBox3;
        private Button disconnect_button;
        private Button ssh_button;
        private ToolStripMenuItem beepToolStripMenuItem;
        private ToolStripComboBox toolStripComboBoxBeep;
        private ToolStripMenuItem colourToolStripMenuItem;
        private ToolStripMenuItem backgroundColourToolStripMenuItem;
        private ToolStripComboBox toolStripComboBoxBGC;
        private ToolStripMenuItem textColorToolStripMenuItem;
        private ToolStripComboBox toolStripComboBoxTXTC;
        private TerminalEmulator terminalEmulator1;
        private ToolStripMenuItem SSHConfigToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItemCom;
        private ToolTip toolTip1;
        private Button mail_button;
        private readonly ModifyRegistry _myRegistryBbs = new ModifyRegistry();
        private readonly Encrypting _myEncrypt = new Encrypting();
        private readonly ModifyRegistry _myRegistry = new ModifyRegistry();
        private readonly ModifyRegistry _myRegistryCluster = new ModifyRegistry();
        private readonly ModifyRegistry _myRegistryCom = new ModifyRegistry();
        private readonly ModifyRegistry _myRegistryNode = new ModifyRegistry();
        private readonly ModifyRegistry _myRegistrySsh = new ModifyRegistry();
        private Button button_read;
        private Button button_personal;
    }
}

