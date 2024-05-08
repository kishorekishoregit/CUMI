using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIBC
{
    public class RMIwardReportBC
    {
        public ResponseRMInwardReport RMInwardReportPageloadBC()
        {
            ResponseRMInwardReport response = new ResponseRMInwardReport();
            WMSDAL DAL = new WMSDAL();
            response = DAL.RMInwardReportPageloadDAL();
            return response;
        }
        public ResponseRMInwardReport RMInwardReportGenerateBC(RequestRMIwardReport request)
        {
            ResponseRMInwardReport response = new ResponseRMInwardReport();
            WMSDAL DAL = new WMSDAL();
            response = DAL.RMInwardReportGenerateDAL(request);
            return response;
        }
    }
}
