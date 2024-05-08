using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIENTITY
{
    public class PhysicalStockReportEntity
    {
        public string AUTOID {  get; set; }
        public string FROMDATE {  get; set; }
        public string TODATE {  get; set; }
        public string MOLDITEMCODE {  get; set; }
        public string USERCODE { get; set; }
    }
    public class RequestPhysicalStockReport
    {
        public PhysicalStockReportEntity requestphysicalstockreport { get; set; }
    }
    public class ResponsePhysicalStockReport
    {
        public bool result { get; set; }
        public List<ErrorItem> ErrorContainer { get; set; } 
        public DataTable JS_PhysicalStockPageload { get; set; }
        public DataTable JS_PhysicalStockGenerate { get; set; }
        public DataTable JS_PhysicalStockView {  get; set; }
    }
}
