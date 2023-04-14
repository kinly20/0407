using System;
using DRsoft.Engine.Core.Interface;
using DRsoft.Engine.Model.Controller;
using DRsoft.Engine.Model.Enum;
using DRsoft.Engine.Model.Vision;
using DRsoft.Runtime.Core.Platform.Exceptions;
using DRsoft.Engine.Model.Const;
using DRsoft.Runtime.Core.Platform.Logging;

namespace DRsoft.Engine.Core.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public abstract partial class AbstractEngine : IEngine
    {
        /// <summary>
        /// 定时器处理
        /// </summary>
        public virtual void HandleEngineOnTimer()
        {
            try
            {
                if (Controller.SysStatus.Running && !Controller.SysStatus.Pause)
                {
                    switch (CurrentSystemStep)
                    {
                        case SystemStep.SendAlignOffset:  //根据历史纠偏数据，得到新的纠偏数据
                            Config.GroupId = Guid.NewGuid().ToString();
                            if (Controller.HMIDataToPLC.StationA_AlignFlag || Controller.HMIDataToPLC.StationB_AlignFlag)
                            {
                                Controller.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_HMIData_TOPLC);
                                CurrentSystemStep = SystemStep.ChooseStation;

                                Log.ErrorFormat($"SystemStep:{CurrentSystemStep}");
                            }
                            break;
                        case SystemStep.ChooseStation:
                            if (Controller.PLCDataToHMI.GantryProcessStationgNum == StationAProcess)
                            {
                                Controller.HMIDataToPLC.StationA_AlignFlag = false;  //清掉StationA的纠偏标志
                                CurrentSystemStep = SystemStep.WaitStationACameraShootRequest;
                                Log.ErrorFormat($"SystemStep:{CurrentSystemStep}");
                            }
                            else if (Controller.PLCDataToHMI.GantryProcessStationgNum == StationBProcess)
                            {
                                Controller.HMIDataToPLC.StationB_AlignFlag = false;  //清掉StationB的纠偏标志
                                CurrentSystemStep = SystemStep.WaitStationBCameraShootRequest;
                                Log.ErrorFormat($"SystemStep:{CurrentSystemStep}");
                            }
                            break;
                        case SystemStep.WaitStationACameraShootRequest:
                            if (Controller.PLCDataToHMI.Gantry1_CameraRequest && Controller.PLCDataToHMI.Gantry2_CameraRequest)
                            {
                                //触发龙门1和2的相机拍照
                                TriggerCameraToShoot(1, Controller.PLCDataToHMI.Gantry1CameraLineNum);
                                TriggerCameraToShoot(2, Controller.PLCDataToHMI.Gantry2CameraLineNum);
                                CurrentSystemStep = SystemStep.SendStationACameraShootDone;
                                Log.ErrorFormat($"SystemStep:{CurrentSystemStep},第{Controller.PLCDataToHMI.Gantry1CameraLineNum}根线执行拍照动作");
                            }
                            else
                            {
                                Log.ErrorFormat($"SystemStep:{CurrentSystemStep},Detail:Gantry1_CameraRequest({Controller.PLCDataToHMI.Gantry1_CameraRequest}),Gantry2_CameraRequest({Controller.PLCDataToHMI.Gantry2_CameraRequest})");
                            }
                            break;
                        case SystemStep.SendStationACameraShootDone:   //等待拍照完成信号后，下位机运动到打标位
                            if (Controller.PLCDataToHMI.Gantry1CameraLineNum == FirstLineProcess && TakePhotoFinished1 && TakePhotoFinished2)
                            {
                                Controller.HMIDataToPLC.Gantry1_CameraShootDone = true;
                                Controller.HMIDataToPLC.Gantry2_CameraShootDone = true;
                                Controller.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_HMIData_TOPLC);
                                GetCurrentAlignMotorPos(StationAProcess);
                                CurrentSystemStep = SystemStep.ReceiveStationACameraData;
                                Log.ErrorFormat($"SystemStep:{CurrentSystemStep},第{Controller.PLCDataToHMI.Gantry1CameraLineNum}根线拍照完成");
                            }
                            else if (Controller.PLCDataToHMI.Gantry1CameraLineNum > FirstLineProcess && TakePhotoFinished1 && TakePhotoFinished2 && Controller.PLCDataToHMI.Gantry1ProcessDone && Controller.PLCDataToHMI.Gantry2ProcessDone)
                            {
                                Controller.HMIDataToPLC.Gantry1_CameraShootDone = true;
                                Controller.HMIDataToPLC.Gantry2_CameraShootDone = true;

                                Controller.HMIDataToPLC.Gantry1_ReadyToMark = false;
                                Controller.HMIDataToPLC.Gantry2_ReadyToMark = false;

                                Controller.HMIDataToPLC.Gantry1_MarkDone = true;
                                Controller.HMIDataToPLC.Gantry2_MarkDone = true;
                                Controller.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_HMIData_TOPLC);
                                GetCurrentAlignMotorPos(StationAProcess);
                                for (int i = 0; i < MarkingRecvPara.RecvFlagA.Length; i++)
                                {
                                    MarkingRecvPara.RecvFlagA[i] = false;
                                    MarkingRecvPara.RecvFlagB[i] = false;
                                }
                                CurrentSystemStep = SystemStep.ReceiveStationACameraData;
                                Log.ErrorFormat($"SystemStep:{CurrentSystemStep},第{Controller.PLCDataToHMI.Gantry1CameraLineNum}根线拍照完成,第{Controller.PLCDataToHMI.Gantry1ProcessLineNum}根线打标完成");
                            }
                            else
                            {
                                Log.ErrorFormat($"SystemStep:{CurrentSystemStep},ProcessLine:{Controller.PLCDataToHMI.Gantry1ProcessLineNum},detail info--TakePhotoFinished1:{TakePhotoFinished1},TakePhotoFinished2:{TakePhotoFinished2}");
                            }
                            break;
                        case SystemStep.ReceiveStationACameraData:
                            TakePhotoFinished1 = false;
                            TakePhotoFinished2 = false;
                            if (PadPositionFinished1 && PadPositionFinished2)
                            {
                                PadPositionFinished1 = false;
                                PadPositionFinished2 = false;
                                CurrentSystemStep = SystemStep.WaitStationALaserMarkRequest;
                                Log.ErrorFormat($"SystemStep:{CurrentSystemStep}");
                            }
                            else
                            {
                                Log.ErrorFormat($"SystemStep:{CurrentSystemStep},detail info--PadPositionFinished1:{PadPositionFinished1},PadPositionFinished2:{PadPositionFinished2}");
                            }
                            break;
                        case SystemStep.WaitStationALaserMarkRequest:
                            if (Controller.PLCDataToHMI.Gantry1_MarkRequest && Controller.PLCDataToHMI.Gantry2_MarkRequest)
                            {
                                ResetLongmenReadyFlag(MarkingRecvStatusFeedback);
                                ResetMark();
                                //触发龙门1和2准备激光加工第n跟线
                                TriggerLaserReadyToMark(1, Controller.PLCDataToHMI.Gantry1ProcessLineNum, _laserPadPosition1, 0, 0, 0);
                                TriggerLaserReadyToMark(2, Controller.PLCDataToHMI.Gantry2ProcessLineNum, _laserPadPosition2, 0, 0, 0);

                                CurrentSystemStep = SystemStep.StationALaserReadyToMark;

                                Log.ErrorFormat($"SystemStep:{CurrentSystemStep},第{Controller.PLCDataToHMI.Gantry1ProcessLineNum}根线准备打标图形");
                            }
                            break;
                        case SystemStep.StationALaserReadyToMark:
                            if (Controller.PLCDataToHMI.Gantry1ProcessLineNum == LastLineProcess && IsLongmen1ReadyToMark(MarkingRecvStatusFeedback) && IsLongmen2ReadyToMark(MarkingRecvStatusFeedback))
                            {
                                //通知下位机触发龙门1和2的激光加工第n跟线
                                Controller.HMIDataToPLC.Gantry1_MarkDone = false;
                                Controller.HMIDataToPLC.Gantry2_MarkDone = false;
                                Controller.HMIDataToPLC.Gantry1_ReadyToMark = true;
                                Controller.HMIDataToPLC.Gantry2_ReadyToMark = true;
                                Controller.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_HMIData_TOPLC);

                                CurrentSystemStep = SystemStep.StationAUnitAllLineProcessDone;
                                Log.ErrorFormat($"SystemStep:{CurrentSystemStep},第{Controller.PLCDataToHMI.Gantry1ProcessLineNum}根线执行打标动作");
                            }
                            else if (IsLongmen1ReadyToMark(MarkingRecvStatusFeedback) && IsLongmen2ReadyToMark(MarkingRecvStatusFeedback))
                            {
                                //通知下位机触发龙门1和2的激光加工第n跟线
                                Controller.HMIDataToPLC.Gantry1_MarkDone = false;
                                Controller.HMIDataToPLC.Gantry2_MarkDone = false;
                                Controller.HMIDataToPLC.Gantry1_ReadyToMark = true;
                                Controller.HMIDataToPLC.Gantry2_ReadyToMark = true;
                                Controller.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_HMIData_TOPLC);

                                CurrentSystemStep = SystemStep.WaitStationACameraShootRequest;
                                Log.ErrorFormat($"SystemStep:{CurrentSystemStep},第{Controller.PLCDataToHMI.Gantry1ProcessLineNum}根线执行打标动作");
                            }
                            break;
                        case SystemStep.SendStationALaserMarkDone: //等待激光打标完成信号，下位机运动到拍照位
                            Controller.HMIDataToPLC.Gantry1_MarkDone = true;
                            Controller.HMIDataToPLC.Gantry2_MarkDone = true;
                            Controller.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_HMIData_TOPLC);
                            CurrentSystemStep = SystemStep.StationAUnitAllLineProcessDone;
                            Log.ErrorFormat($"SystemStep:{CurrentSystemStep}");
                            break;
                        case SystemStep.StationAUnitAllLineProcessDone:
                            if (Controller.PLCDataToHMI.Gantry1ProcessDone && Controller.PLCDataToHMI.Gantry2ProcessDone)
                            {
                                for (int i = 0; i < MarkingRecvPara.RecvFlagA.Length; i++)
                                {
                                    MarkingRecvPara.RecvFlagA[i] = false;
                                    MarkingRecvPara.RecvFlagB[i] = false;
                                }

                                Controller.HMIDataToPLC.Gantry1_ReadyToMark = false;
                                Controller.HMIDataToPLC.Gantry2_ReadyToMark = false;

                                Controller.HMIDataToPLC.Gantry1_MarkDone = true;
                                Controller.HMIDataToPLC.Gantry2_MarkDone = true;
                                Controller.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_HMIData_TOPLC);

                                Controller.PLCDataToHMI.GantryProcessStationgNum = StationNullProcess;  //清掉当前工作的工位
                                CurrentSystemStep = SystemStep.StationACameraDirtyData;

                                Log.ErrorFormat($"SystemStep:{CurrentSystemStep},第{Controller.PLCDataToHMI.Gantry1ProcessLineNum}根线打标完成");
                            }
                            break;
                        case SystemStep.StationACameraDirtyData:      //等待相机的Dirty数据
                            if (GetAlignOffset(StationAProcess))
                            {
                                NoAlignResult1 = false;
                            }
                            else
                            {
                                NoAlignResult1 = true;
                            }
                            CurrentSystemStep = SystemStep.StationAUnitProcessDone;

                            Log.ErrorFormat($"SystemStep:{CurrentSystemStep}，NoAlignResult1:{NoAlignResult1}");
                            break;
                        case SystemStep.StationAUnitProcessDone:
                            if (NoAlignResult1)
                            {
                                Controller.HMIDataToPLC.NoFeedIn = true;
                                Controller.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_HMIData_TOPLC);
                                CurrentSystemStep = SystemStep.NoFeedINMode;
                            }
                            else
                                CurrentSystemStep = SystemStep.SendAlignOffset;

                            Log.ErrorFormat($"SystemStep:{CurrentSystemStep},NoAlignResult1:{NoAlignResult1}");
                            break;
                        case SystemStep.WaitStationBCameraShootRequest:
                            if (Controller.PLCDataToHMI.Gantry1_CameraRequest && Controller.PLCDataToHMI.Gantry2_CameraRequest)
                            {
                                //触发龙门1和2的相机拍照
                                TriggerCameraToShoot(1, Controller.PLCDataToHMI.Gantry1CameraLineNum);
                                TriggerCameraToShoot(2, Controller.PLCDataToHMI.Gantry2CameraLineNum);
                                CurrentSystemStep = SystemStep.SendStationBCameraShootDone;
                                Log.ErrorFormat($"SystemStep:{CurrentSystemStep},第{Controller.PLCDataToHMI.Gantry1CameraLineNum}根线执行拍照动作");
                            }
                            else
                            {
                                Log.ErrorFormat($"SystemStep:{CurrentSystemStep},Detail:Gantry1_CameraRequest({Controller.PLCDataToHMI.Gantry1_CameraRequest}),Gantry2_CameraRequest({Controller.PLCDataToHMI.Gantry2_CameraRequest})");
                            }
                            break;
                        case SystemStep.SendStationBCameraShootDone:
                            if (Controller.PLCDataToHMI.Gantry1CameraLineNum == FirstLineProcess && TakePhotoFinished1 && TakePhotoFinished2)
                            {
                                Controller.HMIDataToPLC.Gantry1_CameraShootDone = true;
                                Controller.HMIDataToPLC.Gantry2_CameraShootDone = true;
                                Controller.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_HMIData_TOPLC);
                                GetCurrentAlignMotorPos(StationBProcess);
                                CurrentSystemStep = SystemStep.ReceiveStationBCameraData;
                                Log.ErrorFormat($"SystemStep:{CurrentSystemStep},第{Controller.PLCDataToHMI.Gantry1CameraLineNum}根线拍照完成");
                            }
                            else if (Controller.PLCDataToHMI.Gantry1CameraLineNum > FirstLineProcess && TakePhotoFinished1 && TakePhotoFinished2 && Controller.PLCDataToHMI.Gantry1ProcessDone && Controller.PLCDataToHMI.Gantry2ProcessDone)
                            {
                                Controller.HMIDataToPLC.Gantry1_CameraShootDone = true;
                                Controller.HMIDataToPLC.Gantry2_CameraShootDone = true;

                                Controller.HMIDataToPLC.Gantry1_ReadyToMark = false;
                                Controller.HMIDataToPLC.Gantry2_ReadyToMark = false;

                                Controller.HMIDataToPLC.Gantry1_MarkDone = true;
                                Controller.HMIDataToPLC.Gantry2_MarkDone = true;
                                Controller.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_HMIData_TOPLC);
                                GetCurrentAlignMotorPos(StationBProcess);
                                for (int i = 0; i < MarkingRecvPara.RecvFlagB.Length; i++)
                                {
                                    MarkingRecvPara.RecvFlagA[i] = false;
                                    MarkingRecvPara.RecvFlagB[i] = false;
                                }
                                CurrentSystemStep = SystemStep.ReceiveStationBCameraData;
                                Log.ErrorFormat($"SystemStep:{CurrentSystemStep},第{Controller.PLCDataToHMI.Gantry1CameraLineNum}根线拍照完成,第{Controller.PLCDataToHMI.Gantry1ProcessLineNum}根线打标完成");
                            }
                            else
                            {
                                Log.ErrorFormat($"SystemStep:{CurrentSystemStep},ProcessLine:{Controller.PLCDataToHMI.Gantry2ProcessLineNum},detail info--TakePhotoFinished1:{TakePhotoFinished1},TakePhotoFinished2:{TakePhotoFinished2}");
                            }
                            break;
                        case SystemStep.ReceiveStationBCameraData:
                            TakePhotoFinished1 = false;
                            TakePhotoFinished2 = false;
                            if (PadPositionFinished1 && PadPositionFinished2)
                            {
                                PadPositionFinished1 = false;
                                PadPositionFinished2 = false;
                                CurrentSystemStep = SystemStep.WaitStationBLaserMarkRequest;
                                Log.ErrorFormat($"SystemStep:{CurrentSystemStep}");
                            }
                            else
                            {
                                Log.ErrorFormat($"SystemStep:{CurrentSystemStep},detail info--PadPositionFinished1:{PadPositionFinished1},PadPositionFinished2:{PadPositionFinished2}");
                            }
                            break;
                        case SystemStep.WaitStationBLaserMarkRequest:
                            if (Controller.PLCDataToHMI.Gantry1_MarkRequest && Controller.PLCDataToHMI.Gantry2_MarkRequest)
                            {
                                ResetLongmenReadyFlag(MarkingRecvStatusFeedback);
                                ResetMark();
                                //触发龙门1和2的激光加工第n跟线
                                TriggerLaserReadyToMark(1, Controller.PLCDataToHMI.Gantry1ProcessLineNum, _laserPadPosition1, 0, 0, 0);
                                TriggerLaserReadyToMark(2, Controller.PLCDataToHMI.Gantry2ProcessLineNum, _laserPadPosition2, 0, 0, 0);

                                CurrentSystemStep = SystemStep.StationBLaserReadyToMark;
                                Log.ErrorFormat($"SystemStep:{CurrentSystemStep},第{Controller.PLCDataToHMI.Gantry1ProcessLineNum}根线准备打标图形");
                            }
                            break;
                        case SystemStep.StationBLaserReadyToMark:
                            if (Controller.PLCDataToHMI.Gantry1ProcessLineNum == LastLineProcess && IsLongmen1ReadyToMark(MarkingRecvStatusFeedback) && IsLongmen2ReadyToMark(MarkingRecvStatusFeedback))
                            {
                                //通知下位机触发龙门1和2的激光加工第n跟线
                                Controller.HMIDataToPLC.Gantry1_MarkDone = false;
                                Controller.HMIDataToPLC.Gantry2_MarkDone = false;
                                Controller.HMIDataToPLC.Gantry1_ReadyToMark = true;
                                Controller.HMIDataToPLC.Gantry2_ReadyToMark = true;
                                Controller.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_HMIData_TOPLC);

                                CurrentSystemStep = SystemStep.StationBUnitAllLineProcessDone;
                                Log.ErrorFormat($"SystemStep:{CurrentSystemStep},第{Controller.PLCDataToHMI.Gantry1ProcessLineNum}根线执行打标动作");
                            }
                            else if (IsLongmen1ReadyToMark(MarkingRecvStatusFeedback) && IsLongmen2ReadyToMark(MarkingRecvStatusFeedback))
                            {
                                //通知下位机触发龙门1和2的激光加工第n跟线
                                Controller.HMIDataToPLC.Gantry1_MarkDone = false;
                                Controller.HMIDataToPLC.Gantry2_MarkDone = false;
                                Controller.HMIDataToPLC.Gantry1_ReadyToMark = true;
                                Controller.HMIDataToPLC.Gantry2_ReadyToMark = true;
                                Controller.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_HMIData_TOPLC);

                                CurrentSystemStep = SystemStep.WaitStationBCameraShootRequest;
                                Log.ErrorFormat($"SystemStep:{CurrentSystemStep},第{Controller.PLCDataToHMI.Gantry1ProcessLineNum}根线执行打标动作");
                            }
                            break;
                        case SystemStep.SendStationBLaserMarkDone:
                            Controller.HMIDataToPLC.Gantry1_MarkDone = true;
                            Controller.HMIDataToPLC.Gantry2_MarkDone = true;
                            Controller.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_HMIData_TOPLC);
                            CurrentSystemStep = SystemStep.StationBUnitAllLineProcessDone;
                            Log.ErrorFormat($"SystemStep:{CurrentSystemStep}");
                            break;
                        case SystemStep.StationBUnitAllLineProcessDone:
                            if (Controller.PLCDataToHMI.Gantry1ProcessDone && Controller.PLCDataToHMI.Gantry2ProcessDone)
                            {
                                for (int i = 0; i < MarkingRecvPara.RecvFlagB.Length; i++)
                                {
                                    MarkingRecvPara.RecvFlagA[i] = false;
                                    MarkingRecvPara.RecvFlagB[i] = false;
                                }

                                Controller.HMIDataToPLC.Gantry1_ReadyToMark = false;
                                Controller.HMIDataToPLC.Gantry2_ReadyToMark = false;

                                Controller.HMIDataToPLC.Gantry1_MarkDone = true;
                                Controller.HMIDataToPLC.Gantry2_MarkDone = true;
                                Controller.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_HMIData_TOPLC);

                                Controller.PLCDataToHMI.GantryProcessStationgNum = StationNullProcess;  //清掉当前工作的工位
                                CurrentSystemStep = SystemStep.StationBCameraDirtyData;

                                Log.ErrorFormat($"SystemStep:{CurrentSystemStep},第{Controller.PLCDataToHMI.Gantry1ProcessLineNum}根线打标完成");
                            }
                            break;
                        case SystemStep.StationBCameraDirtyData:      //等待相机的Dirty数据
                            if (GetAlignOffset(StationBProcess))
                            {
                                NoAlignResult2 = false;
                            }
                            else
                            {
                                NoAlignResult2 = true;
                            }
                            CurrentSystemStep = SystemStep.StationBUnitProcessDone;
                            Log.ErrorFormat($"SystemStep:{CurrentSystemStep}, NoAlignResult2:{NoAlignResult2}");
                            break;
                        case SystemStep.StationBUnitProcessDone:
                            if (NoAlignResult2)
                            {
                                Controller.HMIDataToPLC.NoFeedIn = true;
                                Controller.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_HMIData_TOPLC);
                                CurrentSystemStep = SystemStep.NoFeedINMode;
                            }
                            else
                                CurrentSystemStep = SystemStep.SendAlignOffset;
                            Log.ErrorFormat($"SystemStep:{CurrentSystemStep}, NoAlignResult2:{NoAlignResult2}");
                            break;
                        case SystemStep.NoFeedINMode:    //判断设备是否有硅片
                            if (!Controller.PLCDataToHMI.SystemHaveWafer)    //没有硅片，下发stop指令
                            {
                                Controller.CommandStop();
                                CleanProcess();
                            }
                            else
                            {
                                CurrentSystemStep = SystemStep.SendAlignOffset;
                            }
                            Log.ErrorFormat($"SystemStep:{CurrentSystemStep}, SystemHaveWafer:{Controller.PLCDataToHMI.SystemHaveWafer}");
                            break;
                        case SystemStep.ProcessDone:
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                TriggerException(new ErrorInfo { Message = "HandleEngineOnTimer 发生异常" }, ex);
            }
        }

        /// <summary>
        /// 判断硅胶膜是否清洗过
        /// --清洗过，弹框更换
        /// --没有清洗，下发清洗指令
        /// </summary>
        public void CleanProcess()
        {
            string message = "";
            if (Config.IsSilicaWashed)
            {
                message = "请更换硅胶膜。";
            }
            else
            {
                message = "请清洗硅胶膜。";
            }
            var sendMessage = new SendMessageToView()
            {
                Message = message
            };
            sendMessage.Reset();
            sendMessage.Ower = EventBroadcastNodeDefine.PluginWindowIdentity;
            sendMessage.AddSubscriber(EventBroadcastNodeDefine.EngineIdentity);
            sendMessage.AddSubscriber(EventBroadcastNodeDefine.WindowIdentity);
            SendMessageToViewEventManager.Publish(sendMessage);
        }

        public void GetCurrentAlignMotorPos(short stationNum)
        {
            Controller.ReadFromControllerByIndex(ControllerOutputIndex.OUT_INDEX_PLCDATA_TOHMI);
            if (stationNum == StationAProcess)
            {
                OldAlignPos1 = Controller.PLCDataToHMI.StationA_AlignCamPos;
            }
            else if (stationNum == StationBProcess)
            {
                OldAlignPos2 = Controller.PLCDataToHMI.StationB_AlignCamPos;
            }
        }

        /// <summary>
        /// 获取偏移位置
        /// </summary>
        /// <param name="stationNum"></param>
        /// <returns></returns>
        public bool GetAlignOffset(short stationNum)
        {
            bool success = false;
            string silicaId = Config.SilicaId;
            string machineId = stationNum.ToString();
            float offsetPara = 2; //mm

            string groupId = Config.GroupId;

            if (Simulate)
            {
                if (!SilicaIsEnable(groupId))
                {
                    if (stationNum == StationAProcess)
                    {
                        Controller.HMIDataToPLC.StationA_AlignFlag = FindCleanPosition(silicaId, machineId, offsetPara, ref Controller.HMIDataToPLC.StationA_AlignOffset);
                        success = Controller.HMIDataToPLC.StationA_AlignFlag;
                    }
                    else if (stationNum == StationBProcess)
                    {
                        Controller.HMIDataToPLC.StationB_AlignFlag = FindCleanPosition(silicaId, machineId, offsetPara, ref Controller.HMIDataToPLC.StationB_AlignOffset);
                        success = Controller.HMIDataToPLC.StationB_AlignFlag;
                    }
                }
                else
                {
                    if (stationNum == StationAProcess)
                    {
                        Controller.HMIDataToPLC.StationA_AlignFlag = true;
                        Controller.HMIDataToPLC.StationA_AlignOffset = OldAlignPos1;
                    }
                    else if (stationNum == StationBProcess)
                    {
                        Controller.HMIDataToPLC.StationB_AlignFlag = true;
                        Controller.HMIDataToPLC.StationB_AlignOffset = OldAlignPos2;
                    }
                    success = true;
                }
            }
            else
            {
                Controller.HMIDataToPLC.StationA_AlignFlag = true;
                Controller.HMIDataToPLC.StationA_AlignOffset = 1;
                Controller.HMIDataToPLC.StationB_AlignFlag = true;
                Controller.HMIDataToPLC.StationB_AlignOffset = 2;
                success = true;
            }
            return success;
        }

        /// <summary>
        /// 触发几号龙门的相机组拍照
        /// </summary>
        /// <param name="longMenNum"></param>
        /// <param name="cameraLineNum"></param>
        public void TriggerCameraToShoot(int longMenNum, short cameraLineNum)
        {
            SendCommand sendCameraCommand = new SendCommand();
            //sendCameraCommand.GroupId = longMenNum.ToString();
            sendCameraCommand.GroupId = Config.GroupId;
            sendCameraCommand.WorkStationId = cameraLineNum;
            sendCameraCommand.Hash = Guid.NewGuid().ToString();
            sendCameraCommand.Command = "pad_position";

            if (longMenNum == 1)
            {
                Longmen1CameraShootTimer.Start();        //开启定时器监控是否收到拍照完成
            }
            else if (longMenNum == 2)
            {
                Longmen2CameraShootTimer.Start();        //开启定时器监控是否收到拍照完成
            }

            VisionProduction[longMenNum - 1].CommandAction(VisionProduction_Command.COMMAND_VISIONREQUEST, sendCameraCommand);
        }


        private int index = 0;

        /// <summary>
        /// 触发几号龙门的激光组加工第几根
        /// </summary>
        /// <param name="longMenNum"></param>
        /// <param name="processLineNum"></param>
        /// <param name="ofsX"></param>
        /// <param name="ofsY"></param>
        /// <param name="ofsA"></param>
        /// ofsx,y,t 振镜x,y,t修正
        public void TriggerLaserReadyToMark(int longMenNum, short processLineNum, LaserPadPosition laserPadPosition, double ofsX, double ofsY, double ofsA)
        {
            if (Simulate)
            {
                string msg = "{\r\n  \"groupId\": 1,\r\n  \"workStationId\": 0,\r\n  \"hash\": null,\r\n  \"command\": null,\r\n  \"status\": null,\r\n  \"status_code\": 0,\r\n  \"message\": null,\r\n  \"solder_tapes_group_list\": [{\r\n   \"wafer_id\":1,\r\n   \"status\":\"ok\",\r\n   \"status_code\":123,\r\n   \"type_name\":\"piece\",\r\n   \"type\":1,\r\n   \"message\":\"ok\",\r\n   \"solder_tapes\":[{\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,\r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n  ],\r\n   \"flow_together_tapes\":null,\r\n   \"pad_over_si_dirtys\":null,\r\n   \"snap_cost\":0.0,\r\n   \"calc_cost\":0.0\r\n  },\r\n{\r\n   \"wafer_id\":1,\r\n   \"status\":\"ok\",\r\n   \"status_code\":123,\r\n   \"type_name\":\"piece\",\r\n   \"type\":1,\r\n   \"message\":\"ok\",\r\n   \"solder_tapes\":[{\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,\r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n  ],\r\n   \"flow_together_tapes\":null,\r\n   \"pad_over_si_dirtys\":null,\r\n   \"snap_cost\":0.0,\r\n   \"calc_cost\":0.0\r\n  },\r\n{\r\n   \"wafer_id\":1,\r\n   \"status\":\"ok\",\r\n   \"status_code\":123,\r\n   \"type_name\":\"piece\",\r\n   \"type\":1,\r\n   \"message\":\"ok\",\r\n   \"solder_tapes\":[{\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,\r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n  ],\r\n   \"flow_together_tapes\":null,\r\n   \"pad_over_si_dirtys\":null,\r\n   \"snap_cost\":0.0,\r\n   \"calc_cost\":0.0\r\n  },\r\n{\r\n   \"wafer_id\":1,\r\n   \"status\":\"ok\",\r\n   \"status_code\":123,\r\n   \"type_name\":\"piece\",\r\n   \"type\":1,\r\n   \"message\":\"ok\",\r\n   \"solder_tapes\":[{\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,\r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n  ],\r\n   \"flow_together_tapes\":null,\r\n   \"pad_over_si_dirtys\":null,\r\n   \"snap_cost\":0.0,\r\n   \"calc_cost\":0.0\r\n  },\r\n{\r\n   \"wafer_id\":1,\r\n   \"status\":\"ok\",\r\n   \"status_code\":123,\r\n   \"type_name\":\"piece\",\r\n   \"type\":1,\r\n   \"message\":\"ok\",\r\n   \"solder_tapes\":[{\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,\r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n  ],\r\n   \"flow_together_tapes\":null,\r\n   \"pad_over_si_dirtys\":null,\r\n   \"snap_cost\":0.0,\r\n   \"calc_cost\":0.0\r\n  },\r\n{\r\n   \"wafer_id\":1,\r\n   \"status\":\"ok\",\r\n   \"status_code\":123,\r\n   \"type_name\":\"piece\",\r\n   \"type\":1,\r\n   \"message\":\"ok\",\r\n   \"solder_tapes\":[{\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,\r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n  ],\r\n   \"flow_together_tapes\":null,\r\n   \"pad_over_si_dirtys\":null,\r\n   \"snap_cost\":0.0,\r\n   \"calc_cost\":0.0\r\n  }],\r\n  \"response_type\": \"take_photo\",\r\n  \"time_cost\": 0.0,\r\n  \"snap_cost\": 0.0,\r\n  \"find_pad_cost\": 0.0,\r\n  \"find_si_dirty_spots_cost\": 0.0\r\n}";
                _laserPadPosition1 = LaserPadPosition.FromJson(msg);
                _laserPadPosition2 = LaserPadPosition.FromJson(msg);

                if (longMenNum == 1)
                {
                    laserPadPosition = _laserPadPosition1;
                }
                else if (longMenNum == 2)
                {
                    laserPadPosition = _laserPadPosition2;
                }
            }
            var markingpara = new MarkingSendPara()
            {
                LongMenNum = longMenNum,
                ProcessLineNum = processLineNum,
                LaserPadPosition = laserPadPosition,
                XPos = ofsX,
                YPos = ofsY,
                APos = ofsA,
                Marking = true,
                ClearFlag = false,
            };
            markingpara.Reset();
            markingpara.Ower = EventBroadcastNodeDefine.PluginWindowIdentity;
            markingpara.AddSubscriber(EventBroadcastNodeDefine.EngineIdentity);
            markingpara.AddSubscriber(EventBroadcastNodeDefine.WindowIdentity);
            MarkingSendParaEventManager.Publish(markingpara);
        }

        /// <summary>
        /// 复位打标卡
        /// </summary>
        public void ResetMark()
        {
            var markingpara = new MarkingSendPara()
            {
                LongMenNum = 0,
                ProcessLineNum = 0,
                Marking = false,
                ClearFlag = false
            };
            markingpara.Reset();
            markingpara.Ower = EventBroadcastNodeDefine.PluginWindowIdentity;
            markingpara.AddSubscriber(EventBroadcastNodeDefine.EngineIdentity);
            markingpara.AddSubscriber(EventBroadcastNodeDefine.WindowIdentity);
            MarkingSendParaEventManager.Publish(markingpara);
        }

        /// <summary>
        /// 复位打标卡
        /// </summary>
        public void ResetMarkEndFlag()
        {
            var markingpara = new MarkingSendPara()
            {
                LongMenNum = 0,
                ProcessLineNum = 0,
                Marking = false,
                ClearFlag = true
            };
            markingpara.Reset();
            markingpara.Ower = EventBroadcastNodeDefine.PluginWindowIdentity;
            markingpara.AddSubscriber(EventBroadcastNodeDefine.EngineIdentity);
            markingpara.AddSubscriber(EventBroadcastNodeDefine.WindowIdentity);
            MarkingSendParaEventManager.Publish(markingpara);
        }

        public void EnableProduction()
        {
            Controller.CmdCommand.Start = true;
            Controller.SendCommandToController(ref Controller.CmdCommand.Start);
        }

        private void ResetLongmenReadyFlag(MarkingRecvStatusFeedback markingRecvStatusFeedback)
        {
            for (int i = 0; i < markingRecvStatusFeedback.MarkingStatusAFeedback.Length; i++)
            {
                markingRecvStatusFeedback.MarkingStatusAFeedback[i] = 10000;
                markingRecvStatusFeedback.MarkingStatusBFeedback[i] = 10000;
            }
        }

        private bool IsLongmen1ReadyToMark(MarkingRecvStatusFeedback markingRecvStatusFeedback)
        {
            foreach (var item in markingRecvStatusFeedback.MarkingStatusAFeedback)
            {
                //if (Simulate)
                //    return true;
                if (item != 0)
                    return false;
            }
            return true;
        }

        private bool IsLongmen2ReadyToMark(MarkingRecvStatusFeedback markingRecvStatusFeedback)
        {
            foreach (var item in markingRecvStatusFeedback.MarkingStatusBFeedback)
            {
                //if (Simulate)
                //    return true;
                if (item != 0)
                    return false;
            }
            return true;
        }

        private bool IsAllStationAMarkingEndComplete(MarkingRecvPara recvpara)
        {
            foreach (var item in recvpara.RecvFlagA)
            {
                //if(Simulate)
                //    return true;

                if (!item) 
                    return false;
            }
            return true;
        }

        private bool IsAllStationBMarkingEndComplete(MarkingRecvPara recvpara)
        {
            foreach (var item in recvpara.RecvFlagB)
            {
                //if (Simulate)
                //    return true;

                if (!item) return false;
            }
            return true;
        }
    }
}