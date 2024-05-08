using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIBC
{
    public class MoldNearbyExpiryExpiredReportBC
    {
        public ResponseMoldNearbyExpiryExpiredReport MoldNearbyExpiryExpiredReportPageloadBC()
        {
            ResponseMoldNearbyExpiryExpiredReport response = new ResponseMoldNearbyExpiryExpiredReport();
            WMSDAL DAL = new WMSDAL();
            response = DAL.MoldNearbyExpiryExpiredReportPageloadDAL();
            return response;
        }
    }
}
