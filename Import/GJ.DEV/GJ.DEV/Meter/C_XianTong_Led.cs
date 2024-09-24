using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace GJ.DEV.Meter
{
    public class C_XianTong_Led
    {

        #region 字段

        private int comDataType = 1;

        private SerialPort rs232;

        #endregion

        #region 属性

        public int mComDataType
        {
            set { comDataType = value; }
        }

        #endregion

        #region 串口方法
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="comName">串口号</param>
        /// <param name="setting">9600,n,8,1</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool open(string comName, string setting, out string er)
        {
            try
            {
                er = string.Empty;
                string[] arrayPara = setting.Split(',');
                if (arrayPara.Length != 4)
                {
                    er = "Com port parameters set wrong";
                    return false;
                }
                int bandRate = System.Convert.ToInt32(arrayPara[0]);
                Parity parity = Parity.None;
                switch (arrayPara[1].ToUpper())
                {
                    case "O":
                        parity = Parity.Odd;
                        break;
                    case "E":
                        parity = Parity.Even;
                        break;
                    case "M":
                        parity = Parity.Mark;
                        break;
                    case "S":
                        parity = Parity.Space;
                        break;
                    default:
                        break;
                }
                int dataBit = System.Convert.ToInt16(arrayPara[2]);
                StopBits stopBits = StopBits.One;
                switch (arrayPara[3])
                {
                    case "0":
                        stopBits = StopBits.None;
                        break;
                    case "1.5":
                        stopBits = StopBits.OnePointFive;
                        break;
                    case "2":
                        stopBits = StopBits.Two;
                        break;
                    default:
                        break;
                }
                if (rs232 != null)
                {
                    if (rs232.IsOpen)
                        rs232.Close();
                    rs232 = null;
                }
                rs232 = new SerialPort(comName, bandRate, parity, dataBit, stopBits);
                rs232.Open();
                return true;
            }
            catch (Exception e)
            {
                er = e.ToString();
                return false;
            }
        }
        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <returns></returns>
        public bool close()
        {
            if (rs232 != null)
            {
                if (rs232.IsOpen)
                    rs232.Close();
                rs232 = null;
            }
            return true;
        }

        /// <summary>
        /// 发送数据及接收数据
        /// </summary>
        /// <param name="wData"></param>
        /// <param name="rLen"></param>
        /// <param name="rData"></param>
        /// <param name="er"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        private bool send(string wData, int rLen, out string rData, out string er, int timeOut = 500)
        {
            er = string.Empty;
            rData = string.Empty;
            try
            {
                if (rs232 == null)              //检测串口是否打开
                {
                    er = "Com is not open";
                    return false;
                }
                byte[] wByte = null;
                int wByteLen = 0;
                if (comDataType == 0)
                {
                    wByteLen = wData.Length / 2;
                    wByte = new byte[wByteLen];
                    for (int i = 0; i < wByteLen; i++)
                        wByte[i] = System.Convert.ToByte(wData.Substring(i * 2, 2), 16);
                }
                else
                {
                    wByteLen = System.Text.Encoding.Default.GetByteCount(wData);
                    wByte = new byte[wByteLen];
                    wByte = System.Text.Encoding.Default.GetBytes(wData);
                }
                rs232.DiscardInBuffer();
                rs232.DiscardOutBuffer();
                rs232.Write(wByte, 0, wByteLen);
                if (rLen == 0)
                    return true;
                int waitTime = Environment.TickCount;
                do
                {
                    System.Threading.Thread.Sleep(2);
                } while ((rs232.BytesToRead < rLen) && (Environment.TickCount - waitTime) < timeOut);
                if (rs232.BytesToRead == 0)             //接收数据超时
                {
                    er = "TimeOut";
                    return false;
                }
                int rByteLen = rs232.BytesToRead;
                byte[] rByte = new byte[rByteLen];
                rs232.Read(rByte, 0, rByteLen);
                if (comDataType == 0)
                {
                    for (int i = 0; i < rByteLen; i++)
                        rData += rByte[i].ToString("X2");
                }
                else
                    rData = System.Text.Encoding.Default.GetString(rByte);
                if (rByteLen != rLen)                   //接收数据长度错误
                {
                    er = "Data length error:" + rData;
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                er = e.ToString();
                return false;
            }
        }

        /// <summary>
        /// $（1Byte）+地址（3Byte）+：(1Byte)+时间(4Byte)+#(1Byte)
        /// </summary>
        /// <param name="devAddr">仪表地址3个字节 按ASCLL发送</param>
        /// <param name="wVal">数据4个字节 按ASCLL发送</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool Write(int devAddr, int wVal, out string er)
        {
            er = string.Empty;

            try
            {

                int rLen = 0;

                string wData = string.Empty;

                wData += "$";       //$
                wData += devAddr.ToString("D3");
                wData += ":";       //:
                wData += wVal.ToString("D4");
                wData += "#";       //#

                string rData = string.Empty;
                rLen = 0;
                if (!send(wData, rLen, out rData, out er))
                    return false;

                return true;
            }
            catch (Exception e)
            {
                er = e.ToString();
                return false;
            }
        }


        #endregion

        #region 方法
        /// <summary>
        /// 写入时间
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="setData"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool WTime(int wAddr, int setData, out string er)
        {
            er = string.Empty;

            try
            {
                bool setok = false;

                for (int j = 0; j < 3; j++)
                {
                    if (Write(wAddr, setData, out er))
                    {
                        setok = true;
                        break;
                    }
                }
                if (!setok)
                {
                    er = "写入表头地址" + wAddr.ToString() + "失败__" + er;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }

        #endregion

    }
}
