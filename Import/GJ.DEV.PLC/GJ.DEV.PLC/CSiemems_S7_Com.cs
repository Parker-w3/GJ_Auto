using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace GJ.DEV.PLC
{
    public class CSiemems_S7_Com
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

        /// <summary>
        /// 读线圈03
        /// 从机地址(1Byte)+功能码(1Byte)+寄存器地址(2Byte)+地址数量(2Byte)+CRC检验(2Byte)
        /// </summary>
        /// <param name="devType">地址类型</param>
        /// <param name="regAddr">开始地址</param>
        /// <param name="N">地址长度</param>
        /// <param name="rData">16进制字符:数据值高位在前,低位在后</param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool Read(int devAddr, int regAddr, int N, out string rData, out string er)
        {
            rData = string.Empty;

            er = string.Empty;

            try
            {
                string wCmd = devAddr.ToString("X2");
                int rLen = 0;


                wCmd += "03";      //寄存器功能码为03
                rLen = N * 2;

                wCmd += regAddr.ToString("X4");  //开始地址
                wCmd += N.ToString("X4");        //读地址长度---2Byte

                wCmd += Crc16(wCmd);             //CRC16 低位前,高位后     

                if (!send(wCmd, 5 + rLen, out rData, out er))
                    return false;

                if (!checkCRC(rData))
                {
                    er = "crc16检验和错误" + ":" + rData;
                    return false;
                }
                string temp = rData.Substring(6, rLen * 2);
                rData = temp;     //2个字节为寄存器值，高在前,低位在后，寄存器小排最前面；
                //转换为寄存器小排最后
                rData = string.Empty;
                for (int i = 0; i < temp.Length / 4; i++)
                {
                    rData = temp.Substring(i * 4, 4) + rData;
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
        /// 单写线圈和寄存器值06
        /// 从机地址(1Byte)+功能码(1Byte)+寄存器地址(2Byte)+数据(2Byte)+CRC检验(2Byte)
        /// </summary>
        /// <param name="devType">地址类型</param>
        /// <param name="regAddr">开始地址</param>
        /// <param name="wVal">单个值</param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool Write(int devAddr, int regAddr, int wVal, out string rData, out string er)
        {
            er = string.Empty;
            rData = string.Empty;
            try
            {
                int N = 1;   //单写1个值
                string wCmd = devAddr.ToString("X2");
                int rLen = 0;
                int wLen = 0;
                string wData = string.Empty;

                wCmd += "06";       //寄存器功能码为16
                wLen = N * 2;       //写入字节数
                rLen = 8;           //回读长度

                wCmd += regAddr.ToString("X4");         //开始地址

                wData = wVal.ToString("X" + wLen * 2);
                wCmd += wData;                          //写入数据

                wCmd += Crc16(wCmd);                    //CRC16 低位前,高位后 
  
                rLen = wCmd.Length/2;                   //返回长度

                if (!send(wCmd, rLen, out rData, out er))
                    return false;
                if (!checkCRC(rData))
                {
                    er = "crc16检验和错误" + ":" + rData;
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
        /// 写入单个VW地址数据
        /// </summary>
        /// <param name="plcAddr">PLC地址</param>
        /// <param name="wAddr">控制地址</param>
        /// <param name="Data">写入数据</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool WtAddr(int wAddr, int regAdrs, int Data, out string er)
        {
            er = string.Empty;

            try
            {
                bool setok = false;
                string rData = string.Empty;
                for (int j = 0; j < 3; j++)
                {
                    if (Write(wAddr, regAdrs, Data, out rData, out er))
                    {
                        if (rData.Substring(4, 8) == regAdrs.ToString("X4") + Data.ToString("X4"))
                        {
                            setok = true;
                            break;
                        }
                        else
                            er = "写入PLC地址:" + wAddr.ToString() + "_" + regAdrs.ToString() + "返回异常:" + rData;
                    }
                }
                if (!setok)
                {
                    er = "写入PLC地址:" + wAddr.ToString() + "_" + regAdrs.ToString() + "失败__" + er;
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

        /// <summary>
        /// 读取单个VW地址数据
        /// </summary>
        /// <param name="plcAddr">PLC地址</param>
        /// <param name="wAddr">控制地址</param>
        /// <param name="Data">读取数据</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool RdAddr(int wAddr, int regAdrs, out int rData, out string er)
        {
            er = string.Empty;
            rData = 0;

            try
            {
                bool setok = false;
                string rVal = string.Empty;
                for (int j = 0; j < 3; j++)
                {
                    if (Read(wAddr, regAdrs, 1, out rVal, out er))
                    {
                        rData = Convert.ToInt16(rVal, 16);
                        setok = true;
                        break;
                    }

                }

                if (!setok)
                {
                    er = "读取PLC地址:" + wAddr.ToString() + "_" + regAdrs.ToString() + "失败__" + er;
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

        #region ModBus-RTU通信协议
        /// <summary>
        /// 检查CRC
        /// </summary>
        /// <param name="wCmd"></param>
        /// <returns></returns>
        private bool checkCRC(string wCmd)
        {
            string crc = Crc16(wCmd.Substring(0, wCmd.Length - 4));
            if (crc != wCmd.Substring(wCmd.Length - 4, 4))
                return false;
            return true;
        }


        /// <summary>
        /// CRC16校验算法,低字节在前，高字节在后
        /// </summary>
        /// <param name="data">要校验的数组</param>
        /// <returns>返回校验结果，低字节在前，高字节在后</returns>
        private static string Crc16(string strHex)
        {
            int count = strHex.Length / 2;
            int[] data = new int[count];
            for (int z = 0; z < count; z++)
            {
                data[z] = System.Convert.ToInt32(strHex.Substring(z * 2, 2), 16);
            }
            if (data.Length == 0)
                throw new Exception("调用CRC16校验算法,（低字节在前，高字节在后）时发生异常，异常信息：被校验的数组长度为0。");
            int[] temdata = new int[data.Length + 2];
            int xda, xdapoly;
            int i, j, xdabit;
            xda = 0xFFFF;
            xdapoly = 0xA001;
            for (i = 0; i < data.Length; i++)
            {
                xda ^= data[i];
                for (j = 0; j < 8; j++)
                {
                    xdabit = (int)(xda & 0x01);
                    xda >>= 1;
                    if (xdabit == 1)
                        xda ^= xdapoly;
                }
            }
            temdata = new int[2] { (int)(xda & 0xFF), (int)(xda >> 8) };
            string crc = temdata[0].ToString("X2") + temdata[1].ToString("X2");
            return crc;
        }



        #endregion
    }
}
