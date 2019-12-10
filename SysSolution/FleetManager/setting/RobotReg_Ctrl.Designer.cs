namespace SysSolution.FleetManager.setting
{
    partial class RobotReg_Ctrl
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
            this.dataGridView_robotreg = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRobotReg = new System.Windows.Forms.Button();
            this.btnRobotDelete = new System.Windows.Forms.Button();
            this.btnRobotUpdate = new System.Windows.Forms.Button();
            this.groupBox_reg = new System.Windows.Forms.GroupBox();
            this.cboRobotGroup = new System.Windows.Forms.ComboBox();
            this.txtIP4 = new System.Windows.Forms.TextBox();
            this.txtIP3 = new System.Windows.Forms.TextBox();
            this.txtIP2 = new System.Windows.Forms.TextBox();
            this.txtIP1 = new System.Windows.Forms.TextBox();
            this.txtRobotName = new System.Windows.Forms.TextBox();
            this.txtRobotID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboMap = new System.Windows.Forms.ComboBox();
            this.robot_jobingstatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RobotID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RobotName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RobotGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.map = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_robotreg)).BeginInit();
            this.groupBox_reg.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView_robotreg
            // 
            this.dataGridView_robotreg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_robotreg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.robot_jobingstatus,
            this.RobotID,
            this.RobotName,
            this.IP,
            this.RobotGroup,
            this.map});
            this.dataGridView_robotreg.Location = new System.Drawing.Point(19, 38);
            this.dataGridView_robotreg.Name = "dataGridView_robotreg";
            this.dataGridView_robotreg.RowTemplate.Height = 23;
            this.dataGridView_robotreg.Size = new System.Drawing.Size(547, 265);
            this.dataGridView_robotreg.TabIndex = 0;
            this.dataGridView_robotreg.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_robotreg_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(15, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "등록된 로봇 리스트";
            // 
            // btnRobotReg
            // 
            this.btnRobotReg.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRobotReg.Location = new System.Drawing.Point(598, 270);
            this.btnRobotReg.Name = "btnRobotReg";
            this.btnRobotReg.Size = new System.Drawing.Size(88, 33);
            this.btnRobotReg.TabIndex = 5;
            this.btnRobotReg.Text = "추가";
            this.btnRobotReg.UseVisualStyleBackColor = true;
            this.btnRobotReg.Click += new System.EventHandler(this.btnRobotReg_Click);
            // 
            // btnRobotDelete
            // 
            this.btnRobotDelete.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRobotDelete.Location = new System.Drawing.Point(800, 270);
            this.btnRobotDelete.Name = "btnRobotDelete";
            this.btnRobotDelete.Size = new System.Drawing.Size(88, 33);
            this.btnRobotDelete.TabIndex = 5;
            this.btnRobotDelete.Text = "삭제";
            this.btnRobotDelete.UseVisualStyleBackColor = true;
            this.btnRobotDelete.Click += new System.EventHandler(this.btnRobotDelete_Click);
            // 
            // btnRobotUpdate
            // 
            this.btnRobotUpdate.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRobotUpdate.Location = new System.Drawing.Point(699, 270);
            this.btnRobotUpdate.Name = "btnRobotUpdate";
            this.btnRobotUpdate.Size = new System.Drawing.Size(88, 33);
            this.btnRobotUpdate.TabIndex = 5;
            this.btnRobotUpdate.Text = "변경";
            this.btnRobotUpdate.UseVisualStyleBackColor = true;
            this.btnRobotUpdate.Click += new System.EventHandler(this.btnRobotUpdate_Click);
            // 
            // groupBox_reg
            // 
            this.groupBox_reg.Controls.Add(this.cboMap);
            this.groupBox_reg.Controls.Add(this.cboRobotGroup);
            this.groupBox_reg.Controls.Add(this.txtIP4);
            this.groupBox_reg.Controls.Add(this.txtIP3);
            this.groupBox_reg.Controls.Add(this.txtIP2);
            this.groupBox_reg.Controls.Add(this.txtIP1);
            this.groupBox_reg.Controls.Add(this.txtRobotName);
            this.groupBox_reg.Controls.Add(this.label6);
            this.groupBox_reg.Controls.Add(this.txtRobotID);
            this.groupBox_reg.Controls.Add(this.label5);
            this.groupBox_reg.Controls.Add(this.label4);
            this.groupBox_reg.Controls.Add(this.label3);
            this.groupBox_reg.Controls.Add(this.label2);
            this.groupBox_reg.Location = new System.Drawing.Point(592, 38);
            this.groupBox_reg.Name = "groupBox_reg";
            this.groupBox_reg.Size = new System.Drawing.Size(291, 204);
            this.groupBox_reg.TabIndex = 6;
            this.groupBox_reg.TabStop = false;
            // 
            // cboRobotGroup
            // 
            this.cboRobotGroup.FormattingEnabled = true;
            this.cboRobotGroup.Items.AddRange(new object[] {
            "pallet",
            "conveyor",
            "arm",
            "delivery"});
            this.cboRobotGroup.Location = new System.Drawing.Point(77, 115);
            this.cboRobotGroup.Name = "cboRobotGroup";
            this.cboRobotGroup.Size = new System.Drawing.Size(118, 20);
            this.cboRobotGroup.TabIndex = 2;
            // 
            // txtIP4
            // 
            this.txtIP4.Location = new System.Drawing.Point(227, 85);
            this.txtIP4.Name = "txtIP4";
            this.txtIP4.Size = new System.Drawing.Size(44, 21);
            this.txtIP4.TabIndex = 1;
            // 
            // txtIP3
            // 
            this.txtIP3.Location = new System.Drawing.Point(177, 85);
            this.txtIP3.Name = "txtIP3";
            this.txtIP3.Size = new System.Drawing.Size(44, 21);
            this.txtIP3.TabIndex = 1;
            // 
            // txtIP2
            // 
            this.txtIP2.Location = new System.Drawing.Point(127, 85);
            this.txtIP2.Name = "txtIP2";
            this.txtIP2.Size = new System.Drawing.Size(44, 21);
            this.txtIP2.TabIndex = 1;
            // 
            // txtIP1
            // 
            this.txtIP1.Location = new System.Drawing.Point(77, 85);
            this.txtIP1.Name = "txtIP1";
            this.txtIP1.Size = new System.Drawing.Size(44, 21);
            this.txtIP1.TabIndex = 1;
            // 
            // txtRobotName
            // 
            this.txtRobotName.Location = new System.Drawing.Point(77, 55);
            this.txtRobotName.Name = "txtRobotName";
            this.txtRobotName.Size = new System.Drawing.Size(85, 21);
            this.txtRobotName.TabIndex = 1;
            // 
            // txtRobotID
            // 
            this.txtRobotID.Location = new System.Drawing.Point(77, 25);
            this.txtRobotID.Name = "txtRobotID";
            this.txtRobotID.Size = new System.Drawing.Size(85, 21);
            this.txtRobotID.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(8, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Group :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(35, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "IP :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(11, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Name :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(33, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "ID :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(19, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Map :";
            // 
            // cboMap
            // 
            this.cboMap.FormattingEnabled = true;
            this.cboMap.Location = new System.Drawing.Point(77, 141);
            this.cboMap.Name = "cboMap";
            this.cboMap.Size = new System.Drawing.Size(118, 20);
            this.cboMap.TabIndex = 2;
            // 
            // robot_jobingstatus
            // 
            this.robot_jobingstatus.HeaderText = "로봇상태";
            this.robot_jobingstatus.Name = "robot_jobingstatus";
            // 
            // RobotID
            // 
            this.RobotID.HeaderText = "ID";
            this.RobotID.Name = "RobotID";
            // 
            // RobotName
            // 
            this.RobotName.HeaderText = "로봇 이름";
            this.RobotName.Name = "RobotName";
            // 
            // IP
            // 
            this.IP.HeaderText = "IP";
            this.IP.Name = "IP";
            // 
            // RobotGroup
            // 
            this.RobotGroup.HeaderText = "Group";
            this.RobotGroup.Name = "RobotGroup";
            // 
            // map
            // 
            this.map.HeaderText = "맵";
            this.map.Name = "map";
            // 
            // RobotReg_Ctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox_reg);
            this.Controls.Add(this.btnRobotUpdate);
            this.Controls.Add(this.btnRobotDelete);
            this.Controls.Add(this.btnRobotReg);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView_robotreg);
            this.Name = "RobotReg_Ctrl";
            this.Size = new System.Drawing.Size(917, 600);
            this.Load += new System.EventHandler(this.RobotReg_Ctrl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_robotreg)).EndInit();
            this.groupBox_reg.ResumeLayout(false);
            this.groupBox_reg.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_robotreg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRobotReg;
        private System.Windows.Forms.Button btnRobotDelete;
        private System.Windows.Forms.Button btnRobotUpdate;
        private System.Windows.Forms.GroupBox groupBox_reg;
        private System.Windows.Forms.ComboBox cboRobotGroup;
        private System.Windows.Forms.TextBox txtIP4;
        private System.Windows.Forms.TextBox txtIP3;
        private System.Windows.Forms.TextBox txtIP2;
        private System.Windows.Forms.TextBox txtIP1;
        private System.Windows.Forms.TextBox txtRobotName;
        private System.Windows.Forms.TextBox txtRobotID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn robot_jobingstatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn RobotID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RobotName;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP;
        private System.Windows.Forms.DataGridViewTextBoxColumn RobotGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn map;
        private System.Windows.Forms.ComboBox cboMap;
        private System.Windows.Forms.Label label6;
    }
}
