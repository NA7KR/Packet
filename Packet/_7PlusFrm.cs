using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;     // DLL support

namespace Packet
{

 

public partial class _7PlusFrm : Form
    {
    [DllImport("7plus.dll")]
    public static extern void Do_7plus(string args);
        public _7PlusFrm()
        {
            InitializeComponent();
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            Do_7plus("c:\\temp\\7plus.zip -SAVE \"c:\\temp\\\"  -SB 5000");

            //    c:\temp\7plus.zip -SAVE "c:\temp\"  -SB 5000          
            //    c:\temp\7plus.p01 - SAVE "c:\temp\"                 
            //    c:\temp\7plus.err -SAVE "c:\temp\"					
            //	  c:\temp\7plus.cor -SAVE "c:\temp\	

        }
    }
  
}
