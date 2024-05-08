using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CUMIBC
{
    public class MoldRFIDAssignmentBC
    {
        public ResponseMoldRFIDAssignment MoldRFIDAssignmentPageLoadBC()
        {
            ResponseMoldRFIDAssignment response = new ResponseMoldRFIDAssignment();
            WMSDAL DAL = new WMSDAL();
            response = DAL.PageloadMoldRFIDAssignmentDAL();
            return response;
        }
        public ResponseMoldRFIDAssignment FetchLocationPartcodeBC(RequestMoldRFIDAssignment request)
        {
            ResponseMoldRFIDAssignment response = new ResponseMoldRFIDAssignment();

            WMSDAL DAL = new WMSDAL();
            response = DAL.FetchpartcodeMasterDAL(request);
            return response;
        }

        public ResponseMoldRFIDAssignment FetchVarientCodeBC(RequestMoldRFIDAssignment request)
        {
            ResponseMoldRFIDAssignment response = new ResponseMoldRFIDAssignment();

            WMSDAL DAL = new WMSDAL();
            response = DAL.FetchVarientCodeDAL(request);
            return response;
        }
        public ResponseMoldRFIDAssignment InsertMoldRFIDAssignmentBC(RequestMoldRFIDAssignment request)
        {
            ResponseMoldRFIDAssignment response = new ResponseMoldRFIDAssignment();
            response.ErrorContainer = Validate(request);
            if (response.ErrorContainer.Count == 0)
            {
                WMSDAL DAL = new WMSDAL();
                response = DAL.InsertMoldRFIDAssignmentDAL(request);
            }
            return response;
        }

        public ResponseMoldRFIDAssignment EditMoldRFIDAssignmentBC(RequestMoldRFIDAssignment request)
        {
            ResponseMoldRFIDAssignment response = new ResponseMoldRFIDAssignment();
            WMSDAL DAL = new WMSDAL();
            response = DAL.EditMoldRFIDAssignmentDAL(request);
            return response;

        }

        public ResponseMoldRFIDAssignment UpdateMoldRFIDAssignmentBC(RequestMoldRFIDAssignment request)
        {
            ResponseMoldRFIDAssignment response = new ResponseMoldRFIDAssignment();
            response.ErrorContainer = Validate(request);
            if (response.ErrorContainer.Count == 0)
            {
                WMSDAL DAL = new WMSDAL();
                response = DAL.UpdateMoldRFIDAssignmentDAL(request);
            }
            return response;
        }

        //public ResponseInterlinkingMaster ViewInterlinkingMasterBC(RequestInterlinkingMaster request)
        //{
        //    ResponseInterlinkingMaster response = new ResponseInterlinkingMaster();
        //    WMSDAL DAL = new WMSDAL();
        //    response = DAL.InterlinkingViewDtsDAL(request);
        //    return response;

        //}
        public List<ErrorItem> Validate(RequestMoldRFIDAssignment request)
        {
            List<ErrorItem> err = new List<ErrorItem>();
            if (request.requestMoldRFIDAssignment.PLANTCODE == "")
                err.Add(new ErrorItem { DataItem = "Plant Code", ErrorNo = "SSB0009" });       
            if (request.requestMoldRFIDAssignment.LOCATION == "")
                err.Add(new ErrorItem { DataItem = "Location", ErrorNo = "SSB0009" });
            if (request.requestMoldRFIDAssignment.MOLDITEMCODE == "")
                    err.Add(new ErrorItem { DataItem = "Mold Item Code", ErrorNo = "SSB0009" });
            if (request.requestMoldRFIDAssignment.VARIENTCODE == "")
                    err.Add(new ErrorItem { DataItem = "Varient Code", ErrorNo = "SSB0009" });
            if (request.requestMoldRFIDAssignment.MOLDOPENCOUNT == "")
                err.Add(new ErrorItem { DataItem = "Mold Open Count", ErrorNo = "SSB0009" });
            if (request.requestMoldRFIDAssignment.MOLDOPENCOUNT == "")
                err.Add(new ErrorItem { DataItem = "Previous MR Count", ErrorNo = "SSB0009" });
            if (request.requestMoldRFIDAssignment.MOLDOPENCOUNT == "")
                err.Add(new ErrorItem { DataItem = "Previous MR Date", ErrorNo = "SSB0009" });
            if (request.requestMoldRFIDAssignment.MRALERTCOUNT == "")
                err.Add(new ErrorItem { DataItem = "MR ALert Count", ErrorNo = "SSB0009" });
            if (request.requestMoldRFIDAssignment.MOLDLIFECOUNT == "")
                err.Add(new ErrorItem { DataItem = "Mold Life Count", ErrorNo = "SSB0009" });
            if (request.requestMoldRFIDAssignment.RECORDSTATUS == "")
                err.Add(new ErrorItem { DataItem = "Record Status", ErrorNo = "SSB0010" });
            //if (request.requestInterlinkingMasterDetails.Count == 0)
            //    err.Add(new ErrorItem { DataItem = "Please, Atleast one data detail to proceed!!!", ErrorNo = "SSB0000" });
            return err;
        }
    }
}
