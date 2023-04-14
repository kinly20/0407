using Newtonsoft.Json;

namespace DRsoft.Engine.Model.Vision
{
    public class SendCommand
    {
        [JsonProperty("groupId")]
        public string? GroupId { get; set; } // 组件ID，整版统一

        [JsonProperty("workStationId")]
        public int WorkStationId { get; set; } // 工位ID，1~24

        [JsonProperty("hash")]
        public string? Hash { get; set; } // 请求唯一 hash


        [JsonProperty("command")] //执行的命令 （激光焊接定位 : pad_position）
        public string? Command { get; set; }
    }
}
