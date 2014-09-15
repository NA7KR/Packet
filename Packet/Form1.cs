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
    
    public partial class Form1 : Form
    {
        private TelnetConnection tc;
        ModifyRegistry myRegistry = new ModifyRegistry();
    
        string prompt = "";
        public Form1()
        {
            InitializeComponent();
            tc = new TelnetConnection("209.237.87.235", 6300);
            myRegistry.SubKey = "SOFTWARE\\NA7KR\\Packet";
            myRegistry.ShowError = true;
            myRegistry.Write("Packet", Application.ProductVersion);
            this.toolStripComboBox1.Items.Clear();
            this.toolStripComboBox1.Items.Add("Telnet");
            this.toolStripComboBox1.Items.Add("Com Port");
            this.richTextBox1.Left = 20;
            this.richTextBox2.Left = 20;
            this.richTextBox1.Width = (this.Width - 60);
            this.richTextBox2.Width = (this.Width - 60);
            if (myRegistry.Read("Mode") == "Telnet")
            {
                this.toolStripComboBox1.SelectedIndex = 0;
            }
            if (myRegistry.Read("Mode") == "Com")
            {
                this.toolStripComboBox1.SelectedIndex = 1;
            }
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 box = new AboutBox1();
            box.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tc = new TelnetConnection("209.237.87.235", 6300);
            backgroundWorker1.RunWorkerAsync();
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            this.textBox1.Invoke(new MethodInvoker(delegate() { this.textBox1.Text = "Hello"; }));
            int i = 1;
            // while connected
            while (tc.IsConnected && prompt.Trim() != "exit")
            {
                // display server output
                //richTextBox1.Text += tc.Read();
                // display server output
                this.richTextBox1.Invoke(new MethodInvoker(delegate() { this.richTextBox1.Text += tc.Read(); }));
                i = i + 1;
                string mystring = i.ToString();
                this.textBox1.Invoke(new MethodInvoker(delegate() { this.textBox1.Text = mystring; })); ;
                System.Threading.Thread.Sleep(3000);

            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------
        // ritch Text to send
        //-----------------------------------------------------------------------------------------------------------------------------------------------   
        private void richTextBox2_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                // send client input to server
                prompt = richTextBox2.Text + "\r\n";
                tc.WriteLine(prompt);
                this.textBox1.Invoke(new MethodInvoker(delegate() { this.textBox1.Text = "Enter Pressed"; }));
                richTextBox2.Text = "";

            }
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if  (toolStripComboBox1.SelectedIndex == 0)
            {
            textBox1.Text = "Telnet";
            myRegistry.Write("Mode", "Telnet");
            }
             if  (toolStripComboBox1.SelectedIndex == 1)
            {
            textBox1.Text = "Com Port";
            myRegistry.Write("Mode", "Com");
            }

         }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.richTextBox1.Left = 20;
            this.richTextBox2.Left = 20;
            this.richTextBox1.Width = (this.Width - 60);
            this.richTextBox2.Width = (this.Width - 60);
        }

        private void iPConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 box = new Form2();
            box.ShowDialog();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
