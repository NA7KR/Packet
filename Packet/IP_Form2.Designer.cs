namespace Packet
{
    partial class IP_Form2
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
            this.ip_label = new System.Windows.Forms.Label();
            this.ip_textBox = new System.Windows.Forms.TextBox();
            this.Done_button = new System.Windows.Forms.Button();
            this.Cancel_button = new System.Windows.Forms.Button();
            this.port_label = new System.Windows.Forms.Label();
            this.mycall_label = new System.Windows.Forms.Label();
            this.port_textBox = new System.Windows.Forms.TextBox();
            this.mycall_textBox = new System.Windows.Forms.TextBox();
            this.start_textBox = new System.Windows.Forms.TextBox();
            this.bbs_textBox = new System.Windows.Forms.TextBox();
            this.start_label = new System.Windows.Forms.Label();
            this.bbs_label = new System.Windows.Forms.Label();
            this.password_textBox = new System.Windows.Forms.TextBox();
            this.password_label = new System.Windows.Forms.Label();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.echo_label = new System.Windows.Forms.Label();
            this.echo_comboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // ip_label
            // 
            this.ip_label.AutoSize = true;
            this.ip_label.Location = new System.Drawing.Point(36, 31);
            this.ip_label.Name = "ip_label";
            this.ip_label.Size = new System.Drawing.Size(106, 13);
            this.ip_label.TabIndex = 0;
            this.ip_label.Text = "BBS IP or HostName";
            // 
            // ip_textBox
            // 
            this.ip_textBox.Location = new System.Drawing.Point(145, 31);
            this.ip_textBox.Name = "ip_textBox";
            this.ip_textBox.Size = new System.Drawing.Size(109, 20);
            this.ip_textBox.TabIndex = 5;
            // 
            // Done_button
            // 
            this.Done_button.Location = new System.Drawing.Point(22, 224);
            this.Done_button.Name = "Done_button";
            this.Done_button.Size = new System.Drawing.Size(75, 23);
            this.Done_button.TabIndex = 10;
            this.Done_button.Text = "Done";
            this.Done_button.UseVisualStyleBackColor = true;
            this.Done_button.Click += new System.EventHandler(this.Done_button_Click);
            // 
            // Cancel_button
            // 
            this.Cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_button.Location = new System.Drawing.Point(145, 224);
            this.Cancel_button.Name = "Cancel_button";
            this.Cancel_button.Size = new System.Drawing.Size(75, 23);
            this.Cancel_button.TabIndex = 11;
            this.Cancel_button.Text = "Cancel";
            this.Cancel_button.UseVisualStyleBackColor = true;
            // 
            // port_label
            // 
            this.port_label.AutoSize = true;
            this.port_label.Location = new System.Drawing.Point(37, 57);
            this.port_label.Name = "port_label";
            this.port_label.Size = new System.Drawing.Size(50, 13);
            this.port_label.TabIndex = 1;
            this.port_label.Text = "BBS Port";
            // 
            // mycall_label
            // 
            this.mycall_label.AutoSize = true;
            this.mycall_label.Location = new System.Drawing.Point(35, 140);
            this.mycall_label.Name = "mycall_label";
            this.mycall_label.Size = new System.Drawing.Size(62, 13);
            this.mycall_label.TabIndex = 3;
            this.mycall_label.Text = "My CallSign";
            // 
            // port_textBox
            // 
            this.port_textBox.Location = new System.Drawing.Point(145, 57);
            this.port_textBox.Name = "port_textBox";
            this.port_textBox.Size = new System.Drawing.Size(109, 20);
            this.port_textBox.TabIndex = 6;
            // 
            // mycall_textBox
            // 
            this.mycall_textBox.Location = new System.Drawing.Point(143, 144);
            this.mycall_textBox.Name = "mycall_textBox";
            this.mycall_textBox.Size = new System.Drawing.Size(109, 20);
            this.mycall_textBox.TabIndex = 9;
            // 
            // start_textBox
            // 
            this.start_textBox.Location = new System.Drawing.Point(144, 115);
            this.start_textBox.Name = "start_textBox";
            this.start_textBox.Size = new System.Drawing.Size(109, 20);
            this.start_textBox.TabIndex = 15;
            // 
            // bbs_textBox
            // 
            this.bbs_textBox.Location = new System.Drawing.Point(144, 87);
            this.bbs_textBox.Name = "bbs_textBox";
            this.bbs_textBox.Size = new System.Drawing.Size(109, 20);
            this.bbs_textBox.TabIndex = 14;
            // 
            // start_label
            // 
            this.start_label.AutoSize = true;
            this.start_label.Location = new System.Drawing.Point(35, 115);
            this.start_label.Name = "start_label";
            this.start_label.Size = new System.Drawing.Size(93, 13);
            this.start_label.TabIndex = 13;
            this.start_label.Text = "BBS Start Number";
            // 
            // bbs_label
            // 
            this.bbs_label.AutoSize = true;
            this.bbs_label.Location = new System.Drawing.Point(36, 87);
            this.bbs_label.Name = "bbs_label";
            this.bbs_label.Size = new System.Drawing.Size(67, 13);
            this.bbs_label.TabIndex = 12;
            this.bbs_label.Text = "BBS Callsign";
            // 
            // password_textBox
            // 
            this.password_textBox.Location = new System.Drawing.Point(143, 170);
            this.password_textBox.Name = "password_textBox";
            this.password_textBox.PasswordChar = '#';
            this.password_textBox.Size = new System.Drawing.Size(109, 20);
            this.password_textBox.TabIndex = 18;
            // 
            // password_label
            // 
            this.password_label.AutoSize = true;
            this.password_label.Location = new System.Drawing.Point(34, 164);
            this.password_label.Name = "password_label";
            this.password_label.Size = new System.Drawing.Size(77, 13);
            this.password_label.TabIndex = 16;
            this.password_label.Text = "BBS Password";
            // 
            // echo_label
            // 
            this.echo_label.AutoSize = true;
            this.echo_label.Location = new System.Drawing.Point(35, 198);
            this.echo_label.Name = "echo_label";
            this.echo_label.Size = new System.Drawing.Size(56, 13);
            this.echo_label.TabIndex = 20;
            this.echo_label.Text = "BBS Echo";
            // 
            // echo_comboBox
            // 
            this.echo_comboBox.DisplayMember = "1";
            this.echo_comboBox.FormattingEnabled = true;
            this.echo_comboBox.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.echo_comboBox.Location = new System.Drawing.Point(143, 198);
            this.echo_comboBox.Name = "echo_comboBox";
            this.echo_comboBox.Size = new System.Drawing.Size(111, 21);
            this.echo_comboBox.TabIndex = 21;
            this.echo_comboBox.ValueMember = "1";
            this.echo_comboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // IP_Form2
            // 
            this.AcceptButton = this.Done_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_button;
            this.ClientSize = new System.Drawing.Size(314, 314);
            this.Controls.Add(this.echo_comboBox);
            this.Controls.Add(this.echo_label);
            this.Controls.Add(this.password_textBox);
            this.Controls.Add(this.password_label);
            this.Controls.Add(this.start_textBox);
            this.Controls.Add(this.bbs_textBox);
            this.Controls.Add(this.start_label);
            this.Controls.Add(this.bbs_label);
            this.Controls.Add(this.Cancel_button);
            this.Controls.Add(this.Done_button);
            this.Controls.Add(this.mycall_textBox);
            this.Controls.Add(this.port_textBox);
            this.Controls.Add(this.ip_textBox);
            this.Controls.Add(this.mycall_label);
            this.Controls.Add(this.port_label);
            this.Controls.Add(this.ip_label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IP_Form2";
            this.Text = "IP Config";
            this.Load += new System.EventHandler(this.IP_Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ip_label;
        private System.Windows.Forms.TextBox ip_textBox;
        private System.Windows.Forms.Button Done_button;
        private System.Windows.Forms.Button Cancel_button;
        private System.Windows.Forms.Label port_label;
        private System.Windows.Forms.Label mycall_label;
        private System.Windows.Forms.TextBox port_textBox;
        private System.Windows.Forms.TextBox mycall_textBox;
        private System.Windows.Forms.TextBox start_textBox;
        private System.Windows.Forms.TextBox bbs_textBox;
        private System.Windows.Forms.Label start_label;
        private System.Windows.Forms.Label bbs_label;
        private System.Windows.Forms.TextBox password_textBox;
        private System.Windows.Forms.Label password_label;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Label echo_label;
        private System.Windows.Forms.ComboBox echo_comboBox;
    }
}