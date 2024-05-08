using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIBC
{
    public class ProductionOrderFileUploadBC
    {
        public ResponseProductionOrderFileUpload ProductionOrderFileUploadInsertBC(RequestProductionOrderFileUpload request)
        {
            ResponseProductionOrderFileUpload response = new ResponseProductionOrderFileUpload();
            WMSDAL DAL = new WMSDAL();
            response = DAL.ProductionOrderFileUploadInsertDAL(request);
            return response;
        }
    }
}
