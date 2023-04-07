using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MyControl.tools;
using MyControl.Communication;
using System.Net.Sockets;
using System.Diagnostics;

namespace ICD.ICDUserControls
{
    public partial class HoneywellScanner : UserControl
    {

        public HoneywellScanner()
        {
            InitializeComponent();
            trigerScan.RisingEdge += ScanStart;
            //this.plc = PLC;
        }

        //private PLC plc;

        public bool ScannerOK = false;

        /// <summary>
        /// PLC触发地址
        /// </summary>
        public int[] TrigAddress = new int[2];
        /// <summary>
        /// 扫码完成地址
        /// </summary>
        public int[] AckAddress = new int[2];
        /// <summary>
        /// 扫码失败地址
        /// </summary>
        public int[] FailAddress = new int[2];
        /// <summary>
        /// 扫码成功地址
        /// </summary>
        public int[] SucessAddress = new int[2];
        /// <summary>
        /// 条码写入PLC起始地址
        /// </summary>
        public int WriteCodeAddress;
        /// <summary>
        /// 条码读取PLC起始地址
        /// </summary>
        public int ReadCodeAddress;

        public string FailString = "NR";
        private TcpClient scanner;
        private Trag trigerScan = new Trag();
        private byte[] buffer = new byte[1024];

        public string RemoteIP { get; set; }

        public event Action DoSomething;
        public int Type { get; set; }
        public string BarCode { get; set; }

        private void ScanStart(object sender, EventArgs e)
        {
            if (!bgwScanner.IsBusy)
                bgwScanner.RunWorkerAsync();
        }

        #region 连接
        private void btnLink_Click(object sender, EventArgs e)
        {
            Initial();
        }
        public void Initial()
        {
            if (RemoteIP == null)
                RemoteIP = txtIP.Text;
            else
                txtIP.Text = RemoteIP;
            scanner = new TcpClient();
            try
            {
                scanner.Connect(RemoteIP, int.Parse(txtPort.Text));//连接服务端
                scanner.GetStream().BeginRead(buffer, 0, buffer.Length, new AsyncCallback(TCPCallBack), this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("扫码枪连接失败！");
            }
        }
        private void DisConnect()
        {
            scanner.Close();
        }
        #endregion

        #region 发送数据
        private void button1_Click(object sender, EventArgs e)
        {
            SendData();
        }

        private void SendData(string str = "T")
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(str);
            try
            {
                scanner.GetStream().Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        #endregion


        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="ar"></param>
        private void TCPCallBack(IAsyncResult ar)
        {
            HoneywellScanner client = (HoneywellScanner)ar.AsyncState;
            if (client.scanner.Connected)
            {
                NetworkStream ns = client.scanner.GetStream();
                byte[] recdata = new byte[ns.EndRead(ar)];
                Array.Copy(client.buffer, recdata, recdata.Length);
                if (recdata.Length > 0)
                {
                    ASCIIEncoding Encoding = new ASCIIEncoding();
                    BarCode = Encoding.GetString(recdata);
                    lboxScan.Invoke(new EventHandler(delegate
                    {
                        lboxScan.Items.Add(BarCode);
                    }));

                    ns.BeginRead(client.buffer, 0, client.buffer.Length, new AsyncCallback(TCPCallBack), client);
                }
                else
                {
                    client.DisConnect();
                }
            }
        }

        private void timerTrig_Tick(object sender, EventArgs e)
        {
            //trigerScan.Value = dataChange.GetBitValue(plc.plcNet.MW0[TrigAddress[0]], TrigAddress[1]);
            if (scanner != null)
            {
                btnLink.BackColor = scanner.Connected ? Color.LimeGreen : BackColor;
                btnLink.Text = scanner.Connected ? "已连接" : "连接";
            }
        }

        private void writeCodeToPlc(string barcodeString, int address)
        {
            if (address == 0)
                return;
            ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
            var codeBytes = aSCIIEncoding.GetBytes(barcodeString);
            byte[] tempBytes;
            if (codeBytes.Length % 2 > 0)
            {
                tempBytes = new byte[codeBytes.Length + 1];
            }
            else
            {
                tempBytes = new byte[codeBytes.Length];
            }

            Array.Copy(codeBytes, tempBytes, codeBytes.Length);

            var codeData = NetConvert.BytesToShorts(tempBytes, 0, tempBytes.Length / 2, ByteOrder.None);

            //plc.plcNet.MW0[address] = (short)codeBytes.Length;
            //Array.Copy(codeData, 0, plc.plcNet.MW0, address + 1, codeData.Length);
            //plc.plcNet.Write(address, 50, plc.plcNet.MW0);
        }

        private void bgwScanner_DoWork(object sender, DoWorkEventArgs e)
        {
            BarCode = null;
            SendData();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (BarCode == null && sw.ElapsedMilliseconds < 50000)
            {
                Thread.Sleep(200);
            }
            if (BarCode != null)
            {
                ScannerOK = true;
                writeCodeToPlc(BarCode, WriteCodeAddress);

                //
                //dataChange.SetBitValue(plc.plcNet.MW0[SucessAddress[0]], (ushort)SucessAddress[1], true);
                //dataChange.SetBitValue(ref plc.plcNet.MW0[FailAddress[0]], (ushort)FailAddress[1], false);

            }
            else
            {
                ScannerOK = false;
                //    dataChange.SetBitValue(ref plc.plcNet.MW0[FailAddress[0]], (ushort)FailAddress[1],true);
                //    dataChange.SetBitValue(plc.plcNet.MW0[SucessAddress[0]], (ushort)SucessAddress[1], false);
            }
            DoSomething.Invoke();
            //dataChange.SetPlcPulse(ref plc.plcNet.MW0[AckAddress[0]], (ushort)AckAddress[1]);
        }

        private void onekeyread_Click(object sender, EventArgs e)
        {
            ICD.Class.Scan sca = new Class.Scan();
            string back = sca.GetBarcode();
            MessageBox.Show(back);
        }
    }
}
