#region Using Directive

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using PacketComs;

#endregion

namespace Packet
{
    public partial class Mail : Form
    {
        private static readonly ModifyFile MyFiles = new ModifyFile();

        #region Mail InitializeComponent

        public Mail()
        {
            InitializeComponent();
        }

        #endregion

        #region OK
        private void button_OK_Click(object sender, EventArgs e)
        {
            if (DataGridView1.SelectedRows != null)
            {
                string ToDownLoad = "";
                bool firstvalue = true;
                foreach (DataGridViewRow drv in DataGridView1.SelectedRows)
                {
                    if (!firstvalue)
                    {
                        ToDownLoad += " ";
                    }
                    ToDownLoad += drv.Cells[0].Value.ToString();
                    firstvalue = false;
                }
                MyFiles.WriteST(ToDownLoad, "ToDownLoad");
            }
            Close();
        }
        #endregion

        #region Mid
        public static string Mid(string param, int startIndex, int length)
        {
            if (param == "")
            {
                return null;
            }
            var result = param.Substring(startIndex, length);
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
        public static string RemovePepeatWords(string fileName, Array arrayName)
        {
            //read file
            var str1 = MyFiles.RXST(fileName);

            //
            foreach (string str in arrayName)
            {
                str1 = str + "," + str1;
            }
            str1 = str1.Replace(Environment.NewLine, ",");
            str1 = str1.Replace("\r", ",");
            str1 = str1.Replace("\n", ",");

            str1 = str1.Replace(" ", "");

            var listofUniqueWords = new Dictionary<string, bool>();
            var destBuilder = new StringBuilder();
            var spilltedwords = str1.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            Array.Sort(spilltedwords);
            foreach (var item in spilltedwords)
            {
                if (!listofUniqueWords.ContainsKey(item))
                {
                    destBuilder.Append(item).Append(Environment.NewLine);
                    listofUniqueWords.Add(item, true);
                }
            }

            return destBuilder.ToString().Trim();
        }
        #endregion

        #region Mail_Load
        private void Mail_Load(object sender, EventArgs e)
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
                DataGridView1.Left = 10;
                Width = DataGridView1.Width + 50;
                DataGridView1.Visible = true;
                DataGridView1.Rows.Clear();
                var myString = MyFiles.RX();
                var lines = myString.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
                var rxmsg = new string[lines.Length];
                var rxtsld = new string[lines.Length];
                var rxsize = new string[lines.Length];
                var rxto = new string[lines.Length];
                var rxroute = new string[lines.Length];
                var rxfrom = new string[lines.Length];
                var rxdate = new string[lines.Length];
                var rxsubject = new string[lines.Length];

                for (i = 0; i < lines.Length; i++)
                {
                    var line = lines[i];
                    rxmsg[i] = Mid(line, 0, 4);
                    rxtsld[i] = Mid(line, 7, 4);
                    rxsize[i] = Mid(line, 13, 5);
                    rxto[i] = Mid(line, 18, 6);
                    rxroute[i] = Mid(line, 24, 8);
                    rxfrom[i] = Mid(line, 32, 7);
                    rxdate[i] = Mid(line, 39, 9);
                    rxsubject[i] = Mid(line, 48, (line.Length - 48));
                    DataGridView1.Rows.Add(rxmsg[i], rxtsld[i], rxsize[i], rxto[i], rxroute[i], rxfrom[i], rxdate[i],
                        rxsubject[i]);
                }
                MyFiles.WriteST(RemovePepeatWords("SortTo", rxto), "SortTo");
                MyFiles.WriteST(RemovePepeatWords("SortRoute", rxroute), "SortRoute");
                MyFiles.WriteST(RemovePepeatWords("SortFrom", rxfrom), "SortFrom");
                MyFiles.WriteST(RemovePepeatWords("SortSubject", rxsubject), "SortSubject");
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error in file read" + " " + ex.Source);
            }
        }
        #endregion

        #region DataGridView1_Scroll
        private void DataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            DataGridView1.Invalidate();
        }
        #endregion

        #region exitToolStripMenuItem
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region toolStripMenuItem TO
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var box = new Sort("SelectedTo", "SortTo");
            box.ShowDialog();
        }
        #endregion

        #region toolStripMenuItem Subject
        private void configToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var box = new Sort("SelectedSubject", "SortSubject");
            box.ShowDialog();
        }
        #endregion

        #region toolStripMenuItem From
        private void configToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var box = new Sort("SelectedFrom", "SortFrom");
            box.ShowDialog();
        }
        #endregion

        #region toolStripMenuItem Roure
        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var box = new Sort("SelectedRoute", "SortRoute");
            box.ShowDialog();
        }
        #endregion
    }
}