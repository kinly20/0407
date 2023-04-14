namespace DRsoft.Engine.Model.Enum
{
    public enum SystemStep
    {
        None = 0,   
        SendAlignOffset = 1,
        ChooseStation,
        WaitStationACameraShootRequest,
        SendStationACameraShootDone,
        ReceiveStationACameraData,
        WaitStationALaserMarkRequest,
        StationALaserReadyToMark,
        SendStationALaserMarkDone,
        StationAUnitAllLineProcessDone,
        StationACameraDirtyData,
        StationAUnitProcessDone,
        WaitStationBCameraShootRequest,
        SendStationBCameraShootDone,
        ReceiveStationBCameraData,
        WaitStationBLaserMarkRequest,
        StationBLaserReadyToMark,
        SendStationBLaserMarkDone,
        StationBUnitAllLineProcessDone,
        StationBCameraDirtyData,
        StationBUnitProcessDone,
        NoFeedINMode,
        ProcessDone
    }
}
