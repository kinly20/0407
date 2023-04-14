using System;
using DRsoft.Modeling.Metadata.Managers.Config;
using DRsoft.Modeling.Metadata.Models.Config;
using DRsoft.Modeling.Metadata.Status;
using Modle = DRsoft.Modeling.Metadata.Models;

namespace DRsoft.Modeling.AppServices
{
    public class VisionCalibrationAppService
    {
        private VisionCalibrationManager metadataManager;
        public Guid MetadataGuid { get; set; } =new Guid("FF5BBB55-F312-4F99-9188-8A63C434FC98");

        public VisionCalibrationAppService()
        {
            metadataManager = VisionCalibrationManager.GetInstance();
        }
        public string GetFilePath(Guid id,bool isDesign)
        {
            string startpath = AppDomain.CurrentDomain.BaseDirectory;
            return startpath+metadataManager.GetFilePath(id, isDesign);
        }
        public VisionCalibrationConfig? Read()
        {
            return metadataManager.GetMetadataItemEx(MetadataGuid)?.Metadata as VisionCalibrationConfig;
        }
        public void InsertNewFile(VisionCalibrationConfig metadata)
        {
            metadataManager.Insert(metadata);
        }
        /// <summary>
        /// 签出
        /// </summary>
        /// <returns></returns>
        public VisionCalibrationConfig? CheckOut()
        {
            //1、签出文件
            metadataManager.CheckOut(MetadataGuid);
            //2、返回实体
            return metadataManager.GetMetadataItemEx(MetadataGuid)?.Metadata as VisionCalibrationConfig;
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
        public void Save(VisionCalibrationConfig engine)
        {
            if (engine == null) return;

            //1、更新实体
            metadataManager.Update(engine);
            //2、提交保存
            metadataManager.Submit(metadataManager.GetStatusItem(engine, StatusType.Modified), "");
        }
    }
}
