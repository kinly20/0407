using System.Runtime.InteropServices;

namespace Engine.Transfer
{
    public class IniConfigure
    {
        private string _fileName = "AlarmMessage.ini";
        private string _filePath = System.AppDomain.CurrentDomain.BaseDirectory + @"\";

        public IniConfigure(string filePath, string fileName)
        {
            _fileName = fileName;
            _filePath = filePath;
        }

        public IniConfigure(string filename)
        {
            _fileName = filename;
        }

        public string FilePath
        {
            set => _filePath = value;
        }

        public string FileName
        {
            set => _fileName = value;
        }

        /// <summary>
        /// 读取string类型数据
        /// </summary>
        /// <param name="strAppName"></param>
        /// <param name="strKeyName"></param>
        /// <param name="strDefault"></param>
        /// <returns></returns>
        public string ReadConfig(string strAppName, string strKeyName, string strDefault)
        {
            return ReadConfig(strAppName, strKeyName, strDefault, _filePath + _fileName);
        }

        public double ReadConfig(string strAppName, string strKeyName, double dDefault)
        {
            string result = ReadConfig(strAppName, strKeyName, dDefault.ToString(), _filePath + _fileName);
            return StringToDouble(result, dDefault);
        }

        public float ReadConfig(string strAppName, string strKeyName, float dDefault)
        {
            string result = ReadConfig(strAppName, strKeyName, dDefault.ToString(), _filePath + _fileName);
            return StringToFloat(result, dDefault);
        }

        public string ReadConfig(string strAppName, string strKeyName, string strDefault, string strFilepath)
        {
            StringBuilder strReturn = new StringBuilder(1024);
            GetPrivateProfileString(strAppName, strKeyName, strDefault, strReturn, 1024, strFilepath);
            return strReturn.ToString();
        }

        /// <summary>
        /// 读取int类型数据
        /// </summary>
        /// <param name="strAppName"></param>
        /// <param name="strKeyName"></param>
        /// <param name="ndefault"></param>
        /// <returns></returns>
        public int ReadConfig(string strAppName, string strKeyName, int ndefault)
        {
            return ReadConfig(strAppName, strKeyName, ndefault, _filePath + _fileName);
        }

        public bool ReadConfig(string strAppName, string strKeyName, bool bdefault)
        {
            string result = ReadConfig(strAppName, strKeyName, bdefault.ToString(), _filePath + _fileName);
            return StringToBool(result, bdefault);
        }

        public int ReadConfig(string strAppName, string strKeyName, int ndefault, string strFilepath)
        {
            int result = GetPrivateProfileInt(strAppName, strKeyName, ndefault, strFilepath);
            return result;
        }

        /// <summary>
        /// 写入string类型数据
        /// </summary>
        /// <param name="strAppName"></param>
        /// <param name="strKeyName"></param>
        /// <param name="strString"></param>
        /// <returns></returns>
        public bool WriteConfig(string strAppName, string strKeyName, string strString)
        {
            return WritePrivateProfileString(strAppName, strKeyName, strString, _filePath + _fileName);
        }

        public bool WriteConfig(string strAppName, string strKeyName, string strString, string strFilepath)
        {
            return WritePrivateProfileString(strAppName, strKeyName, strString, strFilepath);
        }

        /// <summary>
        /// 写入int类型数据
        /// </summary>
        /// <param name="strAppName"></param>
        /// <param name="strKeyName"></param>
        /// <param name="strString"></param>
        /// <returns></returns>
        public bool WriteConfig(string strAppName, string strKeyName, int strString)
        {
            return WritePrivateProfileString(strAppName, strKeyName, strString.ToString(), _filePath + _fileName);
        }

        public bool WriteConfig(string strAppName, string strKeyName, int strString, string strFilepath)
        {
            return WritePrivateProfileString(strAppName, strKeyName, strString.ToString(), strFilepath);
        }

        public string[] GetAllSectionNames()
        {
            return INIGetAllSectionNames(_filePath + _fileName);
        }

        public static double StringToDouble(string strValue, double Fail)
        {
            double result = Fail;
            if (strValue == null || strValue == "")
            {
                return result;
            }

            if (!double.TryParse(strValue, out result))
            {
                result = Fail;
            }

            return result;
        }

        public static float StringToFloat(string strValue, float Fail)
        {
            float result = Fail;
            if (strValue == null || strValue == "")
            {
                return result;
            }

            if (!float.TryParse(strValue, out result))
            {
                result = Fail;
            }

            return result;
        }

        public static int StringToInt(string strValue, int Fail)
        {
            int result = Fail;
            if (strValue == null || strValue == "")
            {
                return result;
            }

            if (!int.TryParse(strValue, out result))
            {
                result = Fail;
            }

            return result;
        }

        public static bool StringToBool(string strValue, bool Fail)
        {
            bool result = Fail;
            if (strValue == null || strValue == "")
            {
                return result;
            }

            if (!bool.TryParse(strValue, out result))
            {
                result = Fail;
            }

            return result;
        }

        /// <summary>
        /// 读取INI文件中指定INI文件中的所有节点名称(Section)
        /// </summary>
        /// <param name="iniFile">Ini文件</param>
        /// <returns>所有节点,没有内容返回string[0]</returns>
        public static string[] INIGetAllSectionNames(string iniFile)
        {
            uint MAX_BUFFER = 32767; //默认为32767 
            string[] sections = new string[0]; //返回值 
            //申请内存 
            IntPtr pReturnedString = Marshal.AllocCoTaskMem((int)MAX_BUFFER * sizeof(char));
            uint bytesReturned = GetPrivateProfileSectionNames(pReturnedString, MAX_BUFFER, iniFile);
            if (bytesReturned != 0)
            {
                //读取指定内存的内容 
                string local = Marshal.PtrToStringAuto(pReturnedString, (int)bytesReturned).ToString();
                //每个节点之间用\0分隔,末尾有一个\0
                sections = local.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            }

            //释放内存 
            Marshal.FreeCoTaskMem(pReturnedString);
            return sections;
        }

        [DllImport("Kernel32.dll")]
        private static extern ulong GetPrivateProfileString(string strAppName, string strKeyName, string strDefault,
            StringBuilder sbReturnString, int nSize, string strFileName);

        [DllImport("Kernel32.dll")]
        public static extern bool WritePrivateProfileString(string strAppName, string strKeyName, string strString,
            string strFileName);

        [DllImport("Kernel32.dll")]
        private static extern int GetPrivateProfileInt(string strAppName, string strKeyName, int nDefault,
            string strFileName);


        /// <summary>
        /// 获取所有节点名称(Section)
        /// </summary>
        /// <param name="lpszReturnBuffer">存放节点名称的内存地址,每个节点之间用\0分隔</param>
        /// <param name="nSize">内存大小(characters)</param>
        /// <param name="lpFileName">Ini文件</param>
        /// <returns>内容的实际长度,为0表示没有内容,为nSize-2表示内存大小不够</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint
            GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, uint nSize, string lpFileName);
    }
}