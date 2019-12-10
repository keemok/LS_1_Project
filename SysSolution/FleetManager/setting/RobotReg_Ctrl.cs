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
    public partial class RobotReg_Ctrl : UserControl
    {
        public RobotReg_Ctrl()
        {
            InitializeComponent();
        }
        FleetManager_MainForm mainform;
        SettingMainCtrl settingMain;

        public RobotReg_Ctrl(FleetManager.FleetManager_MainForm mainform, SettingMainCtrl settingmain)
        {
            this.mainform = mainform;
            settingMain = settingmain;
            InitializeComponent();
        }

        private void RobotReg_Ctrl_Load(object sender, EventArgs e)
        {
        }

        private void onCtrlEnable(bool b1)
        {
            btnRobotReg.Enabled = b1;
            btnRobotUpdate.Enabled = b1;
            btnRobotDelete.Enabled = b1;

            groupBox_reg.Enabled = b1;
        }

        public void onInitSet()
        {
            try
            {
                mainform.onDBRead_Robotlist("all");
                mainform.onDBRead_RobotStatus();
                mainform.onDBRead_Maplist();

                onCtrlEnable(false);

                dataGridView_robotreg.Rows.Clear();

                List<string> strrobotlist = new List<string>();

                int cnt = mainform.Robot_RegInfo_list.Count();

                for (int i = 0; i < cnt; i++)
                {
                    string strtmp = "";
                    Robot_RegInfo robotinfo = mainform.Robot_RegInfo_list.ElementAt(i).Value;
                    strtmp = string.Format("{0},{1},{2},{3},{4}({5})", robotinfo.robot_id, robotinfo.robot_name, robotinfo.robot_ip, robotinfo.robot_group,robotinfo.map_name,robotinfo.map_id);

                    if (mainform.Robot_Status_list.ContainsKey(robotinfo.robot_id))
                    {
                        strtmp = string.Format("{0},{1}", mainform.Robot_Status_list[robotinfo.robot_id].work_status, strtmp);
                    }
                    else
                        strtmp = string.Format("wait,{0}", strtmp);

                    string[] strtmpbuf = strtmp.Split(',');
                    dataGridView_robotreg.Rows.Add(strtmpbuf);
                }

                if (mainform.Map_list.Count > 0)
                {
                    for (int j = 0; j < mainform.Map_list.Count; j++)
                    {
                        Map_list maplist = new Map_list();
                        maplist = mainform.Map_list.ElementAt(j).Value;
                        string strmap = string.Format("{0}({1})", maplist.map_name, maplist.map_id);
                        cboMap.Items.Add(strmap);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("robotreg_Ctrl ..onInitSet err" + ex.Message.ToString());
            }
        }

        private void dataGridView_robotreg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                onCtrlEnable(false);

                int nrow = dataGridView_robotreg.SelectedCells[0].RowIndex;

                if (nrow < 0 || nrow > dataGridView_robotreg.RowCount - 2) return;

                string robot_jobingstatus = dataGridView_robotreg["robot_jobingstatus", nrow].Value.ToString();
                string robotid = dataGridView_robotreg["RobotID", nrow].Value.ToString();
                string robotname = dataGridView_robotreg["RobotName", nrow].Value.ToString();
                string ip = dataGridView_robotreg["IP", nrow].Value.ToString();
                string robotgroup = dataGridView_robotreg["RobotGroup", nrow].Value.ToString();
                string map = dataGridView_robotreg["map", nrow].Value.ToString();

                if (robot_jobingstatus=="wait")
                {
                    onCtrlEnable(true);
                }
                else
                {
                    onCtrlEnable(false);
                }

                txtRobotID.Text = robotid;
                txtRobotName.Text = robotname;
                string[] stripbuf = ip.Split('.');
                txtIP1.Text = stripbuf[0];
                txtIP2.Text = stripbuf[1];
                txtIP3.Text = stripbuf[2];
                txtIP4.Text = stripbuf[3];

                if (robotgroup == "pallet") cboRobotGroup.SelectedIndex = 0;
                else if (robotgroup == "conveyor") cboRobotGroup.SelectedIndex = 1;
                else if (robotgroup == "arm") cboRobotGroup.SelectedIndex = 2;
                else if (robotgroup == "delivery") cboRobotGroup.SelectedIndex = 3;

               //for(int i=0; i<cboMap.Items.Count; i++)
               // {
                    cboMap.SelectedItem = map;
                //}

            }
            catch (Exception ex)
            {
                Console.WriteLine("robotreg_Ctrl ..dataGridView_robotreg_CellClick err" + ex.Message.ToString());
            }
        }

        private void btnRobotReg_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtRobotID.Text=="")
                {
                    MessageBox.Show("로봇 id를 입력하세요.");
                    return;
                }
                if(txtRobotName.Text=="")
                {
                    MessageBox.Show("로봇 이름을 입력하세요.");
                    return;
                }
                if(txtIP1.Text=="" || txtIP2.Text == "" || txtIP3.Text == "" || txtIP4.Text == "")
                {
                    MessageBox.Show("로봇 IP를 입력하세요.");
                    return;
                }
                string robotid = txtRobotID.Text.ToString();
                string robotname = txtRobotName.Text.ToString();
                string robotip = string.Format("{0}.{1}.{2}.{3}", txtIP1.Text.ToString(), txtIP2.Text.ToString(), txtIP3.Text.ToString(), txtIP4.Text.ToString());
                string robotgroup = cboRobotGroup.SelectedItem.ToString();
                string robotmap = cboMap.SelectedItem.ToString();
                int idx =robotmap.IndexOf("(");
                int idx2 = robotmap.IndexOf(")");
                robotmap = robotmap.Substring(idx+1, idx2-idx-1);

                mainform.onDBInsert_Robotlist(robotid, robotname, robotip, robotgroup,robotmap);

                onInitSet();
            }
            catch (Exception ex)
            {
                Console.WriteLine("robotreg_Ctrl ..btnRobotReg_Click err" + ex.Message.ToString());
            }

        }

        private void btnRobotUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRobotName.Text == "")
                {
                    MessageBox.Show("로봇 이름을 입력하세요.");
                    return;
                }
                if (txtIP1.Text == "" || txtIP2.Text == "" || txtIP3.Text == "" || txtIP4.Text == "")
                {
                    MessageBox.Show("로봇 IP를 입력하세요.");
                    return;
                }
                string robotid = txtRobotID.Text.ToString();
                string robotname = txtRobotName.Text.ToString();
                string robotip = string.Format("{0}.{1}.{2}.{3}", txtIP1.Text.ToString(), txtIP2.Text.ToString(), txtIP3.Text.ToString(), txtIP4.Text.ToString());
                string robotgroup = cboRobotGroup.SelectedItem.ToString();

                string robotmap = cboMap.SelectedItem.ToString();
                int idx = robotmap.IndexOf("(");
                int idx2 = robotmap.IndexOf(")");
                robotmap = robotmap.Substring(idx + 1, idx2 - idx - 1);

                mainform.onDBUpdate_Robotlist(robotid, robotname, robotip, robotgroup, robotmap);

                onInitSet();
            }
            catch (Exception ex)
            {
                Console.WriteLine("robotreg_Ctrl ..btnRobotReg_Click err" + ex.Message.ToString());
            }
        }

        private void btnRobotDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int nrow = dataGridView_robotreg.SelectedCells[0].RowIndex;

                if (nrow < 0 || nrow > dataGridView_robotreg.RowCount - 2) return;

                string strMsg = "로봇을 삭제하시겠습니까?";
                if (DialogResult.OK == MessageBox.Show(strMsg, "확인", MessageBoxButtons.OKCancel))
                {
                    string robotid = dataGridView_robotreg["RobotID", nrow].Value.ToString();

                    mainform.onDBDelete_Robotlist(robotid);

                    onInitSet();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("robotreg_Ctrl ..btnRobotDelete_Click err" + ex.Message.ToString());
            }
        }
    }
}
