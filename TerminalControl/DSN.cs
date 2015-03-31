using System;
using System.Runtime.InteropServices;

namespace PacketComs
{
	public class OdbcManager
	{
		[DllImport("ODBCCP32.dll")]
		public static extern bool SQLConfigDataSource(IntPtr parent, int request, string driver, string attributes);

		[DllImport("ODBCCP32.dll")]
		public static extern int SQLGetPrivateProfileString(string lpszSection, string lpszEntry, string lpszDefault,
			string retBuffer, int cbRetBuffer, string lpszFilename);

		//private const short ODBC_ADD_DSN = 1;
		//private const short ODBC_CONFIG_DSN = 2;
		//private const short ODBC_REMOVE_DSN = 3;
		//private const short ODBC_ADD_SYS_DSN = 4;
		//private const short ODBC_CONFIG_SYS_DSN = 5;
		//private const short ODBC_REMOVE_SYS_DSN = 6;
		//private const int vbAPINull = 0;

		#region CreateDSN

		public void CreateDsn(string dsnName)
		{
			try
			{
                string connectionString = string.Format("Provider={0}; Data Source={1}; Jet OLEDB:Engine Type={2}",
        "Microsoft.Jet.OLEDB.4.0",
        "Packet.mdb",
        5);

                ADOX.CatalogClass catalog = new ADOX.CatalogClass();
                catalog.Create(connectionString);

         
                // Close the connection to the database after we are done creating it and adding the table to it.
                ADODB.Connection con = (ADODB.Connection)catalog.ActiveConnection;
                if (con != null && con.State != 0)
                    con.Close();

            }
            catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					Console.WriteLine(ex.InnerException.ToString());
				}
				else
				{
					Console.WriteLine(ex.Message);
				}
			}
		}

		#endregion

		#region CheckForDSN

		public int CheckForDsn(string dsnName)
		{
			int iData;
			var strRetBuff = "";
			iData = SQLGetPrivateProfileString("ODBC Data Sources", dsnName, "", strRetBuff, 200, "odbc.ini");
			return iData;
		}

		#endregion
	}
}