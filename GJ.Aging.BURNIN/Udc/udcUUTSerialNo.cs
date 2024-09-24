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
    public partial class udcUUTSerialNo : UserControl
    {
        #region 枚举
        /// <summary>
        /// UI状态
        /// </summary>
        public enum EUI
        {
            空闲,
            扫描OK,
        }
        /// <summary>
        /// 菜单状态
        /// </summary>
        public enum ESetMenu
        {
            清除条码,

        }
        #endregion

        #region 构造函数

        public udcUUTSerialNo(CUUT runUUT, int FixNo, int idNo)
        {
            InitializeComponent();

            IntialControl();

            load_Max_UUT(_uutMax, idNo);

            this._runUUT = runUUT.Clone();

            this._FixNo = FixNo;

            this._idNo = idNo;

            this._iTimer = this._runUUT.Para.TimerNo;

            this._name = "L" + this._runUUT.Led[idNo].iLayer.ToString() + "_" + this._runUUT.Led[idNo].iLayer.ToString("D2");
        }

        public override string ToString()
        {
            return _name;
        }

        #endregion

        #region 初始化
        /// <summary>
        /// 绑定控件
        /// </summary>
        private void IntialControl()
        {
            lblStatus = new Label();
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Font = new Font("微软雅黑", 9.5f);
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;

            //是否显示提示信息
            tlTip.Active = true;
            //是否显示提示信息，当窗体没有获得焦点时
            tlTip.ShowAlways = true;
            //工具提示”窗口显示之前，鼠标指针必须在控件内保持静止的时间（以毫秒计）
            tlTip.InitialDelay = 200;
            // 提示信息刷新时间 
            tlTip.ReshowDelay = 300;
            //提示信息延迟时间
            tlTip.AutomaticDelay = 200;
            // 提示信息弹出时间
            tlTip.AutoPopDelay = 10000;
            // 提示信息
            tlTip.ToolTipTitle = CLanguage.Lan("产品信息");
        }

        /// <summary>
        /// 加载单个模块的通道
        /// </summary>
        private void load_Max_UUT(int uutMax, int idNO)
        {
            try
            {
                //治具界面
                if (pnlUUT != null)
                {
                    foreach (Control item in pnlUUT.Controls)
                    {
                        pnlUUT.Controls.Remove(item);
                        item.Dispose();
                    }
                    lblUUT.Clear();
                    pnlUUT.Dispose();
                    pnlUUT = null;
                }

                for (int i = 0; i < uutMax; i++)
                {
                    Label lab = new Label();
                    lab.Name = "labUUT" + (idNO + i).ToString();
                    lab.Dock = DockStyle.Fill;
                    lab.Margin = new Padding(0);
                    lab.TextAlign = ContentAlignment.MiddleCenter;
                    lab.BackColor = Color.White;
                    lab.Text = "";
                    lblUUT.Add(lab);
                }
                //初始化panelUUT

                pnlUUT = new TableLayoutPanel();
                pnlUUT.Dock = DockStyle.Fill;
                pnlUUT.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;
                pnlUUT.Margin = new Padding(0);

                pnlUUT.ColumnCount = uutMax;
                for (int i = 0; i < uutMax; i++)
                pnlUUT.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

                pnlUUT.RowCount = 1;
                for (int i = 0; i < 1; i++)
                    pnlUUT.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

                for (int i = 0; i < uutMax; i++)
                {
                    pnlUUT.Controls.Add(lblUUT[i], i, 0);
                }
                pnlUUT.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(pnlUUT, true, null);
                this.Controls.Add(pnlUUT);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 面板控件
        private TableLayoutPanel pnlUUT = null;
        private Label lblStatus = null;
        private List<Label> lblUUT = new List<Label>();
        #endregion

        #region 字段
        /// <summary>
        /// 时序编号
        /// </summary>
        private int _iTimer = 0;

        /// <summary>
        /// 治具编号
        /// </summary>
        private int _FixNo = 0;

        /// <summary>
        /// 治具第一个灯编号
        /// </summary>
        private int _idNo = 0;
        /// <summary>
        /// 槽位名称
        /// </summary>
        private string _name = string.Empty;
        /// <summary>
        /// 产品数量
        /// </summary>
        private int _uutMax = 1;
        /// <summary>
        /// 治具信息
        /// </summary>
        private CUUT _runUUT = null;
        /// <summary>
        /// 当前状态
        /// </summary>
        private EUI IsUI = EUI.空闲;
        /// <summary>
        /// 治具信息
        /// </summary>
        private string uutBaseInfo = string.Empty;
        /// <summary>
        /// 设备信息
        /// </summary>
        private string uutDevInfo = string.Empty;
        /// <summary>
        /// 显示备份
        /// </summary>
        private string strBackup = string.Empty;
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
        /// 编号
        /// </summary>
        public int FixNo
        {
            set { _FixNo = FixNo; }
            get { return _FixNo; }
        }


        /// <summary>
        /// 编号
        /// </summary>
        public int iTimer
        {
            set { _iTimer = iTimer; }
            get { return _iTimer; }
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

        #region 面板回调函数
        private void udcFixture_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 设定单个误测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlClearFail_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show(CLanguage.Lan("确定要将该位置设定为误测?"), CLanguage.Lan("状态设置"), MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                menuClick.OnEvented(new CSetMenuArgs(_iTimer, _idNo, ESetMenu.清除条码));
            }

        }

        #endregion

        #region 面板消息
        public class CSetMenuArgs : EventArgs
        {
            public readonly int idNo;
            public readonly int iTimer;
            public readonly ESetMenu menuInfo;
            public CSetMenuArgs(int iTimer, int idNo, ESetMenu menuInfo)
            {
                this.iTimer = iTimer;
                this.idNo = idNo;
                this.menuInfo = menuInfo;
            }
        }
        public COnEvent<CSetMenuArgs> menuClick = new COnEvent<CSetMenuArgs>();
        #endregion

        #region 面板控件
        /// <summary>
        /// 说明
        /// </summary>
        private List<Label> lblData = new List<Label>();
         
        /// <summary>
        /// 数据
        /// </summary>
        private List<Label> lblValue = new List<Label>();
        #endregion

        #region 方法

        /// <summary>
        /// 设置测试状态
        /// </summary>
        public void SetUUT(CUUT runUUT, int idNO)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action<CUUT, int>(SetUUT), runUUT, idNO);
            else
            {
                if (pnlUUT == null)
                    return;

                this._runUUT = runUUT.Clone();

                EUI NowIsUI = EUI.空闲;


                if (_runUUT.Para.DoRun != AgingRunType.空闲)
                    NowIsUI = EUI.扫描OK;
                else
                    NowIsUI = EUI.空闲;

                if (IsUI != NowIsUI)
                {
                    foreach (Control item in this.Controls)
                        this.Controls.Remove(item);
                    if (NowIsUI == EUI.扫描OK)
                        this.Controls.Add(pnlUUT);
                    else
                        this.Controls.Add(lblStatus);
                    IsUI = NowIsUI;
                }

                setBI(idNO);
            }
        }

        /// <summary>
        /// 设置老化房产品运行状态
        /// </summary>
        /// <param name="runUUT"></param>
        private void setBI(int idNO)
        {
            List<string> info = new List<string>();
            string allinfo = string.Empty;
            TimeSpan ts = new TimeSpan(0, 0, _runUUT.Para.RunTime);
            string runTime = ts.Days.ToString("D2") + ":" + ts.Hours.ToString("D2") + ":" +
                             ts.Minutes.ToString("D2") + ":" + ts.Seconds.ToString("D2");

            for (int i = 0; i < _uutMax; i++)
            {
                string testInfo = string.Empty;
                bool haveUUT = false;
                int result = 0;
                int isolt = idNO * _uutMax + i;

                if (_runUUT.Led[isolt].result != 0)
                {
                    haveUUT = true;
                    //if (_runUUT.Led[isolt].result != 1 && _runUUT.Led[isolt].result != 4)
                    //{
                    //    if (_runUUT.Led[isolt].result == 3)
                    //    {
                    //        result = 3;
                    //    }
                    //    else
                    //    {
                    //        result = 2;
                    //    }
                    //}
                    //else
                    //    result = 1;
                }

                if (haveUUT == true)
                {
                    testInfo += "机种名称:" + _runUUT.Led[isolt].modelName + " ;\r\n";
                    testInfo += "【" + CLanguage.Lan("产品") + (_runUUT.Led[isolt].iUUT).ToString("D2") +
                                 CLanguage.Lan("通道") + (_runUUT.Led[isolt].iCH).ToString("D2") + "】\r\n";
                    if (_runUUT.Led[isolt].serialNo != "")
                        testInfo += CLanguage.Lan("条码") + " : " + _runUUT.Led[isolt].serialNo + ";\r\n";

                    testInfo += CLanguage.Lan("扫描时间") + " : " + runTime + ";\r\n";

                    testInfo += _runUUT.Led[isolt].vName + ":";

                    testInfo += CLanguage.Lan("电压") + " : " +
                                _runUUT.Led[isolt].unitV.ToString("F2") + "V ;  ";
                    testInfo += CLanguage.Lan("电流") + " : " +
                                _runUUT.Led[isolt].unitA.ToString("F2") + "A ;  ";
                
                    switch (_runUUT.Led[isolt].result)
                    {
                        case 1:
                            testInfo += "Result=PASS ;\r\n";
                            break;
                        case 2:
                            testInfo += "Result=FAIL ;\r\n";

                            testInfo += CLanguage.Lan("不良时间") + " : " +
                                        _runUUT.Led[isolt].failTime + ";" +
                                        CLanguage.Lan("不良信息") + " : " +
                                        _runUUT.Led[isolt].failInfo + " ;\r\n";
                            break;
                        case 3:
                            testInfo += "Result=ShotDown;\r\n";

                            testInfo += CLanguage.Lan("不良时间") + " : " +
                                        _runUUT.Led[isolt].failTime + " ;\r\n" +
                                        CLanguage.Lan("不良信息") + " : " +
                                        _runUUT.Led[isolt].failInfo + " ;\r\n";
                            break;
                        case 4:
                            testInfo += "Result=PASS;\r\n";
                            break;
                        case 5:
                            testInfo += "Result=FAIL;\r\n";
                            break;
                        default:
                            break;
                    }

                    testInfo += CLanguage.Lan("功率") + " : " +
                         ( _runUUT.Led[isolt].unitA * _runUUT.Led[isolt].unitV) .ToString("F2") + "W "+ ";\r\n";
                    string eloadmsg = string.Empty;
                    if (_runUUT.Led[isolt].elCom == 1)
                    {
                        eloadmsg = "负载串口:" + CGlobalPara.SysPara.Dev.GJ_1050_4Com[0];
                    }
                    else
                    {
                        eloadmsg = "负载串口:" + CGlobalPara.SysPara.Dev.GJ_1050_4Com[0];
                    }
                    testInfo +=  eloadmsg  + "  负载地址:" + _runUUT.Led[isolt].elAdrs.ToString("D2") + "_" +
                                _runUUT.Led[isolt].elCH.ToString("D2") + " ;\r\n";
                  
                    testInfo += "电压上下限:" + _runUUT.Led[isolt].vMin.ToString() + "V--" + _runUUT.Led[isolt].vMax .ToString() +
                                "V  电流上下限:" + _runUUT.Led[isolt].Imin.ToString() + "A--" + _runUUT.Led[isolt].Imax.ToString() + "A ;\r\n";
                   
                }
                else
                {
                    testInfo += "【" + CLanguage.Lan("产品") + (_runUUT.Led[isolt].iUUT).ToString("D2") +
                                 CLanguage.Lan("通道") + (_runUUT.Led[isolt].iCH).ToString("D2") + "】" + CLanguage.Lan("无产品槽位") + ";\r\n";
                }

                switch (result)
                {
                    case 0:
                        lblUUT[i].BackColor = Color.WhiteSmoke;
                        break;
                    case 1:
                        lblUUT[i].BackColor = Color.LimeGreen;
                        break;
                    case 2:
                        lblUUT[i].BackColor = Color.Red;
                        break;
                    case 3:
                        lblUUT[i].BackColor = Color.Brown;
                        break;
                    case 4:
                        lblUUT[i].BackColor = Color.Green;
                        break;
                    case 5:
                        lblUUT[i].BackColor = Color.Yellow;
                        break;
                    default:
                        lblUUT[i].BackColor = Color.WhiteSmoke;
                        break;
                }
                info.Add(testInfo);
                allinfo += testInfo;
            }

            if (strBackup != allinfo)
            {
                strBackup = allinfo;
                tlTip.RemoveAll();
                tlTip.SetToolTip(pnlUUT, allinfo);
            }
            for (int i = 0; i < _uutMax; i++)
            {
                tlTip.SetToolTip(lblUUT[i], info[i]);
            }
        }

        #endregion

  





    }
}
