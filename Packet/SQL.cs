using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PacketComs;

namespace Packet
	{
	class SQL
		{
		private static readonly Mail Mail = new Mail();
		private OdbcCommand sqlComm;

		#region SQLSELECT
		public void Sqlselect(string dsnName)
			{
			try
				{
				var sqlConn = new OdbcConnection(dsnName);
				sqlConn.Open();
				sqlComm = sqlConn.CreateCommand();
				Mail.DataGridView1.Columns.Add("RXMSG", "MSG");
				Mail.DataGridView1.Columns.Add("RXTSLD", "TSLD");
				Mail.DataGridView1.Columns.Add("RXSIZE", "SIZE");
				Mail.DataGridView1.Columns.Add("RXTO", "TO");
				Mail.DataGridView1.Columns.Add("RXROUTE", "ROUTE");
				Mail.DataGridView1.Columns.Add("RXFROM", "FROM");
				Mail.DataGridView1.Columns.Add("RXDATE", "DATE");
				Mail.DataGridView1.Columns.Add("RXSUBJECT", "SUBJECT");

				Mail.DataGridView1.Columns[0].Width = 80;
				Mail.DataGridView1.Columns[1].Width = 60;
				Mail.DataGridView1.Columns[2].Width = 70;
				Mail.DataGridView1.Columns[3].Width = 80;
				Mail.DataGridView1.Columns[4].Width = 100;
				Mail.DataGridView1.Columns[5].Width = 80;
				Mail.DataGridView1.Columns[6].Width = 120;
				Mail.DataGridView1.Columns[7].Width = 400;
				Mail.DataGridView1.Width = Mail.DataGridView1.Columns[0].Width + Mail.DataGridView1.Columns[1].Width +
									  Mail.DataGridView1.Columns[2].Width + Mail.DataGridView1.Columns[3].Width +
									  Mail.DataGridView1.Columns[4].Width + Mail.DataGridView1.Columns[5].Width +
									  Mail.DataGridView1.Columns[6].Width + Mail.DataGridView1.Columns[7].Width + 60;
				Mail.DataGridView1.Left = 10;
				Mail.Width = Mail.DataGridView1.Width + 50;
				Mail.DataGridView1.Visible = true;
				Mail.DataGridView1.Rows.Clear();
				using (OdbcCommand cmd = new OdbcCommand())
					{
					cmd.Connection = sqlConn;
					sqlComm.CommandText = "SELECT * FROM Packet ";
					sqlComm.ExecuteNonQuery();
					using (OdbcDataReader reader = sqlComm.ExecuteReader())
						{
						while (reader.Read())
							{
								{
								Mail.DataGridView1.Rows.Add(
									reader.GetValue(0),
									reader.GetValue(1),
									reader.GetValue(2),
									reader.GetValue(3),
									reader.GetValue(4),
									reader.GetValue(5),
									reader.GetValue(6),
									reader.GetValue(7));
								}
							}
						}

					}
				}
			catch (OdbcException e)
				{
				MessageBox.Show(e.Message);
				}
			}
		#endregion
		}
	}
