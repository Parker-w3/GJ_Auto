using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.COM;

namespace GJ.Aging.BURNIN
{
    /// <summary>
    /// 全局类
    /// </summary>
    public class CGlobalPara
    {
        #region 常量定义
        /// <summary>
        /// 工位标识名称
        /// </summary>
        private static string StationName = "BURNIN";
        /// <summary>
        /// 系统参数路径
        /// </summary>
        public static string SysFile = Application.StartupPath + "\\" + StationName + "\\" + StationName + ".xml";
        /// <summary>
        /// 工位配置文件路径
        /// </summary>
        public static string IniFile = Application.StartupPath + "\\" + StationName + "\\" + StationName + ".ini";
        /// <summary>
        /// 运行数据库
        /// </summary>
        public static string SysDB = Application.StartupPath + "\\" + StationName + "\\" + StationName + ".accdb";

        /// <summary>
        /// Excel样板
        /// </summary>
        public static string ExcelFile = Application.StartupPath + "\\" + StationName + "\\" + StationName + ".DAT";

        /// <summary>
        /// 记录老化完成目录
        /// </summary>
        public static string ResultFile = Application.StartupPath + "\\" + StationName + "\\" + StationName + "Result.csv";

        /// <summary>
        /// PLC运行数据库
        /// </summary>
        public static string PlcDB = Application.StartupPath + "\\" + StationName + "\\PLC_" + StationName + ".accdb";

        /// <summary>
        /// 配置登录名
        /// </summary>
        public static string UserFile = Application.StartupPath + "\\" + "User.ini";

        /// <summary>
        /// 初始化状态
        /// </summary>
        public static bool DownLoad = false;
        /// <summary>
        /// 系统运行中
        /// </summary>
        public static bool C_RUNNING = false;
        /// <summary>
        /// 设备启动扫描
        /// </summary>
        public static bool C_SCAN_START = false;
        /// <summary>
        /// 初始化扫描监控
        /// </summary>
        public static bool C_INI_SCAN = false;
        /// <summary>
        /// 线程扫描间隔
        /// </summary>
        public static int C_THREAD_DELAY = 20;
        /// <summary>
        /// 报警延时
        /// </summary>
        public static int C_ALARM_DELAY = 1000;


        /// <summary>
        /// 报警次数
        /// </summary>
        public static int C_ALARM_TIME = 3;

        /// <summary>
        /// 报表线程
        /// </summary>
        public static int C_Report_Threed = 1;
        /// <summary>
        /// 老化区域数
        /// </summary>
        public static int C_Area_MAX = 1;
        /// <summary>
        /// 区域时序数
        /// </summary>
        public static int C_Area_Timer = 2;
        /// <summary>
        /// 老化总时序数
        /// </summary>
        public static int C_Timer_MAX = 2;
        /// <summary>
        /// 老化时序层数
        /// </summary>
        public static int C_Timer_Lay = 8;

        /// <summary>
        /// 单层板数
        /// </summary>
        public static int C_Layer_Board = 6;

        /// <summary>
        /// 单层产品数量
        /// </summary>
        public static int C_Layer_UUT = 24;

        /// <summary>
        /// 列数
        /// </summary>
        public static int C_COL_MAX = 6;

        /// <summary>
        /// 单区板数量
        /// </summary>
        public static int C_Board_MAX = 48;

        /// <summary>
        /// 单区产品数量
        /// </summary>
        public static int C_UUT_MAX = 192;

        #endregion

        #region GJDA_1050_4负载对应参数设定
        /// <summary>
        /// GJDA_1050_4使用线程数
        /// </summary>
        public static int C_GJDA_1050_4_Num = 2;


        /// <summary>
        /// GJDA_1050_4负载模块数量
        /// </summary>
        public static int[] C_GJDA_1050_4_MAX = new int[] {12,12};

        /// <summary>
        /// GJDA_1050_4负载模块名称
        /// </summary>
        public static string[] C_GJDA_1050_4_NAME = new string[] { "1050_4负载A车1-12", "1050_4负载B车1-12" };

        #endregion

        #region XT_LED表头对应参数设定
        /// <summary>
        /// XT_LED表头使用线程数
        /// </summary>
        public static int C_XT_LED_Num = 1;

        /// <summary>
        /// XT_LED表头数量
        /// </summary>
        public static int[] C_XT_LED_MAX = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

