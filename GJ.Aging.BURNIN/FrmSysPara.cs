using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using GJ.COM;
using GJ.PLUGINS;
using GJ.APP;
using GJ.PDB;
namespace GJ.Aging.BURNIN
{
    public partial class FrmSysPara : Form,IChildMsg 
    {
        #region 插件方法
        /// <summary>
        /// 父窗口
        /// </summary>
        private Form _father = null;
        /// <summary>
        /// 父窗口唯一标识
        /// </summary>
        private string _fatherGuid = string.Empty;
        /// <summary>
        /// 加载当前窗口及软件版本日期
        /// </summary>
        /// <param name="fatherForm"></param>
        /// <param name="control"></param>
        /// <param name="guid"></param>
        public void OnShowDlg(Form fatherForm, Control control, string guid)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action<Form, Control, string>(OnShowDlg), fatherForm, control, guid);
            else
            {
                this._father = fatherForm;
                this._fatherGuid = guid;

                this.Dock = DockStyle.Fill;
                this.TopLevel = false;
                this.FormBorderStyle = FormBorderStyle.None;
                control.Controls.Add(this);
                this.Show();
            }
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
        public void OnLogIn(string user, string password, int[] mPwrLevel)
        {

        }
        /// <summary>
        /// 启动监控
        /// </summary>
        public void OnStartRun()
        {


        }
        /// <summary>
        /// 停止监控
        /// </summary>
        public void OnStopRun()
        {


        }
        /// <summary>
        /// 中英文切换
        /// </summary>
        public void OnChangeLAN()
        {
            SetUILanguage();
        }
        /// <summary>
        /// 消息响应
        /// </summary>
        /// <param name="para"></param>
        public void OnMessage(string name, int lPara, int wPara)
        {

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
                    btnDayBrowse.Text = CLanguage.Lan("报表路径");
                    btnExit.Text = CLanguage.Lan("退出(&E)");
                    btnModelPathSet.Text = CLanguage.Lan("机种路径");
                    btnOK.Text = CLanguage.Lan("确定(&O)");
                    btnReportPath.Text = CLanguage.Lan("报表路径");
                    btnWebReportPath.Text = CLanguage.Lan("报表路径");
                    chkAutoTranFail.Text = CLanguage.Lan("自动上传不良品");
                    chkCur.Text = CLanguage.Lan("判断产品电流");
                    chkLinkMes.Text = CLanguage.Lan("连接网络服务器");
                    chkNoShow.Text = CLanguage.Lan("不显示监控报警信息");
                    chkSaveReport.Text = CLanguage.Lan("保存测试报表:");
                    chkUUTAlarm.Text = CLanguage.Lan("老化过程中出现不良蜂鸣器报警");
                    groupBox1.Text = CLanguage.Lan("老化柜号和条码规则配置");
                    groupBox6.Text = CLanguage.Lan("负载温度报警，不良报警设置");
                    groupBox8.Text = CLanguage.Lan("不良及超时判断次数");
                    label10.Text = CLanguage.Lan("波特率:");
                    label14.Text = CLanguage.Lan("PLC串口:");
                    label16.Text = CLanguage.Lan("负载排风温度(℃):");
                    label18.Text = CLanguage.Lan("日产能报表路径:");
                    label19.Text = CLanguage.Lan("条码规则1:");
                    label2.Text = CLanguage.Lan("条码规则2:");
                    label20.Text = CLanguage.Lan("测试报表网络路径:");
                    label3.Text = CLanguage.Lan("通讯超时判断次数:");
                    label36.Text = CLanguage.Lan("指定机种参数路径:");
                    label37.Text = CLanguage.Lan("测试报表保存路径:");
                    label38.Text = CLanguage.Lan("存入报表间隔(秒):");
                    label39.Text = CLanguage.Lan("单点温度超温报警(℃):");
                    label4.Text = CLanguage.Lan("电压不良判断次数:");
                    label42.Text = CLanguage.Lan("电压上限判定范围(1.05):");
                    label43.Text = CLanguage.Lan("电压下限判定范围(0.95):");
                    label44.Text = CLanguage.Lan("电流上限判定范围(1.05): ");
                    label45.Text = CLanguage.Lan("判断产品在位的最低电压(V):");
                    label46.Text = CLanguage.Lan("电流下限判定范围(0.95): ");
                    label47.Text = CLanguage.Lan("实际电压上限=设定电压上限*判定范围");
                    label48.Text = CLanguage.Lan("实际电压下限=设定电压下限*判定范围");
                    label49.Text = CLanguage.Lan("实际电流上限=设定电流上限*判定范围");
                    label5.Text = CLanguage.Lan("电流不良判断次数:");
                    label50.Text = CLanguage.Lan("实际电流下限=设定电流下限*判定范围");
     
                    label59.Text = CLanguage.Lan("扫描刷新间隔(秒):");
                    label63.Text = CLanguage.Lan("上传账户:");
                    label64.Text = CLanguage.Lan("首次开机扫描(秒):");

                    label9.Text = CLanguage.Lan("波特率:");
                    lblTimerNo1.Text = CLanguage.Lan("A区:");
                    lblTimerNo2.Text = CLanguage.Lan("B区:");
                    tabPage1.Text = CLanguage.Lan("基本配置");
                    tabPage2.Text = CLanguage.Lan("测试参数");
                    tabPage3.Text = CLanguage.Lan("报警设置");
                    tabPage4.Text = CLanguage.Lan("测试数据");
                    tabPage5.Text = CLanguage.Lan("MES设置");
                    tabPage6.Text = CLanguage.Lan("通讯配置");
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 构造函数
        public FrmSysPara()
        {
            InitializeComponent();

            loadMainForm();
        }
        #endregion

        #region 面板控件
        /// <summary>
        /// 时序命名设定
        /// </summary>
        private TextBox[] txtTimerNo = null;
        /// <summary>
        /// 时序命名设定
        /// </summary>
        private TextBox[] txtBarSpec = null;

        /// <summary>
        /// 区域命名设定
        /// </summary>
        private TextBox[] txtareaNo = null;

        /// <summary>
        /// PLC串口号
        /// </summary>
        private ComboBox[] cmbPLCCom = null;
        /// <summary>
        /// PLC波特率
        /// </summary>
        private TextBox[] txtPLCBaud = null;

        /// <summary>
        /// GJ_1050_4串口号
        /// </summary>
        private ComboBox[] cmbGJ_1050_4Com = null;
        /// <summary>
        /// GJ_1050_4波特率
        /// </summary>
        private TextBox[] txtGJ_1050_4Baud = null;

        /// <summary>
        /// GJ_Mon32串口号
        /// </summary>
        private ComboBox[] cmbGJ_Mon32Com = null;
        /// <summary>
        /// GJ_Mon32波特率
        /// </summary>
        private TextBox[] txtGJ_Mon32Baud = null;


        /// <summary>
        /// 条码枪串口号
        /// </summary>
        private ComboBox[] cmb_Bar_Com = null;
        /// <summary>
        /// 条码枪波特率
        /// </summary>
        private TextBox[] txt_bar_Baud = null;



        #endregion

        #region 面板初始化
        private void loadMainForm()
        {
			txtTimerNo = new TextBox[] { txtTimerNO1, txtTimerNO2 };
			txtBarSpec = new TextBox[] { txtBarSpec1, txtBarSpec2};
            txtareaNo = new TextBox[] { txtareaNO1, txtareaNO2 };

            cmbPLCCom = new ComboBox[] { cmbPLCCom1 };
            txtPLCBaud = new TextBox[] { txtPLCBaud1 };
            cmbGJ_1050_4Com = new ComboBox[] { cmbGJ_1050_4Com1,cmbGJ_1050_4Com2};
            txtGJ_1050_4Baud = new TextBox[] { txtGJ_1050_4Baud1,txtGJ_1050_4Baud2};

            cmbGJ_Mon32Com = new ComboBox[] { GJ_Mon32Com1, GJ_Mon32Com2 };
            txtGJ_Mon32Baud = new TextBox[] { GJ_Mon32Buad1, GJ_Mon32Buad2 };

            cmb_Bar_Com = new ComboBox[] { cmb_Bar_Com1  };
            txt_bar_Baud = new TextBox[] { txt_bar_Baud1  };
    
        }

        #endregion

        #region 面板回调函数
        private void FrmSysPara_Load(object sender, EventArgs e)
        {
            SetUILanguage();
            load();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(CLanguage.Lan("确定要保存系统参数设置?"), CLanguage.Lan("系统参数设置"), MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    save();
                    
                    string er = string.Empty;

                    CReflect.SendWndMethod(_father, EMessType.OnMessage, out er, new object[] { "btnOK", (int)ElPara.保存, 0 });

                    CUserApp.OnUserArgs.OnEvented(new CUserArgs("FrmSysPara", (int)ElPara.保存, 0));
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            string er = string.Empty;

            CReflect.SendWndMethod(_father, EMessType.OnMessage, out er, new object[] { "btnExit", (int)ElPara.退出, 0 });
        }
        private void OnTextKeyPressIsNumber(object sender, KeyPressEventArgs e)
        {
            //char-8为退格键
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)'.')
                e.Handled = true;
        }


