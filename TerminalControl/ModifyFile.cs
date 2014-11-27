﻿#region Using Directive

using System;
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

        #region WriteST

        public bool WriteST(string textVale,string fileName)
        {
            
            try
            {
                string path = Directory.GetCurrentDirectory() + @"\Data";
                if (!Directory.Exists(path))
                {
                    // Try to create the directory.
                    Directory.CreateDirectory(path);
                }
                string filePath = path + @"\" + fileName + ".txt";

                File.WriteAllText(filePath, textVale);
                //string path_backup = path + @"\" + FileName + ".bak";
                //File.Delete(path_backup);
                return true;
            } //end try
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        } //end write

        #endregion

        #region RXST
        public string RXST(string fileName)
        {
            string myString = null;
            string path = Directory.GetCurrentDirectory() + @"\Data";
            if (!Directory.Exists(path))
            {
                // Try to create the directory.
                Directory.CreateDirectory(path);
            }
            string filePath = path + @"\" + fileName + ".txt";
            if (File.Exists(filePath))
            {
                StreamReader myFile = new StreamReader(filePath);
                myString = myFile.ReadToEnd();
                myFile.Close();
                //string path_backup = path + @"\" + FileName + ".bak";

                //File.Copy(file_path, path_backup);
                //File.Delete(file_path);
            }
            
            return myString;

        }
        #endregion
   

        #region RX
        public string RX()
        {
            string myString = null;
            string path = Directory.GetCurrentDirectory() + @"\Data";
            if (!Directory.Exists(path))
            {
                // Try to create the directory.
                Directory.CreateDirectory(path);
            }
            path = path + @"\myMailList.txt";

            if (File.Exists(path))
            {
                StreamReader myFile = new StreamReader(path);
                myString = myFile.ReadToEnd();
                
            }
            return myString;
        }
        #endregion
    }
} //end namespace