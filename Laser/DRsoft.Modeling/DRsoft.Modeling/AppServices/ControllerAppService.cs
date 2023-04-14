using DRsoft.Modeling.Metadata.Managers;
using DRsoft.Modeling.Metadata.Status;
using System;
using DRsoft.Modeling.Metadata.Models.Config;
using Modle= DRsoft.Modeling.Metadata.Models;

namespace DRsoft.Modeling.AppServices
{
      public class ControllerAppService
      {
        private ControllerManager metadataManager;
        public Guid metadataGuid = new Guid("A0288F92-65D0-F6FE-D316-3AC433CAAAD9");

        public ControllerAppService()
        {
            metadataManager = ControllerManager.GetInstance();
        }
        public ControlConfig? Read()
        {
            //返回实体
            return metadataManager.GetMetadataItemEx(metadataGuid)?.Metadata as ControlConfig;
        }
        /// <summary>
        /// 签出
        /// </summary>
        /// <returns></returns>
        public ControlConfig? CheckOut()
        {
            //1、签出文件
            metadataManager.CheckOut(metadataGuid);
            //2、返回实体
            return metadataManager.GetMetadataItemEx(metadataGuid)?.Metadata as ControlConfig;
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
        public void Save(ControlConfig engine)
        {
            if (engine == null) return;

            //1、更新实体
            metadataManager.Update(engine);
            //2、提交保存
            metadataManager.Submit(metadataManager.GetStatusItem(engine, StatusType.Modified), "");
        }
    }
}
