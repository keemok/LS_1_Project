using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//add using info
using Rosbridge.Client;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Reflection;
using System.Numerics;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Threading;
using System.Net.Sockets;
using System.Net;

using MySql.Data.MySqlClient;

using System.Drawing.Imaging;using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;


namespace Syscon_Solution.FleetManager.UI.Edit
{
    public partial class TaskEdit_Ctrl : UserControl
    {
        Fleet_Main mainform;

        string strTaskSavekind = "";


        public TaskEdit_Ctrl()
        {
            InitializeComponent();
        }

        public TaskEdit_Ctrl(Fleet_Main frm)
        {
            mainform = frm;
            InitializeComponent();
        }

        public void onInitSet()
        {
            try
            {
                //grid clear
                groupBox_jobitem.Enabled = false;
                dataGridView_reg.Rows.Clear();


                //task list read
                mainform.dbBridge.onDBRead_Tasklist();

                //컨트롤 clear
                onCtrlClear();

                //로봇 작업그룹 읽기 -> 콤보박스 표시
                cboRobotGroup.Items.Clear();
                mainform.dbBridge.onDBRead_RobotGrouplist();

                //grid dp
                int cnt = Data.Instance.Task_list.Count();
                for (int i = 0; i < cnt; i++)
                {
                    string strtmp = "";

                    Task_Info taskinfo = Data.Instance.Task_list.ElementAt(i).Value;

                    string task_status = taskinfo.task_status;

                    if (task_status == "") task_status = "wait";

                    string task_id=taskinfo.task_id;
                    string task_name=taskinfo.task_name;
                    string task_missionlist=taskinfo.mission_id_list;
                    string task_robotlist=taskinfo.robot_id_list;
                    string taskloopflag=taskinfo.taskloopflag;
                    string robotgroupid = taskinfo.robot_group_id;

                    string strgroupname = "";

                    if (robotgroupid == "All")
                    {
                        strgroupname = "All";
                    }
                    else
                    {
                        int cnt2 = Data.Instance.robotgroup_list.robotgroup.Count;
                        for (int i2 = 0; i2 < cnt; i2++)
                        {
                            if (Data.Instance.robotgroup_list.robotgroup[i2].robot_group_id == robotgroupid)
                            {
                                strgroupname = Data.Instance.robotgroup_list.robotgroup[i2].robot_group_name;
                                break;
                            }
                        }
                    }

                    dataGridView_reg.Rows.Add(new string[] { task_status, task_id, task_name, task_missionlist, task_robotlist, taskloopflag, strgroupname });
                }

                groupBox_jobitem.Visible = false;

               
            }
            catch (Exception ex)
            {
                Console.WriteLine("TaskEdit_Ctrl ..onInitSet err" + ex.Message.ToString());
            }
        }

        private void onCtrlClear()
        {
            txtTaskID.Text = "";
            txtTaskName.Text = "";
            listBox_missionlist.Items.Clear();
            listBox_robotlist.Items.Clear();
            listBox_selectedmissionlist.Items.Clear();
            listBox_selectedrobotlist.Items.Clear();
        }

        private void TaskEdit_Ctrl_Load(object sender, EventArgs e)
        {

        }

