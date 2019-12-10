using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Syscon_Solution.LSprogram
{
    public partial class actioninsertDlg : Form
    {
        public actioninsertDlg()
        {
            InitializeComponent();
        }
        public string strActiontype = "";
        private void button1_Click(object sender, EventArgs e)
        {
            if (actionType.SelectedIndex == 0) strActiontype = "Goal-Point";
            else if (actionType.SelectedIndex == 1) strActiontype = "Basic-Move";
            else if (actionType.SelectedIndex == 2) strActiontype = "Stable_pallet";
            else if (actionType.SelectedIndex == 3) strActiontype = "Action_wait";
            else if (actionType.SelectedIndex == 4) strActiontype = "Docking";

            this.DialogResult = DialogResult.OK;
        }
    }
}
