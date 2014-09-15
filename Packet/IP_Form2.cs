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
namespace Packet
{
    public partial class IP_Form2 : Form
    {
        public IP_Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Done_button_Click(object sender, EventArgs e)
        {
            ModifyRegistry myRegistry = new ModifyRegistry();
            myRegistry.SubKey = "SOFTWARE\\NA7KR\\Packet";
            myRegistry.ShowError = true;
            myRegistry.Write("IP", IP_textBox.Text);
            myRegistry.Write("Port", Port_textBox.Text);
            myRegistry.Write("CallSign", CallSign_textBox.Text);
            this.Close();

        }
    }
}
