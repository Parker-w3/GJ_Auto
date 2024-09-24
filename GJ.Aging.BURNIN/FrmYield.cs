using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.PLUGINS;
using GJ.PDB;
using GJ.COM;
namespace GJ.Aging.BURNIN
{
    public partial class FrmYield : Form, IChildMsg
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
                case CLanguage.EL.繁体 :
                    btnQuery.Text=CLanguage.Lan ("查询");
                    Column1.HeaderText = CLanguage.Lan("编号");
                    Column10.HeaderText = CLanguage.Lan("老化状态");
                    Column12.HeaderText = CLanguage.Lan("机种名称");
                    Column2.HeaderText = CLanguage.Lan("时间段");
                    Column3.HeaderText = CLanguage.Lan("总数");
                    Column4.HeaderText = CLanguage.Lan("良品数");
                    Column5.HeaderText = CLanguage.Lan("不良率(%)");
                    Column6.HeaderText = CLanguage.Lan("编号");
                    Column7.HeaderText = CLanguage.Lan("老化位置");
                    Column8.HeaderText = CLanguage.Lan("开始时间");
                    Column9.HeaderText = CLanguage.Lan("运行时间");
                    label1.Text = CLanguage.Lan("老化产能统计(2个小时统计一次)");
                    label10.Text = CLanguage.Lan("累计不良数:");
                    label2.Text = CLanguage.Lan("当前老化中区域:");
                    label3.Text = CLanguage.Lan("当前老化结束区域:");
                    label4.Text = CLanguage.Lan("当前机种数量:");
                    label5.Text = CLanguage.Lan("查询机种名:");
                    label6.Text = CLanguage.Lan("当前即将老化结束时间(MIN):");
                    label7.Text = CLanguage.Lan("当前即将老化结束区域:");
                    label8.Text = CLanguage.Lan("当前出机机种:");
                    label9.Text = CLanguage.Lan("累计总数:");
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 构造函数
        public FrmYield()
        {
            InitializeComponent();
        }
        #endregion

        #region 面板回调函数
        private void FrmYeild_Load(object sender, EventArgs e)
        {

            refreshTotalYield();

            refreshModelList();

            refreshYieldRecord();
            
            refreshChmrStatus();

            timer1.Start();

            string curOutModel = CIniFile.ReadFromIni("Parameter", "curOutModel", CGlobalPara.IniFile);

            labOutModel.Text = curOutModel;

            SetUILanguage();
        
        }
        #endregion

