namespace SysSolution.FleetManager.setting
{
    partial class JobReg_Ctrl
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
            this.label1 = new System.Windows.Forms.Label();
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
            this.btnJobUpdate = new System.Windows.Forms.Button();
            this.btnJobDelete = new System.Windows.Forms.Button();
            this.btnJobReg = new System.Windows.Forms.Button();
            this.groupBox_jobitem = new System.Windows.Forms.GroupBox();
            this.btnRemoverobot = new System.Windows.Forms.Button();
            this.btnSelectrobot = new System.Windows.Forms.Button();
            this.btnRemoveWaitmission = new System.Windows.Forms.Button();
            this.btnRemoveUnloadmission = new System.Windows.Forms.Button();
            this.btnRemovemission = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cboJobCallKind = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboRobotGroup = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSelectWaitmission = new System.Windows.Forms.Button();
            this.btnSelectUnloadmission = new System.Windows.Forms.Button();
            this.btnSelectmission = new System.Windows.Forms.Button();
            this.listBox_robotlist = new System.Windows.Forms.ListBox();
            this.listBox_selectedrobotlist = new System.Windows.Forms.ListBox();
            this.listBox_Waitmissionlist = new System.Windows.Forms.ListBox();
            this.listBox_selectedWaitmissionlist = new System.Windows.Forms.ListBox();
            this.listBox_Unloadmissionlist = new System.Windows.Forms.ListBox();
            this.listBox_selectedUnloadmissionlist = new System.Windows.Forms.ListBox();
            this.listBox_missionlist = new System.Windows.Forms.ListBox();
            this.listBox_selectedmissionlist = new System.Windows.Forms.ListBox();
            this.txtJobName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtJobID = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox_btn = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_reg)).BeginInit();
            this.groupBox_jobitem.SuspendLayout();
            this.groupBox_btn.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 21);
            this.label1.TabIndex = 6;
            this.label1.Text = "등록된 작업 리스트";
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
            this.dataGridView_reg.Location = new System.Drawing.Point(16, 51);
            this.dataGridView_reg.Name = "dataGridView_reg";
            this.dataGridView_reg.RowTemplate.Height = 23;
            this.dataGridView_reg.Size = new System.Drawing.Size(1034, 201);
            this.dataGridView_reg.TabIndex = 5;
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
            this.unloadmissionlist.HeaderText = "unload 미션리스트";
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
            // btnJobUpdate
            // 
            this.btnJobUpdate.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnJobUpdate.Location = new System.Drawing.Point(109, 13);
            this.btnJobUpdate.Name = "btnJobUpdate";
            this.btnJobUpdate.Size = new System.Drawing.Size(88, 33);
            this.btnJobUpdate.TabIndex = 7;
            this.btnJobUpdate.Text = "변경";
            this.btnJobUpdate.UseVisualStyleBackColor = true;
            this.btnJobUpdate.Click += new System.EventHandler(this.btnJobUpdate_Click);
            // 
            // btnJobDelete
            // 
            this.btnJobDelete.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnJobDelete.Location = new System.Drawing.Point(210, 13);
            this.btnJobDelete.Name = "btnJobDelete";
            this.btnJobDelete.Size = new System.Drawing.Size(88, 33);
            this.btnJobDelete.TabIndex = 8;
            this.btnJobDelete.Text = "삭제";
            this.btnJobDelete.UseVisualStyleBackColor = true;
            this.btnJobDelete.Click += new System.EventHandler(this.btnJobDelete_Click);
            // 
            // btnJobReg
            // 
            this.btnJobReg.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnJobReg.Location = new System.Drawing.Point(8, 13);
            this.btnJobReg.Name = "btnJobReg";
            this.btnJobReg.Size = new System.Drawing.Size(88, 33);
            this.btnJobReg.TabIndex = 9;
            this.btnJobReg.Text = "추가";
            this.btnJobReg.UseVisualStyleBackColor = true;
            this.btnJobReg.Click += new System.EventHandler(this.btnJobReg_Click);
            // 
            // groupBox_jobitem
            // 
            this.groupBox_jobitem.Controls.Add(this.btnRemoverobot);
            this.groupBox_jobitem.Controls.Add(this.btnSelectrobot);
            this.groupBox_jobitem.Controls.Add(this.btnRemoveWaitmission);
            this.groupBox_jobitem.Controls.Add(this.btnRemoveUnloadmission);
            this.groupBox_jobitem.Controls.Add(this.btnRemovemission);
            this.groupBox_jobitem.Controls.Add(this.btnCancel);
            this.groupBox_jobitem.Controls.Add(this.cboJobCallKind);
            this.groupBox_jobitem.Controls.Add(this.label7);
            this.groupBox_jobitem.Controls.Add(this.cboRobotGroup);
            this.groupBox_jobitem.Controls.Add(this.label6);
            this.groupBox_jobitem.Controls.Add(this.btnSave);
            this.groupBox_jobitem.Controls.Add(this.btnSelectWaitmission);
            this.groupBox_jobitem.Controls.Add(this.btnSelectUnloadmission);
            this.groupBox_jobitem.Controls.Add(this.btnSelectmission);
            this.groupBox_jobitem.Controls.Add(this.listBox_robotlist);
            this.groupBox_jobitem.Controls.Add(this.listBox_selectedrobotlist);
            this.groupBox_jobitem.Controls.Add(this.listBox_Waitmissionlist);
            this.groupBox_jobitem.Controls.Add(this.listBox_selectedWaitmissionlist);
            this.groupBox_jobitem.Controls.Add(this.listBox_Unloadmissionlist);
            this.groupBox_jobitem.Controls.Add(this.listBox_selectedUnloadmissionlist);
            this.groupBox_jobitem.Controls.Add(this.listBox_missionlist);
            this.groupBox_jobitem.Controls.Add(this.listBox_selectedmissionlist);
            this.groupBox_jobitem.Controls.Add(this.txtJobName);
            this.groupBox_jobitem.Controls.Add(this.label9);
            this.groupBox_jobitem.Controls.Add(this.txtJobID);
            this.groupBox_jobitem.Controls.Add(this.label8);
            this.groupBox_jobitem.Controls.Add(this.label5);
            this.groupBox_jobitem.Controls.Add(this.label4);
            this.groupBox_jobitem.Controls.Add(this.label3);
            this.groupBox_jobitem.Controls.Add(this.label2);
            this.groupBox_jobitem.Location = new System.Drawing.Point(16, 323);
            this.groupBox_jobitem.Name = "groupBox_jobitem";
            this.groupBox_jobitem.Size = new System.Drawing.Size(1023, 422);
            this.groupBox_jobitem.TabIndex = 10;
            this.groupBox_jobitem.TabStop = false;
            this.groupBox_jobitem.Enter += new System.EventHandler(this.groupBox_jobitem_Enter);
            // 
            // btnRemoverobot
            // 
            this.btnRemoverobot.Location = new System.Drawing.Point(781, 107);
            this.btnRemoverobot.Name = "btnRemoverobot";
            this.btnRemoverobot.Size = new System.Drawing.Size(32, 23);
            this.btnRemoverobot.TabIndex = 9;
            this.btnRemoverobot.Text = ">>";
            this.btnRemoverobot.UseVisualStyleBackColor = true;
            this.btnRemoverobot.Click += new System.EventHandler(this.btnRemoverobot_Click);
            // 
            // btnSelectrobot
            // 
            this.btnSelectrobot.Location = new System.Drawing.Point(781, 78);
            this.btnSelectrobot.Name = "btnSelectrobot";
            this.btnSelectrobot.Size = new System.Drawing.Size(32, 23);
            this.btnSelectrobot.TabIndex = 9;
            this.btnSelectrobot.Text = "<<";
            this.btnSelectrobot.UseVisualStyleBackColor = true;
            this.btnSelectrobot.Click += new System.EventHandler(this.btnSelectrobot_Click);
            // 
            // btnRemoveWaitmission
            // 
            this.btnRemoveWaitmission.Location = new System.Drawing.Point(308, 327);
            this.btnRemoveWaitmission.Name = "btnRemoveWaitmission";
            this.btnRemoveWaitmission.Size = new System.Drawing.Size(32, 23);
            this.btnRemoveWaitmission.TabIndex = 9;
            this.btnRemoveWaitmission.Text = ">>";
            this.btnRemoveWaitmission.UseVisualStyleBackColor = true;
            this.btnRemoveWaitmission.Click += new System.EventHandler(this.btnRemoveWaitmission_Click);
            // 
            // btnRemoveUnloadmission
            // 
            this.btnRemoveUnloadmission.Location = new System.Drawing.Point(308, 218);
            this.btnRemoveUnloadmission.Name = "btnRemoveUnloadmission";
            this.btnRemoveUnloadmission.Size = new System.Drawing.Size(32, 23);
            this.btnRemoveUnloadmission.TabIndex = 9;
            this.btnRemoveUnloadmission.Text = ">>";
            this.btnRemoveUnloadmission.UseVisualStyleBackColor = true;
            this.btnRemoveUnloadmission.Click += new System.EventHandler(this.btnRemoveUnloadmission_Click);
            // 
            // btnRemovemission
            // 
            this.btnRemovemission.Location = new System.Drawing.Point(308, 110);
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
            this.btnCancel.Location = new System.Drawing.Point(908, 340);
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
            this.cboJobCallKind.Location = new System.Drawing.Point(471, 30);
            this.cboJobCallKind.Name = "cboJobCallKind";
            this.cboJobCallKind.Size = new System.Drawing.Size(118, 20);
            this.cboJobCallKind.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.Location = new System.Drawing.Point(397, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 17);
            this.label7.TabIndex = 6;
            this.label7.Text = "작업방법 :";
            // 
            // cboRobotGroup
            // 
            this.cboRobotGroup.FormattingEnabled = true;
            this.cboRobotGroup.Items.AddRange(new object[] {
            "pallet",
            "conveyor",
            "arm",
            "delivery"});
            this.cboRobotGroup.Location = new System.Drawing.Point(816, 40);
            this.cboRobotGroup.Name = "cboRobotGroup";
            this.cboRobotGroup.Size = new System.Drawing.Size(118, 20);
            this.cboRobotGroup.TabIndex = 7;
            this.cboRobotGroup.SelectedIndexChanged += new System.EventHandler(this.cboRobotGroup_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(742, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 17);
            this.label6.TabIndex = 6;
            this.label6.Text = "작업그룹 :";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSave.Location = new System.Drawing.Point(781, 341);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 49);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "저장";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSelectWaitmission
            // 
            this.btnSelectWaitmission.Location = new System.Drawing.Point(308, 298);
            this.btnSelectWaitmission.Name = "btnSelectWaitmission";
            this.btnSelectWaitmission.Size = new System.Drawing.Size(32, 23);
            this.btnSelectWaitmission.TabIndex = 9;
            this.btnSelectWaitmission.Text = "<<";
            this.btnSelectWaitmission.UseVisualStyleBackColor = true;
            this.btnSelectWaitmission.Click += new System.EventHandler(this.btnSelectWaitmission_Click);
            // 
            // btnSelectUnloadmission
            // 
            this.btnSelectUnloadmission.Location = new System.Drawing.Point(308, 189);
            this.btnSelectUnloadmission.Name = "btnSelectUnloadmission";
            this.btnSelectUnloadmission.Size = new System.Drawing.Size(32, 23);
            this.btnSelectUnloadmission.TabIndex = 9;
            this.btnSelectUnloadmission.Text = "<<";
            this.btnSelectUnloadmission.UseVisualStyleBackColor = true;
            this.btnSelectUnloadmission.Click += new System.EventHandler(this.btnSelectUnloadmission_Click);
            // 
            // btnSelectmission
            // 
            this.btnSelectmission.Location = new System.Drawing.Point(308, 81);
            this.btnSelectmission.Name = "btnSelectmission";
            this.btnSelectmission.Size = new System.Drawing.Size(32, 23);
            this.btnSelectmission.TabIndex = 9;
            this.btnSelectmission.Text = "<<";
            this.btnSelectmission.UseVisualStyleBackColor = true;
            this.btnSelectmission.Click += new System.EventHandler(this.btnSelectmission_Click);
            // 
            // listBox_robotlist
            // 
            this.listBox_robotlist.FormattingEnabled = true;
            this.listBox_robotlist.ItemHeight = 12;
            this.listBox_robotlist.Location = new System.Drawing.Point(816, 66);
            this.listBox_robotlist.Name = "listBox_robotlist";
            this.listBox_robotlist.Size = new System.Drawing.Size(180, 124);
            this.listBox_robotlist.TabIndex = 8;
            // 
            // listBox_selectedrobotlist
            // 
            this.listBox_selectedrobotlist.FormattingEnabled = true;
            this.listBox_selectedrobotlist.ItemHeight = 12;
            this.listBox_selectedrobotlist.Location = new System.Drawing.Point(595, 66);
            this.listBox_selectedrobotlist.Name = "listBox_selectedrobotlist";
            this.listBox_selectedrobotlist.Size = new System.Drawing.Size(180, 124);
            this.listBox_selectedrobotlist.TabIndex = 8;
            // 
            // listBox_Waitmissionlist
            // 
            this.listBox_Waitmissionlist.FormattingEnabled = true;
            this.listBox_Waitmissionlist.ItemHeight = 12;
            this.listBox_Waitmissionlist.Location = new System.Drawing.Point(343, 283);
            this.listBox_Waitmissionlist.Name = "listBox_Waitmissionlist";
            this.listBox_Waitmissionlist.Size = new System.Drawing.Size(180, 88);
            this.listBox_Waitmissionlist.TabIndex = 8;
            // 
            // listBox_selectedWaitmissionlist
            // 
            this.listBox_selectedWaitmissionlist.FormattingEnabled = true;
            this.listBox_selectedWaitmissionlist.ItemHeight = 12;
            this.listBox_selectedWaitmissionlist.Location = new System.Drawing.Point(122, 283);
            this.listBox_selectedWaitmissionlist.Name = "listBox_selectedWaitmissionlist";
            this.listBox_selectedWaitmissionlist.Size = new System.Drawing.Size(180, 88);
            this.listBox_selectedWaitmissionlist.TabIndex = 8;
            // 
            // listBox_Unloadmissionlist
            // 
            this.listBox_Unloadmissionlist.FormattingEnabled = true;
            this.listBox_Unloadmissionlist.ItemHeight = 12;
            this.listBox_Unloadmissionlist.Location = new System.Drawing.Point(343, 174);
            this.listBox_Unloadmissionlist.Name = "listBox_Unloadmissionlist";
            this.listBox_Unloadmissionlist.Size = new System.Drawing.Size(180, 88);
            this.listBox_Unloadmissionlist.TabIndex = 8;
            // 
            // listBox_selectedUnloadmissionlist
            // 
            this.listBox_selectedUnloadmissionlist.FormattingEnabled = true;
            this.listBox_selectedUnloadmissionlist.ItemHeight = 12;
            this.listBox_selectedUnloadmissionlist.Location = new System.Drawing.Point(122, 174);
            this.listBox_selectedUnloadmissionlist.Name = "listBox_selectedUnloadmissionlist";
            this.listBox_selectedUnloadmissionlist.Size = new System.Drawing.Size(180, 88);
            this.listBox_selectedUnloadmissionlist.TabIndex = 8;
            // 
            // listBox_missionlist
            // 
            this.listBox_missionlist.FormattingEnabled = true;
            this.listBox_missionlist.ItemHeight = 12;
            this.listBox_missionlist.Location = new System.Drawing.Point(343, 66);
            this.listBox_missionlist.Name = "listBox_missionlist";
            this.listBox_missionlist.Size = new System.Drawing.Size(180, 88);
            this.listBox_missionlist.TabIndex = 8;
            // 
            // listBox_selectedmissionlist
            // 
            this.listBox_selectedmissionlist.FormattingEnabled = true;
            this.listBox_selectedmissionlist.ItemHeight = 12;
            this.listBox_selectedmissionlist.Location = new System.Drawing.Point(122, 66);
            this.listBox_selectedmissionlist.Name = "listBox_selectedmissionlist";
            this.listBox_selectedmissionlist.Size = new System.Drawing.Size(180, 88);
            this.listBox_selectedmissionlist.TabIndex = 8;
            // 
            // txtJobName
            // 
            this.txtJobName.Location = new System.Drawing.Point(296, 29);
            this.txtJobName.Name = "txtJobName";
            this.txtJobName.Size = new System.Drawing.Size(85, 21);
            this.txtJobName.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.Location = new System.Drawing.Point(18, 283);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 17);
            this.label9.TabIndex = 2;
            this.label9.Text = "wait 미션 :";
            // 
            // txtJobID
            // 
            this.txtJobID.Enabled = false;
            this.txtJobID.Location = new System.Drawing.Point(75, 29);
            this.txtJobID.Name = "txtJobID";
            this.txtJobID.Size = new System.Drawing.Size(113, 21);
            this.txtJobID.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.Location = new System.Drawing.Point(18, 174);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 17);
            this.label8.TabIndex = 2;
            this.label8.Text = "end 미션 :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(538, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "로봇 :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(18, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "begin 미션 :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(230, 29);
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
            // groupBox_btn
            // 
            this.groupBox_btn.Controls.Add(this.btnJobUpdate);
            this.groupBox_btn.Controls.Add(this.btnJobDelete);
            this.groupBox_btn.Controls.Add(this.btnJobReg);
            this.groupBox_btn.Location = new System.Drawing.Point(16, 258);
            this.groupBox_btn.Name = "groupBox_btn";
            this.groupBox_btn.Size = new System.Drawing.Size(315, 56);
            this.groupBox_btn.TabIndex = 11;
            this.groupBox_btn.TabStop = false;
            // 
            // JobReg_Ctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox_btn);
            this.Controls.Add(this.groupBox_jobitem);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView_reg);
            this.Name = "JobReg_Ctrl";
            this.Size = new System.Drawing.Size(1100, 760);
            this.Load += new System.EventHandler(this.JobReg_Ctrl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_reg)).EndInit();
            this.groupBox_jobitem.ResumeLayout(false);
            this.groupBox_jobitem.PerformLayout();
            this.groupBox_btn.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView_reg;
        private System.Windows.Forms.Button btnJobUpdate;
        private System.Windows.Forms.Button btnJobDelete;
        private System.Windows.Forms.Button btnJobReg;
        private System.Windows.Forms.GroupBox groupBox_jobitem;
        private System.Windows.Forms.TextBox txtJobName;
        private System.Windows.Forms.TextBox txtJobID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboRobotGroup;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox listBox_robotlist;
        private System.Windows.Forms.ListBox listBox_selectedrobotlist;
        private System.Windows.Forms.ListBox listBox_missionlist;
        private System.Windows.Forms.ListBox listBox_selectedmissionlist;
        private System.Windows.Forms.Button btnRemoverobot;
        private System.Windows.Forms.Button btnSelectrobot;
        private System.Windows.Forms.Button btnRemovemission;
        private System.Windows.Forms.Button btnSelectmission;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox_btn;
        private System.Windows.Forms.ComboBox cboJobCallKind;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnRemoveWaitmission;
        private System.Windows.Forms.Button btnRemoveUnloadmission;
        private System.Windows.Forms.Button btnSelectWaitmission;
        private System.Windows.Forms.Button btnSelectUnloadmission;
        private System.Windows.Forms.ListBox listBox_Waitmissionlist;
        private System.Windows.Forms.ListBox listBox_selectedWaitmissionlist;
        private System.Windows.Forms.ListBox listBox_Unloadmissionlist;
        private System.Windows.Forms.ListBox listBox_selectedUnloadmissionlist;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
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
