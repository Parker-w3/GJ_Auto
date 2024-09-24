using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using GJ.COM;
using GJ.PLUGINS;
using GJ.UI;
using GJ.USER.APP;

namespace GJ.Aging.BURNIN
{
    public partial class FrmDataResults : Form, IChildMsg
    {
        #region 插件方法
        /// <summary>
        /// 父窗口
        /// </summary>
        private Form _father = null;
        /// <summary>
        /// 父窗口唯一标识
        /// </summary>
        private string _fatherGuid = string.Empty;
        /// <summary>
        /// 加载当前窗口及软件版本日期
        /// </summary>
        /// <param name="fatherForm"></param>
        /// <param name="control"></param>
        /// <param name="guid"></param>
        public void OnShowDlg(Form fatherForm, Control control, string guid)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action<Form, Control, string>(OnShowDlg), fatherForm, control, guid);
            else
            {
                this._father = fatherForm;
                this._fatherGuid = guid;
                this.Dock = DockStyle.Fill;
                this.TopLevel = false;
                this.FormBorderStyle = FormBorderStyle.None;
                control.Controls.Add(this);
                this.Show();
            }
        }
        /// <summary>
        /// 关闭当前窗口 
        /// </summary>
        public void OnCloseDlg()
        {

        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user"></param>
        /// <param name="mPwrLevel"></param>
        public void OnLogIn(string user, string password, int[] mPwrLevel)
        {

        }
        /// <summary>
        /// 启动监控
        /// </summary>
        public void OnStartRun()
        {


        }
        /// <summary>
        /// 停止监控
        /// </summary>
        public void OnStopRun()
        {


        }
        /// <summary>
        /// 中英文切换
        /// </summary>
        public void OnChangeLAN()
        {
            SetUILanguage();
        }
        /// <summary>
        /// 消息响应
        /// </summary>
        /// <param name="para"></param>
        public void OnMessage(string name, int lPara, int wPara)
        {

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

        #region 构造函数

        public FrmDataResults()
        {
            InitializeComponent();

            IntialControl();
        }

        #endregion

        #region 初始化

        /// <summary>
        /// 绑定控件
        /// </summary>
        private void IntialControl()
        {
            if (!File.Exists(CGlobalPara.ResultFile))
            {
                return;
            }

            List<string> StrData = new List<string>();      //初始化数据

            StreamReader sr = new StreamReader(CGlobalPara.ResultFile, Encoding.GetEncoding("gb2312"));
            /********************读取记录数据**************************/
            while (sr.Peek() != -1)
            {
                try
                {
                    StrData.Add(sr.ReadLine());
                }
                catch (Exception)
                {
                    continue;
                }
            }
            sr.Close();
            sr = null;
            /******************************************************/

            List<string> ModelName = new List<string>();    //机种名称
            List<string> AreaPath = new List<string>();     //老化区域

            for (int i = 0; i < StrData.Count; i++)
            {
                string[] StrVal = StrData[i].Split(',');
                bool NeedAdd = true;
                for (int j = 0; j < ModelName.Count; j++)
                {
                    if (StrVal[0] == ModelName[j])
                        NeedAdd = false;
                }
                if (NeedAdd)
                    ModelName.Add(StrVal[0]);

                NeedAdd = true;
                for (int j = 0; j < AreaPath.Count; j++)
                {
                    if (StrVal[1] == AreaPath[j])
                        NeedAdd = false;
                }
                if (NeedAdd)
                    AreaPath.Add(StrVal[1]);
            }
            cmbModelSel.Items.Clear();
            cmbModelSel.Items.Add("");
            for (int i = 0; i < ModelName.Count; i++)
            {
                cmbModelSel.Items.Add(ModelName[i]);
            }
            cmbModelSel.SelectedIndex = 0;

            cmbareaSel.Items.Clear();
            cmbareaSel.Items.Add("");
            for (int i = 0; i < AreaPath.Count; i++)
            {
                cmbareaSel.Items.Add(AreaPath[i]);
            }
            cmbareaSel.SelectedIndex = 0;

        }

        #endregion

        private void dgvDataShow_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int cow = e.ColumnIndex;
            int Row = e.RowIndex;
            if (cow == 8)
            {
                string Filepath = dgvDataShow.SelectedCells[0].Value.ToString();

                System.Diagnostics.Process.Start(Filepath);
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (!File.Exists(CGlobalPara.ResultFile))
            {
                MessageBox.Show(CLanguage.Lan("找不到记录文件") + "[" + CGlobalPara.ResultFile + "]");
                return;
            }

            List<string> StrData = new List<string>();      //初始化数据
            StreamReader sr = new StreamReader(CGlobalPara.ResultFile, Encoding.GetEncoding("gb2312"));
            /********************读取记录数据**************************/
            while (sr.Peek() != -1)
            {
                try
                {
                    StrData.Add(sr.ReadLine());
                }
                catch (Exception)
                {
                    continue;
                }
            }
            sr.Close();
            sr = null;
            /******************************************************/

            dgvDataShow.Rows.Clear();
            int ShowNO = 0;
            int TTnum=0;
            int Passnum=0;
            int Failnum=0;

            for (int i = 0; i < StrData.Count; i++)
            {
                string[] StrVal = StrData[i].Split(',');
                DateTime chkTime = System.Convert.ToDateTime(StrVal[2]);
                string startTime = dtpStartData.Value.ToShortDateString();
                DateTime _startTime = System.Convert.ToDateTime(startTime);
                string endTime = dtpEndData.Value.ToShortDateString() + " 23:59:59";
                DateTime _endTime = System.Convert.ToDateTime(endTime);
                if (chkTime.CompareTo(_startTime) >= 0 && chkTime.CompareTo(_endTime) <= 0)
                {

                    if (cmbModelSel.Text == "" && cmbareaSel.Text == "")      //未指定机种参数和区域选择
                    {
                        ShowNO += 1;
                        TTnum+=Convert.ToInt16 (StrVal[4]);
                        Passnum+=Convert.ToInt16 (StrVal[5]);
                        Failnum+=Convert.ToInt16 (StrVal[6]);
                        dgvDataShow.Rows.Add(ShowNO, StrVal[0], StrVal[1], StrVal[2], StrVal[3],
                                                     StrVal[4], StrVal[5], StrVal[6], StrVal[7]);
                    }
                    else
                    {
                        if (cmbModelSel.Text != "" && cmbareaSel.Text == "")
                        {
                            if (StrVal[0] == cmbModelSel.Text)
                            {
                                ShowNO += 1;
                                                        TTnum+=Convert.ToInt16 (StrVal[4]);
                        Passnum+=Convert.ToInt16 (StrVal[5]);
                        Failnum+=Convert.ToInt16 (StrVal[6]);
                                dgvDataShow.Rows.Add(ShowNO, StrVal[0], StrVal[1], StrVal[2], StrVal[3],
                                                             StrVal[4], StrVal[5], StrVal[6], StrVal[7]);
                            }
                        }
                        else if (cmbareaSel.Text != "" && cmbModelSel.Text == "")
                        {
                            if (StrVal[1] == cmbareaSel.Text)
                            {
                                ShowNO += 1;
                                                        TTnum+=Convert.ToInt16 (StrVal[4]);
                        Passnum+=Convert.ToInt16 (StrVal[5]);
                        Failnum+=Convert.ToInt16 (StrVal[6]);
                                dgvDataShow.Rows.Add(ShowNO, StrVal[0], StrVal[1], StrVal[2], StrVal[3],
                                                             StrVal[4], StrVal[5], StrVal[6], StrVal[7]);
                            }
                        }
                        else
                        {
                            if (StrVal[0] == cmbModelSel.Text && StrVal[1] == cmbareaSel.Text)
                            {
                                ShowNO += 1;
                                                        TTnum+=Convert.ToInt16 (StrVal[4]);
                        Passnum+=Convert.ToInt16 (StrVal[5]);
                        Failnum+=Convert.ToInt16 (StrVal[6]);
                                dgvDataShow.Rows.Add(ShowNO, StrVal[0], StrVal[1], StrVal[2], StrVal[3],
                                                             StrVal[4], StrVal[5], StrVal[6], StrVal[7]);
                            }
                        }
                    }
                }
            }

            if (TTnum > 0)
            {
                txtTTNum.Text = TTnum.ToString();
                txtPassNum.Text = Passnum.ToString();
                txtFailNum.Text = Failnum.ToString();
                double TTnum1 = TTnum;
                double Passnum1 = Passnum;
                txtPre.Text = (((Passnum1 / TTnum1)) * 100).ToString("F1");
            }
        }
    }
}

