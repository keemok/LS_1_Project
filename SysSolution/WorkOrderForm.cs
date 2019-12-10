using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
using System.Runtime.InteropServices;

namespace SysSolution
{
   

    public partial class WorkOrderForm : Form
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




      


        Worker worker;

       Frm.ingdlg ingdlg = new Frm.ingdlg();

        public WorkOrderForm()
        {
            InitializeComponent();
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
                    worker.onDeleteAllSubscribe_Compulsion();
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("onCompulsion_Close err"+ ex.Message.ToString());
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
                    Console.WriteLine("ROSConnection err ="+ex.Message.ToString());
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

                        Data.Instance.isConnected = true;

                        if (Data.Instance.isConnected == true)
                        {
                           

                            btnConnect.Enabled = true;
                            btnConnect.Text = "disconnect";
                            tabControl_JobCtrl.Enabled = true;
                            groupBox_UR.Enabled = true;
                            groupBox_LADXY.Enabled = true;

                            onSuscribe_RobotsStatus();

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
                    //ROSDisconnect();
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

                btnConnect.Text = "connect";
                btnConnect.Enabled = true;
                Data.Instance.socket = null;
                tabControl_JobCtrl.Enabled = false;
                groupBox_UR.Enabled = false;
                groupBox_LADXY.Enabled = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ROSDisconnect err = " + ex.Message.ToString());
            }
        }

        #endregion
        
        int nTotalJobTablepos_index = 0;
        int nWaitingTablepos_index = 0;
        int nJobTablepos_index = 0;
        int nTotalJobTableEndpos_index=0;
        int nWaitingTableEndpos_index = 0;

        string m_strRobot_Status_File = "";
        string m_strJobschedule_File = "";
        string m_strWaitPosJob_File = "";
        string m_strJobPosJob_File = "";
        string m_strMissionList_File = "";
        string m_strLog_File = "";
        string m_strRobot_List_File = "";

        public List<string> G_robotList = new List<string>();

        Dictionary<string, string> JobSchedule_FileMatching = new Dictionary<string, string>();


        private void WorkOrderForm_Load(object sender, EventArgs e)
        {
#if _workorder
            Data.Instance.MAINFORM = this;
#endif

            worker = new Worker(this, 1);

            m_strRobot_Status_File = "..\\Ros_info\\RobotJobStatus.txt";// Application.StartupPath + "\\RobotJobStatus.txt";
            m_strJobschedule_File = "..\\Ros_info\\jobschedule.txt";
            m_strWaitPosJob_File = "..\\Ros_info\\waitpos_job.txt";
            m_strMissionList_File = "..\\Ros_info\\missionlevel.txt";

            string strsubdir_log = "log";
            DirectoryInfo dir = new DirectoryInfo(strsubdir_log);
            if(dir.Exists== false)
            {
                dir.Create();
            }

            worker.workpos_Evt += new Worker.WorkPosComplete(this.WorkresultComlete);
            worker.looppos_Evt += new Worker.LoopPosComplete(this.Workresult_Loop_Comlete);

            onJobScheduleCheck();
            onMissionLevelCheck();

            // nWaitingTableEndpos_index = 3;
            m_strRobot_List_File = "..\\Ros_info\\RobotList.txt";

            onRobotFile_Open();

            UI_Updatetimer.Interval = 250;
            UI_Updatetimer.Enabled = true;

            Robotstatus_Updatetimer.Interval = 100;
            Robotstatus_Updatetimer.Enabled = true;

            timer_runnigTime.Enabled = false;

            numericUpDown_RepeatCnt.Value = 500;
            numericUpDown_RepeatTime.Value = 5;

            tabControl_JobCtrl.Enabled = false;
            groupBox_UR.Enabled = false;
            groupBox_LADXY.Enabled = false;
            onStopPauseRestart_disable();

            
            #region 수동조작 탭 초기화
            cboRobotID.SelectedIndex = 0;


            radioButton_sen1.Checked = true;
            #endregion

            onRobots_WorkInfo_InitSet();


            cboURRobot.SelectedIndex = 6;

            Data.Instance.XIS_Status_Info.Clear();

            cboR005_lift.SelectedIndex = 0;
            cboR006_lift.SelectedIndex = 0;

          //  timer_palletCheck.Interval = 100;
          //  timer_palletCheck.Enabled = true;

            onDistTable_initial();

           // timer_makeDistTable.Interval = 10;
          //  timer_makeDistTable.Enabled = true;

            timer_CrashCheck.Interval = 50;
            timer_CrashCheck.Enabled = true;

            onUR_Abstacle_Tableinitial();

            timer_URAbstacle.Interval = 1000*2;
            

            timer_URAbstacleTable.Interval = 100;

            timer_URAbstacleTable.Enabled = true;

            chk005Use.Checked = true;
            chk006Use.Checked = true;
            chk007Use.Checked = false;
            chk008Use.Checked = true;

            cboOneRobot.SelectedIndex = 0;

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


            worker.Globalpath_Evt += new Worker.GlobalpathComplete(this.GlobalpathComplete);

            radioButton_p2.Checked = true;

            radioButton_123.Checked = true;

            radioButton_allin.Checked = true;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0312:
                    Keys key = (Keys)(((int)m.LParam >> 16) & 0xffff);
                    KeyModifiers modifier = (KeyModifiers)((int)m.LParam & 0xffff);

                    if((KeyModifiers.Control) == modifier)
                    {
                        if (Data.Instance.isConnected)
                        {
                            if (Keys.D1 == key)
                            {
                                UnregisterHotKey(this.Handle, 31211 + 1);
                                Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                workinfo.Add("R_005", "");
                                onJobPause(workinfo);
                                RegisterHotKey(this.Handle, 31211, KeyModifiers.Control, Keys.D1);
                            }
                            else if (Keys.D2 == key)
                            {
                                UnregisterHotKey(this.Handle, 31221 + 1);
                                Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                workinfo.Add("R_006", "");
                                onJobPause(workinfo);
                                RegisterHotKey(this.Handle, 31221, KeyModifiers.Control, Keys.D2);
                            }
                            else if (Keys.D3 == key)
                            {
                                UnregisterHotKey(this.Handle, 31231 + 1);
                                Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                workinfo.Add("R_008", "");
                                onJobPause(workinfo);
                                RegisterHotKey(this.Handle, 31231, KeyModifiers.Control, Keys.D3);
                            }

                            if (Keys.End == key)
                            {
                                UnregisterHotKey(this.Handle, 31111 + 1);
                                Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                workinfo.Add("R_005", "");
                                onJobPause(workinfo);
                                RegisterHotKey(this.Handle, 31111, KeyModifiers.Control, Keys.End);
                            }
                            else if (Keys.Down == key)
                            {
                                UnregisterHotKey(this.Handle, 31121 + 1);
                                Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                workinfo.Add("R_006", "");
                                onJobPause(workinfo);
                                RegisterHotKey(this.Handle, 31121, KeyModifiers.Control, Keys.Down);
                            }
                            else if (Keys.PageDown == key)
                            {
                                UnregisterHotKey(this.Handle, 31131 + 1);
                                Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                workinfo.Add("R_008", "");
                                onJobPause(workinfo);
                                RegisterHotKey(this.Handle, 31131, KeyModifiers.Control, Keys.PageDown);
                            }


                            if (Keys.Home == key)
                            {
                                UnregisterHotKey(this.Handle, 31511 + 1);
                                Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                workinfo.Add("R_005", "");
                                onJobRestart(workinfo);
                                RegisterHotKey(this.Handle, 31511, KeyModifiers.Control, Keys.Home);
                            }
                            else if (Keys.Up == key)
                            {
                                UnregisterHotKey(this.Handle, 31621 + 1);
                                Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                workinfo.Add("R_006", "");
                                onJobRestart(workinfo);
                                RegisterHotKey(this.Handle, 31621, KeyModifiers.Control, Keys.Up);
                            }
                            else if (Keys.PageUp == key)
                            {
                                UnregisterHotKey(this.Handle, 31731 + 1);
                                Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                workinfo.Add("R_008", "");
                                onJobRestart(workinfo);
                                RegisterHotKey(this.Handle, 31731, KeyModifiers.Control, Keys.PageUp);
                            }

                            if (Keys.A == key)
                            {
                                UnregisterHotKey(this.Handle, 31831 + 1);
                                Twist data = new Twist();
                                data.linear.x = 0;
                                data.angular.z = 0.5;
                                onManualRun(data,"R_006");

                            
                                RegisterHotKey(this.Handle, 31831, KeyModifiers.Control, Keys.A);
                            }

                            if (Keys.S == key)
                            {
                                UnregisterHotKey(this.Handle, 31931 + 1);

                                Twist data = new Twist();
                                data.linear.x = 0;
                                data.angular.z = 0;
                                onManualRun(data, "R_006");


                                RegisterHotKey(this.Handle, 31931, KeyModifiers.Control, Keys.S);
                            }
                        }
                    }
                 

                    break;
            }

