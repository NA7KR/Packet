using System;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Linq;

namespace Utility.ModifyFile
{
    public class ModifyFile
    {
        public bool Write(string textVale)
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

                path = path + @"\myMailList.txt";
                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(textVale);
                        return true;
                    }
                }
                else
                {
                    char[] buffer = new char[2048];
                    string tempFile = path + ".tmp";
                    File.Move(path, tempFile);
                    using (StreamReader reader = new StreamReader(tempFile))
                    { 
                        using (StreamWriter writer = new StreamWriter(path, false))
                        {
                            writer.WriteLine(textVale);
                            int totalRead;
                            while ((totalRead = reader.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                writer.Write(buffer, 0, totalRead);
                            }
                            writer.Close();
                            reader.Close();
                        }
                        
                        File.Delete(tempFile);
                    } 
                    return true;
                }

            } //end try
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        } //end write

    } //end public call
    

} //end namespace




	