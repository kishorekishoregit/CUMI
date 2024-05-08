using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIENTITY
{
    public class SupplierMasterEntity
    {
        public string AUTOID { get; set; }
        public string SUPPLIERCODE {  get; set; }   
        public string SUPPLIERNAME {  get; set; }   
        public string ADDRESS {  get; set; }
        public string RECORDSTATUS {  get; set; }
        public string USERCODE {  get; set; }   
    }
    public class RequestSupplierMaster
    {
        public SupplierMasterEntity requestsuppliermaster { get; set; }
    }
    public class ResponseSupplierMaster
    {
        public bool result { get; set; }
        public List<ErrorItem> ErrorContainer { get; set; }
        public DataTable Js_SupplierDts { get; set; }
        public DataTable Js_Status { get; set; }
    }
}
