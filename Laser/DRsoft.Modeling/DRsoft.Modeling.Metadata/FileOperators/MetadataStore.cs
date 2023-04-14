using DRsoft.Runtime.Core.Common.Xml;
using System.Collections.Generic;
using System.IO;

namespace DRsoft.Modeling.Metadata.FileOperators
{
    internal static class MetadataStore
    {
#if NCRUNCH || TEST
        private static readonly ThreadLocal<Dictionary<string, string>> _store = new ThreadLocal<Dictionary<string, string>>(() => new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase));
#endif

        public static void Store(string filePath, object value)
        {

#if NCRUNCH || TEST
            _store.Value[filePath]= XmlHelper.XmlSerialize(value);
#else
            XmlHelper.XmlSerializeToFile(value, filePath);
#endif

        }

        public static void Copy(string sourcePath, string targetPath)
        {
#if NCRUNCH || TEST
            if (_store.Value.TryGetValue(sourcePath, out var value))
            {
                _store.Value[targetPath] = value;
                return;
            }

#endif
            //拷贝文件
            File.Copy(sourcePath, targetPath, true);
            //设置文件属性
            File.SetAttributes(targetPath, FileAttributes.Normal); 
        }

        public static T Get<T>(string filePath)
        {
#if NCRUNCH || TEST
            if (_store.Value.TryGetValue(filePath, out var value))
            {
              return  XmlHelper.XmlDeserialize<T>(value);
            }

            if (File.Exists(filePath))
            {
                return XmlHelper.XmlDeserializeFromFile<T>(filePath);
            }

            return default(T);
#else
            return XmlHelper.XmlDeserializeFromFile<T>(filePath);
#endif
        }

        public static IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
        {
#if NCRUNCH || TEST
            foreach (var key in _store.Value.Keys.Where(key => key.StartsWith(path)))
            {
                yield return key;
            }

          var files=  Directory.EnumerateFiles(path, searchPattern, searchOption);
          foreach (var file in files)
          {
              if (_store.Value.ContainsKey(file) == false)
                  yield return file;
          }
#else
            return Directory.EnumerateFiles(path, searchPattern, searchOption);
#endif
        }


        public static bool Exists(string filePath)
        {
#if NCRUNCH || TEST
            if (_store.Value.ContainsKey(filePath))
                return true;
#endif
            return File.Exists(filePath);
        }


        public static void Delete(string filePath)
        {
#if NCRUNCH || TEST
            _store.Value.Remove(filePath);
#else
            if (File.Exists(filePath))
            {
                // 去只读（TFS切换到离线模式后会出现只读文件无法删除的情况）
                File.SetAttributes(filePath, FileAttributes.Normal);
                File.Delete(filePath);
            }
#endif
        }
    }
}
