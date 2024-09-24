using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.PLUGINS;
using System.IO;
using GJ.MES;
using GJ.PDB;
using GJ.COM;
using GJ.USER.APP;
namespace GJ.Aging.BURNIN
{
    public partial class FrmReport : Form, IChildMsg
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
                    btnRefresh.Text = CLanguage.Lan("刷新(&S)");
                    labSelete.Text = CLanguage.Lan("双击选择[文件夹]或[文件]");
                    labStatus.Text = CLanguage.Lan("加载完毕...");
                    break;
                case CLanguage.EL.英语:
                    break;
                case CLanguage.EL.繁体 :
                    btnRefresh.Text = CLanguage.Lan("刷新(&S)");
                    labSelete.Text = CLanguage.Lan("双击选择[文件夹]或[文件]");
                    labStatus.Text = CLanguage.Lan("加载完毕...");
                    break;
                default:
                    break;
            }            
        }
        #endregion

        #region 构造函数
        public FrmReport()
        {
            InitializeComponent();
            
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
                                           .SetValue(splitContainer1.Panel1, true, null);
        }
        #endregion

        #region 字段
        private string reportFolder = string.Empty;
        #endregion

        #region 方法
        private void RefreshView()
        {
            try
            {
                treeFiles.Nodes.Clear();
                treeFiles.ImageList = imageList1;
                treeFiles.Nodes.Add(CLanguage.Lan ("测试数据信息"));
                treeFiles.Nodes[0].ImageIndex = 5;
                treeFiles.Nodes[0].SelectedImageIndex = 5;
                TreeNode foldNode = treeFiles.Nodes[0];

                if (!Directory.Exists(reportFolder))
                    return;
                //测试数据文件夹       
                foldNode.Nodes.Add(reportFolder);
                foldNode.Nodes[0].ImageIndex = 1;
                foldNode.Nodes[0].SelectedImageIndex = 1;
                //区域文件夹           
                string[] areaName = Directory.GetDirectories(reportFolder);
                if (areaName.Length == 0)
                    return;
                TreeNode areaNode = foldNode.Nodes[0];
                for (int i = 0; i < areaName.Length; i++)
                {
                    areaNode.Nodes.Add(Path.GetFileNameWithoutExtension(areaName[i]));
                    areaNode.Nodes[i].ImageIndex = 3;
                    areaNode.Nodes[i].SelectedImageIndex = 2;
                    areaNode.Nodes[i].ToolTipText =CLanguage.Lan ( "区域");
                    //机种文件夹
                    string[] modelName = Directory.GetDirectories(areaName[i]);
                    if (modelName.Length == 0)
                        return;
                    TreeNode modelNode = areaNode.Nodes[i];
                    for (int j = 0; j < modelName.Length; j++)
                    {
                        modelNode.Nodes.Add(Path.GetFileNameWithoutExtension(modelName[j]));
                        modelNode.Nodes[j].ImageIndex = 3;
                        modelNode.Nodes[j].SelectedImageIndex = 2;
                        modelNode.Nodes[j].ToolTipText = CLanguage.Lan ("机种名");
                        //日期文件夹
                        string[] dateName = Directory.GetDirectories(modelName[j]);
                        if (dateName.Length == 0)
                            return;
                        TreeNode dateNode = modelNode.Nodes[j];
                        for (int k = 0; k < dateName.Length; k++)
                        {
                            dateNode.Nodes.Add(Path.GetFileNameWithoutExtension(dateName[k]));
                            dateNode.Nodes[k].ImageIndex = 3;
                            dateNode.Nodes[k].SelectedImageIndex = 2;
                            dateNode.Nodes[k].ToolTipText =CLanguage.Lan ( "日期");
                            //报表文件
                            string[] reportFiles = Directory.GetFiles(dateName[k]);
                            if (reportFiles.Length == 0)
                                continue;
                            TreeNode fileNode = dateNode.Nodes[k];
                            for (int t = 0; t < reportFiles.Length; t++)
                            {
                                fileNode.Nodes.Add(Path.GetFileNameWithoutExtension(reportFiles[t]));
                                fileNode.Nodes[t].ToolTipText = reportFiles[t];
                                fileNode.Nodes[t].ImageIndex = 4;
                                fileNode.Nodes[t].SelectedImageIndex = 4;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }


        #endregion

        #region 面板回调函数
        private bool folderFlag = false; 
        private string curDate = string.Empty;
        private string curFileName = string.Empty;
        private void FrmReport_Load(object sender, EventArgs e)
        {
            SetUILanguage();
            reportFolder = CGlobalPara.SysPara.Report.ReportPath;
            RefreshView();
        }
        private void treeFiles_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.ToolTipText == "")
                return;
            if (e.Node.ToolTipText == CLanguage.Lan ("区域") || e.Node.ToolTipText == CLanguage.Lan ("机种名") || e.Node.ToolTipText == CLanguage.Lan ("日期"))
            {
                curDate = e.Node.Text;
                folderFlag = true;
                labSelete.Text = CLanguage.Lan ("双击选择当前为【文件夹】");
            }
            else
            {
                curFileName = e.Node.ToolTipText;
                folderFlag = false;
                labSelete.Text = CLanguage.Lan ("双击选择当前为【文件】");
            }
            labStatus.Text = CLanguage.Lan ("正在加载..");
            labStatus.BackColor = Color.Red;
            progressBar1.Value = 0;
            rtbRunLog.Clear();
            timer1.Interval = 10;
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            if (!folderFlag)
            {
                rtbRunLog.LoadFile(curFileName, RichTextBoxStreamType.PlainText);
            }
            labStatus.Text = CLanguage.Lan ("加载完毕..");
            labStatus.BackColor = Color.Green;
            progressBar1.Value = progressBar1.Maximum;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshView();
            rtbRunLog.Clear();

        }

        #endregion       


    }
}
