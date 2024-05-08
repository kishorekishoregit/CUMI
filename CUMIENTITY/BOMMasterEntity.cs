using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIENTITY
{
    public class BOMMasterEntity
    {
        public string AUTOID {  get; set; }
        public string FGITEMCODE { get; set; }
        public string FGITEMNAME { get; set; }
        public string VARIANT { get; set; }
        public string MOLDITEMCODE { get; set; }
        public string ASSEMBLYID { get; set; }
        public string MOLDITEMNAME { get; set; }
        public string USERCODE { get; set; }
      
    }
    public class BOMMasterDetailsEntity
    {
        public string FGITEMCODE { get; set; }
        public string MOLDITEMCODE { get; set; }
        public string MOLDITEMNAME { get; set; }
        public string VARIANT { get; set; }
        public string QUANTITY { get; set; }
    }
    public class RequestBOMMaster
    {
        public BOMMasterEntity requestbommaster { get; set; }
        public List<BOMMasterDetailsEntity> requestbommasterdetails { get; set; }
    }
    public class ResponseBOMMaster
    {
        public bool result { get; set; }
        public DataTable JS_FGITEMCODE { get; set; }
        public DataTable JS_MOLDITEMCODE { get; set; }
        public DataTable JS_VARIANT {  get; set; }
        public DataTable JS_MOLDITEMNAME {  get; set; }
        public DataTable JS_FGITEMNAME { get;set; }
        public List<ErrorItem> ErrorContainer { get; set; }
        public DataTable JS_BOMMASTERDETAILS { get; set; }  
        public DataTable JS_BOMMASTERHEADER { get; set; }  
        public DataTable JS_BOMMASTERVIEW { get; set; }  
        public DataTable JS_DetailsVariant { get; set; }
    }
}
