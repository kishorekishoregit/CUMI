using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CUMIBC
{
   public class RMMasterBC
    {
        public ResponseRMMaster RMMasterPageLoadBC()
        {
            ResponseRMMaster response = new ResponseRMMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.PageloadRMMasterDAL();
            return response;
        }
        public ResponseRMMaster FetchLocationPartcodeBC(RequestRMMaster request)
        {
            ResponseRMMaster response = new ResponseRMMaster();

            WMSDAL DAL = new WMSDAL();
            response = DAL.FetchpartcodeMasterDAL(request);
            return response;
        }
        public ResponseRMMaster InsertRMMasterBC(RequestRMMaster request)
        {
            ResponseRMMaster response = new ResponseRMMaster();
            response.ErrorContainer = Validate(request);

            WMSDAL DAL = new WMSDAL();
            response = DAL.InsertRMMasterDAL(request);

            return response;

        }

        public ResponseRMMaster EditRMMasterBC(RequestRMMaster request)
        {
            ResponseRMMaster response = new ResponseRMMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.EditRMMasterDAL(request);
            return response;

        }
        public ResponseRMMaster UpdateRMMasterBC(RequestRMMaster request)
        {
            ResponseRMMaster response = new ResponseRMMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.UpdateRMMasterDAL(request);
            return response;

        }

        public List<ErrorItem> Validate(RequestRMMaster request)
        {
            List<ErrorItem> err = new List<ErrorItem>();
            if (request.requestrmmaster.PLANTCODE == "")
                err.Add(new ErrorItem { DataItem = "Plant Code", ErrorNo = "SSB0009" });
            if (request.requestrmmaster.LOCATION == "")
                err.Add(new ErrorItem { DataItem = "Location", ErrorNo = "SSB0009" });
            if (request.requestrmmaster.RMITEMCODE == "")
                err.Add(new ErrorItem { DataItem = "RM Item Code", ErrorNo = "SSB0009" });
            if (request.requestrmmaster.UOM == "")
                err.Add(new ErrorItem { DataItem = "UOM", ErrorNo = "SSB0010" });
            if (request.requestrmmaster.PACKSIZE == "")
                err.Add(new ErrorItem { DataItem = "Pack Size", ErrorNo = "SSB0009" });
            if (request.requestrmmaster.GROUP == "")
                err.Add(new ErrorItem { DataItem = "Group", ErrorNo = "SSB0009" });
            if (request.requestrmmaster.RECORDSTATUS == "")
                err.Add(new ErrorItem { DataItem = "Record Status", ErrorNo = "SSB0010" });

            return err;
        }
    }
}
