using System;
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
            var args = "c:\\temp\\7plus.zip -SAVE \"c:\\temp\\\"";
            Do_7plus(args);

            //    c:\temp\7plus.zip -SAVE "c:\temp\"  -SB 5000          
            //    c:\temp\7plus.p01 - SAVE "c:\temp\"                 
            //    c:\temp\7plus.err -SAVE "c:\temp\"					
            //	  c:\temp\7plus.cor -SAVE "c:\temp\	
        }
    }
}