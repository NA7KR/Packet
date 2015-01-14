#region Using Directive

using System;
using System.Windows.Forms;
using PacketComs;

#endregion

namespace Packet
{
    public partial class Custom : Form
    {
        
        private static readonly FileSql MyFiles = new FileSql();

        public Custom()
        {
            InitializeComponent();
        }

        #region Custom Loader
        private void Custom_Load(object sender, EventArgs e)
        {
            Loader();
        }
        #endregion

        #region Loader
        private void Loader()
        {
            MyFiles.SelectMakeCustomTable("CustomList", 50, "CustomList", "DSN=Packet", "Packet");
        }
        #endregion

    }
}
