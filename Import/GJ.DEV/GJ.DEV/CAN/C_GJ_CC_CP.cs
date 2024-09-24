﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace GJ.DEV.CAN
{
    public class C_GJ_CC_CP
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
        /// 读线圈
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
                wCmd += N.ToString("X2");        //读地址长度---1Byte

                wCmd += Crc16(wCmd);                  //CRC16 低位前,高位后     

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
        /// 返回数值
        /// </summary>
        /// <param name="devType"></param>
        /// <param name="regAddr"></param>
        /// <param name="N"></param>
        /// <param name="rVal">地址N值</param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool Read(int devAddr, int regAddr, ref int[] rVal, out string er)
        {
            er = string.Empty;

            try
            {
                string rData = string.Empty;

                int N = rVal.Length;

                if (!Read(devAddr, regAddr, N, out rData, out er))
                    return false;

                for (int i = 0; i < N; i++)
                    rVal[i] = System.Convert.ToInt32(rData.Substring(rData.Length - (i + 1) * 4, 4), 16);


                return true;
            }
            catch (Exception e)
            {
                er = e.ToString();
                return false;
            }
        }
        /// <summary>
        /// 单写线圈和寄存器值
        /// 从机地址(1Byte)+功能码(1Byte)+寄存器地址(2Byte)+地址数量(2Byte)+字节数(1Byte)+数据+CRC检验(2Byte)
        /// </summary>
        /// <param name="devType">地址类型</param>
        /// <param name="regAddr">开始地址</param>
        /// <param name="wVal">单个值</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool Write(int devAddr, int regAddr, int wVal, out string er)
        {
            er = string.Empty;

            try
            {
                int N = 1;   //单写1个值
                string wCmd = devAddr.ToString("X2");
                int rLen = 0;
                int wLen = 0;
                string wData = string.Empty;

                wCmd += "10";       //寄存器功能码为16
                wLen = N * 2;       //写入字节数
                rLen = 8;           //回读长度
                wData = wVal.ToString("X" + wLen * 2);

                wCmd += regAddr.ToString("X4");         //开始地址
                wCmd += N.ToString("X4");               //读地址长度
                wCmd += wLen.ToString("X2");            //写入字节数  
                wCmd += wData;                          //写入数据
                wCmd += Crc16(wCmd);                    //CRC16 低位前,高位后   
                string rData = string.Empty;
                rLen = 4;
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

        /// <summary>
        /// 写多个线圈和寄存器
        /// </summary>
        /// <param name="devType">地址类型</param>
        /// <param name="regAddr">开始地址</param>
        /// <param name="wVal">多个值</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool Write(int devAddr, int regAddr, int[] wVal, out string er)
        {
            er = string.Empty;

            try
            {
                int N = wVal.Length;   //单写多个值
                string wCmd = devAddr.ToString("X2");
                int rLen = 0;
                int wLen = 0;
                string wData = string.Empty;

                wCmd += "10";        //寄存器功能码为16
                wLen = N * 2;          //写入字节数
                rLen = 8;           //回读长度
                for (int i = 0; i < N; i++)
                {
                    wData += wVal[i].ToString("X4");
                }

                wCmd += regAddr.ToString("X4");  //开始地址
                wCmd += N.ToString("X4");                  //读地址长度
                wCmd += wLen.ToString("X2");               //写入字节数  
                wCmd += wData;                             //写入数据
                wCmd += Crc16(wCmd);                //CRC16 低位前,高位后   
                rLen = 4;
                string rData = string.Empty;
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
        /// 读写CC_CP板地址
        /// </summary>
        /// <param name="wAddr">CC_CP板地址</param>
        /// <param name="setAdrs">设定地址</param>
        /// <param name="getAdrs">读取地址</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool RWAddrs(int wAddr, bool needSet, int[] setAdrs, bool needRead, ref int[] getAdrs, out string er)
        {
            er = string.Empty;

            try
            {
                int regAdrs = 0x8000;

                if (needSet)                                        //写入CAN盒地址
                {
                    if (wAddr < 1 && wAddr > 128)
                        for (int i = 0; i < setAdrs.Length; i++)
                        {
                            bool setok = false;
                            int setAdd = setAdrs[i];
                            for (int j = 0; j < 3; j++)
                            {
                                if (Write(wAddr, regAdrs + i, setAdd, out er))
                                {
                                    setok = true;
                                    break;
                                }
                            }


                            if (!setok)
                            {
                                er = "写入CC_CP板地址" + wAddr.ToString() + "失败__" + er;
                                return false;
                            }

                        }
                    else
                    {
                        er = "写入CC_CP板地址为1~128";
                        return false;
                    }
                }

                if (needRead)                                           //读取CAN盒地址
                    if (!Read(wAddr, regAdrs, ref getAdrs, out er))
                    {
                        er = "读取CC_CP板地址" + wAddr.ToString() + "失败__" + er;
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
        /// 读取CC_CP板版本         0x8001
        /// </summary>
        /// <param name="wAddr">CC_CP板地址</param>
        /// <param name="version">版本</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool ReadVersion(int wAddr, out string version, out string er)
        {
            er = string.Empty;

            version = string.Empty;

            try
            {
                int regAdrs = 0x8001;
                int[] rVal = new int[1];
                if (!Read(wAddr, regAdrs, ref rVal, out er))
                {
                    er = "读取CC_CP板地址" + wAddr.ToString() + "版本信息失败__" + er;
                    return false;
                }
                double ver = ((double)rVal[0] / 10);
                version = ver.ToString("F2");
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }

        /// <summary>
        /// 读写CC_CP板波特率         0x8002
        /// </summary>
        /// <param name="wAddr">CC_CP板地址</param>
        /// <param name="setBaud">设定波特率</param>
        /// <param name="getBaud">读取波特率</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool RWBaud(int wAddr, bool needSet, int[] setBaud, bool needRead, ref int[] getBaud, out string er)
        {
            er = string.Empty;

            try
            {
                int regAdrs = 0x8002;
                if (needSet)                                    //写入CAN盒波特率
                {
                    for (int i = 0; i < setBaud.Length; i++)
                    {
                        bool setok = false;
                        int setbaud = setBaud[i];
                        for (int j = 0; j < 3; j++)
                        {
                            if (Write(wAddr, regAdrs + i, setbaud, out er))
                            {
                                setok = true;
                                break;
                            }
                        }

                        if (!setok)
                        {
                            er = "写入CC_CP板地址" + wAddr.ToString() + "波特率" + setbaud.ToString() + "失败__" + er;
                            return false;
                        }
                    }

                }

                if (needRead)                                       //读取CAN盒波特率
                {
                    if (!Read(wAddr, regAdrs, ref getBaud, out er))
                    {
                        er = "读取CC_CP板地址" + wAddr.ToString() + "波特率失败__" + er;
                        return false;
                    }
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
        /// 读写CC_CP板_CC电阻值                 0x8003
        /// </summary>
        /// <param name="wAddr">CC_CP板地址</param>
        /// <param name="setCCSpec">设定CC电阻值_默认值 0，控制 4 个电阻;Bit0：1.5K 电阻开关,1 为开,读取时此值为 1 表示开;Bit1：1.5K 电阻开关，1 为关;Bit2：680Ω 电阻开关，1 为开，读取时此值为 1 表示开;Bit3：680Ω 电阻开关，1 为关;Bit4：220Ω 电阻开关，1 为开，读取时此值为 1 表示开;Bit5：220Ω 电阻开关，1 为关;Bit6：100Ω 电阻开关，1 为开，读取时此值为 1 表示开;Bit7：100Ω 电阻开关，1 为关;Bit8：enable 控制，1 为开，读取时此值为 1 表示开;Bit9：enable 控制，1 为关</param>
        /// <param name="getCCSpec">读取CC电阻值</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool RWCCSpec(int wAddr, bool needSet, int[] setCCSpec, bool needRead, ref int[] getCCSpec, out string er)
        {
            er = string.Empty;

            try
            {
                int regAdrs = 0x8003;
                if (needSet)                                    //写入CC状态
                {
                    for (int i = 0; i < setCCSpec.Length; i++)
                    {
                        bool setok = false;
                        int setbaud = setCCSpec[i];
                        for (int j = 0; j < 3; j++)
                        {
                            if (Write(wAddr, regAdrs + i, setbaud, out er))
                            {
                                setok = true;
                                break;
                            }
                        }

                        if (!setok)
                        {
                            er = "写入CC_CP板地址" + wAddr.ToString() + "CC电阻值" + setbaud.ToString() + "失败__" + er;
                            return false;
                        }
                    }

                }

                if (needRead)                                       //读取CC状态
                {
                    if (!Read(wAddr, regAdrs, ref getCCSpec, out er))
                    {
                        er = "读取CC_CP板地址" + wAddr.ToString() + "CC电阻值失败__" + er;
                        return false;
                    }
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
        /// 读写CC_CP板_CP占空比                0x8004
        /// </summary>
        /// <param name="wAddr">CC_CP板地址</param>
        /// <param name="setSpec">设定规格默认值 600，表示 60%。设定正负 12V PWM 占空比用。</param>
        /// <param name="getSpec">读取规格</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool RWCPSpec(int wAddr, bool needSet, int[] setSpec, bool needRead, ref int[] getSpec, out string er)
        {
            er = string.Empty;

            try
            {
                int regAdrs = 0x8004;
                if (needSet)                                    //写入CC_CP板_CP占空比 
                {
                    for (int i = 0; i < setSpec.Length; i++)
                    {
                        bool setok = false;
                        int setbaud = setSpec[i];
                        for (int j = 0; j < 3; j++)
                        {
                            if (Write(wAddr, regAdrs + i, setbaud, out er))
                            {
                                setok = true;
                                break;
                            }
                        }

                        if (!setok)
                        {
                            er = "写入CC_CP板地址" + wAddr.ToString() + "CP占空比 " + setbaud.ToString() + "失败__" + er;
                            return false;
                        }
                    }

                }

                if (needRead)                                       //读取CC_CP板_CP占空比 
                {
                    if (!Read(wAddr, regAdrs, ref getSpec, out er))
                    {
                        er = "读取CC_CP板地址" + wAddr.ToString() + "CP占空比 __" + er;
                        return false;
                    }
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
        /// 读写CC_CP板电子锁                  0x8005
        /// </summary>
        /// <param name="wAddr">CC_CP板地址</param>
        /// <param name="setData">设定数据低 8 位值为 1 表示电子锁 1、2 脚合上，为 0 断开;高 8 位值为 1 表示电子锁 3 脚为正 1 脚为负；值为 2 表示电子锁 1 脚为正 3 脚为负</param>
        /// <param name="getData">读取数据</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool RWKey(int wAddr, bool needSet, int[] setData, bool needRead, ref int[] getData, out string er)
        {
            er = string.Empty;

            try
            {
                int regAdrs = 0x8005;
                if (needSet)                                        //写入CC_CP板电子锁
                {
                    for (int i = 0; i < setData.Length; i++)
                    {
                        bool setok = false;
                        int setbaud = setData[i];
                        for (int j = 0; j < 3; j++)
                        {
                            if (Write(wAddr, regAdrs + i, setbaud, out er))
                            {
                                setok = true;
                                break;
                            }
                        }

                        if (!setok)
                        {
                            er = "写入CC_CP板地址" + wAddr.ToString() + "电子锁失败__" + er;
                            return false;
                        }
                    }
                }
                if (needRead)                                       //读取CC_CP板电子锁
                {
                    if (!Read(wAddr, regAdrs, ref getData, out er))
                    {
                        er = "读取CC_CP板地址" + wAddr.ToString() + "电子锁失败__" + er;
                        return false;
                    }
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
