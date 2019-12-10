namespace SysSolution.FleetManager.Monitoring
{
    partial class MonitoringMain_Ctrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitoringMain_Ctrl));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRobotMonitor1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnRobotMonitor = new System.Windows.Forms.Button();
            this.panel_monitor = new System.Windows.Forms.Panel();
            this.dashboardBarAndDockingController1 = new DevExpress.DashboardWin.Native.DashboardBarAndDockingController(this.components);
            this.dashboardBarController1 = new DevExpress.DashboardWin.Bars.DashboardBarController(this.components);
            this.textBoxEditorBarController1 = new DevExpress.DashboardWin.Bars.TextBoxEditorBarController(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dashboardBarAndDockingController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dashboardBarController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxEditorBarController1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Honeydew;
            this.panel1.Controls.Add(this.btnRobotMonitor1);
            this.panel1.Location = new System.Drawing.Point(21, 230);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(177, 487);
            this.panel1.TabIndex = 4;
            // 
            // btnRobotMonitor1
            // 
            this.btnRobotMonitor1.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRobotMonitor1.Appearance.Options.UseFont = true;
            this.btnRobotMonitor1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRobotMonitor1.ImageOptions.Image")));
            this.btnRobotMonitor1.Location = new System.Drawing.Point(20, 36);
            this.btnRobotMonitor1.Name = "btnRobotMonitor1";
            this.btnRobotMonitor1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnRobotMonitor1.Size = new System.Drawing.Size(142, 38);
            this.btnRobotMonitor1.TabIndex = 5;
            this.btnRobotMonitor1.Text = "로봇모니터";
            this.btnRobotMonitor1.Click += new System.EventHandler(this.btnRobotMonitor1_Click);
            // 
            // btnRobotMonitor
            // 
            this.btnRobotMonitor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRobotMonitor.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRobotMonitor.Location = new System.Drawing.Point(336, 119);
            this.btnRobotMonitor.Name = "btnRobotMonitor";
            this.btnRobotMonitor.Size = new System.Drawing.Size(132, 34);
            this.btnRobotMonitor.TabIndex = 0;
            this.btnRobotMonitor.Text = "로봇 모니터";
            this.btnRobotMonitor.UseVisualStyleBackColor = true;
            this.btnRobotMonitor.Visible = false;
            this.btnRobotMonitor.Click += new System.EventHandler(this.btnRobotMonitor_Click);
            this.btnRobotMonitor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);
            this.btnRobotMonitor.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MouseUp);
            // 
            // panel_monitor
            // 
            this.panel_monitor.BackColor = System.Drawing.Color.White;
            this.panel_monitor.Location = new System.Drawing.Point(220, 230);
            this.panel_monitor.Name = "panel_monitor";
            this.panel_monitor.Size = new System.Drawing.Size(894, 486);
            this.panel_monitor.TabIndex = 3;
            // 
            // textBoxEditorBarController1
            // 
            this.textBoxEditorBarController1.Designer = null;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MonitoringMain_Ctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnRobotMonitor);
            this.Controls.Add(this.panel_monitor);
            this.Name = "MonitoringMain_Ctrl";
            this.Size = new System.Drawing.Size(1760, 940);
            this.Load += new System.EventHandler(this.MonitoringMain_Ctrl_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dashboardBarAndDockingController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dashboardBarController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxEditorBarController1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRobotMonitor;
        private System.Windows.Forms.Panel panel_monitor;
        private DevExpress.DashboardWin.Native.DashboardBarAndDockingController dashboardBarAndDockingController1;
        private DevExpress.DashboardWin.Bars.DashboardBarController dashboardBarController1;
        private DevExpress.DashboardWin.Bars.TextBoxEditorBarController textBoxEditorBarController1;
        private DevExpress.XtraEditors.SimpleButton btnRobotMonitor1;
        private System.Windows.Forms.Timer timer1;
    }
}
