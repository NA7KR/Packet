#region Using Directive

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

        public bool WriteST(string textVale,string FileName)
        {
            
            try
            {
                string path = Directory.GetCurrentDirectory() + @"\Data";
                if (!Directory.Exists(path))
                {
                    // Try to create the directory.
                    Directory.CreateDirectory(path);
                }
                path = path + @"\" + FileName + ".txt";

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

        #region RXST
        public string RXST(string FileName)
        {
            string myString = null;
            string path = Directory.GetCurrentDirectory() + @"\Data";
            if (!Directory.Exists(path))
            {
                // Try to create the directory.
                Directory.CreateDirectory(path);
            }
            path = path + @"\" + FileName + ".txt";
            if (File.Exists(path))
            {
                StreamReader myFile = new System.IO.StreamReader(path);
                myString = myFile.ReadToEnd();
                
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
                StreamReader myFile = new System.IO.StreamReader(path);
                myString = myFile.ReadToEnd();
                
            }
            return myString;
        }
        #endregion
    }
} //end namespace