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

namespace SysSolution.Frm
{


    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
        }
        Dashboard_WorkAmount_Ctrl []dashboard_workamount_ctrl;
        Dashboard_RobotStatusCtrl dashboard_robotstatus_ctrl;
        Dashboard_WorkReport_Ctrl dashboard_workreport_ctrl;

        WorkRegFrm workregfrm;
        WorkResultFrm workresultfrm;
        WorkOrderFrm workorderfrm;
        ManualFrm manualfrm;
        MapMonitor mapmonitorfrm;
        RobotStatusFrm robotstatusfrm;


        public Worker worker;

        #region DATA class로 옮길 전역 변수

        /// <summary>
        /// 등록된 로봇별 live상태를 기록 및 보관하는 클래스 
        /// </summary>
        public Dictionary<string,RobotLive_Status> G_robots_live_status = new Dictionary<string,RobotLive_Status>();

        public List<string> G_robotList = new List<string>();

        /// <summary>
        /// 등록된 로봇별 live상태를 기록하는 개별 로봇 파일들의 리스트 
        /// </summary>
        public Dictionary<string, string> G_robotsLiveStatus_File_List = new Dictionary<string, string>();

        #endregion


        string m_strRobot_List_File = "";
        string m_strRobot_Status_File = "";
        string m_strRobot_LiveChk_File = "";
        ingdlg ingdlg = new ingdlg();

        int m_nRobotWorkCnt = 0;
        int m_nRobotLiveCnt = 0;

        private void DashboardForm_Load(object sender, EventArgs e)
        {
#if _sol
            Data.Instance.MAINFORM = this;

#endif
            worker = new Worker(this, 1);

            dashboard_workamount_ctrl = new Dashboard_WorkAmount_Ctrl[4];
            dashboard_workamount_ctrl[0] = new Dashboard_WorkAmount_Ctrl(this);
            dashboard_workamount_ctrl[1] = new Dashboard_WorkAmount_Ctrl(this);
            dashboard_workamount_ctrl[2] = new Dashboard_WorkAmount_Ctrl(this);
            dashboard_workamount_ctrl[3] = new Dashboard_WorkAmount_Ctrl(this);

            dashboard_robotstatus_ctrl = new Dashboard_RobotStatusCtrl(this);

            dashboard_workreport_ctrl = new Dashboard_WorkReport_Ctrl(this);

            worker.robotlivechk_Evt += new Worker.RobotLiveCheck(this.RobotLiveCheck);
            worker.workrequest_Evt += new Worker.Work_request_FromRobot(this.Work_request_FromRobot);
            worker.exceptstatus_Evt += new Worker.ExceptStatus_FromRobot(this.ExceptStatus_FromRobot);

            workregfrm=new WorkRegFrm(this);
            workresultfrm = new WorkResultFrm(this);
            workorderfrm = new WorkOrderFrm(this);
            manualfrm = new ManualFrm(this);
            mapmonitorfrm = new MapMonitor(this);


            robotstatusfrm = new RobotStatusFrm(this);
           


            cboDisplayScale.SelectedIndex = 0;


            m_strRobot_List_File = "..\\Ros_info\\RobotList.txt";
            m_strRobot_Status_File = "..\\Ros_info\\RobotJobStatus.txt";


            onRobotList_Open();
            onRobots_LiveStatus_Check_Open();

            onRobotWork_StatusFile_Open();

            onRobots_WorkInfo_InitSet();

            RobotWorkstatus_Updatetimer.Interval = 500;
            RobotWorkstatus_Updatetimer.Enabled = true;


            timer_goodsload.Interval = 250;
            timer_goodsload.Enabled = true;

            onSolutionInitializing();

            Data.Instance.XIS_Status_Info.Clear();
        }

        private void cboDisplayScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(panel1.Controls.Count==1) panel1.Controls.RemoveAt(0);
            if (panel2.Controls.Count == 1) panel2.Controls.RemoveAt(0);
            if (panel3.Controls.Count == 1) panel3.Controls.RemoveAt(0);
            if (panel4.Controls.Count == 1) panel4.Controls.RemoveAt(0);
            if (panel5.Controls.Count == 1) panel5.Controls.RemoveAt(0);


            if (cboDisplayScale.SelectedIndex==0)
            {

                panel1.Controls.Add(dashboard_workamount_ctrl[0]);
                panel2.Controls.Add(dashboard_robotstatus_ctrl);
                panel5.Controls.Add(dashboard_workreport_ctrl);
                panel3.Hide();
                panel4.Hide();
                panel5.Show();

            }
            else if (cboDisplayScale.SelectedIndex == 1)
            {
                panel1.Controls.Add(dashboard_workamount_ctrl[0]);
                panel2.Controls.Add(dashboard_workamount_ctrl[1]);
                panel3.Controls.Add(dashboard_workamount_ctrl[2]);
                panel4.Controls.Add(dashboard_workamount_ctrl[3]);

                panel3.Show();
                panel4.Show();
                panel5.Hide();
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
                    worker.onDeleteAllSubscribe_Compulsion();
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


                    //   await Data.Instance.md.StopAsync();
                    //   Data.Instance.md = null;

                    //   Data.Instance.isConnected = false;

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

                        Data.Instance.isConnected = true;

                        if (Data.Instance.isConnected == true)
                        {
                            btnConnect.Enabled = true;
                            btnConnect.Text = "disconnect";
                            panel1.Enabled = true;
                            panel2.Enabled = true;
                            panel3.Enabled = true;
                            panel4.Enabled = true;
                            panel5.Enabled = true;

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
                panel1.Enabled = false;
                panel2.Enabled = false;
                panel3.Enabled = false;
                panel4.Enabled = false;
                panel5.Enabled = false;

            }
            catch (Exception ex)
            {
                Console.WriteLine("ROSDisconnect err = " + ex.Message.ToString());
            }
        }

        #endregion

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string strAddr = txtAddr.Text.ToString();

            btnConnect.Enabled = false;
            
            ROSConnection(strAddr);
        }

        private void timer_goodsload_Tick(object sender, EventArgs e)
        {
            if(Data.Instance.isConnected)
            {
                onRobotGoodsLoadCheck();
            }
        }

        public int nCurrLoadCnt = 0;
        bool bGoodsload = false;
    
        private void onRobotGoodsLoadCheck()
        {
            try
            {
                for (int i = 0; i < G_robotList.Count; i++)
                {
                    string strrobotid = G_robotList[i];
                    if (Data.Instance.Robot_work_info.ContainsKey(strrobotid))
                    {
                        int nlen = 0;
                        nlen = Data.Instance.Robot_work_info[strrobotid].nTotalActionidx;
                        int nactidx = Data.Instance.Robot_work_info[strrobotid].nActionidx;

                        if (Data.Instance.Robot_work_info[strrobotid].robot_workdata.Count < 1) continue;

                        string stractiontype = Data.Instance.Robot_work_info[strrobotid].robot_workdata[nactidx].strActionType;

                        if (stractiontype == "리프트&컨베어" || stractiontype == "stablepallet")
                        {
                            int nliftstatus = Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate.msg.lift_status;

                            if (nliftstatus == 2 && !bGoodsload)
                            {
                                bGoodsload = true;
                                nCurrLoadCnt += 1;
                                Invoke(new MethodInvoker(delegate ()
                                {
                                    textBox1.Text = string.Format("{0}", nCurrLoadCnt);
                                }));
                            }
                            else if (nliftstatus == -2)
                            {
                                bGoodsload = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobotGoodsLoadCheck err :={0}", ex.Message.ToString());
            }
        }


        #region Event 처리

        

        public void RobotLiveCheck()
        {
            try
            {
                #region test 
                /*
                if(G_robots_live_status.Count >0)
                {
                    for(int i=0; i< G_robots_live_status.Count; i++)
                    {
                        KeyValuePair<string, RobotLive_Status> info = G_robots_live_status.ElementAt(i);
                        string strlivestatus_robotid = info.Key;

                        for (int j = 0; j < Data.Instance.robot_liveinfo.robotinfo.msg.robolist.Count; j++)
                        {
                            string robotid = Data.Instance.robot_liveinfo.robotinfo.msg.robolist[j].RID;

                            if(info.Key == robotid)
                            {
                                string strmissionid = G_robots_live_status[strlivestatus_robotid].strMissionID;
                                string stractionidx = G_robots_live_status[strlivestatus_robotid].strActionidx;

                                if (G_robots_live_status[strlivestatus_robotid].strRobotLiveStatus == "dead" && G_robots_live_status[strlivestatus_robotid].strJobing == "jobing")
                                {
                                    //live로 변경
                                    G_robots_live_status[strlivestatus_robotid].strRobotLiveStatus = "live";
                                    //미션과 미션idx 전달
                                    if (strmissionid == "")
                                    {
                                        Console.Out.WriteLine("전원부팅시 dead,jobing인데.. 이전 미션정보가 없음..");
                                    }
                                    else
                                    {
                                        //미션과 미션idx 전달
                                        onInitMission(strlivestatus_robotid, strmissionid, stractionidx);
                                    }
                                }
                                else if (G_robots_live_status[strlivestatus_robotid].strRobotLiveStatus == "dead" && G_robots_live_status[strlivestatus_robotid].strJobing == "wait")
                                {
                                    G_robots_live_status[strlivestatus_robotid].strRobotLiveStatus = "live";
                                    //파일 갱신 RID_livestatus.txt
                                }
                                else if (G_robots_live_status[strlivestatus_robotid].strRobotLiveStatus == "live")
                                {
                                    // RobotWorkstatus_Updatetimer.Enabled = false;
                                    string strtemp = Data.Instance.Robot_status_info[strlivestatus_robotid];
                                    string[] strRobotstatus = strtemp.Split(',');

                                    G_robots_live_status[strlivestatus_robotid].strMissionID = strRobotstatus[2];
                                    G_robots_live_status[strlivestatus_robotid].strActionidx = strRobotstatus[6];

                                    //   RobotWorkstatus_Updatetimer.Enabled = true;
                                }

                                break;
                            }
                            else
                            {
                                G_robots_live_status[strlivestatus_robotid].strRobotLiveStatus = "dead";
                            }
                        }
                    }
                }
                */
                #endregion

                if (G_robots_live_status.Count > 0)
                {
                    for (int i = 0; i < G_robots_live_status.Count; i++)
                    {
                        KeyValuePair<string, RobotLive_Status> info = G_robots_live_status.ElementAt(i);
                        string strlivestatus_robotid = info.Key;

                        for (int j = 0; j < Data.Instance.robot_liveinfo.robotinfo.msg.robolist.Count; j++)
                        {
                            string robotid = Data.Instance.robot_liveinfo.robotinfo.msg.robolist[j].RID;

                            if (info.Key == robotid)
                            {
                                string strmissionid = G_robots_live_status[strlivestatus_robotid].strMissionID;
                                string stractionidx = G_robots_live_status[strlivestatus_robotid].strActionidx;

                                if (G_robots_live_status[strlivestatus_robotid].strRobotLiveStatus == "dead" && G_robots_live_status[strlivestatus_robotid].strJobing == "jobing")
                                {
                                    //live로 변경
                                    G_robots_live_status[strlivestatus_robotid].strRobotLiveStatus = "live";
                                   
                                }
                                else if (G_robots_live_status[strlivestatus_robotid].strRobotLiveStatus == "dead" && G_robots_live_status[strlivestatus_robotid].strJobing == "wait")
                                {
                                    G_robots_live_status[strlivestatus_robotid].strRobotLiveStatus = "live";
                                    //파일 갱신 RID_livestatus.txt
                                }
                                else if (G_robots_live_status[strlivestatus_robotid].strRobotLiveStatus == "live")
                                {
                                    string strtemp = Data.Instance.Robot_status_info[strlivestatus_robotid];
                                    string[] strRobotstatus = strtemp.Split(',');

                                    G_robots_live_status[strlivestatus_robotid].strMissionID = strRobotstatus[2];
                                    G_robots_live_status[strlivestatus_robotid].strActionidx = strRobotstatus[6];
                                }

                                break;
                            }
                            else
                            {
                                G_robots_live_status[strlivestatus_robotid].strRobotLiveStatus = "dead";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("RobotLiveCheck err :={0}", ex.Message.ToString());
            }
        }
        
        public void Work_request_FromRobot(string strrobotid)
        {
            try
            {
                if (Data.Instance.Robot_work_info.ContainsKey(strrobotid))
                {
                    string strmissionid = "";
                    int actionidx = 0;
                    strmissionid = Data.Instance.Robot_work_info[strrobotid].robot_status_info.work_request.msg.wid;
                    actionidx = Data.Instance.Robot_work_info[strrobotid].robot_status_info.work_request.msg.start_idx;

                    //기존에 robot_status_info에서 현재 횟수와 전체횟수를 읽어서 남은 수만큼 보낸다.. 
                    //하지만.. 현재 워크지시 작업이 분리되어있어서 한번 보내고..workresult가 오면.. 다른 워크지시프로그램에서 남은거를 처리한다.
                    //차후 통합시 고려할것.. 
                    onRobotRequest_Mission(strrobotid, strmissionid, string.Format("{0}", actionidx));
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Work_request_FromRobot err :={0}", ex.Message.ToString());
            }
        }

        public void ExceptStatus_FromRobot(string strrobotid)
        {
            try
            {
                if (Data.Instance.Robot_work_info.ContainsKey(strrobotid))
                {
                    int nerrdata = Data.Instance.Robot_work_info[strrobotid].robot_status_info.except_status.msg.data;

                    int nemergency_btnErr = (int)(nerrdata & 0x01);
                    int nmissioningstopErr = (int)(nerrdata & 0x02);



                    //비상 상태정보 표시 

                    //비상 정보 => 로봇작업 log에 저장.
                    /*if (Data.Instance.robots_currgoing_status.ContainsKey(strrobotid))
                      {
                          string strRobot = Data.Instance.robots_currgoing_status[strrobotid].strRobotID;
                          string strMissionID = Data.Instance.robots_currgoing_status[strrobotid].strMisssionID;
                          string strRobotName = Data.Instance.robots_currgoing_status[strrobotid].strRobotName;
                          string strMissionName = Data.Instance.robots_currgoing_status[strrobotid].strMisssionName;

                          onRobotWork_save(strRobot, strRobotName, strMissionID, strMissionName, "OK", 1);
                      }
                      */

                    if (nemergency_btnErr == 1)
                    {
                        onRobotWork_save(strrobotid, "", "", "", "Emergency", 1);
                    }

                    if (nmissioningstopErr == 1)
                    {
                        onRobotWork_save(strrobotid, "", "", "", "MissioningErr", 1);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("ExceptStatus_FromRobot err :={0}", ex.Message.ToString());
            }
            

        }

        #endregion

        #region DB 읽기, 저장, 변경

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

                    G_robotsLiveStatus_File_List.Clear();
                    G_robotList.Clear();

                    while (sr1.Peek() >= 0)
                    {
                        string strTemp = sr1.ReadLine();
                        if (ncnt != 0)
                        {
                            string[] strRobotlist = strTemp.Split(',');
                            //    dataGridView1.Rows.Add(strRobotstatus);
                            G_robotList.Add(strRobotlist[0]);
                            string strfile = "";
                            strfile = "..\\Ros_info\\" + string.Format("{0}_livestatus.txt", strRobotlist[0]);
                            G_robotsLiveStatus_File_List.Add(strRobotlist[0], strfile);

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
        /// 등록된 로봇별로 live상태, job 상태 를 읽어온다.
        /// </summary>
        public void onRobots_LiveStatus_Check_Open()
        {
            try
            {
                if (G_robotList.Count > 0)
                {
                    G_robots_live_status.Clear();
                    for (int i = 0; i < G_robotsLiveStatus_File_List.Count; i++)
                    {
                        string strfile = "";

                        if (G_robotsLiveStatus_File_List.ContainsKey(G_robotList[i]))
                        {
                            strfile = G_robotsLiveStatus_File_List[G_robotList[i]];

                            if (!File.Exists(strfile))
                            {
                                using (StreamWriter sw = new System.IO.StreamWriter(strfile, false, Encoding.Default))
                                {
                                    sw.WriteLine("robot_id,livestatus,jobstatus,actionidx,missionid,missionname,submissionid,submissioname");
                                    sw.WriteLine(string.Format("{0},dead,wait,0,0,0,0,0",G_robotList[i]));
                                    sw.Close();
                                }
                                //  return;
                            }

                            using (StreamReader sr1 = new System.IO.StreamReader(strfile, Encoding.Default))
                            {
                                int ncnt = 0; //파일에 첫줄은 항목명으로 빼고 읽기 위해 선언

                                RobotLive_Status robotlive_status = new RobotLive_Status();
                                

                                while (sr1.Peek() >= 0)
                                {
                                    string strTemp = sr1.ReadLine();
                                    if (ncnt != 0)
                                    {
                                        string[] strRobotlivestatus = strTemp.Split(',');

                                        robotlive_status.strRobotID = strRobotlivestatus[0];
                                        robotlive_status.strRobotLiveStatus = strRobotlivestatus[1];
                                        robotlive_status.strJobing = strRobotlivestatus[2];
                                        robotlive_status.strActionidx = strRobotlivestatus[3];
                                        robotlive_status.strMissionID = strRobotlivestatus[4];
                                        robotlive_status.strMissionNum = strRobotlivestatus[5];

                                        G_robots_live_status.Add(strRobotlivestatus[0],robotlive_status);
                                    }
                                    ncnt++;
                                }
                            }

                        }
                    }                                    }            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobots_LiveStatus_Check_Open err :={0}", ex.Message.ToString());
            }
        }

        public void onRobotWork_StatusFile_Open()
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

                            Data.Instance.Robot_status_info.Add(strRobotstatus[0], strTemp);
                        }
                        ncnt++;
                    }
                }            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobotWork_StatusFile_Open err :={0}", ex.Message.ToString());
            }
        }

        private void RobotWorkstatus_Updatetimer_Tick(object sender, EventArgs e)
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

                if (G_robotList.Count > 0)
                {
                    for (int i = 0; i < G_robotsLiveStatus_File_List.Count; i++)
                    {
                        string strfile = "";

                        if (G_robotsLiveStatus_File_List.ContainsKey(G_robotList[i]))
                        {
                            strfile = G_robotsLiveStatus_File_List[G_robotList[i]];

                            if(G_robots_live_status.ContainsKey(G_robotList[i]))
                            {
                                RobotLive_Status value =  G_robots_live_status[G_robotList[i]];

                                using (StreamWriter sw = new System.IO.StreamWriter(strfile, false, Encoding.Default))
                                {
                                    sw.WriteLine("robot_id,livestatus,jobstatus,actionidx,missionid,missionname,submissionid,submissioname");
                                    string strdata = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", value.strRobotID, value.strRobotLiveStatus, value.strJobing, value.strActionidx, value.strMissionID, value.strMissionNum, value.strsubMissionID, value.strsubMissionNum);
                                    sw.WriteLine(strdata);

                                    sw.Close();
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("RobotWorkstatus_Updatetimer_Tick err :={0}", ex.Message.ToString());
            }
        }

        /// <summary> 
        /// 로봇 작업 log 기록
        /// </summary>
        public void onRobotWork_save(string strRobot, string strRobotName, string strMissionID, string strMissionName, string strResult, int ncnt)//"OK",1)
        {
            string m_strLog_File = DateTime.Now.ToString("yyyyMMdd") + ".txt";

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


        /// <summary>
        /// 등록된 로봇의 워크 정보를 초기화 한다.
        /// </summary>
        public void onRobots_WorkInfo_InitSet()
        {
            try
            {
               if(G_robotList.Count >0)
                {
                    Data.Instance.Robot_work_info.Clear();

                    for (int i=0; i<G_robotList.Count; i++)
                    {
                        string strrobotid = G_robotList[i];
                   
                        Data.Instance.Robot_work_info.Add(strrobotid,worker.onNewRobotWorkInfo_initial(strrobotid, "", 1, 0, "", ""));
                    }
                }            }
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
                    worker.onRealtimeRobotStatus_subscribe();

                    Dictionary<string, string> robotinfo = Data.Instance.Robot_status_info;
                    //foreach (KeyValuePair<string, string> info in robotinfo)
                    string[] strrobot = { "R_005", "R_006", "R_007", "R_008" };
                    for (int i = 0; i < 4; i++)
                    {
                        //string key = info.Key;
                        //string value = info.Value;
                        //string[] strvalue=value.Split(',');
                        //string strrobotid = strvalue[0];
                        string strrobotid = strrobot[i];
                        worker.onSelectRobotStatus_subscribe(strrobotid);
                    }

                    worker.onSelectXIS_subscribe();

                  // robotstatusfrm.onMonitoring();
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSuscribe_RobotsStatus err :={0}", ex.Message.ToString());
            }
        }

        /// <summary>
        /// 솔루션이 시작시 초기화 작업을 진행하는부분..(로봇들의 상태 및 작업 내용들을 동기화하는곳)
        /// </summary>
        public void onSolutionInitializing()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onInitializing err :={0}", ex.Message.ToString());
            }
        }

        public async void onRobotRequest_Mission(string strRobot, string strMissionID, string stractionidx)
        {
            try
            {
                string strworkfile = "";
                string strworkrobot = "";

                string strworkid = "";

                List<string> strSelectRobot = new List<string>();
                List<string> strworkdata = new List<string>();
                string[] strSelectRobot_Worker; //워크작업으로 전달하기 위해 형식 맞춤.. 
                string[] strSelectworkdata_Worker;

                int workcnt = 1;


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
                    var task = Task.Run(() => worker.onRobotRequest_WorkOrder_publish(strworkname, strworkid, strSelectRobot_Worker, strSelectworkdata_Worker, nworkcnt, int.Parse(stractionidx)));
                    await task;
                    Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine("onRobotRequest_Mission_1 err :={0}", ex.Message.ToString());
                }

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobotRequest_Mission_2 err :={0}", ex.Message.ToString());
            }
        }


        private void btnJobOrder_Click(object sender, EventArgs e)
        {
            try
            {
               // if (Data.Instance.isConnected)
                {
                    if (!workorderfrm.Visible)
                    {
                        workorderfrm = null;
                        workorderfrm = new WorkOrderFrm(this);
                        workorderfrm.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("btnJobOrder_Click err :={0}", ex.Message.ToString());
            }
        }

        private void btnJobReg_Click(object sender, EventArgs e)
        {
            try
            {
                if (!workregfrm.Visible)
                {
                    workregfrm = null;
                    workregfrm = new WorkRegFrm(this);
                    workregfrm.Show();
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("btnJobReg_Click err :={0}", ex.Message.ToString());
            }
            
        }

        private void btnMapMointor_Click(object sender, EventArgs e)
        {
            try
            {
               // if (Data.Instance.isConnected)
                {
                    if (!mapmonitorfrm.Visible)
                    {
                        mapmonitorfrm = null;
                        mapmonitorfrm = new MapMonitor(this);
                        mapmonitorfrm.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("btnMapMointor_Click err :={0}", ex.Message.ToString());
            }
        }

        private void btnRobotStatusMonitor_Click(object sender, EventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    if (!robotstatusfrm.Visible)
                    {
                        robotstatusfrm = null;
                        robotstatusfrm = new RobotStatusFrm(this);
                        //robotstatusfrm.onFormload();
                        //robotstatusfrm.onMonitoring();
                        robotstatusfrm.Show();
                    }
                }
                else
                {
                    MessageBox.Show("ROS server에 연결후 실행하세요");
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("btnRobotStatusMonitor_Click err :={0}", ex.Message.ToString());
            }
        }

        private void btnJobResult_Click(object sender, EventArgs e)
        {
            try
            {
                //if (Data.Instance.isConnected)
               // {
                    if (!workresultfrm.Visible)
                    {
                        workresultfrm = null;
                        workresultfrm = new WorkResultFrm(this);
                        workresultfrm.Show();
                    }
              //  }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("btnRobotStatusMonitor_Click err :={0}", ex.Message.ToString());
            }
            
        }

        private void btnManual_Click(object sender, EventArgs e)
        {
            try
            {
             //   if (Data.Instance.isConnected)
                {
                    if (!manualfrm.Visible)
                    {
                        manualfrm = null;
                        manualfrm = new ManualFrm(this);
                        manualfrm.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("btnManual_Click err :={0}", ex.Message.ToString());
            }
        }

        private void DashboardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
             RobotWorkstatus_Updatetimer.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           robotstatusfrm.xismonitoringctrl.m_bPalleton = true;

            //xismonitoringctrl.onPalletLamp_DP(true);
            //xismonitoringctrl.onPallet_DP(true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            robotstatusfrm.xismonitoringctrl.m_bPalleton = false;
            //xismonitoringctrl.onPalletLamp_DP(false);
            //xismonitoringctrl.onPallet_DP(false);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int nnum = (int)numericUpDown1.Value;

            robotstatusfrm.xismonitoringctrl.m_bGoodson = true;
            robotstatusfrm.xismonitoringctrl.m_nGoodsKinds = nnum;

            // xismonitoringctrl.onGoodsCode_DP(nnum);

        }

     
    }

    public class RobotLive_Status
    {
        public string strRobotID;
        public string strRobotLiveStatus;
        public string strJobing;
        public string strActionidx;
        public string strMissionID;
        public string strMissionNum;
        public string strsubMissionID;
        public string strsubMissionNum;
    }
}
