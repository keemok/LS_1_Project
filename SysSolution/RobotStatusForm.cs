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
using SysSolution.robotmonitoring;

namespace SysSolution
{
    public delegate void MethodInvoker();

    public partial class RobotStatusForm : Form
    {
        RobotMonitoringCtrl[] robotmonitoringctrl;

       Worker worker;

        string m_strWorklist_File = "";
        string m_strRobot_Status_File = "";
        string m_strCurrentSelectWork = "";
        string m_strCurrentSelectWorkFile = "";

        Panel[] monitorPanel=new Panel[6];
        string m_strLog_File = "";

        public RobotStatusForm()
        {
            InitializeComponent();
        }

        private void RobotStatusForm_Load(object sender, EventArgs e)
        {
#if _robotstatus
            Data.Instance.MAINFORM = this;
#endif
            m_strWorklist_File =  "..\\sysinfo\\worklist.txt";
            m_strRobot_Status_File = "..\\sysinfo\\Robot.txt"; //Application.StartupPath + "\\Robot.txt";

            worker = new Worker(this, 1);

            string strsubdir_log = "log_battery";
            DirectoryInfo dir = new DirectoryInfo(strsubdir_log);
            if (dir.Exists == false)
            {
                dir.Create();
            }

            m_strLog_File = DateTime.Now.ToString("yyyyMMdd") + ".txt";

            monitorPanel[0] = panel1;
            monitorPanel[1] = panel2;
            monitorPanel[2] = panel3;
            monitorPanel[3] = panel4;
            monitorPanel[4] = panel5;
            monitorPanel[5] = panel6;

            robotmonitoringctrl = new RobotMonitoringCtrl[6];
            robotmonitoringctrl[0] = new RobotMonitoringCtrl(this);
            robotmonitoringctrl[1] = new RobotMonitoringCtrl(this);
            robotmonitoringctrl[2] = new RobotMonitoringCtrl(this);
            robotmonitoringctrl[3] = new RobotMonitoringCtrl(this);
            robotmonitoringctrl[4] = new RobotMonitoringCtrl(this);
            robotmonitoringctrl[5] = new RobotMonitoringCtrl(this);


            onRobotFile_Open();

            onInitSet();


            Robotlive_Updatetimer.Interval = 500;
            Robotlive_Updatetimer.Enabled = true;



        }
        string[] strRobot_array;//= { "R_001", "R_002", "R_003", "R_004", "R_005", "R_006" };
        private void onInitSet()
        {
           
                for (int i = 0; i < strRobot_array.Count(); i++)
                {
                    robotmonitoringctrl[i].m_strRobotName = strRobot_array[i];
                    robotmonitoringctrl[i].m_strLog_File = m_strLog_File;
                    monitorPanel[i].Controls.Add(robotmonitoringctrl[i]);
                }
        }

        public void onRobotFile_Open()
        {
            try
            {
                if (!File.Exists(m_strRobot_Status_File))
                {
                    using (StreamWriter sw = new System.IO.StreamWriter(m_strRobot_Status_File, false, Encoding.Default))
                    {
                        sw.WriteLine("robot_id,ip,work id,work status,work cnt,curr work cnt");
                        sw.Close();
                    }
                    //  return;
                }


                int ncnt = 0; //파일에 첫줄은 항목명으로 빼고 읽기 위해 선언
                int nline = 0;
                using (StreamReader sr1 = new System.IO.StreamReader(m_strRobot_Status_File, Encoding.Default))
                {
                    
                     while (sr1.Peek() >= 0)
                    {
                        string strTemp = sr1.ReadLine();
                        if (ncnt != 0)
                        {
                            nline++;
                        }
                        ncnt++;
                    }
                 }                if (ncnt > 0)
                {
                    strRobot_array = new string[nline];

                    using (StreamReader sr1 = new System.IO.StreamReader(m_strRobot_Status_File, Encoding.Default))
                    {
                        int ncnt2 = 0; //파일에 첫줄은 항목명으로 빼고 읽기 위해 선언
                        Data.Instance.Robot_status_info.Clear();
                        int nline2 = 0;
                        while (sr1.Peek() >= 0)
                        {
                            string strTemp = sr1.ReadLine();
                            if (ncnt2 != 0)
                            {
                                string[] strRobotstatus = strTemp.Split(',');
                                strRobot_array[nline2] = strRobotstatus[0];
                                nline2++;
                            }
                            ncnt2++;
                        }
                    }                }            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobotFile_Open err :={0}", ex.Message.ToString());
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string strAddr = txtAddr.Text.ToString();
            ROSConnection(strAddr);
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
                    Console.WriteLine(ex.Message.ToString());
                }
            }
        }

