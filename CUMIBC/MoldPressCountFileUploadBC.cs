using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIBC
{
    public class MoldPressCountFileUploadBC
    {
        public ResponseMoldPressCountFileUpload MoldPressCountFileUploadInsertBC(RequestMoldPressCountFileUpload request)
        {
            ResponseMoldPressCountFileUpload response = new ResponseMoldPressCountFileUpload();
            WMSDAL DAL = new WMSDAL();
            response = DAL.MoldPressCountFileUploadInsertDAL(request);
            return response;
        }
    }
}
