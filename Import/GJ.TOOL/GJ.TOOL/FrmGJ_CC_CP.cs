using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.PLUGINS;
using GJ.DEV.CAN;
using GJ.COM;


namespace GJ.TOOL
{
    public partial class FrmGJ_CC_CP : Form, IChildMsg
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
            if (comCC_CP != null)
            {
                comCC_CP.close();
                comCC_CP = null;
                btnOpen.Text = CLanguage.Lan("打开");
                labStatus.Text = CLanguage.Lan("关闭串口.");
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
        public FrmGJ_CC_CP()
        {
            InitializeComponent();

            IntialControl();

            SetDoubleBuffered();
        }


        #endregion

        #region 初始化
        /// <summary>
        /// 面板初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmGJ_CC_CP_Load(object sender, EventArgs e)
        {
            SetLanguage();

            cmbCom.Items.Clear();
            string[] com = System.IO.Ports.SerialPort.GetPortNames();
            for (int i = 0; i < com.Length; i++)
                cmbCom.Items.Add(com[i]);
            if (com.Length > 0)
                cmbCom.SelectedIndex = 0;
        }
        /// <summary>
        /// 绑定控件
        /// </summary>
        private void IntialControl()
        {

        }
        /// <summary>
        /// 设置双缓冲,防止界面闪烁
        /// </summary>
        private void SetDoubleBuffered()
        {

            splitContainer1.Panel1.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(splitContainer1.Panel1, true, null);

            splitContainer1.Panel2.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(splitContainer1.Panel2, true, null);


        }

        #endregion

        #region 字段
        /// <summary>
        /// CC_CP通讯板
        /// </summary>
        private C_GJ_CC_CP comCC_CP = null;

        /// <summary>
        /// 当前地址
        /// </summary>
        private int curAddr = 0;
        /// <summary>
        /// 扫描行
        /// </summary>
        private int rowNum = 0;
        /// <summary>
        /// 取消
        /// </summary>
        private bool cancel = false;
        #endregion

        #region 面板方法

        /// <summary>
        /// 打开串口 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                btnOpen.Enabled = false;

                if (cmbCom.Text == "")
                {
                    labStatus.Text = CLanguage.Lan("请输入串口编号");
                    labStatus.ForeColor = Color.Red;
                    return;
                }

                string er = string.Empty;

                if (comCC_CP == null)
                {
                    comCC_CP = new  C_GJ_CC_CP();
                    if (!comCC_CP.open(cmbCom.Text, txtBaud.Text, out er))
                    {
                        labStatus.Text = er;
                        labStatus.ForeColor = Color.Red;
                        comCC_CP = null;
                        return;
                    }
                    btnOpen.Text = CLanguage.Lan("关闭");
                    labStatus.Text = CLanguage.Lan("成功打开串口.");
                    labStatus.ForeColor = Color.Blue;
                    cmbCom.Enabled = false;
                }
                else
                {
                    comCC_CP.close();
                    comCC_CP = null;
                    btnOpen.Text = CLanguage.Lan("打开");
                    labStatus.Text = CLanguage.Lan("关闭串口.");
                    labStatus.ForeColor = Color.Blue;
                    cmbCom.Enabled = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                btnOpen.Enabled = true;
            }
        }

        /// <summary>
        /// 写地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetAdrs_Click(object sender, EventArgs e)
        {
            try
            {
                btnSetAdrs.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = 0x00;

                string er = string.Empty;

                string name = string.Empty;

                int[] setAdrs = new int[1];
                int[] readAdrs = new int[1];

                setAdrs[0] = System.Convert.ToInt16(txtAddr.Text);     //获取10进制的数

                if (!comCC_CP.RWAddrs(addr, true, setAdrs, false, ref readAdrs, out er))
                {
                    showInfo(CLanguage.Lan("写入地址[") + addr.ToString("D2") + CLanguage.Lan("]失败:") + er, true);
                    return;
                }
                showInfo(CLanguage.Lan("写入地址[") + addr.ToString("D2") + CLanguage.Lan("]OK"));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnSetAdrs.Enabled = true;
            }
        }

