using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.PDB;
using GJ.COM;
namespace GJ.AUTO
{
    public partial class WndSelectFlow : Form
    {
        public WndSelectFlow()
        {
            InitializeComponent();
            
        }

        private void WndSelectFlow_Load(object sender, EventArgs e)
        {
            try
            {

                SetLanguage();

                listFlow.Clear();  
                string er = string.Empty;
                List<string> flowGUID = new List<string>();
                List<string> flowName = new List<string>();
                List<int> iconKey = new List<int>();
                if (!getFlowInfo(ref flowGUID,ref flowName, ref iconKey, ref er))
                {
                    MessageBox.Show(er.ToString());
                    return;
                }
                for (int i = 0; i < flowName.Count; i++)
                {
                    listFlow.Items.Add(CLanguage.Lan(flowName[i]), iconKey[i]);
                    listFlow.Items[i].ToolTipText = flowGUID[i]; 
                }                  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());   
            }                   
        }
        private bool getFlowInfo(ref List<string> flowGUID, ref List<string> flowName, ref List<int> iconKey,ref string er)
        {
            try
            {
                CDBCOM db = new CDBCOM(EDBType.Access);
                string sqlCmd = "Select * from FlowInfo where FlowUsed=1 order by FlowID,FlowSub";
                DataSet ds=null;
                if (!db.QuerySQL(sqlCmd, out ds, out er))
                    return false;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    flowGUID.Add(ds.Tables[0].Rows[i]["FlowGUID"].ToString());
                    flowName.Add(ds.Tables[0].Rows[i]["FlowDes"].ToString());
                    iconKey.Add(System.Convert.ToInt32(ds.Tables[0].Rows[i]["FlowIcon"].ToString()) - 1);  
                }
                return true; 
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false; 
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if(listFlow.SelectedItems.Count!=0)
            {
                if (MessageBox.Show(CLanguage.Lan("确定要加载该测试工位") + "[" + listFlow.SelectedItems[0].Text + "]?", CLanguage.Lan("选择当前测试工位"),
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    string er = string.Empty; 
                    int idNo = listFlow.SelectedItems[0].Index;
                    if (!CGlobal.CFlow.getFlowInfo(listFlow.Items[idNo].ToolTipText, ref er))
                    {
                        MessageBox.Show(er);  
                    }
                    this.DialogResult = DialogResult.OK;
                }              
            }            
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        #region 语言切换
        /// <summary>
        /// 加载中英文界面
        /// </summary>
        private void SetLanguage()
        {

            switch (CLanguage.languageType)
            {
                case CLanguage.EL.中文:
                    this.Text = "选择当前测试工位";
                    break;
                case CLanguage.EL.英语:
                    this.Text = "Select the current test station";
                    btnCancel.Text = CLanguage.Lan("Exit(&C)");
                    btnOK.Text = CLanguage.Lan("OK(&O)");

                    break;
                case CLanguage.EL.繁体:
                    this.Text = CLanguage.Lan("选择当前测试工位");
                    btnCancel.Text = CLanguage.Lan(" 取消(&C)");
                    btnOK.Text = CLanguage.Lan(" 确定(&O)");
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
