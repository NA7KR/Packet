using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PacketComs;

namespace Packet
	{
	class SQL
		{

		private OdbcCommand sqlComm;

		public SQL()
			{

			}



		#region SQLSELECT
		public List<DtoPacket> Sqlselect(string dsnName)
			{
			List<DtoPacket> packets = new List<DtoPacket>();

			try
				{
				var sqlConn = new OdbcConnection(dsnName);
				sqlConn.Open();
				sqlComm = sqlConn.CreateCommand();

				using (OdbcCommand cmd = new OdbcCommand())
					{
					cmd.Connection = sqlConn;
					sqlComm.CommandText = "SELECT * FROM Packet ";
					sqlComm.ExecuteNonQuery();
					using (OdbcDataReader reader = sqlComm.ExecuteReader())
						{
						while (reader.Read())
						{
							DtoPacket packet = new DtoPacket((int) reader.GetValue(0),
								(string) reader.GetValue(1),
								(int) reader.GetValue(2),
								(string) reader.GetValue(3),
								(string) reader.GetValue(4),
								(string) reader.GetValue(5),
								(string) reader.GetValue(6),
								(string) reader.GetValue(7),
								null);
							packets.Add(packet);
							/*
							Mail.DataGridView1.Rows.Add(
								reader.GetValue(0),
								reader.GetValue(1),
								reader.GetValue(2),
								reader.GetValue(3),
								reader.GetValue(4),
								reader.GetValue(5),
								reader.GetValue(6),
								reader.GetValue(7));
							 */

							;
							//);



							}
						}

					}
				}
			catch (OdbcException e)
				{
				MessageBox.Show(e.Message);
				}
			return packets;
			}
		#endregion
		}

	}