        /// <summary>
        /// XT_LED表头名称
        /// </summary>
        public static string[] C_XT_LED_NAME = new string[] { "A_1~3", "A_4~6", "B_1~3", "B_4~6", "C_1~3", "C_4~6", "D_1~3", "D_4~6", "E_1~3", "E_4~6", "F_1~3", "F_4~6" };

        #endregion

        #region PLC对应参数设定
        /// <summary>
        /// 使用PLC数量
        /// </summary>
        public static int C_PLCCom_Max = 1;
        /// <summary>
        /// 单个PLC的时序数
        /// </summary>
        public static int C_PLC_Timer = 2;

        /// <summary>
        /// PLC超时次数
        /// </summary>
        public static int[] C_PLC_TimeOut= new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        #endregion

        #region 控制线程对应参数设定
        /// <summary>
        /// 使用Ctrl数量
        /// </summary>
        public static int C_Ctrl_Max = 1;
        /// <summary>
        /// 单个Ctrl的时序数
        /// </summary>
        public static int C_Ctrl_Timer = 1;

        #endregion

        #region 用户信息
        /// <summary>
        /// 用户权限等级
        /// </summary>
        private const int C_PWR_LEVEL_MAX = 8;
        /// <summary>
        /// 登录用户
        /// </summary>
        public static string logName = "0903057";

        /// <summary>
        /// 用户密码
        /// </summary>
        public static string logPassword = "0903057";

        /// <summary>
        /// 登录权限
        /// </summary>
        public static int[] logLevel = new int[C_PWR_LEVEL_MAX];
        #endregion

        #region 系统参数
        public static CSysPara SysPara = new CSysPara();
        /// <summary>
        /// 加载系统参数
        /// </summary>
        public static void loadSysXml()
        {
            CSerializable<CSysPara>.ReadXml(SysFile, ref SysPara);
        }
        #endregion
    }

    #region 系统参数类
    /// <summary>
    /// 基本参数
    /// </summary>
    [Serializable]
    public class CSYS_Para
    {
        /// <summary>
        /// 时序个数
        /// </summary>
        public int timerNum = 2;
        /// <summary>
        /// 时序层数
        /// </summary>
        public int[] timerLayerNum = new int[] { 8, 8, 8, 8};
        /// <summary>
        /// 时序最大产品数
        /// </summary>
        public int[] timerChanNum = new int[] { 384, 384, 384, 384 };
        /// <summary>
        /// 时序命名
        /// </summary>
        public string[] timerNO = new string[] { "A", "B", "C", "D" };
        /// <summary>
        /// 时序位置条码规则
        /// </summary>
        public string[] timerBarSpec = new string[] { "A_1", "B_1", "C_1", "D_1" };

        /// <summary>
        /// 区域命名
        /// </summary>
        public string[] areaNO = new string[] { "A", "B", "C", "D", "E", "F"};

        /// 负载个数
        /// </summary>
        /// <summary>
        public int[] eLoadNum = new int[] { 12, 12, 12, 12};        
        /// <summary>
        /// 负载通道数
        /// </summary>
        public int[] eLoadChanNum = new int[] { 4, 4, 4, 4};
        /// <summary>
        /// 采集板个数
        /// </summary>
        public int[] monNum = new int[] { 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6 };  
        /// <summary>
        /// 采集板通道
        /// </summary>
        public int[] monChanNum = new int[] { 192, 192, 192, 192, 192, 192, 192, 192, 192, 192, 192, 192 };

        /// <summary>
        /// 循环风机屏蔽
        /// </summary>
        public bool chkNoF = false;
        /// <summary>
        /// 门控屏蔽
        /// </summary>
        public bool chkNoGating = false;

        /// <summary>
        /// 扫描位置码模式
        /// </summary>
        public bool chkScanPathSn = false;

        /// <summary>
        /// 扫描条码
        /// </summary>
        public int BarFlag;


        /// <summary>
        /// 启用扫码语音播放
        /// </summary>
        public bool chkSpeeck = false;
    }
    /// <summary>
    /// 通信设备
    /// </summary>
    [Serializable]
    public class CSYS_Dev
    {
        /// <summary>
        /// PLC串口
        /// </summary>
        public string[] plcCom = new string[] { "COM1" };
        /// <summary>
        /// PLC波特率
        /// </summary>
        public string[] plcBuad = new string[] { "57600,e,7,1" };

        /// <summary>
        /// 电子负载串口
        /// </summary>
        public string[] GJ_1050_4Com = new string[] { "COM3", "COM4" };

