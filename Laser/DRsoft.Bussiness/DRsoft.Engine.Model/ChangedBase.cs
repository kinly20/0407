using System.Collections.Generic;

namespace DRsoft.Engine.Model
{
    public class ChangedBase<T>
    {
        public virtual bool Changed(T obj)
        {
            return true;
        }      
    }
    public class CompareBase
    {
        public bool Compare<T>(List<T> d1, List<T> d2) where T : ChangedBase<T>
        {
            if(d1==null && d2==null) return false;
            if((d1==null && d2!=null)||(d1!=null && d2 ==null)) return true;
            if (d1.Count != d2.Count)
                return true;
            for (var ri = 0; ri < d1.Count; ri++)
            {
                if (d1[ri].Changed(d2[ri]))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
