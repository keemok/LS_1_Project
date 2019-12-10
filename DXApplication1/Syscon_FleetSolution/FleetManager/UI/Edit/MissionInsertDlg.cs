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

            cboXis_name.Items.Clear();
            cboTriggerkind.Items.Clear();

            onXis_TriggerRead();
        }
        public string strMissionName="";
        public string strMissionID = "";
        public string strMissionLevel = "";
        public string strTriggerflag = "";

        private void onXis_TriggerRead()
        {
            int cnt = Data.Instance.xisInfo_list.xisinfo.Count;

            if(cnt>0)
            {
                cboXis_name.Items.Add("사용안함");
                for (int i = 0; i < cnt; i++)
                {
                    string strxisname = Data.Instance.xisInfo_list.xisinfo[i].xis_name;
                    cboXis_name.Items.Add(strxisname);
                }

                cboXis_name.SelectedIndex = 0;
            }

            cnt = Data.Instance.triggerInfo_list.triggerinfo.Count;

            if (cnt > 0)
            {
                cboTriggerkind.Items.Add("사용안함");
                for (int i = 0; i < cnt; i++)
                {
                    string strname = Data.Instance.triggerInfo_list.triggerinfo[i].trigger_name;
                    cboTriggerkind.Items.Add(strname);
                }

                cboTriggerkind.SelectedIndex = 0;
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (txtMissionName.Text.ToString() == "")
            {
                MessageBox.Show("미션이름을 입력하세요");
                return;
            }

            if(cboXis_name.SelectedIndex<0)
            {
                MessageBox.Show("Trigger종류를 선택하세요.");
                return;
            }
            if (cboTriggerkind.SelectedIndex < 0)
            {
                MessageBox.Show("Trigger종류를 선택하세요.");
                return;
            }

            int xisidx = cboXis_name.SelectedIndex;
            int triggeridx = cboTriggerkind.SelectedIndex;

            string strxisid = "";
            string strtriggerid = "";
            if (xisidx == 0)
            { }
            else strxisid = Data.Instance.xisInfo_list.xisinfo[xisidx - 1].xis_id;

            if (triggeridx == 0)
            { }
            else strtriggerid = Data.Instance.triggerInfo_list.triggerinfo[triggeridx-1].trigger_id;


            if (triggeridx == 0)
                strTriggerflag = "";
            else
                strTriggerflag = string.Format("{0}-{1}", strxisid, strtriggerid);

            strMissionName = txtMissionName.Text.ToString();
            strMissionID = txtMissionID.Text.ToString();
            strMissionLevel = txtMissionLevel.Text.ToString();

            this.DialogResult = DialogResult.OK;

        }

        private void cboXis_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboXis_name.SelectedIndex==0)
            {
                cboTriggerkind.Enabled = false;
            }
            else
                cboTriggerkind.Enabled = true;

        }
    }
}
