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
        public ResponseWarehouseLocationMaster WarehouseLocationMasterPageLoadDAL()
        {
            ResponseWarehouseLocationMaster response = new ResponseWarehouseLocationMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[WAREHOUSELOATIONMASTER_PAGELOAD]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                           response.JS_Plant = ds.Tables[0];
                            response.JS_Warehouse = ds.Tables[1];
                            response.JS_Locationdetails = ds.Tables[2];
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
        public ResponseWarehouseLocationMaster WarehouseLocationMasterPrintViewDAL()
        {
            ResponseWarehouseLocationMaster response = new ResponseWarehouseLocationMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[WAREHOUSELOATIONMASTER_PRINTVIEW]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_PrintView = ds.Tables[0];
                            response.result = true;
                        }
                        scope.Complete();
                    }
                }


            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("WarehouseLocationMasterPrintViewDAL: " + "Method Name WarehouseLocationMasterPrintViewDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }

            return response;
        }

        public ResponseWarehouseLocationMaster FetchWarehousepartcodeMasterDAL(RequestWarehouseLocationMaster request)
        {

            ResponseWarehouseLocationMaster response = new ResponseWarehouseLocationMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[WAREHOUSELOCATIONMASTER_FETCHBYWAREHOUSE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@PLANTCODE", request.requestWarehouseLocationMaster.PLANTCODE.Trim().ToUpper()));
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

        //public ResponseWarehouseLocationMaster FetchWarehousecodeMasterDAL(RequestWarehouseLocationMaster request)
        //{

        //    ResponseWarehouseLocationMaster response = new ResponseWarehouseLocationMaster();
        //    response.ErrorContainer = new List<ErrorItem>();
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope())
        //        {
        //            using (SqlConnection con = new SqlConnection(connectionstring))
        //            {
        //                SqlCommand cmd = new SqlCommand("[MASTERS].[WAREHOUSEMASTER_FETCHBYWAREHOUSECODE]", con);
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.Add(new SqlParameter("@WHCODE", request.requestWarehouseLocationMaster.WAREHOUSE.Trim().ToUpper()));
        //                con.Open();
        //                SqlDataAdapter oda = new SqlDataAdapter(cmd);
        //                DataSet ds = new DataSet();
        //                oda.Fill(ds);
        //                if (ds != null)
        //                {
        //                    response.JS_Plant = ds.Tables[0];
        //                    response.result = true;
        //                    response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestWarehouseLocationMaster.PLANTCODE.ToUpper() });
        //                }
                        
        //            }
        //            scope.Complete();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
        //        string responselog = createlog("FetchUserCreationbyUserCodeDAL: " + "Method Name FetchUserCreationbyUserCodeDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
        //        response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
        //        response.result = false;

        //    }



        //    return response;

        //}
        public ResponseWarehouseLocationMaster InsertWarehouseLocationMasterDAL(RequestWarehouseLocationMaster request)
        {

            ResponseWarehouseLocationMaster response = new ResponseWarehouseLocationMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[WAREHOUSELOCATIONMASTER_INSERT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@PLANT", request.requestWarehouseLocationMaster.PLANTCODE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@WAREHOUSE", request.requestWarehouseLocationMaster.WAREHOUSECODE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@LOCATIONCODE", request.requestWarehouseLocationMaster.LOCATIONCODE));
                        cmd.Parameters.Add(new SqlParameter("@LOCATIONNAME", request.requestWarehouseLocationMaster.LOCATION));
                        cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", request.requestWarehouseLocationMaster.RECORDSTATUS));
                        cmd.Parameters.Add(new SqlParameter("@REMARKS", request.requestWarehouseLocationMaster.REMARKS));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestWarehouseLocationMaster.USERCODE));

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

                            response.JS_Locationdetails = ds.Tables[0];
                            response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestWarehouseLocationMaster.LOCATIONCODE.ToUpper() });
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

        public ResponseWarehouseLocationMaster EditWarehouseLocationMasterDAL(RequestWarehouseLocationMaster request)
        {
            ResponseWarehouseLocationMaster response = new ResponseWarehouseLocationMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[WAREHOUSELOCTIOMMASTER_EDIT]", con);
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestWarehouseLocationMaster.AUTOID.Trim().ToUpper()));
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {

                            response.JS_Locationdetails = ds.Tables[0];

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

        public ResponseWarehouseLocationMaster UpdateWarehouseLocationMasterDAL(RequestWarehouseLocationMaster request)
        {

            ResponseWarehouseLocationMaster response = new ResponseWarehouseLocationMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[WAREHOUSELOCATIONMASTER_UPDATE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestWarehouseLocationMaster.AUTOID.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@PLANT", request.requestWarehouseLocationMaster.PLANTCODE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@WAREHOUSE", request.requestWarehouseLocationMaster.WAREHOUSECODE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@LOCATIONCODE", request.requestWarehouseLocationMaster.LOCATIONCODE));
                        cmd.Parameters.Add(new SqlParameter("@LOCATIONNAME", request.requestWarehouseLocationMaster.LOCATION));
                        cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", request.requestWarehouseLocationMaster.RECORDSTATUS));
                        cmd.Parameters.Add(new SqlParameter("@REMARKS", request.requestWarehouseLocationMaster.REMARKS));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestWarehouseLocationMaster.USERCODE));

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

                            response.JS_Locationdetails = ds.Tables[0];
                            response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestWarehouseLocationMaster.LOCATIONCODE.ToUpper() });
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
