namespace SysSolution.FleetManager.Map
{
    partial class MapCtrl
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
            this.btnCostmap = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnDn = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtTY = new System.Windows.Forms.TextBox();
            this.txtTX = new System.Windows.Forms.TextBox();
            this.txtratio = new System.Windows.Forms.TextBox();
            this.txtYpos = new System.Windows.Forms.TextBox();
            this.txtXpos = new System.Windows.Forms.TextBox();
            this.txtYcell = new System.Windows.Forms.TextBox();
            this.txtXcell = new System.Windows.Forms.TextBox();
            this.pb_map = new System.Windows.Forms.PictureBox();
            this.cboRobotID = new System.Windows.Forms.ComboBox();
            this.btnMaploading = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.chkAngluar = new System.Windows.Forms.CheckBox();
            this.txtAngluar = new System.Windows.Forms.TextBox();
            this.chkInitPosSet = new System.Windows.Forms.CheckBox();
            this.chkDelivery = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_map)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCostmap
            // 
            this.btnCostmap.Font = new System.Drawing.Font("맑은 고딕", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCostmap.Location = new System.Drawing.Point(373, 16);
            this.btnCostmap.Name = "btnCostmap";
            this.btnCostmap.Size = new System.Drawing.Size(145, 61);
            this.btnCostmap.TabIndex = 50;
            this.btnCostmap.Text = "costmap";
            this.btnCostmap.UseVisualStyleBackColor = true;
            this.btnCostmap.Click += new System.EventHandler(this.btnCostmap_Click_1);
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(1468, 304);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(44, 33);
            this.btnLeft.TabIndex = 49;
            this.btnLeft.Text = "왼쪽";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(1556, 304);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(44, 33);
            this.btnRight.TabIndex = 48;
            this.btnRight.Text = "오른쪽";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnDn
            // 
            this.btnDn.Location = new System.Drawing.Point(1512, 304);
            this.btnDn.Name = "btnDn";
            this.btnDn.Size = new System.Drawing.Size(44, 33);
            this.btnDn.TabIndex = 47;
            this.btnDn.Text = "아래";
            this.btnDn.UseVisualStyleBackColor = true;
            this.btnDn.Click += new System.EventHandler(this.btnDn_Click);
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(1512, 265);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(44, 33);
            this.btnUp.TabIndex = 46;
            this.btnUp.Text = "위";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1500, 181);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 40);
            this.button2.TabIndex = 45;
            this.button2.Text = "축소";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1500, 124);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 40);
            this.button1.TabIndex = 44;
            this.button1.Text = "확대";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtTY
            // 
            this.txtTY.Location = new System.Drawing.Point(1532, 343);
            this.txtTY.Name = "txtTY";
            this.txtTY.Size = new System.Drawing.Size(53, 21);
            this.txtTY.TabIndex = 43;
            // 
            // txtTX
            // 
            this.txtTX.Location = new System.Drawing.Point(1473, 343);
            this.txtTX.Name = "txtTX";
            this.txtTX.Size = new System.Drawing.Size(53, 21);
            this.txtTX.TabIndex = 42;
            // 
            // txtratio
            // 
            this.txtratio.Location = new System.Drawing.Point(1485, 227);
            this.txtratio.Name = "txtratio";
            this.txtratio.Size = new System.Drawing.Size(100, 21);
            this.txtratio.TabIndex = 41;
            // 
            // txtYpos
            // 
            this.txtYpos.Location = new System.Drawing.Point(892, 37);
            this.txtYpos.Name = "txtYpos";
            this.txtYpos.Size = new System.Drawing.Size(44, 21);
            this.txtYpos.TabIndex = 40;
            // 
            // txtXpos
            // 
            this.txtXpos.Location = new System.Drawing.Point(842, 37);
            this.txtXpos.Name = "txtXpos";
            this.txtXpos.Size = new System.Drawing.Size(44, 21);
            this.txtXpos.TabIndex = 39;
            // 
            // txtYcell
            // 
            this.txtYcell.Location = new System.Drawing.Point(792, 37);
            this.txtYcell.Name = "txtYcell";
            this.txtYcell.Size = new System.Drawing.Size(44, 21);
            this.txtYcell.TabIndex = 38;
            // 
            // txtXcell
            // 
            this.txtXcell.Location = new System.Drawing.Point(742, 37);
            this.txtXcell.Name = "txtXcell";
            this.txtXcell.Size = new System.Drawing.Size(44, 21);
            this.txtXcell.TabIndex = 37;
            // 
            // pb_map
            // 
            this.pb_map.Location = new System.Drawing.Point(12, 89);
            this.pb_map.Name = "pb_map";
            this.pb_map.Size = new System.Drawing.Size(1440, 763);
            this.pb_map.TabIndex = 36;
            this.pb_map.TabStop = false;
            this.pb_map.Paint += new System.Windows.Forms.PaintEventHandler(this.pb_map_Paint);
            this.pb_map.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pb_map_MouseClick);
            this.pb_map.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_map_MouseDown);
            this.pb_map.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pb_map_MouseMove);
            this.pb_map.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_map_MouseUp);
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
            this.cboRobotID.Location = new System.Drawing.Point(12, 13);
            this.cboRobotID.Name = "cboRobotID";
            this.cboRobotID.Size = new System.Drawing.Size(171, 58);
            this.cboRobotID.TabIndex = 35;
            // 
            // btnMaploading
            // 
            this.btnMaploading.Font = new System.Drawing.Font("맑은 고딕", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnMaploading.Location = new System.Drawing.Point(201, 15);
            this.btnMaploading.Name = "btnMaploading";
            this.btnMaploading.Size = new System.Drawing.Size(145, 61);
            this.btnMaploading.TabIndex = 34;
            this.btnMaploading.Text = "맵로딩";
            this.btnMaploading.UseVisualStyleBackColor = true;
            this.btnMaploading.Click += new System.EventHandler(this.btnMaploading_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // chkAngluar
            // 
            this.chkAngluar.AutoSize = true;
            this.chkAngluar.Location = new System.Drawing.Point(1484, 29);
            this.chkAngluar.Name = "chkAngluar";
            this.chkAngluar.Size = new System.Drawing.Size(76, 16);
            this.chkAngluar.TabIndex = 52;
            this.chkAngluar.Text = "각도 체크";
            this.chkAngluar.UseVisualStyleBackColor = true;
            // 
            // txtAngluar
            // 
            this.txtAngluar.Location = new System.Drawing.Point(1484, 52);
            this.txtAngluar.Name = "txtAngluar";
            this.txtAngluar.Size = new System.Drawing.Size(65, 21);
            this.txtAngluar.TabIndex = 51;
            // 
            // chkInitPosSet
            // 
            this.chkInitPosSet.AutoSize = true;
            this.chkInitPosSet.Location = new System.Drawing.Point(1484, 89);
            this.chkInitPosSet.Name = "chkInitPosSet";
            this.chkInitPosSet.Size = new System.Drawing.Size(76, 16);
            this.chkInitPosSet.TabIndex = 53;
            this.chkInitPosSet.Text = "초기 위치";
            this.chkInitPosSet.UseVisualStyleBackColor = true;
            // 
            // chkDelivery
            // 
            this.chkDelivery.AutoSize = true;
            this.chkDelivery.Font = new System.Drawing.Font("맑은 고딕", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkDelivery.Location = new System.Drawing.Point(552, 15);
            this.chkDelivery.Name = "chkDelivery";
            this.chkDelivery.Size = new System.Drawing.Size(184, 54);
            this.chkDelivery.TabIndex = 53;
            this.chkDelivery.Text = "Delivery";
            this.chkDelivery.UseVisualStyleBackColor = true;
            this.chkDelivery.CheckedChanged += new System.EventHandler(this.chkDelivery_CheckedChanged);
            // 
            // MapCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.chkDelivery);
            this.Controls.Add(this.chkInitPosSet);
            this.Controls.Add(this.chkAngluar);
            this.Controls.Add(this.txtAngluar);
            this.Controls.Add(this.btnCostmap);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnDn);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtTY);
            this.Controls.Add(this.txtTX);
            this.Controls.Add(this.txtratio);
            this.Controls.Add(this.txtYpos);
            this.Controls.Add(this.txtXpos);
            this.Controls.Add(this.txtYcell);
            this.Controls.Add(this.txtXcell);
            this.Controls.Add(this.pb_map);
            this.Controls.Add(this.cboRobotID);
            this.Controls.Add(this.btnMaploading);
            this.Name = "MapCtrl";
            this.Size = new System.Drawing.Size(1611, 920);
            this.Load += new System.EventHandler(this.MapCtrl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb_map)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCostmap;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnDn;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtTY;
        private System.Windows.Forms.TextBox txtTX;
        private System.Windows.Forms.TextBox txtratio;
        private System.Windows.Forms.TextBox txtYpos;
        private System.Windows.Forms.TextBox txtXpos;
        private System.Windows.Forms.TextBox txtYcell;
        private System.Windows.Forms.TextBox txtXcell;
        private System.Windows.Forms.PictureBox pb_map;
        private System.Windows.Forms.ComboBox cboRobotID;
        private System.Windows.Forms.Button btnMaploading;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox chkAngluar;
        private System.Windows.Forms.TextBox txtAngluar;
        private System.Windows.Forms.CheckBox chkInitPosSet;
        private System.Windows.Forms.CheckBox chkDelivery;
    }
}
