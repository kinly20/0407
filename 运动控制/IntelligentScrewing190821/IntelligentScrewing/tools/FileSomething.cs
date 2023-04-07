using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentScrewing.tools
{
    class FileSomething
    {
        public  void DirectoryCheckAndCreate(string Path)
        {
            if (!System.IO.Directory.Exists(Path))
            {
                System.IO.Directory.CreateDirectory(Path);
            }
        }
        public  void FileCheckAndCreate(string Path)
        {
            if (!System.IO.File.Exists(Path))
            {
                System.IO.File.Create(Path).Close();
            }
        }
    }
}
