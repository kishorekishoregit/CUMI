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
        public ResponseWarehouseMaster WarehouseMasterPageLoadDAL()
        {
            ResponseWarehouseMaster response = new ResponseWarehouseMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[WAREHOUSEMASTER_PAGELOAD]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_Plant = ds.Tables[0];
                            response.JS_STATUS = ds.Tables[1];
                            response.JS_Warehousedetails = ds.Tables[2];
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

        public ResponseWarehouseMaster FetchWarehouseMasterDAL(RequestWarehouseMaster request)
        {

            ResponseWarehouseMaster response = new ResponseWarehouseMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[WAREHOUSEMASTER_FETCHBYPARTCODE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@PLANTCODE", request.requestWarehouseMaster.PLANTCODE.Trim().ToUpper()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_Plant = ds.Tables[0];
                            response.result = true;
                        }
                        response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestWarehouseMaster.PLANTCODE.ToUpper() });
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
        public ResponseWarehouseMaster InsertWarehouseMasterDAL(RequestWarehouseMaster request)
        {

            ResponseWarehouseMaster response = new ResponseWarehouseMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[WAREHOUSEMASTER_INSERT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@WHCODE", request.requestWarehouseMaster.WAREHOUSECODE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@WHNAME", request.requestWarehouseMaster.WAREHOUSENAME.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@PLANT", request.requestWarehouseMaster.PLANTCODE));
                        cmd.Parameters.Add(new SqlParameter("@SUPERVISOR", request.requestWarehouseMaster.SUPERVISOR));
                        cmd.Parameters.Add(new SqlParameter("@ADDRESS1", request.requestWarehouseMaster.ADDRESS1));
                        cmd.Parameters.Add(new SqlParameter("@ADDRESS2", request.requestWarehouseMaster.ADDRESS2));
                        cmd.Parameters.Add(new SqlParameter("@ADDRESS3", request.requestWarehouseMaster.ADDRESS3));
                        cmd.Parameters.Add(new SqlParameter("@CITY", request.requestWarehouseMaster.CITY));
                        cmd.Parameters.Add(new SqlParameter("@STATE", request.requestWarehouseMaster.STATE));
                        cmd.Parameters.Add(new SqlParameter("@PINCODE", request.requestWarehouseMaster.PINCODE));
                        cmd.Parameters.Add(new SqlParameter("@REMARKS", request.requestWarehouseMaster.REMARKS));
                        cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", request.requestWarehouseMaster.RECORDSTATUS));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestWarehouseMaster.USERCODE));

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

                            response.JS_Warehousedetails = ds.Tables[0];
                            response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestWarehouseMaster.WAREHOUSECODE.ToUpper() });
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

        public ResponseWarehouseMaster EditWarehouseMasterDAL(RequestWarehouseMaster request)
        {
            ResponseWarehouseMaster response = new ResponseWarehouseMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[WAREHOUSEMASTER_EDIT]", con);
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestWarehouseMaster.AUTOID.Trim().ToUpper()));
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {

                            response.JS_Warehousedetails = ds.Tables[0];

                            response.result = true;
                        }
                        scope.Complete();
                    }
                }


            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("FetchCustomerMasterDetailIdDAL: " + "Method Name FetchCustomerMasterDetailIdDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }



            return response;
        }

        public ResponseWarehouseMaster UpdateWarehouseMasterDAL(RequestWarehouseMaster request)
        {

            ResponseWarehouseMaster response = new ResponseWarehouseMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[WAREHOUSEMASTER_UPDATE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestWarehouseMaster.AUTOID.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@WHCODE", request.requestWarehouseMaster.WAREHOUSECODE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@WHNAME", request.requestWarehouseMaster.WAREHOUSENAME.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@PLANT", request.requestWarehouseMaster.PLANTCODE));
                        cmd.Parameters.Add(new SqlParameter("@SUPERVISOR", request.requestWarehouseMaster.SUPERVISOR));
                        cmd.Parameters.Add(new SqlParameter("@ADDRESS1", request.requestWarehouseMaster.ADDRESS1));
                        cmd.Parameters.Add(new SqlParameter("@ADDRESS2", request.requestWarehouseMaster.ADDRESS2));
                        cmd.Parameters.Add(new SqlParameter("@ADDRESS3", request.requestWarehouseMaster.ADDRESS3));
                        cmd.Parameters.Add(new SqlParameter("@CITY", request.requestWarehouseMaster.CITY));
                        cmd.Parameters.Add(new SqlParameter("@STATE", request.requestWarehouseMaster.STATE));
                        cmd.Parameters.Add(new SqlParameter("@PINCODE", request.requestWarehouseMaster.PINCODE));
                        cmd.Parameters.Add(new SqlParameter("@REMARKS", request.requestWarehouseMaster.REMARKS));
                        cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", request.requestWarehouseMaster.RECORDSTATUS));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestWarehouseMaster.USERCODE));

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

                            response.JS_Warehousedetails = ds.Tables[0];
                            response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = ds.Tables[0].Rows[0][2].ToString() });
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {

                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("UpdateCustomerMasterDAL: " + "Method Name UpdateCustomerMasterDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }
           return response;
        }
    }
}
