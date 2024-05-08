using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CUMIENTITY;
using CUMIDAC;

namespace CUMIBC
{
    public class CityMasterBC
    {
        public ResponseCityMaster CityMasterpageloadBC()
        {
            ResponseCityMaster response = new ResponseCityMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.CityMasterpageloadDAL();
            return response;
        }
        public ResponseCityMaster InsertCityMasterBC(RequestCityMaster request)
        {
            ResponseCityMaster response = new ResponseCityMaster();

            WMSDAL DAL = new WMSDAL();
            response = DAL.InsertCityMasterDAL(request);
            return response;

        }
        public ResponseCityMaster EDITCityMasterBC(RequestCityMaster request)
        {
            ResponseCityMaster response = new ResponseCityMaster();

            WMSDAL DAL = new WMSDAL();
            response = DAL.EDITCityMasterDAL(request);
            return response;

        }
        public ResponseCityMaster UpateCityMasterBC(RequestCityMaster request)
        {
            ResponseCityMaster response = new ResponseCityMaster();

            WMSDAL DAL = new WMSDAL();
            response = DAL.UpdateCityMasterDAL(request);
            return response;

        }
        public ResponseCityMaster FetchZoneCityMasterBC(RequestCityMaster request)
        {
            ResponseCityMaster response = new ResponseCityMaster();

            WMSDAL DAL = new WMSDAL();
            response = DAL.FetchZoneCityMasterDAL(request);
            return response;

        }

    }
}
