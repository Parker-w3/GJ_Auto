namespace GJ.TOOL
{
    partial class FrmRemoteIO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRemoteIO));
            this.panel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tbCtlr = new System.Windows.Forms.TabControl();
            this.panel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbDevType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtStartAddr = new System.Windows.Forms.TextBox();
            this.txtLen = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtData = new System.Windows.Forms.TextBox();
            this.labRtn = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbVal = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.txtSetAddr = new System.Windows.Forms.TextBox();
            this.btnSetAddr = new System.Windows.Forms.Button();
            this.btnReadAddr = new System.Windows.Forms.Button();
            this.txtSetBaud = new System.Windows.Forms.TextBox();
            this.btnSetBaud = new System.Windows.Forms.Button();
            this.btnReadBaud = new System.Windows.Forms.Button();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.txtSoftVer = new System.Windows.Forms.TextBox();
            this.txtErrCode = new System.Windows.Forms.TextBox();
            this.btnReadVer = new System.Windows.Forms.Button();
            this.btnReadErr = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.TableLayoutPanel();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.labPlcDB = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.labStatus = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label29 = new System.Windows.Forms.Label();
            this.cmbCom = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBaud = new System.Windows.Forms.TextBox();
            this.btnCon = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAddr = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.panel2.ColumnCount = 1;
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel2.Controls.Add(this.tbCtlr, 0, 4);
            this.panel2.Controls.Add(this.panel4, 0, 2);
            this.panel2.Controls.Add(this.panel3, 0, 1);
            this.panel2.Controls.Add(this.panel5, 0, 0);
            this.panel2.Controls.Add(this.panel6, 0, 3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.RowCount = 5;
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel2.Size = new System.Drawing.Size(761, 530);
            this.panel2.TabIndex = 0;
            // 
            // tbCtlr
            // 
            this.tbCtlr.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tbCtlr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCtlr.Location = new System.Drawing.Point(4, 220);
            this.tbCtlr.Name = "tbCtlr";
            this.tbCtlr.SelectedIndex = 0;
            this.tbCtlr.Size = new System.Drawing.Size(753, 306);
            this.tbCtlr.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tbCtlr.TabIndex = 6;
            // 
            // panel4
            // 
            this.panel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.panel4.ColumnCount = 8;
            this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel4.Controls.Add(this.label5, 0, 0);
            this.panel4.Controls.Add(this.cmbDevType, 1, 0);
            this.panel4.Controls.Add(this.label6, 2, 0);
            this.panel4.Controls.Add(this.label7, 4, 0);
            this.panel4.Controls.Add(this.txtStartAddr, 3, 0);
            this.panel4.Controls.Add(this.txtLen, 5, 0);
            this.panel4.Controls.Add(this.label8, 6, 0);
            this.panel4.Controls.Add(this.label9, 6, 1);
            this.panel4.Controls.Add(this.txtData, 7, 0);
            this.panel4.Controls.Add(this.labRtn, 7, 1);
            this.panel4.Controls.Add(this.label10, 4, 1);
            this.panel4.Controls.Add(this.cmbVal, 5, 1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(4, 108);
            this.panel4.Name = "panel4";
            this.panel4.RowCount = 2;
            this.panel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.62712F));
            this.panel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.37288F));
            this.panel4.Size = new System.Drawing.Size(753, 72);
            this.panel4.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(4, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 39);
            this.label5.TabIndex = 0;
            this.label5.Text = "地址类型:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbDevType
            // 
            this.cmbDevType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbDevType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbDevType.FormattingEnabled = true;
            this.cmbDevType.Location = new System.Drawing.Point(75, 4);
            this.cmbDevType.Name = "cmbDevType";
            this.cmbDevType.Size = new System.Drawing.Size(64, 22);
            this.cmbDevType.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(146, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 39);
            this.label6.TabIndex = 2;
            this.label6.Text = "开始地址:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(288, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 39);
            this.label7.TabIndex = 3;
            this.label7.Text = "地址长度:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtStartAddr
            // 
            this.txtStartAddr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStartAddr.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStartAddr.Location = new System.Drawing.Point(217, 4);
            this.txtStartAddr.Name = "txtStartAddr";
            this.txtStartAddr.Size = new System.Drawing.Size(64, 26);
            this.txtStartAddr.TabIndex = 4;
            this.txtStartAddr.Text = "0";
            this.txtStartAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtLen
            // 
            this.txtLen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLen.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLen.Location = new System.Drawing.Point(359, 4);
            this.txtLen.Name = "txtLen";
            this.txtLen.Size = new System.Drawing.Size(84, 26);
            this.txtLen.TabIndex = 5;
            this.txtLen.Text = "8";
            this.txtLen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(450, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 39);
            this.label8.TabIndex = 6;
            this.label8.Text = "写数据:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(450, 41);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 30);
            this.label9.TabIndex = 7;
            this.label9.Text = "读字符:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtData
            // 
            this.txtData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtData.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtData.Location = new System.Drawing.Point(522, 4);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(227, 26);
            this.txtData.TabIndex = 8;
            this.txtData.Text = "0";
            // 
            // labRtn
            // 
            this.labRtn.AutoSize = true;
            this.labRtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labRtn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labRtn.Location = new System.Drawing.Point(522, 41);
            this.labRtn.Name = "labRtn";
            this.labRtn.Size = new System.Drawing.Size(227, 30);
            this.labRtn.TabIndex = 9;
            this.labRtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(288, 41);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 30);
            this.label10.TabIndex = 10;
            this.label10.Text = "读数值:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbVal
            // 
            this.cmbVal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbVal.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbVal.FormattingEnabled = true;
            this.cmbVal.Location = new System.Drawing.Point(359, 44);
            this.cmbVal.Name = "cmbVal";
            this.cmbVal.Size = new System.Drawing.Size(84, 24);
            this.cmbVal.TabIndex = 11;
            // 
            // panel3
            // 
            this.panel3.ColumnCount = 3;
            this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.panel3.Controls.Add(this.label4, 0, 0);
            this.panel3.Controls.Add(this.btnRead, 2, 0);
            this.panel3.Controls.Add(this.btnWrite, 1, 0);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(1, 72);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.RowCount = 1;
            this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel3.Size = new System.Drawing.Size(759, 32);
            this.panel3.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(593, 26);
            this.label4.TabIndex = 1;
            this.label4.Text = "功能:PLC地址操作:( 多写数据以分号隔开:10进制值) 备注:X24/Y16超范围读写不返回数据";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnRead
            // 
            this.btnRead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRead.Location = new System.Drawing.Point(679, 0);
            this.btnRead.Margin = new System.Windows.Forms.Padding(0);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(80, 32);
            this.btnRead.TabIndex = 2;
            this.btnRead.Text = "读取";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnWrite
            // 
            this.btnWrite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnWrite.Location = new System.Drawing.Point(599, 0);
            this.btnWrite.Margin = new System.Windows.Forms.Padding(0);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(80, 32);
            this.btnWrite.TabIndex = 3;
            this.btnWrite.Text = "写入";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // panel5
            // 
            this.panel5.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.panel5.ColumnCount = 8;
            this.panel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.panel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.panel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.panel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.panel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.panel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97F));
            this.panel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.panel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel5.Controls.Add(this.label11, 0, 0);
            this.panel5.Controls.Add(this.label30, 0, 1);
            this.panel5.Controls.Add(this.txtSetAddr, 1, 0);
            this.panel5.Controls.Add(this.btnSetAddr, 2, 0);
            this.panel5.Controls.Add(this.btnReadAddr, 3, 0);
            this.panel5.Controls.Add(this.txtSetBaud, 1, 1);
            this.panel5.Controls.Add(this.btnSetBaud, 2, 1);
            this.panel5.Controls.Add(this.btnReadBaud, 3, 1);
            this.panel5.Controls.Add(this.label31, 4, 0);
            this.panel5.Controls.Add(this.label32, 4, 1);
            this.panel5.Controls.Add(this.txtSoftVer, 5, 0);
            this.panel5.Controls.Add(this.txtErrCode, 5, 1);
            this.panel5.Controls.Add(this.btnReadVer, 6, 0);
            this.panel5.Controls.Add(this.btnReadErr, 6, 1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(4, 4);
            this.panel5.Name = "panel5";
            this.panel5.RowCount = 2;
            this.panel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel5.Size = new System.Drawing.Size(753, 64);
            this.panel5.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(4, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 30);
            this.label11.TabIndex = 0;
            this.label11.Text = "IO地址:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label30.Location = new System.Drawing.Point(4, 32);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(67, 31);
            this.label30.TabIndex = 1;
            this.label30.Text = "波特率:";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSetAddr
            // 
            this.txtSetAddr.Location = new System.Drawing.Point(78, 4);
            this.txtSetAddr.Name = "txtSetAddr";
            this.txtSetAddr.Size = new System.Drawing.Size(79, 21);
            this.txtSetAddr.TabIndex = 2;
            this.txtSetAddr.Text = "1";
            this.txtSetAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSetAddr
            // 
            this.btnSetAddr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSetAddr.Location = new System.Drawing.Point(161, 1);
            this.btnSetAddr.Margin = new System.Windows.Forms.Padding(0);
            this.btnSetAddr.Name = "btnSetAddr";
            this.btnSetAddr.Size = new System.Drawing.Size(60, 30);
            this.btnSetAddr.TabIndex = 3;
            this.btnSetAddr.Text = "设置";
            this.btnSetAddr.UseVisualStyleBackColor = true;
            this.btnSetAddr.Click += new System.EventHandler(this.btnSetAddr_Click);
            // 
            // btnReadAddr
            // 
            this.btnReadAddr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReadAddr.Location = new System.Drawing.Point(222, 1);
            this.btnReadAddr.Margin = new System.Windows.Forms.Padding(0);
            this.btnReadAddr.Name = "btnReadAddr";
            this.btnReadAddr.Size = new System.Drawing.Size(60, 30);
            this.btnReadAddr.TabIndex = 4;
            this.btnReadAddr.Text = "读取";
            this.btnReadAddr.UseVisualStyleBackColor = true;
            this.btnReadAddr.Click += new System.EventHandler(this.btnReadAddr_Click);
            // 
            // txtSetBaud
            // 
            this.txtSetBaud.Location = new System.Drawing.Point(78, 35);
            this.txtSetBaud.Name = "txtSetBaud";
            this.txtSetBaud.Size = new System.Drawing.Size(79, 21);
            this.txtSetBaud.TabIndex = 5;
            this.txtSetBaud.Text = "115200";
            this.txtSetBaud.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSetBaud
            // 
            this.btnSetBaud.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSetBaud.Location = new System.Drawing.Point(161, 32);
            this.btnSetBaud.Margin = new System.Windows.Forms.Padding(0);
            this.btnSetBaud.Name = "btnSetBaud";
            this.btnSetBaud.Size = new System.Drawing.Size(60, 31);
            this.btnSetBaud.TabIndex = 6;
            this.btnSetBaud.Text = "设置";
            this.btnSetBaud.UseVisualStyleBackColor = true;
            this.btnSetBaud.Click += new System.EventHandler(this.btnSetBaud_Click);
            // 
            // btnReadBaud
            // 
            this.btnReadBaud.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReadBaud.Location = new System.Drawing.Point(222, 32);
            this.btnReadBaud.Margin = new System.Windows.Forms.Padding(0);
            this.btnReadBaud.Name = "btnReadBaud";
            this.btnReadBaud.Size = new System.Drawing.Size(60, 31);
            this.btnReadBaud.TabIndex = 7;
            this.btnReadBaud.Text = "读取";
            this.btnReadBaud.UseVisualStyleBackColor = true;
            this.btnReadBaud.Click += new System.EventHandler(this.btnReadBaud_Click);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label31.Location = new System.Drawing.Point(286, 1);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(74, 30);
            this.label31.TabIndex = 8;
            this.label31.Text = "软件版本:";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label32.Location = new System.Drawing.Point(286, 32);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(74, 31);
            this.label32.TabIndex = 9;
            this.label32.Text = "错误代码:";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSoftVer
            // 
            this.txtSoftVer.Location = new System.Drawing.Point(367, 4);
            this.txtSoftVer.Name = "txtSoftVer";
            this.txtSoftVer.Size = new System.Drawing.Size(91, 21);
            this.txtSoftVer.TabIndex = 10;
            this.txtSoftVer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtErrCode
            // 
            this.txtErrCode.Location = new System.Drawing.Point(367, 35);
            this.txtErrCode.Name = "txtErrCode";
            this.txtErrCode.Size = new System.Drawing.Size(91, 21);
            this.txtErrCode.TabIndex = 11;
            this.txtErrCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnReadVer
            // 
            this.btnReadVer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReadVer.Location = new System.Drawing.Point(462, 1);
            this.btnReadVer.Margin = new System.Windows.Forms.Padding(0);
            this.btnReadVer.Name = "btnReadVer";
            this.btnReadVer.Size = new System.Drawing.Size(60, 30);
            this.btnReadVer.TabIndex = 12;
            this.btnReadVer.Text = "读取";
            this.btnReadVer.UseVisualStyleBackColor = true;
            this.btnReadVer.Click += new System.EventHandler(this.btnReadVer_Click);
            // 
            // btnReadErr
            // 
            this.btnReadErr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReadErr.Location = new System.Drawing.Point(462, 32);
            this.btnReadErr.Margin = new System.Windows.Forms.Padding(0);
            this.btnReadErr.Name = "btnReadErr";
            this.btnReadErr.Size = new System.Drawing.Size(60, 31);
            this.btnReadErr.TabIndex = 13;
            this.btnReadErr.Text = "读取";
            this.btnReadErr.UseVisualStyleBackColor = true;
            this.btnReadErr.Click += new System.EventHandler(this.btnReadErr_Click);
            // 
            // panel6
            // 
            this.panel6.ColumnCount = 4;
            this.panel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.panel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.panel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.panel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel6.Controls.Add(this.btnRun, 3, 0);
            this.panel6.Controls.Add(this.btnLoad, 2, 0);
            this.panel6.Controls.Add(this.labPlcDB, 1, 0);
            this.panel6.Controls.Add(this.label33, 0, 0);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(4, 187);
            this.panel6.Name = "panel6";
            this.panel6.RowCount = 1;
            this.panel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel6.Size = new System.Drawing.Size(753, 26);
            this.panel6.TabIndex = 5;
            // 
            // btnRun
            // 
            this.btnRun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRun.Location = new System.Drawing.Point(693, 0);
            this.btnRun.Margin = new System.Windows.Forms.Padding(0);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(60, 26);
            this.btnRun.TabIndex = 6;
            this.btnRun.Text = "启动";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLoad.Location = new System.Drawing.Point(633, 0);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(0);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(60, 26);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text = "加载";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // labPlcDB
            // 
            this.labPlcDB.AutoSize = true;
            this.labPlcDB.BackColor = System.Drawing.Color.White;
            this.labPlcDB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labPlcDB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labPlcDB.Location = new System.Drawing.Point(123, 3);
            this.labPlcDB.Margin = new System.Windows.Forms.Padding(3);
            this.labPlcDB.Name = "labPlcDB";
            this.labPlcDB.Size = new System.Drawing.Size(507, 20);
            this.labPlcDB.TabIndex = 4;
            this.labPlcDB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label33.Location = new System.Drawing.Point(3, 3);
            this.label33.Margin = new System.Windows.Forms.Padding(3);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(114, 20);
            this.label33.TabIndex = 0;
            this.label33.Text = "IO监控数据库路径:";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "L");
            this.imageList1.Images.SetKeyName(1, "H");
            this.imageList1.Images.SetKeyName(2, "F");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(432, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 28);
            this.label2.TabIndex = 8;
            this.label2.Text = "状态指示:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labStatus
            // 
            this.labStatus.AutoSize = true;
            this.labStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.labStatus.Location = new System.Drawing.Point(503, 1);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(0, 28);
            this.labStatus.TabIndex = 9;
            this.labStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.panel1.ColumnCount = 7;
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 704F));
            this.panel1.Controls.Add(this.label2, 5, 0);
            this.panel1.Controls.Add(this.labStatus, 6, 0);
            this.panel1.Controls.Add(this.label29, 0, 0);
            this.panel1.Controls.Add(this.cmbCom, 1, 0);
            this.panel1.Controls.Add(this.label1, 0, 1);
            this.panel1.Controls.Add(this.txtBaud, 1, 1);
            this.panel1.Controls.Add(this.btnCon, 2, 0);
            this.panel1.Controls.Add(this.label3, 3, 0);
            this.panel1.Controls.Add(this.txtAddr, 4, 0);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.RowCount = 2;
            this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panel1.Size = new System.Drawing.Size(761, 60);
            this.panel1.TabIndex = 1;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label29.Location = new System.Drawing.Point(4, 1);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(71, 28);
            this.label29.TabIndex = 18;
            this.label29.Text = "串口号:";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbCom
            // 
            this.cmbCom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCom.FormattingEnabled = true;
            this.cmbCom.Location = new System.Drawing.Point(82, 4);
            this.cmbCom.Name = "cmbCom";
            this.cmbCom.Size = new System.Drawing.Size(110, 20);
            this.cmbCom.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(4, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 29);
            this.label1.TabIndex = 20;
            this.label1.Text = "波特率:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBaud
            // 
            this.txtBaud.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBaud.Location = new System.Drawing.Point(82, 33);
            this.txtBaud.Name = "txtBaud";
            this.txtBaud.Size = new System.Drawing.Size(110, 21);
            this.txtBaud.TabIndex = 21;
            this.txtBaud.Text = "115200,n,8,1";
            this.txtBaud.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnCon
            // 
            this.btnCon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCon.Location = new System.Drawing.Point(196, 1);
            this.btnCon.Margin = new System.Windows.Forms.Padding(0);
            this.btnCon.Name = "btnCon";
            this.btnCon.Size = new System.Drawing.Size(67, 28);
            this.btnCon.TabIndex = 22;
            this.btnCon.Text = "连接";
            this.btnCon.UseVisualStyleBackColor = true;
            this.btnCon.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(267, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 28);
            this.label3.TabIndex = 23;
            this.label3.Text = "IO地址:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAddr
            // 
            this.txtAddr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAddr.Location = new System.Drawing.Point(346, 4);
            this.txtAddr.Name = "txtAddr";
            this.txtAddr.Size = new System.Drawing.Size(79, 21);
            this.txtAddr.TabIndex = 24;
            this.txtAddr.Text = "1";
            this.txtAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.splitContainer1.Size = new System.Drawing.Size(761, 594);
            this.splitContainer1.SplitterDistance = 60;
            this.splitContainer1.TabIndex = 3;
            // 
            // FrmRemoteIO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 594);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRemoteIO";
            this.Text = "FrmRemoteIO";
            this.Load += new System.EventHandler(this.FrmRemoteIO_Load);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel panel2;
        private System.Windows.Forms.TableLayoutPanel panel4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbDevType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtStartAddr;
        private System.Windows.Forms.TextBox txtLen;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.Label labRtn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbVal;
        private System.Windows.Forms.TableLayoutPanel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.TableLayoutPanel panel5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txtSetAddr;
        private System.Windows.Forms.Button btnSetAddr;
        private System.Windows.Forms.Button btnReadAddr;
        private System.Windows.Forms.TextBox txtSetBaud;
        private System.Windows.Forms.Button btnSetBaud;
        private System.Windows.Forms.Button btnReadBaud;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txtSoftVer;
        private System.Windows.Forms.TextBox txtErrCode;
        private System.Windows.Forms.Button btnReadVer;
        private System.Windows.Forms.Button btnReadErr;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labStatus;
        private System.Windows.Forms.TableLayoutPanel panel1;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ComboBox cmbCom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBaud;
        private System.Windows.Forms.Button btnCon;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAddr;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel panel6;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label labPlcDB;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.TabControl tbCtlr;
    }
}