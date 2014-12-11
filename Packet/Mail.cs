#region Using Directive

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using PacketComs;

#endregion

namespace Packet
{
    public partial class Mail : Form
    {
		private static readonly Sql SQL = new Sql();
        
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
                //File.Delete("path");
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
                //MyFiles.WriteSt(ToDownLoad, "ToDownLoad");
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

        

        #region Mail_Load
        public void Mail_Load(object sender, EventArgs e)
        {
            //int i;
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
                List<DtoPacket> packets = SQL.Sqlselect("DSN=Packet");
				packets.ForEach(delegate(DtoPacket packet)
				{
					DataGridView1.Rows.Add(
						packet.get_MSG(),
						packet.get_MSGTSLD(),
						packet.get_MSGSize(),
						packet.get_MSGTO(),
						packet.get_MSGRoute(),
						packet.get_MSGFrom(),
						packet.get_MSGDateTime(),
						packet.get_MSGSubject());
				}
					);
				
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
            var box = new Sort("SelectedTo", "SortTo",6,"MSGTO");
            box.ShowDialog();
        }
        #endregion
       // MSG int PRIMARY KEY, MSGTSLD CHAR(3),, MSGRoute CHAR(7),MSGFrom CHAR(6), 
        #region toolStripMenuItem Subject
        private void configToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var box = new Sort("SelectedSubject", "SortSubject", 30,"MSGSubject");
            box.ShowDialog();
        }
        #endregion

        #region toolStripMenuItem From
        private void configToolStripMenuItem1_Click(object sender, EventArgs e)
        {
	        
            var box = new Sort("SelectedFrom", "SortFrom",6, "MSGFrom");
            box.ShowDialog();
        }
        #endregion

        #region toolStripMenuItem Roure
        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var box = new Sort("SelectedRoute", "SortRoute",7, "MSGRoute");
            box.ShowDialog();
        }
        #endregion

        
    }
}