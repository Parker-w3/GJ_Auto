using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using GJ.USER.APP;
namespace GJ.Aging.BURNIN
{
    #region 测试枚举及类定义
    /// <summary>
    /// 老化运行状态
    /// </summary>
    public enum AgingRunType
    {

        空闲 = 0,
        自检 = 1,
        运行 = 2,
        暂停 = 3,
        扫条码 = 4
          
    }
    /// <summary>
    /// CAN模块状态
    /// </summary>
    public enum AgingCanType
    {
        空闲 = 0,
        初始化 = 1,
        扫描数据 = 2,
        清除ID = 3,
        获取ID = 4,
        开启DC = 5,
        关闭DC = 6,
        设定产品规格 = 7,
        扫描完成 = 8,
        设定波特率 = 9
    }

    /// <summary>
    /// 风扇模块状态
    /// </summary>
    public enum AgingFanType
    {
        空闲 = 0,
        EnableOn = 1,
        EnableOff = 2
    }
    /// <summary>
    /// IIC模块状态
    /// </summary>
    public enum AgingIICType
    {
        空闲 = 0,
        设定地址 = 1,
        扫描完成 = 2
    }

    /// <summary>
    /// 负载切换状态
    /// </summary>
    public enum AgingELType
    {
        空闲 = 0,
        清除负载 = 1,
        设定负载 = 2
    }

    /// <summary>
    /// 数据状态设定
    /// </summary>
    public enum AgingDataType
    {
        空闲 = 0,
        保存报表 = 1,
        结束报表 = 2
    }

    /// <summary>
    /// 条码扫描状态
    /// </summary>
    public enum AgingMesType
    {
        空闲 = 0,
        扫描条码 = 1,
        上传条码 = 2
    }

    /// <summary>
    /// 开关机切换状态
    /// </summary>
    public enum AgingONOFFType
    {
        空闲 = 0,
        ON = 1,
        OFF = 2,
        复位报警 = 3
    }

    /// <summary>
    /// 运行异常报警代码
    /// </summary>
    public enum RunAlarmCode
    {
        正常,
        PLC通讯异常,
        温度模块异常,
        单点超温,
        门打开  
    }

    /// <summary>
    /// 快充切换过程
    /// </summary>
    public enum EQCMChage
    { 
      空闲,
      控制ACON,
      自检电压,
      设置快充,
      设置负载
    }
    /// <summary>
    /// 规格参数
    /// </summary>
    public class CTIME
    {
        /// <summary>
        /// 老化时间(S)
        /// </summary>
        public int BITime = 0;
        /// <summary>
        /// 输出组数
        /// </summary>
        public int OutPutNum = 0;
        /// <summary>
        /// 输出通道数
        /// </summary>
        public int OutChanNum = 0;
        /// <summary>
        /// ONOFF组数
        /// </summary>
        public int OnOffNum = 0;
        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>
        public CTIME Clone()
        {
            CTIME timeSpec = new CTIME();

            timeSpec.BITime = this.BITime;

            timeSpec.OutPutNum = this.OutPutNum;

            timeSpec.OutChanNum = this.OutChanNum;

            timeSpec.OnOffNum = this.OnOffNum;

            return timeSpec;
        }
    }
    /// <summary>
    /// 通道规格
    /// </summary>
    public class CChannel
    {
        /// <summary>
        /// 通道名称
        /// </summary>
        public int Vuse;
        /// <summary>
        /// 通道名称
        /// </summary>
        public string Vname;
        /// <summary>
        /// 输出下限
        /// </summary>
        public double Vmin;
        /// <summary>
        /// 输出上限
        /// </summary>
        public double Vmax;
        /// <summary>
        /// 负载模式
        /// </summary>
        public int Imode;
        /// <summary>
        /// 负载电流
        /// </summary>
        public double ISet;
        /// <summary>
        /// 电流下限
        /// </summary>
        public double Imin;
        /// <summary>
        /// 电流上限
        /// </summary>
        public double Imax;
        /// <summary>
        /// Von设置
        /// </summary>
        public double Von;
        /// <summary>
        /// 附加模式 
        /// </summary>
        public double AddMode;


        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>
        public CChannel Clone()
        {
            CChannel _chan = new CChannel();
            _chan.Vuse = this.Vuse;
            _chan.Vname = this.Vname;
            _chan.Imin = this.Imin;
            _chan.Imax = this.Imax;
            _chan.Imode = this.Imode;
            _chan.ISet = this.ISet;
            _chan.Imin = this.Imin;
            _chan.Imax = this.Imax;
            _chan.Von = this.Von;
            _chan.AddMode = this.AddMode;
            return _chan;
        }
    }
    /// <summary>
    /// 输出规格
    /// </summary>
    public class COutPutSpec
    {
        /// <summary>
        /// IIC数据
        /// </summary>
        public string iicSpec =string.Empty ;
        /// <summary>
        /// 输出通道规格
        /// </summary>
        public List<CChannel> Chan = new List<CChannel>();
        /// <summary>
        /// 复制
        /// </summary>
        public COutPutSpec Clone()
        {
            COutPutSpec _outPut = new COutPutSpec();

            _outPut.iicSpec = this.iicSpec;

            for (int i = 0; i < Chan.Count; i++)
            {
                CChannel _chan = new CChannel();
                _chan.Vuse = this.Chan[i].Vuse;
                _chan.Vname = this.Chan[i].Vname;
                _chan.Imin = this.Chan[i].Imin;
                _chan.Imax = this.Chan[i].Imax;
                _chan.Imode = this.Chan[i].Imode;
                _chan.ISet = this.Chan[i].ISet;
                _chan.Imin = this.Chan[i].Imin;
                _chan.Imax = this.Chan[i].Imax;
                _chan.Von = this.Chan[i].Von;
                _chan.AddMode = this.Chan[i].AddMode;
                _outPut.Chan.Add(_chan);
            }
            return _outPut;
        }
    }

