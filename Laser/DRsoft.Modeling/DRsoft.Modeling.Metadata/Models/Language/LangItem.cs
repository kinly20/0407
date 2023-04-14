using Newtonsoft.Json;

namespace DRsoft.Modeling.Metadata.Models.Language
{
    /// <summary>
    /// 
    /// </summary>
    public class LangItem
    {
        /// <summary>
        /// ctor
        /// </summary>
        public LangItem(string id, string parent, string name, string type, bool isVirtual = false, string key = "", string value = "")
        {
            this.Id = id;
            this.Parent = parent;
            this.Name = name;
            this.Type = type;
            if (string.IsNullOrEmpty(key))
            {
                key = id;
            }
            this.Key = key;
            this.Value = value;
            if (string.IsNullOrEmpty(name))
            {
                isVirtual = true;
            }
            this.IsVirtual = isVirtual;

        }
        /// <summary>
        /// 标识
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        [JsonProperty("parent")]
        public string Parent { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }


        /// <summary>
        /// 类型
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }


        /// <summary>
        /// 键
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }


        /// <summary>
        /// 翻译后的文本
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// 是否只显示
        /// </summary>
        [JsonProperty("isVirtual")]
        public bool IsVirtual { get; set; }

        /// <summary>
        /// 所属 0 页面 1 模块 2 业务参数 3 自定义
        /// </summary>
        [JsonProperty("mode")]
        public int Mode { get; set; }
    }
}
