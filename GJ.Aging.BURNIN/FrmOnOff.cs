using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.COM;
using GJ.UI;
using GJ.Aging.BURNIN.Udc;

namespace GJ.Aging.BURNIN
{
    public partial class FrmOnOff : Form
    {
        #region 显示窗口

        #region 字段
        private static FrmOnOff dlg = null;
        private static object syncRoot = new object();
        #endregion

        #region 属性
        public static bool IsAvalible
        {
            get
            {
                lock (syncRoot)
                {
                    if (dlg != null && !dlg.IsDisposed)
                        return true;
                    else
                        return false;
                }
            }
        }
        public static Form mdlg
        {
            get
            {
                return dlg;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 创建唯一实例
        /// </summary>
        public static FrmOnOff CreateInstance(int idNo, int outPutNum, COnOff_List OnOff)
        {
            lock (syncRoot)
            {
                if (dlg == null || dlg.IsDisposed)
                {
                    dlg = new FrmOnOff(idNo, outPutNum, OnOff);
                }
            }
            return dlg;
        }
        #endregion

        #endregion

        #region 构造函数
        public FrmOnOff(int idNo, int outPutNum, COnOff_List Output)
        {
            this._idNo = idNo;

            this._outPutNum = outPutNum;

            this._OnOff = Output.Clone();

            InitializeComponent();

            SetUILanguage();

            IntialControl();

            SetDoubleBuffered();
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 绑定控件
        /// </summary>
        private void IntialControl()
        {
            _udcOnOff = new udcOnOff[ONOFF_MAX];

            for (int i = 0; i < _udcOnOff.Length; i++)
            {
                _udcOnOff[i] = new udcOnOff(i, _outPutNum, _OnOff.Item[i].ChkSec, _OnOff.Item[i].OnOffTime,
                                                          _OnOff.Item[i].OnTime, _OnOff.Item[i].OffTime,
														  _OnOff.Item[i].InPutV, _OnOff.Item[i].OutPutType, _OnOff.Item[i].dcONOFF );

                _udcOnOff[i].Dock = DockStyle.Fill;

                _udcOnOff[i].Margin = new Padding(1);

                panel2.Controls.Add(_udcOnOff[i], i, 0);

            }

            labDescribe.Text = _OnOff.Describe;

            txtTotalTime.Text = (((double)_OnOff.TotalTime) / 60).ToString("0.0");

            txtTotalTime.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
        }
        /// <summary>
        /// 设置双缓冲,防止界面闪烁
        /// </summary>
        private void SetDoubleBuffered()
        {
            splitContainer1.Panel1.GetType().GetProperty("DoubleBuffered",
                                              System.Reflection.BindingFlags.Instance |
                                              System.Reflection.BindingFlags.NonPublic)
                                              .SetValue(splitContainer1.Panel1, true, null);

            splitContainer1.Panel2.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(splitContainer1.Panel2, true, null);

            panel1.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(panel1, true, null);

            panel2.GetType().GetProperty("DoubleBuffered",
                                           System.Reflection.BindingFlags.Instance |
                                           System.Reflection.BindingFlags.NonPublic)
                                           .SetValue(panel2, true, null);

        }

        #endregion

        #region 字段
        private int _idNo = 0;
        private int _outPutNum = 0;
        private COnOff_List _OnOff = null;
        private const int ONOFF_MAX = 4;
        #endregion

        #region 面板控件
        /// <summary>
        /// ONOFF控件
        /// </summary>
        private udcOnOff[] _udcOnOff;
        /// <summary>
        /// ONOFF曲线
        /// </summary>
        private udcChartOnOff _udcChart;
        #endregion

        #region 面板回调函数
        private void FrmOnOff_Load(object sender, EventArgs e)
        {
            refreshChart();
        }
        private void OnTextKeyPressIsNumber(object sender, KeyPressEventArgs e)
        {
            //char-8为退格键
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)'.')
                e.Handled = true;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            save();
            dlg.Close();
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            dlg.Close();
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            refreshChart();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 保存设置
        /// </summary>
        private void save()
        {
            try
            {
                _OnOff.Describe = labDescribe.Text;

                _OnOff.TotalTime = (int)(System.Convert.ToDouble(txtTotalTime.Text) * 60);

                for (int i = 0; i < _OnOff.Item.Length; i++)
                {
                    _OnOff.Item[i].ChkSec = _udcOnOff[i].chkSec;

                    _OnOff.Item[i].OnOffTime = _udcOnOff[i].onoffTime;

                    _OnOff.Item[i].OnTime = _udcOnOff[i].onTime;

                    _OnOff.Item[i].OffTime = _udcOnOff[i].offTime;

                    _OnOff.Item[i].InPutV = _udcOnOff[i].inputV;

                    _OnOff.Item[i].OutPutType = _udcOnOff[i].outPutType;

					_OnOff.Item[i].dcONOFF = _udcOnOff[i].dcONOFF;
                }

                OnSaveArgs.OnEvented(new COnOffArgs(_idNo, _OnOff));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// ON/OFF曲线
        /// </summary>
        private void refreshChart()
        {
            try
            {
                if (_udcChart == null)
                {
                    _udcChart = new udcChartOnOff();

                    _udcChart.Dock = DockStyle.Fill;

                    panel1.Controls.Add(_udcChart, 0, 2);
                }

                int maxInput = 0;

                for (int i = 0; i < ONOFF_MAX; i++)
                {
                    if (maxInput < _udcOnOff[i].inputV)
                        maxInput = _udcOnOff[i].inputV;
                }

                _udcChart.maxVolt = maxInput;

                _udcChart.biTime = System.Convert.ToDouble(txtTotalTime.Text) / 60;

                List<udcChartOnOff.COnOff> itemList = new List<udcChartOnOff.COnOff>();

                for (int i = 0; i < ONOFF_MAX; i++)
                {
                    udcChartOnOff.COnOff item = new udcChartOnOff.COnOff();

                    item.curVolt = _udcOnOff[i].inputV;

                    item.onTimes = System.Convert.ToInt32(_udcOnOff[i].onTime);

                    item.offTimes = System.Convert.ToInt32(_udcOnOff[i].offTime);

                    item.onoffTimes = System.Convert.ToInt32(_udcOnOff[i].onoffTime) * (item.onTimes + item.offTimes);

                    item.outPutType = _udcOnOff[i].outPutType;

					item.dcONOFF  = _udcOnOff[i].dcONOFF;

                    itemList.Add(item);
                }

                _udcChart.onoff = itemList;

                _udcChart.Refresh();

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 输出规格事件
        /// </summary>
        public class COnOffArgs : EventArgs
        {
            public COnOffArgs(int idNo, COnOff_List outPut)
            {
                this.idNo = idNo;
                this.outPut = outPut;
            }
            public readonly int idNo;
            public readonly COnOff_List outPut;
        }
        /// <summary>
        /// 保存参数消息
        /// </summary>
        public static COnEvent<COnOffArgs> OnSaveArgs = new COnEvent<COnOffArgs>();
        #endregion

        #region 关闭按钮失效
        /// <summary>
        /// 重写窗体消息
        /// </summary>
        /// <param name="m">屏蔽关闭按钮</param>
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;
            const int SC_MINIMIZE = 0xF020;
            const int SC_MAXIMIZE = 0xF030;
            if (m.Msg == WM_SYSCOMMAND)
            {
                switch ((int)m.WParam)
                {
                    case SC_CLOSE:
                        return;
                    case SC_MINIMIZE:
                        break;
                    case SC_MAXIMIZE:
                        break;
                    default:
                        break;
                }
            }
            base.WndProc(ref m);
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
                    this.Text = "ON/OFF参数设置";
                    btnBrowse.Text = " 浏览";
                    BtnExit.Text = " 取消(&C)";
                    btnOK.Text = " 确定(&O)";
                    labDescribe.Text = "ON/OFF设置";
                    label1.Text = "功能描述:";
                    label2.Text = "总时间:";
                    label3.Text = "分钟";
                    break;
                case CLanguage.EL.英语:
                    break;
                case CLanguage.EL.繁体:
                    this.Text = CLanguage.Lan("ON/OFF参数设置");
                    btnBrowse.Text = CLanguage.Lan(" 浏览");
                    BtnExit.Text = CLanguage.Lan(" 取消(&C)");
                    btnOK.Text = CLanguage.Lan(" 确定(&O)");
                    labDescribe.Text = CLanguage.Lan("ON/OFF设置");
                    label1.Text = CLanguage.Lan("功能描述:");
                    label2.Text = CLanguage.Lan("总时间:");
                    label3.Text = CLanguage.Lan("分钟");
                    break;

                default:
                    break;
            }
        }
        #endregion
    }
}
