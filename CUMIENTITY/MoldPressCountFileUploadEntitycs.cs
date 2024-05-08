using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIENTITY
{
    public class MoldPressCountFileUploadEntitycs
    {
        public string MOLDITEMCODE { get; set; }
        public string MOLDITEMNAME {  get; set; }   
        public string FGITEMCODE { get; set; }
        public string SHOTCOUNT {  get; set; }
        public string DATEOFSHOT { get; set; }
        public string TOTALSHOTCOUNT {  get; set; } 
        public string RFIDNO {  get; set; }
    }
    public class MoldPressCountFileUploadDetailsEntity   
    {
        public string MOLDITEMCODE { get; set; }
        public string MOLDITEMNAME { get; set; }
        public string FGITEMCODE { get; set; }
        public string SHOTCOUNT { get; set; }
        public string DATEOFSHOT { get; set; }
        public string TOTALSHOTCOUNT { get; set; }
        public string RFIDNO { get; set; }
    }
    public class RequestMoldPressCountFileUpload
    {
        public MoldPressCountFileUploadEntitycs requestmoldpresscount {  get; set; }
        public List<MoldPressCountFileUploadDetailsEntity> requestmoldpresscountdts { get; set; }
    }
    public class ResponseMoldPressCountFileUpload
    {
        public bool result { get; set; }
        public string message { get; set; }
        public List<ErrorItem> ErrorContainer { get; set; }
        public DataTable JS_MoldPressCoutdts { get; set; }
    }
}
