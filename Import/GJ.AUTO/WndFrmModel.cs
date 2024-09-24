using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.COM; 
using GJ.PLUGINS;

namespace GJ.AUTO
{
    public partial class WndFrmModel : Form,IMainMsg
    {
        #region 插件方法
        /// <summary>
        /// 子窗口
        /// </summary>
        private Form _childForm = null;
        /// <summary>
        /// 指示运行状态 
        /// </summary>
        /// <param name="status"></param>
        public void OnShowStatus(EIndicator status)
        {

        }
        /// <summary>
        /// 显示软件版本
        /// </summary>
        /// <param name="verName"></param>
        /// <param name="verDate"></param>
        public void OnShowVersion(string verName, string verDate)
        {

        }
        /// <summary>
        /// 消息触发
        /// </summary>
        /// <param name="para"></param>
        public void OnMessage(string name, int lPara, int wPara)
        {
            if (lPara==(int)ElPara.退出)
            {
                this.Close();
            }              
        }
        /// <summary>
        /// 退出系统
        /// </summary>
        public void OnExitSystem()
        {
            System.Environment.Exit(0);
        }
        #endregion

        #region 显示窗口

        #region 字段
        private static WndFrmModel dlg = null;
        private static object syncRoot = new object();
        #endregion

        #region 属性
        public static bool IsAvalible
        {
            get
            {
                lock (syncRoot)
                {
                    if (dlg != null && !dlg.IsDisposed)
                        return true;
                    else
                        return false;
                }
            }
        }
        public static Form mdlg
        {
            get
            {
                return dlg;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 创建唯一实例
        /// </summary>
        public static WndFrmModel CreateInstance()
        {
            lock (syncRoot)
            {
                if (dlg == null || dlg.IsDisposed)
                {
                    dlg = new WndFrmModel();
                }
            }
            return dlg;
        }
        #endregion

        #endregion

        #region 构造函数
        private WndFrmModel()
        {
            InitializeComponent();

            IntialControl();

            SetDoubleBuffered();
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化界面
        /// </summary>
        private void IntialControl()
        {

        }
        /// <summary>
        /// 设置双缓冲,防止界面闪烁
        /// </summary>
        private void SetDoubleBuffered()
        {
            panelMain.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(panelMain, true, null);
        }
        #endregion

        #region 面板回调函数
        private void WndFrmModel_Load(object sender, EventArgs e)
        {
            LoadChildForm();

            SetLanguage();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 加载测试工位
        /// </summary>
        private void LoadChildForm()
        {
            string er = string.Empty;

            string mainClass = this.Name.Replace("Wnd", "");

            string className = CGlobal.CFlow.NameSpace + "." + mainClass;

            if (!CReflect.LoadChildForm(className, out _childForm, out er))
            {
                MessageBox.Show(er, CLanguage.Lan("加载工位"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!CReflect.ShowChildForm(this, this.panelMain, CGlobal.CFlow.FlowGUID, _childForm, out er))
            {
                MessageBox.Show(er, CLanguage.Lan("显示工位"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
        /// <summary>
        /// 加载中英文界面
        /// </summary>
        private void SetLanguage()
        {
            CLanguage.SetLanguage(dlg);

            switch (CLanguage.languageType)
            {
                case CLanguage.EL.中文:
                    this.Text = "机种参数配置";
                    break;
                case CLanguage.EL.英语:
                    this.Text = "Model Parameter Setting";
                    break;
                case CLanguage.EL.繁体 :
                    this.Text = CLanguage.Lan("机种参数配置");
                    break;
                default:
                    break;
            }
        }
        #endregion

    }
}
