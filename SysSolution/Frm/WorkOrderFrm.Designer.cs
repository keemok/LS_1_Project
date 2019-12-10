namespace SysSolution.Frm
{
    partial class WorkOrderFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.listBox_robot = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView_RegWorklist = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtWorkName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkRuntime = new System.Windows.Forms.CheckBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnPauseRestart = new System.Windows.Forms.Button();
            this.numericUpDown_RepeatTime = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioButton_avoid = new System.Windows.Forms.RadioButton();
            this.radioButton_stop = new System.Windows.Forms.RadioButton();
            this.chklevelrunnig = new System.Windows.Forms.CheckBox();
            this.dataGridView_WorkingRobotList = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.작업중 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.robotid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Workcnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrWorkcnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actionidx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_RegWorklist)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RepeatTime)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_WorkingRobotList)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.dataGridView_RegWorklist);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(109, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1675, 394);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.listBox_robot);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Location = new System.Drawing.Point(897, 45);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(287, 303);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            // 
            // listBox_robot
            // 
            this.listBox_robot.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listBox_robot.FormattingEnabled = true;
            this.listBox_robot.ItemHeight = 21;
            this.listBox_robot.Location = new System.Drawing.Point(10, 35);
            this.listBox_robot.Name = "listBox_robot";
            this.listBox_robot.Size = new System.Drawing.Size(264, 256);
            this.listBox_robot.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(6, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "작업 로봇 리스트:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridView_RegWorklist
            // 
            this.dataGridView_RegWorklist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_RegWorklist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.작업중,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.dataGridView_RegWorklist.Location = new System.Drawing.Point(35, 54);
            this.dataGridView_RegWorklist.Name = "dataGridView_RegWorklist";
            this.dataGridView_RegWorklist.RowTemplate.Height = 23;
            this.dataGridView_RegWorklist.Size = new System.Drawing.Size(794, 286);
            this.dataGridView_RegWorklist.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(31, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(148, 20);
            this.label6.TabIndex = 17;
            this.label6.Text = "등록된 작업 목록 :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtWorkName);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.chkRuntime);
            this.groupBox2.Controls.Add(this.btnRun);
            this.groupBox2.Controls.Add(this.btnPauseRestart);
            this.groupBox2.Controls.Add(this.numericUpDown_RepeatTime);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnStop);
            this.groupBox2.Location = new System.Drawing.Point(1083, 465);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(701, 332);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // txtWorkName
            // 
            this.txtWorkName.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtWorkName.Location = new System.Drawing.Point(152, 44);
            this.txtWorkName.Name = "txtWorkName";
            this.txtWorkName.Size = new System.Drawing.Size(242, 32);
            this.txtWorkName.TabIndex = 34;
            this.txtWorkName.Text = "파레트작업";
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.Location = new System.Drawing.Point(38, 36);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(123, 40);
            this.label10.TabIndex = 30;
            this.label10.Text = "작업명 : ";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(40, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 19;
            this.label1.Text = "반복 시간:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkRuntime
            // 
            this.chkRuntime.AutoSize = true;
            this.chkRuntime.Location = new System.Drawing.Point(45, 102);
            this.chkRuntime.Name = "chkRuntime";
            this.chkRuntime.Size = new System.Drawing.Size(104, 16);
            this.chkRuntime.TabIndex = 28;
            this.chkRuntime.Text = "반복 시간 선택";
            this.chkRuntime.UseVisualStyleBackColor = true;
            // 
            // btnRun
            // 
            this.btnRun.Font = new System.Drawing.Font("굴림", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRun.ForeColor = System.Drawing.Color.Blue;
            this.btnRun.Location = new System.Drawing.Point(45, 198);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(381, 71);
            this.btnRun.TabIndex = 21;
            this.btnRun.Text = "동작";
            this.btnRun.UseVisualStyleBackColor = true;
            // 
            // btnPauseRestart
            // 
            this.btnPauseRestart.Location = new System.Drawing.Point(514, 111);
            this.btnPauseRestart.Name = "btnPauseRestart";
            this.btnPauseRestart.Size = new System.Drawing.Size(134, 49);
            this.btnPauseRestart.TabIndex = 33;
            this.btnPauseRestart.Text = "일시정지";
            this.btnPauseRestart.UseVisualStyleBackColor = true;
            // 
            // numericUpDown_RepeatTime
            // 
            this.numericUpDown_RepeatTime.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.numericUpDown_RepeatTime.Location = new System.Drawing.Point(131, 127);
            this.numericUpDown_RepeatTime.Name = "numericUpDown_RepeatTime";
            this.numericUpDown_RepeatTime.Size = new System.Drawing.Size(69, 29);
            this.numericUpDown_RepeatTime.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(206, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 20);
            this.label2.TabIndex = 19;
            this.label2.Text = "(분)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnStop.ForeColor = System.Drawing.Color.Red;
            this.btnStop.Location = new System.Drawing.Point(514, 194);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(136, 82);
            this.btnStop.TabIndex = 31;
            this.btnStop.Text = "중지";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chklevelrunnig);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Location = new System.Drawing.Point(1213, 45);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(364, 290);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "작업 옵션";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radioButton_stop);
            this.groupBox5.Controls.Add(this.radioButton_avoid);
            this.groupBox5.Location = new System.Drawing.Point(20, 20);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(317, 66);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            // 
            // radioButton_avoid
            // 
            this.radioButton_avoid.AutoSize = true;
            this.radioButton_avoid.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.radioButton_avoid.Location = new System.Drawing.Point(30, 28);
            this.radioButton_avoid.Name = "radioButton_avoid";
            this.radioButton_avoid.Size = new System.Drawing.Size(77, 19);
            this.radioButton_avoid.TabIndex = 0;
            this.radioButton_avoid.TabStop = true;
            this.radioButton_avoid.Text = "회피 모드";
            this.radioButton_avoid.UseVisualStyleBackColor = true;
            // 
            // radioButton_stop
            // 
            this.radioButton_stop.AutoSize = true;
            this.radioButton_stop.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.radioButton_stop.Location = new System.Drawing.Point(132, 28);
            this.radioButton_stop.Name = "radioButton_stop";
            this.radioButton_stop.Size = new System.Drawing.Size(77, 19);
            this.radioButton_stop.TabIndex = 0;
            this.radioButton_stop.TabStop = true;
            this.radioButton_stop.Text = "정지 모드";
            this.radioButton_stop.UseVisualStyleBackColor = true;
            // 
            // chklevelrunnig
            // 
            this.chklevelrunnig.AutoSize = true;
            this.chklevelrunnig.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chklevelrunnig.Location = new System.Drawing.Point(25, 105);
            this.chklevelrunnig.Name = "chklevelrunnig";
            this.chklevelrunnig.Size = new System.Drawing.Size(102, 19);
            this.chklevelrunnig.TabIndex = 1;
            this.chklevelrunnig.Text = "우선순위 주행";
            this.chklevelrunnig.UseVisualStyleBackColor = true;
            // 
            // dataGridView_WorkingRobotList
            // 
            this.dataGridView_WorkingRobotList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_WorkingRobotList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.robotid,
            this.dataGridViewTextBoxColumn1,
            this.workid,
            this.status,
            this.Workcnt,
            this.CurrWorkcnt,
            this.actionidx});
            this.dataGridView_WorkingRobotList.Location = new System.Drawing.Point(55, 40);
            this.dataGridView_WorkingRobotList.Name = "dataGridView_WorkingRobotList";
            this.dataGridView_WorkingRobotList.RowTemplate.Height = 23;
            this.dataGridView_WorkingRobotList.Size = new System.Drawing.Size(774, 273);
            this.dataGridView_WorkingRobotList.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(52, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(182, 20);
            this.label3.TabIndex = 28;
            this.label3.Text = "현재 로봇들 작업내용:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dataGridView_WorkingRobotList);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Location = new System.Drawing.Point(109, 465);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(896, 332);
            this.groupBox6.TabIndex = 30;
            this.groupBox6.TabStop = false;
            // 
            // 작업중
            // 
            this.작업중.HeaderText = "작업중";
            this.작업중.Name = "작업중";
            this.작업중.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.작업중.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "작업명";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "맵 정보";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "등록된 로봇 수";
            this.Column3.Name = "Column3";
            this.Column3.Width = 150;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "작업 레벨";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "작업 횟수";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "작업 시간";
            this.Column6.Name = "Column6";
            // 
            // robotid
            // 
            this.robotid.HeaderText = "Robot ID";
            this.robotid.Name = "robotid";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Robot IP";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // workid
            // 
            this.workid.HeaderText = "Work ID";
            this.workid.Name = "workid";
            // 
            // status
            // 
            this.status.HeaderText = "Status";
            this.status.Name = "status";
            this.status.Width = 50;
            // 
            // Workcnt
            // 
            this.Workcnt.HeaderText = "작업 총 횟수";
            this.Workcnt.Name = "Workcnt";
            // 
            // CurrWorkcnt
            // 
            this.CurrWorkcnt.HeaderText = "현재작업횟수";
            this.CurrWorkcnt.Name = "CurrWorkcnt";
            // 
            // actionidx
            // 
            this.actionidx.HeaderText = "action idx";
            this.actionidx.Name = "actionidx";
            // 
            // WorkOrderFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "WorkOrderFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "작업지시 화면";
            this.Load += new System.EventHandler(this.WorkOrderFrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_RegWorklist)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RepeatTime)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_WorkingRobotList)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView_RegWorklist;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnPauseRestart;
        private System.Windows.Forms.CheckBox chkRuntime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown_RepeatTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtWorkName;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox listBox_robot;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chklevelrunnig;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioButton_stop;
        private System.Windows.Forms.RadioButton radioButton_avoid;
        private System.Windows.Forms.DataGridView dataGridView_WorkingRobotList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 작업중;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn robotid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn workid;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Workcnt;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrWorkcnt;
        private System.Windows.Forms.DataGridViewTextBoxColumn actionidx;
    }
}