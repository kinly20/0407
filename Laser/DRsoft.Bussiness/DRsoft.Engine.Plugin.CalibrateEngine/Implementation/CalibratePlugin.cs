using System;
using DRsoft.Engine.Core.Vision.VisionCalibration;
using DRsoft.Engine.Model.Enum;
using DRsoft.Runtime.Core.Platform.Logging;
using Newtonsoft.Json;

namespace DRsoft.Engine.Plugin.Calibrate.Implementation
{
    /// <summary>
    /// 标定插件
    /// </summary>
    public class CalibratePlugin : AbstractVisionCalibration
    {
        public CalibratePlugin(VisionCalibrationConfig visionCalibrationConfig) : base(visionCalibrationConfig)
        {
        }

        public override string Scope => "VisionCalibration";

        #region 发送数据

        public override void SendMsg(string Send)
        {
            SendMessage = Send;
        }


        public override bool CommandAction(VisionCalib_Command Command)
        {
            if (!IsConnected) return false;
            bool result = false;
            switch (Command)
            {
                case VisionCalib_Command.CALIB_COMMAND_STRING:
                    string send = JsonConvert.SerializeObject(SendMessage, Formatting.Indented);
                    SendMessage(Command, send);
                    break;
                default:
                    break;
            }

            return result;
        }

        #endregion

        public override void OnMessageRecvBack(string recvMessage)
        {
            if (recvMessage != null && recvMessage != "")
            {
                try
                {
                    if (recvMessage.Contains("DataCorr"))
                    {

                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex.ToString());
                }
            }
        }
    }
}