        private async void ROSConnection(string strAddr)
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    onMonitoring();

                    onCompulsion_Close();
                    //   await Data.Instance.md.StopAsync();
                    //   Data.Instance.md = null;

                    //   Data.Instance.isConnected = false;

                    ROSDisconnect();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
            }
            else
            {
                try
                {
                    if (btnConnect.Text == "connect")
                    {

                        string uri = strAddr;
                        Data.Instance.socket = new Rosbridge.Client.Socket(new Uri(uri));

                        Data.Instance.md = new MessageDispatcher(Data.Instance.socket, new MessageSerializerV2_0());
                        await Data.Instance.md.StartAsync();

                        Data.Instance.isConnected = true;

                        if (Data.Instance.isConnected == true)
                        {
                            worker.onRealtimeRobotStatus_subscribe();

                            btnConnect.Text = "disconnect";

                            onMonitoring();
                        }
                    }
                    else
                    { 

                        ROSDisconnect();

                        
                    }
                }
                catch (Exception ex)
                {
                    //ROSDisconnect();
                    if (Data.Instance.isConnected == false)
                    {
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
                Data.Instance.socket = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

#endregion

        private void btnMonitoring_Click(object sender, EventArgs e)
        {
            onMonitoring();
        }

        private void onMonitoring()
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    if (btnMonitoring.Text == "모니터링")
                    {

                        for (int i = 0; i < strRobot_array.Count(); i++)
                        {
                            robotmonitoringctrl[i].onInitSet();
                        }
                        btnMonitoring.Text = "모니터링해제";
                    }
                    else
                    {
                        for (int i = 0; i < strRobot_array.Count(); i++)
                        {
                            worker.onDeleteSelectRobot_monitor_Subscribe(robotmonitoringctrl[i].m_strRobotName);
                            robotmonitoringctrl[i].onTimerOff();
                        }

                        for (int i2 = 0; i2 < strRobot_array.Count(); i2++)
                        {
                            monitorPanel[i2].BackColor = Color.Gray;
                            monitorPanel[i2].Invalidate();
                        }


                        btnMonitoring.Text = "모니터링";

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("onMonitoring() err=" + ex.Message.ToString());
                }
            }
        }
        public void onSelectRobotStatus_monitor(string strrobotid)
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    worker.onSelectRobotStatus_monitor_subscribe(strrobotid);
                    Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("onSelectRobotStatus_monitor err="+ex.Message.ToString());
                }
            }
        }

        private void UI_Updatetimer_Tick(object sender, EventArgs e)
        {
          //  onRobotStatusDP_Update();
        }


        

        private void RobotStatusForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Robotlive_Updatetimer.Enabled = false;

        }


        public void onListmsg(string msg)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                listBox1.Items.Add(msg);
                // listBox1.SelectedIndex = listBox1.Items.Count - 1;

                if (listBox1.Items.Count > 1000)
                    listBox1.Items.Clear();
            }));

        }

      
        public void updateDP(string strtopic, string msg, string strcnt)
        {
            onListmsg(string.Format("topic={0}..data={1}", strtopic, msg));
        }

        private void Robotlive_Updatetimer_Tick(object sender, EventArgs e)
        {

            if (Data.Instance.isConnected)
            {
                
                try
                {
                    if(Data.Instance.robot_liveinfo.robotinfo.msg.robolist.Count >0)
                    {
                        for (int i = 0; i < strRobot_array.Count(); i++)
                        {
                            for (int j = 0; j < Data.Instance.robot_liveinfo.robotinfo.msg.robolist.Count; j++)
                            {
                                if (Data.Instance.robot_liveinfo.robotinfo.msg.robolist[j].RID == strRobot_array[i])
                                {
                                    if (monitorPanel[i].BackColor != Color.Blue)
                                        monitorPanel[i].BackColor = Color.Blue;
                                    robotmonitoringctrl[i].m_bLive = true;
                                    break;
                                }
                                else
                                {
                                    if (monitorPanel[i].BackColor != Color.Gray)
                                        monitorPanel[i].BackColor = Color.Gray;
                                    robotmonitoringctrl[i].m_bLive = true;
                                }
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Robotlive_Updatetimer_Tick err=" + ex.Message.ToString());
                }
            }
           
        }
    }
}
