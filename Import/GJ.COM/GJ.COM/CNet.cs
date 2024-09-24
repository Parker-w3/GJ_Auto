using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace GJ.COM
{
    public class CNet
    {
        /// <summary>
        /// Ping远程电脑IP
        /// </summary>
        /// <param name="remoteHost"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool PingRemotePC(string remoteHost, out string er)
        {
            er = string.Empty;

            try
            {
                if (remoteHost == "127.0.0.1" || remoteHost.ToLower() == "localhost")
                    return true;
                bool pingFlag = false;
                Process proc = new Process();
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                string dosLine = @"ping -n 1 " + remoteHost;
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (proc.HasExited == false)
                {
                    proc.WaitForExit(500);
                }
                string pingResult = proc.StandardOutput.ReadToEnd();

                if ( pingResult.IndexOf("无法访问目标主机")==-1 && pingResult.IndexOf("(0% 丢失)") != -1)
                    pingFlag = true;
                else
                    er = "连接[" + remoteHost + "]超时:" + pingResult;
                proc.StandardOutput.Close();
                return pingFlag;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 登录远程电脑
        /// </summary>
        /// <param name="remoteHost">电脑名称或IP</param>
        /// <param name="userName">登录用户</param>
        /// <param name="passWord">登录密码</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool ConnectRemotePC(string remoteHost, string userName, string passWord, out string er)
        {
            er = string.Empty;

            try
            {
                if (remoteHost == "127.0.0.1" || remoteHost.ToLower() == "localhost")
                    return true;
                bool connFlag = false;
                Process proc = new Process();
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                string dosLine = @"net use \\" + remoteHost + " " + passWord + " " + " /user:" + userName + " >NUL";
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (proc.HasExited == false)
                {
                    proc.WaitForExit(1000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                if (errormsg != "")
                    er = "登录系统:" + errormsg + "[" + dosLine + "]";
                else
                    connFlag = true;
                proc.StandardError.Close();
                return connFlag;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 登录远程目录
        /// </summary>
        /// <param name="remoteFloder">远程目录</param>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool ConnectRemoteFolder(string remoteFloder, string userName, string passWord, out string er)
        {
            er = string.Empty;

            try
            {
                bool connFlag = false;
                Process proc = new Process();
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                string dosLine = @"net use " + remoteFloder + " \"" + passWord + "\" /user:\"" + userName + " >NUL";
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (proc.HasExited == false)
                {
                    proc.WaitForExit(1000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                while (proc.HasExited == false)
                {
                    proc.WaitForExit(1000);
                }
                if (errormsg != "")
                    er = "登录目录:" + errormsg + "[" + dosLine + "]";
                else
                    connFlag = true;
                proc.StandardError.Close();
                return connFlag;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
    }
}
