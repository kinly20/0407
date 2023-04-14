using System;
using DRsoft.Modeling.Metadata.Managers.Config;
using DRsoft.Modeling.Metadata.Models.Config;
using DRsoft.Modeling.Metadata.Status;
using Modle = DRsoft.Modeling.Metadata.Models;

namespace DRsoft.Modeling.AppServices
{
    public class CameraAppService
    {
        private CameraManager metadataManager;
        public Guid metadataGuid =new Guid("A6F196A7-C0CF-CCC1-B297-070AA28B2AE4");

        public CameraAppService()
        {
            metadataManager = CameraManager.GetInstance();
        }
        public CamConfig? Read()
        {
            return metadataManager.GetMetadataItemEx(metadataGuid)?.Metadata as CamConfig;
        }
        /// <summary>
        /// 签出
        /// </summary>
        /// <returns></returns>
        public CamConfig? CheckOut()
        {
            //1、签出文件
            metadataManager.CheckOut(metadataGuid);
            //2、返回实体
            return metadataManager.GetMetadataItemEx(metadataGuid)?.Metadata as CamConfig;
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
        public void Save(CamConfig engine)
        {
            if (engine == null) return;

            //1、更新实体
            metadataManager.Update(engine);
            //2、提交保存
            metadataManager.Submit(metadataManager.GetStatusItem(engine, StatusType.Modified), "");
        }
    }
}
