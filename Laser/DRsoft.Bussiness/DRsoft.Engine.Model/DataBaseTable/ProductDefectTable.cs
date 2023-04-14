using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRsoft.Engine.Model.DataBaseTable
{
    /// <summary>
    /// 串焊产品加工后有问题的产品表
    /// </summary>
    [Table("ProductDefectTable")]
    public class ProductDefectTable
    {
        /// <summary>
        /// 主键ID  GUID
        /// </summary>
        [Key]
        public string? id
        {
            get;
            set;
        }
        /// <summary>
        /// 组件ID
        /// </summary>
        [Required]
        public string? GroupId
        {
            set;
            get;
        }
        /// <summary>
        /// 硅胶膜ID
        /// </summary>
        [Required]
        public string? SilicaId
        {
            set;
            get;
        }
        /// <summary>
        /// 工位1~12
        /// </summary>
        [Required]
        public int WorkStationId
        {
            set;
            get;
        }
        /// <summary>
        /// 激光器编号1~12
        /// </summary>
        [Required]
        public int LaserId
        {
            set;
            get;
        }
        /// <summary>
        /// 产品焊接X坐标
        /// </summary>
        [Required]
        public double PadX
        {
            set;
            get;
        }
        /// <summary>
        /// 产品焊接Y坐标
        /// </summary>
        [Required]
        public double PadY
        {
            set;
            get;
        }
        /// <summary>
        /// 加工时间
        /// </summary>
        [Required]
        public DateTime Time
        {
            set;
            get;
        }
    }
}
