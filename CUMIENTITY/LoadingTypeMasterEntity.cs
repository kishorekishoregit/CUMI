using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CUMIENTITY
{
    public class LoadingTypeMasterEntity
    {

        public string LOADINGTYPE { get; set; }
        public string STATUS { get; set; }
        public string AUTOID { get; set; }
        public string USERCODE { get; set; }
    }
    public class RequestLoadingTypeMaster
    {
        public LoadingTypeMasterEntity requestLoadingTypeMaster { get; set; }
    }
    public class ResponseLoadingTypeMaster
    {
        public bool result { get; set; }


        public List<ErrorItem> ErrorContainer { get; set; }



        public DataTable JS_STATUS { get; set; }
        public DataTable JS_loadingtypedetails { get; set; }

    }
}
