namespace SysSolution.FleetManager.Demo
{
    partial class JobOrder_Demo_ctrl
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
            this.groupBox_btn = new System.Windows.Forms.GroupBox();
            this.txtJobname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtJobCnt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnJobStop = new System.Windows.Forms.Button();
            this.btnJobRun = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblActionidx = new System.Windows.Forms.Label();
            this.lblRobotJobCnt = new System.Windows.Forms.Label();
            this.dataGridView_reg = new System.Windows.Forms.DataGridView();
            this.jobstatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jobid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jobname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.missionlist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unloadmissionlist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.waitmissionlist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.robotlist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calltype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jobgroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnliftdown = new System.Windows.Forms.Button();
            this.btnliftup = new System.Windows.Forms.Button();
            this.btnLiftSet = new System.Windows.Forms.Button();
            this.cboliftrobotID = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboRobot_lift = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnAllStop = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.chkCrashStop = new System.Windows.Forms.CheckBox();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnRestart = new System.Windows.Forms.Button();
            this.cboRobotID = new System.Windows.Forms.ComboBox();
            this.groupBox_btn.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_reg)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_btn
            // 
            this.groupBox_btn.Controls.Add(this.txtJobname);
            this.groupBox_btn.Controls.Add(this.label1);
            this.groupBox_btn.Controls.Add(this.txtJobCnt);
            this.groupBox_btn.Controls.Add(this.label2);
            this.groupBox_btn.Controls.Add(this.btnJobStop);
            this.groupBox_btn.Controls.Add(this.btnJobRun);
            this.groupBox_btn.Location = new System.Drawing.Point(51, 340);
            this.groupBox_btn.Name = "groupBox_btn";
            this.groupBox_btn.Size = new System.Drawing.Size(944, 56);
            this.groupBox_btn.TabIndex = 12;
            this.groupBox_btn.TabStop = false;
            // 
            // txtJobname
            // 
            this.txtJobname.Enabled = false;
            this.txtJobname.Location = new System.Drawing.Point(394, 20);
            this.txtJobname.Name = "txtJobname";
            this.txtJobname.Size = new System.Drawing.Size(113, 21);
            this.txtJobname.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.BurlyWood;
            this.label1.Location = new System.Drawing.Point(341, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 13;
            this.label1.Text = "작업명";
            // 
            // txtJobCnt
            // 
            this.txtJobCnt.Location = new System.Drawing.Point(616, 20);
            this.txtJobCnt.Name = "txtJobCnt";
            this.txtJobCnt.Size = new System.Drawing.Size(113, 21);
            this.txtJobCnt.TabIndex = 14;
            this.txtJobCnt.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.BurlyWood;
            this.label2.Location = new System.Drawing.Point(545, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "작업 횟수";
            // 
            // btnJobStop
            // 
            this.btnJobStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnJobStop.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnJobStop.Location = new System.Drawing.Point(850, 17);
            this.btnJobStop.Name = "btnJobStop";
            this.btnJobStop.Size = new System.Drawing.Size(88, 33);
            this.btnJobStop.TabIndex = 7;
            this.btnJobStop.Text = "작업 정지";
            this.btnJobStop.UseVisualStyleBackColor = false;
            this.btnJobStop.Click += new System.EventHandler(this.btnJobStop_Click);
            // 
            // btnJobRun
            // 
            this.btnJobRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnJobRun.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnJobRun.Location = new System.Drawing.Point(749, 17);
            this.btnJobRun.Name = "btnJobRun";
            this.btnJobRun.Size = new System.Drawing.Size(88, 33);
            this.btnJobRun.TabIndex = 9;
            this.btnJobRun.Text = "작업 시작";
            this.btnJobRun.UseVisualStyleBackColor = false;
            this.btnJobRun.Click += new System.EventHandler(this.btnJobRun_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblActionidx);
            this.groupBox1.Controls.Add(this.lblRobotJobCnt);
            this.groupBox1.Location = new System.Drawing.Point(168, 459);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(706, 228);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(397, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 21);
            this.label5.TabIndex = 1;
            this.label5.Text = "현재 작업시간";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(58, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 21);
            this.label4.TabIndex = 1;
            this.label4.Text = "현재 작업량";
            // 
            // lblActionidx
            // 
            this.lblActionidx.Font = new System.Drawing.Font("맑은 고딕", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblActionidx.ForeColor = System.Drawing.Color.Blue;
            this.lblActionidx.Location = new System.Drawing.Point(363, 66);
            this.lblActionidx.Name = "lblActionidx";
            this.lblActionidx.Size = new System.Drawing.Size(337, 100);
            this.lblActionidx.TabIndex = 0;
            this.lblActionidx.Text = "label3";
            this.lblActionidx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRobotJobCnt
            // 
            this.lblRobotJobCnt.Font = new System.Drawing.Font("맑은 고딕", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblRobotJobCnt.ForeColor = System.Drawing.Color.Blue;
            this.lblRobotJobCnt.Location = new System.Drawing.Point(51, 66);
            this.lblRobotJobCnt.Name = "lblRobotJobCnt";
            this.lblRobotJobCnt.Size = new System.Drawing.Size(247, 100);
            this.lblRobotJobCnt.TabIndex = 0;
            this.lblRobotJobCnt.Text = "label3";
            this.lblRobotJobCnt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridView_reg
            // 
            this.dataGridView_reg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_reg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.jobstatus,
            this.jobid,
            this.jobname,
            this.missionlist,
            this.unloadmissionlist,
            this.waitmissionlist,
            this.robotlist,
            this.calltype,
            this.jobgroup});
            this.dataGridView_reg.Location = new System.Drawing.Point(51, 35);
            this.dataGridView_reg.Name = "dataGridView_reg";
            this.dataGridView_reg.RowTemplate.Height = 23;
            this.dataGridView_reg.Size = new System.Drawing.Size(944, 299);
            this.dataGridView_reg.TabIndex = 14;
            this.dataGridView_reg.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_reg_CellClick);
            // 
            // jobstatus
            // 
            this.jobstatus.HeaderText = "작업 상태";
            this.jobstatus.Name = "jobstatus";
            // 
            // jobid
            // 
            this.jobid.HeaderText = "ID";
            this.jobid.Name = "jobid";
            // 
            // jobname
            // 
            this.jobname.HeaderText = "작업명";
            this.jobname.Name = "jobname";
            // 
            // missionlist
            // 
            this.missionlist.HeaderText = "미션리스트";
            this.missionlist.Name = "missionlist";
            // 
            // unloadmissionlist
            // 
            this.unloadmissionlist.HeaderText = "end 미션리스트";
            this.unloadmissionlist.Name = "unloadmissionlist";
            // 
            // waitmissionlist
            // 
            this.waitmissionlist.HeaderText = "wait 미션리스트";
            this.waitmissionlist.Name = "waitmissionlist";
            // 
            // robotlist
            // 
            this.robotlist.HeaderText = "로봇리스트";
            this.robotlist.Name = "robotlist";
            // 
            // calltype
            // 
            this.calltype.HeaderText = "작업방법";
            this.calltype.Name = "calltype";
            // 
            // jobgroup
            // 
            this.jobgroup.HeaderText = "작업그룹";
            this.jobgroup.Name = "jobgroup";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnliftdown);
            this.groupBox6.Controls.Add(this.btnliftup);
            this.groupBox6.Controls.Add(this.btnLiftSet);
            this.groupBox6.Controls.Add(this.cboliftrobotID);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Controls.Add(this.cboRobot_lift);
            this.groupBox6.Controls.Add(this.label11);
            this.groupBox6.Location = new System.Drawing.Point(168, 710);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox6.Size = new System.Drawing.Size(706, 134);
            this.groupBox6.TabIndex = 23;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "리프트 상태";
            // 
            // btnliftdown
            // 
            this.btnliftdown.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnliftdown.Location = new System.Drawing.Point(431, 64);
            this.btnliftdown.Margin = new System.Windows.Forms.Padding(2);
            this.btnliftdown.Name = "btnliftdown";
            this.btnliftdown.Size = new System.Drawing.Size(102, 30);
            this.btnliftdown.TabIndex = 23;
            this.btnliftdown.Text = "리프트 down";
            this.btnliftdown.UseVisualStyleBackColor = true;
            this.btnliftdown.Click += new System.EventHandler(this.btnliftdown_Click);
            // 
            // btnliftup
            // 
            this.btnliftup.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnliftup.Location = new System.Drawing.Point(318, 64);
            this.btnliftup.Margin = new System.Windows.Forms.Padding(2);
            this.btnliftup.Name = "btnliftup";
            this.btnliftup.Size = new System.Drawing.Size(102, 30);
            this.btnliftup.TabIndex = 23;
            this.btnliftup.Text = "리프트 up";
            this.btnliftup.UseVisualStyleBackColor = true;
            this.btnliftup.Click += new System.EventHandler(this.btnliftup_Click);
            // 
            // btnLiftSet
            // 
            this.btnLiftSet.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnLiftSet.Location = new System.Drawing.Point(596, 16);
            this.btnLiftSet.Margin = new System.Windows.Forms.Padding(2);
            this.btnLiftSet.Name = "btnLiftSet";
            this.btnLiftSet.Size = new System.Drawing.Size(76, 31);
            this.btnLiftSet.TabIndex = 23;
            this.btnLiftSet.Text = "설정";
            this.btnLiftSet.UseVisualStyleBackColor = true;
            this.btnLiftSet.Click += new System.EventHandler(this.btnLiftSet_Click);
            // 
            // cboliftrobotID
            // 
            this.cboliftrobotID.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.cboliftrobotID.FormattingEnabled = true;
            this.cboliftrobotID.Items.AddRange(new object[] {
            "R_005",
            "R_006"});
            this.cboliftrobotID.Location = new System.Drawing.Point(142, 19);
            this.cboliftrobotID.Name = "cboliftrobotID";
            this.cboliftrobotID.Size = new System.Drawing.Size(153, 25);
            this.cboliftrobotID.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(25, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 25);
            this.label3.TabIndex = 20;
            this.label3.Text = "로봇 ID";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboRobot_lift
            // 
            this.cboRobot_lift.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.cboRobot_lift.FormattingEnabled = true;
            this.cboRobot_lift.Items.AddRange(new object[] {
            "리프트 up",
            "리프트 down"});
            this.cboRobot_lift.Location = new System.Drawing.Point(431, 19);
            this.cboRobot_lift.Name = "cboRobot_lift";
            this.cboRobot_lift.Size = new System.Drawing.Size(153, 25);
            this.cboRobot_lift.TabIndex = 22;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label11.Location = new System.Drawing.Point(314, 18);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(120, 25);
            this.label11.TabIndex = 20;
            this.label11.Text = "리프트 초기설정";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAllStop
            // 
            this.btnAllStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnAllStop.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAllStop.Location = new System.Drawing.Point(888, 774);
            this.btnAllStop.Name = "btnAllStop";
            this.btnAllStop.Size = new System.Drawing.Size(107, 69);
            this.btnAllStop.TabIndex = 7;
            this.btnAllStop.Text = "강제 정지";
            this.btnAllStop.UseVisualStyleBackColor = false;
            this.btnAllStop.Click += new System.EventHandler(this.btnAllStop_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(1282, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(454, 807);
            this.panel1.TabIndex = 24;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // chkCrashStop
            // 
            this.chkCrashStop.AutoSize = true;
            this.chkCrashStop.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkCrashStop.Location = new System.Drawing.Point(866, 419);
            this.chkCrashStop.Name = "chkCrashStop";
            this.chkCrashStop.Size = new System.Drawing.Size(123, 34);
            this.chkCrashStop.TabIndex = 25;
            this.chkCrashStop.Text = "충돌 멈춤";
            this.chkCrashStop.UseVisualStyleBackColor = true;
            this.chkCrashStop.CheckedChanged += new System.EventHandler(this.chkCrashStop_CheckedChanged);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(1106, 523);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(116, 62);
            this.btnPause.TabIndex = 26;
            this.btnPause.Text = "일시정지";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(1106, 602);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(116, 62);
            this.btnRestart.TabIndex = 26;
            this.btnRestart.Text = "재시작";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // cboRobotID
            // 
            this.cboRobotID.Font = new System.Drawing.Font("맑은 고딕", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cboRobotID.FormattingEnabled = true;
            this.cboRobotID.Items.AddRange(new object[] {
            "R_005",
            "R_006",
            "R_007",
            "R_008"});
            this.cboRobotID.Location = new System.Drawing.Point(1069, 448);
            this.cboRobotID.Name = "cboRobotID";
            this.cboRobotID.Size = new System.Drawing.Size(171, 58);
            this.cboRobotID.TabIndex = 36;
            // 
            // JobOrder_Demo_ctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Controls.Add(this.cboRobotID);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.chkCrashStop);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.dataGridView_reg);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox_btn);
            this.Controls.Add(this.btnAllStop);
            this.Name = "JobOrder_Demo_ctrl";
            this.Size = new System.Drawing.Size(1760, 940);
            this.Load += new System.EventHandler(this.JobOrder_ctrl_Load);
            this.groupBox_btn.ResumeLayout(false);
            this.groupBox_btn.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_reg)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox_btn;
        private System.Windows.Forms.Button btnJobStop;
        private System.Windows.Forms.Button btnJobRun;
        private System.Windows.Forms.TextBox txtJobCnt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtJobname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView_reg;
        private System.Windows.Forms.DataGridViewTextBoxColumn jobstatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn jobid;
        private System.Windows.Forms.DataGridViewTextBoxColumn jobname;
        private System.Windows.Forms.DataGridViewTextBoxColumn missionlist;
        private System.Windows.Forms.DataGridViewTextBoxColumn unloadmissionlist;
        private System.Windows.Forms.DataGridViewTextBoxColumn waitmissionlist;
        private System.Windows.Forms.DataGridViewTextBoxColumn robotlist;
        private System.Windows.Forms.DataGridViewTextBoxColumn calltype;
        private System.Windows.Forms.DataGridViewTextBoxColumn jobgroup;
        private System.Windows.Forms.Label lblRobotJobCnt;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnliftdown;
        private System.Windows.Forms.Button btnliftup;
        private System.Windows.Forms.Button btnLiftSet;
        private System.Windows.Forms.ComboBox cboliftrobotID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboRobot_lift;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblActionidx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAllStop;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkCrashStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.ComboBox cboRobotID;
    }
}
