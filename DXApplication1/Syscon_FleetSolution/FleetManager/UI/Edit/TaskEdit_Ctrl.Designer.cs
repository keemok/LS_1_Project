namespace Syscon_Solution.FleetManager.UI.Edit
{
    partial class TaskEdit_Ctrl
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
            this.btnTaskUpdate = new System.Windows.Forms.Button();
            this.btnTaskDelete = new System.Windows.Forms.Button();
            this.btnTaskReg = new System.Windows.Forms.Button();
            this.btnRemoverobot = new System.Windows.Forms.Button();
            this.btnSelectrobot = new System.Windows.Forms.Button();
            this.btnRemovemission = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cboJobCallKind = new System.Windows.Forms.ComboBox();
            this.groupBox_jobitem = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboRobotGroup = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSelectmission = new System.Windows.Forms.Button();
            this.listBox_robotlist = new System.Windows.Forms.ListBox();
            this.listBox_selectedrobotlist = new System.Windows.Forms.ListBox();
            this.listBox_missionlist = new System.Windows.Forms.ListBox();
            this.listBox_selectedmissionlist = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtTaskName = new System.Windows.Forms.TextBox();
            this.txtTaskID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView_reg = new System.Windows.Forms.DataGridView();
            this.taskstatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.missionlist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.robotlist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskLoopflag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.robotgroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox_btn.SuspendLayout();
            this.groupBox_jobitem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_reg)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox_btn
            // 
            this.groupBox_btn.Controls.Add(this.btnTaskUpdate);
            this.groupBox_btn.Controls.Add(this.btnTaskDelete);
            this.groupBox_btn.Controls.Add(this.btnTaskReg);
            this.groupBox_btn.Location = new System.Drawing.Point(35, 345);
            this.groupBox_btn.Name = "groupBox_btn";
            this.groupBox_btn.Size = new System.Drawing.Size(315, 56);
            this.groupBox_btn.TabIndex = 15;
            this.groupBox_btn.TabStop = false;
            // 
            // btnTaskUpdate
            // 
            this.btnTaskUpdate.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnTaskUpdate.Location = new System.Drawing.Point(109, 13);
            this.btnTaskUpdate.Name = "btnTaskUpdate";
            this.btnTaskUpdate.Size = new System.Drawing.Size(88, 33);
            this.btnTaskUpdate.TabIndex = 7;
            this.btnTaskUpdate.Text = "변경";
            this.btnTaskUpdate.UseVisualStyleBackColor = true;
            this.btnTaskUpdate.Click += new System.EventHandler(this.btnTaskUpdate_Click);
            // 
            // btnTaskDelete
            // 
            this.btnTaskDelete.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnTaskDelete.Location = new System.Drawing.Point(210, 13);
            this.btnTaskDelete.Name = "btnTaskDelete";
            this.btnTaskDelete.Size = new System.Drawing.Size(88, 33);
            this.btnTaskDelete.TabIndex = 8;
            this.btnTaskDelete.Text = "삭제";
            this.btnTaskDelete.UseVisualStyleBackColor = true;
            this.btnTaskDelete.Click += new System.EventHandler(this.btnTaskDelete_Click);
            // 
            // btnTaskReg
            // 
            this.btnTaskReg.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnTaskReg.Location = new System.Drawing.Point(8, 13);
            this.btnTaskReg.Name = "btnTaskReg";
            this.btnTaskReg.Size = new System.Drawing.Size(88, 33);
            this.btnTaskReg.TabIndex = 9;
            this.btnTaskReg.Text = "추가";
            this.btnTaskReg.UseVisualStyleBackColor = true;
            this.btnTaskReg.Click += new System.EventHandler(this.btnTaskReg_Click);
            // 
            // btnRemoverobot
            // 
            this.btnRemoverobot.Location = new System.Drawing.Point(781, 160);
            this.btnRemoverobot.Name = "btnRemoverobot";
            this.btnRemoverobot.Size = new System.Drawing.Size(32, 23);
            this.btnRemoverobot.TabIndex = 9;
            this.btnRemoverobot.Text = ">>";
            this.btnRemoverobot.UseVisualStyleBackColor = true;
            this.btnRemoverobot.Click += new System.EventHandler(this.btnRemoverobot_Click);
            // 
            // btnSelectrobot
            // 
            this.btnSelectrobot.Location = new System.Drawing.Point(781, 131);
            this.btnSelectrobot.Name = "btnSelectrobot";
            this.btnSelectrobot.Size = new System.Drawing.Size(32, 23);
            this.btnSelectrobot.TabIndex = 9;
            this.btnSelectrobot.Text = "<<";
            this.btnSelectrobot.UseVisualStyleBackColor = true;
            this.btnSelectrobot.Click += new System.EventHandler(this.btnSelectrobot_Click);
            // 
            // btnRemovemission
            // 
            this.btnRemovemission.Location = new System.Drawing.Point(283, 160);
            this.btnRemovemission.Name = "btnRemovemission";
            this.btnRemovemission.Size = new System.Drawing.Size(32, 23);
            this.btnRemovemission.TabIndex = 9;
            this.btnRemovemission.Text = ">>";
            this.btnRemovemission.UseVisualStyleBackColor = true;
            this.btnRemovemission.Click += new System.EventHandler(this.btnRemovemission_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCancel.Location = new System.Drawing.Point(908, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 49);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "취소";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cboJobCallKind
            // 
            this.cboJobCallKind.FormattingEnabled = true;
            this.cboJobCallKind.Items.AddRange(new object[] {
            "Call",
            "Alone"});
            this.cboJobCallKind.Location = new System.Drawing.Point(137, 283);
            this.cboJobCallKind.Name = "cboJobCallKind";
            this.cboJobCallKind.Size = new System.Drawing.Size(118, 20);
            this.cboJobCallKind.TabIndex = 7;
            this.cboJobCallKind.Visible = false;
            // 
            // groupBox_jobitem
            // 
            this.groupBox_jobitem.Controls.Add(this.btnRemoverobot);
            this.groupBox_jobitem.Controls.Add(this.btnSelectrobot);
            this.groupBox_jobitem.Controls.Add(this.btnRemovemission);
            this.groupBox_jobitem.Controls.Add(this.btnCancel);
            this.groupBox_jobitem.Controls.Add(this.cboJobCallKind);
            this.groupBox_jobitem.Controls.Add(this.label7);
            this.groupBox_jobitem.Controls.Add(this.cboRobotGroup);
            this.groupBox_jobitem.Controls.Add(this.label6);
            this.groupBox_jobitem.Controls.Add(this.btnSave);
            this.groupBox_jobitem.Controls.Add(this.btnSelectmission);
            this.groupBox_jobitem.Controls.Add(this.listBox_robotlist);
            this.groupBox_jobitem.Controls.Add(this.listBox_selectedrobotlist);
            this.groupBox_jobitem.Controls.Add(this.listBox_missionlist);
            this.groupBox_jobitem.Controls.Add(this.listBox_selectedmissionlist);
            this.groupBox_jobitem.Controls.Add(this.textBox1);
            this.groupBox_jobitem.Controls.Add(this.txtTaskName);
            this.groupBox_jobitem.Controls.Add(this.txtTaskID);
            this.groupBox_jobitem.Controls.Add(this.label5);
            this.groupBox_jobitem.Controls.Add(this.label8);
            this.groupBox_jobitem.Controls.Add(this.label4);
            this.groupBox_jobitem.Controls.Add(this.label3);
            this.groupBox_jobitem.Controls.Add(this.label2);
            this.groupBox_jobitem.Location = new System.Drawing.Point(35, 407);
            this.groupBox_jobitem.Name = "groupBox_jobitem";
            this.groupBox_jobitem.Size = new System.Drawing.Size(1023, 332);
            this.groupBox_jobitem.TabIndex = 14;
            this.groupBox_jobitem.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.Location = new System.Drawing.Point(63, 283);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 17);
            this.label7.TabIndex = 6;
            this.label7.Text = "작업방법 :";
            this.label7.Visible = false;
            // 
            // cboRobotGroup
            // 
            this.cboRobotGroup.FormattingEnabled = true;
            this.cboRobotGroup.Location = new System.Drawing.Point(772, 30);
            this.cboRobotGroup.Name = "cboRobotGroup";
            this.cboRobotGroup.Size = new System.Drawing.Size(118, 20);
            this.cboRobotGroup.TabIndex = 7;
            this.cboRobotGroup.SelectedIndexChanged += new System.EventHandler(this.cboRobotGroup_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(698, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 17);
            this.label6.TabIndex = 6;
            this.label6.Text = "로봇그룹 :";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSave.Location = new System.Drawing.Point(781, 241);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 49);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "저장";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSelectmission
            // 
            this.btnSelectmission.Location = new System.Drawing.Point(283, 131);
            this.btnSelectmission.Name = "btnSelectmission";
            this.btnSelectmission.Size = new System.Drawing.Size(32, 23);
            this.btnSelectmission.TabIndex = 9;
            this.btnSelectmission.Text = "<<";
            this.btnSelectmission.UseVisualStyleBackColor = true;
            this.btnSelectmission.Click += new System.EventHandler(this.btnSelectmission_Click);
            // 
            // listBox_robotlist
            // 
            this.listBox_robotlist.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listBox_robotlist.FormattingEnabled = true;
            this.listBox_robotlist.ItemHeight = 20;
            this.listBox_robotlist.Location = new System.Drawing.Point(816, 86);
            this.listBox_robotlist.Name = "listBox_robotlist";
            this.listBox_robotlist.Size = new System.Drawing.Size(180, 144);
            this.listBox_robotlist.TabIndex = 8;
            // 
            // listBox_selectedrobotlist
            // 
            this.listBox_selectedrobotlist.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listBox_selectedrobotlist.FormattingEnabled = true;
            this.listBox_selectedrobotlist.ItemHeight = 20;
            this.listBox_selectedrobotlist.Location = new System.Drawing.Point(595, 86);
            this.listBox_selectedrobotlist.Name = "listBox_selectedrobotlist";
            this.listBox_selectedrobotlist.Size = new System.Drawing.Size(180, 144);
            this.listBox_selectedrobotlist.TabIndex = 8;
            // 
            // listBox_missionlist
            // 
            this.listBox_missionlist.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listBox_missionlist.FormattingEnabled = true;
            this.listBox_missionlist.ItemHeight = 20;
            this.listBox_missionlist.Location = new System.Drawing.Point(330, 86);
            this.listBox_missionlist.Name = "listBox_missionlist";
            this.listBox_missionlist.Size = new System.Drawing.Size(180, 144);
            this.listBox_missionlist.TabIndex = 8;
            // 
            // listBox_selectedmissionlist
            // 
            this.listBox_selectedmissionlist.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listBox_selectedmissionlist.FormattingEnabled = true;
            this.listBox_selectedmissionlist.ItemHeight = 20;
            this.listBox_selectedmissionlist.Location = new System.Drawing.Point(75, 86);
            this.listBox_selectedmissionlist.Name = "listBox_selectedmissionlist";
            this.listBox_selectedmissionlist.Size = new System.Drawing.Size(180, 144);
            this.listBox_selectedmissionlist.TabIndex = 8;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(425, 30);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(85, 21);
            this.textBox1.TabIndex = 4;
            this.textBox1.Visible = false;
            // 
            // txtTaskName
            // 
            this.txtTaskName.Location = new System.Drawing.Point(268, 29);
            this.txtTaskName.Name = "txtTaskName";
            this.txtTaskName.Size = new System.Drawing.Size(85, 21);
            this.txtTaskName.TabIndex = 4;
            // 
            // txtTaskID
            // 
            this.txtTaskID.Enabled = false;
            this.txtTaskID.Location = new System.Drawing.Point(66, 29);
            this.txtTaskID.Name = "txtTaskID";
            this.txtTaskID.Size = new System.Drawing.Size(113, 21);
            this.txtTaskID.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(538, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "로봇 :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.Location = new System.Drawing.Point(368, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 17);
            this.label8.TabIndex = 2;
            this.label8.Text = "Name :";
            this.label8.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(18, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "미션 :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(211, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Name :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(31, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "ID :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(31, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 21);
            this.label1.TabIndex = 13;
            this.label1.Text = "등록된 Task 리스트";
            // 
            // dataGridView_reg
            // 
            this.dataGridView_reg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_reg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.taskstatus,
            this.taskid,
            this.taskname,
            this.missionlist,
            this.robotlist,
            this.taskLoopflag,
            this.robotgroup});
            this.dataGridView_reg.Location = new System.Drawing.Point(35, 45);
            this.dataGridView_reg.Name = "dataGridView_reg";
            this.dataGridView_reg.RowTemplate.Height = 23;
            this.dataGridView_reg.Size = new System.Drawing.Size(1034, 294);
            this.dataGridView_reg.TabIndex = 12;
            // 
            // taskstatus
            // 
            this.taskstatus.HeaderText = "작업 상태";
            this.taskstatus.Name = "taskstatus";
            // 
            // taskid
            // 
            this.taskid.HeaderText = "ID";
            this.taskid.Name = "taskid";
            // 
            // taskname
            // 
            this.taskname.HeaderText = "작업명";
            this.taskname.Name = "taskname";
            // 
            // missionlist
            // 
            this.missionlist.HeaderText = "미션리스트";
            this.missionlist.Name = "missionlist";
            // 
            // robotlist
            // 
            this.robotlist.HeaderText = "로봇리스트";
            this.robotlist.Name = "robotlist";
            // 
            // taskLoopflag
            // 
            this.taskLoopflag.HeaderText = "반복작업";
            this.taskLoopflag.Name = "taskLoopflag";
            // 
            // robotgroup
            // 
            this.robotgroup.HeaderText = "로봇 그룹";
            this.robotgroup.Name = "robotgroup";
            // 
            // TaskEdit_Ctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox_btn);
            this.Controls.Add(this.groupBox_jobitem);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView_reg);
            this.Name = "TaskEdit_Ctrl";
            this.Size = new System.Drawing.Size(1100, 760);
            this.Load += new System.EventHandler(this.TaskEdit_Ctrl_Load);
            this.groupBox_btn.ResumeLayout(false);
            this.groupBox_jobitem.ResumeLayout(false);
            this.groupBox_jobitem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_reg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_btn;
        private System.Windows.Forms.Button btnTaskUpdate;
        private System.Windows.Forms.Button btnTaskDelete;
        private System.Windows.Forms.Button btnTaskReg;
        private System.Windows.Forms.Button btnRemoverobot;
        private System.Windows.Forms.Button btnSelectrobot;
        private System.Windows.Forms.Button btnRemovemission;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cboJobCallKind;
        private System.Windows.Forms.GroupBox groupBox_jobitem;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboRobotGroup;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSelectmission;
        private System.Windows.Forms.ListBox listBox_robotlist;
        private System.Windows.Forms.ListBox listBox_selectedrobotlist;
        private System.Windows.Forms.ListBox listBox_missionlist;
        private System.Windows.Forms.ListBox listBox_selectedmissionlist;
        private System.Windows.Forms.TextBox txtTaskName;
        private System.Windows.Forms.TextBox txtTaskID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView_reg;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskstatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskid;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskname;
        private System.Windows.Forms.DataGridViewTextBoxColumn missionlist;
        private System.Windows.Forms.DataGridViewTextBoxColumn robotlist;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskLoopflag;
        private System.Windows.Forms.DataGridViewTextBoxColumn robotgroup;
    }
}