    /// <summary>
    /// ON/OFF段规格
    /// </summary>
    public class CONOFFSpec
    {
        /// <summary>
        /// ONOFF时间(S)
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
        public int inPutVolt = 0;

		/// <summary>
		/// DCONOFF
		/// </summary>
		public int dcONOFF = 0;
    
        /// <summary>
        /// 输出类型
        /// </summary>
        public int outPutCur = 0;
        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>
        public CONOFFSpec Clone()
        {
            CONOFFSpec item = new CONOFFSpec();
            item.OnOffTime = this.OnOffTime;
            item.OnTime = this.OnTime;
            item.OffTime = this.OffTime;
            item.inPutVolt = this.inPutVolt;
            item.outPutCur = this.outPutCur;
			item.dcONOFF = this.dcONOFF;
            return item;
        }
    }
    /// <summary>
    /// 当前运行时间状态
    /// </summary>
    public class CRunTime
    {
        /// <summary>
        /// 当前时序段
        /// </summary>
        public int CurStepNo = 0;
        /// <summary>
        /// 当前运行时间(S)
        /// </summary>
        public int CurRunTime = 0;
        /// <summary>
        /// 当前输入电压(V)
        /// </summary>
        public int CurRunVolt = 0;


        /// <summary>
        /// 当前输出规则编号
        /// </summary>
        public int CurRunOutPut = 0;
		/// <summary>
		/// DCONOFF
		/// </summary>
		public int CurDCONOFF = 0; 
        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>
        public CRunTime Clone()
        {
            CRunTime runTime = new CRunTime();
            runTime.CurStepNo = this.CurStepNo;
            runTime.CurRunTime = this.CurRunTime;
            runTime.CurRunVolt = this.CurRunVolt;            
            runTime.CurRunOutPut = this.CurRunOutPut;
			runTime.CurDCONOFF = this.CurDCONOFF;
            return runTime;
        }
    }
    #endregion

    #region 产品信息
    /// <summary>
    ///老化柜输出及时序
    /// </summary>
    public class CUUT_ONOFF
    {
        /// <summary>
        /// 复制
        /// </summary>
        public CUUT_ONOFF Clone()
        {
            CUUT_ONOFF uut = new CUUT_ONOFF();

            uut.TimeSpec = TimeSpec.Clone();

            uut.TimeRun = TimeRun.Clone();

            for (int i = 0; i < OutPut.Count; i++)
                uut.OutPut.Add(this.OutPut[i].Clone());

            for (int i = 0; i < OnOff.Count; i++)
                uut.OnOff.Add(this.OnOff[i].Clone());
            return uut;
        }
        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="OutPutChan"></param>
        /// <param name="OutPutNum"></param>
        /// <param name="OnOffNum"></param>
        public void AddItem(int OutPutNum, int OutPutChan, int OnOffNum)
        {
            OutPut.Clear();

            OnOff.Clear();

            for (int i = 0; i < OutPutNum; i++)
            {
                OutPut.Add(new COutPutSpec());

                for (int z = 0; z < OutPutChan; z++)
                    OutPut[i].Chan.Add(new CChannel());
            }

            for (int i = 0; i < OnOffNum; i++)
			   OnOff.Add(new CONOFFSpec());
                 
             
        }
        /// <summary>
        /// 时间规格
        /// </summary>
        public CTIME TimeSpec = new CTIME();
        /// <summary>
        /// 输出规格
        /// </summary>
        public List<COutPutSpec> OutPut = new List<COutPutSpec>();
        /// <summary>
        /// ON/OFF参数
        /// </summary>
        public List<CONOFFSpec> OnOff = new List<CONOFFSpec>();

        /// <summary>
        /// 运行时间状态
        /// </summary>
        public CRunTime TimeRun = new CRunTime();

    }

