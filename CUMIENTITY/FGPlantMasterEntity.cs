using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CUMIENTITY
{
    public class FGPlantMasterEntity
    {
        public string AUTOID { get; set; }
        public string PLANTCODE { get; set; }
        public string LOCATION { get; set; }
        public string FGITEMCODE { get; set; }
        public string VARIANTCODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string UOM { get; set; }
        public string Weight { get; set; }
        public string GROUP { get; set; }
        public string ITEMSUBGROUP { get; set; }
        public string CATEGORY { get; set; }
        public string RECORDSTATUS { get; set; }
        public string REMARKS { get; set; }
        public string USERCODE { get; set; }
    }
    public class RequestFGPlantMaster
    {
        public FGPlantMasterEntity requestfgplantmaster { get; set; }
    }
    public class ResponseFGPlantMaster
    {
        public bool result { get; set; }
        public List<ErrorItem> ErrorContainer { get; set; }
        public DataTable JS_FgplantmasterDetails { get; set; }
        public DataTable JS_UOM { get; set; }
        public DataTable JS_Plant { get; set; }
        public DataTable JS_group { get; set; }
        public DataTable JS_Itemsubgroup { get; set; }
        public DataTable JS_Category { get; set; }
        public DataTable JS_Recordstatus { get; set; }
        //public DataTable JS_OrderNo { get; set; }
    }
}
