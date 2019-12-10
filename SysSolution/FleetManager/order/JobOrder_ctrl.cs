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

namespace SysSolution.FleetManager.order
{
    public partial class JobOrder_ctrl : UserControl
    {
        public JobOrder_ctrl()
        {
            InitializeComponent();
        }

        FleetManager_MainForm mainform;
        
        public JobOrder_ctrl(FleetManager.FleetManager_MainForm mainform)
        {
            this.mainform = mainform;
            
            InitializeComponent();
        }

        public void onInitSet()
        {
            try
            {
                mainform.onDBRead_Joblist();

                dataGridView_reg.Rows.Clear();

                int cnt = mainform.JobSchedule_list.Count();
                for (int i = 0; i < cnt; i++)
                {
                    JobSchedule jobinfo = mainform.JobSchedule_list.ElementAt(i).Value;

                    string job_id = jobinfo.job_id;
                    string job_name = jobinfo.job_name;
                    string mission_id_list = "";
                    string unloadmission_id_list = "";
                    string waitmission_id_list = "";
                    for (int j = 0; j < jobinfo.mission_id.Count; j++)
                    {
                        mission_id_list += jobinfo.mission_id[j];

                        if (j != jobinfo.mission_id.Count - 1)
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
                    dataGridView_reg.Rows.Add(new string[] { job_status, job_id, job_name, mission_id_list, unloadmission_id_list, waitmission_id_list, robot_id_list, call_type, job_group });
                }

                btnJobRun.Enabled = false;
                btnJobStop.Enabled = false;


            }
            catch (Exception ex)
            {
                Console.WriteLine("joborder_Ctrl ..onInitSet err" + ex.Message.ToString());
            }
        }

        private void JobOrder_ctrl_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView_reg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtJobname.Text = "";

            int nrow = dataGridView_reg.SelectedCells[0].RowIndex;

            if (nrow < 0 || nrow > dataGridView_reg.RowCount - 2) return;

            string jobstatus = dataGridView_reg["jobstatus", nrow].Value.ToString();
            string jobname = dataGridView_reg["jobname", nrow].Value.ToString();

            txtJobname.Text = jobname;

            if(jobstatus=="wait")
            {
                btnJobRun.Enabled = true;
                btnJobStop.Enabled = false;
                btnJobRun.ForeColor = Color.Black;
                btnJobStop.ForeColor = Color.Gray;
            }
            else if (jobstatus == "run")
            {
                btnJobRun.Enabled = false;
                btnJobStop.Enabled = true;
                btnJobRun.ForeColor = Color.Gray;
                btnJobStop.ForeColor = Color.Black;
            }
        }


