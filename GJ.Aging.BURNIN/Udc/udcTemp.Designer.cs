namespace GJ.Aging.BURNIN.Udc
{
    partial class udcTemp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(udcTemp));
            this.panel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTemp = new System.Windows.Forms.SplitContainer();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTemp)).BeginInit();
            this.pnlTemp.Panel2.SuspendLayout();
            this.pnlTemp.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // pnlTemp
            // 
            resources.ApplyResources(this.pnlTemp, "pnlTemp");
            this.pnlTemp.Name = "pnlTemp";
            // 
            // pnlTemp.Panel2
            // 
            this.pnlTemp.Panel2.Controls.Add(this.panel1);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // udcTemp
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlTemp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "udcTemp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.udcTemp_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.udcTemp_FormClosed);
            this.Load += new System.EventHandler(this.udcTemp_Load);
            this.pnlTemp.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTemp)).EndInit();
            this.pnlTemp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel panel1;
        private System.Windows.Forms.SplitContainer pnlTemp;
        private System.Windows.Forms.Timer timer1;
    }
}