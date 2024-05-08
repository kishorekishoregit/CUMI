using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIBC
{
    public class MoldPicklistReportBC
    {
        public ResponseMoldPicklist MoldPicklistReportpageloadBC()
        {
            ResponseMoldPicklist response = new ResponseMoldPicklist();
            WMSDAL DAL = new WMSDAL();
            response = DAL.MoldPicklistReportpageloadDAL();
            return response;
        }
        public ResponseMoldPicklist MoldPicklistReportGenerateBC(RequestMoldPicklist request)
        {
            ResponseMoldPicklist response = new ResponseMoldPicklist();
            WMSDAL DAL = new WMSDAL();
            response = DAL.MoldPicklistReportGenerateDAL(request);
            return response;
        }
    }
}
