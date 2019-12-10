using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

using System.Runtime.InteropServices;

namespace Syscon_Solution
{
   
    public partial class Fleet_Main : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {

        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Windows = 8,
        };
        [DllImport("User32.dll")]
        public static extern bool RegisterHotKey(IntPtr hwnd, int id, KeyModifiers fsModifiers, Keys vk);
        [DllImport("User32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hwnd, int id);


        public Fleet_Main()
        {
            InitializeComponent();
        }

        

        #region variables

        public FleetManager.DB.DB_bridge dbBridge = new FleetManager.DB.DB_bridge();
        public FleetManager.Comm.Comm_bridge commBridge ;

        FleetManager.UI.Edit.MissionEdit_Ctrl misssioneditCtrl;
        FleetManager.UI.Edit.TaskEdit_Ctrl taskeditCtrl;
        FleetManager.UI.Edit.MapEdit_Ctrl mapeditCtrl;

        FleetManager.UI.Task1.TaskOperation_Ctrl taskopCtrl;

        FleetManager.UI.Monitoring.MapMonitoring_Ctrl mapmonitoringCtrl;
        FleetManager.UI.Monitoring.RobotMonitoring_Ctrl robotmonitoringCtrl;

        public ingdlg ingdlg = new ingdlg();

        Thread ServerConnect_Checkthread;
       

        

       

        #endregion

        private void Fleet_Main_Load(object sender, EventArgs e)
        {
#if _fleetmain
            Data.Instance.MAINFORM = this;

            //commBridge = new FleetManager.Comm.Comm_bridge(this);
            misssioneditCtrl = new FleetManager.UI.Edit.MissionEdit_Ctrl(this);
            taskeditCtrl = new FleetManager.UI.Edit.TaskEdit_Ctrl(this);
            taskopCtrl = new FleetManager.UI.Task1.TaskOperation_Ctrl(this);
            mapmonitoringCtrl = new FleetManager.UI.Monitoring.MapMonitoring_Ctrl(this);
            robotmonitoringCtrl = new FleetManager.UI.Monitoring.RobotMonitoring_Ctrl(this);
            mapeditCtrl = new FleetManager.UI.Edit.MapEdit_Ctrl(this);
#endif


            if (DP_panel.Controls.Count == 1)
            {
                DP_panel.Controls.RemoveAt(0);
            }
           Data.Instance.nFormidx = 0;


            dbBridge.onDBConnectionPath_OpenCheck();

            //RIDiS DB 연결
            if (dbBridge.onRIDiS_InitSql() == 0)
            { }
            else
            {
                //연결 실패
                Data.Instance.isDBConnected = false;
                MessageBox.Show("RIDiS 데이타베이스 연결 에러, 점검후 사용하세요.");
                return;
            }

            //solution DB 연결
        /*    if (dbBridge.onInitSql() == 0)
            { }
            else
            {
                //연결 실패
                Data.Instance.isDBConnected = false;
                MessageBox.Show("Solution 데이타베이스 연결 에러, 점검후 사용하세요.");
                return;
            }
            */
            onRobotListCheck();


            commBridge.taskresult_Evt += new FleetManager.Comm.Comm_bridge.TaskResultResponse(this.TaskResultResponse);
            //commBridge.taskfeedback_Evt += new FleetManager.Comm.Comm_bridge.TaskFeedbackResponse(this.TaskFeedbackResponse);
            //commBridge.RobotState_Evt += new FleetManager.Comm.Comm_bridge.RobotStateComplete(this.RobotStateComplete);
            //commBridge.MoveBase_Status_Evt += new FleetManager.Comm.Comm_bridge.MoveBase_StatusComplete(this.MoveBase_StatusComplete);

            Data.Instance.bTaskRun = false;


            RegisterHotKey(this.Handle, 31111, KeyModifiers.Control, Keys.End);
            RegisterHotKey(this.Handle, 31121, KeyModifiers.Control, Keys.Down);
            RegisterHotKey(this.Handle, 31131, KeyModifiers.Control, Keys.PageDown);

            RegisterHotKey(this.Handle, 31211, KeyModifiers.Control, Keys.D1);
            RegisterHotKey(this.Handle, 31321, KeyModifiers.Control, Keys.D2);
            RegisterHotKey(this.Handle, 31431, KeyModifiers.Control, Keys.D3);


            RegisterHotKey(this.Handle, 31511, KeyModifiers.Control, Keys.Home);
            RegisterHotKey(this.Handle, 31621, KeyModifiers.Control, Keys.Up);
            RegisterHotKey(this.Handle, 31731, KeyModifiers.Control, Keys.PageUp);

            RegisterHotKey(this.Handle, 31831, KeyModifiers.Control, Keys.A);
            RegisterHotKey(this.Handle, 31931, KeyModifiers.Control, Keys.S);
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0312:
                    Keys key = (Keys)(((int)m.LParam >> 16) & 0xffff);
                    KeyModifiers modifier = (KeyModifiers)((int)m.LParam & 0xffff);

                    if ((KeyModifiers.Control) == modifier)
                    {
                        if (Data.Instance.isConnected)
                        {
                            //if (Keys.D1 == key)
                            //{
                            //    UnregisterHotKey(this.Handle, 31211 + 1);
                            //    Dictionary<string, string> workinfo = new Dictionary<string, string>();
                            //    workinfo.Add("R_001", "");
                            //    onJobPause(workinfo);
                            //    RegisterHotKey(this.Handle, 31211, KeyModifiers.Control, Keys.D1);
                            //}
                            //else if (Keys.D2 == key)
                            //{
                            //    UnregisterHotKey(this.Handle, 31221 + 1);
                            //    Dictionary<string, string> workinfo = new Dictionary<string, string>();
                            //    workinfo.Add("R_002", "");
                            //    onJobPause(workinfo);
                            //    RegisterHotKey(this.Handle, 31221, KeyModifiers.Control, Keys.D2);
                            //}
                            //else if (Keys.D3 == key)
                            //{
                            //    UnregisterHotKey(this.Handle, 31231 + 1);
                            //    Dictionary<string, string> workinfo = new Dictionary<string, string>();
                            //    workinfo.Add("R_003", "");
                            //    onJobPause(workinfo);
                            //    RegisterHotKey(this.Handle, 31231, KeyModifiers.Control, Keys.D3);
                            //}

                            if (Keys.End == key)
                            {
                                UnregisterHotKey(this.Handle, 31111 + 1);
                                Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                string strobotid = "R_001";
                                taskopCtrl.onTaskPause(strobotid);
                                RegisterHotKey(this.Handle, 31111, KeyModifiers.Control, Keys.End);
                            }
                            else if (Keys.Down == key)
                            {
                                UnregisterHotKey(this.Handle, 31121 + 1);
                                Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                string strobotid = "R_002";
                                taskopCtrl.onTaskPause(strobotid);
                                RegisterHotKey(this.Handle, 31121, KeyModifiers.Control, Keys.Down);
                            }
                            else if (Keys.PageDown == key)
                            {
                                UnregisterHotKey(this.Handle, 31131 + 1);
                                Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                string strobotid = "R_003";
                                taskopCtrl.onTaskPause(strobotid);
                                RegisterHotKey(this.Handle, 31131, KeyModifiers.Control, Keys.PageDown);
                            }


                            if (Keys.Home == key)
                            {
                                UnregisterHotKey(this.Handle, 31511 + 1);
                                Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                string strobotid = "R_001";
                                taskopCtrl.onTaskResum(strobotid);
                                RegisterHotKey(this.Handle, 31511, KeyModifiers.Control, Keys.Home);
                            }
                            else if (Keys.Up == key)
                            {
                                UnregisterHotKey(this.Handle, 31621 + 1);
                                Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                string strobotid = "R_002";
                                taskopCtrl.onTaskResum(strobotid);
                                RegisterHotKey(this.Handle, 31621, KeyModifiers.Control, Keys.Up);
                            }
                            else if (Keys.PageUp == key)
                            {
                                UnregisterHotKey(this.Handle, 31731 + 1);
                                Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                string strobotid = "R_003";
                                taskopCtrl.onTaskResum(strobotid);
                                RegisterHotKey(this.Handle, 31731, KeyModifiers.Control, Keys.PageUp);
                            }


                        }
                    }


                    break;
            }

            base.WndProc(ref m);
        }

        private void onRobotListCheck()
        {
           dbBridge.onDBRead_Robotlist("all");
         //   onDBRead_RobotStatus();
           onRobots_WorkInfo_InitSet();
        }

        /// <summary>
        /// 등록된 로봇의 워크 정보를 초기화 한다.
        /// </summary>
        public void onRobots_WorkInfo_InitSet()
        {
            try
            {
                Data.Instance.Robot_work_info.Clear();
                //foreach (KeyValuePair<string, string> info in Data.Instance.Robot_status_info)
                int cnt = 0;
                cnt = Data.Instance.Robot_RegInfo_list.Count;
                if(cnt>0)
                {
                    foreach (KeyValuePair<string, Robot_RegInfo> info in Data.Instance.Robot_RegInfo_list)
                    {
                        string strrobotid = info.Key;
                        Robot_RegInfo value = info.Value;

                        Data.Instance.Robot_work_info.Add(strrobotid, commBridge.onNewRobotWorkInfo_initial(strrobotid, "", 1, 0, "", ""));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobots_WorkInfo_InitSet err :={0}", ex.Message.ToString());
            }
        }


        #region 기본 ros 연결/해제
        /// <summary>
        /// Form강제종료시 처리할 내용들
        /// </summary>
        public void onCompulsion_Close() //강제종료시 처리할 부분들
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    //worker.onDeleteAllSubscribe_Compulsion();
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("onCompulsion_Close err" + ex.Message.ToString());
                }
            }
        }

