using System;
using System.IO;
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

        #region OK
        private void button_OK_Click(object sender, EventArgs e)
        {
            int i = 0;
            
            try
            {
                
                DataGridView1.Columns.Add("RXMSG", "MSG");
                DataGridView1.Columns.Add("RXTSLD", "TSLD");
                DataGridView1.Columns.Add("RXSIZE", "SIZE");
                DataGridView1.Columns.Add("RXTO", "TO");
                DataGridView1.Columns.Add("RXROUTE", "ROUTE");
                DataGridView1.Columns.Add("RXFROM", "FROM");
                DataGridView1.Columns.Add("RXDATE", "DATE");
                DataGridView1.Columns.Add("RXSUBJECT", "SUBJECT");

                
                DataGridView1.Columns[0].Width = 80;
                DataGridView1.Columns[1].Width = 60;
                DataGridView1.Columns[2].Width = 70;
                DataGridView1.Columns[3].Width = 80;
                DataGridView1.Columns[4].Width = 100;
                DataGridView1.Columns[5].Width = 80;
                DataGridView1.Columns[6].Width = 120;
                DataGridView1.Columns[7].Width = 400;
                DataGridView1.Width = DataGridView1.Columns[0].Width + DataGridView1.Columns[1].Width +
                                      DataGridView1.Columns[2].Width + DataGridView1.Columns[3].Width +
                                      DataGridView1.Columns[4].Width + DataGridView1.Columns[5].Width +
                                      DataGridView1.Columns[6].Width + DataGridView1.Columns[7].Width + 60;

                DataGridView1.Visible = true;
                    
                string myString = _myFiles.RX();
                string[] lines = myString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                string[] RXMSG = new string[lines.Length];
                string[] RXTSLD = new string[lines.Length];
                string[] RXSIZE = new string[lines.Length];
                string[] RXTO = new string[lines.Length];
                string[] RXROUTE = new string[lines.Length];
                string[] RXFROM = new string[lines.Length];
                string[] RXDATE = new string[lines.Length];
                string[] RXSUBJECT = new string[lines.Length];

                for (i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    RXMSG[i] = Mid(line, 0, 4);
                    RXTSLD[i] = Mid(line, 7, 4);
                    RXSIZE[i] = Mid(line, 13, 5);
                    RXTO[i] = Mid(line, 18, 6);
                    RXROUTE[i] = Mid(line, 24, 8);
                    RXFROM[i] = Mid(line, 32, 7);
                    RXDATE[i] = Mid(line, 39, 9);
                    RXSUBJECT[i] = Mid(line, 48, (line.Length - 48));
                    
                    // Invoke((Action)delegate { richTextBox1.Text = myString; });
                    DataGridView1.Rows.Add(RXMSG[i], RXTSLD[i], RXSIZE[i], RXTO[i], RXROUTE[i], RXFROM[i], RXDATE[i], RXSUBJECT[i]);
                  
                }
                backgroundWorker1.RunWorkerAsync();//this invokes the DoWork event
            }
            catch (IOException ex)

            {
                DialogResult dialogResult = MessageBox.Show("Error in file read" + " " + ex.Source);

            }
        }
        #endregion

        #region Mid
        public static string Mid(string param, int startIndex, int length)
        {
                if (param == "")
                {
                    return null;
                }
                string result = param.Substring(startIndex, length);
                return result;
        }
        #endregion

        #region cancel
        private void button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            // _myFiles.WriteST(RXTO[i]+ Environment.NewLine, "SortTo");
            // _myFiles.WriteST(RXROUTE[i] + Environment.NewLine, "SortRoute");
            // _myFiles.WriteST(RXFROM[i] + Environment.NewLine, "SortFrom");
            // _myFiles.WriteST(RXSUBJECT[i] + Environment.NewLine, "SortSubject");
        }

     

    }
}