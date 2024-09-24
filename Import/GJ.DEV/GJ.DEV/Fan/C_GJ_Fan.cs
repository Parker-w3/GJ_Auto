using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace GJ.DEV.Fan
{
    public class C_GJ_Fan
    {
        #region 字段

        private int comDataType = 0;

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
        /// <param name="rs232">串口</param>
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

                wByteLen = wData.Length / 2;
                wByte = new byte[wByteLen];
                for (int i = 0; i < wByteLen; i++)
                    wByte[i] = System.Convert.ToByte(wData.Substring(i * 2, 2), 16);

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

                for (int i = 0; i < rByteLen; i++)
                    rData += rByte[i].ToString("X2");

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

        #endregion

        #region 方法

        /// <summary>
        /// 设定单个通道的风扇占空比
        /// </summary>
        /// <param name="iAdrs"></param>
        /// <param name="wVal"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool SetSigDutyCyc(int iAdrs, int[] wVal, out string er)
        {
            er = string.Empty;
            try
            {
                string cmd = string.Empty;
                bool setOk = false;
                string wData = string.Empty;
                for (int i = 0; i < wVal.Length; i++)
                    wData += wVal[i].ToString("X2");
                cmd = "01010F02" ;
                
                wData = CalDataFromMon32Cmd(iAdrs, cmd, wData);
                string rData = string.Empty;
                int rLen = 6;
                for (int i = 0; i < 3; i++)
                    if (SendCmdToMon32(wData, rLen, out rData, out er))

                        if ((rData.Substring(4, 2) == "01") && rData.Length >= 6)
                        {
                            setOk = true;
                            break;
                        }
                        else
                            er = rData;

                if (!setOk)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }


        #endregion

        #region 协议
        /* 
       * 发送:桢头(FD)+地址+命令01+命令02+长度+数据+检验和+桢尾(FE)
       * 应答:桢头(FD)+地址+长度+数据+检验和+桢尾(FE)         
      */
        private string SOI = "FE";
        private string EOI = "FF";

        /// <summary>
        /// 发串口信息
        /// </summary>
        /// <param name="wData"></param>
        /// <param name="rLen"></param>
        /// <param name="rData"></param>
        /// <param name="er"></param>
        /// <param name="wTimeOut"></param>
        /// <returns></returns>
        private bool SendCmdToMon32(string wData, int rLen, out string rData, out string er, int wTimeOut = 200)
        {
            rData = string.Empty;
            er = string.Empty;
            try
            {
                string recvData = string.Empty;
                if (!send(wData, rLen, out recvData, out er, wTimeOut))
                    return false;
                if (rLen > 0)
                {
                    if (!CheckSum(recvData, ref er))
                        return false;
                    rData = recvData.Substring(2, recvData.Length - 6);
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 数据格式化
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="wCmd"></param>
        /// <param name="wData"></param>
        /// <returns></returns>
        private string CalDataFromMon32Cmd(int wAddr, string wCmd, string wData)
        {
            string cmd = string.Empty;
            int len = 4 + wData.Length / 2;
            string chkData = string.Empty;
            for (int i = 0; i < wData.Length / 2; i++)
            {
                if (wData.Substring(i * 2, 2) == SOI || wData.Substring(i * 2, 2) == EOI)
                {
                    if (wData.Substring(i * 2, 2) == SOI)
                        chkData += (Convert.ToInt16(SOI, 16) - 2).ToString("X2");
                    if (wData.Substring(i * 2, 2) == EOI)
                        chkData += (Convert.ToInt16(EOI, 16) - 2).ToString("X2");
                }
                else
                    chkData += wData.Substring(i * 2, 2);
            }
            cmd = wAddr.ToString("X2") + wCmd + chkData;
            cmd = SOI + cmd + CalCheckSum(cmd) + EOI;
            return cmd;
        }
        /// <summary>
        /// 检验和
        /// </summary>
        /// <param name="wData"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool CheckSum(string wData, ref string er)
        {
            if (wData.Substring(0, 2) != SOI )
            {
                er = "数据桢头错误:" + wData;
                return false;
            }
            if (wData.Substring(wData.Length - 2, 2) != EOI)
            {
                er = "数据桢尾错误:" + wData;
                return false;
            }
            if (wData.Length / 2 < 6)
            {
                er = "数据长度小于6:" + wData;
                return false;
            }
            //int rLen = System.Convert.ToInt16(wData.Substring(8, 2), 16);
            //if ((wData.Length / 2) != (rLen + 4))
            //{
            //    er = "数据长度错误:" + wData;
            //    return false;
            //}
            string chkStr = wData.Substring(2, wData.Length - 6);
            string chkSum = wData.Substring(wData.Length - 4, 2);
            if (chkSum != CalCheckSum(chkStr))
            {
                er = "数据CheckSum错误:" + wData;
                return false;
            }
            return true;
        }
        /// <summary>
        /// 检验和-(地址+命令01+命令02+长度+数据)%256
        /// </summary>
        /// <param name="wData"></param>
        /// <returns></returns>
        private string CalCheckSum(string wData)
        {
            int sum = 0;
            for (int i = 0; i < wData.Length / 2; i++)
            {
                sum += System.Convert.ToInt16(wData.Substring(i * 2, 2), 16);

                sum = sum % 256;
            }
            //0   1   2   3   4   5   6   7
            //0XFE    ADR Command1    Command2    Data_Long   Data    ChkSum  0XFF
            //FE 00 00 00 05 01 06 FF

            string checkSum = sum.ToString("X2");
            if (checkSum.Substring(0, 2) == SOI || checkSum.Substring(0, 2) == EOI)
                checkSum = (Convert.ToInt16(SOI, 16) - 2).ToString("X2") + checkSum.Substring(2, 2);

            return checkSum;

        }
        #endregion


    }
}
