using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Packet
{
    public partial class Reply : Form
    {
        private static readonly FileSql MyFiles = new FileSql();
         private static readonly Sql sql = new Sql();
        private int _msgnumber;
        public Reply(int msg)
        {
            _msgnumber = msg;
            InitializeComponent();
        }
        #region  load
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
            Text = "Reply to MSG # " + _msgnumber.ToString();
            MyFiles.ReplyMakeTable( "Packet");
            
        }
        #endregion

        #region resize
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
        #endregion

        private void send_button_Click(object sender, EventArgs e)
        {
            var _filename = "";
            var status = "";
            _filename = _msgnumber.ToString() + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm");
            MyFiles.WriteSt(reply_richTextBox.Text, _filename, "Reply");
            sql.WriteSqlReplyUpdate(1, _filename,  status);
        }
    }
}
