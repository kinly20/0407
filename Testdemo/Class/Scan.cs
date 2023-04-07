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
using System.Net.Sockets;

namespace ICD.Class
{
    public class Scan
    {
        private TcpClient scanner;
        private byte[] buffer = new byte[1024];
        public string BarCode = string.Empty;

        public bool Connect()
        {
            Initial();
            return true;
        }

        public void SendData(string str = "T")
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

        //一键读取
        public string GetBarcode()
        {
            Connect();
            SendData();
            Thread.Sleep(100);
            for (int i = 0; i < 30; i++)
            {
                if (!string.IsNullOrEmpty(BarCode))
                {
                    string back = BarCode;
                    BarCode = string.Empty;
                    return back;
                }
                Thread.Sleep(1000);
            }
            MessageBox.Show("读码失败");
            return string.Empty;

        }

        private void DisConnect()
        {
            scanner.Close();
        }

        public void Initial()
        {
            string IP = System.Configuration.ConfigurationManager.AppSettings["ScanIP"];
            string Port = System.Configuration.ConfigurationManager.AppSettings["ScanPort"];
            scanner = new TcpClient();
            try
            {
                scanner.Connect(IP, int.Parse(Port));//连接服务端
                scanner.GetStream().BeginRead(buffer, 0, buffer.Length, new AsyncCallback(TCPCallBack), this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("扫码枪连接失败！");
            }
        }

        private void TCPCallBack(IAsyncResult ar)
        {
            Scan client = (Scan)ar.AsyncState;
            if (client.scanner.Connected)
            {
                NetworkStream ns = client.scanner.GetStream();
                byte[] recdata = new byte[ns.EndRead(ar)];
                Array.Copy(client.buffer, recdata, recdata.Length);
                if (recdata.Length > 0)
                {
                    ASCIIEncoding Encoding = new ASCIIEncoding();
                    BarCode = Encoding.GetString(recdata);
                    //lboxScan.Invoke(new EventHandler(delegate
                    //{
                    //    lboxScan.Items.Add(BarCode);
                    //}));

                    ns.BeginRead(client.buffer, 0, client.buffer.Length, new AsyncCallback(TCPCallBack), client);
                }
                else
                {
                    client.DisConnect();
                }
            }
        }
    }
}
