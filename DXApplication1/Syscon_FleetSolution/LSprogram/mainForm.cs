using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
// ADD
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Syscon_Solution.LSprogram
{
    public partial class mainForm : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {


        int x, y, mov;
        public FleetManager.DB.DB_bridge dbBridge;// = new FleetManager.DB.DB_bridge();
        public FleetManager.Comm.Comm_bridge commBridge;// = new FleetManager.Comm.Comm_bridge();

        public bool bRun_001 = true, bRun_002 = true, bRun_003 = true, bRun_004 = true, bRun_005 = true, bRun_006 = true, bRun_007 = true;

        Screen[] screens = Screen.AllScreens;
        displayMap displaymap;
        public taskoperationForm taskoperationform;
        firstForm firstform;

        Thread crashchk_thread;

        List<string> CrashCheckRobot_list = new List<string>();
        List<string> crashrobot_run_list = new List<string>();
        List<string> crashrobot_pause_list = new List<string>();


        const int MAX_NODE = 15;
        const int INF = 100000;
        private void onCrashCheck()
        {
            try
            {

                int ncheckcnt = CrashCheckRobot_list.Count;
                if (ncheckcnt > 0)
                {
                    Invoke(new MethodInvoker(delegate ()
                    {
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
                                    }));
                                    break;
                                }
                                if (Data.Instance.Robot_work_info[sourceRobot].robot_status_info.robotstate.msg == null)
                                {
                                    string strmsg1 = string.Format("source={0}, robotstate msg fail", sourceRobot);

                                    Invoke(new MethodInvoker(delegate ()
                                    {
                                    }));
                                    break;
                                }

                                if (Data.Instance.Robot_work_info[sourceRobot].robot_status_info.lookahead == null)
                                {
                                    string strmsg1 = string.Format("source={0}, lookahead fail", sourceRobot);

                                    Invoke(new MethodInvoker(delegate ()
                                    {
                                    }));
                                    break;
                                }
                                if (Data.Instance.Robot_work_info[sourceRobot].robot_status_info.lookahead.msg == null)
                                {
                                    string strmsg1 = string.Format("source={0}, lookahead  msg fail", sourceRobot);

                                    Invoke(new MethodInvoker(delegate ()
                                    {
                                    }));
                                    break;
                                }

                                if (Data.Instance.Robot_work_info[targetRobot].robot_status_info.robotstate == null)
                                {
                                    string strmsg1 = string.Format("targetRobot={0}, robotstate fail", sourceRobot);

                                    Invoke(new MethodInvoker(delegate ()
                                    {
                                    }));
                                    continue;
                                }
                                if (Data.Instance.Robot_work_info[targetRobot].robot_status_info.robotstate.msg == null)
                                {
                                    string strmsg1 = string.Format("targetRobot={0}, robotstate  msg fail", sourceRobot);

                                    Invoke(new MethodInvoker(delegate ()
                                    {
                                    }));
                                    continue;
                                }

                                if (Data.Instance.Robot_work_info[targetRobot].robot_status_info.lookahead == null)
                                {
                                    string strmsg1 = string.Format("targetRobot={0}, lookahead   fail", sourceRobot);

                                    Invoke(new MethodInvoker(delegate ()
                                    {
                                        ;
                                    }));
                                    continue;
                                }
                                if (Data.Instance.Robot_work_info[targetRobot].robot_status_info.lookahead.msg == null)
                                {
                                    string strmsg1 = string.Format("targetRobot={0}, lookahead  msg  fail", sourceRobot);

                                    Invoke(new MethodInvoker(delegate ()
                                    {
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


                                double dist_LA = onPointToPointDist(source_lookahead_x, source_lookahead_y, target_lookahead_x, target_lookahead_y);
                                double dist_SourceCenToTargetLA = onPointToPointDist(source_x, source_y, target_lookahead_x, target_lookahead_y);
                                double dist_TargetCenToSourceLA = onPointToPointDist(target_x, target_y, source_lookahead_x, source_lookahead_y);
                                //double dist_LA = onPointToPointDist(source_x, source_y, target_x, target_y);



                                string strmsg = string.Format("{0}_{1} LA ={2:f2}, SourceCenToTargetLA={3:f2}, TargetCenToSourceLA={4:f2}", sourceRobot, targetRobot, dist_LA, dist_SourceCenToTargetLA, dist_TargetCenToSourceLA);
                                strmsg2 = string.Format("{0}_{1}", sourceRobot, targetRobot);
                                Invoke(new MethodInvoker(delegate ()
                                {
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
                                                    taskoperationform.onTaskResum(targetRobot);
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
                taskoperationform.onTaskPause(strPauserobot);
                //Thread.Sleep(300);

                crashrobot_run_list.Add(strRunrobot);
                crashrobot_pause_list.Add(strPauserobot);

                Invoke(new MethodInvoker(delegate ()
                {

                }));

                //onConsolemsgDp(string.Format("crash pause = {0:f2}.. pause", strPauserobot));
                Console.WriteLine("crash pause = {0:f2}.. pause", strPauserobot);
            }
        }
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
        private void checkbRun_false(string strrobotid)
        {
            switch (strrobotid)
            {
                case "R_001":
                    bRun_001 = false;
                    return;
                case "R_002":
                    bRun_002 = false;
                    return;
                case "R_003":
                    bRun_003 = false;
                    return;
                case "R_004":
                    bRun_004 = false;
                    return;
                case "R_005":
                    bRun_005 = false;
                    return;
                case "R_006":
                    bRun_006 = false;
                    return;
                case "R_007":
                    bRun_007 = false;
                    return;
            }
        }
        public void checkbRun(string strrobotid)
        {
            switch (strrobotid)
            {
                case "R_001":
                    bRun_001 = true;
                    return;
                case "R_002":
                    bRun_002 = true;
                    return;
                case "R_003":
                    bRun_003 = true;
                    return;
                case "R_004":
                    bRun_004 = true;
                    return;
                case "R_005":
                    bRun_005 = true;
                    return;
                case "R_006":
                    bRun_006 = true;
                    return;
                case "R_007":
                    bRun_007 = true;
                    return;
            }
        }
        bool anotherCheck = false;
        string temp;
        // 태스크 진행중에 1초마다 날아오는 피드백
        public void TaskFeedback_Ing(string strrobotid)
        {
            int missionid = Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.mission_indx;
            int actidx = Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.action_indx;
            string task_id = Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.task_id;
            int location = 88, location_1 = 88;
            //Console.WriteLine("{0}의 현재 위치는 -> {1}", strrobotid, firstform.findLocation(strrobotid));


            // 태스크 진행중일때 
            if (strrobotid == "R_006" || strrobotid == "R_007")
            {
                location = firstform.findLocation(strrobotid);
                if (location == 12 || location == 6)
                {
                    if (CrashCheckRobot_list.Contains(strrobotid))
                    {
                        CrashCheckRobot_list.Remove(strrobotid);
                    }
                }
                else
                {
                    if (!CrashCheckRobot_list.Contains(strrobotid))
                    {
                        CrashCheckRobot_list.Add(strrobotid);
                    }
                }
                if (task_id == "RETURN")
                {
                    Console.WriteLine($"리턴중 {strrobotid}-----현재 로봇 위치 : {firstform.findLocation(strrobotid)}");
                    if (Session_2_mission != null)
                    {
                        if (Session_2_mission.Count > 0)
                        {
                            if (0 < firstform.findLocation(strrobotid) && firstform.findLocation(strrobotid) != 99)
                            {
                                //int currentLocation = firstform.findLocation(strrobotid);
                                //firstform.onTaskCancel(strrobotid);
                                //Thread.Sleep(3000);

                                //checkbRun(strrobotid);
                                //algorithm_(strrobotid);
                            }
                        }
                    }
                    if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.task_complete == true)
                    {
                        if (firstform.findLocation(strrobotid) == 12)
                        {
                            firstform.Robot_Parking(strrobotid);
                        }
                        Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.task_complete = false;

                        //수정한 부분
                        checkbRun(strrobotid);

                    }
                }
                //Console.WriteLine("{0} -> missionid : {1}", strrobotid, missionid);
            }
            else
            {
                location_1 = firstform.findLocation(strrobotid);
                if (location_1 == 12 || location == 6)
                {
                    if (CrashCheckRobot_list.Contains(strrobotid))
                    {
                        CrashCheckRobot_list.Remove(strrobotid);
                    }
                }
                else
                {
                    if (!CrashCheckRobot_list.Contains(strrobotid))
                    {
                        CrashCheckRobot_list.Add(strrobotid);
                    }
                }
            }
        }



        int[,] a = new int[MAX_NODE, MAX_NODE] {{ 0, 1, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, },
                                              {INF, 0, INF, INF, 1, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
    {INF,   1,  0,  INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF},
{1,   INF,    INF,    0,  INF,  INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF},
{INF,   INF,    INF,    1,    0,  INF,  INF,   1,    INF,    INF,    INF,    INF,    INF,    INF,    INF},
{INF,   INF,    1,    INF,    1,    0,  INF,    INF,   INF,  INF,    INF,    INF,    INF,    INF,    INF},
{INF,   INF,    INF,    1,  INF,    INF,    0,  INF,  INF,    INF,    INF,    INF,    INF,    INF,    INF},
{INF,   INF,    INF,    INF,    INF,  INF,    1,    0,  1,  INF,    1,    INF,    INF,    INF,    INF},
{INF,   INF,    INF,    INF,    INF,    1,    INF,    INF,    0,  INF,    INF,    INF,  INF,    INF,    INF},
{INF,   INF,    INF,    INF,    INF,    INF,    1,  INF,    INF,    0,  INF,  INF,    INF,    INF,    INF},
{INF,   INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    2,    0,  INF,  INF,    1,    INF},
{INF,   INF,    INF,    INF,    INF,    INF,    INF,    INF,    1,    INF,    1,    0,  INF,    INF,    INF},
{INF,   INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    1,  INF,    INF,    0,  INF,    INF},
{INF,   INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    1,  0,  1},
{INF,   INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    1,    INF,    INF,  0} };



        public void Robot_parking(string strrobotid)
        {

        }
        private void accordionControlElement17_Click(object sender, EventArgs e)
        {
            if (mainContainer.Controls.Count > 0)
            {
                mainContainer.Controls.RemoveAt(0);
            }

            mainContainer.Controls.Add(firstform);

        }
        static mainForm form;
        missioneditForm missionedit;
        private void mainForm_Load(object sender, EventArgs e)
        {
            //missionLOAD();
            missionedit = new missioneditForm(this);
            taskoperationform = new taskoperationForm(this);
            firstform = new firstForm(this);
            Readnodemission();
            crashchk_thread = new Thread(onCrashCheck_Thread);
            crashchk_thread.IsBackground = true;
            crashchk_thread.Start();
            node_search();
            path_search();
            mainContainer.Controls.Add(firstform);
            dbBridge.onDBDelete_Missionlist("TEMP_");
            form = this;
        }
        public void MoveBase_StatusComplete(string strrobotid)
        {
            try
            {
                // if (Data.Instance.nFormidx == (int)Data.FORM_IDX.Operaion_TaskForm)
                // {
                this.MoveBase_Complete(strrobotid);
                //  }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MoveBase_StatusComplete err=" + ex.Message.ToString());
            }
        }


        public void MoveBase_Complete(string strrobotid)
        {
            try
            {
                //if (bTmpMove && strrobotid == bTmpMove_pause_robotid)  //tempomove 이동이 완료되었을때... 
                //{
                //    int nlistcnt = Data.Instance.Robot_work_info[strrobotid].robot_status_info.goalrunnigstatus.msg.status_list.Count;
                //    if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.goalrunnigstatus.msg.status_list[nlistcnt - 1].status == 3)
                //    {
                //        onTaskResum(bTmpMove_run_robotid);
                //        bTmpMove_run_robotid = "";
                //        bTmpMove_pause_robotid = "";
                //        bTmpMove = false;
                //    }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine("MoveBase_StatusComplete err=" + ex.Message.ToString());
            }
        }
        private void accordionControlElement1_Click(object sender, EventArgs e)
        {

        }

        robotOverview robotoverview;
        private void accordionControlElement18_Click(object sender, EventArgs e)
        {

            robotoverview = null;
            robotoverview = new robotOverview(this);

            if (mainContainer.Controls.Count == 1)
            {
                mainContainer.Controls.RemoveAt(0);
            }
            mainContainer.Controls.Add(robotoverview);
            robotoverview.oninit();
            robotoverview.th++;
            robotoverview.threadstart();
        }

        robotMonitoring robotmonitoring;
        private void accordionControlElement19_Click(object sender, EventArgs e)
        {
            robotmonitoring = null;
            robotmonitoring = new robotMonitoring();
            Data.Instance.robot_id = "R_001";

            if (mainContainer.Controls.Count > 0)
            {
                mainContainer.Controls.RemoveAt(0);
            }
            mainContainer.Controls.Add(robotmonitoring);

        }
        private void abort_alarm()
        {

        }

        private void accordionControlElement3_Click(object sender, EventArgs e)
        {
            mapmonitoring monitor;
            monitor = null;
            monitor = new mapmonitoring(this);
            monitor.oninit();
            monitor.Show();
            //systemLog systemlog = new systemLog();
            //systemlog.Show();
        }

        private void accordionControlElement27_Click(object sender, EventArgs e)
        {
            try
            {

                if (mainContainer.Controls.Count > 0)
                {
                    mainContainer.Controls.RemoveAt(0);
                }
                mainContainer.Controls.Add(missionedit);

            }
            catch
            {

            }
        }
        taskEdit taskedit;
        private void accordionControlElement28_Click(object sender, EventArgs e)
        {

            taskedit = null;
            taskedit = new taskEdit();
            taskedit.Show();
        }

        Syscon_Solution.LSprogram.splashForm splashform_Ctl;



        Thread test;

        public mainForm()
        {
            oninit();
        }



        private void oninit()
        {
            InitializeComponent();
            commBridge = new FleetManager.Comm.Comm_bridge(this);
            dbBridge = new FleetManager.DB.DB_bridge(this);


            //Thread statecheck = new Thread(onRobotcheck_state);
            //statecheck.IsBackground = true;
            //statecheck.Start();
            Thread th = new Thread(onRobotCheck);
            th.IsBackground = true;
            th.Start();


        }
        etc.alarmForm alarmform = new etc.alarmForm();
        private void checkalarm()
        {
            while (true)
            {
                try
                {

                    foreach (KeyValuePair<string, Robot_RegInfo> info in Data.Instance.Robot_RegInfo_list)
                    {
                        string strrobotid = info.Key;
                        Robot_RegInfo value = info.Value;
                        if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate.msg != null)
                        {
                            if (Data.Instance.Robot_work_info[info.Key].robot_status_info.robotstate.msg.workstate == 5)
                            {
                                alarmform.Show();
                                alarmform.alarmOccur("ABORT");
                            }
                        }
                    }
                }
                catch
                {

                }
                Thread.Sleep(300);
            }
        }
        private void onSuscribe_RobotsStatus_Basic()
        {
            try
            {

                if (Data.Instance.isConnected)
                {
                    //Dictionary<string, string> robotinfo = Data.Instance.Robot_status_info;
                    //foreach (KeyValuePair<string, string> info in robotinfo)
                    int cnt = 0;
                    cnt = Data.Instance.Robot_RegInfo_list.Count;
                    if (cnt > 0)
                    {
                        foreach (KeyValuePair<string, Robot_RegInfo> info in Data.Instance.Robot_RegInfo_list)
                        {
                            string strrobotid = info.Key;
                            Robot_RegInfo value = info.Value;

                            commBridge.onSelectRobotStatus_Basic_subscribes(strrobotid);
                            Thread.Sleep(Data.Instance.nSubscribeDelayTime);

                            Console.WriteLine("{0} basic subscribes complete ", strrobotid);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSuscribe_RobotsStatus_Basic err :={0}", ex.Message.ToString());
            }
        }
        private void alternativeATCNo(string requiredID, string altAtc)
        {
            LS_Socket tmp = null;
            string atcNo = altAtc;
            if (!getATCState(atcNo))
            {
                foreach (string id in new List<string>(PLC_Socket_Info.Keys))
                {
                    string group = PLC_Socket_Info[requiredID].GROUP;
                    tmp = PLC_Socket_Info[id].getSocketFromATCNo(group, atcNo);

                    if (tmp != null)
                    {
                        tmp.checkCondition(requiredID, atcNo);
                        break;
                    }
                }
            }
        }


        #region PLC
        internal List<LS_Socket> ABN100_1;
        internal List<LS_Socket> ABN100_2;
        internal List<LS_Socket> ABN100_3;
        internal List<LS_Socket> ABN100_4;
        private Dictionary<string/*PLCID*/, LS_Socket> pLC_Socket_Info = new Dictionary<string/*PLCID*/, LS_Socket>();
        Dictionary<string/*PLCID*/, int[,]> PLCData = new Dictionary<string/*PLCID*/, int[,]>();
        Dictionary<string/*ATCNo*/, bool> ATCState = new Dictionary<string, bool>();
        List<string> list_id = new List<string>();

        Mutex mtx = new Mutex();
        private void checkCondition(string requiredID, string atcNo)
        {

            //Console.WriteLine("checkCondition requiredID {0} / atcNo {1}",requiredID, atcNo);
            LS_Socket tmp = null;
            mtx.WaitOne();
            try
            {
                if (getATCState(atcNo) == false)
                {
                    for (int i = 0; i < list_id.Count; i++)
                    {
                        string group = PLC_Socket_Info[requiredID].GROUP;
                        tmp = PLC_Socket_Info[list_id[i]].getSocketFromATCNo(group, atcNo);

                        if (tmp != null)
                        {
                            tmp.checkCondition(requiredID, atcNo);
                            break;
                        }
                    }
                }
                else if (atcNo == "30_1" || atcNo == "30_2")
                {
                    if (getATCState("30_1")) // 30_1 이 시나리오 중이다
                    {
                        alternativeATCNo(requiredID, "30_2");
                    }
                    else if (getATCState("30_2"))
                    {
                        alternativeATCNo(requiredID, "30_1");
                    }
                }
                else if (requiredID == "192.168.102.91" || requiredID == "192.168.102.92")
                {
                    if (getATCState(atcNo))
                    {
                        if (atcNo == "33")
                        {
                            alternativeATCNo(requiredID, "35");
                        }
                        if (atcNo == "35")
                        {
                            alternativeATCNo(requiredID, "33");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("checkCondition err ..{0}", ex.Message.ToString());
            }
            finally
            {
                mtx.ReleaseMutex();
            }

        }
        Thread plc_conn;
        Thread Session_;
        Thread Session_1;


        public void PLC_Connect()
        {
            ABN100_1 = new List<LS_Socket>();
            ABN100_2 = new List<LS_Socket>();
            ABN100_3 = new List<LS_Socket>();
            ABN100_4 = new List<LS_Socket>();

            string[] ATCNo;
            // ABN100#1     ip : 11~13, 15~22  
            {

                ATCNo = new string[] { "12" };
                tryConnectABNc_Nomal("192.168.102.11", "192.168.102.11", "ABN100_1", ATCNo, 1);

                ATCNo = new string[] { "4" };
                tryConnectABNc_Nomal("192.168.102.12", "192.168.102.12", "ABN100_1", ATCNo, 1);

                ATCNo = new string[] { "11", "13" };
                tryConnectABNc_Nomal("192.168.102.13", "192.168.102.13", "ABN100_1", ATCNo, 1);
                ATCNo = new string[] { "14" };
                tryConnectABNc_Nomal("192.168.102.15", "192.168.102.15", "ABN100_1", ATCNo, 1);

                ATCNo = new string[] { "1" };
                tryConnectABNc_Nomal("192.168.102.16", "192.168.102.16", "ABN100_1", ATCNo, 1);

                ATCNo = new string[] { "15" };
                tryConnectABNc_Nomal("192.168.102.17", "192.168.102.17", "ABN100_1", ATCNo, 1);

                ATCNo = new string[] { "16" };
                tryConnectABNc_Nomal("192.168.102.18", "192.168.102.18", "ABN100_1", ATCNo, 1);

                ATCNo = new string[] { "2" };
                tryConnectABNc_Nomal("192.168.102.19", "192.168.102.19", "ABN100_1", ATCNo, 1);

                ATCNo = new string[] { "3" };
                tryConnectABNc_Nomal("192.168.102.20", "192.168.102.20", "ABN100_1", ATCNo, 1);

                ATCNo = new string[] { "34_1", "34_2", "33" };
                tryConnectABNc_Nomal("192.168.102.21", "192.168.102.21", "ABN100_1", ATCNo, 1);

                ATCNo = new string[] { "30" };
                tryConnectABNc_Nomal("192.168.102.22", "192.168.102.22", "ABN100_1", ATCNo, 1);

                // ABN100#2    ip : 31~33  ,36, 37, 39~42

                ATCNo = new string[] { "18" };
                tryConnectABNc_Nomal("192.168.102.31", "192.168.102.31", "ABN100_2", ATCNo, 2);

                ATCNo = new string[] { "4" };
                tryConnectABNc_Nomal("192.168.102.32", "192.168.102.32", "ABN100_2", ATCNo, 2);

                ATCNo = new string[] { "17", "19" };
                tryConnectABNc_Nomal("192.168.102.33", "192.168.102.33", "ABN100_2", ATCNo, 2);

                ATCNo = new string[] { "5" };
                tryConnectABNc_Nomal("192.168.102.36", "192.168.102.36", "ABN100_2", ATCNo, 2);

                ATCNo = new string[] { "20" };
                tryConnectABNc_Nomal("192.168.102.37", "192.168.102.37", "ABN100_2", ATCNo, 2);

                ATCNo = new string[] { "6" };
                tryConnectABNc_Nomal("192.168.102.39", "192.168.102.39", "ABN100_2", ATCNo, 2);

                ATCNo = new string[] { "3" };
                tryConnectABNc_Nomal("192.168.102.40", "192.168.102.40", "ABN100_2", ATCNo, 2);

                ATCNo = new string[] { "34_3", "34_4", "35" };
                tryConnectABNc_Nomal("192.168.102.41", "192.168.102.41", "ABN100_2", ATCNo, 2);

                ATCNo = new string[] { "30" };
                tryConnectABNc_Nomal("192.168.102.42", "192.168.102.42", "ABN100_2", ATCNo, 2);

                // ABN100#3   ip : 51~53, 56~62


                ATCNo = new string[] { "22" };
                tryConnectABNc_Nomal("192.168.102.51", "192.168.102.51", "ABN100c_3", ATCNo, 3);

                ATCNo = new string[] { "8" };
                tryConnectABNc_Nomal("192.168.102.52", "192.168.102.52", "ABN100c_3", ATCNo, 3);

                ATCNo = new string[] { "21" };
                tryConnectABNc_Nomal("192.168.102.53", "192.168.102.53", "ABN100c_3", ATCNo, 3);

                ATCNo = new string[] { "7" };
                tryConnectABNc_Nomal("192.168.102.56", "192.168.102.56", "ABN100c_3", ATCNo, 3);

                ATCNo = new string[] { "23" };
                tryConnectABNc_Nomal("192.168.102.57", "192.168.102.57", "ABN100c_3", ATCNo, 3);

                ATCNo = new string[] { "24" };
                tryConnectABNc_Nomal("192.168.102.58", "192.168.102.58", "ABN100c_3", ATCNo, 3);

                ATCNo = new string[] { "9" };
                tryConnectABNc_Nomal("192.168.102.59", "192.168.102.59", "ABN100c_3", ATCNo, 3);

                ATCNo = new string[] { "10" };
                tryConnectABNc_Nomal("192.168.102.60", "192.168.102.60", "ABN100c_3", ATCNo, 3);

                ATCNo = new string[] { "32_5", "32_6", "35" };
                tryConnectABNc_Nomal("192.168.102.61", "192.168.102.61", "ABN100c_3", ATCNo, 3);

                ATCNo = new string[] { "30" };
                tryConnectABNc_Nomal("192.168.102.62", "192.168.102.62", "ABN100c_3", ATCNo, 3);

                // ABN100#4    ip : 71~73  , 76~82

                ATCNo = new string[] { "26" };
                tryConnectABNc_Nomal("192.168.102.71", "192.168.102.71", "ABN100c_4", ATCNo, 4);

                ATCNo = new string[] { "4" };
                tryConnectABNc_Nomal("192.168.102.72", "192.168.102.72", "ABN100c_4", ATCNo, 4);

                ATCNo = new string[] { "25", "27" };
                tryConnectABNc_Nomal("192.168.102.73", "192.168.102.73", "ABN100c_4", ATCNo, 4);

                ATCNo = new string[] { "5" };
                tryConnectABNc_Nomal("192.168.102.76", "192.168.102.76", "ABN100c_4", ATCNo, 4);

                ATCNo = new string[] { "28" };
                tryConnectABNc_Nomal("192.168.102.77", "192.168.102.77", "ABN100c_4", ATCNo, 4);

                ATCNo = new string[] { "29" };
                tryConnectABNc_Nomal("192.168.102.78", "192.168.102.78", "ABN100c_4", ATCNo, 4);

                ATCNo = new string[] { "6" };
                tryConnectABNc_Nomal("192.168.102.79", "192.168.102.79", "ABN100c_4", ATCNo, 4);

                ATCNo = new string[] { "3" };
                tryConnectABNc_Nomal("192.168.102.80", "192.168.102.80", "ABN100c_4", ATCNo, 4);
                ATCNo = new string[] { "32_1", "32_2", "33" };
                tryConnectABNc_Nomal("192.168.102.81", "192.168.102.81", "ABN100c_4", ATCNo, 4);
                //ATCNo[0] = "999"; ATCNo[1] = "0"; ATCNo[2] = "0";
                ATCNo = new string[] { "30" };
                tryConnectABNc_Nomal("192.168.102.82", "192.168.102.82", "ABN100c_4", ATCNo, 4);

                // EBN100_1,2 , 부품창고, ABN100포장_1,2, 경보시스템                
                ATCNo = new string[] { "34_5", "35", "32_3", "33" };
                tryConnectABNc_Nomal("192.168.102.91", "192.168.102.91", "EBN100c_1", ATCNo, 0);
                ATCNo = new string[] { "34_6", "35", "32_4", "33" };
                tryConnectABNc_Nomal("192.168.102.92", "192.168.102.92", "EBN100c_2", ATCNo, 0);
                ATCNo = new string[] { "1", "5_1", "5_2", "2", "6_1", "6_2", "9", "4_1", "4_2", "8", "3_1", "3_2", "7", "10", "28", "29", "15", "16", "20", "23", "24", "25", "26", "27", "11", "12", "13", "14", "17", "18", "19", "21", "22", "30_1", "30_2" };
                tryConnectABNc_Nomal("192.168.102.93", "192.168.102.93", "부품창고", ATCNo, 0);
                ATCNo = new string[] { "33", "32_1", "32_2", "32_3", "32_4", "32_5", "32_6" };
                tryConnectABNc_Nomal("192.168.102.94", "192.168.102.94", "ABN100c포장라인", ATCNo, 0);
                ATCNo = new string[] { "34_1", "34_2", "34_3", "34_4", "34_5", "34_6", "35" };
                tryConnectABNc_Nomal("192.168.102.95", "192.168.102.95", "ABN100c포장라인_2", ATCNo, 0);
                //ATCNo = new string[] { "없음" };
                //tryConnectABNc_Nomal("192.168.102.96", "192.168.102.96", "경보시스템", ATCNo, 0);
                addATCState(new string[] { "30_1", "30_2" });
            }
        }
        void addATCState(string[] atcName)
        {
            foreach (string str in atcName)
            {
                if (!ATCState.ContainsKey(str))
                {
                    ATCState.Add(str, false);
                }
            }
        }
        bool getATCState(string id)
        {
            if (ATCState.ContainsKey(id))
            {
                return ATCState[id];
            }

            Console.WriteLine(id + " is empty id");
            return false;
        }
        public void setATCState(string id, bool value)
        {
            if (ATCState.ContainsKey(id))
            {

                Console.WriteLine("setATCState ATCNo [{0}] [{1}]", id, value);
                printMsg(string.Format("setATCState ATCNo [{0}] [{1}]", id, value)); //로그보는 부분 추가했어요, 수정되면 삭제
                ATCState[id] = value;
                Thread.Sleep(100);
            }
        }

        private void tryConnectABNc_Nomal(string ip, string id, string group, string[] ATC, int lineNo)
        {
            try
            {
                addATCState(ATC);
                LS_Socket tmpSocket;
                tmpSocket = tryConnect(id, ip, group, ATC);
                switch (lineNo)
                {
                    case 1:
                        if (ABN100_1.Contains(tmpSocket))
                        {
                            ABN100_1.Remove(tmpSocket);
                        }
                        ABN100_1.Add(tmpSocket);
                        break;
                    case 2:
                        if (ABN100_2.Contains(tmpSocket))
                        {
                            ABN100_2.Remove(tmpSocket);
                        }
                        ABN100_2.Add(tmpSocket);
                        break;
                    case 3:
                        if (ABN100_3.Contains(tmpSocket))
                        {
                            ABN100_3.Remove(tmpSocket);
                        }
                        ABN100_3.Add(tmpSocket);
                        break;
                    case 4:
                        if (ABN100_4.Contains(tmpSocket))
                        {
                            ABN100_4.Remove(tmpSocket);
                        }
                        ABN100_4.Add(tmpSocket);
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                Console.WriteLine("try connect abn error");
            }

        }
        public List<string> ScenarioList = new List<string>();

        void addScenarioList(string atcNo)
        {
            if (!ScenarioList.Contains(atcNo))
            {
                ScenarioList.Add(atcNo);
            }
        }

        void deleteMisionList(string id, string atcNo)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                if (Session_1_mission.Any(n => n.atcNO == atcNo))
                {
                    Session_1_mission.RemoveAll(n => (n.atcNO == atcNo) && ((n.requiredID == id) || (n.startID == id)));
                    firstform.DP_Session_1();
                }
                else
                {
                    Session_2_mission.RemoveAll(n => (n.atcNO == atcNo) && ((n.requiredID == id) || (n.startID == id)));
                    firstform.DP_Session_2();
                }
            }));
            Console.WriteLine($"상황 해제 [{id}] , [{atcNo}]");
        }

        /// <summary>
        /// 상황 해제 이벤트
        /// </summary>
        /// <param name="id"></param>
        /// <param name="atcNo"></param>
        /// <param name="value"></param>
        void LS_ATCNo_state(string id, string atcNo, bool value)
        {
            if (ScenarioList.Contains(atcNo)) //시나리오 중
            {
                if (PLC_Socket_Info[id].STATE_PRE == LS_Socket.state.REQUIRE) // 물건을 받아야 하는 PLC 이면
                {
                    deleteMisionList(id, atcNo);
                }
                else // 물건 배출해야하는 PLC
                {
                    // 암것도 안함
                }
            }
            else // 시나리오 아님 
            {
                deleteMisionList(id, atcNo);
                printMsg($"상황 해제 [{atcNo}], [{false}] ");
                setATCState(atcNo,false);
            }


            //Invoke(new MethodInvoker(delegate ()
            //{
            //    if (Session_1_mission.Any(n => n.atcNO == atcNo))
            //        Session_1_mission.RemoveAll(n => n.atcNO == atcNo);
            //    else
            //        Session_2_mission.RemoveAll(n => n.atcNO == atcNo);
            //}));
            //setATCState(atcNo, value);

        }
        private LS_Socket tryConnect(string id, string ip, string group, string[] m_ATCNo)
        {
            LS_Socket lssocket = new LS_Socket(id, ip, 2004, group, m_ATCNo);
            try
            {
                lssocket.onLS_SocketInit();
                lssocket.lsconnect_Evt += new LS_Socket.LS_Connect(this.LS_Connect);
                lssocket.lsrev_Evt += new LS_Socket.LS_Response(this.LS_Response);
                lssocket.scenarioCall_Evt += new LS_Socket.LS_ScenarioCall(this.LS_ScenarioCall);
                lssocket.scenarioStart_Evt += new LS_Socket.LS_ScenarioStart(this.LS_ScenarioStart);
                lssocket.ATCState_Evt += new LS_Socket.LS_ATCNo_state(this.LS_ATCNo_state);

                if (PLC_Socket_Info.ContainsKey(id))
                {
                    PLC_Socket_Info[id].Disconnect();
                    PLC_Socket_Info.Remove(id);
                }

                PLC_Socket_Info.Add(id, lssocket);
                //addLineComboBox(id);
                list_id.Add(id);

            }
            catch
            {
                Console.WriteLine("sdlfjasfjiwejfooboix");
            }
            finally
            {
            }
            return lssocket;
        }


        Ping pingSender = new Ping();
        System.Net.NetworkInformation.PingOptions options = new PingOptions();



        private void robotflagTimer_Tick(object sender, EventArgs e)
        {

        }
        private void check_Lastholding(string strrobotid, int idx)
        {
            float temp;
            int index = 0;
            try
            {
                float temp1 = Data.Instance.Robot_work_info[strrobotid].robot_status_info.controllerstate.msg.data[0];
                temp = temp1;
                if (temp == temp1)
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
                if (index == 5)
                {
                    Data.Instance.robotFlag[strrobotid] = false;
                }
                else
                {
                    Data.Instance.robotFlag[strrobotid] = true;
                }
            }
            catch
            {
                Console.WriteLine("error");
            }
        }

        List<string> Enable_Robot_List = new List<string>();

        public Dictionary<string, missionType> resetPLC = new Dictionary<string, missionType>();
        private void onRobotCheck()
        {
            while (true)
            {
                try
                {
                    if (Data.Instance.isConnected) // 리디스 접속 됐는지
                    {

                        foreach (KeyValuePair<string, Robot_RegInfo> info in Data.Instance.Robot_RegInfo_list)
                        {
                            int workstate = 99;
                            if (pingTest(info.Value.robot_ip))
                            {
                                //if (Data.Instance.Robot_work_info[info.Key].robot_status_info.bmsinfo.msg != null)
                                if (true)
                                {
                                    //if (Data.Instance.Robot_work_info[info.Key].robot_status_info.bmsinfo.msg.data.Count() > 0)
                                    if (true)
                                    {
                                        for (int i2 = 0; i2 < Data.Instance.robot_liveinfo.robotinfo.msg.robolist.Count; i2++)
                                        {
                                            string RID = Data.Instance.robot_liveinfo.robotinfo.msg.robolist[i2].RID;
                                            double x = Data.Instance.robot_liveinfo.robotinfo.msg.robolist[i2].pose.x;
                                            double y = Data.Instance.robot_liveinfo.robotinfo.msg.robolist[i2].pose.y;
                                            double theta = Data.Instance.robot_liveinfo.robotinfo.msg.robolist[i2].pose.theta;

                                            Console.WriteLine($"RID : {RID} x : {x} y: : {y}");
                                        }
                                        if (!Data.Instance.onRobot.ContainsKey(info.Key))
                                        {
                                            Data.Instance.onRobot.Add(info.Key, true);
                                            Data.Instance.offRobot.Remove(info.Key);
                                        }
                                        else if (Data.Instance.onRobot.ContainsKey(info.Key))
                                        {
                                            //Console.WriteLine("로봇 대기중 : {0}", info.Value.robot_id);
                                        }
                                        for (int i = 0; i < Data.Instance.robot_liveinfo.robotinfo.msg.robolist.Count; i++)
                                        {
                                            if (Data.Instance.robot_liveinfo.robotinfo.msg.robolist[i].RID.Contains(info.Key))
                                            {
                                                workstate = Data.Instance.robot_liveinfo.robotinfo.msg.robolist[i].workstate;
                                                break;
                                            }
                                        }


                                        if (true)
                                        {
                                            if (workstate == 0)
                                            {
                                                if (!Data.Instance.Enable_Robot.ContainsKey(info.Key))
                                                {
                                                    Data.Instance.Enable_Robot.Add(info.Key, true);
                                                    Data.Instance.Disable_Robot.Remove(info.Key);

                                                    Console.WriteLine("로봇 대기중 : {0}", info.Value.robot_id);
                                                }
                                                else if (Data.Instance.Enable_Robot.ContainsKey(info.Key))
                                                {
                                                    if (!Enable_Robot_List.Contains(info.Key))
                                                    {
                                                        Enable_Robot_List.Add(info.Key);
                                                        Console.WriteLine("로봇 대기중 : {0}", info.Value.robot_id);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {

                                }
                            }
                            else
                            {
                                if (!Data.Instance.Disable_Robot.ContainsKey(info.Key))
                                {
                                    Data.Instance.Disable_Robot.Add(info.Key, false);
                                    Data.Instance.Enable_Robot.Remove(info.Key);
                                    //Console.WriteLine("로봇 대기중 아님 : {0}", info.Value.robot_id);
                                }
                                else if (Data.Instance.Disable_Robot.ContainsKey(info.Key))
                                {
                                    //Console.WriteLine("로봇 안켜져 있음 : {0}", info.Value.robot_id); 
                                }

                            }
                        }
                        Thread.Sleep(100);

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Robot state 불러오기 실패 : {0}", ex);
                }

            }
        }
        public bool pingTest(string robotip)
        {
            options.DontFragment = true;
            string data = "this is test message";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;

            try
            {

                PingReply reply = pingSender.Send(robotip, timeout, buffer, options);

                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("에러 {0} : ", ex);
                return false;
            }
        }
        public void LS_Connect(string id, byte data)
        {
            try
            {
                if (data == 0x01) // connect success
                {
                    int[,] plc = new int[8, 10];
                    if (PLCData.ContainsKey(id))
                    {
                        PLCData.Remove(id);
                    }
                    PLCData.Add(id, plc);
                }
                else if (data == 0x02) //delete id
                {
                    PLCData.Remove(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("LS_Connect err ..{0}", ex.Message.ToString());
            }
        }
        public void LS_Response(string strPLCID, string strData, int[,] plc_data)
        {
            try
            {
                if (PLCData.ContainsKey(strPLCID))
                {
                    for (int i = 0; i < plc_data.GetLength(0); i++)
                    {
                        for (int j = 0; j < plc_data.GetLength(1); j++)
                        {
                            PLCData[strPLCID][i, j] = plc_data[i, j];
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("LS_Response err..{0}", ex.Message.ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            foreach (string pid in new List<string>(PLC_Socket_Info.Keys))
            {

                if (PLC_Socket_Info.ContainsKey(pid))
                {
                    if (PLC_Socket_Info[pid].bConnected)
                    {
                        PLC_Socket_Info[pid].onXGT_ContinuousByteDataRead(6);
                    }
                }
            }
            if (PLC_Socket_Info.Count <= 0)
            {
                Console.WriteLine("timer stop");
                timer1.Stop();
            }
        }


        private void accordionControlElement1_Click_1(object sender, EventArgs e)
        {
            displaymap = new displayMap();
            if (mainContainer.Controls.Count == 1)
            {
                mainContainer.Controls.RemoveAt(0);

            }

            mainContainer.Controls.Add(displaymap);
            mainContainer.BringToFront();
            displaymap.aboutMove();
        }

        private void dataCheck_Tick(object sender, EventArgs e)
        {

        }


        public void LS_ScenarioCall(string id, string atcNo, string scen)
        {

            if (PLC_Socket_Info.ContainsKey(id))
            {
                checkCondition(id, atcNo);
                if (PLC_Socket_Info[id].GetSTATE() == 0) // IDEL
                {
                    Console.WriteLine("plc state : IDLE");
                    PLC_Socket_Info[id].SetSTATE(2);
                }
                else if (PLC_Socket_Info[id].GetSTATE() == 2) //required
                {
                    checkCondition(id, atcNo);
                }
                else if (atcNo == "33" || atcNo == "35")
                {
                    checkCondition(id, atcNo);
                }
                else if (PLC_Socket_Info[id].GROUP == LS_Socket.GROUP_ABN100PACK_1 || PLC_Socket_Info[id].GROUP == LS_Socket.GROUP_ABN100PACK_2 || PLC_Socket_Info[id].GROUP == LS_Socket.GROUP_PARTSWAREHOUSE)
                {
                    checkCondition(id, atcNo);
                }
            }
        }



        private void missionLOAD()
        {
            dbBridge.onDBRead_ACTinfo();
            //robotOverview robotoverview = new robotOverview();

            //mainContainer.Controls.Add(robotoverview);

        }
        string[] missionbuf = { "MID20191023155013", "MID20191024104116" };


        List<string> reservation_required = new List<string>();

        public List<missionType> Session_2_mission = new List<missionType>();
        public List<missionType> Session_1_mission = new List<missionType>();
        public class missionType
        {
            public string requiredID;
            public string startID;
            public string atcNO;
            public string mission_;
            public int startNode;
            public int endNode;
        }

        private static DateTime Delay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }

            return DateTime.Now;
        }
        ManualResetEvent sessionThread = new ManualResetEvent(true);
        private void session_all_start()
        {

            while (sessionThread.WaitOne())
            {
                try
                {
                    if (Session_1_mission != null)
                    {
                        if (Session_1_mission.Count > 0)
                        {
                            if (Enable_Robot_List.Count > 0)
                            {
                                for (int i = 0; i < Enable_Robot_List.Count; i++)
                                {

                                    if (Enable_Robot_List[i] == "R_001" && bRun_001)
                                    {
                                        missionType temp = new missionType();
                                        string atcNO = "";
                                        string startID = "";
                                        string requiredID = "";
                                        string mission = "";
                                        int cnt = Session_1_mission.Count;
                                        atcNO = Session_1_mission.Last().atcNO;
                                        startID = Session_1_mission.Last().startID;
                                        requiredID = Session_1_mission.Last().requiredID;
                                        mission = Session_1_mission.Last().mission_;

                                        PLC_Socket_Info[requiredID].SetSTATE(3);
                                        PLC_Socket_Info[requiredID].startATCNo = atcNO;
                                        PLC_Socket_Info[startID].SetSTATE(3);
                                        setATCState(atcNO, true);


                                        
                                        firstform.DP_Session_1();
                                        //if (Session_1_mission.Count > 0)
                                        //    Session_1_mission.RemoveAt(Session_1_mission.Count - 1);

                                        Session_1_taskStart(requiredID, startID, mission, atcNO, "R_001");

                                        bRun_001 = false;
                                        Enable_Robot_List.Remove("R_001");
                                        
                                        firstform.DP_Session_2();
                                        Session_1_mission.RemoveAll(n => n.atcNO == atcNO && n.requiredID == requiredID);

                                        Invoke(new MethodInvoker(delegate ()
                                        {
                                            firstform.textBox1.AppendText(string.Format("[R_001] {0} -> {1} [{2}] 미션 시작", startID, requiredID, atcNO) + "\r\n");
                                        }));

                                        temp.atcNO = atcNO;
                                        temp.mission_ = mission;
                                        temp.startID = startID;
                                        temp.requiredID = requiredID;
                                        if (!resetPLC.Keys.Contains("R_001"))
                                            resetPLC.Add("R_001", temp);
                                        Delay(10000); 
                                        break;

                                    }
                                    else if (Enable_Robot_List[i] == "R_003" & bRun_003)
                                    {
                                        missionType temp = new missionType();
                                        string atcNO = "";
                                        string startID = "";
                                        string requiredID = "";
                                        string mission = "";
                                        int cnt = Session_1_mission.Count;
                                        atcNO = Session_1_mission.Last().atcNO;
                                        startID = Session_1_mission.Last().startID;
                                        requiredID = Session_1_mission.Last().requiredID;
                                        mission = Session_1_mission.Last().mission_;

                                        PLC_Socket_Info[requiredID].SetSTATE(3);
                                        PLC_Socket_Info[requiredID].startATCNo = atcNO;
                                        PLC_Socket_Info[startID].SetSTATE(3);
                                        setATCState(atcNO, true);

                                        

                                        Session_1_taskStart(requiredID, startID, mission, atcNO, "R_003");
                                        bRun_003 = false;
                                        Enable_Robot_List.Remove("R_003");
                                        Session_1_mission.RemoveAll(n => n.atcNO == atcNO && n.requiredID == requiredID);
                                        firstform.DP_Session_1();


                                        Invoke(new MethodInvoker(delegate ()
                                        {
                                            firstform.textBox1.AppendText(string.Format("[R_003] {0} -> {1} [{2}] 미션 시작", startID, requiredID, atcNO) + "\r\n");
                                        }));

                                        temp.atcNO = atcNO;
                                        temp.mission_ = mission;
                                        temp.startID = startID;
                                        temp.requiredID = requiredID;
                                        if (!resetPLC.Keys.Contains("R_003"))
                                            resetPLC.Add("R_003", temp);
                                        Delay(10000); ;
                                        break;
                                    }
                                    else if (Enable_Robot_List[i] == "R_004" & bRun_004)
                                    {
                                        missionType temp = new missionType();
                                        string atcNO = "";
                                        string startID = "";
                                        string requiredID = "";
                                        string mission = "";
                                        int cnt = Session_1_mission.Count;
                                        atcNO = Session_1_mission.Last().atcNO;
                                        startID = Session_1_mission.Last().startID;
                                        requiredID = Session_1_mission.Last().requiredID;
                                        mission = Session_1_mission.Last().mission_;

                                        PLC_Socket_Info[requiredID].SetSTATE(3);
                                        PLC_Socket_Info[requiredID].startATCNo = atcNO;
                                        PLC_Socket_Info[startID].SetSTATE(3);
                                        setATCState(atcNO, true);



                                        Session_1_taskStart(requiredID, startID, mission, atcNO, "R_004");
                                        bRun_004 = false;
                                        Enable_Robot_List.Remove("R_004");
                                        Session_1_mission.RemoveAll(n => n.atcNO == atcNO && n.requiredID == requiredID);
                                        firstform.DP_Session_1();

                                        Invoke(new MethodInvoker(delegate ()
                                        {
                                            firstform.textBox1.AppendText(string.Format("[R_004] {0} -> {1} [{2}] 미션 시작", startID, requiredID, atcNO) + "\r\n");
                                        }));

                                        temp.atcNO = atcNO;
                                        temp.mission_ = mission;
                                        temp.startID = startID;
                                        temp.requiredID = requiredID;
                                        if (!resetPLC.Keys.Contains("R_004"))
                                            resetPLC.Add("R_004", temp);
                                        Delay(10000); ;
                                        break;
                                    }
                                    else if (Enable_Robot_List[i] == "R_005" & bRun_005)
                                    {
                                        missionType temp = new missionType();
                                        string atcNO = "";
                                        string startID = "";
                                        string requiredID = "";
                                        string mission = "";
                                        int cnt = Session_1_mission.Count;
                                        atcNO = Session_1_mission.Last().atcNO;
                                        startID = Session_1_mission.Last().startID;
                                        requiredID = Session_1_mission.Last().requiredID;
                                        mission = Session_1_mission.Last().mission_;

                                        PLC_Socket_Info[requiredID].SetSTATE(3);
                                        PLC_Socket_Info[requiredID].startATCNo = atcNO;
                                        PLC_Socket_Info[startID].SetSTATE(3);
                                        setATCState(atcNO, true);



                                        Session_1_taskStart(requiredID, startID, mission, atcNO, "R_005");
                                        bRun_005 = false;
                                        Enable_Robot_List.Remove("R_005");
                                        Session_1_mission.RemoveAll(n => n.atcNO == atcNO && n.requiredID == requiredID);
                                        firstform.DP_Session_1();


                                        Invoke(new MethodInvoker(delegate ()
                                        {
                                            firstform.textBox1.AppendText(string.Format("[R_005] {0} -> {1} [{2}] 미션 시작", startID, requiredID, atcNO) + "\r\n");
                                        }));

                                        temp.atcNO = atcNO;
                                        temp.mission_ = mission;
                                        temp.startID = startID;
                                        temp.requiredID = requiredID;
                                        if (!resetPLC.Keys.Contains("R_005"))
                                            resetPLC.Add("R_005", temp);
                                        Delay(10000); ;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Invoke(new MethodInvoker(delegate ()
                        {
                        }));
                    }
                    Thread.Sleep(50);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Session 1 task start error -> {0}", e);
                    //printMsg("Session 1 task start error " + e);
                    firstform.robotThreadstate.BackColor = Color.Red;
                }
                try
                {
                    if (Enable_Robot_List.Count > 0)
                    {
                        Session_2_Method();
                        Thread.Sleep(50);
                    }
                    else
                    {
                        Invoke(new MethodInvoker(delegate ()
                        {
                        }));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Session 2 task start error -> {0}", e);
                    //printMsg("Session 2 task start error " + e);
                    firstform.robotThreadstate.BackColor = Color.Red;
                }
            }
        }
        bool bSession2 = false;
        void Session_2_Method()
        {
            if (Session_2_mission != null)
            {
                if (Session_2_mission.Count > 0)
                {
                    for (int i = 0; i < Enable_Robot_List.Count; i++)
                    {
                        if (Enable_Robot_List[i] == "R_006" && bRun_006)
                        {
                            missionType temp = new missionType();
                            string atcNO = "";
                            string startID = "";
                            string requiredID = "";
                            string mission = "";
                            int cnt = Session_2_mission.Count;

                            atcNO = Session_2_mission.Last().atcNO;
                            startID = Session_2_mission.Last().startID;
                            requiredID = Session_2_mission.Last().requiredID;
                            mission = Session_2_mission.Last().mission_;

                            PLC_Socket_Info[requiredID].SetSTATE(3);
                            PLC_Socket_Info[requiredID].startATCNo = atcNO;
                            PLC_Socket_Info[startID].SetSTATE(3);
                            setATCState(atcNO, true);

                            if (Session_2_mission.Count > 0)
                            {
                                Session_2_mission.RemoveAt(Session_2_mission.Count - 1);
                                firstform.DP_Session_2();
                            }



                            Session_2_taskStart(requiredID, startID, mission, atcNO, "R_006");


                            bRun_006 = false;
                            Enable_Robot_List.Remove("R_006");


                            Invoke(new MethodInvoker(delegate ()
                            {
                                firstform.textBox1.AppendText(string.Format("[R006] {0} -> {1} [{2}] 미션 시작", startID, requiredID, atcNO) + "\r\n");
                            }));

                            temp.atcNO = atcNO;
                            temp.mission_ = mission;
                            temp.startID = startID;
                            temp.requiredID = requiredID;
                            if (!resetPLC.Keys.Contains("R_006"))
                                resetPLC.Add("R_006", temp);

                            Enable_Robot_List.Remove("R_006");
                            Session_2_mission.RemoveAt(Session_2_mission.Count - 1);
                            firstform.DP_Session_2();


                            Delay(10000); ;
                            break;
                        }
                        else if (Enable_Robot_List[i] == "R_007" && bRun_007)
                        {
                            missionType temp = new missionType();
                            string atcNO = "";
                            string startID = "";
                            string requiredID = "";
                            string mission = "";
                            int cnt = Session_2_mission.Count;
                            atcNO = Session_2_mission.Last().atcNO;
                            startID = Session_2_mission.Last().startID;
                            requiredID = Session_2_mission.Last().requiredID;
                            mission = Session_2_mission.Last().mission_;

                            PLC_Socket_Info[requiredID].SetSTATE(3);
                            PLC_Socket_Info[requiredID].startATCNo = atcNO;
                            PLC_Socket_Info[startID].SetSTATE(3);
                            setATCState(atcNO, true);

                            Session_2_taskStart(requiredID, startID, mission, atcNO, "R_007");

                            bRun_007 = false;
                            Enable_Robot_List.Remove("R_007");



                            Invoke(new MethodInvoker(delegate ()
                            {
                                firstform.textBox1.AppendText(string.Format("[R007] {0} -> {1} [{2}] 미션 시작", startID, requiredID, atcNO) + "\r\n");
                            }));

                            temp.atcNO = atcNO;
                            temp.mission_ = mission;
                            temp.startID = startID;
                            temp.requiredID = requiredID;
                            if (!resetPLC.Keys.Contains("R_007"))
                                resetPLC.Add("R_007", temp);

                            Enable_Robot_List.Remove("R_007");
                            Session_2_mission.RemoveAt(Session_2_mission.Count - 1);
                            firstform.DP_Session_2();


                            Delay(10000); ;
                            break;
                        }
                    }
                }
            }
        }
        void printMsg(string msg)
        {
            try
            {
                Invoke(new MethodInvoker(delegate ()
                    {
                        firstform.textBox1.AppendText("[" + DateTime.Now + "] " + msg + "\r\n");
                    }));
            }
            catch (Exception ex)
            {
                Console.WriteLine("printMsg err {0}", ex.Message.ToString());
            }
        }
        string Search_minpath(string strrobotid)
        {
            int min = 0;
            int temp = 0;
            string atc_no = "";
            min = dijkstra_in(firstform.findLocation(strrobotid), Session_2_mission[0].startNode, "EMPTY");
            atc_no = Session_2_mission[0].atcNO;
            foreach (missionType type in Session_2_mission)
            {
                temp = dijkstra_in(firstform.findLocation(strrobotid), type.startNode, "EMPTY");
                if (min > temp)
                {
                    min = temp;
                    atc_no = type.atcNO;
                }
            }
            return atc_no;
        }
        private void path_search()
        {
            Data.Instance.Session_2_path.Add("32_5_C", 12);
            Data.Instance.Session_2_path.Add("32_6_C", 12);
            Data.Instance.Session_2_path.Add("35_C", 12);
            Data.Instance.Session_2_path.Add("34_3_B", 8);
            Data.Instance.Session_2_path.Add("34_4_B", 8);
            Data.Instance.Session_2_path.Add("35_B", 8);
            Data.Instance.Session_2_path.Add("34_1_A", 6);
            Data.Instance.Session_2_path.Add("34_2_B", 6);
            Data.Instance.Session_2_path.Add("33_B", 6);
            Data.Instance.Session_2_path.Add("32_1_D", 3);
            Data.Instance.Session_2_path.Add("32_2_D", 3);
            Data.Instance.Session_2_path.Add("33_D", 3);
            Data.Instance.Session_2_path.Add("32_4EB", 8);
            Data.Instance.Session_2_path.Add("34_6EB", 8);
            Data.Instance.Session_2_path.Add("34_5EA", 11);
            Data.Instance.Session_2_path.Add("32_3EA", 11);
            Data.Instance.Session_2_path.Add("35EA", 11);
            Data.Instance.Session_2_path.Add("33EA", 11);
            Data.Instance.Session_2_path.Add("35EB", 8);
            Data.Instance.Session_2_path.Add("33EB", 8);
        }
        private int Atc_node_check(string atcNO)
        {
            switch (atcNO)
            {
                case "32_1":
                    return 3;
                case "32_2":
                    return 3;
                case "32_3":
                    return 11;
                case "32_4":
                    return 8;
                case "32_5":
                    return 12;
                case "32_6":
                    return 12;
                case "34_1":
                    return 6;
                case "34_2":
                    return 6;
                case "34_3":
                    return 8;
                case "34_4":
                    return 8;
                case "34_5":
                    return 11;
                case "34_6":
                    return 8;
                case "33":
                    return 0;
                case "35":
                    return 0;
                default:
                    return -1;

            }
        }
        public void LS_ScenarioStart(string requiredID, string startID, string mission, string atcNo)
        {
            //Console.WriteLine("ScenarioStart requied {0} / start {1} / atcNo {2}",requiredID,startID,atcNo);
            try
            {
                if (getATCState(atcNo) == true) return;
                missionType temp = new missionType();
                List<string> list = new List<string>(Data.Instance.Enable_Robot.Keys);


                // 포장라인
                if (atcNo == "32_1" || atcNo == "32_2" || atcNo == "32_5" ||
                     atcNo == "32_6" || atcNo == "34_1" || atcNo == "34_2" || atcNo == "34_3" || atcNo == "34_4" || atcNo == "33"
                     || atcNo == "32_3" || atcNo == "32_4" || atcNo == "35" || atcNo == "34_5" || atcNo == "34_6")//atcNo == "32_1" ||atcNo == "32_3" || atcNo == "32_4" ||atcNo == "33"  ||
                {

                    temp.requiredID = requiredID;
                    temp.startID = startID;
                    temp.atcNO = atcNo;
                    temp.mission_ = mission;
                    temp.startNode = Atc_node_check(atcNo);
                    PLC_Socket_Info[requiredID].SetSTATE(3);
                    PLC_Socket_Info[requiredID].startATCNo = atcNo;
                    PLC_Socket_Info[startID].SetSTATE(3);
                    setATCState(atcNo, true);
                    Delay(500); ;

                    if (atcNo == "33" || atcNo == "35")
                    {
                        if (!Session_2_mission.Any(n => n.atcNO == atcNo))
                        {
                            Session_2_mission.Add(temp);
                            firstform.DP_Session_2();
                        }
                    }
                    else
                    {
                        if (!Session_2_mission.Any(n => n.atcNO == atcNo))
                        {
                            Session_2_mission.Insert(0, temp);
                            firstform.DP_Session_2();
                        }
                    }


                    Invoke(new MethodInvoker(delegate ()
                    {
                        string required = requiredID;
                        string start = startID;
                        string atc = atcNo;
                        string str = string.Format("{0} 요청. 시작 -> {1} ATC Number : {2}", required, start, atc);
                        if (atcNo == "33" || atcNo == "35")
                        { }
                        else
                        { }

                    }));


                }

                //부품창고
                else
                {
                    temp.requiredID = requiredID;
                    temp.startID = startID;
                    temp.atcNO = atcNo;
                    temp.mission_ = mission;


                    PLC_Socket_Info[requiredID].SetSTATE(3);
                    PLC_Socket_Info[requiredID].startATCNo = atcNo;
                    PLC_Socket_Info[startID].SetSTATE(3);
                    setATCState(atcNo, true);


                    Delay(500); ;
                    Invoke(new MethodInvoker(delegate ()
                    {
                        string required = requiredID;
                        string start = startID;
                        string atc = atcNo;
                        string str = string.Format("{0} 요청. 시작 -> {1} ATC Number : {2}", required, start, atc);

                    }));
                    if (!Session_1_mission.Any(n => n.atcNO == atcNo))
                    {
                        Session_1_mission.Insert(0, temp);
                        firstform.DP_Session_1();
                    }
                }
                //else
                //{
                //    PLC_Socket_Info[requiredID].SetSTATE(3);
                //    PLC_Socket_Info[startID].SetSTATE(3);
                //    setATCState(atcNo, true);
                //}


                //만약 메시지를 원할때 받고 싶으면 위 3줄(State(3) 2개, setATCState 1개) 주석처리하고 원하는 곳에 위 3줄을 넣어주면 됩니다.
                Console.WriteLine("Required ID :{0} , startid : {1},mission : {2}, actno : {3}", requiredID, startID, mission, atcNo);

            }
            catch (Exception e)
            {
                Console.WriteLine("시나리오 스타트 에러{0}", e);
                throw;
            }
        }

        public static mainForm getForm()
        {
            return form;
        }

        List<string> mission_temp_list = new List<string>();

        private void Session_1_method(string strrobotid, string atcNO)
        {
            mission_temp = new string[mission_temp_list.Count()];
            mission_temp = mission_temp_list.ToArray();
            int cnt = mission_temp.Count();

            taskoperationform.taskSave(atcNO, taskoperationform.ConvertString(mission_temp), strrobotid);

            mission_temp_list.Clear();
            dbBridge.onDBDelete_Missionlist("TEMP_");

            CrashCheckRobot_list.Add(strrobotid);
            Invoke(new MethodInvoker(delegate ()
            {
            }));
        }

        public void DP_currentmission(string strrobotid, string mission)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                switch (strrobotid)
                {
                    case "R_001":
                        //Regex.Replace(firstform.robot5From.Text = mission, "_", " ");
                        firstform.robot1From.Text = mission;
                        return;
                    case "R_002":
                        //Regex.Replace(firstform.robot5From.Text = mission, "_", " ");
                        firstform.robot2From.Text = mission;
                        return;
                    case "R_003":
                        //Regex.Replace(firstform.robot5From.Text = mission, "_", " ");
                        firstform.robot3From.Text = mission;
                        return;
                    case "R_004":
                        //Regex.Replace(firstform.robot5From.Text = mission, "_", " ");
                        firstform.robot4From.Text = mission;
                        return;
                    case "R_005":
                        //Regex.Replace(firstform.robot5From.Text = mission, "_", " ");
                        firstform.robot5From.Text = mission;
                        return;
                    case "R_006":
                        //Regex.Replace(firstform.robot5From.Text = mission, "_", " ");
                        firstform.robot6From.Text = mission;
                        return;
                    case "R_007":
                        //Regex.Replace(firstform.robot5From.Text = mission, "_", " ");
                        firstform.robot7From.Text = mission;
                        return;
                    default:
                        return;
                }
            }));

        }
        //Session 1 start(부품창고쪽)
        public void Session_1_taskStart(string requiredID, string startID, string mission, string atcNO, string strrobotid)
        {
            addScenarioList(atcNO); //수정한 부분
            string temp_str = "";
            int startnode = firstform.findLocation(strrobotid);
            if (atcNO == "30_1")
            {
                if (startID == "192.168.102.22") // ABN100#1 LINE A
                {
                    //DAF
                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), int.Parse(Data.Instance.callByplc["30_A"].in_));
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ABN_LINE_A_30");
                    mission_temp_list.Add("GO_9_13");
                    mission_temp_list.Add("ATC_30_1_wh");
                    dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_30_1_wh", "ABN_LINE_A_30");
                    DP_currentmission(strrobotid, "ABN_LINE_A_30");
                    Session_1_method(strrobotid, atcNO);
                }
                else if (startID == "192.168.102.42") // ABN100#2 LINE B
                {
                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 12);
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ABN_LINE_B_30");
                    mission_temp_list.Add("GO_11_13");
                    mission_temp_list.Add("ATC_30_1_wh");
                    dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_30_1_wh", "ABN_LINE_B_30");
                    DP_currentmission(strrobotid, "ABN_LINE_B_30");
                    Session_1_method(strrobotid, atcNO);
                }
                else if (startID == "192.168.102.62") // ABN100#3 LINE C
                {
                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), int.Parse(Data.Instance.callByplc["30_C"].in_));
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ABN_LINE_C_30");
                    mission_temp_list.Add("GO_15_13");
                    mission_temp_list.Add("ATC_30_1_wh");
                    dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_30_1_wh", "ABN_LINE_C_30");
                    DP_currentmission(strrobotid, "ABN_LINE_C_30");
                    Session_1_method(strrobotid, atcNO);
                }
                else if (startID == "192.168.102.82") // ABN100#4 LINE D
                {
                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 6);
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ABN_LINE_D_30");
                    mission_temp_list.Add("GO_5_13");
                    mission_temp_list.Add("ATC_30_1_wh");
                    dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_30_1_wh", "ABN_LINE_D_30");
                    DP_currentmission(strrobotid, "ABN_LINE_D_30");
                    Session_1_method(strrobotid, atcNO);
                }

            }
            else if (atcNO == "30_2")
            {
                if (startID == "192.168.102.22") // ABN100#1 LINE A
                {
                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), int.Parse(Data.Instance.callByplc["30_A"].in_));
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ABN_LINE_A_30");
                    mission_temp_list.Add("GO_9_13");
                    mission_temp_list.Add("ATC_30_2_wh");
                    dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_30_2_wh", "ABN_LINE_A_30");
                    DP_currentmission(strrobotid, "ABN_LINE_A_30");
                    Session_1_method(strrobotid, atcNO);
                }
                else if (startID == "192.168.102.42") // ABN100#2 LINE B
                {
                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), int.Parse(Data.Instance.callByplc["30_B"].in_));
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ABN_LINE_B_30");
                    mission_temp_list.Add("GO_11_13");
                    mission_temp_list.Add("ATC_30_2_wh");
                    dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_30_2_wh", "ABN_LINE_B_30");
                    DP_currentmission(strrobotid, "ABN_LINE_B_30");
                    Session_1_method(strrobotid, atcNO);
                }
                else if (startID == "192.168.102.62") // ABN100#3 LINE C
                {
                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), int.Parse(Data.Instance.callByplc["30_C"].in_));
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ABN_LINE_C_30");
                    mission_temp_list.Add("GO_15_13");
                    mission_temp_list.Add("ATC_30_2_wh");
                    dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_30_2_wh", "ABN_LINE_C_30");
                    DP_currentmission(strrobotid, "ABN_LINE_C_30");
                    Session_1_method(strrobotid, atcNO);
                }
                else if (startID == "192.168.102.82") // ABN100#4 LINE D
                {
                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), int.Parse(Data.Instance.callByplc["30_D"].in_));
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ABN_LINE_D_30");
                    mission_temp_list.Add("GO_5_13");
                    mission_temp_list.Add("ATC_30_2_wh");
                    dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_30_2_wh", "ABN_LINE_D_30");
                    DP_currentmission(strrobotid, "ABN_LINE_D_30");
                    Session_1_method(strrobotid, atcNO);
                }
            }
            else if (atcNO == "24")
            {
                taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 10);
                mission_temp_list.Add("TEMP_");
                mission_temp_list.Add("ATC_24_wh");
                mission_temp_list.Add("GO_7_14");
                mission_temp_list.Add("ABN_LINE_C_24");
                dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_24_wh", "ABN_LINE_C_24");
                DP_currentmission(strrobotid, "ABN_LINE_C_24");
                Session_1_method(strrobotid, atcNO);
            }
            else if (atcNO == "16")
            {
                taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 7);
                mission_temp_list.Add("TEMP_");
                mission_temp_list.Add("ATC_16_wh");
                mission_temp_list.Add("GO_4_8");
                mission_temp_list.Add("ABN_LINE_A_16");
                dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_16_wh", "ABN_LINE_A_16");
                DP_currentmission(strrobotid, "ABN_LINE_A_16");
                Session_1_method(strrobotid, atcNO);
            }
            else if (atcNO == "29")
            {
                taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 7);
                mission_temp_list.Add("TEMP_");
                mission_temp_list.Add("ATC_29_wh");
                mission_temp_list.Add("GO_4_6");
                mission_temp_list.Add("ABN_LINE_D_29");
                dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_29_wh", "ABN_LINE_D_29");
                DP_currentmission(strrobotid, "ABN_LINE_D_29");
                Session_1_method(strrobotid, atcNO);
            }
            else if (atcNO == "22")
            {
                taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 13);
                mission_temp_list.Add("TEMP_");
                mission_temp_list.Add("ATC_22_wh");
                mission_temp_list.Add("GO_10_14"); // YET
                mission_temp_list.Add("ABN_LINE_C_22");
                dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_22_wh", "ABN_LINE_C_22");
                DP_currentmission(strrobotid, "ABN_LINE_C_22");
                Session_1_method(strrobotid, atcNO); // DONE
            }
            else if (atcNO == "21")
            {
                taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 13);
                mission_temp_list.Add("TEMP_");
                mission_temp_list.Add("ATC_21_wh");
                mission_temp_list.Add("GO_10_14");
                mission_temp_list.Add("ABN_LINE_C_21");
                dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_21_wh", "ABN_LINE_C_21");
                DP_currentmission(strrobotid, "ABN_LINE_C_21");
                Session_1_method(strrobotid, atcNO);
            }
            else if (atcNO == "19")
            {
                taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 13);
                mission_temp_list.Add("TEMP_");
                mission_temp_list.Add("ATC_19_wh");
                mission_temp_list.Add("GO_10_12");
                mission_temp_list.Add("ABN_LINE_B_19");
                dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_19_wh", "ABN_LINE_B_19");
                DP_currentmission(strrobotid, "ABN_LINE_B_19");
                Session_1_method(strrobotid, atcNO);
            }
            else if (atcNO == "18")
            {
                taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 13);
                mission_temp_list.Add("TEMP_");
                mission_temp_list.Add("ATC_18_wh");
                mission_temp_list.Add("GO_10_12");
                mission_temp_list.Add("ABN_LINE_B_18");
                dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_18_wh", "ABN_LINE_B_18");
                DP_currentmission(strrobotid, "ABN_LINE_B_18");
                Session_1_method(strrobotid, atcNO);
            }
            else if (atcNO == "17")
            {
                taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 10);
                mission_temp_list.Add("TEMP_");
                mission_temp_list.Add("ATC_17_wh");
                mission_temp_list.Add("GO_7_12");
                mission_temp_list.Add("ABN_LINE_B_17");
                dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_17_wh", "ABN_LINE_B_17");
                DP_currentmission(strrobotid, "ABN_LINE_B_17");
                Session_1_method(strrobotid, atcNO);
            }
            else if (atcNO == "14")
            {
                taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 10);
                mission_temp_list.Add("TEMP_");
                mission_temp_list.Add("ATC_14_wh");
                mission_temp_list.Add("GO_7_8");
                mission_temp_list.Add("ABN_LINE_A_14");
                dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_14_wh", "ABN_LINE_A_14");
                DP_currentmission(strrobotid, "ABN_LINE_A_14");
                Session_1_method(strrobotid, atcNO);
            }
            else if (atcNO == "13")
            {
                taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 10);
                mission_temp_list.Add("TEMP_");
                mission_temp_list.Add("ATC_13_wh");
                mission_temp_list.Add("GO_7_8");
                mission_temp_list.Add("ABN_LINE_A_13");
                dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_13_wh", "ABN_LINE_A_13");
                DP_currentmission(strrobotid, "ABN_LINE_A_13");
                Session_1_method(strrobotid, atcNO);
            }
            else if (atcNO == "12")
            {
                taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 10);
                mission_temp_list.Add("TEMP_");
                mission_temp_list.Add("ATC_12_wh");
                mission_temp_list.Add("GO_7_8");
                mission_temp_list.Add("ABN_LINE_A_12");
                dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_12_wh", "ABN_LINE_A_12");
                DP_currentmission(strrobotid, "ABN_LINE_A_12");
                Session_1_method(strrobotid, atcNO);
            }
            else if (atcNO == "11")
            {
                taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 10);
                mission_temp_list.Add("TEMP_");
                mission_temp_list.Add("ATC_11_wh");
                mission_temp_list.Add("GO_7_8");
                mission_temp_list.Add("ABN_LINE_A_11");
                dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_11_wh", "ABN_LINE_A_11");
                DP_currentmission(strrobotid, "ABN_LINE_A_11");

                Session_1_method(strrobotid, atcNO);
            }
            else if (atcNO == "27")
            {
                taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 10);
                mission_temp_list.Add("TEMP_");
                mission_temp_list.Add("ATC_27_wh");
                mission_temp_list.Add("GO_7_6");
                mission_temp_list.Add("ABN_LINE_D_27");
                dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_27_wh", "ABN_LINE_D_27");
                DP_currentmission(strrobotid, "ABN_LINE_D_27");
                Session_1_method(strrobotid, atcNO);
            }
            else if (atcNO == "26")
            {
                taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 10);
                mission_temp_list.Add("TEMP_");
                mission_temp_list.Add("ATC_26_wh");
                mission_temp_list.Add("GO_7_6");
                mission_temp_list.Add("ABN_LINE_D_26");
                dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_26_wh", "ABN_LINE_D_26");
                DP_currentmission(strrobotid, "ABN_LINE_D_26");
                Session_1_method(strrobotid, atcNO);
            }
            else if (atcNO == "25")
            {
                taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 10);
                mission_temp_list.Add("TEMP_");
                mission_temp_list.Add("ATC_25_wh");
                mission_temp_list.Add("GO_7_6");
                mission_temp_list.Add("ABN_LINE_D_25");
                dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_25_wh", "ABN_LINE_D_25");
                DP_currentmission(strrobotid, "ABN_LINE_D_25");
                Session_1_method(strrobotid, atcNO);
            }
            else if (atcNO == "23")
            {
                taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 10);
                mission_temp_list.Add("TEMP_");
                mission_temp_list.Add("ATC_23_wh");
                mission_temp_list.Add("GO_7_14");
                mission_temp_list.Add("ABN_LINE_C_23");
                dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_23_wh", "ABN_LINE_C_23");
                DP_currentmission(strrobotid, "ABN_LINE_C_23");
                Session_1_method(strrobotid, atcNO);
            }
            else if (atcNO == "20")
            {
                taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 10);
                mission_temp_list.Add("TEMP_");
                mission_temp_list.Add("ATC_20_wh");
                mission_temp_list.Add("GO_7_12");
                mission_temp_list.Add("ABN_LINE_B_20");
                dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_20_wh", "ABN_LINE_B_20");
                DP_currentmission(strrobotid, "ABN_LINE_B_20");
                Session_1_method(strrobotid, atcNO);
            }
            else if (atcNO == "15")
            {
                taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 7);
                mission_temp_list.Add("TEMP_");
                mission_temp_list.Add("ATC_15_wh");
                mission_temp_list.Add("GO_4_8");
                mission_temp_list.Add("ABN_LINE_A_15");
                dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_15_wh", "ABN_LINE_A_15");
                DP_currentmission(strrobotid, "ABN_LINE_A_15");
                Session_1_method(strrobotid, atcNO);
            }
            else if (atcNO == "28")
            {
                taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 7);
                mission_temp_list.Add("TEMP_");
                mission_temp_list.Add("ATC_28_wh");
                mission_temp_list.Add("GO_4_6");
                mission_temp_list.Add("ABN_LINE_D_28");
                dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_28_wh", "ABN_LINE_D_28");
                DP_currentmission(strrobotid, "ABN_LINE_D_28");
                Session_1_method(strrobotid, atcNO);
            }
            else if (atcNO == "10")
            {
                taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 7);
                mission_temp_list.Add("TEMP_");
                mission_temp_list.Add("ATC_10_wh");
                mission_temp_list.Add("GO_4_12");
                mission_temp_list.Add("ABN_LINE_C_10");
                dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_10_wh", "ABN_LINE_C_10");
                DP_currentmission(strrobotid, "ABN_LINE_C_10");
                Session_1_method(strrobotid, atcNO);
            }
            else if (atcNO == "7")
            {
                taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 7);
                mission_temp_list.Add("TEMP_");
                mission_temp_list.Add("ATC_7_wh");
                mission_temp_list.Add("GO_4_14");
                mission_temp_list.Add("ABN_LINE_C_7");
                dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_7_wh", "ABN_LINE_C_7");
                DP_currentmission(strrobotid, "ABN_LINE_C_7");
                Session_1_method(strrobotid, atcNO);
            }
            else if (atcNO == "3_1")
            {
                if (requiredID == "192.168.102.80") //d3
                {
                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 7);
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ATC_3_1_wh");
                    mission_temp_list.Add("GO_4_3");
                    mission_temp_list.Add("ABN_LINE_D_3");
                    dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_3_1_wh", "ABN_LINE_D_3");
                    DP_currentmission(strrobotid, "ABN_LINE_D_3");

                    Session_1_method(strrobotid, atcNO);
                }
                else if (requiredID == "192.168.102.20") //b8
                {
                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 7);
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ATC_3_1_wh");
                    mission_temp_list.Add("GO_4_8");
                    mission_temp_list.Add("ABN_LINE_B_3");
                    dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_3_1_wh", "ABN_LINE_B_3");
                    DP_currentmission(strrobotid, "ABN_LINE_B_8");

                    Session_1_method(strrobotid, atcNO);
                }
            }
            else if (atcNO == "3_2")
            {
                if (requiredID == "192.168.102.80") //d3
                {
                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 7);
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ATC_3_2_wh");
                    mission_temp_list.Add("GO_4_3");
                    mission_temp_list.Add("ABN_LINE_D_3");
                    dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_3_2_wh", "ABN_LINE_D_3");
                    DP_currentmission(strrobotid, "ABN_LINE_D_3");

                    Session_1_method(strrobotid, atcNO);
                }
                else if (requiredID == "192.168.102.20") //b8
                {
                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 7);
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ATC_3_2_wh");
                    mission_temp_list.Add("GO_4_8");
                    mission_temp_list.Add("ABN_LINE_B_3");
                    dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_3_2_wh", "ABN_LINE_B_3");
                    DP_currentmission(strrobotid, "ABN_LINE_B_8");

                    Session_1_method(strrobotid, atcNO);
                }
            }
            else if (atcNO == "8")
            {
                taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 7);
                mission_temp_list.Add("TEMP_");
                mission_temp_list.Add("ATC_8_wh");
                mission_temp_list.Add("GO_4_14");
                mission_temp_list.Add("ABN_LINE_C_8");
                dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_8_wh", "ABN_LINE_C_8");
                DP_currentmission(strrobotid, "ABN_LINE_C_8");

                Session_1_method(strrobotid, atcNO);
            }
            else if (atcNO == "4_1")
            {
                if (requiredID == "192.168.102.132") // B
                {
                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 4);
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ATC_4_1_wh");
                    mission_temp_list.Add("GO_1_12");
                    mission_temp_list.Add("ABN_LINE_B_4");
                    dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_4_1_wh", "ABN_LINE_B_4");
                    DP_currentmission(strrobotid, "ABN_LINE_B_4");

                    Session_1_method(strrobotid, atcNO);
                }
                else if (requiredID == "192.168.102.72") //D
                {
                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 4);
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ATC_4_1_wh");
                    mission_temp_list.Add("GO_1_6");
                    mission_temp_list.Add("ABN_LINE_D_4");
                    dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_4_1_wh", "ABN_LINE_D_4");
                    DP_currentmission(strrobotid, "ABN_LINE_D_4");

                    Session_1_method(strrobotid, atcNO);
                }
            }
            else if (atcNO == "4_2")
            {
                if (requiredID == "192.168.102.132") // B
                {
                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 4);
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ATC_4_2_wh");
                    mission_temp_list.Add("GO_1_12");
                    mission_temp_list.Add("ABN_LINE_B_4");
                    dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_4_2_wh", "ABN_LINE_B_4");
                    DP_currentmission(strrobotid, "ABN B 4_2");

                    Session_1_method(strrobotid, atcNO);
                }
                else if (requiredID == "192.168.102.72") //D
                {
                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 4);
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ATC_4_2_wh");
                    mission_temp_list.Add("GO_1_6");
                    mission_temp_list.Add("ABN_LINE_D_4");
                    dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_4_2_wh", "ABN_LINE_D_4");
                    DP_currentmission(strrobotid, "ABN D 4_2");

                    Session_1_method(strrobotid, atcNO);
                }
            }

            else if (atcNO == "9")
            {
                taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 4);
                mission_temp_list.Add("TEMP_");
                mission_temp_list.Add("ATC_9_wh");
                mission_temp_list.Add("GO_1_12");
                mission_temp_list.Add("ABN_LINE_C_9");
                dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_9_wh", "ABN_LINE_C_9");
                DP_currentmission(strrobotid, "ABN_LINE_C_9");

                Session_1_method(strrobotid, atcNO);
            }
            else if (atcNO == "6_1")
            {
                if (requiredID == "192.168.102.39") // LINE B
                {
                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 4);
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ATC_6_1_wh");
                    mission_temp_list.Add("GO_1_8");
                    mission_temp_list.Add("ABN_LINE_B_6");
                    dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_6_1_wh", "ABN_LINE_B_6");
                    DP_currentmission(strrobotid, "ABN_LINE_B_6");

                    Session_1_method(strrobotid, atcNO);
                }
                else if (requiredID == "192.168.102.79") // LINE D
                {
                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 4);
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ATC_6_1_wh");
                    mission_temp_list.Add("GO_1_3");
                    mission_temp_list.Add("ABN_LINE_D_6");
                    dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_6_1_wh", "ABN_LINE_D_6");
                    DP_currentmission(strrobotid, "ABN_LINE_D_6");

                    Session_1_method(strrobotid, atcNO);
                }

            }
            else if (atcNO == "6_2")
            {
                if (requiredID == "192.168.102.39") // LINE B
                {
                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 4);
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ATC_6_2_wh");
                    mission_temp_list.Add("GO_1_8");
                    mission_temp_list.Add("ABN_LINE_B_6");
                    dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_6_2_wh", "ABN_LINE_B_6");
                    DP_currentmission(strrobotid, "ABN_LINE_B_6");

                    Session_1_method(strrobotid, atcNO);
                }
                else if (requiredID == "192.168.102.79") // LINE D
                {
                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 4);
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ATC_6_2_wh");
                    mission_temp_list.Add("GO_1_3");
                    mission_temp_list.Add("ABN_LINE_D_6");
                    dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_6_2_wh", "ABN_LINE_D_6");
                    DP_currentmission(strrobotid, "ABN_LINE_D_6");

                    Session_1_method(strrobotid, atcNO);
                }

            }
            else if (atcNO == "2")
            {
                taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 4);
                mission_temp_list.Add("TEMP_");
                mission_temp_list.Add("ATC_2_wh");
                mission_temp_list.Add("GO_1_6");
                mission_temp_list.Add("ABN_LINE_A_2");
                dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_2_wh", "ABN_LINE_A_2");
                DP_currentmission(strrobotid, "ABN_LINE_A_2");

                Session_1_method(strrobotid, atcNO);
            }
            else if (atcNO == "5_1")
            {
                if (requiredID == "192.168.102.36")
                {
                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 4);
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ATC_5_1_wh");
                    mission_temp_list.Add("GO_1_12");
                    mission_temp_list.Add("ABN_LINE_B_5");
                    dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_5_1_wh", "ABN_LINE_B_5");
                    DP_currentmission(strrobotid, "ABN_LINE_B_5");

                    Session_1_method(strrobotid, atcNO);
                }
                else if (requiredID == "192.168.102.76")
                {
                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 4);
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ATC_5_1_wh");
                    mission_temp_list.Add("GO_1_6");
                    mission_temp_list.Add("ABN_LINE_D_5");
                    dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_5_1_wh", "ABN_LINE_D_5");
                    DP_currentmission(strrobotid, "ABN_LINE_D_5");

                    Session_1_method(strrobotid, atcNO);
                }

            }
            else if (atcNO == "5_2")
            {
                if (requiredID == "192.168.102.36")
                {
                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 4);
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ATC_5_2_wh");
                    mission_temp_list.Add("GO_1_12");
                    mission_temp_list.Add("ABN_LINE_B_5");
                    dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_5_2_wh", "ABN_LINE_B_5");
                    DP_currentmission(strrobotid, "ABN_LINE_B_5");

                    Session_1_method(strrobotid, atcNO);
                }
                else if (requiredID == "192.168.102.76")
                {
                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 4);
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ATC_5_2_wh");
                    mission_temp_list.Add("GO_1_6");
                    mission_temp_list.Add("ABN_LINE_D_5");
                    dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_5_2_wh", "ABN_LINE_D_5");
                    DP_currentmission(strrobotid, "ABN_LINE_D_5");

                    Session_1_method(strrobotid, atcNO);
                }
            }

            else if (atcNO == "1")
            {
                taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 4);
                mission_temp_list.Add("TEMP_");
                mission_temp_list.Add("ATC_1_wh");
                mission_temp_list.Add("GO_1_8");
                mission_temp_list.Add("ABN_LINE_A_1");
                dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_1_wh", "ABN_LINE_A_1");
                DP_currentmission(strrobotid, "ABN_LINE_A_1");

                Session_1_method(strrobotid, atcNO);
            }

        }

        private string swap_robot(string strrobotid)
        {
            if (strrobotid == "R_006")
                return "R_007";
            else
                return "R_006";
        }

        //Session 2 start(포장라인쪽)

        public void Session_2_taskStart(string requiredID, string startID, string mission, string atcNO, string strrobotid)
        {
            addScenarioList(atcNO); //수정한 부분
            if (true)
            {
                if (atcNO == "32_1")
                {
                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 3);
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ABN_LINE_D_32_1"); // ABN_LINE D ATC 32_1
                    mission_temp_list.Add("GO_2_9");
                    mission_temp_list.Add("ATC_32_1_T");

                    dbBridge.onDBUpdate_Frommission(strrobotid, "ABN_LINE_D_32_1", "ATC_32_1_T");
                    DP_currentmission(strrobotid, "ATC_32_1_T");

                    mission_temp = new string[mission_temp_list.Count()];
                    mission_temp = mission_temp_list.ToArray();
                    int cnt = mission_temp.Count();

                    taskoperationform.taskSave(atcNO, taskoperationform.ConvertString(mission_temp), strrobotid);

                    mission_temp_list.Clear();
                    dbBridge.onDBDelete_Missionlist("TEMP_");

                    CrashCheckRobot_list.Add(strrobotid);

                }
                else if (atcNO == "32_2")
                {
                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 3);
                    mission_temp_list.Add("TEMP_");

                    mission_temp_list.Add("ABN_LINE_D_32_2"); // ABN_LINE D ATC 32_1
                    mission_temp_list.Add("GO_2_9");
                    mission_temp_list.Add("ATC_32_2_T");

                    dbBridge.onDBUpdate_Frommission(strrobotid, "ABN_LINE_D_32_2", "ATC_32_2_T");
                    DP_currentmission(strrobotid, "ATC_32_2_T");



                    mission_temp = new string[mission_temp_list.Count()];
                    mission_temp = mission_temp_list.ToArray();
                    int cnt = mission_temp.Count();

                    taskoperationform.taskSave(atcNO, taskoperationform.ConvertString(mission_temp), strrobotid);

                    mission_temp_list.Clear();
                    dbBridge.onDBDelete_Missionlist("TEMP_");


                    CrashCheckRobot_list.Add(strrobotid);
                }
                else if (atcNO == "32_3")
                {

                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 11);

                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("EBN_LINE_A_32_3");// 32 - 3 docking - ebn100c line - 1
                    mission_temp_list.Add("GO_14_9");
                    mission_temp_list.Add("ATC_32_3_T"); // atc 32_3 docking

                    dbBridge.onDBUpdate_Frommission(strrobotid, "EBN_LINE_A_32_3", "ATC_32_3_T");
                    DP_currentmission(strrobotid, "ATC_32_3_T");

                    mission_temp = new string[mission_temp_list.Count()];
                    mission_temp = mission_temp_list.ToArray();
                    int cnt = mission_temp.Count();

                    taskoperationform.taskSave(atcNO, taskoperationform.ConvertString(mission_temp), strrobotid);

                    mission_temp_list.Clear();
                    dbBridge.onDBDelete_Missionlist("TEMP_");


                    CrashCheckRobot_list.Add(strrobotid);
                }
                else if (atcNO == "32_4")
                {

                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 8);
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("EBN_LINE_B_32_4");// 32 - 3 docking - ebn100c line - 1
                    mission_temp_list.Add("GO_11_9");
                    mission_temp_list.Add("ATC_32_4_T"); // atc 32_3 docking

                    dbBridge.onDBUpdate_Frommission(strrobotid, "EBN_LINE_B_32_4", "ATC_32_4_T");
                    DP_currentmission(strrobotid, "ATC_32_4_T");

                    mission_temp = new string[mission_temp_list.Count()];
                    mission_temp = mission_temp_list.ToArray();
                    int cnt = mission_temp.Count();

                    taskoperationform.taskSave(atcNO, taskoperationform.ConvertString(mission_temp), strrobotid);

                    mission_temp_list.Clear();
                    dbBridge.onDBDelete_Missionlist("TEMP_");


                    CrashCheckRobot_list.Add(strrobotid);

                }
                else if (atcNO == "32_5")
                {

                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 12);
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ABN_LINE_C_32_5"); // 32-5 docking - ABN100C #3 자동 LINE
                    mission_temp_list.Add("GO_12_9");
                    mission_temp_list.Add("ATC_32_5_T"); // ATC 32_5 docking


                    dbBridge.onDBUpdate_Frommission(strrobotid, "ABN_LINE_C_32_5", "ATC_32_5_T");
                    DP_currentmission(strrobotid, "ATC_32_5_T");

                    mission_temp = new string[mission_temp_list.Count()];
                    mission_temp = mission_temp_list.ToArray();
                    int cnt = mission_temp.Count();

                    taskoperationform.taskSave(atcNO, taskoperationform.ConvertString(mission_temp), strrobotid);

                    mission_temp_list.Clear();
                    dbBridge.onDBDelete_Missionlist("TEMP_");


                    CrashCheckRobot_list.Add(strrobotid);
                }
                else if (atcNO == "32_6")
                {

                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 12);
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ABN_LINE_C_32_6"); // 32-6 docking - ABN100C #3 자동 LINE
                    mission_temp_list.Add("GO_12_9");
                    mission_temp_list.Add("ATC_32_6_T");

                    dbBridge.onDBUpdate_Frommission(strrobotid, "ABN_LINE_C_32_6", "ATC_32_6_T");
                    DP_currentmission(strrobotid, "ATC_32_6_T");

                    mission_temp = new string[mission_temp_list.Count()];
                    mission_temp = mission_temp_list.ToArray();
                    int cnt = mission_temp.Count();

                    taskoperationform.taskSave(atcNO, taskoperationform.ConvertString(mission_temp), strrobotid);

                    mission_temp_list.Clear();
                    dbBridge.onDBDelete_Missionlist("TEMP_");


                    CrashCheckRobot_list.Add(strrobotid);
                }
                else if (atcNO == "33")
                {
                    //mission_temp_list.Add("GO_12_9");
                    if (requiredID == "192.168.102.81")
                    {
                        //ABN #4

                        taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 9);
                        mission_temp_list.Add("TEMP_");
                        mission_temp_list.Add("ATC_33_T");
                        mission_temp_list.Add("GO_6_3");
                        mission_temp_list.Add("ABN_LINE_D_33");

                        dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_33_T", "ABN_LINE_D_33");
                        DP_currentmission(strrobotid, "ABN_LINE_D_33");

                        mission_temp = new string[mission_temp_list.Count()];
                        mission_temp = mission_temp_list.ToArray();
                        int cnt = mission_temp.Count();

                        taskoperationform.taskSave(atcNO, taskoperationform.ConvertString(mission_temp), strrobotid);

                        mission_temp_list.Clear();
                        dbBridge.onDBDelete_Missionlist("TEMP_");


                        CrashCheckRobot_list.Add(strrobotid);

                    }
                    //else if (requiredID == "192.168.102.61")
                    //{
                    //    //ABN #3

                    //    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 9);
                    //    mission_temp_list.Add("TEMP_");
                    //    mission_temp_list.Add("ATC_33_T");
                    //    mission_temp_list.Add("GO_6_12");
                    //    mission_temp_list.Add("ABN_LINE_C_33");
                    //    //mission_temp_list.Add("GO_11_12");
                    //    //mission_temp_list.Add("MID20191117144706"); // session 2 wait pos _ R_007
                    //    mission_temp = new string[mission_temp_list.Count()];
                    //    mission_temp = mission_temp_list.ToArray();
                    //    int cnt = mission_temp.Count();

                    //    taskoperationform.taskSave(atcNO, taskoperationform.ConvertString(mission_temp), strrobotid);

                    //    mission_temp_list.Clear();
                    //    dbBridge.onDBDelete_Missionlist("TEMP_");


                    //    CrashCheckRobot_list.Add(strrobotid);
                    //    displayFirstform();
                    //}
                    else if (requiredID == "192.168.102.21")  // 기존 1라인의 35와 3라인의 33을 교체하였습니다. 현재: 1라인 33, 3자인 35
                    {
                        //ABN #3

                        taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 9);
                        mission_temp_list.Add("TEMP_");
                        mission_temp_list.Add("ATC_33_T");
                        //mission_temp_list.Add("GO_6_12");
                        mission_temp_list.Add("ABN_LINE_A_33");
                        dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_33_T", "ABN_LINE_A_33");
                        DP_currentmission(strrobotid, "ABN_LINE_A_33");

                        mission_temp = new string[mission_temp_list.Count()];
                        mission_temp = mission_temp_list.ToArray();
                        int cnt = mission_temp.Count();

                        taskoperationform.taskSave(atcNO, taskoperationform.ConvertString(mission_temp), strrobotid);

                        mission_temp_list.Clear();
                        dbBridge.onDBDelete_Missionlist("TEMP_");


                        CrashCheckRobot_list.Add(strrobotid);
                    }
                    //수정한 부분( atcNo = 33  ID 91, 92 추가)
                    else if (requiredID == "192.168.102.91")
                    {
                        //ABN100C #1 LINE A ATC NUMBER : 35
                        //mission_temp_list.Add("GO_12_15");

                        taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 15);
                        mission_temp_list.Add("TEMP_");
                        mission_temp_list.Add("ATC_33_T");
                        mission_temp_list.Add("GO_12_11");
                        mission_temp_list.Add("EBN_LINE_A_33");

                        dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_33_T", "EBN_LINE_A_33");
                        DP_currentmission(strrobotid, "EBN_LINE_A_33");
                        mission_temp = new string[mission_temp_list.Count()];
                        mission_temp = mission_temp_list.ToArray();
                        int cnt = mission_temp.Count();

                        taskoperationform.taskSave(atcNO, taskoperationform.ConvertString(mission_temp), strrobotid);

                        mission_temp_list.Clear();
                        dbBridge.onDBDelete_Missionlist("TEMP_");


                        CrashCheckRobot_list.Add(strrobotid);

                    }
                    else if (requiredID == "192.168.102.92")
                    {

                        taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 15);
                        mission_temp_list.Add("TEMP_");
                        mission_temp_list.Add("ATC_33_T");
                        mission_temp_list.Add("GO_12_8");
                        mission_temp_list.Add("EBN_LINE_B_33");

                        dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_33_T", "EBN_LINE_B_33");
                        DP_currentmission(strrobotid, "EBN_LINE_B_33");

                        mission_temp = new string[mission_temp_list.Count()];
                        mission_temp = mission_temp_list.ToArray();
                        int cnt = mission_temp.Count();

                        taskoperationform.taskSave(atcNO, taskoperationform.ConvertString(mission_temp), strrobotid);

                        mission_temp_list.Clear();
                        dbBridge.onDBDelete_Missionlist("TEMP_");


                        CrashCheckRobot_list.Add(strrobotid);
                    }
                }
                else if (atcNO == "34_1")
                {
                    //mission_temp_list.Add("GO_12_6"); // 32-6 docking - ABN100C #3 자동 LINE

                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 6);
                    mission_temp_list.Add("TEMP_");

                    mission_temp_list.Add("ABN_LINE_A_34_1");
                    mission_temp_list.Add("GO_5_15");
                    mission_temp_list.Add("ATC_34_1_T");

                    dbBridge.onDBUpdate_Frommission(strrobotid, "ABN_LINE_A_34_1", "ATC_34_1_T");
                    DP_currentmission(strrobotid, "ATC_34_1_T");

                    mission_temp = new string[mission_temp_list.Count()];
                    mission_temp = mission_temp_list.ToArray();
                    int cnt = mission_temp.Count();

                    taskoperationform.taskSave(atcNO, taskoperationform.ConvertString(mission_temp), strrobotid);

                    mission_temp_list.Clear();
                    dbBridge.onDBDelete_Missionlist("TEMP_");


                    CrashCheckRobot_list.Add(strrobotid);
                }
                else if (atcNO == "34_2")
                {
                    //mission_temp_list.Add("GO_12_6"); // 32-6 docking - ABN100C #3 자동 LINE

                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 6);
                    mission_temp_list.Add("TEMP_");

                    mission_temp_list.Add("ABN_LINE_A_34_2");
                    mission_temp_list.Add("GO_5_15");
                    mission_temp_list.Add("ATC_34_2_T");

                    dbBridge.onDBUpdate_Frommission(strrobotid, "ABN_LINE_A_34_2", "ATC_34_2_T");
                    DP_currentmission(strrobotid, "ATC_34_2_T");


                    mission_temp = new string[mission_temp_list.Count()];
                    mission_temp = mission_temp_list.ToArray();
                    int cnt = mission_temp.Count();

                    taskoperationform.taskSave(atcNO, taskoperationform.ConvertString(mission_temp), strrobotid);

                    mission_temp_list.Clear();
                    dbBridge.onDBDelete_Missionlist("TEMP_");


                    CrashCheckRobot_list.Add(strrobotid);
                }
                else if (atcNO == "34_3")
                {
                    //mission_temp_list.Add("GO_12_9"); // 32-6 docking - ABN100C #3 자동 LINE

                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 8);
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ABN_LINE_B_34_3");
                    mission_temp_list.Add("GO_9_15");
                    mission_temp_list.Add("ATC_34_3_T");

                    dbBridge.onDBUpdate_Frommission(strrobotid, "ABN_LINE_B_34_3", "ATC_34_3_T");
                    DP_currentmission(strrobotid, "ATC_34_3_T");

                    mission_temp = new string[mission_temp_list.Count()];
                    mission_temp = mission_temp_list.ToArray();
                    int cnt = mission_temp.Count();

                    taskoperationform.taskSave(atcNO, taskoperationform.ConvertString(mission_temp), strrobotid);

                    mission_temp_list.Clear();
                    dbBridge.onDBDelete_Missionlist("TEMP_");


                    CrashCheckRobot_list.Add(strrobotid);
                }
                else if (atcNO == "34_4")
                {
                    //mission_temp_list.Add("GO_12_9"); // 32-6 docking - ABN100C #3 자동 LINE

                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 8);
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("ABN_LINE_B_34_4");
                    mission_temp_list.Add("GO_9_15");
                    mission_temp_list.Add("ATC_34_4_T");

                    dbBridge.onDBUpdate_Frommission(strrobotid, "ABN_LINE_B_34_4", "ATC_34_4_T");
                    DP_currentmission(strrobotid, "ATC_34_4_T");

                    mission_temp = new string[mission_temp_list.Count()];
                    mission_temp = mission_temp_list.ToArray();
                    int cnt = mission_temp.Count();

                    taskoperationform.taskSave(atcNO, taskoperationform.ConvertString(mission_temp), strrobotid);

                    mission_temp_list.Clear();
                    dbBridge.onDBDelete_Missionlist("TEMP_");


                    CrashCheckRobot_list.Add(strrobotid);
                }
                else if (atcNO == "34_5")
                {
                    //mission_temp_list.Add("GO_12_11"); // 32-6 docking - ABN100C #3 자동 LINE

                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 11);
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("EBN_LINE_A_34_5");
                    mission_temp_list.Add("GO_14_15");
                    mission_temp_list.Add("ATC_34_5_T");

                    dbBridge.onDBUpdate_Frommission(strrobotid, "EBN_LINE_A_34_5", "ATC_34_5_T");
                    DP_currentmission(strrobotid, "ATC_34_5_T");

                    mission_temp = new string[mission_temp_list.Count()];
                    mission_temp = mission_temp_list.ToArray();
                    int cnt = mission_temp.Count();

                    taskoperationform.taskSave(atcNO, taskoperationform.ConvertString(mission_temp), strrobotid);

                    mission_temp_list.Clear();
                    dbBridge.onDBDelete_Missionlist("TEMP_");


                    CrashCheckRobot_list.Add(strrobotid);
                }
                else if (atcNO == "34_6")
                {
                    //mission_temp_list.Add("GO_12_8"); // 32-6 docking - ABN100C #3 자동 LINE

                    taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 8);
                    mission_temp_list.Add("TEMP_");
                    mission_temp_list.Add("EBN_LINE_B_34_6");
                    mission_temp_list.Add("GO_11_15");
                    mission_temp_list.Add("ATC_34_6_T");

                    dbBridge.onDBUpdate_Frommission(strrobotid, "EBN_LINE_B_34_6", "ATC_34_6_T");
                    DP_currentmission(strrobotid, "ATC_34_6_T");

                    mission_temp = new string[mission_temp_list.Count()];
                    mission_temp = mission_temp_list.ToArray();
                    int cnt = mission_temp.Count();

                    taskoperationform.taskSave(atcNO, taskoperationform.ConvertString(mission_temp), strrobotid);

                    mission_temp_list.Clear();
                    dbBridge.onDBDelete_Missionlist("TEMP_");


                    CrashCheckRobot_list.Add(strrobotid);
                }
                else if (atcNO == "35")
                {
                    //mission_temp_list.Add("MID20191112151959"); // node 15 mission id
                    if (requiredID == "192.168.102.91")
                    {
                        //ABN100C #1 LINE A ATC NUMBER : 35
                        //mission_temp_list.Add("GO_12_15");

                        taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 15);
                        mission_temp_list.Add("TEMP_");
                        mission_temp_list.Add("ATC_35_T");
                        mission_temp_list.Add("GO_12_11");
                        mission_temp_list.Add("EBN_LINE_A_35");

                        dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_35_T", "EBN_LINE_A_35");
                        DP_currentmission(strrobotid, "EBN_LINE_A_35");

                        mission_temp = new string[mission_temp_list.Count()];
                        mission_temp = mission_temp_list.ToArray();
                        int cnt = mission_temp.Count();

                        taskoperationform.taskSave(atcNO, taskoperationform.ConvertString(mission_temp), strrobotid);

                        mission_temp_list.Clear();
                        dbBridge.onDBDelete_Missionlist("TEMP_");


                        CrashCheckRobot_list.Add(strrobotid);

                    }
                    else if (requiredID == "192.168.102.92")
                    {
                        //ABN100C #2 LINE B ATC NUMBER : 35

                        taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 15);
                        mission_temp_list.Add("TEMP_");
                        mission_temp_list.Add("ATC_35_T");
                        mission_temp_list.Add("GO_12_8");
                        mission_temp_list.Add("EBN_LINE_B_35");

                        dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_35_T", "EBN_LINE_B_35");
                        DP_currentmission(strrobotid, "EBN_LINE_B_35");

                        mission_temp = new string[mission_temp_list.Count()];
                        mission_temp = mission_temp_list.ToArray();
                        int cnt = mission_temp.Count();

                        taskoperationform.taskSave(atcNO, taskoperationform.ConvertString(mission_temp), strrobotid);

                        mission_temp_list.Clear();
                        dbBridge.onDBDelete_Missionlist("TEMP_");


                        CrashCheckRobot_list.Add(strrobotid);
                    }
                    else if (requiredID == "192.168.102.41")
                    {
                        //ABN100C #2 LINE B ATC NUMBER : 35

                        taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 15);
                        mission_temp_list.Add("TEMP_");
                        mission_temp_list.Add("ATC_35_T");
                        mission_temp_list.Add("GO_12_8");
                        mission_temp_list.Add("ABN_LINE_B_35");


                        dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_35_T", "ABN_LINE_B_35");
                        DP_currentmission(strrobotid, "ABN_LINE_B_35");

                        mission_temp = new string[mission_temp_list.Count()];
                        mission_temp = mission_temp_list.ToArray();
                        int cnt = mission_temp.Count();

                        taskoperationform.taskSave(atcNO, taskoperationform.ConvertString(mission_temp), strrobotid);

                        mission_temp_list.Clear();
                        dbBridge.onDBDelete_Missionlist("TEMP_");


                        CrashCheckRobot_list.Add(strrobotid);
                    }
                    else if (requiredID == "192.168.102.61")  //3라인 33 ->35로 변경
                    {
                        //ABN100C #2 LINE B ATC NUMBER : 35

                        taskoperationform.dijkstra_2(firstform.findLocation(strrobotid), 15);
                        mission_temp_list.Add("TEMP_");
                        mission_temp_list.Add("ATC_35_T");
                        //mission_temp_list.Add("GO_12_8");
                        mission_temp_list.Add("ABN_LINE_C_35");


                        dbBridge.onDBUpdate_Frommission(strrobotid, "ATC_35_T", "ABN_LINE_C_35");
                        DP_currentmission(strrobotid, "ABN_LINE_C_35");

                        mission_temp = new string[mission_temp_list.Count()];
                        mission_temp = mission_temp_list.ToArray();
                        int cnt = mission_temp.Count();

                        taskoperationform.taskSave(atcNO, taskoperationform.ConvertString(mission_temp), strrobotid);

                        mission_temp_list.Clear();
                        dbBridge.onDBDelete_Missionlist("TEMP_");


                        CrashCheckRobot_list.Add(strrobotid);
                    }
                }
                //}
                //foreach (KeyValuePair<string, string> docking in Data.Instance.docking_warehouse)
                //{
                //    if(docking.Value.Contains(atcNO))
                //    {
                //        mission_temp_list.Add(docking.Key);
                //    }
                //}
                //foreach (KeyValuePair<string,warehouse_inout> inout in Data.Instance.callByplc)
                //{
                //    if(inout.Key.Contains(atcNO))
                //    {
                //        line_IN = Regex.Replace(inout.Value.in_, @"\D", "");
                //        line_OUT = Regex.Replace(inout.Value.out_, @"\D", "");
                //        line_in_number = int.Parse(line_IN);
                //        line_out_number = int.Parse(line_OUT);
                //        foreach (KeyValuePair<string,warehouse_inout> warehouse in Data.Instance.warehouse)
                //        {
                //            if(warehouse.Key.Contains(inout.Key))
                //            {
                //                warehouse_in = Regex.Replace(warehouse.Value.in_,@"\D","");
                //                warehouse_out = Regex.Replace(warehouse.Value.out_,@"\D","");
                //                warehouse_in_number = int.Parse(warehouse_in);
                //                warehouse_out_number = int.Parse(warehouse_out);
                //            }
                //        }
                //    }
                //}
                //dijkstra_in(warehouse_out_number, line_in_number);
            }
        }
        
        const int MAXNODE = 15;
        string[] missionbuf_in;
        string[] mission_list = new string[MAXNODE];

        public Rectangle[] node_area_ = new Rectangle[22];

        private void node_search()
        {
            int i = 0;
            foreach (KeyValuePair<string, RectangleF> node in Data.Instance.location_check_node)
            {
                Rectangle temp = new Rectangle();

                temp.X = (int)node.Value.X;
                temp.Y = (int)node.Value.Y;
                temp.Width = (int)node.Value.Width;
                temp.Height = (int)node.Value.Height;
                node_area_[i] = temp;
                i++;
            }
        }
        private void Readnodemission()
        {
            int i = 0;
            foreach (KeyValuePair<string, Node_mission> mission in Data.Instance.node_mission_list)
            {
                mission_list[i] = mission.Value.mission_id;
                i++;
            }
        }
        public string[] mission_temp;
        private void mission_insert()
        {

            try
            {
                //string[] mission_temp
                mission_temp = new string[mission_temp_list.Count()];
                mission_temp = mission_temp_list.ToArray();
                int cnt = mission_temp.Count();
                mission_temp_list.Clear();


                WorkFlowGoal workflowgoal = new WorkFlowGoal();

                workflowgoal.work_id = "MissionID_LS";
                workflowgoal.action_start_idx = 0;
                workflowgoal.loop_flag = 1;


                for (int i = 0; i < cnt; i++)
                {
                    DB_MissionData db_missiondata = new DB_MissionData();
                    MisssionInfo missioninfo = new MisssionInfo();
                    string strmissionid = mission_temp[i];
                    missioninfo = dbBridge.onDBRead_Mission(strmissionid);

                    string strwork = missioninfo.work;
                    db_missiondata = JsonConvert.DeserializeObject<DB_MissionData>(strwork);
                    float x = db_missiondata.work[0].action_args[0];
                    float y = db_missiondata.work[0].action_args[1];
                    float theta = db_missiondata.work[0].action_args[2];

                    Action act = new Action();
                    act.action_type = (int)Data.ACTION_TYPE.Goal_Point;

                    act.action_args.Add(x);
                    act.action_args.Add(y);
                    act.action_args.Add(theta);
                    ParameterSet paramset = new ParameterSet();
                    paramset.param_name = "max_trans_vel";
                    paramset.type = "float";
                    paramset.value = "0.3";
                    act.action_params.Add(paramset);

                    paramset = new ParameterSet();
                    paramset.param_name = "xy_goal_tolerance";
                    paramset.type = "float";
                    paramset.value = "0.15";
                    act.action_params.Add(paramset);

                    paramset = new ParameterSet();
                    paramset.param_name = "yaw_goal_tolerance";
                    paramset.type = "float";
                    paramset.value = "0.05";
                    act.action_params.Add(paramset);

                    paramset = new ParameterSet();
                    paramset.param_name = "p_drive";
                    paramset.type = "float";
                    paramset.value = "0.4";
                    act.action_params.Add(paramset);

                    paramset = new ParameterSet();
                    paramset.param_name = "d_drive";
                    paramset.type = "float";
                    paramset.value = "1.2";
                    act.action_params.Add(paramset);

                    paramset = new ParameterSet();
                    paramset.param_name = "wp_tolerance";
                    paramset.type = "float";
                    paramset.value = "1";
                    act.action_params.Add(paramset);

                    paramset = new ParameterSet();
                    paramset.param_name = "avoid";
                    paramset.type = "bool";
                    paramset.value = "false";
                    act.action_params.Add(paramset);

                    workflowgoal.work.Add(act);
                }
                string strMissionData_Json = JsonConvert.SerializeObject(workflowgoal);
                dbBridge.onDBInsert_Missionlist(DateTime.Now.ToString("LS_" + "yyyyMMddhhmm"), "LS_TEST_NAME", "0", "", strMissionData_Json);
            }
            catch (Exception e)
            {
                Console.WriteLine("insert mission buf error -> {0}", e);
            }
        }


        public async void onTaskOrder(string strTaskid, string[] missionbuf, string taskname, string strMissionid_list, string strRobotlist, int ntaskcnt)
        {
            try
            {
                Task_Order taskorder = new Task_Order();
                int cnt = missionbuf.Length;
                List<MisssionInfo> missioninfo_list = new List<MisssionInfo>();
                for (int i = 0; i < cnt; i++)
                {
                    MisssionInfo missioninfo = dbBridge.onDBRead_Mission(missionbuf[i]);
                    missioninfo_list.Add(missioninfo);
                }

                taskorder.task_id = strTaskid;
                taskorder.loop_flag = ntaskcnt;
                taskorder.missionlist = strMissionid_list;
                taskorder.robotlist = strRobotlist;

                commBridge.onTaskrder_publish(taskorder, taskname, missioninfo_list);


                //var task1 = Task.Run(() => commBridge.onTaskrder_publish(taskorder, taskname, missioninfo_list));
                //await task1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("onTaskOrder err=" + ex.Message.ToString());
            }
        }

        edit.mapEdit mapedit;

        internal Dictionary<string, LS_Socket> PLC_Socket_Info { get => pLC_Socket_Info; set => pLC_Socket_Info = value; }

        private void accordionControlElement26_Click(object sender, EventArgs e)
        {
            mapedit = null;
            mapedit = new edit.mapEdit(this);
            if (mainContainer.Controls.Count == 1)
            {
                mainContainer.Controls.RemoveAt(0);
            }
            mainContainer.Controls.Add(mapedit);
            mapedit.oninit();
        }

        private void accordionControlElement29_Click(object sender, EventArgs e)
        {

            if (mainContainer.Controls.Count > 0)
            {
                mainContainer.Controls.RemoveAt(0);
            }

            mainContainer.Controls.Add(taskoperationform);
            taskoperationform.oninit();


        }

        private void accordionControlElement20_Click(object sender, EventArgs e)
        {
            robotmonitoring = null;
            robotmonitoring = new robotMonitoring();
            Data.Instance.robot_id = "R_002";

            if (mainContainer.Controls.Count > 0)
            {
                mainContainer.Controls.RemoveAt(0);
            }
            mainContainer.Controls.Add(robotmonitoring);
        }

        private void accordionControlElement21_Click(object sender, EventArgs e)
        {
            robotmonitoring = null;
            robotmonitoring = new robotMonitoring();
            Data.Instance.robot_id = "R_003";

            if (mainContainer.Controls.Count > 0)
            {
                mainContainer.Controls.RemoveAt(0);
            }
            mainContainer.Controls.Add(robotmonitoring);
        }

        private void accordionControlElement22_Click(object sender, EventArgs e)
        {
            try
            {

                robotmonitoring = null;
                robotmonitoring = new robotMonitoring();
                Data.Instance.robot_id = "R_004";

                if (mainContainer.Controls.Count > 0)
                {
                    mainContainer.Controls.RemoveAt(0);
                }
                mainContainer.Controls.Add(robotmonitoring);
            }
            catch
            {
                Console.WriteLine("에러");
            }
        }

        private void accordionControlElement23_Click(object sender, EventArgs e)
        {
            robotmonitoring = null;
            robotmonitoring = new robotMonitoring();
            Data.Instance.robot_id = "R_006";

            if (mainContainer.Controls.Count > 0)
            {
                mainContainer.Controls.RemoveAt(0);
            }
            mainContainer.Controls.Add(robotmonitoring);
        }

        private void accordionControlElement24_Click(object sender, EventArgs e)
        {
            robotmonitoring = null;
            robotmonitoring = new robotMonitoring();
            Data.Instance.robot_id = "R_006";

            if (mainContainer.Controls.Count > 0)
            {
                mainContainer.Controls.RemoveAt(0);
            }
            mainContainer.Controls.Add(robotmonitoring);
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData();
            Application.Exit();
            
            if (Session_1 != null) Session_1.Abort();
            foreach (KeyValuePair<string, LS_Socket> info in PLC_Socket_Info)
            {
                string strid = info.Key;
                LS_Socket value = info.Value;

                if (value.bConnected)
                {
                    value.lsconnect_Evt -= new LS_Socket.LS_Connect(this.LS_Connect);
                    value.lsrev_Evt -= new LS_Socket.LS_Response(this.LS_Response);
                    value.scenarioCall_Evt -= new LS_Socket.LS_ScenarioCall(this.LS_ScenarioCall);
                    value.scenarioStart_Evt -= new LS_Socket.LS_ScenarioStart(this.LS_ScenarioStart);
                    value.ATCState_Evt -= new LS_Socket.LS_ATCNo_state(this.LS_ATCNo_state);
                    value.Disconnect();
                }
                value = null;
            }
            try
            {
                PLC_Socket_Info.Clear();
                ABN100_1.Clear();
                ABN100_2.Clear();
                ABN100_3.Clear();
                ABN100_4.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Closing err {0}", ex.Message.ToString());
            }
            
        }

        private void accordionControlElement25_Click(object sender, EventArgs e)
        {
            robotmonitoring = null;
            robotmonitoring = new robotMonitoring();
            Data.Instance.robot_id = "R_007";

            if (mainContainer.Controls.Count > 0)
            {
                mainContainer.Controls.RemoveAt(0);
            }
            mainContainer.Controls.Add(robotmonitoring);
        }

        private void accordionControlElement4_Click(object sender, EventArgs e)
        {
            plc_conn = new Thread(PLC_Connect);
            plc_conn.IsBackground = true;
            plc_conn.Start();
            timer1.Interval = 100;
            timer1.Enabled = true;
            firstform.plcThreadstate.BackColor = Color.Blue;
        }


        private void accordionControlElement5_Click(object sender, EventArgs e)
        {
            Session_ = new Thread(session_all_start);
            Session_.IsBackground = true;
            Session_.Start();
            sessionThread.Set();
            firstform.robotThreadstate.BackColor = Color.Blue;

        }


        private async void onTaskCancel(string robotid)
        {
            try
            {
                string strRobot = robotid;
                var task = Task.Run(() => commBridge.onTaskCancel_publish(strRobot, ""));
                await task;

                Thread.Sleep(100);

                Console.WriteLine("cancel = {0}" + strRobot);
            }
            catch (Exception ex)
            {
                Console.WriteLine("onTaskCancel err=" + ex.Message.ToString());
            }
        }

        #endregion
        public double onPointToPointDist(double x1, double y1, double x2, double y2)
        {
            double hypo = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            return hypo;
        }
        public void taskresult_dp(string strrobotid, string atc)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                firstform.textBox1.AppendText(string.Format("{0} -> {1} 미션 완료", strrobotid, atc) + "\r\n");

                //수정한 부분
                ScenarioList.Remove(atc);
            }));
        }
        public void taskresult_dp(string strrobotid, string atc, string fail)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                firstform.textBox1.AppendText(string.Format("{0} -> {1} 미션 실패. 사유 : 비정상적인 Taskresult", strrobotid, atc) + "\r\n");
            }));
        }
        public class testClass
        {
            public string ATC;
            public int number;
        }

        private string findmin(List<testClass> list)
        {
            try
            {
                int minvalue = int.MinValue;
                string temp = "";
                foreach (testClass type in list)
                {
                    if (type.number < minvalue)
                    {
                        minvalue = type.number;
                        temp = type.ATC;
                    }

                }
                return temp;
            }
            catch
            {
                return "";
            }
        }
        internal void TaskFeedbackResponse(string strrobotid)
        {


            if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskresult.msg != null)
            {
                if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskresult.msg.status.status == 3)
                {
                    DP_currentmission(strrobotid, "-");
                    Data.Instance.Robot_work_info[strrobotid].task_finished = false;
                    try
                    {

                        if (resetPLC.Count > 0)
                        {
                            if (resetPLC.Keys.Contains(strrobotid))
                            {

                                PLC_Socket_Info[resetPLC[strrobotid].requiredID].SetSTATE(0);
                                PLC_Socket_Info[resetPLC[strrobotid].requiredID].startATCNo = "";
                                PLC_Socket_Info[resetPLC[strrobotid].startID].SetSTATE(0);
                                setATCState(resetPLC[strrobotid].atcNO, false);
                                taskresult_dp(strrobotid, resetPLC[strrobotid].atcNO);

                                resetPLC.Remove(strrobotid);
                                //로봇 6,7번이 정상적으로 끝났을때
                                if (strrobotid == "R_006" || strrobotid == "R_007")
                                {
                                    if (Session_2_mission.Count > 0)
                                    {
                                        sessionThread.Reset();
                                        string ATC_temp = Search_minpath(strrobotid);
                                        Console.WriteLine($"{ATC_temp} <<<---- 검색된 최종 노드");
                                        for (int i2 = 0; i2 < Session_2_mission.Count; i2++)
                                        {
                                            if (Session_2_mission[i2].atcNO == ATC_temp)
                                            {
                                                missionType temp = new missionType();
                                                string atcNO = "";
                                                string startID = "";
                                                string requiredID = "";
                                                string mission = "";
                                                atcNO = Session_2_mission[i2].atcNO;
                                                startID = Session_2_mission[i2].startID;
                                                requiredID = Session_2_mission[i2].requiredID;

                                                PLC_Socket_Info[requiredID].SetSTATE(3);
                                                PLC_Socket_Info[requiredID].startATCNo = atcNO;
                                                PLC_Socket_Info[startID].SetSTATE(3);
                                                setATCState(atcNO, true);

                                                Session_2_taskStart(requiredID, startID, mission, atcNO, strrobotid);


                                                bRun_006 = false;
                                                if (Enable_Robot_List.Contains(strrobotid))
                                                    Enable_Robot_List.Remove(strrobotid);

                                                if (Session_2_mission.Count > 0)
                                                {
                                                    Session_2_mission.RemoveAt(i2);
                                                    firstform.DP_Session_2();
                                                }
                                                Invoke(new MethodInvoker(delegate ()
                                                {
                                                    firstform.textBox1.AppendText($" {strrobotid} -> {startID} [{atcNO}] 미션 시작\r\n");
                                                }));

                                                temp.atcNO = atcNO;
                                                temp.mission_ = mission;
                                                temp.startID = startID;
                                                temp.requiredID = requiredID;
                                                if (!resetPLC.Keys.Contains(strrobotid))
                                                    resetPLC.Add(strrobotid, temp);

                                            }
                                        }
                                        sessionThread.Set();
                                    }
                                    else
                                    {
                                        int start_node = firstform.findLocation(strrobotid);
                                        if (strrobotid == "R_006" || strrobotid == "R_007")
                                        {
                                            if (start_node < 16 && start_node != 12)
                                            {
                                                dijkstra_in(start_node, 12);
                                                taskoperationform.taskSave("99", "RETURN", strrobotid);
                                                Thread.Sleep(10);
                                                
                                                dbBridge.onDBDelete_Missionlist("RETURN");
                                                dbBridge.onDBDelete_Task("RETURN");
                                                checkbRun(strrobotid);
                                                //checkbRun(strrobotid);
                                            }
                                            else if (start_node == 12)
                                            {
                                                firstform.Robot_Parking(strrobotid);
                                                checkbRun(strrobotid);

                                            }
                                        }
                                    }
                                }
                                else // 로봇 1~5번 태스크가 끝났을때
                                {
                                    if(Session_2_mission.Count> 0)
                                    {
                                        foreach(missionType T in Session_2_mission)
                                        {
                                            if(T.atcNO == "35")
                                            {
                                                sessionThread.Reset();

                                                missionType temp = new missionType();
                                                string atcNO = "";
                                                string startID = "";
                                                string requiredID = "";
                                                string mission = "";
                                                atcNO = T.atcNO;
                                                startID = T.startID;
                                                requiredID = T.requiredID;


                                                Session_2_taskStart(requiredID, startID, mission, atcNO, strrobotid);


                                                checkbRun_false(strrobotid);
                                                if (Enable_Robot_List.Contains(strrobotid))
                                                    Enable_Robot_List.Remove(strrobotid);

                                                Invoke(new MethodInvoker(delegate ()
                                                {
                                                    firstform.textBox1.AppendText($" {strrobotid} -> {startID} [{atcNO}] 미션 시작(S1 -> 35)\r\n");
                                                }));

                                                temp.atcNO = atcNO;
                                                temp.mission_ = mission;
                                                temp.startID = startID;
                                                temp.requiredID = requiredID;
                                                if (!resetPLC.Keys.Contains(strrobotid))
                                                    resetPLC.Add(strrobotid, temp);
                                                Session_2_mission.RemoveAll(n => n.atcNO == "35");
                                                firstform.DP_Session_2();
                                                sessionThread.Set();
                                            }
                                        }
                                    }
                                    if (Session_1_mission.Count > 0)
                                    {
                                        sessionThread.Reset();

                                        missionType temp = new missionType();
                                        string atcNO = "";
                                        string startID = "";
                                        string requiredID = "";
                                        string mission = "";
                                        atcNO = Session_1_mission.Last().atcNO;
                                        startID = Session_1_mission.Last().startID;
                                        requiredID = Session_1_mission.Last().requiredID;


                                        Session_1_taskStart(requiredID, startID, mission, atcNO, strrobotid);


                                        checkbRun_false(strrobotid);
                                        if (Enable_Robot_List.Contains(strrobotid))
                                            Enable_Robot_List.Remove(strrobotid);

                                        Invoke(new MethodInvoker(delegate ()
                                        {
                                            firstform.textBox1.AppendText($" {strrobotid} -> {startID} [{atcNO}] 미션 시작\r\n");
                                        }));

                                        temp.atcNO = atcNO;
                                        temp.mission_ = mission;
                                        temp.startID = startID;
                                        temp.requiredID = requiredID;
                                        if (!resetPLC.Keys.Contains(strrobotid))
                                            resetPLC.Add(strrobotid, temp);

                                        Session_1_mission.RemoveAt(Session_1_mission.Count - 1);
                                        firstform.DP_Session_1();
                                        sessionThread.Set();
                                    }
                                    else
                                    {
                                        int start_node = firstform.findLocation(strrobotid);

                                        if (start_node < 16 && start_node != 13)
                                        {
                                            dijkstra_in(start_node, 13);
                                            taskoperationform.taskSave("99", "RETURN", strrobotid);
                                            Thread.Sleep(10);
                                            dbBridge.onDBDelete_Missionlist("RETURN");
                                            dbBridge.onDBDelete_Task("RETURN");
                                            checkbRun(strrobotid);
                                        }
                                        else if (start_node == 13)
                                        {
                                            Console.WriteLine("주차미션 추가예정");

                                        }
                                    }
                                }

                            }

                        }
                        else
                        {

                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("task result response error - >{0}", e);
                    }


                }
                else
                {

                    Console.WriteLine("Set state 0 ok{0},{1},{2}", resetPLC[strrobotid].requiredID, resetPLC[strrobotid].startID, resetPLC[strrobotid].atcNO);
                    taskresult_dp(strrobotid, resetPLC[strrobotid].atcNO, "FAIL");
                    DP_currentmission(strrobotid, "FAIL");  
                    PLC_Socket_Info[resetPLC[strrobotid].requiredID].SetSTATE(0);
                    PLC_Socket_Info[resetPLC[strrobotid].requiredID].startATCNo = "";
                    PLC_Socket_Info[resetPLC[strrobotid].startID].SetSTATE(0);
                    setATCState(resetPLC[strrobotid].atcNO, false);

                    if (resetPLC.Keys.Contains(strrobotid))
                        resetPLC.Remove(strrobotid);
                    checkbRun(strrobotid);


                }

            }

        }
        private void SaveData()
        {
            string title = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
            title += "_log.txt";
            string msg = "";
            try
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    msg = firstform.textBox1.Text;
                    using(System.IO.StreamWriter sw = System.IO.File.AppendText(title))
                    {
                        sw.WriteLine(msg);
                        sw.Close();
                    }
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine("SaveData err {0}",ex.Message.ToString());
            }
            Console.WriteLine("SaveData Complete");
        }
        private void dijkstra_in(int s, int e)
        {
            try
            {

                int i, j, k, r, min;
                int[] v = new int[MAX_NODE];
                int[] distance = new int[MAX_NODE];
                int[] via = new int[MAX_NODE];
                int bb1 = 1;
                k = 0;

                for (j = 0; j < MAX_NODE; j++)
                {
                    v[j] = 0;
                    distance[j] = INF;
                }

                distance[s - 1] = 0;

                int[] path = new int[MAX_NODE];
                int path_cnt = 0;
                for (i = 0; i < MAX_NODE; i++)
                {
                    min = INF;
                    for (j = 0; j < MAX_NODE; j++)
                    {
                        if (v[j] == 0 && distance[j] < min)
                        {
                            k = j;
                            min = distance[j];
                        }
                    }

                    v[k] = 1;

                    if (min == INF) break;
                    {
                        for (j = 0; j < MAX_NODE; j++)
                        {
                            if (distance[j] > distance[k] + a[k, j])
                            {
                                distance[j] = distance[k] + a[k, j];
                                via[j] = k;
                            }
                        }
                    }


                    path_cnt = 0;
                    k = e - 1;
                    while (bb1 < 3)
                    {
                        if (path_cnt > MAX_NODE - 1) break;

                        path[path_cnt++] = k;

                        if (k == s - 1) break;

                        k = via[k];
                    }

                }

                string strdist = "";
                string strnode = "";

                strdist = string.Format("{0}에서 출발하여, {1}로 가는 최단 거리는 {2}입니다.\n", s, e, distance[e - 1]);
                Console.WriteLine("\n {0}에서 출발하여, {1}로 가는 최단 거리는 {2}입니다.\n", s, e, distance[e - 1]);

                strnode = " 경로는 : ";
                missionbuf_in = new string[path_cnt];
                Console.WriteLine(" 경로는 : ");
                int n = 0;

                for (i = path_cnt - 1; i >= 1; i--)
                {
                    Console.WriteLine("{0} -> ", path[i] + 1);
                    missionbuf_in[n] = mission_list[path[i]];
                    n++;
                }
                missionbuf_in[n] = mission_list[path[i]];
                Console.WriteLine("{0}입니다", path[i] + 1);
                n = 0;

                insertTask();
            }
            catch (Exception f)
            {
                Console.WriteLine("다익스트라 에러 -> {0}", f);
            }
        }
        private void common_method(string atcNO, string strrobotid)
        {
            mission_temp = new string[mission_temp_list.Count()];
            mission_temp = mission_temp_list.ToArray();
            int cnt = mission_temp.Count();

            taskoperationform.taskSave(atcNO, taskoperationform.ConvertString(mission_temp), strrobotid);

            mission_temp_list.Clear();
            dbBridge.onDBDelete_Missionlist("TEMP_");


            CrashCheckRobot_list.Add(strrobotid);
            Invoke(new MethodInvoker(delegate ()
            {
            }));
        }
        private int dijkstra_in(int s, int e, string empty)
        {
            try
            {
                if (s > 16)
                    return 50;
                int i, j, k, r, min;
                int[] v = new int[MAX_NODE];
                int[] distance = new int[MAX_NODE];
                int[] via = new int[MAX_NODE];
                int bb1 = 1;
                k = 0;

                for (j = 0; j < MAX_NODE; j++)
                {
                    v[j] = 0;
                    distance[j] = INF;
                }

                distance[s - 1] = 0;

                int[] path = new int[MAX_NODE];
                int path_cnt = 0;
                for (i = 0; i < MAX_NODE; i++)
                {
                    min = INF;
                    for (j = 0; j < MAX_NODE; j++)
                    {
                        if (v[j] == 0 && distance[j] < min)
                        {
                            k = j;
                            min = distance[j];
                        }
                    }

                    v[k] = 1;

                    if (min == INF) break;
                    {
                        for (j = 0; j < MAX_NODE; j++)
                        {
                            if (distance[j] > distance[k] + a[k, j])
                            {
                                distance[j] = distance[k] + a[k, j];
                                via[j] = k;
                            }
                        }
                    }


                    path_cnt = 0;
                    k = e - 1;
                    while (bb1 < 3)
                    {
                        if (path_cnt > MAX_NODE - 1) break;

                        path[path_cnt++] = k;

                        if (k == s - 1) break;

                        k = via[k];
                    }

                }

                string strdist = "";
                string strnode = "";

                Console.WriteLine($"{s}에서 출발하여, {e}로 가는 최단 거리는 {distance[e - 1]}입니다.\n");
                return distance[e - 1];


            }
            catch (Exception f)
            {
                Console.WriteLine("다익스트라 에러 -> {0}", f);
                return 99;
            }
        }
        private void insertTask()
        {

            try
            {
                int cnt = missionbuf_in.Count();


                WorkFlowGoal workflowgoal = new WorkFlowGoal();

                workflowgoal.work_id = "RETURN";
                workflowgoal.action_start_idx = 0;
                workflowgoal.loop_flag = 1;


                for (int i = 0; i < cnt; i++)
                {
                    DB_MissionData db_missiondata = new DB_MissionData();
                    MisssionInfo missioninfo = new MisssionInfo();
                    string strmissionid = missionbuf_in[i];
                    missioninfo = dbBridge.onDBRead_Mission(strmissionid);

                    string strwork = missioninfo.work;
                    db_missiondata = JsonConvert.DeserializeObject<DB_MissionData>(strwork);
                    float x = db_missiondata.work[0].action_args[0];
                    float y = db_missiondata.work[0].action_args[1];
                    float theta = db_missiondata.work[0].action_args[2];

                    Action act = new Action();
                    act.action_type = (int)Data.ACTION_TYPE.Goal_Point;

                    act.action_args.Add(x);
                    act.action_args.Add(y);
                    act.action_args.Add(theta);
                    ParameterSet paramset = new ParameterSet();
                    paramset.param_name = "max_trans_vel";
                    paramset.type = "float";
                    paramset.value = "0.7";
                    act.action_params.Add(paramset);

                    paramset = new ParameterSet();
                    paramset.param_name = "xy_goal_tolerance";
                    paramset.type = "float";
                    paramset.value = "0.15";
                    act.action_params.Add(paramset);

                    paramset = new ParameterSet();
                    paramset.param_name = "yaw_goal_tolerance";
                    paramset.type = "float";
                    paramset.value = "0.05";
                    act.action_params.Add(paramset);

                    paramset = new ParameterSet();
                    paramset.param_name = "p_drive";
                    paramset.type = "float";
                    paramset.value = "0.4";
                    act.action_params.Add(paramset);

                    paramset = new ParameterSet();
                    paramset.param_name = "d_drive";
                    paramset.type = "float";
                    paramset.value = "1.2";
                    act.action_params.Add(paramset);

                    paramset = new ParameterSet();
                    paramset.param_name = "wp_tolerance";
                    paramset.type = "float";
                    paramset.value = "1";
                    act.action_params.Add(paramset);

                    paramset = new ParameterSet();
                    paramset.param_name = "avoid";
                    paramset.type = "bool";
                    paramset.value = "true";
                    act.action_params.Add(paramset);

                    paramset = new ParameterSet();
                    paramset.param_name = "passing_flag";
                    paramset.type = "bool";
                    paramset.value = "true";
                    act.action_params.Add(paramset);

                    workflowgoal.work.Add(act);
                }
                string strMissionData_Json = JsonConvert.SerializeObject(workflowgoal);
                dbBridge.onDBInsert_Missionlist(DateTime.Now.ToString("LS_" + "yyyyMMddhhmm"), "RETURN", "0", "", strMissionData_Json);

            }
            catch (Exception e)
            {
                Console.WriteLine("insert mission buf error -> {0}", e);
            }
        }

    }



}
