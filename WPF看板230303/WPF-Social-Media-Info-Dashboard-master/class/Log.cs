using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.IO;

namespace Dashboard
{
    public class Log
    {
        public static StreamWriter streamWriter;
        public static StreamReader streamReader;
        public static string path = AppDomain.CurrentDomain.BaseDirectory + "log\\";
        public static string date = System.DateTime.Now.Date.ToString("yyyyMMdd") + ".txt";
        public static object obj = new object();
        public static void writelog(string msg)
        {
            lock (obj)
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                streamWriter = new StreamWriter(path + date, true, Encoding.Default);
                streamWriter.Write(System.DateTime.Now.ToString() + " " + msg);
                streamWriter.Write("\r\n");
                streamWriter.Flush();
                streamWriter.Close();
            }
        }

        public static string[] readlog(DateTime time)
        {
            try
            {
                lock (obj)
                {
                    string cpath = path + time.Date.ToString("yyyyMMdd") + ".txt";
                    //if (!Directory.Exists(cpath))
                    //    return null;
                    streamReader = new StreamReader(cpath);
                    string dataall = streamReader.ReadToEnd();
                    return dataall.Split(',');
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
