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
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.IP_textBox = new System.Windows.Forms.TextBox();
            this.Port_textBox = new System.Windows.Forms.TextBox();
            this.CallSign_textBox = new System.Windows.Forms.TextBox();
            this.Done_button = new System.Windows.Forms.Button();
            this.Cancel_button = new System.Windows.Forms.Button();
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
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "CallSign";
            // 
            // IP_textBox
            // 
            this.IP_textBox.Location = new System.Drawing.Point(145, 31);
            this.IP_textBox.Name = "IP_textBox";
            this.IP_textBox.Size = new System.Drawing.Size(109, 20);
            this.IP_textBox.TabIndex = 5;
            // 
            // Port_textBox
            // 
            this.Port_textBox.Location = new System.Drawing.Point(145, 57);
            this.Port_textBox.Name = "Port_textBox";
            this.Port_textBox.Size = new System.Drawing.Size(109, 20);
            this.Port_textBox.TabIndex = 6;
            // 
            // CallSign_textBox
            // 
            this.CallSign_textBox.Location = new System.Drawing.Point(145, 85);
            this.CallSign_textBox.Name = "CallSign_textBox";
            this.CallSign_textBox.Size = new System.Drawing.Size(109, 20);
            this.CallSign_textBox.TabIndex = 9;
            // 
            // Done_button
            // 
            this.Done_button.Location = new System.Drawing.Point(39, 125);
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
            this.Cancel_button.Location = new System.Drawing.Point(145, 125);
            this.Cancel_button.Name = "Cancel_button";
            this.Cancel_button.Size = new System.Drawing.Size(75, 23);
            this.Cancel_button.TabIndex = 11;
            this.Cancel_button.Text = "Cancel";
            this.Cancel_button.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AcceptButton = this.Done_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_button;
            this.ClientSize = new System.Drawing.Size(315, 191);
            this.Controls.Add(this.Cancel_button);
            this.Controls.Add(this.Done_button);
            this.Controls.Add(this.CallSign_textBox);
            this.Controls.Add(this.Port_textBox);
            this.Controls.Add(this.IP_textBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.Text = "IP Config";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox IP_textBox;
        private System.Windows.Forms.TextBox Port_textBox;
        private System.Windows.Forms.TextBox CallSign_textBox;
        private System.Windows.Forms.Button Done_button;
        private System.Windows.Forms.Button Cancel_button;
    }
}