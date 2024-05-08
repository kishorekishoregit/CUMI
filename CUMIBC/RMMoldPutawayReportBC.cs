using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIBC
{
  public  class RMMoldPutawayReportBC
    {
        public ResponseRMMoldPutaway RMMoldPutawayPageLoadBC()
        {
            ResponseRMMoldPutaway response = new ResponseRMMoldPutaway();
            WMSDAL DAL = new WMSDAL();
            response = DAL.RMMoldPutawayPageLoadDAL();

            return response;
        }
        public ResponseRMMoldPutaway RMMoldPutawayGenerateBC(RequestRMMoldPutaway request)
        {
            ResponseRMMoldPutaway response = new ResponseRMMoldPutaway();
            WMSDAL DAL = new WMSDAL();
            response = DAL.RMMoldPutawayGenerateDAL(request);
            return response;
        }
    }
}
