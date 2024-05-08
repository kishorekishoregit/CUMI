using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIENTITY
{
  public class ShotCountReportEntity
    {
        public string AUTOID { get; set; }
        public string RFIDNUMBER { get; set; }
        public string MOLDITEMCODE { get; set; }
        public string FROMDATE { get; set; }
        public string TODATE { get; set; }
        public string USERCODE { get; set; }
    }

    public class RequestShotCountReport
    {
        public ShotCountReportEntity requestshotcountmoldreport { get; set; }
    }
    public class ResponseShotCountReport
    {
        public bool result { get; set; }

        public List<ErrorItem> ErrorContainer { get; set; }
        public DataTable JS_MoldcountreportPageload { get; set; }
        public DataTable JS_MoldcountreportGenerate { get; set; }
        public DataTable JS_moldcode { get; set; }
    }
}
