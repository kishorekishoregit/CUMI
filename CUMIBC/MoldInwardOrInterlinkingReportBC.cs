using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIBC
{
    public  class MoldInwardOrInterlinkingReportBC
    {
        public ResponseMoldInwardOrInterlinking MoldInwardOrInterlinkingPageLoadBC()
        {
            ResponseMoldInwardOrInterlinking response = new ResponseMoldInwardOrInterlinking();
            WMSDAL DAL = new WMSDAL();
            response = DAL.MoldInwardOrInterlinkingPageLoadDAL();

            return response;
        }
        public ResponseMoldInwardOrInterlinking MoldInwardOrInterlinkingGenerateDAL(RequestMoldInwardOrInterlinking request)
        {
            ResponseMoldInwardOrInterlinking response = new ResponseMoldInwardOrInterlinking();
            WMSDAL DAL = new WMSDAL();
            response = DAL.MoldInwardOrInterlinkingGenerateDAL(request);

            return response;
        }
    }
}
