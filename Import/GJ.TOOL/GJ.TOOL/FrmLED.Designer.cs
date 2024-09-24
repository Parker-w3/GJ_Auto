namespace GJ.TOOL
{
    partial class FrmLED
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLED));
            this.DevView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtStartAddr = new System.Windows.Forms.TextBox();
            this.txtEndAddr = new System.Windows.Forms.TextBox();
            this.btnScan = new System.Windows.Forms.Button();
            this.labStatus = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.cmbCom = new System.Windows.Forms.ComboBox();
            this.txtBaud = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAddr = new System.Windows.Forms.TextBox();
            this.btnSetAddr = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labVersion = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbQCV = new System.Windows.Forms.ComboBox();
            this.btnSetLoad = new System.Windows.Forms.Button();
            this.btnReadLoad = new System.Windows.Forms.Button();
            this.btnVer = new System.Windows.Forms.Button();
            this.btnReadData = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label14 = new System.Windows.Forms.Label();
            this.txtCH = new System.Windows.Forms.TextBox();
            this.btnSetCH = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DevView)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // DevView
            // 
            this.DevView.AllowUserToAddRows = false;
            this.DevView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DevView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DevView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DevView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DevView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DevView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
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
            this.DevView.DefaultCellStyle = dataGridViewCellStyle3;
            this.DevView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevView.Location = new System.Drawing.Point(426, 3);
            this.DevView.Name = "DevView";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DevView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DevView.RowHeadersWidth = 20;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DevView.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.DevView.RowTemplate.Height = 23;
            this.DevView.Size = new System.Drawing.Size(579, 629);
            this.DevView.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "地址";
            this.Column1.Name = "Column1";
            this.Column1.Width = 70;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "结果";
            this.Column2.Name = "Column2";
            this.Column2.Width = 80;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "版本";
            this.Column3.Name = "Column3";
            this.Column3.Width = 80;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "状态";
            this.Column4.Name = "Column4";
            this.Column4.Width = 500;
            // 
            // panel2
            // 
            this.panel2.ColumnCount = 2;
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 423F));
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel2.Controls.Add(this.panel3, 0, 0);
            this.panel2.Controls.Add(this.DevView, 1, 0);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.RowCount = 1;
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel2.Size = new System.Drawing.Size(1008, 635);
            this.panel2.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.panel3.ColumnCount = 6;
            this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.panel3.Controls.Add(this.label6, 0, 0);
            this.panel3.Controls.Add(this.label7, 2, 0);
            this.panel3.Controls.Add(this.label11, 1, 0);
            this.panel3.Controls.Add(this.label12, 3, 0);
            this.panel3.Controls.Add(this.label8, 4, 0);
            this.panel3.Controls.Add(this.label15, 5, 0);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.RowCount = 12;
            this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel3.Size = new System.Drawing.Size(417, 629);
            this.panel3.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(4, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 24);
            this.label6.TabIndex = 1;
            this.label6.Text = "通道";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(136, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 24);
            this.label7.TabIndex = 2;
            this.label7.Text = "Von值(V)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(65, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 24);
            this.label11.TabIndex = 4;
            this.label11.Text = "模式";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(207, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 24);
            this.label12.TabIndex = 5;
            this.label12.Text = "设定值(A)";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(278, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 24);
            this.label8.TabIndex = 6;
            this.label8.Text = "附加模式";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(349, 1);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(64, 24);
            this.label15.TabIndex = 7;
            this.label15.Text = "读值(A)";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 29);
            this.label4.TabIndex = 0;
            this.label4.Text = "开始地址:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.ColumnCount = 6;
            this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel4.Controls.Add(this.label4, 0, 0);
            this.panel4.Controls.Add(this.label5, 2, 0);
            this.panel4.Controls.Add(this.txtStartAddr, 1, 0);
            this.panel4.Controls.Add(this.txtEndAddr, 3, 0);
            this.panel4.Controls.Add(this.btnScan, 4, 0);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(550, 61);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.RowCount = 1;
            this.panel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel4.Size = new System.Drawing.Size(457, 29);
            this.panel4.TabIndex = 33;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(163, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 29);
            this.label5.TabIndex = 1;
            this.label5.Text = "结束地址:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtStartAddr
            // 
            this.txtStartAddr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStartAddr.Location = new System.Drawing.Point(83, 3);
            this.txtStartAddr.Name = "txtStartAddr";
            this.txtStartAddr.Size = new System.Drawing.Size(74, 21);
            this.txtStartAddr.TabIndex = 2;
            this.txtStartAddr.Text = "1";
            this.txtStartAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtEndAddr
            // 
            this.txtEndAddr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEndAddr.Location = new System.Drawing.Point(243, 3);
            this.txtEndAddr.Name = "txtEndAddr";
            this.txtEndAddr.Size = new System.Drawing.Size(74, 21);
            this.txtEndAddr.TabIndex = 3;
            this.txtEndAddr.Text = "40";
            this.txtEndAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnScan
            // 
            this.btnScan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnScan.Location = new System.Drawing.Point(320, 0);
            this.btnScan.Margin = new System.Windows.Forms.Padding(0);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(72, 29);
            this.btnScan.TabIndex = 4;
            this.btnScan.Text = "开始扫描";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // labStatus
            // 
            this.labStatus.AutoSize = true;
            this.labStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.labStatus.Location = new System.Drawing.Point(553, 1);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(0, 29);
            this.labStatus.TabIndex = 9;
            this.labStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label29.Location = new System.Drawing.Point(4, 1);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(61, 29);
            this.label29.TabIndex = 18;
            this.label29.Text = "串口号:";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbCom
            // 
            this.cmbCom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCom.FormattingEnabled = true;
            this.cmbCom.Location = new System.Drawing.Point(72, 4);
            this.cmbCom.Name = "cmbCom";
            this.cmbCom.Size = new System.Drawing.Size(114, 20);
            this.cmbCom.TabIndex = 19;
            // 
            // txtBaud
            // 
            this.txtBaud.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBaud.Location = new System.Drawing.Point(72, 64);
            this.txtBaud.Name = "txtBaud";
            this.txtBaud.Size = new System.Drawing.Size(114, 21);
            this.txtBaud.TabIndex = 21;
            this.txtBaud.Text = "9600,n,8,1";
            this.txtBaud.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(1008, 730);
            this.splitContainer1.SplitterDistance = 91;
            this.splitContainer1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.panel1.ColumnCount = 8;
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 63F));
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel1.Controls.Add(this.labStatus, 7, 0);
            this.panel1.Controls.Add(this.label29, 0, 0);
            this.panel1.Controls.Add(this.cmbCom, 1, 0);
            this.panel1.Controls.Add(this.label1, 0, 2);
            this.panel1.Controls.Add(this.txtBaud, 1, 2);
            this.panel1.Controls.Add(this.btnOpen, 2, 0);
            this.panel1.Controls.Add(this.label3, 3, 0);
            this.panel1.Controls.Add(this.txtAddr, 4, 0);
            this.panel1.Controls.Add(this.btnSetAddr, 5, 0);
            this.panel1.Controls.Add(this.label9, 6, 0);
            this.panel1.Controls.Add(this.panel4, 7, 2);
            this.panel1.Controls.Add(this.label10, 0, 1);
            this.panel1.Controls.Add(this.cmbType, 1, 1);
            this.panel1.Controls.Add(this.label2, 3, 1);
            this.panel1.Controls.Add(this.labVersion, 4, 1);
            this.panel1.Controls.Add(this.label13, 3, 2);
            this.panel1.Controls.Add(this.cmbQCV, 4, 2);
            this.panel1.Controls.Add(this.btnSetLoad, 5, 2);
            this.panel1.Controls.Add(this.btnReadLoad, 6, 2);
            this.panel1.Controls.Add(this.btnVer, 5, 1);
            this.panel1.Controls.Add(this.btnReadData, 6, 1);
            this.panel1.Controls.Add(this.panel5, 7, 1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.RowCount = 3;
            this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.panel1.Size = new System.Drawing.Size(1008, 91);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(4, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 29);
            this.label1.TabIndex = 20;
            this.label1.Text = "波特率:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOpen
            // 
            this.btnOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpen.Location = new System.Drawing.Point(190, 1);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(0);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(62, 29);
            this.btnOpen.TabIndex = 22;
            this.btnOpen.Text = "打开";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(256, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 29);
            this.label3.TabIndex = 23;
            this.label3.Text = "模块地址:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAddr
            // 
            this.txtAddr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAddr.Location = new System.Drawing.Point(332, 4);
            this.txtAddr.Name = "txtAddr";
            this.txtAddr.Size = new System.Drawing.Size(84, 21);
            this.txtAddr.TabIndex = 24;
            this.txtAddr.Text = "1";
            this.txtAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSetAddr
            // 
            this.btnSetAddr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSetAddr.Location = new System.Drawing.Point(420, 1);
            this.btnSetAddr.Margin = new System.Windows.Forms.Padding(0);
            this.btnSetAddr.Name = "btnSetAddr";
            this.btnSetAddr.Size = new System.Drawing.Size(63, 29);
            this.btnSetAddr.TabIndex = 25;
            this.btnSetAddr.Text = "设置地址";
            this.btnSetAddr.UseVisualStyleBackColor = true;
            this.btnSetAddr.Click += new System.EventHandler(this.btnSetAddr_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(487, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 29);
            this.label9.TabIndex = 28;
            this.label9.Text = "状态指示:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(4, 31);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 29);
            this.label10.TabIndex = 36;
            this.label10.Text = "模块类型:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbType
            // 
            this.cmbType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "DA_320_8",
            "DA_750_4",
            "DA_750_4_60A",
            "DA3300_V1"});
            this.cmbType.Location = new System.Drawing.Point(72, 34);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(114, 20);
            this.cmbType.TabIndex = 37;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(256, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 29);
            this.label2.TabIndex = 38;
            this.label2.Text = "模块版本:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labVersion
            // 
            this.labVersion.AutoSize = true;
            this.labVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labVersion.Location = new System.Drawing.Point(332, 31);
            this.labVersion.Name = "labVersion";
            this.labVersion.Size = new System.Drawing.Size(84, 29);
            this.labVersion.TabIndex = 39;
            this.labVersion.Text = "---";
            this.labVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(256, 61);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(69, 29);
            this.label13.TabIndex = 40;
            this.label13.Text = "快充电压:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbQCV
            // 
            this.cmbQCV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbQCV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQCV.FormattingEnabled = true;
            this.cmbQCV.Items.AddRange(new object[] {
            "7V",
            "8V",
            "9V",
            "12V"});
            this.cmbQCV.Location = new System.Drawing.Point(332, 64);
            this.cmbQCV.Name = "cmbQCV";
            this.cmbQCV.Size = new System.Drawing.Size(84, 20);
            this.cmbQCV.TabIndex = 41;
            // 
            // btnSetLoad
            // 
            this.btnSetLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSetLoad.Location = new System.Drawing.Point(420, 61);
            this.btnSetLoad.Margin = new System.Windows.Forms.Padding(0);
            this.btnSetLoad.Name = "btnSetLoad";
            this.btnSetLoad.Size = new System.Drawing.Size(63, 29);
            this.btnSetLoad.TabIndex = 42;
            this.btnSetLoad.Text = "设负载";
            this.btnSetLoad.UseVisualStyleBackColor = true;
            this.btnSetLoad.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnReadLoad
            // 
            this.btnReadLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReadLoad.Location = new System.Drawing.Point(484, 61);
            this.btnReadLoad.Margin = new System.Windows.Forms.Padding(0);
            this.btnReadLoad.Name = "btnReadLoad";
            this.btnReadLoad.Size = new System.Drawing.Size(65, 29);
            this.btnReadLoad.TabIndex = 43;
            this.btnReadLoad.Text = "读负载";
            this.btnReadLoad.UseVisualStyleBackColor = true;
            this.btnReadLoad.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnVer
            // 
            this.btnVer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnVer.Location = new System.Drawing.Point(420, 31);
            this.btnVer.Margin = new System.Windows.Forms.Padding(0);
            this.btnVer.Name = "btnVer";
            this.btnVer.Size = new System.Drawing.Size(63, 29);
            this.btnVer.TabIndex = 44;
            this.btnVer.Text = "读版本";
            this.btnVer.UseVisualStyleBackColor = true;
            this.btnVer.Click += new System.EventHandler(this.btnVer_Click);
            // 
            // btnReadData
            // 
            this.btnReadData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReadData.Location = new System.Drawing.Point(484, 31);
            this.btnReadData.Margin = new System.Windows.Forms.Padding(0);
            this.btnReadData.Name = "btnReadData";
            this.btnReadData.Size = new System.Drawing.Size(65, 29);
            this.btnReadData.TabIndex = 45;
            this.btnReadData.Text = "读数据";
            this.btnReadData.UseVisualStyleBackColor = true;
            this.btnReadData.Click += new System.EventHandler(this.btnReadData_Click);
            // 
            // panel5
            // 
            this.panel5.ColumnCount = 4;
            this.panel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.panel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.panel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.panel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel5.Controls.Add(this.label14, 0, 0);
            this.panel5.Controls.Add(this.txtCH, 1, 0);
            this.panel5.Controls.Add(this.btnSetCH, 2, 0);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(553, 34);
            this.panel5.Name = "panel5";
            this.panel5.RowCount = 1;
            this.panel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel5.Size = new System.Drawing.Size(451, 23);
            this.panel5.TabIndex = 46;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Location = new System.Drawing.Point(3, 3);
            this.label14.Margin = new System.Windows.Forms.Padding(3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 17);
            this.label14.TabIndex = 0;
            this.label14.Text = "电流通道:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCH
            // 
            this.txtCH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCH.Location = new System.Drawing.Point(83, 3);
            this.txtCH.Name = "txtCH";
            this.txtCH.Size = new System.Drawing.Size(74, 21);
            this.txtCH.TabIndex = 1;
            this.txtCH.Text = "1";
            this.txtCH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSetCH
            // 
            this.btnSetCH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSetCH.Location = new System.Drawing.Point(160, 0);
            this.btnSetCH.Margin = new System.Windows.Forms.Padding(0);
            this.btnSetCH.Name = "btnSetCH";
            this.btnSetCH.Size = new System.Drawing.Size(85, 23);
            this.btnSetCH.TabIndex = 2;
            this.btnSetCH.Text = "设置单通道";
            this.btnSetCH.UseVisualStyleBackColor = true;
            this.btnSetCH.Click += new System.EventHandler(this.btnSetCH_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "L");
            this.imageList1.Images.SetKeyName(1, "H");
            this.imageList1.Images.SetKeyName(2, "F");
            // 
            // FrmLED
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmLED";
            this.Text = "FrmLED";
            this.Load += new System.EventHandler(this.FrmLED_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DevView)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DevView;
        private System.Windows.Forms.TableLayoutPanel panel2;
        private System.Windows.Forms.TableLayoutPanel panel3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel panel4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtStartAddr;
        private System.Windows.Forms.TextBox txtEndAddr;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Label labStatus;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ComboBox cmbCom;
        private System.Windows.Forms.TextBox txtBaud;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAddr;
        private System.Windows.Forms.Button btnSetAddr;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labVersion;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbQCV;
        private System.Windows.Forms.Button btnSetLoad;
        private System.Windows.Forms.Button btnReadLoad;
        private System.Windows.Forms.Button btnVer;
        private System.Windows.Forms.Button btnReadData;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.TableLayoutPanel panel5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtCH;
        private System.Windows.Forms.Button btnSetCH;
        private System.Windows.Forms.Label label15;
    }
}