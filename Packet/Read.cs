using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PacketComs;

namespace Packet
{
    public partial class Read : Form
    {
        private static readonly Sql Sql = new Sql();
        public Read()
        {
            InitializeComponent();
        }

        private void Read_Load(object sender, EventArgs e)
        {
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
                DataGridView1.Columns.Add("RXSTATE", "STATE");

                DataGridView1.Columns[0].Width = 80;
                DataGridView1.Columns[1].Width = 60;
                DataGridView1.Columns[2].Width = 70;
                DataGridView1.Columns[3].Width = 80;
                DataGridView1.Columns[4].Width = 100;
                DataGridView1.Columns[5].Width = 80;
                DataGridView1.Columns[6].Width = 120;
                DataGridView1.Columns[7].Width = 400;
                DataGridView1.Columns[8].Width = 80;
                DataGridView1.Width = DataGridView1.Columns[0].Width + DataGridView1.Columns[1].Width +
                                      DataGridView1.Columns[2].Width + DataGridView1.Columns[3].Width +
                                      DataGridView1.Columns[4].Width + DataGridView1.Columns[5].Width +
                                      DataGridView1.Columns[6].Width + DataGridView1.Columns[7].Width + DataGridView1.Columns[8].Width + 60;
                DataGridView1.Left = 10;
                Width = DataGridView1.Width + 50;
                DataGridView1.Visible = true;
                DataGridView1.Rows.Clear();
                var packets = Sql.SqlselectRD();
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
                        packet.get_MSGSubject(),
                        packet.get_MSGState() ) ;
                    foreach (DataGridViewRow row in DataGridView1.Rows)
                    {
                        string RowType = row.Cells[0].Value.ToString();

                        if (packet.get_MSGState().Trim() == "R")
                        {
                            row.DefaultCellStyle.BackColor = Color.Red;
                            row.DefaultCellStyle.ForeColor = Color.White;
                        }
                        if (packet.get_MSGState().Trim() == "P")
                        {
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                            row.DefaultCellStyle.ForeColor = Color.Black;
                        }
                    }
                }
                    );
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error in file read" + " " + ex.Source);
            }
        }
                                                          
        private void Read_Resize(object sender, EventArgs e)
        {
            {
                DataGridView1.Left = 10;
                DataGridView1.Width = Width - 40;
                DataGridView1.Top = 30;
                DataGridView1.Height = Height - 150;
                button_Cancel.Top = DataGridView1.Height + 50;
                button_OK.Top = DataGridView1.Height + 50;
                button_Cancel.Left = ((Width / 2) + (button_OK.Width / 2));
                button_OK.Left = ((Width / 3) - (button_OK.Width / 2));
            } 
        }
    }
}
