namespace GJ.TOOL
{
    partial class FrmBarCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBarCode));
            this.panel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCOM = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.labStatus = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbBarType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBand = new System.Windows.Forms.TextBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnTriger = new System.Windows.Forms.Button();
            this.btnClr = new System.Windows.Forms.Button();
            this.labLen = new System.Windows.Forms.Label();
            this.labTT = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDelayTimes = new System.Windows.Forms.TextBox();
            this.labTimes = new System.Windows.Forms.Label();
            this.labSn = new System.Windows.Forms.Label();
            this.runLog = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.panel1.ColumnCount = 1;
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel1.Controls.Add(this.panel2, 0, 0);
            this.panel1.Controls.Add(this.labSn, 0, 1);
            this.panel1.Controls.Add(this.runLog, 0, 2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.RowCount = 3;
            this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel1.Size = new System.Drawing.Size(688, 482);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.panel2.ColumnCount = 5;
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 123F));
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel2.Controls.Add(this.label2, 0, 1);
            this.panel2.Controls.Add(this.cmbCOM, 1, 1);
            this.panel2.Controls.Add(this.label3, 3, 1);
            this.panel2.Controls.Add(this.btnOpen, 2, 1);
            this.panel2.Controls.Add(this.labStatus, 4, 1);
            this.panel2.Controls.Add(this.label4, 0, 0);
            this.panel2.Controls.Add(this.cmbBarType, 1, 0);
            this.panel2.Controls.Add(this.label5, 0, 2);
            this.panel2.Controls.Add(this.txtBand, 1, 2);
            this.panel2.Controls.Add(this.btnRead, 2, 2);
            this.panel2.Controls.Add(this.btnTriger, 2, 3);
            this.panel2.Controls.Add(this.btnClr, 3, 3);
            this.panel2.Controls.Add(this.labLen, 3, 2);
            this.panel2.Controls.Add(this.labTT, 4, 3);
            this.panel2.Controls.Add(this.label1, 0, 3);
            this.panel2.Controls.Add(this.txtDelayTimes, 1, 3);
            this.panel2.Controls.Add(this.labTimes, 4, 2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(5, 5);
            this.panel2.Name = "panel2";
            this.panel2.RowCount = 4;
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.99813F));
            this.panel2.Size = new System.Drawing.Size(678, 134);
            this.panel2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(4, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "串口编号:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbCOM
            // 
            this.cmbCOM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCOM.FormattingEnabled = true;
            this.cmbCOM.Location = new System.Drawing.Point(89, 37);
            this.cmbCOM.Name = "cmbCOM";
            this.cmbCOM.Size = new System.Drawing.Size(117, 20);
            this.cmbCOM.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(281, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 32);
            this.label3.TabIndex = 4;
            this.label3.Text = "状态指示:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnOpen
            // 
            this.btnOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpen.Location = new System.Drawing.Point(210, 34);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(0);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(67, 32);
            this.btnOpen.TabIndex = 5;
            this.btnOpen.Text = "打开";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // labStatus
            // 
            this.labStatus.AutoSize = true;
            this.labStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labStatus.Location = new System.Drawing.Point(347, 37);
            this.labStatus.Margin = new System.Windows.Forms.Padding(3);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(327, 26);
            this.labStatus.TabIndex = 6;
            this.labStatus.Text = "---";
            this.labStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(4, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 32);
            this.label4.TabIndex = 8;
            this.label4.Text = "条码枪型号:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbBarType
            // 
            this.cmbBarType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbBarType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBarType.FormattingEnabled = true;
            this.cmbBarType.Items.AddRange(new object[] {
            "CR1000",
            "KS100",
            "CR85"});
            this.cmbBarType.Location = new System.Drawing.Point(89, 4);
            this.cmbBarType.Name = "cmbBarType";
            this.cmbBarType.Size = new System.Drawing.Size(117, 20);
            this.cmbBarType.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(4, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 32);
            this.label5.TabIndex = 10;
            this.label5.Text = "波特率:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBand
            // 
            this.txtBand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBand.Location = new System.Drawing.Point(89, 70);
            this.txtBand.Name = "txtBand";
            this.txtBand.Size = new System.Drawing.Size(117, 21);
            this.txtBand.TabIndex = 11;
            this.txtBand.Text = "115200,N,8,1";
            this.txtBand.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnRead
            // 
            this.btnRead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRead.Location = new System.Drawing.Point(210, 67);
            this.btnRead.Margin = new System.Windows.Forms.Padding(0);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(67, 32);
            this.btnRead.TabIndex = 12;
            this.btnRead.Text = "读条码";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnTriger
            // 
            this.btnTriger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTriger.Location = new System.Drawing.Point(210, 100);
            this.btnTriger.Margin = new System.Windows.Forms.Padding(0);
            this.btnTriger.Name = "btnTriger";
            this.btnTriger.Size = new System.Drawing.Size(67, 33);
            this.btnTriger.TabIndex = 13;
            this.btnTriger.Text = "启动触发";
            this.btnTriger.UseVisualStyleBackColor = true;
            this.btnTriger.Click += new System.EventHandler(this.btnTriger_Click);
            // 
            // btnClr
            // 
            this.btnClr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClr.Location = new System.Drawing.Point(278, 100);
            this.btnClr.Margin = new System.Windows.Forms.Padding(0);
            this.btnClr.Name = "btnClr";
            this.btnClr.Size = new System.Drawing.Size(65, 33);
            this.btnClr.TabIndex = 14;
            this.btnClr.Text = "清除显示";
            this.btnClr.UseVisualStyleBackColor = true;
            this.btnClr.Click += new System.EventHandler(this.btn_Click);
            // 
            // labLen
            // 
            this.labLen.AutoSize = true;
            this.labLen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labLen.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labLen.Location = new System.Drawing.Point(281, 67);
            this.labLen.Name = "labLen";
            this.labLen.Size = new System.Drawing.Size(59, 32);
            this.labLen.TabIndex = 15;
            this.labLen.Text = "0";
            this.labLen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labTT
            // 
            this.labTT.AutoSize = true;
            this.labTT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labTT.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTT.Location = new System.Drawing.Point(347, 100);
            this.labTT.Name = "labTT";
            this.labTT.Size = new System.Drawing.Size(327, 33);
            this.labTT.TabIndex = 16;
            this.labTT.Text = "0";
            this.labTT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(1, 100);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 33);
            this.label1.TabIndex = 17;
            this.label1.Text = "延时时间(ms):";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDelayTimes
            // 
            this.txtDelayTimes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDelayTimes.Location = new System.Drawing.Point(89, 103);
            this.txtDelayTimes.Name = "txtDelayTimes";
            this.txtDelayTimes.Size = new System.Drawing.Size(117, 21);
            this.txtDelayTimes.TabIndex = 18;
            this.txtDelayTimes.Text = "1000";
            this.txtDelayTimes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labTimes
            // 
            this.labTimes.AutoSize = true;
            this.labTimes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labTimes.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTimes.Location = new System.Drawing.Point(347, 67);
            this.labTimes.Name = "labTimes";
            this.labTimes.Size = new System.Drawing.Size(327, 32);
            this.labTimes.TabIndex = 19;
            this.labTimes.Text = "0";
            this.labTimes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labSn
            // 
            this.labSn.AutoSize = true;
            this.labSn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labSn.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labSn.Location = new System.Drawing.Point(5, 144);
            this.labSn.Name = "labSn";
            this.labSn.Size = new System.Drawing.Size(678, 31);
            this.labSn.TabIndex = 1;
            this.labSn.Text = "---";
            this.labSn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // runLog
            // 
            this.runLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.runLog.Location = new System.Drawing.Point(5, 180);
            this.runLog.Name = "runLog";
            this.runLog.Size = new System.Drawing.Size(678, 297);
            this.runLog.TabIndex = 2;
            this.runLog.Text = "";
            // 
            // FrmBarCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 482);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmBarCode";
            this.Text = "FrmCR1000";
            this.Load += new System.EventHandler(this.FrmCR1000_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel panel1;
        private System.Windows.Forms.TableLayoutPanel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCOM;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label labStatus;
        private System.Windows.Forms.Label labSn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbBarType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBand;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnTriger;
        private System.Windows.Forms.Button btnClr;
        private System.Windows.Forms.Label labLen;
        private System.Windows.Forms.RichTextBox runLog;
        private System.Windows.Forms.Label labTT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDelayTimes;
        private System.Windows.Forms.Label labTimes;
    }
}