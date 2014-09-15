using System;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;

namespace Utility.ModifyFiles
{
    public class ModifyFiles
    {
        public string Write(string KeyName)
        {
            try
            {
                string path = @"C:\temp\myTest.txt";
                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine("Hello");
                        return KeyName = "Hello";
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message ;
            }
            return KeyName = "Hello";
        }
    }

}




	