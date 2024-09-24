using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.DEV.Meter ;
using GJ.PLUGINS;

namespace GJ.TOOL
{
    public partial class FrmXianTong_Led : Form, IChildMsg
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
            if (comMeter != null)
            {
                comMeter.close ();
                comMeter = null;
                btnOpen.Text = "打开";
                labStatus.Text = "关闭串口.";
                labStatus.ForeColor = Color.Blue;
            }
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

        public FrmXianTong_Led()
        {
            InitializeComponent();

            InitialControl();

            SetDoubleBuffered();


        }
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitialControl()
        {

        }
        /// <summary>
        /// 设置双缓冲,防止界面闪烁
        /// </summary>
        private void SetDoubleBuffered()
        {
            panel1.GetType().GetProperty("DoubleBuffered",
                                           System.Reflection.BindingFlags.Instance |
                                           System.Reflection.BindingFlags.NonPublic)
                                           .SetValue(panel1, true, null);
            panel2.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(panel2, true, null);
        }
        #endregion

        #region 字段
        private C_XianTong_Led  comMeter = null;
        #endregion
        #region 面板回调函数

        /// <summary>
        /// 初始化界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_XianTong_Led_Load(object sender, EventArgs e)
        {
            string[] com = System.IO.Ports.SerialPort.GetPortNames();
            cmbCOM.Items.Clear();
            for (int i = 0; i < com.Length; i++)
                cmbCOM.Items.Add(com[i]);
            if (com.Length > 0)
                cmbCOM.Text = com[0];
            cmbType.SelectedIndex = 0;
        }

        #endregion

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (cmbCOM.Text == "")
            {
                labStatus.Text = "请输入串口编号";
                labStatus.ForeColor = Color.Red;
                return;
            }

            string er = string.Empty;

            if (comMeter == null)
            {
                comMeter = new C_XianTong_Led ();

                if (!comMeter.open (cmbCOM.Text,  txtBand.Text,out er))
                {
                    labStatus.Text = er;
                    labStatus.ForeColor = Color.Red;
                    comMeter = null;
                    return;
                }
                btnOpen.Text = "关闭";
                labStatus.Text = "成功打开串口.";
                labStatus.ForeColor = Color.Blue;
                cmbCOM.Enabled = false;
            }
            else
            {
                comMeter.close ();
                comMeter = null;
                btnOpen.Text = "打开";
                labStatus.Text = "关闭串口.";
                labStatus.ForeColor = Color.Blue;
                cmbCOM.Enabled = true;
            }
        }
        /// <summary>
        /// 设定时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTimeSet_Click(object sender, EventArgs e)
        {
            string er = string.Empty;

            int wAdrs = Convert.ToInt16(txtMutiAddr.Text);
            if (txtTimeSet.Text == "") 
            {
                labStatus.Text = "请输入时间";
                labStatus.ForeColor = Color.Red;
                return;
            }

            int wTime = Convert.ToInt16(txtTimeSet.Text);
            if (!comMeter.WTime(wAdrs, wTime, out er))
            {
                labStatus.Text = er;
                labStatus.ForeColor = Color.Red;

            }
            else
            {
                labStatus.Text = "时间设定成功";
                labStatus.ForeColor = Color.Blue;
            }


        }


    }
}
