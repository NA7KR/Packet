using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

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


        public void CreateFileWatcher(string path)
        {
            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = path;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = "*.*";
            watcher.Changed += OnChanged;
            watcher.EnableRaisingEvents = true;

        }

        // Define the event handlers.
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
            }
            else
            {
                newfile = path + file + ".P01";
            }

            var args = newfile + " -SAVE \"c:\\temp\\out\\\"";

            Thread.Sleep(5000);
            int rn = Do_7plus(args);
            MessageBox.Show(rn.ToString());



            //MessageBox.Show("File: " + e.FullPath + " " + e.ChangeType);
            /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*
            *+ Possible return codes:                                                 +*
            *++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*

            0 No errors detected.
            1 Write error.
            2 File not found.
            3 7PLUS header not found.
            4 File does not contain expected part.
            5 7PLUS header corrupted.
            6 No filename for extracting defined.
            7 invalid error report / correction / index file.
            8 Max number of parts exceeded.
            9 Bit 8 stripped.
            10 User break in test_file();
            11 Error report generated.
            12 Only one or no error report to join
            13 Error report/cor-file does not refer to the same original file
            14 Couldn't write 7plus.fls
            15 File size of original file and the size reported in err/cor-file not equal
            16 Correction not successful.
            17 No CRC found in err/cor-file.
            18 Time stamp in meta file differs from that in the correction file.
            19 Meta file already exists.
            20 Can't encode files with 0 file length.
            21 Not enough memory available

            *++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
        }

    }
}
