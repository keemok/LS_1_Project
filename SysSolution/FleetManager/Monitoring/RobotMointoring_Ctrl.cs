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
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Imaging;
using DevExpress.XtraCharts;
using DevExpress.Utils;

namespace SysSolution.FleetManager.Monitoring
{
    public partial class RobotMointoring_Ctrl : UserControl
    {
        public RobotMointoring_Ctrl()
        {
            InitializeComponent();
        }

        FleetManager_MainForm mainform;
        MonitoringMain_Ctrl monitoringMain;

        string m_strRobotID = "";

        public bool m_bMonitoring = false;

        public RobotMointoring_Ctrl(FleetManager.FleetManager_MainForm mainform, MonitoringMain_Ctrl monitormain)
        {
            this.mainform = mainform;
            monitoringMain = monitormain;
            InitializeComponent();
        }

        public void onInitSet()
        {
            int idx = cboRobotID.SelectedIndex;

            cboRobotID.Items.Clear();

            int cnt = mainform.Robot_RegInfo_list.Count();
         
            for (int i = 0; i < cnt; i++)
            {
                string strtmp = "";
                Robot_RegInfo robotinfo = mainform.Robot_RegInfo_list.ElementAt(i).Value;
                strtmp = string.Format("{0}({1})", robotinfo.robot_name, robotinfo.robot_id);

                cboRobotID.Items.Add(strtmp);
            }

            cboRobotID.SelectedIndex = idx;
            progressBar_capacity.Value = 0;
        }
   

        private void RobotMointoring_Ctrl_Load(object sender, EventArgs e)
        {
            progressBar_capacity.Value = 0;
            onMonitoringEventOn();

            ChartSettiong_positiondata1();
            ChartSettiong_angluardata_result();

            ChartSettiong_positiondata_realtime();
            ChartSettiong_positiondata_result();
        }

        private void onMonitoringEventOn()
        {
           // mainform.worker.robotpositionstate_Evt += new Worker.RobotPostionResponse(this.onRobotPositionComplete);
         //   mainform.worker.robotmotorstate_Evt += new Worker.RobotMotorStateResponse(this.onRobotMotorStateComplete);
       //     mainform.worker.cam1data_Evt += new Worker.Cam1DataResponse(this.onCam1Complete);
       //     mainform.worker.cam2data_Evt += new Worker.Cam2DataResponse(this.onCam2Complete);
            mainform.worker.Globalpath_Evt += new Worker.GlobalpathComplete(this.GlobalpathComplete);
            mainform.worker.workfeedback_Evt += new Worker.WorkFeedbackResponse(this.WorkFeedbackResponse);
        }
        private void onMonitoringEventOff()
        {
          //  mainform.worker.robotpositionstate_Evt -= new Worker.RobotPostionResponse(this.onRobotPositionComplete);
         //   mainform.worker.robotmotorstate_Evt -= new Worker.RobotMotorStateResponse(this.onRobotMotorStateComplete);
         //   mainform.worker.cam1data_Evt -= new Worker.Cam1DataResponse(this.onCam1Complete);
          //  mainform.worker.cam2data_Evt -= new Worker.Cam2DataResponse(this.onCam2Complete);
            mainform.worker.Globalpath_Evt -= new Worker.GlobalpathComplete(this.GlobalpathComplete);
            mainform.worker.workfeedback_Evt -= new Worker.WorkFeedbackResponse(this.WorkFeedbackResponse);
        }
        private void cboRobotID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboRobotID.SelectedIndex < 0)
                {
                }
                else
                {
                    //if (Data.Instance.isConnected)
                    {
                        string robotid = cboRobotID.SelectedItem.ToString();

                        int idx = robotid.IndexOf("(");
                        int idx2 = robotid.IndexOf(")");

                        m_strRobotID = robotid.Substring(idx + 1, idx2 - idx - 1);

                        onAll_SusbcribeDataClear();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.Out.WriteLine("cboRobotID_SelectedIndexChanged err :={0}", ex.Message.ToString());
            }

        }

        private void onAll_SusbcribeDataClear()
        {
            DP_Timer.Enabled = false;

            robot_linear_speed = new double[1000];
            robot_anglua_speed = new double[1000];
            nlinearspeed_idx = 0;
            nangularspeed_idx = 0;

            robot_dist = new double[10000];
            nrobotdist_idx = 0;

            robot_angle = new double[10000];
            nrobotangle_idx = 0;

            robot_BMS_ampr = new double[1000];
            robot_BMS_temper = new double[1000];
            robot_BMS_capacity = new double[1000];
            nbms_amp_idx = 0;
            nbms_temper_idx = 0;
            nbms_capacity_idx = 0;

            progressBar_capacity.Value = 0;

            Thread.Sleep(500);
            DP_Timer.Interval = 300;
            DP_Timer.Enabled = true;
        }

     

