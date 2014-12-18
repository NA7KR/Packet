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

	internal class Sql
	{
		private static readonly FileSql FileSql = new FileSql();
		private OdbcCommand _sqlComm;
		private readonly OdbcCommand _sqlCommSelectUpdate;
		private readonly DtoListMsgFrom msgfrom = new DtoListMsgFrom();
		private readonly DtoListMsgRoute msgroute = new DtoListMsgRoute();
		private readonly DtoListMsgSubject msgsubject = new DtoListMsgSubject();
		private readonly DtoListMsgto msgtodto = new DtoListMsgto();
		private readonly DtoPacket packet = new DtoPacket();

		#region constructor

		public Sql()
		{
			var sqlConn = new OdbcConnection("DSN=Packet");
			sqlConn.Open();
			_sqlCommSelectUpdate = sqlConn.CreateCommand();
		}

		#endregion

		#region SQLSELECT

		public List<DtoPacket> Sqlselect(string dsnName)
		{
			var packets = new List<DtoPacket>();

			try
			{
				var sqlConn = new OdbcConnection(dsnName);
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
			var selectLists = new List<DtoListMsgto>();
			try
			{
				var sqlConn = new OdbcConnection(dsnName);
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
			var selectLists = new List<DtoListMsgFrom>();
			try
			{
				var sqlConn = new OdbcConnection(dsnName);
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

		public List<DtoListMsgRoute> SQLSELECT_ON_Lists_MsgRoute(string dsnName)
		{
			var selectLists = new List<DtoListMsgRoute>();
			try
			{
				var sqlConn = new OdbcConnection(dsnName);
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
			var selectLists = new List<DtoListMsgSubject>();
			try
			{
				var sqlConn = new OdbcConnection(dsnName);
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
				using (var cmd = new OdbcCommand())
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
				Sqlupdatepacket(packet);
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

		public void WriteSQLMSGTOUpdate(string Value, String textValue)
		{
			try
			{
				msgtodto.set_MSGTO(Value);
				msgtodto.set_Selected(textValue);
				SqlupdateMSGUpdate(msgtodto);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		#endregion

		#region SqlupdateMSGUpdate

		public void SqlupdateMSGUpdate(DtoListMsgto packetdto)
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

		public void WriteSQLMSGFromUpdate(string Value, String textValue)
		{
			try
			{
				msgfrom.set_MSGFROM(Value);
				msgfrom.set_Selected(textValue);
				SqlupdateMSGFROMUpdate(msgfrom);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		#endregion

		#region SqlupdateMSGFROMUpdate

		public void SqlupdateMSGFROMUpdate(DtoListMsgFrom packetdto)
		{
			_sqlCommSelectUpdate.CommandText = "UPDATE MSGFrom SET  Selected = ? Where MSGFrom = ?   ";
			_sqlCommSelectUpdate.Parameters.Clear();
			_sqlCommSelectUpdate.Parameters.AddWithValue("@p1", packetdto.get_Selected());
			_sqlCommSelectUpdate.Parameters.AddWithValue("@p2", packetdto.get_MSGFROM());
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

		#region WriteSQLMSGRouteUpdate

		public void WriteSQLMSGRouteUpdate(string Value, String textValue)
		{
			try
			{
				msgroute.set_MSGRoute(Value);
				msgroute.set_Selected(textValue);
				SqlupdateMSGROUTEUpdate(msgroute);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		#endregion

		#region SqlupdateMSGROUTEUpdate

		public void SqlupdateMSGROUTEUpdate(DtoListMsgRoute packetdto)
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

		public void WriteSQLMSGSubjectUpdate(string Value, String textValue)
		{
			try
			{
				msgsubject.set_MSGSubject(Value);
				msgsubject.set_Selected(textValue);
				SqlupdateMSGSUBJECTUpdate(msgsubject);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		#endregion

		#region SqlupdateMSGFROMUpdate

		public void SqlupdateMSGSUBJECTUpdate(DtoListMsgSubject packetdto)
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

		#region SQLPacketSubjectDelete

		public void SQLPacketSubjectDelete()
		{
			if (FileSql.DoesTableExist("MSGSubject", "DSN=Packet"))
			{
				_sqlCommSelectUpdate.CommandText = "Delete From Packet " +
				                                   "Where MSGSubject in " +
				                                   "(Select = MSGSubject from MSGSubject where Selected  = D)   ";
			}
			try
			{
				_sqlCommSelectUpdate.ExecuteNonQuery();
			}
			catch (OdbcException e)
			{
				MessageBox.Show(e.Message);
			}
		}

		#endregion

		#region SQLPacketFromDelete

		public void SQLPacketFromDelete()
		{
			if (FileSql.DoesTableExist("MSGFrom", "DSN=Packet"))
			{
				_sqlCommSelectUpdate.CommandText = "Delete From Packet " +
				                                   "Where MSGFrom in " +
				                                   "(Select = MSGFrom from MSGFrom where Selected  = D)   ";
			}
			try
			{
				_sqlCommSelectUpdate.ExecuteNonQuery();
			}
			catch (OdbcException e)
			{
				MessageBox.Show(e.Message);
			}
		}

		#endregion

		#region SQLPacketRouteDelete

		public void SQLPacketRouteDelete()
		{
			if (FileSql.DoesTableExist("MSGRoute", "DSN=Packet"))
			{
				_sqlCommSelectUpdate.CommandText = "Delete From Packet " +
				                                   "Where MSGRoute in " +
				                                   "(Select = MSGRoute from MSGRoute where Selected  = D)   ";
			}
			try
			{
				_sqlCommSelectUpdate.ExecuteNonQuery();
			}
			catch (OdbcException e)
			{
				MessageBox.Show(e.Message);
			}
		}

		#endregion

		#region SQLPackeToDelete

		public void SQLPacketToDelete()
		{
			if (FileSql.DoesTableExist("MSGTO", "DSN=Packet"))
			{
				_sqlCommSelectUpdate.CommandText = "Delete From Packet " +
				                                   "Where MSGTO in " +
				                                   "(Select = MSGTO from MSGTO where Selected  = D)   ";
			}
			try
			{
				_sqlCommSelectUpdate.ExecuteNonQuery();
			}
			catch (OdbcException e)
			{
				MessageBox.Show(e.Message);
			}
		}

		#endregion

		#region SQLPacket Delete

		public void SqlPacketDelete()
		{
			SQLPacketSubjectDelete();
			SQLPacketFromDelete();
			SQLPacketRouteDelete();
			SQLPacketToDelete();
		}

		#endregion

		#region SQLSelectMail

		public void SQLSelectMail()
		{
		}

		#endregion
	}

	#endregion CLASS
}