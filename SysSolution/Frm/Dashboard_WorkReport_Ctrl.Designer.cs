namespace SysSolution.Frm
{
    partial class Dashboard_WorkReport_Ctrl
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lblCurrWorkAmount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblcurrTime = new System.Windows.Forms.Label();
            this.lblremainTime = new System.Windows.Forms.Label();
            this.chart_Worktime = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.chart_Worktime)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCurrWorkAmount
            // 
            this.lblCurrWorkAmount.AutoSize = true;
            this.lblCurrWorkAmount.Font = new System.Drawing.Font("굴림", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCurrWorkAmount.ForeColor = System.Drawing.Color.Green;
            this.lblCurrWorkAmount.Location = new System.Drawing.Point(8, 238);
            this.lblCurrWorkAmount.Name = "lblCurrWorkAmount";
            this.lblCurrWorkAmount.Size = new System.Drawing.Size(715, 96);
            this.lblCurrWorkAmount.TabIndex = 1;
            this.lblCurrWorkAmount.Text = "남은 작업 시간:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(8, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(715, 96);
            this.label1.TabIndex = 1;
            this.label1.Text = "현재 작업 시간:";
            // 
            // lblcurrTime
            // 
            this.lblcurrTime.AutoSize = true;
            this.lblcurrTime.Font = new System.Drawing.Font("굴림", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblcurrTime.ForeColor = System.Drawing.Color.Black;
            this.lblcurrTime.Location = new System.Drawing.Point(729, 112);
            this.lblcurrTime.Name = "lblcurrTime";
            this.lblcurrTime.Size = new System.Drawing.Size(529, 96);
            this.lblcurrTime.TabIndex = 1;
            this.lblcurrTime.Text = "2시간 30분";
            // 
            // lblremainTime
            // 
            this.lblremainTime.AutoSize = true;
            this.lblremainTime.Font = new System.Drawing.Font("굴림", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblremainTime.ForeColor = System.Drawing.Color.Black;
            this.lblremainTime.Location = new System.Drawing.Point(729, 238);
            this.lblremainTime.Name = "lblremainTime";
            this.lblremainTime.Size = new System.Drawing.Size(529, 96);
            this.lblremainTime.TabIndex = 1;
            this.lblremainTime.Text = "3시간 10분";
            // 
            // chart_Worktime
            // 
            chartArea2.Name = "ChartArea1";
            this.chart_Worktime.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart_Worktime.Legends.Add(legend2);
            this.chart_Worktime.Location = new System.Drawing.Point(1450, 53);
            this.chart_Worktime.Name = "chart_Worktime";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart_Worktime.Series.Add(series2);
            this.chart_Worktime.Size = new System.Drawing.Size(429, 367);
            this.chart_Worktime.TabIndex = 2;
            this.chart_Worktime.Text = "chart1";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Dashboard_WorkReport_Ctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chart_Worktime);
            this.Controls.Add(this.lblremainTime);
            this.Controls.Add(this.lblcurrTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCurrWorkAmount);
            this.Name = "Dashboard_WorkReport_Ctrl";
            this.Size = new System.Drawing.Size(1906, 490);
            this.Load += new System.EventHandler(this.Dashboard_WorkReport_Ctrl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart_Worktime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCurrWorkAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblcurrTime;
        private System.Windows.Forms.Label lblremainTime;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Worktime;
        private System.Windows.Forms.Timer timer1;
    }
}
