using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Threading;

namespace GJ.MES
{
    public class Oracle
    {

        #region 读写锁
        private ReaderWriterLock wrLock = new ReaderWriterLock();
        #endregion

        #region 字段
        private string serverName = "localhost";
        private string dbName = "suhcmes";
        private string userName = "mes_public";
        private string passWord = "mes_public";
        private OleDbConnection objConn = new OleDbConnection();
        #endregion

        #region 属性
        public string mServerName
        {
            set { serverName = value; }
        }
        public string mDbName
        {
            set { dbName = value; }
        }
        public string mUserName
        {
            set { userName = value; }
        }
        public string mPassWord
        {
            set { passWord = value; }
        }
        #endregion

        #region 方法
        public bool connect()
        {
            try
            {
                string extName = Path.GetExtension(dbName).ToLower();
                string provider = "Provider=MSDAORA;Data Source=" + dbName +
                                 ";Persist Security Info=True;User ID=" + userName +
                                 ";Password=" + passWord;
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.ConnectionString = provider;
                objConn.Open();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void close()
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }

        public bool queryCmd(string sqlCmd, ref System.Data.DataSet ds, ref string er)
        {
            try
            {
                wrLock.AcquireWriterLock(-1);
                if (!connect())
                    return false;
                OleDbDataAdapter adapter = new OleDbDataAdapter(sqlCmd, objConn);
                adapter.Fill(ds);
                adapter.Dispose();
                adapter = null;
                return true;
            }
            catch (Exception e)
            {
                er = e.ToString();
                return false;
            }
            finally
            {
                close();
                wrLock.ReleaseWriterLock();
            }
        }

        public bool excuteCmd(string sqlCmd, ref string er)
        {
            try
            {
                wrLock.AcquireWriterLock(-1);
                if (!connect())
                    return false;
                OleDbCommand objCmd = new OleDbCommand(sqlCmd, objConn);
                objCmd.ExecuteNonQuery();
                objCmd.Dispose();
                objCmd = null;
                return true;
            }
            catch (Exception e)
            {
                er = e.ToString();
                return false;
            }
            finally
            {
                close();
                wrLock.ReleaseWriterLock();
            }
        }

        public bool excuteArrayCmd(List<string> sqlCmd, ref string er)
        {
            try
            {
                wrLock.AcquireWriterLock(-1);
                er = string.Empty;
                if (!connect())
                    return false;
                for (int i = 0; i < sqlCmd.Count; i++)
                {
                    string cmd = sqlCmd[i];
                    OleDbCommand objCmd = new OleDbCommand(cmd, objConn);
                    objCmd.ExecuteNonQuery();
                    objCmd.Dispose();
                    objCmd = null;
                }
                return true;
            }
            catch (Exception e)
            {
                er = e.ToString();
                return false;
            }
            finally
            {
                close();
                wrLock.ReleaseWriterLock();
            }
        }
        /// <summary>
        /// sql语句需要主键
        /// </summary>
        /// <param name="sqlCmd"></param>
        /// <param name="dt"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool updateTableCmd(string sqlCmd, System.Data.DataTable dt, ref string er)
        {

            try
            {
                wrLock.AcquireWriterLock(-1);
                if (!connect())
                    return false;
                OleDbDataAdapter adapter = new OleDbDataAdapter(sqlCmd, objConn);
                OleDbCommandBuilder dbbuilder = new OleDbCommandBuilder(adapter);
                DataSet ds = new DataSet();
                string tableName = "newTable";
                adapter.Fill(ds, tableName);
                if (ds.Tables.Count == 0)
                {
                    ds.Dispose();
                    dbbuilder.Dispose();
                    adapter.Dispose();
                    throw new Exception("查询表单语句错误.");
                }
                object[] RowVal = new object[ds.Tables[tableName].Columns.Count];
                for (int iRow = 0; iRow < ds.Tables[tableName].Rows.Count; iRow++)
                {
                    if (iRow < dt.Rows.Count)
                    {
                        for (int iCol = 0; iCol < ds.Tables[tableName].Columns.Count; iCol++)
                        {
                            if (iCol < dt.Columns.Count)
                                RowVal[iCol] = dt.Rows[iRow].ItemArray[iCol];
                        }
                        ds.Tables[tableName].Rows[iRow].ItemArray = RowVal;
                    }
                }
                adapter.Update(ds, tableName);
                ds.Dispose();
                dbbuilder.Dispose();
                adapter.Dispose();
                ds = null;
                dbbuilder = null;
                adapter = null;
                return true;
            }
            catch (Exception e)
            {
                er = e.ToString();
                return false;
            }
            finally
            {
                close();
                wrLock.ReleaseWriterLock();
            }
        }

        public bool returnTableNames(ref string[] tableNames, ref string er)
        {
            try
            {
                wrLock.AcquireWriterLock(-1);
                if (!connect())
                    return false;
                DataTable dt = new DataTable();
                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new Object[] { null, null, null, "TABLE" });
                tableNames = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tableNames[i] = dt.Rows[i].ItemArray[2].ToString();
                }
                dt.Dispose();
                dt = null;
                return true;
            }
            catch (Exception e)
            {
                er = e.ToString();
                return false;
            }
            finally
            {
                close();
                wrLock.ReleaseWriterLock();
            }
        }

        #endregion
    }
}
