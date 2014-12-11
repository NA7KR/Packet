#region Using Directive

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
		private static readonly Sql Sql = new Sql();
	
		public string s_list_type;
		public Int32 d_size;

		#region Sort
		public Sort(  Int32 dsize, string sListType)
		{
			InitializeComponent();
			s_list_type = sListType;
			d_size = dsize;
		}

		#endregion

        #region OK
        private void button_ok_Click(object sender, EventArgs e)
		{
		MyFiles.SelectMakeTable(s_list_type, d_size, s_list_type, "DSN=Packet", "Packet");
			foreach (ListViewItem anItem in listView1.CheckedItems)
			{
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

			Sql.SqlselectOptrion("DSN=Packet", s_list_type);
	        if (s_list_type == "MSGTO")
	        {
		        List<DtoListMsgto> selectLists = Sql.SQLSELECT_ON_Lists_Msgto("DSN=Packet");
		        selectLists.ForEach(delegate(DtoListMsgto selectList)
		        {
			        listView1.Items.Add(selectList.get_MSGTO());
			        if (selectList.get_Selected() == "Y")
			        {
					ListViewItem itemYouAreLookingFor = listView1.FindItemWithText(selectList.get_MSGTO());
					if (itemYouAreLookingFor != null)
						{
						itemYouAreLookingFor.Checked = true;
						}
			        }
		        }
			    );	  //end of foreach
	        }

			if (s_list_type == "MSGFrom")
				{
				List<DtoListMSGFrom> selectLists = Sql.SQLSELECT_ON_Lists_MsgFrom("DSN=Packet");
				selectLists.ForEach(delegate(DtoListMSGFrom selectList)
				{
					listView1.Items.Add(selectList.get_MSGFROM());
					if (selectList.get_Selected() == "Y")
						{
						ListViewItem itemYouAreLookingFor = listView1.FindItemWithText(selectList.get_MSGFROM());
						if (itemYouAreLookingFor != null)
							{
							itemYouAreLookingFor.Checked = true;
							}
						}
				}
				);	  //end of foreach
			}

			if (s_list_type == "MSGRoute")
				{
				List<DtoListMsgRoute> selectLists = Sql.SQLSELECT_ON_Lists_MsgRoute("DSN=Packet");
				selectLists.ForEach(delegate(DtoListMsgRoute selectList)
				{
					listView1.Items.Add(selectList.get_MSGROUTE());
					if (selectList.get_Selected() == "Y")
						{
						ListViewItem itemYouAreLookingFor = listView1.FindItemWithText(selectList.get_MSGROUTE());
						if (itemYouAreLookingFor != null)
							{
							itemYouAreLookingFor.Checked = true;
							}
						}
				}
				);	  //end of foreach
				}

			if (s_list_type == "MSGSubject")
				{
				List<DtoListMsgSubject> selectLists = Sql.SQLSELECT_ON_Lists_MsgSubject("DSN=Packet");
				selectLists.ForEach(delegate(DtoListMsgSubject selectList)
				{
					listView1.Items.Add(selectList.get_MSGSubject());
					if (selectList.get_Selected() == "Y")
						{
						ListViewItem itemYouAreLookingFor = listView1.FindItemWithText(selectList.get_MSGSubject());
						if (itemYouAreLookingFor != null)
							{
							itemYouAreLookingFor.Checked = true;
							}
						}
				}
				);	  //end of foreach
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
