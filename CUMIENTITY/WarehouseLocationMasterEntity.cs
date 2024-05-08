using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CUMIENTITY
{
   public class WarehouseLocationMasterEntity
    {
        public string PLANTCODE { get; set; }
        public string WAREHOUSECODE { get; set; }
        public string WAREHOUSE { get; set; }
        public string LOCATIONCODE { get; set; }
        public string LOCATION { get; set; }
        public string RECORDSTATUS { get; set; }
        public string REMARKS { get; set; }
        public string AUTOID { get; set; }
        public string USERCODE { get; set; }
    }
        public class RequestWarehouseLocationMaster
    {
        public WarehouseLocationMasterEntity requestWarehouseLocationMaster { get; set; }
    }
    public class ResponseWarehouseLocationMaster
    {
        public bool result { get; set; }
        public List<ErrorItem> ErrorContainer { get; set; }
        public DataTable JS_Plant { get; set; }
        public DataTable JS_Warehouse { get; set; }
        public DataTable JS_Locationdetails { get; set; }
        public DataTable JS_PrintView { get; set; }

    }
}

