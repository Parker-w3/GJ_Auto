using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;
using System.Threading;
using GJ.COM;
using GJ.USER.APP;
using System.Drawing;
//using Spire.Xls;

namespace GJ.USER.APP
{
    /// <summary>
    /// 报表类
    /// </summary>
    public class CReport
    {
        #region 类定义
        /// <summary>
        /// 报表消息
        /// </summary>
        public class ConArgs : EventArgs
        {
            public readonly int idNo;

            public readonly string name;

            public readonly string Info;

            public readonly bool bErr;

            public ConArgs(int idNo, string name, string Info, bool bErr)
            {
                this.idNo = idNo;

                this.name = name;

                this.Info = Info;

                this.bErr = bErr;
            }
        }
        /// <summary>
        /// 数据类
        /// </summary>
        public class CPara
        {
            public CPara(string excelFolder, string sampleFile, string sheetName, int saveNo)
            {
                this.ExcelFolder = excelFolder;

                this.SampleFile = sampleFile;

                this.sheetName = sheetName;

                this.saveNo = saveNo;
            }

            /// <summary>
            /// Excel文件目录
            /// </summary>
            public string ExcelFolder = string.Empty;

            /// <summary>
            /// 样板文件
            /// </summary>
            public string SampleFile = string.Empty;

            /// <summary>
            /// 表单文件
            /// </summary>
            public string sheetName = string.Empty;

            /// <summary>
            /// 存储操作 0_初始化,1_保存数据,2_结束数据
            /// </summary>
            public int saveNo = 0;
        }

        #endregion

        #region 构造函数
        public CReport(int idNo = 0, string name = "Save to Excel")
        {
            this.idNo = idNo;

            this.name = name;
        }
        #endregion

        #region 定义事件
        /// <summary>
        /// 状态事件委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void EventOnConHander(object sender, ConArgs e);
        /// <summary>
        /// 状态消息
        /// </summary>
        public event EventOnConHander OnReport;
        /// <summary>
        /// 状态触发事件
        /// </summary>
        /// <param name="e"></param>
        private void OnTriger(ConArgs e)
        {
            if (OnReport != null)
            {
                OnReport(this, e);
            }
        }
        #endregion

        #region 字段
        /// <summary>
        /// 线程编号
        /// </summary>
        private int idNo = 0;
        /// <summary>
        /// 线程名称
        /// </summary>
        private string name = string.Empty;
        /// <summary>
        /// Excel
        /// </summary>
        private Application xlApp = null;
        /// <summary>
        /// 报表线程
        /// </summary>
        private Thread _Thread = null;
        /// <summary>
        /// 取消任务
        /// </summary>
        private CancellationTokenSource _cts = null;
        /// <summary>
        /// 报表队列
        /// </summary>
        private Queue<CPara> excelQue = new Queue<CPara>();
        /// <summary>
        /// 线程状态
        /// </summary>
        public volatile bool TheadAlive = false;
        #endregion

        #region 共享方法

