using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIENTITY
{
  public class LocationWiseMoldStockReportEntity
    
    {
        public string AUTOID { get; set; }
        public string LOCATION { get; set; }
        public string FROMDATE { get; set; }
        public string TODATE { get; set; }
        public string USERCODE { get; set; }
    }
  

    public class RequestLocationWiseMoldStockReport
    {
        public LocationWiseMoldStockReportEntity requestlocationwisemoldreport { get; set; }
    }
    public class ResponseLocationWiseMoldStockReport
    {
        public bool result { get; set; }

        public List<ErrorItem> ErrorContainer { get; set; }
        public DataTable JS_LocationwisemoldreportPageload { get; set; }
        public DataTable JS_LocationwisemoldreportGenerate { get; set; }
        public DataTable JS_Location { get; set; }
    }

}
