using DRsoft.Engine.Core.Control.AbstractController;
using DRsoft.Engine.Model.Const;
using DRsoft.Engine.Model.Controller;
using DRsoft.Engine.Model.Error;
using DRsoft.Runtime.Core.Platform.Exceptions;
using DRsoft.Runtime.Core.Platform.Plc;

namespace DRsoft.Engine.Plugin.Control.Beckhoff.Implementation
{
    public partial class BeckhoffController : AbstractController
    {

        /// <summary>
        /// 处理PLC变量值变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void PlcHandler_OnNotificationed(object? sender, PlcNotifyArg e)
        {
            if (e.VarName == DRSoftInteractiveDefine.GMGR_Output_FeedBack_Output)
            {
                IoOutput = (StOutput)e.Value;
            }
            else if (e.VarName == DRSoftInteractiveDefine.GMGR_Output_FeedBack_Alarm_List)
            {
                Alarms = (AlarmClass)e.Value;
            }
            else if (e.VarName == DRSoftInteractiveDefine.GMGR_Output_FeedBack_System_Status)
            {
                SysStatus = (StStatus)e.Value;
            }
            else if (e.VarName == DRSoftInteractiveDefine.GMGR_Output_FeedBack_IO_Input)
            {
                IoInput = (StInput)e.Value;
            }
            else if (e.VarName == DRSoftInteractiveDefine.GMGR_Output_FeedBack_PLCDATA_TOHMI)
            {
                PLCDataToHMI = (PlcToHmi)e.Value;
            }
        }

        public override void PlcHander_OnConnected(object? sender, PlcEventArg e)
        {
            if (e.VarName == "BF")
            {
                //此处处理控制器连接后，触发参数下发事件
            }
        }

        /// <summary>
        /// 处理BeckHoffPLC-Ads通信过程中的异常
        ///         ConnectError =1,
        ///         CreateHandleError,
        ///         CreateNotifyError,
        ///         ReadError,
        ///         WriteError
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void PlcHander_OnErrored(object? sender, PlcException e)
        {
            if (e.ErrorInfo.Type == ErrorType.ConnectError)
            {
                ControllerConnectedFlag = false;
            }
        }
    }
}