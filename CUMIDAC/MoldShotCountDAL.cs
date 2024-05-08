using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.UI.WebControls;

namespace CUMIDAC
{
    public partial class WMSDAL
    {
        public ResponseMoldShotCount MoldShotCountPageloadDAL()
        {
            ResponseMoldShotCount response = new ResponseMoldShotCount();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[MOLDSHOTCOUNTENTRY_PAGELOAD]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_Molddetails = ds.Tables[0];
                            response.JS_shotdetails = ds.Tables[1];
                            response.result = true;
                        }
                        scope.Complete();

                    }
                }
            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("MoldShotCountPageloadDAL: " + "Method Name MoldReceipt_RDCPageloadDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }
            return response;
        }
        public ResponseMoldShotCount MoldshotcountDetailDAL(RequestMoldShotCount request)
        {
            ResponseMoldShotCount response = new ResponseMoldShotCount();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[MOLDSHOTCOUNTENTRY_FETCHDETAILS]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MOLDITEMCODE", request.requestmoldshotcount.MOLDITEMCODE.ToString()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_Molddetails = ds.Tables[0];
                            response.result = true;
                        }
                        scope.Complete();

                    }
                }
            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("MoldshotcountDetailDAL: " + "Method Name MoldShotCount" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }
            return response;
        }

        public ResponseMoldShotCount MoldshotcountRFIDDetailDAL(RequestMoldShotCount request)
        {
            ResponseMoldShotCount response = new ResponseMoldShotCount();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[MOLDSHOTCOUNTENTRY_RFIDFETCHDETAILS]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@RFID", request.requestmoldshotcount.MOLDITEMCODE.ToString()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_Molddetails = ds.Tables[0];
                            response.result = true;
                        }
                        scope.Complete();

                    }
                }
            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("MoldshotcountDetailDAL: " + "Method Name MoldShotCount" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }
            return response;
        }
        public ResponseMoldShotCount MoldCountShoutInsertDAL(RequestMoldShotCount request)
        {
            ResponseMoldShotCount response = new ResponseMoldShotCount();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                DataSet ds = new DataSet();
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[MOLDSHOTCOUNTENTRY_INSERT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MOLDITEMCODE", request.requestmoldshotcount.MOLDITEMCODE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@RFIDNUMBER", request.requestmoldshotcount.RFIDNUMBER.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@MOLDITEMSESCRIPTION", request.requestmoldshotcount.MOLDNAME.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@FGITEMCODE", request.requestmoldshotcount.FGITEMCODE.Trim()));
                        //cmd.Parameters.Add(new SqlParameter("@CHILDITEMCODE", request.requestmoldshotcount.CHILDITEMCODE));
                        cmd.Parameters.Add(new SqlParameter("@SHOTCOUNT", request.requestmoldshotcount.SHOTCOUNT));
                        cmd.Parameters.Add(new SqlParameter("@DATEOFSHOT", request.requestmoldshotcount.DATEOFSHOT));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestmoldshotcount.USERCODE));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                                response.result = true;
                            else
                                response.result = false;
                            response.JS_shotdetails = ds.Tables[0];// get the Shift Details
                        }
                        response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestmoldshotcount.MOLDITEMCODE.ToUpper() });
                      
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("MoldShotCountInsertDAL: " + "Method Name MoldShotCountInsertDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }

            return response;
        }

        public ResponseMoldShotCount EditMoldCountShoutDAL(RequestMoldShotCount request)
        {
            ResponseMoldShotCount response = new ResponseMoldShotCount();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[MOLDSHOTCOUNTENTRY_EDIT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestmoldshotcount.AUTOID.Trim().ToUpper()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_shotdetails = ds.Tables[0];
                            response.result = true;
                        }

                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {

                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("EditMoldShotCountDAL: " + "Method Name EditMoldShotCountDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }
            return response;
        }
        public ResponseMoldShotCount UpdateMoldCountShoutDAL(RequestMoldShotCount request)
        {
            ResponseMoldShotCount response = new ResponseMoldShotCount();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[MOLDSHOTCOUNTENTRY_UPDATE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestmoldshotcount.AUTOID.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@MOLDITEMCODE", request.requestmoldshotcount.MOLDITEMCODE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@RFIDNUMBER", request.requestmoldshotcount.RFIDNUMBER.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@MOLDITEMSESCRIPTION", request.requestmoldshotcount.MOLDNAME.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@FGITEMCODE", request.requestmoldshotcount.FGITEMCODE.Trim()));
                       // cmd.Parameters.Add(new SqlParameter("@CHILDITEMCODE", request.requestmoldshotcount.CHILDITEMCODE));
                        cmd.Parameters.Add(new SqlParameter("@SHOTCOUNT", request.requestmoldshotcount.SHOTCOUNT));
                        cmd.Parameters.Add(new SqlParameter("@DATEOFSHOT", request.requestmoldshotcount.DATEOFSHOT));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestmoldshotcount.USERCODE));
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
                            response.JS_shotdetails = ds.Tables[0];// get the Shift Details
                        }
                        response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestmoldshotcount.MOLDITEMCODE.ToUpper() });
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
