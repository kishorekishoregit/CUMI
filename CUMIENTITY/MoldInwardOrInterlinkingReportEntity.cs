using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIENTITY
{
    public class MoldInwardOrInterlinkingReportEntity
    {
        public string AUTOID { get; set; }  
        public string FROMDATE { get; set; }
        public string TODATE { get; set; }    
        public string USERCODE { get; set; }
    }
    public class RequestMoldInwardOrInterlinking
    {
        public MoldInwardOrInterlinkingReportEntity requestinwardorinterlinking { get; set; }
    }
    public class ResponseMoldInwardOrInterlinking
    {
        public bool result { get; set; }

        public List<ErrorItem> ErrorContainer { get; set; }
        public DataTable JS_MoldInwardPageload { get; set; }
        public DataTable JS_MoldInwardGenerate { get; set; }


    }

}
