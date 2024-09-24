namespace GJ.TOOL
{
    partial class FrmAcbelIIC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAcbelIIC));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.spc1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSwitch = new System.Windows.Forms.Button();
            this.btnRelay = new System.Windows.Forms.Button();
            this.cmbRelay = new System.Windows.Forms.ComboBox();
            this.cmbSwitch = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.btnScan = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.txtEndAdrs = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtStartAdrs = new System.Windows.Forms.TextBox();
            this.btnsetUUTAdrs = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.txtUUTAdrs = new System.Windows.Forms.TextBox();
            this.btnclearonoff = new System.Windows.Forms.Button();
            this.btnClearFail = new System.Windows.Forms.Button();
            this.chkModel = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbCH = new System.Windows.Forms.ComboBox();
            this.btnReadData = new System.Windows.Forms.Button();
            this.labStatus = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.cmbCom = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBaud = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAddr = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labVer = new System.Windows.Forms.Label();
            this.btnreadVersion = new System.Windows.Forms.Button();
            this.pnlsys = new System.Windows.Forms.TableLayoutPanel();
            this.IICView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnl1 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.spc1)).BeginInit();
            this.spc1.Panel1.SuspendLayout();
            this.spc1.Panel2.SuspendLayout();
            this.spc1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlsys.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IICView)).BeginInit();
            this.pnl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // spc1
            // 
            resources.ApplyResources(this.spc1, "spc1");
            this.spc1.Name = "spc1";
            // 
            // spc1.Panel1
            // 
            this.spc1.Panel1.Controls.Add(this.groupBox1);
            // 
            // spc1.Panel2
            // 
            this.spc1.Panel2.Controls.Add(this.pnlsys);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.btnSwitch);
            this.groupBox1.Controls.Add(this.btnRelay);
            this.groupBox1.Controls.Add(this.cmbRelay);
            this.groupBox1.Controls.Add(this.cmbSwitch);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.btnScan);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtEndAdrs);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtStartAdrs);
            this.groupBox1.Controls.Add(this.btnsetUUTAdrs);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtUUTAdrs);
            this.groupBox1.Controls.Add(this.btnclearonoff);
            this.groupBox1.Controls.Add(this.btnClearFail);
            this.groupBox1.Controls.Add(this.chkModel);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.cmbCH);
            this.groupBox1.Controls.Add(this.btnReadData);
            this.groupBox1.Controls.Add(this.labStatus);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.cmbCom);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtBaud);
            this.groupBox1.Controls.Add(this.btnOpen);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtAddr);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.labVer);
            this.groupBox1.Controls.Add(this.btnreadVersion);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnSwitch
            // 
            resources.ApplyResources(this.btnSwitch, "btnSwitch");
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.UseVisualStyleBackColor = true;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // btnRelay
            // 
            resources.ApplyResources(this.btnRelay, "btnRelay");
            this.btnRelay.Name = "btnRelay";
            this.btnRelay.UseVisualStyleBackColor = true;
            this.btnRelay.Click += new System.EventHandler(this.btnRelay_Click);
            // 
            // cmbRelay
            // 
            this.cmbRelay.FormattingEnabled = true;
            resources.ApplyResources(this.cmbRelay, "cmbRelay");
            this.cmbRelay.Name = "cmbRelay";
            // 
            // cmbSwitch
            // 
            this.cmbSwitch.FormattingEnabled = true;
            resources.ApplyResources(this.cmbSwitch, "cmbSwitch");
            this.cmbSwitch.Name = "cmbSwitch";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // btnScan
            // 
            resources.ApplyResources(this.btnScan, "btnScan");
            this.btnScan.Name = "btnScan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.txtScan_Click);
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // txtEndAdrs
            // 
            resources.ApplyResources(this.txtEndAdrs, "txtEndAdrs");
            this.txtEndAdrs.Name = "txtEndAdrs";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // txtStartAdrs
            // 
            resources.ApplyResources(this.txtStartAdrs, "txtStartAdrs");
            this.txtStartAdrs.Name = "txtStartAdrs";
            // 
            // btnsetUUTAdrs
            // 
            resources.ApplyResources(this.btnsetUUTAdrs, "btnsetUUTAdrs");
            this.btnsetUUTAdrs.Name = "btnsetUUTAdrs";
            this.btnsetUUTAdrs.UseVisualStyleBackColor = true;
            this.btnsetUUTAdrs.Click += new System.EventHandler(this.btnsetUUTAdrs_Click);
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // txtUUTAdrs
            // 
            resources.ApplyResources(this.txtUUTAdrs, "txtUUTAdrs");
            this.txtUUTAdrs.Name = "txtUUTAdrs";
            // 
            // btnclearonoff
            // 
            resources.ApplyResources(this.btnclearonoff, "btnclearonoff");
            this.btnclearonoff.Name = "btnclearonoff";
            this.btnclearonoff.UseVisualStyleBackColor = true;
            this.btnclearonoff.Click += new System.EventHandler(this.btnclearonoff_Click);
            // 
            // btnClearFail
            // 
            resources.ApplyResources(this.btnClearFail, "btnClearFail");
            this.btnClearFail.Name = "btnClearFail";
            this.btnClearFail.UseVisualStyleBackColor = true;
            this.btnClearFail.Click += new System.EventHandler(this.btnClearFail_Click);
            // 
            // chkModel
            // 
            resources.ApplyResources(this.chkModel, "chkModel");
            this.chkModel.Name = "chkModel";
            this.chkModel.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // cmbCH
            // 
            this.cmbCH.FormattingEnabled = true;
            resources.ApplyResources(this.cmbCH, "cmbCH");
            this.cmbCH.Name = "cmbCH";
            // 
            // btnReadData
            // 
            resources.ApplyResources(this.btnReadData, "btnReadData");
            this.btnReadData.Name = "btnReadData";
            this.btnReadData.UseVisualStyleBackColor = true;
            this.btnReadData.Click += new System.EventHandler(this.btnReadData_Click);
            // 
            // labStatus
            // 
            resources.ApplyResources(this.labStatus, "labStatus");
            this.labStatus.Name = "labStatus";
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.Name = "label29";
            // 
            // cmbCom
            // 
            this.cmbCom.FormattingEnabled = true;
            resources.ApplyResources(this.cmbCom, "cmbCom");
            this.cmbCom.Name = "cmbCom";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtBaud
            // 
            resources.ApplyResources(this.txtBaud, "txtBaud");
            this.txtBaud.Name = "txtBaud";
            // 
            // btnOpen
            // 
            resources.ApplyResources(this.btnOpen, "btnOpen");
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtAddr
            // 
            resources.ApplyResources(this.txtAddr, "txtAddr");
            this.txtAddr.Name = "txtAddr";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // labVer
            // 
            resources.ApplyResources(this.labVer, "labVer");
            this.labVer.ForeColor = System.Drawing.Color.Green;
            this.labVer.Name = "labVer";
            // 
            // btnreadVersion
            // 
            resources.ApplyResources(this.btnreadVersion, "btnreadVersion");
            this.btnreadVersion.Name = "btnreadVersion";
            this.btnreadVersion.UseVisualStyleBackColor = true;
            this.btnreadVersion.Click += new System.EventHandler(this.btnreadVersion_Click);
            // 
            // pnlsys
            // 
            resources.ApplyResources(this.pnlsys, "pnlsys");
            this.pnlsys.Controls.Add(this.IICView, 0, 0);
            this.pnlsys.Controls.Add(this.pnl1, 0, 0);
            this.pnlsys.Name = "pnlsys";
            // 
            // IICView
            // 
            this.IICView.AllowUserToAddRows = false;
            this.IICView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IICView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.IICView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.IICView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.IICView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.IICView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.IICView.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.IICView, "IICView");
            this.IICView.Name = "IICView";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.IICView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IICView.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.IICView.RowTemplate.Height = 23;
            // 
            // Column1
            // 
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            resources.ApplyResources(this.Column2, "Column2");
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            resources.ApplyResources(this.Column3, "Column3");
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            resources.ApplyResources(this.Column4, "Column4");
            this.Column4.Name = "Column4";
            // 
            // pnl1
            // 
            resources.ApplyResources(this.pnl1, "pnl1");
            this.pnl1.Controls.Add(this.label6, 0, 0);
            this.pnl1.Controls.Add(this.label7, 1, 0);
            this.pnl1.Controls.Add(this.label8, 3, 0);
            this.pnl1.Controls.Add(this.label12, 2, 0);
            this.pnl1.Controls.Add(this.label4, 4, 0);
            this.pnl1.Controls.Add(this.label5, 5, 0);
            this.pnl1.Controls.Add(this.label9, 6, 0);
            this.pnl1.Controls.Add(this.label10, 7, 0);
            this.pnl1.Name = "pnl1";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // FrmAcbelIIC
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spc1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAcbelIIC";
            this.Load += new System.EventHandler(this.FrmAcbelIIC_Load);
            this.spc1.Panel1.ResumeLayout(false);
            this.spc1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spc1)).EndInit();
            this.spc1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlsys.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.IICView)).EndInit();
            this.pnl1.ResumeLayout(false);
            this.pnl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ComboBox cmbCom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBaud;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAddr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labVer;
        private System.Windows.Forms.Button btnreadVersion;
        private System.Windows.Forms.Label labStatus;
        private System.Windows.Forms.SplitContainer spc1;
        private System.Windows.Forms.Button btnReadData;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbCH;
        private System.Windows.Forms.CheckBox chkModel;
        private System.Windows.Forms.Button btnclearonoff;
        private System.Windows.Forms.Button btnClearFail;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtUUTAdrs;
        private System.Windows.Forms.Button btnsetUUTAdrs;
        private System.Windows.Forms.TableLayoutPanel pnlsys;
        private System.Windows.Forms.TableLayoutPanel pnl1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView IICView;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtEndAdrs;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtStartAdrs;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.Button btnRelay;
        private System.Windows.Forms.ComboBox cmbRelay;
        private System.Windows.Forms.ComboBox cmbSwitch;
    }
}