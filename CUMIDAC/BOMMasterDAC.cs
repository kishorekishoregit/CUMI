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
        public ResponseBOMMaster PageloadBOMMasterDAL()
        {
            ResponseBOMMaster response = new ResponseBOMMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[BOMMASTER_PAGELOAD]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_VARIANT = ds.Tables[0];
                            response.JS_MOLDITEMCODE = ds.Tables[1];
                            //response.JS_MOLDITEMNAME = ds.Tables[3];
                            response.JS_BOMMASTERDETAILS = ds.Tables[2];
                            response.JS_DetailsVariant = ds.Tables[3];
                            response.result = true;
                        }
                        scope.Complete();
                    }
                }


            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("PageloadBOMMasterDAL: " + "Method Name PageloadBOMMasterDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }



            return response;
        }
        public ResponseBOMMaster FetchItemCodeBOMMasterDAL(RequestBOMMaster request)
        {
            ResponseBOMMaster response = new ResponseBOMMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[BOMMASTER_FETCHMOLDITEMCODE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MOLDITEMNAME", request.requestbommaster.MOLDITEMNAME.Trim().ToString()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {

                            response.JS_MOLDITEMNAME = ds.Tables[0];
                            response.result = true;
                        }
                        scope.Complete();
                    }
                }


            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("FetchItemCodeBOMMasterDAL: " + "Method Name FetchItemCodeBOMMasterDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }



            return response;
        }
        public ResponseBOMMaster FetchItemNameBOMMasterDAL(RequestBOMMaster request)
        {
            ResponseBOMMaster response = new ResponseBOMMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[BOMMASTER_FETCHMOLDITEMNAME]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MOLDITEMCODE", request.requestbommaster.MOLDITEMCODE.Trim().ToString()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {

                            response.JS_MOLDITEMCODE = ds.Tables[0];
                            response.result = true;
                        }
                        scope.Complete();
                    }
                }


            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("FetchItemNameBOMMasterDAL: " + "Method Name FetchItemNameBOMMasterDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }



            return response;
        }
        public ResponseBOMMaster FetchFGItemnameBOMMasterDAL(RequestBOMMaster request)
        {
            ResponseBOMMaster response = new ResponseBOMMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[BOMMASTER_FETCHFGITEMNAME]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@FGITEMCODE", request.requestbommaster.FGITEMCODE.Trim().ToString()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {

                            response.JS_FGITEMNAME = ds.Tables[0];
                            response.result = true;
                        }
                        scope.Complete();
                    }
                }


            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("FetchItemNameBOMMasterDAL: " + "Method Name FetchItemNameBOMMasterDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }



            return response;
        }
        public ResponseBOMMaster InsertBOMMMasterDAL(RequestBOMMaster request)
        {

            ResponseBOMMaster response = new ResponseBOMMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        int rowcount = 0;
                        SqlCommand cmd = new SqlCommand("[MASTERS].[BOMMASTER_HEADERINSERT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@FGITEMCODE", request.requestbommaster.FGITEMCODE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@FGITEMNAME", request.requestbommaster.FGITEMNAME.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@ASSEMBLYID", request.requestbommaster.ASSEMBLYID.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@VARIANT", request.requestbommaster.VARIANT));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestbommaster.USERCODE));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                            {
                                foreach (BOMMasterDetailsEntity det in request.requestbommasterdetails)
                                {
                                    SqlCommand cmd1 = new SqlCommand("[MASTERS].[BOMMASTER_DETAILSINSERT]", con);
                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.Parameters.Add(new SqlParameter("@FGITEMCODE", request.requestbommaster.FGITEMCODE));
                                    cmd1.Parameters.Add(new SqlParameter("@MOLDITEMCODE", det.MOLDITEMCODE));
                                    cmd1.Parameters.Add(new SqlParameter("@MOLDITEMNAME", det.MOLDITEMNAME));
                                    cmd1.Parameters.Add(new SqlParameter("@VARIANT", det.VARIANT));
                                    cmd1.Parameters.Add(new SqlParameter("@QUANTITY", det.QUANTITY));
                                    cmd1.Parameters.Add(new SqlParameter("@ASSEMBLYID", request.requestbommaster.ASSEMBLYID.Trim()));
                                    cmd1.Parameters.Add(new SqlParameter("@USERCODE", request.requestbommaster.USERCODE.Trim()));
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
                        if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS" && request.requestbommasterdetails.Count == rowcount)
                        {
                            scope.Complete();
                            response.result = true;
                            response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = "Assembly ID"+'-'+ request.requestbommaster.ASSEMBLYID.Trim().ToString() });
                        }
                        else
                        {
                            response.result = false;
                            response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = "Assembly ID" +'-'+ request.requestbommaster.ASSEMBLYID.Trim().ToString() });
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("InsertBOMMMasterDAL: " + "Method Name InsertBOMMMasterDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }

            return response;

        }
        public ResponseBOMMaster EditBOMMasterDAL(RequestBOMMaster request)
        {
            ResponseBOMMaster response = new ResponseBOMMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        con.Open();

                        // Fetch the updated data
                        SqlCommand retrieveCmd = new SqlCommand("[MASTERS].[BOMMASTER_EDIT]", con);
                        retrieveCmd.CommandType = CommandType.StoredProcedure;
                        retrieveCmd.Parameters.Add(new SqlParameter("@FGITEMCODE", request.requestbommaster.FGITEMCODE.Trim().ToString()));
                        string username = request.requestbommaster.USERCODE.Trim().ToString();
                        SqlDataAdapter oda = new SqlDataAdapter(retrieveCmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);

                        if (ds != null && ds.Tables.Count >= 2)
                        {

                            DateTime currentDate = DateTime.Now;

                            DataTable headerTable = ds.Tables[0];
                            headerTable.Columns.Add("CREATEDDATE", typeof(DateTime));
                            headerTable.Columns.Add("CREATEDBY");
                            headerTable.Columns.Add("MODIFIEDDATE", typeof(DateTime));
                            headerTable.Columns.Add("MODIFIEDBY");
                            foreach (DataRow row in headerTable.Rows)
                            {
                                row["CREATEDDATE"] = currentDate;
                                row["CREATEDBY"] = username;
                                row["MODIFIEDDATE"] = currentDate;
                                row["MODIFIEDBY"] = username;
                            }

                            DataTable detailsTable = ds.Tables[1];
                            detailsTable.Columns.Add("CREATEDDATE", typeof(DateTime));
                            detailsTable.Columns.Add("CREATEDBY");
                            detailsTable.Columns.Add("MODIFIEDDATE", typeof(DateTime));
                            detailsTable.Columns.Add("MODIFIEDBY");
                            foreach (DataRow row in detailsTable.Rows)
                            {
                                row["CREATEDDATE"] = currentDate;
                                row["CREATEDBY"] = username;
                                row["MODIFIEDDATE"] = currentDate;
                                row["MODIFIEDBY"] = username;
                            }

                            using (SqlBulkCopy bulkCopyHeader = new SqlBulkCopy(con))
                            {
                                bulkCopyHeader.DestinationTableName = "[MASTERS].[BOMMASTER_LOGHEADER]"; 
                                bulkCopyHeader.WriteToServer(ds.Tables[0]);
                            }

                            using (SqlBulkCopy bulkCopyDetails = new SqlBulkCopy(con))
                            {
                                bulkCopyDetails.DestinationTableName = "[MASTERS].[BOMMASTER_LOGDETAILS]";
                                bulkCopyDetails.WriteToServer(ds.Tables[1]);
                            }

                            response.JS_BOMMASTERHEADER = ds.Tables[0];

                            DataTable selectedTable = new DataTable();
                            selectedTable = ds.Tables[1].DefaultView.ToTable(false, "Mold_Item_Code", "Mold_Item_Name", "Variant", "Quantity", "Remove");

                            // Assigning the selected table to response.JS_BOMMASTERDETAILS
                            response.JS_BOMMASTERDETAILS = selectedTable;
                            //response.JS_BOMMASTERDETAILS = ds.Tables[1];
                            response.result = true;
                        }

                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("EditBOMMasterDAL: " + "Method Name EditBOMMasterDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }
            return response;
        }
        public ResponseBOMMaster ViewBOMMasterDAL(RequestBOMMaster request)
        {
            ResponseBOMMaster response = new ResponseBOMMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[BOMMASTER_VIEW]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@FGITEMCODE", request.requestbommaster.FGITEMCODE.Trim().ToString()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {

                            response.JS_BOMMASTERVIEW = ds.Tables[0];
                            response.result = true;
                        }
                        scope.Complete();
                    }
                }


            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("ViewBOMMasterDAL: " + "Method Name ViewBOMMasterDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }



            return response;
        }
        public ResponseBOMMaster UpdateBOMMasterDAL(RequestBOMMaster request)
        {
            ResponseBOMMaster response = new ResponseBOMMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        int rowcount = 0;
                        SqlCommand cmd = new SqlCommand("[MASTERS].[BOMMASTER_UPDATE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestbommaster.AUTOID.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@ASSEMBLYID", request.requestbommaster.ASSEMBLYID.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@FGITEMCODE", request.requestbommaster.FGITEMCODE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@FGITEMNAME", request.requestbommaster.FGITEMNAME.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@VARIANT", request.requestbommaster.VARIANT));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestbommaster.USERCODE));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                            {
                                foreach (BOMMasterDetailsEntity det in request.requestbommasterdetails)
                                {
                                    SqlCommand cmd1 = new SqlCommand("[MASTERS].[BOMMASTER_DETAILSINSERT]", con);
                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.Parameters.Add(new SqlParameter("@FGITEMCODE", request.requestbommaster.FGITEMCODE));
                                    cmd1.Parameters.Add(new SqlParameter("@MOLDITEMCODE", det.MOLDITEMCODE));
                                    cmd1.Parameters.Add(new SqlParameter("@MOLDITEMNAME", det.MOLDITEMNAME));
                                    cmd1.Parameters.Add(new SqlParameter("@VARIANT", det.VARIANT));
                                    cmd1.Parameters.Add(new SqlParameter("@QUANTITY", det.QUANTITY));
                                    cmd1.Parameters.Add(new SqlParameter("@ASSEMBLYID", request.requestbommaster.ASSEMBLYID.Trim()));
                                    cmd1.Parameters.Add(new SqlParameter("@USERCODE", request.requestbommaster.USERCODE.Trim()));
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
                        if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS" && request.requestbommasterdetails.Count == rowcount)
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
                string responselog = createlog("UpdateBOMMasterDAL: " + "Method Name UpdateBOMMasterDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }

            return response;

        }

    }

}
