using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.MES.ACBEL;
using GJ.PLUGINS;
using GJ.COM ;

namespace GJ.TOOL
{
    public partial class FrmMesACBEL : Form, IChildMsg
    {
        #region 插件方法
        /// <summary>
        /// 父窗口
        /// </summary>
        private Form _fatherForm = null;
        /// <summary>
        /// 父窗口GUID
        /// </summary>
        private string _fatherGuid = string.Empty;
        /// <summary>
        /// 显示当前窗口到父窗口容器中
        /// </summary>
        /// <param name="fatherForm">父窗口</param>
        /// <param name="control">父窗口容器</param>
        /// <param name="guid">父窗口全局名称</param>
        public void OnShowDlg(Form fatherForm, Control control, string guid)
        {
            _fatherForm = fatherForm;
            _fatherGuid = guid;
            this.Dock = DockStyle.Fill;
            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Show();
            control.Controls.Add(this);
        }
        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        public void OnCloseDlg()
        {
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user"></param>
        /// <param name="mPwrLevel"></param>
        public void OnLogIn(string user, int[] mPwrLevel)
        {

        }
        /// <summary>
        /// 启动运行
        /// </summary>
        public void OnStartRun()
        {

        }
        /// <summary>
        /// 停止运行
        /// </summary>
        public void OnStopRun()
        {

        }
        /// <summary>
        /// 切换语言 
        /// </summary>
        public void OnChangeLAN()
        {

        }
        /// <summary>
        /// 消息事件
        /// </summary>
        /// <param name="para"></param>
        public void OnMessage(string name, int lPara, int wPara)
        {

        }
        #endregion

        #region 构造函数
        public FrmMesACBEL()
        {
            InitializeComponent();
        }
        #endregion

        #region 字段
        private C_web_ACBEL acbel_shopflow = null;
        #endregion


        private void btnLink_Click(object sender, EventArgs e)
        {
            try
            {
                btnLink.Enabled = false;
                 
                if (txtLineNO.Text == "")
                {
                    labStatus.Text = CLanguage.Lan( "请输入线体编号");
                    labStatus.ForeColor = Color.Red;
                    return;
                }

                string er = string.Empty;

                if (btnLink.Text == "Link")
                {

                    acbel_shopflow = new C_web_ACBEL();
                    int iCont = acbel_shopflow.OpenMes(txtLineNO.Text.Trim());

                    if (iCont != 1)
                    {
                        labStatus.Text = CLanguage.Lan( "连接DATABASE失败");
                        labStatus.ForeColor = Color.Red;
                        return;
                    }
                    btnLink.Text = "Close";
                    labStatus.Text = CLanguage.Lan( "成功连接DATABASE.");
                    labStatus.ForeColor = Color.Blue;
                    lblDataBase.Text ="database";
                    txtLineNO.Enabled = false;
                }
                else
                {
                    acbel_shopflow.CloseMes();
                    btnLink.Text = "Link";
                    labStatus.Text = CLanguage.Lan( "关闭DATABASE连接.");
                    labStatus.ForeColor = Color.Blue;
                    txtLineNO.Enabled = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                btnLink.Enabled = true;
            }
        }

        private void FrmMesACBEL_Load(object sender, EventArgs e)
        {

        }

        private void btnChkBar_Click(object sender, EventArgs e)
        {

            try
            {
                string er = string.Empty;
                btnChkBar.Enabled = false;
                int iCont = acbel_shopflow.UUTID (lblDataBase.Text.Trim(), txtStation.Text.Trim(),
                            txtBarCode.Text.Trim(), txtDEDTOOL_NO.Text.Trim(), txtModelName.Text.Trim());

                if (iCont != 1)
                {
                    labStatus.Text = CLanguage.Lan( "条码验证失败;ErrCode_") + iCont.ToString();
                    labStatus.ForeColor = Color.Red;
                    return;
                }

                labStatus.Text = CLanguage.Lan( "条码验证成功.");
                labStatus.ForeColor = Color.Blue;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                btnChkBar.Enabled = true;
            }

        }

        private void btnINSERT_DATA_Click(object sender, EventArgs e)
        {
            try
            {
                string er = string.Empty;
                btnINSERT_DATA.Enabled = false ;
                int iCont = acbel_shopflow.TranUUT (lblDataBase.Text.Trim(), txtOracleStr1.Text.Trim(),
                             txtOracleStr2.Text.Trim());

                if (iCont != 1)
                {
                    labStatus.Text =CLanguage.Lan(  "数据上传失败;ErrCode_") + iCont.ToString();
                    labStatus.ForeColor = Color.Red;
                    return;
                }

                labStatus.Text = CLanguage.Lan( "数据上传成功.");
                labStatus.ForeColor = Color.Blue;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                btnINSERT_DATA.Enabled = true;
            }  
        }
    }
}
