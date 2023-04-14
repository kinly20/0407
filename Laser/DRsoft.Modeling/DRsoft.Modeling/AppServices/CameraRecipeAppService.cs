using System;
using DRsoft.Modeling.Metadata.Managers.Config;
using DRsoft.Modeling.Metadata.Models.Config;
using DRsoft.Modeling.Metadata.Status;
using Modle = DRsoft.Modeling.Metadata.Models;

namespace DRsoft.Modeling.AppServices
{
    public class CameraRecipeAppService
    {
        private CameraRecipeManager metadataManager;
        public Guid MetadataGuid { get; set; } =new Guid("A0FD7AEB-8B5B-D2E9-7DF2-051B73BED541");

        public CameraRecipeAppService()
        {
            metadataManager = CameraRecipeManager.GetInstance();
        }
        public string GetFilePath(Guid id,bool isDesign)
        {
            string startpath = AppDomain.CurrentDomain.BaseDirectory;
            return startpath+metadataManager.GetFilePath(id, isDesign);
        }
        public CamRecipeConfig? Read()
        {
            return metadataManager.GetMetadataItemEx(MetadataGuid)?.Metadata as CamRecipeConfig;
        }
        public void InsertNewFile(CamRecipeConfig metadata)
        {
            metadataManager.Insert(metadata);
        }
        /// <summary>
        /// 签出
        /// </summary>
        /// <returns></returns>
        public CamRecipeConfig? CheckOut()
        {
            //1、签出文件
            metadataManager.CheckOut(MetadataGuid);
            //2、返回实体
            return metadataManager.GetMetadataItemEx(MetadataGuid)?.Metadata as CamRecipeConfig;
        }
        private string newRecipePath;
        public void CreatRecipeFile(Guid OriginalGuid, Guid newGuid,out string newRecipePath)
        {
            //复制 签出新文件
            metadataManager.CreatRecipeFile(OriginalGuid, newGuid,out newRecipePath);
        }
        public void DeleteRecipeFile(string path)
        {
            metadataManager.DeleteRecipeFile(path);
        }
        /// <summary>
        /// 取消签出(撤销)
        /// </summary>
        public void CancleCheckOut()
        {
            //1、签出文件
            metadataManager.CancelCheckOut(MetadataGuid);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="engine"></param>
        public void Save(CamRecipeConfig engine)
        {
            if (engine == null) return;

            //1、更新实体
            metadataManager.Update(engine);
            //2、提交保存
            metadataManager.Submit(metadataManager.GetStatusItem(engine, StatusType.Modified), "");
        }
    }
}
