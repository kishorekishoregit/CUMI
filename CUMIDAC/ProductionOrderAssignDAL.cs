using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Transactions;

namespace CUMIDAC
{
    public partial class WMSDAL
    {
        public ResponseProductionOrderAssign PageloadProductionordernoDAL()
        {
            ResponseProductionOrderAssign response = new ResponseProductionOrderAssign();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[PRODUCTIONPICKLISTASSIGNMENT_PAGELOAD]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_warehousepicker = ds.Tables[0];
                            response.JS_location = ds.Tables[1];
                            response.JS_Productiondetails = ds.Tables[2];
                            response.JS_ProductionERPDts = ds.Tables[3];
                            response.JS_AssemblyID = ds.Tables[4];

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

        public ResponseProductionOrderAssign FetchproductionordernofetchdetailsDAL(RequestProductionOrderAssign request)
        {

            ResponseProductionOrderAssign response = new ResponseProductionOrderAssign();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[PRODUCTIONPICKLISTASSIGNMENT_DETAILSBYPRODUCTIONNO]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@PRODUCTIONORDERNO", request.requestproductionordernoheaderdetails.PRODUCTIONORDERNO.Trim().ToUpper()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        //if (ds != null)
                        //{
                        //    if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS" || ds.Tables[1].Rows[0][0].ToString() == "SUCCESS")
                        //        response.result = true;
                        //    else
                        //        response.result = false;
                        //    response.JS_Productiondetails = ds.Tables[0];// get the Shift Details
                        //}

                        if (ds != null)
                        {

                            if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                            {
                                response.JS_Productiondetails = ds.Tables[1];
                                response.result = true;
                            }


                            else
                            {

                                response.JS_Productiondetails = ds.Tables[0];
                                response.result = false;
                            }
                        }
                        response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestproductionordernoheaderdetails.PRODUCTIONORDERNO.ToUpper() });
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


        //public ResponseProductionOrderAssign InsertProductionorderDetailsDAL1(RequestProductionOrderAssign request)
        //{
        //    ResponseProductionOrderAssign response = new ResponseProductionOrderAssign();
        //    response.ErrorContainer = new List<ErrorItem>();
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope())
        //        {
        //            using (SqlConnection con = new SqlConnection(connectionstring))
        //            {
        //                int rowcount = 0;
        //                DataSet ds = new DataSet();
        //                foreach (ProductionOrderAssignDetailsEntity det in request.requestProductionOrderAssigndetails)
        //                {
        //                    SqlCommand cmd1 = new SqlCommand("[TRANSACTIONS].[PRODUCTIONORDERNO_INSERT]", con);
        //                    cmd1.CommandType = CommandType.StoredProcedure;
        //                    cmd1.Parameters.Add(new SqlParameter("@WAREHOUSEPICKER", request.requestproductionordernoheaderdetails.WAREHOUSEPICKER));
        //                    cmd1.Parameters.Add(new SqlParameter("@PRODUCTIONORDERNO", det.PRODUCTIONORDERNO));
        //                    cmd1.Parameters.Add(new SqlParameter("@DATE", det.PRODUCTIONORDERDATE));
        //                    cmd1.Parameters.Add(new SqlParameter("@FGITEMCODE", det.FGITEMCODE));
        //                    cmd1.Parameters.Add(new SqlParameter("@MOLDITEMCODE", det.MOLDITEMCODE));
        //                    cmd1.Parameters.Add(new SqlParameter("@MOLDITEMNAME", det.MOLDITEMNAME));
        //                    cmd1.Parameters.Add(new SqlParameter("@PRODUCTIONQTY", det.PRODUCTIONQTY));
        //                    cmd1.Parameters.Add(new SqlParameter("@USERCODE", request.requestproductionordernoheaderdetails.USERCODE.Trim()));
        //                    SqlDataAdapter oda1 = new SqlDataAdapter(cmd1);
        //                    oda1.Fill(ds);
        //                    if (ds != null)
        //                    {
        //                        if (ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][0].ToString() == "SUCCESS")
        //                        {
        //                            rowcount++;
        //                        }
        //                    }
        //                }


        //                if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS" && request.requestProductionOrderAssigndetails.Count == rowcount)
        //                {
        //                    scope.Complete();
        //                    response.result = true;
        //                    response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = ds.Tables[0].Rows[0][2].ToString() });
        //                }
        //                else
        //                {
        //                    response.result = false;
        //                    response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = ds.Tables[0].Rows[0][2].ToString() });
        //                }
        //            }
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
       