            base.WndProc(ref m);
        }



        /// <summary>
        /// 등록된 로봇 리스트 읽어오기
        /// </summary>
        public void onRobotList_Open()
        {
            try
            {
                if (!File.Exists(m_strRobot_List_File))
                {
                    using (StreamWriter sw = new System.IO.StreamWriter(m_strRobot_List_File, false, Encoding.Default))
                    {
                        sw.WriteLine("robot_id,robot_name");
                        sw.Close();
                    }
                    //  return;
                }

                // dataGridView1.Rows.Clear();


                using (StreamReader sr1 = new System.IO.StreamReader(m_strRobot_List_File, Encoding.Default))
                {
                    int ncnt = 0; //파일에 첫줄은 항목명으로 빼고 읽기 위해 선언
                    Data.Instance.Robot_status_info.Clear();

                    G_robotList.Clear();

                    while (sr1.Peek() >= 0)
                    {
                        string strTemp = sr1.ReadLine();
                        if (ncnt != 0)
                        {
                            string[] strRobotlist = strTemp.Split(',');
                            //    dataGridView1.Rows.Add(strRobotstatus);
                            G_robotList.Add(strRobotlist[0]);
                        }
                        ncnt++;
                    }
                }            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobotList_Open err :={0}", ex.Message.ToString());
            }
        }
        /// <summary>
        /// 등록된 로봇의 워크 정보를 초기화 한다.
        /// </summary>
        public void onRobots_WorkInfo_InitSet()
        {
            try
            {
                Data.Instance.Robot_work_info.Clear();
                foreach (KeyValuePair<string, string> info in Data.Instance.Robot_status_info)
                {
                    string key = info.Key;
                    string value = info.Value;
                    string[] strvalue = value.Split(',');
                    string strrobotid = strvalue[0];
                    Data.Instance.Robot_work_info.Add(strrobotid, worker.onNewRobotWorkInfo_initial(strrobotid, "", 1, 0, "", ""));
                }
              /*      if (G_robotList.Count > 0)
                {
                    Data.Instance.Robot_work_info.Clear();

                    for (int i = 0; i < G_robotList.Count; i++)
                    {
                        string strrobotid = G_robotList[i];
                        Data.Instance.Robot_work_info.Add(strrobotid, worker.onNewRobotWorkInfo_initial(strrobotid, "", 1, 0, "", ""));
                    }
                }*/            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobots_WorkInfo_InitSet err :={0}", ex.Message.ToString());
            }
        }

        private void onSuscribe_RobotsStatus()
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    Dictionary<string, string> robotinfo = Data.Instance.Robot_status_info;
                    //foreach (KeyValuePair<string, string> info in robotinfo)
                    string[] strrobot = { "R_005", "R_006", "R_007", "R_008" };
                    for(int i=0; i<4; i++)
                    {
                        //string key = info.Key;
                        //string value = info.Value;
                        //string[] strvalue=value.Split(',');
                        //string strrobotid = strvalue[0];
                        string strrobotid = strrobot[i];
                        worker.onSelectRobotStatus_subscribe(strrobotid);
                    }

                    worker.onSelectXIS_subscribe();
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSuscribe_RobotsStatus err :={0}", ex.Message.ToString());
            }
        }



        private void onMissionLevelCheck()
        {
            Data.Instance.missionlist_table.missioninfo = new List<MisssionInfo>();
            try
            {
                if (!File.Exists(m_strMissionList_File))
                {
                    using (StreamWriter sw = new System.IO.StreamWriter(m_strMissionList_File, false, Encoding.Default))
                    {
                        sw.WriteLine("work id,work_name,work level");
                        sw.Close();
                    }
                    //  return;
                }

                using (StreamReader sr1 = new System.IO.StreamReader(m_strMissionList_File, Encoding.Default))
                {
                    int ncnt = 0; //파일에 첫줄은 항목명으로 빼고 읽기 위해 선언
                    while (sr1.Peek() >= 0)
                    {
                        string strTemp = sr1.ReadLine();
                        if (ncnt != 0)
                        {
                            string[] strjobinfo = strTemp.Split(',');

                            MisssionInfo missioninfo = new MisssionInfo();
                            missioninfo.strMisssionID = strjobinfo[0];
                            missioninfo.strMisssionName = strjobinfo[1];
                            missioninfo.nMisssionLevel = int.Parse(strjobinfo[2]);

                            Data.Instance.missionlist_table.missioninfo.Add(missioninfo);
                        }
                        ncnt++;
                    }
                }            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onMissionLevelCheck err :={0}", ex.Message.ToString());
            }
        }

        private void onJobScheduleCheck()
        {

            try
            {
                if (!File.Exists(m_strJobschedule_File))
                {
                    using (StreamWriter sw = new System.IO.StreamWriter(m_strJobschedule_File, false, Encoding.Default))
                    {
                        sw.WriteLine("jobname,jobtype");
                        sw.Close();
                    }
                    //  return;
                }

                dataGridView_Jobschedule.Rows.Clear();
                JobSchedule_FileMatching.Clear();

                using (StreamReader sr1 = new System.IO.StreamReader(m_strJobschedule_File, Encoding.Default))
                {
                    int ncnt = 0; //파일에 첫줄은 항목명으로 빼고 읽기 위해 선언
                    Data.Instance.totalJobschedule.jobInfo = new List<JobInfo>();
                    while (sr1.Peek() >= 0)
                    {
                        string strTemp = sr1.ReadLine();
                        int ncomment = 0;

                        ncomment = strTemp.IndexOf("//");

                        if (ncnt != 0 && ncomment < 0)
                        {
                            string[] strjobinfo = strTemp.Split(',');
                            JobInfo jobinfo = new JobInfo();
                            jobinfo.strJobname = strjobinfo[0];
                            if(strjobinfo[1]=="순서")
                                jobinfo.nJobType = (int)JOB_TYPE.sequence;
                            else if (strjobinfo[1] == "동시")
                                jobinfo.nJobType = (int)JOB_TYPE.synchronous;

                            Data.Instance.totalJobschedule.jobInfo.Add(jobinfo);

                            JobSchedule_FileMatching.Add(strjobinfo[0], strjobinfo[2]);

                            dataGridView_Jobschedule.Rows.Add(strjobinfo);
                            
                            
                        }
                        ncnt++;
                    }
                }            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onJobScheduleCheck err :={0}", ex.Message.ToString());
            }


            nTotalJobTableEndpos_index = Data.Instance.totalJobschedule.jobInfo.Count() ;
           
        }

        public void onRobotFile_Open()
        {
            try
            {
                if (!File.Exists(m_strRobot_Status_File))
                {
                    using (StreamWriter sw = new System.IO.StreamWriter(m_strRobot_Status_File, false, Encoding.Default))
                    {
                        sw.WriteLine("robot_id,ip,work id,work status,work cnt,curr work cnt,actionidx,lift");
                        sw.WriteLine("R_001,192.168.0.10,,wait,1,1,0,stop");
                        sw.WriteLine("R_002,192.168.0.10,,wait,1,1,0,stop");
                        sw.WriteLine("R_003,192.168.0.10,,wait,1,1,0,stop");
                        sw.WriteLine("R_004,192.168.0.10,,wait,1,1,0,stop");
                        sw.WriteLine("R_005,192.168.0.10,,wait,1,1,0,stop");
                        sw.WriteLine("R_006,192.168.0.10,,wait,1,1,0,stop");
                        sw.WriteLine("R_007,192.168.0.10,,wait,1,1,0,stop");
                        sw.WriteLine("R_008,192.168.0.10,,wait,1,1,0,stop");
                        sw.Close();
                    }
                    //  return;
                }

                dataGridView1.Rows.Clear();


                using (StreamReader sr1 = new System.IO.StreamReader(m_strRobot_Status_File, Encoding.Default))
                {
                    int ncnt = 0; //파일에 첫줄은 항목명으로 빼고 읽기 위해 선언
                    Data.Instance.Robot_status_info.Clear();

                    while (sr1.Peek() >= 0)
                    {
                        string strTemp = sr1.ReadLine();
                        if (ncnt != 0)
                        {
                            string[] strRobotstatus = strTemp.Split(',');
                            dataGridView1.Rows.Add(strRobotstatus);



                            Data.Instance.Robot_status_info.Add(strRobotstatus[0], strTemp);
                        }
                        ncnt++;
                    }
                }            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobotFile_Open err :={0}", ex.Message.ToString());
            }
        }

        private void Robotstatus_Updatetimer_Tick(object sender, EventArgs e)
        {
            try
            {
                using (StreamWriter sw = new System.IO.StreamWriter(m_strRobot_Status_File, false, Encoding.Default))
                {
                    sw.WriteLine("robot_id,ip,work id,work status,work cnt,curr work cnt,actionidx,lift");

                    foreach (KeyValuePair<string, string> info in Data.Instance.Robot_status_info)
                    {
                        string key = info.Key;
                        string value = info.Value;

                        sw.WriteLine(value);
                    }

                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Robotstatus_Updatetimer_Tick err :={0}", ex.Message.ToString());
            }
        }

        private void UI_Updatetimer_Tick(object sender, EventArgs e)
        {
            Robotstatus_Updatetimer.Enabled = false;
            onRobotStatusDP_Update();
            Robotstatus_Updatetimer.Enabled = true;
        }

        public void onRobotStatusDP_Update()
        {
            try
            {

                for (int i = 0; i < Data.Instance.Robot_status_info.Count; i++)
                {
                    string strrobot = dataGridView1["robotid", i].Value.ToString();
                    if (Data.Instance.Robot_status_info.ContainsKey(strrobot))
                    {
                        string strtemp = Data.Instance.Robot_status_info[strrobot];
                        string[] strRobotstatus = strtemp.Split(',');

                        dataGridView1["workid", i].Value = strRobotstatus[2];
                        dataGridView1["status", i].Value = strRobotstatus[3];
                        dataGridView1["Workcnt", i].Value = strRobotstatus[4];
                        dataGridView1["CurrWorkcnt", i].Value = strRobotstatus[5];
                        dataGridView1["actionidx", i].Value = strRobotstatus[6];
                        dataGridView1["lift_state", i].Value = strRobotstatus[7];
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobotStatusDP_Update err :={0}", ex.Message.ToString());
            }
        }

        #region Button
        private void btnConnect_Click(object sender, EventArgs e)
        {
            string strAddr = txtAddr.Text.ToString();

            btnConnect.Enabled = false;
            ROSConnection(strAddr);
        }

        private void btnWaitPos_Click(object sender, EventArgs e)
        {
            onStopPauseRestart_disable();
            if (Data.Instance.isConnected)
            {
                try
                {
                    using (StreamWriter sw = new System.IO.StreamWriter(m_strJobschedule_File, false, Encoding.Default))
                    {
                        sw.WriteLine("jobname,jobtype");
                        sw.WriteLine("대기위치1,순서,waitpos_job.txt");
                        sw.Close();
                    }
                    onJobScheduleCheck();
                    //onWaitingpos_RobotInfo_Check();

                    Data.Instance.robots_currgoing_status.Clear();
                    nTotalJobTablepos_index = 0;
                    nTotalJobTableEndpos_index = 1;

                    onSequencepos_RobotInfo_Check(nTotalJobTablepos_index);

                    nWaitingTableEndpos_index = Data.Instance.waitingpos_table.waitpos_robotinfo.Count();

                    int ncnt = Data.Instance.waitingpos_table.waitpos_robotinfo.Count();
                    string strMsg = "";
                    for (int i = 0; i < ncnt; i++)
                    {
                        string strrobotid = Data.Instance.waitingpos_table.waitpos_robotinfo[i].strRobotID;
                        string strrobotname = Data.Instance.waitingpos_table.waitpos_robotinfo[i].strRobotName;

                        strMsg += string.Format("{0}({1}) ", strrobotname, strrobotid);
                    }

                    strMsg += " 대기위치로 이동하시겠습니까?";

                    if (DialogResult.OK == MessageBox.Show(strMsg, "확인", MessageBoxButtons.OKCancel))
                    {
                       
                       


                        onBtn_enable(false);
                        m_strLog_File = DateTime.Now.ToString("yyyyMMdd") + ".txt";
                        onInitPosMove();
                        
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("btnWaitPos_Click" + ex.Message.ToString());
                }
            }
        }

        private void btnInitPosMove_Click(object sender, EventArgs e)
        {
            //onSequencepos_RobotInfo_Check(nTotalJobTablepos_index);
            onStopPauseRestart_disable();
            if (Data.Instance.isConnected)
            {
                try
                {
                    using (StreamWriter sw = new System.IO.StreamWriter(m_strJobschedule_File, false, Encoding.Default))
                    {
                        sw.WriteLine("jobname,jobtype");
                        sw.WriteLine("정렬,순서,centerpos_job.txt");
                        sw.Close();
                    }
                    onJobScheduleCheck();

                    //onWaitingpos_RobotInfo_Check();

                    Data.Instance.robots_currgoing_status.Clear();
                    nTotalJobTablepos_index = 0;
                    nTotalJobTableEndpos_index = 1;
                    
                    onSequencepos_RobotInfo_Check(nTotalJobTablepos_index);

                    nWaitingTableEndpos_index = Data.Instance.waitingpos_table.waitpos_robotinfo.Count();

                    int ncnt = Data.Instance.waitingpos_table.waitpos_robotinfo.Count();
                    string strMsg = "";
                    for (int i = 0; i < ncnt; i++)
                    {
                        string strrobotid = Data.Instance.waitingpos_table.waitpos_robotinfo[i].strRobotID;
                        string strrobotname = Data.Instance.waitingpos_table.waitpos_robotinfo[i].strRobotName;

                        strMsg += string.Format("{0}({1}) ", strrobotname, strrobotid);
                    }

                    strMsg += " 정렬위치로 이동하시겠습니까?";

                   if(DialogResult.OK == MessageBox.Show(strMsg, "확인", MessageBoxButtons.OKCancel))
                    {
                        
                        

                        onBtn_enable(false);
                        m_strLog_File = DateTime.Now.ToString("yyyyMMdd") + ".txt";
                        onInitPosMove();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("btnInitPosMove_Click"+ex.Message.ToString());
                }
            }
        }

        private void btnStartPos_Click(object sender, EventArgs e)
        {
            onStopPauseRestart_disable();
            if (Data.Instance.isConnected)
            {
                try
                {
                    using (StreamWriter sw = new System.IO.StreamWriter(m_strJobschedule_File, false, Encoding.Default))
                    {
                        sw.WriteLine("jobname,jobtype");
                        sw.WriteLine("시작위치,순서,startpos_job.txt");
                        sw.Close();
                    }
                    onJobScheduleCheck();

                    //onWaitingpos_RobotInfo_Check();

                    Data.Instance.robots_currgoing_status.Clear();
                    nTotalJobTablepos_index = 0;
                    nTotalJobTableEndpos_index = 1;

                    onSequencepos_RobotInfo_Check(nTotalJobTablepos_index);

                    nWaitingTableEndpos_index = Data.Instance.waitingpos_table.waitpos_robotinfo.Count();

                    int ncnt = Data.Instance.waitingpos_table.waitpos_robotinfo.Count();
                    string strMsg = "";
                    for (int i = 0; i < ncnt; i++)
                    {
                        string strrobotid = Data.Instance.waitingpos_table.waitpos_robotinfo[i].strRobotID;
                        string strrobotname = Data.Instance.waitingpos_table.waitpos_robotinfo[i].strRobotName;

                        strMsg += string.Format("{0}({1}) ", strrobotname, strrobotid);
                    }

                    strMsg += " 시작위치로 이동하시겠습니까?";

                    if (DialogResult.OK == MessageBox.Show(strMsg, "확인", MessageBoxButtons.OKCancel))
                    {
                       
                        


                        onBtn_enable(false);
                        m_strLog_File = DateTime.Now.ToString("yyyyMMdd") + ".txt";
                        onInitPosMove();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("btnStartPos_Click" + ex.Message.ToString());
                }
            }
        }

        int nRuntime = 0;

        bool bRuntime_complete = false;
        private void btnJob1_Click(object sender, EventArgs e)
        {
            onStopPauseRestart_disable();
            if (Data.Instance.isConnected)
            {
                try
                {
                    // onJobpos_RobotInfo_Check();
                    using (StreamWriter sw = new System.IO.StreamWriter(m_strJobschedule_File, false, Encoding.Default))
                    {
                        sw.WriteLine("jobname,jobtype");
                        sw.WriteLine("작업1,동시,job1pos_job.txt");
                        sw.Close();
                    }
                    onJobScheduleCheck();

                    Data.Instance.robots_currgoing_status.Clear();

                    nTotalJobTablepos_index = 0;
                    nTotalJobTableEndpos_index = 1;  //동시작업은 인덱스가 1이다.
                                                     // nTotalJobTableEndpos_index = Data.Instance.jobpos_table.jobpos_robotinfo.Count();

                    onSynchronous_RobotInfo_Check(nTotalJobTablepos_index);
                    int ncnt = Data.Instance.jobpos_table.jobpos_robotinfo.Count();
                    string strMsg = "";
                    for (int i = 0; i < ncnt; i++)
                    {
                        string strrobotid = Data.Instance.jobpos_table.jobpos_robotinfo[i].strRobotID;
                        string strrobotname = Data.Instance.jobpos_table.jobpos_robotinfo[i].strRobotName;
                        string strrobotcnt = string.Format("{0}",Data.Instance.jobpos_table.jobpos_robotinfo[i].nCnt);

                        strMsg += string.Format("{0}({1},반복({2}) ", strrobotname, strrobotid,strrobotcnt);
                    }

                    strMsg += " 동작 하시겠습니까?";

                    if (DialogResult.OK == MessageBox.Show(strMsg, "확인", MessageBoxButtons.OKCancel))
                    {
                        
                        

                        onBtn_enable(false);
                        m_strLog_File = DateTime.Now.ToString("yyyyMMdd") +".txt";

                        onJobMove(true);
                    }


                }
                catch (Exception ex)
                {
                    onListmsg(ex.Message.ToString());
                }
            }
        }
        bool bCombine_RunFirst = false;
        private void btnRun_Click(object sender, EventArgs e)
        {
            onDistTable_initial();

            onStopPauseRestart_disable();

            bCombine_RunFirst = true;

            if (Data.Instance.isConnected)
            {
                try
                {
                    //   onWaitingpos_RobotInfo_Check();

                    if (radioButton_sen1.Checked)
                    {
                        using (StreamWriter sw = new System.IO.StreamWriter(m_strJobschedule_File, false, Encoding.Default))
                        {
                            sw.WriteLine("jobname,jobtype");
                            sw.WriteLine("모여,순서,gatheringpos_job.txt");
                            sw.WriteLine("헤처,순서,plowingpos_job.txt");
                            sw.WriteLine("작업1,동시,job1pos_job.txt");
                            sw.Close();
                        }
                        onJobScheduleCheck();

                        Data.Instance.robots_currgoing_status.Clear();
                        nTotalJobTablepos_index = 0;
                        nTotalJobTableEndpos_index = Data.Instance.totalJobschedule.jobInfo.Count();


                        int ncnt = Data.Instance.totalJobschedule.jobInfo.Count();
                        string strMsg = "";
                        for (int i = 0; i < ncnt; i++)
                        {
                            string strjobname = Data.Instance.totalJobschedule.jobInfo[i].strJobname;

                            strMsg += string.Format("{0} , ", strjobname);
                        }

                        strMsg += "을 동작 하시겠습니까?";

                        if (DialogResult.OK == MessageBox.Show(strMsg, "확인", MessageBoxButtons.OKCancel))
                        {
                            m_strLog_File = DateTime.Now.ToString("yyyyMMdd") + ".txt";

                            onBtn_enable(false);



                            if (Data.Instance.totalJobschedule.jobInfo[nTotalJobTablepos_index].nJobType == (int)JOB_TYPE.sequence)
                            {
                                onSequencepos_RobotInfo_Check(nTotalJobTablepos_index);
                                nWaitingTableEndpos_index = Data.Instance.waitingpos_table.waitpos_robotinfo.Count();
                                onInitPosMove();
                                
                            }
                            else if (Data.Instance.totalJobschedule.jobInfo[nTotalJobTablepos_index].nJobType == (int)JOB_TYPE.synchronous)
                            {
                                onSynchronous_RobotInfo_Check(nTotalJobTablepos_index);
                            }
                        }

                        #region test
                        /*    using (StreamWriter sw = new System.IO.StreamWriter(m_strJobschedule_File, false, Encoding.Default))
                            {
                                sw.WriteLine("jobname,jobtype");
                              //  sw.WriteLine("대기위치1,순서,waitpos_job.txt");
                                sw.WriteLine("정렬,순서,centerpos_job.txt");
                              //  sw.WriteLine("시작위치,순서,startpos_job.txt");
                                sw.WriteLine("작업1,동시,job1pos_job.txt");
                                sw.Close();
                            }
                            onJobScheduleCheck();

                            Data.Instance.robots_currgoing_status.Clear();
                            nTotalJobTablepos_index = 0;
                            nTotalJobTableEndpos_index = Data.Instance.totalJobschedule.jobInfo.Count();


                            int ncnt = Data.Instance.totalJobschedule.jobInfo.Count();
                            string strMsg = "";
                            for (int i = 0; i < ncnt; i++)
                            {
                                string strjobname = Data.Instance.totalJobschedule.jobInfo[i].strJobname;

                                strMsg += string.Format("{0} , ", strjobname);
                            }

                            strMsg += "을 동작 하시겠습니까?";

                            if (DialogResult.OK == MessageBox.Show(strMsg, "확인", MessageBoxButtons.OKCancel))
                            {
                                m_strLog_File = DateTime.Now.ToString("yyyyMMdd") + ".txt";

                                onBtn_enable(false);



                                if (Data.Instance.totalJobschedule.jobInfo[nTotalJobTablepos_index].nJobType == (int)JOB_TYPE.sequence)
                                {
                                    onSequencepos_RobotInfo_Check(nTotalJobTablepos_index);
                                    nWaitingTableEndpos_index = Data.Instance.waitingpos_table.waitpos_robotinfo.Count();
                                    onInitPosMove();
                                }
                                else if (Data.Instance.totalJobschedule.jobInfo[nTotalJobTablepos_index].nJobType == (int)JOB_TYPE.synchronous)
                                {
                                    onSynchronous_RobotInfo_Check(nTotalJobTablepos_index);
                                    onJobMove(true);
                                }
                            }
                            */
                        #endregion
                    }
                    else if (radioButton_sen2.Checked)
                    {
                        MessageBox.Show("팔로우");
                   
                    }

                    
                }
                catch (Exception ex)
                {
                    onListmsg(ex.Message.ToString());
                }
            }
        }

        private void btnPauseRestart_Click(object sender, EventArgs e)
        {
            btnPauseRestart.Enabled = false;
            onStopPauseRestart_disable();
            if (Data.Instance.isConnected)
            {
                try
                {
                    Dictionary<string, string> workinfo = new Dictionary<string, string>();
                    int ncnt = Data.Instance.Robot_work_info.Count(); //Data.Instance.robots_currgoing_status.Count();
                    for (int i = 0; i < ncnt; i++)
                    {
                        KeyValuePair<string, Robot_WorkKInfo> robot_info = Data.Instance.Robot_work_info.ElementAt(i);
                        workinfo.Add(robot_info.Value.strRobotID, "");
                    }

                    if (Data.Instance.robots_currgoing_status.Count() > 0)
                    {
                        if (btnPauseRestart.Text == "일시정지")
                        {
                            onJobPause(workinfo);
                            
                            btnPauseRestart.Text = "재시작";
                        }
                        else
                        {
                            onJobRestart(workinfo);
                            btnPauseRestart.Text = "일시정지";
                        }
                        Thread.Sleep(100);
                    }

                    btnPauseRestart.Enabled = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("btnPauseRestart_Click err" +ex.Message.ToString());
                }
            }
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            bRobotMissioning = false;

            onStopPauseRestart_disable();

            if (Data.Instance.isConnected)
            {
                try
                {
                    Dictionary<string, string> workinfo = new Dictionary<string, string>();

                    int ncnt = Data.Instance.Robot_work_info.Count(); //Data.Instance.robots_currgoing_status.Count();

                    if (ncnt > 0)
                    {
                        for (int i = 0; i < ncnt; i++)
                        {
                            KeyValuePair<string, Robot_WorkKInfo> robot_info = Data.Instance.Robot_work_info.ElementAt(i);
                            workinfo.Add(robot_info.Value.strRobotID, "");
                        }
                        onJobStop(workinfo);
                    }
                    else
                    {
                       
                        workinfo.Add("R_005", "");
                        workinfo.Add("R_006", "");
                        //workinfo.Add("R_007", "");
                        workinfo.Add("R_008", "");
                        onJobStop(workinfo);

                        Thread.Sleep(200);
                        onJobStop(workinfo);


                        /*  if (Data.Instance.Robot_work_info["R_005"].robot_status_info.workfeedback.msg.status.status!=2)
                          {
                              worker.onWorkCancel_publish("R_005", "");
                          }

                          if (Data.Instance.Robot_work_info["R_006"].robot_status_info.workfeedback.msg.status.status != 2)
                          {
                              worker.onWorkCancel_publish("R_006", "");
                          }

                          if (Data.Instance.Robot_work_info["R_008"].robot_status_info.workfeedback.msg.status.status != 2)
                          {
                              worker.onWorkCancel_publish("R_008", "");
                          }

                          if (Data.Instance.Robot_work_info["R_005"].robot_status_info.workfeedback.msg.status.status != 2)
                          {
                              worker.onWorkCancel_publish("R_005", "");
                          }
                          */
                        onBtn_enable(true);

                        MessageBox.Show("작업이 중지되었습니다.");

                        onStopAfterStartposMove_Chk();

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("btnStop_Click err ==" + ex.Message.ToString());
                }
            }
        }
        #endregion
        #region function
        private async void onJobPause(Dictionary<string, string> workinfo)
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    string strworkfile = "";
                    string strworkrobot = "";

                    foreach (KeyValuePair<string, string> info in workinfo)
                    {
                        strworkrobot = info.Key;
                        strworkfile = info.Value;

                        if (Data.Instance.Robot_work_info.ContainsKey(strworkrobot))
                        {
                            m_bPause = true;
                            string strgoal_id = Data.Instance.Robot_work_info[strworkrobot].strGoalid;

                            worker.onWorkPause_publish(strworkrobot, strgoal_id);
                            //var task = Task.Run(() => worker.onWorkPause_publish(strworkrobot, strgoal_id));
                            //await task;

                            //Task t = new Task(() => worker.onWorkPause_publish(strworkrobot, strgoal_id));
                            //t.Start();
                            //Thread.Sleep(100);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("onJobPause err ==" + ex.Message.ToString());
                }
            }
        }

        private async void onJobRestart(Dictionary<string, string> workinfo)
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    string strworkfile = "";
                    string strworkrobot = "";

                    foreach (KeyValuePair<string, string> info in workinfo)
                    {
                        strworkrobot = info.Key;
                        strworkfile = info.Value;

                        if (Data.Instance.Robot_work_info.ContainsKey(strworkrobot))
                        {
                            m_bRestart = true;
                            string strgoal_id = Data.Instance.Robot_work_info[strworkrobot].strGoalid;
                            worker.onWorkResume_publish(strworkrobot, strgoal_id);

                            //var task = Task.Run(() => worker.onWorkResume_publish(strworkrobot, strgoal_id));
                            //await task;

                           
                            //Thread.Sleep(100);

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("onJobRestart err ==" + ex.Message.ToString());
                }
            }
        }

        bool m_bStop = false;
        bool m_bPause = false;
        bool m_bRestart = false;

        private async void onJobStop(Dictionary<string, string> workinfo)
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    Invoke(new MethodInvoker(delegate ()
                    {
                        timer_runnigTime.Enabled = false;
                    }));

                    string strworkfile = "";
                    string strworkrobot = "";

                    foreach (KeyValuePair<string, string> info in workinfo)
                    {
                        strworkrobot = info.Key;
                        strworkfile = info.Value;

                        if (Data.Instance.Robot_work_info.ContainsKey(strworkrobot))
                        {
                            m_bStop = true;

                            string strgoal_id = Data.Instance.Robot_work_info[strworkrobot].strGoalid;
                            var task = Task.Run(() => worker.onWorkCancel_publish(strworkrobot, strgoal_id));
                            await task;

                            //Task t = new Task(() => worker.onWorkCancel_publish(strworkrobot, strgoal_id));
                            //t.Start();

                            Thread.Sleep(Data.Instance.nPulishDelayTime);

                            //worker.onDeleteSelectRobotSubscribe(strworkrobot);
                        }
                    }

                    onBtn_enable(true);

                    MessageBox.Show("작업이 중지되었습니다.");

                    onStopAfterStartposMove_Chk();


                }
                catch (Exception ex)
                {
                    Console.WriteLine("onJobStop err ==" + ex.Message.ToString());
                }
            }
        }

        private void onStopAfterStartposMove_Chk()
        {

            if (chkWaitPosMove.Checked)
            {
                onGatheringRobot(true);
            }
            else
            {

            }
        }

        private void timer_runnigTime_Tick(object sender, EventArgs e)
        {
            // bRuntime_complete = true;
            onRuntimeOver_JobStop();
        }

        private void onRuntimeOver_JobStop()
        {
            timer_runnigTime.Enabled = false;

            Dictionary<string, string> workinfo = new Dictionary<string, string>();
            int ncnt = Data.Instance.robots_currgoing_status.Count();
            for (int i = 0; i < ncnt; i++)
            {
                KeyValuePair<string, Robot_Going_Status> robot_info = Data.Instance.robots_currgoing_status.ElementAt(i);
                workinfo.Add(robot_info.Value.strRobotID, "");
            }

            if (Data.Instance.isConnected)
            {
                try
                {
                    string strworkfile = "";
                    string strworkrobot = "";

                    foreach (KeyValuePair<string, string> info in workinfo)
                    {
                        strworkrobot = info.Key;
                        strworkfile = info.Value;

                        if (Data.Instance.Robot_work_info.ContainsKey(strworkrobot))
                        {
                            m_bStop = true;
                            string strgoal_id = Data.Instance.Robot_work_info[strworkrobot].strGoalid;

                            worker.onWorkCancel_publish(strworkrobot, strgoal_id);
                            Thread.Sleep(Data.Instance.nPulishDelayTime);

                            //worker.onDeleteSelectRobotSubscribe(strworkrobot);

                        }
                    }
                    Thread.Sleep(100);
                    Data.Instance.robots_currgoing_status.Clear();

                    nWaitingTablepos_index = 0;
                    nTotalJobTablepos_index++;
                    onNextJobCheck();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("onRuntimeOver_JobStop err ==" + ex.Message.ToString());
                }
            }
        }

        double[,] gathering_posArray = new double[4, 2];
        double[,] plowing_posArray = new double[4, 2];
        double[,] robot_currPosArray = new double[4, 2];
        string[] pos_Robortname = { "R_008", "R_005", "R_006", "R_007" };
        enum Robot_CurrPosArray_Idx
        {
            R_005=0,
            R_006 ,
            R_007 ,
            R_008 ,
        };
        private void onRobotGather_Check()
        {
            try
            {
                #region gatheringposFile check
                string strGathering_Pos_File = "..\\Ros_info\\Robotgathering_Pos.txt";

                if (!File.Exists(strGathering_Pos_File))
                {
                    using (StreamWriter sw = new System.IO.StreamWriter(strGathering_Pos_File, false, Encoding.Default))
                    {
                        sw.WriteLine("1pos:0,0");
                        sw.WriteLine("2pos:0,0");
                        sw.WriteLine("3pos:0,0");
                        sw.WriteLine("4pos:0,0");
                        sw.Close();
                    }
                }

                using (StreamReader sr1 = new System.IO.StreamReader(strGathering_Pos_File, Encoding.Default))
                {
                    int ncnt = 0;
                    while (sr1.Peek() >= 0)
                    {
                        string strTemp = sr1.ReadLine();

                        string[] strdata = strTemp.Split(':');
                        string [] strsubdata= strdata[1].Split(',');

                        gathering_posArray[ncnt, 0] = double.Parse(strsubdata[0]);
                        gathering_posArray[ncnt, 1] = double.Parse(strsubdata[1]);

                        ncnt++;
                    }
                }
                #endregion


                pos_Robortname = new string[4];
                string strGathering_Pos_Job_File = "..\\Ros_info\\gatheringpos_job.txt";
                if (!File.Exists(strGathering_Pos_Job_File))
                {
                    using (StreamWriter sw = new System.IO.StreamWriter(strGathering_Pos_Job_File, false, Encoding.Default))
                    {
                        sw.WriteLine("robot_id,robot_name,work id, work_name, work_cnt, actidx");
                        sw.Close();
                    }
                }
                else
                {

                    //File.Delete(strGathering_Pos_Job_File);
                    string[] strrobotllist = { "R_005", "R_006", "R_007", "R_008" };

                    double curr_x = 0;
                    double curr_y = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        if (Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.robotstate.msg == null)
                        {
                            curr_x = 88;
                            curr_y = 88;
                        }
                        else
                        {
                            curr_x = Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.robotstate.msg.pose.x;
                            curr_y = Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.robotstate.msg.pose.y;
                        }

                        robot_currPosArray[i, 0] = curr_x;
                        robot_currPosArray[i, 1] = curr_y;
                    }

                    using (StreamWriter sw = new System.IO.StreamWriter(strGathering_Pos_Job_File,false, Encoding.Default))
                    {
                        sw.WriteLine("robot_id,robot_name,work id, work_name, work_cnt, actidx");

                        double gather_x = gathering_posArray[0, 0];
                        double gather_y = gathering_posArray[0, 1];

                        double dist1 = onPointToPointDist(gather_x, gather_y, robot_currPosArray[0, 0], robot_currPosArray[0, 1]);
                        double dist2 = onPointToPointDist(gather_x, gather_y, robot_currPosArray[1, 0], robot_currPosArray[1, 1]);
                        double dist3 = onPointToPointDist(gather_x, gather_y, robot_currPosArray[3, 0], robot_currPosArray[3, 1]);


                        string  strmsg = onRobotGathering_SubCheck(dist1, dist2, dist3, "gathering_1");
                        string[] strdata = strmsg.Split(':');
                        pos_Robortname[0] = strdata[0];
                        sw.WriteLine(strdata[1]); //1 위치 
                                              /*    if (chk008Use.Checked)
                                                      sw.WriteLine("R_008,300C,gathering_1,,1,0"); //1 위치 
                                                  else
                                                      sw.WriteLine("//R_008,300C,gathering_1,,1,0"); //1 위치 

                                                  pos_Robortname[0] = "R_008";
                                                  */



                        gather_x = gathering_posArray[1, 0];
                        gather_y = gathering_posArray[1, 1];

                        double curr_x1, curr_y1, curr_x2, curr_y2, curr_x3, curr_y3;
                        curr_x1 = robot_currPosArray[0, 0];
                        curr_x2 = robot_currPosArray[1, 0];
                        curr_x3 = robot_currPosArray[3, 0];

                        curr_y1 = robot_currPosArray[0, 1];
                        curr_y2 = robot_currPosArray[1, 1];
                        curr_y3 = robot_currPosArray[3, 1];

                        for (int i = 0; i < 4; i++)
                        {
                            if (pos_Robortname[i] == "R_005")
                            {
                                curr_x1 = 99;
                                curr_y1 = 99;
                            }
                            else if (pos_Robortname[i] == "R_006")
                            {
                                curr_x2 = 99;
                                curr_y2 = 99;
                            }
                            else if (pos_Robortname[i] == "R_008")
                            {
                                curr_x3 = 99;
                                curr_y3 = 99;
                            }
                        }

                        dist1 = onPointToPointDist(gather_x, gather_y, curr_x1, curr_y1);
                        dist2 = onPointToPointDist(gather_x, gather_y, curr_x2, curr_y2);
                        dist3 = onPointToPointDist(gather_x, gather_y, curr_x3, curr_y3);


                        strmsg = onRobotGathering_SubCheck(dist1, dist2, dist3, "gathering_2");
                        strdata = strmsg.Split(':');
                        pos_Robortname[1] = strdata[0];
                        sw.WriteLine(strdata[1]); //2 위치 


                        gather_x = gathering_posArray[2, 0];
                        gather_y = gathering_posArray[2, 1];

                        curr_x1 = robot_currPosArray[0, 0];
                        curr_x2 = robot_currPosArray[1, 0];
                        curr_x3 = robot_currPosArray[3, 0];

                        curr_y1 = robot_currPosArray[0, 1];
                        curr_y2 = robot_currPosArray[1, 1];
                        curr_y3 = robot_currPosArray[3, 1];

                        for (int i = 0; i < 4; i++)
                        {
                            if (pos_Robortname[i] == "R_005")
                            {
                                curr_x1 = 99;
                                curr_y1 = 99;
                            }
                            else if (pos_Robortname[i] == "R_006")
                            {
                                curr_x2 = 99;
                                curr_y2 = 99;
                            }
                            else if (pos_Robortname[i] == "R_008")
                            {
                                curr_x3 = 99;
                                curr_y3 = 99;
                            }
                        }

                        dist1 = onPointToPointDist(gather_x, gather_y, curr_x1, curr_y1);
                        dist2 = onPointToPointDist(gather_x, gather_y, curr_x2, curr_y2);
                        dist3 = onPointToPointDist(gather_x, gather_y, curr_x3, curr_y3);



                        strmsg = onRobotGathering_SubCheck(dist1, dist2, dist3, "gathering_3");
                        strdata = strmsg.Split(':');
                        pos_Robortname[2] = strdata[0];
                        sw.WriteLine(strdata[1]); ; //3 위치 


                        #region test
                      /*  gather_x =gathering_posArray[1, 0];
                        gather_y = gathering_posArray[1, 1];
                        //R_005, R_006만 체크
                        dist1 = onPointToPointDist(gather_x, gather_y, robot_currPosArray[0, 0], robot_currPosArray[0, 1]);
                        dist2 = onPointToPointDist(gather_x, gather_y, robot_currPosArray[1, 0], robot_currPosArray[1, 1]);

                        if (dist1 < dist2)
                        {
                            if (chk005Use.Checked)
                                sw.WriteLine("R_005,500_1,gathering_2,,1,0"); //2 위치 
                            else sw.WriteLine("//R_005,500_1,gathering_2,,1,0"); //2 위치

                            pos_Robortname[1] = "R_005";
                        }
                        else
                        {
                            if (chk006Use.Checked)
                                sw.WriteLine("R_006,500_2,gathering_2,,1,0"); //2 위치 
                            else
                                sw.WriteLine("R_006,500_2,gathering_2,,1,0"); //2 위치 

                            pos_Robortname[1] = "R_006";
                        }

                        if (pos_Robortname[1] == "R_005")
                        {
                            if (chk006Use.Checked)
                                sw.WriteLine("R_006,500_2,gathering_3,,1,0"); //3 위치 
                            else
                                sw.WriteLine("//R_006,500_2,gathering_3,,1,0"); //3 위치 

                            pos_Robortname[2] = "R_006";
                        }
                        else if (pos_Robortname[1] == "R_006")
                        {
                            if (chk005Use.Checked)
                                sw.WriteLine("R_005,500_1,gathering_3,,1,0"); //3 위치 
                            else
                                sw.WriteLine("//R_005,500_1,gathering_3,,1,0"); //3 위치 

                            pos_Robortname[2] = "R_005";
                        }
                        */
                        #endregion
                        if (chk007Use.Checked)
                            sw.WriteLine("R_007,100A,gathering_4,,1,0"); //4 위치 
                        else
                            sw.WriteLine("//R_007,100A,gathering_4,,1,0"); //4 위치 

                        pos_Robortname[3] = "R_007";

                        sw.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobotGather_Check err :={0}", ex.Message.ToString());
            }
        }

        private string onRobotGathering_SubCheck(double dist1,double dist2, double dist3, string strgatherfile)
        {
            string strmsg = "";
            string strrobotid = "", strrobotname = "";
            string pos_robotname = "";
            if (dist1 < dist2)
            {
                if (dist1 < dist3) //r005
                {
                    if (chk005Use.Checked)
                    {
                        strrobotid = "R_005";
                    }
                    else
                    {
                        strrobotid = "//R_005";
                    }
                    strrobotname = "500_1";
                    pos_robotname = "R_005";
                }
                else //r008
                {
                    if (chk008Use.Checked)
                    {
                        strrobotid = "R_008";
                    }
                    else
                    {
                        strrobotid = "//R_008";
                    }
                    strrobotname = "300C";
                    pos_robotname = "R_008";
                }
            }
            else
            {
                if (dist2 < dist3) //r006
                {
                    if (chk006Use.Checked)
                    {
                        strrobotid = "R_006";
                    }
                    else
                    {
                        strrobotid = "//R_006";
                    }
                    strrobotname = "600_1";
                    pos_robotname = "R_006";
                }
                else //r008
                {
                    if (chk008Use.Checked)
                    {
                        strrobotid = "R_008";
                    }
                    else
                    {
                        strrobotid = "//R_008";
                    }
                    strrobotname = "300C";
                    pos_robotname = "R_008";
                }
            }

           strmsg= string.Format("{0}:{1},{2},{3},,1,0", pos_robotname, strrobotid, strrobotname, strgatherfile);

            return strmsg;
        }

        private void onRobotPlowing_Check()
        {
            try
            {
                #region plowingposFile check
                string strPlowing_Pos_File = "..\\Ros_info\\Robotplowing_Pos.txt";

                if (!File.Exists(strPlowing_Pos_File))
                {
                    using (StreamWriter sw = new System.IO.StreamWriter(strPlowing_Pos_File, false, Encoding.Default))
                    {
                        sw.WriteLine("1pos:0,0");
                        sw.WriteLine("2pos:0,0");
                        sw.WriteLine("3pos:0,0");
                        sw.WriteLine("4pos:0,0");
                        sw.Close();
                    }
                }

                using (StreamReader sr1 = new System.IO.StreamReader(strPlowing_Pos_File, Encoding.Default))
                {
                    int ncnt = 0;
                    while (sr1.Peek() >= 0)
                    {
                        string strTemp = sr1.ReadLine();

                        string[] strdata = strTemp.Split(':');
                        string[] strsubdata = strdata[1].Split(',');

                        plowing_posArray[ncnt, 0] = double.Parse(strsubdata[0]);
                        plowing_posArray[ncnt, 1] = double.Parse(strsubdata[1]);

                        ncnt++;
                    }
                }
                #endregion

                string[] pos_Robortname2 = new string[4];


                string strPlowing_Pos_Job_File = "..\\Ros_info\\plowingpos_job.txt";
                if (!File.Exists(strPlowing_Pos_Job_File))
                {
                    using (StreamWriter sw = new System.IO.StreamWriter(strPlowing_Pos_Job_File, false, Encoding.Default))
                    {
                        sw.WriteLine("robot_id,robot_name,work id, work_name, work_cnt, actidx");
                        sw.Close();
                    }
                }
                else
                {
                    //File.Delete(strGathering_Pos_Job_File);
                    string[] strrobotllist = { "R_005", "R_006", "R_007", "R_008" };

                    double curr_x = 0;
                    double curr_y = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        if (Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.robotstate.msg == null)
                        {
                            curr_x = 88;
                            curr_y = 88;
                        }
                        else
                        {
                            curr_x = Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.robotstate.msg.pose.x;
                            curr_y = Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.robotstate.msg.pose.y;
                        }
                        robot_currPosArray[i, 0] = curr_x;
                        robot_currPosArray[i, 1] = curr_y;
                    }

                    using (StreamWriter sw = new System.IO.StreamWriter(strPlowing_Pos_Job_File, false, Encoding.Default))
                    {
                        int n005_actionidx = 0;
                        int n006_actionidx = 0;
                        int n008_actionidx = 0;

                        sw.WriteLine("robot_id,robot_name,work id, work_name, work_cnt, actidx");

                        if (chk007Use.Checked)
                            sw.WriteLine("R_007,100A,plowing_1,,1,0"); //1 위치 
                        else
                            sw.WriteLine("//R_007,100A,plowing_1,,1,0"); //1 위치 

                        pos_Robortname2[0] = "R_007";

                        double plow_x = plowing_posArray[1, 0];
                        double plow_y = plowing_posArray[1, 1];
                        double dist1 = onPointToPointDist(plow_x, plow_y, robot_currPosArray[0, 0], robot_currPosArray[0, 1]);
                        double dist2 = onPointToPointDist(plow_x, plow_y, robot_currPosArray[1, 0], robot_currPosArray[1, 1]);
                        double dist3 = onPointToPointDist(plow_x, plow_y, robot_currPosArray[3, 0], robot_currPosArray[3, 1]);
                        
                        string strmsg = onRobotGathering_SubCheck(dist1, dist2, dist3, "plowing_2");
                        string [] strdata = strmsg.Split(':');
                        pos_Robortname2[1] = strdata[0];
                        sw.WriteLine(strdata[1]);  //2 위치 

                        double curr_x1, curr_y1, curr_x2, curr_y2, curr_x3, curr_y3;

                        
                        plow_x = plowing_posArray[2, 0];
                        plow_y = plowing_posArray[2, 1];

                        curr_x1 = robot_currPosArray[0, 0];
                        curr_x2 = robot_currPosArray[1, 0];
                        curr_x3 = robot_currPosArray[3, 0];

                        curr_y1 = robot_currPosArray[0, 1];
                        curr_y2 = robot_currPosArray[1, 1];
                        curr_y3 = robot_currPosArray[3, 1];

                        for (int i = 0; i < 4; i++)
                        {
                            if (pos_Robortname2[i] == "R_005")
                            {
                                curr_x1 = 99;
                                curr_y1 = 99;
                            }
                            else if (pos_Robortname2[i] == "R_006")
                            {
                                curr_x2 = 99;
                                curr_y2 = 99;
                            }
                            else if (pos_Robortname2[i] == "R_008")
                            {
                                curr_x3 = 99;
                                curr_y3 = 99;
                            }
                        }

                        dist1 = onPointToPointDist(plow_x, plow_y, curr_x1, curr_y1);
                        dist2 = onPointToPointDist(plow_x, plow_y, curr_x2, curr_y2);
                        dist3 = onPointToPointDist(plow_x, plow_y, curr_x3, curr_y3);

                        strmsg = onRobotGathering_SubCheck(dist1, dist2, dist3, "plowing_3");
                        strdata = strmsg.Split(':');
                        pos_Robortname2[2] = strdata[0];
                        sw.WriteLine(strdata[1]); //3 위치 

                        plow_x = plowing_posArray[3, 0];
                        plow_y = plowing_posArray[3, 1];

                        curr_x1 = robot_currPosArray[0, 0];
                        curr_x2 = robot_currPosArray[1, 0];
                        curr_x3 = robot_currPosArray[3, 0];

                        curr_y1 = robot_currPosArray[0, 1];
                        curr_y2 = robot_currPosArray[1, 1];
                        curr_y3 = robot_currPosArray[3, 1];

                        for (int i = 0; i < 4; i++)
                        {
                            if (pos_Robortname2[i] == "R_005")
                            {
                                curr_x1 = 99;
                                curr_y1 = 99;
                            }
                            else if (pos_Robortname2[i] == "R_006")
                            {
                                curr_x2 = 99;
                                curr_y2 = 99;
                            }
                            else if (pos_Robortname2[i] == "R_008")
                            {
                                curr_x3 = 99;
                                curr_y3 = 99;
                            }
                        }

                        dist1 = onPointToPointDist(plow_x, plow_y, curr_x1, curr_y1);
                        dist2 = onPointToPointDist(plow_x, plow_y, curr_x2, curr_y2);
                        dist3 = onPointToPointDist(plow_x, plow_y, curr_x3, curr_y3);

                        strmsg = onRobotGathering_SubCheck(dist1, dist2, dist3, "plowing_4");
                        strdata = strmsg.Split(':');
                        pos_Robortname2[3] = strdata[0];
                        sw.WriteLine(strdata[1]); //4 위치 

                        #region test
                        /*
                        if (dist1 < dist2)
                        {
                            if (chk005Use.Checked)
                                sw.WriteLine("R_005,500_1,plowing_2,,1,0"); //2 위치  
                            else
                                sw.WriteLine("//R_005,500_1,plowing_2,,1,0"); //2 위치  

                            n005_actionidx = 2;
                        }
                        else
                        {
                            if (chk006Use.Checked)
                                sw.WriteLine("R_006,500_2,plowing_2,,1,0"); //2 위치 
                            else
                                sw.WriteLine("//R_006,500_2,plowing_2,,1,0"); //2 위치 

                            n006_actionidx = 4;
                        }

                        if(pos_Robortname[2]=="R_005")
                        {
                            if (chk005Use.Checked)
                                sw.WriteLine("R_005,500_1,plowing_3_2,,1,0"); //3 위치 
                            else
                                sw.WriteLine("//R_005,500_1,plowing_3_2,,1,0"); //3 위치 
                            n005_actionidx = 3;
                        }
                        else if (pos_Robortname[2] == "R_006")
                        {
                            if (chk006Use.Checked)
                                sw.WriteLine("R_006,500_2,plowing_3,,1,0"); //3 위치 
                            else
                                sw.WriteLine("//R_006,500_2,plowing_3,,1,0"); //3 위치 

                            n006_actionidx = 0;
                        }

                        if (chk008Use.Checked)
                            sw.WriteLine("R_008,300C,plowing_1,,1,0"); //1 위치 
                        else
                            sw.WriteLine("//R_008,300C,plowing_1,,1,0"); //1 위치 
                        */
                        #endregion

                        sw.Close();

                        if (pos_Robortname2[1] == "R_005") n005_actionidx = 1;
                        
                        else if (pos_Robortname2[1] == "R_006" && radioButton_inout.Checked) n006_actionidx = 4;
                        else if (pos_Robortname2[1] == "R_006" && !radioButton_inout.Checked) n006_actionidx = 1; //4;
                        else if (pos_Robortname2[1] == "R_008") n008_actionidx = 1;

                        if (pos_Robortname2[2] == "R_005") n005_actionidx = 2;
                        else if (pos_Robortname2[2] == "R_006" && radioButton_inout.Checked) n006_actionidx =  4;
                        else if (pos_Robortname2[2] == "R_006" && !radioButton_inout.Checked) n006_actionidx = 2;// 4;
                        else if (pos_Robortname2[2] == "R_008") n008_actionidx = 2;

                        if (pos_Robortname2[3] == "R_005") n005_actionidx = 3;
                        else if (pos_Robortname2[3] == "R_006" && radioButton_inout.Checked) n006_actionidx =  0;
                        else if (pos_Robortname2[3] == "R_006" && !radioButton_inout.Checked) n006_actionidx = 3;// 0;
                        else if (pos_Robortname2[3] == "R_008") n008_actionidx = 3;


                        string strJob1_pos_Job_File = "..\\Ros_info\\job1pos_job.txt";

                        using (StreamWriter sw2 = new System.IO.StreamWriter(strJob1_pos_Job_File, false, Encoding.Default))
                        {
                            sw2.WriteLine("robot_id,robot_name,work id, work_name, work_cnt, actidx");
                            string strmsg2 = "";
                            if(chk005Use.Checked)
                            {
                                strmsg2 = string.Format("R_005,500_1,In_Job,500_1 작업 1,500,{0}", n005_actionidx);
                                
                            }
                            else
                            {
                                strmsg2 = string.Format("//R_005,500_1,In_Job,500_1 작업 1,500,{0}", n005_actionidx);
                            }

                            sw2.WriteLine(strmsg2);

                            if (chk006Use.Checked)
                            {
                                if(radioButton_inout.Checked)
                                {
                                    if (radioButton_p1.Checked)
                                     strmsg2 = string.Format("R_006,500_2,Out_Job_new,500_1 작업 1,500,{0}", n006_actionidx);

                                    else if (radioButton_p2.Checked)
                                    strmsg2 = string.Format("R_006,500_2,Out_Job_new2,500_1 작업 1,500,{0}", n006_actionidx);
                                else if (radioButton_p3.Checked)
                                    strmsg2 = string.Format("R_006,500_2,Out_Job_new3,500_1 작업 1,500,{0}", n006_actionidx);
                                }
                                else 
                                    strmsg2 = string.Format("R_006,500_2,In_Job,500_1 작업 1,500,{0}", n006_actionidx);
                                /*
                                if (radioButton_p1.Checked)
                                    // strmsg2 = string.Format("R_006,500_2,Out_Job_new,500_1 작업 1,500,{0}", n006_actionidx);
                                    
                                else if (radioButton_p2.Checked)
                                  //  strmsg2 = string.Format("R_006,500_2,Out_Job_new2,500_1 작업 1,500,{0}", n006_actionidx);
                                else if (radioButton_p3.Checked)
                                  //  strmsg2 = string.Format("R_006,500_2,Out_Job_new3,500_1 작업 1,500,{0}", n006_actionidx);
                                  */
                            }
                            else
                            {
                                if (radioButton_inout.Checked)
                                {
                                    if (radioButton_p1.Checked)
                                        strmsg2 = string.Format("//R_006,500_2,Out_Job_new,500_1 작업 1,500,{0}", n006_actionidx);
                                    else if (radioButton_p2.Checked)
                                        strmsg2 = string.Format("//R_006,500_2,Out_Job_new2,500_1 작업 1,500,{0}", n006_actionidx);
                                    else if (radioButton_p3.Checked)
                                        strmsg2 = string.Format("//R_006,500_2,Out_Job_new3,500_1 작업 1,500,{0}", n006_actionidx);
                                }
                                else
                                    strmsg2 = string.Format("//R_006,500_2,In_Job,500_1 작업 1,500,{0}", n006_actionidx);

                                /* if (radioButton_p1.Checked)
                                     strmsg2 = string.Format("//R_006,500_2,Out_Job_new,500_1 작업 1,500,{0}", n006_actionidx);
                                 else if (radioButton_p2.Checked)
                                     strmsg2 = string.Format("//R_006,500_2,Out_Job_new2,500_1 작업 1,500,{0}", n006_actionidx);
                                 else if (radioButton_p3.Checked)
                                     strmsg2 = string.Format("//R_006,500_2,Out_Job_new3,500_1 작업 1,500,{0}", n006_actionidx);
                                     */
                            }
                            sw2.WriteLine(strmsg2);

                            if (chk008Use.Checked)
                            {
                                strmsg2 = string.Format("R_008,100_1,In_Job,100_1 작업 1,500,{0}",n008_actionidx);

                            }
                            else
                            {
                                strmsg2 = string.Format("//R_008,100_1,In_Job,100_1 작업 1,500,{0}", n008_actionidx);
                            }
                            sw2.WriteLine(strmsg2);
                   
                            sw2.Close();
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobotPlowing_Check err :={0}", ex.Message.ToString());
            }
        }


        private void onSequencepos_RobotInfo_Check(int nJobscheduleindex)
        {

            // 파일에서 읽어 테이블을 갱신한다. 
            Data.Instance.waitingpos_table.waitpos_robotinfo = new List<WaitingPos_RobotInfo>();
            KeyValuePair<string, string> job_file = JobSchedule_FileMatching.ElementAt(nJobscheduleindex);
            //  string strrobotid = robot_workinfo.Value.strRobotID;

            m_strWaitPosJob_File = job_file.Value;

            m_strWaitPosJob_File = "..\\Ros_info\\" + m_strWaitPosJob_File;
            try
            {
                if (!File.Exists(m_strWaitPosJob_File))
                {
                    using (StreamWriter sw = new System.IO.StreamWriter(m_strWaitPosJob_File, false, Encoding.Default))
                    {
                        sw.WriteLine("robot_id,robot_name,work id,work_name,work_cnt");
                        sw.Close();
                    }
                    //  return;
                }

                Invoke(new MethodInvoker(delegate ()
                {
                    dataGridView_InitJob.Rows.Clear();
                    dataGridView_Job.Rows.Clear();
                }));

                //모여파일 재생성....
                onRobotGather_Check();
                //헤처파일 재생성....
                onRobotPlowing_Check();

                using (StreamReader sr1 = new System.IO.StreamReader(m_strWaitPosJob_File, Encoding.Default))
                {
                    int ncnt = 0; //파일에 첫줄은 항목명으로 빼고 읽기 위해 선언
                    //Data.Instance.totalJobschedule.jobInfo = new List<JobInfo>();
                    while (sr1.Peek() >= 0)
                    {
                        string strTemp = sr1.ReadLine();

                        int ncomment = 0;

                        ncomment = strTemp.IndexOf("//");

                        if (ncnt != 0 && ncomment < 0)
                        {
                            string[] strjobinfo = strTemp.Split(',');

                            WaitingPos_RobotInfo waitposrobotinfo = new WaitingPos_RobotInfo();
                            waitposrobotinfo.strRobotID = strjobinfo[0];
                            waitposrobotinfo.strRobotName = strjobinfo[1];
                            waitposrobotinfo.strMisssionID = strjobinfo[2];
                            waitposrobotinfo.strMisssionName = strjobinfo[3];
                            waitposrobotinfo.nCnt = 1;

                            Data.Instance.waitingpos_table.waitpos_robotinfo.Add(waitposrobotinfo);


                            Invoke(new MethodInvoker(delegate ()
                            {
                                dataGridView_InitJob.Rows.Add(strjobinfo);
                            }));


                        }
                        ncnt++;
                    }
                }            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSequencepos_RobotInfo_Check err :={0}", ex.Message.ToString());
            }

            //화면에 표시 
        }

        private void onSynchronous_RobotInfo_Check(int nJobscheduleindex)
        {
            // 파일에서 읽어 테이블을 갱신한다. 
            Data.Instance.jobpos_table.jobpos_robotinfo = new List<JobPos_RobotInfo>();
            KeyValuePair<string, string> job_file = JobSchedule_FileMatching.ElementAt(nJobscheduleindex);
            //  string strrobotid = robot_workinfo.Value.strRobotID;

            m_strJobPosJob_File = job_file.Value;
            m_strJobPosJob_File = "..\\Ros_info\\" + m_strJobPosJob_File;
            try
            {
                if (!File.Exists(m_strJobPosJob_File))
                {
                    using (StreamWriter sw = new System.IO.StreamWriter(m_strJobPosJob_File, false, Encoding.Default))
                    {
                        sw.WriteLine("robot_id,robot_name,work id,work_name,work_cnt,actidx");
                        sw.Close();
                    }
                    //  return;
                }
                Invoke(new MethodInvoker(delegate ()
                {
                    dataGridView_Job.Rows.Clear();
                    dataGridView_InitJob.Rows.Clear();
                }));

                //모여파일 재생성....
                 onRobotGather_Check(); //20190507 test로 비활성화
                //헤처파일 재생성....
                  onRobotPlowing_Check(); //20190507 test로 비활성화

                using (StreamReader sr1 = new System.IO.StreamReader(m_strJobPosJob_File, Encoding.Default))
                {
                    int ncnt = 0; //파일에 첫줄은 항목명으로 빼고 읽기 위해 선언
                    //Data.Instance.totalJobschedule.jobInfo = new List<JobInfo>();
                    while (sr1.Peek() >= 0)
                    {
                        string strTemp = sr1.ReadLine();
                        int ncomment = 0;

                        ncomment = strTemp.IndexOf("//");

                        if (ncnt != 0 && ncomment < 0)
                        {
                            string[] strjobinfo = strTemp.Split(',');

                            JobPos_RobotInfo jobposrobotinfo = new JobPos_RobotInfo();
                            jobposrobotinfo.strRobotID = strjobinfo[0];
                            jobposrobotinfo.strRobotName = strjobinfo[1];
                            jobposrobotinfo.strMisssionID = strjobinfo[2];
                            jobposrobotinfo.strMisssionName = strjobinfo[3];
                            jobposrobotinfo.nCnt = int.Parse(strjobinfo[4]);
                            jobposrobotinfo.nStartidx = int.Parse(strjobinfo[5]);

                            Data.Instance.jobpos_table.jobpos_robotinfo.Add(jobposrobotinfo);


                            Invoke(new MethodInvoker(delegate ()
                            {
                                dataGridView_Job.Rows.Add(strjobinfo);
                            }));

                        }
                        ncnt++;
                    }
                }            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSequencepos_RobotInfo_Check err :={0}", ex.Message.ToString());
            }
        }

        public async void onInitPosMove()
        {
            try
            {
                if (Data.Instance.totalJobschedule.jobInfo[nTotalJobTablepos_index].nJobType == (int)JOB_TYPE.sequence)
                {
                    bRobotMissioning = false;

                    string strworkfile = "";
                    string strworkrobot = "";

                    string strworkid = "";

                    List<string> strSelectRobot = new List<string>();
                    List<string> strworkdata = new List<string>();
                    string[] strSelectRobot_Worker; //워크작업으로 전달하기 위해 형식 맞춤.. 
                    string[] strSelectworkdata_Worker;

                    int workcnt = 1;



                    string strRobot = Data.Instance.waitingpos_table.waitpos_robotinfo[nWaitingTablepos_index].strRobotID;
                    string strMissionID = Data.Instance.waitingpos_table.waitpos_robotinfo[nWaitingTablepos_index].strMisssionID;
                    string strRobotName = Data.Instance.waitingpos_table.waitpos_robotinfo[nWaitingTablepos_index].strRobotName;
                    string strMissionName = Data.Instance.waitingpos_table.waitpos_robotinfo[nWaitingTablepos_index].strMisssionName;
                    try
                    {
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


                        strSelectRobot_Worker = new string[1];
                        strSelectworkdata_Worker = new string[strworkdata.Count - 1];
                        strworkrobot = strRobot;
                        strSelectRobot_Worker[0] = strworkrobot;


                        string strworkname = strworkdata[0];
                        for (int i = 1; i < strworkdata.Count; i++)
                        {
                            strSelectworkdata_Worker[i - 1] = strworkdata[i];
                        }
                        int nworkcnt = workcnt;
                        int nactidx = 0;
                        var task = Task.Run(() => worker.onWorkOrder_publish(strworkname, strworkid, strSelectRobot_Worker, strSelectworkdata_Worker, nworkcnt, nactidx));
                        await task;

                        //Task t = new Task(() => worker.onWorkOrder_publish(strworkname, strworkid, strSelectRobot_Worker, strSelectworkdata_Worker, nworkcnt, nactidx));
                        //t.Start();

                        Thread.Sleep(100);

                        if (Data.Instance.robots_currgoing_status.ContainsKey(strRobot))
                        {
                            Data.Instance.robots_currgoing_status[strRobot].strMisssionName = strMissionName;
                            Data.Instance.robots_currgoing_status[strRobot].strMisssionID = strMissionID;
                            Data.Instance.robots_currgoing_status[strRobot].strStatus = "sequencepos";
                        }
                        else
                        {

                            Robot_Going_Status robot_going_status = new Robot_Going_Status();
                            robot_going_status.strRobotID = strRobot;
                            robot_going_status.strRobotName = strRobotName;
                            robot_going_status.strMisssionName = strMissionName;
                            robot_going_status.strMisssionID = strMissionID;
                            robot_going_status.strStatus = "initpos";


                            Data.Instance.robots_currgoing_status.Add(strRobot, robot_going_status);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Out.WriteLine("onInitPosMove err :={0}", ex.Message.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onInitPosMove err :={0}", ex.Message.ToString());
            }
        }

        public async void onJobMove(bool bfirst)
        {
            //  if (Data.Instance.totalJobschedule.jobInfo[nTotalJobTablepos_index].strJobname == "작업1")
            //    {
            try
            {
                if (Data.Instance.totalJobschedule.jobInfo[nTotalJobTablepos_index].nJobType == (int)JOB_TYPE.synchronous)
                {
                    Invoke(new MethodInvoker(delegate ()
                    {
                        if (chkRuntime.Checked)
                        {
                            nRuntime = (int)numericUpDown_RepeatTime.Value;
                            timer_runnigTime.Interval = nRuntime * 60 * 1000;
                            timer_runnigTime.Enabled = true;
                            bRuntime_complete = false;
                        }
                    }));

                    int nrobottotalcnt = Data.Instance.jobpos_table.jobpos_robotinfo.Count();

                    for (int nrobotcnt = 0; nrobotcnt < nrobottotalcnt; nrobotcnt++)
                    {

                        string strworkfile = "";
                        string strworkrobot = "";

                        string strworkid = "";

                        List<string> strSelectRobot = new List<string>();
                        List<string> strworkdata = new List<string>();
                        string[] strSelectRobot_Worker; //워크작업으로 전달하기 위해 형식 맞춤.. 
                        string[] strSelectworkdata_Worker;

                        int workcnt = 1;

                        string strRobot = Data.Instance.jobpos_table.jobpos_robotinfo[nrobotcnt].strRobotID;
                        string strMissionID = Data.Instance.jobpos_table.jobpos_robotinfo[nrobotcnt].strMisssionID;
                        string strRobotName = Data.Instance.jobpos_table.jobpos_robotinfo[nrobotcnt].strRobotName;
                        string strMissionName = Data.Instance.jobpos_table.jobpos_robotinfo[nrobotcnt].strMisssionName;
                        workcnt = Data.Instance.jobpos_table.jobpos_robotinfo[nrobotcnt].nCnt;

                        int nactidx = 0;
                        if (bfirst || bCombine_RunFirst)
                        {
                            nactidx = Data.Instance.jobpos_table.jobpos_robotinfo[nrobotcnt].nStartidx;
                            
                        }


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


                        strSelectRobot_Worker = new string[1];
                        strSelectworkdata_Worker = new string[strworkdata.Count - 1];
                        strworkrobot = strRobot;
                        strSelectRobot_Worker[0] = strworkrobot;


                        string strworkname = strworkdata[0];
                        for (int i = 1; i < strworkdata.Count; i++)
                        {
                            strSelectworkdata_Worker[i - 1] = strworkdata[i];
                        }
                        int nworkcnt = workcnt;

                        // var task = Task.Run(() => worker.onWorkOrder_publish(strworkname, strworkid, strSelectRobot_Worker, strSelectworkdata_Worker, nworkcnt, nactidx));
                        var task = Task.Run(() => worker.onWorkOrder_publish(strworkname, strworkid, strSelectRobot_Worker, strSelectworkdata_Worker, nworkcnt, nactidx));

                        await task;

                        //Task t = new Task(() => worker.onWorkOrder_publish(strworkname, strworkid, strSelectRobot_Worker, strSelectworkdata_Worker, nworkcnt, nactidx));
                        // t.Start();

                        if (Data.Instance.robots_currgoing_status.ContainsKey(strRobot))
                        {
                            Data.Instance.robots_currgoing_status[strRobot].strMisssionName = strMissionName;
                            Data.Instance.robots_currgoing_status[strRobot].strMisssionID = strMissionID;
                            Data.Instance.robots_currgoing_status[strRobot].strStatus = "synchronousjob";
                        }
                        else
                        {

                            Robot_Going_Status robot_going_status = new Robot_Going_Status();
                            robot_going_status.strRobotID = strRobot;
                            robot_going_status.strRobotName = strRobotName;
                            robot_going_status.strMisssionName = strMissionName;
                            robot_going_status.strMisssionID = strMissionID;
                            robot_going_status.strStatus = "synchronousjob";


                            Data.Instance.robots_currgoing_status.Add(strRobot, robot_going_status);
                        }

                        Thread.Sleep(100);

                        bRobotMissioning = true;

                    }

                    if(bCombine_RunFirst) bCombine_RunFirst = false;
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onJobMove err :={0}", ex.Message.ToString());
            }
            // }
        }

        bool m_bLift1_Status = false;

        public async void onLoopJobMove(string strrobotid)
        {
            try
            {
                if (Data.Instance.Robot_work_info.Count > 0)
                {
                    for (int i = 0; i < Data.Instance.Robot_work_info.Count; i++)
                    {
                        if (Data.Instance.Robot_work_info.ContainsKey(strrobotid))
                        {
                            if (Data.Instance.Robot_work_info[strrobotid].strLoop_Flag == "loop")
                            {
                                for (int j = 0; j < Data.Instance.jobpos_table.jobpos_robotinfo.Count; j++)
                                {
                                    if (Data.Instance.jobpos_table.jobpos_robotinfo[j].strRobotID.Contains(strrobotid))
                                    {
                                        string strRobot = Data.Instance.jobpos_table.jobpos_robotinfo[j].strRobotID;
                                        string strMissionID = Data.Instance.jobpos_table.jobpos_robotinfo[j].strMisssionID;
                                        string strRobotName = Data.Instance.jobpos_table.jobpos_robotinfo[j].strRobotName;
                                        string strMissionName = Data.Instance.jobpos_table.jobpos_robotinfo[j].strMisssionName;
                                        string strworkfile = "";
                                        List<string> strworkdata = new List<string>();
                                        string[] strSelectworkdata_Worker;


                                        strworkfile = strMissionID;
                                        string strworkid = "";

                                        if (strworkfile.IndexOf("Out_Job") > -1)
                                        {

                                            if (radioButton_p1.Checked)
                                                strworkfile = "Out_Job_new";
                                            else if (radioButton_p2.Checked)
                                                strworkfile = "Out_Job_new2";
                                            else if (radioButton_p3.Checked)
                                                strworkfile = "Out_Job_new3";
                                        }

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
                                        for (int i2 = 1; i2 < strworkdata.Count; i2++)
                                        {
                                            strSelectworkdata_Worker[i2 - 1] = strworkdata[i2];
                                        }

                                        Data.Instance.Robot_work_info[strRobot].robot_workdata.Clear(); //기존 로봇데이타 클리어. 

                                        for (int nworkidx = 0; nworkidx < strSelectworkdata_Worker.Length; nworkidx++)
                                        {
                                            string straction = strSelectworkdata_Worker[nworkidx];
                                            string[] straction_sub = straction.Split('/');
                                            string stractiontype = "";
                                            if (straction_sub[0].Split(':')[1].Equals("Goal-Point"))
                                                stractiontype = "주행";
                                            else if (straction_sub[0].Split(':')[1].Equals("Basic-Move"))
                                                stractiontype = "기본움직임";
                                            else if (straction_sub[0].Split(':')[1].Equals("Lift-Conveyor-Control"))
                                                stractiontype = "리프트&컨베어";
                                            else if (straction_sub[0].Split(':')[1].Equals("PLC"))
                                                stractiontype = "PLC";
                                            else if (straction_sub[0].Split(':')[1].Equals("URMISSION"))
                                                stractiontype = "URMission";
                                            else if (straction_sub[0].Split(':')[1].Equals("Stable_pallet"))
                                                stractiontype = "stablepallet";

                                            Robot_Work_Data robot_work_data = new Robot_Work_Data();
                                            robot_work_data.strTopic = "";
                                            robot_work_data.strWorkData = strSelectworkdata_Worker[nworkidx];
                                            robot_work_data.strActionType = stractiontype;


                                            Data.Instance.Robot_work_info[strRobot].robot_workdata.Add(robot_work_data);
                                        }


                                        Data.Instance.Robot_work_info[strrobotid].strLoop_Flag = "wait";
                                        int nworkcnt = 0;
                                        nworkcnt = Data.Instance.Robot_work_info[strrobotid].nCurrWork_cnt;
                                        nworkcnt++;

                                        onListmsg(string.Format("work mission send :={0}, cnt={1}", strrobotid,nworkcnt));

                                        Thread.Sleep(Data.Instance.nWorkDelayTime);
                                        var task = Task.Run(() => worker.onLoopWork_Publish(Data.Instance.Robot_work_info[strrobotid].strRobotID, nworkcnt));
                                        await task;
                                        //task.Wait();

                                        //Task t = new Task(() => worker.onLoopWork_Publish(Data.Instance.Robot_work_info[strrobotid].strRobotID, nworkcnt));
                                        //t.Start();

                                        break;
                                    }

                                }

                                // Thread.Sleep(500);
                                break;
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onLoopJobMove err :={0}", ex.Message.ToString());
            }
        }

        public void onNextJobCheck()
        {
            try
            {
                if (nTotalJobTablepos_index >= nTotalJobTableEndpos_index)
                {
                    //모든 job 끝
                    onBtn_enable(true);
                    MessageBox.Show("작업을 완료하였습니다.");
                }
                else
                {
                    if (Data.Instance.totalJobschedule.jobInfo[nTotalJobTablepos_index].nJobType == (int)JOB_TYPE.sequence)
                    {
                        onSequencepos_RobotInfo_Check(nTotalJobTablepos_index);
                        nWaitingTableEndpos_index = Data.Instance.waitingpos_table.waitpos_robotinfo.Count();
                        onInitPosMove();
                    }
                    else if (Data.Instance.totalJobschedule.jobInfo[nTotalJobTablepos_index].nJobType == (int)JOB_TYPE.synchronous)
                    {
                        onSynchronous_RobotInfo_Check(nTotalJobTablepos_index);
                        onJobMove(false);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onNextJobCheck err :={0}", ex.Message.ToString());
            }
        }

        public void WorkresultComlete(string strrobotid)
        {
            try
            {
                if (m_bStop)//|| m_bPause || m_bRestart)
                {
                    onStopPauseRestart_disable();
                    nWaitingTableEndpos_index = 0;
                    nTotalJobTableEndpos_index = 0;
                    return;
                }

                if (Data.Instance.robots_currgoing_status.ContainsKey(strrobotid))
                {
                    string strRobot = Data.Instance.robots_currgoing_status[strrobotid].strRobotID;
                    string strMissionID = Data.Instance.robots_currgoing_status[strrobotid].strMisssionID;
                    string strRobotName = Data.Instance.robots_currgoing_status[strrobotid].strRobotName;
                    string strMissionName = Data.Instance.robots_currgoing_status[strrobotid].strMisssionName;

                    //로봇이 한일을 파일에 저장하여 나중에 집계한다.
                    onRobotWork_save(strRobot, strRobotName, strMissionID, strMissionName, "OK", 1);
                }



                if (Data.Instance.totalJobschedule.jobInfo[nTotalJobTablepos_index].nJobType == (int)JOB_TYPE.sequence)
                {

                    if (Data.Instance.waitingpos_table.waitpos_robotinfo == null) return;

                    string strRobot = Data.Instance.waitingpos_table.waitpos_robotinfo[nWaitingTablepos_index].strRobotID;
                    string strMissionID = Data.Instance.waitingpos_table.waitpos_robotinfo[nWaitingTablepos_index].strMisssionID;
                    string strRobotName = Data.Instance.waitingpos_table.waitpos_robotinfo[nWaitingTablepos_index].strRobotName;
                    string strMissionName = Data.Instance.waitingpos_table.waitpos_robotinfo[nWaitingTablepos_index].strMisssionName;


                    if (Data.Instance.robots_currgoing_status.ContainsKey(strRobot))
                    {
                        Data.Instance.robots_currgoing_status[strRobot].strMisssionName = strMissionName;
                        Data.Instance.robots_currgoing_status[strRobot].strMisssionID = strMissionID;
                        Data.Instance.robots_currgoing_status[strRobot].strStatus = "wait";
                    }

                    nWaitingTablepos_index++;
                    if (nWaitingTablepos_index >= nWaitingTableEndpos_index)
                    {
                        nWaitingTablepos_index = 0;

                        nTotalJobTablepos_index++;
                        onNextJobCheck();
                    }
                    else
                    {
                        onInitPosMove();
                    }
                }
                else if (Data.Instance.totalJobschedule.jobInfo[nTotalJobTablepos_index].nJobType == (int)JOB_TYPE.synchronous)
                {

                    if (Data.Instance.robots_currgoing_status.ContainsKey(strrobotid))
                    {
                        Data.Instance.robots_currgoing_status[strrobotid].strStatus = "wait";
                    }

                    int njobcomplete_check = 0;

                    foreach (KeyValuePair<string, Robot_Going_Status> info in Data.Instance.robots_currgoing_status)
                    {
                        if (info.Value.strStatus == "wait")
                            njobcomplete_check++;

                    }

                    if (Data.Instance.robots_currgoing_status.Count() == njobcomplete_check)
                    {
                        Data.Instance.robots_currgoing_status.Clear();
                        nTotalJobTablepos_index++;
                        //다음 job 테이블로 이동
                        onNextJobCheck();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("WorkresultComlete err :={0}", ex.Message.ToString());
            }
        }

        public void Workresult_Loop_Comlete(string strrobotid)
        {
            try
            {
                if (m_bStop)//|| m_bPause || m_bRestart)
                {
                    //onStopPauseRestart_disable();
                    nWaitingTableEndpos_index = 0;
                    nTotalJobTableEndpos_index = 0;

                    //worker.onDeleteSelectRobotSubscribe(strrobotid);

                    return;
                }


                if (Data.Instance.robots_currgoing_status.ContainsKey(strrobotid))
                {
                    string strRobot = Data.Instance.robots_currgoing_status[strrobotid].strRobotID;
                    string strMissionID = Data.Instance.robots_currgoing_status[strrobotid].strMisssionID;
                    string strRobotName = Data.Instance.robots_currgoing_status[strrobotid].strRobotName;
                    string strMissionName = Data.Instance.robots_currgoing_status[strrobotid].strMisssionName;

                    //로봇이 한일을 파일에 저장하여 나중에 집계한다.
                    onRobotWork_save(strRobot, strRobotName, strMissionID, strMissionName, "OK", 1);
                }



                if (Data.Instance.totalJobschedule.jobInfo[nTotalJobTablepos_index].nJobType == (int)JOB_TYPE.synchronous)
                {
                    onLoopJobMove(strrobotid);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Workresult_Loop_Comlete err :={0}", ex.Message.ToString());
            }
        }


        /// <summary> 
        /// 로봇 작업 log 기록
        /// </summary>
        public void onRobotWork_save(string strRobot, string strRobotName, string strMissionID, string strMissionName, string strResult, int ncnt)//"OK",1)
        {
            string strLog_File = "..\\Ros_info\\log\\" + strRobot + "_" + m_strLog_File;
            try
            {
                if (!File.Exists(strLog_File))
                {
                    using (StreamWriter sw = new System.IO.StreamWriter(strLog_File, false, Encoding.Default))
                    {
                        sw.Close();
                    }
                }

                using (StreamWriter sw = new System.IO.StreamWriter(strLog_File, true, Encoding.Default))
                {
                    string strmsg = "";
                    strmsg = strRobot + "," + strRobotName + "," + strMissionID + "," + strMissionName + "," + strResult + "," + ncnt.ToString();
                    sw.WriteLine(strmsg);
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobotWork_save err :={0}", ex.Message.ToString());
            }

        }

        #endregion


        #region Crash fountion
        bool bR005pause = false;
        bool bR006pause = false;
        bool bR008pause = false;

        bool bR008pause2 = false;
        int npauselimit_Cnt = 0;

        bool bR005pause2 = false;
        bool bR006pause2 = false;

        string[,] strR005_T=new string[4,5];
        string[,] strR006_T = new string[4, 5];
        string[,] strR007_T = new string[4, 5];
        string[,] strR008_T = new string[4, 5];

        bool[] bPauseTable = new bool[4];


        enum RobotdistTableColums
        {
            Robot_id=0,
            R1_R2_LAD,
            R1_R2_CenToLA,
            R2_R1_CenToLA,
            STATUS,
        };

        enum URdistTableColums
        {
            Robot_id = 0,
            R1_R2_Dist,
            R2_X,
            R2_Y,
            R2_Theta,
        };

        private void timer_makeDistTable_Tick(object sender, EventArgs e)
        {
            //onRobotsDist_Cal();
            //onRobotDist_TableMake2();
        }
        bool bRobotMissioning = false;
        private void timer_CrashCheck_Tick(object sender, EventArgs e)
        {
            onRobotDist_TableMake2();

            if (bRobotMissioning)
                onCrashCheck2();
        }

       

        private double onPointToPointDist(double x1, double y1, double x2, double y2)
        {
            double hypo = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            return hypo;
        }

        private void timer_palletCheck_Tick(object sender, EventArgs e)
        {
          //  onCrashCheck();
        }

        #region crash routine
        double[,] XYdata = new double[3, 4];
        private void onDistTable_initial()
        {

            Dictionary<string, double[]> RobotsDistTable = new Dictionary<string, double[]>();

            strR005_T = new string[3, 5];
            strR006_T = new string[3, 5];
            strR008_T = new string[3, 5];

            strR005_T[0, (int)RobotdistTableColums.Robot_id] = "R_005";
            strR005_T[1, (int)RobotdistTableColums.Robot_id] = "R_006";
            strR005_T[2, (int)RobotdistTableColums.Robot_id] = "R_008";

            strR006_T[0, (int)RobotdistTableColums.Robot_id] = "R_005";
            strR006_T[1, (int)RobotdistTableColums.Robot_id] = "R_006";
            strR006_T[2, (int)RobotdistTableColums.Robot_id] = "R_008";

            strR008_T[0, (int)RobotdistTableColums.Robot_id] = "R_005";
            strR008_T[1, (int)RobotdistTableColums.Robot_id] = "R_006";
            strR008_T[2, (int)RobotdistTableColums.Robot_id] = "R_008";

            for (int i = 0; i < 3; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    strR005_T[i, j] = "0";
                    strR006_T[i, j] = "0";
                    strR008_T[i, j] = "0";
                }
                strR005_T[i, 4] = "ok";
                strR006_T[i, 4] = "ok";
                strR008_T[i, 4] = "ok";
            }

            for (int k1 = 0; k1 < 3; k1++)
            {
                for (int k2 = 0; k2 < 4; k2++)
                {
                    XYdata[k1, k2] = 999;
                }
            }
        }

        private void onRobotDist_TableMake2()
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    string[] strrobotllist = { "R_005", "R_006", "R_008" };
                    


                    for (int i = 0; i < strrobotllist.Length; i++)
                    {

                        if (Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.robotstate.msg == null) continue;

                        if (Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.lookahead.msg == null) continue;

                        double px = Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.lookahead.msg.point.x;
                        double py = Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.lookahead.msg.point.y;

                        if (px == 0 && py == 0) continue;

                        XYdata[i, 0] = Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.robotstate.msg.pose.x;
                        XYdata[i, 1] = Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.robotstate.msg.pose.y;
                        XYdata[i, 2] = Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.lookahead.msg.point.x;
                        XYdata[i, 3] = Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.lookahead.msg.point.y;
                    }

                    strR005_T = onRobotDist_TableSubDataMake("R_005", XYdata, strR005_T, 0);
                    strR006_T = onRobotDist_TableSubDataMake("R_006", XYdata, strR006_T, 1);
                    strR008_T = onRobotDist_TableSubDataMake("R_008", XYdata, strR008_T, 2);

                    Invoke(new MethodInvoker(delegate ()
                    {
                        txtLAD_005_1.Text = strR005_T[0, 1];
                        txtLAD_005_2.Text = strR005_T[1, 1];
                        txtLAD_005_3.Text = strR005_T[2, 1];

                        txtLAD_005_1_1.Text = strR005_T[0, 2];
                        txtLAD_005_2_1.Text = strR005_T[1, 2];
                        txtLAD_005_3_1.Text = strR005_T[2, 2];

                        txtLAD_005_1_2.Text = strR005_T[0, 3];
                        txtLAD_005_2_2.Text = strR005_T[1, 3];
                        txtLAD_005_3_2.Text = strR005_T[2, 3];
                        
                        txtLAD_006_1.Text = strR006_T[0, 1];
                        txtLAD_006_2.Text = strR006_T[1, 1];
                        txtLAD_006_3.Text = strR006_T[2, 1];

                        txtLAD_006_1_1.Text = strR006_T[0, 2];
                        txtLAD_006_2_1.Text = strR006_T[1, 2];
                        txtLAD_006_3_1.Text = strR006_T[2, 2];

                        txtLAD_006_1_2.Text = strR006_T[0, 3];
                        txtLAD_006_2_2.Text = strR006_T[1, 3];
                        txtLAD_006_3_2.Text = strR006_T[2, 3];

                        txtLAD_008_1.Text = strR008_T[0, 1];
                        txtLAD_008_2.Text = strR008_T[1, 1];
                        txtLAD_008_3.Text = strR008_T[2, 1];

                        txtLAD_008_1_1.Text = strR008_T[0, 2];
                        txtLAD_008_2_1.Text = strR008_T[1, 2];
                        txtLAD_008_3_1.Text = strR008_T[2, 2];

                        txtLAD_008_1_2.Text = strR008_T[0, 3];
                        txtLAD_008_2_2.Text = strR008_T[1, 3];
                        txtLAD_008_3_2.Text = strR008_T[2, 3];

                        txtx1.Text = string.Format("{0:f2}", XYdata[2, 0]);
                        txty1.Text = string.Format("{0:f2}", XYdata[2, 1]);

                        txtx2.Text = string.Format("{0:f2}", XYdata[2, 2]);
                        txty2.Text = string.Format("{0:f2}", XYdata[2, 3]);

                    }));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("onRobotDist_TableMake2 err ==" + ex.Message.ToString());
            }
        }
        private string[,] onRobotDist_TableSubDataMake(string strsourcerobot, double[,] xydata, string[,] strTableTmp, int nskipidx)
        {
            try
            {
                double sourceX = 0;
                double sourceY = 0;
                double sourceLad_X = 0;
                double sourceLad_Y = 0;
                double targetX = 0;
                double targetY = 0;
                double targetLad_X = 0;
                double targetLad_Y = 0;

                double dR1_R2LAD = 0;
                double dR1_R2CenToLA = 0;
                double dR2_R1CenToLA = 0;

                sourceX = xydata[nskipidx, 0];
                sourceY = xydata[nskipidx, 1];
                sourceLad_X = xydata[nskipidx, 2];
                sourceLad_Y = xydata[nskipidx, 3];


                for (int i2 = 0; i2 < 3; i2++)
                {
                    string strcheckrobot = strTableTmp[i2, (int)RobotdistTableColums.Robot_id];

                    if (strsourcerobot == strcheckrobot)
                    { }
                    else
                    {
                        if (strsourcerobot == "R_008")
                        {
                            targetX = xydata[i2, 0];
                            targetY = xydata[i2, 1];
                            targetLad_X = xydata[i2, 2];
                            targetLad_Y = xydata[i2, 3];
                        }
                        else
                        {
                            targetX = xydata[i2, 0];
                            targetY = xydata[i2, 1];
                            targetLad_X = xydata[i2, 2];
                            targetLad_Y = xydata[i2, 3];
                        }

                        if (Data.Instance.Robot_work_info[strcheckrobot].robot_status_info.goalrunnigstatus == null || Data.Instance.Robot_work_info[strsourcerobot].robot_status_info.goalrunnigstatus == null)
                        {

                            dR1_R2LAD = 99;
                            dR1_R2CenToLA = 99;
                            dR2_R1CenToLA = 99;
                        }
                        else
                        {
                            if (Data.Instance.Robot_work_info[strcheckrobot].robot_status_info.goalrunnigstatus.msg == null || Data.Instance.Robot_work_info[strsourcerobot].robot_status_info.goalrunnigstatus.msg == null)
                            {
                                dR1_R2LAD = 99;
                                dR1_R2CenToLA = 99;
                                dR2_R1CenToLA = 99;
                            }
                            else
                            {
                                
                                int nidx1 = Data.Instance.Robot_work_info[strsourcerobot].robot_status_info.goalrunnigstatus.msg.status_list.Count;

                                if (nidx1 > 0)
                                {
                                    if (Data.Instance.Robot_work_info[strsourcerobot].robot_status_info.goalrunnigstatus.msg.status_list[nidx1 - 1].status != 1)
                                    {
                                      //  sourceLad_X = sourceX;
                                      //  sourceLad_Y = sourceY;
                                    }
                                
                                }
                          
                                int nidx = Data.Instance.Robot_work_info[strcheckrobot].robot_status_info.goalrunnigstatus.msg.status_list.Count;
                                if (nidx > 0)
                                {
                                    if (Data.Instance.Robot_work_info[strcheckrobot].robot_status_info.goalrunnigstatus.msg.status_list[nidx - 1].status != 1)
                                    {
                                     //  targetLad_X = targetX;
                                     //   targetLad_Y = targetY;
                                    }
                                }
                           

                                dR1_R2LAD = onPointToPointDist(sourceLad_X, sourceLad_Y, targetLad_X, targetLad_Y);
                                dR1_R2CenToLA = onPointToPointDist(sourceX, sourceY, targetLad_X, targetLad_Y);
                                dR2_R1CenToLA = onPointToPointDist(targetX, targetY, sourceLad_X, sourceLad_Y);
                            }
                        }

                        strTableTmp[i2, (int)RobotdistTableColums.R1_R2_LAD] = string.Format("{0:f2}", dR1_R2LAD);
                        strTableTmp[i2, (int)RobotdistTableColums.R1_R2_CenToLA] = string.Format("{0:f2}", dR1_R2CenToLA);
                        strTableTmp[i2, (int)RobotdistTableColums.R2_R1_CenToLA] = string.Format("{0:f2}", dR2_R1CenToLA);
                    }
                }

                return strTableTmp;
            }
            catch (Exception ex)
            {
                Console.WriteLine("onRobotDist_TableSubDataMake err ==" + ex.Message.ToString());
                return strTableTmp;
            }
        }

        private void onCrashCheck2()
        {
            onRobotsCrashCheck("R_005", 0, strR005_T);
            onRobotsCrashCheck("R_006", 0, strR006_T);
           // onRobotsCrashCheck("R_007", 0, strR007_T);
            onRobotsCrashCheck("R_008", 0, strR008_T);
        }

        string[,] crashstatus = new string[3, 3];

        private void onRobotsCrashCheck(string strsourcerobot, int nskipidx, string[,] strTableTmp)
        {
            crashstatus[0, 0] = "56";
            crashstatus[1, 0] = "58";
            crashstatus[2, 0] = "86";

            try
            {
                if (Data.Instance.isConnected)
                {
                    if (Data.Instance.Robot_work_info.ContainsKey(strsourcerobot))
                    {
                        string strR1 = strsourcerobot;
                        string strR2 = "";
                        string currR1R2 = "";
                        string currR1 = "";
                        string currR2 = "";

                        for (int i = 0; i < 3; i++)
                        {
                            //if (nskipidx == i) continue;

                            strR2 = strTableTmp[i, (int)RobotdistTableColums.Robot_id];

                            if ((strR1 == "R_005" && strR2 == "R_006") || (strR2 == "R_005" && strR1 == "R_006")) currR1R2 = "56";
                            if ((strR1 == "R_005" && strR2 == "R_008") || (strR2 == "R_005" && strR1 == "R_008")) currR1R2 = "58";
                            if ((strR1 == "R_008" && strR2 == "R_006") || (strR2 == "R_008" && strR1 == "R_006")) currR1R2 = "86";

                            if (strR1 == "R_005") currR1 = "5";
                            else if (strR1 == "R_006") currR1 = "6";
                            else if (strR1 == "R_008") currR1 = "8";

                            if (strR2 == "R_005") currR2 = "5";
                            else if (strR2 == "R_006") currR2 = "6";
                            else if (strR2 == "R_008") currR2 = "8";


                            if (strR1 == strR2) continue;

                            double r1_r2Lad = double.Parse(strTableTmp[i, (int)RobotdistTableColums.R1_R2_LAD]);
                            double r1_r2CenToLad = double.Parse(strTableTmp[i, (int)RobotdistTableColums.R1_R2_CenToLA]);
                            double r2_r1CenToLad = double.Parse(strTableTmp[i, (int)RobotdistTableColums.R2_R1_CenToLA]);



                            
                            if (Data.Instance.Robot_work_info[strR1].robot_status_info.goalrunnigstatus.msg == null) continue; 

                            int nidx2 = Data.Instance.Robot_work_info[strR1].robot_status_info.goalrunnigstatus.msg.status_list.Count;
                            if (nidx2 > 0)
                            {
                                if (Data.Instance.Robot_work_info[strR1].robot_status_info.goalrunnigstatus.msg.status_list[nidx2 - 1].status != 1)
                                {
                                   // return;
                                }
                            }
                            if (Data.Instance.Robot_work_info[strR2].robot_status_info.goalrunnigstatus.msg == null) continue;

                            int nidx = Data.Instance.Robot_work_info[strR2].robot_status_info.goalrunnigstatus.msg.status_list.Count;
                            if (nidx > 0)
                            {
                                if (Data.Instance.Robot_work_info[strR2].robot_status_info.goalrunnigstatus.msg.status_list[nidx - 1].status != 1)
                                {
                                   // return;
                                }
                            }


                            if (r1_r2Lad < 1.5 && r1_r2Lad > 0) //crash
                            {
                                //strTableTmp[i, 4] = "crash";

                                if (r1_r2CenToLad < r2_r1CenToLad) //r1 move, r2 pause
                                {
                                    for (int i2 = 0; i2 < 3; i2++)
                                    {
                                        if (crashstatus[i2, 0] == currR1R2)
                                        {
                                            if (crashstatus[i2, 2] == "pause")
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                                workinfo.Add(strR2, "");
                                                onJobPause(workinfo);
                                                Thread.Sleep(200);
                                                onListmsg(string.Format("{0}==pause", strR2));

                                                for (int i1 = 0; i1 < 5; i1++)
                                                {
                                                    int cnt = Data.Instance.Robot_work_info[strR2].robot_status_info.goalrunnigstatus.msg.status_list.Count;
                                                    if (Data.Instance.Robot_work_info[strR2].robot_status_info.goalrunnigstatus.msg.status_list[cnt-1].status != 2)
                                                    {
                                                        onJobPause(workinfo);

                                                        onListmsg(string.Format("{0}==pause", strR2));
                                                    }
                                                    else if (Data.Instance.Robot_work_info[strR2].robot_status_info.goalrunnigstatus.msg.status_list[cnt - 1].status == 2)
                                                    {
                                                        break;
                                                
                                                    }
                                                    Thread.Sleep(200);
                                                }
                   
                                                break;
                                            }
                                        }
                                    }
                                   
                                    for (int i2 = 0; i2 < 3; i2++)
                                    {
                                        if (crashstatus[i2,0] == currR1R2)
                                        {
                                            crashstatus[i2, 1] = currR2;
                                            crashstatus[i2, 2] = "pause";

                                            break;
                                        }
                                    }
                                }
                                else //r2 move r1 pause
                                {
                                    for (int i2 = 0; i2 < 3; i2++)
                                    {
                                        if (crashstatus[i2, 0] == currR1R2)
                                        {
                                            if (crashstatus[i2, 2] == "pause")
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                                workinfo.Add(strR1, "");
                                                onJobPause(workinfo);
                                                Thread.Sleep(200);
                                                onListmsg(string.Format("{0}==pause", strR1));

                                                for (int i1 = 0; i1 < 5; i1++)
                                                {
                                                    int cnt = Data.Instance.Robot_work_info[strR1].robot_status_info.goalrunnigstatus.msg.status_list.Count;
                                                    if (Data.Instance.Robot_work_info[strR1].robot_status_info.goalrunnigstatus.msg.status_list[cnt - 1].status !=2)
                                                    {
                                                        onJobPause(workinfo);
                                                        onListmsg(string.Format("{0}==pause", strR1));
                                                    }
                                                    else if (Data.Instance.Robot_work_info[strR1].robot_status_info.goalrunnigstatus.msg.status_list[cnt - 1].status == 2)
                                                    {
                                                        break;
                                                    }
                                                    Thread.Sleep(200);
                                                }
                                                
                                                
                                                break;
                                            }
                                        }
                                    }

                                    for (int i2 = 0; i2 < 3; i2++)
                                    {
                                        if (crashstatus[i2, 0] == currR1R2)
                                        {
                                            crashstatus[i2, 1] = currR1;
                                            crashstatus[i2, 2] = "pause";

                                            break;
                                        }
                                    }
                                }
                            }
                            else if (r1_r2Lad >=2.0 )
                            {
                                for (int i2 = 0; i2 < 3; i2++)
                                {
                                    if (crashstatus[i2, 0] == currR1R2 && crashstatus[i2, 2] == "pause")
                                    {
                                        string strTemp = "";
                                        if (crashstatus[i2, 1] == "5") strTemp = "R_005";
                                        else if (crashstatus[i2, 1] == "6") strTemp = "R_006";
                                        else if (crashstatus[i2, 1] == "8") strTemp = "R_008";
                                        Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                        workinfo.Add(strTemp, "");
                                        onJobRestart(workinfo);
                                        Thread.Sleep(200);
                                        crashstatus[i2, 2] = "idle";

                                        onListmsg(string.Format("{0}, {1}==restart", strR1, strR2));

                                        onJobRestart(workinfo);
                                        Thread.Sleep(200);
                                        crashstatus[i2, 2] = "idle";

                                        onListmsg(string.Format("{0}, {1}==restart", strR1, strR2));

                                        break;
                                    }
                                }
                               
                                //  if (strTableTmp[i, 4] == "crash")
                                //  {

                                // workinfo.Add(strR1, "");
                                // workinfo.Add(strR2, "");
                                // onJobRestart(workinfo);
                                //    strTableTmp[i, 4] = "ok";

                                //   }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("onRobotsCrashCheck err ==" + ex.Message.ToString());
            }
        }
        #endregion 

        #region test backup
        /*
        private void onDistTable_initial()
        {

            Dictionary<string, double[]> RobotsDistTable = new Dictionary<string, double[]>();

            strR005_T = new string[4, 5];
            strR006_T = new string[4, 5];
            strR007_T = new string[4, 5];
            strR008_T = new string[4, 5];

            strR005_T[0, (int)RobotdistTableColums.Robot_id] = "R_005";
            strR005_T[1, (int)RobotdistTableColums.Robot_id] = "R_006";
            strR005_T[2, (int)RobotdistTableColums.Robot_id] = "R_007";
            strR005_T[3, (int)RobotdistTableColums.Robot_id] = "R_008";

            strR006_T[0, (int)RobotdistTableColums.Robot_id] = "R_005";
            strR006_T[1, (int)RobotdistTableColums.Robot_id] = "R_006";
            strR006_T[2, (int)RobotdistTableColums.Robot_id] = "R_007";
            strR006_T[3, (int)RobotdistTableColums.Robot_id] = "R_008";

            strR007_T[0, (int)RobotdistTableColums.Robot_id] = "R_005";
            strR007_T[1, (int)RobotdistTableColums.Robot_id] = "R_006";
            strR007_T[2, (int)RobotdistTableColums.Robot_id] = "R_007";
            strR007_T[3, (int)RobotdistTableColums.Robot_id] = "R_008";

            strR008_T[0, (int)RobotdistTableColums.Robot_id] = "R_005";
            strR008_T[1, (int)RobotdistTableColums.Robot_id] = "R_006";
            strR008_T[2, (int)RobotdistTableColums.Robot_id] = "R_007";
            strR008_T[3, (int)RobotdistTableColums.Robot_id] = "R_008";

            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    strR005_T[i, j] = "0";
                    strR006_T[i, j] = "0";
                    strR007_T[i, j] = "0";
                    strR008_T[i, j] = "0";
                }
                strR005_T[i, 4] = "ok";
                strR006_T[i, 4] = "ok";
                strR007_T[i, 4] = "ok";
                strR008_T[i, 4] = "ok";
            }

            for (int k1 = 0; k1 < 4; k1++)
            {
                for (int k2 = 0; k2 < 4; k2++)
                {
                    XYdata[k1, k2] = 999;
                }
            }
        }

        private void onRobotDist_TableMake2()
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    string[] strrobotllist = { "R_005", "R_006", "R_007", "R_008" };
                    double[,,] xydata = new double[4,4,2];

                    

                    for (int i = 0; i < strrobotllist.Length; i++)
                    {

                        if (Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.robotstate.msg == null) continue;

                        if (Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.lookahead.msg == null) continue;

                        double px = Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.lookahead.msg.point.x;
                        double py = Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.lookahead.msg.point.y;

                        if (px == 0 && py == 0)  continue;

                        XYdata[i, 0] = Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.robotstate.msg.pose.x;
                        XYdata[i, 1] = Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.robotstate.msg.pose.y;
                       // long ltim = Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.lookahead.msg.header.stamp.secs;
                        XYdata[i, 2] = Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.lookahead.msg.point.x;
                        XYdata[i, 3] = Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.lookahead.msg.point.y;
                    }

                    strR005_T = onRobotDist_TableSubDataMake("R_005", XYdata, strR005_T, 0);
                    strR006_T = onRobotDist_TableSubDataMake("R_006", XYdata, strR006_T, 1);
                    strR007_T = onRobotDist_TableSubDataMake("R_007", XYdata, strR007_T, 2);
                    strR008_T = onRobotDist_TableSubDataMake("R_008", XYdata, strR008_T, 3);



                    Invoke(new MethodInvoker(delegate ()
                    {
                        txtLAD_005_1.Text = strR005_T[1, 1];
                        txtLAD_005_2.Text = strR005_T[2, 1];
                        txtLAD_005_3.Text = strR005_T[3, 1];

                        txtLAD_005_1_1.Text = strR005_T[1, 2];
                        txtLAD_005_2_1.Text = strR005_T[2, 2];
                        txtLAD_005_3_1.Text = strR005_T[3, 2];

                        txtLAD_005_1_2.Text = strR005_T[1, 3];
                        txtLAD_005_2_2.Text = strR005_T[2, 3];
                        txtLAD_005_3_2.Text = strR005_T[3, 3];

                        txtLAD_006_1.Text = strR006_T[0, 1];
                        txtLAD_006_2.Text = strR006_T[2, 1];
                        txtLAD_006_3.Text = strR006_T[3, 1];

                        txtLAD_006_1_1.Text = strR006_T[0, 2];
                        txtLAD_006_2_1.Text = strR006_T[2, 2];
                        txtLAD_006_3_1.Text = strR006_T[3, 2];

                        txtLAD_006_1_2.Text = strR006_T[0, 3];
                        txtLAD_006_2_2.Text = strR006_T[2, 3];
                        txtLAD_006_3_2.Text = strR006_T[3, 3];

                        txtLAD_008_1.Text = strR008_T[0, 1];
                        txtLAD_008_2.Text = strR008_T[1, 1];
                        txtLAD_008_3.Text = strR008_T[2, 1];

                        txtLAD_008_1_1.Text = strR008_T[0, 2];
                        txtLAD_008_2_1.Text = strR008_T[1, 2];
                        txtLAD_008_3_1.Text = strR008_T[2, 2];

                        txtLAD_008_1_2.Text = strR008_T[0, 3];
                        txtLAD_008_2_2.Text = strR008_T[1, 3];
                        txtLAD_008_3_2.Text = strR008_T[2, 3];

                        txtx1.Text = string.Format("{0:f2}", XYdata[3, 0]);
                        txty1.Text = string.Format("{0:f2}", XYdata[3, 1]);

                        txtx2.Text = string.Format("{0:f2}", XYdata[3, 2]);
                        txty2.Text = string.Format("{0:f2}", XYdata[3, 3]);

                    }));
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine("onRobotDist_TableMake2 err ==" + ex.Message.ToString());
            }
        }
        private string[,] onRobotDist_TableSubDataMake(string strsourcerobot, double[,] xydata, string[,] strTableTmp, int nskipidx)
        {
            try
            {
                double sourceX = 0;
                double sourceY = 0;
                double sourceLad_X = 0;
                double sourceLad_Y = 0;
                double targetX = 0;
                double targetY = 0;
                double targetLad_X = 0;
                double targetLad_Y = 0;

                double dR1_R2LAD = 0;
                double dR1_R2CenToLA = 0;
                double dR2_R1CenToLA = 0;
            
                sourceX = xydata[nskipidx, 0];
                sourceY = xydata[nskipidx, 1];
                sourceLad_X = xydata[nskipidx, 2];
                sourceLad_Y = xydata[nskipidx, 3];


                for (int i2 = 0; i2 < 4; i2++)
                {
                    if (nskipidx == i2)
                    { }
                    else
                    {
                        string strcheckrobot = strTableTmp[i2, (int)RobotdistTableColums.Robot_id];
                        targetX = xydata[i2, 0];
                        targetY = xydata[i2, 1];
                        targetLad_X = xydata[i2, 2];
                        targetLad_Y = xydata[i2, 3];

                        if (Data.Instance.Robot_work_info[strcheckrobot].robot_status_info.goalrunnigstatus.msg == null || Data.Instance.Robot_work_info[strsourcerobot].robot_status_info.goalrunnigstatus.msg==null)
                        {
                            dR1_R2LAD = 0;
                            dR1_R2CenToLA = 0;
                            dR2_R1CenToLA = 0;
                        }
                        else
                        {
                            bool bcheck = false;
                            bool bcheck2 = false;
                            int nidx1 = Data.Instance.Robot_work_info[strsourcerobot].robot_status_info.goalrunnigstatus.msg.status_list.Count;

                            if (nidx1 > 0)
                            {
                                if (Data.Instance.Robot_work_info[strsourcerobot].robot_status_info.goalrunnigstatus.msg.status_list[nidx1 - 1].status != 1)
                                {
                                    targetLad_X = targetX;
                                    targetLad_Y = targetY;
                                    //bcheck = true;
                                }
                            }
                            else
                            {
                                targetLad_X = targetX;
                                targetLad_Y = targetY;
                            }
                            int nidx = Data.Instance.Robot_work_info[strcheckrobot].robot_status_info.goalrunnigstatus.msg.status_list.Count;
                            if (nidx > 0)
                            {
                                if (Data.Instance.Robot_work_info[strcheckrobot].robot_status_info.goalrunnigstatus.msg.status_list[nidx - 1].status != 1)
                                {
                                    sourceLad_X = sourceX;
                                    sourceLad_Y = sourceY;
                                   // bcheck2 = true;
                                }
                            }
                            else
                            {
                                sourceLad_X = sourceX;
                                sourceLad_Y = sourceY;
                            }

                            dR1_R2LAD = onPointToPointDist(sourceLad_X, sourceLad_Y, targetLad_X, targetLad_Y);
                            dR1_R2CenToLA = onPointToPointDist(sourceX, sourceY, targetLad_X, targetLad_Y);
                            dR2_R1CenToLA = onPointToPointDist(targetX, targetY, sourceLad_X, sourceLad_Y);
                        }

                        strTableTmp[i2, (int)RobotdistTableColums.R1_R2_LAD] = string.Format("{0:f2}", dR1_R2LAD);
                        strTableTmp[i2, (int)RobotdistTableColums.R1_R2_CenToLA] = string.Format("{0:f2}", dR1_R2CenToLA);
                        strTableTmp[i2, (int)RobotdistTableColums.R2_R1_CenToLA] = string.Format("{0:f2}", dR2_R1CenToLA);
                    }
                }

                return strTableTmp;
            }
            catch (Exception ex)
            {
                Console.WriteLine("onRobotDist_TableSubDataMake err ==" + ex.Message.ToString());
                return strTableTmp;
            }
        }

        private void onCrashCheck2()
        {
            onRobotsCrashCheck("R_005", 0, strR005_T);
            onRobotsCrashCheck("R_006", 0, strR006_T);
            onRobotsCrashCheck("R_007", 0, strR007_T);
            onRobotsCrashCheck("R_008", 0, strR008_T);
        }
        private void onRobotsCrashCheck(string strsourcerobot, int nskipidx, string[,] strTableTmp)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    if (Data.Instance.Robot_work_info.ContainsKey(strsourcerobot))
                    {
                        string strR1 = strsourcerobot;
                        string strR2 = "";
                        for (int i = 0; i < 4; i++)
                        {
                            if (nskipidx == i) continue;

                            
                            strR2 = strTableTmp[i, (int)RobotdistTableColums.Robot_id];

                            double r1_r2Lad = double.Parse(strTableTmp[i, (int)RobotdistTableColums.R1_R2_LAD]);
                            double r1_r2CenToLad = double.Parse(strTableTmp[i, (int)RobotdistTableColums.R1_R2_CenToLA]);
                            double r2_r1CenToLad = double.Parse(strTableTmp[i, (int)RobotdistTableColums.R2_R1_CenToLA]);


                            if (Data.Instance.Robot_work_info[strR1].robot_status_info.goalrunnigstatus.msg == null) break; ;

                            int nidx2 = Data.Instance.Robot_work_info[strR1].robot_status_info.goalrunnigstatus.msg.status_list.Count;
                            if (nidx2 > 0)
                            {
                          
                                if (Data.Instance.Robot_work_info[strR1].robot_status_info.goalrunnigstatus.msg.status_list[nidx2 - 1].status == 3)
                                {
                                    break;
                                }
                            }
                            if (Data.Instance.Robot_work_info[strR2].robot_status_info.goalrunnigstatus.msg == null) break;

                            int nidx = Data.Instance.Robot_work_info[strR2].robot_status_info.goalrunnigstatus.msg.status_list.Count;
                            if (nidx > 0)
                            {
                                if (Data.Instance.Robot_work_info[strR2].robot_status_info.goalrunnigstatus.msg.status_list[nidx - 1].status ==3)
                                {
                                    break;
                                }
                            }
                            


                         
                            if(r1_r2Lad < 1.0 && r1_r2Lad >0) //crash
                            {
                                strTableTmp[i, 4] = "crash";

                                if (r1_r2CenToLad < r2_r1CenToLad) //r1 move, r2 pause
                                {
                                    Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                    workinfo.Add(strR2, "");
                                    onJobPause(workinfo);

                                    bPauseTable[i] = true;

                                    workinfo.Clear();
                                    workinfo.Add(strR1, "");
                                    onJobRestart(workinfo);
                                }
                                else //r2 move r1 pause
                                {
                                    Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                    workinfo.Add(strR1, "");
                                    onJobPause(workinfo);
                                    bPauseTable[nskipidx] = true;

                                    workinfo.Clear();
                                    workinfo.Add(strR2, "");
                                    onJobRestart(workinfo);
                                }
                            }
                            else if(r1_r2Lad > 0)
                            {
                                if(strTableTmp[i, 4] == "crash")
                                {
                                    Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                
                                    workinfo.Add(strR1, "");
                                    workinfo.Add(strR2, "");
                                    onJobRestart(workinfo);
                                    bPauseTable[i] = false;
                                    strTableTmp[i, 4] = "ok";
                                 }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("onRobotsCrashCheck err ==" + ex.Message.ToString());
            }
        }
        */
        #endregion

        private void onCrashCheck()
        {
            try
            {
                if (Data.Instance.isConnected)
                {

                    Invoke(new MethodInvoker(delegate ()
                    {
                        textBox1.Text = string.Format("{0}", Data.Instance.Robot_work_info["R_005"].nActionidx);
                        textBox2.Text = string.Format("{0}", Data.Instance.Robot_work_info["R_006"].nActionidx);
                        textBox3.Text = string.Format("{0}", Data.Instance.Robot_work_info["R_008"].nActionidx);

                    }));

                    if (Data.Instance.Robot_work_info.ContainsKey("R_005"))
                    {
                        if (Data.Instance.Robot_work_info["R_005"].nActionidx == 1)
                        {
                            if ((Data.Instance.Robot_work_info["R_006"].nActionidx < 4 && Data.Instance.Robot_work_info["R_006"].nActionidx > 1) && !bR005pause)
                            {
                                Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                workinfo.Add("R_005", "");
                                onJobPause(workinfo);
                                bR005pause = true;
                                Invoke(new MethodInvoker(delegate ()
                                {
                                    textBox4.Text = "pause";
                                }));
                            }
                            else if (Data.Instance.Robot_work_info["R_006"].nActionidx > 3 && bR005pause)
                            {
                                Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                workinfo.Add("R_005", "");
                                onJobRestart(workinfo);
                                bR005pause = false;
                                Invoke(new MethodInvoker(delegate ()
                                {
                                    textBox4.Text = "restart";
                                }));
                            }

                        }
                        

                    }

                    if (Data.Instance.Robot_work_info.ContainsKey("R_006"))
                    {
                        if (Data.Instance.Robot_work_info["R_006"].nActionidx == 1)
                        {
                            if ((Data.Instance.Robot_work_info["R_005"].nActionidx < 4 && Data.Instance.Robot_work_info["R_005"].nActionidx > 1)&&!bR006pause)
                            {
                                Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                workinfo.Add("R_006", "");
                                onJobPause(workinfo);
                                bR006pause = true;
                                Invoke(new MethodInvoker(delegate ()
                                {
                                    textBox5.Text = "pause";
                                }));
                            }
                            else if (Data.Instance.Robot_work_info["R_005"].nActionidx > 3 && bR006pause)
                            {
                                Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                workinfo.Add("R_006", "");
                                onJobRestart(workinfo);
                                bR006pause = false;
                                Invoke(new MethodInvoker(delegate ()
                                {
                                    textBox5.Text = "restart";
                                }));

                            }
                        }

                    }
                    /*
                    if (Data.Instance.Robot_work_info.ContainsKey("R_008"))
                    {
                        if (Data.Instance.Robot_work_info["R_008"].nActionidx == 1)
                        {
                            if ((Data.Instance.Robot_work_info["R_005"].nActionidx == 6 || Data.Instance.Robot_work_info["R_006"].nActionidx == 6)&& !bR008pause)
                            {
                                npauselimit_Cnt = 0;
                                Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                workinfo.Add("R_008", "");
                                onJobPause(workinfo);
                                bR008pause = true;
                                Invoke(new MethodInvoker(delegate ()
                                {
                                    textBox6.Text = "pause";
                                }));
                            }
                            else if ((Data.Instance.Robot_work_info["R_005"].nActionidx == 6 || Data.Instance.Robot_work_info["R_006"].nActionidx == 6) && bR008pause)
                            {
                                npauselimit_Cnt++;
                                if(npauselimit_Cnt > 50)
                                {
                                    Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                    workinfo.Add("R_008", "");
                                    onJobRestart(workinfo);
                                    bR008pause = false;
                                    Thread.Sleep(5000);
                                    Invoke(new MethodInvoker(delegate ()
                                    {
                                        textBox6.Text = "restart";
                                    }));

                                    npauselimit_Cnt = 0;
                                }
                               
                            }
                            else if ((Data.Instance.Robot_work_info["R_005"].nActionidx != 6 || Data.Instance.Robot_work_info["R_006"].nActionidx != 6) && bR008pause)
                            {
                                npauselimit_Cnt = 0;
                                Dictionary<string, string> workinfo = new Dictionary<string, string>();
                                workinfo.Add("R_008", "");
                                onJobRestart(workinfo);
                                bR008pause = false;
                                Invoke(new MethodInvoker(delegate ()
                                {
                                    textBox6.Text = "restart";
                                }));
                            }
                        }

                    }
                    */
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("onCrashCheck err ==" + ex.Message.ToString());
            }
        }
        #endregion

        #region UR

        string m_strURRobotName = "R_007";

        string[,] strR007_UR_T = new string[3, 5]; // robot id,robot dist, robot x, robot y,  robot theta

        bool b500_1_urzone = false;
        bool bUR_runflag = false;
        bool bUR_obstacle = false;

        private void GlobalpathComplete(string strrobotid)
        {
            if (strrobotid == "R_005")
            {
                if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.globalplan.msg.poses.Count > 0)
                {
                    List<PoseStamped> pose = new List<PoseStamped>();
                    pose = Data.Instance.Robot_work_info[strrobotid].robot_status_info.globalplan.msg.poses;

                    if(b500_1_urzone)
                    {
                        bUR_runflag = true;
                    }
                }
            }
        }

        double urdist1=0, urdist_half =0;
        private void onURZone_RobotCheck()
        {
            if (chkURAbstacleMode.Checked)
            {
                double pos_x = Data.Instance.Robot_work_info["R_005"].robot_status_info.robotstate.msg.pose.x;
                double pos_y = Data.Instance.Robot_work_info["R_005"].robot_status_info.robotstate.msg.pose.y;

                double ur_x = Data.Instance.Robot_work_info["R_007"].robot_status_info.robotstate.msg.pose.x;
                double ur_y = Data.Instance.Robot_work_info["R_007"].robot_status_info.robotstate.msg.pose.y;

                string strPlowing_Pos_File = "..\\Ros_info\\Robotplowing_Pos.txt";

                using (StreamReader sr1 = new System.IO.StreamReader(strPlowing_Pos_File, Encoding.Default))
                {
                    int ncnt = 0;
                    while (sr1.Peek() >= 0)
                    {
                        string strTemp = sr1.ReadLine();

                        string[] strdata = strTemp.Split(':');
                        string[] strsubdata = strdata[1].Split(',');

                        plowing_posArray[ncnt, 0] = double.Parse(strsubdata[0]);
                        plowing_posArray[ncnt, 1] = double.Parse(strsubdata[1]);

                        ncnt++;
                    }
                }

                double check_x = plowing_posArray[2, 0];
                double check_y = plowing_posArray[2, 1];

                //double x1 =pos_x - check_x;
                //double y1 = pos_y - check_y;
                double dist = onPointToPointDist(pos_x, pos_y, check_x, check_y);
                urdist_half = onPointToPointDist(pos_x, pos_y, ur_x, ur_y);

                if ((dist < 0.1)&& !b500_1_urzone)
                {
                    urdist1 = 0;
                    b500_1_urzone = true;

                    urdist1 = onPointToPointDist(pos_x, pos_y, ur_x, ur_y);
                }

                if (b500_1_urzone && !bUR_obstacle)

                {
                    //ur장애물
                    //onURObstacle();
                    Console.WriteLine("장애물");
                    //onURObstacle();
                    bUR_runflag = false;
                    bUR_obstacle = true;
                }

                if((urdist1/2 < urdist_half) && bUR_obstacle)
                {
                    timer_URAbstacle.Enabled = true;
                    bUR_obstacle = false;
                }

              /*  if ((pos_y > ur_y) && bUR_obstacle)
                {
                    Console.WriteLine("장애물해제");
                    //ur 홈 
                    bUR_obstacle = false;
                    b500_1_urzone = false;
                }*/


            }
            else
            {
                b500_1_urzone = false;
                bUR_runflag = false;
                bUR_obstacle = false;
                urdist1 = 0;
                urdist_half = 0;
            }
        }

        public void onUR_Abstacle_Tableinitial()
        {
            try
            {
                strR007_UR_T[0, (int)RobotdistTableColums.Robot_id] = "R_005";
                strR007_UR_T[1, (int)RobotdistTableColums.Robot_id] = "R_006";
                strR007_UR_T[2, (int)RobotdistTableColums.Robot_id] = "R_008";

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 1; j < 5; j++)
                    {
                        strR007_UR_T[i, j] = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("장애물해제");

                b500_1_urzone = false;
                bUR_runflag = false;
                bUR_obstacle = false;
                urdist1 = 0;
                urdist_half = 0;
            }
        }

        private void timer_URAbstacleTable_Tick(object sender, EventArgs e)
        {
            //onUR_Abstacle_CheckTableMake();
            //onUR_Abstacle_Run();

            onURZone_RobotCheck();
        }
        private void timer_URAbstacle_Tick(object sender, EventArgs e)
        {
            timer_URAbstacle.Enabled = false;
            // onFolding();
            Console.WriteLine("장애물해제");
            // babstacle = false;

        }

        public void onUR_Abstacle_CheckTableMake()
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    if (Data.Instance.Robot_work_info["R_007"].robot_status_info.robotstate.msg == null) return;

                    double ur_x = Data.Instance.Robot_work_info["R_007"].robot_status_info.robotstate.msg.pose.x;
                    double ur_y = Data.Instance.Robot_work_info["R_007"].robot_status_info.robotstate.msg.pose.y;
                    double ur_theta = Data.Instance.Robot_work_info["R_007"].robot_status_info.robotstate.msg.pose.theta;

                    string[] strrobotllist = { "R_005", "R_006", "R_008" };

                    for (int i = 0; i < strrobotllist.Length; i++)
                    {

                        if (Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.robotstate.msg == null) continue;


                        double pos_x = Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.robotstate.msg.pose.x;
                        double pos_y = Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.robotstate.msg.pose.y;
                        double pos_theta = Data.Instance.Robot_work_info[strrobotllist[i]].robot_status_info.robotstate.msg.pose.theta;

                        strR007_UR_T[i, (int)URdistTableColums.R1_R2_Dist] = string.Format("{0:f2}", onPointToPointDist(ur_x, ur_y, pos_x, pos_y));
                        strR007_UR_T[i, (int)URdistTableColums.R2_X] = string.Format("{0:f2}", pos_x);
                        strR007_UR_T[i, (int)URdistTableColums.R2_Y] = string.Format("{0:f2}", pos_y);

                        strR007_UR_T[i, (int)URdistTableColums.R2_Theta] = string.Format("{0:f2}", (pos_theta * 180 / 3.14));
                    }
                    Invoke(new MethodInvoker(delegate ()
                    {
                        txtUr_005dist.Text = strR007_UR_T[0, 1];
                        txtUr_006dist.Text = strR007_UR_T[1, 1];
                        txtUr_008dist.Text = strR007_UR_T[2, 1];
                    }));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("onUR_Abstacle_CheckTableMake err ==" + ex.Message.ToString());
            }
        }
        bool babstacle = false;
        public void onUR_Abstacle_Run()
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    if (chkURAbstacleMode.Checked)
                    {
                        if (babstacle) return;

                        if (Data.Instance.Robot_work_info["R_007"].robot_status_info.ur_status.msg == null) return;

                        double ur_x = Data.Instance.Robot_work_info["R_007"].robot_status_info.robotstate.msg.pose.x;
                        double ur_y = Data.Instance.Robot_work_info["R_007"].robot_status_info.robotstate.msg.pose.y;

                        string urstatus = Data.Instance.Robot_work_info["R_007"].robot_status_info.ur_status.msg.status;

                        for (int i = 0; i < 3; i++)
                        {

                            double dist = double.Parse(strR007_UR_T[i, (int)URdistTableColums.R1_R2_Dist]);
                            double pos_x = double.Parse(strR007_UR_T[i, (int)URdistTableColums.R2_X]);
                            double pos_y = double.Parse(strR007_UR_T[i, (int)URdistTableColums.R2_Y]);
                            double theta = double.Parse(strR007_UR_T[i, (int)URdistTableColums.R2_Theta]);

                            if (dist > 3)
                            {

                            }
                            else if ((2 < dist && dist < 3) && (theta > 80 && theta < 95) && (pos_x > 2.5) && (pos_y < ur_y))
                            {
                                //abstacle run 
                                onURObstacle();
                                //abstacle timer
                                //timer_URAbstacle.Enabled = true;
                                babstacle = true;
                                break;
                            }
                            else
                            {
                                //abstacle wait
                                onURObstacleWait();
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("onUR_Abstacle_Run err ==" + ex.Message.ToString());
            }
        }

        private void onURObstacle()
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    m_strURRobotName = cboURRobot.SelectedItem.ToString();
                    string[] strSelectRobot_Worker;
                    string[] strSelectworkdata_Worker;
                    string strworkname = "UR_obstacle";
                    string strworkid = "UR_obstacle1";
                    int nworkcnt = 1;

                    strSelectRobot_Worker = new string[1];
                    strSelectworkdata_Worker = new string[1];
                    strSelectRobot_Worker[0] = m_strURRobotName;

                    string strmsg = "type:URMISSION/mode:1/countnumber:1/ur_command:obstacle";
                    strSelectworkdata_Worker[0] = strmsg;

                    int nactidx = 0;
                    var task = Task.Run(() => worker.onWorkOrder_publish(strworkname, strworkid, strSelectRobot_Worker, strSelectworkdata_Worker, nworkcnt, nactidx));
                    Thread.Sleep(200);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onObstacle err :={0}", ex.Message.ToString());
            }
        }

        private void onURObstacleWait()
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    m_strURRobotName = cboURRobot.SelectedItem.ToString();
                    string[] strSelectRobot_Worker; //워크작업으로 전달하기 위해 형식 맞춤.. 
                    string[] strSelectworkdata_Worker;
                    string strworkname = "UR_homing";
                    string strworkid = "UR_homing1";
                    int nworkcnt = 1;

                    strSelectRobot_Worker = new string[1];
                    strSelectworkdata_Worker = new string[1];
                    strSelectRobot_Worker[0] = m_strURRobotName;

                    string strmsg = "type:URMISSION/mode:1/countnumber:1/ur_command:homing";
                    //strSelectworkdata_Worker[0] = "UR homing test";
                    strSelectworkdata_Worker[0] = strmsg;

                    int nactidx = 0;
                    var task = Task.Run(() => worker.onWorkOrder_publish(strworkname, strworkid, strSelectRobot_Worker, strSelectworkdata_Worker, nworkcnt, nactidx));

                    onbtnDelay(btnHoming, 500);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onURObstacleWait err :={0}", ex.Message.ToString());
            }
        }



        public void onURListmsg(string msg)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                listBox_UR.Items.Add(msg);

                if (listBox_UR.Items.Count > 100)
                    listBox_UR.Items.Clear();
            }));
        }
        public void updateURListDP(string strtopic, string msg)
        {
            onURListmsg(string.Format("topic={0}..data={1}", strtopic, msg));
        }

        private void btnAssembly_Click(object sender, EventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    m_strURRobotName = cboURRobot.SelectedItem.ToString();

                    string[] strSelectRobot_Worker; //워크작업으로 전달하기 위해 형식 맞춤.. 
                    string[] strSelectworkdata_Worker;
                    string strworkname = "UR_assembly";
                    string strworkid = "UR_assembly1";
                    int nworkcnt = 1;

                    strSelectRobot_Worker = new string[1];
                    strSelectworkdata_Worker = new string[1];
                    strSelectRobot_Worker[0] = m_strURRobotName;

                    string strmsg = "type:URMISSION/mode:1/countnumber:1/ur_command:assembly";
                    //strSelectworkdata_Worker[0] = "UR assembly test";
                    strSelectworkdata_Worker[0] = strmsg;

                    int nactidx = 0;
                    var task = Task.Run(() => worker.onWorkOrder_publish(strworkname, strworkid, strSelectRobot_Worker, strSelectworkdata_Worker, nworkcnt, nactidx));

                    onbtnDelay(btnAssembly, 500);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("btnAssembly_Click err :={0}", ex.Message.ToString());
            }
        }

        private void onFolding()
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    m_strURRobotName = cboURRobot.SelectedItem.ToString();
                    string[] strSelectRobot_Worker; //워크작업으로 전달하기 위해 형식 맞춤.. 
                    string[] strSelectworkdata_Worker;
                    string strworkname = "UR_folding";
                    string strworkid = "UR_folding1";
                    int nworkcnt = 1;

                    strSelectRobot_Worker = new string[1];
                    strSelectworkdata_Worker = new string[1];
                    strSelectRobot_Worker[0] = m_strURRobotName;

                    string strmsg = "type:URMISSION/mode:1/countnumber:1/ur_command:folding";
                    //strSelectworkdata_Worker[0] = "UR folding test";
                    strSelectworkdata_Worker[0] = strmsg;

                    int nactidx = 0;
                    var task = Task.Run(() => worker.onWorkOrder_publish(strworkname, strworkid, strSelectRobot_Worker, strSelectworkdata_Worker, nworkcnt, nactidx));
                    Thread.Sleep(200);
                    
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onFolding err :={0}", ex.Message.ToString());
            }

        }
        private async void btnFolding_Click(object sender, EventArgs e)
        {
            onFolding();
            onbtnDelay(btnFolding, 500);

        }

        private async void btnHoming_Click(object sender, EventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    m_strURRobotName = cboURRobot.SelectedItem.ToString();
                    string[] strSelectRobot_Worker; //워크작업으로 전달하기 위해 형식 맞춤.. 
                    string[] strSelectworkdata_Worker;
                    string strworkname = "UR_homing";
                    string strworkid = "UR_homing1";
                    int nworkcnt = 1;

                    strSelectRobot_Worker = new string[1];
                    strSelectworkdata_Worker = new string[1];
                    strSelectRobot_Worker[0] = m_strURRobotName;

                    string strmsg = "type:URMISSION/mode:1/countnumber:1/ur_command:homing";
                    //strSelectworkdata_Worker[0] = "UR homing test";
                    strSelectworkdata_Worker[0] = strmsg;

                    int nactidx = 0;
                    var task = Task.Run(() => worker.onWorkOrder_publish(strworkname, strworkid, strSelectRobot_Worker, strSelectworkdata_Worker, nworkcnt, nactidx));

                    onbtnDelay(btnHoming, 500);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("btnHoming_Click err :={0}", ex.Message.ToString());
            }
        }

        private void btnObstacle_Click(object sender, EventArgs e)
        {
            onURObstacle();
        }

        private void btnWaving_Click(object sender, EventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    m_strURRobotName = cboURRobot.SelectedItem.ToString();
                    string[] strSelectRobot_Worker; //워크작업으로 전달하기 위해 형식 맞춤.. 
                    string[] strSelectworkdata_Worker;
                    string strworkname = "UR_waving";
                    string strworkid = "UR_waving1";
                    int nworkcnt = 1;

                    strSelectRobot_Worker = new string[1];
                    strSelectworkdata_Worker = new string[1];
                    strSelectRobot_Worker[0] = m_strURRobotName;

                    int nrepeatcnt = 1;//= (int)numericUpDown_UR_RepeatCnt.Value;

                    string strmsg = string.Format("type:URMISSION/mode:1/countnumber:{0}/ur_command:waving", nrepeatcnt);
                    //strSelectworkdata_Worker[0] = "UR waving test";
                    strSelectworkdata_Worker[0] = strmsg;

                    int nactidx = 0;
                    var task = Task.Run(() => worker.onWorkOrder_publish(strworkname, strworkid, strSelectRobot_Worker, strSelectworkdata_Worker, nworkcnt, nactidx));

                   // onbtnDelay(btnWaving, 500);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("btnWaving_Click err :={0}", ex.Message.ToString());
            }
        }

        private void btnWriting_Click(object sender, EventArgs e)
        {

        }

        private async void btnURStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    m_strURRobotName = cboURRobot.SelectedItem.ToString();
                    string[] strSelectRobot_Worker; //워크작업으로 전달하기 위해 형식 맞춤.. 
                    string[] strSelectworkdata_Worker;
                    string strworkname = "UR_stop";
                    string strworkid = "UR_stop1";
                    int nworkcnt = 1;

                    strSelectRobot_Worker = new string[1];
                    strSelectworkdata_Worker = new string[1];
                    strSelectRobot_Worker[0] = m_strURRobotName;

                    string strmsg = "type:URMISSION/mode:0/countnumber:0/ur_switch_to:stop";
                    //strSelectworkdata_Worker[0] = "UR stop test";
                    strSelectworkdata_Worker[0] = strmsg;

                    int nactidx = 0;
                    var task = Task.Run(() => worker.onWorkOrder_publish(strworkname, strworkid, strSelectRobot_Worker, strSelectworkdata_Worker, nworkcnt, nactidx));

                    onbtnDelay(btnStop, 500);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("btnStop_Click err :={0}", ex.Message.ToString());
            }
        }

        private void onbtnDelay(Button btn, int ndelaytime)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                btn.Enabled = false;
                Thread.Sleep(ndelaytime);
                btn.Enabled = true;
            }));

        }
        private void onUrStatus_DP()
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    m_strURRobotName = cboURRobot.SelectedItem.ToString();
                    if (Data.Instance.Robot_work_info.ContainsKey(m_strURRobotName))
                    {
                        string strtopic = "";
                        string strstatus = "";
                        if (Data.Instance.Robot_work_info[m_strURRobotName].robot_status_info.ur_status == null) return;
                        if (Data.Instance.Robot_work_info[m_strURRobotName].robot_status_info.ur_status.msg == null) return;


                        if (Data.Instance.Robot_work_info[m_strURRobotName].robot_status_info.ur_status.msg != null)
                        {
                            strtopic = Data.Instance.Robot_work_info[m_strURRobotName].robot_status_info.ur_status.topic;
                            strstatus = Data.Instance.Robot_work_info[m_strURRobotName].robot_status_info.ur_status.msg.status;

                            List<string> strname = Data.Instance.Robot_work_info[m_strURRobotName].robot_status_info.ur_status.msg.arm_status.name;
                            List<float> position = Data.Instance.Robot_work_info[m_strURRobotName].robot_status_info.ur_status.msg.arm_status.position;
                            List<float> velocity = Data.Instance.Robot_work_info[m_strURRobotName].robot_status_info.ur_status.msg.arm_status.velocity;

                            string strparam = "";

                            for (int i = 0; i < strname.Count; i++)
                            {
                                strparam += string.Format("name:{0},position:{1:f2},velocity:{2:f2}", strname[i], position[i], velocity[i]);
                            }


                            string strmsg = string.Format("topic:{0},status:{1},param={2}", strtopic, strstatus, strparam);

                            updateURListDP(strtopic, strmsg);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("timer1_Tick err :={0}", ex.Message.ToString());
            }

        }


        #endregion

        private void onStopPauseRestart_disable()
        {
            m_bStop = false;
            m_bPause = false;
            m_bRestart = false;

            bCombine_RunFirst = false;
        }

        private void onBtn_enable(bool _b)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                btnWaitPos.Enabled = _b;
                btnInitPosMove.Enabled = _b;
                btnStartPos.Enabled = _b;
                btnJob1.Enabled = _b;
               
                btnRun.Enabled = _b;

                btnGatheringRobot.Enabled = _b;
                btnPlowingRobot.Enabled = _b;
                btnRunRobot.Enabled = _b;
            })); 
        }
        #region test
        private void onWaitingpos_RobotInfo_Check()
        {

            // 파일에서 읽어 테이블을 갱신한다. 

            Data.Instance.waitingpos_table.waitpos_robotinfo = new List<WaitingPos_RobotInfo>();

            try
            {
                if (!File.Exists(m_strWaitPosJob_File))
                {
                    using (StreamWriter sw = new System.IO.StreamWriter(m_strWaitPosJob_File, false, Encoding.Default))
                    {
                        sw.WriteLine("robot_id,robot_name,work id,work_name,work_cnt");
                        sw.Close();
                    }
                    //  return;
                }

                dataGridView_InitJob.Rows.Clear();


                using (StreamReader sr1 = new System.IO.StreamReader(m_strWaitPosJob_File, Encoding.Default))
                {
                    int ncnt = 0; //파일에 첫줄은 항목명으로 빼고 읽기 위해 선언
                    //Data.Instance.totalJobschedule.jobInfo = new List<JobInfo>();
                    while (sr1.Peek() >= 0)
                    {
                        string strTemp = sr1.ReadLine();
                        if (ncnt != 0)
                        {
                      /*      string[] strjobinfo = strTemp.Split(',');
                            JobInfo jobinfo = new JobInfo();
                            jobinfo.strJobname = strjobinfo[0];
                            if (strjobinfo[1] == "순서")
                                jobinfo.nJobType = (int)JOB_TYPE.sequence;
                            else if (strjobinfo[1] == "동시")
                                jobinfo.nJobType = (int)JOB_TYPE.synchronous;

                            Data.Instance.totalJobschedule.jobInfo.Add(jobinfo);



                            dataGridView_Jobschedule.Rows.Add(strjobinfo);
                            */

                        }
                        ncnt++;
                    }
                }            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onJobScheduleCheck err :={0}", ex.Message.ToString());
            }


            WaitingPos_RobotInfo waitposrobotinfo = new WaitingPos_RobotInfo();
            waitposrobotinfo.strRobotID = "R_002";
            waitposrobotinfo.strRobotName = "TR200";
            waitposrobotinfo.strMisssionID = "WID021404";
            waitposrobotinfo.strMisssionName = "TR50 대기장소 이동";
            waitposrobotinfo.nCnt = 1;

            Data.Instance.waitingpos_RobotsInfo.Add("대기위치", waitposrobotinfo);
            Data.Instance.waitingpos_table.waitpos_robotinfo.Add(waitposrobotinfo);

            waitposrobotinfo = new WaitingPos_RobotInfo();
            waitposrobotinfo.strRobotID = "R_001";
            waitposrobotinfo.strRobotName = "TR50";
            waitposrobotinfo.strMisssionID = "WID021405";
            waitposrobotinfo.strMisssionName = "TR200 대기장소 이동";
            waitposrobotinfo.nCnt = 1;
            Data.Instance.waitingpos_RobotsInfo.Add("대기위치", waitposrobotinfo);
            Data.Instance.waitingpos_table.waitpos_robotinfo.Add(waitposrobotinfo);

            waitposrobotinfo = new WaitingPos_RobotInfo();
            waitposrobotinfo.strRobotID = "R_004";
            waitposrobotinfo.strRobotName = "200P";
            waitposrobotinfo.strMisssionID = "WID021406";
            waitposrobotinfo.strMisssionName = "200P 대기장소 이동";
            waitposrobotinfo.nCnt = 1;
            Data.Instance.waitingpos_RobotsInfo.Add("대기위치", waitposrobotinfo);
            Data.Instance.waitingpos_table.waitpos_robotinfo.Add(waitposrobotinfo);
            
            //화면에 표시 
        }

        private void onJobpos_RobotInfo_Check()
        {
            // 파일에서 읽어 테이블을 갱신한다. 

            Data.Instance.jobpos_table.jobpos_robotinfo = new List<JobPos_RobotInfo>();

            Data.Instance.jobpos_table.jobpos_robotinfo.Clear();

            JobPos_RobotInfo jobposrobotinfo = new JobPos_RobotInfo();
            jobposrobotinfo.strRobotID = "R_004";
            jobposrobotinfo.strRobotName = "200P";
            jobposrobotinfo.strMisssionID = "WID021403";
            jobposrobotinfo.strMisssionName = "200P 작업1";
            jobposrobotinfo.nCnt = 2; //numericUpDown_RepeatCnt
            Data.Instance.jobos_robotsInfo.Add("작업1", jobposrobotinfo);
            Data.Instance.jobpos_table.jobpos_robotinfo.Add(jobposrobotinfo);

            jobposrobotinfo = new JobPos_RobotInfo();
            jobposrobotinfo.strRobotID = "R_002";
            jobposrobotinfo.strRobotName = "TR200";
            jobposrobotinfo.strMisssionID = "WID021401";
            jobposrobotinfo.strMisssionName = "TR200 작업1";
            jobposrobotinfo.nCnt = 2;
            Data.Instance.jobos_robotsInfo.Add("작업1", jobposrobotinfo);
            Data.Instance.jobpos_table.jobpos_robotinfo.Add(jobposrobotinfo);

            jobposrobotinfo = new JobPos_RobotInfo();
            jobposrobotinfo.strRobotID = "R_001";
            jobposrobotinfo.strRobotName = "TR50";
            jobposrobotinfo.strMisssionID = "WID021402";
            jobposrobotinfo.strMisssionName = "TR50 작업1";
            jobposrobotinfo.nCnt = 2;
            Data.Instance.jobos_robotsInfo.Add("작업1", jobposrobotinfo);
            Data.Instance.jobpos_table.jobpos_robotinfo.Add(jobposrobotinfo);

            //화면에 표시 
        }
        #endregion

        


        #region 수동 동작 탭 관련

        private void onManualRun(Twist data)
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    rosinterface ros = new rosinterface();
                    TopicList list = new TopicList();

                   

                    string strrobot = (string)cboRobotID.SelectedItem;

                    string strobj = JsonConvert.SerializeObject(data);
                    JObject obj = JObject.Parse(strobj);


                    ros.PublisherTopicMsgtype(strrobot + list.topic_cmdvel, list.msg_cmdvel);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);
                    ros.publisher(obj);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine("pictureBox_up_left_diagonal_MouseDown err :={0}", ex.Message.ToString());
                }
            }
        }

        private void onManualRun(Twist data, string strrobot)
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    rosinterface ros = new rosinterface();
                    TopicList list = new TopicList();



                   // string strrobot = (string)cboRobotID.SelectedItem;

                    string strobj = JsonConvert.SerializeObject(data);
                    JObject obj = JObject.Parse(strobj);


                    ros.PublisherTopicMsgtype(strrobot + list.topic_cmdvel, list.msg_cmdvel);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);
                    ros.publisher(obj);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine("pictureBox_up_left_diagonal_MouseDown err :={0}", ex.Message.ToString());
                }
            }
        }
        private void pictureBox_up_left_diagonal_MouseDown(object sender, MouseEventArgs e)
        {
            Twist data = new Twist();
            data.linear.x = 0.3;
            data.angular.z = 0.5;
            onManualRun(data);
        }

        private void pictureBox_up_MouseDown(object sender, MouseEventArgs e)
        {
            Twist data = new Twist();
            data.linear.x = 0.3;
            data.angular.z = 0;
            onManualRun(data);
        }

        private void pictureBox_up_right_diagonal_MouseDown(object sender, MouseEventArgs e)
        {
            Twist data = new Twist();
            data.linear.x = 0.3;
            data.angular.z = -0.5;
            onManualRun(data);
        }

        private void pictureBox_left_MouseDown(object sender, MouseEventArgs e)
        {
            Twist data = new Twist();
            data.linear.x = 0;
            data.angular.z = 0.5;
            onManualRun(data);
        }

        private void pictureBox_right_MouseDown(object sender, MouseEventArgs e)
        {
            Twist data = new Twist();
            data.linear.x = 0;
            data.angular.z = -0.5;
            onManualRun(data);
        }

        private void pictureBox_down_left_diagonal_MouseDown(object sender, MouseEventArgs e)
        {
            Twist data = new Twist();
            data.linear.x = -0.3;
            data.angular.z = -0.5;
            onManualRun(data);
        }

        private void pictureBox_down_MouseDown(object sender, MouseEventArgs e)
        {
            Twist data = new Twist();
            data.linear.x = -0.3;
            data.angular.z = 0;
            onManualRun(data);
        }

        private void pictureBox_down_right_diagonal_MouseDown(object sender, MouseEventArgs e)
        {
            Twist data = new Twist();
            data.linear.x = -0.3;
            data.angular.z = 0.5;
            onManualRun(data);
        }

        private void onManualStop(object sender, MouseEventArgs e)
        {
            Twist data = new Twist();
            data.linear.x = 0;
            data.angular.z = 0;
            onManualRun(data);
        }

        private void btnR005liftup_Click(object sender, EventArgs e)
        {
            onLiftcontorl("R_005", "Top", btnR005liftup);
        }

        private void btnR005liftdown_Click(object sender, EventArgs e)
        {
            onLiftcontorl("R_005", "Bottom", btnR005liftdown);
        }

        private void btnR006liftup_Click(object sender, EventArgs e)
        {
            onLiftcontorl("R_006", "Top", btnR006liftup);
        }

        private void btnR006liftdown_Click(object sender, EventArgs e)
        {
            onLiftcontorl("R_006", "Bottom", btnR006liftdown);
        }

        private void onLiftcontorl(string strrobotid, string strlift,Button btn)
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
                    if (strlift=="Bottom")
                        strmsg = "type:Lift-Conveyor-Control/mode:Top-Bottom/action:Bottom";
                    else if (strlift == "Top")
                        strmsg = "type:Lift-Conveyor-Control/mode:Top-Bottom/action:Top";
                    //strSelectworkdata_Worker[0] = "UR assembly test";
                    strSelectworkdata_Worker[0] = strmsg;

                    int nactidx = 0;
                    var task = Task.Run(() => worker.onWorkOrder_publish(strworkname, strworkid, strSelectRobot_Worker, strSelectworkdata_Worker, nworkcnt, nactidx));

                    onbtnDelay(btn, 300);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("btnAssembly_Click err :={0}", ex.Message.ToString());
            }
        }
        #endregion

        private void WorkOrderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Robotstatus_Updatetimer.Enabled = false;
        }

        private void btnR005Set_Click(object sender, EventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    int nlift = cboR005_lift.SelectedIndex;

                    LiftStatus_Set_Arg lift = new LiftStatus_Set_Arg();

                    if(nlift==0)
                    {
                        lift.data = 2;
                    }
                    else if(nlift==1)
                    {
                        lift.data = -2;
                    }
                    string strobj = JsonConvert.SerializeObject(lift);
                    JObject obj = JObject.Parse(strobj);

                    rosinterface ros = new rosinterface();

                    TopicList list = new TopicList();
                    ros.PublisherTopicMsgtype("R_005"+list.topic_set_liftstatus, list.msg_set_liftstatus);
                    Thread.Sleep(100);
                    ros.publisher(obj);
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("btnR005Set_Click err :={0}", ex.Message.ToString());
            }
        }

        private void btnR006Set_Click(object sender, EventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    int nlift = cboR006_lift.SelectedIndex;

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
                    ros.PublisherTopicMsgtype("R_006" + list.topic_set_liftstatus, list.msg_set_liftstatus);
                    Thread.Sleep(100);
                    ros.publisher(obj);
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("btnR006Set_Click err :={0}", ex.Message.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

            //onDistTable_initial();

            //onRobotsDist_Cal();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void chkURAbstacleMode_CheckedChanged(object sender, EventArgs e)
        {
            if (chkURAbstacleMode.Checked)
                groupBox11.Enabled = false;
            else
                groupBox11.Enabled = true;
        }

        private void onGatheringRobot(bool bLastComback)
        {
            bRobotMissioning = false;
            onStopPauseRestart_disable();
            if (Data.Instance.isConnected)
            {
                try
                {
                    using (StreamWriter sw = new System.IO.StreamWriter(m_strJobschedule_File, false, Encoding.Default))
                    {
                        sw.WriteLine("jobname,jobtype");
                        sw.WriteLine("모여,순서,gatheringpos_job.txt");
                        sw.Close();
                    }
                    onJobScheduleCheck();
                    //onWaitingpos_RobotInfo_Check();

                    Data.Instance.robots_currgoing_status.Clear();
                    nTotalJobTablepos_index = 0;
                    nTotalJobTableEndpos_index = 1;

                    onSequencepos_RobotInfo_Check(nTotalJobTablepos_index);

                    nWaitingTableEndpos_index = Data.Instance.waitingpos_table.waitpos_robotinfo.Count();
                    if (!bLastComback)
                    {
                        int ncnt = Data.Instance.waitingpos_table.waitpos_robotinfo.Count();
                        string strMsg = "";
                        for (int i = 0; i < ncnt; i++)
                        {
                            string strrobotid = Data.Instance.waitingpos_table.waitpos_robotinfo[i].strRobotID;
                            string strrobotname = Data.Instance.waitingpos_table.waitpos_robotinfo[i].strRobotName;

                            strMsg += string.Format("{0}({1}) ", strrobotname, strrobotid);
                        }

                        strMsg += " 중심위치로 이동하시겠습니까?";

                        if (DialogResult.OK == MessageBox.Show(strMsg, "확인", MessageBoxButtons.OKCancel))
                        {
                            onBtn_enable(false);
                            m_strLog_File = DateTime.Now.ToString("yyyyMMdd") + ".txt";
                            onInitPosMove();
                        }
                    }
                    else
                    {
                        onBtn_enable(false);
                        m_strLog_File = DateTime.Now.ToString("yyyyMMdd") + ".txt";
                        onInitPosMove();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("onGatheringRobot" + ex.Message.ToString());
                }
            }
        }
        private void btnGatheringRobot_Click(object sender, EventArgs e)
        {
            onGatheringRobot(false);
        }

        private void btnPlowingRobot_Click(object sender, EventArgs e)
        {
            bRobotMissioning = false;
            onStopPauseRestart_disable();
            if (Data.Instance.isConnected)
            {
                try
                {
                    using (StreamWriter sw = new System.IO.StreamWriter(m_strJobschedule_File, false, Encoding.Default))
                    {
                        sw.WriteLine("jobname,jobtype");
                        sw.WriteLine("헤처,순서,plowingpos_job.txt");
                        sw.Close();
                    }
                    onJobScheduleCheck();
                    //onWaitingpos_RobotInfo_Check();

                    Data.Instance.robots_currgoing_status.Clear();
                    nTotalJobTablepos_index = 0;
                    nTotalJobTableEndpos_index = 1;

                    onSequencepos_RobotInfo_Check(nTotalJobTablepos_index);

                    nWaitingTableEndpos_index = Data.Instance.waitingpos_table.waitpos_robotinfo.Count();

                    

                    int ncnt = Data.Instance.waitingpos_table.waitpos_robotinfo.Count();
                    string strMsg = "";
                    for (int i = 0; i < ncnt; i++)
                    {
                        string strrobotid = Data.Instance.waitingpos_table.waitpos_robotinfo[i].strRobotID;
                        string strrobotname = Data.Instance.waitingpos_table.waitpos_robotinfo[i].strRobotName;

                        strMsg += string.Format("{0}({1}) ", strrobotname, strrobotid);
                    }

                    strMsg += " 퍼트리기 하시겠습니까?";

                    if (DialogResult.OK == MessageBox.Show(strMsg, "확인", MessageBoxButtons.OKCancel))
                    {
                        onBtn_enable(false);
                        m_strLog_File = DateTime.Now.ToString("yyyyMMdd") + ".txt";
                        onInitPosMove();

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("btnPlowingRobot_Click" + ex.Message.ToString());
                }
            }
        }

        private void btnRunRobot_Click(object sender, EventArgs e)
        {
            onDistTable_initial();
            

            onStopPauseRestart_disable();
            if (Data.Instance.isConnected)
            {
                try
                {
                    // onJobpos_RobotInfo_Check();
                    using (StreamWriter sw = new System.IO.StreamWriter(m_strJobschedule_File, false, Encoding.Default))
                    {
                        sw.WriteLine("jobname,jobtype");
                        sw.WriteLine("작업,동시,job1pos_job.txt");
                        sw.Close();
                    }
                    onJobScheduleCheck();

                    Data.Instance.robots_currgoing_status.Clear();

                    nTotalJobTablepos_index = 0;
                    nTotalJobTableEndpos_index = 1;  //동시작업은 인덱스가 1이다.
                                                     // nTotalJobTableEndpos_index = Data.Instance.jobpos_table.jobpos_robotinfo.Count();

                    onSynchronous_RobotInfo_Check(nTotalJobTablepos_index);
                    int ncnt = Data.Instance.jobpos_table.jobpos_robotinfo.Count();
                    string strMsg = "";
                    for (int i = 0; i < ncnt; i++)
                    {
                        string strrobotid = Data.Instance.jobpos_table.jobpos_robotinfo[i].strRobotID;
                        string strrobotname = Data.Instance.jobpos_table.jobpos_robotinfo[i].strRobotName;
                        string strrobotcnt = string.Format("{0}", Data.Instance.jobpos_table.jobpos_robotinfo[i].nCnt);

                        strMsg += string.Format("{0}({1},반복({2}) ", strrobotname, strrobotid, strrobotcnt);
                    }

                    strMsg += " 동작 하시겠습니까?";

                    if (DialogResult.OK == MessageBox.Show(strMsg, "확인", MessageBoxButtons.OKCancel))
                    {
                        onBtn_enable(false);
                        m_strLog_File = DateTime.Now.ToString("yyyyMMdd") + ".txt";
                        onJobMove(true);

                        bRobotMissioning = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("btnRunRobot_Click err= " + ex.Message.ToString());
                }
            }
        }

        private void btnStopRobot_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, string> workinfo = new Dictionary<string, string>();
                int ncnt = Data.Instance.Robot_work_info.Count(); //Data.Instance.robots_currgoing_status.Count();

                workinfo.Add("R_005", "");
                workinfo.Add("R_006", "");
                workinfo.Add("R_008", "");
                onJobPause(workinfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("btnStopRobot_Click err" + ex.Message.ToString());
            }
        }

        private void btnOnePause_Click(object sender, EventArgs e)
        {
            btnOnePause.Enabled = false;
            if (Data.Instance.isConnected)
            {
                try
                {
                    string strrobotid = cboOneRobot.SelectedItem.ToString();
                    Dictionary<string, string> workinfo = new Dictionary<string, string>();
                    workinfo.Add(strrobotid, "");
                    onJobPause(workinfo);

                    Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("btnOnePause_Click err" + ex.Message.ToString());
                }
            }
            btnOnePause.Enabled = true;
        }

        private void btnOneRestart_Click(object sender, EventArgs e)
        {
            btnOneRestart.Enabled = false;
            if (Data.Instance.isConnected)
            {
                try
                {
                    string strrobotid = cboOneRobot.SelectedItem.ToString();
                    Dictionary<string, string> workinfo = new Dictionary<string, string>();
                    workinfo.Add(strrobotid, "");
                    onJobRestart(workinfo);

                    Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("btnOneRestart_Click err" + ex.Message.ToString());
                }
            }
            btnOneRestart.Enabled = true;
        }

        private void WorkOrderForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtAddr_KeyPress(object sender, KeyPressEventArgs e)
        {

            //MessageBox.Show(e.KeyChar.ToString());
        }

        private void radioButton_all_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_all.Checked)
            {
                chk005Use.Checked = true;
                chk006Use.Checked = true;
                chk007Use.Checked = true;
                chk008Use.Checked = true;
            }
        }

        private void radioButton_123_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_123.Checked)
            {
                chk005Use.Checked = true;
                chk006Use.Checked = true;
                chk007Use.Checked = false;
                chk008Use.Checked = true;
            }
        }

        private void radioButton_13_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_13.Checked)
            {
                chk005Use.Checked = true;
                chk006Use.Checked = false;
                chk007Use.Checked = false;
                chk008Use.Checked = true;
            }
        }

        private void radioButton23_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton23.Checked)
            {
                chk005Use.Checked = false;
                chk006Use.Checked = true;
                chk007Use.Checked = false;
                chk008Use.Checked = true;
            }
        }

        private void radioButton_other_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_other.Checked)
            {
                chk005Use.Checked = false;
                chk006Use.Checked = false;
                chk007Use.Checked = false;
                chk008Use.Checked = false;
            }
        }

        public void onListmsg(string msg)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                listBox1.Items.Add(msg);
                int n =listBox1.Items.Count;
                listBox1.SelectedIndex = n - 1;

                if (n > 100) listBox1.Items.Clear();
            }));
        }
    }
}