    /// <summary>
    /// 产品测试参数
    /// </summary>
    public class CUUT_Para
    {
        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>
        public CUUT_Para Clone()
        {
            CUUT_Para para = new CUUT_Para();
            para.DoRun = this.DoRun;
            para.DoONOFF = this.DoONOFF;
            para.DoData = this.DoData;

            para.TimerNo = this.TimerNo;
            para.TimerName = this.TimerName;
            para.MesFlag = this.MesFlag;
            para.UserName = this.UserName;
            para.BarFlag = this.BarFlag;
            para.FixBar = this.FixBar;
            para.barSpec = this.barSpec;
            para.barLength = this.barLength;
            para.MO_NO = this.MO_NO;

            para.ModelName = this.ModelName;
            para.BurnTime = this.BurnTime;
            para.OutPutChan = this.OutPutChan;
            para.OutPutNum = this.OutPutNum;
            para.OnOffNum = this.OnOffNum;
            para.StartTime = this.StartTime;
            para.EndTime = this.EndTime;
            para.SavePath = this.SavePath;
            para.SaveWebPath = this.SaveWebPath;
            para.SaveBakPath = this.SaveBakPath;
            para.SaveDataTime = this.SaveDataTime;
            para.SaveDataIndex = this.SaveDataIndex;

            para.SaveReStartTime = this.SaveReStartTime;
            para.SaveCtrlTime = this.SaveCtrlTime;
            para.SaveTempTime = this.SaveTempTime;
            para.FailNum = this.FailNum;
            para.InPutData = this.InPutData;
            para.LoadDataSpec = this.LoadDataSpec;
            para.RunTime = this.RunTime;

            para.BarNum = this.BarNum;
            para.TTNum = this.TTNum;
            para.PassNum = this.PassNum;
            para.PLCAdrs = this.PLCAdrs;
            para.CtrlONOFF = this.CtrlONOFF;
            para.CtrlInPutVolt = this.CtrlInPutVolt;
            para.bInPutErrNum = this.bInPutErrNum;
            para.bNoTran = this.bNoTran;
            para.RunInVolt = this.RunInVolt;
            para.RunSetON = this.RunSetON;
            para.AlarmInfo = this.AlarmInfo;
            para.AlarmTime = this.AlarmTime;
            para.bInVoltErrNum = this.bInVoltErrNum;
            para.bInVoltNum = this.bInVoltNum;
            para.iniSpec = this.iniSpec;
            para.DCONOFF = this.DCONOFF;
             return para;
        }
        /// <summary>
        /// 老化运行状态
        /// </summary>
        public AgingRunType DoRun = AgingRunType.空闲;

        /// <summary>
        /// 老化开关机状态
        /// </summary>
        public AgingONOFFType DoONOFF = AgingONOFFType.空闲;

        /// <summary>
        /// 数据保存模式
        /// </summary>
        public AgingDataType DoData = AgingDataType.空闲;

        /// <summary>
        /// 时序编号
        ///  </summary>
        public int TimerNo = 0;

        /// <summary>
        /// 时序命名
        ///  </summary>
        public string TimerName = string.Empty;

        /// <summary>
        /// MES连线
        /// </summary>
        public int MesFlag = 0;

        /// <summary>
        /// 使用者 
        /// </summary>
        public string UserName = string.Empty;

        /// <summary>
        /// 扫描治具条码
        /// </summary>
        public int FixBar= 0;

        /// <summary>
        /// 扫描产品条码
        /// </summary>
        public int BarFlag = 0;

        /// <summary>
        /// 条码长度
        /// </summary>
        public int barLength = 0;

        /// <summary>
        /// 任务令
        /// </summary>
        public string MO_NO = string.Empty;

        /// <summary>
        /// 条码规则
        /// </summary>
        public string  barSpec = string.Empty ;        

        /// <summary>
        /// 老化机种名
        /// </summary>
        public string ModelName = string.Empty;
        /// <summary>
        /// 老化时间(S)
        /// </summary>
        public int BurnTime = 0;
        /// <summary>
        /// 输出组数
        /// </summary>
        public int OutPutChan = 0;
        /// <summary>
        /// 输出项目数
        /// </summary>
        public int OutPutNum = 0;
        /// <summary>
        /// ONOFF项目数
        /// </summary>
        public int OnOffNum = 0;

        /// <summary>
        /// 老化开始时间
        /// </summary>
        public string StartTime = string.Empty;
        /// <summary>
        /// 老化结束时间
        /// </summary>
        public string EndTime = string.Empty;
        /// <summary>
        /// 保存报表路径
        /// </summary>
        public string SavePath = string.Empty;
        /// <summary>
        /// 报表保存文件名
        /// </summary>
        public string SaveFile = string.Empty;
        /// <summary>
        /// 数据上传路径
        /// </summary>
        public string SaveWebPath = string.Empty;
        /// <summary>
        /// 数据备份路径
        /// </summary>
        public string SaveBakPath = string.Empty;

        /// <summary>
        /// 保存序号
        /// </summary>
        public int SaveDataIndex = 0;

        /// <summary>
        /// 保存报表时间
        /// </summary>
        public string SaveDataTime = string.Empty;

        /// <summary>
        /// 保存控制时间
        /// </summary>
        public string SaveCtrlTime = string.Empty;

