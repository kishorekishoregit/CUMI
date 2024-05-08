using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CUMIENTITY
{
   public class RMMasterEntity
    {
        public string AUTOID { get; set; }
        public string PLANTCODE { get; set; }
        public string LOCATION { get; set; }
        public string RMITEMCODE { get; set; }
        public string VARIANTCODE { get; set; }
        public string RMDESCRIPTION { get; set; }
        public string UOM { get; set; }
        public string PACKSIZE { get; set; }
        public string GROUP { get; set; }
        public string CATEGORY { get; set; }
        public string ITEMSUBGROUP { get; set; }
        public string RECORDSTATUS { get; set; }
        public string REMARKS { get; set; }
        public string USERCODE { get; set; }
    }

    public class RequestRMMaster
    {
        public RMMasterEntity requestrmmaster { get; set; }
    }
    public class ResponseRMMaster
    {
        public bool result { get; set; }
        public List<ErrorItem> ErrorContainer { get; set; }

        public DataTable JS_Plant { get; set; }
        public DataTable JS_UOM { get; set; }
        public DataTable JS_ITEMSUBGROUP { get; set; }

        public DataTable JS_Group { get; set; }
        public DataTable JS_CATEGORY { get; set; }
        public DataTable JS_Recordstatus { get; set; }
        //public DataTable JS_OrderNo { get; set; }
        public DataTable JS_rmmasterDetails { get; set; }
    
    }
}
