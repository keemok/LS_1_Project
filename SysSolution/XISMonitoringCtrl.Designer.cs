namespace SysSolution
{
    partial class XISMonitoringCtrl
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
            this.UI_Updatetimer = new System.Windows.Forms.Timer(this.components);
            this.myTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblgoodsname = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox_pallet = new System.Windows.Forms.PictureBox();
            this.pictureBox_goodscode = new System.Windows.Forms.PictureBox();
            this.pictureBox_goods = new System.Windows.Forms.PictureBox();
            this.pictureBox_Lamp = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_pallet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_goodscode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_goods)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Lamp)).BeginInit();
            this.SuspendLayout();
            // 
            // UI_Updatetimer
            // 
            this.UI_Updatetimer.Tick += new System.EventHandler(this.UI_Updatetimer_Tick);
            // 
            // myTimer
            // 
            this.myTimer.Interval = 250;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(37, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "물건 :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblgoodsname);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pictureBox_goodscode);
            this.groupBox1.Controls.Add(this.pictureBox_goods);
            this.groupBox1.Location = new System.Drawing.Point(308, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(288, 413);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // lblgoodsname
            // 
            this.lblgoodsname.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblgoodsname.Location = new System.Drawing.Point(93, 53);
            this.lblgoodsname.Name = "lblgoodsname";
            this.lblgoodsname.Size = new System.Drawing.Size(112, 19);
            this.lblgoodsname.TabIndex = 2;
            this.lblgoodsname.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pictureBox_Lamp);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.pictureBox_pallet);
            this.groupBox2.Location = new System.Drawing.Point(14, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(288, 413);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(25, 292);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "거치대 ";
            // 
            // pictureBox_pallet
            // 
            this.pictureBox_pallet.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.pictureBox_pallet.Location = new System.Drawing.Point(20, 119);
            this.pictureBox_pallet.Name = "pictureBox_pallet";
            this.pictureBox_pallet.Size = new System.Drawing.Size(250, 170);
            this.pictureBox_pallet.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_pallet.TabIndex = 1;
            this.pictureBox_pallet.TabStop = false;
            // 
            // pictureBox_goodscode
            // 
            this.pictureBox_goodscode.Location = new System.Drawing.Point(126, 245);
            this.pictureBox_goodscode.Name = "pictureBox_goodscode";
            this.pictureBox_goodscode.Size = new System.Drawing.Size(51, 44);
            this.pictureBox_goodscode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_goodscode.TabIndex = 0;
            this.pictureBox_goodscode.TabStop = false;
            // 
            // pictureBox_goods
            // 
            this.pictureBox_goods.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.pictureBox_goods.Location = new System.Drawing.Point(41, 75);
            this.pictureBox_goods.Name = "pictureBox_goods";
            this.pictureBox_goods.Size = new System.Drawing.Size(214, 279);
            this.pictureBox_goods.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_goods.TabIndex = 1;
            this.pictureBox_goods.TabStop = false;
            // 
            // pictureBox_Lamp
            // 
            this.pictureBox_Lamp.Location = new System.Drawing.Point(171, 295);
            this.pictureBox_Lamp.Name = "pictureBox_Lamp";
            this.pictureBox_Lamp.Size = new System.Drawing.Size(99, 20);
            this.pictureBox_Lamp.TabIndex = 3;
            this.pictureBox_Lamp.TabStop = false;
            // 
            // XISMonitoringCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "XISMonitoringCtrl";
            this.Size = new System.Drawing.Size(610, 440);
            this.Load += new System.EventHandler(this.XISMonitoringCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_pallet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_goodscode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_goods)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Lamp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer UI_Updatetimer;
        private System.Windows.Forms.Timer myTimer;
        private System.Windows.Forms.PictureBox pictureBox_goodscode;
        private System.Windows.Forms.PictureBox pictureBox_goods;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblgoodsname;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox_pallet;
        private System.Windows.Forms.PictureBox pictureBox_Lamp;
    }
}
