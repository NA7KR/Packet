﻿#region Using Directive

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

#endregion Using Directive

namespace Packet
{
    public class FileSql
    {
        public const string DsnName = "DSN=Packet";
        private readonly OdbcCommand _cmdp = new OdbcCommand();
        private readonly OdbcConnection _conp = new OdbcConnection(DsnName);
        private readonly OdbcManager _odbc;
        private readonly DtoPacket _packet = new DtoPacket();
        #region Constructor

        public FileSql()
        {
            var dsnTableName = "Packet";
            _odbc = new OdbcManager();
            _cmdp.Connection = _conp;
            try
            {
                _conp.Open();
            }
            catch (Exception)
            {
                //
            }
            {
                if (_odbc.CheckForDsn(dsnTableName) > 0)
                {
                    if (DoesTableExist(dsnTableName, DsnName) == false)
                    {
                        SqlMakeTable("CREATE TABLE " + dsnTableName +
                                     "( MSG int PRIMARY KEY, MSGTSLD CHAR(3), MSGSize int, MSGTO CHAR(6), MSGRoute CHAR(7),MSGFrom CHAR(6), MSGDateTime CHAR(9), MSGSubject CHAR(30), MSGState CHAR(8) )");
                    }
                }
                else
                {
                    _odbc.CreateDsn(DsnName);
                    if (MessageBox.Show(
                        "No Packet System DSN found" + Environment.NewLine + "Please make one. Must be name Packet",
                        "Critical Warning", MessageBoxButtons.OK, MessageBoxIcon.Error
                        ) == DialogResult.OK)
                    {
                        var check = CheckForInternetConnection();
                        if (check)
                        {
                            Process.Start("https://github.com/NA7KR/Packet/wiki/Install_odbc");
                        }
                    }
                    Environment.Exit(1);
                }
            }
        }

        #endregion Constructor
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

        #endregion SQLMakeTable
        #region SQLInsertPacket

