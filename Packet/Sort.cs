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
		private static readonly Sql Sql = new Sql();
		public char d_key;
		public Int32 d_size;
		public string s_list_type;
		public bool loaded = false;
		#region Sort

		public Sort(Int32 dsize, string sListType, char cKey)
		{
			InitializeComponent();
			s_list_type = sListType;
			d_size = dsize;
			d_key = cKey;
		}

		#endregion

		#region OK

		private void button_ok_Click(object sender, EventArgs e)
		{
			
			MyFiles.UpdateSqlto("MSGTO");
			MyFiles.UpdateSqlto("MSGFrom");
			MyFiles.UpdateSqlto("MSGRoute");
			MyFiles.UpdateSqlto("MSGSubject");

			MyFiles.SqlPacketDelete("MSGTO");
			MyFiles.SqlPacketDelete("MSGFrom");
			MyFiles.SqlPacketDelete("MSGRoute");
			MyFiles.SqlPacketDelete("MSGSubject");

			Close();
		}

		#endregion

		#region Load

		private void Sort_Load(object sender, EventArgs e)
		{
			MyFiles.SelectMakeTable(s_list_type, d_size, s_list_type, "Packet");
			listView1.Left = 5;
			listView1.Width = (Width - 30);
			listView1.Top = 5;
			listView1.Height = (Height - 100);
			button_ok.Top = (Height - 75);
			button_Cancel.Top = (Height - 75);

			Sql.SqlselectOptrion( s_list_type);
			if (s_list_type == "MSGTO")
			{
				var selectLists = Sql.SQLSELECT_ON_Lists_Msgto();
				selectLists.ForEach(delegate(DtoListMsgto selectList)
				{
					if (d_key == 'Y')
					{
						listView1.Items.Add(selectList.get_MSGTO());
						if (selectList.get_Selected() == "Y")
						{
							var itemYouAreLookingFor = listView1.FindItemWithText(selectList.get_MSGTO());
							if (itemYouAreLookingFor != null)
							{
								itemYouAreLookingFor.Checked = true;
							}
						}
						 if (selectList.get_Selected() == "D")
						{
							var itemYouAreLookingFor = listView1.FindItemWithText(selectList.get_MSGTO());
							if (itemYouAreLookingFor != null)
							{
								itemYouAreLookingFor.Remove();
							}
						}
						
					}
					if (d_key == 'D')
					{
						listView1.Items.Add(selectList.get_MSGTO());
						if (selectList.get_Selected() == "D")
						{
							var itemYouAreLookingFor = listView1.FindItemWithText(selectList.get_MSGTO());
							if (itemYouAreLookingFor != null)
							{
								itemYouAreLookingFor.Checked = true;
							}
						}
                        if (selectList.get_Selected() == "Y")
                        {
                            var itemYouAreLookingFor = listView1.FindItemWithText(selectList.get_MSGTO());
                            if (itemYouAreLookingFor != null)
                            {
                                itemYouAreLookingFor.Remove();
                            }
                        }
					}
				}
					); //end of foreach
			}

			if (s_list_type == "MSGFrom")
			{
				var selectLists = Sql.SQLSELECT_ON_Lists_MsgFrom();
				selectLists.ForEach(delegate(DtoListMsgFrom selectList)
				{
					if (d_key == 'Y')
					{
						listView1.Items.Add(selectList.get_MSGFROM());
						if (selectList.get_Selected() == "Y")
						{
							var itemYouAreLookingFor = listView1.FindItemWithText(selectList.get_MSGFROM());
							if (itemYouAreLookingFor != null)
							{
								itemYouAreLookingFor.Checked = true;
							}
						}
					
						if (selectList.get_Selected() == "D")
						{
							var itemYouAreLookingFor = listView1.FindItemWithText(selectList.get_MSGFROM());
							if (itemYouAreLookingFor != null)
							{
								itemYouAreLookingFor.Remove();
							}
						}
					
					}
					if (d_key == 'D')
					{
						listView1.Items.Add(selectList.get_MSGFROM());
						if (selectList.get_Selected() == "D")
						{
							var itemYouAreLookingFor = listView1.FindItemWithText(selectList.get_MSGFROM());
							if (itemYouAreLookingFor != null)
							{
								itemYouAreLookingFor.Checked = true;
							}
						}
                        if (selectList.get_Selected() == "Y")
                        {
                            var itemYouAreLookingFor = listView1.FindItemWithText(selectList.get_MSGFROM());
                            if (itemYouAreLookingFor != null)
                            {
                                itemYouAreLookingFor.Remove();
                            }
                        }
					}
				}
					); //end of foreach
			}

			if (s_list_type == "MSGRoute")
			{
				var selectLists = Sql.SQLSELECT_ON_Lists_MsgRoute();
				selectLists.ForEach(delegate(DtoListMsgRoute selectList)
				{
					if (d_key == 'Y')
					{
						listView1.Items.Add(selectList.get_MSGROUTE());
						if (selectList.get_Selected() == "Y")
						{
							var itemYouAreLookingFor = listView1.FindItemWithText(selectList.get_MSGROUTE());
							if (itemYouAreLookingFor != null)
							{
								itemYouAreLookingFor.Checked = true;
							}
						}
					
						if (selectList.get_Selected() == "D")
						{
							var itemYouAreLookingFor = listView1.FindItemWithText(selectList.get_MSGROUTE());
							if (itemYouAreLookingFor != null)
							{
								itemYouAreLookingFor.Remove();
							}
						}
						
					}
					if (d_key == 'D')
					{
						listView1.Items.Add(selectList.get_MSGROUTE());
						if (selectList.get_Selected() == "D")
						{
							var itemYouAreLookingFor = listView1.FindItemWithText(selectList.get_MSGROUTE());
							if (itemYouAreLookingFor != null)
							{
								itemYouAreLookingFor.Checked = true;
							}
						}
                        if (selectList.get_Selected() == "Y")
                        {
                            var itemYouAreLookingFor = listView1.FindItemWithText(selectList.get_MSGROUTE());
                            if (itemYouAreLookingFor != null)
                            {
                                itemYouAreLookingFor.Remove();
                            }
                        }
						
					}
				}
					); //end of foreach
			}

			if (s_list_type == "MSGSubject")
			{
				var selectLists = Sql.SQLSELECT_ON_Lists_MsgSubject();
				selectLists.ForEach(delegate(DtoListMsgSubject selectList)
				{
					if (d_key == 'Y')
					{
						listView1.Items.Add(selectList.get_MSGSubject());
						if (selectList.get_Selected() == "Y")
						{
							var itemYouAreLookingFor = listView1.FindItemWithText(selectList.get_MSGSubject());
							if (itemYouAreLookingFor != null)
							{
								itemYouAreLookingFor.Checked = true;
							}
						}
						
						if (selectList.get_Selected() == "D")
						{
							var itemYouAreLookingFor = listView1.FindItemWithText(selectList.get_MSGSubject());
							if (itemYouAreLookingFor != null)
							{
								itemYouAreLookingFor.Remove();
							}
						}
						
					}
					if (d_key == 'D')
					{
						listView1.Items.Add(selectList.get_MSGSubject());
						if (selectList.get_Selected() == "D")
						{
							var itemYouAreLookingFor = listView1.FindItemWithText(selectList.get_MSGSubject());
							if (itemYouAreLookingFor != null)
							{
								itemYouAreLookingFor.Checked = true;
							}
						}
					}
				}
					); //end of foreach
			}
			loaded = true;
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

		#region listView1_ItemChecked

		private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
			{
			var checkedItems = listView1.CheckedItems;
			if (loaded)
				{
                String ITEMS = null;
				foreach (ListViewItem item in checkedItems)
					{
					ITEMS = item.Text;

					if (e.Item.Checked)
						{
						if (d_key == 'Y')
							{
							if (s_list_type == "MSGTO")
								Sql.WriteSqlmsgtoUpdate(ITEMS, "Y");
							else if (s_list_type == "MSGRoute")
								Sql.WriteSqlmsgRouteUpdate(ITEMS, "Y");
							else if (s_list_type == "MSGFrom")
								Sql.WriteSqlmsgFromUpdate(ITEMS, "Y");
							else if (s_list_type == "MSGSubject")
								Sql.WriteSqlmsgSubjectUpdate(ITEMS, "Y");
							}
						if (d_key == 'D')
							{
							if (s_list_type == "MSGTO")
								Sql.WriteSqlmsgtoUpdate(ITEMS, "D");
							else if (s_list_type == "MSGRoute")
								Sql.WriteSqlmsgRouteUpdate(ITEMS, "D");
							else if (s_list_type == "MSGFrom")
								Sql.WriteSqlmsgFromUpdate(ITEMS, "D");
							else if (s_list_type == "MSGSubject")
								Sql.WriteSqlmsgSubjectUpdate(ITEMS, "D");
							}
						}
					else if (!e.Item.Checked)
						{
						if (s_list_type == "MSGTO")
							Sql.WriteSqlmsgtoUpdate(ITEMS, "N");
						else if (s_list_type == "MSGRoute")
							Sql.WriteSqlmsgRouteUpdate(ITEMS, "N");
						else if (s_list_type == "MSGFrom")
							Sql.WriteSqlmsgFromUpdate(ITEMS, "N");
						else if (s_list_type == "MSGSubject")
							Sql.WriteSqlmsgSubjectUpdate(ITEMS, "N");
						}
					}
				}
			}

		#endregion
	}

	#endregion
}