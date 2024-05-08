using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIENTITY
{
    public class MoldReceipt_RDCEntity
    {
        public string AUTOID { get; set; }
        public string RDCNO { get; set; }
        public string USERCODE { get; set; }
    }
    public class MoldReceipt_RDCDetailsEntity
    {
        public string RDCNO { get; set; }
        public string ITEMCODE { get; set; }
        public string ITEMNAME { get; set; }
        public string SUPPLIER { get; set; }
        public string RFIDNO { get; set; }
        public string UOM { get; set; }
        public string QUANTITY { get; set; }
    }

    public class RequestMoldReceipt_RDC
    {
        public MoldReceipt_RDCEntity requestmoldrecieptrdc { get; set; }
        public List<MoldReceipt_RDCDetailsEntity> requestmoldreceiptrdcdetails { get; set; }
    }
    public class ResponseMoldReceipt_RDC
    {
        public bool result { get; set; }
        public DataTable JS_MoldReceiptPageload { get; set; }
        public DataTable JS_MoldReceiptDetails { get; set; }
        public DataTable JS_RDCNo { get; set; }
        public List<ErrorItem> ErrorContainer { get; set; }

    }

}
