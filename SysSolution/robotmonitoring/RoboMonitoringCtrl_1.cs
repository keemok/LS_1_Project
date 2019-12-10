using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Windows.Forms.DataVisualization.Charting;

using System.Drawing.Drawing2D;

using System.Drawing.Imaging;
using System.IO;

namespace SysSolution.robotmonitoring
{
    public partial class RoboMonitoringCtrl_1 : UserControl
    {
        RobotStatusForm_One mainForm;

        public string m_strRobotName = "";
        string m_strCurrentRobotDetailInfo_ID = "";
        public bool m_bLive = false;

        public string m_strLog_File = "";

        public RoboMonitoringCtrl_1()
        {
            InitializeComponent();
        }
        public RoboMonitoringCtrl_1(RobotStatusForm_One mfrm)
        {
            mainForm = mfrm;
            InitializeComponent();
        }


        private void RoboMonitoringCtrl_1_Load(object sender, EventArgs e)
        {
            
        }

        public void onInitSet()
        {
            if (Data.Instance.isConnected)
            {
                mainForm.worker.Globalpath_Evt += new Worker.GlobalpathComplete(this.GlobalpathComplete);

                m_strCurrentRobotDetailInfo_ID = m_strRobotName;

                UI_Updatetimer.Interval = 100;
                UI_Updatetimer.Enabled = true;

                myTimer.Interval = 100;
                myTimer.Enabled = true;

                progressBar_capacity.Value = 0;

      //          timer_BatteryLogSave.Interval = 1000 * 60;
        //        timer_BatteryLogSave.Enabled = true;
            }
        }


        public void onTimerOff()
        {
            UI_Updatetimer.Enabled = false;
            myTimer.Enabled = false;
        }

