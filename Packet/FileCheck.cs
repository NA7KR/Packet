using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Packet
{
    internal class FileCheck
    {
        [DllImport("7plus.dll")]
        public static extern int Do_7plus([MarshalAs(UnmanagedType.LPStr)] string args);






        //    c:\temp\7plus.zip -SAVE "c:\temp\"  -SB 5000          
        //    c:\temp\7plus.p01 - SAVE "c:\temp\"                 
        //    c:\temp\7plus.err -SAVE "c:\temp\"					
        //	  c:\temp\7plus.cor -SAVE "c:\temp\	


        public void CreateFileWatcher(string path)
        {
            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher
            {
                Path = path,
                NotifyFilter =
                    NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName |
                    NotifyFilters.DirectoryName,
                Filter = "*"
            };
            /* Watch for changes in LastAccess and LastWrite times, and   the renaming of files or directories. */


            // Add event handlers.
            watcher.Changed += OnChanged;
            watcher.Created += OnChanged;
            watcher.Renamed += OnRenamed;
            watcher.Renamed += OnChanged;

            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }

        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            
            string ext = Path.GetExtension(e.FullPath);
            string file = Path.GetFileNameWithoutExtension(e.FullPath);
            string path = Path.GetDirectoryName(e.FullPath) + Path.DirectorySeparatorChar;

            string newfile = path + file + ".P01";
            var args = newfile + " -SAVE \"c:\\temp\\out\\\"";
            Do_7plus(args);
            MessageBox.Show("File: " + e.FullPath + " " + e.ChangeType);
        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed.
            MessageBox.Show("File: {0} renamed to {1}" + e.OldFullPath + e.FullPath);

        }
    }
}
