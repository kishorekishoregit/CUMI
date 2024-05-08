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
        public ResponseMoldMaster PageloadMoldMasterDAL()
        {
            ResponseMoldMaster response = new ResponseMoldMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[MOLDMASTER_PAGELOAD]", con);
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
                            response.JS_group = ds.Tables[3];
                            response.JS_Itemsubgroup = ds.Tables[4];
                            response.JS_Category = ds.Tables[5];
                            response.JS_moldmasterDetails = ds.Tables[6];
                            response.JS_Moldtype = ds.Tables[7];
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

        public ResponseMoldMaster FetchpartcodeMasterDAL(RequestMoldMaster request)
        {

            ResponseMoldMaster response = new ResponseMoldMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[MOLDLOCATIONMASTER_FETCHBYPARTCODE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@PLANTCODE", request.requestrmoldmaster.PLANTCODE.Trim().ToUpper()));
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
        public ResponseMoldMaster InsertMoldMasterDAL(RequestMoldMaster request)
        {

            ResponseMoldMaster response = new ResponseMoldMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[MOLDMASTER_INSERT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;                       
                        cmd.Parameters.Add(new SqlParameter("@PLANTCODE", request.requestrmoldmaster.PLANTCODE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@LOCATION", request.requestrmoldmaster.LOCATION.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@MOLDTYPE", request.requestrmoldmaster.MOLDTYPE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@MOLDITEMCODE", request.requestrmoldmaster.MOLDITEMCODE));
                        cmd.Parameters.Add(new SqlParameter("@DESCRIPTION", request.requestrmoldmaster.MOLDDESCRIPTION));
                        //cmd.Parameters.Add(new SqlParameter("@PONUMBER", request.requestrmoldmaster.PONUMBER));
                       // cmd.Parameters.Add(new SqlParameter("@BATCHNUMBER", request.requestrmoldmaster.BATCHNUMBER));
                        cmd.Parameters.Add(new SqlParameter("@SUPPLIERCODE", request.requestrmoldmaster.SUPPLIERCODE));
                        cmd.Parameters.Add(new SqlParameter("@SUPPLIERNAME", request.requestrmoldmaster.SUPPLIERNAME));
                        cmd.Parameters.Add(new SqlParameter("@SUPPLIERADDRESS", request.requestrmoldmaster.SUPPLIERADDRESS));
                        cmd.Parameters.Add(new SqlParameter("@VARIANTCODE", request.requestrmoldmaster.VARIANTCODE));
                        cmd.Parameters.Add(new SqlParameter("@UOM", request.requestrmoldmaster.UOM));                      
                        cmd.Parameters.Add(new SqlParameter("@GROUP", request.requestrmoldmaster.GROUP));
                        cmd.Parameters.Add(new SqlParameter("@ITEMSUBGROUP", request.requestrmoldmaster.ITEMSUBGROUP));
                        cmd.Parameters.Add(new SqlParameter("@CATEGORY", request.requestrmoldmaster.CATEGORY));
                        cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", request.requestrmoldmaster.RECORDSTATUS));
                        cmd.Parameters.Add(new SqlParameter("@REMARKS", request.requestrmoldmaster.REMARKS));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestrmoldmaster.USERCODE));
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
                            response.JS_moldmasterDetails = ds.Tables[0];// get the Shift Details
                        }
                        response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestrmoldmaster.MOLDITEMCODE.ToUpper() });
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
        public ResponseMoldMaster EditMoldMasterDAL(RequestMoldMaster request)
        {
            ResponseMoldMaster response = new ResponseMoldMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[MOLDMASTER_EDIT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestrmoldmaster.AUTOID.Trim().ToUpper()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_moldmasterDetails = ds.Tables[0];
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
        public ResponseMoldMaster UpdateMoldMasterDAL(RequestMoldMaster request)
        {
            ResponseMoldMaster response = new ResponseMoldMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[MOLDMASTER_UPDATE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestrmoldmaster.AUTOID.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@PLANTCODE", request.requestrmoldmaster.PLANTCODE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@LOCATION", request.requestrmoldmaster.LOCATION.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@MOLDTYPE", request.requestrmoldmaster.MOLDTYPE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@MOLDITEMCODE", request.requestrmoldmaster.MOLDITEMCODE));
                        cmd.Parameters.Add(new SqlParameter("@DESCRIPTION", request.requestrmoldmaster.MOLDDESCRIPTION));
                       // cmd.Parameters.Add(new SqlParameter("@PONUMBER", request.requestrmoldmaster.PONUMBER));
                       // cmd.Parameters.Add(new SqlParameter("@BATCHNUMBER", request.requestrmoldmaster.BATCHNUMBER));
                        cmd.Parameters.Add(new SqlParameter("@SUPPLIERCODE", request.requestrmoldmaster.SUPPLIERCODE));
                        cmd.Parameters.Add(new SqlParameter("@SUPPLIERNAME", request.requestrmoldmaster.SUPPLIERNAME));
                        cmd.Parameters.Add(new SqlParameter("@SUPPLIERADDRESS", request.requestrmoldmaster.SUPPLIERADDRESS));
                        cmd.Parameters.Add(new SqlParameter("@VARIANTCODE", request.requestrmoldmaster.VARIANTCODE));
                        cmd.Parameters.Add(new SqlParameter("@UOM", request.requestrmoldmaster.UOM));
                        cmd.Parameters.Add(new SqlParameter("@GROUP", request.requestrmoldmaster.GROUP));
                        cmd.Parameters.Add(new SqlParameter("@ITEMSUBGROUP", request.requestrmoldmaster.ITEMSUBGROUP));
                        cmd.Parameters.Add(new SqlParameter("@CATEGORY", request.requestrmoldmaster.CATEGORY));
                        cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", request.requestrmoldmaster.RECORDSTATUS));
                        cmd.Parameters.Add(new SqlParameter("@REMARKS", request.requestrmoldmaster.REMARKS));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestrmoldmaster.USERCODE));
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
                            response.JS_moldmasterDetails = ds.Tables[0];// get the Shift Details
                        }
                        response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestrmoldmaster.MOLDITEMCODE.ToUpper() });
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
