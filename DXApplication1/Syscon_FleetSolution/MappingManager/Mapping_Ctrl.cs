using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
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

using MySql.Data.MySqlClient;

using System.Drawing.Imaging;using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace Syscon_Solution.MappingManager
{
    public partial class Mapping_Ctrl : UserControl
    {
        public Mapping_Ctrl()
        {
            InitializeComponent();
        }
        LSprogram.mainForm mainform;

        string m_strRobotID="";
        string m_strPrevRobotID = "";
        string m_strCurrMode = "NAV";

        public Mapping_Ctrl(LSprogram.mainForm fmain)
        {
            mainform = fmain;
            InitializeComponent();
        }

       public bool bBtnOff = true;
       public Thread manualRun_thread;
        public bool bThreadBreak = false;
        private void Mapping_Ctrl_Load(object sender, EventArgs e)
        {
            //mainform.commBridge.mapinfo_Evt += new  A.Comm.Comm_bridge.MapInfoComplete(mainform.MapInfoComplete);
          //  mainform.commBridge.Localcostmapinfo_Evt += new FleetManager.Comm.Comm_bridge.LocalCostInfoComplete(mainform.LocalCostInfoComplete);

            if (manualRun_thread != null)
            {
                manualRun_thread.Abort();
                manualRun_thread = null;
            }

            manualRun_thread = new Thread(manualRun_thread_func);
            manualRun_thread.Start();

           // bcostmaploading = true;
        }

        private void onBtnModeChg(string strmode)
        {
            btnEcoMode2.Checked = false;
            btnDriveMode2.Checked = false;
            btnMappingMode2.Checked = false;
            groupBox_mapping.Enabled = false;
            
            if(strmode=="ECO")
            {
                btnEcoMode2.Checked = true;
            }
            else if (strmode == "NAV")
            {
                btnDriveMode2.Checked = true;
            }
            if (strmode == "SLAM")
            {
                groupBox_mapping.Enabled = true;
                btnMappingMode2.Checked = true;
            }
        }

        private void cboRobotID_SelectedIndexChanged(object sender, EventArgs e)
        {
         
            if (cboRobotID.SelectedIndex >-1)
            {
                m_strRobotID = cboRobotID.SelectedItem.ToString();

                //로봇의 현재 모드 확인하기..
                m_strCurrMode = "NAV";// 디폴트 모드 NAV 로.. 
                onBtnModeChg(m_strCurrMode);
                if (m_strPrevRobotID!=m_strRobotID)
                {
                    
                }
            }
        }

        private void btnEcoMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    if (m_strRobotID != "")
                    {
                        mainform.commBridge.onSP_routine_publish(m_strRobotID, "ECO");
                        onModeDp(string.Format("RobotID={0}, mode =ECO", m_strRobotID));
                        m_strCurrMode = "ECO";
                        onBtnModeChg(m_strCurrMode);

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("btnEcoMode_Click err=" + ex.Message.ToString());
            }
        }

        private void btnDriveMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    if (m_strRobotID != "")
                    {
                        
                        

                        mainform.commBridge.onSP_routine_publish(m_strRobotID, "NAV");
                        onModeDp(string.Format("RobotID={0}, mode =NAV", m_strRobotID));
                        m_strCurrMode = "NAV";
                        onBtnModeChg(m_strCurrMode);


                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("btnDriveMode_Click err=" + ex.Message.ToString());
            }
        }

        private void btnMappingMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    if (m_strRobotID != "")
                    {
                        mainform.commBridge.onSP_routine_publish(m_strRobotID, "SLAM");
                        onModeDp(string.Format("RobotID={0}, mode =SLAM", m_strRobotID));
                        m_strCurrMode = "SLAM";
                        onBtnModeChg(m_strCurrMode);

                        TopicList list = new TopicList();
                        mainform.commBridge.onDeleteSelectSubscribe(m_strRobotID + list.topic_staticMap);

                        onSelectRobotMap_monitor(m_strRobotID);
                        //onSelectRobotLocalCostMap_monitor(m_strRobotID);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("btnMappingMode_Click err=" + ex.Message.ToString());
            }
        }

       



        private void btnMapSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    if(m_strCurrMode=="SLAM")
                    {
                        MapSaveDlg mapsave = new MapSaveDlg();
                        mapsave.onInitSet(m_strRobotID);
                        if (mapsave.ShowDialog() == DialogResult.OK)
                        {

                            string strdata = "";
                            strdata = string.Format("{0},{1}", mapsave.strRobotID, mapsave.strMapID);
                            //strdata = "R_003,MAPTEST_JO_1";
                            mainform.commBridge.onMap_Save_publish(strdata);

                            MessageBox.Show("map save ok");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("btnMapSave_Click err=" + ex.Message.ToString());
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    if (m_strRobotID != "")
                        mainform.commBridge.onSP_routine_publish(m_strRobotID, "RESET");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("btnReset_Click err=" + ex.Message.ToString());
            }
        }

        private void onModeDp(string strmode)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                lblRobotMode.Text = strmode;
            }));
        }
       
        private void btnRobotUp_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                   
                    string strspeed = txtSpeed.Text.ToString();
                    if (strspeed != "")
                    {
                        

                            Twist data = new Twist();
                            data.linear.x = double.Parse(strspeed);
                            data.angular.z = 0;
                            mainform.commBridge.onManualRun(data, m_strRobotID);

                           
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("btnRobotUp_MouseDown err=" + ex.Message.ToString());
            }
        }

        private void pictureBox_up_left_diagonal_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    string strspeed = txtSpeed.Text.ToString();
                    string strAngular = txtAngularSpeed.Text.ToString();
                    if (strspeed != "")
                    {
                        m_dManualspeed = double.Parse(strspeed);
                        m_dManualAngluar = double.Parse(strAngular);// -0.5;
                        bBtnOff = false;

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("pictureBox_up_left_diagonal_MouseDown err=" + ex.Message.ToString());
            }
        }

        private void manualRun_thread_func()
        {
            for(; ; )
            {
                if (bThreadBreak) break;

                if (bBtnOff) { }
                else
                {
                    Twist data = new Twist();
                    data.linear.x = m_dManualspeed;
                    data.angular.z = m_dManualAngluar;
                    mainform.commBridge.onManualRun(data, m_strRobotID);
                }
                Thread.Sleep(50);
            }
        }
        double m_dManualspeed = 0;
        double m_dManualAngluar = 0;

        private void pictureBox_up_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    Console.Out.WriteLine("1");
                   

                    string strspeed = txtSpeed.Text.ToString();
                    string strAngular = txtAngularSpeed.Text.ToString();
                    if (strspeed != "")
                    {
                        m_dManualspeed = double.Parse(strspeed);
                        m_dManualAngluar = 0;
                        bBtnOff = false;
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("pictureBox_up_left_diagonal_MouseDown err=" + ex.Message.ToString());
            }
            
        }

        private void onMouseDown()
        {

        }

        private void pictureBox_up_right_diagonal_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    string strspeed = txtSpeed.Text.ToString();
                    string strAngular = txtAngularSpeed.Text.ToString();
                    if (strspeed != "")
                    {
                        m_dManualspeed = double.Parse(strspeed);
                        m_dManualAngluar = double.Parse(strAngular) * -1;// -0.5;
                        bBtnOff = false;

                       
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("pictureBox_up_left_diagonal_MouseDown err=" + ex.Message.ToString());
            }
           
        }

        private void pictureBox_left_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    string strspeed = txtSpeed.Text.ToString();
                    string strAngular = txtAngularSpeed.Text.ToString();
                    if (strspeed != "")
                    {
                        m_dManualspeed = 0;
                        m_dManualAngluar = double.Parse(strAngular) ;// -.5;
                        bBtnOff = false;

                       
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("pictureBox_up_left_diagonal_MouseDown err=" + ex.Message.ToString());
            }
            
        }

        private void pictureBox_right_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    string strspeed = txtSpeed.Text.ToString();
                    string strAngular = txtAngularSpeed.Text.ToString();
                    if (strspeed != "")
                    {
                        m_dManualspeed = 0;
                        m_dManualAngluar = double.Parse(strAngular) * -1;// -0.5;
                        bBtnOff = false;

                      
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("pictureBox_up_left_diagonal_MouseDown err=" + ex.Message.ToString());
            }
            
        }

        private void pictureBox_down_left_diagonal_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    string strspeed = txtSpeed.Text.ToString();
                    string strAngular = txtAngularSpeed.Text.ToString();
                    if (strspeed != "")
                    {
                        m_dManualspeed = double.Parse(strspeed) * -1;// - 0.3;
                        m_dManualAngluar = double.Parse(strAngular) * -1;// -0.5;
                        bBtnOff = false;

                      
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("pictureBox_up_left_diagonal_MouseDown err=" + ex.Message.ToString());
            }
           
        }

        private void pictureBox_down_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    string strspeed = txtSpeed.Text.ToString();
                    string strAngular = txtAngularSpeed.Text.ToString();
                    if (strspeed != "")
                    {
                        m_dManualspeed = double.Parse(strspeed) * -1;// - 0.3;
                        m_dManualAngluar = 0;
                        bBtnOff = false;
                     
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("pictureBox_up_left_diagonal_MouseDown err=" + ex.Message.ToString());
            }
            
        }

        private void pictureBox_down_right_diagonal_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    string strspeed = txtSpeed.Text.ToString();
                    string strAngular = txtAngularSpeed.Text.ToString();
                    if (strspeed != "")
                    {
                        m_dManualspeed = double.Parse(strspeed) * -1;// - 0.3;
                        m_dManualAngluar = double.Parse(strAngular);// 0.5;
                        bBtnOff = false;


                       
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("pictureBox_up_left_diagonal_MouseDown err=" + ex.Message.ToString());
            }
           
        }

        private void onManualStop(object sender, MouseEventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    bBtnOff = true;
                    string strspeed = txtSpeed.Text.ToString();
                    if (strspeed != "")
                    {
                        m_dManualspeed =0;
                        m_dManualAngluar = 0;
                        bBtnOff = true;

                        //Twist data = new Twist();
                        //data.linear.x = 0;
                        //data.angular.z = 0;
                        //mainform.commBridge.onManualRun(data, m_strRobotID);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("pictureBox_up_left_diagonal_MouseDown err=" + ex.Message.ToString());
            }
           
        }

        #region map 관련

      //  string m_strRobotName = "R_006";

        float resoultion1 = (float)0.025;
        float ori_x = -10;
        float ori_y = (float)-10;
        byte[] sourceMapValues;
        int nSourceMapWidth = 0;
        int nSourceMapHeight = 0;

        float resoultion_localcost = (float)0.1;
        float ori_x_localcost = (float)0.8;//(float)2.1;
        float ori_y_localcost = (float)-0.3;//(float)-0.8;
        byte[] sourceMaplocalcostValues;
        int nSourceMap_localcostWidth = 0;
        int nSourceMap_localcostHeight = 0;
        float ratio_localcost = 4;
        Image imgcostmap;

        float resoultion_globalcost = (float)0.1;
        float ori_x_globalcost = (float)0.8;//(float)2.1;
        float ori_y_globalcost = (float)-0.3;//(float)-0.8;
        byte[] sourceMapglobalcostValues;
        int nSourceMap_globalcostWidth = 0;
        int nSourceMap_globalcostHeight = 0;
        float ratio_globalcost = 1;
        Image imgglobalcostmap;

        float dOrignX = 0;
        float dOrignY = 0;

        bool bMaploading = false;
        bool bcostmaploading = false;
        bool bGlobalcostmaploading = false;
        bool bPathloading = false;

        bool bMapMouseDN = false;
        bool bPosRectSelect = false;
        int nPosRectidx = 0;
        float ratio = 1;

        float translate_x = 0;
        float translate_y = 0;

        List<RectangleF> waringRectList = new List<RectangleF>();
        List<WarningRegionClass> waringPointList = new List<WarningRegionClass>();
        List<Rectangle> posRectList = new List<Rectangle>();
        Rectangle RectTmp = new Rectangle();

        float W_x_1;
        float W_y_1;
        float W_x_2;
        float W_y_2;

        double[] robot_globalpath_x = new double[100];
        double[] robot_globalpath_y = new double[100];
        double[] robot_path_x = new double[10000];
        double[] robot_path_y = new double[10000];
        long nrobotpathidx = 0;

      

    

        public void onSelectRobotMap_monitor(string strrobotid)
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    mainform.commBridge.onSelectRobotMap_monitor_subscribe(strrobotid);
                    Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("onSelectRobotMap_monitor err=" + ex.Message.ToString());
                }
            }
        }

        public void onSelectRobotLocalCostMap_monitor(string strrobotid)
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    mainform.commBridge.onSelectRobotLocalCostMap_monitor_subscribe(strrobotid);
                    Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("onSelectRobotLocalCostMap_monitor err=" + ex.Message.ToString());
                }
            }
        }

        public void MapInfoComplete(string strrobotid)
        {
            try
            {
                if (strrobotid == m_strRobotID)
                {
                    int width = 0;
                    int height = 0;

                    width = Data.Instance.Robot_work_info[strrobotid].robot_status_info.mapinfo.msg.info.width;
                    height = Data.Instance.Robot_work_info[strrobotid].robot_status_info.mapinfo.msg.info.height;
                    resoultion1 = (float)Data.Instance.Robot_work_info[strrobotid].robot_status_info.mapinfo.msg.info.resolution;
                    ori_x = (float)Data.Instance.Robot_work_info[strrobotid].robot_status_info.mapinfo.msg.info.origin.position.x;
                    ori_y = (float)Data.Instance.Robot_work_info[strrobotid].robot_status_info.mapinfo.msg.info.origin.position.y;

                    nSourceMapWidth = width;
                    nSourceMapHeight = height;
                    Invoke(new MethodInvoker(delegate ()
                    {
                        txtWidth.Text = string.Format("{0}", width);
                        txtHeight.Text = string.Format("{0}", height);
                        txtMapsize.Text = string.Format("{0}", width * height);
                    }));

                    Size sz = pb_map.Size;
                    if (sz.Width > width)
                    {
                        float tmpratio_w = (float)(sz.Width) / width;
                        float tmpratio_h = 1;
                        if (sz.Height < height)
                        {
                            tmpratio_h = (float)(sz.Height) / height;
                        }
                        else
                        {
                            //tmpratio_h = (float)(height) / sz.Height;
                        }
                        if (tmpratio_w > tmpratio_h )//&& tmpratio_h!=0)
                            ratio = tmpratio_h;
                        else ratio = tmpratio_w;

                    }

                    sourceMapValues = new byte[width * height];

                    for (var y = 0; y < width * height; y++)
                    {
                        sourceMapValues[y] = (byte)(Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.mapinfo.msg.data[y]);
                    }
                    onMapDisplay1();

                    Invoke(new MethodInvoker(delegate ()
                    {
                        zoomTrackBarControl1.Value = (int)(ratio * 10);
                    }));
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("MapInfoComplete err :={0}", ex.Message.ToString());
            }
        }
        public void LocalCostInfoComplete(string strrobotid)
        {
            try
            {
                if (bMaploading)
                {
                    foreach (KeyValuePair<string, Robot_RegInfo> info in Data.Instance.Robot_RegInfo_list)
                    //for (int i = 0; i < mainform.G_robotList.Count; i++)
                    {
                        string strrobotname = info.Key;

                        if (Data.Instance.Robot_work_info[strrobotname].robot_status_info.localcostmap.msg == null) continue;

                        int width = 0;
                        int height = 0;
                        double resolution = 0;
                        width = Data.Instance.Robot_work_info[strrobotname].robot_status_info.localcostmap.msg.info.width;
                        height = Data.Instance.Robot_work_info[strrobotname].robot_status_info.localcostmap.msg.info.height;
                        resolution = Data.Instance.Robot_work_info[strrobotname].robot_status_info.localcostmap.msg.info.resolution;
                        Data.Instance.Robot_work_info[strrobotname].costmap_originX = (float)Data.Instance.Robot_work_info[strrobotname].robot_status_info.localcostmap.msg.info.origin.position.x;
                        Data.Instance.Robot_work_info[strrobotname].costmap_originY = (float)Data.Instance.Robot_work_info[strrobotname].robot_status_info.localcostmap.msg.info.origin.position.y;
                        nSourceMap_localcostWidth = width;
                        nSourceMap_localcostHeight = height;


                        sourceMaplocalcostValues = new byte[width * height];

                        for (var y = 0; y < width * height; y++)
                        {
                            sourceMaplocalcostValues[y] = (byte)(Data.Instance.Robot_work_info[strrobotname].robot_status_info.localcostmap.msg.data[y]);
                        }

                        Bitmap bmSource = new Bitmap(nSourceMap_localcostWidth, nSourceMap_localcostHeight, PixelFormat.Format32bppRgb);//, PixelFormat.Format8bppIndexed);

                        Map_Robot_Image_Processing2(ref bmSource, bmSource.Width, bmSource.Height, sourceMaplocalcostValues, "cost");

                        Image imgSource_Chg = ZoomIn(bmSource, resolution / resoultion1);

                        Data.Instance.Robot_work_info[strrobotname].costmap = (Image)imgSource_Chg.Clone();

                    }

                    bcostmaploading = true;

                    onMapDisplay1();
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("LocalCostInfoComplete err :={0}", ex.Message.ToString());
            }
        }
        public void onMapDisplay1()
        {
            try
            {
                string path = "..\\Ros_info\\map\\mapinfo.bmp";
                int width = 0, height = 0;

                Bitmap bmSource = new Bitmap(nSourceMapWidth, nSourceMapHeight, PixelFormat.Format32bppRgb);//, PixelFormat.Format8bppIndexed);

                Bitmap bmMergeOKSource = new Bitmap(nSourceMapWidth, nSourceMapHeight, PixelFormat.Format32bppRgb);//, PixelFormat.Format8bppIndexed);

                Map_Robot_Image_Processing2(ref bmSource, bmSource.Width, bmSource.Height, sourceMapValues, "gray");

                dOrignX = ((ori_x * -1) / resoultion1);
                dOrignY = ((ori_y) / resoultion1);

                if (dOrignY < 0) dOrignY *= -1;
                dOrignY = nSourceMapHeight - dOrignY;


                bMaploading = true;

                if (bcostmaploading)
                {
                    Bitmap bmcost = new Bitmap(nSourceMapWidth, nSourceMapHeight, PixelFormat.Format32bppRgb);
                    Graphics g2 = Graphics.FromImage(bmcost);
                    foreach (KeyValuePair<string, Robot_RegInfo> info in Data.Instance.Robot_RegInfo_list)
                    //    for (int ii = 0; ii < mainform.G_robotList.Count; ii++)
                    {
                        string strrobotname = info.Key;

                        if (Data.Instance.Robot_work_info[strrobotname].robot_status_info.localcostmap.msg == null) continue;

                        float cellX1 = Data.Instance.Robot_work_info[strrobotname].costmap_originX / resoultion1;
                        float cellY1 = Data.Instance.Robot_work_info[strrobotname].costmap_originY / resoultion1;

                        PointF pos = new PointF();
                        pos.X = dOrignX + cellX1;
                        pos.Y = dOrignY - cellY1;

                        Bitmap costmap_temp = (Bitmap)Data.Instance.Robot_work_info[strrobotname].costmap;

                        Rectangle cost_map = new Rectangle(0, 0, costmap_temp.Width, costmap_temp.Height);
                        System.Drawing.Imaging.BitmapData bmpData_costmap =
                           costmap_temp.LockBits(cost_map, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                           costmap_temp.PixelFormat);

                        IntPtr ptr_costmap = bmpData_costmap.Scan0;
                        byte[] sourceMapValues_costmap = new byte[costmap_temp.Width * costmap_temp.Height * 4];
                        {
                            System.Runtime.InteropServices.Marshal.Copy(ptr_costmap, sourceMapValues_costmap, 0, costmap_temp.Width * costmap_temp.Height * 4);
                        }

                        costmap_temp.UnlockBits(bmpData_costmap);
                        int pos_cost = 0;
                        for (int iy = 0; iy < costmap_temp.Height; iy++)
                        {
                            for (int ix = 0; ix < costmap_temp.Width; ix++)
                            {
                                int color1 = (int)sourceMapValues_costmap[pos_cost];
                                int color2 = (int)sourceMapValues_costmap[pos_cost + 1];
                                int color3 = (int)sourceMapValues_costmap[pos_cost + 2];
                                int color4 = (int)sourceMapValues_costmap[pos_cost + 3];

                                if (color1 == 0 && color2 == 0 && color3 == 0) { }
                                else if (color1 == 255 && color2 == 255 && color3 == 255) { }
                                else if (color1 == 0x80 || color2 == 0x80 || color3 == 0x80 || color4 == 0x80)
                                {
                                }
                                else if (color1 == 0xbe || color2 == 0xbe || color3 == 0xbe || color4 == 0xbf)
                                {
                                }
                                else if (color1 == 0x40 || color2 == 0x40 || color3 == 0x40 || color4 == 0x40)
                                {
                                }
                                else
                                {
                                    bmcost.SetPixel((int)pos.X + ix, (int)(pos.Y - costmap_temp.Height + iy), Color.FromArgb((int)sourceMapValues_costmap[pos_cost], (int)sourceMapValues_costmap[pos_cost + 1], (int)sourceMapValues_costmap[pos_cost + 2]));
                                }
                                pos_cost += 4;
                            }
                        }
                    }



                    Rectangle r_map = new Rectangle(0, 0, nSourceMapWidth, nSourceMapHeight);
                    System.Drawing.Imaging.BitmapData bmpData_map =
                       bmSource.LockBits(r_map, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                       bmSource.PixelFormat);

                    IntPtr ptr_map = bmpData_map.Scan0;
                    byte[] sourceMapValues_map = new byte[nSourceMapWidth * nSourceMapHeight * 4];
                    {
                        System.Runtime.InteropServices.Marshal.Copy(ptr_map, sourceMapValues_map, 0, nSourceMapWidth * nSourceMapHeight * 4);
                    }

                    bmSource.UnlockBits(bmpData_map);

                    Rectangle r1 = new Rectangle(0, 0, nSourceMapWidth, nSourceMapHeight);
                    System.Drawing.Imaging.BitmapData bmpData_r1 =
                       bmcost.LockBits(r1, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                       bmcost.PixelFormat);

                    IntPtr ptr_r1 = bmpData_r1.Scan0;
                    byte[] sourceMapValues_r1 = new byte[nSourceMapWidth * nSourceMapHeight * 4];
                    {
                        System.Runtime.InteropServices.Marshal.Copy(ptr_r1, sourceMapValues_r1, 0, bmcost.Width * bmcost.Height * 4);
                    }
                    bmcost.UnlockBits(bmpData_r1);

                    int cnt = 0;
                    for (int i = 0; i < nSourceMapWidth * nSourceMapHeight * 4; i++)
                    {
                        if (sourceMapValues_r1[i] == 0x00)
                        {

                        }
                        else if (sourceMapValues_r1[i] == 0xff)
                        {

                        }
                        else if (sourceMapValues_r1[i] == 0x80 || sourceMapValues_r1[i + 1] == 0x80 || sourceMapValues_r1[i + 2] == 0x80 || sourceMapValues_r1[i + 3] == 0x80)
                        {
                            i += 4;
                        }
                        else if (sourceMapValues_r1[i] == 0xbe || sourceMapValues_r1[i + 1] == 0xbe || sourceMapValues_r1[i + 2] == 0xbe || sourceMapValues_r1[i + 3] == 0xbf)
                        {
                            i += 4;
                        }
                        else if (sourceMapValues_r1[i] == 0x40 || sourceMapValues_r1[i + 1] == 0x40 || sourceMapValues_r1[i + 2] == 0x40 || sourceMapValues_r1[i + 3] == 0x40)
                        {
                            i += 4;
                        }
                        else
                        {
                            {
                                sourceMapValues_map[i] = (byte)(sourceMapValues_r1[i]);
                            }
                        }

                    }



                    Rectangle or_map = new Rectangle(0, 0, nSourceMapWidth, nSourceMapHeight);
                    System.Drawing.Imaging.BitmapData bmpData_ormap =
                       bmMergeOKSource.LockBits(or_map, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                       bmMergeOKSource.PixelFormat);
                    IntPtr ptr_or = bmpData_ormap.Scan0;
                    System.Runtime.InteropServices.Marshal.Copy(sourceMapValues_map, 0, ptr_or, nSourceMapWidth * nSourceMapHeight * 4);

                    bmMergeOKSource.UnlockBits(bmpData_ormap);

                    g2.Dispose();
                }

                if (bGlobalcostmaploading)
                {
                    Bitmap bmglobalcost = new Bitmap(nSourceMapWidth, nSourceMapHeight, PixelFormat.Format32bppRgb);
                    Graphics g2 = Graphics.FromImage(bmglobalcost);


                    if (Data.Instance.Robot_work_info[m_strRobotID].robot_status_info.globalcostmap.msg == null) return;

                    float cellX1 = Data.Instance.Robot_work_info[m_strRobotID].globalcostmap_originX / resoultion1;
                    float cellY1 = Data.Instance.Robot_work_info[m_strRobotID].globalcostmap_originY / resoultion1;

                    PointF pos = new PointF();
                    pos.X = dOrignX + cellX1;
                    pos.Y = dOrignY - cellY1;

                    Bitmap globalcostmap_temp = (Bitmap)Data.Instance.Robot_work_info[m_strRobotID].globalcostmap;

                    Rectangle globalcost_map = new Rectangle(0, 0, globalcostmap_temp.Width, globalcostmap_temp.Height);
                    System.Drawing.Imaging.BitmapData bmpData_globalcostmap =
                        globalcostmap_temp.LockBits(globalcost_map, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                        globalcostmap_temp.PixelFormat);

                    IntPtr ptr_globalcostmap = bmpData_globalcostmap.Scan0;
                    byte[] sourceMapValues_globalcostmap = new byte[globalcostmap_temp.Width * globalcostmap_temp.Height * 4];
                    {
                        System.Runtime.InteropServices.Marshal.Copy(ptr_globalcostmap, sourceMapValues_globalcostmap, 0, globalcostmap_temp.Width * globalcostmap_temp.Height * 4);
                    }

                    globalcostmap_temp.UnlockBits(bmpData_globalcostmap);


                    int pos_cost = 0;
                    for (int iy = 0; iy < globalcostmap_temp.Height; iy++)
                    {
                        for (int ix = 0; ix < globalcostmap_temp.Width; ix++)
                        {
                            bmglobalcost.SetPixel(ix, iy, Color.FromArgb((int)sourceMapValues_globalcostmap[pos_cost], (int)sourceMapValues_globalcostmap[pos_cost + 1], (int)sourceMapValues_globalcostmap[pos_cost + 2]));

                            pos_cost += 4;
                        }
                    }
                  

                    Rectangle r_map = new Rectangle(0, 0, nSourceMapWidth, nSourceMapHeight);
                    System.Drawing.Imaging.BitmapData bmpData_map =
                       bmSource.LockBits(r_map, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                       bmSource.PixelFormat);

                    IntPtr ptr_map = bmpData_map.Scan0;
                    byte[] sourceMapValues_map = new byte[nSourceMapWidth * nSourceMapHeight * 4];
                    {
                        System.Runtime.InteropServices.Marshal.Copy(ptr_map, sourceMapValues_map, 0, nSourceMapWidth * nSourceMapHeight * 4);
                    }

                    bmSource.UnlockBits(bmpData_map);

                    Rectangle r1 = new Rectangle(0, 0, nSourceMapWidth, nSourceMapHeight);
                    System.Drawing.Imaging.BitmapData bmpData_globalr1 =
                       bmglobalcost.LockBits(r1, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                       bmglobalcost.PixelFormat);

                    IntPtr ptr_r1 = bmpData_globalr1.Scan0;
                    byte[] sourceMapValues_globalr1 = new byte[nSourceMapWidth * nSourceMapHeight * 4];
                    {
                        System.Runtime.InteropServices.Marshal.Copy(ptr_r1, sourceMapValues_globalr1, 0, bmglobalcost.Width * bmglobalcost.Height * 4);
                    }
                    bmglobalcost.UnlockBits(bmpData_globalr1);

                    int cnt = 0;
                    for (int i = 0; i < nSourceMapWidth * nSourceMapHeight * 4; i++)
                    {
                        if (sourceMapValues_globalr1[i] == 0x02)
                        {
                            sourceMapValues_map[i] = 125;
                        }
                        /* if (sourceMapValues_globalr1[i] == 0x02 || sourceMapValues_globalr1[i + 1] == 0x02 || sourceMapValues_globalr1[i + 2] == 0x02 )
                         {
                             sourceMapValues_globalr1[i] = 230;
                             sourceMapValues_globalr1[i+1] = 230;
                             sourceMapValues_globalr1[i+2] = 230;

                             sourceMapValues_map[i] = sourceMapValues_globalr1[i];
                             sourceMapValues_map[i+1] = sourceMapValues_globalr1[i+1];
                             sourceMapValues_map[i+2] = sourceMapValues_globalr1[i+2];
                             //sourceMapValues_map[i + 3] = 0;

                             i += 3;
                         }*/
                        else
                            sourceMapValues_map[i] = (byte)(sourceMapValues_globalr1[i]);

                    }



                    Rectangle or_map = new Rectangle(0, 0, nSourceMapWidth, nSourceMapHeight);
                    System.Drawing.Imaging.BitmapData bmpData_ormap =
                       bmMergeOKSource.LockBits(or_map, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                       bmMergeOKSource.PixelFormat);
                    IntPtr ptr_or = bmpData_ormap.Scan0;
                    System.Runtime.InteropServices.Marshal.Copy(sourceMapValues_map, 0, ptr_or, nSourceMapWidth * nSourceMapHeight * 4);

                    bmMergeOKSource.UnlockBits(bmpData_ormap);

                    g2.Dispose();
                }

                dOrignX = dOrignX * ratio + translate_x;
                dOrignY = dOrignY * ratio + translate_y;

                Image imgSource_Chg;

                if (bcostmaploading)
                {
                    imgSource_Chg = ZoomIn(bmMergeOKSource, ratio);
                }
                else if (bGlobalcostmaploading)
                {
                    imgSource_Chg = ZoomIn(bmMergeOKSource, ratio);
                }
                else
                {
                    imgSource_Chg = ZoomIn(bmSource, ratio);
                }

                Bitmap translateBmp = new Bitmap(imgSource_Chg.Width, imgSource_Chg.Height);
                translateBmp.SetResolution(imgSource_Chg.HorizontalResolution, imgSource_Chg.VerticalResolution);

                Graphics g = Graphics.FromImage(translateBmp);
                g.TranslateTransform(translate_x, translate_y);
                g.DrawImage(imgSource_Chg, new PointF(0, 0));

                pb_map.Image = translateBmp;

                pb_map.Invalidate();

                bmSource.Dispose();
                bmMergeOKSource.Dispose();


            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onMapDisplay1 err :={0}", ex.Message.ToString());
            }
        }

        private void Map_Robot_Image_Processing2(ref Bitmap bmSource, int Width, int Height, byte[] sourcemapvalue, string strfiltername)
        {
            try
            {
                //
                // 여기서 부터 Picture Box의 이미지를 복사해 오는 부분입니다
                //
                Rectangle rect = new Rectangle(0, 0, bmSource.Width, bmSource.Height);
                System.Drawing.Imaging.BitmapData bmpData =
                    bmSource.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                    bmSource.PixelFormat);

                IntPtr ptr = bmpData.Scan0;
                byte[] rgbValues;


                if (bmSource.PixelFormat == PixelFormat.Format32bppArgb || bmSource.PixelFormat == PixelFormat.Format32bppRgb)
                {
                    rgbValues = new byte[Width * Height * 4];
                }
                else
                {
                    rgbValues = new byte[Width * Height];
                }

                if (bmSource.PixelFormat == PixelFormat.Format32bppArgb || bmSource.PixelFormat == PixelFormat.Format32bppRgb)
                {
                    var k = 0;
                    for (var y = 0; y < Height; y++)
                    {
                        for (var x = 0; x < Width; x++)
                        {
                            byte btemp = sourcemapvalue[y * Width + x];

                            if (strfiltername == "gray" || strfiltername == "cost" || strfiltername == "globalcost")
                            {
                                //if (btemp == 0) btemp = 0xff;
                                //else if (btemp == 0xff) btemp = 0xf0;
                            }
                            else
                            {
                                if (btemp == 0) btemp = 0xff;
                            }


                            #region  gray filter 그레이는 r,g,b가 동일 값으로 들어감
                            if (strfiltername == "gray" || strfiltername == "cost" || strfiltername == "globalcost")
                            {
                                rgbValues[k] = btemp;
                                rgbValues[k + 1] = btemp;
                                rgbValues[k + 2] = btemp;
                            }


                            #endregion

                            k += 4;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < Width * Height; i++)
                    {
                        rgbValues[i] = sourcemapvalue[i];
                    }
                }

                //
                // 여기까지가 Marshal Copy로 rgbValues 버퍼로 영상을 Copy해 오는 부분입니다.
                //

                //
                // 여기서부터 2차원 배열로 1차원 영상을 옮기는 부분입니다
                //
                double[,] Source = new double[Width, Height];
                double[,] Target = new double[Width, Height];

                int XPos, YPos = 0;
                if (bmSource.PixelFormat == PixelFormat.Format32bppArgb || bmSource.PixelFormat == PixelFormat.Format32bppRgb)
                {
                    for (int nH = 0; nH < Height; nH++)
                    {
                        XPos = 0;

                        if (strfiltername == "gray")
                            XPos = 0; //gray xpos

                        for (int nW = 0; nW < Width; nW++)
                        {
                            Source[nW, nH] = rgbValues[XPos + YPos];
                            Target[nW, nH] = rgbValues[XPos + YPos];
                            XPos += 4;
                        }
                        YPos += Width * 4;
                    }
                }
                else
                {
                    for (int nH = 0; nH < Height; nH++)
                    {
                        XPos = 0;
                        for (int nW = 0; nW < Width; nW++)
                        {
                            Source[nW, nH] = rgbValues[XPos + YPos];
                            Target[nW, nH] = rgbValues[XPos + YPos];
                            XPos++;
                        }
                        YPos += Width;
                    }
                }

                //
                // 여기까지는 2차원 배열로 영상을 복사하는 부분입니다.
                //

                //좌우반전//
                int nconvert = 0;

                //상하반전
                nconvert = 0;
                double[,] bconvertTarget = new double[Width, Height];
                for (int nh = 0; nh < Height; nh++)
                {
                    nconvert = 0;
                    for (int nw = 0; nw < Width; nw++)
                    {
                        bconvertTarget[nw, Height - nh - 1] = Target[nw, nh];
                        //nconvert++;
                    }
                }



                //
                // 여기서 부터는 2차원 배열을 다시 1차원 버터로 옮기는 부분입니다
                //

                if (bmSource.PixelFormat == PixelFormat.Format32bppArgb || bmSource.PixelFormat == PixelFormat.Format32bppRgb)
                {
                    rgbValues = new byte[Width * Height * 4];
                }
                else
                {
                    rgbValues = new byte[Width * Height];
                }

                YPos = 0;
                if (bmSource.PixelFormat == PixelFormat.Format32bppArgb || bmSource.PixelFormat == PixelFormat.Format32bppRgb)
                {
                    for (int nH = 0; nH < Height; nH++)
                    {
                        XPos = 0;
                        for (int nW = 0; nW < Width; nW++)
                        {

                            #region  gray filter 그레이는 r,g,b가 동일 값으로 들어감
                            if (strfiltername == "gray")
                            {
                                bconvertTarget[nW, nH] = (byte)(255 - (255 * bconvertTarget[nW, nH]) / 100);
                                rgbValues[XPos + YPos] = (byte)bconvertTarget[nW, nH];
                                rgbValues[XPos + YPos + 1] = (byte)bconvertTarget[nW, nH];
                                rgbValues[XPos + YPos + 2] = (byte)bconvertTarget[nW, nH];
                            }

                            #endregion

                            if (strfiltername == "globalcost")
                            {
                                bconvertTarget[nW, nH] = (byte)(255 - (255 * bconvertTarget[nW, nH]) / 100);
                                rgbValues[XPos + YPos] = (byte)bconvertTarget[nW, nH];
                                rgbValues[XPos + YPos + 1] = (byte)bconvertTarget[nW, nH];
                                rgbValues[XPos + YPos + 2] = (byte)bconvertTarget[nW, nH];
                            }

                            #region  cost map filter
                            if (strfiltername == "cost")
                            {
                                //cost map 색상 테스트
                                if (bconvertTarget[nW, nH] < 36)
                                {
                                    rgbValues[XPos + YPos] = 0xff;
                                    rgbValues[XPos + YPos + 1] = 0xff;
                                    rgbValues[XPos + YPos + 2] = 0xff;
                                    rgbValues[XPos + YPos + 3] = 255;
                                }

                                else if (bconvertTarget[nW, nH] == 100) // lethal obstacle values (100) in purple
                                {
                                    rgbValues[XPos + YPos] = 255;
                                    rgbValues[XPos + YPos + 1] = 0;
                                    rgbValues[XPos + YPos + 2] = 255;
                                    rgbValues[XPos + YPos + 3] = 255;
                                }
                                else if (bconvertTarget[nW, nH] > 101 && bconvertTarget[nW, nH] < 128) // illegal positive values in green
                                {
                                    rgbValues[XPos + YPos] = 0;
                                    rgbValues[XPos + YPos + 1] = 255;
                                    rgbValues[XPos + YPos + 2] = 0;
                                    rgbValues[XPos + YPos + 3] = 255;
                                }

                                else if (bconvertTarget[nW, nH] > 155 && bconvertTarget[nW, nH] < 255) // illegal negative (char) values in shades of red/yellow
                                {
                                    rgbValues[XPos + YPos] = 255;
                                    rgbValues[XPos + YPos + 1] = (byte)((255 * (bconvertTarget[nW, nH] - 128)) / (254 - 128));
                                    rgbValues[XPos + YPos + 2] = 0;
                                    rgbValues[XPos + YPos + 3] = 255;
                                }
                                else
                                {
                                    rgbValues[XPos + YPos] = 255;
                                    rgbValues[XPos + YPos + 1] = 255;
                                    rgbValues[XPos + YPos + 2] = (byte)bconvertTarget[nW, nH];
                                    rgbValues[XPos + YPos + 3] = 255;
                                }

                                //cost map 색상 테스트 2
                                /*  if (bconvertTarget[nW, nH] > 1 && bconvertTarget[nW, nH] < 99) // Blue to red spectrum for most normal cost values
                                  {
                                      bconvertTarget[nW, nH] = (byte)((255 * bconvertTarget[nW, nH]) / 100);
                                      rgbValues[XPos + YPos] = (byte)bconvertTarget[nW, nH];
                                      rgbValues[XPos + YPos + 1] = 0;
                                      rgbValues[XPos + YPos + 2] = (byte)(255 - bconvertTarget[nW, nH]);
                                      rgbValues[XPos + YPos + 3] = 255;
                                  }
                                  else if (bconvertTarget[nW, nH] == 99) // inscribed obstacle values (99) in cyan
                                  {
                                      rgbValues[XPos + YPos] = 0;
                                      rgbValues[XPos + YPos + 1] = 255;
                                      rgbValues[XPos + YPos + 2] = 255;
                                      rgbValues[XPos + YPos + 3] = 255;
                                  }
                                  else if (bconvertTarget[nW, nH] == 100) // lethal obstacle values (100) in purple
                                  {
                                      rgbValues[XPos + YPos] = 255;
                                      rgbValues[XPos + YPos + 1] = 0;
                                      rgbValues[XPos + YPos + 2] = 255;
                                      rgbValues[XPos + YPos + 3] = 255;
                                  }
                                  else if (bconvertTarget[nW, nH] > 101 && bconvertTarget[nW, nH] < 128) // illegal positive values in green
                                  {
                                      rgbValues[XPos + YPos] = 0;
                                      rgbValues[XPos + YPos + 1] = 255;
                                      rgbValues[XPos + YPos + 2] = 0;
                                      rgbValues[XPos + YPos + 3] = 255;
                                  }
                                  else if (bconvertTarget[nW, nH] > 128 && bconvertTarget[nW, nH] < 255) // illegal negative (char) values in shades of red/yellow
                                  {
                                      rgbValues[XPos + YPos] = 255;
                                      rgbValues[XPos + YPos + 1] = (byte)((255 * (bconvertTarget[nW, nH] - 128)) / (254 - 128));
                                      rgbValues[XPos + YPos + 2] = 0;
                                      rgbValues[XPos + YPos + 3] = 255;
                                  }
                                  else
                                  {
                                      rgbValues[XPos + YPos] = 0xff;
                                      rgbValues[XPos + YPos + 1] = 0xff;
                                      rgbValues[XPos + YPos + 2] = 0xff;
                                  }*/
                            }
                            #endregion

                            XPos += 4;
                        }
                        YPos += Width * 4;

                    }
                }
                else
                {
                    for (int nH = 0; nH < Height; nH++)
                    {
                        XPos = 0;
                        for (int nW = 0; nW < Width; nW++)
                        {
                            rgbValues[XPos + YPos] = (byte)bconvertTarget[nW, nH];

                            XPos++;
                        }
                        YPos += Width;
                    }
                }


                //
                // 다시 Marshal Copy로 Picture Box로 옮기는 부분입니다
                //
                if (bmSource.PixelFormat == PixelFormat.Format32bppArgb || bmSource.PixelFormat == PixelFormat.Format32bppRgb)
                {
                    System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, Width * Height * 4);
                }
                else
                {
                    System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, Width * Height);
                }

                bmSource.UnlockBits(bmpData);

                //System.Drawing.Rectangle cropArea = new System.Drawing.Rectangle(6, 6, Width - 12, Height - 12);
                System.Drawing.Rectangle cropArea = new System.Drawing.Rectangle(0, 0, Width, Height);
                Bitmap bmpTemp = bmSource.Clone(cropArea, bmSource.PixelFormat);
                bmSource.Dispose();
                bmSource = null;
                bmSource = (Bitmap)(bmpTemp.Clone());
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Map_Robot_Image_Processing2 err :={0}", ex.Message.ToString());
            }

        }


        Image ZoomIn(Image img, double nresolution)
        {

            Bitmap bmp = new Bitmap(img, (int)(img.Width * nresolution), (int)(img.Height * nresolution));
            bmp.SetResolution((int)(bmp.VerticalResolution * nresolution), (int)(bmp.HorizontalResolution * nresolution));
            Graphics g = Graphics.FromImage(bmp);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bmp;
        }



        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            ratio += 0.1f;
          

            onMapDisplay1();
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {

            ratio -= 0.1f;
            onMapDisplay1();

        }


        private void pb_map_Click(object sender, EventArgs e)
        {

        }

        private void zoomTrackBarControl1_EditValueChanged(object sender, EventArgs e)
        {
            int tmpratio = zoomTrackBarControl1.Value;

            ratio = (float)(tmpratio) / 10;


            onMapDisplay1();
        }


        private void pb_map_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Pen pen = new Pen(Brushes.Red, 1);
                pen.DashStyle = DashStyle.Dash;

                if (bMaploading)
                {
                    e.Graphics.DrawLine(pen, (float)dOrignX - 10, (float)dOrignY, (float)dOrignX + 10, (float)dOrignY);
                    e.Graphics.DrawLine(pen, (float)dOrignX, (float)dOrignY - 10, (float)dOrignX, (float)dOrignY + 10);
                    e.Graphics.DrawEllipse(Pens.Red, dOrignX - 10, dOrignY - 10, 20, 20);
                    //e.Graphics.FillEllipse(Brushes.Blue, dOrignX - 10, dOrignY - 10, 20, 20);

                     foreach (KeyValuePair<string, Robot_RegInfo> info in Data.Instance.Robot_RegInfo_list)
                     {
                          string strrobotid = info.Key;

                   //string strrobotid = m_strRobotID;

                        if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate != null)
                        {
                            if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate.msg != null)
                            {
                                float robotx = (float)Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate.msg.pose.x;
                                float roboty = (float)Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate.msg.pose.y;
                                float robottheta = (float)Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate.msg.pose.theta;

                                float cellX = robotx / resoultion1;
                                float cellY = roboty / resoultion1;

                                PointF pos = new PointF();
                                pos.X = dOrignX + cellX * ratio;
                                pos.Y = dOrignY - cellY * ratio;
                                Pen pen_robot = new Pen(Brushes.BlueViolet, 3);
                                pen_robot.DashStyle = DashStyle.Solid;
                                e.Graphics.DrawEllipse(pen_robot, pos.X - 10, pos.Y - 10, 20, 20);
                                e.Graphics.DrawString(string.Format("{0}", strrobotid), new Font("고딕체", 5), Brushes.Black, pos.X - 10, pos.Y - 20);
                            
                            }
                        }
                    }

                    Pen pen4 = new Pen(Brushes.Red, 2);
                    pen4.DashStyle = DashStyle.Dot;
                    if (bMapMouseDN)
                    {
                        e.Graphics.DrawRectangle(pen4, RectTmp);
                    }

                    pen4.Dispose();

                }
                pen.Dispose();
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("pb_map_Paint err :={0}", ex.Message.ToString());
            }
        }

        float nangluar_x1 = 0;
        float nangluar_x2 = 0;
        float nangluar_y1 = 0;
        float nangluar_y2 = 0;

        PointF initposset_first = new PointF();
        PointF initposset_second = new PointF();

        private void pb_map_MouseDown(object sender, MouseEventArgs e)
        {
            int nx = e.X;
            int ny = e.Y;
            txtXcell.Text = string.Format("{0}", e.X);
            txtYcell.Text = string.Format("{0}", e.Y);

            //  nx = (int)(nx - translate_x);
            //  ny = (int)(ny - translate_y);

            float tmpOriginX = dOrignX / ratio;
            float tmpOriginY = dOrignY / ratio;

            float currXpos = (float)(((nx - dOrignX) * resoultion1)) / ratio;
            float currYpos = (float)(((ny - dOrignY) * resoultion1)) / ratio;
            nangluar_x1 = currXpos;
            nangluar_y1 = currYpos;




            W_x_1 = e.X;
            W_y_1 = e.Y;

            initposset_first = new PointF();

            //if (chkAngluar.Checked)
            //{ }
            //else if (chkInitPosSet.Checked)
            //{
            //    initposset_first.X = nx;
            //    initposset_first.Y = ny;

            //    bMapMouseDN = true;
            //}
            //else
            //{
            //    if (posRectList.Count > 0)
            //    {
            //        for (int i = 0; i < posRectList.Count; i++)
            //        {
            //            if (posRectList[i].Contains(e.X, e.Y))
            //            {
            //                bPosRectSelect = true;
            //                nPosRectidx = i;
            //                break;
            //            }
            //        }
            //    }

            //    if (!bPosRectSelect) bMapMouseDN = true;
            //}
        }

        private void pb_map_MouseUp(object sender, MouseEventArgs e)
        {
            int nx = e.X;
            int ny = e.Y;
            txtXcell.Text = string.Format("{0}", e.X);
            txtYcell.Text = string.Format("{0}", e.Y);

      
            //float tmpOriginX = dOrignX / ratio;
            //float tmpOriginY = dOrignY / ratio;

            //float currXpos = (float)(((nx - dOrignX) * resoultion1)) / ratio;
            //float currYpos = (float)(((ny - dOrignY) * resoultion1)) / ratio;

            //nangluar_x2 = currXpos;
            //nangluar_y2 = currYpos;

            //float TargetAngle = (float)(Math.Atan2(nangluar_y2 - nangluar_y1, nangluar_x2 - nangluar_x1) * 180f / Math.PI);
            //TargetAngle = mainform.DegreeToRadian(string.Format("{0:f2}", TargetAngle));
            //TargetAngle *= -1;
            //txtAngluar.Text = string.Format("{0:f2}", TargetAngle);

            //W_x_2 = e.X;
            //W_y_2 = e.Y;
            //if (chkAngluar.Checked)
            //{
            //}
            //else if (chkInitPosSet.Checked)
            //{
            //    initposset_first = new PointF();
            //    initposset_second = new PointF();


            //}
            //else
            //{
            //    if (bMapMouseDN)
            //    {
            //        RectangleF rectT = RectTmp;
            //        //rectT.X = (float)((rectT.X / ratio) -translate_x);
            //        //rectT.Y = (float)((rectT.Y / ratio)-translate_y);

            //        rectT.X = (float)((rectT.X - translate_x) / ratio);
            //        rectT.Y = (float)((rectT.Y - translate_y) / ratio);

            //        rectT.Width = (float)(rectT.Width / ratio);
            //        rectT.Height = (float)(rectT.Height / ratio);
            //        waringRectList.Add(rectT);

            //        WarningRegionClass wariningregion = new WarningRegionClass();
            //        wariningregion.nX1 = (float)(((W_x_1 - dOrignX) * resoultion1)) / ratio;
            //        wariningregion.nY1 = (float)(((W_y_1 - dOrignY) * resoultion1)) / ratio * -1;
            //        wariningregion.nX2 = (float)(((W_x_2 - dOrignX) * resoultion1)) / ratio;
            //        wariningregion.nY2 = (float)(((W_y_2 - dOrignY) * resoultion1)) / ratio * -1;

            //        waringPointList.Add(wariningregion);

            //    }
            //}

            bMapMouseDN = false;
            bPosRectSelect = false;
        }

        private void pb_map_MouseClick(object sender, MouseEventArgs e)
        {
            int nx = e.X;
            int ny = e.Y;
            txtXcell.Text = string.Format("{0}", e.X);
            txtYcell.Text = string.Format("{0}", e.Y);

            //  nx = (int)(nx - translate_x);
            //  ny = (int)(ny - translate_y);

            float tmpOriginX = dOrignX / ratio;
            float tmpOriginY = dOrignY / ratio;

            float currXpos = (float)(((nx - dOrignX) * resoultion1)) / ratio;
            float currYpos = (float)(((ny - dOrignY) * resoultion1)) / ratio;

            txtXpos.Text = string.Format("{0:f2}", currXpos);
            txtYpos.Text = string.Format("{0:f2}", currYpos * -1);

        }





        private void pb_map_MouseMove(object sender, MouseEventArgs e)
        {
            int nx = e.X;
            int ny = e.Y;
            txtXcell.Text = string.Format("{0}", e.X);
            txtYcell.Text = string.Format("{0}", e.Y);


            //    nx = (int)(nx - translate_x);
            //   ny = (int)(ny - translate_y);

            float currXpos = (float)(((nx - dOrignX) * resoultion1)) / ratio;
            float currYpos = (float)(((ny - dOrignY) * resoultion1)) / ratio;

            txtXpos.Text = string.Format("{0:f2}", currXpos);
            txtYpos.Text = string.Format("{0:f2}", currYpos * -1);
            //if (chkAngluar.Checked)
            //{
            //    nangluar_x2 = currXpos;
            //    nangluar_y2 = currYpos;

            //    float TargetAngle = (float)(Math.Atan2(nangluar_y2 - nangluar_y1, nangluar_x2 - nangluar_x1) * 180f / Math.PI);
            //    TargetAngle = mainform.DegreeToRadian(string.Format("{0:f2}", TargetAngle));
            //    TargetAngle *= -1;
            //    txtAngluar.Text = string.Format("{0:f2}", TargetAngle);
            //}
            //else if (chkInitPosSet.Checked)
            //{
            //    initposset_second = new PointF();
            //    initposset_second.X = nx;
            //    initposset_second.Y = ny;
            //}
            //else
            {
                if (bMapMouseDN)
                {
                    Rectangle rect = new Rectangle();
                    rect.X = (int)(W_x_1);
                    rect.Y = (int)(W_y_1);
                    rect.Width = (int)((nx - (int)W_x_1));
                    rect.Height = (int)((ny - (int)W_y_1));

                    RectTmp = rect;

                    pb_map.Invalidate();
                }

                if (bPosRectSelect)
                {
                    if (posRectList.Count > 0)
                    {

                        pb_map.Invalidate();
                    }
                }
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            translate_y -= 50;
            txtTY.Text = string.Format("{0}", translate_y);
            onMapDisplay1();
        }

        private void btnDn_Click(object sender, EventArgs e)
        {
            translate_y += 50;
            txtTY.Text = string.Format("{0}", translate_y);
            onMapDisplay1();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            translate_x -= 50;
            txtTX.Text = string.Format("{0}", translate_x);
            onMapDisplay1();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            translate_x += 50;
            txtTX.Text = string.Format("{0}", translate_x);
            onMapDisplay1();
        }


        #endregion

        private void btnEcoMode2_Click(object sender, EventArgs e)
        {
            btnDriveMode2.Checked = false;
        }

        private void pictureBox_right_Click(object sender, EventArgs e)
        {

        }

        private void btnMappingMode2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }

    public class MapClass
    {
        public int nMapWidth;
        public int nMapHeight;
        public int Mapresoultion;

    }

    public class WarningRegionClass
    {
        public float nX1;
        public float nY1;
        public float nX2;
        public float nY2;
    }

    public class LocalCostMap_DataInfomation
    {
        public List<LocalCostMap_Data> localcostmap;
    }

    public class LocalCostMap_Data
    {
        public float originX;
        public float originY;
        public float resoultion;

    }
}
