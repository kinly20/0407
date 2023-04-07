using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace ThreeAxisTable.tools
{
    class Ini
    {
        public class IniFile
        {
            public string path;             //INI文件名   

            [DllImport("kernel32")]
            public static extern long WritePrivateProfileString(string section, string key,
                        string val, string filePath);

            [DllImport("kernel32")]
            public static extern int GetPrivateProfileString(string section, string key, string def,
                        StringBuilder retVal, int size, string filePath);

            //声明读写INI文件的API函数   
            public IniFile(string INIPath)
            {
                path = INIPath;
                if(!File.Exists(path))
                {
                    File.Create(path);
                }
            }

            //类的构造函数，传递INI文件名   
            public void IniWriteValue(string Section, string Key, string Value)
            {
                WritePrivateProfileString(Section, Key, Value, this.path);
            }

            //写INI文件   
            public string IniReadValue(string Section, string Key)
            {
                StringBuilder temp = new StringBuilder(255);
                int i = GetPrivateProfileString(Section, Key, "0", temp, 255, this.path);
                return temp.ToString();
            }
            public void Create(string dir)
            {
               if(!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            }
        }

    }
}
