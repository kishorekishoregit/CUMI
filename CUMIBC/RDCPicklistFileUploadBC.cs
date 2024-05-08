using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIBC
{
    public class RDCPicklistFileUploadBC
    {
        public ResponseRDCPicklistFileupload RDCPicklistFileuploadInsertBC(RequestRDCPicklistFileupload request)
        {
            ResponseRDCPicklistFileupload response = new ResponseRDCPicklistFileupload();
            WMSDAL DAL = new WMSDAL();
            response = DAL.RDCPicklistFileuploadInsertDAL(request);
            return response;
        }
    }
}
