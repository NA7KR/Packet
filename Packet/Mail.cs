#region Using Directive

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using PacketComs;

#endregion

namespace Packet
{
	public partial class Mail : Form
	{
		private static readonly Sql Sql = new Sql();
        ModifyRegistry _myRegistryKeep = new ModifyRegistry();
         

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
				foreach (DataGridViewRow drv in DataGridView1.SelectedRows)
				{
					var ToDownLoad = drv.Cells[0].Value.ToString();
					var inumber = Convert.ToInt32(ToDownLoad);
					Sql.WriteSqlPacketUpdate(inumber, "P");
				}	
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
		    try
		    {
                toolStripComboBoxTime.Items.Clear();
                toolStripComboBoxTime.Items.Add("Off");
                toolStripComboBoxTime.Items.Add("30");
                toolStripComboBoxTime.Items.Add("60");
                toolStripComboBoxTime.Items.Add("90");
                toolStripComboBoxTime.Items.Add("180");

                toolStripComboBoxQTY.Items.Clear();
                toolStripComboBoxQTY.Items.Add("Off");
                toolStripComboBoxQTY.Items.Add("100");
                toolStripComboBoxQTY.Items.Add("500");
                toolStripComboBoxQTY.Items.Add("1000");
                toolStripComboBoxQTY.Items.Add("1500");
                toolStripComboBoxQTY.Items.Add("2000");
                toolStripComboBoxQTY.Items.Add("5000");
                toolStripComboBoxQTY.Items.Add("10000");
                toolStripComboBoxQTY.Items.Add("20000");

                _myRegistryKeep.SubKey = "SOFTWARE\\NA7KR\\Packet\\Keep";
                _myRegistryKeep.ShowError = true;

		        int idays = _myRegistryKeep.ReadDW("DaystoKeep");
		        Sql.deletedays(idays);
                    
                Loader();
		    }
		    catch (Exception)
		    {
		        
		        throw;
		    }
           
        }
        #endregion

        #region Loader
        private void Loader()
        {
            _myRegistryKeep.SubKey = "SOFTWARE\\NA7KR\\Packet\\Keep";
            _myRegistryKeep.ShowError = true;
            
          

            int QTYtoKeep = _myRegistryKeep.ReadDW("QTYtoKeep");
            if (QTYtoKeep == 0)
            {
                toolStripComboBoxQTY.SelectedIndex = 0;   
            }
            else if (QTYtoKeep == 100)
            {
                toolStripComboBoxQTY.SelectedIndex = 1; 
            }
            else if (QTYtoKeep == 500)
            {
                toolStripComboBoxQTY.SelectedIndex = 2;
            }
            else if (QTYtoKeep == 1000)
            {
                toolStripComboBoxQTY.SelectedIndex = 3;
            }
            else if (QTYtoKeep == 1500)
            {
                toolStripComboBoxQTY.SelectedIndex = 4;
            }
            else if (QTYtoKeep == 2000)
            {
                toolStripComboBoxQTY.SelectedIndex = 5;
            }
            else if (QTYtoKeep == 5000)
            {
                toolStripComboBoxQTY.SelectedIndex = 6;
            }
            else if (QTYtoKeep == 10000)
            {
                toolStripComboBoxQTY.SelectedIndex = 7;
            }
            else if (QTYtoKeep == 20000)
            {
                toolStripComboBoxQTY.SelectedIndex = 8;
            }
            else
            {
                _myRegistryKeep.Write("DaystoKeep", 0);
            }

            int DaystoKeep = _myRegistryKeep.ReadDW("DaystoKeep");
            if (DaystoKeep == 0)
            {
                toolStripComboBoxTime.SelectedIndex = 0;
            }
            else if (DaystoKeep == 30)
            {
                toolStripComboBoxTime.SelectedIndex = 1;
            }
            else if (DaystoKeep == 60)
            {
                toolStripComboBoxTime.SelectedIndex = 2;
            }
            else if (DaystoKeep == 90)
            {
                toolStripComboBoxTime.SelectedIndex = 3;
            }
            else if (DaystoKeep == 180)
            {
                toolStripComboBoxTime.SelectedIndex = 4;
            }
            else
            {
                _myRegistryKeep.Write("DaystoKeep", 0);
            }

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
				var packets = Sql.Sqlselect();
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

		#region TO Y

		private void toolStripMenuItemTO_Click(object sender, EventArgs e)
		{
			var box = new Sort(6, "MSGTO", 'Y');
			box.ShowDialog();
		}

		#endregion

		#region From Y

		private void configToolStripMenuItem1_Click_1(object sender, EventArgs e)
		{
			var box = new Sort(6, "MSGFrom", 'Y');
			box.ShowDialog();
		}

		#endregion

		#region Route Y

		private void configToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			var box = new Sort(7, "MSGRoute", 'Y');
			box.ShowDialog();
		}

