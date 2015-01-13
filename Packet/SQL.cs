#region Using Directive

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using PacketComs;

#endregion

namespace Packet
{

	#region Class

    internal class Sql
    {
        private static readonly FileSql FileSql = new FileSql();
        private readonly DtoListMsgFrom _msgfrom = new DtoListMsgFrom();
        private readonly DtoListMsgRoute _msgroute = new DtoListMsgRoute();
        private readonly DtoListMsgSubject _msgsubject = new DtoListMsgSubject();
        private readonly DtoListMsgto _msgtodto = new DtoListMsgto();
        private readonly DtoPacket _packet = new DtoPacket();

        #region constructor

        public Sql()
        {

        }

        #endregion

        #region SQLSELECTRD

        public List<DtoPacket> SqlselectRD()
        {
            var packets = new List<DtoPacket>();

            try
            {
                using (var con = new OdbcConnection(Main.dsnName))
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
                                    var packet = new DtoPacket((int) reader.GetValue(0),
                                        (string) reader.GetValue(1),
                                        (int) reader.GetValue(2),
                                        (string) reader.GetValue(3),
                                        (string) reader.GetValue(4),
                                        (string) reader.GetValue(5),
                                        (string) reader.GetValue(6),
                                        (string) reader.GetValue(7),
                                        (string) reader.GetValue(8));
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

        #endregion

        #region SQLSELECT

        public List<DtoPacket> Sqlselect()
        {
            var packets = new List<DtoPacket>();

            try
            {
                using (var con = new OdbcConnection(Main.dsnName))
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
                                    var packet = new DtoPacket((int) reader.GetValue(0),
                                        (string) reader.GetValue(1),
                                        (int) reader.GetValue(2),
                                        (string) reader.GetValue(3),
                                        (string) reader.GetValue(4),
                                        (string) reader.GetValue(5),
                                        (string) reader.GetValue(6),
                                        (string) reader.GetValue(7),
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

        #endregion

        #region SQLSELECT_ON_Lists_Msgto

        public List<DtoListMsgto> SQLSELECT_ON_Lists_Msgto()
        {
            var selectLists = new List<DtoListMsgto>();
            try
            {
                using (var con = new OdbcConnection(Main.dsnName))
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
                                    (String) convertDBNull(reader.GetValue(0)),
                                    (String) convertDBNull(reader.GetValue(1)),
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

        #endregion

        #region SQLSELECT_ON_Lists_MsgFrom

        public List<DtoListMsgFrom> SQLSELECT_ON_Lists_MsgFrom()
        {
            var selectLists = new List<DtoListMsgFrom>();
            try
            {
                var sqlConn = new OdbcConnection(Main.dsnName);

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
                                (String) convertDBNull(reader.GetValue(0)),
                                (String) convertDBNull(reader.GetValue(1)),
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

        #endregion

        #region SQLSELECT_ON_Lists_Route

        public List<DtoListMsgRoute> SQLSELECT_ON_Lists_MsgRoute()
        {
            var selectLists = new List<DtoListMsgRoute>();
            try
            {
                using (var con = new OdbcConnection(Main.dsnName))
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
                                    (String) convertDBNull(reader.GetValue(0)),
                                    (String) convertDBNull(reader.GetValue(1)),
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

        #endregion

        #region SQLSELECT_ON_Lists_MsgSubject

        public List<DtoListMsgSubject> SQLSELECT_ON_Lists_MsgSubject()
        {
            var selectLists = new List<DtoListMsgSubject>();
            try
            {
                using (var con = new OdbcConnection(Main.dsnName))
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
                                    (String) convertDBNull(reader.GetValue(0)),
                                    (String) convertDBNull(reader.GetValue(1)),
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

        #endregion

        #region SQLSELECTOPTION

        public void SqlselectOptrion(string tableName)
        {
            try
            {
                using (var con = new OdbcConnection(Main.dsnName))
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

        #endregion

        #region  convertDBNull

        private object convertDBNull(object o)
        {
            if (o is DBNull)
            {
                return null;
            }
            return o;
        }

        #endregion

        #region WriteSQLPacketUpdate

        public void WriteSqlPacketUpdate(int value, String textValue)
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

        #endregion

        #region SQLUPDATEPACKET

        public void Sqlupdatepacket(DtoPacket packetdto)
        {
            try
            {
                using (var con = new OdbcConnection(Main.dsnName))
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

        #endregion

        #region WriteSQLMSGTOUpdate

        public void WriteSqlmsgtoUpdate(string value, String textValue)
        {
            _msgtodto.set_MSGTO(value);
            _msgtodto.set_Selected(textValue);
            SqlupdateMsgUpdate(_msgtodto);
        }

        #endregion

        #region SqlupdateMSGUpdate

        public void SqlupdateMsgUpdate(DtoListMsgto packetdto)
        {
            try
            {
                using (var con = new OdbcConnection(Main.dsnName))
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

        #endregion

        #region WriteSQLMSGFromUpdate

        public void WriteSqlmsgFromUpdate(string value, String textValue)
        {
            _msgfrom.set_MSGFROM(value);
            _msgfrom.set_Selected(textValue);
            SqlupdateMsgfromUpdate(_msgfrom);
        }

        #endregion

        #region SqlupdateMSGFROMUpdate

        public void SqlupdateMsgfromUpdate(DtoListMsgFrom packetdto)
        {
            try
            {
                using (var con = new OdbcConnection(Main.dsnName))
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

        #endregion

        #region WriteSQLMSGRouteUpdate

        public void WriteSqlmsgRouteUpdate(string value, String textValue)
        {
            _msgroute.set_MSGRoute(value);
            _msgroute.set_Selected(textValue);
            SqlupdateMsgrouteUpdate(_msgroute);
        }

        #endregion

        #region SqlupdateMSGROUTEUpdate

        public void SqlupdateMsgrouteUpdate(DtoListMsgRoute packetdto)
        {
            try
            {
                using (var con = new OdbcConnection(Main.dsnName))
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

        #endregion

        #region WriteSQLMSGSubjectUpdate

        public void WriteSqlmsgSubjectUpdate(string value, String textValue)
        {
            _msgsubject.set_MSGSubject(value);
            _msgsubject.set_Selected(textValue);
            SqlupdateMsgsubjectUpdate(_msgsubject);
        }

        #endregion

        #region SqlupdateMSGFROMUpdate

        public void SqlupdateMsgsubjectUpdate(DtoListMsgSubject packetdto)
        {
            try
            {
                using (var con = new OdbcConnection(Main.dsnName))
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

        #endregion

        #region DeleteST

        public bool? DeleteSt(string fileName, string pathNo)
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

            } //end try
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        } //end write

        #endregion

        #region RXST

        public string Rxst(string fileName, string pathNo)
        {
            string myString = null;
            string path = Directory.GetCurrentDirectory() + @"\Data\" + pathNo + @"\";
            if (!Directory.Exists(path))
            {
                // Try to create the directory.
                Directory.CreateDirectory(path);
            }
            string filePath = path + @"\" + fileName + ".txt";
            //path = path + @"\" + fileName + ".txt";
            if (File.Exists(filePath))
            {
                StreamReader myFile = new StreamReader(filePath);
                myString = myFile.ReadToEnd();
                myFile.Close();
            }
            return myString;

        }

        #endregion

        #region SQL delete row

        public static void deleteRow(string table, string columnName, string IDNumber)
        {
            try
            {
                using (var con = new OdbcConnection(Main.dsnName))
                {
                    con.Open();
                    using (var cmd = new OdbcCommand())
                    {
                        cmd.CommandText = ("DELETE FROM " + table + " WHERE " + columnName + " = " + IDNumber);
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

        #endregion

        #region delete days

        public static void deletedays(int days)
        {
            using (var con = new OdbcConnection(Main.dsnName))
            {
                con.Open();
                using (var da = new OdbcDataAdapter("SELECT MSG, MSGDateTime FROM Packet Where MSGState is null", con))
                {
                    var cb = new OdbcCommandBuilder(da);
                    cb.QuotePrefix = "[";
                    cb.QuoteSuffix = "]";
                    int s_year = 2000;
                    var dt = new DataTable();
                    da.Fill(dt);
                    DateTime dateNow = DateTime.Now;
                    var now = (TimeZoneInfo.ConvertTimeToUtc(dateNow));
                    // save current date/time (without seconds) for comparison
                    var currDateTime = new DateTime(s_year, now.Month, now.Day, now.Hour, now.Minute, 0);
                    foreach (DataRow r in dt.Rows)
                    {
                        string mdhm = r["MSGDateTime"].ToString();
                        // evaluate as current year
                        var s_month = Convert.ToInt32(mdhm.Substring(0, 2));
                        var s_day = Convert.ToInt32(mdhm.Substring(2, 2));
                        var s_hr = Convert.ToInt32(mdhm.Substring(5, 2));
                        var s_min = Convert.ToInt32(mdhm.Substring(7, 2));

                        if (s_day == 29 && s_month == 2)
                        {

                        }

                        DateTime rowDateTime = new DateTime(s_year, s_month, s_day, s_hr, s_min, 0);
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

        #endregion



        #region SQL delete count

        public static void deleteCount(int INumber)
        {
            try
            {
                using (var con = new OdbcConnection(Main.dsnName))
                {
                    con.Open();
                    using (var cmd = new OdbcCommand())
                    {
                        cmd.CommandText = ("delete * From Packet where MSG not in(select top " + INumber +" MSG from Packet order by MSG desc  )");
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

        #endregion
    }

    #endregion CLASS
}