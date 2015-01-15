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
            MyFiles.SelectMakeCustomTable(  "Packet");
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

            // ID Int,Custom_Name CHAR(20), CustomTable CHAR(50), TableName CHAR(20), Enable CHAR(1)  )");
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
