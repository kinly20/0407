using System;
using DRsoft.Modeling.Metadata.Managers.Config;
using DRsoft.Modeling.Metadata.Models.Config;
using DRsoft.Modeling.Metadata.Status;
using Modle = DRsoft.Modeling.Metadata.Models;

namespace DRsoft.Modeling.AppServices
{
    public class EngineAppService
    {
        private EngineMetadataManager metadataManager;
        public Guid metadataGuid = new Guid("AFA94CBC-A010-3A28-7B44-F409992825E4");

        public EngineAppService()
        {
            metadataManager = EngineMetadataManager.GetInstance();
        }

        public EngineConfig? Read()
        {
            return metadataManager.GetMetadataItemEx(metadataGuid)?.Metadata as EngineConfig;
        }

        /// <summary>
        /// 签出
        /// </summary>
        /// <returns></returns>
        public EngineConfig? CheckOut()
        {
            //1、签出文件
            metadataManager.CheckOut(metadataGuid);
            //2、返回实体
            return metadataManager.GetMetadataItemEx(metadataGuid)?.Metadata as EngineConfig;
        }

        /// <summary>
        /// 取消签出(撤销)
        /// </summary>
        public void CancleCheckOut()
        {
            //1、签出文件
            metadataManager.CancelCheckOut(metadataGuid);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="engine"></param>
        public void Save(EngineConfig engine)
        {
            if (engine == null) return;
            //1、更新实体
            metadataManager.Update(engine);
            //2、提交保存
            metadataManager.Submit(metadataManager.GetStatusItem(engine, StatusType.Modified), "");
        }
    }
}
