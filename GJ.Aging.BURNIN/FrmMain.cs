using GJ.Aging.BURNIN.Udc;
using GJ.APP;
using GJ.COM;
using GJ.DEV.LED;
using GJ.DEV.Meter;
using GJ.DEV.PLC;
using GJ.DEV.Mon;
using GJ.MES;
using GJ.PDB;
using GJ.PLUGINS;
using GJ.UI;
using GJ.USER.APP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using GJ.MES.Sajet;

namespace GJ.Aging.BURNIN
{
    public partial class FrmMain : Form, IChildMsg
    {
        #region 当前软件版本及日期
        private const string PROGRAM_VERSION = "V1.0.0";
        private const string PROGRAM_DATE = "2024/05/30";
        #endregion

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

                string er = string.Empty;

                CReflect.SendWndMethod(_father, EMessType.OnShowVersion, out er,
                                                 new object[] { PROGRAM_VERSION, PROGRAM_DATE });
            }
        }
        /// <summary>
        /// 关闭当前窗口 
        /// </summary>
        public void OnCloseDlg()
        {
            if (MessageBox.Show(CLanguage.Lan("确定要退出系统?"), CLanguage.Lan("退出系统"),
                     MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (CGlobalPara.SysPara.Mes.Connect)
                {
                    if (!OpenMes(out string  er, false))
                    {
                        runLog.Log(er.ToString(), udcRunLog.ELog.Err);
                    }
                }
                System.Environment.Exit(0);
            }
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user"></param>
        /// <param name="mPwrLevel"></param>
        public void OnLogIn(string user, string password, int[] mPwrLevel)
        {
            CGlobalPara.logName = user;
            CGlobalPara.logPassword = password;

            for (int i = 0; i < mPwrLevel.Length; i++)
            {
                if (CGlobalPara.logLevel.Length > i)
                    CGlobalPara.logLevel[i] = mPwrLevel[i];
            }
        }
        /// <summary>
        /// 启动监控
        /// </summary>
        public void OnStartRun()
        {
            if (!CGlobalPara.DownLoad)
                return;

            if (_runModel == null)
            {
                MessageBox.Show(CLanguage.Lan("请选择要测试机种名称,再启动监控."), CLanguage.Lan("启动监控"),
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (CGlobalPara.C_RUNNING)
                return;
     
            if (!OnRun())
                return;

            string er = string.Empty;

            CGlobalPara.C_RUNNING = true;

            CGlobalPara.C_SCAN_START = false;

            CReflect.SendWndMethod(_father, EMessType.OnShowStatus, out er, EIndicator.Auto);

        }
        /// <summary>
        /// 停止监控
        /// </summary>
        public void OnStopRun()
        {
            OnStop();

            CGlobalPara.C_RUNNING = false;

            string er = string.Empty;

            CReflect.SendWndMethod(_father, EMessType.OnShowStatus, out er, EIndicator.Idel);

        }
        /// <summary>
        /// 语言切换
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
            if (name == "F8")
            {
                if (!_maxSize)
                    skinSplitContainer1.Panel2Collapsed = true;
                else
                    skinSplitContainer1.Panel2Collapsed = false;

                _maxSize = !_maxSize;
            }
        }
        #endregion

        #region 语言设置
        /// <summary>
        /// 设置中英文界面
        /// </summary>
        private void SetUILanguage()
        {

            CLanguage.SetLanguage(runLog);

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

        #region 全局消息
        /// <summary>
        /// 全局消息触发
        /// object[0]:表示功能名称
        /// object[1]:表示功能状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnUserArgs(object sender, CUserArgs e)
        {
            try
            {
                if (e.lPara == (int)ElPara.保存)
                {
                    refreshUISetting();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 字段
        /// <summary>
        /// 窗体最大化
        /// </summary>
        private bool _maxSize = false;

        private bool _Start = false;
        #endregion

        #region 构造函数
        public FrmMain()
        {
            InitializeComponent();

            //启动初始化线程
            InitUIWorker.WorkerReportsProgress = true;

            InitUIWorker.WorkerSupportsCancellation = true;

            InitUIWorker.DoWork += new DoWorkEventHandler(InitUIWorker_DoWork);

            InitUIWorker.ProgressChanged += new ProgressChangedEventHandler(InitUIWorker_ProgressChanged);

            InitUIWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(InitUIWorker_RunWorkerCompleted);

            InitUIWorker.RunWorkerAsync();

        }
        #endregion

        #region 老化库体信息

        /// <summary>
        /// 扫描时间计数
        /// </summary>
        private int scanCount = 0;

        /// <summary>
        /// 扫描时间监控
        /// </summary>
        private Stopwatch scanWather = new Stopwatch();
        ///// <summary>
        ///// 采集时间计数
        ///// </summary>
        //private int monCount = 0;
        /// <summary>
        /// 采集时间监控
        /// </summary>
        private Stopwatch monWatcher = new Stopwatch();

        /// <summary>
        /// UI刷新时间
        /// </summary>
        private Stopwatch _uiWatcher = new Stopwatch();

        /// <summary>
        /// 老化库体信息
        /// </summary>
        private List<CCHmrStatus> _chmr = new List<CCHmrStatus>();

        /// <summary>
        /// 默认机种路径
        /// </summary>
        private string[] _defaultModelPath = new string[CGlobalPara.C_Timer_MAX];
        /// <summary>
        /// 机种参数
        /// </summary>
        private List<CModelPara> _runModel = new List<CModelPara>();
        /// <summary>
        /// 产品参数
        /// </summary>
        private List<CUUT> _runUUT = new List<CUUT>();

        /// <summary>
        /// Oracle 服务器
        /// </summary>
        private Oracle MesOracle = new Oracle();
        #endregion

        #region 初始化UI线程
        /// <summary>
        /// 初始化线程
        /// </summary>
        BackgroundWorker InitUIWorker = new BackgroundWorker();
        /// <summary>
        /// 初始化线程运行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InitUIWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                InitUIWorker.ReportProgress(1, CLanguage.Lan("加载应用程序全局配置."));

                loadAppSetting();

                InitUIWorker.ReportProgress(2, CLanguage.Lan("加载系统配置INI文件."));

                loadIniFile();

                InitUIWorker.ReportProgress(3, CLanguage.Lan("加载系统配置XML文件."));

                loadSysFile();

                InitUIWorker.ReportProgress(4, CLanguage.Lan("加载系统运行数据库配置."));

                loadRunPara();

                InitUIWorker.ReportProgress(5, CLanguage.Lan("加载界面UI基本控件."));

                loadNewControl();

                InitUIWorker.ReportProgress(6, CLanguage.Lan("显示主界面UI控件."));

                InitialControl();

                SetDoubleBuffered();

                InitUIWorker.ReportProgress(7, CLanguage.Lan("初始化PLC参数."));
                InitialPLC();

                InitUIWorker.ReportProgress(8, CLanguage.Lan("加载机种参数配置XML文件."));
                for (int i = 0; i < CGlobalPara.C_Timer_MAX; i++)
                {
                    _runModel.Add(new CModelPara());
                    loadModelPara(i, _defaultModelPath[i]);

                    showUUT(i);
                }
                InitUIWorker.ReportProgress(9, CLanguage.Lan("系统参数和主界面UI初始化完成."));

       
                CGlobalPara.DownLoad = true;

            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }

        }
        /// <summary>
        /// 初始化线程进展
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InitUIWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            runLog.Log(e.UserState.ToString(), udcRunLog.ELog.Action);
        }
        /// <summary>
        /// 初始化线程完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InitUIWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!CGlobalPara.DownLoad)
                runLog.Log(CLanguage.Lan("系统初始化失败,请检查."), udcRunLog.ELog.NG);

            //refreshUISetting();

            //labInNum.Text = _chmr.fixYield.InBIFixNo.ToString();
            //labOutNum.Text = _chmr.fixYield.OutBIFixNo.ToString();
            //updateUUTBIUI(_chmr.fixYield.BITTNum, _chmr.fixYield.BIPASSNum);
        }

        #endregion

        #region  初始化方法

        /// <summary>
        /// 加载应用程序配置
        /// </summary>
        private void loadAppSetting()
        {
            //定义全局消息
            CUserApp.OnUserArgs.OnEvent += new COnEvent<CUserArgs>.OnEventHandler(OnUserArgs);
        }

        /// <summary>
        /// 加载Ini文件 
        /// </summary>
        private void loadIniFile()
        {
            try
            {
                for (int i = 0; i < CGlobalPara.C_Timer_MAX; i++)
                {
                    _defaultModelPath[i] = CIniFile.ReadFromIni("Parameter", "ModelPath" + i, CGlobalPara.IniFile);
                    _chmr.Add(new CCHmrStatus());
                    _chmr[i].curOutModel = CIniFile.ReadFromIni("Parameter", "curOutModel" + i, CGlobalPara.IniFile);
                    _chmr[i].status.doRun = (ERun)System.Convert.ToInt32(CIniFile.ReadFromIni("Parameter", "doRun" + i, CGlobalPara.IniFile, "0"));
                    _chmr[i].areaYield.BITTNum = System.Convert.ToInt32(CIniFile.ReadFromIni("Parameter", "BITTNum" + i, CGlobalPara.IniFile, "0"));
                    _chmr[i].areaYield.BIPASSNum = System.Convert.ToInt32(CIniFile.ReadFromIni("Parameter", "BIPASSNum" + i, CGlobalPara.IniFile, "0"));
                    _chmr[i].dayYield.dayNow = CIniFile.ReadFromIni("DailyYield", "dayNow" + i, CGlobalPara.IniFile);
                    _chmr[i].dayYield.ttNum = System.Convert.ToInt32(CIniFile.ReadFromIni("DailyYield", "ttNum" + i, CGlobalPara.IniFile, "0"));
                    _chmr[i].dayYield.failNum = System.Convert.ToInt32(CIniFile.ReadFromIni("DailyYield", "failNum" + i, CGlobalPara.IniFile, "0"));
                    _chmr[i].dayYield.yieldTTNum = System.Convert.ToInt32(CIniFile.ReadFromIni("DailyYield", "yieldTTNum" + i, CGlobalPara.IniFile, "0"));
                    _chmr[i].dayYield.yieldFailNum = System.Convert.ToInt32(CIniFile.ReadFromIni("DailyYield", "yieldFailNum" + i, CGlobalPara.IniFile, "0"));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 加载系统配置
        /// </summary>
        private void loadSysFile()
        {
            try
            {
                CGlobalPara.loadSysXml();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 加载测试参数
        /// </summary>
        private void loadRunPara()
        {
            try
            {
                CMES.WebAddr = CAgingApp.DllFile;

                for (int i = 0; i < CGlobalPara.C_Timer_MAX; i++)
                    _runUUT.Add(new CUUT());

                string er = string.Empty;

                string sqlCmd = string.Empty;

                DataSet ds = null;

                CDBCOM db = new CDBCOM(EDBType.Access, "", CGlobalPara.SysDB);

                //测试参数

                sqlCmd = "select * from RUN_PARA order by TimerNO";

                if (!db.QuerySQL(sqlCmd, out ds, out er))
                    return;

                if (ds.Tables[0].Rows.Count != CGlobalPara.C_Timer_MAX)
                {
                    runLog.Log(CLanguage.Lan("数据库表单【RUN_PARA】数据异常"), udcRunLog.ELog.NG);
                    return;
                }

                for (int i = 0; i < CGlobalPara.C_Timer_MAX; i++)
                {
                    _runUUT[i].Para.DoRun = (AgingRunType)System.Convert.ToInt32(ds.Tables[0].Rows[i]["doRun"].ToString());
                    _runUUT[i].Para.DoData = AgingDataType.空闲;

                    _runUUT[i].Para.TimerNo = System.Convert.ToInt32(ds.Tables[0].Rows[i]["TimerNo"].ToString());
                    _runUUT[i].Para.TimerName = CGlobalPara.SysPara.Para.timerNO[i];
                    _runUUT[i].Para.ModelName = ds.Tables[0].Rows[i]["ModelName"].ToString();

                    _runUUT[i].Para.MesFlag = System.Convert.ToInt32(ds.Tables[0].Rows[i]["MesFlag"].ToString());
                    _runUUT[i].Para.BarFlag = System.Convert.ToInt32(ds.Tables[0].Rows[i]["BarFlag"].ToString());
                    _runUUT[i].Para.UserName = ds.Tables[0].Rows[i]["UserName"].ToString();
                    _runUUT[i].Para.FixBar = System.Convert.ToInt32(ds.Tables[0].Rows[i]["FixBar"].ToString());
                    _runUUT[i].Para.barSpec = ds.Tables[0].Rows[i]["barSpec"].ToString();
                    _runUUT[i].Para.barLength = System.Convert.ToInt32(ds.Tables[0].Rows[i]["barLength"].ToString());
                    _runUUT[i].Para.MO_NO = ds.Tables[0].Rows[i]["MO_NO"].ToString();
                    _runUUT[i].Para.BurnTime = System.Convert.ToInt32(ds.Tables[0].Rows[i]["BurnTime"].ToString());

                    _runUUT[i].Para.StartTime = ds.Tables[0].Rows[i]["StartTime"].ToString();
                    _runUUT[i].Para.EndTime = ds.Tables[0].Rows[i]["EndTime"].ToString();
                    _runUUT[i].Para.SavePath = ds.Tables[0].Rows[i]["SavePath"].ToString();
                    _runUUT[i].Para.SaveWebPath = ds.Tables[0].Rows[i]["SaveWebPath"].ToString();
                    _runUUT[i].Para.SaveBakPath = ds.Tables[0].Rows[i]["SaveBakPath"].ToString();
                    _runUUT[i].Para.SaveDataTime = ds.Tables[0].Rows[i]["SaveDataTime"].ToString();
                    _runUUT[i].Para.SaveDataIndex = System.Convert.ToInt32(ds.Tables[0].Rows[i]["SaveDataIndex"].ToString());
                    _runUUT[i].Para.FailNum = System.Convert.ToInt32(ds.Tables[0].Rows[i]["FailNum"].ToString());
                    _runUUT[i].Para.RunTime = System.Convert.ToInt32(ds.Tables[0].Rows[i]["RunTime"].ToString());
                    _runUUT[i].Para.OutPutChan = System.Convert.ToInt32(ds.Tables[0].Rows[i]["OutPutChan"].ToString());
                    _runUUT[i].Para.OutPutNum = System.Convert.ToInt32(ds.Tables[0].Rows[i]["OutPutNum"].ToString());
                    _runUUT[i].Para.OnOffNum = System.Convert.ToInt32(ds.Tables[0].Rows[i]["OnOffNum"].ToString());
                    _runUUT[i].Para.PassNum = System.Convert.ToInt32(ds.Tables[0].Rows[i]["PassNum"].ToString());
                    _runUUT[i].Para.TTNum = System.Convert.ToInt32(ds.Tables[0].Rows[i]["TTNum"].ToString());

                    _runUUT[i].Para.iniSpec = 1;
                    _runUUT[i].OnOff = new CUUT_ONOFF();
                    _runUUT[i].OnOff.AddItem(_runUUT[i].Para.OutPutNum, _runUUT[i].Para.OutPutChan, _runUUT[i].Para.OnOffNum);
                    _runUUT[i].OnOff.TimeSpec.BITime = _runUUT[i].Para.BurnTime;
                    _runUUT[i].OnOff.TimeSpec.OutChanNum = _runUUT[i].Para.OutPutChan;
                    _runUUT[i].OnOff.TimeSpec.OutPutNum = _runUUT[i].Para.OutPutNum;
                    _runUUT[i].OnOff.TimeSpec.OnOffNum = _runUUT[i].Para.OnOffNum;

                    _runUUT[i].OnOff.TimeRun.CurStepNo = System.Convert.ToInt32(ds.Tables[0].Rows[i]["CurStepNo"].ToString());
                    _runUUT[i].OnOff.TimeRun.CurRunVolt = System.Convert.ToInt32(ds.Tables[0].Rows[i]["CurRunVolt"].ToString());
                    _runUUT[i].OnOff.TimeRun.CurRunOutPut = System.Convert.ToInt32(ds.Tables[0].Rows[i]["CurRunOutPut"].ToString());
                    _runUUT[i].OnOff.TimeRun.CurDCONOFF = System.Convert.ToInt32(ds.Tables[0].Rows[i]["CurDCONOFF"].ToString());
                    _runUUT[i].Para.RunInVolt = _runUUT[i].OnOff.TimeRun.CurRunVolt;
                    string StrOutPut = ds.Tables[0].Rows[i]["OutPutList"].ToString();

                    if (StrOutPut != string.Empty)
                    {
                        string[] Str1 = StrOutPut.Split(';');

                        if (Str1.Length >= _runUUT[i].Para.OutPutNum)
                        {
                            for (int z = 0; z < _runUUT[i].Para.OutPutNum; z++)
                            {
                                string[] Str2 = Str1[z].Split('*');

                                _runUUT[i].OnOff.OutPut[z].iicSpec = Str2[0];

                                for (int k = 0; k < _runUUT[i].Para.OutPutChan; k++)
                                {
                                    string[] Str3 = Str2[k + 1].Split(',');
                                    _runUUT[i].OnOff.OutPut[z].Chan[k].Vuse = System.Convert.ToInt16(Str3[0]);
                                    _runUUT[i].OnOff.OutPut[z].Chan[k].Vname = Str3[1];
                                    _runUUT[i].OnOff.OutPut[z].Chan[k].Vmin = System.Convert.ToDouble(Str3[2]);
                                    _runUUT[i].OnOff.OutPut[z].Chan[k].Vmax = System.Convert.ToDouble(Str3[3]);
                                    _runUUT[i].OnOff.OutPut[z].Chan[k].Imode = System.Convert.ToInt16(Str3[4]);
                                    _runUUT[i].OnOff.OutPut[z].Chan[k].ISet = System.Convert.ToDouble(Str3[5]);
                                    _runUUT[i].OnOff.OutPut[z].Chan[k].Imin = System.Convert.ToDouble(Str3[6]);
                                    _runUUT[i].OnOff.OutPut[z].Chan[k].Imax = System.Convert.ToDouble(Str3[7]);
                                    _runUUT[i].OnOff.OutPut[z].Chan[k].Von = System.Convert.ToDouble(Str3[8]);
                                    _runUUT[i].OnOff.OutPut[z].Chan[k].AddMode = System.Convert.ToDouble(Str3[9]);
                                }
                            }
                        }
                    }

                    string StrOnOff = ds.Tables[0].Rows[i]["OnOffList"].ToString();

                    if (StrOnOff != string.Empty)
                    {
                        string[] Str1 = StrOnOff.Split(';');

                        if (Str1.Length >= _runUUT[i].Para.OnOffNum)
                        {
                            for (int z = 0; z < _runUUT[i].Para.OnOffNum; z++)
                            {
                                string[] Str2 = Str1[z].Split(',');

                                _runUUT[i].OnOff.OnOff[z].inPutVolt = System.Convert.ToInt32(Str2[0]);
                                _runUUT[i].OnOff.OnOff[z].OnOffTime = System.Convert.ToInt32(Str2[1]);
                                _runUUT[i].OnOff.OnOff[z].OnTime = System.Convert.ToInt32(Str2[2]);
                                _runUUT[i].OnOff.OnOff[z].OffTime = System.Convert.ToInt32(Str2[3]);
                                _runUUT[i].OnOff.OnOff[z].outPutCur = System.Convert.ToInt32(Str2[4]);
                                _runUUT[i].OnOff.OnOff[z].dcONOFF = System.Convert.ToInt32(Str2[5]);
                            }
                        }
                    }
                }

                //产品运行信息
                sqlCmd = "select * from RUN_DATA order by LEDNO";

                if (!db.QuerySQL(sqlCmd, out ds, out er))
                    return;

                if (ds.Tables[0].Rows.Count != CGlobalPara.C_Timer_MAX * CAgingApp.TimerChanMax)
                {
                    runLog.Log(CLanguage.Lan("数据库表单【RUN_DATA】数据异常"), udcRunLog.ELog.NG);
                    return;
                }

                for (int i = 0; i < CGlobalPara.C_Timer_MAX; i++)
                {
                    for (int j = 0; j < CAgingApp.TimerChanMax; j++)
                    {
                        int uutNo = i * CAgingApp.TimerChanMax + j;

                        _runUUT[i].Led[j].uutNo = System.Convert.ToInt16(ds.Tables[0].Rows[uutNo]["uutNo"].ToString());
                        _runUUT[i].Led[j].timerNo = System.Convert.ToInt16(ds.Tables[0].Rows[uutNo]["timerNo"].ToString());
                        _runUUT[i].Led[j].iRoomNo = System.Convert.ToInt16(ds.Tables[0].Rows[uutNo]["iRoomNo"].ToString());
                        _runUUT[i].Led[j].iLayer = System.Convert.ToInt16(ds.Tables[0].Rows[uutNo]["iLayer"].ToString());
                        _runUUT[i].Led[j].iUUT = System.Convert.ToInt16(ds.Tables[0].Rows[uutNo]["iUUT"].ToString());
                        _runUUT[i].Led[j].iCH = System.Convert.ToInt16(ds.Tables[0].Rows[uutNo]["iCH"].ToString());
                        _runUUT[i].Led[j].elCom = System.Convert.ToInt16(ds.Tables[0].Rows[uutNo]["elCom"].ToString());
                        _runUUT[i].Led[j].elAdrs = System.Convert.ToInt16(ds.Tables[0].Rows[uutNo]["elAdrs"].ToString());
                        _runUUT[i].Led[j].elCH = System.Convert.ToInt16(ds.Tables[0].Rows[uutNo]["elCH"].ToString());
                        _runUUT[i].Led[j].monCom = System.Convert.ToInt16(ds.Tables[0].Rows[uutNo]["monCom"].ToString());
                        _runUUT[i].Led[j].monAdrs = System.Convert.ToInt16(ds.Tables[0].Rows[uutNo]["monAdrs"].ToString());
                        _runUUT[i].Led[j].monCH = System.Convert.ToInt16(ds.Tables[0].Rows[uutNo]["monCH"].ToString());

                        _runUUT[i].Led[j].canScanLocal = System.Convert.ToInt16(ds.Tables[0].Rows[uutNo]["canScanLocal"].ToString());
                        _runUUT[i].Led[j].barType = System.Convert.ToInt16(ds.Tables[0].Rows[uutNo]["barType"].ToString());
                        _runUUT[i].Led[j].barNo = System.Convert.ToInt16(ds.Tables[0].Rows[uutNo]["barNo"].ToString());
                        _runUUT[i].Led[j].localPath = ds.Tables[0].Rows[uutNo]["localPath"].ToString();
                        _runUUT[i].Led[j].localBar = ds.Tables[0].Rows[uutNo]["localBar"].ToString();
                        _runUUT[i].Led[j].fixBar = ds.Tables[0].Rows[uutNo]["fixBar"].ToString();
                        _runUUT[i].Led[j].serialNo = ds.Tables[0].Rows[uutNo]["SerialNo"].ToString();

                        _runUUT[i].Led[j].vUse = System.Convert.ToInt16(ds.Tables[0].Rows[uutNo]["vUse"].ToString());
                        _runUUT[i].Led[j].vName = ds.Tables[0].Rows[uutNo]["VName"].ToString();
                        _runUUT[i].Led[j].vMin = System.Convert.ToDouble(ds.Tables[0].Rows[uutNo]["Vmin"].ToString());
                        _runUUT[i].Led[j].vMax = System.Convert.ToDouble(ds.Tables[0].Rows[uutNo]["Vmax"].ToString());
                        _runUUT[i].Led[j].IMode = System.Convert.ToInt16(ds.Tables[0].Rows[uutNo]["IMode"].ToString());
                        _runUUT[i].Led[j].ISet = System.Convert.ToDouble(ds.Tables[0].Rows[uutNo]["ISET"].ToString());
                        _runUUT[i].Led[j].Imin = System.Convert.ToDouble(ds.Tables[0].Rows[uutNo]["IMin"].ToString());
                        _runUUT[i].Led[j].Imax = System.Convert.ToDouble(ds.Tables[0].Rows[uutNo]["IMax"].ToString());
                        _runUUT[i].Led[j].Von = System.Convert.ToDouble(ds.Tables[0].Rows[uutNo]["Von"].ToString());
                        _runUUT[i].Led[j].AddMode = System.Convert.ToDouble(ds.Tables[0].Rows[uutNo]["AddMode"].ToString());

                        _runUUT[i].Led[j].unitV = System.Convert.ToDouble(ds.Tables[0].Rows[uutNo]["UnitV"].ToString());
                        _runUUT[i].Led[j].unitA = System.Convert.ToDouble(ds.Tables[0].Rows[uutNo]["UnitA"].ToString());
                        _runUUT[i].Led[j].result = System.Convert.ToInt32(ds.Tables[0].Rows[uutNo]["result"].ToString());
                        _runUUT[i].Led[j].failEnd = System.Convert.ToInt32(ds.Tables[0].Rows[uutNo]["FailEnd"].ToString());
                        _runUUT[i].Led[j].failTime = ds.Tables[0].Rows[uutNo]["FailTime"].ToString();
                        _runUUT[i].Led[j].failInfo = ds.Tables[0].Rows[uutNo]["failInfo"].ToString();
                        _runUUT[i].Led[j].vBack = System.Convert.ToDouble(ds.Tables[0].Rows[uutNo]["UnitV"].ToString());
                        _runUUT[i].Led[j].iBack = System.Convert.ToDouble(ds.Tables[0].Rows[uutNo]["UnitA"].ToString());

                        _runUUT[i].Led[j].iicData = ds.Tables[0].Rows[uutNo]["iicData"].ToString();
                        _runUUT[i].Led[j].iicSpec = ds.Tables[0].Rows[uutNo]["iicSpec"].ToString();

                        _runUUT[i].Led[j].reportPath = ds.Tables[0].Rows[uutNo]["reportPath"].ToString();
                        _runUUT[i].Led[j].tranResult = System.Convert.ToInt32(ds.Tables[0].Rows[uutNo]["tranResult"].ToString());

                        _runUUT[i].Led[j].modelName = ds.Tables[0].Rows[uutNo]["modelName"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
        }

        /// <summary>
        /// 加载动态控件
        /// </summary>
        private void loadNewControl()
        {
            try
            {
                //初始化区域页面              
                for (int i = 0; i < CGlobalPara.C_Area_MAX; i++)
                {
                    pageArea.Add(new TabPage());
                    pageArea[i].Name = "Page" + i.ToString();
                    pageArea[i].Text = CGlobalPara.SysPara.Para.areaNO[i];

                    pageArea[i].Font = new Font("微软雅黑", 15);
                    pageArea[i].BackColor = System.Drawing.SystemColors.Control;
                    pageArea[i].Margin = new Padding(0);
                    pageArea[i].Padding = new Padding(0);
                }

                //初始化区域页面(单个区域页面对应的时序数）
                for (int i = 0; i < CGlobalPara.C_Area_MAX; i++)
                {
                    pnlAreaTimer.Add(new TableLayoutPanel());
                    pnlAreaTimer[i].Dock = DockStyle.Fill;
                    pnlAreaTimer[i].CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                    pnlAreaTimer[i].Margin = new Padding(0);
                    pnlAreaTimer[i].Padding = new Padding(0);
                    pnlAreaTimer[i].GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(pnlAreaTimer[i], true, null);
                    pnlAreaTimer[i].RowCount = 1;
                    pnlAreaTimer[i].RowStyles.Add(new RowStyle(SizeType.Percent, 100));

                    pnlAreaTimer[i].ColumnCount = CGlobalPara.C_Area_Timer;
                    for (int j = 0; j < CGlobalPara.C_Area_Timer; j++)
                        pnlAreaTimer[i].ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                }

                //初始化单个时序（产品指示框+状态框+时序框）
                for (int i = 0; i < CGlobalPara.C_Timer_MAX; i++)
                {
                    pnlTimer.Add(new TableLayoutPanel());
                    pnlTimer[i].Dock = DockStyle.Fill;
                    pnlTimer[i].CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
                    pnlTimer[i].Margin = new Padding(0);
                    pnlTimer[i].Padding = new Padding(0);
                    pnlTimer[i].GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(pnlTimer[i], true, null);
                    pnlTimer[i].RowCount = 2;
                    pnlTimer[i].RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                    pnlTimer[i].RowStyles.Add(new RowStyle(SizeType.Absolute, 180));
                    pnlTimer[i].ColumnCount = 1;
                    //pnlTimer[i].ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300));
                    pnlTimer[i].ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                }


                //初始化单个时序（产品指示框+状态框）
                for (int i = 0; i < CGlobalPara.C_Timer_MAX; i++)
                {
                    pnlUUT.Add(new TableLayoutPanel());
                    pnlUUT[i].Dock = DockStyle.Fill;
                    pnlUUT[i].CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                    pnlUUT[i].Margin = new Padding(0);
                    pnlUUT[i].Padding = new Padding(0);
                    pnlUUT[i].GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(pnlUUT[i], true, null);
                    pnlUUT[i].RowCount = 2;
                    pnlUUT[i].RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
                    pnlUUT[i].RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                    pnlUUT[i].ColumnCount = 2;
                    pnlUUT[i].ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));
                    pnlUUT[i].ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));


                    TableLayoutPanel pnlAreaName = new TableLayoutPanel();
                    pnlAreaName.Dock = DockStyle.Fill;
                    pnlAreaName.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                    pnlAreaName.Margin = new Padding(0);
                    pnlAreaName.Padding = new Padding(0);
                    pnlAreaName.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(pnlUUT[i], true, null);
                    pnlAreaName.RowCount = 1;
                    pnlAreaName.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                    pnlAreaName.ColumnCount = 2;
                   
                    pnlAreaName.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
                    pnlAreaName.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));

                    string[] AreaName = new string[] { i == 0 ? "A车正面" : "B车正面", i == 0 ? "A车反面" : "B车反面" };
                    for (int j = 0; j < 2; j++)
                    {
                        Label labRow = new Label();
                        labRow.Name = "labAreaName" + (j).ToString();
                        labRow.Text = AreaName[j];
                        labRow.Dock = DockStyle.Fill;
                        labRow.TextAlign = ContentAlignment.MiddleCenter;
                        labRow.ForeColor = System.Drawing.Color.Black; 
                        labRow.Font = new Font("微软雅黑", 20);
         
                        labRow.Margin = new Padding(1);
                        pnlAreaName.Controls.Add(labRow, j, 0);
                    }
                    pnlUUT[i].Controls.Add(pnlAreaName, 1, 0);
                }

               
                //初始化标题栏,按时序分配标题框
                for (int i = 0; i < CGlobalPara.C_Timer_MAX; i++)
                {
                    pnlTilte.Add(new TableLayoutPanel());
                    pnlTilte[i].Dock = DockStyle.Fill;
                    pnlTilte[i].CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                    pnlTilte[i].Margin = new Padding(0);
                    pnlTilte[i].Padding = new Padding(0);
                    pnlTilte[i].GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(pnlTilte[i], true, null);
                    pnlTilte[i].RowCount = CGlobalPara.C_Timer_Lay;
                    for (int j = 0; j < CGlobalPara.C_Timer_Lay; j++)
                        pnlTilte[i].RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                    pnlTilte[i].ColumnCount = 1;
                    pnlTilte[i].ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

                }

                //显示层号
                for (int i = 0; i < CGlobalPara.C_Timer_MAX; i++)
                {
                    for (int j = 0; j < CGlobalPara.C_Timer_Lay; j++)
                    {
                        Label labRow = new Label();
                        labRow.Name = "labRowTilte" + (_runUUT[i].Led[j * CGlobalPara.C_Layer_UUT].iLayer).ToString();
                        //labRow.Text = "L" + (_runUUT[i].Led[j * CGlobalPara.C_Layer_UUT].iLayer).ToString();
                        labRow.Text = "L" + (j+1).ToString();
                        labRow.Dock = DockStyle.Fill;
                        labRow.TextAlign = ContentAlignment.MiddleCenter;
                        labRow.ForeColor = System.Drawing.Color.Black;
                        if (labRow.Text.Length == 2)
                            labRow.Font = new Font("微软雅黑", 15);
                        else
                            labRow.Font = new Font("微软雅黑", 12);
                        labRow.Margin = new Padding(0);
                        pnlTilte[i].Controls.Add(labRow, 0, j);
                    }
                }

                //加载产品分割线
                for (int iTimer = 0; iTimer < CGlobalPara.C_Timer_MAX; iTimer++)
                {
                    pnlMain.Add(new TableLayoutPanel());
                    pnlMain[iTimer].Dock = DockStyle.Fill;
                    pnlMain[iTimer].CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                    pnlMain[iTimer].Margin = new Padding(0);
                    pnlMain[iTimer].Padding = new Padding(0);
                    pnlMain[iTimer].GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(pnlMain[iTimer], true, null);
                    pnlMain[iTimer].RowCount = CGlobalPara.C_Timer_Lay * 2;

                    for (int i = 0; i < CGlobalPara.C_Timer_Lay; i++)
                    {
                        pnlMain[iTimer].RowStyles.Add(new RowStyle(SizeType.Absolute, 20));
                        pnlMain[iTimer].RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                    }
                    pnlMain[iTimer].ColumnCount = CGlobalPara.C_Layer_Board;

                    for (int i = 0; i < CGlobalPara.C_Layer_Board; i++)
                        pnlMain[iTimer].ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

                }

                //初始化接口位置，分配单板对接的行和列                
                pnlUUTCon = new List<TableLayoutPanel>();

                int mSLot = 0;

                for (int iTimer = 0; iTimer < CGlobalPara.C_Timer_MAX; iTimer++)
                {
                    for (int i = 0; i < CGlobalPara.C_Timer_Lay; i++)
                    {
                        for (int j = 0; j < CGlobalPara.C_Layer_Board; j++)
                        {
                            Label lblSlot = new Label();        //加载指示灯位置标签
                            lblSlot.Name = "labSlot" + mSLot.ToString();
                          //  lblSlot.Text =  ((i * CGlobalPara.C_Layer_Board) + (j + 1)).ToString("D3");
                            lblSlot.Text = ((j%12 + 1)).ToString("D2");
                            lblSlot.Dock = DockStyle.Fill;
                            lblSlot.TextAlign = ContentAlignment.MiddleCenter;
                            lblSlot.ForeColor = System.Drawing.Color.Black;
                            lblSlot.Font = new Font("新宋体", 10);
                            lblSlot.Margin = new Padding(0);
                            lblSlot.Padding = new Padding(0);
                            lblUUTConTitle.Add(lblSlot);

                            TableLayoutPanel pnlLed = new TableLayoutPanel();       //加载指示灯框
                            pnlLed.Dock = DockStyle.Fill;
                            pnlLed.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
                            pnlLed.Margin = new Padding(1);
                            pnlLed.GetType().GetProperty("DoubleBuffered",
                                                    System.Reflection.BindingFlags.Instance |
                                                    System.Reflection.BindingFlags.NonPublic)
                                                    .SetValue(pnlLed, true, null);
                            pnlLed.RowCount = 1;
                            pnlLed.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                            pnlLed.ColumnCount = 1;
                            pnlLed.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                            //pnlLed.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
                            pnlUUTCon.Add(pnlLed);
                            mSLot += 1;
                        }
                    }
                }

                //加载测试信息
                for (int iTimer = 0; iTimer < CGlobalPara.C_Timer_MAX; iTimer++)
                {
                    Label labarea = new Label();
                    labarea.Name = "labAreaName" + (iTimer).ToString();
                    labarea.Text = CGlobalPara.SysPara.Para.timerNO[iTimer];
                    labarea.Dock = DockStyle.Fill;
                    labarea.TextAlign = ContentAlignment.MiddleCenter;
                    labarea.ForeColor = System.Drawing.Color.Black;
                    labarea.Font = new Font("微软雅黑", 9);
                    labarea.Margin = new Padding(0);
                    labarea.Padding = new Padding(0);
                    lblAreaName.Add(labarea);

                    Label labRun = new Label();
                    labRun.Text = CLanguage.Lan("空闲");
                    labRun.Dock = DockStyle.Fill;
                    labRun.TextAlign = ContentAlignment.MiddleCenter;
                    labRun.ForeColor = System.Drawing.Color.Blue;
                    labRun.Font = new Font("微软雅黑", 9);
                    labRun.Margin = new Padding(0);
                    labRun.Padding = new Padding(0);
                    lblRunType.Add(labRun);

                    Label labRunProgress = new Label();
                    labRunProgress.Text = CLanguage.Lan("0%");
                    labRunProgress.Dock = DockStyle.Fill;
                    labRunProgress.TextAlign = ContentAlignment.MiddleCenter;
                    labRunProgress.ForeColor = System.Drawing.Color.Blue;
                    labRunProgress.Font = new Font("微软雅黑", 9);
                    labRunProgress.Margin = new Padding(0);
                    labRunProgress.Padding = new Padding(0);
                    lblRunProgress.Add(labRunProgress);


                    TableLayoutPanel pnlpro = new TableLayoutPanel();       //加载运行进度条框
                    pnlpro.Dock = DockStyle.Fill;
                    pnlpro.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
                    pnlpro.Margin = new Padding(1);
                    pnlpro.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(pnlpro, true, null);
                    pnlpro.RowCount = 1;
                    pnlpro.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                    pnlpro.ColumnCount = 2;
                    pnlpro.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
                    pnlpro.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
                    pnlProRun.Add(pnlpro);

                    Label labUUTProgress = new Label();                     //运行进度条百分比
                    labUUTProgress.Text = CLanguage.Lan("0%");
                    labUUTProgress.Dock = DockStyle.Fill;
                    labUUTProgress.TextAlign = ContentAlignment.MiddleCenter;
                    labUUTProgress.ForeColor = System.Drawing.Color.Blue;
                    labUUTProgress.Font = new Font("微软雅黑", 9);
                    labUUTProgress.Margin = new Padding(0);
                    labUUTProgress.Padding = new Padding(0);
                    lblUUTProgress.Add(labUUTProgress);

                    ProgressBar progress = new ProgressBar();             //运行进度条百分比
                    progress.Maximum = _runUUT[iTimer].Para.BurnTime;
                    progress.Dock = DockStyle.Fill;
                    progress.Value = _runUUT[iTimer].Para.RunTime;
                    progress.ForeColor = System.Drawing.Color.Blue;
                    progress.Font = new Font("微软雅黑", 9);
                    progress.Margin = new Padding(0);
                    progress.Padding = new Padding(0);
                    proRunProgress.Add(progress);


                    Label labUUTStatus = new Label();
                    labUUTStatus.Text = CLanguage.Lan("关闭");
                    labUUTStatus.Dock = DockStyle.Fill;
                    labUUTStatus.TextAlign = ContentAlignment.MiddleCenter;
                    labUUTStatus.ForeColor = System.Drawing.Color.Blue;
                    labUUTStatus.Font = new Font("微软雅黑", 9);
                    labUUTStatus.Margin = new Padding(0);
                    labUUTStatus.Padding = new Padding(0);
                    lblUUTStatus.Add(labUUTStatus);
                }


                //加载公共接口数
                uiUUT = new List<udcUUT>();
                for (int iTimer = 0; iTimer < CGlobalPara.C_Timer_MAX; iTimer++)
                {
                    for (int i = 0; i < CGlobalPara.C_Board_MAX; i++)
                    {
                        udcUUT udcSlot = new udcUUT(_runUUT[iTimer], i, i);
                        udcSlot.Dock = DockStyle.Fill;
                        udcSlot.Margin = new Padding(1);
                        udcSlot.menuClick.OnEvent += new COnEvent<udcUUT.CSetMenuArgs>.OnEventHandler(OnMenuClick);
                        uiUUT.Add(udcSlot);
                    }
                }

                //加载公共接口数
                uiRunStatus = new List<udcRunStatus>();
                for (int iTimer = 0; iTimer < CGlobalPara.C_Timer_MAX; iTimer++)
                {
                   CTimer.WaitMs(1);
                    udcRunStatus udcType = new udcRunStatus(_runUUT[iTimer]);
                    udcType.Dock = DockStyle.Fill;
                    udcType.Margin = new Padding(1);
                    udcType.menuClick.OnEvent += new COnEvent<udcRunStatus.CRunStatusArgs>.OnEventHandler(OnRunStatusClick);
                    uiRunStatus.Add(udcType);

                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitialControl()
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(InitialControl));
            else
            {
                for (int i = 0; i < CGlobalPara.C_Area_MAX; i++)
                {
                    stcTilte.Controls.Add(pageArea[i]);

                    pageArea[i].Controls.Add(pnlAreaTimer[i]);

                    for (int j = 0; j < CGlobalPara.C_Area_Timer; j++)
                    {
                        int k = i * CGlobalPara.C_Area_Timer + j;

                        pnlAreaTimer[i].Controls.Add(pnlTimer[k], k, 0);
                        pnlTimer[k].Controls.Add(pnlUUT[k], 0, 0);

                        pnlUUT[k].Controls.Add(pnlTilte[k], 0, 1);
                        pnlUUT[k].Controls.Add(pnlMain[k], 1, 1);

                    }
                }
                //加载标题及母治具控件
                int mSLot = 0;
                for (int iTimer = 0; iTimer < CGlobalPara.C_Timer_MAX; iTimer++)
                {
                    //for (int i = 0; i < CGlobalPara.C_Timer_Lay; i++)
                    //{
                    //    for (int j = 0; j < CGlobalPara.C_Layer_Board; j++)
                    //    {
                    //        pnlMain[iTimer].Controls.Add(lblUUTConTitle[mSLot], j, i * 2);
                    //        pnlMain[iTimer].Controls.Add(pnlUUTCon[mSLot], j, i * 2 + 1);
                    //        mSLot += 1;
                    //    }
                    //}

                    for (int i = 0; i < CGlobalPara.C_Timer_Lay; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            pnlMain[iTimer].Controls.Add(lblUUTConTitle[mSLot], j, i * 2);
                            pnlMain[iTimer].Controls.Add(pnlUUTCon[mSLot], j, i * 2 + 1);
                            mSLot += 1;
                        }
                    }

                    for (int i = 0; i < CGlobalPara.C_Timer_Lay; i++)
                    {
                        for (int j = 3; j < CGlobalPara.C_Layer_Board; j++)
                        {
                            pnlMain[iTimer].Controls.Add(lblUUTConTitle[mSLot], j, i * 2);
                            pnlMain[iTimer].Controls.Add(pnlUUTCon[mSLot], j, i * 2 + 1);
                            mSLot += 1;
                        }
                    }
                }

                ////加载子治具控件
                for (int i = 0; i < CGlobalPara.C_Timer_MAX; i++)
                {
                    pnlTimer[i].Controls.Add(uiRunStatus[i], 1, 0);

                    IniTempChart(i);

                    uiRunStatus[i].SetRunStatus(_runUUT[i]);

                    for (int j = 0; j < CGlobalPara.C_Board_MAX; j++)
                    {
                        int k = i * CGlobalPara.C_Board_MAX + j;
                        uiUUT[k].Visible = false;
                        uiUUT[k].SetUUT(_runUUT[i], j);
                        pnlUUTCon[k].Controls.Add(uiUUT[k], 0, 0);
                        uiUUT[k].Visible = true;
                    }
                }
                //加载测试信息
                for (int iTimer = 0; iTimer < CGlobalPara.C_Timer_MAX; iTimer++)
                {

                    if (iTimer < 6)
                        pnlRunType.Controls.Add(lblAreaName[iTimer], 0, iTimer + 1);
                    else
                        pnlRunType.Controls.Add(lblAreaName[iTimer], 5, iTimer - 6 + 1);



                    if (iTimer < 6)
                        pnlRunType.Controls.Add(lblRunType[iTimer], 1, iTimer + 1);
                    else
                        pnlRunType.Controls.Add(lblRunType[iTimer], 6, iTimer - 6 + 1);

                    if (iTimer < 6)
                        pnlRunType.Controls.Add(pnlProRun[iTimer], 2, iTimer + 1);
                    else
                        pnlRunType.Controls.Add(pnlProRun[iTimer], 7, iTimer - 6 + 1);

                    pnlProRun[iTimer].Controls.Add(proRunProgress[iTimer], 0, 0);
                    pnlProRun[iTimer].Controls.Add(lblRunProgress[iTimer], 1, 0);


                    if (iTimer < 6)
                        pnlRunType.Controls.Add(lblUUTProgress[iTimer], 3, iTimer + 1);
                    else
                        pnlRunType.Controls.Add(lblUUTProgress[iTimer], 8, iTimer - 6 + 1);


                    if (iTimer < 6)
                        pnlRunType.Controls.Add(lblUUTStatus[iTimer], 4, iTimer + 1);
                    else
                        pnlRunType.Controls.Add(lblUUTStatus[iTimer], 9, iTimer - 6 + 1);

                }
                for (int iTimer = 0; iTimer < CGlobalPara.C_Timer_MAX; iTimer++)
                {
                    if (_runUUT[iTimer].Para.DoRun != AgingRunType.空闲)
                    {
                        for (int iuutNo = 0; iuutNo < CGlobalPara.C_Board_MAX; iuutNo++)
                            uiUUT[iuutNo + iTimer * CGlobalPara.C_Board_MAX].SetUUT(_runUUT[iTimer], iuutNo);
                    }

                    //for (int i = 0; i < _runUUT[iTimer].Led.Count; i++)
                    //{
                    //    if (_runUUT[iTimer].Led[i].barType == 1)
                    //        lblUUTConTitle[iTimer * CGlobalPara.C_UUT_MAX + _runUUT[iTimer].Led[i].barNo].ForeColor = Color.Blue;
                    //}
                }
              
            }
        }

        /// <summary>
        /// 设置双缓冲,防止界面闪烁
        /// </summary>
        private void SetDoubleBuffered()
        {
            skinSplitContainer1.Panel1.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(skinSplitContainer1.Panel1, true, null);
            skinSplitContainer1.Panel2.GetType().GetProperty("DoubleBuffered",
                                           System.Reflection.BindingFlags.Instance |
                                           System.Reflection.BindingFlags.NonPublic)
                                           .SetValue(skinSplitContainer1.Panel2, true, null);
            pnlRunType.GetType().GetProperty("DoubleBuffered",
                                           System.Reflection.BindingFlags.Instance |
                                           System.Reflection.BindingFlags.NonPublic)
                                           .SetValue(pnlRunType, true, null);
            pnlType.GetType().GetProperty("DoubleBuffered",
                                           System.Reflection.BindingFlags.Instance |
                                           System.Reflection.BindingFlags.NonPublic)
                                           .SetValue(pnlType, true, null);
            punAgingType.GetType().GetProperty("DoubleBuffered",
                                           System.Reflection.BindingFlags.Instance |
                                           System.Reflection.BindingFlags.NonPublic)
                                           .SetValue(punAgingType, true, null);
            
            panel2.GetType().GetProperty("DoubleBuffered",
                                         System.Reflection.BindingFlags.Instance |
                                         System.Reflection.BindingFlags.NonPublic)
                                         .SetValue(panel2, true, null);

        }

        /// <summary>
        /// 加载机种参数
        /// </summary>
        private void loadModelPara(int idNO, string modelPath)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action<int, string>(loadModelPara), idNO, modelPath);
            else
            {
                try
                {
                    CModelPara runModel = new CModelPara();

                    _runModel[idNO] = runModel;

                    if (!File.Exists(modelPath))
                        return;


                    COM.CSerializable<CModelPara>.ReadXml(modelPath, ref runModel);

                    //刷新界面
                    if (runModel != null)
                    {
                        _runModel[idNO] = runModel;
                        _defaultModelPath[idNO] = modelPath;
                        CIniFile.WriteToIni("Parameter", "ModelPath" + idNO, _defaultModelPath[idNO], CGlobalPara.IniFile);
                    }
                    else
                    {
                        _runUUT[idNO].Para.ModelName = "";
                        _runUUT[idNO].Para.RunTime = 0;
                        _runUUT[idNO].Para.BurnTime = 0;
                        _runUUT[idNO].OnOff.TimeRun.CurRunVolt = -1;
                        _runUUT[idNO].Para.StartTime = string.Empty;
                        _runUUT[idNO].Para.EndTime = string.Empty;

                        uiRunStatus[idNO].SetRunStatus(_runUUT[idNO]);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        #endregion

        #region 条码扫描
        private SerialPort barComm = new SerialPort();
        private StringBuilder builder = new StringBuilder();//避免在事件处理方法中反复的创建，定义到外面。
        private long received_count = 0;//接收计数
        private long send_count = 0;//发送计数
        private string readBar = string.Empty;
        private bool HexView = false;
        private int BarTimerNo = 0;
        private bool localbarOK = false;

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="comName">串口号</param>
        /// <param name="setting">9600,n,8,1</param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool openBar(string comName, string setting, out string er)
        {
            try
            {
                er = string.Empty;
                string[] arrayPara = setting.Split(',');
                if (arrayPara.Length != 4)
                {
                    er = "Com port parameters set wrong";
                    return false;
                }
                int bandRate = System.Convert.ToInt32(arrayPara[0]);
                Parity parity = Parity.None;
                switch (arrayPara[1].ToUpper())
                {
                    case "O":
                        parity = Parity.Odd;
                        break;
                    case "E":
                        parity = Parity.Even;
                        break;
                    case "M":
                        parity = Parity.Mark;
                        break;
                    case "S":
                        parity = Parity.Space;
                        break;
                    default:
                        break;
                }
                int dataBit = System.Convert.ToInt16(arrayPara[2]);
                StopBits stopBits = StopBits.One;
                switch (arrayPara[3])
                {
                    case "0":
                        stopBits = StopBits.None;
                        break;
                    case "1.5":
                        stopBits = StopBits.OnePointFive;
                        break;
                    case "2":
                        stopBits = StopBits.Two;
                        break;
                    default:
                        break;
                }


                if (barComm != null)
                {
                    if (barComm.IsOpen)
                        barComm.Close();
                    barComm = null;
                }
                barComm = new SerialPort(comName, bandRate, parity, dataBit, stopBits);
                barComm.Open();
                //初始化SerialPort对象
                barComm.NewLine = "/r/n";
                barComm.RtsEnable = true;//根据实际情况吧。
                //添加事件注册
                barComm.DataReceived += Barcomm_DataReceived;
                return true;
            }
            catch (Exception e)
            {
                er = e.ToString();
                return false;
            }
        }
        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <returns></returns>
        private bool closeBar()
        {
            if (barComm != null)
            {
                if (barComm.IsOpen)
                    barComm.Close();
                barComm = null;
            }
            return true;
        }
        /// <summary>
        /// 串口触发采集条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Barcomm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            CTimer.WaitMs(50);//用于接收完整信息

            int n = barComm.BytesToRead;        //先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致

            byte[] buf = new byte[n];           //声明一个临时数组存储当前来的串口数据
            received_count += n;                //增加接收计数
            barComm.Read(buf, 0, n);            //读取缓冲数据
            builder.Clear();                    //清除字符串构造器的内容
            //因为要访问ui资源，所以需要使用invoke方式同步ui。
            this.Invoke((EventHandler)(delegate
            {
                //判断是否是显示为16禁止
                if (HexView)
                {
                    //依次的拼接出16进制字符串
                    foreach (byte b in buf)
                    {
                        builder.Append(b.ToString("X2") + " ");
                    }
                }
                else
                {
                    //直接按ASCII规则转换成字符串
                    builder.Append(Encoding.ASCII.GetString(buf));
                }

                readBar = builder.ToString();



            }));
        }

        /// <summary>
        /// 导入条码
        /// </summary>
        /// <param name="readBar"></param>
        private void ChkSN_Bar(string readBar)
        {
            //获取位置条码
            for (int i = 0; i < CGlobalPara.C_Timer_MAX; i++)
            {
                if (readBar.ToUpper() == _runUUT[i].Led[0].localBar)
                {
                    BarTimerNo = i;
                    localbarOK = true;
                    break;
                }
            }
            if (localbarOK)     //位置条码OK 
            {



            }
            else
            {
                runLog.Log(readBar + "获取位置条码失败,未查询到位置条码!", udcRunLog.ELog.Err);
            }
        }

        # endregion

        #region 温度曲线

        /// <summary>
        /// 清除曲线
        /// </summary>
        /// <param name="iTimer"></param>
        private void ClearTempChart(int iTimer)
        {
            string TempPath = Application.StartupPath + "\\TempData";
            if (!Directory.Exists(TempPath))
            {
                Directory.CreateDirectory(TempPath);
                return;
            }
            string filepath = TempPath + "\\" + _runUUT[iTimer].Para.TimerName + ".txt";
            if (!File.Exists(filepath))
                return;
            else
                File.Delete(filepath);
            _runUUT[iTimer].PLC.tempTime.Clear();
            _runUUT[iTimer].PLC.tempVal.Clear();
            IniTempChart(iTimer);
        }

        /// <summary>
        /// 初始化温度曲线
        /// </summary>
        /// <param name="iTimer"></param>
        private void IniTempChart(int iTimer)
        {
            string TempPath = Application.StartupPath + "\\TempData";
            if (!Directory.Exists(TempPath))
            {
                Directory.CreateDirectory(TempPath);
                return;
            }
            string filepath = TempPath + "\\" + _runUUT[iTimer].Para.TimerName + ".txt";
            if (!File.Exists(filepath))
                return;

            StreamReader sr = new StreamReader(filepath, Encoding.Default);
            string str = sr.ReadLine();
            _runUUT[iTimer].PLC.tempTime.Clear();
            _runUUT[iTimer].PLC.tempVal.Clear();
            while (str != null)
            {
                str = sr.ReadLine();
                if (str != null)
                {
                    string[] Str1 = str.Split(',');
                    if (Str1.Length >= 3)
                    {
                        _runUUT[iTimer].PLC.tempTime.Add(Convert.ToDouble(Str1[1]));
                        _runUUT[iTimer].PLC.tempVal.Add(Convert.ToDouble(Str1[2]));
                    }
                }
            }
        }

        /// <summary>
        /// 刷新温度曲线
        /// </summary>
        /// <param name="iTimer"></param>
        private void RefTempChart(int iTimer)
        {
            try
            {
                string TempPath = Application.StartupPath + "\\TempData";
                if (!Directory.Exists(TempPath))
                    Directory.CreateDirectory(TempPath);

                string filepath = TempPath + "\\" + _runUUT[iTimer].Para.TimerName + ".txt";

                bool IsExist = true;

                if (!File.Exists(filepath))
                    IsExist = false;

                StreamWriter sw = new StreamWriter(filepath, true, Encoding.UTF8);

                string strWrite = string.Empty;

                if (!IsExist)//写入标题栏
                {
                    strWrite = "序号,时间,水冷温度";
                    sw.WriteLine(strWrite);
                }

                _runUUT[iTimer].PLC.tempTime.Add((int)_runUUT[iTimer].Para.RunTime / 60);

                _runUUT[iTimer].PLC.tempVal.Add(_runUUT[iTimer].PLC.curTemp);

                int startCount = _runUUT[iTimer].PLC.tempTime.Count;

                strWrite = _runUUT[iTimer].PLC.tempTime.Count.ToString() + "," + _runUUT[iTimer].PLC.tempTime[startCount - 1].ToString() + "," + _runUUT[iTimer].PLC.tempVal[startCount - 1].ToString();

                sw.WriteLine(strWrite);
                sw.Flush();
                sw.Close();
                sw = null;
                if (startCount > 11000)
                    File.Delete(filepath);
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
        }

        #endregion

        #region 面板控件

        #region 新增控件
        /// <summary>
        /// 区域分页 
        /// </summary>
        private List<TabPage> pageArea = new List<TabPage>();
        /// <summary>
        /// 区域时序 
        /// </summary>
        private List<TableLayoutPanel> pnlAreaTimer = new List<TableLayoutPanel>();

        /// <summary>
        /// 时序分页（产品栏+状态栏）  
        /// </summary>
        private List<TableLayoutPanel> pnlTimer = new List<TableLayoutPanel>();

        /// <summary>
        /// 产品分页 （层号+产品）   
        /// </summary>
        private List<TableLayoutPanel> pnlUUT = new List<TableLayoutPanel>();

        /// <summary>
        /// 标题栏 
        /// </summary>
        private List<TableLayoutPanel> pnlTilte = new List<TableLayoutPanel>();

        /// <summary>
        /// 主框架
        /// </summary>
        private List<TableLayoutPanel> pnlMain = new List<TableLayoutPanel>();

        /// <summary>
        /// 显示分割
        /// </summary>
        private List<TableLayoutPanel> pnlShow = new List<TableLayoutPanel>();

        /// <summary>
        /// 产品负载接口（按最大通道计算）
        /// </summary>
        private List<TableLayoutPanel> pnlUUTCon = null;

        /// <summary>
        /// 产品负载接口标题
        /// </summary>
        private List<Label> lblUUTConTitle = new List<Label>();

        /// <summary>
        /// 产品
        /// </summary>
        private List<udcUUT> uiUUT = new List<udcUUT>();

        /// <summary>
        /// 状态
        /// </summary>
        private List<udcRunStatus> uiRunStatus = new List<udcRunStatus>();

        /// <summary>
        /// 区号
        /// </summary>
        private List<Label> lblAreaName = new List<Label>();


        /// <summary>
        /// 运行状态
        /// </summary>
        private List<Label> lblRunType = new List<Label>();

        /// <summary>
        /// 运行进度
        /// </summary>
        private List<Label> lblRunProgress = new List<Label>();

        /// <summary>
        /// 运行进度条
        /// </summary>
        private List<ProgressBar> proRunProgress = new List<ProgressBar>();

        /// <summary>
        /// 运行进度条
        /// </summary>
        private List<TableLayoutPanel> pnlProRun = new List<TableLayoutPanel>();

        /// <summary>
        /// 直通率
        /// </summary>
        private List<Label> lblUUTProgress = new List<Label>();

        /// <summary>
        /// 温度状态
        /// </summary>
        private List<Label> lblUUTStatus = new List<Label>();

        /// <summary>
        /// 运行状态
        /// </summary>
        private Label[] lblStatus = null;

        #endregion

        #region 界面控件方法

        /// <summary>
        /// 不良数据隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvFailData_DoubleClick(object sender, EventArgs e)
        {
            gpbFailShow.Visible = false;
        }

     
        /// <summary>
        /// 显示不良状态框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_DoubleClick(object sender, EventArgs e)
        {
            UpdateFailUI();
            gpbFailShow.Visible = true;
            gpbFailShow.BringToFront();
        }

    
        /// <summary>
        /// 显示并联产品数量
        /// </summary>
        /// <param name="iTimer"></param>
        private void showUUT( int iTimer)
        {
            try
            {

                if (this.InvokeRequired)
                {
                    this.Invoke(new Action<int>(showUUT),iTimer);
                }
                else
                {
                    //if (_runModel[iTimer].Para.ChanAdd == 0 )
                    //{
                    //    return;
                    //}
                    //int OutChan = _runModel[iTimer].Para.ChanAdd ;
                    //bool showSlot = false;
                    //int ChanNo = 0;
                    //for (int k = 0; k < CGlobalPara.C_Layer_Board; k++)
                    //{
                    //     showSlot = false;
                    //    if (OutChan == 1)
                    //    {
                    //        pnlMain[iTimer].ColumnStyles[k] = new ColumnStyle(SizeType.Percent, 100);
                    //        showSlot = true;
                    //    }
                    //    else
                    //    {
                    //        if (k%OutChan == 0)
                    //        {
                    //            pnlMain[iTimer].ColumnStyles[k] = new ColumnStyle(SizeType.Percent, 100);
                    //            showSlot = true;
                    //        }
                    //        else
                    //        {
                    //            pnlMain[iTimer].ColumnStyles[k] = new ColumnStyle(SizeType.Percent, 0);
                    //        }
                    //    }

                    //    if (showSlot)
                    //    {
                    //        ChanNo++;
                    //        for (int i = 0; i < CGlobalPara.C_Timer_Lay; i++)
                    //        {
                    //            lblUUTConTitle[iTimer*CGlobalPara.C_UUT_MAX + i*CGlobalPara.C_Layer_Board + k].Text =
                    //                (ChanNo).ToString("D2");
                    //        }

                    //    }
                    //    else
                    //    {
                    //        for (int i = 0; i < CGlobalPara.C_Timer_Lay; i++)
                    //        {
                    //            lblUUTConTitle[iTimer * CGlobalPara.C_UUT_MAX + i * CGlobalPara.C_Layer_Board + k].Text =
                    //               "";
                    //        }
                    //    }
                    //}
                }

            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.NG);
            }
        }
        #endregion

        #endregion

        #region 数据操作

        /// <summary>
        /// 选机种初始化运行参数
        /// </summary>
        /// <param name="iTimer"></param>
        private void updata_inirunPara_database(int iTimer)
        {
            try
            {
                string er = string.Empty;

                CDBCOM db = new CDBCOM(EDBType.Access, "", CGlobalPara.SysDB);

                List<string> sqlCmdList = new List<string>();

                string OutPutList = string.Empty;

                for (int i = 0; i < _runUUT[iTimer].Para.OutPutNum; i++)
                {
                    OutPutList += _runUUT[iTimer].OnOff.OutPut[i].iicSpec + "*";

                    for (int z = 0; z < _runUUT[iTimer].Para.OutPutChan; z++)
                    {

                        OutPutList += _runUUT[iTimer].OnOff.OutPut[i].Chan[z].Vuse.ToString() + ",";
                        OutPutList += _runUUT[iTimer].OnOff.OutPut[i].Chan[z].Vname + ",";
                        OutPutList += _runUUT[iTimer].OnOff.OutPut[i].Chan[z].Vmin.ToString() + ",";
                        OutPutList += _runUUT[iTimer].OnOff.OutPut[i].Chan[z].Vmax.ToString() + ",";
                        OutPutList += _runUUT[iTimer].OnOff.OutPut[i].Chan[z].Imode.ToString() + ",";
                        OutPutList += _runUUT[iTimer].OnOff.OutPut[i].Chan[z].ISet.ToString() + ",";
                        OutPutList += _runUUT[iTimer].OnOff.OutPut[i].Chan[z].Imin.ToString() + ",";
                        OutPutList += _runUUT[iTimer].OnOff.OutPut[i].Chan[z].Imax.ToString() + ",";
                        OutPutList += _runUUT[iTimer].OnOff.OutPut[i].Chan[z].Von.ToString() + ",";
                        OutPutList += _runUUT[iTimer].OnOff.OutPut[i].Chan[z].AddMode.ToString() + ",";

                        OutPutList += "*";
                    }


                    if (i != _runUUT[iTimer].Para.OutPutNum - 1)
                        OutPutList += ";";
                }

                string OnOffList = string.Empty;

                for (int i = 0; i < _runUUT[iTimer].Para.OnOffNum; i++)
                {
                    OnOffList += _runUUT[iTimer].OnOff.OnOff[i].inPutVolt.ToString() + ",";
                    OnOffList += _runUUT[iTimer].OnOff.OnOff[i].OnOffTime + ",";
                    OnOffList += _runUUT[iTimer].OnOff.OnOff[i].OnTime + ",";
                    OnOffList += _runUUT[iTimer].OnOff.OnOff[i].OffTime + ",";
                    OnOffList += _runUUT[iTimer].OnOff.OnOff[i].outPutCur + ",";
                    OnOffList += _runUUT[iTimer].OnOff.OnOff[i].dcONOFF + ",";
                    if (i != _runUUT[iTimer].Para.OnOffNum - 1)
                        OnOffList += ";";
                }

                string sqlCmd = "Update RUN_PARA set DoRun=" + (int)_runUUT[iTimer].Para.DoRun +
                                ",MesFlag=" + _runUUT[iTimer].Para.MesFlag + ",ModelName='" + _runUUT[iTimer].Para.ModelName +
                                "',BarFlag=" + _runUUT[iTimer].Para.BarFlag + ",FixBar=" + _runUUT[iTimer].Para.FixBar +
                                ",barSpec='" + _runUUT[iTimer].Para.BarFlag + "',barLength=" + _runUUT[iTimer].Para.barLength +
                                ",BurnTime=" + _runUUT[iTimer].Para.BurnTime +
                                ",StartTime='',EndTime='',SavePath='',SaveWebPath='',SaveBakPath='',SaveDataTime='',SaveDataIndex=0,RunTime=0,OutPutChan=" +
                                _runUUT[iTimer].Para.OutPutChan + ",OutPutNum=" + _runUUT[iTimer].Para.OutPutNum + ",OnOffNum=" +
                                _runUUT[iTimer].Para.OnOffNum + ",CurStepNo=" + _runUUT[iTimer].OnOff.TimeRun.CurStepNo +
                                ",CurRunVolt=" + _runUUT[iTimer].OnOff.TimeRun.CurRunVolt + ",CurDCONOFF=" +
                                _runUUT[iTimer].OnOff.TimeRun.CurDCONOFF + ",CurRunOutPut=" + _runUUT[iTimer].OnOff.TimeRun.CurRunOutPut +
                                ",OutPutList='" + OutPutList + "',OnOffList='" + OnOffList +
                                "' where TimerNO=" + iTimer;
                sqlCmdList.Add(sqlCmd);

                if (!db.excuteSQLArray(sqlCmdList, out er))
                    runLog.Log(er, udcRunLog.ELog.NG);

            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
        }

        /// <summary>
        /// 更新产品运行参数
        /// </summary>
        /// <param name="iTimer"></param>
        private void updata_runPara_database(int iTimer)
        {
            try
            {
                string er = string.Empty;

                CDBCOM db = new CDBCOM(EDBType.Access, "", CGlobalPara.SysDB);

                List<string> sqlCmdList = new List<string>();

                string sqlCmd = "Update RUN_PARA set DoRun=" + (int)_runUUT[iTimer].Para.DoRun +
                                ",StartTime='" + _runUUT[iTimer].Para.StartTime + "',EndTime='" + _runUUT[iTimer].Para.EndTime +
                                "',MO_NO='" + _runUUT[iTimer].Para.MO_NO + "',UserName='" + _runUUT[iTimer].Para.UserName +
                                "',SaveDataTime='" + _runUUT[iTimer].Para.SaveDataTime +
                                "',SaveDataIndex=" + _runUUT[iTimer].Para.SaveDataIndex + ",RunTime=" + _runUUT[iTimer].Para.RunTime +
                                ",PassNum=" + _runUUT[iTimer].Para.PassNum + ",TTNum=" + _runUUT[iTimer].Para.TTNum + ",CurStepNo=" +
                                _runUUT[iTimer].OnOff.TimeRun.CurStepNo + ",CurRunVolt=" + _runUUT[iTimer].OnOff.TimeRun.CurRunVolt +
                                ",CurRunOutPut=" + _runUUT[iTimer].OnOff.TimeRun.CurRunOutPut + ",SavePath='" + _runUUT[iTimer].Para.SavePath +
                                "' where TimerNO=" + iTimer;
                sqlCmdList.Add(sqlCmd);

                if (!db.excuteSQLArray(sqlCmdList, out er))
                    runLog.Log(er, udcRunLog.ELog.NG);

            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
        }
        /// <summary>
        /// 更新产品运行数据
        /// </summary>
        /// <param name="iTimer"></param>
        private void updata_runData_database(int iTimer)
        {
            try
            {
                string er = string.Empty;

                CDBCOM db = new CDBCOM(EDBType.Access, "", CGlobalPara.SysDB);

                List<string> sqlCmdList = new List<string>();

                string sqlCmd = "Update RUN_PARA set DoRun=" + (int)_runUUT[iTimer].Para.DoRun +
                                 ",SaveDataTime='" + _runUUT[iTimer].Para.SaveDataTime + "',SaveDataIndex="
                                 + _runUUT[iTimer].Para.SaveDataIndex + ",RunTime=" + _runUUT[iTimer].Para.RunTime +
                                ",CurStepNo=" + _runUUT[iTimer].OnOff.TimeRun.CurStepNo + ",CurRunVolt=" +
                                _runUUT[iTimer].OnOff.TimeRun.CurRunVolt + ",CurDCONOFF=" +
                                _runUUT[iTimer].OnOff.TimeRun.CurDCONOFF + ",CurRunOutPut=" + _runUUT[iTimer].OnOff.TimeRun.CurRunOutPut +
                                " where TimerNO=" + iTimer;
                sqlCmdList.Add(sqlCmd);

                if (!db.excuteSQLArray(sqlCmdList, out er))
                    runLog.Log(er, udcRunLog.ELog.NG);

            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
        }
        /// <summary>
        /// 重置产品位
        /// </summary>
        /// <param name="iTimer"></param>
        private void updata_uutSpec_database(int iTimer)
        {
            try
            {

                string er = string.Empty;

                CDBCOM db = new CDBCOM(EDBType.Access, "", CGlobalPara.SysDB);

                List<string> sqlCmdList = new List<string>();
                sqlCmdList.Clear();
                string sqlCmd = "Update RUN_DATA set unitV=0,unitA=0,result=0,failEnd=0,failTime=''," +
                                "failInfo='',iicData='' where timerNo=" + (iTimer + 1);
                sqlCmdList.Add(sqlCmd);
                for (int i = 0; i < _runUUT[iTimer].Led.Count / _runUUT[iTimer].Para.OutPutChan; i++)
                {
                    for (int j = 0; j < _runUUT[iTimer].Para.OutPutChan; j++)
                    {
                        int uutNo = i * _runUUT[iTimer].Para.OutPutChan + j;

                        _runUUT[iTimer].Led[uutNo].iUUT = (i + 1);
                        _runUUT[iTimer].Led[uutNo].iCH = (j + 1);

                        sqlCmd = "Update RUN_DATA set iUUT=" + (i + 1) + ",iCH=" + (j + 1) +
                                 " where timerNo=" + (iTimer + 1) + " and uutNo=" + uutNo;
                        sqlCmdList.Add(sqlCmd);
                    }
                }

                if (!db.excuteSQLArray(sqlCmdList, out er))
                    runLog.Log(er, udcRunLog.ELog.NG);

            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
        }

        /// <summary>
        /// 更新输出电压的规格设定
        /// </summary>
        /// <param name="iTimer"></param>
        private void updata_outSpec_database(int iTimer)
        {
            try
            {
                string er = string.Empty;

                int stepNo = _runUUT[iTimer].OnOff.TimeRun.CurRunOutPut;

                //获取新的负载数据
                for (int i = 0; i < _runUUT[iTimer].Led.Count / _runUUT[iTimer].Para.OutPutChan; i++)
                {
                    for (int j = 0; j < _runUUT[iTimer].Para.OutPutChan; j++)
                    {
                        int slot = i * _runUUT[iTimer].Para.OutPutChan + j;
                        if (_runUUT[iTimer].Led[slot].modelName == _runUUT[iTimer].Para.ModelName)
                        {   
                            _runUUT[iTimer].Led[slot].vUse = _runUUT[iTimer].OnOff.OutPut[stepNo].Chan[j].Vuse;
                            _runUUT[iTimer].Led[slot].vName = _runUUT[iTimer].OnOff.OutPut[stepNo].Chan[j].Vname;
                            _runUUT[iTimer].Led[slot].vMin = _runUUT[iTimer].OnOff.OutPut[stepNo].Chan[j].Vmin;
                            _runUUT[iTimer].Led[slot].vMax = _runUUT[iTimer].OnOff.OutPut[stepNo].Chan[j].Vmax;
                            _runUUT[iTimer].Led[slot].IMode = _runUUT[iTimer].OnOff.OutPut[stepNo].Chan[j].Imode;
                            _runUUT[iTimer].Led[slot].ISet = _runUUT[iTimer].OnOff.OutPut[stepNo].Chan[j].ISet;
                            _runUUT[iTimer].Led[slot].Imin = _runUUT[iTimer].OnOff.OutPut[stepNo].Chan[j].Imin;
                            _runUUT[iTimer].Led[slot].Imax = _runUUT[iTimer].OnOff.OutPut[stepNo].Chan[j].Imax;
                            _runUUT[iTimer].Led[slot].Von = _runUUT[iTimer].OnOff.OutPut[stepNo].Chan[j].Von;
                            _runUUT[iTimer].Led[slot].AddMode = _runUUT[iTimer].OnOff.OutPut[stepNo].Chan[j].AddMode;
                            _runUUT[iTimer].Led[slot].iicSpec = _runUUT[iTimer].OnOff.OutPut[stepNo].iicSpec;

                            _runUUT[iTimer].Led[slot].unitV = 0;
                            _runUUT[iTimer].Led[slot].unitA = 0;
                            _runUUT[iTimer].Led[slot].vFailNum = 0;
                            _runUUT[iTimer].Led[slot].iFailNum = 0;
                        }
                    }
                }
                CDBCOM db = new CDBCOM(EDBType.Access, "", CGlobalPara.SysDB);

                List<string> sqlCmdList = new List<string>();

                for (int slot = 0; slot < _runUUT[iTimer].Led.Count; slot++)
                {
                    if (_runUUT[iTimer].Led[slot].modelName == _runUUT[iTimer].Para.ModelName)
                    {
                        string sqlCmd = "Update RUN_DATA set vUse=" + _runUUT[iTimer].Led[slot].vUse +
                                        ",vName='" + _runUUT[iTimer].Led[slot].vName + "',vMin=" +
                                        _runUUT[iTimer].Led[slot].vMin + ",vMax=" + _runUUT[iTimer].Led[slot].vMax +
                                        ",IMode=" + _runUUT[iTimer].Led[slot].IMode + ",ISet=" + _runUUT[iTimer].Led[slot].ISet +
                                        ",Imin=" + _runUUT[iTimer].Led[slot].Imin + ",Imax=" + _runUUT[iTimer].Led[slot].Imax +
                                        ",Von=" + _runUUT[iTimer].Led[slot].Von + ",AddMode=" + _runUUT[iTimer].Led[slot].AddMode +
                                        ",iicSpec='" + _runUUT[iTimer].Led[slot].iicSpec + "',modelName='" + _runUUT[iTimer].Led[slot].modelName +
                                        "' where  timerNo=" + (iTimer + 1) + " and uutNo=" + slot;
                        sqlCmdList.Add(sqlCmd);
                    }
                }
                if (!db.excuteSQLArray(sqlCmdList, out er))
                    runLog.Log(er, udcRunLog.ELog.NG);

            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
        }

        /// <summary>
        /// 更新位置条码
        /// </summary>
        /// <param name="iTimer"></param>
        private void updata_localBar_database(int iTimer)
        {
            try
            {
                string er = string.Empty;
                CDBCOM db = new CDBCOM(EDBType.Access, "", CGlobalPara.SysDB);
                List<string> sqlCmdList = new List<string>();
                for (int i = 0; i < _runUUT[iTimer].Led.Count; i++)
                {
                    _runUUT[iTimer].Led[i].barNo = 0;
                    _runUUT[iTimer].Led[i].localPath = "";
                    _runUUT[iTimer].Led[i].localBar = "";
                    _runUUT[iTimer].Led[i].canScanLocal = 0;
                }
                string sqlCmd = "Update RUN_DATA set localPath='',localBar='',barNo=0,canScanLocal=0  where timerNo=" + (iTimer + 1);
                sqlCmdList.Add(sqlCmd);
                if (!db.excuteSQLArray(sqlCmdList, out er))
                    runLog.Log(er, udcRunLog.ELog.NG);

                sqlCmdList.Clear();

                for (int i = 0; i < CGlobalPara.C_Timer_Lay; i++)
                {
                    for (int j = 0; j < CGlobalPara.C_Layer_UUT / _runUUT[iTimer].Para.OutPutChan; j++)
                    {
                        int uutNo = i * CGlobalPara.C_Layer_UUT + j * _runUUT[iTimer].Para.OutPutChan;
                        int barNo = i * CGlobalPara.C_Layer_UUT + j * _runUUT[iTimer].Para.OutPutChan;
                        string localPath = "L" + (i + 1).ToString("D1") + "_" + (i * CGlobalPara.C_Layer_UUT + j * _runUUT[iTimer].Para.OutPutChan + 1).ToString("D2");
                        string localBar = "L" + (i + 1).ToString("D1") + (i * CGlobalPara.C_Layer_UUT + j * _runUUT[iTimer].Para.OutPutChan + 1).ToString("D3");
                        _runUUT[iTimer].Led[uutNo].barNo = barNo;
                        _runUUT[iTimer].Led[uutNo].localPath = localPath;
                        _runUUT[iTimer].Led[uutNo].localBar = localBar;

                        sqlCmd = "Update RUN_DATA set localPath='" + localPath + "',localBar='" + localBar +
                                       "',barNo=" + barNo + " where timerNo=" + (iTimer + 1) + " and uutNo=" + uutNo;
                        sqlCmdList.Add(sqlCmd);
                    }
                }

                for (int i = 0; i < CGlobalPara.C_Timer_Lay; i++)
                {
                    for (int j = 0; j < CGlobalPara.C_Layer_UUT / _runUUT[iTimer].Para.OutPutChan; j++)
                    {
                        int uutNo = (i * CGlobalPara.C_Layer_UUT) + j * _runUUT[iTimer].Para.OutPutChan;
                        _runUUT[iTimer].Led[uutNo].canScanLocal = 1;
                        sqlCmd = "Update RUN_DATA set canScanLocal=1 where timerNo=" + (iTimer + 1) + " and uutNo=" + uutNo;
                        sqlCmdList.Add(sqlCmd);
                    }
                }

                if (!db.excuteSQLArray(sqlCmdList, out er))
                    runLog.Log(er, udcRunLog.ELog.NG);
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
        }

        #endregion

        #region 老化房手动功能

        /// <summary>
        /// 手动按钮功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRunStatusClick(object sender, udcRunStatus.CRunStatusArgs e)
        {
       
            int uutNo = 0;
            uutNo = e.idNo;
            if (!_Start)
            {
                MessageBox.Show("请先启动程序！", "启动提示", MessageBoxButtons.OK);
                return;
            }
            switch (e.menuInfo)
            {
                case udcRunStatus.ESetMenu.选择机种:
                    select_model_info(e.idNo);
                    break;
                case udcRunStatus.ESetMenu.自检老化:
                    ctrl_reStart_info(e.idNo);
                    break;
                case udcRunStatus.ESetMenu.启动老化:
                    ctrl_Start_info(e.idNo);
                    break;
                case udcRunStatus.ESetMenu.扫描条码:
                    ctrl_scanBar_info(e.idNo);
                    break;
                case udcRunStatus.ESetMenu.暂停老化:
                    ctrl_Pause_info(e.idNo, true);
                    break;
                case udcRunStatus.ESetMenu.继续老化:
                    ctrl_continue_info(e.idNo,true );
                    break;
                case udcRunStatus.ESetMenu.停止老化:
                    ctrl_Stop_info(e.idNo);
                    break;
                case udcRunStatus.ESetMenu.温度显示:
                    ctrl_show_tempInfo(e.idNo);
                    break;
                case udcRunStatus.ESetMenu.时序显示:
                    ctrl_show_runInfo(e.idNo);
                    break;                  
                default:
                    break;
            }
        }

        /// <summary>
        /// 选择机种参数
        /// </summary>
        /// <param name="iTimer"></param>
        private void select_model_info(int iTimer)
        {
            try
            {

                string er = string.Empty;

                if (_runUUT[iTimer].Para.DoRun != AgingRunType.空闲 && _runUUT[iTimer].Para.DoRun != AgingRunType.扫条码)
                {
                    MessageBox.Show(CLanguage.Lan("请停止老化后再选择机种！"), CLanguage.Lan("提示"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    //udcSelectModel selmodel = new udcSelectModel(CLanguage.Lan("请输入机种名称:"), iTimer);

                    //if (selmodel.ShowDialog() != DialogResult.OK)
                    //    return;

                    //string modelFile = CGlobalPara.SysPara.Report.ModelPath + "\\" + udcSelectModel.modelName + ".bi";
                    if (_runUUT[iTimer].Para.ChooseModel == true)
                        return;
                    _runUUT[iTimer].Para.ChooseModel = true;
                    string fileDirectry = string.Empty;
                    fileDirectry = CGlobalPara.SysPara.Report.ModelPath;
                    OpenFileDialog dlg = new OpenFileDialog();
                    dlg.InitialDirectory = fileDirectry;
                    dlg.Filter = "BI files (*.bi)|*.bi";
                    if (dlg.ShowDialog() != DialogResult.OK)
                        return;
                    string modelFile = dlg.FileName;

                    runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("获取机种参数!"), udcRunLog.ELog.Action);
                    loadModelPara(iTimer, modelFile);

                    if (_runModel[iTimer].Base.Model == null)
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("获取机种参数") + udcSelectModel.modelName + CLanguage.Lan("异常!"), udcRunLog.ELog.Err);
                    }

                    else
                    {

                        showUUT(iTimer);
                        //输出
                        for (int slot = 0; slot < _runUUT[iTimer].Led.Count; slot++)
                        {
                            _runUUT[iTimer].Led[slot].unitV = 0;
                            _runUUT[iTimer].Led[slot].unitA = 0;
                            _runUUT[iTimer].Led[slot].result = 0;
                            _runUUT[iTimer].Led[slot].HaveCanId = false;
                            _runUUT[iTimer].Led[slot].vFailNum = 0;
                            _runUUT[iTimer].Led[slot].iFailNum = 0;
                            _runUUT[iTimer].Led[slot].failEnd = 0;
                            _runUUT[iTimer].Led[slot].failTime = "";
                            _runUUT[iTimer].Led[slot].failInfo = "";
                            _runUUT[iTimer].Led[slot].iicData = "";
                        }
                  

                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("初始化PLC参数!"), udcRunLog.ELog.Action);

                        _runUUT[iTimer].Para.ModelName = _runModel[iTimer].Base.Model;
                        _runUUT[iTimer].Para.MesFlag = _runModel[iTimer].Base.MesFlag;
                        _runUUT[iTimer].Para.BarFlag = CGlobalPara.SysPara.Para.BarFlag;
                        _runUUT[iTimer].Para.FixBar = _runModel[iTimer].Base.fixBar;
                        _runUUT[iTimer].Para.barSpec = _runModel[iTimer].Base.BarSpec;
                        _runUUT[iTimer].Para.barLength = _runModel[iTimer].Base.BarLength;

                        _runUUT[iTimer].Para.OutPutChan = _runModel[iTimer].Para.OutPut_Chan;
                        _runUUT[iTimer].Para.RunTime = 0;
                        _runUUT[iTimer].Para.BurnTime = _runModel[iTimer].Para.BITime;
                        _runUUT[iTimer].Para.TTNum = 0;
                        _runUUT[iTimer].Para.PassNum = 0;
                        _runUUT[iTimer].Para.StartTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                        _runUUT[iTimer].Para.EndTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                        _runUUT[iTimer].Para.UserName = CIniFile.ReadFromIni("UseSet", "UserName", CGlobalPara.UserFile, "");

                        for (int i = 0; i < CGlobalPara.C_Board_MAX; i++)
                        {
                            uiUUT[i + iTimer * CGlobalPara.C_Board_MAX].SetUUT(_runUUT[iTimer], i);

                        }

                        if (CPara.GetOutPutAndOnOffFromModel(_runModel[iTimer], ref _runUUT[iTimer].OnOff, out er))
                        {
                            _runUUT[iTimer].Para.OutPutChan = _runUUT[iTimer].OnOff.TimeSpec.OutChanNum;
                            _runUUT[iTimer].Para.OutPutNum = _runUUT[iTimer].OnOff.TimeSpec.OutPutNum;
                            _runUUT[iTimer].Para.OnOffNum = _runUUT[iTimer].OnOff.OnOff.Count;
                            _runUUT[iTimer].OnOff.TimeRun.CurStepNo = 0;
                            _runUUT[iTimer].OnOff.TimeRun.CurRunVolt = _runModel[iTimer].OnOff[0].Item[0].InPutV;
                            _runUUT[iTimer].OnOff.TimeRun.CurDCONOFF = _runModel[iTimer].OnOff[0].Item[0].dcONOFF;
                            _runUUT[iTimer].OnOff.TimeRun.CurRunOutPut = _runModel[iTimer].OnOff[0].Item[0].OutPutType;
                            _runUUT[iTimer].Para.RunInVolt = _runUUT[iTimer].OnOff.TimeRun.CurRunVolt;
                            _runUUT[iTimer].Para.DCONOFF = _runUUT[iTimer].OnOff.TimeRun.CurDCONOFF;

                        }
                        int outPutNo = _runUUT[iTimer].OnOff.OnOff[0].outPutCur;

                        //输出
                        for (int i = 0; i < _runUUT[iTimer].Led.Count / _runUUT[iTimer].Para.OutPutChan; i++)
                        {
                            for (int j = 0; j < _runUUT[iTimer].Para.OutPutChan; j++)
                            {
                                int slot = i * _runUUT[iTimer].Para.OutPutChan + j;

                                _runUUT[iTimer].Led[slot].unitV = 0;
                                _runUUT[iTimer].Led[slot].unitA = 0;
                                _runUUT[iTimer].Led[slot].result = 0;
                                _runUUT[iTimer].Led[slot].vFailNum = 0;
                                _runUUT[iTimer].Led[slot].iFailNum = 0;
                                _runUUT[iTimer].Led[slot].failEnd = 0;
                                _runUUT[iTimer].Led[slot].failTime = "";
                                _runUUT[iTimer].Led[slot].failInfo = "";
                                _runUUT[iTimer].Led[slot].modelName = _runModel[iTimer].Base.Model;
                            }
                        }

                        _runUUT[iTimer].Dev.DoEL = AgingELType.清除负载;

                        updata_uutSpec_database(iTimer);

                        updata_outSpec_database(iTimer);

                        for (int iuutNo = 0; iuutNo < CGlobalPara.C_Board_MAX; iuutNo++)
                        {
                            uiUUT[iuutNo + iTimer * CGlobalPara.C_Board_MAX].SetUUT(_runUUT[iTimer], iuutNo);
                        }
                        //runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("初始化位置条码!"), udcRunLog.ELog.Action);
                        //updata_localBar_database(iTimer);

                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("初始产品规格条码!"), udcRunLog.ELog.Action);
                        updata_inirunPara_database(iTimer);

                        setplc_Temp_Spec(iTimer);                               //传送温度
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("初始化完成!"), udcRunLog.ELog.Action);

                        _runUUT[iTimer].Para.DoRun = AgingRunType.空闲;
                        _runUUT[iTimer].Para.DoONOFF = AgingONOFFType.OFF;
                        _runUUT[iTimer].Para.DoData = AgingDataType.空闲;
                       // _runUUT[iTimer].Dev.DoEL = AgingELType.清除负载;
                        uiRunStatus[iTimer].SetRunStatus(_runUUT[iTimer]);          //初始化界面
                        ClearTempChart(iTimer);
                        //uut_ini_report_excel(iTimer);
                      
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                _runUUT[iTimer].Para.ChooseModel = false;
            }
        }

        /// <summary>
        /// 自检电压
        /// </summary>
        /// <param name="iTimer"></param>
        private void ctrl_reStart_info(int iTimer)
        {
            try
            {
                if (_runUUT[iTimer].Para.ChooseModel == true )
                {
                    MessageBox.Show("请等待加载完毕后再自检", "启动老化", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_runUUT[iTimer].Dev.DoEL != AgingELType.空闲)
                {
                    MessageBox.Show("请等待加载完毕后再自检", "启动老化", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string er = string.Empty;
                _runUUT[iTimer].Para.RunInVolt = _runModel[iTimer].OnOff[0].Item[0].InPutV;

                if (MessageBox.Show(CLanguage.Lan("确定启动") + _runUUT[iTimer].Para.TimerName + CLanguage.Lan("的自检？当前输入电压为：") +
                    _runUUT[iTimer].Para.RunInVolt.ToString(), CLanguage.Lan("自检老化"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    setplc_Temp_Spec(iTimer);   
                    _runUUT[iTimer].Para.RunInVolt = _runModel[iTimer].OnOff[0].Item[0].InPutV;
                    _runUUT[iTimer].Dev.DoEL = AgingELType.设定负载;
                    _runUUT[iTimer].Para.DoRun = AgingRunType.自检;
                    _runUUT[iTimer].Para.DoONOFF = AgingONOFFType.ON;
                    _runUUT[iTimer].Para.DoData = AgingDataType.空闲;
             
                    _runUUT[iTimer].Para.SaveCtrlTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                }
                else
                {
                    _runUUT[iTimer].Para.DoRun = AgingRunType.空闲;
                }
                updata_runPara_database(iTimer);
                uiRunStatus[iTimer].SetRunStatus(_runUUT[iTimer]);      //初始化界面
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 扫描条码
        /// </summary>
        /// <param name="iTimer"></param>
        private void ctrl_scanBar_info(int iTimer)
        {
            try
            {
                string er = string.Empty;
                if (_runUUT[iTimer].Mes.DoMes == AgingMesType.空闲)
                {
                    _runUUT[iTimer].Para.DoRun = AgingRunType.扫条码;
                }
                //else 
                //{
                //    _runUUT[iTimer].Para.DoRun = AgingRunType.选机种;
                //}
                if (_runModel[iTimer] != null)
                {
                    _runUUT[iTimer].Para.barLength = _runModel[iTimer].Base.BarLength;
                }
                udcSerialNo uutbarInfo = new udcSerialNo(_runUUT[iTimer],_runModel [iTimer ].Para .ChanAdd );

                uutbarInfo.menuClick.OnEvent += new COnEvent<udcSerialNo.CSetMenuArgs>.OnEventHandler(OnBaSet);

                uutbarInfo.Show();

                _runUUT[iTimer].Mes.DoMes = AgingMesType.空闲;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 启动老化
        /// </summary>
        /// <param name="iTimer"></param>
        private void ctrl_Start_info(int iTimer)
        {
            try
            {
                
                string er = string.Empty;
                //    TranMes(iTimer, ref er);

                if (MessageBox.Show(CLanguage.Lan("确定启动") + _runUUT[iTimer].Para.TimerName + CLanguage.Lan("的老化？"), CLanguage.Lan("启动老化"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    _runUUT[iTimer].Para.UserName = CGlobalPara.logName;
                    if (_runUUT[iTimer].Para.MesFlag == 1 && CGlobalPara.SysPara.Mes.Connect)
                    {
                        string FailBar = string.Empty;
                        for (int islot = 0; islot < _runUUT[iTimer].Led.Count; islot++)
                        {

                            if (islot > 191) continue;
                            if (_runUUT[iTimer].Led[islot].iCH != 1) continue;

                            if (_runUUT[iTimer].Led[islot].serialNo == string.Empty) continue;

                            if (_runUUT[iTimer].Led[islot].barType != 1) continue;

                            if (_runUUT[iTimer].Led[islot].tranResult == 2) continue;

                            bool haveuut = false;
                            for (int ich = 0; ich < _runUUT[iTimer].Para.OutPutChan; ich++)
                            {
                                if (_runUUT[iTimer].Led[islot].result > 0)
                                {
                                    haveuut = true;
                                }
                            }

                            if (!haveuut) continue;

                            if (!CSajet_LiteonChangzhou.CheckIn(CGlobalPara.logName, _runUUT[iTimer].Para.MO_NO, _runUUT[iTimer].Led[islot].localBar, _runUUT[iTimer].Led[islot].serialNo, out string model, out er))
                            {
                                MessageBox.Show($"位置{_runUUT[iTimer].Led[islot].localBar}条码{_runUUT[iTimer].Led[islot].serialNo}验证异常=>{er}", "启动老化", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            else
                            {
                                if (model != _runUUT[iTimer].Para.ModelName)
                                {

                                    MessageBox.Show($"位置{_runUUT[iTimer].Led[islot].localBar}条码{_runUUT[iTimer].Led[islot].serialNo}验证异常=>获取机种{model}与选择机种{_runUUT[iTimer].Para.ModelName}不一致", "启动老化", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;

                                }
                            }

                            _runUUT[iTimer].Led[islot].tranResult = 2;

                        }

                        if (!CSajet_LiteonChangzhou.StartBI(_runUUT[iTimer].Para.UserName, _runUUT[iTimer].Para.MO_NO, out er))
                        {
                            MessageBox.Show($"MES开机绑定异常=>{er}", "启动老化", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            runLog.Log($"MES开机绑定完成", udcRunLog.ELog.OK);
                        }
                    }

                    if (_runUUT[iTimer].Dev.DoEL == AgingELType.设定负载 || _runUUT[iTimer].Para.DoONOFF == AgingONOFFType.ON)
                    {
                        MessageBox.Show("请等待自检完毕后再启动", "启动老化", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (_runUUT[iTimer].Para.BarFlag == 1)

                        if (!hand_chkbar_bar_info(iTimer, ref er))
                        {
                            MessageBox.Show(_runUUT[iTimer].Para.TimerName + CLanguage.Lan("条码对应异常[") + er + CLanguage.Lan("],请确认后再开始老化?"), CLanguage.Lan("条码检测"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                    _runUUT[iTimer].Para.DoRun = AgingRunType.运行;
                    _runUUT[iTimer].Para.DoONOFF = AgingONOFFType.ON;
                    _runUUT[iTimer].Para.RunTime = 0;
                    _runUUT[iTimer].Para.iniSpec = 1;
                    _runUUT[iTimer].Para.UserName = CIniFile.ReadFromIni("UseSet", "UserName", CGlobalPara.UserFile, "");
                    _runUUT[iTimer].Para.StartTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    _runUUT[iTimer].Para.EndTime = DateTime.Now.AddSeconds(_runUUT[iTimer].Para.BurnTime).ToString("yyyy/MM/dd HH:mm:ss");
                    _runUUT[iTimer].Para.SaveDataTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    _runUUT[iTimer].Para.SaveDataIndex = 0;
                    _runUUT[iTimer].Para.SaveCtrlTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    _runUUT[iTimer].Para.SavePath = string.Empty;
             

                    ClearTempChart(iTimer);
      
                    uut_save_report_Path(iTimer);

                }
                else
                {
                    _runUUT[iTimer].Dev.DoEL = AgingELType.空闲;
                    _runUUT[iTimer].Para.DoRun = AgingRunType.空闲;
                    _runUUT[iTimer].Para.DoONOFF = AgingONOFFType.OFF;
                    _runUUT[iTimer].Para.DoData = AgingDataType.空闲;
                    
                }
                updata_runPara_database(iTimer);
                uiRunStatus[iTimer].SetRunStatus(_runUUT[iTimer]);      //初始化界面
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 暂停老化
        /// </summary>
        /// <param name="iTimer"></param>
        private void ctrl_Pause_info(int iTimer, bool msgShow)
        {
            try
            {
                string er = string.Empty;
                if (msgShow)
                {
                    if (MessageBox.Show(CLanguage.Lan("确定暂停") + _runUUT[iTimer].Para.TimerName + CLanguage.Lan("的老化？"), CLanguage.Lan("暂停老化"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        _runUUT[iTimer].Para.DoRun = AgingRunType.暂停;
                        _runUUT[iTimer].Para.DoONOFF = AgingONOFFType.OFF;
                        _runUUT[iTimer].Para.DoData = AgingDataType.空闲;
                        updata_runPara_database(iTimer);

                        uiRunStatus[iTimer].SetRunStatus(_runUUT[iTimer]);      //初始化界面
                    }
                }
                else
                {
                    _runUUT[iTimer].Para.DoRun = AgingRunType.暂停;
                    _runUUT[iTimer].Para.DoONOFF = AgingONOFFType.OFF;
                    _runUUT[iTimer].Para.DoData = AgingDataType.空闲;
                    updata_runPara_database(iTimer);

                    uiRunStatus[iTimer].SetRunStatus(_runUUT[iTimer]);      //初始化界面

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 继续老化
        /// </summary>
        /// <param name="iTimer"></param>
        private void ctrl_continue_info(int iTimer, bool msgShow)
        {
            try
            {
                string er = string.Empty;
                if (msgShow)
                {
                    if (MessageBox.Show(CLanguage.Lan("确定继续") + _runUUT[iTimer].Para.TimerName + CLanguage.Lan("的老化？"), CLanguage.Lan("继续老化"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {

                        _runUUT[iTimer].Para.DoRun = AgingRunType.运行;
                        _runUUT[iTimer].Para.DoONOFF = AgingONOFFType.ON;
                        _runUUT[iTimer].Para.DoData = AgingDataType.空闲;

                        _runUUT[iTimer].Para.SaveCtrlTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                        _runUUT[iTimer].Para.iniSpec = 1;
                    

                        uiRunStatus[iTimer].SetRunStatus(_runUUT[iTimer]);      //初始化界面     
                        updata_runPara_database(iTimer);
                    }
                    else
                    {
                        if (MessageBox.Show(CLanguage.Lan("是否要停止") + _runUUT[iTimer].Para.TimerName + CLanguage.Lan("的老化？"), CLanguage.Lan("停止老化"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                        {
                            _runUUT[iTimer].Para.DoRun = AgingRunType.空闲;

                            _runUUT[iTimer].Para.DoONOFF = AgingONOFFType.OFF;

                            _runUUT[iTimer].Para.DoData = AgingDataType.空闲;

                            updata_runPara_database(iTimer);

                            uiRunStatus[iTimer].SetRunStatus(_runUUT[iTimer]);      //初始化界面

                        }
                    }
                }
                else
                {
                    _runUUT[iTimer].Para.DoRun = AgingRunType.运行;
                    _runUUT[iTimer].Para.DoONOFF = AgingONOFFType.ON;
                    _runUUT[iTimer].Para.DoData = AgingDataType.空闲;

                    _runUUT[iTimer].Para.SaveCtrlTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    _runUUT[iTimer].Para.iniSpec = 1;
                    updata_runPara_database(iTimer);
                    uiRunStatus[iTimer].SetRunStatus(_runUUT[iTimer]);      //初始化界面
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 停止老化
        /// </summary>
        /// <param name="iTimer"></param>
        private void ctrl_Stop_info(int iTimer)
        {
            try
            {
                string er = string.Empty;
                if (MessageBox.Show(CLanguage.Lan("确定停止") + _runUUT[iTimer].Para.TimerName + CLanguage.Lan("的老化？"), CLanguage.Lan("停止老化"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    _runUUT[iTimer].Para.DoRun = AgingRunType.空闲;
                    _runUUT[iTimer].Para.DoONOFF = AgingONOFFType.OFF;
                    _runUUT[iTimer].Para.DoData = AgingDataType.空闲;
                    updata_runPara_database(iTimer);

                    uiRunStatus[iTimer].SetRunStatus(_runUUT[iTimer]);      //初始化界面
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 显示运行信息
        /// </summary>
        /// <param name="uutNo"></param>
        private void ctrl_show_runInfo(int iTimer)
        {
            try
            {
                udcUUTInfo uutInfo = new udcUUTInfo(_runUUT[iTimer]);

                uutInfo.UIRefresh.OnEvent += new COnEvent<udcUUTInfo.CUIRefreshArgs>.OnEventHandler(onRefreshRunInfo);

                uutInfo.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 同步刷新子治具数据界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onRefreshRunInfo(object sender, udcUUTInfo.CUIRefreshArgs e)
        {
            e.runUUT = _runUUT[e.idNo];
        }

        /// <summary>
        /// 显示温度信息
        /// </summary>
        /// <param name="uutNo"></param>
        private void ctrl_show_tempInfo(int iTimer)
        {
            try
            {
                udcTemp uutTemp = new udcTemp(_runUUT[iTimer]);

                uutTemp.UIRefresh.OnEvent += new COnEvent<udcTemp.CUIRefreshArgs>.OnEventHandler(onRefreshTempInfo);

                uutTemp.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 同步刷新子治具数据界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onRefreshTempInfo(object sender, udcTemp.CUIRefreshArgs e)
        {
            e.runUUT = _runUUT[e.idNo];
        }

        private void OnMenuClick(object sender, udcUUT.CSetMenuArgs e)
        {
            int uutNo = 0;
            uutNo = e.idNo;

            switch (e.menuInfo)
            {
                case udcUUT.ESetMenu.设定误测:
                    hand_clear_fail_info(e.iTimer, e.idNo);
                    break;
                case udcUUT.ESetMenu.整区误测:
                    hand_clear_allfail_info(e.iTimer);
                    break;
                case udcUUT.ESetMenu.设定无机:
                    hand_clear_uut_info(e.iTimer, e.idNo);
                    break;
                case udcUUT.ESetMenu.复位报警:
                    hand_ref_Alarm_info(e.iTimer);
                    break;
                case udcUUT.ESetMenu .选择机种:
       
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 解除单个产品的异常
        /// </summary>
        /// <param name="uutNo"></param>
        private void hand_clear_fail_info(int iTimer, int uutNo)
        {
            try
            {
                string er = string.Empty;

                int slot = uutNo ;

                if (_runUUT[iTimer].Para.DoRun == AgingRunType.运行 || _runUUT[iTimer].Para.DoRun == AgingRunType.暂停)
                {
                    for (int j = 0; j < _runUUT[iTimer].Para.OutPutChan; j++)
                    {
                        if (_runUUT[iTimer].Led[slot + j].result != 0)
                        {
                            _runUUT[iTimer].Led[slot + j].iFailNum = 0;
                            _runUUT[iTimer].Led[slot + j].vFailNum = 0;
                            _runUUT[iTimer].Led[slot + j].result = 1;
                            _runUUT[iTimer].Led[slot + j].failEnd = 0;
                            _runUUT[iTimer].Led[slot + j].failTime = string.Empty;
                            _runUUT[iTimer].Led[slot + j].failInfo = string.Empty;

                            uut_upFail_Data(iTimer, slot + j);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 设定无机
        /// </summary>
        /// <param name="uutNo"></param>
        private void hand_clear_uut_info(int iTimer, int uutNo)
        {
            try
            {
                string er = string.Empty;

                int slot = uutNo;

                if (_runUUT[iTimer].Para.DoRun == AgingRunType.运行 || _runUUT[iTimer].Para.DoRun == AgingRunType.暂停)
                {
                    for (int j = 0; j < _runUUT[iTimer].Para.OutPutChan; j++)
                    {
                        if (_runUUT[iTimer].Led[slot + j].result != 0)
                        {
                            _runUUT[iTimer].Led[slot + j].iFailNum = 0;
                            _runUUT[iTimer].Led[slot + j].vFailNum = 0;
                            _runUUT[iTimer].Led[slot + j].result = 0;
                            _runUUT[iTimer].Led[slot + j].failEnd = 0;
                            _runUUT[iTimer].Led[slot + j].failTime = string.Empty;
                            _runUUT[iTimer].Led[slot + j].failInfo = string.Empty;

                            uut_upFail_Data(iTimer, slot + j);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 解除所有产品的异常的
        /// </summary>
        /// <param name="uutNo"></param>
        private void hand_clear_allfail_info(int iTimer)
        {
            try
            {
                string er = string.Empty;

                if (_runUUT[iTimer].Para.DoRun == AgingRunType.运行 || _runUUT[iTimer].Para.DoRun == AgingRunType.暂停)
                {
                    for (int slot = 0; slot < _runUUT[iTimer].Led.Count; slot++)
                    {
                        if (_runUUT[iTimer].Led[slot].result != 0)
                        {
                            if (_runUUT[iTimer].Led[slot].result != 1)
                            {
                                _runUUT[iTimer].Led[slot].iFailNum = 0;
                                _runUUT[iTimer].Led[slot].vFailNum = 0;
                                _runUUT[iTimer].Led[slot].iicFailNum = 0;
                                _runUUT[iTimer].Led[slot].result = 1;
                                _runUUT[iTimer].Led[slot].failEnd = 0;
                                _runUUT[iTimer].Led[slot].failTime = string.Empty;
                                _runUUT[iTimer].Led[slot].failInfo = string.Empty;
                                uut_upFail_Data(iTimer, slot);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        /// <summary>
        /// 解除所有产品的异常的
        /// </summary>
        /// <param name="uutNo"></param>
        private void hand_ref_Alarm_info(int iTimer)
        {
            try
            {
                string er = string.Empty;

                _runUUT[iTimer].Para.DoONOFF = AgingONOFFType.复位报警;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

     
        #endregion

        #region 条码扫描



        /// <summary>
        /// 条码扫描触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnBaSet(object sender, udcSerialNo.CSetMenuArgs e)
        {
            switch (e.menuInfo)
            {
                case udcSerialNo.ESetMenu.位置空闲:
                    hand_clear_bar_info(e.idNo, e.barNo);
                    break;
                case udcSerialNo.ESetMenu.位置条码OK:
                    hand_localok_bar_info(e.idNo, e.barNo, e.scanbar);
                    break;
                case udcSerialNo.ESetMenu.治具条码OK:
                    hand_fixok_bar_info(e.idNo, e.barNo, e.scanbar);
                    break;
                case udcSerialNo.ESetMenu.产品条码OK:
                    hand_barok_bar_info(e.idNo, e.barNo, e.scanbar);
                    break;
                case udcSerialNo.ESetMenu.扫码完成:
                    _runUUT[e.idNo].Para.UserName = CGlobalPara.logName;
                    _runUUT[e.idNo].Para.MO_NO = e.scanbar;
                    
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 修改测试报表保存时间
        /// </summary>
        /// <param name="iTimer"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private void update_barType_timer(int iTimer, int iSolt)
        {
            try
            {

                string er = string.Empty;

                CDBCOM db = new CDBCOM(EDBType.Access, "", CGlobalPara.SysDB);

                List<string> sqlCmdList = new List<string>();

                string sqlCmd = "Update RUN_DATA set fixBar='" + _runUUT[iTimer].Led[iSolt].fixBar +
                                "',serialNo='" + _runUUT[iTimer].Led[iSolt].serialNo + "',localBar='" + _runUUT[iTimer].Led[iSolt].localBar + "',barType=" +
                                _runUUT[iTimer].Led[iSolt].barType + ",tranResult=" + _runUUT[iTimer].Led[iSolt].tranResult +
                                " where  timerNo=" + (iTimer + 1) + " and uutNo=" + iSolt;
                sqlCmdList.Add(sqlCmd);

                if (!db.excuteSQLArray(sqlCmdList, out er))
                    runLog.Log(er, udcRunLog.ELog.NG);

            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
            finally
            {

            }
        }

        /// <summary>
        /// 修改位置吗
        /// </summary>
        /// <param name="iTimer"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private void update_LockBar_timer(int iTimer, int iSolt)
        {
            try
            {

                string er = string.Empty;

                CDBCOM db = new CDBCOM(EDBType.Access, "", CGlobalPara.SysDB);

                List<string> sqlCmdList = new List<string>();
                
                string sqlCmd = "Update RUN_DATA set fixBar='" + _runUUT[iTimer].Led[iSolt].fixBar +
                                "',serialNo='" + _runUUT[iTimer].Led[iSolt].serialNo + "',localBar='" + _runUUT[iTimer].Led[iSolt].localBar + "',barType=" +
                                _runUUT[iTimer].Led[iSolt].barType + ",tranResult=" + _runUUT[iTimer].Led[iSolt].tranResult +
                                " where  timerNo=" + (iTimer + 1) + " and uutNo=" + iSolt;
                sqlCmdList.Add(sqlCmd);

                if (!db.excuteSQLArray(sqlCmdList, out er))
                    runLog.Log(er, udcRunLog.ELog.NG);

            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
            finally
            {

            }
        }
        /// <summary>
        /// 删除条码
        /// </summary>
        /// <param name="uutNo"></param>
        private void hand_clear_bar_info(int iTimer, int barNo)
        {
            try
            {
                string er = string.Empty;

                for (int slot = 0; slot < _runUUT[iTimer].Led.Count; slot++)
                {
                    if (_runUUT[iTimer].Led[slot].barNo == barNo)
                    {
                        for (int j = 0; j < _runUUT[iTimer].Para.OutPutChan; j++)
                        {
                            _runUUT[iTimer].Led[slot + j].fixBar = string.Empty;
                            _runUUT[iTimer].Led[slot + j].serialNo = string.Empty;
                            _runUUT[iTimer].Led[slot + j].tranResult = 0;
                            _runUUT[iTimer].Led[slot + j].barType = 0;
                            update_barType_timer(iTimer, slot + j);
                        }

                        for (int iuutNo = 0; iuutNo < CGlobalPara.C_Board_MAX; iuutNo++)
                            uiUUT[iuutNo + iTimer * CGlobalPara.C_Board_MAX].SetUUT(_runUUT[iTimer], iuutNo);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 位置条码OK
        /// </summary>
        /// <param name="uutNo"></param>
        private void hand_localok_bar_info(int iTimer, int barNo,string barcode)
        {
            try
            {
                string er = string.Empty;

                for (int slot = 0; slot < _runUUT[iTimer].Led.Count; slot++)
                {
                    if (_runUUT[iTimer].Led[slot].barNo == barNo)
                    {
                        _runUUT[iTimer].Led[slot].localBar = barcode;
                    }
                }
                for (int iuutNo = 0; iuutNo < CGlobalPara.C_Board_MAX; iuutNo++)
                    uiUUT[iuutNo + iTimer * CGlobalPara.C_Board_MAX].SetUUT(_runUUT[iTimer], iuutNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 上传条码OK
        /// </summary>
        /// <param name="uutNo"></param>
        private void hand_tran_bar_info(int iTimer, int barNo)
        {
            try
            {

                string er = string.Empty;

                for (int slot = 0; slot < _runUUT[iTimer].Led.Count; slot++)
                {
                    if (_runUUT[iTimer].Led[slot].barNo == barNo)
                    {
                        _runUUT[iTimer].Led[slot].tranResult = 1;
                        update_barType_timer(iTimer, slot);
                    }
                }
                for (int iuutNo = 0; iuutNo < CGlobalPara.C_Board_MAX; iuutNo++)
                    uiUUT[iuutNo + iTimer * CGlobalPara.C_Board_MAX].SetUUT(_runUUT[iTimer], iuutNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 治具条码OK
        /// </summary>
        /// <param name="uutNo"></param>
        private void hand_fixok_bar_info(int iTimer, int barNo, string scanbar)
        {
            try
            {
                string er = string.Empty;

                for (int slot = 0; slot < _runUUT[iTimer].Led.Count; slot++)
                {
                    if (_runUUT[iTimer].Led[slot].barNo == barNo)
                    {
                        _runUUT[iTimer].Led[slot].fixBar = scanbar;
                        update_barType_timer(iTimer, slot);
                    }
                }
                for (int iuutNo = 0; iuutNo < CGlobalPara.C_Board_MAX; iuutNo++)
                    uiUUT[iuutNo + iTimer * CGlobalPara.C_Board_MAX].SetUUT(_runUUT[iTimer], iuutNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 产品条码OK
        /// </summary>
        /// <param name="uutNo"></param>
        private void hand_barok_bar_info(int iTimer, int barNo, string scanbar)
        {
            try
            {
                string er = string.Empty;

                for (int slot = 0; slot < _runUUT[iTimer].Led.Count; slot++)
                {
                    if (_runUUT[iTimer].Led[slot].barNo == barNo)
                    {
                        _runUUT[iTimer].Led[slot].serialNo = scanbar; ;
                        _runUUT[iTimer].Led[slot].barType = 1;
                        update_barType_timer(iTimer, slot);
                    }
                }
                for (int iuutNo = 0; iuutNo < CGlobalPara.C_Board_MAX; iuutNo++)
                    uiUUT[iuutNo + iTimer * CGlobalPara.C_Board_MAX].SetUUT(_runUUT[iTimer], iuutNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 验证条码
        /// </summary>
        /// <param name="uutNo"></param>
        private bool hand_chkbar_bar_info(int iTimer, ref string er)
        {
            try
            {
                int uutnum = 0;
                //int passnum = 0;
                int barNum = 0;
                //bool localOK = true;
                er = string.Empty;
                int chanNum = _runUUT[iTimer].Para.OutPutChan;
                chanNum = _runModel[iTimer].Para.ChanAdd;
                for (int uut = 0; uut < _runUUT[iTimer].Led.Count / chanNum; uut++)
                {
                    bool haveuut = false;
                    //bool uutpass = true;
                    bool havebar = false;

                    for (int ch = 0; ch < chanNum; ch++)
                    {
                        int slot = uut * chanNum + ch;
                        if (_runUUT[iTimer].Led[slot].iCH == 1)
                            if (_runUUT[iTimer].Led[slot].barType == 1)
                            {
                                havebar = true;
                            }

                        if (_runUUT[iTimer].Led[slot].result != 0)
                        {
                            haveuut = true;
                            //if (_runUUT[iTimer].Led[slot].result != 1)
                            //    uutpass = false;

                        }
                    }
                    if (haveuut || havebar)
                    {

                        //if ((haveuut && !havebar) || (havebar && !haveuut))
                        //    localOK = false;
                        if (haveuut)
                            uutnum += 1;
                        //if (uutpass)
                        //    passnum += 1;
                        if (havebar)
                            barNum += 1;
                    }
                }
                //if (!localOK)
                //{
                //    er = CLanguage.Lan("产品位置与条码位置不匹配");
                //    return false;
                //}
                if (uutnum == 0)
                {
                    er = CLanguage.Lan("产品数为0");
                    return false;
                }
                if (barNum == 0)
                {
                    er = CLanguage.Lan("条码为0");
                    return false;
                }
                if (uutnum != barNum)
                {
                    er = CLanguage.Lan("产品数与条码数不一致");
                    return false;
                }
                //if (passnum != barNum)
                //{
                //    er = CLanguage.Lan("良品数与条码数不一致");
                //    return false;
                //}
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        #endregion

        #region 界面UI刷新
        /// <summary>
        /// 刷新UI设置状态
        /// </summary>
        private void refreshUISetting()
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(refreshUISetting));
            else
            {
                if (CGlobalPara.SysPara.Mes.Connect)
                {
                    lblRunStatus.Text = CLanguage.Lan("基本信息");
                    lblRunStatus.ForeColor = Color.Blue;
                    updateSignalUI();
                }
                else
                {
                    lblRunStatus.Text = CLanguage.Lan("基本信息");
                    lblRunStatus.ForeColor = Color.Black;
                    updateSignalUI();
                }
            }
        }

        #endregion

        #region 启动与停止测试系统

        #region 打开MES

        /// <summary>
        /// 打开MES
        /// </summary>
        /// <param name="er"></param>
        /// <param name="connect">true=open,false=close</param>
        /// <returns></returns>
        private bool OpenMes(out string er, bool connect = true)
        {
            er = string.Empty;
            try
            {
                if (connect)
                {
                    if (!CSajet_LiteonChangzhou.Connect())
                        er = "连接MES失败!!";
                    else
                    {
                        bool loginOK = false;
                        for (int i = 0; i < 20; i++)
                        {
                            Thread.Sleep(500);
                            if (CSajet_LiteonChangzhou.LogIn(CGlobalPara.logName, CGlobalPara.logPassword, out er))
                            {
                                runLog.Log("登陆MES成功;账号:" + CGlobalPara.logName, udcRunLog.ELog.Action);
                                loginOK = true;
                                break;
                            }
                        }

                        if (!loginOK)
                        {
                            er = "登陆MES失败;账号:" + CGlobalPara.logName + ";Errcode:" + er;
                            return false;
                        }
                    }
                }
                else
                {
                    if (!CSajet_LiteonChangzhou.Close())
                    {
                        er = "MES断开失败!!";
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        #endregion
        /// <summary>
        /// 启动
        /// </summary>
        private bool OnRun()
        {
            try
            {
                InitialPara();
                if (CGlobalPara.SysPara.Mes.Connect)
                {
                    if (!OpenMes(out string er, true))
                    {
                        runLog.Log(er.ToString(), udcRunLog.ELog.Err);
                        return false;
                    }
                }


                if (!OpenDevice())
                {
                    CloseDevice();
                    return false;
                }

                if (!StartThread())
                {
                    StopThread();
                    CloseDevice();
                    return false;
                }
                _Start = true;
                return true;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }
        /// <summary>
        /// 停止
        /// </summary>
        private void OnStop()
        {
            try
            {
                _Start = false;

                StopThread();

                CloseDevice();

            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
        }

        /// <summary>
        /// 初始化测试状态
        /// </summary>
        private void InitialPara()
        {

            CGlobalPara.C_INI_SCAN = false;

            for (int idNo = 0; idNo < CGlobalPara.C_Timer_MAX; idNo++)
            {
                _chmr[idNo].status.doRun = ERun.空闲;
            }
        }

        /// <summary>
        /// 打开设备
        /// </summary>
        /// <returns></returns>
        private bool OpenDevice()
        {
            string er = string.Empty;
            try
            {
                bool ChkDev = true;

                //打开GJDA_1050_4模块
                if (!OpenGJDA_1050_4(ref er, true))
                {
                    runLog.Log(er.ToString(), udcRunLog.ELog.Err);
                    ChkDev = false;
                }

                //打开VOLT Mon模块
                if (!OpenGJVoltMon(ref er, true))
                {
                    runLog.Log(er.ToString(), udcRunLog.ELog.Err);
                    ChkDev = false;
                }

                //打开PLC模块
                if (!OpenPLC(ref er, true))
                {
                    runLog.Log(er.ToString(), udcRunLog.ELog.Err);
                    ChkDev = false;
                }

               
                ////打开条码枪串口
                //if (!openBar(CGlobalPara.SysPara.Dev.Bar_Com[0], CGlobalPara.SysPara.Dev.Bar_Buad[0], out er))
                //{
                //    runLog.Log(er.ToString(), udcRunLog.ELog.Err);
                //    ChkDev = false;
                //}

                return ChkDev;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        private void CloseDevice()
        {
            string er = string.Empty;

            try
            {
                if (CGlobalPara.SysPara.Mes.Connect)
                {
                    if (!OpenMes(out  er, false))
                    {
                        runLog.Log(er.ToString(), udcRunLog.ELog.Err);
                    }
                }

                if (!OpenGJDA_1050_4(ref er, false))
                    runLog.Log(er.ToString(), udcRunLog.ELog.Err);

                if(!OpenGJVoltMon(ref er, false))
                    runLog.Log(er.ToString(), udcRunLog.ELog.Err);

                if (!OpenPLC(ref er, false))
                    runLog.Log(er.ToString(), udcRunLog.ELog.Err);

                //if (!closeBar())
                //    runLog.Log("关闭条码枪串口失败!", udcRunLog.ELog.Err);

            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
        }

        /// <summary>
        /// 启动线程
        /// </summary>
        /// <returns></returns>
        private bool StartThread()
        {
            try
            {
                //GJDA_1050_4线程
                for (int idNo = 0; idNo < CGlobalPara.C_GJDA_1050_4_Num; idNo++)
                {
                    _threadGJDA_1050_4.Add(new Thread(OnGJDA_1050_4Start));
                    _threadGJDA_1050_4[idNo].IsBackground = true;
                    _threadGJDA_1050_4[idNo].Name = idNo.ToString("D2") + "GJDA_1050_4";
                    _threadGJDA_1050_4[idNo].Start();
                    _threadGJDA_1050_4Cancel.Add(new bool());
                    runLog.Log(CLanguage.Lan("DA模块GJDA_1050_4线程") + (idNo + 1).ToString() + CLanguage.Lan("启动"), udcRunLog.ELog.Action);
                }

                Set_VoltMon_Thread(true);

                //测试报表保存
                for (int idNo = 0; idNo < CGlobalPara.C_Report_Threed; idNo++)
                {

                    _threadReport.Add(new Thread(OnReportStart));
                    _threadReport[idNo].IsBackground = true;
                    _threadReport[idNo].Name = idNo.ToString("D2") + "数据更新模块";
                    _threadReport[idNo].Start();
                    _threadReportCancel.Add(new bool());
                    runLog.Log(CLanguage.Lan("数据更新线程") + (idNo + 1).ToString() + CLanguage.Lan("启动"), udcRunLog.ELog.Action);
                }

                //PLC线程
                for (int idNo = 0; idNo < CGlobalPara.C_PLCCom_Max; idNo++)
                {

                    _PLC_LOCK.Add(new ReaderWriterLock());
                    _threadPLCCancel.Add(new bool());
                    _PLC_ErrCode.Add(new int());
                    _conToPLCAgain.Add(new int());

                    _threadPLC.Add(new Thread(OnPLCStart));
                    _threadPLC[idNo].IsBackground = true;
                    _threadPLC[idNo].Name = idNo.ToString("D2") + "PLC";
                    _threadPLC[idNo].Start();
                    _threadPLCCancel.Add(new bool());

                    runLog.Log(CLanguage.Lan("PLC线程") + (idNo + 1).ToString() + CLanguage.Lan("启动"), udcRunLog.ELog.Action);
                }

                //启动主线程
                if (!MainWorker.IsBusy)
                    MainWorker.RunWorkerAsync();

                return true;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }
        /// <summary>
        /// 停止线程
        /// </summary>
        private void StopThread()
        {
            try
            {

                //销毁主线程                
                if (MainWorker.IsBusy)
                    MainWorker.CancelAsync();

                while (MainWorker.IsBusy)
                {
                    Application.DoEvents();
                }

                Set_VoltMon_Thread(false );
                //GJDA_1050_4线程关闭
                for (int i = 0; i < CGlobalPara.C_GJDA_1050_4_Num; i++)
                {
                    if (_threadGJDA_1050_4[i] != null)
                    {
                        _threadGJDA_1050_4Cancel[i] = true;
                        while (_threadGJDA_1050_4Cancel[i])
                            Application.DoEvents();
                        _threadGJDA_1050_4[i] = null;
                    }
                }
                _threadGJDA_1050_4.Clear();
                _threadGJDA_1050_4Cancel.Clear();

                ////XT_LED显示屏
                //for (int i = 0; i < CGlobalPara.C_XT_LED_Num; i++)
                //{
                //    if (_threadXT_LED[i] != null)
                //    {
                //        _threadXT_LEDCancel[i] = true;
                //        while (_threadXT_LEDCancel[i])
                //            Application.DoEvents();
                //        _threadXT_LED[i] = null;
                //    }
                //}
                //_threadXT_LED.Clear();
                //_threadXT_LEDCancel.Clear();

                //报表保存线程
                for (int i = 0; i < CGlobalPara.C_Report_Threed; i++)
                {

                    if (_threadReport[i] != null)
                    {
                        _threadReportCancel[i] = true;
                        while (_threadReportCancel[i])
                            Application.DoEvents();
                        _threadReport[i] = null;
                    }
                }
                _threadReport.Clear();
                _threadReportCancel.Clear();

                //PLC线程关闭
                for (int i = 0; i < CGlobalPara.C_PLCCom_Max; i++)
                {
                    if (_threadPLC[i] != null)
                    {
                        _threadPLCCancel[i] = true;
                        while (_threadPLCCancel[i])
                            Application.DoEvents();
                        _threadPLC[i] = null;

                    }
                }
                _threadPLC.Clear();
                _PLC_LOCK.Clear();
                _threadPLCCancel.Clear();
                _PLC_ErrCode.Clear();
                _conToPLCAgain.Clear();
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
        }
        #endregion

        #region GJVoltMon线程

        #region 线程控件
        /// <summary>
        /// GJFCMB模块
        /// </summary>
        private List<CMON32> _dev_GJ_VoltMon = new List<CMON32>();

        /// <summary>
        /// GJFCMB线程
        /// </summary>
        private List<Thread> _thread_GJ_VoltMon = new List<Thread>();

        /// <summary>
        /// GJFCMB线程状态
        /// </summary>
        private volatile List<bool> _thread_GJ_VoltMon_Cancel = new List<bool>();

        /// <summary>
        /// GJFCMB使用线程起始
        /// </summary>
        public volatile List<int> C_GJ_VoltMon_Timer_Start = new List<int>();

        /// <summary>
        /// GJFCMB使用线程结束
        /// </summary>
        public volatile List<int> C_GJ_VoltMon_Timer_End = new List<int>();


        /// <summary>
        /// GJFCMB使用地址起始
        /// </summary>
        public volatile List<int> C_GJ_VoltMon_Adrs_Start = new List<int>();

        /// <summary>
        /// GJFCMB使用地址结束
        /// </summary>
        public volatile List<int> C_GJ_VoltMon_Adrs_End = new List<int>();

        #endregion

        #region 线程串口控制
        /// <summary>
        /// 打开GJFCMB模块串口
        /// </summary>
        /// <param name="er"></param>
        /// <param name="connect">true=open,false=close</param>
        /// <returns></returns>
        private bool OpenGJVoltMon(ref string er, bool connect = true)
        {
            try
            {
                er = string.Empty;

                if (connect)
                {
                    _dev_GJ_VoltMon.Clear();

                    for (int iDevNo = 0; iDevNo < 2; iDevNo++)
                    {

                        _dev_GJ_VoltMon.Add(new CMON32(iDevNo, "CMON32" + iDevNo.ToString()));

                        if (_dev_GJ_VoltMon[iDevNo] != null)
                        {
                            if (!_dev_GJ_VoltMon[iDevNo].Open(CGlobalPara.SysPara.Dev.GJ_Mon32Com[iDevNo], out er,
                                CGlobalPara.SysPara.Dev.GJ_Mon32Buad[iDevNo]))
                            {
                                _dev_GJ_VoltMon[iDevNo] = null;
                                er = CLanguage.Lan("打开") + CGlobalPara.SysPara.Dev.GJ_Mon32Com[iDevNo] + CLanguage.Lan("]失败:") + er;
                                return false;
                            }
                            er = CLanguage.Lan("成功打开[") + CGlobalPara.SysPara.Dev.GJ_Mon32Com[iDevNo] + "]";
                        }
                    }
                }
                else
                {
                    for (int iFCMBNo = 0; iFCMBNo < _dev_GJ_VoltMon.Count; iFCMBNo++)
                    {
                        if (_dev_GJ_VoltMon[iFCMBNo] != null)
                        {
                            _dev_GJ_VoltMon[iFCMBNo].Close();
                            _dev_GJ_VoltMon[iFCMBNo] = null;
                            er = CLanguage.Lan("关闭[") + CGlobalPara.SysPara.Dev.GJ_Mon32Com[iFCMBNo] + "]";
                        }
                    }

                    _dev_GJ_VoltMon.Clear();
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }

        #endregion

        #region 线程控制

        /// <summary>
        /// 采集板线程控制
        /// </summary>
        /// <param name="threadType">线程状态 true=启动线程</param>
        /// <returns></returns>
        private bool Set_VoltMon_Thread(bool threadType = true)
        {
            try
            {
                if (threadType)
                {
                    _thread_GJ_VoltMon.Clear();
                    _thread_GJ_VoltMon_Cancel.Clear();

                    C_GJ_VoltMon_Timer_Start.Clear();
                    C_GJ_VoltMon_Timer_End.Clear();
                    C_GJ_VoltMon_Adrs_Start.Clear();
                    C_GJ_VoltMon_Adrs_End.Clear();

                    C_GJ_VoltMon_Timer_Start.Add(0);
                    C_GJ_VoltMon_Timer_Start.Add(1);
                    C_GJ_VoltMon_Timer_End.Add(1);
                    C_GJ_VoltMon_Timer_End.Add(2);

                    C_GJ_VoltMon_Adrs_Start.Add(1);
                    C_GJ_VoltMon_Adrs_Start.Add(1);
                    C_GJ_VoltMon_Adrs_End.Add(6);
                    C_GJ_VoltMon_Adrs_End.Add(6);

                    for (int idNo = 0; idNo < _dev_GJ_VoltMon.Count ; idNo++)
                    {
                        ///添加线程状态
                        _thread_GJ_VoltMon_Cancel.Add(new bool());
                        /// LED板线程启动
                        _thread_GJ_VoltMon.Add(new Thread(On_GJ_FCMB_Start));
                        _thread_GJ_VoltMon[idNo].IsBackground = true;
                        _thread_GJ_VoltMon[idNo].Name = idNo.ToString("D2") + "电压板线程启动!";
                        _thread_GJ_VoltMon[idNo].Start();
                        runLog.Log(CLanguage.Lan("电压板线程") + (idNo + 1).ToString() + CLanguage.Lan("启动"), udcRunLog.ELog.Action);
                    }
                }
                else
                {
                    for (int i = 0; i < _dev_GJ_VoltMon.Count; i++)
                        if (_thread_GJ_VoltMon[i] != null)
                        {
                            _thread_GJ_VoltMon_Cancel[i] = true;
                            while (_thread_GJ_VoltMon_Cancel[i])
                                Application.DoEvents();
                            _thread_GJ_VoltMon[i] = null;
                        }
                    _thread_GJ_VoltMon.Clear();
                    _thread_GJ_VoltMon_Cancel.Clear();
                }
                return true;
            }
            catch (Exception ex)
            {
                runLog.Log("电压板线程控制失败__" + ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }

        /// <summary>
        /// GJFCMB刷新线程
        /// </summary>
        private void On_GJ_FCMB_Start()
        {
            int iThread = System.Convert.ToInt16(Thread.CurrentThread.Name.Substring(0, 2));       //获取当前线程的ID
            try
            {
                while (true)
                {
                    try
                    {
                        string er = string.Empty;

                        if (_thread_GJ_VoltMon_Cancel[iThread])
                            return;

                        //每个线程6个时序
                        for (int iTimer = C_GJ_VoltMon_Timer_Start[iThread]; iTimer < C_GJ_VoltMon_Timer_End[iThread]; iTimer++)
                        {
                            if (_thread_GJ_VoltMon_Cancel[iThread])
                                continue;

                            if (_runUUT[iTimer].Para.DoRun == AgingRunType.运行 || _runUUT[iTimer].Para.DoRun == AgingRunType.自检)
                            {
                                if (_runUUT[iTimer].Para.RunInVolt > 0)
                                {
                                    Scan_GJ_VoltMon(iThread);
                                    Thread.Sleep(2000);
                                    Get_GJ_VoltMon(iThread, iTimer);
                                }
                            }
                        }
                        Thread.Sleep(1000);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    Thread.Sleep(2000);
                }
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
            finally
            {
                _thread_GJ_VoltMon_Cancel[iThread] = false;
                runLog.Log(CLanguage.Lan("GJFCMB模块线程") + iThread.ToString() + CLanguage.Lan("关闭"), udcRunLog.ELog.Content);
            }
        }

        #endregion


        /// <summary>
        ///  获取单个时序的采集板数据
        /// </summary>
        /// <param name="iTimer"></param>
        private bool Scan_GJ_VoltMon(int iThread)
        {
            try
            {

                string er = string.Empty;
             
                _dev_GJ_VoltMon[iThread].SetScanAll();

                return true;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }
        /// <summary>
        ///  获取单个时序的采集板数据
        /// </summary>
        /// <param name="iTimer"></param>
        private bool Get_GJ_VoltMon(int iThread, int iTimer)
        {
            try
            {

                string er = string.Empty;
                for (int iAdrs = C_GJ_VoltMon_Adrs_Start[iThread]; iAdrs <= C_GJ_VoltMon_Adrs_End[iThread]; iAdrs++)
                {

                    List<double> rData = new List<double>();

                    bool readOk = false;
                    int onoff = 0;
                    for (int j = 0; j < 5; j++)        //刷新测试数据
                    {
                        if (_dev_GJ_VoltMon[iThread].ReadVolt(iAdrs, out rData, out onoff,out er,ESynON.异步,ERunMode.普通老化房模式))
                        {
                            readOk = true;
                            break;
                        }
                        else
                            Thread.Sleep(300);
                    }
                    if (readOk)
                    {
                        for (int iCH = 0; iCH < rData.Count; iCH++)
                        {
                            for (int iLed = 0; iLed < _runUUT[iTimer].Led.Count; iLed++)
                            {
                                if (_runUUT[iTimer].Led[iLed].monCom == iThread + 1 &&
                                    _runUUT[iTimer].Led[iLed].monAdrs == iAdrs &&
                                    _runUUT[iTimer].Led[iLed].monCH == iCH + 1)
                                {

                                    _runUUT[iTimer].Led[iLed].dataunitV = Math.Abs(rData[iCH]);

                                    break;
                                }

                            }
                        }
                    }
                    else
                    {
                        for (int iCH = 0; iCH < rData.Count; iCH++)
                        {

                            for (int iLed = 0; iLed < _runUUT[iTimer].Led.Count; iLed++)
                            {
                                if (_runUUT[iTimer].Led[iLed].monCom == iThread &&
                                    _runUUT[iTimer].Led[iLed].monAdrs == iAdrs &&
                                    _runUUT[iTimer].Led[iLed].monCH == iCH + 1)
                                {

                                    _runUUT[iTimer].Led[iLed].dataunitV = Math.Abs(rData[iCH]) + CGlobalPara.SysPara.Reg.offsetVolt;
                                    break;
                                }
                            }
                        }

                        runLog.Log(_runUUT[iTimer].Para.TimerName + "电压板板地址:" + iAdrs.ToString() + "采集电压失败", udcRunLog.ELog.Err);
                        return false;
                    }                  

                }
                return true;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }

        #endregion

        #region GJDA_1050_4线程

        /// <summary>
        /// GJDA_320_8模块
        /// </summary>
        private List<CDA_1050_4> _devGJDA_1050_4 = new List<CDA_1050_4>();
 
        /// <summary>
        /// GJDA_1050_4线程
        /// </summary>
        private List<Thread> _threadGJDA_1050_4 = new List<Thread>();

        /// <summary>
        /// GJDA_1050_4线程状态
        /// </summary>
        private volatile List<bool> _threadGJDA_1050_4Cancel = new List<bool>();

        /// <summary>
        /// 打开GJDA_1050_4模块串口
        /// </summary>
        /// <param name="er"></param>
        /// <param name="connect">true=open,false=close</param>
        /// <returns></returns>
        private bool OpenGJDA_1050_4(ref string er, bool connect = true)
        {
            try
            {
                er = string.Empty;

                if (connect)
                {
                    for (int iELNo = 0; iELNo < 2; iELNo++)
                    {
                        _devGJDA_1050_4.Add(new CDA_1050_4(iELNo, CGlobalPara.SysPara.Dev.GJ_1050_4Com[iELNo]));
                        if (_devGJDA_1050_4[iELNo] != null)
                        {
                            if (!_devGJDA_1050_4[iELNo].Open(CGlobalPara.SysPara.Dev.GJ_1050_4Com[iELNo], out  er, CGlobalPara.SysPara.Dev.GJ_1050_4Buad[iELNo]))
                            {
                                _devGJDA_1050_4[iELNo] = null;
                                _devGJDA_1050_4.Clear();
                                er = CLanguage.Lan("打开") + CGlobalPara.SysPara.Dev.GJ_1050_4Com[iELNo] + CLanguage.Lan("]失败:") + er;
                                return false;
                            }
                            er = CLanguage.Lan("成功打开[") + CGlobalPara.SysPara.Dev.GJ_1050_4Com[iELNo] + "]";
                        }                   
                    }
                }
                else
                {
                    for (int iELNo = 0; iELNo < 2; iELNo++)
                    {
                        if (_devGJDA_1050_4[iELNo] != null)
                        {
                            _devGJDA_1050_4[iELNo].Close();
                            _devGJDA_1050_4[iELNo] = null;
                            er = CLanguage.Lan("关闭[") + CGlobalPara.SysPara.Dev.GJ_1050_4Com[iELNo] + "]";
                        }
                    }
                    _devGJDA_1050_4.Clear();              
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }

        /// <summary>
        /// GJDA_1050_4刷新线程
        /// </summary>
        private void OnGJDA_1050_4Start()
        {
            int iThread = System.Convert.ToInt16(Thread.CurrentThread.Name.Substring(0, 2));       //获取当前线程的ID
            try
            {
                while (true)
                {
                    try
                    {
                        string er = string.Empty;

                        if (_threadGJDA_1050_4Cancel[iThread])
                            return;

                        //每个线程2个时序的负载设定
                        int iTimer = iThread;

                        if (_runUUT[iTimer].Dev.DoEL == AgingELType.清除负载)        //清除负载
                        {
                            _runUUT[iTimer].Para.SaveCtrlTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                            SetGJDA_1050_4Load(iThread,iTimer);
                            if (_runUUT[iTimer].Dev.DoEL == AgingELType.清除负载)
                                _runUUT[iTimer].Dev.DoEL = AgingELType.空闲;
                            runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CGlobalPara.SysPara.Dev.GJ_1050_4Com[iThread] + "GJDA_1050_4清除负载完成", udcRunLog.ELog.Action);
                            continue;
                        }

                        if (_runUUT[iTimer].Dev.DoEL == AgingELType.设定负载)        //设定负载
                        {
                            _runUUT[iTimer].Para.SaveCtrlTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                            SetGJDA_1050_4Load(iThread, iTimer);
                            if (_runUUT[iTimer].Dev.DoEL == AgingELType.设定负载)
                            {
                                _runUUT[iTimer].Para.SaveCtrlTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                                _runUUT[iTimer].Dev.DoEL = AgingELType.空闲;
                            }
                            runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CGlobalPara.SysPara.Dev.GJ_1050_4Com[iThread] + "GJDA_1050_4设定负载完成", udcRunLog.ELog.Action);

                            continue;
                        }
                        if (_runUUT[iTimer].Para.DoRun == AgingRunType.运行 || _runUUT[iTimer].Para.DoRun == AgingRunType.自检)
                        {
                            if (_runUUT[iTimer].Para.RunInVolt == 0 || _runUUT[iTimer].Para.SaveCtrlTime == "")
                                _runUUT[iTimer].Para.SaveCtrlTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                            if (_runUUT[iTimer].Para.RunInVolt > 0 || _runUUT[iTimer].Para.DoRun == AgingRunType.自检)       //采集数据
                            {
                                if (CTimer.DateDiff(_runUUT[iTimer].Para.SaveCtrlTime) > 10 || _runUUT[iTimer].Para.DoRun == AgingRunType.自检)
                                {
                                    if (_runUUT[iTimer].Dev.DoEL == AgingELType.空闲)
                                        for (int i = 0; i < 3; i++)
                                            if (GetGJDA_1050_4Load(iThread, iTimer))
                                                break;
                                }
                            }
                        }

                        Thread.Sleep(1000);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
            finally
            {
                _threadGJDA_1050_4Cancel[iThread] = false;
                runLog.Log(CLanguage.Lan("关闭GJDA_1050_4模块线程关闭"), udcRunLog.ELog.Content);
            }
        }

        /// <summary>
        ///  获取单个时序的负载数据
        /// </summary>
        /// <param name="iTimer"></param>
        private bool GetGJDA_1050_4Load( int iThread,int iTimer)
        {
            try
            {
                string er = string.Empty;


                for (int i = 0; i < CGlobalPara.C_GJDA_1050_4_MAX[iTimer]; i++)//刷新测试数据
                {
                    int iAdrs = i + 1;

                    bool readOk = false;

                    CData rData = new CData();

                    for (int j = 0; j < 3; j++)        //刷新测试数据
                    {
                        CTimer.WaitMs(20);
                        if (_devGJDA_1050_4[iThread].ReadLoadValue(iAdrs, ref rData, out er))
                        {
                            readOk = true;
                            Thread.Sleep(50);
                            break;
                        }
                        else
                            Thread.Sleep(50);
                    }

                    if (readOk)
                    {
                        for (int iCH = 0; iCH < 4; iCH++)
                        {
                            for (int iLed = 0; iLed < _runUUT[iTimer].Led.Count; iLed++)
                            {
                                if (_runUUT[iTimer].Led[iLed].elCom == iThread + 1 && _runUUT[iTimer].Led[iLed].elAdrs == iAdrs && _runUUT[iTimer].Led[iLed].elCH == iCH + 1)
                                {
                                    _runUUT[iTimer].Led[iLed].dataunitA = Math.Abs(rData.chan[iCH].current + rData.chan[iCH + 1].current + rData.chan[iCH + 2].current + rData.chan[iCH + 3].current);
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int iCH = 0; iCH < 4; iCH++)
                        {
                            for (int iLed = 0; iLed < _runUUT[iTimer].Led.Count; iLed++)
                            {
                                if (_runUUT[iTimer].Led[iLed].elCom == iThread + 1 && _runUUT[iTimer].Led[iLed].elAdrs == iAdrs && _runUUT[iTimer].Led[iLed].elCH == iCH + 1)
                                {
                                    _runUUT[iTimer].Led[iLed].dataunitA = 0;
                                }
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }

        /// <summary>
        ///  设定单个区的负载电流
        /// </summary>
        /// <param name="iTimer"></param>
        private bool SetGJDA_1050_4Load(int iThread,int iTimer)
        {
            try
            {
                string er = string.Empty;

                bool AdrsOK = true;


                for (int i = 0; i < CGlobalPara.C_GJDA_1050_4_MAX[iTimer]; i++)
                {
                    int iAdrs = i + 1;

                    bool setOK = false;
                    for (int _count = 0; _count < 3; _count++)
                    {
                        setOK = false;
                        List<CLOAD> loadList = new List<CLOAD>();

                        for (int iCH = 0; iCH < 4; iCH++)
                        {
                            CLOAD wPara = new CLOAD();

                            int iNo = 0;

                            wPara.Mode = (EMODE)_runUUT[iTimer].Led[iNo].IMode;
                            wPara.Von = _runUUT[iTimer].Led[iNo].Von;

                            if (_runUUT[iTimer].Dev.DoEL != AgingELType.清除负载)
                            {
                                switch (wPara.Mode)
                                {
                                    case EMODE.CC_Slow:
                                        wPara.load = _runUUT[iTimer].Led[iNo].ISet / 4 + _count * 0.01;
                                        wPara.mark = _runUUT[iTimer].Led[iNo].AddMode;
                                        break;
                                    case EMODE.CV:
                                        wPara.load = _runUUT[iTimer].Led[iNo].ISet + _count * 0.01;
                                        wPara.mark = _runUUT[iTimer].Led[iNo].AddMode;
                                        break;
                                    case EMODE.CP:
                                        wPara.load = _runUUT[iTimer].Led[iNo].ISet + _count * 0.01;
                                        wPara.mark = _runUUT[iTimer].Led[iNo].AddMode;
                                        break;
                                    case EMODE.CR:
                                        wPara.load = _runUUT[iTimer].Led[iNo].ISet + _count * 0.01;
                                        wPara.mark = _runUUT[iTimer].Led[iNo].AddMode;
                                        break;
                                    case EMODE.CC_Fast:
                                        wPara.load = _runUUT[iTimer].Led[iNo].ISet / 4 + _count * 0.01;
                                        wPara.mark = _runUUT[iTimer].Led[iNo].AddMode;
                                        break;
                                    default:
                                        wPara.load = _runUUT[iTimer].Led[iNo].ISet + _count * 0.01;
                                        wPara.mark = _runUUT[iTimer].Led[iNo].AddMode;
                                        break;
                                }
                            }
                            else
                            {
                                wPara.Mode = (EMODE)0;
                                wPara.load = 0.5 + _count * 0.01;
                                wPara.Von = _runUUT[iTimer].Led[iNo].vMax + 20 + _count * 0.01;
                                wPara.mark = 10;
                            }

                            loadList.Add(wPara);
                        }

                        if (_devGJDA_1050_4[iThread].SetLoadValue(iAdrs, loadList, false, out er))
                        {
                            setOK = true;
                            runLog.Log(CGlobalPara.C_GJDA_1050_4_NAME[iThread] + CLanguage.Lan("GJDA_1050_4地址:") + (iAdrs).ToString() + CLanguage.Lan("设定负载成功"), udcRunLog.ELog.OK);
                            Thread.Sleep(50);
                            break;
                        }
                        else
                        {
                            Thread.Sleep(300);

                            if (_devGJDA_1050_4[iTimer].SetLoadValue(iAdrs, loadList, true, out er))
                            {
                                setOK = true;
                                runLog.Log(CGlobalPara.C_GJDA_1050_4_NAME[iThread] + CLanguage.Lan("GJDA_1050_4地址:") + (iAdrs).ToString() + CLanguage.Lan("设定负载成功"), udcRunLog.ELog.OK);
                                Thread.Sleep(50);
                                break;
                            }

                        }
                    }

                    if (setOK != true)
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("GJDA_1050_4地址:") + (iAdrs).ToString() + CLanguage.Lan("设定负载失败__") + er, udcRunLog.ELog.Err);
                        AdrsOK = false;
                    }

                }

                if (AdrsOK)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }

      
        #endregion

        #region 显通LED表头线程

        /// <summary>
        /// XT_LED模块
        /// </summary>
        private List<C_XianTong_Led> _devXT_LED = new List<C_XianTong_Led>();

        /// <summary>
        /// XT_LED线程
        /// </summary>
        private List<Thread> _threadXT_LED = new List<Thread>();

        /// <summary>
        /// XT_LED线程状态
        /// </summary>
        private volatile List<bool> _threadXT_LEDCancel = new List<bool>();

        /// <summary>
        /// 打开XT_LED模块串口
        /// </summary>
        /// <param name="er"></param>
        /// <param name="connect">true=open,false=close</param>
        /// <returns></returns>
        private bool OpenXT_LED(ref string er, bool connect = true)
        {
            try
            {
                er = string.Empty;

                if (connect)
                {
                    for (int iLedNo = 0; iLedNo < CGlobalPara.C_XT_LED_Num; iLedNo++)
                    {

                        _devXT_LED.Add(new C_XianTong_Led());
                        if (_devXT_LED[iLedNo] != null)
                        {
                            if (!_devXT_LED[iLedNo].open(CGlobalPara.SysPara.Dev.XT_Led_Com[iLedNo], CGlobalPara.SysPara.Dev.XT_Led_Buad[iLedNo], out  er))
                            {
                                _devXT_LED[iLedNo] = null;
                                _devXT_LED.Clear();
                                er = CLanguage.Lan("打开") + CGlobalPara.SysPara.Dev.XT_Led_Com[iLedNo] + CLanguage.Lan("]失败:") + er;
                                return false;
                            }
                            er = CLanguage.Lan("成功打开[") + CGlobalPara.SysPara.Dev.XT_Led_Com[iLedNo] + "]";
                        }
                    }
                }
                else
                {
                    for (int iLedNo = 0; iLedNo < CGlobalPara.C_XT_LED_Num; iLedNo++)
                    {
                        if (_devXT_LED[iLedNo] != null)
                        {
                            _devXT_LED[iLedNo].close();
                            _devXT_LED[iLedNo] = null;
                            er = CLanguage.Lan("关闭[") + CGlobalPara.SysPara.Dev.XT_Led_Com[iLedNo] + "]";
                        }
                    }

                    _devXT_LED.Clear();
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }

        /// <summary>
        /// XT_LED刷新线程
        /// </summary>
        private void OnXT_LEDStart()
        {
            int threadNo = System.Convert.ToInt16(Thread.CurrentThread.Name.Substring(0, 2));       //获取当前线程的ID
            try
            {
                while (true)
                {
                    try
                    {
                        string er = string.Empty;

                        if (_threadXT_LEDCancel[threadNo])
                            return;

                        for (int iTimer = 0; iTimer < CGlobalPara.C_Timer_MAX; iTimer++)
                        {

                            if (_runUUT[iTimer].Para.DoRun == AgingRunType.运行 || _runUUT[iTimer].Para.DoRun == AgingRunType.自检)
                            {
                                TimeSpan ts = new TimeSpan(0, 0, _runUUT[iTimer].Para.RunTime);
                                int hour = ts.Days * 24 + ts.Hours;
                                string runTime = string.Empty;
                                if (hour < 99)
                                    runTime = (ts.Days * 24 + ts.Hours).ToString("D2") + ts.Minutes.ToString("D2");
                                else
                                    runTime = "99" + ts.Minutes.ToString("D2");

                                if (!_devXT_LED[threadNo].WTime(iTimer + 1, Convert.ToInt16(runTime), out er))
                                    runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + "写入表头时间失败", udcRunLog.ELog.Err);


                            }
                        }
                        CTimer.WaitMs(5000);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
            finally
            {
                _threadXT_LEDCancel[threadNo] = false;
                runLog.Log(CLanguage.Lan("显通LED表头线程关闭"), udcRunLog.ELog.Content);
            }
        }


        #endregion

        #region 汇川PLC线程

        /// <summary>
        /// 汇川PLC设备（串口通信）
        /// </summary>
        private List<CInovance_COM> _devPLC = new List<CInovance_COM>();

        /// <summary>
        /// PLC线程
        /// </summary>
        private List<Thread> _threadPLC = new List<Thread>();

        /// <summary>
        /// PLC线程锁
        /// </summary>
        private List<ReaderWriterLock> _PLC_LOCK = new List<ReaderWriterLock>();

        /// <summary>
        /// PLC线程状态
        /// </summary>
        private volatile List<bool> _threadPLCCancel = new List<bool>();

        /// <summary>
        /// PLC错误代码
        /// </summary>
        private List<int> _PLC_ErrCode = new List<int>();

        /// <summary>
        /// PLC重连接次数
        /// </summary>
        private List<int> _conToPLCAgain = new List<int>();

        /// <summary>
        /// 初始化PLC
        /// </summary>
        private void InitialPLC()
        {

            for (int i = 0; i < CGlobalPara.C_PLCCom_Max; i++)
            {

                _PLC_LOCK.Add(new ReaderWriterLock());
                _threadPLCCancel.Add(new bool());

                _PLC_ErrCode.Add(new int());
                _conToPLCAgain.Add(new int());

                InitialPLCREGAdrs();
            }

        }
        /// <summary>
        /// PLC结果
        /// </summary>
        private enum EPLC_RESULT
        {
            空闲,
            结果OK,
            结果NG,
            直接过站
        }

        /// <summary>
        /// PLC 输入信号和数据读取
        /// </summary>
        private enum EPLCInput
        {
            
            启动信号,
            KM1ONOFF信号,
            KM2ONOFF信号,
            KM3ONOFF信号,
            KM4ONOFF信号,



            负载温度1,
            温度点1,
            温度点2,
            温度点3,
            温度点4,
            温度点5,
            温度点6,


            平均温度,

            时间小时,
            时间分钟,
            时间秒,

           

            KM1输入电压检测信号110V,
            KM1输入电压检测信号220V,
            KM2输入电压检测信号110V,
            KM2输入电压检测信号220V,
            KM3输入电压检测信号110V,
            KM3输入电压检测信号220V,
            KM4输入电压检测信号110V,
            KM4输入电压检测信号220V,

            故障1_循环风机异常信号,
            故障2_辅助电热异常信号,
            故障3_产品区超温断电,
            故障4_EGO异常,
            故障5_温度模块异常,
            故障6_产品区烟感检测信号触发,
            故障7_负载区烟感检测信号触发,
            故障8_控制柜烟感检测信触发,
            故障9_单点超温异常,
            故障10_风压开关1信号异常报警,
            故障11_风压开关2信号异常报警,
            故障12_老化中开门报警,
        }

        /// <summary>
        /// PLC 输出信号设定与参数设定
        /// </summary>
        private enum EPLCOutput
        {
            台车1启动,
            台车2启动,

         
            KM1ONOFF控制,
            KM2ONOFF控制,
            KM3ONOFF控制,
            KM4ONOFF控制,

            门控屏蔽,
   

            产品区温度设定,
            产品区上偏差温度,
            产品区下偏差温度,
            产品区超温上限偏差,
            产品区启动排风温度,
            产品区停止排风温度,
     

            负载区启动排风温度,
            负载区断电温度,
            单点超温报警温度,
            不良报警,
      
            KM1输入电压,
         
            KM2输入电压,

            KM3输入电压,
  
            KM4输入电压,
          

        }


        ///<summary>
        ///读寄存器地址
        ///</summary>
        private Dictionary<EPLCInput, string[]> rREGAdrs = new Dictionary<EPLCInput, string[]>();

        ///<summary>
        ///写寄存器地址
        ///</summary>
        private Dictionary<EPLCOutput, string[]> wREGAdrs = new Dictionary<EPLCOutput, string[]>();


        /// <summary>
        /// 打开PLC设备
        /// </summary>
        /// <param name="iPLCNo">PLC串口编号<param>
        /// <param name="er"></param>
        /// <param name="connect">true=open,false=close</param>
        /// <returns></returns>
        private bool OpenPLC(ref string er, bool connect = true)
        {
            try
            {
                er = string.Empty;

                if (connect)
                {
                    for (int iPLCNo = 0; iPLCNo < CGlobalPara.C_PLCCom_Max; iPLCNo++)
                    {
                        _devPLC.Add (  new CInovance_COM());
                        
                        if (_devPLC[iPLCNo] != null)
                        {
                            if (!_devPLC[iPLCNo].Open(CGlobalPara.SysPara.Dev.plcCom[iPLCNo], out er, CGlobalPara.SysPara.Dev.plcBuad[iPLCNo]))
                            {
                                _devPLC[iPLCNo] = null;
                               er = "打开" + CGlobalPara.SysPara.Dev.plcCom[iPLCNo] + "]失败:" + er;
                                return false;
                            }
                            er = "成功打开[" + CGlobalPara.SysPara.Dev.plcCom[iPLCNo] + "]";
                        }
                    }
                }
                else
                {
                    for (int iPLCNo = 0; iPLCNo < CGlobalPara.C_PLCCom_Max; iPLCNo++)
                    {
                        if (_devPLC[iPLCNo] != null)
                        {
                            _devPLC[iPLCNo].Close();
                            _devPLC[iPLCNo] = null;
                            _devPLC.Clear();
                            er = "关闭[" + CGlobalPara.SysPara.Dev.plcCom[iPLCNo] + "]";
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }


        /// <summary>
        /// 添加输出点地址
        /// </summary>
        /// <param name="outREG"></param>
        /// <param name="Type"></param>
        /// <param name="StartAdrs"></param>
        /// <param name="Times"></param>
        private void addOutAdds(EPLCOutput outREG, string Type, int StartAdrs, int Times, int iAreaTime, int AdrsCount)
        {
            string[] wAdrsValue = new string[CGlobalPara.C_Timer_MAX];
            for (int i = 0; i < CGlobalPara.C_Timer_MAX; i++)
            {
                int iCount = 0;
                if (iAreaTime != 0)
                    iCount = i / iAreaTime;
                if (Times == 0)
                    wAdrsValue[i] = Type + (StartAdrs + i * Times + iCount * AdrsCount).ToString();
                else
                    wAdrsValue[i] = Type + (StartAdrs + i * Times + iCount * AdrsCount - iCount * iAreaTime * Times).ToString();
            }

            wREGAdrs.Add(outREG, wAdrsValue);
        }

        /// <summary>
        /// 添加输入点地址
        /// </summary>
        /// <param name="inREG"></param>
        /// <param name="Type"></param>
        /// <param name="StartAdrs"></param>
        /// <param name="Times"></param>
        private void addInAdds(EPLCInput inREG, string Type, int StartAdrs, int Times, int iAreaTime, int AdrsCount)
        {
            string[] wAdrsValue = new string[CGlobalPara.C_Timer_MAX];
            for (int i = 0; i < CGlobalPara.C_Timer_MAX; i++)
            {
                int iCount = 0;
                if (iAreaTime != 0)
                    iCount = i / iAreaTime;

                if (Times == 0)
                    wAdrsValue[i] = Type + (StartAdrs + i * Times + iCount * AdrsCount).ToString();
                else
                    wAdrsValue[i] = Type + (StartAdrs + i * Times + iCount * AdrsCount - iCount * iAreaTime * Times).ToString();

            }
            rREGAdrs.Add(inREG, wAdrsValue);
        }

        /// <summary>
        /// 初始化PLC寄存器地址 
        /// </summary>
        private void InitialPLCREGAdrs()
        {
          
            addOutAdds(EPLCOutput.台车1启动, "M", 50, 0, 0, 0);
            addOutAdds(EPLCOutput.台车2启动, "M", 51, 0, 0, 0);
            addOutAdds(EPLCOutput.KM1ONOFF控制, "D", 20, 0, 0, 0);
            addOutAdds(EPLCOutput.KM2ONOFF控制, "D", 21, 0, 0, 0);
            addOutAdds(EPLCOutput.KM3ONOFF控制, "D", 22, 0, 0, 0);
            addOutAdds(EPLCOutput.KM4ONOFF控制, "D", 23, 0, 0, 0);

           
            addOutAdds(EPLCOutput.负载区启动排风温度, "D", 2006, 0, 0, 0);
            addOutAdds(EPLCOutput.负载区断电温度, "D", 2102, 0, 0, 0);
            addOutAdds(EPLCOutput.单点超温报警温度, "D", 2110, 0, 0, 0);
            addOutAdds(EPLCOutput.不良报警, "D", 500, 0, 0, 0);
            addOutAdds(EPLCOutput.产品区温度设定, "D", 2000, 0, 0, 0);
            addOutAdds(EPLCOutput.产品区上偏差温度, "D", 2001, 0, 0, 0);
            addOutAdds(EPLCOutput.产品区下偏差温度, "D", 2002, 0, 0, 0);
            addOutAdds(EPLCOutput.产品区超温上限偏差, "D", 2003, 0, 0, 0);
            addOutAdds(EPLCOutput.产品区启动排风温度, "D", 2004, 0, 0, 0);
            addOutAdds(EPLCOutput.产品区停止排风温度, "D", 2005, 0, 0, 0);
            addOutAdds(EPLCOutput.门控屏蔽, "D", 503, 0, 0, 0);
            //addOutAdds(EPLCOutput.循环风机屏蔽, "M", 401, 50, 0, 0);
            addOutAdds(EPLCOutput.KM1输入电压, "D", 10, 0, 0, 0);
            addOutAdds(EPLCOutput.KM2输入电压, "D", 11, 0, 0, 0);
            addOutAdds(EPLCOutput.KM3输入电压, "D", 12, 0, 0, 0);
            addOutAdds(EPLCOutput.KM4输入电压, "D", 13, 0, 0, 0);
            

            addInAdds(EPLCInput.启动信号, "M", 900, 0, 0, 0);  
            addInAdds(EPLCInput.KM1ONOFF信号, "M", 502, 0, 0, 0);
            addInAdds(EPLCInput.KM2ONOFF信号, "M", 505, 0, 0, 0);
            addInAdds(EPLCInput.KM3ONOFF信号, "M", 508, 0, 0, 0);
            addInAdds(EPLCInput.KM4ONOFF信号, "M", 511, 0, 0, 0);
   
            addInAdds(EPLCInput.负载温度1, "D", 86, 0, 0, 0);
            addInAdds(EPLCInput.平均温度, "D", 90, 0, 0, 0);
            addInAdds(EPLCInput.温度点1, "D", 80, 0, 0, 0);
            addInAdds(EPLCInput.温度点2, "D", 81, 0, 0, 0);
            addInAdds(EPLCInput.温度点3, "D", 82, 0, 0, 0);
            addInAdds(EPLCInput.温度点4, "D", 83, 0, 0, 0);
            addInAdds(EPLCInput.温度点5, "D", 84, 0, 0, 0);
            addInAdds(EPLCInput.温度点6, "D", 85, 0, 0, 0);

            addInAdds(EPLCInput.故障1_循环风机异常信号, "M", 800, 0, 0, 0);
            addInAdds(EPLCInput.故障2_辅助电热异常信号, "M", 801, 0, 0, 0);
            addInAdds(EPLCInput.故障3_产品区超温断电, "M", 802, 0, 0, 0);
            addInAdds(EPLCInput.故障4_EGO异常, "M", 803, 0, 0, 0);
            addInAdds(EPLCInput.故障5_温度模块异常, "M", 804, 0, 0, 0);
            addInAdds(EPLCInput.故障6_产品区烟感检测信号触发, "M", 805, 0, 0, 0);
            addInAdds(EPLCInput.故障7_负载区烟感检测信号触发, "M", 806, 0, 0, 0);
            addInAdds(EPLCInput.故障8_控制柜烟感检测信触发, "M", 807, 0, 0, 0);
            addInAdds(EPLCInput.故障9_单点超温异常, "M", 808, 0, 0, 0);
            addInAdds(EPLCInput.故障10_风压开关1信号异常报警, "M", 809, 0, 0, 0);
            addInAdds(EPLCInput.故障11_风压开关2信号异常报警, "M", 810, 0, 0, 0);
            addInAdds(EPLCInput.故障12_老化中开门报警, "M", 811, 0, 0, 0);
         

            addInAdds(EPLCInput.KM1输入电压检测信号110V, "M", 500, 0, 0, 0);
            addInAdds(EPLCInput.KM1输入电压检测信号220V, "M", 501, 0, 0, 0);
            addInAdds(EPLCInput.KM2输入电压检测信号110V, "M", 503, 0, 0, 0);
            addInAdds(EPLCInput.KM2输入电压检测信号220V, "M", 504, 0, 0, 0);
            addInAdds(EPLCInput.KM3输入电压检测信号110V, "M", 506, 0, 0, 0);
            addInAdds(EPLCInput.KM3输入电压检测信号220V, "M", 507, 0, 0, 0);
            addInAdds(EPLCInput.KM4输入电压检测信号110V, "M", 509, 0, 0, 0);
            addInAdds(EPLCInput.KM4输入电压检测信号220V, "M", 510, 0, 0, 0);

        }

        /// <summary>
        /// 获取PLC串口
        /// </summary>
        /// <param name="iCom"></param>
        /// <returns></returns>
        private int getPLCcom(int iCom)
        {
            try
            {
                if (iCom < 3)
                    return 0;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);

                return 0;
            }
        }

        /// <summary>
        /// 获取PLC使用地址
        /// </summary>
        /// <param name="iTimer"></param>
        /// <returns></returns>
        private int getPLCAdrs(int iTimer)
        {
            try
            {
                if (iTimer < 3)
                    return 1;
                else
                    return 1;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);

                return 1;
            }
        }

        /// <summary>
        /// PLC线程
        /// </summary>
        private void OnPLCStart()
        {
            int idNo = System.Convert.ToInt16(Thread.CurrentThread.Name.Substring(0, 2));       //获取当前线程的ID
            try
            {
                while (true)
                {
                    try
                    {
                        if (_threadPLCCancel[idNo])
                            return;
                        string er = string.Empty;
                        readplc_Alarm_Type();
                        for (int i = 0; i < CGlobalPara.C_PLC_Timer; i++)
                        {
                            int iTimer = i + idNo * CGlobalPara.C_PLC_Timer;

                            bool failLink = false;
                            if (_runUUT[iTimer].Para.DoONOFF == AgingONOFFType.复位报警)
                            {
                                setplc_Alarm_Type(iTimer, 0);
                            }
                            //设定ON状态
                            if (_runUUT[iTimer].Para.DoONOFF == AgingONOFFType.ON  && _runUUT[iTimer].Dev.DoEL != AgingELType.设定负载)
                            {
                                runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + "_PLC设定状态ON", udcRunLog.ELog.Action);
                                if (_runUUT[i].Para.DoRun != AgingRunType.运行)
                                {
                                    // if (!setplc_Run_Type(iTimer, 0, 1, 0, 1, 1)) failLink = true;
                                    for (int j = 0; j < 2; j++)
                                    {
                                        if (!setplc_Run_Type(iTimer, 0, 1, 1, 1, 1)) failLink = true;
                                    }
                                }
                                else
                                {
                                    for (int j = 0; j < 2; j++)
                                    {
                                        if (!setplc_Run_Type(iTimer, 0, 1, 1, 1, 1)) failLink = true;
                                    }
                                }
                                   

                                if (_runUUT[iTimer].PLC.onoffStat == 1)
                                {
                                    if (_runUUT[iTimer].Para.DoONOFF == AgingONOFFType.ON)
                                        _runUUT[iTimer].Para.DoONOFF = AgingONOFFType.空闲;
                                    runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + "_PLC设定状态ON完成", udcRunLog.ELog.Action);
                                    _runUUT[iTimer].Para.SaveCtrlTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                                }
                            }

                            if (_runUUT[iTimer].Para.DoONOFF == AgingONOFFType.OFF)         //设定OFF状态
                            {
                                runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + "_PLC设定状态OFF", udcRunLog.ELog.Action);
                                if (_runUUT[i].Para.DoRun != AgingRunType.运行 && _runUUT[iTimer].Para.DoRun != AgingRunType.暂停)
                                {
                                    for (int j = 0; j < 2; j++)
                                    {
                                      if (!setplc_Run_Type(iTimer, 1, 0, 0, 0, 0)) failLink = true;
                                   }

                                    if (CGlobalPara.SysPara.Alarm.uutAlarm)
                                    {
                                        if (_runUUT[iTimer].Para.TTNum != _runUUT[iTimer].Para.PassNum)
                                            setplc_Alarm_Type(iTimer, 1);
                                        else
                                            setplc_Alarm_Type(iTimer, 0);
                                    }
                                    else
                                    {
                                        setplc_Alarm_Type(iTimer, 0);
                                    }
                                }
                                else if (_runUUT[iTimer].Para.DoRun == AgingRunType.暂停)
                                {
                                    for (int j = 0; j < 2; j++)
                                    {
                                        if (!setplc_Run_Type(iTimer, 0, 0, 0, 0, 0)) failLink = true;
                                    }
                                    
                                }
                                else
                                {
                                    for (int j = 0; j < 2; j++)
                                    {
                                        if (!setplc_Run_Type(iTimer, 0, 0, 0, 0, 0)) failLink = true;
                                    }
                                }

                                if (_runUUT[iTimer].PLC.onoffStat == 0)
                                {
                                    if (_runUUT[iTimer].Para.DoONOFF == AgingONOFFType.OFF)
                                        _runUUT[iTimer].Para.DoONOFF = AgingONOFFType.空闲;
                                    runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + "_PLC设定状态OFF完成", udcRunLog.ELog.Action);
                                }
                                else
                                {
                                    runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + "_PLC设定状态OFF", udcRunLog.ELog.Action);
                                    if (_runUUT[i].Para.DoRun != AgingRunType.运行)
                                    {
                                       
                                        if (!setplc_Run_Type(iTimer, 1, 0, 0, 0, 0)) failLink = true;

                                        if (CGlobalPara.SysPara.Alarm.uutAlarm)
                                        {
                                            if (_runUUT[iTimer].Para.TTNum != _runUUT[iTimer].Para.PassNum)
                                                setplc_Alarm_Type(iTimer, 1);
                                            else
                                                setplc_Alarm_Type(iTimer, 0);
                                        }
                                        else
                                        {
                                            setplc_Alarm_Type(iTimer, 0);
                                        }
                                    }
                                    else
                                  
                                            if (!setplc_Run_Type(iTimer, 0, 1, 0, 1, 0)) failLink = true;
                                        
                                }
                                _runUUT[iTimer].Para.SaveCtrlTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                            }
                            if (_runUUT[iTimer].Para.DoRun == AgingRunType.暂停)
                            {
                                if ( _runUUT[iTimer].PLC.runStartStat == 1)
                                {
                                    ctrl_continue_info(iTimer, false);
                                }
                            }

                            readPLCSignal(iTimer);              //实时刷新PLC状态

                            ///***************************检测急停信号*******************************/
                            //if (_runUUT[iTimer].Para.DoRun == AgingRunType.自检 || _runUUT[iTimer].Para.DoRun == AgingRunType.运行)
                            //{
                            //    if (_runUUT[iTimer].PLC.onoffStat == 0 && _runUUT[i].OnOff.TimeRun.CurRunVolt > 0)
                            //    {
                            //        CGlobalPara.C_PLC_TimeOut[iTimer] += 1;
                            //        if (CGlobalPara.C_PLC_TimeOut[iTimer] > 3)
                            //        {
                            //            ctrl_Pause_info(iTimer, false);
                            //            runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + "_PLC检测不到运行状态异常,自动暂停，请确认后手动开启！", udcRunLog.ELog.NG);
                            //        }
                            //    }
                            //    else
                            //        CGlobalPara.C_PLC_TimeOut[iTimer] = 0;
                            //}
                            //else
                            //    CGlobalPara.C_PLC_TimeOut[iTimer] = 0;

                            /***************************检测到信号开始老化*******************************/

                            if (_runUUT[iTimer].PLC.reStartStat == 0 && _runUUT[iTimer].PLC.runStartStat == 0 && _runUUT[iTimer].PLC.runStat == 0)               //停止
                            {
                                //if (_runUUT[iTimer].Para.DoRun == AgingRunType.运行)
                                //{
                                //    _runUUT[iTimer].Para.DoRun = AgingRunType.空闲;
                                //    _runUUT[iTimer].Para.DoONOFF = AgingONOFFType.OFF;
                                //    _runUUT[iTimer].Para.DoData = AgingDataType.空闲;
                                //    updata_runPara_database(iTimer);
                                //    uiRunStatus[iTimer].SetRunStatus(_runUUT[iTimer]);      //初始化界面
                                //}
                            }



                            //if (_runUUT[iTimer].PLC.runStat == 1 && _runUUT[iTimer].Para.DoRun != AgingRunType.暂停)
                            //{

                            //    if (_runUUT[iTimer].Para.DoRun != AgingRunType.自检 &&
                            //        _runUUT[iTimer].Para.DoRun != AgingRunType.运行)     //空闲时开启自检
                            //    {
                            //        _runUUT[iTimer].Para.RunInVolt = _runModel[iTimer].OnOff[0].Item[0].InPutV;
                            //        _runUUT[iTimer].Para.DoRun = AgingRunType.自检;
                            //        _runUUT[iTimer].Para.DoONOFF = AgingONOFFType.ON;
                            //        _runUUT[iTimer].Dev.DoEL = AgingELType.设定负载;
                            //        _runUUT[iTimer].Para.DoData = AgingDataType.空闲;
                            //        _runUUT[iTimer].Para.SaveReStartTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                            //    }

                            //    if (_runUUT[iTimer].Para.DoRun == AgingRunType.自检 &&
                            //        _runUUT[iTimer].PLC.runStartStat == 1)               //检测到启动信号时开始老化
                            //    {
                            //        _runUUT[iTimer].Para.DoRun = AgingRunType.运行;
                            //        _runUUT[iTimer].Para.RunTime = 0;
                            //        _runUUT[iTimer].Para.iniSpec = 1;
                            //        _runUUT[iTimer].Para.UserName = CIniFile.ReadFromIni("UseSet", "UserName", CGlobalPara.UserFile, "");
                            //        _runUUT[iTimer].Para.StartTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                            //        _runUUT[iTimer].Para.EndTime = DateTime.Now.AddSeconds(_runUUT[iTimer].Para.BurnTime).ToString("yyyy/MM/dd HH:mm:ss");
                            //        _runUUT[iTimer].Para.SaveDataTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                            //        _runUUT[iTimer].Para.SaveDataIndex = 0;
                            //        _runUUT[iTimer].Para.SaveCtrlTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                            //        _runUUT[iTimer].Para.SavePath = CGlobalPara.SysPara.Report.ReportPath + "\\" + _runUUT[iTimer].Para.TimerName + "\\" +
                            //                                     _runUUT[iTimer].Para.ModelName + "\\" + DateTime.Now.ToString("yyyy_MM_dd");
                            //        ClearTempChart(iTimer);

                            //        updata_runPara_database(iTimer);
                            //        uut_save_report_Path(iTimer);
                            //    }
                            //}

                            if (_runUUT[iTimer].Para.DoRun == AgingRunType.自检 || _runUUT[iTimer].Para.DoRun == AgingRunType.运行)
                            {
                                if (CGlobalPara.SysPara.Alarm.uutAlarm)
                                    if (_runUUT[iTimer].Para.TTNum != _runUUT[iTimer].Para.PassNum)
                                        setplc_Alarm_Type(iTimer, 1);
                                    else
                                        setplc_Alarm_Type(iTimer, 0);
                                else
                                    setplc_Alarm_Type(iTimer, 0);
                            }
                        }

                        CTimer.WaitMs(100);
                    }
                    catch (Exception ex)
                    {
                        runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
            finally
            {
                _threadPLCCancel[idNo] = false;
                runLog.Log(CLanguage.Lan("时序监控线程销毁"), udcRunLog.ELog.Content);
            }
        }
        /// <summary>
        /// 获取寄存器类型
        /// </summary>
        /// <param name="reg"></param>
        /// <returns></returns>
        private int getRegType(string regAdrs)
        {
            try
            {
                if (regAdrs.Length < 2)
                    return -1;
                Dictionary<string, int> devType = new Dictionary<string, int>();
                devType.Add("M", 0);
                devType.Add("W", 1);
                devType.Add("D", 2);
                devType.Add("X", 3);
                devType.Add("Y", 4);
                string regType = regAdrs.Substring(0, 1);
                int rType = devType[regType];
                return rType;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 获取寄存器地址
        /// </summary>
        /// <param name="reg"></param>
        /// <returns></returns>
        private int getRegAdrs(string regAdrs)
        {
            try
            {
                if (regAdrs.Length < 2)
                    return 0;

                int rType = System.Convert.ToInt16(regAdrs.Substring(1, regAdrs.Length - 1));
                return rType;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 初始化老化区运行状态
        /// </summary>
        /// <param name="outREG"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IniPLCSpec(int iTimer)
        {
            try
            {
                if (!setplc_Temp_Spec(iTimer))
                    return false;
                for (int j = 0; j < 2; j++)
                {
                    if (!setplc_Run_Type(iTimer, 1, 0, 0, 0, 0))
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }

        /// <summary>
        /// 读取PLC输出信号及运行信息
        /// </summary>
        private bool readPLCSignal(int iTimer)
        {
            try
            {
                if (!readplc_Run_Type(iTimer))
                    return false;
                if (_runUUT[iTimer].Para.DoRun == AgingRunType.自检 || _runUUT[iTimer].Para.DoRun == AgingRunType.运行)
                {
                    if (!readplc_Temp_Type(iTimer))
                        return false;
                    if (!readplc_Time_Type(iTimer))
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }

        }

        /// <summary>
        /// 设定温度参数
        /// </summary>
        /// <param name="iTimer">时序号</param>
        /// <returns></returns>
        private bool setplc_Temp_Spec(int iTimer)
        {
            try
            {

                int[] wVal = new int[1];

                wVal[0] = System.Convert.ToInt16(_runModel[iTimer].Para .TSet  * 10);       //温度设定
                if (!addREGWrite(0, EPLCOutput.产品区温度设定, wVal))
                {
                    runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("设定产品区温度:") + _runModel[iTimer].Para.TSet.ToString() + CLanguage.Lan("失败!"), udcRunLog.ELog.Err);
                    return false;
                }

                wVal[0] = System.Convert.ToInt16(_runModel[iTimer].Para.TLP * 10);       //产品区温度下限
                if (!addREGWrite(0, EPLCOutput.产品区下偏差温度, wVal))
                {
                    runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("设定产品区温度下限:") + _runModel[iTimer].Para.TLP.ToString() + CLanguage.Lan("失败!"), udcRunLog.ELog.Err);
                    return false;
                }


                wVal[0] = System.Convert.ToInt16(_runModel[iTimer].Para.THP* 10);       //产品区温度上限
                if (!addREGWrite(0, EPLCOutput.产品区上偏差温度, wVal))
                {
                    runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("设定产品区温度上限:") + _runModel[iTimer].Para.THP.ToString() + CLanguage.Lan("失败!"), udcRunLog.ELog.Err);
                    return false;
                }


                wVal[0] = System.Convert.ToInt16(_runModel[iTimer].Para.THAlarm* 10);       //产品区超温上限
                if (!addREGWrite(0, EPLCOutput.产品区超温上限偏差, wVal))
                {
                    runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("设定产品区超温上限偏差:") + _runModel[iTimer].Para.THAlarm.ToString() + CLanguage.Lan("失败!"), udcRunLog.ELog.Err);
                    return false;
                }

                wVal[0] = System.Convert.ToInt16(_runModel[iTimer].Para.TOPEN * 10);       //产品区启动排风温度
                if (!addREGWrite(0, EPLCOutput.产品区启动排风温度, wVal))
                {
                    runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("设定产品区启动排风温度:") + _runModel[iTimer].Para.TOPEN.ToString() + CLanguage.Lan("失败!"), udcRunLog.ELog.Err);
                    return false;
                }


                wVal[0] = System.Convert.ToInt16(_runModel[iTimer].Para.TCLOSE * 10);       //产品区停止排风温度
                if (!addREGWrite(0, EPLCOutput.产品区停止排风温度, wVal))
                {
                    runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("设定产品区停止排风温度:") + _runModel[iTimer].Para.TCLOSE.ToString() + CLanguage.Lan("失败!"), udcRunLog.ELog.Err);
                    return false;
                }

              

                wVal[0] = System.Convert.ToInt16( CGlobalPara.SysPara.Alarm.LoadOpenFan   * 10);       //负载启动排风
                if (!addREGWrite(0, EPLCOutput.负载区启动排风温度, wVal))
                {
                    runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("设定负载启动排风温度:") + CGlobalPara.SysPara.Alarm.LoadOpenFan.ToString() + CLanguage.Lan("失败!"), udcRunLog.ELog.Err);
                    return false;
                }

                wVal[0] = System.Convert.ToInt16(CGlobalPara.SysPara.Alarm.LoadCloseFan  * 10);       //负载停止排风
                if (!addREGWrite(0, EPLCOutput.负载区断电温度, wVal))
                {
                    runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("设定负载停止排风温度:") + CGlobalPara.SysPara.Alarm.LoadCloseFan.ToString() + CLanguage.Lan("失败!"), udcRunLog.ELog.Err);
                    return false;
                }

                wVal[0] = System.Convert.ToInt16(CGlobalPara.SysPara.Alarm.LoadAlarm * 10);      //单点超温报警温度
                if (!addREGWrite(0, EPLCOutput.单点超温报警温度, wVal))
                {
                    runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("设定单点超温报警温度:") + CGlobalPara.SysPara.Alarm.LoadAlarm.ToString() + CLanguage.Lan("失败!"), udcRunLog.ELog.Err);
                    return false;
                }

          

                wVal[0] = 0;      //单点超温报警温度
                EPLCOutput[] name = new EPLCOutput[] { iTimer == 0 ? EPLCOutput.KM1输入电压 : EPLCOutput.KM3输入电压, iTimer == 0 ? EPLCOutput.KM2输入电压 : EPLCOutput.KM4输入电压 };
                if (!addREGWrite(iTimer, name[0], wVal))
                {
                    runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("设定输入电压KM1失败:") + CGlobalPara.SysPara.Alarm.LoadAlarm.ToString() + CLanguage.Lan("失败!"), udcRunLog.ELog.Err);
                    return false;
                }
                if (!addREGWrite(iTimer, name[1], wVal))
                {
                    runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("设定输入电压KM2失败:") + CGlobalPara.SysPara.Alarm.LoadAlarm.ToString() + CLanguage.Lan("失败!"), udcRunLog.ELog.Err);
                    return false;
                }
           

                Thread.Sleep(2000);
                  //单点超温报警温度
                if(_runModel[iTimer].OnOff[0].Item[0].InPutV==110)
                {
                    wVal[0] = 1;
                    if (!addREGWrite(iTimer, name[0], wVal))
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("设定输入电压KM1失败:") + CGlobalPara.SysPara.Alarm.LoadAlarm.ToString() + CLanguage.Lan("失败!"), udcRunLog.ELog.Err);
                        return false;
                    }
                    if (!addREGWrite(iTimer, name[1], wVal))
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("设定输入电压KM2失败:") + CGlobalPara.SysPara.Alarm.LoadAlarm.ToString() + CLanguage.Lan("失败!"), udcRunLog.ELog.Err);
                        return false;
                    }

                }
                else if( _runModel[iTimer].OnOff[0].Item[0].InPutV==220)
                {
                    wVal[0] = 2;
                    if (!addREGWrite(iTimer, name[0], wVal))
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("设定输入电压KM1失败:") + CGlobalPara.SysPara.Alarm.LoadAlarm.ToString() + CLanguage.Lan("失败!"), udcRunLog.ELog.Err);
                        return false;
                    }
                    if (!addREGWrite(iTimer, name[1], wVal))
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("设定输入电压KM2失败:") + CGlobalPara.SysPara.Alarm.LoadAlarm.ToString() + CLanguage.Lan("失败!"), udcRunLog.ELog.Err);
                        return false;
                    }
                }
              
            
                //wVal[0] = 0;
                //if ( CGlobalPara.SysPara.Para .chkNoF  == true)
                //{
                //    wVal[0] = 1;
                //}
                //if (!addREGWrite(iTimeNo, EPLCOutput.循环风机屏蔽, wVal))
                //{
                //    runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimeNo] + CLanguage.Lan("循环风机屏蔽失败!"), udcRunLog.ELog.Err);
                //    return false;
                //}

                wVal[0] = 0;
                if (CGlobalPara.SysPara.Para.chkNoGating == true)
                {
                    wVal[0] = 1;
                }

                if (!addREGWrite(iTimer, EPLCOutput.门控屏蔽, wVal))
                {
                    runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("门控屏蔽失败!"), udcRunLog.ELog.Err);
                    return false;
                }

             

                return true;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }



        /// <summary>
        /// 设定输入电压切换
        /// </summary>
        /// <param name="iTimeNo">时序号</param>
        /// <returns></returns>
        private bool setplc_ACVolt_Spec(int iTimer, int curVolt)
        {
            try
            {
                int[] wVal = new int[1];
                wVal[0] = 1;
                if (curVolt == 110)
                {
                    wVal[0] = 1;
                    EPLCOutput[] name = new EPLCOutput[] { iTimer == 0 ? EPLCOutput.KM1输入电压 : EPLCOutput.KM3输入电压, iTimer == 0 ? EPLCOutput.KM2输入电压 : EPLCOutput.KM4输入电压 };
                    if (!addREGWrite(iTimer, name[0], wVal))
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("设定输入电压KM1失败:") + CGlobalPara.SysPara.Alarm.LoadAlarm.ToString() + CLanguage.Lan("失败!"), udcRunLog.ELog.Err);
                        return false;
                    }
                    if (!addREGWrite(iTimer, name[1], wVal))
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("设定输入电压KM2失败:") + CGlobalPara.SysPara.Alarm.LoadAlarm.ToString() + CLanguage.Lan("失败!"), udcRunLog.ELog.Err);
                        return false;
                    }

                }
                else if (curVolt == 220)
                {
                    wVal[0] = 2;
                    EPLCOutput[] name = new EPLCOutput[] { iTimer == 0 ? EPLCOutput.KM1输入电压 : EPLCOutput.KM3输入电压, iTimer == 0 ? EPLCOutput.KM2输入电压 : EPLCOutput.KM4输入电压 };

                    if (!addREGWrite(iTimer, name[0], wVal))
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("设定输入电压KM1失败:") + CGlobalPara.SysPara.Alarm.LoadAlarm.ToString() + CLanguage.Lan("失败!"), udcRunLog.ELog.Err);
                        return false;
                    }
                    if (!addREGWrite(iTimer, name[1], wVal))
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("设定输入电压KM2失败:") + CGlobalPara.SysPara.Alarm.LoadAlarm.ToString() + CLanguage.Lan("失败!"), udcRunLog.ELog.Err);
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }

        /// <summary>
        /// 设定运行状态 （风机信号+ONOFF信号）
        /// </summary>
        /// <param name="iTimer"></param>
        /// <param name="stop">停止</param>
        /// <param name="scan">设定自检</param>
        /// <param name="startRun">启动老化</param>
        /// <param name="runStat">风机运行</param>
        /// <param name="onoffStat">ONOFF运行</param>
        /// <returns></returns>
        private bool setplc_Run_Type(int iTimer, int stop, int scan, int startRun, int runStat, int onoffStat)
        {
            try
            {
                int[] wVal = new int[1];
                if (stop == 1)
                {
                    wVal[0] = 0;
                    EPLCOutput[] name = new EPLCOutput[] { EPLCOutput.台车1启动, EPLCOutput.台车2启动 };
                    if (!addREGWrite(iTimer, name[iTimer], wVal))
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("设定停止状态:") + runStat + CLanguage.Lan("失败!"), udcRunLog.ELog.Err);
                        return false;
                    }
                }

                if (startRun == 1)
                {
                    wVal[0] = 1;
                    EPLCOutput[] name = new EPLCOutput[] { EPLCOutput.台车1启动, EPLCOutput.台车2启动 };
                    if (!addREGWrite(iTimer, name[iTimer], wVal))
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("设定启动状态:") + runStat + CLanguage.Lan("失败!"), udcRunLog.ELog.Err);
                        return false;
                    }
                }

                if (onoffStat == 0)
                {

                    wVal[0] = 0;
                    EPLCOutput[] name = new EPLCOutput[] { iTimer == 0 ? EPLCOutput.KM1ONOFF控制 : EPLCOutput.KM3ONOFF控制, iTimer == 0 ? EPLCOutput.KM2ONOFF控制 : EPLCOutput.KM4ONOFF控制 };
                    if (!addREGWrite(iTimer, name[0], wVal))
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("设定ONOFF状态:") + onoffStat + CLanguage.Lan("失败!"), udcRunLog.ELog.Err);
                        return false;
                    }

                    if (!addREGWrite(iTimer, name[1], wVal))
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("设定ONOFF状态:") + onoffStat + CLanguage.Lan("失败!"), udcRunLog.ELog.Err);
                        return false;
                    }
                    CTimer.WaitMs(1000);


                }
                else
                {

                    wVal[0] = 1;
                    EPLCOutput[] name = new EPLCOutput[] { iTimer == 0 ? EPLCOutput.KM1ONOFF控制 : EPLCOutput.KM3ONOFF控制, iTimer == 0 ? EPLCOutput.KM2ONOFF控制 : EPLCOutput.KM4ONOFF控制 };
                    if (!addREGWrite(iTimer, name[0], wVal))
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("设定ONOFF状态:") + onoffStat + CLanguage.Lan("失败!"), udcRunLog.ELog.Err);
                        return false;
                    }
                    if (!addREGWrite(iTimer, name[1], wVal))
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("设定ONOFF状态:") + onoffStat + CLanguage.Lan("失败!"), udcRunLog.ELog.Err);
                        return false;
                    }
                    CTimer.WaitMs(1000);
                }
                return true;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }

        /// <summary>
        /// 设定不良报警状态
        /// </summary>
        /// <param name="iTimer"></param>
        /// <param name="alarmType"></param>
        /// <returns></returns>
        private bool setplc_Alarm_Type(int iTimer, int alarmType)
        {
            try
            {
                int[] wVal = new int[1];

                wVal[0] = alarmType;
                if (!addREGWrite(0, EPLCOutput.不良报警, wVal))
                {
                    runLog.Log(CLanguage.Lan("设定不良报警状态:") + alarmType + CLanguage.Lan("失败!"), udcRunLog.ELog.Err);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }

        /// <summary>
        /// 读取PLC运行状态信号
        /// </summary>
        /// <param name="iTimeNo"></param>
        /// <returns></returns>
        private bool readplc_Run_Type(int iTimer)
        {
            try
            {
                int[] wVal = new int[1];
                bool readok = false;
                for (int i = 0; i < 3; i++)
                {
                    wVal[0] = -1;
                    if (!addREGRead(iTimer , EPLCInput.启动信号, ref wVal))
                    {
                        runLog.Log(CLanguage.Lan("读取启动信号失败!"), udcRunLog.ELog.Err);
                    }
                    else
                    {
                        readok = true;
                        _runUUT[iTimer].PLC.runStat = wVal[0];
                        break;
                    }
                }
                if (!readok)
                    return false;

                readok = false;
                for (int i = 0; i < 3; i++)
                {
                    wVal[0] = -1;
                    EPLCInput[] name = new EPLCInput[] { iTimer == 0 ? EPLCInput.KM1ONOFF信号 : EPLCInput.KM3ONOFF信号, iTimer == 0 ? EPLCInput.KM2ONOFF信号 : EPLCInput.KM4ONOFF信号 };
                    if (!addREGRead(iTimer, name[0], ref wVal))
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("读取ONOFF信号失败!"), udcRunLog.ELog.Err);
                    }
                    else
                    {
                        readok = true;
                        _runUUT[iTimer].PLC.onoffStat = wVal[0];
                        break;
                    }
                }
                if (!readok)
                    return false;

                readok = false;
                for (int i = 0; i < 3; i++)
                {
                    wVal[0] = -1;
                    EPLCInput[] name = new EPLCInput[] { iTimer == 0 ? EPLCInput.KM1输入电压检测信号110V : EPLCInput.KM3输入电压检测信号110V, iTimer == 0 ? EPLCInput.KM2输入电压检测信号110V : EPLCInput.KM4输入电压检测信号110V };
                    if (!addREGRead(iTimer, name[0], ref wVal))
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("读取ONOFF信号失败!"), udcRunLog.ELog.Err);

                    }
                    else
                    {
                        readok = true;
                        if (wVal[0] == 1)
                        {
                            _runUUT[iTimer].PLC.InVolt = 110;
                        }
                        break;
                    }
                }
                if (!readok)
                    return false;

                readok = false;
                for (int i = 0; i < 3; i++)
                {
                    wVal[0] = -1;
                    EPLCInput[] name = new EPLCInput[] { iTimer == 0 ? EPLCInput.KM1输入电压检测信号220V : EPLCInput.KM3输入电压检测信号220V, iTimer == 0 ? EPLCInput.KM2输入电压检测信号220V : EPLCInput.KM4输入电压检测信号220V };

                    if (!addREGRead(iTimer, name[0], ref wVal))
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimer] + CLanguage.Lan("读取ONOFF信号失败!"), udcRunLog.ELog.Err);

                    }
                    else
                    {
                        readok = true;
                        if (wVal[0] == 1)
                        {
                            _runUUT[iTimer].PLC.InVolt = 220;
                        }
                        break;
                    }
                }
                if (!readok)
                    return false;

               
    

                return true;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }
        /// <summary>
        /// 读取异常信号
        /// </summary>
        /// <param name="iTimeNo"></param>
        /// <returns></returns>
        private bool readplc_Alarm_Type()
        {
            try
            {

                bool readok = false;
                string AlarmData = string.Empty;
         

                for (int iTimeNo = 0; iTimeNo < CGlobalPara.C_Timer_MAX; iTimeNo++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int[] wVal = new int[1];
                        for (int j = 0; j < 1; j++)
                        {
                            wVal[j] = 0;
                        }
                        if (!addREGRead(0, EPLCInput.故障1_循环风机异常信号, ref wVal))
                        {
                            runLog.Log(CLanguage.Lan("读取循环风机异常信号_" + (iTimeNo + 1).ToString() + "失败!"), udcRunLog.ELog.Err);
                        }
                        else
                        {
                            readok = true;
                            if (wVal[0] == 1)
                            {
                                AlarmData += "循环风机异常" + ",";
                            }
                            break;
                        }
                    }
                    if (!readok)
                        return false;
                }

                for (int iTimeNo = 0; iTimeNo < CGlobalPara.C_Timer_MAX; iTimeNo++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int[] wVal = new int[1];
                        for (int j = 0; j < 1; j++)
                        {
                            wVal[j] = 0;
                        }
                        if (!addREGRead(0, EPLCInput.故障2_辅助电热异常信号, ref wVal))
                        {
                            runLog.Log(CLanguage.Lan("读取辅助电热异常信号_" + (iTimeNo + 1).ToString() + "失败!"), udcRunLog.ELog.Err);
                        }
                        else
                        {
                            readok = true;
                            if (wVal[0] == 1)
                            {
                                AlarmData += "辅助电热异常" + ",";
                            }
                            break;
                        }
                    }
                    if (!readok)
                        return false;
                }
                for (int iTimeNo = 0; iTimeNo < CGlobalPara.C_Timer_MAX; iTimeNo++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int[] wVal = new int[1];
                        for (int j = 0; j < 1; j++)
                        {
                            wVal[j] = 0;
                        }
                        if (!addREGRead(0, EPLCInput.故障3_产品区超温断电, ref wVal))
                        {
                            runLog.Log(CLanguage.Lan("读取产品区超温断电信号_" + (iTimeNo + 1).ToString() + "失败!"), udcRunLog.ELog.Err);
                        }
                        else
                        {
                            readok = true;
                            if (wVal[0] == 1)
                            {
                                AlarmData += "产品区超温断电" + ",";
                            }
                            break;
                        }
                    }
                    if (!readok)
                        return false;
                }
                for (int iTimeNo = 0; iTimeNo < CGlobalPara.C_Timer_MAX; iTimeNo++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int[] wVal = new int[1];
                        for (int j = 0; j < 1; j++)
                        {
                            wVal[j] = 0;
                        }
                        if (!addREGRead(0, EPLCInput.故障4_EGO异常, ref wVal))
                        {
                            runLog.Log(CLanguage.Lan("读取EGO异常异常信号_" + (iTimeNo + 1).ToString() + "失败!"), udcRunLog.ELog.Err);
                        }
                        else
                        {
                            readok = true;
                            if (wVal[0] == 1)
                            {
                                AlarmData += "EGO异常" + ",";
                            }
                            break;
                        }
                    }
                    if (!readok)
                        return false;
                }
                for (int iTimeNo = 0; iTimeNo < CGlobalPara.C_Timer_MAX; iTimeNo++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int[] wVal = new int[1];
                        for (int j = 0; j < 1; j++)
                        {
                            wVal[j] = 0;
                        }
                        if (!addREGRead(0, EPLCInput.故障5_温度模块异常, ref wVal))
                        {
                            runLog.Log(CLanguage.Lan("读取温度模块异常信号_" + (iTimeNo + 1).ToString() + "失败!"), udcRunLog.ELog.Err);
                        }
                        else
                        {
                            readok = true;
                            if (wVal[0] == 1)
                            {
                                AlarmData += "温度模块异常" + ",";
                            }
                            break;
                        }
                    }
                    if (!readok)
                        return false;
                }
                for (int iTimeNo = 0; iTimeNo < CGlobalPara.C_Timer_MAX; iTimeNo++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int[] wVal = new int[1];
                        for (int j = 0; j < 1; j++)
                        {
                            wVal[j] = 0;
                        }
                        if (!addREGRead(0, EPLCInput.故障6_产品区烟感检测信号触发, ref wVal))
                        {
                            runLog.Log(CLanguage.Lan("读取产品区烟感检测信号触发信号_" + (iTimeNo + 1).ToString() + "失败!"), udcRunLog.ELog.Err);
                        }
                        else
                        {
                            readok = true;
                            if (wVal[0] == 1)
                            {
                                AlarmData += "产品区烟感检测信号触发" + ",";
                            }
                            break;
                        }
                    }
                    if (!readok)
                        return false;
                }
                for (int iTimeNo = 0; iTimeNo < CGlobalPara.C_Timer_MAX; iTimeNo++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int[] wVal = new int[1];
                        for (int j = 0; j < 1; j++)
                        {
                            wVal[j] = 0;
                        }
                        if (!addREGRead(0, EPLCInput.故障7_负载区烟感检测信号触发, ref wVal))
                        {
                            runLog.Log(CLanguage.Lan("读取负载区烟感检测信号触发_" + (iTimeNo + 1).ToString() + "失败!"), udcRunLog.ELog.Err);
                        }
                        else
                        {
                            readok = true;
                            if (wVal[0] == 1)
                            {
                                AlarmData += "负载区烟感检测信号触发" + ",";
                            }
                            break;
                        }
                    }
                    if (!readok)
                        return false;
                }
                for (int iTimeNo = 0; iTimeNo < CGlobalPara.C_Timer_MAX; iTimeNo++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int[] wVal = new int[1];
                        for (int j = 0; j < 1; j++)
                        {
                            wVal[j] = 0;
                        }
                        if (!addREGRead(0, EPLCInput.故障8_控制柜烟感检测信触发, ref wVal))
                        {
                            runLog.Log(CLanguage.Lan("读取控制柜烟感检测信触发信号_" + (iTimeNo + 1).ToString() + "失败!"), udcRunLog.ELog.Err);
                        }
                        else
                        {
                            readok = true;
                            if (wVal[0] == 1)
                            {
                                AlarmData += "控制柜烟感检测信触发" + ",";
                            }
                            break;
                        }
                    }
                    if (!readok)
                        return false;
                }
                for (int iTimeNo = 0; iTimeNo < CGlobalPara.C_Timer_MAX; iTimeNo++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int[] wVal = new int[1];
                        for (int j = 0; j < 1; j++)
                        {
                            wVal[j] = 0;
                        }
                        if (!addREGRead(0, EPLCInput.故障9_单点超温异常, ref wVal))
                        {
                            runLog.Log(CLanguage.Lan("读取单点超温信号_" + (iTimeNo + 1).ToString() + "失败!"), udcRunLog.ELog.Err);
                        }
                        else
                        {
                            readok = true;
                            if (wVal[0] == 1)
                            {
                                AlarmData += "单点超温" + ",";
                            }
                            break;
                        }
                    }
                    if (!readok)
                        return false;
                }
                for (int iTimeNo = 0; iTimeNo < CGlobalPara.C_Timer_MAX; iTimeNo++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int[] wVal = new int[1];
                        for (int j = 0; j < 1; j++)
                        {
                            wVal[j] = 0;
                        }
                        if (!addREGRead(0, EPLCInput.故障10_风压开关1信号异常报警, ref wVal))
                        {
                            runLog.Log(CLanguage.Lan("读取风压开关1信号异常报警信号_" + (iTimeNo + 1).ToString() + "失败!"), udcRunLog.ELog.Err);
                        }
                        else
                        {
                            readok = true;
                            if (wVal[0] == 1)
                            {
                                AlarmData += "风压开关1信号异常报警" + ",";
                            }
                            break;
                        }
                    }
                    if (!readok)
                        return false;
                }
                for (int iTimeNo = 0; iTimeNo < CGlobalPara.C_Timer_MAX; iTimeNo++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int[] wVal = new int[1];
                        for (int j = 0; j < 1; j++)
                        {
                            wVal[j] = 0;
                        }
                        if (!addREGRead(0, EPLCInput.故障11_风压开关2信号异常报警, ref wVal))
                        {
                            runLog.Log(CLanguage.Lan("读取风压开关2信号异常报警信号_" + (iTimeNo + 1).ToString() + "失败!"), udcRunLog.ELog.Err);
                        }
                        else
                        {
                            readok = true;
                            if (wVal[0] == 1)
                            {
                                AlarmData += "风压开关2信号异常报警" + ",";
                            }
                            break;
                        }
                    }
                    if (!readok)
                        return false;
                }
                for (int iTimeNo = 0; iTimeNo < CGlobalPara.C_Timer_MAX; iTimeNo++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int[] wVal = new int[1];
                        for (int j = 0; j < 1; j++)
                        {
                            wVal[j] = 0;
                        }
                        if (!addREGRead(0, EPLCInput.故障12_老化中开门报警, ref wVal))
                        {
                            runLog.Log(CLanguage.Lan("读取开门报警信号_" + (iTimeNo + 1).ToString() + "失败!"), udcRunLog.ELog.Err);
                        }
                        else
                        {
                            readok = true;
                            if (wVal[0] == 1)
                            {
                                AlarmData += "开门报警" + ",";
                            }
                            break;
                        }
                    }
                    if (!readok)
                        return false;
                }

                for (int iTimeNo = 0; iTimeNo < CGlobalPara.C_Timer_MAX; iTimeNo++)
                {
                    _runUUT[iTimeNo].PLC.alarmStat = AlarmData;
                    if (_runUUT[iTimeNo].PLC.alarmStat != string.Empty )
                    {
                        runLog.Log($"PLC异常告警：{AlarmData}", udcRunLog.ELog.NG);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }


        /// <summary>
        /// 读取温度信号
        /// </summary>
        /// <param name="iTimeNo"></param>
        /// <returns></returns>
        private bool readplc_Temp_Type(int iTimeNo)
        {
            try
            {

                bool readok = false; ;

                for (int i = 0; i < 3; i++)
                {
                    int[] wVal = new int[1];
                    for (int j = 0; j < 1; j++)
                    {
                        wVal[j] = 0;
                    }
                    if (!addREGRead(0, EPLCInput.负载温度1, ref wVal))
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimeNo] + CLanguage.Lan("读取负载温度1失败!"), udcRunLog.ELog.Err);
                    }
                    else
                    {
                        readok = true;
                        _runUUT[iTimeNo].PLC.LTempPoint[0] = (double)wVal[0] / 10;
                        break;
                    }
                }
                if (!readok)
                    return false;


                for (int i = 0; i < 3; i++)
                {
                    int[] wVal = new int[1];
                    for (int j = 0; j < 1; j++)
                    {
                        wVal[j] = 0;
                    }
                    if (!addREGRead(0, EPLCInput.平均温度, ref wVal))
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimeNo] + CLanguage.Lan("读取平均温度失败!"), udcRunLog.ELog.Err);
                    }
                    else
                    {
                        readok = true;
                        _runUUT[iTimeNo].PLC.curTemp = (double)wVal[0] / 10;
                        break;
                    }
                }
                if (!readok)
                    return false;


                for (int i = 0; i < 3; i++)
                {
                    int[] wVal = new int[1];
                    for (int j = 0; j < 1; j++)
                    {
                        wVal[j] = 0;
                    }
                    if (!addREGRead(0, EPLCInput.温度点1, ref wVal))
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimeNo] + CLanguage.Lan("读取温度点1失败!"), udcRunLog.ELog.Err);
                    }
                    else
                    {
                        readok = true;
                        _runUUT[iTimeNo].PLC.TempPoint[0] = (double)wVal[0] / 10;
                        break;
                    }
                }
                if (!readok)
                    return false;

                for (int i = 0; i < 3; i++)
                {
                    int[] wVal = new int[1];
                    for (int j = 0; j < 1; j++)
                    {
                        wVal[j] = 0;
                    }
                    if (!addREGRead(0, EPLCInput.温度点2, ref wVal))
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimeNo] + CLanguage.Lan("读取温度点2失败!"), udcRunLog.ELog.Err);
                    }
                    else
                    {
                        readok = true;
                        _runUUT[iTimeNo].PLC.TempPoint[1] = (double)wVal[0] / 10;
                        break;
                    }
                }
                if (!readok)
                    return false;


                for (int i = 0; i < 3; i++)
                {
                    int[] wVal = new int[1];
                    for (int j = 0; j < 1; j++)
                    {
                        wVal[j] = 0;
                    }
                    if (!addREGRead(0, EPLCInput.温度点3, ref wVal))
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimeNo] + CLanguage.Lan("读取温度点3失败!"), udcRunLog.ELog.Err);
                    }
                    else
                    {
                        readok = true;
                        _runUUT[iTimeNo].PLC.TempPoint[2] = (double)wVal[0] / 10;
                        break;
                    }
                }
                if (!readok)
                    return false;


                for (int i = 0; i < 3; i++)
                {
                    int[] wVal = new int[1];
                    for (int j = 0; j < 1; j++)
                    {
                        wVal[j] = 0;
                    }
                    if (!addREGRead(0, EPLCInput.温度点4, ref wVal))
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimeNo] + CLanguage.Lan("读取温度点4失败!"), udcRunLog.ELog.Err);
                    }
                    else
                    {
                        readok = true;
                        _runUUT[iTimeNo].PLC.TempPoint[3] = (double)wVal[0] / 10;
                        break;
                    }
                }
                if (!readok)
                    return false;

                for (int i = 0; i < 3; i++)
                {
                    int[] wVal = new int[1];
                    for (int j = 0; j < 1; j++)
                    {
                        wVal[j] = 0;
                    }
                    if (!addREGRead(0, EPLCInput.温度点5, ref wVal))
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimeNo] + CLanguage.Lan("读取温度点5失败!"), udcRunLog.ELog.Err);
                    }
                    else
                    {
                        readok = true;
                        _runUUT[iTimeNo].PLC.TempPoint[4] = (double)wVal[0] / 10;
                        break;
                    }
                }

                for (int i = 0; i < 3; i++)
                {
                    int[] wVal = new int[1];
                    for (int j = 0; j < 1; j++)
                    {
                        wVal[j] = 0;
                    }
                    if (!addREGRead(0, EPLCInput.温度点6, ref wVal))
                    {
                        runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimeNo] + CLanguage.Lan("读取温度点5失败!"), udcRunLog.ELog.Err);
                    }
                    else
                    {
                        readok = true;
                        _runUUT[iTimeNo].PLC.TempPoint[5] = (double)wVal[0] / 10;
                        break;
                    }
                }

                if (!readok)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }

        /// <summary>
        /// 读取时间信号
        /// </summary>
        /// <param name="iTimeNo"></param>
        /// <returns></returns>
        private bool readplc_Time_Type(int iTimeNo)
        {
            try
            {

            
                if (!(_runUUT[iTimeNo].Para.DoRun == AgingRunType.运行 || _runUUT[iTimeNo].Para.DoRun == AgingRunType.暂停))
                    _runUUT[iTimeNo].Para.RunTime = 0;
                return true;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }

        /// <summary>
        /// 返回PLC输入名称
        /// </summary>
        /// <param name="inpIo"></param>
        /// <param name="idNo"></param>
        /// <returns></returns>
        private string InpPLC(EPLCInput inpIo, int idNo)
        {
            return ((EPLCInput)((int)inpIo + idNo)).ToString();
        }
        /// <summary>
        /// 返回PLC输出名称
        /// </summary>
        /// <param name="outIo"></param>
        /// <param name="idNo"></param>
        /// <returns></returns>
        private string OutPLC(EPLCOutput outIo, int idNo)
        {
            return ((EPLCOutput)((int)outIo + idNo)).ToString();
        }
        /// <summary>
        /// 读取PLC寄存器数据
        /// </summary>
        /// <param name="iTimeNo">时序好</param>
        /// <param name="inREG">输入信号</param>
        /// <param name="rVal">数据</param>
        /// <returns></returns>
        private bool addREGRead(int iTimeNo, EPLCInput inREG, ref int[] rVal)
        {
            try
            {
                _PLC_LOCK[getPLCcom(iTimeNo)].AcquireWriterLock(-1);

                string er = string.Empty;

                CTimer.WaitMs(10);               //设定输入状态
                GJ.DEV.PLC.ERegType type = (GJ.DEV.PLC.ERegType)getRegType(rREGAdrs[inREG][iTimeNo]);

                int Adrs = getRegAdrs(rREGAdrs[inREG][iTimeNo]);
                bool PlcOK = false;

                for (int i = 0; i < 3; i++)     //连续读取3次,成功退出循环,失败延时20MS再次读取
                {
                    PlcOK = _devPLC[getPLCcom(iTimeNo)].Read(getPLCAdrs(iTimeNo), type, Adrs, ref rVal, out er);

                    if (PlcOK == true)
                        break ;
                    else
                        CTimer.WaitMs(20);
                }
                if (PlcOK != true)
                {
                    runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimeNo] + CLanguage.Lan("读取地址:") + inREG + Adrs.ToString() + CLanguage.Lan("失败,Err:") + er, udcRunLog.ELog.Action);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimeNo] + CLanguage.Lan("读取地址:") + inREG + CLanguage.Lan("错误,Err:") + ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
            finally
            {
                _PLC_LOCK[getPLCcom(iTimeNo)].ReleaseWriterLock();
            }
        }

        /// <summary>
        /// 写入PLC寄存器数据
        /// </summary>
        /// <param name="iTimeNo"></param>
        /// <param name="outREG"></param>
        /// <param name="value">写入值</param>
        /// <returns></returns>
        private bool addREGWrite(int iTimeNo, EPLCOutput outREG, int[] wVal)
        {
            try
            {
                _PLC_LOCK[getPLCcom(iTimeNo)].AcquireWriterLock(-1);

                string er = string.Empty;

                CTimer.WaitMs(20);               //设定输入状态
                DEV.PLC.ERegType type = (DEV.PLC.ERegType)getRegType(wREGAdrs[outREG][iTimeNo]);

                int Adrs = getRegAdrs(wREGAdrs[outREG][iTimeNo]);

                bool PlcOK = false;

                for (int i = 0; i < 3; i++)     //连续读取3次,成功退出循环,失败延时20MS再次读取
                {
                    PlcOK = _devPLC[getPLCcom(iTimeNo)].Write(getPLCAdrs(iTimeNo), type, Adrs, wVal, out   er);

                    if (PlcOK == true)
                        break;
                    else
                        CTimer.WaitMs(20);
                }
                if (PlcOK != true)
                {
                    runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimeNo] + CLanguage.Lan("写入地址:") + outREG + Adrs.ToString() + CLanguage.Lan("失败,Err:") + er, udcRunLog.ELog.Action);
                    return false;
                }
                return true;
            }

            catch (Exception ex)
            {
                runLog.Log(CGlobalPara.SysPara.Para.timerNO[iTimeNo] + CLanguage.Lan("写入地址:") + outREG.ToString() + CLanguage.Lan("错误,Err:") + ex.ToString(), udcRunLog.ELog.Err);

                return false;
            }
            finally
            {
                _PLC_LOCK[getPLCcom(iTimeNo)].ReleaseWriterLock();
            }
        }

        #endregion

        #region 线程方法
        /// <summary>
        /// 老化时序负载切换
        /// </summary>
        /// <returns></returns>
        private bool Control_ONOFFLoad()
        {
            try
            {
                string er = string.Empty;

                for (int i = 0; i < CGlobalPara.C_Timer_MAX; i++)
                {
                    if (_runUUT[i].Para.DoRun != AgingRunType.运行)
                        continue; ;

                    if (_runUUT[i].Para.DoRun == AgingRunType.运行)
                    {
                        _runUUT[i].Para.RunTime = CTimer.DateDiff(_runUUT[i].Para.StartTime);
                    }
                    if (_runUUT[i].Para.RunTime >= _runUUT[i].Para.BurnTime) //老化完成
                    {
                        _runUUT[i].Para.DoRun = AgingRunType.空闲;
                        _runUUT[i].Para.DoONOFF = AgingONOFFType.OFF;
                        _runUUT[i].Para.DoData = AgingDataType.空闲;
                        _runUUT[i].Para.DoData = AgingDataType.结束报表;
                        _runUUT[i].Para.RunTime = _runUUT[i].Para.BurnTime;
                        if (CGlobalPara .SysPara.Mes.Connect)
                        {
                            runLog.Log(_runUUT[i].Para.TimerName + CLanguage.Lan("上传数据！"), udcRunLog.ELog.Action);

                            _runUUT[i].Mes.DoMes = AgingMesType.上传条码;

                            if (!TranMes(i, ref er))
                            {
                                runLog.Log("上传服务器错误：" + er +"请手动上传！" , udcRunLog.ELog.NG);
                            }
                            _runUUT[i].Mes.DoMes = AgingMesType.空闲;
                        }


                        StreamWriter sw = new StreamWriter(CGlobalPara.ResultFile, true, Encoding.GetEncoding("gb2312"));
                        string strWrite = string.Empty;
                        strWrite += _runUUT[i].Para.ModelName + ",";
                        strWrite += CGlobalPara.SysPara.Para.timerNO[i] + ",";
                        strWrite += _runUUT[i].Para.StartTime + ",";
                        strWrite += _runUUT[i].Para.EndTime + ",";
                        strWrite += _runUUT[i].Para.TTNum.ToString() + ",";
                        strWrite += _runUUT[i].Para.PassNum.ToString() + ",";
                        strWrite += (_runUUT[i].Para.TTNum - _runUUT[i].Para.PassNum).ToString() + ",";
                        strWrite += _runUUT[i].Para.SavePath + ",";
                        sw.WriteLine(strWrite);
                        sw.Flush();
                        sw.Close();
                        sw = null;
                    
                        updata_runPara_database(i);
                        uiRunStatus[i].SetRunStatus(_runUUT[i]);      //初始化界面
                        continue;
                    }

                    List<CONOFFSpec> OnOffList = new List<CONOFFSpec>();

                    for (int z = 0; z < _runUUT[i].Para.OnOffNum; z++)
                    {
                        CONOFFSpec OnOff = new CONOFFSpec()
                        {
                            inPutVolt = _runUUT[i].OnOff.OnOff[z].inPutVolt,
                            OnOffTime = _runUUT[i].OnOff.OnOff[z].OnOffTime,
                            OnTime = _runUUT[i].OnOff.OnOff[z].OnTime,
                            OffTime = _runUUT[i].OnOff.OnOff[z].OffTime,
                            outPutCur = _runUUT[i].OnOff.OnOff[z].outPutCur,
                            dcONOFF = _runUUT[i].OnOff.OnOff[z].dcONOFF,
                        };
                        OnOffList.Add(OnOff);
                    }

                    bool RunInput_Changed = false;

                    bool RunOutPut_Changed = false;

                    CRunTime CurOnOff = null;

                    if (CPara.GetCurStepFromOnOff(_runUUT[i].Para.RunTime, OnOffList, out CurOnOff, out er))    //获取现在运行的进度
                    {
                        if (CurOnOff.CurRunVolt != _runUUT[i].OnOff.TimeRun.CurRunVolt )   // || _runUUT[i].Para.iniSpec ==1)       //输入切换
                        {
                            RunInput_Changed = true;
                        }

                        if (CurOnOff.CurRunOutPut != _runUUT[i].OnOff.TimeRun.CurRunOutPut)// || _runUUT[i].Para.iniSpec == 1)     //负载切换
                        {
                            RunOutPut_Changed = true;
                        }
                        _runUUT[i].Para.iniSpec = 0;
                    }

                    if (RunInput_Changed)
                    {
                        _runUUT[i].Para.DoData = AgingDataType.空闲;
                        _runUUT[i].OnOff.TimeRun.CurRunVolt = CurOnOff.CurRunVolt;
                        _runUUT[i].OnOff.TimeRun.CurDCONOFF = CurOnOff.CurDCONOFF;
                        if (_runUUT[i].OnOff.TimeRun.CurRunVolt > 0 && _runUUT[i].Para.CtrlONOFF == 0)  //OFF-ON
                        {
                            //当前输入电压与设置电压不一致-->重启AC

                            if (_runUUT[i].Para.CtrlInPutVolt == 0)
                            {
                                _runUUT[i].Para.CtrlInPutVolt = _runUUT[i].OnOff.TimeRun.CurRunVolt;
                            }
                            else
                            {
                                if (_runUUT[i].Para.CtrlInPutVolt != _runUUT[i].OnOff.TimeRun.CurRunVolt)
                                {

                                    //当前输入电压与设置电压不一致-->重启AC
                                    setplc_ACVolt_Spec(i, CurOnOff.CurRunVolt);
                                    _runUUT[i].Para.CtrlInPutVolt = _runUUT[i].OnOff.TimeRun.CurRunVolt;
                                    Thread.Sleep(500);
                                }
                            }
                        

                            _runUUT[i].Para.DoONOFF = AgingONOFFType.ON;

                            _runUUT[i].Para.SaveCtrlTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                            _runUUT[i].Para.RunInVolt = _runUUT[i].OnOff.TimeRun.CurRunVolt;
                            _runUUT[i].Para.DCONOFF = _runUUT[i].OnOff.TimeRun.CurDCONOFF;

                            //runLog.Log(_runUUT[i].ToString() + "当前输入电压=" +
                            //            _runUUT[i].OnOff.TimeRun.CurRunVolt.ToString() + "V;检测不到ONOFF信号,重启ONOFF.",
                            //            udcRunLog.ELog.NG);

                        }
                        else if (_runUUT[i].OnOff.TimeRun.CurRunVolt == 0)
                        {
                            //OFF时检测到信号是开的
                            _runUUT[i].Para.SaveCtrlTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                            _runUUT[i].Para.DoONOFF = AgingONOFFType.OFF;
                            _runUUT[i].Para.RunInVolt = _runUUT[i].OnOff.TimeRun.CurRunVolt;
                            _runUUT[i].Para.DCONOFF = _runUUT[i].OnOff.TimeRun.CurDCONOFF;
                        }
                        else if (_runUUT[i].Para.CtrlInPutVolt > 0)  //切换电压?
                        {
                            double acv = Math.Abs(_runUUT[i].Para.CtrlInPutVolt - _runUUT[i].OnOff.TimeRun.CurRunVolt);

                            if (acv < 10) //电压偏差10-->存在电压切换
                                _runUUT[i].Para.bInVoltNum++;

                            if (_runUUT[i].Para.bInVoltNum > 3)
                            {
                                runLog.Log(_runUUT[i].ToString() + CLanguage.Lan("原先输入电压=") + _runUUT[i].Para.RunInVolt.ToString("0.0") +
                                            CLanguage.Lan("V;当前输入电压=") + _runUUT[i].Para.CtrlInPutVolt.ToString("0.0") + CLanguage.Lan("V,老化切换输入AC电压"),
                                            udcRunLog.ELog.Action);
                            }
                            else
                            {
                                _runUUT[i].Para.bInVoltNum++;
                            }
                        }
                        else
                        {
                            _runUUT[i].Para.bInVoltNum++;
                        }
                        updata_runPara_database(i);
                    }

                    _runUUT[i].Para.bInPutErrNum = 0;

                    _runUUT[i].Para.bInVoltNum = 0;

                    if (RunOutPut_Changed)
                    {
                        _runUUT[i].OnOff.TimeRun.CurRunOutPut = CurOnOff.CurRunOutPut;
                        _runUUT[i].Para.DoData = AgingDataType.空闲;
                        updata_outSpec_database(i);

                        _runUUT[i].Dev.DoEL = AgingELType.设定负载;
                        _runUUT[i].Para.SaveCtrlTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }

        /// <summary>
        /// 更新治具状态
        /// </summary>
        /// <returns></returns>
        private bool UpdateUUTStatus()
        {
            try
            {
                for (int i = 0; i < CGlobalPara.C_Timer_MAX; i++)
                {
                    switch (_runUUT[i].Para.DoRun)
                    {

                        case AgingRunType.自检:
                            if (CGlobalPara.C_SCAN_START) //扫描周期结束有效
                                break;

                            uut_bi_reStarting(i);
                            break;
                        case AgingRunType.运行:

                            if (CGlobalPara.C_SCAN_START) //扫描周期结束有效
                                break;

                            if (_runUUT[i].Para.RunInVolt == 0 || _runUUT[i].PLC.onoffStat == 0 || _runUUT[i].Para.SaveCtrlTime == "")
                                _runUUT[i].Para.SaveCtrlTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                            if ((_runUUT[i].Para.RunInVolt > 0 && _runUUT[i].PLC.onoffStat == 1))
                                if (CTimer.DateDiff(_runUUT[i].Para.SaveCtrlTime) > (CGlobalPara.SysPara.Report.FirstScan))
                                {
                                    runLog.Log(_runUUT[i].Para.TimerName + CLanguage.Lan("刷新结果"), udcRunLog.ELog.Action);
                                    uut_bi_runing(i);
                                }

                            break;
                        default:
                            break;
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }

        /// <summary>
        /// 老化运行中
        /// </summary>
        /// <param name="uutNo"></param>
        private void uut_bi_runing(int iTimer)
        {
            try
            {
                string er = string.Empty;
                AddCur(iTimer);
                int ttNum = 0;
                int PassNum = 0;

                for (int i = 0; i < _runUUT[iTimer].Led.Count / _runUUT[iTimer].Para.OutPutChan; i++)
                {
                    if (_runModel[iTimer].Para.ChanAdd != 1)
                    {
                        if (i%_runModel[iTimer].Para.ChanAdd != 0)
                        {
                            continue;
                        }
                    }
                    bool haveuut = false;
                    bool uutpass = true;
                    for (int j = 0; j < _runUUT[iTimer].Para.OutPutChan; j++)
                    {
                        int slot = i * _runUUT[iTimer].Para.OutPutChan + j;

                        if (_runUUT[iTimer].Led[slot].result != 0)
                        {
                            haveuut = true;
                            _runUUT[iTimer].Led[slot].unitV = _runUUT[iTimer].Led[slot].dataunitV;
                        }
                    }

                    if (haveuut)
                    {
                        uutpass = true;
                        for (int j = 0; j < _runUUT[iTimer].Para.OutPutChan; j++)
                        {
                            int slot = i * _runUUT[iTimer].Para.OutPutChan + j;
                            //if (_runUUT[iTimer].Led[slot].result != 0)
                            //{
                            if (_runUUT[iTimer].Led[slot].unitV > _runUUT[iTimer].Led[slot].vMax || _runUUT[iTimer].Led[slot].unitV < _runUUT[iTimer].Led[slot].vMin)
                            {
                                if (_runUUT[iTimer].Led[slot].unitV > _runUUT[iTimer].Led[slot].vMax * CGlobalPara.SysPara.Reg.VHP ||
                                    _runUUT[iTimer].Led[slot].unitV < _runUUT[iTimer].Led[slot].vMin * CGlobalPara.SysPara.Reg.VLP)
                                {
                                    if (_runUUT[iTimer].Led[slot].vFailNum < CGlobalPara.SysPara.Alarm.VoltFailTimes)
                                    {
                                        _runUUT[iTimer].Led[slot].vFailNum += 1;
                                        _runUUT[iTimer].Led[slot].unitV = _runUUT[iTimer].Led[slot].vMin + 0.1;
                                    }
                                    else
                                    {
                                        if (_runUUT[iTimer].Led[slot].unitV > _runUUT[iTimer].Led[slot].vMax)
                                        {
                                            _runUUT[iTimer].Led[slot].result = 3;
                                        }
                                        else if (_runUUT[iTimer].Led[slot].unitV < _runUUT[iTimer].Led[slot].vMin )
                                        {
                                            _runUUT[iTimer].Led[slot].result = 2;
                                        }
                                    
                                      
                                        if (_runUUT[iTimer].Led[slot].failEnd == 0)
                                        {
                                            _runUUT[iTimer].Led[slot].failEnd = 1;
                                            _runUUT[iTimer].Led[slot].failTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                                            _runUUT[iTimer].Led[slot].failInfo = CLanguage.Lan("电压:") + _runUUT[iTimer].Led[slot].unitV.ToString("0.000") + "V;" +
                                            CLanguage.Lan("电流:") + _runUUT[iTimer].Led[slot].unitA.ToString("0.00") + "A;";

                                            uut_upFail_Data(iTimer, slot);
                                        }
                                        uutpass = false;
                                    }
                                }
                                else
                                {
                                    _runUUT[iTimer].Led[slot].vFailNum = 0;
                                    _runUUT[iTimer].Led[slot].unitV = _runUUT[iTimer].Led[slot].vMin + 0.1;
                                }
                            }
                            else
                            {
                                _runUUT[iTimer].Led[slot].vFailNum = 0;
                                _runUUT[iTimer].Led[slot].vBack = _runUUT[iTimer].Led[slot].unitV;
                            }
                            if (CGlobalPara.SysPara.Reg.ChkNoJugdeCur)
                            {
                                if (_runUUT[iTimer].Led[slot].unitA > _runUUT[iTimer].Led[slot].Imax || _runUUT[iTimer].Led[slot].unitA < _runUUT[iTimer].Led[slot].Imin)
                                {
                                    if (_runUUT[iTimer].Led[slot].unitA > _runUUT[iTimer].Led[slot].Imax * CGlobalPara.SysPara.Reg.IHP ||
                                        _runUUT[iTimer].Led[slot].unitA < _runUUT[iTimer].Led[slot].Imin * CGlobalPara.SysPara.Reg.ILP)
                                    {
                                        if (_runUUT[iTimer].Led[slot].iFailNum < CGlobalPara.SysPara.Alarm.CurFailTimes)
                                        {
                                            _runUUT[iTimer].Led[slot].iFailNum += 1;
                                            _runUUT[iTimer].Led[slot].unitA = _runUUT[iTimer].Led[slot].ISet;
                                        }

                                        else
                                        {
                                            if (_runUUT[iTimer].Led[slot].unitA > _runUUT[iTimer].Led[slot].Imax)
                                            {
                                                _runUUT[iTimer].Led[slot].result = 5;
                                            }
                                            else
                                            {
                                                _runUUT[iTimer].Led[slot].result = 4;
                                            }
                                            if (_runUUT[iTimer].Led[slot].failEnd == 0)
                                            {
                                                _runUUT[iTimer].Led[slot].failEnd = 1;
                                                _runUUT[iTimer].Led[slot].failTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                                                _runUUT[iTimer].Led[slot].failInfo = CLanguage.Lan("电压:") + _runUUT[iTimer].Led[slot].unitV.ToString("0.000") + "V;" +
                                                CLanguage.Lan("电流:") + _runUUT[iTimer].Led[slot].unitA.ToString("0.00") + "A;";
                                                uut_upFail_Data(iTimer, slot);
                                            }
                                            uutpass = false;
                                        }
                                    }
                                    else
                                    {
                                        _runUUT[iTimer].Led[slot].iFailNum = 0;
                                        _runUUT[iTimer].Led[slot].unitA = _runUUT[iTimer].Led[slot].ISet;
                                    }
                                }
                                else
                                {
                                    _runUUT[iTimer].Led[slot].iFailNum = 0;
                                    _runUUT[iTimer].Led[slot].iBack = _runUUT[iTimer].Led[slot].unitA;
                                }
                            }
                            else
                            {
                                if (_runUUT[iTimer].Led[slot].unitA > _runUUT[iTimer].Led[slot].Imax || _runUUT[iTimer].Led[slot].unitA < _runUUT[iTimer].Led[slot].Imin)
                                    _runUUT[iTimer].Led[slot].unitA = _runUUT[iTimer].Led[slot].ISet;

                            }
                            if (uutpass)
                            {
                                if (_runUUT[iTimer].Led[slot].result == 0)
                                    _runUUT[iTimer].Led[slot].result = 1;
                                if (_runUUT[iTimer].Led[slot].result != 1 && _runUUT[iTimer].Led[slot].failEnd == 1 )
                                {
                                    _runUUT[iTimer].Led[slot].result = 6;
                                    uutpass = false;
                                }
                            }
                        }
                        // }
                        ttNum += 1;
                        if (uutpass)
                            PassNum += 1;
                    }
                }
                _runUUT[iTimer].Para.TTNum = ttNum;
                _runUUT[iTimer].Para.PassNum = PassNum;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
        }

        /// <summary>
        /// 自检老化中
        /// </summary>
        /// <param name="uutNo"></param>
        private void uut_bi_reStarting(int iTimer)
        {
            try
            {
                string er = string.Empty;
                AddCur(iTimer);
                for (int slot = 0; slot < _runUUT[iTimer].Led.Count; slot++)
                {

                    _runUUT[iTimer].Led[slot].result = 0;
                    _runUUT[iTimer].Led[slot].HaveCanId = false;
                    _runUUT[iTimer].Led[slot].vFailNum = 0;
                    _runUUT[iTimer].Led[slot].iFailNum = 0;
                    _runUUT[iTimer].Led[slot].failEnd = 0;
                    _runUUT[iTimer].Led[slot].failTime = "";
                    _runUUT[iTimer].Led[slot].failInfo = "";
                    _runUUT[iTimer].Led[slot].iicData = "";
                  
                }
                int ttNum = 0;
                int PassNum = 0;

                for (int i = 0; i < _runUUT[iTimer].Led.Count / _runUUT[iTimer].Para.OutPutChan; i++)
                {

                    bool haveuut = false;
                    bool uutpass = true;
                    for (int j = 0; j < _runUUT[iTimer].Para.OutPutChan; j++)
                    {
                        int slot = i * _runUUT[iTimer].Para.OutPutChan + j;
                        if (_runUUT[iTimer].Led[slot].vUse == 1)
                            if (_runUUT[iTimer].Led[slot].dataunitV >= CGlobalPara.SysPara.Reg.haveUUT || _runUUT[iTimer].Led[slot].barType == 1)
                            {
                                haveuut = true;
                                break;
                            }
                    }

                    if (haveuut)
                    {
                        for (int j = 0; j < _runUUT[iTimer].Para.OutPutChan; j++)
                        {
                            int slot = i * _runUUT[iTimer].Para.OutPutChan + j;

                            if (_runUUT[iTimer].Led[slot].vUse == 1)
                            {
                                _runUUT[iTimer].Led[slot].result = 1;
                                _runUUT[iTimer].Led[slot].unitV = _runUUT[iTimer].Led[slot].dataunitV;
                            }
                            else
                                _runUUT[iTimer].Led[slot].result = 0;

                            if (_runUUT[iTimer].Led[slot].result != 0)
                            {
                                //判断电压
                                if (_runUUT[iTimer].Led[slot].unitV > _runUUT[iTimer].Led[slot].vMax || _runUUT[iTimer].Led[slot].unitV < _runUUT[iTimer].Led[slot].vMin)

                                    if (_runUUT[iTimer].Led[slot].unitV > _runUUT[iTimer].Led[slot].vMax * CGlobalPara.SysPara.Reg.VHP || _runUUT[iTimer].Led[slot].unitV < _runUUT[iTimer].Led[slot].vMin * CGlobalPara.SysPara.Reg.VLP)
                                    {
                                        _runUUT[iTimer].Led[slot].result = 2;
                                        uutpass = false;
                                    }
                                    else
                                    {
                                        _runUUT[iTimer].Led[slot].unitV = _runUUT[iTimer].Led[slot].vMin + 0.1;
                                        _runUUT[iTimer].Led[slot].vBack = _runUUT[iTimer].Led[slot].unitV;
                                        _runUUT[iTimer].Led[slot].vFailNum = 0;
                                    }

                                else
                                {
                                    _runUUT[iTimer].Led[slot].vBack = _runUUT[iTimer].Led[slot].unitV;
                                    _runUUT[iTimer].Led[slot].vFailNum = 0;
                                }
                                //判断电流
                                if (CGlobalPara.SysPara.Reg.ChkNoJugdeCur)
                                {
                                    if (_runUUT[iTimer].Led[slot].unitA > _runUUT[iTimer].Led[slot].Imax || _runUUT[iTimer].Led[slot].unitA < _runUUT[iTimer].Led[slot].Imin)
                                        if (_runUUT[iTimer].Led[slot].unitA > _runUUT[iTimer].Led[slot].Imax * CGlobalPara.SysPara.Reg.IHP || _runUUT[iTimer].Led[slot].unitA < _runUUT[iTimer].Led[slot].Imin * CGlobalPara.SysPara.Reg.ILP)
                                        {
                                            _runUUT[iTimer].Led[slot].result = 2;
                                            uutpass = false;
                                        }
                                        else
                                        {
                                            _runUUT[iTimer].Led[slot].unitA = _runUUT[iTimer].Led[slot].ISet - 0.01;
                                            _runUUT[iTimer].Led[slot].iBack = _runUUT[iTimer].Led[slot].unitA;
                                            _runUUT[iTimer].Led[slot].iFailNum = 0;
                                        }

                                    else
                                    {
                                        _runUUT[iTimer].Led[slot].iBack = _runUUT[iTimer].Led[slot].unitA;
                                        _runUUT[iTimer].Led[slot].iFailNum = 0;
                                    }
                                }
                                else
                                    //不判断电流
                                    if (_runUUT[iTimer].Led[slot].unitA > _runUUT[iTimer].Led[slot].Imax || _runUUT[iTimer].Led[slot].unitA < _runUUT[iTimer].Led[slot].Imin)
                                        _runUUT[iTimer].Led[slot].unitA = _runUUT[iTimer].Led[slot].ISet;
                            }
                        }
                        ttNum += 1;
                        if (uutpass)
                            PassNum += 1;
                    }
                }

                _runUUT[iTimer].Para.TTNum = ttNum;
                _runUUT[iTimer].Para.PassNum = PassNum;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
        }


        /// <summary>
        /// 加总电流
        /// </summary>
        /// <param name="iTimer"></param>
        private void AddCur(int iTimer)
        {
            try
            {

                ///判定是否有产品
                for (int i = 0; i < _runUUT[iTimer].Led.Count / _runModel[iTimer].Para.ChanAdd; i++)
                {
                    double loadCur = 0;
                    int slot = 0;
                    for (int k = 0; k < _runModel[iTimer].Para.ChanAdd; k++)
                    {
                        slot = i * _runModel[iTimer].Para.ChanAdd + k;

                        loadCur += _runUUT[iTimer].Led[slot].dataunitA;
                    }

                    slot = i * _runModel[iTimer].Para.ChanAdd;

                    ///更细通道号和电流通道号一致的电流数据 
                    _runUUT[iTimer].Led[slot].unitA = loadCur;

                    if (_runUUT[iTimer].Led[slot].unitA < 0 || _runUUT[iTimer].Led[slot].dataunitV < 2)
                        _runUUT[iTimer].Led[slot].unitA = 0.01;

                    for (int k = 1; k < _runModel[iTimer].Para.ChanAdd; k++)
                    {

                        slot = i * _runModel[iTimer].Para.ChanAdd + k;
                        ///更细通道号和电流通道号一致的电流数据 

                        _runUUT[iTimer].Led[slot].unitA = 0;
                        _runUUT[iTimer].Led[slot].unitV = 0;
                    }

                }
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.NG);
            }
        }
        #endregion

        #region 线程消息
        private void OnPLCConArgs(object sender, CPLCConArgs e)
        {
            try
            {

                if (e.e == EMessage.异常)
                    runLog.Log(e.status, udcRunLog.ELog.NG);
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
        }
        private void OnPLCDataArgs(object sender, CPLCDataArgs e)
        {
            try
            {
                //检测PLC通信是否断开?

                if (e.e == EMessage.异常)
                    runLog.Log(e.rData, udcRunLog.ELog.NG);
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
        }
        private void OnMonConArgs(object sender, GJ.DEV.FCMB.CConArgs e)
        {
            if (e.bErr)
                runLog.Log(e.conStatus, udcRunLog.ELog.NG);
        }
        private void OnMonDataArgs(object sender, GJ.DEV.FCMB.CDataArgs e)
        {
            if (CGlobalPara.SysPara.Alarm.NoShowAlarm)
                return;
            if (e.bErr)
                runLog.Log(e.rData, udcRunLog.ELog.NG);
        }
        private void OnDAConArgs(object sender, GJ.DEV.LED.CConArgs e)
        {
            if (e.bErr)
                runLog.Log(e.conStatus, udcRunLog.ELog.NG);
        }
        private void OnDADataArgs(object sender, GJ.DEV.LED.CDataArgs e)
        {
            if (CGlobalPara.SysPara.Alarm.NoShowAlarm)
                return;
            if (e.bErr)
                runLog.Log(e.rData, udcRunLog.ELog.NG);
        }
        #endregion

        #region 主线程
        private void MainWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            runLog.Log(CLanguage.Lan("主监控线程开始"), udcRunLog.ELog.Action);

            scanWather.Restart();

            while (true)
            {
                try
                {
                    if (MainWorker.CancellationPending)
                        return;

                    int delayMs = 1000;// CGlobalPara.SysPara.Report.MonInterval;
                    
                    CTimer.WaitMs(delayMs);

                    scanWather.Stop();

                    updateScanTime(scanWather.ElapsedMilliseconds);

                    scanWather.Restart();

                    string er = string.Empty;

                    if (!Control_ONOFFLoad())
                    {
                        CTimer.WaitMs(CGlobalPara.C_ALARM_DELAY);
                        continue;
                    }
                    
                    if (!RefreshSignal())
                    {
                        CTimer.WaitMs(CGlobalPara.C_ALARM_DELAY);
                        continue;
                    }

                    if (!_uiWatcher.IsRunning)
                        _uiWatcher.Restart();

                    if (_uiWatcher.ElapsedMilliseconds > CGlobalPara.SysPara.Report.MonInterval * 1000)
                    {
                        _uiWatcher.Restart();
                        
                        if (!UpdateUUTStatus())
                        {
                            CTimer.WaitMs(CGlobalPara.C_ALARM_DELAY);
                            continue;
                        }
                        
                        if (!UpdateUUTUI())
                        {
                            CTimer.WaitMs(CGlobalPara.C_ALARM_DELAY);
                            continue;
                        }
                           
                        if (!SaveBIReport())
                        {
                            CTimer.WaitMs(CGlobalPara.C_ALARM_DELAY);
                            continue;
                        }
                        
                    }

                }
                catch (Exception ex)
                {
                    runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                    CTimer.WaitMs(CGlobalPara.C_ALARM_DELAY);
                    continue;
                }
            }
        }
        private void MainWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            runLog.Log(CLanguage.Lan("主监控线程销毁"), udcRunLog.ELog.NG);
        }
        #endregion

        #region UI界面刷新

        /// <summary>
        /// 刷新扫描时间
        /// </summary>
        /// <param name="mTime"></param>
        private void updateScanTime(long mTime)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action<long>(updateScanTime), mTime);
            else
            {
                if (scanCount >= 100)
                    scanCount = 0;
                else
                    scanCount++;
                // runLog.Log ("监控时间(ms):" + mTime.ToString() + ":" + scanCount.ToString("D2"),udcRunLog.ELog.Content );
            }
        }

        /// <summary>
        /// 刷新PLC读取信号
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool RefreshSignal()
        {
            try
            {

                updateSignalUI();
                return true;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }

        /// <summary>
        /// 刷新动作状态显示
        /// </summary>
        private void updateSignalUI()
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(updateSignalUI));
            else
            {
                string[] Str1 = _runUUT[0].PLC.alarmStat .Split(',');
                if (Str1.Length > 42)
                {
                    for (int i = 0; i < 24; i++)
                    {
                        lblStatus[i].Text = Convert.ToInt16(Str1[i]) == 0 ? "正常" : "异常";
                        lblStatus[i].ForeColor = Convert.ToInt16(Str1[i]) == 0 ? Color.Lime : Color.Red;
                    }
                    for (int i = 24; i < 36; i++)
                    {
                        lblStatus[i].Text = Convert.ToDouble(Str1[i]).ToString("F1");
                        lblStatus[i].ForeColor = Color.Lime;
                    }
                    for (int i = 36; i < 48; i++)
                    {
                        lblStatus[i].Text = Convert.ToInt16(Str1[i]) == 0 ? "正常" : "异常";
                        lblStatus[i].ForeColor = Convert.ToInt16(Str1[i]) == 0 ? Color.Lime : Color.Red;
                    }
                }

                for (int i = 0; i < CGlobalPara.C_Timer_MAX; i++)
                {
                    if (_runUUT[i].Para.DoRun == AgingRunType.自检 || _runUUT[i].Para.DoRun == AgingRunType.运行)
                    {
                        switch (_runUUT[i].Para.DoRun)
                        {
                            case AgingRunType.空闲:
                                lblRunType[i].Text = CLanguage.Lan("空闲中");
                                lblRunType[i].ForeColor = Color.Olive;
                                break;
                            case AgingRunType.扫条码:
                                lblRunType[i].Text = CLanguage.Lan("扫条码");
                                lblRunType[i].ForeColor = Color.Aqua;
                                break;
                            case AgingRunType.运行:
                                lblRunType[i].Text = CLanguage.Lan("运行中");
                                lblRunType[i].ForeColor = Color.Green;
                                break;
                            case AgingRunType.暂停:
                                lblRunType[i].Text = CLanguage.Lan("暂停中");
                                lblRunType[i].ForeColor = Color.Blue;
                                break;
                            case AgingRunType.自检:
                                lblRunType[i].Text = CLanguage.Lan("自检中");
                                lblRunType[i].ForeColor = Color.Magenta;
                                break;
                            default:
                                lblRunType[i].Text = CLanguage.Lan("异常");
                                lblRunType[i].ForeColor = Color.Red;
                                break;
                        }

                        if (_runUUT[i].Para.TTNum == _runUUT[i].Para.PassNum)
                        {
                            lblUUTStatus[i].Text = CLanguage.Lan("正常");
                            lblUUTStatus[i].ForeColor = Color.LimeGreen;
                        }
                        else
                        {
                            lblUUTStatus[i].Text = CLanguage.Lan("异常");
                            lblUUTStatus[i].ForeColor = Color.Red;
                        }
                        proRunProgress[i].Maximum  = _runUUT[i].Para.BurnTime ;
                        proRunProgress[i].Value = _runUUT[i].Para.RunTime;
                        if( _runUUT[i].Para.BurnTime!=0)
                        {
                        double runProgress = ((double)_runUUT[i].Para.RunTime) / ((double)_runUUT[i].Para.BurnTime);
                        lblRunProgress[i].Text = (runProgress * 100).ToString("F1") + "%";
                        }
                        else
                            lblRunProgress[i].Text =  "100%";
                        if (_runUUT[i].Para.TTNum != 0)
                        {
                            double uutProgress = ((double)_runUUT[i].Para.PassNum) / ((double)_runUUT[i].Para.TTNum);
                            lblUUTProgress[i].Text = (uutProgress * 100).ToString("F1") + "%";
                        }
                        else
                            lblUUTProgress[i].Text = "100%";
                        uiRunStatus[i].SetRunStatus(_runUUT[i]);      //初始化界面

                        if (_runUUT[i].Para.SaveTempTime == "")
                            _runUUT[i].Para.SaveTempTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                        if (CTimer.DateDiff(_runUUT[i].Para.SaveTempTime) > 5 && _runUUT[i].Para.DoRun == AgingRunType.运行)
                        {
                            if (CTimer.DateDiff(_runUUT[i].Para.SaveTempTime) >= (_runUUT[i].Para.BurnTime / 3000) && CTimer.DateDiff(_runUUT[i].Para.SaveTempTime) >= 60)
                            {
                                //runLog.Log("更新温度曲线", udcRunLog.ELog.NG);
                                RefTempChart(i);
                                //runLog.Log("更新温度曲线完成", udcRunLog.ELog.NG);
                                _runUUT[i].Para.SaveTempTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                            }
                        }

                    }
                    else
                    {

                        lblRunType[i].Text = CLanguage.Lan("空闲中");
                        lblRunType[i].ForeColor = Color.Olive;

                        lblUUTStatus[i].Text = CLanguage.Lan("关闭");
                        lblUUTStatus[i].ForeColor = Color.DarkRed;

                        lblUUTProgress[i].Text = "100%";
                        lblRunProgress[i].Text = "100%";
                        proRunProgress[i].Value = proRunProgress[i].Maximum;
                        uiRunStatus[i].SetRunStatus(_runUUT[i]);      //初始化界面
                    }

                }
            }
        }

        /// <summary>
        /// 更新槽位UI显示
        /// </summary>
        /// <returns></returns>
        private bool UpdateUUTUI()
        {
            try
            {
                for (int iTimer = 0; iTimer < CGlobalPara.C_Timer_MAX; iTimer++)
                {
                    if (_runUUT[iTimer].Para.DoRun == AgingRunType.运行 || _runUUT[iTimer].Para.DoRun == AgingRunType.自检)
                    {

                        if (_runUUT[iTimer].Para.RunInVolt == 0 || _runUUT[iTimer].PLC.onoffStat == 0 || _runUUT[iTimer].Para.SaveCtrlTime == "")
                        {
                            _runUUT[iTimer].Para.SaveCtrlTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                            continue;
                        }

                        if ((_runUUT[iTimer].Para.RunInVolt > 0 && _runUUT[iTimer].PLC.onoffStat == 1))
                        {
                            if (CTimer.DateDiff(_runUUT[iTimer].Para.SaveCtrlTime) < (CGlobalPara.SysPara.Report.FirstScan + 2))
                                continue;
                        }
                        else
                            continue;

                        runLog.Log(_runUUT[iTimer].Para.TimerName + CLanguage.Lan("更新UI"), udcRunLog.ELog.Action);

                        for (int iuutNo = 0; iuutNo < CGlobalPara.C_Board_MAX; iuutNo++)
                        {

                            uiUUT[iuutNo + iTimer * CGlobalPara.C_Board_MAX].SetUUT(_runUUT[iTimer], iuutNo);

                            //for (int j = 0; j < _runUUT[iTimer].Led.Count; j++)
                            //{
                            //    if (_runUUT[iTimer].Led[j].barType == 1)
                            //        lblUUTConTitle[iTimer * CGlobalPara.C_UUT_MAX + _runUUT[iTimer].Led[j].barNo].ForeColor = Color.Blue;
                            //}
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }

        /// <summary>
        /// 保存测试报表
        /// </summary>
        /// <returns></returns>
        private bool SaveBIReport()
        {
            try
            {
                if (!CGlobalPara.SysPara.Report.SaveReport)
                    return true;
                for (int iTimer = 0; iTimer < CGlobalPara.C_Timer_MAX; iTimer++)
                {

                    if (_runUUT[iTimer].Para.DoRun != AgingRunType.运行)
                        continue;

                    if (_runUUT[iTimer].Para.SaveCtrlTime == "")
                        _runUUT[iTimer].Para.SaveCtrlTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                    if (CTimer.DateDiff(_runUUT[iTimer].Para.SaveCtrlTime) < (CGlobalPara.SysPara.Report.FirstScan + 5))
                        continue;

                    if (_runUUT[iTimer].OnOff.TimeRun.CurRunVolt == 0 || _runUUT[iTimer].PLC.onoffStat == 0)
                    {
                        continue;
                    }
                    if (_runUUT[iTimer].Para.SaveDataTime == "")
                    {
                        _runUUT[iTimer].Para.SaveDataTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                        continue;
                    }

                    if (_runUUT[iTimer].Para.DoData == AgingDataType.空闲)
                        _runUUT[iTimer].Para.DoData = AgingDataType.保存报表;

                }
                return true;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }

        private bool UpdateFailUI()
        {
            try
            {
                int FailNo = 0;
                dgvFailData.Rows.Clear();
                dgvFailData.ForeColor = Color.Red;
                for (int iTimer = 0; iTimer < CGlobalPara.C_Timer_MAX; iTimer++)
                {
                    if (_runUUT[iTimer].Para.DoRun == AgingRunType.运行 || _runUUT[iTimer].Para.DoRun == AgingRunType.自检)
                    {
                        for (int i = 0; i < _runUUT[iTimer].Led.Count; i++)
                        {
                            if (_runUUT[iTimer].Led[i].result == 2 || _runUUT[iTimer].Led[i].result == 3)
                            {
                                FailNo += 1;
                                dgvFailData.Rows.Add((FailNo), CGlobalPara.SysPara.Para.timerNO[iTimer] + "_L" +
                                                             _runUUT[iTimer].Led[i].iLayer.ToString () + "_" +
                                                             _runUUT[iTimer].Led[i].iUUT.ToString("D2") + "_" +
                                                             _runUUT[iTimer].Led[i].iCH.ToString("D2"),
                                                             _runUUT[iTimer].Led[i].failTime + "_" + _runUUT[iTimer].Led[i].failInfo);
                            }
                        }

                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }

        #endregion

        #region 测试报表

        #region 线程变量
        /// <summary>
        /// 数据保存线程
        /// </summary>
        private List<Thread> _threadReport = new List<Thread>();

        /// <summary>
        /// 数据保存线程状态
        /// </summary>
        private volatile List<bool> _threadReportCancel = new List<bool>();
        #endregion

        #region 线程控制
        /// <summary>
        /// 数据保存线程
        /// </summary>
        private void OnReportStart()
        {
            int threadNo = System.Convert.ToInt16(Thread.CurrentThread.Name.Substring(0, 2));       //获取当前线程的ID
            try
            {
                while (true)
                {
                    try
                    {
                        string er = string.Empty;

                        if (_threadReportCancel[threadNo])
                            return;

                        int iTimer = threadNo;  //时序号

                        if (_runUUT[iTimer].Para.DoData == AgingDataType.结束报表)
                        {
                            if (CGlobalPara.SysPara.Report.SaveExcel)
                                uut_save_endreport_excel(iTimer);
                            if (_runUUT[iTimer].Para.DoData != AgingDataType.空闲)
                                _runUUT[iTimer].Para.DoData = AgingDataType.空闲;
                        }
                        if (_runUUT[iTimer].Para.DoRun == AgingRunType.运行 || _runUUT[iTimer].Para.DoRun == AgingRunType.自检)
                        {

                            if (_runUUT[iTimer].Para.DoData == AgingDataType.保存报表)
                            {
                                updata_runData_database(iTimer);

                                bool needSave = false;

                                if (_runUUT[iTimer].Para.TTNum == _runUUT[iTimer].Para.PassNum)
                                {
                                    if (CTimer.DateDiff(_runUUT[iTimer].Para.SaveDataTime) < CGlobalPara.SysPara.Report.SaveReportTimes)
                                        continue;
                                    needSave = true;
                                }
                                if (_runUUT[iTimer].Para.TTNum != _runUUT[iTimer].Para.PassNum)
                                    needSave = true;

                                if (needSave)
                                    if (CGlobalPara.SysPara.Report.SaveExcel)
                                        uut_save_report_excel(iTimer);
                                    else
                                        uut_save_report_csv_test(iTimer);
                                if (_runUUT[iTimer].Para.DoData != AgingDataType.空闲)
                                    _runUUT[iTimer].Para.DoData = AgingDataType.空闲;
                            }


                        }
                        CTimer.WaitMs(1000);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
            finally
            {
                _threadReportCancel[threadNo] = false;
                runLog.Log(CLanguage.Lan("数据保存线程" + threadNo.ToString() + "关闭"), udcRunLog.ELog.Content);
            }
        }
        #endregion

        #region 报表存储方法

        /// <summary>
        /// 保存测试报表(Excel格式)
        /// </summary>
        /// <param name="uutNo"></param>
        private void uut_save_report_excel(int iTimer)
        {
            string er = string.Empty;
            try
            {

                runLog.Log(_runUUT[iTimer].Para.TimerName + CLanguage.Lan("_保存报表"), udcRunLog.ELog.Action);
                if (_runUUT[iTimer].Para.TTNum == _runUUT[iTimer].Para.PassNum)
                {
                    _runUUT[iTimer].Para.SaveDataTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                }
                else
                    _runUUT[iTimer].Para.SaveDataTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");


                bool CanSave = true;
                if (!File.Exists(_runUUT[iTimer].Para.SavePath))           //判断文件是否存在  
                    if (!uut_ini_report_excel(iTimer))
                        CanSave = false;
                if (CanSave)
                {
                    _runUUT[iTimer].Para.SaveDataIndex += 1;
                    string[] sData = new string[4];
                    sData[0] = "";
                    for (int i = 1; i < 8; i++)
                    {
                        sData[i] += "L" + i.ToString() + ",";
                        sData[i] += _runUUT[iTimer].Para.SaveDataIndex + ",";                                   //表单名称
                        sData[i] += "'" + _runUUT[iTimer].Para.SaveDataTime + ",";                                  //表单名称
                        sData[i] += _runUUT[iTimer].PLC.LTempPoint[0] + ",";
                        sData[i] += _runUUT[iTimer].OnOff.TimeRun.CurRunVolt + ",";
                        for (int j = 0; j < 24; j++)
                        {
                            int solt = (i - 1) * 24 + j;
                            if (_runUUT[iTimer].Led[solt].result != 0)
                            {
                                sData[i] += _runUUT[iTimer].Led[solt].unitV.ToString("F2") + "," +
                                            _runUUT[iTimer].Led[solt].vMin.ToString("F2") + "," +
                                            _runUUT[iTimer].Led[solt].vMax.ToString("F2") + "," +
                                            _runUUT[iTimer].Led[solt].unitA.ToString("F2") + "," +
                                            _runUUT[iTimer].Led[solt].Imin.ToString("F2") + "," +
                                            _runUUT[iTimer].Led[solt].Imax.ToString("F2") + ",";
                            }
                            else
                                sData[i] += "--,--,--,--,--,--,";

                        }
                    }
                    if (_runUUT[iTimer].OnOff.TimeRun.CurRunVolt > 0)
                    {
                        if (CReport.Save_To_Excel(_runUUT[iTimer].Para.SavePath, sData, out er))
                        {
                            uut_save_report_Path(iTimer);
                            uut_save_report_timer(iTimer);
                        }
                        else
                            runLog.Log(_runUUT[iTimer].Para.TimerName + CLanguage.Lan("_保存报表失败:") + er, udcRunLog.ELog.Err);
                    }
                    else
                        _runUUT[iTimer].Para.SaveDataIndex -= 1;
                }

            }
            catch (Exception ex)
            {
                runLog.Log(_runUUT[iTimer].Para.TimerName + CLanguage.Lan("_保存报表失败:") + ex.ToString(), udcRunLog.ELog.Err);
            }
        }

        /// <summary>
        /// 保存测试报表(Excel格式)
        /// </summary>
        /// <param name="uutNo"></param>
        private void uut_save_endreport_excel(int iTimer)
        {
            string er = string.Empty;
            try
            {

                runLog.Log(_runUUT[iTimer].Para.TimerName + CLanguage.Lan("_保存结束报表"), udcRunLog.ELog.Action);

                bool CanSave = true;
                if (!File.Exists(_runUUT[iTimer].Para.SavePath))           //判断文件是否存在  
                    if (!uut_ini_report_excel(iTimer))
                        CanSave = false;
                if (CanSave)
                {
                    _runUUT[iTimer].Para.SaveDataIndex += 1;
                    string[] sData = new string[1];
                    sData[0] += "Gerneral Info,";
                    sData[0] += DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + ",";
                    sData[0] += _runUUT[iTimer].Para.PassNum.ToString() + ",";
                    sData[0] += (_runUUT[iTimer].Para.TTNum - _runUUT[iTimer].Para.PassNum).ToString() + ",";
                    for (int i = 0; i < _runUUT[iTimer].Led.Count; i++)
                    {

                        if (_runUUT[iTimer].Led[i].result != 0)
                        {
                            sData[0] += CGlobalPara.SysPara.Para.timerNO[iTimer] + "_L" +
                                        _runUUT[iTimer].Led[i].iLayer.ToString() + "_" +
                                        _runUUT[iTimer].Led[i].iUUT.ToString("D2") + "_" +
                                        _runUUT[iTimer].Led[i].iCH.ToString("D2") + ",";

                            if (_runUUT[iTimer].Led[i].result == 1 )
                            {
                                sData[0] += "Pass,,,";
                            }
                            else
                            {
                                sData[0] += "NG,";
                                sData[0] += _runUUT[iTimer].Led[i].failTime + ",";
                                sData[0] += _runUUT[iTimer].Led[i].failInfo + ",";
                            }
                        }
                    }

                    if (CReport.End_To_Excel(_runUUT[iTimer].Para.SavePath, sData, out er))
                    {
                        uut_save_report_Path(iTimer);
                        uut_save_report_timer(iTimer);
                    }
                    else
                        runLog.Log(_runUUT[iTimer].Para.TimerName + CLanguage.Lan("_保存结束报表失败:") + er, udcRunLog.ELog.Err);

                }

            }
            catch (Exception ex)
            {
                runLog.Log(_runUUT[iTimer].Para.TimerName + CLanguage.Lan("_保存结束报表失败:") + ex.ToString(), udcRunLog.ELog.Err);
            }
        }

        /// <summary>
        /// 保存测试报表(CSV格式)
        /// </summary>
        /// <param name="uutNo"></param>
        private bool uut_ini_report_excel(int iTimer)
        {
            try
            {
                string er = string.Empty;

                runLog.Log(_runUUT[iTimer].Para.TimerName + CLanguage.Lan("_创建报表"), udcRunLog.ELog.Action);

                string SavePath = CGlobalPara.SysPara.Report.ReportPath + "\\" + _runUUT[iTimer].Para.TimerName + "\\" +
                                  _runUUT[iTimer].Para.ModelName + "\\" + DateTime.Now.ToString("yyyy_MM_dd");

                if (!Directory.Exists(SavePath))
                    Directory.CreateDirectory(SavePath);           //创建文件夹


                _runUUT[iTimer].Para.SavePath = SavePath + "\\" + _runUUT[iTimer].Para.ModelName + "_"
                                  + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (!File.Exists(CGlobalPara.ExcelFile))                                //查询样板文件状态
                {
                    runLog.Log(GJ.COM.CLanguage.Lan("找不到报表样板") + "[" + CGlobalPara.ExcelFile + "]", udcRunLog.ELog.Err);
                    return false;
                }

                //复制样板
                File.Copy(CGlobalPara.ExcelFile, _runUUT[iTimer].Para.SavePath, true);
                string[] sData = new string[4];

                sData[0] += "Gerneral Info,";                                               //表单名称
                sData[0] += _runUUT[iTimer].Para.ModelName + ",";                           //机种名称
                sData[0] += _runUUT[iTimer].Para.MO_NO + ",";                               //任务令
                sData[0] += CGlobalPara.SysPara.Para.timerNO[iTimer] + ",";                 //老化架编号
                sData[0] += _runUUT[iTimer].Para.TTNum + ",";                               //实际老化总数
                sData[0] += "'" + _runUUT[iTimer].Para.StartTime + ",";                     //开始时间
                sData[0] += (_runUUT[iTimer].Para.BurnTime / 3600).ToString("F1") + "H,";   //老化时间(H)
                sData[0] += _runUUT[iTimer].Para.UserName + ",";                            //操作员代码

                for (int i = 1; i < 8; i++)
                {
                    sData[i] += "L" + i.ToString() + ",";                                   //表单名称
                    for (int j = 0; j < 24; j++)
                    {
                        int solt = (i - 1) * 24 + j;
                        sData[i] += CGlobalPara.SysPara.Para.timerNO[iTimer] + "_L" +
                                    _runUUT[iTimer].Led[solt].iLayer.ToString("D2") + "_" +
                                    _runUUT[iTimer].Led[solt].iUUT.ToString("D2") + "_" +
                                    _runUUT[iTimer].Led[solt].iCH.ToString("D2") + ",";
                        sData[i] += "'" + (_runUUT[iTimer].Led[solt].serialNo == "" ? "NA"
                                    : _runUUT[iTimer].Led[solt].serialNo) + ",";
                    }
                }
                _runUUT[iTimer].Para.SaveDataIndex = 0;
                if (CReport.Ini_To_Excel(_runUUT[iTimer].Para.SavePath, sData, out er))
                    return true;
                else
                {
                    runLog.Log(_runUUT[iTimer].Para.TimerName + CLanguage.Lan("_创建报表失败:") + er, udcRunLog.ELog.Err);
                    return false;
                }
            }
            catch (Exception ex)
            {
                runLog.Log(_runUUT[iTimer].Para.TimerName + CLanguage.Lan("_创建报表失败:") + ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }

        /// <summary>
        /// 保存测试报表(CSV格式)
        /// </summary>
        /// <param name="uutNo"></param>
        private void uut_save_report_csv(int iTimer)
        {
            try
            {

                runLog.Log(_runUUT[iTimer].Para.TimerName + CLanguage.Lan("_保存报表"), udcRunLog.ELog.Action);
                if (_runUUT[iTimer].Para.TTNum == _runUUT[iTimer].Para.PassNum)
                {
                    _runUUT[iTimer].Para.SaveDataTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                }
                else
                    _runUUT[iTimer].Para.SaveDataTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                if (!Directory.Exists(_runUUT[iTimer].Para.SavePath))
                    Directory.CreateDirectory(_runUUT[iTimer].Para.SavePath);

                bool upDb = false;
                for (int iuutNo = 0; iuutNo < _runUUT[iTimer].Led.Count / _runUUT[iTimer].Para.OutPutChan; iuutNo++)
                {
                    string filePath = string.Empty;
                    bool savefilePath = false;
                    bool haveuut = false;
                    bool uutpass = true;
                    bool havebar = false;
                    string barNo = string.Empty;
                    string local = string.Empty;
                    int canNo = 0;
                    for (int j = 0; j < _runUUT[iTimer].Para.OutPutChan; j++)
                    {
                        int slot = iuutNo * _runUUT[iTimer].Para.OutPutChan + j;

                        if (_runUUT[iTimer].Led[slot].iCH == 1)
                        {
                            local = _runUUT[iTimer].Led[slot].localPath;

                            if (_runUUT[iTimer].Led[slot].barType == 1 && _runUUT[iTimer].Led[slot].serialNo != "")
                            {

                                havebar = true;
                                barNo = _runUUT[iTimer].Led[slot].serialNo;
                            }
                        }
                        if (_runUUT[iTimer].Led[slot].result > 0)
                        {
                            haveuut = true;
                            canNo = slot;
                            if (!savefilePath)
                            {
                                filePath = _runUUT[iTimer].Led[slot].reportPath;
                                if (_runUUT[iTimer].Led[slot].reportPath == "")
                                {
                                    if (havebar)
                                        filePath = _runUUT[iTimer].Para.SavePath + "\\" + barNo + "_" + local + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
                                    else
                                        filePath = _runUUT[iTimer].Para.SavePath + "\\" + local + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";

                                }
                                _runUUT[iTimer].Led[slot].reportPath = filePath;

                                savefilePath = true;

                            }
                            else
                                _runUUT[iTimer].Led[slot].reportPath = filePath;

                            if (_runUUT[iTimer].Led[slot].result == 2 || _runUUT[iTimer].Led[slot].result == 3 || _runUUT[iTimer].Led[slot].result == 5)
                                uutpass = false;
                        }
                    }

                    if (haveuut)
                    {


                        bool IsExist = true;

                        if (!File.Exists(filePath))
                            IsExist = false;

                        StreamWriter sw = new StreamWriter(filePath, true, Encoding.UTF8);

                        string strWrite = string.Empty;

                        string strTemp = string.Empty;

                        //写入标题栏
                        if (!IsExist)
                        {

                            upDb = true;
                            strWrite = "Model Name:," + _runUUT[iTimer].Para.ModelName;
                            sw.WriteLine(strWrite);
                            strWrite = "Location:," + _runUUT[iTimer].Para.TimerName;
                            sw.WriteLine(strWrite);

                            if (havebar)
                            {
                                strWrite = "SerialNo:," + barNo;
                                sw.WriteLine(strWrite);
                            }

                            strWrite = "StartTime:," + _runUUT[iTimer].Para.StartTime;
                            sw.WriteLine(strWrite);
                            DateTime endTime = (System.Convert.ToDateTime(_runUUT[iTimer].Para.StartTime)).AddSeconds(_runUUT[iTimer].Para.BurnTime);
                            strWrite = "EndTime:," + endTime.ToString("yyyy/MM/dd HH:mm:ss");
                            sw.WriteLine(strWrite);
                            strWrite = "BurnTime(H):," + (_runUUT[iTimer].Para.BurnTime / 3600).ToString("0.0");
                            sw.WriteLine(strWrite);

                            strWrite = string.Empty;
                            strTemp = "Scan Time,Temp.(℃),AC Volt(V),";

                            strWrite += strTemp;

                            for (int j = 0; j < _runUUT[iTimer].Para.OutPutChan; j++)
                            {
                                int slot = iuutNo * _runUUT[iTimer].Para.OutPutChan + j;
                                if (_runUUT[iTimer].Led[slot].result > 0)
                                {

                                    strTemp = "CH" + (j + 1).ToString("D2") + "_LowLimit(V)" + ",";
                                    strWrite += strTemp;
                                    strTemp = "CH" + (j + 1).ToString("D2") + "_Voltage(V)" + ",";
                                    strWrite += strTemp;
                                    strTemp = "CH" + (j + 1).ToString("D2") + "_HILimit(V)" + ",";
                                    strWrite += strTemp;
                                    strTemp = "CH" + (j + 1).ToString("D2") + "_LowLimit(A)" + ",";
                                    strWrite += strTemp;
                                    strTemp = "CH" + (j + 1).ToString("D2") + "_Current(A)" + ",";
                                    strWrite += strTemp;
                                    strTemp = "CH" + (j + 1).ToString("D2") + "_HILimit(A)" + ",";
                                    strWrite += strTemp;
                                }
                            }
                            sw.WriteLine(strWrite);
                        }

                        int iLay = 0;
                        for (int j = 0; j < _runUUT[iTimer].Para.OutPutChan; j++)
                        {
                            int slot = iuutNo * _runUUT[iTimer].Para.OutPutChan + j;

                            iLay = _runUUT[iTimer].Led[slot].iLayer - 1;

                        }

                        //写入内容
                        strWrite = string.Empty;
                        strTemp = _runUUT[iTimer].Para.SaveDataTime + ",";
                        strWrite += strTemp;

                        strTemp = _runUUT[iTimer].PLC.curTemp.ToString("0.0") + ",";
                        strWrite += strTemp;

                        strTemp = (_runUUT[iTimer].Para.RunInVolt).ToString("0.0") + ",";
                        strWrite += strTemp;


                        for (int j = 0; j < _runUUT[iTimer].Para.OutPutChan; j++)
                        {
                            int slot = iuutNo * _runUUT[iTimer].Para.OutPutChan + j;
                            if (_runUUT[iTimer].Led[slot].result > 0)
                            {

                                strTemp = _runUUT[iTimer].Led[slot].vMin + ",";
                                strWrite += strTemp;
                                strTemp = _runUUT[iTimer].Led[slot].unitV + ",";
                                strWrite += strTemp;
                                strTemp = _runUUT[iTimer].Led[slot].vMax + ",";
                                strWrite += strTemp;
                                strTemp = _runUUT[iTimer].Led[slot].Imin + ",";
                                strWrite += strTemp;
                                strTemp = _runUUT[iTimer].Led[slot].unitA + ",";
                                strWrite += strTemp;
                                strTemp = _runUUT[iTimer].Led[slot].Imax + ",";
                                strWrite += strTemp;
                            }
                        }
                        if (uutpass)
                            strTemp = "Pass,";
                        else
                            strTemp = "Fail,";

                        strWrite += strTemp;

                        sw.WriteLine(strWrite);
                        sw.Flush();
                        sw.Close();
                        sw = null;

                    }
                }
                //if (upDb)
                uut_save_report_Path(iTimer);

                uut_save_report_timer(iTimer);

            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
        }

        /// <summary>
        /// 保存测试报表(CSV格式)
        /// </summary>
        /// <param name="uutNo"></param>
        private void uut_save_report_csv_test(int iTimer)
        {
            try
            {

                runLog.Log(_runUUT[iTimer].Para.TimerName + CLanguage.Lan("_保存报表"), udcRunLog.ELog.Action);
                if (_runUUT[iTimer].Para.TTNum == _runUUT[iTimer].Para.PassNum)
                {
                    _runUUT[iTimer].Para.SaveDataTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                }
                else
                    _runUUT[iTimer].Para.SaveDataTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                if(string.IsNullOrEmpty(  CGlobalPara.SysPara .Report .ReportPath))
                     CGlobalPara.SysPara .Report .ReportPath="D:\\Report";
                if (!Directory.Exists(CGlobalPara.SysPara .Report .ReportPath + "\\" + _runUUT[iTimer].Para.ModelName ))
                    Directory.CreateDirectory(CGlobalPara.SysPara.Report.ReportPath + "\\" + _runUUT[iTimer].Para.ModelName);
                if( _runUUT[iTimer].Para.SavePath==string.Empty )
                    _runUUT[iTimer].Para.SavePath = CGlobalPara.SysPara.Report.ReportPath + "\\" + _runUUT[iTimer].Para.ModelName + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
           
                bool IsExist = true;


                if (!File.Exists(_runUUT[iTimer].Para.SavePath))
                    IsExist = false;

                StreamWriter sw = new StreamWriter(_runUUT[iTimer].Para.SavePath, true, Encoding.UTF8);

                string strWrite = string.Empty;

                string strTemp = string.Empty;

                //写入标题栏
                if (!IsExist)
                {
                    strWrite = "机种名称:," + _runUUT[iTimer].Para.ModelName;
                    sw.WriteLine(strWrite);
                    strWrite = "开始时间:," + _runUUT[iTimer].Para.StartTime;
                    sw.WriteLine(strWrite);
                    DateTime endTime = (System.Convert.ToDateTime(_runUUT[iTimer].Para.StartTime)).AddSeconds(_runUUT[iTimer].Para.BurnTime);
                    strWrite = "结束时间:," + endTime.ToString("yyyy/MM/dd HH:mm:ss");
                    sw.WriteLine(strWrite);
                    strWrite = "老化时间(分钟):," + (_runUUT[iTimer].Para.BurnTime / 60).ToString("0.0");
                    sw.WriteLine(strWrite);
                    strWrite = "台车信息:," + _runUUT[iTimer].Para.MO_NO ;
                    sw.WriteLine(strWrite);
                    strWrite = "老化总数:," + _runUUT[iTimer].Para.TTNum ;
                    sw.WriteLine(strWrite);
                    strWrite = "良品数量:," + _runUUT[iTimer].Para.PassNum ;
                    sw.WriteLine(strWrite);
                    strWrite = "机种设置信息";
                    sw.WriteLine(strWrite);
                    strWrite = string.Empty;
                    for (int j = 0; j < _runUUT[iTimer].Para.OutPutChan; j++)
                    {
                   
                        strTemp = _runUUT[iTimer].Led[j].vName + "输出模式" +  ",";
                        strWrite += strTemp;
                        strTemp = _runUUT[iTimer].Led[j].vName + "设定值" + ",";
                        strWrite += strTemp;
                        strTemp = _runUUT[iTimer].Led[j].vName + "_CH" + (j + 1).ToString("D2") + "_Vmin(V)" + ",";
                        strWrite += strTemp;
                        strTemp = _runUUT[iTimer].Led[j].vName + "CH" + (j + 1).ToString("D2") + "_Vmax(V)" + ",";
                        strWrite += strTemp;

                        strTemp = _runUUT[iTimer].Led[j].vName + "CH" + (j + 1).ToString("D2") + "_Imin(A)" + ",";
                        strWrite += strTemp;
                        strTemp = _runUUT[iTimer].Led[j].vName + "CH" + (j + 1).ToString("D2") + "_Imax(A)" + ",";
                        strWrite += strTemp;
                    }
                    sw.WriteLine(strWrite);
                    strWrite = string.Empty;
                    for (int j = 0; j < _runUUT[iTimer].Para.OutPutChan; j++)
                    {
                        if (_runUUT[iTimer].OnOff.OutPut[0].Chan[j].Vuse == 1)
                        {
                            if (_runUUT[iTimer].Led[j].IMode == 0)
                            {
                                strTemp = "CC" + "," + _runUUT[iTimer].Led[j].ISet + ",";
                            }
                            else
                            {
                                strTemp = "CV" + "," + _runUUT[iTimer].Led[j].ISet + ",";
                            }
                            strWrite += strTemp;
                            strTemp = _runUUT[iTimer].Led[j].vMin + ",";
                            strWrite += strTemp;
                            strTemp = _runUUT[iTimer].Led[j].vMax + ",";
                            strWrite += strTemp;
                            strTemp = _runUUT[iTimer].Led[j].Imin + ",";
                            strWrite += strTemp;
                            strTemp = _runUUT[iTimer].Led[j].Imax + ",";
                            strWrite += strTemp;
                        }
                        else
                        {
                            strTemp = ",";
                            strWrite += strTemp;
                            strTemp = ",";
                            strWrite += strTemp;
                            strTemp = ",";
                            strWrite += strTemp;
                            strTemp = ",";
                            strWrite += strTemp;
                            strTemp =",";
                            strWrite += strTemp;
                            strTemp = ",";
                            strWrite += strTemp;

                        }
                    }
                    sw.WriteLine(strWrite);

                    strWrite = string.Empty;
                    sw.WriteLine(strWrite);
                    sw.WriteLine(strWrite);
                    strTemp = "产品条码,,,";
                    strWrite += strTemp;
                    for (int iuutNo = 0; iuutNo < _runUUT[iTimer].Led.Count / _runUUT[iTimer].Para.OutPutChan; iuutNo++)
                    {
                        strTemp =_runUUT[iTimer].Led[iuutNo].serialNo  ;
                        strWrite += strTemp;
                        for (int j = 0; j < _runUUT[iTimer].Para.OutPutChan; j++)
                        {
                            strTemp = ",,,";
                            strWrite += strTemp;
                        }

                    }
                    sw.WriteLine(strWrite);
                    strWrite = string.Empty;
                    strTemp = "Scan Time,Temp.(℃),AC Volt(V),";
                    strWrite += strTemp;
                    for (int iuutNo = 0; iuutNo < _runUUT[iTimer].Led.Count / _runUUT[iTimer].Para.OutPutChan; iuutNo++)
                    {
                        for (int j = 0; j < _runUUT[iTimer].Para.OutPutChan; j++)
                        {
                            strTemp = "产品" + (iuutNo + 1).ToString("D2") +"电压"+ ",";
                            strWrite += strTemp;
                            strTemp = "产品" + (iuutNo + 1).ToString("D2") + "电流" + ",";
                            strWrite += strTemp;
                            strTemp = "产品" +(iuutNo + 1).ToString("D2")  + "结果" + ",";
                            strWrite += strTemp;
                        }
                        //strTemp = "产品" + (iuutNo + 1).ToString("D2") + "总结果" + ",";
                        //strWrite += strTemp;
                    }
                    sw.WriteLine(strWrite);
                }


                //写入内容
                strWrite = string.Empty;
                strTemp = _runUUT[iTimer].Para.SaveDataTime + ",";
                strWrite += strTemp;

                strTemp = _runUUT[iTimer].PLC.curTemp.ToString("0.0") + ",";
                strWrite += strTemp;

                strTemp = (_runUUT[iTimer].Para.RunInVolt).ToString("0.0") + ",";
                strWrite += strTemp;
               
                for (int iuutNo = 0; iuutNo < _runUUT[iTimer].Led.Count / _runUUT[iTimer].Para.OutPutChan; iuutNo++)
                {
                    //bool uutpass = true;
                    //bool uutHave = false ;
                    for (int j = 0; j < _runUUT[iTimer].Para.OutPutChan; j++)
                    {   
                        int slot = iuutNo * _runUUT[iTimer].Para.OutPutChan + j;
                        if (_runUUT[iTimer].Led[slot].result > 0)
                        {
                            strTemp = _runUUT[iTimer].Led[slot].unitV + ",";
                            strWrite += strTemp;
                            strTemp = _runUUT[iTimer].Led[slot].unitA + ",";
                            strWrite += strTemp;

                        }
                        else
                        {
                            strTemp = "--" + ",";
                            strWrite += strTemp;
                            strTemp = "--" + ",";
                            strWrite += strTemp;

                        }
                        if (_runUUT[iTimer].Led[slot].result >1)
                        {
                            strTemp = "Fail,";
                            strWrite += strTemp;
                            //uutpass = false;
                            //uutHave = true;
                        }
                        else if (_runUUT[iTimer].Led[slot].result == 1 )
                        {
                            strTemp = "Pass,";
                            strWrite += strTemp;
                          //  uutHave = true;
                        }
                        else
                        {
                            strTemp = "--,";
                            strWrite += strTemp;

                        }
                    }
                    //if (uutHave == true)
                    //{
                    //    if (uutpass)
                    //        strTemp = "PASS,";
                    //    else
                    //        strTemp = "FAIL,";
                    //}
                    //else
                    //{
                    //    strTemp = "--,";
                    //}

                    //strWrite += strTemp;
                }
              

                sw.WriteLine(strWrite);
                sw.Flush();
                sw.Close();
                sw = null;



                //if (upDb)
                uut_save_report_Path(iTimer);

                uut_save_report_timer(iTimer);
            }

            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
        }

        #endregion

        #region 线程方法

        /// <summary>
        /// 修改测试报表保存时间
        /// </summary>
        /// <param name="iTimer"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool uut_save_report_timer(int iTimer)
        {
            try
            {
                string er = string.Empty;

                CDBCOM db = new CDBCOM(EDBType.Access, "", CGlobalPara.SysDB);

                string sqlCmd = "update RUN_PARA Set SaveDataTime='" + _runUUT[iTimer].Para.SaveDataTime +
                                "',SavePath='" + _runUUT[iTimer].Para.SavePath + "',SaveDataIndex=" + _runUUT[iTimer].Para.SaveDataIndex + " where TimerNO=" + iTimer;

                if (!db.excuteSQL(sqlCmd, out er))
                    runLog.Log(er, udcRunLog.ELog.NG);

                return true;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);

                return false;
            }
            finally
            {

            }
        }

        /// <summary>
        /// 保存测试报表路径
        /// </summary>
        /// <param name="iTimer"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private void uut_save_report_Path(int iTimer)
        {
            try
            {

                string er = string.Empty;

                CDBCOM db = new CDBCOM(EDBType.Access, "", CGlobalPara.SysDB);

                List<string> sqlCmdList = new List<string>();

                for (int uutNo = 0; uutNo < _runUUT[iTimer].Led.Count; uutNo++)
                {
                    string sqlCmd = "Update RUN_DATA set barType=" + _runUUT[iTimer].Led[uutNo].barType +
                                    ",barNo=" + _runUUT[iTimer].Led[uutNo].barNo + ",fixBar='" +
                                    _runUUT[iTimer].Led[uutNo].fixBar + "',serialNo='" + _runUUT[iTimer].Led[uutNo].serialNo +
                                    "',tranResult='" + _runUUT[iTimer].Led[uutNo].tranResult + "',unitV=" +
                                    _runUUT[iTimer].Led[uutNo].unitV + ",unitA=" + _runUUT[iTimer].Led[uutNo].unitA +
                                    ",result=" + _runUUT[iTimer].Led[uutNo].result + ",failEnd=" +
                                    _runUUT[iTimer].Led[uutNo].failEnd + ",failTime='" + _runUUT[iTimer].Led[uutNo].failTime +
                                    "',failInfo='" + _runUUT[iTimer].Led[uutNo].failInfo + "',vBack=" +
                                    _runUUT[iTimer].Led[uutNo].vBack + ",iBack=" + _runUUT[iTimer].Led[uutNo].iBack +
                                    ",iicData='" + _runUUT[iTimer].Led[uutNo].iicData + "',reportPath='" +
                                    _runUUT[iTimer].Led[uutNo].reportPath + "' where  timerNo=" + (iTimer + 1) + " and uutNo=" + uutNo;
                    sqlCmdList.Add(sqlCmd);
                }
                if (!db.excuteSQLArray(sqlCmdList, out er))
                    runLog.Log(er, udcRunLog.ELog.NG);

            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
            finally
            {

            }
        }

        /// <summary>
        /// 保存测试报表路径
        /// </summary>
        /// <param name="iTimer"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private void uut_upFail_Data(int iTimer, int iSolt)
        {
            try
            {

                string er = string.Empty;

                CDBCOM db = new CDBCOM(EDBType.Access, "", CGlobalPara.SysDB);

                List<string> sqlCmdList = new List<string>();

                string sqlCmd = "Update RUN_DATA set result=" + _runUUT[iTimer].Led[iSolt].result +
                                ",failEnd=" + _runUUT[iTimer].Led[iSolt].failEnd + ",failTime='" +
                                _runUUT[iTimer].Led[iSolt].failTime + "',failInfo='" + _runUUT[iTimer].Led[iSolt].failInfo +
                                "' where  timerNo=" + (iTimer + 1) + " and uutNo=" + iSolt;
                sqlCmdList.Add(sqlCmd);

                if (!db.excuteSQLArray(sqlCmdList, out er))
                    runLog.Log(er, udcRunLog.ELog.NG);

            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
            finally
            {

            }
        }

        #endregion

        #endregion

        #region 语言翻译
        private void FrmMain_Load(object sender, EventArgs e)
        {
            SetUILanguage();
        }
        #endregion

        #region "MES操作"

        private bool TranMes(int iTimer,  ref string  er)
        {
            try
            {
                if (!CGlobalPara.SysPara.Mes.Connect)
                    return true;

                if (_runUUT[iTimer].Para.MesFlag != 1)
                    return true;
                string failstr = string.Empty;
                er = string.Empty;
                if (_runUUT[iTimer].Para.UserName == string.Empty) _runUUT[iTimer].Para.UserName = "0903057";
                for (int islot = 0; islot < _runUUT[iTimer].Led.Count; islot++)
                {

                    if (_runUUT[iTimer].Led[islot].iCH != 1) continue;

                    if (_runUUT[iTimer].Led[islot].serialNo == string.Empty) continue;

                    if (_runUUT[iTimer].Led[islot].barType != 1) continue;

                    if (islot > 191) continue;

                    bool haveuut = false;
                    bool uutpass = true;
                    for (int ich = 0; ich < _runUUT[iTimer].Para.OutPutChan; ich++)
                    {
                        if (_runUUT[iTimer].Led[islot].result > 0)
                        {
                            haveuut = true;
                            if (_runUUT[iTimer].Led[islot].result == 2 || _runUUT[iTimer].Led[islot].result == 3 || _runUUT[iTimer].Led[islot].result == 5)
                                uutpass = false;
                        }
                    }

                    if (!haveuut) continue;
                    if (uutpass) continue;
                    
                    if (!CSajet_LiteonChangzhou.TranFailBarCode(_runUUT[iTimer].Para.UserName, _runUUT[iTimer].Led[islot].serialNo, "PDTE0001", out er))
                    {
                        runLog.Log($"不良产品条码{_runUUT[iTimer].Para.UserName}_{_runUUT[iTimer].Led[islot].serialNo}_{"PDTE0001"}上传异常=>{er}", udcRunLog.ELog.NG);
                        failstr += $"不良条码{_runUUT[iTimer].Led[islot].serialNo}";
                    }
                    else
                    {
                        runLog.Log($"不良产品条码{_runUUT[iTimer].Led[islot].serialNo}上传完成", udcRunLog.ELog.OK );
                    }
                }

                if (!CSajet_LiteonChangzhou.EndBI(_runUUT[iTimer].Para.UserName, _runUUT[iTimer].Para.MO_NO, out er))
                {
                    runLog.Log($"整柜数据上传{_runUUT[iTimer].Para.UserName}_{_runUUT[iTimer].Para.MO_NO}上传异常=>{er}", udcRunLog.ELog.NG);
                    failstr += $"整柜{_runUUT[iTimer].Para.MO_NO}";
                }
                else
                {
                    runLog.Log($"整柜数据上传{_runUUT[iTimer].Para.MO_NO}上传完成", udcRunLog.ELog.OK);
                }


                for (int islot = 0; islot < _runUUT[iTimer].Led.Count; islot++)
                {

                    if (_runUUT[iTimer].Led[islot].iCH != 1) continue;

                    if (_runUUT[iTimer].Led[islot].serialNo == string.Empty) continue;

                    if (_runUUT[iTimer].Led[islot].barType != 1) continue;

                    bool haveuut = false;
                    bool uutpass = true;
                    for (int ich = 0; ich < _runUUT[iTimer].Para.OutPutChan; ich++)
                    {
                        if (_runUUT[iTimer].Led[islot].result > 0)
                        {
                            haveuut = true;
                            if (_runUUT[iTimer].Led[islot].result == 2 || _runUUT[iTimer].Led[islot].result == 3 || _runUUT[iTimer].Led[islot].result == 5)
                                uutpass = false;
                        }
                    }

                    if (!haveuut) continue;

                    if (!CSajet_LiteonChangzhou.EndData(_runUUT[iTimer].Para.UserName, _runUUT[iTimer].Led[islot].serialNo, $"{_runUUT[iTimer].Para.ModelName}-Vout@{_runUUT[iTimer].Para.ModelName}-Iout@{_runUUT[iTimer].Para.ModelName}-Tout",
                        $"{_runUUT[iTimer].Led[islot].unitV:F2}@{_runUUT[iTimer].Led[islot].unitA:F2}@{_runUUT[iTimer].PLC.curTemp:F1}", uutpass ? "0@0@0" : "1@1@0", out er))
                    {
                        runLog.Log($"条码数据{_runUUT[iTimer].Para.UserName}_{_runUUT[iTimer].Led[islot].serialNo}_{"PDTE0001"}上传异常=>{er}", udcRunLog.ELog.NG );
                        failstr += $"条码数据{_runUUT[iTimer].Led[islot].serialNo}";
                    }
                    else
                    {
                        runLog.Log($"条码数据{_runUUT[iTimer].Led[islot].serialNo}上传完成", udcRunLog.ELog.OK);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        
        # endregion


    }
}
