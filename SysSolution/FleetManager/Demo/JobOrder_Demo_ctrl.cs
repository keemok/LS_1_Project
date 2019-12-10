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

namespace SysSolution.FleetManager.Demo
{
    public partial class JobOrder_Demo_ctrl : UserControl
    {
        public JobOrder_Demo_ctrl()
        {
            InitializeComponent();
        }

        FleetManager_MainForm mainform;
        int m_nJobcnt = 0;

        public JobOrder_Demo_ctrl(FleetManager.FleetManager_MainForm mainform)
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

                cboliftrobotID.SelectedIndex = 0;



                btnJobRun.BackColor = Color.Gray;
                btnJobStop.BackColor = Color.Gray;

            }
            catch (Exception ex)
            {
                Console.WriteLine("joborder_Ctrl ..onInitSet err" + ex.Message.ToString());
            }
        }

        private void JobOrder_ctrl_Load(object sender, EventArgs e)
        {
            mainform.worker.workresult_Evt += new Worker.WorkResultResponse(this.WorkResultResponse);
            mainform.worker.workfeedback_Evt += new Worker.WorkFeedbackResponse(this.WorkFeedbackResponse);

            lblRobotJobCnt.Text = "0";
            lblActionidx.Text = "0";
        }


        public void WorkResultResponse(string strrobotid)
        {
            try
            {
                timer1.Enabled = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("WorkResultResponse err=" + ex.Message.ToString());
            }
        }

        public void WorkFeedbackResponse(string strrobotid)
        {
            try
            {
                int nLoopcount = Data.Instance.Robot_work_info[strrobotid].robot_status_info.workfeedback.msg.feedback.loop_count;
                int nactionidx = Data.Instance.Robot_work_info[strrobotid].robot_status_info.workfeedback.msg.feedback.action_indx;

                Invoke(new MethodInvoker(delegate ()
                {

                    //lblActionidx.Text = string.Format("{0}", nactionidx);
                }));

                if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.workfeedback.msg.feedback.act_complete)
                {
                }

                if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.workfeedback.msg.feedback.mission_complete)
                {
                    m_nJobcnt++;
                    Invoke(new MethodInvoker(delegate ()
                    {
                        lblRobotJobCnt.Text = string.Format("{0}", m_nJobcnt);

                    }));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("WorkFeedbackResponse err=" + ex.Message.ToString());
            }
        }

        private void dataGridView_reg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtJobname.Text = "";

            int nrow = dataGridView_reg.SelectedCells[0].RowIndex;

            if (nrow < 0 || nrow > dataGridView_reg.RowCount - 2) return;

            string jobstatus = dataGridView_reg["jobstatus", nrow].Value.ToString();
            string jobname = dataGridView_reg["jobname", nrow].Value.ToString();

            txtJobname.Text = jobname;

            if (jobstatus == "wait")
            {
                btnJobRun.Enabled = true;
                btnJobStop.Enabled = false;
                btnJobRun.ForeColor = Color.Black;
                btnJobRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
                btnJobStop.ForeColor = Color.Gray;
                btnJobStop.BackColor = Color.Gray;
            }
            else if (jobstatus == "run")
            {
                btnJobRun.Enabled = false;
                btnJobStop.Enabled = true;
                btnJobRun.ForeColor = Color.Gray;
                btnJobRun.BackColor = Color.Gray;
                btnJobStop.ForeColor = Color.Black;
                btnJobStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            }
        }

        DateTime Starttime;
        DateTime Currtime;
        private void btnJobRun_Click(object sender, EventArgs e)
        {
            try
            {
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

                    int njobcnt = int.Parse(txtJobCnt.Text.ToString());
                    string strMsg = "";
                    for (int i = 0; i < robotbuf.Length; i++)
                    {
                        string strrobotcnt = string.Format("{0}", njobcnt);

                        strMsg += string.Format("{0},반복({1}) ", robotbuf[i], strrobotcnt);
                    }

                    strMsg += " 동작 하시겠습니까?";

                    if (DialogResult.OK == MessageBox.Show(strMsg, "확인", MessageBoxButtons.OKCancel))
                    {
                        //test cj 데모 
                        onCjDemo_Run(robotbuf, missionbuf);
                        {
                            //db 정보 갱신
                            mainform.onDBUpdate_Joblist_status(jobid, "run");
                            onInitSet();
                        }
                        timer1.Interval = 500;
                        timer1.Enabled = true;
                        Starttime = DateTime.Now;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("btnJobRun_Click err=" + ex.Message.ToString());
            }
        }

        private async void onCjDemo_Run(string[] robotlist_buf, string[] missionbuf)
        {
            try
            {
                int nrobottotalcnt = robotlist_buf.Length;
                int njobcnt = int.Parse(txtJobCnt.Text.ToString());

                for (int nrobotcnt = 0; nrobotcnt < nrobottotalcnt; nrobotcnt++)
                {

                    string strworkfile = "";
                    string strworkid = "";

                    List<string> strSelectRobot = new List<string>();
                    List<string> strworkdata = new List<string>();
                    string[] strSelectworkdata_Worker;

                    int workcnt = njobcnt;

                    int idx = robotlist_buf[nrobotcnt].IndexOf("(");
                    int idx2 = robotlist_buf[nrobotcnt].IndexOf(")");

                    string strRobot = robotlist_buf[nrobotcnt].Substring(idx + 1, idx2 - idx - 1);

                    idx = missionbuf[0].IndexOf("(");
                    idx2 = missionbuf[0].IndexOf(")");

                    string strMissionID = missionbuf[0].Substring(idx + 1, idx2 - idx - 1);

                    string strRobotName = "";
                    string strMissionName = "";

                    int nactidx = 0;

                    strworkfile = strMissionID;

                    strworkid = "<" + strworkfile + ">";

                    strworkfile = "..\\Ros_info\\" + strworkfile + ".xml";

                    strworkdata.Clear();

                    using (StreamReader sr1 = new System.IO.StreamReader(strworkfile, Encoding.Default))
                    {
                        while (sr1.Peek() >= 0)
                        {
                            string strTemp = sr1.ReadLine();

                            if (strTemp.IndexOf('<') < 0 && strTemp != "")
                            {
                                strworkdata.Add(strTemp);
                            }
                        }
                        sr1.Close();
                    }

                    Invoke(new MethodInvoker(delegate ()
                    {
                        if (strworkdata.Count < 0)
                        {
                            MessageBox.Show("작업 내용이 존재하지 않습니다.");
                            return;
                        }
                    }));


                    strSelectworkdata_Worker = new string[strworkdata.Count - 1];

                    string strworkname = strworkdata[0];
                    for (int i = 1; i < strworkdata.Count; i++)
                    {
                        strSelectworkdata_Worker[i - 1] = strworkdata[i];
                    }
                    int nworkcnt = workcnt;

                    var task = Task.Run(() => mainform.worker.onFleetManager_WorkOrder_publish(strworkname, strworkid, strRobot, strSelectworkdata_Worker, nworkcnt, nactidx));
                    await task;

                    Thread.Sleep(100);

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("onCjDemo_Run err=" + ex.Message.ToString());
            }

        }

        private void btnJobStop_Click(object sender, EventArgs e)
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

                    onCjDemo_Stop(robotbuf);
                    {
                        //db 정보 갱신
                        mainform.onDBUpdate_Joblist_status(jobid, "wait");
                        onInitSet();
                    }

                    timer1.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("btnJobStop_Click err=" + ex.Message.ToString());
            }
        }

        private async void onCjDemo_Stop(string[] robotlist_buf)
        {
            try
            {
                int nrobottotalcnt = robotlist_buf.Length;

                for (int nrobotcnt = 0; nrobotcnt < nrobottotalcnt; nrobotcnt++)
                {
                    int idx = robotlist_buf[nrobotcnt].IndexOf("(");
                    int idx2 = robotlist_buf[nrobotcnt].IndexOf(")");

                    string strRobot = robotlist_buf[nrobotcnt].Substring(idx + 1, idx2 - idx - 1);

                    var task = Task.Run(() => mainform.worker.onWorkCancel_publish(strRobot, ""));

                    Thread.Sleep(100);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("onCjDemo_Stop err=" + ex.Message.ToString());
            }

        }

        private void btnLiftSet_Click(object sender, EventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    string strrobotid = "";
                    strrobotid = cboliftrobotID.SelectedItem.ToString();

                    int nlift = cboRobot_lift.SelectedIndex;

                    LiftStatus_Set_Arg lift = new LiftStatus_Set_Arg();

                    if (nlift == 0)
                    {
                        lift.data = 2;
                    }
                    else if (nlift == 1)
                    {
                        lift.data = -2;
                    }
                    string strobj = JsonConvert.SerializeObject(lift);
                    JObject obj = JObject.Parse(strobj);

                    rosinterface ros = new rosinterface();

                    TopicList list = new TopicList();
                    ros.PublisherTopicMsgtype(string.Format("{0}", strrobotid) + list.topic_set_liftstatus, list.msg_set_liftstatus);
                    Thread.Sleep(100);
                    ros.publisher(obj);
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("btnLiftSet_Click err :={0}", ex.Message.ToString());
            }
        }

        private void btnliftup_Click(object sender, EventArgs e)
        {
            string strrobotid = "";
            strrobotid = cboliftrobotID.SelectedItem.ToString();
            onLiftcontorl(strrobotid, "Top", btnliftup);
        }

        private void btnliftdown_Click(object sender, EventArgs e)
        {
            string strrobotid = "";
            strrobotid = cboliftrobotID.SelectedItem.ToString();
            onLiftcontorl(strrobotid, "Bottom", btnliftdown);
        }

        private void onLiftcontorl(string strrobotid, string strlift, Button btn)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    string[] strSelectRobot_Worker; //워크작업으로 전달하기 위해 형식 맞춤.. 
                    string[] strSelectworkdata_Worker;
                    string strworkname = strlift;
                    //string strworkid = strrobotid+"/"+strlift;
                    string strworkid = strlift;
                    int nworkcnt = 1;

                    strSelectRobot_Worker = new string[1];
                    strSelectworkdata_Worker = new string[1];
                    strSelectRobot_Worker[0] = strrobotid;
                    string strmsg = "";
                    if (strlift == "Bottom")
                        strmsg = "type:Lift-Conveyor-Control/mode:Top-Bottom/action:Bottom";
                    else if (strlift == "Top")
                        strmsg = "type:Lift-Conveyor-Control/mode:Top-Bottom/action:Top";
                    //strSelectworkdata_Worker[0] = "UR assembly test";
                    strSelectworkdata_Worker[0] = strmsg;

                    int nactidx = 0;
                    var task = Task.Run(() => mainform.worker.onFleetManager_WorkOrder_publish(strworkname, strworkid, strrobotid, strSelectworkdata_Worker, nworkcnt, nactidx));

                    // onbtnDelay(btn, 300);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("btnAssembly_Click err :={0}", ex.Message.ToString());
            }
        }

        private void btnAllStop_Click(object sender, EventArgs e)
        {
            int nrobotcnt = mainform.Robot_RegInfo_list.Count;

            if (nrobotcnt > 0)
            {
                for (int i = 0; i < nrobotcnt; i++)
                {
                    Robot_RegInfo robotreginfo = mainform.Robot_RegInfo_list.ElementAt(i).Value;

                    var task = Task.Run(() => mainform.worker.onWorkCancel_publish(robotreginfo.robot_id, ""));

                    Thread.Sleep(100);
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                TimeSpan result = DateTime.Now - Starttime;
                lblActionidx.Text = string.Format("{0}m{1}sec", result.Minutes, result.Seconds);
            }));



        }

        List<CrashCheck_RobotInfo_Class> crashcheck_robottable = new List<CrashCheck_RobotInfo_Class>();
        Thread crashcheck_thread;
        bool bcrashcheck = false;

        private void chkCrashStop_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    if (chkCrashStop.Checked)
                    {

                        int nrobotcnt = mainform.G_robotList.Count;
                        for (int i = 0; i < nrobotcnt; i++)
                        {
                            CrashCheck_RobotInfo_Class crashcheck_robot = new CrashCheck_RobotInfo_Class();
                            crashcheck_robot.strRobotid = mainform.G_robotList[i];
                            // if(crashcheck_robottable.con)

                            string strcheckrobot = mainform.G_robotList[i];
                            if (Data.Instance.Robot_work_info.ContainsKey(strcheckrobot))
                            {
                                if (Data.Instance.Robot_work_info[strcheckrobot].robot_status_info.goalrunnigstatus.msg == null || Data.Instance.Robot_work_info[strcheckrobot].robot_status_info.goalrunnigstatus.msg == null)
                                {

                                }
                                else
                                {
                                    double x = Data.Instance.Robot_work_info[strcheckrobot].robot_status_info.robotstate.msg.pose.x;
                                    double y = Data.Instance.Robot_work_info[strcheckrobot].robot_status_info.robotstate.msg.pose.y;
                                    double look_x = Data.Instance.Robot_work_info[strcheckrobot].robot_status_info.lookahead.msg.point.x;
                                    double look_y = Data.Instance.Robot_work_info[strcheckrobot].robot_status_info.lookahead.msg.point.y;
                                }
                            }
                        }


                        if (crashcheck_thread != null)
                        {
                            crashcheck_thread.Abort();
                            crashcheck_thread = null;
                        }

                        bcrashcheck = true;
                        crashcheck_thread = new Thread(crashcheck_thread_func);
                        crashcheck_thread.Start();
                    }
                    else
                    {
                        if (crashcheck_thread != null)
                        {
                            bcrashcheck = false;
                            crashcheck_thread.Abort();
                            crashcheck_thread = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("chkCrashStop_CheckedChanged err :={0}", ex.Message.ToString());
            }
        }



        private void crashcheck_thread_func()
        {
            try
            {
                for (; ; )
                {
                    if (bcrashcheck)
                    {

                    }
                    else
                    {
                        break;
                    }
                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("cam_thread_func err :={0}", ex.Message.ToString());
            }
        }

        private async void btnPause_Click(object sender, EventArgs e)
        {
            try
            {
                //ros 연결후 
                if (Data.Instance.isConnected)
                {

                    string strrobotid = cboRobotID.SelectedItem.ToString();
                    var task = Task.Run(() => mainform.worker.onWorkPause_publish(strrobotid, ""));

                    Thread.Sleep(100);


                    timer1.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("btnJobStop_Click err=" + ex.Message.ToString());
            }
        }

        private async void btnRestart_Click(object sender, EventArgs e)
        {
            try
            {
                //ros 연결후 
                if (Data.Instance.isConnected)
                {

                    string strrobotid = cboRobotID.SelectedItem.ToString();
                    var task = Task.Run(() => mainform.worker.onWorkResume_publish(strrobotid, ""));

                    Thread.Sleep(100);


                    timer1.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("btnRestart_Click err=" + ex.Message.ToString());
            }
        }
    
        
    }

    public class CrashCheck_RobotInfo_Class
    {
        public string strRobotid;
        public double pose_x;
        public double pose_y;
        public double lookahead_x;
        public double lookahead_y;
        public int nPriority;
        public string strStatus;
    }
}