        /// <summary>
        /// 初始化Excel,创建Excel报表
        /// </summary>
        /// <param name="excelFolder">Excel文件路径</param>
        /// <param name="sampleExcelFile">样本文件路径</param>
        /// <param name="sheetName">表单文件</param>
        /// <param name="saveNo">存储操作</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool Ini_To_Excel(string excelFile, string[] wData, out string er)
        {
            er = string.Empty;

            try
            {


                Application xlApp = new Application();

                Workbook xlWorkBook = xlApp.Workbooks.Open(excelFile);              //打开Excel
                for (int i = 0; i < wData.Length; i++)
                {
                    string[] Str1 = wData[i].Split(',');
                    Worksheet xlWorkSheet = xlWorkBook.Sheets[Str1[0]];               //运行中存储数据
                    string RangeX = string.Empty;
                    if (Str1[0] == "Gerneral Info")
                    {
                        /*---------老化基本信息-------------*/
                        xlWorkSheet.Cells[2, 2] = Str1[1];          //机种名
                        xlWorkSheet.Cells[2, 4] = Str1[2];          //工单名称
                        xlWorkSheet.Cells[2, 6] = Str1[3];          //老化架编号

                        xlWorkSheet.Cells[3, 2] = 144;              //可老化位置
                        xlWorkSheet.Cells[3, 4] = Str1[4];          //实际老化总数

                        xlWorkSheet.Cells[4, 2] = Str1[5];          //开始时间
                        xlWorkSheet.Cells[4, 6] = Str1[6];          //老化时间(H)
                        xlWorkSheet.Cells[5, 2] = Str1[7];          //操作员代码	
                    }

                    else
                    {
                        /*---------条码信息-------------*/
                        for (int j = 0; j < (Str1.Length - 2) / 2; j++)
                        {

                            xlWorkSheet.Cells[3, 5 + j * 2] = Str1[j * 2 + 1];
                            xlWorkSheet.Cells[4, 5 + j * 2] = Str1[j * 2 + 2];
                        }
                    }
                    xlWorkSheet = null;
                }

                xlWorkBook.Save();

                xlWorkBook.Close();

                xlWorkBook = null;

                xlApp.DisplayAlerts = false;

                xlApp.Quit();

                xlApp = null;

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                Process[] proKill = Process.GetProcessesByName("Excel");
                for (int i = 0; i < proKill.Length; i++)
                    proKill[i].Kill();
                return false;
            }
            finally
            {

            }
        }

        /// <summary>
        /// 保存EXCEL数据
        /// </summary>
        /// <param name="excelFile"></param>
        /// <param name="wData"></param>
        /// <param name="sheetName"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool Save_To_Excel(string excelFile, string[] wData, out string er)
        {
            er = string.Empty;

            try
            {
                Application xlApp = new Application();

                Workbook xlWorkBook = xlApp.Workbooks.Open(excelFile);              //打开Excel
                for (int i = 1; i < wData.Length; i++)
                {
                    string[] Str1 = wData[i].Split(',');
                    Worksheet xlWorkSheet = xlWorkBook.Sheets[Str1[0]];               //运行中存储数据
                    string RangeX = string.Empty;
                    /*---------老化信息-------------*/
                    for (int j = 1; j < 5; j++)
                    {
                        xlWorkSheet.Cells[5 + Convert.ToInt16(Str1[1]), j] = Str1[j];
                    }
                    /*---------数据信息-------------*/
                    for (int j = 0; j < (Str1.Length - 6) / 3; j++)
                    {

                        xlWorkSheet.Cells[5 + Convert.ToInt16(Str1[1]), 5 + j] = Str1[5 + j * 3];
                        //if (Str1[5 + j * 3] != "--" && Str1[5 + j * 3] != "")
                        //{
                        //    if (Convert.ToDouble(Str1[5 + j * 3]) > Convert.ToDouble(Str1[6 + j * 3]) &&
                        //        Convert.ToDouble(Str1[5 + j * 3]) < Convert.ToDouble(Str1[7 + j * 3]))
                        //        xlWorkSheet.Range [getRangeX(5 + Convert.ToInt16(Str1[1]))+(5 + j).ToString ()+":"+
                        //                           getRangeX(5 + Convert.ToInt16(Str1[1]))+(5 + j).ToString ()].Style.Color=Color.Red;  
                        //}
                    }
                    //xlWorkSheet.get_Range(xlWorkSheet.Cells[5 + Convert.ToInt16(Str1[0]), 5 + i], xlWorkSheet.Cells[5 + Convert.ToInt16(Str1[0]), 5 + i]).Interior.Color =System.Drawing.ColorTranslator.ToOle(Color.Red);
                    xlWorkSheet = null;
                }

                xlWorkBook.Save();

                xlWorkBook.Close();

                xlWorkBook = null;

                xlApp.DisplayAlerts = false;

                xlApp.Quit();

                xlApp = null;

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                Process[] proKill = Process.GetProcessesByName("Excel");
                for (int i = 0; i < proKill.Length; i++)
                    proKill[i].Kill();
                return false;
            }
            finally
            {
                //Process[] proKill = Process.GetProcessesByName("Excel");
                //for (int i = 0; i < proKill.Length; i++)
                //    proKill[i].Kill();
            }
        }


