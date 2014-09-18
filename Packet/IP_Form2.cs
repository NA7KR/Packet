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
         ModifyRegistry myRegistry = new ModifyRegistry();
           

        public IP_Form2()
        {
            InitializeComponent();
            this.myRegistry.SubKey = "SOFTWARE\\NA7KR\\Packet";
            myRegistry.ShowError = true;
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Done_button_Click(object sender, EventArgs e)
        {
            
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

        private void IP_Form2_Load(object sender, EventArgs e)
        {
           ip_textBox.Text = myRegistry.Read("IP");
           port_textBox.Text = myRegistry.Read("Port");
           callSign_textBox.Text = myRegistry.Read("CallSign");
           bbs_textBox.Text = myRegistry.Read("BBS");
           start_textBox.Text = myRegistry.Read("Start Number");
           password_textBox.Text = myRegistry.Read("Password");
        }
    }
}
