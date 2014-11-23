using System;
using System.Windows.Forms;
using PacketComs;

namespace Packet
{

    public partial class Mail : Form
    {
        private readonly ModifyFile _myFiles = new ModifyFile();
        public Mail()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            try
            {


                string myString = _myFiles.RX();
                string[] lines = myString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                string[] RXMSG = new string[lines.Length];
                string[] RXTSLD = new string[lines.Length];
                string[] RXSIZE = new string[lines.Length];
                string[] RXTO = new string[lines.Length];
                string[] RXROUTE = new string[lines.Length];
                string[] RXFROM = new string[lines.Length];
                string[] RXDATE = new string[lines.Length];
                string[] RXSUBJECT = new string[lines.Length];
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    RXMSG[i] = Mid(line, 0, 4);
                    RXTSLD[i] = Mid(line, 7, 4);
                    RXSIZE[i] = Mid(line, 13, 5);
                    RXTO[i] = Mid(line, 18, 6);
                    RXROUTE[i] = Mid(line, 24, 8);
                    RXFROM[i] = Mid(line, 32, 7);
                    RXDATE[i] = Mid(line, 39, 9);
                    RXSUBJECT[i] = Mid(line, 48, line.Length);
                    // Invoke((Action)delegate { richTextBox1.Text = myString; });

                }
            }
            catch
            {
                DialogResult dialogResult = MessageBox.Show("Error in file read");

            }
        }

        public static string Mid(string param, int startIndex, int length)
        {
            string result = param.Substring(startIndex, length);
            return result;
        }

    }
}