        private async void ROSConnection(string strAddr)
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    onCompulsion_Close();

                    ROSDisconnect();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ROSConnection err =" + ex.Message.ToString());
                }
            }
            else
            {
                try
                {
                    if (btnConnect.Text == "connect")
                    {
                        ingdlg.onLblMsg("서버에 연결중입니다.");
                        ingdlg.Show();
                        string uri = strAddr;
                        Data.Instance.socket = new Rosbridge.Client.Socket(new Uri(uri));

                        Data.Instance.md = new MessageDispatcher(Data.Instance.socket, new MessageSerializerV2_0());
                        await Data.Instance.md.StartAsync();

                        if(Data.Instance.socket.Connected)
                        {
                        }

                        Data.Instance.isConnected = true;

                        if(ServerConnect_Checkthread!=null)
                        {
                            ServerConnect_Checkthread.Abort();
                            ServerConnect_Checkthread = null;
                        }
                        ServerConnect_Checkthread = new Thread(onConnectCheck);
                        ServerConnect_Checkthread.Start();


                        if (Data.Instance.isConnected == true)
                        {

                            btnConnect.Enabled = true;
                            btnConnect.Text = "disconnect";


                            onSuscribe_RobotsStatus_Basic();

                            ingdlg.Hide();

                        }
                    }
                    else
                    {
                        ROSDisconnect();

                        btnConnect.Enabled = true;
                        ingdlg.Hide();
                    }
                }
                catch (Exception ex)
                {
                    ROSDisconnect();
                    if (Data.Instance.isConnected == false)
                    {
                        ingdlg.Hide();
                        btnConnect.Enabled = true;
                        MessageBox.Show("연결에 실패하였습니다.");
                    }
                    return;
                }
            }
        }

        public void ROSDisconnect()
        {
            try
            {
                if (Data.Instance.md != null)
                {
                    Data.Instance.md.Dispose();
                    Data.Instance.md = null;
                }
                Data.Instance.isConnected = false;

                Invoke(new MethodInvoker(delegate ()
                {
                    btnConnect.Text = "connect";
                    btnConnect.Enabled = true;
                }));
                Data.Instance.socket = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ROSDisconnect err = " + ex.Message.ToString());
            }
        }

        private void onConnectCheck()
        {
            try
            {
                for (; ; )
                {
                    if (Data.Instance.bFormClose) break;

                    if (!Data.Instance.isConnected) break;

                    if (!Data.Instance.socket.Connected)
                    {
                        
                        ROSDisconnect();
                        break;
                    }
                    Thread.Sleep(10);

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onConnectCheck err :={0}", ex.Message.ToString());
            }
        }

        #endregion

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
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSuscribe_RobotsStatus_Basic err :={0}", ex.Message.ToString());
            }
        }



        private void btnConnect_Click(object sender, EventArgs e)
        {
            string strAddr = txtAddr.Text.ToString();

            btnConnect.Enabled = false;
            ROSConnection(strAddr);
        }

        public void onCurrMisionDP(string strRobot, string strmission_id)
        {
            taskopCtrl.onCurrMisionDP(strRobot, strmission_id);
        }

        #region monitoring form
        private void mapMonitoring_open_Click(object sender, EventArgs e)
        {
            if (DP_panel.Controls.Count == 1) DP_panel.Controls.RemoveAt(0);

            DP_panel.Controls.Add(mapmonitoringCtrl);
            Data.Instance.nFormidx = (int)Data.FORM_IDX.Monitoring_Map;
            mapmonitoringCtrl.onInitSet();
        }

        private void robotMonitoring_open_Click(object sender, EventArgs e)
        {
            if (DP_panel.Controls.Count == 1) DP_panel.Controls.RemoveAt(0);

            DP_panel.Controls.Add(robotmonitoringCtrl);
            Data.Instance.nFormidx = (int)Data.FORM_IDX.Monitoring_Robot;
            robotmonitoringCtrl.onInitSet();
        }
        #endregion

        #region edit form
        private void missionEdit_open_Click(object sender, EventArgs e)
        {

            if (DP_panel.Controls.Count == 1) DP_panel.Controls.RemoveAt(0);

            DP_panel.Controls.Add(misssioneditCtrl);
            Data.Instance.nFormidx = (int)Data.FORM_IDX.Edit_MissionForm;
            misssioneditCtrl.onInitSet();
        }

        private void taskSet_open_Click(object sender, EventArgs e)
        {
            if (DP_panel.Controls.Count == 1) DP_panel.Controls.RemoveAt(0);

            DP_panel.Controls.Add(taskeditCtrl);
            Data.Instance.nFormidx = (int)Data.FORM_IDX.Edit_TaskForm;
            taskeditCtrl.onInitSet();

            
        }

        private void mapEdit_open_Click(object sender, EventArgs e)
        {
            if (DP_panel.Controls.Count == 1) DP_panel.Controls.RemoveAt(0);

            DP_panel.Controls.Add(mapeditCtrl);
            Data.Instance.nFormidx = (int)Data.FORM_IDX.Edit_MapForm;
            mapeditCtrl.onInitSet();
            
        }
        #endregion

       
        #region task run form
        private void taskRun_open_Click(object sender, EventArgs e)
        {
            if (DP_panel.Controls.Count == 1) DP_panel.Controls.RemoveAt(0);

            DP_panel.Controls.Add(taskopCtrl);
            Data.Instance.nFormidx = (int)Data.FORM_IDX.Operaion_TaskForm;
            taskopCtrl.onInitSet();
        }
        #endregion


        #region result form
        private void robotResult_open_Click(object sender, EventArgs e)
        {

        }

        private void taskResult_open_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region setting form
        private void robotSetting_open_Click(object sender, EventArgs e)
        {

        }

        private void xisSetting_open_Click(object sender, EventArgs e)
        {

        }
        #endregion


        public void TaskResultResponse(string strrobotid)
        {
            try
            {
                Console.WriteLine("sdklfjlasfsadfweiilzxcjlvxzvjz");
                Console.WriteLine("sdklfjlasfsadfweiilzxcjlvxzvjz");
                Console.WriteLine("sdklfjlasfsadfweiilzxcjlvxzvjz");
                Console.WriteLine("sdklfjlasfsadfweiilzxcjlvxzvjz");
                Console.WriteLine("sdklfjlasfsadfweiilzxcjlvxzvjz");
            }
            catch (Exception ex)
            {
                Console.WriteLine("TaskResultResponse err=" + ex.Message.ToString());
            }
        }

        public void MoveBase_StatusComplete(string strrobotid)
        {
            try
            {
               // if (Data.Instance.nFormidx == (int)Data.FORM_IDX.Operaion_TaskForm)
               // {
                    taskopCtrl.MoveBase_StatusComplete(strrobotid);
              //  }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MoveBase_StatusComplete err=" + ex.Message.ToString());
            }
        }

        public void TaskFeedbackResponse(string strrobotid)
        {
            try
            {

                Invoke(new MethodInvoker(delegate ()
                {
                    //lblActionidx.Text = string.Format("{0}", nactionidx);
                }));

              //  if (Data.Instance.nFormidx == (int)Data.FORM_IDX.Operaion_TaskForm)
              //  {
                    if (Data.Instance.Robot_work_info[strrobotid].mission_complete)
                    {
                        if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.task_complete) return;

                        Data.Instance.Robot_work_info[strrobotid].mission_complete = false;

                        taskopCtrl.TaskFeedback_Complete(strrobotid);
                    }
                    else
                    {
                       taskopCtrl.TaskFeedback_Ing(strrobotid);
                    }

           //     }

            }
            catch (Exception ex)
            {
                Console.WriteLine("TaskFeedbackResponse err=" + ex.Message.ToString());
            }
        }

        public void TaskFinished(string strtaskid)
        {
            try
            {
                lock (this)
                {
                    //db 정보 갱신
                    dbBridge.onDBUpdate_Tasklist_status(strtaskid, "wait");

                    //task operation db 삭제 
                    dbBridge.onDBDelete_TaskOperation(strtaskid, "");

                    Data.Instance.bMissionCompleteCheck = false;

                    //쓰레드 삭제
                    int threadcnt = Data.Instance.TaskCheck_threadList.Count;
                    if (threadcnt > 0)
                    {
                        int thridx = 0;
                        TaskCheck_class taskclass = Data.Instance.TaskCheck_threadList.ElementAt(thridx);

                        if (taskclass.strTaskid == strtaskid)
                        {
                            if (taskclass.taskthred != null)
                            {
                                taskclass.taskthred.Abort();
                                taskclass = null;
                            }

                            Data.Instance.TaskCheck_threadList.RemoveAt(thridx);
                        }
                    }

                    if (Data.Instance.nFormidx == (int)Data.FORM_IDX.Operaion_TaskForm)
                    {
                        taskopCtrl.onInitSet();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("TaskFinished err=" + ex.Message.ToString());
            }
        }

        double prev_robot_x = 100000;
        double prev_robot_y = 100000;

        public void RobotStateComplete(string strrobotid)
        {
            string strRobotPos_RectFile = "..//Ros_info//robotpos_rec";
            strRobotPos_RectFile = strRobotPos_RectFile + string.Format("{0}_{1}.txt", strrobotid, Data.Instance.strTaskRun_StartTime);

            if (!File.Exists(strRobotPos_RectFile))
            {
                using (StreamWriter sw = new System.IO.StreamWriter(strRobotPos_RectFile, false, Encoding.Default))
                {
                    sw.Close();
                }
            }
           

            if (Data.Instance.bTaskRun)
            {
                if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate == null) return;
                if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate.msg == null) return;

            //    if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.globalplan == null) return;
            //    if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.globalplan.msg == null) return;

                if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback == null) return;
                if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg == null) return;

                int act_idx = Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.action_indx;


                double x = Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate.msg.pose.x;
                double y = Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate.msg.pose.y;

                string strprev_x = string.Format("{0:f4}", prev_robot_x);
                string strprev_y = string.Format("{0:f4}", prev_robot_y);
                string str_x = string.Format("{0:f4}", x);
                string str_y = string.Format("{0:f4}", y);


                if (strprev_x!=str_x && strprev_y!=str_y)
                {
                    prev_robot_x = x;
                    prev_robot_y = y;

                    if (act_idx > 0)
                    {
                        string strmsg = string.Format("act idx:{0},X:{1:f4},Y:{2:f4},Time:{3}", act_idx, x, y, "");

                        using (StreamWriter sw = new System.IO.StreamWriter(strRobotPos_RectFile, true, Encoding.Default))
                        {
                            sw.WriteLine(strmsg);
                            sw.Close();
                        }
                    }
                }
            }
        }


        public void MapInfoComplete(string strrobotid)
        {
            try
            {
                if(Data.Instance.nFormidx == (int)Data.FORM_IDX.Monitoring_Map)
                {
                    mapmonitoringCtrl.MapInfoComplete(strrobotid);
                }
                else if (Data.Instance.nFormidx == (int)Data.FORM_IDX.Edit_MissionForm)
                {
                    misssioneditCtrl.MapInfoComplete(strrobotid);
                }

                TopicList list = new TopicList();
                commBridge.onDeleteSelectSubscribe(strrobotid + list.topic_staticMap);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("MapInfoComplete err :={0}", ex.Message.ToString());
            }
        }

        public void GlobalCostInfoComplete(string strrobotid)
        {
            try
            {
                if (Data.Instance.nFormidx == (int)Data.FORM_IDX.Monitoring_Map)
                {
                    mapmonitoringCtrl.GlobalCostComplete(strrobotid);
                }
                else if (Data.Instance.nFormidx == (int)Data.FORM_IDX.Edit_MissionForm)
                {
                    misssioneditCtrl.GlobalCostComplete(strrobotid);
                }

                TopicList list = new TopicList();
                commBridge.onDeleteSelectSubscribe(strrobotid + list.topic_globalCost);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("GlobalCostInfoComplete err :={0}", ex.Message.ToString());
            }
        }

        public void LocalCostInfoComplete(string strrobotid)
        {
            try
            {
                if (Data.Instance.nFormidx == (int)Data.FORM_IDX.Monitoring_Map)
                {
                    mapmonitoringCtrl.LocalCostInfoComplete(strrobotid);
                }

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("LocalCostInfoComplete err :={0}", ex.Message.ToString());
            }
        }

        public void GlobalpathComplete(string strrobotid)
        {
            try
            {
                if (Data.Instance.nFormidx == (int)Data.FORM_IDX.Monitoring_Robot)
                {
                   robotmonitoringCtrl.GlobalpathComplete(strrobotid);
                }
                

            }
            catch (Exception ex)
            {
               
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
           Data.Instance.TaskCheck_threadList.Clear();

            for (int i = 0; i < 10; i++)
            {
                string strid = string.Format("thr{0}", i);

                TaskCheck_class taskcheck = new TaskCheck_class();

                taskcheck.taskthred = new Thread(taskcheck.taskCheck_thread_func);
                taskcheck.taskthred.Start(strid);

                Data.Instance.TaskCheck_threadList.Add(taskcheck);
            }

        }

        private void Fleet_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Data.Instance.bFormClose = true;
        }

        private void Fleet_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            // if (Data.Instance.socket.Connected)
            // {
            //    ROSDisconnect();
            // }

            Data.Instance.bCrashcheckStop = true;

            if (Data.Instance.G_SqlCon != null)
            {
                if (Data.Instance.G_SqlCon.State == ConnectionState.Open)
                {
                    Data.Instance.G_SqlCon.Close();
                }
            }

            if (Data.Instance.G_DynaSqlCon != null)
            {
                if (Data.Instance.G_DynaSqlCon.State == ConnectionState.Open)
                {
                    Data.Instance.G_DynaSqlCon.Close();
                }
            }
            
            if (ServerConnect_Checkthread != null)
            {
                ServerConnect_Checkthread.Abort();
                ServerConnect_Checkthread = null;
            }

            if (robotmonitoringCtrl != null)
            {
                if (robotmonitoringCtrl.m_bMonitoring)
                {
                    robotmonitoringCtrl.m_bMonitoring = false;
                }
            }
        }

        public double onPointToPointDist(double x1, double y1, double x2, double y2)
        {
            double hypo = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            return hypo;
        }

        public float DegreeToRadian(string degree)
        {
            return ((float)(Math.PI / 180.0f) * float.Parse(degree));
        }

        public float RadianToDegree(string radian)
        {
            return (float)((float.Parse(radian) * 180.0f) / Math.PI);// ((float)(Math.PI / 180.0f) * float.Parse(radian));
        }
    }

    


    public class TaskCheck_class
    {
        public string strTaskid;
        public Thread taskthred;

        public event TaskFinished taskfinish_Evt;
        public delegate void TaskFinished(string strTaskid);

        public void taskCheck_thread_func(object taskinfo)
        {
            try
            {
                if(Data.Instance.isConnected)
                { 
                    Task_checkThread_TableInfo task_checkinfo = (Task_checkThread_TableInfo)taskinfo;
                    int cnt =task_checkinfo.taskcheck_info.Count;
            
                    for (; ; )
                    {
                        if (Data.Instance.bFormClose) break;

                        int nfinishcnt = 0;

                        for (int i = 0; i < cnt; i++)
                        {
                            string strrobotid = task_checkinfo.taskcheck_info[i].strrobotid;

                            if (Data.Instance.Robot_work_info.ContainsKey(strrobotid))
                            {
                                if (Data.Instance.Robot_work_info[strrobotid].task_finished)
                                {
                                    nfinishcnt++;
                                }
                                else
                                {

                                }
                            }
                        }

                        if (nfinishcnt == cnt)
                        {
#if !_mapping
                            taskfinish_Evt(strTaskid);
#endif
                            break;
                        }

                        Thread.Sleep(10);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("taskCheck_thread_func err :={0}", ex.Message.ToString());
            }
        }
    }
}
