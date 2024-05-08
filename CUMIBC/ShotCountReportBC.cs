using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIBC
{
  public class ShotCountReportBC
    {
        public ResponseShotCountReport MoldShotCountReportPageLoadBC()
        {
            ResponseShotCountReport response = new ResponseShotCountReport();
            WMSDAL DAL = new WMSDAL();
            response = DAL.ShotCountReportPageLoadDAL();

            return response;
        }
        public ResponseShotCountReport MoldShotCountReportGenerateBC(RequestShotCountReport request)
        {
            ResponseShotCountReport response = new ResponseShotCountReport();
            WMSDAL DAL = new WMSDAL();
            response = DAL.MoldShotCountReportGenerateDAL(request);

            return response;
        }
    }
}
