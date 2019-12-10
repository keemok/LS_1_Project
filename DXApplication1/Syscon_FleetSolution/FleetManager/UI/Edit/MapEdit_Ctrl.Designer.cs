namespace Syscon_Solution.FleetManager.UI.Edit
{
    partial class MapEdit_Ctrl
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
            this.zoomTrackBarControl1 = new DevExpress.XtraEditors.ZoomTrackBarControl();
            this.checkEdit2 = new DevExpress.XtraEditors.CheckEdit();
            this.chkPath = new DevExpress.XtraEditors.CheckEdit();
            this.chkGlobalCost = new DevExpress.XtraEditors.CheckEdit();
            this.chkCostmap = new DevExpress.XtraEditors.CheckEdit();
            this.chkDelivery = new System.Windows.Forms.CheckBox();
            this.chkInitPosSet = new System.Windows.Forms.CheckBox();
            this.chkAngluar = new System.Windows.Forms.CheckBox();
            this.txtAngluar = new System.Windows.Forms.TextBox();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnDn = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.btnZoomIn = new System.Windows.Forms.Button();
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
            this.panel_MissionEdit_right = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGlobalCost.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCostmap.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_map)).BeginInit();
            this.panel_MissionEdit_right.SuspendLayout();
            this.SuspendLayout();
            // 
            // zoomTrackBarControl1
            // 
            this.zoomTrackBarControl1.EditValue = 50;
            this.zoomTrackBarControl1.Location = new System.Drawing.Point(288, 18);
            this.zoomTrackBarControl1.Name = "zoomTrackBarControl1";
            this.zoomTrackBarControl1.Properties.Maximum = 100;
            this.zoomTrackBarControl1.Size = new System.Drawing.Size(230, 16);
            this.zoomTrackBarControl1.TabIndex = 57;
            this.zoomTrackBarControl1.Value = 50;
            // 
            // checkEdit2
            // 
            this.checkEdit2.Location = new System.Drawing.Point(844, 22);
            this.checkEdit2.Name = "checkEdit2";
            this.checkEdit2.Properties.Caption = "Lidar";
            this.checkEdit2.Size = new System.Drawing.Size(65, 19);
            this.checkEdit2.TabIndex = 56;
            this.checkEdit2.Visible = false;
            // 
            // chkPath
            // 
            this.chkPath.Location = new System.Drawing.Point(757, 22);
            this.chkPath.Name = "chkPath";
            this.chkPath.Properties.Caption = "Path";
            this.chkPath.Size = new System.Drawing.Size(71, 19);
            this.chkPath.TabIndex = 56;
            // 
            // chkGlobalCost
            // 
            this.chkGlobalCost.Location = new System.Drawing.Point(559, 22);
            this.chkGlobalCost.Name = "chkGlobalCost";
            this.chkGlobalCost.Properties.Caption = "GlobalCost";
            this.chkGlobalCost.Size = new System.Drawing.Size(86, 19);
            this.chkGlobalCost.TabIndex = 56;
            // 
            // chkCostmap
            // 
            this.chkCostmap.Location = new System.Drawing.Point(665, 22);
            this.chkCostmap.Name = "chkCostmap";
            this.chkCostmap.Properties.Caption = "Costmap";
            this.chkCostmap.Size = new System.Drawing.Size(86, 19);
            this.chkCostmap.TabIndex = 56;
            // 
            // chkDelivery
            // 
            this.chkDelivery.AutoSize = true;
            this.chkDelivery.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkDelivery.Location = new System.Drawing.Point(1495, 103);
            this.chkDelivery.Name = "chkDelivery";
            this.chkDelivery.Size = new System.Drawing.Size(102, 29);
            this.chkDelivery.TabIndex = 54;
            this.chkDelivery.Text = "Delivery";
            this.chkDelivery.UseVisualStyleBackColor = true;
            this.chkDelivery.Visible = false;
            // 
            // chkInitPosSet
            // 
            this.chkInitPosSet.AutoSize = true;
            this.chkInitPosSet.Location = new System.Drawing.Point(1499, 46);
            this.chkInitPosSet.Name = "chkInitPosSet";
            this.chkInitPosSet.Size = new System.Drawing.Size(76, 16);
            this.chkInitPosSet.TabIndex = 35;
            this.chkInitPosSet.Text = "초기 위치";
            this.chkInitPosSet.UseVisualStyleBackColor = true;
            // 
            // chkAngluar
            // 
            this.chkAngluar.AutoSize = true;
            this.chkAngluar.Location = new System.Drawing.Point(1499, 24);
            this.chkAngluar.Name = "chkAngluar";
            this.chkAngluar.Size = new System.Drawing.Size(76, 16);
            this.chkAngluar.TabIndex = 35;
            this.chkAngluar.Text = "각도 체크";
            this.chkAngluar.UseVisualStyleBackColor = true;
            // 
            // txtAngluar
            // 
            this.txtAngluar.Location = new System.Drawing.Point(1585, 22);
            this.txtAngluar.Name = "txtAngluar";
            this.txtAngluar.Size = new System.Drawing.Size(76, 21);
            this.txtAngluar.TabIndex = 34;
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(1510, 389);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(44, 33);
            this.btnLeft.TabIndex = 31;
            this.btnLeft.Text = "왼쪽";
            this.btnLeft.UseVisualStyleBackColor = true;
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(1598, 389);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(44, 33);
            this.btnRight.TabIndex = 31;
            this.btnRight.Text = "오른쪽";
            this.btnRight.UseVisualStyleBackColor = true;
            // 
            // btnDn
            // 
            this.btnDn.Location = new System.Drawing.Point(1554, 389);
            this.btnDn.Name = "btnDn";
            this.btnDn.Size = new System.Drawing.Size(44, 33);
            this.btnDn.TabIndex = 31;
            this.btnDn.Text = "아래";
            this.btnDn.UseVisualStyleBackColor = true;
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(1554, 350);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(44, 33);
            this.btnUp.TabIndex = 31;
            this.btnUp.Text = "위";
            this.btnUp.UseVisualStyleBackColor = true;
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Location = new System.Drawing.Point(1542, 266);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(53, 27);
            this.btnZoomOut.TabIndex = 30;
            this.btnZoomOut.Text = "축소";
            this.btnZoomOut.UseVisualStyleBackColor = true;
            this.btnZoomOut.Visible = false;
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Location = new System.Drawing.Point(1542, 233);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(53, 27);
            this.btnZoomIn.TabIndex = 30;
            this.btnZoomIn.Text = "확대";
            this.btnZoomIn.UseVisualStyleBackColor = true;
            this.btnZoomIn.Visible = false;
            // 
            // txtTY
            // 
            this.txtTY.Location = new System.Drawing.Point(1574, 428);
            this.txtTY.Name = "txtTY";
            this.txtTY.Size = new System.Drawing.Size(53, 21);
            this.txtTY.TabIndex = 29;
            // 
            // txtTX
            // 
            this.txtTX.Location = new System.Drawing.Point(1515, 428);
            this.txtTX.Name = "txtTX";
            this.txtTX.Size = new System.Drawing.Size(53, 21);
            this.txtTX.TabIndex = 29;
            // 
            // txtratio
            // 
            this.txtratio.Location = new System.Drawing.Point(1526, 299);
            this.txtratio.Name = "txtratio";
            this.txtratio.Size = new System.Drawing.Size(100, 21);
            this.txtratio.TabIndex = 29;
            this.txtratio.Visible = false;
            // 
            // txtYpos
            // 
            this.txtYpos.Location = new System.Drawing.Point(1437, 23);
            this.txtYpos.Name = "txtYpos";
            this.txtYpos.Size = new System.Drawing.Size(52, 21);
            this.txtYpos.TabIndex = 29;
            // 
            // txtXpos
            // 
            this.txtXpos.Location = new System.Drawing.Point(1370, 23);
            this.txtXpos.Name = "txtXpos";
            this.txtXpos.Size = new System.Drawing.Size(52, 21);
            this.txtXpos.TabIndex = 29;
            // 
            // txtYcell
            // 
            this.txtYcell.Location = new System.Drawing.Point(1302, 23);
            this.txtYcell.Name = "txtYcell";
            this.txtYcell.Size = new System.Drawing.Size(52, 21);
            this.txtYcell.TabIndex = 29;
            // 
            // txtXcell
            // 
            this.txtXcell.Location = new System.Drawing.Point(1235, 23);
            this.txtXcell.Name = "txtXcell";
            this.txtXcell.Size = new System.Drawing.Size(52, 21);
            this.txtXcell.TabIndex = 29;
            // 
            // pb_map
            // 
            this.pb_map.Location = new System.Drawing.Point(19, 52);
            this.pb_map.Name = "pb_map";
            this.pb_map.Size = new System.Drawing.Size(1470, 751);
            this.pb_map.TabIndex = 28;
            this.pb_map.TabStop = false;
            // 
            // cboRobotID
            // 
            this.cboRobotID.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cboRobotID.FormattingEnabled = true;
            this.cboRobotID.Items.AddRange(new object[] {
            "R_005",
            "R_006",
            "R_007",
            "R_008",
            "R_009"});
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
            // panel_MissionEdit_right
            // 
            this.panel_MissionEdit_right.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_MissionEdit_right.Controls.Add(this.zoomTrackBarControl1);
            this.panel_MissionEdit_right.Controls.Add(this.checkEdit2);
            this.panel_MissionEdit_right.Controls.Add(this.chkPath);
            this.panel_MissionEdit_right.Controls.Add(this.chkGlobalCost);
            this.panel_MissionEdit_right.Controls.Add(this.chkCostmap);
            this.panel_MissionEdit_right.Controls.Add(this.chkDelivery);
            this.panel_MissionEdit_right.Controls.Add(this.chkInitPosSet);
            this.panel_MissionEdit_right.Controls.Add(this.chkAngluar);
            this.panel_MissionEdit_right.Controls.Add(this.txtAngluar);
            this.panel_MissionEdit_right.Controls.Add(this.btnLeft);
            this.panel_MissionEdit_right.Controls.Add(this.btnRight);
            this.panel_MissionEdit_right.Controls.Add(this.btnDn);
            this.panel_MissionEdit_right.Controls.Add(this.btnUp);
            this.panel_MissionEdit_right.Controls.Add(this.btnZoomOut);
            this.panel_MissionEdit_right.Controls.Add(this.btnZoomIn);
            this.panel_MissionEdit_right.Controls.Add(this.txtTY);
            this.panel_MissionEdit_right.Controls.Add(this.txtTX);
            this.panel_MissionEdit_right.Controls.Add(this.txtratio);
            this.panel_MissionEdit_right.Controls.Add(this.txtYpos);
            this.panel_MissionEdit_right.Controls.Add(this.txtXpos);
            this.panel_MissionEdit_right.Controls.Add(this.txtYcell);
            this.panel_MissionEdit_right.Controls.Add(this.txtXcell);
            this.panel_MissionEdit_right.Controls.Add(this.pb_map);
            this.panel_MissionEdit_right.Controls.Add(this.cboRobotID);
            this.panel_MissionEdit_right.Controls.Add(this.btnMaploading);
            this.panel_MissionEdit_right.Location = new System.Drawing.Point(19, 13);
            this.panel_MissionEdit_right.Name = "panel_MissionEdit_right";
            this.panel_MissionEdit_right.Size = new System.Drawing.Size(1676, 823);
            this.panel_MissionEdit_right.TabIndex = 31;
            // 
            // MapEdit_Ctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel_MissionEdit_right);
            this.Name = "MapEdit_Ctrl";
            this.Size = new System.Drawing.Size(1720, 850);
            this.Load += new System.EventHandler(this.MapEdit_Ctrl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGlobalCost.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCostmap.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_map)).EndInit();
            this.panel_MissionEdit_right.ResumeLayout(false);
            this.panel_MissionEdit_right.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ZoomTrackBarControl zoomTrackBarControl1;
        private DevExpress.XtraEditors.CheckEdit checkEdit2;
        private DevExpress.XtraEditors.CheckEdit chkPath;
        private DevExpress.XtraEditors.CheckEdit chkGlobalCost;
        private DevExpress.XtraEditors.CheckEdit chkCostmap;
        private System.Windows.Forms.CheckBox chkDelivery;
        private System.Windows.Forms.CheckBox chkInitPosSet;
        private System.Windows.Forms.CheckBox chkAngluar;
        private System.Windows.Forms.TextBox txtAngluar;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnDn;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Button btnZoomIn;
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
        private System.Windows.Forms.Panel panel_MissionEdit_right;
    }
}
