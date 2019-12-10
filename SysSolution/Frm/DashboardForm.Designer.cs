namespace SysSolution.Frm
{
    partial class DashboardForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashboardForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnJobOrder = new System.Windows.Forms.Button();
            this.btnManual = new System.Windows.Forms.Button();
            this.btnJobReg = new System.Windows.Forms.Button();
            this.btnJobResult = new System.Windows.Forms.Button();
            this.btnMapMointor = new System.Windows.Forms.Button();
            this.btnRobotStatusMonitor = new System.Windows.Forms.Button();
            this.cboDisplayScale = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtAddr = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.RobotWorkstatus_Updatetimer = new System.Windows.Forms.Timer(this.components);
            this.timer_goodsload = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.cboDisplayScale);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.txtAddr);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1904, 58);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1233, 22);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(71, 21);
            this.textBox1.TabIndex = 36;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(325, 22);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(83, 21);
            this.numericUpDown1.TabIndex = 35;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button3.Location = new System.Drawing.Point(416, 17);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(83, 28);
            this.button3.TabIndex = 32;
            this.button3.Text = "물건";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button2.Location = new System.Drawing.Point(207, 17);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 28);
            this.button2.TabIndex = 33;
            this.button2.Text = "거치off";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(118, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 28);
            this.button1.TabIndex = 34;
            this.button1.Text = "거치on";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnJobOrder);
            this.groupBox2.Controls.Add(this.btnManual);
            this.groupBox2.Controls.Add(this.btnJobReg);
            this.groupBox2.Controls.Add(this.btnJobResult);
            this.groupBox2.Controls.Add(this.btnMapMointor);
            this.groupBox2.Controls.Add(this.btnRobotStatusMonitor);
            this.groupBox2.Location = new System.Drawing.Point(513, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(668, 58);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            // 
            // btnJobOrder
            // 
            this.btnJobOrder.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnJobOrder.Location = new System.Drawing.Point(18, 18);
            this.btnJobOrder.Name = "btnJobOrder";
            this.btnJobOrder.Size = new System.Drawing.Size(98, 32);
            this.btnJobOrder.TabIndex = 10;
            this.btnJobOrder.Text = "작업지시";
            this.btnJobOrder.UseVisualStyleBackColor = true;
            this.btnJobOrder.Click += new System.EventHandler(this.btnJobOrder_Click);
            // 
            // btnManual
            // 
            this.btnManual.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnManual.Location = new System.Drawing.Point(543, 18);
            this.btnManual.Name = "btnManual";
            this.btnManual.Size = new System.Drawing.Size(98, 32);
            this.btnManual.TabIndex = 10;
            this.btnManual.Text = "수동동작";
            this.btnManual.UseVisualStyleBackColor = true;
            this.btnManual.Click += new System.EventHandler(this.btnManual_Click);
            // 
            // btnJobReg
            // 
            this.btnJobReg.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnJobReg.Location = new System.Drawing.Point(123, 18);
            this.btnJobReg.Name = "btnJobReg";
            this.btnJobReg.Size = new System.Drawing.Size(98, 32);
            this.btnJobReg.TabIndex = 10;
            this.btnJobReg.Text = "작업등록";
            this.btnJobReg.UseVisualStyleBackColor = true;
            this.btnJobReg.Click += new System.EventHandler(this.btnJobReg_Click);
            // 
            // btnJobResult
            // 
            this.btnJobResult.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnJobResult.Location = new System.Drawing.Point(438, 18);
            this.btnJobResult.Name = "btnJobResult";
            this.btnJobResult.Size = new System.Drawing.Size(98, 32);
            this.btnJobResult.TabIndex = 10;
            this.btnJobResult.Text = "생산집계";
            this.btnJobResult.UseVisualStyleBackColor = true;
            this.btnJobResult.Click += new System.EventHandler(this.btnJobResult_Click);
            // 
            // btnMapMointor
            // 
            this.btnMapMointor.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnMapMointor.Location = new System.Drawing.Point(228, 18);
            this.btnMapMointor.Name = "btnMapMointor";
            this.btnMapMointor.Size = new System.Drawing.Size(98, 32);
            this.btnMapMointor.TabIndex = 10;
            this.btnMapMointor.Text = "맵 모니터";
            this.btnMapMointor.UseVisualStyleBackColor = true;
            this.btnMapMointor.Click += new System.EventHandler(this.btnMapMointor_Click);
            // 
            // btnRobotStatusMonitor
            // 
            this.btnRobotStatusMonitor.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRobotStatusMonitor.Location = new System.Drawing.Point(333, 18);
            this.btnRobotStatusMonitor.Name = "btnRobotStatusMonitor";
            this.btnRobotStatusMonitor.Size = new System.Drawing.Size(98, 32);
            this.btnRobotStatusMonitor.TabIndex = 10;
            this.btnRobotStatusMonitor.Text = "로봇 상태";
            this.btnRobotStatusMonitor.UseVisualStyleBackColor = true;
            this.btnRobotStatusMonitor.Click += new System.EventHandler(this.btnRobotStatusMonitor_Click);
            // 
            // cboDisplayScale
            // 
            this.cboDisplayScale.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cboDisplayScale.FormattingEnabled = true;
            this.cboDisplayScale.Items.AddRange(new object[] {
            "1X1",
            "2X2"});
            this.cboDisplayScale.Location = new System.Drawing.Point(12, 29);
            this.cboDisplayScale.Name = "cboDisplayScale";
            this.cboDisplayScale.Size = new System.Drawing.Size(87, 21);
            this.cboDisplayScale.TabIndex = 9;
            this.cboDisplayScale.SelectedIndexChanged += new System.EventHandler(this.cboDisplayScale_SelectedIndexChanged);
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnConnect.Location = new System.Drawing.Point(1752, 20);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(145, 32);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Text = "connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtAddr
            // 
            this.txtAddr.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtAddr.Location = new System.Drawing.Point(1478, 20);
            this.txtAddr.Name = "txtAddr";
            this.txtAddr.Size = new System.Drawing.Size(268, 32);
            this.txtAddr.TabIndex = 6;
            this.txtAddr.Text = "ws://192.168.20.28:9090";
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label24.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label24.Location = new System.Drawing.Point(1391, 26);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(85, 20);
            this.label24.TabIndex = 8;
            this.label24.Text = "ROS서버:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Location = new System.Drawing.Point(0, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(950, 490);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel2.Location = new System.Drawing.Point(956, 58);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(950, 490);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel3.Location = new System.Drawing.Point(0, 550);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(950, 490);
            this.panel3.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel4.Location = new System.Drawing.Point(956, 550);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(950, 490);
            this.panel4.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel5.Location = new System.Drawing.Point(0, 550);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1906, 490);
            this.panel5.TabIndex = 1;
            // 
            // RobotWorkstatus_Updatetimer
            // 
            this.RobotWorkstatus_Updatetimer.Tick += new System.EventHandler(this.RobotWorkstatus_Updatetimer_Tick);
            // 
            // timer_goodsload
            // 
            this.timer_goodsload.Tick += new System.EventHandler(this.timer_goodsload_Tick);
            // 
            // DashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Syscon Solution";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DashboardForm_FormClosing);
            this.Load += new System.EventHandler(this.DashboardForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtAddr;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cboDisplayScale;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Timer RobotWorkstatus_Updatetimer;
        private System.Windows.Forms.Button btnJobReg;
        private System.Windows.Forms.Button btnJobOrder;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnManual;
        private System.Windows.Forms.Button btnJobResult;
        private System.Windows.Forms.Button btnMapMointor;
        private System.Windows.Forms.Button btnRobotStatusMonitor;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer_goodsload;
        private System.Windows.Forms.TextBox textBox1;
    }
}