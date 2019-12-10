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

namespace SysSolution.Frm
{
    public partial class Dashboard_WorkReport_Ctrl : UserControl
    {
        public Dashboard_WorkReport_Ctrl()
        {
            InitializeComponent();
        }

        DashboardForm mainfrm;
        public Dashboard_WorkReport_Ctrl(DashboardForm mfrm)
        {
            mainfrm = mfrm;
            InitializeComponent();
        }

       // double currtime = 0;
        //double remaintime = 0;

        void onTimeCheck()
        {

        }

        private void Dashboard_WorkReport_Ctrl_Load(object sender, EventArgs e)
        {
            ChartWorktimeSetting(chart_Worktime);
            viewWorktimeAll(chart_Worktime);

            timer1.Interval = 1000;
            timer1.Enabled = true;

        }
        double worktime = 2.3;
        double remaintime = 3.1;
        private void viewWorktimeAll(System.Windows.Forms.DataVisualization.Charting.Chart ch)
        {
            ch.Series["ECG"].Points.Add(worktime);
            ch.Series["ECG"].Points[0].Label = string.Format("2:30");
            ch.Series["PPG"].Points.Add(remaintime);
            ch.Series["PPG"].Points[0].Label = string.Format("3:10");
        }
        private void ChartWorktimeSetting(System.Windows.Forms.DataVisualization.Charting.Chart ch)
        {
            ch.ChartAreas.Clear();
            ch.Series.Clear();
            ch.ChartAreas.Add("Draw");
            ch.ChartAreas["Draw"].BackColor = Color.White;
            ch.ChartAreas["Draw"].AxisX.Minimum = 0;
            ch.ChartAreas["Draw"].AxisX.Maximum = 2;
            ch.ChartAreas["Draw"].AxisX.Interval = 1; //간격50
            ch.ChartAreas["Draw"].AxisX.MajorGrid.LineColor = Color.Gray;  //LineColor 눈금의 선 색
            ch.ChartAreas["Draw"].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash; //MajorGrid=선 속성 설정.//LineDashStyle 눈금의 선 스타일

            ch.ChartAreas["Draw"].AxisY.Minimum = 0;
            ch.ChartAreas["Draw"].AxisY.Maximum = 8;
            ch.ChartAreas["Draw"].AxisY.Interval = 1;
            ch.ChartAreas["Draw"].AxisY.MajorGrid.LineColor = Color.Gray;
            ch.ChartAreas["Draw"].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            // 2. Series(차트에 그려지는 데이터)
            ch.Series.Add("ECG");
            ch.Series["ECG"].ChartType = SeriesChartType.Column; //SeriesChartType.Line 꺾은선형 차트 종류.
            ch.Series["ECG"].Color = Color.Blue;
            ch.Series["ECG"].BorderWidth = 2; //BorderWidth 데이터 요소의 테두리 너비.
            ch.Series["ECG"].LegendText = "작업시간"; //LegendText 범례에서 항목의 텍스트를 가져오거나 설정

            ch.Series.Add("PPG");
            ch.Series["PPG"].ChartType = SeriesChartType.Column;
            ch.Series["PPG"].Color = Color.Green;
            ch.Series["PPG"].BorderWidth = 2;
            ch.Series["PPG"].LegendText = "남은시간";

            // Zoom 기능(그래프 영역을 확대)
            ch.ChartAreas["Draw"].CursorX.IsUserSelectionEnabled = true; //사용자가 구간을 선택.
            ch.ChartAreas["Draw"].AxisX.ScrollBar.ButtonColor = Color.LightSteelBlue; //구간을 스크롤했을때 색은 파란색.
        }
        long ltime = 0;
        long lremaintime = 28800;
        long lremaintime2 = 28800;
        private void timer1_Tick(object sender, EventArgs e)
        {
            ltime++;
            string strcurrtime = "";
            string strremaintime = "";

            lremaintime2 = lremaintime- ltime;

            if (lremaintime2 == 0) ltime = 0;

            int min =(int)ltime / 60;

            int remin = (int)lremaintime2 / 60;

            int hour = 0;
            int min2 = 0;
            if (min / 60 > 0)
            {
                hour = min / 60;

                min2 = min % 60;
            }
            else
            {
                min2 = min;
            }

            int rehour = 0;
            int remin2 = 0;
            if (remin / 60 > 0)
            {
                rehour = remin / 60;

                if (remin % 60 > 0)
                {
                    remin2 = remin % 60;
                }

            }

            strcurrtime = string.Format("{0:d2}시{1:d2}분{2:d2}초", hour, min2, ltime % 60);
            strremaintime = string.Format("{0:d2}시{1:d2}분{2:d2}초", rehour, remin2, lremaintime2 % 60);


            Invoke(new MethodInvoker(delegate ()
            {
                lblcurrTime.Text = strcurrtime;
                lblremainTime.Text = strremaintime;
            }));

        }
    }
}
