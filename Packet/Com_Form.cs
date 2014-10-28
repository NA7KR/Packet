#region Using Directive

using System;
using System.IO.Ports;
using System.Windows.Forms;

#endregion

namespace Packet
{

    #region partial class Com_Form

    public partial class ComForm : Form
    {
        private readonly ModifyRegistry _myRegistry = new ModifyRegistry();

        #region Com_Form()

        public ComForm(Encrypting myEncrypt)
        {
            MyEncrypt = myEncrypt;
            InitializeComponent();
            _myRegistry.SubKey = "SOFTWARE\\NA7KR\\Packet\\Port";
            _myRegistry.ShowError = true;
            radioButton110.Top = top;
            radioButton300.Top = top;
            radioButton600.Top = top;
            radioButton1200.Top = top;
            radioButton2400.Top = top;
            radioButton4800.Top = top*2;
            radioButton9600.Top = top*2;
            radioButton19200.Top = top*2;
            radioButton38400.Top = top*2;
            radioButton57600.Top = top*2;

            groupBoxBaudRate.Height = top*3;
            groupBoxBaudRate.Top = top;
            groupBoxBaudRate.Left = top;
            radioButton9600.Checked = true;


            radioButton_Data_5.Top = top;
            radioButton_Data_6.Top = top;
            radioButton_Data_7.Top = top;
            radioButton_Data_8.Top = top;

            groupBoxDataBits.Height = top*2;
            groupBoxDataBits.Top = top*5;
            groupBoxDataBits.Left = top;
            radioButton_Data_8.Checked = true;

            radioButton_Stop_1.Top = top;
            radioButton_Stop_1_5.Top = top;
            radioButton_Stop_2.Top = top;
            radioButton_Stop_None.Top = top;

            groupBoxStopBits.Height = top*2;
            groupBoxStopBits.Top = top*5;
            radioButton_Stop_1.Checked = true;

            radioButton_Parity_None.Top = top;
            radioButton_Parity_Odd.Top = top*2;
            radioButton_Parity_Even.Top = top*3;
            radioButton_Parity_Mark.Top = top*4;
            radioButton_Parity_Space.Top = top*5;

            groupBoxParity.Height = top*6;
            groupBoxParity.Top = top*8;
            groupBoxParity.Left = top;
            radioButton_Parity_None.Checked = true;

            radioButton_Flow_Xon_Xoff.Top = top;
            radioButton_Flow_RequestToSendXOnXOff.Top = top*2;
            radioButton_Flow_RequestToSend.Top = top*3;
            radioButton_Flow_None.Top = top*4;

            groupBoxFlow.Height = top*5;
            groupBoxFlow.Top = top*8;
            radioButton_Flow_None.Checked = true;

            ok_button.Top = top + 10;
            cancel_button.Top = top + 10 + ok_button.Height + top;

            Port_label.Top = top*8 + 10;
            comboBoxPort.Top = top*8 + 30;
        }

        public ComForm()
        {
            throw new NotImplementedException();
        }

        public Encrypting MyEncrypt
        {
            get { return _myEncrypt; }
            set { _myEncrypt = value; }
        }

        #endregion

        #region button

        private void ok_button_Click(object sender, EventArgs e)
        {
            try
            {
                _myRegistry.Write("Baud", Convert.ToString(Speed));
                _myRegistry.Write("Data Bits", Convert.ToString(DataBits));
                _myRegistry.Write("Stop Bits", StopBits);
                _myRegistry.Write("Parity", Parity);
                _myRegistry.Write("Flow", Flow);
                _myRegistry.Write("Port", port);
                Close();
            }
            catch
            {
                Close();
            }
        }

        #endregion

        #region screen layout

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

        private void radioButton_Data_5_CheckedChanged(object sender, EventArgs e)
        {
            DataBits = 5;
        }

        private void radioButton_Data_6_CheckedChanged(object sender, EventArgs e)
        {
            DataBits = 6;
        }

        private void radioButton_Data_7_CheckedChanged(object sender, EventArgs e)
        {
            DataBits = 7;
        }

        private void radioButton_Data_8_CheckedChanged(object sender, EventArgs e)
        {
            DataBits = 8;
        }

        private void radioButton_Stop_1_CheckedChanged(object sender, EventArgs e)
        {
            StopBits = "One";
        }

        private void radioButton_Stop_1_5_CheckedChanged(object sender, EventArgs e)
        {
            StopBits = "OnePointFive";
        }

        private void radioButton_Stop_2_CheckedChanged(object sender, EventArgs e)
        {
            StopBits = "Two";
        }

        private void radioButton_Parity_None_CheckedChanged(object sender, EventArgs e)
        {
            Parity = "None";
        }

