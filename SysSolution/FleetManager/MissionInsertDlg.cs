using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysSolution.FleetManager
{
    public partial class MissionInsertDlg : Form
    {
        public MissionInsertDlg()
        {
            InitializeComponent();
        }

        private void MissionInsertDlg_Load(object sender, EventArgs e)
        {
            txtMissionName.Text = "name";
            txtMissionLevel.Text = "0";
            txtMissionID.Enabled = false;

            DateTime dt = DateTime.Now;
            string strtime = string.Format("{0:d4}{1:d2}{2:d2}{3:d2}{4:d2}{5:d2}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute,dt.Second);

            string strid = "MID" + strtime;
            txtMissionID.Text = strid;
        }
        public string strMissionName="";
        public string strMissionID = "";
        public string strMissionLevel = "";
        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (txtMissionName.Text.ToString() == "")
            {
                MessageBox.Show("미션이름을 입력하세요");
                return;
            }

            strMissionName = txtMissionName.Text.ToString();
            strMissionID = txtMissionID.Text.ToString();
            strMissionLevel = txtMissionLevel.Text.ToString();

            this.DialogResult = DialogResult.OK;

        }
    }
}
