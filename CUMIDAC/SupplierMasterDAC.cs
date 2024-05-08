using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CUMIDAC
{
    public partial class WMSDAL
    {
        public ResponseSupplierMaster PageloadSupplierMasterDAL()
        {
            ResponseSupplierMaster response = new ResponseSupplierMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[SUPPLIERMASTER_PAGELOAD]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.Js_Status = ds.Tables[0];
                            response.Js_SupplierDts = ds.Tables[1];
                            response.result = true;
                        }
                        scope.Complete();
                    }
                }


            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("PageloadSupplierMasterDAL: " + "Method Name PageloadSupplierMasterDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }



            return response;
        }
        public ResponseSupplierMaster InsertSupplierMasterDAL(RequestSupplierMaster request)
        {

            ResponseSupplierMaster response = new ResponseSupplierMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[SUPPLIERMASTER_INSERT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@SUPPLIERCODE", request.requestsuppliermaster.SUPPLIERCODE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@SUPPLIERNAME", request.requestsuppliermaster.SUPPLIERNAME.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@ADDRESS", request.requestsuppliermaster.ADDRESS.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", request.requestsuppliermaster.RECORDSTATUS));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestsuppliermaster.USERCODE));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                                response.result = true;
                            else
                                response.result = false;
                            response.Js_SupplierDts = ds.Tables[0];// get the Shift Details
                        }
                        response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestsuppliermaster.SUPPLIERCODE.ToUpper() });
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {

                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("InsertSupplierMasterDAL: " + "Method Name InsertSupplierMasterDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }

            return response;

        }
        public ResponseSupplierMaster EditSupplierMasterDAL(RequestSupplierMaster request)
        {
            ResponseSupplierMaster response = new ResponseSupplierMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[SUPPLIERMASTER_EDIT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestsuppliermaster.AUTOID.Trim().ToUpper()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.Js_SupplierDts = ds.Tables[0];
                            response.result = true;
                        }

                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {

                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("EditSupplierMasterDAL: " + "Method Name EditSupplierMasterDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }
            return response;
        }
        public ResponseSupplierMaster UpdateSupplierMasterDAL(RequestSupplierMaster request)
        {
            ResponseSupplierMaster response = new ResponseSupplierMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[SUPPLIERMASTER_UPDATE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestsuppliermaster.AUTOID.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@SUPPLIERCODE", request.requestsuppliermaster.SUPPLIERCODE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@SUPPLIERNAME", request.requestsuppliermaster.SUPPLIERNAME.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@ADDRESS", request.requestsuppliermaster.ADDRESS.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", request.requestsuppliermaster.RECORDSTATUS));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestsuppliermaster.USERCODE));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                                response.result = true;
                            else
                                response.result = false;
                            response.Js_SupplierDts = ds.Tables[0];// get the Shift Details
                        }
                        response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestsuppliermaster.SUPPLIERCODE.ToUpper() });
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {

                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("UpdateSupplierMasterDAL: " + "Method Name UpdateSupplierMasterDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }

            return response;

        }

    }
}
