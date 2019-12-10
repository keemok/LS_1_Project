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
    public partial class Dashboard_WorkAmount_Ctrl : UserControl
    {
       
        public Dashboard_WorkAmount_Ctrl()
        {
            InitializeComponent();
        }
        DashboardForm mainfrm;
        public Dashboard_WorkAmount_Ctrl(DashboardForm mfrm)
        {
            mainfrm = mfrm;
            InitializeComponent();
        }

        private void Dashboard_WorkAmount_Ctrl_Load(object sender, EventArgs e)
        {
            ChartSetting_RobotworkData(chart_WorkAmount);
            viewRobotworkResult(chart_WorkAmount);

        }

        double[] worAmountkdata = new double[5];
        string[] workDate = { "2019-3-27", "2019-3-28", "2019-3-29", "2019-4-1", "2019-4-2" };
        private void viewRobotworkResult(System.Windows.Forms.DataVisualization.Charting.Chart ch)
        {
            for (int i = 0; i < 5; i++)
            {
                Random ra = new Random();
                int data = ra.Next(0, 500);
                //worAmountkdata[i] = data;
            }

            worAmountkdata[0] = 10;
            worAmountkdata[1] = 10;
            worAmountkdata[2] = 50;
            worAmountkdata[3] = 20;
            worAmountkdata[4] = 20;

            //차트를 설정하고 Series에 점 좌표를 넣어주면 차트가 그려짐.
            int cnt = 0;
            foreach (double x in worAmountkdata)
            {
                //ch.Series["ECG"].Points.DataBindXY(aryX, aryY);
                ch.Series["ECG"].Points.Add(x);
                ch.Series["ECG"].Points[cnt].LegendText = workDate[cnt];
                ch.Series["ECG"].Points[cnt].Label = string.Format("{0}%", x);

                // ch.Series["ECG"].LegendText = workDate[cnt]; string.Format("작업량{0}",x);
                cnt++;
            }

          //  ch.Series["ECG"].Points.Add(100);
          //  ch.Series["ECG"].LegendText = workDate[0];
          //  ch.Series["ECG2"].Points.Add(200);
          //  ch.Series["ECG2"].LegendText = workDate[1];
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

            ch.Series.Add("ECG2");
            ch.Series["ECG2"].ChartType = SeriesChartType.Pie; //SeriesChartType.Line 꺾은선형 차트 종류.
            ch.Series["ECG2"].Color = Color.Blue;
            ch.Series["ECG2"].BorderWidth = 2; //BorderWidth 데이터 요소의 테두리 너비.

            //ch.Series["ECG"].LegendText = "작업량"; //LegendText 범례에서 항목의 텍스트를 가져오거나 설정

            // Zoom 기능(그래프 영역을 확대)
            ch.ChartAreas["Draw"].CursorX.IsUserSelectionEnabled = true; //사용자가 구간을 선택.
            ch.ChartAreas["Draw"].AxisX.ScrollBar.ButtonColor = Color.LightSteelBlue; //구간을 스크롤했을때 색은 파란색.
        }

        private void UI_Updatetimer_Tick(object sender, EventArgs e)
        {

        }
    }
}
