using ADODB;
using ADOX;
using System;
using System.Runtime.InteropServices;

namespace Packet
{
    public class OdbcManager
    {
        [DllImport("ODBCCP32.dll")]
        public static extern int SQLGetPrivateProfileString(string lpszSection, string lpszEntry, string lpszDefault,
            string retBuffer, int cbRetBuffer, string lpszFilename);

        #region CreateDSN

        public void CreateDsn(string dsnName)
        {
            try
            {
                var connectionString = string.Format("Provider={0}; Data Source={1}; Jet OLEDB:Engine Type={2}", "Microsoft.Jet.OLEDB.4.0", "Packet.mdb", 5);
                var catalog = new Catalog();
                catalog.Create(connectionString);
                // Close the connection to the database after we are done creating it and adding the table to it.
                var con = (Connection)catalog.ActiveConnection;
                if (con != null && con.State != 0)
                    con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException != null ? ex.InnerException.ToString() : ex.Message);
            }
        }

        #endregion CreateDSN

        #region CheckForDSN

        public int CheckForDsn(string dsnName)
        {
            var strRetBuff = "";
            var iData = SQLGetPrivateProfileString("ODBC Data Sources", dsnName, "", strRetBuff, 200, "odbc.ini");
            return iData;
        }

        #endregion CheckForDSN
    }
}