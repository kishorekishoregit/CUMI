using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIENTITY
{
    public class RDCPicklistFileuploadEntity
    {
        public string AUTOID { get; set; }
        public string RDCNO { get; set; }
        public string DATE {  get; set; }   
        public string ITEMCODE {  get; set; }
        public string ITEMNAME {  get; set; }   
        public string SUPPLIER {  get; set; }
        public string QTY {  get; set; }
    }
    public class RDCPicklistFileuploadDetailsEntity
    {
        public string AUTOID { get; set; }
        public string RDCNO { get; set; }
        public string DATE { get; set; }
        public string ITEMCODE { get; set; }
        public string ITEMNAME { get; set; }
        public string SUPPLIER { get; set; }
        public string QTY { get; set; }
    }
    public class RequestRDCPicklistFileupload
    {
        public RDCPicklistFileuploadEntity requestRDCPicklistFileupload { get; set; }
        public List<RDCPicklistFileuploadDetailsEntity> requestRDCPicklistFileuploadDetails {  get; set; }  
    }
    public class ResponseRDCPicklistFileupload
    {
        public bool result { get; set; }
        public string message { get; set; }
        public DataTable JS_RDCPicklistdts { get; set; }
        public List<ErrorItem> ErrorContainer {  get; set; }
    }
}
