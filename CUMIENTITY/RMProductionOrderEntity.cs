using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace CUMIENTITY
{
    public class RMProductionOrderEntity
    {
        public string AUTOID { get; set; }
        public string PRODUCTIONORDERNO { get; set;}
        public string RECEIVEDDATE { get; set;}
        public string WAREHOUSEPICKER { get; set;}
        public string USERCODE { get; set; }


    }
    public class RMProductionOrderDetailsEntity
    {
        public string PRODUCTIONORDERNO { get; set;}
        public string ITEMCODE { get; set;}
        public string ITEMNAME { get; set; }
        public string QUANTITY { get; set; }
        public string UOM { get; set; }

    }
    public class RequestRMProductionOrder
    {
        public RMProductionOrderEntity requestrmproductionorder { get; set; }
        public List<RMProductionOrderDetailsEntity> requestrmproductionorderdts { get; set; }

    }
    public class ResponseRMProductionOrder
    {
        public DataTable JS_ProductioNo { get; set; }
        public DataTable JS_RMProductionDts { get; set; }
        public DataTable JS_RMProductionPageload { get; set; }
        public DataTable JS_RMProductionOrderView { get; set; }
        public bool result { get; set; }
        public List<ErrorItem> ErrorContainer { get; set; }
    }
}
