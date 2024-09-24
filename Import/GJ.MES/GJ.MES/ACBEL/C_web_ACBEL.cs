using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace GJ.MES.ACBEL
{
   public class C_web_ACBEL
    {
        [DllImport("H_ACBEL_TEST_DATA.DLL")]
        public static extern int ACBEL_DATABASE_OPEN(string DEDFAC_CODE);
        [DllImport("H_ACBEL_TEST_DATA.DLL")]
        public static extern void ACBEL_DATABASE_CLOSE();
        [DllImport("H_ACBEL_TEST_DATA.DLL")]
        public static extern int H_ACBEL_TEST_DATA(string DEDDATABASE,
                                                      string DEDSTATION,
                                                      string dedINTERNAL_SR,
                                                      string DEDTOOL_NO,
                                                      string DEDPROGRAM);
        [DllImport("H_ACBEL_TEST_DATA.DLL")]
        public static extern int INSERT_DATA(string DEDDATABASE,
                                                string DEDDATE1,
                                                string DEDDATE2);
        [DllImport("H_ACBEL_TEST_DATA.DLL")]
        public static extern int TRANSMIT_DATA(string DEDDATABASE,
                                                  string DEDTOOL_NO);

       /// <summary>
       /// 打开MES
       /// </summary>
       /// <param name="DEDFAC_CODE"></param>
       /// <returns></returns>
       public int OpenMes(string DEDFAC_CODE)
       {
           int iResult=0;
           iResult = ACBEL_DATABASE_OPEN(DEDFAC_CODE);

           return iResult;
       }
       /// <summary>
       /// 关闭MES
       /// </summary>
       public void CloseMes()
       {
           ACBEL_DATABASE_CLOSE();
       }
       /// <summary>
       /// 验证条码
       /// </summary>
       /// <param name="DEDDATABASE"></param>
       /// <param name="DEDSTATION"></param>
       /// <param name="dedINTERNAL_SR"></param>
       /// <param name="DEDTOOL_NO"></param>
       /// <param name="DEDPROGRAM"></param>
       /// <returns></returns>
       public int UUTID(string DEDDATABASE, string DEDSTATION, string dedINTERNAL_SR, string DEDTOOL_NO, string DEDPROGRAM)
       {
           int iResult = 0;
           iResult = H_ACBEL_TEST_DATA(DEDDATABASE, DEDSTATION, dedINTERNAL_SR, DEDTOOL_NO, DEDPROGRAM);
           return iResult;
       }
       /// <summary>
       /// 条码上传
       /// </summary>
       /// <param name="DEDDATABASE"></param>
       /// <param name="DEDDATE1"></param>
       /// <param name="DEDDATE2"></param>
       /// <returns></returns>
       public int TranUUT(string DEDDATABASE, string DEDDATE1, string DEDDATE2)
       {
           int iResult = 0;
           iResult = INSERT_DATA(DEDDATABASE, DEDDATE1, DEDDATE2);
           return iResult;
       }

    }
}


