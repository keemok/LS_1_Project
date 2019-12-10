namespace Syscon_Solution.FleetManager.UI.Edit
{
    partial class MissionInsertDlg
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
            this.btnInsert = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMissionName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMissionID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMissionLevel = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cboXis_name = new System.Windows.Forms.ComboBox();
            this.cboTriggerkind = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInsert
            // 
            this.btnInsert.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnInsert.Location = new System.Drawing.Point(119, 189);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(97, 39);
            this.btnInsert.TabIndex = 0;
            this.btnInsert.Text = "추가";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "미션이름";
            // 
            // txtMissionName
            // 
            this.txtMissionName.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtMissionName.Location = new System.Drawing.Point(12, 57);
            this.txtMissionName.Name = "txtMissionName";
            this.txtMissionName.Size = new System.Drawing.Size(100, 23);
            this.txtMissionName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(127, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "미션ID";
            // 
            // txtMissionID
            // 
            this.txtMissionID.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtMissionID.Location = new System.Drawing.Point(127, 57);
            this.txtMissionID.Name = "txtMissionID";
            this.txtMissionID.Size = new System.Drawing.Size(100, 23);
            this.txtMissionID.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(243, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "미션레벨";
            // 
            // txtMissionLevel
            // 
            this.txtMissionLevel.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtMissionLevel.Location = new System.Drawing.Point(243, 57);
            this.txtMissionLevel.Name = "txtMissionLevel";
            this.txtMissionLevel.Size = new System.Drawing.Size(100, 23);
            this.txtMissionLevel.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button2.Location = new System.Drawing.Point(243, 189);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 39);
            this.button2.TabIndex = 0;
            this.button2.Text = "취소";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(5, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "Trigger 종류";
            // 
            // cboXis_name
            // 
            this.cboXis_name.FormattingEnabled = true;
            this.cboXis_name.Items.AddRange(new object[] {
            "Go",
            "Rotate"});
            this.cboXis_name.Location = new System.Drawing.Point(8, 36);
            this.cboXis_name.Name = "cboXis_name";
            this.cboXis_name.Size = new System.Drawing.Size(145, 20);
            this.cboXis_name.TabIndex = 3;
            this.cboXis_name.SelectedIndexChanged += new System.EventHandler(this.cboXis_name_SelectedIndexChanged);
            // 
            // cboTriggerkind
            // 
            this.cboTriggerkind.FormattingEnabled = true;
            this.cboTriggerkind.Items.AddRange(new object[] {
            "Go",
            "Rotate"});
            this.cboTriggerkind.Location = new System.Drawing.Point(169, 36);
            this.cboTriggerkind.Name = "cboTriggerkind";
            this.cboTriggerkind.Size = new System.Drawing.Size(145, 20);
            this.cboTriggerkind.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboTriggerkind);
            this.groupBox1.Controls.Add(this.cboXis_name);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(11, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(331, 72);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // MissionInsertDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 240);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtMissionLevel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMissionID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMissionName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnInsert);
            this.Name = "MissionInsertDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "미션추가";
            this.Load += new System.EventHandler(this.MissionInsertDlg_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMissionName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMissionID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMissionLevel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboXis_name;
        private System.Windows.Forms.ComboBox cboTriggerkind;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}