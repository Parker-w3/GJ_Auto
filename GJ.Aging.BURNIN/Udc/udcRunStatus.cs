using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.COM;

namespace GJ.Aging.BURNIN.Udc
{
    public partial class udcRunStatus : UserControl
    {

        #region 枚举
        /// <summary>
        /// 按钮状态
        /// </summary>
        public enum ESetMenu
        {
            自检老化,
            启动老化,
            暂停老化,
            继续老化,
            停止老化,
            选择机种,
            扫描条码,
            温度显示,
            时序显示
        }
        #endregion

        #region 初始化
        public udcRunStatus(CUUT runUUT)
        {
            InitializeComponent();

            SetDoubleBuffered();

            SetUILanguage();

            this._runUUT = runUUT.Clone();

            this._idNo = this._runUUT.Para.TimerNo;

            this._name = this._runUUT.Para.TimerName;
        }

        ///// <summary>
        ///// 扫描条码
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnScanBar_Click(object sender, EventArgs e)
        //{
        //    menuClick.OnEvented(new CRunStatusArgs(_idNo, ESetMenu.扫描条码));
        //}



        /// <summary>
        /// 时序显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRunWave_Click(object sender, EventArgs e)
        {
            menuClick.OnEvented(new CRunStatusArgs(_idNo, ESetMenu.时序显示));
        }

        ///// <summary>
        ///// 温度显示
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnTempWave_Click(object sender, EventArgs e)
        //{
        //    menuClick.OnEvented(new CRunStatusArgs(_idNo, ESetMenu.温度显示));
        //}

        /// <summary>
        /// 启动暂停老化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCtrlRun_Click(object sender, EventArgs e)
        {
            string btntxt = btnCtrlRun.Text.Trim();

            if (btntxt == CLanguage.Lan("自检电压"))
                menuClick.OnEvented(new CRunStatusArgs(_idNo, ESetMenu.自检老化));

            if (btntxt == CLanguage.Lan("启动老化"))
                menuClick.OnEvented(new CRunStatusArgs(_idNo, ESetMenu.启动老化));

            if (btntxt == CLanguage.Lan("暂停老化"))
                menuClick.OnEvented(new CRunStatusArgs(_idNo, ESetMenu.暂停老化));

            if (btntxt == CLanguage.Lan("继续老化"))
                menuClick.OnEvented(new CRunStatusArgs(_idNo, ESetMenu.继续老化));

        }
        /// <summary>
        /// 选择机种
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblModelName_DoubleClick(object sender, EventArgs e)
        {
            if (_runUUT.Para.DoRun == AgingRunType.空闲 || _runUUT.Para.DoRun == AgingRunType.扫条码) 
            menuClick.OnEvented(new CRunStatusArgs(_idNo, ESetMenu.选择机种));
        }

        /// <summary>
        /// 选择机种
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModel_Click(object sender, EventArgs e)
        {
            if (_runUUT.Para.DoRun == AgingRunType.空闲 || _runUUT.Para.DoRun == AgingRunType.扫条码)
                menuClick.OnEvented(new CRunStatusArgs(_idNo, ESetMenu.选择机种));
        }
        /// <summary>
        /// 扫条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScanSN_Click(object sender, EventArgs e)
        {
            if (_runUUT.Para.DoRun != AgingRunType.运行)
                menuClick.OnEvented(new CRunStatusArgs(_idNo, ESetMenu.扫描条码));
        }
        #endregion

        #region 初始化

        /// <summary>
        /// 设置双缓冲
        /// </summary>
        private void SetDoubleBuffered()
        {
            Pnl1.GetType().GetProperty("DoubleBuffered",
                                      System.Reflection.BindingFlags.Instance |
                                      System.Reflection.BindingFlags.NonPublic)
                                      .SetValue(Pnl1, true, null);
        }

        #endregion

        #region 字段
        /// <summary>
        /// 治具编号
        /// </summary>
        private int _idNo = 0;
        /// <summary>
        /// 房间名称
        /// </summary>
        private string _name = string.Empty;
        /// <summary>
        /// 房间信息
        /// </summary>
        private CUUT _runUUT = null;

        #endregion

