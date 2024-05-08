using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIENTITY
{
    public class RMIwardReportEntity
    {
        public string AUTOID { get; set; }
        public string FROMDATE { get; set; }
        public string TODATE { get; set; }
        public string USERCODE { get; set; }

    }
    public class RequestRMIwardReport
    {
        public RMIwardReportEntity requestrminward { get; set; }
    }
    public class ResponseRMInwardReport
    {
        public bool result { get; set; }
        public List<ErrorItem> ErrorContainere { get; set; }
        public DataTable JS_RMInwardPageload { get; set; }
        public DataTable JS_RMInwardGenerate { get; set; }
    }
}
