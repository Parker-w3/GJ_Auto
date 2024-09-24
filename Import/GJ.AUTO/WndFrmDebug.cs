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
using GJ.PLUGINS;
using System.Xml;
namespace GJ.AUTO
{
   public partial class WndFrmDebug : Form,IMainMsg
   {
      #region 插件方法
       /// <summary>
       /// 子窗口名称
       /// </summary>
       private List<string> _childFormName = new List<string>();
        /// <summary>
        /// 子窗口
        /// </summary>
        private List<Form> _childFormList = new List<Form>();
        /// <summary>
        /// 指示运行状态 
        /// </summary>
        /// <param name="status"></param>
        public void OnShowStatus(EIndicator status)
        {

        }
        /// <summary>
        /// 显示软件版本
        /// </summary>
        /// <param name="verName"></param>
        /// <param name="verDate"></param>
        public void OnShowVersion(string verName, string verDate)
        {

        }
        /// <summary>
        /// 消息触发
        /// </summary>
        /// <param name="para"></param>
        public void OnMessage(string name,int lPara,int wPara)
        {

        }
        /// <summary>
        /// 退出系统
        /// </summary>
        public void OnExitSystem()
        {
            System.Environment.Exit(0);
        }
        #endregion

      #region 显示窗口

       #region 字段
       private static WndFrmDebug dlg = null;
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
       public static WndFrmDebug CreateInstance()
       {
           lock (syncRoot)
           {
               if (dlg == null || dlg.IsDisposed)
               {
                   dlg = new WndFrmDebug();
               }
           }
           return dlg;
       }
       #endregion

       #endregion

      #region 构造函数
       private WndFrmDebug()
       {
           InitializeComponent();

           SetDoubleBuffered();

           IntialControl();

           SetDoubleBuffered();

       }
       #endregion

      #region 字段
       /// <summary>
       /// 调式工具路径
       /// </summary>
       private string _tbrFile = Application.StartupPath + "\\ToolLog\\device.xml";
       /// <summary>
       /// 调式工具对应名称
       /// </summary>
       private Dictionary<string, string> _className = null;
       /// <summary>
       /// 调式工具列表
       /// </summary>
       private Dictionary<string, int> _devView = null;
       /// <summary>
       /// 初始化OK
       /// </summary>
       private bool _InitOK = false;
       #endregion

      #region 面板控件
       private TabControl TabDev = null;
       #endregion

