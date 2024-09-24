using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ.AUTO
{
    #region 用户登录事件消息
    /// <summary>
    /// 用户登录
    /// </summary>
    public class CLogInArgs : EventArgs
    {
        public readonly string userName;
        public readonly string userPassword;
        public readonly List<int> pwrLevel;
        public CLogInArgs(string userName, string userPassword, List<int> pwrLevel)
        {
            this.userName = userName;
            this.userPassword = userPassword;
            this.pwrLevel = pwrLevel;
        }
    }
    #endregion
    
}
