#region Using Directive

using System;
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

        public FileSQL()
        {
            ODBC_Manager odbc = new ODBC_Manager();
            string dsnName = "Packet"; //Name of the DSN connection here
            string path = Directory.GetCurrentDirectory() + @"\Data";

            if (odbc.CheckForDSN("Packet") > 0)
            {
                DataSet dataS = SQLSELECT("SELECT MSG FROM  Packet where MSG = 101 ; ", "Packet");

                if (DoesTableExist(path + "\\packet") == false)
                    {
                    SQLInsert(
                        "CREATE TABLE  Packet ( MSG int PRIMARY KEY, MSGTSLD CHAR(3), MSGSize int, MSGTO CHAR(6), MSGRoute CHAR(7),MSGFrom CHAR(6), MSGDateTime CHAR(9), MSGSubject CHAR(30), MSGState CHAR(8)    )");
                    }
                
  
            }
            else
            {
                  odbc.CreateDSN(dsnName);
                  MessageBox.Show("No Packet System DSN " + Environment.NewLine +
                                  "Please make one...","Critical Warning",MessageBoxButtons.OK,MessageBoxIcon.Error);
                  Environment.Exit(1);
            }

           
        }

        #region SQLSELECT
        public static DataSet SQLSELECT(string Query, string tableName)
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
                        if (row.ItemArray[0].ToString() == TableName)
                            {
                                TableExists = true;
                                break;
                            }
                        }
                    }
                catch (Exception e)
                    {
                    // Handle your ERRORS!
                    }

                finally
                    {
                    DbConnection.Close();
                    }
                }     
            return TableExists;
        }
        #endregion

        #region Write

        public bool Write(string textVale)
            {
            try
                {
                SQLInsert("INSERT INTO  Packet " +
                             "( MSG," +
                             "MSGTSLD," +
                             "MSGSize," +
                             "MSGTO," +
                             "MSGRoute," +
                             "MSGFrom," +
                             "MSGDateTime," +
                             "MSGSubject," +
                             "MSGState  ) VALUES ( '101','BF','1504','SWPC','@WW','CX2SA','0318/1811','Solar Region Summary','')");
                string path = Directory.GetCurrentDirectory() + @"\Data";
                if (!Directory.Exists(path))
                    {
                    // Try to create the directory.
                    Directory.CreateDirectory(path);
                    }
                path = path + @"\myMailList.txt";
                File.AppendAllText(path, textVale);
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
        }
    } //end name-space