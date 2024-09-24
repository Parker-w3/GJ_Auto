using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.COM;
namespace GJ.Aging.BURNIN.Udc
{
    public partial class udcSelectModel : Form
    {
        public udcSelectModel(string message, int iTimerNo)
        {
            InitializeComponent();

            this.msg = message;

            lblmessage.Text = CLanguage.Lan(msg);
            switch (iTimerNo)
            {
                case 0:
                    this.Location = new Point(500, 260);
                    break;
                case 1:
                    this.Location = new Point(500, 560);
                    break;
                case 2:
                    this.Location = new Point(500, 260);
                    break;
                case 3:
                    this.Location = new Point(500, 560);
                    break;
                case 4:
                    this.Location = new Point(500, 260);
                    break;
                case 5:
                    this.Location = new Point(500, 560);
                    break;
                case 6:
                    this.Location = new Point(500, 260);
                    break;
                case 7:
                    this.Location = new Point(500, 560);
                    break;
                case 8:
                    this.Location = new Point(500, 260);
                    break;
                case 9:
                    this.Location = new Point(500, 560);
                    break;
                case 10:
                    this.Location = new Point(500, 260);
                    break;
                case 11:
                    this.Location = new Point(500, 560);
                    break;


                default:
                    break;
            }

            SetUILanguage();

        }
        public static string modelName = string.Empty;
        private string msg = string.Empty;

        #region 面板加载
        private void udcSelectModel_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region 控件方法
        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            modelName = txtmodelName.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// 回车确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtmodelName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                modelName = txtmodelName.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        #endregion

        #region 语言设置
        /// <summary>
        /// 设置中英文界面
        /// </summary>
        private void SetUILanguage()
        {

            switch (GJ.COM.CLanguage.languageType)
            {
                case CLanguage.EL.中文:
                    btnOK.Text = CLanguage.Lan("确定");
                    btnCancel.Text = CLanguage.Lan("取消");
                    break;
                case CLanguage.EL.英语:
                    break;
                case CLanguage.EL.繁体:
                    btnOK.Text = CLanguage.Lan("确定");
                    btnCancel.Text = CLanguage.Lan("取消");
                    break;
                default:
                    break;
            }
        }
        #endregion

    }
}
