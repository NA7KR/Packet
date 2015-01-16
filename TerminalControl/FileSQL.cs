﻿#region Using Directive

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
		        if (odbc.CheckForDsn(dsnTableName) > 0)
		        {
			        if (DoesTableExist(dsnTableName, DsnName) == false)
			        {
				        SqlMakeTable("CREATE TABLE " + dsnTableName +
				                     "( MSG int PRIMARY KEY, MSGTSLD CHAR(3), MSGSize int, MSGTO CHAR(6), MSGRoute CHAR(7),MSGFrom CHAR(6), MSGDateTime CHAR(9), MSGSubject CHAR(30), MSGState CHAR(8) )");
			        }
		        }
		        else
		        {
			        odbc.CreateDsn(DsnName);
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
	                    cmdp.Prepare();
                        cmdp.ExecuteNonQuery();       
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
        public void SelectMakeTable(string textVale, Int32 intsize, string tableName,  string systemDsn)
        {
            if (odbc.CheckForDsn(systemDsn) > 0)
            {
                if (DoesTableExist(tableName, DsnName) == false)
                {
                    SqlMakeTable("CREATE TABLE " + tableName + " (  " + textVale + " CHAR(" + intsize  + "), DateCreate datetime, Selected CHAR(1)  )");
                }
            }
            else
            {
                odbc.CreateDsn(DsnName);
                MessageBox.Show("No Packet System DSN " + Environment.NewLine + "Please make one. Must be name Packet",  "Critical Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }
        #endregion

        #region SelectMakeCustomTable
        public void SelectMakeCustomTable(   string systemDsn)
        {
            if (odbc.CheckForDsn(systemDsn) > 0)
            {
                if (DoesTableExist("CustomTable", DsnName) == false)
                {
                    SqlMakeTable("CREATE TABLE CustomTable ( ID AUTOINCREMENT PRIMARY KEY ,CustomName CHAR(20), CustomTable CHAR(50), TableName CHAR(20), Enable CHAR(1)  )");
                }
            }
            else
            {
                odbc.CreateDsn(DsnName);
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

        #region SQLPacket Delete
        public void SqlPacketDelete(String tableName)
        {
		if (DoesTableExist(tableName, DsnName))
			{
			try
				{
				using (var con = new OdbcConnection(DsnName))
					{
					using (var cmd = new OdbcCommand())
						{
						cmd.Connection = con;
						cmd.CommandText = "Delete From Packet  Where "+ tableName + " in  ( Select "+ tableName +" from "+ tableName +" where Selected  = ? )   ";
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

        #region SQLSelectMail
		public int[] SqlSelectMail()
        {
            using (var con = new OdbcConnection(DsnName))
            {
                int msgId; 
                con.Open();
				String msgList = "SELECT MSG FROM Packet where MSGState = ?";
	            using (var cmd = new OdbcCommand(msgList, con))
	            {
		            using (var da = new OdbcDataAdapter(cmd))
		            {
			            cmd.Parameters.Clear();
			            cmd.Parameters.AddWithValue("@p1", "P");
			            cmd.Prepare();
		                int[] MsgList;
		                using (var dt = new DataTable())
			            {
				            da.Fill(dt);
				            int no = dt.Rows.Count;
				            MsgList = new int[no];
				            for (int i = 0; i < dt.Rows.Count; i++)
				            {
					            msgId = Convert.ToInt32(dt.Rows[i]["MSG"]);
					            MsgList[i] = msgId;
				            }
				            con.Close();
				            return MsgList;
			            }
		            }
	            }
            }
        }
		#endregion

		#region SqlupdateRead

		public void SqlupdateRead(Int32 msgNumber)
        {
            if (DoesTableExist("Packet", DsnName))
            {
	            try
	            {
		            using (var con = new OdbcConnection(DsnName))
		            {
			            using (var cmd = new OdbcCommand())
			            {
				            cmd.Connection = con;
				            cmd.CommandText =
					            "UPDATE Packet SET  MSGState = ? Where MSG = ?  ";
							cmd.Parameters.Clear();
							cmd.Parameters.AddWithValue("@p1", "R");
							cmd.Parameters.AddWithValue("@p2", msgNumber);
							con.Open();
							cmd.Prepare();
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
        }


        #endregion

        #region WriteST

        public bool WriteSt(string textVale, string fileName,string pathNo)
        {

            try
            {
                string path = Directory.GetCurrentDirectory() + @"\Data\" + pathNo + @"\";
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

		#region	   UpdateSqlto

		public void UpdateSqlto(string tableName)
        {
		if (DoesTableExist(tableName, DsnName))
			{
				try
				{
					using (var con = new OdbcConnection(DsnName))
					{
						using (var cmd = new OdbcCommand())
						{
						    cmd.CommandText =
                            "UPDATE Packet " +
                            " INNER JOIN " + tableName + " ON Packet." + tableName + " = " + tableName + "." + tableName +
                            " SET Packet.MSGState = ? " +
                            " where (Packet.MSGState is null    " +
                            " or (Packet.MSGState<> ? And Packet.MSGState<> ?) ) " +
                            " and  " + tableName + ".Selected = ?" ;

                            //  "UPDATE Packet SET Packet.MSGState = ? Where (MSGState <> ? and  MSGState <> ? and  MSGState <> ?) and Exists( Select " + tableName+ ".Selected From " + tableName + " Where " + tableName + "." + tableName + " = Packet." + tableName + " and Selected = ? )  ";
							cmd.Parameters.Clear();
							cmd.Parameters.AddWithValue("@p1", "P");
							cmd.Parameters.AddWithValue("@p2", "R");
                            cmd.Parameters.AddWithValue("@p2", "V");
							cmd.Parameters.AddWithValue("@p3", "D");
							cmd.Parameters.AddWithValue("@p4", "Y");
							cmd.Connection = con;
							con.Open();
							cmd.Prepare();
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
        }
        #endregion

    }
} //end name-space