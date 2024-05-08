using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CUMIENTITY
{
  public  class ChildPartMasterEntity
    {
        public string AUTOID { get; set; }
        public string PLANTCODE { get; set; }
        public string LOCATION { get; set; }
        public string CHILDITEMCODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string UOM { get; set; }
        public string QUANTITY { get; set; } 
        public string RECORDSTATUS { get; set; }
        public string USERCODE { get; set; }
    }

    public class RequestChildPartMaster
    {
        public ChildPartMasterEntity requestchildpartmaster { get; set; }
    }
    public class ResponseChildPartMaster
    {
        public bool result { get; set; }
        public List<ErrorItem> ErrorContainer { get; set; }
        public DataTable JS_ChildpartmasterDetails { get; set; }
        public DataTable JS_UOM { get; set; }
        public DataTable JS_Plant { get; set; }
        public DataTable JS_Recordstatus { get; set; }
        //public DataTable JS_OrderNo { get; set; }
    }
}
