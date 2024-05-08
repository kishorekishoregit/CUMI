using CUMIDAC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIBC
{
    public class MoldReceipt_RDCBC
    {
        public ResponseMoldReceipt_RDC MoldReceipt_RDCPageloadBC()
        {
            ResponseMoldReceipt_RDC response = new ResponseMoldReceipt_RDC();
            WMSDAL DAL = new WMSDAL();
            response = DAL.MoldReceipt_RDCPageloadDAL();
            return response;
        }
        public ResponseMoldReceipt_RDC MoldReceipt_RDCFetchDetailsByRDCNOBC(RequestMoldReceipt_RDC request)
        {
            ResponseMoldReceipt_RDC response = new ResponseMoldReceipt_RDC();
            WMSDAL DAL = new WMSDAL();
            response = DAL.MoldReceipt_RDCFetchDetailsByRDCNODAL(request);
            return response;
        }
        public ResponseMoldReceipt_RDC MoldReceiptRDCInsertBC(RequestMoldReceipt_RDC request)
        {
            ResponseMoldReceipt_RDC response = new ResponseMoldReceipt_RDC();
            WMSDAL DAL = new WMSDAL();
            response = DAL.MoldReceiptRDCInsertDAL(request);
            return response;
        }
    }
}
