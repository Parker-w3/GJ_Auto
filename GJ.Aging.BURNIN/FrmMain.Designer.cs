namespace GJ.Aging.BURNIN
{
    partial class FrmMain
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
            base.Dispose(disposing);
                components.Dispose();
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle65 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle66 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle70 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle71 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle72 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle67 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle68 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle69 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.MainWorker = new System.ComponentModel.BackgroundWorker();
            this.skinSplitContainer1 = new CCWin.SkinControl.SkinSplitContainer();
            this.stcTilte = new System.Windows.Forms.TabControl();
            this.pnlType = new System.Windows.Forms.TableLayoutPanel();
            this.runLog = new GJ.UI.udcRunLog();
            this.punAgingType = new System.Windows.Forms.TableLayoutPanel();
            this.lblRunStatus = new System.Windows.Forms.Label();
            this.pnlRunType = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label36 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.gpbFailShow = new System.Windows.Forms.GroupBox();
            this.dgvFailData = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.skinSplitContainer1)).BeginInit();
            this.skinSplitContainer1.Panel1.SuspendLayout();
            this.skinSplitContainer1.Panel2.SuspendLayout();
            this.skinSplitContainer1.SuspendLayout();
            this.pnlType.SuspendLayout();
            this.punAgingType.SuspendLayout();
            this.pnlRunType.SuspendLayout();
            this.panel2.SuspendLayout();
            this.gpbFailShow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFailData)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "OPEN");
            // 
            // MainWorker
            // 
            this.MainWorker.WorkerSupportsCancellation = true;
            this.MainWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.MainWorker_DoWork);
            // 
            // skinSplitContainer1
            // 
            this.skinSplitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.skinSplitContainer1, "skinSplitContainer1");
            this.skinSplitContainer1.LineBack = System.Drawing.SystemColors.Control;
            this.skinSplitContainer1.LineBack2 = System.Drawing.SystemColors.Control;
            this.skinSplitContainer1.Name = "skinSplitContainer1";
            // 
            // skinSplitContainer1.Panel1
            // 
            this.skinSplitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.skinSplitContainer1.Panel1.Controls.Add(this.stcTilte);
            // 
            // skinSplitContainer1.Panel2
            // 
            this.skinSplitContainer1.Panel2.Controls.Add(this.pnlType);
            // 
            // stcTilte
            // 
            resources.ApplyResources(this.stcTilte, "stcTilte");
            this.stcTilte.Name = "stcTilte";
            this.stcTilte.SelectedIndex = 0;
            // 
            // pnlType
            // 
            this.pnlType.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.pnlType, "pnlType");
            this.pnlType.Controls.Add(this.punAgingType, 0, 0);
            this.pnlType.Controls.Add(this.runLog, 1, 0);
            this.pnlType.Name = "pnlType";
            // 
            // runLog
            // 
            resources.ApplyResources(this.runLog, "runLog");
            this.runLog.mFont = new System.Drawing.Font("宋体", 9F);
            this.runLog.mMaxLine = 1000;
            this.runLog.mMaxMB = 1D;
            this.runLog.mSaveEnable = true;
            this.runLog.mSaveName = "RunLog";
            this.runLog.mTitle = "运行日志";
            this.runLog.mTitleEnable = true;
            this.runLog.Name = "runLog";
            // 
            // punAgingType
            // 
            resources.ApplyResources(this.punAgingType, "punAgingType");
            this.punAgingType.Controls.Add(this.lblRunStatus, 0, 0);
            this.punAgingType.Controls.Add(this.pnlRunType, 0, 1);
            this.punAgingType.Controls.Add(this.panel2, 0, 2);
            this.punAgingType.Name = "punAgingType";
            // 
            // lblRunStatus
            // 
            resources.ApplyResources(this.lblRunStatus, "lblRunStatus");
            this.lblRunStatus.ForeColor = System.Drawing.Color.Blue;
            this.lblRunStatus.Name = "lblRunStatus";
            // 
            // pnlRunType
            // 
            this.pnlRunType.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.pnlRunType, "pnlRunType");
            this.pnlRunType.Controls.Add(this.label3, 2, 0);
            this.pnlRunType.Controls.Add(this.label1, 0, 0);
            this.pnlRunType.Controls.Add(this.label2, 1, 0);
            this.pnlRunType.Controls.Add(this.label4, 3, 0);
            this.pnlRunType.Controls.Add(this.label6, 4, 0);
            this.pnlRunType.Name = "pnlRunType";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Name = "label3";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Name = "label2";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Name = "label4";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Name = "label6";
            this.label6.DoubleClick += new System.EventHandler(this.label_DoubleClick);
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.label36, 8, 0);
            this.panel2.Controls.Add(this.label5, 0, 0);
            this.panel2.Controls.Add(this.label7, 1, 0);
            this.panel2.Controls.Add(this.label8, 2, 0);
            this.panel2.Controls.Add(this.label9, 3, 0);
            this.panel2.Controls.Add(this.label10, 4, 0);
            this.panel2.Controls.Add(this.label12, 5, 0);
            this.panel2.Controls.Add(this.label20, 6, 0);
            this.panel2.Controls.Add(this.label28, 7, 0);
            this.panel2.Name = "panel2";
            // 
            // label36
            // 
            resources.ApplyResources(this.label36, "label36");
            this.label36.BackColor = System.Drawing.Color.LightGray;
            this.label36.Name = "label36";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.BackColor = System.Drawing.Color.LimeGreen;
            this.label8.Name = "label8";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.BackColor = System.Drawing.Color.Red;
            this.label9.Name = "label9";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.BackColor = System.Drawing.Color.Blue;
            this.label10.Name = "label10";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.BackColor = System.Drawing.Color.Yellow;
            this.label12.Name = "label12";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.BackColor = System.Drawing.Color.Pink;
            this.label20.Name = "label20";
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.BackColor = System.Drawing.Color.LightCoral;
            this.label28.Name = "label28";
            // 
            // gpbFailShow
            // 
            this.gpbFailShow.Controls.Add(this.dgvFailData);
            this.gpbFailShow.ForeColor = System.Drawing.Color.Blue;
            resources.ApplyResources(this.gpbFailShow, "gpbFailShow");
            this.gpbFailShow.Name = "gpbFailShow";
            this.gpbFailShow.TabStop = false;
            // 
            // dgvFailData
            // 
            this.dgvFailData.AllowUserToAddRows = false;
            this.dgvFailData.AllowUserToDeleteRows = false;
            dataGridViewCellStyle65.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvFailData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle65;
            this.dgvFailData.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle66.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle66.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle66.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle66.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle66.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle66.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle66.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFailData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle66;
            resources.ApplyResources(this.dgvFailData, "dgvFailData");
            this.dgvFailData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvFailData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            dataGridViewCellStyle70.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle70.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle70.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle70.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle70.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle70.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle70.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFailData.DefaultCellStyle = dataGridViewCellStyle70;
            this.dgvFailData.Name = "dgvFailData";
            dataGridViewCellStyle71.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle71.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle71.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle71.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle71.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle71.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle71.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFailData.RowHeadersDefaultCellStyle = dataGridViewCellStyle71;
            dataGridViewCellStyle72.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvFailData.RowsDefaultCellStyle = dataGridViewCellStyle72;
            this.dgvFailData.RowTemplate.Height = 23;
            this.dgvFailData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvFailData.DoubleClick += new System.EventHandler(this.dgvFailData_DoubleClick);
            // 
            // Column1
            // 
            dataGridViewCellStyle67.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle67;
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            dataGridViewCellStyle68.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle68;
            resources.ApplyResources(this.Column2, "Column2");
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            dataGridViewCellStyle69.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle69;
            resources.ApplyResources(this.Column3, "Column3");
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FrmMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.skinSplitContainer1);
            this.Controls.Add(this.gpbFailShow);
            this.Name = "FrmMain";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.skinSplitContainer1.Panel1.ResumeLayout(false);
            this.skinSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.skinSplitContainer1)).EndInit();
            this.skinSplitContainer1.ResumeLayout(false);
            this.pnlType.ResumeLayout(false);
            this.punAgingType.ResumeLayout(false);
            this.punAgingType.PerformLayout();
            this.pnlRunType.ResumeLayout(false);
            this.pnlRunType.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.gpbFailShow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFailData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.ComponentModel.BackgroundWorker MainWorker;
        private CCWin.SkinControl.SkinSplitContainer skinSplitContainer1;
        private System.Windows.Forms.TableLayoutPanel pnlType;
        private UI.udcRunLog runLog;
        private System.Windows.Forms.Label lblRunStatus;
      
        private System.Windows.Forms.TableLayoutPanel punAgingType;
        private System.Windows.Forms.GroupBox gpbFailShow;
        private System.Windows.Forms.DataGridView dgvFailData;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.TabControl stcTilte;
        private System.Windows.Forms.TableLayoutPanel pnlRunType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label36;


    }
}