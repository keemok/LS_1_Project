using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysSolution
{
    public partial class RobotJob_HistoryForm : Form
    {
        public RobotJob_HistoryForm()
        {
            InitializeComponent();
        }

        private void RobotJob_HistoryForm_Load(object sender, EventArgs e)
        {
#if _jobhistory
            Data.Instance.MAINFORM = this;
#endif
        }
    }
}
