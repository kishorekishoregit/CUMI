using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace CUMIDAC
{
    public partial class WMSDAL
    {
        public ResponseRDCPicklistAssign PageloadRDCPicklistAssignDAL()
        {
            ResponseRDCPicklistAssign response = new ResponseRDCPicklistAssign();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[RDCPICKLISTASSIGNMENT_PAGELOAD]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_warehousepicker = ds.Tables[0];
                            response.JS_location = ds.Tables[1];
                            response.JS_Supplier = ds.Tables[2];
                            response.JS_RDCdetails = ds.Tables[3];
                            response.JS_ERPdts = ds.Tables[4];

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

        public ResponseRDCPicklistAssign FetchRdcnofetchdetailsDAL(RequestRDCPicklistAssign request)
        {

            ResponseRDCPicklistAssign response = new ResponseRDCPicklistAssign();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[RDCNOPICKLISTASSIGNMENT_DETAILSBYRDCNO]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@RDCNO", request.requestrdcheaderdetails.RDCNO.Trim().ToUpper()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {

                            if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                            {
                                response.JS_RDCdetails = ds.Tables[1];
                                response.result = true;
                            }


                            else
                            {

                                response.JS_RDCdetails = ds.Tables[0];
                                response.result = false;
                            }
                            response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestrdcheaderdetails.RDCNO.ToUpper() });

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


        public ResponseRDCPicklistAssign InsertRDCPicklistAssignDetailsDAL(RequestRDCPicklistAssign request)
        {
            ResponseRDCPicklistAssign response = new ResponseRDCPicklistAssign();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        int rowcount = 0;
                        DataSet ds = new DataSet();

                        foreach (RDCPicklistAssignDetailsEntity det in request.requestrdcassigndetails)
                        {
                            SqlCommand cmd1 = new SqlCommand("[TRANSACTIONS].[RDCPICKLISTASSIGN_INSERT]", con);
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Parameters.Add(new SqlParameter("@WAREHOUSEPICKER", request.requestrdcheaderdetails.WAREHOUSEPICKER));
                            cmd1.Parameters.Add(new SqlParameter("@RDCNO", det.RDCNO));
                            cmd1.Parameters.Add(new SqlParameter("@DATE", det.DATE));
                            cmd1.Parameters.Add(new SqlParameter("@ITEMCODE", det.ITEMCODE));
                            cmd1.Parameters.Add(new SqlParameter("@ITEMNAME", det.ITEMNAME));
                            cmd1.Parameters.Add(new SqlParameter("@SUPPLIERCODE", det.SUPPLIER));
                            cmd1.Parameters.Add(new SqlParameter("@REMARKS", det.REMARKS));
                            cmd1.Parameters.Add(new SqlParameter("@QUANTITY", det.QUANTITY));
                            cmd1.Parameters.Add(new SqlParameter("@RFIDNO", det.RFIDNO));
                            cmd1.Parameters.Add(new SqlParameter("@USERCODE", request.requestrdcheaderdetails.USERCODE.Trim()));
                            SqlDataAdapter oda1 = new SqlDataAdapter(cmd1);
                            oda1.Fill(ds);
                            if (ds != null)
                            {
                                if (ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][0].ToString() == "SUCCESS")
                                {
                                    rowcount++;
                                }
                            }
                        }

                        if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS" && request.requestrdcassigndetails.Count == rowcount)
                        {
                            scope.Complete();
                            response.result = true;
                            response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = ds.Tables[0].Rows[0][2].ToString() });
                        }
                        else
                        {
                            response.result = false;
                            response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = ds.Tables[0].Rows[0][2].ToString() });
                        }
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

        public ResponseRDCPicklistAssign RDCPicklistAssignViewDtsDAL(RequestRDCPicklistAssign request)
        {
            ResponseRDCPicklistAssign response = new ResponseRDCPicklistAssign();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[RDCPICKLISTASSIGN_VIEW]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@RDCNO", request.requestrdcheaderdetails.RDCNO.Trim()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_RDCdetails = ds.Tables[0];
                            response.result = true;
                        }

                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {

                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("ProductionOrderViewDtsDAL: " + "Method Name ProductionOrderViewDtsDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }


            return response;

        }


    }
}
