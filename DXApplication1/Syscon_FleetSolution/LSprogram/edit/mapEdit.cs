using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Drawing.Drawing2D;
using Newtonsoft.Json;

namespace Syscon_Solution.LSprogram.edit
{
    public partial class mapEdit : UserControl
    {
        LSprogram.mainForm mainform;
        public mapEdit(mainForm frm)
        {
            mainform = frm;
            InitializeComponent();
        }
        public mapEdit()
        {
            InitializeComponent();
        }


        private float Wheelratio;
        private Point clickPoint;
        private Point imgPoint;
        private Rectangle imgRect;
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

            try
            {
                if (pictureBox1.Image != null)
                {
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

                    imgRect.Width = (int)Math.Round(img.Width * Wheelratio);
                    imgRect.Height = (int)Math.Round(img.Height * Wheelratio);
                    imgRect.X = 0;
                    imgRect.Y = 0;

                    onDisplay();
                }
                else
                {
                    return;
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
        bool bCautiondraw = false;
        float dOrignX = 0;
        float dOrignY = 0;
        float resoultion = 0;
        List<Point> polyPoint = new List<Point>();
        List<Point> polyPointcp = new List<Point>();
        Polygon pg = new Polygon();
        Rectangle rect = new Rectangle();
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int nx = e.X;
            int ny = e.Y;
            mouseClick_point temppoint = new mouseClick_point();

            try
            {
                if (bCautiondraw == true)
                {
                    float currXpos = (float)(((nx - dOrignX) * resoultion)) / Wheelratio;
                    float currYpos = (float)(((ny - dOrignY) * resoultion)) / Wheelratio;

                    //PresentPoint.Add(new Point(nx, ny));
                    polyPoint.Add(new Point(nx, ny));

                    temppoint.x = currXpos;
                    temppoint.y = currYpos;
                    temppoint.z = 0;

                    pg.point.Add(temppoint);

                    Graphics graphics = pictureBox1.CreateGraphics();
                    Pen redpen = new Pen(Color.Red, 2);
                    Rectangle rect = new Rectangle(nx, ny, 4, 4);
                    graphics.DrawEllipse(redpen, rect);
                    graphics.Dispose();

                    float tmpOriginX = dOrignX / Wheelratio;
                    float tmpOriginY = dOrignY / Wheelratio;
                }
                else
                {
                    return;
                }
            }
            catch
            {
                Console.WriteLine("caution area draw error");
            }
        }

        int x, y;
        float ori_x;
        float ori_y;

        float resolution = (float)0.05;
        MySqlConnection conn;
        MapState mapstate = new MapState();
        MapInfo mapinfo = new MapInfo();

        Bitmap image_resize;

        Image ZoomIn(Image img, double nresolution)
        {

            Bitmap bmp = new Bitmap(img, (int)(img.Width * nresolution), (int)(img.Height * nresolution));
            bmp.SetResolution((int)(bmp.VerticalResolution * nresolution), (int)(bmp.HorizontalResolution * nresolution));
            Graphics g = Graphics.FromImage(bmp);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bmp;
        }
        private void picturebox_paint()
        {
            try
            {
                Bitmap sourceBMP = new Bitmap(img, img.Width, img.Height);

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
                if (pictureBox1.Image != null)
                {
                    sourceBMP.Dispose();
                    g.Dispose();
                }

            }
            catch
            {

            }
        }
        private void cautionToggle_Toggled(object sender, EventArgs e)
        {
            if (cautionToggle.IsOn)
                bCautiondraw = true;
            else
                bCautiondraw = false;
        }

        float width, height;
        Bitmap img;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = @"F:\MAP";
            string image_file = string.Empty;
            if (dialog.ShowDialog() == DialogResult.OK)
                image_file = dialog.FileName;
            else if (dialog.ShowDialog() == DialogResult.Cancel)
                return;
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            img = new Bitmap(Bitmap.FromFile(image_file));
            width = img.Width;
            height = img.Height;

            connSql();
        }


        int areaIdx = 1;
        Area area = new Area();
        private void button2_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            CautionArea cautionarea = new CautionArea();
            Polygon polygontemp = new Polygon();
            string areaIdtemp = "";
            string descriptiontemp = "";
            polygontemp = pg;

            using (SolidBrush br = new SolidBrush(Color.FromArgb(100, Color.Yellow)))
            {
                try
                {

                    if (polyPoint.Count() > 0)
                        g.FillPolygon(br, polyPoint.ToArray());
                    else
                        return;
                }
                catch { }
            }
            g.DrawPolygon(Pens.DarkBlue, polyPoint.ToArray());

            foreach (Point p in polyPoint)
            {
                g.FillEllipse(Brushes.Red, new Rectangle(p.X - 2, p.Y - 2, 4, 4));
            }

            CautionAreaSaveDlg areasave = new CautionAreaSaveDlg();
            areasave.onInitSet(areaIdx);
            if (areasave.ShowDialog() == DialogResult.OK)
            {
                areaIdtemp = areasave.areaID;
                descriptiontemp = areasave.description;
            }



            cautionarea.area_id = areaIdtemp;
            cautionarea.area_description = descriptiontemp;
            cautionarea.polygon = pg;
            area.cautionarea.Add(cautionarea);



            polyPoint.Clear();
            pg = new Polygon();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string strPoint = JsonConvert.SerializeObject(area);
            mainform.dbBridge.onUpdate(strPoint);
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pg = null;
            picturebox_paint();
        }

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
    }
}
