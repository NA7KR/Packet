namespace Packet
{
    partial class Reply
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
            this.cancel_button = new System.Windows.Forms.Button();
            this.send_button = new System.Windows.Forms.Button();
            this.reply_richTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // cancel_button
            // 
            this.cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel_button.Location = new System.Drawing.Point(308, 402);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 0;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            // 
            // send_button
            // 
            this.send_button.Location = new System.Drawing.Point(161, 402);
            this.send_button.Name = "send_button";
            this.send_button.Size = new System.Drawing.Size(75, 23);
            this.send_button.TabIndex = 1;
            this.send_button.Text = "Send";
            this.send_button.UseVisualStyleBackColor = true;
            // 
            // reply_richTextBox
            // 
            this.reply_richTextBox.Location = new System.Drawing.Point(12, 12);
            this.reply_richTextBox.Name = "reply_richTextBox";
            this.reply_richTextBox.Size = new System.Drawing.Size(629, 355);
            this.reply_richTextBox.TabIndex = 2;
            this.reply_richTextBox.Text = "Reply";
            // 
            // Reply
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel_button;
            this.ClientSize = new System.Drawing.Size(653, 459);
            this.Controls.Add(this.reply_richTextBox);
            this.Controls.Add(this.send_button);
            this.Controls.Add(this.cancel_button);
            this.Name = "Reply";
            this.Text = "Reply";
            this.Load += new System.EventHandler(this.Reply_Load);
            this.Resize += new System.EventHandler(this.Reply_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.Button send_button;
        private System.Windows.Forms.RichTextBox reply_richTextBox;
    }
}