using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIENTITY
{
  public  class RMMoldPutawayReportEntity
    {
        public string AUTOID { get; set; }
        public string FROMDATE { get; set; }
        public string TODATE { get; set; }
        public string USERCODE { get; set; }
    }

    public class RequestRMMoldPutaway
    {
        public RMMoldPutawayReportEntity requestrmmoldputaway { get; set; }
    }
    public class ResponseRMMoldPutaway
    {
        public bool result { get; set; }

        public List<ErrorItem> ErrorContainer { get; set; }
        public DataTable JS_RMMoldPutawayPageload { get; set; }
        public DataTable JS_RMMoldPutawayGenerate { get; set; }

    }

}
