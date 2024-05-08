using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CUMIENTITY
{
    public class InventoryReportEntity
    {
        public string PLANT { get; set; }
        public string BARCODE { get; set; }
        public string RACKTYPE { get; set; }
        public string RACKCOLOR { get; set; }
        public string RACKSTATUS { get; set; }
        public string TODATE { get; set; }
    }

    public class RequestInventoryReport
    {
        public InventoryReportEntity requestInventoryReport { get; set; }
    }

    public class ResponseInventoryReport
    {
        public bool result { get; set; }


        public List<ErrorItem> ErrorContainer { get; set; }


        public DataTable JS_RackType { get; set; }
        public DataTable JS_RackColor { get; set; }
        public DataTable JS_RackStatus { get; set; }
        public DataTable JS_Barcode { get; set; }
        public DataTable JS_plant { get; set; }
        public DataTable JS_InventoryDetails { get; set; }
    }

}
