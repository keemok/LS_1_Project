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
using System.Drawing;
using System.Drawing.Imaging;using System.Runtime.InteropServices;

namespace SysSolution
{
    public partial class MapDspForm : Form
    {
        private int curValue = 0;
        private int bfValue = 0;

        private int orgWidth = 0;
        private int orgHeight = 0;

        Worker worker;

        byte[] sourceMapValues;
        int nSourceMapWidth = 0;
        int nSourceMapHeight = 0;
        byte[] sourceGlobalcostMapValues;
        int nSourceGlobalcostMapWidth = 0;
        int nSourceGlobalcostMapHeight = 0;

        byte[] sourceLocalcostMapValues;
        int nSourceLocalcostMapWidth = 0;
        int nSourceLocalcostMapHeight = 0;

        private Point LastPoint;
        private Point imgPoint;
        private Rectangle imgRect;
        private Point clickPoint;
        private double ratio = 1.0F;
        private double globalcost_ratio = 1.0F;

        public MapDspForm()
        {
            InitializeComponent();
        }

        private void MapDspForm_Load(object sender, EventArgs e)
        {
            worker = new Worker(this, 1);

            worker.mapinfo_Evt += new Worker.MapInfoComplete(this.MapInfoComplete);

            worker.Globalcostmapinfo_Evt += new Worker.GlobalCostInfoComplete(this.GlobalCostInfoComplete);
            worker.Localcostmapinfo_Evt += new Worker.LocalCostInfoComplete(this.LocalCostInfoComplete);
            pb_map.MouseWheel += new MouseEventHandler(onMap_resize);
            pb_Globalcost.MouseWheel += new MouseEventHandler(onGlobalcostMap_resize);

            //onBtnEnable(false);
            groupBox1.Enabled = false;
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
                    onCompulsion_Close();

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
                            //worker.onRealtimeRobotStatus_subscribe();

                            btnConnect.Text = "disconnect";
                            groupBox1.Enabled = true;
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
                groupBox1.Enabled = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        #endregion


       public void onBtnEnable(bool _b)
        {
            btnReadMap.Enabled = _b;
            button5.Enabled = _b;
            btnCostMapRead.Enabled = _b;
            button6.Enabled = _b;
        }
        public void MapInfoComplete()
        {
            try
            {
                int width = 0;
                int height = 0;
                double resolution = 0;
                width = Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.mapinfo.msg.info.width;
                height = Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.mapinfo.msg.info.height;
                resolution = Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.mapinfo.msg.info.resolution;
                nSourceMapWidth = width;
                nSourceMapHeight = height;

                sourceMapValues = new byte[width * height];

                for (var y = 0; y < width * height; y++)
                {
                    sourceMapValues[y] = (byte)(Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.mapinfo.msg.data[y]);
                }

                string startPath = Application.StartupPath;
                string path = string.Empty;
                path = startPath + @"\mapinfo.bmp";

                onMapinfoSave(path, width, height,sourceMapValues);

                #region 8bit파일을 로드 하여 32bit 이미지로 화면에 표시
                Bitmap bits = new Bitmap(path);
                width = bits.Width ;
                height = bits.Height;
                
                nSourceMapWidth = width;
                nSourceMapHeight = height;

                sourceMapValues = new byte[width * height];

                Rectangle rect3 = new Rectangle(0, 0, width, height);
                System.Drawing.Imaging.BitmapData bmpData3 =
                   bits.LockBits(rect3, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                   bits.PixelFormat);

                IntPtr ptr2 = bmpData3.Scan0;

                {
                    System.Runtime.InteropServices.Marshal.Copy(ptr2, sourceMapValues, 0, width * height);
                }

                bits.UnlockBits(bmpData3);


                Bitmap bmSource = new Bitmap(width, height,PixelFormat.Format32bppRgb);//, PixelFormat.Format8bppIndexed);

                Map_Robot_Image_Processing2(ref bmSource, bmSource.Width, bmSource.Height, sourceMapValues,"gray");
                pb_map.Image = (Bitmap)(bmSource.Clone());
                ratio *= 1.1f;
                ratio *= 1.1f;
               // pb_map.Image = ZoomIn(bmSource, ratio);
                pb_map.Invalidate();

                bmSource.Dispose();
                #endregion

            //    robot_image = pb_map.Image;
            //    opacity_image = robot_image;
             //   opacity_image = RotateImage(robot_image, new PointF(robot_image.Width / 2, robot_image.Height / 2), (float)(-90));

            //    pb_map.Invalidate();

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("MapInfoComplete err :={0}", ex.Message.ToString());
            }
        }
        private void onMapinfoSave(string strfilename, int width,int height, byte[] maparray)
        {
            try
            {
                Bitmap bmSource2 = new Bitmap(width, height, PixelFormat.Format8bppIndexed);

                Rectangle rect2 = new Rectangle(0, 0, bmSource2.Width, bmSource2.Height);
                System.Drawing.Imaging.BitmapData bmpData2 =
                    bmSource2.LockBits(rect2, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                    bmSource2.PixelFormat);

                IntPtr ptr = bmpData2.Scan0;

                {
                    System.Runtime.InteropServices.Marshal.Copy(maparray, 0, ptr, width * height);
                }

                bmSource2.UnlockBits(bmpData2);

                System.Drawing.Rectangle cropArea = new System.Drawing.Rectangle(0, 0, width , height );
                Bitmap bmpTemp = bmSource2.Clone(cropArea, bmSource2.PixelFormat);
                bmSource2.Dispose();
                bmSource2 = null;
                bmSource2 = (Bitmap)(bmpTemp.Clone());
                
                bmSource2.Save(strfilename, ImageFormat.Bmp);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onMapinfoSave err :={0}", ex.Message.ToString());
            }
        }




        public void GlobalCostInfoComplete()
        {
            int width = 0;
            int height = 0;
            double resolution = 0;
            width = Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.globalcostmap.msg.info.width;
            height = Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.globalcostmap.msg.info.height;
            resolution = Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.globalcostmap.msg.info.resolution;
            nSourceGlobalcostMapWidth = width;
            nSourceGlobalcostMapHeight = height;

            sourceGlobalcostMapValues = new byte[width * height];

            for (var y = 0; y < width * height; y++)
            {
                sourceGlobalcostMapValues[y] = (byte)(Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.globalcostmap.msg.data[y]);
            }

            string startPath = Application.StartupPath;
            string path = string.Empty;
            path = startPath + @"\map_globalcost.bmp";

            onMapinfoSave(path, width, height, sourceGlobalcostMapValues);

            #region 8bit파일을 로드 하여 32bit 이미지로 화면에 표시
            Bitmap bits = new Bitmap(path);
            width = bits.Width;
            height = bits.Height;

            nSourceGlobalcostMapWidth = width;
            nSourceGlobalcostMapHeight = height;

            sourceGlobalcostMapValues = new byte[width * height];

            Rectangle rect3 = new Rectangle(0, 0, width, height);
            System.Drawing.Imaging.BitmapData bmpData3 =
               bits.LockBits(rect3, System.Drawing.Imaging.ImageLockMode.ReadWrite,
               bits.PixelFormat);

            IntPtr ptr2 = bmpData3.Scan0;

            {
                System.Runtime.InteropServices.Marshal.Copy(ptr2, sourceGlobalcostMapValues, 0, width * height);
            }

            bits.UnlockBits(bmpData3);


            Bitmap bmSource = new Bitmap(width, height, PixelFormat.Format32bppRgb);//, PixelFormat.Format8bppIndexed);

            Map_Robot_Image_Processing2(ref bmSource, bmSource.Width, bmSource.Height, sourceGlobalcostMapValues, "yellow");
            pb_Globalcost.Image = (Bitmap)(bmSource.Clone());
            globalcost_ratio *= 1.1f;
            globalcost_ratio *= 1.1f;
            pb_Globalcost.Image = ZoomIn(bmSource, ratio);
            pb_Globalcost.Invalidate();

            path = string.Empty;
            path = startPath + @"\map_globalcost_32.bmp";
            bmSource.Save(path, ImageFormat.Bmp);


            bmSource.Dispose();
            #endregion

            /*    byte[] rgbValues = new byte[Width * Height];
                     sourceMapValues = new byte[Width * Height];
                     for (int i = 0; i < width * height; i++)
                     {
                         rgbValues[i] = (byte)(Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.mapinfo.msg.data[i]);
                         //if(rgbValues[i] < (byte)0xff)
                         rgbValues[i] = (byte)(0xff - rgbValues[i]);

                         sourceMapValues[i] = rgbValues[i];
                    }
                     Image bmp = ImageFromRawBgraArray(rgbValues, width, height);

                     imgPoint = new Point(bmp.Width / 2, bmp.Height / 2);
                     imgRect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                     ratio = 1.0;
                     clickPoint = imgPoint;
                     pb_map.Image = bmp;
               */

        }

        public void LocalCostInfoComplete()
        {
            int width = 0;
            int height = 0;
            double resolution = 0;
            width = Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.localcostmap.msg.info.width;
            height = Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.localcostmap.msg.info.height;
            resolution = Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.localcostmap.msg.info.resolution;
            nSourceLocalcostMapWidth = width;
            nSourceLocalcostMapHeight = height;

            sourceLocalcostMapValues = new byte[width * height];

            for (var y = 0; y < width * height; y++)
            {
                sourceLocalcostMapValues[y] = (byte)(Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.localcostmap.msg.data[y]);
            }

            string startPath = Application.StartupPath;
            string path = string.Empty;
            path = startPath + @"\map_localcost.bmp";

            onMapinfoSave(path, width, height, sourceLocalcostMapValues);

            #region 8bit파일을 로드 하여 32bit 이미지로 화면에 표시
            Bitmap bits = new Bitmap(path);
            width = bits.Width;
            height = bits.Height;

            nSourceLocalcostMapWidth = width;
            nSourceLocalcostMapHeight = height;

            sourceLocalcostMapValues = new byte[width * height];

            Rectangle rect3 = new Rectangle(0, 0, width, height);
            System.Drawing.Imaging.BitmapData bmpData3 =
               bits.LockBits(rect3, System.Drawing.Imaging.ImageLockMode.ReadWrite,
               bits.PixelFormat);

            IntPtr ptr2 = bmpData3.Scan0;

            {
                System.Runtime.InteropServices.Marshal.Copy(ptr2, sourceLocalcostMapValues, 0, width * height);
            }

            bits.UnlockBits(bmpData3);


            Bitmap bmSource = new Bitmap(width, height, PixelFormat.Format32bppRgb);//, PixelFormat.Format8bppIndexed);

            Map_Robot_Image_Processing2(ref bmSource, bmSource.Width, bmSource.Height, sourceLocalcostMapValues, "gray");
            pb_localcostmap.Image = (Bitmap)(bmSource.Clone());
            bmSource.Dispose();
            #endregion
        }

        public Image opacity_image;
        public Image robot_image;
        private void pb_map_Paint(object sender, PaintEventArgs e)
        {

            // e.Graphics.DrawImage(bmp, rect);
            if (robot_image != null)
            {
              //  e.Graphics.DrawImage(opacity_image, new Point(0, 0));
            }
        }
       
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



        private void Map_Robot_Image_Processing2(ref Bitmap bmSource, int Width, int Height,byte[] sourcemapvalue,string strfiltername)
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

                            if (strfiltername == "gray")
                            {
                                if (btemp == 0) btemp = 0xff;
                                else if (btemp == 0xff) btemp = 0xd2;
                            }
                            else
                            {
                                if (btemp == 0) btemp = 0xff;
                            }

                           

                      

                            #region yellow filter
                            if (strfiltername == "yellow")
                            {
                                if(btemp==0xff)
                                {
                                    rgbValues[k] = btemp;
                                    rgbValues[k + 1] = btemp;
                                    rgbValues[k + 2] = btemp;
                                    
                                }
                                else
                                {
                                    if (btemp > 0x9b) btemp = 0x9b;
                                    if (btemp < 0x24) btemp = 0x24;

                                    rgbValues[k + 1] = 0xff;// (byte)(0xff- btemp);
                                    rgbValues[k + 2] = 0xff;// (byte)(0xff - btemp);


                                    rgbValues[k + 3] = btemp;
                                }
                                
                               
                            }
                            #endregion


                           

                            #region  gray filter 그레이는 r,g,b가 동일 값으로 들어감
                            if (strfiltername == "gray")
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
                    for (int i = 0; i < Width* Height; i++)
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

                        if (strfiltername == "yellow" || strfiltername =="red")
                            
                            XPos = 3;//blue xpos

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
             /*   double[,] bconvertTarget = new double[Width, Height];
                for (int nh = 0; nh < Height; nh++)
                {
                    nconvert = 0;
                    for (int nw = Width; nw > 0; nw--)
                    {
                        bconvertTarget[nw-1, nh] = Target[nconvert, nh];
                        nconvert++;
                    }
                }
                */
                //상하반전
                nconvert = 0;
                double[,] bconvertTarget = new double[Width, Height];
                for (int nh = 0; nh < Height; nh++)
                {
                    nconvert = 0;
                    for (int nw = 0; nw < Width; nw++)
                    {
                        bconvertTarget[nw, Height-nh-1] = Target[nw, nh];
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
                            #region yellow filter
                            if (strfiltername == "yellow")
                            {
                                byte btemp =(byte)bconvertTarget[nW, nH];

                                
                                if (btemp == 0) btemp = 0xff;

                                if (btemp == 0xff)
                                {
                                    rgbValues[XPos + YPos] = btemp;
                                    rgbValues[XPos + YPos + 1] = btemp;
                                    rgbValues[XPos + YPos + 2] = btemp;

                                }
                                else
                                {
                                    if (btemp > 0x9b) btemp = 0x9b;
                                    if (btemp < 0x24) btemp = 0x24;

                                    rgbValues[XPos + YPos + 1] = 0xff;// (byte)(0xff- btemp);
                                    rgbValues[XPos + YPos + 2] = 0xff;// (byte)(0xff - btemp);


                                    rgbValues[XPos + YPos + 3] = btemp;
                                }
                            }
                            #endregion

                           


                            #region  gray filter 그레이는 r,g,b가 동일 값으로 들어감
                            if (strfiltername == "gray")
                            {
                                rgbValues[XPos + YPos] = (byte)bconvertTarget[nW, nH];
                                rgbValues[XPos + YPos + 1] = (byte)bconvertTarget[nW, nH];
                                rgbValues[XPos + YPos + 2] = (byte)bconvertTarget[nW, nH];
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

                System.Drawing.Rectangle cropArea = new System.Drawing.Rectangle(6, 6, Width - 12, Height - 12);
                Bitmap bmpTemp = bmSource.Clone(cropArea, bmSource.PixelFormat);
                bmSource.Dispose();
                bmSource = null;
                bmSource = (Bitmap)(bmpTemp.Clone());
            }
            catch(Exception ex)
            {
                Console.Out.WriteLine("Map_Robot_Image_Processing2 err :={0}", ex.Message.ToString());
            }

        }

        string m_strCurrentRobotDetailInfo_ID = "";
        string m_strRobotName = "R_006";
        private void btnReadMap_Click(object sender, EventArgs e)
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    //worker.onDeleteSelectSubscribe(Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.mapinfo.topic);
                    //Thread.Sleep(Data.Instance.nSubscribeDelayTime);


                    Data.Instance.Robot_work_info.Clear();

                    m_strCurrentRobotDetailInfo_ID = m_strRobotName;
                    Robot_WorkKInfo robot_workinfo = new Robot_WorkKInfo();
                    robot_workinfo.robot_workdata = new List<Robot_Work_Data>();

                    robot_workinfo.strRobotID = m_strRobotName;
                    robot_workinfo.nCurrWork_cnt = 1;
                    robot_workinfo.strLoop_Flag = "wait";

                    robot_workinfo.robot_status_info = new Robot_Status_info();
                    robot_workinfo.robot_status_info.robotstate = new RobotState_1();
                    robot_workinfo.robot_status_info.motorstate = new MotorInformation();
                    robot_workinfo.robot_status_info.lidar = new LidarScan();
                    robot_workinfo.robot_status_info.ultrasonic = new UltrasonicRawInfo();
                    robot_workinfo.robot_status_info.workfeedback = new WorkFeedback();
                    robot_workinfo.robot_status_info.workresult = new WorkResult();
                    robot_workinfo.robot_status_info.bmsinfo = new BMSInfo();
                    robot_workinfo.robot_status_info.ur_status = new UR_StatusInformation();

                    Data.Instance.Robot_work_info.Add(m_strRobotName, robot_workinfo);

                    ratio = 1.0f;
                    onSelectRobotMap_monitor(m_strRobotName);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("onMonitoring() err=" + ex.Message.ToString());
                }
            }
        }
        public void onSelectRobotMap_monitor(string strrobotid)
        {
            if (Data.Instance.isConnected)
            {
                try
                {

                    worker.onSelectRobotMap_monitor_subscribe(strrobotid);
                    Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("onSelectRobotMap_monitor err=" + ex.Message.ToString());
                }
            }
        }

