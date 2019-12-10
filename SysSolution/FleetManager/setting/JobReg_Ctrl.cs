using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysSolution.FleetManager.setting
{
    public partial class JobReg_Ctrl : UserControl
    {
        public JobReg_Ctrl()
        {
            InitializeComponent();
        }

        FleetManager_MainForm mainform;
        SettingMainCtrl settingMain;

        public JobReg_Ctrl(FleetManager.FleetManager_MainForm mainform, SettingMainCtrl settingmain)
        {
            this.mainform = mainform;
            settingMain = settingmain;
            InitializeComponent();
        }
        private void onCtrlEnable(bool b1)
        {
            btnJobReg.Enabled = b1;
            btnJobUpdate.Enabled = b1;
            btnJobDelete.Enabled = b1;

        }

        public void onInitSet()
        {
            try
            {
                groupBox_jobitem.Enabled = false;
                mainform.onDBRead_Joblist();

                dataGridView_reg.Rows.Clear();

                int cnt = mainform.JobSchedule_list.Count();
                for (int i = 0; i < cnt; i++)
                {
                    string strtmp = "";

                    JobSchedule jobinfo = mainform.JobSchedule_list.ElementAt(i).Value;

                    string job_id = jobinfo.job_id;
                    string job_name = jobinfo.job_name;
                    string mission_id_list = "";
                    string unloadmission_id_list = "";
                    string waitmission_id_list = "";
                    for (int j=0; j<jobinfo.mission_id.Count; j++)
                    {
                        mission_id_list += jobinfo.mission_id[j];

                        if(j!= jobinfo.mission_id.Count-1)
                            mission_id_list += ",";
                    }
                    for (int j = 0; j < jobinfo.unloadmission_id.Count; j++)
                    {
                        unloadmission_id_list += jobinfo.unloadmission_id[j];

                        if (j != jobinfo.unloadmission_id.Count - 1)
                            unloadmission_id_list += ",";
                    }
                    for (int j = 0; j < jobinfo.waitmission_id.Count; j++)
                    {
                        waitmission_id_list += jobinfo.waitmission_id[j];

                        if (j != jobinfo.waitmission_id.Count - 1)
                            waitmission_id_list += ",";
                    }

                    string robot_id_list = "";
                    for (int j = 0; j < jobinfo.robot_id.Count; j++)
                    {
                        robot_id_list += jobinfo.robot_id[j];

                        if (j != jobinfo.robot_id.Count - 1)
                            robot_id_list += ",";
                    }

                    string call_type = jobinfo.call_type;
                    string job_status = jobinfo.job_status;
                    string job_group = jobinfo.job_group;
                    dataGridView_reg.Rows.Add(new string[]{job_status, job_id,job_name,mission_id_list,unloadmission_id_list, waitmission_id_list, robot_id_list,call_type,job_group});
                }

                groupBox_jobitem.Visible = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("jobreg_Ctrl ..onInitSet err" + ex.Message.ToString());
            }
        }

        private void JobReg_Ctrl_Load(object sender, EventArgs e)
        {
            
        }

        private void onMissionlistRead()
        {
            try
            {
                mainform.onDBRead_Missionlist();

                int ncnt = mainform.missionlisttable.missioninfo.Count;

                listBox_missionlist.Items.Clear();
                listBox_Unloadmissionlist.Items.Clear();
                listBox_Waitmissionlist.Items.Clear();

                if (mainform.actiondatalitTable.Count > 0)
                    mainform.actiondatalitTable.Clear();

                for (int i = 0; i < ncnt; i++)
                {
                    string strdata = "";
                    strdata = string.Format("{0}({1})", mainform.missionlisttable.missioninfo[i].strMisssionName, mainform.missionlisttable.missioninfo[i].strMisssionID);
                    listBox_missionlist.Items.Add(strdata);
                    listBox_Unloadmissionlist.Items.Add(strdata);
                    listBox_Waitmissionlist.Items.Add(strdata);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("jobreg_Ctrl ..onMissionlistRead err" + ex.Message.ToString());
            }
        }

        private void onRobotlistRead(string strgroup)
        {
            try
            {
                mainform.onDBRead_Robotlist(strgroup);
                int cnt = mainform.Robot_RegInfo_list.Count();

                listBox_robotlist.Items.Clear();

                for (int i = 0; i < cnt; i++)
                {
                    string strtmp = "";
                    Robot_RegInfo robotinfo = mainform.Robot_RegInfo_list.ElementAt(i).Value;
                    strtmp = string.Format("{0}({1})", robotinfo.robot_name, robotinfo.robot_id);

                    listBox_robotlist.Items.Add(strtmp);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("jobreg_Ctrl ..onRobotlistRead err" + ex.Message.ToString());
            }
        }

        string strJobSavekind = ""; //insert or update  작업 DB 저장 종류
        private void btnJobReg_Click(object sender, EventArgs e)
        {
            groupBox_btn.Enabled = false;
            groupBox_jobitem.Enabled = true;
            groupBox_jobitem.Visible = true;

            strJobSavekind = "insert";

            DateTime dt = DateTime.Now;
            string strtime = string.Format("{0:d4}{1:d2}{2:d2}{3:d2}{4:d2}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute);//DateTime.Now.ToString("yyyyMMddHH");
            string strjobid = "Job_" + strtime;

            txtJobID.Text = strjobid;

            onMissionlistRead();
            

            listBox_selectedmissionlist.Items.Clear();
            listBox_selectedUnloadmissionlist.Items.Clear();
            listBox_selectedWaitmissionlist.Items.Clear();
            listBox_selectedrobotlist.Items.Clear();
        }

        private void btnJobUpdate_Click(object sender, EventArgs e)
        {
            strJobSavekind = "update";

            onJobUpdate1();
        }

        private void onJobUpdate1()
        {
            try
            {
                int nrow = dataGridView_reg.SelectedCells[0].RowIndex;

                if (nrow < 0 || nrow > dataGridView_reg.RowCount - 2)
                {
                    MessageBox.Show("변경할 작업을 선택하세요.");
                    return;
                }

                groupBox_btn.Enabled = false;
                groupBox_jobitem.Enabled = true;
                groupBox_jobitem.Visible = true;

                string jobstatus = dataGridView_reg["jobstatus", nrow].Value.ToString();
                string jobid = dataGridView_reg["jobid", nrow].Value.ToString();
                string jobname = dataGridView_reg["jobname", nrow].Value.ToString();
                string missionlist = dataGridView_reg["missionlist", nrow].Value.ToString();
                string unloadmissionlist = dataGridView_reg["unloadmissionlist", nrow].Value.ToString();
                string waitmissionlist = dataGridView_reg["waitmissionlist", nrow].Value.ToString();
                string robotlist = dataGridView_reg["robotlist", nrow].Value.ToString();
                string calltype = dataGridView_reg["calltype", nrow].Value.ToString();
                string jobgroup = dataGridView_reg["jobgroup", nrow].Value.ToString();


                if (jobstatus == "wait" || jobstatus == "")
                {
                    txtJobID.Text = jobid;
                    txtJobName.Text = jobname;

                    onMissionlistRead();

                    listBox_selectedmissionlist.Items.Clear();
                    listBox_selectedUnloadmissionlist.Items.Clear();
                    listBox_selectedWaitmissionlist.Items.Clear();
                    listBox_selectedrobotlist.Items.Clear();

                    string[] missionbuf = missionlist.Split(',');
                    for (int i = 0; i < missionbuf.Count(); i++)
                    {
                        listBox_selectedmissionlist.Items.Add(missionbuf[i]);
                    }

                    string[] unloadmissionbuf = unloadmissionlist.Split(',');
                    for (int i = 0; i < unloadmissionbuf.Count(); i++)
                    {
                        listBox_selectedUnloadmissionlist.Items.Add(unloadmissionbuf[i]);
                    }

                    string[] waitmissionbuf = waitmissionlist.Split(',');
                    for (int i = 0; i < waitmissionbuf.Count(); i++)
                    {
                        listBox_selectedWaitmissionlist.Items.Add(waitmissionbuf[i]);
                    }

                    string[] robotbuf = robotlist.Split(',');
                    for (int i = 0; i < robotbuf.Count(); i++)
                    {
                        listBox_selectedrobotlist.Items.Add(robotbuf[i]);
                    }

                    if (calltype == "Call")
                        cboJobCallKind.SelectedIndex = 0;
                    else if (calltype == "Alone")
                        cboJobCallKind.SelectedIndex = 1;

                    if (jobgroup == "pallet")
                        cboRobotGroup.SelectedIndex = 0;
                    else if (jobgroup == "conveyor")
                        cboRobotGroup.SelectedIndex = 1;
                    else if (jobgroup == "arm")
                        cboRobotGroup.SelectedIndex = 2;
                    else if (jobgroup == "delivery")
                        cboRobotGroup.SelectedIndex = 3;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("jobreg_Ctrl ..onJobUpdate1 err" + ex.Message.ToString());
            }
        }

        private void btnJobDelete_Click(object sender, EventArgs e)
        {
            strJobSavekind = "";
            int nrow = dataGridView_reg.SelectedCells[0].RowIndex;

            if (nrow < 0 || nrow > dataGridView_reg.RowCount - 2)
            {
                MessageBox.Show("삭제할 작업을 선택하세요.");
                return;
            }

            string strMsg = "작업을 삭제하시겠습니까?";
            if (DialogResult.OK == MessageBox.Show(strMsg, "확인", MessageBoxButtons.OKCancel))
            {
                string jobid = dataGridView_reg["jobid", nrow].Value.ToString();
                mainform.onDBDelete_JobSchedule(jobid);

                onInitSet();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                onJobScheduleSave();
            }
            catch (Exception ex)
            {
                Console.WriteLine("jobreg_Ctrl ..btnSave_Click err" + ex.Message.ToString());
            }
        }

        private void onJobScheduleSave()
        {
            try
            {
                JobSchedule job = new JobSchedule();
                job.mission_id = new List<string>();
                job.unloadmission_id = new List<string>();
                job.waitmission_id = new List<string>();
                job.robot_id = new List<string>();
                

                string strjobid = txtJobID.Text.ToString();
                job.job_id = strjobid;

                if (txtJobName.Text == "")
                {
                    MessageBox.Show("작업명을 입력하세요.");
                    return;
                }
                string strjobname = txtJobName.Text.ToString();
                job.job_name = strjobname;

                if (cboJobCallKind.SelectedIndex < 0)
                {
                    MessageBox.Show("작업방법을 입력하세요.");
                    return;
                }

                string strjobgroupcallkind = cboJobCallKind.SelectedItem.ToString();
                job.call_type = strjobgroupcallkind;


                if (cboRobotGroup.SelectedIndex < 0)
                {
                    MessageBox.Show("작업그룹을 입력하세요.");
                    return;
                }

                string strjobgroup = cboRobotGroup.SelectedItem.ToString();
                job.job_group = strjobgroup;

                if (listBox_selectedmissionlist.Items.Count < 0)
                {
                    MessageBox.Show("미션을 선택하세요.");
                    return;
                }

                string strmissionlist = "";
                for (int i = 0; i < listBox_selectedmissionlist.Items.Count; i++)
                {
                     strmissionlist = listBox_selectedmissionlist.Items[i].ToString();
                    job.mission_id.Add(strmissionlist);
                }

                string strunloadmissionlist = "";
                for (int i = 0; i < listBox_selectedUnloadmissionlist.Items.Count; i++)
                {
                    strunloadmissionlist = listBox_selectedUnloadmissionlist.Items[i].ToString();
                    job.unloadmission_id.Add(strunloadmissionlist);
                }

                string strwaitmissionlist = "";
                for (int i = 0; i < listBox_selectedWaitmissionlist.Items.Count; i++)
                {
                    strwaitmissionlist = listBox_selectedWaitmissionlist.Items[i].ToString();
                    job.waitmission_id.Add(strwaitmissionlist);
                }

                if (listBox_selectedrobotlist.Items.Count < 0)
                {
                    MessageBox.Show("로봇을 선택하세요.");
                    return;
                }

                string strrobotlist = "";
                for (int i = 0; i < listBox_selectedrobotlist.Items.Count; i++)
                {
                    strrobotlist = listBox_selectedrobotlist.Items[i].ToString();
                    job.robot_id.Add(strrobotlist);
                }

                //if (strJobSavekind == "insert")
                    job.job_status = "wait";
                

                mainform.onDBSave_JobSchedule(strJobSavekind, job);

                onInitSet();

                groupBox_btn.Enabled = true;
                groupBox_jobitem.Visible = false;
                strJobSavekind = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine("jobreg_Ctrl ..onJobScheduleSave err" + ex.Message.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            groupBox_btn.Enabled = true;
            groupBox_jobitem.Visible = false;
            strJobSavekind = "";
        }

        private void btnSelectmission_Click(object sender, EventArgs e)
        {
            int idx = listBox_missionlist.SelectedIndex;
            if(idx>-1)
            {
                listBox_selectedmissionlist.Items.Add(listBox_missionlist.SelectedItem.ToString());
            }
        }

        private void btnRemovemission_Click(object sender, EventArgs e)
        {
            int idx = listBox_selectedmissionlist.SelectedIndex;
            if (idx > -1)
            {
                listBox_selectedmissionlist.Items.RemoveAt(idx);
            }
        }

        private void btnSelectUnloadmission_Click(object sender, EventArgs e)
        {
            int idx = listBox_Unloadmissionlist.SelectedIndex;
            if (idx > -1)
            {
                listBox_selectedUnloadmissionlist.Items.Add(listBox_Unloadmissionlist.SelectedItem.ToString());
            }
        }

        private void btnRemoveUnloadmission_Click(object sender, EventArgs e)
        {
            int idx = listBox_selectedUnloadmissionlist.SelectedIndex;
            if (idx > -1)
            {
                listBox_selectedUnloadmissionlist.Items.RemoveAt(idx);
            }
        }

        private void btnSelectWaitmission_Click(object sender, EventArgs e)
        {
            int idx = listBox_Waitmissionlist.SelectedIndex;
            if (idx > -1)
            {
                listBox_selectedWaitmissionlist.Items.Add(listBox_Waitmissionlist.SelectedItem.ToString());
            }
        }

        private void btnRemoveWaitmission_Click(object sender, EventArgs e)
        {
            int idx = listBox_selectedWaitmissionlist.SelectedIndex;
            if (idx > -1)
            {
                listBox_selectedWaitmissionlist.Items.RemoveAt(idx);
            }
        }


        private void btnSelectrobot_Click(object sender, EventArgs e)
        {
            int idx = listBox_robotlist.SelectedIndex;
            if (idx > -1)
            {
                listBox_selectedrobotlist.Items.Add(listBox_robotlist.SelectedItem.ToString());
            }
        }

        private void btnRemoverobot_Click(object sender, EventArgs e)
        {
            int idx = listBox_selectedrobotlist.SelectedIndex;
            if (idx > -1)
            {
                listBox_selectedrobotlist.Items.RemoveAt(idx);
            }
        }

        

        private void cboRobotGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboRobotGroup.SelectedIndex > -1)
            {
                string strgroup = cboRobotGroup.SelectedItem.ToString();
                onRobotlistRead(strgroup);

                listBox_selectedrobotlist.Items.Clear();
            }
        }

        private void dataGridView_reg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int nrow = dataGridView_reg.SelectedCells[0].RowIndex;

            if (nrow < 0 || nrow > dataGridView_reg.RowCount - 2) return;

            if(strJobSavekind=="update")
            {
                onJobUpdate1();
            }
        }

        private void groupBox_jobitem_Enter(object sender, EventArgs e)
        {

        }
    }
}
