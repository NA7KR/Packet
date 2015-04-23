using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Packet
{
    public partial class Main
    {
        [DllImport("7plus.dll")]
        public static extern int Do_7plus([MarshalAs(UnmanagedType.LPStr)] string args);

        #region CreateFile Watch
        public void CreateFileWatcher(string path)
        {
            // Create a new FileSystemWatcher and set its properties.
            tmrEditNotify.Enabled = true;
            _fmWatcher = new FileSystemWatcher();
            _fmWatcher.Filter = "*.*";
            _fmWatcher.Path = path ;
            _fmWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            _fmWatcher.Changed += OnChanged;
            _fmWatcher.Created += OnChanged;
            _fmWatcher.EnableRaisingEvents = true;
        }
        #endregion

        #region Stop watching files
        public void FilewatchStop()
        {
            _fmWatcher.EnableRaisingEvents = false;
            _fmWatcher.Dispose();
            tmrEditNotify.Enabled = false;
        }
        #endregion

        #region OnChange
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (!_mBDirty)
            {
                m_FullPath = e.FullPath;

                m_Sb.Remove(0, m_Sb.Length);
                m_Sb.Append(e.FullPath);
                m_Sb.Append(" ");
                m_Sb.Append(e.ChangeType);
                m_Sb.Append("    ");
                m_Sb.Append(DateTime.Now);
                _mBDirty = true;
            }
        }
        #endregion

        #region
        private void tmrEditNotify_Tick(object sender, EventArgs e)
        {
            if (_mBDirty)
            {
                toolStripStatusLabel1.Text = (m_Sb.ToString());
                _mBDirty = false;


                // Specify what is done when a file is changed, created, or deleted.
                string newfile;
                string ext = Path.GetExtension(m_FullPath);
                string file = Path.GetFileNameWithoutExtension(m_FullPath);
                string path = Path.GetDirectoryName(m_FullPath) + Path.DirectorySeparatorChar;
                if (ext == ".7pl" || ext == ".7PL")
                {
                    newfile = path + file + ".7pl";
                    string lockfile = Directory.GetCurrentDirectory() + "\\Data\\Lock\\" + file + ".lock";
                    string logfile = Directory.GetCurrentDirectory() + "\\Data\\Log\\" + file + ".LOG";
                    string outpath = Directory.GetCurrentDirectory() + "\\Data\\Out\\";
                    if (!File.Exists(lockfile))
                    {
                        using (File.Create(lockfile))
                        {
                            var args = newfile + " -SAVE " + outpath + " -LOG " + logfile;
                            int rn = Do_7plus(args);
                            Msg(newfile, rn);
                        }
                    }
                    else
                    {
                        File.Delete(lockfile);
                    }
                }
                else if (ext == ".lock" || ext == ".LOG")
                {
                }
                else
                {
                    newfile = path + file + ".P01";
                    string lockfile = Directory.GetCurrentDirectory() + "\\Data\\Lock\\" + file + ".lock";
                    string logfile = Directory.GetCurrentDirectory() + "\\Data\\Log\\" + file + ".LOG";
                    string outpath = Directory.GetCurrentDirectory() + "\\Data\\Out\\";
                    if (!File.Exists(lockfile))
                    {
                        using (File.Create(lockfile))
                        {
                            var args = newfile + " -SAVE " + outpath + " -LOG " + logfile;
                            int rn = Do_7plus(args);
                            Msg(newfile, rn);
                        }
                    }
                    else
                    {
                        File.Delete(lockfile);
                    }
                }
            }
        }
        #endregion  

        #region    msg
        public void Msg(string newfile, int rn) 
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
            
            toolStripStatusLabel1.Text = txt + " " + newfile;

        }
        #endregion
       
    }
}
