using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CUMIENTITY
{
   public class MoldMasterEntity
    {
        public string AUTOID { get; set; }
        public string PLANTCODE { get; set; }
        public string LOCATION { get; set; }
        public string MOLDTYPE { get; set; }
        public string MOLDITEMCODE { get; set; }

        public string MOLDDESCRIPTION { get; set; }
        public string PONUMBER { get; set; }
        public string BATCHNUMBER { get; set; }
        public string SUPPLIERCODE { get; set; }
        public string SUPPLIERNAME { get; set; }
        public string SUPPLIERADDRESS { get; set; }
        public string VARIANTCODE { get; set; }       
        public string UOM { get; set; }     
        public string GROUP { get; set; }
        public string ITEMSUBGROUP { get; set; }
        public string CATEGORY { get; set; }
        public string RECORDSTATUS { get; set; }
        public string REMARKS { get; set; }
        public string USERCODE { get; set; }
    }
    public class RequestMoldMaster
    {
        public MoldMasterEntity requestrmoldmaster { get; set; }
    }
    public class ResponseMoldMaster
    {
        public bool result { get; set; }
        public List<ErrorItem> ErrorContainer { get; set; }

        public DataTable JS_Plant { get; set; }
        public DataTable JS_UOM { get; set; }
        public DataTable JS_Recordstatus { get; set; }
        //public DataTable JS_OrderNo { get; set; }
        public DataTable JS_Itemsubgroup { get; set; }
        public DataTable JS_Category { get; set; }
        public DataTable JS_group { get; set; }
        public DataTable JS_moldmasterDetails { get; set; }
        public DataTable JS_Moldtype { get; set; }

    }
}
