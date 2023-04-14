using System.Collections.Generic;

namespace DRsoft.Modeling.Metadata.VersionManagers
{
    /// <summary>
    /// 版本管理调用接口
    /// </summary>
    public interface IVersionManager
    {
        /// <summary>
        /// 检查是否开启工具
        /// </summary>
        void VerifyService();

        /// <summary>
        /// 签出文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="body"></param>
        void CheckOutFile(string filePath, string body = "");

        /// <summary>
        /// 重新签出文件(为了容错处理)
        /// </summary>
        /// <param name="filePath">文件路径</param>
        void ReCheckOutFile(string filePath);

        /// <summary>
        /// 撤销签出文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        void CancelCheckOut(string filePath);

        /// <summary>
        /// 提交(签入) 文件
        /// </summary>
        /// <param name="files">文件</param>
        /// <param name="comment">备注</param>

        void Submit(Dictionary<string, int> files, string comment);

        /// <summary>
        /// 通知
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="email"></param>
        void Notification(string batch, string email);

        /// <summary>
        /// 提交检查
        /// </summary>
        void SubmitVerification();

        /// <summary>
        /// 返回一个文件的状态
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>0 表示文件不存在于TFS中,1 表示文件是新增状态, 2表示文件是签出状态, 3表示正常(可能被别人签走)</returns>
        int GetFileState(string filePath);

        /// <summary>
        /// 返回本地文件是否是最新版本
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否是最新版本</returns>
        bool LocalIsLastVersion(string filePath);
    }
}
