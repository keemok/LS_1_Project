namespace SysSolution.Frm
{
    partial class Dashboard_RobotStatusCtrl
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
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView_errorrobot = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView_idlerobot = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView_runingrobot = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbldeadcnt = new System.Windows.Forms.Label();
            this.lblidlecnt = new System.Windows.Forms.Label();
            this.lblWorkcnt = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_errorrobot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_idlerobot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_runingrobot)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 50F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(650, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 67);
            this.label3.TabIndex = 1;
            this.label3.Text = "OFF";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView_errorrobot);
            this.groupBox1.Controls.Add(this.dataGridView_idlerobot);
            this.groupBox1.Controls.Add(this.dataGridView_runingrobot);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lbldeadcnt);
            this.groupBox1.Controls.Add(this.lblidlecnt);
            this.groupBox1.Controls.Add(this.lblWorkcnt);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(34, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(881, 435);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "로봇 상태";
            // 
            // dataGridView_errorrobot
            // 
            this.dataGridView_errorrobot.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_errorrobot.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dataGridView_errorrobot.Location = new System.Drawing.Point(618, 247);
            this.dataGridView_errorrobot.Name = "dataGridView_errorrobot";
            this.dataGridView_errorrobot.RowTemplate.Height = 23;
            this.dataGridView_errorrobot.Size = new System.Drawing.Size(243, 150);
            this.dataGridView_errorrobot.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "로봇ID";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "로봇이름";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridView_idlerobot
            // 
            this.dataGridView_idlerobot.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_idlerobot.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dataGridView_idlerobot.Location = new System.Drawing.Point(318, 247);
            this.dataGridView_idlerobot.Name = "dataGridView_idlerobot";
            this.dataGridView_idlerobot.RowTemplate.Height = 23;
            this.dataGridView_idlerobot.Size = new System.Drawing.Size(243, 150);
            this.dataGridView_idlerobot.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "로봇ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "로봇이름";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridView_runingrobot
            // 
            this.dataGridView_runingrobot.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_runingrobot.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridView_runingrobot.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridView_runingrobot.Location = new System.Drawing.Point(18, 247);
            this.dataGridView_runingrobot.Name = "dataGridView_runingrobot";
            this.dataGridView_runingrobot.RowTemplate.Height = 23;
            this.dataGridView_runingrobot.Size = new System.Drawing.Size(243, 150);
            this.dataGridView_runingrobot.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "로봇ID";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "로봇이름";
            this.Column2.Name = "Column2";
            // 
            // lbldeadcnt
            // 
            this.lbldeadcnt.Font = new System.Drawing.Font("굴림", 50F, System.Drawing.FontStyle.Bold);
            this.lbldeadcnt.ForeColor = System.Drawing.Color.Black;
            this.lbldeadcnt.Location = new System.Drawing.Point(608, 146);
            this.lbldeadcnt.Name = "lbldeadcnt";
            this.lbldeadcnt.Size = new System.Drawing.Size(253, 67);
            this.lbldeadcnt.TabIndex = 1;
            this.lbldeadcnt.Text = "1";
            this.lbldeadcnt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblidlecnt
            // 
            this.lblidlecnt.Font = new System.Drawing.Font("굴림", 50F, System.Drawing.FontStyle.Bold);
            this.lblidlecnt.ForeColor = System.Drawing.Color.Black;
            this.lblidlecnt.Location = new System.Drawing.Point(307, 146);
            this.lblidlecnt.Name = "lblidlecnt";
            this.lblidlecnt.Size = new System.Drawing.Size(253, 67);
            this.lblidlecnt.TabIndex = 1;
            this.lblidlecnt.Text = "3";
            this.lblidlecnt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWorkcnt
            // 
            this.lblWorkcnt.Font = new System.Drawing.Font("굴림", 50F, System.Drawing.FontStyle.Bold);
            this.lblWorkcnt.ForeColor = System.Drawing.Color.Black;
            this.lblWorkcnt.Location = new System.Drawing.Point(8, 146);
            this.lblWorkcnt.Name = "lblWorkcnt";
            this.lblWorkcnt.Size = new System.Drawing.Size(253, 67);
            this.lblWorkcnt.TabIndex = 1;
            this.lblWorkcnt.Text = "4";
            this.lblWorkcnt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("굴림", 50F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(8, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(253, 67);
            this.label4.TabIndex = 1;
            this.label4.Text = "작동 중";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("굴림", 50F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Green;
            this.label5.Location = new System.Drawing.Point(356, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 67);
            this.label5.TabIndex = 1;
            this.label5.Text = "Idle";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Dashboard_RobotStatusCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.Controls.Add(this.groupBox1);
            this.Name = "Dashboard_RobotStatusCtrl";
            this.Size = new System.Drawing.Size(950, 490);
            this.Load += new System.EventHandler(this.Dashboard_RobotStatusCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_errorrobot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_idlerobot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_runingrobot)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbldeadcnt;
        private System.Windows.Forms.Label lblidlecnt;
        private System.Windows.Forms.Label lblWorkcnt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView_errorrobot;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridView dataGridView_idlerobot;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridView dataGridView_runingrobot;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Timer timer1;
    }
}
