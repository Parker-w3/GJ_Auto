namespace GJ.Aging.BURNIN.Udc
{
    partial class udcRunStatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(udcRunStatus));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.Pnl1 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblModelName = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblRunTime = new System.Windows.Forms.Label();
            this.lblHaveTime = new System.Windows.Forms.Label();
            this.lblArea = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblAgingTime = new System.Windows.Forms.Label();
            this.btnCtrlRun = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.lblReadTemp = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblInputVolt = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblPassNum = new System.Windows.Forms.Label();
            this.lblTTNum = new System.Windows.Forms.Label();
            this.btnRunWave = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnScanSN = new System.Windows.Forms.Button();
            this.btnModel = new System.Windows.Forms.Button();
            this.labOutModel = new System.Windows.Forms.Label();
            this.Pnl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Open");
            this.imageList1.Images.SetKeyName(1, "Off");
            this.imageList1.Images.SetKeyName(2, "On");
            this.imageList1.Images.SetKeyName(3, "scan");
            // 
            // Pnl1
            // 
            this.Pnl1.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.Pnl1, "Pnl1");
            this.Pnl1.Controls.Add(this.label1, 0, 0);
            this.Pnl1.Controls.Add(this.lblModelName, 1, 0);
            this.Pnl1.Controls.Add(this.label8, 2, 0);
            this.Pnl1.Controls.Add(this.lblReadTemp, 3, 0);
            this.Pnl1.Controls.Add(this.label14, 2, 1);
            this.Pnl1.Controls.Add(this.lblInputVolt, 3, 1);
            this.Pnl1.Controls.Add(this.label19, 0, 4);
            this.Pnl1.Controls.Add(this.labOutModel, 1, 4);
            this.Pnl1.Controls.Add(this.label20, 2, 4);
            this.Pnl1.Controls.Add(this.lblPassNum, 3, 4);
            this.Pnl1.Controls.Add(this.label2, 2, 3);
            this.Pnl1.Controls.Add(this.lblTTNum, 3, 3);
            this.Pnl1.Controls.Add(this.btnModel, 4, 1);
            this.Pnl1.Controls.Add(this.lblArea, 4, 0);
            this.Pnl1.Controls.Add(this.btnRunWave, 4, 2);
            this.Pnl1.Controls.Add(this.btnScanSN, 4, 3);
            this.Pnl1.Controls.Add(this.btnCtrlRun, 4, 4);
            this.Pnl1.Controls.Add(this.lblAgingTime, 1, 1);
            this.Pnl1.Controls.Add(this.lblHaveTime, 1, 3);
            this.Pnl1.Controls.Add(this.lblRunTime, 1, 2);
            this.Pnl1.Controls.Add(this.label11, 0, 1);
            this.Pnl1.Controls.Add(this.label4, 0, 3);
            this.Pnl1.Controls.Add(this.label7, 0, 2);
            this.Pnl1.Name = "Pnl1";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Name = "label4";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Name = "label1";
            // 
            // lblModelName
            // 
            resources.ApplyResources(this.lblModelName, "lblModelName");
            this.lblModelName.BackColor = System.Drawing.Color.AliceBlue;
            this.lblModelName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblModelName.Name = "lblModelName";
            this.lblModelName.DoubleClick += new System.EventHandler(this.lblModelName_DoubleClick);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Name = "label7";
            // 
            // lblRunTime
            // 
            resources.ApplyResources(this.lblRunTime, "lblRunTime");
            this.lblRunTime.BackColor = System.Drawing.Color.White;
            this.lblRunTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRunTime.ForeColor = System.Drawing.Color.Blue;
            this.lblRunTime.Name = "lblRunTime";
            // 
            // lblHaveTime
            // 
            resources.ApplyResources(this.lblHaveTime, "lblHaveTime");
            this.lblHaveTime.BackColor = System.Drawing.Color.White;
            this.lblHaveTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHaveTime.ForeColor = System.Drawing.Color.Black;
            this.lblHaveTime.Name = "lblHaveTime";
            // 
            // lblArea
            // 
            resources.ApplyResources(this.lblArea, "lblArea");
            this.lblArea.ForeColor = System.Drawing.Color.Red;
            this.lblArea.Name = "lblArea";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Name = "label11";
            // 
            // lblAgingTime
            // 
            this.lblAgingTime.BackColor = System.Drawing.Color.White;
            this.lblAgingTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lblAgingTime, "lblAgingTime");
            this.lblAgingTime.ForeColor = System.Drawing.Color.Black;
            this.lblAgingTime.Name = "lblAgingTime";
            // 
            // btnCtrlRun
            // 
            resources.ApplyResources(this.btnCtrlRun, "btnCtrlRun");
            this.btnCtrlRun.ImageList = this.imageList1;
            this.btnCtrlRun.Name = "btnCtrlRun";
            this.btnCtrlRun.UseVisualStyleBackColor = true;
            this.btnCtrlRun.Click += new System.EventHandler(this.btnCtrlRun_Click);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Name = "label8";
            // 
            // lblReadTemp
            // 
            this.lblReadTemp.BackColor = System.Drawing.Color.White;
            this.lblReadTemp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lblReadTemp, "lblReadTemp");
            this.lblReadTemp.ForeColor = System.Drawing.Color.Black;
            this.lblReadTemp.Name = "lblReadTemp";
            this.lblReadTemp.DoubleClick += new System.EventHandler(this.lblReadTemp_DoubleClick);
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Name = "label14";
            // 
            // lblInputVolt
            // 
            this.lblInputVolt.BackColor = System.Drawing.Color.White;
            this.lblInputVolt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lblInputVolt, "lblInputVolt");
            this.lblInputVolt.ForeColor = System.Drawing.Color.Red;
            this.lblInputVolt.Name = "lblInputVolt";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Name = "label20";
            // 
            // lblPassNum
            // 
            this.lblPassNum.BackColor = System.Drawing.Color.White;
            this.lblPassNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lblPassNum, "lblPassNum");
            this.lblPassNum.ForeColor = System.Drawing.Color.Lime;
            this.lblPassNum.Name = "lblPassNum";
            // 
            // lblTTNum
            // 
            this.lblTTNum.BackColor = System.Drawing.Color.White;
            this.lblTTNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lblTTNum, "lblTTNum");
            this.lblTTNum.ForeColor = System.Drawing.Color.Blue;
            this.lblTTNum.Name = "lblTTNum";
            // 
            // btnRunWave
            // 
            resources.ApplyResources(this.btnRunWave, "btnRunWave");
            this.btnRunWave.Name = "btnRunWave";
            this.btnRunWave.UseVisualStyleBackColor = true;
            this.btnRunWave.Click += new System.EventHandler(this.btnRunWave_Click);
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Name = "label19";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnScanSN
            // 
            resources.ApplyResources(this.btnScanSN, "btnScanSN");
            this.btnScanSN.Name = "btnScanSN";
            this.btnScanSN.UseVisualStyleBackColor = true;
            this.btnScanSN.Click += new System.EventHandler(this.btnScanSN_Click);
            // 
            // btnModel
            // 
            resources.ApplyResources(this.btnModel, "btnModel");
            this.btnModel.Name = "btnModel";
            this.btnModel.UseVisualStyleBackColor = true;
            this.btnModel.Click += new System.EventHandler(this.btnModel_Click);
            // 
            // labOutModel
            // 
            resources.ApplyResources(this.labOutModel, "labOutModel");
            this.labOutModel.BackColor = System.Drawing.Color.White;
            this.labOutModel.Name = "labOutModel";
            // 
            // udcRunStatus
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(222)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.Pnl1);
            this.Name = "udcRunStatus";
            this.Pnl1.ResumeLayout(false);
            this.Pnl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TableLayoutPanel Pnl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCtrlRun;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblPassNum;
        private System.Windows.Forms.Label lblModelName;
        private System.Windows.Forms.Label lblTTNum;
        private System.Windows.Forms.Label lblInputVolt;
        private System.Windows.Forms.Label lblRunTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblAgingTime;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblReadTemp;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.Label lblHaveTime;
		private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRunWave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnScanSN;
        private System.Windows.Forms.Button btnModel;
        private System.Windows.Forms.Label labOutModel;
    }
}
