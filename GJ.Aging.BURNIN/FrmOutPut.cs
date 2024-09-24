using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.COM;

namespace GJ.Aging.BURNIN
{
    public partial class FrmOutPut : Form
    {
        #region 显示窗口

        #region 字段
        private static FrmOutPut dlg = null;
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
        public static FrmOutPut CreateInstance(int idNo, int chanNum,  COutPut_List Output)
        {
            lock (syncRoot)
            {
                if (dlg == null || dlg.IsDisposed)
                {
                    dlg = new FrmOutPut(idNo, chanNum, Output);
                }
            }
            return dlg;
        }
        #endregion

        #endregion

        #region 构造函数
        public FrmOutPut(int idNo, int chanNum,  COutPut_List Output)
        {
            InitializeComponent();

            IntialControl();

            SetDoubleBuffered();

            load(idNo, chanNum, Output);
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 绑定控件
        /// </summary>
        private void IntialControl()
        {

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

            pnl1.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(pnl1, true, null);

            panel2.GetType().GetProperty("DoubleBuffered",
                                           System.Reflection.BindingFlags.Instance |
                                           System.Reflection.BindingFlags.NonPublic)
                                           .SetValue(panel2, true, null);

        }
        #endregion

        #region 面板控件
        /// <summary>
        /// 电压输出控件
        /// </summary>
        private class CV_CTR
        {
            /// <summary>
            /// 使用状态
            /// </summary>
            public CheckBox chkIsUsed = new CheckBox();
            /// <summary>
            /// 输出名称
            /// </summary>
            public TextBox txtName = new TextBox();
            /// <summary>
            /// 输出下限
            /// </summary>
            public TextBox txtVmin = new TextBox();
            /// <summary>
            /// 输出上限
            /// </summary>
            public TextBox txtVmax = new TextBox();
            /// <summary>
            /// 负载模式
            /// </summary>
            public ComboBox cmbIMode = new ComboBox();
            /// <summary>
            /// 负载设置
            /// </summary>
            public TextBox txtISet = new TextBox();
            /// <summary>
            /// 电流下限
            /// </summary>
            public TextBox txtImin = new TextBox();
            /// <summary>
            /// 电流上限
            /// </summary>
            public TextBox txtImax = new TextBox();
            /// <summary>
            /// Von设置
            /// </summary>
            public TextBox txtVon = new TextBox();
            /// <summary>
            /// 附加模式
            /// </summary>
            public TextBox txtAddMode = new TextBox();
 
        }

        /// <summary>
        /// CAN数据
        /// </summary>
        private string canData = string.Empty;
        /// <summary>
        /// 电压输出参数
        /// </summary>
        private TableLayoutPanel vOutPanel = null;
        /// <summary>
        /// 电压输出控件
        /// </summary>
        private List<CV_CTR> vControl = new List<CV_CTR>();
        /// <summary>
        /// 数据判定使用
        /// </summary>
        private CheckBox[] chkData = new CheckBox[3];

        /// <summary>
        /// 数据判定上限
        /// </summary>
        private TextBox[] txtDataMax = new TextBox[3];

        /// <summary>
        /// 数据判定下限
        /// </summary>
        private TextBox[] txtDataMin = new TextBox[3];

        #endregion

        #region 面板回调函数
        private void FrmOutPut_Load(object sender, EventArgs e)
        {

        }
        private void OnTextKeyPressIsNumber(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)'-' || e.KeyChar == (char)'+')
                return;
            //char-8为退格键
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)'.')
                e.Handled = true;
        }
        /// <summary>
        /// 电压选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCheckChanged(object sender, EventArgs e)
        {
            CheckBox chkBox = (CheckBox)sender;

            int idNo = System.Convert.ToInt16(chkBox.Tag.ToString());

            if (chkBox.Checked)
            {
                vControl[idNo].txtName.Enabled = true;
                vControl[idNo].txtVmin.Enabled = true;
                vControl[idNo].txtVmax.Enabled = true;
                vControl[idNo].cmbIMode.Enabled = true;
                vControl[idNo].txtISet.Enabled = true;
                vControl[idNo].txtImin.Enabled = true;
                vControl[idNo].txtImax.Enabled = true;
                vControl[idNo].txtVon.Enabled = true;
                vControl[idNo].txtAddMode.Enabled = true;
            }
            else
            {
                vControl[idNo].txtName.Enabled = false;
                vControl[idNo].txtVmin.Enabled = false;
                vControl[idNo].txtVmax.Enabled = false;
                vControl[idNo].cmbIMode.Enabled = false;
                vControl[idNo].txtISet.Enabled = false;
                vControl[idNo].txtImin.Enabled = false;
                vControl[idNo].txtImax.Enabled = false;
                vControl[idNo].txtVon.Enabled = false;
                vControl[idNo].txtAddMode.Enabled = false;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            save();

            dlg.Close();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExit_Click(object sender, EventArgs e)
        {
            dlg.Close();
        }

        #endregion

        #region 面板方法
        /// 刷新电压输出规格
        /// </summary>
        private void refreshOutPutPara()
        {
            try
            {
                if (vOutPanel != null)
                {
                    vOutPanel.Dispose();
                    vOutPanel = null;
                }
                //初始化电压控件
                int rowNum = _chanNum;

                vOutPanel = new TableLayoutPanel();

                vOutPanel.Dock = DockStyle.Fill;

                vOutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

                vOutPanel.RowCount = rowNum + 2;

                if (rowNum <= 16)
                {
                    vOutPanel.AutoScroll = false;
                }
                else
                {
                    vOutPanel.AutoScroll = true;
                }

                vOutPanel.ColumnCount = 12;

                vOutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
                for (int i = 0; i < rowNum + 1; i++)
                    vOutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
                vOutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

                vOutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 110));
                vOutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                vOutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                vOutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                vOutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                vOutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                vOutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                vOutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                vOutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                vOutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));


                Label lab1 = new Label();
                lab1.Dock = DockStyle.Fill;
                lab1.TextAlign = ContentAlignment.MiddleCenter;
                lab1.Text = CLanguage.Lan("输出通道");
                vOutPanel.Controls.Add(lab1, 0, 0);

                Label lab2 = new Label();
                lab2.Dock = DockStyle.Fill;
                lab2.TextAlign = ContentAlignment.MiddleCenter;
                lab2.Text = CLanguage.Lan("输出名称");
                vOutPanel.Controls.Add(lab2, 1, 0);

                Label lab3 = new Label();
                lab3.Dock = DockStyle.Fill;
                lab3.TextAlign = ContentAlignment.MiddleCenter;
                lab3.Text = CLanguage.Lan("电压下限(V)");
                vOutPanel.Controls.Add(lab3, 2, 0);

                Label lab4 = new Label();
                lab4.Dock = DockStyle.Fill;
                lab4.TextAlign = ContentAlignment.MiddleCenter;
                lab4.Text = CLanguage.Lan("电压上限(V)");
                vOutPanel.Controls.Add(lab4, 3, 0);

                Label lab5 = new Label();
                lab5.Dock = DockStyle.Fill;
                lab5.TextAlign = ContentAlignment.MiddleCenter;
                lab5.Text = CLanguage.Lan("负载模式");
                vOutPanel.Controls.Add(lab5, 4, 0);

                Label lab6 = new Label();
                lab6.Dock = DockStyle.Fill;
                lab6.TextAlign = ContentAlignment.MiddleCenter;
                lab6.Text = CLanguage.Lan("负载设置");
                vOutPanel.Controls.Add(lab6, 5, 0);

                Label lab7 = new Label();
                lab7.Dock = DockStyle.Fill;
                lab7.TextAlign = ContentAlignment.MiddleCenter;
                lab7.Text = CLanguage.Lan("电流下限(A)");
                vOutPanel.Controls.Add(lab7, 6, 0);

                Label lab8 = new Label();
                lab8.Dock = DockStyle.Fill;
                lab8.TextAlign = ContentAlignment.MiddleCenter;
                lab8.Text = CLanguage.Lan("电流上限(A)");
                vOutPanel.Controls.Add(lab8, 7, 0);

                Label lab9 = new Label();
                lab9.Dock = DockStyle.Fill;
                lab9.TextAlign = ContentAlignment.MiddleCenter;
                lab9.Text = "Von(V)";
                vOutPanel.Controls.Add(lab9, 8, 0);

                Label lab10 = new Label();
                lab10.Dock = DockStyle.Fill;
                lab10.TextAlign = ContentAlignment.MiddleCenter;
                lab10.Text = CLanguage.Lan("附加模式");
                vOutPanel.Controls.Add(lab10, 9, 0);

                vControl.Clear();

                for (int i = 0; i < rowNum; i++)
                {
                    vControl.Add(new CV_CTR());

                    vControl[i].chkIsUsed.Dock = DockStyle.Fill;
                    vControl[i].chkIsUsed.Checked = true;
                    vControl[i].chkIsUsed.Margin = new Padding(20, 0, 0, 0);
                    vControl[i].chkIsUsed.Name = "CH" + i.ToString();
                    vControl[i].chkIsUsed.Text = "CH" + (i + 1).ToString();
                    vControl[i].chkIsUsed.Tag = i;
                    vControl[i].chkIsUsed.CheckedChanged += new EventHandler(OnCheckChanged);
                    vControl[i].chkIsUsed.Checked = (_outPut.Chan[i].Vuse == 1 ? true : false);

                    vControl[i].txtName.Dock = DockStyle.Fill;
                    vControl[i].txtName.TextAlign = HorizontalAlignment.Center;
                    vControl[i].txtName.Margin = new Padding(1);
                    vControl[i].txtName.Text = _outPut.Chan[i].Vname;

                    vControl[i].txtVmin.Dock = DockStyle.Fill;
                    vControl[i].txtVmin.TextAlign = HorizontalAlignment.Center;
                    vControl[i].txtVmin.Margin = new Padding(1);
                    vControl[i].txtVmin.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
                    vControl[i].txtVmin.Text = _outPut.Chan[i].Vmin.ToString();

                    vControl[i].txtVmax.Dock = DockStyle.Fill;
                    vControl[i].txtVmax.TextAlign = HorizontalAlignment.Center;
                    vControl[i].txtVmax.Margin = new Padding(1);
                    vControl[i].txtVmax.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
                    vControl[i].txtVmax.Text = _outPut.Chan[i].Vmax.ToString();

                    vControl[i].cmbIMode.Dock = DockStyle.Fill;
                    vControl[i].cmbIMode.Margin = new Padding(1);
                    vControl[i].cmbIMode.DropDownStyle = ComboBoxStyle.DropDownList;
                    vControl[i].cmbIMode.Items.Clear();
                    vControl[i].cmbIMode.Items.Add("0:CC_Slow");
                    vControl[i].cmbIMode.Items.Add("1:CV");
                    vControl[i].cmbIMode.Items.Add("2:CP");
                    vControl[i].cmbIMode.Items.Add("3:CR");
                    vControl[i].cmbIMode.Items.Add("4:CC_Fast");
                    vControl[i].cmbIMode.Items.Add("5:LED_Slow");
                    vControl[i].cmbIMode.Items.Add("6:LED_Fast");
                    //vControl[i].cmbIMode.Items.Add("7:MTK");
                    //vControl[i].cmbIMode.Items.Add("8:Power");

                    vControl[i].cmbIMode.SelectedIndex = (int)_outPut.Chan[i].Imode;

                    vControl[i].txtISet.Dock = DockStyle.Fill;
                    vControl[i].txtISet.TextAlign = HorizontalAlignment.Center;
                    vControl[i].txtISet.Margin = new Padding(1);
                    vControl[i].txtISet.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
                    vControl[i].txtISet.Text = _outPut.Chan[i].ISet.ToString();

                    vControl[i].txtImin.Dock = DockStyle.Fill;
                    vControl[i].txtImin.TextAlign = HorizontalAlignment.Center;
                    vControl[i].txtImin.Margin = new Padding(1);
                    vControl[i].txtImin.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
                    vControl[i].txtImin.Text = _outPut.Chan[i].Imin.ToString();

                    vControl[i].txtImax.Dock = DockStyle.Fill;
                    vControl[i].txtImax.TextAlign = HorizontalAlignment.Center;
                    vControl[i].txtImax.Margin = new Padding(1);
                    vControl[i].txtImax.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
                    vControl[i].txtImax.Text = _outPut.Chan[i].Imax.ToString();

                    vControl[i].txtVon.Dock = DockStyle.Fill;
                    vControl[i].txtVon.TextAlign = HorizontalAlignment.Center;
                    vControl[i].txtVon.Margin = new Padding(1);
                    vControl[i].txtVon.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
                    vControl[i].txtVon.Text = _outPut.Chan[i].Von.ToString();

                    vControl[i].txtAddMode.Dock = DockStyle.Fill;
                    vControl[i].txtAddMode.TextAlign = HorizontalAlignment.Center;
                    vControl[i].txtAddMode.Margin = new Padding(1);
                    vControl[i].txtAddMode.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
                    vControl[i].txtAddMode.Text = _outPut.Chan[i].AddMode.ToString();

                    vOutPanel.Controls.Add(vControl[i].chkIsUsed, 0, i + 1);
                    vOutPanel.Controls.Add(vControl[i].txtName, 1, i + 1);
                    vOutPanel.Controls.Add(vControl[i].txtVmin, 2, i + 1);
                    vOutPanel.Controls.Add(vControl[i].txtVmax, 3, i + 1);
                    vOutPanel.Controls.Add(vControl[i].cmbIMode, 4, i + 1);
                    vOutPanel.Controls.Add(vControl[i].txtISet, 5, i + 1);
                    vOutPanel.Controls.Add(vControl[i].txtImin, 6, i + 1);
                    vOutPanel.Controls.Add(vControl[i].txtImax, 7, i + 1);
                    vOutPanel.Controls.Add(vControl[i].txtVon, 8, i + 1);
                    vOutPanel.Controls.Add(vControl[i].txtAddMode, 9, i + 1);
                }

                chkData = new CheckBox[] { chkData0, chkData1, chkData2 };
                txtDataMax = new TextBox[] { txtDataMax0, txtDataMax1, txtDataMax2 };
                txtDataMin = new TextBox[] { txtDataMin0, txtDataMin1, txtDataMin2 };

                pnl1.Controls.Add(vOutPanel, 0, 0);

                labDescribe.Text = _outPut.Describe;

                if (_outPut.SpecData != null)
                {
                    string[] spec = _outPut.SpecData.Split('^');

                    if (spec.Length > 0)
                    {


                        for (int i = 0; i < 3; i++)
                        {
                            chkData[i].Checked = Convert.ToInt16(spec[0 + i * 3]) == 1 ? true : false;
                            txtDataMax[i].Text = spec[1 + i * 3];
                            txtDataMax[i].KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
                            txtDataMin[i].Text = spec[2 + i * 3];
                            txtDataMin[i].KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region 字段
        /// <summary>
        /// 输出参数
        /// </summary>
        private COutPut_List _outPut = null;
        private int _idNo = 0;
        /// <summary>
        /// 使用通道数
        /// </summary>
        private int _chanNum = 0;

        #endregion

        #region 方法
        /// <summary>
        /// 加载设置
        /// </summary>
        /// <param name="idNo"></param>
        /// <param name="Output"></param>
        public void load(int idNo, int chanNum, COutPut_List Output)
        {
            try
            {
                this._idNo = idNo;

                this._chanNum = chanNum;

                SetUILanguage();

                _outPut = Output.Clone();

                refreshOutPutPara();

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存设置
        /// </summary>
        public void save()
        {
            try
            {
                _outPut.Describe = labDescribe.Text;

                string specStr = string.Empty ;

                for (int i = 0; i < 3; i++)
                {
                    specStr += ((chkData[i].Checked == true ? "1" : "0") + "^");
                    specStr += (txtDataMax[i].Text + "^");
                    specStr += (txtDataMin[i].Text + "^");
                }

                _outPut.SpecData = specStr;

                for (int i = 0; i < _chanNum; i++)
                {
                    _outPut.Chan[i].Vuse = (vControl[i].chkIsUsed.Checked == true ? 1 : 0);
                    _outPut.Chan[i].Vname = vControl[i].txtName.Text;
                    _outPut.Chan[i].Vmin = System.Convert.ToDouble(vControl[i].txtVmin.Text);
                    _outPut.Chan[i].Vmax = System.Convert.ToDouble(vControl[i].txtVmax.Text);
                    _outPut.Chan[i].Imode = vControl[i].cmbIMode.SelectedIndex;
                    _outPut.Chan[i].ISet = System.Convert.ToDouble(vControl[i].txtISet.Text);
                    _outPut.Chan[i].Imin = System.Convert.ToDouble(vControl[i].txtImin.Text);
                    _outPut.Chan[i].Imax = System.Convert.ToDouble(vControl[i].txtImax.Text);
                    _outPut.Chan[i].Von = System.Convert.ToDouble(vControl[i].txtVon.Text);
                    _outPut.Chan[i].AddMode = System.Convert.ToDouble(vControl[i].txtAddMode.Text);

                }

                OnSaveArgs.OnEvented(new COutPutArgs(_idNo, _outPut));

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
        public class COutPutArgs : EventArgs
        {
            public COutPutArgs(int idNo, COutPut_List outPut)
            {
                this.idNo = idNo;
                this.outPut = outPut;
            }
            public readonly int idNo;
            public readonly COutPut_List outPut;
        }
        /// <summary>
        /// 保存参数消息
        /// </summary>
        public static COnEvent<COutPutArgs> OnSaveArgs = new COnEvent<COutPutArgs>();
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
                    this.Text = "输出规格设置";
                    BtnExit.Text = " 取消(&C)";
                    btnOK.Text = " 确定(&O)";
                    labDescribe.Text = "输出规格设置";
                    label1.Text = "功能描述:";
                    break;
                case CLanguage.EL.英语:
                    break;
                case CLanguage.EL.繁体:
                    this.Text = CLanguage.Lan("输出规格设置");
                    BtnExit.Text = CLanguage.Lan(" 取消(&C)");
                    btnOK.Text = CLanguage.Lan(" 确定(&O)");
                    labDescribe.Text = CLanguage.Lan("输出规格设置");
                    label1.Text = CLanguage.Lan("功能描述:");
                    break;

                default:
                    break;
            }

        }
        #endregion
    }
}
