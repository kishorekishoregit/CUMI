using System;
using System.Collections.Generic;
using System.Data;
using System.Transactions;
using System.Data.SqlClient;
using System.Linq;
using CUMIENTITY;

using System.Text;

namespace CUMIDAC
{
    public partial class WMSDAL
    {

        public ResponseLoadingTypeMaster LoadingTypeMasterloadDAL()
        {
            ResponseLoadingTypeMaster response = new ResponseLoadingTypeMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[LOADINGMASTER_PAGELOAD]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_STATUS = ds.Tables[0];
                            response.JS_loadingtypedetails = ds.Tables[1];

                            response.result = true;
                        }
                        scope.Complete();

                    }
                }
            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("ARacktrackingSystempageloadDAL: " + "Method Name ARacktrackingSystempageloadDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }
            return response;
        }
        public ResponseLoadingTypeMaster InsertLoadingTypeMasterDAL(RequestLoadingTypeMaster request)
        {

            ResponseLoadingTypeMaster response = new ResponseLoadingTypeMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[LOADINGTYPEMASTER_INSERT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@LOADINGTYPE", request.requestLoadingTypeMaster.LOADINGTYPE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@STATUS", request.requestLoadingTypeMaster.STATUS.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestLoadingTypeMaster.USERCODE));

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

                            response.JS_loadingtypedetails = ds.Tables[0];
                            response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestLoadingTypeMaster.LOADINGTYPE.ToUpper() });
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {

                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("InsertCustomerMasterDAL: " + "Method Name InsertCustomerMasterDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }


            return response;

        }
        public ResponseLoadingTypeMaster EDITLoadingTypeMasterDAL(RequestLoadingTypeMaster request)
        {

            ResponseLoadingTypeMaster response = new ResponseLoadingTypeMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[LOADINGTYPE_EDIT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestLoadingTypeMaster.AUTOID.Trim().ToUpper()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_loadingtypedetails = ds.Tables[0];
                            response.result = true;
                        }
                        response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestLoadingTypeMaster.AUTOID.ToUpper() });
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
        public ResponseLoadingTypeMaster UpdateLoadingTypeMasterDAL(RequestLoadingTypeMaster request)
        {

            ResponseLoadingTypeMaster response = new ResponseLoadingTypeMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[LOADINGTYPE_UPDATE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestLoadingTypeMaster.AUTOID.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@LOADINGTYPE", request.requestLoadingTypeMaster.LOADINGTYPE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@STATUS", request.requestLoadingTypeMaster.STATUS.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestLoadingTypeMaster.USERCODE));

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

                            response.JS_loadingtypedetails = ds.Tables[0];
                            response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestLoadingTypeMaster.LOADINGTYPE.ToUpper() });
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {

                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("InsertCustomerMasterDAL: " + "Method Name InsertCustomerMasterDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }


            return response;

        }
    }
}
