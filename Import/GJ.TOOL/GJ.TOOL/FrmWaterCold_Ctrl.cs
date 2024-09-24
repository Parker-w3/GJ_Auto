using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.PLUGINS;
using GJ.DEV.WaterCold ;
using GJ.COM;

namespace GJ.TOOL
{
    public partial class FrmWaterCold_Ctrl : Form, IChildMsg
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
            if (comWaterCold != null)
            {
                comWaterCold.close();
                comWaterCold = null;
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
        public FrmWaterCold_Ctrl()
        {
            InitializeComponent();

            IntialControl();

            SetDoubleBuffered();

        }
        #endregion

        #region 初始化

        /// <summary>
        /// 绑定控件
        /// </summary>
        private void IntialControl()
        {
            txtWaterFlow = new TextBox[] { txtWaterFlow0, txtWaterFlow1, txtWaterFlow2 };
            txtWaterTemp = new TextBox[] { txtWaterTemp0, txtWaterTemp1, txtWaterTemp2 };
            txtWaterPressure = new TextBox[] { txtWaterPressure0, txtWaterPressure1, txtWaterPressure2 };
            btnWaterFlow = new Button[] { btnWaterFlow0, btnWaterFlow1, btnWaterFlow2 };
            btnWaterTemp = new Button[] { btnWaterTemp0, btnWaterTemp1, btnWaterTemp2 };
            btnWaterPressure = new Button[] { btnWaterPressure0, btnWaterPressure1, btnWaterPressure2 };
            btnInWater = new Button[] { btnInWater0, btnInWater1, btnInWater2 };
            btnBackWater = new Button[] { btnBackWater0, btnBackWater1, btnBackWater2 };
            btnClose = new Button[] { btnClose0, btnClose1, btnClose2 };
            lblStatus = new Label[] { lblStatus0, lblStatus1, lblStatus2 };
            lblWaterFlow = new Label[] { lblWaterFlow0, lblWaterFlow1, lblWaterFlow2 };
            lblWaterPressure = new Label[] { lblWaterPressure0, lblWaterPressure1, lblWaterPressure2 };
            lblInWaterTemp = new Label[] { lblInWaterTemp0, lblInWaterTemp1, lblInWaterTemp2 };
            lblBackWaterTemp = new Label[] { lblBackWaterTemp0, lblBackWaterTemp1, lblBackWaterTemp2 };
        }

        /// <summary>
        /// 初始化界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmWaterCold_Ctrl_Load(object sender, EventArgs e)
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
        /// 设置双缓冲,防止界面闪烁
        /// </summary>
        private void SetDoubleBuffered()
        {

        }

        #endregion

        #region 字段
        /// <summary>
        /// CAN通讯盒
        /// </summary>
        private C_JY_Ctrl comWaterCold = null;

        /// <summary>
        /// 当前地址
        /// </summary>
        private int curAddr = 0;

        /// <summary>
        /// 取消
        /// </summary>
        private bool cancel = false;
        #endregion

        #region 面板控件

        /// <summary>
        /// 水流
        /// </summary>
        private TextBox[] txtWaterFlow = null;

        /// <summary>
        /// 水温
        /// </summary>
        private TextBox[] txtWaterTemp = null;

        /// <summary>
        /// 水压 
        /// </summary>
        private TextBox[] txtWaterPressure = null;

        /// <summary>
        /// 水流设定
        /// </summary>
        private Button[] btnWaterFlow = null;

        /// <summary>
        /// 水温设定
        /// </summary>
        private Button[] btnWaterTemp = null;

        /// <summary>
        /// 水压设定
        /// </summary>
        private Button[] btnWaterPressure = null;

        /// <summary>
        /// 进水
        /// </summary>
        private Button[] btnInWater = null;

        /// <summary>
        /// 吹水
        /// </summary>
        private Button[] btnBackWater = null;

        /// <summary>
        /// 关闭
        /// </summary>
        private Button[] btnClose = null;

        /// <summary>
        /// 状态
        /// </summary>
        private Label[] lblStatus = null;

        /// <summary>
        /// 水流状态
        /// </summary>
        private Label[] lblWaterFlow = null;

        /// <summary>
        /// 水压状态
        /// </summary>
        private Label[] lblWaterPressure = null;

        /// <summary>
        /// 进水温度
        /// </summary>
        private Label[] lblInWaterTemp = null;

        /// <summary>
        /// 回水温度
        /// </summary>
        private Label[] lblBackWaterTemp = null;


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

                if (comWaterCold == null)
                {
                    comWaterCold = new C_JY_Ctrl();
                    if (!comWaterCold.open(cmbCom.Text, txtBaud.Text, out er))
                    {
                        labStatus.Text = er;
                        labStatus.ForeColor = Color.Red;
                        comWaterCold = null;
                        return;
                    }
                    btnOpen.Text = CLanguage.Lan("关闭");
                    labStatus.Text = CLanguage.Lan("成功打开串口.");
                    labStatus.ForeColor = Color.Blue;
                    cmbCom.Enabled = false;
                }
                else
                {
                    comWaterCold.close();
                    comWaterCold = null;
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
        /// 设定水流
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWaterFlow_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;

            int index = Convert.ToInt16(b.Tag);

            string er = string.Empty;
            try
            {
                btnWaterFlow[index].Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                int regAdrs=6+index*1;

                int val = Convert.ToInt32(txtWaterFlow[index].Text) * 10;

                if (!comWaterCold.SetData(addr, regAdrs, val, out er))
                {
                    showInfo(CLanguage.Lan("设定PLC地址[") + addr.ToString("D2") + "_" + regAdrs.ToString("D2") + CLanguage.Lan("]水流参数失败:") + er, true);
                    return;
                }

                showInfo(CLanguage.Lan("设定PLC地址[") + addr.ToString("D2") + "_" + regAdrs.ToString("D2") + CLanguage.Lan("]水流参数成功"));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnWaterFlow[index].Enabled = true;
            }
        }

        /// <summary>
        /// 水温设定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWaterTemp_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;

            int index = Convert.ToInt16(b.Tag);

            string er = string.Empty;
            try
            {
                btnWaterTemp[index].Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                int regAdrs = 9;

                int val = Convert.ToInt32(txtWaterTemp[index].Text) * 10;

                if (!comWaterCold.SetData(addr, regAdrs, val, out er))
                {
                    showInfo(CLanguage.Lan("设定PLC地址[") + addr.ToString("D2") + "_" + regAdrs.ToString("D2") + CLanguage.Lan("]温度设定失败:") + er, true);
                    return;
                }

                showInfo(CLanguage.Lan("设定PLC地址[") + addr.ToString("D2") + "_" + regAdrs.ToString("D2") + CLanguage.Lan("]温度设定成功"));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnWaterTemp[index].Enabled = true;
            }
        }