        /// <summary>
        /// 电子负载波特率
        /// </summary>
        public string[] GJ_1050_4Buad = new string[] { "9600,n,8,1", "9600,n,8,1" };


        /// <summary>
        /// 电子负载串口
        /// </summary>
        public string[] GJ_Mon32Com = new string[] { "COM5", "COM6"};

        /// <summary>
        /// 电子负载波特率
        /// </summary>
        public string[] GJ_Mon32Buad = new string[] { "9600,n,8,1", "9600,n,8,1" };

        /// <summary>
        /// 1500负载串口
        /// </summary>
        public string[] GJ_1500Com = new string[] { "COM5" };

        /// <summary>
        /// 1500负载波特率
        /// </summary>
        public string[] GJ_1500Buad = new string[] { "9600,n,8,1" };

        /// <summary>
        /// 显通表头串口
        /// </summary>
        public string[] XT_Led_Com = new string[] { "COM6" };

        /// <summary>
        /// 显通表头波特率
        /// </summary>
        public string[] XT_Led_Buad = new string[] { "57600,n,8,1" };

        /// <summary>
        /// 条码枪串口
        /// </summary>
        public string[] Bar_Com = new string[] { "COM7" };

        /// <summary>
        /// 条码枪波特率
        /// </summary>
        public string[] Bar_Buad = new string[] { "9600,n,8,1" };
    }

    /// <summary>
    /// 测试参数
    /// </summary>
    [Serializable]
    public class CSYS_Reg
    {
        /// <summary>
        /// 不判断电流
        /// </summary>
        public bool ChkNoJugdeCur = false;
        /// <summary>
        /// 电压校正下限
        /// </summary>
        public double VLP = 1;
        /// <summary>
        /// 电压校正上限
        /// </summary>
        public double VHP = 1;
        /// <summary>
        /// 电流校正下限
        /// </summary>
        public double ILP = 1;
        /// <summary>
        /// 电流校正上限
        /// </summary>
        public double IHP = 1;
        /// <summary>
        /// 判断是否有产品的最低电压||电流
        /// </summary>
        public double haveUUT = 2;
        /// <summary>
        /// 电压补偿
        /// </summary>
        public double offsetVolt = 0.1;
    }
    /// <summary>
    /// 报警参数
    /// </summary>
    [Serializable]
    public class CSYS_Alarm
    {
        /// <summary>
        /// 通信异常报警
        /// </summary>
        public int IICFailTimes = 0;
        /// <summary>
        /// 电压不良次数
        /// </summary>
        public int VoltFailTimes = 0;
        /// <summary>
        /// 电流不良次数
        /// </summary>
        public int CurFailTimes = 0;

        /// <summary>
        /// 不显示报警提示
        /// </summary>
        public bool NoShowAlarm = false;

        /// <summary>
        /// 有不良是否告警
        /// </summary>
        public bool uutAlarm = false;

        /// <summary>
        /// 单点超温断电
        /// </summary>
        public int LoadAlarm = 50;

        /// <summary>
        /// 负载启动排风
        /// </summary>
        public int LoadOpenFan = 35;

        /// <summary>
        /// 负载关闭排风
        /// </summary>
        public int LoadCloseFan = 30;

        /// <summary>
        /// 门控
        /// </summary>
        public bool DoorCtrl = false;


