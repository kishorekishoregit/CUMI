using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIENTITY
{
    public  class GRNEntryEntity
    {
        public string AUTOID {  get; set; }
        public string GRNNO { get; set; }
        public string GRNDATE { get; set; }
            
        public string SUPPLIER { get; set; }
        public string PONO { get; set; }
        public string INVOICENO { get; set; }
        public string USERCODE { get; set; }
        public string ITEMCODE { get; set; }

    }
    public class GRNEntryDetailsEntity
    {
        public string GRNNO { get; set; }
        public string RMITEMCODE { get; set;}
        public string RMITEMNAME { get; set;}
        public string VARIANT { get; set;}
        public string UOM { get; set;}
        public string QUANTITY { get; set;}
    }
    public class RequestGRNEntry
    {
        public GRNEntryEntity requestgrnentry { get; set; }
        public List<GRNEntryDetailsEntity> requestgrnentrydetails { get; set; }

    }
    public class ResponseGRNEntry
    {
        public bool result { get; set; }
        public List<ErrorItem> ErrorContainer { get; set; }
        public DataTable JS_RMItemcode { get; set; }
        public DataTable JS_GRNEnrtyDetails { get; set; }
        public DataTable JS_ItemDetails { get; set; }
        public DataTable JS_GRNpageload { get; set; }

    }

}
