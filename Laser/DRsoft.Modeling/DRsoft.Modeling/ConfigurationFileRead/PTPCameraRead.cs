namespace DRsoft.Modeling.ConfigurationFileRead
{
    public class PTPCameraRead
    {
        //public readonly string path = @"D:\PTP\PTP_Camera.dat";//temporary
        //public Camera cameras = new Camera();
        //public static Configure cfg = new Configure("D:\\PTP\\PTP_Camera.dat");//temporary
        //string tmp = "";
        //public PTPCameraRead()
        //{
        //    string[] strArray = File.ReadAllLines(path);
        //    //'Version 1.1.0
        //    cameras.CameraVersion = strArray[0];


        //    //[PAL]
        //    var sr1 = new Regex("[\\s]+").Replace(cfg.ReadConfig("PAL", "DefaultTaskSetup", ""), " ");
        //    var sr2 = new Regex("[\\s]+").Replace(cfg.ReadConfig("PAL", "ChuckCornerSetup", ""), " ");
        //    var sr3 = new Regex("[\\s]+").Replace(cfg.ReadConfig("PAL", "CameraCalibrationSetup", ""), " ");
        //    cameras.CameraInfoDic.Add(new CameraInfoPTP
        //    {
        //        CameraName = cfg.ReadConfig("PAL", "Camera_Name", ""),
        //        RealCameraName = cfg.ReadConfig("PAL", "'Camera_Name", ""),
        //        TriggerType = cfg.ReadConfig("PAL", "Trigger_Type", ""),
        //        PaketSize = Convert.ToInt32(cfg.ReadConfig("PAL", "PacketSize", "")),
        //        PaketDelay = Convert.ToInt32(cfg.ReadConfig("PAL", "PacketDelay", "")),
        //        DefaultTaskSetup1 = Convert.ToInt32(sr1.Split(" ")[0]),
        //        DefaultTaskSetup2 = Convert.ToInt32(sr1.Split(" ")[1]),
        //        DefaultTaskSetup3 = Convert.ToInt32(sr1.Split(" ")[2]),
        //        DefaultTaskSetup4 = Convert.ToInt32(sr1.Split(" ")[3]),
        //        DefaultTaskSetup5 = Convert.ToInt32(sr1.Split(" ")[4]),
        //        DefaultTaskSetup6 = Convert.ToInt32(sr1.Split(" ")[5]),
        //        DefaultTaskSetup7 = Convert.ToInt32(sr1.Split(" ")[6]),
        //        DefaultTaskSetup8 = Convert.ToInt32(sr1.Split(" ")[7]),
        //        ChuckCornerSetup1 = Convert.ToInt32(sr2.Split(" ")[0]),
        //        ChuckCornerSetup2 = Convert.ToInt32(sr2.Split(" ")[1]),
        //        ChuckCornerSetup3 = Convert.ToInt32(sr2.Split(" ")[2]),
        //        ChuckCornerSetup4 = Convert.ToInt32(sr2.Split(" ")[3]),
        //        ChuckCornerSetup5 = Convert.ToInt32(sr2.Split(" ")[4]),
        //        ChuckCornerSetup6 = Convert.ToInt32(sr2.Split(" ")[5]),
        //        ChuckCornerSetup7 = Convert.ToInt32(sr2.Split(" ")[6]),
        //        ChuckCornerSetup8 = Convert.ToInt32(sr2.Split(" ")[7]),
        //        CameraCalibrationSetup1 = Convert.ToInt32(sr3.Split(" ")[0]),
        //        CameraCalibrationSetup2 = Convert.ToInt32(sr3.Split(" ")[1]),
        //        CameraCalibrationSetup3 = Convert.ToInt32(sr3.Split(" ")[2]),
        //        CameraCalibrationSetup4 = Convert.ToInt32(sr3.Split(" ")[3]),
        //        CameraCalibrationSetup5 = Convert.ToInt32(sr3.Split(" ")[4]),
        //        CameraCalibrationSetup6 = Convert.ToInt32(sr3.Split(" ")[5]),
        //        CameraCalibrationSetup7 = Convert.ToInt32(sr3.Split(" ")[6]),
        //        CameraCalibrationSetup8 = Convert.ToInt32(sr3.Split(" ")[7])
        //    }
        //    );



        //    //[PAL2]
        //    var sr4 = new Regex("[\\s]+").Replace(cfg.ReadConfig("PAL2", "DefaultTaskSetup", ""), " ");
        //    var sr5 = new Regex("[\\s]+").Replace(cfg.ReadConfig("PAL2", "ChuckCornerSetup", ""), " ");
        //    var sr6 = new Regex("[\\s]+").Replace(cfg.ReadConfig("PAL2", "CameraCalibrationSetup", ""), " ");
        //    cameras.CameraInfoDic.Add(new CameraInfoPTP
        //    {
        //        CameraName = cfg.ReadConfig("PAL2", "Camera_Name", ""),
        //        RealCameraName = cfg.ReadConfig("PA2L", "'Camera_Name", ""),
        //        TriggerType = cfg.ReadConfig("PAL2", "Trigger_Type", ""),
        //        PaketSize = Convert.ToInt32(cfg.ReadConfig("PAL2", "PacketSize", "")),
        //        PaketDelay = Convert.ToInt32(cfg.ReadConfig("PAL2", "PacketDelay", "")),
        //        DefaultTaskSetup1 = Convert.ToInt32(sr4.Split(" ")[0]),
        //        DefaultTaskSetup2 = Convert.ToInt32(sr4.Split(" ")[1]),
        //        DefaultTaskSetup3 = Convert.ToInt32(sr4.Split(" ")[2]),
        //        DefaultTaskSetup4 = Convert.ToInt32(sr4.Split(" ")[3]),
        //        DefaultTaskSetup5 = Convert.ToInt32(sr4.Split(" ")[4]),
        //        DefaultTaskSetup6 = Convert.ToInt32(sr4.Split(" ")[5]),
        //        DefaultTaskSetup7 = Convert.ToInt32(sr4.Split(" ")[6]),
        //        DefaultTaskSetup8 = Convert.ToInt32(sr4.Split(" ")[7]),
        //        ChuckCornerSetup1 = Convert.ToInt32(sr5.Split(" ")[0]),
        //        ChuckCornerSetup2 = Convert.ToInt32(sr5.Split(" ")[1]),
        //        ChuckCornerSetup3 = Convert.ToInt32(sr5.Split(" ")[2]),
        //        ChuckCornerSetup4 = Convert.ToInt32(sr5.Split(" ")[3]),
        //        ChuckCornerSetup5 = Convert.ToInt32(sr5.Split(" ")[4]),
        //        ChuckCornerSetup6 = Convert.ToInt32(sr5.Split(" ")[5]),
        //        ChuckCornerSetup7 = Convert.ToInt32(sr5.Split(" ")[6]),
        //        ChuckCornerSetup8 = Convert.ToInt32(sr5.Split(" ")[7]),
        //        CameraCalibrationSetup1 = Convert.ToInt32(sr6.Split(" ")[0]),
        //        CameraCalibrationSetup2 = Convert.ToInt32(sr6.Split(" ")[1]),
        //        CameraCalibrationSetup3 = Convert.ToInt32(sr6.Split(" ")[2]),
        //        CameraCalibrationSetup4 = Convert.ToInt32(sr6.Split(" ")[3]),
        //        CameraCalibrationSetup5 = Convert.ToInt32(sr6.Split(" ")[4]),
        //        CameraCalibrationSetup6 = Convert.ToInt32(sr6.Split(" ")[5]),
        //        CameraCalibrationSetup7 = Convert.ToInt32(sr6.Split(" ")[6]),
        //        CameraCalibrationSetup8 = Convert.ToInt32(sr6.Split(" ")[7])
        //    }
        //    );


        //    //[Alignment Right Start]
        //    var sr7 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Right Start", "DefaultTaskSetup", ""), " ");
        //    var sr8 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Right Start", "SegmentAlignTaskSetup", ""), " ");
        //    var sr9 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Right Start", "WaferAlignTaskSetup", ""), " ");
        //    var sr10 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Right Start", "ScanCalibTaskSetup", ""), " ");
        //    var sr11 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Right Start", "ChuckCornerSetup", ""), " ");
        //    var sr12 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Right Start", "CameraCalibrationSetup", ""), " ");
        //    cameras.CameraInfoDic.Add(new CameraInfoPTP
        //    {
        //        CameraName = cfg.ReadConfig("Alignment Right Start", "Camera_Name", ""),
        //        RealCameraName = cfg.ReadConfig("Alignment Right Start", "'Camera_Name", ""),
        //        TriggerType = cfg.ReadConfig("Alignment Right Start", "Trigger_Type", ""),
        //        PaketSize = Convert.ToInt32(cfg.ReadConfig("Alignment Right Start", "PacketSize", "")),
        //        PaketDelay = Convert.ToInt32(cfg.ReadConfig("Alignment Right Start", "PacketDelay", "")),
        //        DefaultTaskSetup1 = Convert.ToInt32(sr7.Split(" ")[0]),
        //        DefaultTaskSetup2 = Convert.ToInt32(sr7.Split(" ")[1]),
        //        DefaultTaskSetup3 = Convert.ToInt32(sr7.Split(" ")[2]),
        //        DefaultTaskSetup4 = Convert.ToInt32(sr7.Split(" ")[3]),
        //        DefaultTaskSetup5 = Convert.ToInt32(sr7.Split(" ")[4]),
        //        DefaultTaskSetup6 = Convert.ToInt32(sr7.Split(" ")[5]),
        //        DefaultTaskSetup7 = Convert.ToInt32(sr7.Split(" ")[6]),
        //        DefaultTaskSetup8 = Convert.ToInt32(sr7.Split(" ")[7]),
        //        SegmentAlignTaskSetup1 = Convert.ToInt32(sr8.Split(" ")[0]),
        //        SegmentAlignTaskSetup2 = Convert.ToInt32(sr8.Split(" ")[1]),
        //        SegmentAlignTaskSetup3 = Convert.ToInt32(sr8.Split(" ")[2]),
        //        SegmentAlignTaskSetup4 = Convert.ToInt32(sr8.Split(" ")[3]),
        //        SegmentAlignTaskSetup5 = Convert.ToInt32(sr8.Split(" ")[4]),
        //        SegmentAlignTaskSetup6 = Convert.ToInt32(sr8.Split(" ")[5]),
        //        SegmentAlignTaskSetup7 = Convert.ToInt32(sr8.Split(" ")[6]),
        //        SegmentAlignTaskSetup8 = Convert.ToInt32(sr8.Split(" ")[7]),
        //        WaferAlignTaskSetup1 = Convert.ToInt32(sr9.Split(" ")[0]),
        //        WaferAlignTaskSetup2 = Convert.ToInt32(sr9.Split(" ")[1]),
        //        WaferAlignTaskSetup3 = Convert.ToInt32(sr9.Split(" ")[2]),
        //        WaferAlignTaskSetup4 = Convert.ToInt32(sr9.Split(" ")[3]),
        //        WaferAlignTaskSetup5 = Convert.ToInt32(sr9.Split(" ")[4]),
        //        WaferAlignTaskSetup6 = Convert.ToInt32(sr9.Split(" ")[5]),
        //        WaferAlignTaskSetup7 = Convert.ToInt32(sr9.Split(" ")[6]),
        //        WaferAlignTaskSetup8 = Convert.ToInt32(sr9.Split(" ")[7]),
        //        ScanCalibTaskSetup1 = Convert.ToInt32(sr10.Split(" ")[0]),
        //        ScanCalibTaskSetup2 = Convert.ToInt32(sr10.Split(" ")[1]),
        //        ScanCalibTaskSetup3 = Convert.ToInt32(sr10.Split(" ")[2]),
        //        ScanCalibTaskSetup4 = Convert.ToInt32(sr10.Split(" ")[3]),
        //        ScanCalibTaskSetup5 = Convert.ToInt32(sr10.Split(" ")[4]),
        //        ScanCalibTaskSetup6 = Convert.ToInt32(sr10.Split(" ")[5]),
        //        ScanCalibTaskSetup7 = Convert.ToInt32(sr10.Split(" ")[6]),
        //        ScanCalibTaskSetup8 = Convert.ToInt32(sr10.Split(" ")[7]),
        //        ChuckCornerSetup1 = Convert.ToInt32(sr11.Split(" ")[0]),
        //        ChuckCornerSetup2 = Convert.ToInt32(sr11.Split(" ")[1]),
        //        ChuckCornerSetup3 = Convert.ToInt32(sr11.Split(" ")[2]),
        //        ChuckCornerSetup4 = Convert.ToInt32(sr11.Split(" ")[3]),
        //        ChuckCornerSetup5 = Convert.ToInt32(sr11.Split(" ")[4]),
        //        ChuckCornerSetup6 = Convert.ToInt32(sr11.Split(" ")[5]),
        //        ChuckCornerSetup7 = Convert.ToInt32(sr11.Split(" ")[6]),
        //        ChuckCornerSetup8 = Convert.ToInt32(sr11.Split(" ")[7]),
        //        CameraCalibrationSetup1 = Convert.ToInt32(sr12.Split(" ")[0]),
        //        CameraCalibrationSetup2 = Convert.ToInt32(sr12.Split(" ")[1]),
        //        CameraCalibrationSetup3 = Convert.ToInt32(sr12.Split(" ")[2]),
        //        CameraCalibrationSetup4 = Convert.ToInt32(sr12.Split(" ")[3]),
        //        CameraCalibrationSetup5 = Convert.ToInt32(sr12.Split(" ")[4]),
        //        CameraCalibrationSetup6 = Convert.ToInt32(sr12.Split(" ")[5]),
        //        CameraCalibrationSetup7 = Convert.ToInt32(sr12.Split(" ")[6]),
        //        CameraCalibrationSetup8 = Convert.ToInt32(sr12.Split(" ")[7])
        //    }
        //    );



        //    //[Alignment Right mid Start]
        //    var sr13 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Right mid Start", "DefaultTaskSetup", ""), " ");
        //    var sr14 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Right mid Start", "SegmentAlignTaskSetup", ""), " ");
        //    var sr15 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Right mid Start", "WaferAlignTaskSetup", ""), " ");
        //    var sr16 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Right mid Start", "ScanCalibTaskSetup", ""), " ");
        //    var sr17 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Right mid Start", "ChuckCornerSetup", ""), " ");
        //    var sr18 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Right mid Start", "CameraCalibrationSetup", ""), " ");
        //    cameras.CameraInfoDic.Add(new CameraInfoPTP
        //    {
        //        CameraName = cfg.ReadConfig("Alignment Right mid Start", "Camera_Name", ""),
        //        RealCameraName = cfg.ReadConfig("Alignment Right mid Start", "'Camera_Name", ""),
        //        TriggerType = cfg.ReadConfig("Alignment Right mid Start", "Trigger_Type", ""),
        //        PaketSize = Convert.ToInt32(cfg.ReadConfig("Alignment Right mid Start", "PacketSize", "")),
        //        PaketDelay = Convert.ToInt32(cfg.ReadConfig("Alignment Right mid Start", "PacketDelay", "")),
        //        DefaultTaskSetup1 = Convert.ToInt32(sr13.Split(" ")[0]),
        //        DefaultTaskSetup2 = Convert.ToInt32(sr13.Split(" ")[1]),
        //        DefaultTaskSetup3 = Convert.ToInt32(sr13.Split(" ")[2]),
        //        DefaultTaskSetup4 = Convert.ToInt32(sr13.Split(" ")[3]),
        //        DefaultTaskSetup5 = Convert.ToInt32(sr13.Split(" ")[4]),
        //        DefaultTaskSetup6 = Convert.ToInt32(sr13.Split(" ")[5]),
        //        DefaultTaskSetup7 = Convert.ToInt32(sr13.Split(" ")[6]),
        //        DefaultTaskSetup8 = Convert.ToInt32(sr13.Split(" ")[7]),
        //        SegmentAlignTaskSetup1 = Convert.ToInt32(sr14.Split(" ")[0]),
        //        SegmentAlignTaskSetup2 = Convert.ToInt32(sr14.Split(" ")[1]),
        //        SegmentAlignTaskSetup3 = Convert.ToInt32(sr14.Split(" ")[2]),
        //        SegmentAlignTaskSetup4 = Convert.ToInt32(sr14.Split(" ")[3]),
        //        SegmentAlignTaskSetup5 = Convert.ToInt32(sr14.Split(" ")[4]),
        //        SegmentAlignTaskSetup6 = Convert.ToInt32(sr14.Split(" ")[5]),
        //        SegmentAlignTaskSetup7 = Convert.ToInt32(sr14.Split(" ")[6]),
        //        SegmentAlignTaskSetup8 = Convert.ToInt32(sr14.Split(" ")[7]),
        //        WaferAlignTaskSetup1 = Convert.ToInt32(sr15.Split(" ")[0]),
        //        WaferAlignTaskSetup2 = Convert.ToInt32(sr15.Split(" ")[1]),
        //        WaferAlignTaskSetup3 = Convert.ToInt32(sr15.Split(" ")[2]),
        //        WaferAlignTaskSetup4 = Convert.ToInt32(sr15.Split(" ")[3]),
        //        WaferAlignTaskSetup5 = Convert.ToInt32(sr15.Split(" ")[4]),
        //        WaferAlignTaskSetup6 = Convert.ToInt32(sr15.Split(" ")[5]),
        //        WaferAlignTaskSetup7 = Convert.ToInt32(sr15.Split(" ")[6]),
        //        WaferAlignTaskSetup8 = Convert.ToInt32(sr15.Split(" ")[7]),
        //        ScanCalibTaskSetup1 = Convert.ToInt32(sr16.Split(" ")[0]),
        //        ScanCalibTaskSetup2 = Convert.ToInt32(sr16.Split(" ")[1]),
        //        ScanCalibTaskSetup3 = Convert.ToInt32(sr16.Split(" ")[2]),
        //        ScanCalibTaskSetup4 = Convert.ToInt32(sr16.Split(" ")[3]),
        //        ScanCalibTaskSetup5 = Convert.ToInt32(sr16.Split(" ")[4]),
        //        ScanCalibTaskSetup6 = Convert.ToInt32(sr16.Split(" ")[5]),
        //        ScanCalibTaskSetup7 = Convert.ToInt32(sr16.Split(" ")[6]),
        //        ScanCalibTaskSetup8 = Convert.ToInt32(sr16.Split(" ")[7]),
        //        ChuckCornerSetup1 = Convert.ToInt32(sr17.Split(" ")[0]),
        //        ChuckCornerSetup2 = Convert.ToInt32(sr17.Split(" ")[1]),
        //        ChuckCornerSetup3 = Convert.ToInt32(sr17.Split(" ")[2]),
        //        ChuckCornerSetup4 = Convert.ToInt32(sr17.Split(" ")[3]),
        //        ChuckCornerSetup5 = Convert.ToInt32(sr17.Split(" ")[4]),
        //        ChuckCornerSetup6 = Convert.ToInt32(sr17.Split(" ")[5]),
        //        ChuckCornerSetup7 = Convert.ToInt32(sr17.Split(" ")[6]),
        //        ChuckCornerSetup8 = Convert.ToInt32(sr17.Split(" ")[7]),
        //        CameraCalibrationSetup1 = Convert.ToInt32(sr18.Split(" ")[0]),
        //        CameraCalibrationSetup2 = Convert.ToInt32(sr18.Split(" ")[1]),
        //        CameraCalibrationSetup3 = Convert.ToInt32(sr18.Split(" ")[2]),
        //        CameraCalibrationSetup4 = Convert.ToInt32(sr18.Split(" ")[3]),
        //        CameraCalibrationSetup5 = Convert.ToInt32(sr18.Split(" ")[4]),
        //        CameraCalibrationSetup6 = Convert.ToInt32(sr18.Split(" ")[5]),
        //        CameraCalibrationSetup7 = Convert.ToInt32(sr18.Split(" ")[6]),
        //        CameraCalibrationSetup8 = Convert.ToInt32(sr18.Split(" ")[7])
        //    }
        //    );

        //    //[Alignment Right Mid End]
        //    var sr19 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Right Mid End", "DefaultTaskSetup", ""), " ");
        //    var sr20 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Right Mid End", "SegmentAlignTaskSetup", ""), " ");
        //    var sr21 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Right Mid End", "WaferAlignTaskSetup", ""), " ");
        //    var sr22 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Right Mid End", "ScanCalibTaskSetup", ""), " ");
        //    var sr23 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Right Mid End", "ChuckCornerSetup", ""), " ");
        //    var sr24 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Right Mid End", "CameraCalibrationSetup", ""), " ");
        //    cameras.CameraInfoDic.Add(new CameraInfoPTP
        //    {
        //        CameraName = cfg.ReadConfig("Alignment Right Mid End", "Camera_Name", ""),
        //        RealCameraName = cfg.ReadConfig("Alignment Right Mid End", "'Camera_Name", ""),
        //        TriggerType = cfg.ReadConfig("Alignment Right Mid End", "Trigger_Type", ""),
        //        PaketSize = Convert.ToInt32(cfg.ReadConfig("Alignment Right Mid End", "PacketSize", "")),
        //        PaketDelay = Convert.ToInt32(cfg.ReadConfig("Alignment Right Mid End", "PacketDelay", "")),
        //        DefaultTaskSetup1 = Convert.ToInt32(sr19.Split(" ")[0]),
        //        DefaultTaskSetup2 = Convert.ToInt32(sr19.Split(" ")[1]),
        //        DefaultTaskSetup3 = Convert.ToInt32(sr19.Split(" ")[2]),
        //        DefaultTaskSetup4 = Convert.ToInt32(sr19.Split(" ")[3]),
        //        DefaultTaskSetup5 = Convert.ToInt32(sr19.Split(" ")[4]),
        //        DefaultTaskSetup6 = Convert.ToInt32(sr19.Split(" ")[5]),
        //        DefaultTaskSetup7 = Convert.ToInt32(sr19.Split(" ")[6]),
        //        DefaultTaskSetup8 = Convert.ToInt32(sr19.Split(" ")[7]),
        //        SegmentAlignTaskSetup1 = Convert.ToInt32(sr20.Split(" ")[0]),
        //        SegmentAlignTaskSetup2 = Convert.ToInt32(sr20.Split(" ")[1]),
        //        SegmentAlignTaskSetup3 = Convert.ToInt32(sr20.Split(" ")[2]),
        //        SegmentAlignTaskSetup4 = Convert.ToInt32(sr20.Split(" ")[3]),
        //        SegmentAlignTaskSetup5 = Convert.ToInt32(sr20.Split(" ")[4]),
        //        SegmentAlignTaskSetup6 = Convert.ToInt32(sr20.Split(" ")[5]),
        //        SegmentAlignTaskSetup7 = Convert.ToInt32(sr20.Split(" ")[6]),
        //        SegmentAlignTaskSetup8 = Convert.ToInt32(sr20.Split(" ")[7]),
        //        WaferAlignTaskSetup1 = Convert.ToInt32(sr21.Split(" ")[0]),
        //        WaferAlignTaskSetup2 = Convert.ToInt32(sr21.Split(" ")[1]),
        //        WaferAlignTaskSetup3 = Convert.ToInt32(sr21.Split(" ")[2]),
        //        WaferAlignTaskSetup4 = Convert.ToInt32(sr21.Split(" ")[3]),
        //        WaferAlignTaskSetup5 = Convert.ToInt32(sr21.Split(" ")[4]),
        //        WaferAlignTaskSetup6 = Convert.ToInt32(sr21.Split(" ")[5]),
        //        WaferAlignTaskSetup7 = Convert.ToInt32(sr21.Split(" ")[6]),
        //        WaferAlignTaskSetup8 = Convert.ToInt32(sr21.Split(" ")[7]),
        //        ScanCalibTaskSetup1 = Convert.ToInt32(sr22.Split(" ")[0]),
        //        ScanCalibTaskSetup2 = Convert.ToInt32(sr22.Split(" ")[1]),
        //        ScanCalibTaskSetup3 = Convert.ToInt32(sr22.Split(" ")[2]),
        //        ScanCalibTaskSetup4 = Convert.ToInt32(sr22.Split(" ")[3]),
        //        ScanCalibTaskSetup5 = Convert.ToInt32(sr22.Split(" ")[4]),
        //        ScanCalibTaskSetup6 = Convert.ToInt32(sr22.Split(" ")[5]),
        //        ScanCalibTaskSetup7 = Convert.ToInt32(sr22.Split(" ")[6]),
        //        ScanCalibTaskSetup8 = Convert.ToInt32(sr22.Split(" ")[7]),
        //        ChuckCornerSetup1 = Convert.ToInt32(sr23.Split(" ")[0]),
        //        ChuckCornerSetup2 = Convert.ToInt32(sr23.Split(" ")[1]),
        //        ChuckCornerSetup3 = Convert.ToInt32(sr23.Split(" ")[2]),
        //        ChuckCornerSetup4 = Convert.ToInt32(sr23.Split(" ")[3]),
        //        ChuckCornerSetup5 = Convert.ToInt32(sr23.Split(" ")[4]),
        //        ChuckCornerSetup6 = Convert.ToInt32(sr23.Split(" ")[5]),
        //        ChuckCornerSetup7 = Convert.ToInt32(sr23.Split(" ")[6]),
        //        ChuckCornerSetup8 = Convert.ToInt32(sr23.Split(" ")[7]),
        //        CameraCalibrationSetup1 = Convert.ToInt32(sr24.Split(" ")[0]),
        //        CameraCalibrationSetup2 = Convert.ToInt32(sr24.Split(" ")[1]),
        //        CameraCalibrationSetup3 = Convert.ToInt32(sr24.Split(" ")[2]),
        //        CameraCalibrationSetup4 = Convert.ToInt32(sr24.Split(" ")[3]),
        //        CameraCalibrationSetup5 = Convert.ToInt32(sr24.Split(" ")[4]),
        //        CameraCalibrationSetup6 = Convert.ToInt32(sr24.Split(" ")[5]),
        //        CameraCalibrationSetup7 = Convert.ToInt32(sr24.Split(" ")[6]),
        //        CameraCalibrationSetup8 = Convert.ToInt32(sr24.Split(" ")[7])
        //    }
        //    );


        //    //[Alignment Right End]
        //    var sr25 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Right End", "DefaultTaskSetup", ""), " ");
        //    var sr26 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Right End", "SegmentAlignTaskSetup", ""), " ");
        //    var sr27 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Right End", "WaferAlignTaskSetup", ""), " ");
        //    var sr28 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Right End", "ScanCalibTaskSetup", ""), " ");
        //    var sr29 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Right End", "ChuckCornerSetup", ""), " ");
        //    var sr30 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Right End", "CameraCalibrationSetup", ""), " ");
        //    cameras.CameraInfoDic.Add(new CameraInfoPTP
        //    {
        //        CameraName = cfg.ReadConfig("Alignment Right End", "Camera_Name", ""),
        //        RealCameraName = cfg.ReadConfig("Alignment Right End", "'Camera_Name", ""),
        //        TriggerType = cfg.ReadConfig("Alignment Right End", "Trigger_Type", ""),
        //        PaketSize = Convert.ToInt32(cfg.ReadConfig("Alignment Right End", "PacketSize", "")),
        //        PaketDelay = Convert.ToInt32(cfg.ReadConfig("Alignment Right End", "PacketDelay", "")),
        //        DefaultTaskSetup1 = Convert.ToInt32(sr25.Split(" ")[0]),
        //        DefaultTaskSetup2 = Convert.ToInt32(sr25.Split(" ")[1]),
        //        DefaultTaskSetup3 = Convert.ToInt32(sr25.Split(" ")[2]),
        //        DefaultTaskSetup4 = Convert.ToInt32(sr25.Split(" ")[3]),
        //        DefaultTaskSetup5 = Convert.ToInt32(sr25.Split(" ")[4]),
        //        DefaultTaskSetup6 = Convert.ToInt32(sr25.Split(" ")[5]),
        //        DefaultTaskSetup7 = Convert.ToInt32(sr25.Split(" ")[6]),
        //        DefaultTaskSetup8 = Convert.ToInt32(sr25.Split(" ")[7]),
        //        SegmentAlignTaskSetup1 = Convert.ToInt32(sr26.Split(" ")[0]),
        //        SegmentAlignTaskSetup2 = Convert.ToInt32(sr26.Split(" ")[1]),
        //        SegmentAlignTaskSetup3 = Convert.ToInt32(sr26.Split(" ")[2]),
        //        SegmentAlignTaskSetup4 = Convert.ToInt32(sr26.Split(" ")[3]),
        //        SegmentAlignTaskSetup5 = Convert.ToInt32(sr26.Split(" ")[4]),
        //        SegmentAlignTaskSetup6 = Convert.ToInt32(sr26.Split(" ")[5]),
        //        SegmentAlignTaskSetup7 = Convert.ToInt32(sr26.Split(" ")[6]),
        //        SegmentAlignTaskSetup8 = Convert.ToInt32(sr26.Split(" ")[7]),
        //        WaferAlignTaskSetup1 = Convert.ToInt32(sr27.Split(" ")[0]),
        //        WaferAlignTaskSetup2 = Convert.ToInt32(sr27.Split(" ")[1]),
        //        WaferAlignTaskSetup3 = Convert.ToInt32(sr27.Split(" ")[2]),
        //        WaferAlignTaskSetup4 = Convert.ToInt32(sr27.Split(" ")[3]),
        //        WaferAlignTaskSetup5 = Convert.ToInt32(sr27.Split(" ")[4]),
        //        WaferAlignTaskSetup6 = Convert.ToInt32(sr27.Split(" ")[5]),
        //        WaferAlignTaskSetup7 = Convert.ToInt32(sr27.Split(" ")[6]),
        //        WaferAlignTaskSetup8 = Convert.ToInt32(sr27.Split(" ")[7]),
        //        ScanCalibTaskSetup1 = Convert.ToInt32(sr28.Split(" ")[0]),
        //        ScanCalibTaskSetup2 = Convert.ToInt32(sr28.Split(" ")[1]),
        //        ScanCalibTaskSetup3 = Convert.ToInt32(sr28.Split(" ")[2]),
        //        ScanCalibTaskSetup4 = Convert.ToInt32(sr28.Split(" ")[3]),
        //        ScanCalibTaskSetup5 = Convert.ToInt32(sr28.Split(" ")[4]),
        //        ScanCalibTaskSetup6 = Convert.ToInt32(sr28.Split(" ")[5]),
        //        ScanCalibTaskSetup7 = Convert.ToInt32(sr28.Split(" ")[6]),
        //        ScanCalibTaskSetup8 = Convert.ToInt32(sr28.Split(" ")[7]),
        //        ChuckCornerSetup1 = Convert.ToInt32(sr29.Split(" ")[0]),
        //        ChuckCornerSetup2 = Convert.ToInt32(sr29.Split(" ")[1]),
        //        ChuckCornerSetup3 = Convert.ToInt32(sr29.Split(" ")[2]),
        //        ChuckCornerSetup4 = Convert.ToInt32(sr29.Split(" ")[3]),
        //        ChuckCornerSetup5 = Convert.ToInt32(sr29.Split(" ")[4]),
        //        ChuckCornerSetup6 = Convert.ToInt32(sr29.Split(" ")[5]),
        //        ChuckCornerSetup7 = Convert.ToInt32(sr29.Split(" ")[6]),
        //        ChuckCornerSetup8 = Convert.ToInt32(sr29.Split(" ")[7]),
        //        CameraCalibrationSetup1 = Convert.ToInt32(sr30.Split(" ")[0]),
        //        CameraCalibrationSetup2 = Convert.ToInt32(sr30.Split(" ")[1]),
        //        CameraCalibrationSetup3 = Convert.ToInt32(sr30.Split(" ")[2]),
        //        CameraCalibrationSetup4 = Convert.ToInt32(sr30.Split(" ")[3]),
        //        CameraCalibrationSetup5 = Convert.ToInt32(sr30.Split(" ")[4]),
        //        CameraCalibrationSetup6 = Convert.ToInt32(sr30.Split(" ")[5]),
        //        CameraCalibrationSetup7 = Convert.ToInt32(sr30.Split(" ")[6]),
        //        CameraCalibrationSetup8 = Convert.ToInt32(sr30.Split(" ")[7])
        //    }
        //    );

        //    //[Alignment Left Start]
        //    var sr31 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Left Start", "DefaultTaskSetup", ""), " ");
        //    var sr32 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Left Start", "SegmentAlignTaskSetup", ""), " ");
        //    var sr33 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Left Start", "WaferAlignTaskSetup", ""), " ");
        //    var sr34 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Left Start", "ScanCalibTaskSetup", ""), " ");
        //    var sr35 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Left Start", "ChuckCornerSetup", ""), " ");
        //    var sr36 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Left Start", "CameraCalibrationSetup", ""), " ");
        //    cameras.CameraInfoDic.Add(new CameraInfoPTP
        //    {
        //        CameraName = cfg.ReadConfig("Alignment Left Start", "Camera_Name", ""),
        //        RealCameraName = cfg.ReadConfig("Alignment Left Start", "'Camera_Name", ""),
        //        TriggerType = cfg.ReadConfig("Alignment Left Start", "Trigger_Type", ""),
        //        PaketSize = Convert.ToInt32(cfg.ReadConfig("Alignment Left Start", "PacketSize", "")),
        //        PaketDelay = Convert.ToInt32(cfg.ReadConfig("Alignment Left Start", "PacketDelay", "")),
        //        DefaultTaskSetup1 = Convert.ToInt32(sr31.Split(" ")[0]),
        //        DefaultTaskSetup2 = Convert.ToInt32(sr31.Split(" ")[1]),
        //        DefaultTaskSetup3 = Convert.ToInt32(sr31.Split(" ")[2]),
        //        DefaultTaskSetup4 = Convert.ToInt32(sr31.Split(" ")[3]),
        //        DefaultTaskSetup5 = Convert.ToInt32(sr31.Split(" ")[4]),
        //        DefaultTaskSetup6 = Convert.ToInt32(sr31.Split(" ")[5]),
        //        DefaultTaskSetup7 = Convert.ToInt32(sr31.Split(" ")[6]),
        //        DefaultTaskSetup8 = Convert.ToInt32(sr31.Split(" ")[7]),
        //        SegmentAlignTaskSetup1 = Convert.ToInt32(sr32.Split(" ")[0]),
        //        SegmentAlignTaskSetup2 = Convert.ToInt32(sr32.Split(" ")[1]),
        //        SegmentAlignTaskSetup3 = Convert.ToInt32(sr32.Split(" ")[2]),
        //        SegmentAlignTaskSetup4 = Convert.ToInt32(sr32.Split(" ")[3]),
        //        SegmentAlignTaskSetup5 = Convert.ToInt32(sr32.Split(" ")[4]),
        //        SegmentAlignTaskSetup6 = Convert.ToInt32(sr32.Split(" ")[5]),
        //        SegmentAlignTaskSetup7 = Convert.ToInt32(sr32.Split(" ")[6]),
        //        SegmentAlignTaskSetup8 = Convert.ToInt32(sr32.Split(" ")[7]),
        //        WaferAlignTaskSetup1 = Convert.ToInt32(sr33.Split(" ")[0]),
        //        WaferAlignTaskSetup2 = Convert.ToInt32(sr33.Split(" ")[1]),
        //        WaferAlignTaskSetup3 = Convert.ToInt32(sr33.Split(" ")[2]),
        //        WaferAlignTaskSetup4 = Convert.ToInt32(sr33.Split(" ")[3]),
        //        WaferAlignTaskSetup5 = Convert.ToInt32(sr33.Split(" ")[4]),
        //        WaferAlignTaskSetup6 = Convert.ToInt32(sr33.Split(" ")[5]),
        //        WaferAlignTaskSetup7 = Convert.ToInt32(sr33.Split(" ")[6]),
        //        WaferAlignTaskSetup8 = Convert.ToInt32(sr33.Split(" ")[7]),
        //        ScanCalibTaskSetup1 = Convert.ToInt32(sr34.Split(" ")[0]),
        //        ScanCalibTaskSetup2 = Convert.ToInt32(sr34.Split(" ")[1]),
        //        ScanCalibTaskSetup3 = Convert.ToInt32(sr34.Split(" ")[2]),
        //        ScanCalibTaskSetup4 = Convert.ToInt32(sr34.Split(" ")[3]),
        //        ScanCalibTaskSetup5 = Convert.ToInt32(sr34.Split(" ")[4]),
        //        ScanCalibTaskSetup6 = Convert.ToInt32(sr34.Split(" ")[5]),
        //        ScanCalibTaskSetup7 = Convert.ToInt32(sr34.Split(" ")[6]),
        //        ScanCalibTaskSetup8 = Convert.ToInt32(sr34.Split(" ")[7]),
        //        ChuckCornerSetup1 = Convert.ToInt32(sr35.Split(" ")[0]),
        //        ChuckCornerSetup2 = Convert.ToInt32(sr35.Split(" ")[1]),
        //        ChuckCornerSetup3 = Convert.ToInt32(sr35.Split(" ")[2]),
        //        ChuckCornerSetup4 = Convert.ToInt32(sr35.Split(" ")[3]),
        //        ChuckCornerSetup5 = Convert.ToInt32(sr35.Split(" ")[4]),
        //        ChuckCornerSetup6 = Convert.ToInt32(sr35.Split(" ")[5]),
        //        ChuckCornerSetup7 = Convert.ToInt32(sr35.Split(" ")[6]),
        //        ChuckCornerSetup8 = Convert.ToInt32(sr35.Split(" ")[7]),
        //        CameraCalibrationSetup1 = Convert.ToInt32(sr36.Split(" ")[0]),
        //        CameraCalibrationSetup2 = Convert.ToInt32(sr36.Split(" ")[1]),
        //        CameraCalibrationSetup3 = Convert.ToInt32(sr36.Split(" ")[2]),
        //        CameraCalibrationSetup4 = Convert.ToInt32(sr36.Split(" ")[3]),
        //        CameraCalibrationSetup5 = Convert.ToInt32(sr36.Split(" ")[4]),
        //        CameraCalibrationSetup6 = Convert.ToInt32(sr36.Split(" ")[5]),
        //        CameraCalibrationSetup7 = Convert.ToInt32(sr36.Split(" ")[6]),
        //        CameraCalibrationSetup8 = Convert.ToInt32(sr36.Split(" ")[7])
        //    }
        //    );


        //    //[Alignment Left mid Start]
        //    var sr37 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Left mid Start", "DefaultTaskSetup", ""), " ");
        //    var sr38 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Left mid Start", "SegmentAlignTaskSetup", ""), " ");
        //    var sr39 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Left mid Start", "WaferAlignTaskSetup", ""), " ");
        //    var sr40 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Left mid Start", "ScanCalibTaskSetup", ""), " ");
        //    var sr41 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Left mid Start", "ChuckCornerSetup", ""), " ");
        //    var sr42 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Left mid Start", "CameraCalibrationSetup", ""), " ");
        //    cameras.CameraInfoDic.Add(new CameraInfoPTP
        //    {
        //        CameraName = cfg.ReadConfig("Alignment Left mid Start", "Camera_Name", ""),
        //        RealCameraName = cfg.ReadConfig("Alignment Left mid Start", "'Camera_Name", ""),
        //        TriggerType = cfg.ReadConfig("Alignment Left mid Start", "Trigger_Type", ""),
        //        PaketSize = Convert.ToInt32(cfg.ReadConfig("Alignment Left mid Start", "PacketSize", "")),
        //        PaketDelay = Convert.ToInt32(cfg.ReadConfig("Alignment Left mid Start", "PacketDelay", "")),
        //        DefaultTaskSetup1 = Convert.ToInt32(sr37.Split(" ")[0]),
        //        DefaultTaskSetup2 = Convert.ToInt32(sr37.Split(" ")[1]),
        //        DefaultTaskSetup3 = Convert.ToInt32(sr37.Split(" ")[2]),
        //        DefaultTaskSetup4 = Convert.ToInt32(sr37.Split(" ")[3]),
        //        DefaultTaskSetup5 = Convert.ToInt32(sr37.Split(" ")[4]),
        //        DefaultTaskSetup6 = Convert.ToInt32(sr37.Split(" ")[5]),
        //        DefaultTaskSetup7 = Convert.ToInt32(sr37.Split(" ")[6]),
        //        DefaultTaskSetup8 = Convert.ToInt32(sr37.Split(" ")[7]),
        //        SegmentAlignTaskSetup1 = Convert.ToInt32(sr38.Split(" ")[0]),
        //        SegmentAlignTaskSetup2 = Convert.ToInt32(sr38.Split(" ")[1]),
        //        SegmentAlignTaskSetup3 = Convert.ToInt32(sr38.Split(" ")[2]),
        //        SegmentAlignTaskSetup4 = Convert.ToInt32(sr38.Split(" ")[3]),
        //        SegmentAlignTaskSetup5 = Convert.ToInt32(sr38.Split(" ")[4]),
        //        SegmentAlignTaskSetup6 = Convert.ToInt32(sr38.Split(" ")[5]),
        //        SegmentAlignTaskSetup7 = Convert.ToInt32(sr38.Split(" ")[6]),
        //        SegmentAlignTaskSetup8 = Convert.ToInt32(sr38.Split(" ")[7]),
        //        WaferAlignTaskSetup1 = Convert.ToInt32(sr39.Split(" ")[0]),
        //        WaferAlignTaskSetup2 = Convert.ToInt32(sr39.Split(" ")[1]),
        //        WaferAlignTaskSetup3 = Convert.ToInt32(sr39.Split(" ")[2]),
        //        WaferAlignTaskSetup4 = Convert.ToInt32(sr39.Split(" ")[3]),
        //        WaferAlignTaskSetup5 = Convert.ToInt32(sr39.Split(" ")[4]),
        //        WaferAlignTaskSetup6 = Convert.ToInt32(sr39.Split(" ")[5]),
        //        WaferAlignTaskSetup7 = Convert.ToInt32(sr39.Split(" ")[6]),
        //        WaferAlignTaskSetup8 = Convert.ToInt32(sr39.Split(" ")[7]),
        //        ScanCalibTaskSetup1 = Convert.ToInt32(sr40.Split(" ")[0]),
        //        ScanCalibTaskSetup2 = Convert.ToInt32(sr40.Split(" ")[1]),
        //        ScanCalibTaskSetup3 = Convert.ToInt32(sr40.Split(" ")[2]),
        //        ScanCalibTaskSetup4 = Convert.ToInt32(sr40.Split(" ")[3]),
        //        ScanCalibTaskSetup5 = Convert.ToInt32(sr40.Split(" ")[4]),
        //        ScanCalibTaskSetup6 = Convert.ToInt32(sr40.Split(" ")[5]),
        //        ScanCalibTaskSetup7 = Convert.ToInt32(sr40.Split(" ")[6]),
        //        ScanCalibTaskSetup8 = Convert.ToInt32(sr40.Split(" ")[7]),
        //        ChuckCornerSetup1 = Convert.ToInt32(sr41.Split(" ")[0]),
        //        ChuckCornerSetup2 = Convert.ToInt32(sr41.Split(" ")[1]),
        //        ChuckCornerSetup3 = Convert.ToInt32(sr41.Split(" ")[2]),
        //        ChuckCornerSetup4 = Convert.ToInt32(sr41.Split(" ")[3]),
        //        ChuckCornerSetup5 = Convert.ToInt32(sr41.Split(" ")[4]),
        //        ChuckCornerSetup6 = Convert.ToInt32(sr41.Split(" ")[5]),
        //        ChuckCornerSetup7 = Convert.ToInt32(sr41.Split(" ")[6]),
        //        ChuckCornerSetup8 = Convert.ToInt32(sr41.Split(" ")[7]),
        //        CameraCalibrationSetup1 = Convert.ToInt32(sr42.Split(" ")[0]),
        //        CameraCalibrationSetup2 = Convert.ToInt32(sr42.Split(" ")[1]),
        //        CameraCalibrationSetup3 = Convert.ToInt32(sr42.Split(" ")[2]),
        //        CameraCalibrationSetup4 = Convert.ToInt32(sr42.Split(" ")[3]),
        //        CameraCalibrationSetup5 = Convert.ToInt32(sr42.Split(" ")[4]),
        //        CameraCalibrationSetup6 = Convert.ToInt32(sr42.Split(" ")[5]),
        //        CameraCalibrationSetup7 = Convert.ToInt32(sr42.Split(" ")[6]),
        //        CameraCalibrationSetup8 = Convert.ToInt32(sr42.Split(" ")[7])
        //    }
        //    );



        //    //[Alignment Left Mid End]
        //    var sr43 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Left Mid End", "DefaultTaskSetup", ""), " ");
        //    var sr44 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Left Mid End", "SegmentAlignTaskSetup", ""), " ");
        //    var sr45 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Left Mid End", "WaferAlignTaskSetup", ""), " ");
        //    var sr46 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Left Mid End", "ScanCalibTaskSetup", ""), " ");
        //    var sr47 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Left Mid End", "ChuckCornerSetup", ""), " ");
        //    var sr48 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Left Mid End", "CameraCalibrationSetup", ""), " ");
        //    cameras.CameraInfoDic.Add(new CameraInfoPTP
        //    {
        //        CameraName = cfg.ReadConfig("Alignment Left Mid End", "Camera_Name", ""),
        //        RealCameraName = cfg.ReadConfig("Alignment Left Mid End", "'Camera_Name", ""),
        //        TriggerType = cfg.ReadConfig("Alignment Left Mid End", "Trigger_Type", ""),
        //        PaketSize = Convert.ToInt32(cfg.ReadConfig("Alignment Left Mid End", "PacketSize", "")),
        //        PaketDelay = Convert.ToInt32(cfg.ReadConfig("Alignment Left Mid End", "PacketDelay", "")),
        //        DefaultTaskSetup1 = Convert.ToInt32(sr43.Split(" ")[0]),
        //        DefaultTaskSetup2 = Convert.ToInt32(sr43.Split(" ")[1]),
        //        DefaultTaskSetup3 = Convert.ToInt32(sr43.Split(" ")[2]),
        //        DefaultTaskSetup4 = Convert.ToInt32(sr43.Split(" ")[3]),
        //        DefaultTaskSetup5 = Convert.ToInt32(sr43.Split(" ")[4]),
        //        DefaultTaskSetup6 = Convert.ToInt32(sr43.Split(" ")[5]),
        //        DefaultTaskSetup7 = Convert.ToInt32(sr43.Split(" ")[6]),
        //        DefaultTaskSetup8 = Convert.ToInt32(sr43.Split(" ")[7]),
        //        SegmentAlignTaskSetup1 = Convert.ToInt32(sr44.Split(" ")[0]),
        //        SegmentAlignTaskSetup2 = Convert.ToInt32(sr44.Split(" ")[1]),
        //        SegmentAlignTaskSetup3 = Convert.ToInt32(sr44.Split(" ")[2]),
        //        SegmentAlignTaskSetup4 = Convert.ToInt32(sr44.Split(" ")[3]),
        //        SegmentAlignTaskSetup5 = Convert.ToInt32(sr44.Split(" ")[4]),
        //        SegmentAlignTaskSetup6 = Convert.ToInt32(sr44.Split(" ")[5]),
        //        SegmentAlignTaskSetup7 = Convert.ToInt32(sr44.Split(" ")[6]),
        //        SegmentAlignTaskSetup8 = Convert.ToInt32(sr44.Split(" ")[7]),
        //        WaferAlignTaskSetup1 = Convert.ToInt32(sr45.Split(" ")[0]),
        //        WaferAlignTaskSetup2 = Convert.ToInt32(sr45.Split(" ")[1]),
        //        WaferAlignTaskSetup3 = Convert.ToInt32(sr45.Split(" ")[2]),
        //        WaferAlignTaskSetup4 = Convert.ToInt32(sr45.Split(" ")[3]),
        //        WaferAlignTaskSetup5 = Convert.ToInt32(sr45.Split(" ")[4]),
        //        WaferAlignTaskSetup6 = Convert.ToInt32(sr45.Split(" ")[5]),
        //        WaferAlignTaskSetup7 = Convert.ToInt32(sr45.Split(" ")[6]),
        //        WaferAlignTaskSetup8 = Convert.ToInt32(sr45.Split(" ")[7]),
        //        ScanCalibTaskSetup1 = Convert.ToInt32(sr46.Split(" ")[0]),
        //        ScanCalibTaskSetup2 = Convert.ToInt32(sr46.Split(" ")[1]),
        //        ScanCalibTaskSetup3 = Convert.ToInt32(sr46.Split(" ")[2]),
        //        ScanCalibTaskSetup4 = Convert.ToInt32(sr46.Split(" ")[3]),
        //        ScanCalibTaskSetup5 = Convert.ToInt32(sr46.Split(" ")[4]),
        //        ScanCalibTaskSetup6 = Convert.ToInt32(sr46.Split(" ")[5]),
        //        ScanCalibTaskSetup7 = Convert.ToInt32(sr46.Split(" ")[6]),
        //        ScanCalibTaskSetup8 = Convert.ToInt32(sr46.Split(" ")[7]),
        //        ChuckCornerSetup1 = Convert.ToInt32(sr47.Split(" ")[0]),
        //        ChuckCornerSetup2 = Convert.ToInt32(sr47.Split(" ")[1]),
        //        ChuckCornerSetup3 = Convert.ToInt32(sr47.Split(" ")[2]),
        //        ChuckCornerSetup4 = Convert.ToInt32(sr47.Split(" ")[3]),
        //        ChuckCornerSetup5 = Convert.ToInt32(sr47.Split(" ")[4]),
        //        ChuckCornerSetup6 = Convert.ToInt32(sr47.Split(" ")[5]),
        //        ChuckCornerSetup7 = Convert.ToInt32(sr47.Split(" ")[6]),
        //        ChuckCornerSetup8 = Convert.ToInt32(sr47.Split(" ")[7]),
        //        CameraCalibrationSetup1 = Convert.ToInt32(sr48.Split(" ")[0]),
        //        CameraCalibrationSetup2 = Convert.ToInt32(sr48.Split(" ")[1]),
        //        CameraCalibrationSetup3 = Convert.ToInt32(sr48.Split(" ")[2]),
        //        CameraCalibrationSetup4 = Convert.ToInt32(sr48.Split(" ")[3]),
        //        CameraCalibrationSetup5 = Convert.ToInt32(sr48.Split(" ")[4]),
        //        CameraCalibrationSetup6 = Convert.ToInt32(sr48.Split(" ")[5]),
        //        CameraCalibrationSetup7 = Convert.ToInt32(sr48.Split(" ")[6]),
        //        CameraCalibrationSetup8 = Convert.ToInt32(sr48.Split(" ")[7])
        //    }
        //    );


        //    //[Alignment Left End]
        //    var sr49 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Left End", "DefaultTaskSetup", ""), " ");
        //    var sr50 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Left End", "SegmentAlignTaskSetup", ""), " ");
        //    var sr51 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Left End", "WaferAlignTaskSetup", ""), " ");
        //    var sr52 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Left End", "ScanCalibTaskSetup", ""), " ");
        //    var sr53 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Left End", "ChuckCornerSetup", ""), " ");
        //    var sr54 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Alignment Left End", "CameraCalibrationSetup", ""), " ");
        //    cameras.CameraInfoDic.Add(new CameraInfoPTP
        //    {
        //        CameraName = cfg.ReadConfig("Alignment Left End", "Camera_Name", ""),
        //        RealCameraName = cfg.ReadConfig("Alignment Left End", "'Camera_Name", ""),
        //        TriggerType = cfg.ReadConfig("Alignment Left End", "Trigger_Type", ""),
        //        PaketSize = Convert.ToInt32(cfg.ReadConfig("Alignment Left End", "PacketSize", "")),
        //        PaketDelay = Convert.ToInt32(cfg.ReadConfig("Alignment Left End", "PacketDelay", "")),
        //        DefaultTaskSetup1 = Convert.ToInt32(sr49.Split(" ")[0]),
        //        DefaultTaskSetup2 = Convert.ToInt32(sr49.Split(" ")[1]),
        //        DefaultTaskSetup3 = Convert.ToInt32(sr49.Split(" ")[2]),
        //        DefaultTaskSetup4 = Convert.ToInt32(sr49.Split(" ")[3]),
        //        DefaultTaskSetup5 = Convert.ToInt32(sr49.Split(" ")[4]),
        //        DefaultTaskSetup6 = Convert.ToInt32(sr49.Split(" ")[5]),
        //        DefaultTaskSetup7 = Convert.ToInt32(sr49.Split(" ")[6]),
        //        DefaultTaskSetup8 = Convert.ToInt32(sr49.Split(" ")[7]),
        //        SegmentAlignTaskSetup1 = Convert.ToInt32(sr50.Split(" ")[0]),
        //        SegmentAlignTaskSetup2 = Convert.ToInt32(sr50.Split(" ")[1]),
        //        SegmentAlignTaskSetup3 = Convert.ToInt32(sr50.Split(" ")[2]),
        //        SegmentAlignTaskSetup4 = Convert.ToInt32(sr50.Split(" ")[3]),
        //        SegmentAlignTaskSetup5 = Convert.ToInt32(sr50.Split(" ")[4]),
        //        SegmentAlignTaskSetup6 = Convert.ToInt32(sr50.Split(" ")[5]),
        //        SegmentAlignTaskSetup7 = Convert.ToInt32(sr50.Split(" ")[6]),
        //        SegmentAlignTaskSetup8 = Convert.ToInt32(sr50.Split(" ")[7]),
        //        WaferAlignTaskSetup1 = Convert.ToInt32(sr51.Split(" ")[0]),
        //        WaferAlignTaskSetup2 = Convert.ToInt32(sr51.Split(" ")[1]),
        //        WaferAlignTaskSetup3 = Convert.ToInt32(sr51.Split(" ")[2]),
        //        WaferAlignTaskSetup4 = Convert.ToInt32(sr51.Split(" ")[3]),
        //        WaferAlignTaskSetup5 = Convert.ToInt32(sr51.Split(" ")[4]),
        //        WaferAlignTaskSetup6 = Convert.ToInt32(sr51.Split(" ")[5]),
        //        WaferAlignTaskSetup7 = Convert.ToInt32(sr51.Split(" ")[6]),
        //        WaferAlignTaskSetup8 = Convert.ToInt32(sr51.Split(" ")[7]),
        //        ScanCalibTaskSetup1 = Convert.ToInt32(sr52.Split(" ")[0]),
        //        ScanCalibTaskSetup2 = Convert.ToInt32(sr52.Split(" ")[1]),
        //        ScanCalibTaskSetup3 = Convert.ToInt32(sr52.Split(" ")[2]),
        //        ScanCalibTaskSetup4 = Convert.ToInt32(sr52.Split(" ")[3]),
        //        ScanCalibTaskSetup5 = Convert.ToInt32(sr52.Split(" ")[4]),
        //        ScanCalibTaskSetup6 = Convert.ToInt32(sr52.Split(" ")[5]),
        //        ScanCalibTaskSetup7 = Convert.ToInt32(sr52.Split(" ")[6]),
        //        ScanCalibTaskSetup8 = Convert.ToInt32(sr52.Split(" ")[7]),
        //        ChuckCornerSetup1 = Convert.ToInt32(sr53.Split(" ")[0]),
        //        ChuckCornerSetup2 = Convert.ToInt32(sr53.Split(" ")[1]),
        //        ChuckCornerSetup3 = Convert.ToInt32(sr53.Split(" ")[2]),
        //        ChuckCornerSetup4 = Convert.ToInt32(sr53.Split(" ")[3]),
        //        ChuckCornerSetup5 = Convert.ToInt32(sr53.Split(" ")[4]),
        //        ChuckCornerSetup6 = Convert.ToInt32(sr53.Split(" ")[5]),
        //        ChuckCornerSetup7 = Convert.ToInt32(sr53.Split(" ")[6]),
        //        ChuckCornerSetup8 = Convert.ToInt32(sr53.Split(" ")[7]),
        //        CameraCalibrationSetup1 = Convert.ToInt32(sr54.Split(" ")[0]),
        //        CameraCalibrationSetup2 = Convert.ToInt32(sr54.Split(" ")[1]),
        //        CameraCalibrationSetup3 = Convert.ToInt32(sr54.Split(" ")[2]),
        //        CameraCalibrationSetup4 = Convert.ToInt32(sr54.Split(" ")[3]),
        //        CameraCalibrationSetup5 = Convert.ToInt32(sr54.Split(" ")[4]),
        //        CameraCalibrationSetup6 = Convert.ToInt32(sr54.Split(" ")[5]),
        //        CameraCalibrationSetup7 = Convert.ToInt32(sr54.Split(" ")[6]),
        //        CameraCalibrationSetup8 = Convert.ToInt32(sr54.Split(" ")[7])
        //    }
        //    );


        //    //[Print Quality1]
        //    var sr55 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Print Quality1", "DefaultTaskSetup", ""), " ");
        //    var sr56 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Print Quality1", "CameraCalibrationSetup", ""), " ");
        //    cameras.CameraInfoDic.Add(new CameraInfoPTP
        //    {
        //        CameraName = cfg.ReadConfig("Print Quality1", "Camera_Name", ""),
        //        RealCameraName = cfg.ReadConfig("Print Quality1", "'Camera_Name", ""),
        //        TriggerType = cfg.ReadConfig("Print Quality1", "Trigger_Type", ""),
        //        PaketSize = Convert.ToInt32(cfg.ReadConfig("Print Quality1", "PacketSize", "")),
        //        PaketDelay = Convert.ToInt32(cfg.ReadConfig("Print Quality1", "PacketDelay", "")),
        //        DefaultTaskSetup1 = Convert.ToInt32(sr55.Split(" ")[0]),
        //        DefaultTaskSetup2 = Convert.ToInt32(sr55.Split(" ")[1]),
        //        DefaultTaskSetup3 = Convert.ToInt32(sr55.Split(" ")[2]),
        //        DefaultTaskSetup4 = Convert.ToInt32(sr55.Split(" ")[3]),
        //        DefaultTaskSetup5 = Convert.ToInt32(sr55.Split(" ")[4]),
        //        DefaultTaskSetup6 = Convert.ToInt32(sr55.Split(" ")[5]),
        //        DefaultTaskSetup7 = Convert.ToInt32(sr55.Split(" ")[6]),
        //        DefaultTaskSetup8 = Convert.ToInt32(sr55.Split(" ")[7]),
               
        //        CameraCalibrationSetup1 = Convert.ToInt32(sr56.Split(" ")[0]),
        //        CameraCalibrationSetup2 = Convert.ToInt32(sr56.Split(" ")[1]),
        //        CameraCalibrationSetup3 = Convert.ToInt32(sr56.Split(" ")[2]),
        //        CameraCalibrationSetup4 = Convert.ToInt32(sr56.Split(" ")[3]),
        //        CameraCalibrationSetup5 = Convert.ToInt32(sr56.Split(" ")[4]),
        //        CameraCalibrationSetup6 = Convert.ToInt32(sr56.Split(" ")[5]),
        //        CameraCalibrationSetup7 = Convert.ToInt32(sr56.Split(" ")[6]),
        //        CameraCalibrationSetup8 = Convert.ToInt32(sr56.Split(" ")[7])
        //    }
        //    );

        //    //[Print Quality2]
        //    var sr57 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Print Quality2", "DefaultTaskSetup", ""), " ");
        //    var sr58 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Print Quality2", "CameraCalibrationSetup", ""), " ");
        //    cameras.CameraInfoDic.Add(new CameraInfoPTP
        //    {
        //        CameraName = cfg.ReadConfig("Print Quality2", "Camera_Name", ""),
        //        RealCameraName = cfg.ReadConfig("Print Quality2", "'Camera_Name", ""),
        //        TriggerType = cfg.ReadConfig("Print Quality2", "Trigger_Type", ""),
        //        PaketSize = Convert.ToInt32(cfg.ReadConfig("Print Quality2", "PacketSize", "")),
        //        PaketDelay = Convert.ToInt32(cfg.ReadConfig("Print Quality2", "PacketDelay", "")),
        //        DefaultTaskSetup1 = Convert.ToInt32(sr57.Split(" ")[0]),
        //        DefaultTaskSetup2 = Convert.ToInt32(sr57.Split(" ")[1]),
        //        DefaultTaskSetup3 = Convert.ToInt32(sr57.Split(" ")[2]),
        //        DefaultTaskSetup4 = Convert.ToInt32(sr57.Split(" ")[3]),
        //        DefaultTaskSetup5 = Convert.ToInt32(sr57.Split(" ")[4]),
        //        DefaultTaskSetup6 = Convert.ToInt32(sr57.Split(" ")[5]),
        //        DefaultTaskSetup7 = Convert.ToInt32(sr57.Split(" ")[6]),
        //        DefaultTaskSetup8 = Convert.ToInt32(sr57.Split(" ")[7]),

        //        CameraCalibrationSetup1 = Convert.ToInt32(sr58.Split(" ")[0]),
        //        CameraCalibrationSetup2 = Convert.ToInt32(sr58.Split(" ")[1]),
        //        CameraCalibrationSetup3 = Convert.ToInt32(sr58.Split(" ")[2]),
        //        CameraCalibrationSetup4 = Convert.ToInt32(sr58.Split(" ")[3]),
        //        CameraCalibrationSetup5 = Convert.ToInt32(sr58.Split(" ")[4]),
        //        CameraCalibrationSetup6 = Convert.ToInt32(sr58.Split(" ")[5]),
        //        CameraCalibrationSetup7 = Convert.ToInt32(sr58.Split(" ")[6]),
        //        CameraCalibrationSetup8 = Convert.ToInt32(sr58.Split(" ")[7])
        //    }
        //    );

        //    //[Print Quality2]
        //    var sr59 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Print Quality2", "DefaultTaskSetup", ""), " ");
        //    var sr60 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Print Quality2", "CameraCalibrationSetup", ""), " ");
        //    cameras.CameraInfoDic.Add(new CameraInfoPTP
        //    {
        //        CameraName = cfg.ReadConfig("Print Quality2", "Camera_Name", ""),
        //        RealCameraName = cfg.ReadConfig("Print Quality2", "'Camera_Name", ""),
        //        TriggerType = cfg.ReadConfig("Print Quality2", "Trigger_Type", ""),
        //        PaketSize = Convert.ToInt32(cfg.ReadConfig("Print Quality2", "PacketSize", "")),
        //        PaketDelay = Convert.ToInt32(cfg.ReadConfig("Print Quality2", "PacketDelay", "")),
        //        DefaultTaskSetup1 = Convert.ToInt32(sr59.Split(" ")[0]),
        //        DefaultTaskSetup2 = Convert.ToInt32(sr59.Split(" ")[1]),
        //        DefaultTaskSetup3 = Convert.ToInt32(sr59.Split(" ")[2]),
        //        DefaultTaskSetup4 = Convert.ToInt32(sr59.Split(" ")[3]),
        //        DefaultTaskSetup5 = Convert.ToInt32(sr59.Split(" ")[4]),
        //        DefaultTaskSetup6 = Convert.ToInt32(sr59.Split(" ")[5]),
        //        DefaultTaskSetup7 = Convert.ToInt32(sr59.Split(" ")[6]),
        //        DefaultTaskSetup8 = Convert.ToInt32(sr59.Split(" ")[7]),

        //        CameraCalibrationSetup1 = Convert.ToInt32(sr60.Split(" ")[0]),
        //        CameraCalibrationSetup2 = Convert.ToInt32(sr60.Split(" ")[1]),
        //        CameraCalibrationSetup3 = Convert.ToInt32(sr60.Split(" ")[2]),
        //        CameraCalibrationSetup4 = Convert.ToInt32(sr60.Split(" ")[3]),
        //        CameraCalibrationSetup5 = Convert.ToInt32(sr60.Split(" ")[4]),
        //        CameraCalibrationSetup6 = Convert.ToInt32(sr60.Split(" ")[5]),
        //        CameraCalibrationSetup7 = Convert.ToInt32(sr60.Split(" ")[6]),
        //        CameraCalibrationSetup8 = Convert.ToInt32(sr60.Split(" ")[7])
        //    }
        //    );


        //    //[Fill & Clean]
        //    var sr61 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Fill & Clean", "DefaultTaskSetup", ""), " ");
        //    var sr62 = new Regex("[\\s]+").Replace(cfg.ReadConfig("Fill & Clean", "CameraCalibrationSetup", ""), " ");
        //    cameras.CameraInfoDic.Add(new CameraInfoPTP
        //    {
        //        CameraName = cfg.ReadConfig("Fill & Clean", "Camera_Name", ""),
        //        RealCameraName = cfg.ReadConfig("Fill & Clean", "'Camera_Name", ""),
        //        TriggerType = cfg.ReadConfig("Fill & Clean", "Trigger_Type", ""),
        //        PaketSize = Convert.ToInt32(cfg.ReadConfig("Fill & Clean", "PacketSize", "")),
        //        PaketDelay = Convert.ToInt32(cfg.ReadConfig("Fill & Clean", "PacketDelay", "")),
        //        DefaultTaskSetup1 = Convert.ToInt32(sr61.Split(" ")[0]),
        //        DefaultTaskSetup2 = Convert.ToInt32(sr61.Split(" ")[1]),
        //        DefaultTaskSetup3 = Convert.ToInt32(sr61.Split(" ")[2]),
        //        DefaultTaskSetup4 = Convert.ToInt32(sr61.Split(" ")[3]),
        //        DefaultTaskSetup5 = Convert.ToInt32(sr61.Split(" ")[4]),
        //        DefaultTaskSetup6 = Convert.ToInt32(sr61.Split(" ")[5]),
        //        DefaultTaskSetup7 = Convert.ToInt32(sr61.Split(" ")[6]),
        //        DefaultTaskSetup8 = Convert.ToInt32(sr61.Split(" ")[7]),

        //        CameraCalibrationSetup1 = Convert.ToInt32(sr62.Split(" ")[0]),
        //        CameraCalibrationSetup2 = Convert.ToInt32(sr62.Split(" ")[1]),
        //        CameraCalibrationSetup3 = Convert.ToInt32(sr62.Split(" ")[2]),
        //        CameraCalibrationSetup4 = Convert.ToInt32(sr62.Split(" ")[3]),
        //        CameraCalibrationSetup5 = Convert.ToInt32(sr62.Split(" ")[4]),
        //        CameraCalibrationSetup6 = Convert.ToInt32(sr62.Split(" ")[5]),
        //        CameraCalibrationSetup7 = Convert.ToInt32(sr62.Split(" ")[6]),
        //        CameraCalibrationSetup8 = Convert.ToInt32(sr62.Split(" ")[7])
        //    }
        //    );


        //    //[UPAL HR1 Left Start]
        //    var sr63 = new Regex("[\\s]+").Replace(cfg.ReadConfig("UPAL HR1 Left Start", "DefaultTaskSetup", ""), " ");
        //    var sr64 = new Regex("[\\s]+").Replace(cfg.ReadConfig("UPAL HR1 Left Start", "ChuckCornerSetup", ""), " ");
        //    var sr65 = new Regex("[\\s]+").Replace(cfg.ReadConfig("UPAL HR1 Left Start", "CameraCalibrationSetup", ""), " ");
        //    cameras.CameraInfoDic.Add(new CameraInfoPTP
        //    {
        //        CameraName = cfg.ReadConfig("UPAL HR1 Left Start", "Camera_Name", ""),
        //        RealCameraName = cfg.ReadConfig("UPAL HR1 Left Start", "'Camera_Name", ""),
        //        TriggerType = cfg.ReadConfig("UPAL HR1 Left Start", "Trigger_Type", ""),
        //        PaketSize1 = Convert.ToInt32(cfg.ReadConfig("UPAL HR1 Left Start", "'PacketSize", "")),
        //        PaketDelay1 = Convert.ToInt32(cfg.ReadConfig("UPAL HR1 Left Start", "'PacketDelay", "")),
        //        PaketSize = Convert.ToInt32(cfg.ReadConfig("UPAL HR1 Left Start", "PacketSize", "")),
        //        PaketDelay = Convert.ToInt32(cfg.ReadConfig("UPAL HR1 Left Start", "PacketDelay", "")),
        //        DefaultTaskSetup1 = Convert.ToInt32(sr63.Split(" ")[0]),
        //        DefaultTaskSetup2 = Convert.ToInt32(sr63.Split(" ")[1]),
        //        DefaultTaskSetup3 = Convert.ToInt32(sr63.Split(" ")[2]),
        //        DefaultTaskSetup4 = Convert.ToInt32(sr63.Split(" ")[3]),
        //        DefaultTaskSetup5 = Convert.ToInt32(sr63.Split(" ")[4]),
        //        DefaultTaskSetup6 = Convert.ToInt32(sr63.Split(" ")[5]),
        //        DefaultTaskSetup7 = Convert.ToInt32(sr63.Split(" ")[6]),
        //        DefaultTaskSetup8 = Convert.ToInt32(sr63.Split(" ")[7]),

        //        ChuckCornerSetup1 = Convert.ToInt32(sr64.Split(" ")[0]),
        //        ChuckCornerSetup2 = Convert.ToInt32(sr64.Split(" ")[1]),
        //        ChuckCornerSetup3 = Convert.ToInt32(sr64.Split(" ")[2]),
        //        ChuckCornerSetup4 = Convert.ToInt32(sr64.Split(" ")[3]),
        //        ChuckCornerSetup5 = Convert.ToInt32(sr64.Split(" ")[4]),
        //        ChuckCornerSetup6 = Convert.ToInt32(sr64.Split(" ")[5]),
        //        ChuckCornerSetup7 = Convert.ToInt32(sr64.Split(" ")[6]),
        //        ChuckCornerSetup8 = Convert.ToInt32(sr64.Split(" ")[7]),

        //        CameraCalibrationSetup1 = Convert.ToInt32(sr65.Split(" ")[0]),
        //        CameraCalibrationSetup2 = Convert.ToInt32(sr65.Split(" ")[1]),
        //        CameraCalibrationSetup3 = Convert.ToInt32(sr65.Split(" ")[2]),
        //        CameraCalibrationSetup4 = Convert.ToInt32(sr65.Split(" ")[3]),
        //        CameraCalibrationSetup5 = Convert.ToInt32(sr65.Split(" ")[4]),
        //        CameraCalibrationSetup6 = Convert.ToInt32(sr65.Split(" ")[5]),
        //        CameraCalibrationSetup7 = Convert.ToInt32(sr65.Split(" ")[6]),
        //        CameraCalibrationSetup8 = Convert.ToInt32(sr65.Split(" ")[7])
        //    }
        //    );


        //    //[UPAL HR1 Right Start]
        //    var sr66 = new Regex("[\\s]+").Replace(cfg.ReadConfig("UPAL HR1 Right Start", "DefaultTaskSetup", ""), " ");
        //    var sr67 = new Regex("[\\s]+").Replace(cfg.ReadConfig("UPAL HR1 Right Start", "ChuckCornerSetup", ""), " ");
        //    var sr68 = new Regex("[\\s]+").Replace(cfg.ReadConfig("UPAL HR1 Right Start", "CameraCalibrationSetup", ""), " ");
        //    cameras.CameraInfoDic.Add(new CameraInfoPTP
        //    {
        //        CameraName = cfg.ReadConfig("UPAL HR1 Right Start", "Camera_Name", ""),
        //        RealCameraName = cfg.ReadConfig("UPAL HR1 Right Start", "'Camera_Name", ""),
        //        TriggerType = cfg.ReadConfig("UPAL HR1 Right Start", "Trigger_Type", ""),
        //        PaketSize1 = Convert.ToInt32(cfg.ReadConfig("UPAL HR1 Right Start", "'PacketSize", "")),
        //        PaketDelay1 = Convert.ToInt32(cfg.ReadConfig("UPAL HR1 Right Start", "'PacketDelay", "")),
        //        PaketSize = Convert.ToInt32(cfg.ReadConfig("UPAL HR1 Right Start", "PacketSize", "")),
        //        PaketDelay = Convert.ToInt32(cfg.ReadConfig("UPAL HR1 Right Start", "PacketDelay", "")),
        //        DefaultTaskSetup1 = Convert.ToInt32(sr66.Split(" ")[0]),
        //        DefaultTaskSetup2 = Convert.ToInt32(sr66.Split(" ")[1]),
        //        DefaultTaskSetup3 = Convert.ToInt32(sr66.Split(" ")[2]),
        //        DefaultTaskSetup4 = Convert.ToInt32(sr66.Split(" ")[3]),
        //        DefaultTaskSetup5 = Convert.ToInt32(sr66.Split(" ")[4]),
        //        DefaultTaskSetup6 = Convert.ToInt32(sr66.Split(" ")[5]),
        //        DefaultTaskSetup7 = Convert.ToInt32(sr66.Split(" ")[6]),
        //        DefaultTaskSetup8 = Convert.ToInt32(sr66.Split(" ")[7]),

        //        ChuckCornerSetup1 = Convert.ToInt32(sr67.Split(" ")[0]),
        //        ChuckCornerSetup2 = Convert.ToInt32(sr67.Split(" ")[1]),
        //        ChuckCornerSetup3 = Convert.ToInt32(sr67.Split(" ")[2]),
        //        ChuckCornerSetup4 = Convert.ToInt32(sr67.Split(" ")[3]),
        //        ChuckCornerSetup5 = Convert.ToInt32(sr67.Split(" ")[4]),
        //        ChuckCornerSetup6 = Convert.ToInt32(sr67.Split(" ")[5]),
        //        ChuckCornerSetup7 = Convert.ToInt32(sr67.Split(" ")[6]),
        //        ChuckCornerSetup8 = Convert.ToInt32(sr67.Split(" ")[7]),

        //        CameraCalibrationSetup1 = Convert.ToInt32(sr68.Split(" ")[0]),
        //        CameraCalibrationSetup2 = Convert.ToInt32(sr68.Split(" ")[1]),
        //        CameraCalibrationSetup3 = Convert.ToInt32(sr68.Split(" ")[2]),
        //        CameraCalibrationSetup4 = Convert.ToInt32(sr68.Split(" ")[3]),
        //        CameraCalibrationSetup5 = Convert.ToInt32(sr68.Split(" ")[4]),
        //        CameraCalibrationSetup6 = Convert.ToInt32(sr68.Split(" ")[5]),
        //        CameraCalibrationSetup7 = Convert.ToInt32(sr68.Split(" ")[6]),
        //        CameraCalibrationSetup8 = Convert.ToInt32(sr68.Split(" ")[7])
        //    }
        //    );

        //    //[UPAL HR1 Right End]
        //    var sr69 = new Regex("[\\s]+").Replace(cfg.ReadConfig("UPAL HR1 Right End", "DefaultTaskSetup", ""), " ");
        //    var sr70 = new Regex("[\\s]+").Replace(cfg.ReadConfig("UPAL HR1 Right End", "ChuckCornerSetup", ""), " ");
        //    var sr71 = new Regex("[\\s]+").Replace(cfg.ReadConfig("UPAL HR1 Right End", "CameraCalibrationSetup", ""), " ");
        //    cameras.CameraInfoDic.Add(new CameraInfoPTP
        //    {
        //        CameraName = cfg.ReadConfig("UPAL HR1 Right End", "Camera_Name", ""),
        //        RealCameraName = cfg.ReadConfig("UPAL HR1 Right End", "'Camera_Name", ""),
        //        TriggerType = cfg.ReadConfig("UPAL HR1 Right End", "Trigger_Type", ""),
        //        PaketSize = Convert.ToInt32(cfg.ReadConfig("UPAL HR1 Right End", "PacketSize", "")),
        //        PaketDelay = Convert.ToInt32(cfg.ReadConfig("UPAL HR1 Right End", "PacketDelay", "")),
        //        PaketSize1 = Convert.ToInt32(cfg.ReadConfig("UPAL HR1 Right End", "'PacketSize", "")),
        //        PaketDelay1 = Convert.ToInt32(cfg.ReadConfig("UPAL HR1 Right End", "'PacketDelay", "")),
        //        DefaultTaskSetup1 = Convert.ToInt32(sr69.Split(" ")[0]),
        //        DefaultTaskSetup2 = Convert.ToInt32(sr69.Split(" ")[1]),
        //        DefaultTaskSetup3 = Convert.ToInt32(sr69.Split(" ")[2]),
        //        DefaultTaskSetup4 = Convert.ToInt32(sr69.Split(" ")[3]),
        //        DefaultTaskSetup5 = Convert.ToInt32(sr69.Split(" ")[4]),
        //        DefaultTaskSetup6 = Convert.ToInt32(sr69.Split(" ")[5]),
        //        DefaultTaskSetup7 = Convert.ToInt32(sr69.Split(" ")[6]),
        //        DefaultTaskSetup8 = Convert.ToInt32(sr69.Split(" ")[7]),

        //        ChuckCornerSetup1 = Convert.ToInt32(sr70.Split(" ")[0]),
        //        ChuckCornerSetup2 = Convert.ToInt32(sr70.Split(" ")[1]),
        //        ChuckCornerSetup3 = Convert.ToInt32(sr70.Split(" ")[2]),
        //        ChuckCornerSetup4 = Convert.ToInt32(sr70.Split(" ")[3]),
        //        ChuckCornerSetup5 = Convert.ToInt32(sr70.Split(" ")[4]),
        //        ChuckCornerSetup6 = Convert.ToInt32(sr70.Split(" ")[5]),
        //        ChuckCornerSetup7 = Convert.ToInt32(sr70.Split(" ")[6]),
        //        ChuckCornerSetup8 = Convert.ToInt32(sr70.Split(" ")[7]),

        //        CameraCalibrationSetup1 = Convert.ToInt32(sr71.Split(" ")[0]),
        //        CameraCalibrationSetup2 = Convert.ToInt32(sr71.Split(" ")[1]),
        //        CameraCalibrationSetup3 = Convert.ToInt32(sr71.Split(" ")[2]),
        //        CameraCalibrationSetup4 = Convert.ToInt32(sr71.Split(" ")[3]),
        //        CameraCalibrationSetup5 = Convert.ToInt32(sr71.Split(" ")[4]),
        //        CameraCalibrationSetup6 = Convert.ToInt32(sr71.Split(" ")[5]),
        //        CameraCalibrationSetup7 = Convert.ToInt32(sr71.Split(" ")[6]),
        //        CameraCalibrationSetup8 = Convert.ToInt32(sr71.Split(" ")[7])
        //    }
        //    );


        //    //[UPAL HR2 Left Start]
        //    var sr72 = new Regex("[\\s]+").Replace(cfg.ReadConfig("UPAL HR2 Left Start", "DefaultTaskSetup", ""), " ");
        //    var sr73 = new Regex("[\\s]+").Replace(cfg.ReadConfig("UPAL HR2 Left Start", "ChuckCornerSetup", ""), " ");
        //    var sr74 = new Regex("[\\s]+").Replace(cfg.ReadConfig("UPAL HR2 Left Start", "CameraCalibrationSetup", ""), " ");
        //    cameras.CameraInfoDic.Add(new CameraInfoPTP
        //    {
        //        CameraName = cfg.ReadConfig("UPAL HR2 Left Start", "Camera_Name", ""),
        //        RealCameraName = cfg.ReadConfig("UPAL HR2 Left Start", "'Camera_Name", ""),
        //        TriggerType = cfg.ReadConfig("UPAL HR2 Left Start","Trigger_Type", ""),
        //        PaketSize1 = Convert.ToInt32(cfg.ReadConfig("UPAL HR2 Left Start", "'PacketSize", "")),
        //        PaketDelay1 = Convert.ToInt32(cfg.ReadConfig("UPAL HR2 Left Start", "'PacketDelay", "")),
        //        PaketSize = Convert.ToInt32(cfg.ReadConfig("UPAL HR2 Left Start", "PacketSize", "")),
        //        PaketDelay = Convert.ToInt32(cfg.ReadConfig("UPAL HR2 Left Start", "PacketDelay", "")),
        //        DefaultTaskSetup1 = Convert.ToInt32(sr72.Split(" ")[0]),
        //        DefaultTaskSetup2 = Convert.ToInt32(sr72.Split(" ")[1]),
        //        DefaultTaskSetup3 = Convert.ToInt32(sr72.Split(" ")[2]),
        //        DefaultTaskSetup4 = Convert.ToInt32(sr72.Split(" ")[3]),
        //        DefaultTaskSetup5 = Convert.ToInt32(sr72.Split(" ")[4]),
        //        DefaultTaskSetup6 = Convert.ToInt32(sr72.Split(" ")[5]),
        //        DefaultTaskSetup7 = Convert.ToInt32(sr72.Split(" ")[6]),
        //        DefaultTaskSetup8 = Convert.ToInt32(sr72.Split(" ")[7]),

        //        ChuckCornerSetup1 = Convert.ToInt32(sr73.Split(" ")[0]),
        //        ChuckCornerSetup2 = Convert.ToInt32(sr73.Split(" ")[1]),
        //        ChuckCornerSetup3 = Convert.ToInt32(sr73.Split(" ")[2]),
        //        ChuckCornerSetup4 = Convert.ToInt32(sr73.Split(" ")[3]),
        //        ChuckCornerSetup5 = Convert.ToInt32(sr73.Split(" ")[4]),
        //        ChuckCornerSetup6 = Convert.ToInt32(sr73.Split(" ")[5]),
        //        ChuckCornerSetup7 = Convert.ToInt32(sr73.Split(" ")[6]),
        //        ChuckCornerSetup8 = Convert.ToInt32(sr73.Split(" ")[7]),

        //        CameraCalibrationSetup1 = Convert.ToInt32(sr74.Split(" ")[0]),
        //        CameraCalibrationSetup2 = Convert.ToInt32(sr74.Split(" ")[1]),
        //        CameraCalibrationSetup3 = Convert.ToInt32(sr74.Split(" ")[2]),
        //        CameraCalibrationSetup4 = Convert.ToInt32(sr74.Split(" ")[3]),
        //        CameraCalibrationSetup5 = Convert.ToInt32(sr74.Split(" ")[4]),
        //        CameraCalibrationSetup6 = Convert.ToInt32(sr74.Split(" ")[5]),
        //        CameraCalibrationSetup7 = Convert.ToInt32(sr74.Split(" ")[6]),
        //        CameraCalibrationSetup8 = Convert.ToInt32(sr74.Split(" ")[7])
        //    }
        //    );


        //    //[UPAL HR1 Right Start]
        //    var sr75 = new Regex("[\\s]+").Replace(cfg.ReadConfig("UPAL HR2 Right Start", "DefaultTaskSetup", ""), " ");
        //    var sr76 = new Regex("[\\s]+").Replace(cfg.ReadConfig("UPAL HR2 Right Start", "ChuckCornerSetup", ""), " ");
        //    var sr77 = new Regex("[\\s]+").Replace(cfg.ReadConfig("UPAL HR2 Right Start", "CameraCalibrationSetup", ""), " ");
        //    cameras.CameraInfoDic.Add(new CameraInfoPTP
        //    {
        //        CameraName = cfg.ReadConfig("UPAL HR2 Right Start", "Camera_Name", ""),
        //        RealCameraName = cfg.ReadConfig("UPAL HR2 Right Start", "'Camera_Name", ""),
        //        TriggerType = cfg.ReadConfig("UPAL HR2 Right Start", "Trigger_Type", ""),
        //        PaketSize1 = Convert.ToInt32(cfg.ReadConfig("UPAL HR2 Right Start", "'PacketSize", "")),
        //        PaketDelay1 = Convert.ToInt32(cfg.ReadConfig("UPAL HR2 Right Start", "'PacketDelay", "")),
        //        PaketSize = Convert.ToInt32(cfg.ReadConfig("UPAL HR2 Right Start", "PacketSize", "")),
        //        PaketDelay = Convert.ToInt32(cfg.ReadConfig("UPAL HR2 Right Start", "PacketDelay", "")),
        //        DefaultTaskSetup1 = Convert.ToInt32(sr75.Split(" ")[0]),
        //        DefaultTaskSetup2 = Convert.ToInt32(sr75.Split(" ")[1]),
        //        DefaultTaskSetup3 = Convert.ToInt32(sr75.Split(" ")[2]),
        //        DefaultTaskSetup4 = Convert.ToInt32(sr75.Split(" ")[3]),
        //        DefaultTaskSetup5 = Convert.ToInt32(sr75.Split(" ")[4]),
        //        DefaultTaskSetup6 = Convert.ToInt32(sr75.Split(" ")[5]),
        //        DefaultTaskSetup7 = Convert.ToInt32(sr75.Split(" ")[6]),
        //        DefaultTaskSetup8 = Convert.ToInt32(sr75.Split(" ")[7]),

        //        ChuckCornerSetup1 = Convert.ToInt32(sr76.Split(" ")[0]),
        //        ChuckCornerSetup2 = Convert.ToInt32(sr76.Split(" ")[1]),
        //        ChuckCornerSetup3 = Convert.ToInt32(sr76.Split(" ")[2]),
        //        ChuckCornerSetup4 = Convert.ToInt32(sr76.Split(" ")[3]),
        //        ChuckCornerSetup5 = Convert.ToInt32(sr76.Split(" ")[4]),
        //        ChuckCornerSetup6 = Convert.ToInt32(sr76.Split(" ")[5]),
        //        ChuckCornerSetup7 = Convert.ToInt32(sr76.Split(" ")[6]),
        //        ChuckCornerSetup8 = Convert.ToInt32(sr76.Split(" ")[7]),

        //        CameraCalibrationSetup1 = Convert.ToInt32(sr77.Split(" ")[0]),
        //        CameraCalibrationSetup2 = Convert.ToInt32(sr77.Split(" ")[1]),
        //        CameraCalibrationSetup3 = Convert.ToInt32(sr77.Split(" ")[2]),
        //        CameraCalibrationSetup4 = Convert.ToInt32(sr77.Split(" ")[3]),
        //        CameraCalibrationSetup5 = Convert.ToInt32(sr77.Split(" ")[4]),
        //        CameraCalibrationSetup6 = Convert.ToInt32(sr77.Split(" ")[5]),
        //        CameraCalibrationSetup7 = Convert.ToInt32(sr77.Split(" ")[6]),
        //        CameraCalibrationSetup8 = Convert.ToInt32(sr77.Split(" ")[7])
        //    }
        //    );

        //    //[UPAL HR2 Right End]
        //    var sr78 = new Regex("[\\s]+").Replace(cfg.ReadConfig("UPAL HR2 Right End", "DefaultTaskSetup", ""), " ");
        //    var sr79 = new Regex("[\\s]+").Replace(cfg.ReadConfig("UPAL HR2 Right End", "ChuckCornerSetup", ""), " ");
        //    var sr80 = new Regex("[\\s]+").Replace(cfg.ReadConfig("UPAL HR2 Right End", "CameraCalibrationSetup", ""), " ");
        //    cameras.CameraInfoDic.Add(new CameraInfoPTP
        //    {
        //        CameraName = cfg.ReadConfig("UPAL HR2 Right End", "Camera_Name", ""),
        //        RealCameraName = cfg.ReadConfig("UPAL HR2 Right End", "'Camera_Name", ""),
        //        TriggerType = cfg.ReadConfig("UPAL HR2 Right End", "Trigger_Type", ""),
        //        PaketSize = Convert.ToInt32(cfg.ReadConfig("UPAL HR2 Right End", "PacketSize", "")),
        //        PaketDelay = Convert.ToInt32(cfg.ReadConfig("UPAL HR2 Right End", "PacketDelay", "")),
        //        PaketSize1 = Convert.ToInt32(cfg.ReadConfig("UPAL HR2 Right End", "'PacketSize", "")),
        //        PaketDelay1 = Convert.ToInt32(cfg.ReadConfig("UPAL HR2 Right End", "'PacketDelay", "")),
        //        DefaultTaskSetup1 = Convert.ToInt32(sr78.Split(" ")[0]),
        //        DefaultTaskSetup2 = Convert.ToInt32(sr78.Split(" ")[1]),
        //        DefaultTaskSetup3 = Convert.ToInt32(sr78.Split(" ")[2]),
        //        DefaultTaskSetup4 = Convert.ToInt32(sr78.Split(" ")[3]),
        //        DefaultTaskSetup5 = Convert.ToInt32(sr78.Split(" ")[4]),
        //        DefaultTaskSetup6 = Convert.ToInt32(sr78.Split(" ")[5]),
        //        DefaultTaskSetup7 = Convert.ToInt32(sr78.Split(" ")[6]),
        //        DefaultTaskSetup8 = Convert.ToInt32(sr78.Split(" ")[7]),

        //        ChuckCornerSetup1 = Convert.ToInt32(sr79.Split(" ")[0]),
        //        ChuckCornerSetup2 = Convert.ToInt32(sr79.Split(" ")[1]),
        //        ChuckCornerSetup3 = Convert.ToInt32(sr79.Split(" ")[2]),
        //        ChuckCornerSetup4 = Convert.ToInt32(sr79.Split(" ")[3]),
        //        ChuckCornerSetup5 = Convert.ToInt32(sr79.Split(" ")[4]),
        //        ChuckCornerSetup6 = Convert.ToInt32(sr79.Split(" ")[5]),
        //        ChuckCornerSetup7 = Convert.ToInt32(sr79.Split(" ")[6]),
        //        ChuckCornerSetup8 = Convert.ToInt32(sr79.Split(" ")[7]),

        //        CameraCalibrationSetup1 = Convert.ToInt32(sr80.Split(" ")[0]),
        //        CameraCalibrationSetup2 = Convert.ToInt32(sr80.Split(" ")[1]),
        //        CameraCalibrationSetup3 = Convert.ToInt32(sr80.Split(" ")[2]),
        //        CameraCalibrationSetup4 = Convert.ToInt32(sr80.Split(" ")[3]),
        //        CameraCalibrationSetup5 = Convert.ToInt32(sr80.Split(" ")[4]),
        //        CameraCalibrationSetup6 = Convert.ToInt32(sr80.Split(" ")[5]),
        //        CameraCalibrationSetup7 = Convert.ToInt32(sr80.Split(" ")[6]),
        //        CameraCalibrationSetup8 = Convert.ToInt32(sr80.Split(" ")[7])
        //    }
        //    );
        //}

    }
}
