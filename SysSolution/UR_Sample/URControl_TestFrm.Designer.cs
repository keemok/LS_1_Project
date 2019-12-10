namespace SysSolution.UR_Sample
{
    partial class URControl_TestFrm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(URControl_TestFrm));
            this.groupBox_UR = new System.Windows.Forms.GroupBox();
            this.btnWriting = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnWaving = new System.Windows.Forms.Button();
            this.numericUpDown_UR_RepeatCnt = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.listBox_UR = new System.Windows.Forms.ListBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnHoming = new System.Windows.Forms.Button();
            this.btnAssembly = new System.Windows.Forms.Button();
            this.btnObstacle = new System.Windows.Forms.Button();
            this.btnFolding = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtAddr = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox_UR.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_UR_RepeatCnt)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_UR
            // 
            this.groupBox_UR.BackColor = System.Drawing.Color.Transparent;
            this.groupBox_UR.Controls.Add(this.btnWriting);
            this.groupBox_UR.Controls.Add(this.groupBox1);
            this.groupBox_UR.Controls.Add(this.listBox_UR);
            this.groupBox_UR.Controls.Add(this.btnStop);
            this.groupBox_UR.Controls.Add(this.btnHoming);
            this.groupBox_UR.Controls.Add(this.btnAssembly);
            this.groupBox_UR.Controls.Add(this.btnObstacle);
            this.groupBox_UR.Controls.Add(this.btnFolding);
            this.groupBox_UR.Controls.Add(this.label1);
            this.groupBox_UR.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox_UR.Location = new System.Drawing.Point(12, 80);
            this.groupBox_UR.Name = "groupBox_UR";
            this.groupBox_UR.Size = new System.Drawing.Size(658, 468);
            this.groupBox_UR.TabIndex = 25;
            this.groupBox_UR.TabStop = false;
            this.groupBox_UR.Text = "UR 로봇";
            // 
            // btnWriting
            // 
            this.btnWriting.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnWriting.ForeColor = System.Drawing.Color.Blue;
            this.btnWriting.Location = new System.Drawing.Point(300, 177);
            this.btnWriting.Name = "btnWriting";
            this.btnWriting.Size = new System.Drawing.Size(112, 54);
            this.btnWriting.TabIndex = 23;
            this.btnWriting.Text = "Writing";
            this.btnWriting.UseVisualStyleBackColor = true;
            this.btnWriting.Click += new System.EventHandler(this.btnWriting_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnWaving);
            this.groupBox1.Controls.Add(this.numericUpDown_UR_RepeatCnt);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(441, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(190, 154);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            // 
            // btnWaving
            // 
            this.btnWaving.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnWaving.ForeColor = System.Drawing.Color.Blue;
            this.btnWaving.Location = new System.Drawing.Point(17, 88);
            this.btnWaving.Name = "btnWaving";
            this.btnWaving.Size = new System.Drawing.Size(151, 54);
            this.btnWaving.TabIndex = 23;
            this.btnWaving.Text = "Waving";
            this.btnWaving.UseVisualStyleBackColor = true;
            this.btnWaving.Click += new System.EventHandler(this.btnWaving_Click);
            // 
            // numericUpDown_UR_RepeatCnt
            // 
            this.numericUpDown_UR_RepeatCnt.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.numericUpDown_UR_RepeatCnt.Location = new System.Drawing.Point(52, 49);
            this.numericUpDown_UR_RepeatCnt.Name = "numericUpDown_UR_RepeatCnt";
            this.numericUpDown_UR_RepeatCnt.Size = new System.Drawing.Size(79, 26);
            this.numericUpDown_UR_RepeatCnt.TabIndex = 20;
            this.numericUpDown_UR_RepeatCnt.ValueChanged += new System.EventHandler(this.numericUpDown_UR_RepeatCnt_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(48, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 21);
            this.label8.TabIndex = 2;
            this.label8.Text = "동작 횟수";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // listBox_UR
            // 
            this.listBox_UR.FormattingEnabled = true;
            this.listBox_UR.HorizontalScrollbar = true;
            this.listBox_UR.ItemHeight = 12;
            this.listBox_UR.Location = new System.Drawing.Point(29, 237);
            this.listBox_UR.Name = "listBox_UR";
            this.listBox_UR.ScrollAlwaysVisible = true;
            this.listBox_UR.Size = new System.Drawing.Size(602, 208);
            this.listBox_UR.TabIndex = 22;
            this.listBox_UR.SelectedIndexChanged += new System.EventHandler(this.listBox_UR_SelectedIndexChanged);
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnStop.ForeColor = System.Drawing.Color.Red;
            this.btnStop.Location = new System.Drawing.Point(441, 180);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(190, 54);
            this.btnStop.TabIndex = 23;
            this.btnStop.Text = "정지";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnHoming
            // 
            this.btnHoming.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnHoming.ForeColor = System.Drawing.Color.Blue;
            this.btnHoming.Location = new System.Drawing.Point(152, 105);
            this.btnHoming.Name = "btnHoming";
            this.btnHoming.Size = new System.Drawing.Size(120, 54);
            this.btnHoming.TabIndex = 23;
            this.btnHoming.Text = "초기위치";
            this.btnHoming.UseVisualStyleBackColor = true;
            this.btnHoming.Click += new System.EventHandler(this.btnHoming_Click);
            // 
            // btnAssembly
            // 
            this.btnAssembly.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAssembly.ForeColor = System.Drawing.Color.Teal;
            this.btnAssembly.Location = new System.Drawing.Point(12, 20);
            this.btnAssembly.Name = "btnAssembly";
            this.btnAssembly.Size = new System.Drawing.Size(132, 54);
            this.btnAssembly.TabIndex = 23;
            this.btnAssembly.Text = "패키징";
            this.btnAssembly.UseVisualStyleBackColor = true;
            this.btnAssembly.Click += new System.EventHandler(this.btnAssembly_Click);
            // 
            // btnObstacle
            // 
            this.btnObstacle.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnObstacle.ForeColor = System.Drawing.Color.Blue;
            this.btnObstacle.Location = new System.Drawing.Point(300, 105);
            this.btnObstacle.Name = "btnObstacle";
            this.btnObstacle.Size = new System.Drawing.Size(124, 54);
            this.btnObstacle.TabIndex = 23;
            this.btnObstacle.Text = "장애물";
            this.btnObstacle.UseVisualStyleBackColor = true;
            this.btnObstacle.Click += new System.EventHandler(this.btnObstacle_Click);
            // 
            // btnFolding
            // 
            this.btnFolding.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnFolding.ForeColor = System.Drawing.Color.Blue;
            this.btnFolding.Location = new System.Drawing.Point(12, 105);
            this.btnFolding.Name = "btnFolding";
            this.btnFolding.Size = new System.Drawing.Size(112, 54);
            this.btnFolding.TabIndex = 23;
            this.btnFolding.Text = "폴딩";
            this.btnFolding.UseVisualStyleBackColor = true;
            this.btnFolding.Click += new System.EventHandler(this.btnFolding_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(28, 213);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "UR 상태정보";
            this.label1.Click += new System.EventHandler(this.label8_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnConnect);
            this.groupBox5.Controls.Add(this.txtAddr);
            this.groupBox5.Controls.Add(this.label24);
            this.groupBox5.Location = new System.Drawing.Point(15, -2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(537, 66);
            this.groupBox5.TabIndex = 26;
            this.groupBox5.TabStop = false;
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
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
            this.txtAddr.Text = "ws://192.168.0.56:9090";
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label24.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label24.Location = new System.Drawing.Point(6, 27);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(85, 20);
            this.label24.TabIndex = 5;
            this.label24.Text = "100A서버:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // URControl_TestFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 581);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox_UR);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "URControl_TestFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UR컨트롤";
            this.Load += new System.EventHandler(this.URControl_TestFrm_Load);
            this.groupBox_UR.ResumeLayout(false);
            this.groupBox_UR.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_UR_RepeatCnt)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_UR;
        private System.Windows.Forms.ListBox listBox_UR;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDown_UR_RepeatCnt;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtAddr;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button btnFolding;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnWriting;
        private System.Windows.Forms.Button btnWaving;
        private System.Windows.Forms.Button btnHoming;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnObstacle;
        private System.Windows.Forms.Button btnAssembly;
    }
}