        public ResponseProductionOrderAssign InsertProductionorderDetailsDAL(RequestProductionOrderAssign request)
        {
            ResponseProductionOrderAssign response = new ResponseProductionOrderAssign();
            response.ErrorContainer = new List<ErrorItem>();

            try
            {
                DataSet ds = new DataSet();
                int rowcount = 0;
                int rowcount1 = 0;
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        con.Open(); 

                        SqlCommand cmdGetPicklistNo = new SqlCommand("[TRANSACTIONS].[PRODUCTIONORDERNO_GENERATEPICKLISTNO]", con);
                        cmdGetPicklistNo.CommandType = CommandType.StoredProcedure;
                        string picklistNo = cmdGetPicklistNo.ExecuteScalar()?.ToString();

                        if (string.IsNullOrEmpty(picklistNo))
                        {
                            response.result = false;
                            response.ErrorContainer.Add(new ErrorItem { ErrorNo = "Error", DataItem = "Failed to generate PICKLISTNO" });
                            return response;
                        }

                        foreach (ProductionOrderAssignDetailsEntity det in request.requestProductionOrderAssigndetails)
                        {
                            SqlCommand cmdInsertProductionOrder = new SqlCommand("[TRANSACTIONS].[PRODUCTIONORDERNO_INSERT]", con);
                            cmdInsertProductionOrder.CommandType = CommandType.StoredProcedure;
                            cmdInsertProductionOrder.Parameters.Add(new SqlParameter("@WAREHOUSEPICKER", request.requestproductionordernoheaderdetails.WAREHOUSEPICKER));
                            cmdInsertProductionOrder.Parameters.Add(new SqlParameter("@PRODUCTIONORDERNO", det.PRODUCTIONORDERNO));
                            cmdInsertProductionOrder.Parameters.Add(new SqlParameter("@DATE", det.PRODUCTIONORDERDATE));
                            cmdInsertProductionOrder.Parameters.Add(new SqlParameter("@FGITEMCODE", det.FGITEMCODE));
                            cmdInsertProductionOrder.Parameters.Add(new SqlParameter("@FGVARIANTCODE", det.FGVARIANT));
                            cmdInsertProductionOrder.Parameters.Add(new SqlParameter("@PRODUCTIONQTY", det.PRODUCTIONQTY));
                            cmdInsertProductionOrder.Parameters.Add(new SqlParameter("@ASSEMBLYID", det.ASSEMBLYID));

                            foreach (ProductionOrderAssignEntity det2 in request.requestProductionOrderAssigndetailsss)
                            {
                                if (det.FGITEMCODE == det2.FGITEMCODE) {
                                    SqlCommand cmdInsertProductionOrderCopy = cmdInsertProductionOrder.Clone();
                                    cmdInsertProductionOrderCopy.Parameters.Add(new SqlParameter("@MOLDITEMCODE", det2.MOLDITEMCODE));
                                    cmdInsertProductionOrderCopy.Parameters.Add(new SqlParameter("@MOLDITEMNAME", det2.MOLDITEMNAME));
                                    cmdInsertProductionOrderCopy.Parameters.Add(new SqlParameter("@MOLDVARIANT", det2.MOLDVARIANT));
                                    cmdInsertProductionOrderCopy.Parameters.Add(new SqlParameter("@ASSEMBLYQTY", det2.ASSEMBLYQTY));
                                    cmdInsertProductionOrderCopy.Parameters.Add(new SqlParameter("@USERCODE", request.requestproductionordernoheaderdetails.USERCODE.Trim()));
                                    cmdInsertProductionOrderCopy.Parameters.Add(new SqlParameter("@PICKLISTNO", picklistNo));

                                    SqlDataAdapter oda1 = new SqlDataAdapter(cmdInsertProductionOrderCopy);
                                    oda1.Fill(ds);
                                    if (ds != null)
                                    {
                                        if (ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][0].ToString() == "SUCCESS")
                                        {
                                            rowcount++;
                                        }
                                    }
                                }
                            }
                            //if (ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][0].ToString() == "SUCCESS")
                            //{
                            //    rowcount1++;
                            //}


                        }
                      
                        //if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS" && request.requestProductionOrderAssigndetails.Count == rowcount)
                        //{
                        //    scope.Complete();
                        //    response.result = true;
                        //    response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = ds.Tables[0].Rows[0][2].ToString() });
                        //}
                        //else
                        //{
                        //    response.result = false;
                        //    response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = ds.Tables[0].Rows[0][2].ToString() });
                        //}
                       // if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS" && request.requestProductionOrderAssigndetails.Count == rowcount)
                        if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
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
                string responselog = createlog("InsertProductionorderDetailsDAL: " + "Method Name InsertProductionorderDetailsDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }

            return response;
        }

        public ResponseProductionOrderAssign ProductionorderViewDtsDAL(RequestProductionOrderAssign request)
        {
            ResponseProductionOrderAssign response = new ResponseProductionOrderAssign();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[PRODUCTIONORDER_VIEW]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@PRODECTIONORDERNO", request.requestproductionordernoheaderdetails.PRODUCTIONORDERNO.Trim()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_Productiondetails = ds.Tables[0];
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
        public ResponseProductionOrderAssign ProductionOrderAssigndtsDAL(RequestProductionOrderAssign request)
        {
            ResponseProductionOrderAssign response = new ResponseProductionOrderAssign();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        con.Open();
                        foreach (ProductionOrderAssignEntity det in request.requestProductionOrderAssigndetailsss)
                        {
                            SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[PRODUCTIONPICKLISTASSIGNMENT_ASSIGN]", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@FGITEMCODE", det.FGITEMCODE));
                            cmd.Parameters.Add(new SqlParameter("@VARIANTCODE", det.VARIANT));
                            cmd.Parameters.Add(new SqlParameter("@ASSEMBLYID", det.ASSEMBLYID));
                            cmd.Parameters.Add(new SqlParameter("@PRODUCTIONQTY", det.PRODUCTIONQTY));

                            SqlDataAdapter oda = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            oda.Fill(ds);
                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                if (response.JS_ProductionDtsAssign == null)
                                    response.JS_ProductionDtsAssign = ds.Tables[0];
                                else
                                    response.JS_ProductionDtsAssign.Merge(ds.Tables[0]);
                            }
                        }
                        response.result = true; // Set the result to true once the loop completes.
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("ProductionOrderAssigndts: " + "Method Name ProductionOrderAssigndts" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }
            return response;
        }



    }
}
