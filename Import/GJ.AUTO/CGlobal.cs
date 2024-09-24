using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data;
using GJ.PDB;
using GJ.COM;
namespace GJ.AUTO
{
    public class CGlobal
    {
        #region 设备信息
        public static string equipmentID = "BILH0100225007";

        public static string equipmentName = "节能老化柜";

        public static string equipmentStation = "老化测试位";

        public static string projectID = "BT3-GB240304-4";

        public static string projectName = "常州光宝节能负载老化柜";
        #endregion
        #region 系统配置 
        public static string sysDB = "SysConfig.accdb";

        /// <summary>
        /// 配置登录名
        /// </summary>
        public static string UserFile = Application.StartupPath + "\\" + "User.ini";

        #endregion

        #region 用户登录
        /// <summary>
        /// 超级用户名
        /// </summary>
        public static string superUser = "GUANJIA";
        /// <summary>
        /// 超级密码
        /// </summary>
        public static string superPwr = "GuanJia";
        /// <summary>
        /// 用户权限等级
        /// </summary>
        private const int C_PWR_LEVEL_MAX = 8;
        /// <summary>
        /// 用户信息
        /// </summary>
        public class User
        {
            /// <summary>
            /// 登录用户
            /// </summary>
            public static string mlogName;
            /// <summary>
            /// 登录密码
            /// </summary>
            public static string mlogpassword;
            /// <summary>
            /// 登录权限
            /// </summary>
            public static int[] mLevel = new int[C_PWR_LEVEL_MAX];
        }
        #endregion

        #region 工站信息
        public class CFlow
        {
            #region 字段
            /// <summary>
            /// 配置文件
            /// </summary>
            private static string iniFile = Application.StartupPath + "\\iniFile.ini";
            /// <summary>
            /// 编号
            /// </summary>
            public static int idNo;
            /// <summary>
            /// 站别编号
            /// </summary>
            public static int FlowID;
            /// <summary>
            /// 站别唯一标识
            /// </summary>
            public static string FlowGUID;
            /// <summary>
            /// 站别名称
            /// </summary>
            public static string FlowDes;
            /// <summary>
            /// 子站别:0,1,2
            /// </summary>
            public static int FlowSub;
            /// <summary>
            /// 图标编号
            /// </summary>
            public static int FlowIcon;
            /// <summary>
            /// 类命名空间
            /// </summary>
            public static string NameSpace;
            #endregion

            /// <summary>
            /// 加载当前测试位
            /// </summary>
            /// <returns></returns>
            public static bool load(ref string er)
            {
                try
                {
                    if (!File.Exists(iniFile))
                    {
                        er = "找不到系统配置文件";
                        return false;
                    }
                    string GUIDTemp = CIniFile.ReadFromIni("STATION", "FlowGUID", iniFile);
                    if (GUIDTemp == "")
                    {
                        er = "初始化系统配置文件错误";
                        return false;
                    }
                    return getFlowInfo(GUIDTemp, ref er);
                }
                catch (Exception ex)
                {
                    er = ex.ToString();
                    return false;
                }
            }
            /// <summary>
            /// 保存当前测试站
            /// </summary>
            /// <param name="idNo"></param>
            public static void save()
            {
                CIniFile.WriteToIni("STATION", "FlowGUID", FlowGUID, iniFile);
            }
            /// <summary>
            /// 获取当前测试站信息
            /// </summary>
            /// <param name="wGUID"></param>
            /// <param name="er"></param>
            /// <returns></returns>
            public static bool getFlowInfo(string wGUID, ref string er)
            {
                try
                {
                    //获取测试站
                    CDBCOM db = new CDBCOM(EDBType.Access);
                    DataSet ds = new DataSet();
                    string sqlCmd = "select * from FlowInfo where FlowGUID='" + wGUID + "' and FlowUsed=1 order by FlowID,FlowSub";
                    if (!db.QuerySQL(sqlCmd, out ds, out er))
                        return false;
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        er = "当前测试站不存在";
                        return false;
                    }
                    FlowID = System.Convert.ToInt16(ds.Tables[0].Rows[0]["FlowID"].ToString());
                    FlowGUID = ds.Tables[0].Rows[0]["FlowGUID"].ToString();
                    FlowDes = ds.Tables[0].Rows[0]["FlowDes"].ToString();
                    FlowSub = System.Convert.ToInt16(ds.Tables[0].Rows[0]["FlowSub"].ToString());
                    FlowIcon = System.Convert.ToInt16(ds.Tables[0].Rows[0]["FlowIcon"].ToString());
                    NameSpace = ds.Tables[0].Rows[0]["NameSpace"].ToString();
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
}
