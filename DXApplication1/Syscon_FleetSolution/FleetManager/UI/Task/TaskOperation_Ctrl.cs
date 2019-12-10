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



namespace Syscon_Solution.FleetManager.UI.Task1
{
    public partial class TaskOperation_Ctrl : UserControl
    {



        Fleet_Main mainform;

        public TaskOperation_Ctrl()
        {
            InitializeComponent();
        }

        public TaskOperation_Ctrl(Fleet_Main frm)
        {
            mainform = frm;
            InitializeComponent();
        }

        public void onInitSet()
        {
            try
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    //로봇 작업그룹 읽기 -> 콤보박스 표시
                    mainform.dbBridge.onDBRead_RobotGrouplist();

                    mainform.dbBridge.onDBRead_Tasklist();

                    dataGridView_reg.Rows.Clear();

                    //grid dp
                    int cnt = Data.Instance.Task_list.Count();
                    for (int i = 0; i < cnt; i++)
                    {
                        string strtmp = "";

                        Task_Info taskinfo = Data.Instance.Task_list.ElementAt(i).Value;

                        string task_status = taskinfo.task_status;

                        if (task_status == "") task_status = "wait";

                        string task_id = taskinfo.task_id;
                        string task_name = taskinfo.task_name;
                        string task_missionlist = taskinfo.mission_id_list;
                        string task_robotlist = taskinfo.robot_id_list;
                        string taskloopflag = taskinfo.taskloopflag;
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

                    btnTaskRun1.Enabled = false;
                    btnTaskStop1.Enabled = false;

                    btnTaskRun1.BackColor = Color.Gray;
                    btnTaskStop1.BackColor = Color.Gray;

                    Invalidate();
                }));

            }
            catch (Exception ex)
            {
                Console.WriteLine("TaskOperation_Ctrl ..onInitSet err" + ex.Message.ToString());
            }
        }

        private void TaskOperation_Ctrl_Load(object sender, EventArgs e)
        {
            Data.Instance.bWaitPos_Run = false;
            Data.Instance.bRunToWaitPos = false;

            //mainform.commBridge.Waitpos_evt += new Comm.Comm_bridge.WaitPos_Complete(this.WaitPos_Complete);

            //   timer_CrashCheck.Interval = 50;
            //   timer_CrashCheck.Enabled = true;

         



            //   mainform.commBridge.taskfeedback_Evt += new Comm.Comm_bridge.TaskFeedbackResponse(this.TaskFeedback_Complete);
        }

    

        private void dataGridView_reg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTaskname.Text = "";

            int nrow = dataGridView_reg.SelectedCells[0].RowIndex;

            if (nrow < 0 || nrow > dataGridView_reg.RowCount - 2) return;

            string jobstatus = dataGridView_reg["taskstatus", nrow].Value.ToString();
            string jobname = dataGridView_reg["taskname", nrow].Value.ToString();

            string robotlist = dataGridView_reg["robotlist", nrow].Value.ToString();

            //txtLineSelectRobot.Text = robotlist;

            txtTaskname.Text = jobname;

            if (jobstatus == "wait")
            {
                btnTaskRun1.Enabled = true;
                btnTaskStop1.Enabled = false;
                btnTaskRun1.ForeColor = Color.Black;
                btnTaskRun1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
                btnTaskStop1.ForeColor = Color.Gray;
                btnTaskStop1.BackColor = Color.Gray;
            }
            else if (jobstatus == "run")
            {
                btnTaskRun1.Enabled = false;
                btnTaskStop1.Enabled = true;
                btnTaskRun1.ForeColor = Color.Gray;
                btnTaskRun1.BackColor = Color.Gray;
                btnTaskStop1.ForeColor = Color.Black;
                btnTaskStop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            }
        }

        private async void btnTaskRun_Click(object sender, EventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    int nrow = dataGridView_reg.SelectedCells[0].RowIndex;

                    if (nrow < 0 || nrow > dataGridView_reg.RowCount - 2) return;

                    string taskid = dataGridView_reg["taskid", nrow].Value.ToString();
                    string taskname = dataGridView_reg["taskname", nrow].Value.ToString();
                    string missionlist = dataGridView_reg["missionlist", nrow].Value.ToString();
                    string robotlist = dataGridView_reg["robotlist", nrow].Value.ToString();

                    string[] missionbuf = missionlist.Split(',');
                    string[] robotbuf = robotlist.Split(',');

                    int ntaskcnt = int.Parse(txtTaskCnt.Text.ToString());
                    string strMsg = "";
                    for (int i = 0; i < robotbuf.Length; i++)
                    {
                        string strrobotcnt = string.Format("{0}", ntaskcnt);

                        strMsg += string.Format("{0},반복({1}) ", robotbuf[i], strrobotcnt);
                    }

                    strMsg += " 동작 하시겠습니까?";

                    if (DialogResult.OK == MessageBox.Show(strMsg, "확인", MessageBoxButtons.OKCancel))
                    {
                        int ntaskcnt1 = int.Parse(txtTaskCnt.Text.ToString());
                        //RIDiS에 task order 전달 
                        onTaskOrder(taskid, missionbuf, taskname, missionlist,robotlist, ntaskcnt1);
                        Thread.Sleep(100);
                        {
                            //db task  table 정보 갱신
                            mainform.dbBridge.onDBUpdate_Tasklist_status(taskid, "run");
                            onInitSet();

                            //db task operation table 정보 갱신 => Task 쓰레드에서 실시간 갱신 
                            for (int i2 = 0; i2 < robotbuf.Length; i2++)
                            {
                                mainform.dbBridge.onDBSave_TaskOperation(taskid, robotbuf[i2], missionlist, "", 0, "insert");
                            }

                            //task 쓰레드가 존재하는지 파악후 구동
                            int threadcnt = Data.Instance.TaskCheck_threadList.Count;
                            if(threadcnt>0)
                            {
                                int thridx = 0;
                                for (int i = 0; i < threadcnt; i++)
                                {
                                    TaskCheck_class taskclass = Data.Instance.TaskCheck_threadList.ElementAt(i);
                                    if (taskclass.strTaskid == taskid)
                                    {
                                        if (taskclass.taskthred != null)
                                        {
                                            taskclass.taskthred.Abort();
                                            taskclass.taskthred = null;
                                        }

                                        Data.Instance.TaskCheck_threadList.RemoveAt(i);
                                    }
                                }
                                
                            }


                            { 
                                //task에 잇는 로봇들이 task 종료를 체크하기 위해
                                TaskCheck_class taskclass = new TaskCheck_class();
                                taskclass.strTaskid = taskid;
                                taskclass.taskfinish_Evt += new TaskCheck_class.TaskFinished(mainform.TaskFinished);

                                taskclass.taskthred = new Thread(taskclass.taskCheck_thread_func);
                                Task_checkThread_TableInfo task_checkinfo = new Task_checkThread_TableInfo();
                                task_checkinfo.taskcheck_info = new List<Task_checkThread_Info>();

                                for (int i2 = 0; i2 < robotbuf.Length; i2++)
                                {
                                    Task_checkThread_Info task_info = new Task_checkThread_Info();
                                    task_info.strrobotid = robotbuf[i2];

                                    task_checkinfo.taskcheck_info.Add(task_info);
                                }

                                Data.Instance.TaskCheck_threadList.Add(taskclass);
                                taskclass.taskthred.Start(task_checkinfo);
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("btnTaskRun_Click err=" + ex.Message.ToString());
            }
        }

        private async void onTaskOrder(string strTaskid, string[] missionbuf, string taskname, string strMissionid_list, string strRobotlist,int ntaskcnt)
        {
            try
            {
                Task_Order taskorder = new Task_Order();
                int cnt = missionbuf.Length;
                List<MisssionInfo> missioninfo_list = new List<MisssionInfo>();
                for (int i = 0; i < cnt; i++)
                {
                    MisssionInfo missioninfo = mainform.dbBridge.onDBRead_Mission(missionbuf[i]);

                    missioninfo_list.Add(missioninfo);
                }

                taskorder.task_id = strTaskid;
                taskorder.loop_flag = ntaskcnt;
                taskorder.missionlist = strMissionid_list;
                taskorder.robotlist = strRobotlist;

               mainform.commBridge.onTaskrder_publish(taskorder, taskname, missioninfo_list);

               // var task = Task.Run(() => mainform.commBridge.onTaskrder_publish(taskorder, taskname, missioninfo_list));
               // await task;
            }
            catch (Exception ex)
            {
                Console.WriteLine("onTaskOrder err=" + ex.Message.ToString());
            }
        }

        private void btnTaskStop_Click(object sender, EventArgs e)
        {
            try
            {
                //ros 연결후 
                if (Data.Instance.isConnected)
                {
                    int nrow = dataGridView_reg.SelectedCells[0].RowIndex;

                    if (nrow < 0 || nrow > dataGridView_reg.RowCount - 2) return;

                    string taskid = dataGridView_reg["taskid", nrow].Value.ToString();

                    string missionlist = dataGridView_reg["missionlist", nrow].Value.ToString();
                    string robotlist = dataGridView_reg["robotlist", nrow].Value.ToString();

                    string[] missionbuf = missionlist.Split(',');
                    string[] robotbuf = robotlist.Split(',');

                    onTaskCancel(robotbuf);
                    {
                        //db 정보 갱신
                        mainform.dbBridge.onDBUpdate_Tasklist_status(taskid, "wait");

                        //task operation db 삭제 
                        for (int i2 = 0; i2 < robotbuf.Length; i2++)
                        {
                            mainform.dbBridge.onDBDelete_TaskOperation(taskid, robotbuf[i2]);
                        }

                        //쓰레드 삭제
                        int threadcnt = Data.Instance.TaskCheck_threadList.Count;
                        if (threadcnt > 0)
                        {
                            int thridx = 0;
                            TaskCheck_class taskclass = Data.Instance.TaskCheck_threadList.ElementAt(thridx);

                            if(taskclass.strTaskid == taskid)
                            {
                                if(taskclass.taskthred!=null)
                                {
                                    taskclass.taskthred.Abort();
                                    taskclass = null;
                                }

                                Data.Instance.TaskCheck_threadList.RemoveAt(thridx);
                            }
                        }

                        onInitSet();
                    }

                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("btnTaskStop_Click err=" + ex.Message.ToString());
            }
        }

        private async void onTaskCancel(string[] robotbuf)
        {
            try
            {
                int nrobottotalcnt = robotbuf.Length;

                for (int nrobotcnt = 0; nrobotcnt < nrobottotalcnt; nrobotcnt++)
                {

                    string strRobot = robotbuf[nrobotcnt];
                    var task = Task.Run(() => mainform.commBridge.onTaskCancel_publish(strRobot, ""));
                    await task;

                    Thread.Sleep(100);

                    Console.WriteLine("cancel = {0}" + strRobot);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("onTaskCancel err=" + ex.Message.ToString());
            }
        }

       // string[] strWaitPos_MissionID = {"MID20190704111900","MID20190704111912","MID20190704111918","MID20190704111942" };
        string[] strLineDrive_MissionID = { "MID20190712143725", "MID20190712144134" };

        private void btnWaitPos_Click(object sender, EventArgs e)
        {
            Data.Instance.nWaitPos_Robot_idx = 0;
            Data.Instance.bWaitPos_Run = false;
            Data.Instance.bRunToWaitPos = false;
            try
            {
                List<string> moveToWaitPos_robot = new List<string>();

                if(!chk500_1.Checked && !chk500_2.Checked && !chk300D.Checked && !chk1000_1.Checked && !chk1000_2.Checked)
                {
                    MessageBox.Show("대기장소로 이동할 로봇을 선택하세요.");
                    return;
                }

                if (chk500_1.Checked)
                {
                    moveToWaitPos_robot.Add("R_002");
                }

                if (chk500_2.Checked)
                {
                    moveToWaitPos_robot.Add("R_001");
                }

                if (chk1000_1.Checked)
                {
                    moveToWaitPos_robot.Add("R_005");
                }

                if (chk1000_2.Checked)
                {
                    moveToWaitPos_robot.Add("R_006");
                }

                if (chk300D.Checked)
                {
                    moveToWaitPos_robot.Add("R_003");
                }

                onMoveToWaitPos(moveToWaitPos_robot);

                Data.Instance.bTaskRun = true;

                DateTime dt = DateTime.Now;
                Data.Instance.strTaskRun_StartTime = string.Format("{0:d4}{1:d2}{2:d2}{3:d2}{4:d2}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute);
            }
            catch (Exception ex)
            {
                Console.WriteLine("btnWaitPos_Click err=" + ex.Message.ToString());
            }

        }

       
        List<string> StartRobot_Skip = new List<string>();
       
        string strCurrStartRobot = "";


        #region waitpos move ver2 
        //대기장소1,대기장소2,대기장소3,대기장소4,대기장소5,대기장소6,대기장소7 
        string[] strWaitPos_MissionID = { "MID201908wait11", "MID201908wait12", "MID201908wait13", "MID201908wait14", "MID201908wait15", "MID201908wait16", "MID201908wait17" };
        string[] strWaitPos_MissionID_one = { "MID201908wait1", "MID201908wait2", "MID201908wait3", "MID201908wait4", "MID201908wait5", "MID201908wait6", "MID201908wait7" };

        List<WaitPos_List> waitpos_xy = new List<WaitPos_List>();

        Dictionary<string, string> strWaitPosMove_Robot = new Dictionary<string, string>();

        Dictionary<string, string> strWaitPosMove_Robot_one = new Dictionary<string, string>();

        double dPoint1 = 4.5;
        double dPoint1_y = -4.5;
        double dPoint2_x = 7.14;
        double dPoint2_y = 8.8;

        private void btnWaitPos2_Click(object sender, EventArgs e)
        {
            onWaitPos_Ver2_Start();
        }

        private void onWaitPos_Ver2_Start()
        {
            Data.Instance.nWaitPos_Robot_idx = 0;
            Data.Instance.bWaitPos_Run = false;
            Data.Instance.bRunToWaitPos = false;
            try
            {
                List<string> moveToWaitPos_robot = new List<string>();

                if (!chk500_1.Checked && !chk500_2.Checked && !chk300D.Checked && !chk1000_1.Checked && !chk1000_2.Checked && !chk100M.Checked)
                {
                    MessageBox.Show("대기장소로 이동할 로봇을 선택하세요.");
                    return;
                }

                if(chk200P.Checked)
                {
                    moveToWaitPos_robot.Add("R_000");
                }

                if (chk500_2.Checked)
                {
                    moveToWaitPos_robot.Add("R_001");
                }

                if (chk500_1.Checked)
                {
                    moveToWaitPos_robot.Add("R_002");
                }
                
                if (chk300D.Checked)
                {
                    moveToWaitPos_robot.Add("R_003");
                }

                if (chk100M.Checked)
                {
                    moveToWaitPos_robot.Add("R_004");
                }

                if (chk1000_1.Checked)
                {
                    moveToWaitPos_robot.Add("R_005");
                }

                if (chk1000_2.Checked)
                {
                    moveToWaitPos_robot.Add("R_006");
                }

               

                onMoveToWaitPos(moveToWaitPos_robot);

                Data.Instance.bTaskRun = true;

                DateTime dt = DateTime.Now;
                Data.Instance.strTaskRun_StartTime = string.Format("{0:d4}{1:d2}{2:d2}{3:d2}{4:d2}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute);
            }
            catch (Exception ex)
            {
                Console.WriteLine("onWaitPos_Ver2_Start err=" + ex.Message.ToString());
            }
        }

        private void onMoveToWaitPos(List<string> moveToWaitPos_robot)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    mainform.dbBridge.onDBRead_Missionlist();

                    int nmissioncnt = Data.Instance.missionlisttable.missioninfo.Count;
                    if (nmissioncnt > 0)
                    {
                        waitpos_xy.Clear();
                        txtWaitpos1.Text = "";
                        txtWaitpos2.Text = "";
                        txtWaitpos3.Text = "";
                        txtWaitpos4.Text = "";
                        txtWaitpos5.Text = "";
                        txtWaitpos6.Text = "";
                        txtWaitpos7.Text = "";

                        //대기장소 위치 db에서 읽어서 저장
                        for (int i1 = 0; i1 < strWaitPos_MissionID_one.Length; i1++)
                        {
                            for (int i2 = 0; i2 < nmissioncnt; i2++)
                            {
                                if (strWaitPos_MissionID_one[i1] == Data.Instance.missionlisttable.missioninfo[i2].strMisssionID)
                                {
                                    string strwork = Data.Instance.missionlisttable.missioninfo[i2].work;
                                    WorkFlowGoal db_missiondata = new WorkFlowGoal();
                                    db_missiondata = JsonConvert.DeserializeObject<WorkFlowGoal>(strwork);

                                    float x = db_missiondata.work[0].action_args[0];
                                    float y = db_missiondata.work[0].action_args[1];
                                    float th = db_missiondata.work[0].action_args[2];

                                    WaitPos_List waitposlist = new WaitPos_List();
                                    waitposlist.wait_x = x;
                                    waitposlist.wait_y = y;
                                    waitposlist.wait_theta = th;

                                    waitpos_xy.Add(waitposlist);

                                    break;
                                }
                            }
                        }

                        
                        //대기장소 1부터.. 로봇이 대기장소와 가장 가까운 로봇 를 [대기장소 id , 로봇 id]배열로 저장 
                        int nwaitposcnt = waitpos_xy.Count;
                        int nrobotcnt = moveToWaitPos_robot.Count;


                        if (nwaitposcnt > 0)
                        {
                            strWaitPosMove_Robot.Clear();
                            strWaitPosMove_Robot_one.Clear();

                            for (int i3 = 0; i3 < nwaitposcnt; i3++)
                            {
                                double dist_min = 99999;

                                strWaitPosMove_Robot_one.Add(strWaitPos_MissionID_one[i3], "");

                                strWaitPosMove_Robot.Add(strWaitPos_MissionID[i3], "");

                                for (int nrobotidx = 0; nrobotidx < nrobotcnt; nrobotidx++)
                                {
                                    if (strWaitPosMove_Robot_one.ContainsValue(moveToWaitPos_robot[nrobotidx]))
                                    {
                                        continue;
                                    }

                                    if (Data.Instance.Robot_work_info[moveToWaitPos_robot[nrobotidx]].robot_status_info.robotstate != null)
                                    {
                                        if (Data.Instance.Robot_work_info[moveToWaitPos_robot[nrobotidx]].robot_status_info.robotstate.msg != null)
                                        {
                                            float robotx = (float)Data.Instance.Robot_work_info[moveToWaitPos_robot[nrobotidx]].robot_status_info.robotstate.msg.pose.x;
                                            float roboty = (float)Data.Instance.Robot_work_info[moveToWaitPos_robot[nrobotidx]].robot_status_info.robotstate.msg.pose.y;
                                            float robottheta = (float)Data.Instance.Robot_work_info[moveToWaitPos_robot[nrobotidx]].robot_status_info.robotstate.msg.pose.theta;

                                            double disttmp = mainform.onPointToPointDist(waitpos_xy[i3].wait_x, waitpos_xy[i3].wait_y, robotx, roboty);
                                            double disttmp2 = mainform.onPointToPointDist(dPoint2_x, dPoint2_y, robotx, roboty);

                                            if (robotx > dPoint1 && roboty > dPoint1_y)
                                                disttmp += disttmp2;


                                            if (dist_min > disttmp)
                                            {
                                                dist_min = disttmp;

                                                strWaitPosMove_Robot[strWaitPos_MissionID[i3]] = moveToWaitPos_robot[nrobotidx];

                                                strWaitPosMove_Robot_one[strWaitPos_MissionID_one[i3]] = moveToWaitPos_robot[nrobotidx];
                                            }
                                        }
                                    }
                                }
                            }

                            //대기장소 이동 flag ON
                            Data.Instance.bWaitPos_Run = true;

                            for (int nidx2 = 0; nidx2 < strWaitPosMove_Robot_one.Count; nidx2++)
                            {
                                KeyValuePair<string, string> waitposmove_robotinfo = strWaitPosMove_Robot_one.ElementAt(nidx2);
                                string strmissionid = waitposmove_robotinfo.Key;
                                string strrobotid = waitposmove_robotinfo.Value;

                                if (nidx2 == 0)
                                    txtWaitpos1.Text = strrobotid;
                                if (nidx2 == 1)
                                    txtWaitpos2.Text = strrobotid;
                                if (nidx2 == 2)
                                    txtWaitpos3.Text = strrobotid;
                                if (nidx2 == 3)
                                    txtWaitpos4.Text = strrobotid;
                                if (nidx2 == 4)
                                    txtWaitpos5.Text = strrobotid;
                                if (nidx2 == 5)
                                    txtWaitpos6.Text = strrobotid;
                                if (nidx2 == 6)
                                    txtWaitpos7.Text = strrobotid;
                            }
                        }

                    }


                    if (Data.Instance.bWaitPos_Run)
                    {
                        onMoveToWaitPos_publish(0);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("onMoveToWaitPos err=" + ex.Message.ToString());
            }
        }

        private void onMoveToWaitPos_publish(int robotidx)
        {
            try
            {
                if (robotidx > strWaitPosMove_Robot_one.Count - 1)
                {
                    Data.Instance.nWaitPos_Robot_idx = 0;
                    Data.Instance.bWaitPos_Run = false;
                    Data.Instance.bTaskRun = false;
                    MessageBox.Show("대기장소 이동 완료-1");
                }
                else
                {
                    KeyValuePair<string, string> waitposmove_robotinfo = strWaitPosMove_Robot_one.ElementAt(robotidx);
                    string strmissionid = waitposmove_robotinfo.Key;
                    string strrobotid = waitposmove_robotinfo.Value;

                    if (strrobotid == "")
                    {
                        {
                            Data.Instance.nWaitPos_Robot_idx = 0;
                            Data.Instance.bWaitPos_Run = false;
                            MessageBox.Show("대기장소 이동 완료-2");
                            return;
                        }
                    }


                    string[] missionbuf = strmissionid.Split(',');

                    onTaskOrder("taskwait", missionbuf, "taskwait", strmissionid, strrobotid, 1);
                    Console.WriteLine("대기장소 ={0},robot={1}", strmissionid, strrobotid);
                    onConsolemsgDp(string.Format("대기장소 ={0},robot={1}", strmissionid, strrobotid));
                    //Thread.Sleep(1500);
                    onTaskResum(strrobotid);

                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("onMoveToWaitPos_publish err=" + ex.Message.ToString());
            }
        }

        private void WaitPos_Complete(string strrobotid)
        {
            if (Data.Instance.bWaitPos_Run)
            {
                Data.Instance.nWaitPos_Robot_idx++;
                onMoveToWaitPos_publish(Data.Instance.nWaitPos_Robot_idx);
            }
            else if(Data.Instance.bRunToWaitPos)
            {
                //if (ncurrTaskRobotid_idx >= currTaskRobotid.Length - 1)
                //{
                //    ncurrTaskRobotid_idx = 0;
                //    Data.Instance.bRunToWaitPos = false;
                //    MessageBox.Show("대기장소로 복귀완료.");
                //}
            }
        }

        #endregion

        #region demo run ver2

        bool bDemorun_ver2 = false;

        string strMissionid_0 = "";

        public void onConsolemsgDp(string strmsg)
        {
            Invoke(new MethodInvoker(delegate ()
            {
               listBox6.Items.Add(strmsg);
               listBox6.SelectedIndex = listBox6.Items.Count - 1;
            }));
        }

        private void btnDemoRun_Click(object sender, EventArgs e)
        {
            //  bLineChk = false;

            strDriveOnly_robotid = "R_999";
            strDriveOnly_robotid2 = "R_999";
            strDriveOnly_robotid3 = "R_999";

            strline1_robotid = "R_099";
            strline2_robotid = "R_099";

            strlineonly_robotid = "R_999";


            strSonly_robotid = "R_099";

            strLiftonly_robotid = "R_999";

            strURonly_robotid = "R_999";

            strDocknly_robotid = "R_999";

            if (!chkDemo.Checked)
                onDemoRun2();
            else
            {
                listBox6.Items.Clear();
                bSdrive1_waitok = false;
                bSdrive2_waitok = false;

                onDemoRun_ver2();

                
            }

        }

        string[] currTaskRobotid = new string[100];
        int[] nCurrTaskRobot_workcnt = new int[100];
        string[] currTaskMissionList = new string[100];

        Dictionary<string, Robot_Demo_Info> robot_demoinfo = new Dictionary<string, Robot_Demo_Info>();

        int n1_workcnt = 0;
        int n2_workcnt = 0;
        int n3_workcnt = 0;
        int n4_workcnt = 0;
        int n5_workcnt = 0;
        int n6_workcnt = 0;
        int n7_workcnt = 0;


        string strStartMissionid = "";
        int ncurrTaskRobotid_idx = 0;
        int ncurrWaitRobotid_idx = 0;


        int nwaitpos_To_demorun_Currentrobotidx = 0;
        int nwaitpos_To_demorun_Totalrobotcnt = 0;

        private void onDemoRun_ver2()
        {
            try
            {
                bDemorun_ver2 = true;
                Data.Instance.bCrashcheckPause = false;
                bLinedrive1_waitok = false;
                bLinedrive2_waitok = false;

                nlinedriveCnt = 0;

                nbasicmoveCnt = 0;
                nliftCnt = 0;

                bTmpMove = false;
                bTmpMove_pause_robotid = "";
                bTmpMove_run_robotid = "";

                nwaitpos_To_demorun_Totalrobotcnt = 0;
                nwaitpos_To_demorun_Currentrobotidx = 0;

                ncurrWaitRobotid_idx = 0;

                strCurrStartRobot = "";
                if (Data.Instance.isConnected)
                {
                    int nrow = dataGridView_reg.SelectedCells[0].RowIndex;

                    if (nrow < 0 || nrow > dataGridView_reg.RowCount - 2) return;

                    string taskid = dataGridView_reg["taskid", nrow].Value.ToString();
                    string taskname = dataGridView_reg["taskname", nrow].Value.ToString();
                    string missionlist = dataGridView_reg["missionlist", nrow].Value.ToString();
                    string robotlist = dataGridView_reg["robotlist", nrow].Value.ToString();

                    string[] missionbuf = missionlist.Split(',');
                    string[] robotbuf = robotlist.Split(',');

                    int ntaskcnt = int.Parse(txtTaskCnt.Text.ToString());
                    string strMsg = "";


                    currTaskRobotid = new string[robotbuf.Length];
                    nCurrTaskRobot_workcnt = new int[robotbuf.Length];

                    n1_workcnt = 0;
                    n2_workcnt = 0;
                    n3_workcnt = 0;
                    n4_workcnt = 0;
                    n5_workcnt = 0;
                    n6_workcnt = 0;
                    n7_workcnt = 0;

                    ncurrTaskRobotid_idx = 0;
                    strStartMissionid = "";
                    StartRobot_Skip.Clear();
                    CrashCheckRobot_list.Clear();
                    CrashCheckRobot_Linedrive_list.Clear();
                    Data.Instance.bMissionCompleteCheck = false;

                    robot_demoinfo.Clear();

                    if(chkLineRobotSelect.Checked)
                    {
                        string strlineselectrobot_buf = txtLineSelectRobot.Text.ToString();

                        if(strlineselectrobot_buf=="")
                        {
                            MessageBox.Show("라인주행 로봇을 선택하세요. ");
                            return;
                        }
                        else
                        {
                            string[] strlineselectrobots = strlineselectrobot_buf.Split(',');

                            if(strlineselectrobots.Length !=2)
                            {
                                MessageBox.Show("라인주행 로봇은 두대를 선택합니다. ");
                                return;
                            }

                            strline1_robotid = strlineselectrobots[0];
                            strline2_robotid = strlineselectrobots[1];

                        }
                    }

                    if(chkSline.Checked)
                    {
                        string strSlineselectrobot_buf = txt_S_LineSelectRobot.Text.ToString();
                        if (strSlineselectrobot_buf == "")
                        {
                            MessageBox.Show("S라인주행 로봇을 선택하세요. ");
                            return;
                        }
                        else
                        {
                            string[] strSlineselectrobots = strSlineselectrobot_buf.Split(',');

                            strSonly_robotid = strSlineselectrobots[0];
                        }
                    }


                    if (chkDrive.Checked)
                    {
                        string strDriveselectrobot_buf = txt_DriveSelectRobot.Text.ToString();
                        if (strDriveselectrobot_buf == "")
                        {
                            MessageBox.Show("기본주행 로봇을 선택하세요. ");
                            return;
                        }
                        else
                        {
                            string[] strDriveselectrobots = strDriveselectrobot_buf.Split(',');


                            strDriveOnly_robotid = strDriveselectrobots[0];
                            strDriveOnly_robotid2 = strDriveselectrobots[1];
                            strDriveOnly_robotid3 = strDriveselectrobots[2];
                        }
                    }

                    if(chkURdrive.Checked)
                    {
                        string strURselectrobot_buf = txtURselectrobot.Text.ToString();
                        if (strURselectrobot_buf == "")
                        {
                            MessageBox.Show("UR주행 로봇을 선택하세요. ");
                            return;
                        }
                        else
                        {
                            string[] strURselectrobots = strURselectrobot_buf.Split(',');
                            strURonly_robotid = strURselectrobots[0];
                        }
                        
                    }

                    if (chkDockdrive.Checked)
                    {
                        string strDockselectrobot_buf = txtDockSelectRobot.Text.ToString();
                        if (strDockselectrobot_buf == "")
                        {
                            MessageBox.Show("Dock주행 로봇을 선택하세요. ");
                            return;
                        }
                        else
                        {
                            string[] strDockselectrobots = strDockselectrobot_buf.Split(',');
                            strDocknly_robotid = strDockselectrobots[0];
                        }

                    }


                    for (int i = 0; i < robotbuf.Length; i++)
                    {
                        string strrobotcnt = string.Format("{0}", ntaskcnt);

                        strMsg += string.Format("{0},반복({1}) ", robotbuf[i], strrobotcnt);

                        currTaskRobotid[i] = robotbuf[i];


                        Robot_Demo_Info robotdemoinfo = new Robot_Demo_Info();
                        robotdemoinfo.strrobotid = robotbuf[i];
                        if(strDriveOnly_robotid == robotbuf[i] || strDriveOnly_robotid2 == robotbuf[i] || strDriveOnly_robotid3 == robotbuf[i])
                        {
                            robotdemoinfo.workcnt = 2;
                        }
                        else robotdemoinfo.workcnt = 1;

                        

                        robotdemoinfo.currcnt = 0;

                        robotdemoinfo.liftlinecnt = 1;
                        robotdemoinfo.curr_liftlinecnt = 0;
                        robotdemoinfo.line2cnt = 1;
                        robotdemoinfo.curr_line2cnt = 0;

                        robotdemoinfo.Sonlycnt = 1;
                        robotdemoinfo.curr_Sonlycnt = 0;

                        robotdemoinfo.urcnt = 1;
                        robotdemoinfo.curr_urcnt = 0;


                        robotdemoinfo.lineonlycnt = 2;
                        robotdemoinfo.curr_lineonlycnt = 0;

                        robotdemoinfo.liftonlycnt = 2;
                        robotdemoinfo.curr_liftonlycnt = 0;

                        robotdemoinfo.s1cnt = 1;
                        robotdemoinfo.curr_s1cnt = 0;
                        robotdemoinfo.s2cnt = 1;
                        robotdemoinfo.curr_s2cnt = 0;

                       

                        robotdemoinfo.strdemomode = "basicmode";

                        robot_demoinfo.Add(robotbuf[i], robotdemoinfo);
                    }

                    strMsg += " 동작 하시겠습니까?";

                    if (DialogResult.OK == MessageBox.Show(strMsg, "확인", MessageBoxButtons.OKCancel))
                    {
                        int ntaskcnt1 = int.Parse(txtTaskCnt.Text.ToString());

                        for (int i = 0; i < robotbuf.Length; i++)
                        {
                            if (Data.Instance.Robot_work_info[robotbuf[i]].robot_status_info.taskfeedback != null)
                            {
                                if (Data.Instance.Robot_work_info[robotbuf[i]].robot_status_info.taskfeedback.msg != null)
                                {
                                    Data.Instance.Robot_work_info[robotbuf[i]].robot_status_info.taskfeedback.msg.feedback.action_indx = 0;
                                    Data.Instance.Robot_work_info[robotbuf[i]].robot_status_info.taskfeedback.msg.feedback.mission_indx = 0;
                                }
                            }
                        }

                        listBox1.Items.Clear();

                        currTaskMissionList= missionlist.Split(',');

                        //RIDiS에 task order 전달 
                        onTaskOrder(taskid, missionbuf, taskname, missionlist, robotlist, ntaskcnt1);
                        Thread.Sleep(1000);

                        Data.Instance.bTaskRun = true;
                        crashrobot_run_list.Clear();
                        crashrobot_pause_list.Clear();

                        DateTime dt = DateTime.Now;
                        Data.Instance.strTaskRun_StartTime = string.Format("{0:d4}{1:d2}{2:d2}{3:d2}{4:d2}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute);

                        mainform.dbBridge.onDBRead_Missionlist();
                        {
                            //db task  table 정보 갱신
                            mainform.dbBridge.onDBUpdate_Tasklist_status(taskid, "run");
                            onInitSet();

                            //db task operation table 정보 갱신 => Task 쓰레드에서 실시간 갱신 
                            for (int i2 = 0; i2 < robotbuf.Length; i2++)
                            {
                                mainform.dbBridge.onDBSave_TaskOperation(taskid, robotbuf[i2], missionlist, "", 0, "insert");
                            }
                            strMissionid_0 = missionbuf[nbasic_drive_idx];

                            Data.Instance.bCrashcheckStop = true;

                            if (crashchk_thread != null)
                            {
                                crashchk_thread.Abort();
                                crashchk_thread = null;
                            }

                            if (crashchk_Linedrive_thread != null)
                            {
                                crashchk_Linedrive_thread.Abort();
                                crashchk_Linedrive_thread = null;
                            }

                            if (crashchk_Sdrive_thread != null)
                            {
                                crashchk_Sdrive_thread.Abort();
                                crashchk_Sdrive_thread = null;
                            }

                            Data.Instance.bMissionCompleteCheck = true;

                            onCrash_Start();

                            timer_StartTrack.Interval = 100;
                            timer_StartTrack.Start();

                            timer_LinedrivewaitokChk.Interval = 500;
                            timer_LinedrivewaitokChk.Start();

                            onDemoRuning(strMissionid_0);

                            //task 쓰레드가 존재하는지 파악후 구동
                            int threadcnt = Data.Instance.TaskCheck_threadList.Count;
                            if (threadcnt > 0)
                            {
                                int thridx = 0;
                                for (int i = 0; i < threadcnt; i++)
                                {
                                    TaskCheck_class taskclass = Data.Instance.TaskCheck_threadList.ElementAt(i);
                                    if (taskclass.strTaskid == taskid)
                                    {
                                        if (taskclass.taskthred != null)
                                        {
                                            taskclass.taskthred.Abort();
                                            taskclass.taskthred = null;
                                        }

                                        Data.Instance.TaskCheck_threadList.RemoveAt(i);
                                    }
                                }

                            }

                            {
                                TaskCheck_class taskclass = new TaskCheck_class();
                                taskclass.strTaskid = taskid;
                                taskclass.taskfinish_Evt += new TaskCheck_class.TaskFinished(mainform.TaskFinished);

                                taskclass.taskthred = new Thread(taskclass.taskCheck_thread_func);
                                Task_checkThread_TableInfo task_checkinfo = new Task_checkThread_TableInfo();
                                task_checkinfo.taskcheck_info = new List<Task_checkThread_Info>();

                                for (int i2 = 0; i2 < robotbuf.Length; i2++)
                                {
                                    Task_checkThread_Info task_info = new Task_checkThread_Info();
                                    task_info.strrobotid = robotbuf[i2];

                                    task_checkinfo.taskcheck_info.Add(task_info);
                                }

                                Data.Instance.TaskCheck_threadList.Add(taskclass);
                                taskclass.taskthred.Start(task_checkinfo);
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("onDemoRun_ver2 err=" + ex.Message.ToString());
            }
        }

        private void onDemoRuning(string strtempmission)
        {
            try
            {
                int nwaitpos_To_demorun_Totalrobotcnt = strWaitPosMove_Robot.Count;
                nwaitpos_To_demorun_Currentrobotidx = 0;

                for (int i = 0; i < nwaitpos_To_demorun_Totalrobotcnt; i++)
                {
                    //미션1에 제일 먼 로봇부터 resume 시킨다.
                    KeyValuePair<string, string> waitposmove_robotinfo = strWaitPosMove_Robot.ElementAt(nwaitpos_To_demorun_Totalrobotcnt - (nwaitpos_To_demorun_Currentrobotidx+1));

                    string strmissionid = waitposmove_robotinfo.Key;
                    string strrobotid = waitposmove_robotinfo.Value;

                    if (strrobotid != "")
                    {

                        strStartMissionid = strtempmission;
                        strCurrStartRobot = strrobotid;

                        

                        mainform.commBridge.onMissionChange_publish(strrobotid, strStartMissionid);
                        robot_demoinfo[strrobotid].currmissionid = strStartMissionid;
                        Console.WriteLine("mission change running = {0},,robot={1}", strStartMissionid, strrobotid);
                        onConsolemsgDp(string.Format("mission change running= {0},,robot={1}", strStartMissionid, strrobotid));
                        onTaskResum(strrobotid);


                        CrashCheckRobot_list.Add(strrobotid);

                        strWaitPosMove_Robot[strmissionid] = "";
                        strWaitPosMove_Robot_one[strmissionid] = "";

                        Console.WriteLine("startrobot={0}", strCurrStartRobot);
                        onConsolemsgDp(string.Format("startrobot={0}", strCurrStartRobot));
                        nwaitpos_To_demorun_Currentrobotidx++;
                        break;
                    }

                    nwaitpos_To_demorun_Currentrobotidx++;
                }

                bstarttrack_timerflag = true;
                //timer_StartTrack.Interval = 100;
                //timer_StartTrack.Start();
                //timer_StartTrack.Enabled = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("onDemoRuning err=" + ex.Message.ToString());
            }
        }

        bool bstarttrack_timerflag = false;

        private void timer_StartTrack_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!bstarttrack_timerflag) return;

                if (ncurrTaskRobotid_idx >= currTaskRobotid.Length - 1)
                {
                    bstarttrack_timerflag = false;
                    //timer_StartTrack.Stop();
                    //timer_StartTrack.Enabled = false;
                    ncurrTaskRobotid_idx = 0;
                    strCurrStartRobot = "";
                }
                else
                {
                    if (strCurrStartRobot == "") return;

                    if (Data.Instance.Robot_work_info[strCurrStartRobot].robot_status_info.taskfeedback == null) return;
                    if (Data.Instance.Robot_work_info[strCurrStartRobot].robot_status_info.taskfeedback.msg == null) return;

                    if (Data.Instance.Robot_work_info[strCurrStartRobot].robot_status_info.taskfeedback.msg.feedback.action_indx == 1)
                    {
                        ncurrTaskRobotid_idx++;

                        int nwaitpos_To_demorun_Totalrobotcnt = strWaitPosMove_Robot.Count;

                        if (nwaitpos_To_demorun_Currentrobotidx >= nwaitpos_To_demorun_Totalrobotcnt) return;

                        KeyValuePair<string, string> waitposmove_robotinfo = strWaitPosMove_Robot.ElementAt(nwaitpos_To_demorun_Totalrobotcnt - (nwaitpos_To_demorun_Currentrobotidx + 1));

                        string strmissionid = waitposmove_robotinfo.Key;
                        string strrobotid = waitposmove_robotinfo.Value;

                        if (strrobotid != "")
                        {
                            //미션1에 제일 먼 로봇부터 resume 시킨다. 
                            strCurrStartRobot = strrobotid;
                            Thread.Sleep(1000);



                            mainform.commBridge.onMissionChange_publish(strrobotid, strStartMissionid);
                            robot_demoinfo[strrobotid].currmissionid = strStartMissionid;
                            Console.WriteLine("mission change timer = {0},,robot={1}", strStartMissionid, strrobotid);
                            onConsolemsgDp(string.Format("mission change timer = {0},,robot={1}", strStartMissionid, strrobotid));

                            onTaskResum(strrobotid);

                            CrashCheckRobot_list.Add(strrobotid);
                            strWaitPosMove_Robot[strmissionid] = "";
                            strWaitPosMove_Robot_one[strmissionid] = "";
                            Console.WriteLine("startrobot={0}", strCurrStartRobot);
                            onConsolemsgDp(string.Format("startrobot={0}", strCurrStartRobot));
                        }

                        nwaitpos_To_demorun_Currentrobotidx++;

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("timer_StartTrack_Tick err=" + ex.Message.ToString());
            }
        }

        private void onCrash_Start()
        {
            try
            {
                timer_StartTrack.Enabled = false;
                ncurrTaskRobotid_idx = 0;
                strCurrStartRobot = "";

                crashrobot_run_list.Clear();
                crashrobot_pause_list.Clear();

                crashrobot_Linedrive_run_list.Clear();
                crashrobot_Linedrive_pause_list.Clear();

                crashrobot_Sdrive_run_list.Clear();
                crashrobot_Sdrive_pause_list.Clear();

                Invoke(new MethodInvoker(delegate ()
                {
                    listBox2.Items.Clear();
                }));

                Data.Instance.bCrashcheckStop = false;

                crashchk_thread = new Thread(onCrashCheck_Thread);
                crashchk_thread.Start();

                crashchk_Linedrive_thread = new Thread(onCrashCheck_Linedrive_Thread);
                crashchk_Linedrive_thread.Start();

                crashchk_Sdrive_thread = new Thread(onCrashCheck_Sdrive_Thread);
                crashchk_Sdrive_thread.Start();



            }
            catch (Exception ex)
            {
                Console.WriteLine("onCrash_Start err=" + ex.Message.ToString());
            }
        }
        private void onRobotXYCheck()
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    //for (int i = 0; i < 7; i++)
                    //{
                    //    string strrobot = "";
                    //    strrobot = string.Format("R_00{0}", i);

                    //    if (!Data.Instance.Robot_work_info.ContainsKey(strrobot)) continue;

                    //    if (Data.Instance.Robot_work_info[strrobot].robot_status_info.robotstate == null)
                    //    {

                    //        continue;
                    //    }
                    //    if (Data.Instance.Robot_work_info[strrobot].robot_status_info.robotstate.msg == null)
                    //    {
                    //        continue;
                    //    }

                    //    if (Data.Instance.Robot_work_info[strrobot].robot_status_info.lookahead == null)
                    //    {
                    //        continue;
                    //    }
                    //    if (Data.Instance.Robot_work_info[strrobot].robot_status_info.lookahead.msg == null)
                    //    {

                    //        continue;
                    //    }

                    //    double source_x = 0;
                    //    double source_y = 0;
                    //    double source_lookahead_x = 0;
                    //    double source_lookahead_y = 0;
                    //    source_x = Data.Instance.Robot_work_info[strrobot].robot_status_info.robotstate.msg.pose.x;
                    //    source_y = Data.Instance.Robot_work_info[strrobot].robot_status_info.robotstate.msg.pose.y;
                    //    source_lookahead_x = Data.Instance.Robot_work_info[strrobot].robot_status_info.lookahead.msg.point.x;
                    //    source_lookahead_y = Data.Instance.Robot_work_info[strrobot].robot_status_info.lookahead.msg.point.y;

                    //    Console.WriteLine("robotid={0}, x={1:f2}, y={2:f2}, look x={3:f2}, look y={4:f2}", source_x, source_y, source_lookahead_x, source_lookahead_y);
                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("onRobotXYCheck err=" + ex.Message.ToString());
            }

        }


        //MID20190703172026,MID20190704195647,MID20190712143725,MID20190714161637,MID20190721121228,MID20190712144134,MID20190714161622,MID20190721121240

        //기본주행 MID20190703172026
        //거치대 MID20190704195647
        //직선대기1 MID20190712143725, 직선1_1 MID20190714161637, 직선1_2 MID20190721121228, 
        //직선대기2 MID20190712144134, 직선2_1 MID20190714161622,직선2_2 MID20190721121240

        //S자주행 MID20190711162309

        //ur주행

        string strlift_robotid = "R_005";


        string strCurve_robotid = "R_008";

        string strUR_robotid = "R_004";

        string strSpeed_robotid = "R_006";
        int nline_speed_idx = 3;

       
        int nline1_wait_idx = 2;
       // int nline1_idx = 3;
        int nline1_2_idx = 4;

        
        int nline2_wait_idx = 5;
       // int nline2_idx = 6;
        int nline2_2_idx = 7;

        bool bLinedrive1_waitok = false;
        bool bLinedrive2_waitok = false;

        int nlinedriveCnt = 0;

        int nbasicmoveCnt = 0;
        int nliftCnt = 0;
        int nCurveCnt = 0;

        int nmissioncompleteskipcnt = 0;

        bool bsequenceDemo = false;


        bool bLineChk = false;
        bool bLiftChk = false;
        bool bLiftChk_200 = false;
        //MID20190703172026,MID20190807210301,MID20190807210333,MID20190807210339,MID20190807210353,MID20190807210402,MID20190807210414,MID201908wait1,MID201908wait2,MID201908wait3,MID201908wait4,MID201908wait5,MID201908wait6,MID201908wait7,MID20190808151030,MID20190808151157,MID20190808151226


        //기본주행 MID20190703172026 //스텐바이 MID20190809165048

        //MID20190807210301	리프트_라인1대기	 //스텐바이 MID20190809165048
        //MID20190807210333	라인1            //스텐바이 MID20190809165048
        //MID20190807210339	라인2대기	        //스텐바이 MID20190809165048

        //MID20190807210353	라인2             //스텐바이 MID20190809165048
        //MID20190807210402	리프트_end  //스텐바이 MID20190809165048
        //MID20190807210414	라인2_end     //스텐바이 MID20190809165048
        //MID201908wait11
        //MID201908wait12
        //MID201908wait13
        //MID201908wait14
        //MID201908wait15
        //MID201908wait16
        //MID201908wait17

        //MID20190812154402 UR만

        //MID20190808151030	라인만_대기  //스텐바이 MID20190809165048
        //MID20190808151157	라인만         //스텐바이 MID20190809165048
        //MID20190808151226	라인_나가기      //스텐바이 MID20190809165048

        //MID20190808161838	S자만대기       //스텐바이 MID20190809165048
        //MID20190808162105	S자만     //스텐바이 MID20190809165048
        //MID20190808162243	S자_나가기      //스텐바이 MID20190809165048

        //MID20190811145027	리프트만

        //MID20190811171931 S자1대기
        //MID20190811171944 S자1라인
        //MID20190811171958 S자1end

        //MID20190811172009 S자2대기
        //MID20190811172017 S자2라인
        //MID20190811172028 S자2end
        //MID20190811172036 S자2라인wait

    

        /*주행만*/
        string strDriveOnly_robotid = "R_999";
        string strDriveOnly_robotid2 = "R_999";
        string strDriveOnly_robotid3 = "R_999";

        /*lift & line cross */
        bool bLiftOn = false;
        bool blinedrive = false;
        int nbasic_drive_idx = 0;
        int nlift_linewait1_mission_idx = 1;
        int nline1_idx = 2;
        int nlinewait2_idx = 3;
        int nline2_idx = 4;
        int nlift_end_idx = 5;
        int line2_end_idx = 6;

        int nlift1_crash_skipidx = 2 +1;
        int nlift1_crash_chkidx = 4 + 1;
        int nlift1_crash_skipidx2 = 7 + 1;
        int nlift1_end_crash_chkidx = 3 + 1;

        int nlift2_crash_skipidx = 2 + 1;


        string strline1_robotid = "R_099";
        string strline1_missionid = "";
        string strline2_robotid = "R_099";
        string strline2_missionid = "";

        int nwait1_idx = 7;
        int nwait2_idx = 8;
        int nwait3_idx = 9;
        int nwait4_idx = 10;
        int nwait5_idx = 11;
        int nwait6_idx = 12;
        int nwait7_idx = 13;

        /*line만 주행 */
        bool bLineOnly = false;
        //int nlineonly_wait_idx = 14;
        //int nlineonly_idx = 15;
        //int nlineonly_end_idx = 16;
        string strlineonly_robotid = "R_999";

        /*S자만 주행*/
        bool bSOnly = false;
        //int nSonly_wait_idx = 17;
        //int nSonly_idx = 18;
        //int nSonly_end_idx = 19;
        string strSonly_robotid = "R_099";

        int nS_crash_skipidx = 1 + 1;

        /*lift만 동작*/
        bool bLiftOnly = false;
        string strLiftonly_robotid = "R_999";


        /*UR 만 동작*/
        bool bUROnly = false;
        string strURonly_robotid = "R_999";
        int nUR_crash_skipidx = 4 + 1;
        int nUR_crash_chkidx = 6 + 1;

        /*docking 만 */
        bool bDockOnly = false;
        string strDocknly_robotid = "R_999";
        int nDock_crash_skipidx = 7 + 1;
        int nDock_crash_chkidx = 9 + 1;

        /*S자 cross 주행*/
        bool bSdrive1_waitok = false;
        bool bSdrive2_waitok = false;
        bool bS2linewait = false;
        bool bS1On = false;
        string strS1_robotid = "R_999";
        string strS1_missionid = "";
        bool bS2On = false;
        string strS2_robotid = "R_999";
        string strS2_missionid = "";

        bool bs2_linewait_complete = false;


        public async void TaskFeedback_Complete(string strrobotid)
        {
            try
            {
                string missionid = "";
                //Console.WriteLine("robotid ={3},act idx ={0}, mission idx={1},mission id={4}, task idx={2}", Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.action_indx,
                //        Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.mission_indx,
                //        Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.loop_count,
                //        strrobotid, Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.work_id);


                if (!bDemoRun) return;

                if (bDemorun_ver2)
                {
                    onTaskFeedbak_Complete_ver2(strrobotid);
                }
                else
                {
                    //bool bmissionChg_Line = false;
                    //string strrevmissionid = Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.work_id;

                    //if (Data.Instance.bMissionCompleteCheck)
                    //{
                    //    Data.Instance.bCrashcheckPause = true;

                    //    Console.WriteLine("mission complete idx= {0},,robot={1}", Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.mission_indx, strrobotid);

                    //    bool bbasicmove = true;

                    //    //리프트 주행
                    //    if (bLiftChk && strrobotid == strlift_robotid)
                    //    {
                    //        if (bsequenceDemo)
                    //        {
                    //            if (nliftCnt >= 1)
                    //            {
                    //                nliftCnt = 0;
                    //                chkLiftRun.Checked = false;

                    //                bbasicmove = true;
                    //            }
                    //            else
                    //            {
                    //                missionid = "MID20190704195647";
                    //                bbasicmove = false;
                    //            }
                    //            nliftCnt++;
                    //        }
                    //        else
                    //        {
                    //            missionid = "MID20190704195647";
                    //            bbasicmove = false;
                    //        }
                    //    }

                    //    //s자 주행
                    //    if (chkCurveRun.Checked && strrobotid == strCurve_robotid)
                    //    {
                    //        if (bsequenceDemo)
                    //        {
                    //            if (nCurveCnt == 1)
                    //            {
                    //                nCurveCnt = 0;
                    //                chkCurveRun.Checked = false;

                    //                bbasicmove = true;
                    //            }
                    //            else
                    //            {
                    //                missionid = "MID20190711162309";
                    //                bbasicmove = false;
                    //            }
                    //            nCurveCnt++;
                    //        }
                    //        else
                    //        {
                    //            missionid = "MID20190711162309";
                    //            bbasicmove = false;
                    //        }
                    //    }

                    //    //UR 주행
                    //    if (chkURRun.Checked && strrobotid == strUR_robotid)
                    //    {
                    //        //missionid = "MID20190704195647"; 
                    //        bbasicmove = false;
                    //    }

                    //    //직선주행
                    //    if (bLineChk)
                    //    {
                    //        //우선순위 지정
                    //        Data.Instance.Robot_work_info[strline1_robotid].nPriorityLevel = 1;
                    //        Data.Instance.Robot_work_info[strline2_robotid].nPriorityLevel = 2;

                    //        int nmissionidx = Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.mission_indx;

                    //        if (nmissionidx == 0)
                    //        {
                    //            if (strrobotid == strline1_robotid)
                    //            {
                    //                missionid = "MID20190712143725"; //직선대기1 MID20190712143725
                    //                bbasicmove = false;
                    //            }

                    //            if (strrobotid == strline2_robotid)
                    //            {
                    //                missionid = "MID20190712144134";
                    //                bbasicmove = false;
                    //            }
                    //        }
                    //        else if (nmissionidx == nline1_wait_idx && strrobotid == strline1_robotid)
                    //        {
                    //            // Thread.Sleep(500);
                    //            onTaskPause(strrobotid);
                    //            missionid = "";

                    //            int ncheckcnt = CrashCheckRobot_list.Count;
                    //            if (ncheckcnt > 0)
                    //            {
                    //                for (int i = 0; i < ncheckcnt; i++)
                    //                {
                    //                    if (CrashCheckRobot_list[i] == strrobotid)
                    //                    {
                    //                        CrashCheckRobot_list.RemoveAt(i);

                    //                        break;
                    //                    }
                    //                }
                    //            }

                    //            int nchecklinecnt = CrashCheckRobot_Linedrive_list.Count;

                    //            bool bchk = false;
                    //            if (nchecklinecnt > 0)
                    //            {

                    //                for (int i = 0; i < nchecklinecnt; i++)
                    //                {
                    //                    if (CrashCheckRobot_Linedrive_list[i] == strrobotid)
                    //                    {
                    //                        bchk = true;
                    //                        break;
                    //                    }
                    //                }
                    //            }
                    //            if (!bchk)
                    //            {
                    //                CrashCheckRobot_Linedrive_list.Add(strrobotid);
                    //            }

                    //            //Thread.Sleep(500);
                    //            bLinedrive1_waitok = true;

                    //            bmissionChg_Line = true;
                    //            bbasicmove = false;
                    //        }
                    //        else if (nmissionidx == nline2_wait_idx && strrobotid == strline2_robotid)
                    //        {
                    //            // Thread.Sleep(500);
                    //            onTaskPause(strrobotid);
                    //            missionid = "";

                    //            int ncheckcnt = CrashCheckRobot_list.Count;
                    //            if (ncheckcnt > 0)
                    //            {
                    //                for (int i = 0; i < ncheckcnt; i++)
                    //                {
                    //                    if (CrashCheckRobot_list[i] == strrobotid)
                    //                    {
                    //                        CrashCheckRobot_list.RemoveAt(i);

                    //                        break;
                    //                    }
                    //                }
                    //            }

                    //            int nchecklinecnt = CrashCheckRobot_Linedrive_list.Count;

                    //            bool bchk = false;
                    //            if (nchecklinecnt > 0)
                    //            {

                    //                for (int i = 0; i < nchecklinecnt; i++)
                    //                {
                    //                    if (CrashCheckRobot_Linedrive_list[i] == strrobotid)
                    //                    {
                    //                        bchk = true;
                    //                        break;
                    //                    }
                    //                }
                    //            }
                    //            if (!bchk)
                    //            {
                    //                CrashCheckRobot_Linedrive_list.Add(strrobotid);
                    //            }

                    //            //Thread.Sleep(500);
                    //            bLinedrive2_waitok = true;

                    //            bmissionChg_Line = true;
                    //            bbasicmove = false;
                    //        }

                    //        else if (nmissionidx == nline1_idx && strrobotid == strline1_robotid)
                    //        {
                    //            if (strrevmissionid != "MID20190714161637")
                    //            {
                    //                Data.Instance.bCrashcheckPause = false;
                    //                return;
                    //            }

                    //            onTaskPause(strrobotid);
                    //            missionid = "";

                    //            int ncheckcnt = CrashCheckRobot_list.Count;
                    //            if (ncheckcnt > 0)
                    //            {
                    //                for (int i = 0; i < ncheckcnt; i++)
                    //                {
                    //                    if (CrashCheckRobot_list[i] == strrobotid)
                    //                    {
                    //                        CrashCheckRobot_list.RemoveAt(i);

                    //                        break;
                    //                    }
                    //                }
                    //            }

                    //            int nchecklinecnt = CrashCheckRobot_Linedrive_list.Count;

                    //            bool bchk = false;
                    //            if (nchecklinecnt > 0)
                    //            {

                    //                for (int i = 0; i < nchecklinecnt; i++)
                    //                {
                    //                    if (CrashCheckRobot_Linedrive_list[i] == strrobotid)
                    //                    {
                    //                        bchk = true;
                    //                        break;
                    //                    }
                    //                }
                    //            }
                    //            if (!bchk)
                    //            {
                    //                CrashCheckRobot_Linedrive_list.Add(strrobotid);
                    //            }



                    //            // Thread.Sleep(500);
                    //            bLinedrive1_waitok = true;

                    //            bmissionChg_Line = true;
                    //            bbasicmove = false;
                    //        }
                    //        else if (nmissionidx == nline2_idx && strrobotid == strline2_robotid)
                    //        {
                    //            if (strrevmissionid != "MID20190714161622")
                    //            {
                    //                Data.Instance.bCrashcheckPause = false;
                    //                return;
                    //            }
                    //            //Thread.Sleep(500);
                    //            onTaskPause(strrobotid);
                    //            missionid = "";

                    //            int ncheckcnt = CrashCheckRobot_list.Count;
                    //            if (ncheckcnt > 0)
                    //            {
                    //                for (int i = 0; i < ncheckcnt; i++)
                    //                {
                    //                    if (CrashCheckRobot_list[i] == strrobotid)
                    //                    {
                    //                        CrashCheckRobot_list.RemoveAt(i);

                    //                        break;
                    //                    }
                    //                }
                    //            }

                    //            int nchecklinecnt = CrashCheckRobot_Linedrive_list.Count;

                    //            bool bchk = false;
                    //            if (nchecklinecnt > 0)
                    //            {

                    //                for (int i = 0; i < nchecklinecnt; i++)
                    //                {
                    //                    if (CrashCheckRobot_Linedrive_list[i] == strrobotid)
                    //                    {
                    //                        bchk = true;
                    //                        break;
                    //                    }
                    //                }
                    //            }
                    //            if (!bchk)
                    //            {
                    //                CrashCheckRobot_Linedrive_list.Add(strrobotid);
                    //            }


                    //            //Thread.Sleep(500);
                    //            bLinedrive2_waitok = true;

                    //            bmissionChg_Line = true;
                    //            bbasicmove = false;
                    //        }

                    //        else if (nmissionidx == nline1_2_idx && strrobotid == strline1_robotid)
                    //        {
                    //            if (strrevmissionid != "MID20190721121228")
                    //            {
                    //                Data.Instance.bCrashcheckPause = false;
                    //                return;
                    //            }

                    //            // Thread.Sleep(500);
                    //            // onTaskPause(strrobotid);
                    //            bLineChk = false;
                    //            missionid = "MID20190714161637";

                    //            int ncheckcnt = CrashCheckRobot_list.Count;
                    //            if (ncheckcnt > 0)
                    //            {
                    //                for (int i = 0; i < ncheckcnt; i++)
                    //                {
                    //                    if (CrashCheckRobot_list[i] == strrobotid)
                    //                    {
                    //                        CrashCheckRobot_list.RemoveAt(i);

                    //                        break;
                    //                    }
                    //                }
                    //            }

                    //            int nchecklinecnt = CrashCheckRobot_Linedrive_list.Count;

                    //            bool bchk = false;
                    //            if (nchecklinecnt > 0)
                    //            {

                    //                for (int i = 0; i < nchecklinecnt; i++)
                    //                {
                    //                    if (CrashCheckRobot_Linedrive_list[i] == strrobotid)
                    //                    {
                    //                        bchk = true;
                    //                        break;
                    //                    }
                    //                }
                    //            }
                    //            if (!bchk)
                    //            {
                    //                CrashCheckRobot_Linedrive_list.Add(strrobotid);
                    //            }


                    //            bLinedrive1_waitok = true;

                    //            bmissionChg_Line = true;
                    //            bbasicmove = false;
                    //        }
                    //        else if (nmissionidx == nline2_2_idx && strrobotid == strline2_robotid)
                    //        {
                    //            if (strrevmissionid != "MID20190721121240")
                    //            {
                    //                Data.Instance.bCrashcheckPause = false;
                    //                return;
                    //            }
                    //            //    Thread.Sleep(500);
                    //            //    onTaskPause(strrobotid);
                    //            bLineChk = false;
                    //            missionid = "MID20190703172026";

                    //            int ncheckcnt = CrashCheckRobot_list.Count;
                    //            if (ncheckcnt > 0)
                    //            {
                    //                for (int i = 0; i < ncheckcnt; i++)
                    //                {
                    //                    if (CrashCheckRobot_list[i] == strrobotid)
                    //                    {
                    //                        CrashCheckRobot_list.RemoveAt(i);

                    //                        break;
                    //                    }
                    //                }
                    //            }

                    //            int nchecklinecnt = CrashCheckRobot_Linedrive_list.Count;

                    //            bool bchk = false;
                    //            if (nchecklinecnt > 0)
                    //            {

                    //                for (int i = 0; i < nchecklinecnt; i++)
                    //                {
                    //                    if (CrashCheckRobot_Linedrive_list[i] == strrobotid)
                    //                    {
                    //                        bchk = true;
                    //                        break;
                    //                    }
                    //                }
                    //            }
                    //            if (!bchk)
                    //            {
                    //                CrashCheckRobot_Linedrive_list.Add(strrobotid);
                    //            }


                    //            //Thread.Sleep(500);
                    //            bLinedrive2_waitok = true;

                    //            bmissionChg_Line = true;
                    //            bbasicmove = false;
                    //        }
                    //    }

                    //    if (chkSpeedDrive.Checked && strrobotid == strSpeed_robotid)
                    //    {
                    //        int nmissionidx = Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.mission_indx;

                    //        if (nmissionidx == 0)
                    //        {
                    //            if (strrobotid == strSpeed_robotid)
                    //            {
                    //                missionid = "MID20190712144134";
                    //                bbasicmove = false;
                    //            }
                    //        }
                    //        else if (nmissionidx == nline1_wait_idx && strrobotid == strSpeed_robotid)
                    //        {
                    //            // Thread.Sleep(500);

                    //            missionid = "MID20190722102505"; //고속주행

                    //            int ncheckcnt = CrashCheckRobot_list.Count;
                    //            if (ncheckcnt > 0)
                    //            {
                    //                for (int i = 0; i < ncheckcnt; i++)
                    //                {
                    //                    if (CrashCheckRobot_list[i] == strrobotid)
                    //                    {
                    //                        CrashCheckRobot_list.RemoveAt(i);

                    //                        break;
                    //                    }
                    //                }
                    //            }

                    //            int nchecklinecnt = CrashCheckRobot_Linedrive_list.Count;

                    //            bool bchk = false;
                    //            if (nchecklinecnt > 0)
                    //            {

                    //                for (int i = 0; i < nchecklinecnt; i++)
                    //                {
                    //                    if (CrashCheckRobot_Linedrive_list[i] == strrobotid)
                    //                    {
                    //                        bchk = true;
                    //                        break;
                    //                    }
                    //                }
                    //            }
                    //            if (!bchk)
                    //            {
                    //                CrashCheckRobot_Linedrive_list.Add(strrobotid);
                    //            }

                    //            bmissionChg_Line = true;
                    //            bbasicmove = false;
                    //        }

                    //        else if (nmissionidx == nline_speed_idx && strrobotid == strSpeed_robotid)
                    //        {

                    //            missionid = "MID20190712144134";

                    //            int ncheck_line_cnt = CrashCheckRobot_Linedrive_list.Count;
                    //            if (ncheck_line_cnt > 0)
                    //            {
                    //                for (int i = 0; i < ncheck_line_cnt; i++)
                    //                {
                    //                    if (CrashCheckRobot_Linedrive_list[i] == strrobotid)
                    //                    {
                    //                        CrashCheckRobot_Linedrive_list.RemoveAt(i);

                    //                        break;
                    //                    }
                    //                }
                    //            }


                    //            int ncheckcnt = CrashCheckRobot_list.Count;
                    //            bool bchk = false;
                    //            if (ncheckcnt > 0)
                    //            {
                    //                for (int i = 0; i < ncheckcnt; i++)
                    //                {
                    //                    if (CrashCheckRobot_list[i] == strrobotid)
                    //                    {
                    //                        bchk = true;
                    //                        break;
                    //                    }
                    //                }
                    //            }
                    //            if (!bchk)
                    //            {
                    //                CrashCheckRobot_list.Add(strrobotid);
                    //            }

                    //            bmissionChg_Line = true;
                    //            bbasicmove = false;
                    //        }
                    //    }

                    //    //기본 주행
                    //    if (bbasicmove && !bLineChk)
                    //    {
                    //        missionid = "MID20190703172026"; //기본주행

                    //        int ncheck_line_cnt = CrashCheckRobot_Linedrive_list.Count;
                    //        if (ncheck_line_cnt > 0)
                    //        {
                    //            for (int i = 0; i < ncheck_line_cnt; i++)
                    //            {
                    //                if (CrashCheckRobot_Linedrive_list[i] == strrobotid)
                    //                {
                    //                    CrashCheckRobot_Linedrive_list.RemoveAt(i);

                    //                    break;
                    //                }
                    //            }
                    //        }


                    //        int ncheckcnt = CrashCheckRobot_list.Count;
                    //        bool bchk = false;
                    //        if (ncheckcnt > 0)
                    //        {
                    //            for (int i = 0; i < ncheckcnt; i++)
                    //            {
                    //                if (CrashCheckRobot_list[i] == strrobotid)
                    //                {
                    //                    bchk = true;
                    //                    break;
                    //                }
                    //            }
                    //        }

                    //        if (!bchk)
                    //        {
                    //            CrashCheckRobot_list.Add(strrobotid);
                    //        }


                    //        if (bsequenceDemo)
                    //        {
                    //            nbasicmoveCnt++;
                    //            Console.WriteLine("basic count = {0},", nbasicmoveCnt);

                    //            if (nbasicmoveCnt > 0)
                    //            {
                    //                Invoke(new MethodInvoker(delegate ()
                    //                {
                    //                    chkLineDrive.Checked = true;
                    //                    bLineChk = true;
                    //                }));
                    //                nbasicmoveCnt = 0;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            nbasicmoveCnt = 0;
                    //        }
                    //    }

                    //    if (missionid != "")
                    //    {
                    //        mainform.commBridge.onMissionChange_publish(strrobotid, missionid);
                    //        Thread.Sleep(100);
                    //        Console.WriteLine("mission change = {0},,robot={1}", missionid, strrobotid);
                    //    }

                    //    Data.Instance.bCrashcheckPause = false;
                    //}
                    //else
                    //{

                    //}
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("TaskFeedback_Complete err=" + ex.Message.ToString());
            }

        }

        private async void onTaskFeedbak_Complete_ver2(string strrobotid)
        {
            try
            {
                string missionid = "";

                bool bmissionChg_Line = false;

                string strrevmissionid = Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.work_id;

                if (Data.Instance.bMissionCompleteCheck)
                {
                    Data.Instance.bCrashcheckPause = true;
                    Thread.Sleep(100);

                    Console.WriteLine("mission complete idx= {0}, ID={1}, robot={2}", Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.mission_indx,
                        Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.work_id,strrobotid);

                    onConsolemsgDp(string.Format("mission complete idx= {0}, ID={1}, robot={2}", Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.mission_indx,
                        Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.work_id, strrobotid));

                    int ncurrmission_idx = Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.mission_indx;
                    string strrevmission = Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.work_id;
                  
                    //미션id와 index가 맞지않으면  skip
                    if (currTaskMissionList[ncurrmission_idx] != strrevmissionid)
                    {
                        Console.WriteLine("feedback skip");
                        onConsolemsgDp("feedback skip");
                        Data.Instance.bCrashcheckPause = false;
                        return;
                    }

                    if(robot_demoinfo[strrobotid].currmissionid != strrevmission)
                    {
                        Console.WriteLine("feedback mission fail skip");
                        onConsolemsgDp("feedback mission fail skip");
                        Data.Instance.bCrashcheckPause = false;
                        return;
                    }

                    if (robot_demoinfo.ContainsKey(strrobotid))
                    {
                        Robot_Demo_Info robotdemoinfo = robot_demoinfo[strrobotid];

                        if (robot_demoinfo[strrobotid].strdemomode == "basicmode" && strrevmission != "MID20190703172026")
                        {
                            Console.WriteLine("basic mode and mission mismatch => skip");
                            onConsolemsgDp("basic mode and mission mismatch => skip");
                            Data.Instance.bCrashcheckPause = false;
                            return;
                        }

                        if (robot_demoinfo[strrobotid].strdemomode == "basicmode")
                        {
                            if (robotdemoinfo.currcnt == robotdemoinfo.workcnt - 1)
                            {
                                if (strrobotid == strlineonly_robotid)
                                {
                                    bLineOnly = true;
                                    robot_demoinfo[strrobotid].strdemomode = "lineonly_start";
                                    robot_demoinfo[strrobotid].currcnt = 0;
                                }
                                else if (strrobotid == strSonly_robotid)
                                {
                                    bSOnly = true;
                                    robot_demoinfo[strrobotid].strdemomode = "Sonly_start";
                                    robot_demoinfo[strrobotid].currcnt = 0;
                                }
                                else if (strrobotid == strline1_robotid)
                                {
                                    bLiftOn = true;
                                    robot_demoinfo[strrobotid].strdemomode = "liftline_start";
                                    robot_demoinfo[strrobotid].currcnt = 0;
                                    
                                }
                                else if (strrobotid == strline2_robotid)
                                {
                                    blinedrive = true;
                                    robot_demoinfo[strrobotid].strdemomode = "line2_start";
                                    robot_demoinfo[strrobotid].currcnt = 0;
                                    
                                }
                                else if (strrobotid == strS1_robotid)
                                {
                                    bS1On = true;
                                    robot_demoinfo[strrobotid].strdemomode = "S1_start";
                                    robot_demoinfo[strrobotid].currcnt = 0;
                                }
                                else if (strrobotid == strS2_robotid)
                                {
                                    bS2On = true;
                                    robot_demoinfo[strrobotid].strdemomode = "S2_start";
                                    robot_demoinfo[strrobotid].currcnt = 0;
                                }

                                else if (strrobotid == strLiftonly_robotid)
                                {
                                    bLiftOnly = true;
                                    robot_demoinfo[strrobotid].strdemomode = "liftonly_start";
                                    robot_demoinfo[strrobotid].currcnt = 0;
                                }
                                else if(strrobotid == strURonly_robotid)
                                {
                                    bUROnly = true;
                                    robot_demoinfo[strrobotid].strdemomode = "uronly_start";
                                    robot_demoinfo[strrobotid].currcnt = 0;
                                }

                                else if (strrobotid == strDocknly_robotid)
                                {
                                    bDockOnly = true;
                                    robot_demoinfo[strrobotid].strdemomode = "dockonly_start";
                                    robot_demoinfo[strrobotid].currcnt = 0;
                                }
                                else
                                {
                                    robotdemoinfo.strdemomode = "waitpos";
                                    robot_demoinfo[strrobotid].strdemomode = "waitpos";
                                    robot_demoinfo[strrobotid].currcnt = 0;
                                }
                            }
                            else
                            {
                                robot_demoinfo[strrobotid].currcnt++;

                                missionid = "MID20190703172026"; //기본주행

                                onDrive_crashcheck_add(strrobotid);
                            }
                        }

                        #region line only
                        if(strrobotid == strlineonly_robotid && bLineOnly)
                        {
                            if (robot_demoinfo[strrobotid].strdemomode == "lineonly_start")
                            {
                                missionid = "MID20190808151030"; //MID20190808151030 라인만_대기
                                onDrive_crashcheck_add(strrobotid);

                                robot_demoinfo[strrobotid].strdemomode = "lineonly_wait";
                            }
                            else if (robot_demoinfo[strrobotid].strdemomode == "lineonly_wait")
                            {
                                missionid = "MID20190808151157"; //MID20190808151157	라인만
                                onLine_crashcheck_add(strrobotid);

                                robot_demoinfo[strrobotid].strdemomode = "lineonly";
                            }
                            else if (robot_demoinfo[strrobotid].strdemomode == "lineonly")
                            {
                                missionid = "MID20190808151226"; //MID20190808151226	라인_나가기
                                onDrive_crashcheck_add(strrobotid);

                                robot_demoinfo[strrobotid].strdemomode = "lineonly_end";
                            }
                            else if (robot_demoinfo[strrobotid].strdemomode == "lineonly_end")
                            {
                                if (robotdemoinfo.curr_lineonlycnt == robotdemoinfo.lineonlycnt - 1)
                                {
                                    robotdemoinfo.strdemomode = "waitpos";
                                    robot_demoinfo[strrobotid].strdemomode = "waitpos";
                                    robot_demoinfo[strrobotid].curr_lineonlycnt = 0;
                                    bLineOnly = false;
                                }
                                else
                                {
                                    robot_demoinfo[strrobotid].curr_lineonlycnt++;

                                    missionid = "MID20190808151030"; //MID20190808151030 라인만_대기
                                    onDrive_crashcheck_add(strrobotid);

                                    robot_demoinfo[strrobotid].strdemomode = "lineonly_wait";
                                }
                            }

                        }
                        #endregion

                        #region S only
                        if (strrobotid == strSonly_robotid && bSOnly)
                        {
                            if (robot_demoinfo[strrobotid].strdemomode == "Sonly_start")
                            {
                                missionid = "MID20190808161838"; //MID20190808161838	S자만대기
                                onDrive_crashcheck_add(strrobotid);

                                robot_demoinfo[strrobotid].strdemomode = "Sonly_wait";
                            }
                            else if (robot_demoinfo[strrobotid].strdemomode == "Sonly_wait")
                            {
                                missionid = "MID20190808162105"; //MID20190808162105	S자만
                                onS_crashcheck_add(strrobotid);

                                robot_demoinfo[strrobotid].strdemomode = "Sonly";
                            }
                            else if (robot_demoinfo[strrobotid].strdemomode == "Sonly")
                            {
                                missionid = "MID20190808162243"; //MID20190808162243	S자_나가기
                                onDrive_crashcheck_add(strrobotid);

                                robot_demoinfo[strrobotid].strdemomode = "Sonly_end";
                            }
                            else if (robot_demoinfo[strrobotid].strdemomode == "Sonly_end")
                            {
                                if (robotdemoinfo.curr_Sonlycnt == robotdemoinfo.Sonlycnt - 1)
                                {
                                    robotdemoinfo.strdemomode = "waitpos";
                                    robot_demoinfo[strrobotid].strdemomode = "waitpos";
                                    robot_demoinfo[strrobotid].curr_Sonlycnt = 0;
                                    bSOnly = false;
                                }
                                else
                                {
                                    robot_demoinfo[strrobotid].curr_Sonlycnt++;

                                    missionid = "MID20190808161838"; //MID20190808161838	S자만대기
                                    onDrive_crashcheck_add(strrobotid);

                                    robot_demoinfo[strrobotid].strdemomode = "Sonly_wait";
                                }
                            }

                        }
                        #endregion

                        #region lift only
                        if (strrobotid == strLiftonly_robotid && bLiftOnly)
                        {
                            if (robot_demoinfo[strrobotid].strdemomode == "liftonly_start")
                            {
                                missionid = "MID20190811145027"; //MID20190811145027	리프트만
                                onDrive_crashcheck_add(strrobotid);

                                robot_demoinfo[strrobotid].strdemomode = "liftonly_end";
                            }
                            else if (robot_demoinfo[strrobotid].strdemomode == "liftonly_end")
                            {
                                if (robotdemoinfo.curr_liftonlycnt == robotdemoinfo.liftonlycnt - 1)
                                {
                                    robotdemoinfo.strdemomode = "waitpos";
                                    robot_demoinfo[strrobotid].strdemomode = "waitpos";
                                    robot_demoinfo[strrobotid].curr_liftonlycnt = 0;
                                    bLiftOnly = false;
                                }
                                else
                                {
                                    robot_demoinfo[strrobotid].curr_liftonlycnt++;

                                    missionid = "MID20190811145027"; //MID20190811145027	리프트만
                                    onDrive_crashcheck_add(strrobotid);

                                    robot_demoinfo[strrobotid].strdemomode = "liftonly_end";
                                }
                            }
                        }
                        #endregion

                        #region UR
                        if (strrobotid == strURonly_robotid && bUROnly)
                        {
                            if (robot_demoinfo[strrobotid].strdemomode == "uronly_start")
                            {
                                missionid = "MID20190812154402"; //MID20190812154402 UR만
                                onDrive_crashcheck_add(strrobotid);

                                robot_demoinfo[strrobotid].strdemomode = "uronly_end";
                            }
                            else if (robot_demoinfo[strrobotid].strdemomode == "uronly_end")
                            {
                                if (robotdemoinfo.curr_urcnt == robotdemoinfo.urcnt - 1)
                                {
                                    robotdemoinfo.strdemomode = "waitpos";
                                    robot_demoinfo[strrobotid].strdemomode = "waitpos";
                                    robot_demoinfo[strrobotid].curr_urcnt = 0;
                                    bUROnly = false;
                                }
                                else
                                {
                                    robot_demoinfo[strrobotid].curr_urcnt++;

                                    missionid = "MID20190812154402"; //MID20190812154402 UR만
                                    onDrive_crashcheck_add(strrobotid);

                                    robot_demoinfo[strrobotid].strdemomode = "uronly_end";
                                }
                            }
                        }
                        #endregion

                        #region Dock
                        if (strrobotid == strDocknly_robotid && bDockOnly)
                        {
                            if (robot_demoinfo[strrobotid].strdemomode == "dockonly_start")
                            {
                                missionid = "MID20190916172735"; //MID20190916172735 dock만
                                onDrive_crashcheck_add(strrobotid);

                                robot_demoinfo[strrobotid].strdemomode = "dockonly_end";
                            }
                            else if (robot_demoinfo[strrobotid].strdemomode == "dockonly_end")
                            {
                                if (robotdemoinfo.curr_urcnt == robotdemoinfo.urcnt - 1)
                                {
                                    robotdemoinfo.strdemomode = "waitpos";
                                    robot_demoinfo[strrobotid].strdemomode = "waitpos";
                                    robot_demoinfo[strrobotid].curr_urcnt = 0;
                                    bDockOnly = false;
                                }
                                else
                                {
                                    robot_demoinfo[strrobotid].curr_urcnt++;

                                    missionid = "MID20190916172735"; //MID20190916172735 dock만
                                    onDrive_crashcheck_add(strrobotid);

                                    robot_demoinfo[strrobotid].strdemomode = "dockonly_end";
                                }
                            }
                        }
                        #endregion

                        #region lift , line drive
                        if (strrobotid == strline1_robotid && bLiftOn)
                        {
                            if (robot_demoinfo[strrobotid].strdemomode == "liftline_start")
                            {
                                missionid = "MID20190807210301"; //MID20190807210301 리프트_라인1대기
                                onDrive_crashcheck_add(strrobotid);

                                robot_demoinfo[strrobotid].strdemomode = "liftline_wait";
                            }
                            else if (robot_demoinfo[strrobotid].strdemomode == "liftline_wait")
                            {
                                Data.Instance.Robot_work_info[strrobotid].nPriorityLevel = 2;

                                onLine_crashcheck_add(strrobotid);
                                onTaskPause(strrobotid);
                                
                                strline1_missionid = "MID20190807210333"; //MID20190807210333	라인1
                                robot_demoinfo[strrobotid].strdemomode = "liftline";
                                bLinedrive1_waitok = true;
                            }
                            else if (robot_demoinfo[strrobotid].strdemomode == "liftline")
                            {
                                missionid = "MID20190807210402"; //MID20190807210402	리프트_end
                                //onDrive_crashcheck_add(strrobotid);

                                robot_demoinfo[strrobotid].strdemomode = "liftline_end";
                            }
                            else if (robot_demoinfo[strrobotid].strdemomode == "liftline_end")
                            {
                                if (robotdemoinfo.curr_liftlinecnt == robotdemoinfo.liftlinecnt - 1)
                                {
                                    robotdemoinfo.strdemomode = "waitpos";
                                    robot_demoinfo[strrobotid].strdemomode = "waitpos";
                                    robot_demoinfo[strrobotid].curr_liftlinecnt = 0;
                                    bLiftOn = false;
                                }
                                else
                                {
                                    robot_demoinfo[strrobotid].curr_liftlinecnt++;

                                    missionid = "MID20190807210301"; //MID20190807210301 리프트_라인1대기
                                    onDrive_crashcheck_add(strrobotid);

                                    robot_demoinfo[strrobotid].strdemomode = "liftline_wait";
                                }
                            }

                        }

                        if (strrobotid == strline2_robotid && blinedrive)
                        {
                            if (robot_demoinfo[strrobotid].strdemomode == "line2_start")
                            {
                                missionid = "MID20190807210339"; //MID20190807210339	라인2대기	
                                onDrive_crashcheck_add(strrobotid);

                                robot_demoinfo[strrobotid].strdemomode = "line2_wait";
                            }
                            else if (robot_demoinfo[strrobotid].strdemomode == "line2_wait")
                            {
                                Data.Instance.Robot_work_info[strrobotid].nPriorityLevel = 1;

                                onLine_crashcheck_add(strrobotid);


                                onTaskPause(strrobotid);
                               

                                strline2_missionid = "MID20190807210353"; //MID20190807210353	라인2

                                robot_demoinfo[strrobotid].strdemomode = "line2";
                                bLinedrive2_waitok = true;
                            }
                            else if (robot_demoinfo[strrobotid].strdemomode == "line2")
                            {
                                missionid = "MID20190807210414"; //MID20190807210414	라인2_end
                                onDrive_crashcheck_add(strrobotid);

                                robot_demoinfo[strrobotid].strdemomode = "line2_end";
                            }
                            else if (robot_demoinfo[strrobotid].strdemomode == "line2_end")
                            {
                                if (robotdemoinfo.curr_line2cnt == robotdemoinfo.line2cnt - 1)
                                {
                                    robotdemoinfo.strdemomode = "waitpos";
                                    robot_demoinfo[strrobotid].strdemomode = "waitpos";
                                    robot_demoinfo[strrobotid].curr_line2cnt = 0;
                                    blinedrive = false;
                                }
                                else
                                {
                                    robot_demoinfo[strrobotid].curr_line2cnt++;

                                    missionid = "MID20190807210339"; //MID20190807210339	라인2대기	
                                    onDrive_crashcheck_add(strrobotid);

                                    robot_demoinfo[strrobotid].strdemomode = "line2_wait";
                                }
                            }

                        }

                        #endregion

                        #region S

                        if (strrobotid == strS1_robotid && bS1On)
                        {
                            if (robot_demoinfo[strrobotid].strdemomode == "S1_start")
                            {
                                missionid = "MID20190811171931"; //MID20190811171931 S자1대기
                                onDrive_crashcheck_add(strrobotid);

                                robot_demoinfo[strrobotid].strdemomode = "S1_wait";
                            }
                            else if (robot_demoinfo[strrobotid].strdemomode == "S1_wait")
                            {
                                Data.Instance.Robot_work_info[strrobotid].nPriorityLevel = 2;

                                onS_crashcheck_add(strrobotid);

                                onTaskPause(strrobotid);
                                
                                strS1_missionid = "MID20190811171944"; //MID20190811171944 S자1라인
                                robot_demoinfo[strrobotid].strdemomode = "S1line";
                                bSdrive1_waitok = true;
                            }
                            else if (robot_demoinfo[strrobotid].strdemomode == "S1line")
                            {
                            
                                bS2waitMove = false;
                                
                                missionid = "MID20190811171958"; //MID20190811171958 S자1end
                                onDrive_crashcheck_add(strrobotid);
                                robot_demoinfo[strrobotid].strdemomode = "S1_end";

                            }
                            else if (robot_demoinfo[strrobotid].strdemomode == "S1_end")
                            {
                                if (robotdemoinfo.curr_s1cnt == robotdemoinfo.s1cnt - 1)
                                {
                                    robotdemoinfo.strdemomode = "waitpos";
                                    robot_demoinfo[strrobotid].strdemomode = "waitpos";
                                    robot_demoinfo[strrobotid].curr_s1cnt = 0;
                                    bS1On = false;
                                }
                                else
                                {
                                    robot_demoinfo[strrobotid].curr_s1cnt++;

                                    missionid = "MID20190811171931"; //MID20190811171931 S자1대기
                                    onDrive_crashcheck_add(strrobotid);

                                    robot_demoinfo[strrobotid].strdemomode = "S1_wait";
                                }
                            }

                        }

                        if (strrobotid == strS2_robotid && bS2On)
                        {
                            if (robot_demoinfo[strrobotid].strdemomode == "S2_start")
                            {
                                missionid = "MID20190811172009"; //MID20190811172009 S자2대기
                                onDrive_crashcheck_add(strrobotid);

                                robot_demoinfo[strrobotid].strdemomode = "S2_wait";
                            }
                            else if (robot_demoinfo[strrobotid].strdemomode == "S2_wait")
                            {
                                Data.Instance.Robot_work_info[strrobotid].nPriorityLevel = 1;

                                onTaskPause(strrobotid);
                                onS_crashcheck_add(strrobotid);
                                strS1_missionid = "MID20190811172017"; //MID20190811172017 S자2라인
                                robot_demoinfo[strrobotid].strdemomode = "S2line";
                                bSdrive2_waitok = true;
                            }
                            else if(robot_demoinfo[strrobotid].strdemomode == "S2_linewait")
                            {
                                bs2_linewait_complete = true;
                                missionid = "";
                                onS_crashcheck_remove(strrobotid);
                                onTaskResum(strS1_robotid);

                            }
                            else if (robot_demoinfo[strrobotid].strdemomode == "S2line")
                            {
                                missionid = "MID20190811172028"; //MID20190811172028 S자2end
                                onDrive_crashcheck_add(strrobotid);

                                robot_demoinfo[strrobotid].strdemomode = "S2_end";
                            }
                            else if (robot_demoinfo[strrobotid].strdemomode == "S2_end")
                            {
                                if (robotdemoinfo.curr_s2cnt == robotdemoinfo.s2cnt - 1)
                                {
                                    robotdemoinfo.strdemomode = "waitpos";
                                    robot_demoinfo[strrobotid].strdemomode = "waitpos";
                                    robot_demoinfo[strrobotid].curr_s2cnt = 0;
                                    bS2On = false;
                                }
                                else
                                {
                                    robot_demoinfo[strrobotid].curr_s2cnt++;

                                    missionid = "MID20190811172009"; //MID20190811172009 S자2대기
                                    onDrive_crashcheck_add(strrobotid);

                                    robot_demoinfo[strrobotid].strdemomode = "S2_wait";
                                }
                            }

                        }

                        #endregion

                        if (robot_demoinfo[strrobotid].strdemomode == "waitpos")
                        {

                            //Data.Instance.bRunToWaitPos = true;
                            missionid = "";
                            //대기장소로 이동
                            int nwaitmissioncnt = strWaitPos_MissionID.Count();
                            for (int i = 0; i < nwaitmissioncnt; i++)
                            {
                                KeyValuePair<string, string> waitposmove_robotinfo = strWaitPosMove_Robot.ElementAt(i);
                                string strmissionid = waitposmove_robotinfo.Key;
                                string strrobot = waitposmove_robotinfo.Value;
                                string[] missionbuf = strmissionid.Split(',');


                                KeyValuePair<string, string> waitposmove_robotinfo_one = strWaitPosMove_Robot_one.ElementAt(i);
                                string strmissionid_one = waitposmove_robotinfo_one.Key;
                                string strrobot_one = waitposmove_robotinfo_one.Value;

                                if (strrobot == "")
                                {
                                    robot_demoinfo[strrobotid].strdemomode = "wait_pos_move";

                                    missionid = strmissionid;
                                    ncurrWaitRobotid_idx++;
                                    strWaitPosMove_Robot[strmissionid] = strrobotid;

                                    strWaitPosMove_Robot_one[strmissionid_one] = strrobotid;

                                    on_crashcheck_remove(strrobotid);

                                    Console.WriteLine("대기장소 ={0},robot={1}", i+1, strrobotid);
                                    onConsolemsgDp(string.Format("대기장소 ={0},robot={1}", i + 1, strrobotid));
                                    break;
                                }
                            }

                        }
                        else if (robot_demoinfo[strrobotid].strdemomode == "wait_pos_move")
                        {
                            robot_demoinfo[strrobotid].strdemomode = "basicmode";

                            onTaskPause(strrobotid);
                           // onTaskPause(strrobotid);
                            Console.WriteLine("대기장소 도착 ={0}", strrobotid);
                            onConsolemsgDp(string.Format("대기장소 도착 ={0}", strrobotid));

                            Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.action_indx = 0;

                            if (ncurrWaitRobotid_idx >= currTaskRobotid.Length)// - 1)
                            {

                                Console.WriteLine("모두 복귀");
                                onConsolemsgDp("모두 복귀");
                                Thread.Sleep(3000);
                                Console.WriteLine("다시 시작");
                                onConsolemsgDp("다시 시작");
                                ncurrWaitRobotid_idx = 0;
                                onDemoRuning(strMissionid_0);
                            }
                            else
                            {
                               
                            }
                        }
                    }

                    if (missionid != "")
                    {
                        mainform.commBridge.onMissionChange_publish(strrobotid, missionid);
                        robot_demoinfo[strrobotid].currmissionid = missionid;
                        //Thread.Sleep(100);
                        Console.WriteLine("mission change = {0},,robot={1}", missionid, strrobotid);
                        onConsolemsgDp(string.Format("mission change = {0},,robot={1}", missionid, strrobotid));
                    }

                    Thread.Sleep(100);
                    Data.Instance.bCrashcheckPause = false;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("onTaskFeedbak_Complete_ver2 err=" + ex.Message.ToString());
            }
        }

        private void timer_LinedrivewaitokChk_Tick(object sender, EventArgs e)
        {
            onRobotXYCheck();

            if (bLinedrive1_waitok && bLinedrive2_waitok)
            {
                //if (!Data.Instance.Robot_work_info[strline1_robotid].robot_status_info.taskfeedback.msg.feedback.is_paused || !Data.Instance.Robot_work_info[strline2_robotid].robot_status_info.taskfeedback.msg.feedback.is_paused)
                //    return;

                Thread.Sleep(2000);



                mainform.commBridge.onMissionChange_publish(strline1_robotid, strline1_missionid);
                robot_demoinfo[strline1_robotid].currmissionid = strline1_missionid;
                Console.WriteLine("mission change = {0},,robot={1}", strline1_missionid, strline1_robotid);
                onConsolemsgDp(string.Format("mission change = {0},,robot={1}", strline1_missionid, strline1_robotid));
                onTaskResum(strline1_robotid); //tt


                mainform.commBridge.onMissionChange_publish(strline2_robotid, strline2_missionid);
                robot_demoinfo[strline2_robotid].currmissionid = strline2_missionid;
                Console.WriteLine("mission change = {0},,robot={1}", strline2_missionid, strline2_robotid);
                onConsolemsgDp(string.Format("mission change = {0},,robot={1}", strline2_missionid, strline2_robotid));
                onTaskResum(strline2_robotid); //tt

                strline1_missionid = "";
                strline2_missionid = "";

                bLinedrive1_waitok = false;
                bLinedrive2_waitok = false;
            }

            if (bSdrive1_waitok && bSdrive2_waitok)
            {

                //Thread.Sleep(1000);

                //mainform.commBridge.onMissionChange_publish(strS1_robotid, strS1_missionid);
                //Console.WriteLine("mission change = {0},,robot={1}", strS1_missionid, strS1_robotid);
                //onTaskResum(strS1_robotid);

                //mainform.commBridge.onMissionChange_publish(strS2_robotid, strS2_missionid);
                //Console.WriteLine("mission change = {0},,robot={1}", strS2_missionid, strS2_robotid);
                //onTaskResum(strS2_robotid);

                strS1_missionid = "";
                strS2_missionid = "";

                bSdrive1_waitok = false;
                bSdrive2_waitok = false;

            }
        }

        public void TaskFeedback_Ing(string strrobotid)
        {
            try
            {
                bool bmissionChg_Line = false;
                if (Data.Instance.bMissionCompleteCheck)
                {

                    if (!chkDemo.Checked) return;

                    int nactidx = Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.action_indx;
                    bool bact_complete = Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.act_complete;

                    robot_demoinfo[strrobotid].actcomplete = Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.act_complete;

                    string missionid = Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.work_id;
                    //if (bLiftOnly && strrobotid == strLiftonly_robotid && missionid == "MID20190811145027")

                    if (strrobotid == strline1_robotid && missionid == "MID20190807210301") //MID20190807210301 리프트_라인1대기
                    {

                        if ((nactidx == nlift1_crash_skipidx && robot_demoinfo[strrobotid].actcomplete) || (nactidx == nlift1_crash_skipidx2 && robot_demoinfo[strrobotid].actcomplete))
                        {
                            Console.WriteLine("onDrive_crashcheck skip = {0} ... actidx ={1}", strrobotid, nactidx);

                            Data.Instance.bCrashcheckPause = true;
                            Thread.Sleep(100);

                            on_crashcheck_remove(strrobotid);

                            int runcnt = crashrobot_run_list.Count;

                            bool bskip = false;
                            if (runcnt > 0)
                            {
                                for (int r = 0; r < runcnt; r++)
                                {
                                    if (crashrobot_run_list[r] == strrobotid)
                                    {
                                        onTaskResum(crashrobot_pause_list[r]);
                                        crashrobot_run_list.RemoveAt(r);
                                        crashrobot_pause_list.RemoveAt(r);
                                        break;
                                    }
                                }
                            }
                            Data.Instance.bCrashcheckPause = false;
                            Thread.Sleep(100);
                        }
                        else if (nactidx == nlift1_crash_chkidx && robot_demoinfo[strrobotid].actcomplete)
                        {
                            Console.WriteLine("onDrive_crashcheck add = {0} ... actidx ={1}", strrobotid, nactidx);


                            Data.Instance.bCrashcheckPause = true;
                            Thread.Sleep(100);

                            onDrive_crashcheck_add(strrobotid);

                            Data.Instance.bCrashcheckPause = false;
                            Thread.Sleep(100);
                        }

                    }

                    if (strrobotid == strline1_robotid && missionid == "MID20190807210402") //MID20190807210402	리프트_end
                    {

                        if (nactidx == nlift1_end_crash_chkidx && robot_demoinfo[strrobotid].actcomplete)
                        {
                            Console.WriteLine("onDrive_crashcheck add = {0} ... actidx ={1}", strrobotid, nactidx);

                            Data.Instance.bCrashcheckPause = true;
                            Thread.Sleep(100);

                            onDrive_crashcheck_add(strrobotid);

                            Data.Instance.bCrashcheckPause = false;
                            Thread.Sleep(100);
                        }

                    }



                    if (strrobotid == strline2_robotid && missionid == "MID20190807210339") //MID20190807210339 라인2대기
                    {

                        if ((nactidx == nlift2_crash_skipidx && robot_demoinfo[strrobotid].actcomplete))// || (nactidx == 7 && robot_demoinfo[strrobotid].actcomplete))
                        {
                            Console.WriteLine("onDrive_crashcheck skip = {0} ... actidx ={1}", strrobotid, nactidx);

                            Data.Instance.bCrashcheckPause = true;
                            Thread.Sleep(100);

                            on_crashcheck_remove(strrobotid);

                            int runcnt = crashrobot_run_list.Count;

                            bool bskip = false;
                            if (runcnt > 0)
                            {
                                for (int r = 0; r < runcnt; r++)
                                {
                                    if (crashrobot_run_list[r] == strrobotid)
                                    {
                                        onTaskResum(crashrobot_pause_list[r]);
                                        crashrobot_run_list.RemoveAt(r);
                                        crashrobot_pause_list.RemoveAt(r);
                                        break;
                                    }
                                }
                            }
                            Data.Instance.bCrashcheckPause = false;
                            Thread.Sleep(100);
                        }

                    }


                    if (strrobotid == strSonly_robotid && missionid == "MID20190808161838") //MID20190808161838	S자만대기
                    {

                        if ((nactidx == nS_crash_skipidx && robot_demoinfo[strrobotid].actcomplete))
                        {
                            Console.WriteLine("onDrive_crashcheck skip = {0} ... actidx ={1}", strrobotid, nactidx);

                            Data.Instance.bCrashcheckPause = true;
                            Thread.Sleep(100);

                            on_crashcheck_remove(strrobotid);

                            int runcnt = crashrobot_run_list.Count;

                            bool bskip = false;
                            if (runcnt > 0)
                            {
                                for (int r = 0; r < runcnt; r++)
                                {
                                    if (crashrobot_run_list[r] == strrobotid)
                                    {
                                        onTaskResum(crashrobot_pause_list[r]);
                                        crashrobot_run_list.RemoveAt(r);
                                        crashrobot_pause_list.RemoveAt(r);
                                        break;
                                    }
                                }
                            }
                            Data.Instance.bCrashcheckPause = false;
                            Thread.Sleep(100);
                        }

                    }


                    if (strrobotid == strURonly_robotid && missionid == "MID20190812154402") //ur 미션
                    {
                        if (nactidx == nUR_crash_skipidx && robot_demoinfo[strrobotid].actcomplete)
                        {
                            Data.Instance.bCrashcheckPause = true;
                            Thread.Sleep(100);

                            on_crashcheck_remove(strrobotid);

                            int runcnt = crashrobot_run_list.Count;

                            bool bskip = false;
                            if (runcnt > 0)
                            {
                                for (int r = 0; r < runcnt; r++)
                                {
                                    if (crashrobot_run_list[r] == strrobotid)
                                    {
                                        onTaskResum(crashrobot_pause_list[r]);
                                        crashrobot_run_list.RemoveAt(r);
                                        crashrobot_pause_list.RemoveAt(r);
                                        break;
                                    }
                                }
                            }

                            Data.Instance.bCrashcheckPause = false;
                            Thread.Sleep(100);

                        }
                        else if (nactidx == nUR_crash_chkidx && robot_demoinfo[strrobotid].actcomplete)
                        {
                            Data.Instance.bCrashcheckPause = true;
                            Thread.Sleep(100);

                            onDrive_crashcheck_add(strrobotid);

                            Data.Instance.bCrashcheckPause = false;
                            Thread.Sleep(100);
                        }

                    }

                    if (strrobotid == strDocknly_robotid && missionid == "MID20190916172735") //Dock 미션
                    {
                        if (nactidx == nDock_crash_skipidx && robot_demoinfo[strrobotid].actcomplete)
                        {
                            Data.Instance.bCrashcheckPause = true;
                            Thread.Sleep(100);

                            on_crashcheck_remove(strrobotid);

                            int runcnt = crashrobot_run_list.Count;

                            bool bskip = false;
                            if (runcnt > 0)
                            {
                                for (int r = 0; r < runcnt; r++)
                                {
                                    if (crashrobot_run_list[r] == strrobotid)
                                    {
                                        onTaskResum(crashrobot_pause_list[r]);
                                        crashrobot_run_list.RemoveAt(r);
                                        crashrobot_pause_list.RemoveAt(r);
                                        break;
                                    }
                                }
                            }

                            Data.Instance.bCrashcheckPause = false;
                            Thread.Sleep(100);

                        }
                        else if (nactidx == nDock_crash_chkidx && robot_demoinfo[strrobotid].actcomplete)
                        {
                            Data.Instance.bCrashcheckPause = true;
                            Thread.Sleep(100);

                            onDrive_crashcheck_add(strrobotid);

                            Data.Instance.bCrashcheckPause = false;
                            Thread.Sleep(100);
                        }

                    }

                    if (bs2_linewait_complete && missionid == "MID20190811171958" && nactidx == 1)
                    {

                        bs2_linewait_complete = false;

                        onS_crashcheck_add(strrobotid);
                        string tempmissionid = "MID20190811172017"; //MID20190811172017 S자2라인
                        robot_demoinfo[strrobotid].strdemomode = "S2line";
                        mainform.commBridge.onMissionChange_publish(strrobotid, tempmissionid);

                        robot_demoinfo[strrobotid].currmissionid = tempmissionid;

                        //Thread.Sleep(100);
                        Console.WriteLine("mission change = {0},,robot={1}", tempmissionid, strrobotid);

                        onConsolemsgDp(string.Format("mission change = {0},,robot={1}", tempmissionid, strrobotid));
                    }
                    robot_demoinfo[strrobotid].actcomplete = false;
                    bact_complete = false;

                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("TaskFeedback_Ing err=" + ex.Message.ToString());
            }

        }

        private void onDrive_crashcheck_add(string strrobotid)
        {
            try
            {
                Console.WriteLine("onDrive_crashcheck_add = {0}", strrobotid);
                onConsolemsgDp(string.Format("onDrive_crashcheck_add = {0}", strrobotid));
                int ncheck_line_cnt = CrashCheckRobot_Linedrive_list.Count;
                if (ncheck_line_cnt > 0)
                {
                    for (int i = 0; i < ncheck_line_cnt; i++)
                    {
                        if (CrashCheckRobot_Linedrive_list[i] == strrobotid)
                        {
                            CrashCheckRobot_Linedrive_list.RemoveAt(i);

                            break;
                        }
                    }
                }

                int ncheck_S_cnt = CrashCheckRobot_Sdrive_list.Count;
                if (ncheck_S_cnt > 0)
                {
                    for (int i = 0; i < ncheck_S_cnt; i++)
                    {
                        if (CrashCheckRobot_Sdrive_list[i] == strrobotid)
                        {
                            CrashCheckRobot_Sdrive_list.RemoveAt(i);

                            break;
                        }
                    }
                }



                int ncheckcnt = CrashCheckRobot_list.Count;
                bool bchk = false;
                if (ncheckcnt > 0)
                {
                    for (int i = 0; i < ncheckcnt; i++)
                    {
                        if (CrashCheckRobot_list[i] == strrobotid)
                        {
                            bchk = true;
                            break;
                        }
                    }
                }

                if (!bchk)
                {
                    CrashCheckRobot_list.Add(strrobotid);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("onDrive_crashcheck_add err=" + ex.Message.ToString());
            }
        }

        private void onLine_crashcheck_add(string strrobotid)
        {
            try
            {
                Console.WriteLine("onLine_crashcheck_add = {0}", strrobotid);
                onConsolemsgDp(string.Format("onLine_crashcheck_add = {0}", strrobotid));
                int ncheckcnt = CrashCheckRobot_list.Count;
                if (ncheckcnt > 0)
                {
                    for (int i = 0; i < ncheckcnt; i++)
                    {
                        if (CrashCheckRobot_list[i] == strrobotid)
                        {
                            CrashCheckRobot_list.RemoveAt(i);

                            break;
                        }
                    }
                }

                int runcnt = crashrobot_run_list.Count;


                if (runcnt > 0)
                {
                    for (int r = 0; r < runcnt; r++)
                    {
                        if (crashrobot_run_list[r] == strrobotid)
                        {
                            onTaskResum(crashrobot_pause_list[r]);
                            crashrobot_run_list.RemoveAt(r);
                            crashrobot_pause_list.RemoveAt(r);
                            break;
                        }
                    }
                }


                int nchecklinecnt = CrashCheckRobot_Linedrive_list.Count;

                bool bchk = false;
                if (nchecklinecnt > 0)
                {

                    for (int i = 0; i < nchecklinecnt; i++)
                    {
                        if (CrashCheckRobot_Linedrive_list[i] == strrobotid)
                        {
                            bchk = true;
                            break;
                        }
                    }
                }
                if (!bchk)
                {
                    CrashCheckRobot_Linedrive_list.Add(strrobotid);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("onLine_crashcheck_add err=" + ex.Message.ToString());
            }
        }

        private void onS_crashcheck_add(string strrobotid)
        {
            try
            {
                Console.WriteLine("onS_crashcheck_add = {0}", strrobotid);
                onConsolemsgDp(string.Format("onS_crashcheck_add = {0}", strrobotid));

                int ncheckcnt = CrashCheckRobot_list.Count;
                if (ncheckcnt > 0)
                {
                    for (int i = 0; i < ncheckcnt; i++)
                    {
                        if (CrashCheckRobot_list[i] == strrobotid)
                        {
                            CrashCheckRobot_list.RemoveAt(i);

                            break;
                        }
                    }
                }

                int runcnt = crashrobot_run_list.Count;


                if (runcnt > 0)
                {
                    for (int r = 0; r < runcnt; r++)
                    {
                        if (crashrobot_run_list[r] == strrobotid)
                        {
                            onTaskResum(crashrobot_pause_list[r]);
                            crashrobot_run_list.RemoveAt(r);
                            crashrobot_pause_list.RemoveAt(r);
                            break;
                        }
                    }
                }


                int ncheck_line_cnt = CrashCheckRobot_Linedrive_list.Count;
                if (ncheck_line_cnt > 0)
                {
                    for (int i = 0; i < ncheck_line_cnt; i++)
                    {
                        if (CrashCheckRobot_Linedrive_list[i] == strrobotid)
                        {
                            CrashCheckRobot_Linedrive_list.RemoveAt(i);

                            break;
                        }
                    }
                }

                int ncheck_S_cnt = CrashCheckRobot_Sdrive_list.Count;
                bool bchk = false;
                if (ncheck_S_cnt > 0)
                {
                    for (int i = 0; i < ncheck_S_cnt; i++)
                    {
                        if (CrashCheckRobot_Sdrive_list[i] == strrobotid)
                        {
                            bchk = true;
                            break;
                        }
                    }
                }

                if (!bchk)
                {
                    CrashCheckRobot_Sdrive_list.Add(strrobotid);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("onS_crashcheck_add err=" + ex.Message.ToString());
            }
        }

        private void on_crashcheck_remove(string strrobotid)
        {
            try
            {
                Console.WriteLine("on_crashcheck_remove = {0}", strrobotid);
                onConsolemsgDp(string.Format("on_crashcheck_remove = {0}", strrobotid));

                int ncheckcnt = CrashCheckRobot_list.Count;
                if (ncheckcnt > 0)
                {
                    for (int ii = 0; ii < ncheckcnt; ii++)
                    {
                        if (CrashCheckRobot_list[ii] == strrobotid)
                        {
                            CrashCheckRobot_list.RemoveAt(ii);

                            break;
                        }
                    }
                }

                int runcnt = crashrobot_run_list.Count;

                if (runcnt > 0)
                {
                    for (int r = 0; r < runcnt; r++)
                    {
                        if (crashrobot_run_list[r] == strrobotid)
                        {
                            onTaskResum(crashrobot_pause_list[r]);
                            crashrobot_run_list.RemoveAt(r);
                            crashrobot_pause_list.RemoveAt(r);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("on_crashcheck_remove err=" + ex.Message.ToString());
            }
        }


        private void onS_crashcheck_remove(string strrobotid)
        {
            try
            {
                Console.WriteLine("onS_crashcheck_remove = {0}", strrobotid);

                int ncheckcnt = CrashCheckRobot_Sdrive_list.Count;
                if (ncheckcnt > 0)
                {
                    for (int ii = 0; ii < ncheckcnt; ii++)
                    {
                        if (CrashCheckRobot_Sdrive_list[ii] == strrobotid)
                        {
                            CrashCheckRobot_Sdrive_list.RemoveAt(ii);

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("on_crashcheck_remove err=" + ex.Message.ToString());
            }
        }

        public void onCurrMisionDP(string strrobot, string missionid)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                if(strrobot=="R_000")  textBox1.Text = missionid;
                else if (strrobot == "R_001") textBox2.Text = missionid;
                else if (strrobot == "R_002") textBox3.Text = missionid;
                else if (strrobot == "R_003") textBox4.Text = missionid;
                else if (strrobot == "R_004") textBox5.Text = missionid;
                else if (strrobot == "R_005") textBox6.Text = missionid;
                else if (strrobot == "R_006") textBox7.Text = missionid;

            }));
        }

        private void onRetryMissionchg(string strrobot, string strmission)
        {
            if (Data.Instance.isConnected)
            {
                //onTaskPause(strrobot);
               
                mainform.commBridge.onMissionChange_publish(strrobot, strmission);
                Console.WriteLine("mission Retry change = {0},,robot={1}", strmission, strrobot);
                onConsolemsgDp(string.Format("mission Retry change = {0},,robot={1}", strmission, strrobot));
                robot_demoinfo[strrobot].currmissionid = strmission;
                //Thread.Sleep(300);
              //  onTaskResum(strrobot);
            }
           
        }


        #region Crash Check(Driving) 

        List<string> CrashCheckRobot_list = new List<string>();
        Thread crashchk_thread;
        List<string> crashrobot_run_list = new List<string>();
        List<string> crashrobot_pause_list = new List<string>();

        private void onCrashCheck_Thread()
        {
            for (; ; )
            {
                if (Data.Instance.bCrashcheckStop) break;

                if (Data.Instance.bCrashcheckPause) { }
                else
                    onCrashCheck();

                Thread.Sleep(50);
            }
        }

        private void onCrashCheck()
        {
            try
            {

                int ncheckcnt = CrashCheckRobot_list.Count;
                if (ncheckcnt > 0)
                {
                    Invoke(new MethodInvoker(delegate ()
                    {
                        listBox5.Items.Clear();
                        for (int ii = 0; ii < ncheckcnt; ii++)
                        {
                            string strrobot = CrashCheckRobot_list[ii];
                            listBox5.Items.Add(strrobot);
                        }
                    }));


                    string sourceRobot = "";
                    string targetRobot = "";
                    for (int i = 0; i < ncheckcnt; i++)
                    {
                        sourceRobot = CrashCheckRobot_list[i];

                        for (int j = 0; j < ncheckcnt; j++)
                        {
                            targetRobot = CrashCheckRobot_list[j];

                            if (sourceRobot != targetRobot)
                            {
                                string strmsg2 = string.Format("{0}_{1}", sourceRobot, targetRobot);

                                if (Data.Instance.Robot_work_info[sourceRobot].robot_status_info.robotstate == null)
                                {
                                    string strmsg1 = string.Format("source={0}, robotstate fail", sourceRobot);

                                    Invoke(new MethodInvoker(delegate ()
                                    {
                                        listBox1.Items.Add(strmsg1);
                                        listBox1.SelectedIndex = listBox1.Items.Count - 1;
                                    }));
                                    break;
                                }
                                if (Data.Instance.Robot_work_info[sourceRobot].robot_status_info.robotstate.msg == null)
                                {
                                    string strmsg1 = string.Format("source={0}, robotstate msg fail", sourceRobot);

                                    Invoke(new MethodInvoker(delegate ()
                                    {
                                        listBox1.Items.Add(strmsg1);
                                        listBox1.SelectedIndex = listBox1.Items.Count - 1;
                                    }));
                                    break;
                                }

                                if (Data.Instance.Robot_work_info[sourceRobot].robot_status_info.lookahead == null)
                                {
                                    string strmsg1 = string.Format("source={0}, lookahead fail", sourceRobot);

                                    Invoke(new MethodInvoker(delegate ()
                                    {
                                        listBox1.Items.Add(strmsg1);
                                        listBox1.SelectedIndex = listBox1.Items.Count - 1;
                                    }));
                                    break;
                                }
                                if (Data.Instance.Robot_work_info[sourceRobot].robot_status_info.lookahead.msg == null)
                                {
                                    string strmsg1 = string.Format("source={0}, lookahead  msg fail", sourceRobot);

                                    Invoke(new MethodInvoker(delegate ()
                                    {
                                        listBox1.Items.Add(strmsg1);
                                        listBox1.SelectedIndex = listBox1.Items.Count - 1;
                                    }));
                                    break;
                                }

                                if (Data.Instance.Robot_work_info[targetRobot].robot_status_info.robotstate == null)
                                {
                                    string strmsg1 = string.Format("targetRobot={0}, robotstate fail", sourceRobot);

                                    Invoke(new MethodInvoker(delegate ()
                                    {
                                        listBox1.Items.Add(strmsg1);
                                        listBox1.SelectedIndex = listBox1.Items.Count - 1;
                                    }));
                                    continue;
                                }
                                if (Data.Instance.Robot_work_info[targetRobot].robot_status_info.robotstate.msg == null)
                                {
                                    string strmsg1 = string.Format("targetRobot={0}, robotstate  msg fail", sourceRobot);

                                    Invoke(new MethodInvoker(delegate ()
                                    {
                                        listBox1.Items.Add(strmsg1);
                                        listBox1.SelectedIndex = listBox1.Items.Count - 1;
                                    }));
                                    continue;
                                }

                                if (Data.Instance.Robot_work_info[targetRobot].robot_status_info.lookahead == null)
                                {
                                    string strmsg1 = string.Format("targetRobot={0}, lookahead   fail", sourceRobot);

                                    Invoke(new MethodInvoker(delegate ()
                                    {
                                        listBox1.Items.Add(strmsg1);
                                        listBox1.SelectedIndex = listBox1.Items.Count - 1;
                                    }));
                                    continue;
                                }
                                if (Data.Instance.Robot_work_info[targetRobot].robot_status_info.lookahead.msg == null)
                                {
                                    string strmsg1 = string.Format("targetRobot={0}, lookahead  msg  fail", sourceRobot);

                                    Invoke(new MethodInvoker(delegate ()
                                    {
                                        listBox1.Items.Add(strmsg1);
                                        listBox1.SelectedIndex = listBox1.Items.Count - 1;
                                    }));
                                    continue;
                                }

                                double source_x = 0;
                                double source_y = 0;
                                double source_lookahead_x = 0;
                                double source_lookahead_y = 0;

                                int nsource_priority = 0;

                                double target_x = 0;
                                double target_y = 0;
                                double target_lookahead_x = 0;
                                double target_lookahead_y = 0;
                                int ntargetpriority = 0;


                                source_x = Data.Instance.Robot_work_info[sourceRobot].robot_status_info.robotstate.msg.pose.x;
                                source_y = Data.Instance.Robot_work_info[sourceRobot].robot_status_info.robotstate.msg.pose.y;
                                source_lookahead_x = Data.Instance.Robot_work_info[sourceRobot].robot_status_info.lookahead.msg.point.x;
                                source_lookahead_y = Data.Instance.Robot_work_info[sourceRobot].robot_status_info.lookahead.msg.point.y;
                                nsource_priority = Data.Instance.Robot_work_info[sourceRobot].nPriorityLevel;

                                target_x = Data.Instance.Robot_work_info[targetRobot].robot_status_info.robotstate.msg.pose.x;
                                target_y = Data.Instance.Robot_work_info[targetRobot].robot_status_info.robotstate.msg.pose.y;
                                target_lookahead_x = Data.Instance.Robot_work_info[targetRobot].robot_status_info.lookahead.msg.point.x;
                                target_lookahead_y = Data.Instance.Robot_work_info[targetRobot].robot_status_info.lookahead.msg.point.y;
                                ntargetpriority = Data.Instance.Robot_work_info[targetRobot].nPriorityLevel;


                                double dist_LA = mainform.onPointToPointDist(source_lookahead_x, source_lookahead_y, target_lookahead_x, target_lookahead_y);
                                double dist_SourceCenToTargetLA = mainform.onPointToPointDist(source_x, source_y, target_lookahead_x, target_lookahead_y);
                                double dist_TargetCenToSourceLA = mainform.onPointToPointDist(target_x, target_y, source_lookahead_x, source_lookahead_y);




                                string strmsg = string.Format("{0}_{1} LA ={2:f2}, SourceCenToTargetLA={3:f2}, TargetCenToSourceLA={4:f2}", sourceRobot, targetRobot, dist_LA, dist_SourceCenToTargetLA, dist_TargetCenToSourceLA);
                                strmsg2 = string.Format("{0}_{1}", sourceRobot, targetRobot);
                                Invoke(new MethodInvoker(delegate ()
                                {
                                    listBox1.Items.Add(strmsg2);
                                    listBox1.Items.Add(strmsg);
                                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                                }));
                                if (dist_LA < 2.5 && dist_LA > 0) //crash warnning
                                {
                                    int runcnt = crashrobot_run_list.Count;

                                    bool bskip = false;
                                    if (runcnt > 0)
                                    {
                                        for (int r = 0; r < runcnt; r++)
                                        {
                                            if (crashrobot_run_list[r] == sourceRobot && crashrobot_pause_list[r] == targetRobot)
                                            {
                                                bskip = true;
                                                break;
                                                //return;
                                            }
                                            else if (crashrobot_run_list[r] == targetRobot && crashrobot_pause_list[r] == sourceRobot)
                                            {
                                                bskip = true;
                                                break;
                                                //return;
                                            }
                                        }
                                    }

                                    if (!bskip)
                                    {

                                        if (dist_SourceCenToTargetLA < dist_TargetCenToSourceLA) //source move , target pause
                                        {
                                            onCrashCheck_ToProcessRobot(sourceRobot, targetRobot);
                                        }
                                        else
                                        {
                                            onCrashCheck_ToProcessRobot(targetRobot, sourceRobot);
                                        }
                                    }
                                }
                                else
                                {
                                    int nlistcnt = crashrobot_run_list.Count;
                                    if (nlistcnt > 0)
                                    {
                                        for (int nix = 0; nix < nlistcnt; nix++)
                                        {
                                            string strlist_source = crashrobot_run_list[nix];
                                            string strlist_target = crashrobot_pause_list[nix];

                                            if (sourceRobot == strlist_source && targetRobot == strlist_target)
                                            {
                                                if (Data.Instance.Robot_work_info[targetRobot].robot_status_info.taskfeedback.msg.feedback.is_paused)
                                                {
                                                    onTaskResum(targetRobot);
                                                    //Thread.Sleep(300);
                                                    crashrobot_run_list.RemoveAt(nix);
                                                    crashrobot_pause_list.RemoveAt(nix);
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }

                                Invoke(new MethodInvoker(delegate ()
                                {
                                    if (crashrobot_run_list.Count > 0)
                                        listBox2.Items.Clear();

                                    for (int k = 0; k < crashrobot_run_list.Count; k++)
                                    {
                                        string strtemp = string.Format("run={0},pause={1}", crashrobot_run_list[k], crashrobot_pause_list[k]);
                                        listBox2.Items.Add(strtemp);
                                    }
                                    listBox2.SelectedIndex = listBox2.Items.Count - 1;
                                }));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("onCrashCheck err=" + ex.Message.ToString());
            }
        }

        private void onCrashCheck_ToProcessRobot(string strRunrobot, string strPauserobot)
        {
            if (!Data.Instance.Robot_work_info[strPauserobot].robot_status_info.taskfeedback.msg.feedback.is_paused)//&& !Data.Instance.Robot_work_info[strRunrobot].robot_status_info.taskfeedback.msg.feedback.is_paused)
            {
                onTaskPause(strPauserobot);
                //Thread.Sleep(300);

                crashrobot_run_list.Add(strRunrobot);
                crashrobot_pause_list.Add(strPauserobot);

                Invoke(new MethodInvoker(delegate ()
                {
                    listBox1.Items.Add(string.Format("{0:f2}.. pause", strPauserobot));
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                }));

                onConsolemsgDp(string.Format("crash pause = {0:f2}.. pause", strPauserobot));
                Console.WriteLine("crash pause = {0:f2}.. pause", strPauserobot);
            }
        }

        #endregion

        #region Crash Check(Line Driving) 

        List<string> CrashCheckRobot_Linedrive_list = new List<string>();
        Thread crashchk_Linedrive_thread;
        List<string> crashrobot_Linedrive_run_list = new List<string>();
        List<string> crashrobot_Linedrive_pause_list = new List<string>();

        bool bTmpMove = false;
        string bTmpMove_pause_robotid = "";
        string bTmpMove_run_robotid = "";

        private void onCrashCheck_Linedrive_Thread()
        {
            for (; ; )
            {
                if (Data.Instance.bCrashcheckStop) break;

                if (Data.Instance.bCrashcheckPause) { }
                else
                    onCrashCheckk_Linedrive();

                Thread.Sleep(50);
            }
        }

        private void onCrashCheckk_Linedrive()
        {
            try
            {

                int ncheckcnt = CrashCheckRobot_Linedrive_list.Count;
                if (ncheckcnt > 0)
                {
                    string sourceRobot = "";
                    string targetRobot = "";
                    for (int i = 0; i < ncheckcnt; i++)
                    {
                        sourceRobot = CrashCheckRobot_Linedrive_list[i];

                        for (int j = 0; j < ncheckcnt; j++)
                        {
                            targetRobot = CrashCheckRobot_Linedrive_list[j];

                            if (sourceRobot != targetRobot)
                            {
                                string strmsg2 = string.Format("{0}_{1}", sourceRobot, targetRobot);

                                if (Data.Instance.Robot_work_info[sourceRobot].robot_status_info.robotstate == null)
                                {

                                    break;
                                }
                                if (Data.Instance.Robot_work_info[sourceRobot].robot_status_info.robotstate.msg == null)
                                {
                                    break;
                                }

                                if (Data.Instance.Robot_work_info[sourceRobot].robot_status_info.lookahead == null)
                                {
                                    break;
                                }
                                if (Data.Instance.Robot_work_info[sourceRobot].robot_status_info.lookahead.msg == null)
                                {
                                    break;
                                }

                                if (Data.Instance.Robot_work_info[targetRobot].robot_status_info.robotstate == null)
                                {

                                    continue;
                                }
                                if (Data.Instance.Robot_work_info[targetRobot].robot_status_info.robotstate.msg == null)
                                {

                                    continue;
                                }

                                if (Data.Instance.Robot_work_info[targetRobot].robot_status_info.lookahead == null)
                                {

                                    continue;
                                }
                                if (Data.Instance.Robot_work_info[targetRobot].robot_status_info.lookahead.msg == null)
                                {

                                    continue;
                                }

                                double source_x = 0;
                                double source_y = 0;
                                double source_lookahead_x = 0;
                                double source_lookahead_y = 0;

                                int nsource_priority = 0;

                                double target_x = 0;
                                double target_y = 0;
                                double target_lookahead_x = 0;
                                double target_lookahead_y = 0;
                                int ntargetpriority = 0;


                                source_x = Data.Instance.Robot_work_info[sourceRobot].robot_status_info.robotstate.msg.pose.x;
                                source_y = Data.Instance.Robot_work_info[sourceRobot].robot_status_info.robotstate.msg.pose.y;
                                source_lookahead_x = Data.Instance.Robot_work_info[sourceRobot].robot_status_info.lookahead.msg.point.x;
                                source_lookahead_y = Data.Instance.Robot_work_info[sourceRobot].robot_status_info.lookahead.msg.point.y;
                                nsource_priority = Data.Instance.Robot_work_info[sourceRobot].nPriorityLevel;

                                target_x = Data.Instance.Robot_work_info[targetRobot].robot_status_info.robotstate.msg.pose.x;
                                target_y = Data.Instance.Robot_work_info[targetRobot].robot_status_info.robotstate.msg.pose.y;
                                target_lookahead_x = Data.Instance.Robot_work_info[targetRobot].robot_status_info.lookahead.msg.point.x;
                                target_lookahead_y = Data.Instance.Robot_work_info[targetRobot].robot_status_info.lookahead.msg.point.y;
                                ntargetpriority = Data.Instance.Robot_work_info[targetRobot].nPriorityLevel;


                                double dist_LA = mainform.onPointToPointDist(source_lookahead_x, source_lookahead_y, target_lookahead_x, target_lookahead_y);
                                double dist_SourceCenToTargetLA = mainform.onPointToPointDist(source_x, source_y, target_lookahead_x, target_lookahead_y);
                                double dist_TargetCenToSourceLA = mainform.onPointToPointDist(target_x, target_y, source_lookahead_x, source_lookahead_y);


                                string strmsg = string.Format("{0}_{1} LA ={2:f2}, SourceCenToTargetLA={3:f2}, TargetCenToSourceLA={4:f2}", sourceRobot, targetRobot, dist_LA, dist_SourceCenToTargetLA, dist_TargetCenToSourceLA);
                                strmsg2 = string.Format("{0}_{1}", sourceRobot, targetRobot);
                                Invoke(new MethodInvoker(delegate ()
                                {
                                    listBox3.Items.Add(strmsg2);
                                    listBox3.Items.Add(strmsg);
                                    listBox3.SelectedIndex = listBox3.Items.Count - 1;
                                }));
                                if (dist_LA < 3 && dist_LA > 0) //crash warnning
                                {

                                    if (nsource_priority != ntargetpriority) //priority check
                                    {
                                        if (nsource_priority > ntargetpriority) //source move, target pause
                                        {
                                            if (!Data.Instance.Robot_work_info[targetRobot].robot_status_info.taskfeedback.msg.feedback.is_paused && !bTmpMove)
                                            {
                                                onTaskPause(sourceRobot);
                                                onTaskPause(targetRobot);
                                                //Thread.Sleep(1000);

                                                crashrobot_Linedrive_run_list.Add(sourceRobot);
                                                crashrobot_Linedrive_pause_list.Add(targetRobot);
                                                float direction = 0;
                                                float distance = 0;

                                                onTempomove(sourceRobot, targetRobot, (float)1.8, "30", "right");

                                                Invoke(new MethodInvoker(delegate ()
                                                {
                                                    listBox3.Items.Add(string.Format("{0:f2}.. pause", targetRobot));
                                                    listBox3.SelectedIndex = listBox3.Items.Count - 1;
                                                }));
                                            }
                                        }
                                        else
                                        {
                                            if (!Data.Instance.Robot_work_info[sourceRobot].robot_status_info.taskfeedback.msg.feedback.is_paused && !bTmpMove)
                                            {
                                                onTaskPause(sourceRobot);
                                                onTaskPause(targetRobot);
                                                //Thread.Sleep(1000);

                                                crashrobot_Linedrive_run_list.Add(targetRobot);
                                                crashrobot_Linedrive_pause_list.Add(sourceRobot);
                                                float direction = 0;
                                                float distance = 0;


                                                onTempomove(targetRobot, sourceRobot, (float)1.8, "30", "right");


                                                Invoke(new MethodInvoker(delegate ()
                                                {
                                                    listBox3.Items.Add(string.Format("{0:f2}.. pause", sourceRobot));
                                                    listBox3.SelectedIndex = listBox3.Items.Count - 1;
                                                }));
                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    int nlistcnt = crashrobot_Linedrive_run_list.Count;
                                    if (nlistcnt > 0)
                                    {
                                        for (int nix = 0; nix < nlistcnt; nix++)
                                        {
                                            string strlist_source = crashrobot_Linedrive_run_list[nix];
                                            string strlist_target = crashrobot_Linedrive_pause_list[nix];

                                            if (sourceRobot == strlist_source && targetRobot == strlist_target)
                                            {
                                                if (Data.Instance.Robot_work_info[targetRobot].robot_status_info.taskfeedback.msg.feedback.is_paused)
                                                {
                                                    onTaskResum(targetRobot);
                                                    //Thread.Sleep(300);
                                                    crashrobot_Linedrive_run_list.RemoveAt(nix);
                                                    crashrobot_Linedrive_pause_list.RemoveAt(nix);
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }

                                Invoke(new MethodInvoker(delegate ()
                                {
                                    if (crashrobot_Linedrive_run_list.Count > 0)
                                        listBox2.Items.Clear();

                                    for (int k = 0; k < crashrobot_Linedrive_run_list.Count; k++)
                                    {
                                        string strtemp = string.Format("run={0},pause={1}", crashrobot_Linedrive_run_list[k], crashrobot_Linedrive_pause_list[k]);
                                        listBox2.Items.Add(strtemp);
                                    }
                                    listBox2.SelectedIndex = listBox2.Items.Count - 1;
                                }));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("onCrashCheck err=" + ex.Message.ToString());
            }
        }

        private void onTempomove(string strRunrobot, string strPauserobot, float fdist, string strAngular, string strdirection)
        {
            try
            {
                float direction = 0;
                float distance = 0;

                direction = mainform.DegreeToRadian(strAngular);

                if (strdirection == "right")
                    direction *= -1;
                else if (strdirection == "left")
                    direction *= 1;
                distance = fdist;

                mainform.commBridge.onTempoMove_publish(strPauserobot, direction, distance);

                Console.WriteLine("Tempomove robot={0}, dist={1}, angluar={2}, direction={3}", strPauserobot, fdist, strAngular, strdirection);


                bTmpMove = true;
                bTmpMove_pause_robotid = strPauserobot;
                bTmpMove_run_robotid = strRunrobot;
            }
            catch (Exception ex)
            {
                Console.WriteLine("onTempomove err=" + ex.Message.ToString());
            }
        }

        public void MoveBase_StatusComplete(string strrobotid)
        {
            try
            {
                if (bTmpMove && strrobotid == bTmpMove_pause_robotid)  //tempomove 이동이 완료되었을때... 
                {
                    int nlistcnt = Data.Instance.Robot_work_info[strrobotid].robot_status_info.goalrunnigstatus.msg.status_list.Count;
                    if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.goalrunnigstatus.msg.status_list[nlistcnt - 1].status == 3)
                    {
                        onTaskResum(bTmpMove_run_robotid);
                        bTmpMove_run_robotid = "";
                        bTmpMove_pause_robotid = "";
                        bTmpMove = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MoveBase_StatusComplete err=" + ex.Message.ToString());
            }

        }

        #endregion

        #region Crash Check(S Driving) 

        List<string> CrashCheckRobot_Sdrive_list = new List<string>();
        Thread crashchk_Sdrive_thread;
        List<string> crashrobot_Sdrive_run_list = new List<string>();
        List<string> crashrobot_Sdrive_pause_list = new List<string>();

        bool bS2waitMove = false;
        string bSMove_pause_robotid = "";
        string bSMove_run_robotid = "";

        private void onCrashCheck_Sdrive_Thread()
        {
            for (; ; )
            {
                if (Data.Instance.bCrashcheckStop) break;

                if (Data.Instance.bCrashcheckPause) { }
                else
                    onCrashCheckk_Sdrive();

                Thread.Sleep(50);
            }
        }

        private void onCrashCheckk_Sdrive()
        {
            try
            {

                int ncheckcnt = CrashCheckRobot_Sdrive_list.Count;
                if (ncheckcnt > 0)
                {
                    string sourceRobot = "";
                    string targetRobot = "";
                    for (int i = 0; i < ncheckcnt; i++)
                    {
                        sourceRobot = CrashCheckRobot_Sdrive_list[i];

                        for (int j = 0; j < ncheckcnt; j++)
                        {
                            targetRobot = CrashCheckRobot_Sdrive_list[j];

                            if (sourceRobot != targetRobot)
                            {
                                string strmsg2 = string.Format("{0}_{1}", sourceRobot, targetRobot);

                                if (Data.Instance.Robot_work_info[sourceRobot].robot_status_info.robotstate == null)
                                {

                                    break;
                                }
                                if (Data.Instance.Robot_work_info[sourceRobot].robot_status_info.robotstate.msg == null)
                                {
                                    break;
                                }

                                if (Data.Instance.Robot_work_info[sourceRobot].robot_status_info.lookahead == null)
                                {
                                    break;
                                }
                                if (Data.Instance.Robot_work_info[sourceRobot].robot_status_info.lookahead.msg == null)
                                {
                                    break;
                                }

                                if (Data.Instance.Robot_work_info[targetRobot].robot_status_info.robotstate == null)
                                {

                                    continue;
                                }
                                if (Data.Instance.Robot_work_info[targetRobot].robot_status_info.robotstate.msg == null)
                                {

                                    continue;
                                }

                                if (Data.Instance.Robot_work_info[targetRobot].robot_status_info.lookahead == null)
                                {

                                    continue;
                                }
                                if (Data.Instance.Robot_work_info[targetRobot].robot_status_info.lookahead.msg == null)
                                {

                                    continue;
                                }

                                double source_x = 0;
                                double source_y = 0;
                                double source_lookahead_x = 0;
                                double source_lookahead_y = 0;

                                int nsource_priority = 0;

                                double target_x = 0;
                                double target_y = 0;
                                double target_lookahead_x = 0;
                                double target_lookahead_y = 0;
                                int ntargetpriority = 0;


                                source_x = Data.Instance.Robot_work_info[sourceRobot].robot_status_info.robotstate.msg.pose.x;
                                source_y = Data.Instance.Robot_work_info[sourceRobot].robot_status_info.robotstate.msg.pose.y;
                                source_lookahead_x = Data.Instance.Robot_work_info[sourceRobot].robot_status_info.lookahead.msg.point.x;
                                source_lookahead_y = Data.Instance.Robot_work_info[sourceRobot].robot_status_info.lookahead.msg.point.y;
                                nsource_priority = Data.Instance.Robot_work_info[sourceRobot].nPriorityLevel;

                                target_x = Data.Instance.Robot_work_info[targetRobot].robot_status_info.robotstate.msg.pose.x;
                                target_y = Data.Instance.Robot_work_info[targetRobot].robot_status_info.robotstate.msg.pose.y;
                                target_lookahead_x = Data.Instance.Robot_work_info[targetRobot].robot_status_info.lookahead.msg.point.x;
                                target_lookahead_y = Data.Instance.Robot_work_info[targetRobot].robot_status_info.lookahead.msg.point.y;
                                ntargetpriority = Data.Instance.Robot_work_info[targetRobot].nPriorityLevel;


                                double dist_LA = mainform.onPointToPointDist(source_lookahead_x, source_lookahead_y, target_lookahead_x, target_lookahead_y);
                                double dist_SourceCenToTargetLA = mainform.onPointToPointDist(source_x, source_y, target_lookahead_x, target_lookahead_y);
                                double dist_TargetCenToSourceLA = mainform.onPointToPointDist(target_x, target_y, source_lookahead_x, source_lookahead_y);


                                string strmsg = string.Format("{0}_{1} LA ={2:f2}, SourceCenToTargetLA={3:f2}, TargetCenToSourceLA={4:f2}", sourceRobot, targetRobot, dist_LA, dist_SourceCenToTargetLA, dist_TargetCenToSourceLA);
                                strmsg2 = string.Format("{0}_{1}", sourceRobot, targetRobot);
                                Invoke(new MethodInvoker(delegate ()
                                {
                                    listBox4.Items.Add(strmsg2);
                                    listBox4.Items.Add(strmsg);
                                    listBox4.SelectedIndex = listBox4.Items.Count - 1;
                                }));
                                if (dist_LA < 3 && dist_LA > 0) //crash warnning
                                {


                                    if (nsource_priority != ntargetpriority) //priority check
                                    {
                                        if (nsource_priority > ntargetpriority) //source move, target pause
                                        {
                                            if (!Data.Instance.Robot_work_info[targetRobot].robot_status_info.taskfeedback.msg.feedback.is_paused && !bS2waitMove)
                                            {
                                                onTaskPause(sourceRobot);
                                                onTaskPause(targetRobot);
                                                //Thread.Sleep(1000);

                                                crashrobot_Sdrive_run_list.Add(sourceRobot);
                                                crashrobot_Sdrive_pause_list.Add(targetRobot);

                                                //s2wait 장소로 이동

                                                string missionid = "MID20190811172036"; //MID20190811172036 S자2라인wait
                                                robot_demoinfo[targetRobot].strdemomode = "S2_linewait";
                                                mainform.commBridge.onMissionChange_publish(targetRobot, missionid);
                                                robot_demoinfo[targetRobot].currmissionid = missionid;
                                                Thread.Sleep(100);
                                                Console.WriteLine("mission change = {0},,robot={1}", missionid, targetRobot);
                                                bS2waitMove = true;


                                                Invoke(new MethodInvoker(delegate ()
                                                {
                                                    listBox4.Items.Add(string.Format("{0:f2}.. pause", targetRobot));
                                                    listBox4.SelectedIndex = listBox4.Items.Count - 1;
                                                }));
                                            }
                                        }
                                        else
                                        {
                                            if (!Data.Instance.Robot_work_info[sourceRobot].robot_status_info.taskfeedback.msg.feedback.is_paused && !bS2waitMove)
                                            {
                                                onTaskPause(sourceRobot);
                                                onTaskPause(targetRobot);
                                                //Thread.Sleep(1000);

                                                crashrobot_Sdrive_run_list.Add(targetRobot);
                                                crashrobot_Sdrive_pause_list.Add(sourceRobot);

                                                //s2wait 장소로 이동
                                                string missionid = "MID20190811172036"; //MID20190811172036 S자2라인wait
                                                robot_demoinfo[sourceRobot].strdemomode = "S2_linewait";
                                                mainform.commBridge.onMissionChange_publish(sourceRobot, missionid);
                                                robot_demoinfo[sourceRobot].currmissionid = missionid;
                                                //Thread.Sleep(100);
                                                Console.WriteLine("mission change = {0},,robot={1}", missionid, sourceRobot);
                                                bS2waitMove = true;


                                                Invoke(new MethodInvoker(delegate ()
                                                {
                                                    listBox4.Items.Add(string.Format("{0:f2}.. pause", sourceRobot));
                                                    listBox4.SelectedIndex = listBox4.Items.Count - 1;
                                                }));
                                            }

                                        }
                                    }
                                }
                                else
                                {

                                }

                                Invoke(new MethodInvoker(delegate ()
                                {
                                    if (crashrobot_Sdrive_run_list.Count > 0)
                                        listBox2.Items.Clear();

                                    for (int k = 0; k < crashrobot_Sdrive_run_list.Count; k++)
                                    {
                                        string strtemp = string.Format("run={0},pause={1}", crashrobot_Sdrive_run_list[k], crashrobot_Sdrive_pause_list[k]);
                                        listBox2.Items.Add(strtemp);
                                    }
                                    listBox2.SelectedIndex = listBox2.Items.Count - 1;
                                }));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("onCrashCheck err=" + ex.Message.ToString());
            }
        }


        #endregion

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            onRetryMissionchg("R_000", textBox1.Text.ToString());
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            onRetryMissionchg("R_001", textBox2.Text.ToString());
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            onRetryMissionchg("R_002", textBox3.Text.ToString());
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            onRetryMissionchg("R_003", textBox4.Text.ToString());
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            onRetryMissionchg("R_004", textBox5.Text.ToString());
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            onRetryMissionchg("R_005", textBox6.Text.ToString());
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            onRetryMissionchg("R_006", textBox7.Text.ToString());
        }


    

        #endregion



        private void btnDemoStop_Click(object sender, EventArgs e)
        {
            try
            {
                bDemorun_ver2 = false;
                Data.Instance.nWaitPos_Robot_idx = 0;
                Data.Instance.bWaitPos_Run = false;
                bLinedrive1_waitok = false;
                bLinedrive2_waitok = false;

                bSdrive1_waitok = false;
                bSdrive2_waitok = false;

                bstarttrack_timerflag = false;

                strWaitPosMove_Robot.Clear();
                strWaitPosMove_Robot_one.Clear();

                Data.Instance.bMissionCompleteCheck = false;
                //ros 연결후 
                if (Data.Instance.isConnected)
                {
                    int nrow = dataGridView_reg.SelectedCells[0].RowIndex;

                    if (nrow < 0 || nrow > dataGridView_reg.RowCount - 2) return;

                    string taskid = dataGridView_reg["taskid", nrow].Value.ToString();

                    string missionlist = dataGridView_reg["missionlist", nrow].Value.ToString();
                    string robotlist = dataGridView_reg["robotlist", nrow].Value.ToString();

                    string[] missionbuf = missionlist.Split(',');
                    string[] robotbuf = robotlist.Split(',');

                    onTaskCancel(robotbuf);

                    if (crashchk_thread != null)
                    {
                        Data.Instance.bCrashcheckStop = true;
                        crashchk_thread.Abort();
                        crashchk_thread = null;
                    }

                    CrashCheckRobot_list.Clear();
                    CrashCheckRobot_Linedrive_list.Clear();

                    Data.Instance.bTaskRun = false;
                    {
                        //db 정보 갱신
                        mainform.dbBridge.onDBUpdate_Tasklist_status(taskid, "wait");

                        //task operation db 삭제 
                        for (int i2 = 0; i2 < robotbuf.Length; i2++)
                        {
                            mainform.dbBridge.onDBDelete_TaskOperation(taskid, robotbuf[i2]);
                        }

                        //쓰레드 삭제
                        int threadcnt = Data.Instance.TaskCheck_threadList.Count;
                        if (threadcnt > 0)
                        {
                            int thridx = 0;
                            TaskCheck_class taskclass = Data.Instance.TaskCheck_threadList.ElementAt(thridx);

                            if (taskclass.strTaskid == taskid)
                            {
                                if (taskclass.taskthred != null)
                                {
                                    taskclass.taskthred.Abort();
                                    taskclass = null;
                                }

                                Data.Instance.TaskCheck_threadList.RemoveAt(thridx);
                            }
                        }

                        onInitSet();
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("btnTaskStop_Click err=" + ex.Message.ToString());
            }
        }

        private void btnTaskPause_Click(object sender, EventArgs e)
        {
            onTaskPauseAll();
        }

        private void btnTaskResume_Click(object sender, EventArgs e)
        {
            onTaskResumeAll();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            onDemoRun2();
        }
        private void onDemoRun2()
        {
            try
            {
                Data.Instance.bCrashcheckPause = false;
                bLinedrive1_waitok = false;
                bLinedrive2_waitok = false;

                nlinedriveCnt = 0;

                nbasicmoveCnt = 0;
                nliftCnt = 0;

                bTmpMove = false;
                bTmpMove_pause_robotid = "";
                bTmpMove_run_robotid = "";


                strCurrStartRobot = "";
                if (Data.Instance.isConnected)
                {
                    int nrow = dataGridView_reg.SelectedCells[0].RowIndex;

                    if (nrow < 0 || nrow > dataGridView_reg.RowCount - 2) return;

                    string taskid = dataGridView_reg["taskid", nrow].Value.ToString();
                    string taskname = dataGridView_reg["taskname", nrow].Value.ToString();
                    string missionlist = dataGridView_reg["missionlist", nrow].Value.ToString();
                    string robotlist = dataGridView_reg["robotlist", nrow].Value.ToString();

                    string[] missionbuf = missionlist.Split(',');
                    string[] robotbuf = robotlist.Split(',');

                    int ntaskcnt = int.Parse(txtTaskCnt.Text.ToString());
                    string strMsg = "";


                    currTaskRobotid = new string[robotbuf.Length];
                    ncurrTaskRobotid_idx = 0;
                    strStartMissionid = "";
                    StartRobot_Skip.Clear();
                    CrashCheckRobot_list.Clear();
                    CrashCheckRobot_Linedrive_list.Clear();
                    Data.Instance.bMissionCompleteCheck = false;


                    for (int i = 0; i < robotbuf.Length; i++)
                    {
                        string strrobotcnt = string.Format("{0}", ntaskcnt);

                        strMsg += string.Format("{0},반복({1}) ", robotbuf[i], strrobotcnt);

                        currTaskRobotid[i] = robotbuf[i];
                    }

                    strMsg += " 동작 하시겠습니까?";

                    if (DialogResult.OK == MessageBox.Show(strMsg, "확인", MessageBoxButtons.OKCancel))
                    {
                        int ntaskcnt1 = int.Parse(txtTaskCnt.Text.ToString());

                        for (int i = 0; i < robotbuf.Length; i++)
                        {
                            if (Data.Instance.Robot_work_info[robotbuf[i]].robot_status_info.taskfeedback != null)
                            {
                                if (Data.Instance.Robot_work_info[robotbuf[i]].robot_status_info.taskfeedback.msg != null)
                                {
                                    Data.Instance.Robot_work_info[robotbuf[i]].robot_status_info.taskfeedback.msg.feedback.action_indx = 0;
                                    Data.Instance.Robot_work_info[robotbuf[i]].robot_status_info.taskfeedback.msg.feedback.mission_indx = 0;
                                }
                            }
                        }

                        listBox1.Items.Clear();

                        //RIDiS에 task order 전달 
                        onTaskOrder(taskid, missionbuf, taskname, missionlist, robotlist, ntaskcnt1);
                        Thread.Sleep(1000);

                        Data.Instance.bTaskRun = true;
                        crashrobot_run_list.Clear();
                        crashrobot_pause_list.Clear();

                        DateTime dt = DateTime.Now;
                        Data.Instance.strTaskRun_StartTime = string.Format("{0:d4}{1:d2}{2:d2}{3:d2}{4:d2}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute);

                        mainform.dbBridge.onDBRead_Missionlist();
                        {
                            //db task  table 정보 갱신
                            mainform.dbBridge.onDBUpdate_Tasklist_status(taskid, "run");
                            onInitSet();

                            //db task operation table 정보 갱신 => Task 쓰레드에서 실시간 갱신 
                            for (int i2 = 0; i2 < robotbuf.Length; i2++)
                            {
                                mainform.dbBridge.onDBSave_TaskOperation(taskid, robotbuf[i2], missionlist, "", 0, "insert");
                            }

                            // onTaskPauseAll(); //ros에서 처리 

                            if (crashchk_thread != null)
                            {
                                Data.Instance.bCrashcheckStop = true;
                                crashchk_thread.Abort();
                                crashchk_thread = null;
                            }

                            Data.Instance.bMissionCompleteCheck = true;

                            for (int nrobotidx = 0; nrobotidx < currTaskRobotid.Length; nrobotidx++)
                            {
                                CrashCheckRobot_list.Add(currTaskRobotid[nrobotidx]);
                            }

                            ////미션1에 가장 가까운 로봇부터 resume 시킨다. 
                            //    strStartMissionid = missionbuf[0];
                            //    string strresumrobot = onStartPos_DistMin_RobotCheck(missionbuf[0]);
                            //    StartRobot_Skip.Add(strresumrobot);
                            //    onTaskResum(strresumrobot);
                            //    strCurrStartRobot = strresumrobot;

                            //    Console.WriteLine("startrobot={0}", strCurrStartRobot);

                            //     CrashCheckRobot_list.Add(strresumrobot);

                            

                            //task 쓰레드가 존재하는지 파악후 구동
                            int threadcnt = Data.Instance.TaskCheck_threadList.Count;
                            if (threadcnt > 0)
                            {
                                int thridx = 0;
                                for (int i = 0; i < threadcnt; i++)
                                {
                                    TaskCheck_class taskclass = Data.Instance.TaskCheck_threadList.ElementAt(i);
                                    if (taskclass.strTaskid == taskid)
                                    {
                                        if (taskclass.taskthred != null)
                                        {
                                            taskclass.taskthred.Abort();
                                            taskclass.taskthred = null;
                                        }

                                        Data.Instance.TaskCheck_threadList.RemoveAt(i);
                                    }
                                }

                            }

                            {
                                TaskCheck_class taskclass = new TaskCheck_class();
                                taskclass.strTaskid = taskid;
                                taskclass.taskfinish_Evt += new TaskCheck_class.TaskFinished(mainform.TaskFinished);

                                taskclass.taskthred = new Thread(taskclass.taskCheck_thread_func);
                                Task_checkThread_TableInfo task_checkinfo = new Task_checkThread_TableInfo();
                                task_checkinfo.taskcheck_info = new List<Task_checkThread_Info>();

                                for (int i2 = 0; i2 < robotbuf.Length; i2++)
                                {
                                    Task_checkThread_Info task_info = new Task_checkThread_Info();
                                    task_info.strrobotid = robotbuf[i2];

                                    task_checkinfo.taskcheck_info.Add(task_info);
                                }

                                Data.Instance.TaskCheck_threadList.Add(taskclass);
                                taskclass.taskthred.Start(task_checkinfo);
                            }

                        }

                        //  timer_StartTrack.Interval = 100;
                        //  timer_StartTrack.Enabled = true;

                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("btnDemoRun_Click err=" + ex.Message.ToString());
            }
        }

     

       

       


        private void chkLineDrive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLineDrive.Checked)
            {
                timer_LinedrivewaitokChk.Interval = 500;
                timer_LinedrivewaitokChk.Enabled = true;
            }
            else
            {
                timer_LinedrivewaitokChk.Enabled = false;
            }
        }


       

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) bsequenceDemo = true;
            else bsequenceDemo = false;

        }

        private string onStartPos_DistMin_RobotCheck(string strmissionid)
        {
            string dist_min_robotid = "";
            try
            {
                double dist_min = 0;
                
                int nmissioncnt = Data.Instance.missionlisttable.missioninfo.Count;
                if (nmissioncnt > 0)
                {
                    for (int nmcnt = 0; nmcnt < nmissioncnt; nmcnt++)
                    {
                        if (strmissionid == Data.Instance.missionlisttable.missioninfo[nmcnt].strMisssionID)
                        {
                            string strwork = Data.Instance.missionlisttable.missioninfo[nmcnt].work;
                            WorkFlowGoal db_missiondata = new WorkFlowGoal();
                            db_missiondata = JsonConvert.DeserializeObject<WorkFlowGoal>(strwork);

                            float x = db_missiondata.work[0].action_args[0];
                            float y = db_missiondata.work[0].action_args[1];
                            float th = db_missiondata.work[0].action_args[2];
                            bool bskip = false;
                            for (int nrobotidx = 0; nrobotidx < currTaskRobotid.Length; nrobotidx++)
                            {
                                bskip = false;
                                if (StartRobot_Skip.Count>0)
                                {
                                    for(int nskip=0; nskip < StartRobot_Skip.Count; nskip++)
                                    {
                                        if(currTaskRobotid[nrobotidx]==StartRobot_Skip[nskip])
                                        {
                                            bskip = true;
                                           // nrobotidx++;
                                        }
                                    }
                                }
                                if (bskip) continue;

                                if (Data.Instance.Robot_work_info[currTaskRobotid[nrobotidx]].robot_status_info.robotstate != null)
                                {
                                    if (Data.Instance.Robot_work_info[currTaskRobotid[nrobotidx]].robot_status_info.robotstate.msg != null)
                                    {
                                        float robotx = (float)Data.Instance.Robot_work_info[currTaskRobotid[nrobotidx]].robot_status_info.robotstate.msg.pose.x;
                                        float roboty = (float)Data.Instance.Robot_work_info[currTaskRobotid[nrobotidx]].robot_status_info.robotstate.msg.pose.y;
                                        float robottheta = (float)Data.Instance.Robot_work_info[currTaskRobotid[nrobotidx]].robot_status_info.robotstate.msg.pose.theta;

                                        double disttmp = mainform.onPointToPointDist(x, y, robotx,roboty);

                                        if (dist_min > disttmp)
                                        {
                                            dist_min = disttmp;
                                            dist_min_robotid = currTaskRobotid[nrobotidx];
                                        }
                                    }
                                }
                            }
                            

                            break;
                        }
                    }
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("onStartPos_DistMin_RobotCheck err=" + ex.Message.ToString());
            }
            return dist_min_robotid;
        }



        private void onTaskPauseAll()
        {
            try
            {
                //ros 연결후 
                if (Data.Instance.isConnected)
                {
                    int nrow = dataGridView_reg.SelectedCells[0].RowIndex;

                    if (nrow < 0 || nrow > dataGridView_reg.RowCount - 2) return;

                    string taskid = dataGridView_reg["taskid", nrow].Value.ToString();

                    string missionlist = dataGridView_reg["missionlist", nrow].Value.ToString();
                    string robotlist = dataGridView_reg["robotlist", nrow].Value.ToString();

                    string[] missionbuf = missionlist.Split(',');
                    string[] robotbuf = robotlist.Split(',');
                    int nrobottotalcnt = robotbuf.Length;
                    for (int nrobotcnt = 0; nrobotcnt < nrobottotalcnt; nrobotcnt++)
                    {

                        string strRobot = robotbuf[nrobotcnt];

                        onTaskPause(strRobot);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("onTaskPauseAll err=" + ex.Message.ToString());
            }
        }

        public async void onTaskPause(string robotid)
        {
            try
            {
                string strRobot = robotid;
                //var task = Task.Run(() => mainform.commBridge.onTaskPause_publish(strRobot, ""));
                //await task;
                mainform.commBridge.onTaskPause_publish(strRobot, "");
               // Thread.Sleep(200);
               
                Console.WriteLine("pause1 = {0}",strRobot);
                onConsolemsgDp(string.Format("pause1 = {0}", strRobot));

                mainform.commBridge.onTaskPause_publish(strRobot, "");
              //  Thread.Sleep(200);

                Console.WriteLine("pause2 = {0}", strRobot);
                onConsolemsgDp(string.Format("pause2 = {0}", strRobot));
            }
            catch (Exception ex)
            {
                Console.WriteLine("onTaskCancel err=" + ex.Message.ToString());
            }
        }

        private void onTaskResumeAll()
        {
            try
            {
                //ros 연결후 
                if (Data.Instance.isConnected)
                {
                    int nrow = dataGridView_reg.SelectedCells[0].RowIndex;

                    if (nrow < 0 || nrow > dataGridView_reg.RowCount - 2) return;

                    string taskid = dataGridView_reg["taskid", nrow].Value.ToString();

                    string missionlist = dataGridView_reg["missionlist", nrow].Value.ToString();
                    string robotlist = dataGridView_reg["robotlist", nrow].Value.ToString();

                    string[] missionbuf = missionlist.Split(',');
                    string[] robotbuf = robotlist.Split(',');
                    int nrobottotalcnt = robotbuf.Length;
                    for (int nrobotcnt = 0; nrobotcnt < nrobottotalcnt; nrobotcnt++)
                    {

                        string strRobot = robotbuf[nrobotcnt];

                        onTaskResum(strRobot);
                    }
                    /*
                    int nrobottotalcnt = currTaskRobotid.Length;

                    for (int nrobotcnt = 0; nrobotcnt < nrobottotalcnt; nrobotcnt++)
                    {
                  
                        string strRobot = currTaskRobotid[nrobotcnt];

                        onTaskResum(strRobot);
                    }*/
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("onTaskResumeAll err=" + ex.Message.ToString());
            }
        }

        public async void onTaskResum(string  robotid)
        {
            try
            {
                 string strRobot = robotid;
                //var task = Task.Run(() => mainform.commBridge.onTaskResume_publish(strRobot, ""));
                //await task;
                mainform.commBridge.onTaskResume_publish(strRobot, "");
               // Thread.Sleep(100);

                Console.WriteLine("resume = {0}", strRobot);

                onConsolemsgDp(string.Format("resume = {0}", strRobot));
            }
            catch (Exception ex)
            {
                Console.WriteLine("onTaskResum err=" + ex.Message.ToString());
            }
        }


        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtLiftCnt_TextChanged(object sender, EventArgs e)
        {

        }

        private double onPointToPointDist(double x1, double y1, double x2, double y2)
        {
            double hypo = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            return hypo;
        }

        public void onListmsg(string msg)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                listBox1.Items.Add(msg);
                int n = listBox1.Items.Count;
                listBox1.SelectedIndex = n - 1;

                if (n > 100) listBox1.Items.Clear();
            }));
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

                    Action[] act = new Action[1];
                    act[0] = new Action();

                    act[0].action_type = 3;
                    act[0].action_args.Add(0);
                    string strmsg = "";
                    if (strlift == "Bottom")
                        act[0].action_args.Add(-1);
                    else if (strlift == "Top")
                        act[0].action_args.Add(1);



                    mainform.commBridge.onWorkGoal_publish("lift", "lift", strrobotid, 0, 1, act);
                   

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("btnAssembly_Click err :={0}", ex.Message.ToString());
            }
        }

        private void timer_runnigTime_Tick(object sender, EventArgs e)
        {

        }

        #region 주행시 성능평가 분석 관련

        private void chkRobotPosRec_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRobotPosRec.Checked)
                Data.Instance.bRobotPosRec = true;
            else
                Data.Instance.bRobotPosRec = false;
        }

        #endregion

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        #region Test btn
        private void button5_Click(object sender, EventArgs e) //crashcheck thread start
        {
            onCrash_Start();

           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string strresumrobot = "R_002";
            StartRobot_Skip.Add(strresumrobot);
            onTaskResum(strresumrobot);
            strCurrStartRobot = strresumrobot;

            //Console.WriteLine("startrobot={0}", strCurrStartRobot);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string strresumrobot = "R_001";
            StartRobot_Skip.Add(strresumrobot);
            onTaskResum(strresumrobot);
            strCurrStartRobot = strresumrobot;

           // Console.WriteLine("startrobot={0}", strCurrStartRobot);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string strresumrobot = "R_005";
            StartRobot_Skip.Add(strresumrobot);
            onTaskResum(strresumrobot);
            strCurrStartRobot = strresumrobot;

            //Console.WriteLine("startrobot={0}", strCurrStartRobot);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string strresumrobot = "R_003";
            StartRobot_Skip.Add(strresumrobot);
            onTaskResum(strresumrobot);
            strCurrStartRobot = strresumrobot;

            //Console.WriteLine("startrobot={0}", strCurrStartRobot);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string strresumrobot = "R_002";
            onTaskPause(strresumrobot);

           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string strresumrobot = "R_001";
            onTaskPause(strresumrobot);

            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string strresumrobot = "R_005";
            onTaskPause(strresumrobot);
            

            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string strresumrobot = "R_003";
            onTaskPause(strresumrobot);
        
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string strresumrobot = "R_006";
            StartRobot_Skip.Add(strresumrobot);
            onTaskResum(strresumrobot);
            strCurrStartRobot = strresumrobot;

        }

        private void button11_Click(object sender, EventArgs e)
        {
            string strresumrobot = "R_006";
            onTaskPause(strresumrobot);

         
        }






        #endregion

        bool bDemoRun = false;
        private void chkDemo_CheckedChanged(object sender, EventArgs e)
        {
            if(chkDemo.Checked)
            {
                bDemoRun = true;
            }
            else
            {
                bDemoRun = false;
            }
        }

        private void chkLiftRun_CheckedChanged(object sender, EventArgs e)
        {

            if (chkLiftRun.Checked) bLiftChk = true;
            else bLiftChk = false;
        }

        private void btnliftup_Click_1(object sender, EventArgs e)
        {

        }

        private void btnTempomove_Click(object sender, EventArgs e)
        {
            onTempomove("R_001","R_006",(float)1.5,"45", "left");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string strresumrobot = "R_004";
            StartRobot_Skip.Add(strresumrobot);
            onTaskResum(strresumrobot);
            strCurrStartRobot = strresumrobot;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string strresumrobot = "R_004";
            onTaskPause(strresumrobot);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string strresumrobot = "R_000";
            StartRobot_Skip.Add(strresumrobot);
            onTaskResum(strresumrobot);
            strCurrStartRobot = strresumrobot;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string strresumrobot = "R_000";
            onTaskPause(strresumrobot);
        }

        private void btnListclear_Click(object sender, EventArgs e)
        {
            listBox6.Items.Clear();
        }
    }

    public class WaitPos_List
    {
        public float wait_x;
        public float wait_y;
        public float wait_theta;
    }

    public class Robot_Demo_Info
    {
        public string strrobotid;
        public string strdemomode;
        public int workcnt;
        public int currcnt;

        public string currmissionid;

        public int lineonlycnt;
        public int curr_lineonlycnt;

        public int Sonlycnt;
        public int curr_Sonlycnt;

        public int liftlinecnt;
        public int curr_liftlinecnt;

        public int line2cnt;
        public int curr_line2cnt;

        public int liftonlycnt;
        public int curr_liftonlycnt;

        public int s1cnt;
        public int curr_s1cnt;

        public int s2cnt;
        public int curr_s2cnt;

        public int urcnt;
        public int curr_urcnt;

        public bool actcomplete;
    }
}
