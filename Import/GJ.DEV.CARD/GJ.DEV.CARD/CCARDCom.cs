using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;  
namespace GJ.DEV.CARD
{
    /// <summary>
    /// ID Card公共类
    /// </summary>
    public class CCARDCom
    {
      #region 构造函数
      public CCARDCom(ECardType cardType, int idNo = 0, string name = "IFMB",string version="A")
      {
        this._idNo = idNo;

        this._name = name;

        this._cardType = cardType;

        this._version = version;

        //反射获取PLC类型

        string plcModule = "C" + _cardType.ToString();

        Assembly asb = Assembly.GetAssembly(typeof(ICARD));

        Type[] types = asb.GetTypes();

        object[] parameters = new object[2];

        parameters[0] = _idNo;

        parameters[1] = _name;

        foreach (Type t in types)
        {
            if (t.Name == plcModule && t.GetInterface("ICARD") != null)
            {
                _devCard = (ICARD)asb.CreateInstance(t.FullName, true, System.Reflection.BindingFlags.Default, null, parameters, null, null);
                _devCard.version = version; 
                break;
            }
        }
      }
      public override string ToString()
      {
          return _name;
      }
      #endregion

      #region 字段
      private int _idNo = 0;
      private string _name = string.Empty;
      private bool _conStatus = false;
      private ECardType _cardType = ECardType.MFID;
      private string _version = "B";
      private ICARD _devCard = null;
      private ReaderWriterLock idLock = new ReaderWriterLock();
      #endregion

      #region 属性
      /// <summary>
      /// 编号
      /// </summary>
      public int idNo
      {
          set
          {
              _idNo = value;
          }
          get
          {
              return _idNo;
          }
      }
      /// <summary>
      /// 名称
      /// </summary>
      public string name
      {
          set
          {
              _name = value;
          }
          get
          {
              return _name;
          }
      }
      /// <summary>
      /// 连接状态
      /// </summary>
      public bool conStatus
      {
          get
          {
              return _conStatus;
          }
      }
      /// <summary>
      /// 版本
      /// </summary>
      public string version
      {
          set {                
                   if (_devCard != null)
                   {
                       _version = value;
                       _devCard.version = _version; 
                  
                   }
               }
          get {
              return _version;
              }
      }
      #endregion

       #region 方法
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="comName"></param>
        /// <param name="setting"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool Open(string comName, out string er, string setting="19200,E,8,1")
        {
            return _devCard.Open(comName, out er, setting);  
        }
        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <returns></returns>
        public void Close()
        {
            _devCard.Close(); 
        }
        /// <summary>
        /// 读取ID卡值
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="rIdCard"></param>
        /// <param name="er"></param>
        /// <param name="clrFlag"></param>
        /// <param name="delayMs"></param>
        public bool ReadIdCard(int idAddr, out string rIdCard, out string er,int idTimes, bool clrFlag = true, int delayMs=250)
        {
            rIdCard = string.Empty;

            er = string.Empty;

            try
            {
                idLock.AcquireWriterLock(-1);

                System.Threading.Thread.Sleep(delayMs);

                if (clrFlag)
                {
                    if (!_devCard.GetRecord(idAddr, out rIdCard, out er))
                        return false;
                    System.Threading.Thread.Sleep(delayMs);
                }

                if (!_devCard.GetRecord(idAddr, out rIdCard, out er))
                {
                    for (int i = 0; i < idTimes; i++)
                    {
                        System.Threading.Thread.Sleep(delayMs);

                        if (_devCard.GetRecord(idAddr, out rIdCard, out er))
                            return true;
                    }
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                idLock.ReleaseWriterLock(); 
            }
        }
        /// <summary>
        /// 读取对应地址的卡号
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="rSn"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool GetRecorderSn(int idAddr, out string rSn, out string er)
        {
            return _devCard.GetRecorderSn(idAddr, out rSn, out er);  
        }
        /// <summary>
        /// 设置卡号的地址
        /// </summary>
        /// <param name="strSn"></param>
        /// <param name="idAddr"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool SetRecorderID(string strSn, int idAddr, out string er)
        {
            return _devCard.SetRecorderID(strSn, idAddr, out er);  
        }
        /// <summary>
        /// 读取卡号的地址
        /// </summary>
        /// <param name="strSn"></param>
        /// <param name="idAddr"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool GetRecorderID(string strSn, out int idAddr, out string er)
        {
            return _devCard.GetRecorderID(strSn, out idAddr, out er);  
        }
        /// <summary>
        /// 读取地址编号卡号资料
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="rSn"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool GetRecord(int idAddr, out string rIDCard, out string er)
        {
            return _devCard.GetRecord(idAddr, out rIDCard, out er);  
        }
        /// <summary>
        /// 读取地址编号卡号资料
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="rSn"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool GetRecordAgain(int idAddr, out string rIDCard, out string er)
        {
            return _devCard.GetRecordAgain(idAddr, out rIDCard, out er);   
        }
        /// <summary>
        /// 读取触发信号状态和卡片资料(有卡片感应)-触发信号为1
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="rSn"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool GetRecordTriggerSignal(int idAddr, out string rIDCard, out bool rTrigger, out string er)
        {
            return _devCard.GetRecordTriggerSignal(idAddr, out rIDCard, out rTrigger, out er);  
        }
        /// <summary>
        /// 设置读卡器工作模式(广播)
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool SetRecorderWorkMode(EMode mode, out string er)
        {
            return _devCard.SetRecorderWorkMode(mode, out er);  
        }
        /// <summary>
        /// 设置读卡器工作模式
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="mode"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool SetRecorderWorkMode(int idAddr, EMode mode, out string er)
        {
            return _devCard.SetRecorderWorkMode(idAddr, mode, out er);    
        }
        /// <summary>
        /// 读取读卡器工作模式
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="mode"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool GetRecordMode(int idAddr, out EMode mode, out string er)
        {
            return _devCard.GetRecordMode(idAddr, out mode, out er);  
        }
        /// <summary>
        /// 读取读卡器触发信号状态
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="Trigger"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool GetTriggerSignal(int idAddr, out bool Trigger, out string er)
        {
            return _devCard.GetTriggerSignal(idAddr, out Trigger, out er);    
        }
        #endregion

    }
}
