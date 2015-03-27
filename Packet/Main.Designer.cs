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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.mail_button = new System.Windows.Forms.Button();
            this.button_read = new System.Windows.Forms.Button();
            this.button_personal = new System.Windows.Forms.Button();
            this.button_7plus = new System.Windows.Forms.Button();
            this.terminalEmulator1 = new PacketComs.TerminalEmulator();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_bbs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_fwd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_cls = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_Node = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_Disconnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_SSH = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
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
            // mail_button
            // 
            this.mail_button.Location = new System.Drawing.Point(658, 74);
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
            this.button_read.Location = new System.Drawing.Point(758, 74);
            this.button_read.Name = "button_read";
            this.button_read.Size = new System.Drawing.Size(94, 23);
            this.button_read.TabIndex = 9;
            this.button_read.TabStop = false;
            this.button_read.Text = "Mail Read";
            this.toolTip1.SetToolTip(this.button_read, "Mail List");
            this.button_read.UseVisualStyleBackColor = true;
            this.button_read.Click += new System.EventHandler(this.button_read_Click);
            // 
            // button_personal
            // 
            this.button_personal.Location = new System.Drawing.Point(858, 74);
            this.button_personal.Name = "button_personal";
            this.button_personal.Size = new System.Drawing.Size(94, 23);
            this.button_personal.TabIndex = 10;
            this.button_personal.TabStop = false;
            this.button_personal.Text = "Personal Mail";
            this.toolTip1.SetToolTip(this.button_personal, "Mail List");
            this.button_personal.UseVisualStyleBackColor = true;
            this.button_personal.Click += new System.EventHandler(this.button_personal_Click);
            // 
            // button_7plus
            // 
            this.button_7plus.Location = new System.Drawing.Point(958, 74);
            this.button_7plus.Name = "button_7plus";
            this.button_7plus.Size = new System.Drawing.Size(94, 23);
            this.button_7plus.TabIndex = 11;
            this.button_7plus.TabStop = false;
            this.button_7plus.Text = "7Plus";
            this.toolTip1.SetToolTip(this.button_7plus, "Mail List");
            this.button_7plus.UseVisualStyleBackColor = true;
            this.button_7plus.Click += new System.EventHandler(this.button1_Click);
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
            this.terminalEmulator1.Location = new System.Drawing.Point(31, 103);
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
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(105, 40);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_bbs,
            this.toolStripSeparator1,
            this.toolStripButton_fwd,
            this.toolStripSeparator2,
            this.toolStripButton_cls,
            this.toolStripSeparator3,
            this.toolStripButton_Node,
            this.toolStripSeparator4,
            this.toolStripButton_Disconnect,
            this.toolStripButton_SSH,
            this.toolStripSeparator5});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1278, 47);
            this.toolStrip1.TabIndex = 12;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton_bbs
            // 
            this.toolStripButton_bbs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_bbs.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_bbs.Image")));
            this.toolStripButton_bbs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_bbs.Name = "toolStripButton_bbs";
            this.toolStripButton_bbs.Size = new System.Drawing.Size(109, 44);
            this.toolStripButton_bbs.Text = "toolStripButton_bbs";
            this.toolStripButton_bbs.Click += new System.EventHandler(this.toolStripButton_BBS_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 47);
            // 
            // toolStripButton_fwd
            // 
            this.toolStripButton_fwd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_fwd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_fwd.Image")));
            this.toolStripButton_fwd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_fwd.Name = "toolStripButton_fwd";
            this.toolStripButton_fwd.Size = new System.Drawing.Size(109, 44);
            this.toolStripButton_fwd.Text = "toolStripButton_fwd";
            this.toolStripButton_fwd.Click += new System.EventHandler(this.toolStripButton_FWD_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 47);
            // 
            // toolStripButton_cls
            // 
            this.toolStripButton_cls.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_cls.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_cls.Image")));
            this.toolStripButton_cls.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_cls.Name = "toolStripButton_cls";
            this.toolStripButton_cls.Size = new System.Drawing.Size(109, 44);
            this.toolStripButton_cls.Text = "+";
            this.toolStripButton_cls.Click += new System.EventHandler(this.toolStripButton_Cluster_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 47);
            // 
            // toolStripButton_Node
            // 
            this.toolStripButton_Node.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Node.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Node.Image")));
            this.toolStripButton_Node.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Node.Name = "toolStripButton_Node";
            this.toolStripButton_Node.Size = new System.Drawing.Size(109, 44);
            this.toolStripButton_Node.Text = "toolStripButton1";
            this.toolStripButton_Node.Click += new System.EventHandler(this.toolStripButton_Node_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 47);
            // 
            // toolStripButton_Disconnect
            // 
            this.toolStripButton_Disconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Disconnect.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Disconnect.Image")));
            this.toolStripButton_Disconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Disconnect.Name = "toolStripButton_Disconnect";
            this.toolStripButton_Disconnect.Size = new System.Drawing.Size(109, 44);
            this.toolStripButton_Disconnect.Text = "toolStripButton1";
            this.toolStripButton_Disconnect.Click += new System.EventHandler(this.toolStripButton_Disconnect_Click);
            // 
            // toolStripButton_SSH
            // 
            this.toolStripButton_SSH.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_SSH.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_SSH.Image")));
            this.toolStripButton_SSH.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_SSH.Name = "toolStripButton_SSH";
            this.toolStripButton_SSH.Size = new System.Drawing.Size(109, 44);
            this.toolStripButton_SSH.Text = "toolStripButton1";
            this.toolStripButton_SSH.Click += new System.EventHandler(this.toolStripButton_SSH_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 47);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1278, 637);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.button_7plus);
            this.Controls.Add(this.button_personal);
            this.Controls.Add(this.button_read);
            this.Controls.Add(this.mail_button);
            this.Controls.Add(this.terminalEmulator1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Packet Radio";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize_1);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private ToolStripMenuItem clusterTelnetComToolStripMenuItem;
        private ToolStripMenuItem clusterIPConfigToolStripMenuItem;
        private ToolStripMenuItem nodeTelnetComToolStripMenuItem;
        private ToolStripMenuItem nodeIPConfigToolStripMenuItem;
        private ToolStripComboBox toolStripComboBox2;
        private ToolStripComboBox toolStripComboBox3;
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
        private Button button_7plus;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton_bbs;
        private ToolStripButton toolStripButton_fwd;
        private ToolStripButton toolStripButton_cls;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton toolStripButton_Node;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton toolStripButton_Disconnect;
        private ToolStripButton toolStripButton_SSH;
        private ToolStripSeparator toolStripSeparator5;
    }
}

