#region Using Directive

using System;
using System.Windows.Forms;
using PacketComs;

#endregion

namespace Packet
{
    #region class Sort
    public partial class Sort : Form
    {
        private static readonly FileSql MyFiles = new FileSql();
        public string file_Name_Select; // "SelectedTo"
        public string file_Name_Sort; // SortTo
        public Int32 dsize;

        #region Sort
        public Sort(string fileNameSelect, string fileNameSort, Int32 dSize)
        {   
            InitializeComponent();
            file_Name_Select = fileNameSelect; //local to public
            file_Name_Sort = fileNameSort; //local to public
            dSize = dsize;
        }
        #endregion

        #region OK
        private void button_ok_Click(object sender, EventArgs e)
        {
            string myString = string.Empty;
            foreach (ListViewItem anItem in listView1.CheckedItems)
            {
                 myString += "," + anItem.Text;
            }
            MyFiles.SelectMakeTable(file_Name_Select, dsize, file_Name_Select, "Packet");
            Close();
        }
        #endregion

        #region Load
        private void Sort_Load(object sender, EventArgs e)
        {
            listView1.Left =  5;
            listView1.Width = (Width - 30);
            listView1.Top = 5;
            listView1.Height = (Height - 100);
            button_ok.Top = (Height - 75);
            button_Cancel.Top = (Height - 75);

            string myString = MyFiles.Rxst(file_Name_Sort);
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
            

            string myString2 = MyFiles.Rxst(file_Name_Sort);
            string[] myArray2 = myString2.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in myArray2)
            {
                if (s == " ")
                {
                }
                else
                {
                    ListViewItem itemYouAreLookingFor = listView1.FindItemWithText(s);
                    // Did we find a match?
                    if (itemYouAreLookingFor != null)
                    {
                        itemYouAreLookingFor.Checked = true;
                    }
                }
            }
        }
        #endregion

        #region resize
        private void Sort_Resize(object sender, EventArgs e)
        {
            listView1.Left = 5;
            listView1.Width = (Width - 30);
            listView1.Top = 5;
            listView1.Height = (Height - 100);
            button_ok.Top = (Height - 75);
            button_Cancel.Top = (Height - 75);
        }
        #endregion
    }
    #endregion
}
