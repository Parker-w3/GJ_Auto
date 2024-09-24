using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using System.Windows.Forms;
using GJ.COM;

namespace GJ.APP
{
    /// <summary>
    /// 用户APP接口
    /// </summary>
    public interface IAPP
    {
        void loadAppSetting();
    }
    /// <summary>
    /// 用户消息类:
    /// object[0]=功能类型
    /// object[1]=功能参数
    /// </summary>
    public class CUserArgs:EventArgs
    {
        public readonly string name;
        public readonly int lPara;
        public readonly int wPara;
        public CUserArgs(string name,int lPara,int wPara)
        {
            this.name = name;
            this.lPara = lPara;
            this.wPara = wPara;
        }
    }
    /// <summary>
    /// 用户配置参数
    /// </summary>
    public class CUserApp
    {
        /// <summary>
        /// 用户触发静态消息
        /// </summary>
        public static COnEvent<CUserArgs> OnUserArgs = new COnEvent<CUserArgs>();
        /// <summary>
        /// 项目平台配置加载GJ.USER.APP.dll类静态参数
        /// </summary>
        public static void LoadAppSetting()
        {
            try
            {
                string appFile = AppDomain.CurrentDomain.BaseDirectory + "GJ.USER.APP.dll";

                if (!File.Exists(appFile))
                    return;

                NameValueCollection appSettings = ConfigurationManager.AppSettings;

                List<string> coustoms = new List<string>();

                List<string> projects = new List<string>();

                foreach (var item in appSettings.Keys)
                {
                    string keyName = item.ToString();

                    string keyValue = appSettings[keyName].ToString();

                    string proName = "Project";

                    if (keyName.Length > proName.Length)
                        keyName = keyName.Substring(keyName.Length - proName.Length, proName.Length);

                    if (keyName == "Project")
                    {
                        if (!projects.Contains(keyValue))
                        {
                            projects.Add(keyValue);
                        }
                    }
                }

                for (int i = 0; i < projects.Count; i++)
                {
                    string projectName = "GJ.USER.APP." + projects[i];

                    Assembly asb = Assembly.UnsafeLoadFrom(appFile);

                    IAPP app = (IAPP)asb.CreateInstance(projectName);

                    if (app != null)
                    {
                        loadApp(projects[i], app);

                        app.loadAppSetting();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "LoadAppSetting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        /// <summary>
        /// 加载app.config配置参数
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="app"></param>
        private static void loadApp(string projectName ,IAPP app)
        {
            try
            {
                 FieldInfo[] fields = app.GetType().GetFields();

                 NameValueCollection appSettings = ConfigurationManager.AppSettings;

                 bool progList = false;

                 string c_proName = "Project";

                 string custom = string.Empty;

                 foreach (var item in appSettings.Keys)
                 {
                     string keyName = item.ToString();

                     string keyValue = appSettings[keyName].ToString();

                     if (keyValue == projectName)
                     {
                         if (!progList)
                         {
                             custom = item.ToString().Substring(0,item.ToString().Length - c_proName.Length-1);
                             progList = true;
                         }
                         else
                         {
                             break;
                         }
                     }
                     else
                     {
                         if (progList)
                         {
                             for (int i = 0; i < fields.Length; i++)
                             {
                                 string itemName = custom + "_" + fields[i].Name;

                                 if (itemName == keyName)
                                 {
                                     object valObj = System.Convert.ChangeType(keyValue,fields[i].FieldType);
                                     fields[i].SetValue(app, valObj);
                                 }
                             }                         
                         }
                     }
                 }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "loadApp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        
        }
    }
}
