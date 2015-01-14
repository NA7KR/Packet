#region Using Directive

using System;
using System.Windows.Forms;
using PacketComs;

#endregion

namespace Packet
{

    public partial class Custom : Form
   {
        private static readonly
            FileSql MyFiles = new FileSql();
            private static readonly
            Sql Sql = new Sql();
           
            public bool loaded = false;
        

        #region Load
        private void Custom_Load_1(object sender, EventArgs e)
        {
            MyFiles.SelectMakeTable("CustomList", 50, "CustomList", "DSN=Packet", "Packet");
        }
        #endregion
   }
}