using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using GJ.COM;
using GJ.MES.Sajet;

namespace GJ.Aging.BURNIN.Udc
{
    public partial class udcSerialNo : Form
    {

        #region 枚举

        /// <summary>
        /// 条码状态
        /// </summary>
        public enum ESetMenu
        {
            位置条码OK,
            治具条码OK,
            产品条码OK,
            扫码完成,
            清除条码,
            位置空闲
        }
        #endregion

        #region 构造函数
        public udcSerialNo(CUUT runUUT, int ChanNum = 1)
        {
            InitializeComponent();

            this.runUUT = runUUT;

            this.idNo = runUUT.Para.TimerNo;

            this.ChanNum = ChanNum;

            SetUILanguage();

            SetDoubleBuffered();

            loadMainForm();

 
        }
        #endregion

        #region 面板控件
        private TableLayoutPanel panelMain = null;
        private List<Label> lblId = new List<Label>();
        private List<Label> lblLocalPath = new List<Label>();
        private List<Label> lblLocalBar = new List<Label>();
     //   private List<Label> lblFixBar = new List<Label>();
        private List<Label> lblSn = new List<Label>();
        private List<Label> lblResult = new List<Label>();
     //   private List<Button> btnClearBar = new List<Button>();
        #endregion

        #region 面板变量
        /// <summary>
        /// 序号
        /// </summary>
        private int idNo = 0;

        /// <summary>
        /// 老化区信息 
        /// </summary>
        private CUUT runUUT = null;
        /// <summary>
        /// 并联数量
        /// </summary>
        private int ChanNum = 1;
        /// <summary>
        /// 可扫描条码数
        /// </summary>
        private int slotNum = 0;

        /// <summary>
        /// 位置
        /// </summary>
        private List<string> localPath = new List<string>();

        /// <summary>
        /// 位置条码
        /// </summary>
        private List<string> localBar = new List<string>();
        /// <summary>
        /// 治具条码
        /// </summary>
        private List<string> fixBar = new List<string>();
        /// <summary>
        /// 产品条码
        /// </summary>
        private List<string> serialNos = new List<string>();
        /// <summary>
        /// 上传数据1
        /// </summary>
        private List<string> OracleStr1 = new List<string>();
        /// <summary>
        /// 上传数据2
        /// </summary>
        private List<string> OracleStr2 = new List<string>();

        /// <summary>
        /// 结果
        /// </summary>
        private List<int> result = new List<int>();

        /// <summary>
        /// 使用状态
        /// </summary>
        private List<int> barType = new List<int>();

        /// <summary>
        /// 扫描次序
        /// </summary>
        private int ScanBarItem = 0;


        /// <summary>
        /// 扫描编号
        /// </summary>
        private int barNo = 0;

        /// <summary>
        /// 清除编号
        /// </summary>
        private int selNo = 0;


        //private  SpeechSynthesizer speech = new SpeechSynthesizer();
        #endregion

        #region 面板初始化
        ///// <summary>
        ///// 初始化面板
        ///// </summary>
        //private void loadMainForm()
        //{

        //    labTitle.Text = CLanguage.Lan("扫描[") + runUUT.Para.TimerName + CLanguage.Lan("]产品条码");

        //    panelMain = new TableLayoutPanel();
        //    panelMain.BackColor = Color.Transparent;
        //    panelMain.Dock = DockStyle.Fill;
        //    panelMain.AutoScroll = true;
        //    panelMain.GetType().GetProperty("DoubleBuffered",
        //                                  System.Reflection.BindingFlags.Instance |
        //                                  System.Reflection.BindingFlags.NonPublic)
        //                                  .SetValue(panelMain, true, null);

        //    panelMain.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        //    panelMain.ColumnCount = 4;
        //    panelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));
        // //   panelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));
        //    panelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
        //  //  panelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
        //    panelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        //    panelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));
        ////    panelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));
        //    for (int i = 0; i < runUUT.Led.Count; i++)
        //    {
        //        if (runUUT.Led[i].canScanLocal == 1)
        //        {
        //            slotNum += 1;
        //            localPath.Add(runUUT.Led[i].localPath);
        //            localBar.Add(runUUT.Led[i].localBar);
        //            fixBar.Add(runUUT.Led[i].fixBar);
        //            serialNos.Add(runUUT.Led[i].serialNo);
        //            barType.Add(runUUT.Led[i].barType);
        //            result.Add(runUUT.Led[i].tranResult);
        //            OracleStr1.Add("");
        //            OracleStr2.Add("");
        //        }
        //    }
        //    panelMain.RowCount = slotNum + 1;
        //    for (int i = 0; i < slotNum / 2; i++)
        //        panelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
        //    panelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        //    Label lab = new Label();
        //    lab.Name = "lblId";
        //    lab.Dock = DockStyle.Fill;
        //    lab.TextAlign = ContentAlignment.MiddleCenter;
        //    lab.Text = CLanguage.Lan("序号");
        //    lblId.Add(lab);
        //    panelMain.Controls.Add(lblId[0], 0, 0);