        /// <summary>
        /// 保存启动信号读取时间
        /// </summary>
        public string SaveReStartTime = string.Empty;

        /// <summary>
        /// 保存控制时间
        /// </summary>
        public string SaveTempTime = string.Empty;

        /// <summary>
        /// 连续不良次数
        /// </summary>
        public int FailNum = 0;

        /// <summary>
        /// 输入参数设置
        /// </summary>
        public string InPutData = string.Empty;

        /// <summary>
        /// 负载参数设置
        /// </summary>
        public string LoadDataSpec = string.Empty;


        public bool ChooseModel = false;



        /// <summary>
        /// 老化运行时间
        /// </summary>
        public int RunTime = 0;

        /// <summary>
        /// 条码数量
        /// </summary>
        public int BarNum = 0;

        /// <summary>
        /// 老化总数
        /// </summary>
        public int TTNum = 0;

        /// <summary>
        /// 老化良品数
        /// </summary>
        public int PassNum = 0;

        /// <summary>
        /// PLC地址
        /// </summary>
        public int PLCAdrs = 1;

        /// <summary>
        /// 输入接触器状态
        /// </summary>
        public int CtrlONOFF = 0;

        /// <summary>
        /// 输入电压值
        /// </summary>
        public double CtrlInPutVolt = 0;

        /// <summary>
        /// 检测输入异常
        /// </summary>
        public int bInPutErrNum = 0;

        /// <summary>
        /// 不上传MES数据
        /// </summary>
        public bool bNoTran = false;

        /// <summary>
        /// 当前工作电压 
        /// </summary>
        public int RunInVolt = 0;

        /// <summary>
        /// 当前ON状态
        /// </summary>
        public int RunSetON = 0;

        /// <summary>
        /// 检测AC异常
        /// </summary>
        public int bInVoltErrNum = 0;

        /// <summary>
        /// 检测输入电压值异常次数
        /// </summary>
        public int bInVoltNum = 0;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string AlarmInfo = string.Empty;
        /// <summary>
        /// 错误次数
        /// </summary>
        public int AlarmTime = 0;

        /// <summary>
        /// 等待信息
        /// </summary>
        public string waitInfo = string.Empty;
        public bool waitAlarm = false;
        public string waitStartTime = string.Empty;
        public int waitTimes = 0;
        public int iniSpec = 0;

        /// <summary>
        /// DCONOFF
        /// </summary>
		public int DCONOFF = 0;

        


    }

    /// <summary>
    /// PLC运行参数
    /// </summary>
    public class CPLC_Para
    {
        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>
        public CPLC_Para Clone()
        {
            CPLC_Para para = new CPLC_Para();
            para.alarmStat = this.alarmStat;
            para.runStat = this.runStat;
            para.reStartStat = this.reStartStat;
            para.runStartStat = this.runStartStat;
            para.onoffStat = this.onoffStat;
            para.prechargeStat = this.prechargeStat;
            para.tempStat = this.tempStat;
            para.curTemp = this.curTemp;
            para.loadTemp = this.loadTemp;
            para.TempPoint = this.TempPoint;
            para.LTempPoint = this.LTempPoint;
            para.InVolt = this.InVolt;
            para.tempTime = this.tempTime;
            para.tempVal = this.tempVal;
            para.conFailTimes = this.conFailTimes;
            return para;
        }
        /// <summary>
        /// 报警状态
        /// </summary>
        public string alarmStat = string.Empty ;
        /// <summary>
        /// 运行状态
        /// </summary>
        public int runStat= 0;

        /// <summary>
        /// 自检老化状态
        /// </summary>
        public int reStartStat = 0;

        /// <summary>
        /// 开始老化状态
        /// </summary>
        public int runStartStat = 0;

        /// <summary>
        /// ONFF状态
        /// </summary>
        public int onoffStat = 0;

        /// <summary>
        /// 预冲电源状态
        /// </summary>
        public int prechargeStat = 0;

        /// <summary>
        /// 温度状态 1=高温,2=恒温,3=低温,4=关闭,5=异常,6=关闭
        /// </summary>
        public int tempStat = 0;
        /// <summary>
        /// 平均温度
        /// </summary>
        public double curTemp =0;

        /// <summary>
        /// 负载区平均温度
        /// </summary>
        public double loadTemp = 0;

        /// <summary>
        /// 温度点 
        /// </summary>
        public double[] TempPoint =new double[6]{0,0,0,0,0,0} ;

        /// <summary>
        /// 温度点 
        /// </summary>
        public double[] LTempPoint = new double[1] { 0 };

        /// <summary>
        /// 输入电压 
        /// </summary>
        public int InVolt =0;

        /// <summary>
        /// 温度时间 
        /// </summary>
        public List<double> tempTime =new List<double>();

        /// <summary>
        /// 温度值
        /// </summary>
        public List<double > tempVal =new List<double>();

        /// <summary>
        /// PLC状态异常次数 
        /// </summary>
        public int conFailTimes = 0;
    }


