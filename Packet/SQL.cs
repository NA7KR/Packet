#region Using Directive

using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Windows.Forms;
using PacketComs;

#endregion

namespace Packet
	{
	#region Class
	class Sql
		{
		DtoListMsgto _selectList = new DtoListMsgto();
		private OdbcCommand _sqlComm;

		#region constructor
		//public Sql()
		//{

		//}
		#endregion

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
							DtoPacket packet = new DtoPacket((int)reader.GetValue(0),
								(string)reader.GetValue(1),
								(int)reader.GetValue(2),
								(string)reader.GetValue(3),
								(string)reader.GetValue(4),
								(string)reader.GetValue(5),
								(string)reader.GetValue(6),
								(string)reader.GetValue(7),
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
		public List<DtoListMsgto> SQLSELECT_ON_Lists_Msgto(string dsnName)
			{
			List<DtoListMsgto> selectLists = new List<DtoListMsgto>();
			try
				{
				var sqlConn = new OdbcConnection(dsnName);
				sqlConn.Open();
				_sqlComm = sqlConn.CreateCommand();

				using (OdbcCommand cmd = new OdbcCommand())
					{
					cmd.Connection = sqlConn;
					_sqlComm.CommandText = "SELECT MSGTO,Selected FROM MSGTO";
					_sqlComm.ExecuteNonQuery();
					using (OdbcDataReader reader = _sqlComm.ExecuteReader())
						{
						while (reader.Read())
							{
							DtoListMsgto selectList = new DtoListMsgto(
								(String)convertDBNull(reader.GetValue(0)),

								(String)convertDBNull(reader.GetValue(1)));

							selectLists.Add(selectList);
							}
						}
					}
				}
			catch (OdbcException e)
				{
				MessageBox.Show(e.Message);
				}
			return selectLists;
			}
		#endregion

		#region SQLSELECT_ON_Lists_MsgFrom
		public List<DtoListMSGFrom> SQLSELECT_ON_Lists_MsgFrom(string dsnName)
			{
			List<DtoListMSGFrom> selectLists = new List<DtoListMSGFrom>();
			try
				{
				var sqlConn = new OdbcConnection(dsnName);
				sqlConn.Open();
				_sqlComm = sqlConn.CreateCommand();

				using (OdbcCommand cmd = new OdbcCommand())
					{
					cmd.Connection = sqlConn;
					_sqlComm.CommandText = "SELECT MSGFROM,Selected FROM MSGFROM";
					_sqlComm.ExecuteNonQuery();
					using (OdbcDataReader reader = _sqlComm.ExecuteReader())
						{
						while (reader.Read())
							{
							DtoListMSGFrom selectList = new DtoListMSGFrom(
								(String)convertDBNull(reader.GetValue(0)),

								(String)convertDBNull(reader.GetValue(1)));

							selectLists.Add(selectList);
							}
						}
					}
				}
			catch (OdbcException e)
				{
				MessageBox.Show(e.Message);
				}
			return selectLists;
			}
		#endregion

		#region SQLSELECT_ON_Lists_Route
		public List<DtoListMsgRoute> SQLSELECT_ON_Lists_MsgRoute(string dsnName)
			{
			List<DtoListMsgRoute> selectLists = new List<DtoListMsgRoute>();
			try
				{
				var sqlConn = new OdbcConnection(dsnName);
				sqlConn.Open();
				_sqlComm = sqlConn.CreateCommand();

				using (OdbcCommand cmd = new OdbcCommand())
					{
					cmd.Connection = sqlConn;
					_sqlComm.CommandText = "SELECT MSGROUTE,Selected FROM MSGROUTE";
					_sqlComm.ExecuteNonQuery();
					using (OdbcDataReader reader = _sqlComm.ExecuteReader())
						{
						while (reader.Read())
							{
							DtoListMsgRoute selectList = new DtoListMsgRoute(
								(String)convertDBNull(reader.GetValue(0)),

								(String)convertDBNull(reader.GetValue(1)));

							selectLists.Add(selectList);
							}
						}
					}
				}
			catch (OdbcException e)
				{
				MessageBox.Show(e.Message);
				}
			return selectLists;
			}
		#endregion

		#region SQLSELECT_ON_Lists_MsgSubject
		public List<DtoListMsgSubject> SQLSELECT_ON_Lists_MsgSubject(string dsnName)
			{
			List<DtoListMsgSubject> selectLists = new List<DtoListMsgSubject>();
			try
				{
				var sqlConn = new OdbcConnection(dsnName);
				sqlConn.Open();
				_sqlComm = sqlConn.CreateCommand();

				using (OdbcCommand cmd = new OdbcCommand())
					{
					cmd.Connection = sqlConn;
					_sqlComm.CommandText = "SELECT MSGSUBJECT,Selected FROM MSGSUBJECT";
					_sqlComm.ExecuteNonQuery();
					using (OdbcDataReader reader = _sqlComm.ExecuteReader())
						{
						while (reader.Read())
							{
							DtoListMsgSubject selectList = new DtoListMsgSubject(
								(String)convertDBNull(reader.GetValue(0)),

								(String)convertDBNull(reader.GetValue(1)));

							selectLists.Add(selectList);
							}
						}
					}
				}
			catch (OdbcException e)
				{
				MessageBox.Show(e.Message);
				}
			return selectLists;
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

		#region  convertDBNull
		private object convertDBNull(object o)
			{
			if (o is DBNull)
				{
				return null;
				}
			return o;
			}
		#endregion

		}
	#endregion CLASS

	}
