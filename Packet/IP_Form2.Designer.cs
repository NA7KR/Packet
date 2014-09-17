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
            this.label1 = new System.Windows.Forms.Label();
            this.ip_textBox = new System.Windows.Forms.TextBox();
            this.Done_button = new System.Windows.Forms.Button();
            this.Cancel_button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.port_textBox = new System.Windows.Forms.TextBox();
            this.callSign_textBox = new System.Windows.Forms.TextBox();
            this.start_textBox = new System.Windows.Forms.TextBox();
            this.bbs_textBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.password_textBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP or HostName";
            this.label1.Click += new System.EventHandler(this.label1_Click);
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
            this.Done_button.Location = new System.Drawing.Point(43, 255);
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
            this.Cancel_button.Location = new System.Drawing.Point(149, 255);
            this.Cancel_button.Name = "Cancel_button";
            this.Cancel_button.Size = new System.Drawing.Size(75, 23);
            this.Cancel_button.TabIndex = 11;
            this.Cancel_button.Text = "Cancel";
            this.Cancel_button.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "My CallSign";
            // 
            // port_textBox
            // 
            this.port_textBox.Location = new System.Drawing.Point(145, 57);
            this.port_textBox.Name = "port_textBox";
            this.port_textBox.Size = new System.Drawing.Size(109, 20);
            this.port_textBox.TabIndex = 6;
            // 
            // callSign_textBox
            // 
            this.callSign_textBox.Location = new System.Drawing.Point(145, 85);
            this.callSign_textBox.Name = "callSign_textBox";
            this.callSign_textBox.Size = new System.Drawing.Size(109, 20);
            this.callSign_textBox.TabIndex = 9;
            // 
            // start_textBox
            // 
            this.start_textBox.Location = new System.Drawing.Point(145, 141);
            this.start_textBox.Name = "start_textBox";
            this.start_textBox.Size = new System.Drawing.Size(109, 20);
            this.start_textBox.TabIndex = 15;
            this.start_textBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // bbs_textBox
            // 
            this.bbs_textBox.Location = new System.Drawing.Point(145, 113);
            this.bbs_textBox.Name = "bbs_textBox";
            this.bbs_textBox.Size = new System.Drawing.Size(109, 20);
            this.bbs_textBox.TabIndex = 14;
            this.bbs_textBox.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Start Number";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "BBS";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // password_textBox
            // 
            this.password_textBox.Location = new System.Drawing.Point(145, 168);
            this.password_textBox.Name = "password_textBox";
            this.password_textBox.Size = new System.Drawing.Size(109, 20);
            this.password_textBox.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(37, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Password";
            // 
            // IP_Form2
            // 
            this.AcceptButton = this.Done_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_button;
            this.ClientSize = new System.Drawing.Size(315, 314);
            this.Controls.Add(this.password_textBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.start_textBox);
            this.Controls.Add(this.bbs_textBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Cancel_button);
            this.Controls.Add(this.Done_button);
            this.Controls.Add(this.callSign_textBox);
            this.Controls.Add(this.port_textBox);
            this.Controls.Add(this.ip_textBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IP_Form2";
            this.Text = "IP Config";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ip_textBox;
        private System.Windows.Forms.Button Done_button;
        private System.Windows.Forms.Button Cancel_button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox port_textBox;
        private System.Windows.Forms.TextBox callSign_textBox;
        private System.Windows.Forms.TextBox start_textBox;
        private System.Windows.Forms.TextBox bbs_textBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox password_textBox;
        private System.Windows.Forms.Label label7;
    }
}