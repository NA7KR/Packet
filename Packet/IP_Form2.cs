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
            myRegistry.Write("IP", ip_textBox.Text);
            myRegistry.Write("Port", port_textBox.Text);
            myRegistry.Write("CallSign", callSign_textBox.Text);
            myRegistry.Write("BBS", bbs_textBox.Text);
            myRegistry.Write("Start Number", start_textBox.Text);
            myRegistry.Write("Password", password_textBox.Text);
            this.Close();

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
