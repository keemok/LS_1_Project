using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Syscon_Solution.LSprogram
{
    public partial class mapmonitoring : Form
    {
        public mapmonitoring()
        {
            InitializeComponent();
        }
        mainForm mainform;
        private float Wheelratio;
        private Point clickPoint;
        private Point imgPoint;
        private Rectangle imgRect;
        public mapmonitoring(mainForm frm)
        {
            InitializeComponent();
            mainform = frm;
        }
        public void oninit()
        {
            pictureBox1.MouseWheel += new MouseEventHandler(picturebox_Wheel);
            imgPoint = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);
            imgRect = new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height);
            Wheelratio = 1;
            clickPoint = imgPoint;
        }

        private void picturebox_Wheel(object sender, MouseEventArgs e)
        {
            int lines = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            PictureBox pb = (PictureBox)sender;

            if (lines > 0)
            {
                Wheelratio *= 1.1F;
                if (Wheelratio > 100.0) Wheelratio = (float)100.0;
            }
            else if (lines < 0)
            {
                Wheelratio *= 0.9F;
                //if (Wheelratio < 1) Wheelratio = 1;
            }

            //imgRect.Width = (int)Math.Round(imgRect.Width * ratio);
            //imgRect.Height = (int)Math.Round(imgRect.Height * ratio);
            //imgRect.X = (int)Math.Round(pb.Width / 2 - imgPoint.X * ratio);
            //imgRect.Y = (int)Math.Round(pb.Height / 2 - imgPoint.Y * ratio);

            imgRect.Width = (int)Math.Round(img.Width * Wheelratio);
            imgRect.Height = (int)Math.Round(img.Height * Wheelratio);
            imgRect.X = 0;
            imgRect.Y = 0;

            onDisplay();
            //pb_map.Invalidate();
        }

        float width, height;
        Bitmap img;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = @"F:\MAP";
            string image_file = string.Empty;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image_file = dialog.FileName;
            }
            else if (dialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            img = new Bitmap(Bitmap.FromFile(image_file));
            width = img.Width;
            height = img.Height;

            //test_2();
            connSql();
        }
        Image ZoomIn(Image img, double nresolution)
        {

            Bitmap bmp = new Bitmap(img, (int)(img.Width * nresolution), (int)(img.Height * nresolution));
            bmp.SetResolution((int)(bmp.VerticalResolution * nresolution), (int)(bmp.HorizontalResolution * nresolution));
            Graphics g = Graphics.FromImage(bmp);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bmp;
        }


        float dOrignX = 0;
        float dOrignY = 0;
        int x, y;
        float ori_x;
        float ori_y;

        float resolution = (float)0.05;

        MySqlConnection conn;
        MapState mapstate = new MapState();
        MapInfo mapinfo = new MapInfo();

        Bitmap image_resize;
        private void connSql()
        {
            string strConn = "Server=192.168.20.28;Database=ridis_db;Uid=syscon;Pwd=r023866677!";
            conn = new MySqlConnection(strConn);
            conn.Open();
            selectMap();
        }
        private void selectMap()
        {

            DataSet ds = new DataSet();
            string sql = "select * from map_t order by idx DESC limit 1";
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
            string temp = "";
            adapter.Fill(ds, "map_t");
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    mapstate = JsonConvert.DeserializeObject<MapState>(r["map_data"].ToString());
                }
            }
            width = mapstate.info.width;
            height = mapstate.info.height;
            resolution = (float)mapstate.info.resolution;
            ori_x = (float)mapstate.info.origin.position.x;
            ori_y = (float)mapstate.info.origin.position.y;
            onDisplay();
            conn.Close();
        }
        private void picturebox_paint()
        {
            try
            {
                Bitmap sourceBMP = new Bitmap(img,img.Width, img.Height);

                Image image_resize;
                image_resize = ZoomIn(sourceBMP, (float)Wheelratio);

                Bitmap displayBMP = new Bitmap(image_resize.Width, image_resize.Height);
                displayBMP.SetResolution(image_resize.HorizontalResolution, image_resize.VerticalResolution);
                Graphics g = Graphics.FromImage(displayBMP);
                g.DrawImage(image_resize, new PointF(0, 0));
                if (pictureBox1.Image != null)
                {
                    foreach (KeyValuePair<string, Robot_RegInfo> info in Data.Instance.Robot_RegInfo_list)
                    {
                        string strrobotid = info.Key;

                        //    for (int idx = 0; idx < mainform.G_robotList.Count; idx++)
                        // {
                        if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate != null)
                        {
                            if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate.msg != null)
                            {
                                float robotx = (float)Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate.msg.pose.x;
                                float roboty = (float)Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate.msg.pose.y;

                                float cellX = (float)(robotx / resolution);
                                float cellY = (float)(roboty / resolution);

                                PointF pos = new PointF();
                                pos.X = dOrignX + cellX * (float)Wheelratio;
                                pos.Y = dOrignY - cellY * (float)Wheelratio;
                                Pen pen_robot = new Pen(Brushes.BlueViolet, 3);
                                pen_robot.DashStyle = DashStyle.Solid;

                                g.DrawEllipse(pen_robot, pos.X - 10, pos.Y - 10, 20, 20);
                                g.DrawString(string.Format("{0}", strrobotid), new Font("고딕체", 5), Brushes.Black, pos.X - 10, pos.Y - 20);
                                pen_robot.Dispose();
                            }
                        }
                    }
                }
                pictureBox1.Image = displayBMP;
                if(pictureBox1.Image !=null)
                {
                    sourceBMP.Dispose();
                    g.Dispose();
                }
                
            }
            catch
            {

            }
        }
        private void onDisplay()
        {
            dOrignX = ((ori_x * -1) / resolution);
            dOrignY = ((ori_y) / resolution);

            if (dOrignY < 0) dOrignY *= -1;
            dOrignY = img.Height - dOrignY;

            dOrignX = dOrignX * (float)Wheelratio;
            dOrignY = dOrignY * (float)Wheelratio;
            //test_2();
            picturebox_paint();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Interval = 500;
            timer1.Enabled = true;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            picturebox_paint();
        }
        /*
        private void locationDraw()
        {
            Graphics g = pictureBox1.CreateGraphics();
            foreach (KeyValuePair<string, Robot_RegInfo> info in Data.Instance.Robot_RegInfo_list)
            {
                string strrobotid = info.Key;

                //    for (int idx = 0; idx < mainform.G_robotList.Count; idx++)
                // {
                if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate != null)
                {
                    if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate.msg != null)
                    {
                        float robotx = (float)Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate.msg.pose.x;
                        float roboty = (float)Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate.msg.pose.y;

                        float cellX = (float)(robotx / resolution);
                        float cellY = (float)(roboty / resolution);

                        PointF pos = new PointF();
                        pos.X = dOrignX + cellX * (float)Wheelratio;
                        pos.Y = dOrignY - cellY * (float)Wheelratio;
                        Pen pen_robot = new Pen(Brushes.BlueViolet, 3);
                        pen_robot.DashStyle = DashStyle.Solid;

                        g.DrawEllipse(pen_robot, pos.X - 10, pos.Y - 10, 20, 20);
                        g.DrawString(string.Format("{0}", strrobotid), new Font("고딕체", 5), Brushes.Black, pos.X - 10, pos.Y - 20);

                    }
                }
            }
        }
        */
    }
}
