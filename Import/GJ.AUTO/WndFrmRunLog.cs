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

namespace GJ.AUTO
{
    public partial class WndFrmRunLog : Form
    {
        #region 构造函数
        public WndFrmRunLog()
        {
            InitializeComponent();

            IntialControl();

            SetDoubleBuffered();
        }
        #endregion

        #region 显示窗口

        #region 字段
        private static WndFrmRunLog dlg = null;
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
        public static WndFrmRunLog CreateInstance()
        {
            lock (syncRoot)
            {
                if (dlg == null || dlg.IsDisposed)
                {
                    dlg = new WndFrmRunLog();
                }
            }
            return dlg;
        }
        #endregion

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

        #region 面板回调函数
        private void WndFrmRunLog_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            RefreshView();

            SetLanguage();
        }
        private void treeFiles_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.ToolTipText == "")
                return;
            runlogFile = e.Node.ToolTipText;
            this.Text = CLanguage.Lan("运行日志查询");
            labStatus.Text = CLanguage.Lan("正在加载..");
            labStatus.BackColor = Color.Red;
            progressBar1.Value = 0;
            rtbRunLog.Clear();
            timer1.Interval = 10;
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            rtbRunLog.LoadFile(runlogFile, RichTextBoxStreamType.PlainText);
            labStatus.Text = CLanguage.Lan("加载完毕..");
            this.Text = CLanguage.Lan("运行日志查询") + "--" + runlogFile;
            labStatus.BackColor = Color.Green;
            progressBar1.Value = progressBar1.Maximum;
        }
        #endregion

        #region 字段
        private string runlogFile;
        private string[] _runLogName = new string[] { "RunLog" };
        #endregion

        #region 属性
        public string[] mRunLogName
        {
            set
            {
                _runLogName = value;
            }
        }
        #endregion

        #region 方法
        private void RefreshView()
        {
            try
            {
                treeFiles.Nodes.Clear();
                treeFiles.ImageList = imageList1;
                treeFiles.Nodes.Add(CLanguage.Lan("运行日志"));
                treeFiles.Nodes[0].ImageIndex = 5;
                treeFiles.Nodes[0].SelectedImageIndex = 5;
                TreeNode foldNode = treeFiles.Nodes[0];
                for (int N = 0; N < _runLogName.Length; N++)
                {
                    if (!Directory.Exists(_runLogName[N]))
                        return;
                    //RunLog文件夹       
                    foldNode.Nodes.Add(_runLogName[N]);
                    foldNode.Nodes[N].ImageIndex = 1;
                    foldNode.Nodes[N].SelectedImageIndex = 1;
                    //年份文件夹
                    string[] yearLogName = Directory.GetDirectories(_runLogName[N]);
                    if (yearLogName.Length == 0)
                        return;
                    TreeNode yearNode = foldNode.Nodes[N];
                    for (int i = 0; i < yearLogName.Length; i++)
                    {
                        yearNode.Nodes.Add(Path.GetFileNameWithoutExtension(yearLogName[i]));
                        yearNode.Nodes[i].ImageIndex = 3;
                        yearNode.Nodes[i].SelectedImageIndex = 2;
                        //月份文件夹
                        string[] monthName = Directory.GetDirectories(yearLogName[i]);
                        if (monthName.Length == 0)
                            continue;
                        TreeNode monthNode = yearNode.Nodes[i];
                        for (int j = 0; j < monthName.Length; j++)
                        {
                            monthNode.Nodes.Add(Path.GetFileNameWithoutExtension(monthName[j]));
                            monthNode.Nodes[j].ImageIndex = 3;
                            monthNode.Nodes[j].SelectedImageIndex = 2;
                            //日期文件夹
                            string[] dateName = Directory.GetDirectories(monthName[j]);
                            if (dateName.Length == 0)
                                continue;
                            TreeNode dateNode = monthNode.Nodes[j];
                            for (int z = 0; z < dateName.Length; z++)
                            {
                                dateNode.Nodes.Add(Path.GetFileNameWithoutExtension(dateName[z]));
                                dateNode.Nodes[z].ImageIndex = 3;
                                dateNode.Nodes[z].SelectedImageIndex = 2;
                                //运行日志
                                string[] logFiles = Directory.GetFiles(dateName[z]);
                                if (logFiles.Length == 0)
                                    continue;
                                TreeNode fileNode = dateNode.Nodes[z];
                                for (int k = 0; k < logFiles.Length; k++)
                                {
                                    fileNode.Nodes.Add(Path.GetFileNameWithoutExtension(logFiles[k]));
                                    fileNode.Nodes[k].ToolTipText = logFiles[k];
                                    fileNode.Nodes[k].ImageIndex = 4;
                                    fileNode.Nodes[k].SelectedImageIndex = 4;
                                }
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

        #region 语言切换
        /// <summary>
        /// 加载中英文界面
        /// </summary>
        private void SetLanguage()
        {
            CLanguage.SetLanguage(dlg);

            switch (CLanguage.languageType)
            {
                case CLanguage.EL.中文:
                    this.Text = "运行日志查询";
                    break;
                case CLanguage.EL.英语:
                    this.Text = "Run Log Query";
                    break;
                case CLanguage.EL.繁体:
                    this.Text = CLanguage.Lan("运行日志查询");
                    labStatus.Text = CLanguage.Lan("加载完毕..");
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
