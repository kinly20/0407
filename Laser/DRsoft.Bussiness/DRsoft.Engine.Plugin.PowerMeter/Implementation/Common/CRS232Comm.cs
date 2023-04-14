using System;
using System.Text;
using System.Threading;
using DRsoft.Runtime.Core.Platform.Logging;
using RJCP.IO.Ports;


//using Levero.DeviceService.Ports;

namespace DRsoft.Engine.Plugin.PowerMeter.Implementation.Common
{
    /// <summary>
    /// 串口通讯类
    /// </summary>
    public class CRS232Comm
    {
        //private SerialPort RSPort;
        private SerialPortStream RSPort;

       // private CustomSerialPort RSPort;
        private Mutex mMutex;
        private string sReceive; //串口接收到的字符

        public double m_PowerData;

        public delegate void EventHandler(double index,bool flag);
        public event EventHandler OnPowerData;
        protected ILog Log = null;
        /// <summary>
        /// 串口端口是否已经打开
        /// </summary>
        public bool PortOpened
        {
            get { return RSPort.IsOpen; }
        }
        //*****************
        public string PortName { get; set; }
        public int BaudRate { get; set; }

        public int DataBits { get; set; }

        public StopBits StopBits { get; set; }

        public Parity Parity { get; set; }

        public int WriteTimeout { get; set; }

        public int ReadTimeout { get; set; }
        /// <summary>
        /// 串口通讯类的构造函数
        /// </summary>
        public CRS232Comm()
        {
            Log = LogProvider.GetLogger(GetType());
            //RSPort = new SerialPort();
            mMutex = new Mutex();
        }

        public void InitialCom()
        {
            int l = 0;
            try
            {
                RSPort = new SerialPortStream();
                RSPort.PortName = PortName;
                RSPort.BaudRate = BaudRate;
                RSPort.DataBits = DataBits;
                RSPort.Parity = Parity;
                RSPort.StopBits = StopBits;
                RSPort.Close();
                RSPort.Open();
            }
            catch (Exception ex)
            {
                Log.ErrorException("串口初始化失败!", ex);
                OnPowerData(0.0, false);
                return;
            }
            if (PortOpened)
            {
                RSPort.DataReceived += new EventHandler<SerialDataReceivedEventArgs>(ReceiveData);
                OnPowerData(0.0, true);
            }

        }
        private void ReceiveData(object sender, SerialDataReceivedEventArgs args)
        {
            int readNum = RSPort.BytesToRead;
            if (readNum == 0)
                return;
            //byte[] receiveBuffer = new byte[readNum];
            byte[] readBuffer = new byte[readNum];
            int i = 0;
            while(i<readNum)
            {
                int j = RSPort.Read(readBuffer, i, readNum - i);
                i += j;
            }
            string[] DataBuffer;
            string strData = Encoding.UTF8.GetString(readBuffer);
            DataBuffer = strData.Split(';');
            if (!double.TryParse(DataBuffer[0], out m_PowerData))
                return;
            OnPowerData(m_PowerData, true);
        }

        public string ReadPort()
        {
            if (PortName == "")
                return null;
            else                       
                return  RSPort.PortName;
        }

        /// <summary>
        /// 关闭串口，释放资源
        /// </summary>
        public void ClosePort()
        {
            try
            {
                RSPort.Close();
                RSPort.Dispose();
                RSPort = null;
                mMutex.Close();
                mMutex = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  发送串口数据
        /// </summary>
        /// <param name="strText">数据内容</param>
        public void WiterPort(string strText)
        {
            try
            {
                sReceive = string.Empty;
                mMutex.WaitOne();
                RSPort.Write(strText);
                mMutex.ReleaseMutex();
                Thread.Sleep(20);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 返回读入的串口数据内容
        /// </summary>
        public string ReceiveString
        {
            get { return sReceive; }
            set { sReceive = value; }
        }
    } //class
}