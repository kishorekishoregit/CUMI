using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CUMIBC
{
    public class ProductionOrderAssignBC

    {
        public ResponseProductionOrderAssign ProductionOrderAssignPageLoadBC(RequestProductionOrderAssign request)
        {
            ResponseProductionOrderAssign response = new ResponseProductionOrderAssign();
            WMSDAL DAL = new WMSDAL();
            response = DAL.PageloadProductionordernoDAL();
            return response;
        }
        public ResponseProductionOrderAssign FetchProductionorderassignBC(RequestProductionOrderAssign request)
        {
            ResponseProductionOrderAssign response = new ResponseProductionOrderAssign();

            WMSDAL DAL = new WMSDAL();
            response = DAL.FetchproductionordernofetchdetailsDAL(request);
            return response;
        }
        public ResponseProductionOrderAssign InsertProductionorderassignBC(RequestProductionOrderAssign request)
        {
            ResponseProductionOrderAssign response = new ResponseProductionOrderAssign();
            WMSDAL DAL = new WMSDAL();
            response = DAL.InsertProductionorderDetailsDAL(request);
            return response;

        }

        public ResponseProductionOrderAssign ViewProductionorderBC(RequestProductionOrderAssign request)
        {
            ResponseProductionOrderAssign response = new ResponseProductionOrderAssign();
            WMSDAL DAL = new WMSDAL();
            response = DAL.ProductionorderViewDtsDAL(request);
            return response;

        }
        public ResponseProductionOrderAssign ProductionOrderAssigndtsBC(RequestProductionOrderAssign request)
        {
            ResponseProductionOrderAssign response = new ResponseProductionOrderAssign();
            WMSDAL DAL = new WMSDAL();
            response = DAL.ProductionOrderAssigndtsDAL(request);
            return response;

        }
        public List<ErrorItem> Validate(RequestProductionOrderAssign request)
        {
            List<ErrorItem> err = new List<ErrorItem>();
            if (request.requestproductionordernoheaderdetails.PRODUCTIONORDERNO == "")
                err.Add(new ErrorItem { DataItem = "Production Orderno", ErrorNo = "SSB0009" });
            if (request.requestproductionordernoheaderdetails.LOCATION == "")
                err.Add(new ErrorItem { DataItem = "Location", ErrorNo = "SSB0009" });
            if (request.requestproductionordernoheaderdetails.LOCATION == "")
                err.Add(new ErrorItem { DataItem = "FG Item Code", ErrorNo = "SSB0009" });
            if (request.requestproductionordernoheaderdetails.DATE == "")
                err.Add(new ErrorItem { DataItem = "FG Item Code", ErrorNo = "SSB0009" });
            if (request.requestproductionordernoheaderdetails.WAREHOUSEPICKER == "")
                err.Add(new ErrorItem { DataItem = "Warehouse Picker", ErrorNo = "SSB0009" });
            if (request.requestProductionOrderAssigndetails.Count == 0)
                err.Add(new ErrorItem { DataItem = "Please, Atleast one data detail to proceed!!!", ErrorNo = "SSB0009" });


            return err;
        }
    }
}
