#region Using Directive

using System;
using System.Windows.Forms;

#endregion

namespace Packet
{
	public partial class IpForm : Form
	{
		#region IP_Form2

		private readonly Encrypting _myEncrypt = new Encrypting();
		private readonly ModifyRegistry _myRegistry = new ModifyRegistry();

		#region IP_Form2

		public IpForm(string var1, string var2)
		{
			if (var1 == null) throw new ArgumentNullException(nameof(var1));
			var key = "SOFTWARE\\NA7KR\\Packet\\" + var1;
			InitializeComponent();
			_myRegistry.SubKey = key;
			_myRegistry.ShowError = true;
			_var1 = var1;
			_var2 = var2;
		}

		#endregion

		#region Done_button

		private void Done_button_Click(object sender, EventArgs e)
		{
			if (_var2 == "Telnet")
			{
				_myRegistry.Write("IP", textBox_ip.Text);
				_myRegistry.Write("Port", textBox_port.Text);
				_myRegistry.Write("CallSign", textBox_mycall.Text);
				_myRegistry.Write("Password", _myEncrypt.Encrypt(textBox_password.Text));
				_myRegistry.Write("Echo", echo_comboBox.Text);
				_myRegistry.Write("UserNamePrompt", textBox_username_prompt.Text);
				_myRegistry.Write("Prompt", textBox_prompt.Text);
				_myRegistry.Write("PasswordPrompt", textBox_password_prompt.Text);
				if (_var1 == "BBS")
				{
					_myRegistry.Write("Start Number", Convert.ToInt32(textBox_start.Text));
				}
			}
			else if (_var2 == "SSH")
			{
				_myRegistry.Write("IP", textBox_ip.Text);
				_myRegistry.Write("Port", textBox_port.Text);
				_myRegistry.Write("UserName", textBox_mycall.Text);
				_myRegistry.Write("Active", echo_comboBox.Text);
				_myRegistry.Write("Password", _myEncrypt.Encrypt(textBox_password.Text));
			}
			else
			{
				_myRegistry.Write("CallSign", textBox_mycall.Text);
				_myRegistry.Write("Prompt", textBox_prompt.Text);
				if (_var2 == "BBS")
				{
					_myRegistry.Write("Start Number", Convert.ToInt32(textBox_start.Text));
				}
			}
			Close();
		}

		#endregion

