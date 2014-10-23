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
        string Var2;

        //---------------------------------------------------------------------------------------------------------
        //  IP_Form2
        //---------------------------------------------------------------------------------------------------------
        #region IP_Form2
        public IP_Form(string _var1, string _var2)
        {
            string key = "SOFTWARE\\NA7KR\\Packet\\" + _var1;
            InitializeComponent();
            this.myRegistry.SubKey = key;
            myRegistry.ShowError = true;
            Var1 = _var1;
            Var2 = _var2;
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        //  IP_Form2
        //---------------------------------------------------------------------------------------------------------
        #region IP_Form2
        private void Done_button_Click(object sender, EventArgs e)
        {
            myRegistry.Write( "IP", textBox_ip.Text);
            myRegistry.Write( "Port", textBox_port.Text);
            myRegistry.Write( "CallSign", textBox_mycall.Text);
            myRegistry.Write( "BBS", textBox_bbs.Text);
            myRegistry.Write( "Start Number", textBox_start.Text);
            myRegistry.Write( "Password", myEncrypt.Encrypt(textBox_password.Text));
            myRegistry.Write( "Echo", echo_comboBox.Text);
            myRegistry.Write( "UserNamePrompt", textBox_username_prompt.Text);
            myRegistry.Write( "Prompt", textBox_prompt.Text);
            myRegistry.Write( "PasswordPrompt", textBox_password_prompt.Text);
            this.Close();

        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        //  IP_Form2
        //---------------------------------------------------------------------------------------------------------
        #region IP_Form2
        private void IP_Form2_Load(object sender, EventArgs e)
        {
            if ( Var2 == "Telnet")
            {
                textBox_ip.Text = myRegistry.Read( "IP");
                textBox_port.Text = myRegistry.Read( "Port");
                textBox_mycall.Text = myRegistry.Read( "CallSign");
                textBox_bbs.Text = myRegistry.Read( "BBS");
                textBox_password.Text = myRegistry.Read( "Password");
                textBox_prompt.Text = myRegistry.Read( "Prompt");
                textBox_password_prompt.Text = myRegistry.Read( "PasswordPrompt");
                textBox_username_prompt.Text = myRegistry.Read( "UserNamePrompt");
                if (myRegistry.Read( "Echo") == "Yes")
                {
                    echo_comboBox.SelectedIndex = 0;
                }
                if (myRegistry.Read( "Echo") == "No")
                {
                    echo_comboBox.SelectedIndex = 1;
                }
                textBox_ip.TabIndex = 1;
                textBox_port.TabIndex = 2;
                textBox_bbs.TabIndex = 3;
                textBox_mycall.TabIndex = 4;
                textBox_password.TabIndex = 5;
                echo_comboBox.TabIndex = 6;
                textBox_prompt.TabIndex = 7;
                textBox_username_prompt.TabIndex = 8;
                textBox_password_prompt.TabIndex = 9;

                label_ip.Text =  " IP or Hostname";
                label_port.Text =  " Port";
                label_bbs.Text =  " Callsign";
                label_mycall.Text =  " Your Callsign";
                label_password.Text =  " Password";
                label_echo.Text =  " Echo";
                label_prompt.Text =  " Prompt";
                label_username_prompt.Text =  " UserName Prompt";
                label_password_prompt.Text =  " Password Prompt";

                label_ip.Left = 20;
                label_port.Left = 20;
                label_bbs.Left = 20;
                label_mycall.Left = 20;
                label_password.Left = 20;
                label_echo.Left = 20;
                label_prompt.Left = 20;
                label_username_prompt.Left = 20;
                label_password_prompt.Left = 20;


                textBox_ip.Left = 160;
                textBox_port.Left = 160;
                textBox_bbs.Left = 160;
                textBox_mycall.Left = 160;
                textBox_password.Left = 160;
                echo_comboBox.Left = 160;
                textBox_prompt.Left = 160;
                textBox_username_prompt.Left = 160;
                textBox_password_prompt.Left = 160;

                label_ip.Width = 120;
                label_port.Width = 120;
                label_bbs.Width = 120;
                label_mycall.Width = 120;
                label_password.Width = 120;
                label_echo.Width = 120;
                label_prompt.Width = 120;
                label_username_prompt.Width = 120;
                label_password_prompt.Width = 120;

                label_ip.Top = 20;
                label_port.Top = label_ip.Top + 30;
                label_bbs.Top = label_port.Top + 30;
                label_mycall.Top = label_bbs.Top + 30;
                label_password.Top = label_mycall.Top + 30;
                label_echo.Top = label_password.Top + 30;
                label_prompt.Top = label_echo.Top + 30;
                label_username_prompt.Top = label_prompt.Top + 30;
                label_password_prompt.Top = label_username_prompt.Top + 30;

                textBox_ip.Top = 20;
                textBox_port.Top = textBox_ip.Top + 30;
                textBox_bbs.Top = textBox_port.Top + 30;
                textBox_mycall.Top = textBox_bbs.Top + 30;
                textBox_password.Top = textBox_mycall.Top + 30;
                echo_comboBox.Top = textBox_password.Top + 30;
                textBox_prompt.Top = echo_comboBox.Top + 30;
                textBox_username_prompt.Top = textBox_prompt.Top + 30;
                textBox_password_prompt.Top = textBox_username_prompt.Top + 30;

                textBox_ip.Width = 140;
                textBox_port.Width = 140;
                textBox_bbs.Width = 140;
                textBox_mycall.Width = 140;
                textBox_password.Width = 140;
                echo_comboBox.Width = 140;
                textBox_prompt.Width = 140;
                textBox_username_prompt.Width = 140;
                textBox_password_prompt.Width = 140;

                Done_button.Width = 75;
                Done_button.Left = 60;
                Cancel_button.Width = 75;
                Cancel_button.Left = 195;

                this.Width = 350;

                if (Var1 == "BBS")
                {
                    textBox_start.Text = myRegistry.Read( "Start Number");
                    textBox_start.TabIndex = 6;
                    label_start.Text =  " Start Number *";
                    label_start.Left = 20;
                    label_start.Width = 120;
                    label_start.Top = label_password_prompt.Top + 30;
                    textBox_start.Top = textBox_password_prompt.Top + 30;
                    textBox_start.Width = 140;
                    textBox_start.Left = 160;
                    Done_button.Top = textBox_start.Top + 30;
                    Cancel_button.Top = textBox_start.Top + 30;

                    this.Height = Done_button.Top + 80;
                }
                
                else
                {
                    textBox_start.Visible = false;
                    label_start.Visible = false;
                    Done_button.Top = textBox_password_prompt.Top + 30;
                    Cancel_button.Top = textBox_password_prompt.Top + 30;
                    this.Height = Done_button.Top + 80;
                }

            }
            else if (Var1 == "SSH")
            {
                if (myRegistry.Read("Echo") == "Yes")
                {
                    echo_comboBox.SelectedIndex = 0;
                }
                if (myRegistry.Read("Echo") == "No")
                {
                    echo_comboBox.SelectedIndex = 1;
                }
                textBox_ip.Text = myRegistry.Read("IP");
                textBox_port.Text = myRegistry.Read("Port");
                textBox_mycall.Text = myRegistry.Read("CallSign");
                textBox_password.Text = myRegistry.Read("Password");

                textBox_ip.Width = 120;
                textBox_port.Width = 120;
                textBox_mycall.Width = 120;
                textBox_password.Width = 120;
                echo_comboBox.Width = 120;


                textBox_ip.Left = 160;
                textBox_port.Left = 160;
                textBox_mycall.Left = 160;
                textBox_password.Left = 160;
                echo_comboBox.Left = 160;

                textBox_password.Top = 110;
                label_password.Top = 110;
                textBox_mycall.Top = 80;
                label_mycall.Top = 80;
                textBox_ip.TabIndex = 1;
                textBox_port.TabIndex = 2;
                textBox_mycall.TabIndex = 3;
                textBox_password.TabIndex = 4;

                label_ip.Text = " IP or Hostname";
                label_port.Text = " Port";
                label_mycall.Text = " User Name";
                label_password.Text = " Password";
                label_echo.Text = " Enable";

                label_bbs.Visible = false;
                label_start.Visible = false;
                label_prompt.Visible = false;
                label_username_prompt.Visible = false;
                label_password_prompt.Visible = false;

                textBox_bbs.Visible = false;
                textBox_start.Visible = false;
                textBox_prompt.Visible = false;
                textBox_username_prompt.Visible = false;
                textBox_password_prompt.Visible = false;

                label_echo.Top = 140;
                Done_button.Top = 170;
                Cancel_button.Top = 170;
                this.Height = 260;
                echo_comboBox.Top = 140;
                
            }
            else
            {
                textBox_ip.Visible = false;
                textBox_port.Visible = false;
                textBox_mycall.Visible = false;
                textBox_bbs.Visible = false;
                textBox_password.Visible = false;
                echo_comboBox.Visible = false;

                label_ip.Visible = false;
                label_port.Visible = false;
                label_mycall.Visible = false;
                label_bbs.Visible = false;
                label_password.Visible = false;
                label_echo.Visible = false;

                textBox_prompt.Text = myRegistry.Read( "Prompt");
                textBox_password_prompt.Text = myRegistry.Read( "PasswordPrompt");
                textBox_username_prompt.Text = myRegistry.Read( "UserNamePrompt");
               
                textBox_prompt.TabIndex = 7;
                textBox_username_prompt.TabIndex = 8;
                textBox_password_prompt.TabIndex = 9;

                label_prompt.Text =  " Prompt";
                label_username_prompt.Text =  " UserName Prompt";
                label_password_prompt.Text =  " Password Prompt";
                
                label_prompt.Left = 20;
                label_username_prompt.Left = 20;
                label_password_prompt.Left = 20;

                textBox_prompt.Left = 160;
                textBox_username_prompt.Left = 160;
                textBox_password_prompt.Left = 160;

                label_prompt.Width = 120;
                label_username_prompt.Width = 120;
                label_password_prompt.Width = 120;

                textBox_prompt.Top = 30;
                textBox_username_prompt.Top = textBox_prompt.Top + 30;
                textBox_password_prompt.Top = textBox_username_prompt.Top + 30;

                label_prompt.Top = 30;
                label_username_prompt.Top = textBox_prompt.Top + 30;
                label_password_prompt.Top = textBox_username_prompt.Top + 30;

                Done_button.Width = 75;
                Done_button.Left = 60;
                Cancel_button.Width = 75;
                Cancel_button.Left = 195;

                textBox_prompt.Width = 140;
                textBox_username_prompt.Width = 140;
                textBox_password_prompt.Width = 140;

                this.Width = 350;

                if (Var1 == "BBS")
                {
                    textBox_start.Text = myRegistry.Read( "Start Number");
                    textBox_start.TabIndex = 6;
                    label_start.Text =  " Start Number *";
                    label_start.Left = 20;
                    label_start.Width = 120;
                    label_start.Top = label_password_prompt.Top + 30;
                    textBox_start.Top = textBox_password_prompt.Top + 30;
                    textBox_start.Width = 140;
                    textBox_start.Left = 160;
                    Done_button.Top = textBox_start.Top + 30;
                    Cancel_button.Top = textBox_start.Top + 30;

                    this.Height = Done_button.Top + 80;
                }
                else
                {
                    textBox_start.Visible = false;
                    label_start.Visible = false;
                    Done_button.Top = textBox_password_prompt.Top + 30;
                    Cancel_button.Top = textBox_password_prompt.Top + 30;
                    this.Height = Done_button.Top + 80;
                }
            }
        #endregion
        }
        
    }
}
