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
    public partial class ManualFrm : Form
    {
        public ManualFrm()
        {
            InitializeComponent();
        }

        DashboardForm mainForm;

        public ManualFrm(DashboardForm frm)
        {
            mainForm = frm;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ManualFrm_Load(object sender, EventArgs e)
        {
            cboRobotID.SelectedIndex = 0;
            cboRobotID2.SelectedIndex = 0;
            chkAvoid.Checked = true;
            chkLevelrunning.Checked = true;
        }
    }
}
