namespace Syscon_Solution.MappingManager
{
    partial class MappingMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MappingMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnConnect = new DevExpress.XtraEditors.SimpleButton();
            this.txtAddr = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnConnect);
            this.panel1.Controls.Add(this.txtAddr);
            this.panel1.Controls.Add(this.label24);
            this.panel1.Location = new System.Drawing.Point(12, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(660, 62);
            this.panel1.TabIndex = 32;
            // 
            // btnConnect
            // 
            this.btnConnect.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnConnect.ImageOptions.Image")));
            this.btnConnect.Location = new System.Drawing.Point(410, 16);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnConnect.Size = new System.Drawing.Size(148, 36);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "connect";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtAddr
            // 
            this.txtAddr.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtAddr.Location = new System.Drawing.Point(117, 18);
            this.txtAddr.Name = "txtAddr";
            this.txtAddr.Size = new System.Drawing.Size(268, 32);
            this.txtAddr.TabIndex = 6;
            this.txtAddr.Text = "ws://192.168.20.28:9090";
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(30, 24);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(85, 20);
            this.label24.TabIndex = 8;
            this.label24.Text = "ROS서버:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(12, 86);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1870, 918);
            this.panel2.TabIndex = 33;
            // 
            // MappingMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1894, 1016);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MappingMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "맵핑 솔루션Ver1.0";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MappingMain_FormClosed);
            this.Load += new System.EventHandler(this.MappingMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnConnect;
        private System.Windows.Forms.TextBox txtAddr;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Panel panel2;
    }
}