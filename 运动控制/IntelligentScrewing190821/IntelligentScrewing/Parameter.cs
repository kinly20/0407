using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IntelligentScrewing.tools;

namespace IntelligentScrewing
{
    class Parameter
    {
        public static string ParaDisk;
        public static string ParaPath;
        public static string SysParaFile;
        public static string PlcAlarmFile;
        public static string TableFile;
        public static string ReportPath;      
        public static string WorkPicFile;
        public static string RptDataFile;  
        public static string ImagePath;
        public static string ProgFilePath;
        public static string CameraPath;
        public static string LogoPath;
        //public static string CameraFixFile;

        public static string[] SupUserMac = new string[10];
        public static string[] Para=new string[300];
        
        public static void LoadPara(int StratNum=0)
        {
            Ini.IniFile ini = new Ini.IniFile(SysParaFile);
            for (int i = 0; i < Para.Length; i++)
            {
                Para[i]=ini.IniReadValue("Para", i.ToString());
            }
        }
        public static void SavePara(string path,int StratNum=0)
        {
            Ini.IniFile ini = new Ini.IniFile(path);
            for (int i = 0; i < Para.Length; i++)
            {
                ini.IniWriteValue("Para", i.ToString(), Para[i]);
               // ini.IniReadValue("Para", i.ToString());
            }
        }
       
    
    }
}
