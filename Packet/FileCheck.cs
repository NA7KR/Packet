using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using PacketComs;
using SafeControls;

namespace Packet
{
    public partial class Main

    {
        [DllImport("7plus.dll")]
        public static extern int Do_7plus([MarshalAs(UnmanagedType.LPStr)] string args);

        //    c:\temp\7plus.zip -SAVE "c:\temp\"  -SB 5000          
        //    c:\temp\7plus.p01 - SAVE "c:\temp\"                 
        //    c:\temp\7plus.err -SAVE "c:\temp\"					
        //	  c:\temp\7plus.cor -SAVE "c:\temp\	

        #region CreateFile Watch
        public void CreateFileWatcher(string path)
        {
            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher
            {
                Path = path,
                NotifyFilter = NotifyFilters.LastWrite,
                Filter = "*.*"
            };
            watcher.Changed += OnChanged;
            watcher.EnableRaisingEvents = true;

        }
        #endregion

        #region OnChange
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            string newfile;
            string ext = Path.GetExtension(e.FullPath);
            string file = Path.GetFileNameWithoutExtension(e.FullPath);
            string path = Path.GetDirectoryName(e.FullPath) + Path.DirectorySeparatorChar;
            if (ext == ".7pl")
            {
                newfile = path + file + ".7pl";
                string lockfile = newfile + ".lock";
                string logfile = newfile + ".LOG";
                string outpath = path + "Out";
                if (!File.Exists(lockfile))
                {
                    using (File.Create(lockfile))
                    {
                        var args = newfile + " -SAVE " + outpath + " -LOG " + logfile;
                        Run7Plus(args);
                    }
                }
                else
                {
                    File.Delete(lockfile);
                }
            }
            else
            {
                newfile = path + file + ".P01";
                string lockfile = newfile + ".lock";
                string logfile = newfile + ".LOG";
                string outpath = path + "Out";
                if (!File.Exists(lockfile))
                {
                    using (File.Create(lockfile))
                    {
                        var args = newfile + " -SAVE " + outpath + " -LOG " + logfile;
                        Run7Plus(args);
                    }
                }
                else
                {
                    File.Delete(lockfile);
                }
            }

        }
        #endregion OnChange 

        #region  Run 7plus
        public static void Run7Plus(string newfile)
        {
            var args = newfile + " -SAVE \"c:\\temp\\out\\\"";
            Thread.Sleep(5000);
            int rn = Do_7plus(args);
            Msg(newfile, rn);

        }
        #endregion

        #region   is File locked
        protected virtual bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }
        #endregion

        #region    msg
        public static  void Msg(string newfile, int rn)
        {
            string txt;
            switch (rn)
            {
                case 0:
                    {
                        txt = "No errors detected.";
                        break;
                    }
                case 1:
                    {
                        txt = "Write error.";
                        break;
                    }
                case 2:
                    {
                        txt = "File not found.";
                        break;
                    }
                case 3:
                    {
                        txt = "7PLUS header not found.";
                        break;
                    }
                case 4:
                    {
                        txt = "File does not contain expected part.";
                        break;
                    }
                case 5:
                    {
                        txt = "7PLUS header corrupted.";
                        break;
                    }
                case 6:
                    {
                        txt = "No filename for extracting defined.";
                        break;
                    }
                case 7:
                    {
                        txt = "invalid error report / correction / index file.";
                        break;
                    }
                case 8:
                    {
                        txt = "Max number of parts exceeded.";
                        break;
                    }
                case 9:
                    {
                        txt = "Bit 8 stripped.";
                        break;
                    }
                case 10:
                    {
                        txt = "User break in test_file();";
                        break;
                    }
                case 11:
                    {
                        txt = "Error report generated.";
                        break;
                    }
                case 12:
                    {
                        txt = "Only one or no error report to join.";
                        break;
                    }
                case 13:
                    {
                        txt = "Error report/cor-file does not refer to the same original file.";
                        break;
                    }
                case 14:
                    {
                        txt = "Couldn't write 7plus.fls.";
                        break;
                    }
                case 15:
                    {
                        txt = "File size of original file and the size reported in err/cor-file not equal.";
                        break;
                    }
                case 16:
                    {
                        txt = "Correction not successful.";
                        break;
                    }
                case 17:
                    {
                        txt = "No CRC found in err/cor-file.";
                        break;
                    }
                case 18:
                    {
                        txt = "Time stamp in meta file differs from that in the correction file.";
                        break;
                    }

                case 19:
                    {
                        txt = "Meta file already exists.";
                        break;
                    }
                case 20:
                    {
                        txt = "Can't encode files with 0 file length.";
                        break;
                    }
                case 21:
                    {
                        txt = " Not enough memory available.";
                        break;
                    }


                default:
                    {
                        txt = "?";
                        break;
                    }

            }

            this.toolStripStatusLabel.Text = txt + " " + newfile;

            MessageBox.Show(txt);
            

        }
        #endregion

    }
}
