using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CUMIENTITY
{
    public class ProductionOrderFileUploadEntity
    {
        public string AUTOID { get; set; }
        public string PRODUCTIONORDERNO { get; set; }
        public string ORDERDATE { get; set; }
        public string CUMIREFORDERNO { get; set; }
        public string CUSTITEMCODE { get; set; }
        public string CUSTITEMNAME { get; set; }
        public string UOM { get; set; }
        public string ORDERQTY { get; set; }
        public string VARIANT { set; get; }
        public string USERCODE { get; set; }
    }
    public class ProductionOrderFileUploadDetailsEntity
    {
        public string AUTOID { get; set; }
        public string PRODUCTIONORDERNO { get; set; }
        public string ORDERDATE { get; set; }
        public string CUMIREFORDERNO { get; set; }
        public string CUSTITEMCODE { get; set; }
        public string CUSTITEMNAME { get; set; }
        public string UOM { get; set; }
        public string ORDERQTY { get; set; }
        public string VARIANT { set; get; }
        public string USERCODE { set; get; }
    }
    public class RequestProductionOrderFileUpload
    {
        public ProductionOrderFileUploadEntity requestproductionorderfileupload { get; set; }
        public List<ProductionOrderFileUploadDetailsEntity> requestproductionfileuploaddetails { get; set; }
    }
    public class ResponseProductionOrderFileUpload
    {
        public bool result { get; set; }
        public string message { get; set; }
        public List<ErrorItem> ErrorConatiner { get; set; }
        public DataTable JS_ProductionOrderDts { get; set; }
    }
}
