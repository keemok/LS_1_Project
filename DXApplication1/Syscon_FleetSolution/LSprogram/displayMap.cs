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
using Newtonsoft.Json;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading;

namespace Syscon_Solution.LSprogram
{
    public partial class displayMap : UserControl
    {
        public displayMap()
        {
            InitializeComponent();
        }
        LSprogram.mainForm mainform;

        public displayMap(LSprogram.mainForm frm)
        {
            mainform = frm;
            InitializeComponent();
        }

        private double Wheelratio;
        private Point clickPoint;
        private Point imgPoint;
        private Rectangle imgRect;
        public void aboutMove()
        {
            pb_map.MouseWheel += new MouseEventHandler(picturebox_Wheel);
            imgPoint = new Point(pb_map.Width / 2, pb_map.Height / 2);
            imgRect = new Rectangle(0, 0, pb_map.Width, pb_map.Height);
            Wheelratio = 1;
            clickPoint = imgPoint;
        }



        #region MAP Variable



        private void picturebox_Wheel(object sender, MouseEventArgs e)
        {
            int lines = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            PictureBox pb = (PictureBox)sender;

            if (lines > 0)
            {
                Wheelratio *= 1.1F;
                if (Wheelratio > 100.0) Wheelratio = 100.0;
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
            imgRect.Width = (int)Math.Round(pb_map.Width * Wheelratio);
            imgRect.Height = (int)Math.Round(pb_map.Height * Wheelratio);
            imgRect.X = 0;
            imgRect.Y = 0;
            flag = true;


            pb_map_Paint(); // invalidate
            //pb_map.Invalidate();
        }
        int width;
        int height;
        float resoultion1=0.02F;
        float ori_x;
        float ori_y;
        float ratio = 10;
        float translate_x = 0;
        float translate_y = 0;
        float dOrignX = 0;
        float dOrignY = 0;
        int nSourceMapWidth = 0;
        int nSourceMapHeight = 0;


        bool flag = false;

        MapState mapstate = new MapState();
        MapInfo mapinfo = new MapInfo();
        byte[] sourceMapValues;
        private void saveMAP()
        {
            DataSet ds = new DataSet();
            string sql = "";
            MySqlCommand command = new MySqlCommand();

            MapInfo info = new MapInfo();
            MapState state = new MapState();
            byte[] tempByte;

        }
        private void loadMAP()
        {
            DataSet ds = new DataSet();
            string sql = "select * from map_t order by idx DESC limit 1";
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, Data.Instance.G_SqlCon);
            string temp = "";
            adapter.Fill(ds, "map_t");
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    //Console.WriteLine(r["map_data"]);
                    mapstate = JsonConvert.DeserializeObject<MapState>(r["map_data"].ToString());
                    mapinfo = JsonConvert.DeserializeObject<MapInfo>(r["map_data"].ToString());
                    //Console.WriteLine(mapstate.data);
                }
            }

