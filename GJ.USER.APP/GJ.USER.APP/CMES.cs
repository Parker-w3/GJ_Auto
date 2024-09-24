using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using System.Data;
using System.Windows.Forms;
using System.Threading; 

namespace GJ.USER.APP
{
    public class CMES
    {
        #region 类定义
        /// <summary>
        /// 上传数据结果
        /// </summary>
        public class CXml_Info
        {
            public string IntSerial;
            public string Process;
            public string Result;
            public string OperatorName;
            public string Tester;
            public string ProgramName;
            public string ProgramVersion;
            public string IPSNo;
            public string IPSVersion;
            public string Remark;

            public int Item;
            public int TestStep;
            public string TestName;
            public string OutputName;
            public string InputCondition;
            public string OutputLoading;
            public double LowerLimit;
            public double UpperLimit;
            public string TestData;
            public string Unit;
            public string Status;
            public string IPSReference;
            public string TestID;
            public string StartTime;
            public string EndTime;
            public string BurnTime;
        }
        #endregion

        #region 字段
        /// <summary>
        /// Web Service 网址
        /// </summary>
        public static string WebAddr = "http://cnaecfuyapp20/eTrace_OracleERP/eTraceOracleERP.asmx";
        /// <summary>
        /// 产品数量
        /// </summary>
        public static int SlotMax = 20;
        /// <summary>
        /// 同步锁
        /// </summary>
        private static ReaderWriterLock webLock = new ReaderWriterLock();
        #endregion

