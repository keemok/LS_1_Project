namespace Syscon_Solution.LSprogram
{
    partial class missioneditForm
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
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery1 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(missioneditForm));
            this.button1 = new System.Windows.Forms.Button();
            this.pb_map = new System.Windows.Forms.PictureBox();
            this.missionList = new DevExpress.XtraGrid.GridControl();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIndex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col미션아이디 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col미션이름 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.actionList = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.waypointPanel = new System.Windows.Forms.Panel();
            this.passingCheck = new System.Windows.Forms.CheckBox();
            this.separatorControl6 = new DevExpress.XtraEditors.SeparatorControl();
            this.separatorControl5 = new DevExpress.XtraEditors.SeparatorControl();
            this.yawTextbox = new System.Windows.Forms.TextBox();
            this.separatorControl7 = new DevExpress.XtraEditors.SeparatorControl();
            this.separatorControl4 = new DevExpress.XtraEditors.SeparatorControl();
            this.maxveloTextbox = new System.Windows.Forms.TextBox();
            this.xyTextbox = new System.Windows.Forms.TextBox();
            this.separatorControl3 = new DevExpress.XtraEditors.SeparatorControl();
            this.thetaTextbox = new System.Windows.Forms.TextBox();
            this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
            this.yposTextbox = new System.Windows.Forms.TextBox();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.xposTextbox = new System.Windows.Forms.TextBox();
            this.toggleSwitch1 = new DevExpress.XtraEditors.ToggleSwitch();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.selectRobot = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.readTheta = new System.Windows.Forms.TextBox();
            this.readY = new System.Windows.Forms.TextBox();
            this.readX = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.dockingPanel = new System.Windows.Forms.Panel();
            this.dockingScanview = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dockingFrontoffset = new System.Windows.Forms.TextBox();
            this.dockingLocation = new System.Windows.Forms.TextBox();
            this.dockingMarkersize = new System.Windows.Forms.TextBox();
            this.dockingCenteroffset = new System.Windows.Forms.TextBox();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.label22 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.missionAdd = new System.Windows.Forms.Button();
            this.missionDelete = new System.Windows.Forms.Button();
            this.actionAdd = new System.Windows.Forms.Button();
            this.actionDelete = new System.Windows.Forms.Button();
            this.upBtn = new System.Windows.Forms.Button();
            this.downBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pb_map)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.missionList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.waypointPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).BeginInit();
            this.panel4.SuspendLayout();
            this.dockingPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(681, 97);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 57);
            this.button1.TabIndex = 1;
            this.button1.Text = "LOAD BUTTON";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pb_map
            // 
            this.pb_map.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_map.Location = new System.Drawing.Point(796, 14);
            this.pb_map.Name = "pb_map";
            this.pb_map.Size = new System.Drawing.Size(812, 870);
            this.pb_map.TabIndex = 0;
            this.pb_map.TabStop = false;
            this.pb_map.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_map_MouseDown);
            this.pb_map.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pb_map_MouseMove);
            this.pb_map.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_map_MouseUp);
            // 
            // missionList
            // 
            this.missionList.DataMember = "Query";
            this.missionList.DataSource = this.sqlDataSource1;
            this.missionList.Location = new System.Drawing.Point(23, 17);
            this.missionList.MainView = this.gridView1;
            this.missionList.Name = "missionList";
            this.missionList.Size = new System.Drawing.Size(350, 399);
            this.missionList.TabIndex = 2;
            this.missionList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "192.168.20.28_ridis_db_Connection";
            this.sqlDataSource1.Name = "sqlDataSource1";
            customSqlQuery1.Name = "Query";
            customSqlQuery1.Sql = "select idx as \'Index\',mission_id as \'미션 아이디\',mission_name as \'미션 이름\'\r\nfrom missio" +
    "n_t";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            customSqlQuery1});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIndex,
            this.col미션아이디,
            this.col미션이름});
            this.gridView1.GridControl = this.missionList;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridView1_RowCellClick);
            // 
            // colIndex
            // 
            this.colIndex.FieldName = "Index";
            this.colIndex.Name = "colIndex";
            this.colIndex.Visible = true;
            this.colIndex.VisibleIndex = 0;
            // 
            // col미션아이디
            // 
            this.col미션아이디.FieldName = "미션 아이디";
            this.col미션아이디.Name = "col미션아이디";
            this.col미션아이디.Visible = true;
            this.col미션아이디.VisibleIndex = 1;
            // 
            // col미션이름
            // 
            this.col미션이름.FieldName = "미션 이름";
            this.col미션이름.Name = "col미션이름";
            this.col미션이름.Visible = true;
            this.col미션이름.VisibleIndex = 2;
            // 
            // actionList
            // 
            this.actionList.FormattingEnabled = true;
            this.actionList.ItemHeight = 12;
            this.actionList.Location = new System.Drawing.Point(413, 17);
            this.actionList.Name = "actionList";
            this.actionList.Size = new System.Drawing.Size(247, 400);
            this.actionList.TabIndex = 3;
            this.actionList.SelectedIndexChanged += new System.EventHandler(this.actionList_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.waypointPanel);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.dockingPanel);
            this.panel1.Controls.Add(this.simpleButton1);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Location = new System.Drawing.Point(23, 498);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(637, 367);
            this.panel1.TabIndex = 4;
            // 
            // waypointPanel
            // 
            this.waypointPanel.Controls.Add(this.passingCheck);
            this.waypointPanel.Controls.Add(this.separatorControl6);
            this.waypointPanel.Controls.Add(this.separatorControl5);
            this.waypointPanel.Controls.Add(this.yawTextbox);
            this.waypointPanel.Controls.Add(this.separatorControl7);
            this.waypointPanel.Controls.Add(this.separatorControl4);
            this.waypointPanel.Controls.Add(this.maxveloTextbox);
            this.waypointPanel.Controls.Add(this.xyTextbox);
            this.waypointPanel.Controls.Add(this.separatorControl3);
            this.waypointPanel.Controls.Add(this.thetaTextbox);
            this.waypointPanel.Controls.Add(this.separatorControl2);
            this.waypointPanel.Controls.Add(this.yposTextbox);
            this.waypointPanel.Controls.Add(this.separatorControl1);
            this.waypointPanel.Controls.Add(this.xposTextbox);
            this.waypointPanel.Controls.Add(this.toggleSwitch1);
            this.waypointPanel.Controls.Add(this.label6);
            this.waypointPanel.Controls.Add(this.label5);
            this.waypointPanel.Controls.Add(this.label4);
            this.waypointPanel.Controls.Add(this.label3);
            this.waypointPanel.Controls.Add(this.label7);
            this.waypointPanel.Controls.Add(this.label2);
            this.waypointPanel.Controls.Add(this.label1);
            this.waypointPanel.Location = new System.Drawing.Point(11, 17);
            this.waypointPanel.Name = "waypointPanel";
            this.waypointPanel.Size = new System.Drawing.Size(294, 280);
            this.waypointPanel.TabIndex = 51;
            // 
            // passingCheck
            // 
            this.passingCheck.AutoSize = true;
            this.passingCheck.Location = new System.Drawing.Point(143, 200);
            this.passingCheck.Name = "passingCheck";
            this.passingCheck.Size = new System.Drawing.Size(15, 14);
            this.passingCheck.TabIndex = 42;
            this.passingCheck.UseVisualStyleBackColor = true;
            // 
            // separatorControl6
            // 
            this.separatorControl6.BackColor = System.Drawing.Color.Black;
            this.separatorControl6.Location = new System.Drawing.Point(122, 219);
            this.separatorControl6.Name = "separatorControl6";
            this.separatorControl6.Size = new System.Drawing.Size(56, 1);
            this.separatorControl6.TabIndex = 41;
            // 
            // separatorControl5
            // 
            this.separatorControl5.BackColor = System.Drawing.Color.Black;
            this.separatorControl5.Location = new System.Drawing.Point(122, 181);
            this.separatorControl5.Name = "separatorControl5";
            this.separatorControl5.Size = new System.Drawing.Size(56, 1);
            this.separatorControl5.TabIndex = 37;
            // 
            // yawTextbox
            // 
            this.yawTextbox.BackColor = System.Drawing.Color.White;
            this.yawTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.yawTextbox.Font = new System.Drawing.Font("나눔바른고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.yawTextbox.Location = new System.Drawing.Point(125, 162);
            this.yawTextbox.Name = "yawTextbox";
            this.yawTextbox.Size = new System.Drawing.Size(51, 18);
            this.yawTextbox.TabIndex = 36;
            this.yawTextbox.Text = "0";
            this.yawTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // separatorControl7
            // 
            this.separatorControl7.BackColor = System.Drawing.Color.Black;
            this.separatorControl7.Location = new System.Drawing.Point(216, 90);
            this.separatorControl7.Name = "separatorControl7";
            this.separatorControl7.Size = new System.Drawing.Size(56, 1);
            this.separatorControl7.TabIndex = 29;
            // 
            // separatorControl4
            // 
            this.separatorControl4.BackColor = System.Drawing.Color.Black;
            this.separatorControl4.Location = new System.Drawing.Point(122, 143);
            this.separatorControl4.Name = "separatorControl4";
            this.separatorControl4.Size = new System.Drawing.Size(56, 1);
            this.separatorControl4.TabIndex = 35;
            // 
            // maxveloTextbox
            // 
            this.maxveloTextbox.BackColor = System.Drawing.Color.White;
            this.maxveloTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.maxveloTextbox.Font = new System.Drawing.Font("나눔바른고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.maxveloTextbox.Location = new System.Drawing.Point(219, 71);
            this.maxveloTextbox.Name = "maxveloTextbox";
            this.maxveloTextbox.Size = new System.Drawing.Size(51, 18);
            this.maxveloTextbox.TabIndex = 28;
            this.maxveloTextbox.Text = "0";
            this.maxveloTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // xyTextbox
            // 
            this.xyTextbox.BackColor = System.Drawing.Color.White;
            this.xyTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.xyTextbox.Font = new System.Drawing.Font("나눔바른고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.xyTextbox.Location = new System.Drawing.Point(125, 124);
            this.xyTextbox.Name = "xyTextbox";
            this.xyTextbox.Size = new System.Drawing.Size(51, 18);
            this.xyTextbox.TabIndex = 34;
            this.xyTextbox.Text = "0";
            this.xyTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // separatorControl3
            // 
            this.separatorControl3.BackColor = System.Drawing.Color.Black;
            this.separatorControl3.Location = new System.Drawing.Point(122, 105);
            this.separatorControl3.Name = "separatorControl3";
            this.separatorControl3.Size = new System.Drawing.Size(56, 1);
            this.separatorControl3.TabIndex = 33;
            // 
            // thetaTextbox
            // 
            this.thetaTextbox.BackColor = System.Drawing.Color.White;
            this.thetaTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.thetaTextbox.Font = new System.Drawing.Font("나눔바른고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.thetaTextbox.Location = new System.Drawing.Point(125, 86);
            this.thetaTextbox.Name = "thetaTextbox";
            this.thetaTextbox.Size = new System.Drawing.Size(51, 18);
            this.thetaTextbox.TabIndex = 32;
            this.thetaTextbox.Text = "0";
            this.thetaTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // separatorControl2
            // 
            this.separatorControl2.BackColor = System.Drawing.Color.Black;
            this.separatorControl2.Location = new System.Drawing.Point(122, 67);
            this.separatorControl2.Name = "separatorControl2";
            this.separatorControl2.Size = new System.Drawing.Size(56, 1);
            this.separatorControl2.TabIndex = 31;
            // 
            // yposTextbox
            // 
            this.yposTextbox.BackColor = System.Drawing.Color.White;
            this.yposTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.yposTextbox.Font = new System.Drawing.Font("나눔바른고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.yposTextbox.Location = new System.Drawing.Point(125, 48);
            this.yposTextbox.Name = "yposTextbox";
            this.yposTextbox.Size = new System.Drawing.Size(51, 18);
            this.yposTextbox.TabIndex = 30;
            this.yposTextbox.Text = "0";
            this.yposTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // separatorControl1
            // 
            this.separatorControl1.BackColor = System.Drawing.Color.Black;
            this.separatorControl1.Location = new System.Drawing.Point(122, 29);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(56, 1);
            this.separatorControl1.TabIndex = 29;
            // 
            // xposTextbox
            // 
            this.xposTextbox.BackColor = System.Drawing.Color.White;
            this.xposTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.xposTextbox.Font = new System.Drawing.Font("나눔바른고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.xposTextbox.Location = new System.Drawing.Point(125, 10);
            this.xposTextbox.Name = "xposTextbox";
            this.xposTextbox.Size = new System.Drawing.Size(51, 18);
            this.xposTextbox.TabIndex = 28;
            this.xposTextbox.Text = "0";
            this.xposTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.Location = new System.Drawing.Point(79, 236);
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.Properties.OffText = "False";
            this.toggleSwitch1.Properties.OnText = "True";
            this.toggleSwitch1.Size = new System.Drawing.Size(116, 26);
            this.toggleSwitch1.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(4, 200);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 19);
            this.label6.TabIndex = 0;
            this.label6.Text = "Passing Flag";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(0, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 19);
            this.label5.TabIndex = 0;
            this.label5.Text = "yaw 허용 범위 :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(14, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 19);
            this.label4.TabIndex = 0;
            this.label4.Text = "xy 허용 범위 :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(45, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "Theta :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.Location = new System.Drawing.Point(215, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 19);
            this.label7.TabIndex = 0;
            this.label7.Text = "최대속도";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(52, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "Y 좌표 :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(52, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "X 좌표 :";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("나눔바른고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label17.Location = new System.Drawing.Point(386, 46);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 17);
            this.label17.TabIndex = 48;
            this.label17.Text = "Docking";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.selectRobot);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.readTheta);
            this.panel4.Controls.Add(this.readY);
            this.panel4.Controls.Add(this.readX);
            this.panel4.Controls.Add(this.button2);
            this.panel4.Location = new System.Drawing.Point(359, 277);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(257, 75);
            this.panel4.TabIndex = 50;
            // 
            // selectRobot
            // 
            this.selectRobot.FormattingEnabled = true;
            this.selectRobot.Location = new System.Drawing.Point(11, 9);
            this.selectRobot.Name = "selectRobot";
            this.selectRobot.Size = new System.Drawing.Size(121, 20);
            this.selectRobot.TabIndex = 46;
            this.selectRobot.SelectedIndexChanged += new System.EventHandler(this.selectRobot_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(94, 40);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 12);
            this.label10.TabIndex = 45;
            this.label10.Text = "Theta";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(63, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(13, 12);
            this.label9.TabIndex = 45;
            this.label9.Text = "Y";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 12);
            this.label8.TabIndex = 44;
            this.label8.Text = "X";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // readTheta
            // 
            this.readTheta.BackColor = System.Drawing.Color.White;
            this.readTheta.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.readTheta.Location = new System.Drawing.Point(94, 57);
            this.readTheta.Name = "readTheta";
            this.readTheta.ReadOnly = true;
            this.readTheta.Size = new System.Drawing.Size(38, 14);
            this.readTheta.TabIndex = 43;
            this.readTheta.TextChanged += new System.EventHandler(this.readTheta_TextChanged);
            // 
            // readY
            // 
            this.readY.BackColor = System.Drawing.Color.White;
            this.readY.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.readY.Location = new System.Drawing.Point(50, 57);
            this.readY.Name = "readY";
            this.readY.ReadOnly = true;
            this.readY.Size = new System.Drawing.Size(38, 14);
            this.readY.TabIndex = 43;
            this.readY.TextChanged += new System.EventHandler(this.readY_TextChanged);
            // 
            // readX
            // 
            this.readX.BackColor = System.Drawing.Color.White;
            this.readX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.readX.Location = new System.Drawing.Point(6, 57);
            this.readX.Name = "readX";
            this.readX.ReadOnly = true;
            this.readX.Size = new System.Drawing.Size(38, 14);
            this.readX.TabIndex = 43;
            this.readX.TextChanged += new System.EventHandler(this.readX_TextChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(147, 20);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 43);
            this.button2.TabIndex = 42;
            this.button2.Text = "좌표 읽기";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dockingPanel
            // 
            this.dockingPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dockingPanel.Controls.Add(this.dockingScanview);
            this.dockingPanel.Controls.Add(this.label16);
            this.dockingPanel.Controls.Add(this.label15);
            this.dockingPanel.Controls.Add(this.label14);
            this.dockingPanel.Controls.Add(this.label13);
            this.dockingPanel.Controls.Add(this.label12);
            this.dockingPanel.Controls.Add(this.dockingFrontoffset);
            this.dockingPanel.Controls.Add(this.dockingLocation);
            this.dockingPanel.Controls.Add(this.dockingMarkersize);
            this.dockingPanel.Controls.Add(this.dockingCenteroffset);
            this.dockingPanel.Enabled = false;
            this.dockingPanel.Location = new System.Drawing.Point(379, 56);
            this.dockingPanel.Name = "dockingPanel";
            this.dockingPanel.Size = new System.Drawing.Size(210, 170);
            this.dockingPanel.TabIndex = 49;
            // 
            // dockingScanview
            // 
            this.dockingScanview.FormattingEnabled = true;
            this.dockingScanview.Items.AddRange(new object[] {
            "전방",
            "좌측",
            "후방",
            "우측"});
            this.dockingScanview.Location = new System.Drawing.Point(108, 101);
            this.dockingScanview.Name = "dockingScanview";
            this.dockingScanview.Size = new System.Drawing.Size(68, 20);
            this.dockingScanview.TabIndex = 29;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label16.Location = new System.Drawing.Point(14, 134);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(64, 14);
            this.label16.TabIndex = 2;
            this.label16.Text = "전방 offset";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label15.Location = new System.Drawing.Point(14, 104);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(61, 14);
            this.label15.TabIndex = 2;
            this.label15.Text = "Scan view";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label14.Location = new System.Drawing.Point(14, 78);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 14);
            this.label14.TabIndex = 2;
            this.label14.Text = "도킹 방향";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label13.Location = new System.Drawing.Point(14, 55);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(73, 14);
            this.label13.TabIndex = 2;
            this.label13.Text = "Marker size";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label12.Location = new System.Drawing.Point(14, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 14);
            this.label12.TabIndex = 1;
            this.label12.Text = "Center offset";
            // 
            // dockingFrontoffset
            // 
            this.dockingFrontoffset.BackColor = System.Drawing.Color.White;
            this.dockingFrontoffset.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dockingFrontoffset.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dockingFrontoffset.Location = new System.Drawing.Point(105, 134);
            this.dockingFrontoffset.Name = "dockingFrontoffset";
            this.dockingFrontoffset.Size = new System.Drawing.Size(51, 14);
            this.dockingFrontoffset.TabIndex = 28;
            this.dockingFrontoffset.Text = "0";
            this.dockingFrontoffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dockingLocation
            // 
            this.dockingLocation.BackColor = System.Drawing.Color.White;
            this.dockingLocation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dockingLocation.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dockingLocation.Location = new System.Drawing.Point(105, 79);
            this.dockingLocation.Name = "dockingLocation";
            this.dockingLocation.Size = new System.Drawing.Size(51, 14);
            this.dockingLocation.TabIndex = 28;
            this.dockingLocation.Text = "0";
            this.dockingLocation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dockingMarkersize
            // 
            this.dockingMarkersize.BackColor = System.Drawing.Color.White;
            this.dockingMarkersize.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dockingMarkersize.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dockingMarkersize.Location = new System.Drawing.Point(105, 53);
            this.dockingMarkersize.Name = "dockingMarkersize";
            this.dockingMarkersize.Size = new System.Drawing.Size(51, 14);
            this.dockingMarkersize.TabIndex = 28;
            this.dockingMarkersize.Text = "0";
            this.dockingMarkersize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dockingCenteroffset
            // 
            this.dockingCenteroffset.BackColor = System.Drawing.Color.White;
            this.dockingCenteroffset.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dockingCenteroffset.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dockingCenteroffset.Location = new System.Drawing.Point(105, 29);
            this.dockingCenteroffset.Name = "dockingCenteroffset";
            this.dockingCenteroffset.Size = new System.Drawing.Size(51, 14);
            this.dockingCenteroffset.TabIndex = 28;
            this.dockingCenteroffset.Text = "0";
            this.dockingCenteroffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(60, 296);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(119, 56);
            this.simpleButton1.TabIndex = 27;
            this.simpleButton1.Text = "SAVE";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label22.Location = new System.Drawing.Point(3, 254);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(81, 23);
            this.label22.TabIndex = 24;
            this.label22.Text = "AVOID :";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1633, 173);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 5;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(1633, 200);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 5;
            // 
            // missionAdd
            // 
            this.missionAdd.Location = new System.Drawing.Point(23, 422);
            this.missionAdd.Name = "missionAdd";
            this.missionAdd.Size = new System.Drawing.Size(118, 39);
            this.missionAdd.TabIndex = 6;
            this.missionAdd.Text = "MISSION ADD";
            this.missionAdd.UseVisualStyleBackColor = true;
            this.missionAdd.Click += new System.EventHandler(this.missionAdd_Click);
            // 
            // missionDelete
            // 
            this.missionDelete.Location = new System.Drawing.Point(160, 422);
            this.missionDelete.Name = "missionDelete";
            this.missionDelete.Size = new System.Drawing.Size(104, 39);
            this.missionDelete.TabIndex = 6;
            this.missionDelete.Text = "MISSION DEL";
            this.missionDelete.UseVisualStyleBackColor = true;
            this.missionDelete.Click += new System.EventHandler(this.missionDelete_Click);
            // 
            // actionAdd
            // 
            this.actionAdd.Location = new System.Drawing.Point(413, 423);
            this.actionAdd.Name = "actionAdd";
            this.actionAdd.Size = new System.Drawing.Size(91, 38);
            this.actionAdd.TabIndex = 6;
            this.actionAdd.Text = "ACTION ADD";
            this.actionAdd.UseVisualStyleBackColor = true;
            this.actionAdd.Click += new System.EventHandler(this.actionAdd_Click);
            // 
            // actionDelete
            // 
            this.actionDelete.Location = new System.Drawing.Point(544, 423);
            this.actionDelete.Name = "actionDelete";
            this.actionDelete.Size = new System.Drawing.Size(116, 38);
            this.actionDelete.TabIndex = 6;
            this.actionDelete.Text = "ACTION DEL";
            this.actionDelete.UseVisualStyleBackColor = true;
            this.actionDelete.Click += new System.EventHandler(this.actionDelete_Click);
            // 
            // upBtn
            // 
            this.upBtn.Location = new System.Drawing.Point(674, 253);
            this.upBtn.Name = "upBtn";
            this.upBtn.Size = new System.Drawing.Size(37, 23);
            this.upBtn.TabIndex = 7;
            this.upBtn.Text = "up";
            this.upBtn.UseVisualStyleBackColor = true;
            this.upBtn.Click += new System.EventHandler(this.button3_Click);
            // 
            // downBtn
            // 
            this.downBtn.Location = new System.Drawing.Point(674, 282);
            this.downBtn.Name = "downBtn";
            this.downBtn.Size = new System.Drawing.Size(37, 23);
            this.downBtn.TabIndex = 7;
            this.downBtn.Text = "dn";
            this.downBtn.UseVisualStyleBackColor = true;
            this.downBtn.Click += new System.EventHandler(this.downBtn_Click);
            // 
            // missioneditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.downBtn);
            this.Controls.Add(this.upBtn);
            this.Controls.Add(this.actionDelete);
            this.Controls.Add(this.actionAdd);
            this.Controls.Add(this.missionDelete);
            this.Controls.Add(this.missionAdd);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.actionList);
            this.Controls.Add(this.missionList);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pb_map);
            this.Name = "missioneditForm";
            this.Size = new System.Drawing.Size(1800, 900);
            ((System.ComponentModel.ISupportInitialize)(this.pb_map)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.missionList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.waypointPanel.ResumeLayout(false);
            this.waypointPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.dockingPanel.ResumeLayout(false);
            this.dockingPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_map;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraGrid.GridControl missionList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ListBox actionList;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraGrid.Columns.GridColumn colIndex;
        private DevExpress.XtraGrid.Columns.GridColumn col미션아이디;
        private DevExpress.XtraGrid.Columns.GridColumn col미션이름;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label22;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SeparatorControl separatorControl7;
        private System.Windows.Forms.TextBox maxveloTextbox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button missionAdd;
        private System.Windows.Forms.Button missionDelete;
        private System.Windows.Forms.Button actionAdd;
        private System.Windows.Forms.Button actionDelete;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox selectRobot;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox readTheta;
        private System.Windows.Forms.TextBox readY;
        private System.Windows.Forms.TextBox readX;
        private System.Windows.Forms.Panel dockingPanel;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox dockingMarkersize;
        private System.Windows.Forms.TextBox dockingCenteroffset;
        private System.Windows.Forms.ComboBox dockingScanview;
        private System.Windows.Forms.TextBox dockingLocation;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox dockingFrontoffset;
        private System.Windows.Forms.Button upBtn;
        private System.Windows.Forms.Button downBtn;
        private System.Windows.Forms.Panel waypointPanel;
        private DevExpress.XtraEditors.SeparatorControl separatorControl6;
        private DevExpress.XtraEditors.SeparatorControl separatorControl5;
        private System.Windows.Forms.TextBox yawTextbox;
        private DevExpress.XtraEditors.SeparatorControl separatorControl4;
        private System.Windows.Forms.TextBox xyTextbox;
        private DevExpress.XtraEditors.SeparatorControl separatorControl3;
        private System.Windows.Forms.TextBox thetaTextbox;
        private DevExpress.XtraEditors.SeparatorControl separatorControl2;
        private System.Windows.Forms.TextBox yposTextbox;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private System.Windows.Forms.TextBox xposTextbox;
        private DevExpress.XtraEditors.ToggleSwitch toggleSwitch1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox passingCheck;
    }
}
