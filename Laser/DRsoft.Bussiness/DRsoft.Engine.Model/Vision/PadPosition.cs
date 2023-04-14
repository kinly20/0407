//using System;
//using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DRsoft.Engine.Model.Vision
{
    // 激光焊接 Pad 点坐标
    public partial class LaserPadPosition
    {
        [JsonProperty("groupId")]
        public string? GroupId { get; set; } // 组件ID，整版统一

        [JsonProperty("workStationId")]
        public int WorkStationId { get; set; } // 工位ID，1~24

        [JsonProperty("hash")]
        public string? Hash { get; set; } // 请求唯一 hash

        [JsonProperty("command")] //执行的命令 （激光焊接定位 : pad_position）
        public string? Command { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("status_code")]
        public long StatusCode { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        // 每个片子上的 Pad 点坐标，该数组长度为 6
        [JsonProperty("solder_tapes_group_list")]
        public SolderTapesGroupList[]? SolderTapesGroupList { get; set; }

        // 返回类型：
        // take_photo: 抓拍完成
        // pad_position: Pad 点坐标，此时 SolderTapesGroupList.SolderTapes 有值
        // silica_gel_status: 硅胶状态，此时 SolderTapesGroupList，PadOverSiDirtys 有值
        [JsonProperty("response_type")]
        public string? ResponseType { get; set; }

        [JsonProperty("time_cost")]
        public double TimeCost { get; set; } // 抓拍和定位所有片子总耗时

        [JsonProperty("snap_cost")]
        public double SnapCost { get; set; } // 抓拍耗时

        [JsonProperty("find_pad_cost")]
        public double FindPadCost { get; set; } // 计算耗时

        [JsonProperty("find_si_dirty_spots_cost")]
        public double FindSiDirtySpotsCost { get; set; } // 查找硅胶脏污耗时


        public const string KResponseType_TakePhoto = "take_photo";
        public const string KResponseType_PadPosition = "pad_position";
        public const string KResponseType_SilicaGelStatus = "silica_gel_status";

        public void CleanData()
        {
            GroupId = null;
            WorkStationId = 0;// 工位ID，1~24
            Hash = null;
            Command = null;
            Status = null;
            StatusCode = 0;
            Message = null;
            SolderTapesGroupList = null;
            ResponseType = null;
            TimeCost = 0;
            SnapCost = 0;
            FindPadCost = 0;
            FindSiDirtySpotsCost = 0;

        }
    }

    public partial class SolderTapesGroupList
    {
        [JsonProperty("wafer_id")]
        public long WaferId { get; set; } // 片子ID，1~6

        [JsonProperty("status")]
        public string? Status { get; set; } // 定位状态文字

        [JsonProperty("status_code")]
        public long StatusCode { get; set; } // 定位状态码

        [JsonProperty("type_name")]
        public string? TypeName { get; set; } // 片子类型，起始、常规、中间前、中间后、结束，可忽略

        [JsonProperty("type")]
        public long Type { get; set; } // 片子类型，可忽略

        [JsonProperty("message")]
        public string? Message { get; set; } // 如果NG，会有文字提示

        [JsonProperty("solder_tapes")] public SolderTape[]? SolderTapes { get; set; } // 需要焊的点坐标，长度根据实际情况而定

        [JsonProperty("flow_together_tapes")] public SolderTape[]? FlowTogetherTapes { get; set; } // 汇流焊的点坐标，仅 ResponseType == "pad_position" 且工位处于起始、结束位时时有值

        [JsonProperty("pad_over_si_dirtys")]
        public PadOverSiDirty[]? PadOverSiDirtys { get; set; } // Pad 上方是否有脏污，数组长度与 SolderTapes 相等，仅 ResponseType == "silica_gel_status" 时有值

        [JsonProperty("snap_cost")]
        public double SnapCost { get; set; } // 这一片抓拍耗时，单位秒

        [JsonProperty("calc_cost")]
        public double CalcCost { get; set; } // 这一片计算耗时，单位秒
    }

    public partial class PadOverSiDirty
    {
        [JsonProperty("is_dirty")]
        public bool IsDirty { get; set; } // 当前这组的 3 个 Pad 点上方是否有脏污

        [JsonProperty("current_pad_dirty_spots")]
        public DirtySpot[]? CurrentPadDirtySpots { get; set; } //当前这组的脏污坐标，长度不定，没有则长度为 0

        [JsonProperty("neighbour_dirty_spots")]
        public DirtySpot[][]? NeighbourDirtySpots { get; set; } // 当前这组隔壁 ±7mm 的范围内是否有干净的地方，可供重复利用，每 2mm 一组，数组长度为 7
    }

    public partial class DirtySpot
    {
        [JsonProperty("x")]
        public double X { get; set; } // 脏污坐标

        [JsonProperty("y")]
        public double Y { get; set; } // 脏污坐标

        [JsonProperty("width")]
        public double Width { get; set; } // 脏污宽

        [JsonProperty("height")]
        public double Height { get; set; } // 脏污高
    }

    public partial class SolderTape
    {
        [JsonProperty("x")]
        public double X { get; set; } // Pad 点坐标

        [JsonProperty("y")]
        public double Y { get; set; } // Pad 点坐标

        [JsonProperty("a")]
        public double A { get; set; } // Pad 点所在焊带的角度，分光情况下忽略
    }



    public partial class LaserPadPosition
    {
        public static LaserPadPosition FromJson(string json)
        {
            return JsonConvert.DeserializeObject<LaserPadPosition>(json, Converter.Settings);
        }
    }

    public static class Serialize
    {
        public static string ToJson(this LaserPadPosition self)
        {
            return JsonConvert.SerializeObject(self, Converter.Settings);
        }

        public static string ToJson(this SendCommand self)
        {
            return JsonConvert.SerializeObject(self, Converter.Settings);
        }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Formatting = Formatting.Indented,
            Converters = { new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal } }
        };
    }
    /*
    public partial class SendCommand
    {

        [JsonProperty("groupId")]
        public string? GroupId { get; set; } // 组件ID，整版统一

        [JsonProperty("workStationId")]
        public int  WorkStationId { get; set; } // 工位ID，1~24

        [JsonProperty("hash")]
        public string? Hash { get; set; } // 请求唯一 hash


        [JsonProperty("command")] //执行的命令 （激光焊接定位 : pad_position）
        public string? Command { get; set; }
    
    }*/

}
