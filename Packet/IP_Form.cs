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
            myRegistry.Write(Var1 + "-IP", textBox_ip.Text);
            myRegistry.Write(Var1 + "-Port", textBox_port.Text);
            myRegistry.Write(Var1 + "-CallSign", textBox_mycall.Text);
            myRegistry.Write(Var1 + "-BBS", textBox_bbs.Text);
            myRegistry.Write(Var1 + "-Start Number", textBox_start.Text);
            myRegistry.Write(Var1 + "-Password", myEncrypt.Encrypt(textBox_password.Text));
            myRegistry.Write(Var1 + "-Echo", echo_comboBox.Text);
            myRegistry.Write(Var1 + "-UserNamePrompt", textBox_username_prompt);
            myRegistry.Write(Var1 + "-Prompt", textBox_prompt);
            myRegistry.Write(Var1 + "-PasswordPrompt", textBox_password_prompt);
            this.Close();

        }
        #endregion

        //---------------------------------------------------------------------------------------------------------
        //  IP_Form2
        //---------------------------------------------------------------------------------------------------------
        #region IP_Form2
        private void IP_Form2_Load(object sender, EventArgs e)
        {
            textBox_ip.Text = myRegistry.Read(Var1 + "-IP");
            textBox_port.Text = myRegistry.Read(Var1 + "-Port");
            textBox_mycall.Text = myRegistry.Read(Var1 + "-CallSign");
            textBox_bbs.Text = myRegistry.Read(Var1 + "-BBS");
            textBox_password.Text = myRegistry.Read(Var1 + "-Password");
            textBox_prompt.Text = myRegistry.Read(Var1 + "-Prompt");
            textBox_password_prompt.Text = myRegistry.Read(Var1 + "-PasswordPrompt");
            textBox_username_prompt.Text = myRegistry.Read(Var1 + "-UserNamePrompt");
            if (myRegistry.Read(Var1 + "-Echo") == "Yes")
            {
                echo_comboBox.SelectedIndex = 0;
            }
            if (myRegistry.Read(Var1 + "-Echo") == "No")
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

            label_ip.Text = Var1 + " IP or Hostname";
            label_port.Text = Var1 + " Port";
            label_bbs.Text = Var1 + " Callsign";
            label_mycall.Text = Var1 + " Your Callsign";
            label_password.Text = Var1 + " Password";
            label_echo.Text = Var1 + " Echo";
            label_prompt.Text = Var1 + " Prompt";
            label_username_prompt.Text = Var1 + " UserName Prompt";
            label_password_prompt.Text = Var1 + " Password Prompt";

            label_ip.Left = 20;
            label_port.Left = 20;
            label_bbs.Left = 20;
            label_mycall.Left = 20;
            label_password.Left = 20;
            label_echo.Left = 20;
            label_prompt.Left = 20;
            label_username_prompt.Left = 20;
            label_password_prompt.Left = 20;

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
            if (Var1 == "BBS")
            {
                textBox_start.Text = myRegistry.Read(Var1 + "-Start Number");
                textBox_start.TabIndex = 6;
                label_start.Text = Var1 + " Start Number *";
                label_start.Left = 20;
                label_start.Width = 120;
                label_start.Top = label_password_prompt.Top + 30 ;
                textBox_start.Top = textBox_password_prompt.Top + 30;
                textBox_start.Width = 140;
                Done_button.Top = textBox_start.Top + 30;
                Cancel_button.Top = textBox_start.Top + 30;

                this.Height = Done_button.Top + 80;
            }
            else if (Var1 == "SSH")
            {
                textBox_ip.Text = myRegistry.Read(Var1 + "-IP");
                textBox_port.Text = myRegistry.Read(Var1 + "-Port");
                textBox_mycall.Text = myRegistry.Read(Var1 + "-CallSign");
                textBox_password.Text = myRegistry.Read(Var1 + "-Password");
                textBox_password.Top = 110;
                label_password.Top = 110;
                textBox_mycall.Top = 80;
                label_mycall.Top = 80;
                textBox_ip.TabIndex = 1;
                textBox_port.TabIndex = 2;
                textBox_mycall.TabIndex = 3;
                textBox_password.TabIndex = 4;

                label_ip.Text = Var1 + " IP or Hostname";
                label_port.Text = Var1 + " Port";
                label_mycall.Text = Var1 + " User Name";
                label_password.Text = Var1 + " Password";
                label_echo.Text = Var1 + " Enable";

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

                Done_button.Top = 170;
                Cancel_button.Top = 170;
                this.Height = 260;
                echo_comboBox.Top = 140;
                label_echo.Top = 140;

            }
            else
            {
                textBox_start.Visible = false;
                label_start.Visible = false;
                Done_button.Top = 200;
                Cancel_button.Top = 200;
                this.Height = 280;
            }

        }
        #endregion


    }
}