        private void btnCostMapRead_Click(object sender, EventArgs e)
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    Data.Instance.Robot_work_info.Clear();

                    m_strCurrentRobotDetailInfo_ID = m_strRobotName;
                    Robot_WorkKInfo robot_workinfo = new Robot_WorkKInfo();
                    robot_workinfo.robot_workdata = new List<Robot_Work_Data>();

                    robot_workinfo.strRobotID = m_strRobotName;
                    robot_workinfo.nCurrWork_cnt = 1;
                    robot_workinfo.strLoop_Flag = "wait";

                    robot_workinfo.robot_status_info = new Robot_Status_info();
                    robot_workinfo.robot_status_info.robotstate = new RobotState_1();
                    robot_workinfo.robot_status_info.motorstate = new MotorInformation();
                    robot_workinfo.robot_status_info.lidar = new LidarScan();
                    robot_workinfo.robot_status_info.ultrasonic = new UltrasonicRawInfo();
                    robot_workinfo.robot_status_info.workfeedback = new WorkFeedback();
                    robot_workinfo.robot_status_info.workresult = new WorkResult();
                    robot_workinfo.robot_status_info.bmsinfo = new BMSInfo();
                    robot_workinfo.robot_status_info.ur_status = new UR_StatusInformation();

                    Data.Instance.Robot_work_info.Add(m_strRobotName, robot_workinfo);

