using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CUMIENTITY
{
 public class MoldRFIDAssignmentEntity
    {
        public string AUTOID { get; set; }
        public string PLANTCODE { get; set; }
        public string LOCATION { get; set; }
       // public string FGITEMCODE { get; set; }
        public string MOLDITEMCODE { get; set; }
        public string RECORDSTATUS { get; set; }
        public string RFIDNUMBER { get; set; }
        public string REMARKS { get; set; }
        public string VARIENTCODE { get; set; }
        public string MOLDOPENCOUNT { get; set; }
        public string PREVIOUSMRCOUNT { get; set; }
        public string PREVIUOSMRDATE { get; set; }
        public string MRALERTCOUNT { get; set; }
        public string MRCOUNT { get; set; }
        public string PONUMBER { get; set; }
        public string BATCHNUMBER { get; set; }
        public string MOLDLIFECOUNT { get; set; }
        public string USERCODE { get; set; }
        public string SUPPLIERID { get; set; }
        public string MOLDITEMNAME { get; set; }
        public string UOM { get; set; }
    }
    public class RequestMoldRFIDAssignment
    {
        public MoldRFIDAssignmentEntity requestMoldRFIDAssignment { get; set; }
        //public List<InterlinkingMasterDetailsEntity> requestInterlinkingMasterDetails { get; set; }

    }

    public class ResponseMoldRFIDAssignment
    {
        public bool result { get; set; }


        public List<ErrorItem> ErrorContainer { get; set; }

        public DataTable JS_Plants { get; set; }
        public DataTable JS_Recordstatus { get; set; }
        public DataTable JS_UOM { get; set; }
        public DataTable JS_plantdetails { get; set; }
        public DataTable JS_FGdetails { get; set; }
        public DataTable JS_Molddetails { get; set; }
        public DataTable JS_Childpartdetails { get; set; }
        public DataTable JS_Moldrfidassignmentdetails { get; set; }
    }
}