        /// <summary>
        /// 读地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetAdrs_Click(object sender, EventArgs e)
        {
            try
            {
                btnGetAdrs.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = 0x00;

                string er = string.Empty;

                string name = string.Empty;

                txtAddr.Text = "";

                int[] setAdrs = new int[1];
                int[] reaAdrs = new int[1];

                if (!comCC_CP.RWAddrs(addr, false, setAdrs, true, ref reaAdrs, out er))
                {
                    showInfo(CLanguage.Lan("读取地址失败:") + er, true);
                    return;
                }
                txtAddr.Text = reaAdrs[0].ToString() ;

                showInfo(CLanguage.Lan("读取地址OK"));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

                btnGetAdrs.Enabled = true;
            }
        }

        /// <summary>
        /// 读取波特率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadBaud_Click(object sender, EventArgs e)
        {
            try
            {
                btnReadBaud.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                string er = string.Empty;

                string name = string.Empty;

                txtBaud.Text = "";

                string baud = string.Empty;

                int[] setbaud = new int[1];
                int[] readbaud = new int[1];

                if (!comCC_CP.RWBaud(addr, false, setbaud, true, ref readbaud, out er))
                {
                    showInfo(CLanguage.Lan("读取地址[") + addr.ToString("D2") + CLanguage.Lan("]波特率失败:") + er, true);
                    return;
                }
                txtBaud.Text = readbaud[0].ToString() + ",n,8,1";

                showInfo(CLanguage.Lan("读取地址[") + addr.ToString("D2") + CLanguage.Lan("]波特率OK"));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

                btnReadBaud.Enabled = true;
            }
        }

        /// <summary>
        /// 写入波特率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWriteBaud_Click(object sender, EventArgs e)
        {
            try
            {
                btnWriteBaud.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                string er = string.Empty;

                string name = string.Empty;

                string baud = txtBaud.Text.Trim();

                txtBaud.Text = "";

                string[] Str = baud.Split(',');

                int[] setbaud = new int[1];
                int[] readbaud = new int[1];

                setbaud[0] = System.Convert.ToInt32(Str[0]);     //获取10进制的数

                if (!comCC_CP.RWBaud(addr, true, setbaud, false, ref readbaud, out er))
                {
                    showInfo(CLanguage.Lan("写入地址[") + addr.ToString("D2") + CLanguage.Lan("]波特率失败:") + er, true);
                    return;
                }
                txtBaud.Text = baud.ToString() + ",n,8,1";
                showInfo(CLanguage.Lan("写入地址[") + addr.ToString("D2") + CLanguage.Lan("]波特率OK"));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnWriteBaud.Enabled = true;
            }
        }




        /// <summary>
        /// 读取CAN盒版本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadVersion_Click(object sender, EventArgs e)
        {
            try
            {
                btnReadVersion.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                string er = string.Empty;

                string name = string.Empty;

                lblVer.Text = "";

                string readVer = "";

                if (!comCC_CP.ReadVersion(addr, out readVer, out er))
                {
                    showInfo(CLanguage.Lan("读取地址[") + addr.ToString("D3") + CLanguage.Lan("]版本失败:") + er, true);
                    return;
                }

                lblVer.Text = readVer;
                showInfo(CLanguage.Lan("读取地址[") + addr.ToString("D3") + CLanguage.Lan("]版本OK"));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnReadVersion.Enabled = true;
            }
        }

 


        #endregion

