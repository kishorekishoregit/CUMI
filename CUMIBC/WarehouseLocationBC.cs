using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CUMIBC
{
   public class WarehouseLocationBC
    {
        public ResponseWarehouseLocationMaster WarehouseLocationMasterPageLoadBC()
        {
            ResponseWarehouseLocationMaster response = new ResponseWarehouseLocationMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.WarehouseLocationMasterPageLoadDAL();

            return response;
        }
        public ResponseWarehouseLocationMaster WarehouseLocationMasterPrintViewBC()
        {
            ResponseWarehouseLocationMaster response = new ResponseWarehouseLocationMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.WarehouseLocationMasterPrintViewDAL();

            return response;
        }
        public ResponseWarehouseLocationMaster FetchWarehouseMasterLocationPartcodeBC(RequestWarehouseLocationMaster request)
        {
            ResponseWarehouseLocationMaster response = new ResponseWarehouseLocationMaster();

            WMSDAL DAL = new WMSDAL();
            response = DAL.FetchWarehousepartcodeMasterDAL(request);
            return response;
        }

       
        public ResponseWarehouseLocationMaster InsertWarehouseLocationMasterBC(RequestWarehouseLocationMaster request)
        {

            ResponseWarehouseLocationMaster response = new ResponseWarehouseLocationMaster();
            response.ErrorContainer = Validate(request);
            if (response.ErrorContainer.Count == 0)
            {
                WMSDAL DAL = new WMSDAL();
                response = DAL.InsertWarehouseLocationMasterDAL(request);
            }
            return response;

        }
        public ResponseWarehouseLocationMaster EditWarehouseLocationMasterBC(RequestWarehouseLocationMaster request)
        {
            ResponseWarehouseLocationMaster response = new ResponseWarehouseLocationMaster();

            WMSDAL DAL = new WMSDAL();
            response = DAL.EditWarehouseLocationMasterDAL(request);
            return response;
        }
        public ResponseWarehouseLocationMaster UpdateWarehouseLocationMasterBC(RequestWarehouseLocationMaster request)
        {

            ResponseWarehouseLocationMaster response = new ResponseWarehouseLocationMaster();
            response.ErrorContainer = Validate(request);
            if (response.ErrorContainer.Count == 0)
            {
                WMSDAL DAL = new WMSDAL();
                response = DAL.UpdateWarehouseLocationMasterDAL(request);
            }
            return response;

        }

        public List<ErrorItem> Validate(RequestWarehouseLocationMaster request)
        {
            List<ErrorItem> err = new List<ErrorItem>();
            if (request.requestWarehouseLocationMaster.PLANTCODE == "")
                err.Add(new ErrorItem { DataItem = "Plant Code", ErrorNo = "SSB0010" });
            if (request.requestWarehouseLocationMaster.WAREHOUSECODE == "")
                err.Add(new ErrorItem { DataItem = "Warehouse", ErrorNo = "SSB0009" });
            if (request.requestWarehouseLocationMaster.LOCATION == "")
                err.Add(new ErrorItem { DataItem = "Email ID", ErrorNo = "SSB0009" });
            if (request.requestWarehouseLocationMaster.RECORDSTATUS == "")
                err.Add(new ErrorItem { DataItem = "Record Status", ErrorNo = "SSB0010" });
            return err;
        }
    }
}
