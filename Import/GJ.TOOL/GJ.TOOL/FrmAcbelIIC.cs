using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.PLUGINS;
using GJ.DEV.I2C;
using GJ.COM;

namespace GJ.TOOL
{
    public partial class FrmAcbelIIC : Form, IChildMsg
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
            if (comIIC != null)
            {
                comIIC.close();
                comIIC = null;
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
        public FrmAcbelIIC()
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

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    Label lbl1 = new Label();

                    lbl1.Dock = DockStyle.Fill;

                    lbl1.Margin = new Padding(2);
                    switch (j)
                    {
                        case 0:
                            lbl1.Text = CLanguage.Lan("运行状态:");
                            break;
                        case 1:
                            lbl1.Text = CLanguage.Lan("输入电压:");
                            break;
                        case 2:
                            lbl1.Text = CLanguage.Lan("输入电流:");
                            break;
                        case 3:
                            lbl1.Text = CLanguage.Lan("输出电压:");
                            break;
                        case 4:
                            lbl1.Text = CLanguage.Lan("输出电流:");
                            break;
                        case 5:
                            lbl1.Text = CLanguage.Lan("温度 1:");
                            break;
                        case 6:
                            lbl1.Text = CLanguage.Lan("温度 2:");
                            break;
                        case 7:
                            lbl1.Text = CLanguage.Lan("温度 3:");
                            break;
                        case 8:
                            lbl1.Text = CLanguage.Lan("风扇1速度:");
                            break;
                        case 9:
                            lbl1.Text = CLanguage.Lan("风扇2速度:");
                            break;
                        case 10:
                            lbl1.Text = CLanguage.Lan("输出功率:");
                            break;
                        case 11:
                            lbl1.Text = CLanguage.Lan("输出功率:");
                            break;
                        case 12:
                            lbl1.Text = CLanguage.Lan("输出电压状态:");
                            break;
                        case 13:
                            lbl1.Text = CLanguage.Lan("输出电流状态:");
                            break;
                        case 14:
                            lbl1.Text = CLanguage.Lan("输入状态:");
                            break;
                        case 15:
                            lbl1.Text = CLanguage.Lan("温度状态1:");
                            break;
                        case 16:
                            lbl1.Text = CLanguage.Lan("温度状态2:");
                            break;
                        case 17:
                            lbl1.Text = CLanguage.Lan("温度状态3:");
                            break;
                        case 18:
                            lbl1.Text = CLanguage.Lan("温度状态4:");
                            break;
                        case 19:
                            lbl1.Text = CLanguage.Lan("错误数量:");
                            break;
                        case 20:
                            lbl1.Text = CLanguage.Lan("On/off 次数:");
                            break;
                        default:
                            break;
                    }
                    lbl1.TextAlign = ContentAlignment.MiddleCenter;

                    labDataName.Add(lbl1);

                    Label lbl2 = new Label();

                    lbl2.Dock = DockStyle.Fill;

                    lbl2.BorderStyle = BorderStyle.Fixed3D;

                    lbl2.Margin = new Padding(2);

                    lbl2.BackColor = Color.Black;

                    lbl2.ForeColor = Color.Cyan;

                    lbl2.Text = "---";

                    lbl2.TextAlign = ContentAlignment.MiddleCenter;

                    labDataValue.Add(lbl2);

                    pnl1.Controls.Add(labDataName[j + i * 21], 0 + i * 2, j + 1);

                    pnl1.Controls.Add(labDataValue[j + i * 21], 1 + i * 2, j + 1);
                }
            }
        }
        /// <summary>
        /// 设置双缓冲,防止界面闪烁
        /// </summary>
        private void SetDoubleBuffered()
        {
            pnl1.GetType().GetProperty("DoubleBuffered",
                                           System.Reflection.BindingFlags.Instance |
                                           System.Reflection.BindingFlags.NonPublic)
                                           .SetValue(pnl1, true, null);
            spc1.Panel1.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(spc1.Panel1, true, null);
            spc1.Panel2.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(spc1.Panel2, true, null);
        }

        #endregion

        #region 字段
        /// <summary>
        /// IIC通讯板
        /// </summary>
        private C_GJ485_I2C_ACBEL comIIC = null;
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

        #region 面板控件
        private List<Label> labDataName = new List<Label>();
        private List<Label> labDataValue = new List<Label>();
        #endregion

        #region 面板回调函数

        /// <summary>
        /// 加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmAcbelIIC_Load(object sender, EventArgs e)
        {
            SetLanguage();

            cmbCom.Items.Clear();
            string[] com = System.IO.Ports.SerialPort.GetPortNames();
            for (int i = 0; i < com.Length; i++)
                cmbCom.Items.Add(com[i]);
            if (com.Length > 0)
                cmbCom.SelectedIndex = 0;

            cmbCH.Items.Clear();
            cmbCH.Items.Add(CLanguage.Lan("通道1"));
            cmbCH.Items.Add(CLanguage.Lan("通道2"));
            cmbCH.Items.Add(CLanguage.Lan("通道3"));
            cmbCH.Items.Add(CLanguage.Lan("通道4"));
            cmbCH.Items.Add(CLanguage.Lan("全部"));
            cmbCH.SelectedIndex = 0;

            cmbRelay.Items.Clear();
            cmbRelay.Items.Add("CH1_PSON_ON");
            cmbRelay.Items.Add("CH1_PSON_OFF");
            cmbRelay.Items.Add("CH2_PSON_ON");
            cmbRelay.Items.Add("CH2_PSON_OFF");
            cmbRelay.Items.Add("CH3_PSON_ON");
            cmbRelay.Items.Add("CH3_PSON_OFF");
            cmbRelay.Items.Add("CH4_PSON_ON");
            cmbRelay.Items.Add("CH4_PSON_OFF");
            cmbRelay.Items.Add("CH1_Sleep_ON");
            cmbRelay.Items.Add("CH2_Sleep_ON");
            cmbRelay.Items.Add("CH3_Sleep_ON");
            cmbRelay.Items.Add("CH4_Sleep_ON");
            cmbRelay.Items.Add("ALL_PSON_ON");
            cmbRelay.Items.Add("ALL_PSON_OFF");
            cmbRelay.Items.Add("ALL_Sleep_ON");
            cmbRelay.SelectedIndex = 0;

            cmbSwitch.Items.Clear();
            cmbSwitch.Items.Add("CH1_PSON_Ctrl");
            cmbSwitch.Items.Add("CH1_Soft_Ctrl");
            cmbSwitch.Items.Add("CH2_PSON_Ctrl");
            cmbSwitch.Items.Add("CH2_Soft_Ctrl");
            cmbSwitch.Items.Add("CH3_PSON_Ctrl");
            cmbSwitch.Items.Add("CH3_Soft_Ctrl");
            cmbSwitch.Items.Add("CH4_PSON_Ctrl");
            cmbSwitch.Items.Add("CH4_Soft_Ctrl");
            cmbSwitch.Items.Add("ALL_PSON_Ctrl");
            cmbSwitch.Items.Add("ALL_Soft_Ctrl");
            cmbSwitch.SelectedIndex = 0;

        }

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

                if (comIIC == null)
                {
                    comIIC = new C_GJ485_I2C_ACBEL();
                    if (!comIIC.open(cmbCom.Text, txtBaud.Text, out er))
                    {
                        labStatus.Text = er;
                        labStatus.ForeColor = Color.Red;
                        comIIC = null;
                        return;
                    }
                    btnOpen.Text = CLanguage.Lan("关闭");
                    labStatus.Text = CLanguage.Lan("成功打开串口.");
                    labStatus.ForeColor = Color.Blue;
                    cmbCom.Enabled = false;
                }
                else
                {
                    comIIC.close();
                    comIIC = null;
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
        /// 读版本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnreadVersion_Click(object sender, EventArgs e)
        {
            try
            {
                btnreadVersion.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                string er = string.Empty;

                string name = string.Empty;

                labVer.Text = "";

                string ver = string.Empty;

                if (!comIIC.ReadVersion(addr, out ver, out er))
                {
                    showInfo(CLanguage.Lan("读取地址[") + addr.ToString("D2") + CLanguage.Lan("]设备版本错误:") + er, true);
                    return;
                }
                labVer.Text = ver;

                showInfo(CLanguage.Lan("读取地址[") + addr.ToString("D2") + CLanguage.Lan("]基本信息OK"));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

                btnreadVersion.Enabled = true;
            }
        }

        /// <summary>
        /// 读取数据 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadData_Click(object sender, EventArgs e)
        {
            try
            {
                btnReadData.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                int startAddr = 0;

                int endAddr = 0;

                string er = string.Empty;

                if (cmbCH.SelectedIndex == 4)
                {
                    startAddr = 0;
                    endAddr = 3;
                }
                else
                {
                    startAddr = cmbCH.SelectedIndex;
                    endAddr = cmbCH.SelectedIndex;
                }

                for (int ich = startAddr; ich < endAddr + 1; ich++)
                {
                    int[] rVal = new int[21];
                    string rStrData = string.Empty;

                    if (!comIIC.GetValueData(addr, ich, ref  rVal, out er))
                    {
                        showInfo(CLanguage.Lan("读取地址[") + addr.ToString("D2") + "_" + (ich + 1).ToString("D2") + CLanguage.Lan("]IIC数据错误:") + er, true);
                        for (int i = 0; i < 21; i++)
                        {
                            labDataValue[i + ich * 21].Text = "--";
                        }

                        return;
                    }

                    string[] showVal = new string[21];
                    showVal[0] = getiicStatus(rVal[0]).ToString();        //运行状态
                    showVal[1] = getlinear11(rVal[1]).ToString();      //输入电压
                    showVal[2] = getlinear11(rVal[2]).ToString();      //输入电流
                    if (chkModel.Checked)         //输出电压
                        showVal[3] = (((double)rVal[3]) / 512).ToString();
                    else
                        showVal[3] = (((double)rVal[3]) / 12).ToString();
                    showVal[4] = getlinear11(rVal[4]).ToString();      //输出电流
                    showVal[5] = getlinear11(rVal[5]).ToString();      //温度 1
                    showVal[6] = getlinear11(rVal[6]).ToString();      //温度 2
                    showVal[7] = getlinear11(rVal[7]).ToString();      //温度 3
                    showVal[8] = getlinear11(rVal[8]).ToString();      //风扇1速度
                    showVal[9] = getlinear11(rVal[9]).ToString();      //风扇2速度
                    showVal[10] = getlinear11(rVal[10]).ToString();     //输出功率
                    showVal[11] = getlinear11(rVal[11]).ToString();     //输入功率
                    showVal[12] = getlinear11(rVal[12]).ToString();      //输出电压状态
                    showVal[13] = getlinear11(rVal[13]).ToString();      //输出电流状态
                    showVal[14] = getlinear11(rVal[14]).ToString();     //输入状态
                    showVal[15] = getlinear11(rVal[15]).ToString();     //温度状态1
                    showVal[16] = getlinear11(rVal[16]).ToString();     //温度状态2
                    showVal[17] = getlinear11(rVal[17]).ToString();     //温度状态3
                    showVal[18] = getlinear11(rVal[18]).ToString();     //温度状态4
                    showVal[19] = getlinear11(rVal[19]).ToString();     //错误数量
                    showVal[20] = getlinear11(rVal[20]).ToString();     //On/off 次数

                    for (int i = 0; i < 21; i++)
                    {
                        labDataValue[i + ich * 21].Text = showVal[i];
                    }
                    showInfo(CLanguage.Lan("读取地址[") + addr.ToString("D2") + "_" + (ich + 1).ToString("D2") +CLanguage.Lan( "]IIC数据正常:") + er);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnReadData.Enabled = true;
            }
        }

        /// <summary>
        /// 清除错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearFail_Click(object sender, EventArgs e)
        {
            try
            {
                btnClearFail.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                int startAddr = 0;

                int endAddr = 0;

                string er = string.Empty;

                if (cmbCH.SelectedIndex == 4)
                {
                    startAddr = 0;
                    endAddr = 3;
                }
                else
                {
                    startAddr = cmbCH.SelectedIndex;
                    endAddr = cmbCH.SelectedIndex;
                }

                for (int ich = startAddr; ich < endAddr + 1; ich++)
                {
                    if (!comIIC.ClearFailNum(addr, ich, out er))
                    {
                        showInfo(CLanguage.Lan("清除地址[") + addr.ToString("D2") + "_" + (ich + 1).ToString("D2") + CLanguage.Lan("]错误次数错误:") + er, true);
                        return;
                    }
                    showInfo(CLanguage.Lan("清除地址[") + addr.ToString("D2") + "_" + (ich + 1).ToString("D2") + CLanguage.Lan("]错误次数正常:") + er);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnClearFail.Enabled = true;
            }
        }

        /// <summary>
        /// 清除ONOFF次数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnclearonoff_Click(object sender, EventArgs e)
        {
            try
            {
                btnclearonoff.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                int startAddr = 0;

                int endAddr = 0;

                string er = string.Empty;

                if (cmbCH.SelectedIndex == 4)
                {
                    startAddr = 0;
                    endAddr = 3;
                }
                else
                {
                    startAddr = cmbCH.SelectedIndex;
                    endAddr = cmbCH.SelectedIndex;
                }

                for (int ich = startAddr; ich < endAddr + 1; ich++)
                {
                    if (!comIIC.ClearFailNum(addr, ich, out er))
                    {
                        showInfo(CLanguage.Lan("清除地址[" )+ addr.ToString("D2") + "_" + (ich + 1).ToString("D2") + CLanguage.Lan("]ONOFF次数错误:") + er, true);
                        return;
                    }
                    showInfo(CLanguage.Lan("清除地址[") + addr.ToString("D2") + "_" + (ich + 1).ToString("D2") + CLanguage.Lan("]ONOFF正常:") + er);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnclearonoff.Enabled = true;
            }
        }

        /// <summary>
        /// 设定产品地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnsetUUTAdrs_Click(object sender, EventArgs e)
        {
            try
            {
                btnsetUUTAdrs.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                int startAddr = 0;

                int endAddr = 0;

                string er = string.Empty;

                if (cmbCH.SelectedIndex == 4)
                {
                    startAddr = 0;
                    endAddr = 3;
                }
                else
                {
                    startAddr = cmbCH.SelectedIndex;
                    endAddr = cmbCH.SelectedIndex;
                }
                int[] setAdrs = new int[4];
                int[] readAdrs = new int[4];
                for (int i = 0; i < 4; i++)
                {
                    if (cmbCH.SelectedIndex == 4)
                        setAdrs[i] = System.Convert.ToInt16(txtUUTAdrs.Text, 16) + i * 2;
                    else
                    {
                        if (i == cmbCH.SelectedIndex)
                            setAdrs[i] = System.Convert.ToInt16(txtUUTAdrs.Text, 16) + i * 2;
                        else
                            setAdrs[i] = System.Convert.ToInt16("D0", 16);
                    }
                }
                if (!comIIC.SetUUTAdrs(addr, true, setAdrs, true, ref  readAdrs, out er))
                {
                    showInfo(CLanguage.Lan("设定IIC板地址[") + addr.ToString("D2") + CLanguage.Lan("]产品地址[") + txtUUTAdrs.Text + CLanguage.Lan("]错误次数错误:") + er, true);
                    return;
                }
                for (int ich = startAddr; ich < endAddr + 1; ich++)
                {
                    if (setAdrs[ich] != readAdrs[ich])
                    {
                        showInfo(CLanguage.Lan("设定IIC板地址[") + addr.ToString("D2") + CLanguage.Lan("]产品地址[") + txtUUTAdrs.Text + CLanguage.Lan("]与读取地址") + readAdrs[ich].ToString("X2") + CLanguage.Lan("不匹配:") + er, true);
                        return;
                    }
                }
                showInfo(CLanguage.Lan("设定IIC板地址[") + addr.ToString("D2") + CLanguage.Lan("]产品地址[") + txtUUTAdrs.Text + CLanguage.Lan("]正常:") + er);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnsetUUTAdrs.Enabled = true;
            }
        }

        /// <summary>
        /// 设定开关产品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRelay_Click(object sender, EventArgs e)
        {
            try
            {
                btnRelay.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                string er = string.Empty;
                int[] sType = new int[12]; 
                if (cmbRelay.SelectedIndex==12 )
                {
                    for (int i=0 ;i<12 ;i++)
                    {
                        sType[i]=0;
                    }
                    sType[0]=1;
                    sType[2]=1;
                    sType[4]=1;
                    sType[6]=1;
                }
                else  if (cmbRelay.SelectedIndex==13 )
                {
                    for (int i=0 ;i<12 ;i++)
                    {
                        sType[i]=0;
                    }
                    sType[1]=1;
                    sType[3]=1;
                    sType[4]=1;
                    sType[7]=1;
                }
                else if (cmbRelay.SelectedIndex == 14)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        sType[i] = 0;
                    }
                    sType[8] = 1;
                    sType[9] = 1;
                    sType[10] = 1;
                    sType[11] = 1;
                }
                else
                {
                    for (int i = 0; i < 12; i++)
                    {
                        if (cmbRelay.SelectedIndex == i)
                            sType[i] = 1;
                        else
                            sType[i] = 0;
                    }
                }
                string strValue=string.Empty ;
                int[]  setValue =new int [1];
                int[] readValue = new int[1];
                for (int i = 0; i < 12; i++)
                {                     
                    strValue =sType[i].ToString ()+strValue;
                }
                setValue[0] = System.Convert.ToInt16(strValue, 2);
                if (!comIIC.SetUUTRelay(addr, true, setValue, ref  readValue, out er))
                {
                    showInfo(CLanguage.Lan("IIC板地址[") + addr.ToString("D2") + CLanguage.Lan("]开关产品错误:") + er, true);
                    return;
                }

                if (setValue[0] != readValue[0])
                    {
                        showInfo(CLanguage.Lan("IIC板地址[") + addr.ToString("D2") + CLanguage.Lan("]开关产品与信号读取不一致:") + er, true);
                        return;
                    }

                showInfo(CLanguage.Lan("板地址[") + addr.ToString("D2") + CLanguage.Lan("]开关产品正常:") + er);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnRelay.Enabled = true;
            }
        }
        /// <summary>
        /// 设定开关配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSwitch_Click(object sender, EventArgs e)
        {
            try
            {
                btnSwitch.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                string er = string.Empty;
                int[] sType = new int[8];

                if (cmbSwitch.SelectedIndex == 8)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        sType[i] = 0;
                    }
                    sType[0] = 1;
                    sType[2] = 1;
                    sType[4] = 1;
                    sType[6] = 1;
                }
                else if (cmbSwitch.SelectedIndex == 9)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        sType[i] = 0;
                    }
                    sType[1] = 1;
                    sType[3] = 1;
                    sType[4] = 1;
                    sType[7] = 1;
                }
               else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (cmbSwitch.SelectedIndex == i)
                            sType[i] = 1;
                        else
                            sType[i] = 0;
                    }
                }
                string strValue = string.Empty;
                int[] setValue = new int[1];
                int[] readValue = new int[1];
                for (int i = 0; i < 8; i++)
                {
                    strValue = sType[i].ToString() + strValue;
                }
                setValue[0] = System.Convert.ToInt16(strValue, 2);
                if (!comIIC.SetUUTSwicth(addr,  setValue,  out er))
                {
                    showInfo(CLanguage.Lan("IIC板地址[") + addr.ToString("D2") + CLanguage.Lan("]开关设置错误:") + er, true);
                    return;
                }

                showInfo(CLanguage.Lan("板地址[") + addr.ToString("D2") + CLanguage.Lan("]开关设置正常:") + er);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnSwitch.Enabled = true;
            }
        } 

        #endregion

        # region 扫描IIC板
        /// <summary>
        /// 开始扫描IIC板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtScan_Click(object sender, EventArgs e)
        {

            try
            {
                if (btnScan.Text == CLanguage.Lan("扫描"))
                {
                    if (!checkSystem())
                        return;

                    btnScan.Text = CLanguage.Lan("停止");

                    curAddr = System.Convert.ToInt16(txtStartAdrs.Text);

                    IICView.Rows.Clear();

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

                System.Threading.Thread.Sleep(10);

                if (!comIIC.ReadVersion(curAddr , out ver, out er))     //读取版本
                    pass = false;

                System.Threading.Thread.Sleep(10);

                string str = string.Empty;

                int[] setAdrs = new int[4];
                int[] readAdrs = new int[4];
                if (!comIIC.SetUUTAdrs(curAddr, false , setAdrs,true, ref  readAdrs, out er))
                    pass = false;
                string  strAdrsData = string.Empty;
                for (int i = 0; i < 4; i++)
                {
                    strAdrsData += readAdrs[i].ToString("X2") + "||";
                }
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
                    IICView.Rows.Add(curAddr, "PASS", ver, data);
                    IICView.Rows[rowNum].Cells[1].Style.BackColor = Color.LimeGreen;
                }
                else
                {
                    IICView.Rows.Add(curAddr, "FAIL", ver, data);
                    IICView.Rows[rowNum].Cells[1].Style.BackColor = Color.Red;
                }
                IICView.CurrentCell = IICView.Rows[IICView.Rows.Count - 1].Cells[0];
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
            if (comIIC == null)
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
        /// <summary>
        ///linear11 数据格式读取
        ///这个格式数据为用 16 位数据表示小数，高 5 位为指数位，低 11 位为小数位。计算公式为：
        ///值 = 2 ^ 高5位 * 低 11 位。其中高5位和低11位都是有符号数，当最高位为 0 时:
        ///2 ^ 高5位 = 2 ^ (16位数据 >> 11)，当最高位为 1 时: 2 ^ 高5位 = 1 / 2 ^ (0x20 – (16位数据 >> 11))。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static double getlinear11(int data)
        {
            int high5;
            double d, low11;
            high5 = data >> 11;
            low11 = data & 0x7FF;
            //可以不考虑低 11 位为负数的情况，因为电压电流不会出现负数。
            if (low11 >= 0x400)
                low11 = -(0x800 - low11);
            if ((data & 0x8000) == 0)
            {
                d = (1 << high5) * low11;
            }
            else
            {
                d = low11 / (1 << (0x20 - high5));
            }
            return d;
        }

        /// <summary>
        /// 读取IIC板运行状态
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string getiicStatus(int data)
        {
            string getStr = string.Empty;
            switch (data)
            {
                case 1:
                    getStr = CLanguage.Lan("通信、存储或逻辑出错;");
                    break;
                case 2:
                    getStr = CLanguage.Lan("温度错误或报警;");
                    break;
                case 3:
                    getStr = CLanguage.Lan("输入欠压错误;");
                    break;
                case 4:
                    getStr = CLanguage.Lan("输出过流错误;");
                    break;
                case 5:
                    getStr = CLanguage.Lan("输出过压错误;");
                    break;
                case 6:
                    getStr = CLanguage.Lan("模块无输出;");
                    break;
                case 7:
                    getStr = CLanguage.Lan("模块忙碌;");
                    break;
                case 8:
                    getStr = CLanguage.Lan("未知错误;");
                    break;
                case 9:
                    getStr = CLanguage.Lan("其他错误;");
                    break;
                case 10:
                    getStr = CLanguage.Lan("风扇错误或警告;");
                    break;
                case 11:
                    getStr = CLanguage.Lan("功率偏低;");
                    break;
                case 12:
                    getStr = CLanguage.Lan("制造商信息错误;");
                    break;
                case 13:
                    getStr = CLanguage.Lan("输入电压、电流或功率错误或警告;");
                    break;
                case 14:
                    getStr = CLanguage.Lan("输出电流或功率错误;");
                    break;
                case 15:
                    getStr = CLanguage.Lan("输出错误或警告;");
                    break;
                default:
                    getStr = CLanguage.Lan("有错误，但不在 1~7 位所列的范围;");
                    break;
            }
            return getStr;
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
                    this.Text = "康舒IIC调试";
                    break;
                case CLanguage.EL.英语:
                    this.Text = "IIC Tool";
                    break;
                case CLanguage.EL.繁体:
                    this.Text = CLanguage.Lan("康舒IIC调试");
                    btnClearFail.Text = CLanguage.Lan("清除不良");
                    btnclearonoff.Text = CLanguage.Lan("清除ONOFF");
                    btnOpen.Text = CLanguage.Lan("打开");
                    btnReadData.Text = CLanguage.Lan("读数据");
                    btnreadVersion.Text = CLanguage.Lan("读版本");
                    btnRelay.Text = CLanguage.Lan("设定");
                    btnScan.Text = CLanguage.Lan("扫描");
                    btnsetUUTAdrs.Text = CLanguage.Lan("设产品地址");
                    btnSwitch.Text = CLanguage.Lan("设定");
                    chkModel.Text = CLanguage.Lan("联想机种");
                    Column1.HeaderText = CLanguage.Lan("地址");
                    Column2.HeaderText = CLanguage.Lan("结果");
                    Column3.HeaderText = CLanguage.Lan("版本");
                    Column4.HeaderText = CLanguage.Lan("产品地址");
                    label1.Text = CLanguage.Lan("波特率:");
                    label10.Text = CLanguage.Lan("数据");
                    label11.Text = CLanguage.Lan("通道号:");
                    label12.Text = CLanguage.Lan("通道2");
                    label13.Text = CLanguage.Lan("产品地址:");
                    label14.Text = CLanguage.Lan("起始地址:");
                    label15.Text = CLanguage.Lan("结束地址:");
                    label16.Text = CLanguage.Lan("开关产品:");
                    label17.Text = CLanguage.Lan("开关配置:");
                    label2.Text = CLanguage.Lan("IIC版本:");
                    label29.Text = CLanguage.Lan("串口号:");
                    label3.Text = CLanguage.Lan("IIC地址:");
                    label4.Text = CLanguage.Lan("通道3");
                    label5.Text = CLanguage.Lan("数据");
                    label6.Text = CLanguage.Lan("通道1");
                    label7.Text = CLanguage.Lan("数据");
                    label8.Text = CLanguage.Lan("数据");
                    label9.Text = CLanguage.Lan("通道4");
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
