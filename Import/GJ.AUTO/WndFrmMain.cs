using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using GJ.COM;
using GJ.APP;
using GJ.PLUGINS;
using GJ.PDB;
namespace GJ.AUTO
{
    public partial class WndFrmMain : Form,IMainMsg
    {
        #region 插件方法
        /// <summary>
        /// 子窗口
        /// </summary>
        private Form _childForm = null;
        /// <summary>
        /// 指示运行状态 
        /// </summary>
        /// <param name="status"></param>
        public void OnShowStatus(EIndicator status)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action<EIndicator>(OnShowStatus), status);
            else
            {
                tlLabStatus.Text = status.ToString();
                if (status == EIndicator.Auto)
                {
                    tlLabStatus.ForeColor = Color.Blue;
                    tlBtnStart.Enabled = false;
                }
                else
                {
                    tlLabStatus.ForeColor = Color.Red;
                    tlBtnStart.Enabled = true;
                }
            }
        }
        /// <summary>
        /// 显示软件版本
        /// </summary>
        /// <param name="verName"></param>
        /// <param name="verDate"></param>
        public void OnShowVersion(string verName, string verDate)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action<string, string>(OnShowVersion), verName, verDate);
            else 
            {
                _verName = verName;

                _verDate = verDate;

                tlLabVer.Text = verName;

                switch (CLanguage.languageType)
                {
                    case CLanguage.EL.中文:
                        this.Text = "自动测试监控系统--东莞市冠佳电子设备有限公司 更新日期:[" + verDate + "]";                        
                        break;
                    case CLanguage.EL.英语:
                        this.Text = "Automatic test system--Dongguan Guan Jia electronic equipment Co.,Ltd Modify Date:[" + verDate + "]";
                        break;
                    case CLanguage.EL.繁体 :
                        this.Text =CLanguage.Lan ( "自动测试监控系统--东莞市冠佳电子设备有限公司 更新日期:[") + verDate + "]";

                        break;
                    default:
                        break;
                }            
            }
        }
        /// <summary>
        /// 消息触发
        /// </summary>
        /// <param name="para"></param>
        public void OnMessage(string name, int lPara, int wPara)
        {
            
        }
        /// <summary>
        /// 退出系统
        /// </summary>
        public void OnExitSystem()
        {
           
        }
        #endregion

        #region 加载用户配置参数
        /// <summary>
        /// 加载应用程序配置
        /// </summary>
        private void LoadAppSetting()
        {
            try
            {
                CUserApp.LoadAppSetting();  
            }
            catch (Exception)
            {                
                throw;
            }            
        }
        /// <summary>
        /// 加载中英文字典
        /// </summary>
        private void LoadLanguge()
        { 
            try 
	        {
                CLanguage.LoadLanType();

                string lanDB = Application.StartupPath + "\\LAN.accdb";

                if (!File.Exists(lanDB))
                    return;

                CDBCOM db = new CDBCOM(EDBType.Access, ".", lanDB);

                string er = string.Empty;

                DataSet ds = null;

                string sqlCmd="select * from LanList order by idNo";

                if (!db.QuerySQL(sqlCmd, out ds, out er))
                    return;

                Dictionary<string, string> lan = new Dictionary<string, string>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string LAN_CH = ds.Tables[0].Rows[i]["LAN_CH"].ToString();
                    string LAN_EN = string.Empty ;
                    if (CLanguage.languageType == CLanguage.EL.繁体)
                        LAN_EN = CLanguage.Lan(ds.Tables[0].Rows[i]["LAN_CH"].ToString());
                    else
                        LAN_EN = ds.Tables[0].Rows[i]["LAN_EN"].ToString();

                    if (!lan.ContainsKey(LAN_CH))
                        lan.Add(LAN_CH, LAN_EN);
                }

                CLanguage.Load(lan, out er);

	        }
	        catch (Exception)
	        {		
		        throw;
	        }            
        }
        /// <summary>
        /// 加载中英文界面
        /// </summary>
        private void SetLanguage()
        {

            switch (CLanguage.languageType)
            {
                case CLanguage.EL.中文:
                    menuEnglish.Checked = false;
                    menuTaiWan.Checked = false;                    
                    menuChinese.Checked = true;
                    tlLabVer.Text = _verName;
                    this.Text = "自动测试监控系统--东莞市冠佳电子设备有限公司 更新日期:[" + _verDate  + "]";                    
                    break;
                case CLanguage.EL.英语:
                    menuEnglish.Checked = true;
                    menuTaiWan.Checked = false;  
                    menuChinese.Checked = false;
                    this.Text = "Automatic test system--Dongguan Guan Jia electronic equipment Co.,Ltd Modify Date:[" + _verDate + "]";
                    tlLabVer.Text = _verName;
                    break;
                case CLanguage.EL.繁体 :
                    menuEnglish.Checked = false;
                    menuTaiWan.Checked = true;
                    menuChinese.Checked = false;
                    this.Text = CLanguage.Lan("自动测试监控系统--东莞市冠佳电子设备有限公司 更新日期:[") + _verDate + "]"; 
                    tlLabVer.Text = _verName;
                    menu1.Text = CLanguage.Lan("用户登录(&U)");
                    menu2.Text = CLanguage.Lan("参数设置(&P)");
                    menu3.Text = CLanguage.Lan("数据查询(&Q)");
                    menu4.Text = CLanguage.Lan("系统帮助(&H)");
                    menu5.Text = CLanguage.Lan("语言设置(&L)");
                    menuChinese.Text = CLanguage.Lan("中文(C)");
                    menuDebug.Text = CLanguage.Lan("调式工具(&D)");
                    menuEnglish.Text = CLanguage.Lan("英语(&E)");
                    menuExit.Text = CLanguage.Lan("退出系统(&E)");
                    menuHelp.Text = CLanguage.Lan("操作说明(&O)");
                    menuLoad.Text = CLanguage.Lan("加载工位(&S)");
                    menuLogIn.Text = CLanguage.Lan("切换用户(&L)");
                    menuModel.Text = CLanguage.Lan("机种参数(&M)");
                    menuRunLog.Text = CLanguage.Lan("运行日志(&L)");
                    menuSample.Text = CLanguage.Lan("治具点检(&S)");
                    menuSkin.Text = CLanguage.Lan("更换皮肤(&S)");
                    menuSnQuery.Text = CLanguage.Lan("条码查询(&U)");
                    menuSysPara.Text = CLanguage.Lan("系统参数(&S)");
                    menuTaiWan.Text = CLanguage.Lan("繁体(T)");
                    menuTestData.Text = CLanguage.Lan("测试数据(&D)");
                    menuUsers.Text = CLanguage.Lan("用户权限(&U)");
                    menuVersion.Text = CLanguage.Lan("软件版本(&V)");
                    menuYieldQuery.Text = CLanguage.Lan("产能查询(Q)");
                    tlBtnExit.Text = CLanguage.Lan("退出系统");
                    tlBtnLogIn.Text = CLanguage.Lan("切换用户");
                    tlBtnStart.Text = CLanguage.Lan(" 启动 (&F9)");
                    tlBtnStop.Text = CLanguage.Lan(" 暂停(&F10)");
                    tlLabCurVer.Text = CLanguage.Lan("软件版本:");
                    toolStripLabel2.Text = CLanguage.Lan("运行状态:");
                    toolStripLabel3.Text = CLanguage.Lan("当前工位:");
                    toolUserLabel.Text = CLanguage.Lan("当前用户:");
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 切换界面语言
        /// </summary>
        /// <param name="language"></param>
        private void ChangeLanguage(CLanguage.EL language)
        {
            CLanguage.SetLanType(language);
            SetLanguage();
        }
        #endregion

        #region 构造函数
        public WndFrmMain()
        {
            InitializeComponent();

            LoadAppSetting();

            LoadLanguge();

            IntialControl();

            SetDoubleBuffered();

            SetLanguage();

            loadUserLogIn();

        }
        #endregion

        #region 初始化过程
        /// <summary>
        /// 初始化界面
        /// </summary>
        private void IntialControl()
        {

        }
         /// <summary>
        /// 设置双缓冲,防止界面闪烁
        /// </summary>
        private void SetDoubleBuffered()
        {
            panelMain.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(panelMain, true, null);
            panel1.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(panel1, true, null);
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        private void loadUserLogIn()
        {
            this.Visible = false; 
            WndFrmLogIn logInDlg = new WndFrmLogIn();
            logInDlg.OnLogInArgs.OnEvent += new COnEvent<CLogInArgs>.OnEventHandler(OnUserLogIn);
            if(logInDlg.ShowDialog() != DialogResult.OK)
            {
                System.Environment.Exit(0);
            }
            logInDlg.OnLogInArgs.OnEvent -= new COnEvent<CLogInArgs>.OnEventHandler(OnUserLogIn);
            logInDlg.Dispose();
            logInDlg = null;
            this.Visible = true;
        }
        /// <summary>
        /// 加载当前工位
        /// </summary>
        private void loadCurrentStation()
        {
            try
            {
                string er = string.Empty;
                if (!CGlobal.CFlow.load(ref er))
                {
                    WndSelectFlow dg = new WndSelectFlow();
                    if (dg.ShowDialog() != DialogResult.OK)
                        return;
                    CGlobal.CFlow.save();
                }           
            }
            catch (Exception)
            {
                
                throw;
            }          
        }
        /// <summary>
        /// 加载子窗口
        /// </summary>
        private void loadChildForm()
        {
            try
            {
                string er=string.Empty;

                string mainClass = this.Name.Replace("Wnd", "");

                string className = CGlobal.CFlow.NameSpace + "." + mainClass;

                if(!CReflect.LoadChildForm(className, out _childForm, out er))
                {
                    MessageBox.Show(er,CLanguage.Lan("加载工位"),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    return;
                }

                if(!CReflect.ShowChildForm(this,this.panelMain,CGlobal.CFlow.FlowGUID,_childForm,out er))
                {
                    MessageBox.Show(er, CLanguage.Lan("显示工位"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                object[] para=new object[]{CGlobal.User.mlogName, CGlobal.User.mlogName, CGlobal.User.mLevel};

                if (!CReflect.SendWndMethod(_childForm, EMessType.OnLogIn, out er, para))
                {
                    MessageBox.Show(er, CLanguage.Lan("登录工位信息"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                tlLabStation.Text = CLanguage.Lan(CGlobal.CFlow.FlowDes);
                
            }
            catch (Exception)
            {                
                throw;
            }
        }

        #endregion

        #region 字段
        private string _verName = "V1.0.0.0";
        private string _verDate = "2019/09/06";
        #endregion

        #region 面板控件
        
        #endregion

        #region 面板回调函数
        private void WndFrmMain_Load(object sender, EventArgs e)
        {
            loadCurrentStation();

            loadChildForm();

        }
        private void tlBtnStart_Click(object sender, EventArgs e)
        {
            if (_childForm != null)
            {
                string er = string.Empty;
                if (!CReflect.SendWndMethod(_childForm, EMessType.OnStartRun, out er, null))
                {
                    MessageBox.Show(er, CLanguage.Lan("消息机制"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
        }
        private void tlBtnStop_Click(object sender, EventArgs e)
        {
            if (_childForm != null)
            {
                string er = string.Empty;
                if (!CReflect.SendWndMethod(_childForm, EMessType.OnStopRun, out er, null))
                {
                    MessageBox.Show(er, CLanguage.Lan("消息机制"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
        }
        private void WndFrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            string er = string.Empty;

            if (tlBtnStart.Enabled == true && e.KeyValue == (int)Keys.F9)
            {                
                if (!CReflect.SendWndMethod(_childForm, EMessType.OnStartRun, out er, null))
                {
                    MessageBox.Show(er, CLanguage.Lan("消息机制"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                tlLabStatus.Text = EIndicator.Auto.ToString();
                tlLabStatus.ForeColor = Color.Blue;
                tlBtnStart.Enabled = false;   
            }
            else if (tlBtnStop.Enabled == true && e.KeyValue == (int)Keys.F10)
            {
                if (!CReflect.SendWndMethod(_childForm, EMessType.OnStopRun, out er, null))
                {
                    MessageBox.Show(er, CLanguage.Lan("消息机制"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else if (e.KeyValue == (int)Keys.F8)
            {
                object[] para = new object[] { "F8", 0 , 0 };

                if (!CReflect.SendWndMethod(_childForm, EMessType.OnMessage, out er, para))
                {
                    MessageBox.Show(er, CLanguage.Lan("消息机制"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else if (e.KeyValue == (int)Keys.F7)
            {
                object[] para = new object[] { "F7", 0, 0 };

                if (!CReflect.SendWndMethod(_childForm, EMessType.OnMessage, out er, para))
                {
                    MessageBox.Show(er, CLanguage.Lan("消息机制"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else if (e.KeyValue == (int)Keys.F6)
            {
                object[] para = new object[] { "F6", 0, 0 };

                if (!CReflect.SendWndMethod(_childForm, EMessType.OnMessage, out er, para))
                {
                    MessageBox.Show(er, CLanguage.Lan("消息机制"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else if (e.KeyValue == (int)Keys.F5)
            {
                object[] para = new object[] { "F5", 0, 0 };

                if (!CReflect.SendWndMethod(_childForm, EMessType.OnMessage, out er, para))
                {
                    MessageBox.Show(er, CLanguage.Lan("消息机制"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else if (e.KeyValue == (int)Keys.F4)
            {
                object[] para = new object[] { "F4", 0, 0 };

                if (!CReflect.SendWndMethod(_childForm, EMessType.OnMessage, out er, para))
                {
                    MessageBox.Show(er, CLanguage.Lan("消息机制"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else if (e.KeyValue == (int)Keys.F3)
            {
                object[] para = new object[] { "F3", 0, 0 };

                if (!CReflect.SendWndMethod(_childForm, EMessType.OnMessage, out er, para))
                {
                    MessageBox.Show(er, CLanguage.Lan("消息机制"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else if (e.KeyValue == (int)Keys.F2)
            {
                object[] para = new object[] { "F2", 0, 0 };

                if (!CReflect.SendWndMethod(_childForm, EMessType.OnMessage, out er, para))
                {
                    MessageBox.Show(er, CLanguage.Lan("消息机制"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else if (e.KeyValue == (int)Keys.F1)
            {
                object[] para = new object[] { "F1", 0, 0 };

                if (!CReflect.SendWndMethod(_childForm, EMessType.OnMessage, out er, para))
                {
                    MessageBox.Show(er, CLanguage.Lan("消息机制"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
        }
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

        #region 登录窗口
        private void menuLogIn_Click(object sender, EventArgs e)
        {
            WndFrmLogIn logInDlg = new WndFrmLogIn();
            logInDlg.OnLogInArgs.OnEvent += new COnEvent<CLogInArgs>.OnEventHandler(OnUserLogIn);
            logInDlg.ShowDialog(); 
            logInDlg.OnLogInArgs.OnEvent -= new COnEvent<CLogInArgs>.OnEventHandler(OnUserLogIn);
            logInDlg.Dispose();
            logInDlg = null;
        }
        private void tlBtnLogIn_Click(object sender, EventArgs e)
        {
            WndFrmLogIn logInDlg = new WndFrmLogIn();
            logInDlg.OnLogInArgs.OnEvent += new COnEvent<CLogInArgs>.OnEventHandler(OnUserLogIn);
            logInDlg.ShowDialog();
            logInDlg.OnLogInArgs.OnEvent -= new COnEvent<CLogInArgs>.OnEventHandler(OnUserLogIn);
            logInDlg.Dispose();
            logInDlg = null;
        }
        private void OnUserLogIn(object sender, CLogInArgs e)
        {
            CGlobal.User.mlogName = e.userName;
            CGlobal.User.mlogpassword = e.userPassword;
            for (int i = 0; i < e.pwrLevel.Count; i++)
                CGlobal.User.mLevel[i] = (int)e.pwrLevel[i];

            menuSysPara.Enabled = (int)e.pwrLevel[0] == 1 ? true : false;
            menuModel.Enabled = (int)e.pwrLevel[1] == 1 ? true : false;
            menuUsers.Enabled = (int)e.pwrLevel[2] == 1 ? true : false;
            menuTestData.Enabled = (int)e.pwrLevel[3] == 1 ? true : false;
            menuRunLog.Enabled = (int)e.pwrLevel[4] == 1 ? true : false;
            menuDebug.Enabled = (int)e.pwrLevel[5] == 1 ? true : false;
            menuVersion.Enabled = (int)e.pwrLevel[6] == 1 ? true : false;
            
            if (e.userName == CGlobal.superUser)
                menuLoad.Enabled = true;
            else
                menuLoad.Enabled = false;

            tlLabCurUser.Text = CGlobal.User.mlogName;

            //CIniFile.WriteToIni("UseSet", "UserName", CGlobal.User.mlogName, CGlobal.UserFile);
            
            if (_childForm != null)
            {
                string er = string.Empty;

                object[] para = new object[] { CGlobal.User.mlogName, CGlobal.User.mlogpassword, CGlobal.User.mLevel };

                if (!CReflect.SendWndMethod(_childForm, EMessType.OnLogIn, out er, para))
                {
                    MessageBox.Show(er, CLanguage.Lan("登录工位信息"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            } 
        }
        #endregion

        #region 退出窗口
        private void menuExit_Click(object sender, EventArgs e)
        {
            OnExitProgram();
        }
        private void tlBtnExit_Click(object sender, EventArgs e)
        {
            OnExitProgram();
        }
        private void OnExitProgram()
        {
            if (_childForm == null)
            {
                if (MessageBox.Show(CLanguage.Lan("确定要退出系统?"), CLanguage.Lan("退出系统"),
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    System.Environment.Exit(0);
                }
            }
            else
            {
                string er = string.Empty;
                if (!CReflect.SendWndMethod(_childForm, EMessType.OnCloseDlg, out er, null))
                {
                    MessageBox.Show(er, CLanguage.Lan("消息机制"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }  
        }
        #endregion

        #region 显示窗口
        private DialogResult OnShowDialog(Form frm)
        {
            if (frm != null)
                return frm.ShowDialog();
            else
                return DialogResult.Cancel; 
        }
        #endregion

        #region 加载工位窗口
        private void menuLoad_Click(object sender, EventArgs e)
        {
             WndSelectFlow dg = new WndSelectFlow();
             if (dg.ShowDialog() != DialogResult.OK)
                 return;
             CGlobal.CFlow.save();
            
             loadChildForm();
 
        }
        #endregion

        #region 系统参数窗口
        private void menuSysPara_Click(object sender, EventArgs e)
        {
            WndFrmSysPara.CreateInstance().Show();
        }
        #endregion

        #region 机种参数窗口
        private void menuModel_Click(object sender, EventArgs e)
        {
            WndFrmModel.CreateInstance().Show();
        }
        #endregion

        #region 用户权限窗口
        private void menuUsers_Click(object sender, EventArgs e)
        {
            WndFrmUsers.CreateInstance().Show();
        }
        #endregion

        #region 测试报表窗口
        private void menuTestData_Click(object sender, EventArgs e)
        {
            WndFrmReport.CreateInstance().Show();
        }
        #endregion

        #region 运行日志窗口
        private void menuRunLog_Click(object sender, EventArgs e)
        {
            WndFrmRunLog.CreateInstance().Show();
        }
        #endregion

        #region 产能查询窗口
        private void menuYieldQuery_Click(object sender, EventArgs e)
        {
            WndFrmYield.CreateInstance().Show();              
        }
        #endregion

        #region 条码查询窗口
        private void menuSnQuery_Click(object sender, EventArgs e)
        {
            WndFrmSnQuery.CreateInstance().Show();  
        }
        #endregion

        #region 点检治具窗口
        private void menuSample_Click(object sender, EventArgs e)
        {
            wndFrmSample.CreateInstance().Show();  
        }   
        #endregion

        #region 调式工具窗口
        private void menuDebug_Click(object sender, EventArgs e)
        {
            WndFrmDebug.CreateInstance().Show();
        }
        #endregion

        #region 软件版本窗口
        private void menuVersion_Click(object sender, EventArgs e)
        {
            WndFrmVersion.CreateInstance().Show();
        }
        #endregion

        #region 系统帮助窗口
        private void menuHelp_Click(object sender, EventArgs e)
        {
            WndFrmHelp.CreateInstance().Show();
        }
        #endregion

        #region 更换皮肤窗口
        private void menuSkin_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region 测试数据统计窗口
        private void menuDataResults_Click(object sender, EventArgs e)
        {
            WndFrmDataResults.CreateInstance().Show();
        }
        #endregion

        #region 语言设置
        private void menuEnglish_Click(object sender, EventArgs e)
        {
            ChangeLanguage(CLanguage.EL.英语);

            string er = string.Empty;

            if (!CReflect.SendWndMethod(_childForm, EMessType.OnChangeLAN, out er, null))
            {
                MessageBox.Show(er, CLanguage.Lan("消息机制"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
        private void menuChinese_Click(object sender, EventArgs e)
        {
            ChangeLanguage(CLanguage.EL.中文);

            string er = string.Empty;

            if (!CReflect.SendWndMethod(_childForm, EMessType.OnChangeLAN, out er, null))
            {
                MessageBox.Show(er,  CLanguage.Lan("消息机制"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }           
        }

        private void menuTaiWan_Click(object sender, EventArgs e)
        {
            ChangeLanguage(CLanguage.EL.繁体 );

            string er = string.Empty;

            if (!CReflect.SendWndMethod(_childForm, EMessType.OnChangeLAN, out er, null))
            {
                MessageBox.Show(er, CLanguage.Lan("消息机制"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
        #endregion

 


    }
}
