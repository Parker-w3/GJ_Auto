using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using GJ;
using GJ.PDB;
using GJ.COM;
using GJ.APP;

namespace GJ.AUTO
{
    public partial class WndFrmLogIn : Form
    {
        #region 构造函数
        public WndFrmLogIn()
        {
            InitializeComponent();
        }
        #endregion

        #region 面板回调函数
        private void WndFrmLogIn_Load(object sender, EventArgs e)
        {
            loadLanguage();

            //定义控件焦点事件
            cmbUsers.Enter += new EventHandler(Control_Enter);
            cmbUsers.Leave += new EventHandler(control_Clear);
            txtPassWord.Enter += new EventHandler(Control_Enter);
            txtPassWord.Leave += new EventHandler(control_Clear);
            //设置查询功能
            cmbUsers.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbUsers.AutoCompleteSource = AutoCompleteSource.ListItems;
            //初始化登录用户
            cmbUsers.Items.Clear();
            List<string> users = new List<string>();
            string er=string.Empty ;
            if(!GetAllUserName(ref users,ref er))
            {
                MessageBox.Show(er);  
            }
            for (int i = 0; i < users.Count; i++)
            {
                cmbUsers.Items.Add(users[i]);
                if (i == 0)
                    cmbUsers.Text = users[i].ToString(); 
            }
        }
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            List<int> pwrLevel = new List<int>();
            string er = string.Empty;
            if (this.cmbUsers.Text == CGlobal.superUser  && this.txtPassWord.Text == CGlobal.superPwr)
            {
                              
               for (int i = 0; i < 8; i++)               
                  pwrLevel.Add(1);

               OnLogInArgs.OnEvented(new CLogInArgs(this.cmbUsers.Text, this.txtPassWord.Text, pwrLevel));
                txtPassWord.Text = string.Empty;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                if (!CheckUserIsValiable(this.cmbUsers.Text, this.txtPassWord.Text, ref pwrLevel, ref er))
                    MessageBox.Show(er, CLanguage.Lan("用户登录"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                {

                    OnLogInArgs.OnEvented(new CLogInArgs(this.cmbUsers.Text, this.txtPassWord.Text, pwrLevel));
                    txtPassWord.Text = string.Empty;
                    this.DialogResult = DialogResult.OK;
                }            
            }
        }
        #endregion

        #region 焦点应用
        private void Control_Enter(object sender, EventArgs e)
        {
            if (sender.GetType().ToString() == "System.Windows.Forms.ComboBox")
                ((ComboBox)sender).BackColor = Color.Cyan;
            else
                ((TextBox)sender).BackColor = Color.Cyan;
        }
        private void control_Clear(object sender, EventArgs e)
        {
            if (sender.GetType().ToString() == "System.Windows.Forms.ComboBox")
                ((ComboBox)sender).BackColor = Color.White;
            else
                ((TextBox)sender).BackColor = Color.White;
        }
        #endregion

        #region 定义事件
        public COnEvent<CLogInArgs> OnLogInArgs = new COnEvent<CLogInArgs>();
        #endregion

        #region 数据库查询操作
        /// <summary>
        /// 获取登录用户列表
        /// </summary>
        /// <param name="users"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool GetAllUserName(ref List<string> users, ref string er)
        {
            CDBCOM db = new CDBCOM(EDBType.Access);
            DataSet ds = null;
            string sqlCmd = "Select UserName from UserInfo order by UserName";
            if (!db.QuerySQL(sqlCmd, out ds, out er))
                return false;
            int userNum = ds.Tables[0].Rows.Count;
            for (int i = 0; i < userNum; i++)
            {
                users.Add(ds.Tables[0].Rows[i].ItemArray[0].ToString());
            }
            ds.Dispose();
            return true;
        }
        /// <summary>
        /// 检查登陆用户和密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwr"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public bool CheckUserIsValiable(string userName, string pwr, ref List<int> pwrLevel, ref string er)
        {
            CDBCOM db = new CDBCOM(EDBType.Access);
            DataSet ds = null;
            string sqlCmd = "Select * from UserInfo Where UserName='" + userName + "' order by UserName";
            if (!db.QuerySQL(sqlCmd, out ds, out er))
                return false;
            if (ds.Tables[0].Rows.Count == 0)
            {
                er = CLanguage.Lan("用户不存在,请重新输入.");
                return false;
            }
            bool chkPassWord = false;
            if (ds.Tables[0].Rows[0].ItemArray[1].ToString() == pwr)
            {
                chkPassWord = true;
                for (int j = 2; j < ds.Tables[0].Columns.Count; j++)
                {
                    pwrLevel.Add(System.Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[j]));
                }
            }

            if (!chkPassWord)
            {
                er = CLanguage.Lan("密码错误,请重新输入.");
                return false;
            }
            return true;
        }
        #endregion

        #region 语言设置
        /// <summary>
        /// 中英设置
        /// </summary>
        private void loadLanguage()
        {
          switch (CLanguage.languageType)
          {
              case CLanguage.EL.中文:
                  break;
              case CLanguage.EL.英语:
                  break;
              case CLanguage.EL.繁体:
                  this.Text = CLanguage.Lan("用户登录");
                  btnCancel.Text = CLanguage.Lan("取消(&C)");
                  btnLogIn.Text = CLanguage.Lan("确认(&O)");
                  label2.Text = CLanguage.Lan("冠佳使命:致力于人性化 节能化 自动化 智能化工厂缔造");
                  PasswordLabel.Text = CLanguage.Lan("密码(&P):");
                  UsernameLabel.Text = CLanguage.Lan("用户名(&U):");

                  break;
              default:
                  break;
          }

        }
        #endregion

    }
}