            sourceMapValues = mapstate.data.ToArray().Select(x => (byte)x).ToArray();
            for (var y = 0; y < width * height; y++)
            {
                sourceMapValues[y] = (byte)mapstate.data[y];

            }
        }
        public void MapInfoComplete()
        {
            try
            {
                nSourceMapWidth = width;
                nSourceMapHeight = height;


                Size sz = pb_map.Size;

                if (sz.Width > width)
                {
                    float tmpratio_w = (float)(sz.Width) / width;
                    float tmpratio_h = 0;
                    if (sz.Height > height)
                    {
                        tmpratio_h = (float)(sz.Height) / height;
                    }
                    if (tmpratio_w > tmpratio_h)
                        ratio = tmpratio_h;
                    else ratio = tmpratio_w;
                }
                onMapDisplay1();


            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("MapInfoComplete err :={0}", ex.Message.ToString());
            }
        }
        private void pb_map_Paint()
        {
            try
            {
                Pen pen = new Pen(Brushes.Red, 1);
                pen.DashStyle = DashStyle.Dash;

                Bitmap bmSource = new Bitmap(nSourceMapWidth, nSourceMapHeight, PixelFormat.Format32bppRgb);//, PixelFormat.Format8bppIndexed);

                Bitmap bmMergeOKSource = new Bitmap(nSourceMapWidth, nSourceMapHeight, PixelFormat.Format32bppRgb);//, PixelFormat.Format8bppIndexed);

                Map_Robot_Image_Processing2(ref bmSource, bmSource.Width, bmSource.Height, sourceMapValues, "gray");

                dOrignX = ((ori_x * -1) / resoultion1);
                dOrignY = ((ori_y) / resoultion1);

                if (dOrignY < 0) dOrignY *= -1;
                dOrignY = nSourceMapHeight - dOrignY;

                dOrignX = dOrignX * (float)Wheelratio + translate_x;
                dOrignY = dOrignY * (float)Wheelratio + translate_y;

                Image imgSource_Chg;
                imgSource_Chg = ZoomIn(bmSource, (float)Wheelratio);

                Bitmap translateBmp = new Bitmap(imgSource_Chg.Width, imgSource_Chg.Height);
                translateBmp.SetResolution(imgSource_Chg.HorizontalResolution, imgSource_Chg.VerticalResolution);

                Graphics g = Graphics.FromImage(translateBmp);
                g.TranslateTransform(translate_x, translate_y);
                g.DrawImage(imgSource_Chg, new PointF(0, 0));
                if (pb_map.Image != null)
                {
                    g.DrawLine(pen, (float)dOrignX - 10, (float)dOrignY, (float)dOrignX + 10, (float)dOrignY);
                    g.DrawLine(pen, (float)dOrignX, (float)dOrignY - 10, (float)dOrignX, (float)dOrignY + 10);
                    g.DrawEllipse(Pens.Red, dOrignX - 10, dOrignY - 10, 20, 20);
                    //e.Graphics.FillEllipse(Brushes.Blue, dOrignX - 10, dOrignY - 10, 20, 20);


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
                                float robottheta = (float)Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate.msg.pose.theta;

                                float cellX = robotx / resoultion1;
                                float cellY = roboty / resoultion1;

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
                    //WorkFlowGoal temp_action = new WorkFlowGoal();
                    //try
                    //{
                    //    foreach (KeyValuePair<string, Node_mission> mission in Data.Instance.node_mission_list)
                    //    {
                    //        temp_action = JsonConvert.DeserializeObject<WorkFlowGoal>(mission.Value.work);
                    //        float nodeX = temp_action.work[0].action_args[0];
                    //        float nodeY = temp_action.work[0].action_args[1];

                    //        float cellX = nodeX / resoultion1;
                    //        float cellY = nodeY / resoultion1;

                    //        PointF pos = new PointF();
                    //        pos.X = dOrignX + cellX * (float)Wheelratio;
                    //        pos.Y = dOrignY - cellY * (float)Wheelratio;

                    //        Pen node_pen = new Pen(Brushes.Aqua, 3);
                    //        node_pen.DashStyle = DashStyle.Solid;
                    //        g.DrawEllipse(node_pen, pos.X - 10, pos.Y - 10, 20, 20);
                    //        g.DrawString(string.Format("{0}", mission.Key), new Font("고딕체", 5), Brushes.Black, pos.X - 10, pos.Y - 20);

                    //    }
                    //}
                    //catch
                    //{

                    //}


                    Pen pen4 = new Pen(Brushes.Magenta, 2);
                    pen4.DashStyle = DashStyle.Dot;

                    pen4.Dispose();

                }
                pb_map.Image = translateBmp;

                //pb_map.Invalidate();


                bmSource.Dispose();
                bmMergeOKSource.Dispose();



            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onMapDisplay1 err :={0}", ex.Message.ToString());
            }
            finally
            {
            }
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (img == null)
                return;


            Pen pen = new Pen(Brushes.Red, 1);

            Bitmap test = new Bitmap(img);

            Image imgSource_Chg;
            imgSource_Chg = ZoomIn(test, (float)Wheelratio);

            dOrignX = ((ori_x * -1) / resoultion1);
            dOrignY = ((ori_y) / resoultion1);

            if (dOrignY < 0) dOrignY *= -1;
            dOrignY = nSourceMapHeight - dOrignY;

            dOrignX = dOrignX * (float)Wheelratio + translate_x;
            dOrignY = dOrignY * (float)Wheelratio + translate_y;

            Bitmap translateBmp = new Bitmap(imgSource_Chg.Width, imgSource_Chg.Height);
            translateBmp.SetResolution(imgSource_Chg.HorizontalResolution, imgSource_Chg.VerticalResolution);
            Graphics g = Graphics.FromImage(translateBmp);

            g.TranslateTransform(translate_x, translate_y);
            g.DrawImage(imgSource_Chg, new PointF(0, 0));
            //g.DrawImage(img, imgRect);

            if (testflag)
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                //g.DrawImage(img, imgRect);

                g.DrawLine(pen, (float)dOrignX - 10, (float)dOrignY, (float)dOrignX + 10, (float)dOrignY);
                g.DrawLine(pen, (float)dOrignX, (float)dOrignY - 10, (float)dOrignX, (float)dOrignY + 10);
                g.DrawEllipse(Pens.Blue, dOrignX - 10, dOrignY - 10, 20, 20);
                //e.Graphics.FillEllipse(Brushes.Blue, dOrignX - 10, dOrignY - 10, 20, 20);


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
                            float robottheta = (float)Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate.msg.pose.theta;

                            float cellX = robotx / resoultion1;
                            float cellY = roboty / resoultion1;

                            PointF pos = new PointF();
                            pos.X = dOrignX + cellX * (float)Wheelratio;
                            pos.Y = dOrignY - cellY * (float)Wheelratio;
                            Pen pen_robot = new Pen(Brushes.DeepPink, 3);
                            pen_robot.DashStyle = DashStyle.Solid;
                            g.DrawEllipse(pen_robot, pos.X - 10, pos.Y - 10, 20, 20);
                            g.DrawString(string.Format("{0}", strrobotid), new Font("고딕체", 5), Brushes.Black, pos.X - 10, pos.Y - 20);
                            
                        }
                    }
                }
                //WorkFlowGoal temp_action = new WorkFlowGoal();
                //try
                //{
                //    foreach (KeyValuePair<string, Node_mission> mission in Data.Instance.node_mission_list)
                //    {
                //        temp_action = JsonConvert.DeserializeObject<WorkFlowGoal>(mission.Value.work);
                //        float nodeX = temp_action.work[0].action_args[0];
                //        float nodeY = temp_action.work[0].action_args[1];

                //        float cellX = nodeX / resoultion1;
                //        float cellY = nodeY / resoultion1;

                //        PointF pos = new PointF();
                //        pos.X = dOrignX + cellX * (float)Wheelratio;
                //        pos.Y = dOrignY - cellY * (float)Wheelratio;

                //        Pen node_pen = new Pen(Brushes.Aqua, 3);
                //        node_pen.DashStyle = DashStyle.Solid;
                //        g.DrawEllipse(node_pen, pos.X - 10, pos.Y - 10, 20, 20);
                //        g.DrawString(string.Format("{0}", mission.Key), new Font("고딕체", 5), Brushes.Black, pos.X - 10, pos.Y - 20);

                //    }
                //}
                //catch
                //{

                //}
            }
            pb_map.Image = translateBmp;
            test.Dispose();
            translateBmp.Dispose();
        }
        public void pb_paint_test()
        {
            //Graphics g = pb_map.CreateGraphics();
            //g.DrawImage(img, imgRect);
        }
        /*private void pb_map_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Bitmap bmSource = new Bitmap(nSourceMapWidth, nSourceMapHeight, PixelFormat.Format32bppRgb);//, PixelFormat.Format8bppIndexed);

                Bitmap bmMergeOKSource = new Bitmap(nSourceMapWidth, nSourceMapHeight, PixelFormat.Format32bppRgb);//, PixelFormat.Format8bppIndexed);

                Map_Robot_Image_Processing2(ref bmSource, bmSource.Width, bmSource.Height, sourceMapValues, "gray");
                //Map_Robot_Image_Processing2(ref bmSource, bmSource.Width, bmSource.Height, sourceMapValues, "gray");

                dOrignX = ((ori_x * -1) / resoultion1);
                dOrignY = ((ori_y) / resoultion1);

                if (dOrignY < 0) dOrignY *= -1;
                dOrignY = nSourceMapHeight - dOrignY;

                dOrignX = dOrignX * (float)Wheelratio + translate_x;
                dOrignY = dOrignY * (float)Wheelratio + translate_y;

                Image imgSource_Chg;
                imgSource_Chg = ZoomIn(bmSource, (float)Wheelratio);

                Bitmap translateBmp = new Bitmap(imgSource_Chg.Width, imgSource_Chg.Height);
                translateBmp.SetResolution(imgSource_Chg.HorizontalResolution, imgSource_Chg.VerticalResolution);

                Graphics g = Graphics.FromImage(translateBmp);
                g.TranslateTransform(translate_x, translate_y);
                g.DrawImage(imgSource_Chg, new PointF(0, 0));
                if(pb_map.Image != null)
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
                                float robottheta = (float)Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate.msg.pose.theta;

                                float cellX = robotx / resoultion1;
                                float cellY = roboty / resoultion1;

                                PointF pos = new PointF();
                                pos.X = dOrignX + cellX * (float)Wheelratio;
                                pos.Y = dOrignY - cellY * (float)Wheelratio;
                                Pen pen_robot = new Pen(Brushes.BlueViolet, 3);
                                pen_robot.DashStyle = DashStyle.Solid;
                                g.DrawEllipse(pen_robot, pos.X - 10, pos.Y - 10, 20, 20);
                                g.DrawString(string.Format("{0}", strrobotid), new Font("고딕체", 5), Brushes.Black, pos.X - 20, pos.Y - 20);

                            }
                        }
                    }
                    WorkFlowGoal temp_action = new WorkFlowGoal();
                    try
                    {
                        foreach (KeyValuePair<string, Node_mission> mission in Data.Instance.node_mission_list)
                        {
                            temp_action = JsonConvert.DeserializeObject<WorkFlowGoal>(mission.Value.work);
                            float nodeX = temp_action.work[0].action_args[0];
                            float nodeY = temp_action.work[0].action_args[1];

                            float cellX = nodeX / resoultion1;
                            float cellY = nodeY / resoultion1;

                            PointF pos = new PointF();
                            pos.X = dOrignX + cellX * (float)Wheelratio;
                            pos.Y = dOrignY - cellY * (float)Wheelratio;

                            Pen node_pen = new Pen(Brushes.Aqua, 3);
                            node_pen.DashStyle = DashStyle.Solid;
                            g.DrawEllipse(node_pen, pos.X - 10, pos.Y - 10, 20, 20);
                            g.DrawString(string.Format("{0}", mission.Key), new Font("고딕체", 5), Brushes.Black, pos.X - 10, pos.Y - 20);

                        }
                    }
                    catch
                    {

                    }
                    
                }
                pb_map.Image = translateBmp;

                bmSource.Dispose();
                bmMergeOKSource.Dispose();
                flag = false;
               

            
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onMapDisplay1 err :={0}", ex.Message.ToString());
            }
        }*/
       
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
        public void onMapDisplay1()
        {
            try
            {
                Bitmap bmSource = new Bitmap(nSourceMapWidth, nSourceMapHeight, PixelFormat.Format32bppRgb);//, PixelFormat.Format8bppIndexed);

                Bitmap bmMergeOKSource = new Bitmap(nSourceMapWidth, nSourceMapHeight, PixelFormat.Format32bppRgb);//, PixelFormat.Format8bppIndexed);

                Map_Robot_Image_Processing2(ref bmSource, bmSource.Width, bmSource.Height, sourceMapValues, "gray");

                dOrignX = ((ori_x * -1) / resoultion1);
                dOrignY = ((ori_y) / resoultion1);

                if (dOrignY < 0) dOrignY *= -1;
                dOrignY = nSourceMapHeight - dOrignY;

                dOrignX = dOrignX * (float)Wheelratio + translate_x;
                dOrignY = dOrignY * (float)Wheelratio + translate_y;

                Image imgSource_Chg;
                imgSource_Chg = ZoomIn(bmSource, Wheelratio);

                Bitmap translateBmp = new Bitmap(imgSource_Chg.Width, imgSource_Chg.Height);
                translateBmp.SetResolution(imgSource_Chg.HorizontalResolution, imgSource_Chg.VerticalResolution);

                Graphics g = Graphics.FromImage(translateBmp);
                g.TranslateTransform(translate_x, translate_y);
                g.DrawImage(imgSource_Chg, new PointF(0, 0));

                pb_map.Image = translateBmp;

                //pb_map.Invalidate();
                pb_map_Paint();
                //pb_paint_test();

                bmSource.Dispose();
                bmMergeOKSource.Dispose();

                //map_timer.Interval = 500;
                //map_timer.Enabled = true;

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
        #endregion
        MySqlConnection conn;
        private void button1_Click(object sender, EventArgs e)
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
                    //Console.WriteLine(r["map_data"]);
                    mapstate = JsonConvert.DeserializeObject<MapState>(r["map_data"].ToString());
                    //Console.WriteLine(mapstate.data);
                }
            }
            Console.WriteLine(mapstate.data.Count());
            width = mapstate.info.width;
            height = mapstate.info.height;
            resoultion1 = (float)mapstate.info.resolution;
            ori_x = (float)mapstate.info.origin.position.x;
            ori_y = (float)mapstate.info.origin.position.y;
            sourceMapValues = mapstate.data.ToArray().Select(x => (byte)x).ToArray();
            for (var y = 0; y < width * height; y++)
            {
                sourceMapValues[y] = (byte)mapstate.data[y];

            }
            MapInfoComplete();
        }

        private void displayMap_Load(object sender, EventArgs e)
        {
            flag = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (pb_map != null)
                {
                    pb_map.Image.Save(@"F:\MAP\map_original.png", System.Drawing.Imaging.ImageFormat.Png);
                    MessageBox.Show("이미지 저장 완료");
                }
            }
            catch { }
        }
        private Bitmap img;
        bool testflag = false;
        private void button5_Click(object sender, EventArgs e)
        {
            string strConn = "Server=192.168.20.28;Database=ridis_db;Uid=syscon;Pwd=r023866677!";
            conn = new MySqlConnection(strConn);
            conn.Open();

            DataSet ds = new DataSet();
            string sql = "select * from map_t order by idx DESC limit 1";
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
            string temp = "";
            adapter.Fill(ds, "map_t");
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    //Console.WriteLine(r["map_data"]);
                    mapstate = JsonConvert.DeserializeObject<MapState>(r["map_data"].ToString());
                    //Console.WriteLine(mapstate.data);
                }
            }
            Console.WriteLine(mapstate.data.Count());
            width = mapstate.info.width;
            height = mapstate.info.height;
            resoultion1 = (float)mapstate.info.resolution;
            ori_x = (float)mapstate.info.origin.position.x;
            ori_y = (float)mapstate.info.origin.position.y;
            string image_file = string.Empty;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = @"F:\MAP";

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                image_file = dialog.FileName;
            }
            else if(dialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            //pb_map.Image = Bitmap.FromFile(image_file);
            pb_map.SizeMode = PictureBoxSizeMode.Normal;
            img = new Bitmap(Bitmap.FromFile(image_file));
            testflag = true;
            pb_map.Invalidate();

            


            //map_timer.Interval = 500;
            //map_timer.Enabled = true;

        }
        public byte[] imageToByte(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Console.WriteLine(imageToByte(pb_map.Image)[0]);
            sourceMapValues = imageToByte(pb_map.Image);
            MapInfoComplete();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pb_paint_test();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            findNode();
        }

        string startNode;
        string endNode;
        private void findNode()
        {

            double min = 999;
            double robotX = Data.Instance.Robot_work_info["R_002"].robot_status_info.robotstate.msg.pose.x;
            double robotY = Data.Instance.Robot_work_info["R_002"].robot_status_info.robotstate.msg.pose.y;

            WorkFlowGoal temp_action = new WorkFlowGoal();
            string missionname = "";
            foreach (KeyValuePair<string, Node_mission> mission in Data.Instance.node_mission_list)
            {
                temp_action = JsonConvert.DeserializeObject<WorkFlowGoal>(mission.Value.work);

                double mintemp = 0;
                double nodeX = temp_action.work[0].action_args[0];
                double nodeY = temp_action.work[0].action_args[1];
                mintemp = onPointToPointDist(robotX, robotY, nodeX, nodeY);
                string strTemp = Data.Instance.node_mission_list[mission.Key].mission_name;
                if (min > mintemp)
                {
                    min = mintemp;
                    startNode = strTemp;
                }
            }
            Console.WriteLine("가장 가까운 노드는 : {0}, 거리 :{1}", missionname, min);
        }
        private double onPointToPointDist(double x1, double y1, double x2, double y2)
        {
            double hypo = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            return hypo;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
    }
}
