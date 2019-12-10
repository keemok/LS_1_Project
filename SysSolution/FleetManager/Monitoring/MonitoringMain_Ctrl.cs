using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraCharts;
using System.Windows.Forms;
using DevExpress.Utils;


namespace SysSolution.FleetManager.Monitoring
{
    public partial class MonitoringMain_Ctrl : UserControl
    {
        FleetManager_MainForm mainform;
        public Monitoring.RobotMointoring_Ctrl robotmonitoringctrl;

        public MonitoringMain_Ctrl()
        {
            InitializeComponent();
        }

        public MonitoringMain_Ctrl(FleetManager_MainForm frm)
        {
            mainform = frm;
            InitializeComponent();
        }
        
        public void onInitSet()
        {
        }

        int testi = 0;
        Random r = new Random();
        Series[] series = new Series[2];

       
        private void MonitoringMain_Ctrl_Load(object sender, EventArgs e)
        {
            if (panel_monitor.Controls.Count == 1) panel_monitor.Controls.RemoveAt(0);

            robotmonitoringctrl = new RobotMointoring_Ctrl(mainform,this);
     /*       series[0] = new Series("time1", ViewType.Line);
            series[1] = new Series("time2", ViewType.Line);

            series[0].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;  //시리즈에 라벨 표시
            series[1].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;


            //ChartControl에 Series 추가            
            chartControl1.Series.Add(series[0]);
            chartControl1.Series.Add(series[1]);

            chartControl1.CrosshairEnabled = DefaultBoolean.False;
            XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
            diagram.AxisY.WholeRange.MaxValue = 200;    // y축 최대값            
            diagram.AxisY.WholeRange.MinValue = -100;   // y축 최소값             
            diagram.AxisY.WholeRange.Auto = false;      // y축 범위 자동변경 설정             
            diagram.AxisX.WholeRange.SideMarginsValue = 0;

            ConstantLine zeroLine = new ConstantLine();
            zeroLine.Color = Color.LightYellow;
            zeroLine.AxisValue = 0;
            zeroLine.ShowInLegend = false;
            diagram.AxisY.ConstantLines.Add(zeroLine);  // y값 0인 x축 생성             

        //    timer1.Interval = 500;      // 0.4초            
       //     timer1.Start();
       */
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
          /*  if (series[0].Points.Count > 10) // x축은 10개까지만 값을 출력하게 한다.           
            {
                series[0].Points.RemoveAt(0);
                series[1].Points.RemoveAt(0);
            }
        
            series[0].Points.Add(new SeriesPoint(testi, r.Next(-100, 200)));
            series[1].Points.Add(new SeriesPoint(testi++, r.Next(-50, 100)));
            */
        }

        private void btnRobotMonitor1_Click(object sender, EventArgs e)
        {
            panel_monitor.Size = new Size(1300, 1000);
            panel_monitor.Location = new Point(220, 30);

            if (panel_monitor.Controls.Count == 1) panel_monitor.Controls.RemoveAt(0);

            panel_monitor.Controls.Add(robotmonitoringctrl);
            robotmonitoringctrl.onInitSet();
        }

        private void btnRobotMonitor_Click(object sender, EventArgs e)
        {
            panel_monitor.Size = new Size(1300, 1000);
            panel_monitor.Location = new Point(220, 30);

            if (panel_monitor.Controls.Count == 1) panel_monitor.Controls.RemoveAt(0);

            panel_monitor.Controls.Add(robotmonitoringctrl);
            robotmonitoringctrl.onInitSet();
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            Button ownbtn = (Button)sender;
            ownbtn.ForeColor = Color.White;
        }

        private void Button_MouseUp(object sender, MouseEventArgs e)
        {
            Button ownbtn = (Button)sender;
            ownbtn.ForeColor = System.Drawing.Color.Black;
        }

        private void insertPiesBarItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        bool bgraphstop = false;
        private void chartControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if(!bgraphstop)
            {
                timer1.Enabled = false;
                bgraphstop = true;
            }
            else
            {
                timer1.Enabled = true;
                bgraphstop = false;
            }
        }
    }
}
