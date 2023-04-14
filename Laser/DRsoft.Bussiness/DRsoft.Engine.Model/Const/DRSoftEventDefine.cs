namespace DRsoft.Engine.Model.Const
{
    /// <summary>
    /// 
    /// </summary>
    public static class DRSoftEventDefine
    {
        public const int EVENT_VISIONCALIBRATION_ERR = -3;
        public const int EVENT_VISIONPRODUCTION_ERR = -2;
        public const int EVENT_CAMERA_ERR = -1;
        public const int EVENT_ENGINE_TIMER = 0;
        public const int EVENT_QUIT = 1;
        public const int EVENT_VISIONPRODUCTION_SHOOTDONE1 = 2; //视觉1拍照完成
        public const int EVENT_VISIONPRODUCTION_PADPOS1 = 3; //视觉1收到焊接点数据
        public const int EVENT_VISIONPRODUCTION_SILICADATA1 = 4; //视觉1收到脏污数据
        public const int EVENT_VISIONPRODUCTION_SHOOTDONE2 = 5; //视觉2拍照完成
        public const int EVENT_VISIONPRODUCTION_PADPOS2 = 6; //视觉2收到焊接点数据
        public const int EVENT_VISIONPRODUCTION_SILICADATA2 = 7; //视觉2收到脏污数据
    }
}