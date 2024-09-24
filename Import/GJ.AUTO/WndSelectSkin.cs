using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GJ.AUTO
{
    public partial class WndSelectSkin : Form
    {
        public WndSelectSkin()
        {
            InitializeComponent();
        }

        #region 面板回调函数
        private void WndSelectSkin_Load(object sender, EventArgs e)
        {
            string skinFolder = Application.StartupPath + "\\Skin";

            string[] skinFiles = Directory.GetFiles(skinFolder);

            for (int i = 0; i < skinFiles.Length; i++)
            {
                listSkin.Items.Add(Path.GetFileNameWithoutExtension(skinFiles[i]));
                listSkin.Items[i].ToolTipText = skinFiles[i];
                listSkin.Items[i].ImageIndex = 6;  
            }

        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (listSkin.SelectedItems.Count != 0)
            {
                int idNo = listSkin.SelectedItems[0].Index;
                CGlobal.CFlow.skinFile = listSkin.Items[idNo].ToolTipText;    
                this.DialogResult = DialogResult.OK;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        #endregion
       
    }
}
