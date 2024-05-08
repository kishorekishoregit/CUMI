using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Linq;
using System.Text;
using CUMIENTITY;

namespace CUMIDAC
{
    public partial class WMSDAL
    {

        public ResponseCityMaster CityMasterpageloadDAL()
        {
            ResponseCityMaster response = new ResponseCityMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[CITYMASTER_PAGELOAD]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_ZONE = ds.Tables[0];
                            response.JS_STATUS = ds.Tables[1];
                            response.JS_Statedetails = ds.Tables[2];

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
        public ResponseCityMaster InsertCityMasterDAL(RequestCityMaster request)
        {

            ResponseCityMaster response = new ResponseCityMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[CITYMASTER_INSERT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ZONE", request.requestCityMaster.ZONE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@STATE", request.requestCityMaster.STATE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@CITY", request.requestCityMaster.CITY.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@STATUS", request.requestCityMaster.STATUS.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestCityMaster.USERCODE));

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

                            response.JS_Statedetails = ds.Tables[0];
                            response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestCityMaster.CITY.ToUpper() });
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
        public ResponseCityMaster EDITCityMasterDAL(RequestCityMaster request)
        {

            ResponseCityMaster response = new ResponseCityMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[CITYMASTER_EDIT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestCityMaster.AUTOID.Trim().ToUpper()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_Statedetails = ds.Tables[0];
                            response.result = true;
                        }
                        response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestCityMaster.AUTOID.ToUpper() });
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
        public ResponseCityMaster UpdateCityMasterDAL(RequestCityMaster request)
        {

            ResponseCityMaster response = new ResponseCityMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[CITYMASTER_UPDATE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestCityMaster.AUTOID.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@ZONE", request.requestCityMaster.ZONE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@STATE", request.requestCityMaster.STATE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@CITY", request.requestCityMaster.CITY.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@STATUS", request.requestCityMaster.STATUS.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestCityMaster.USERCODE));

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

                            response.JS_Statedetails = ds.Tables[0];
                            response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestCityMaster.CITY.ToUpper() });
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
        public ResponseCityMaster FetchZoneCityMasterDAL(RequestCityMaster request)
        {

            ResponseCityMaster response = new ResponseCityMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[CITYMASTER_FETCHSTATE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ZONE", request.requestCityMaster.ZONE.Trim().ToUpper()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_Statedetails = ds.Tables[0];
                            response.result = true;
                        }
                        response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestCityMaster.ZONE.ToUpper() });
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
    }
}
