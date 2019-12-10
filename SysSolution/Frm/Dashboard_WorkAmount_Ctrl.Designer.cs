namespace SysSolution.Frm
{
    partial class Dashboard_WorkAmount_Ctrl
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.lblCurrWorkAmount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblWorkGoalAmount = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chart_WorkAmount = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.UI_Updatetimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_WorkAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCurrWorkAmount
            // 
            this.lblCurrWorkAmount.Font = new System.Drawing.Font("굴림", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCurrWorkAmount.ForeColor = System.Drawing.Color.Blue;
            this.lblCurrWorkAmount.Location = new System.Drawing.Point(49, 74);
            this.lblCurrWorkAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrWorkAmount.Name = "lblCurrWorkAmount";
            this.lblCurrWorkAmount.Size = new System.Drawing.Size(583, 144);
            this.lblCurrWorkAmount.TabIndex = 0;
            this.lblCurrWorkAmount.Text = "150";
            this.lblCurrWorkAmount.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(594, 165);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 144);
            this.label1.TabIndex = 0;
            this.label1.Text = "/";
            // 
            // lblWorkGoalAmount
            // 
            this.lblWorkGoalAmount.Font = new System.Drawing.Font("굴림", 50F, System.Drawing.FontStyle.Bold);
            this.lblWorkGoalAmount.ForeColor = System.Drawing.Color.Black;
            this.lblWorkGoalAmount.Location = new System.Drawing.Point(719, 183);
            this.lblWorkGoalAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWorkGoalAmount.Name = "lblWorkGoalAmount";
            this.lblWorkGoalAmount.Size = new System.Drawing.Size(457, 100);
            this.lblWorkGoalAmount.TabIndex = 0;
            this.lblWorkGoalAmount.Text = "500";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblCurrWorkAmount);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblWorkGoalAmount);
            this.groupBox1.Location = new System.Drawing.Point(49, 34);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(1221, 370);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "현재 생산 수량";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("굴림", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(707, 284);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(230, 68);
            this.label3.TabIndex = 0;
            this.label3.Text = "(목표량)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("굴림", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(383, 218);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(230, 68);
            this.label2.TabIndex = 0;
            this.label2.Text = "(생산량)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // chart_WorkAmount
            // 
            chartArea1.Name = "ChartArea1";
            this.chart_WorkAmount.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart_WorkAmount.Legends.Add(legend1);
            this.chart_WorkAmount.Location = new System.Drawing.Point(414, 429);
            this.chart_WorkAmount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chart_WorkAmount.Name = "chart_WorkAmount";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart_WorkAmount.Series.Add(series1);
            this.chart_WorkAmount.Size = new System.Drawing.Size(469, 282);
            this.chart_WorkAmount.TabIndex = 2;
            this.chart_WorkAmount.Text = "chart1";
            title1.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            title1.Name = "Title1";
            title1.Text = "생산량";
            this.chart_WorkAmount.Titles.Add(title1);
            // 
            // UI_Updatetimer
            // 
            this.UI_Updatetimer.Tick += new System.EventHandler(this.UI_Updatetimer_Tick);
            // 
            // Dashboard_WorkAmount_Ctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.Controls.Add(this.chart_WorkAmount);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Dashboard_WorkAmount_Ctrl";
            this.Size = new System.Drawing.Size(1357, 735);
            this.Load += new System.EventHandler(this.Dashboard_WorkAmount_Ctrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_WorkAmount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCurrWorkAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblWorkGoalAmount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_WorkAmount;
        private System.Windows.Forms.Timer UI_Updatetimer;
    }
}
