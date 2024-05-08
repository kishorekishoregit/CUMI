using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIBC
{
  public class RDCPicklistAssignBC

    {
        public ResponseRDCPicklistAssign RDCPicklistAssignPageLoadBC(RequestRDCPicklistAssign request)
        {
            ResponseRDCPicklistAssign response = new ResponseRDCPicklistAssign();
            WMSDAL DAL = new WMSDAL();
            response = DAL.PageloadRDCPicklistAssignDAL();
            return response;
        }
        public ResponseRDCPicklistAssign FetchRDCNoassignBC(RequestRDCPicklistAssign request)
        {
            ResponseRDCPicklistAssign response = new ResponseRDCPicklistAssign();

            WMSDAL DAL = new WMSDAL();
            response = DAL.FetchRdcnofetchdetailsDAL(request);
            return response;
        }
        public ResponseRDCPicklistAssign InsertRDCNOassignBC(RequestRDCPicklistAssign request)
        {
            ResponseRDCPicklistAssign response = new ResponseRDCPicklistAssign();
            WMSDAL DAL = new WMSDAL();
            response = DAL.InsertRDCPicklistAssignDetailsDAL(request);
            return response;

        }

        public ResponseRDCPicklistAssign ViewRDCNoBC(RequestRDCPicklistAssign request)
        {
            ResponseRDCPicklistAssign response = new ResponseRDCPicklistAssign();
            WMSDAL DAL = new WMSDAL();
            response = DAL.RDCPicklistAssignViewDtsDAL(request);
            return response;

        }
        public List<ErrorItem> Validate(RequestRDCPicklistAssign request)
        {
            List<ErrorItem> err = new List<ErrorItem>();
            if (request.requestrdcheaderdetails.RDCNO == "")
                err.Add(new ErrorItem { DataItem = "RDC Number", ErrorNo = "SSB0009" });
            if (request.requestrdcheaderdetails.LOCATION == "")
                err.Add(new ErrorItem { DataItem = "Location", ErrorNo = "SSB0009" });
            if (request.requestrdcheaderdetails.DATE == "")
                err.Add(new ErrorItem { DataItem = "Date", ErrorNo = "SSB0009" });
            if (request.requestrdcheaderdetails.SUPPLIER == "")
                err.Add(new ErrorItem { DataItem = "Supplier", ErrorNo = "SSB0009" });
            if (request.requestrdcheaderdetails.WAREHOUSEPICKER == "")
                err.Add(new ErrorItem { DataItem = "Warehouse Picker", ErrorNo = "SSB0009" });
            if (request.requestrdcassigndetails.Count == 0)
                err.Add(new ErrorItem { DataItem = "Please, Atleast one data detail to proceed!!!", ErrorNo = "SSB0009" });


            return err;
        }
    }
}