      #region 初始化
       /// <summary>
       /// 绑定控件
       /// </summary>
       private void IntialControl()
       {
           try
           {
               string er = string.Empty;

               if (!load(_tbrFile, out _className, out _devView, out er))
               {
                   MessageBox.Show(er, CLanguage.Lan("加载调式工具"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                   return;
               }

               if (!OnLoadToolChilds(out er))
               {
                   MessageBox.Show(er, CLanguage.Lan("加载调式工具"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                   return;
               }

               _InitOK = true;
           }
           catch (Exception)
           {
               throw;
           }
       }
       /// <summary>
       /// 设置双缓冲,防止界面闪烁
       /// </summary>
       private void SetDoubleBuffered()
       {

       }
       #endregion

      #region 面板回调函数
      private void WndFrmDebug_Load(object sender, EventArgs e)
      {
          if (_InitOK)
          {
              string er = string.Empty;              

              LoadToolViewHanlder loadToolView = new LoadToolViewHanlder(OnLoadToolView);

              IAsyncResult result= loadToolView.BeginInvoke(null, null);

              while (!result.IsCompleted)
                  Application.DoEvents();

              for (int i = 0; i < _childFormList.Count; i++)
              {
                  CReflect.SendWndMethod(_childFormList[i], EMessType.OnChangeLAN, out er, null);
              }
          }

          SetLanguage();
      }
      private void WndFrmDebug_FormClosing(object sender, FormClosingEventArgs e)
      {
          string er = string.Empty;

          for (int i = 0; i < _childFormList.Count; i++)
          {
              CReflect.SendWndMethod(_childFormList[i], EMessType.OnCloseDlg, out er,null);
          }
      }
      #endregion

      #region 获取调式工具XML
      /// <summary>
      /// 加载xml
      /// </summary>
      /// <param name="xlmFile"></param>
      public bool load(string xlmFile,out Dictionary<string,string> className, out Dictionary<string,int> debugView,out string er)
      {

          className = new Dictionary<string, string>();

          debugView = new Dictionary<string, int>();

          er = string.Empty;

          try
          {
              if (xlmFile == string.Empty)
                  xlmFile = Application.StartupPath + "\\ToolLog\\device.xml";

              if (!File.Exists(xlmFile))
              {
                  er = CLanguage.Lan("找不到配置文件[")+ xlmFile +"]";
                  return false;
              }

              XmlDocument doc = new XmlDocument();
              doc.Load(xlmFile);
              XmlNode node = doc.DocumentElement;
              XmlNodeList nodeList = node.ChildNodes;
              for (int i = 0; i < nodeList.Count; i++)
              {
                  string name = nodeList[i].Name;
                  if (nodeList[i].Attributes.Count > 0)
                      name = nodeList[i].Attributes[0].Value;
                  switch (nodeList[i].NodeType)
                  {
                      case XmlNodeType.Element:
                          className.Add(nodeList[i].Name,CLanguage.Lan ( name));
                          debugView.Add(nodeList[i].Name, System.Convert.ToInt32(nodeList[i].InnerText));
                          break;
                      case XmlNodeType.Text:
                          className.Add(nodeList[i].Name, CLanguage.Lan(name));
                          debugView.Add(name, System.Convert.ToInt32(nodeList[i].Value));
                          break;
                      default:
                          break;
                  }
              }
              return true;
          }
          catch (Exception ex)
          {
              er = ex.ToString();
              return false;
          }
      }
      #endregion      

      #region 方法
      private delegate void LoadToolViewHanlder();
      /// <summary>
      /// 加载子窗口
      /// </summary>
      private bool OnLoadToolChilds(out string er)
      {
          er = string.Empty;

          try
          {
              int devNo = 0;

              _childFormName.Clear();

              _childFormList.Clear();

              foreach (string keyVal in _devView.Keys)
              {
                  if (_devView[keyVal] == 1)
                  {
                      Form obj = LoadChildForm(keyVal);

                      if (obj != null)
                      {
                          _childFormName.Add(_className[keyVal]); 
                         _childFormList.Add(obj);
                         devNo++;
                      }
                  }
              }

              return true;
          }
          catch (Exception ex)
          {
              er = ex.ToString();
              return false;
          }
      }
      /// <summary>
      /// 加载调式工具列表
      /// </summary>
      private void OnLoadToolView()
      {
          if (this.InvokeRequired)
              this.Invoke(new Action(OnLoadToolView));
          else
          {
              string er = string.Empty;
              TabDev = new TabControl();
              TabDev.Dock = DockStyle.Fill;
              TabDev.SizeMode = TabSizeMode.Fixed;
              TabDev.Appearance = TabAppearance.FlatButtons;
              this.Controls.Add(TabDev);
              for (int i = 0; i < _childFormList.Count; i++)
              {
                  _childFormList[i].Dock = DockStyle.Fill;
                  TabDev.TabPages.Add(_childFormName[i]);
                  TabDev.TabPages[i].BorderStyle = BorderStyle.Fixed3D;

                  CReflect.ShowChildForm(this,(Control)TabDev.TabPages[i],_childFormList[i].Name,_childFormList[i],out er);
              }
          }        
      }
      /// <summary>
      /// 加载测试工位
      /// </summary>
      private Form LoadChildForm(string dlgName)
      {
          try
          {
              string er = string.Empty;

              Form childForm = null;

              string className = "GJ.TOOL.Frm" + dlgName;

              if (!CReflect.LoadChildForm(className, out childForm, out er))
              {
                  MessageBox.Show(er, CLanguage.Lan("加载工位"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                  return null;
              }

              return childForm;
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
          if (this.InvokeRequired)
              this.Invoke(new Action(SetLanguage));
          else
          {
              CLanguage.SetLanguage(dlg);

              switch (CLanguage.languageType)
              {
                  case CLanguage.EL.中文:
                      this.Text = CLanguage.Lan("调式工具");
                      break;
                  case CLanguage.EL.英语:
                      this.Text = "Debug Tool";
                      break;
                  case CLanguage.EL.繁体 :
                      this.Text = CLanguage.Lan("调式工具");
                      break;
                  default:
                      break;
              }
          }
      }
      #endregion

   }
}
