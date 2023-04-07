using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;

namespace IntelligentScrewing
{
    class AFurl
    {
         public RFService rf = new RFService();
         string ifid = "BD0D45B1-5D0D-43A8-84C7-E98066507C99";
         DateTime date;
         public Result res = new Result();
         public SNInfo sninfo = new SNInfo();
         public List<Fault> faults = new List<Fault>();
       
        public Result SendData()
        {
            
            res = rf.SaveSNInfo(ifid, sninfo, faults.ToArray());
            return res;
            
        }

    }
}