    /// <summary>
    /// 设备运行参数
    /// </summary>
    public class CDev_Para
    {
        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>
        public CDev_Para Clone()
        {
            CDev_Para para = new CDev_Para();
            para.DoEL = this.DoEL;
            para.DoFMB_V1 = this.DoFMB_V1;
            para.setfailNum = this.setfailNum;
            return para;
        }

        /// <summary>
        /// 老化负载状态
        /// </summary>
        public AgingELType DoEL = AgingELType.空闲;

        /// <summary>
        /// FMB_V1状态
        /// </summary>
        public AgingELType DoFMB_V1 = AgingELType.空闲;

        /// <summary>
        /// 设定不良次数 
        /// </summary>
        public int[] setfailNum = new int[] { 0 };

    }

    /// <summary>
    /// Mes运行参数
    /// </summary>
    public class CMes_Para
    {
        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>
        public CMes_Para Clone()
        {
            CMes_Para para = new CMes_Para();
            para.DoMes = this.DoMes;
            return para;
        }

        /// <summary>
        /// 老化负载状态
        /// </summary>
        public AgingMesType DoMes = AgingMesType.空闲;


    }

    /// <summary>
    /// 产品信号参数
    /// </summary>
    public class CUUT_LED
    {
        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>
        public CUUT_LED Clone()
        {
            CUUT_LED led = new CUUT_LED();

            led.uutNo = this.uutNo;
            led.timerNo = this.timerNo;
            led.iRoomNo = this.iRoomNo;
            led.iLayer = this.iLayer;
            led.iUUT = this.iUUT;
            led.iCH = this.iCH;

            led.elCom = this.elCom;
            led.elAdrs = this.elAdrs;
            led.elCH = this.elCH;

            led.monCom = this.monCom;
            led.monAdrs = this.monAdrs;
            led.monCH = this.monCH;

            led.barNo = this.barNo;
            led.barType = this.barType;
            led.localPath = this.localPath;
            led.localBar = this.localBar;
            led.fixBar = this.fixBar;
            led.serialNo = this.serialNo;

			led.vName = this.vName;
            led.unitV = this.unitV;
            led.unitA = this.unitA;

            led.dataunitV = this.dataunitV;
            led.dataunitA = this.dataunitA;

            led.result = this.result;
            led.vFailNum = this.vFailNum;
            led.vBack = this.vBack;
            led.iFailNum = this.iFailNum;
            led.iBack = this.iBack;
            led.failEnd = this.failEnd;
            led.failTime = this.failTime;
            led.failInfo = this.failInfo;

            led.iicData = this.iicData;
            led.iicSpec = this.iicSpec;
            led.iicFailNum = this.iicFailNum;
            led.iicBack = this.iicBack;

            led.reportPath = this.reportPath;

            led.tranResult = this.tranResult;
            led.HaveCanId = this.HaveCanId;
            led.iicData = this.iicData;

            led.vMax = this.vMax;
            led.vMin = this.vMin;
            led.Imax = this.Imax;
            led.Imin = this.Imin;
            led.modelName = this.modelName;
            return led;
        }

        /// <summary>
        /// 产品编号
        /// </summary>
        public int uutNo;
        /// <summary>
        /// 时序编号
        /// </summary>
        public int timerNo;
        /// <summary>
        /// 房号
        /// </summary>
        public int iRoomNo;
        /// <summary>
        /// 层号
        /// </summary>
        public int iLayer;
        /// <summary>
        /// 产品号
        /// </summary>
        public int iUUT;
        /// <summary>
        /// 通道号
        /// </summary>
        public int iCH;
        /// <summary>
        /// 电子负载串口
        /// </summary>
        public int elCom;
        /// <summary>
        /// 电子负载地址
        /// </summary>
        public int elAdrs;
        /// <summary>
        /// 电子负载通道
        /// </summary>
        public int elCH;

        /// <summary>
        /// 采集板串口
        /// </summary>
        public int monCom;
        /// <summary>
        /// 采集板地址 
        /// </summary>
        public int monAdrs;
        /// <summary>
        /// 采集板通道
        /// </summary>
        public int monCH;

        /// <summary>
        /// 可扫位置
        /// </summary>
        public int canScanLocal = 0;

        /// <summary>
        /// 条码状态(1=条码在位）
        /// </summary>
        public int barType=0;

        /// <summary>
        /// 条码编号
        /// </summary>
        public int barNo = -1;

        /// <summary>
        /// 位置
        /// </summary>
        public string localPath= string.Empty;

        /// <summary>
        /// 位置条码
        /// </summary>
        public string localBar = string.Empty;

