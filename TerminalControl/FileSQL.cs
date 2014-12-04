#region Using Directive

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Windows.Forms;

#endregion

namespace PacketComs
    {
   
    public class FileSQL
        {
        ArrayList rxmsg =  new ArrayList() ;
        ArrayList rxtsld = new ArrayList() ;
        ArrayList rxsize = new ArrayList() ;
        ArrayList rxto =   new ArrayList() ;
        ArrayList rxroute = new ArrayList() ;
        ArrayList rxfrom = new ArrayList() ;
        ArrayList rxdate = new ArrayList() ;
        ArrayList rxsubject = new ArrayList() ;
        DtoPacket packet = new DtoPacket(); 
       

        public FileSQL()
        {
        ODBC_Manager odbc = new ODBC_Manager();
            string dsnName = "Packet"; 
            string path = Directory.GetCurrentDirectory() + @"\Data";
            if (odbc.CheckForDSN("Packet") > 0)
            {
                if (DoesTableExist(path + "\\" + dsnName) == false)
                    {
                    SQLInsert(
                        "CREATE TABLE  Packet ( MSG int PRIMARY KEY, MSGTSLD CHAR(3), MSGSize int, MSGTO CHAR(6), MSGRoute CHAR(7),MSGFrom CHAR(6), MSGDateTime CHAR(9), MSGSubject CHAR(30), MSGState CHAR(8) )");
                    } 
            }
            else
            {
                  odbc.CreateDSN(dsnName);
                  MessageBox.Show("No Packet System DSN " + Environment.NewLine + "Please make one. Must be name Packet","Critical Warning",MessageBoxButtons.OK,MessageBoxIcon.Error);
                  Environment.Exit(1);
            }           
        }

        #region SQLSELECT
        public static DataSet Sqlselect(string Query, string tableName)
        {
            try
            {
                DataSet sqlSet = new DataSet();
                OdbcConnection sqlConn = new OdbcConnection("DSN=Packet");
                OdbcDataAdapter sqlAdapt = new OdbcDataAdapter();
                sqlAdapt.SelectCommand = new OdbcCommand(Query,sqlConn);
                OdbcCommandBuilder sqlCmdBuilder = new OdbcCommandBuilder(sqlAdapt);
                sqlConn.Open();
                sqlAdapt.Fill(sqlSet, tableName);
                sqlAdapt.Update(sqlSet, tableName);
                sqlConn.Close();
                return sqlSet;
            }
            catch (OdbcException e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
            
        }
        #endregion

        #region SQLInsert
        public bool SQLInsert(string Query)
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

        #region SQLUPDATE

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
        #endregion

        #region DoesTableExist
        public bool DoesTableExist(string TableName)
        {
        string dsnName = "DSN=Packet"; 
            bool TableExists = false;
            OdbcConnection DbConnection = new OdbcConnection(dsnName);
            {
                try
                    {
                    DbConnection.Open();
                    DataTable dt = DbConnection.GetSchema("Tables");
                    foreach (DataRow row in dt.Rows)
                        {
                        if (row.ItemArray[0].ToString().ToLower() == TableName.ToLower())
                            {
                                TableExists = true;
                                break;
                            }
                        }
                    }
                catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                        return false;
                    }

                finally
                    {
                    DbConnection.Close();
                    }
                }     
            return TableExists;
        }
        #endregion

        #region WriteSQL

        public bool WriteSQL(string textValue)
        {
        try
        {
                packet.set_MSG(Convert.ToInt32(Mid(textValue, 0, 5)));
                packet.set_MSGTSLD  ("'" + Mid(textValue, 7, 4) + "'" );
                packet.set_MSGSize(Convert.ToInt32(Mid(textValue, 13, 5)));
                packet.set_MSGTO("'" + Mid(  textValue, 18, 6) + "'" );
                packet.set_MSGRoute("'" + Mid(textValue, 24, 8)+ "'" );
                packet.set_MSGFrom("'" + Mid(textValue, 32, 7)+ "'" );
                packet.set_MSGDateTime("'" + Mid(textValue, 39, 9)+ "'" );
                packet.set_MSGSubject("'" + Mid(textValue, 49, (textValue.Length - 49))+ "'" );


                SQLInsert("INSERT INTO  Packet " +
                          "(MSG," +
                          "MSGTSLD," +
                          "MSGSize," +
                          "MSGTO," +
                          "MSGRoute," +
                          "MSGFrom," +
                          "MSGDateTime," +
                          "MSGSubject) "  +
                          
                          "VALUES (" +
                          packet.get_MSG() + "," +
                          packet.get_MSGTSLD() + "," +
                          packet.get_MSGSize() + "," +
                          packet.get_MSGTO() + "," +
                          packet.get_MSGRoute() + "," +
                          packet.get_MSGFrom() + "," +
                          packet.get_MSGDateTime() + "," +
                          packet.get_MSGSubject() + ")");
                          
                return true;
            } 
        catch (Exception e)
            {
            MessageBox.Show(e.Message);
            return false;
            }
        } 
        #endregion 

        #region Write
        public bool Write(string textValue)
            {
            try
                {
                string path = Directory.GetCurrentDirectory() + @"\Data";
                if (!Directory.Exists(path))
                    {
                    // Try to create the directory.
                    Directory.CreateDirectory(path);
                    }
                path = path + @"\myMailList.txt";
                File.AppendAllText(path, textValue);
                return true;
                } //end try
            catch (Exception e)
                {
                MessageBox.Show(e.Message);
                return false;
                }
            } //end write

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
                path = path + @"\" + fileName + ".txt";
                if (File.Exists(path))
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
            } //end delete
        #endregion

        #region checkST
        public bool? CheckST(string fileName)
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

        #region Mid
        public static string Mid(string param, int startIndex, int length)
        {
            var result = " ";
            if (param == " ")
            {
                return null;
            }
             result = param.Substring(startIndex, length);
            return result;
        }
        #endregion
        }
    } //end name-space