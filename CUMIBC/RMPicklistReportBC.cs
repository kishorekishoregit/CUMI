using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIBC
{
    public class RMPicklistReportBC
    {
        public ResponseRMPicklistReport RMPicklistReportpageloadBC()
        {
            ResponseRMPicklistReport response = new ResponseRMPicklistReport();
            WMSDAL DAL = new WMSDAL();
            response = DAL.RMPicklistReportpageloadDAL();
            return response;
        }
        public ResponseRMPicklistReport RMPicklistReportGenerateBC(RequestRMPicklistReport request)
        {
            ResponseRMPicklistReport response = new ResponseRMPicklistReport();
            WMSDAL DAL = new WMSDAL();
            response = DAL.RMPicklistReportGenerateDAL(request);
            return response;
        }
    }
}
