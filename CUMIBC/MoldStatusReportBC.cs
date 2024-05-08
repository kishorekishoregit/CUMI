using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIBC
{
  public class MoldStatusReportBC
    {
        public ResponseMoldStatusReport MoldStatusReportPageLoadBC()
        {
            ResponseMoldStatusReport response = new ResponseMoldStatusReport();
            WMSDAL DAL = new WMSDAL();
            response = DAL.MoldStatusReportPageLoadDAL();

            return response;
        }
        public ResponseMoldStatusReport MoldStatusReportGenerateBC(RequestMoldStatusReport request)
        {
            ResponseMoldStatusReport response = new ResponseMoldStatusReport();
            WMSDAL DAL = new WMSDAL();
            response = DAL.MoldStatusReportGenerateDAL(request);

            return response;
        }
    }
}