        /// <summary>
        /// 保存结束数据
        /// </summary>
        /// <param name="excelFile"></param>
        /// <param name="wData"></param>
        /// <param name="sheetName"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool End_To_Excel(string excelFile, string[] wData, out string er)
        {
            er = string.Empty;

            try
            {
                Application xlApp = new Application();

                Workbook xlWorkBook = xlApp.Workbooks.Open(excelFile);              //打开Excel
                for (int i = 0; i < wData.Length; i++)
                {
                    string[] Str1 = wData[i].Split(',');
                    Worksheet xlWorkSheet = xlWorkBook.Sheets[Str1[0]];               //运行中存储数据
                    string RangeX = string.Empty;
                    if (Str1[0] == "Gerneral Info")
                    {
                        /*---------老化基本信息-------------*/
                        xlWorkSheet.Cells[4, 4] = Str1[1];          //开始时间
                        xlWorkSheet.Cells[9, 2] = Str1[2];          //老化时间(H)
                        xlWorkSheet.Cells[9, 4] = Str1[3];          //操作员代码

                        for (int j = 0; j < (Str1.Length - 5) / 4; j++)
                        {
                            xlWorkSheet.Cells[11 + j, 1] = Str1[j * 4 + 4];
                            xlWorkSheet.Cells[11 + j, 2] = Str1[j * 4 + 5];
                            xlWorkSheet.Cells[11 + j, 3] = Str1[j * 4 + 6];
                            xlWorkSheet.Cells[11 + j, 4] = Str1[j * 4 + 7];
                        }
                    }

                    xlWorkSheet = null;
                }

                xlWorkBook.Save();

                xlWorkBook.Close();

                xlWorkBook = null;

                xlApp.DisplayAlerts = false;

                xlApp.Quit();

                xlApp = null;

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                Process[] proKill = Process.GetProcessesByName("Excel");
                for (int i = 0; i < proKill.Length; i++)
                    proKill[i].Kill();
                return false;
            }
            finally
            {

            }
        }
        #region 获取字母坐标列表
        /// <summary>
        /// 获取X字母坐标
        /// </summary>
        /// <param name="xNo"></param>
        /// <returns></returns>
        private static string getRangeX(int xNo)
        {
            List<int> xNum = new List<int>();

            if (xNo < 26)           //输出坐标
                return getX(xNo);
            else
            {
                while (xNo < 26)
                {
                    xNum.Add(xNo % 26);
                    xNo = xNo / 26;
                }
            }
            string RangeX = string.Empty;
            for (int i = 0; i < xNum.Count; i++)
            {
                RangeX += getX(xNum[i]);
            }
            return RangeX;
        }

        /// <summary>
        /// 获取字母坐标列表
        /// </summary>
        /// <param name="xNo"></param>
        /// <returns></returns>
        private static string getX(int xNo)
        {
            switch (xNo)
            {
                case 0: return "A";
                case 1: return "B";
                case 2: return "C";
                case 3: return "D";
                case 4: return "E";
                case 5: return "F";
                case 6: return "G";
                case 7: return "H";
                case 8: return "I";
                case 9: return "J";
                case 10: return "K";
                case 11: return "L";
                case 12: return "M";
                case 13: return "N";
                case 14: return "O";
                case 15: return "P";
                case 16: return "Q";
                case 17: return "R";
                case 18: return "S";
                case 19: return "T";
                case 20: return "U";
                case 21: return "V";
                case 22: return "W";
                case 23: return "X";
                case 24: return "Y";
                case 25: return "Z";
                default: return "A";
            }
        }
        #endregion

        #endregion
    }
}
