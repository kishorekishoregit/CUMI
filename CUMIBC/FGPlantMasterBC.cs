using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CUMIBC
{
  public class FGPlantMasterBC
    {
        public ResponseFGPlantMaster FGPlantMasterPageLoadBC()
        {
            ResponseFGPlantMaster response = new ResponseFGPlantMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.PageloadFGPlantMasterDAL();
            return response;
        }
        public ResponseFGPlantMaster FetchLocationPartcodeBC(RequestFGPlantMaster request)
        {
            ResponseFGPlantMaster response = new ResponseFGPlantMaster();

            WMSDAL DAL = new WMSDAL();
            response = DAL.FetchpartcodeMasterDAL(request);
            return response;
        }
        public ResponseFGPlantMaster InsertFGPlantMasterBC(RequestFGPlantMaster request)
        {
            ResponseFGPlantMaster response = new ResponseFGPlantMaster();
            response.ErrorContainer = Validate(request);

            WMSDAL DAL = new WMSDAL();
            response = DAL.InsertFGPlantMasterDAL(request);

            return response;

        }

        public ResponseFGPlantMaster EditFGPlantMasterBC(RequestFGPlantMaster request)
        {
            ResponseFGPlantMaster response = new ResponseFGPlantMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.EditFGPlantMasterDAL(request);
            return response;

        }
        public ResponseFGPlantMaster UpdateFGPlantMasterBC(RequestFGPlantMaster request)
        {
            ResponseFGPlantMaster response = new ResponseFGPlantMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.UpdateFGPlantMasterDAL(request);
            return response;

        }

        public List<ErrorItem> Validate(RequestFGPlantMaster request)
        {
            List<ErrorItem> err = new List<ErrorItem>();
            if (request.requestfgplantmaster.PLANTCODE == "")
                err.Add(new ErrorItem { DataItem = "Plant Code", ErrorNo = "SSB0009" });
            if (request.requestfgplantmaster.LOCATION == "")
                err.Add(new ErrorItem { DataItem = "Location", ErrorNo = "SSB0009" });
            if (request.requestfgplantmaster.FGITEMCODE == "")
                err.Add(new ErrorItem { DataItem = "FG Item Code", ErrorNo = "SSB0009" });
            if (request.requestfgplantmaster.UOM == "")
                err.Add(new ErrorItem { DataItem = "UOM", ErrorNo = "SSB0010" });
            if (request.requestfgplantmaster.Weight == "")
                err.Add(new ErrorItem { DataItem = "Weight", ErrorNo = "SSB0009" });
            if (request.requestfgplantmaster.GROUP == "")
                err.Add(new ErrorItem { DataItem = "Group", ErrorNo = "SSB0009" });
            if (request.requestfgplantmaster.RECORDSTATUS == "")
                err.Add(new ErrorItem { DataItem = "Record Status", ErrorNo = "SSB0010" });

            return err;
        }
    }
}
