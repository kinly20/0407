using Newtonsoft.Json;
using System.Windows.Forms;
using static Engine.Transfer.HardwareUtility;
using LicenseManager = DRsoft.Runtime.Core.Platform.Crypt.LicenseManager;
using Engine.Transfer;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Engine
{
    public partial class RegisterKey : Form
    {
        public RegisterKey()
        {
            InitializeComponent();
            txtSysID.Text = AtapiDevice.GetHddInfo(0);
        }

        private void btnAuth_Click(object sender, EventArgs e)
        {
            if (txtSysID.Text.Trim() == "")
            {
                MessageBox.Show("当前平台系统ID不能为空,请联系研发部门");
                return;
            }

            DataClass dataClass = new DataClass();
            dataClass.SysID = txtSysID.Text;
            string EncryptInfo = "";
            try
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "EncryptInfo.json");
                if (File.Exists(filePath)) File.Delete(filePath);
                EncryptInfo = JsonConvert.SerializeObject(dataClass);
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(EncryptInfo);
                    fs.Write(buffer, 0, buffer.Length);
                }
            }
            catch
            {
                MessageBox.Show("授权文件生成失败");
                return;
            }

            MessageBox.Show("授权文件生成成功,软件目录下EncryptInfo.json文件");
        }

        private void LicenseActive_Click(object sender, EventArgs e)
        {
            if (DecryLicense.Text == "")
            {
                MessageBox.Show("请先选择授权申请文件");
                return;
            }

            try
            {
                string EncryptFile = DecryLicense.Text;
                if (LicenseManager.Instance.IsValid(EncryptFile))
                {
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "EncryptInfo.dat");
                    using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                    {
                        using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                        {
                            sw.Write(EncryptFile);
                        }
                    }

                    MessageBox.Show("创建授权审批文件成功,软件目录下EncryptInfo.dat文件");
                }
                else
                {
                    MessageBox.Show("授权文件无效");
                }
            }
            catch
            {
                MessageBox.Show("创建授权审批文件失败");
            }
        }
    }
}