using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysSolution.Frm
{
    public partial class ingdlg : Form
    {
        public ingdlg()
        {
            InitializeComponent();
        }
        
        public void onLblMsg(string msg)
        {
            lblmsg.Text = msg;
        }

        private void ingdlg_Load(object sender, EventArgs e)
        {

        }
    }
}