        //    //Label lab1 = new Label();
        //    //lab1.Name = "lblLocal";
        //    //lab1.Dock = DockStyle.Fill;
        //    //lab1.TextAlign = ContentAlignment.MiddleCenter;
        //    //lab1.Text = CLanguage.Lan("位置");
        //    //lblLocalPath.Add(lab1);
        //    //panelMain.Controls.Add(lblLocalPath[0], 1, 0);

        //    Label lab2 = new Label();
        //    lab2.Name = "lblLocalBar";
        //    lab2.Dock = DockStyle.Fill;
        //    lab2.TextAlign = ContentAlignment.MiddleCenter;
        //    lab2.Text = CLanguage.Lan("位置条码");
        //    lblLocalBar.Add(lab2);
        //    panelMain.Controls.Add(lblLocalBar[0], 1, 0);

        //    //Label lab3 = new Label();
        //    //lab3.Name = "lblfixBar";
        //    //lab3.Dock = DockStyle.Fill;
        //    //lab3.TextAlign = ContentAlignment.MiddleCenter;
        //    //lab3.Text = CLanguage.Lan("治具条码");
        //    //lblFixBar.Add(lab3);
        //    //panelMain.Controls.Add(lblFixBar[0], 3, 0);

        //    Label lab4 = new Label();
        //    lab4.Name = "lblSNBar";
        //    lab4.Dock = DockStyle.Fill;
        //    lab4.TextAlign = ContentAlignment.MiddleCenter;
        //    lab4.Text = CLanguage.Lan("产品条码");
        //    lblSn.Add(lab4);
        //    panelMain.Controls.Add(lblSn[0], 2, 0);

        //    Label lab5 = new Label();
        //    lab5.Name = "lblResult";
        //    lab5.Dock = DockStyle.Fill;
        //    lab5.TextAlign = ContentAlignment.MiddleCenter;
        //    lab5.Text = CLanguage.Lan("结果");
        //    lblResult.Add(lab5);
        //    panelMain.Controls.Add(lblResult[0], 3, 0);

        //    //Label lblClear = new Label();
        //    //lblClear.Name = "btnClear";
        //    //lblClear.Dock = DockStyle.Fill;
        //    //lblClear.TextAlign = ContentAlignment.MiddleCenter;
        //    //lblClear.Text = CLanguage.Lan("清除");
        //    //panelMain.Controls.Add(lblClear, 6, 0);

        //    for (int i = 0; i < slotNum; i++)
        //    {
        //        Label lblRstId = new Label();       //序号
        //        lblRstId.Name = "lblId" + i.ToString();
        //        lblRstId.Dock = DockStyle.Fill;
        //        lblRstId.TextAlign = ContentAlignment.MiddleCenter;
        //        lblRstId.Text = (i + 1).ToString("D3");
        //        lblId.Add(lblRstId);
        //        panelMain.Controls.Add(lblId[i + 1], 0, i + 1);

        //        //Label lblLoacl = new Label();       //位置
        //        //lblLoacl.Name = "lblLoacl" + i.ToString();
        //        //lblLoacl.Dock = DockStyle.Fill;
        //        //lblLoacl.TextAlign = ContentAlignment.MiddleCenter;
        //        //lblLoacl.Text = localPath[i];
        //        //lblLocalPath.Add(lblLoacl);
        //        //panelMain.Controls.Add(lblLocalPath[i + 1], 1, i + 1);

        //        Label lblLoaclNo = new Label();     //位置条码
        //        lblLoaclNo.Name = "lblLoaclNo" + i.ToString();
        //        lblLoaclNo.Dock = DockStyle.Fill;
        //        lblLoaclNo.TextAlign = ContentAlignment.MiddleCenter;
        //        lblLoaclNo.Text = localBar[i];
        //        lblLocalBar.Add(lblLoaclNo);
        //        panelMain.Controls.Add(lblLocalBar[i + 1], 1, i + 1);

