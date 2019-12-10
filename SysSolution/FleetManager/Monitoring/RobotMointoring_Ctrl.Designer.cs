namespace SysSolution.FleetManager.Monitoring
{
    partial class RobotMointoring_Ctrl
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title4 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.cboRobotID = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTemper1 = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.lblBatterycapacity = new System.Windows.Forms.Label();
            this.chart_battery = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.robotspeed_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.lblCurrRobotAngluarGab = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblCurrRobotPosGab = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCurrRobotSpeed = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chartControl_path = new DevExpress.XtraCharts.ChartControl();
            this.chartControl_result_path = new DevExpress.XtraCharts.ChartControl();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.progressBar_capacity = new SysSolution.robotmonitoring.aProgressBar();
            this.View = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Cam1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.DP_Timer = new System.Windows.Forms.Timer(this.components);
            this.toggleSwitch_monitoring = new DevExpress.XtraEditors.ToggleSwitch();
            this.chartControl2 = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.chart_battery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.robotspeed_chart)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_path)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_result_path)).BeginInit();
            this.View.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch_monitoring.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl2)).BeginInit();
            this.SuspendLayout();
            // 
            // cboRobotID
            // 
            this.cboRobotID.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cboRobotID.FormattingEnabled = true;
            this.cboRobotID.Items.AddRange(new object[] {
            "R_005",
            "R_006",
            "R_007",
            "R_008"});
            this.cboRobotID.Location = new System.Drawing.Point(105, 12);
            this.cboRobotID.Name = "cboRobotID";
            this.cboRobotID.Size = new System.Drawing.Size(171, 33);
            this.cboRobotID.TabIndex = 36;
            this.cboRobotID.SelectedIndexChanged += new System.EventHandler(this.cboRobotID_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(21, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 20);
            this.label1.TabIndex = 37;
            this.label1.Text = "Robot:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTemper1
            // 
            this.txtTemper1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtTemper1.Location = new System.Drawing.Point(654, 24);
            this.txtTemper1.Name = "txtTemper1";
            this.txtTemper1.Size = new System.Drawing.Size(10, 29);
            this.txtTemper1.TabIndex = 45;
            this.txtTemper1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTemper1.Visible = false;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label40.Location = new System.Drawing.Point(650, 4);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(56, 19);
            this.label40.TabIndex = 42;
            this.label40.Text = "온도(C)";
            this.label40.Visible = false;
            // 
            // lblBatterycapacity
            // 
            this.lblBatterycapacity.AutoSize = true;
            this.lblBatterycapacity.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblBatterycapacity.Location = new System.Drawing.Point(526, 4);
            this.lblBatterycapacity.Name = "lblBatterycapacity";
            this.lblBatterycapacity.Size = new System.Drawing.Size(101, 19);
            this.lblBatterycapacity.TabIndex = 43;
            this.lblBatterycapacity.Text = "배터리용량(%)";
            this.lblBatterycapacity.Visible = false;
            // 
            // chart_battery
            // 
            chartArea4.Name = "ChartArea1";
            this.chart_battery.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chart_battery.Legends.Add(legend4);
            this.chart_battery.Location = new System.Drawing.Point(526, 41);
            this.chart_battery.Name = "chart_battery";
            this.chart_battery.Size = new System.Drawing.Size(88, 24);
            this.chart_battery.TabIndex = 38;
            this.chart_battery.Text = "c1";
            title3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title3.Name = "Title1";
            title3.Text = "배터리상태";
            this.chart_battery.Titles.Add(title3);
            this.chart_battery.Visible = false;
            // 
            // robotspeed_chart
            // 
            chartArea5.Name = "ChartArea1";
            this.robotspeed_chart.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.robotspeed_chart.Legends.Add(legend5);
            this.robotspeed_chart.Location = new System.Drawing.Point(6, 45);
            this.robotspeed_chart.Name = "robotspeed_chart";
            this.robotspeed_chart.Size = new System.Drawing.Size(619, 297);
            this.robotspeed_chart.TabIndex = 39;
            this.robotspeed_chart.Text = "c1";
            title4.Name = "Title1";
            title4.Text = "로봇속도";
            this.robotspeed_chart.Titles.Add(title4);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.chartControl1);
            this.groupBox7.Controls.Add(this.lblCurrRobotAngluarGab);
            this.groupBox7.Controls.Add(this.label5);
            this.groupBox7.Controls.Add(this.label4);
            this.groupBox7.Controls.Add(this.label7);
            this.groupBox7.Location = new System.Drawing.Point(654, 71);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(631, 462);
            this.groupBox7.TabIndex = 46;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "앵글 모니터링";
            // 
            // chartControl1
            // 
            this.chartControl1.Legend.Name = "Default Legend";
            this.chartControl1.Location = new System.Drawing.Point(65, 53);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl1.SeriesTemplate.SeriesColorizer = null;
            this.chartControl1.Size = new System.Drawing.Size(529, 188);
            this.chartControl1.TabIndex = 14;
            // 
            // lblCurrRobotAngluarGab
            // 
            this.lblCurrRobotAngluarGab.AutoSize = true;
            this.lblCurrRobotAngluarGab.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCurrRobotAngluarGab.Location = new System.Drawing.Point(211, 24);
            this.lblCurrRobotAngluarGab.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCurrRobotAngluarGab.Name = "lblCurrRobotAngluarGab";
            this.lblCurrRobotAngluarGab.Size = new System.Drawing.Size(38, 25);
            this.lblCurrRobotAngluarGab.TabIndex = 1;
            this.lblCurrRobotAngluarGab.Text = "(o)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(67, 24);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 25);
            this.label5.TabIndex = 1;
            this.label5.Text = "현재 앵글 오차";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(7, 108);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "실시간";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(20, 329);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 17);
            this.label7.TabIndex = 1;
            this.label7.Text = "전체";
            // 
            // lblCurrRobotPosGab
            // 
            this.lblCurrRobotPosGab.AutoSize = true;
            this.lblCurrRobotPosGab.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCurrRobotPosGab.Location = new System.Drawing.Point(196, 24);
            this.lblCurrRobotPosGab.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCurrRobotPosGab.Name = "lblCurrRobotPosGab";
            this.lblCurrRobotPosGab.Size = new System.Drawing.Size(39, 25);
            this.lblCurrRobotPosGab.TabIndex = 1;
            this.lblCurrRobotPosGab.Text = "cm";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCurrRobotSpeed);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.robotspeed_chart);
            this.groupBox1.Location = new System.Drawing.Point(654, 539);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(631, 346);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "속도 그래프";
            // 
            // lblCurrRobotSpeed
            // 
            this.lblCurrRobotSpeed.AutoSize = true;
            this.lblCurrRobotSpeed.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCurrRobotSpeed.Location = new System.Drawing.Point(104, 17);
            this.lblCurrRobotSpeed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCurrRobotSpeed.Name = "lblCurrRobotSpeed";
            this.lblCurrRobotSpeed.Size = new System.Drawing.Size(66, 25);
            this.lblCurrRobotSpeed.TabIndex = 1;
            this.lblCurrRobotSpeed.Text = "m/sec";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(5, 17);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 25);
            this.label3.TabIndex = 1;
            this.label3.Text = "현재 속도";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chartControl_path);
            this.groupBox2.Controls.Add(this.chartControl_result_path);
            this.groupBox2.Controls.Add(this.lblCurrRobotPosGab);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(25, 71);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(607, 462);
            this.groupBox2.TabIndex = 48;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "위치 모니터링";
            // 
            // chartControl_path
            // 
            this.chartControl_path.Legend.Name = "Default Legend";
            this.chartControl_path.Location = new System.Drawing.Point(68, 52);
            this.chartControl_path.Name = "chartControl_path";
            this.chartControl_path.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl_path.SeriesTemplate.SeriesColorizer = null;
            this.chartControl_path.Size = new System.Drawing.Size(529, 189);
            this.chartControl_path.TabIndex = 47;
            // 
            // chartControl_result_path
            // 
            this.chartControl_result_path.Legend.Name = "Default Legend";
            this.chartControl_result_path.Location = new System.Drawing.Point(68, 259);
            this.chartControl_result_path.Name = "chartControl_result_path";
            this.chartControl_result_path.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl_result_path.SeriesTemplate.SeriesColorizer = null;
            this.chartControl_result_path.Size = new System.Drawing.Size(529, 189);
            this.chartControl_result_path.TabIndex = 46;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.ForeColor = System.Drawing.Color.Blue;
            this.label9.Location = new System.Drawing.Point(13, 329);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 17);
            this.label9.TabIndex = 1;
            this.label9.Text = "전체";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(6, 120);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 17);
            this.label8.TabIndex = 1;
            this.label8.Text = "실시간";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(52, 23);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 25);
            this.label6.TabIndex = 1;
            this.label6.Text = "현재 주행 오차";
            // 
            // progressBar_capacity
            // 
            this.progressBar_capacity.BackColor = System.Drawing.Color.Blue;
            this.progressBar_capacity.Location = new System.Drawing.Point(526, 25);
            this.progressBar_capacity.Name = "progressBar_capacity";
            this.progressBar_capacity.Size = new System.Drawing.Size(10, 10);
            this.progressBar_capacity.TabIndex = 44;
            this.progressBar_capacity.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.progressBar_capacity.TextColor = System.Drawing.SystemColors.ControlText;
            this.progressBar_capacity.TextFont = new System.Drawing.Font("Times New Roman", 10F);
            this.progressBar_capacity.TextMargin = new System.Drawing.Point(1, 1);
            this.progressBar_capacity.Visible = false;
            // 
            // View
            // 
            this.View.Controls.Add(this.label2);
            this.View.Controls.Add(this.Cam1);
            this.View.Controls.Add(this.pictureBox2);
            this.View.Controls.Add(this.pictureBox1);
            this.View.Location = new System.Drawing.Point(25, 539);
            this.View.Margin = new System.Windows.Forms.Padding(2);
            this.View.Name = "View";
            this.View.Padding = new System.Windows.Forms.Padding(2);
            this.View.Size = new System.Drawing.Size(607, 346);
            this.View.TabIndex = 49;
            this.View.TabStop = false;
            this.View.Text = "블랙박스";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(307, 34);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Cam2";
            // 
            // Cam1
            // 
            this.Cam1.AutoSize = true;
            this.Cam1.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Cam1.Location = new System.Drawing.Point(31, 34);
            this.Cam1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Cam1.Name = "Cam1";
            this.Cam1.Size = new System.Drawing.Size(63, 25);
            this.Cam1.TabIndex = 1;
            this.Cam1.Text = "Cam1";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(36, 62);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(272, 255);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(307, 62);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(265, 255);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // chart1
            // 
            chartArea6.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chart1.Legends.Add(legend6);
            this.chart1.Location = new System.Drawing.Point(1194, 21);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(91, 44);
            this.chart1.TabIndex = 46;
            this.chart1.Text = "chart1";
            this.chart1.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // DP_Timer
            // 
            this.DP_Timer.Tick += new System.EventHandler(this.DP_Timer_Tick);
            // 
            // toggleSwitch_monitoring
            // 
            this.toggleSwitch_monitoring.Location = new System.Drawing.Point(282, 13);
            this.toggleSwitch_monitoring.Name = "toggleSwitch_monitoring";
            this.toggleSwitch_monitoring.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch_monitoring.Properties.Appearance.Options.UseFont = true;
            this.toggleSwitch_monitoring.Properties.OffText = "모니터링 Off";
            this.toggleSwitch_monitoring.Properties.OnText = "모니터링 On";
            this.toggleSwitch_monitoring.Size = new System.Drawing.Size(202, 32);
            this.toggleSwitch_monitoring.TabIndex = 50;
            this.toggleSwitch_monitoring.Toggled += new System.EventHandler(this.toggleSwitch1_Toggled);
            // 
            // chartControl2
            // 
            this.chartControl2.Legend.Name = "Default Legend";
            this.chartControl2.Location = new System.Drawing.Point(720, 330);
            this.chartControl2.Name = "chartControl2";
            this.chartControl2.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl2.SeriesTemplate.SeriesColorizer = null;
            this.chartControl2.Size = new System.Drawing.Size(528, 189);
            this.chartControl2.TabIndex = 15;
            // 
            // RobotMointoring_Ctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.Controls.Add(this.chartControl2);
            this.Controls.Add(this.toggleSwitch_monitoring);
            this.Controls.Add(this.txtTemper1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.progressBar_capacity);
            this.Controls.Add(this.View);
            this.Controls.Add(this.label40);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblBatterycapacity);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chart_battery);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboRobotID);
            this.Name = "RobotMointoring_Ctrl";
            this.Size = new System.Drawing.Size(1300, 900);
            this.Load += new System.EventHandler(this.RobotMointoring_Ctrl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart_battery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.robotspeed_chart)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_path)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_result_path)).EndInit();
            this.View.ResumeLayout(false);
            this.View.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch_monitoring.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboRobotID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTemper1;
        private robotmonitoring.aProgressBar progressBar_capacity;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label lblBatterycapacity;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_battery;
        private System.Windows.Forms.DataVisualization.Charting.Chart robotspeed_chart;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox View;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Cam1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblCurrRobotSpeed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCurrRobotPosGab;
        private System.Windows.Forms.Timer DP_Timer;
        private DevExpress.XtraEditors.ToggleSwitch toggleSwitch_monitoring;
        private System.Windows.Forms.Label lblCurrRobotAngluarGab;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private DevExpress.XtraCharts.ChartControl chartControl_result_path;
        private DevExpress.XtraCharts.ChartControl chartControl_path;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraCharts.ChartControl chartControl2;
    }
}
