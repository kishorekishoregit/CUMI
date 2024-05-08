using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIBC
{
    public class PhysicalStockReportBC
    {
        public ResponsePhysicalStockReport PhysicalStockReportPageLoadBC()
        {
            ResponsePhysicalStockReport response = new ResponsePhysicalStockReport();
            WMSDAL DAL = new WMSDAL();
            response = DAL.PhysicalStockReportPageLoadDAL();
            return response;
        }
        
        public ResponsePhysicalStockReport PhysicalStockReportViewBC(RequestPhysicalStockReport request)
        {
            ResponsePhysicalStockReport response = new ResponsePhysicalStockReport();
            WMSDAL DAL = new WMSDAL();
            response = DAL.PhysicalStockReportViewDAL(request);
            return response;
        }
    }
}
