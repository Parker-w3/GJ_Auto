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
    public partial class udcTemp : Form
    {
        #region 构造函数
        public udcTemp(CUUT  runUUT)
        {
            this.runUUT = runUUT;
            this.idNo = runUUT.Para.TimerNo;
            InitializeComponent();
            IntialControl();
            SetDoubleBuffered();
            IniLanguage();
        }
        #endregion

        #region 面板控件
        private List<Label> labNo = new List<Label>();
        private List<Label> labT = new List<Label>();

        /// <summary>
        /// 温度曲线
        /// </summary>
        private udcChartLine _udcTempChart = new udcChartLine();

        private int AgingTime = new int();

        #endregion

        #region 初始化
        /// <summary>
        /// 绑定控件
        /// </summary>
        private void IntialControl()
        {

            int col_num = 1;

            int row_num = 8;

            labNo.Clear();

            labT.Clear();

            for (int col = 0; col < col_num; col++)
            {
				for (int row = 0; row < row_num; row++)
				{
					int idNo = col * row_num + row;

					Label lab1 = new Label();
					lab1.Name = "labNo" + idNo.ToString();
					lab1.Dock = DockStyle.Fill;
					lab1.Margin = new Padding(0);
					lab1.Font = new Font("宋体", 10);
					lab1.TextAlign = ContentAlignment.MiddleCenter;
					if (idNo < 7)
						if (idNo == 6)
							lab1.Text = CLanguage.Lan("平均温度:");
						else
							lab1.Text = CLanguage.Lan("温度点") + (idNo + 1).ToString("D2") + ":";
					else if (idNo == 7)
					    lab1.Text = CLanguage.Lan("负载温度") + ":";
				

				    Label lab2 = new Label();
					lab2.Name = "labT" + idNo.ToString();
					lab2.Dock = DockStyle.Fill;
					lab2.Margin = new Padding(0);
					lab2.TextAlign = ContentAlignment.MiddleCenter;
					lab2.BorderStyle = BorderStyle.Fixed3D;
					lab2.Font = new Font("宋体", 12);
					lab2.BackColor = Color.Black;
					lab2.ForeColor = Color.Cyan;

					labNo.Add(lab1);
					labT.Add(lab2);

					panel1.Controls.Add(labNo[idNo], 0 + col * 2, row);

					panel1.Controls.Add(labT[idNo], 1 + col * 2, row);

					if (idNo >= TempMax)
						break;

				}

                _udcTempChart.Dock = DockStyle.Fill;
                pnlTemp.Panel1.Controls.Add(_udcTempChart);
            }

        }
        /// <summary>
        /// 设置双缓冲
        /// </summary>
        private void SetDoubleBuffered()
        {
            panel1.GetType().GetProperty("DoubleBuffered",
                                 System.Reflection.BindingFlags.Instance |
                                 System.Reflection.BindingFlags.NonPublic)
                                 .SetValue(panel1, true, null);

            pnlTemp.Panel1.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(pnlTemp.Panel1, true, null);
            pnlTemp.Panel2.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(pnlTemp.Panel2, true, null);

        }
        #endregion

        #region 字段
        private int idNo = 0;
        private int TempMax = 18;
        private CUUT runUUT = null;
        private int lentemp=0;
        #endregion

        #region 面板回调函数
        private void udcTemp_Load(object sender, EventArgs e)
        {
			for (int i = 0; i < 7; i++)
			{
				if (i == runUUT.PLC.TempPoint.Length)
					labT[i].Text = runUUT.PLC.curTemp.ToString("0.0");
				else
					labT[i].Text = runUUT.PLC.TempPoint[i].ToString("0.0");
				labT[i].ForeColor = Color.Cyan;

			}

            for (int i = 0; i <  runUUT.PLC.LTempPoint.Length ;i++)
            {
                labT[i + 7].Text = runUUT.PLC.LTempPoint[i].ToString("0.0");
                labT[i + 7].ForeColor = Color.Cyan;
            }

       
            lentemp = runUUT.PLC.tempTime.Count;
            iniTempWave(runUUT.Para.BurnTime , runUUT.PLC.tempTime, runUUT.PLC.tempVal);

            if (CGlobalPara.C_RUNNING && runUUT.Para.DoRun == AgingRunType.运行)
                timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 7; i++)
            {
                if (i == runUUT.PLC.TempPoint.Length )
                    labT[i].Text = runUUT.PLC.curTemp.ToString("0.0");
                else
                    labT[i].Text = runUUT.PLC.TempPoint[i].ToString("0.0");
                labT[i].ForeColor = Color.Cyan;

            }

            for (int i = 0; i < runUUT.PLC.LTempPoint.Length; i++)
            {
                labT[i + 7].Text = runUUT.PLC.LTempPoint[i].ToString("0.0");
                labT[i + 7].ForeColor = Color.Cyan;
            }

           
            if (runUUT.PLC.tempTime.Count > lentemp)
            {
                lentemp = runUUT.PLC.tempTime.Count;
                _udcTempChart.BindXY(lentemp, runUUT.PLC.tempTime, runUUT.PLC.tempVal);
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 初始化温度曲线
        /// </summary>
        private void iniTempWave(int agingTime, List<double> tempTime, List<double> tempVal)
        {
            _udcTempChart.axisX_VisNum = agingTime/60;
            _udcTempChart.axisX_InterVal = agingTime / 600;
            _udcTempChart.axisX_MinNum = 0;
            _udcTempChart.axisX_MaxNum = agingTime;
            _udcTempChart.axisY_InterVal = 10;
            _udcTempChart.axisY_MinNum = 20;
            _udcTempChart.axisY_MaxNum = 70;
            _udcTempChart.axisY_VisNum = 50;
            _udcTempChart.Initial();
            _udcTempChart.BindXY(0,tempTime, tempVal);
        }
        #endregion

        #region 语言转换
        /// <summary>
        /// 初始化温度曲线
        /// </summary>
        private void IniLanguage()
        {
            this.Text = CLanguage.Lan("温度点显示");
        }
        #endregion

        private void udcTemp_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void udcTemp_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        #region 事件定义
        public class CUIRefreshArgs : EventArgs
        {
            public readonly int idNo;
            public CUUT  runUUT;
            public CUIRefreshArgs(int idNo, ref CUUT runUUT)
            {
                this.idNo = idNo;
                this.runUUT = runUUT;
            }
        }
        public COnEvent<CUIRefreshArgs> UIRefresh = new COnEvent<CUIRefreshArgs>();
        #endregion


    }
}
