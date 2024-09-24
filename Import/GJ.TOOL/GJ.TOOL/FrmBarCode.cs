using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics; 
using GJ.DEV.BARCODE;
using GJ.PLUGINS;
namespace GJ.TOOL
{
    public partial class FrmBarCode : Form,IChildMsg
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
            if (comMon != null)
            {
                comMon.Close();
                comMon = null;
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
        public FrmBarCode()
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
        private CBarCOM comMon = null;
        #endregion

        #region 面板控件
    
        #endregion

        #region 面板回调函数
        private void FrmCR1000_Load(object sender, EventArgs e)
        {
            string[] com = System.IO.Ports.SerialPort.GetPortNames();
            cmbCOM.Items.Clear();
            for (int i = 0; i < com.Length; i++)
                cmbCOM.Items.Add(com[i]);
            if (com.Length > 0)
                cmbCOM.Text = com[0];
            cmbBarType.SelectedIndex = 0; 
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (cmbCOM.Text == "")
            {
                labStatus.Text = "请输入串口编号";
                labStatus.ForeColor = Color.Red;
                return;
            }
            
            string er = string.Empty;

            if (comMon == null)
            {
                if (!Enum.IsDefined(typeof(EBarType), cmbBarType.Text))
                {
                    labStatus.Text = "找不到【"+ cmbBarType.Text +"】条码枪类型";
                    labStatus.ForeColor = Color.Red;
                    return;
                }

                EBarType barType = (EBarType)Enum.Parse(typeof(EBarType), cmbBarType.Text);

                comMon = new CBarCOM(barType,0,cmbBarType.Text);

                comMon.OnRecved += new OnRecvHandler(OnSerialPortRecv);

                if (!comMon.Open(cmbCOM.Text, out er,txtBand.Text))
                {
                    labStatus.Text = er;
                    labStatus.ForeColor = Color.Red;
                    comMon = null;
                    return;
                }
                btnOpen.Text = "关闭";
                labStatus.Text = "成功打开串口.";
                labStatus.ForeColor = Color.Blue;
                cmbCOM.Enabled = false;
                cmbBarType.Enabled = false; 
            }
            else
            {
                comMon.OnRecved -= new OnRecvHandler(OnSerialPortRecv);
                comMon.Close();
                comMon = null;
                btnOpen.Text = "打开";
                labStatus.Text = "关闭串口.";
                labStatus.ForeColor = Color.Blue;
                cmbCOM.Enabled = true;
                cmbBarType.Enabled = true; 
            }
        }
        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                btnRead.Enabled = false;

                if (comMon == null)
                {
                    labStatus.Text = "请确定串口是否打开?";
                    labStatus.ForeColor = Color.Red;
                    return;
                }
                if (btnTriger.Text == "停止触发")
                {
                    labStatus.Text = "请先停止触发";
                    labStatus.ForeColor = Color.Red;
                    return;
                }

                Stopwatch watcher = new Stopwatch();

                watcher.Restart(); 

                string serialNo = string.Empty;

                string er = string.Empty;

                int delayTime = System.Convert.ToInt32(txtDelayTimes.Text);

                if (comMon.Read(out serialNo, out er, 0, delayTime))
                {
                    labSn.Text = serialNo;
                    labLen.Text = serialNo.Length.ToString();
                    labStatus.Text = "读取条码OK.";
                    labStatus.ForeColor = Color.Blue;
                }
                else
                {
                    labSn.Text = serialNo;
                    labStatus.Text = "读取条码失败:" + er;
                    labStatus.ForeColor = Color.Red;
                }

                watcher.Stop();

                labTimes.Text = watcher.ElapsedMilliseconds.ToString() + "ms";
            }
            catch (Exception)
            {

            }
            finally
            {
                btnRead.Enabled = true;
            }           
        }
        private void btnTriger_Click(object sender, EventArgs e)
        {
            try
            {
                btnTriger.Enabled = false;

                string er=string.Empty;

                if (btnTriger.Text == "启动触发")
                {
                    if (comMon == null)
                    {
                        labStatus.Text = "请确定串口是否打开?";
                        labStatus.ForeColor = Color.Red;
                        return;
                    }

                    if (!comMon.Triger_Start(out er))
                    {
                        labStatus.Text = "启动触发错误:" + er;
                        labStatus.ForeColor = Color.Red;
                        return;
                    }

                    btnTriger.Text = "停止触发";
                }
                else
                {
                    if (comMon != null)
                    {
                        if (!comMon.Triger_End(out er))
                        {
                            labStatus.Text = "停止触发错误:" + er;
                            labStatus.ForeColor = Color.Red;
                            return;
                        }
                    }
                    btnTriger.Text = "启动触发";
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                btnTriger.Enabled = true;
            }            
        }
        private void btn_Click(object sender, EventArgs e)
        {
            runLog.Clear();

            _ttNum = 0;
        }
        #endregion

        #region 条码触发接收
        private int _ttNum = 0;
        private void OnSerialPortRecv(object sender, CRecvArgs e)
        {
            Log(e.recvData+"\r\n"); 
        }
        private void Log(string recv)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action<string>(Log), recv);
            else
            {
                runLog.ScrollToCaret();
                runLog.AppendText(recv);
                if (runLog.Lines.Length > 200)
                    runLog.Clear();
                _ttNum++;
                labTT.Text = _ttNum.ToString();
            }
        }
        #endregion
    }
}
