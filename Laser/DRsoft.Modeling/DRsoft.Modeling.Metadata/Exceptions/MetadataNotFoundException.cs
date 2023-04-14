using System;
using System.Runtime.Serialization;

namespace DRsoft.Modeling.Metadata.Exceptions
{
    /// <summary>
    /// 元数据没有找到的异常信息
    /// </summary>
    [Serializable]
    public class MetadataNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataNotFoundException"/> class.
        /// </summary>
        public MetadataNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataNotFoundException"/> class.
        /// </summary>
        /// <param name="message">描述错误的消息。</param>
        public MetadataNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataNotFoundException"/> class.
        /// </summary>
        /// <param name="metadataId">The metadata identifier.</param>
        /// <param name="metadataTypeName">Name of the metadata type.</param>
        public MetadataNotFoundException(Guid metadataId, string metadataTypeName)
            : base($"在元数据【{metadataTypeName}】中，没有找到Id为【{metadataId}】的记录。")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataNotFoundException"/> class.
        /// </summary>
        /// <param name="metadataName">Name of the metadata.</param>
        /// <param name="metadataTypeName">Name of the metadata type.</param>
        public MetadataNotFoundException(string metadataName, string metadataTypeName)
            : base($"在元数据【{metadataTypeName}】中，没有找到名称为【{metadataName}】的记录。")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataNotFoundException"/> class.
        /// </summary>
        /// <param name="info"><see cref="T:System.Runtime.Serialization.SerializationInfo" />，它存有有关所引发异常的序列化的对象数据。</param>
        /// <param name="context"><see cref="T:System.Runtime.Serialization.StreamingContext" />，它包含有关源或目标的上下文信息。</param>
        protected MetadataNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
