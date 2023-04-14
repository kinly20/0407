namespace DRsoft.Modeling.ConfigurationFileRead
{
    public class PTPEngineRead
    {
        //public static Configure cfg = new Configure("D:\\PTP\\PTP_Engine.dat");//temporary
        //string tmp = "";
        //PTPEngine engine = new PTPEngine();

        //public PTPEngineRead()
        //{
        //    //[Wafer_Edges]
        //    engine.WaferEdges.CountType = cfg.ReadConfig("Wafer_Edges", "CountType", "");
        //    engine.WaferEdges.WafersNum = Convert.ToInt32(cfg.ReadConfig("Wafer_Edges", "WafersNum", ""));
        //    engine.WaferEdges.SegmentsNum = Convert.ToInt32(cfg.ReadConfig("Wafer_Edges", "SegmentsNum", ""));


        //    //[Print_Order]
        //    engine.PrintOrder.PostImagesBefore = Convert.ToInt32(cfg.ReadConfig("Print_Order", "PostImagesBefore", ""));
        //    var sr1 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Print_Order", "PTPEnginePrintOrder", ""), " ");
        //    engine.PrintOrder.PrintOrderInfo.Add(Convert.ToInt32(sr1.Split(" ")[0]));
        //    engine.PrintOrder.PrintOrderInfo.Add(Convert.ToInt32(sr1.Split(" ")[1]));
        //    engine.PrintOrder.PrintOrderInfo.Add(Convert.ToInt32(sr1.Split(" ")[2]));
        //    engine.PrintOrder.PrintOrderInfo.Add(Convert.ToInt32(sr1.Split(" ")[3]));
        //    engine.PrintOrder.PrintOrderInfo.Add(Convert.ToInt32(sr1.Split(" ")[4]));
        //    engine.PrintOrder.PrintOrderInfo.Add(Convert.ToInt32(sr1.Split(" ")[5]));
        //    engine.PrintOrder.PrintOrderInfo.Add(Convert.ToInt32(sr1.Split(" ")[6]));
        //    engine.PrintOrder.PrintOrderInfo.Add(Convert.ToInt32(sr1.Split(" ")[7]));
        //    engine.PrintOrder.PrintOrderInfo.Add(Convert.ToInt32(sr1.Split(" ")[8]));
        //    engine.PrintOrder.FastOffsetsApply = Convert.ToInt32(cfg.ReadConfig("Print_Order", "FastOffsetsApply", ""));

        //    //[PTPEngineSplicing]
        //    engine.Splicing.SegmentsInRoll = Convert.ToInt32(cfg.ReadConfig("PTPEngineSplicing", "SegmentsInRoll", ""));
        //    engine.Splicing.AccumulatorSize = Convert.ToInt32(cfg.ReadConfig("PTPEngineSplicing", "AccumulatorSize", ""));
        //    engine.Splicing.SegmentTime = Convert.ToInt32(cfg.ReadConfig("PTPEngineSplicing", "SegmentTime", ""));
        //    engine.Splicing.SegmentsLeftWarning = Convert.ToInt32(cfg.ReadConfig("PTPEngineSplicing", "SegmentsLeftWarning", ""));

        //    //[PTPEngineLaser]
        //    engine.Laser.Type = cfg.ReadConfig("PTPEngineLaser", "Type", "");
        //    engine.Laser.IPAddress = cfg.ReadConfig("PTPEngineLaser", "IP_Address", "");
        //    engine.Laser.AlarmInterval = Convert.ToInt32(cfg.ReadConfig("PTPEngineLaser", "AlarmInterval", ""));
        //    engine.Laser.TemperatureInterval = Convert.ToInt32(cfg.ReadConfig("PTPEngineLaser", "TemperatureInterval", ""));
        //    engine.Laser.PowerInterval = Convert.ToInt32(cfg.ReadConfig("PTPEngineLaser", "PowerInterval", ""));
        //    engine.Laser.PowerSetpoint = Convert.ToInt32(cfg.ReadConfig("PTPEngineLaser", "PowerSetpoint", ""));
        //    engine.Laser.BoostedPower = Convert.ToInt32(cfg.ReadConfig("PTPEngineLaser", "BoostedPower", ""));
        //    engine.Laser.AuxiliaryPort = Convert.ToInt32(cfg.ReadConfig("PTPEngineLaser", "AuxiliaryPort", ""));

        //    //[PTPEngineSegmentShifts]
        //    engine.SegmentShifts.NumSegmentShifts = Convert.ToInt32(cfg.ReadConfig("PTPEngineSegmentShifts", "NumSegmentShifts", ""));
        //    engine.SegmentShifts.MaxAutoAdvances = Convert.ToInt32(cfg.ReadConfig("PTPEngineSegmentShifts", "MaxAutoAdvances", ""));
        //    engine.SegmentShifts.PitchCorrectionFreq = Convert.ToInt32(cfg.ReadConfig("PTPEngineSegmentShifts", "PitchCorrectionFreq", ""));
        //    engine.SegmentShifts.StableCorrectionRange = Convert.ToInt32(cfg.ReadConfig("PTPEngineSegmentShifts", "StableCorrectionRange", ""));

        //    //[Cameras]
        //    engine.Cameras.MaxRetries = Convert.ToInt32(cfg.ReadConfig("Cameras", "MaxRetries", ""));
        //    engine.Cameras.ProcessFnC = Convert.ToInt32(cfg.ReadConfig("Cameras", "ProcessFnC", ""));
        //    engine.Cameras.ProcessMisprints = Convert.ToInt32(cfg.ReadConfig("Cameras", "ProcessMisprints", ""));
        //    engine.Cameras.MaxRejectedSegments = Convert.ToInt32(cfg.ReadConfig("Cameras", "MaxRejectedSegments", ""));

        //    //[Delays]
        //    engine.Delays.WaferPrintDelay = Convert.ToInt32(cfg.ReadConfig("Delays", "WaferPrintDelay", ""));
        //    engine.Delays.PostBreakSegments = Convert.ToInt32(cfg.ReadConfig("Delays", "PostBreakSegments", ""));
        //    engine.Delays.PasteDryTime = Convert.ToInt32(cfg.ReadConfig("Delays", "PasteDryTime", ""));
        //    engine.Delays.SegmentSkipMax = Convert.ToInt32(cfg.ReadConfig("Delays", "SegmentSkipMax", ""));
        //    engine.Delays.ExtraSkip = Convert.ToInt32(cfg.ReadConfig("Delays", "ExtraSkip", ""));

        //    //[TestSegments]
        //    engine.TestSegments.NumberOfSegments = Convert.ToInt32(cfg.ReadConfig("TestSegments", "NumberOfSegments", ""));
        //    engine.TestSegments.LaserPower = Convert.ToInt32(cfg.ReadConfig("TestSegments", "LaserPower", ""));
        //    engine.TestSegments.MaxLateral = Convert.ToInt32(cfg.ReadConfig("TestSegments", "MaxLateral", ""));
        //    engine.TestSegments.MaxAngular = Convert.ToInt32(cfg.ReadConfig("TestSegments", "MaxAngular", ""));

        //    //[DefectSThresholds]
        //    engine.Defects.DefectLength = Convert.ToInt32(cfg.ReadConfig("DefectSThresholds", "DefectLength", ""));
        //    engine.Defects.DarkerDefectGL = Convert.ToInt32(cfg.ReadConfig("DefectSThresholds", "DarkerDefectGL", ""));
        //    engine.Defects.BrighterDefectGL = Convert.ToInt32(cfg.ReadConfig("DefectSThresholds", "BrighterDefectGL", ""));
        //    engine.Defects.MaxDefectsSequence = Convert.ToInt32(cfg.ReadConfig("DefectSThresholds", "MaxDefectsSequence", ""));
        //    engine.Defects.SkipDefectTrenches = Convert.ToInt32(cfg.ReadConfig("DefectSThresholds", "SkipDefectTrenches", ""));

        //    //[WorkingWindow]
        //    engine.WWW.MaxDistortion = Convert.ToInt32(cfg.ReadConfig("WorkingWindow", "MaxDistortion", ""));

        //    //[PrintDefects]
        //    engine.PrintDefects.CMDMisprint = Convert.ToInt32(cfg.ReadConfig("PrintDefects", "CMD_Misprint", ""));
        //    engine.PrintDefects.MDMisprint = Convert.ToInt32(cfg.ReadConfig("PrintDefects", "MD_Misprint", ""));
        //    engine.PrintDefects.MaxMisprints = Convert.ToInt32(cfg.ReadConfig("PrintDefects", "MaxMisprints", ""));
        //    engine.PrintDefects.SegmentMisprints = Convert.ToInt32(cfg.ReadConfig("PrintDefects", "SegmentMisprints", ""));

        //    //[ScannerBoard]
        //    engine.ScannerBoard.Port = Convert.ToInt32(cfg.ReadConfig("ScannerBoard", "Port", ""));
        //    engine.ScannerBoard.LeftOffset = Convert.ToInt32(cfg.ReadConfig("ScannerBoard", "LeftOffset", ""));
        //    engine.ScannerBoard.RightOffset = Convert.ToInt32(cfg.ReadConfig("ScannerBoard", "RightOffset", ""));


        //    //[PrintSequence]
        //    engine.PrintSequence.InitialSegments = Convert.ToInt32(cfg.ReadConfig("PrintSequence", "InitialSegments", ""));
        //    engine.PrintSequence.WWSegments = Convert.ToInt32(cfg.ReadConfig("PrintSequence", "WWSegments", ""));
        //    engine.PrintSequence.TestSegmentsFlag = Convert.ToInt32(cfg.ReadConfig("PrintSequence", "TestSegmentsFlag", ""));

        //    //[Booster]
        //    engine.Booster.FingerDistance = Convert.ToInt32(cfg.ReadConfig("Booster", "FingerDistance", ""));

        //    //[Errors]
        //    engine.Errors.ErrorFolder = Convert.ToInt32(cfg.ReadConfig("Errors", "ErrorFolder", ""));

        //}
    }
}
