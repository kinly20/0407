using System.Runtime.InteropServices;

namespace DRsoft.Engine.Model.Controller
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class StAxis_CMD
    {
        public StAxisCommand sT_AxisCommand = new StAxisCommand();
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class StAxis_Par
    {
        public StAxisParameter sT_AxisParameter { get; set; }  = new StAxisParameter();

        public bool Changed(StAxis_Par obj)
        {
            if (obj == null) return false;
            return sT_AxisParameter != obj.sT_AxisParameter;
        }

        public StAxis_Par Clone()
        {
            return new StAxis_Par
            {
                sT_AxisParameter = this.sT_AxisParameter
            };
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class StAxis_Status
    {
        public StAxisStatus sT_AxisStatus = new StAxisStatus();
    }


}
