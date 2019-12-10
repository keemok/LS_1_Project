using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SysSolution.Frm
{
    public partial class MapMonitor : Form
    {
        public MapMonitor()
        {
            InitializeComponent();
        }

        DashboardForm mainForm;

        public MapMonitor(DashboardForm frm)
        {
            mainForm = frm;
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void MapMonitor_Load(object sender, EventArgs e)
        {
            ChartSetting();
            for(int i=0; i<120; i++)
            {
                Random ra = new Random();
                int n1= ra.Next(-12, 12);

                robot_linear_speed[i] = (double)(n1 / 10);

                n1 = ra.Next(-12, 12);
                robot_anglua_speed[i] = (double)(n1 / 10);

            }
            ViewAll();


            cboMaplist.SelectedIndex = 0;


            string[] strRobotreglist = { "로봇500", "R_004", "1", "이송" };
            dataGridView_waitRobot.Rows.Add(strRobotreglist);

            string[] strRobotreglist2 = { "로봇501", "R_005", "2", "파레트" };
            dataGridView_waitRobot.Rows.Add(strRobotreglist2);

            string[] strRobotreglist3 = { "로봇1000", "R_006", "2", "파레트" };
            dataGridView_workRobot.Rows.Add(strRobotreglist3);

            string[] strRobotreglist4 = { "로봇100", "R_007", "1", "이송" };
            dataGridView_workRobot.Rows.Add(strRobotreglist4);

        }


        double[] robot_linear_speed = new double[1000];
        double[] robot_anglua_speed = new double[1000];
        int nlinearspeed_idx = 0;
        int nangularspeed_idx = 0;
        private void ChartSetting()
        {
            //0.디폴트 ChartAreas와 Series 삭제

            chart_robotspeed.ChartAreas.Clear();
            chart_robotspeed.Series.Clear();
            //1.ChartAreas
            chart_robotspeed.ChartAreas.Add("Draw"); ;
            chart_robotspeed.ChartAreas["Draw"].BackColor = Color.Black;
            chart_robotspeed.ChartAreas["Draw"].AxisX.Minimum = 0;
            chart_robotspeed.ChartAreas["Draw"].AxisX.Maximum = 120;
            chart_robotspeed.ChartAreas["Draw"].AxisX.Interval = 20;
            chart_robotspeed.ChartAreas["Draw"].AxisX.Title = "시간";
            chart_robotspeed.ChartAreas["Draw"].AxisX.MajorGrid.LineColor = Color.Gray;
            chart_robotspeed.ChartAreas["Draw"].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;  //너무길면 using 으로 보내주자
            chart_robotspeed.ChartAreas["Draw"].AxisY.Minimum = -1.2;
            chart_robotspeed.ChartAreas["Draw"].AxisY.Maximum = 1.2;
            chart_robotspeed.ChartAreas["Draw"].AxisY.Interval = 0.2;
            chart_robotspeed.ChartAreas["Draw"].AxisY.Title = "속도";
            chart_robotspeed.ChartAreas["Draw"].AxisY.MajorGrid.LineColor = Color.Gray;
            chart_robotspeed.ChartAreas["Draw"].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            //2.Series
            chart_robotspeed.Series.Add("Liner");
            chart_robotspeed.Series["Liner"].ChartType = SeriesChartType.Line;
            chart_robotspeed.Series["Liner"].Color = Color.LightGreen;
            chart_robotspeed.Series["Liner"].BorderWidth = 2; ;
            chart_robotspeed.Series["Liner"].LegendText = "선속도(m/s)"; //레전드는 범례

            chart_robotspeed.Series.Add("Angular");
            chart_robotspeed.Series["Angular"].ChartType = SeriesChartType.Line;
            chart_robotspeed.Series["Angular"].Color = Color.Orange;
            chart_robotspeed.Series["Angular"].BorderWidth = 2; ;
            chart_robotspeed.Series["Angular"].LegendText = "각속도(rad/s)"; //레전드는 범례
            //줌기능을 넣어보자!
            chart_robotspeed.ChartAreas["Draw"].CursorX.IsUserSelectionEnabled = true;
            //스크롤 색변경
            chart_robotspeed.ChartAreas["Draw"].AxisX.ScrollBar.ButtonColor = Color.Blue;
        }

        private void ViewAll()
        {
            Random ra = new Random();

            //데이터는 ecg배열 ppg배열에 있어
            foreach (double x in robot_linear_speed)
            {
                chart_robotspeed.Series["Liner"].Points.Add(x);
            }
            foreach (double x in robot_anglua_speed)
            {
                chart_robotspeed.Series["Angular"].Points.Add(x);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
