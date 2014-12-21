#region Using Directive

using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Windows.Forms;
using PacketComs;

#endregion

namespace Packet
{

	#region Class

	internal class Sql
	{
		private static readonly FileSql FileSql = new FileSql();
		private OdbcCommand _sqlComm;
		private readonly OdbcCommand _sqlCommSelectUpdate;
		private readonly DtoListMsgFrom _msgfrom = new DtoListMsgFrom();
		private readonly DtoListMsgRoute _msgroute = new DtoListMsgRoute();
		private readonly DtoListMsgSubject _msgsubject = new DtoListMsgSubject();
		private readonly DtoListMsgto _msgtodto = new DtoListMsgto();
		private readonly DtoPacket _packet = new DtoPacket();
        OdbcConnection sqlConn = new OdbcConnection(Form1.dsnName);
		#region constructor

		public Sql()
		{
			
			//sqlConn.Open();
			_sqlCommSelectUpdate = sqlConn.CreateCommand();
		}

		#endregion

		#region SQLSELECT

		public List<DtoPacket> Sqlselect()
		{
			var packets = new List<DtoPacket>();

			try
			{
				var sqlConn = new OdbcConnection(Form1.dsnName);
				sqlConn.Open();
				_sqlComm = sqlConn.CreateCommand();

				using (var cmd = new OdbcCommand())
				{
					cmd.Connection = sqlConn;
					_sqlComm.CommandText = "SELECT * FROM Packet ";
					_sqlComm.ExecuteNonQuery();
					using (var reader = _sqlComm.ExecuteReader())
					{
						while (reader.Read())
						{
							var packet = new DtoPacket((int) reader.GetValue(0),
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
                sqlConn.Close();
			}
			catch (OdbcException e)
			{
				MessageBox.Show(e.Message);
			}
			return packets;
		}

		#endregion

		#region SQLSELECT_ON_Lists_Msgto

		public List<DtoListMsgto> SQLSELECT_ON_Lists_Msgto()
		{
			var selectLists = new List<DtoListMsgto>();
			try
			{
				var sqlConn = new OdbcConnection(Form1.dsnName);
				sqlConn.Open();
				_sqlComm = sqlConn.CreateCommand();

				using (var cmd = new OdbcCommand())
				{
					cmd.Connection = sqlConn;
					_sqlComm.CommandText = "SELECT MSGTO,Selected FROM MSGTO";
					_sqlComm.ExecuteNonQuery();
					using (var reader = _sqlComm.ExecuteReader())
					{
						while (reader.Read())
						{
							var selectList = new DtoListMsgto(
								(String) convertDBNull(reader.GetValue(0)),
								(String) convertDBNull(reader.GetValue(1)),
								DateTime.MinValue);
							selectLists.Add(selectList);
						}
					}
				}
                sqlConn.Close();
			}
			catch (OdbcException e)
			{
				MessageBox.Show(e.Message);
			}
			return selectLists;
		}

		#endregion

		#region SQLSELECT_ON_Lists_MsgFrom

		public List<DtoListMsgFrom> SQLSELECT_ON_Lists_MsgFrom()
		{
			var selectLists = new List<DtoListMsgFrom>();
			try
			{
				var sqlConn = new OdbcConnection(Form1.dsnName);
				sqlConn.Open();
				_sqlComm = sqlConn.CreateCommand();

				using (var cmd = new OdbcCommand())
				{
					cmd.Connection = sqlConn;
					_sqlComm.CommandText = "SELECT MSGFROM,Selected FROM MSGFROM";
					_sqlComm.ExecuteNonQuery();
					using (var reader = _sqlComm.ExecuteReader())
					{
						while (reader.Read())
						{
							var selectList = new DtoListMsgFrom(
								(String) convertDBNull(reader.GetValue(0)),
								(String) convertDBNull(reader.GetValue(1)),
								DateTime.MinValue);
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

		public List<DtoListMsgRoute> SQLSELECT_ON_Lists_MsgRoute()
		{
			var selectLists = new List<DtoListMsgRoute>();
			try
			{
				var sqlConn = new OdbcConnection(Form1.dsnName);
				sqlConn.Open();
				_sqlComm = sqlConn.CreateCommand();

				using (var cmd = new OdbcCommand())
				{
					cmd.Connection = sqlConn;
					_sqlComm.CommandText = "SELECT MSGROUTE,Selected FROM MSGROUTE";
					_sqlComm.ExecuteNonQuery();
					using (var reader = _sqlComm.ExecuteReader())
					{
						while (reader.Read())
						{
							var selectList = new DtoListMsgRoute(
								(String) convertDBNull(reader.GetValue(0)),
								(String) convertDBNull(reader.GetValue(1)),
								DateTime.MinValue);
							selectLists.Add(selectList);
						}
					}
				}
                sqlConn.Close();
			}
			catch (OdbcException e)
			{
				MessageBox.Show(e.Message);
			}
			return selectLists;
		}

		#endregion

		#region SQLSELECT_ON_Lists_MsgSubject

		public List<DtoListMsgSubject> SQLSELECT_ON_Lists_MsgSubject()
		{
			var selectLists = new List<DtoListMsgSubject>();
			try
			{
				var sqlConn = new OdbcConnection(Form1.dsnName);
				sqlConn.Open();
				_sqlComm = sqlConn.CreateCommand();

				using (var cmd = new OdbcCommand())
				{
					cmd.Connection = sqlConn;
					_sqlComm.CommandText = "SELECT MSGSUBJECT,Selected FROM MSGSUBJECT";
					_sqlComm.ExecuteNonQuery();
					using (var reader = _sqlComm.ExecuteReader())
					{
						while (reader.Read())
						{
							var selectList = new DtoListMsgSubject(
								(String) convertDBNull(reader.GetValue(0)),
								(String) convertDBNull(reader.GetValue(1)),
								DateTime.MinValue);
							selectLists.Add(selectList);
						}
					}
				}
                sqlConn.Close();
			}
			catch (OdbcException e)
			{
				MessageBox.Show(e.Message);
			}
			return selectLists;
		}

		#endregion

		#region SQLSELECTOPTION

		public void SqlselectOptrion( string tableName)
		{
			try
			{
				var sqlConn = new OdbcConnection(Form1.dsnName);
				sqlConn.Open();
				_sqlComm = sqlConn.CreateCommand();
				using (var cmd = new OdbcCommand())
				{
					cmd.Connection = sqlConn;
					_sqlComm.CommandText = "Insert into " + tableName + " ( " + tableName + " ,DateCreate ) " +
					                       " Select DISTINCT " + tableName + " , now() from Packet Where " + tableName +
					                       " not in (Select " + tableName + " from " + tableName + " )";
					_sqlComm.ExecuteNonQuery();
				}
                sqlConn.Close();
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

		public void WriteSqlPacketUpdate(int value, String textValue)
		{
			try
			{
				_packet.set_MSG(value);
				_packet.set_MSGState(textValue);
				Sqlupdatepacket(_packet);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		#endregion

		#region SQLUPDATEPACKET

		public void Sqlupdatepacket(DtoPacket packetdto)
		{
			_sqlCommSelectUpdate.CommandText = "UPDATE Packet SET  MSGState = ? Where MSG = ?   ";
			_sqlCommSelectUpdate.Parameters.Clear();
			_sqlCommSelectUpdate.Parameters.AddWithValue("@p1", packetdto.get_MSGState());
			_sqlCommSelectUpdate.Parameters.AddWithValue("@p2", packetdto.get_MSG());
			try
			{
				_sqlCommSelectUpdate.Prepare();
				_sqlCommSelectUpdate.ExecuteNonQuery();
			}
			catch (OdbcException e)
			{
				MessageBox.Show(e.Message);
			}
		}

		#endregion

		#region WriteSQLMSGTOUpdate

		public void WriteSqlmsgtoUpdate(string value, String textValue)
		{
			try
			{
				_msgtodto.set_MSGTO(value);
				_msgtodto.set_Selected(textValue);
				SqlupdateMsgUpdate(_msgtodto);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		#endregion

		#region SqlupdateMSGUpdate

		public void SqlupdateMsgUpdate(DtoListMsgto packetdto)
		{
			_sqlCommSelectUpdate.CommandText = "UPDATE MSGTO SET  Selected = ? Where MSGTO = ?   ";
			_sqlCommSelectUpdate.Parameters.Clear();
			_sqlCommSelectUpdate.Parameters.AddWithValue("@p1", packetdto.get_Selected());
			_sqlCommSelectUpdate.Parameters.AddWithValue("@p2", packetdto.get_MSGTO());
			try
			{
				_sqlCommSelectUpdate.Prepare();
				_sqlCommSelectUpdate.ExecuteNonQuery();
			}
			catch (OdbcException e)
			{
				MessageBox.Show(e.Message);
			}
		}

		#endregion

		#region WriteSQLMSGFromUpdate

		public void WriteSqlmsgFromUpdate(string value, String textValue)
		{
			try
			{
				_msgfrom.set_MSGFROM(value);
				_msgfrom.set_Selected(textValue);
				SqlupdateMsgfromUpdate(_msgfrom);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		#endregion

		#region SqlupdateMSGFROMUpdate

		public void SqlupdateMsgfromUpdate(DtoListMsgFrom packetdto)
		{
			_sqlCommSelectUpdate.CommandText = "UPDATE MSGFrom SET  Selected = ? Where MSGFrom = ?   ";
			_sqlCommSelectUpdate.Parameters.Clear();
			_sqlCommSelectUpdate.Parameters.AddWithValue("@p1", packetdto.get_Selected());
			_sqlCommSelectUpdate.Parameters.AddWithValue("@p2", packetdto.get_MSGFROM());
			try
            {
               sqlConn.Open();
				_sqlCommSelectUpdate.Prepare();
				_sqlCommSelectUpdate.ExecuteNonQuery();
                sqlConn.Close();
			}
			catch (OdbcException e)
			{
				MessageBox.Show(e.Message);
			}
		}

		#endregion

		#region WriteSQLMSGRouteUpdate

		public void WriteSqlmsgRouteUpdate(string value, String textValue)
		{
			try
			{
				_msgroute.set_MSGRoute(value);
				_msgroute.set_Selected(textValue);
				SqlupdateMsgrouteUpdate(_msgroute);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		#endregion

		#region SqlupdateMSGROUTEUpdate

		public void SqlupdateMsgrouteUpdate(DtoListMsgRoute packetdto)
		{
			_sqlCommSelectUpdate.CommandText = "UPDATE MSGRoute SET  Selected = ? Where MSGRoute = ?   ";
			_sqlCommSelectUpdate.Parameters.Clear();
			_sqlCommSelectUpdate.Parameters.AddWithValue("@p1", packetdto.get_Selected());
			_sqlCommSelectUpdate.Parameters.AddWithValue("@p2", packetdto.get_MSGROUTE());
			try
			{
				_sqlCommSelectUpdate.Prepare();
				_sqlCommSelectUpdate.ExecuteNonQuery();
			}
			catch (OdbcException e)
			{
				MessageBox.Show(e.Message);
			}
		}

		#endregion

		#region WriteSQLMSGSubjectUpdate

		public void WriteSqlmsgSubjectUpdate(string value, String textValue)
		{
			try
			{
				_msgsubject.set_MSGSubject(value);
				_msgsubject.set_Selected(textValue);
				SqlupdateMsgsubjectUpdate(_msgsubject);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		#endregion

		#region SqlupdateMSGFROMUpdate

		public void SqlupdateMsgsubjectUpdate(DtoListMsgSubject packetdto)
		{
			_sqlCommSelectUpdate.CommandText = "UPDATE MSGSubject SET  Selected = ? Where MSGSubject = ?   ";
			_sqlCommSelectUpdate.Parameters.Clear();
			_sqlCommSelectUpdate.Parameters.AddWithValue("@p1", packetdto.get_Selected());
			_sqlCommSelectUpdate.Parameters.AddWithValue("@p2", packetdto.get_MSGSubject());
			try
			{
				_sqlCommSelectUpdate.Prepare();
				_sqlCommSelectUpdate.ExecuteNonQuery();
			}
			catch (OdbcException e)
			{
				MessageBox.Show(e.Message);
			}
		}

		#endregion

	
	}

	#endregion CLASS
}