        /// <summary>
        /// CtrlSet控制切换,0=PLC触发,1=共同控制
        /// </summary>
        public bool[] CtrlSet = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false};


    }
    /// <summary>
    /// 测试报表
    /// </summary>
    [Serializable]
    public class CSYS_Report
    {
        /// <summary>
        /// 扫描时间间隔
        /// </summary>
        public int MonInterval = 10;

        /// <summary>
        /// 切换电压，负载首次扫描时间
        /// </summary>
        public int FirstScan = 30;
        /// <summary>
        /// 获取机种路径
        /// </summary>
        public string ModelPath = string.Empty;
        /// <summary>
        /// 保存测试报表
        /// </summary>
        public bool SaveReport = false;
        /// <summary>
        /// 保存报表间隔时间(Sec)
        /// </summary>
        public double SaveReportTimes = 60;
        /// <summary>
        /// 测试报表路径
        /// </summary>
        public string ReportPath = string.Empty;
        /// <summary>
        /// 网络报表路径
        /// </summary>
        public string WebReportPath = string.Empty;
        /// <summary>
        /// 日产能报表
        /// </summary>
        public string DayRecordPath = string.Empty;

        /// <summary>
        /// 自检开机时间
        /// </summary>
        public double ReStartTime = 60;

        /// <summary>
        /// 保存Excel报表
        /// </summary>
        public bool  SaveExcel = false;


      

    }
    /// <summary>
    /// MES设置
    /// </summary>
    [Serializable]
    public class CSYS_MES
    {
        /// <summary>
        /// 连线模式
        /// </summary>
        public bool Connect = false;
        /// <summary>
        /// 不良产品自动上传
        /// </summary>
        public bool AutoTranFail = false;
        /// <summary>
        /// 扫描条码备份
        /// </summary>
        public bool ScanBarBak = false;
        /// <summary>
        /// 扫描条码备份路径
        /// </summary>
        public String ScanBarBakPath = string.Empty;
        /// <summary>
        /// 上传条码备份
        /// </summary>
        public bool TranBarBak = false;
        /// <summary>
        /// 上传条码备份路径
        /// </summary>
        public String TranBarBakPath = string.Empty;
        /// <summary>
        /// 上传条码
        /// </summary>
        public bool TranBar = false;
        /// <summary>
        /// 上传条码备份路径
        /// </summary>
        public String TranBarPath = string.Empty;

        /// <summary>
        /// 服务器地址
        /// </summary>
        public String OracleDB = string.Empty;

        /// <summary>
        /// 线体
        /// </summary>
        public String LineNo = string.Empty;

        /// <summary>
        /// 上传账号
        /// </summary>
        public String tranUser = string.Empty;

        /// <summary>
        /// 进站号
        /// </summary>
        public String inStation = string.Empty;
        /// <summary>
        /// 出站号
        /// </summary>
        public String outStation = string.Empty;

    }
    /// <summary>
    /// 系统参数
    /// </summary>
    [Serializable]
    public class CSysPara
    {
        /// <summary>
        /// 基本参数
        /// </summary>
        public CSYS_Para Para = new CSYS_Para();
        /// <summary>
        /// 通信设备
        /// </summary>
        public CSYS_Dev Dev = new CSYS_Dev();
        /// <summary>
        /// 通信设备
        /// </summary>
        public CSYS_Reg Reg = new CSYS_Reg();
        /// <summary>
        /// 报警参数
        /// </summary>
        public CSYS_Alarm Alarm = new CSYS_Alarm();
        /// <summary>
        /// 测试报表
        /// </summary>
        public CSYS_Report Report = new CSYS_Report();
        /// <summary>
        /// MES设置
        /// </summary>
        public CSYS_MES Mes = new CSYS_MES();
    }
    #endregion

    #region 机种参数
    /// <summary>
    /// 基本信息
    /// </summary>
    [Serializable]
    public class CM_Base
    {
        /// <summary>
        /// 机种名
        /// </summary>
        public string Model;
        /// <summary>
        /// 客户
        /// </summary>
        public string Custom;
        /// <summary>
        /// 机种版本
        /// </summary>
        public string Version;
        /// <summary>
        /// 发行人
        /// </summary>
        public string Publisher;

        /// <summary>
        /// 条码规则
        /// </summary>
        public string BarSpec;

        /// <summary>
        /// 上传数据
        /// </summary>
        public int MesFlag;

        /// <summary>
        /// 扫描条码
        /// </summary>
        public int BarFlag;

        /// <summary>
        /// 条码长度
        /// </summary>
        public int BarLength;

        /// <summary>
        /// 治具条码
        /// </summary>
        public int fixBar;


    }
    /// <summary>
    /// 测试参数
    /// </summary>
    [Serializable]
    public class CM_Para
    {
        /// <summary>
        /// 老化时间(H)
        /// </summary>
        public int BITime = 0;
        /// <summary>
        /// 产品输出组数
        /// </summary>
        public int OutPut_Chan = 1;
        /// <summary>
        /// 产品通道并联数
        /// </summary>
        public int ChanAdd = 1;
        /// <summary>
        /// 输出定义组数
        /// </summary>
        public int OutPut_Num = 0;
        /// <summary>
        /// ON/OFF组数
        /// </summary>
        public int OnOff_Num = 0;
        /// <summary>
        /// 设定温度
        /// </summary>
        public double TSet = 0;
        /// <summary>
        /// 温度下限偏差
        /// </summary>
        public double TLP = 0;
        /// <summary>
        /// 温度上限偏差
        /// </summary>
        public double THP = 0;
        /// <summary>
        /// 高温报警
        /// </summary>
        public double THAlarm = 0;
        /// <summary>
        /// 启动排风温度
        /// </summary>
        public double TOPEN = 0;
        /// <summary>
        /// 停止排风温度
        /// </summary>
        public double TCLOSE = 0;
       

        /// <summary>
        /// DC输出电压
        /// </summary>
        public double[] DcPower = new double[4];
        /// <summary>
        /// DC输出延迟时间
        /// </summary>
        public int[] DcPowerDelayTime = new int[4];
        /// <summary>
        /// DC关闭输出延迟时间
        /// </summary>
        public int[] CloseDcPowerDelayTime = new int[4];
        /// <summary>
        /// 循环风机屏蔽
        /// </summary>
        public bool ChkNoF = false;

    }




    /// <summary>
    /// CAN参数设定
    /// </summary>
    [Serializable]
    public class CM_Can
    {
        /// <summary>
        /// CAN波特率
        /// </summary>
        public int CanBaud = 0;
        /// <summary>
        /// CAN类型
        /// </summary>
        public int CanType = 0;
        /// <summary>
        /// CAN格式
        /// </summary>
        public int CanFrom = 0;
    }

    /// <summary>
    /// 单通道输出规格
    /// </summary>
    public class COutPut_CH
    {
        public int Vuse;
        public string Vname;
        public double Vmin;
        public double Vmax;
        public int Imode;
        public double ISet;
        public double Imin;
        public double Imax;
        public double Von;
        public double AddMode;
    }

    /// <summary>
    /// 输出规格列表
    /// </summary>
    [Serializable]
    public class COutPut_List
    {
        public COutPut_List()
        {

            for (int i = 0; i < CHAN_MAX; i++)
            {
                Chan[i] = new COutPut_CH();
                Chan[i].Vuse  = _Vuse[i];
                Chan[i].Vname = _Vname[i];
                Chan[i].Vmin = _Vmin[i];
                Chan[i].Vmax = _Vmax[i];
                Chan[i].Imode = _Imode[i];
                Chan[i].ISet = _ISet[i];
                Chan[i].Imin = _Imin[i];
                Chan[i].Imax = _Imax[i];
                Chan[i].Von = _Von[i];
                Chan[i].AddMode = _AddMode[i];
            }
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>
        public COutPut_List Clone()
        {
            COutPut_List outPut = new COutPut_List();

            outPut.Describe = this.Describe;
            outPut.SpecData = this.SpecData;
            for (int i = 0; i < CHAN_MAX; i++)
            {
                outPut.Chan[i] = new COutPut_CH();

                outPut.Chan[i].Vuse = this.Chan[i].Vuse;
                outPut.Chan[i].Vname = this.Chan[i].Vname;
                outPut.Chan[i].Vmin = this.Chan[i].Vmin;
                outPut.Chan[i].Vmax = this.Chan[i].Vmax;
                outPut.Chan[i].Imode = this.Chan[i].Imode; 
                outPut.Chan[i].ISet = this.Chan[i].ISet;
                outPut.Chan[i].Imin = this.Chan[i].Imin;
                outPut.Chan[i].Imax = this.Chan[i].Imax;
                outPut.Chan[i].Von = this.Chan[i].Von;
                outPut.Chan[i].AddMode = this.Chan[i].AddMode;

            }
            return outPut;
        }

        /// <summary>
        /// 最多通道数
        /// </summary>
        private const int CHAN_MAX = 20;
        /// <summary>
        /// 功能描述
        /// </summary>
        public string Describe;

        /// <summary>
        /// CAN数据
        /// </summary>
        public string SpecData;

        /// <summary>
        /// 输出规格
        /// </summary>
        public COutPut_CH[] Chan = new COutPut_CH[CHAN_MAX];

        private int[] _Vuse = new int[CHAN_MAX];// { "+5V", "+12V1", "+12V2", "+12V3", "+12V4", "+12V5", "+3.3V", "+5Vsb" };
        private string[] _Vname = new string[CHAN_MAX];// { "+5V", "+12V1", "+12V2", "+12V3", "+12V4", "+12V5", "+3.3V", "+5Vsb" };
        private double[] _Vmin = new double[CHAN_MAX];// { 4.75, 11.75, 11.75, 11.75, 11.75, 11.75, 2.75, 4.75 };
        private double[] _Vmax = new double[CHAN_MAX];// { 5.25, 12.25, 12.25, 12.25, 12.25, 12.25, 3.75, 5.25 };
        private int[] _Imode = new int[CHAN_MAX];// { 0, 0, 0, 0, 0, 0, 0, 0 };
        private double[] _ISet = new double[CHAN_MAX];//{ 1, 1, 1, 1, 1, 1, 1, 1 };
        private double[] _Imin = new double[CHAN_MAX];// { 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8 };
        private double[] _Imax = new double[CHAN_MAX];// { 1.2, 1.2, 1.2, 1.2, 1.2, 1.2, 1.2, 1.2 };
        private double[] _Von = new double[CHAN_MAX];// { 3, 3, 3, 3, 3, 3, 3, 3 };
        private double[] _AddMode = new double[CHAN_MAX];// { 10, 10, 10, 10, 10, 10, 10, 10 };
    }

    /// <summary>
    /// ON/OFF子类
    /// </summary>
    [Serializable]
    public class COnOff_Item
    {
        /// <summary>
        /// 0：分钟 1：秒单位
        /// </summary>
        public int ChkSec=0;
        /// <summary>
        /// 循环次数
        /// </summary>
        public int OnOffTime = 0;
        /// <summary>
        /// ON时间(S)
        /// </summary>
        public int OnTime = 0;
        /// <summary>
        /// OFF时间(S)
        /// </summary>
        public int OffTime = 0;
        /// <summary>
        /// 输入电压
        /// </summary>
        public int InPutV = 0;
        /// <summary>
        /// 输出类型
        /// </summary>
        public int OutPutType = 0;
		/// <summary>
		/// DCONOFF
		/// </summary>
		public int dcONOFF = 0;
        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>
        public COnOff_Item Clone()
        {
            COnOff_Item item = new COnOff_Item();

            item.ChkSec = this.ChkSec;

            item.OnOffTime = this.OnOffTime;

            item.OnTime = this.OnTime;

            item.OffTime = this.OffTime;

            item.InPutV = this.InPutV; 

            item.OutPutType = this.OutPutType;

			item.dcONOFF = this.dcONOFF;

            return item;
        }
    }
    /// <summary>
    /// ON/OFF步骤
    /// </summary>
    [Serializable]
    public class COnOff_List
    {
        public COnOff_List()
        {
            Item = new COnOff_Item[ITEM_MAX]; 

            for (int i = 0; i < ITEM_MAX; i++)
            {
                Item[i] = new COnOff_Item();

                Item[i].ChkSec = 0;

                Item[i].OnOffTime = 0;

                Item[i].OnTime = 0;

                Item[i].OffTime = 0;

                Item[i].InPutV = 110;

                Item[i].OutPutType = 0;

				Item[i].dcONOFF  = 0;
            }        
        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>
        public COnOff_List Clone()
        {
            try
            {
                COnOff_List list = new COnOff_List();

                list.Describe = this.Describe;
                list.TotalTime = this.TotalTime;

                for (int i = 0; i < ITEM_MAX; i++)
                {
                    list.Item[i].ChkSec = this.Item[i].ChkSec;
                    list.Item[i].OnOffTime = this.Item[i].OnOffTime;
                    list.Item[i].OnTime = this.Item[i].OnTime;
                    list.Item[i].OffTime = this.Item[i].OffTime;
                    list.Item[i].InPutV = this.Item[i].InPutV; 
                    list.Item[i].OutPutType = this.Item[i].OutPutType;
					list.Item[i].dcONOFF = this.Item[i].dcONOFF;
                }

                return list;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        /// <summary>
        /// 功能描述
        /// </summary>
        public string Describe;
        /// <summary>
        /// 总时间(S)
        /// </summary>
        public int TotalTime = 0;
 
        /// <summary>
        /// ON/OFF项目
        /// </summary>
        public COnOff_Item[] Item;
        /// <summary>
        /// 4段ON/OFF
        /// </summary>
        private int ITEM_MAX = 4;
    }

    [Serializable]
    public class CModelPara
    {
        public CModelPara()
        {
            for (int i = 0; i < C_ITEM_MAX; i++)
            {
                OutPut[i] = new COutPut_List();
                OnOff[i] = new COnOff_List();
            }
        }
        private const int C_ITEM_MAX = 50;
        public CM_Base Base = new CM_Base();
        public CM_Para Para = new CM_Para();
        public CM_Can Can = new CM_Can();
        public COutPut_List[] OutPut = new COutPut_List[C_ITEM_MAX];
        public COnOff_List[] OnOff = new COnOff_List[C_ITEM_MAX]; 
    }
    #endregion

}
