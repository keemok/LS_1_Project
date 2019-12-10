namespace Syscon_Solution.FleetManager.UI.Edit
{
    partial class MissionEdit_Ctrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MissionEdit_Ctrl));
            this.panel_MissionEdit_left = new System.Windows.Forms.Panel();
            this.btnActionidxDn1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnActionidxUp1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnActionDelete1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnActionInsert1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnMissionDelete1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnMissionInsert1 = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox_ActionOpt = new System.Windows.Forms.GroupBox();
            this.btnRobotPosRead = new System.Windows.Forms.Button();
            this.cboPassingflag = new System.Windows.Forms.ComboBox();
            this.chkPassingflag = new System.Windows.Forms.CheckBox();
            this.cboavoid = new System.Windows.Forms.ComboBox();
            this.chkavoid = new System.Windows.Forms.CheckBox();
            this.chkwp_tolerance = new System.Windows.Forms.CheckBox();
            this.chkd_drive = new System.Windows.Forms.CheckBox();
            this.chkp_drive = new System.Windows.Forms.CheckBox();
            this.chkyaw_goal_tolerance = new System.Windows.Forms.CheckBox();
            this.btnMissionSave1 = new DevExpress.XtraEditors.SimpleButton();
            this.chkxy_goal_tolerance = new System.Windows.Forms.CheckBox();
            this.chkmax_trans_vel = new System.Windows.Forms.CheckBox();
            this.txtGoalpoint_theta = new System.Windows.Forms.TextBox();
            this.groupBox_ActionWait = new System.Windows.Forms.GroupBox();
            this.cboXisWait_mode = new System.Windows.Forms.ComboBox();
            this.txtXisWaitTime = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtXisWait_ID = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox_lift = new System.Windows.Forms.GroupBox();
            this.radioButton_backward = new System.Windows.Forms.RadioButton();
            this.radioButton_forward = new System.Windows.Forms.RadioButton();
            this.cboPalletmode = new System.Windows.Forms.ComboBox();
            this.txtPalletID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPalletDist = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox_UR = new System.Windows.Forms.GroupBox();
            this.cboURdata = new System.Windows.Forms.ComboBox();
            this.cboURmode = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox_basicmove = new System.Windows.Forms.GroupBox();
            this.cboBasicmove_mode = new System.Windows.Forms.ComboBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBasicmove_action = new System.Windows.Forms.TextBox();
            this.txtGoalpoint_Y = new System.Windows.Forms.TextBox();
            this.txtwp_tolerance = new System.Windows.Forms.TextBox();
            this.txtd_drive = new System.Windows.Forms.TextBox();
            this.txtp_drive = new System.Windows.Forms.TextBox();
            this.txtyaw_goal_tolerance = new System.Windows.Forms.TextBox();
            this.txtxy_goal_tolerance = new System.Windows.Forms.TextBox();
            this.txtmax_trans_vel = new System.Windows.Forms.TextBox();
            this.txtGoalpoint_X = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox_ActionData = new System.Windows.Forms.ListBox();
            this.listBox_Mission = new System.Windows.Forms.ListBox();
            this.panel_MissionEdit_right = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.chkRobotpos = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.zoomTrackBarControl1 = new DevExpress.XtraEditors.ZoomTrackBarControl();
            this.txtMapsize = new System.Windows.Forms.TextBox();
            this.chkGlobalCost = new DevExpress.XtraEditors.CheckEdit();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.chkDelivery = new System.Windows.Forms.CheckBox();
            this.chkInitPosSet = new System.Windows.Forms.CheckBox();
            this.chkAngluar = new System.Windows.Forms.CheckBox();
            this.txtAngluar = new System.Windows.Forms.TextBox();
            this.pb_cost = new System.Windows.Forms.PictureBox();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnDn = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.txtTY = new System.Windows.Forms.TextBox();
            this.txtTX = new System.Windows.Forms.TextBox();
            this.txtYpos = new System.Windows.Forms.TextBox();
            this.txtXpos = new System.Windows.Forms.TextBox();
            this.txtYcell = new System.Windows.Forms.TextBox();
            this.txtXcell = new System.Windows.Forms.TextBox();
            this.pb_map = new System.Windows.Forms.PictureBox();
            this.cboRobotID = new System.Windows.Forms.ComboBox();
            this.btnMaploading = new System.Windows.Forms.Button();
            this.timer_Robotposchk = new System.Windows.Forms.Timer(this.components);
            this.panel_MissionEdit_left.SuspendLayout();
            this.groupBox_ActionOpt.SuspendLayout();
            this.groupBox_ActionWait.SuspendLayout();
            this.groupBox_lift.SuspendLayout();
            this.groupBox_UR.SuspendLayout();
            this.groupBox_basicmove.SuspendLayout();
            this.panel_MissionEdit_right.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGlobalCost.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_cost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_map)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_MissionEdit_left
            // 
            this.panel_MissionEdit_left.Controls.Add(this.btnActionidxDn1);
            this.panel_MissionEdit_left.Controls.Add(this.btnActionidxUp1);
            this.panel_MissionEdit_left.Controls.Add(this.btnActionDelete1);
            this.panel_MissionEdit_left.Controls.Add(this.btnActionInsert1);
            this.panel_MissionEdit_left.Controls.Add(this.btnMissionDelete1);
            this.panel_MissionEdit_left.Controls.Add(this.btnMissionInsert1);
            this.panel_MissionEdit_left.Controls.Add(this.groupBox_ActionOpt);
            this.panel_MissionEdit_left.Controls.Add(this.label2);
            this.panel_MissionEdit_left.Controls.Add(this.label1);
            this.panel_MissionEdit_left.Controls.Add(this.listBox_ActionData);
            this.panel_MissionEdit_left.Controls.Add(this.listBox_Mission);
            this.panel_MissionEdit_left.Location = new System.Drawing.Point(3, 3);
            this.panel_MissionEdit_left.Name = "panel_MissionEdit_left";
            this.panel_MissionEdit_left.Size = new System.Drawing.Size(740, 835);
            this.panel_MissionEdit_left.TabIndex = 5;
            // 
            // btnActionidxDn1
            // 
            this.btnActionidxDn1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnActionidxDn1.ImageOptions.Image")));
            this.btnActionidxDn1.Location = new System.Drawing.Point(677, 183);
            this.btnActionidxDn1.Name = "btnActionidxDn1";
            this.btnActionidxDn1.Size = new System.Drawing.Size(40, 43);
            this.btnActionidxDn1.TabIndex = 7;
            this.btnActionidxDn1.Click += new System.EventHandler(this.btnActionidxDn_Click);
            // 
            // btnActionidxUp1
            // 
            this.btnActionidxUp1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnActionidxUp1.ImageOptions.Image")));
            this.btnActionidxUp1.Location = new System.Drawing.Point(677, 124);
            this.btnActionidxUp1.Name = "btnActionidxUp1";
            this.btnActionidxUp1.Size = new System.Drawing.Size(40, 43);
            this.btnActionidxUp1.TabIndex = 7;
            this.btnActionidxUp1.Click += new System.EventHandler(this.btnActionidxUp_Click);
            // 
            // btnActionDelete1
            // 
            this.btnActionDelete1.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActionDelete1.Appearance.Options.UseFont = true;
            this.btnActionDelete1.Location = new System.Drawing.Point(537, 379);
            this.btnActionDelete1.Name = "btnActionDelete1";
            this.btnActionDelete1.Size = new System.Drawing.Size(119, 36);
            this.btnActionDelete1.TabIndex = 6;
            this.btnActionDelete1.Text = "액션 삭제";
            this.btnActionDelete1.Click += new System.EventHandler(this.btnActionDelete_Click);
            // 
            // btnActionInsert1
            // 
            this.btnActionInsert1.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActionInsert1.Appearance.Options.UseFont = true;
            this.btnActionInsert1.Location = new System.Drawing.Point(406, 380);
            this.btnActionInsert1.Name = "btnActionInsert1";
            this.btnActionInsert1.Size = new System.Drawing.Size(119, 36);
            this.btnActionInsert1.TabIndex = 6;
            this.btnActionInsert1.Text = "액션 추가";
            this.btnActionInsert1.Click += new System.EventHandler(this.btnActionInsert_Click);
            // 
            // btnMissionDelete1
            // 
            this.btnMissionDelete1.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMissionDelete1.Appearance.Options.UseFont = true;
            this.btnMissionDelete1.Location = new System.Drawing.Point(241, 380);
            this.btnMissionDelete1.Name = "btnMissionDelete1";
            this.btnMissionDelete1.Size = new System.Drawing.Size(119, 36);
            this.btnMissionDelete1.TabIndex = 6;
            this.btnMissionDelete1.Text = "미션 삭제";
            this.btnMissionDelete1.Click += new System.EventHandler(this.btnMissionDelete_Click);
            // 
            // btnMissionInsert1
            // 
            this.btnMissionInsert1.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMissionInsert1.Appearance.Options.UseFont = true;
            this.btnMissionInsert1.Location = new System.Drawing.Point(116, 380);
            this.btnMissionInsert1.Name = "btnMissionInsert1";
            this.btnMissionInsert1.Size = new System.Drawing.Size(119, 36);
            this.btnMissionInsert1.TabIndex = 6;
            this.btnMissionInsert1.Text = "미션 추가";
            this.btnMissionInsert1.Click += new System.EventHandler(this.btnMissionInsert_Click);
            // 
            // groupBox_ActionOpt
            // 
            this.groupBox_ActionOpt.Controls.Add(this.btnRobotPosRead);
            this.groupBox_ActionOpt.Controls.Add(this.cboPassingflag);
            this.groupBox_ActionOpt.Controls.Add(this.chkPassingflag);
            this.groupBox_ActionOpt.Controls.Add(this.cboavoid);
            this.groupBox_ActionOpt.Controls.Add(this.chkavoid);
            this.groupBox_ActionOpt.Controls.Add(this.chkwp_tolerance);
            this.groupBox_ActionOpt.Controls.Add(this.chkd_drive);
            this.groupBox_ActionOpt.Controls.Add(this.chkp_drive);
            this.groupBox_ActionOpt.Controls.Add(this.chkyaw_goal_tolerance);
            this.groupBox_ActionOpt.Controls.Add(this.btnMissionSave1);
            this.groupBox_ActionOpt.Controls.Add(this.chkxy_goal_tolerance);
            this.groupBox_ActionOpt.Controls.Add(this.chkmax_trans_vel);
            this.groupBox_ActionOpt.Controls.Add(this.txtGoalpoint_theta);
            this.groupBox_ActionOpt.Controls.Add(this.groupBox_ActionWait);
            this.groupBox_ActionOpt.Controls.Add(this.groupBox_lift);
            this.groupBox_ActionOpt.Controls.Add(this.label4);
            this.groupBox_ActionOpt.Controls.Add(this.groupBox_UR);
            this.groupBox_ActionOpt.Controls.Add(this.groupBox_basicmove);
            this.groupBox_ActionOpt.Controls.Add(this.txtGoalpoint_Y);
            this.groupBox_ActionOpt.Controls.Add(this.txtwp_tolerance);
            this.groupBox_ActionOpt.Controls.Add(this.txtd_drive);
            this.groupBox_ActionOpt.Controls.Add(this.txtp_drive);
            this.groupBox_ActionOpt.Controls.Add(this.txtyaw_goal_tolerance);
            this.groupBox_ActionOpt.Controls.Add(this.txtxy_goal_tolerance);
            this.groupBox_ActionOpt.Controls.Add(this.txtmax_trans_vel);
            this.groupBox_ActionOpt.Controls.Add(this.txtGoalpoint_X);
            this.groupBox_ActionOpt.Controls.Add(this.label5);
            this.groupBox_ActionOpt.Controls.Add(this.label6);
            this.groupBox_ActionOpt.Controls.Add(this.label40);
            this.groupBox_ActionOpt.Controls.Add(this.label41);
            this.groupBox_ActionOpt.Controls.Add(this.label42);
            this.groupBox_ActionOpt.Location = new System.Drawing.Point(24, 427);
            this.groupBox_ActionOpt.Name = "groupBox_ActionOpt";
            this.groupBox_ActionOpt.Size = new System.Drawing.Size(702, 396);
            this.groupBox_ActionOpt.TabIndex = 5;
            this.groupBox_ActionOpt.TabStop = false;
            this.groupBox_ActionOpt.Text = "액션 정보";
            // 
            // btnRobotPosRead
            // 
            this.btnRobotPosRead.Location = new System.Drawing.Point(579, 20);
            this.btnRobotPosRead.Name = "btnRobotPosRead";
            this.btnRobotPosRead.Size = new System.Drawing.Size(68, 31);
            this.btnRobotPosRead.TabIndex = 13;
            this.btnRobotPosRead.Text = "좌표읽기";
            this.btnRobotPosRead.UseVisualStyleBackColor = true;
            this.btnRobotPosRead.Click += new System.EventHandler(this.btnRobotPosRead_Click);
            // 
            // cboPassingflag
            // 
            this.cboPassingflag.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cboPassingflag.FormattingEnabled = true;
            this.cboPassingflag.Items.AddRange(new object[] {
            "false",
            "true"});
            this.cboPassingflag.Location = new System.Drawing.Point(172, 286);
            this.cboPassingflag.Name = "cboPassingflag";
            this.cboPassingflag.Size = new System.Drawing.Size(83, 25);
            this.cboPassingflag.TabIndex = 12;
            // 
            // chkPassingflag
            // 
            this.chkPassingflag.AutoSize = true;
            this.chkPassingflag.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkPassingflag.Location = new System.Drawing.Point(20, 288);
            this.chkPassingflag.Name = "chkPassingflag";
            this.chkPassingflag.Size = new System.Drawing.Size(103, 21);
            this.chkPassingflag.TabIndex = 11;
            this.chkPassingflag.Text = "Passing flag";
            this.chkPassingflag.UseVisualStyleBackColor = true;
            // 
            // cboavoid
            // 
            this.cboavoid.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cboavoid.FormattingEnabled = true;
            this.cboavoid.Items.AddRange(new object[] {
            "false",
            "true"});
            this.cboavoid.Location = new System.Drawing.Point(172, 255);
            this.cboavoid.Name = "cboavoid";
            this.cboavoid.Size = new System.Drawing.Size(83, 25);
            this.cboavoid.TabIndex = 12;
            // 
            // chkavoid
            // 
            this.chkavoid.AutoSize = true;
            this.chkavoid.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkavoid.Location = new System.Drawing.Point(20, 257);
            this.chkavoid.Name = "chkavoid";
            this.chkavoid.Size = new System.Drawing.Size(61, 21);
            this.chkavoid.TabIndex = 11;
            this.chkavoid.Text = "avoid";
            this.chkavoid.UseVisualStyleBackColor = true;
            // 
            // chkwp_tolerance
            // 
            this.chkwp_tolerance.AutoSize = true;
            this.chkwp_tolerance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkwp_tolerance.Location = new System.Drawing.Point(20, 228);
            this.chkwp_tolerance.Name = "chkwp_tolerance";
            this.chkwp_tolerance.Size = new System.Drawing.Size(108, 21);
            this.chkwp_tolerance.TabIndex = 11;
            this.chkwp_tolerance.Text = "wp_tolerance";
            this.chkwp_tolerance.UseVisualStyleBackColor = true;
            // 
            // chkd_drive
            // 
            this.chkd_drive.AutoSize = true;
            this.chkd_drive.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkd_drive.Location = new System.Drawing.Point(20, 201);
            this.chkd_drive.Name = "chkd_drive";
            this.chkd_drive.Size = new System.Drawing.Size(72, 21);
            this.chkd_drive.TabIndex = 11;
            this.chkd_drive.Text = "d_drive";
            this.chkd_drive.UseVisualStyleBackColor = true;
            // 
            // chkp_drive
            // 
            this.chkp_drive.AutoSize = true;
            this.chkp_drive.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkp_drive.Location = new System.Drawing.Point(20, 173);
            this.chkp_drive.Name = "chkp_drive";
            this.chkp_drive.Size = new System.Drawing.Size(72, 21);
            this.chkp_drive.TabIndex = 11;
            this.chkp_drive.Text = "p_drive";
            this.chkp_drive.UseVisualStyleBackColor = true;
            // 
            // chkyaw_goal_tolerance
            // 
            this.chkyaw_goal_tolerance.AutoSize = true;
            this.chkyaw_goal_tolerance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkyaw_goal_tolerance.Location = new System.Drawing.Point(20, 146);
            this.chkyaw_goal_tolerance.Name = "chkyaw_goal_tolerance";
            this.chkyaw_goal_tolerance.Size = new System.Drawing.Size(147, 21);
            this.chkyaw_goal_tolerance.TabIndex = 11;
            this.chkyaw_goal_tolerance.Text = "yaw_goal_tolerance";
            this.chkyaw_goal_tolerance.UseVisualStyleBackColor = true;
            // 
            // btnMissionSave1
            // 
            this.btnMissionSave1.Appearance.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnMissionSave1.Appearance.Options.UseFont = true;
            this.btnMissionSave1.Location = new System.Drawing.Point(136, 326);
            this.btnMissionSave1.Name = "btnMissionSave1";
            this.btnMissionSave1.Size = new System.Drawing.Size(119, 56);
            this.btnMissionSave1.TabIndex = 6;
            this.btnMissionSave1.Text = "정보 갱신";
            this.btnMissionSave1.Click += new System.EventHandler(this.btnMissionSave_Click);
            // 
            // chkxy_goal_tolerance
            // 
            this.chkxy_goal_tolerance.AutoSize = true;
            this.chkxy_goal_tolerance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkxy_goal_tolerance.Location = new System.Drawing.Point(20, 118);
            this.chkxy_goal_tolerance.Name = "chkxy_goal_tolerance";
            this.chkxy_goal_tolerance.Size = new System.Drawing.Size(137, 21);
            this.chkxy_goal_tolerance.TabIndex = 11;
            this.chkxy_goal_tolerance.Text = "xy_goal_tolerance";
            this.chkxy_goal_tolerance.UseVisualStyleBackColor = true;
            // 
            // chkmax_trans_vel
            // 
            this.chkmax_trans_vel.AutoSize = true;
            this.chkmax_trans_vel.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkmax_trans_vel.Location = new System.Drawing.Point(20, 90);
            this.chkmax_trans_vel.Name = "chkmax_trans_vel";
            this.chkmax_trans_vel.Size = new System.Drawing.Size(114, 21);
            this.chkmax_trans_vel.TabIndex = 11;
            this.chkmax_trans_vel.Text = "max_trans_vel";
            this.chkmax_trans_vel.UseVisualStyleBackColor = true;
            // 
            // txtGoalpoint_theta
            // 
            this.txtGoalpoint_theta.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtGoalpoint_theta.Location = new System.Drawing.Point(406, 30);
            this.txtGoalpoint_theta.Name = "txtGoalpoint_theta";
            this.txtGoalpoint_theta.Size = new System.Drawing.Size(83, 22);
            this.txtGoalpoint_theta.TabIndex = 8;
            // 
            // groupBox_ActionWait
            // 
            this.groupBox_ActionWait.Controls.Add(this.cboXisWait_mode);
            this.groupBox_ActionWait.Controls.Add(this.txtXisWaitTime);
            this.groupBox_ActionWait.Controls.Add(this.label8);
            this.groupBox_ActionWait.Controls.Add(this.txtXisWait_ID);
            this.groupBox_ActionWait.Controls.Add(this.label9);
            this.groupBox_ActionWait.Controls.Add(this.label10);
            this.groupBox_ActionWait.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox_ActionWait.Location = new System.Drawing.Point(301, 298);
            this.groupBox_ActionWait.Name = "groupBox_ActionWait";
            this.groupBox_ActionWait.Size = new System.Drawing.Size(355, 85);
            this.groupBox_ActionWait.TabIndex = 7;
            this.groupBox_ActionWait.TabStop = false;
            this.groupBox_ActionWait.Text = "Action_Wait";
            // 
            // cboXisWait_mode
            // 
            this.cboXisWait_mode.FormattingEnabled = true;
            this.cboXisWait_mode.Items.AddRange(new object[] {
            "XIS 응답대기",
            "타이머대기"});
            this.cboXisWait_mode.Location = new System.Drawing.Point(89, 28);
            this.cboXisWait_mode.Name = "cboXisWait_mode";
            this.cboXisWait_mode.Size = new System.Drawing.Size(109, 23);
            this.cboXisWait_mode.TabIndex = 1;
            // 
            // txtXisWaitTime
            // 
            this.txtXisWaitTime.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtXisWaitTime.Location = new System.Drawing.Point(245, 28);
            this.txtXisWaitTime.Name = "txtXisWaitTime";
            this.txtXisWaitTime.Size = new System.Drawing.Size(109, 22);
            this.txtXisWaitTime.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(209, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "Time";
            // 
            // txtXisWait_ID
            // 
            this.txtXisWait_ID.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtXisWait_ID.Location = new System.Drawing.Point(89, 53);
            this.txtXisWait_ID.Name = "txtXisWait_ID";
            this.txtXisWait_ID.Size = new System.Drawing.Size(109, 22);
            this.txtXisWait_ID.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 60);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 15);
            this.label9.TabIndex = 0;
            this.label9.Text = "XID";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 30);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 15);
            this.label10.TabIndex = 0;
            this.label10.Text = "mode";
            // 
            // groupBox_lift
            // 
            this.groupBox_lift.Controls.Add(this.radioButton_backward);
            this.groupBox_lift.Controls.Add(this.radioButton_forward);
            this.groupBox_lift.Controls.Add(this.cboPalletmode);
            this.groupBox_lift.Controls.Add(this.txtPalletID);
            this.groupBox_lift.Controls.Add(this.label7);
            this.groupBox_lift.Controls.Add(this.txtPalletDist);
            this.groupBox_lift.Controls.Add(this.label35);
            this.groupBox_lift.Controls.Add(this.label34);
            this.groupBox_lift.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox_lift.Location = new System.Drawing.Point(301, 178);
            this.groupBox_lift.Name = "groupBox_lift";
            this.groupBox_lift.Size = new System.Drawing.Size(355, 114);
            this.groupBox_lift.TabIndex = 7;
            this.groupBox_lift.TabStop = false;
            this.groupBox_lift.Text = "Stable_pallet";
            // 
            // radioButton_backward
            // 
            this.radioButton_backward.AutoSize = true;
            this.radioButton_backward.Location = new System.Drawing.Point(254, 60);
            this.radioButton_backward.Name = "radioButton_backward";
            this.radioButton_backward.Size = new System.Drawing.Size(76, 19);
            this.radioButton_backward.TabIndex = 2;
            this.radioButton_backward.TabStop = true;
            this.radioButton_backward.Text = "Backward";
            this.radioButton_backward.UseVisualStyleBackColor = true;
            // 
            // radioButton_forward
            // 
            this.radioButton_forward.AutoSize = true;
            this.radioButton_forward.Location = new System.Drawing.Point(254, 32);
            this.radioButton_forward.Name = "radioButton_forward";
            this.radioButton_forward.Size = new System.Drawing.Size(68, 19);
            this.radioButton_forward.TabIndex = 2;
            this.radioButton_forward.TabStop = true;
            this.radioButton_forward.Text = "Forward";
            this.radioButton_forward.UseVisualStyleBackColor = true;
            // 
            // cboPalletmode
            // 
            this.cboPalletmode.FormattingEnabled = true;
            this.cboPalletmode.Items.AddRange(new object[] {
            "-1(LiftDown)",
            "0(AutoDetect)",
            "1(LiftUp)"});
            this.cboPalletmode.Location = new System.Drawing.Point(89, 28);
            this.cboPalletmode.Name = "cboPalletmode";
            this.cboPalletmode.Size = new System.Drawing.Size(145, 23);
            this.cboPalletmode.TabIndex = 1;
            // 
            // txtPalletID
            // 
            this.txtPalletID.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtPalletID.Location = new System.Drawing.Point(89, 81);
            this.txtPalletID.Name = "txtPalletID";
            this.txtPalletID.Size = new System.Drawing.Size(145, 22);
            this.txtPalletID.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "PalletID";
            // 
            // txtPalletDist
            // 
            this.txtPalletDist.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtPalletDist.Location = new System.Drawing.Point(89, 53);
            this.txtPalletDist.Name = "txtPalletDist";
            this.txtPalletDist.Size = new System.Drawing.Size(145, 22);
            this.txtPalletDist.TabIndex = 1;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(23, 60);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(51, 15);
            this.label35.TabIndex = 0;
            this.label35.Text = "distance";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(23, 30);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(38, 15);
            this.label34.TabIndex = 0;
            this.label34.Text = "mode";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(358, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Th :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox_UR
            // 
            this.groupBox_UR.Controls.Add(this.cboURdata);
            this.groupBox_UR.Controls.Add(this.cboURmode);
            this.groupBox_UR.Controls.Add(this.label13);
            this.groupBox_UR.Controls.Add(this.label14);
            this.groupBox_UR.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox_UR.Location = new System.Drawing.Point(524, 58);
            this.groupBox_UR.Name = "groupBox_UR";
            this.groupBox_UR.Size = new System.Drawing.Size(169, 114);
            this.groupBox_UR.TabIndex = 8;
            this.groupBox_UR.TabStop = false;
            this.groupBox_UR.Text = "UR";
            // 
            // cboURdata
            // 
            this.cboURdata.FormattingEnabled = true;
            this.cboURdata.Items.AddRange(new object[] {
            "Pick",
            "Place"});
            this.cboURdata.Location = new System.Drawing.Point(66, 59);
            this.cboURdata.Name = "cboURdata";
            this.cboURdata.Size = new System.Drawing.Size(78, 23);
            this.cboURdata.TabIndex = 1;
            // 
            // cboURmode
            // 
            this.cboURmode.FormattingEnabled = true;
            this.cboURmode.Items.AddRange(new object[] {
            "Pick & Place"});
            this.cboURmode.Location = new System.Drawing.Point(66, 22);
            this.cboURmode.Name = "cboURmode";
            this.cboURmode.Size = new System.Drawing.Size(78, 23);
            this.cboURmode.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(19, 59);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(30, 15);
            this.label13.TabIndex = 0;
            this.label13.Text = "data";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 25);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(38, 15);
            this.label14.TabIndex = 0;
            this.label14.Text = "mode";
            // 
            // groupBox_basicmove
            // 
            this.groupBox_basicmove.Controls.Add(this.cboBasicmove_mode);
            this.groupBox_basicmove.Controls.Add(this.label33);
            this.groupBox_basicmove.Controls.Add(this.label3);
            this.groupBox_basicmove.Controls.Add(this.txtBasicmove_action);
            this.groupBox_basicmove.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox_basicmove.Location = new System.Drawing.Point(301, 58);
            this.groupBox_basicmove.Name = "groupBox_basicmove";
            this.groupBox_basicmove.Size = new System.Drawing.Size(209, 114);
            this.groupBox_basicmove.TabIndex = 8;
            this.groupBox_basicmove.TabStop = false;
            this.groupBox_basicmove.Text = "Basic-Move";
            // 
            // cboBasicmove_mode
            // 
            this.cboBasicmove_mode.FormattingEnabled = true;
            this.cboBasicmove_mode.Items.AddRange(new object[] {
            "Go",
            "Rotate"});
            this.cboBasicmove_mode.Location = new System.Drawing.Point(53, 33);
            this.cboBasicmove_mode.Name = "cboBasicmove_mode";
            this.cboBasicmove_mode.Size = new System.Drawing.Size(145, 23);
            this.cboBasicmove_mode.TabIndex = 1;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(6, 66);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(40, 15);
            this.label33.TabIndex = 0;
            this.label33.Text = "action";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "mode";
            // 
            // txtBasicmove_action
            // 
            this.txtBasicmove_action.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtBasicmove_action.Location = new System.Drawing.Point(53, 62);
            this.txtBasicmove_action.Name = "txtBasicmove_action";
            this.txtBasicmove_action.Size = new System.Drawing.Size(145, 22);
            this.txtBasicmove_action.TabIndex = 1;
            // 
            // txtGoalpoint_Y
            // 
            this.txtGoalpoint_Y.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtGoalpoint_Y.Location = new System.Drawing.Point(222, 30);
            this.txtGoalpoint_Y.Name = "txtGoalpoint_Y";
            this.txtGoalpoint_Y.Size = new System.Drawing.Size(83, 22);
            this.txtGoalpoint_Y.TabIndex = 9;
            // 
            // txtwp_tolerance
            // 
            this.txtwp_tolerance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtwp_tolerance.Location = new System.Drawing.Point(172, 227);
            this.txtwp_tolerance.Name = "txtwp_tolerance";
            this.txtwp_tolerance.Size = new System.Drawing.Size(83, 25);
            this.txtwp_tolerance.TabIndex = 10;
            // 
            // txtd_drive
            // 
            this.txtd_drive.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtd_drive.Location = new System.Drawing.Point(172, 200);
            this.txtd_drive.Name = "txtd_drive";
            this.txtd_drive.Size = new System.Drawing.Size(83, 25);
            this.txtd_drive.TabIndex = 10;
            // 
            // txtp_drive
            // 
            this.txtp_drive.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtp_drive.Location = new System.Drawing.Point(172, 172);
            this.txtp_drive.Name = "txtp_drive";
            this.txtp_drive.Size = new System.Drawing.Size(83, 25);
            this.txtp_drive.TabIndex = 10;
            // 
            // txtyaw_goal_tolerance
            // 
            this.txtyaw_goal_tolerance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtyaw_goal_tolerance.Location = new System.Drawing.Point(172, 145);
            this.txtyaw_goal_tolerance.Name = "txtyaw_goal_tolerance";
            this.txtyaw_goal_tolerance.Size = new System.Drawing.Size(83, 25);
            this.txtyaw_goal_tolerance.TabIndex = 10;
            // 
            // txtxy_goal_tolerance
            // 
            this.txtxy_goal_tolerance.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtxy_goal_tolerance.Location = new System.Drawing.Point(172, 117);
            this.txtxy_goal_tolerance.Name = "txtxy_goal_tolerance";
            this.txtxy_goal_tolerance.Size = new System.Drawing.Size(83, 25);
            this.txtxy_goal_tolerance.TabIndex = 10;
            // 
            // txtmax_trans_vel
            // 
            this.txtmax_trans_vel.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtmax_trans_vel.Location = new System.Drawing.Point(172, 89);
            this.txtmax_trans_vel.Name = "txtmax_trans_vel";
            this.txtmax_trans_vel.Size = new System.Drawing.Size(83, 25);
            this.txtmax_trans_vel.TabIndex = 10;
            // 
            // txtGoalpoint_X
            // 
            this.txtGoalpoint_X.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtGoalpoint_X.Location = new System.Drawing.Point(63, 30);
            this.txtGoalpoint_X.Name = "txtGoalpoint_X";
            this.txtGoalpoint_X.Size = new System.Drawing.Size(83, 22);
            this.txtGoalpoint_X.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(193, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 16);
            this.label5.TabIndex = 3;
            this.label5.Text = "Y :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(29, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "X :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label40
            // 
            this.label40.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label40.Location = new System.Drawing.Point(152, 36);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(35, 16);
            this.label40.TabIndex = 6;
            this.label40.Text = "(m)";
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label41
            // 
            this.label41.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label41.Location = new System.Drawing.Point(311, 35);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(35, 16);
            this.label41.TabIndex = 4;
            this.label41.Text = "(m)";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label42
            // 
            this.label42.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label42.Location = new System.Drawing.Point(495, 36);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(48, 16);
            this.label42.TabIndex = 5;
            this.label42.Text = "(Rad)";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(381, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "액션 리스트";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(30, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "미션 리스트";
            // 
            // listBox_ActionData
            // 
            this.listBox_ActionData.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listBox_ActionData.FormattingEnabled = true;
            this.listBox_ActionData.ItemHeight = 21;
            this.listBox_ActionData.Location = new System.Drawing.Point(385, 37);
            this.listBox_ActionData.Name = "listBox_ActionData";
            this.listBox_ActionData.Size = new System.Drawing.Size(286, 319);
            this.listBox_ActionData.TabIndex = 1;
            this.listBox_ActionData.SelectedIndexChanged += new System.EventHandler(this.listBox_ActionData_SelectedIndexChanged);
            // 
            // listBox_Mission
            // 
            this.listBox_Mission.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listBox_Mission.FormattingEnabled = true;
            this.listBox_Mission.ItemHeight = 25;
            this.listBox_Mission.Location = new System.Drawing.Point(34, 37);
            this.listBox_Mission.Name = "listBox_Mission";
            this.listBox_Mission.Size = new System.Drawing.Size(326, 329);
            this.listBox_Mission.TabIndex = 1;
            this.listBox_Mission.SelectedIndexChanged += new System.EventHandler(this.listBox_Mission_SelectedIndexChanged);
            // 
            // panel_MissionEdit_right
            // 
            this.panel_MissionEdit_right.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_MissionEdit_right.Controls.Add(this.label11);
            this.panel_MissionEdit_right.Controls.Add(this.chkRobotpos);
            this.panel_MissionEdit_right.Controls.Add(this.label12);
            this.panel_MissionEdit_right.Controls.Add(this.zoomTrackBarControl1);
            this.panel_MissionEdit_right.Controls.Add(this.txtMapsize);
            this.panel_MissionEdit_right.Controls.Add(this.chkGlobalCost);
            this.panel_MissionEdit_right.Controls.Add(this.txtHeight);
            this.panel_MissionEdit_right.Controls.Add(this.txtWidth);
            this.panel_MissionEdit_right.Controls.Add(this.chkDelivery);
            this.panel_MissionEdit_right.Controls.Add(this.chkInitPosSet);
            this.panel_MissionEdit_right.Controls.Add(this.chkAngluar);
            this.panel_MissionEdit_right.Controls.Add(this.txtAngluar);
            this.panel_MissionEdit_right.Controls.Add(this.pb_cost);
            this.panel_MissionEdit_right.Controls.Add(this.btnLeft);
            this.panel_MissionEdit_right.Controls.Add(this.btnRight);
            this.panel_MissionEdit_right.Controls.Add(this.btnDn);
            this.panel_MissionEdit_right.Controls.Add(this.btnUp);
            this.panel_MissionEdit_right.Controls.Add(this.txtTY);
            this.panel_MissionEdit_right.Controls.Add(this.txtTX);
            this.panel_MissionEdit_right.Controls.Add(this.txtYpos);
            this.panel_MissionEdit_right.Controls.Add(this.txtXpos);
            this.panel_MissionEdit_right.Controls.Add(this.txtYcell);
            this.panel_MissionEdit_right.Controls.Add(this.txtXcell);
            this.panel_MissionEdit_right.Controls.Add(this.pb_map);
            this.panel_MissionEdit_right.Controls.Add(this.cboRobotID);
            this.panel_MissionEdit_right.Controls.Add(this.btnMaploading);
            this.panel_MissionEdit_right.Location = new System.Drawing.Point(766, 3);
            this.panel_MissionEdit_right.Name = "panel_MissionEdit_right";
            this.panel_MissionEdit_right.Size = new System.Drawing.Size(951, 835);
            this.panel_MissionEdit_right.TabIndex = 29;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(831, 253);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(11, 12);
            this.label11.TabIndex = 63;
            this.label11.Text = "=";
            // 
            // chkRobotpos
            // 
            this.chkRobotpos.AutoSize = true;
            this.chkRobotpos.Location = new System.Drawing.Point(836, 20);
            this.chkRobotpos.Name = "chkRobotpos";
            this.chkRobotpos.Size = new System.Drawing.Size(96, 16);
            this.chkRobotpos.TabIndex = 8;
            this.chkRobotpos.Text = "로봇위치좌표";
            this.chkRobotpos.UseVisualStyleBackColor = true;
            this.chkRobotpos.CheckedChanged += new System.EventHandler(this.chkRobotpos_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(885, 220);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(13, 12);
            this.label12.TabIndex = 64;
            this.label12.Text = "X";
            // 
            // zoomTrackBarControl1
            // 
            this.zoomTrackBarControl1.EditValue = 50;
            this.zoomTrackBarControl1.Location = new System.Drawing.Point(278, 15);
            this.zoomTrackBarControl1.Name = "zoomTrackBarControl1";
            this.zoomTrackBarControl1.Properties.Maximum = 100;
            this.zoomTrackBarControl1.Size = new System.Drawing.Size(139, 23);
            this.zoomTrackBarControl1.TabIndex = 59;
            this.zoomTrackBarControl1.Value = 50;
            this.zoomTrackBarControl1.EditValueChanged += new System.EventHandler(this.zoomTrackBarControl1_EditValueChanged);
            // 
            // txtMapsize
            // 
            this.txtMapsize.Location = new System.Drawing.Point(848, 244);
            this.txtMapsize.Name = "txtMapsize";
            this.txtMapsize.Size = new System.Drawing.Size(98, 21);
            this.txtMapsize.TabIndex = 60;
            // 
            // chkGlobalCost
            // 
            this.chkGlobalCost.Location = new System.Drawing.Point(438, 19);
            this.chkGlobalCost.Name = "chkGlobalCost";
            this.chkGlobalCost.Properties.Caption = "GlobalCost";
            this.chkGlobalCost.Size = new System.Drawing.Size(86, 19);
            this.chkGlobalCost.TabIndex = 58;
            this.chkGlobalCost.EditValueChanged += new System.EventHandler(this.chkGlobalCost_CheckedChanged);
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(899, 217);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(54, 21);
            this.txtHeight.TabIndex = 61;
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(825, 217);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(54, 21);
            this.txtWidth.TabIndex = 62;
            // 
            // chkDelivery
            // 
            this.chkDelivery.AutoSize = true;
            this.chkDelivery.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkDelivery.Location = new System.Drawing.Point(836, 149);
            this.chkDelivery.Name = "chkDelivery";
            this.chkDelivery.Size = new System.Drawing.Size(102, 29);
            this.chkDelivery.TabIndex = 54;
            this.chkDelivery.Text = "Delivery";
            this.chkDelivery.UseVisualStyleBackColor = true;
            // 
            // chkInitPosSet
            // 
            this.chkInitPosSet.AutoSize = true;
            this.chkInitPosSet.Location = new System.Drawing.Point(836, 127);
            this.chkInitPosSet.Name = "chkInitPosSet";
            this.chkInitPosSet.Size = new System.Drawing.Size(76, 16);
            this.chkInitPosSet.TabIndex = 35;
            this.chkInitPosSet.Text = "초기 위치";
            this.chkInitPosSet.UseVisualStyleBackColor = true;
            // 
            // chkAngluar
            // 
            this.chkAngluar.AutoSize = true;
            this.chkAngluar.Location = new System.Drawing.Point(836, 52);
            this.chkAngluar.Name = "chkAngluar";
            this.chkAngluar.Size = new System.Drawing.Size(76, 16);
            this.chkAngluar.TabIndex = 35;
            this.chkAngluar.Text = "각도 체크";
            this.chkAngluar.UseVisualStyleBackColor = true;
            // 
            // txtAngluar
            // 
            this.txtAngluar.Location = new System.Drawing.Point(836, 81);
            this.txtAngluar.Name = "txtAngluar";
            this.txtAngluar.Size = new System.Drawing.Size(76, 21);
            this.txtAngluar.TabIndex = 34;
            // 
            // pb_cost
            // 
            this.pb_cost.Location = new System.Drawing.Point(786, 692);
            this.pb_cost.Name = "pb_cost";
            this.pb_cost.Size = new System.Drawing.Size(160, 160);
            this.pb_cost.TabIndex = 32;
            this.pb_cost.TabStop = false;
            this.pb_cost.Visible = false;
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(817, 354);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(44, 33);
            this.btnLeft.TabIndex = 31;
            this.btnLeft.Text = "왼쪽";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(905, 354);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(44, 33);
            this.btnRight.TabIndex = 31;
            this.btnRight.Text = "오른쪽";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnDn
            // 
            this.btnDn.Location = new System.Drawing.Point(861, 354);
            this.btnDn.Name = "btnDn";
            this.btnDn.Size = new System.Drawing.Size(44, 33);
            this.btnDn.TabIndex = 31;
            this.btnDn.Text = "아래";
            this.btnDn.UseVisualStyleBackColor = true;
            this.btnDn.Click += new System.EventHandler(this.btnDn_Click);
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(861, 315);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(44, 33);
            this.btnUp.TabIndex = 31;
            this.btnUp.Text = "위";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // txtTY
            // 
            this.txtTY.Location = new System.Drawing.Point(881, 393);
            this.txtTY.Name = "txtTY";
            this.txtTY.Size = new System.Drawing.Size(53, 21);
            this.txtTY.TabIndex = 29;
            // 
            // txtTX
            // 
            this.txtTX.Location = new System.Drawing.Point(822, 393);
            this.txtTX.Name = "txtTX";
            this.txtTX.Size = new System.Drawing.Size(53, 21);
            this.txtTX.TabIndex = 29;
            // 
            // txtYpos
            // 
            this.txtYpos.Location = new System.Drawing.Point(767, 20);
            this.txtYpos.Name = "txtYpos";
            this.txtYpos.Size = new System.Drawing.Size(52, 21);
            this.txtYpos.TabIndex = 29;
            // 
            // txtXpos
            // 
            this.txtXpos.Location = new System.Drawing.Point(700, 20);
            this.txtXpos.Name = "txtXpos";
            this.txtXpos.Size = new System.Drawing.Size(52, 21);
            this.txtXpos.TabIndex = 29;
            // 
            // txtYcell
            // 
            this.txtYcell.Location = new System.Drawing.Point(632, 20);
            this.txtYcell.Name = "txtYcell";
            this.txtYcell.Size = new System.Drawing.Size(52, 21);
            this.txtYcell.TabIndex = 29;
            // 
            // txtXcell
            // 
            this.txtXcell.Location = new System.Drawing.Point(565, 20);
            this.txtXcell.Name = "txtXcell";
            this.txtXcell.Size = new System.Drawing.Size(52, 21);
            this.txtXcell.TabIndex = 29;
            // 
            // pb_map
            // 
            this.pb_map.Location = new System.Drawing.Point(19, 52);
            this.pb_map.Name = "pb_map";
            this.pb_map.Size = new System.Drawing.Size(800, 734);
            this.pb_map.TabIndex = 28;
            this.pb_map.TabStop = false;
            this.pb_map.Paint += new System.Windows.Forms.PaintEventHandler(this.pb_map_Paint);
            this.pb_map.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pb_map_MouseClick);
            this.pb_map.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_map_MouseDown);
            this.pb_map.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pb_map_MouseMove);
            this.pb_map.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_map_MouseUp);
            // 
            // cboRobotID
            // 
            this.cboRobotID.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cboRobotID.FormattingEnabled = true;
            this.cboRobotID.Items.AddRange(new object[] {
            "R_001",
            "R_002",
            "R_003",
            "R_004",
            "R_005",
            "R_006"});
            this.cboRobotID.Location = new System.Drawing.Point(19, 13);
            this.cboRobotID.Name = "cboRobotID";
            this.cboRobotID.Size = new System.Drawing.Size(153, 33);
            this.cboRobotID.TabIndex = 27;
            // 
            // btnMaploading
            // 
            this.btnMaploading.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnMaploading.Location = new System.Drawing.Point(178, 13);
            this.btnMaploading.Name = "btnMaploading";
            this.btnMaploading.Size = new System.Drawing.Size(94, 33);
            this.btnMaploading.TabIndex = 26;
            this.btnMaploading.Text = "맵로딩";
            this.btnMaploading.UseVisualStyleBackColor = true;
            this.btnMaploading.Click += new System.EventHandler(this.btnMaploading_Click);
            // 
            // timer_Robotposchk
            // 
            this.timer_Robotposchk.Tick += new System.EventHandler(this.timer_Robotposchk_Tick);
            // 
            // MissionEdit_Ctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel_MissionEdit_right);
            this.Controls.Add(this.panel_MissionEdit_left);
            this.Name = "MissionEdit_Ctrl";
            this.Size = new System.Drawing.Size(1720, 850);
            this.Load += new System.EventHandler(this.MissionEdit_Ctrl_Load);
            this.panel_MissionEdit_left.ResumeLayout(false);
            this.panel_MissionEdit_left.PerformLayout();
            this.groupBox_ActionOpt.ResumeLayout(false);
            this.groupBox_ActionOpt.PerformLayout();
            this.groupBox_ActionWait.ResumeLayout(false);
            this.groupBox_ActionWait.PerformLayout();
            this.groupBox_lift.ResumeLayout(false);
            this.groupBox_lift.PerformLayout();
            this.groupBox_UR.ResumeLayout(false);
            this.groupBox_UR.PerformLayout();
            this.groupBox_basicmove.ResumeLayout(false);
            this.groupBox_basicmove.PerformLayout();
            this.panel_MissionEdit_right.ResumeLayout(false);
            this.panel_MissionEdit_right.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGlobalCost.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_cost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_map)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_MissionEdit_left;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox_ActionData;
        private System.Windows.Forms.ListBox listBox_Mission;
        private System.Windows.Forms.GroupBox groupBox_ActionOpt;
        private System.Windows.Forms.ComboBox cboavoid;
        private System.Windows.Forms.CheckBox chkavoid;
        private System.Windows.Forms.CheckBox chkwp_tolerance;
        private System.Windows.Forms.CheckBox chkd_drive;
        private System.Windows.Forms.CheckBox chkp_drive;
        private System.Windows.Forms.CheckBox chkyaw_goal_tolerance;
        private System.Windows.Forms.CheckBox chkxy_goal_tolerance;
        private System.Windows.Forms.CheckBox chkmax_trans_vel;
        private System.Windows.Forms.TextBox txtGoalpoint_theta;
        private System.Windows.Forms.GroupBox groupBox_ActionWait;
        private System.Windows.Forms.ComboBox cboXisWait_mode;
        private System.Windows.Forms.TextBox txtXisWaitTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtXisWait_ID;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox_lift;
        private System.Windows.Forms.RadioButton radioButton_backward;
        private System.Windows.Forms.RadioButton radioButton_forward;
        private System.Windows.Forms.ComboBox cboPalletmode;
        private System.Windows.Forms.TextBox txtPalletID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPalletDist;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox_basicmove;
        private System.Windows.Forms.ComboBox cboBasicmove_mode;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBasicmove_action;
        private System.Windows.Forms.TextBox txtGoalpoint_Y;
        private System.Windows.Forms.TextBox txtwp_tolerance;
        private System.Windows.Forms.TextBox txtd_drive;
        private System.Windows.Forms.TextBox txtp_drive;
        private System.Windows.Forms.TextBox txtyaw_goal_tolerance;
        private System.Windows.Forms.TextBox txtxy_goal_tolerance;
        private System.Windows.Forms.TextBox txtmax_trans_vel;
        private System.Windows.Forms.TextBox txtGoalpoint_X;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Panel panel_MissionEdit_right;
        private System.Windows.Forms.CheckBox chkDelivery;
        private System.Windows.Forms.CheckBox chkInitPosSet;
        private System.Windows.Forms.CheckBox chkAngluar;
        private System.Windows.Forms.TextBox txtAngluar;
        private System.Windows.Forms.PictureBox pb_cost;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnDn;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.TextBox txtTY;
        private System.Windows.Forms.TextBox txtTX;
        private System.Windows.Forms.TextBox txtYpos;
        private System.Windows.Forms.TextBox txtXpos;
        private System.Windows.Forms.TextBox txtYcell;
        private System.Windows.Forms.TextBox txtXcell;
        private System.Windows.Forms.PictureBox pb_map;
        private System.Windows.Forms.ComboBox cboRobotID;
        private System.Windows.Forms.Button btnMaploading;
        private DevExpress.XtraEditors.SimpleButton btnMissionInsert1;
        private DevExpress.XtraEditors.SimpleButton btnMissionDelete1;
        private DevExpress.XtraEditors.SimpleButton btnActionDelete1;
        private DevExpress.XtraEditors.SimpleButton btnActionInsert1;
        private DevExpress.XtraEditors.SimpleButton btnActionidxDn1;
        private DevExpress.XtraEditors.SimpleButton btnActionidxUp1;
        private DevExpress.XtraEditors.SimpleButton btnMissionSave1;
        private DevExpress.XtraEditors.ZoomTrackBarControl zoomTrackBarControl1;
        private DevExpress.XtraEditors.CheckEdit chkGlobalCost;
        private System.Windows.Forms.ComboBox cboPassingflag;
        private System.Windows.Forms.CheckBox chkPassingflag;
        private System.Windows.Forms.CheckBox chkRobotpos;
        private System.Windows.Forms.Timer timer_Robotposchk;
        private System.Windows.Forms.Button btnRobotPosRead;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtMapsize;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.GroupBox groupBox_UR;
        private System.Windows.Forms.ComboBox cboURmode;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cboURdata;
        private System.Windows.Forms.Label label13;
    }
}
