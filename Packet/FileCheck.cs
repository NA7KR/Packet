using System.IO;
using System.Windows.Forms;

namespace Packet
{
    class FileCheck
    {
        public void CreateFileWatcher(string path)
        {
            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher
            {
                Path = path,
                NotifyFilter =  NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                Filter = "*"
            };
            /* Watch for changes in LastAccess and LastWrite times, and   the renaming of files or directories. */
            

            // Add event handlers.
            watcher.Changed += OnChanged;
            watcher.Created += OnChanged;
            watcher.Deleted += OnChanged;
            watcher.Renamed += OnRenamed;

            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }

        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            MessageBox.Show("File: " + e.FullPath + " " + e.ChangeType);
        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed.
            MessageBox.Show("File: {0} renamed to {1}" +  e.OldFullPath + e.FullPath);
        }
    }
}
