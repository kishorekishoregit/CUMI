using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CUMIBC
{
  public class WarehouseMasterBC

    {
        public ResponseWarehouseMaster WarehouseMasterPageLoadBC()
        {
            ResponseWarehouseMaster response = new ResponseWarehouseMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.WarehouseMasterPageLoadDAL();

            return response;
        }
        public ResponseWarehouseMaster FetchWarehouseMasterPartBC(RequestWarehouseMaster request)
        {
            ResponseWarehouseMaster response = new ResponseWarehouseMaster();

            WMSDAL DAL = new WMSDAL();
            response = DAL.FetchWarehouseMasterDAL(request);
            return response;
        }
        public ResponseWarehouseMaster InsertWarehouseMasterBC(RequestWarehouseMaster request)
        {

            ResponseWarehouseMaster response = new ResponseWarehouseMaster();
            response.ErrorContainer = Validate(request);
            if (response.ErrorContainer.Count == 0)
            {
                WMSDAL DAL = new WMSDAL();
                response = DAL.InsertWarehouseMasterDAL(request);
            }
            return response;

        }
        public ResponseWarehouseMaster EditWarehouseMasterBC(RequestWarehouseMaster request)
        {
            ResponseWarehouseMaster response = new ResponseWarehouseMaster();

            WMSDAL DAL = new WMSDAL();
            response = DAL.EditWarehouseMasterDAL(request);
            return response;
        }
        public ResponseWarehouseMaster UpdateWarehouseMasterBC(RequestWarehouseMaster request)
        {

            ResponseWarehouseMaster response = new ResponseWarehouseMaster();
            response.ErrorContainer = Validate(request);
            if (response.ErrorContainer.Count == 0)
            {
                WMSDAL DAL = new WMSDAL();
                response = DAL.UpdateWarehouseMasterDAL(request);
            }
            return response;

        }

        public List<ErrorItem> Validate(RequestWarehouseMaster request)
        {
            List<ErrorItem> err = new List<ErrorItem>();
            if (request.requestWarehouseMaster.WAREHOUSECODE == "")
                err.Add(new ErrorItem { DataItem = "Customer Code", ErrorNo = "SSB0009" });
            if (request.requestWarehouseMaster.WAREHOUSENAME == "")
                err.Add(new ErrorItem { DataItem = "Customer Name", ErrorNo = "SSB0009" });
            if (request.requestWarehouseMaster.PLANTCODE == "")
                err.Add(new ErrorItem { DataItem = "Email ID", ErrorNo = "SSB0009" });
            if (request.requestWarehouseMaster.RECORDSTATUS == "")
                err.Add(new ErrorItem { DataItem = "Record Status", ErrorNo = "SSB0010" });
            return err;
        }
    }
}
