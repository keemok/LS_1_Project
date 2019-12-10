using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Syscon_Solution.Exhibition
{
    public partial class monitoringForm : Form
    {

        Syscon_Solution.Exhibition.exhibitionMain mainform;

        Thread GetData;
        
        public monitoringForm()
        {
            InitializeComponent();
        }
        public monitoringForm(exhibitionMain frm)
        {
            mainform = frm;
            InitializeComponent();
        }
        string[] enableRobot = { "R_000", "R_001", "R_002", "R_003", "R_004", "R_005"};
        
        public void onInit()
        {
            checkThestatus();
            Variable();
            Thread th = new Thread(onSelectRobotbms_monitor);
            th.IsBackground = true;
            th.Start();
        }


        Thread[] th = new Thread[6];
        Label[] robotState;
        Label[] robotVolt;
        Label[] robotMission;
        Label[] robotTime;
        Label[] robotAmpare;
        Label[] robotTemp;
        Label[] robotRemaincap;
        Label[] robotRemainenergy;
        DevExpress.XtraEditors.ProgressBarControl[] robotBattery;
        



        private void Variable()
        {
            robotState = new Label[] { robot0State, robot1State, robot2State, robot3State, robot4State, robot5State };
            robotVolt = new Label[] { robot0Volt, robot1Volt, robot2Volt, robot3Volt, robot4Volt, robot5Volt };
            robotAmpare = new Label[] { robot0Ampare, robot1Ampare, robot2Ampare, robot3Ampare, robot4Ampare, robot5Ampare };
            robotMission = new Label[] { robot0Mission, robot1Mission, robot2Mission, robot3Mission, robot4Mission, robot5Mission };
            robotBattery = new DevExpress.XtraEditors.ProgressBarControl[] { robot0Battery, robot1Battery, robot2Battery, robot3Battery, robot4Battery, robot5Battery };
            robotTemp = new Label[] { robot0Temp, robot1Temp, robot2Temp, robot3Temp, robot4Temp, robot5Temp };
            robotTime = new Label[] { robot0State, robot1Time, robot2Time, robot3Time, robot4Time, robot5Time };
            robotRemaincap = new Label[] { robot0Remaincap, robot1Remaincap, robot2Remaincap, robot3Remaincap, robot4Remaincap, robot5Remaincap };
            robotRemainenergy = new Label[] { robot0Remainenergy, robot1Remainenergy, robot2Remainenergy, robot3Remainenergy, robot4Remainenergy, robot5Remainenergy };
        }
        private void checkThestatus()
        {
            Panel[] robotPanel = { robot0Panel, robot1Panel, robot2Panel, robot3Panel, robot4Panel, robot5Panel };
            for (int i = 0; i < Data.Instance.robotStatus.Count(); i++)
            {
                if (Data.Instance.robotStatus[i] == true)
                {
                    robotPanel[i].Enabled = true;
                    th[i] = new Thread(() => getData(enableRobot[i],i));
                }
                else
                {
                    robotPanel[i].Enabled = false;
                }
            }
        }



        private void onSelectRobotbms_monitor()
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    for (int i = 0; i < enableRobot.Length; i++)
                    {
                        if (Data.Instance.robotStatus[i] == true)
                        {
                            mainform.commBridge.onBMS_subscribe(enableRobot[i]);
                            Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("onSelectRobotbms_monitor err=" + ex.Message.ToString());
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        string[] exhibition_missionlist = { "MID20190919165405", "MID20190930170453", "MID20190930170524", "MID20190930170546",
            "MID20190930170555", "MID20190930170618", "MID20190930170649", "MID20190930170656", "MID20191001145359","MID20191002144305"
                ,"MID20191006143551" };

        private void getData(string robotid,int kk)
        {
            float volt, current, temp, battery, health, chargetime, dischargetime, remainenergy, remaincap, errorhighvolt, errorlowvolt, errorcharge, errordischarge, errorhightemp, errorlowtemp, errorbmu;
            string robot_id, robottype;
            float radius;
            double robotxpose, robotypose, theta;
            int workstate;
            float amrcpu, amrmem, visioncpu, visionmem;
            double cmdlinear, cmdangular, feedlinear, feedangular;

            int rank;

            switch(robotid)
            {
                case "R_000":
                    rank = 0;
                    break;
                case "R_001":
                    rank = 1;
                    break;
                case "R_003":
                    rank = 2;
                    break;
                case "R_004":
                    rank = 3;
                    break;
                case "R_005":
                    rank = 4;
                    break;
                default:
                    break;
            }

            while (true)
            {
                try
                {
                    if (Data.Instance.isConnected)
                    {
                        if (Data.Instance.Robot_work_info[robotid].robot_status_info.bmsinfo != null)
                        {
                            if (Data.Instance.Robot_work_info[robotid].robot_status_info.bmsinfo.msg != null)
                            {
                                if (Data.Instance.Robot_work_info[robotid].robot_status_info.bmsinfo.msg.data.Count > 0)
                                {
                                    int missionid = Data.Instance.Robot_work_info[robotid].robot_status_info.taskfeedback.msg.feedback.mission_indx;
                                    volt = Data.Instance.Robot_work_info[robotid].robot_status_info.bmsinfo.msg.data[0];
                                    current = Data.Instance.Robot_work_info[robotid].robot_status_info.bmsinfo.msg.data[1];
                                    temp = Data.Instance.Robot_work_info[robotid].robot_status_info.bmsinfo.msg.data[2];
                                    battery = Data.Instance.Robot_work_info[robotid].robot_status_info.bmsinfo.msg.data[3];
                                    health = Data.Instance.Robot_work_info[robotid].robot_status_info.bmsinfo.msg.data[4];
                                    chargetime = Data.Instance.Robot_work_info[robotid].robot_status_info.bmsinfo.msg.data[5];
                                    dischargetime = Data.Instance.Robot_work_info[robotid].robot_status_info.bmsinfo.msg.data[6];
                                    remainenergy = Data.Instance.Robot_work_info[robotid].robot_status_info.bmsinfo.msg.data[7];
                                    remaincap = Data.Instance.Robot_work_info[robotid].robot_status_info.bmsinfo.msg.data[8];
                                    errorhighvolt = Data.Instance.Robot_work_info[robotid].robot_status_info.bmsinfo.msg.data[9];
                                    errorlowvolt = Data.Instance.Robot_work_info[robotid].robot_status_info.bmsinfo.msg.data[10];
                                    errorcharge = Data.Instance.Robot_work_info[robotid].robot_status_info.bmsinfo.msg.data[11];
                                    errordischarge = Data.Instance.Robot_work_info[robotid].robot_status_info.bmsinfo.msg.data[12];
                                    errorhightemp = Data.Instance.Robot_work_info[robotid].robot_status_info.bmsinfo.msg.data[13];
                                    errorlowtemp = Data.Instance.Robot_work_info[robotid].robot_status_info.bmsinfo.msg.data[14];
                                    errorbmu = Data.Instance.Robot_work_info[robotid].robot_status_info.bmsinfo.msg.data[15];

                                    robot_id = Data.Instance.Robot_work_info[robotid].robot_status_info.robotstate.msg.RID;
                                    robottype = Data.Instance.Robot_work_info[robotid].robot_status_info.robotstate.msg.type;
                                    robotxpose = Data.Instance.Robot_work_info[robotid].robot_status_info.robotstate.msg.pose.x;
                                    robotypose = Data.Instance.Robot_work_info[robotid].robot_status_info.robotstate.msg.pose.y;
                                    theta = Data.Instance.Robot_work_info[robotid].robot_status_info.robotstate.msg.pose.theta;
                                    workstate = Data.Instance.Robot_work_info[robotid].robot_status_info.robotstate.msg.workstate;



                                    //선속도
                                    feedlinear = Data.Instance.Robot_work_info[robotid].robot_status_info.motorstate.msg.feed_vel.linear.x;
                                    cmdlinear = Data.Instance.Robot_work_info[robotid].robot_status_info.motorstate.msg.cmd_vel.linear.x;

                                    //각속도
                                    feedangular = Data.Instance.Robot_work_info[robotid].robot_status_info.motorstate.msg.feed_vel.angular.z;
                                    cmdangular = Data.Instance.Robot_work_info[robotid].robot_status_info.motorstate.msg.cmd_vel.angular.z;

                                    Invoke(new MethodInvoker(delegate ()
                                    {
                                        robotState[kk].Text = "---";
                                        robotAmpare[kk].Text = string.Format("{0} A", current);
                                        robotVolt[kk].Text = string.Format("{0} V", volt);
                                        robotMission[kk].Text = string.Format("{0}", exhibition_missionlist[missionid]);
                                        robotBattery[kk].Position = Convert.ToInt32(battery);
                                        robotTime[kk].Text = string.Format("{0}시간 {1}분", dischargetime/60, dischargetime%60);
                                        robotTemp[kk].Text = string.Format("{0}℃", temp);

                                    }));





                                }
                                else
                                {
                                    Invoke(new MethodInvoker(delegate ()
                                    {
                                        robotBattery[kk].Position = 0;
                                    }));
                                }
                            }
                        }
                    }

                }


                catch
                { }

                finally
                { }

                Thread.Sleep(500);
            }
        }

        
    }
}
