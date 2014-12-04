using System;
using System.Runtime.InteropServices;

namespace PacketComs
{
    public class ODBC_Manager
    {
        [DllImport("ODBCCP32.dll")]
        public static extern bool SQLConfigDataSource(IntPtr parent, int request, string driver, string attributes);

        [DllImport("ODBCCP32.dll")]
        public static extern int SQLGetPrivateProfileString(string lpszSection, string lpszEntry, string lpszDefault, string @RetBuffer, int cbRetBuffer, string lpszFilename);

        //private const short ODBC_ADD_DSN = 1;
        //private const short ODBC_CONFIG_DSN = 2;
        //private const short ODBC_REMOVE_DSN = 3;
        //private const short ODBC_ADD_SYS_DSN = 4;
        //private const short ODBC_CONFIG_SYS_DSN = 5;
        //private const short ODBC_REMOVE_SYS_DSN = 6;
        //private const int vbAPINull = 0;

        #region CreateDSN
        public void CreateDSN(string strDSNName)
        {
            try
            {
               // to come soon...
           
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.ToString());
                }
                else
                {
                    Console.WriteLine(ex.Message.ToString());
                }
            }
        }
        #endregion

        #region CheckForDSN
        public int CheckForDSN(string strDSNName)
        {
            int iData;
            string strRetBuff = "";
            iData = SQLGetPrivateProfileString("ODBC Data Sources", strDSNName, "", strRetBuff, 200, "odbc.ini");
            return iData;
        }
        #endregion
    }

}
