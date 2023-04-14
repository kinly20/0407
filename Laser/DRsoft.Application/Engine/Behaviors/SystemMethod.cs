using DRsoft.Runtime.Core.DataBase.Common.Extentions;

namespace Engine.Behaviors
{
    public class SystemCommonFuction
    {
        public bool CheckSoftware(Dictionary<string, bool> dic)
        {
            const string pathx86 = $"C:\\Program Files (x86)";
            const string pathx64 = $"C:\\Program Files";
            var filesx86 = GetFiles(pathx86, new string[] { "*.*" });
            var filesx64 = GetFiles(pathx64, new string[] { "*.*" });
            var newdic = dic.DeepClone();
            foreach (var item in dic)
            {
                foreach (var dummy in filesx86.Where(fileitem => fileitem.Contains(item.Key)))
                {
                    newdic.Remove(item.Key);
                    newdic.Add(item.Key, true);
                }

                foreach (var dummy in filesx86.Where(fileitem => fileitem.Contains(item.Key)))
                {
                    newdic.Remove(item.Key);
                    newdic.Add(item.Key, true);
                }
            }
            return newdic.All(item => item.Value);
        }

        private static List<string> GetFiles(string directory, IEnumerable<string> patterns)
        {
            var files = new List<string>();
            foreach (var pattern in patterns)
            {
                try
                {
                    files.AddRange(Directory.GetDirectories(directory, pattern));
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            return files;
        }


        public bool ThreadExitis(string threadName, bool kill)
        {
            var bo = false;

            System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcesses();
            foreach (var process in processList)
            {
                if (!process.ProcessName.ToLower().Contains(threadName.ToLower())) continue;
                if (kill)
                {
                    bo = false;
                    process.Kill(); //结束进程 
                }
                else
                {
                    bo = true;
                }
            }

            return bo;
        }
    }
}