        #region 共享方法
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="AccessCardID"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool Employee_Login(string AccessCardID,out string er)
        {
            er = string.Empty;

            try
            {
                webLock.AcquireWriterLock(-1); 

                string requestName = "Employee_Login";

                string reponseXml = string.Empty;

                string webXml = string.Empty;

                webXml += "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + "\r\n";
                webXml += "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance" +
                          "\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema" + "\" xmlns:soap=\"" +
                          "http://schemas.xmlsoap.org/soap/envelope/" + "\">" + "\r\n";
                webXml += "<soap:Body>" + "\r\n";
                webXml += "<" + requestName + " xmlns=\"" + "http://eTraceOracleERP.org/" + "\">" + "\r\n";
                webXml += "<AccessCardID>" + AccessCardID + "</AccessCardID>" + "\r\n";
                webXml += "</" + requestName + ">" + "\r\n";
                webXml += "</soap:Body>" + "\r\n";
                webXml += "</soap:Envelope>";

                if (!PostMessageToHttp(WebAddr, webXml, out reponseXml, out er))
                    return false;

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(reponseXml);

                string reposeInfo = string.Empty;

                XmlElement rotElement = doc.DocumentElement;

                XmlNodeList xnl = rotElement.ChildNodes;

                foreach (XmlNode node in xnl)
                {
                    XmlElement xe = (XmlElement)node;

                    reposeInfo = xe.InnerText;
                }

                DataSet ds = GetDataSetByXml(reposeInfo);

                if (ds == null)
                {
                    er = "工号[" + AccessCardID + "]不存在:" + reposeInfo;
                    return false;
                }

                if (ds.DataSetName !="NewDataSet")
                {
                    er = "工号[" + AccessCardID + "]不存在:" + reposeInfo;
                    return false;
                }

                string EmployeeID = ds.Tables[0].Rows[0]["EmployeeID"].ToString(); 

                string name = ds.Tables[0].Rows[0]["Name"].ToString();   

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                webLock.ReleaseWriterLock();
            }
        }
        /// <summary>
        /// 检查治具ID与测试机型类型是否配对
        /// </summary>
        /// <param name="FixtureID"></param>
        /// <param name="FixtureType"></param>
        /// <returns></returns>
        public static bool ATE_FixtureVerify(string FixtureID, string FixtureType,out string er)
        {
            er = string.Empty;

            try
            {
                webLock.AcquireWriterLock(-1); 

                FixtureID = FixtureID.Substring(FixtureID.Length - 8, 8); 

                string requestName = "ATE_FixtureVerify";

                string reponseXml = string.Empty;

                string webXml = string.Empty;

                webXml += "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + "\r\n";
                webXml += "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance" +
                          "\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema" + "\" xmlns:soap=\"" +
                          "http://schemas.xmlsoap.org/soap/envelope/" + "\">" + "\r\n";
                webXml += "<soap:Body>" + "\r\n";
                webXml += "<" + requestName + " xmlns=\"" + "http://eTraceOracleERP.org/" + "\">" + "\r\n";
                webXml += "<FixtureID>" + FixtureID + "</FixtureID>" + "\r\n";
                webXml += "<Type>" + FixtureType + "</Type>" + "\r\n";
                webXml += "</" + requestName + ">" + "\r\n";
                webXml += "</soap:Body>" + "\r\n";
                webXml += "</soap:Envelope>";

                if (!PostMessageToHttp(WebAddr, webXml, out reponseXml, out er))
                    return false;

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(reponseXml);

                string reposeInfo = string.Empty;

                XmlElement rotElement = doc.DocumentElement;

                XmlNodeList xnl = rotElement.ChildNodes;

                foreach (XmlNode node in xnl)
                {
                    XmlElement xe = (XmlElement)node;

                    reposeInfo = xe.InnerText;
                }

                switch (reposeInfo)
                {
                    case "0":
                        er = "Type is incorrect";
                        break;
                    case "1":
                        er = "Warning";
                        break;
                    case "2":
                        er = "Exceel the upper limit of quantity";
                        break;
                    case "3":
                        er = "The fixture is Fail";
                        break;
                    case "4":
                        er = "The fixture not be register";
                        break;
                    case "5":
                        er = "Pass for verify";
                        return true;
                    case "6":
                        er = "The fixture still be used, not be release";
                        break;
                    case "7":
                        er = "The fixture be scrapped";
                        break;
                    default:
                        er = "The Error Code=" + reposeInfo;
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                webLock.ReleaseWriterLock();
            }
        }
        /// <summary>
        /// 删除建立关系：将序列号跟机架位对应上
        /// </summary>
        /// <param name="FixtureID"></param>
        /// <param name="User"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool ATE_ReleaseRelationbyFixture(string FixtureID, string User, out string er)
        {
            er = string.Empty;

            try
            {
                webLock.AcquireWriterLock(-1); 

                FixtureID = FixtureID.Substring(FixtureID.Length - 8, 8); 

                string requestName = "ATE_ReleaseRelationbyFixture";

                string reponseXml = string.Empty;

                string webXml = string.Empty;

                webXml += "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + "\r\n";
                webXml += "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance" +
                          "\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema" + "\" xmlns:soap=\"" +
                          "http://schemas.xmlsoap.org/soap/envelope/" + "\">" + "\r\n";
                webXml += "<soap:Body>" + "\r\n";
                webXml += "<" + requestName + " xmlns=\"" + "http://eTraceOracleERP.org/" + "\">" + "\r\n";
                webXml += "<FixtureID>" + FixtureID + "</FixtureID>" + "\r\n";
                webXml += "<User>" + User + "</User>" + "\r\n";
                webXml += "</" + requestName + ">" + "\r\n";
                webXml += "</soap:Body>" + "\r\n";
                webXml += "</soap:Envelope>";

                if (!PostMessageToHttp(WebAddr, webXml, out reponseXml, out er))
                    return false;

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(reponseXml);

                string reposeInfo = string.Empty;

                XmlElement rotElement = doc.DocumentElement;

                XmlNodeList xnl = rotElement.ChildNodes;

                foreach (XmlNode node in xnl)
                {
                    XmlElement xe = (XmlElement)node;

                    reposeInfo = xe.InnerText;
                }

                if (reposeInfo.ToUpper() != "TRUE")
                {
                    er = reposeInfo;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                webLock.ReleaseWriterLock();
            }
        }
        /// <summary>
        ///  删除建立关系:将序列号跟机架位对应上
        /// </summary>
        /// <param name="FixtureID"></param>
        /// <param name="SlotNo"></param>
        /// <param name="User"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool ATE_ReleaseRelationbySlot(string FixtureID,int SlotNo, string User, out string er)
        {
            er = string.Empty;

            try
            {
                webLock.AcquireWriterLock(-1); 

                FixtureID = FixtureID.Substring(FixtureID.Length - 8, 8); 

                string requestName = "ATE_ReleaseRelationbySlot";

                string reponseXml = string.Empty;

                string webXml = string.Empty;

                webXml += "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + "\r\n";
                webXml += "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance" +
                          "\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema" + "\" xmlns:soap=\"" +
                          "http://schemas.xmlsoap.org/soap/envelope/" + "\">" + "\r\n";
                webXml += "<soap:Body>" + "\r\n";
                webXml += "<" + requestName + " xmlns=\"" + "http://eTraceOracleERP.org/" + "\">" + "\r\n";
                webXml += "<FixtureID>" + FixtureID + "</FixtureID>" + "\r\n";
                webXml += "<Slot>" + SlotNo.ToString() + "</Slot>" + "\r\n";
                webXml += "<User>" + User + "</User>" + "\r\n";
                webXml += "</" + requestName + ">" + "\r\n";
                webXml += "</soap:Body>" + "\r\n";
                webXml += "</soap:Envelope>";

                if (!PostMessageToHttp(WebAddr, webXml, out reponseXml, out er))
                    return false;

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(reponseXml);

                string reposeInfo = string.Empty;

                XmlElement rotElement = doc.DocumentElement;

                XmlNodeList xnl = rotElement.ChildNodes;

                foreach (XmlNode node in xnl)
                {
                    XmlElement xe = (XmlElement)node;

                    reposeInfo = xe.InnerText;
                }

                if (reposeInfo.ToUpper() != "TRUE")
                {
                    er = reposeInfo;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                webLock.ReleaseWriterLock();
            }
        }
        /// <summary>
        /// 建立关系，将序列号跟机架位对应上
        /// </summary>
        /// <param name="FixtureID">设备编号</param>
        /// <param name="SlotNo">设备单元位数</param>
        /// <param name="Model">产品型号</param>
        /// <param name="IntSN">序列号</param>
        /// <param name="Process">流程控制位</param>
        /// <param name="User">操作员</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool ATE_CreateRelation(string FixtureID, int SlotNo, string Model, string IntSN,string Process, string User,out string er)
        {
            er = string.Empty;

            try
            {
                webLock.AcquireWriterLock(-1); 

                FixtureID = FixtureID.Substring(FixtureID.Length - 8, 8); 

                string requestName = "ATE_CreateRelation";

                string reponseXml = string.Empty;

                string webXml = string.Empty;

                webXml += "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + "\r\n";
                webXml += "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance" +
                          "\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema" + "\" xmlns:soap=\"" +
                          "http://schemas.xmlsoap.org/soap/envelope/" + "\">" + "\r\n";
                webXml += "<soap:Body>" + "\r\n";
                webXml += "<" + requestName + " xmlns=\"" + "http://eTraceOracleERP.org/" + "\">" + "\r\n";
                webXml += "<FixtureID>" + FixtureID + "</FixtureID>" + "\r\n";
                webXml += "<Slot>" + SlotNo.ToString() + "</Slot>" + "\r\n";
                webXml += "<Model>" + Model + "</Model>" + "\r\n";
                webXml += "<IntSN>" + IntSN + "</IntSN>" + "\r\n";
                webXml += "<Process>" + Process + "</Process>" + "\r\n";
                webXml += "<User>" + User + "</User>" + "\r\n";
                webXml += "</" + requestName + ">" + "\r\n";
                webXml += "</soap:Body>" + "\r\n";
                webXml += "</soap:Envelope>";

                if (!PostMessageToHttp(WebAddr, webXml, out reponseXml, out er))
                    return false;

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(reponseXml);

                string reposeInfo = string.Empty;

                XmlElement rotElement = doc.DocumentElement;

                XmlNodeList xnl = rotElement.ChildNodes;

                foreach (XmlNode node in xnl)
                {
                    XmlElement xe = (XmlElement)node;

                    reposeInfo = xe.InnerText;
                }

                switch (reposeInfo)
                {
                    case "0":
                        er = "Fail to create";
                        break;
                    case "1":
                        er = "FixtureID + slot not exist";
                        break;
                    case "2":
                        er = "Slot not be released";
                        break;
                    case "3":
                        er = "Relation has been created for the Serial";
                        break;
                    case "4":
                        er = "Model is incorrect";
                        break;
                    case "5":
                        er = "Duplicated SN";
                        break;
                    case "6":
                        er = "Fail in last process";
                        break;
                    case "7":
                        er = "OK";
                        return true;
                    case "8":
                        er = "Process can’t find";
                        break;
                    case "9":
                        er = "The SN be used another process";
                        break;
                    default:
                        er = "The Error Code=" + reposeInfo;
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                webLock.ReleaseWriterLock();
            }
        }
        /// <summary>
        /// 查询治具上产品条码
        /// </summary>
        /// <param name="FixtureID"></param>
        /// <param name="serialNo"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool ATE_ReturnSNbyFixture(string FixtureID, out List<string> serialNos, out string er)
        {
            serialNos = new List<string>();

            er = string.Empty;

            try
            {
                webLock.AcquireWriterLock(-1); 

                FixtureID = FixtureID.Substring(FixtureID.Length - 8, 8); 

                string requestName = "ATE_ReturnSNbyFixture";

                string reponseXml = string.Empty;

                string webXml = string.Empty;

                webXml += "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + "\r\n";
                webXml += "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance" +
                          "\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema" + "\" xmlns:soap=\"" +
                          "http://schemas.xmlsoap.org/soap/envelope/" + "\">" + "\r\n";
                webXml += "<soap:Body>" + "\r\n";
                webXml += "<" + requestName + " xmlns=\"" + "http://eTraceOracleERP.org/" + "\">" + "\r\n";
                webXml += "<FixtureID>" + FixtureID + "</FixtureID>" + "\r\n";
                webXml += "</" + requestName + ">" + "\r\n";
                webXml += "</soap:Body>" + "\r\n";
                webXml += "</soap:Envelope>";

                if (!PostMessageToHttp(WebAddr, webXml, out reponseXml, out er))
                    return false;

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(reponseXml);

                string reposeTable = string.Empty;
                XmlElement rotElement = doc.DocumentElement;
                XmlNodeList xnl = rotElement.ChildNodes;
                foreach (XmlNode node in xnl)
                {
                    XmlElement xe = (XmlElement)node;
                    reposeTable = xe.InnerXml;
                }

                reposeTable = reposeTable.Replace("&lt;", "<");
                reposeTable = reposeTable.Replace("&gt;", ">");
                reposeTable = reposeTable.Replace("&quot;", "\"");
                reposeTable = reposeTable.Replace("&nbsp;", " ");

                DataSet ds = GetDataSetByXml(reposeTable);

                if (ds == null)
                {
                    er = "治具ID[" + FixtureID + "]获取信息错误:[" + reposeTable + "]";
                    return false;
                }

                if (!ds.Tables.Contains("Table1"))
                {
                    er = "治具ID[" + FixtureID + "]获取信息错误:[" + reposeTable + "]";
                    return false;
                }

                for (int i = 0; i < SlotMax; i++)
                    serialNos.Add("");

                for (int i = 0; i < ds.Tables["Table1"].Rows.Count; i++)
                {
                    int slotNum = System.Convert.ToInt16(ds.Tables["Table1"].Rows[i]["SlotNum"].ToString());
                    serialNos[slotNum - 1] = ds.Tables["Table1"].Rows[i]["IntSerialNo"].ToString();
                }
                
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                webLock.ReleaseWriterLock();
            }
        }
        /// <summary>
        /// 查询产品是否到该测试站
        /// </summary>
        /// <param name="IntSN"></param>
        /// <param name="Process"></param>
        /// <param name="User"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool ATEWIPIn(string IntSN, string Process, string User, out string er)
        {
            er = string.Empty;

            try
            {
                webLock.AcquireWriterLock(-1); 

                string requestName = "ATEWIPIn";

                string reponseXml = string.Empty;

                string webXml = string.Empty;

                webXml += "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + "\r\n";
                webXml += "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance" +
                          "\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema" + "\" xmlns:soap=\"" +
                          "http://schemas.xmlsoap.org/soap/envelope/" + "\">" + "\r\n";
                webXml += "<soap:Body>" + "\r\n";
                webXml += "<" + requestName + " xmlns=\"" + "http://eTraceOracleERP.org/" + "\">" + "\r\n";
                webXml += "<IntSerialNo>" + IntSN + "</IntSerialNo>" + "\r\n";
                webXml += "<Process>" + Process + "</Process>" + "\r\n";
                webXml += "<User>" + User + "</User>" + "\r\n";
                webXml += "</" + requestName + ">" + "\r\n";
                webXml += "</soap:Body>" + "\r\n";
                webXml += "</soap:Envelope>";

                if (!PostMessageToHttp(WebAddr, webXml, out reponseXml, out er))
                    return false;

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(reponseXml);

                string reposeInfo = string.Empty;

                XmlElement rotElement = doc.DocumentElement;

                XmlNodeList xnl = rotElement.ChildNodes;

                foreach (XmlNode node in xnl)
                {
                    XmlElement xe = (XmlElement)node;

                    reposeInfo = xe.InnerText;
                }

                if(reposeInfo.ToUpper()!="PASS")  
                   return false;

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                webLock.ReleaseWriterLock();
            }
        }
        /// <summary>
        /// 上传测试结果
        /// </summary>
        /// <param name="xml_Info"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool ATEWIPout(CXml_Info xml_Info, out string er)
        {
            er = string.Empty;

            try
            {
                webLock.AcquireWriterLock(-1); 

                string requestName = "ATEWIPout";

                string strXML = formatXmlInfo(xml_Info);

                strXML = strXML.Replace("<", "&lt;");
                strXML = strXML.Replace(">", "&gt;");
                strXML = strXML.Replace("\"", "&quot;");

                string reponseXml = string.Empty;

                string webXml = string.Empty;

                webXml += "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + "\r\n";
                webXml += "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance" +
                          "\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema" + "\" xmlns:soap=\"" +
                          "http://schemas.xmlsoap.org/soap/envelope/" + "\">" + "\r\n";
                webXml += "<soap:Body>" + "\r\n";
                webXml += "<" + requestName + " xmlns=\"" + "http://eTraceOracleERP.org/" + "\">" + "\r\n";
                webXml += "<xml>" + strXML + "</xml>" + "\r\n";
                webXml += "</" + requestName + ">" + "\r\n";
                webXml += "</soap:Body>" + "\r\n";
                webXml += "</soap:Envelope>";


                if (!PostMessageToHttp(WebAddr, webXml, out reponseXml, out er))
                    return false;

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(reponseXml);

                string reposeInfo = string.Empty;

                XmlElement rotElement = doc.DocumentElement;

                XmlNodeList xnl = rotElement.ChildNodes;

                foreach (XmlNode node in xnl)
                {
                    XmlElement xe = (XmlElement)node;

                    reposeInfo = xe.InnerText;
                }

                if (reposeInfo != "PASS")
                {
                    er = reposeInfo;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                webLock.ReleaseWriterLock();
            }
        }
        /// <summary>
        /// 上传测试结果
        /// </summary>
        /// <param name="xml_Info"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool ATEWIPout(string strXML, out string er)
        {
            er = string.Empty;

            try
            {
                webLock.AcquireWriterLock(-1); 

                string requestName = "ATEWIPout";

                string reponseXml = string.Empty;

                string webXml = string.Empty;

                strXML = strXML.Replace("<", "&lt;");
                strXML = strXML.Replace(">", "&gt;");
                strXML = strXML.Replace("\"", "&quot;");

                webXml += "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + "\r\n";
                webXml += "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance" +
                          "\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema" + "\" xmlns:soap=\"" +
                          "http://schemas.xmlsoap.org/soap/envelope/" + "\">" + "\r\n";
                webXml += "<soap:Body>" + "\r\n";
                webXml += "<" + requestName + " xmlns=\"" + "http://eTraceOracleERP.org/" + "\">" + "\r\n";
                webXml += "<xml>" + strXML + "</xml>" + "\r\n";
                webXml += "</" + requestName + ">" + "\r\n";
                webXml += "</soap:Body>" + "\r\n";
                webXml += "</soap:Envelope>";

                if (!PostMessageToHttp(WebAddr, webXml, out reponseXml, out er))
                    return false;

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(reponseXml);

                string reposeInfo = string.Empty;

                XmlElement rotElement = doc.DocumentElement;

                XmlNodeList xnl = rotElement.ChildNodes;

                foreach (XmlNode node in xnl)
                {
                    XmlElement xe = (XmlElement)node;

                    reposeInfo = xe.InnerText;
                }

                if (reposeInfo != "PASS")
                {
                    er = reposeInfo;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                webLock.ReleaseWriterLock();
            }
        }
        /// <summary>
        /// 查询产品是否到该测试站
        /// </summary>
        /// <param name="IntSN"></param>
        /// <param name="Process"></param>
        /// <param name="User"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool GetLastTestResult(string IntSN, string Process, out string er)
        {
            er = string.Empty;

            try
            {
                webLock.AcquireWriterLock(-1); 

                string requestName = "GetLastTestResult";

                string reponseXml = string.Empty;

                string webXml = string.Empty;

                webXml += "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + "\r\n";
                webXml += "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance" +
                          "\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema" + "\" xmlns:soap=\"" +
                          "http://schemas.xmlsoap.org/soap/envelope/" + "\">" + "\r\n";
                webXml += "<soap:Body>" + "\r\n";
                webXml += "<" + requestName + " xmlns=\"" + "http://eTraceOracleERP.org/" + "\">" + "\r\n";
                webXml += "<intSN>" + IntSN + "</intSN>" + "\r\n";
                webXml += "<ProcessName>" + Process + "</ProcessName>" + "\r\n";
                webXml += "</" + requestName + ">" + "\r\n";
                webXml += "</soap:Body>" + "\r\n";
                webXml += "</soap:Envelope>";

                if (!PostMessageToHttp(WebAddr, webXml, out reponseXml, out er))
                    return false;

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(reponseXml);

                string reposeInfo = string.Empty;

                XmlElement rotElement = doc.DocumentElement;

                XmlNodeList xnl = rotElement.ChildNodes;

                foreach (XmlNode node in xnl)
                {
                    XmlElement xe = (XmlElement)node;

                    reposeInfo = xe.InnerText;
                }

                if (reposeInfo != "PASS")
                {
                    er = reposeInfo;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                webLock.ReleaseWriterLock();
            }
        }
        /// <summary>
        /// 保存上传失败数据
        /// </summary>
        /// <param name="xml_Info"></param>
        public static bool SaveXmlFail(CXml_Info xml_Info, out string filePath, out string er)
        {
            er = string.Empty;

            filePath = string.Empty;

            try
            {
                webLock.AcquireWriterLock(-1);

                string fileFolder = Application.StartupPath + "\\SnXml\\" + DateTime.Now.ToString("yyyyMMdd");

                string fileName = xml_Info.IntSerial + ".xml";

                filePath = fileFolder + "\\" + fileName;

                string strXML = formatXmlInfo(xml_Info);

                if (!Directory.Exists(fileFolder))
                    Directory.CreateDirectory(fileFolder);

                StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default);

                sw.Write(strXML);

                sw.Flush();

                sw.Close();

                sw = null;

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                webLock.ReleaseWriterLock(); 
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 向HTTP发消息和接收消息
        /// </summary>
        /// <param name="requestXml"></param>
        /// <param name="reponseXml"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private static bool PostMessageToHttp(string ulrWebAddr, string requestXml, out string reponseXml, out string er)
        {
            reponseXml = string.Empty;

            er = string.Empty;

            try
            {
                //发送数据转换为Bytes
                byte[] sendByte = System.Text.Encoding.Default.GetBytes(requestXml);  
                
                //发送HTTP的POST请求
                HttpWebRequest request = (HttpWebRequest)(HttpWebRequest.Create(ulrWebAddr)); 
                //Post请求方式
                request.Method = "POST";
                //内容类型
                request.ContentType = "text/xml; charset=utf-8";
                //设置请求的 ContentLength
                request.ContentLength = sendByte.Length;
                //获得请求流
                Stream fs = request.GetRequestStream();
                fs.Write(sendByte, 0, sendByte.Length);
                fs.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream s = response.GetResponseStream();
                XmlTextReader xmlReader = new XmlTextReader(s);
                xmlReader.MoveToContent();
                string recv = xmlReader.ReadInnerXml();
                //recv = recv.Replace("&amp;", "&");
                //recv = recv.Replace("&lt;", "<");
                //recv = recv.Replace("&gt;", ">");
                //recv = recv.Replace("&apos;", "'");
                //recv = recv.Replace("&quot;", "\""); //((char)34) 双引号
                reponseXml = recv;
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// xml->DataSet
        /// </summary>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        private static DataSet GetDataSetByXml(string xmlData)
        {
            try
            {
                DataSet ds = new DataSet();
                using (StringReader xmlSR = new StringReader(xmlData))
                {
                    ds.ReadXml(xmlSR, XmlReadMode.InferTypedSchema);
                    //忽视任何内联架构，从数据推断出强类型架构并加载数据,如果无法推断，则解释成字符串数据
                    if (ds.Tables.Count > 0)
                        return ds;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// xml上传字符串
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private static string formatXmlInfo(CXml_Info info)
        {

            string dsxml = string.Empty;

            dsxml = "<dsWIPTD>" + "\r\n";
            dsxml += "<WIPTDHeader>" + "\r\n";
            dsxml += "<IntSerial>" + info.IntSerial + "</IntSerial>" + "\r\n";
            dsxml += "<Process>" + info.Process + "</Process>" + "\r\n";
            dsxml += "<Result>" + info.Result + "</Result>" + "\r\n";
            dsxml += "<OperatorName>" + info.OperatorName + "</OperatorName>" + "\r\n";
            dsxml += "<Tester>" + info.Tester + "</Tester>" + "\r\n";
            dsxml += "<ProgramName>" + info.ProgramName + "</ProgramName>" + "\r\n";
            dsxml += "<ProgramVersion>" + info.ProgramName + "</ProgramVersion>" + "\r\n";
            dsxml += "<IPSNo>" + info.IPSNo + "</IPSNo>" + "\r\n";
            dsxml += "<IPSVersion>" + info.IPSVersion + "</IPSVersion>" + "\r\n";
            dsxml += "<Remark>" + info.Remark + "</Remark>" + "\r\n";
            dsxml += "</WIPTDHeader>" + "\r\n";

            dsxml += "<WIPTDItem>" + "\r\n";
            dsxml += "<Item>" + info.Item + "</Item>" + "\r\n";
            dsxml += "<TestStep>" + info.TestStep + "</TestStep>" + "\r\n";
            dsxml += "<TestName>" + info.TestName + "</TestName>" + "\r\n";
            dsxml += "<OutputName>" + info.OutputName + "</OutputName>" + "\r\n";
            dsxml += "<InputCondition>" + info.InputCondition + "</InputCondition>" + "\r\n";
            dsxml += "<OutputLoading>" + info.OutputLoading + "</OutputLoading>" + "\r\n";
            dsxml += "<LowerLimit>" + info.LowerLimit + "</LowerLimit>" + "\r\n";
            dsxml += "<Result>" + info.TestData + "</Result>" + "\r\n";
            dsxml += "<UpperLimit>" + info.UpperLimit + "</UpperLimit>" + "\r\n";
            dsxml += "<Unit>" + info.Unit + "</Unit>" + "\r\n";
            dsxml += "<Status>" + info.Status + "</Status>" + "\r\n";
            dsxml += "<IPSReference>" + info.IPSReference + "</IPSReference>" + "\r\n";
            dsxml += "<TestID>" + info.TestID + "</TestID>" + "\r\n";
            dsxml += "</WIPTDItem>" + "\r\n";
            dsxml += "</dsWIPTD>" + "\r\n";

            return dsxml;
        }
        #endregion
    }
}
