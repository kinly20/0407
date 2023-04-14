using DRsoft.Modeling.Metadata.Status;
using System;
using DRsoft.Modeling.Metadata.Models.Config;
using Modle= DRsoft.Modeling.Metadata.Models;
using DRsoft.Modeling.Metadata.Managers.Config;

namespace DRsoft.Modeling.AppServices
{
      public class RecipeAppService
    {
        private RecipeMetadataManager metadataManager;
        private Guid metadataGuid = new Guid("C8930820-2B7A-9439-9F04-CFF721DE0822");

        public RecipeAppService()
        {
            metadataManager = RecipeMetadataManager.GetInstance();
        }
        public RecipeConfig? Read()
        {
            //返回实体
            return metadataManager.GetMetadataItemEx(metadataGuid)?.Metadata as RecipeConfig;
        }
        /// <summary>
        /// 签出
        /// </summary>
        /// <returns></returns>
        public RecipeConfig? CheckOut()
        {
            //1、签出文件
            metadataManager.CheckOut(metadataGuid);
            //2、返回实体
            return metadataManager.GetMetadataItemEx(metadataGuid)?.Metadata as RecipeConfig;
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
        public void Save(RecipeConfig engine)
        {
            if (engine == null) return;

            //1、更新实体
            metadataManager.Update(engine);
            //2、提交保存
            metadataManager.Submit(metadataManager.GetStatusItem(engine, StatusType.Modified), "");
        }
    }
}
