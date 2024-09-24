using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GJ.DEV.COM;

namespace GJ.DEV.ELOAD
{
    public class CEL_100_04 : IEL
    {
        #region 构造函数
        public CEL_100_04(int idNo = 0, string name = "EL_100_04")
        {
            _idNo = idNo;
            _name = name;
        }
        #endregion

        #region 字段
        private int _idNo = 0;
        private string _name = "EL_100_04";
        private bool _conStatus = false;
        private CSerialPort com;
        private int ELCH = 2;
        #endregion

        #region 属性
        /// <summary>
        /// 编号
        /// </summary>
        public int idNo
        {
            get { return _idNo; }
            set { _idNo = value; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public bool conStatus
        {
            get { return _conStatus; }
        }
        /// <summary>
        /// 负载通道数
        /// </summary>
        public int maxCH
        { 
            get { return ELCH; }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 57600,n,8,1
        /// </summary>
        /// <param name="comName"></param>
        /// <param name="setting"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool Open(string comName, out string er, string setting = "57600,n,8,1")
        {
            er = string.Empty;

            try
            {
                if (com != null)
                {
                    com.close();
                    com = null;
                }

                com = new CSerialPort(idNo, name, EDataType.HEX格式);

                if (!com.open(comName, out er, setting))
                    return false;

                _conStatus = true;

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <returns></returns>
        public void Close()
        {
            if (com == null)
                return;
            com.close();
            com = null;
            _conStatus = false;
        }
        /// <summary>
        /// 设置新地址
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool SetNewAddr(int wAddr, out string er)
        {
            er = string.Empty;

            try
            {
                string cmd0 = "00";
                string cmd1 = "00";
                string wCmd = cmd0 + cmd1;
                string wData = wAddr.ToString("X2");
                string rData = string.Empty;
                int rLen = 6;
                wData = CalDataFromELCmd(0, wCmd, wData);
                if (!SendCmdToELoad(wData, rLen, out rData, out er))
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 设置负载值
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="wDataSet"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool SetELData(int wAddr, CEL_SetPara wDataSet, out string er)
        {
            er = string.Empty;

            try
            {
                string cmd0 = "01";
                string cmd1 = "01";
                string wCmd = cmd0 + cmd1;
                string wData = string.Empty;
                string rData = string.Empty;
                int rLen = 7;
                wData += wDataSet.SaveEEPROM.ToString("X2");     //D0:设定写EEPROM状态
                wData += wDataSet.PWM_Status.ToString("X2");      //D1:设定PWM状态
                wData += wDataSet.PWM_Freq.ToString("X4");        //D2,D3:设定PWM频率
                wData += wDataSet.PWM_DutyCycle.ToString("X4");   //D4,D5:设定PWM占空比
                wData += wDataSet.Run_Status.ToString("X2");      //D6:设定工作状态
                wData += wDataSet.Run_Power[0].ToString("X2");    //D7:设定工作功率
                for (int i = 0; i < ELCH; i++)
                {
                    wData += ((int)wDataSet.Run_Mode[i]).ToString("X2");            //D8:设定工作模式

                    int val = 0;

                    //D9,D10:设定工作状态　工作数据

                    if (wDataSet.Run_Mode[i] == EMode.CC)
                        val = (int)(wDataSet.Run_Val[i] * 1000);
                    else
                        val = (int)(wDataSet.Run_Val[i] * 100);

                    wData += ((val / 240).ToString("X2") + (val % 240).ToString("X2"));

                    //D11,D12:设定工作状态　V_ON数据

                    val = (int)(wDataSet.Run_Von[i] * 100);

                    wData += ((val / 240).ToString("X2") + (val % 240).ToString("X2"));
                }
                
                wData = CalDataFromELCmd(wAddr, wCmd, wData);

                if (!SendCmdToELoad(wData, rLen, out rData, out er))
                    return false;
                
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 读取负载设定值
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="rDataSet"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool ReadELLoadSet(int wAddr, CEL_ReadSetPara rDataSet, out string er)
        {
            er = string.Empty;

            try
            {
                string cmd0 = "02";
                string cmd1 = "02";
                string wCmd = cmd0 + cmd1;
                string wData = string.Empty;
                string rData = string.Empty;
                int rLen = 29;
                wData = CalDataFromELCmd(wAddr, wCmd, wData);
                if (!SendCmdToELoad(wData, rLen, out rData, out er))
                    return false;
                for (int i = 0; i < ELCH; i++)
                {
                    if (rData.Substring(i * 18 + 10, 2) == "00") //D5:读取工作状态　00：停止 01：运行
                        rDataSet.status[i] = "STOP";
                    else
                        rDataSet.status[i] = "OK";

                    //D7:读取工作模式  00：CC模式 01：CV模式02：LED模式

                    rDataSet.LoadMode[i] = (EMode)System.Convert.ToInt16(rData.Substring(i * 18 + 14, 2), 16);

                    //D8,D9:读取工作状态　工作数据

                    int val_H = System.Convert.ToInt16(rData.Substring(i * 18 + 16, 2), 16);

                    int val_L = System.Convert.ToInt16(rData.Substring(i * 18 + 18, 2), 16);

                    double val = (double)(val_H * 240 + val_L);

                    if (rDataSet.LoadMode[i] == EMode.CC)
                        rDataSet.LoadVal[i] = val / 1000;
                    else
                        rDataSet.LoadVal[i] = val / 100;

                    //D10,D11:读取工作状态　V_ON数据

                    val_H = System.Convert.ToInt16(rData.Substring(i * 18 + 20, 2), 16);

                    val_L = System.Convert.ToInt16(rData.Substring(i * 18 + 22, 2), 16);

                    val = (double)(val_H * 240 + val_L);

                    rDataSet.Von[i] = val / 100;

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
        /// 读取负载数据
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="rDataVal"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool ReadELData(int wAddr, CEL_ReadData rDataVal, out string er)
        {
            er = string.Empty;

            try
            {
                string cmd0 = "02";
                string cmd1 = "01";
                string wCmd = cmd0 + cmd1;
                string wData = string.Empty;
                string rData = string.Empty;
                int rLen = 25;
                wData = CalDataFromELCmd(wAddr, wCmd, wData);
                if (!SendCmdToELoad(wData, rLen, out rData, out er))
                    return false;
                //D0:读取工状态（Start、Stop）
                //D1.――>读取工作功率（100W、150W）
                //D2. D3  ――>Vsen电压
                //D4. D5  ――>电压
                //D6. D7  ――>电流
                for (int i = 0; i < ELCH; i++)
                {
                    rDataVal.ONOFF = System.Convert.ToInt16(rData.Substring(i * 16, 2), 16);

                    int val_H = System.Convert.ToInt16(rData.Substring(i * 16 + 4, 2), 16);

                    int val_L = System.Convert.ToInt16(rData.Substring(i * 16 + 6, 2), 16);

                    double val = (double)(val_H * 240 + val_L);

                    rDataVal.Vs[i] = val / 100;

                    val_H = System.Convert.ToInt16(rData.Substring(i * 16 + 8, 2), 16);

                    val_L = System.Convert.ToInt16(rData.Substring(i * 16 + 10, 2), 16);

                    val = (double)(val_H * 240 + val_L);

                    rDataVal.Volt[i] = val / 100;

                    val_H = System.Convert.ToInt16(rData.Substring(i * 16 + 12, 2), 16);

                    val_L = System.Convert.ToInt16(rData.Substring(i * 16 + 14, 2), 16);

                    val = (double)(val_H * 240 + val_L);

                    rDataVal.Load[i] = val / 1000;
                }
                //D16
                rDataVal.OCP = System.Convert.ToInt16(rData.Substring(32, 2), 16) / 100;
                rDataVal.OVP = System.Convert.ToInt16(rData.Substring(34, 2), 16) / 100;
                rDataVal.OPP = System.Convert.ToInt16(rData.Substring(36, 2), 16) / 100;
                rDataVal.OTP = System.Convert.ToInt16(rData.Substring(38, 2), 16) / 100;
                if (rDataVal.OCP == 1 || rDataVal.OVP == 1 || rDataVal.OPP == 1 || rDataVal.OTP == 1)
                {
                    rDataVal.Status = "Alarm:";
                    if (rDataVal.OCP == 1)
                        rDataVal.Status += "OCP;";
                    if (rDataVal.OVP == 1)
                        rDataVal.Status += "OVP;";
                    if (rDataVal.OPP == 1)
                        rDataVal.Status += "OPP;";
                    if (rDataVal.OTP == 1)
                        rDataVal.Status += "OTP;";
                }
                else
                {
                    rDataVal.Status = "OK";
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

        #region 协议
        /*
       * 发送:桢头(FE)+地址+命令01+命令02+长度+数据+校验和+桢尾(FF)
       * 应答:桢头(FE)+地址+长度+数据+校验和+桢尾(FF)
      */
        private string SOI = "FE";
        private string EOI = "FF";
        /// <summary>
        /// 协议格式
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="wCmd"></param>
        /// <param name="wData"></param>
        /// <returns></returns>
        private string CalDataFromELCmd(int wAddr, string wCmd, string wData)
        {
            string cmd = string.Empty;

            int nLen = 4 + wData.Length / 2;

            cmd = wAddr.ToString("X2") + wCmd + nLen.ToString("X2") + wData;

            int cmdByteLen = cmd.Length / 2;

            byte[] cmdBytes = new byte[cmdByteLen];

            int sum = 0;

            string cmdFormat = string.Empty;

            //在发设定命令时，需要检查数据的低位字节是否与帧头或帧尾相等，如相等需改为FE-2,FF-4

            for (int i = 0; i < cmdByteLen; i++)
            {
                cmdBytes[i] = System.Convert.ToByte(cmd.Substring(i * 2, 2), 16);
                if (cmdBytes[i] == 0xFE)
                    cmdBytes[i] = 0xFE - 2;
                else if (cmdBytes[i] == 0xFF)
                    cmdBytes[i] = 0xFF - 4;
                cmdFormat += cmdBytes[i].ToString("X2");
                sum += cmdBytes[i];
            }

            sum = sum % 0x100;

            string chkSum = sum.ToString("X2");

            cmdFormat = SOI + cmdFormat + chkSum + EOI;

            return cmdFormat;
        }
        /// <summary>
        /// 检验和
        /// </summary>
        /// <param name="wData"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool EL_CheckSum(string wData, out string er)
        {
            er = string.Empty;

            try
            {
                int s1 = wData.IndexOf(SOI);
                int s2 = wData.LastIndexOf(EOI);
                if (s2 == 0 || s2 - s1 < 10)
                {
                    er = "数据错误:" + wData;
                    return false;
                }
                string cmdFormat = wData.Substring(s1, s2 - s1 + 2);
                int sum = 0;
                for (int i = 1; i <= cmdFormat.Length / 2 - 3; i++)
                    sum += System.Convert.ToInt16(cmdFormat.Substring(i * 2, 2), 16);
                sum = sum % 256;
                string calSum = sum.ToString("X2");

                //计算校验和时, 检查数据是否与帧头或帧尾相等，如相等需改为FE-2,FF-4.
                if (calSum == SOI)
                    calSum = "FC";
                else if (calSum == EOI)
                    calSum = "FB";
                string getSum = cmdFormat.Substring(cmdFormat.Length - 4, 2);
                if (calSum != getSum)
                {
                    er = "数据校验和错误:" + wData;
                    return false;
                }
                int rLen = System.Convert.ToInt16(cmdFormat.Substring(4, 2), 16);
                if ((wData.Length / 2) != (rLen + 3))
                {
                    er = "数据长度错误:" + wData;
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
        /// 发送和接收数据
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="wData"></param>
        /// <param name="rLen"></param>
        /// <param name="rData"></param>
        /// <param name="er"></param>
        /// <param name="wTimeOut"></param>
        /// <returns></returns>
        private bool SendCmdToELoad(string wData, int rLen, out string rData, out string er, int wTimeOut = 500)
        {
            rData = string.Empty;

            er = string.Empty;

            try
            {
                string recvData = string.Empty;
                if (!com.send(wData, rLen, out recvData, out er, wTimeOut))
                    return false;
                if (rLen > 0)
                {
                    if (!EL_CheckSum(recvData, out er))
                        return false;
                    rData = recvData.Substring(6, recvData.Length - 10);
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