        #region 属性
        /// <summary>
        /// 编号
        /// </summary>
        public int idNo
        {
            set { _idNo = idNo; }
            get { return _idNo; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string name
        {
            set { _name = name; }
            get { return _name; }
        }

        #endregion

        #region 面板消息
        public class CRunStatusArgs : EventArgs
        {
            public readonly int idNo;
            public readonly ESetMenu menuInfo;
            public CRunStatusArgs(int idNo, ESetMenu menuInfo)
            {
                this.idNo = idNo;
                this.menuInfo = menuInfo;
            }
        }
        public COnEvent<CRunStatusArgs> menuClick = new COnEvent<CRunStatusArgs>();
        #endregion

        #region 方法
        /// <summary>
        /// 设置测试状态
        /// </summary>
        public void SetRunStatus(CUUT runUUT)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action<CUUT>(SetRunStatus), runUUT);
            else
            {
                this._runUUT = runUUT.Clone();
                setRunType();
            }
        }

        /// <summary>
        /// 设置老化房产品运行状态
        /// </summary>
        /// <param name="runUUT"></param>
        private void setRunType()
        {
            string testInfo = string.Empty;

            lblArea.Text = _runUUT.Para.TimerName;
            TimeSpan ts = new TimeSpan(0, 0, _runUUT.Para.RunTime);
            string runTime = ts.Days.ToString("D2") + ":" + ts.Hours.ToString("D2") + ":" +
                             ts.Minutes.ToString("D2") + ":" + ts.Seconds.ToString("D2");

            lblRunTime.Text = runTime;
            lblModelName.Text = _runUUT.Para.ModelName;


          //  lblMO_NO.Text = _runUUT.Para.MO_NO ;xA
            labOutModel.Text = "电压:" + _runUUT.Led[0].vMin  + "V-" + _runUUT.Led[0].vMax  + "V 电流:" + _runUUT.Led[0].Imin + "A-" + _runUUT.Led[0].Imax + "A"; 
              
            ts = new TimeSpan(0, 0, (_runUUT.Para.BurnTime - _runUUT.Para.RunTime));
            string haveTime = ts.Days.ToString("D2") + ":" + ts.Hours.ToString("D2") + ":" +
                             ts.Minutes.ToString("D2") + ":" + ts.Seconds.ToString("D2");

            lblHaveTime.Text = haveTime;

            lblReadTemp.Text = _runUUT.PLC.curTemp.ToString() + "℃";

        

            double iTimer = ((double)(_runUUT.Para.BurnTime) / 60);
            lblAgingTime.Text = iTimer.ToString("F1") + "Min";


            if (_runUUT.Para.DCONOFF == 0)
            {
                lblInputVolt.Text = _runUUT.Para.RunInVolt.ToString("F1") + "V";
            }
            else
            {
                lblInputVolt.Text ="220" + "V";
            }

            lblTTNum.Text = _runUUT.Para.TTNum.ToString();
            lblPassNum.Text = _runUUT.Para.PassNum.ToString();

            //if (_runUUT.Mes.DoMes == AgingMesType.上传条码)
            //    menuClick.OnEvented(new CRunStatusArgs(_idNo, ESetMenu.扫描条码));

         
            switch (_runUUT.Para.DoRun)
            {
                case AgingRunType.空闲:
                    btnCtrlRun.Text = CLanguage.Lan("    自检电压");
                    btnCtrlRun.ImageKey = "Off";
                    //btnScanBar.Enabled = true;
                    break;
                case AgingRunType.自检:
                    btnCtrlRun.Text =CLanguage.Lan ("    启动老化");
                    btnCtrlRun.ImageKey = "On";
                    //btnScanBar.Enabled = true;
                    break;
                case AgingRunType.运行:
                    btnCtrlRun.Text =CLanguage.Lan ("    暂停老化");
                    btnCtrlRun.ImageKey = "On";
                    //btnScanBar.Enabled = false;
                    break;
                case AgingRunType.暂停:
                    btnCtrlRun.Text =CLanguage.Lan ("    继续老化");
                    btnCtrlRun.ImageKey = "Off";
                    //btnScanBar.Enabled = false;
                    break;

                case AgingRunType.扫条码:
                    //btnCtrlRun.Text = CLanguage.Lan("    启动老化");
                    //btnCtrlRun.ImageKey = "On";
                    break;

                default:
                    btnCtrlRun.Text = CLanguage.Lan("    自检电压");
                    btnCtrlRun.ImageKey = "Off";
                    break;
            }





        }
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

                    break;
                case CLanguage.EL.英语:
                    break;
                case CLanguage.EL.繁体:

                    break;
                default:
                    break;
            }
        }

        #endregion

        private void lblReadTemp_DoubleClick(object sender, EventArgs e)
        {
            menuClick.OnEvented(new CRunStatusArgs(_idNo, ESetMenu.温度显示));
        }
    }
}
