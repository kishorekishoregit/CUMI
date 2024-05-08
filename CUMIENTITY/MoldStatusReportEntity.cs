using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIENTITY
{
    public class MoldStatusReportEntity
    {

        public string AUTOID { get; set; }
        public string MOLDHEALTHTYPE { get; set; }
        public string TODATE { get; set; }
        public string USERCODE { get; set; }

    }

    public class RequestMoldStatusReport
    {
        public MoldStatusReportEntity requestmoldstatusreport { get; set; }
    }
    public class ResponseMoldStatusReport
    {
        public bool result { get; set; }

        public List<ErrorItem> ErrorContainer { get; set; }
        public DataTable JS_MoldstatusPageload { get; set; }
        public DataTable JS_MoldstatusGenerate { get; set; }
        public DataTable JS_type { get; set; }


    }


}
