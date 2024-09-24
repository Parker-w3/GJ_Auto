namespace GJ.TOOL
{
    partial class FrmGJ_CC_CP
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkCC4 = new System.Windows.Forms.CheckBox();
            this.chkCC3 = new System.Windows.Forms.CheckBox();
            this.chkCC2 = new System.Windows.Forms.CheckBox();
            this.chkCC1 = new System.Windows.Forms.CheckBox();
            this.chkCC0 = new System.Windows.Forms.CheckBox();
            this.chkKey = new System.Windows.Forms.CheckBox();
            this.txtCP = new System.Windows.Forms.TextBox();
            this.btnGetKey = new System.Windows.Forms.Button();
            this.btnGetCP = new System.Windows.Forms.Button();
            this.btnGetCC = new System.Windows.Forms.Button();
            this.btnSetCP = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGetAdrs = new System.Windows.Forms.Button();
            this.btnSetAdrs = new System.Windows.Forms.Button();
            this.btnWriteBaud = new System.Windows.Forms.Button();
            this.btnScan = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.txtEndAdrs = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtStartAdrs = new System.Windows.Forms.TextBox();
            this.btnReadBaud = new System.Windows.Forms.Button();
            this.labStatus = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.cmbCom = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBaud = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAddr = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblVer = new System.Windows.Forms.Label();
            this.btnReadVersion = new System.Windows.Forms.Button();
            this.CC_CPView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CC_CPView)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.CC_CPView);
            this.splitContainer1.Size = new System.Drawing.Size(775, 366);
            this.splitContainer1.SplitterDistance = 408;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.chkCC4);
            this.groupBox1.Controls.Add(this.chkCC3);
            this.groupBox1.Controls.Add(this.chkCC2);
            this.groupBox1.Controls.Add(this.chkCC1);
            this.groupBox1.Controls.Add(this.chkCC0);
            this.groupBox1.Controls.Add(this.chkKey);
            this.groupBox1.Controls.Add(this.txtCP);
            this.groupBox1.Controls.Add(this.btnGetKey);
            this.groupBox1.Controls.Add(this.btnGetCP);
            this.groupBox1.Controls.Add(this.btnGetCC);
            this.groupBox1.Controls.Add(this.btnSetCP);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnGetAdrs);
            this.groupBox1.Controls.Add(this.btnSetAdrs);
            this.groupBox1.Controls.Add(this.btnWriteBaud);
            this.groupBox1.Controls.Add(this.btnScan);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtEndAdrs);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtStartAdrs);
            this.groupBox1.Controls.Add(this.btnReadBaud);
            this.groupBox1.Controls.Add(this.labStatus);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.cmbCom);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtBaud);
            this.groupBox1.Controls.Add(this.btnOpen);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtAddr);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblVer);
            this.groupBox1.Controls.Add(this.btnReadVersion);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(408, 366);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GJ_CAN_485调试";
            // 
            // chkCC4
            // 
            this.chkCC4.AutoSize = true;
            this.chkCC4.Location = new System.Drawing.Point(12, 227);
            this.chkCC4.Name = "chkCC4";
            this.chkCC4.Size = new System.Drawing.Size(66, 21);
            this.chkCC4.TabIndex = 79;
            this.chkCC4.Text = "Enable";
            this.chkCC4.UseVisualStyleBackColor = true;
            // 
            // chkCC3
            // 
            this.chkCC3.AutoSize = true;
            this.chkCC3.Location = new System.Drawing.Point(217, 200);
            this.chkCC3.Name = "chkCC3";
            this.chkCC3.Size = new System.Drawing.Size(58, 21);
            this.chkCC3.TabIndex = 78;
            this.chkCC3.Text = "100Ω";
            this.chkCC3.UseVisualStyleBackColor = true;
            // 
            // chkCC2
            // 
            this.chkCC2.AutoSize = true;
            this.chkCC2.Location = new System.Drawing.Point(148, 200);
            this.chkCC2.Name = "chkCC2";
            this.chkCC2.Size = new System.Drawing.Size(58, 21);
            this.chkCC2.TabIndex = 77;
            this.chkCC2.Text = "220Ω";
            this.chkCC2.UseVisualStyleBackColor = true;
            // 
            // chkCC1
            // 
            this.chkCC1.AutoSize = true;
            this.chkCC1.Location = new System.Drawing.Point(79, 200);
            this.chkCC1.Name = "chkCC1";
            this.chkCC1.Size = new System.Drawing.Size(58, 21);
            this.chkCC1.TabIndex = 76;
            this.chkCC1.Text = "680Ω";
            this.chkCC1.UseVisualStyleBackColor = true;
            // 
            // chkCC0
            // 
            this.chkCC0.AutoSize = true;
            this.chkCC0.Location = new System.Drawing.Point(12, 200);
            this.chkCC0.Name = "chkCC0";
            this.chkCC0.Size = new System.Drawing.Size(62, 21);
            this.chkCC0.TabIndex = 75;
            this.chkCC0.Text = "1.5KΩ";
            this.chkCC0.UseVisualStyleBackColor = true;
            // 
            // chkKey
            // 
            this.chkKey.AutoSize = true;
            this.chkKey.Location = new System.Drawing.Point(108, 269);
            this.chkKey.Name = "chkKey";
            this.chkKey.Size = new System.Drawing.Size(63, 21);
            this.chkKey.TabIndex = 74;
            this.chkKey.Text = "电子锁";
            this.chkKey.UseVisualStyleBackColor = true;
            // 
            // txtCP
            // 
            this.txtCP.Location = new System.Drawing.Point(108, 236);
            this.txtCP.Name = "txtCP";
            this.txtCP.Size = new System.Drawing.Size(113, 23);
            this.txtCP.TabIndex = 73;
            this.txtCP.Text = "1";
            this.txtCP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnGetKey
            // 
            this.btnGetKey.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnGetKey.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGetKey.Location = new System.Drawing.Point(306, 267);
            this.btnGetKey.Margin = new System.Windows.Forms.Padding(0);
            this.btnGetKey.Name = "btnGetKey";
            this.btnGetKey.Size = new System.Drawing.Size(82, 23);
            this.btnGetKey.TabIndex = 72;
            this.btnGetKey.Text = "读取";
            this.btnGetKey.UseVisualStyleBackColor = true;
            // 
            // btnGetCP
            // 
            this.btnGetCP.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnGetCP.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGetCP.Location = new System.Drawing.Point(306, 236);
            this.btnGetCP.Margin = new System.Windows.Forms.Padding(0);
            this.btnGetCP.Name = "btnGetCP";
            this.btnGetCP.Size = new System.Drawing.Size(82, 23);
            this.btnGetCP.TabIndex = 71;
            this.btnGetCP.Text = "读取";
            this.btnGetCP.UseVisualStyleBackColor = true;
            // 
            // btnGetCC
            // 
            this.btnGetCC.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnGetCC.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGetCC.Location = new System.Drawing.Point(306, 207);
            this.btnGetCC.Margin = new System.Windows.Forms.Padding(0);
            this.btnGetCC.Name = "btnGetCC";
            this.btnGetCC.Size = new System.Drawing.Size(82, 23);
            this.btnGetCC.TabIndex = 70;
            this.btnGetCC.Text = "读取";
            this.btnGetCC.UseVisualStyleBackColor = true;
            // 
            // btnSetCP
            // 
            this.btnSetCP.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSetCP.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSetCP.Location = new System.Drawing.Point(224, 236);
            this.btnSetCP.Margin = new System.Windows.Forms.Padding(0);
            this.btnSetCP.Name = "btnSetCP";
            this.btnSetCP.Size = new System.Drawing.Size(82, 23);
            this.btnSetCP.TabIndex = 68;
            this.btnSetCP.Text = "设定";
            this.btnSetCP.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(76, 239);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 17);
            this.label5.TabIndex = 63;
            this.label5.Text = "CP:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(12, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 17);
            this.label4.TabIndex = 61;
            this.label4.Text = "CC:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnGetAdrs
            // 
            this.btnGetAdrs.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnGetAdrs.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGetAdrs.Location = new System.Drawing.Point(306, 50);
            this.btnGetAdrs.Margin = new System.Windows.Forms.Padding(0);
            this.btnGetAdrs.Name = "btnGetAdrs";
            this.btnGetAdrs.Size = new System.Drawing.Size(82, 23);
            this.btnGetAdrs.TabIndex = 60;
            this.btnGetAdrs.Text = "读地址";
            this.btnGetAdrs.UseVisualStyleBackColor = true;
            this.btnGetAdrs.Click += new System.EventHandler(this.btnGetAdrs_Click);
            // 
            // btnSetAdrs
            // 
            this.btnSetAdrs.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSetAdrs.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSetAdrs.Location = new System.Drawing.Point(224, 50);
            this.btnSetAdrs.Margin = new System.Windows.Forms.Padding(0);
            this.btnSetAdrs.Name = "btnSetAdrs";
            this.btnSetAdrs.Size = new System.Drawing.Size(82, 23);
            this.btnSetAdrs.TabIndex = 59;
            this.btnSetAdrs.Text = "写地址";
            this.btnSetAdrs.UseVisualStyleBackColor = true;
            this.btnSetAdrs.Click += new System.EventHandler(this.btnSetAdrs_Click);
            // 
            // btnWriteBaud
            // 
            this.btnWriteBaud.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnWriteBaud.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnWriteBaud.Location = new System.Drawing.Point(306, 79);
            this.btnWriteBaud.Margin = new System.Windows.Forms.Padding(0);
            this.btnWriteBaud.Name = "btnWriteBaud";
            this.btnWriteBaud.Size = new System.Drawing.Size(82, 23);
            this.btnWriteBaud.TabIndex = 58;
            this.btnWriteBaud.Text = "写波特率";
            this.btnWriteBaud.UseVisualStyleBackColor = true;
            this.btnWriteBaud.Click += new System.EventHandler(this.btnWriteBaud_Click);
            // 
            // btnScan
            // 
            this.btnScan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnScan.Location = new System.Drawing.Point(309, 331);
            this.btnScan.Margin = new System.Windows.Forms.Padding(0);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(82, 23);
            this.btnScan.TabIndex = 57;
            this.btnScan.Text = "扫描";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label15.Location = new System.Drawing.Point(164, 334);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 17);
            this.label15.TabIndex = 55;
            this.label15.Text = "结束地址:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtEndAdrs
            // 
            this.txtEndAdrs.Location = new System.Drawing.Point(224, 331);
            this.txtEndAdrs.Name = "txtEndAdrs";
            this.txtEndAdrs.Size = new System.Drawing.Size(82, 23);
            this.txtEndAdrs.TabIndex = 56;
            this.txtEndAdrs.Text = "48";
            this.txtEndAdrs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(164, 305);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 17);
            this.label14.TabIndex = 53;
            this.label14.Text = "起始地址:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtStartAdrs
            // 
            this.txtStartAdrs.Location = new System.Drawing.Point(224, 302);
            this.txtStartAdrs.Name = "txtStartAdrs";
            this.txtStartAdrs.Size = new System.Drawing.Size(82, 23);
            this.txtStartAdrs.TabIndex = 54;
            this.txtStartAdrs.Text = "1";
            this.txtStartAdrs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnReadBaud
            // 
            this.btnReadBaud.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnReadBaud.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnReadBaud.Location = new System.Drawing.Point(224, 79);
            this.btnReadBaud.Margin = new System.Windows.Forms.Padding(0);
            this.btnReadBaud.Name = "btnReadBaud";
            this.btnReadBaud.Size = new System.Drawing.Size(82, 23);
            this.btnReadBaud.TabIndex = 44;
            this.btnReadBaud.Text = "读波特率";
            this.btnReadBaud.UseVisualStyleBackColor = true;
            this.btnReadBaud.Click += new System.EventHandler(this.btnReadBaud_Click);
            // 
            // labStatus
            // 
            this.labStatus.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labStatus.Location = new System.Drawing.Point(12, 141);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(376, 39);
            this.labStatus.TabIndex = 43;
            this.labStatus.Text = "---";
            this.labStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label29.Location = new System.Drawing.Point(55, 23);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(47, 17);
            this.label29.TabIndex = 33;
            this.label29.Text = "串口号:";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbCom
            // 
            this.cmbCom.FormattingEnabled = true;
            this.cmbCom.Location = new System.Drawing.Point(108, 20);
            this.cmbCom.Name = "cmbCom";
            this.cmbCom.Size = new System.Drawing.Size(113, 25);
            this.cmbCom.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(55, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 35;
            this.label1.Text = "波特率:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBaud
            // 
            this.txtBaud.Location = new System.Drawing.Point(108, 79);
            this.txtBaud.Name = "txtBaud";
            this.txtBaud.Size = new System.Drawing.Size(113, 23);
            this.txtBaud.TabIndex = 36;
            this.txtBaud.Text = "57600,n,8,1";
            this.txtBaud.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnOpen
            // 
            this.btnOpen.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOpen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOpen.Location = new System.Drawing.Point(224, 20);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(0);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(82, 23);
            this.btnOpen.TabIndex = 37;
            this.btnOpen.Text = "打开";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(17, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 17);
            this.label3.TabIndex = 38;
            this.label3.Text = "CAN盒版地址:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAddr
            // 
            this.txtAddr.Location = new System.Drawing.Point(108, 50);
            this.txtAddr.Name = "txtAddr";
            this.txtAddr.Size = new System.Drawing.Size(113, 23);
            this.txtAddr.TabIndex = 39;
            this.txtAddr.Text = "1";
            this.txtAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(29, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 40;
            this.label2.Text = "CAN盒版本:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVer
            // 
            this.lblVer.BackColor = System.Drawing.SystemColors.Control;
            this.lblVer.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblVer.ForeColor = System.Drawing.Color.Green;
            this.lblVer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblVer.Location = new System.Drawing.Point(108, 108);
            this.lblVer.Name = "lblVer";
            this.lblVer.Size = new System.Drawing.Size(113, 23);
            this.lblVer.TabIndex = 41;
            this.lblVer.Text = "---";
            this.lblVer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnReadVersion
            // 
            this.btnReadVersion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnReadVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnReadVersion.Location = new System.Drawing.Point(224, 108);
            this.btnReadVersion.Margin = new System.Windows.Forms.Padding(0);
            this.btnReadVersion.Name = "btnReadVersion";
            this.btnReadVersion.Size = new System.Drawing.Size(82, 23);
            this.btnReadVersion.TabIndex = 42;
            this.btnReadVersion.Text = "读版本";
            this.btnReadVersion.UseVisualStyleBackColor = true;
            this.btnReadVersion.Click += new System.EventHandler(this.btnReadVersion_Click);
            // 
            // CC_CPView
            // 
            this.CC_CPView.AllowUserToAddRows = false;
            this.CC_CPView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CC_CPView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.CC_CPView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CC_CPView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.CC_CPView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CC_CPView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
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
            this.CC_CPView.DefaultCellStyle = dataGridViewCellStyle3;
            this.CC_CPView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CC_CPView.Location = new System.Drawing.Point(0, 0);
            this.CC_CPView.Name = "CC_CPView";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CC_CPView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.CC_CPView.RowHeadersWidth = 20;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CC_CPView.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.CC_CPView.RowTemplate.Height = 23;
            this.CC_CPView.Size = new System.Drawing.Size(363, 366);
            this.CC_CPView.TabIndex = 47;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "地址";
            this.Column1.Name = "Column1";
            this.Column1.Width = 60;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "结果";
            this.Column2.Name = "Column2";
            this.Column2.Width = 60;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "版本";
            this.Column3.Name = "Column3";
            this.Column3.Width = 60;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "占空比";
            this.Column4.Name = "Column4";
            // 
            // FrmGJ_CC_CP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 366);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmGJ_CC_CP";
            this.Text = "CC_CP控制板调试工具";
            this.Load += new System.EventHandler(this.FrmGJ_CC_CP_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CC_CPView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGetAdrs;
        private System.Windows.Forms.Button btnSetAdrs;
        private System.Windows.Forms.Button btnWriteBaud;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtEndAdrs;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtStartAdrs;
        private System.Windows.Forms.Button btnReadBaud;
        private System.Windows.Forms.Label labStatus;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ComboBox cmbCom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBaud;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAddr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblVer;
        private System.Windows.Forms.Button btnReadVersion;
        private System.Windows.Forms.DataGridView CC_CPView;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSetCP;
        private System.Windows.Forms.Button btnGetKey;
        private System.Windows.Forms.Button btnGetCP;
        private System.Windows.Forms.Button btnGetCC;
        private System.Windows.Forms.CheckBox chkKey;
        private System.Windows.Forms.TextBox txtCP;
        private System.Windows.Forms.CheckBox chkCC4;
        private System.Windows.Forms.CheckBox chkCC3;
        private System.Windows.Forms.CheckBox chkCC2;
        private System.Windows.Forms.CheckBox chkCC1;
        private System.Windows.Forms.CheckBox chkCC0;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}