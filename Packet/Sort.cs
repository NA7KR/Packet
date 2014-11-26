#region Using Directive
using System;
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
            //string myString = listView1.SelectedItems;

            string myString = string.Empty;
            foreach (ListViewItem.Selected anItem in listView1.Items)
            {
                 myString += "," + anItem.Text;
            }


            _myFiles.WriteST(myString, "SelectedTo"); 
        }

        private void Sort_Load(object sender, EventArgs e)
        {
            listView1.Left =  5;
            listView1.Width = (Width - 30);
            listView1.Top = 5;
            listView1.Height = (Height - 100);
            button_ok.Top = (Height - 75);
            button_Cancel.Top = (Height - 75);

            /*
             int selectedItem = Properties.Settings.Default.SelectedItem;
                if (selectedItem != -1)
                {
                   this.listView1.Items[selectedItem].Selected = true;
                }
             */

            
            string myString = _myFiles.RXST("SortTo");
            string[] myArray = myString.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in myArray)
            {
                if (s == " ")
                { }
                else
                {
                    listView1.Items.Add(s);
                } 
            }
          //  _myFiles.RXST("SortTo");
        }

        private void Sort_Resize(object sender, EventArgs e)
        {
            listView1.Left = 5;
            listView1.Width = (Width - 30);
            listView1.Top = 5;
            listView1.Height = (Height - 100);
            button_ok.Top = (Height - 75);
            button_Cancel.Top = (Height - 75);
        }

     
    }
}
