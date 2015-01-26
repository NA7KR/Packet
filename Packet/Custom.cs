#region Using Directive

using System;
using System.Windows.Forms;

#endregion

namespace Packet
{
    public partial class Custom : Form
    {
        
        private static readonly FileSql MyFiles = new FileSql();
        private static readonly Sql Sql = new Sql();
        private static readonly RunCustom RunCustom = new RunCustom();
        private Int32 _textId;
        private string _nameId;
        private string _queryId;
        private string _tableId;
        private string _enableId;
        private bool _selectedCkeck;

        #region Custom
        public Custom()
        {
            InitializeComponent();
        }
        #endregion

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
            _selectedCkeck = false;
            if (MyFiles.SelectMakeCustomQuery("Packet") == false)
            {
                Sql.WriteSqlCustomUpdate(1, "7+", "7+", "MSGSubject", "Y");
            }
           
            DataGridView1.Rows.Clear();
            var packets = MyFiles.SqlCustomRead();

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

        #region resize
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
        #endregion

        #region OK button
        private void OK_button_Click(object sender, EventArgs e) 
        {
            _nameId = name_textBox.Text ;
            _queryId = Query_richTextBox.Text ;

             
            if (MSGSubject_radioButton.Checked )
            {
                _tableId = "MSGSubject";

            }
            if (MSGRoute_radioButton.Checked)
            {
                _tableId = "MSGRoute";
            }
            if (MSGTSLD_radioButton.Checked)
            {
                _tableId = "MSGTSLD";
            }
            if (MSGFrom_radioButton.Checked)
            {
                _tableId = "MSGFrom";
            }
            _enableId = Enabel_checkBox.Checked ? "Y" : "N";
            

            if (OK_button.Text == "Save")
            {
                Sql.WriteSqlCustomUpdate(_textId, _nameId, _queryId, _tableId, _enableId);
            }
            else
            {
                if (name_textBox.Text != "")
                {
                    if (Query_richTextBox.Text != "")
                    {
                        _textId = 99999;
                        Sql.WriteSqlCustomUpdate(_textId, _nameId, _queryId, _tableId, _enableId);
                    }
                } 
            }
            RunCustom.RunSqlCustom();
            OK_button.Text = "OK";
            Loader();
        }
        #endregion

        #region DataGridView1_CellClick(
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _textId = Convert.ToInt32(DataGridView1[0, e.RowIndex].Value);
            _nameId = DataGridView1[1, e.RowIndex].Value.ToString().Trim();
            _queryId = DataGridView1[2, e.RowIndex].Value.ToString().Trim();
            _tableId = DataGridView1[3, e.RowIndex].Value.ToString().Trim();
            _enableId = DataGridView1[4, e.RowIndex].Value.ToString().Trim();
            _selectedCkeck = true;
        }
        #endregion

        #region button click
        private void edit_button_Click(object sender, EventArgs e)
        {
            if (_selectedCkeck)
            {

                name_textBox.Text = _nameId;
                Query_richTextBox.Text = _queryId;
                if (_tableId.Trim() == "MSGSubject")
                {
                    MSGSubject_radioButton.Checked = true;
                }
                if (_tableId.Trim() == "MSGRoure")
                {
                    MSGRoute_radioButton.Checked = true;
                }
                if (_tableId.Trim() == "MSGTSLD")
                {
                    MSGTSLD_radioButton.Checked = true;
                }
                if (_tableId.Trim() == "MSGFrom")
                {
                    MSGFrom_radioButton.Checked = true;
                }
                if (_enableId.Trim() == "Y")
                {
                    Enabel_checkBox.Checked = true;
                }
                OK_button.Text = "Save";
            }
            else
            {
                MessageBox.Show("Select to edit!");
            }
        }
        #endregion

        #region cancel
        private void Cancel_button_Click(object sender, EventArgs e)
        {
            OK_button.Text = "OK";
        }
        #endregion
    }
}
