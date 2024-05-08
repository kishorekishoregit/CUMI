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
        public ResponseChildPartMaster PageloadChildPartMasterDAL()
        {
            ResponseChildPartMaster response = new ResponseChildPartMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[CHILDPARTMASTER_PAGELOAD]", con);
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
                            response.JS_ChildpartmasterDetails = ds.Tables[3];
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

        public ResponseChildPartMaster FetchpartcodeMasterDAL(RequestChildPartMaster request)
        {

            ResponseChildPartMaster response = new ResponseChildPartMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[FGLOCATIONMASTER_FETCHBYPARTCODE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@PLANTCODE", request.requestchildpartmaster.PLANTCODE.Trim().ToUpper()));
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
        public ResponseChildPartMaster InsertChildPartMasterDAL(RequestChildPartMaster request)
        {

            ResponseChildPartMaster response = new ResponseChildPartMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[CHIDPARTMASTER_INSERT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@PLANTCODE", request.requestchildpartmaster.PLANTCODE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@LOCATION", request.requestchildpartmaster.LOCATION.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@CHILDITEMCODE", request.requestchildpartmaster.CHILDITEMCODE));
                        cmd.Parameters.Add(new SqlParameter("@DESCRIPTION", request.requestchildpartmaster.DESCRIPTION));
                        cmd.Parameters.Add(new SqlParameter("@UOM", request.requestchildpartmaster.UOM));
                        cmd.Parameters.Add(new SqlParameter("@QUANTITY", request.requestchildpartmaster.QUANTITY));
                        cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", request.requestchildpartmaster.RECORDSTATUS));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestchildpartmaster.USERCODE));
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
                            response.JS_ChildpartmasterDetails = ds.Tables[0];// get the Shift Details
                        }
                        response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestchildpartmaster.CHILDITEMCODE.ToUpper() });
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
        public ResponseChildPartMaster EditChildPartMasterDAL(RequestChildPartMaster request)
        {
            ResponseChildPartMaster response = new ResponseChildPartMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[CHILDPARTMASTER_EDIT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestchildpartmaster.AUTOID.Trim().ToUpper()));
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
                            response.JS_ChildpartmasterDetails = ds.Tables[0];// get the Shift Details
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
        public ResponseChildPartMaster UpdateChildPartMasterDAL(RequestChildPartMaster request)
        {
            ResponseChildPartMaster response = new ResponseChildPartMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[CHILDPARTMASTER_UPDATE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestchildpartmaster.AUTOID.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@PLANTCODE", request.requestchildpartmaster.PLANTCODE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@LOCATION", request.requestchildpartmaster.LOCATION.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@CHILDITEMCODE", request.requestchildpartmaster.CHILDITEMCODE));
                        cmd.Parameters.Add(new SqlParameter("@DESCRIPTION", request.requestchildpartmaster.DESCRIPTION));
                        cmd.Parameters.Add(new SqlParameter("@UOM", request.requestchildpartmaster.UOM));
                        cmd.Parameters.Add(new SqlParameter("@QUANTITY", request.requestchildpartmaster.QUANTITY));
                        cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", request.requestchildpartmaster.RECORDSTATUS));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestchildpartmaster.USERCODE));
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
                            response.JS_ChildpartmasterDetails = ds.Tables[0];// get the Shift Details
                        }
                        response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestchildpartmaster.CHILDITEMCODE.ToUpper() });
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
