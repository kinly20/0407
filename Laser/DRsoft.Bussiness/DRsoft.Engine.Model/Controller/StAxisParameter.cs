using System.Runtime.InteropServices;

namespace DRsoft.Engine.Model.Controller
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class StAxisParameter
    {
        /// <summary>
        /// 回零偏移
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float HomeOffset { get; set; }

        /// <summary>
        /// 相对距离
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float RelDistance { get; set; }

        /// <summary>
        /// 绝对位置1
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float AbsPosition1 { get; set; }

        /// <summary>
        /// 绝对位置2
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float AbsPosition2 { get; set; }

        /// <summary>
        /// 回零速度
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float HomeVelo { get; set; }

        /// <summary>
        /// 手动速度
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float ManualVelo { get; set; }

        /// <summary>
        /// 工作速度
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float WorkVelo { get; set; }

        /// <summary>
        /// 加速度
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Acc { get; set; }

        /// <summary>
        /// 减速度
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)]
        [field: BR()]
        public float Dec { get; set; }


        public bool Changed(StAxisParameter obj)
        {
            if (obj == null) return false;
            return HomeOffset != obj.HomeOffset ||
                   RelDistance != obj.RelDistance ||
                   AbsPosition1 != obj.AbsPosition1 ||
                   AbsPosition2 != obj.AbsPosition2 ||
                   HomeVelo != obj.HomeVelo ||
                   ManualVelo != obj.ManualVelo ||
                   WorkVelo != obj.WorkVelo ||
                   Acc != obj.Acc ||
                   Dec != obj.Dec;
        }

        public StAxisParameter Clone()
        {
            return new StAxisParameter
            {
                HomeOffset = this.HomeOffset,
                RelDistance = this.RelDistance,
                AbsPosition1 = this.AbsPosition1,
                AbsPosition2 = this.AbsPosition2,
                HomeVelo = this.HomeVelo,
                ManualVelo = this.ManualVelo,
                WorkVelo = this.WorkVelo,
                Acc = this.Acc,
                Dec = this.Dec
            };
        }
    }
}
