namespace SysSolution.robotmonitoring
{
    partial class RoboMonitoringCtrl_1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title5 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title6 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend8 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea9 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend9 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title7 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea10 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend10 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title8 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.chart_globalpath = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.lblangular = new System.Windows.Forms.Label();
            this.chart_angluar = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.radarchart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer_BatteryLogSave = new System.Windows.Forms.Timer(this.components);
            this.myTimer = new System.Windows.Forms.Timer(this.components);
            this.UI_Updatetimer = new System.Windows.Forms.Timer(this.components);
            this.lblGoodsStatus = new System.Windows.Forms.Label();
            this.txtTemper1 = new System.Windows.Forms.TextBox();
            this.txtRobotModel = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.lblBatterycapacity = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.lblLamp_Warnnig = new System.Windows.Forms.Label();
            this.lblLamp_Run = new System.Windows.Forms.Label();
            this.Robot_pictureBox = new System.Windows.Forms.PictureBox();
            this.lblLamp_idle = new System.Windows.Forms.Label();
            this.chart_battery = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.robotspeed_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.Cam1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.progressBar_capacity = new SysSolution.robotmonitoring.aProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.chart_globalpath)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_angluar)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radarchart1)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Robot_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_battery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.robotspeed_chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart_globalpath
            // 
            chartArea6.Name = "ChartArea1";
            this.chart_globalpath.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chart_globalpath.Legends.Add(legend6);
            this.chart_globalpath.Location = new System.Drawing.Point(6, 30);
            this.chart_globalpath.Name = "chart_globalpath";
            this.chart_globalpath.Size = new System.Drawing.Size(530, 375);
            this.chart_globalpath.TabIndex = 12;
            this.chart_globalpath.Text = "c1";
            title5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title5.Name = "Title1";
            this.chart_globalpath.Titles.Add(title5);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.chart_globalpath);
            this.groupBox8.Location = new System.Drawing.Point(1328, 83);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(548, 417);
            this.groupBox8.TabIndex = 13;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "globalpath";
            // 
            // lblangular
            // 
            this.lblangular.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblangular.Location = new System.Drawing.Point(5, 12);
            this.lblangular.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblangular.Name = "lblangular";
            this.lblangular.Size = new System.Drawing.Size(70, 15);
            this.lblangular.TabIndex = 13;
            this.lblangular.Text = "각도=180";
            this.lblangular.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chart_angluar
            // 
            chartArea7.Name = "ChartArea1";
            this.chart_angluar.ChartAreas.Add(chartArea7);
            legend7.Name = "Legend1";
            this.chart_angluar.Legends.Add(legend7);
            this.chart_angluar.Location = new System.Drawing.Point(13, 30);
            this.chart_angluar.Name = "chart_angluar";
            this.chart_angluar.Size = new System.Drawing.Size(544, 375);
            this.chart_angluar.TabIndex = 12;
            this.chart_angluar.Text = "c1";
            title6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title6.Name = "Title1";
            this.chart_angluar.Titles.Add(title6);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.lblangular);
            this.groupBox7.Controls.Add(this.chart_angluar);
            this.groupBox7.Location = new System.Drawing.Point(750, 83);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(572, 417);
            this.groupBox7.TabIndex = 12;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Angular";
            // 
            // radarchart1
            // 
            chartArea8.Name = "ChartArea1";
            this.radarchart1.ChartAreas.Add(chartArea8);
            legend8.Name = "Legend1";
            this.radarchart1.Legends.Add(legend8);
            this.radarchart1.Location = new System.Drawing.Point(6, 20);
            this.radarchart1.Name = "radarchart1";
            this.radarchart1.Size = new System.Drawing.Size(516, 399);
            this.radarchart1.TabIndex = 9;
            this.radarchart1.Text = "c1";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radarchart1);
            this.groupBox5.Location = new System.Drawing.Point(763, 517);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(528, 425);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Lidar";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            // 
            // myTimer
            // 
            this.myTimer.Interval = 250;
            this.myTimer.Tick += new System.EventHandler(this.myTimer_Tick);
            // 
            // UI_Updatetimer
            // 
            this.UI_Updatetimer.Tick += new System.EventHandler(this.UI_Updatetimer_Tick);
            // 
            // lblGoodsStatus
            // 
            this.lblGoodsStatus.Location = new System.Drawing.Point(240, 40);
            this.lblGoodsStatus.Name = "lblGoodsStatus";
            this.lblGoodsStatus.Size = new System.Drawing.Size(51, 16);
            this.lblGoodsStatus.TabIndex = 21;
            this.lblGoodsStatus.Text = "짐(X)";
            // 
            // txtTemper1
            // 
            this.txtTemper1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtTemper1.Location = new System.Drawing.Point(431, 43);
            this.txtTemper1.Name = "txtTemper1";
            this.txtTemper1.Size = new System.Drawing.Size(54, 29);
            this.txtTemper1.TabIndex = 20;
            this.txtTemper1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtRobotModel
            // 
            this.txtRobotModel.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtRobotModel.Location = new System.Drawing.Point(56, 43);
            this.txtRobotModel.Name = "txtRobotModel";
            this.txtRobotModel.Size = new System.Drawing.Size(100, 22);
            this.txtRobotModel.TabIndex = 20;
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label24.Location = new System.Drawing.Point(74, 27);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(67, 12);
            this.label24.TabIndex = 19;
            this.label24.Text = "로봇모델";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label40.Location = new System.Drawing.Point(428, 22);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(56, 19);
            this.label40.TabIndex = 16;
            this.label40.Text = "온도(C)";
            // 
            // lblBatterycapacity
            // 
            this.lblBatterycapacity.AutoSize = true;
            this.lblBatterycapacity.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblBatterycapacity.Location = new System.Drawing.Point(305, 22);
            this.lblBatterycapacity.Name = "lblBatterycapacity";
            this.lblBatterycapacity.Size = new System.Drawing.Size(101, 19);
            this.lblBatterycapacity.TabIndex = 16;
            this.lblBatterycapacity.Text = "배터리용량(%)";
            // 
            // label39
            // 
            this.label39.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label39.Location = new System.Drawing.Point(33, 523);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(80, 12);
            this.label39.TabIndex = 14;
            this.label39.Text = "로봇 속도";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label36
            // 
            this.label36.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label36.Location = new System.Drawing.Point(33, 94);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(80, 12);
            this.label36.TabIndex = 14;
            this.label36.Text = "배터리 상태";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLamp_Warnnig
            // 
            this.lblLamp_Warnnig.BackColor = System.Drawing.Color.Red;
            this.lblLamp_Warnnig.Location = new System.Drawing.Point(211, 7);
            this.lblLamp_Warnnig.Name = "lblLamp_Warnnig";
            this.lblLamp_Warnnig.Size = new System.Drawing.Size(20, 13);
            this.lblLamp_Warnnig.TabIndex = 13;
            this.lblLamp_Warnnig.Visible = false;
            // 
            // lblLamp_Run
            // 
            this.lblLamp_Run.BackColor = System.Drawing.Color.Blue;
            this.lblLamp_Run.Location = new System.Drawing.Point(187, 7);
            this.lblLamp_Run.Name = "lblLamp_Run";
            this.lblLamp_Run.Size = new System.Drawing.Size(20, 13);
            this.lblLamp_Run.TabIndex = 13;
            this.lblLamp_Run.Visible = false;
            // 
            // Robot_pictureBox
            // 
            this.Robot_pictureBox.BackColor = System.Drawing.Color.White;
            this.Robot_pictureBox.Location = new System.Drawing.Point(162, 23);
            this.Robot_pictureBox.Name = "Robot_pictureBox";
            this.Robot_pictureBox.Size = new System.Drawing.Size(72, 72);
            this.Robot_pictureBox.TabIndex = 12;
            this.Robot_pictureBox.TabStop = false;
            this.Robot_pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.Robot_pictureBox_Paint);
            // 
            // lblLamp_idle
            // 
            this.lblLamp_idle.BackColor = System.Drawing.Color.Yellow;
            this.lblLamp_idle.Location = new System.Drawing.Point(163, 7);
            this.lblLamp_idle.Name = "lblLamp_idle";
            this.lblLamp_idle.Size = new System.Drawing.Size(20, 13);
            this.lblLamp_idle.TabIndex = 13;
            this.lblLamp_idle.Visible = false;
            // 
            // chart_battery
            // 
            chartArea9.Name = "ChartArea1";
            this.chart_battery.ChartAreas.Add(chartArea9);
            legend9.Name = "Legend1";
            this.chart_battery.Legends.Add(legend9);
            this.chart_battery.Location = new System.Drawing.Point(30, 109);
            this.chart_battery.Name = "chart_battery";
            this.chart_battery.Size = new System.Drawing.Size(704, 379);
            this.chart_battery.TabIndex = 11;
            this.chart_battery.Text = "c1";
            title7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title7.Name = "Title1";
            title7.Text = "배터리상태";
            this.chart_battery.Titles.Add(title7);
            // 
            // robotspeed_chart
            // 
            chartArea10.Name = "ChartArea1";
            this.robotspeed_chart.ChartAreas.Add(chartArea10);
            legend10.Name = "Legend1";
            this.robotspeed_chart.Legends.Add(legend10);
            this.robotspeed_chart.Location = new System.Drawing.Point(30, 538);
            this.robotspeed_chart.Name = "robotspeed_chart";
            this.robotspeed_chart.Size = new System.Drawing.Size(704, 404);
            this.robotspeed_chart.TabIndex = 11;
            this.robotspeed_chart.Text = "c1";
            title8.Name = "Title1";
            title8.Text = "로봇속도";
            this.robotspeed_chart.Titles.Add(title8);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(1599, 573);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 25);
            this.label1.TabIndex = 16;
            this.label1.Text = "Cam2";
            // 
            // Cam1
            // 
            this.Cam1.AutoSize = true;
            this.Cam1.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Cam1.Location = new System.Drawing.Point(1323, 573);
            this.Cam1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Cam1.Name = "Cam1";
            this.Cam1.Size = new System.Drawing.Size(63, 25);
            this.Cam1.TabIndex = 17;
            this.Cam1.Text = "Cam1";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(1328, 601);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(272, 255);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(1599, 601);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(265, 255);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // progressBar_capacity
            // 
            this.progressBar_capacity.BackColor = System.Drawing.Color.Blue;
            this.progressBar_capacity.Location = new System.Drawing.Point(305, 43);
            this.progressBar_capacity.Name = "progressBar_capacity";
            this.progressBar_capacity.Size = new System.Drawing.Size(121, 28);
            this.progressBar_capacity.TabIndex = 17;
            this.progressBar_capacity.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.progressBar_capacity.TextColor = System.Drawing.SystemColors.ControlText;
            this.progressBar_capacity.TextFont = new System.Drawing.Font("Times New Roman", 10F);
            this.progressBar_capacity.TextMargin = new System.Drawing.Point(1, 1);
            // 
            // RoboMonitoringCtrl_1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.Cam1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.lblGoodsStatus);
            this.Controls.Add(this.txtTemper1);
            this.Controls.Add(this.chart_battery);
            this.Controls.Add(this.txtRobotModel);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.progressBar_capacity);
            this.Controls.Add(this.robotspeed_chart);
            this.Controls.Add(this.label40);
            this.Controls.Add(this.label39);
            this.Controls.Add(this.lblBatterycapacity);
            this.Controls.Add(this.Robot_pictureBox);
            this.Controls.Add(this.lblLamp_Warnnig);
            this.Controls.Add(this.lblLamp_idle);
            this.Controls.Add(this.lblLamp_Run);
            this.Name = "RoboMonitoringCtrl_1";
            this.Size = new System.Drawing.Size(1890, 960);
            this.Load += new System.EventHandler(this.RoboMonitoringCtrl_1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart_globalpath)).EndInit();
            this.groupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart_angluar)).EndInit();
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radarchart1)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Robot_pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_battery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.robotspeed_chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_globalpath;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label lblangular;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_angluar;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.DataVisualization.Charting.Chart radarchart1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer_BatteryLogSave;
        private System.Windows.Forms.Timer myTimer;
        private System.Windows.Forms.Timer UI_Updatetimer;
        private System.Windows.Forms.Label lblGoodsStatus;
        private System.Windows.Forms.TextBox txtTemper1;
        private System.Windows.Forms.TextBox txtRobotModel;
        private System.Windows.Forms.Label label24;
        private aProgressBar progressBar_capacity;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label lblBatterycapacity;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label lblLamp_Warnnig;
        private System.Windows.Forms.Label lblLamp_Run;
        private System.Windows.Forms.PictureBox Robot_pictureBox;
        private System.Windows.Forms.Label lblLamp_idle;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_battery;
        private System.Windows.Forms.DataVisualization.Charting.Chart robotspeed_chart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Cam1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
