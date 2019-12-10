using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Syscon_Solution.FleetManager.UI.Edit
{
    public partial class ActionInsertDlg : Form
    {
        public ActionInsertDlg()
        {
            InitializeComponent();
        }

        private void ActionInsertDlg_Load(object sender, EventArgs e)
        {
            cboActionType.SelectedIndex = 0;
        }
        public string strActiontype = "";
        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (cboActionType.SelectedIndex == 0) strActiontype = "Goal-Point";
            else if (cboActionType.SelectedIndex == 1) strActiontype = "Basic-Move";
            else if (cboActionType.SelectedIndex == 2) strActiontype = "Stable_pallet";
            else if (cboActionType.SelectedIndex == 3) strActiontype = "Action_wait";
            else if (cboActionType.SelectedIndex == 4) strActiontype = "UR_MISSION";

            this.DialogResult = DialogResult.OK;
        }
    }
}