        private void onRobotMotorStateComplete(string robotid)
        {
            try
            {
                if (!m_bMonitoring) return;

                if (Data.Instance.isConnected)
                {
                    if (robotid == "")
                    {
                    }
                    else if(robotid==m_strRobotID)
                    {
                        if (Data.Instance.Robot_work_info.ContainsKey(robotid))
                        {
                            #region 선속도, 각속도 데이타배열 저장
                            if (Data.Instance.Robot_work_info[robotid].robot_status_info.motorstate != null)
                            {
                                if (Data.Instance.Robot_work_info[robotid].robot_status_info.motorstate.msg != null)
                                {
                                    //선속도
                                    double motor_Robotspeed_returnlinear_x = Data.Instance.Robot_work_info[robotid].robot_status_info.motorstate.msg.feed_vel.linear.x;

                                    //각속도
                                    double motor_Robotspeed_return_angular_z = Data.Instance.Robot_work_info[robotid].robot_status_info.motorstate.msg.feed_vel.angular.z;

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

                                    if (nangularspeed_idx < 120)
                                    {
                                        //각속도
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

                                    Invoke(new MethodInvoker(delegate ()
                                    {
                                        lblCurrRobotSpeed.Text = string.Format("{0:f2}m/sec", motor_Robotspeed_returnlinear_x);
                                        onRobotSpeed_DP();
                                    }));
                                }
                            }
                            #endregion
                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobotMotorStateComplete err :={0}", ex.Message.ToString());
            }
        }

        byte[] sourceMapValues;
        byte[] sourceMapValues2;
        int nSourceMapWidth = 0;
        int nSourceMapHeight = 0;

        private void onCam1Complete(string robotid)
        {
            if (!m_bMonitoring) return;
            
            if (Data.Instance.isConnected)
            {
                if (robotid == "")
                {
                }
                else if (robotid == m_strRobotID)
                {
                    if (Data.Instance.Robot_work_info[robotid].robot_status_info.cam1 == null) return;

                    CamInformation cam = Data.Instance.Robot_work_info[robotid].robot_status_info.cam1;

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
                        Invoke(new MethodInvoker(delegate ()
                        {
                            pictureBox2.Image = bmpTemp;
                            pictureBox2.Image.RotateFlip(RotateFlipType.Rotate180FlipX);
                            pictureBox2.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        }));

                        //Thread.Sleep(50);

                        bmSource2.Dispose();
                        bmSource2 = null;
                        bmSource2 = (Bitmap)(bmpTemp.Clone());
                    }
                    catch (Exception ex)
                    {
                        Console.Out.WriteLine("onCam1Complete err :={0}", ex.Message.ToString());
                    }
                }
            }
            
        }

        private void onCam2Complete(string robotid)
        {
            if (!m_bMonitoring) return;

            if (Data.Instance.isConnected)
            {
                if (robotid == "")
                {
                }
                else if (robotid == m_strRobotID)
                {
                    if (Data.Instance.Robot_work_info[robotid].robot_status_info.cam2 == null) return;

                    CamInformation cam = Data.Instance.Robot_work_info[robotid].robot_status_info.cam2;

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
                        Invoke(new MethodInvoker(delegate ()
                        {
                            pictureBox1.Image = bmpTemp;
                        }));

                        //Thread.Sleep(50);

                        bmSource2.Dispose();
                        bmSource2 = null;
                        bmSource2 = (Bitmap)(bmpTemp.Clone());
                    }
                    catch (Exception ex)
                    {
                        Console.Out.WriteLine("onCam2Complete err :={0}", ex.Message.ToString());
                    }
                }
            }
        }

        double[] robot_dist = new double[10000];
        int nrobotdist_idx = 0;

        double[] robot_angle = new double[10000];
        int nrobotangle_idx = 0;

        double[] robot_globalpath_x = new double[10000];
        double[] robot_globalpath_y = new double[10000];
        int nrobotgolbalpath_count = 0;

        bool bglobalplan = false;

        Thread cam_thread;

        private void DP_Timer_Tick(object sender, EventArgs e)
        {
            if (!m_bMonitoring) return;

            onRobotMotorStateComplete(m_strRobotID);

            onDp_Update();

          //  onCam1Complete(m_strRobotID);
          //  onCam2Complete(m_strRobotID);

        }

        private void cam_thread_func()
        {
            try
            {
                for (; ; )
                {
                    if (!m_bMonitoring) break ;

                    onCam1Complete(m_strRobotID);

                    onCam2Complete(m_strRobotID);

                    Thread.Sleep(100);
                }
            }
            catch(Exception ex)
            {
                Console.Out.WriteLine("cam_thread_func err :={0}", ex.Message.ToString());
            }
        }

        int postion_chart_idx = 0;
        int position_realtimechart_idx = 0;

        private void onDp_Update()
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    if (m_strRobotID == "")
                    {
                    }
                    else
                    {
                        if (Data.Instance.Robot_work_info.ContainsKey(m_strRobotID))
                        {
                            #region 로봇 위치, 앵글 오차 체크
                            if (Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.robotstate != null)
                            {
                                if (Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.robotstate.msg != null)
                                {
                                    float robotx = (float)Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.robotstate.msg.pose.x;
                                    float roboty = (float)Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.robotstate.msg.pose.y;
                                    float robottheta = (float)Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.robotstate.msg.pose.theta;

                                    if (bglobalplan)
                                    {
                                        double distmin = 0;

                                        distmin = onPointToPointDist(robotx, roboty, robot_globalpath_x[0], robot_globalpath_y[0]);
                                        bool bminfind = false;
                                        for (int i = 1; i < nrobotgolbalpath_count; i++)
                                        {
                                            double disttmp = 0;

                                            disttmp = onPointToPointDist(robotx, roboty, robot_globalpath_x[i], robot_globalpath_y[i]);

                                            if (distmin > disttmp)
                                            {
                                                distmin = disttmp;
                                                bminfind = true;
                                            }
                                            else
                                            {
                                                if (bminfind)
                                                {
                                                    bminfind = false;
                                                  //  break;
                                                }
                                            }
                                        }

                                        robot_dist[nrobotdist_idx] = distmin;
                                        nrobotdist_idx++;

                                   

                                        double angle_cal = 0;
                                 
                                        if (Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.currAngluar.msg!= null)
                                        {
                                            angle_cal = (float)Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.currAngluar.msg.data;
                                            robot_angle[nrobotangle_idx] = angle_cal;
                                            nrobotangle_idx++;

                                          
                                        }

                                        if (position_series[0].Points.Count > 50) // x축은 10개까지만 값을 출력하게 한다.           
                                        {
                                            position_series[0].Points.RemoveAt(0);
                                          //  position_series[1].Points.RemoveAt(0);
                                        }

                                     //  position_series[0].Points.Add(new SeriesPoint(postion_chart_idx, distmin));
                                        position_series[0].Points.Add(new SeriesPoint(postion_chart_idx++, angle_cal));


                                        if (position_realtime_series.Points.Count > 50) // x축은 10개까지만 값을 출력하게 한다.           
                                        {
                                            position_realtime_series.Points.RemoveAt(0);
                                        }
                                        position_realtime_series.Points.Add(new SeriesPoint(position_realtimechart_idx++, distmin * 100));

                                        Invoke(new MethodInvoker(delegate ()
                                        {
                                            lblCurrRobotPosGab.Text = string.Format("{0:f3}(cm)", distmin * 100);

                                            float angluarDegree = mainform.worker.RadianToDegree(string.Format("{0:f2}", angle_cal));
                                            lblCurrRobotAngluarGab.Text= string.Format("{0:f1}도({1:f2})", angluarDegree, angle_cal);

                                            onRobotPos_DP();
                                        }));

                                        //  robot_dist[nrobotdist_idx] = distmin;
                                        //  nrobotdist_idx++;
                                    }
                                }
                            }
                            #endregion

                            #region bms 데이타배열 저장
                            if (Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.bmsinfo != null)
                            {
                                if (Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.bmsinfo.msg != null)
                                {
                                    if (Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.bmsinfo.msg.data.Count < 1) return;

                                    double bms_return_amp = Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.bmsinfo.msg.data[1];
                                    double bms_return_temper = Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.bmsinfo.msg.data[2];
                                    double bms_return_capacity = Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.bmsinfo.msg.data[3];

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

                                    onBMS_DP();

                                }
                            }
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onDp_Update err :={0}", ex.Message.ToString());
            }
        }

        private double onPointToPointDist(double x1, double y1, double x2, double y2)
        {
            double hypo = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            return hypo;
        }

        private float onPointToPointAngle(float nangluar_x1, float nangluar_y1, float nangluar_x2, float nangluar_y2)
        {
            float TargetAngle = (float)(Math.Atan2(nangluar_y2 - nangluar_y1, nangluar_x2 - nangluar_x1) * 180f / Math.PI);
            TargetAngle = mainform.worker.DegreeToRadian(string.Format("{0:f2}", TargetAngle));
            TargetAngle *= -1;

            return TargetAngle;
        }

        private void GlobalpathComplete(string robotid)
        {
            try
            {
                if (robotid == "")
                {
                }
                else if (robotid == m_strRobotID)
                { 
                    if (Data.Instance.Robot_work_info[robotid].robot_status_info.globalplan.msg.poses.Count > 0)
                    {
                        List<PoseStamped> pose = new List<PoseStamped>();
                        pose = Data.Instance.Robot_work_info[robotid].robot_status_info.globalplan.msg.poses;

                        robot_dist = new double[10000];
                        nrobotdist_idx = 0;

                        robot_angle = new double[10000];
                        nrobotangle_idx = 0;

                        position_result_series.Points.Clear();
                        angluar_result_series.Points.Clear();

                        int ncnt = pose.Count;

                        if (ncnt < 15) return;

                        bglobalplan = true;

                        nrobotgolbalpath_count = pose.Count;

                        robot_globalpath_x = new double[ncnt];
                        robot_globalpath_y = new double[ncnt];

                      //  double x = pose[pose.Count - 1].pose.position.x;
                      //  double y = pose[pose.Count - 1].pose.position.y;

                        for (int i = 0; i < ncnt; i++)
                        {
                            robot_globalpath_x[i] = pose[i].pose.position.x;
                            robot_globalpath_y[i] = pose[i].pose.position.y;
                        }
                        // float checkdist =(float)onPointToPointDist(robot_globalpath_x[0], robot_globalpath_y[0], robot_globalpath_x[ncnt - 1], robot_globalpath_y[ncnt - 1]);
                        Invoke(new MethodInvoker(delegate ()
                        {
                            //     txtCheckDist.Text = string.Format("{0:f5}", checkdist);
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("GlobalpathComplete err :={0}", ex.Message.ToString());
            }
        }

        public void WorkFeedbackResponse(string strrobotid)
        {
            try
            {
                int nLoopcount = Data.Instance.Robot_work_info[strrobotid].robot_status_info.workfeedback.msg.feedback.loop_count;
                int nactionidx = Data.Instance.Robot_work_info[strrobotid].robot_status_info.workfeedback.msg.feedback.action_indx;

                Invoke(new MethodInvoker(delegate ()
                {
                    //lblActionidx.Text = string.Format("{0}", nactionidx);
                }));

                if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.workfeedback.msg.feedback.act_complete)
                {
                    // onRobotPos_DP();

                    if (nrobotdist_idx < 1) return;

                    position_result_series.Points.Clear();
                    SeriesPoint[] points = new SeriesPoint[nrobotdist_idx];
                    for(int i=0; i< nrobotdist_idx; i++)
                    {
                        points[i] = new SeriesPoint(i, robot_dist[i]*100);
                    }
                    position_result_series.Points.AddRange(points);

                    if (nrobotangle_idx < 1) return;

                    angluar_result_series.Points.Clear();
                    SeriesPoint[] points2 = new SeriesPoint[nrobotangle_idx];
                    for (int i = 0; i < nrobotangle_idx; i++)
                    {
                        points2[i] = new SeriesPoint(i, robot_angle[i] * 100);
                    }
                    angluar_result_series.Points.AddRange(points2);

                    bglobalplan = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("WorkFeedbackResponse err=" + ex.Message.ToString());
            }
        }

      
        private void onRobotPos_DP()
        {
        }

        DevExpress.XtraCharts.Series[] position_series = new DevExpress.XtraCharts.Series[2];
        DevExpress.XtraCharts.Series angluar_result_series = new DevExpress.XtraCharts.Series();


        DevExpress.XtraCharts.Series position_result_series = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.Series position_realtime_series = new DevExpress.XtraCharts.Series();


        private void ChartSettiong_positiondata1()
        {
          //  position_series[0] = new DevExpress.XtraCharts.Series("위치", ViewType.Line);
            position_series[0] = new DevExpress.XtraCharts.Series("앵글", ViewType.Line);

         //   position_series[0].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;  //시리즈에 라벨 표시
         //   position_series[1].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;


            //ChartControl에 Series 추가            
            chartControl1.Series.Add(position_series[0]);
           // chartControl1.Series.Add(position_series[1]);

            chartControl1.CrosshairEnabled = DefaultBoolean.False;
            XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
            diagram.AxisY.WholeRange.MaxValue = 3.14;    // y축 최대값            
            diagram.AxisY.WholeRange.MinValue = -3.14;   // y축 최소값             
            diagram.AxisY.WholeRange.Auto = false;      // y축 범위 자동변경 설정             
            diagram.AxisX.WholeRange.SideMarginsValue = 0;

            ConstantLine zeroLine = new ConstantLine();
            zeroLine.Color = Color.Green;
            zeroLine.AxisValue = 0;
            zeroLine.ShowInLegend = false;
            diagram.AxisY.ConstantLines.Add(zeroLine);  // y값 0인 x축 생성     
        }

        private void ChartSettiong_angluardata_result()
        {
            //  position_series[0] = new DevExpress.XtraCharts.Series("위치", ViewType.Line);
            angluar_result_series = new DevExpress.XtraCharts.Series("앵글", ViewType.Line);

            //   position_series[0].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;  //시리즈에 라벨 표시
            //   position_series[1].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;


            //ChartControl에 Series 추가            
            chartControl2.Series.Add(angluar_result_series);
            // chartControl1.Series.Add(position_series[1]);

            chartControl2.CrosshairEnabled = DefaultBoolean.False;
            XYDiagram diagram = (XYDiagram)chartControl2.Diagram;
            diagram.AxisY.WholeRange.MaxValue = 3.14;    // y축 최대값            
            diagram.AxisY.WholeRange.MinValue = -3.14;   // y축 최소값             
            diagram.AxisY.WholeRange.Auto = false;      // y축 범위 자동변경 설정             
            diagram.AxisX.WholeRange.SideMarginsValue = 0;

            ConstantLine zeroLine = new ConstantLine();
            zeroLine.Color = Color.LightYellow;
            zeroLine.AxisValue = 0;
            zeroLine.ShowInLegend = false;
            diagram.AxisY.ConstantLines.Add(zeroLine);  // y값 0인 x축 생성     
        }
        
        private void ChartSettiong_positiondata_result()
        {
            position_result_series = new DevExpress.XtraCharts.Series("위치", ViewType.Line);

            //position_result_series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;  //시리즈에 라벨 표시
            //   position_series[1].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;


            //ChartControl에 Series 추가            
            chartControl_result_path.Series.Add(position_result_series);

            chartControl_result_path.CrosshairEnabled = DefaultBoolean.False;
            XYDiagram diagram = (XYDiagram)chartControl_result_path.Diagram;
            diagram.AxisY.WholeRange.MaxValue = 10;    // y축 최대값            
            diagram.AxisY.WholeRange.MinValue =-1;   // y축 최소값             
            diagram.AxisY.WholeRange.Auto = false;      // y축 범위 자동변경 설정             
            diagram.AxisX.WholeRange.SideMarginsValue = 0;

            ConstantLine zeroLine = new ConstantLine();
            zeroLine.Color = Color.Green;
            zeroLine.AxisValue = 0;
            zeroLine.ShowInLegend = false;
            diagram.AxisY.ConstantLines.Add(zeroLine);  // y값 0인 x축 생성     
        }

        private void ChartSettiong_positiondata_realtime()
        {
            position_realtime_series = new DevExpress.XtraCharts.Series("위치", ViewType.Line);

            //   position_series[0].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;  //시리즈에 라벨 표시
            //   position_series[1].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;


            //ChartControl에 Series 추가            
            chartControl_path.Series.Add(position_realtime_series);

            chartControl_path.CrosshairEnabled = DefaultBoolean.False;
            XYDiagram diagram = (XYDiagram)chartControl_path.Diagram;
            diagram.AxisY.WholeRange.MaxValue = 10;    // y축 최대값            
            diagram.AxisY.WholeRange.MinValue = -1;   // y축 최소값             
            diagram.AxisY.WholeRange.Auto = false;      // y축 범위 자동변경 설정             
            diagram.AxisX.WholeRange.SideMarginsValue = 0;

            ConstantLine zeroLine = new ConstantLine();
            zeroLine.Color = Color.Green;
            zeroLine.AxisValue = 0;
            zeroLine.ShowInLegend = false;
            diagram.AxisY.ConstantLines.Add(zeroLine);  // y값 0인 x축 생성     
        }

        #region 로봇 선속도, 각속도 그래프 관련
        double[] robot_linear_speed = new double[1000];
        double[] robot_anglua_speed = new double[1000];
        int nlinearspeed_idx = 0;
        int nangularspeed_idx = 0;

        private void onRobotSpeed_DP()
        {
            ChartSetting();
            ViewAll();
        }

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
            robotspeed_chart.ChartAreas["Draw"].AxisY.Maximum = 2.0;
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


        #region 배터리 그래프 관련

        private void onBMS_DP()
        {
            try
            {
                if (Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.bmsinfo.msg != null)
                {
                    if (Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.bmsinfo.msg.data.Count < 1)
                    {
                        return;
                    }

                    float volt = Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.bmsinfo.msg.data[0];
                    float amp = Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.bmsinfo.msg.data[1];
                    float temper = Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.bmsinfo.msg.data[2];
                    float capacity = Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.bmsinfo.msg.data[3];
                    float heathy = Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.bmsinfo.msg.data[4];
                    float chargetime = Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.bmsinfo.msg.data[5];
                    float dischargetime = Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.bmsinfo.msg.data[6];
                    float remaincapacity = Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.bmsinfo.msg.data[7];
                    float remainenergy = Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.bmsinfo.msg.data[8];
                    float err_overVolt = Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.bmsinfo.msg.data[9];
                    float err_underVolt = Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.bmsinfo.msg.data[10];
                    float err_chargeover = Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.bmsinfo.msg.data[11];
                    float err_dischargeover = Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.bmsinfo.msg.data[12];
                    float err_hightemper = Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.bmsinfo.msg.data[13];
                    float err_lowtemper = Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.bmsinfo.msg.data[14];
                    float err_bmu = Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.bmsinfo.msg.data[15];

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

        #region test
        private List<int> _valueList = new List<int>();
        private Random _ran = new Random();

        private void onGraphtest()
        {
            chart1.ChartAreas[0].AxisX.IsStartedFromZero = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
            chart1.Series[0].XValueType = ChartValueType.Time;
            chart1.ChartAreas[0].AxisX.ScaleView.SizeType = DateTimeIntervalType.Seconds;
            chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;
            chart1.ChartAreas[0].AxisX.Interval = 1;

            chart1.ChartAreas[0].AxisY.Minimum = -10;
            chart1.ChartAreas[0].AxisY.Maximum = 110;
            chart1.ChartAreas[0].AxisY.Interval = 20;


            DateTime now = DateTime.Now;
            chart1.ChartAreas[0].AxisX.Minimum = now.ToOADate();
            chart1.ChartAreas[0].AxisX.Maximum = now.AddSeconds(10).ToOADate();

            chart1.Series[0].ChartType = SeriesChartType.Line;
            timer1.Interval = 10;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // AddData();
        }

        private void AddData()
        {
            DateTime now = DateTime.Now;
            //Insert a number into the list.
            _valueList.Add(_ran.Next(0, 100));


            chart1.ResetAutoValues();

            //Remove old datas from the chart.
            if (chart1.Series[0].Points.Count > 0)
            {
                while (chart1.Series[0].Points[0].XValue < now.AddSeconds(-5).ToOADate())
                {
                    chart1.Series[0].Points.RemoveAt(0);

                    chart1.ChartAreas[0].AxisX.Minimum = chart1.Series[0].Points[0].XValue;
                    chart1.ChartAreas[0].AxisX.Maximum = now.AddSeconds(10).ToOADate();
                }
            }

            //Insert a data into the chart.

            chart1.Series[0].Points.AddXY(now.ToOADate(), _valueList[_valueList.Count - 1]);

            chart1.Invalidate();
        }


        #endregion

        private void toggleSwitch1_Toggled(object sender, EventArgs e)
        {
            if (toggleSwitch_monitoring.IsOn)
            {
                if(cam_thread!=null)
                {
                    cam_thread.Abort();
                    cam_thread = null;
                }

                cam_thread = new Thread(cam_thread_func);
                cam_thread.Start();
                m_bMonitoring = true;
            }
            else
            {
                if (cam_thread != null)
                {
                    cam_thread.Abort();
                    cam_thread = null;
                }
                m_bMonitoring = false;
            }
        }
    }
}
