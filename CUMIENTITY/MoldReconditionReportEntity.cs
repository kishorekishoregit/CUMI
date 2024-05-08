using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIENTITY
{
    public class MoldReconditionReportEntity
    {
        public string AUTOID {  get; set; }
        public string MOLDITEMCODE {  get; set; }
        public string USERCODE {  get; set; }
    }
    public class RequestMoldReconditionReport
    {
        public MoldReconditionReportEntity requestmoldrecondition {  get; set; }
    }
    public class ResponseMoldReconditionReport
    {
        public bool result { get; set; }
        public List<ErrorItem> ErrorContainer { get; set; }
        public DataTable JS_MoldReconditionDts { get; set; }
    }
}
