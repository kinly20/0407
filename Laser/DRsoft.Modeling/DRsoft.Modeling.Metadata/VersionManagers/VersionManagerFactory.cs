using System.IO;

namespace DRsoft.Modeling.Metadata.VersionManagers
{
    /// <summary>
    /// 版本管理工厂
    /// </summary>
    internal static class VersionManagerFactory
    {
        private static readonly object s_lock = new object();
        public static volatile IVersionManager VersionManage;

        /// <summary>
        /// 创建版本管理实例
        /// </summary>
        /// <returns>版本管理实例</returns>
        public static IVersionManager Create()
        {
            if (VersionManage == null)
            {
                lock (s_lock)
                {
                    if (VersionManage == null)
                        VersionManage = CreateVersionManager();
                }
            }
            return VersionManage;
        }

        private static IVersionManager CreateVersionManager()
        {
            IVersionManager manager = new NullVersionManager();
            return manager;
        }

        internal static string GitPath(string path)
        {

            string dir = path, gitLocalUrl = "";
            bool bDone = true;
            do
            {
                if (Directory.Exists(dir))
                {

                    var dirs = Directory.GetDirectories(dir, ".git", SearchOption.TopDirectoryOnly);
                    if (dirs.Length > 0)
                    {
                        gitLocalUrl = dir;
                        bDone = false;
                    }

                    if (bDone)
                    {
                        DirectoryInfo info = new DirectoryInfo(dir);

                        if (info.Parent == null)
                        {
                            break;
                        }

                        dir = info.Parent.FullName;

                        if (dir == info.Root.FullName)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    bDone = false;
                }

            } while (bDone);

            return gitLocalUrl;
        }
    }
}