        #region 方法
        /// <summary>
        /// 检查设置
        /// </summary>
        /// <returns></returns>
        private bool checkSystem()
        {
            if (comCC_CP == null)
            {
                labStatus.Text = CLanguage.Lan("请确定已打开串口?");
                labStatus.ForeColor = Color.Red;
                return false;
            }
            if (txtAddr.Text == "")
            {
                labStatus.Text = CLanguage.Lan("请输入地址.");
                labStatus.ForeColor = Color.Red;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="er"></param>
        /// <param name="alarm"></param>
        private void showInfo(string er, bool alarm = false)
        {
            if (!alarm)
            {
                labStatus.Text = er;
                labStatus.ForeColor = Color.Blue;
            }
            else
            {
                labStatus.Text = er;
                labStatus.ForeColor = Color.Red;
            }
        }

        #endregion

        #region 语言切换
        /// <summary>
        /// 加载中英文界面
        /// </summary>
        private void SetLanguage()
        {

            switch (CLanguage.languageType)
            {
                case CLanguage.EL.中文:
                    this.Text = "GJ_CC_CP调试工具";
                    break;
                case CLanguage.EL.英语:
                    this.Text = "GJ_CC_CP Tool";
                    break;
                case CLanguage.EL.繁体:

                    break;
                default:
                    break;
            }
        }
        #endregion



        # region 扫描CC_CP通讯板
        /// <summary>
        /// 开始扫描CC_CP通讯板串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScan_Click(object sender, EventArgs e)
        {

            try
            {
                if (btnScan.Text == CLanguage.Lan("扫描"))
                {
                    if (!checkSystem())
                        return;

                    btnScan.Text = CLanguage.Lan("停止");

                    curAddr = System.Convert.ToInt16(txtStartAdrs.Text);

                    CC_CPView.Rows.Clear();

                    rowNum = 0;

                    cancel = false;

                    scanHandler scan = new scanHandler(OnScan);

                    scan.BeginInvoke(null, null);
                }
                else
                {
                    cancel = true;

                    btnScan.Text = CLanguage.Lan("扫描");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private delegate void scanHandler();
        /// <summary>
        /// 扫描
        /// </summary>
        private void OnScan()
        {
            while (true)
            {
                if (cancel)
                    return;

                string er = string.Empty;

                bool pass = true;

                string ver = string.Empty;

                System.Threading.Thread.Sleep(20);

                if (!comCC_CP.ReadVersion(curAddr, out ver, out er))     //读取版本
                    pass = false;

                System.Threading.Thread.Sleep(20);

                string str = string.Empty;

                int[] setCP = new int[1];
                int[] readCP = new int[1];
                if (!comCC_CP.RWCPSpec  (curAddr, false, setCP, true, ref  readCP, out er))
                    pass = false;
                System.Threading.Thread.Sleep(10);

                string strAdrsData = readCP[0].ToString()+"%";

                showView(curAddr, pass, ver, strAdrsData);

                if (curAddr < System.Convert.ToInt16(txtEndAdrs.Text))
                {
                    curAddr++;
                    rowNum++;
                }
                else
                {
                    showEnd();
                    return;
                }

            }
        }
        /// <summary>
        /// 结束显示
        /// </summary>
        private void showEnd()
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(showEnd));
            else
            {
                btnScan.Text = CLanguage.Lan("扫描");
            }
        }
        /// <summary>
        /// 显示数据
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="result"></param>
        /// <param name="ver"></param>
        /// <param name="data"></param>
        private void showView(int addr, bool result, string ver, string data)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action<int, bool, string, string>(showView), addr, result, ver, data);
            else
            {
                if (result)
                {
                    CC_CPView.Rows.Add(curAddr, "PASS", ver, data);
                    CC_CPView.Rows[rowNum].Cells[1].Style.BackColor = Color.LimeGreen;
                }
                else
                {
                    CC_CPView.Rows.Add(curAddr, "FAIL", ver, data);
                    CC_CPView.Rows[rowNum].Cells[1].Style.BackColor = Color.Red;
                }
                CC_CPView.CurrentCell = CC_CPView.Rows[CC_CPView.Rows.Count - 1].Cells[0];
            }
        }
        #endregion




    }
}
