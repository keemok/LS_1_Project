using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraGauges.Win.Base;
using DevExpress.XtraGauges.Win.Gauges.Circular;

namespace Syscon_Solution.LSprogram
{
    public partial class robotOverview : UserControl
    {

        Panel[] robotPanel;
        Label[] robotstateLabel;
        Label[] robotnetworkLabel;
        Label[] robotworkloadLabel;
        LabelComponent[] robotBattery;
        LabelComponent[] robotAmpare;
        LabelComponent[] robotVolt;
        ArcScaleRangeBarComponent[] robotBatteryscale;

        string[] robotList = new string[] { "R_001", "R_002", "R_003", "R_004", "R_005", "R_006", "R_007" };
        //public static robotOverview _instance;
        //public static robotOverview instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //            _instance = new robotOverview();
        //        return _instance;
        //    }
        //}

        LSprogram.mainForm mainform;

        public robotOverview(mainForm frm)
        {
            
            mainform = frm;

        }

        
        public robotOverview()
        {
            InitializeComponent();
            
        }
        public void oninit()
        {
            InitializeComponent();
            robotPanel = new Panel[] { robot1Panel, robot2Panel, robot3Panel, robot4Panel, robot5Panel, robot6Panel, robot7Panel };
            robotstateLabel = new Label[] { robot1State, robot2State, robot3State, robot4State, robot5State, robot6State, robot7State };
            robotnetworkLabel = new Label[] { robot1Network, robot2Network, robot3Network, robot4Network, robot5Network, robot6Network, robot7Network };
            robotworkloadLabel = new Label[] { robot1Workload, robot2Workload, robot3Workload, robot4Workload, robot5Workload, robot6Workload, robot7Workload };
            robotBattery = new LabelComponent[] { robot1Battery, robot2Battery, robot3Battery, robot4Battery, robot5Battery, robot6Battery, robot7Battery };
            robotVolt = new LabelComponent[] { robot1Volt, robot2Volt, robot3Volt, robot4Volt, robot5Volt, robot6Volt, robot7Volt };
            robotAmpare = new LabelComponent[] { robot1Ampare, robot2Ampare, robot3Ampare, robot4Ampare, robot5Ampare, robot6Ampare, robot7Ampare };
            robotBatteryscale = new ArcScaleRangeBarComponent[]{ robot1Batteryscale, robot2Batteryscale, robot3Batteryscale, robot4Batteryscale, robot5Batteryscale, robot6Batteryscale, robot7Batteryscale};
            for (int i =0;i<robotPanel.Count();i++)
            {
                robotPanel[i].Enabled = false;
            }



        }
        Thread apsnmp;
        Thread checkRobot;
        public int th= 0;
        public void threadstart()
        {
            if (th == 1)
            {
                checkRobot = new Thread(checkRobotstate);
                checkRobot.IsBackground = true;
                checkRobot.Start();
            }
            else
            {
                return;
            }
        }
        private string workstate(int state)
        {
            
            switch(state)
            {
                case 0:
                    string result = "대기중";
                    return result;
                case 1:
                    result = "미션 수행중";
                    return result;
                case 2:
                    result = "이벤트 발생";
                    return result;
                case 3:
                    result = "중지 상태";
                    return result;
                case 4:
                    result = "미션 취소";
                    return result;
                case 5:
                    result = "ABORT";
                    return result;
                case 6:
                    result = "돌아가는중";
                    return result;
                case 7:
                    result = "충전중";
                    return result;
                default:
                    result = "-";
                    return result;
            }
        }
        
        private void checkClient()
        {
            APsnmp apsnmp;
            apsnmp = new APsnmp("192.168.102.103");
            apsnmp.getData();
            Console.WriteLine("103 값 : {0}", apsnmp.response);
            apsnmp = new APsnmp("192.168.102.106");
            apsnmp.getData();
            Console.WriteLine("106의 값 : {0}", apsnmp.response);
        }
        private void checkdata()
        {
            int i = 0;


            while (true)
            {
                foreach (KeyValuePair<string, Robot_RegInfo> info in Data.Instance.Robot_RegInfo_list)
                {
                    string strrobotid = info.Key;
                    float temp = Data.Instance.Robot_work_info[strrobotid].robot_status_info.controllerstate.msg.data[0];


                    

                    Thread.Sleep(1500);
                    Invoke(new MethodInvoker(delegate ()
                    {
                        textBox1.Text = temp.ToString();
                        textBox2.Text = Data.Instance.Robot_work_info[info.Key].robot_status_info.controllerstate.msg.data[0].ToString();

                    }));
                    if (temp == Data.Instance.Robot_work_info[info.Key].robot_status_info.controllerstate.msg.data[0])
                    {
                        i++;
                        if (i == 3)
                        {
                            MessageBox.Show("R_007 죽음");
                        }
                    }
                    else
                    {
                        i = 0;
                    }

                    Thread.Sleep(1000);
                }
            }
        }
        public void checkRobotstate()
        {
            try
            {
                
                while (true)
                {   
                    Thread.Sleep(1);
                    
                    List<string> key = new List<string>(Data.Instance.onRobot.Keys);
                    foreach (string robot in key)
                    {
                        for (int i = 0; i < robotPanel.Count(); i++)
                        {
                            if (robotPanel[i].Tag.ToString().Contains(robot))
                            {
                                Invoke(new MethodInvoker(delegate ()
                                {
                                    robotPanel[i].Enabled = true;

                                    robotBattery[i].Text = string.Format("{0:f1}%", Data.Instance.Robot_work_info[robot].robot_status_info.bmsinfo.msg.data[3]);
                                    robotAmpare[i].Text = string.Format("{0:f1}A", Data.Instance.Robot_work_info[robot].robot_status_info.bmsinfo.msg.data[1]);
                                    robotVolt[i].Text = string.Format("{0:f1}V", Data.Instance.Robot_work_info[robot].robot_status_info.bmsinfo.msg.data[0]);
                                    robotBatteryscale[i].Value = Data.Instance.Robot_work_info[robot].robot_status_info.bmsinfo.msg.data[3];

                                    for(int j = 0; j<Data.Instance.robot_liveinfo.robotinfo.msg.robolist.Count;j++)
                                    {
                                        if(Data.Instance.robot_liveinfo.robotinfo.msg.robolist[j].RID.Contains(robot))
                                        {
                                            robotstateLabel[i].Text = workstate(Data.Instance.robot_liveinfo.robotinfo.msg.robolist[j].workstate);
                                        }
                                    }
                                }));
                            }
                        }
                    }                  

                    Thread.Sleep(300);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error -> {0}", e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread a;
            a = new Thread(checkdata);
            a.Start();
        }
    }
}
