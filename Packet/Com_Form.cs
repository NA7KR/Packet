using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility.ModifyRegistry;
using Utility.Encrypting;

namespace Packet
{
    public partial class Com_Form : Form
    {

        ModifyRegistry myRegistry = new ModifyRegistry();
        Encrypting myEncrypt = new Encrypting();
        public System.Int32  Speed;

        public Com_Form()
        {
            InitializeComponent();
            this.myRegistry.SubKey = "SOFTWARE\\NA7KR\\Packet\\Port";
            myRegistry.ShowError = true;
          
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            myRegistry.Write( "Baud", Convert.ToString(Speed));
            myRegistry.Write( "Stop", "2");
        }

       
        private void radioButton110_CheckedChanged(object sender, EventArgs e)
        {
            Speed = 110;
        }

        private void radioButton300_CheckedChanged(object sender, EventArgs e)
        {
            Speed = 300;
        }

        private void radioButton600_CheckedChanged(object sender, EventArgs e)
        {
            Speed = 600;
        }

        private void radioButton1200_CheckedChanged(object sender, EventArgs e)
        {
            Speed = 1200;
        }

        private void radioButton2400_CheckedChanged(object sender, EventArgs e)
        {
            Speed = 2400;
        }

        private void radioButton4800_CheckedChanged(object sender, EventArgs e)
        {
            Speed = 4800;
        }

        private void radioButton9600_CheckedChanged(object sender, EventArgs e)
        {
            Speed = 9600;
        }

        private void radioButton19200_CheckedChanged(object sender, EventArgs e)
        {
            Speed = 19200;
        }

        private void radioButton38400_CheckedChanged(object sender, EventArgs e)
        {
            Speed = 38400;
        }

        private void radioButton57600_CheckedChanged(object sender, EventArgs e)
        {
            Speed = 57600;
        }
        
    }
}
