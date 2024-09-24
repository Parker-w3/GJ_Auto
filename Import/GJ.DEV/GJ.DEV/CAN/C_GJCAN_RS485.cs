using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace GJ.DEV.CAN
{
    public class C_GJCAN_RS485
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

                wCmd += "10";       //寄存器功能码为16
                wLen = N * 2;       //写入字节数
                rLen = 8;           //回读长度
                wData = wVal.ToString("X" + wLen * 2);

                wCmd += regAddr.ToString("X4");         //开始地址
                wCmd += N.ToString("X4");               //读地址长度
                wCmd += wLen.ToString("X2");            //写入字节数  
                wCmd += wData;                          //写入数据
                wCmd += Crc16(wCmd);                    //CRC16 低位前,高位后   
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
        private bool Write(int devAddr, int regAddr, int[] wVal, out string rData, out string er)
        {
            er = string.Empty;
            rData = string.Empty;
            try
            {
                int N = wVal.Length;   //单写多个值
                string wCmd = devAddr.ToString("X2");
                int rLen = 0;
                int wLen = 0;
                string wData = string.Empty;

                wCmd += "10";        //寄存器功能码为16
                wLen = N * 2;        //写入字节数
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
                rData = string.Empty;

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
        /// 设定CAN盒地址 0x8000
        /// </summary>
        /// <param name="wAddr">00写入地址</param>
        /// <param name="setAdrs">设定地址</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool WtAddrs(int wAddr, int setAdrs, out string er)
        {
            er = string.Empty;

            try
            {
                int regAdrs = 0x8000;
                bool setok = false;
                string rData = string.Empty;

                if (setAdrs > 0 && setAdrs < 129)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (Write(wAddr, regAdrs, setAdrs, out rData, out er))
                        {
                            if (rData.Substring(0, 4) == setAdrs.ToString("X2") + "10")
                            {
                                setok = true;
                                break;
                            }
                            else
                            {
                                er = "返回异常:" + rData;
                            }
                        }
                    }

                    if (!setok)
                    {
                        er = "写入CAN盒地址" + setAdrs.ToString() + "失败__" + er;
                        return false;
                    }
                }
                else
                {
                    er = "写入CAN盒地址为1~128";
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
        /// 读取版本         0x8001
        /// </summary>
        /// <param name="wAddr">CAN盒地址</param>
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
                    er = "读取IIC地址" + wAddr.ToString() + "版本信息失败__" + er;
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
        /// 设定CAN盒485波特率         0x8002
        /// </summary>
        /// <param name="wAddr">CAN盒地址</param>
        /// <param name="setBaud">设定波特率</param>
        /// <param name="getBaud">读取波特率</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool RWBaud(int wAddr, bool needSet, int setBaud, bool needRead, out int getBaud, out string er)
        {
            getBaud = 57600;
            er = string.Empty;
            try
            {
                int regAdrs = 0x8002;
                string rData = string.Empty;
                bool setok = false;
                if (needSet)                                    //写入CAN盒波特率
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (Write(wAddr, regAdrs, setBaud, out rData, out er))
                        {
                            if (rData.Substring(0, 4) == wAddr.ToString("X2") + "10")
                            {
                                setok = true;
                                break;
                            }
                            else
                            {
                                er = "返回异常:" + rData;
                            }
                        }
                    }

                    if (!setok)
                    {
                        er = "写入CAN盒地址" + wAddr.ToString() + "波特率" + setBaud.ToString() + "失败__" + er;
                        getBaud = setBaud;
                        return false;
                    }
                }

                if (needRead)                                       //读取CAN盒波特率
                {
                    setok = false;
                    int[] getValue = new int[1];
                    for (int j = 0; j < 3; j++)
                    {
                        if (Read(wAddr, regAdrs, ref getValue, out er))
                        {
                            getBaud = getValue[0];
                            setok = true;
                            break;
                        }
                    }
                    if (!setok)
                    {
                        er = "读取CAN盒地址" + wAddr.ToString() + "波特率失败__" + er;
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
        /// 设定CAN盒CAN波特率                0x8003
        /// </summary>
        /// <param name="wAddr">CAN盒地址</param>
        /// <param name="setCanBaud">设定CAN波特率</param>
        /// <param name="getCanBaud">读取CAN波特率</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool RWCANBaud(int wAddr, bool needSet, int setCanBaud, bool needRead, ref int getCanBaud, out string er)
        {
            er = string.Empty;

            try
            {
                int regAdrs = 0x8003;
                bool setok = false;
                string rData = string.Empty;
                if (needSet)                                    //写入CAN盒波特率
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (Write(wAddr, regAdrs, setCanBaud, out rData, out er))
                        {
                            if (rData.Substring(0, 4) == wAddr.ToString("X2") + "10")
                            {
                                setok = true;
                                break;
                            }
                            else
                            {
                                er = "返回异常:" + rData;
                            }
                        }
                    }

                    if (!setok)
                    {
                        er = "写入CAN盒地址" + wAddr.ToString() + "CAN波特率" + setCanBaud.ToString() + "K失败__" + er;
                        return false;
                    }
                }

                if (needRead)                                       //读取CAN盒波特率
                {
                    setok = false;
                    int[] getValue = new int[1];
                    for (int j = 0; j < 3; j++)
                    {
                        if (Read(wAddr, regAdrs, ref getValue, out er))
                        {
                            getCanBaud = getValue[0];
                            setok = true;
                            break;
                        }
                    }
                    if (!setok)
                    {
                        er = "读取CAN盒地址" + wAddr.ToString() + "CAN波特率失败__" + er;
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
        /// 设定CAN盒特定机种数据过滤                  0x8004
        /// </summary>
        /// <param name="wAddr">CAN盒地址</param>
        /// <param name="setSpec">设定规格</param>
        /// <param name="getSpec">读取规格</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool RWModelSpec(int wAddr, bool needSet, int setSpec, bool needRead, ref int getSpec, out string er)
        {
            er = string.Empty;

            try
            {
                int regAdrs = 0x8004;
                bool setok = false;
                string rData = string.Empty;
                if (needSet)                                    //写入CAN盒特定机种数据过滤
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (Write(wAddr, regAdrs, setSpec, out rData, out er))
                        {
                            if (rData.Substring(0, 4) == wAddr.ToString("X2") + "10")
                            {
                                setok = true;
                                break;
                            }
                            else
                            {
                                er = "返回异常:" + rData;
                            }
                        }
                    }
                    if (!setok)
                    {
                        er = "写入CAN盒地址" + wAddr.ToString() + "特定机种数据过滤" + setSpec.ToString() + "失败__" + er;
                        return false;
                    }
                }

                if (needRead)                                       //读取CAN盒特定机种数据过滤
                {

                    setok = false;
                    int[] getValue = new int[1];
                    for (int j = 0; j < 3; j++)
                    {
                        if (Read(wAddr, regAdrs, ref getValue, out er))
                        {
                            getSpec = getValue[0];
                            setok = true;
                            break;
                        }
                    }
                    if (!setok)
                    {
                        er = "读取CAN盒地址" + wAddr.ToString() + "特定机种数据过滤失败__" + er;
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
        /// 设定美达模式配置参数                  0x80OD
        /// </summary>
        /// <param name="wAddr">CAN盒地址</param>
        /// <param name="setvalue">设定规格</param>
        /// <param name="getvalue">读取规格</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool RWCanValue(int wAddr, int regAdrs, bool needSet, string setvalue, bool needRead, ref string getvalue, out string er)
        {
            er = string.Empty;
            try
            {
                bool setOK = false;
                if (needSet)                                    //写入CAN盒特定机种数据过滤
                {
                    string[] Str = setvalue.Split(',');

                    string rData = string.Empty;

                    for (int i = 0; i < Str.Length; i++)
                    {
                        int setData = Convert.ToInt16(Str[i]);

                        for (int j = 0; j < 3; j++)
                        {
                            if (Write(wAddr, regAdrs + i, setData, out rData, out er))
                            {
                                if (rData.Substring(0, 4) == wAddr.ToString("X2") + "10")
                                {
                                    setOK = true;
                                    break;
                                }
                                else
                                {
                                    er = "写入数据返回异常:" + rData;
                                }
                            }

                            System.Threading.Thread.Sleep(5);
                        }

                        if (!setOK)
                        {
                            er = "写入CAN盒地址:" + wAddr.ToString() + "寄存器地址:0x" + regAdrs.ToString("X4") + "数据" + setData.ToString() + "失败__" + er;
                            return false;
                        }
                    }

                }

                if (needRead)                                       //读取CAN盒特定机种数据过滤
                {
                    setOK = false;
                    string[] Str = getvalue.Split(',');
                    int[] getValue = new int[Str.Length];
                    getvalue = string.Empty;
                    for (int j = 0; j < 3; j++)
                    {

                        if (Read(wAddr, regAdrs, ref getValue, out er))
                        {
                            for (int i = 0; i < Str.Length; i++)
                            {
                                getvalue += getValue[i].ToString() + ",";
                            }
                            getvalue = getvalue.Substring(0,getvalue.Length - 1);
                            setOK = true;
                            break;
                        }
                    }
                    if (!setOK)
                    {
                        er = "读取CAN盒地址" + wAddr.ToString() + "特定机种数据过滤失败__" + er;
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
        /// 邮箱中可用数据个数(设定和清除)                  0x8005
        /// </summary>
        /// <param name="wAddr">CAN盒地址</param>
        /// <param name="setNum">清除数量</param>
        /// <param name="getNum">读取数量</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool RWCanNum(int wAddr, bool needSet, int setNum, bool needRead, ref int getNum, out string er)
        {
            er = string.Empty;

            try
            {
                int regAdrs = 0x8005;
                bool setok = false;
                string rData = string.Empty;
                if (needSet)                                        //获取当前CAN中的缓存帧
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (Write(wAddr, regAdrs, setNum, out rData, out er))
                        {
                            if (rData.Substring(0, 4) == wAddr.ToString("X2") + "10")
                            {
                                setok = true;
                                break;
                            }
                            else
                            {
                                er = "返回异常:" + rData;
                            }
                        }
                    }
                    if (!setok)
                    {
                        er = "清除CAN盒地址" + wAddr.ToString() + "邮箱中数量失败__" + er;
                        return false;
                    }
                }

                if (needRead)                                       //获取当前CAN中的缓存帧数
                {
                    setok = false;
                    int[] getValue = new int[1];
                    for (int j = 0; j < 3; j++)
                    {
                        if (Read(wAddr, regAdrs, ref getValue, out er))
                        {
                            getNum = getValue[0];
                            setok = true;
                            break;
                        }
                    }
                    if (!setok)
                    {
                        er = "读取CAN盒地址" + wAddr.ToString() + "邮箱中可用数据个数失败__" + er;
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
        /// 读写CAN数据
        /// </summary>
        /// <param name="wAddr">CAN盒地址</param>
        /// <param name="setPara">发送邮件的数据结构为 4 个 32 位： TIR、TDTR、TDLR、TDHR。发送或接收 32 位时高 16 位在前，低 16 位在后，后面的一样。TIR 的第 31~21 位，StdId 0~0x7FF 标准 ID;TIR 的第 31~3 位，ExtId 0~0x1FFFFFFF 扩展 ID;TIR 的第 2 位为 1 表示扩展帧，否则标准帧;TIR 的第 1 位为 1 表示远程帧，否则数据帧;TIR 的第 0 位为发送请求，发送数据时填内部会自动填上。;TDTR 的第 31~16 位，时间戳。;TDTR 的第 8 位，发送全局时间，一般硬件设定这位无效。;TDTR 的第 3~0 位，发送数据长度。;TDLR 从高到低为数据 3、2、1、0，TDHR 7、6、5、4。;</param>
        /// <param name="getPara">读取数据</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool RWCanData(int wAddr, bool needSet, string setPara, bool needRead, ref string getPara, out string er)
        {
            er = string.Empty;

            try
            {
                int getNumData = 0;
                if (!RWCanNum(wAddr, true, 0, false, ref getNumData, out er))      //清除缓存CAN数量
                    return false;

                int regAdrs = 0x8018;
                if (needSet)                                            //发送帧
                {
                    string[] StrSet = setPara.Split(';');               //获取写入帧数量

                    for (int i = 0; i < StrSet.Length; i++)
                    {
                        string setpara = StrSet[i];
                        string[] Str = setpara.Split(',');
                        int canId = System.Convert.ToInt32(Str[0]);     //获取16进制的CANID
                        string Cmd = string.Empty;
                        long TIR = 0;
                        string StrTIR = string.Empty;
                        if (Convert.ToInt32(Str[1]) == 0)
                        {
                            StrTIR += Convert.ToString(canId, 2);
                            StrTIR += "000000000000000000" + Str[1] + Str[2] + Str[3];
                            TIR = Convert.ToInt64(StrTIR, 2);           //CANID+帧类型+帧格式+发送请求
                        }
                        else
                        {
                            StrTIR += Convert.ToString(canId, 2);
                            StrTIR += Str[1] + Str[2] + Str[3];
                            TIR = Convert.ToInt64(Convert.ToString(canId, 2) + Str[1] + Str[2] + Str[3], 2);  //CANID+帧类型+帧格式+发送请求
                        }
                        Cmd += TIR.ToString("X8");
                        string TDTR = Convert.ToInt32(Str[4]).ToString("X4") + Convert.ToInt32(Str[5]).ToString("X2") + Convert.ToInt32(Str[6]).ToString("X2");    //31~16 位，时间戳。8 位，发送全局时间，一般硬件设定这位无效。3~0 位，发送数据长度。
                        Cmd += TDTR;
                        string TDR = Str[7].Substring(6, 2) + Str[7].Substring(4, 2) + Str[7].Substring(2, 2) + Str[7].Substring(0, 2) + Str[7].Substring(14, 2) + Str[7].Substring(12, 2) + Str[7].Substring(10, 2) + Str[7].Substring(8, 2);    //TDLR 从高到低为数据 3、2、1、0，TDHR 7、6、5、4。             
                        Cmd += TDR;

                        int[] setData = new int[8];
                        string Strdata = string.Empty;
                        for (int j = 0; j < 8; j++)
                        {
                            Strdata = Cmd.Substring(j * 4, 4);
                            int Data = System.Convert.ToInt32(Strdata, 16);
                            setData[j] = Data;
                        }
                        string rData = string.Empty;
                        bool setOK = false;
                        //for (int k = 0; k < 3;k++)
                        //{

                        for (int j = 0; j < 3; j++)
                        {
                            if (Write(wAddr, regAdrs + i, setData, out rData, out er))
                            {
                                if (rData.Substring(0, 4) == wAddr.ToString("X2") + "10")
                                {
                                    setOK = true;
                                    break;
                                }
                                else
                                {
                                    er = "写入数据返回异常:" + rData;
                                }
                            }
                        }
                        //}

                        if (!setOK)
                        {
                            er = "写入数据失败:" + er;
                            return false;
                        }
                    }

                }

                if (needRead)                                       //获取当前CAN中的缓存帧数
                {
                    System.Threading.Thread.Sleep(40);

                    int iData = 0;

                    bool Readok = false;
                    string[] StrGet = getPara.Split(';');           //获取读取ID的数量

                    getPara = "";
                    for (int iScan = 0; iScan < 50; iScan++)
                    {
                        int[] getData = new int[8];

                        if (!Read(wAddr, regAdrs, ref getData, out er))
                        {
                            er = "读取CAN盒地址" + wAddr.ToString() + "读取数据失败__" + er;
                        }
                        else
                        {
                            bool readok = false;

                            string getStr = string.Empty;
                            string readStr = string.Empty;
                            string datastr = string.Empty;
                            for (int j = 0; j < 8; j++)
                            {
                                datastr = "0000" + Convert.ToString(getData[j], 16);
                                readStr += datastr.Substring(datastr.Length - 4, 4);
                            }

                            getStr = readStr.Substring(0 , 32);

                            long lngID = Convert.ToInt64(getStr.Substring(0, 8), 16);

                            string strID = Convert.ToString(lngID, 2);
                            strID = "0000000000000000000000000000000000" + strID;
                            strID = strID.Substring(strID.Length - 32, 32);
                            string ID = string.Empty;
                            if (strID.Substring(strID.Length - 3, 1) != "1")

                                ID = Convert.ToInt64(strID.Substring(0, 11), 2).ToString("X4");

                            else
                                ID = Convert.ToInt64(strID.Substring(0, 29), 2).ToString("X8");

                            string strdata = string.Empty;
                            System.Diagnostics.Debug.Print(ID);
                            if (ID == StrGet[iData])
                            {
                                readok = true;
                                strdata = getStr.Substring(getStr.Length - 10, 2) + getStr.Substring(getStr.Length - 12, 2) + getStr.Substring(getStr.Length - 14, 2) + getStr.Substring(getStr.Length - 16, 2) + getStr.Substring(getStr.Length - 2, 2) + getStr.Substring(getStr.Length - 4, 2) + getStr.Substring(getStr.Length - 6, 2) + getStr.Substring(getStr.Length - 8, 2);
                                StrGet[iData] = strdata;
                                getPara = getPara + StrGet[iData];
                                iData += 1;
                                if (readok && iData >= StrGet.Length)
                                {
                                    Readok = true;
                                    break;
                                }
                            }
                        }
                        System.Threading.Thread.Sleep(10);
                    }
                    if (!Readok)
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
        /// 写入自发CAN数据  0x80C0+i*9
        /// </summary>
        /// <param name="wAddr">CAN盒地址</param>
        /// <param name="setPara">发送邮件的数据结构为 4 个 32 位： TIR、TDTR、TDLR、TDHR。发送或接收 32 位时高 16 位在前，低 16 位在后，后面的一样。TIR 的第 31~21 位，StdId 0~0x7FF 标准 ID;TIR 的第 31~3 位，ExtId 0~0x1FFFFFFF 扩展 ID;TIR 的第 2 位为 1 表示扩展帧，否则标准帧;TIR 的第 1 位为 1 表示远程帧，否则数据帧;TIR 的第 0 位为发送请求，发送数据时填内部会自动填上。;TDTR 的第 31~16 位，时间戳。;TDTR 的第 8 位，发送全局时间，一般硬件设定这位无效。;TDTR 的第 3~0 位，发送数据长度。;TDLR 从高到低为数据 3、2、1、0，TDHR 7、6、5、4。自动发送的时间间隔，单位毫秒，最大值 65535，低于 40 时认为对应报文不发送。;</param>
        /// <param name="getPara">读取数据</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool RWSendCanData(int wAddr, string setPara, out string er)
        {
            er = string.Empty;

            try
            {
                int regAdrs = 0x80C0;

                string[] StrSet = setPara.Split(';');                   //获取写入帧数量

                for (int i = 0; i < StrSet.Length; i++)
                {
                    bool setok = false;
                    string setpara = StrSet[i];
                    string[] Str = setpara.Split(',');
                    int canId = System.Convert.ToInt32(Str[0]);     //获取16进制的CANID
                    string Cmd = string.Empty;
                    long TIR = 0;
                    string StrTIR = string.Empty;
                    if (Convert.ToInt32(Str[1]) == 0)
                    {
                        StrTIR += Convert.ToString(canId, 2);
                        StrTIR += "000000000000000000" + Str[1] + Str[2] + Str[3];
                        TIR = Convert.ToInt64(StrTIR, 2);           //CANID+帧类型+帧格式+发送请求
                    }
                    else
                    {
                        StrTIR += Convert.ToString(canId, 2);
                        StrTIR += Str[1] + Str[2] + Str[3];
                        TIR = Convert.ToInt64(Convert.ToString(canId, 2) + Str[1] + Str[2] + Str[3], 2);  //CANID+帧类型+帧格式+发送请求
                    }
                    Cmd += TIR.ToString("X8");
                    string TDTR = Convert.ToInt32(Str[4]).ToString("X4") + Convert.ToInt32(Str[5]).ToString("X2") + Convert.ToInt32(Str[6]).ToString("X2");    //31~16 位，时间戳。8 位，发送全局时间，一般硬件设定这位无效。3~0 位，发送数据长度。
                    Cmd += TDTR;
                    string TDR = Str[7].Substring(6, 2) + Str[7].Substring(4, 2) + Str[7].Substring(2, 2) + Str[7].Substring(0, 2) + Str[7].Substring(14, 2) + Str[7].Substring(12, 2) + Str[7].Substring(10, 2) + Str[7].Substring(8, 2);    //TDLR 从高到低为数据 3、2、1、0，TDHR 7、6、5、4。             
                    Cmd += TDR;
                    string Time = Convert.ToInt32(Str[8]).ToString("X4");   //自动发送的时间间隔，单位毫秒，最大值 65535，低于 40 时认为对应报文不发送 
                    Cmd += Time;

                    string rData = string.Empty;
                    int[] setData = new int[9];
                    string Strdata = string.Empty;

                    for (int j = 0; j < 9; j++)
                    {
                        Strdata = Cmd.Substring(j * 4, 4);
                        int Data = System.Convert.ToInt32(Strdata, 16);
                        setData[j] = Data;
                    }

                    for (int j = 0; j < 3; j++)
                    {
                        //Write( wAddr, regAdrs + i, setData, out er);

                        if (Write(wAddr, regAdrs + i * 9, setData, out rData, out er))
                        {
                            if (rData.Substring(0, 4) == wAddr.ToString("X2") + "10")
                            {
                                setok = true;
                                break;
                            }
                            else
                            {
                                er = "返回异常:" + rData;
                            }
                        }

                        System.Threading.Thread.Sleep(5);
                    }
                    if (!setok)
                    {
                        er = "CAN盒地址" + wAddr.ToString() + "写入自发CAN数据失败__" + er;
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

