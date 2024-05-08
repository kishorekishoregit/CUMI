using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIENTITY
{
    public class RDCPicklistAssignEntity
    {
        public string AUTOID { get; set; }
        public string LOCATION { get; set; }
        public string RDCNO { get; set; }
        public string DATE { get; set; }
        public string MOLDITEMCODE { get; set; }
        public string WAREHOUSEPICKER { get; set; }
        public string RFIDNO { get; set; }
        public string SUPPLIER { get; set; }
        public string USERCODE { get; set; }
    }

    public class RDCPicklistAssignDetailsEntity
    {
        public string RDCNO { get; set; }
        public string DATE { get; set; }
        public string ITEMCODE { get; set; }
        public string ITEMNAME { get; set; }
        public string SUPPLIER { get; set; }
        public string REMARKS { get; set; }
        public string RFIDNO { get; set; }
        public string UOM { get; set; }
        public string QUANTITY { get; set; }
        public string USERCODE { get; set; }
    }
    public class RequestRDCPicklistAssign
    {
        public RDCPicklistAssignEntity requestrdcheaderdetails { get; set; }

        public List<RDCPicklistAssignDetailsEntity> requestrdcassigndetails { get; set; }
    }
    public class ResponseRDCPicklistAssign
    {
        public bool result { get; set; }
        public List<ErrorItem> ErrorContainer { get; set; }

        public DataTable JS_warehousepicker { get; set; }
        public DataTable JS_location { get; set; }
        public DataTable JS_RDCdetails { get; set; }
        public DataTable JS_Supplier { get; set; }
        public DataTable JS_ERPdts { get; set; }

    }
}