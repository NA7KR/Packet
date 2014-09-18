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
         string Var1;

         public IP_Form2(string _var1)
        {
            InitializeComponent();
            this.myRegistry.SubKey = "SOFTWARE\\NA7KR\\Packet";
            myRegistry.ShowError = true;
            Var1 = _var1;
        }

        private void Done_button_Click(object sender, EventArgs e)
        {   
            myRegistry.Write(Var1 + "-IP", ip_textBox.Text);
            myRegistry.Write(Var1 + "-Port", port_textBox.Text);
            myRegistry.Write(Var1 + "-CallSign", callSign_textBox.Text);
            myRegistry.Write(Var1 + "-BBS", bbs_textBox.Text);
            myRegistry.Write(Var1 + "-Start Number", start_textBox.Text);
            myRegistry.Write(Var1 + "-Password", password_textBox.Text);
            this.Close();

        }


        private void IP_Form2_Load(object sender, EventArgs e)
        {
            ip_textBox.Text = myRegistry.Read(Var1 + "-IP");
            port_textBox.Text = myRegistry.Read(Var1 + "-Port");
            callSign_textBox.Text = myRegistry.Read(Var1 + "-CallSign");
            
            if (Var1 == "BBS")
            {
               start_textBox.Text = myRegistry.Read(Var1 + "-Start Number");
               start_textBox.TabIndex = 6;
               start_label.Text = Var1 + " Start Number *";
               start_label.Left = 20;
               start_label.Width = 120;
               start_label.Top = 170;
               start_textBox.Top = 170;
               start_textBox.Width = 140;
               Done_button.Top = 200;
               Cancel_button.Top = 200;
               Done_button.Width = 75;
               Done_button.Left = 60;
               Cancel_button.Width = 75;
               Cancel_button.Left = 195;
               this.Height = 280;
            }
           
            else
            {
                start_textBox.Visible = false;
                start_label.Visible = false;
                Done_button.Top = 170;
                Done_button.Width = 75;
                Done_button.Left = 60;
                Cancel_button.Width = 75;
                Cancel_button.Left = 195;
                Cancel_button.Top = 170;
                this.Height = 250;
            }
            
            bbs_textBox.Text = myRegistry.Read(Var1 + "-BBS");
            
            password_textBox.Text = myRegistry.Read(Var1 + "-Password");

            ip_textBox.TabIndex = 1;
            port_textBox.TabIndex = 2;
            bbs_textBox.TabIndex = 3;
            callSign_textBox.TabIndex = 4;
            password_textBox.TabIndex = 5;

            IP_label.Text = Var1 + " IP or Hostname";
            port_label.Text = Var1 + " Port";
            bbs_label.Text = Var1 + " Callsign";
            
            mycall_label.Text = Var1 + " Callsign";
            password_label.Text = Var1 + " Password";

            IP_label.Left = 20;
            IP_label.Width = 120;
            port_label.Left = 20;
            port_label.Width = 120;
            bbs_label.Left = 20;
            bbs_label.Width = 120;
            mycall_label.Left = 20;
            mycall_label.Width = 120;
            password_label.Left = 20;
            password_label.Width = 120;

            IP_label.Top = 20;
            port_label.Top = 50;
            bbs_label.Top = 80;
            mycall_label.Top = 110;
            password_label.Top = 140;

            ip_textBox.Top = 20;
            port_textBox.Top = 50;
            bbs_textBox.Top = 80;
            callSign_textBox.Top = 110;
            password_textBox.Top = 140;

            ip_textBox.Width = 140;
            port_textBox.Width = 140;
            bbs_textBox.Width = 140;
            callSign_textBox.Width = 140;
            password_textBox.Width = 140;
        }

     


    }
}
