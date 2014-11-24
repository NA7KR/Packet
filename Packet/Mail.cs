using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using PacketComs;

namespace Packet
{

    public partial class Mail : Form
    {
        private static readonly ModifyFile _myFiles = new ModifyFile();
        public Mail()
        {
            InitializeComponent();
        }

        #region OK
        private void button_OK_Click(object sender, EventArgs e)
        {
            int i;
            
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
                string[] rxmsg = new string[lines.Length];
                string[] rxtsld = new string[lines.Length];
                string[] rxsize = new string[lines.Length];
                string[] rxto = new string[lines.Length];
                string[] rxroute = new string[lines.Length];
                string[] rxfrom = new string[lines.Length];
                string[] rxdate = new string[lines.Length];
                string[] rxsubject = new string[lines.Length];

                for (i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    rxmsg[i] = Mid(line, 0, 4);
                    rxtsld[i] = Mid(line, 7, 4);
                    rxsize[i] = Mid(line, 13, 5);
                    rxto[i] = Mid(line, 18, 6);
                    rxroute[i] = Mid(line, 24, 8);
                    rxfrom[i] = Mid(line, 32, 7);
                    rxdate[i] = Mid(line, 39, 9);
                    rxsubject[i] = Mid(line, 48, (line.Length - 48));
                    
                    // Invoke((Action)delegate { richTextBox1.Text = myString; });
                    DataGridView1.Rows.Add(rxmsg[i], rxtsld[i], rxsize[i], rxto[i], rxroute[i], rxfrom[i], rxdate[i], rxsubject[i]);
                   
                  
                }



                _myFiles.WriteST(RemovePepeatWords("SortTo", rxto), "SortTo");
                // _myFiles.WriteST(RXROUTE[i] + Environment.NewLine, "SortRoute");
                // _myFiles.WriteST(RXFROM[i] + Environment.NewLine, "SortFrom");
                // _myFiles.WriteST(RXSUBJECT[i] + Environment.NewLine, "SortSubject");
                
            }
            catch (IOException ex)

            {
                MessageBox.Show("Error in file read" + " " + ex.Source);

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

        #region RemovePepeatWords
        public static string RemovePepeatWords( string File_Name, Array Array_Name)
        {
            //read file
            string str1 = _myFiles.RXST(File_Name);
            if (str1 == null)
            { }
            else
            {
                str1 = str1.Replace("\r", ",").Replace("\r", ",");
            }
            //
            foreach (string str in Array_Name)
            {
                
                str1 = str + "," + str1;
            }

            Dictionary<string, bool> listofUniqueWords = new Dictionary<string, bool>();
            StringBuilder destBuilder = new StringBuilder();
            string[] spilltedwords = str1.Split(new[] { ' ', ',', ';', '.'}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in spilltedwords)
            {
                if (!listofUniqueWords.ContainsKey(item))
                {
                    destBuilder.Append(item).Append("\n\r");
                    listofUniqueWords.Add(item, true);
                }
            }
 
           return destBuilder.ToString().Trim();
            
        }
        #endregion
    }
}