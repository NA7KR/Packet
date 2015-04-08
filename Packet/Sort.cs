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
        public char DKey;
        public int DSize;
        public bool Loaded;
        public string SListType;

        #region Sort

        public Sort(int dsize, string sListType, char cKey)
        {
            InitializeComponent();
            SListType = sListType;
            DSize = dsize;
            DKey = cKey;
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
            MyFiles.SelectMakeTable(SListType, DSize, SListType, "Packet");
            listView1.Left = 5;
            listView1.Width = (Width - 30);
            listView1.Top = 5;
            listView1.Height = (Height - 100);
            button_ok.Top = (Height - 75);
            button_Cancel.Top = (Height - 75);

            Sql.SqlselectOptrion(SListType);
            if (SListType == "MSGTO")
            {
                var selectLists = Sql.SQLSELECT_ON_Lists_Msgto();
                selectLists.ForEach(delegate(DtoListMsgto selectList)
                {
                    if (DKey == 'Y')
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
                    if (DKey == 'D')
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

            if (SListType == "MSGFrom")
            {
                var selectLists = Sql.SQLSELECT_ON_Lists_MsgFrom();
                selectLists.ForEach(delegate(DtoListMsgFrom selectList)
                {
                    if (DKey == 'Y')
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
                    if (DKey == 'D')
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

            if (SListType == "MSGRoute")
            {
                var selectLists = Sql.SQLSELECT_ON_Lists_MsgRoute();
                selectLists.ForEach(delegate(DtoListMsgRoute selectList)
                {
                    if (DKey == 'Y')
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
                    if (DKey == 'D')
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

            if (SListType == "MSGSubject")
            {
                var selectLists = Sql.SQLSELECT_ON_Lists_MsgSubject();
                selectLists.ForEach(delegate(DtoListMsgSubject selectList)
                {
                    if (DKey == 'Y')
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
                    if (DKey == 'D')
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
            Loaded = true;
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
            if (Loaded)
            {
                foreach (ListViewItem item in checkedItems)
                {
                    var items = item.Text;

                    if (e.Item.Checked)
                    {
                        if (DKey == 'Y')
                        {
                            if (SListType == "MSGTO")
                                Sql.WriteSqlmsgtoUpdate(items, "Y");
                            else if (SListType == "MSGRoute")
                                Sql.WriteSqlmsgRouteUpdate(items, "Y");
                            else if (SListType == "MSGFrom")
                                Sql.WriteSqlmsgFromUpdate(items, "Y");
                            else if (SListType == "MSGSubject")
                                Sql.WriteSqlmsgSubjectUpdate(items, "Y");
                        }
                        if (DKey == 'D')
                        {
                            if (SListType == "MSGTO")
                                Sql.WriteSqlmsgtoUpdate(items, "D");
                            else if (SListType == "MSGRoute")
                                Sql.WriteSqlmsgRouteUpdate(items, "D");
                            else if (SListType == "MSGFrom")
                                Sql.WriteSqlmsgFromUpdate(items, "D");
                            else if (SListType == "MSGSubject")
                                Sql.WriteSqlmsgSubjectUpdate(items, "D");
                        }
                    }
                    else if (!e.Item.Checked)
                    {
                        if (SListType == "MSGTO")
                            Sql.WriteSqlmsgtoUpdate(items, "N");
                        else if (SListType == "MSGRoute")
                            Sql.WriteSqlmsgRouteUpdate(items, "N");
                        else if (SListType == "MSGFrom")
                            Sql.WriteSqlmsgFromUpdate(items, "N");
                        else if (SListType == "MSGSubject")
                            Sql.WriteSqlmsgSubjectUpdate(items, "N");
                    }
                }
            }
        }

        #endregion
    }

    #endregion
}