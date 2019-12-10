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

namespace SysSolution
{
    public partial class RobotStatusForm_One : Form
    {
        public delegate void MethodInvoker();

        robotmonitoring.RoboMonitoringCtrl_1 robotmonitoringctrl;

        public Worker worker;

        string m_strWorklist_File = "";
        string m_strRobot_Status_File = "";
        string m_strCurrentSelectWork = "";
        string m_strCurrentSelectWorkFile = "";

        Panel monitorPanel = new Panel();

        string m_strLog_File = "";

        Frm.ingdlg ingdlg = new Frm.ingdlg();

        public RobotStatusForm_One()
        {
            InitializeComponent();
        }

        private void RobotStatusForm_One_Load(object sender, EventArgs e)
        {
#if _statusone
            Data.Instance.MAINFORM = this;
#endif

            m_strWorklist_File = "..\\sysinfo\\worklist.txt";
            m_strRobot_Status_File = "..\\sysinfo\\Robot.txt"; //Application.StartupPath + "\\Robot.txt";

            worker = new Worker(this, 1);


            monitorPanel = panel1;


            robotmonitoringctrl = new robotmonitoring.RoboMonitoringCtrl_1(this);
            robotmonitoringctrl.m_strRobotName = "R_006";
            monitorPanel.Controls.Add(robotmonitoringctrl);

            // onRobotFile_Open();

            Robotlive_Updatetimer.Interval = 500;
            Robotlive_Updatetimer.Enabled = true;

            //onMonitoring();

            onRobots_WorkInfo_InitSet();

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
                        

                            onSuscribe_RobotsStatus();
                            ingdlg.Hide();

                           // onMonitoring();
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
          
            }
            catch (Exception ex)
            {
                Console.WriteLine("ROSDisconnect err = " + ex.Message.ToString());
            }
        }

        #endregion


        public void onMonitoring()
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    if (btnMonitoring.Text == "모니터링")
                    {

                        robotmonitoringctrl.onInitSet();
                        btnMonitoring.Text = "모니터링해제";
                    }
                    else
                    {
                       robotmonitoringctrl.onTimerOff();
                       btnMonitoring.Text = "모니터링";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("onMonitoring() err=" + ex.Message.ToString());
                }
            }
        }

        /// <summary>
        /// 등록된 로봇의 워크 정보를 초기화 한다.
        /// </summary>
        public void onRobots_WorkInfo_InitSet()
        {
            try
            {
                string strrobotid = "R_006";

                Data.Instance.Robot_work_info.Add(strrobotid, worker.onNewRobotWorkInfo_initial(strrobotid, "", 1, 0, "", ""));
            }
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
                    string[] strrobot = { "R_006"};
                    for (int i = 0; i < 1; i++)
                    {
                        string strrobotid = strrobot[i];
                        worker.onSelectRobotStatus_subscribe(strrobotid);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSuscribe_RobotsStatus err :={0}", ex.Message.ToString());
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string strAddr = txtAddr.Text.ToString();

            btnConnect.Enabled = false;

            ROSConnection(strAddr);
        }

        private void btnMonitoring_Click(object sender, EventArgs e)
        {
            onMonitoring();
        }
    }
}
