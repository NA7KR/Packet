using System;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;

namespace Utility.ModifyFiles
{
    public class ModifyFiles
    {
        public bool Write(string KeyName)
        {
            try
            {
                string path = @"C:\temp\myTest.txt";
                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(KeyName);
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show  (e.Message) ;
                return false;
            }
            return true;
        }
    }

}




	