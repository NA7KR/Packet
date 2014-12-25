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
        private DtoPacket packet = new DtoPacket();
        public const string DsnName = "DSN=Packet";
		private OdbcManager odbc;
		private OdbcConnection conp = new OdbcConnection(DsnName);  
		private OdbcCommand cmdp = new OdbcCommand();
         
        #region Constructor
        public FileSql()
        {
            var dsnTableName = "Packet";
            odbc = new OdbcManager();
			cmdp.Connection = conp;
			conp.Open();

	        {
		        if (odbc.CheckForDSN(dsnTableName) > 0)
		        {
			        if (DoesTableExist(dsnTableName, DsnName) == false)
			        {
				        SqlMakeTable("CREATE TABLE " + dsnTableName +
				                     "( MSG int PRIMARY KEY, MSGTSLD CHAR(3), MSGSize int, MSGTO CHAR(6), MSGRoute CHAR(7),MSGFrom CHAR(6), MSGDateTime CHAR(9), MSGSubject CHAR(30), MSGState CHAR(8) )");
			        }
		        }
		        else
		        {
			        odbc.CreateDSN(DsnName);
			        MessageBox.Show("No Packet System DSN " + Environment.NewLine + "Please make one. Must be name Packet",
				        "Critical Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
			        Environment.Exit(1);
		        }
	        }
        }
        #endregion

        #region SQLMakeTable
        private void SqlMakeTable(string query)
        {
            try
            {
                using (var con = new OdbcConnection(DsnName))
                {
                    using (var cmd = new OdbcCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = query;
						con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
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
            try
            {
                        cmdp.CommandText =  "INSERT INTO  Packet (MSG, MSGTSLD, MSGSize, MSGTO, MSGRoute, MSGFrom, MSGDateTime, MSGSubject) VALUES (?,?,?,?,?,?,?,?)";
                        cmdp.Parameters.Clear();
                        cmdp.Parameters.AddWithValue("@p1", packetdto.get_MSG());
                        cmdp.Parameters.AddWithValue("@p2", packetdto.get_MSGTSLD());
                        cmdp.Parameters.AddWithValue("@p3", packetdto.get_MSGSize());
                        cmdp.Parameters.AddWithValue("@p4", packetdto.get_MSGTO());
                        cmdp.Parameters.AddWithValue("@p5", packetdto.get_MSGRoute());
                        cmdp.Parameters.AddWithValue("@p6", packetdto.get_MSGFrom());
                        cmdp.Parameters.AddWithValue("@p7", packetdto.get_MSGDateTime());
                        cmdp.Parameters.AddWithValue("@p8", packetdto.get_MSGSubject());
                        //cmd.Connection = conp;
	                   
		                //con.Open();
	                    
	                    cmdp.Prepare();
                        cmdp.ExecuteNonQuery();
	                   
		                //con.Close();
            }
            catch
                (OdbcException e)
            {
                MessageBox.Show(e.Message);
            }

        }
        #endregion

        #region DoesTableExist
        public bool DoesTableExist(string tableName, string dsnName)
        {
            using (var con = new OdbcConnection(dsnName))
            {
                    var tableExists = false;
                    {
                        try
                        {
                            con.Open();
                            var dt = con.GetSchema("TABLES");
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[2].ToString().ToLower() == tableName.ToLower())
                                {
                                    tableExists = true;
                                    break;
                                }
                            }
                            con.Close();
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

        #region SelectMakeTable
        public void SelectMakeTable(string textVale, Int32 intsize, string tableName, string dsnName, string systemDsn)
        {
            if (odbc.CheckForDSN(systemDsn) > 0)
            {
                if (DoesTableExist(tableName, dsnName) == false)
                {
                    SqlMakeTable("CREATE TABLE " + tableName + " (  " + textVale + " CHAR(" + intsize  + "), DateCreate datetime, Selected CHAR(1)  )");
                }
            }
            else
            {
                odbc.CreateDSN(dsnName);
                MessageBox.Show("No Packet System DSN " + Environment.NewLine + "Please make one. Must be name Packet",  "Critical Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
		try
			{
			if (DoesTableExist("MSGSubject", DsnName))
				{
				using (var con = new OdbcConnection(DsnName))
					{
					using (var cmd = new OdbcCommand())
						{
						cmd.Connection = con;
						cmd.CommandText = "Delete From Packet Where MSGSubject in (Select MSGSubject from MSGSubject where Selected  = ?)   ";
						cmd.Parameters.Clear();
						cmd.Parameters.AddWithValue("@p1", "D");
						con.Open();
						cmd.Prepare();
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

        #region SQLPacketFromDelete
        public void SqlPacketFromDelete()
        {
            if (DoesTableExist("MSGFrom", DsnName))
            {
                try
                {
                    using (var con = new OdbcConnection(DsnName))
                    {
                        using (var cmd = new OdbcCommand())
                        {
                            cmd.Connection = con;
                            cmd.CommandText = "Delete From Packet  Where MSGFrom in  ( Select MSGFrom from MSGFrom where Selected  = ? )   ";
							cmd.Parameters.Clear();
							cmd.Parameters.AddWithValue("@p1", "D");
							con.Open();
							cmd.Prepare();
                        }
                    }
                }
                catch
                (OdbcException e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
        #endregion

        #region SQLPacketRouteDelete
        public void SqlPacketRouteDelete()
        {
            if (DoesTableExist("MSGRoute", DsnName))
            {
                try
                {
                    using (var con = new OdbcConnection(DsnName))
                    {
                        using (var cmd = new OdbcCommand())
                        {
                            cmd.Connection = con;
                            cmd.CommandText =  "Delete From Packet Where MSGRoute in (Select MSGRoute from MSGRoute where Selected  = ?)   ";
							cmd.Parameters.Clear();
							cmd.Parameters.AddWithValue("@p1", "D");
							con.Open();
							cmd.Prepare();

                        }
                    }
                }
                catch
                    (OdbcException e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
        #endregion

        #region SQLPackeToDelete
        public void SqlPacketToDelete()
        {
            if (DoesTableExist("MSGTO", DsnName))
            {
                try
                {
                    using (var con = new OdbcConnection(DsnName))
                    {
                        using (var cmd = new OdbcCommand())
                        {
                            cmd.Connection = con;
                            cmd.CommandText = "Delete From Packet Where MSGTO in (Select = MSGTO from MSGTO where Selected  = ?)   ";
							cmd.Parameters.Clear();
							cmd.Parameters.AddWithValue("@p1", "D");
							con.Open();
							cmd.Prepare();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                catch
                    (OdbcException e)
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
                using (var con = new OdbcConnection(DsnName))
                {
                    using (var cmd = new OdbcCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "SELECT MSG FROM Packet where MSGState = ? ";
                        con.Open();
						cmd.Parameters.Clear();
						cmd.Parameters.AddWithValue("@p1", "R");
						cmd.Prepare();
                        cmd.ExecuteNonQuery();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                selectLists = reader.GetString(0);
                            }
                        }
                        con.Close();
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

        public void SqlupdateMsgUpdate()
        {
            SqlupdateMsgUpdateTo();
            SqlupdateMsgUpdateFrom();
            SqlupdateMsgUpdateRoute();
            SqlupdateMsgUpdateSubject();
        }

        #endregion

        #region SqlupdateMSGUpdateTO

        public void SqlupdateMsgUpdateTo()
        {
            if (DoesTableExist("MSGTO", DsnName))
            {
	            try
	            {
		            using (var con = new OdbcConnection(DsnName))
		            {
			            using (var cmd = new OdbcCommand())
			            {
				            cmd.Connection = con;
				            cmd.CommandText =
					            "UPDATE Packet SET  MSGState = \"R\" Where MSGTO in (Select  MSGTO from MSGTO where Selected  = ?)  ";
							cmd.Parameters.Clear();
							cmd.Parameters.AddWithValue("@p1", "Y");
							con.Open();
							cmd.Prepare();
			            }
		            }
	            }
			   catch (OdbcException e)
				{
					MessageBox.Show(e.Message);
				}
		    
            }
        }


        #endregion

        #region SqlupdateMSGUpdateFROM

        public void SqlupdateMsgUpdateFrom()
        {
            if (DoesTableExist("MSGFrom", DsnName))
            {
			try
				{
				using (var con = new OdbcConnection(DsnName))
					{
					using (var cmd = new OdbcCommand())
						{
						cmd.Connection = con;
						cmd.CommandText = "UPDATE Packet SET  MSGState = \"R\" Where MSGFrom in (Select  MSGFROM from MSGFROM where Selected  = ?)  ";
						cmd.Parameters.Clear();
						cmd.Parameters.AddWithValue("@p1", "Y");
						con.Open();
						cmd.Prepare();
						}
					}
				}
			catch (OdbcException e)
				{
				MessageBox.Show(e.Message);
				}
            }
        }

        #endregion

        #region SqlupdateMSGUpdateRoure

        public void SqlupdateMsgUpdateRoute()
        {
	        if (DoesTableExist("MSGRoute", DsnName))
	        {
		        try
		        {
			        using (var con = new OdbcConnection(DsnName))
			        {
				        using (var cmd = new OdbcCommand())
				        {
					        cmd.Connection = con;
					        cmd.CommandText =
						        "UPDATE Packet SET  MSGState = \"R\" Where MSGROUTE in (Select  MSGROUTE from MSGROUTE where Selected  = ?)  ";
							cmd.Parameters.Clear();
							cmd.Parameters.AddWithValue("@p1", "Y");
							con.Open();
							cmd.Prepare();
				        }
			        }
		        }
		        catch (OdbcException e)
		        {
			        MessageBox.Show(e.Message);
		        }
	        }
        }
        

        #endregion

        #region SqlupdateMSGUpdateSubject

        public void SqlupdateMsgUpdateSubject()
        {
            if (DoesTableExist("MSGSubject", DsnName))
            {
			try
				{
				using (var con = new OdbcConnection(DsnName))
					{
					using (var cmd = new OdbcCommand())
						{
						cmd.Connection = con;
						cmd.CommandText ="UPDATE Packet SET  MSGState = \"R\" Where MSGSubject in (Select  MSGSubject from MSGSubject where Selected  = ?)  ";
						cmd.Parameters.Clear();
						cmd.Parameters.AddWithValue("@p1", "Y");
						con.Open();
						cmd.Prepare();
						}
					}
				}
			catch (OdbcException e)
				{
				MessageBox.Show(e.Message);
				} 
            }
        }

        #endregion

        #region WriteST

        public bool WriteST(string textVale, string fileName)
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

        #region DeleteST
        public bool? DeleteST(string fileName)
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
                path = path + @"\" + fileName + ".txt";
                if (File.Exists(path))
                {
                    File.Delete(filePath);
                    return null;
                }
                else
                {
                    File.Delete(path);
                    return true;
                }

                return null;


            } //end try
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        } //end write

        #endregion

        #region RXST
        public string RXST(string fileName)
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

        #region RX
        public string RX()
        {
            string myString = null;
            string path = Directory.GetCurrentDirectory() + @"\Data";
            if (!Directory.Exists(path))
            {
                // Try to create the directory.
                Directory.CreateDirectory(path);
            }
            path = path + @"\myMailList.txt";

            if (File.Exists(path))
            {
                StreamReader myFile = new StreamReader(path);
                myString = myFile.ReadToEnd();
            }
            return myString;
        }
        #endregion

        #region

        public void UpdateSQLTO()
        {
            try
            {
                using (var con = new OdbcConnection(DsnName))
                {
                    using (var cmd = new OdbcCommand())
                    {
                        cmd.CommandText = "UPDATE MSGTO SET  Selected = ? Where Selected not = ? or   Where Selected not = ? not in (Select MSGTO From MSGTO )";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@p1", "P");
                        cmd.Parameters.AddWithValue("@p2", "R");
                        cmd.Parameters.AddWithValue("@p3", "D");
                        cmd.Connection = con;
                        con.Open();
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                        con.Close();
                       /* UPDATE MSGTO SET  Selected = "P"
Where Selected <> "R" or Selected <> "D"
not in
(Select MSGTO From MSGTO )   */
                    }
                }
            }
            catch (OdbcException e)
            {
                MessageBox.Show(e.Message);
            }
        }
        #endregion

    }
} //end name-space