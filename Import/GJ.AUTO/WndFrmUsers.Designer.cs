namespace GJ.AUTO
{
    partial class WndFrmUsers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WndFrmUsers));
            this.label1 = new System.Windows.Forms.Label();
            this.gridUsers = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkLevel8 = new System.Windows.Forms.CheckBox();
            this.chkLevel7 = new System.Windows.Forms.CheckBox();
            this.chkLevel6 = new System.Windows.Forms.CheckBox();
            this.chkLevel5 = new System.Windows.Forms.CheckBox();
            this.chkLevel4 = new System.Windows.Forms.CheckBox();
            this.chkLevel3 = new System.Windows.Forms.CheckBox();
            this.chkLevel2 = new System.Windows.Forms.CheckBox();
            this.chkLevel1 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkLook = new System.Windows.Forms.CheckBox();
            this.txtPassWord = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridUsers)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // gridUsers
            // 
            this.gridUsers.AllowUserToAddRows = false;
            this.gridUsers.AllowUserToDeleteRows = false;
            this.gridUsers.AllowUserToResizeColumns = false;
            this.gridUsers.AllowUserToResizeRows = false;
            this.gridUsers.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.gridUsers, "gridUsers");
            this.gridUsers.Name = "gridUsers";
            this.gridUsers.RowTemplate.Height = 23;
            this.gridUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridUsers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridUsers_CellClick);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkLevel8);
            this.groupBox1.Controls.Add(this.chkLevel7);
            this.groupBox1.Controls.Add(this.chkLevel6);
            this.groupBox1.Controls.Add(this.chkLevel5);
            this.groupBox1.Controls.Add(this.chkLevel4);
            this.groupBox1.Controls.Add(this.chkLevel3);
            this.groupBox1.Controls.Add(this.chkLevel2);
            this.groupBox1.Controls.Add(this.chkLevel1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // chkLevel8
            // 
            resources.ApplyResources(this.chkLevel8, "chkLevel8");
            this.chkLevel8.Name = "chkLevel8";
            this.chkLevel8.UseVisualStyleBackColor = true;
            // 
            // chkLevel7
            // 
            resources.ApplyResources(this.chkLevel7, "chkLevel7");
            this.chkLevel7.Name = "chkLevel7";
            this.chkLevel7.UseVisualStyleBackColor = true;
            // 
            // chkLevel6
            // 
            resources.ApplyResources(this.chkLevel6, "chkLevel6");
            this.chkLevel6.Name = "chkLevel6";
            this.chkLevel6.UseVisualStyleBackColor = true;
            // 
            // chkLevel5
            // 
            resources.ApplyResources(this.chkLevel5, "chkLevel5");
            this.chkLevel5.Name = "chkLevel5";
            this.chkLevel5.UseVisualStyleBackColor = true;
            // 
            // chkLevel4
            // 
            resources.ApplyResources(this.chkLevel4, "chkLevel4");
            this.chkLevel4.Name = "chkLevel4";
            this.chkLevel4.UseVisualStyleBackColor = true;
            // 
            // chkLevel3
            // 
            resources.ApplyResources(this.chkLevel3, "chkLevel3");
            this.chkLevel3.Name = "chkLevel3";
            this.chkLevel3.UseVisualStyleBackColor = true;
            // 
            // chkLevel2
            // 
            resources.ApplyResources(this.chkLevel2, "chkLevel2");
            this.chkLevel2.Name = "chkLevel2";
            this.chkLevel2.UseVisualStyleBackColor = true;
            // 
            // chkLevel1
            // 
            resources.ApplyResources(this.chkLevel1, "chkLevel1");
            this.chkLevel1.Name = "chkLevel1";
            this.chkLevel1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkLook);
            this.groupBox2.Controls.Add(this.txtPassWord);
            this.groupBox2.Controls.Add(this.txtUserName);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // chkLook
            // 
            resources.ApplyResources(this.chkLook, "chkLook");
            this.chkLook.Name = "chkLook";
            this.chkLook.UseVisualStyleBackColor = true;
            this.chkLook.CheckedChanged += new System.EventHandler(this.chkLook_CheckedChanged);
            // 
            // txtPassWord
            // 
            resources.ApplyResources(this.txtPassWord, "txtPassWord");
            this.txtPassWord.Name = "txtPassWord";
            // 
            // txtUserName
            // 
            resources.ApplyResources(this.txtUserName, "txtUserName");
            this.txtUserName.Name = "txtUserName";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            resources.ApplyResources(this.btnExit, "btnExit");
            this.btnExit.Name = "btnExit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // WndFrmUsers
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gridUsers);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WndFrmUsers";
            this.Load += new System.EventHandler(this.WndFrmUsers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridUsers)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gridUsers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkLevel8;
        private System.Windows.Forms.CheckBox chkLevel7;
        private System.Windows.Forms.CheckBox chkLevel6;
        private System.Windows.Forms.CheckBox chkLevel5;
        private System.Windows.Forms.CheckBox chkLevel4;
        private System.Windows.Forms.CheckBox chkLevel3;
        private System.Windows.Forms.CheckBox chkLevel2;
        private System.Windows.Forms.CheckBox chkLevel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtPassWord;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.CheckBox chkLook;
    }
}