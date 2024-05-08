using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIBC
{
   public class LocationWiseMoldStockReportBC
    {
        public ResponseLocationWiseMoldStockReport LocationWiseMoldReportPageLoadBC()
        {
            ResponseLocationWiseMoldStockReport response = new ResponseLocationWiseMoldStockReport();
            WMSDAL DAL = new WMSDAL();
            response = DAL.LocationWiseMoldReportPageLoadDAL();

            return response;
        }
        public ResponseLocationWiseMoldStockReport LocationWiseMoldReportGenerateBC(RequestLocationWiseMoldStockReport request)
        {
            ResponseLocationWiseMoldStockReport response = new ResponseLocationWiseMoldStockReport();
            WMSDAL DAL = new WMSDAL();
            response = DAL.LocationWiseMoldStockReportGenerateDAL(request);

            return response;
        }
    }
}
