using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIBC
{
    public class BOMMasterBC
    {
        public ResponseBOMMaster PageloadBOMMasterBC()
        {
            ResponseBOMMaster response = new ResponseBOMMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.PageloadBOMMasterDAL();
            return response;
        }
        public ResponseBOMMaster FetchItemCodeBOMMasterBC(RequestBOMMaster request)
        {
            ResponseBOMMaster response = new ResponseBOMMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.FetchItemCodeBOMMasterDAL(request);
            return response;
        }
        public ResponseBOMMaster FetchItemNameBOMMasterBC(RequestBOMMaster request)
        {
            ResponseBOMMaster response = new ResponseBOMMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.FetchItemNameBOMMasterDAL(request);
            return response;
        }
        public ResponseBOMMaster FetchFGItemnameBOMMasterBC(RequestBOMMaster request)
        {
            ResponseBOMMaster response = new ResponseBOMMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.FetchFGItemnameBOMMasterDAL(request);
            return response;
        }
        public ResponseBOMMaster EditBOMMasterBC(RequestBOMMaster request)
        {
            ResponseBOMMaster response = new ResponseBOMMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.EditBOMMasterDAL(request);
            return response;
        }
        public ResponseBOMMaster ViewBOMMasterBC(RequestBOMMaster request)
        {
            ResponseBOMMaster response = new ResponseBOMMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.ViewBOMMasterDAL(request);
            return response;
        }
        public ResponseBOMMaster InsertBOMMMasterBC(RequestBOMMaster request)
        {
            ResponseBOMMaster response = new ResponseBOMMaster();
            response.ErrorContainer = Validate(request);
            if (response.ErrorContainer.Count == 0)
            {
                WMSDAL DAL = new WMSDAL();
                response = DAL.InsertBOMMMasterDAL(request);
            }
            return response;
        }
        public ResponseBOMMaster UpdateBOMMasterBC(RequestBOMMaster request)
        {
            ResponseBOMMaster response = new ResponseBOMMaster();
            response.ErrorContainer = Validate(request);
            if (response.ErrorContainer.Count == 0)
            {
                WMSDAL DAL = new WMSDAL();
                response = DAL.UpdateBOMMasterDAL(request);
            }
            return response;
        }
        public List<ErrorItem> Validate(RequestBOMMaster request)
        {
            List<ErrorItem> err = new List<ErrorItem>();
            if (request.requestbommaster.ASSEMBLYID == "")
                err.Add(new ErrorItem { DataItem = "Assembly ID", ErrorNo = "SSB0009" });
            if (request.requestbommaster.FGITEMCODE == "")
                err.Add(new ErrorItem { DataItem = "FG Item Code", ErrorNo = "SSB0010" });
            if (request.requestbommaster.VARIANT == "")
                err.Add(new ErrorItem { DataItem = "Variant", ErrorNo = "SSB0010" });
            if (request.requestbommasterdetails.Count == 0)
                err.Add(new ErrorItem { DataItem = "Please, Atleast one data detail to proceed!!!", ErrorNo = "SSB0000" });
            return err;
        }
    }
}
