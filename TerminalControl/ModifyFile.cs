#region Using Directive
using System;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Linq;
#endregion
namespace Utility.ModifyFile
{
    public class ModifyFile
    {
        #region Write
        public bool Write(string textVale)
        {
            try
            {
                string path = System.IO.Directory.GetCurrentDirectory() + @"\Data";
                if (!Directory.Exists(path))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path);
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
    } //end public call
} //end namespace