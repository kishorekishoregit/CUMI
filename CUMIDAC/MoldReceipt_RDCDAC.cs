using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Globalization;

namespace CUMIDAC
{
    public partial class WMSDAL
    {
        public ResponseMoldReceipt_RDC MoldReceipt_RDCPageloadDAL()
        {
            ResponseMoldReceipt_RDC response = new ResponseMoldReceipt_RDC();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[MOLDRECEIPT-RDC_PAGELOAD]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_RDCNo = ds.Tables[0];
                            response.JS_MoldReceiptPageload = ds.Tables[1];
                            response.result = true;
                        }
                        scope.Complete();

                    }
                }
            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("MoldReceipt_RDCPageloadDAL: " + "Method Name MoldReceipt_RDCPageloadDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }
            return response;
        }
        public ResponseMoldReceipt_RDC MoldReceipt_RDCFetchDetailsByRDCNODAL(RequestMoldReceipt_RDC request)
        {
            ResponseMoldReceipt_RDC response = new ResponseMoldReceipt_RDC();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[MOLDRECEIPT-RDC_FETCHDETAILSBYRDCNO]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@RDCNO", request.requestmoldrecieptrdc.RDCNO.ToString()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_MoldReceiptDetails= ds.Tables[0];
                            response.result = true;
                        }
                        scope.Complete();

                    }
                }
            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("MoldReceipt_RDCFetchDetailsByRDCNODAL: " + "Method Name MoldReceipt_RDCFetchDetailsByRDCNODAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }
            return response;
        }
        public ResponseMoldReceipt_RDC MoldReceiptRDCInsertDAL(RequestMoldReceipt_RDC request)
        {
            ResponseMoldReceipt_RDC response = new ResponseMoldReceipt_RDC();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                DataSet ds = new DataSet();
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        con.Open();
                        int rowcount = 0;
                        foreach (MoldReceipt_RDCDetailsEntity det in request.requestmoldreceiptrdcdetails)
                        {
                            SqlCommand cmd1 = new SqlCommand("[TRANSACTIONS].[MOLDRECEIPT-RDC_INSERT]", con);
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Parameters.Add(new SqlParameter("@RDCNO", request.requestmoldrecieptrdc.RDCNO.ToString().ToUpper()));
                            cmd1.Parameters.Add(new SqlParameter("@RFIDNO", det.RFIDNO.Trim().ToUpper()));
                            cmd1.Parameters.Add(new SqlParameter("@ITEMCODE", det.ITEMCODE));
                            cmd1.Parameters.Add(new SqlParameter("@ITEMNAME", det.ITEMNAME));
                            //cmd1.Parameters.Add(new SqlParameter("@UOM", det.UOM));
                            cmd1.Parameters.Add(new SqlParameter("@SUPPLIER", det.SUPPLIER));
                            cmd1.Parameters.Add(new SqlParameter("@QUANTITY", det.QUANTITY.Trim().ToUpper()));
                            cmd1.Parameters.Add(new SqlParameter("@USERCODE", request.requestmoldrecieptrdc.USERCODE.Trim().ToUpper()));
                            SqlDataAdapter oda1 = new SqlDataAdapter(cmd1);
                            oda1.Fill(ds);
                            if (ds != null)
                            {
                                if (ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][0].ToString() == "SUCCESS")
                                {
                                    rowcount++;
                                }
                            }
                        }
                       
                       
                        if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS" && request.requestmoldreceiptrdcdetails.Count == rowcount)
                        {
                            scope.Complete();
                            response.result = true;
                            response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = ds.Tables[0].Rows[0][2].ToString() });
                        }
                        else
                        {
                            response.result = false;
                            response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = ds.Tables[0].Rows[0][2].ToString() });
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("MoldReceiptRDCInsertDAL: " + "Method Name MoldReceiptRDCInsertDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }

            return response;
        }
    }
}
