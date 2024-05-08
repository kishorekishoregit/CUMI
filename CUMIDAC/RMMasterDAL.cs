using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CUMIDAC
{
    public partial class WMSDAL
    {
        public ResponseRMMaster PageloadRMMasterDAL()
        {
            ResponseRMMaster response = new ResponseRMMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[RMMASTER_PAGELOAD]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {

                            response.JS_Recordstatus = ds.Tables[0];
                            response.JS_UOM = ds.Tables[1];
                            response.JS_Plant = ds.Tables[2];
                            response.JS_Group = ds.Tables[3];
                            response.JS_ITEMSUBGROUP = ds.Tables[4];
                            response.JS_CATEGORY = ds.Tables[5];
                            response.JS_rmmasterDetails = ds.Tables[6];
                            response.result = true;
                        }
                        scope.Complete();
                    }
                }


            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("FetchCustomerMasterPageLoadDAL: " + "Method Name FetchCustomerMasterPageLoadDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }



            return response;
        }

        public ResponseRMMaster FetchpartcodeMasterDAL(RequestRMMaster request)
        {

            ResponseRMMaster response = new ResponseRMMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[FGLOCATIONMASTER_FETCHBYPARTCODE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@PLANTCODE", request.requestrmmaster.PLANTCODE.Trim().ToUpper()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_Plant = ds.Tables[0];
                            response.result = true;
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {

                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("FetchUserCreationbyUserCodeDAL: " + "Method Name FetchUserCreationbyUserCodeDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }



            return response;

        }
        public ResponseRMMaster InsertRMMasterDAL(RequestRMMaster request)
        {

            ResponseRMMaster response = new ResponseRMMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[RMMASTER_INSERT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@PLANTCODE", request.requestrmmaster.PLANTCODE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@LOCATION", request.requestrmmaster.LOCATION.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@RMITEMCODE", request.requestrmmaster.RMITEMCODE));
                        cmd.Parameters.Add(new SqlParameter("@VARIANTCODE", request.requestrmmaster.VARIANTCODE));
                        cmd.Parameters.Add(new SqlParameter("@DESCRIPTION", request.requestrmmaster.RMDESCRIPTION));
                        cmd.Parameters.Add(new SqlParameter("@UOM", request.requestrmmaster.UOM));
                        cmd.Parameters.Add(new SqlParameter("@PACKSIZE", request.requestrmmaster.PACKSIZE));
                        cmd.Parameters.Add(new SqlParameter("@GROUP", request.requestrmmaster.GROUP));
                        cmd.Parameters.Add(new SqlParameter("@ITEMSUBGROUP", request.requestrmmaster.ITEMSUBGROUP));
                        cmd.Parameters.Add(new SqlParameter("@CATEGORY", request.requestrmmaster.CATEGORY));
                        cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", request.requestrmmaster.RECORDSTATUS));
                        cmd.Parameters.Add(new SqlParameter("@REMARKS", request.requestrmmaster.REMARKS));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestrmmaster.USERCODE));
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
                            response.JS_rmmasterDetails = ds.Tables[0];// get the Shift Details
                        }
                        response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestrmmaster.RMITEMCODE.ToUpper() });
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {

                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("FetchUserCreationbyUserCodeDAL: " + "Method Name FetchUserCreationbyUserCodeDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }

            return response;

        }
        public ResponseRMMaster EditRMMasterDAL(RequestRMMaster request)
        {
            ResponseRMMaster response = new ResponseRMMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[RMMASTER_EDIT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestrmmaster.AUTOID.Trim().ToUpper()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_rmmasterDetails = ds.Tables[0];
                            response.result = true;
                        }

                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {

                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("EditCategoryMasterDAL: " + "Method Name EditCategoryMasterDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }
            return response;
        }
        public ResponseRMMaster UpdateRMMasterDAL(RequestRMMaster request)
        {
            ResponseRMMaster response = new ResponseRMMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[RMMASTER_UPDATE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestrmmaster.AUTOID.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@PLANTCODE", request.requestrmmaster.PLANTCODE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@LOCATION", request.requestrmmaster.LOCATION.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@RMITEMCODE", request.requestrmmaster.RMITEMCODE));
                        cmd.Parameters.Add(new SqlParameter("@VARIANTCODE", request.requestrmmaster.VARIANTCODE));
                        cmd.Parameters.Add(new SqlParameter("@DESCRIPTION", request.requestrmmaster.RMDESCRIPTION));
                        cmd.Parameters.Add(new SqlParameter("@UOM", request.requestrmmaster.UOM));
                        cmd.Parameters.Add(new SqlParameter("@PACKSIZE", request.requestrmmaster.PACKSIZE));
                        cmd.Parameters.Add(new SqlParameter("@GROUP", request.requestrmmaster.GROUP));
                        cmd.Parameters.Add(new SqlParameter("@ITEMSUBGROUP", request.requestrmmaster.ITEMSUBGROUP));
                        cmd.Parameters.Add(new SqlParameter("@CATEGORY", request.requestrmmaster.CATEGORY));
                        cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", request.requestrmmaster.RECORDSTATUS));
                        cmd.Parameters.Add(new SqlParameter("@REMARKS", request.requestrmmaster.REMARKS));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestrmmaster.USERCODE));
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
                            response.JS_rmmasterDetails = ds.Tables[0];// get the Shift Details
                        }
                        response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestrmmaster.RMITEMCODE.ToUpper() });
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {

                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("InsertShiftDetailsDAL: " + "Method Name InsertShiftDetailsDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }

            return response;

     
        }
    }
}
