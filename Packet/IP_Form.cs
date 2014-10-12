#region Using Directive
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
#endregion

namespace Packet
{
    public partial class IP_Form : Form
    {
         ModifyRegistry myRegistry = new ModifyRegistry();
         Encrypting myEncrypt = new Encrypting();
         string Var1;

        //---------------------------------------------------------------------------------------------------------
        //  IP_Form2
        //---------------------------------------------------------------------------------------------------------
        #region IP_Form2
        public IP_Form(string _var1)
            {
                InitializeComponent();
                this.myRegistry.SubKey = "SOFTWARE\\NA7KR\\Packet";
                myRegistry.ShowError = true;
                Var1 = _var1;
            }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        //  IP_Form2
        //---------------------------------------------------------------------------------------------------------
        #region IP_Form2
        private void Done_button_Click(object sender, EventArgs e)
        {   
            myRegistry.Write(Var1 + "-IP", ip_textBox.Text);
            myRegistry.Write(Var1 + "-Port", port_textBox.Text);
            myRegistry.Write(Var1 + "-CallSign", mycall_textBox.Text);
            myRegistry.Write(Var1 + "-BBS", bbs_textBox.Text);
            myRegistry.Write(Var1 + "-Start Number", start_textBox.Text);
            myRegistry.Write(Var1 + "-Password", myEncrypt.Encrypt(password_textBox.Text));
            myRegistry.Write(Var1 + "-Echo", echo_comboBox.Text);
            this.Close();
            
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        //  IP_Form2
        //---------------------------------------------------------------------------------------------------------
        #region IP_Form2
        private void IP_Form2_Load(object sender, EventArgs e)
        {
            ip_textBox.Text = myRegistry.Read(Var1 + "-IP");
            port_textBox.Text = myRegistry.Read(Var1 + "-Port");
            mycall_textBox.Text = myRegistry.Read(Var1 + "-CallSign");
            bbs_textBox.Text = myRegistry.Read(Var1 + "-BBS");
            password_textBox.Text = myRegistry.Read(Var1 + "-Password");
            if (myRegistry.Read(Var1 + "-Echo") == "Yes")
            {
                echo_comboBox.SelectedIndex = 0;
            }
            if (myRegistry.Read(Var1 + "-Echo") == "No")
            {
                echo_comboBox.SelectedIndex = 1;
            }
            ip_textBox.TabIndex = 1;
            port_textBox.TabIndex = 2;
            bbs_textBox.TabIndex = 3;
            mycall_textBox.TabIndex = 4;
            password_textBox.TabIndex = 5;
            echo_comboBox.TabIndex = 6;
            ip_label.Text = Var1 + " IP or Hostname";
            port_label.Text = Var1 + " Port";
            bbs_label.Text = Var1 + " Callsign";
            mycall_label.Text = Var1 + " Your Callsign";
            password_label.Text = Var1 + " Password";
            echo_label.Text = Var1 + " Echo";
            ip_label.Left = 20;
            ip_label.Width = 120;
            port_label.Left = 20;
            port_label.Width = 120;
            bbs_label.Left = 20;
            bbs_label.Width = 120;
            mycall_label.Left = 20;
            mycall_label.Width = 120;
            password_label.Left = 20;
            password_label.Width = 120;
            echo_label.Left = 20;
            echo_label.Width =120;
            ip_label.Top = 20;
            port_label.Top = 50;
            bbs_label.Top = 80;
            mycall_label.Top = 110;
            password_label.Top = 140;
            echo_label.Top = 170;
            ip_textBox.Top = 20;
            port_textBox.Top = 50;
            bbs_textBox.Top = 80;
            mycall_textBox.Top = 110;
            password_textBox.Top = 140;
            echo_comboBox.Top = 170;
            ip_textBox.Width = 140;
            port_textBox.Width = 140;
            bbs_textBox.Width = 140;
            mycall_textBox.Width = 140;
            password_textBox.Width = 140;
            echo_comboBox.Width=140;
            Done_button.Width = 75;
            Done_button.Left = 60;
            Cancel_button.Width = 75;
            Cancel_button.Left = 195;
            if (Var1 == "BBS")
            {
               start_textBox.Text = myRegistry.Read(Var1 + "-Start Number");
               start_textBox.TabIndex = 6;
               start_label.Text = Var1 + " Start Number *";
               start_label.Left = 20;
               start_label.Width = 120;
               start_label.Top = 200;
               start_textBox.Top = 200;
               start_textBox.Width = 140;
               Done_button.Top = 230;
               Cancel_button.Top = 230;
             
               this.Height = 310;
            }  
            else if (Var1 == "SSH")
            {
                ip_textBox.Text = myRegistry.Read(Var1 + "-IP");
                port_textBox.Text = myRegistry.Read(Var1 + "-Port");
                mycall_textBox.Text = myRegistry.Read(Var1 + "-CallSign");
                password_textBox.Text = myRegistry.Read(Var1 + "-Password");
                password_textBox.Top = 110;
                password_label.Top = 110;
                mycall_textBox.Top = 80;
                mycall_label.Top = 80;
                ip_textBox.TabIndex = 1;
                port_textBox.TabIndex = 2;
                mycall_textBox.TabIndex = 3;
                password_textBox.TabIndex = 4;

                ip_label.Text = Var1 + " IP or Hostname";
                port_label.Text = Var1 + " Port";
                mycall_label.Text = Var1 + " User Name";
                password_label.Text = Var1 + " Password";
                echo_label.Text = Var1 + " Enable";

                bbs_label.Visible = false;
                start_label.Visible = false;
                bbs_textBox.Visible = false;
                start_textBox.Visible = false;
                Done_button.Top = 170;
                Cancel_button.Top = 170;
                this.Height = 260;
                echo_comboBox.Top = 140;
                echo_label.Top = 140;
                
            }
            else
            {
                start_textBox.Visible = false;
                start_label.Visible = false;
                Done_button.Top = 200;
                Cancel_button.Top = 200;
                this.Height = 280;
            }     
          
        }
        #endregion

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
