using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIBC
{
   public class MoldShotCountBC
    {
        public ResponseMoldShotCount MoldshotcountPageLoadBC()
        {
            ResponseMoldShotCount response = new ResponseMoldShotCount();
            WMSDAL DAL = new WMSDAL();
            response = DAL.MoldShotCountPageloadDAL();
            return response;
        }
        public ResponseMoldShotCount FetchMoldshotcountDetailsBC(RequestMoldShotCount request)
        {
            ResponseMoldShotCount response = new ResponseMoldShotCount();

            WMSDAL DAL = new WMSDAL();
            response = DAL.MoldshotcountDetailDAL(request);
            return response;
        }

        public ResponseMoldShotCount FetchMoldshotcountRFIDDetailsBC(RequestMoldShotCount request)
        {
            ResponseMoldShotCount response = new ResponseMoldShotCount();

            WMSDAL DAL = new WMSDAL();
            response = DAL.MoldshotcountRFIDDetailDAL(request);
            return response;
        }
        public ResponseMoldShotCount InsertMoldshotcountBC(RequestMoldShotCount request)
        {
            ResponseMoldShotCount response = new ResponseMoldShotCount();
            response.ErrorContainer = Validate(request);

            WMSDAL DAL = new WMSDAL();
            response = DAL.MoldCountShoutInsertDAL(request);

            return response;

        }

        public ResponseMoldShotCount EditMoldshotcountBC(RequestMoldShotCount request)
        {
            ResponseMoldShotCount response = new ResponseMoldShotCount();
            WMSDAL DAL = new WMSDAL();
            response = DAL.EditMoldCountShoutDAL(request);
            return response;

        }
        public ResponseMoldShotCount UpdateMoldshotcountBC(RequestMoldShotCount request)
        {
            ResponseMoldShotCount response = new ResponseMoldShotCount();
            WMSDAL DAL = new WMSDAL();
            response = DAL.UpdateMoldCountShoutDAL(request);
            return response;

        }

        public List<ErrorItem> Validate(RequestMoldShotCount request)
        {
            List<ErrorItem> err = new List<ErrorItem>();
            if (request.requestmoldshotcount.MOLDITEMCODE == "")
                err.Add(new ErrorItem { DataItem = "Plant Code", ErrorNo = "SSB0010" });
            if (request.requestmoldshotcount.SHOTCOUNT == "")
                err.Add(new ErrorItem { DataItem = "Location", ErrorNo = "SSB0009" });
            if (request.requestmoldshotcount.DATEOFSHOT == "")
                err.Add(new ErrorItem { DataItem = "RM Item Code", ErrorNo = "SSB0009" });

            return err;
        }
    }
}