        /// <summary>
        /// 治具条码
        /// </summary>
        public string fixBar = string.Empty;
        /// <summary>
        /// 条码
        /// </summary>
        public string serialNo = string.Empty;
        /// <summary>
        /// 输出名称
        /// </summary>
        public int vUse = 0;
        /// <summary>
        /// 输出名称
        /// </summary>
        public string vName = string.Empty;
        /// <summary>
        /// 输出下限
        /// </summary>
        public double vMin = 0;
        /// <summary>
        /// 输出上限
        /// </summary>
        public double vMax = 0;
        /// <summary>
        /// 负载模式
        /// </summary>
        public int IMode = 0;
        /// <summary>
        /// 当前电流
        /// </summary>
        public double ISet = 0;
        /// <summary>
        /// 电流下限
        /// </summary>
        public double Imin = 0;
        /// <summary>
        /// 电流上限
        /// </summary>
        public double Imax = 0;
        /// <summary>
        /// Von
        /// </summary>
        public double Von = 0;
        /// <summary>
        /// 附加模式
        /// </summary>
        public double AddMode = 0;
        /// <summary>
        /// 快充模式
        /// </summary>
        public double FastChargeType = 0;
        /// <summary>
        /// 快充电压
        /// </summary>
        public double FastChargeVolt= 0;
        /// <summary>
        /// 读取电压
        /// </summary>
        public double unitV = 0;
        /// <summary>
        /// 读取电流 
        /// </summary>
        public double unitA = 0;

        /// <summary>
        /// 读取电压
        /// </summary>
        public double dataunitV = 0;
        /// <summary>
        /// 读取电流 
        /// </summary>
        public double dataunitA = 0;


        /// <summary>
        /// 测试结果(0=空闲,1=良品,2=欠压,3=过压,4=欠流,5=过流,6=当机,7=无输出)
        /// </summary>
        public int result = 0;
        /// <summary>
        /// 电压不良次数记录
        /// </summary>
        public int vFailNum = 0;
        /// <summary>
        /// 老化电压备份
        /// </summary>
        public double vBack = 0;
        /// <summary>
        /// 电流不良次数记录
        /// </summary>
        public int iFailNum = 0;
        /// <summary>
        /// 老化电流备份
        /// </summary>
        public double iBack = 0;

        /// <summary>
        /// IIC不良次数记录
        /// </summary>
        public int iicFailNum = 0;
        /// <summary>
        /// 老化IIC数据备份
        /// </summary>
        public string iicBack = string.Empty ;
        /// <summary>
        /// 不良标志
        /// </summary>
        public int failEnd = 0;
        /// <summary>
        /// 不良时间
        /// </summary>
        public string failTime = string.Empty;
        /// <summary>
        /// 不良信息
        /// </summary>
        public string failInfo = string.Empty;

        /// <summary>
        /// IIC数据
        /// </summary>
        public string iicData = string.Empty;

        /// <summary>
        /// IICSPEC规格
        /// </summary>
        public string iicSpec = string.Empty;

        /// <summary>
        /// 报表路径
        /// </summary>
        public string reportPath = string.Empty;

        /// <summary>
        /// 上传结果
        /// </summary>
        public int tranResult = 0;

        /// <summary>
        /// 是否读取到CANID数
        /// </summary>
        public bool  HaveCanId = false ;

        /// <summary>
        /// 机种信息
        /// </summary>
        public string modelName = "";

    }

    /// <summary>
    /// 子治具信息
    /// </summary>
    public class CUUT
    {
        public CUUT()
        {
            for (int i = 0; i < CAgingApp.TimerChanMax ; i++)
                Led.Add(new CUUT_LED());
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>
        public CUUT Clone()
        {
            CUUT uut = new CUUT();

            uut.Para = this.Para.Clone();

            uut.OnOff = this.OnOff.Clone();

            uut.PLC = this.PLC.Clone();

            uut.Dev = this.Dev.Clone();

            uut.Mes = this.Mes.Clone();

            for (int i = 0; i < CAgingApp.TimerChanMax; i++)
                uut.Led[i] = this.Led[i].Clone();

            return uut;
        }

        /// <summary>
        /// 测试参数
        /// </summary>
        public CUUT_Para Para = new CUUT_Para();

        /// <summary>
        /// 输出与时序关系
        /// </summary>
        public CUUT_ONOFF OnOff = new CUUT_ONOFF();

        /// <summary>
        /// PLC状态读取
        /// </summary>
        public CPLC_Para PLC = new CPLC_Para();

        /// <summary>
        /// EL状态读取
        /// </summary>
        public CDev_Para Dev = new CDev_Para();
        /// <summary>
        /// Mes状态读取
        /// </summary>
        public CMes_Para Mes = new CMes_Para(); 

        /// <summary>
        /// 电压电流输出
        /// </summary>
        public List<CUUT_LED> Led = new List<CUUT_LED>();
    }
    #endregion

    #region 老化库体信息
    /// <summary>
    /// 动作状态 
    /// </summary>
    public enum ERun
    {
        空闲,
        自检,
        运行,
        暂停
    }

    /// <summary>
    /// 运行状态
    /// </summary>
    public class CRunStat
    {
        /// <summary>
        /// 状态标识
        /// </summary>
        public ERun doRun = ERun.空闲;
    }



