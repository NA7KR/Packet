﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility.ModifyRegistry;
using Utility.ModifyFile;
using Utility.StringExtension;

namespace Packet
{
    
    public partial class Form1 : Form
    {
        private TelnetConnection tc;
        ModifyRegistry myRegistry = new ModifyRegistry();
        ModifyFile myFiles = new ModifyFile();
        StringExtension myString = new StringExtension();
        bool forward = false;
        string prompt = "";
        
        public Form1()
        {
            InitializeComponent();
            tc = new TelnetConnection("209.237.87.235", 6300);
            myRegistry.SubKey = "SOFTWARE\\NA7KR\\Packet";
            myRegistry.ShowError = true;
            myRegistry.Write("Packet", Application.ProductVersion);
            this.connect_button1.Enabled = true;
            this.forward_button.Enabled = false;
            this.toolStripComboBox1.Items.Clear();
            this.toolStripComboBox1.Items.Add("Telnet");
            this.toolStripComboBox1.Items.Add("Com Port");
            this.richTextBox1.Left = 20;
            this.richTextBox1.Top = 80;
            this.richTextBox1.Height = ((this.Height - 160) / 2);
            this.richTextBox2.Top = (this.richTextBox1.Top + this.richTextBox1.Size.Height + 20);
            this.richTextBox2.Height = this.richTextBox1.Size.Height;
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
            connect_button1.Enabled = false;
            this.forward_button.Enabled = true;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string rd2;
            int i = 1;
            
            // while connected
            while (tc.IsConnected && prompt.Trim() != "Timeout !!")
            {
             string rd = tc.Read();
                if (rd != "") // stop text on screen jump
                { 
                 if (forward == true)
                 {
                     rd2 = rd.Replace("\u0007", "");
                     rd2 = rd2.TrimEnd('\r', '\n');
                     rd2 = rd2.TrimStart('\r', '\n');
                    // if (rd2.IsNumber() == false)
                         Console.WriteLine("123".IsNumber());
                     {
                         myFiles.Write(rd2);
                     }
               
                 }
                this.richTextBox1.Invoke(new MethodInvoker(delegate() { this.richTextBox1.Text += rd ; }));
                i = i + 1;
                string mystring = i.ToString();
                //this.textBox1.Invoke(new MethodInvoker(delegate() { this.textBox1.Text = mystring; })); ;
            }
                System.Threading.Thread.Sleep(300);

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


        private void iPConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IP_Form2 box = new IP_Form2();
            box.ShowDialog();
        }

        private void Form1_Resize_1(object sender, EventArgs e)
        {
            this.richTextBox1.Left = 20;
            this.richTextBox1.Top = 80;
            this.richTextBox1.Height = ((this.Height - 160) / 2);
            this.richTextBox2.Top = (this.richTextBox1.Top + this.richTextBox1.Size.Height + 20);
            this.richTextBox2.Height = this.richTextBox1.Size.Height;
            this.richTextBox2.Left = 20;
            this.richTextBox1.Width = (this.Width - 60);
            this.richTextBox2.Width = (this.Width - 60);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            forward = true;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length; 
            richTextBox1.ScrollToCaret(); 
        }

      

    }
}
