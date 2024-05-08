using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CUMIBC
{
  public  class MoldMasterBC
    {
        public ResponseMoldMaster MoldMasterPageLoadBC()
        {
            ResponseMoldMaster response = new ResponseMoldMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.PageloadMoldMasterDAL();
            return response;
        }
        public ResponseMoldMaster FetchLocationPartcodeBC(RequestMoldMaster request)
        {
            ResponseMoldMaster response = new ResponseMoldMaster();

            WMSDAL DAL = new WMSDAL();
            response = DAL.FetchpartcodeMasterDAL(request);
            return response;
        }
        public ResponseMoldMaster InsertMoldMasterBC(RequestMoldMaster request)
        {
            ResponseMoldMaster response = new ResponseMoldMaster();
            response.ErrorContainer = Validate(request);

            WMSDAL DAL = new WMSDAL();
            response = DAL.InsertMoldMasterDAL(request);

            return response;

        }

        public ResponseMoldMaster EditMoldMasterBC(RequestMoldMaster request)
        {
            ResponseMoldMaster response = new ResponseMoldMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.EditMoldMasterDAL(request);
            return response;

        }
        public ResponseMoldMaster UpdateMoldMasterBC(RequestMoldMaster request)
        {
            ResponseMoldMaster response = new ResponseMoldMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.UpdateMoldMasterDAL(request);
            return response;

        }

        public List<ErrorItem> Validate(RequestMoldMaster request)
        {
            List<ErrorItem> err = new List<ErrorItem>();
            if (request.requestrmoldmaster.PLANTCODE == "")
                err.Add(new ErrorItem { DataItem = "Plant Code", ErrorNo = "SSB0009" });
            if (request.requestrmoldmaster.LOCATION == "")
                err.Add(new ErrorItem { DataItem = "Location", ErrorNo = "SSB0009" });
            if (request.requestrmoldmaster.MOLDITEMCODE == "")
                err.Add(new ErrorItem { DataItem = "RM Item Code", ErrorNo = "SSB0009" });
            if (request.requestrmoldmaster.UOM == "")
                err.Add(new ErrorItem { DataItem = "UOM", ErrorNo = "SSB0010" });
            if (request.requestrmoldmaster.PONUMBER == "")
                err.Add(new ErrorItem { DataItem = "PO Number", ErrorNo = "SSB0009" });
            if (request.requestrmoldmaster.PONUMBER == "")
                err.Add(new ErrorItem { DataItem = "Supplier Code", ErrorNo = "SSB0009" });
            if (request.requestrmoldmaster.GROUP == "")
                err.Add(new ErrorItem { DataItem = "Group", ErrorNo = "SSB0009" });
            if (request.requestrmoldmaster.RECORDSTATUS == "")
                err.Add(new ErrorItem { DataItem = "Record Status", ErrorNo = "SSB0010" });

            return err;
        }
    }
}
