using DRsoft.Modeling.Metadata.FileOperators;
using DRsoft.Runtime.Core.Common.Xml;
using System;
using System.Collections.Generic;
using System.IO;

namespace DRsoft.Modeling.Metadata.Publish
{
    /// <summary>
    /// 包发布管理类
    /// </summary>
    internal class PackageManager
    {
        private static object s_lockObj = new object();

        /// <summary>
        /// 缓存文件路径
        /// </summary>
        private static readonly string s_FilePath = Path.Combine(MetadataFileConstants.MetadataDirectory, MetadataFileConstants.ProductDirectory, $"PublishLog{MetadataFileConstants.RuntimeFileExtension}");

        private List<Package> GetPackage()
        {
            List<Package> list;

            if (File.Exists(s_FilePath))
            {
                list = XmlHelper.XmlDeserializeFromFile<List<Package>>(s_FilePath);
            }
            else
            {
                list = new List<Package>();
            }

            return list;
        }

        /// <summary>
        /// 发布一个包
        /// </summary>
        /// <param name="package">包对象</param>
        public void PublicPackage(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException(nameof(package));
            }

            lock (s_lockObj)
            {
                var list = GetPackage();

                list.Add(package);

                if (File.Exists(s_FilePath))
                {
                    File.SetAttributes(s_FilePath, FileAttributes.Normal);
                }
                XmlHelper.XmlSerializeToFile(list, s_FilePath);
            }
        }
    }
}
