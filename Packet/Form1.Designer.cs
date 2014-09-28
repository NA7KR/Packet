namespace Packet
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
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
            this.beepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxBeep = new System.Windows.Forms.ToolStripComboBox();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.forward_button = new System.Windows.Forms.Button();
            this.cluster_button = new System.Windows.Forms.Button();
            this.node_button = new System.Windows.Forms.Button();
            this.disconnect_button = new System.Windows.Forms.Button();
            this.ssh_button = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.colourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundColourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxBGC = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripComboBoxTXTC = new System.Windows.Forms.ToolStripComboBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // bbs_button
            // 
            this.bbs_button.Location = new System.Drawing.Point(55, 40);
            this.bbs_button.Name = "bbs_button";
            this.bbs_button.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.bbs_button.Size = new System.Drawing.Size(92, 23);
            this.bbs_button.TabIndex = 0;
            this.bbs_button.Text = "Connect BBS";
            this.bbs_button.UseVisualStyleBackColor = true;
            this.bbs_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.setupToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1207, 24);
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
            this.iPConfigToolStripMenuItem.Click += new System.EventHandler(this.iPConfigToolStripMenuItem_Click);
            // 
            // clusterTelnetComToolStripMenuItem
            // 
            this.clusterTelnetComToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox2});
            this.clusterTelnetComToolStripMenuItem.Name = "clusterTelnetComToolStripMenuItem";
            this.clusterTelnetComToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.clusterTelnetComToolStripMenuItem.Text = "Cluster Telnet/Com";
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
            this.clusterIPConfigToolStripMenuItem.Click += new System.EventHandler(this.clusterIPConfigToolStripMenuItem_Click);
            // 
            // nodeTelnetComToolStripMenuItem
            // 
            this.nodeTelnetComToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox3});
            this.nodeTelnetComToolStripMenuItem.Name = "nodeTelnetComToolStripMenuItem";
            this.nodeTelnetComToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.nodeTelnetComToolStripMenuItem.Text = "Node Telnet/Com";
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
            this.nodeIPConfigToolStripMenuItem.Click += new System.EventHandler(this.nodeIPConfigToolStripMenuItem_Click);
            // 
            // beepToolStripMenuItem
            // 
            this.beepToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxBeep});
            this.beepToolStripMenuItem.Name = "beepToolStripMenuItem";
            this.beepToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.beepToolStripMenuItem.Text = "Beep";
            // 
            // toolStripComboBoxBeep
            // 
            this.toolStripComboBoxBeep.Name = "toolStripComboBoxBeep";
            this.toolStripComboBoxBeep.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBoxBeep.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxBeep_SelectedIndexChanged);
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
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.WindowText;
            this.richTextBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //this.richTextBox1.ForeColor = System.Drawing.Color.Yellow;
            this.richTextBox1.Location = new System.Drawing.Point(55, 87);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ShortcutsEnabled = false;
            this.richTextBox1.Size = new System.Drawing.Size(557, 224);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.SystemColors.WindowText;
            this.richTextBox2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //this.richTextBox2.ForeColor = System.Drawing.Color.Yellow;
            this.richTextBox2.Location = new System.Drawing.Point(31, 339);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(581, 214);
            this.richTextBox2.TabIndex = 3;
            this.richTextBox2.Text = "";
            this.richTextBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox2_KeyDown_1);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(690, 41);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(250, 20);
            this.textBox1.TabIndex = 4;
            // 
            // forward_button
            // 
            this.forward_button.Location = new System.Drawing.Point(149, 41);
            this.forward_button.Name = "forward_button";
            this.forward_button.Size = new System.Drawing.Size(93, 23);
            this.forward_button.TabIndex = 5;
            this.forward_button.Text = "Forward";
            this.forward_button.UseVisualStyleBackColor = true;
            this.forward_button.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // cluster_button
            // 
            this.cluster_button.Location = new System.Drawing.Point(231, 40);
            this.cluster_button.Name = "cluster_button";
            this.cluster_button.Size = new System.Drawing.Size(94, 23);
            this.cluster_button.TabIndex = 6;
            this.cluster_button.Text = "Connect Cluster";
            this.cluster_button.UseVisualStyleBackColor = true;
            this.cluster_button.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // node_button
            // 
            this.node_button.Location = new System.Drawing.Point(331, 40);
            this.node_button.Name = "node_button";
            this.node_button.Size = new System.Drawing.Size(94, 23);
            this.node_button.TabIndex = 7;
            this.node_button.Text = "Connect Node";
            this.node_button.UseVisualStyleBackColor = true;
            this.node_button.Click += new System.EventHandler(this.node_button_Click);
            // 
            // disconnect_button
            // 
            this.disconnect_button.Location = new System.Drawing.Point(449, 41);
            this.disconnect_button.Name = "disconnect_button";
            this.disconnect_button.Size = new System.Drawing.Size(94, 23);
            this.disconnect_button.TabIndex = 8;
            this.disconnect_button.Text = "Disconnect";
            this.disconnect_button.UseVisualStyleBackColor = true;
            this.disconnect_button.Click += new System.EventHandler(this.disconnect_button_Click);
            // 
            // ssh_button
            // 
            this.ssh_button.Location = new System.Drawing.Point(561, 40);
            this.ssh_button.Name = "ssh_button";
            this.ssh_button.Size = new System.Drawing.Size(94, 23);
            this.ssh_button.TabIndex = 9;
            this.ssh_button.Text = "SSH";
            this.ssh_button.UseVisualStyleBackColor = true;
            this.ssh_button.Click += new System.EventHandler(this.ssh_button_Click);
            // 
            // colourToolStripMenuItem
            // 
            this.colourToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backgroundColourToolStripMenuItem,
            this.textColorToolStripMenuItem});
            this.colourToolStripMenuItem.Name = "colourToolStripMenuItem";
            this.colourToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.colourToolStripMenuItem.Text = "Color";
            // 
            // backgroundColourToolStripMenuItem
            // 
            this.backgroundColourToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxBGC});
            this.backgroundColourToolStripMenuItem.Name = "backgroundColourToolStripMenuItem";
            this.backgroundColourToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.backgroundColourToolStripMenuItem.Text = "Background Color";
            // 
            // textColorToolStripMenuItem
            // 
            this.textColorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxTXTC});
            this.textColorToolStripMenuItem.Name = "textColorToolStripMenuItem";
            this.textColorToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.textColorToolStripMenuItem.Text = "Text Color";
            // 
            // toolStripComboBoxBGC
            // 
            this.toolStripComboBoxBGC.Name = "toolStripComboBoxBGC";
            this.toolStripComboBoxBGC.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBoxBGC.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxBGC_SelectedIndexChanged);
            // 
            // toolStripComboBoxTXTC
            // 
            this.toolStripComboBoxTXTC.Name = "toolStripComboBoxTXTC";
            this.toolStripComboBoxTXTC.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBoxTXTC.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxTXTC_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1207, 599);
            this.Controls.Add(this.ssh_button);
            this.Controls.Add(this.disconnect_button);
            this.Controls.Add(this.node_button);
            this.Controls.Add(this.cluster_button);
            this.Controls.Add(this.forward_button);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.bbs_button);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Packet Radio";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize_1);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button bbs_button;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        public System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem setupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem telnetComToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripMenuItem iPConfigToolStripMenuItem;
        private System.Windows.Forms.Button forward_button;
        private System.Windows.Forms.ToolStripMenuItem clusterTelnetComToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clusterIPConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nodeTelnetComToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nodeIPConfigToolStripMenuItem;
        private System.Windows.Forms.Button cluster_button;
        private System.Windows.Forms.Button node_button;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox3;
        private System.Windows.Forms.Button disconnect_button;
        private System.Windows.Forms.Button ssh_button;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.BindingSource bindingSource2;
        private System.Windows.Forms.ToolStripMenuItem beepToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxBeep;
        private System.Windows.Forms.ToolStripMenuItem colourToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backgroundColourToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxBGC;
        private System.Windows.Forms.ToolStripMenuItem textColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxTXTC;
    }
}