        /// <summary>
        /// 报表保存路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReportPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
                txtReportPath.Text = dlg.SelectedPath;
        }
        /// <summary>
        /// 日产能报表路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDayBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
                txtDayRecord.Text = dlg.SelectedPath;
        }

        /// <summary>
        /// 网络报表路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWebReportPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
                txtWebReportPath.Text = dlg.SelectedPath;
        }
        /// <summary>
        /// 机种参数路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModelPathSet_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
                txtModelPath.Text = dlg.SelectedPath;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载XML文件到机种参数
        /// </summary>
        private void load()
        {
            try
            {
                for (int j = 0; j < txtTimerNo.Length; j++)
                {
                    txtTimerNo[j].Text = CGlobalPara.SysPara.Para.timerNO[j];
                    txtBarSpec[j].Text = CGlobalPara.SysPara.Para.timerBarSpec[j];
                }

                for (int j = 0; j < txtareaNo.Length; j++)
                {
                    txtareaNo[j].Text = CGlobalPara.SysPara.Para.areaNO [j];                    
                }
                chkNoF.Checked = CGlobalPara.SysPara.Para.chkNoF;
                chkNoGating.Checked = CGlobalPara.SysPara.Para.chkNoGating;
                chkScanPathSn.Checked = CGlobalPara.SysPara.Para.chkScanPathSn;
                chkBarFlag.Checked = CGlobalPara.SysPara.Para.BarFlag ==0 ? false :true ;

                chkSpeeck.Checked = CGlobalPara.SysPara.Para.chkSpeeck;
        

                txtMonInterval.Text = CGlobalPara.SysPara.Report.MonInterval.ToString();
                txtFirstScan.Text = CGlobalPara.SysPara.Report.FirstScan.ToString();
                txtModelPath.Text = CGlobalPara.SysPara.Report.ModelPath;
                chkSaveReport.Checked = CGlobalPara.SysPara.Report.SaveReport;
                txtSaveReportTime.Text = CGlobalPara.SysPara.Report.SaveReportTimes.ToString();
                txtReportPath.Text = CGlobalPara.SysPara.Report.ReportPath;
                txtWebReportPath.Text = CGlobalPara.SysPara.Report.WebReportPath;
                txtDayRecord.Text = CGlobalPara.SysPara.Report.DayRecordPath;
                txtReStartTime.Text = CGlobalPara.SysPara.Report.ReStartTime.ToString();
                chkSaveExcel.Checked = CGlobalPara.SysPara.Report.SaveExcel ;
                 
                chkUUTAlarm.Checked = CGlobalPara.SysPara.Alarm.uutAlarm;

                chkNoShow.Checked = CGlobalPara.SysPara.Alarm.NoShowAlarm;

                txtLoadAlarm.Text = CGlobalPara.SysPara.Alarm.LoadAlarm.ToString();
                txtLoadOpenFan.Text = CGlobalPara.SysPara.Alarm.LoadOpenFan.ToString();
                txtLoadCloseFan.Text = CGlobalPara.SysPara.Alarm.LoadCloseFan.ToString();



                txtComFailTimes.Text = CGlobalPara.SysPara.Alarm.IICFailTimes.ToString();
                txtVlotFailTimes.Text = CGlobalPara.SysPara.Alarm.VoltFailTimes.ToString();
                txtCurFailTimes.Text = CGlobalPara.SysPara.Alarm.CurFailTimes.ToString();

                chkCur.Checked = CGlobalPara.SysPara.Reg.ChkNoJugdeCur;
                txtHaveUUTVolt.Text = CGlobalPara.SysPara.Reg.haveUUT.ToString();
                txtChkVmax.Text = CGlobalPara.SysPara.Reg.VHP.ToString();
                txtChkVmin.Text = CGlobalPara.SysPara.Reg.VLP.ToString();
                txtChkImax.Text = CGlobalPara.SysPara.Reg.IHP.ToString();
                txtChkImin.Text = CGlobalPara.SysPara.Reg.ILP.ToString();
                txtoffsetVolt.Text = CGlobalPara.SysPara.Reg.offsetVolt.ToString();

                chkLinkMes.Checked = CGlobalPara.SysPara.Mes.Connect;
             
                txtMesIP.Text = CGlobalPara.SysPara.Mes.OracleDB ;
             
                chkAutoTranFail.Checked = CGlobalPara.SysPara.Mes.AutoTranFail;
              

                for (int j = 0; j < cmbPLCCom.Length; j++)
                {
                    cmbPLCCom[j].Items.Clear();
                }

                for (int j = 0; j < cmbGJ_Mon32Com.Length; j++)
                {
                    cmbGJ_Mon32Com[j].Items.Clear();
                }

                for (int j = 0; j < cmbGJ_1050_4Com.Length; j++)
                {
                    cmbGJ_1050_4Com[j].Items.Clear();
                }


                string[] comArray = SerialPort.GetPortNames();

                for (int i = 0; i < comArray.Length; i++)
                {
                    for (int j = 0; j < cmbPLCCom.Length; j++)
                    {
                        cmbPLCCom[j].Items.Add(comArray[i]);
                    }

                    for (int j = 0; j < cmbGJ_1050_4Com.Length; j++)
                    {
                        cmbGJ_1050_4Com[j].Items.Add(comArray[i]);
                    }


                    for (int j = 0; j < cmbGJ_Mon32Com.Length; j++)
                    {
                        cmbGJ_Mon32Com[j].Items.Add(comArray[i]);
                    }
                }


                for (int j = 0; j < cmbPLCCom.Length; j++)
                {
                    cmbPLCCom[j].Text = CGlobalPara.SysPara.Dev.plcCom[j];
                    txtPLCBaud[j].Text = CGlobalPara.SysPara.Dev.plcBuad[j];
                }

                for (int j = 0; j < cmbGJ_1050_4Com.Length; j++)
                {
                    cmbGJ_1050_4Com[j].Text = CGlobalPara.SysPara.Dev.GJ_1050_4Com[j];
                    txtGJ_1050_4Baud[j].Text = CGlobalPara.SysPara.Dev.GJ_1050_4Buad[j];
                    
                }
                for (int j = 0; j < cmbGJ_Mon32Com.Length; j++)
                {
                    cmbGJ_Mon32Com[j].Text = CGlobalPara.SysPara.Dev.GJ_Mon32Com[j];
                    txtGJ_Mon32Baud[j].Text = CGlobalPara.SysPara.Dev.GJ_Mon32Buad[j];
                }


                for (int j = 0; j < cmb_Bar_Com.Length; j++)
                {

                    cmb_Bar_Com[j].Text = CGlobalPara.SysPara.Dev.Bar_Com [j];
                    txt_bar_Baud[j].Text = CGlobalPara.SysPara.Dev.Bar_Buad [j];
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存几种参数到XML文件
        /// </summary>
        private void save()
        {
            try
            {
                CGlobalPara.SysPara = new CSysPara();
                for (int j = 0; j < txtTimerNo.Length; j++)
                {
                    CGlobalPara.SysPara.Para.timerNO[j] = txtTimerNo[j].Text;
                    CGlobalPara.SysPara.Para.timerBarSpec[j] = txtBarSpec[j].Text;
                }
                for (int j = 0; j < txtareaNo.Length; j++)
                {
                    CGlobalPara.SysPara.Para.areaNO[j] = txtareaNo[j].Text;
                }
                CGlobalPara.SysPara.Para.chkNoF = chkNoF.Checked;
                CGlobalPara.SysPara.Para.chkNoGating = chkNoGating.Checked;
                CGlobalPara.SysPara.Para.chkScanPathSn = chkScanPathSn.Checked;
                CGlobalPara.SysPara.Para.BarFlag = chkBarFlag.Checked == true ? 1 : 0;
                CGlobalPara.SysPara.Para.chkSpeeck = chkSpeeck.Checked;

                CGlobalPara.SysPara.Report.MonInterval = System.Convert.ToInt16(txtMonInterval.Text);
                CGlobalPara.SysPara.Report.FirstScan = System.Convert.ToInt16(txtFirstScan.Text);
                CGlobalPara.SysPara.Report.ModelPath = txtModelPath.Text;
                CGlobalPara.SysPara.Report.SaveReport = chkSaveReport.Checked;
                CGlobalPara.SysPara.Report.SaveReportTimes = System.Convert.ToInt16(txtSaveReportTime.Text);
                CGlobalPara.SysPara.Report.ReportPath = txtReportPath.Text;
                CGlobalPara.SysPara.Report.WebReportPath = txtWebReportPath.Text;
                CGlobalPara.SysPara.Report.DayRecordPath = txtDayRecord.Text;
                CGlobalPara.SysPara.Report.ReStartTime = System.Convert.ToInt16(txtReStartTime.Text);
                CGlobalPara.SysPara.Report.SaveExcel  = chkSaveExcel.Checked;

                CGlobalPara.SysPara.Alarm.uutAlarm = chkUUTAlarm.Checked;
                CGlobalPara.SysPara.Alarm.LoadAlarm  = System.Convert.ToInt16(txtLoadAlarm.Text);
                CGlobalPara.SysPara.Alarm.LoadOpenFan = System.Convert.ToInt16(txtLoadOpenFan.Text);
                CGlobalPara.SysPara.Alarm.LoadCloseFan = System.Convert.ToInt16(txtLoadCloseFan.Text);
                

                CGlobalPara.SysPara.Alarm.NoShowAlarm = chkNoShow.Checked;
            

                CGlobalPara.SysPara.Alarm.IICFailTimes = System.Convert.ToInt16(txtComFailTimes.Text);
                CGlobalPara.SysPara.Alarm.VoltFailTimes = System.Convert.ToInt16(txtVlotFailTimes.Text);
                CGlobalPara.SysPara.Alarm.CurFailTimes = System.Convert.ToInt16(txtCurFailTimes.Text);

                CGlobalPara.SysPara.Reg.ChkNoJugdeCur = chkCur.Checked;
                CGlobalPara.SysPara.Reg.haveUUT = System.Convert.ToSingle(txtHaveUUTVolt.Text);
                CGlobalPara.SysPara.Reg.VHP = System.Convert.ToSingle(txtChkVmax.Text);
                CGlobalPara.SysPara.Reg.VLP = System.Convert.ToSingle(txtChkVmin.Text);
                CGlobalPara.SysPara.Reg.IHP = System.Convert.ToSingle(txtChkImax.Text);
                CGlobalPara.SysPara.Reg.ILP = System.Convert.ToSingle(txtChkVmax.Text);
                CGlobalPara.SysPara.Reg.offsetVolt = double.Parse(txtoffsetVolt.Text);

                CGlobalPara.SysPara.Mes.Connect = chkLinkMes.Checked;
                CGlobalPara.SysPara.Mes.OracleDB = txtMesIP.Text;
                CGlobalPara.SysPara.Mes.tranUser = txtMesIP.Text;
                CGlobalPara.SysPara.Mes.AutoTranFail = chkAutoTranFail.Checked;
        

                for (int j = 0; j < cmbPLCCom.Length; j++)
                {
                    CGlobalPara.SysPara.Dev.plcCom[j] = cmbPLCCom[j].Text;
                    CGlobalPara.SysPara.Dev.plcBuad[j] = txtPLCBaud[j].Text;
                }

                for (int j = 0; j < cmbGJ_1050_4Com.Length; j++)
                {
                    CGlobalPara.SysPara.Dev.GJ_1050_4Com[j] = cmbGJ_1050_4Com[j].Text;
                    CGlobalPara.SysPara.Dev.GJ_1050_4Buad[j] = txtGJ_1050_4Baud[j].Text;
                }
                for (int j = 0; j < cmbGJ_Mon32Com.Length; j++)
                {
                    CGlobalPara.SysPara.Dev.GJ_Mon32Com[j] = cmbGJ_Mon32Com[j].Text;
                    CGlobalPara.SysPara.Dev.GJ_Mon32Buad[j] = txtGJ_Mon32Baud[j].Text;
                }



                CSerializable<CSysPara>.WriteXml(CGlobalPara.SysFile, CGlobalPara.SysPara);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


    }
}