        private void btnTaskReg_Click(object sender, EventArgs e)
        {
            groupBox_btn.Enabled = false;
            groupBox_jobitem.Enabled = true;
            groupBox_jobitem.Visible = true;

            strTaskSavekind = "insert";

            DateTime dt = DateTime.Now;
            string strtime = string.Format("{0:d4}{1:d2}{2:d2}{3:d2}{4:d2}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute);//DateTime.Now.ToString("yyyyMMddHH");
            string strjobid = "Task_" + strtime;

            txtTaskID.Text = strjobid;

            onMissionlistRead();

            cboRobotGroup.Items.Clear();
            int cnt = Data.Instance.robotgroup_list.robotgroup.Count;
            for(int i=0; i<cnt; i++)
            {
                string strrobotgroupname = Data.Instance.robotgroup_list.robotgroup[i].robot_group_name;
                cboRobotGroup.Items.Add(strrobotgroupname);
            }
            cboRobotGroup.Items.Add("All");
            cboRobotGroup.SelectedIndex = 0;
            listBox_selectedmissionlist.Items.Clear();
            
        }

        private void onMissionlistRead()
        {
            try
            {
                mainform.dbBridge.onDBRead_Missionlist();
                int ncnt = Data.Instance.missionlisttable.missioninfo.Count;

                listBox_missionlist.Items.Clear();
                
                for (int i = 0; i < ncnt; i++)
                {
                    string strdata = "";
                    strdata = string.Format("{0}({1})", Data.Instance.missionlisttable.missioninfo[i].strMisssionName, Data.Instance.missionlisttable.missioninfo[i].strMisssionID);
                    listBox_missionlist.Items.Add(strdata);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("TaskEdit_Ctrl ..onMissionlistRead err" + ex.Message.ToString());
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

        private void onRobotlistRead(string strgroup)
        {
            try
            {
                if (strgroup == "All")
                {

                    int cnt = Data.Instance.Robot_RegInfo_list.Count();
                    listBox_robotlist.Items.Clear();

                    for (int i = 0; i < cnt; i++)
                    {
                        string strtmp = "";
                        Robot_RegInfo robotinfo = Data.Instance.Robot_RegInfo_list.ElementAt(i).Value;
                            strtmp = string.Format("{0}({1})", robotinfo.robot_name, robotinfo.robot_id);

                            listBox_robotlist.Items.Add(strtmp);
                    }
                }
                else
                {
                    string strgroupid = "";
                    int cnt = Data.Instance.robotgroup_list.robotgroup.Count;
                    for (int i = 0; i < cnt; i++)
                    {
                        if (Data.Instance.robotgroup_list.robotgroup[i].robot_group_name == strgroup)
                        {
                            strgroupid = Data.Instance.robotgroup_list.robotgroup[i].robot_group_id;
                            break;
                        }
                    }
                    if (strgroupid == "")
                    {
                        return;
                    }

                    cnt = Data.Instance.Robot_RegInfo_list.Count();

                    listBox_robotlist.Items.Clear();

                    for (int i = 0; i < cnt; i++)
                    {
                        string strtmp = "";
                        Robot_RegInfo robotinfo = Data.Instance.Robot_RegInfo_list.ElementAt(i).Value;
                        if (robotinfo.robot_group == strgroupid)
                        {
                            strtmp = string.Format("{0}({1})", robotinfo.robot_name, robotinfo.robot_id);

                            listBox_robotlist.Items.Add(strtmp);
                        }

                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("TaskEdit_Ctrl ..onRobotlistRead err" + ex.Message.ToString());
            }
        }

        private void btnTaskUpdate_Click(object sender, EventArgs e)
        {

            strTaskSavekind = "update";

            onTaskUpdate1();
        }

        private void onTaskUpdate1()
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

                string taskstatus = dataGridView_reg["taskstatus", nrow].Value.ToString();
                string taskid = dataGridView_reg["taskid", nrow].Value.ToString();
                string taskname = dataGridView_reg["taskname", nrow].Value.ToString();
                string missionlist = dataGridView_reg["missionlist", nrow].Value.ToString();
                string robotlist = dataGridView_reg["robotlist", nrow].Value.ToString();
                string taskLoopflag = dataGridView_reg["taskLoopflag", nrow].Value.ToString();
                string robotgroup = dataGridView_reg["robotgroup", nrow].Value.ToString();


                if (taskstatus == "wait" || taskstatus == "")
                {
                    txtTaskID.Text = taskid;
                    txtTaskName.Text = taskname;

                    onMissionlistRead();

                    listBox_selectedmissionlist.Items.Clear();
                    listBox_selectedrobotlist.Items.Clear();

                    string[] missionbuf = missionlist.Split(',');
                    for (int i = 0; i < missionbuf.Count(); i++)
                    {
                        listBox_selectedmissionlist.Items.Add(missionbuf[i]);
                    }

                    string[] robotbuf = robotlist.Split(',');
                    for (int i = 0; i < robotbuf.Count(); i++)
                    {
                        listBox_selectedrobotlist.Items.Add(robotbuf[i]);
                    }

                    cboRobotGroup.Items.Clear();
                    int cnt = Data.Instance.robotgroup_list.robotgroup.Count;
                    for (int i = 0; i < cnt; i++)
                    {
                        string strrobotgroupname = Data.Instance.robotgroup_list.robotgroup[i].robot_group_name;
                        cboRobotGroup.Items.Add(strrobotgroupname);
                    }
                    cboRobotGroup.SelectedIndex = 0;
                    

                    int cnt2 = cboRobotGroup.Items.Count;
                    for(int i=0; i<cnt2; i++)
                    {
                        string strcbo = cboRobotGroup.Items[i].ToString();
                        if(strcbo== robotgroup)
                        {
                            cboRobotGroup.SelectedIndex = i;
                            break;
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("onTaskUpdate1 err" + ex.Message.ToString());
            }
        }

        private void btnTaskDelete_Click(object sender, EventArgs e)
        {
            strTaskSavekind = "";
            int nrow = dataGridView_reg.SelectedCells[0].RowIndex;

            if (nrow < 0 || nrow > dataGridView_reg.RowCount - 2)
            {
                MessageBox.Show("삭제할 작업을 선택하세요.");
                return;
            }

            string strMsg = "작업을 삭제하시겠습니까?";
            if (DialogResult.OK == MessageBox.Show(strMsg, "확인", MessageBoxButtons.OKCancel))
            {
                string taskid = dataGridView_reg["taskid", nrow].Value.ToString();
                mainform.dbBridge.onDBDelete_Task(taskid);

                onInitSet();
            }
        }

        private void btnSelectmission_Click(object sender, EventArgs e)
        {
            int idx = listBox_missionlist.SelectedIndex;
            if (idx > -1)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                onTaskSave();
            }
            catch (Exception ex)
            {
                Console.WriteLine("..btnSave_Click err" + ex.Message.ToString());
            }
        }

        private void onTaskSave()
        {
            try
            {
                Task_Info task = new Task_Info();
         
                string strjobid = txtTaskID.Text.ToString();
                task.task_id = strjobid;

                if (txtTaskName.Text == "")
                {
                    MessageBox.Show("작업명을 입력하세요.");
                    return;
                }
                string strjobname = txtTaskName.Text.ToString();
                task.task_name = strjobname;

              
                if (cboRobotGroup.SelectedIndex < 0)
                {
                    MessageBox.Show("로봇그룹을 선택하세요.");
                    return;
                }

                if (listBox_selectedrobotlist.Items.Count < 0)
                {
                    MessageBox.Show("로봇을 선택하세요.");
                    return;
                }

                if (listBox_selectedmissionlist.Items.Count < 0)
                {
                    MessageBox.Show("미션을 선택하세요.");
                    return;
                }

                

                string strrobotgroup = cboRobotGroup.SelectedItem.ToString();
                string strmissionlist = "";
                string strrobotlist = "";
                string strgroupid = "";
                if (strrobotgroup == "All")
                {
                    strgroupid = "All";
                }
                else
                {
                    int cnt = Data.Instance.robotgroup_list.robotgroup.Count;
                    for (int i = 0; i < cnt; i++)
                    {
                        if (Data.Instance.robotgroup_list.robotgroup[i].robot_group_name == strrobotgroup)
                        {
                            strgroupid = Data.Instance.robotgroup_list.robotgroup[i].robot_group_id;
                            break;
                        }
                    }
                }

                for (int i = 0; i < listBox_selectedmissionlist.Items.Count; i++)
                {
                    string strmissionlist2 = listBox_selectedmissionlist.Items[i].ToString();

                    int idx = strmissionlist2.IndexOf("(");
                    int idx2 = strmissionlist2.IndexOf(")");

                    strmissionlist2 = strmissionlist2.Substring(idx + 1, idx2 - idx - 1);


                    if (i < listBox_selectedmissionlist.Items.Count - 1)
                        strmissionlist += strmissionlist2 + ",";
                    else strmissionlist += strmissionlist2;
                }


                for (int i = 0; i < listBox_selectedrobotlist.Items.Count; i++)
                {
                    string strrobotlist2 = listBox_selectedrobotlist.Items[i].ToString();
                    int idx = strrobotlist2.IndexOf("(");
                    int idx2 = strrobotlist2.IndexOf(")");

                    strrobotlist2 = strrobotlist2.Substring(idx + 1, idx2 - idx - 1);

                    if (i < listBox_selectedrobotlist.Items.Count - 1)
                        strrobotlist += strrobotlist2 + ",";
                    else strrobotlist += strrobotlist2;

                }

                task.mission_id_list = strmissionlist;
                task.robot_id_list = strrobotlist;
                task.task_status = "wait";
                task.start_idx = "0";
                task.taskloopflag = "1";

                task.robot_group_id = strgroupid;


                mainform.dbBridge.onDBSave_Task(strTaskSavekind, task);
                //Thread.Sleep(1000);
                onInitSet();

                groupBox_btn.Enabled = true;
                groupBox_jobitem.Visible = false;
                strTaskSavekind = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine("onTaskSave err" + ex.Message.ToString());
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            groupBox_btn.Enabled = true;
            groupBox_jobitem.Visible = false;
            strTaskSavekind = "";
            onCtrlClear();
        }

    


        /*
      

      

       

      

     
     

   





        private void cboRobotGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView_reg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int nrow = dataGridView_reg.SelectedCells[0].RowIndex;

            if (nrow < 0 || nrow > dataGridView_reg.RowCount - 2) return;

            if (strJobSavekind == "update")
            {
                onJobUpdate1();
            }
        }
        */
    }
}
