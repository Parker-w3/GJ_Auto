using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GJ.DEV.CARD;

namespace GJ.DEV.CARD
{
    public interface ICARD
    {
        #region 属性
        /// <summary>
        /// 设备ID
        /// </summary>
        int idNo
        { set; get; }
        /// <summary>
        /// 设备名称
        /// </summary>
        string name
        { set; get; }
        /// <summary>
        /// 连接状态
        /// </summary>
        bool conStatus
        { get; }
        /// <summary>
        /// 版本
        /// </summary>
        string version
        {            
            set;
            get;
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
        bool Open(string comName, out string er, string setting);
        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <returns></returns>
        void Close();
        /// <summary>
        /// 读取对应地址的卡号
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="rSn"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool GetRecorderSn(int idAddr, out string rSn, out string er);
        /// <summary>
        /// 设置卡号的地址
        /// </summary>
        /// <param name="strSn"></param>
        /// <param name="idAddr"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool SetRecorderID(string strSn, int idAddr, out string er);
        /// <summary>
        /// 读取卡号的地址
        /// </summary>
        /// <param name="strSn"></param>
        /// <param name="idAddr"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool GetRecorderID(string strSn, out int idAddr, out string er);
        /// <summary>
        /// 读取地址编号卡号资料
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="rSn"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool GetRecord(int idAddr, out string rSn, out string er);
        /// <summary>
        /// 读取地址编号卡号资料
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="rSn"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool GetRecordAgain(int idAddr, out string rSn, out string er);
        /// <summary>
        /// 读取触发信号状态和卡片资料(有卡片感应)-触发信号为1
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="rSn"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool GetRecordTriggerSignal(int idAddr, out string rSn, out bool rTrigger, out string er);
        /// <summary>
        /// 设置读卡器工作模式(广播)
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool SetRecorderWorkMode(EMode mode, out string er);
        /// <summary>
        /// 设置读卡器工作模式
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="mode"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool SetRecorderWorkMode(int idAddr, EMode mode, out string er);
        /// <summary>
        /// 读取读卡器工作模式
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="mode"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool GetRecordMode(int idAddr, out EMode mode, out string er);
        /// <summary>
        /// 读取读卡器触发信号状态
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="Trigger"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool GetTriggerSignal(int idAddr, out bool Trigger, out string er);
        #endregion
    }
}
