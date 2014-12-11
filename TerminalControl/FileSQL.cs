#region Using Directive

using System;
using System.Data;
using System.Data.Odbc;
using System.Windows.Forms;

#endregion

namespace PacketComs
	{

	public class FileSql
		{
		DtoPacket packet = new DtoPacket();
		private OdbcCommand sqlCommPacketInsert;
		private OdbcCommand _sqlCommSelectInsert;
		private OdbcConnection _sqlConn; 

		#region Constructor
		public FileSql()
			{
			string dsnName = "DSN=Packet";
			string dsnTableName = "Packet";
			OdbcManager odbc = new OdbcManager();
			if (odbc.CheckForDSN(dsnTableName) > 0)
				{
				if (DoesTableExist(dsnTableName, dsnName) == false)
					{
					SqlMakeTable(
						"CREATE TABLE " + dsnTableName + "( MSG int PRIMARY KEY, MSGTSLD CHAR(3), MSGSize int, MSGTO CHAR(6), MSGRoute CHAR(7),MSGFrom CHAR(6), MSGDateTime CHAR(9), MSGSubject CHAR(30), MSGState CHAR(8) )");
					}
				}
			else
				{
				odbc.CreateDSN(dsnName);
				MessageBox.Show("No Packet System DSN " + Environment.NewLine + "Please make one. Must be name Packet", "Critical Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Environment.Exit(1);
				}

			_sqlConn = new OdbcConnection(dsnName);
			_sqlConn.Open();
			sqlCommPacketInsert = _sqlConn.CreateCommand();
			sqlCommPacketInsert.CommandText = "INSERT INTO  Packet " +
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
				sqlCommPacketInsert.CommandText = query;
				sqlCommPacketInsert.ExecuteNonQuery();

				}
			catch (OdbcException e)
				{
				MessageBox.Show(e.Message);
				}
			}

		#endregion

		#region SQLInsertPacket
		private void  SqlInsertPacket(DtoPacket packetdto)
		{
		sqlCommPacketInsert.Parameters.Clear();
		sqlCommPacketInsert.Parameters.AddWithValue("@p1", packetdto.get_MSG());
		sqlCommPacketInsert.Parameters.AddWithValue("@p2", packetdto.get_MSGTSLD());
		sqlCommPacketInsert.Parameters.AddWithValue("@p3", packetdto.get_MSGSize());
		sqlCommPacketInsert.Parameters.AddWithValue("@p4", packetdto.get_MSGTO());
		sqlCommPacketInsert.Parameters.AddWithValue("@p5", packetdto.get_MSGRoute());
		sqlCommPacketInsert.Parameters.AddWithValue("@p6", packetdto.get_MSGFrom());
		sqlCommPacketInsert.Parameters.AddWithValue("@p7", packetdto.get_MSGDateTime());
		sqlCommPacketInsert.Parameters.AddWithValue("@p8", packetdto.get_MSGSubject());
			try
				{
				sqlCommPacketInsert.Prepare();
				sqlCommPacketInsert.ExecuteNonQuery();
				//sqlConn.Close();
				}
			catch (OdbcException e)
				{
					MessageBox.Show(e.Message);
				}

		}
		#endregion

		#region SQLInsertSelect
		private void SqlInsertSelect(DtoPacket packetdto,string tableName)
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

		#region SQLUPDATEPACKET
		
		public bool SQLUPDATEPACKET(string Query)
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

		#region DoesTableExist

		private bool DoesTableExist(string tableName, string dsnName)
		{
		using (OdbcConnection sqlConn = new OdbcConnection(dsnName))
			{
				bool tableExists = false;
				{
					try
					{
						sqlConn.Open();
						DataTable dt = sqlConn.GetSchema("TABLES");
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
			else if(tableName == "SelectedTo")
				packet.set_MSGTO(textValue);
			else if (tableName == "SelectedRoute")
				packet.set_MSGRoute(textValue);
			else if (tableName == "SelectedFrom")
				packet.set_MSGFrom(textValue);
			else if (tableName == "SelectedSubject")
			packet.set_MSGSubject(textValue);
			SqlInsertSelect(packet,tableName);
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
             OdbcManager odbc = new OdbcManager();
			 if (odbc.CheckForDSN(systemDsn) > 0)
		    {
		        if (DoesTableExist(tableName,  dsnName) == false)
		        {
			        SqlMakeTable("CREATE TABLE " + tableName + " (  " + textVale + " CHAR(" + intsize + "), DateCreate datetime, Selected CHAR(1)  )");
					_sqlConn = new OdbcConnection(dsnName);
					_sqlConn.Open();	
		        }
		    }
		    else
		    {
		        odbc.CreateDSN(dsnName);
		        MessageBox.Show("No Packet System DSN " + Environment.NewLine + "Please make one. Must be name Packet", "Critical Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
		}
	} //end name-space