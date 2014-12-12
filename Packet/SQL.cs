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
		DtoPacket packet = new DtoPacket();
		private OdbcCommand _sqlComm;
		private OdbcCommand _sqlCommSelectInsert;
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
		public List<DtoListMsgFrom> SQLSELECT_ON_Lists_MsgFrom(string dsnName)
			{
			List<DtoListMsgFrom> selectLists = new List<DtoListMsgFrom>();
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
							DtoListMsgFrom selectList = new DtoListMsgFrom(
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

		#region WriteSQLPacketUpdate
		public void WriteSQLPacketUpdate(int Value, String textValue)
			{
			try
				{
				packet.set_MSG(Value);
				packet.set_MSGState(textValue);
				SQLUPDATEPACKET(packet);
				}
			catch (Exception e)
				{
				MessageBox.Show(e.Message);
				}
			}
		#endregion

		#region SQLUPDATEPACKET

		public bool SQLUPDATEPACKET(DtoPacket packetdto)
		{
			try
				{

				//_sqlCommSelectInsert = _sqlComm.CreateCommand();
				_sqlCommSelectInsert.CommandText = "INSERT INTO  Packet ";
				_sqlCommSelectInsert.Prepare();
				_sqlCommSelectInsert.ExecuteNonQuery();

				return true;
				}
			catch (OdbcException e)
				{
				MessageBox.Show(e.Message);
				return false;
				}
			}

		#endregion

		#region SQLDELETEPACKET

		public bool SQLDELETEPACKET(string Query)
			{
			try
				{
				OdbcConnection sqlConn = new OdbcConnection("DSN=Packet");
				OdbcCommand sqlComm = new OdbcCommand();
				sqlComm = sqlConn.CreateCommand();
				sqlComm.CommandText = Query;
				sqlConn.Open();
				sqlComm.ExecuteNonQuery();
				sqlConn.Close();
				return true;
				}
			catch (OdbcException e)
				{
				MessageBox.Show(e.Message);
				return false;
				}
			}

		#endregion

		}
	#endregion CLASS

	}