		#endregion

		#region Subject Y

		private void configToolStripMenuItem2_Click_1(object sender, EventArgs e)
		{
			var box = new Sort(30, "MSGSubject", 'Y');
			box.ShowDialog();
		}

		#endregion

		#region To D

		private void toolStripMenuItem5_Click(object sender, EventArgs e)
		{
			var box = new Sort(6, "MSGTO", 'D');
			box.ShowDialog();
		}

		#endregion

		#region From D

		private void toolStripMenuItem7_Click(object sender, EventArgs e)
		{
			var box = new Sort(6, "MSGFrom", 'D');
			box.ShowDialog();
		}

		#endregion

		#region Route D

		private void toolStripMenuItem9_Click(object sender, EventArgs e)
		{
			var box = new Sort(7, "MSGRoute", 'D');
			box.ShowDialog();
		}

		#endregion

		#region Subject D

		private void toolStripMenuItem2_Click(object sender, EventArgs e)
		{
			var box = new Sort(30, "MSGSubject", 'D');
			box.ShowDialog();
		}

		#endregion

        #region Resize
        private void Mail_Resize(object sender, EventArgs e)
        {
            DataGridView1.Left = 10;
            DataGridView1.Width = Width - 40;
            DataGridView1.Top = 30;
            DataGridView1.Height = Height - 150;
            button_Cancel.Top = DataGridView1.Height + 50;
            button_OK.Top = DataGridView1.Height + 50;
            button_Cancel.Left = ((Width/2) + (button_OK.Width/2));
            button_OK.Left = ((Width / 3 ) - (button_OK.Width / 2));
        }
        #endregion

 
        #region Date 
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (toolStripComboBoxTime.SelectedIndex)
            {
                case 0:
                    _myRegistryKeep.Write("DaystoKeep", 0);
                    break;
                case 1:
                    _myRegistryKeep.Write("DaystoKeep", 30);
                    break;
                case 2:
                    _myRegistryKeep.Write("DaystoKeep", 60);
                    break;
                case 4:
                    _myRegistryKeep.Write("DaystoKeep", 90);
                    
                    break;
                case 5:
                    _myRegistryKeep.Write("DaystoKeep", 180);
                    break;
                
            }
            Loader();
        }
        #endregion

        #region  Clear
        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {

           DialogResult result1 = MessageBox.Show("Sure you want to do this?", "Important Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result1 == DialogResult.Yes )
            {
                Sql.Sqlupdateclear("MSGTO");
                Sql.Sqlupdateclear("MSGFrom");
                Sql.Sqlupdateclear("MSGRoute");
                Sql.Sqlupdateclear("MSGSubject");
                Close();
            }

        }
        #endregion

        #region QTY to keep
        private void toolStripComboBoxQTY_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (toolStripComboBoxQTY.SelectedIndex)
            {
                case 0:
                    _myRegistryKeep.Write("QTYtoKeep", 0);
                    break;
                case 1:
                    _myRegistryKeep.Write("QTYtoKeep", 100);
                    break;
                case 2:
                    _myRegistryKeep.Write("QTYtoKeep", 500);
                    break;
                case 4:
                    _myRegistryKeep.Write("QTYtoKeep", 1000);
                    break;
                case 5:
                    _myRegistryKeep.Write("QTYtoKeep", 1500);
                    break;
                case 6:
                    _myRegistryKeep.Write("QTYtoKeep", 2000);
                    break;
                case 7:
                    _myRegistryKeep.Write("QTYtoKeep", 5000);
                    break;
                case 8:
                    _myRegistryKeep.Write("QTYtoKeep", 10000);
                    break;
                case 9:
                    _myRegistryKeep.Write("QTYtoKeep", 20000);
                    break;
            }
            Loader();
        }
        #endregion

        #region Custom
        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var box = new Custom();
            box.ShowDialog();
        }
        #endregion
    }
}