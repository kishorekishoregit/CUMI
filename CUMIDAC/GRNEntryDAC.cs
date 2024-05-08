using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Globalization;

namespace CUMIDAC
{
    public partial class WMSDAL
    {
        public ResponseGRNEntry GRNEntryPageloadDAL()
        {
            ResponseGRNEntry response = new ResponseGRNEntry();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[GRNENTRY_PAGELOAD]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {

                            response.JS_RMItemcode = ds.Tables[0];
                            response.JS_GRNpageload = ds.Tables[1];
                            response.result = true;
                        }
                        scope.Complete();
                    }
                }


            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("GRNEntryPageloadDAL: " + "Method Name GRNEntryPageloadDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }



            return response;
        }
        public ResponseGRNEntry GRNEntryItemDetailsFetchDAL(RequestGRNEntry request)
        {
            ResponseGRNEntry response = new ResponseGRNEntry();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[GRNENTRY_FETCHBYITEMCODE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ITEMCODE", request.requestgrnentry.ITEMCODE.ToString()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {

                            response.JS_ItemDetails = ds.Tables[0];
                            response.result = true;
                        }
                        scope.Complete();
                    }
                }


            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("GRNEntryPageloadDAL: " + "Method Name GRNEntryPageloadDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }



            return response;
        }
        public ResponseGRNEntry GRNEntryInsertDAL(RequestGRNEntry request)
        {
            ResponseGRNEntry response = new ResponseGRNEntry();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                DataSet ds = new DataSet();

                int totalcount = 0;
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        con.Open();

                        int barcode = 0;
                        int rowcount = 0, rowcount1 = 0, totalqty = 0;
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[GRNENTRY_HEADERINSERT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@GRNNO", request.requestgrnentry.GRNNO.Trim().ToUpper()));
                        cmd.Parameters.Add(new SqlParameter("@GRNDATE", request.requestgrnentry.GRNDATE == "" ? null : Convert.ToDateTime(request.requestgrnentry.GRNDATE).ToString("dd/MM/yyyy", new CultureInfo("en-US"))));
                        cmd.Parameters.Add(new SqlParameter("@SUPPLIER", request.requestgrnentry.SUPPLIER.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@PONO", request.requestgrnentry.PONO.Trim().ToUpper()));
                        cmd.Parameters.Add(new SqlParameter("@INVOICENO", request.requestgrnentry.INVOICENO.Trim().ToUpper()));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestgrnentry.USERCODE.Trim().ToUpper()));

                        SqlDataAdapter oda = new SqlDataAdapter(cmd);

                        oda.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                            {
                                foreach (GRNEntryDetailsEntity det in request.requestgrnentrydetails)
                                {
                                    SqlCommand cmd1 = new SqlCommand("[TRANSACTIONS].[GRNENTRY_DETAILSINSERT]", con);
                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.Parameters.Add(new SqlParameter("@GRNNO", request.requestgrnentry.GRNNO.Trim().ToString()));
                                    cmd1.Parameters.Add(new SqlParameter("@RMITEMCODE", det.RMITEMCODE.Trim().ToUpper()));
                                    cmd1.Parameters.Add(new SqlParameter("@RMITEMNAME", det.RMITEMNAME));
                                    cmd1.Parameters.Add(new SqlParameter("@VARIANT", det.VARIANT));
                                    cmd1.Parameters.Add(new SqlParameter("@UOM", det.UOM.Trim().ToUpper()));
                                    cmd1.Parameters.Add(new SqlParameter("@QUANTITY", det.QUANTITY.Trim().ToUpper()));
                                    cmd1.Parameters.Add(new SqlParameter("@USERCODE", request.requestgrnentry.USERCODE.Trim().ToUpper()));

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

                            }
                        }
                        if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS" && request.requestgrnentrydetails.Count == rowcount)
                        {
                            totalcount++;
                        }
                        if (request.requestgrnentrydetails.Count == rowcount)
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
                string responselog = createlog("UploadInventoryDAL: " + "Method Name UploadInventoryDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }


            return response;

        }
        public ResponseGRNEntry GRNEntryEditDAL(RequestGRNEntry request)
        {
            ResponseGRNEntry response = new ResponseGRNEntry();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[GRNENTRY_EDIT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@GRNNO", request.requestgrnentry.GRNNO.ToString()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {

                            response.JS_GRNEnrtyDetails = ds.Tables[0];
                            response.JS_ItemDetails = ds.Tables[1];
                            response.result = true;
                        }
                        scope.Complete();
                    }
                }


            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("GRNEntryPageloadDAL: " + "Method Name GRNEntryPageloadDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }



            return response;
        }
        public ResponseGRNEntry GRNUpdateDAL(RequestGRNEntry request)
        {
            ResponseGRNEntry response = new ResponseGRNEntry();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                DataSet ds = new DataSet();

                int totalcount = 0;
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        con.Open();

                        int barcode = 0;
                        int rowcount = 0, rowcount1 = 0, totalqty = 0;
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[GRNENTRY_UPDATE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@GRNNO", request.requestgrnentry.GRNNO.Trim().ToUpper()));
                        cmd.Parameters.Add(new SqlParameter("@GRNDATE", request.requestgrnentry.GRNDATE == "" ? null : Convert.ToDateTime(request.requestgrnentry.GRNDATE).ToString("MM/dd/yyyy", new CultureInfo("en-US"))));
                        cmd.Parameters.Add(new SqlParameter("@SUPPLIER", request.requestgrnentry.SUPPLIER.Trim().ToUpper()));
                        cmd.Parameters.Add(new SqlParameter("@PONO", request.requestgrnentry.PONO.Trim().ToUpper()));
                        cmd.Parameters.Add(new SqlParameter("@INVOICENO", request.requestgrnentry.INVOICENO.Trim().ToUpper()));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestgrnentry.USERCODE.Trim().ToUpper()));
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);

                        oda.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                            {
                                foreach (GRNEntryDetailsEntity det in request.requestgrnentrydetails)
                                {
                                    SqlCommand cmd1 = new SqlCommand("[TRANSACTIONS].[GRNENTRY_DETAILSINSERT]", con);
                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.Parameters.Add(new SqlParameter("@GRNNO", ds.Tables[0].Rows[0][2].ToString().ToUpper()));
                                    cmd1.Parameters.Add(new SqlParameter("@RMITEMCODE", det.RMITEMCODE.Trim().ToUpper()));
                                    cmd1.Parameters.Add(new SqlParameter("@RMITEMNAME", det.RMITEMNAME));
                                    cmd1.Parameters.Add(new SqlParameter("@VARIANT", det.VARIANT.Trim().ToUpper()));
                                    cmd1.Parameters.Add(new SqlParameter("@UOM", det.UOM.Trim().ToUpper()));
                                    cmd1.Parameters.Add(new SqlParameter("@QUANTITY", det.QUANTITY.Trim().ToUpper()));
                                    cmd1.Parameters.Add(new SqlParameter("@USERCODE", request.requestgrnentry.USERCODE.Trim().ToUpper()));
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

                            }
                        }
                        if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS" && request.requestgrnentrydetails.Count == rowcount)
                        {
                            totalcount++;
                        }

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
                string responselog = createlog("InvoiceEntryUpdateDAL: " + "Method Name InvoiceEntryUpdateDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }


            return response;

        }
        public ResponseGRNEntry GRNEntryViewDAL(RequestGRNEntry request)
        {
            ResponseGRNEntry response = new ResponseGRNEntry();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[GRNENTRY_VIEW]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@GRNNO", request.requestgrnentry.GRNNO.ToString()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {

                            response.JS_GRNEnrtyDetails = ds.Tables[0];
                            response.result = true;
                        }
                        scope.Complete();
                    }
                }


            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("GRNEntryViewDAL: " + "Method Name GRNEntryViewDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }



            return response;
        }
    }
   
}
