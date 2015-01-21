using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PacketComs
{
    public partial class Reply : Form
    {
        public Reply()
        {
            InitializeComponent();
        }

        private void Reply_Load(object sender, EventArgs e)
        {
            reply_richTextBox.Width = Width - 50;
            reply_richTextBox.Left = 20;
            reply_richTextBox.Width = Width - 55;
            reply_richTextBox.Top = 20;
            reply_richTextBox.Height = Height - 115;
            send_button.Left = (Width - 150)/3;
            cancel_button.Left = (((Width - 150)/3)*2) + 75;
            send_button.Top = reply_richTextBox.Bottom + 20;
            cancel_button.Top = reply_richTextBox.Bottom + 20;
        }

        private void Reply_Resize(object sender, EventArgs e)
        {
            reply_richTextBox.Width = Width - 50;
            reply_richTextBox.Left = 20;
            reply_richTextBox.Width = Width - 55;
            reply_richTextBox.Top = 20;
            reply_richTextBox.Height = Height - 115;
            send_button.Left = (Width - 150) / 3;
            cancel_button.Left = (((Width - 150) / 3) * 2) + 75;
            send_button.Top = reply_richTextBox.Bottom + 20;
            cancel_button.Top = reply_richTextBox.Bottom + 20;
        }
    }
}
