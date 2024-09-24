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
    public partial class FrmModel : Form, IChildMsg
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
        public void OnLogIn(string user, string password,  int[] mPwrLevel)
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
                    btnAdd.Text = CLanguage.Lan(" 增加");
                    btnBrowse.Text = CLanguage.Lan(" 浏览");
                    btnDel.Text = CLanguage.Lan(" 删除");
                    btnEdit.Text = CLanguage.Lan(" 编辑");
                    btnExit.Text = CLanguage.Lan("退出(&E)");
                    btnNew.Text = CLanguage.Lan("新建(&N)");
                    btnOpen.Text = CLanguage.Lan("打开(&O)");
                    btnSave.Text = CLanguage.Lan("保存(&S)");
                    btnVAdd.Text = CLanguage.Lan(" 增加");
                    btnVDel.Text = CLanguage.Lan(" 删除");
                    btnVEdit.Text = CLanguage.Lan(" 编辑");
                    chkBarFlag.Text = CLanguage.Lan("扫描条码");
                    chkFixBar.Text = CLanguage.Lan("治具条码");
                    chkMesFlag.Text = CLanguage.Lan("上传数据");
                    Column1.HeaderText = CLanguage.Lan("序号");
                    Column3.HeaderText = CLanguage.Lan("输出功能描述");
                    dataGridViewTextBoxColumn1.HeaderText = CLanguage.Lan("编号");
                    dataGridViewTextBoxColumn2.HeaderText = CLanguage.Lan("ON/OFF描述");
                    groupBox1.Text = CLanguage.Lan("基本信息");
                    label1.Text = CLanguage.Lan("机种名称:");
                    label2.Text = CLanguage.Lan(" 机种客户:");
                    label3.Text = CLanguage.Lan("版本:");
                    label4.Text = CLanguage.Lan("发行人:");
                    label6.Text = CLanguage.Lan("条码规则:");
                    label8.Text = CLanguage.Lan("条码长度:");
                    label9.Text = CLanguage.Lan("老化总时间(H):");
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 构造函数
        public FrmModel()
        {
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

            _outPut = new COutPut_List[OUTPUT_MAX];

            for (int i = 0; i < OUTPUT_MAX; i++)
                _outPut[i] = new COutPut_List();

            FrmOutPut.OnSaveArgs.OnEvent += new COnEvent<FrmOutPut.COutPutArgs>.OnEventHandler(OnOutPutSaveArgs);


            cmbDCVNum.Items.Clear();
            cmbDCVNum.Items.Add("1");

        
            cmbDCVNum.SelectedIndex = 0;
           

            _onOff = new COnOff_List[ONOFF_MAX];

            for (int i = 0; i < ONOFF_MAX; i++)
                _onOff[i] = new COnOff_List();

            FrmOnOff.OnSaveArgs.OnEvent += new COnEvent<FrmOnOff.COnOffArgs>.OnEventHandler(OnOnOffSaveArgs);

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
            panel3.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(panel3, true, null);
        }
        #endregion

        #region 字段
        private CModelPara modelPara = new CModelPara();
        private OpenFileDialog openFiledlg = new OpenFileDialog();
        private SaveFileDialog saveFiledlg = new SaveFileDialog();
        #endregion

        #region 面板回调函数
        private void FrmModel_Load(object sender, EventArgs e)
        {
            SetUILanguage();
            
            txtBITime.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
            txtBarlength.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
            clr();
        }
        private void OnTextKeyPressIsNumber(object sender, KeyPressEventArgs e)
        {
            //char-8为退格键
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)'.')
                e.Handled = true;
        }


        /// <summary>
        /// 新建参数 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            clr();
        }
        /// <summary>
        /// 打开参数 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            open();
        }
        /// <summary>
        /// 保存参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            save();
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            string er = string.Empty;

            CReflect.SendWndMethod(_father, EMessType.OnMessage, out er, new object[] { "btnExit", (int)ElPara.退出, 0 });
        }
        private void cmbDCVNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            _outPutChan = System.Convert.ToInt16(cmbDCVNum.Text);
        }
        #endregion

        #region 面板控件
        private udcChartOnOff _udcChart = null;


        #endregion

        #region 文件方法

        /// <summary>
        /// 新建
        /// </summary>
        private void clr()
        {
            txtModel.Text = "";
            txtCustom.Text = "";
            txtPublisher.Text = "";
            txtVersion.Text = "";

            chkMesFlag.Checked =  false;
            chkFixBar.Checked = false;
            chkBarFlag.Checked = true ;
            txtBarSpec.Text ="";
            txtBarlength.Text = "12";

            txtBITime.Text = "120";
            cmbDCVNum.SelectedIndex = 0;
            txtTSet .Text = "50";
            txtTLP.Text = "5";
            txtTHP.Text = "5";
            txtHAlarm.Text = "10";
            txtTOpen.Text = "53";
            txtTClose.Text = "47";
            OutputView.Rows.Clear();
            OnOffView.Rows.Clear();
            _outPutNum = 0;
            _onOffNum = 0;
        }

        /// <summary>
        /// 打开
        /// </summary>
        private void open()
        {
            try
            {
                string modelPath = string.Empty;
                string fileDirectry = string.Empty;
                fileDirectry = CGlobalPara.SysPara.Report.ModelPath;
                openFiledlg.InitialDirectory = fileDirectry;
                openFiledlg.Filter = "BI files (*.bi)|*.bi";
                if (openFiledlg.ShowDialog() != DialogResult.OK)
                    return;
                modelPath = openFiledlg.FileName;
                CSerializable<CModelPara>.ReadXml(modelPath, ref modelPara);

                txtModel.Text = modelPara.Base.Model;
                txtCustom.Text = modelPara.Base.Custom;
                txtPublisher.Text = modelPara.Base.Publisher;
                txtVersion.Text = modelPara.Base.Version;

                chkMesFlag.Checked = (modelPara.Base.MesFlag == 1 ? true : false);
                chkFixBar.Checked = (modelPara.Base.fixBar == 1 ? true : false);
                chkBarFlag.Checked = (modelPara.Base.BarFlag == 1 ? true : false);
                txtBarSpec.Text = modelPara.Base.BarSpec;
                txtBarlength.Text = modelPara.Base.BarLength.ToString();

                txtBITime.Text = (System.Convert.ToDouble(modelPara.Para.BITime) / 60).ToString("F1");
                cmbDCVNum.Text = modelPara.Para.OutPut_Chan.ToString();
              
                txtTSet.Text = modelPara.Para.TSet.ToString();
                txtTLP.Text = modelPara.Para.TLP.ToString();
                txtTHP.Text = modelPara.Para.THP.ToString();
                txtHAlarm.Text = modelPara.Para.THAlarm.ToString();
                txtTOpen.Text = modelPara.Para.TOPEN.ToString();
                txtTClose.Text = modelPara.Para.TCLOSE.ToString();
               
      

                _outPutNum = modelPara.Para.OutPut_Num;

                _outPutChan = modelPara.Para.OutPut_Chan;

                _onOffNum = modelPara.Para.OnOff_Num;

                for (int i = 0; i < modelPara.Para.OutPut_Num; i++)
                    _outPut[i] = modelPara.OutPut[i].Clone();

                for (int i = 0; i < modelPara.Para.OnOff_Num; i++)
                    _onOff[i] = modelPara.OnOff[i].Clone();

                refreshOutPutView();

                refreshOnOffView();

                refreshChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        ///保存
        /// </summary>
        private void save()
        {
            try
            {
                string modelPath = string.Empty;
                string fileDirectry = string.Empty;

                if (CGlobalPara.SysPara.Report.ModelPath == "")
                {
                    fileDirectry = Application.StartupPath + "\\Model";
                    if (!Directory.Exists(fileDirectry))
                        Directory.CreateDirectory(fileDirectry);
                }
                else
                    fileDirectry = CGlobalPara.SysPara.Report.ModelPath;
                saveFiledlg.InitialDirectory = fileDirectry;
                saveFiledlg.Filter = "BI files (*.bi)|*.bi";
                saveFiledlg.FileName = txtModel.Text;
                if (saveFiledlg.ShowDialog() != DialogResult.OK)
                    return;
                modelPath = saveFiledlg.FileName;

                modelPara.Base.Model = txtModel.Text;
                modelPara.Base.Custom = txtCustom.Text;
                modelPara.Base.Version = txtVersion.Text;
                modelPara.Base.Publisher = txtPublisher.Text;

                modelPara.Base.MesFlag = (chkMesFlag.Checked == true ? 1 : 0);
                modelPara.Base.BarFlag = (chkBarFlag.Checked == true ? 1 : 0);
                modelPara.Base.fixBar = (chkFixBar.Checked == true ? 1 : 0);
                modelPara.Base.BarSpec = txtBarSpec.Text;
                modelPara.Base.BarLength = Convert.ToInt16(txtBarlength.Text);

                modelPara.Para.BITime = System.Convert.ToInt32(System.Convert.ToDouble(txtBITime.Text) * 60);
                modelPara.Para.OutPut_Chan = System.Convert.ToInt32(cmbDCVNum.Text);
               
                modelPara.Para.OutPut_Num = _outPutNum;
                modelPara.Para.OnOff_Num = _onOffNum;
                modelPara.Para.TSet = System.Convert.ToDouble(txtTSet.Text);
                modelPara.Para.TLP = System.Convert.ToDouble(txtTLP.Text);
                modelPara.Para.THP = System.Convert.ToDouble(txtTHP.Text);
                modelPara.Para.THAlarm = System.Convert.ToDouble(txtHAlarm.Text);
                modelPara.Para.TOPEN = System.Convert.ToDouble(txtTOpen.Text);
                modelPara.Para.TCLOSE = System.Convert.ToDouble(txtTClose.Text);
            

                for (int i = 0; i < modelPara.Para.OutPut_Num; i++)
                    modelPara.OutPut[i] = _outPut[i].Clone();

                for (int i = 0; i < modelPara.Para.OnOff_Num; i++)
                    modelPara.OnOff[i] = _onOff[i].Clone();

                CSerializable<CModelPara>.WriteXml(modelPath, modelPara);

                MessageBox.Show(CLanguage.Lan("机种参数保存成功."), CLanguage.Lan("机种保存"), MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region 输出规格

        /// <summary>
        /// 最多输出规格
        /// </summary>
        private const int OUTPUT_MAX = 50;
        /// <summary>
        /// 输出列表数
        /// </summary>
        private int _outPutNum = 0;
        /// <summary>
        /// 输出通道数
        /// </summary>
        private int _outPutChan = 0;
        /// <summary>
        /// 输出规格列表
        /// </summary>
        private COutPut_List[] _outPut = null;
        /// <summary>
        /// 刷新输出规格列表
        /// </summary>
        private void refreshOutPutView()
        {
            try
            {
                OutputView.Rows.Clear();

                for (int i = 0; i < modelPara.Para.OutPut_Num; i++)
                {
                    OutputView.Rows.Add(i + 1, _outPut[i].Describe);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        private void SpecView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                DataGridViewTextBoxCell txtCell = (DataGridViewTextBoxCell)OutputView.Rows[e.RowIndex].DataGridView[e.ColumnIndex, e.RowIndex];

                txtCell.Style.BackColor = Color.Cyan;
            }
        }
        private void SpecView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                DataGridViewTextBoxCell txtCell = (DataGridViewTextBoxCell)OutputView.Rows[e.RowIndex].DataGridView[e.ColumnIndex, e.RowIndex];

                txtCell.Style.BackColor = Color.White;

                _outPut[e.RowIndex].Describe = txtCell.Value.ToString();
            }
        }
        private void btnVAdd_Click(object sender, EventArgs e)
        {
            if (_outPutNum >= OUTPUT_MAX)
            {
                MessageBox.Show(CLanguage.Lan("设置输出规格组数超过最多") + "【" + OUTPUT_MAX.ToString() + "】", CLanguage.Lan("输出规格"),
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            OutputView.Rows.Add(OutputView.Rows.Count + 1, "");

            _outPutNum++;
            if (_outPutNum > 1)
                _outPut[_outPutNum - 1] = _outPut[_outPutNum - 2];

            _outPutChan = System.Convert.ToInt16(cmbDCVNum.Text);

        }
        private void btnVEdit_Click(object sender, EventArgs e)
        {
            if (OutputView.SelectedCells.Count == 0)
                return;

            if (OutputView.SelectedCells[0].RowIndex < 0)
                return;

            _outPutChan = System.Convert.ToInt16(cmbDCVNum.Text);

            int row = OutputView.SelectedCells[0].RowIndex;

            _outPut[row].Describe = OutputView.Rows[row].Cells[1].Value.ToString();

            FrmOutPut.CreateInstance(row, _outPutChan, _outPut[row]).Show();

        }
        private void btnVDel_Click(object sender, EventArgs e)
        {
            if (OutputView.SelectedCells.Count == 0)
                return;

            if (OutputView.SelectedCells[0].RowIndex < 0)
                return;

            int row = OutputView.SelectedCells[0].RowIndex;

            OutputView.Rows.RemoveAt(row);

            _outPutNum--;

            for (int i = row; i < OUTPUT_MAX - 1; i++)
                _outPut[i] = _outPut[i + 1].Clone();

            for (int i = 0; i < OutputView.Rows.Count; i++)
                OutputView.Rows[i].Cells[0].Value = (i + 1).ToString();

        }
        private void OnOutPutSaveArgs(object sender, FrmOutPut.COutPutArgs e)
        {
            _outPut[e.idNo] = e.outPut.Clone();
        }
        #endregion

        #region ON/OFF规格
        /// <summary>
        /// 最多ON/OFF段数
        /// </summary>
        private const int ONOFF_MAX = 8;
        /// <summary>
        /// 输出ONOFF段数
        /// </summary>
        private int _onOffNum = 0;
        /// <summary>
        /// ONOFF列表
        /// </summary>
        private COnOff_List[] _onOff = null;
        /// <summary>
        /// 刷新输出规格列表
        /// </summary>
        private void refreshOnOffView()
        {
            try
            {
                OnOffView.Rows.Clear();

                for (int i = 0; i < modelPara.Para.OnOff_Num; i++)
                {
                    OnOffView.Rows.Add(i + 1, _onOff[i].Describe);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        private void OnOffView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {

                DataGridViewTextBoxCell txtCell = (DataGridViewTextBoxCell)OnOffView.Rows[e.RowIndex].DataGridView[e.ColumnIndex, e.RowIndex];

                txtCell.Style.BackColor = Color.Cyan;

                _onOff[e.RowIndex].Describe = txtCell.Value.ToString();
            }
        }
        private void OnOffView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                DataGridViewTextBoxCell txtCell = (DataGridViewTextBoxCell)OnOffView.Rows[e.RowIndex].DataGridView[e.ColumnIndex, e.RowIndex];

                txtCell.Style.BackColor = Color.White;
            }
            if (e.RowIndex >= 0 && e.ColumnIndex == 2)
            {
                DataGridViewTextBoxCell txtCell = (DataGridViewTextBoxCell)OnOffView.Rows[e.RowIndex].DataGridView[e.ColumnIndex, e.RowIndex];
                if (!VldInt(txtCell.Value.ToString()))
                    txtCell.Value = 0;
                txtCell.Style.BackColor = Color.White;
            }
            if (e.RowIndex >= 0 && e.ColumnIndex == 3)
            {
                DataGridViewTextBoxCell txtCell = (DataGridViewTextBoxCell)OnOffView.Rows[e.RowIndex].DataGridView[e.ColumnIndex, e.RowIndex];
                if (!VldInt(txtCell.Value.ToString()))
                    txtCell.Value = 0;
                txtCell.Style.BackColor = Color.White;
            }

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (_outPutNum == 0)
            {
                MessageBox.Show(CLanguage.Lan("请先设置输出规格,再设置") + "ON/OFF" + CLanguage.Lan("参数"), CLanguage.Lan("设置") + "ON/OFF",
                                 MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (_onOffNum >= ONOFF_MAX)
            {
                MessageBox.Show(CLanguage.Lan("设置ON/OFF组数超过最多【") + ONOFF_MAX.ToString() + "】", CLanguage.Lan("设置") + "ON/OFF",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            OnOffView.Rows.Add(OnOffView.Rows.Count + 1, "", 0, 0);

            _onOffNum++;

        }
        /// <summary>
        /// 编辑项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (OnOffView.SelectedCells.Count == 0)
                return;

            if (OnOffView.SelectedCells[0].RowIndex < 0)
                return;

            int row = OnOffView.SelectedCells[0].RowIndex;

            _onOff[row].Describe = OnOffView.Rows[row].Cells[1].Value.ToString();

            FrmOnOff.CreateInstance(row, _outPutNum, _onOff[row]).Show();
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {

            if (OnOffView.SelectedCells.Count == 0)
                return;

            if (OnOffView.SelectedCells[0].RowIndex < 0)
                return;

            int row = OnOffView.SelectedCells[0].RowIndex;

            OnOffView.Rows.RemoveAt(row);

            _onOffNum--;


            for (int i = 0; i < OnOffView.Rows.Count; i++)
                OnOffView.Rows[i].Cells[0].Value = (i + 1).ToString();
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            refreshChart();
        }
        private void OnOnOffSaveArgs(object sender, FrmOnOff.COnOffArgs e)
        {
            _onOff[e.idNo] = e.outPut.Clone();
        }
        private void refreshChart()
        {
            try
            {
                if (_udcChart == null)
                {
                    _udcChart = new udcChartOnOff();

                    _udcChart.Dock = DockStyle.Fill;

                    panel3.Controls.Add(_udcChart, 0, 2);
                }

                int maxInputV = 0;

                for (int i = 0; i < _onOffNum; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (maxInputV < _onOff[i].Item[j].InPutV)
                            maxInputV = _onOff[i].Item[j].InPutV;
                    }
                }

                _udcChart.maxVolt = maxInputV;

                _udcChart.biTime = System.Convert.ToDouble(txtBITime.Text)/60;

                List<udcChartOnOff.COnOff> itemList = calOnOffItem();

                _udcChart.onoff = itemList;

                _udcChart.Refresh();

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 计算总时序时间段参数
        /// </summary>
        private List<udcChartOnOff.COnOff> calOnOffItem()
        {
            try
            {
                List<udcChartOnOff.COnOff> itemList = new List<udcChartOnOff.COnOff>();

                int burnTime = (int)(_udcChart.biTime * 3600);

                int leftTime = burnTime;

                int onoffNoNum = 0;
                List<int> onoffNo = new List<int>();

                for (int i = 0; i < _onOffNum; i++)
                {
                        onoffNo.Add(i);
                        onoffNoNum += 1;
    
                }


                while (leftTime > 0)
                {
                    for (int i = 0; i < onoffNoNum; i++)
                    {
                        int itemTime = (int)(_onOff[onoffNo[i]].TotalTime);

                        if (leftTime < itemTime)  //剩余时间<ON/OFF组时间
                        {
                            itemTime = leftTime;

                            leftTime = 0;
                        }
                        else
                        {
                            leftTime -= itemTime;
                        }
                        //4小段ON/OFF时间

                        int itemLeftTime = itemTime;

                        while (itemLeftTime > 0)
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                if (itemLeftTime == 0)
                                    break;

                                int onoffTime = _onOff[onoffNo[i]].Item[j].OnOffTime * (_onOff[onoffNo[i]].Item[j].OnTime + _onOff[onoffNo[i]].Item[j].OffTime);

                                if (onoffTime == 0)
                                    continue;

                                //单个ON/OFF时序
                                for (int z = 0; z < _onOff[onoffNo[i]].Item[j].OnOffTime; z++)
                                {
                                    udcChartOnOff.COnOff onoffItem = new udcChartOnOff.COnOff();

                                    onoffItem.curVolt = _onOff[onoffNo[i]].Item[j].InPutV;

                                    onoffItem.outPutType = _onOff[onoffNo[i]].Item[j].OutPutType;

                                    //ON段
                                    if (itemLeftTime >= _onOff[onoffNo[i]].Item[j].OnTime)
                                    {
                                        onoffItem.onTimes = _onOff[onoffNo[i]].Item[j].OnTime;

                                        onoffItem.onoffTimes += onoffItem.onTimes;

                                        itemLeftTime -= _onOff[onoffNo[i]].Item[j].OnTime;
                                    }
                                    else
                                    {
                                        onoffItem.onTimes = itemLeftTime;

                                        onoffItem.onoffTimes += onoffItem.onTimes;

                                        itemLeftTime = 0; ;
                                    }

                                    //OFF段
                                    if (itemLeftTime >= _onOff[onoffNo[i]].Item[j].OnOffTime)
                                    {
                                        onoffItem.offTimes = _onOff[onoffNo[i]].Item[j].OffTime;

                                        onoffItem.onoffTimes += onoffItem.offTimes;

                                        itemLeftTime -= _onOff[onoffNo[i]].Item[j].OffTime;
                                    }
                                    else
                                    {
                                        onoffItem.offTimes = itemLeftTime;

                                        onoffItem.onoffTimes += onoffItem.offTimes;

                                        itemLeftTime = 0; ;
                                    }
                                    if (onoffItem.onoffTimes > 0)
                                        itemList.Add(onoffItem);
                                }
                            }
                        }
                    }
                }
                return itemList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region
        /// <summary>
        /// 判断输入是否数字
        /// </summary>
        /// <param name="num">要判断的字符串</param>
        /// <returns></returns>
        private bool VldInt(string num)
        {
            try
            {
                Convert.ToInt32(num);
                return true;
            }
            catch { return false; }
        }

        #endregion


    }
}
