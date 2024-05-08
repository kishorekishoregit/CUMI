using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIBC
{
    public class MoldIssueConfirmationBC
    {
        public ResponseMoldIssueConfirmation MoldIssueConfirmationPageloadBC()
        {
            ResponseMoldIssueConfirmation response = new ResponseMoldIssueConfirmation();
            WMSDAL DAL = new WMSDAL();
            response = DAL.MoldIssueConfirmationPageloadDAL();
            return response;
        }
        public ResponseMoldIssueConfirmation MoldIssueConfirmationAssignBC(RequestMoldIssueConfirmation request)
        {
            ResponseMoldIssueConfirmation response = new ResponseMoldIssueConfirmation();
            WMSDAL DAL = new WMSDAL();
            response = DAL.MoldIssueConfirmationAssignDAL(request);
            return response;
        }
    }
}
