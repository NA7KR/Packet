#region Using Directive

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#endregion

namespace PacketComs
{
    public class ModifyFile
    {
        #region Write

        public bool Write(string textVale)
        {
            try
            {
                string path = Directory.GetCurrentDirectory() + @"\Data";
                if (!Directory.Exists(path))
                {
                    // Try to create the directory.
                    Directory.CreateDirectory(path);
                }
                path = path + @"\myMailList.txt";

                /* if (!File.Exists(path))
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
                     */
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


        public void RX(out int RXMSG, out string RXTSLD, out int RXSIZE, out string RXTO, out string RXROUTE,out string RXFROM, out string RXDATE, out string RXSUBJECT)
        {
            RXMSG = 10;
            RXTSLD = "BF";
            RXSIZE = 100;
            RXTO = "WP";
            RXROUTE = "USA";
            RXFROM = "NA7KR";
            RXDATE = "1014/1014";
            RXSUBJECT = "TEST";
        }




        /*


            #region Read

            public bool Read(string textVale)
            {
                // The 5+5+2 is the assumed lenght of a line
                const int recLength = 12;

                string path = @"d:\temp\DATA1.txt";

                if (File.Exists(path))
                {
                    int recNum = 4;

                    string key;
                    string value;
                    using (BinaryReader br = new BinaryReader(File.Open(path, FileMode.Open)))
                    {
                        // The key point is the Position property that should be set using
                        // some kind of simple math to the exact position needed
                        br.BaseStream.Position = recNum*recLength;

                        // Read the 5 bytes and build the key and value string, 
                        // note that reading (or writing) advances the Position 
                        key = new string(br.ReadChars(5));
                        value = new string(br.ReadChars(5));
                    }
                }
            }
            */


        //end public call
    }
} //end namespace