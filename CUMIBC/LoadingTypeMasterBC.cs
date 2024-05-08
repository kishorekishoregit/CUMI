using System;
using System.Collections.Generic;
using System.Linq;
using CUMIENTITY;
using CUMIDAC;
using System.Text;

namespace CUMIBC
{
    public class LoadingTypeMasterBC
    {
        public ResponseLoadingTypeMaster LoadingTypeMasterpageloadBC()
        {
            ResponseLoadingTypeMaster response = new ResponseLoadingTypeMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.LoadingTypeMasterloadDAL();
            return response;
        }
        public ResponseLoadingTypeMaster InsertLoadingTypeMasterBC(RequestLoadingTypeMaster request)
        {
            ResponseLoadingTypeMaster response = new ResponseLoadingTypeMaster();

            WMSDAL DAL = new WMSDAL();
            response = DAL.InsertLoadingTypeMasterDAL(request);
            return response;

        }
        public ResponseLoadingTypeMaster EDITLoadingTypeMasterBC(RequestLoadingTypeMaster request)
        {
            ResponseLoadingTypeMaster response = new ResponseLoadingTypeMaster();

            WMSDAL DAL = new WMSDAL();
            response = DAL.EDITLoadingTypeMasterDAL(request);
            return response;

        }
        public ResponseLoadingTypeMaster UpateLoadingTypeMasterBC(RequestLoadingTypeMaster request)
        {
            ResponseLoadingTypeMaster response = new ResponseLoadingTypeMaster();

            WMSDAL DAL = new WMSDAL();
            response = DAL.UpdateLoadingTypeMasterDAL(request);
            return response;

        }
    }
}
