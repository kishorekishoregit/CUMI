using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIBC
{
    public class GRNEntryBC
    {
        public ResponseGRNEntry GRNEntryPageloadBC()
        {
            ResponseGRNEntry response = new ResponseGRNEntry();
            WMSDAL DAL = new WMSDAL();
            response = DAL.GRNEntryPageloadDAL();
            return response;
        }
        public ResponseGRNEntry GRNEntryItemDetailsFetchBC(RequestGRNEntry request)
        {
            ResponseGRNEntry response = new ResponseGRNEntry();
            WMSDAL DAL = new WMSDAL();
            response = DAL.GRNEntryItemDetailsFetchDAL(request);
            return response;
        }
        public ResponseGRNEntry GRNEntryViewBC(RequestGRNEntry request)
        {
            ResponseGRNEntry response = new ResponseGRNEntry();
            WMSDAL DAL = new WMSDAL();
            response = DAL.GRNEntryViewDAL(request);
            return response;
        }
        public ResponseGRNEntry GRNEntryEditBC(RequestGRNEntry request)
        {
            ResponseGRNEntry response = new ResponseGRNEntry();
            WMSDAL DAL = new WMSDAL();
            response = DAL.GRNEntryEditDAL(request);
            return response;
        }
        public ResponseGRNEntry GRNEntryInsertBC(RequestGRNEntry request)
        {
            ResponseGRNEntry response = new ResponseGRNEntry();
            response.ErrorContainer = ValidateUpdate(request);
            if (response.ErrorContainer.Count == 0)
            {
                WMSDAL DAL = new WMSDAL();
                response = DAL.GRNEntryInsertDAL(request);
            }
            return response;

        }
        public ResponseGRNEntry GRNUpdateBC(RequestGRNEntry request)
        {
            ResponseGRNEntry response = new ResponseGRNEntry();
            response.ErrorContainer = ValidateUpdate(request);
            if (response.ErrorContainer.Count == 0)
            {
                WMSDAL DAL = new WMSDAL();
                response = DAL.GRNUpdateDAL(request);
            }
            return response;

        }
        public List<ErrorItem> ValidateUpdate(RequestGRNEntry request)
        {
            List<ErrorItem> err = new List<ErrorItem>();
            if (request.requestgrnentry.GRNNO == "")
                err.Add(new ErrorItem { DataItem = "GRN No", ErrorNo = "SSB0009" });
            if (request.requestgrnentry.GRNDATE == "")
                err.Add(new ErrorItem { DataItem = "GRN Date", ErrorNo = "SSB0009" });
            if (request.requestgrnentry.SUPPLIER == "")
                err.Add(new ErrorItem { DataItem = "Supplier", ErrorNo = "SSB0009" });
            if (request.requestgrnentry.PONO == "")
                err.Add(new ErrorItem { DataItem = "PO NO", ErrorNo = "SSB0009" });
            if (request.requestgrnentry.INVOICENO == "")
                err.Add(new ErrorItem { DataItem = "Invoice No", ErrorNo = "SSB0009" });
            if (request.requestgrnentrydetails.Count == 0)
                err.Add(new ErrorItem { DataItem = "Please, Atleast one detail data  to proceed!!!", ErrorNo = "SSB0000" });
            return err;
        }
    }
}