        //        //Label lblFixlNo = new Label();      //治具条码
        //        //lblFixlNo.Name = "labLoaclNo" + i.ToString();
        //        //lblFixlNo.Dock = DockStyle.Fill;
        //        //lblFixlNo.TextAlign = ContentAlignment.MiddleCenter;
        //        //lblFixBar.Add(lblFixlNo);
        //        //lblFixlNo.Text = fixBar[i];
        //        //panelMain.Controls.Add(lblFixBar[i + 1], 3, i + 1);

        //        //Button btnClear = new Button();
        //        //btnClear.Name = "btnClear";
        //        //btnClear.Dock = DockStyle.Fill;
        //        //btnClear.TextAlign = ContentAlignment.MiddleCenter;
        //        //btnClear.Text = CLanguage.Lan("清除");
        //        //btnClear.Tag = i;
        //        //btnClear.Click += new EventHandler(btnClearBar_Click);
        //        //btnClearBar.Add(btnClear);
        //        //panelMain.Controls.Add(btnClearBar[i], 6, i + 1);

        //        Label labSn = new Label();                  //产品条码
        //        labSn.Name = "labLoacl" + i.ToString();
        //        labSn.Dock = DockStyle.Fill;
        //        labSn.TextAlign = ContentAlignment.MiddleCenter;
        //        lblSn.Add(labSn);

        //        Label labResult = new Label();              //产品结果
        //        labResult.Name = "labResult" + i.ToString();
        //        labResult.Dock = DockStyle.Fill;
        //        labResult.TextAlign = ContentAlignment.MiddleCenter;
        //        lblResult.Add(labResult);

        //        if (barType[i] == 1)
        //        {                    
        //            lblLoaclNo.ForeColor = Color.Blue;
        //            labSn.Text = serialNos[i];
        //            //lblFixBar[i + 1].ForeColor = Color.Blue;
        //            lblSn[i+1].ForeColor = Color.Blue;
        //            lblResult[i + 1].ForeColor = Color.Blue;
        //           // btnClearBar[i].Enabled = true ;
        //            if (result[i] == 1)
        //                labResult.Text = "Tran OK";
        //            else
        //                labResult.Text = "Relay Tran";
        //        }
        //        else
        //        {
        //          //  btnClearBar[i].Enabled = false;
        //            lblLoaclNo.ForeColor = Color.Green;
        //            labSn.Text = "";
        //            labResult.Text = "";
        //        }
        //        panelMain.Controls.Add(lblSn[i + 1], 2, i + 1);
        //        panelMain.Controls.Add(lblResult[i + 1], 3, i + 1);
        //    }

        //    panel1.Controls.Add(panelMain, 0, 1);

        //    int barnum = 0;
        //    int trannum = 0;
        //    for (int i = 0; i < slotNum; i++)
        //    {
        //        if (barType[i] == 1)
        //        {
        //            barnum += 1;
        //        }
        //        if (result[i] == 1)
        //        {
        //            trannum += 1;
        //        }
        //    }
        //    lbltranresult.Text = trannum.ToString();
        //    lblbarcodeNum.Text = barnum.ToString();
        //}
        /// <summary>
        /// 初始化面板
        /// </summary>
        private void loadMainForm()
        {

            labTitle.Text = CLanguage.Lan("扫描[") + runUUT.Para.TimerName + CLanguage.Lan("]产品条码");
            labName1.Text = runUUT.Para.TimerName+"正面";
            labName2.Text = runUUT.Para.TimerName + "反面";
            panel1.SuspendLayout();
            panelMain = new TableLayoutPanel();
            panelMain.BackColor = Color.Transparent;
            panelMain.Dock = DockStyle.Fill;
            panelMain.AutoScroll = true;
            panelMain.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panelMain, true, null);

