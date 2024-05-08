using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIENTITY
{
   public class MoldPutawayReportEntity
    {
        public string AUTOID { get; set; }
        public string FROMDATE { get; set; }
        public string TODATE { get; set; }
        public string USERCODE { get; set; }
    }
   
    public class RequestMoldPutawayReport
    {
        public MoldPutawayReportEntity requestmoldputaway { get; set; }
    }
    public class ResponseMoldPutawayReport
    {
        public bool result { get; set; }

        public List<ErrorItem> ErrorContainer { get; set; }
        public DataTable JS_MoldPutawayPageload { get; set; }
        public DataTable JS_MoldPutawayGenerate { get; set; }

    }
}
