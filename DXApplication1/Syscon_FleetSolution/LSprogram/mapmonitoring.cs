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
        private float Wheelratio = (float)0.33;
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
            Wheelratio = (float)0.33;
            clickPoint = imgPoint;
        }

        private void picturebox_Wheel(object sender, MouseEventArgs e)
        {
            int lines = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            PictureBox pb = (PictureBox)sender;
            /*
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
            */
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

        float resolution;

        MySqlConnection conn;
        MapState mapstate = new MapState();
        MapInfo mapinfo = new MapInfo();

        Bitmap image_resize;
        private void connSql()
        {
            //string strConn = "Server=192.168.20.28;Database=ridis_db;Uid=syscon;Pwd=r023866677!";
            //conn = new MySqlConnection(strConn);
            //conn.Open();
            selectMap();
        }
        private void selectMap()
        {

            //DataSet ds = new DataSet();
            //string sql = "select * from map_t order by idx DESC limit 1";
            //MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
            //string temp = "";
            //adapter.Fill(ds, "map_t");
            //if (ds.Tables.Count > 0)
            //{
            //    foreach (DataRow r in ds.Tables[0].Rows)
            //    {
            //        mapstate = JsonConvert.DeserializeObject<MapState>(r["map_data"].ToString());
            //    }
            //}
            width = 3727;
            height = 3475;
            resolution = (float)0.019999999552965164;
            ori_x = (float)-10.2549853515625;
            ori_y = (float)-19.7270654296875;
            //Size sz = pictureBox1.Size;
            //if (sz.Width > width)
            //{
            //    float tmpratio_w = (float)(sz.Width) / width;
            //    float tmpratio_h = 0;
            //    if (sz.Height > height)
            //    {
            //        tmpratio_h = (float)(sz.Height) / height;
            //    }
            //    if (tmpratio_w > tmpratio_h)
            //        Wheelratio = tmpratio_h;
            //    else Wheelratio = tmpratio_w;

            //}
            //width = 1515;
            //height = 823;

            //resolution = (float)0.02;
            //ori_x = (float)-7.15;
            //ori_y = (float)-9.56932;
            picturebox_paint();
            //conn.Close();
        }
        private void picturebox_paint()
        {
            try
            {
                dOrignX = ((ori_x * -1) / resolution);
                dOrignY = ((ori_y) / resolution);

                if (dOrignY < 0) dOrignY *= -1;
                dOrignY = img.Height - dOrignY;

                dOrignX = dOrignX * (float)Wheelratio;
                dOrignY = dOrignY * (float)Wheelratio;
                Bitmap sourceBMP = new Bitmap(img, img.Width, img.Height);

                Image image_resize;
                image_resize = ZoomIn(sourceBMP, (float)Wheelratio);

                Bitmap displayBMP = new Bitmap(image_resize.Width, image_resize.Height);
                displayBMP.SetResolution(image_resize.HorizontalResolution, image_resize.VerticalResolution);
                Graphics g = Graphics.FromImage(displayBMP);
                g.DrawImage(image_resize, new PointF(0, 0));
                Pen pen4 = new Pen(Brushes.Red, 5);
                pen4.DashStyle = DashStyle.Dot;
                if (pictureBox1.Image != null)
                {

                    if (Data.Instance.isConnected)
                    {
                        if (Data.Instance.robot_liveinfo.robotinfo.msg != null)
                        {
                            for (int i = 0; i < Data.Instance.robot_liveinfo.robotinfo.msg.robolist.Count; i++)
                            {
                                if (Data.Instance.robot_liveinfo.robotinfo.msg.robolist.Count > 0)
                                {
                                    float robotx = (float)Data.Instance.robot_liveinfo.robotinfo.msg.robolist[i].pose.x;
                                    float roboty = (float)Data.Instance.robot_liveinfo.robotinfo.msg.robolist[i].pose.y;
                                    float cellX = (float)(robotx / resolution);
                                    float cellY = (float)(roboty / resolution);

                                    PointF pos = new PointF();
                                    pos.X = dOrignX + cellX * (float)Wheelratio;
                                    pos.Y = dOrignY - cellY * (float)Wheelratio;
                                    Pen pen_robot = new Pen(Brushes.BlueViolet, 3);
                                    pen_robot.DashStyle = DashStyle.Solid;

                                    g.DrawEllipse(pen_robot, pos.X - 10, pos.Y - 10, 20, 20);
                                    g.DrawString(string.Format("{0}", Data.Instance.robot_liveinfo.robotinfo.msg.robolist[i].RID), new Font("고딕체", 30), Brushes.Black, pos.X - 20, pos.Y + 10);
                                    pen_robot.Dispose();
                                }
                            }
                        }
                    }

                }
                Pen bb = new Pen(Brushes.Red, 4);

                if (aa.Count > 0)
                {
                    for (int i = 0; i < save.Count; i++)
                    {
                        

                        g.DrawEllipse(bb, aa[i].X-1, aa[i].Y-1, 2, 2);
                    }
                }
                if (mainform.node_area_.Count() > 0)
                {

                    for (int i = 0; i < waringPointList.Count; i++)
                    {
                        WarningRegionClass wariningregion = new WarningRegionClass();
                        wariningregion = waringPointList[i];
                        float wx = wariningregion.nX1;
                        float wy = wariningregion.nY1;
                        float wx2 = wariningregion.nX2;
                        float wy2 = wariningregion.nY2;

                        float cellX = wx / resolution;
                        float cellY = wy / resolution;

                        float cellX2 = wx2 / resolution;
                        float cellY2 = wy2 / resolution;

                        PointF pos = new PointF();
                        pos.X = dOrignX + cellX * Wheelratio;
                        pos.Y = dOrignY - cellY * Wheelratio;

                        PointF pos2 = new PointF();
                        pos2.X = dOrignX + cellX2 * Wheelratio;
                        pos2.Y = dOrignY - cellY2 * Wheelratio;


                        g.DrawRectangle(pen4, pos.X, pos.Y, pos2.X - pos.X, pos2.Y - pos.Y);

                    }
                }

                pictureBox1.Image = displayBMP;
                if (pictureBox1.Image != null)
                {
                    sourceBMP.Dispose();
                    g.Dispose();
                    pen4.Dispose();
                }

            }
            catch
            {

            }
        }
        private void onDisplay()
        {
            //dOrignX = ((ori_x * -1) / resolution);
            //dOrignY = ((ori_y) / resolution);

            //if (dOrignY < 0) dOrignY *= -1;
            //dOrignY = img.Height - dOrignY;

            //dOrignX = dOrignX * (float)Wheelratio;
            //dOrignY = dOrignY * (float)Wheelratio;
            //test_2();
            picturebox_paint();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Interval = 500;
            timer1.Enabled = true;
        }
        bool bMapMouseDN = false;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            bMapMouseDN = true;
            W_x_1 = e.X;
            W_y_1 = e.Y;
        }

        List<Rectangle> test = new List<Rectangle>();
        List<WarningRegionClass> waringPointList = new List<WarningRegionClass>();
        Rectangle RectTmp = new Rectangle();
        List<RectangleF> waringRectList = new List<RectangleF>();
        float W_x_1;
        float W_y_1;
        float W_x_2;
        float W_y_2;

        public class WarningRegionClass
        {
            public float nX1;
            public float nY1;
            public float nX2;
            public float nY2;
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            int nx = e.X;
            int ny = e.Y;
            W_x_2 = e.X;
            W_y_2 = e.Y;
            Graphics g = pictureBox1.CreateGraphics();
            Pen pen = new Pen(Color.Red, 2);
            if (bMapMouseDN)
            {

                RectangleF rectT = RectTmp;
                //rectT.X = (float)((rectT.X / ratio) -translate_x);
                //rectT.Y = (float)((rectT.Y / ratio)-translate_y);

                rectT.X = (float)((rectT.X - 0) / Wheelratio);
                rectT.Y = (float)((rectT.Y - 0) / Wheelratio);

                rectT.Width = (float)(rectT.Width / Wheelratio);
                rectT.Height = (float)(rectT.Height / Wheelratio);
                waringRectList.Add(rectT);

                WarningRegionClass wariningregion = new WarningRegionClass();
                wariningregion.nX1 = (float)(((W_x_1 - dOrignX) * resolution)) / Wheelratio;
                wariningregion.nY1 = (float)(((W_y_1 - dOrignY) * resolution)) / Wheelratio * -1;
                wariningregion.nX2 = (float)(((W_x_2 - dOrignX) * resolution)) / Wheelratio;
                wariningregion.nY2 = (float)(((W_y_2 - dOrignY) * resolution)) / Wheelratio * -1;

                waringPointList.Add(wariningregion);

                picturebox_paint();
                g.Dispose();
                pen.Dispose();

            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int nx = e.X;
            int ny = e.Y;

            if (bMapMouseDN)
            {
                Rectangle rect = new Rectangle();
                rect.X = (int)(W_x_1);
                rect.Y = (int)(W_y_1);
                rect.Width = (int)((nx - (int)W_x_1));
                rect.Height = (int)((ny - (int)W_y_1));

                RectTmp = rect;

            }
        }
        private void save_conv()
        {
            try
            {
                for(int i=0;i<save.Count;i++)
                {

                    string strJson = JsonConvert.SerializeObject(save[i]);
                    mainform.dbBridge.onDBAtcconv_insert(strJson);
                }
            }
            catch
            {

            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (waringPointList.Count > 0)
                {
                    for (int i = 0; i < waringPointList.Count; i++)
                    {
                        WarningRegionClass wariningregion = new WarningRegionClass();

                        string strJson = JsonConvert.SerializeObject(waringPointList[i]);

                        mainform.dbBridge.onDBnodearea_Insert(strJson);
                    }
                }
            }
            catch
            {

            }
        }


        List<PointF> aa = new List<PointF>();
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {

            tempDlg tempdlg = new tempDlg();
            PointF temp = new PointF();
            int tx = e.X;
            int ty = e.Y;

            temp.X = tx;
            temp.Y = ty;
            aa.Add(temp);

            Graphics g = pictureBox1.CreateGraphics();



            float currXpos = (float)(((e.X - dOrignX) * resolution)) / Wheelratio;
            float currYpos = (float)(((e.Y - dOrignY) * resolution)) / Wheelratio;
            temp.X = currXpos;
            temp.Y = currYpos;

            string temp_name = "";
            ATC_ atc_temp = new ATC_();
            atc_temp.pointf.X = temp.X;
            atc_temp.pointf.Y = temp.Y;

            if (tempdlg.ShowDialog() == DialogResult.OK)
            {
                temp_name = tempdlg.atc_name;
            }
            atc_temp.atc_name = temp_name;
            save.Add(atc_temp);

            picturebox_paint();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                waringPointList.RemoveAt(waringPointList.Count - 1);
                picturebox_paint();
            }
            catch
            {
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            Pen pen4 = new Pen(Color.Red, 2);
            for (int i = 0; i < mainform.node_area_.Count(); i++)
            {

                //RectangleF wariningregion = new RectangleF();
                //wariningregion = mainform.node_area_[i];
                //float wx = wariningregion.X;
                //float wy = wariningregion.Y;
                //float wx2 = wariningregion.Width;
                //float wy2 = wariningregion.Height;

                //float cellX = wx / resolution;
                //float cellY = wy / resolution;

                //float cellX2 = wx2 / resolution;
                //float cellY2 = wy2 / resolution;

                //PointF pos = new PointF();
                //pos.X = dOrignX + cellX * Wheelratio;
                //pos.Y = dOrignY - cellY * Wheelratio;

                //PointF pos2 = new PointF();
                //pos2.X = dOrignX + cellX2 * Wheelratio;
                //pos2.Y = dOrignY - cellY2 * Wheelratio;

                g.DrawRectangle(pen4, mainform.node_area_[i].X, mainform.node_area_[i].Y, mainform.node_area_[i].Width, mainform.node_area_[i].Height);
            }
            pen4.Dispose();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        class ATC_
        {
            public PointF pointf;
            public string atc_name;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            save_conv();
        }

        List<  ATC_> save = new List<ATC_>();

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
