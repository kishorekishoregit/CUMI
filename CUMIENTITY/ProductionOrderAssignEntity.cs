using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CUMIENTITY
{
   public class ProductionOrderAssignEntity
    {
        public string AUTOID { get; set; }   
        public string LOCATION { get; set; }
        public string PRODUCTIONORDERNO { get; set; }
        public string DATE { get; set; }
        public string MOLDITEMCODE { get; set; }
        public string MOLDITEMNAME { get; set; }
        public string MOLDVARIANT { get; set; }
        public string WAREHOUSEPICKER { get; set; }
        public string FGITEMCODE { get; set; }
        public string PRODUCTIONQTY { get; set; }
        public string ASSEMBLYQTY { get; set; }
        public string VARIANT {  get; set; }
        public string ASSEMBLYID {  get; set; }
        public string USERCODE { get; set; }
    }

    public class ProductionOrderAssignDetailsEntity
    {
        public string PRODUCTIONORDERNO { get; set; }
        public string PRODUCTIONORDERDATE { get; set; }
        public string MOLDITEMCODE { get;set; }
        public string MOLDITEMNAME { get; set; }
        public string MOLDVARIANT {  get; set; }
        public string FGITEMCODE { get; set; }
        public string FGVARIANT { get; set; }

        public string PRODUCTIONQTY { get; set; }
        public string ASSEMBLYID { get; set; }
        public string ASSEMBLYQTY {  get; set; }
        
        public string USERCODE { get; set; }
    }
    public class RequestProductionOrderAssign
    {
        public ProductionOrderAssignEntity requestproductionordernoheaderdetails { get; set; }

        public List<ProductionOrderAssignDetailsEntity> requestProductionOrderAssigndetails { get; set; }
        public List<ProductionOrderAssignEntity> requestProductionOrderAssigndetailsss { get; set; }
    }
    public class ResponseProductionOrderAssign
    {
        public bool result { get; set; }
        public List<ErrorItem> ErrorContainer { get; set; }
        
        public DataTable JS_warehousepicker { get; set; }
        public DataTable JS_location { get; set; }
        public DataTable JS_Productiondetails { get; set; }
        public DataTable JS_ProductionERPDts { get; set; }
        public DataTable JS_AssemblyID { get; set; }
        public DataTable JS_ProductionDtsAssign { get; set; }
       
    }
}
