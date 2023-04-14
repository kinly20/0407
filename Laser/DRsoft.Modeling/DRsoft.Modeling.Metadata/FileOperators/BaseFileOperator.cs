using DRsoft.Modeling.Metadata.Caches;
using DRsoft.Modeling.Metadata.Interfaces;
using DRsoft.Modeling.Metadata.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DRsoft.Modeling.Metadata.FileOperators
{
    /// <summary>
    /// 文件操作基类
    /// </summary>
    /// <typeparam name="T">元数据类型</typeparam>
    internal abstract class BaseFileOperator<T> where T : IModelKey
    {
        /// <summary>
        /// 元数据存储的目录
        /// </summary>
        protected string MetadataDirectory { get; }

        protected BaseFileOperator()
        {
            Type targetType = typeof(T);
            var cacheType = targetType.GetCustomAttribute<CacheProviderTypeAttribute>();
            MetadataDirectory = string.IsNullOrEmpty(cacheType.Directory) ? targetType.Name : cacheType.Directory;
        }

        /// <summary>
        ///  删除元数据文件
        /// </summary>
        /// <remarks>
        /// 文件存在，则移除只读后删除
        /// </remarks>
        /// <param name="filePath">元数据文件地址</param>
        protected void DeleteMetadataFile(string filePath)
        {
            MetadataStore.Delete(filePath);
        }

        /// <summary>
        /// 加载元数据(有设计时)
        /// </summary>
        /// <param name="directory">目录</param>
        /// <param name="isDesign">是否设计时</param>
        /// <param name="action">回调方法</param>
        public virtual void LoadMetadata(string directory, bool isDesign, Action<T> action)
        {
            string directoryPath = Path.Combine(string.IsNullOrEmpty(MetadataFileConstants.MetadataDirectory)
                ? System.Environment.CurrentDirectory
                : MetadataFileConstants.MetadataDirectory, directory, MetadataDirectory);

            if (Directory.Exists(directoryPath) == false)
            {
                return;
            }
            string fileExtension = isDesign ? MetadataFileConstants.DesignFileExtension : MetadataFileConstants.RuntimeFileExtension;
            IEnumerable<string> files = MetadataStore.EnumerateFiles(directoryPath, "*" + fileExtension, SearchOption.AllDirectories);
            var list = files.AsParallel().Select(MetadataStore.Get<T>).ToList();
            foreach (var item in list)
            {
                if (item != null)
                    action(item);
            }
        }

        /// <summary>
        /// 根据文件加载元数据
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <returns>元数据类型</returns>
        public T LoadMetadata(string file)
        {
            if (string.IsNullOrEmpty(file))
            {
                throw new ArgumentNullException(nameof(file));
            }
            //T metadata = XmlHelper.XmlDeserializeFromFile<T>(file);
            T metadata = MetadataStore.Get<T>(file);
            return metadata;
        }

        /// <summary>
        /// 获取元数据文件路径
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        /// <param name="isDesign">是否设计时</param>
        /// <returns>元数据文件路径</returns>
        public abstract string GetFilePath(Guid metadataId, bool isDesign);

        public abstract void SaveDesignFile(T metadata);

        /// <summary>
        /// 拷贝源路径到目标路径
        /// </summary>
        /// <param name="sourcePath">源路径</param>
        /// <param name="targetPath">目标路径</param>
        public void CopyFile(string sourcePath, string targetPath)
        {
            //源路径为空
            if (sourcePath == null)
            {
                throw new ArgumentNullException(nameof(sourcePath));
            }
            //目标路径为空
            if (targetPath == null)
            {
                throw new ArgumentNullException(nameof(targetPath));
            }
            //获取路径目录
            string dir = Path.GetDirectoryName(targetPath);

            //如果目录为空
            if (dir == null)
            {
                throw new InvalidOperationException($"文件路径{targetPath}不正确。");
            }
            //如果不存在目录，则创建目录
            if (Directory.Exists(dir) == false)
            {
                Directory.CreateDirectory(dir);
            }
            //如果存在目标文件
            if (File.Exists(targetPath))
            {
                File.SetAttributes(targetPath, FileAttributes.Normal);
            }
            MetadataStore.Copy(sourcePath, targetPath);
        }

        /// <summary>
        /// 删除设计时文件
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        public void DeleteDesignFile(Guid metadataId)
        {
            string designFilePath = GetFilePath(metadataId, true);
            // 删除设计时文件
            DeleteMetadataFile(designFilePath);
        }

        /// <summary>
        /// 确认删除元数据
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        /// <param name="checkInMessage"></param>
        public void ConfirmDelete(Guid metadataId, string checkInMessage)
        {
            string filePath = GetFilePath(metadataId, false);
            // 删除运行时文件
            DeleteMetadataFile(filePath);
        }

        /// <summary> 
        /// 提交元数据
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        /// <param name="checkInMessage">签入信息</param>
        public string Submit(Guid metadataId, string checkInMessage)
        {
            string filePath = GetFilePath(metadataId, false);
            string designFilePath = GetFilePath(metadataId, true);

            // 拷贝设计时文件到运行时文件
            CopyFile(designFilePath, filePath);

            // 删除设计时文件
            DeleteMetadataFile(designFilePath);

            return filePath;
        }

        /// <summary>
        /// 是否存在运行时文件
        /// </summary>
        /// <param name="metadataId">元数据id</param>
        /// <returns>是否存在运行时文件</returns>
        public bool HasRuntimeFile(Guid metadataId)
        {
            //获取运行时文件路径
            string file = GetFilePath(metadataId, false);
            return File.Exists(file);
        }

        /// <summary>
        /// 是否为产品元数据文件（是否在产品目录存在元数据文件）
        /// </summary>
        /// <param name="metadataId"></param>
        /// <returns></returns>
        public virtual bool IsProduct(Guid metadataId)
        {
            return true;
        }

        public virtual string GetAppCode(string filepath)
        {
            return "0000";
        }

        /// <summary>
        /// 获取元数据文件Hash
        /// </summary>
        /// <param name="metadataId"></param>
        /// <param name="isDesign"></param>
        /// <returns></returns>
        public virtual long GetFileHash(Guid metadataId, bool isDesign)
        {
            long hash = 0;
            var metadataFile = GetFilePath(metadataId, isDesign);
            if (File.Exists(metadataFile))
            {
                hash = GetFileTextHash(metadataFile);
            }
            return hash;
        }

        /// <summary>
        /// 获取文件内容Hash
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        protected long GetFileTextHash(string file)
        {
            if (File.Exists(file) == false) return 0;
            var hashCodeCombiner = new HashCodeCombiner();
            var text = File.ReadAllText(file);
            hashCodeCombiner.AddObject(text);
            return hashCodeCombiner.CombinedHash;
        }
    }
}
