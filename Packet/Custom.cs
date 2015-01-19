#region Using Directive

using System;
using System.Windows.Forms;
using PacketComs;

#endregion

namespace Packet
{
    public partial class Custom : Form
    {
        
        private static readonly FileSql MyFiles = new FileSql();
        private static readonly Sql Sql = new Sql();
        private string textID;
        private string nameID;
        private string queryID;
        private string tableID;
        private string enableID;

        public Custom()
        {
            InitializeComponent();
        }

        #region Custom Loader
        private void Custom_Load(object sender, EventArgs e)
        {
            DataGridView1.Columns.Add("ID", "ID");
            DataGridView1.Columns.Add("CustomName", "CustomName");
            DataGridView1.Columns.Add("CustomQuery", "CustomQuery");
            DataGridView1.Columns.Add("TableName", "TableName");
            DataGridView1.Columns.Add("Enable", "Enable");
            DataGridView1.Columns[0].Width = 50;
            DataGridView1.Columns[1].Width = 200;
            DataGridView1.Columns[2].Width = 200;
            DataGridView1.Columns[3].Width = 100;
            DataGridView1.Columns[4].Width = 50;
            DataGridView1.Width = DataGridView1.Columns[0].Width + DataGridView1.Columns[1].Width +
                                  DataGridView1.Columns[2].Width + DataGridView1.Columns[3].Width +
                                  DataGridView1.Columns[4].Width + 48;

            DataGridView1.Visible = true;
            MSGFrom_radioButton.Left = 20;
            MSGRoute_radioButton.Left = 20;
            MSGTSLD_radioButton.Left = 20;
            MSGSubject_radioButton.Left = 20;

            MSGFrom_radioButton.Top = 20;
            MSGTSLD_radioButton.Top = 40;
            MSGRoute_radioButton.Top = 60;
            MSGSubject_radioButton.Top = 80;
            Width = DataGridView1.Right + 48;
            Loader();
        }
        #endregion

        #region Loader

        private void Loader()
        {
            if (MyFiles.SelectMakeCustomQuery("Packet") == false)
            {
                Sql.WriteSqlCustomUpdate(1, "7+", "7+", "MSGSubject", "Y");
            }
           
            DataGridView1.Rows.Clear();
            var packets = Sql.SqlselectCustom();

            packets.ForEach(delegate(DtoCustom packet)
            {
                DataGridView1.Rows.Add(
                    packet.get_ID(),
                    packet.get_CustomName(),
                    packet.get_CustomQuery(),
                    packet.get_TableName(),
                    packet.get_Enable());

            }
                );
            
        }


        #endregion

        private void Custom_Resize(object sender, EventArgs e)
        {
            MSGFrom_radioButton.Left = 20;
            MSGRoute_radioButton.Left = 20;
            MSGTSLD_radioButton.Left = 20;
            MSGSubject_radioButton.Left = 20;

            MSGFrom_radioButton.Top = 20;
            MSGTSLD_radioButton.Top = 40;
            MSGRoute_radioButton.Top = 60;
            MSGSubject_radioButton.Top = 80;
            Width = DataGridView1.Right + 48;
        }

        private void OK_button_Click(object sender, EventArgs e)
        {
            Sql.WriteSqlCustomUpdate(0, "9+","79+", "MSGSubject", "Y");
            Loader();
        }

       

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textID = DataGridView1[0, e.RowIndex].Value.ToString();
            nameID = DataGridView1[1, e.RowIndex].Value.ToString();
            queryID = DataGridView1[2, e.RowIndex].Value.ToString();
            tableID = DataGridView1[3, e.RowIndex].Value.ToString();
            enableID = DataGridView1[4, e.RowIndex].Value.ToString();
           
           
        }

        private void edit_button_Click(object sender, EventArgs e)
        {
            name_textBox.Text = nameID;
            Query_richTextBox.Text = queryID;
        }

      

    }
}
