﻿#region Using Directive

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Windows.Forms;

#endregion Using Directive

namespace Packet
{
    #region Class

    internal class Sql
    {
        private readonly DtoCustom _custom = new DtoCustom();
        private readonly DtoListMsgFrom _msgfrom = new DtoListMsgFrom();
        private readonly DtoListMsgRoute _msgroute = new DtoListMsgRoute();
        private readonly DtoListMsgSubject _msgsubject = new DtoListMsgSubject();
        private readonly DtoListMsgto _msgtodto = new DtoListMsgto();
        private readonly DtoPacket _packet = new DtoPacket();
        private readonly DtoListReply _reply = new DtoListReply();

        #region SQLSELECTRD

        public List<DtoPacket> SqlselectRd()
        {
            var packets = new List<DtoPacket>();
            try
            {
                using (var con = new OdbcConnection(Main.DsnName))
                {
                    using (var cmd = new OdbcCommand())
                    {
                        {
                            cmd.Connection = con;
                            cmd.CommandText =
                                "SELECT * FROM Packet Where MSGState = 'R' or MSGState = 'P'  or MSGState = 'V'";
                            con.Open();
                            cmd.ExecuteNonQuery();
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var packet = new DtoPacket((int)reader.GetValue(0),
                                        (string)reader.GetValue(1),
                                        (int)reader.GetValue(2),
                                        (string)reader.GetValue(3),
                                        (string)reader.GetValue(4),
                                        (string)reader.GetValue(5),
                                        (string)reader.GetValue(6),
                                        (string)reader.GetValue(7),
                                        (string)reader.GetValue(8));
                                    packets.Add(packet);
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

        #endregion SQLSELECTRD

        #region SQLSELECT

        public List<DtoPacket> Sqlselect()
        {
            var packets = new List<DtoPacket>();
            try
            {
                using (var con = new OdbcConnection(Main.DsnName))
                {
                    using (var cmd = new OdbcCommand())
                    {
                        {
                            cmd.Connection = con;
                            cmd.CommandText = "SELECT * FROM Packet ";
                            con.Open();
                            cmd.ExecuteNonQuery();
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var packet = new DtoPacket((int)reader.GetValue(0),
                                        (string)reader.GetValue(1),
                                        (int)reader.GetValue(2),
                                        (string)reader.GetValue(3),
                                        (string)reader.GetValue(4),
                                        (string)reader.GetValue(5),
                                        (string)reader.GetValue(6),
                                        (string)reader.GetValue(7),
                                        null);
                                    packets.Add(packet);
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

        #endregion SQLSELECT

        #region SQLReply

        public List<DtoListReply> SqlReply()
        {
            var packets = new List<DtoListReply>();
            try
            {
                using (var con = new OdbcConnection(Main.DsnName))
                {
                    using (var cmd = new OdbcCommand())
                    {
                        {
                            cmd.Connection = con;
                            cmd.CommandText = "SELECT * FROM Reply ";
                            con.Open();
                            cmd.ExecuteNonQuery();
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var packet = new DtoListReply((int)reader.GetValue(0),
                                        (string)reader.GetValue(1),
                                        (string)reader.GetValue(2),
                                        (int)reader.GetValue(3),
                                        (string)reader.GetValue(4),
                                        (string)reader.GetValue(5),
                                        (string)reader.GetValue(6));
                                    packets.Add(packet);
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

        #endregion SQLReply

        #region SQLSELECT_ON_Lists_Msgto

        public List<DtoListMsgto> SQLSELECT_ON_Lists_Msgto()
        {
            var selectLists = new List<DtoListMsgto>();
            try
            {
                using (var con = new OdbcConnection(Main.DsnName))
                {
                    using (var cmd = new OdbcCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "SELECT MSGTO,Selected FROM MSGTO";
                        con.Open();
                        cmd.ExecuteNonQuery();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var selectList = new DtoListMsgto(
                                    (string)convertDBNull(reader.GetValue(0)),
                                    (string)convertDBNull(reader.GetValue(1)),
                                    DateTime.MinValue);
                                selectLists.Add(selectList);
                            }
                        }
                    }
                    con.Close();
                }
            }
            catch (OdbcException e)
            {
                MessageBox.Show(e.Message);
            }
            return selectLists;
        }

        #endregion SQLSELECT_ON_Lists_Msgto

        #region SQLSELECT_ON_Lists_MsgFrom

        public List<DtoListMsgFrom> SQLSELECT_ON_Lists_MsgFrom()
        {
            var selectLists = new List<DtoListMsgFrom>();
            try
            {
                var sqlConn = new OdbcConnection(Main.DsnName);
                using (var cmd = new OdbcCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = "SELECT MSGFROM,Selected FROM MSGFROM";
                    sqlConn.Open();
                    cmd.ExecuteNonQuery();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var selectList = new DtoListMsgFrom(
                                (string)convertDBNull(reader.GetValue(0)),
                                (string)convertDBNull(reader.GetValue(1)),
                                DateTime.MinValue);
                            selectLists.Add(selectList);
                        }
                    }
                    sqlConn.Close();
                }
            }
            catch (OdbcException e)
            {
                MessageBox.Show(e.Message);
            }
            return selectLists;
        }

        #endregion SQLSELECT_ON_Lists_MsgFrom

        #region SQLSELECT_ON_Lists_Route

        public List<DtoListMsgRoute> SQLSELECT_ON_Lists_MsgRoute()
        {
            var selectLists = new List<DtoListMsgRoute>();
            try
            {
                using (var con = new OdbcConnection(Main.DsnName))
                {
                    using (var cmd = new OdbcCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "SELECT MSGROUTE,Selected FROM MSGROUTE";
                        con.Open();
                        cmd.ExecuteNonQuery();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var selectList = new DtoListMsgRoute(
                                    (string)convertDBNull(reader.GetValue(0)),
                                    (string)convertDBNull(reader.GetValue(1)),
                                    DateTime.MinValue);
                                selectLists.Add(selectList);
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

        #endregion SQLSELECT_ON_Lists_Route

        #region SQLSELECT_ON_Lists_MsgSubject

        public List<DtoListMsgSubject> SQLSELECT_ON_Lists_MsgSubject()
        {
            var selectLists = new List<DtoListMsgSubject>();
            try
            {
                using (var con = new OdbcConnection(Main.DsnName))
                {
                    using (var cmd = new OdbcCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "SELECT MSGSUBJECT,Selected FROM MSGSUBJECT";
                        con.Open();
                        cmd.ExecuteNonQuery();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var selectList = new DtoListMsgSubject(
                                    (string)convertDBNull(reader.GetValue(0)),
                                    (string)convertDBNull(reader.GetValue(1)),
                                    DateTime.MinValue);
                                selectLists.Add(selectList);
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

        #endregion SQLSELECT_ON_Lists_MsgSubject

        #region SQLSELECTOPTION

        public void SqlselectOptrion(string tableName)
        {
            try
            {
                using (var con = new OdbcConnection(Main.DsnName))
                {
                    using (var cmd = new OdbcCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "Insert into " + tableName + " ( " + tableName + " ,DateCreate ) " +
                                          " Select DISTINCT " + tableName + " , now() from Packet Where " + tableName +
                                          " not in (Select " + tableName + " from " + tableName + " )";
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch (OdbcException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #endregion SQLSELECTOPTION

        #region convertDBNull

        private object convertDBNull(object o)
        {
            if (o is DBNull)
            {
                return null;
            }
            return o;
        }

        #endregion convertDBNull

        #region WriteSQLPacketUpdate

        public void WriteSqlPacketUpdate(int value, string textValue)
        {
            try
            {
                _packet.set_MSG(value);
                _packet.set_MSGState(textValue);
                Sqlupdatepacket(_packet);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #endregion WriteSQLPacketUpdate

        #region WriteSQLCustomUpdate

        public void WriteSqlCustomUpdate(int value, string customName, string customQuery, string tableName,
            string enable)
        {
            try
            {
                _custom.set_ID(value);
                _custom.set_CustomName(customName);
                _custom.set_CustomQuery(customQuery);
                _custom.set_TableName(tableName);
                _custom.set_Enable(enable);
                SqlupdateCustom(_custom);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #endregion WriteSQLCustomUpdate

        #region WriteSQLReplyUpdate

        public void WriteSqlReplyUpdate(string filename, string status, int msgnumber, string msgtype, string msgcall,
            string msggroup, bool update)
        {
            try
            {
                //_reply.set_MSGID(value);
                _reply.set_MSGFileName(filename);
                _reply.set_Status(status);
                _reply.set_MSGNumber(msgnumber);
                _reply.set_Type(msgtype);
                _reply.set_Call(msgcall);
                _reply.set_Group(msggroup);
                SqlReplyInsert(_reply, update);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #endregion WriteSQLReplyUpdate

        #region SqlReply

        public void SqlReplyInsert(DtoListReply reply, bool update)
        {
            try
            {
                using (var con = new OdbcConnection(Main.DsnName))
                {
                    using (var cmd = new OdbcCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        cmd.CommandText =
                            ("INSERT into Send ( FileName,  Status,MSGNumber, MSGType, MSGCall, MSGGroup) VALUES (?, ?, ?, ?, ?, ?)");
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@p1", reply.get_MSGFileName());
                        cmd.Parameters.AddWithValue("@p2", reply.get_Status());
                        cmd.Parameters.AddWithValue("@p3", reply.get_MSGNumber());
                        cmd.Parameters.AddWithValue("@p4", reply.get_Type());
                        cmd.Parameters.AddWithValue("@p5", reply.get_Call());
                        cmd.Parameters.AddWithValue("@p6", reply.get_Group());
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

        #endregion SqlReply

        #region SqlupdateCustom

        public void SqlupdateCustom(DtoCustom custom)
        {
            try
            {
                using (var con = new OdbcConnection(Main.DsnName))
                {
                    using (var cmd = new OdbcCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = ("SELECT count(*) from  CustomQuery WHERE ID=?");
                        con.Open();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@p1", custom.get_ID());
                        var count = (int)cmd.ExecuteScalar();
                        if (count > 0)
                        {
                            cmd.CommandText =
                                ("UPDATE CustomQuery  SET CustomName=?,CustomQuery=?,TableName=?,Enable=? where ID=?");
                        }
                        else
                        {
                            cmd.CommandText =
                                ("INSERT into CustomQuery (CustomName, CustomQuery, TableName, Enable) VALUES (?, ?, ?, ?)");
                        }
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@p1", custom.get_CustomName());
                        cmd.Parameters.AddWithValue("@p2", custom.get_CustomQuery());
                        cmd.Parameters.AddWithValue("@p3", custom.get_TableName());
                        cmd.Parameters.AddWithValue("@p4", custom.get_Enable());
                        cmd.Parameters.AddWithValue("@p5", custom.get_ID());
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

        #endregion SqlupdateCustom

        #region SQLUPDATEPACKET

        public void Sqlupdatepacket(DtoPacket packetdto)
        {
            try
            {
                using (var con = new OdbcConnection(Main.DsnName))
                {
                    using (var cmd = new OdbcCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "UPDATE Packet SET  MSGState = ? Where MSG = ?   ";
                        cmd.Parameters.Clear();
                        con.Open();
                        cmd.Parameters.AddWithValue("@p1", packetdto.get_MSGState());
                        cmd.Parameters.AddWithValue("@p2", packetdto.get_MSG());
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

        #endregion SQLUPDATEPACKET

        #region WriteSQLMSGTOUpdate

        public void WriteSqlmsgtoUpdate(string value, string textValue)
        {
            _msgtodto.set_MSGTO(value);
            _msgtodto.set_Selected(textValue);
            SqlupdateMsgUpdate(_msgtodto);
        }

        #endregion WriteSQLMSGTOUpdate

        #region SqlupdateMSGUpdate

        public void SqlupdateMsgUpdate(DtoListMsgto packetdto)
        {
            try
            {
                using (var con = new OdbcConnection(Main.DsnName))
                {
                    using (var cmd = new OdbcCommand())
                    {
                        cmd.CommandText = "UPDATE MSGTO SET  Selected = ? Where MSGTO = ?   ";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@p1", packetdto.get_Selected());
                        cmd.Parameters.AddWithValue("@p2", packetdto.get_MSGTO());
                        con.Open();
                        cmd.Connection = con;
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

        #endregion SqlupdateMSGUpdate

        #region WriteSQLMSGFromUpdate

        public void WriteSqlmsgFromUpdate(string value, string textValue)
        {
            _msgfrom.set_MSGFROM(value);
            _msgfrom.set_Selected(textValue);
            SqlupdateMsgfromUpdate(_msgfrom);
        }

        #endregion WriteSQLMSGFromUpdate

        #region SqlupdateMSGFROMUpdate

        public void SqlupdateMsgfromUpdate(DtoListMsgFrom packetdto)
        {
            try
            {
                using (var con = new OdbcConnection(Main.DsnName))
                {
                    using (var cmd = new OdbcCommand())
                    {
                        cmd.CommandText = "UPDATE MSGFrom SET  Selected = ? Where MSGFrom = ?   ";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@p1", packetdto.get_Selected());
                        cmd.Parameters.AddWithValue("@p2", packetdto.get_MSGFROM());
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

        #endregion SqlupdateMSGFROMUpdate

        #region WriteSQLMSGRouteUpdate

        public void WriteSqlmsgRouteUpdate(string value, string textValue)
        {
            _msgroute.set_MSGRoute(value);
            _msgroute.set_Selected(textValue);
            SqlupdateMsgrouteUpdate(_msgroute);
        }

        #endregion WriteSQLMSGRouteUpdate

        #region SqlupdateMSGROUTEUpdate

        public void SqlupdateMsgrouteUpdate(DtoListMsgRoute packetdto)
        {
            try
            {
                using (var con = new OdbcConnection(Main.DsnName))
                {
                    using (var cmd = new OdbcCommand())
                    {
                        cmd.CommandText = "UPDATE MSGRoute SET  Selected = ? Where MSGRoute = ?   ";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@p1", packetdto.get_Selected());
                        cmd.Parameters.AddWithValue("@p2", packetdto.get_MSGROUTE());
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

        #endregion SqlupdateMSGROUTEUpdate

        #region WriteSQLMSGSubjectUpdate

        public void WriteSqlmsgSubjectUpdate(string value, string textValue)
        {
            _msgsubject.set_MSGSubject(value);
            _msgsubject.set_Selected(textValue);
            SqlupdateMsgsubjectUpdate(_msgsubject);
        }

        #endregion WriteSQLMSGSubjectUpdate

        #region SqlupdateMSGFROMUpdate

        public void SqlupdateMsgsubjectUpdate(DtoListMsgSubject packetdto)
        {
            try
            {
                using (var con = new OdbcConnection(Main.DsnName))
                {
                    using (var cmd = new OdbcCommand())
                    {
                        cmd.CommandText = "UPDATE MSGSubject SET  Selected = ? Where MSGSubject = ?   ";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@p1", packetdto.get_Selected());
                        cmd.Parameters.AddWithValue("@p2", packetdto.get_MSGSubject());
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

        #endregion SqlupdateMSGFROMUpdate

        #region DeleteST

        public bool? DeleteSt(string fileName, string pathNo)
        {
            try
            {
                var path = Directory.GetCurrentDirectory() + @"\Data\" + pathNo + @"\";
                if (!Directory.Exists(path))
                {
                    // Try to create the directory.
                    Directory.CreateDirectory(path);
                }
                var filePath = path + @"\" + fileName + ".txt";
                path = path + @"\" + fileName + ".txt";
                if (File.Exists(path))
                {
                    File.Delete(filePath);
                    return null;
                }
                File.Delete(path);
                return true;
            } //end try
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        } //end write

        #endregion DeleteST

        #region RXST

        public string Rxst(string fileName, string pathNo)
        {
            string myString = null;
            var path = Directory.GetCurrentDirectory() + @"\Data\" + pathNo + @"\";
            if (!Directory.Exists(path))
            {
                // Try to create the directory.
                Directory.CreateDirectory(path);
            }
            var filePath = path + @"\" + fileName + ".txt";
            //path = path + @"\" + fileName + ".txt";
            if (File.Exists(filePath))
            {
                var myFile = new StreamReader(filePath);
                myString = myFile.ReadToEnd();
                myFile.Close();
            }
            return myString;
        }

        #endregion RXST

        #region SQL delete row

        public static void DeleteRow(string table, string columnName, string idNumber)
        {
            try
            {
                using (var con = new OdbcConnection(Main.DsnName))
                {
                    con.Open();
                    using (var cmd = new OdbcCommand())
                    {
                        cmd.CommandText = ("DELETE FROM " + table + " WHERE " + columnName + " = " + idNumber);
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch (SystemException ex)
            {
                MessageBox.Show(string.Format("An error occurred: {0}", ex.Message));
            }
        }

        #endregion SQL delete row

        #region delete days

        public static void Deletedays(int days)
        {
            using (var con = new OdbcConnection(Main.DsnName))
            {
                con.Open();
                using (var da = new OdbcDataAdapter("SELECT MSG, MSGDateTime FROM Packet Where MSGState is null", con))
                {
                    var cb = new OdbcCommandBuilder(da);
                    cb.QuotePrefix = "[";
                    cb.QuoteSuffix = "]";
                    var sYear = 2000;
                    var dt = new DataTable();
                    da.Fill(dt);
                    var dateNow = DateTime.Now;
                    var now = (TimeZoneInfo.ConvertTimeToUtc(dateNow));
                    // save current date/time (without seconds) for comparison
                    var currDateTime = new DateTime(sYear, now.Month, now.Day, now.Hour, now.Minute, 0);
                    foreach (DataRow r in dt.Rows)
                    {
                        var mdhm = r["MSGDateTime"].ToString();
                        // evaluate as current year
                        var sMonth = Convert.ToInt32(mdhm.Substring(0, 2));
                        var sDay = Convert.ToInt32(mdhm.Substring(2, 2));
                        var sHr = Convert.ToInt32(mdhm.Substring(5, 2));
                        var sMin = Convert.ToInt32(mdhm.Substring(7, 2));
                        if (sDay == 29 && sMonth == 2)
                        {
                        }
                        var rowDateTime = new DateTime(sYear, sMonth, sDay, sHr, sMin, 0);
                        // if in future then convert to previous year
                        if (rowDateTime > currDateTime)
                        {
                            rowDateTime = rowDateTime.AddYears(-1);
                        }
                        if (rowDateTime.AddDays(days) < currDateTime)
                        {
                            r.Delete();
                        }
                    }
                    da.Update(dt); // write changes back to database
                }
                con.Close();
            }
        }

        #endregion delete days

        #region clear

        public void Sqlupdateclear(string sTable)
        {
            try
            {
                using (var con = new OdbcConnection(Main.DsnName))
                {
                    using (var cmd = new OdbcCommand())
                    {
                        cmd.CommandText = "UPDATE " + sTable + " SET  Selected = null Where Selected <> null";
                        con.Open();
                        cmd.Connection = con;
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

        #endregion clear

        #region SQL delete count

        public static void DeleteCount(int number)
        {
            if (number > 0)
            {
                try
                {
                    using (var con = new OdbcConnection(Main.DsnName))
                    {
                        con.Open();
                        using (var cmd = new OdbcCommand())
                        {
                            cmd.CommandText = ("delete * From Packet where MSG not in(select top " + number +
                                               " MSG from Packet order by MSG desc  )");
                            cmd.Connection = con;
                            cmd.ExecuteNonQuery();
                        }
                        con.Close();
                    }
                }
                catch (SystemException ex)
                {
                    MessageBox.Show(string.Format("An error occurred: {0}", ex.Message));
                }
            }
        }

        #endregion SQL delete count
    }

    #endregion Class
}