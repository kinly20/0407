using System.Runtime.InteropServices;

namespace DRsoft.Engine.Model.Controller
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class StInput
    {
		//	EL1889-A
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Pause { get; set; } //	暂停按钮
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Mute { get; set; }  //	报警消音按钮
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool AirPress { get; set; }  //	总气压表
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool SafeDoor1 { get; set; } //	安全门开关1
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool SafeDoor2 { get; set; } //	安全门开关2
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool SafeDoor3 { get; set; } //	安全门开关3
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool SafeDoor4 { get; set; } //	安全门开关4
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool SafeDoor5 { get; set; } //	安全门开关5
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool SafeDoor6 { get; set; } //	安全门开关6
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool SafeDoor7 { get; set; } //	安全门开关7
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool SafeDoor8 { get; set; } //	安全门开关8
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Mark3_Status { get; set; }  //	打标状态信号3
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Mark4_Status { get; set; }  //	打标状态信号4
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Mark5_Status { get; set; }  //	打标状态信号5
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Mark6_Status { get; set; }  //	打标状态信号6
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Mark7_Status { get; set; }  //	打标状态信号7
																							//	EL1889-B
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool SaftyGrating1 { get; set; } //	光栅感应1
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool SaftyGrating2 { get; set; } //	光栅感应2
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool SaftyGrating3 { get; set; } //	光栅感应3
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool SaftyGrating4 { get; set; } //	光栅感应4
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve2_5 { get; set; }    //	预留
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool UpStreamReady { get; set; } //	上游准备好
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool UpStreamAlm { get; set; }   //	上游故障
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool DownStreamAllow { get; set; }   //	下游允许
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool DownStreamAlm { get; set; } //	下游故障
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Mark8_Status { get; set; }  //	打标状态信号8
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Mark9_Status { get; set; }  //	打标状态信号9
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Mark10_Status { get; set; } //	打标状态信号10
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Mark11_Status { get; set; } //	打标状态信号11
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Mark12_Status { get; set; } //	打标状态信号12
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationA_CycleVac2Ok { get; set; }  //	升降平台A沿边负压检测2
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationB_CycleVac2Ok { get; set; }  //	升降平台B沿边负压检测2
																									//	EL1889-C
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Gantry1Sensor1 { get; set; }    //	龙门1光敏检测1
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Gantry1Sensor2 { get; set; }    //	龙门1光敏检测2
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Gantry1Sensor3 { get; set; }    //	龙门1光敏检测3
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Gantry1Sensor4 { get; set; }    //	龙门1光敏检测4
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Gantry1Sensor5 { get; set; }    //	龙门1光敏检测5
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Gantry1Sensor6 { get; set; }    //	龙门1光敏检测6
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Gantry2Sensor1 { get; set; }    //	龙门2光敏检测1
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Gantry2Sensor2 { get; set; }    //	龙门2光敏检测2
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Gantry2Sensor3 { get; set; }    //	龙门2光敏检测3
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Gantry2Sensor4 { get; set; }    //	龙门2光敏检测4
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Gantry2Sensor5 { get; set; }    //	龙门2光敏检测5
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Gantry2Sensor6 { get; set; }    //	龙门2光敏检测6
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Cooling1_Alm { get; set; }  //	冷水机1报警信号
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Cooling2_Alm { get; set; }  //	冷水机2报警信号
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Mark1_Status { get; set; }  //	打标状态信号1
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Mark2_Status { get; set; }  //	打标状态信号2
																							//	EL1889-D
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool UwSteer_LimP { get; set; }  //	放卷纠偏电机正限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool UwSteer_Home { get; set; }  //	放卷纠偏电机原点
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool UwSteer_LimN { get; set; }  //	放卷纠偏电机负限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool UwLift_LimP { get; set; }   //	放卷升降电机正限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool UwLift_Home { get; set; }   //	放卷升降电机原点
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool UwLift_LimN { get; set; }   //	放卷升降电机负限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool InPressCyl1Basic { get; set; }  //	入口压膜气缸1松开
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool InPressCyl1Work { get; set; }   //	入口压膜气缸1夹紧
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool InPressCyl2Basic { get; set; }  //	入口压膜气缸2松开
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool InPressCyl2Work { get; set; }   //	入口压膜气缸2夹紧
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool InPressCyl3Basic { get; set; }  //	入口压膜气缸3松开
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool InPressCyl3Work { get; set; }   //	入口压膜气缸3夹紧
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool InPressCyl4Basic { get; set; }  //	入口压膜气缸4松开
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool InPressCyl4Work { get; set; }   //	入口压膜气缸4夹紧
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool InBeltSensor1 { get; set; } //	入口流水线检测1
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool InBeltSensor2 { get; set; } //	入口流水线检测2
																							//	EL1889-E
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Peeling1_LimP { get; set; } //	A侧刮膜电机正限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Peeling1_Home { get; set; } //	A侧刮膜电机原点
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Peeling1_LimN { get; set; } //	A侧刮膜电机负限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Align11_LimP { get; set; }  //	A侧打齐模组1正限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Align11_Home { get; set; }  //	A侧打齐模组1原点
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Align11_LimN { get; set; }  //	A侧打齐模组1负限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Align12_LimP { get; set; }  //	A侧打齐模组2正限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Align12_Home { get; set; }  //	A侧打齐模组2原点
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Align12_LimN { get; set; }  //	A侧打齐模组2负限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Z1_LimP { get; set; }   //	A区升降平台正限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Z1_Home { get; set; }   //	A区升降平台原点
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Z1_LimN { get; set; }   //	A区升降平台负限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationA_Sensor1 { get; set; }  //	A侧托盘输送检测1
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationA_Sensor2 { get; set; }  //	A侧托盘输送检测2
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve5_15 { get; set; }   //	预留
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve5_16 { get; set; }   //	预留
																							//	EL1889-F
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationA_AlignCyl1Work { get; set; }    //	A侧托盘打齐气缸1伸出
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationA_AlignCyl1Basic { get; set; }   //	A侧托盘打齐气缸1缩回
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationA_AlignCyl2Work { get; set; }    //	A侧托盘打齐气缸2伸出
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationA_AlignCyl2Basic { get; set; }   //	A侧托盘打齐气缸2缩回
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool MidPressCyl1Work { get; set; }  //	中间压膜气缸1伸出
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool MidPressCyl1Basic { get; set; } //	中间压膜气缸1缩回
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve6_7 { get; set; }    //	预留
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationA_AlignCyl3Work { get; set; }    //	A侧托盘打齐气缸3伸出
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationA_AlignCyl3Basic { get; set; }   //	A侧托盘打齐气缸3缩回
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationA_AlignCyl4Work { get; set; }    //	A侧托盘打齐气缸4伸出
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationA_AlignCyl4Basic { get; set; }   //	A侧托盘打齐气缸4缩回
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool MidPressCyl2Work { get; set; }  //	中间压膜气缸2伸出
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool MidPressCyl2Basic { get; set; } //	中间压膜气缸2缩回
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool MidBeltSensor1 { get; set; }    //	中间流水线检测1
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool MidBeltSensor2 { get; set; }    //	中间流水线检测2
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve6_16 { get; set; }   //	预留
																							//	EL1889-G
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Peeling2_LimP { get; set; } //	B侧刮膜电机正限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Peeling2_Home { get; set; } //	B侧刮膜电机原点
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Peeling2_LimN { get; set; } //	B侧刮膜电机负限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Align21_LimP { get; set; }  //	B侧打齐模组1正限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Align21_Home { get; set; }  //	B侧打齐模组1原点
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Align21_LimN { get; set; }  //	B侧打齐模组1负限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Align22_LimP { get; set; }  //	B侧打齐模组2正限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Align22_Home { get; set; }  //	B侧打齐模组2原点
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Align22_LimN { get; set; }  //	B侧打齐模组2负限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Z2_LimP { get; set; }   //	B区升降平台正限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Z2_Home { get; set; }   //	B区升降平台原点
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Z2_LimN { get; set; }   //	B区升降平台负限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationB_Sensor1 { get; set; }  //	B侧托盘输送检测1
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationB_Sensor2 { get; set; }  //	B侧托盘输送检测2
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve7_15 { get; set; }   //	预留
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve7_16 { get; set; }   //	预留
																							//	EL1889-H
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationB_AlignCyl1Work { get; set; }    //	B侧托盘打齐气缸1伸出
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationB_AlignCyl1Basic { get; set; }   //	B侧托盘打齐气缸1缩回
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationB_AlignCyl2Work { get; set; }    //	B侧托盘打齐气缸2伸出
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationB_AlignCyl2Basic { get; set; }   //	B侧托盘打齐气缸2缩回
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Gantry1_LimP { get; set; }  //	龙门1正限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Gantry1_Home { get; set; }  //	龙门1原点
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Gantry1_LimN { get; set; }  //	龙门1负限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve8_8 { get; set; }    //	预留
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationB_AlignCyl3Work { get; set; }    //	B侧托盘打齐气缸3伸出
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationB_AlignCyl3Basic { get; set; }   //	B侧托盘打齐气缸3缩回
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationB_AlignCyl4Work { get; set; }    //	B侧托盘打齐气缸4伸出
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationB_AlignCyl4Basic { get; set; }   //	B侧托盘打齐气缸4缩回
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Gantry2_LimP { get; set; }  //	龙门2正限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Gantry2_Home { get; set; }  //	龙门2原点
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Gantry2_LimN { get; set; }  //	龙门2负限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve8_16 { get; set; }   //	预留
																							//	EL1889-I
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool RwSteer_LimP { get; set; }  //	收卷纠偏电机正限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool RwSteer_Home { get; set; }  //	收卷纠偏电机原点
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool RwSteer_LimN { get; set; }  //	收卷纠偏电机负限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool RwLift_LimP { get; set; }   //	收卷升降电机
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool RwLift_Home { get; set; }   //	收卷升降电机
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool RwLift_LimN { get; set; }   //	收卷升降电机
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool OutPressCyl1Basic { get; set; } //	出口压膜气缸1松开
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool OutPressCyl1Work { get; set; }  //	出口压膜气缸1夹紧
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool OutPressCyl2Basic { get; set; } //	出口压膜气缸2松开
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool OutPressCyl2Work { get; set; }  //	出口压膜气缸2夹紧
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool OutBeltSensor1 { get; set; }    //	出口流水线检测1
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool OutBeltSensor2 { get; set; }    //	出口流水线检测2
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve9_13 { get; set; }   //	预留
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve9_14 { get; set; }   //	预留
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve9_15 { get; set; }   //	预留
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve9_16 { get; set; }   //	预留
																							//	EL1889-J
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool PowerMeter_LimP { get; set; }   //	功率计电机正限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool PowerMeter_Home { get; set; }   //	功率计电机原点
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool PowerMeter_LimN { get; set; }   //	功率计电机负限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool CamShutter1_LimP { get; set; }  //	相机挡光电机1正限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool CamShutter1_Home { get; set; }  //	相机挡光电机1原点
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool CamShutter1_LimN { get; set; }  //	相机挡光电机1负限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool CamShutter2_LimP { get; set; }  //	相机挡光电机2正限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool CamShutter2_Home { get; set; }  //	相机挡光电机2原点
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool CamShutter2_LimN { get; set; }  //	相机挡光电机2负限位
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationA_MidVacOk { get; set; } //	升降平台A中间负压检测
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationA_CycleVac1Ok { get; set; }  //	升降平台A边沿负压检测
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationB_MidVacOk { get; set; } //	升降平台B中间负压检测
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationB_CycleVac1Ok { get; set; }  //	升降平台B边沿负压检测
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool GantrySafeSensor1 { get; set; } //	龙门防撞光电1
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool GantrySafeSensor2 { get; set; } //	龙门防撞光电2
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve10_16 { get; set; }  //	预留
																							//	EL1889-K
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve11_1 { get; set; }   //	预留
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve11_2 { get; set; }   //	预留
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve11_3 { get; set; }   //	预留
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve11_4 { get; set; }   //	预留
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve11_5 { get; set; }   //	预留
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve11_6 { get; set; }   //	预留
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve11_7 { get; set; }   //	预留
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve11_8 { get; set; }   //	预留
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve11_9 { get; set; }   //	预留
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve11_10 { get; set; }  //	预留
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve11_11 { get; set; }  //	预留
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve11_12 { get; set; }  //	预留
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve11_13 { get; set; }  //	预留
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve11_14 { get; set; }  //	预留
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve11_15 { get; set; }  //	预留
		[field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve11_16 { get; set; }  //	预留
																							//	EL3054-A
		[field: MarshalAs(UnmanagedType.R4)] [BR()] public float Ruler11 { get; set; }  //	位移尺11
		[field: MarshalAs(UnmanagedType.R4)] [BR()] public float Ruler12 { get; set; }  //	位移尺12
		[field: MarshalAs(UnmanagedType.R4)] [BR()] public float Ruler21 { get; set; }  //	位移尺21
		[field: MarshalAs(UnmanagedType.R4)] [BR()] public float Ruler22 { get; set; }  //	位移尺22
																						//	EL3054-B
		[field: MarshalAs(UnmanagedType.R4)] [BR()] public float Uw_EOT { get; set; }   //	放卷卷径测量
		[field: MarshalAs(UnmanagedType.R4)] [BR()] public float Rw_EOT { get; set; }   //	收卷卷径测量
		[field: MarshalAs(UnmanagedType.R4)] [BR()] public float ReserveAi2_3 { get; set; } //	预留
		[field: MarshalAs(UnmanagedType.R4)] [BR()] public float ReserveAi2_4 { get; set; } //	预留
																							//	EL3064-C
		[field: MarshalAs(UnmanagedType.R4)] [BR()] public float Uw_TED { get; set; }   //	放卷寻边传感器
		[field: MarshalAs(UnmanagedType.R4)] [BR()] public float Rw_TED { get; set; }   //	收卷寻边传感器
		[field: MarshalAs(UnmanagedType.R4)] [BR()] public float ReserveAi3_3 { get; set; } //	预留
		[field: MarshalAs(UnmanagedType.R4)] [BR()] public float ReserveAi3_4 { get; set; } //	预留

	}
}