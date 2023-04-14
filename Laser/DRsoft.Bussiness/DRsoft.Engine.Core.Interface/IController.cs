using System;

namespace DRsoft.Engine.Core.Interface
{
    /// <summary>
    /// 控制器接口
    /// </summary>
    public interface IController : IDisposable
    {
        /// <summary>
        /// 上报信息
        /// </summary>
        /// <param name="report"></param>
        public void GetReport(string report);
        /// <summary>
        /// 命令初始化
        /// </summary>
        public void CommandInit();
        /// <summary>
        /// 生产命令
        /// </summary>
        public void CommandProduction();
        /// <summary>
        /// 终止命令
        /// </summary>
        /// <param name="reset"></param>
        public void CommandAbort(bool reset);
        /// <summary>
        /// 
        /// </summary>
        public void CommandErrAck();
        /// <summary>
        /// 调试命令
        /// </summary>
        public void CommandService();
        /// <summary>
        /// 停止命令
        /// </summary>
        public void CommandStop();
        /// <summary>
        /// 暂停命令
        /// </summary>
        public void CommandPause();
        /// <summary>
        /// 恢复命令
        /// </summary>
        public void CommandResume();
        /// <summary>
        /// 调试命令
        /// </summary>
        /// <param name="command"></param>
        public void CommandServiceOperation();
        /// <summary>
        /// 标定命令
        /// </summary>
        /// <param name="on"></param>
        public void CommandCalibration(bool on);
        /// <summary>
        /// 通过命令
        /// </summary>
        public void CommandPassThrough();
        /// <summary>
        /// 错误异常
        /// </summary>
        /// <param name="error"></param>
        public void PcErrorMode(bool error);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="needed"></param>
        public void MaintenanceNeeded(bool needed);

        /// <summary>
        /// 插入模拟的硅片
        /// </summary>
        public void InsertSimulatedWafer();
        /// <summary>
        /// 调试模式操作完毕
        /// </summary>
        /// <returns></returns>
        public bool IsServiceOperationDone();
        /// <summary>
        /// 版本号
        /// </summary>
        /// <returns></returns>
        public string GetControllerSW_Version();
        
        public void ReadControlLoop();
    }
}