        private void btnJobRun_Click(object sender, EventArgs e)
        {
            try
            {
                //ros 연결후 
                if (Data.Instance.isConnected)
                {
                    int nrow = dataGridView_reg.SelectedCells[0].RowIndex;

                    if (nrow < 0 || nrow > dataGridView_reg.RowCount - 2) return;

                    string jobid = dataGridView_reg["jobid", nrow].Value.ToString();

                    string missionlist = dataGridView_reg["missionlist", nrow].Value.ToString();
                    string unloadmissionlist = dataGridView_reg["unloadmissionlist", nrow].Value.ToString();
                    string waitmissionlist = dataGridView_reg["waitmissionlist", nrow].Value.ToString();
                    string robotlist = dataGridView_reg["robotlist", nrow].Value.ToString();

                    string[] missionbuf = missionlist.Split(',');
                    string[] unloadmissionbuf = unloadmissionlist.Split(',');
                    string[] waitmissionbuf = waitmissionlist.Split(',');
                    string[] robotbuf = robotlist.Split(',');

                  

                    //job table 추가 
                    if (!mainform.Job_ScheduleTable_list.ContainsKey(jobid))
                    {
                        JobScheduleTable jobtable = new JobScheduleTable();
                        jobtable.beginmission = new List<Job_MissionInfo>();
                        jobtable.endmission = new List<Job_MissionInfo>();
                        jobtable.waitmission = new List<Job_MissionInfo>();
                        jobtable.robotid = new List<string>();
                        jobtable.bRobotuse = new List<bool>();
                        
                        for (int i = 0; i < robotbuf.Count(); i++)
                        {
                            jobtable.robotid.Add(robotbuf[i]);
                            jobtable.bRobotuse.Add(false);
                        }

                        jobtable.beginmission = onJobTable_Add(missionbuf);
                        jobtable.endmission = onJobTable_Add(unloadmissionbuf);
                        jobtable.waitmission = onJobTable_Add(waitmissionbuf);

                        mainform.Job_ScheduleTable_list.Add(jobid, jobtable);
                    }

                    //초기 로봇 작업 지정 함수
                    if (mainform.Job_ScheduleTable_list.ContainsKey(jobid))
                    {
                        if (mainform.Job_ScheduleTable_list[jobid].beginmission.Count > 0)
                        {
                            int m_cnt = mainform.Job_ScheduleTable_list[jobid].beginmission.Count;
                            for (int mission_idx = 0; mission_idx < m_cnt; mission_idx++)
                            {
                                if (mainform.Job_ScheduleTable_list[jobid].beginmission[mission_idx].actinfo[0].act_robotid == "" &&
                                    mainform.Job_ScheduleTable_list[jobid].beginmission[mission_idx].actinfo[0].acttype.Equals("Goal-Point"))
                                {
                                    int r_cnt = mainform.Job_ScheduleTable_list[jobid].robotid.Count;
                                    double dist_min = 0;
                                    string dist_min_robotid = "";
                                    int ndist_min_robotidx = 0;
                                    for (int i = 0; i < r_cnt; i++)
                                    {
                                        string robotid = mainform.Job_ScheduleTable_list[jobid].robotid[i];

                                        double xpos = mainform.Job_ScheduleTable_list[jobid].beginmission[0].actinfo[0].xpos;
                                        double ypos = mainform.Job_ScheduleTable_list[jobid].beginmission[0].actinfo[0].ypos;

                                        double r_xpos = Data.Instance.Robot_work_info[robotid].robot_status_info.robotstate.msg.pose.x;
                                        double r_ypos = Data.Instance.Robot_work_info[robotid].robot_status_info.robotstate.msg.pose.x;

                                        double dist = mainform.onPointToPointDist(xpos, ypos, r_xpos, r_ypos);

                                        if (i == 0)
                                        {
                                            dist_min = dist;
                                            dist_min_robotid = robotid;
                                            ndist_min_robotidx = i;
                                        }
                                        else
                                        {
                                            if (dist < dist_min)
                                            {
                                                dist_min = dist;
                                                dist_min_robotid = robotid;
                                                ndist_min_robotidx = i;
                                            }
                                        }
                                    }

                                    mainform.Job_ScheduleTable_list[jobid].beginmission[mission_idx].actinfo[0].act_robotid = dist_min_robotid;
                                    mainform.Job_ScheduleTable_list[jobid].bRobotuse[ndist_min_robotidx] = true;
                                    string missionid = mainform.Job_ScheduleTable_list[jobid].beginmission[mission_idx].missionid;
                                    //publish(missionid, actidx, act_robotid);
                                }
                                else
                                {

                                }
                            }

                            //남은 로봇이 있을경우 

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("btnJobRun_Click err=" + ex.Message.ToString());
            }
        }

       
        //job table 추가함수 
        private List<Job_MissionInfo> onJobTable_Add(string[] missionbuf)
        {
            List<Job_MissionInfo> jobmissionlist = new List<Job_MissionInfo>();
            try
            {
                if (missionbuf.Length > 0 && missionbuf[0] != "Not")
                {
                    for (int i = 0; i < missionbuf.Length; i++)
                    {
                        string missionid = missionbuf[i];
                        int idx = missionid.IndexOf("(");
                        int idx2 = missionid.IndexOf(")");
                        missionid = missionid.Substring(idx + 1, idx2 - idx - 1);

                        //해당되는 missionid에 action정보 read
                        Action_Data_List actiondatalist = onActionListRead(missionid);

                        if (actiondatalist.action_data.Count > 0)
                        {
                            Job_MissionInfo jobmission = new Job_MissionInfo();
                            jobmission.actinfo = new List<Job_Mission_ActInfo>();
                            jobmission.missionid = missionid;
                            Job_Mission_ActInfo jobmission_act = new Job_Mission_ActInfo();

                            for (int j = 0; j < actiondatalist.action_data.Count; j++)
                            {
                                if (j > 0)
                                {
                                    jobmission_act.actidx = j - 1;
                                    jobmission_act.acttype = actiondatalist.action_data[j].strActionType;

                                    if (jobmission_act.acttype.Equals("Action_wait"))
                                    {
                                        jobmission_act.bXis_use = true;
                                    }

                                    string[] strgoal_sub = actiondatalist.action_data[j].strWorkData.Split('/');

                                    if (jobmission_act.acttype.Equals("Goal-Point"))
                                    {
                                        string[] strgoal_sub_act_param = strgoal_sub[1].Split(':');
                                        string x = strgoal_sub_act_param[1];
                                        jobmission_act.xpos = float.Parse(x);
                                        strgoal_sub_act_param = strgoal_sub[2].Split(':');
                                        string y = strgoal_sub_act_param[1];
                                        jobmission_act.ypos = float.Parse(y);
                                    }

                                    jobmission_act.robot_status = "wait";

                                    jobmission.actinfo.Add(jobmission_act);
                                }
                            }

                            jobmissionlist.Add(jobmission);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("onJobTable_Add err=" + ex.Message.ToString());
            }

            return jobmissionlist;
        }
        


        private Action_Data_List onActionListRead(string strmissionid)
        {
            Action_Data_List actiondatalist = new Action_Data_List();
            actiondatalist.action_data = new List<Action_Data>();
            try
            {
                string strworkfile = "..\\Ros_info\\" + strmissionid + ".xml";
                using (StreamReader sr1 = new System.IO.StreamReader(strworkfile, Encoding.Default))
                {
          
                    while (sr1.Peek() >= 0)
                    {
                        string strTemp = sr1.ReadLine();

                        if (strTemp.IndexOf('<') < 0 && strTemp != "")
                        {
                            Action_Data actiondata = new Action_Data();

                            string[] straction_sub = strTemp.Split('/');

                            actiondata.strWorkData = strTemp;
                            if (straction_sub.Length == 1)
                                actiondata.strActionType = "";
                            else actiondata.strActionType = straction_sub[0].Split(':')[1];

                            actiondatalist.action_data.Add(actiondata);
                        }
                    }
                    sr1.Close();
                }
                return actiondatalist;
            }
            catch (Exception ex)
            {
                Console.WriteLine("onActionListRead err=" + ex.Message.ToString());

                return actiondatalist;
            }
        }

        private void btnJobStop_Click(object sender, EventArgs e)
        {

        }
    }
}
