using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GJ.APP;
namespace GJ.USER.APP
{
    public class CAgingApp:IAPP
    {
        #region 字段
        /// <summary>
        /// 项目编号
        /// </summary>
        public static string ProId = "BT3-GB240304-4";
         /// <summary>
         /// 单个时序老化柜最大通道数
         /// </summary>
        public static int TimerChanMax = 192;
        /// <summary>
        /// 治具方向 0:(1->9)
        /// </summary>
        public static int FixPos = 0;
        /// <summary>
        /// 线体编号
        /// </summary>
        public static int LineNo = 0;
        /// <summary>
        /// 线体名称
        /// </summary>
        public static string LineName = string.Empty;

        /// <summary>
        /// 快充模式
        /// </summary>
        public static string QCM_Type = "Normal,QC2_0,QC3_0,FCP,SCP,PD3_0,PE3_0,PE1_0,PE2_0,MTK1_0,MTK2_0";
        /// <summary>
        /// 自动扫描条码枪1对应产品序号
        /// </summary>
        public static string LOADUP_Auto_SnNo1 = "1,2,3,4,5,6,7,8,9,10";
        /// <summary>
        /// 自动扫描条码枪2对应产品序号
        /// </summary>
        public static string LOADUP_Auto_SnNo2 = "20,19,18,17,16,15,14,13,12,11";
        /// <summary>
        /// 绑定流程编号
        /// </summary>
        public static int LOADUP_FlowId = 0;
        /// <summary>
        /// 绑定流程名称
        /// </summary>
        public static string LOADUP_FlowName = "LOADUP";
        /// <summary>
        /// 初测流程编号
        /// </summary>
        public static int PREATE_FlowId = 1;
        /// <summary>
        /// 初测流程名称
        /// </summary>
        public static string PREATE_FlowName = "PREATE";
        /// <summary>
        /// 老化流程编号
        /// </summary>
        public static int BI_FlowId = 1;
        /// <summary>
        /// 老化流程名称
        /// </summary>
        public static string BI_FlowName = "BURNIN";
        /// <summary>
        /// 高压流程编号
        /// </summary>
        public static int HIPOT_FlowId = 3;
        /// <summary>
        /// 高压流程名称
        /// </summary>
        public static string HIPOT_FlowName = "HIPOT";
        /// <summary>
        /// 终测流程编号
        /// </summary>
        public static int FINALATE_FlowId = 4;
        /// <summary>
        /// 终测流程名称
        /// </summary>
        public static string FINALATE_FlowName = "FINALATE";
        /// <summary>
        /// 下机流程编号
        /// </summary>
        public static int UNLOAD_FlowId = 2;
        /// <summary>
        /// 下机流程名称
        /// </summary>
        public static string UNLOAD_FlowName = "UNLOAD";
        /// <summary>
        /// 客户MES
        /// </summary>
        public static string DllFile = "http://cnaecnpiapp04/eTrace_OracleERP/eTraceOracleERP.asmx";
        /// <summary>
        /// 客户User
        /// </summary>
        public static string DllUser = "0138182942";
        /// <summary>
        /// 客户夹具类型
        /// </summary>
        public static string DllFixType = "20In1";
        /// <summary>
        /// 客户绑定工位
        /// </summary>
        public static string Dll_PREIN = "USBC LOCM";
        /// <summary>
        /// 客户绑定工位
        /// </summary>
        public static string Dll_POWERUP = "POWERUP";
        /// <summary>
        /// 客户初测工位
        /// </summary>
        public static string Dll_PREATE = "PRE ATE";
        /// <summary>
        /// 客户老化工位
        /// </summary>
        public static string Dll_BURNIN = "BURNIN";
        /// <summary>
        /// 客户高压工位
        /// </summary>
        public static string Dll_HIPOT = "HIPOT";
        /// <summary>
        /// 客户终测工位
        /// </summary>
        public static string Dll_FINALATE = "FINAL ATE";
        #endregion

        #region 方法
        /// <summary>
        /// 初始化设置
        /// </summary>
        public void loadAppSetting()
        {
          
        }
        #endregion
       
    }
}
