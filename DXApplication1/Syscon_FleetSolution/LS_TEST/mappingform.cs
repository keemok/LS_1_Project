using Rosbridge.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Syscon_Solution.LS_TEST
{
    public partial class mappingform : Form
    {

        FleetManager.Comm.Comm_bridge commBridge;
        FleetManager.DB.DB_bridge dbBridge;


        public mappingform()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strAddr = textBox1.Text.ToString();
            ROSConnection(strAddr);
        }


        public Thread manualRun_thread;
        public bool bThreadBreak = false;



        Thread ServerConnect_Checkthread;
        private async void ROSConnection(string strAddr)
        {
            if (Data.Instance.isConnected)
            {
                try
                {

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

                        
                        string uri = strAddr;
                        Data.Instance.socket = new Rosbridge.Client.Socket(new Uri(uri));

                        Data.Instance.md = new MessageDispatcher(Data.Instance.socket, new MessageSerializerV2_0());
                        await Data.Instance.md.StartAsync();

                        if (Data.Instance.socket.Connected)
                        {
                        }

                        Data.Instance.isConnected = true;

                        if (ServerConnect_Checkthread != null)
                        {
                            ServerConnect_Checkthread.Abort();
                            ServerConnect_Checkthread = null;
                        }
                        ServerConnect_Checkthread = new Thread(onConnectCheck);
                        ServerConnect_Checkthread.Start();


                        if (Data.Instance.isConnected == true)
                        {



                            onSuscribe_RobotsStatus_Basic();


                        }

                }
                catch (Exception ex)
                {
                    ROSDisconnect();
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

                Invoke(new MethodInvoker(delegate ()
                {
                }));
                Data.Instance.socket = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ROSDisconnect err = " + ex.Message.ToString());
            }
        }
        private void onConnectCheck()
        {
            try
            {
                for (; ; )
                {
                    if (Data.Instance.bFormClose) break;

                    if (!Data.Instance.isConnected) break;

                    if (!Data.Instance.socket.Connected)
                    {

                        ROSDisconnect();
                        break;
                    }
                    Thread.Sleep(10);

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onConnectCheck err :={0}", ex.Message.ToString());
            }
        }
        private void onSuscribe_RobotsStatus_Basic()
        {
            try
            {


                if (Data.Instance.isConnected)
                {
                    int cnt = 0;
                    cnt = Data.Instance.Robot_RegInfo_list.Count;
                    if (cnt > 0)
                    {
                        foreach (KeyValuePair<string, Robot_RegInfo> info in Data.Instance.Robot_RegInfo_list)
                        {
                            string strrobotid = info.Key;
                            Robot_RegInfo value = info.Value;

                           commBridge.onSelectRobotStatus_Basic_subscribes(strrobotid);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSuscribe_RobotsStatus_Basic err :={0}", ex.Message.ToString());
            }
        }
        private void manualRun_thread_func()
        {
            for (; ; )
            {
                if (bThreadBreak) break;

                else
                {
                    Twist data = new Twist();
                    data.linear.x = m_dManualspeed;
                    data.angular.z = m_dManualAngluar;
                    commBridge.onManualRun(data, "R_006");
                }
                Thread.Sleep(50);
            }
        }

        float resoultion1,ori_x,ori_y;
        int nSourceMapWidth = 0;
        int nSourceMapHeight = 0;
        float ratio;
        byte[] sourceMapValues;


        public void MapInfoComplete(string strrobotid)
        {
            try
            {
                if (strrobotid == "R_006")
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
                        if (tmpratio_w > tmpratio_h)//&& tmpratio_h!=0)
                            ratio = tmpratio_h;
                        else ratio = tmpratio_w;

                    }

                    sourceMapValues = new byte[width * height];

                    for (var y = 0; y < width * height; y++)
                    {
                        sourceMapValues[y] = (byte)(Data.Instance.Robot_work_info["R_006"].robot_status_info.mapinfo.msg.data[y]);
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
        private void mappingform_Load(object sender, EventArgs e)
        {
            commBridge = new FleetManager.Comm.Comm_bridge(this);

            commBridge.mapinfo_Evt += new FleetManager.Comm.Comm_bridge.MapInfoComplete(this.MapInfoComplete);
            //  mainform.commBridge.Localcostmapinfo_Evt += new FleetManager.Comm.Comm_bridge.LocalCostInfoComplete(mainform.LocalCostInfoComplete);

            if (manualRun_thread != null)
            {
                manualRun_thread.Abort();
                manualRun_thread = null;
            }

            manualRun_thread = new Thread(manualRun_thread_func);
            manualRun_thread.Start();
        }




        #region 이동 관련

        double m_dManualspeed = 0;
        double m_dManualAngluar = 0;


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
                        m_dManualAngluar = double.Parse(strAngular);// -.5;


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
                    string strspeed = txtSpeed.Text.ToString();
                    if (strspeed != "")
                    {
                        m_dManualspeed = 0;
                        m_dManualAngluar = 0;

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

        #endregion


        float dOrignX = 0;
        float dOrignY = 0;
        float translate_x = 0;
        float translate_y = 0;
        bool bcostmaploading = false;
        bool bGlobalcostmaploading = false;

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

        #region 드라이브 모드

        string m_strPrevRobotID = "";
        string m_strCurrMode = "NAV";


        private void onModeDp(string strmode)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                lblRobotMode.Text = strmode;
            }));
        }

        private void btnDriveMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    if (m_strRobotID != "")
                    {



                        commBridge.onSP_routine_publish(m_strRobotID, "NAV");
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
        private void onBtnModeChg(string strmode)
        {
            btnDriveMode2.Checked = false;
            btnMappingMode2.Checked = false;
            groupBox_mapping.Enabled = false;

            if (strmode == "ECO")
            {
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
        private void btnMappingMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    if (m_strRobotID != "")
                    {
                        commBridge.onSP_routine_publish(m_strRobotID, "SLAM");
                        onModeDp(string.Format("RobotID={0}, mode =SLAM", m_strRobotID));
                        m_strCurrMode = "SLAM";
                        onBtnModeChg(m_strCurrMode);

                        TopicList list = new TopicList();
                        commBridge.onDeleteSelectSubscribe(m_strRobotID + list.topic_staticMap);

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
        #endregion

        public void onSelectRobotMap_monitor(string strrobotid)
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    commBridge.onSelectRobotMap_monitor_subscribe(strrobotid);
                    Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("onSelectRobotMap_monitor err=" + ex.Message.ToString());
                }
            }
        }
        string m_strRobotID = "R_006";

        private void btnMapSave_Click(object sender, EventArgs e)
        {

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
        Image ZoomIn(Image img, double nresolution)
        {

            Bitmap bmp = new Bitmap(img, (int)(img.Width * nresolution), (int)(img.Height * nresolution));
            bmp.SetResolution((int)(bmp.VerticalResolution * nresolution), (int)(bmp.HorizontalResolution * nresolution));
            Graphics g = Graphics.FromImage(bmp);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bmp;
        }
    }
}
