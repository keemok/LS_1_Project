namespace SysSolution.FleetManager
{
    partial class FleetManager_MainForm
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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtAddr = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMissionEdit = new System.Windows.Forms.Label();
            this.lblSetting = new System.Windows.Forms.Label();
            this.lblJobOrder = new System.Windows.Forms.Label();
            this.lblMonitoring = new System.Windows.Forms.Label();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnConnect);
            this.groupBox5.Controls.Add(this.txtAddr);
            this.groupBox5.Controls.Add(this.label24);
            this.groupBox5.Location = new System.Drawing.Point(12, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(537, 66);
            this.groupBox5.TabIndex = 23;
            this.groupBox5.TabStop = false;
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnConnect.Location = new System.Drawing.Point(367, 21);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(145, 32);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtAddr
            // 
            this.txtAddr.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtAddr.Location = new System.Drawing.Point(93, 21);
            this.txtAddr.Name = "txtAddr";
            this.txtAddr.Size = new System.Drawing.Size(268, 32);
            this.txtAddr.TabIndex = 2;
            this.txtAddr.Text = "ws://192.168.20.28:9090";
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label24.ForeColor = System.Drawing.SystemColors.Control;
            this.label24.Location = new System.Drawing.Point(6, 27);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(85, 20);
            this.label24.TabIndex = 5;
            this.label24.Text = "ROS서버:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(126, 93);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1760, 940);
            this.panel1.TabIndex = 24;
            // 
            // lblMissionEdit
            // 
            this.lblMissionEdit.BackColor = System.Drawing.Color.Transparent;
            this.lblMissionEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMissionEdit.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMissionEdit.ForeColor = System.Drawing.Color.Khaki;
            this.lblMissionEdit.Location = new System.Drawing.Point(17, 158);
            this.lblMissionEdit.Name = "lblMissionEdit";
            this.lblMissionEdit.Size = new System.Drawing.Size(98, 32);
            this.lblMissionEdit.TabIndex = 26;
            this.lblMissionEdit.Text = "미션편집";
            this.lblMissionEdit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMissionEdit.Click += new System.EventHandler(this.lblMissionEdit_Click);
            this.lblMissionEdit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);
            this.lblMissionEdit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MouseUp);
            // 
            // lblSetting
            // 
            this.lblSetting.BackColor = System.Drawing.Color.Transparent;
            this.lblSetting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSetting.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblSetting.ForeColor = System.Drawing.Color.Khaki;
            this.lblSetting.Location = new System.Drawing.Point(17, 215);
            this.lblSetting.Name = "lblSetting";
            this.lblSetting.Size = new System.Drawing.Size(98, 32);
            this.lblSetting.TabIndex = 26;
            this.lblSetting.Text = "셋팅";
            this.lblSetting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSetting.Click += new System.EventHandler(this.lblSetting_Click);
            this.lblSetting.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);
            this.lblSetting.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MouseUp);
            // 
            // lblJobOrder
            // 
            this.lblJobOrder.BackColor = System.Drawing.Color.Transparent;
            this.lblJobOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblJobOrder.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblJobOrder.ForeColor = System.Drawing.Color.Khaki;
            this.lblJobOrder.Location = new System.Drawing.Point(17, 267);
            this.lblJobOrder.Name = "lblJobOrder";
            this.lblJobOrder.Size = new System.Drawing.Size(98, 32);
            this.lblJobOrder.TabIndex = 26;
            this.lblJobOrder.Text = "작업 지시";
            this.lblJobOrder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblJobOrder.Click += new System.EventHandler(this.lblJobOrder_Click);
            this.lblJobOrder.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);
            this.lblJobOrder.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MouseUp);
            // 
            // lblMonitoring
            // 
            this.lblMonitoring.BackColor = System.Drawing.Color.Transparent;
            this.lblMonitoring.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMonitoring.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMonitoring.ForeColor = System.Drawing.Color.Khaki;
            this.lblMonitoring.Location = new System.Drawing.Point(17, 320);
            this.lblMonitoring.Name = "lblMonitoring";
            this.lblMonitoring.Size = new System.Drawing.Size(98, 32);
            this.lblMonitoring.TabIndex = 26;
            this.lblMonitoring.Text = "모니터링";
            this.lblMonitoring.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMonitoring.Click += new System.EventHandler(this.lblMonitoring_Click);
            this.lblMonitoring.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);
            this.lblMonitoring.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MouseUp);
            // 
            // FleetManager_MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.lblMonitoring);
            this.Controls.Add(this.lblJobOrder);
            this.Controls.Add(this.lblSetting);
            this.Controls.Add(this.lblMissionEdit);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox5);
            this.Name = "FleetManager_MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FleetManager_MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FleetManager_MainForm_FormClosing);
            this.Load += new System.EventHandler(this.FleetManager_MainForm_Load);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtAddr;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMissionEdit;
        private System.Windows.Forms.Label lblSetting;
        private System.Windows.Forms.Label lblJobOrder;
        private System.Windows.Forms.Label lblMonitoring;
    }
}