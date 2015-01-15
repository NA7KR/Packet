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

        public Custom()
        {
            InitializeComponent();
        }

        #region Custom Loader
        private void Custom_Load(object sender, EventArgs e)
        {
            Loader();
        }
        #endregion

        #region Loader

        private void Loader()
        {
            MyFiles.SelectMakeCustomTable("Packet");
            DataGridView1.Columns.Add("ID", "ID");
            DataGridView1.Columns.Add("CustomTable", "CustomTable");
            DataGridView1.Columns.Add("TableName", "TableName");
            DataGridView1.Columns.Add("Enable", "Enable");
            DataGridView1.Columns[0].Width = 50;
            DataGridView1.Columns[1].Width = 200;
            DataGridView1.Columns[2].Width = 100;
            DataGridView1.Columns[3].Width = 50;
            DataGridView1.Width = DataGridView1.Columns[0].Width + DataGridView1.Columns[1].Width +
                                  DataGridView1.Columns[2].Width + DataGridView1.Columns[3].Width + 48;

            DataGridView1.Visible = true;
            DataGridView1.Rows.Clear();
            var packets = Sql.SqlselectCustom();
            packets.ForEach(delegate(DtoPacket packet)
            {
                DataGridView1.Rows.Add(
                    packet.get_MSG(),
                    packet.get_MSGTSLD(),
                    packet.get_MSGSize(),
                    packet.get_MSGState());

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

        }

    }
}
