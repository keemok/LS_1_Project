namespace SysSolution.Delivery
{
    partial class DeliveryForm
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
            this.btnLoadingPosMove = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn4PosMove = new System.Windows.Forms.Button();
            this.btn3PosMove = new System.Windows.Forms.Button();
            this.btn2PosMove = new System.Windows.Forms.Button();
            this.btn1PosMove = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnWaitPosMove = new System.Windows.Forms.Button();
            this.btnJobStop = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnPauseRestart = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboRobotID = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnConnect);
            this.groupBox5.Controls.Add(this.txtAddr);
            this.groupBox5.Controls.Add(this.label24);
            this.groupBox5.Location = new System.Drawing.Point(22, 23);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(560, 92);
            this.groupBox5.TabIndex = 24;
            this.groupBox5.TabStop = false;
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnConnect.Location = new System.Drawing.Point(367, 21);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(172, 54);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtAddr
            // 
            this.txtAddr.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtAddr.Location = new System.Drawing.Point(93, 29);
            this.txtAddr.Name = "txtAddr";
            this.txtAddr.Size = new System.Drawing.Size(268, 32);
            this.txtAddr.TabIndex = 2;
            this.txtAddr.Text = "ws://192.168.0.28:9090";
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(6, 34);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(85, 20);
            this.label24.TabIndex = 5;
            this.label24.Text = "ROS서버:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLoadingPosMove
            // 
            this.btnLoadingPosMove.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btnLoadingPosMove.Font = new System.Drawing.Font("맑은 고딕", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnLoadingPosMove.Location = new System.Drawing.Point(36, 37);
            this.btnLoadingPosMove.Name = "btnLoadingPosMove";
            this.btnLoadingPosMove.Size = new System.Drawing.Size(438, 213);
            this.btnLoadingPosMove.TabIndex = 25;
            this.btnLoadingPosMove.Text = "로딩 장소 이동";
            this.btnLoadingPosMove.UseVisualStyleBackColor = false;
            this.btnLoadingPosMove.Click += new System.EventHandler(this.btnLoadingPosMove_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLoadingPosMove);
            this.groupBox1.Location = new System.Drawing.Point(34, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(510, 292);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Loading";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn4PosMove);
            this.groupBox2.Controls.Add(this.btn3PosMove);
            this.groupBox2.Controls.Add(this.btn2PosMove);
            this.groupBox2.Controls.Add(this.btn1PosMove);
            this.groupBox2.Location = new System.Drawing.Point(565, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1181, 515);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Unloading";
            // 
            // btn4PosMove
            // 
            this.btn4PosMove.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btn4PosMove.Font = new System.Drawing.Font("맑은 고딕", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn4PosMove.Location = new System.Drawing.Point(63, 47);
            this.btn4PosMove.Name = "btn4PosMove";
            this.btn4PosMove.Size = new System.Drawing.Size(438, 213);
            this.btn4PosMove.TabIndex = 25;
            this.btn4PosMove.Text = "6번 장소";
            this.btn4PosMove.UseVisualStyleBackColor = false;
            this.btn4PosMove.Click += new System.EventHandler(this.btn4PosMove_Click);
            // 
            // btn3PosMove
            // 
            this.btn3PosMove.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btn3PosMove.Font = new System.Drawing.Font("맑은 고딕", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn3PosMove.Location = new System.Drawing.Point(662, 47);
            this.btn3PosMove.Name = "btn3PosMove";
            this.btn3PosMove.Size = new System.Drawing.Size(438, 213);
            this.btn3PosMove.TabIndex = 25;
            this.btn3PosMove.Text = "7번 장소 ";
            this.btn3PosMove.UseVisualStyleBackColor = false;
            this.btn3PosMove.Click += new System.EventHandler(this.btn3PosMove_Click);
            // 
            // btn2PosMove
            // 
            this.btn2PosMove.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btn2PosMove.Font = new System.Drawing.Font("맑은 고딕", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn2PosMove.Location = new System.Drawing.Point(63, 283);
            this.btn2PosMove.Name = "btn2PosMove";
            this.btn2PosMove.Size = new System.Drawing.Size(438, 213);
            this.btn2PosMove.TabIndex = 25;
            this.btn2PosMove.Text = "8번 장소";
            this.btn2PosMove.UseVisualStyleBackColor = false;
            this.btn2PosMove.Click += new System.EventHandler(this.btn2PosMove_Click);
            // 
            // btn1PosMove
            // 
            this.btn1PosMove.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btn1PosMove.Font = new System.Drawing.Font("맑은 고딕", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn1PosMove.Location = new System.Drawing.Point(662, 283);
            this.btn1PosMove.Name = "btn1PosMove";
            this.btn1PosMove.Size = new System.Drawing.Size(438, 213);
            this.btn1PosMove.TabIndex = 25;
            this.btn1PosMove.Text = "9번 장소 ";
            this.btn1PosMove.UseVisualStyleBackColor = false;
            this.btn1PosMove.Click += new System.EventHandler(this.btn1PosMove_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnWaitPosMove);
            this.groupBox3.Location = new System.Drawing.Point(34, 474);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(510, 280);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            // 
            // btnWaitPosMove
            // 
            this.btnWaitPosMove.BackColor = System.Drawing.Color.LawnGreen;
            this.btnWaitPosMove.Font = new System.Drawing.Font("맑은 고딕", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnWaitPosMove.Location = new System.Drawing.Point(36, 35);
            this.btnWaitPosMove.Name = "btnWaitPosMove";
            this.btnWaitPosMove.Size = new System.Drawing.Size(438, 213);
            this.btnWaitPosMove.TabIndex = 25;
            this.btnWaitPosMove.Text = "대기 장소 이동";
            this.btnWaitPosMove.UseVisualStyleBackColor = false;
            this.btnWaitPosMove.Click += new System.EventHandler(this.btnWaitPosMove_Click);
            // 
            // btnJobStop
            // 
            this.btnJobStop.BackColor = System.Drawing.Color.Red;
            this.btnJobStop.Font = new System.Drawing.Font("맑은 고딕", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnJobStop.ForeColor = System.Drawing.Color.White;
            this.btnJobStop.Location = new System.Drawing.Point(36, 22);
            this.btnJobStop.Name = "btnJobStop";
            this.btnJobStop.Size = new System.Drawing.Size(499, 140);
            this.btnJobStop.TabIndex = 25;
            this.btnJobStop.Text = "작업 취소";
            this.btnJobStop.UseVisualStyleBackColor = false;
            this.btnJobStop.Click += new System.EventHandler(this.btnJobStop_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnPauseRestart);
            this.groupBox4.Controls.Add(this.btnJobStop);
            this.groupBox4.Location = new System.Drawing.Point(565, 576);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1181, 178);
            this.groupBox4.TabIndex = 26;
            this.groupBox4.TabStop = false;
            // 
            // btnPauseRestart
            // 
            this.btnPauseRestart.BackColor = System.Drawing.Color.Red;
            this.btnPauseRestart.Font = new System.Drawing.Font("맑은 고딕", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnPauseRestart.ForeColor = System.Drawing.Color.White;
            this.btnPauseRestart.Location = new System.Drawing.Point(625, 22);
            this.btnPauseRestart.Name = "btnPauseRestart";
            this.btnPauseRestart.Size = new System.Drawing.Size(499, 140);
            this.btnPauseRestart.TabIndex = 25;
            this.btnPauseRestart.Text = "일시정지";
            this.btnPauseRestart.UseVisualStyleBackColor = false;
            this.btnPauseRestart.Click += new System.EventHandler(this.btnPauseRestart_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tabControl1.Location = new System.Drawing.Point(22, 121);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1856, 828);
            this.tabControl1.TabIndex = 27;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tabPage1.Location = new System.Drawing.Point(4, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1848, 785);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "메인";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tabPage2.Location = new System.Drawing.Point(4, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1848, 785);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "장소 지정 이동";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(67, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1748, 726);
            this.panel1.TabIndex = 0;
            // 
            // cboRobotID
            // 
            this.cboRobotID.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cboRobotID.FormattingEnabled = true;
            this.cboRobotID.Items.AddRange(new object[] {
            "R_005",
            "R_006",
            "R_007",
            "R_008"});
            this.cboRobotID.Location = new System.Drawing.Point(622, 65);
            this.cboRobotID.Name = "cboRobotID";
            this.cboRobotID.Size = new System.Drawing.Size(153, 33);
            this.cboRobotID.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(619, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Delivery Robot:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DeliveryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.ClientSize = new System.Drawing.Size(1904, 961);
            this.Controls.Add(this.cboRobotID);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox5);
            this.Name = "DeliveryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Delivery Robot";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DeliveryForm_Load);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtAddr;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button btnLoadingPosMove;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn2PosMove;
        private System.Windows.Forms.Button btn1PosMove;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnJobStop;
        private System.Windows.Forms.Button btnWaitPosMove;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cboRobotID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn4PosMove;
        private System.Windows.Forms.Button btn3PosMove;
        private System.Windows.Forms.Button btnPauseRestart;
    }
}