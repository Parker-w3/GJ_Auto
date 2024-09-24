namespace GJ.TOOL
{
    partial class FrmGJ_CAN_485
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
            this.spcSys = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGetAdrs = new System.Windows.Forms.Button();
            this.btnSetAdrs = new System.Windows.Forms.Button();
            this.btnReadModelNo = new System.Windows.Forms.Button();
            this.btnGetCanNum = new System.Windows.Forms.Button();
            this.btnClearCanNum = new System.Windows.Forms.Button();
            this.btnWriteModelNo = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCanDataNum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtModelNo = new System.Windows.Forms.TextBox();
            this.txtWriteCanBaud = new System.Windows.Forms.Button();
            this.btnReadCanBaud = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCanBaud = new System.Windows.Forms.TextBox();
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSend = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSendCyc = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCanData = new System.Windows.Forms.TextBox();
            this.CANView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.spcSys)).BeginInit();
            this.spcSys.Panel1.SuspendLayout();
            this.spcSys.Panel2.SuspendLayout();
            this.spcSys.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CANView)).BeginInit();
            this.SuspendLayout();
            // 
            // spcSys
            // 
            this.spcSys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcSys.Location = new System.Drawing.Point(0, 0);
            this.spcSys.Name = "spcSys";
            this.spcSys.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spcSys.Panel1
            // 
            this.spcSys.Panel1.Controls.Add(this.groupBox1);
            // 
            // spcSys.Panel2
            // 
            this.spcSys.Panel2.Controls.Add(this.splitContainer1);
            this.spcSys.Size = new System.Drawing.Size(1029, 750);
            this.spcSys.SplitterDistance = 164;
            this.spcSys.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.btnGetAdrs);
            this.groupBox1.Controls.Add(this.btnSetAdrs);
            this.groupBox1.Controls.Add(this.btnReadModelNo);
            this.groupBox1.Controls.Add(this.btnGetCanNum);
            this.groupBox1.Controls.Add(this.btnClearCanNum);
            this.groupBox1.Controls.Add(this.btnWriteModelNo);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtCanDataNum);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtModelNo);
            this.groupBox1.Controls.Add(this.txtWriteCanBaud);
            this.groupBox1.Controls.Add(this.btnReadCanBaud);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCanBaud);
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
            this.groupBox1.Size = new System.Drawing.Size(1029, 164);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GJ_CAN_485调试";
            // 
            // btnGetAdrs
            // 
            this.btnGetAdrs.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnGetAdrs.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGetAdrs.Location = new System.Drawing.Point(306, 47);
            this.btnGetAdrs.Margin = new System.Windows.Forms.Padding(0);
            this.btnGetAdrs.Name = "btnGetAdrs";
            this.btnGetAdrs.Size = new System.Drawing.Size(82, 23);
            this.btnGetAdrs.TabIndex = 72;
            this.btnGetAdrs.Text = "读地址";
            this.btnGetAdrs.UseVisualStyleBackColor = true;
            // 
            // btnSetAdrs
            // 
            this.btnSetAdrs.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSetAdrs.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSetAdrs.Location = new System.Drawing.Point(224, 47);
            this.btnSetAdrs.Margin = new System.Windows.Forms.Padding(0);
            this.btnSetAdrs.Name = "btnSetAdrs";
            this.btnSetAdrs.Size = new System.Drawing.Size(82, 23);
            this.btnSetAdrs.TabIndex = 71;
            this.btnSetAdrs.Text = "写地址";
            this.btnSetAdrs.UseVisualStyleBackColor = true;
            // 
            // btnReadModelNo
            // 
            this.btnReadModelNo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnReadModelNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnReadModelNo.Location = new System.Drawing.Point(625, 50);
            this.btnReadModelNo.Margin = new System.Windows.Forms.Padding(0);
            this.btnReadModelNo.Name = "btnReadModelNo";
            this.btnReadModelNo.Size = new System.Drawing.Size(82, 23);
            this.btnReadModelNo.TabIndex = 70;
            this.btnReadModelNo.Text = "读取编号";
            this.btnReadModelNo.UseVisualStyleBackColor = true;
            this.btnReadModelNo.Click += new System.EventHandler(this.btnReadModelNo_Click);
            // 
            // btnGetCanNum
            // 
            this.btnGetCanNum.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnGetCanNum.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGetCanNum.Location = new System.Drawing.Point(625, 79);
            this.btnGetCanNum.Margin = new System.Windows.Forms.Padding(0);
            this.btnGetCanNum.Name = "btnGetCanNum";
            this.btnGetCanNum.Size = new System.Drawing.Size(82, 23);
            this.btnGetCanNum.TabIndex = 69;
            this.btnGetCanNum.Text = "读取数量";
            this.btnGetCanNum.UseVisualStyleBackColor = true;
            this.btnGetCanNum.Click += new System.EventHandler(this.btnGetCanNum_Click);
            // 
            // btnClearCanNum
            // 
            this.btnClearCanNum.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClearCanNum.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClearCanNum.Location = new System.Drawing.Point(707, 79);
            this.btnClearCanNum.Margin = new System.Windows.Forms.Padding(0);
            this.btnClearCanNum.Name = "btnClearCanNum";
            this.btnClearCanNum.Size = new System.Drawing.Size(82, 23);
            this.btnClearCanNum.TabIndex = 68;
            this.btnClearCanNum.Text = "清除数量";
            this.btnClearCanNum.UseVisualStyleBackColor = true;
            this.btnClearCanNum.Click += new System.EventHandler(this.btnClearCanNum_Click);
            // 
            // btnWriteModelNo
            // 
            this.btnWriteModelNo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnWriteModelNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnWriteModelNo.Location = new System.Drawing.Point(707, 50);
            this.btnWriteModelNo.Margin = new System.Windows.Forms.Padding(0);
            this.btnWriteModelNo.Name = "btnWriteModelNo";
            this.btnWriteModelNo.Size = new System.Drawing.Size(82, 23);
            this.btnWriteModelNo.TabIndex = 67;
            this.btnWriteModelNo.Text = "写入编号";
            this.btnWriteModelNo.UseVisualStyleBackColor = true;
            this.btnWriteModelNo.Click += new System.EventHandler(this.btnWriteModelNo_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(396, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 17);
            this.label6.TabIndex = 65;
            this.label6.Text = "邮箱数据个数读取:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCanDataNum
            // 
            this.txtCanDataNum.Location = new System.Drawing.Point(509, 79);
            this.txtCanDataNum.Name = "txtCanDataNum";
            this.txtCanDataNum.Size = new System.Drawing.Size(113, 23);
            this.txtCanDataNum.TabIndex = 66;
            this.txtCanDataNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(396, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 17);
            this.label5.TabIndex = 63;
            this.label5.Text = "特定机种设定编号:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtModelNo
            // 
            this.txtModelNo.Location = new System.Drawing.Point(509, 50);
            this.txtModelNo.Name = "txtModelNo";
            this.txtModelNo.Size = new System.Drawing.Size(113, 23);
            this.txtModelNo.TabIndex = 64;
            this.txtModelNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtWriteCanBaud
            // 
            this.txtWriteCanBaud.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtWriteCanBaud.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtWriteCanBaud.Location = new System.Drawing.Point(306, 105);
            this.txtWriteCanBaud.Margin = new System.Windows.Forms.Padding(0);
            this.txtWriteCanBaud.Name = "txtWriteCanBaud";
            this.txtWriteCanBaud.Size = new System.Drawing.Size(82, 23);
            this.txtWriteCanBaud.TabIndex = 62;
            this.txtWriteCanBaud.Text = "写波特率";
            this.txtWriteCanBaud.UseVisualStyleBackColor = true;
            this.txtWriteCanBaud.Click += new System.EventHandler(this.txtWriteCanBaud_Click);
            // 
            // btnReadCanBaud
            // 
            this.btnReadCanBaud.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnReadCanBaud.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnReadCanBaud.Location = new System.Drawing.Point(224, 105);
            this.btnReadCanBaud.Margin = new System.Windows.Forms.Padding(0);
            this.btnReadCanBaud.Name = "btnReadCanBaud";
            this.btnReadCanBaud.Size = new System.Drawing.Size(82, 23);
            this.btnReadCanBaud.TabIndex = 61;
            this.btnReadCanBaud.Text = "读波特率";
            this.btnReadCanBaud.UseVisualStyleBackColor = true;
            this.btnReadCanBaud.Click += new System.EventHandler(this.btnReadCanBaud_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(13, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 17);
            this.label4.TabIndex = 59;
            this.label4.Text = "CAN波特率(K):";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCanBaud
            // 
            this.txtCanBaud.Location = new System.Drawing.Point(108, 105);
            this.txtCanBaud.Name = "txtCanBaud";
            this.txtCanBaud.Size = new System.Drawing.Size(113, 23);
            this.txtCanBaud.TabIndex = 60;
            this.txtCanBaud.Text = "125";
            this.txtCanBaud.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnWriteBaud
            // 
            this.btnWriteBaud.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnWriteBaud.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnWriteBaud.Location = new System.Drawing.Point(306, 76);
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
            this.btnScan.Location = new System.Drawing.Point(919, 108);
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
            this.label15.Location = new System.Drawing.Point(859, 82);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 17);
            this.label15.TabIndex = 55;
            this.label15.Text = "结束地址:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtEndAdrs
            // 
            this.txtEndAdrs.Location = new System.Drawing.Point(919, 79);
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
            this.label14.Location = new System.Drawing.Point(859, 53);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 17);
            this.label14.TabIndex = 53;
            this.label14.Text = "起始地址:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtStartAdrs
            // 
            this.txtStartAdrs.Location = new System.Drawing.Point(919, 50);
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
            this.btnReadBaud.Location = new System.Drawing.Point(224, 76);
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
            this.labStatus.BackColor = System.Drawing.SystemColors.Control;
            this.labStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labStatus.Location = new System.Drawing.Point(509, 19);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(492, 23);
            this.labStatus.TabIndex = 43;
            this.labStatus.Text = "---";
            this.labStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label29.Location = new System.Drawing.Point(55, 19);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(47, 17);
            this.label29.TabIndex = 33;
            this.label29.Text = "串口号:";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbCom
            // 
            this.cmbCom.FormattingEnabled = true;
            this.cmbCom.Location = new System.Drawing.Point(108, 16);
            this.cmbCom.Name = "cmbCom";
            this.cmbCom.Size = new System.Drawing.Size(113, 25);
            this.cmbCom.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(55, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 35;
            this.label1.Text = "波特率:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBaud
            // 
            this.txtBaud.Location = new System.Drawing.Point(108, 76);
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
            this.btnOpen.Location = new System.Drawing.Point(224, 16);
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
            this.label3.Location = new System.Drawing.Point(17, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 17);
            this.label3.TabIndex = 38;
            this.label3.Text = "CAN盒版地址:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAddr
            // 
            this.txtAddr.Location = new System.Drawing.Point(108, 47);
            this.txtAddr.Name = "txtAddr";
            this.txtAddr.Size = new System.Drawing.Size(93, 23);
            this.txtAddr.TabIndex = 39;
            this.txtAddr.Text = "1";
            this.txtAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(29, 137);
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
            this.lblVer.Location = new System.Drawing.Point(108, 134);
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
            this.btnReadVersion.Location = new System.Drawing.Point(224, 134);
            this.btnReadVersion.Margin = new System.Windows.Forms.Padding(0);
            this.btnReadVersion.Name = "btnReadVersion";
            this.btnReadVersion.Size = new System.Drawing.Size(82, 23);
            this.btnReadVersion.TabIndex = 42;
            this.btnReadVersion.Text = "读版本";
            this.btnReadVersion.UseVisualStyleBackColor = true;
            this.btnReadVersion.Click += new System.EventHandler(this.btnReadVersion_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.CANView);
            this.splitContainer1.Size = new System.Drawing.Size(1029, 582);
            this.splitContainer1.SplitterDistance = 694;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Size = new System.Drawing.Size(694, 582);
            this.splitContainer2.SplitterDistance = 361;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panel1);
            this.groupBox3.Controls.Add(this.btnSend);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtSendCyc);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(694, 361);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "发送栏";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(3, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(688, 381);
            this.panel1.TabIndex = 3;
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSend.Location = new System.Drawing.Point(582, 20);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(96, 26);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(418, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 17);
            this.label7.TabIndex = 1;
            this.label7.Text = "循环次数:";
            // 
            // txtSendCyc
            // 
            this.txtSendCyc.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSendCyc.Location = new System.Drawing.Point(480, 22);
            this.txtSendCyc.Name = "txtSendCyc";
            this.txtSendCyc.Size = new System.Drawing.Size(96, 23);
            this.txtSendCyc.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCanData);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(694, 217);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "接收栏";
            // 
            // txtCanData
            // 
            this.txtCanData.Location = new System.Drawing.Point(12, 25);
            this.txtCanData.Multiline = true;
            this.txtCanData.Name = "txtCanData";
            this.txtCanData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCanData.Size = new System.Drawing.Size(666, 225);
            this.txtCanData.TabIndex = 0;
            // 
            // CANView
            // 
            this.CANView.AllowUserToAddRows = false;
            this.CANView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CANView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.CANView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CANView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.CANView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CANView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
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
            this.CANView.DefaultCellStyle = dataGridViewCellStyle3;
            this.CANView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CANView.Location = new System.Drawing.Point(0, 0);
            this.CANView.Name = "CANView";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CANView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.CANView.RowHeadersWidth = 20;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CANView.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.CANView.RowTemplate.Height = 23;
            this.CANView.Size = new System.Drawing.Size(331, 582);
            this.CANView.TabIndex = 46;
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
            this.Column4.HeaderText = "CAN波特率";
            this.Column4.Name = "Column4";
            // 
            // FrmGJ_CAN_485
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 750);
            this.Controls.Add(this.spcSys);
            this.Name = "FrmGJ_CAN_485";
            this.Text = "FrmGJ_CAN_485";
            this.Load += new System.EventHandler(this.FrmGJ_CAN_485_Load);
            this.spcSys.Panel1.ResumeLayout(false);
            this.spcSys.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcSys)).EndInit();
            this.spcSys.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CANView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer spcSys;
        private System.Windows.Forms.GroupBox groupBox1;
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtModelNo;
        private System.Windows.Forms.Button txtWriteCanBaud;
        private System.Windows.Forms.Button btnReadCanBaud;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCanBaud;
        private System.Windows.Forms.Button btnWriteBaud;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCanDataNum;
        private System.Windows.Forms.Button btnGetCanNum;
        private System.Windows.Forms.Button btnClearCanNum;
        private System.Windows.Forms.Button btnWriteModelNo;
        private System.Windows.Forms.Button btnReadModelNo;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView CANView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtCanData;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSendCyc;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnGetAdrs;
        private System.Windows.Forms.Button btnSetAdrs;

    }
}