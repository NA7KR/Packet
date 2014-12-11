#region Using Directive

using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Windows.Forms;
using PacketComs;
#endregion

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

		#region SQLSELECT_ON_Lists
		public List<DtoList> SQLSELECT_ON_Lists(string dsnName, string tableName)
			{
			List<DtoList> packets = new List<DtoList>();

			try
				{
				var sqlConn = new OdbcConnection(dsnName);
				sqlConn.Open();
				sqlComm = sqlConn.CreateCommand();

				using (OdbcCommand cmd = new OdbcCommand())
					{
					cmd.Connection = sqlConn;
					sqlComm.CommandText = "SELECT * FROM " + tableName;
					sqlComm.ExecuteNonQuery();
					using (OdbcDataReader reader = sqlComm.ExecuteReader())
						{
						while (reader.Read())
						{
							//DtoList packet = new DtoList(
							//	(string) reader.GetValue(0),
							//	(string) reader.GetValue(1),
							//	(string) reader.GetValue(2));
								
							// packets.Add(packet);
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

		#region SQLSELECTOPTION
		public void SqlselectOptrion(string dsnName, string TableName)
			{
			try
				{
				var sqlConn = new OdbcConnection(dsnName);
				sqlConn.Open();
				sqlComm = sqlConn.CreateCommand();
				using (OdbcCommand cmd = new OdbcCommand())
					{
					cmd.Connection = sqlConn;
					sqlComm.CommandText = "Insert into " + TableName + " ( " + TableName + " ,DateCreate ) " +
										  " Select DISTINCT " + TableName + " , now() from Packet Where " + TableName +
										  " not in (Select " + TableName + " from " + TableName + " )";
					sqlComm.ExecuteNonQuery();
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
