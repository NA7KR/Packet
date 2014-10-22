﻿namespace Packet
{
    partial class IP_Form
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
            this.label_ip = new System.Windows.Forms.Label();
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.Done_button = new System.Windows.Forms.Button();
            this.Cancel_button = new System.Windows.Forms.Button();
            this.label_port = new System.Windows.Forms.Label();
            this.label_mycall = new System.Windows.Forms.Label();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.textBox_mycall = new System.Windows.Forms.TextBox();
            this.textBox_start = new System.Windows.Forms.TextBox();
            this.textBox_bbs = new System.Windows.Forms.TextBox();
            this.label_start = new System.Windows.Forms.Label();
            this.label_bbs = new System.Windows.Forms.Label();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.label_password = new System.Windows.Forms.Label();
            this.label_echo = new System.Windows.Forms.Label();
            this.echo_comboBox = new System.Windows.Forms.ComboBox();
            this.textBox_prompt = new System.Windows.Forms.TextBox();
            this.label_prompt = new System.Windows.Forms.Label();
            this.textBox_username_prompt = new System.Windows.Forms.TextBox();
            this.label_username_prompt = new System.Windows.Forms.Label();
            this.textBox_password_prompt = new System.Windows.Forms.TextBox();
            this.label_password_prompt = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_ip
            // 
            this.label_ip.AutoSize = true;
            this.label_ip.Location = new System.Drawing.Point(36, 31);
            this.label_ip.Name = "label_ip";
            this.label_ip.Size = new System.Drawing.Size(106, 13);
            this.label_ip.TabIndex = 0;
            this.label_ip.Text = "BBS IP or HostName";
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(145, 31);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(109, 20);
            this.textBox_ip.TabIndex = 5;
            // 
            // Done_button
            // 
            this.Done_button.Location = new System.Drawing.Point(28, 321);
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
            this.Cancel_button.Location = new System.Drawing.Point(154, 321);
            this.Cancel_button.Name = "Cancel_button";
            this.Cancel_button.Size = new System.Drawing.Size(75, 23);
            this.Cancel_button.TabIndex = 11;
            this.Cancel_button.Text = "Cancel";
            this.Cancel_button.UseVisualStyleBackColor = true;
            // 
            // label_port
            // 
            this.label_port.AutoSize = true;
            this.label_port.Location = new System.Drawing.Point(37, 57);
            this.label_port.Name = "label_port";
            this.label_port.Size = new System.Drawing.Size(50, 13);
            this.label_port.TabIndex = 1;
            this.label_port.Text = "BBS Port";
            // 
            // label_mycall
            // 
            this.label_mycall.AutoSize = true;
            this.label_mycall.Location = new System.Drawing.Point(35, 140);
            this.label_mycall.Name = "label_mycall";
            this.label_mycall.Size = new System.Drawing.Size(62, 13);
            this.label_mycall.TabIndex = 3;
            this.label_mycall.Text = "My CallSign";
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(145, 57);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(109, 20);
            this.textBox_port.TabIndex = 6;
            // 
            // textBox_mycall
            // 
            this.textBox_mycall.Location = new System.Drawing.Point(143, 144);
            this.textBox_mycall.Name = "textBox_mycall";
            this.textBox_mycall.Size = new System.Drawing.Size(109, 20);
            this.textBox_mycall.TabIndex = 9;
            // 
            // textBox_start
            // 
            this.textBox_start.Location = new System.Drawing.Point(144, 115);
            this.textBox_start.Name = "textBox_start";
            this.textBox_start.Size = new System.Drawing.Size(109, 20);
            this.textBox_start.TabIndex = 15;
            // 
            // textBox_bbs
            // 
            this.textBox_bbs.Location = new System.Drawing.Point(144, 87);
            this.textBox_bbs.Name = "textBox_bbs";
            this.textBox_bbs.Size = new System.Drawing.Size(109, 20);
            this.textBox_bbs.TabIndex = 14;
            // 
            // label_start
            // 
            this.label_start.AutoSize = true;
            this.label_start.Location = new System.Drawing.Point(35, 115);
            this.label_start.Name = "label_start";
            this.label_start.Size = new System.Drawing.Size(93, 13);
            this.label_start.TabIndex = 13;
            this.label_start.Text = "BBS Start Number";
            // 
            // label_bbs
            // 
            this.label_bbs.AutoSize = true;
            this.label_bbs.Location = new System.Drawing.Point(36, 87);
            this.label_bbs.Name = "label_bbs";
            this.label_bbs.Size = new System.Drawing.Size(67, 13);
            this.label_bbs.TabIndex = 12;
            this.label_bbs.Text = "BBS Callsign";
            // 
            // textBox_password
            // 
            this.textBox_password.Location = new System.Drawing.Point(160, 170);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.PasswordChar = '#';
            this.textBox_password.Size = new System.Drawing.Size(109, 20);
            this.textBox_password.TabIndex = 18;
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Location = new System.Drawing.Point(35, 170);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(77, 13);
            this.label_password.TabIndex = 16;
            this.label_password.Text = "BBS Password";
            // 
            // label_echo
            // 
            this.label_echo.AutoSize = true;
            this.label_echo.Location = new System.Drawing.Point(35, 198);
            this.label_echo.Name = "label_echo";
            this.label_echo.Size = new System.Drawing.Size(56, 13);
            this.label_echo.TabIndex = 20;
            this.label_echo.Text = "BBS Echo";
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
            // 
            // textBox_prompt
            // 
            this.textBox_prompt.Location = new System.Drawing.Point(144, 227);
            this.textBox_prompt.Name = "textBox_prompt";
            this.textBox_prompt.Size = new System.Drawing.Size(109, 20);
            this.textBox_prompt.TabIndex = 23;
            // 
            // label_prompt
            // 
            this.label_prompt.AutoSize = true;
            this.label_prompt.Location = new System.Drawing.Point(36, 227);
            this.label_prompt.Name = "label_prompt";
            this.label_prompt.Size = new System.Drawing.Size(64, 13);
            this.label_prompt.TabIndex = 22;
            this.label_prompt.Text = "BBS Prompt";
            // 
            // textBox_username_prompt
            // 
            this.textBox_username_prompt.Location = new System.Drawing.Point(158, 253);
            this.textBox_username_prompt.Name = "textBox_username_prompt";
            this.textBox_username_prompt.Size = new System.Drawing.Size(109, 20);
            this.textBox_username_prompt.TabIndex = 25;
            // 
            // label_username_prompt
            // 
            this.label_username_prompt.AutoSize = true;
            this.label_username_prompt.Location = new System.Drawing.Point(35, 253);
            this.label_username_prompt.Name = "label_username_prompt";
            this.label_username_prompt.Size = new System.Drawing.Size(117, 13);
            this.label_username_prompt.TabIndex = 24;
            this.label_username_prompt.Text = "BBS UserName Prompt";
            // 
            // textBox_password_prompt
            // 
            this.textBox_password_prompt.Location = new System.Drawing.Point(176, 286);
            this.textBox_password_prompt.Name = "textBox_password_prompt";
            this.textBox_password_prompt.Size = new System.Drawing.Size(109, 20);
            this.textBox_password_prompt.TabIndex = 27;
            // 
            // label_password_prompt
            // 
            this.label_password_prompt.AutoSize = true;
            this.label_password_prompt.Location = new System.Drawing.Point(35, 286);
            this.label_password_prompt.Name = "label_password_prompt";
            this.label_password_prompt.Size = new System.Drawing.Size(135, 13);
            this.label_password_prompt.TabIndex = 26;
            this.label_password_prompt.Text = "BBS UserPassword Prompt";
            // 
            // IP_Form
            // 
            this.AcceptButton = this.Done_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_button;
            this.ClientSize = new System.Drawing.Size(314, 356);
            this.Controls.Add(this.textBox_password_prompt);
            this.Controls.Add(this.label_password_prompt);
            this.Controls.Add(this.textBox_username_prompt);
            this.Controls.Add(this.label_username_prompt);
            this.Controls.Add(this.textBox_prompt);
            this.Controls.Add(this.label_prompt);
            this.Controls.Add(this.echo_comboBox);
            this.Controls.Add(this.label_echo);
            this.Controls.Add(this.textBox_password);
            this.Controls.Add(this.label_password);
            this.Controls.Add(this.textBox_start);
            this.Controls.Add(this.textBox_bbs);
            this.Controls.Add(this.label_start);
            this.Controls.Add(this.label_bbs);
            this.Controls.Add(this.Cancel_button);
            this.Controls.Add(this.Done_button);
            this.Controls.Add(this.textBox_mycall);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.textBox_ip);
            this.Controls.Add(this.label_mycall);
            this.Controls.Add(this.label_port);
            this.Controls.Add(this.label_ip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IP_Form";
            this.Text = "IP Config";
            this.Load += new System.EventHandler(this.IP_Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_ip;
        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.Button Done_button;
        private System.Windows.Forms.Button Cancel_button;
        private System.Windows.Forms.Label label_port;
        private System.Windows.Forms.Label label_mycall;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.TextBox textBox_mycall;
        private System.Windows.Forms.TextBox textBox_start;
        private System.Windows.Forms.TextBox textBox_bbs;
        private System.Windows.Forms.Label label_start;
        private System.Windows.Forms.Label label_bbs;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Label label_password;
        private System.Windows.Forms.Label label_echo;
        private System.Windows.Forms.ComboBox echo_comboBox;
        private System.Windows.Forms.TextBox textBox_prompt;
        private System.Windows.Forms.Label label_prompt;
        private System.Windows.Forms.TextBox textBox_username_prompt;
        private System.Windows.Forms.Label label_username_prompt;
        private System.Windows.Forms.TextBox textBox_password_prompt;
        private System.Windows.Forms.Label label_password_prompt;
    }
}