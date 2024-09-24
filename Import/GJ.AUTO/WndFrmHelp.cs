using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using GJ.COM;
using ThoughtWorks.QRCode.Codec;

namespace GJ.AUTO
{
    partial class WndFrmHelp : Form
    {
        #region 构造函数
        private WndFrmHelp()
        {
            InitializeComponent();
            this.Text = String.Format(CLanguage.Lan("关于 {0}"), AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format(CLanguage.Lan("版本 {0}"), AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.textBoxDescription.Text = AssemblyDescription;

            SetLanguage();
        }
        #endregion        

        #region 显示窗口

        #region 字段
        private static WndFrmHelp dlg = null;
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
        public static WndFrmHelp CreateInstance()
        {
            lock (syncRoot)
            {
                if (dlg == null || dlg.IsDisposed)
                {
                    dlg = new WndFrmHelp();
                    CLanguage.SetLanguage(dlg);
                }
            }
            return dlg;
        }
        #endregion

        #endregion

        #region 程序集特性访问器

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

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
                        break;
                    case CLanguage.EL.英语:
                        break;
                    case CLanguage.EL.繁体:
                        this.Text = CLanguage.Lan("软件说明");
                        labelCompanyName.Text = CLanguage.Lan("公司名称");
                        labelCopyright.Text = CLanguage.Lan("版权");
                        labelProductName.Text = CLanguage.Lan("产品名称");
                        labelVersion.Text = CLanguage.Lan("版本");
                        okButton.Text = CLanguage.Lan("确定(&O)");
                        textBoxDescription.Text = CLanguage.Lan("说明");
                        break;
                    default:
                        break;
                }
            }
        }

        private void WndFrmHelp_Load(object sender, EventArgs e)
        {
            ShowQRCode();
            this.Text = String.Format(CLanguage.Lan("关于:{0}"), AssemblyTitle);
            this.labelProductName.Text += AssemblyProduct;
            this.labelVersion.Text += AssemblyVersion;
            this.labelCopyright.Text += AssemblyCopyright;
            this.labelCompanyName.Text += AssemblyCompany;
            this.textBoxDescription.Text += AssemblyDescription;
        }
        #region QRCode
        private Bitmap bimg = null; //保存生成的二维码，方便后面保存
        public void ShowQRCode()
        {
            string rData = CLanguage.Lan("设备编号：") + CLanguage.Lan(CGlobal.equipmentID) + "\r\n"
                + CLanguage.Lan("设备名称：") + CLanguage.Lan(CGlobal.equipmentName) + "\r\n"
                 + CLanguage.Lan("设备工位：") + CLanguage.Lan(CGlobal.equipmentStation) + "\r\n"
                 + CLanguage.Lan("项目编号：") + CLanguage.Lan(CGlobal.projectID) + "\r\n"
                 + CLanguage.Lan("项目名称：") + CLanguage.Lan(CGlobal.projectName) + "\r\n";

            bimg = CreateQRCode(rData);

            picQRCode.Image = bimg;
        }

        /// <summary>
        /// 生成二维码，如果有Logo，则在二维码中添加Logo
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Bitmap CreateQRCode(string content)
        {
            QRCodeEncoder qrEncoder = new QRCodeEncoder();
            qrEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrEncoder.QRCodeScale = 2;
            qrEncoder.QRCodeVersion = 10;
            qrEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            try
            {
                Bitmap qrcode = qrEncoder.Encode(content, Encoding.UTF8);

                return qrcode;
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show("超出当前二维码版本的容量上限，请选择更高的二维码版本！", "系统提示");
                return new Bitmap(100, 100);
            }
            catch (Exception ex)
            {
                MessageBox.Show("生成二维码出错！", "系统提示:" + ex.ToString());
                return new Bitmap(100, 100);
            }
        }
        #endregion

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
