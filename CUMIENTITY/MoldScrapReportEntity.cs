using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIENTITY
{
    public class MoldScrapReportEntity
    {
        public string AUTOID {  get; set; }
        public string MOLDITEMCODE {  get; set; }
        public string USERCODE {  get; set; }
    }
    public class RequestMoldScrapReport
    {
        public MoldScrapReportEntity requestmoldscrapreport {  get; set; }
    }
    public class ResponseMoldScrapReport
    {
        public bool result { get; set; }
        public List<ErrorItem> ErrorContainer { get; set; }
        public DataTable JS_MoldScrapPageload { get; set; }
    }
}
