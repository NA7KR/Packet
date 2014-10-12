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

namespace Packet
{
    public partial class Com_Form : Form
    {

        ModifyRegistry myRegistry = new ModifyRegistry();
        Encrypting myEncrypt = new Encrypting();
        public System.Int32  Speed;
        public System.Int32 DataBits;
        public System.Double StopBits;
        public string Parity;
        public string Flow;
        public System.Int32 top = 30;

        public Com_Form()
        {
            InitializeComponent();
            this.myRegistry.SubKey = "SOFTWARE\\NA7KR\\Packet\\Port";
            myRegistry.ShowError = true;
            radioButton110.Top = top;
            radioButton300.Top = top;
            radioButton600.Top = top;
            radioButton1200.Top = top;
            radioButton2400.Top = top;
            radioButton4800.Top = top*2;
            radioButton9600.Top = top * 2;
            radioButton19200.Top = top * 2;
            radioButton38400.Top = top * 2;
            radioButton57600.Top = top * 2;

            groupBoxBaudRate.Height = top * 3;
            groupBoxBaudRate.Top = top;
            groupBoxBaudRate.Left = top;


            radioButton_Data_5.Top = top;
            radioButton_Data_6.Top = top;
            radioButton_Data_7.Top = top;
            radioButton_Data_8.Top = top;

            groupBoxDataBits.Height = top * 2;
            groupBoxDataBits.Top = top * 5;
            groupBoxDataBits.Left = top;

            radioButton_Stop_1.Top = top;
            radioButton_Stop_1_5.Top = top;
            radioButton_Stop_2.Top = top;

            groupBoxStopBits.Height = top * 2;
            groupBoxStopBits.Top = top * 5;

            radioButton_Parity_None.Top = top;
            radioButton_Parity_Odd.Top = top * 2;
            radioButton_Parity_Even.Top = top * 3;
            radioButton_Parity_Mark.Top = top * 4;
            radioButton_Parity_Space.Top = top * 5;

            groupBoxParity.Height = top * 6;
            groupBoxParity.Top = top * 8;
            groupBoxParity.Left = top;


            radioButton_Flow_Xon_Xoff.Top = top;
            radioButton_Flow_Hardware.Top = top * 2;
            radioButton_Flow_None.Top = top * 3;

            groupBoxFlow.Height = top * 4;
            groupBoxFlow.Top = top * 8;

            ok_button.Top = top + 10;
            cancel_button.Top = top + 10 + ok_button.Height + top;

            Port_label.Top = top * 8 + 10;
            comboBoxPort.Top = top * 8 + 30;


        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            myRegistry.Write( "Baud", Convert.ToString(Speed));
            myRegistry.Write("Data Bits", Convert.ToString(DataBits));
            myRegistry.Write("Stop Bits", Convert.ToString(StopBits));
            myRegistry.Write("Parity", Parity);
            myRegistry.Write("Flow", Flow);
            this.Close();
        }

       
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
            StopBits = 1;
        }

        private void radioButton_Stop_1_5_CheckedChanged(object sender, EventArgs e)
        {
            StopBits = 1.5;
        }

        private void radioButton_Stop_2_CheckedChanged(object sender, EventArgs e)
        {
            StopBits = 2;

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
            Flow = "Xon Xoff";
        }

        private void radioButton_Flow_Hardware_CheckedChanged(object sender, EventArgs e)
        {
            Flow = "Hardware";
        }

        private void radioButton_Flow_None_CheckedChanged(object sender, EventArgs e)
        {
            Flow = "None";
        }

      
        
    }
}
