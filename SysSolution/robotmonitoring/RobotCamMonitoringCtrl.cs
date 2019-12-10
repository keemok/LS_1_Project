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
    public partial class RobotCamMonitoringCtrl : UserControl
    {
        Frm.RobotStatusFrm mainForm2;
        public string m_strRobotName = "";
        string m_strCurrentRobotDetailInfo_ID = "";
        public bool m_bLive = false;

        public RobotCamMonitoringCtrl()
        {
            InitializeComponent();
        }

        public RobotCamMonitoringCtrl(Frm.RobotStatusFrm mfrm)
        {
            mainForm2 = mfrm;
            InitializeComponent();
        }

        public void onInitSet()
        {
            // this.DoubleBuffered = true;

            if (Data.Instance.isConnected)
            {

                m_strCurrentRobotDetailInfo_ID = m_strRobotName;

                cboRobotID.SelectedIndex = 0;
            }
        }

        private void RobotCamMonitoringCtrl_Load(object sender, EventArgs e)
        {

        }
        byte[] sourceMapValues;
        byte[] sourceMapValues2;
        int nSourceMapWidth = 0;
        int nSourceMapHeight = 0;
        private void onCamMonitoring()
        {
            if (Data.Instance.Robot_work_info[strRobotID].robot_status_info.cam2 == null) return;

            CamInformation cam = Data.Instance.Robot_work_info[strRobotID].robot_status_info.cam2;

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
            if (Data.Instance.Robot_work_info[strRobotID].robot_status_info.cam1 == null) return;

            CamInformation cam = Data.Instance.Robot_work_info[strRobotID].robot_status_info.cam1;

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
        string strRobotID = "";
        private void button1_Click(object sender, EventArgs e)
        {
            if(timer1.Enabled)
            {
                timer1.Enabled = false;
            }

            strRobotID = cboRobotID.SelectedItem.ToString();
            //onCamMonitoring();
            timer1.Interval = 100;
            timer1.Enabled = true;
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            onCam1Monitoring();
            onCamMonitoring();
        }
    }
}
