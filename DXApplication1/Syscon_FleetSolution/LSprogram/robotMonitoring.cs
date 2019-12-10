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

namespace Syscon_Solution.LSprogram
{
    public partial class robotMonitoring : UserControl
    {

        LSprogram.mainForm mainform;

        public robotMonitoring(mainForm frm)
        {
            InitializeComponent();
            mainform = frm;
        }
        public robotMonitoring()
        {
            InitializeComponent();
        }

        public void threadStart()
        {
            th = new Thread(DisplayValue);
            th.IsBackground = true;
            th.Start();
        }

        Thread th;
        float volt, current, temp, battery, health, chargetime, dischargetime, remainenergy, remaincap, errorhighvolt, errorlowvolt, errorcharge, errordischarge, errorhightemp, errorlowtemp, errorbmu;
        float amrcpu, amrmem, visioncpu, visionmem;
        string robot_id, robottype;
        int workstate;

        private void DisplayValue()
        {
            string strrobotid = Data.Instance.robot_id;

            while (true)
            {
                try
                {
                    robotBattery.Value = (float)(Data.Instance.Robot_work_info[strrobotid].robot_status_info.bmsinfo.msg.data[3]);



                    volt = Data.Instance.Robot_work_info[strrobotid].robot_status_info.bmsinfo.msg.data[0];
                    current = Data.Instance.Robot_work_info[strrobotid].robot_status_info.bmsinfo.msg.data[1];
                    temp = Data.Instance.Robot_work_info[strrobotid].robot_status_info.bmsinfo.msg.data[2];
                    battery = Data.Instance.Robot_work_info[strrobotid].robot_status_info.bmsinfo.msg.data[3];
                    health = Data.Instance.Robot_work_info[strrobotid].robot_status_info.bmsinfo.msg.data[4];
                    chargetime = Data.Instance.Robot_work_info[strrobotid].robot_status_info.bmsinfo.msg.data[5];
                    dischargetime = Data.Instance.Robot_work_info[strrobotid].robot_status_info.bmsinfo.msg.data[6];
                    remainenergy = Data.Instance.Robot_work_info[strrobotid].robot_status_info.bmsinfo.msg.data[7];
                    remaincap = Data.Instance.Robot_work_info[strrobotid].robot_status_info.bmsinfo.msg.data[8];
                    errorhighvolt = Data.Instance.Robot_work_info[strrobotid].robot_status_info.bmsinfo.msg.data[9];
                    errorlowvolt = Data.Instance.Robot_work_info[strrobotid].robot_status_info.bmsinfo.msg.data[10];
                    errorcharge = Data.Instance.Robot_work_info[strrobotid].robot_status_info.bmsinfo.msg.data[11];
                    errordischarge = Data.Instance.Robot_work_info[strrobotid].robot_status_info.bmsinfo.msg.data[12];
                    errorhightemp = Data.Instance.Robot_work_info[strrobotid].robot_status_info.bmsinfo.msg.data[13];
                    errorlowtemp = Data.Instance.Robot_work_info[strrobotid].robot_status_info.bmsinfo.msg.data[14];
                    errorbmu = Data.Instance.Robot_work_info[strrobotid].robot_status_info.bmsinfo.msg.data[15];

                    robot_id = Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate.msg.RID;
                    robottype = Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate.msg.type;
                    workstate = Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate.msg.workstate;

                    amrcpu = Data.Instance.Robot_work_info[strrobotid].robot_status_info.controllerstate.msg.data[0];
                    amrmem = Data.Instance.Robot_work_info[strrobotid].robot_status_info.controllerstate.msg.data[1];

                    Invoke(new MethodInvoker(delegate ()
                    {
                        robotVolt.Text = string.Format("{0:f1} V", volt);
                        robotidLabel.Text = robot_id;
                        robothardwareLabel.Text = robottype;
                        robotremaintimeLabel.Text = string.Format("{0:f0} 시간 {1}분", dischargetime / 60, dischargetime % 60);
                        robotremaincapLabel.Text = remaincap.ToString("N1");
                        robotremainenergyLabel.Text = remainenergy.ToString("N1");

                        robotbatteryhelthLabel.Text = health.ToString("N1");
                        robotBattery.Value = battery;
                        robotcpuLabel.Text = string.Format("{0:f1}", amrcpu);
                        robotramLabel.Text = string.Format("{0:f1}", amrmem);
                        robotvoltScale.Value = volt;
                        robotampareScale.Value = current;
                    //veloScale.Value = (float)feedlinear;
                    //angularScale.Value = (float)feedangular;
                    robotcpuLabel.Text = string.Format("{0} %", amrcpu.ToString("N0"));
                        robotramLabel.Text = string.Format("{0} %", amrmem.ToString("N0"));
                    //robotVelo.Text = feedlinear.ToString("N1");
                    //robotAngular.Text = feedangular.ToString("N1");

                }));
                    
                }
                catch
                {

                }
                Thread.Sleep(300);
            }
        }

    }
}
