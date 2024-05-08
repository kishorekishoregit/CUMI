using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIBC
{
   public class MoldPutawayReportBC
    {
        public ResponseMoldPutawayReport MoldPutawayPageLoadBC()
        {
            ResponseMoldPutawayReport response = new ResponseMoldPutawayReport();
            WMSDAL DAL = new WMSDAL();
            response = DAL.MoldPutawayPageLoadDAL();

            return response;
        }
        public ResponseMoldPutawayReport MoldPutawayGenerateBC(RequestMoldPutawayReport request)
        {
            ResponseMoldPutawayReport response = new ResponseMoldPutawayReport();
            WMSDAL DAL = new WMSDAL();
            response = DAL.MoldPutawayGenerateDAL(request);
            return response;
        }

    }
}