                    globalcost_ratio = 1.0f;
                    onSelectRobotCostMap_monitor(m_strRobotName);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("onMonitoring() err=" + ex.Message.ToString());
                }
            }
        }

        public void onSelectRobotCostMap_monitor(string strrobotid)
        {
            if (Data.Instance.isConnected)
            {
                try
                {

                    worker.onSelectRobotCostMap_monitor_subscribe(strrobotid);
                    Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("onSelectRobotMap_monitor err=" + ex.Message.ToString());
                }
            }
        }

        private void btnLocalCostMapRead_Click(object sender, EventArgs e)
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    Data.Instance.Robot_work_info.Clear();

                    m_strCurrentRobotDetailInfo_ID = m_strRobotName;
                    Robot_WorkKInfo robot_workinfo = new Robot_WorkKInfo();
                    robot_workinfo.robot_workdata = new List<Robot_Work_Data>();

                    robot_workinfo.strRobotID = m_strRobotName;
                    robot_workinfo.nCurrWork_cnt = 1;
                    robot_workinfo.strLoop_Flag = "wait";

                    robot_workinfo.robot_status_info = new Robot_Status_info();
                    robot_workinfo.robot_status_info.robotstate = new RobotState_1();
                    robot_workinfo.robot_status_info.motorstate = new MotorInformation();
                    robot_workinfo.robot_status_info.lidar = new LidarScan();
                    robot_workinfo.robot_status_info.ultrasonic = new UltrasonicRawInfo();
                    robot_workinfo.robot_status_info.workfeedback = new WorkFeedback();
                    robot_workinfo.robot_status_info.workresult = new WorkResult();
                    robot_workinfo.robot_status_info.bmsinfo = new BMSInfo();

                    Data.Instance.Robot_work_info.Add(m_strRobotName, robot_workinfo);


                    onSelectRobotLocalCostMap_monitor(m_strRobotName);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("btnLocalCostMapRead_Click() err=" + ex.Message.ToString());
                }
            }
            
        }
        public void onSelectRobotLocalCostMap_monitor(string strrobotid)
        {
            if (Data.Instance.isConnected)
            {
                try
                {

                    worker.onSelectRobotLocalCostMap_monitor_subscribe(strrobotid);
                    Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("onSelectRobotLocalCostMap_monitor err=" + ex.Message.ToString());
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    worker.onDeleteSelectSubscribe(Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.mapinfo.topic);
                    Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("onSelectRobotMap_monitor err=" + ex.Message.ToString());
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    worker.onDeleteSelectSubscribe(Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.globalcostmap.topic);
                    Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("onSelectRobotMap_monitor err=" + ex.Message.ToString());
                }
            }
        }
        private void button7_Click_1(object sender, EventArgs e)
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    worker.onDeleteSelectSubscribe(Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.localcostmap.topic);
                    Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("onSelectRobotMap_monitor err=" + ex.Message.ToString());
                }
            }
        }

        int nwheelcont = 0;
        private void onMap_resize(object sender, MouseEventArgs e)
        {
            int lines = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            PictureBox pb = (PictureBox)sender;

            if (lines > 0)
            {
                ratio *= 1.1F;
                if (ratio > 100.0) ratio = 100.0;
            }
            else if (lines < 0)
            {
                ratio *= 0.9F;
                if (ratio < 1) ratio = 1;
            }

            Bitmap bmSource = new Bitmap(nSourceMapWidth, nSourceMapHeight, PixelFormat.Format32bppRgb);//, PixelFormat.Format8bppIndexed);

            Map_Robot_Image_Processing2(ref bmSource, bmSource.Width, bmSource.Height, sourceMapValues, "gray");

            /* Image bmp = ImageFromRawBgraArray(sourceMapValues, nSourceMapWidth, nSourceMapHeight);

             imgRect.Width = (int)Math.Round(bmp.Width * ratio);
             imgRect.Height = (int)Math.Round(bmp.Height * ratio);
             imgRect.X = (int)Math.Round(bmp.Width / 2 - imgPoint.X * ratio);
             imgRect.Y = (int)Math.Round(bmp.Height / 2 - imgPoint.Y * ratio);
             pb_map.Image = ZoomIn(bmp, ratio);
             pb_map.Invalidate();
             */


            pb_map.Image = ZoomIn(bmSource, ratio);
            pb_map.Invalidate();

            textBox1.Text = string.Format("{0},{1}", e.Delta.ToString(), ratio);

        }


        private void onGlobalcostMap_resize(object sender, MouseEventArgs e)
        {
            int lines = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            PictureBox pb = (PictureBox)sender;

            if (lines > 0)
            {
                globalcost_ratio *= 1.1F;
                if (globalcost_ratio > 100.0) globalcost_ratio = 100.0;
            }
            else if (lines < 0)
            {
                globalcost_ratio *= 0.9F;
                if (globalcost_ratio < 1) globalcost_ratio = 1;
            }

            Bitmap bmSource = new Bitmap(nSourceGlobalcostMapWidth, nSourceGlobalcostMapHeight, PixelFormat.Format32bppRgb);//, PixelFormat.Format8bppIndexed);

            Map_Robot_Image_Processing2(ref bmSource, bmSource.Width, bmSource.Height, sourceGlobalcostMapValues, "yellow");

            pb_Globalcost.Image = ZoomIn(bmSource, globalcost_ratio);
            pb_Globalcost.Invalidate();



        }
        #region Zoom Out/In
        Image ZoomIn(Image img, int nresolution)
        {

            Bitmap bmp = new Bitmap(img, img.Width * nresolution, img.Height * nresolution);
            bmp.SetResolution(bmp.VerticalResolution * nresolution, bmp.HorizontalResolution * nresolution);
            Graphics g = Graphics.FromImage(bmp);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bmp;
        }

        Image ZoomIn(Image img, double nresolution)
        {

            Bitmap bmp = new Bitmap(img, (int)(img.Width * nresolution), (int)(img.Height * nresolution));
            bmp.SetResolution((int)(bmp.VerticalResolution * nresolution), (int)(bmp.HorizontalResolution * nresolution));
            Graphics g = Graphics.FromImage(bmp);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bmp;
        }

        Image ZoomOut(Image img, int nresolutione)
        {

            Bitmap bmp = new Bitmap(img, img.Width / nresolutione, img.Height / nresolutione);
            Graphics g = Graphics.FromImage(bmp);
            bmp.SetResolution(bmp.VerticalResolution / nresolutione, bmp.HorizontalResolution / nresolutione);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bmp;
        }



        #endregion



        #region image test
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            Bitmap openBmp;
            Image openImg;

            openFile.ShowDialog();

            if (openFile.FileName.Length > 0)
            {
                openBmp = new Bitmap(openFile.FileName);
                openImg = Image.FromFile(openFile.FileName);

                pb_ori.Image = openImg;
                //pb_ori.Size = new Size(400, 400);
                pb_ori.Invalidate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pb_ori.Image == null)
                return;

            Bitmap bmSource = (Bitmap)(pb_ori.Image.Clone());

            Map_Image_Processing2(ref bmSource, bmSource.Width, bmSource.Height);

            pb_map.Image = (Bitmap)(bmSource.Clone());
            bmSource.Dispose();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            
            Image bmp = ImageFromRawBgraArray(sourceMapValues, nSourceMapWidth, nSourceMapHeight);
            pb_map.Image = ZoomIn(pb_map.Image, 2);
            //  bfValue = tB_zoom.Value;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Image bmp = ImageFromRawBgraArray(sourceMapValues, nSourceMapWidth, nSourceMapHeight);

            pb_map.Image = ZoomOut(pb_map.Image, 2);
            //  bfValue = tB_zoom.Value;
        }




        public void updateDP(string strtopic, string msg, string strcnt)
        {
            onListmsg(string.Format("topic={0}..data={1}", strtopic, msg));
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
     
       

        
   

        private void button7_Click(object sender, EventArgs e)
        {
            

          /*  int width = 0;
            int height = 0;
            double resolution = 0;
           
            byte[] rgbValues = new byte[Width * Height];
            sourceGlobalcostMapValues = new byte[Width * Height];
            for (int i = 0; i < width * height; i++)
            {
                rgbValues[i] = (byte)(sourceMapValues[i] | sourceGlobalcostMapValues[i]);
            }
            Image bmp = ImageFromRawBgraArray(rgbValues, nSourceMapWidth, nSourceMapHeight);

           
           pictureBox1.Image = bmp;*/
        }

        private void pb_map_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                clickPoint = new Point(e.X, e.Y);
            }
            pb_map.Invalidate();
        }

        private void pb_map_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                imgRect.X = imgRect.X + (int)Math.Round((double)(e.X - clickPoint.X) / 5);
                if (imgRect.X >= 0) imgRect.X = 0;
                if (Math.Abs(imgRect.X) >= Math.Abs(imgRect.Width - pb_map.Width)) imgRect.X = -(imgRect.Width - pb_map.Width);
                imgRect.Y = imgRect.Y + (int)Math.Round((double)(e.Y - clickPoint.Y) / 5);
                if (imgRect.Y >= 0) imgRect.Y = 0;
                if (Math.Abs(imgRect.Y) >= Math.Abs(imgRect.Height - pb_map.Height)) imgRect.Y = -(imgRect.Height - pb_map.Height);
            }
            else
            {
                LastPoint = e.Location;
            }

            pb_map.Invalidate();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)pb_map.Image;
            bmp.Save("D:\\output.bmp", ImageFormat.Bmp);
        }

       
        private void Map_Image_Processing(ref Bitmap bmSource, int Width, int Height)
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
                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, Width * Height * 4);
            }
            else
            {
                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, Width * Height);
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


            ////
            //// 여기까지는 3 x 3 Average Filter입니다
            ////
            double fCount;
            double Lmt_Val = 100, Col_Val, tmp_Val = 0;

            int stx = -3;
            int etx = 3;
            int cnt_sel = -stx * etx;

            for (int i = etx; i < Height + stx; i += etx - stx)   // loop for the image pixels height
            {
                for (int j = etx; j < Width + stx; j += etx - stx) // loop for image pixels width    
                {
                    fCount = 0;
                    for (int y = stx; y < etx; y++)
                    {
                        for (int x = stx; x < etx; x++)
                        {
                            Col_Val = Source[j + x, i + y];
                            if (Col_Val < Lmt_Val) fCount++;
                        }
                    }
                    for (int y = stx; y < etx - 2; y++)
                    {
                        for (int x = stx; x < etx - 2; x++)
                        {
                            if (fCount > cnt_sel / 2)
                                Target[j + x, i + y] = 80;
                            else if (fCount > cnt_sel / 4)
                                Target[j + x, i + y] = 120;
                            else if (fCount > cnt_sel / 7)
                                Target[j + x, i + y] = 160;
                            else if (fCount > cnt_sel / 10)
                                Target[j + x, i + y] = 180;
                            else
                            {
                                tmp_Val = Source[j, i];
                                Target[j + x, i + y] = tmp_Val;
                            }
                        }
                    }
                }
            }

            //격자-5cm
            for (int i = 5; i < Height - 5; i += 10)   // loop for the image pixels height
            {
                for (int j = 5; j < Width - 5; j += 10) // loop for image pixels width    
                {

                    if (Source[j, i] > 240)
                    {
                        for (int x = -5; x < 5; x++)
                        {
                            Target[j + x, i] = 200;
                        }
                        for (int y = -5; y < 5; y++)
                        {
                            Target[j, i + y] = 200;
                        }
                    }
                }
            }

            //
            // 여기서 부터는 2차원 배열을 다시 1차원 버터로 옮기는 부분입니다
            //
            YPos = 0;
            if (bmSource.PixelFormat == PixelFormat.Format32bppArgb || bmSource.PixelFormat == PixelFormat.Format32bppRgb)
            {
                for (int nH = 0; nH < Height; nH++)
                {
                    XPos = 0;
                    for (int nW = 0; nW < Width; nW++)
                    {
                        rgbValues[XPos + YPos] = (byte)Target[nW, nH];
                        rgbValues[XPos + YPos + 1] = (byte)Target[nW, nH];
                        rgbValues[XPos + YPos + 2] = (byte)Target[nW, nH];
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
                        rgbValues[XPos + YPos] = (byte)Target[nW, nH];
                        XPos++;
                    }
                    YPos += Width;
                }
            }

            //
            // 여기까지는 2차원 배열을 1차원 버터로 옮기는 부분입니다
            //

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

            System.Drawing.Rectangle cropArea = new System.Drawing.Rectangle(6, 6, Width - 12, Height - 12);
            Bitmap bmpTemp = bmSource.Clone(cropArea, bmSource.PixelFormat);
            bmSource.Dispose();
            bmSource = null;
            bmSource = (Bitmap)(bmpTemp.Clone());

        }
        private void Map_Image_Processing2(ref Bitmap bmSource, int Width, int Height)
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
            else if (bmSource.PixelFormat == PixelFormat.Format24bppRgb)
            {
                rgbValues = new byte[Width * Height * 3];
            }
            else
            {
                rgbValues = new byte[Width * Height];
            }

            if (bmSource.PixelFormat == PixelFormat.Format32bppArgb || bmSource.PixelFormat == PixelFormat.Format32bppRgb)
            {
                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, Width * Height * 4);
            }
            else if (bmSource.PixelFormat == PixelFormat.Format24bppRgb)
            {
                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, Width * Height * 3);
            }
            else
            {
                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, Width * Height);
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
                    for (int nW = 0; nW < Width; nW++)
                    {
                        Source[nW, nH] = rgbValues[XPos + YPos];
                        Target[nW, nH] = rgbValues[XPos + YPos];
                        XPos += 4;
                    }
                    YPos += Width * 4;
                }
            }
            if (bmSource.PixelFormat == PixelFormat.Format24bppRgb)
            {
                for (int nH = 0; nH < Height; nH++)
                {
                    XPos = 0;
                    for (int nW = 0; nW < Width; nW++)
                    {
                        Source[nW, nH] = rgbValues[XPos + YPos];
                        Target[nW, nH] = rgbValues[XPos + YPos];
                        XPos += 3;
                    }
                    YPos += Width * 3;
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



            //
            // 여기서 부터는 2차원 배열을 다시 1차원 버터로 옮기는 부분입니다
            //
            YPos = 0;
            if (bmSource.PixelFormat == PixelFormat.Format32bppArgb || bmSource.PixelFormat == PixelFormat.Format32bppRgb)
            {
                for (int nH = 0; nH < Height; nH++)
                {
                    XPos = 0;
                    for (int nW = 0; nW < Width; nW++)
                    {
                        rgbValues[XPos + YPos] = (byte)Target[nW, nH];
                        rgbValues[XPos + YPos + 1] = (byte)Target[nW, nH];
                        rgbValues[XPos + YPos + 2] = (byte)Target[nW, nH];
                        XPos += 4;
                    }
                    YPos += Width * 4;
                }
            }
            if (bmSource.PixelFormat == PixelFormat.Format24bppRgb)
            {
                for (int nH = 0; nH < Height; nH++)
                {
                    XPos = 0;
                    for (int nW = 0; nW < Width; nW++)
                    {
                        rgbValues[XPos + YPos] = (byte)Target[nW, nH];
                        rgbValues[XPos + YPos + 1] = (byte)Target[nW, nH];
                        rgbValues[XPos + YPos + 2] = (byte)Target[nW, nH];
                        XPos += 3;
                    }
                    YPos += Width * 3;
                }
            }
            else
            {
                for (int nH = 0; nH < Height; nH++)
                {
                    XPos = 0;
                    for (int nW = 0; nW < Width; nW++)
                    {
                        rgbValues[XPos + YPos] = (byte)Target[nW, nH];
                        XPos++;
                    }
                    YPos += Width;
                }
            }

            //
            // 여기까지는 2차원 배열을 1차원 버터로 옮기는 부분입니다
            //

            //
            // 다시 Marshal Copy로 Picture Box로 옮기는 부분입니다
            //
            if (bmSource.PixelFormat == PixelFormat.Format32bppArgb || bmSource.PixelFormat == PixelFormat.Format32bppRgb)
            {
                System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, Width * Height * 4);
            }
            else if (bmSource.PixelFormat == PixelFormat.Format24bppRgb)
            {
                System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, Width * Height * 3);
            }
            else
            {
                System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, Width * Height);
            }

            bmSource.UnlockBits(bmpData);

            System.Drawing.Rectangle cropArea = new System.Drawing.Rectangle(6, 6, Width - 12, Height - 12);
            Bitmap bmpTemp = bmSource.Clone(cropArea, bmSource.PixelFormat);
            bmSource.Dispose();
            bmSource = null;
            bmSource = (Bitmap)(bmpTemp.Clone());

        }
        public Image ImageFromRawBgraArray(byte[] arr, int width, int height)
        {
            Bitmap output = new Bitmap(width, height, PixelFormat.Format32bppRgb);
            output.SetResolution(width, height);
            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = output.LockBits(rect, ImageLockMode.ReadWrite, output.PixelFormat);
            int bpp = Image.GetPixelFormatSize(output.PixelFormat) / 8;
            byte[] newArr = new byte[width * height * bpp];
            var arrRowLength = width * bpp;
            var ptr = bmpData.Scan0;

            var k = 0;
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    newArr[k] = arr[y * width + x];
                    newArr[k + 1] = arr[y * width + x];
                    newArr[k + 2] = arr[y * width + x];
                    k += bpp;
                }
            }

            for (var i = 0; i < height; i++)
            {
                Marshal.Copy(newArr, i * arrRowLength, ptr, arrRowLength);
                ptr += bmpData.Stride;
            }
            output.UnlockBits(bmpData);
            return output;
        }
        public Bitmap MakeGrayscale3(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            System.Drawing.Imaging.ColorMatrix colorMatrix = new System.Drawing.Imaging.ColorMatrix(
                new float[][]
                {
            new float[] {.3f, .3f, .3f, 0, 0},
            new float[] {.59f, .59f, .59f, 0, 0},
            new float[] {.11f, .11f, .11f, 0, 0},
            new float[] {0, 0, 0, 1, 0},
            new float[] {0, 0, 0, 0, 1}
                });

            //create some image attributes
            System.Drawing.Imaging.ImageAttributes attributes = new System.Drawing.Imaging.ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }



        #endregion

        
    }
}
