using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

// DLL support

namespace Packet
{
    public partial class _7PlusFrm : Form
    {
        public _7PlusFrm()
        {
            InitializeComponent();
        }

        [DllImport("7plus.dll")]
        public static extern int Do_7plus([MarshalAs(UnmanagedType.LPStr)] string args);

        private void button_ok_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog fbd = new OpenFileDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)

            {
                string fp = (Path.GetFullPath(fbd.FileName));
                var args = fp +" -SAVE \"c:\\temp\\out\\\"";
                Do_7plus(args);
            }


            //    c:\temp\7plus.zip -SAVE "c:\temp\"  -SB 5000          
            //    c:\temp\7plus.p01 - SAVE "c:\temp\"                 
            //    c:\temp\7plus.err -SAVE "c:\temp\"					
            //	  c:\temp\7plus.cor -SAVE "c:\temp\	
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }
    }
}