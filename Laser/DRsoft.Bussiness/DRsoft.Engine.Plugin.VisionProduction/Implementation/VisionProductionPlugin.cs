using DRsoft.Engine.Core.Internal;
using DRsoft.Engine.Model.Const;
using DRsoft.Engine.Model.Enum;
using DRsoft.Engine.Model.Vision;
using DRsoft.Runtime.Core.Platform.Logging;
using Newtonsoft.Json;
using System;
using DRsoft.Engine.Plugin.VisionProduction.Configurations;
using DRsoft.Runtime.Core.Platform.Vision;

namespace DRsoft.Engine.Plugin.VisionProduction.Implementation
{
    /// <summary>
    /// 视觉生产相关通讯插件
    /// </summary>

    public class VisionProductionPlugin : AbstractProvider, IVisionHandler
    {
        private ConfigOption _config;
        public VisionProductionPlugin(ConfigOption Config) : base(Config)
        {
            _config = Config;
        }
        public override bool CommandAction(VisionProduction_Command Command, SendCommand para)
        {
            bool result = false;
            if (!IsConnected)
            {
                return result;
            }
            switch (Command)
            {
                case VisionProduction_Command.COMMAND_VISIONCMDSEND:
                    string send = JsonConvert.SerializeObject(Send, Formatting.Indented);
                    SendMessage(Command, send);
                    break;
                case VisionProduction_Command.COMMAND_VISIONREQUEST:
                    shootFlag = false;
                    padFlag = false;
                    silicaFlag = false;
                    cameraDataList.Clear();

                    string sendjson = Serialize.ToJson(para) + "#";
                    SendMessage(Command, sendjson);
                    result = true;
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }

        //public override void OnMessageRecvBack(string recvMessage)
        //{
        //    if (recvMessage != null && recvMessage != "")
        //    {
        //        try
        //        {
        //            ReceiveMsg = recvMessage;
        //            //触发事件
        //            if (_config.Key == "DRSoft.Plugin.Vision.ProductionA")
        //            {
        //                PubSubEventManager?.Publish(new EngineEventArgs { Handle = DRSoftEventDefine.EVENT_VISIONPRODUCTION_RECVMSG1 });
        //            }
        //            else
        //            {
        //                PubSubEventManager?.Publish(new EngineEventArgs { Handle = DRSoftEventDefine.EVENT_VISIONPRODUCTION_RECVMSG2 });
        //            }                    
        //        }
        //        catch (Exception)
        //        {
        //            Log.Error("VisionProductionPlugin OnMessageRecvBack Error");
        //        }
        //    }
        //}
       // public List<string> cameraDataList = new List<string>();
        public bool shootFlag = false;
        public bool padFlag = false;
        public bool silicaFlag = false;
        public override void OnMessageRecvBack(string recvMessage)
        {

            if (recvMessage != null && recvMessage != "")
            {
                try
                {
                    ReceiveMsg = recvMessage;
                    string[] msgList = ReceiveMsg.Split("#");

                    foreach (var item in msgList)
                    {
                        if (cameraDataList.Count > 3)
                        {
                            if (item.Contains("take_photo"))
                            {
                                cameraDataList[0] = item;
                            }
                            else if (item.Contains("pad_position"))
                            {
                                cameraDataList[1] = item;
                            }
                            else if (item.Contains("silica_gel_status"))
                            {
                                cameraDataList[2] = item;
                                break;
                            }
                        }
                        else
                        {
                            if (item.Contains("take_photo"))
                            {
                                cameraDataList.Clear();
                                cameraDataList.Add(item);

                                if (!shootFlag)
                                {
                                    string msg = cameraDataList[0];
                                    ShootDoneData = LaserPadPosition.FromJson(msg);
                                    shootFlag = true;
                                }
                            }
                            else if (item.Contains("pad_position"))
                            {
                                cameraDataList.Add(item);

                                if (!padFlag)
                                {
                                    string msg = cameraDataList[1];
                                    PadPosData = LaserPadPosition.FromJson(msg);
                                    padFlag = true;
                                }
                            }
                            else if (item.Contains("silica_gel_status"))
                            {
                                cameraDataList.Add(item);
                                if (!silicaFlag)
                                {
                                    string msg = cameraDataList[2];
                                    SilicaData = LaserPadPosition.FromJson(msg);
                                    silicaFlag = true;
                                }
                                break;
                            }
                        }
                    }

                    //if (cameraDataList.Count == 1)
                    //{
                    //    if (!shootFlag)
                    //    {
                    //        string msg = cameraDataList[0] + "}";
                    //        ShootDoneData = LaserPadPosition.FromJson(msg);
                    //        shootFlag = true;
                    //    }
                    //}
                    //else if (cameraDataList.Count == 2)
                    //{
                    //    if (!padFlag)
                    //    {
                    //        string msg = cameraDataList[1] + "}";
                    //        PadPosData = LaserPadPosition.FromJson(msg);
                    //        padFlag = true;
                    //    }
                    //}
                    //else if (cameraDataList.Count == 3)
                    //{
                    //    if (!padFlag)
                    //    {
                    //        string msg1 = cameraDataList[1] + "}";
                    //        PadPosData = LaserPadPosition.FromJson(msg1);
                    //        padFlag = true;
                    //    }
                    //    if (!silicaFlag)
                    //    {
                    //        string msg = cameraDataList[2] + "}";
                    //        SilicaData = LaserPadPosition.FromJson(msg);
                    //        silicaFlag = true;
                    //    }
                    //}

                    for (int i = 0; i < cameraDataList.Count; i++)
                    {
                        //触发事件
                        if (shootFlag && _config.Key == "DRSoft.Plugin.Vision.ProductionA" && (ShootDoneData != null && ShootDoneData.ResponseType == "take_photo"))
                        {
                            shootFlag = false;
                            PubSubEventManager?.Publish(new EngineEventArgs { Handle = DRSoftEventDefine.EVENT_VISIONPRODUCTION_SHOOTDONE1 });
                            Log.ErrorFormat("Receive CameraA ShootDoneData");
                        }
                        if (padFlag && _config.Key == "DRSoft.Plugin.Vision.ProductionA" && (PadPosData != null && PadPosData.ResponseType == "pad_position"))
                        {
                            padFlag = false;
                            PubSubEventManager?.Publish(new EngineEventArgs { Handle = DRSoftEventDefine.EVENT_VISIONPRODUCTION_PADPOS1 });
                            Log.ErrorFormat("Receive CameraA PadPosData");
                        }
                        if (silicaFlag && _config.Key == "DRSoft.Plugin.Vision.ProductionA" && (SilicaData != null && SilicaData.ResponseType == "silica_gel_status"))
                        {
                            silicaFlag = false;
                            PubSubEventManager?.Publish(new EngineEventArgs { Handle = DRSoftEventDefine.EVENT_VISIONPRODUCTION_SILICADATA1 });
                            Log.ErrorFormat("Receive CameraA SilicaData");
                        }

                        if (shootFlag && _config.Key == "DRSoft.Plugin.Vision.ProductionB" && (ShootDoneData != null && ShootDoneData.ResponseType == "take_photo"))
                        {
                            shootFlag = false;
                            PubSubEventManager?.Publish(new EngineEventArgs { Handle = DRSoftEventDefine.EVENT_VISIONPRODUCTION_SHOOTDONE2 });
                            Log.ErrorFormat("Receive CameraB ShootDoneData");
                        }
                        if (padFlag && _config.Key == "DRSoft.Plugin.Vision.ProductionB" && (PadPosData != null && PadPosData.ResponseType == "pad_position"))
                        {
                            padFlag = false;
                            PubSubEventManager?.Publish(new EngineEventArgs { Handle = DRSoftEventDefine.EVENT_VISIONPRODUCTION_PADPOS2 });
                            Log.ErrorFormat("Receive CameraB PadPosData");
                        }
                        if (silicaFlag && _config.Key == "DRSoft.Plugin.Vision.ProductionB" && (SilicaData != null && SilicaData.ResponseType == "silica_gel_status"))
                        {
                            silicaFlag = false;
                            PubSubEventManager?.Publish(new EngineEventArgs { Handle = DRSoftEventDefine.EVENT_VISIONPRODUCTION_SILICADATA2 });
                            Log.ErrorFormat("Receive CameraB SilicaData");
                        }
                    }

                }
                catch (Exception)
                {
                    Log.Error("VisionProductionPlugin OnMessageRecvBack Error");
                }
            }
        }


    }
}