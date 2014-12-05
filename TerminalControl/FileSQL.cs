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
        private OdbcConnection sqlConn;


        public FileSql()
        {
            sqlConn = new OdbcConnection("DSN=Packet");
            //sqlComm = new OdbcCommand();
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
                        
            ODBC_Manager odbc = new ODBC_Manager();

            string dsnName = "Packet";

            if (odbc.CheckForDSN("Packet") > 0)
                {
                if (DoesTableExist("Packet") == false)
                    {
                    SQLMakeTable(
                        "CREATE TABLE  Packet ( MSG int PRIMARY KEY, MSGTSLD CHAR(3), MSGSize int, MSGTO CHAR(6), MSGRoute CHAR(7),MSGFrom CHAR(6), MSGDateTime CHAR(9), MSGSubject CHAR(30), MSGState CHAR(8) )");
                    }
                }
            else
                {
                odbc.CreateDSN(dsnName);
                MessageBox.Show("No Packet System DSN " + Environment.NewLine + "Please make one. Must be name Packet", "Critical Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
                }
            }

        #region SQLSELECT
        /*
        public static DataSet Sqlselect(string query, string tableName)
            {
            try
                {
                DataSet sqlSet = new DataSet();
                OdbcConnection sqlConn = new OdbcConnection("DSN=Packet");
                OdbcDataAdapter sqlAdapt = new OdbcDataAdapter();
                sqlAdapt.SelectCommand = new OdbcCommand(query, sqlConn);
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
         */
        #endregion

        #region SQLMakeTable
        private void SQLMakeTable(string query)
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

        #region SQLInsert
  
        public bool SQLInsert(DtoPacket packet)
        {
            sqlComm.Parameters.Clear();
            
     
            sqlComm.Parameters.AddWithValue("@p1",  packet.get_MSG());
            sqlComm.Parameters.AddWithValue("@p2", packet.get_MSGTSLD());
            sqlComm.Parameters.AddWithValue("@p3",  packet.get_MSGSize());
            sqlComm.Parameters.AddWithValue("@p4", packet.get_MSGTO());
            sqlComm.Parameters.AddWithValue("@p5", packet.get_MSGRoute());
            sqlComm.Parameters.AddWithValue("@p6", packet.get_MSGFrom());
            sqlComm.Parameters.AddWithValue("@p7", packet.get_MSGDateTime());
            sqlComm.Parameters.AddWithValue("@p8", packet.get_MSGSubject());

            try
            {
            sqlComm.Prepare();
                sqlComm.ExecuteNonQuery();
                //sqlConn.Close();
            }
            catch (OdbcException e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }
                    
            return true;
                
       
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
        public bool DoesTableExist(string tableName)
            {
            string dsnName = "DSN=Packet";
            bool tableExists = false;
            OdbcConnection dbConnection = new OdbcConnection(dsnName);
                {
                try
                    {
                    dbConnection.Open();
                    DataTable dt = dbConnection.GetSchema("TABLES");
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

                finally
                    {
                    dbConnection.Close();
                    }
                }
                return tableExists;
            }
        #endregion

        #region WriteSQL

        public bool WriteSql(string textValue)
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


                SQLInsert(packet);

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
        /*
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
        */
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

        #region DeleteST
        public bool? DeleteSt(string fileName)
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