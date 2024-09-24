namespace GJ.AUTO
{
    partial class WndFrmVersion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WndFrmVersion));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panelMain = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "FA1");
            this.imageList1.Images.SetKeyName(1, "FA2");
            this.imageList1.Images.SetKeyName(2, "FA3");
            this.imageList1.Images.SetKeyName(3, "FA4");
            this.imageList1.Images.SetKeyName(4, "FA5");
            this.imageList1.Images.SetKeyName(5, "0");
            this.imageList1.Images.SetKeyName(6, "1");
            this.imageList1.Images.SetKeyName(7, "2");
            this.imageList1.Images.SetKeyName(8, "3");
            this.imageList1.Images.SetKeyName(9, "4");
            this.imageList1.Images.SetKeyName(10, "5");
            this.imageList1.Images.SetKeyName(11, "6");
            this.imageList1.Images.SetKeyName(12, "7");
            // 
            // panelMain
            // 
            resources.ApplyResources(this.panelMain, "panelMain");
            this.panelMain.Name = "panelMain";
            // 
            // WndFrmVersion
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMain);
            this.Name = "WndFrmVersion";
            this.Load += new System.EventHandler(this.WndFrmVersion_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panelMain;
    }
}