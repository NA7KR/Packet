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
            label_ip.Text = Var1 + " IP or Hostname";
            label_port.Text = Var1 + " Port";
            label_bbs.Text = Var1 + " Callsign";
            label_mycall.Text = Var1 + " Your Callsign";
            label_password.Text = Var1 + " Password";
            label_echo.Text = Var1 + " Echo";
            label_ip.Left = 20;
            label_ip.Width = 120;
            label_port.Left = 20;
            label_port.Width = 120;
            label_bbs.Left = 20;
            label_bbs.Width = 120;
            label_mycall.Left = 20;
            label_mycall.Width = 120;
            label_password.Left = 20;
            label_password.Width = 120;
            label_echo.Left = 20;
            label_echo.Width = 120;
            label_ip.Top = 20;
            label_port.Top = 50;
            label_bbs.Top = 80;
            label_mycall.Top = 110;
            label_password.Top = 140;
            label_echo.Top = 170;
            textBox_ip.Top = 20;
            textBox_port.Top = 50;
            textBox_bbs.Top = 80;
            textBox_mycall.Top = 110;
            textBox_password.Top = 140;
            echo_comboBox.Top = 170;
            textBox_ip.Width = 140;
            textBox_port.Width = 140;
            textBox_bbs.Width = 140;
            textBox_mycall.Width = 140;
            textBox_password.Width = 140;
            echo_comboBox.Width = 140;
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
                label_start.Top = 200;
                textBox_start.Top = 200;
                textBox_start.Width = 140;
                Done_button.Top = 230;
                Cancel_button.Top = 230;

                this.Height = 310;
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
                textBox_bbs.Visible = false;
                textBox_start.Visible = false;
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