        /// <summary>
        /// 水压设定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWaterPressure_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;

            int index = Convert.ToInt16(b.Tag);

            string er = string.Empty;
            try
            {
                btnWaterPressure[index].Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                int regAdrs = 10;

                int val = Convert.ToInt32(txtWaterPressure[index].Text)*10 ;

                if (!comWaterCold.SetData(addr, regAdrs, val, out er))
                {
                    showInfo(CLanguage.Lan("设定PLC地址[") + addr.ToString("D2") + "_" + regAdrs.ToString("D2") + CLanguage.Lan("]水压设定失败:") + er, true);
                    return;
                }

                showInfo(CLanguage.Lan("设定PLC地址[") + addr.ToString("D2") + "_" + regAdrs.ToString("D2") + CLanguage.Lan("]水压设定成功"));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnWaterPressure[index].Enabled = true;
            }
        }

        /// <summary>
        /// 设定运水
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInWater_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;

            int index = Convert.ToInt16(b.Tag);

            string er = string.Empty;
            try
            {
                btnInWater[index].Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                int regAdrs = 3+index*1;

                int val = 0;

                if (!comWaterCold.SetData(addr, regAdrs, val, out er))              //关闭吹水
                {
                    showInfo(CLanguage.Lan("设定PLC地址[") + addr.ToString("D2") + "_" + regAdrs.ToString("D2") + CLanguage.Lan("]关闭吹水失败:") + er, true);
                    return;
                }

                regAdrs = 0 + index * 1;

                val = 1;

                if (!comWaterCold.SetData(addr, regAdrs, val, out er))              //打开进水
                {
                    showInfo(CLanguage.Lan("设定PLC地址[") + addr.ToString("D2") + "_" + regAdrs.ToString("D2") + CLanguage.Lan("]打开运水失败:") + er, true);
                    return;
                }
                showInfo(CLanguage.Lan("设定PLC地址[") + addr.ToString("D2") + "_" + regAdrs.ToString("D2") + CLanguage.Lan("]打开运水成功"));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnInWater[index].Enabled = true;
            }
        }

        /// <summary>
        /// 设定吹水
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackWater_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;

            int index = Convert.ToInt16(b.Tag);

            string er = string.Empty;
            try
            {
                btnBackWater[index].Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                int regAdrs = 0 + index * 1;

                int val = 0;

                if (!comWaterCold.SetData(addr, regAdrs, val, out er))              //关闭运水
                {
                    showInfo(CLanguage.Lan("设定PLC地址[") + addr.ToString("D2") + "_" + regAdrs.ToString("D2") + CLanguage.Lan("]关闭运水失败:") + er, true);
                    return;
                }

                regAdrs = 3 + index * 1;

                val = 1;

                if (!comWaterCold.SetData(addr, regAdrs, val, out er))              //打开吹水
                {
                    showInfo(CLanguage.Lan("设定PLC地址[") + addr.ToString("D2") + "_" + regAdrs.ToString("D2") + CLanguage.Lan("]打开吹水失败:") + er, true);
                    return;
                }
                showInfo(CLanguage.Lan("设定PLC地址[") + addr.ToString("D2") + "_" + regAdrs.ToString("D2") + CLanguage.Lan("]打开吹水成功"));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnBackWater[index].Enabled = true;
            }
        }

        /// <summary>
        /// 关闭水道
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;

            int index = Convert.ToInt16(b.Tag);

            string er = string.Empty;
            try
            {
                btnClose[index].Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                int regAdrs = 0 + index * 1;

                int val = 0;

                if (!comWaterCold.SetData(addr, regAdrs, val, out er))              //关闭运水
                {
                    showInfo(CLanguage.Lan("设定PLC地址[") + addr.ToString("D2") + "_" + regAdrs.ToString("D2") + CLanguage.Lan("]关闭运水失败:") + er, true);
                    return;
                }

                regAdrs = 3 + index * 1;

                val = 0;

                if (!comWaterCold.SetData(addr, regAdrs, val, out er))              //关闭吹水
                {
                    showInfo(CLanguage.Lan("设定PLC地址[") + addr.ToString("D2") + "_" + regAdrs.ToString("D2") + CLanguage.Lan("]关闭吹水失败:") + er, true);
                    return;
                }
                showInfo(CLanguage.Lan("设定PLC地址[") + addr.ToString("D2") + "_" + regAdrs.ToString ("D2") + CLanguage.Lan("]关闭水道成功"));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnClose[index].Enabled = true;
            }
        }

        /// <summary>
        /// 读取水冷状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRead_Click(object sender, EventArgs e)
        {
            string er = string.Empty;
            try
            {
                btnRead.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);                    //设备地址
                for (int index = 0; index < 3; index++)
                {
                    int regAdrs = 0 + index * 1;
                    string rData = string.Empty;
                    int Status = 0;
                    int val = 10;

                    if (!comWaterCold.GetData(addr, regAdrs, 1, out rData, out er))              //运水状态
                    {
                        showInfo(CLanguage.Lan("读取PLC地址[") + addr.ToString("D2") + "_" + regAdrs.ToString("D2") + CLanguage.Lan("]运水状态失败:") + er, true);
                        return;
                    }
                    else
                    {
                        val = System.Convert.ToInt16(rData.Substring(rData.Length - 4, 4), 16);
                        if (val == 1)
                            Status = 1;

                    }

                    regAdrs = 3 + index * 1;
                    rData = string.Empty;
                    val = 10;

                    if (!comWaterCold.GetData(addr, regAdrs, 1, out rData, out er))              //吹水状态
                    {
                        showInfo(CLanguage.Lan("读取PLC地址[") + addr.ToString("D2") + "_" + regAdrs.ToString("D2") + CLanguage.Lan("]吹水状态失败:") + er, true);
                        return;
                    }
                    else
                    {
                        val = System.Convert.ToInt16(rData.Substring(rData.Length - 4, 4), 16);
                        if (val == 1)
                            Status = 2;
                    }
                    if (Status == 1)
                        lblStatus[index].Text = "运水";
                    else if (Status == 2)
                        lblStatus[index].Text = "吹水";
                    else
                        lblStatus[index].Text = "关闭";

                    regAdrs = 12 + index * 1;
                    rData = string.Empty;
                    val = 0;

                    if (!comWaterCold.GetData(addr, regAdrs, 1, out rData, out er))              //水流量
                    {
                        showInfo(CLanguage.Lan("读取PLC地址[") + addr.ToString("D2") + "_" + regAdrs.ToString("D2") + CLanguage.Lan("]水流量失败:") + er, true);
                        return;
                    }
                    else
                    {
                        val = System.Convert.ToInt16(rData.Substring(rData.Length - 4, 4), 16);
                        lblWaterFlow[index].Text = (val / 10).ToString("0.0");
                    }

                    regAdrs = 15 + index * 1;
                    rData = string.Empty;
                    val = 0;

                    if (!comWaterCold.GetData(addr, regAdrs, 1, out rData, out er))              //回水温度
                    {
                        showInfo(CLanguage.Lan("读取PLC地址[") + addr.ToString("D2") + "_" + regAdrs.ToString("D2") + CLanguage.Lan("]回水温度失败:") + er, true);
                        return;
                    }
                    else
                    {
                        val = System.Convert.ToInt16(rData.Substring(rData.Length - 4, 4), 16);
                        lblBackWaterTemp[index].Text = (val / 10).ToString("0.0");
                    }

                    regAdrs = 18 + index * 1;
                    rData = string.Empty;
                    val = 0;

                    if (!comWaterCold.GetData(addr, regAdrs, 1, out rData, out er))              //压力
                    {
                        showInfo(CLanguage.Lan("读取PLC地址[") + addr.ToString("D2") + "_" + regAdrs.ToString("D2") + CLanguage.Lan("]压力失败:") + er, true);
                        return;
                    }
                    else
                    {
                        val = System.Convert.ToInt16(rData.Substring(rData.Length - 4, 4), 16);
                        lblWaterPressure[index].Text = (val/10).ToString("0.0");
                    }

                    regAdrs = 21;
                    rData = string.Empty;
                    val = 0;

                    if (!comWaterCold.GetData(addr, regAdrs, 1, out rData, out er))              //出水温度
                    {
                        showInfo(CLanguage.Lan("读取PLC地址[") + addr.ToString("D2") + "_" + regAdrs.ToString("D2") + CLanguage.Lan("]出水温度失败:") + er, true);
                        return;
                    }
                    else
                    {
                        val = System.Convert.ToInt16(rData.Substring(rData.Length - 4, 4), 16);
                        lblInWaterTemp[index].Text = (val / 10).ToString("0.0");
                    }

                }
                showInfo(CLanguage.Lan("读取PLC地址[") + addr.ToString("D2")  + CLanguage.Lan("]运行信息成功"));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnRead.Enabled = true;
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
            if (comWaterCold == null)
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




        #region 方法

        #endregion




    }
}
