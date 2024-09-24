namespace GJ.Aging.BURNIN.Udc
{
    partial class udcUUTInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(udcUUTInfo));
            this.panel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lablocalName = new System.Windows.Forms.Label();
            this.labModel = new System.Windows.Forms.Label();
            this.labStartTime = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.labRunTime = new System.Windows.Forms.Label();
            this.labLeftTime = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labEndTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.TableLayoutPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.panel3, 0, 1);
            this.panel1.Controls.Add(this.label1, 0, 0);
            this.panel1.Controls.Add(this.panel4, 0, 2);
            this.panel1.Name = "panel1";
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Controls.Add(this.label2, 0, 0);
            this.panel3.Controls.Add(this.label3, 0, 1);
            this.panel3.Controls.Add(this.label4, 0, 2);
            this.panel3.Controls.Add(this.lablocalName, 1, 0);
            this.panel3.Controls.Add(this.labModel, 1, 1);
            this.panel3.Controls.Add(this.labStartTime, 1, 2);
            this.panel3.Controls.Add(this.label15, 2, 0);
            this.panel3.Controls.Add(this.label16, 2, 1);
            this.panel3.Controls.Add(this.labRunTime, 3, 0);
            this.panel3.Controls.Add(this.labLeftTime, 3, 1);
            this.panel3.Controls.Add(this.label8, 2, 2);
            this.panel3.Controls.Add(this.labEndTime, 3, 2);
            this.panel3.Name = "panel3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // lablocalName
            // 
            resources.ApplyResources(this.lablocalName, "lablocalName");
            this.lablocalName.ForeColor = System.Drawing.Color.Black;
            this.lablocalName.Name = "lablocalName";
            // 
            // labModel
            // 
            resources.ApplyResources(this.labModel, "labModel");
            this.labModel.ForeColor = System.Drawing.Color.Navy;
            this.labModel.Name = "labModel";
            // 
            // labStartTime
            // 
            resources.ApplyResources(this.labStartTime, "labStartTime");
            this.labStartTime.ForeColor = System.Drawing.Color.Navy;
            this.labStartTime.Name = "labStartTime";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // labRunTime
            // 
            resources.ApplyResources(this.labRunTime, "labRunTime");
            this.labRunTime.ForeColor = System.Drawing.Color.Green;
            this.labRunTime.Name = "labRunTime";
            // 
            // labLeftTime
            // 
            resources.ApplyResources(this.labLeftTime, "labLeftTime");
            this.labLeftTime.ForeColor = System.Drawing.Color.Green;
            this.labLeftTime.Name = "labLeftTime";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // labEndTime
            // 
            resources.ApplyResources(this.labEndTime, "labEndTime");
            this.labEndTime.ForeColor = System.Drawing.Color.Navy;
            this.labEndTime.Name = "labEndTime";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Name = "label1";
            // 
            // panel4
            // 
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // udcUUTInfo
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "udcUUTInfo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.udcUUTInfo_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.udcUUTInfo_FormClosed);
            this.Load += new System.EventHandler(this.udcUUTInfo_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel panel4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TableLayoutPanel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lablocalName;
        private System.Windows.Forms.Label labModel;
        private System.Windows.Forms.Label labStartTime;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label labRunTime;
        private System.Windows.Forms.Label labLeftTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labEndTime;
    }
}