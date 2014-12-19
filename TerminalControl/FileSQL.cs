﻿#region Using Directive

using System;
using System.Data;
using System.Data.Odbc;
using System.Windows.Forms;

#endregion

namespace PacketComs
	{
	public class FileSql
		{
		private OdbcCommand _sqlCommSelectInsert;
		private OdbcCommand _sqlComm;
		private OdbcConnection _sqlConn;
		private readonly OdbcCommand _sqlCommPacketInsert;
		private readonly OdbcManager odbc;
		private readonly DtoPacket packet = new DtoPacket();
		public const string dsnName = "DSN=Packet";
		private readonly OdbcCommand _sqlCommSelectUpdate;

		#region Constructor

		public FileSql()
			{


			var dsnTableName = "Packet";
			odbc = new OdbcManager();
			_sqlConn = new OdbcConnection(dsnName);
			_sqlCommPacketInsert = new OdbcCommand();
			_sqlCommPacketInsert = _sqlConn.CreateCommand();


			if (odbc.CheckForDSN(dsnTableName) > 0)
				{
				if (DoesTableExist(dsnTableName, dsnName) == false)
				{
				    _sqlConn.Open();
					SqlMakeTable("CREATE TABLE " + dsnTableName + "( MSG int PRIMARY KEY, MSGTSLD CHAR(3), MSGSize int, MSGTO CHAR(6), MSGRoute CHAR(7),MSGFrom CHAR(6), MSGDateTime CHAR(9), MSGSubject CHAR(30), MSGState CHAR(8) )");
					}
				}
			else
				{
				odbc.CreateDSN(dsnName);
				MessageBox.Show("No Packet System DSN " + Environment.NewLine + "Please make one. Must be name Packet",
					"Critical Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Environment.Exit(1);
				}
			_sqlConn.Open(); // must be after DSN check.

			_sqlCommSelectUpdate = _sqlConn.CreateCommand();

			_sqlCommPacketInsert.CommandText = "INSERT INTO  Packet " +
				    							   "(MSG," +
											   "MSGTSLD," +
											   "MSGSize," +
											   "MSGTO," +
											   "MSGRoute," +
											   "MSGFrom," +
											   "MSGDateTime," +
											   "MSGSubject) " +
											   "VALUES (?,?,?,?,?,?,?,?)";
			}

		#endregion

		#region SQLMakeTable

		private void SqlMakeTable(string query)
			{
			try
				{
				_sqlCommPacketInsert.CommandText = query;
                _sqlCommPacketInsert.ExecuteNonQuery();
                //_sqlConn.Close();
				}
			catch (OdbcException e)
				{
				MessageBox.Show(e.Message);
				}
			}

		#endregion

		#region SQLInsertPacket

		private void SqlInsertPacket(DtoPacket packetdto)
			{
			_sqlCommPacketInsert.Parameters.Clear();
			_sqlCommPacketInsert.Parameters.AddWithValue("@p1", packetdto.get_MSG());
			_sqlCommPacketInsert.Parameters.AddWithValue("@p2", packetdto.get_MSGTSLD());
			_sqlCommPacketInsert.Parameters.AddWithValue("@p3", packetdto.get_MSGSize());
			_sqlCommPacketInsert.Parameters.AddWithValue("@p4", packetdto.get_MSGTO());
			_sqlCommPacketInsert.Parameters.AddWithValue("@p5", packetdto.get_MSGRoute());
			_sqlCommPacketInsert.Parameters.AddWithValue("@p6", packetdto.get_MSGFrom());
			_sqlCommPacketInsert.Parameters.AddWithValue("@p7", packetdto.get_MSGDateTime());
			_sqlCommPacketInsert.Parameters.AddWithValue("@p8", packetdto.get_MSGSubject());
			try
				{
				_sqlCommPacketInsert.Prepare();
				_sqlCommPacketInsert.ExecuteNonQuery();
				//sqlConn.Close();
				}
			catch (OdbcException e)
				{
				MessageBox.Show(e.Message);
				}
			}

		#endregion

		#region SQLInsertSelect

		private void SqlInsertSelect(DtoPacket packetdto, string tableName)
			{
			_sqlCommSelectInsert.Parameters.Clear();
			if (tableName == "SelectedMSG")
				_sqlCommSelectInsert.Parameters.AddWithValue("@p1", packetdto.get_MSG());
			else if (tableName == "SelectedTo")
				_sqlCommSelectInsert.Parameters.AddWithValue("@p1", packetdto.get_MSGTO());
			else if (tableName == "SelectedRoute")
				_sqlCommSelectInsert.Parameters.AddWithValue("@p1", packetdto.get_MSGRoute());
			else if (tableName == "SelectedFrom")
				_sqlCommSelectInsert.Parameters.AddWithValue("@p1", packetdto.get_MSGFrom());
			else if (tableName == "SelectedSubject")
				_sqlCommSelectInsert.Parameters.AddWithValue("@p1", packetdto.get_MSGSubject());
			try
				{
				_sqlCommSelectInsert.Prepare();
				_sqlCommSelectInsert.ExecuteNonQuery();
				//sqlConn.Close();
				}
			catch (OdbcException e)
				{
				MessageBox.Show(e.Message);
				}
			}

		#endregion

		#region DoesTableExist

		public bool DoesTableExist(string tableName, string dsnName)
			{
			using (var sqlConn = new OdbcConnection(dsnName))
				{
				var tableExists = false;
					{
					try
						{
						sqlConn.Open();
						var dt = sqlConn.GetSchema("TABLES");
						foreach (DataRow row in dt.Rows)
							{
							if (row[2].ToString().ToLower() == tableName.ToLower())
								{
								tableExists = true;
								break;
								}
							}
						}
					catch (Exception e)
						{
						MessageBox.Show(e.Message);
						return false;
						}
					}
					return tableExists;
				}
			}

		#endregion

		#region WriteSQLPacket

		public void WriteSqlPacket(string textValue)
			{
			try
				{
				packet.set_MSG(Convert.ToInt32(Mid(textValue, 0, 5)));
				packet.set_MSGTSLD(Mid(textValue, 7, 4));
				packet.set_MSGSize(Convert.ToInt32(Mid(textValue, 13, 5)));
				packet.set_MSGTO(Mid(textValue, 18, 6));
				packet.set_MSGRoute(Mid(textValue, 24, 8));
				packet.set_MSGFrom(Mid(textValue, 32, 7));
				packet.set_MSGDateTime(Mid(textValue, 39, 9));
				packet.set_MSGSubject(Mid(textValue, 49, (textValue.Length - 49)));
				SqlInsertPacket(packet);
				}
			catch (Exception e)
				{
				MessageBox.Show(e.Message);
				}
			}

		#endregion

		#region	   WriteSQLSelect

		public void WriteSqlSelect(string textValue, string tableName)
			{
			try
				{
				_sqlCommSelectInsert = _sqlConn.CreateCommand();
				_sqlCommSelectInsert.CommandText = "INSERT INTO " + tableName + " (" + tableName + ") VALUES (?)";
				if (tableName == "SelectedMSG")
					packet.set_MSG(Convert.ToInt32(textValue));
				else if (tableName == "SelectedTo")
					packet.set_MSGTO(textValue);
				else if (tableName == "SelectedRoute")
					packet.set_MSGRoute(textValue);
				else if (tableName == "SelectedFrom")
					packet.set_MSGFrom(textValue);
				else if (tableName == "SelectedSubject")
					packet.set_MSGSubject(textValue);
				SqlInsertSelect(packet, tableName);
				}
			catch (Exception e)
				{
				MessageBox.Show(e.Message);
				}
			}

		#endregion

		#region SelectMakeTable

		public void SelectMakeTable(string textVale, Int32 intsize, string tableName, string dsnName, string systemDsn)
			{
			if (odbc.CheckForDSN(systemDsn) > 0)
				{
				if (DoesTableExist(tableName, dsnName) == false)
					{
					SqlMakeTable("CREATE TABLE " + tableName + " (  " + textVale + " CHAR(" + intsize +
								 "), DateCreate datetime, Selected CHAR(1)  )");
					_sqlConn = new OdbcConnection(dsnName);
					_sqlConn.Open();
					}
				}
			else
				{
				odbc.CreateDSN(dsnName);
				MessageBox.Show("No Packet System DSN " + Environment.NewLine + "Please make one. Must be name Packet",
					"Critical Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Environment.Exit(1);
				}
			}

		#endregion

		#region Mid

		private static string Mid(string param, int startIndex, int length)
			{
			if (param == " ")
				{
				return null;
				}
			var result = param.Substring(startIndex, length);
			return result;
			}

		#endregion

		#region SQLPacketSubjectDelete

		public void SqlPacketSubjectDelete()
			{
			if (DoesTableExist("MSGSubject", dsnName))
				{
					{
					_sqlCommSelectUpdate.CommandText = "Delete From Packet " +
													   "Where MSGSubject in " +
													   "(Select MSGSubject from MSGSubject where Selected  = \"D\")   ";
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
			}

		#endregion

		#region SQLPacketFromDelete

		public void SqlPacketFromDelete()
			{
			if (DoesTableExist("MSGFrom", dsnName))
				{
					{
					_sqlCommSelectUpdate.CommandText = "Delete From Packet " +
													   "Where MSGFrom in " +
													   "(Select MSGFrom from MSGFrom where Selected  = \"D\")   ";
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
			}

		#endregion

		#region SQLPacketRouteDelete

		public void SqlPacketRouteDelete()
			{
			if (DoesTableExist("MSGRoute", dsnName))
				{
					{
					_sqlCommSelectUpdate.CommandText = "Delete From Packet " +
													   "Where MSGRoute in " +
													   "(Select MSGRoute from MSGRoute where Selected  = \"D\")   ";
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
			}

		#endregion

		#region SQLPackeToDelete

		public void SqlPacketToDelete()
			{
			if (DoesTableExist("MSGTO", dsnName))
				{
					{
					_sqlCommSelectUpdate.CommandText = "Delete From Packet " +
													   "Where MSGTO in " +
													   "(Select = MSGTO from MSGTO where Selected  = \"D\")   ";
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
			}

		#endregion

		#region SQLPacket Delete

		public void SqlPacketDelete()
			{
			SqlPacketSubjectDelete();
			SqlPacketFromDelete();
			SqlPacketRouteDelete();
			SqlPacketToDelete();
			}

		#endregion

		#region SQLSelectMail

		public string SqlSelectMail()
			{
			string selectLists = "";
			try
				{
				var sqlConn = new OdbcConnection(dsnName);
				//sqlConn.Open();
				_sqlComm = sqlConn.CreateCommand();

				using (var cmd = new OdbcCommand())
					{
					cmd.Connection = sqlConn;
					_sqlComm.CommandText = "SELECT MSG FROM Packet where MSGState = \"R\" ";
					_sqlComm.ExecuteNonQuery();
					using (var reader = _sqlComm.ExecuteReader())
						{
						while (reader.Read())
							{
							selectLists = reader.GetString(0);

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



		#region SqlupdateMSGUpdate

		public void SqlupdateMSGUpdate()
			{
			SqlupdateMSGUpdateTO();
			SqlupdateMSGUpdateFROM();
			SqlupdateMSGUpdateRoute();
			SqlupdateMSGUpdateSubject();
			}

		#endregion

		#region SqlupdateMSGUpdateTO

		public void SqlupdateMSGUpdateTO()
			{
			if (DoesTableExist("MSGTO", dsnName))
				{
				_sqlCommSelectUpdate.CommandText = "UPDATE Packet SET  MSGState = \"R\" Where MSGTO in (Select = MSGTO from MSGTO where Selected  = \"Y\")  ";
				try
					{
					_sqlCommSelectUpdate.ExecuteNonQuery();
					}
				catch (OdbcException e)
					{
					MessageBox.Show(e.Message);
					}
				}
			}


		#endregion

		#region SqlupdateMSGUpdateFROM

		public void SqlupdateMSGUpdateFROM()
			{
			if (DoesTableExist("MSGFrom", dsnName))
				{
				_sqlCommSelectUpdate.CommandText = "UPDATE Packet SET  MSGState = \"R\" Where MSGFrom in (Select = MSGFROM from MSGFROM where Selected  = \"Y\")  ";
				try
					{
					_sqlCommSelectUpdate.ExecuteNonQuery();
					}
				catch (OdbcException e)
					{
					MessageBox.Show(e.Message);
					}
				}
			}

		#endregion

		#region SqlupdateMSGUpdateRoure

		public void SqlupdateMSGUpdateRoute()
			{
			if (DoesTableExist("MSGRoute", dsnName))
				{
				_sqlCommSelectUpdate.CommandText =
					"UPDATE Packet SET  MSGState = \"R\" Where MSGROUTE in (Select = MSGROUTE from MSGROUTE where Selected  = \"Y\")  ";
				try
					{
					_sqlCommSelectUpdate.ExecuteNonQuery();
					}
				catch (OdbcException e)
					{
					MessageBox.Show(e.Message);
					}
				}
			}

		#endregion

		#region SqlupdateMSGUpdateSubject

		public void SqlupdateMSGUpdateSubject()
			{
			if (DoesTableExist("MSGSubject", dsnName))
				{
				_sqlCommSelectUpdate.CommandText =
					"UPDATE Packet SET  MSGState = \"R\" Where MSGSubject in (Select = MSGSubject from MSGSubject where Selected  = \"Y\")  ";
				try
					{
					_sqlCommSelectUpdate.ExecuteNonQuery();
					}
				catch (OdbcException e)
					{
					MessageBox.Show(e.Message);
					}
				}
			}

		#endregion

		}
	} //end name-space