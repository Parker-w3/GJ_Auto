using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ.DEV.CARD
{
    /// <summary>
    /// IO类型
    /// </summary>
    public enum ECardType
    {
        MFID
    }
    /// <summary>
    /// 读卡器版本
    /// </summary>
    public enum EVersion
    {
        A,   //旧版本
        B    //新版本
    }
    /// <summary>
    /// 读卡器工作模式
    /// </summary>
    public enum EMode
    {
        /// <summary>
        /// 指令读取模式: 只能用相关指令读取卡片资料(出厂模式)
        /// </summary>
        A,
        /// <summary>
        /// 直接模式:读到卡片资料自动发送 (如果连续在0.5S内读到同一卡片将不发送)
        /// </summary>
        B,
        /// <summary>
        /// 外部触发模式:  收到外部电平触发时读取卡片资料并发送(电平触发保持时间>200ms)
        /// </summary>
        C
    }
}
