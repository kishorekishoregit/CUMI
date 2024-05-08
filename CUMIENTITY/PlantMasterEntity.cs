using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CUMIENTITY
{
   public class PlantMasterEntity
    {
        public string AUTOID { get; set; }
        public string PLANTCODE { get; set; }
        public string PLANTNAME { get; set; }
        public string RECORDSTATUS { get; set; }
        public string REMARKS { get; set; }
        public string USERCODE { get; set; }
    }

    public class RequestPlantMaster
    {
        public PlantMasterEntity requestPlantMaster { get; set; }
    }

    public class ResponsePlantMaster
    {
        public bool result { get; set; }


        public List<ErrorItem> ErrorContainer { get; set; }


        public DataTable JS_PlantDetails { get; set; }

        public DataTable JS_RecordStatus { get; set; }
    }
}