        private void onLampVisable(bool bv1, bool bv2, bool bv3)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                lblLamp_idle.Visible = bv1;
                lblLamp_Run.Visible = bv2;
                lblLamp_Warnnig.Visible = bv3;
            }));

        }

        private void UI_Updatetimer_Tick(object sender, EventArgs e)
        {
            onRobotStatusDP_Update();
        }
        public void onRobotStatusDP_Update()
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    if (Data.Instance.Robot_work_info.ContainsKey(m_strRobotName))
                    {

                        string strrobotmodel = "";
                        double x = 0, y = 0, theta = 0;

                        if (Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.robotstate.msg == null)
                        {
                            return;
                        }


                        if (Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.robotstate.msg != null)
                        {
                            strrobotmodel = Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.robotstate.msg.type.ToString();

                            if (m_strCurrentRobotDetailInfo_ID == "R_005") strrobotmodel = "500_1";
                            else if (m_strCurrentRobotDetailInfo_ID == "R_006") strrobotmodel = "500_2";
                            else if (m_strCurrentRobotDetailInfo_ID == "R_007") strrobotmodel = "100A";
                            else if (m_strCurrentRobotDetailInfo_ID == "R_008") strrobotmodel = "300C";
                        }

                        if (Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.robotstate.msg != null)
                        {
                            x = Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.robotstate.msg.pose.x;

                            y = Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.robotstate.msg.pose.y;
                            theta = Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.robotstate.msg.pose.theta;
                        }


                        Invoke(new MethodInvoker(delegate ()
                        {
                            txtRobotModel.Text = strrobotmodel;
                        }));

                        if (Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.robotstate.msg.type == "TR50.1")
                        {
                            if (Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.robotstate.msg.workstate == 11 || Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.robotstate.msg.workstate == 0)
                            {
                                onLampVisable(true, false, false);
                            }
                            else if (Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.robotstate.msg.workstate == 41)
                            {
                                onLampVisable(false, true, false);
                            }
                            else if (Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.robotstate.msg.workstate == 51)
                            {
                                onLampVisable(false, true, false);
                            }
                            else if (Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.robotstate.msg.workstate == 91)
                            {
                                onLampVisable(false, false, true);
                            }

                            else
                            {
                                onLampVisable(false, false, false);
                            }
                        }
                        else
                        {
                            //onLampVisable(true);
                            if (Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.robotstate.msg.workstate == 0)
                            {
                                onLampVisable(true, false, false);
                            }
                            else if (Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.robotstate.msg.workstate == 1)
                            {
                                onLampVisable(false, true, false);
                            }
                            else if (Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.robotstate.msg.workstate == 2)
                            {
                                onLampVisable(false, true, false);
                            }
                            else if (Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.robotstate.msg.workstate == 3)
                            {
                                onLampVisable(false, false, true);
                            }
                            else if (Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.robotstate.msg.workstate == 4)
                            {
                                onLampVisable(false, false, true);
                            }
                            else
                            {
                                onLampVisable(false, false, false);
                            }
                        }


                        robot_image = Properties.Resource_jo.Robot2;
                        opacity_image = robot_image;
                        Random ra = new Random();
                        opacity_image = RotateImage(robot_image, new PointF(robot_image.Width / 2, robot_image.Height / 2), (float)(theta * 180 / 3.14));
                        Robot_pictureBox.Invalidate();

                        if (Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.robotstate.msg != null)
                        {

                            int nliftstate = Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.robotstate.msg.lift_status;
                            if (nliftstate == 0 || nliftstate == -1 || nliftstate == -2)
                            {
                                robot_image = Properties.Resource_jo.Robot2;
                                lblGoodsStatus.Text = "";
                            }
                            else if (nliftstate == 2 || nliftstate == 1)
                            {
                                robot_image = Properties.Resource_jo.Robot_goods;
                                lblGoodsStatus.Text = "짐(황산)";
                            }


                        }


                        onRobotSpeed_DP();
                        onBMS_DP();

                        onLidar_Graph();
                        Invoke(new MethodInvoker(delegate ()
                        {
                            lblangular.Text = string.Format("{0:f2}", (float)(theta * 180 / 3.14));
                        }));

                        onRobotAgular_Graph();
                        onRobotGlobalpath_Graph();

                        onCamMonitoring();
                        onCam1Monitoring();
                    }
                  
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobotStatusDP_Update err :={0}", ex.Message.ToString());
            }
        }

        private void GlobalpathComplete(string strrobotid)
        {
            if (strrobotid == m_strRobotName)
            {
                if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.globalplan.msg.poses.Count > 0)
                {
                    List<PoseStamped> pose = new List<PoseStamped>();
                    pose = Data.Instance.Robot_work_info[strrobotid].robot_status_info.globalplan.msg.poses;

                    int ncnt = pose.Count;
                    robot_globalpath_x = new double[ncnt];
                    robot_globalpath_y = new double[ncnt];

                    double x = pose[0].pose.position.x;
                    double y = pose[0].pose.position.y;

                    for (int i = 0; i < ncnt; i++)
                    {
                        robot_globalpath_x[i] = pose[i].pose.position.x;
                        robot_globalpath_y[i] = pose[i].pose.position.y;
                    }

                    robot_path_x = new double[1000];
                    robot_path_y = new double[1000];
                }
            }
        }


        byte[] sourceMapValues;
        byte[] sourceMapValues2;
        int nSourceMapWidth = 0;
        int nSourceMapHeight = 0;
        private void onCamMonitoring()
        {
            if (Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.cam2 == null) return;

            CamInformation cam = Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.cam2;

            byte[] bytecam = Convert.FromBase64String(cam.msg.data);
            int height = cam.msg.height;
            int width = cam.msg.width;

            nSourceMapWidth = width;
            nSourceMapHeight = height;

            sourceMapValues = new byte[width * height * 3];

            for (var y = 0; y < width * height * 3; y++)
            {
                sourceMapValues[y] = bytecam[y];
            }

            string startPath = Application.StartupPath;
            string path = string.Empty;
            path = startPath + @"\mapinfo.bmp";

            try
            {
                Bitmap bmSource2 = new Bitmap(width, height, PixelFormat.Format24bppRgb);

                Rectangle rect2 = new Rectangle(0, 0, bmSource2.Width, bmSource2.Height);
                System.Drawing.Imaging.BitmapData bmpData2 =
                    bmSource2.LockBits(rect2, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                    bmSource2.PixelFormat);

                IntPtr ptr = bmpData2.Scan0;

                {
                    System.Runtime.InteropServices.Marshal.Copy(sourceMapValues, 0, ptr, width * height * 3);
                }

                bmSource2.UnlockBits(bmpData2);

                System.Drawing.Rectangle cropArea = new System.Drawing.Rectangle(0, 0, width, height);
                Bitmap bmpTemp = bmSource2.Clone(cropArea, bmSource2.PixelFormat);

                pictureBox1.Image = bmpTemp;

                bmSource2.Dispose();
                bmSource2 = null;
                bmSource2 = (Bitmap)(bmpTemp.Clone());

                // bmSource2.Save(path, ImageFormat.Bmp);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onCamMonitoring err :={0}", ex.Message.ToString());
            }
        }

        private void onCam1Monitoring()
        {
            if (Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.cam1 == null) return;

            CamInformation cam = Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.cam1;

            byte[] bytecam = Convert.FromBase64String(cam.msg.data);
            int height = cam.msg.height;
            int width = cam.msg.width;

            //nSourceMapWidth = width;
            //nSourceMapHeight = height;

            sourceMapValues2 = new byte[width * height * 3];

            for (var y = 0; y < width * height * 3; y++)
            {
                sourceMapValues2[y] = bytecam[y];
            }

            string startPath = Application.StartupPath;
            string path = string.Empty;
            path = startPath + @"\mapinfo2.bmp";

            try
            {
                Bitmap bmSource2 = new Bitmap(width, height, PixelFormat.Format24bppRgb);

                Rectangle rect2 = new Rectangle(0, 0, bmSource2.Width, bmSource2.Height);
                System.Drawing.Imaging.BitmapData bmpData2 =
                    bmSource2.LockBits(rect2, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                    bmSource2.PixelFormat);

                IntPtr ptr = bmpData2.Scan0;

                {
                    System.Runtime.InteropServices.Marshal.Copy(sourceMapValues2, 0, ptr, width * height * 3);
                }

                bmSource2.UnlockBits(bmpData2);

                System.Drawing.Rectangle cropArea = new System.Drawing.Rectangle(0, 0, width, height);
                Bitmap bmpTemp = bmSource2.Clone(cropArea, bmSource2.PixelFormat);

                pictureBox2.Image = bmpTemp;
                pictureBox2.Image.RotateFlip(RotateFlipType.Rotate180FlipX);
                pictureBox2.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);

                bmSource2.Dispose();
                bmSource2 = null;
                bmSource2 = (Bitmap)(bmpTemp.Clone());

                // bmSource2.Save(path, ImageFormat.Bmp);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onCam1Monitoring err :={0}", ex.Message.ToString());
            }
        }

        #region 모니터링 데이타 DP 관련
        private void onLidar_Graph()
        {
            try
            {
                //pictureBox1.Invalidate();

                if (Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.lidar.msg != null)
                {
                    float angle_incre = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.lidar.msg.angle_increment;
                    float scan_time = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.lidar.msg.scan_time;
                    float range_min = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.lidar.msg.range_min;
                    float range_max = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.lidar.msg.range_max;

                    int nangle_cnt = 360 / (int)(angle_incre * 60);

                    int nrange = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.lidar.msg.ranges.Count;

                    robot_lidar = new float[nrange];
                    robot_lidar = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.lidar.msg.ranges.ToArray();

                    RadarChartSetting();
                    RadarViewAll();
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onLidar_DP err :={0}", ex.Message.ToString());
            }
        }

        private void onMotorstate_DP()
        {
            try
            {
                if (Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg != null)
                {
                    float motorspeedsetting_right = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.cmd_rpm.data[0];
                    float motorspeedsetting_left = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.cmd_rpm.data[1];
                    float motorspeedreturn_right = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.rpm.data[0];
                    float motorspeedreturn_left = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.rpm.data[1];

                    double motor_Robotspeed_setting_linear_x = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.cmd_vel.linear.x;
                    double motor_Robotspeed_setting_linear_y = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.cmd_vel.linear.y;
                    double motor_Robotspeed_setting_linear_z = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.cmd_vel.linear.z;
                    double motor_Robotspeed_setting_angular_x = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.cmd_vel.angular.x;
                    double motor_Robotspeed_setting_angular_y = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.cmd_vel.angular.y;
                    double motor_Robotspeed_setting_angular_z = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.cmd_vel.angular.z;

                    double motor_Robotspeed_returnlinear_x = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.feed_vel.linear.x;
                    double motor_Robotspeed_return_linear_y = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.feed_vel.linear.y;
                    double motor_Robotspeed_return_linear_z = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.feed_vel.linear.z;
                    double motor_Robotspeed_return_angular_x = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.feed_vel.angular.x;
                    double motor_Robotspeed_return_angular_y = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.feed_vel.angular.y;
                    double motor_Robotspeed_return_angular_z = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.feed_vel.angular.z;

                   
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onMotorstate_DP err :={0}", ex.Message.ToString());
            }
        }
        private void onUltrasonic_DP()
        {
            try
            {
                if (Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.ultrasonic.msg != null)
                {
                    if (Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.ultrasonic.msg.data.Count < 1) return;

                    float ultrasonicraw_1 = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.ultrasonic.msg.data[0];
                    float ultrasonicraw_2 = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.ultrasonic.msg.data[1];
                    float ultrasonicraw_3 = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.ultrasonic.msg.data[2];
                    float ultrasonicraw_4 = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.ultrasonic.msg.data[3];
                    float ultrasonicraw_5 = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.ultrasonic.msg.data[4];
                    float ultrasonicraw_6 = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.ultrasonic.msg.data[5];
                    float ultrasonicraw_7 = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.ultrasonic.msg.data[6];
                    float ultrasonicraw_8 = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.ultrasonic.msg.data[7];
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onUltrasonic_DP err :={0}", ex.Message.ToString());
            }
        }

        private void onLidar_DP()
        {
            try
            {
                if (Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.lidar.msg != null)
                {
                    float angle_min = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.lidar.msg.angle_min;
                    float angle_max = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.lidar.msg.angle_max;
                    float angle_incre = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.lidar.msg.angle_increment;
                    float time_incre = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.lidar.msg.time_increment;
                    float scan_time = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.lidar.msg.scan_time;
                    float range_min = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.lidar.msg.range_min;
                    float range_max = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.lidar.msg.range_max;
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onLidar_DP err :={0}", ex.Message.ToString());
            }
        }

        private void onBMS_DP()
        {
            try
            {
                if (Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg != null)
                {
                    if (Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data.Count < 1)
                    {
                        return;
                    }

                    float volt = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[0];
                    float amp = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[1];
                    float temper = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[2];
                    float capacity = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[3];
                    float heathy = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[4];
                    float chargetime = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[5];
                    float dischargetime = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[6];
                    float remaincapacity = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[7];
                    float remainenergy = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[8];
                    float err_overVolt = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[9];
                    float err_underVolt = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[10];
                    float err_chargeover = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[11];
                    float err_dischargeover = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[12];
                    float err_hightemper = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[13];
                    float err_lowtemper = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[14];
                    float err_bmu = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[15];




                    Invoke(new MethodInvoker(delegate ()
                    {
                        // txtangle_min.Text = string.Format("{0:F3}", angle_min);
                        // txtVolt.Text= string.Format("{0:F2}", volt);
                        // txtTemper.Text=string.Format("{0:F1}", temper);

                        lblBatterycapacity.Text = string.Format("배터리용량({0}%)", (int)capacity);
                        progressBar_capacity.Value = (int)capacity;
                        if (capacity < 20)
                        {

                            progressBar_capacity.onBarcolor(Brushes.Red);
                            progressBar_capacity.ForeColor = Color.Red;
                        }
                        else
                        {
                            progressBar_capacity.onBarcolor(Brushes.Green);
                        }

                        txtTemper1.Text = string.Format("({0})", (int)temper); ;

                        //    txtHealth.Text= string.Format("{0:F}", heathy);
                        //    txtChargeTime.Text = string.Format("{0:F}", chargetime);
                        //    txtDischargeTime.Text = string.Format("{0:F}", dischargetime);
                        //    txtRemainCapacity.Text = string.Format("{0:F2}", remaincapacity);
                        //    txtRemainEnergy.Text = string.Format("{0:F2}", remainenergy);

                        ChartBMSSetting();
                        ViewBMSAll();
                    }));
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onBMS_DP err :={0}", ex.Message.ToString());
            }
        }



    
        private void onRobotSpeed_DP()
        {
            ChartSetting();
            ViewAll();
        }


        #endregion

        #region 선속도 각속도 그래프 관련
        double[] robot_linear_speed = new double[1000];
        double[] robot_anglua_speed = new double[1000];
        int nlinearspeed_idx = 0;
        int nangularspeed_idx = 0;
        private void ChartSetting()
        {
            //0.디폴트 ChartAreas와 Series 삭제

            robotspeed_chart.ChartAreas.Clear();
            robotspeed_chart.Series.Clear();
            //1.ChartAreas
            robotspeed_chart.ChartAreas.Add("Draw"); ;
            robotspeed_chart.ChartAreas["Draw"].BackColor = Color.Black;
            robotspeed_chart.ChartAreas["Draw"].AxisX.Minimum = 0;
            robotspeed_chart.ChartAreas["Draw"].AxisX.Maximum = 120;
            robotspeed_chart.ChartAreas["Draw"].AxisX.Interval = 20;
            robotspeed_chart.ChartAreas["Draw"].AxisX.Title = "시간";
            robotspeed_chart.ChartAreas["Draw"].AxisX.MajorGrid.LineColor = Color.Gray;
            robotspeed_chart.ChartAreas["Draw"].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;  //너무길면 using 으로 보내주자
            robotspeed_chart.ChartAreas["Draw"].AxisY.Minimum = -1.5;
            robotspeed_chart.ChartAreas["Draw"].AxisY.Maximum = 1.5;
            robotspeed_chart.ChartAreas["Draw"].AxisY.Interval = 0.3;
            robotspeed_chart.ChartAreas["Draw"].AxisY.Title = "속도";
            robotspeed_chart.ChartAreas["Draw"].AxisY.MajorGrid.LineColor = Color.Gray;
            robotspeed_chart.ChartAreas["Draw"].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            //2.Series
            robotspeed_chart.Series.Add("Liner");
            robotspeed_chart.Series["Liner"].ChartType = SeriesChartType.Line;
            robotspeed_chart.Series["Liner"].Color = Color.LightGreen;
            robotspeed_chart.Series["Liner"].BorderWidth = 2; ;
            robotspeed_chart.Series["Liner"].LegendText = "선속도(m/s)"; //레전드는 범례

            robotspeed_chart.Series.Add("Angular");
            robotspeed_chart.Series["Angular"].ChartType = SeriesChartType.Line;
            robotspeed_chart.Series["Angular"].Color = Color.Orange;
            robotspeed_chart.Series["Angular"].BorderWidth = 2; ;
            robotspeed_chart.Series["Angular"].LegendText = "각속도(rad/s)"; //레전드는 범례
            //줌기능을 넣어보자!
            robotspeed_chart.ChartAreas["Draw"].CursorX.IsUserSelectionEnabled = true;
            //스크롤 색변경
            robotspeed_chart.ChartAreas["Draw"].AxisX.ScrollBar.ButtonColor = Color.Blue;
        }

        private void ViewAll()
        {
            Random ra = new Random();

            //데이터는 ecg배열 ppg배열에 있어
            foreach (double x in robot_linear_speed)
            {
                robotspeed_chart.Series["Liner"].Points.Add(x);
            }
            foreach (double x in robot_anglua_speed)
            {
                robotspeed_chart.Series["Angular"].Points.Add(x);
            }
        }
        #endregion

        /// <summary> 그래프 데이타 저장할 타이머 </summary>
        private void myTimer_Tick(object sender, EventArgs e)
        {
            //       ChartSetting();
            //       ViewAll();
            try
            {
                if (Data.Instance.isConnected)
                {
                    if (m_strCurrentRobotDetailInfo_ID == "")
                    {
                    }
                    else
                    {
                        if (Data.Instance.Robot_work_info.ContainsKey(m_strCurrentRobotDetailInfo_ID))
                        {
                            #region 선속도, 각속도 데이타배열 저장
                            if (Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg != null)
                            {
                                double motor_Robotspeed_returnlinear_x = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.feed_vel.linear.x;
                                if (nlinearspeed_idx < 120)
                                {
                                    //선속도
                                    robot_linear_speed[nlinearspeed_idx] = motor_Robotspeed_returnlinear_x;
                                    nlinearspeed_idx++;
                                }
                                else
                                {
                                    for (int i = 0; i < 119; i++)
                                    {
                                        robot_linear_speed[i] = robot_linear_speed[i + 1];
                                        robot_linear_speed[119] = motor_Robotspeed_returnlinear_x;
                                    }
                                }

                                //각속도
                                double motor_Robotspeed_return_angular_z = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.feed_vel.angular.z;

                                if (nangularspeed_idx < 120)
                                {
                                    //선속도
                                    robot_anglua_speed[nangularspeed_idx] = motor_Robotspeed_return_angular_z;
                                    nangularspeed_idx++;
                                }
                                else
                                {
                                    for (int i = 0; i < 119; i++)
                                    {
                                        robot_anglua_speed[i] = robot_anglua_speed[i + 1];
                                        robot_anglua_speed[119] = motor_Robotspeed_return_angular_z;
                                    }
                                }
                            }
                            #endregion

                            #region #region angluar 데이타배열 저장
                            if (Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.robotstate.msg != null)
                            {
                                double bms_return_angular = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.robotstate.msg.pose.theta;
                                bms_return_angular = (bms_return_angular * 180 / 3.14);


                                if (nrobotangluar_idx < 120)
                                {
                                    robot_Robot_Angluar[nrobotangluar_idx] = bms_return_angular;
                                    nrobotangluar_idx++;
                                }
                                else
                                {
                                    for (int i = 0; i < 119; i++)
                                    {
                                        robot_Robot_Angluar[i] = robot_Robot_Angluar[i + 1];
                                        robot_Robot_Angluar[119] = bms_return_angular;
                                    }
                                }

                                double x1 = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.robotstate.msg.pose.x;

                                double y1 = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.robotstate.msg.pose.y;

                                if (nrobotpath_idx < 500)
                                {
                                    robot_path_x[nrobotpath_idx] = x1;
                                    robot_path_y[nrobotpath_idx] = y1;
                                    nrobotpath_idx++;
                                }
                                else
                                {
                                    for (int i = 0; i < 499; i++)
                                    {
                                        robot_path_x[i] = robot_path_x[i + 1];
                                        robot_path_x[499] = x1;

                                        robot_path_y[i] = robot_path_y[i + 1];
                                        robot_path_y[499] = y1;
                                    }
                                }

                            }
                            #endregion

                            #region bms 데이타배열 저장
                            if (Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg != null)
                            {
                                if (Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data.Count < 1) return;

                                double bms_return_amp = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[1];
                                double bms_return_temper = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[2];
                                double bms_return_capacity = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[3];

                                if (bms_return_amp < 0) bms_return_amp = bms_return_amp * -1;

                                bms_return_amp *= 10;


                                if (nbms_amp_idx < 120)
                                {
                                    robot_BMS_ampr[nbms_amp_idx] = bms_return_temper;
                                    nbms_amp_idx++;
                                }
                                else
                                {
                                    for (int i = 0; i < 119; i++)
                                    {
                                        robot_BMS_ampr[i] = robot_BMS_ampr[i + 1];
                                        robot_BMS_ampr[119] = bms_return_amp;
                                    }
                                }

                                if (nbms_temper_idx < 120)
                                {
                                    robot_BMS_temper[nbms_temper_idx] = bms_return_temper;
                                    nbms_temper_idx++;
                                }
                                else
                                {
                                    for (int i = 0; i < 119; i++)
                                    {
                                        robot_BMS_temper[i] = robot_BMS_temper[i + 1];
                                        robot_BMS_temper[119] = bms_return_temper;
                                    }
                                }

                                if (nbms_capacity_idx < 120)
                                {

                                    robot_BMS_capacity[nbms_capacity_idx] = bms_return_capacity;
                                    nbms_capacity_idx++;
                                }
                                else
                                {
                                    for (int i = 0; i < 119; i++)
                                    {
                                        robot_BMS_capacity[i] = robot_BMS_capacity[i + 1];
                                        robot_BMS_capacity[119] = bms_return_capacity;
                                    }
                                }



                            }
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("myTimer_Tick err :={0}", ex.Message.ToString());
            }

        }

        #region 로봇angluar 그래프 관련
        double[] robot_Robot_Angluar = new double[121];
        int nrobotangluar_idx = 0;

        private void onRobotAgular_Graph()
        {

            ChartRobotAngluarSetting();
            ViewRobotAngluarAll();
        }

        private void ChartRobotAngluarSetting()
        {
            //0.디폴트 ChartAreas와 Series 삭제

            chart_angluar.ChartAreas.Clear();
            chart_angluar.Series.Clear();
            //1.ChartAreas
            chart_angluar.ChartAreas.Add("Draw"); ;
            chart_angluar.ChartAreas["Draw"].BackColor = Color.Black;
            chart_angluar.ChartAreas["Draw"].AxisX.Minimum = 0;
            chart_angluar.ChartAreas["Draw"].AxisX.Maximum = 120;
            chart_angluar.ChartAreas["Draw"].AxisX.Interval = 20;
            chart_angluar.ChartAreas["Draw"].AxisX.Title = "시간";
            chart_angluar.ChartAreas["Draw"].AxisX.MajorGrid.LineColor = Color.Gray;
            chart_angluar.ChartAreas["Draw"].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;  //너무길면 using 으로 보내주자
            //     chart_angluar.ChartAreas["Draw"].AxisY.Minimum = -3.14;
            //     chart_angluar.ChartAreas["Draw"].AxisY.Maximum = 3.14;
            //     chart_angluar.ChartAreas["Draw"].AxisY.Interval = 0.314;

            chart_angluar.ChartAreas["Draw"].AxisY.Minimum = -180;
            chart_angluar.ChartAreas["Draw"].AxisY.Maximum = 180;
            chart_angluar.ChartAreas["Draw"].AxisY.Interval = 20;
            chart_angluar.ChartAreas["Draw"].AxisY.Title = "각도";
            chart_angluar.ChartAreas["Draw"].AxisY.MajorGrid.LineColor = Color.Gray;
            chart_angluar.ChartAreas["Draw"].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            //2.Series
            chart_angluar.Series.Add("angular");
            chart_angluar.Series["angular"].ChartType = SeriesChartType.Line;
            chart_angluar.Series["angular"].Color = Color.Yellow;
            chart_angluar.Series["angular"].BorderWidth = 2; ;
            chart_angluar.Series["angular"].LegendText = "각도"; //레전드는 범례

            //줌기능을 넣어보자!
            chart_angluar.ChartAreas["Draw"].CursorX.IsUserSelectionEnabled = true;
            //스크롤 색변경
            chart_angluar.ChartAreas["Draw"].AxisX.ScrollBar.ButtonColor = Color.Blue;
        }

        private void ViewRobotAngluarAll()
        {
            Random ra = new Random();

            //데이터는 ecg배열 ppg배열에 있어
            foreach (double x in robot_Robot_Angluar)
            {
                chart_angluar.Series["angular"].Points.Add(x);

            }


        }
        #endregion

        #region Globalpath 그래프 관련
        double[] robot_path_x = new double[500];
        double[] robot_path_y = new double[500];
        int nrobotpath_idx = 0;

        double[] robot_globalpath_x = new double[1000];
        double[] robot_globalpath_y = new double[1000];
        int nrobotgolbalpath_idx = 0;
        string strX = "X";
        string strY = "Y";
        int nXmax = 0;
        int nXmin = 0;
        double nXinterval = 0;
        int nYmax = 0;
        int nYmin = 0;
        double nYinterval = 0;

       
        private void onRobotGlobalpath_Graph()
        {
            ChartRobotGlobalpathSetting();
            ViewRobotGlobalpathAll();
        }

        private void ChartRobotGlobalpathSetting()
        {
            //0.디폴트 ChartAreas와 Series 삭제

            chart_globalpath.ChartAreas.Clear();
            chart_globalpath.Series.Clear();
            //1.ChartAreas
            chart_globalpath.ChartAreas.Add("Draw"); ;
            chart_globalpath.ChartAreas["Draw"].BackColor = Color.White;
            chart_globalpath.ChartAreas["Draw"].AxisX.Minimum = -2;
            chart_globalpath.ChartAreas["Draw"].AxisX.Maximum = 5;
            chart_globalpath.ChartAreas["Draw"].AxisX.Interval = 0.5;
            chart_globalpath.ChartAreas["Draw"].AxisX.Title = strX;
            chart_globalpath.ChartAreas["Draw"].AxisX.MajorGrid.LineColor = Color.Gray;
            chart_globalpath.ChartAreas["Draw"].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Solid;  //너무길면 using 으로 보내주자

            chart_globalpath.ChartAreas["Draw"].AxisY.Minimum = -2;
            chart_globalpath.ChartAreas["Draw"].AxisY.Maximum = 5;
            chart_globalpath.ChartAreas["Draw"].AxisY.Interval = 0.5;
            chart_globalpath.ChartAreas["Draw"].AxisY.Title = strY;
            chart_globalpath.ChartAreas["Draw"].AxisY.MajorGrid.LineColor = Color.Gray;
            chart_globalpath.ChartAreas["Draw"].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Solid;
            //2.Series
            chart_globalpath.Series.Add("globalpath");
            chart_globalpath.Series["globalpath"].ChartType = SeriesChartType.Line;
            chart_globalpath.Series["globalpath"].Color = Color.Yellow;
            chart_globalpath.Series["globalpath"].BorderWidth = 3; ;
            chart_globalpath.Series["globalpath"].LegendText = "path"; //레전드는 범례

            chart_globalpath.Series.Add("pos");
            chart_globalpath.Series["pos"].ChartType = SeriesChartType.Line;
            chart_globalpath.Series["pos"].Color = Color.Red;
            chart_globalpath.Series["pos"].BorderWidth = 1; ;
            chart_globalpath.Series["pos"].LegendText = "pos"; //레전드는 범례

            //줌기능을 넣어보자!
            chart_globalpath.ChartAreas["Draw"].CursorX.IsUserSelectionEnabled = true;
            //스크롤 색변경
            chart_globalpath.ChartAreas["Draw"].AxisX.ScrollBar.ButtonColor = Color.Blue;
        }

        private void ViewRobotGlobalpathAll()
        {
            Random ra = new Random();

            //데이터는 ecg배열 ppg배열에 있어
            int ncnt = robot_globalpath_x.Length;
            for (int i = 0; i < ncnt; i++)
            {
                chart_globalpath.Series["globalpath"].Points.AddXY(robot_globalpath_x[i], robot_globalpath_y[i]);
            }

            for (int i = 0; i < 499; i++)
            {
                if (robot_path_x[i] == 0 && robot_path_y[i] == 0)
                    continue;
                chart_globalpath.Series["pos"].Points.AddXY(robot_path_x[i], robot_path_y[i]);
            }


        }
        #endregion

        #region 배터리 그래프 관련
        double[] robot_BMS_ampr = new double[1000];
        double[] robot_BMS_temper = new double[1000];
        double[] robot_BMS_capacity = new double[1000];
        int nbms_amp_idx = 0;
        int nbms_temper_idx = 0;
        int nbms_capacity_idx = 0;


        private void ChartBMSSetting()
        {
            //0.디폴트 ChartAreas와 Series 삭제

            chart_battery.ChartAreas.Clear();
            chart_battery.Series.Clear();
            //1.ChartAreas
            chart_battery.ChartAreas.Add("Draw"); ;
            chart_battery.ChartAreas["Draw"].BackColor = Color.Black;
            chart_battery.ChartAreas["Draw"].AxisX.Minimum = 0;
            chart_battery.ChartAreas["Draw"].AxisX.Maximum = 120;
            chart_battery.ChartAreas["Draw"].AxisX.Interval = 20;
            chart_battery.ChartAreas["Draw"].AxisX.Title = "시간";
            chart_battery.ChartAreas["Draw"].AxisX.MajorGrid.LineColor = Color.Gray;
            chart_battery.ChartAreas["Draw"].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;  //너무길면 using 으로 보내주자
            chart_battery.ChartAreas["Draw"].AxisY.Minimum = -20;
            chart_battery.ChartAreas["Draw"].AxisY.Maximum = 110;
            chart_battery.ChartAreas["Draw"].AxisY.Interval = 20;
            chart_battery.ChartAreas["Draw"].AxisY.Title = "배터리";
            chart_battery.ChartAreas["Draw"].AxisY.MajorGrid.LineColor = Color.Gray;
            chart_battery.ChartAreas["Draw"].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            //2.Series
            chart_battery.Series.Add("Temper");
            chart_battery.Series["Temper"].ChartType = SeriesChartType.Line;
            chart_battery.Series["Temper"].Color = Color.Blue;
            chart_battery.Series["Temper"].BorderWidth = 2; ;
            chart_battery.Series["Temper"].LegendText = "배터리온도(C)"; //레전드는 범례

            chart_battery.Series.Add("Capacity");
            chart_battery.Series["Capacity"].ChartType = SeriesChartType.Line;
            chart_battery.Series["Capacity"].Color = Color.Red;
            chart_battery.Series["Capacity"].BorderWidth = 2; ;
            chart_battery.Series["Capacity"].LegendText = "배터리용량(%)"; //레전드는 범례

            chart_battery.Series.Add("arm");
            chart_battery.Series["arm"].ChartType = SeriesChartType.Line;
            chart_battery.Series["arm"].Color = Color.Yellow;
            chart_battery.Series["arm"].BorderWidth = 2; ;
            chart_battery.Series["arm"].LegendText = "소모전료(A)"; //레전드는 범례


            //줌기능을 넣어보자!
            chart_battery.ChartAreas["Draw"].CursorX.IsUserSelectionEnabled = true;
            //스크롤 색변경
            chart_battery.ChartAreas["Draw"].AxisX.ScrollBar.ButtonColor = Color.Blue;
        }

        private void ViewBMSAll()
        {
            Random ra = new Random();

            //데이터는 ecg배열 ppg배열에 있어
            foreach (double x in robot_BMS_temper)
            {
                chart_battery.Series["Temper"].Points.Add(x);
            }

            foreach (double x in robot_BMS_capacity)
            {
                chart_battery.Series["Capacity"].Points.Add(x);
            }

            foreach (double x in robot_BMS_ampr)
            {
                chart_battery.Series["arm"].Points.Add(x);
            }
        }
        #endregion

        #region 배터리 로그파일 관련
        private void timer_BatteryLogSave_Tick(object sender, EventArgs e)
        {
            onBatteryLogSave();
        }

        private void onBatteryLogSave()
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    if (Data.Instance.Robot_work_info.ContainsKey(m_strRobotName))
                    {
                        if (Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg != null)
                        {
                            if (Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data.Count < 1) return;

                            float volt = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[0];
                            float amp = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[1];
                            float temper = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[2];
                            float capacity = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[3];
                            float heathy = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[4];
                            float chargetime = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[5];
                            float dischargetime = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[6];
                            float remaincapacity = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[7];
                            float remainenergy = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[8];
                            float err_overVolt = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[9];
                            float err_underVolt = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[10];
                            float err_chargeover = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[11];
                            float err_dischargeover = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[12];
                            float err_hightemper = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[13];
                            float err_lowtemper = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[14];
                            float err_bmu = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[15];

                            string strvolt = string.Format("{0:F2}", volt);
                            string stramp = string.Format("{0:F2}", amp);
                            string strtemper = string.Format("{0:F2}", temper);
                            string strcapacity = string.Format("{0:F}", capacity);
                            string strheathy = string.Format("{0:F}", heathy);
                            string strchargetime = string.Format("{0:F}", chargetime);
                            string strdischargetime = string.Format("{0:F}", dischargetime);
                            string strremaincapacity = string.Format("{0:F2}", remaincapacity);
                            string strremainenergy = string.Format("{0:F2}", remainenergy);
                            string strerr_overVolt = string.Format("{0}", err_overVolt);
                            string strerr_underVolt = string.Format("{0}", err_underVolt);
                            string strerr_chargeover = string.Format("{0}", err_chargeover);
                            string strerr_dischargeover = string.Format("{0}", err_dischargeover);
                            string strerr_hightemper = string.Format("{0}", err_hightemper);
                            string strerr_lowtemper = string.Format("{0}", err_lowtemper);
                            string strerr_bmu = string.Format("{0}", err_bmu);





                            string strLog_File = Application.StartupPath + "\\log_battery\\" + m_strRobotName + "_" + m_strLog_File;


                            if (!File.Exists(strLog_File))
                            {
                                using (StreamWriter sw = new System.IO.StreamWriter(strLog_File, false, Encoding.Default))
                                {
                                    sw.WriteLine("checktime,volt,amp,temper,capacity,healty,chargetime,dischargetime,remaincapacity,remainenergy,err_overvolt,err_undervolt,err_chargeover,err_dischargeover,err_hightemper,err_lowtemper,err_bmu");
                                    sw.Close();
                                }
                                //  return;
                            }
                            DateTime dt = DateTime.Now;

                            string strtime = string.Format("{0:d4}{1:d2}{2:d2}{3:d2}{4:d2}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute);//DateTime.Now.ToString("yyyyMMddHH");
                            using (StreamWriter sw = new System.IO.StreamWriter(strLog_File, true, Encoding.Default))
                            {
                                string strmsg = "";
                                strmsg = strtime + "," + strvolt + "," + stramp + "," + strtemper + "," + strcapacity + "," + strheathy + "," + strchargetime + "," + strdischargetime + "," + strremaincapacity + "," + strremainenergy
                                + "," + strerr_overVolt + "," + strerr_underVolt + "," + strerr_chargeover + "," + strerr_dischargeover + "," + strerr_hightemper + "," + strerr_lowtemper + "," + strerr_bmu;
                                //strmsg = strRobot + "," + strRobotName + "," + strMissionID + "," + strMissionName + "," + strResult + "," + ncnt.ToString();
                                sw.WriteLine(strmsg);
                                sw.Close();
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onBatteryLogSave err :={0}", ex.Message.ToString());
            }


        }
        #endregion


        #region LiDar 그래프 관련
        private void RadarChartSetting()
        {


            //  double[] yValues = { 65.62, 75.54, 60.45, 34.73, 85.42, 55.9, 63.6, 55.2, 77.1, 65.62, 75.54, 60.45, 34.73, 85.42, 55.9, 63.6, 55.2, 77.1, 65.62, 75.54, 60.45, 34.73, 85.42, 55.9, 63.6, 55.2, 77.1 };
            // string[] xValues = { "", "", "", "", "", "", "", "", "" , "", "", "", "", "", "", "", "", "" , "", "", "", "", "", "", "", "", "" };

            string[] xValues = new string[90];
            double[] yValues = new double[90];
            Random ra = new Random();
            for (int i = 0; i < 90; i++)
            {

                double dd = ra.Next(1, 10);
                yValues[i] = dd;

            }


            radarchart1.ChartAreas.Clear();
            radarchart1.Series.Clear();

            radarchart1.ChartAreas.Add("Default");

            radarchart1.ChartAreas["Default"].AxisX.Interval = 60;



            radarchart1.Series.Add("Default");
            //   foreach (double x in yValues)
            //   {
            //          if(x>0)
            //          radarchart1.Series["Default"].Points.Add(x);
            //   }

            //radarchart1.Series["Default"].Points.DataBindXY(xValues, yValues);

            // Set radar chart type
            radarchart1.Series["Default"].ChartType = SeriesChartType.Radar;

            // Set radar chart style (Area, Line or Marker)
            radarchart1.Series["Default"]["RadarDrawingStyle"] = "Area";
            radarchart1.Series["Default"].LegendText = "Ranges";

            // Set circular area drawing style (Circle or Polygon)
            //radarchart1.Series["Default"]["AreaDrawingStyle"] = "Polygon";

            // Set labels style (Auto, Horizontal, Circular or Radial)
            //radarchart1.Series["Default"]["CircularLabelsStyle"] = "Horizontal";

            // Show as 3D
            // radarchart1.ChartAreas["Default"].Area3DStyle.Enable3D = true;

            //radarchart1.ChartAreas["Default"].AxisY.Interval = 10;
            radarchart1.ChartAreas["Default"].AxisY.Enabled = AxisEnabled.False;
            //radarchart1.ChartAreas["Default"].AxisY.


        }
        float[] robot_lidar = new float[360];
        private void RadarViewAll()
        {
            Random ra = new Random();

            foreach (double x in robot_lidar)
            {
                if (x > 0)//&& x <9)
                    radarchart1.Series["Default"].Points.Add(x);
            }
        }
        #endregion


        public Image robot_image;
        public Image opacity_image;
        public bool m_bTransparent = true;

        public Bitmap RotateImage(Image image, PointF offset, float angle)
        {
            if (image == null)
                throw new ArgumentNullException("image");

            //create a new empty bitmap to hold rotated image
            Bitmap rotatedBmp = new Bitmap(image.Width, image.Height);
            rotatedBmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(rotatedBmp);

            //Put the rotation point in the center of the image
            g.TranslateTransform(offset.X, offset.Y);

            //rotate the image
            g.RotateTransform(angle);

            //move the image back
            g.TranslateTransform(-offset.X, -offset.Y);

            //draw passed in image onto graphics object
            g.DrawImage(image, new PointF(0, 0));

            return rotatedBmp;
        }
        private void Robot_pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (robot_image != null)
            {
                if (m_bTransparent)
                {
                    Bitmap bitmap = new Bitmap(opacity_image);
                    //bitmap.MakeTransparent(Color.FromArgb(Convert.ToInt16(textBox_R.Text), Convert.ToInt16(textBox_G.Text), Convert.ToInt16(textBox_B.Text)));
                    Random ra = new Random();
                    int r = ra.Next(0, 255);
                    int g = ra.Next(0, 255);
                    int b = ra.Next(0, 255);


                    //bitmap.MakeTransparent(Color.FromArgb(r,g,b));
                    opacity_image = (Image)bitmap;
                }
                e.Graphics.DrawImage(opacity_image, new Point(0, 0));
            }
        }

    }
}