    /// <summary>
    /// 产品信息
    /// </summary>
    public class CFixture
    {
        public int MesFlag = 0;
        public string modelName = string.Empty;
        public int inVolt = 0;
        public List<string> serialNo = new List<string>();
        public List<int> result = new List<int>();
        public List<int> resultId = new List<int>();
        public CFixture()
        {
            serialNo.Clear();
            result.Clear();
            resultId.Clear();
            for (int i = 0; i < CAgingApp.TimerChanMax; i++)
            {
                serialNo.Add("");
                result.Add(0);
                resultId.Add(0);
            }
        }
    }

    /// <summary>
    /// 读PLC信号
    /// </summary>
    public class CPLCSignal
    {
        /// <summary>
        /// PLC运行状态
        /// </summary>
        public string sysStat = string.Empty;
        /// <summary>
        /// 平均温度值
        /// </summary>
        public double rTemp = 0;
        /// <summary>
        /// 5个温度读值
        /// </summary>
        public double[] rTempPoint = new double[5];

    }

    /// <summary>
    /// 老化房单个时序的产品统计
    /// </summary>
    public class CAreaYield
    {
        /// <summary>
        /// 默认机种路径
        /// </summary>
        public string defaultModelPath = string.Empty;
        /// <summary>
        /// 当前老化产品数
        /// </summary>
        public int BITTNum = 0;
        /// <summary>
        /// 当前老化良品数
        /// </summary>
        public int BIPASSNum = 0;

    }
    /// <summary>
    /// 当日产能
    /// </summary>
    public class CDailyYield
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int ttNum;
        /// <summary>
        /// 不良数
        /// </summary>
        public int failNum;
        /// <summary>
        /// 本地时间
        /// </summary>
        public string dayNow;
        /// <summary>
        /// 累计总数
        /// </summary>
        public int yieldTTNum;
        /// <summary>
        /// 累计不良数
        /// </summary>
        public int yieldFailNum;
    }
    /// <summary>
    /// 库体运行状态
    /// </summary>
    public class CCHmrStatus
    {
        /// <summary>
        /// 运行信息
        /// </summary>
        public CRunStat status = new CRunStat();
        /// <summary>
        /// PLC信号解析
        /// </summary>
        public CPLCSignal plc = new CPLCSignal();
        /// <summary>
        /// 治具产品统计
        /// </summary>
        public CAreaYield areaYield = new CAreaYield();
        /// <summary>
        /// 日统计
        /// </summary>
        public CDailyYield dayYield = new CDailyYield();
        /// <summary>
        /// 当前出老化机种
        /// </summary>
        public string curOutModel = string.Empty;
    }
    #endregion

