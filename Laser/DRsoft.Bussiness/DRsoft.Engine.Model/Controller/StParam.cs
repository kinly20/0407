using System.Runtime.InteropServices;

namespace DRsoft.Engine.Model.Controller
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class StParam
    {
        /// <summary>
        /// Z11位移尺安装基准位
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Ruler11BasePos { get; set; }

        /// <summary>
        /// Z12位移尺安装基准位
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Ruler12BasePos { get; set; }

        /// <summary>
        /// 龙门1等待位 
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry1WaitPos { get; set; }

        /// <summary>
        /// 龙门1拍照位1
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry1StationAGrabPos { get; set; }

        /// <summary>
        /// 龙门2拍照位1
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry2StationAGrabPos { get; set; }

        /// <summary>
        /// 龙门1台面A打标位置1
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry1StationAMark1Pos { get; set; }

        /// <summary>
        /// 龙门1台面A打标位置2
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry1StationAMark2Pos { get; set; }

        /// <summary>
        /// 龙门1台面A打标位置3
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry1StationAMark3Pos { get; set; }

        /// <summary>
        /// 龙门1台面A打标位置4
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry1StationAMark4Pos { get; set; }

        /// <summary>
        /// 龙门1台面A打标位置5
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry1StationAMark5Pos { get; set; }

        /// <summary>
        /// 龙门1台面A打标位置6
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry1StationAMark6Pos { get; set; }

        /// <summary>
        /// 龙门1台面A打标位置7 
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry1StationAMark7Pos { get; set; }

        /// <summary>
        /// 龙门1台面A打标位置8
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry1StationAMark8Pos { get; set; }

        /// <summary>
        /// 龙门2台面A打标位置1
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry2StationAMark1Pos { get; set; }

        /// <summary>
        /// 龙门2台面A打标位置2
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry2StationAMark2Pos { get; set; }

        /// <summary>
        /// 龙门2台面A打标位置3
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry2StationAMark3Pos { get; set; }

        /// <summary>
        /// 龙门2台面A打标位置4
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry2StationAMark4Pos { get; set; }

        /// <summary>
        /// 龙门2台面A打标位置5
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry2StationAMark5Pos { get; set; }

        /// <summary>
        /// 龙门2台面A打标位置6
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry2StationAMark6Pos { get; set; }

        /// <summary>
        /// 龙门2台面A打标位置7
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry2StationAMark7Pos { get; set; }

        /// <summary>
        /// 龙门2台面A打标位置8
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry2StationAMark8Pos { get; set; }

        /// <summary>
        /// 刮膜1起点位置
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Peeling1StartPos { get; set; }

        /// <summary>
        /// 刮膜1终点位置
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Peeling1EndPos { get; set; }

        /// <summary>
        /// Z1下降位
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Z1DownPos { get; set; }

        /// <summary>
        /// Z1顶升位
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Z1UpPos { get; set; }

        /// <summary>
        /// Z21位移尺安装基准位
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Ruler21BasePos { get; set; }

        /// <summary>
        /// Z22位移尺安装基准位
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Ruler22BasePos { get; set; }

        /// <summary>
        /// 龙门2等待位
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry2WaitPos { get; set; }

        /// <summary>
        /// 龙门1-StationB拍照位1
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry1StationBGrabPos { get; set; }

        /// <summary>
        /// 龙门2-StationB拍照位1
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry2StationBGrabPos { get; set; }

        /// <summary>
        /// 龙门1台面B打标位置1
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry1StationBMark1Pos { get; set; }

        /// <summary>
        /// 龙门1台面B打标位置2
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry1StationBMark2Pos { get; set; }

        /// <summary>
        /// 龙门1台面B打标位置3
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry1StationBMark3Pos { get; set; }

        /// <summary>
        /// 龙门1台面B打标位置4
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry1StationBMark4Pos { get; set; }

        /// <summary>
        /// 龙门1台面B打标位置5
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry1StationBMark5Pos { get; set; }

        /// <summary>
        /// 龙门1台面B打标位置6
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry1StationBMark6Pos { get; set; }

        /// <summary>
        /// 龙门1台面B打标位置7
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry1StationBMark7Pos { get; set; }

        /// <summary>
        /// 龙门1台面B打标位置8
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry1StationBMark8Pos { get; set; }

        /// <summary>
        /// 龙门2台面B打标位置1
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry2StationBMark1Pos { get; set; }

        /// <summary>
        /// 龙门2台面B打标位置2
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry2StationBMark2Pos { get; set; }

        /// <summary>
        /// 龙门2台面B打标位置3
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry2StationBMark3Pos { get; set; }

        /// <summary>
        /// 龙门2台面B打标位置4
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry2StationBMark4Pos { get; set; }

        /// <summary>
        /// 龙门2台面B打标位置5
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry2StationBMark5Pos { get; set; }

        /// <summary>
        /// 龙门2台面B打标位置6
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry2StationBMark6Pos { get; set; }

        /// <summary>
        /// 龙门2台面B打标位置7
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry2StationBMark7Pos { get; set; }

        /// <summary>
        /// 龙门2台面B打标位置8
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry2StationBMark8Pos { get; set; }

        /// <summary>
        /// 刮膜2起点位置
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Peeling2StartPos { get; set; }

        /// <summary>
        /// 刮膜2终点位置
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Peeling2EndPos { get; set; }

        /// <summary>
        /// Z2下降位
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Z2DownPos { get; set; }

        /// <summary>
        /// Z2顶升位
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Z2UpPos { get; set; }

        /// <summary>
        /// 挡光伺服1位置0
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float CamShutter1Pos0 { get; set; }

        /// <summary>
        /// 挡光伺服1位置1
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float CamShutter1Pos1 { get; set; }

        /// <summary>
        /// 挡光伺服1位置2
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float CamShutter1Pos2 { get; set; }

        /// <summary>
        /// 挡光伺服1位置3 
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float CamShutter1Pos3 { get; set; }

        /// <summary>
        /// 挡光伺服2位置0
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float CamShutter2Pos0 { get; set; }

        /// <summary>
        /// 挡光伺服2位置1
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float CamShutter2Pos1 { get; set; }

        /// <summary>
        /// 挡光伺服2位置2
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float CamShutter2Pos2 { get; set; }

        /// <summary>
        /// 挡光伺服2位置3
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float CamShutter2Pos3 { get; set; }

        /// <summary>
        /// 放卷提升位置
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float UwLiftUpPos { get; set; }

        /// <summary>
        /// 收卷提升位置
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float RwLiftUpPos { get; set; }

        /// <summary>
        /// 焊接次数
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float ProcessTimes { get; set; }

        /// <summary>
        /// 拍照超时时间
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float GrabTimeOutSet { get; set; }

        /// <summary>
        /// 台面A真空延时
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float StationAVacOkDelay { get; set; }

        /// <summary>
        /// 台面B真空延时 
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float StationBVacOkDelay { get; set; }

        /// <summary>
        /// 台面A破真空延时
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float StationABlowDelay { get; set; }

        /// <summary>
        /// 台面B破真空延时
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float StationBBlowDelay { get; set; }

        /// <summary>
        /// 自动测功率片数设定
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float AutoLeaserMeasureNum { get; set; }

        /// <summary>
        /// 龙门1测功率位
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry1PowerMeterPos { get; set; }

        /// <summary>
        /// 龙门2测功率位
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry2PowerMeterPos { get; set; }

        /// <summary>
        /// 功率计测量光速一龙门偏差
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float LeftOffset { get; set; }

        /// <summary>
        /// 功率计测量光速二龙门偏差
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float MidOffset { get; set; }

        /// <summary>
        /// 功率计测量光速三龙门偏差
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float RightOffset { get; set; }

        /// <summary>
        /// 功率计测量位1
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float PowerMeterMeasurePos1 { get; set; }

        /// <summary>
        /// 功率计测量位2
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float PowerMeterMeasurePos2 { get; set; }

        /// <summary>
        /// 功率计测量位3
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float PowerMeterMeasurePos3 { get; set; }

        /// <summary>
        /// 功率计测量位4
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float PowerMeterMeasurePos4 { get; set; }

        /// <summary>
        /// 功率计测量位5
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float PowerMeterMeasurePos5 { get; set; }

        /// <summary>
        /// 功率计测量位6
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float PowerMeterMeasurePos6 { get; set; }

        /// <summary>
        /// 放卷设定张力
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float UwTorqueSet { get; set; }

        /// <summary>
        /// 收卷设定张力
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float RwTorqueSet { get; set; }

        /// <summary>
        /// 拉膜长度
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float TapeLength { get; set; }

        /// <summary>
        /// 焊接1定位延时
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float StationPosADelay { get; set; }

        /// <summary>
        /// 焊接2定位延时
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float StationPosBDelay { get; set; }

        /// <summary>
        /// 放卷力矩模式速度限幅
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float UwTorqueModeVeloLimt { get; set; }

        /// <summary>
        /// 收卷力矩模式速度限幅
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float RwTorqueModeVeloLimt { get; set; }

        /// <summary>
        /// 放卷半径检测模拟量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float UwRadius_AnalogMax { get; set; }

        /// <summary>
        /// 放卷半径检测模拟量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float UwRadius_AnalogMin { get; set; }

        /// <summary>
        /// 放卷半径检测工程量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float UwRadius_MeasurementMax { get; set; }

        /// <summary>
        /// 放卷半径检测工程量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float UwRadius_MeasurementMin { get; set; }

        /// <summary>
        /// 收卷半径检测模拟量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float RwRadius_AnalogMax { get; set; }

        /// <summary>
        /// 收卷半径检测模拟量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float RwRadius_AnalogMin { get; set; }

        /// <summary>
        /// 收卷半径检测工程量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float RwRadius_MeasurementMax { get; set; }

        /// <summary>
        /// 收卷半径检测工程量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float RwRadius_MeasurementMin { get; set; }

        /// <summary>
        /// 放卷纠偏检测模拟量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float UwSteer_AnalogMax { get; set; }

        /// <summary>
        /// 放卷纠偏检测模拟量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float UwSteer_AnalogMin { get; set; }

        /// <summary>
        /// 放卷纠偏检测工程量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float UwSteer_MeasurementMax { get; set; }

        /// <summary>
        /// 放卷纠偏检测工程量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float UwSteer_MeasurementMin { get; set; }

        /// <summary>
        /// 放卷纠偏检测模拟量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float RwSteer_AnalogMax { get; set; }

        /// <summary>
        /// 放卷纠偏检测模拟量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float RwSteer_AnalogMin { get; set; }

        /// <summary>
        /// 放卷纠偏检测工程量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float RwSteer_MeasurementMax { get; set; }

        /// <summary>
        /// 放卷纠偏检测工程量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float RwSteer_MeasurementMin { get; set; }

        /// <summary>
        /// 位移尺11检测模拟量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Ruler11_AnalogMax { get; set; }

        /// <summary>
        /// 位移尺11检测模拟量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Ruler11_AnalogMin { get; set; }

        /// <summary>
        /// 位移尺11检测工程量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Ruler11_MeasurementMax { get; set; }

        /// <summary>
        /// 位移尺11检测工程量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Ruler11_MeasurementMin { get; set; }

        /// <summary>
        /// 位移尺12检测模拟量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Ruler12_AnalogMax { get; set; }

        /// <summary>
        /// 位移尺12检测模拟量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Ruler12_AnalogMin { get; set; }

        /// <summary>
        /// 位移尺12检测工程量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Ruler12_MeasurementMax { get; set; }

        /// <summary>
        /// 位移尺12检测工程量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Ruler12_MeasurementMin { get; set; }

        /// <summary>
        /// 位移尺21检测模拟量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Ruler21_AnalogMax { get; set; }

        /// <summary>
        /// 位移尺21检测模拟量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Ruler21_AnalogMin { get; set; }

        /// <summary>
        /// 位移尺21检测工程量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Ruler21_MeasurementMax { get; set; }

        /// <summary>
        /// 位移尺21检测工程量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Ruler21_MeasurementMin { get; set; }

        /// <summary>
        /// 位移尺22检测模拟量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Ruler22_AnalogMax { get; set; }

        /// <summary>
        /// 位移尺22检测模拟量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Ruler22_AnalogMin { get; set; }

        /// <summary>
        /// 位移尺22检测工程量最大值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Ruler22_MeasurementMax { get; set; }

        /// <summary>
        /// 位移尺22检测工程量最小值
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Ruler22_MeasurementMin { get; set; }

        /// <summary>
        /// 打齐11等待位
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Align11WaitPos { get; set; }

        /// <summary>
        /// 打齐12等待位
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Align12WaitPos { get; set; }

        /// <summary>
        /// 打齐21等待位
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Align21WaitPos { get; set; }

        /// <summary>
        /// 打齐22等待位
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Align22WaitPos { get; set; }

        /// <summary>
        /// Z1刮膜高度
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Z1_PeelingPos { get; set; }

        /// <summary>
        /// Z2刮膜高度
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Z2_PeelingPos { get; set; }

        /// <summary>
        /// 龙门11安装基准位
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry11BasePos { get; set; }

        /// <summary>
        /// 龙门12安装基准位
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry12BasePos { get; set; }

        /// <summary>
        /// 龙门21安装基准位
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry21BasePos { get; set; }

        /// <summary>
        /// 龙门22安装基准位
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Gantry22BasePos { get; set; }


        public bool Changed(StParam obj)
        {
            if (obj == null) return false;
            return Ruler11BasePos != obj.Ruler11BasePos ||
                   Ruler12BasePos != obj.Ruler12BasePos ||
                   Gantry1WaitPos != obj.Gantry1WaitPos ||
                   Gantry1StationAGrabPos != obj.Gantry1StationAGrabPos ||
                   Gantry2StationAGrabPos != obj.Gantry2StationAGrabPos ||
                   Gantry1StationAMark1Pos != obj.Gantry1StationAMark1Pos ||
                   Gantry1StationAMark2Pos != obj.Gantry1StationAMark2Pos ||
                   Gantry1StationAMark3Pos != obj.Gantry1StationAMark3Pos ||
                   Gantry1StationAMark4Pos != obj.Gantry1StationAMark4Pos ||
                   Gantry1StationAMark5Pos != obj.Gantry1StationAMark5Pos ||
                   Gantry1StationAMark6Pos != obj.Gantry1StationAMark6Pos ||
                   Gantry1StationAMark7Pos != obj.Gantry1StationAMark7Pos ||
                   Gantry1StationAMark8Pos != obj.Gantry1StationAMark8Pos ||
                   Gantry2StationAMark1Pos != obj.Gantry2StationAMark1Pos ||
                   Gantry2StationAMark2Pos != obj.Gantry2StationAMark2Pos ||
                   Gantry2StationAMark3Pos != obj.Gantry2StationAMark3Pos ||
                   Gantry2StationAMark4Pos != obj.Gantry2StationAMark4Pos ||
                   Gantry2StationAMark5Pos != obj.Gantry2StationAMark5Pos ||
                   Gantry2StationAMark6Pos != obj.Gantry2StationAMark6Pos ||
                   Gantry2StationAMark7Pos != obj.Gantry2StationAMark7Pos ||
                   Gantry2StationAMark8Pos != obj.Gantry2StationAMark8Pos ||
                   Peeling1StartPos != obj.Peeling1StartPos ||
                   Peeling1EndPos != obj.Peeling1EndPos ||
                   Z1DownPos != obj.Z1DownPos ||
                   Z1UpPos != obj.Z1UpPos ||
                   Ruler21BasePos != obj.Ruler21BasePos ||
                   Ruler22BasePos != obj.Ruler22BasePos ||
                   Gantry2WaitPos != obj.Gantry2WaitPos ||
                   Gantry1StationBGrabPos != obj.Gantry1StationBGrabPos ||
                   Gantry2StationBGrabPos != obj.Gantry2StationBGrabPos ||
                   Gantry1StationBMark1Pos != obj.Gantry1StationBMark1Pos ||
                   Gantry1StationBMark2Pos != obj.Gantry1StationBMark2Pos ||
                   Gantry1StationBMark3Pos != obj.Gantry1StationBMark3Pos ||
                   Gantry1StationBMark4Pos != obj.Gantry1StationBMark4Pos ||
                   Gantry1StationBMark5Pos != obj.Gantry1StationBMark5Pos ||
                   Gantry1StationBMark6Pos != obj.Gantry1StationBMark6Pos ||
                   Gantry1StationBMark7Pos != obj.Gantry1StationBMark7Pos ||
                   Gantry1StationBMark8Pos != obj.Gantry1StationBMark8Pos ||
                   Gantry2StationBMark1Pos != obj.Gantry2StationBMark1Pos ||
                   Gantry2StationBMark2Pos != obj.Gantry2StationBMark2Pos ||
                   Gantry2StationBMark3Pos != obj.Gantry2StationBMark3Pos ||
                   Gantry2StationBMark4Pos != obj.Gantry2StationBMark4Pos ||
                   Gantry2StationBMark5Pos != obj.Gantry2StationBMark5Pos ||
                   Gantry2StationBMark6Pos != obj.Gantry2StationBMark6Pos ||
                   Gantry2StationBMark7Pos != obj.Gantry2StationBMark7Pos ||
                   Gantry2StationBMark8Pos != obj.Gantry2StationBMark8Pos ||
                   Peeling2StartPos != obj.Peeling2StartPos ||
                   Peeling2EndPos != obj.Peeling2EndPos ||
                   Z2DownPos != obj.Z2DownPos ||
                   Z2UpPos != obj.Z2UpPos ||
                   CamShutter1Pos0 != obj.CamShutter1Pos0 ||
                   CamShutter1Pos1 != obj.CamShutter1Pos1 ||
                   CamShutter1Pos2 != obj.CamShutter1Pos2 ||
                   CamShutter1Pos3 != obj.CamShutter1Pos3 ||
                   CamShutter2Pos0 != obj.CamShutter2Pos0 ||
                   CamShutter2Pos1 != obj.CamShutter2Pos1 ||
                   CamShutter2Pos2 != obj.CamShutter2Pos2 ||
                   CamShutter2Pos3 != obj.CamShutter2Pos3 ||
                   UwLiftUpPos != obj.UwLiftUpPos ||
                   RwLiftUpPos != obj.RwLiftUpPos ||
                   ProcessTimes != obj.ProcessTimes ||
                   GrabTimeOutSet != obj.GrabTimeOutSet ||
                   StationAVacOkDelay != obj.StationAVacOkDelay ||
                   StationBVacOkDelay != obj.StationBVacOkDelay ||
                   StationABlowDelay != obj.StationABlowDelay ||
                   StationBBlowDelay != obj.StationBBlowDelay ||
                   AutoLeaserMeasureNum != obj.AutoLeaserMeasureNum ||
                   Gantry1PowerMeterPos != obj.Gantry1PowerMeterPos ||
                   Gantry2PowerMeterPos != obj.Gantry2PowerMeterPos ||
                   LeftOffset != obj.LeftOffset ||
                   MidOffset != obj.MidOffset ||
                   RightOffset != obj.RightOffset ||
                   PowerMeterMeasurePos1 != obj.PowerMeterMeasurePos1 ||
                   PowerMeterMeasurePos2 != obj.PowerMeterMeasurePos2 ||
                   PowerMeterMeasurePos3 != obj.PowerMeterMeasurePos3 ||
                   PowerMeterMeasurePos4 != obj.PowerMeterMeasurePos4 ||
                   PowerMeterMeasurePos5 != obj.PowerMeterMeasurePos5 ||
                   PowerMeterMeasurePos6 != obj.PowerMeterMeasurePos6 ||
                   UwLiftUpPos != obj.UwLiftUpPos ||
                   RwLiftUpPos != obj.RwLiftUpPos ||
                   TapeLength != obj.TapeLength ||
                   StationPosADelay != obj.StationPosADelay ||
                   StationPosBDelay != obj.StationPosBDelay ||
                   UwTorqueModeVeloLimt != obj.UwTorqueModeVeloLimt ||
                   RwTorqueModeVeloLimt != obj.RwTorqueModeVeloLimt ||
                   UwRadius_AnalogMax != obj.UwRadius_AnalogMax ||
                   UwRadius_AnalogMin != obj.UwRadius_AnalogMin ||
                   UwRadius_MeasurementMax != obj.UwRadius_MeasurementMax ||
                   UwRadius_MeasurementMin != obj.UwRadius_MeasurementMin ||
                   RwRadius_AnalogMax != obj.RwRadius_AnalogMax ||
                   RwRadius_AnalogMin != obj.RwRadius_AnalogMin ||
                   RwRadius_MeasurementMax != obj.RwRadius_MeasurementMax ||
                   RwRadius_MeasurementMin != obj.RwRadius_MeasurementMin ||
                   UwSteer_AnalogMax != obj.UwSteer_AnalogMax ||
                   UwSteer_AnalogMin != obj.UwSteer_AnalogMin ||
                   UwSteer_MeasurementMax != obj.UwSteer_MeasurementMax ||
                   UwSteer_MeasurementMin != obj.UwSteer_MeasurementMin ||
                   RwSteer_AnalogMax != obj.RwSteer_AnalogMax ||
                   RwSteer_AnalogMin != obj.RwSteer_AnalogMin ||
                   RwSteer_MeasurementMax != obj.RwSteer_MeasurementMax ||
                   RwSteer_MeasurementMin != obj.RwSteer_MeasurementMin ||
                   Ruler11_AnalogMax != obj.Ruler11_AnalogMax ||
                   Ruler11_AnalogMin != obj.Ruler11_AnalogMin ||
                   Ruler11_MeasurementMax != obj.Ruler11_MeasurementMax ||
                   Ruler11_MeasurementMin != obj.Ruler11_MeasurementMin ||
                   Ruler12_AnalogMax != obj.Ruler12_AnalogMax ||
                   Ruler12_AnalogMin != obj.Ruler12_AnalogMin ||
                   Ruler12_MeasurementMax != obj.Ruler12_MeasurementMax ||
                   Ruler12_MeasurementMin != obj.Ruler12_MeasurementMin ||
                   Ruler21_AnalogMax != obj.Ruler21_AnalogMax ||
                   Ruler21_AnalogMin != obj.Ruler21_AnalogMin ||
                   Ruler21_MeasurementMax != obj.Ruler21_MeasurementMax ||
                   Ruler21_MeasurementMin != obj.Ruler21_MeasurementMin ||
                   Ruler22_AnalogMax != obj.Ruler22_AnalogMax ||
                   Ruler22_AnalogMin != obj.Ruler22_AnalogMin ||
                   Ruler22_MeasurementMax != obj.Ruler22_MeasurementMax ||
                   Ruler22_MeasurementMin != obj.Ruler22_MeasurementMin ||
                   Align11WaitPos != obj.Align11WaitPos ||
                   Align12WaitPos != obj.Align12WaitPos ||
                   Align21WaitPos != obj.Align21WaitPos ||
                   Align22WaitPos != obj.Align22WaitPos ||
                   Z1_PeelingPos != obj.Z1_PeelingPos ||
                   Z2_PeelingPos != obj.Z2_PeelingPos ||
                   Gantry11BasePos != obj.Gantry11BasePos ||
                   Gantry12BasePos != obj.Gantry12BasePos ||
                   Gantry21BasePos != obj.Gantry21BasePos ||
                   Gantry22BasePos != obj.Gantry22BasePos;


        }

        public StParam Clone()
        {
            return new StParam
            {
                Ruler11BasePos = this.Ruler11BasePos,
                Ruler12BasePos = this.Ruler12BasePos,
                Gantry1WaitPos = this.Gantry1WaitPos,
                Gantry1StationAGrabPos = this.Gantry1StationAGrabPos,
                Gantry2StationAGrabPos = this.Gantry2StationAGrabPos,
                Gantry1StationAMark1Pos = this.Gantry1StationAMark1Pos,
                Gantry1StationAMark2Pos = this.Gantry1StationAMark2Pos,
                Gantry1StationAMark3Pos = this.Gantry1StationAMark3Pos,
                Gantry1StationAMark4Pos = this.Gantry1StationAMark4Pos,
                Gantry1StationAMark5Pos = this.Gantry1StationAMark5Pos,
                Gantry1StationAMark6Pos = this.Gantry1StationAMark6Pos,
                Gantry1StationAMark7Pos = this.Gantry1StationAMark7Pos,
                Gantry1StationAMark8Pos = this.Gantry1StationAMark8Pos,
                Gantry2StationAMark1Pos = this.Gantry2StationAMark1Pos,
                Gantry2StationAMark2Pos = this.Gantry2StationAMark2Pos,
                Gantry2StationAMark3Pos = this.Gantry2StationAMark3Pos,
                Gantry2StationAMark4Pos = this.Gantry2StationAMark4Pos,
                Gantry2StationAMark5Pos = this.Gantry2StationAMark5Pos,
                Gantry2StationAMark6Pos = this.Gantry2StationAMark6Pos,
                Gantry2StationAMark7Pos = this.Gantry2StationAMark7Pos,
                Gantry2StationAMark8Pos = this.Gantry2StationAMark8Pos,
                Peeling1StartPos = this.Peeling1StartPos,
                Peeling1EndPos = this.Peeling1EndPos,
                Z1DownPos = this.Z1DownPos,
                Z1UpPos = this.Z1UpPos,
                Ruler21BasePos = this.Ruler21BasePos,
                Ruler22BasePos = this.Ruler22BasePos,
                Gantry2WaitPos = this.Gantry2WaitPos,
                Gantry1StationBGrabPos = this.Gantry1StationBGrabPos,
                Gantry2StationBGrabPos = this.Gantry2StationBGrabPos,
                Gantry1StationBMark1Pos = this.Gantry1StationBMark1Pos,
                Gantry1StationBMark2Pos = this.Gantry1StationBMark2Pos,
                Gantry1StationBMark3Pos = this.Gantry1StationBMark3Pos,
                Gantry1StationBMark4Pos = this.Gantry1StationBMark4Pos,
                Gantry1StationBMark5Pos = this.Gantry1StationBMark5Pos,
                Gantry1StationBMark6Pos = this.Gantry1StationBMark6Pos,
                Gantry1StationBMark7Pos = this.Gantry1StationBMark7Pos,
                Gantry1StationBMark8Pos = this.Gantry1StationBMark8Pos,
                Gantry2StationBMark1Pos = this.Gantry2StationBMark1Pos,
                Gantry2StationBMark2Pos = this.Gantry2StationBMark2Pos,
                Gantry2StationBMark3Pos = this.Gantry2StationBMark3Pos,
                Gantry2StationBMark4Pos = this.Gantry2StationBMark4Pos,
                Gantry2StationBMark5Pos = this.Gantry2StationBMark5Pos,
                Gantry2StationBMark6Pos = this.Gantry2StationBMark6Pos,
                Gantry2StationBMark7Pos = this.Gantry2StationBMark7Pos,
                Gantry2StationBMark8Pos = this.Gantry2StationBMark8Pos,
                Peeling2StartPos = this.Peeling2StartPos,
                Peeling2EndPos = this.Peeling2EndPos,
                Z2DownPos = this.Z2DownPos,
                Z2UpPos = this.Z2UpPos,
                CamShutter1Pos0 = this.CamShutter1Pos0,
                CamShutter1Pos1 = this.CamShutter1Pos1,
                CamShutter1Pos2 = this.CamShutter1Pos2,
                CamShutter1Pos3 = this.CamShutter1Pos3,
                CamShutter2Pos0 = this.CamShutter2Pos0,
                CamShutter2Pos1 = this.CamShutter2Pos1,
                CamShutter2Pos2 = this.CamShutter2Pos2,
                CamShutter2Pos3 = this.CamShutter2Pos3,
                UwLiftUpPos = this.UwLiftUpPos,
                RwLiftUpPos = this.RwLiftUpPos,
                ProcessTimes = this.ProcessTimes,
                GrabTimeOutSet = this.GrabTimeOutSet,
                StationAVacOkDelay = this.StationAVacOkDelay,
                StationBVacOkDelay = this.StationBVacOkDelay,
                StationABlowDelay = this.StationABlowDelay,
                StationBBlowDelay = this.StationBBlowDelay,
                AutoLeaserMeasureNum = this.AutoLeaserMeasureNum,
                Gantry1PowerMeterPos = this.Gantry1PowerMeterPos,
                Gantry2PowerMeterPos = this.Gantry2PowerMeterPos,
                LeftOffset = this.LeftOffset,
                MidOffset = this.MidOffset,
                RightOffset = this.RightOffset,
                PowerMeterMeasurePos1 = this.PowerMeterMeasurePos1,
                PowerMeterMeasurePos2 = this.PowerMeterMeasurePos2,
                PowerMeterMeasurePos3 = this.PowerMeterMeasurePos3,
                PowerMeterMeasurePos4 = this.PowerMeterMeasurePos4,
                PowerMeterMeasurePos5 = this.PowerMeterMeasurePos5,
                PowerMeterMeasurePos6 = this.PowerMeterMeasurePos6,
                UwTorqueSet = this.UwTorqueSet,
                RwTorqueSet = this.RwTorqueSet,
                TapeLength = this.TapeLength,
                StationPosADelay = this.StationPosADelay,
                StationPosBDelay = this.StationPosBDelay,
                UwTorqueModeVeloLimt = this.UwTorqueModeVeloLimt,
                RwTorqueModeVeloLimt = this.RwTorqueModeVeloLimt,
                UwRadius_AnalogMax = this.UwRadius_AnalogMax,
                UwRadius_AnalogMin = this.UwRadius_AnalogMin,
                UwRadius_MeasurementMax = this.UwRadius_MeasurementMax,
                UwRadius_MeasurementMin = this.UwRadius_MeasurementMin,
                RwRadius_AnalogMax = this.RwRadius_AnalogMax,
                RwRadius_AnalogMin = this.RwRadius_AnalogMin,
                RwRadius_MeasurementMax = this.RwRadius_MeasurementMax,
                RwRadius_MeasurementMin = this.RwRadius_MeasurementMin,
                UwSteer_AnalogMax = this.UwSteer_AnalogMax,
                UwSteer_AnalogMin = this.UwSteer_AnalogMin,
                UwSteer_MeasurementMax = this.UwSteer_MeasurementMax,
                UwSteer_MeasurementMin = this.UwSteer_MeasurementMin,
                RwSteer_AnalogMax = this.RwSteer_AnalogMax,
                RwSteer_AnalogMin = this.RwSteer_AnalogMin,
                RwSteer_MeasurementMax = this.RwSteer_MeasurementMax,
                RwSteer_MeasurementMin = this.RwSteer_MeasurementMin,
                Ruler11_AnalogMax = this.Ruler11_AnalogMax,
                Ruler11_AnalogMin = this.Ruler11_AnalogMin,
                Ruler11_MeasurementMax = this.Ruler11_MeasurementMax,
                Ruler11_MeasurementMin = this.Ruler11_MeasurementMin,
                Ruler12_AnalogMax = this.Ruler12_AnalogMax,
                Ruler12_AnalogMin = this.Ruler12_AnalogMin,
                Ruler12_MeasurementMax = this.Ruler12_MeasurementMax,
                Ruler12_MeasurementMin = this.Ruler12_MeasurementMin,
                Ruler21_AnalogMax = this.Ruler21_AnalogMax,
                Ruler21_AnalogMin = this.Ruler21_AnalogMin,
                Ruler21_MeasurementMax = this.Ruler21_MeasurementMax,
                Ruler21_MeasurementMin = this.Ruler21_MeasurementMin,
                Ruler22_AnalogMax = this.Ruler22_AnalogMax,
                Ruler22_AnalogMin = this.Ruler22_AnalogMin,
                Ruler22_MeasurementMax = this.Ruler22_MeasurementMax,
                Ruler22_MeasurementMin = this.Ruler22_MeasurementMin,
                Align11WaitPos = this.Align11WaitPos,
                Align12WaitPos = this.Align12WaitPos,
                Align21WaitPos = this.Align21WaitPos,
                Align22WaitPos = this.Align22WaitPos,
                Z1_PeelingPos = this.Z1_PeelingPos,
                Z2_PeelingPos = this.Z2_PeelingPos,
                Gantry11BasePos = this.Gantry11BasePos,
                Gantry12BasePos = this.Gantry12BasePos,
                Gantry21BasePos = this.Gantry21BasePos,
                Gantry22BasePos = this.Gantry22BasePos

            };
        }
    }
}