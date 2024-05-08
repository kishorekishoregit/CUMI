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
        public ResponseMoldRFIDAssignment PageloadMoldRFIDAssignmentDAL()
        {
            ResponseMoldRFIDAssignment response = new ResponseMoldRFIDAssignment();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[MOLDRFIDASSIGNMENT_PAGELOAD]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {

                            response.JS_Recordstatus = ds.Tables[0];
                            response.JS_UOM = ds.Tables[1];
                            response.JS_plantdetails = ds.Tables[2];
                            //response.JS_FGdetails = ds.Tables[3];
                            response.JS_Molddetails = ds.Tables[3];
                            //response.JS_Childpartdetails = ds.Tables[5];
                            response.JS_Moldrfidassignmentdetails = ds.Tables[4];
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

        public ResponseMoldRFIDAssignment FetchpartcodeMasterDAL(RequestMoldRFIDAssignment request)
        {

            ResponseMoldRFIDAssignment response = new ResponseMoldRFIDAssignment();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[FGLOCATIONMASTER_FETCHBYPARTCODE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@PLANTCODE", request.requestMoldRFIDAssignment.PLANTCODE.Trim().ToUpper()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_Plants = ds.Tables[0];
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

        public ResponseMoldRFIDAssignment FetchVarientCodeDAL(RequestMoldRFIDAssignment request)
        {

            ResponseMoldRFIDAssignment response = new ResponseMoldRFIDAssignment();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[MOLDRFID_VARIENTCODEFETCHDETAILS]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MOLDITEMCODE", request.requestMoldRFIDAssignment.MOLDITEMCODE.Trim().ToUpper()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_Plants = ds.Tables[0];
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

        public ResponseMoldRFIDAssignment InsertMoldRFIDAssignmentDAL(RequestMoldRFIDAssignment request)
        {         
            ResponseMoldRFIDAssignment response = new ResponseMoldRFIDAssignment();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[MOLDRFIDASSIGNMENT_INSERT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@PLANTCODE", request.requestMoldRFIDAssignment.PLANTCODE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@LOCATION", request.requestMoldRFIDAssignment.LOCATION.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@MOLDITEMCODE", request.requestMoldRFIDAssignment.MOLDITEMCODE));
                        cmd.Parameters.Add(new SqlParameter("@VARIENTCODE", request.requestMoldRFIDAssignment.VARIENTCODE));
                        cmd.Parameters.Add(new SqlParameter("@PONUMBER", request.requestMoldRFIDAssignment.PONUMBER));
                        cmd.Parameters.Add(new SqlParameter("@BATCHNUMBER", request.requestMoldRFIDAssignment.BATCHNUMBER));
                        cmd.Parameters.Add(new SqlParameter("@MOLDOPENCOUNT", request.requestMoldRFIDAssignment.MOLDOPENCOUNT));
                        cmd.Parameters.Add(new SqlParameter("@PREVIOUSMRCOUNT", request.requestMoldRFIDAssignment.PREVIOUSMRCOUNT));
                        cmd.Parameters.Add(new SqlParameter("@PREVIOUSMRDATE", request.requestMoldRFIDAssignment.PREVIUOSMRDATE));
                        cmd.Parameters.Add(new SqlParameter("@MRCOUNT", request.requestMoldRFIDAssignment.MRCOUNT));
                        cmd.Parameters.Add(new SqlParameter("@MRALERTCOUNT", request.requestMoldRFIDAssignment.MRALERTCOUNT));
                        cmd.Parameters.Add(new SqlParameter("@MOLDLIFECOUNT", request.requestMoldRFIDAssignment.MOLDLIFECOUNT));
                        cmd.Parameters.Add(new SqlParameter("@RFIDNUMBER", request.requestMoldRFIDAssignment.RFIDNUMBER));
                        cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", request.requestMoldRFIDAssignment.RECORDSTATUS));
                        cmd.Parameters.Add(new SqlParameter("@REMARKS", request.requestMoldRFIDAssignment.REMARKS));
                        cmd.Parameters.Add(new SqlParameter("@SUPPLIERID", request.requestMoldRFIDAssignment.SUPPLIERID));
                        cmd.Parameters.Add(new SqlParameter("@MOLDITEMNAME", request.requestMoldRFIDAssignment.MOLDITEMNAME));
                        cmd.Parameters.Add(new SqlParameter("@UOM", request.requestMoldRFIDAssignment.UOM));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestMoldRFIDAssignment.USERCODE));
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
                            response.JS_Moldrfidassignmentdetails = ds.Tables[0];// get the Shift Details
                        }
                        response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestMoldRFIDAssignment.MOLDITEMCODE.ToUpper() });

                        scope.Complete();
                    }
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
        public ResponseMoldRFIDAssignment EditMoldRFIDAssignmentDAL(RequestMoldRFIDAssignment request)
        {
            ResponseMoldRFIDAssignment response = new ResponseMoldRFIDAssignment();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[MOLDRFIDASSIGNMENT_EDIT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestMoldRFIDAssignment.AUTOID.Trim().ToUpper()));
                        //cmd.Parameters.Add(new SqlParameter("@MOLDITEMCODE", request.requestInterlinkingMaster.MOLDITEMCODE.Trim().ToUpper()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_Moldrfidassignmentdetails = ds.Tables[0];
                            //response.JS_Childpartdetails = ds.Tables[1];
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
        public ResponseMoldRFIDAssignment UpdateMoldRFIDAssignmentDAL(RequestMoldRFIDAssignment request)
        {     
            ResponseMoldRFIDAssignment response = new ResponseMoldRFIDAssignment();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[MOLDRFIDASSIGNMENT_UPDATE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestMoldRFIDAssignment.AUTOID.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@PLANTCODE", request.requestMoldRFIDAssignment.PLANTCODE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@LOCATION", request.requestMoldRFIDAssignment.LOCATION.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@MOLDITEMCODE", request.requestMoldRFIDAssignment.MOLDITEMCODE));
                        cmd.Parameters.Add(new SqlParameter("@VARIENTCODE", request.requestMoldRFIDAssignment.VARIENTCODE));
                        cmd.Parameters.Add(new SqlParameter("@PONUMBER", request.requestMoldRFIDAssignment.PONUMBER));
                        cmd.Parameters.Add(new SqlParameter("@BATCHNUMBER", request.requestMoldRFIDAssignment.BATCHNUMBER));
                        cmd.Parameters.Add(new SqlParameter("@MOLDOPENCOUNT", request.requestMoldRFIDAssignment.MOLDOPENCOUNT));
                        cmd.Parameters.Add(new SqlParameter("@PREVIOUSMRCOUNT", request.requestMoldRFIDAssignment.PREVIOUSMRCOUNT));
                        cmd.Parameters.Add(new SqlParameter("@PREVIOUSMRDATE", request.requestMoldRFIDAssignment.PREVIUOSMRDATE));
                        cmd.Parameters.Add(new SqlParameter("@MRCOUNT", request.requestMoldRFIDAssignment.MRCOUNT));
                        cmd.Parameters.Add(new SqlParameter("@MRALERTCOUNT", request.requestMoldRFIDAssignment.MRALERTCOUNT));
                        cmd.Parameters.Add(new SqlParameter("@MOLDLIFECOUNT", request.requestMoldRFIDAssignment.MOLDLIFECOUNT));
                        cmd.Parameters.Add(new SqlParameter("@RFIDNUMBER", request.requestMoldRFIDAssignment.RFIDNUMBER));
                        cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", request.requestMoldRFIDAssignment.RECORDSTATUS));
                        cmd.Parameters.Add(new SqlParameter("@SUPPLIERID", request.requestMoldRFIDAssignment.SUPPLIERID));
                        cmd.Parameters.Add(new SqlParameter("@MOLDITEMNAME", request.requestMoldRFIDAssignment.MOLDITEMNAME));
                        cmd.Parameters.Add(new SqlParameter("@UOM", request.requestMoldRFIDAssignment.UOM));
                        cmd.Parameters.Add(new SqlParameter("@REMARKS", request.requestMoldRFIDAssignment.REMARKS));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestMoldRFIDAssignment.USERCODE));
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
                            response.JS_Moldrfidassignmentdetails = ds.Tables[0];// get the Shift Details
                        }
                        response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestMoldRFIDAssignment.MOLDITEMCODE.ToUpper() });
                       
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

        //public ResponseInterlinkingMaster InterlinkingViewDtsDAL(RequestInterlinkingMaster request)
        //{
        //    ResponseInterlinkingMaster response = new ResponseInterlinkingMaster();
        //    response.ErrorContainer = new List<ErrorItem>();
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope())
        //        {
        //            using (SqlConnection con = new SqlConnection(connectionstring))
        //            {
        //                SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[MOLDRFIDASSIGNMENT_VIEW]", con);
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.Add(new SqlParameter("@MOLDITEMCODE", request.requestInterlinkingMaster.MOLDITEMCODE.Trim()));
        //                con.Open();
        //                SqlDataAdapter oda = new SqlDataAdapter(cmd);
        //                DataSet ds = new DataSet();
        //                oda.Fill(ds);
        //                if (ds != null)
        //                {
        //                    response.JS_Interlinkingdetails = ds.Tables[0];
        //                    response.result = true;
        //                }

        //            }
        //            scope.Complete();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
        //        string responselog = createlog("ProductionOrderViewDtsDAL: " + "Method Name ProductionOrderViewDtsDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
        //        response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
        //        response.result = false;

        //    }


        //    return response;

        //}
    }

}
