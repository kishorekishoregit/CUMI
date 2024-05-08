using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIBC
{
    public class SupplierMasterBC
    {
        public ResponseSupplierMaster PageloadSupplierMasterBC()
        {
            ResponseSupplierMaster response = new ResponseSupplierMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.PageloadSupplierMasterDAL();
            return response;
        }
        public ResponseSupplierMaster InsertSupplierMasterBC(RequestSupplierMaster request)
        {
            ResponseSupplierMaster response = new ResponseSupplierMaster();
            response.ErrorContainer = Validate(request);

            WMSDAL DAL = new WMSDAL();
            response = DAL.InsertSupplierMasterDAL(request);

            return response;

        }
        public ResponseSupplierMaster EditSupplierMasterBC(RequestSupplierMaster request)
        {
            ResponseSupplierMaster response = new ResponseSupplierMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.EditSupplierMasterDAL(request);
            return response;

        }
        public ResponseSupplierMaster UpdateSupplierMasterBC(RequestSupplierMaster request)
        {
            ResponseSupplierMaster response = new ResponseSupplierMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.UpdateSupplierMasterDAL(request);
            return response;

        }
        public List<ErrorItem> Validate(RequestSupplierMaster request)
        {
            List<ErrorItem> err = new List<ErrorItem>();
            if (request.requestsuppliermaster.SUPPLIERCODE == "")
                err.Add(new ErrorItem { DataItem = "Supplier Code", ErrorNo = "SSB0009" });
            if (request.requestsuppliermaster.SUPPLIERNAME == "")
                err.Add(new ErrorItem { DataItem = "Supplier Name", ErrorNo = "SSB0009" });
            if (request.requestsuppliermaster.ADDRESS == "")
                err.Add(new ErrorItem { DataItem = "Address", ErrorNo = "SSB0009" });
            if (request.requestsuppliermaster.RECORDSTATUS == "")
                err.Add(new ErrorItem { DataItem = "Record Status", ErrorNo = "SSB0010" });

            return err;
        }
    }
}
