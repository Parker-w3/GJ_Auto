namespace GJ.Aging.BURNIN.Udc
{
    partial class udcUUT
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(udcUUT));
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tlTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuOp = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem0 = new System.Windows.Forms.ToolStripSeparator();
            this.tlClearFail = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlClearAllFail = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tlSetNA = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.tlTranFail = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.tlRefAlarm = new System.Windows.Forms.ToolStripMenuItem();
            this.tlChooseModel = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOp.SuspendLayout();
            this.SuspendLayout();
            // 
            // ImageList1
            // 
            this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
            this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList1.Images.SetKeyName(0, "FREE");
            this.ImageList1.Images.SetKeyName(1, "PASS1");
            this.ImageList1.Images.SetKeyName(2, "FAIL1");
            this.ImageList1.Images.SetKeyName(3, "PASS2");
            this.ImageList1.Images.SetKeyName(4, "FAIL2");
            // 
            // tlTip
            // 
            this.tlTip.BackColor = System.Drawing.Color.LightYellow;
            this.tlTip.IsBalloon = true;
            // 
            // menuOp
            // 
            this.menuOp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem0,
            this.tlClearFail,
            this.toolStripMenuItem1,
            this.tlClearAllFail,
            this.toolStripMenuItem2,
            this.tlSetNA,
            this.toolStripMenuItem3,
            this.tlTranFail,
            this.toolStripMenuItem4,
            this.tlRefAlarm,
            this.tlChooseModel});
            this.menuOp.Name = "contextMenuStrip1";
            this.menuOp.Size = new System.Drawing.Size(169, 200);
            // 
            // toolStripMenuItem0
            // 
            this.toolStripMenuItem0.Name = "toolStripMenuItem0";
            this.toolStripMenuItem0.Size = new System.Drawing.Size(165, 6);
            // 
            // tlClearFail
            // 
            this.tlClearFail.Name = "tlClearFail";
            this.tlClearFail.Size = new System.Drawing.Size(168, 24);
            this.tlClearFail.Text = "清除单个不良";
            this.tlClearFail.Click += new System.EventHandler(this.tlClearFail_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(165, 6);
            // 
            // tlClearAllFail
            // 
            this.tlClearAllFail.Name = "tlClearAllFail";
            this.tlClearAllFail.Size = new System.Drawing.Size(168, 24);
            this.tlClearAllFail.Text = "清除所有不良";
            this.tlClearAllFail.Click += new System.EventHandler(this.tlClearAllFail_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(165, 6);
            // 
            // tlSetNA
            // 
            this.tlSetNA.Name = "tlSetNA";
            this.tlSetNA.Size = new System.Drawing.Size(168, 24);
            this.tlSetNA.Text = "设定产品无机";
            this.tlSetNA.Click += new System.EventHandler(this.tlSetNA_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(165, 6);
            // 
            // tlTranFail
            // 
            this.tlTranFail.Name = "tlTranFail";
            this.tlTranFail.Size = new System.Drawing.Size(168, 24);
            this.tlTranFail.Text = "上传不良产品";
            this.tlTranFail.Visible = false;
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(165, 6);
            // 
            // tlRefAlarm
            // 
            this.tlRefAlarm.Name = "tlRefAlarm";
            this.tlRefAlarm.Size = new System.Drawing.Size(168, 24);
            this.tlRefAlarm.Text = "复位报警";
            this.tlRefAlarm.Click += new System.EventHandler(this.tlRefAlarm_Click);
            // 
            // tlChooseModel
            // 
            this.tlChooseModel.Name = "tlChooseModel";
            this.tlChooseModel.Size = new System.Drawing.Size(168, 24);
            this.tlChooseModel.Text = "选择机种";
            this.tlChooseModel.Visible = false;
            this.tlChooseModel.Click += new System.EventHandler(this.tlChooseModel_Click);
            // 
            // udcUUT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.menuOp;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.Name = "udcUUT";
            this.Size = new System.Drawing.Size(418, 133);
            this.Load += new System.EventHandler(this.udcFixture_Load);
            this.menuOp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ImageList ImageList1;
        private System.Windows.Forms.ToolTip tlTip;
        private System.Windows.Forms.ContextMenuStrip menuOp;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem0;
        private System.Windows.Forms.ToolStripMenuItem tlClearFail;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tlClearAllFail;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tlSetNA;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem tlTranFail;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem tlRefAlarm;
        private System.Windows.Forms.ToolStripMenuItem tlChooseModel;
    }
}