        private void SqlInsertPacket(DtoPacket packetdto)
        {
            try
            {
                _cmdp.CommandText = "INSERT INTO  Packet (MSG, MSGTSLD, MSGSize, MSGTO, MSGRoute, MSGFrom, MSGDateTime, MSGSubject) VALUES (?,?,?,?,?,?,?,?)";
                _cmdp.Parameters.Clear();
                _cmdp.Parameters.AddWithValue("@p1", packetdto.get_MSG());
                _cmdp.Parameters.AddWithValue("@p2", packetdto.get_MSGTSLD());
                _cmdp.Parameters.AddWithValue("@p3", packetdto.get_MSGSize());
                _cmdp.Parameters.AddWithValue("@p4", packetdto.get_MSGTO());
                _cmdp.Parameters.AddWithValue("@p5", packetdto.get_MSGRoute());
                _cmdp.Parameters.AddWithValue("@p6", packetdto.get_MSGFrom());
                _cmdp.Parameters.AddWithValue("@p7", packetdto.get_MSGDateTime());
                _cmdp.Parameters.AddWithValue("@p8", packetdto.get_MSGSubject());
                _cmdp.Prepare();
                _cmdp.ExecuteNonQuery();
            }
            catch
                (OdbcException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #endregion SQLInsertPacket
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

        #endregion DoesTableExist
        #region WriteSQLPacket

        public void WriteSqlPacket(string textValue)
        {
            try
            {
                _packet.set_MSG(Convert.ToInt32(Mid(textValue, 0, 5)));
                _packet.set_MSGTSLD(Mid(textValue, 7, 4));
                _packet.set_MSGSize(Convert.ToInt32(Mid(textValue, 13, 5)));
                _packet.set_MSGTO(Mid(textValue, 18, 6));
                _packet.set_MSGRoute(Mid(textValue, 24, 8));
                _packet.set_MSGFrom(Mid(textValue, 32, 7));
                _packet.set_MSGDateTime(Mid(textValue, 39, 9));
                _packet.set_MSGSubject(Mid(textValue, 49, (textValue.Length - 49)));
                SqlInsertPacket(_packet);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #endregion WriteSQLPacket
        #region SelectMakeTable

        public void SelectMakeTable(string textVale, int intsize, string tableName, string systemDsn)
        {
            if (_odbc.CheckForDsn(systemDsn) > 0)
            {
                if (DoesTableExist(tableName, DsnName) == false)
                {
                    SqlMakeTable("CREATE TABLE " + tableName + " (  " + textVale + " CHAR(" + intsize + "), DateCreate datetime, Selected CHAR(1)  )");
                }
            }
            else
            {
                _odbc.CreateDsn(DsnName);
                MessageBox.Show("No Packet System DSN " + Environment.NewLine + "Please make one. Must be name Packet", "Critical Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }

        #endregion SelectMakeTable
        #region ReplyMakeTable

        public void ReplyMakeTable(string systemDsn)
        {
            if (_odbc.CheckForDsn(systemDsn) > 0)
            {
                if (DoesTableExist("Send", DsnName) == false)
                {
                    SqlMakeTable("CREATE TABLE Send ( ID AUTOINCREMENT PRIMARY KEY , FileName CHAR(30), Status CHAR(1),MSGNumber INT, MSGType CHAR(1), MSGCall CHAR(7),MSGGroup CHAR(10)  )");
                }
            }
            else
            {
                _odbc.CreateDsn(DsnName);
                MessageBox.Show("No Send System DSN " + Environment.NewLine + "Please make one. Must be name Packet", "Critical Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }

        #endregion ReplyMakeTable
        #region SelectMakeCustomQuery

        public bool SelectMakeCustomQuery(string systemDsn)
        {
            if (_odbc.CheckForDsn(systemDsn) > 0)
            {
                if (DoesTableExist("CustomQuery", DsnName) == false)
                {
                    SqlMakeTable(
                        "CREATE TABLE CustomQuery ( ID AUTOINCREMENT PRIMARY KEY ,CustomName CHAR(20), CustomQuery CHAR(50), TableName CHAR(20), Enable CHAR(1)  )");
                    return false;
                }
                return true;
            }
            _odbc.CreateDsn(DsnName);
            MessageBox.Show("No Packet System DSN " + Environment.NewLine + "Please make one. Must be name Packet", "Critical Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(1);
            return false;
        }

        #endregion SelectMakeCustomQuery
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

        #endregion Mid
        #region SQLPacket Delete

        public void SqlPacketDelete(string tableName)
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
                            cmd.CommandText = "Delete From Packet  Where " + tableName + " in  ( Select " + tableName + " from " + tableName + " where Selected  = ? )   ";
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

        #endregion SQLPacket Delete
        #region SQLSelectMail

        public int[] SqlSelectMail()
        {
            using (var con = new OdbcConnection(DsnName))
            {
                con.Open();
                var msgList = "SELECT MSG FROM Packet where MSGState = ?";
                using (var cmd = new OdbcCommand(msgList, con))
                {
                    using (var da = new OdbcDataAdapter(cmd))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@p1", "P");
                        cmd.Prepare();
                        using (var dt = new DataTable())
                        {
                            da.Fill(dt);
                            var no = dt.Rows.Count;
                            var msgListr = new int[no];
                            for (var i = 0; i < dt.Rows.Count; i++)
                            {
                                var msgId = Convert.ToInt32(dt.Rows[i]["MSG"]);
                                msgListr[i] = msgId;
                            }
                            con.Close();
                            return msgListr;
                        }
                    }
                }
            }
        }

        #endregion SQLSelectMail
        #region SqlupdateRead

        public void SqlupdateRead(int msgNumber)
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

        #endregion SqlupdateRead
        #region SqlCustomRead

        public List<DtoCustom> SqlCustomRead()
        {
            var packets = new List<DtoCustom>();
            try
            {
                using (var con = new OdbcConnection(DsnName))
                {
                    using (var cmd = new OdbcCommand())
                    {
                        {
                            cmd.Connection = con;
                            cmd.CommandText = "SELECT * FROM CustomQuery";
                            con.Open();
                            cmd.ExecuteNonQuery();
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var custom = new DtoCustom((int)reader.GetValue(0),
                                        (string)reader.GetValue(1),
                                        (string)reader.GetValue(2),
                                        (string)reader.GetValue(3),
                                        (string)reader.GetValue(4));
                                    packets.Add(custom);
                                }
                            }
                            con.Close();
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

        #endregion SqlCustomRead
        #region WriteST

        public bool WriteSt(string textVale, string fileName, string pathNo, bool txt)
        {
            string filePath;
            filePath = null;
            try
            {
                //bool index = textVale.Contains("\u00B1");
                //MessageBox.Show(index.ToString());
                var path = Directory.GetCurrentDirectory() + @"\Data\" + pathNo + @"\";
                if (!Directory.Exists(path))
                {
                    // Try to create the directory.
                    Directory.CreateDirectory(path);
                }
                if (txt)
                {
                    filePath = path + @"\" + fileName + ".txt";
                }
                else
                {
                    filePath = path + @"\" + fileName;
                }
                byte[] y = Encoding.ASCII.GetBytes(textVale);
                //byte[] y = Encoding.UTF8.GetBytes(textVale);
                File.WriteAllBytes(filePath, y);
                //File.WriteAllText(filePath, textVale, Encoding.GetEncoding(1252));
                //File.WriteAllLines(filePath, textVale, Encoding.ASCII);
                return true;
            } //end try
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        } //end write

        #endregion WriteST
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
                                "UPDATE Packet " + " INNER JOIN " + tableName + " ON Packet." + tableName + " = " + tableName + "." +
                                tableName + " SET Packet.MSGState = ? " + " where (Packet.MSGState is null    " +
                                " or (Packet.MSGState<> ? And Packet.MSGState<> ? And Packet.MSGState<> ?) ) " + " and  " + tableName + ".Selected = ?";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@p1", "P");
                            cmd.Parameters.AddWithValue("@p2", "R");
                            cmd.Parameters.AddWithValue("@p3", "V");
                            cmd.Parameters.AddWithValue("@p4", "D");
                            cmd.Parameters.AddWithValue("@p5", "Y");
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
        #region	   UpdateSqlCistom

        public void UpdateSqlCustom(string tableName, string customQuery)
        {
            if (DoesTableExist("Packet", DsnName))
            {
                try
                {
                    using (var con = new OdbcConnection(DsnName))
                    {
                        using (var cmd = new OdbcCommand())
                        {
                            cmd.CommandText =
                                "UPDATE Packet " + " SET Packet.MSGState = ? " +
                                " where (Packet.MSGState is null    " + " or (Packet.MSGState <> ? And Packet.MSGState <> ? And Packet.MSGState <> ? ))  and  Packet." +
                                tableName + " like ?";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@p1", "P");
                            cmd.Parameters.AddWithValue("@p2", "R");
                            cmd.Parameters.AddWithValue("@p3", "V");
                            cmd.Parameters.AddWithValue("@p4", "D");
                            cmd.Parameters.AddWithValue("@p5", customQuery);
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
        #region Check for Internet

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://github.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
} //end name-space