using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIENTITY
{
    public class MoldPicklistReportEntity
    {
        public string AUTOID {  get; set; }
        public string PICKLISTNO {  get; set; }
        public string USERCODE {  get; set; }   
    }
    public class RequestMoldPicklist
    {
        public MoldPicklistReportEntity requestmoldpicklist { get; set; }
    }
    public class ResponseMoldPicklist
    {
        public bool result { get; set; }
        public DataTable JS_Moldpicklistpageload { get; set; }
        public DataTable JS_MoldpicklistHeader {  get; set; }
        public DataTable JS_MoldpicklistDetails {  get; set; }
        public List<ErrorItem> ErrorContainer { get; set; }
    }
}
