using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CUMIBC
{
   public class ChildPartMasterBC
    {
        public ResponseChildPartMaster ChildPartMasterPageLoadBC()
        {
            ResponseChildPartMaster response = new ResponseChildPartMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.PageloadChildPartMasterDAL();
            return response;
        }
        public ResponseChildPartMaster FetchLocationPartcodeBC(RequestChildPartMaster request)
        {
            ResponseChildPartMaster response = new ResponseChildPartMaster();

            WMSDAL DAL = new WMSDAL();
            response = DAL.FetchpartcodeMasterDAL(request);
            return response;
        }
        public ResponseChildPartMaster InsertChildPartMasterBC(RequestChildPartMaster request)
        {
            ResponseChildPartMaster response = new ResponseChildPartMaster();
            response.ErrorContainer = Validate(request);

            WMSDAL DAL = new WMSDAL();
            response = DAL.InsertChildPartMasterDAL(request);

            return response;

        }

        public ResponseChildPartMaster EditChildPartMasterBC(RequestChildPartMaster request)
        {
            ResponseChildPartMaster response = new ResponseChildPartMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.EditChildPartMasterDAL(request);
            return response;

        }
        public ResponseChildPartMaster UpdateChildPartMasterBC(RequestChildPartMaster request)
        {
            ResponseChildPartMaster response = new ResponseChildPartMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.UpdateChildPartMasterDAL(request);
            return response;

        }

        public List<ErrorItem> Validate(RequestChildPartMaster request)
        {
            List<ErrorItem> err = new List<ErrorItem>();
            if (request.requestchildpartmaster.PLANTCODE == "")
                err.Add(new ErrorItem { DataItem = "Plant Code", ErrorNo = "SSB0009" });
            if (request.requestchildpartmaster.LOCATION == "")
                err.Add(new ErrorItem { DataItem = "Location", ErrorNo = "SSB0009" });
            if (request.requestchildpartmaster.CHILDITEMCODE == "")
                err.Add(new ErrorItem { DataItem = " Item Code", ErrorNo = "SSB0009" });
            if (request.requestchildpartmaster.UOM == "")
                err.Add(new ErrorItem { DataItem = "UOM", ErrorNo = "SSB0010" });
           
            if (request.requestchildpartmaster.RECORDSTATUS == "")
                err.Add(new ErrorItem { DataItem = "Record Status", ErrorNo = "SSB0010" });

            return err;
        }
    }
}
