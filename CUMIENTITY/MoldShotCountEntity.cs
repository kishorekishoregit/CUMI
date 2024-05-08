using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIENTITY
{
   public class MoldShotCountEntity
    {
        public string AUTOID { get; set; }
        public string MOLDITEMCODE { get; set; }
        public string MOLDNAME { get; set; }
        public string FGITEMCODE { get; set; }
        public string CHILDITEMCODE { get; set; }
        public string RFIDNUMBER { get; set; }
        public string SHOTCOUNT { get; set; }
        public string DATEOFSHOT { get; set; }
        public string USERCODE { get; set; }
    }

    public class RequestMoldShotCount
    {
        public MoldShotCountEntity requestmoldshotcount { get; set; }
    }
    public class ResponseMoldShotCount
    {
        public bool result { get; set; }
        public List<ErrorItem> ErrorContainer { get; set; }
        public DataTable JS_shotdetails { get; set; }
        public DataTable JS_Molddetails { get; set; }
    }
}
