using DRsoft.Runtime.Core.Platform.Exceptions;
using System;
using System.Runtime.Serialization;

namespace DRsoft.Modeling.Metadata.Exceptions
{
    /// <summary>
    /// 元数据锁定异常
    /// </summary>
    [Serializable]
    public class MetadataLockedException : PlatformException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataLockedException"/> class.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public MetadataLockedException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataLockedException"/> class.
        /// </summary>
        /// <param name="message">描述错误的消息。</param>
        public MetadataLockedException(string message)
            : base(message)
        {
        }
    }
}
