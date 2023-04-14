using System;
using System.Xml.Serialization;
using DRsoft.Modeling.Metadata.Tools;

namespace DRsoft.Modeling.Metadata.Status
{
    /// <summary>
    /// 元数据状态对象
    /// </summary>
    [Serializable]
    public class MetadataStatusItem
    {
        /// <summary>
        /// 元数据Id
        /// </summary>
        [XmlAttribute]
        public Guid MetadataId { get; set; }

        /// <summary>
        /// 父级数据Id，不保存到状态文件中，每次展示时自动计算。
        /// </summary>
        [XmlIgnore]
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 展示时分组用，不保存到状态文件中。
        /// </summary>
        [XmlIgnore]
        public int GroupId { get; set; }

        /// <summary>
        /// 表示是否虚拟节点，这种节点为动态计算出来的父级节点，不保存到状态文件中。
        /// </summary>
        [XmlIgnore]
        public bool IsVirtual { get; set; }

        /// <summary>
        /// 表示FunctionPage的预览的Url
        /// </summary>
        [XmlIgnore]
        public string Url { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        [XmlAttribute]
        public MetadataType TypeName { get; set; }

        private string _typeNameText;

        /// <summary>
        /// 类型名称文本
        /// </summary>
        [XmlIgnore]
        public string TypeNameText
        {
            get
            {
                if (_typeNameText == null)
                {
                    return TypeName.GetDescription();
                }
                return _typeNameText;
            }
            set => _typeNameText = value;
        }
        /// <summary>
        /// 控件类型
        /// </summary>
        [XmlAttribute]
        public string ControlType { get; set; }
        /// <summary>
        /// 元数据状态
        /// </summary>
        [XmlAttribute]
        public StatusType Status { get; set; }

        private string _statusText;

        /// <summary>
        /// 元数据状态文本
        /// </summary>
        [XmlIgnore]
        public string StatusText
        {
            get
            {
                if (_statusText == null)
                {
                    return Status.GetDescription();
                }
                return _statusText;
            }
            set => _statusText = value;
        }
        /// <summary>
        /// 修改人
        /// </summary>
        [XmlAttribute]
        public string ModifiedBy { get; set; }

        /// <summary>
        /// 修改人Guid
        /// </summary>
        [XmlAttribute]
        public string ModifiedGuid { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [XmlAttribute]
        public DateTime ModifiedOn { get; set; }
    }
}