        private void radioButton_Parity_Odd_CheckedChanged(object sender, EventArgs e)
        {
            Parity = "Old";
        }

        private void radioButton_Parity_Even_CheckedChanged(object sender, EventArgs e)
        {
            Parity = "Even";
        }

        private void radioButton_Parity_Mark_CheckedChanged(object sender, EventArgs e)
        {
            Parity = "Mark";
        }

        private void radioButton_Parity_Space_CheckedChanged(object sender, EventArgs e)
        {
            Parity = "Space";
        }

        private void radioButton_Flow_Xon_Xoff_CheckedChanged(object sender, EventArgs e)
        {
            Flow = "XOnXOff";
        }

        private void radioButton_Flow_Hardware_CheckedChanged(object sender, EventArgs e)
        {
            Flow = "RequestToSendXOnXOff";
        }

        private void radioButton_Flow_None_CheckedChanged(object sender, EventArgs e)
        {
            Flow = "None";
        }

        #endregion

        #region load

        private void Com_Form_Load(object sender, EventArgs e)
        {
            #region  case switch baud

            speed = Convert.ToInt32(_myRegistry.Read("Baud"));
            switch (speed)
            {
                case 110:
                {
                    radioButton110.Checked = true;
                    break;
                }
                case 300:
                {
                    radioButton300.Checked = true;
                    break;
                }
                case 600:
                {
                    radioButton600.Checked = true;
                    break;
                }
                case 1200:
                {
                    radioButton1200.Checked = true;
                    break;
                }
                case 2400:
                {
                    radioButton2400.Checked = true;
                    break;
                }
                case 4800:
                {
                    radioButton4800.Checked = true;
                    break;
                }
                case 9600:
                {
                    radioButton9600.Checked = true;
                    break;
                }
                case 19200:
                {
                    radioButton19200.Checked = true;
                    break;
                }
                case 38400:
                {
                    radioButton38400.Checked = true;
                    break;
                }
                case 57600:
                {
                    radioButton57600.Checked = true;
                    break;
                }
            }

            #endregion

            #region  case switch Data Bits

            Data = Convert.ToInt32(_myRegistry.Read("Data Bits"));
            switch (Data)
            {
                case 5:
                {
                    radioButton_Data_5.Checked = true;
                    break;
                }
                case 6:
                {
                    radioButton_Data_6.Checked = true;
                    break;
                }
                case 7:
                {
                    radioButton_Data_7.Checked = true;
                    break;
                }
                case 8:
                {
                    radioButton_Data_8.Checked = true;
                    break;
                }
            }

            #endregion

            #region  case switch Stop Bits

            stop = _myRegistry.Read("Stop Bits");
            switch (stop)
            {
                case "One":
                {
                    radioButton_Stop_1.Checked = true;
                    break;
                }
                case "OnePointFive":
                {
                    radioButton_Stop_1_5.Checked = true;
                    break;
                }
                case "Two":
                {
                    radioButton_Stop_2.Checked = true;
                    break;
                }
                case "None":
                {
                    radioButton_Stop_None.Checked = true;
                    break;
                }
            }

            #endregion

            #region  case switch Parity

            parity = _myRegistry.Read("Parity");
            switch (parity)
            {
                case "None":
                {
                    radioButton_Parity_None.Checked = true;
                    break;
                }
                case "Old":
                {
                    radioButton_Parity_Odd.Checked = true;
                    break;
                }
                case "Even":
                {
                    radioButton_Parity_Even.Checked = true;
                    break;
                }
                case "Mark":
                {
                    radioButton_Parity_Mark.Checked = true;
                    break;
                }
                case "Space":
                {
                    radioButton_Parity_Space.Checked = true;
                    break;
                }
            }

            #endregion

            #region  case switch flow

            flow = _myRegistry.Read("Flow");
            switch (flow)
            {
                case "XOnXOff":
                {
                    radioButton_Flow_Xon_Xoff.Checked = true;
                    break;
                }
                case "RequestToSendXOnXOff":
                {
                    radioButton_Flow_RequestToSendXOnXOff.Checked = true;
                    break;
                }
                case "None":
                {
                    radioButton_Flow_None.Checked = true;
                    break;
                }
            }

            #endregion

            comboBoxPort.Items.Clear();
            foreach (string item in SerialPort.GetPortNames())
            {
                comboBoxPort.Items.Add(item);
            }
            comboBoxPort.SelectedItem = _myRegistry.Read("Port");
            // needs work
        }

        #endregion

        #region combbox changed

        private void comboBoxPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            port = Convert.ToString(comboBoxPort.SelectedItem);
        }

        #endregion
    }

    #endregion
}