using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.USER.APP;
using GJ.COM;
using GJ.UI;

namespace GJ.Aging.BURNIN.Udc
{
    public partial class udcUUTInfo : Form
    {
        #region 构造函数

        public udcUUTInfo(CUUT runUUT)
        {
            this.runUUT = runUUT;

            this.idNo = runUUT.Para .TimerNo ;            

            InitializeComponent();

            SetUILanguage();

            IntialControl();

            SetDoubleBuffered();

            
        }
        #endregion

        #region 字段
        private int idNo = 0;
        private CUUT runUUT = null;
        private int runNowTime = 0;
        #endregion

        #region 初始化
        /// <summary>
        /// 绑定控件
        /// </summary>
        private void IntialControl()
        {
            _udcChart.Dock = DockStyle.Fill;
            panel4.Controls.Add(_udcChart, 0, 0);   
        }
        /// <summary>
        /// 设置双缓冲,防止界面闪烁
        /// </summary>
        private void SetDoubleBuffered()
        {
            panel1.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel1, true, null);
            panel3.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel3, true, null);
            panel4.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel4, true, null);

        }
        #endregion

        #region 面板控件
        private udcChartOnOff _udcChart = new udcChartOnOff(); 
        #endregion

        #region 面板回调函数
        private void udcUUTInfo_Load(object sender, EventArgs e)
        {

            lablocalName.Text = runUUT.Para.TimerName  ;
            labModel.Text = runUUT.Para.ModelName  ;
            labStartTime.Text = runUUT.Para.StartTime;

            DateTime endTime;
            if (runUUT.Para.StartTime == "")
                labEndTime.Text = "";
            else
            {
                endTime = (System.Convert.ToDateTime( runUUT.Para.StartTime).AddSeconds(runUUT.Para.BurnTime ) );
                labEndTime.Text = endTime.ToString("yyyy/MM/dd HH:mm:ss");
            }

            runNowTime =(int) runUUT.Para .RunTime ;
            TimeSpan ts = new TimeSpan(0, 0, runNowTime);
            TimeSpan tl = new TimeSpan(0, 0, runUUT.Para.BurnTime - runNowTime);
            string runTime = ts.Days.ToString("D2") + ":" + ts.Hours.ToString("D2") + ":" + ts.Minutes.ToString("D2") + ":" + ts.Seconds.ToString("D2");
            string leftTime = tl.Days.ToString("D2") + ":" + tl.Hours.ToString("D2") + ":" + tl.Minutes.ToString("D2") + ":" + tl.Seconds.ToString("D2");
            labRunTime.Text = runTime;
            labLeftTime.Text = leftTime;

            refreshOnOff();

            if (CGlobalPara.C_RUNNING && runUUT.Para.DoRun == AgingRunType.运行)
                timer1.Enabled = true;
        }
        private void udcUUTInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
        private void udcUUTInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
 
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            refreshRunTimeUI();
            UIRefresh.OnEvented(new CUIRefreshArgs(idNo, ref runUUT));
        }
        private void refreshRunTimeUI()
        {
            runNowTime=(int)runUUT.Para.RunTime ;
            TimeSpan ts = new TimeSpan(0, 0, runNowTime);
            TimeSpan tl = new TimeSpan(0, 0, runUUT.Para.BurnTime - runNowTime);
            string runTime = ts.Days.ToString("D2") + ":" + ts.Hours.ToString("D2") + ":" + ts.Minutes.ToString("D2") + ":" + ts.Seconds.ToString("D2");
            string leftTime = tl.Days.ToString("D2") + ":" + tl.Hours.ToString("D2") + ":" + tl.Minutes.ToString("D2") + ":" + tl.Seconds.ToString("D2");
            labRunTime.Text = runTime;
            labLeftTime.Text = leftTime;
        }

        private void refreshOnOff()
        {
            _udcChart.maxVolt = 600;

            _udcChart.biTime = ((double)runUUT.Para.BurnTime) / 3600;

            List<udcChartOnOff.COnOff> itemList = new List<udcChartOnOff.COnOff>();

            for (int i = 0; i < runUUT.Para.OnOffNum; i++)
            {

                udcChartOnOff.COnOff onoff = new udcChartOnOff.COnOff();

                onoff.curVolt = runUUT.OnOff.OnOff[i].inPutVolt;
  
                onoff.onoffTimes = runUUT.OnOff.OnOff[i].OnOffTime;

                onoff.onTimes = runUUT.OnOff.OnOff[i].OnTime;

                onoff.offTimes = runUUT.OnOff.OnOff[i].OffTime;

                itemList.Add(onoff);

            }

            _udcChart.onoff = itemList;

            _udcChart.Refresh();

            if (CGlobalPara.C_RUNNING && runUUT.Para.DoRun == AgingRunType.运行 )
                _udcChart.startRun(runUUT.Para.RunTime);
        }
        #endregion

        #region 事件定义
        public class CUIRefreshArgs : EventArgs
        {
            public readonly int idNo;
            public CUUT runUUT;
            public CUIRefreshArgs(int idNo, ref CUUT runUUT)
            {
                this.idNo = idNo;
                this.runUUT = runUUT;
            }
        }
        public COnEvent<CUIRefreshArgs> UIRefresh = new COnEvent<CUIRefreshArgs>();
        #endregion

        #region 语言设置
        /// <summary>
        /// 设置中英文界面
        /// </summary>
        private void SetUILanguage()
        {
            switch (GJ.COM.CLanguage.languageType)
            {
                case CLanguage.EL.中文:
                    this.Text = CLanguage.Lan("老化运行信息");
                    label1.Text = CLanguage.Lan("运行状态信息");
                    label15.Text = CLanguage.Lan("运行时间:");
                    label16.Text = CLanguage.Lan("剩余时间:");
                    label2.Text = CLanguage.Lan("老化位置:");
                    label3.Text = CLanguage.Lan("机种名称:");
                    label4.Text = CLanguage.Lan("开始时间:");
                    label8.Text = CLanguage.Lan("结束时间:");
                    break;
                case CLanguage.EL.英语:
                    break;
                case CLanguage.EL.繁体:
                    this.Text = CLanguage.Lan("老化运行信息");
                    label1.Text = CLanguage.Lan("运行状态信息");
                    label15.Text = CLanguage.Lan("运行时间:");
                    label16.Text = CLanguage.Lan("剩余时间:");
                    label2.Text = CLanguage.Lan("老化位置:");
                    label3.Text = CLanguage.Lan("机种名称:");
                    label4.Text = CLanguage.Lan("开始时间:");
                    label8.Text = CLanguage.Lan("结束时间:");
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
