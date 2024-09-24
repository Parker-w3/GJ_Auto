namespace GJ.TOOL
{
    partial class FrmXianTong_Led
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
            this.txtMutiAddr = new System.Windows.Forms.TextBox();
            this.btnTimeSet = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTimeSet = new System.Windows.Forms.TextBox();
            this.labStatus = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
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
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.RowCount = 3;
            this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 138F));
            this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 151F));
            this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel1.Size = new System.Drawing.Size(664, 494);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.panel2.ColumnCount = 5;
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 144F));
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel2.Controls.Add(this.label2, 0, 1);
            this.panel2.Controls.Add(this.cmbCOM, 1, 1);
            this.panel2.Controls.Add(this.label4, 0, 0);
            this.panel2.Controls.Add(this.cmbType, 1, 0);
            this.panel2.Controls.Add(this.label5, 0, 2);
            this.panel2.Controls.Add(this.txtBand, 1, 2);
            this.panel2.Controls.Add(this.label1, 0, 3);
            this.panel2.Controls.Add(this.btnOpen, 2, 0);
            this.panel2.Controls.Add(this.txtMutiAddr, 1, 3);
            this.panel2.Controls.Add(this.btnTimeSet, 3, 3);
            this.panel2.Controls.Add(this.label3, 3, 2);
            this.panel2.Controls.Add(this.txtTimeSet, 4, 2);
            this.panel2.Controls.Add(this.labStatus, 4, 0);
            this.panel2.Controls.Add(this.label6, 3, 0);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(5, 5);
            this.panel2.Name = "panel2";
            this.panel2.RowCount = 4;
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.99813F));
            this.panel2.Size = new System.Drawing.Size(654, 132);
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
            // btnTimeSet
            // 
            this.btnTimeSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTimeSet.Location = new System.Drawing.Point(298, 97);
            this.btnTimeSet.Margin = new System.Windows.Forms.Padding(0);
            this.btnTimeSet.Name = "btnTimeSet";
            this.btnTimeSet.Size = new System.Drawing.Size(76, 34);
            this.btnTimeSet.TabIndex = 19;
            this.btnTimeSet.Text = "时间设定";
            this.btnTimeSet.UseVisualStyleBackColor = true;
            this.btnTimeSet.Click += new System.EventHandler(this.btnTimeSet_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(301, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 31);
            this.label3.TabIndex = 19;
            this.label3.Text = "时间:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTimeSet
            // 
            this.txtTimeSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTimeSet.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTimeSet.Location = new System.Drawing.Point(378, 68);
            this.txtTimeSet.Name = "txtTimeSet";
            this.txtTimeSet.Size = new System.Drawing.Size(272, 23);
            this.txtTimeSet.TabIndex = 12;
            this.txtTimeSet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labStatus
            // 
            this.labStatus.AutoSize = true;
            this.labStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labStatus.Location = new System.Drawing.Point(378, 1);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(272, 31);
            this.labStatus.TabIndex = 21;
            this.labStatus.Text = "---";
            this.labStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(301, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 31);
            this.label6.TabIndex = 20;
            this.label6.Text = "状态指示:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmXianTong_Led
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 494);
            this.Controls.Add(this.panel1);
            this.Name = "FrmXianTong_Led";
            this.Text = "Frm_XianTong_Led";
            this.Load += new System.EventHandler(this.Frm_XianTong_Led_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel panel1;
        private System.Windows.Forms.TableLayoutPanel panel2;
        private System.Windows.Forms.Button btnTimeSet;
        private System.Windows.Forms.TextBox txtTimeSet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCOM;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBand;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox txtMutiAddr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labStatus;
        private System.Windows.Forms.Label label6;
    }
}