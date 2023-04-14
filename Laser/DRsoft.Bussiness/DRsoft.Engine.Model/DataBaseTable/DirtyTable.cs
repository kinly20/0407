using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRsoft.Engine.Model.DataBaseTable
{
    /// <summary>
    /// 硅胶膜缺陷表
    /// </summary>
    [Table("DirtyTable")]
    public class DirtyTable
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
        /// 机台ID
        /// </summary>
        [Required]
        public string? MachineId
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
        /// 是否存在脏污
        /// </summary>
        [Required]
        public string? IsDirty
        {
            set;
            get;
        }
        /// <summary>
        /// 缺陷矩形中心x坐标值
        /// </summary>
        [Required]
        public double DirtyX
        {
            set;
            get;
        }
        /// <summary>
        /// 缺陷矩形中心y坐标值
        /// </summary>
        [Required]
        public double DirtyY
        {
            set;
            get;
        }
        /// <summary>
        /// 缺陷矩形宽度
        /// </summary>
        [Required]
        public double DirtyWidth
        {
            set;
            get;
        }
        /// <summary>
        /// 缺陷矩形高度
        /// </summary>
        [Required]
        public double DirtyHeight
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
