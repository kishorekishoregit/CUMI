using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CUMIBC
{
   public class PlantMastersBC
    {
        public ResponsePlantMaster PlantMasterPageLoadBC()
        {
            ResponsePlantMaster response = new ResponsePlantMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.PlantMasterPageLoadDAL();

            return response;
        }

        public ResponsePlantMaster InsertPlantMasterBC(RequestPlantMaster request)
        {

            ResponsePlantMaster response = new ResponsePlantMaster();
            response.ErrorContainer = Validate(request);
            if (response.ErrorContainer.Count == 0)
            {
                WMSDAL DAL = new WMSDAL();
                response = DAL.InsertPlantMasterDAL(request);
            }
            return response;

        }

        public ResponsePlantMaster EditPlantMasterByIDBC(RequestPlantMaster request)
        {

            ResponsePlantMaster response = new ResponsePlantMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.EditPlantMasterDetailIdDAL(request);

            return response;


        }

        public ResponsePlantMaster UpdateCustomerMasterBC(RequestPlantMaster request)
        {

            ResponsePlantMaster response = new ResponsePlantMaster();
            response.ErrorContainer = Validate(request);
            if (response.ErrorContainer.Count == 0)
            {
                WMSDAL DAL = new WMSDAL();
                response = DAL.UpdatePlantMasterDAL(request);
            }
            return response;

        }

        public List<ErrorItem> Validate(RequestPlantMaster request)
        {
            List<ErrorItem> err = new List<ErrorItem>();
            if (request.requestPlantMaster.PLANTCODE == "")
                err.Add(new ErrorItem { DataItem = "Plant Code", ErrorNo = "SSB0009" });
            if (request.requestPlantMaster.PLANTNAME == "")
                err.Add(new ErrorItem { DataItem = "Plant Name", ErrorNo = "SSB0009" });         
            if (request.requestPlantMaster.RECORDSTATUS == "")
                err.Add(new ErrorItem { DataItem = "Record Status", ErrorNo = "SSB0010" });
            return err;
        }
    }
}