        #region 方法
        private void refreshTotalYield()
        {
            int ttNum = System.Convert.ToInt32(CIniFile.ReadFromIni("DailyYield", "yieldTTNum", CGlobalPara.IniFile, "0"));
            int failNum = System.Convert.ToInt32(CIniFile.ReadFromIni("DailyYield", "yieldFailNum", CGlobalPara.IniFile, "0"));
            labTTNum.Text = ttNum.ToString();
            labFailNum.Text = failNum.ToString();   
        }
        private void refreshModelList()
        {
            try
            {
                string er = string.Empty;

                CDBCOM db = new CDBCOM(EDBType.Access, "", CGlobalPara.SysDB);

                DataSet ds = null;

                string sqlCmd = "select distinct ModelName from RUN_PARA where doRun=" + (int)AgingRunType.运行 + " or doRun=" + (int)AgingRunType.空闲;

                if (!db.QuerySQL(sqlCmd, out ds, out er))
                {
                    MessageBox.Show(er);
                    return;
                }

                labModelNum.Text = ds.Tables[0].Rows.Count.ToString();

                cmbModel.Items.Clear();

                cmbModel.Items.Add( CLanguage.Lan ( "所有老化机种"));

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    cmbModel.Items.Add(ds.Tables[0].Rows[i]["ModelName"].ToString());

                cmbModel.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }            
        }
        /// <summary>
        /// 产能统计
        /// </summary>
        private void refreshYieldRecord()
        {
            try
            {
                string er=string.Empty; 

                YieldView.Rows.Clear();

                CDBCOM db = new CDBCOM(EDBType.Access,"", CGlobalPara.SysDB);

                string sqlCmd = "select * from YieldRecord order by idNo";

                DataSet ds = null;
 
                if(!db.QuerySQL(sqlCmd,out ds,out er))
                {
                      MessageBox.Show(er);
                      return; 
                }
                int dayTTNum = 0;
                int dayPassNum = 0;
                double dayFailRate = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string idNo = (i + 1).ToString(); 
                    string yieldTimes=ds.Tables[0].Rows[i]["YieldTimes"].ToString();
                    int ttNum = System.Convert.ToInt16(ds.Tables[0].Rows[i]["ttNum"].ToString());
                    int passNum = System.Convert.ToInt16(ds.Tables[0].Rows[i]["passNum"].ToString());
                    double failRate = 0;
                    if (ttNum != 0)
                        failRate = (double)(ttNum - passNum) / (double)ttNum;
                    YieldView.Rows.Add(idNo, yieldTimes, ttNum, passNum, failRate.ToString("P2"));
                    dayTTNum += ttNum;
                    dayPassNum += passNum;
                }
                if (dayTTNum != 0)
                    dayFailRate = (double)(dayTTNum - dayPassNum) / (double)dayTTNum;
                YieldView.Rows.Add("*", "00:00--24:00", dayTTNum, dayPassNum, dayFailRate.ToString("P2"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 老化中数量
        /// </summary>
        private void refreshBIStatus()
        {
            try
            {
                string er = string.Empty;

                CDBCOM db = new CDBCOM(EDBType.Access, "", CGlobalPara.SysDB);

                DataSet ds = null;

                string sqlCmd = string.Empty;

                if (cmbModel.Text == CLanguage.Lan ( "所有老化机种"))
                    sqlCmd = "select * from RUN_PARA where doRun=" + (int)AgingRunType.运行 + " order by TimerNO";
                else
                    sqlCmd = "select * from RUN_PARA where doRun=" + (int)AgingRunType.运行 + " and ModelName='" + cmbModel.Text + "' order by TimerNO";

                if (!db.QuerySQL(sqlCmd, out ds, out er))
                {
                    MessageBox.Show(er);
                    return;
                }

                labBINum.Text = ds.Tables[0].Rows.Count.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }            
        }
        /// <summary>
        /// 老化结束数量
        /// </summary>
        private void refreshEndBIStatus()
        {
            try
            {
                string er = string.Empty;

                CDBCOM db = new CDBCOM(EDBType.Access, "", CGlobalPara.SysDB);

                DataSet ds = null;

                string sqlCmd = string.Empty;

                if (cmbModel.Text == CLanguage.Lan ("所有老化机种"))
                    sqlCmd = "select * from RUN_PARA where (doRun=" + (int)AgingRunType.空闲 + ") order by TimerNO";
                else
                    sqlCmd = "select * from RUN_PARA where (doRun=" + (int)AgingRunType.空闲 + ") and ModelName='" + cmbModel.Text + "' order by TimerNO";

                if (!db.QuerySQL(sqlCmd, out ds, out er))
                {
                    MessageBox.Show(er);
                    return;
                }

                labOutNum.Text = ds.Tables[0].Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }           
        }
        /// <summary>
        /// 即将老化结束
        /// </summary>
        private void refreshChmrStatus()
        {
            try
            {
                string er = string.Empty;
 
                CDBCOM db = new CDBCOM(EDBType.Access, "", CGlobalPara.SysDB);

                DataSet ds = null;

                int leftTimes = System.Convert.ToInt16(txtEndTimes.Text)*60;

                string sqlCmd = string.Empty;

                if (cmbModel.Text ==CLanguage.Lan ( "所有老化机种"))
                    sqlCmd = "select RUN_PARA.TimerNO,RUN_PARA.ModelName,RUN_PARA.StartTime," +
                                "RUN_PARA.RunTime,RUN_PARA.doRun,RUN_PARA.BurnTime" +
                                " from RUN_PARA where (RUN_PARA.doRun=" + (int)AgingRunType.空闲 + " or (RUN_PARA.doRun=" + (int)AgingRunType.运行 + " and (RUN_PARA.BurnTime-RUN_PARA.RunTime)<" +
                                leftTimes.ToString() + "))  order by RUN_PARA.TimerNO";
                else
                    sqlCmd = "select RUN_PARA.TimerNO,RUN_PARA.ModelName,RUN_PARA.StartTime," +
                                "RUN_PARA.RunTime,RUN_PARA.doRun,RUN_PARA.BurnTime from RUN_PARA where (RUN_PARA.doRun=" + (int)AgingRunType.空闲 + " or (RUN_PARA.doRun=" + (int)AgingRunType.运行 + " and (RUN_PARA.BurnTime-RUN_PARA.RunTime)<" +
                                leftTimes.ToString() + ")) and RUN_PARA.ModelName='" + cmbModel.Text + "' order by RUN_PARA.TimerNO";

                if (!db.QuerySQL(sqlCmd, out ds, out er))
                {
                    MessageBox.Show(er);
                    return;
                }

                int preNum = ds.Tables[0].Rows.Count;

                uutView.Rows.Clear();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    int idNo = i + 1;

                    string modelName = ds.Tables[0].Rows[i]["ModelName"].ToString();

                    string localName =CGlobalPara.SysPara.Para.timerNO [ Convert .ToInt16 ( ds.Tables[0].Rows[i]["TimerNO"].ToString())];
                    string startTime = ds.Tables[0].Rows[i]["StartTime"].ToString();

                    int runTime = System.Convert.ToInt16(ds.Tables[0].Rows[i]["RunTime"].ToString());

                    TimeSpan ts = new TimeSpan(0, 0, runTime);
                    
                    string runTimes = ts.Days.ToString("D2") + ":" + ts.Hours.ToString("D2") + ":" +
                                      ts.Minutes.ToString("D2") + ":" + ts.Seconds.ToString("D2");    

                    int doRun = System.Convert.ToInt16(ds.Tables[0].Rows[i]["doRun"].ToString());

                    string status = string.Empty;

                    if (doRun == (int)AgingRunType.运行 )
                        status = CLanguage.Lan ("老化中");
                    else
                        status = CLanguage.Lan ("老化结束");

                    uutView.Rows.Add(idNo, modelName,localName, startTime, runTimes, status);

                }

                labPreNum.Text = preNum.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        private void btnQuery_Click(object sender, EventArgs e)
        {
            refreshTotalYield();

            refreshBIStatus();
            
            refreshEndBIStatus();

            refreshChmrStatus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
 
            refreshBIStatus();

            refreshEndBIStatus();
        }

    }
}
