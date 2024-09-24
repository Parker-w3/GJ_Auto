using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.COM;

namespace GJ.PLUGINS
{

    /// <summary>
    /// 父窗口插件接口
    /// </summary>
    public interface IMainMsg
    {
        /// <summary>
        /// 显示状态
        /// </summary>
        /// <param name="status"></param>
        void OnShowStatus(EIndicator status);
        /// <summary>
        /// 显示版本及日期
        /// </summary>
        /// <param name="verName"></param>
        /// <param name="verDate"></param>
        void OnShowVersion(string verName, string verDate);
        /// <summary>
        /// 消息状态
        /// </summary>
        /// <param name="para"></param>
        void OnMessage(string name,int lPara,int wPara);
        /// <summary>
        /// 退出系统
        /// </summary>
        void OnExitSystem();
    }
    /// <summary>
    /// 子窗口插件接口
    /// </summary>
    public interface IChildMsg
    {
        /// <summary>
        /// 显示当前窗口到指定父窗口容器中
        /// </summary>
        /// <param name="fatherForm">父窗口</param>
        /// <param name="control">父窗口容器</param>
        /// <param name="guid">设备唯一名称</param>
        void OnShowDlg(Form fatherForm,Control control,string guid);
        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        void OnCloseDlg();

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user"></param>
        /// <param name="mPwrLevel"></param>
        void OnLogIn(string user, string password, int[] mPwrLevel);

        /// <summary>
        /// 启动运行
        /// </summary>
        void OnStartRun();

        /// <summary>
        /// 停止运行
        /// </summary>
        void OnStopRun();

        /// <summary>
        /// 语言切换
        /// </summary>
        void OnChangeLAN();

        /// <summary>
        /// 消息状态
        /// </summary>
        /// <param name="para"></param>
        void OnMessage(string name, int lPara, int wPara);
    }
}