            panelMain.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            panelMain.ColumnCount = CGlobalPara.C_Layer_UUT + 1;
            panelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));

            panelMain.SuspendLayout();
            for (int i = 0; i < panelMain.ColumnCount; i++)
            {
                panelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3));
            }

            for (int i = 0; i < runUUT.Led.Count; i++)
            {
                if (runUUT.Led[i].canScanLocal == 1)
                {
                    slotNum += 1;
                    localPath.Add(runUUT.Led[i].localPath);
                    localBar.Add(runUUT.Led[i].localBar);
                    fixBar.Add(runUUT.Led[i].fixBar);
                    serialNos.Add(runUUT.Led[i].serialNo);
                    barType.Add(runUUT.Led[i].barType);
                    result.Add(runUUT.Led[i].tranResult);
                }
            }
            panelMain.RowCount = CGlobalPara.C_Timer_Lay * 2;

            for (int i = 0; i < CGlobalPara.C_Timer_Lay; i++)
            {
                Label lblPathNo = new Label();     //序号
                lblPathNo.Name = "lblLoaclNo";
                lblPathNo.Dock = DockStyle.Fill;
                lblPathNo.TextAlign = ContentAlignment.MiddleCenter;
                lblPathNo.Font = new Font("微软雅黑", 9);
                lblPathNo.Text = "序号";

                panelMain.Controls.Add(lblPathNo, 0, i * 2);
                Label labSn = new Label();                  //产品条码
                labSn.Name = "labLoacl" + i.ToString();
                labSn.Dock = DockStyle.Fill;
                labSn.TextAlign = ContentAlignment.MiddleCenter;
                labSn.Font = new Font("微软雅黑", 9);
                labSn.Text = "产品条码";

                panelMain.Controls.Add(labSn, 0, i * 2 + 1);

            }
            for (int i = 0; i < panelMain.RowCount; i++)
            {
                panelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
                panelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
                i++;
            }
            int iRow = 0;
            int iCol = 0;
            for (int i = 0; i < slotNum ; i++)
            {
                iRow = ((i / 12) * 2) % 16;
                iCol = i % 12 + 1 + (i / 96) * 12;
                Label lblPathNo = new Label();     //位置条码
                lblPathNo.Name = "lblLoaclNo" + i.ToString();
                lblPathNo.Dock = DockStyle.Fill;
                lblPathNo.TextAlign = ContentAlignment.MiddleCenter;
                lblPathNo.Font = new Font("微软雅黑", 7);
                lblPathNo.Text = (i + 1).ToString("D3");
                lblLocalPath.Add(lblPathNo);
                System.Threading.Thread.Sleep(2);
                //panelMain.Controls.Add(lblLocalPath[i], i % CGlobalPara.C_Layer_UUT + 1, (i / CGlobalPara.C_Layer_UUT) * 2);
                panelMain.Controls.Add(lblLocalPath[i], iCol, iRow);

                Label labSn = new Label();                  //产品条码
                labSn.Name = "labLoacl" + i.ToString();
                labSn.Dock = DockStyle.Fill;
                labSn.TextAlign = ContentAlignment.MiddleCenter;
                labSn.DoubleClick += new System.EventHandler(this.lab_DoubleClick);
                labSn.Tag = i;
                lblSn.Add(labSn);
                System.Threading.Thread.Sleep(2);
                //panelMain.Controls.Add(lblSn[i], i % CGlobalPara.C_Layer_UUT + 1, (i / CGlobalPara.C_Layer_UUT) * 2 + 1);
               panelMain.Controls.Add(lblSn[i], iCol, iRow + 1);
            }

            panel1.Controls.Add(panelMain, 0, 2);


            int barnum = 0;

            for (int i = 0; i < slotNum; i++)
            {
                if (barType[i] == 1)
                {
                    barnum += 1;
                    lblSn[i].Text = runUUT.Led[i].serialNo;
                    lblSn[i].BackColor = Color.Green;
                }
            }
            barNo = barnum;
            lblbarcodeNum.Text = barnum.ToString();
            panelMain.ResumeLayout();
            panel1.ResumeLayout();
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
            panel4.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel4, true, null);
            panel5.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel5, true, null);
            panel6.GetType().GetProperty("DoubleBuffered",
                                      System.Reflection.BindingFlags.Instance |
                                      System.Reflection.BindingFlags.NonPublic)
                                      .SetValue(panel6, true, null);
        }
        #endregion

        #region 面板回调函数
        /// <summary>
        /// 扫描条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (runUUT.Para.MesFlag == 1 && CGlobalPara.SysPara.Mes.Connect)
                {
                    if (txtTrolleyBarcode.Text.Trim().Length <3)
                    {
                        SpeechText("扫描异常");
                        labStatus.Text = CLanguage.Lan("扫描前请扫描小车条码...");
                        labStatus.ForeColor = Color.Red;
                        txtTrolleyBarcode.SelectAll();
                        txtTrolleyBarcode.Focus();
                        e.Handled = true;
                        return;
                    }
                }

                string serialNo = txtSn.Text.Trim();
                if (serialNo.Length == 0)
                {
                    labStatus.Text = CLanguage.Lan("扫描条码>") + serialNo + CLanguage.Lan("<未发现正确条码,请确认...");
                    labStatus.ForeColor = Color.Red;
                    txtSn.SelectAll();
                    txtSn.Focus();
                    e.Handled = true;
                    return;
                }

                if (CGlobalPara.SysPara.Para.chkScanPathSn)
                {
                    for (int i = 0; i < slotNum; i++)           //扫空位置的条码重置扫描状态
                    {
                        if (txtTrolleyBarcode.Text.Trim().Contains (serialNo.Substring(0,6)) && serialNo.Length ==10)
                        {
                            if (i + 1 == Convert.ToInt16(serialNo.Substring(6, 4)))
                            {
                                if (barType[i] != 1)
                                {
                                    ScanBarItem = 0;
                                    barNo = i;
                                    break;
                                }
                                else
                                {
                                    labStatus.Text = CLanguage.Lan("扫描条码>") + serialNo + CLanguage.Lan("<位置条码已使用,请确认...");
                                    labStatus.ForeColor = Color.Red;
                                    txtSn.SelectAll();
                                    txtSn.Focus();
                                    e.Handled = true;
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    ScanBarItem = 2;
                }
                switch (ScanBarItem)
                {
                    case 0:
                        if (ScanLocalBar(serialNo))
                        {
                            //if (runUUT.Para.FixBar == 1)            //治具条码状态
                            //    ScanBarItem = 1;
                            //else
                            ScanBarItem = 2;
                        }

                        break;
                    case 1:
                        if (ScanFixBar(serialNo))
                            ScanBarItem = 2;
                        break;
                    case 2:
                        if (ScanUUTBar(serialNo))
                            ScanBarItem = 0;
                        break;
                    default:
                        ScanBarItem = 0;
                        labStatus.Text = CLanguage.Lan("条码状态异常,请重新扫描位置条码...");
                        labStatus.ForeColor = Color.Red;
                        txtSn.Focus();
                        txtSn.SelectAll();

                        break;
                }

                int barnum = 0;

                for (int i = 0; i < slotNum; i++)
                {
                    if (barType[i] == 1)
                    {
                        barnum += 1;
                    }
                }
                lblbarcodeNum.Text = barnum.ToString();

            }
        }

        /// <summary>
        /// 检查位置条码
        /// </summary>
        /// <param name="serialNo"></param>
        /// <returns></returns>
        private bool ScanLocalBar(string serialNo)
        {
            try
            {
                bool haveLocal = false;
                //for (int i = 0; i < slotNum; i++)
                //{
                //    if (localBar[i] == serialNo)            //是否匹配位置条码
                //    {
                //        int ScanCount = int.Parse(txtScanSnCount.Text);
                //        i = i / ScanCount;
                //        i = i * ScanCount;
                //        if (barType[i] != 1)                //是否使用
                //        {
                //            bool haveuut = false;
                //            for (int j = 0; j < runUUT.Para.OutPutChan; j++)            //判断是否有产品
                //            {
                //                int islot = i * runUUT.Para.OutPutChan ;
                //                if (runUUT.Led[islot + j].result != 0)
                //                   haveuut = true;
                //            }
                //            if (!haveuut)
                //            {
                //            ////    labStatus.Text = CLanguage.Lan("位置条码>") + txtSn.Text.Trim() + CLanguage.Lan("<扫描错误,该位置无产品...");
                //            ////    labStatus.ForeColor = Color.Red;
                //            ////    txtSn.Focus();
                //            ////    txtSn.SelectAll();
                //            ////    return false;
                //            }
                haveLocal = true;
                if (!localBar.Contains(serialNo))
                {

                    localBar[barNo] = serialNo;
                    if (runUUT.Para.FixBar == 1)
                        labStatus.Text = CLanguage.Lan("位置条码>") + txtSn.Text.Trim() + CLanguage.Lan("<扫描完成,请扫描治具条码...");
                    else
                        labStatus.Text = CLanguage.Lan("位置条码>") + txtSn.Text.Trim() + CLanguage.Lan("<扫描完成,请扫描产品条码...");
                    labStatus.ForeColor = Color.Blue;
                    //lblLocalBar[i + 1].ForeColor = Color.Blue;
                    lblLocalPath[barNo].ForeColor = Color.Blue;
                    txtSn.Focus();
                    txtSn.SelectAll();
                    menuClick.OnEvented(new CSetMenuArgs(idNo, barNo, serialNo, ESetMenu.位置条码OK));
                    //break;
                }
                else
                {
                    labStatus.Text = CLanguage.Lan("位置条码>") + txtSn.Text.Trim() + CLanguage.Lan("<扫描错误,该位置已经使用...");
                    labStatus.ForeColor = Color.Red;
                    txtSn.Focus();
                    txtSn.SelectAll();
                    return false;
                }
                //    }
                //}
                if (haveLocal == false)
                {
                    labStatus.Text = CLanguage.Lan("位置条码>") + txtSn.Text.Trim() + CLanguage.Lan("<扫描错误,请重新扫描位置条码...");
                    labStatus.ForeColor = Color.Red;
                    txtSn.Focus();
                    txtSn.SelectAll();
                    return false;
                }
                return true;
            }
            catch
            {
                labStatus.Text = CLanguage.Lan("位置条码>") + txtSn.Text.Trim() + CLanguage.Lan("<扫描异常,请重新扫描位置条码...");
                labStatus.ForeColor = Color.Red;
                txtSn.Focus();
                txtSn.SelectAll();
                return false;
            }
        }

        /// <summary>
        /// 检查治具条码
        /// </summary>
        /// <param name="serialNo"></param>
        /// <returns></returns>
        private bool ScanFixBar(string serialNo)
        {
            try
            {
            //    for (int i = 0; i < slotNum; i++)
            //    {
            //        if (fixBar[i] == serialNo)          //判断治具条码是否使用
            //        {
            //            if (barType[i] == 1)
            //            {
            //                labStatus.Text = CLanguage.Lan("治具条码>") + txtSn.Text.Trim() + CLanguage.Lan("<扫描错误,该条码已经使用,请重新扫描...");
            //                labStatus.ForeColor = Color.Red;
            //                txtSn.Focus();
            //                txtSn.SelectAll();
            //                return false;
            //            }
            //        }
            //        if (localBar[i] == serialNo)        //判断治具条码是否扫描错误
            //        {
            //            labStatus.Text = CLanguage.Lan("治具条码>") + txtSn.Text.Trim() + CLanguage.Lan("<扫描错误,该条码是位置条码,请重新扫描...");
            //            labStatus.ForeColor = Color.Red;
            //            txtSn.Focus();
            //            txtSn.SelectAll();
            //            return false;
            //        }
            //    }
            //    labStatus.Text = CLanguage.Lan("治具条码>") + txtSn.Text.Trim() + CLanguage.Lan("<扫描完成,请扫描产品条码...");
            //    labStatus.ForeColor = Color.Blue;
            //    fixBar[barNo] = serialNo;
            //    lblFixBar[barNo + 1].Text = fixBar[barNo];
            //    lblFixBar[barNo + 1].ForeColor = Color.Blue;
            //    txtSn.Focus();
            //    txtSn.SelectAll();
            //    menuClick.OnEvented(new CSetMenuArgs(idNo, barNo, serialNo, ESetMenu.治具条码OK));

                return true;
            }
            catch
            {
                labStatus.Text = CLanguage.Lan("治具条码>") + txtSn.Text.Trim() + CLanguage.Lan("<扫描异常,请重新扫描位置条码...");
                labStatus.ForeColor = Color.Red;
                txtSn.Focus();
                txtSn.SelectAll();
                return false;
            }
        }

        /// <summary>
        /// 检查产品条码
        /// </summary>
        /// <param name="serialNo"></param>
        /// <returns></returns>
        private bool ScanUUTBar(string serialNo)
        {
            try
            {
                if (serialNo.Length != runUUT.Para.barLength)
                {
                    SpeechText("扫描失败");
                    labStatus.Text = CLanguage.Lan("扫描产品条码>") + serialNo + CLanguage.Lan("<长度[") +
                                     serialNo.Length.ToString() + CLanguage.Lan("]与配置长度:[") + runUUT.Para.barLength +
                                     CLanguage.Lan("]不一致,请重新扫描产品条码!");
                    labStatus.ForeColor = Color.Red;
                    // lblLocalBar[barNo + 1].ForeColor = Color.Green;
                    txtSn.SelectAll();
                    txtSn.Focus();
                    return false;
                }

                for (int i = 0; i < slotNum; i++)
                {
                    if (serialNos[i] == serialNo)
                    {
                        if (barType[i] == 1)
                        {
                            SpeechText("扫描失败");
                            labStatus.Text = CLanguage.Lan("产品条码>") + serialNo + CLanguage.Lan("<扫描错误,该条码已经使用,请重新扫描...");
                            labStatus.ForeColor = Color.Red;
                            txtSn.Focus();
                            txtSn.SelectAll();
                            return false;
                        }
                    }
                }

                int ScanCount = int.Parse(txtScanSnCount.Text);
                if (ScanCount == 1 && ChanNum >0)
                    ScanCount = ChanNum;

                for (int i = 0; i < ScanCount; i++)
                {
                    serialNos[barNo] = serialNo;
                    lblSn[barNo].Text = serialNos[barNo];
                    lblSn[barNo].BackColor = Color.Green;
                    barType[barNo] = 1;
                    menuClick.OnEvented(new CSetMenuArgs(idNo, barNo, serialNo, ESetMenu.产品条码OK));
                   
                     barNo++;
                    
                    if (barNo >= barType.Count)
                        break;
                }
                SpeechText("成功");
              //  txtSn.SelectAll();
             
                int nextSlot = -1;
                for (int i = barNo; i < barType.Count; i++)
                {
                    if (barType[i] != 1)
                    {
                        nextSlot = i;
                        break;
                    }
                }

                if (nextSlot == -1)
                {
                    labStatus.Text = CLanguage.Lan("扫描完毕,请退出.");
                    labStatus.ForeColor = Color.Blue;
                }
                else
                {
                    txtSn.Text = "";
                    txtSn.SelectAll();
                    txtSn.Focus();
                    labStatus.Text = CLanguage.Lan("请扫描下一位置...");
                    labStatus.ForeColor = Color.Blue;                    
                }

                return true;
            }
            catch
            {
                labStatus.Text = CLanguage.Lan("产品条码>") + txtSn.Text.Trim() + CLanguage.Lan("<扫描异常,请重新扫描位置条码...");
                labStatus.ForeColor = Color.Red;
                txtSn.Focus();
                txtSn.SelectAll();
                return false;
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (runUUT.Para.MesFlag == 1 && CGlobalPara.SysPara.Mes.Connect)
            {
                if (txtTrolleyBarcode.Text.Trim().Length < 3 && Convert.ToInt16(lblbarcodeNum.Text) > 0)
                {

                    labStatus.Text = CLanguage.Lan("扫描前请扫描小车条码...");
                    labStatus.ForeColor = Color.Red;
                    return;
                }
            }

            menuClick.OnEvented(new CSetMenuArgs(idNo, 0, txtTrolleyBarcode.Text , ESetMenu.扫码完成));

            labStatus.Text = CLanguage.Lan("台车>") + txtTrolleyBarcode.Text.Trim() + CLanguage.Lan("<扫码完成...");
            labStatus.ForeColor = Color.Blue;

            this.Close();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            //if (speech != null)
            //{
            //    speech.SpeakAsyncCancelAll();
            //    speech.Dispose();
            //    speech = null;
            //}
            if (runUUT.Para.MesFlag == 1 && CGlobalPara.SysPara.Mes.Connect)
            {
                if (txtTrolleyBarcode.Text.Trim().Length < 3 && Convert.ToInt16(lblbarcodeNum.Text) > 0)
                {

                    labStatus.Text = CLanguage.Lan("扫描前请扫描小车条码...");
                    labStatus.ForeColor = Color.Red;
                    return;
                }
            }
            menuClick.OnEvented(new CSetMenuArgs(idNo, 0, txtTrolleyBarcode.Text, ESetMenu.扫码完成));
            this.Close();
        }

        /// <summary>
        /// 清除所有条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClrSn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(CLanguage.Lan("确定要删除所有条码?"), CLanguage.Lan("删除条码"), MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                barNo = 0;
                for (int i = 0; i < slotNum; i++)
                {
                    if (barType[i] == 1 || serialNos[i] != "")
                    {
                        localBar[i] = "";
                        serialNos[i] = "";
                        menuClick.OnEvented(new CSetMenuArgs(idNo, i, "", ESetMenu.位置空闲));

                        lblSn[i].Text = "";
                        lblSn[i].BackColor = Color.Transparent;
                        barType[i] = 0;
                    }

                }
                int barnum = 0;
                for (int i = 0; i < slotNum; i++)
                {
                    if (barType[i] == 1)
                    {
                        barnum += 1;
                    }
                }
                lblbarcodeNum.Text = barnum.ToString();

                txtSn.SelectAll();
                txtSn.Focus();
            }
        }

        /// <summary>
        /// 清除单个条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearBar_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            int SelNo = (int)b.Tag;
            int ScanCount = int.Parse(txtScanSnCount.Text);
            SelNo = SelNo / ScanCount;
            SelNo = SelNo * ScanCount;
            if (MessageBox.Show(CLanguage.Lan("确定要清除该位置的条码?"), CLanguage.Lan("清除条码"), MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {

                for (int i = 0; i < ScanCount; i++)
                {
                    SelNo = SelNo + i;
                    serialNos[SelNo] = "";
                    fixBar[SelNo] = "";
                    barType[SelNo] = 0;
                    //  lblFixBar[SelNo + 1].Text = "";
                    lblSn[SelNo].Text = "";

                    lblLocalBar[SelNo].ForeColor = Color.Green;
                    barType[SelNo] = 0;
                    localBar[SelNo] = "";
                    lblLocalPath[SelNo].ForeColor = Color.Black ;
                    //  btnClearBar[SelNo].Enabled = false;
                    menuClick.OnEvented(new CSetMenuArgs(idNo, SelNo, "", ESetMenu.位置空闲));
                }
            }
            int barnum = 0;
            for (int i = 0; i < slotNum; i++)
            {
                if (barType[i] == 1)
                {
                    barnum += 1;
                }
            }
            lblbarcodeNum.Text = barnum.ToString();

            txtSn.SelectAll();
            txtSn.Focus();
        }
        /// <summary>
        /// 老化时间到上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void udcSerialNo_Load(object sender, EventArgs e)
        {
        
        }

        #endregion

        #region 上传结果
        private void SpeechText(string txtVoice)
        {
            try
            {


                if (CGlobalPara.SysPara.Para.chkSpeeck)
                {
                   // speech.Speak(txtVoice);

                }

            }
            catch (Exception ex)
            {
                labStatus.Text = ex.ToString();

            }

        }

        #endregion

        #region 面板消息
        public class CSetMenuArgs : EventArgs
        {
            public readonly int idNo;
            public readonly int barNo;
            public readonly string scanbar;
            public readonly ESetMenu menuInfo;
            public CSetMenuArgs(int idNo, int barNo, string scanbar, ESetMenu menuInfo)
            {
                this.idNo = idNo;
                this.barNo = barNo;
                this.scanbar = scanbar;
                this.menuInfo = menuInfo;
            }
        }
        public COnEvent<CSetMenuArgs> menuClick = new COnEvent<CSetMenuArgs>();
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
                    this.Text = CLanguage.Lan("人工扫描产品条码");
                    BtnCancel.Text = CLanguage.Lan(" 退出");
                    btnClrSn.Text = CLanguage.Lan("清除条码");
                    btntranSn.Text = CLanguage.Lan("上传条码");
                    label1.Text = CLanguage.Lan("扫描条码状态:");
                    label2.Text = CLanguage.Lan("扫描状态:");
                    label3.Text = CLanguage.Lan("条码数量:");
                 
                    labStatus.Text = CLanguage.Lan("等待扫描产品条码..");
                    labTitle.Text = CLanguage.Lan("扫描[A1]产品条码");
                    break;
                case CLanguage.EL.英语:
                    break;
                case CLanguage.EL.繁体:
                    this.Text = CLanguage.Lan("人工扫描产品条码");
                    BtnCancel.Text = CLanguage.Lan(" 退出");
                    btnClrSn.Text = CLanguage.Lan("清除条码");
                    btntranSn.Text = CLanguage.Lan("上传条码");
                    label1.Text = CLanguage.Lan("扫描条码状态:");
                    label2.Text = CLanguage.Lan("扫描状态:");
                    label3.Text = CLanguage.Lan("条码数量:");
                   
                    labStatus.Text = CLanguage.Lan("等待扫描产品条码..");
                    labTitle.Text = CLanguage.Lan("扫描[A1]产品条码");
                    break;
                default:
                    break;
            }
        }
        #endregion

        private void lab_DoubleClick(object sender, EventArgs e)
        {
            Label labsn = (Label)sender;
            int SelNo = (int)labsn.Tag ;
            int ScanCount = int.Parse(txtScanSnCount.Text);
            SelNo = SelNo / ScanCount;
            SelNo = SelNo * ScanCount;
            if (MessageBox.Show(CLanguage.Lan("确定要清除该位置的条码?"), CLanguage.Lan("清除条码"), MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = 0; i < ScanCount; i++)
                {
                    SelNo = SelNo + i;
                    serialNos[SelNo] = "";
                    fixBar[SelNo] = "";
                    //  lblFixBar[SelNo + 1].Text = "";
                    lblSn[SelNo].Text = "";
                    lblSn[SelNo].BackColor = Color.Transparent;

                    barType[SelNo] = 0;
                    localBar[SelNo] = "";
                    lblLocalPath[SelNo].ForeColor = Color.Black;
                    //  btnClearBar[SelNo].Enabled = false;
                    menuClick.OnEvented(new CSetMenuArgs(idNo, SelNo, "", ESetMenu.位置空闲));
                }
            }
            int barnum = 0;
            for (int i = 0; i < slotNum; i++)
            {
                if (barType[i] == 1)
                {
                    barnum += 1;
                }
            }
            lblbarcodeNum.Text = barnum.ToString();

            txtSn.SelectAll();
            txtSn.Focus();

        }

      

    }
}
