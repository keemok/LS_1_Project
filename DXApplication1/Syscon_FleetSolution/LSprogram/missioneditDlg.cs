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
    public partial class missioneditDlg : Form
    {
        public missioneditDlg()
        {
            InitializeComponent();
        }

        private void missioneditDlg_Load(object sender, EventArgs e)
        {
            missionnameTextbox.Text = "";
            missionidTextbox.Text = "";
            DateTime dt = DateTime.Now;
            string strtime = string.Format("{0:d4}{1:d2}{2:d2}{3:d2}{4:d2}{5:d2}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);

            string strid = "MID" + strtime;

            missionidTextbox.Text = strid;

        }
        public string strMissionName = "";
        public string strMissionID = "";
        public string strMissionATC = "";
        private void button1_Click(object sender, EventArgs e)
        {
            strMissionName = missionnameTextbox.Text.ToString();
            strMissionID = missionidTextbox.Text.ToString();

            this.DialogResult = DialogResult.OK;
        }
    }
}
