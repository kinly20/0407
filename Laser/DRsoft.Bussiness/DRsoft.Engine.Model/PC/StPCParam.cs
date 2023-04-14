namespace DRsoft.Engine.Model.PC
{
    public class StPCParam
    {
        /// <summary>
        /// 产品类型
        /// </summary>
        public int ProductionType { get; set; }

        /// <summary>
        /// 打标文件路径 例如D:\Marking
        /// </summary>
        public string? MarkingPath { get; set; }

        /// <summary>
        /// 打标文件名前缀 例如Marking*(1,2,3...12)
        /// </summary>
        public string? MarkingNamePrefix { get; set; }

        /// <summary>
        /// 权限自动退出时间 超过指定时间权限自动退出
        /// </summary>
        public int LogOutTime { get; set; }

        /// <summary>
        /// 1#测功率X
        /// </summary>
        public double PowerMeterMeasurePos1X { get; set; }

        /// <summary>
        /// 1#测功率Y
        /// </summary>
        public double PowerMeterMeasurePos1Y { get; set; }

        /// <summary>
        /// 2#测功率X
        /// </summary>
        public double PowerMeterMeasurePos2X { get; set; }

        /// <summary>
        /// 2#测功率Y
        /// </summary>
        public double PowerMeterMeasurePos2Y { get; set; }

        /// <summary>
        /// 3#测功率X
        /// </summary>
        public double PowerMeterMeasurePos3X { get; set; }

        /// <summary>
        /// 3#测功率Y
        /// </summary>
        public double PowerMeterMeasurePos3Y { get; set; }

        /// <summary>
        /// 4#测功率X
        /// </summary>
        public double PowerMeterMeasurePos4X { get; set; }

        /// <summary>
        /// 4#测功率Y
        /// </summary>
        public double PowerMeterMeasurePos4Y { get; set; }

        /// <summary>
        /// 5#测功率X
        /// </summary>
        public double PowerMeterMeasurePos5X { get; set; }

        /// <summary>
        /// 5#测功率Y
        /// </summary>
        public double PowerMeterMeasurePos5Y { get; set; }

        /// <summary>
        /// 6#测功率X
        /// </summary>
        public double PowerMeterMeasurePos6X { get; set; }

        /// <summary>
        /// 6#测功率Y
        /// </summary>
        public double PowerMeterMeasurePos6Y { get; set; }

        /// <summary>
        /// 7#测功率X
        /// </summary>
        public double PowerMeterMeasurePos7X { get; set; }

        /// <summary>
        /// 7#测功率Y
        /// </summary>
        public double PowerMeterMeasurePos7Y { get; set; }

        /// <summary>
        /// 8#测功率X
        /// </summary>
        public double PowerMeterMeasurePos8X { get; set; }

        /// <summary>
        /// 8#测功率Y
        /// </summary>
        public double PowerMeterMeasurePos8Y { get; set; }

        /// <summary>
        /// 9#测功率X
        /// </summary>
        public double PowerMeterMeasurePos9X { get; set; }

        /// <summary>
        /// 9#测功率Y
        /// </summary>
        public double PowerMeterMeasurePos9Y { get; set; }

        /// <summary>
        /// 10#测功率X
        /// </summary>
        public double PowerMeterMeasurePos10X { get; set; }

        /// <summary>
        /// 10#测功率Y
        /// </summary>
        public double PowerMeterMeasurePos10Y { get; set; }

        /// <summary>
        /// 11#测功率X
        /// </summary>
        public double PowerMeterMeasurePos11X { get; set; }

        /// <summary>
        /// 11#测功率Y
        /// </summary>
        public double PowerMeterMeasurePos11Y { get; set; }

        /// <summary>
        /// 12#测功率X
        /// </summary>
        public double PowerMeterMeasurePos12X { get; set; }

        /// <summary>
        /// 12#测功率Y
        /// </summary>
        public double PowerMeterMeasurePos12Y { get; set; }

        /// <summary>
        /// 功率测量间隔
        /// </summary>
        public double PowerMeterInterval { get; set; }

        /// <summary>
        /// 功率测量上限
        /// </summary>
        public double PowerMeterMeasureHl { get; set; }

        /// <summary>
        /// 功率测量下限
        /// </summary>
        public double PowerMeterMeasureLl { get; set; }

        /// <summary>
        /// 功率系数
        /// </summary>
        public double PowerMeterRatio { get; set; }

        /// <summary>
        /// 功率百分比
        /// </summary>
        public double PowerMeterPercent { get; set; }

        /// <summary>
        /// 1#激光器功率
        /// </summary>
        public double Laser1Power { get; set; }

        /// <summary>
        /// 1#激光器频率
        /// </summary>
        public double Laser1Freq { get; set; }

        /// <summary>
        /// 2#激光器功率
        /// </summary>
        public double Laser2Power { get; set; }

        /// <summary>
        /// 2#激光器频率
        /// </summary>
        public double Laser2Freq { get; set; }

        /// <summary>
        /// 3#激光器功率
        /// </summary>
        public double Laser3Power { get; set; }

        /// <summary>
        /// 3#激光器频率
        /// </summary>
        public double Laser3Freq { get; set; }
        
        /// <summary>
        /// 4#激光器功率
        /// </summary>
        public double Laser4Power { get; set; }

        /// <summary>
        /// 4#激光器频率
        /// </summary>
        public double Laser4Freq { get; set; }
        
        /// <summary>
        /// 5#激光器功率
        /// </summary>
        public double Laser5Power { get; set; }

        /// <summary>
        /// 5#激光器频率
        /// </summary>
        public double Laser5Freq { get; set; }

        /// <summary>
        /// 6#激光器功率
        /// </summary>
        public double Laser6Power { get; set; }

        /// <summary>
        /// 6#激光器频率
        /// </summary>
        public double Laser6Freq { get; set; }

        /// <summary>
        /// 7#激光器功率
        /// </summary>
        public double Laser7Power { get; set; }

        /// <summary>
        /// 7#激光器频率
        /// </summary>
        public double Laser7Freq { get; set; }

        /// <summary>
        /// 8#激光器功率
        /// </summary>
        public double Laser8Power { get; set; }

        /// <summary>
        /// 8#激光器频率
        /// </summary>
        public double Laser8Freq { get; set; }

        /// <summary>
        /// 9#激光器功率
        /// </summary>
        public double Laser9Power { get; set; }

        /// <summary>
        /// 9#激光器频率
        /// </summary>
        public double Laser9Freq { get; set; }

        /// <summary>
        /// 10#激光器功率
        /// </summary>
        public double Laser10Power { get; set; }

        /// <summary>
        /// 10#激光器频率
        /// </summary>
        public double Laser10Freq { get; set; }

        /// <summary>
        /// 11#激光器功率
        /// </summary>
        public double Laser11Power { get; set; }

        /// <summary>
        /// 11#激光器频率
        /// </summary>
        public double Laser11Freq { get; set; }

        /// <summary>
        /// 12#激光器功率
        /// </summary>
        public double Laser12Power { get; set; }

        /// <summary>
        /// 12#激光器频率
        /// </summary>
        public double Laser12Freq { get; set; }

        /// <summary>
        /// 硅胶膜是否清洗过
        /// </summary>
        public bool IsSilicaWashed { get; set; }

        /// <summary>
        /// 脏污位置是否焊接
        /// </summary>
        public bool IsDirtyPosMarked { get; set; }

        /// <summary>
        /// 1#振镜补偿X
        /// </summary>
        public double VibraOfs1X { get; set; }

        /// <summary>
        /// 1#振镜补偿Y
        /// </summary>
        public double VibraOfs1Y { get; set; }

        /// <summary>
        /// 1#振镜补偿A
        /// </summary>
        public double VibraOfs1A { get; set; }

        /// <summary>
        /// 2#振镜补偿X
        /// </summary>
        public double VibraOfs2X { get; set; }

        /// <summary>
        /// 2#振镜补偿Y
        /// </summary>
        public double VibraOfs2Y { get; set; }

        /// <summary>
        /// 2#振镜补偿A
        /// </summary>
        public double VibraOfs2A { get; set; }

        /// <summary>
        /// 3#振镜补偿X
        /// </summary>
        public double VibraOfs3X { get; set; }

        /// <summary>
        /// 3#振镜补偿Y
        /// </summary>
        public double VibraOfs3Y { get; set; }

        /// <summary>
        /// 3#振镜补偿A
        /// </summary>
        public double VibraOfs3A { get; set; }

        /// <summary>
        /// 4#振镜补偿X
        /// </summary>
        public double VibraOfs4X { get; set; }

        /// <summary>
        /// 4#振镜补偿Y
        /// </summary>
        public double VibraOfs4Y { get; set; }

        /// <summary>
        /// 4#振镜补偿A
        /// </summary>
        public double VibraOfs4A { get; set; }

        /// <summary>
        /// 5#振镜补偿X
        /// </summary>
        public double VibraOfs5X { get; set; }

        /// <summary>
        /// 5#振镜补偿Y
        /// </summary>
        public double VibraOfs5Y { get; set; }

        /// <summary>
        /// 5#振镜补偿A
        /// </summary>
        public double VibraOfs5A { get; set; }

        /// <summary>
        /// 6#振镜补偿X
        /// </summary>
        public double VibraOfs6X { get; set; }

        /// <summary>
        /// 6#振镜补偿Y
        /// </summary>
        public double VibraOfs6Y { get; set; }

        /// <summary>
        /// 6#振镜补偿A
        /// </summary>
        public double VibraOfs6A { get; set; }

        /// <summary>
        /// 7#振镜补偿X
        /// </summary>
        public double VibraOfs7X { get; set; }

        /// <summary>
        /// 7#振镜补偿Y
        /// </summary>
        public double VibraOfs7Y { get; set; }

        /// <summary>
        /// 7#振镜补偿A
        /// </summary>
        public double VibraOfs7A { get; set; }

        /// <summary>
        /// 8#振镜补偿X
        /// </summary>
        public double VibraOfs8X { get; set; }

        /// <summary>
        /// 8#振镜补偿Y
        /// </summary>
        public double VibraOfs8Y { get; set; }

        /// <summary>
        /// 8#振镜补偿A
        /// </summary>
        public double VibraOfs8A { get; set; }

        /// <summary>
        /// 9#振镜补偿X
        /// </summary>
        public double VibraOfs9X { get; set; }

        /// <summary>
        /// 9#振镜补偿Y
        /// </summary>
        public double VibraOfs9Y { get; set; }

        /// <summary>
        /// 9#振镜补偿A
        /// </summary>
        public double VibraOfs9A { get; set; }

        /// <summary>
        /// 10#振镜补偿X
        /// </summary>
        public double VibraOfs10X { get; set; }

        /// <summary>
        /// 10#振镜补偿Y
        /// </summary>
        public double VibraOfs10Y { get; set; }

        /// <summary>
        /// 10#振镜补偿A
        /// </summary>
        public double VibraOfs10A { get; set; }

        /// <summary>
        /// 11#振镜补偿X
        /// </summary>
        public double VibraOfs11X { get; set; }

        /// <summary>
        /// 11#振镜补偿Y
        /// </summary>
        public double VibraOfs11Y { get; set; }

        /// <summary>
        /// 11#振镜补偿A
        /// </summary>
        public double VibraOfs11A { get; set; }

        /// <summary>
        /// 12#振镜补偿X
        /// </summary>
        public double VibraOfs12X { get; set; }

        /// <summary>
        /// 12#振镜补偿Y
        /// </summary>
        public double VibraOfs12Y { get; set; }

        /// <summary>
        /// 12#振镜补偿A
        /// </summary>
        public double VibraOfs12A { get; set; }

        /// <summary>
        /// 拍照失败X阈值
        /// </summary>
        public double CameraShootFailThresX { get; set; }

        /// <summary>
        /// 拍照失败Y阈值
        /// </summary>
        public double CameraShootFailThresY { get; set; }

        /// <summary>
        /// 拍照失败A阈值
        /// </summary>
        public double CameraShootFailThresA { get; set; }

        public bool Changed(StPCParam obj)
        {
            if (obj == null) return false;
            return
                ProductionType != obj.ProductionType ||
                MarkingPath != obj.MarkingPath ||
                MarkingNamePrefix != obj.MarkingNamePrefix ||
                LogOutTime != obj.LogOutTime ||
                PowerMeterMeasurePos1X != obj.PowerMeterMeasurePos1X ||
                PowerMeterMeasurePos1Y != obj.PowerMeterMeasurePos1Y ||
                PowerMeterMeasurePos2X != obj.PowerMeterMeasurePos2X ||
                PowerMeterMeasurePos2Y != obj.PowerMeterMeasurePos2Y ||
                PowerMeterMeasurePos3X != obj.PowerMeterMeasurePos3X ||
                PowerMeterMeasurePos3Y != obj.PowerMeterMeasurePos3Y ||
                PowerMeterMeasurePos4X != obj.PowerMeterMeasurePos4X ||
                PowerMeterMeasurePos4Y != obj.PowerMeterMeasurePos4Y ||
                PowerMeterMeasurePos5X != obj.PowerMeterMeasurePos5X ||
                PowerMeterMeasurePos5Y != obj.PowerMeterMeasurePos5Y ||
                PowerMeterMeasurePos6X != obj.PowerMeterMeasurePos6X ||
                PowerMeterMeasurePos6Y != obj.PowerMeterMeasurePos6Y ||
                PowerMeterMeasurePos7X != obj.PowerMeterMeasurePos7X ||
                PowerMeterMeasurePos7Y != obj.PowerMeterMeasurePos7Y ||
                PowerMeterMeasurePos8X != obj.PowerMeterMeasurePos8X ||
                PowerMeterMeasurePos8Y != obj.PowerMeterMeasurePos8Y ||
                PowerMeterMeasurePos9X != obj.PowerMeterMeasurePos9X ||
                PowerMeterMeasurePos9Y != obj.PowerMeterMeasurePos9Y ||
                PowerMeterMeasurePos10X != obj.PowerMeterMeasurePos10X ||
                PowerMeterMeasurePos10Y != obj.PowerMeterMeasurePos10Y ||
                PowerMeterMeasurePos11X != obj.PowerMeterMeasurePos11X ||
                PowerMeterMeasurePos11Y != obj.PowerMeterMeasurePos11Y ||
                PowerMeterMeasurePos12X != obj.PowerMeterMeasurePos12X ||
                PowerMeterMeasurePos12Y != obj.PowerMeterMeasurePos12Y ||
                PowerMeterInterval != obj.PowerMeterInterval ||
                Laser1Power != obj.Laser1Power ||
                Laser1Freq != obj.Laser1Freq ||
                Laser2Power != obj.Laser2Power ||
                Laser2Freq != obj.Laser2Freq ||
                Laser3Power != obj.Laser3Power ||
                Laser3Freq != obj.Laser3Freq ||
                Laser4Power != obj.Laser4Power ||
                Laser4Freq != obj.Laser4Freq ||
                Laser5Power != obj.Laser5Power ||
                Laser5Freq != obj.Laser5Freq ||
                Laser6Power != obj.Laser6Power ||
                Laser6Freq != obj.Laser6Freq ||
                Laser7Power != obj.Laser7Power ||
                Laser7Freq != obj.Laser7Freq ||
                Laser8Power != obj.Laser8Power ||
                Laser8Freq != obj.Laser8Freq ||
                Laser9Power != obj.Laser9Power ||
                Laser9Freq != obj.Laser9Freq ||
                Laser10Power != obj.Laser10Power ||
                Laser10Freq != obj.Laser10Freq ||
                Laser11Power != obj.Laser11Power ||
                Laser11Freq != obj.Laser11Freq ||
                Laser12Power != obj.Laser12Power ||
                Laser12Freq != obj.Laser12Freq ||
                PowerMeterMeasureHl != obj.PowerMeterMeasureHl ||
                PowerMeterMeasureLl != obj.PowerMeterMeasureLl ||
                PowerMeterRatio != obj.PowerMeterRatio ||
                PowerMeterPercent != obj.PowerMeterPercent ||
                IsSilicaWashed != obj.IsSilicaWashed ||
                IsDirtyPosMarked != obj.IsDirtyPosMarked ||
                VibraOfs1X != obj.VibraOfs1X ||
                VibraOfs1Y != obj.VibraOfs1Y ||
                VibraOfs1A != obj.VibraOfs1A ||
                VibraOfs2X != obj.VibraOfs2X ||
                VibraOfs2Y != obj.VibraOfs2Y ||
                VibraOfs2A != obj.VibraOfs2A ||
                VibraOfs3X != obj.VibraOfs3X ||
                VibraOfs3Y != obj.VibraOfs3Y ||
                VibraOfs3A != obj.VibraOfs3A ||
                VibraOfs4X != obj.VibraOfs4X ||
                VibraOfs4Y != obj.VibraOfs4Y ||
                VibraOfs4A != obj.VibraOfs4A ||
                VibraOfs5X != obj.VibraOfs5X ||
                VibraOfs5Y != obj.VibraOfs5Y ||
                VibraOfs5A != obj.VibraOfs5A ||
                VibraOfs6X != obj.VibraOfs6X ||
                VibraOfs6Y != obj.VibraOfs6Y ||
                VibraOfs6A != obj.VibraOfs6A ||
                VibraOfs7X != obj.VibraOfs7X ||
                VibraOfs7Y != obj.VibraOfs7Y ||
                VibraOfs7A != obj.VibraOfs7A ||
                VibraOfs8X != obj.VibraOfs8X ||
                VibraOfs8Y != obj.VibraOfs8Y ||
                VibraOfs8A != obj.VibraOfs8A ||
                VibraOfs9X != obj.VibraOfs9X ||
                VibraOfs9Y != obj.VibraOfs9Y ||
                VibraOfs9A != obj.VibraOfs9A ||
                VibraOfs10X != obj.VibraOfs10X ||
                VibraOfs10Y != obj.VibraOfs10Y ||
                VibraOfs10A != obj.VibraOfs10A ||
                VibraOfs11X != obj.VibraOfs11X ||
                VibraOfs11Y != obj.VibraOfs11Y ||
                VibraOfs11A != obj.VibraOfs11A ||
                VibraOfs12X != obj.VibraOfs12X ||
                VibraOfs12Y != obj.VibraOfs12Y ||
                VibraOfs12A != obj.VibraOfs12A ||
                CameraShootFailThresX != obj.CameraShootFailThresX ||
                CameraShootFailThresY != obj.CameraShootFailThresY ||
                CameraShootFailThresA != obj.CameraShootFailThresA;
        }

        public StPCParam Clone()
        {
            return new StPCParam
            {
                ProductionType = this.ProductionType,
                MarkingPath = this.MarkingPath,
                MarkingNamePrefix = this.MarkingNamePrefix,
                LogOutTime = this.LogOutTime,
                PowerMeterMeasurePos1X = this.PowerMeterMeasurePos1X,
                PowerMeterMeasurePos1Y = this.PowerMeterMeasurePos1Y,
                PowerMeterMeasurePos2X = this.PowerMeterMeasurePos2X,
                PowerMeterMeasurePos2Y = this.PowerMeterMeasurePos2Y,
                PowerMeterMeasurePos3X = this.PowerMeterMeasurePos3X,
                PowerMeterMeasurePos3Y = this.PowerMeterMeasurePos3Y,
                PowerMeterMeasurePos4X = this.PowerMeterMeasurePos4X,
                PowerMeterMeasurePos4Y = this.PowerMeterMeasurePos4Y,
                PowerMeterMeasurePos5X = this.PowerMeterMeasurePos5X,
                PowerMeterMeasurePos5Y = this.PowerMeterMeasurePos5Y,
                PowerMeterMeasurePos6X = this.PowerMeterMeasurePos6X,
                PowerMeterMeasurePos6Y = this.PowerMeterMeasurePos6Y,
                PowerMeterMeasurePos7X = this.PowerMeterMeasurePos7X,
                PowerMeterMeasurePos7Y = this.PowerMeterMeasurePos7Y,
                PowerMeterMeasurePos8X = this.PowerMeterMeasurePos8X,
                PowerMeterMeasurePos8Y = this.PowerMeterMeasurePos8Y,
                PowerMeterMeasurePos9X = this.PowerMeterMeasurePos9X,
                PowerMeterMeasurePos9Y = this.PowerMeterMeasurePos9Y,
                PowerMeterMeasurePos10X = this.PowerMeterMeasurePos10X,
                PowerMeterMeasurePos10Y = this.PowerMeterMeasurePos10Y,
                PowerMeterMeasurePos11X = this.PowerMeterMeasurePos11X,
                PowerMeterMeasurePos11Y = this.PowerMeterMeasurePos11Y,
                PowerMeterMeasurePos12X = this.PowerMeterMeasurePos12X,
                PowerMeterMeasurePos12Y = this.PowerMeterMeasurePos12Y,
                PowerMeterInterval = this.PowerMeterInterval,
                Laser1Power = this.Laser1Power,
                Laser1Freq = this.Laser1Freq,
                Laser2Power = this.Laser2Power,
                Laser2Freq = this.Laser2Freq,
                Laser3Power = this.Laser3Power,
                Laser3Freq = this.Laser3Freq,
                Laser4Power = this.Laser4Power,
                Laser4Freq = this.Laser4Freq,
                Laser5Power = this.Laser5Power,
                Laser5Freq = this.Laser5Freq,
                Laser6Power = this.Laser6Power,
                Laser6Freq = this.Laser6Freq,
                Laser7Power = this.Laser7Power,
                Laser7Freq = this.Laser7Freq,
                Laser8Power = this.Laser8Power,
                Laser8Freq = this.Laser8Freq,
                Laser9Power = this.Laser9Power,
                Laser9Freq = this.Laser9Freq,
                Laser10Power = this.Laser10Power,
                Laser10Freq = this.Laser10Freq,
                Laser11Power = this.Laser11Power,
                Laser11Freq = this.Laser11Freq,
                Laser12Power = this.Laser12Power,
                Laser12Freq = this.Laser12Freq,
                PowerMeterMeasureHl = this.PowerMeterMeasureHl,
                PowerMeterMeasureLl = this.PowerMeterMeasureLl,
                PowerMeterRatio = this.PowerMeterRatio,
                PowerMeterPercent = this.PowerMeterPercent,
                IsSilicaWashed = this.IsSilicaWashed,
                IsDirtyPosMarked = this.IsDirtyPosMarked,
                VibraOfs1X = this.VibraOfs1X,
                VibraOfs1Y = this.VibraOfs1Y,
                VibraOfs1A = this.VibraOfs1A,
                VibraOfs2X = this.VibraOfs2X,
                VibraOfs2Y = this.VibraOfs2Y,
                VibraOfs2A = this.VibraOfs2A,
                VibraOfs3X = this.VibraOfs3X,
                VibraOfs3Y = this.VibraOfs3Y,
                VibraOfs3A = this.VibraOfs3A,
                VibraOfs4X = this.VibraOfs4X,
                VibraOfs4Y = this.VibraOfs4Y,
                VibraOfs4A = this.VibraOfs4A,
                VibraOfs5X = this.VibraOfs5X,
                VibraOfs5Y = this.VibraOfs5Y,
                VibraOfs5A = this.VibraOfs5A,
                VibraOfs6X = this.VibraOfs6X,
                VibraOfs6Y = this.VibraOfs6Y,
                VibraOfs6A = this.VibraOfs6A,
                VibraOfs7X = this.VibraOfs7X,
                VibraOfs7Y = this.VibraOfs7Y,
                VibraOfs7A = this.VibraOfs7A,
                VibraOfs8X = this.VibraOfs8X,
                VibraOfs8Y = this.VibraOfs8Y,
                VibraOfs8A = this.VibraOfs8A,
                VibraOfs9X = this.VibraOfs9X,
                VibraOfs9Y = this.VibraOfs9Y,
                VibraOfs9A = this.VibraOfs9A,
                VibraOfs10X = this.VibraOfs10X,
                VibraOfs10Y = this.VibraOfs10Y,
                VibraOfs10A = this.VibraOfs10A,
                VibraOfs11X = this.VibraOfs11X,
                VibraOfs11Y = this.VibraOfs11Y,
                VibraOfs11A = this.VibraOfs11A,
                VibraOfs12X = this.VibraOfs12X,
                VibraOfs12Y = this.VibraOfs12Y,
                VibraOfs12A = this.VibraOfs12A,
                CameraShootFailThresX = this.CameraShootFailThresX,
                CameraShootFailThresY = this.CameraShootFailThresY,
                CameraShootFailThresA = this.CameraShootFailThresA
            };
        }
    }
}