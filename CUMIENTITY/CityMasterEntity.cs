using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  System.Data;

namespace CUMIENTITY
{
    public class CityMasterEntity
    {
        public string ZONE { get; set; }
        public string STATE { get; set; }
        public string CITY { get; set; }
        public string STATUS { get; set; }
        public string AUTOID { get; set; }
        public string USERCODE { get; set; }
    }
    public class RequestCityMaster
    {
        public CityMasterEntity requestCityMaster { get; set; }
    }
    public class ResponseCityMaster
    {
        public bool result { get; set; }


        public List<ErrorItem> ErrorContainer { get; set; }


        public DataTable JS_ZONE { get; set; }
        public DataTable JS_STATE { get; set; }
        public DataTable JS_STATUS { get; set; }
        public DataTable JS_Statedetails { get; set; }

    }
}