        #region Load
        private void IP_Form2_Load(object sender, EventArgs e)
		{
			#region Telnet

			if (_var2 == "Telnet")
			{
				Text = "Telnet";
                var ip = _myRegistry.Read("IP");
                if (ip == null)
                {
                    _myRegistry.Write("IP", "NA7KR.NA7KR.US");
                }
                textBox_ip.Text = _myRegistry.Read("IP");

                var port = _myRegistry.Read("Port");
                if (port == null)
                {
                    if (_var1 == "BBS")
                    {
                        _myRegistry.Write("Port", "6300");
                    }
                    if (_var1 == "Node")
                    {
                        _myRegistry.Write("Port", "23");
                    }
                    if (_var1 == "Cluster")
                    {
                        _myRegistry.Write("Port", "9000");
                    }
                }
                textBox_port.Text = _myRegistry.Read("Port");
				textBox_mycall.Text = _myRegistry.Read("CallSign");
				textBox_password.Text = _myEncrypt.Decrypt(_myRegistry.Read("Password"));

                var passwordPrompt = _myRegistry.Read("PasswordPrompt");
                if (passwordPrompt == null)
                {
                    _myRegistry.Write("PasswordPrompt", "Password :");
                }   
                textBox_password_prompt.Text = _myRegistry.Read("PasswordPrompt");

                var userNamePrompt = _myRegistry.Read("UserNamePrompt");
                if (userNamePrompt == null)
                {
                    if (_var1 == "BBS")
                    {
                        _myRegistry.Write("UserNamePrompt", "Callsign :");
                    }
                    if (_var1 == "Node")
                    {
                        _myRegistry.Write("UserNamePrompt", "login:");
                    }
                    if (_var1 == "Cluster")
                    {
                        _myRegistry.Write("UserNamePrompt", "login:");
                    }
                }
                textBox_username_prompt.Text = _myRegistry.Read("UserNamePrompt");

                var echo = _myRegistry.Read("Echo");
                if (echo == null)
                {
                    _myRegistry.Write("Echo", "Yes");
                }

                if (_myRegistry.Read("Echo") == "Yes")
				{
					echo_comboBox.SelectedIndex = 0;
				}
				if (_myRegistry.Read("Echo") == "No")
				{
					echo_comboBox.SelectedIndex = 1;
				}
				textBox_ip.TabIndex = 1;
				textBox_port.TabIndex = 2;
				textBox_mycall.TabIndex = 4;
				textBox_password.TabIndex = 5;
				echo_comboBox.TabIndex = 6;
				textBox_prompt.TabIndex = 7;
				textBox_username_prompt.TabIndex = 8;
				textBox_password_prompt.TabIndex = 9;
				label_ip.Text = "IP or Hostname";
				label_port.Text = "Port";
				label_mycall.Text = "Your Callsign";
				label_password.Text = "Password";
				label_echo.Text = "Echo";
				label_prompt.Text = "Prompt";
				label_username_prompt.Text = "UserName Prompt";
				label_password_prompt.Text = "Password Prompt";

				label_ip.Left = 20;
				label_port.Left = 20;
				label_mycall.Left = 20;
				label_password.Left = 20;
				label_echo.Left = 20;
				label_prompt.Left = 20;
				label_username_prompt.Left = 20;
				label_password_prompt.Left = 20;
				textBox_ip.Left = 160;
				textBox_port.Left = 160;
				textBox_mycall.Left = 160;
				textBox_password.Left = 160;
				echo_comboBox.Left = 160;
				textBox_prompt.Left = 160;
				textBox_username_prompt.Left = 160;
				textBox_password_prompt.Left = 160;
				label_ip.Width = 120;
				label_port.Width = 120;
				label_mycall.Width = 120;
				label_password.Width = 120;
				label_echo.Width = 120;
				label_prompt.Width = 120;
				label_username_prompt.Width = 120;
				label_password_prompt.Width = 120;
				label_ip.Top = 20;
				label_port.Top = label_ip.Top + 30;
				label_mycall.Top = label_port.Top + 30;
                label_password.Top = label_mycall.Top + 30;
				label_echo.Top = label_password.Top + 30;
				label_prompt.Top = label_echo.Top + 30;
				label_username_prompt.Top = label_prompt.Top + 30;
				label_password_prompt.Top = label_username_prompt.Top + 30;
				textBox_ip.Top = 20;
				textBox_port.Top = textBox_ip.Top + 30;

				textBox_mycall.Top = textBox_port.Top + 30;
                textBox_password.Top = textBox_mycall.Top + 30;
				echo_comboBox.Top = textBox_password.Top + 30;
				textBox_prompt.Top = echo_comboBox.Top + 30;
				textBox_username_prompt.Top = textBox_prompt.Top + 30;
				textBox_password_prompt.Top = textBox_username_prompt.Top + 30;

				textBox_ip.Width = 140;
				textBox_port.Width = 140;
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

				Width = 350;

				if (_var1 == "BBS")
				{
					textBox_start.Text = _myRegistry.ReadDw("Start Number").ToString();
					textBox_start.TabIndex = 6;
					label_start.Text = "Start Number *";
					label_start.Left = 20;
					label_start.Width = 120;
					label_mycall.Width = 120;
					label_start.Top = label_password_prompt.Top + 30;
					textBox_start.Top = textBox_password_prompt.Top + 30;
					textBox_start.Width = 140;
					textBox_mycall.Width = 140;
					textBox_start.Left = 160;
                    var bbsPrompt = _myRegistry.Read("Prompt");
                    if (bbsPrompt == null)
                    {
                        _myRegistry.Write("Prompt", "BBS>");
                    }
                    
                    textBox_prompt.Text = _myRegistry.Read("Prompt");
                    Done_button.Top = textBox_start.Top + 30;
					Cancel_button.Top = textBox_start.Top + 30;

					Height = Done_button.Top + 80;
				}

				else
				{
					textBox_start.Visible = false;
					label_start.Visible = false;
					Done_button.Top = textBox_password_prompt.Top + 30;
					Cancel_button.Top = textBox_password_prompt.Top + 30;
					Height = Done_button.Top + 80;
				}
			}
				#endregion

				#region SSH

			else if (_var1 == "SSH")
			{
				Text = "SSH";
				if (_myRegistry.Read("Active") == "Yes")
				{
					echo_comboBox.SelectedIndex = 0;
				}
				if (_myRegistry.Read("Active") == "No")
				{
					echo_comboBox.SelectedIndex = 1;
				}
				textBox_ip.Text = _myRegistry.Read("IP");
				textBox_port.Text = _myRegistry.Read("Port");
				textBox_mycall.Text = _myRegistry.Read("UserName");
				textBox_password.Text = _myEncrypt.Decrypt(_myRegistry.Read("Password"));
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
				label_ip.Text = "IP or Hostname";
				label_port.Text = "Port";
				label_mycall.Text = "User Name";
				label_password.Text = "Password";
				label_echo.Text = "Enable";
				label_start.Visible = false;
				label_prompt.Visible = false;
				label_username_prompt.Visible = false;
				label_password_prompt.Visible = false;
				textBox_start.Visible = false;
				textBox_prompt.Visible = false;
				textBox_username_prompt.Visible = false;
				textBox_password_prompt.Visible = false;
				label_echo.Top = 140;
				Done_button.Top = 170;
				Cancel_button.Top = 170;
				Height = 260;
				echo_comboBox.Top = 140;
			}
				#endregion

				#region else com

			else
			{
				Text = "Com Port";
				textBox_ip.Visible = false;
				textBox_port.Visible = false;
				textBox_mycall.Visible = true;
				textBox_password.Visible = false;
				echo_comboBox.Visible = false;
				label_ip.Visible = false;
				label_port.Visible = false;
				label_mycall.Visible = true;
				label_password.Visible = false;
				label_echo.Visible = false;
				textBox_mycall.Text = _myRegistry.Read("CallSign");
				textBox_prompt.Text = _myRegistry.Read("Prompt");
				textBox_mycall.TabIndex = 5;
				textBox_prompt.TabIndex = 7;
				textBox_username_prompt.TabIndex = 8;
				textBox_password_prompt.TabIndex = 9;
				label_mycall.Text = "Your Callsign";
				label_prompt.Text = "Prompt";
				label_mycall.Left = 20;
				label_prompt.Left = 20;
				label_username_prompt.Left = 20;
				label_password_prompt.Left = 20;
				textBox_mycall.Left = 160;
				textBox_prompt.Left = 160;
				label_mycall.Width = 120;
				label_prompt.Width = 120;
				textBox_mycall.Top =  30;
				textBox_prompt.Top = textBox_mycall.Top + 30;
				label_mycall.Top =  30;
				label_prompt.Top = textBox_mycall.Top + 30;
				Done_button.Width = 75;
				Done_button.Left = 60;
				Cancel_button.Width = 75;
				Cancel_button.Left = 195;
				textBox_prompt.Width = 140;
				Width = 350;

				if (_var1 == "BBS")
				{
					textBox_mycall.Text = _myRegistry.Read("CallSign");
					textBox_start.Text = _myRegistry.ReadDw("Start Number").ToString();
					textBox_start.TabIndex = 6;
					label_start.Text = "Start Number *";
					textBox_start.Left = 20;
					label_start.Left = 20;
					label_start.Width = 120;
					textBox_mycall.Width = 140;
					textBox_start.Width = 140;
					textBox_start.Left = 160;
					Done_button.Top = label_start.Top + 30;
					Cancel_button.Top = label_start.Top + 30;
					Height = Done_button.Top + 80;
				}
				else
				{
					textBox_start.Visible = false;
					label_start.Visible = false;
					Done_button.Top = textBox_prompt.Top + 30;
					Cancel_button.Top = textBox_prompt.Top + 30;
					Height = Done_button.Top + 80;
				}
			}

			#endregion

			#endregion
        }

        #endregion

        private void textBox_prompt_Enter(object sender, EventArgs e)
        {
            MessageBox.Show("Must match what BBS sends. Like BBS>");
        }

        private void textBox_username_prompt_Enter(object sender, EventArgs e)
        {
            MessageBox.Show("Must match what BBS sends. Like Callsign:");
        }

        private void textBox_password_prompt_Enter(object sender, EventArgs e)
        {
            MessageBox.Show("Must match what BBS sends. Like Password:");
        }

    
    }
}