    #region 参数类
    /// <summary>
    /// 参数类
    /// </summary>
    public class CPara
    {
        /// <summary>
        /// 由机种参数获取输出规格及ONOFF规格
        /// </summary>
        /// <param name="OnOff"></param>
        /// <param name="model"></param>
        public static bool GetOutPutAndOnOffFromModel(CModelPara runModel, ref CUUT_ONOFF OnOff, out string er)
        {
            er = string.Empty;

            try
            {
                if (runModel == null)
                {
                    er = "机种参数不存在";
                    return false;
                }

                OnOff.TimeSpec.BITime = (int)(runModel.Para.BITime );

                OnOff.TimeSpec.OutChanNum = runModel.Para.OutPut_Chan;

                OnOff.TimeSpec.OutPutNum = runModel.Para.OutPut_Num;

                OnOff.TimeSpec.OnOffNum = runModel.Para.OnOff_Num;

                //获取输出规格

                OnOff.OutPut.Clear();

                for (int i = 0; i < OnOff.TimeSpec.OutPutNum; i++)
                {
                    COutPutSpec outPut = new COutPutSpec();
                    outPut.iicSpec = runModel.OutPut[i].SpecData;
                    for (int j = 0; j < OnOff.TimeSpec.OutChanNum; j++)
                    {
                        outPut.Chan.Add(new CChannel());

                        outPut.Chan[j].Vuse = runModel.OutPut[i].Chan[j].Vuse ;

                        outPut.Chan[j].Vname = runModel.OutPut[i].Chan[j].Vname;

                        outPut.Chan[j].Vmin = runModel.OutPut[i].Chan[j].Vmin;

                        outPut.Chan[j].Vmax = runModel.OutPut[i].Chan[j].Vmax;

                        outPut.Chan[j].Imode = runModel.OutPut[i].Chan[j].Imode;

                        outPut.Chan[j].ISet = runModel.OutPut[i].Chan[j].ISet;

                        outPut.Chan[j].Imin = runModel.OutPut[i].Chan[j].Imin;

                        outPut.Chan[j].Imax = runModel.OutPut[i].Chan[j].Imax;

                        outPut.Chan[j].Von = runModel.OutPut[i].Chan[j].Von;

                        outPut.Chan[j].AddMode = runModel.OutPut[i].Chan[j].AddMode ;
                    }

                    OnOff.OutPut.Add(outPut);
                }


                //获取ONOFF规格

                List<CONOFFSpec> onOffList = new List<CONOFFSpec>();

                for (int i = 0; i < OnOff.TimeSpec.OnOffNum; i++)
                {
                    int leftTotalTime = runModel.OnOff[i].TotalTime;

                    while (leftTotalTime > 0)
                    {
                        for (int j = 0; j < runModel.OnOff[i].Item.Length; j++)
                        {
                            int itemTotalTime = runModel.OnOff[i].Item[j].OnOffTime *
                                               (runModel.OnOff[i].Item[j].OnTime + runModel.OnOff[i].Item[j].OffTime);

                            if (itemTotalTime == 0)
                                continue;

                            for (int z = 0; z < runModel.OnOff[i].Item[j].OnOffTime; z++)
                            {
                                int ItemTime = runModel.OnOff[i].Item[j].OnTime + runModel.OnOff[i].Item[j].OffTime;

                                if (leftTotalTime < ItemTime)  //剩余时间<ON/OFF组时间
                                {
                                    leftTotalTime = 0;

                                    ItemTime = leftTotalTime;
                                }
                                else
                                {
                                    leftTotalTime -= ItemTime;
                                }

                                CONOFFSpec onff = new CONOFFSpec();

                                onff.inPutVolt = runModel.OnOff[i].Item[j].InPutV ;

                                onff.outPutCur = runModel.OnOff[i].Item[j].OutPutType;

								onff.dcONOFF = runModel.OnOff[i].Item[j].dcONOFF;

                                 onff.OnOffTime = ItemTime;

                                if (itemTotalTime >= runModel.OnOff[i].Item[j].OnTime)
                                {
                                    onff.OnTime = runModel.OnOff[i].Item[j].OnTime;
                                    onff.OffTime = ItemTime - runModel.OnOff[i].Item[j].OnTime;
                                }
                                else
                                {
                                    onff.OnTime = ItemTime;
                                    onff.OffTime = 0;
                                }

                                onOffList.Add(onff);

                                if (leftTotalTime == 0)
                                    break;
                            }
                        }
                    
                    }
                    
                }

                //计算实际ON/OFF

                OnOff.OnOff.Clear();

                int leftTime = OnOff.TimeSpec.BITime;

                while (leftTime > 0)
                {
                    for (int i = 0; i < onOffList.Count; i++)
                    {
                        int itemTime = onOffList[i].OnOffTime;

                        if (leftTime < itemTime)  //剩余时间<ON/OFF组时间
                        {
                            itemTime = leftTime;

                            leftTime = 0;
                        }
                        else
                        {
                            leftTime -= itemTime;
                        }

                        CONOFFSpec onoff = new CONOFFSpec();

                        onoff.inPutVolt = onOffList[i].inPutVolt;

                        onoff.outPutCur = onOffList[i].outPutCur;

						onoff.dcONOFF = onOffList[i].dcONOFF;

                        onoff.OnOffTime = itemTime;

                        if (itemTime >= onOffList[i].OnTime)
                        {
                            onoff.OnTime = onOffList[i].OnTime;
                            onoff.OffTime = itemTime - onOffList[i].OnTime;
                        }
                        else
                        {
                            onoff.OnTime = itemTime;
                            onoff.OffTime = 0;
                        }

                        OnOff.OnOff.Add(onoff);

                        if (leftTime == 0)
                            break;
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
        /// 获取当前时序步骤
        /// </summary>
        /// <param name="runTime"></param>
        /// <param name="onOffList"></param>
        /// <param name="runStep"></param>
        /// <returns></returns>
        public static bool GetCurStepFromOnOff(int runTime, List<CONOFFSpec> onOffList, out CRunTime runStep, out string er)
        {
            runStep = null;

            er = string.Empty;

            try
            {
                int leftTime = runTime;
                if (leftTime == 0)
                {
                    runStep = new CRunTime();
                    runStep.CurRunTime = runTime;
                    runStep.CurStepNo = 0;
                    runStep.CurRunOutPut = onOffList[0].outPutCur;
                    runStep.CurDCONOFF = onOffList[0].dcONOFF;
                    runStep.CurRunVolt = onOffList[0].inPutVolt;
                    return true;
                }
                while (leftTime > 0)
                {
                    for (int i = 0; i < onOffList.Count; i++)
                    {
                        int itemTime = onOffList[i].OnOffTime;

                        if (leftTime <= itemTime)  //剩余时间<ON/OFF组时间
                        {
                            itemTime = leftTime;

                            leftTime = 0;
                        }
                        else
                        {
                            leftTime -= itemTime;
                        }

                        //当前ON/OFF段
                        if (leftTime == 0)
                        {
                            runStep = new CRunTime();
                            runStep.CurRunTime = runTime;
                            runStep.CurStepNo = i;
                            runStep.CurRunOutPut = onOffList[i].outPutCur;
							runStep.CurDCONOFF = onOffList[i].dcONOFF;
                            if (itemTime <= onOffList[i].OnTime)
                                runStep.CurRunVolt = onOffList[i].inPutVolt;
                            else
                                runStep.CurRunVolt = 0;
                            break;
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
    }
    #endregion
   
}
