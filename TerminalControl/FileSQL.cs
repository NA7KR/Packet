#region Using Directive

using System;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Windows.Forms;

#endregion

namespace PacketComs
    {
    public class FileSQL
        {
        
        public FileSQL()
            {
            
            if 
            (SQLInsert("CREATE TABLE  Packet ( MSG int PRIMARY KEY, MSGTSLD CHAR(3), MSGSize int, MSGTO CHAR(6), MSGRoute CHAR(7),MSGFrom CHAR(6), MSGDateTime CHAR(9), MSGSubject CHAR(30), MSGState CHAR(8)     )"))
            { }
            }

        #region SQLSELECT
        public string SQLSELECT(OdbcCommand Query)
        {
            try
            {
                OdbcConnection sqlConn = new OdbcConnection("DSN=Packet");
                OdbcDataAdapter sqlAdapt = new OdbcDataAdapter(Query);
                OdbcCommandBuilder sqlCmdBuilder = new OdbcCommandBuilder(sqlAdapt);
                DataSet sqlSet = new DataSet();
                sqlAdapt.Fill(sqlSet, "dataSetTableName");
                sqlConn.Close();
                return "";
            }
            catch (OdbcException e)
            {
                MessageBox.Show(e.Message);
                return "";
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