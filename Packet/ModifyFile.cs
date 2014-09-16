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
                string path = System.IO.Directory.GetCurrentDirectory() + @"\Data";
                if (!Directory.Exists(path))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    //Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));
                }
                
                    path = path + @"\myText.txt";
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




	