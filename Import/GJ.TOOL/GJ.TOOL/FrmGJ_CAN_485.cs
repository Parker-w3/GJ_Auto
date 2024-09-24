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
    public partial class FrmGJ_CAN_485 : Form, IChildMsg
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
            if (comCan != null)
            {
                comCan.close();
                comCan = null;
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
        public FrmGJ_CAN_485()
        {
            InitializeComponent();

            IntialControl();

            SetDoubleBuffered();

            load();
        }
        #endregion

        #region 初始化
        private void FrmGJ_CAN_485_Load(object sender, EventArgs e)
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
            spcSys.Panel1.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(spcSys.Panel1, true, null);
            spcSys.Panel2.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(spcSys.Panel2, true, null);

            splitContainer1.Panel1.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(splitContainer1.Panel1, true, null);

            splitContainer1.Panel2.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(splitContainer1.Panel2, true, null);

            splitContainer2.Panel1.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(splitContainer2.Panel1, true, null);

            splitContainer2.Panel2.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(splitContainer2.Panel2, true, null);

            panel1.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(panel1, true, null);

        }

        #endregion

        #region 字段
        /// <summary>
        /// CAN通讯盒
        /// </summary>
        private C_GJCAN_RS485 comCan = null;

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

                if (comCan == null)
                {
                    comCan = new C_GJCAN_RS485();
                    if (!comCan.open(cmbCom.Text, txtBaud.Text, out er))
                    {
                        labStatus.Text = er;
                        labStatus.ForeColor = Color.Red;
                        comCan = null;
                        return;
                    }
                    btnOpen.Text = CLanguage.Lan("关闭");
                    labStatus.Text = CLanguage.Lan("成功打开串口.");
                    labStatus.ForeColor = Color.Blue;
                    cmbCom.Enabled = false;
                }
                else
                {
                    comCan.close();
                    comCan = null;
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

                int setAdrs = 0;
                int readAdrs =0;

                setAdrs = System.Convert.ToInt16(txtAddr.Text);     //获取10进制的数

                if (!comCan.WtAddrs(addr,  setAdrs, out er))
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

                int setbaud = 0;
                int readbaud = 0;

                if (!comCan.RWBaud(addr, false, setbaud, true, out readbaud, out er))
                {
                    showInfo(CLanguage.Lan("读取地址[") + addr.ToString("D2") + CLanguage.Lan("]波特率失败:") + er, true);
                    return;
                }
                txtBaud.Text = readbaud.ToString() + ",n,8,1";

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

                int setbaud = 0;
                int readbaud =0;

                setbaud = System.Convert.ToInt32(Str[0]);     //获取10进制的数

                if (!comCan.RWBaud(addr, true, setbaud, false, out readbaud, out er))
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
        /// 读取CAN波特率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadCanBaud_Click(object sender, EventArgs e)
        {
            try
            {
                btnReadCanBaud.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                string er = string.Empty;

                string name = string.Empty;

                txtCanBaud.Text = "";

                string baud = string.Empty;

                int setCanbaud =0;
                int reaCandbaud =0;

                if (!comCan.RWCANBaud(addr, false, setCanbaud, true, ref reaCandbaud, out er))
                {
                    showInfo(CLanguage.Lan("读取地址[") + addr.ToString("D2") + CLanguage.Lan("]CAN波特率失败:") + er, true);
                    return;
                }

                txtCanBaud.Text = reaCandbaud.ToString();
                showInfo(CLanguage.Lan("读取地址[") + addr.ToString("D2") + CLanguage.Lan("]CAN波特率OK"));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

                btnReadCanBaud.Enabled = true;
            }
        }

        /// <summary>
        /// 写入CAN波特率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWriteCanBaud_Click(object sender, EventArgs e)
        {
            try
            {
                txtWriteCanBaud.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                string er = string.Empty;

                string name = string.Empty;

                string Canbaud = txtCanBaud.Text.Trim();

                txtCanBaud.Text = "";

                int Cansetbaud = 0;
                int Canreadbaud = 0;

                Cansetbaud = System.Convert.ToInt32(Canbaud);     //获取10进制的数

                if (!comCan.RWCANBaud(addr, true, Cansetbaud, false, ref Canreadbaud, out er))
                {
                    showInfo(CLanguage.Lan("写入地址[") + addr.ToString("D3") + CLanguage.Lan("]CAN波特率失败:") + er, true);
                    return;
                }
                txtCanBaud.Text = Canbaud;
                showInfo(CLanguage.Lan("写入地址[") + addr.ToString("D3") + CLanguage.Lan("]CAN波特率OK"));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                txtWriteCanBaud.Enabled = true;
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

                if (!comCan.ReadVersion(addr, out readVer, out er))
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

        /// <summary>
        /// 读取特种机种模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadModelNo_Click(object sender, EventArgs e)
        {
            try
            {
                btnReadModelNo.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                string er = string.Empty;

                string name = string.Empty;

                txtModelNo.Text = "";

                string baud = string.Empty;

                int setNo = 0;
                int readNo =0;

                if (!comCan.RWModelSpec(addr, false, setNo, true, ref readNo, out er))
                {
                    showInfo(CLanguage.Lan("读取地址[") + addr.ToString("D2") + CLanguage.Lan("]特定机种数据编号,失败:") + er, true);
                    return;
                }

                txtModelNo.Text = readNo.ToString();
                showInfo(CLanguage.Lan("读取地址[") + addr.ToString("D2") + CLanguage.Lan("]特定机种数据编号OK"));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnReadModelNo.Enabled = true;
            }
        }

        /// <summary>
        /// 写入特种机种模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWriteModelNo_Click(object sender, EventArgs e)
        {
            try
            {
                btnWriteModelNo.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                string er = string.Empty;

                string name = string.Empty;

                string modelNo = txtModelNo.Text.Trim();

                int setNo = 0;
                int readNo = 0;

                setNo = System.Convert.ToInt32(modelNo);     //获取10进制的数

                if (!comCan.RWModelSpec(addr, true, setNo, false, ref readNo, out er))
                {
                    showInfo(CLanguage.Lan("写入地址[") + addr.ToString("D3") + CLanguage.Lan("]特定机种数据编号,失败:") + er, true);
                    return;
                }
                txtModelNo.Text = readNo.ToString();

                showInfo(CLanguage.Lan("写入地址[") + addr.ToString("D3") + CLanguage.Lan("]特定机种数据编号OK"));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnWriteModelNo.Enabled = true;
            }
        }

        /// <summary>
        /// 获取CAN帧数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetCanNum_Click(object sender, EventArgs e)
        {
            try
            {
                btnGetCanNum.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                string er = string.Empty;

                string name = string.Empty;

                txtCanDataNum.Text = "";

                int setNum = 0;
                int readNum =0;

                if (!comCan.RWCanNum(addr, false, setNum, true, ref readNum, out er))
                {
                    showInfo(CLanguage.Lan("读取地址[") + addr.ToString("D3") + CLanguage.Lan("]CAN模块帧数量,失败:") + er, true);
                    return;
                }

                txtCanDataNum.Text = readNum.ToString();
                showInfo(CLanguage.Lan("读取地址[") + addr.ToString("D3") + CLanguage.Lan("]CAN模块帧数量OK"));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnGetCanNum.Enabled = true;
            }
        }

        /// <summary>
        /// 清除CAN的帧数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearCanNum_Click(object sender, EventArgs e)
        {
            try
            {
                btnClearCanNum.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                string er = string.Empty;

                string name = string.Empty;

                string clearNum = txtCanDataNum.Text.Trim();

                int setNum = 0;
                int readNum = 0;

                setNum = System.Convert.ToInt32(clearNum);     //获取10进制的数

                if (!comCan.RWCanNum(addr, true, setNum, false, ref readNum, out er))
                {
                    showInfo(CLanguage.Lan("写入地址[") + addr.ToString("D3") + CLanguage.Lan("]清除帧,失败:") + er, true);
                    return;
                }
                txtCanDataNum.Text = readNum.ToString();

                showInfo(CLanguage.Lan("写入地址[") + addr.ToString("D3") + CLanguage.Lan("]清除帧OK"));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnClearCanNum.Enabled = true;
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
            if (comCan == null)
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
                    this.Text = "GJ_CAN_485调试工具";
                    break;
                case CLanguage.EL.英语:
                    this.Text = "GJ_CAN_485 Tool";
                    break;
                case CLanguage.EL.繁体:

                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 发送Can数据

        #endregion

        #region 面板控件

        /// <summary>
        /// CAN模块输出控件
        /// </summary>
        private class Can_Ctrl
        {
            /// <summary>
            /// 帧类型 1表示扩展帧，否则标准帧
            /// </summary>
            public CheckBox chkCanUse = new CheckBox();

            /// <summary>
            /// 帧类型 1表示扩展帧，否则标准帧
            /// </summary>
            public ComboBox cmbExternFlag = new ComboBox();

            /// <summary>
            /// 帧格式 1 表示远程帧，否则数据帧
            /// </summary>
            public ComboBox cmbRemoteFlag = new ComboBox();

            /// <summary>
            /// 发送ID
            /// </summary>
            public TextBox txtid = new TextBox();

            /// <summary>
            /// 发送数据
            /// </summary>
            public TextBox txtdata = new TextBox();

            /// <summary>
            /// 帧发送延时
            /// </summary>
            public TextBox txtsendTime = new TextBox();

        }
        /// <summary>
        /// Can发送框
        /// </summary>
        private TableLayoutPanel canSetPanel = null;
        /// <summary>
        /// Can发送配置控件
        /// </summary>
        private List<Can_Ctrl> iicControl = new List<Can_Ctrl>();

        /// <summary>
        /// Can配置发送最大帧数
        /// </summary>
        private int _CanNum = 10;

        #endregion

        #region 方法
        /// <summary>
        /// 加载设置
        /// </summary>
        /// <param name="idNo"></param>
        /// <param name="Output"></param>
        public void load()
        {
            try
            {
                SetUILanguage();

                refreshCanPara();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region 面板方法

        /// 刷新电压输出规格
        /// </summary>
        private void refreshCanPara()
        {
            try
            {
                if (canSetPanel != null)
                {
                    canSetPanel.Dispose();
                    canSetPanel = null;
                }
                if (canSetPanel != null)
                {
                    canSetPanel.Dispose();
                    canSetPanel = null;
                }
                //初始化电压控件
                int rowNum = _CanNum;

                canSetPanel = new TableLayoutPanel();

                canSetPanel.Dock = DockStyle.Fill;

                canSetPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

                canSetPanel.RowCount = rowNum + 2;

                canSetPanel.AutoScroll = false;

                canSetPanel.ColumnCount = 6;

                canSetPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
                for (int i = 0; i < rowNum + 1; i++)
                    canSetPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
                canSetPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

                canSetPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
                canSetPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                canSetPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                canSetPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
                canSetPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 140));
                canSetPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));

                Label lab1 = new Label();
                lab1.Dock = DockStyle.Fill;
                lab1.TextAlign = ContentAlignment.MiddleCenter;
                lab1.Text = CLanguage.Lan("数据");
                canSetPanel.Controls.Add(lab1, 0, 0);

                Label lab2 = new Label();
                lab2.Dock = DockStyle.Fill;
                lab2.TextAlign = ContentAlignment.MiddleCenter;
                lab2.Text = CLanguage.Lan("帧类型");
                canSetPanel.Controls.Add(lab2, 1, 0);

                Label lab3 = new Label();
                lab3.Dock = DockStyle.Fill;
                lab3.TextAlign = ContentAlignment.MiddleCenter;
                lab3.Text = CLanguage.Lan("帧格式");
                canSetPanel.Controls.Add(lab3, 2, 0);

                Label lab4 = new Label();
                lab4.Dock = DockStyle.Fill;
                lab4.TextAlign = ContentAlignment.MiddleCenter;
                lab4.Text = CLanguage.Lan("帧ID(HEX)");
                canSetPanel.Controls.Add(lab4, 3, 0);

                Label lab5 = new Label();
                lab5.Dock = DockStyle.Fill;
                lab5.TextAlign = ContentAlignment.MiddleCenter;
                lab5.Text = CLanguage.Lan("帧数据(HEX)");
                canSetPanel.Controls.Add(lab5, 4, 0);

                Label lab6 = new Label();
                lab6.Dock = DockStyle.Fill;
                lab6.TextAlign = ContentAlignment.MiddleCenter;
                lab6.Text = CLanguage.Lan("发送间隔(ms)");
                canSetPanel.Controls.Add(lab6, 5, 0);

                iicControl.Clear();

                for (int i = 0; i < rowNum; i++)
                {
                    iicControl.Add(new Can_Ctrl());

                    iicControl[i].chkCanUse.Dock = DockStyle.Fill;
                    iicControl[i].chkCanUse.CheckAlign = ContentAlignment.MiddleLeft;
                    iicControl[i].chkCanUse.Checked = false;
                    iicControl[i].chkCanUse.Margin = new Padding(20, 0, 0, 0);
                    iicControl[i].chkCanUse.Name = "Chan" + i.ToString();
                    iicControl[i].chkCanUse.Text = "Chan" + (i + 1).ToString();

                    iicControl[i].cmbExternFlag.Dock = DockStyle.Fill;
                    iicControl[i].cmbExternFlag.Margin = new Padding(1);
                    iicControl[i].cmbExternFlag.DropDownStyle = ComboBoxStyle.DropDownList;
                    iicControl[i].cmbExternFlag.Font = new System.Drawing.Font("微软雅黑", 8F);
                    iicControl[i].cmbExternFlag.Items.Clear();
                    iicControl[i].cmbExternFlag.Items.Add("0:标准帧");
                    iicControl[i].cmbExternFlag.Items.Add("1:扩展帧");
                    iicControl[i].cmbExternFlag.SelectedIndex = 0;

                    iicControl[i].cmbRemoteFlag.Dock = DockStyle.Fill;
                    iicControl[i].cmbRemoteFlag.Margin = new Padding(1);
                    iicControl[i].cmbRemoteFlag.DropDownStyle = ComboBoxStyle.DropDownList;
                    iicControl[i].cmbRemoteFlag.Font = new System.Drawing.Font("微软雅黑", 7F);
                    iicControl[i].cmbRemoteFlag.Items.Clear();
                    iicControl[i].cmbRemoteFlag.Items.Add("0:数据帧");
                    iicControl[i].cmbRemoteFlag.Items.Add("1:远程帧");
                    iicControl[i].cmbRemoteFlag.SelectedIndex = 0;

                    iicControl[i].txtid.Dock = DockStyle.Fill;
                    iicControl[i].txtid.TextAlign = HorizontalAlignment.Center;
                    iicControl[i].txtid.Margin = new Padding(1);
                    iicControl[i].txtid.Text = "";

                    iicControl[i].txtdata.Dock = DockStyle.Fill;
                    iicControl[i].txtdata.TextAlign = HorizontalAlignment.Center;
                    iicControl[i].txtdata.Margin = new Padding(1);
                    iicControl[i].txtdata.Text = "";

                    iicControl[i].txtsendTime.Dock = DockStyle.Fill;
                    iicControl[i].txtsendTime.TextAlign = HorizontalAlignment.Center;
                    iicControl[i].txtsendTime.Margin = new Padding(1);
                    iicControl[i].txtsendTime.Text = "";

                    canSetPanel.Controls.Add(iicControl[i].chkCanUse, 0, i + 1);
                    canSetPanel.Controls.Add(iicControl[i].cmbExternFlag, 1, i + 1);
                    canSetPanel.Controls.Add(iicControl[i].cmbRemoteFlag, 2, i + 1);
                    canSetPanel.Controls.Add(iicControl[i].txtid, 3, i + 1);
                    canSetPanel.Controls.Add(iicControl[i].txtdata, 4, i + 1);
                    canSetPanel.Controls.Add(iicControl[i].txtsendTime, 5, i + 1);

                }
                panel1.Controls.Add(canSetPanel);

            }
            catch (Exception)
            {

                throw;
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

                    break;
                case CLanguage.EL.英语:
                    break;
                case CLanguage.EL.繁体:

                    break;

                default:
                    break;
            }

        }
        #endregion

        # region 扫描CAN串口
        /// <summary>
        /// 开始扫描CAN串口
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

                    CANView.Rows.Clear();

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

                if (!comCan.ReadVersion(curAddr, out ver, out er))     //读取版本
                    pass = false;

                System.Threading.Thread.Sleep(20);

                string str = string.Empty;

                int setAdrs = 0;
                int readAdrs = 0;
                if (!comCan.RWCANBaud(curAddr, false, setAdrs, true, ref  readAdrs, out er))
                    pass = false;
                System.Threading.Thread.Sleep(10);

                string strAdrsData = readAdrs.ToString()+"K";

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
                    CANView.Rows.Add(curAddr, "PASS", ver, data);
                    CANView.Rows[rowNum].Cells[1].Style.BackColor = Color.LimeGreen;
                }
                else
                {
                    CANView.Rows.Add(curAddr, "FAIL", ver, data);
                    CANView.Rows[rowNum].Cells[1].Style.BackColor = Color.Red;
                }
                CANView.CurrentCell = CANView.Rows[CANView.Rows.Count - 1].Cells[0];
            }
        }
        #endregion

 


    }
}
