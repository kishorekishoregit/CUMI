using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CUMIENTITY;
using System.Transactions;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;

namespace CUMIDAC
{
    public partial class WMSDAL
    {
        public ResponseLoginDetails LoginDAL(RequestLoginDetails request)
        {

            ResponseLoginDetails response = new ResponseLoginDetails();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("Users.LoginCheck", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@USERNAME", request.requestLoginDetails.UserName);
                        cmd.Parameters.AddWithValue("@USERPASSWORD", request.requestLoginDetails.Password);
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                            {
                                response.JS_LoginDetails = ds.Tables[0];//Login Details
                                response.JS_ScreenDetails = ds.Tables[1];//Screen Details
                                response.JS_LoginEmployeeCode = ds.Tables[3];//Login Emp Code
                                response.result = true;
                            }
                            else
                            {
                                response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestLoginDetails.UserName.ToUpper() });
                                response.result = false;
                            }
                        }
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("LoginDAL: " + "Method Name LoginDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "QIMS0000" });
                response.result = false;
            }

      

            return response;
        }

        public ResponseLoginDetails FetchUserDetails(RequestLoginDetails request)
        {
            ResponseLoginDetails response = new ResponseLoginDetails();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("Users.UserDetails", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EMPLOYEECODE", request.requestLoginDetails.EmployeeCode);
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_LoginDetails = ds.Tables[0];//Login Details
                            response.result = true;
                        }
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("LoginDAL: " + "Method Name LoginDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "QIMS0000" });
                response.result = false;
            }

  

            return response;
        }

        public ResponseLoginDetails ChangePassWord(RequestLoginDetails request)
        {
           

            ResponseLoginDetails response = new ResponseLoginDetails();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("Users.ChangePassword", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EMPLOYEECODE", request.requestLoginDetails.EmployeeCode);
                        cmd.Parameters.AddWithValue("@USERNAME", request.requestLoginDetails.UserName);
                        cmd.Parameters.AddWithValue("@USERPASSWORD", request.requestLoginDetails.Password);
                        cmd.Parameters.AddWithValue("@MODIFIEDBY", request.requestLoginDetails.Modifiedby);
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                            {
                                response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestLoginDetails.UserName.ToUpper() });
                                response.result = true;
                            }
                            else
                            {
                                response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestLoginDetails.UserName.ToUpper() });
                                response.result = false;
                            }
                        }
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("LoginDAL: " + "Method Name LoginDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "QIMS0000" });
                response.result = false;
            }


            return response;
        }
    }
}
