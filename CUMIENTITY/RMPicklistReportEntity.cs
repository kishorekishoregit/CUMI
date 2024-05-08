using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIENTITY
{
    public class RMPicklistReportEntity
    {
        public string AUTOID {  get; set; }
        public string PICKLISTNO {  get; set; }
        public string USERCODE {  get; set; }
    }
    public class RequestRMPicklistReport
    {
        public RMPicklistReportEntity requestrmpicklistreport { get; set; }
    }
    public class ResponseRMPicklistReport
    {
        public bool result { get; set; }
        public List<ErrorItem> ErrorContainer { get; set; }
        public DataTable JS_RMPicklistdetails { get; set; }
        public DataTable JS_RMPicklistheader {  get; set; }
        public DataTable JS_RMpicklistpageload { get; set; }
    }
}
