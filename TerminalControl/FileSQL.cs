#region Using Directive

using System;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Windows.Forms;

#endregion

namespace PacketComs
	{

	public class FileSql
		{

		DtoPacket packet = new DtoPacket();
		private OdbcCommand sqlComm;
        
	
		#region Constructor
		public FileSql()
			{
			string dsnName = "DSN=Packet";
			string dsnTableName = "Packet";
			var sqlConn = new OdbcConnection(dsnName);
			sqlConn.Open();
			sqlComm = sqlConn.CreateCommand();
			sqlComm.CommandText = "INSERT INTO  Packet " +
				"(MSG," +
				"MSGTSLD," +
				"MSGSize," +
				"MSGTO," +
				"MSGRoute," +
				"MSGFrom," +
				"MSGDateTime," +
				"MSGSubject) " +
				"VALUES (?,?,?,?,?,?,?,?)";

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
			}
		#endregion

		#region SQLSELECT	
        public void Sqlselect(string dsnName)
        {
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
                            {
                                packet.set_MSG(Convert.ToInt32(reader.GetValue(0)));
                                packet.set_MSGTSLD(reader.GetValue(1).ToString());
                                packet.set_MSGSize(Convert.ToInt32(reader.GetValue(2)));
                                packet.set_MSGTO(reader.GetValue(3).ToString());
                                packet.set_MSGRoute(reader.GetValue(4).ToString());
                                packet.set_MSGFrom(reader.GetValue(5).ToString());
                                packet.set_MSGDateTime(reader.GetValue(6).ToString());
                                packet.set_MSGSubject(reader.GetValue(7).ToString());
                                packet.set_MSGState(reader.GetValue(8).ToString());
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

		#region SQLMakeTable
		private void SqlMakeTable(string query)
			{
			try
				{
				sqlComm.CommandText = query;
				sqlComm.ExecuteNonQuery();

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
			sqlComm.Parameters.Clear();
			sqlComm.Parameters.AddWithValue("@p1", packetdto.get_MSG());
			sqlComm.Parameters.AddWithValue("@p2", packetdto.get_MSGTSLD());
			sqlComm.Parameters.AddWithValue("@p3", packetdto.get_MSGSize());
			sqlComm.Parameters.AddWithValue("@p4", packetdto.get_MSGTO());
			sqlComm.Parameters.AddWithValue("@p5", packetdto.get_MSGRoute());
			sqlComm.Parameters.AddWithValue("@p6", packetdto.get_MSGFrom());
			sqlComm.Parameters.AddWithValue("@p7", packetdto.get_MSGDateTime());
			sqlComm.Parameters.AddWithValue("@p8", packetdto.get_MSGSubject());
			try
				{
				    sqlComm.Prepare();
				    sqlComm.ExecuteNonQuery();
				//sqlConn.Close();
				}
			catch (OdbcException e)
				{
					MessageBox.Show(e.Message);
				}

		}
		#endregion

		#region SQLUPDATE
		/*
		public bool SQLUPDATE(string Query)
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

		#region SQLDELETE

		public bool SQLDELETE(string Query)
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
		 */
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



		#region WriteST
		public bool WriteSt(string textVale, string fileName)
			{

			try
				{
				string path = Directory.GetCurrentDirectory() + @"\Data";
				if (!Directory.Exists(path))
					{
					// Try to create the directory.
					Directory.CreateDirectory(path);
					}
				string filePath = path + @"\" + fileName + ".txt";

				File.WriteAllText(filePath, textVale);
				return true;
				} //end try
			catch (Exception e)
				{
				MessageBox.Show(e.Message);
				return false;
				}
			} //end write
		#endregion

		#region checkST
		public bool? CheckSt(string fileName)
			{

			try
				{
				string path = Directory.GetCurrentDirectory() + @"\Data";
				if (!Directory.Exists(path))
					{
					// Try to create the directory.
					Directory.CreateDirectory(path);
					}
				path = path + @"\" + fileName + ".txt";
				if (File.Exists(path))
					{
					return true;
					}
				return null;
				} //end try
			catch (Exception e)
				{
				MessageBox.Show(e.Message);
				return false;
				}
			} //end check
		#endregion

		#region RXST
		public string Rxst(string fileName)
			{
			string myString = null;
			string path = Directory.GetCurrentDirectory() + @"\Data";
			if (!Directory.Exists(path))
				{
				// Try to create the directory.
				Directory.CreateDirectory(path);
				}
			path = path + @"\" + fileName + ".txt";
			if (File.Exists(path))
				{
				StreamReader myFile = new StreamReader(path);
				myString = myFile.ReadToEnd();
				myFile.Close();
				}
			return myString;

			}
		#endregion

	

		#region Mid
		public static string Mid(string param, int startIndex, int length)
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