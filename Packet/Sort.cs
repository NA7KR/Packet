﻿#region Using Directive

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PacketComs;

#endregion

namespace Packet
{
	#region class Sort
    public partial class Sort : Form
	{
		private static readonly FileSql MyFiles = new FileSql();
		private static readonly Sql SQL = new Sql();
		public string file_Name_Select; // "SelectedTo"
        public string file_Name_Sort; // SortTo
		public string s_list_type;
		public Int32 d_size;

		#region Sort
		public Sort(string fileNameSelect, string fileNameSort, Int32 dsize, string sListType)
		{
			InitializeComponent();
			file_Name_Select = fileNameSelect; //local to public
            file_Name_Sort = fileNameSort; //local to public
			s_list_type = sListType;
			d_size = dsize;
		}

		#endregion

        #region OK
        private void button_ok_Click(object sender, EventArgs e)
		{
		MyFiles.SelectMakeTable(s_list_type, d_size, s_list_type, "DSN=Packet", "Packet");
			//string myString = string.Empty;
			foreach (ListViewItem anItem in listView1.CheckedItems)
			{
				//myString += "," + anItem.Text;
			MyFiles.WriteSqlSelect(anItem.Text, s_list_type);
			}

			Close();
		}

		#endregion

        #region Load
        private void Sort_Load(object sender, EventArgs e)
		{
			MyFiles.SelectMakeTable(s_list_type, d_size, s_list_type, "DSN=Packet","Packet");
			listView1.Left = 5;
			listView1.Width = (Width - 30);
			listView1.Top = 5;
			listView1.Height = (Height - 100);
			button_ok.Top = (Height - 75);
			button_Cancel.Top = (Height - 75);

			SQL.SqlselectOptrion("DSN=Packet", s_list_type);
	        if (s_list_type == "MSGTO")
	        {
		        List<DtoListMsgto> select_lists = SQL.SQLSELECT_ON_Lists_Msgto("DSN=Packet");
		        select_lists.ForEach(delegate(DtoListMsgto select_list)
		        {
			        listView1.Items.Add(select_list.get_MSGTO());
			        if (select_list.get_Selected() == "Y")
			        {
					ListViewItem itemYouAreLookingFor = listView1.FindItemWithText(select_list.get_MSGTO());
					if (itemYouAreLookingFor != null)
						{
						itemYouAreLookingFor.Checked = true;
						}
			        }
		        }
			        );	  //end of foreach
	        }
	       

	        /*	 KRR
          
            

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
			 */
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
