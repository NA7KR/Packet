#region Using Directive
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using PacketComs;
#endregion

namespace Packet
{
    public partial class Sort : Form
    {
        private static readonly ModifyFile _myFiles = new ModifyFile();
        public Sort()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

       

        private void button_ok_Click(object sender, EventArgs e)
        {
           
           
        }

        private void Sort_Load(object sender, EventArgs e)
        {
            string myString = _myFiles.RXST("SortTo");
            string[] myArray = myString.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in myArray)
            {
                listBox1.Items.Add(s);
            }
          //  _myFiles.RXST("SortTo");
        }
    }
}
