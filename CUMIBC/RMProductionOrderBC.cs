using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIBC
{
    public class RMProductionOrderBC
    {
        public ResponseRMProductionOrder RMProductionOrderPageloadBC()
        {
            ResponseRMProductionOrder response = new ResponseRMProductionOrder();
            WMSDAL DAL = new WMSDAL();
            response = DAL.RMProductionOrderPageloadDAL();

            return response;
        }
        public ResponseRMProductionOrder RMProductionOrderFetchDtsByProductionnoBC(RequestRMProductionOrder request)
        {
            ResponseRMProductionOrder response = new ResponseRMProductionOrder();
            WMSDAL DAL = new WMSDAL();
            response = DAL.RMProductionOrderFetchDtsByProductionnoDAL(request);

            return response;
        }
        public ResponseRMProductionOrder RMProductionOrderBViewBC(RequestRMProductionOrder request)
        {
            ResponseRMProductionOrder response = new ResponseRMProductionOrder();
            WMSDAL DAL = new WMSDAL();
            response = DAL.RMProductionOrderBViewDAL(request);

            return response;
        }
        public ResponseRMProductionOrder RMProductionOrderInsertBC(RequestRMProductionOrder request)
        {
            ResponseRMProductionOrder response = new ResponseRMProductionOrder();
            WMSDAL DAL = new WMSDAL();
            response = DAL.RMProductionOrderInsertDAL(request);
            return response;

        }

    }
}
