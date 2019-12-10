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

namespace SysSolution.Frm
{
    public partial class RobotStatusFrm : Form
    {
        public delegate void MethodInvoker();
       // Worker worker;

        string m_strWorklist_File = "";
        string m_strRobot_Status_File = "";
        string m_strCurrentSelectWork = "";
        string m_strCurrentSelectWorkFile = "";

        Panel[] monitorPanel = new Panel[6];
        string m_strLog_File = "";

        RobotMonitoringCtrl[] robotmonitoringctrl;

        public XISMonitoringCtrl xismonitoringctrl;

        public RobotCamMonitoringCtrl robotcammintorctrl;

        public RobotStatusFrm()
        {
            InitializeComponent();
        }
       public DashboardForm mainForm;

        public RobotStatusFrm(DashboardForm frm)
        {
            mainForm = frm;
            InitializeComponent();
        }

        private void RobotStatusFrm_Load(object sender, EventArgs e)
        {
            onFormload();
        }

        public void onFormload()
        {
            m_strWorklist_File = "..\\sysinfo\\worklist.txt";
            //  m_strRobot_Status_File = "..\\sysinfo\\Robot.txt"; //Application.StartupPath + "\\Robot.txt";

            // worker = new Worker(this, 1);

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

            robotcammintorctrl = new RobotCamMonitoringCtrl(this);

            //  robotmonitoringctrl[4] = new RobotMonitoringCtrl(this);
            xismonitoringctrl = new XISMonitoringCtrl(this);

            //onRobotFile_Open(); //전시회 동안 사용안함
            onInitSet();

            Robotlive_Updatetimer.Interval = 500;
            Robotlive_Updatetimer.Enabled = true;


            //test
            numericUpDown1.Value = 1;



            onMonitoring();

        }
        string[] strRobot_array= { "R_005","R_006", "R_008", "R_007" };
        private void onInitSet()
        {

            for (int i = 0; i < strRobot_array.Count(); i++)
            {
                robotmonitoringctrl[i].m_strRobotName = strRobot_array[i];
                robotmonitoringctrl[i].m_strLog_File = m_strLog_File;
              /*  if(i%2==0)
                    robotmonitoringctrl[i].onTempDataInput(true, true, true);
                else robotmonitoringctrl[i].onTempDataInput(true, true, false);
                */
                monitorPanel[i].Controls.Add(robotmonitoringctrl[i]);
            }

            robotcammintorctrl.m_strRobotName = "R_004";
            monitorPanel[4].Controls.Add(robotcammintorctrl);

            monitorPanel[5].Controls.Add(xismonitoringctrl);
        }
        #region 전시회 동안 사용안함
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
        #endregion

        Frm.ErrorMsgDlg errormsgdlg = new Frm.ErrorMsgDlg();

        private void btnMonitoring_Click(object sender, EventArgs e)
        {
           onMonitoring();
        }

        public  void onMonitoring()
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

                        robotcammintorctrl.onInitSet();

                        xismonitoringctrl.onInitSet();

                        btnMonitoring.Text = "모니터링해제";
                    }
                    else
                    {
                        for (int i = 0; i < strRobot_array.Count(); i++)
                        {
                            robotmonitoringctrl[i].onTimerOff();
                        }

                        for (int i2 = 0; i2 < strRobot_array.Count(); i2++)
                        {
                            monitorPanel[i2].BackColor = Color.Gray;
                            monitorPanel[i2].Invalidate();
                        }

                        monitorPanel[5].BackColor = Color.Gray;
                        monitorPanel[5].Invalidate();

                        btnMonitoring.Text = "모니터링";

                    }



                }
                catch (Exception ex)
                {
                    Console.WriteLine("onMonitoring() err=" + ex.Message.ToString());
                }
            }
        }


        private void Robotlive_Updatetimer_Tick(object sender, EventArgs e)
        {
            if (Data.Instance.isConnected)
            {

                try
                {
                    if (Data.Instance.robot_liveinfo.robotinfo == null)
                    {
                        return;
                    }

                    if (monitorPanel[5].BackColor != Color.Aquamarine) //xis장치는 모두 켜놓는다.. 
                        monitorPanel[5].BackColor = Color.Aquamarine;

                    if (Data.Instance.robot_liveinfo.robotinfo.msg.robolist.Count > 0)
                    {
                        for (int i = 0; i < strRobot_array.Count(); i++)
                        {
                            for (int j = 0; j < Data.Instance.robot_liveinfo.robotinfo.msg.robolist.Count; j++)
                            {
                                if (Data.Instance.robot_liveinfo.robotinfo.msg.robolist[j].RID == strRobot_array[i])
                                {
                                    if (monitorPanel[i].BackColor != Color.GreenYellow)
                                        monitorPanel[i].BackColor = Color.GreenYellow;
                                    robotmonitoringctrl[i].m_bLive = true;
                                    break;
                                }
                                else
                                {
                                    if (monitorPanel[i].BackColor != Color.Gray)
                                        monitorPanel[i].BackColor = Color.Gray;
                                    robotmonitoringctrl[i].m_bLive = false;
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

        

        private void RobotStatusFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Robotlive_Updatetimer.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            xismonitoringctrl.m_bPalleton = true;

            //xismonitoringctrl.onPalletLamp_DP(true);
            //xismonitoringctrl.onPallet_DP(true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            xismonitoringctrl.m_bPalleton = false;
            //xismonitoringctrl.onPalletLamp_DP(false);
            //xismonitoringctrl.onPallet_DP(false);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int nnum = (int)numericUpDown1.Value;

            xismonitoringctrl.m_bGoodson = true;
            xismonitoringctrl.m_nGoodsKinds = nnum;

           // xismonitoringctrl.onGoodsCode_DP(nnum);

        }
    }
}
