using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIBC
{
    public class MoldScrapReportBC
    {
        public ResponseMoldScrapReport PageloadMoldScrapReportBC()
        {
            ResponseMoldScrapReport response = new ResponseMoldScrapReport();
            WMSDAL DAL = new WMSDAL();
            response = DAL.PageloadMoldScrapReportDAL();
            return response;
        }
    }
}
