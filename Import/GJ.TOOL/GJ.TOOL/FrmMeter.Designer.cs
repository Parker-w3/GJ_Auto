namespace GJ.TOOL
{
    partial class FrmMeter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMeter));
            this.panel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCOM = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBand = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMutiAddr = new System.Windows.Forms.TextBox();
            this.labStatus = new System.Windows.Forms.Label();
            this.btnRead = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labVolt = new System.Windows.Forms.Label();
            this.labCurrent = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.panel1.ColumnCount = 1;
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel1.Controls.Add(this.panel2, 0, 0);
            this.panel1.Controls.Add(this.panel3, 0, 1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.RowCount = 3;
            this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 138F));
            this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 151F));
            this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel1.Size = new System.Drawing.Size(793, 489);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.panel2.ColumnCount = 5;
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 144F));
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel2.Controls.Add(this.label2, 0, 1);
            this.panel2.Controls.Add(this.cmbCOM, 1, 1);
            this.panel2.Controls.Add(this.label4, 0, 0);
            this.panel2.Controls.Add(this.cmbType, 1, 0);
            this.panel2.Controls.Add(this.label5, 0, 2);
            this.panel2.Controls.Add(this.txtBand, 1, 2);
            this.panel2.Controls.Add(this.label1, 0, 3);
            this.panel2.Controls.Add(this.btnOpen, 2, 0);
            this.panel2.Controls.Add(this.label3, 3, 0);
            this.panel2.Controls.Add(this.txtMutiAddr, 1, 3);
            this.panel2.Controls.Add(this.labStatus, 4, 0);
            this.panel2.Controls.Add(this.btnRead, 2, 3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(5, 5);
            this.panel2.Name = "panel2";
            this.panel2.RowCount = 4;
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.99813F));
            this.panel2.Size = new System.Drawing.Size(783, 132);
            this.panel2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(4, 36);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "串口编号:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbCOM
            // 
            this.cmbCOM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCOM.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCOM.FormattingEnabled = true;
            this.cmbCOM.Location = new System.Drawing.Point(78, 36);
            this.cmbCOM.Name = "cmbCOM";
            this.cmbCOM.Size = new System.Drawing.Size(138, 22);
            this.cmbCOM.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(4, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 31);
            this.label4.TabIndex = 8;
            this.label4.Text = "电表型号:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbType
            // 
            this.cmbType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "PRU80_R1_2A_AC"});
            this.cmbType.Location = new System.Drawing.Point(78, 4);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(138, 22);
            this.cmbType.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(4, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 31);
            this.label5.TabIndex = 10;
            this.label5.Text = "波特率:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBand
            // 
            this.txtBand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBand.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBand.Location = new System.Drawing.Point(78, 68);
            this.txtBand.Name = "txtBand";
            this.txtBand.Size = new System.Drawing.Size(138, 23);
            this.txtBand.TabIndex = 11;
            this.txtBand.Text = "9600,N,8,1";
            this.txtBand.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(4, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 34);
            this.label1.TabIndex = 13;
            this.label1.Text = "电表地址:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnOpen
            // 
            this.btnOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpen.Location = new System.Drawing.Point(220, 1);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(0);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(77, 31);
            this.btnOpen.TabIndex = 14;
            this.btnOpen.Text = "打开";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(301, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 31);
            this.label3.TabIndex = 15;
            this.label3.Text = "状态指示:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMutiAddr
            // 
            this.txtMutiAddr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMutiAddr.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMutiAddr.Location = new System.Drawing.Point(78, 100);
            this.txtMutiAddr.Name = "txtMutiAddr";
            this.txtMutiAddr.Size = new System.Drawing.Size(138, 23);
            this.txtMutiAddr.TabIndex = 16;
            this.txtMutiAddr.Text = "0";
            this.txtMutiAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labStatus
            // 
            this.labStatus.AutoSize = true;
            this.labStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labStatus.Location = new System.Drawing.Point(372, 1);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(407, 31);
            this.labStatus.TabIndex = 17;
            this.labStatus.Text = "---";
            this.labStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnRead
            // 
            this.btnRead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRead.Location = new System.Drawing.Point(220, 97);
            this.btnRead.Margin = new System.Windows.Forms.Padding(0);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(77, 34);
            this.btnRead.TabIndex = 18;
            this.btnRead.Text = "读取";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // panel3
            // 
            this.panel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.panel3.ColumnCount = 2;
            this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.14815F));
            this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.85185F));
            this.panel3.Controls.Add(this.label6, 0, 0);
            this.panel3.Controls.Add(this.label7, 1, 0);
            this.panel3.Controls.Add(this.labVolt, 0, 1);
            this.panel3.Controls.Add(this.labCurrent, 1, 1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(5, 145);
            this.panel3.Name = "panel3";
            this.panel3.RowCount = 2;
            this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel3.Size = new System.Drawing.Size(783, 145);
            this.panel3.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(4, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(369, 51);
            this.label6.TabIndex = 0;
            this.label6.Text = "电压值(V)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(380, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(399, 51);
            this.label7.TabIndex = 1;
            this.label7.Text = "电流值(A)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labVolt
            // 
            this.labVolt.AutoSize = true;
            this.labVolt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labVolt.Font = new System.Drawing.Font("宋体", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labVolt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labVolt.Location = new System.Drawing.Point(4, 53);
            this.labVolt.Name = "labVolt";
            this.labVolt.Size = new System.Drawing.Size(369, 91);
            this.labVolt.TabIndex = 2;
            this.labVolt.Text = "0.0";
            this.labVolt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labCurrent
            // 
            this.labCurrent.AutoSize = true;
            this.labCurrent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labCurrent.Font = new System.Drawing.Font("宋体", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labCurrent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labCurrent.Location = new System.Drawing.Point(380, 53);
            this.labCurrent.Name = "labCurrent";
            this.labCurrent.Size = new System.Drawing.Size(399, 91);
            this.labCurrent.TabIndex = 3;
            this.labCurrent.Text = "0.0";
            this.labCurrent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmMeter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 489);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMeter";
            this.Text = "电表设备";
            this.Load += new System.EventHandler(this.FrmMeter_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel panel1;
        private System.Windows.Forms.TableLayoutPanel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCOM;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBand;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMutiAddr;
        private System.Windows.Forms.Label labStatus;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.TableLayoutPanel panel3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labVolt;
        private System.Windows.Forms.Label labCurrent;
    }
}