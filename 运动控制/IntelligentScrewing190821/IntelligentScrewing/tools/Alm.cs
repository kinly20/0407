using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntelligentScrewing.tools
{
    class Alm
    {
        public bool[] AlmBoolList = new bool[100];
        public string[] AlmString = new string[100];
        private bool almflag;
        public ListBox AlmShow = new ListBox();
        System.Timers.Timer AlmMonitor = new System.Timers.Timer(100);
        public bool reset;
        public bool Almflag
        {
            get
            {
                almflag = false;
                for (int i = 0; i < AlmBoolList.Length; i++)
                {
                    almflag = almflag | AlmBoolList[i];
                }
                return almflag;
            }
        }
        public void LoadAlm(string path)
        {
            //List<string> alm=new List<string>();
            //string[] almstr = new string[64];
            if (!System.IO.File.Exists(path))
            {
                //alm.Add("0");
                return;
            }
            System.IO.StreamReader sr = new System.IO.StreamReader(path, System.Text.Encoding.Default);
            while (sr.Peek() != -1)
            {
                string readdata = sr.ReadLine();
                char[] spittchar = new char[] { '，', ',' };
                string[] item = readdata.Split(spittchar);
                for (int i = 0; i < 64; i++)
                {
                    if (item[0].Trim() == i.ToString())
                    {
                        AlmString[i] = readdata.Trim();
                    }
                }
                //alm.Add(item[1]);         
            }
            AlmMonitor.Elapsed += AlmMonitor_Elapsed;
            AlmMonitor.Start();
            sr.Dispose();
            sr.Close();
            // return almstr;
        }

        void AlmMonitor_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            AlmMonitor.Stop();
            for (int i = 0; i < 64;i++ )
            {
                if (AlmBoolList[i])
               {
                   if (AlmShow.FindString(AlmString[i]) == ListBox.NoMatches)
                   {
                       AlmShow.Items.Add(AlmString[i]);
                   }
               }else if(reset)
                {
                    int intIndex = AlmShow.FindString(AlmString[i]);
                    if (intIndex != ListBox.NoMatches)
                    {
                        AlmShow.Items.Remove(AlmString[i]);
                    }
                }
            }
                AlmMonitor.Start();
        }
    }
}
