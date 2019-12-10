namespace SysSolution
{
    partial class MapDspForm
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
            this.pb_ori = new System.Windows.Forms.PictureBox();
            this.pb_map = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnReadMap = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtAddr = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pb_Globalcost = new System.Windows.Forms.PictureBox();
            this.btnCostMapRead = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.pb_localcostmap = new System.Windows.Forms.PictureBox();
            this.btnLocalCostMapRead = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_ori)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_map)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Globalcost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_localcostmap)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pb_ori
            // 
            this.pb_ori.Location = new System.Drawing.Point(1682, 934);
            this.pb_ori.Name = "pb_ori";
            this.pb_ori.Size = new System.Drawing.Size(10, 10);
            this.pb_ori.TabIndex = 0;
            this.pb_ori.TabStop = false;
            this.pb_ori.Visible = false;
            // 
            // pb_map
            // 
            this.pb_map.Location = new System.Drawing.Point(22, 66);
            this.pb_map.Name = "pb_map";
            this.pb_map.Size = new System.Drawing.Size(800, 800);
            this.pb_map.TabIndex = 0;
            this.pb_map.TabStop = false;
            this.pb_map.Paint += new System.Windows.Forms.PaintEventHandler(this.pb_map_Paint);
            this.pb_map.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_map_MouseDown);
            this.pb_map.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pb_map_MouseMove);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1669, 758);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(10, 10);
            this.button1.TabIndex = 1;
            this.button1.Text = "열기";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1716, 758);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(10, 10);
            this.button2.TabIndex = 1;
            this.button2.Text = "프로세싱";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnReadMap
            // 
            this.btnReadMap.Location = new System.Drawing.Point(22, 20);
            this.btnReadMap.Name = "btnReadMap";
            this.btnReadMap.Size = new System.Drawing.Size(132, 40);
            this.btnReadMap.TabIndex = 1;
            this.btnReadMap.Text = "맵읽기";
            this.btnReadMap.UseVisualStyleBackColor = true;
            this.btnReadMap.Click += new System.EventHandler(this.btnReadMap_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(1488, 985);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(394, 4);
            this.listBox1.TabIndex = 22;
            this.listBox1.Visible = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnConnect);
            this.groupBox5.Controls.Add(this.txtAddr);
            this.groupBox5.Controls.Add(this.label24);
            this.groupBox5.Location = new System.Drawing.Point(12, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(537, 66);
            this.groupBox5.TabIndex = 21;
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
            this.label24.Location = new System.Drawing.Point(6, 27);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(85, 20);
            this.label24.TabIndex = 5;
            this.label24.Text = "ROS서버:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1792, 859);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(10, 10);
            this.button3.TabIndex = 23;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1600, 754);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(10, 10);
            this.button4.TabIndex = 23;
            this.button4.Text = "button3";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(173, 20);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 40);
            this.button5.TabIndex = 24;
            this.button5.Text = "Unsubscribe";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1722, 774);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(10, 21);
            this.textBox1.TabIndex = 25;
            this.textBox1.Visible = false;
            // 
            // pb_Globalcost
            // 
            this.pb_Globalcost.Location = new System.Drawing.Point(970, 65);
            this.pb_Globalcost.Name = "pb_Globalcost";
            this.pb_Globalcost.Size = new System.Drawing.Size(491, 701);
            this.pb_Globalcost.TabIndex = 0;
            this.pb_Globalcost.TabStop = false;
            this.pb_Globalcost.Paint += new System.Windows.Forms.PaintEventHandler(this.pb_map_Paint);
            // 
            // btnCostMapRead
            // 
            this.btnCostMapRead.Location = new System.Drawing.Point(970, 20);
            this.btnCostMapRead.Name = "btnCostMapRead";
            this.btnCostMapRead.Size = new System.Drawing.Size(132, 40);
            this.btnCostMapRead.TabIndex = 1;
            this.btnCostMapRead.Text = "GlobalCost맵읽기";
            this.btnCostMapRead.UseVisualStyleBackColor = true;
            this.btnCostMapRead.Click += new System.EventHandler(this.btnCostMapRead_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(1121, 20);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 40);
            this.button6.TabIndex = 24;
            this.button6.Text = "Unsubscribe";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // pb_localcostmap
            // 
            this.pb_localcostmap.Location = new System.Drawing.Point(1534, 300);
            this.pb_localcostmap.Name = "pb_localcostmap";
            this.pb_localcostmap.Size = new System.Drawing.Size(348, 284);
            this.pb_localcostmap.TabIndex = 0;
            this.pb_localcostmap.TabStop = false;
            this.pb_localcostmap.Paint += new System.Windows.Forms.PaintEventHandler(this.pb_map_Paint);
            // 
            // btnLocalCostMapRead
            // 
            this.btnLocalCostMapRead.Location = new System.Drawing.Point(1534, 227);
            this.btnLocalCostMapRead.Name = "btnLocalCostMapRead";
            this.btnLocalCostMapRead.Size = new System.Drawing.Size(106, 54);
            this.btnLocalCostMapRead.TabIndex = 26;
            this.btnLocalCostMapRead.Text = "button7";
            this.btnLocalCostMapRead.UseVisualStyleBackColor = true;
            this.btnLocalCostMapRead.Click += new System.EventHandler(this.btnLocalCostMapRead_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(1826, 785);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(10, 10);
            this.button8.TabIndex = 27;
            this.button8.Text = "button8";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Visible = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(1586, 793);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(10, 10);
            this.button7.TabIndex = 24;
            this.button7.Text = "Unsubscribe";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Visible = false;
            this.button7.Click += new System.EventHandler(this.button7_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(609, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(456, 96);
            this.label1.TabIndex = 28;
            this.label1.Text = "로봇 Map";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnReadMap);
            this.groupBox1.Controls.Add(this.pb_map);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.btnCostMapRead);
            this.groupBox1.Controls.Add(this.pb_Globalcost);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Location = new System.Drawing.Point(12, 128);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1496, 881);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Map 정보";
            // 
            // MapDspForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.btnLocalCostMapRead);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pb_localcostmap);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pb_ori);
            this.Name = "MapDspForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MapDspForm";
            this.Load += new System.EventHandler(this.MapDspForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb_ori)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_map)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Globalcost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_localcostmap)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_ori;
        private System.Windows.Forms.PictureBox pb_map;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnReadMap;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtAddr;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pb_Globalcost;
        private System.Windows.Forms.Button btnCostMapRead;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.PictureBox pb_localcostmap;
        private System.Windows.Forms.Button btnLocalCostMapRead;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}