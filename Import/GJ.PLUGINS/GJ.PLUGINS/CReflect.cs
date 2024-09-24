using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using GJ.COM;
namespace GJ.PLUGINS
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum EMessType
    {
        /// <summary>
        /// 显示状态
        /// 输入:EIndicator status
        /// </summary>
        OnShowStatus,
        /// <summary>
        /// 显示版本
        /// 输入:string verName, string verDate
        /// </summary>
        OnShowVersion,
        /// <summary>
        /// 消息应答
        /// 输入:object[] para
        /// </summary>
        OnMessage,
        /// <summary>
        /// 退出系统
        /// </summary>
        OnExitSystem,

        /// <summary>
        /// 显示窗口
        /// 输入:Form fatherForm,Control control,string guid
        /// </summary>
        OnShowDlg,
        /// <summary>
        /// 关闭窗口 
        /// 输入:无
        /// </summary>
        OnCloseDlg,
        /// <summary>
        /// 用户登录
        /// 输入:string user, int[] mPwrLevel
        /// </summary>
        OnLogIn,
        /// <summary>
        /// 启动监控
        /// 输入:无
        /// </summary>
        OnStartRun,
        /// <summary>
        /// 停止监控
        /// 输入:无
        /// </summary>
        OnStopRun,
        /// <summary>
        /// 中英文切换
        /// 输入:无
        /// </summary>
        OnChangeLAN,
    }
    /// <summary>
    /// lPara消息类型
    /// </summary>
    public enum ElPara
    {
        保存,
        退出
    }
    /// <summary>
    /// 反射类
    /// </summary>
    public class CReflect
    {
        /// <summary>
        /// 通过动态库加载子窗口
        /// </summary>
        /// <param name="childClass">子窗口类名</param>
        /// <param name="childForm">子窗口</param>
        /// <param name="er"></param>
        /// <param name="interfaceName">接口名称</param>
        /// <param name="dllType">动态库类型</param>
        /// <returns></returns>
        public static bool LoadChildForm(string childClass, out Form childForm, out string er,
                                                            string interfaceName = "IChildMsg", string dllType = "GJ")
        {
            childForm = null;

            er = string.Empty;

            try
            {

                //本目录调用dll文件

                object obj = null;

                string[] fileNames = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory);

                foreach (string file in fileNames)
                {
                    string fileName = Path.GetFileName(file);

                    if ((fileName.ToUpper().StartsWith(dllType)) && (fileName.ToUpper().EndsWith(".DLL")))
                    {
                        try
                        {
                            //载入dll
                            Assembly asb = Assembly.LoadFrom(file);
                            Type[] types = asb.GetTypes();
                            foreach (Type t in types)
                            {
                                if (t.GetInterface(interfaceName) != null && t.FullName == childClass)
                                {
                                    obj = asb.CreateInstance(t.FullName);
                                    break;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            
                        }                       
                    }
                }

                if (obj == null)
                {
                    er = CLanguage.Lan("加载动态库失败") + ":" + "[" + childClass + "]";
                    return false;
                }

                childForm = (Form)obj;

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 加载父窗口到子窗口中,并加载子窗口到父窗口容器中
        /// </summary>
        /// <param name="fatherForm">父窗口</param>
        /// <param name="fatherControl">父窗口显示子窗口容器</param>
        /// <param name="childForm">子窗口</param>        
        /// <param name="?"></param>
        /// <returns></returns>
        public static bool ShowChildForm(Form fatherForm, Control fatherControl, string guid, Form childForm,
                                                          out string er, string interfaceName = "OnShowDlg")
        {
            er = string.Empty;

            try
            {
                Type type = childForm.GetType();

                MethodInfo OnWndDlg = type.GetMethod(interfaceName);

                //MethodInfo OnWndDlg = type.GetMethod(interfaceName, new Type[] { typeof(Form), typeof(Control), typeof(string) });

                if (OnWndDlg == null)
                {
                    er = CLanguage.Lan("找不到接口函数") + "[" + interfaceName + "]";
                    return false;
                }

                foreach (Control obj in fatherControl.Controls)
                {
                    fatherControl.Controls.Remove(obj);
                    obj.Dispose();
                }

                OnWndDlg.Invoke(childForm, new object[] { fatherForm, fatherControl, guid });

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }

        /// <summary>
        /// 触发窗口方法
        /// </summary>
        /// <param name="wndForm"></param>
        /// <param name="methodName"></param>
        /// <param name="er"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public static bool SendWndMethod(Form wndForm, EMessType methodName, out string er, params object[] para)
        {
            er = string.Empty;

            try
            {
                if (wndForm == null)
                {
                    er = CLanguage.Lan("未初始化窗口");
                    return false;                    
                }

                Type type = wndForm.GetType();

                MethodInfo OnWndDlg = type.GetMethod(methodName.ToString());

                if (OnWndDlg == null)
                {
                    er = CLanguage.Lan("找不到窗口方法") + "[" + methodName.ToString() + "]";
                    return false;
                }

                
                
                OnWndDlg.Invoke(wndForm, para);

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
    }
}
