using System;
using System.Windows.Forms;
using PacketComs;

namespace Packet
{

    public partial class Mail : Form
    {
        private readonly ModifyFile _myFiles = new ModifyFile();
        public Mail()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            int i, k;
            string j,l,m,n,o,p ;
            _myFiles.RX(out i, out j, out k, out l, out m, out n, out o, out p);
            Invoke((Action)delegate { richTextBox1.Text = i.ToString() + " " + j + " " + k.ToString() + " " + l + " " + m + " "  + n + " "  + o + " "  + p; });
        }


        
    }
}