using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.PLUGINS;
using GJ.DEV.FCMB;
using GJ.COM;
namespace GJ.TOOL
{
    public partial class FrmFCMB : Form,IChildMsg
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
        public FrmFCMB()
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
            for (int i = 0; i <CHILD_MAX; i++)
            {
                Label lab1 = new Label();

                lab1.Dock = DockStyle.Fill;

                lab1.Margin = new Padding(0);

                lab1.Text = (i + 1).ToString("D2"); 

                lab1.TextAlign = ContentAlignment.MiddleCenter;

                labAddrs.Add(lab1);

                Label lab2 = new Label();

                lab2.Dock = DockStyle.Fill;

                lab2.BorderStyle = BorderStyle.Fixed3D; 

                lab2.Margin = new Padding(1);

                lab2.BackColor = Color.White;

                lab2.Text = "---";

                lab2.TextAlign = ContentAlignment.MiddleCenter;

                labVers.Add(lab2);

                Label lab3 = new Label();

                lab3.Dock = DockStyle.Fill;

                lab3.Margin = new Padding(1);

                lab3.BackColor = Color.White;

                lab3.BorderStyle = BorderStyle.Fixed3D; 

                lab3.Text = "---";

                lab3.TextAlign = ContentAlignment.MiddleCenter;

                labVolts.Add(lab3);

                int row = 0;

                int col = 0;

                if (i < CHILD_MAX / 2)
                {
                    row = i + 1;

                    col = 0;
                }
                else
                {
                    row = i - (CHILD_MAX / 2) + 1;

                    col = 3;
                }

                panel3.Controls.Add(labAddrs[i], col, row);

                panel3.Controls.Add(labVers[i], col + 1, row);

                panel3.Controls.Add(labVolts[i], col+ 2, row);
            }

            labIO.Add(labX1);
            labIO.Add(labX2);
            labIO.Add(labX3);
            labIO.Add(labX4);
            labIO.Add(labX5);
            labIO.Add(labX6);
            labIO.Add(labX7);
            labIO.Add(labX8);
            labIO.Add(labX9);
            labIO.Add(labX10);
            labIO.Add(labX11);
            labIO.Add(labX12);
            labIO.Add(labX13);
            labIO.Add(labX14);
            labIO.Add(labX15);

