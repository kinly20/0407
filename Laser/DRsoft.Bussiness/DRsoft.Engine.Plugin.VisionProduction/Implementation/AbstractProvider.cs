using System;
using DRsoft.Engine.Core.Vision.VisionProduction;
using DRsoft.Engine.Model.Enum;
using DRsoft.Engine.Model.Vision;
using DRsoft.Engine.Plugin.VisionProduction.Configurations;
using DRsoft.Runtime.Core.Platform.Logging;
using Newtonsoft.Json;

namespace DRsoft.Engine.Plugin.VisionProduction.Implementation
{
    public abstract class AbstractProvider:AbstractVisionProduction
    {
        private ConfigOption config;
        public string send;
        public override string Scope => "VisionProduction";

        public AbstractProvider(ConfigOption Config)
        {
            config = Config;
            Initialize();
        }
        public override void OnMessageRecvBack(string recvMessage)
        {
            if (recvMessage != null && recvMessage != "")
            {
                try
                {
                    if (recvMessage.Contains("CameraOK"))
                    {

                    }
                }
                catch (Exception)
                {
                    Log.Error("VisionProductionPlugin OnMessageRecvBack Error");
                }
            }
        }

        public override void SendMsg(string Send)
        {
            send = Send;
        }

        public override bool CommandAction(VisionProduction_Command Command, SendCommand para)
        {
            if (!IsConnected) return false;
            bool result = false;
            switch (Command)
            {
                case VisionProduction_Command.COMMAND_VISIONCMDSEND:
                    string send = JsonConvert.SerializeObject(Send, Formatting.Indented);
                    SendMessage(Command, send);
                    break;
                default:
                    result = false;
                    break;
            }

            return result;
        }

        public override bool Initialize()
        {
            if (IsConnected) return true;
            return StartCommunication();
        }

        public override bool Reconnect()
        {
            if (IsConnected) return true;
            return StartCommunication();
        }

        public override bool StartCommunication()
        {
            return OpenConnection(config.IpAddress, config.Port);
        }

        public override void StopCommunication()
        {
            CloseConnection();
        }
    }
}
