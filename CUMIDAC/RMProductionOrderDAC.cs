using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CUMIDAC
{
    public partial class WMSDAL
    {
        public ResponseRMProductionOrder RMProductionOrderPageloadDAL()
        {
            ResponseRMProductionOrder response = new ResponseRMProductionOrder();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[RMPRODUCTIONORDER_PAGELOAD]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_ProductioNo = ds.Tables[0];
                            response.JS_RMProductionPageload = ds.Tables[1];

                            response.result = true;
                        }
                        scope.Complete();
                    }
                }


            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("RMProductionOrderPageloadDAL: " + "Method Name RMProductionOrderPageloadDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }



            return response;
        }
        public ResponseRMProductionOrder RMProductionOrderFetchDtsByProductionnoDAL(RequestRMProductionOrder request)
        {
            ResponseRMProductionOrder response = new ResponseRMProductionOrder();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[RMPRODUCTIONORDER_FETCHPRODUCTIONNODETAILS]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@PRODUCTIONORDERNO", request.requestrmproductionorder.PRODUCTIONORDERNO.Trim().ToString()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_ProductioNo = ds.Tables[0];
                            response.result = true;
                        }
                        scope.Complete();
                    }
                }


            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("RMProductionOrderFetchDtsByProductionnoDAL: " + "Method Name RMProductionOrderFetchDtsByProductionnoDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }



            return response;
        }
        public ResponseRMProductionOrder RMProductionOrderBViewDAL(RequestRMProductionOrder request)
        {
            ResponseRMProductionOrder response = new ResponseRMProductionOrder();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[RMPRODUCTIONORDER_VIEW]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@PRODUCTIONORDERNO", request.requestrmproductionorder.PRODUCTIONORDERNO.Trim().ToString()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_RMProductionOrderView = ds.Tables[0];
                            response.result = true;
                        }
                        scope.Complete();
                    }
                }


            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("RMProductionOrderBViewDAL: " + "Method Name RMProductionOrderBViewDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }



            return response;
        }
        public ResponseRMProductionOrder RMProductionOrderInsertDAL(RequestRMProductionOrder request)
        {
            ResponseRMProductionOrder response = new ResponseRMProductionOrder();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        int rowcount = 0;
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[RMPRODUCTIONORDER_HEADERINSERT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        // cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestproductionordernoheaderdetails.AUTOID.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@PRODUCTIONORDERNO", request.requestrmproductionorder.PRODUCTIONORDERNO));
                        cmd.Parameters.Add(new SqlParameter("@RECEIVEDDATE", request.requestrmproductionorder.RECEIVEDDATE));
                        cmd.Parameters.Add(new SqlParameter("@WAREHOUSEPICKER", request.requestrmproductionorder.WAREHOUSEPICKER));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestrmproductionorder.USERCODE));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                            {
                                foreach (RMProductionOrderDetailsEntity det in request.requestrmproductionorderdts)
                                {
                                    SqlCommand cmd1 = new SqlCommand("[TRANSACTIONS].[RMPRODUCTIONORDER_DETAILSINSERT]", con);
                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.Parameters.Add(new SqlParameter("@PRODUCTIONORDERNO", request.requestrmproductionorder.PRODUCTIONORDERNO));
                                    cmd1.Parameters.Add(new SqlParameter("@ITEMCODE", det.ITEMCODE));
                                    cmd1.Parameters.Add(new SqlParameter("@ITEMNAME", det.ITEMNAME));
                                    cmd1.Parameters.Add(new SqlParameter("@UOM", det.UOM));
                                    cmd1.Parameters.Add(new SqlParameter("@QUANTITY", det.QUANTITY));
                                    cmd1.Parameters.Add(new SqlParameter("@USERCODE", request.requestrmproductionorder.USERCODE.Trim()));
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
                        if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS" && request.requestrmproductionorderdts.Count == rowcount)
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
    }
}