            txtTimer.Add(txtTimer1);
            txtTimer.Add(txtTimer2);
            txtTimer.Add(txtTimer3);
            txtTimer.Add(txtTimer4);
            txtTimer.Add(txtTimer5);
            txtTimer.Add(txtTimer6);
            txtTimer.Add(txtTimer7);
            txtTimer.Add(txtTimer8);
            txtTimer.Add(txtTimer9);
            txtTimer.Add(txtTimer10);
            txtTimer.Add(txtTimer11);
            txtTimer.Add(txtTimer12);
            txtTimer.Add(txtTimer13);
            txtTimer.Add(txtTimer14);
            txtTimer.Add(txtTimer15);
            txtTimer.Add(txtTimer16); 
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
            panel3.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(panel3, true, null);
            panel4.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(panel4, true, null);
            panel5.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(panel5, true, null);
            panel6.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(panel6, true, null);
            panel7.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(panel7, true, null);
            panel8.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(panel8, true, null);
        }
        #endregion

        #region 字段
        /// <summary>
        /// 快充板
        /// </summary>
        private CFMBCom comMon = null;
        /// <summary>
        /// 小板数量
        /// </summary>
        private const int CHILD_MAX = 40;
        #endregion

        #region 面板控件
        private List<Label> labAddrs = new List<Label>();
        private List<Label> labVers = new List<Label>();
        private List<Label> labVolts = new List<Label>();
        private List<Label> labIO = new List<Label>();
        private List<TextBox> txtTimer = new List<TextBox>(); 
        #endregion

        #region 面板回调函数
        private void FrmFCMB_Load(object sender, EventArgs e)
        {
            cmbCom.Items.Clear();
            string[] com = System.IO.Ports.SerialPort.GetPortNames();
            for (int i = 0; i < com.Length; i++)
                cmbCom.Items.Add(com[i]);
            if (com.Length > 0)
                cmbCom.Text = com[0];

            cmbQCM.Items.Clear();
            cmbQCM.Items.Add(EQCM.普通模式.ToString());
            cmbQCM.Items.Add(EQCM.QC2_0.ToString());
            cmbQCM.Items.Add(EQCM.QC3_0.ToString());
            cmbQCM.Items.Add(EQCM.FCP.ToString());
            cmbQCM.Items.Add(EQCM.SCP.ToString());
            cmbQCM.Items.Add(EQCM.PD3_0.ToString());
            cmbQCM.Items.Add(EQCM.PE3_0.ToString());
            cmbQCM.Items.Add(EQCM.PE1_0.ToString());
            cmbQCM.Items.Add(EQCM.PE2_0.ToString());
            cmbQCM.SelectedIndex = 0;

            cmbTimer.Items.Clear();
            cmbTimer.Items.Add(EOnOffMode.上位机控制);  
            cmbTimer.Items.Add(EOnOffMode.正常运行);
            cmbTimer.Items.Add(EOnOffMode.暂停运行);
            cmbTimer.Items.Add(EOnOffMode.继续运行);
            cmbTimer.Items.Add(EOnOffMode.重计时运行);
            cmbTimer.Items.Add(EOnOffMode.停止运行);
            cmbTimer.SelectedIndex = 0; 
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            string er = string.Empty;

            if (cmbCom.Text == "")
            {
                labStatus.Text = "请输入串口编号";
                labStatus.ForeColor = Color.Red;
                return;
            }

            if (btnOpen.Text == "打开")
            {
                comMon = new CFMBCom(EType.FMB_V1);

                if (!comMon.Open(cmbCom.Text, out er, txtBaud.Text))
                {
                    comMon = null;
                    labStatus.Text = "打开串口失败:" + er;
                    labStatus.ForeColor = Color.Red;
                    return;
                }

                btnOpen.Text = "关闭";
                labStatus.Text = "成功打开串口";
                labStatus.ForeColor = Color.Blue;
                cmbCom.Enabled = false;
            }
            else
            {
                if (comMon != null)
                {
                    comMon.Close();
                    comMon = null;
                }
                btnOpen.Text = "打开";
                labStatus.Text = "关闭串口";
                labStatus.ForeColor = Color.Black;
                cmbCom.Enabled = true;
            }
        }
        private void btnSetAddr_Click(object sender, EventArgs e)
        {
            try
            {
                btnSetAddr.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                string er = string.Empty;

                if (comMon.SetAddr(addr, out er))
                    showInfo("成功设置当前地址[" + addr.ToString("D2") + "]");
                else
                    showInfo("设置当前地址[" + addr.ToString("D2") + "]失败:" + er, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnSetAddr.Enabled = true;
            }
        }
        private void btnChildAddr_Click(object sender, EventArgs e)
        {
            try
            {
                btnChildAddr.Enabled = false;

                if (!checkSystem())
                    return;

                int childAddr = System.Convert.ToInt16(txtChildAddr.Text);  

                string er = string.Empty;

                if (comMon.SetChildAddr(childAddr,out er))
                    showInfo("成功设置当前小板地址[" + childAddr.ToString("D2") + "]");
                else
                    showInfo("设置当前小板地址[" + childAddr.ToString("D2") + "]失败:" + er, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnChildAddr.Enabled = true;
            }
        }
        private void btnInfo_Click(object sender, EventArgs e)
        {
            try
            {
                btnInfo.Enabled=false;

                labName.Text=string.Empty;

                labSN.Text =string.Empty;

                labVer.Text =string.Empty;

                for (int i = 0; i < labVers.Count; i++)                
                    labVers[i].Text = "";

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                string er = string.Empty;

                string name=string.Empty;

                if (!comMon.ReadName(addr, out name, out er))
                {
                    showInfo("读取地址[" + addr.ToString("D2") + "]设备名称错误:" + er, true);
                    return;
                }

                labName.Text = name;

                string sn = string.Empty;

                if (!comMon.ReadSn(addr, out sn, out er))
                {
                    showInfo("读取地址[" + addr.ToString("D2") + "]设备序号错误:" + er, true);
                    return;
                }

                labSN.Text = sn;

                int ver = 0;

                if (!comMon.ReadVersion(addr, out ver, out er))
                {
                    showInfo("读取地址[" + addr.ToString("D2") + "]设备版本错误:" + er, true);
                    return;
                }

                labVer.Text = ((double)ver / 100).ToString("0.00");

                List<int> childVers = null;

                if (!comMon.ReadChildVersion(addr, out childVers, out er))
                {
                    showInfo("读取地址[" + addr.ToString("D2") + "]小板版本错误:" + er, true);
                    return;
                }

                for (int i = 0; i < labVers.Count; i++)
                {
                    if (childVers[i] == 0)
                    {
                        labVers[i].Text = "异常";
                        labVers[i].ForeColor = Color.Red;
                    }
                    else
                    {
                        labVers[i].Text = ((double)childVers[i] / 10).ToString("0.0");
                        labVers[i].ForeColor = Color.Blue;
                    }
                }

                showInfo("读取地址[" + addr.ToString("D2") + "]基本信息OK");
                   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
              
              btnInfo.Enabled=true;
            }
        }
        private void btnReadV_Click(object sender, EventArgs e)
        {
            try
            {
                btnReadV.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                string er = string.Empty;

                List<double> volts = null;

                string name = string.Empty;

                if (!comMon.ReadVolt(addr, out volts, out er, chkSync.Checked))
                {
                    showInfo("读取地址[" + addr.ToString("D2") + "]电压错误:" + er, true);
                    return;
                }

                for (int i = 0; i < volts.Count; i++)
                {
                    labVolts[i].Text = volts[i].ToString(); 
                }

                showInfo("读取地址[" + addr.ToString("D2") + "]电压OK");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnReadV.Enabled = true;
            }
        }
        private void btnWriteQCM_Click(object sender, EventArgs e)
        {
            try
            {
                btnWriteQCM.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                string er = string.Empty;

                EQCM qcm = (EQCM)cmbQCM.SelectedIndex;

                double qcv = System.Convert.ToDouble(txtQCV.Text);

                double qci = System.Convert.ToDouble(txtQCI.Text);

                if (!comMon.SetQCM(addr, qcm,qcv, qci, out er))
                {
                    showInfo("设置地址[" + addr.ToString("D2") + "]快充模式错误:" + er, true);
                    return;
                }

                showInfo("设置地址[" + addr.ToString("D2") + "]快充OK");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnWriteQCM.Enabled = true;
            }
        }
        private void btnReadQCM_Click(object sender, EventArgs e)
        {
            try
            {
                btnReadQCM.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                string er = string.Empty;

                EQCM qcm = EQCM.普通模式;

                double qcv = 0;

                double qci = 0;

                if (!comMon.ReadQCM(addr, out qcm,out qcv,out qci, out er))
                {
                    showInfo("读取地址[" + addr.ToString("D2") + "]快充模式错误:" + er, true);
                    return;
                }

                cmbQCM.SelectedIndex = (int)qcm;

                txtQCV.Text = qcv.ToString();   

                txtQCI.Text = qci.ToString();    

                showInfo("读取地址[" + addr.ToString("D2") + "]快充OK");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnReadQCM.Enabled = true;
            }
        }
        private void btnReadIO_Click(object sender, EventArgs e)
        {
            try
            {
                btnReadIO.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                string er = string.Empty;

                List<int> io = null;

                if (!comMon.ReadIO (addr, out io, out er))
                {
                    for (int i = 0; i < labIO.Count; i++)
                    {
                        labIO[i].ImageKey = "F";
                    }
                    showInfo("读取地址[" + addr.ToString("D2") + "]IO信号错误:" + er, true);
                    return;
                }

                for (int i = 0; i < labIO.Count; i++)
                {
                    if (io[i] == 1)
                        labIO[i].ImageKey = "H";
                    else
                        labIO[i].ImageKey = "L";
                }

                System.Threading.Thread.Sleep(10); 

                double acv=0;

                if (!comMon.ReadAC(addr, out acv, out er))
                {
                    showInfo("读取地址[" + addr.ToString("D2") + "]AC电压值错误:" + er, true);
                    return;
                }

                labACV.Text = acv.ToString();

                showInfo("读取地址[" + addr.ToString("D2") + "]IO信号OK");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnReadIO.Enabled = true;
            }
        }
        private void btnAlarm_Click(object sender, EventArgs e)
        {
            try
            {
                btnAlarm.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                string er = string.Empty;

                int wOnOff = 0;

                if (btnAlarm.Text == "报警ON")
                    wOnOff = 1;

                if (comMon.SetIO(addr, EFMB_wIO.错误信号灯, wOnOff, out er))
                {
                    if (btnAlarm.Text == "报警ON")
                        btnAlarm.Text = "报警OFF";
                    else
                        btnAlarm.Text = "报警ON";

                    showInfo("设置当前地址[" + addr.ToString("D2") + "]IO信号OK");
                }
                else
                {
                    showInfo("设置当前地址[" + addr.ToString("D2") + "]IO信号错误:" + er, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnAlarm.Enabled = true;
            }
        }
        private void btnRelay_Click(object sender, EventArgs e)
        {
            try
            {
                btnRelay.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                string er = string.Empty;

                int wOnOff = 0;

                if (btnRelay.Text == "继电器ON")
                    wOnOff = 1;

                if (comMon.SetIO(addr, EFMB_wIO.继电器信号, wOnOff, out er))
                {
                    if (btnRelay.Text == "继电器ON")
                        btnRelay.Text = "继电器OFF";
                    else
                        btnRelay.Text = "继电器ON";

                    showInfo("设置当前地址[" + addr.ToString("D2") + "]IO信号OK");
                }
                else
                {
                    showInfo("设置当前地址[" + addr.ToString("D2") + "]IO信号错误:" + er, true);
                }
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
        private void btnAir1_Click(object sender, EventArgs e)
        {
            try
            {
                btnAir1.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                string er = string.Empty;

                int wOnOff = 0;

                if (btnAir1.Text == "气缸1ON")
                    wOnOff = 1;

                if (comMon.SetIO(addr, EFMB_wIO.气缸控制1, wOnOff, out er))
                {
                    if (btnAir1.Text == "气缸1ON")
                        btnAir1.Text = "气缸1OFF";
                    else
                        btnAir1.Text = "气缸1ON";

                    showInfo("设置当前地址[" + addr.ToString("D2") + "]IO信号OK");
                }
                else
                {
                    showInfo("设置当前地址[" + addr.ToString("D2") + "]IO信号错误:" + er, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnAir1.Enabled = true;
            }
        }
        private void btnAir2_Click(object sender, EventArgs e)
        {
            try
            {
                btnAir2.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                string er = string.Empty;

                int wOnOff = 0;

                if (btnAir2.Text == "气缸2ON")
                    wOnOff = 1;

                if (comMon.SetIO(addr, EFMB_wIO.气缸控制2, wOnOff, out er))
                {
                    if (btnAir2.Text == "气缸2ON")
                        btnAir2.Text = "气缸2OFF";
                    else
                        btnAir2.Text = "气缸2ON";

                    showInfo("设置当前地址[" + addr.ToString("D2") + "]IO信号OK");
                }
                else
                {
                    showInfo("设置当前地址[" + addr.ToString("D2") + "]IO信号错误:" + er, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnAir2.Enabled = true;
            }
        }
        private void btnAC_Click(object sender, EventArgs e)
        {
            try
            {
                btnAC.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                string er = string.Empty;

                int wOnOff = 0;

                if (btnAC.Text == "AC ON")
                    wOnOff = 1;

                if (comMon.SetACON(addr, wOnOff, out er))
                {
                    if (btnAC.Text == "AC ON")
                        btnAC.Text = "AC OFF";
                    else
                        btnAC.Text = "AC ON";

                    showInfo("设置当前地址[" + addr.ToString("D2") + "]AC信号OK");
                }
                else
                {
                    showInfo("设置当前地址[" + addr.ToString("D2") + "]AC信号错误:" + er, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnAC.Enabled = true;
            }
        }
        private void btnReadTimer_Click(object sender, EventArgs e)
        {
            try
            {
                btnReadTimer.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                string er = string.Empty;

                EOnOffMode mode = EOnOffMode.上位机控制;

                if (!comMon.ReadOnOffMode(addr, out mode, out er))
                {
                    showInfo("读取地址[" + addr.ToString("D2") + "]时序模式错误:" + er, true);
                    return;
                }

                cmbTimer.SelectedIndex = (int)mode;

                int runMin = 0;

                if (!comMon.ReadTotalTime(addr, out runMin, out er))
                {
                    showInfo("读取地址[" + addr.ToString("D2") + "]总时间错误:" + er, true);
                    return;
                }

                txtRunMin.Text = runMin.ToString();  

                List<COnOff> OnOff=null;

                if (!comMon.ReadOnOffTime(addr, out OnOff, out er))
                {
                    showInfo("读取地址[" + addr.ToString("D2") + "]时序错误:" + er, true);
                    return;
                }

                int idNo = 0;

                for (int i = 0; i < txtTimer.Count/3; i++)
                {
                    txtTimer[i*3].Text = OnOff[idNo].OnOffTime.ToString();
                    txtTimer[i*3+1].Text = OnOff[idNo].OnTime.ToString();
                    txtTimer[i*3+2].Text = OnOff[idNo].OffTime.ToString();
                    idNo++;
                }

                showInfo("读取地址[" + addr.ToString("D2") + "]时序OK");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnReadTimer.Enabled = true;
            }
        }
        private void btnSetTimer_Click(object sender, EventArgs e)
        {
            try
            {
                btnAir2.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                string er = string.Empty;

                int runMin = System.Convert.ToInt16(txtRunMin.Text);  

                if (!comMon.SetTotalTime(addr, runMin, out er))
                {
                    showInfo("设置地址[" + addr.ToString("D2") + "]总时间错误:" + er, true);
                    return;
                }

                List<int> timer = new List<int>();

                for (int i = 0; i < txtTimer.Count; i++)
                    timer.Add(System.Convert.ToInt16(txtTimer[i].Text));

                List<COnOff> OnOff = new List<COnOff>();

                for (int i = 0; i < 10; i++)
                    OnOff.Add(new COnOff());

                int idNo=0;

                for (int i = 0; i < timer.Count/3; i++)
                {
                    OnOff[idNo].OnOffTime = timer[i * 3];
                    OnOff[idNo].OnTime = timer[i * 3 + 1];
                    OnOff[idNo].OffTime = timer[i * 3 + 2]; 
                }

                if (!comMon.SetOnOffTime(addr, OnOff, out er))
                {
                    showInfo("设置地址[" + addr.ToString("D2") + "]时序错误:" + er, true);
                    return;
                }

                EOnOffMode mode = (EOnOffMode)cmbTimer.SelectedIndex;

                if (!comMon.SetOnOffMode(addr, mode, out er))
                {
                    showInfo("设置地址[" + addr.ToString("D2") + "]时序模式错误:" + er, true);
                    return;
                }

                showInfo("设置地址[" + addr.ToString("D2") + "]时序OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnAir2.Enabled = true;
            }
        }
        private void btnWReg_Click(object sender, EventArgs e)
        {
            try
            {
                btnWReg.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt16(txtAddr.Text);

                int regAddr = System.Convert.ToInt16(txtRegAddr.Text, 16);

                int regVal = System.Convert.ToInt16(txtRegVal.Text); 

                string er = string.Empty;

                if (!comMon.Write(addr, ERegType.D, regAddr, regVal, out er))
                { 
                    showInfo("写入当前地址[" + addr.ToString("D2") + "]寄存器错误:" + er, true);
                    return;
                }

                showInfo("写入当前地址[" + addr.ToString("D2") + "]寄存器OK");  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnWReg.Enabled = true;
            }
        }
        private void btnRReg_Click(object sender, EventArgs e)
        {
            try
            {
                btnWReg.Enabled = false;

                if (!checkSystem())
                    return;

                int addr = System.Convert.ToInt32(txtAddr.Text);

                int regAddr = System.Convert.ToInt32(txtRegAddr.Text, 16);

                int regVal = 0;

                string er = string.Empty;

                if (!comMon.Read(addr, ERegType.D, regAddr, out regVal, out er))
                {
                    showInfo("读取当前地址[" + addr.ToString("D2") + "]寄存器错误:" + er, true);
                    return;
                }

                txtRegVal.Text = regVal.ToString(); 

                showInfo("读取当前地址[" + addr.ToString("D2") + "]寄存器OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnWReg.Enabled = true;
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
            if (comMon == null)
            {
                labStatus.Text = "请确定已打开串口?";
                labStatus.ForeColor = Color.Red;
                return false;
            }
            if (txtAddr.Text == "")
            {
                labStatus.Text = "请输入地址.";
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

        #region 扫描监控
        private int _rowNum = 0;
        private bool _scanStopFlag = false;
        private int _startAddr = 1;
        private int _endAddr = 36;
        private int _delayMs = 100;
        private delegate void ScanHandler();
        private void btnScan_Click(object sender, EventArgs e)
        {
            try
            {
                btnScan.Enabled = false;

                if (btnScan.Text == CLanguage.Lan("开始扫描"))
                {
                    if (comMon == null)
                    {
                        labStatus.Text = CLanguage.Lan("请确定串口是否打开?");
                        labStatus.ForeColor = Color.Red;
                        return;
                    }
                    _startAddr = System.Convert.ToInt16(txtStartAddr.Text);
                    _endAddr = System.Convert.ToInt16(txtEndAddr.Text);

                    DevView.Rows.Clear();

                    _rowNum = 0;

                    ScanHandler scanEvent = new ScanHandler(OnScan);

                    scanEvent.BeginInvoke(null, null);

                    _scanStopFlag = false;

                    btnScan.Text = CLanguage.Lan("停止扫描");
                }
                else
                {
                    _scanStopFlag = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                btnScan.Enabled = true;
            }
        }
        private void OnScan()
        {
            try
            {
                string er = string.Empty;

                for (int i = _startAddr; i <= _endAddr; i++)
                {
                    if (_scanStopFlag)
                        return;

                    System.Threading.Thread.Sleep(_delayMs);

                    bool result = true;

                    string rVer = string.Empty;

                    string rSn = string.Empty;

                    int ver = 0;

                    if (!comMon.ReadVersion(i, out ver, out er))
                    {
                        result = false;
                    }
                    else
                    {
                        rVer = ((double)ver / 100).ToString("0.00");

                        System.Threading.Thread.Sleep(50);

                        if (!comMon.ReadName(i, out rSn, out er))
                            result = false;
                    }

                    ShowID(i, rVer, rSn, result);

                    _rowNum++;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ShowEnd();
            }
        }
        private void ShowEnd()
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(ShowEnd));
            else
            {
                btnScan.Text = CLanguage.Lan("开始扫描");
            }
        }
        private void ShowID(int Addr, string rVer, string rSn, bool result)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action<int, string, string, bool>(ShowID), Addr, rVer, rSn, result);
            else
            {
                if (result)
                {
                    DevView.Rows.Add(Addr, "PASS", rVer, rSn);
                    DevView.Rows[_rowNum].Cells[1].Style.BackColor = Color.LimeGreen;
                }
                else
                {
                    DevView.Rows.Add(Addr, "FAIL", rVer, rSn);
                    DevView.Rows[_rowNum].Cells[1].Style.BackColor = Color.Red;
                }
                DevView.CurrentCell = DevView.Rows[DevView.Rows.Count - 1].Cells[0];
            }
        }
        #endregion

    }
}
