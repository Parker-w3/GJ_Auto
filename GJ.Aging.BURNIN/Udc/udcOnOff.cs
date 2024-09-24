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
    public partial class udcOnOff : UserControl
    {
        #region 构造函数
		public udcOnOff(int idNo, int outPutNum, int chkSec, int onoffTime, int onTime, int offTime, int inputV, int outPutType, int dcONOFF)
        {
            this._idNo = idNo;

            this._outPutNum = outPutNum;

            this._chkSec = chkSec;

            this._onoffTime = onoffTime;

            this._onTime = onTime;

            this._offTime = offTime;

            this._inputV = inputV;

            this._outPutType = outPutType;

			this._dcONOFF = dcONOFF;

            InitializeComponent();

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
            cmbInputV.Items.Clear();
            cmbInputV.Items.Add("110");
            cmbInputV.Items.Add("220");




            txtOnOff.Text = _onoffTime.ToString();
            cmbInputV .Text  = _inputV.ToString();      
            cmbVType.Items.Clear();
            for (int i = 0; i < _outPutNum; i++)
                cmbVType.Items.Add(i + 1);
            cmbVType.SelectedIndex = _outPutType;

			if (_dcONOFF == 1)
				chkDCONOFF.Checked = true;
			else
				chkDCONOFF.Checked = false;

            chkUnit.Text = "ONOFF" + (_idNo + 1).ToString() + CLanguage.Lan("----单位:秒");
            labOnOff.Text = "ONOFF" + (_idNo + 1).ToString() + CLanguage.Lan("循环:");
            labOn.Text = "ON" + (_idNo + 1).ToString() + CLanguage.Lan("时间:");
            labOff.Text = "OFF" + (_idNo + 1).ToString() + CLanguage.Lan("时间:");
            chkUnit.Checked = false;
            if (_chkSec == 1)
            {
                chkUnit.Checked = true;
                txtOn.Text = _onTime.ToString();
                txtOff.Text = _offTime.ToString();
                labOnUint.Text = CLanguage.Lan("秒");
                labOffUint.Text = CLanguage.Lan("秒");
            }
            else
            {
                chkUnit.Checked = false;
                txtOn.Text = (_onTime / 60).ToString();
                txtOff.Text = (_offTime / 60).ToString();
                labOnUint.Text = CLanguage.Lan("分钟");
                labOffUint.Text = CLanguage.Lan("分钟");
            }

            txtOnOff.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
            txtOn.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
            txtOff.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
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
            panel2.GetType().GetProperty("DoubleBuffered",
                                         System.Reflection.BindingFlags.Instance |
                                         System.Reflection.BindingFlags.NonPublic)
                                         .SetValue(panel2, true, null);

        }
        #endregion

        #region 字段
        private CheckBox[] chkSetOn = null;
        #endregion

        #region 字段
        /// <summary>
        /// 编号
        /// </summary>
        private int _idNo = 0;
        /// <summary>
        /// 输出组数
        /// </summary>
        private int _outPutNum = 0;
        /// <summary>
        /// 选择秒
        /// </summary>
        private int _chkSec = 0;
        /// <summary>
        /// 循环次数
        /// </summary>
        private int _onoffTime = 0;
        /// <summary>
        /// ON时间(S)
        /// </summary>
        private int _onTime = 0;
        /// <summary>
        /// OFF时间(S)
        /// </summary>
        private int _offTime = 0;
        /// <summary>
        /// 输入电压
        /// </summary>
        private int _inputV = 0;
        /// <summary>
        /// 输出序号
        /// </summary>
        private int _outPutType = 0;
		/// <summary>
		/// DCONOFF
		/// </summary>
		private int _dcONOFF = 0;
        #endregion

        #region 属性
        /// <summary>
        /// 0：分钟 1：秒
        /// </summary>
        public int chkSec
        {
            set
            {
                _chkSec = value;
                if (_chkSec == 1)
                {
                    chkUnit.Checked = true;
                    labOnUint.Text = CLanguage.Lan("秒");
                    labOffUint.Text = CLanguage.Lan("秒");
                }
                else
                {
                    chkUnit.Checked = false;
                    labOnUint.Text = CLanguage.Lan("分钟");
                    labOffUint.Text = CLanguage.Lan("分钟");
                }
            }
            get
            {
                if (chkUnit.Checked)
                    _chkSec = 1;
                else
                    _chkSec = 0;
                return _chkSec;
            }
        }
        /// <summary>
        /// 循环时间
        /// </summary>
        public int onoffTime
        {
            set
            {
                _onoffTime = value;
                txtOnOff.Text = _onoffTime.ToString();
            }
            get
            {
                _onoffTime = System.Convert.ToInt32(txtOnOff.Text);
                return _onoffTime;
            }
        }
        /// <summary>
        /// ON时间(S)
        /// </summary>
        public int onTime
        {
            set
            {
                _onTime = value;

                if (chkUnit.Checked)
                    txtOn.Text = _onTime.ToString();
                else
                    txtOn.Text = (_onTime / 60).ToString();

            }
            get
            {

                _onTime = System.Convert.ToInt32(txtOn.Text);

                if (chkUnit.Checked)
                    return _onTime;
                else
                    return _onTime * 60;

            }
        }
        /// <summary>
        /// Off时间(S)
        /// </summary>
        public int offTime
        {
            set
            {
                _offTime = value;

                if (chkUnit.Checked)
                    txtOff.Text = _offTime.ToString();
                else
                    txtOff.Text = (_offTime / 60).ToString();
            }
            get
            {
                _offTime = System.Convert.ToInt32(txtOff.Text);

                if (chkUnit.Checked)
                    return _offTime;
                else
                    return _offTime * 60;
            }
        }
        /// <summary>
        /// 输入电压
        /// </summary>
        public int inputV
        {
            set
            {
                _inputV = value;
                cmbInputV.Text  = _inputV.ToString ();
            }
            get
            {
                _inputV = System.Convert.ToInt16(cmbInputV.Text);
                return _inputV;
            }
        }
        /// <summary>
        /// 输出序号
        /// </summary>
        public int outPutType
        {
            set
            {
                _outPutType = value;
                cmbVType.SelectedIndex = _outPutType;
            }
            get
            {
                _outPutType = cmbVType.SelectedIndex;
                return _outPutType;
            }
        }

		/// <summary>
		/// 输出序号
		/// </summary>
		public int dcONOFF
		{
			set
			{
				_dcONOFF  = value;
				if (_dcONOFF == 1)
				{
					chkDCONOFF.Checked = true;
				}
				else
				{
					chkDCONOFF.Checked = false;
				}
			}
			get
			{
				if (chkDCONOFF.Checked)
					_dcONOFF = 1;
				else
					_dcONOFF = 0;
				return _dcONOFF;
			}
		}
 
        #endregion

        #region 面板回调函数
        /// <summary>
        /// 界面初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void udcOnOff_Load(object sender, EventArgs e)
        {

            SetUILanguage();
        }
        /// <summary>
        /// 单位切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkUnit_Click(object sender, EventArgs e)
        {
            if (chkUnit.Checked)
            {
                labOnUint.Text = CLanguage.Lan("秒");
                labOffUint.Text = CLanguage.Lan("秒");
            }
            else
            {
                labOnUint.Text = CLanguage.Lan("分钟");
                labOffUint.Text = CLanguage.Lan("分钟");
            }
        }

        private void OnTextKeyPressIsNumber(object sender, KeyPressEventArgs e)
        {
            //char-8为退格键
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)'.')
                e.Handled = true;
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
                case GJ.COM.CLanguage.EL.中文:
                    chkUnit.Text = CLanguage.Lan("ONOFF1----单位:秒");
                    label1.Text = CLanguage.Lan("输出序号:");
                    label2.Text = CLanguage.Lan("次数");
                    label3.Text = CLanguage.Lan("输入电压:");
                    label4.Text = CLanguage.Lan("V");
                    labOff.Text = CLanguage.Lan("OFF1时间:");
                    labOn.Text = CLanguage.Lan("ON1时间:");
                    labOnOff.Text = CLanguage.Lan("ONOFF1循环:");
                    break;
                case GJ.COM.CLanguage.EL.英语:
                    break;
                case GJ.COM.CLanguage.EL.繁体:
                    chkUnit.Text = CLanguage.Lan("ONOFF1----单位:秒");
                    label1.Text = CLanguage.Lan("输出序号:");
                    label2.Text = CLanguage.Lan("次数");
                    label3.Text = CLanguage.Lan("输入电压:");
                    label4.Text = CLanguage.Lan("V");
                    labOff.Text = CLanguage.Lan("OFF1时间:");
                    labOn.Text = CLanguage.Lan("ON1时间:");
                    labOnOff.Text = CLanguage.Lan("ONOFF1循环:");

                    break;
                default:
                    break;
            }
        }
        #endregion

    }
}
