#region Using Directive

using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Windows.Forms;
using PacketComs;

#endregion

namespace Packet
	{
	class Sql
		{
        DtoListMsgto select_list = new DtoListMsgto();
		private OdbcCommand _sqlComm;

		public Sql()
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
				_sqlComm = sqlConn.CreateCommand();

				using (OdbcCommand cmd = new OdbcCommand())
					{
					cmd.Connection = sqlConn;
					_sqlComm.CommandText = "SELECT * FROM Packet ";
					_sqlComm.ExecuteNonQuery();
					using (OdbcDataReader reader = _sqlComm.ExecuteReader())
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

        #region SQLSELECT_ON_Lists_Msgto
        public List<DtoListMsgto> SQLSELECT_ON_Lists_Msgto(string dsnName, string tableName)
			{
			List<DtoListMsgto> select_lists = new List<DtoListMsgto>();
			try
				{
				var sqlConn = new OdbcConnection(dsnName);
				sqlConn.Open();
				_sqlComm = sqlConn.CreateCommand();

				using (OdbcCommand cmd = new OdbcCommand())
					{
					cmd.Connection = sqlConn;
					_sqlComm.CommandText = "SELECT * FROM " + tableName;
					_sqlComm.ExecuteNonQuery();
					using (OdbcDataReader reader = _sqlComm.ExecuteReader())
						{
						while (reader.Read())
						{
						    DtoListMsgto select_list  = new DtoListMsgto(
								(string) reader.GetValue(0),
								(DateTime) reader.GetValue(1),
								(string) reader.GetValue(2));

                            select_lists.Add(select_list);
							}
						}
					}
				}
			catch (OdbcException e)
				{
				MessageBox.Show(e.Message);
				}
            return select_lists;
			}
		#endregion

		#region SQLSELECTOPTION
		public void SqlselectOptrion(string dsnName, string tableName)
			{
			try
				{
				var sqlConn = new OdbcConnection(dsnName);
				sqlConn.Open();
				_sqlComm = sqlConn.CreateCommand();
				using (OdbcCommand cmd = new OdbcCommand())
					{
					cmd.Connection = sqlConn;
					_sqlComm.CommandText = "Insert into " + tableName + " ( " + tableName + " ,DateCreate ) " +
										  " Select DISTINCT " + tableName + " , now() from Packet Where " + tableName +
										  " not in (Select " + tableName + " from " + tableName + " )";
					_sqlComm.ExecuteNonQuery();
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
