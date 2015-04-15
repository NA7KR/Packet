using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

// DLL support

namespace Packet
{
    public partial class PlusFrm : Form
    {
        public PlusFrm()
        {
            InitializeComponent();
        }

        [DllImport("7plus.dll")]
        public static extern int Do_7plus([MarshalAs(UnmanagedType.LPStr)] string args);

        private void button_ok_Click(object sender, EventArgs e)
        {
            var fbd = new OpenFileDialog();
            string newfile;
            
                if (fbd.ShowDialog() == DialogResult.OK)
            { 
                string file = Path.GetFileNameWithoutExtension(fbd.FileName);
                string path = Path.GetDirectoryName(fbd.FileName) + Path.DirectorySeparatorChar;
                newfile = path + file ;
                string logfile = newfile + ".LOG";
                string inpath = path + "Out";

                var fp = (Path.GetFullPath(fbd.FileName));
                var args = fp + " -SAVE " + inpath + " -LOG " + logfile;
                Do_7plus(args);
            }

            //    c:\temp\7plus.zip -SAVE "c:\temp\"  -SB 5000          
            //    c:\temp\7plus.p01 - SAVE "c:\temp\"                 
            //    c:\temp\7plus.err -SAVE "c:\temp\"					
            //	  c:\temp\7plus.cor -SAVE "c:\temp\	
        }


    }
}