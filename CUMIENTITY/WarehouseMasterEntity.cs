using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CUMIENTITY
{
   public class WarehouseMasterEntity
    {
        public string WAREHOUSECODE { get; set; }
        public string WAREHOUSENAME { get; set; }
        public string PLANTCODE { get; set; }
        public string RECORDSTATUS { get; set; }
        public string SUPERVISOR { get; set; }
        public string ADDRESS1 { get; set; }
        public string ADDRESS2 { get; set; }
        public string ADDRESS3 { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string PINCODE { get; set; }
        public string REMARKS { get; set; }
        public string AUTOID { get; set; }
        public string USERCODE { get; set; }
    }
    public class RequestWarehouseMaster
    {
        public WarehouseMasterEntity requestWarehouseMaster { get; set; }
    }
    public class ResponseWarehouseMaster
    {
        public bool result { get; set; }
        public List<ErrorItem> ErrorContainer { get; set; }
        public DataTable JS_Plant { get; set; }
        public DataTable JS_STATUS { get; set; }
        public DataTable JS_Warehousedetails { get; set; }

    }
}
