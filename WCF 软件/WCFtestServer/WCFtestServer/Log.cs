using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections;

namespace WCFtestServer
{
    public class Log
    {
        //记录系统日志的类
        private Thread threadWriteLog = null;
        private Thread threadReadLog = null;
        //private Thread threadDebug = null;
        private static Mutex mutex = null;

        public string msg = "";
        public DateTime dte = DateTime.Now;
        public string fName = "";
        public ArrayList sLog = null;
        
        public Log()
        {
            mutex = new Mutex();
            threadWriteLog = new Thread(new ThreadStart(WriteLog));
            threadReadLog = new Thread(new ThreadStart(ReadLog));

            threadWriteLog.Start();
            threadReadLog.Start();
        }

        //互斥锁
        public void WriteLog()
        {
            lock (this)
            {
                try
                {
                    mutex.WaitOne();
                    WriteLog(msg);
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
        }

        public void ReadLog()
        {
            lock (this)
            {
                try
                {
                    mutex.WaitOne();
                    sLog = ReadLog(fName);
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
        }

        public void DebugData()
        {
            lock (this)
            {
                try
                {
                    mutex.WaitOne();
                    WriteDebugData(msg);
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
        }

        private void WriteLog(string msg)
        {
            string folder = Process.GetCurrentProcess().MainModule.FileName;
            folder = folder.Substring(0, folder.LastIndexOf("\\") + 1) + "Log";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string FileName = folder + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

            //判断是否存在文件
            if (!File.Exists(FileName))
            {
                //如果文件不存在，则创建文件后向文件添加日志                
                StreamWriter sw = File.CreateText(FileName);
                if (msg != "")
                {
                    sw.WriteLine(dte.ToString("yyyy-MM-dd HH:mm:ss.fff") + "                                     " + msg);                   
                }
                sw.Close();
                sw.Dispose();
            }
            else
            {
                //如果存在文件，则向文件添加日志
                using (StreamWriter sr = new StreamWriter(FileName, true))
                {
                    if (msg != "")
                    {
                        sr.WriteLine(dte.ToString("yyyy-MM-dd HH:mm:ss.fff") + "                                     " + msg);                       
                    }
                    sr.Close();
                    sr.Dispose();
                }
            }
        }

        private ArrayList ReadLog(string fName)
        {
            string folder = Process.GetCurrentProcess().MainModule.FileName;
            folder = folder.Substring(0, folder.LastIndexOf("\\") + 1) + "Log";
            string fn = folder + "\\" + fName + ".txt";

            ArrayList arr = new ArrayList();
            //判断是否存在文件
            if (File.Exists(fn))
            {
                //如果存在文件，则向文件添加日志                
                StreamReader sw = new StreamReader(fn, true);
                while (sw.Peek() != -1)
                {
                    arr.Add(sw.ReadLine());
                }                
                sw.Close();
                sw.Dispose();
            }
            return arr;
        }

        private void WriteDebugData(string msg)
        {
            string folder = Process.GetCurrentProcess().MainModule.FileName;
            folder = folder.Substring(0, folder.LastIndexOf("\\") + 1) + "DebugData";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string FileName = folder + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

            //判断是否存在文件
            if (!File.Exists(FileName))
            {
                //如果文件不存在，则创建文件后向文件添加日志                
                StreamWriter sw = File.CreateText(FileName);
                if (msg != "")
                {
                    sw.WriteLine(msg);
                }
                sw.Close();
                sw.Dispose();
            }
            else
            {
                //如果存在文件，则向文件添加日志
                using (StreamWriter sr = new StreamWriter(FileName, true))
                {
                    if (msg != "")
                    {
                        sr.WriteLine(msg);
                    }
                    sr.Close();
                    sr.Dispose();
                }
            }
        }
    }
}
