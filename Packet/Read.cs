#region Using Directive

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#endregion

namespace Packet
{
    public partial class Read : Form
    {
        private static readonly Sql Sql = new Sql();
        private bool _selectedCkeck;
        private Int32 _textId;
        private string _tsld;
        private string _to;
        private string _from;

        #region Read

        public Read()
        {
            InitializeComponent();
        }

        #endregion

        #region Read_Load

        private void Read_Load(object sender, EventArgs e)
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
                                  DataGridView1.Columns[6].Width + DataGridView1.Columns[7].Width +
                                  DataGridView1.Columns[8].Width + 60;
            DataGridView1.Left = 10;
            Width = DataGridView1.Width + 50;
            _selectedCkeck = false;
            Loader();
        }

        #endregion

        #region Loader

        private void Loader()
        {
             
            try
            {
               
                DataGridView1.Visible = true;
                DataGridView1.Rows.Clear();
                var packets = Sql.SqlselectRd();
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
                        packet.get_MSGState());
                }
                    );
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error in file read" + " " + ex.Source);
            }
        }

        #endregion

        #region resize

        private void Read_Resize(object sender, EventArgs e)
        {
            {
                DataGridView1.Left = 10;
                DataGridView1.Width = Width - 40;
                DataGridView1.Top = 30;
                DataGridView1.Height = Height - 150;
                button_Cancel.Top = DataGridView1.Height + 50;
                button_OK.Top = DataGridView1.Height + 50;
                button_Relpy.Top = DataGridView1.Height + 50;
                button_Delete.Top = DataGridView1.Height + 50;

                button_OK.Left = ((Width - (button_OK.Width*4))/5);
                button_Cancel.Left = (((Width - (button_OK.Width*4))/5) + button_OK.Right);
                button_Relpy.Left = (((Width - (button_OK.Width*4))/5) + button_Cancel.Right);
                button_Delete.Left = (((Width - (button_OK.Width*4))/5) + button_Relpy.Right);

                richTextBox1.Left = 10;
                richTextBox1.Width = Width - 40;
                richTextBox1.Top = 30;
                richTextBox1.Height = Height - 150;
            }
        }

        #endregion

        #region RowPost

        private void DataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            foreach (DataGridViewRow row in DataGridView1.Rows)
            {
                var rowType = row.Cells[8].Value.ToString();

                if (rowType.Trim() == "P")
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
                else if (rowType.Trim() == "R")
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (rowType.Trim() == "V")
                {
                    row.DefaultCellStyle.BackColor = Color.Gray;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        #endregion

        #region CellContentDoubleClick

        private void DataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                var number = DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                var check = DataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                if (check.Trim() == "R")
                {
                    var lastNumber = (Convert.ToInt32(number)%10).ToString();
                    richTextBox1.Text = Sql.Rxst(number, lastNumber);
                    DataGridView1.Visible = false;
                    richTextBox1.Visible = true;
                    Sql.WriteSqlPacketUpdate(Convert.ToInt32(number), "V");
                }
                if (check.Trim() == "P")
                {
                    MessageBox.Show("Not downloaded yet");
                }
                Loader();
            }
        }

        #endregion

        #region OK

        private void button_OK_Click(object sender, EventArgs e)
        {
            DataGridView1.Visible = true;
            richTextBox1.Visible = false;
            _selectedCkeck = false;
        }

        #endregion

        #region Delete

        private void button_Delete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow drv in DataGridView1.SelectedRows)
            {
                var number = drv.Cells[0].Value.ToString();
                var lastNumber = (Convert.ToInt32(number)%10).ToString();
                Sql.DeleteSt(number, lastNumber);
                Sql.DeleteRow("Packet", "MSG", number);
            }
            _selectedCkeck = false;
            Loader();
        }

        #endregion

        #region Reply
        private void button_Relpy_Click(object sender, EventArgs e)
        {
            if (_selectedCkeck)
            {
                
                var box = new Reply(_textId,_tsld,_to,_from);
                box.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select a message first");
            }
        }
        #endregion

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            _textId = Convert.ToInt32(DataGridView1[0, e.RowIndex].Value);
            _tsld = (DataGridView1[1, e.RowIndex].Value).ToString();
            _to = (DataGridView1[3, e.RowIndex].Value).ToString();
            _from  = (DataGridView1[5, e.RowIndex].Value).ToString();
            _tsld = _tsld.Substring(0, 1);
            _selectedCkeck = true;
        }
    }
}
