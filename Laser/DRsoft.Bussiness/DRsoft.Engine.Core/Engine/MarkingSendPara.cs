using DRsoft.Engine.Model.Vision;
using DRsoft.Runtime.Core.Platform.Config;

namespace DRsoft.Engine.Model.Controller
{
    public class MarkingSendPara : ConfigEventBase, IConfigExt<MarkingSendPara>
    {
        public int LongMenNum { get; set; } = 1;
        public int ProcessLineNum { get; set; } = 1;
        public LaserPadPosition LaserPadPosition { get; set; } = new LaserPadPosition();
        public double XPos { get; set; } = 0.0;
        public double YPos { get; set; } = 0.0;
        public double APos { get; set; } = 0.0;
        public bool Marking { get; set; } = false;
        public bool ClearFlag { get; set; } = false;
        public bool Changed(MarkingSendPara obj)
        {
            if (obj == null) return false;
            return obj.LongMenNum != LongMenNum ||
                   obj.ProcessLineNum != ProcessLineNum ||
                   obj.LaserPadPosition != LaserPadPosition ||
                   obj.XPos != XPos ||
                   obj.YPos != YPos ||
                   obj.APos != APos ||
                   obj.Marking != Marking ||
                   obj.ClearFlag != ClearFlag;
        }

        public MarkingSendPara Clone()
        {
            return new MarkingSendPara
            {
                LongMenNum = this.LongMenNum,
                ProcessLineNum = this.ProcessLineNum,
                LaserPadPosition = this.LaserPadPosition,
                XPos = this.XPos,
                YPos = this.YPos,
                APos = this.APos,
                Marking = this.Marking,
                ClearFlag = this.ClearFlag
            };
        }
    }

    public class MarkingRecvPara : ConfigEventBase, IConfigExt<MarkingRecvPara>
    {
        public bool[] RecvFlagA { get; set; } = new bool[6];
        public bool[] RecvFlagB { get; set; } = new bool[6];
        public bool Changed(MarkingRecvPara obj)
        {
            if (obj == null) return false;
            return obj.RecvFlagA != RecvFlagA ||
                   obj.RecvFlagB != RecvFlagB;
        }

        public MarkingRecvPara Clone()
        {
            return new MarkingRecvPara
            {
                RecvFlagA = this.RecvFlagA,
                RecvFlagB = this.RecvFlagB
            };
        }
    }

    public class MarkingRecvStatusFeedback : ConfigEventBase, IConfigExt<MarkingRecvStatusFeedback>
    {
        public int[] MarkingStatusAFeedback { get; set; } = new int[6];
        public int[] MarkingStatusBFeedback { get; set; } = new int[6];
        public bool Changed(MarkingRecvStatusFeedback obj)
        {
            if (obj == null) return false;
            return obj.MarkingStatusAFeedback != MarkingStatusAFeedback ||
                   obj.MarkingStatusBFeedback != MarkingStatusBFeedback;
        }

        public MarkingRecvStatusFeedback Clone()
        {
            return new MarkingRecvStatusFeedback
            {
                MarkingStatusAFeedback = this.MarkingStatusAFeedback,
                MarkingStatusBFeedback = this.MarkingStatusBFeedback
            };
        }
    }

}
