using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Drawing2D;

using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace SysSolution.Frm
{
    public partial class WorkResultFrm : Form
    {
        public WorkResultFrm()
        {
            InitializeComponent();
        }

        DashboardForm mainForm;

        public WorkResultFrm(DashboardForm frm)
        {
            mainForm = frm;
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void WorkResultFrm_Load(object sender, EventArgs e)
        {
            cboWorkList.SelectedIndex = 0;

            ChartSetting();
            viewMonthResultAll(chart_monthworkresult);

            ChartSetting_RobotworkData(chart_robotworkdata);
            viewRobotworkResult(chart_robotworkdata);


            ChartRobotDailySetting(chart_dailyResult);
            viewRobotDailyAll(chart_dailyResult);


            string[] strRobotreglist = { "R_004","로봇500", "3시간" };
            dataGridView_robotworktime.Rows.Add(strRobotreglist);

            string[] strRobotreglist2 = { "R_005", "로봇501", "2시간" };
            dataGridView_robotworktime.Rows.Add(strRobotreglist2);

            string[] strRobotreglist3 = { "R_006", "로봇100", "1시간" };
            dataGridView_robotworktime.Rows.Add(strRobotreglist3);

            string[] strRobotreglist4 = { "R_007", "로봇1000", "4시간" };
            dataGridView_robotworktime.Rows.Add(strRobotreglist4);

            //chart_robotworkdata
        }

        double[] ecg = new double[50000];
        double[] ppg = new double[50000];
        private int ecgCount;
        private int ppgCount;

        double[] robotworkdata = new double[5];
        double[] robotworkname = new double[5];
       
        private void viewMonthResultAll(System.Windows.Forms.DataVisualization.Charting.Chart ch)
        {
            for(int i=0; i<30; i++)
            {
                Random ra = new Random();
                int data = ra.Next(0, 500);
                ecg[i] = data;
                data = ra.Next(0, 500);
                ppg[i] = data;
            }

            int cnt = 0;
            //차트를 설정하고 Series에 점 좌표를 넣어주면 차트가 그려짐.
            foreach (double x in ecg)
            {
                ch.Series["ECG"].Points.Add(x);
                ch.Series["ECG"].Points[cnt].Label = string.Format("{0}", x);
                ch.Series["ECG"].Points[cnt].LabelForeColor = Color.White;
                cnt++;
            }
            cnt = 0;
            foreach (double x in ppg)
            {
                ch.Series["PPG"].Points.Add(x);
                ch.Series["PPG"].Points[cnt].Label = string.Format("{0}", x);
                ch.Series["PPG"].Points[cnt].LabelForeColor = Color.White;
                cnt++;
            }
        }

        private void ChartSetting()
        {
            chart_monthworkresult.ChartAreas.Clear();
            chart_monthworkresult.Series.Clear();
            chart_monthworkresult.ChartAreas.Add("Draw");
            chart_monthworkresult.ChartAreas["Draw"].BackColor = Color.Black;
            chart_monthworkresult.ChartAreas["Draw"].AxisX.Minimum = 0;
            chart_monthworkresult.ChartAreas["Draw"].AxisX.Maximum = 31;
            chart_monthworkresult.ChartAreas["Draw"].AxisX.Interval = 1; //간격50
            chart_monthworkresult.ChartAreas["Draw"].AxisX.MajorGrid.LineColor = Color.Gray;  //LineColor 눈금의 선 색
            chart_monthworkresult.ChartAreas["Draw"].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash; //MajorGrid=선 속성 설정.//LineDashStyle 눈금의 선 스타일

            chart_monthworkresult.ChartAreas["Draw"].AxisY.Minimum = 0;
            chart_monthworkresult.ChartAreas["Draw"].AxisY.Maximum = 500;
            chart_monthworkresult.ChartAreas["Draw"].AxisY.Interval = 100;
            chart_monthworkresult.ChartAreas["Draw"].AxisY.MajorGrid.LineColor = Color.Gray;
            chart_monthworkresult.ChartAreas["Draw"].AxisY.MajorGrid.LineDashStyle        = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            // 2. Series(차트에 그려지는 데이터)
            chart_monthworkresult.Series.Add("ECG");
            chart_monthworkresult.Series["ECG"].ChartType = SeriesChartType.Column; //SeriesChartType.Line 꺾은선형 차트 종류.
            chart_monthworkresult.Series["ECG"].Color = Color.Green;
            chart_monthworkresult.Series["ECG"].BorderWidth = 2; //BorderWidth 데이터 요소의 테두리 너비.
            chart_monthworkresult.Series["ECG"].LegendText = "목표량"; //LegendText 범례에서 항목의 텍스트를 가져오거나 설정

            chart_monthworkresult.Series.Add("PPG");
            chart_monthworkresult.Series["PPG"].ChartType = SeriesChartType.Column;
            chart_monthworkresult.Series["PPG"].Color = Color.Blue;
            chart_monthworkresult.Series["PPG"].BorderWidth = 2;
            chart_monthworkresult.Series["PPG"].LegendText = "생산량";

            // Zoom 기능(그래프 영역을 확대)
            chart_monthworkresult.ChartAreas["Draw"].CursorX.IsUserSelectionEnabled = true; //사용자가 구간을 선택.
            chart_monthworkresult.ChartAreas["Draw"].AxisX.ScrollBar.ButtonColor = Color.LightSteelBlue; //구간을 스크롤했을때 색은 파란색.
        }

        double[] robotworkdaily = new double[6];
        double[] robotworkdaily2 = new double[6];
        private void viewRobotDailyAll(System.Windows.Forms.DataVisualization.Charting.Chart ch)
        {
            for (int i = 0; i < 6; i++)
            {
                Random ra = new Random();
                int data = ra.Next(0, 500);
                robotworkdaily[i] = data;
                data = ra.Next(0, 500);
                robotworkdaily2[i] = data;
            }

            int cnt = 0;
            //차트를 설정하고 Series에 점 좌표를 넣어주면 차트가 그려짐.
            foreach (double x in robotworkdaily)
            {
               // ch.Series["ECG"].Points.DataBindXY(strrobotname[cnt], robotworkdaily); // 자료 첨부
                ch.Series["ECG"].Points.Add(x);
                ch.Series["ECG"].Points[cnt].Label = string.Format("{0}", x);
                ch.Series["ECG"].Points[cnt].LabelForeColor = Color.White;
                cnt++;
            }
            cnt = 0;
            foreach (double x in robotworkdaily2)
            {
                ch.Series["PPG"].Points.Add(x);
                ch.Series["PPG"].Points[cnt].Label = string.Format("{0}", x);
                ch.Series["PPG"].Points[cnt].LabelForeColor = Color.White;
                cnt++;
            }
        }
        private void ChartRobotDailySetting(System.Windows.Forms.DataVisualization.Charting.Chart ch)
        {
            ch.ChartAreas.Clear();
            ch.Series.Clear();
            ch.ChartAreas.Add("Draw");
            ch.ChartAreas["Draw"].BackColor = Color.Black;
            ch.ChartAreas["Draw"].AxisX.Minimum = 0;
            ch.ChartAreas["Draw"].AxisX.Maximum = 7;
            ch.ChartAreas["Draw"].AxisX.Interval = 1; //간격50
            ch.ChartAreas["Draw"].AxisX.MajorGrid.LineColor = Color.Gray;  //LineColor 눈금의 선 색
            ch.ChartAreas["Draw"].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash; //MajorGrid=선 속성 설정.//LineDashStyle 눈금의 선 스타일

            ch.ChartAreas["Draw"].AxisY.Minimum = 0;
            ch.ChartAreas["Draw"].AxisY.Maximum = 500;
            ch.ChartAreas["Draw"].AxisY.Interval = 100;
            ch.ChartAreas["Draw"].AxisY.MajorGrid.LineColor = Color.Gray;
            ch.ChartAreas["Draw"].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            // 2. Series(차트에 그려지는 데이터)
            ch.Series.Add("ECG");
            ch.Series["ECG"].ChartType = SeriesChartType.Column; //SeriesChartType.Line 꺾은선형 차트 종류.
            ch.Series["ECG"].Color = Color.Green;
            ch.Series["ECG"].BorderWidth = 2; //BorderWidth 데이터 요소의 테두리 너비.
            ch.Series["ECG"].LegendText = "목표량"; //LegendText 범례에서 항목의 텍스트를 가져오거나 설정

            ch.Series.Add("PPG");
            ch.Series["PPG"].ChartType = SeriesChartType.Column;
            ch.Series["PPG"].Color = Color.Blue;
            ch.Series["PPG"].BorderWidth = 2;
            ch.Series["PPG"].LegendText = "생산량";

            // Zoom 기능(그래프 영역을 확대)
            ch.ChartAreas["Draw"].CursorX.IsUserSelectionEnabled = true; //사용자가 구간을 선택.
            ch.ChartAreas["Draw"].AxisX.ScrollBar.ButtonColor = Color.LightSteelBlue; //구간을 스크롤했을때 색은 파란색.
        }



        string[] strrobotname = { "robot1", "robot2", "robot3", "robot4", "robot5", "robot6" };
        private void viewRobotworkResult(System.Windows.Forms.DataVisualization.Charting.Chart ch)
        {
            /* for (int i = 0; i < 5; i++)
             {
                 Random ra = new Random();
                 int data = ra.Next(0, 500);
                 robotworkdata[i] = data;
                 data = ra.Next(0, 500);
                 robotworkname[i] = data;
             }
             */

            robotworkdata[0] = 10;
            robotworkdata[1] = 10;
            robotworkdata[2] = 50;
            robotworkdata[3] = 20;
            robotworkdata[4] = 20;

            //차트를 설정하고 Series에 점 좌표를 넣어주면 차트가 그려짐.
            int cnt = 0;
            foreach (double x in robotworkdata)
            {
                //ch.Series["ECG"].Points.DataBindXY(aryX, aryY);
                ch.Series["ECG"].Points.Add(x);
                ch.Series["ECG"].Points[cnt].LegendText = strrobotname[cnt];
                ch.Series["ECG"].Points[cnt].Label = string.Format("{0}%",x);
                cnt++;
            }
        }

        private void ChartSetting_RobotworkData(System.Windows.Forms.DataVisualization.Charting.Chart ch)
        {
            ch.ChartAreas.Clear();
            ch.Series.Clear();
            ch.ChartAreas.Add("Draw");
            // 2. Series(차트에 그려지는 데이터)
            ch.Series.Add("ECG");
            ch.Series["ECG"].ChartType = SeriesChartType.Pie; //SeriesChartType.Line 꺾은선형 차트 종류.
            ch.Series["ECG"].Color = Color.Green;
            ch.Series["ECG"].BorderWidth = 2; //BorderWidth 데이터 요소의 테두리 너비.

            ch.Series["ECG"].LegendText = "로봇"; //LegendText 범례에서 항목의 텍스트를 가져오거나 설정

            // Zoom 기능(그래프 영역을 확대)
            ch.ChartAreas["Draw"].CursorX.IsUserSelectionEnabled = true; //사용자가 구간을 선택.
            ch.ChartAreas["Draw"].AxisX.ScrollBar.ButtonColor = Color.LightSteelBlue; //구간을 스크롤했을때 색은 파란색.
        }

        private void chart_monthworkresult_CursorPositionChanged(object sender, CursorEventArgs e)
        {
            MessageBox.Show(e.NewPosition.ToString());
        }
    }
}
