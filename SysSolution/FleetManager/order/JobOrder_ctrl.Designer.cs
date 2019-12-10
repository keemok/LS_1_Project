namespace SysSolution.FleetManager.order
{
    partial class JobOrder_ctrl
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
            this.groupBox_btn = new System.Windows.Forms.GroupBox();
            this.txtJobname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtJobCnt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnJobStop = new System.Windows.Forms.Button();
            this.btnJobRun = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.groupBox_btn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_reg)).BeginInit();
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
            this.btnJobStop.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnJobStop.Location = new System.Drawing.Point(850, 17);
            this.btnJobStop.Name = "btnJobStop";
            this.btnJobStop.Size = new System.Drawing.Size(88, 33);
            this.btnJobStop.TabIndex = 7;
            this.btnJobStop.Text = "작업 정지";
            this.btnJobStop.UseVisualStyleBackColor = true;
            this.btnJobStop.Click += new System.EventHandler(this.btnJobStop_Click);
            // 
            // btnJobRun
            // 
            this.btnJobRun.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnJobRun.Location = new System.Drawing.Point(749, 17);
            this.btnJobRun.Name = "btnJobRun";
            this.btnJobRun.Size = new System.Drawing.Size(88, 33);
            this.btnJobRun.TabIndex = 9;
            this.btnJobRun.Text = "작업 시작";
            this.btnJobRun.UseVisualStyleBackColor = true;
            this.btnJobRun.Click += new System.EventHandler(this.btnJobRun_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(51, 453);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(944, 342);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
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
            // JobOrder_ctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Controls.Add(this.dataGridView_reg);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox_btn);
            this.Name = "JobOrder_ctrl";
            this.Size = new System.Drawing.Size(1760, 940);
            this.Load += new System.EventHandler(this.JobOrder_ctrl_Load);
            this.groupBox_btn.ResumeLayout(false);
            this.groupBox_btn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_reg)).EndInit();
            this.ResumeLayout(false);

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
    }
}
