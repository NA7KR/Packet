using System;
using System.Windows.Forms;
using PacketComs;

namespace Packet
{
    public partial class Reply : Form
    {
        private static readonly FileSql MyFiles = new FileSql();
        private static readonly Sql Sql = new Sql();
        private int _msgnumber;
        private string _key;
        private string _tsld;
        private string _to;
        private string _from ;
        private int _count;

        #region constructor
        public Reply(int msg,string tsld, string to, string from)
        {
            _tsld =tsld ;
            _to = to;
            _from = from;
            _msgnumber = msg;
            InitializeComponent();
        }
        #endregion

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
            Text = "Reply to MSG # " + _msgnumber;
            MyFiles.ReplyMakeTable("Packet");
            var lastNumber = (Convert.ToInt32(_msgnumber) % 10).ToString();
            var myString = Sql.Rxst(_msgnumber.ToString(), lastNumber)
                .Replace(Environment.NewLine, Environment.NewLine + "> ");
            reply_richTextBox.Text = Environment.NewLine + Environment.NewLine + "> " + myString;
          
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
            send_button.Left = (Width - 150)/3;
            cancel_button.Left = (((Width - 150)/3)*2) + 75;
            send_button.Top = reply_richTextBox.Bottom + 20;
            cancel_button.Top = reply_richTextBox.Bottom + 20;
            
        }

        #endregion

        #region Button click
        private void send_button_Click(object sender, EventArgs e)
        {
            var status = "Y";
            var filename = _msgnumber + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm");
            MyFiles.WriteSt(reply_richTextBox.Text, filename, "Send", true);
            Sql.WriteSqlReplyUpdate( filename, status ,_msgnumber, _tsld , _from,  _to,false  );
            Close();
        }
        #endregion

        #region keydown
        private void reply_richTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.OemQuestion)
            {
                _key += "/";
                _count++;
            }
            if (e.KeyCode == Keys.E && _count == 1)
            {
                _key += "e";
                _count++;
            }
            if (e.KeyCode == Keys.X && _count == 2)
            {
                _key += "x";
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (_key == "/ex")
                {
                    _count = 0;
                    _key = "";
                    MessageBox.Show("You have entered /ex command");
                    //Call your command function code here
                }

            }
        }
        #endregion

        #region Show form
        private void Reply_Shown(object sender, EventArgs e)
        {
            Focus();
            reply_richTextBox.SelectionStart = 0;
            
            reply_richTextBox.ScrollToCaret();
            reply_richTextBox.Focus();
        }
        #endregion

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
