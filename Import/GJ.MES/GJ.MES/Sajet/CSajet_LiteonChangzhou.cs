using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace GJ.MES.Sajet
{
    public class CSajet_LiteonChangzhou
    {
        [DllImport("SAJETCONNECT.DLL", CallingConvention = CallingConvention.StdCall)]
        private static extern bool SajetTransStart();

        [DllImport("SAJETCONNECT.DLL", CallingConvention = CallingConvention.StdCall)]
        private static extern bool SajetTransClose();

        [DllImport("SAJETCONNECT.DLL", EntryPoint = "SajetTransData", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern bool SajetTransData(int f_iCommandNo, System.Text.StringBuilder f_pData, ref int f_pLen);

        #region 通用方法
        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public static bool Connect()
        {
            if (!SajetTransStart())
                return false;

            return true;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public static bool Close()
        {
            return SajetTransClose();
        }
        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="cmdNo"></param>
        /// <param name="pData"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private static bool SendCmd(int cmdNo, string pData, out string er)
        {
            er = string.Empty;
            try
            {
                StringBuilder f_pData = new StringBuilder(255);

                int f_iCommandNo = cmdNo;

                f_pData.Append(pData);

                int f_pLen = pData.Length;

                if (!SajetTransData(f_iCommandNo, f_pData, ref f_pLen))
                {
                    er = f_pData.ToString().Substring(0, f_pData.Length);
                    return false;
                }

                er = f_pData.ToString().Substring(0, f_pData.Length);

                if (!er.Contains("OK"))
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        #endregion

        #region 特定方法
        /// <summary>
        /// 登录MES CMD=1;
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pwr"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool LogIn(string user, string pwr, out string er)
        {
            er = string.Empty;

            try
            {
                string CmdInfo = user + ";" + pwr + ";";

                if (!SendCmd(1, CmdInfo, out er))
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }

        /// <summary>
        /// 进站绑定
        /// </summary>
        /// <param name="UserId">工号</param>
        /// <param name="ChmrId">台车码</param>
        /// <param name="localBar">位置条码</param>
        /// <param name="ScanBar">产品条码</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool CheckIn(string UserId, string ChmrId, string localBar, string ScanBar, out string model, out string er)
        {
            er = string.Empty;
            model = string.Empty;
            try
            {
                string CmdInfo = UserId + ";" + ChmrId + ";" + localBar + ";" + ScanBar + ";";

                if (!SendCmd(7, CmdInfo, out er))
                    return false;
                List<string> str = er.Split(';').ToList();
                if (str.Count < 2)
                    return false;
                model = str[1];
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }

        /// <summary>
        /// 开始老化
        /// </summary>
        /// <param name="UserId">工号</param>
        /// <param name="ChmrId">台车码</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool StartBI(string UserId, string ChmrId, out string er)
        {
            er = string.Empty;

            try
            {
                string CmdInfo = UserId + ";" + ChmrId + ";";

                if (!SendCmd(8, CmdInfo, out er))
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }

        /// <summary>
        /// 上传不良条码
        /// </summary>
        /// <param name="UserId">工号</param>
        /// <param name="barcoe">条码</param>
        /// <param name="Failcode">不良代码</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool TranFailBarCode(string UserId, string barcoe, string Failcode, out string er)
        {
            er = string.Empty;

            try
            {
                string CmdInfo = UserId + ";" + barcoe + ";" + Failcode + ";";

                if (!SendCmd(9, CmdInfo, out er))
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }

        /// <summary>
        /// 结束老化
        /// </summary>
        /// <param name="UserId">工号</param>
        /// <param name="ChmrId">小车码</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool EndBI(string UserId, string ChmrId, out string er)
        {
            er = string.Empty;

            try
            {
                string CmdInfo = UserId + ";" + ChmrId + ";" ;

                if (!SendCmd(10, CmdInfo, out er))
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }


        /// <summary>
        /// 结束数据上传
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ChmrId"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool EndData(string UserId, string barcode, string spec, string value, string result, out string er)
        {
            er = string.Empty;

            try
            {
                string CmdInfo = UserId + ";" + barcode + ";" + spec + ";" + value + ";" + result + ";";

                if (!SendCmd(11, CmdInfo, out er))
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }


        #endregion
    }
}
