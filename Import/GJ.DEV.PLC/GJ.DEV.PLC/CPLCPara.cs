using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ.DEV.PLC
{
    #region 状态定义
    /// <summary>
    /// PLC类型
    /// </summary>
    public enum EPlcType
    {
        /// <summary>
        /// 汇川TCP
        /// </summary>
        Inovance_TCP,
        /// <summary>
        /// 汇川串口
        /// </summary>
        Inovance_COM,
        /// <summary>
        /// 欧姆龙TCP
        /// </summary>
        Omron_TCP,
        /// <summary>
        /// 欧姆龙UDP
        /// </summary>
        Omron_UDP,
        /// <summary>
        /// 欧姆龙串口
        /// </summary>
        Omron_COM,
        /// <summary>
        /// 台达串口
        /// </summary>
        Delta_COM,
        /// <summary>
        /// 产电TCP
        /// </summary>
        Lsis_TCP
    }
    /// <summary>
    /// 连接状态
    /// </summary>
    public enum EStatus
    {
        空闲,
        连接中,
        正常,
        断开,
        错误
    }
    /// <summary>
    /// 通信接口
    /// </summary>
    public enum EConType
    {
        RS232,
        TCP
    }
    /// <summary>
    /// 线圈:M,W,X,Y;寄存器:D
    /// </summary>
    public enum ERegType
    {
        M,
        W,
        D,
        X,
        Y,
        WB
    }
    /// <summary>
    /// 消息状态
    /// </summary>
    public enum EMessage
    {
        启动,
        正常,
        异常,
        暂停,
        退出
    }
    #endregion

    #region PLC消息
    /// <summary>
    /// 定义状态消息
    /// </summary>
    public class CPLCConArgs : EventArgs
    {
        public CPLCConArgs(int idNo, string name, string status, EMessage e)
        {
            this.idNo = idNo;
            this.name = name;
            this.status = status;
            this.e = e;
        }
        public readonly int idNo;
        public readonly string name;
        public readonly string status;
        public readonly EMessage e;
    }
    /// <summary>
    /// 定义数据消息
    /// </summary>
    public class CPLCDataArgs : EventArgs
    {
        public CPLCDataArgs(int idNo, string name, string rData, EMessage e)
        {
            this.idNo = idNo;
            this.name = name;
            this.rData = rData;
            this.e = e;
        }
        public readonly int idNo;
        public readonly string name;
        public readonly string rData;
        public readonly EMessage e;
    }
    #endregion

    #region 参数类
    public class CPLCPara
    {
        #region 常量定义
        public const int ON = 1;
        public const int OFF = 0;
        #endregion
    }
    #endregion
   

}
