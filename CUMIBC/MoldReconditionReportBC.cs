using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIBC
{
    public class MoldReconditionReportBC
    {
        public ResponseMoldReconditionReport PageloadMoldReconditionReportBC()
        {
            ResponseMoldReconditionReport response = new ResponseMoldReconditionReport();
            WMSDAL DAL = new WMSDAL();
            response = DAL.PageloadMoldReconditionReportDAL();
            return response;
        }
    }
}
