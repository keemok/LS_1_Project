namespace Syscon_Solution.FleetManager.UI.Task1
{
    partial class TaskOperation_Ctrl
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
            this.btnTaskStop1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnTaskRun1 = new DevExpress.XtraEditors.SimpleButton();
            this.txtTaskname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTaskCnt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView_reg = new System.Windows.Forms.DataGridView();
            this.taskstatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.missionlist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.robotlist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskLoopflag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.robotgroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.btnWaitPos = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chk100M = new System.Windows.Forms.CheckBox();
            this.chk300D = new System.Windows.Forms.CheckBox();
            this.txtLiftCnt = new System.Windows.Forms.TextBox();
            this.chk1000_2 = new System.Windows.Forms.CheckBox();
            this.txtTrackdriveCnt = new System.Windows.Forms.TextBox();
            this.chk1000_1 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chk200P = new System.Windows.Forms.CheckBox();
            this.chk500_2 = new System.Windows.Forms.CheckBox();
            this.chk500_1 = new System.Windows.Forms.CheckBox();
            this.btnWaitPos2 = new DevExpress.XtraEditors.SimpleButton();
            this.label13 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtWaitpos7 = new System.Windows.Forms.TextBox();
            this.txtWaitpos6 = new System.Windows.Forms.TextBox();
            this.txtWaitpos5 = new System.Windows.Forms.TextBox();
            this.txtWaitpos4 = new System.Windows.Forms.TextBox();
            this.txtWaitpos3 = new System.Windows.Forms.TextBox();
            this.txtWaitpos2 = new System.Windows.Forms.TextBox();
            this.txtWaitpos1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDemoStop = new DevExpress.XtraEditors.SimpleButton();
            this.btnDemoRun = new DevExpress.XtraEditors.SimpleButton();
            this.btnTaskResume = new DevExpress.XtraEditors.SimpleButton();
            this.btnTaskPause = new DevExpress.XtraEditors.SimpleButton();
            this.chkSpeedDrive = new System.Windows.Forms.CheckBox();
            this.chkLineDrive = new System.Windows.Forms.CheckBox();
            this.chkURRun = new System.Windows.Forms.CheckBox();
            this.chkCurveRun = new System.Windows.Forms.CheckBox();
            this.chkLiftRun = new System.Windows.Forms.CheckBox();
            this.timer_StartTrack = new System.Windows.Forms.Timer(this.components);
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.timer_runnigTime = new System.Windows.Forms.Timer(this.components);
            this.cboliftrobotID = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnLiftSet = new System.Windows.Forms.Button();
            this.cboRobot_lift = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.btnliftdown = new System.Windows.Forms.Button();
            this.btnliftup = new System.Windows.Forms.Button();
            this.timer_CrashCheck = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkRobotPosRec = new System.Windows.Forms.CheckBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.timer_LinedrivewaitokChk = new System.Windows.Forms.Timer(this.components);
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.chkDemo = new System.Windows.Forms.CheckBox();
            this.btnTempomove = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.listBox4 = new System.Windows.Forms.ListBox();
            this.chkLineRobotSelect = new System.Windows.Forms.CheckBox();
            this.txtLineSelectRobot = new System.Windows.Forms.TextBox();
            this.txt_S_LineSelectRobot = new System.Windows.Forms.TextBox();
            this.chkSline = new System.Windows.Forms.CheckBox();
            this.txt_DriveSelectRobot = new System.Windows.Forms.TextBox();
            this.chkDrive = new System.Windows.Forms.CheckBox();
            this.txtURselectrobot = new System.Windows.Forms.TextBox();
            this.chkURdrive = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.simpleButton6 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton7 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton8 = new DevExpress.XtraEditors.SimpleButton();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.simpleButton9 = new DevExpress.XtraEditors.SimpleButton();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.listBox5 = new System.Windows.Forms.ListBox();
            this.listBox6 = new System.Windows.Forms.ListBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.textBox19 = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.textBox20 = new System.Windows.Forms.TextBox();
            this.textBox21 = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.btnListclear = new System.Windows.Forms.Button();
            this.txtDockSelectRobot = new System.Windows.Forms.TextBox();
            this.chkDockdrive = new System.Windows.Forms.CheckBox();
            this.groupBox_btn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_reg)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_btn
            // 
            this.groupBox_btn.Controls.Add(this.btnTaskStop1);
            this.groupBox_btn.Controls.Add(this.btnTaskRun1);
            this.groupBox_btn.Controls.Add(this.txtTaskname);
            this.groupBox_btn.Controls.Add(this.label1);
            this.groupBox_btn.Controls.Add(this.txtTaskCnt);
            this.groupBox_btn.Controls.Add(this.label2);
            this.groupBox_btn.Location = new System.Drawing.Point(33, 326);
            this.groupBox_btn.Name = "groupBox_btn";
            this.groupBox_btn.Size = new System.Drawing.Size(944, 64);
            this.groupBox_btn.TabIndex = 15;
            this.groupBox_btn.TabStop = false;
            // 
            // btnTaskStop1
            // 
            this.btnTaskStop1.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaskStop1.Appearance.Options.UseFont = true;
            this.btnTaskStop1.Location = new System.Drawing.Point(841, 14);
            this.btnTaskStop1.Name = "btnTaskStop1";
            this.btnTaskStop1.Size = new System.Drawing.Size(97, 35);
            this.btnTaskStop1.TabIndex = 18;
            this.btnTaskStop1.Text = "작업정지";
            this.btnTaskStop1.Click += new System.EventHandler(this.btnTaskStop_Click);
            // 
            // btnTaskRun1
            // 
            this.btnTaskRun1.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaskRun1.Appearance.Options.UseFont = true;
            this.btnTaskRun1.Location = new System.Drawing.Point(738, 14);
            this.btnTaskRun1.Name = "btnTaskRun1";
            this.btnTaskRun1.Size = new System.Drawing.Size(97, 35);
            this.btnTaskRun1.TabIndex = 18;
            this.btnTaskRun1.Text = "작업시작";
            this.btnTaskRun1.Click += new System.EventHandler(this.btnTaskRun_Click);
            // 
            // txtTaskname
            // 
            this.txtTaskname.Enabled = false;
            this.txtTaskname.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtTaskname.Location = new System.Drawing.Point(109, 20);
            this.txtTaskname.Name = "txtTaskname";
            this.txtTaskname.Size = new System.Drawing.Size(259, 33);
            this.txtTaskname.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.SeaShell;
            this.label1.Location = new System.Drawing.Point(32, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 13;
            this.label1.Text = "작업명";
            // 
            // txtTaskCnt
            // 
            this.txtTaskCnt.Location = new System.Drawing.Point(460, 24);
            this.txtTaskCnt.Name = "txtTaskCnt";
            this.txtTaskCnt.Size = new System.Drawing.Size(113, 21);
            this.txtTaskCnt.TabIndex = 14;
            this.txtTaskCnt.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.SeaShell;
            this.label2.Location = new System.Drawing.Point(389, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "작업 횟수";
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
            this.dataGridView_reg.Location = new System.Drawing.Point(38, 67);
            this.dataGridView_reg.Name = "dataGridView_reg";
            this.dataGridView_reg.RowTemplate.Height = 23;
            this.dataGridView_reg.Size = new System.Drawing.Size(944, 253);
            this.dataGridView_reg.TabIndex = 16;
            this.dataGridView_reg.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_reg_CellClick);
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(34, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 21);
            this.label3.TabIndex = 17;
            this.label3.Text = "등록된 Task 리스트";
            // 
            // btnWaitPos
            // 
            this.btnWaitPos.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWaitPos.Appearance.Options.UseFont = true;
            this.btnWaitPos.Location = new System.Drawing.Point(31, 184);
            this.btnWaitPos.Name = "btnWaitPos";
            this.btnWaitPos.Size = new System.Drawing.Size(97, 35);
            this.btnWaitPos.TabIndex = 18;
            this.btnWaitPos.Text = "대기장소 이동";
            this.btnWaitPos.Click += new System.EventHandler(this.btnWaitPos_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.chk100M);
            this.groupBox1.Controls.Add(this.chk300D);
            this.groupBox1.Controls.Add(this.txtLiftCnt);
            this.groupBox1.Controls.Add(this.chk1000_2);
            this.groupBox1.Controls.Add(this.txtTrackdriveCnt);
            this.groupBox1.Controls.Add(this.chk1000_1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.chk200P);
            this.groupBox1.Controls.Add(this.chk500_2);
            this.groupBox1.Controls.Add(this.chk500_1);
            this.groupBox1.Controls.Add(this.btnWaitPos2);
            this.groupBox1.Controls.Add(this.btnWaitPos);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtWaitpos7);
            this.groupBox1.Controls.Add(this.txtWaitpos6);
            this.groupBox1.Controls.Add(this.txtWaitpos5);
            this.groupBox1.Controls.Add(this.txtWaitpos4);
            this.groupBox1.Controls.Add(this.txtWaitpos3);
            this.groupBox1.Controls.Add(this.txtWaitpos2);
            this.groupBox1.Controls.Add(this.txtWaitpos1);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(33, 470);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(479, 237);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "대기장소 옵션";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.Color.SeaShell;
            this.label7.Location = new System.Drawing.Point(417, 217);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 17);
            this.label7.TabIndex = 13;
            this.label7.Text = "리프트 횟수";
            this.label7.Visible = false;
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // chk100M
            // 
            this.chk100M.AutoSize = true;
            this.chk100M.Location = new System.Drawing.Point(59, 41);
            this.chk100M.Name = "chk100M";
            this.chk100M.Size = new System.Drawing.Size(78, 16);
            this.chk100M.TabIndex = 19;
            this.chk100M.Text = "100A(4번)";
            this.chk100M.UseVisualStyleBackColor = true;
            // 
            // chk300D
            // 
            this.chk300D.AutoSize = true;
            this.chk300D.Location = new System.Drawing.Point(351, 20);
            this.chk300D.Name = "chk300D";
            this.chk300D.Size = new System.Drawing.Size(78, 16);
            this.chk300D.TabIndex = 19;
            this.chk300D.Text = "300D(3번)";
            this.chk300D.UseVisualStyleBackColor = true;
            // 
            // txtLiftCnt
            // 
            this.txtLiftCnt.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtLiftCnt.Location = new System.Drawing.Point(420, 239);
            this.txtLiftCnt.Name = "txtLiftCnt";
            this.txtLiftCnt.Size = new System.Drawing.Size(75, 33);
            this.txtLiftCnt.TabIndex = 14;
            this.txtLiftCnt.Visible = false;
            this.txtLiftCnt.TextChanged += new System.EventHandler(this.txtLiftCnt_TextChanged);
            // 
            // chk1000_2
            // 
            this.chk1000_2.AutoSize = true;
            this.chk1000_2.Location = new System.Drawing.Point(263, 39);
            this.chk1000_2.Name = "chk1000_2";
            this.chk1000_2.Size = new System.Drawing.Size(96, 16);
            this.chk1000_2.TabIndex = 19;
            this.chk1000_2.Text = "1000B-2(6번)";
            this.chk1000_2.UseVisualStyleBackColor = true;
            // 
            // txtTrackdriveCnt
            // 
            this.txtTrackdriveCnt.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtTrackdriveCnt.Location = new System.Drawing.Point(317, 239);
            this.txtTrackdriveCnt.Name = "txtTrackdriveCnt";
            this.txtTrackdriveCnt.Size = new System.Drawing.Size(88, 33);
            this.txtTrackdriveCnt.TabIndex = 14;
            this.txtTrackdriveCnt.Visible = false;
            // 
            // chk1000_1
            // 
            this.chk1000_1.AutoSize = true;
            this.chk1000_1.Location = new System.Drawing.Point(153, 41);
            this.chk1000_1.Name = "chk1000_1";
            this.chk1000_1.Size = new System.Drawing.Size(84, 16);
            this.chk1000_1.TabIndex = 19;
            this.chk1000_1.Text = "1000B(5번)";
            this.chk1000_1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.SeaShell;
            this.label6.Location = new System.Drawing.Point(314, 219);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 17);
            this.label6.TabIndex = 13;
            this.label6.Text = "트랙 주행횟수";
            this.label6.Visible = false;
            // 
            // chk200P
            // 
            this.chk200P.AutoSize = true;
            this.chk200P.Location = new System.Drawing.Point(59, 17);
            this.chk200P.Name = "chk200P";
            this.chk200P.Size = new System.Drawing.Size(78, 16);
            this.chk200P.TabIndex = 19;
            this.chk200P.Text = "200P(0번)";
            this.chk200P.UseVisualStyleBackColor = true;
            // 
            // chk500_2
            // 
            this.chk500_2.AutoSize = true;
            this.chk500_2.Location = new System.Drawing.Point(147, 17);
            this.chk500_2.Name = "chk500_2";
            this.chk500_2.Size = new System.Drawing.Size(82, 16);
            this.chk500_2.TabIndex = 19;
            this.chk500_2.Text = "500-2(1번)";
            this.chk500_2.UseVisualStyleBackColor = true;
            // 
            // chk500_1
            // 
            this.chk500_1.AutoSize = true;
            this.chk500_1.Location = new System.Drawing.Point(243, 17);
            this.chk500_1.Name = "chk500_1";
            this.chk500_1.Size = new System.Drawing.Size(82, 16);
            this.chk500_1.TabIndex = 19;
            this.chk500_1.Text = "500-1(2번)";
            this.chk500_1.UseVisualStyleBackColor = true;
            // 
            // btnWaitPos2
            // 
            this.btnWaitPos2.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWaitPos2.Appearance.Options.UseFont = true;
            this.btnWaitPos2.Location = new System.Drawing.Point(205, 163);
            this.btnWaitPos2.Name = "btnWaitPos2";
            this.btnWaitPos2.Size = new System.Drawing.Size(163, 56);
            this.btnWaitPos2.TabIndex = 18;
            this.btnWaitPos2.Text = "대기장소 이동2";
            this.btnWaitPos2.Click += new System.EventHandler(this.btnWaitPos2_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label13.ForeColor = System.Drawing.Color.SeaShell;
            this.label13.Location = new System.Drawing.Point(242, 116);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 17);
            this.label13.TabIndex = 13;
            this.label13.Text = "대기 장소7";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.SeaShell;
            this.label5.Location = new System.Drawing.Point(159, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "대기 장소6";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label11.ForeColor = System.Drawing.Color.SeaShell;
            this.label11.Location = new System.Drawing.Point(325, 62);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 17);
            this.label11.TabIndex = 13;
            this.label11.Text = "대기 장소4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.SeaShell;
            this.label4.Location = new System.Drawing.Point(76, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "대기 장소5";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.ForeColor = System.Drawing.Color.SeaShell;
            this.label10.Location = new System.Drawing.Point(242, 62);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 17);
            this.label10.TabIndex = 13;
            this.label10.Text = "대기 장소3";
            // 
            // txtWaitpos7
            // 
            this.txtWaitpos7.Location = new System.Drawing.Point(245, 136);
            this.txtWaitpos7.Name = "txtWaitpos7";
            this.txtWaitpos7.Size = new System.Drawing.Size(69, 21);
            this.txtWaitpos7.TabIndex = 14;
            // 
            // txtWaitpos6
            // 
            this.txtWaitpos6.Location = new System.Drawing.Point(162, 136);
            this.txtWaitpos6.Name = "txtWaitpos6";
            this.txtWaitpos6.Size = new System.Drawing.Size(69, 21);
            this.txtWaitpos6.TabIndex = 14;
            // 
            // txtWaitpos5
            // 
            this.txtWaitpos5.Location = new System.Drawing.Point(79, 136);
            this.txtWaitpos5.Name = "txtWaitpos5";
            this.txtWaitpos5.Size = new System.Drawing.Size(69, 21);
            this.txtWaitpos5.TabIndex = 14;
            // 
            // txtWaitpos4
            // 
            this.txtWaitpos4.Location = new System.Drawing.Point(328, 82);
            this.txtWaitpos4.Name = "txtWaitpos4";
            this.txtWaitpos4.Size = new System.Drawing.Size(69, 21);
            this.txtWaitpos4.TabIndex = 14;
            // 
            // txtWaitpos3
            // 
            this.txtWaitpos3.Location = new System.Drawing.Point(245, 82);
            this.txtWaitpos3.Name = "txtWaitpos3";
            this.txtWaitpos3.Size = new System.Drawing.Size(69, 21);
            this.txtWaitpos3.TabIndex = 14;
            // 
            // txtWaitpos2
            // 
            this.txtWaitpos2.Location = new System.Drawing.Point(164, 82);
            this.txtWaitpos2.Name = "txtWaitpos2";
            this.txtWaitpos2.Size = new System.Drawing.Size(69, 21);
            this.txtWaitpos2.TabIndex = 14;
            // 
            // txtWaitpos1
            // 
            this.txtWaitpos1.Location = new System.Drawing.Point(79, 82);
            this.txtWaitpos1.Name = "txtWaitpos1";
            this.txtWaitpos1.Size = new System.Drawing.Size(69, 21);
            this.txtWaitpos1.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.ForeColor = System.Drawing.Color.SeaShell;
            this.label9.Location = new System.Drawing.Point(159, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 17);
            this.label9.TabIndex = 13;
            this.label9.Text = "대기 장소2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.ForeColor = System.Drawing.Color.SeaShell;
            this.label8.Location = new System.Drawing.Point(76, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 17);
            this.label8.TabIndex = 13;
            this.label8.Text = "대기 장소1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDemoStop);
            this.groupBox2.Controls.Add(this.btnDemoRun);
            this.groupBox2.Location = new System.Drawing.Point(548, 663);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(397, 63);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "주행";
            // 
            // btnDemoStop
            // 
            this.btnDemoStop.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDemoStop.Appearance.Options.UseFont = true;
            this.btnDemoStop.Location = new System.Drawing.Point(133, 24);
            this.btnDemoStop.Name = "btnDemoStop";
            this.btnDemoStop.Size = new System.Drawing.Size(97, 35);
            this.btnDemoStop.TabIndex = 18;
            this.btnDemoStop.Text = "작업정지";
            this.btnDemoStop.Click += new System.EventHandler(this.btnDemoStop_Click);
            // 
            // btnDemoRun
            // 
            this.btnDemoRun.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDemoRun.Appearance.Options.UseFont = true;
            this.btnDemoRun.Location = new System.Drawing.Point(20, 24);
            this.btnDemoRun.Name = "btnDemoRun";
            this.btnDemoRun.Size = new System.Drawing.Size(97, 35);
            this.btnDemoRun.TabIndex = 18;
            this.btnDemoRun.Text = "작업시작";
            this.btnDemoRun.Click += new System.EventHandler(this.btnDemoRun_Click);
            // 
            // btnTaskResume
            // 
            this.btnTaskResume.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaskResume.Appearance.Options.UseFont = true;
            this.btnTaskResume.Location = new System.Drawing.Point(510, 439);
            this.btnTaskResume.Name = "btnTaskResume";
            this.btnTaskResume.Size = new System.Drawing.Size(97, 35);
            this.btnTaskResume.TabIndex = 18;
            this.btnTaskResume.Text = "작업 재시작";
            this.btnTaskResume.Visible = false;
            this.btnTaskResume.Click += new System.EventHandler(this.btnTaskResume_Click);
            // 
            // btnTaskPause
            // 
            this.btnTaskPause.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaskPause.Appearance.Options.UseFont = true;
            this.btnTaskPause.Location = new System.Drawing.Point(510, 471);
            this.btnTaskPause.Name = "btnTaskPause";
            this.btnTaskPause.Size = new System.Drawing.Size(97, 35);
            this.btnTaskPause.TabIndex = 18;
            this.btnTaskPause.Text = "작업 일시정지";
            this.btnTaskPause.Visible = false;
            this.btnTaskPause.Click += new System.EventHandler(this.btnTaskPause_Click);
            // 
            // chkSpeedDrive
            // 
            this.chkSpeedDrive.AutoSize = true;
            this.chkSpeedDrive.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkSpeedDrive.Location = new System.Drawing.Point(989, 640);
            this.chkSpeedDrive.Name = "chkSpeedDrive";
            this.chkSpeedDrive.Size = new System.Drawing.Size(93, 25);
            this.chkSpeedDrive.TabIndex = 19;
            this.chkSpeedDrive.Text = "고속주행";
            this.chkSpeedDrive.UseVisualStyleBackColor = true;
            this.chkSpeedDrive.Visible = false;
            // 
            // chkLineDrive
            // 
            this.chkLineDrive.AutoSize = true;
            this.chkLineDrive.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkLineDrive.Location = new System.Drawing.Point(1207, 819);
            this.chkLineDrive.Name = "chkLineDrive";
            this.chkLineDrive.Size = new System.Drawing.Size(167, 25);
            this.chkLineDrive.TabIndex = 19;
            this.chkLineDrive.Text = "직선주행(1000B-2)";
            this.chkLineDrive.UseVisualStyleBackColor = true;
            this.chkLineDrive.Visible = false;
            this.chkLineDrive.CheckedChanged += new System.EventHandler(this.chkLineDrive_CheckedChanged);
            // 
            // chkURRun
            // 
            this.chkURRun.AutoSize = true;
            this.chkURRun.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkURRun.Location = new System.Drawing.Point(1216, 636);
            this.chkURRun.Name = "chkURRun";
            this.chkURRun.Size = new System.Drawing.Size(143, 25);
            this.chkURRun.TabIndex = 19;
            this.chkURRun.Text = "UR 주행(100M)";
            this.chkURRun.UseVisualStyleBackColor = true;
            this.chkURRun.Visible = false;
            // 
            // chkCurveRun
            // 
            this.chkCurveRun.AutoSize = true;
            this.chkCurveRun.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkCurveRun.Location = new System.Drawing.Point(1079, 636);
            this.chkCurveRun.Name = "chkCurveRun";
            this.chkCurveRun.Size = new System.Drawing.Size(143, 25);
            this.chkCurveRun.TabIndex = 19;
            this.chkCurveRun.Text = "S자 주행(300D)";
            this.chkCurveRun.UseVisualStyleBackColor = true;
            this.chkCurveRun.Visible = false;
            // 
            // chkLiftRun
            // 
            this.chkLiftRun.AutoSize = true;
            this.chkLiftRun.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkLiftRun.Location = new System.Drawing.Point(1086, 819);
            this.chkLiftRun.Name = "chkLiftRun";
            this.chkLiftRun.Size = new System.Drawing.Size(115, 25);
            this.chkLiftRun.TabIndex = 19;
            this.chkLiftRun.Text = "리프트 주행";
            this.chkLiftRun.UseVisualStyleBackColor = true;
            this.chkLiftRun.Visible = false;
            this.chkLiftRun.CheckedChanged += new System.EventHandler(this.chkLiftRun_CheckedChanged);
            // 
            // timer_StartTrack
            // 
            this.timer_StartTrack.Tick += new System.EventHandler(this.timer_StartTrack_Tick);
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 11;
            this.listBox1.Location = new System.Drawing.Point(8, 732);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(520, 81);
            this.listBox1.TabIndex = 21;
            // 
            // timer_runnigTime
            // 
            this.timer_runnigTime.Tick += new System.EventHandler(this.timer_runnigTime_Tick);
            // 
            // cboliftrobotID
            // 
            this.cboliftrobotID.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.cboliftrobotID.FormattingEnabled = true;
            this.cboliftrobotID.Items.AddRange(new object[] {
            "R_001",
            "R_002",
            "R_005",
            "R_006"});
            this.cboliftrobotID.Location = new System.Drawing.Point(1310, 17);
            this.cboliftrobotID.Name = "cboliftrobotID";
            this.cboliftrobotID.Size = new System.Drawing.Size(153, 25);
            this.cboliftrobotID.TabIndex = 50;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label12.Location = new System.Drawing.Point(1193, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(120, 25);
            this.label12.TabIndex = 49;
            this.label12.Text = "로봇 ID";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLiftSet
            // 
            this.btnLiftSet.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnLiftSet.Location = new System.Drawing.Point(1607, 29);
            this.btnLiftSet.Margin = new System.Windows.Forms.Padding(2);
            this.btnLiftSet.Name = "btnLiftSet";
            this.btnLiftSet.Size = new System.Drawing.Size(76, 31);
            this.btnLiftSet.TabIndex = 53;
            this.btnLiftSet.Text = "설정";
            this.btnLiftSet.UseVisualStyleBackColor = true;
            this.btnLiftSet.Visible = false;
            this.btnLiftSet.Click += new System.EventHandler(this.btnLiftSet_Click);
            // 
            // cboRobot_lift
            // 
            this.cboRobot_lift.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.cboRobot_lift.FormattingEnabled = true;
            this.cboRobot_lift.Items.AddRange(new object[] {
            "리프트 up",
            "리프트 down"});
            this.cboRobot_lift.Location = new System.Drawing.Point(1310, 52);
            this.cboRobot_lift.Name = "cboRobot_lift";
            this.cboRobot_lift.Size = new System.Drawing.Size(153, 25);
            this.cboRobot_lift.TabIndex = 52;
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label24.Location = new System.Drawing.Point(1179, 52);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(120, 25);
            this.label24.TabIndex = 51;
            this.label24.Text = "리프트 초기설정";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnliftdown
            // 
            this.btnliftdown.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnliftdown.Location = new System.Drawing.Point(1490, 52);
            this.btnliftdown.Margin = new System.Windows.Forms.Padding(2);
            this.btnliftdown.Name = "btnliftdown";
            this.btnliftdown.Size = new System.Drawing.Size(102, 30);
            this.btnliftdown.TabIndex = 54;
            this.btnliftdown.Text = "리프트 down";
            this.btnliftdown.UseVisualStyleBackColor = true;
            this.btnliftdown.Click += new System.EventHandler(this.btnliftdown_Click);
            // 
            // btnliftup
            // 
            this.btnliftup.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnliftup.Location = new System.Drawing.Point(1490, 13);
            this.btnliftup.Margin = new System.Windows.Forms.Padding(2);
            this.btnliftup.Name = "btnliftup";
            this.btnliftup.Size = new System.Drawing.Size(102, 30);
            this.btnliftup.TabIndex = 55;
            this.btnliftup.Text = "리프트 up";
            this.btnliftup.UseVisualStyleBackColor = true;
            this.btnliftup.Click += new System.EventHandler(this.btnliftup_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(1194, 477);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 63);
            this.button1.TabIndex = 56;
            this.button1.Text = "500_1_R(2번)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button2.Location = new System.Drawing.Point(1106, 477);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 63);
            this.button2.TabIndex = 56;
            this.button2.Text = "500_2_R(1번)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button3.Location = new System.Drawing.Point(1459, 477);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(82, 63);
            this.button3.TabIndex = 56;
            this.button3.Text = "1000B_R(5번)";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button4.Location = new System.Drawing.Point(1282, 477);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(82, 63);
            this.button4.TabIndex = 56;
            this.button4.Text = "300D_R (3번)";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1192, 676);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(86, 38);
            this.button5.TabIndex = 56;
            this.button5.Text = "LookAHead";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button6.Location = new System.Drawing.Point(1194, 555);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(82, 63);
            this.button6.TabIndex = 56;
            this.button6.Text = "500_1_P(2번)";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button7.Location = new System.Drawing.Point(1106, 556);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(82, 63);
            this.button7.TabIndex = 56;
            this.button7.Text = "500_2_P(1번)";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button8.Location = new System.Drawing.Point(1459, 555);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(82, 63);
            this.button8.TabIndex = 56;
            this.button8.Text = "1000B_P(5번)";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button9.Location = new System.Drawing.Point(1282, 556);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(82, 63);
            this.button9.TabIndex = 56;
            this.button9.Text = "300D_P (3번)";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkRobotPosRec);
            this.groupBox3.Location = new System.Drawing.Point(830, 9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(152, 52);
            this.groupBox3.TabIndex = 57;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "성능평가";
            this.groupBox3.Visible = false;
            // 
            // chkRobotPosRec
            // 
            this.chkRobotPosRec.AutoSize = true;
            this.chkRobotPosRec.Location = new System.Drawing.Point(22, 29);
            this.chkRobotPosRec.Name = "chkRobotPosRec";
            this.chkRobotPosRec.Size = new System.Drawing.Size(100, 16);
            this.chkRobotPosRec.TabIndex = 19;
            this.chkRobotPosRec.Text = "로봇패스 기록";
            this.chkRobotPosRec.UseVisualStyleBackColor = true;
            this.chkRobotPosRec.CheckedChanged += new System.EventHandler(this.chkRobotPosRec_CheckedChanged);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(1404, 638);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(254, 88);
            this.listBox2.TabIndex = 21;
            // 
            // button10
            // 
            this.button10.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button10.Location = new System.Drawing.Point(1547, 477);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(82, 63);
            this.button10.TabIndex = 56;
            this.button10.Text = "1000B_2_R(6번)";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button11.Location = new System.Drawing.Point(1547, 555);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(82, 63);
            this.button11.TabIndex = 56;
            this.button11.Text = "1000B_2_P(6번)";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // timer_LinedrivewaitokChk
            // 
            this.timer_LinedrivewaitokChk.Tick += new System.EventHandler(this.timer_LinedrivewaitokChk_Tick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(317, 443);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 16);
            this.checkBox1.TabIndex = 58;
            this.checkBox1.Text = "시퀀스데모";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // listBox3
            // 
            this.listBox3.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listBox3.FormattingEnabled = true;
            this.listBox3.ItemHeight = 11;
            this.listBox3.Location = new System.Drawing.Point(542, 732);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(531, 81);
            this.listBox3.TabIndex = 21;
            // 
            // chkDemo
            // 
            this.chkDemo.AutoSize = true;
            this.chkDemo.Location = new System.Drawing.Point(549, 405);
            this.chkDemo.Name = "chkDemo";
            this.chkDemo.Size = new System.Drawing.Size(72, 16);
            this.chkDemo.TabIndex = 59;
            this.chkDemo.Text = "데모시연";
            this.chkDemo.UseVisualStyleBackColor = true;
            this.chkDemo.CheckedChanged += new System.EventHandler(this.chkDemo_CheckedChanged);
            // 
            // btnTempomove
            // 
            this.btnTempomove.Location = new System.Drawing.Point(1284, 681);
            this.btnTempomove.Name = "btnTempomove";
            this.btnTempomove.Size = new System.Drawing.Size(97, 35);
            this.btnTempomove.TabIndex = 60;
            this.btnTempomove.Text = "Tempomove";
            this.btnTempomove.UseVisualStyleBackColor = true;
            this.btnTempomove.Click += new System.EventHandler(this.btnTempomove_Click);
            // 
            // button12
            // 
            this.button12.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button12.Location = new System.Drawing.Point(1370, 477);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(82, 63);
            this.button12.TabIndex = 56;
            this.button12.Text = "100A  (4번)";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button13
            // 
            this.button13.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button13.Location = new System.Drawing.Point(1370, 555);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(82, 63);
            this.button13.TabIndex = 56;
            this.button13.Text = "100A_P (4번)";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button14
            // 
            this.button14.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button14.Location = new System.Drawing.Point(1018, 477);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(82, 63);
            this.button14.TabIndex = 56;
            this.button14.Text = "200P_R (0번)";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button15
            // 
            this.button15.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button15.Location = new System.Drawing.Point(1018, 555);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(82, 63);
            this.button15.TabIndex = 56;
            this.button15.Text = "200_P (0번)";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(1086, 676);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(97, 35);
            this.simpleButton1.TabIndex = 18;
            this.simpleButton1.Text = "작업정지";
            this.simpleButton1.Click += new System.EventHandler(this.btnDemoStop_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Location = new System.Drawing.Point(964, 676);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(97, 35);
            this.simpleButton2.TabIndex = 18;
            this.simpleButton2.Text = "작업시작";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // listBox4
            // 
            this.listBox4.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listBox4.FormattingEnabled = true;
            this.listBox4.ItemHeight = 11;
            this.listBox4.Location = new System.Drawing.Point(1101, 732);
            this.listBox4.Name = "listBox4";
            this.listBox4.Size = new System.Drawing.Size(557, 81);
            this.listBox4.TabIndex = 21;
            // 
            // chkLineRobotSelect
            // 
            this.chkLineRobotSelect.AutoSize = true;
            this.chkLineRobotSelect.Location = new System.Drawing.Point(641, 405);
            this.chkLineRobotSelect.Name = "chkLineRobotSelect";
            this.chkLineRobotSelect.Size = new System.Drawing.Size(124, 16);
            this.chkLineRobotSelect.TabIndex = 59;
            this.chkLineRobotSelect.Text = "직선주행로봇 선택";
            this.chkLineRobotSelect.UseVisualStyleBackColor = true;
            // 
            // txtLineSelectRobot
            // 
            this.txtLineSelectRobot.Location = new System.Drawing.Point(771, 400);
            this.txtLineSelectRobot.Name = "txtLineSelectRobot";
            this.txtLineSelectRobot.Size = new System.Drawing.Size(226, 21);
            this.txtLineSelectRobot.TabIndex = 14;
            this.txtLineSelectRobot.Text = "R_001,R_002";
            // 
            // txt_S_LineSelectRobot
            // 
            this.txt_S_LineSelectRobot.Location = new System.Drawing.Point(771, 426);
            this.txt_S_LineSelectRobot.Name = "txt_S_LineSelectRobot";
            this.txt_S_LineSelectRobot.Size = new System.Drawing.Size(226, 21);
            this.txt_S_LineSelectRobot.TabIndex = 14;
            this.txt_S_LineSelectRobot.Text = "R_006";
            // 
            // chkSline
            // 
            this.chkSline.AutoSize = true;
            this.chkSline.Location = new System.Drawing.Point(641, 431);
            this.chkSline.Name = "chkSline";
            this.chkSline.Size = new System.Drawing.Size(120, 16);
            this.chkSline.TabIndex = 59;
            this.chkSline.Text = "S자주행로봇 선택";
            this.chkSline.UseVisualStyleBackColor = true;
            // 
            // txt_DriveSelectRobot
            // 
            this.txt_DriveSelectRobot.Location = new System.Drawing.Point(771, 453);
            this.txt_DriveSelectRobot.Name = "txt_DriveSelectRobot";
            this.txt_DriveSelectRobot.Size = new System.Drawing.Size(226, 21);
            this.txt_DriveSelectRobot.TabIndex = 14;
            this.txt_DriveSelectRobot.Text = "R_000,R_003,R_006";
            // 
            // chkDrive
            // 
            this.chkDrive.AutoSize = true;
            this.chkDrive.Location = new System.Drawing.Point(641, 458);
            this.chkDrive.Name = "chkDrive";
            this.chkDrive.Size = new System.Drawing.Size(124, 16);
            this.chkDrive.TabIndex = 59;
            this.chkDrive.Text = "기본주행로봇 선택";
            this.chkDrive.UseVisualStyleBackColor = true;
            // 
            // txtURselectrobot
            // 
            this.txtURselectrobot.Location = new System.Drawing.Point(771, 480);
            this.txtURselectrobot.Name = "txtURselectrobot";
            this.txtURselectrobot.Size = new System.Drawing.Size(226, 21);
            this.txtURselectrobot.TabIndex = 14;
            this.txtURselectrobot.Text = "R_004";
            // 
            // chkURdrive
            // 
            this.chkURdrive.AutoSize = true;
            this.chkURdrive.Location = new System.Drawing.Point(641, 485);
            this.chkURdrive.Name = "chkURdrive";
            this.chkURdrive.Size = new System.Drawing.Size(64, 16);
            this.chkURdrive.TabIndex = 59;
            this.chkURdrive.Text = "UR선택";
            this.chkURdrive.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(577, 556);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(136, 21);
            this.textBox1.TabIndex = 14;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(577, 583);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(136, 21);
            this.textBox2.TabIndex = 14;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(577, 610);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(136, 21);
            this.textBox3.TabIndex = 14;
            // 
            // simpleButton3
            // 
            this.simpleButton3.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton3.Appearance.Options.UseFont = true;
            this.simpleButton3.Location = new System.Drawing.Point(719, 556);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(54, 21);
            this.simpleButton3.TabIndex = 18;
            this.simpleButton3.Text = "chg";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton4
            // 
            this.simpleButton4.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton4.Appearance.Options.UseFont = true;
            this.simpleButton4.Location = new System.Drawing.Point(719, 580);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(54, 21);
            this.simpleButton4.TabIndex = 18;
            this.simpleButton4.Text = "chg";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // simpleButton5
            // 
            this.simpleButton5.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton5.Appearance.Options.UseFont = true;
            this.simpleButton5.Location = new System.Drawing.Point(719, 607);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(54, 21);
            this.simpleButton5.TabIndex = 18;
            this.simpleButton5.Text = "chg";
            this.simpleButton5.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(809, 556);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(136, 21);
            this.textBox4.TabIndex = 14;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(809, 583);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(136, 21);
            this.textBox5.TabIndex = 14;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(809, 610);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(136, 21);
            this.textBox6.TabIndex = 14;
            // 
            // simpleButton6
            // 
            this.simpleButton6.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton6.Appearance.Options.UseFont = true;
            this.simpleButton6.Location = new System.Drawing.Point(951, 556);
            this.simpleButton6.Name = "simpleButton6";
            this.simpleButton6.Size = new System.Drawing.Size(54, 21);
            this.simpleButton6.TabIndex = 18;
            this.simpleButton6.Text = "chg";
            this.simpleButton6.Click += new System.EventHandler(this.simpleButton6_Click);
            // 
            // simpleButton7
            // 
            this.simpleButton7.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton7.Appearance.Options.UseFont = true;
            this.simpleButton7.Location = new System.Drawing.Point(951, 580);
            this.simpleButton7.Name = "simpleButton7";
            this.simpleButton7.Size = new System.Drawing.Size(54, 21);
            this.simpleButton7.TabIndex = 18;
            this.simpleButton7.Text = "chg";
            this.simpleButton7.Click += new System.EventHandler(this.simpleButton7_Click);
            // 
            // simpleButton8
            // 
            this.simpleButton8.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton8.Appearance.Options.UseFont = true;
            this.simpleButton8.Location = new System.Drawing.Point(951, 607);
            this.simpleButton8.Name = "simpleButton8";
            this.simpleButton8.Size = new System.Drawing.Size(54, 21);
            this.simpleButton8.TabIndex = 18;
            this.simpleButton8.Text = "chg";
            this.simpleButton8.Click += new System.EventHandler(this.simpleButton8_Click);
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(704, 643);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(136, 21);
            this.textBox7.TabIndex = 14;
            // 
            // simpleButton9
            // 
            this.simpleButton9.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton9.Appearance.Options.UseFont = true;
            this.simpleButton9.Location = new System.Drawing.Point(846, 640);
            this.simpleButton9.Name = "simpleButton9";
            this.simpleButton9.Size = new System.Drawing.Size(54, 21);
            this.simpleButton9.TabIndex = 18;
            this.simpleButton9.Text = "chg";
            this.simpleButton9.Click += new System.EventHandler(this.simpleButton9_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(555, 562);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(11, 12);
            this.label14.TabIndex = 61;
            this.label14.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(555, 589);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(11, 12);
            this.label15.TabIndex = 61;
            this.label15.Text = "1";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(555, 616);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(11, 12);
            this.label16.TabIndex = 61;
            this.label16.Text = "2";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(792, 559);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(11, 12);
            this.label17.TabIndex = 61;
            this.label17.Text = "3";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(792, 586);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(11, 12);
            this.label18.TabIndex = 61;
            this.label18.Text = "4";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(792, 613);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(11, 12);
            this.label19.TabIndex = 61;
            this.label19.Text = "5";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(687, 646);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(11, 12);
            this.label20.TabIndex = 61;
            this.label20.Text = "6";
            // 
            // listBox5
            // 
            this.listBox5.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listBox5.FormattingEnabled = true;
            this.listBox5.ItemHeight = 11;
            this.listBox5.Location = new System.Drawing.Point(1574, 87);
            this.listBox5.Name = "listBox5";
            this.listBox5.Size = new System.Drawing.Size(109, 334);
            this.listBox5.TabIndex = 21;
            // 
            // listBox6
            // 
            this.listBox6.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listBox6.FormattingEnabled = true;
            this.listBox6.ItemHeight = 11;
            this.listBox6.Location = new System.Drawing.Point(1009, 87);
            this.listBox6.Name = "listBox6";
            this.listBox6.Size = new System.Drawing.Size(521, 334);
            this.listBox6.TabIndex = 21;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(1036, 427);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(44, 21);
            this.textBox8.TabIndex = 14;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(1014, 433);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(11, 12);
            this.label21.TabIndex = 61;
            this.label21.Text = "0";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(1086, 427);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(44, 21);
            this.textBox9.TabIndex = 14;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(1166, 427);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(44, 21);
            this.textBox10.TabIndex = 14;
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(1216, 427);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(44, 21);
            this.textBox11.TabIndex = 14;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(1144, 433);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(11, 12);
            this.label22.TabIndex = 61;
            this.label22.Text = "1";
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(1287, 427);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(44, 21);
            this.textBox12.TabIndex = 14;
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(1337, 427);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(44, 21);
            this.textBox13.TabIndex = 14;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(1265, 433);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(11, 12);
            this.label23.TabIndex = 61;
            this.label23.Text = "2";
            // 
            // textBox14
            // 
            this.textBox14.Location = new System.Drawing.Point(1434, 427);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(44, 21);
            this.textBox14.TabIndex = 14;
            // 
            // textBox15
            // 
            this.textBox15.Location = new System.Drawing.Point(1484, 427);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(44, 21);
            this.textBox15.TabIndex = 14;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(1412, 433);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(11, 12);
            this.label25.TabIndex = 61;
            this.label25.Text = "3";
            // 
            // textBox16
            // 
            this.textBox16.Location = new System.Drawing.Point(1036, 450);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(44, 21);
            this.textBox16.TabIndex = 14;
            // 
            // textBox17
            // 
            this.textBox17.Location = new System.Drawing.Point(1086, 450);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(44, 21);
            this.textBox17.TabIndex = 14;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(1014, 456);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(11, 12);
            this.label26.TabIndex = 61;
            this.label26.Text = "4";
            // 
            // textBox18
            // 
            this.textBox18.Location = new System.Drawing.Point(1166, 450);
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new System.Drawing.Size(44, 21);
            this.textBox18.TabIndex = 14;
            // 
            // textBox19
            // 
            this.textBox19.Location = new System.Drawing.Point(1216, 450);
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new System.Drawing.Size(44, 21);
            this.textBox19.TabIndex = 14;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(1144, 456);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(11, 12);
            this.label27.TabIndex = 61;
            this.label27.Text = "5";
            // 
            // textBox20
            // 
            this.textBox20.Location = new System.Drawing.Point(1287, 450);
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new System.Drawing.Size(44, 21);
            this.textBox20.TabIndex = 14;
            // 
            // textBox21
            // 
            this.textBox21.Location = new System.Drawing.Point(1337, 450);
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new System.Drawing.Size(44, 21);
            this.textBox21.TabIndex = 14;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(1265, 456);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(11, 12);
            this.label28.TabIndex = 61;
            this.label28.Text = "6";
            // 
            // btnListclear
            // 
            this.btnListclear.Location = new System.Drawing.Point(1009, 54);
            this.btnListclear.Name = "btnListclear";
            this.btnListclear.Size = new System.Drawing.Size(74, 29);
            this.btnListclear.TabIndex = 62;
            this.btnListclear.Text = "Clear";
            this.btnListclear.UseVisualStyleBackColor = true;
            this.btnListclear.Click += new System.EventHandler(this.btnListclear_Click);
            // 
            // txtDockSelectRobot
            // 
            this.txtDockSelectRobot.Location = new System.Drawing.Point(771, 511);
            this.txtDockSelectRobot.Name = "txtDockSelectRobot";
            this.txtDockSelectRobot.Size = new System.Drawing.Size(226, 21);
            this.txtDockSelectRobot.TabIndex = 14;
            this.txtDockSelectRobot.Text = "R_005";
            // 
            // chkDockdrive
            // 
            this.chkDockdrive.AutoSize = true;
            this.chkDockdrive.Location = new System.Drawing.Point(641, 516);
            this.chkDockdrive.Name = "chkDockdrive";
            this.chkDockdrive.Size = new System.Drawing.Size(76, 16);
            this.chkDockdrive.TabIndex = 59;
            this.chkDockdrive.Text = "도킹 선택";
            this.chkDockdrive.UseVisualStyleBackColor = true;
            // 
            // TaskOperation_Ctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.Controls.Add(this.btnListclear);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.chkLiftRun);
            this.Controls.Add(this.simpleButton9);
            this.Controls.Add(this.simpleButton8);
            this.Controls.Add(this.simpleButton5);
            this.Controls.Add(this.simpleButton7);
            this.Controls.Add(this.simpleButton4);
            this.Controls.Add(this.simpleButton6);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.btnTaskResume);
            this.Controls.Add(this.chkLineDrive);
            this.Controls.Add(this.btnTaskPause);
            this.Controls.Add(this.chkSpeedDrive);
            this.Controls.Add(this.chkURRun);
            this.Controls.Add(this.btnTempomove);
            this.Controls.Add(this.chkCurveRun);
            this.Controls.Add(this.chkDockdrive);
            this.Controls.Add(this.chkURdrive);
            this.Controls.Add(this.chkDrive);
            this.Controls.Add(this.chkSline);
            this.Controls.Add(this.chkLineRobotSelect);
            this.Controls.Add(this.chkDemo);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txtDockSelectRobot);
            this.Controls.Add(this.txtURselectrobot);
            this.Controls.Add(this.txt_DriveSelectRobot);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.txt_S_LineSelectRobot);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox21);
            this.Controls.Add(this.textBox20);
            this.Controls.Add(this.textBox19);
            this.Controls.Add(this.textBox18);
            this.Controls.Add(this.textBox17);
            this.Controls.Add(this.textBox16);
            this.Controls.Add(this.textBox15);
            this.Controls.Add(this.textBox14);
            this.Controls.Add(this.textBox13);
            this.Controls.Add(this.textBox12);
            this.Controls.Add(this.textBox11);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtLineSelectRobot);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnliftdown);
            this.Controls.Add(this.btnliftup);
            this.Controls.Add(this.btnLiftSet);
            this.Controls.Add(this.cboRobot_lift);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.cboliftrobotID);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox4);
            this.Controls.Add(this.listBox3);
            this.Controls.Add(this.listBox6);
            this.Controls.Add(this.listBox5);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView_reg);
            this.Controls.Add(this.groupBox_btn);
            this.Name = "TaskOperation_Ctrl";
            this.Size = new System.Drawing.Size(1686, 882);
            this.Load += new System.EventHandler(this.TaskOperation_Ctrl_Load);
            this.groupBox_btn.ResumeLayout(false);
            this.groupBox_btn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_reg)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox_btn;
        private System.Windows.Forms.TextBox txtTaskname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTaskCnt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView_reg;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskstatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskid;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskname;
        private System.Windows.Forms.DataGridViewTextBoxColumn missionlist;
        private System.Windows.Forms.DataGridViewTextBoxColumn robotlist;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskLoopflag;
        private System.Windows.Forms.DataGridViewTextBoxColumn robotgroup;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton btnTaskStop1;
        private DevExpress.XtraEditors.SimpleButton btnTaskRun1;
        private DevExpress.XtraEditors.SimpleButton btnWaitPos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chk300D;
        private System.Windows.Forms.CheckBox chk1000_1;
        private System.Windows.Forms.CheckBox chk500_2;
        private System.Windows.Forms.CheckBox chk500_1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtWaitpos4;
        private System.Windows.Forms.TextBox txtWaitpos3;
        private System.Windows.Forms.TextBox txtWaitpos2;
        private System.Windows.Forms.TextBox txtWaitpos1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTrackdriveCnt;
        private System.Windows.Forms.TextBox txtLiftCnt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.SimpleButton btnDemoRun;
        private System.Windows.Forms.Timer timer_StartTrack;
        private DevExpress.XtraEditors.SimpleButton btnTaskResume;
        private DevExpress.XtraEditors.SimpleButton btnTaskPause;
        private System.Windows.Forms.CheckBox chkLiftRun;
        private DevExpress.XtraEditors.SimpleButton btnDemoStop;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Timer timer_runnigTime;
        private System.Windows.Forms.ComboBox cboliftrobotID;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnLiftSet;
        private System.Windows.Forms.ComboBox cboRobot_lift;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button btnliftdown;
        private System.Windows.Forms.Button btnliftup;
        private System.Windows.Forms.Timer timer_CrashCheck;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkRobotPosRec;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.CheckBox chkCurveRun;
        private System.Windows.Forms.CheckBox chkURRun;
        private System.Windows.Forms.CheckBox chkLineDrive;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Timer timer_LinedrivewaitokChk;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.CheckBox chk1000_2;
        private System.Windows.Forms.CheckBox chkDemo;
        private System.Windows.Forms.CheckBox chkSpeedDrive;
        private System.Windows.Forms.Button btnTempomove;
        private System.Windows.Forms.CheckBox chk100M;
        private DevExpress.XtraEditors.SimpleButton btnWaitPos2;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtWaitpos6;
        private System.Windows.Forms.TextBox txtWaitpos5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtWaitpos7;
        private System.Windows.Forms.CheckBox chk200P;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private System.Windows.Forms.ListBox listBox4;
        private System.Windows.Forms.CheckBox chkLineRobotSelect;
        private System.Windows.Forms.TextBox txtLineSelectRobot;
        private System.Windows.Forms.TextBox txt_S_LineSelectRobot;
        private System.Windows.Forms.CheckBox chkSline;
        private System.Windows.Forms.TextBox txt_DriveSelectRobot;
        private System.Windows.Forms.CheckBox chkDrive;
        private System.Windows.Forms.TextBox txtURselectrobot;
        private System.Windows.Forms.CheckBox chkURdrive;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private DevExpress.XtraEditors.SimpleButton simpleButton6;
        private DevExpress.XtraEditors.SimpleButton simpleButton7;
        private DevExpress.XtraEditors.SimpleButton simpleButton8;
        private System.Windows.Forms.TextBox textBox7;
        private DevExpress.XtraEditors.SimpleButton simpleButton9;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ListBox listBox5;
        private System.Windows.Forms.ListBox listBox6;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.TextBox textBox17;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox textBox18;
        private System.Windows.Forms.TextBox textBox19;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox textBox20;
        private System.Windows.Forms.TextBox textBox21;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Button btnListclear;
        private System.Windows.Forms.TextBox txtDockSelectRobot;
        private System.Windows.Forms.CheckBox chkDockdrive;
    }
}
