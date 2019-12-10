namespace SysSolution.FleetManager
{
    partial class SettingMainCtrl
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
            this.btnRobotReg = new System.Windows.Forms.Button();
            this.btnXisReg = new System.Windows.Forms.Button();
            this.btnJobReg = new System.Windows.Forms.Button();
            this.panel_setting = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRobotReg
            // 
            this.btnRobotReg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRobotReg.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRobotReg.Location = new System.Drawing.Point(22, 87);
            this.btnRobotReg.Name = "btnRobotReg";
            this.btnRobotReg.Size = new System.Drawing.Size(132, 34);
            this.btnRobotReg.TabIndex = 0;
            this.btnRobotReg.Text = "로봇 등록";
            this.btnRobotReg.UseVisualStyleBackColor = true;
            this.btnRobotReg.Click += new System.EventHandler(this.btnRobotReg_Click);
            this.btnRobotReg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);
            this.btnRobotReg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MouseUp);
            // 
            // btnXisReg
            // 
            this.btnXisReg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXisReg.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnXisReg.Location = new System.Drawing.Point(22, 127);
            this.btnXisReg.Name = "btnXisReg";
            this.btnXisReg.Size = new System.Drawing.Size(132, 34);
            this.btnXisReg.TabIndex = 0;
            this.btnXisReg.Text = "XIS 등록";
            this.btnXisReg.UseVisualStyleBackColor = true;
            this.btnXisReg.Click += new System.EventHandler(this.btnXisReg_Click);
            this.btnXisReg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);
            this.btnXisReg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MouseUp);
            // 
            // btnJobReg
            // 
            this.btnJobReg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnJobReg.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnJobReg.Location = new System.Drawing.Point(22, 47);
            this.btnJobReg.Name = "btnJobReg";
            this.btnJobReg.Size = new System.Drawing.Size(132, 34);
            this.btnJobReg.TabIndex = 0;
            this.btnJobReg.Text = "작업 등록";
            this.btnJobReg.UseVisualStyleBackColor = true;
            this.btnJobReg.Click += new System.EventHandler(this.btnJobReg_Click);
            this.btnJobReg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);
            this.btnJobReg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MouseUp);
            // 
            // panel_setting
            // 
            this.panel_setting.BackColor = System.Drawing.Color.White;
            this.panel_setting.Location = new System.Drawing.Point(220, 90);
            this.panel_setting.Name = "panel_setting";
            this.panel_setting.Size = new System.Drawing.Size(894, 486);
            this.panel_setting.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Olive;
            this.panel1.Controls.Add(this.btnJobReg);
            this.panel1.Controls.Add(this.btnXisReg);
            this.panel1.Controls.Add(this.btnRobotReg);
            this.panel1.Location = new System.Drawing.Point(21, 90);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(177, 487);
            this.panel1.TabIndex = 2;
            // 
            // SettingMainCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel_setting);
            this.Name = "SettingMainCtrl";
            this.Size = new System.Drawing.Size(1760, 940);
            this.Load += new System.EventHandler(this.SettingMainCtrl_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRobotReg;
        private System.Windows.Forms.Button btnXisReg;
        private System.Windows.Forms.Button btnJobReg;
        private System.Windows.Forms.Panel panel_setting;
        private System.Windows.Forms.Panel panel1;
    }
}
