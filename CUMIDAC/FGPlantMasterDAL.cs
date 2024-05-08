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
        public ResponseFGPlantMaster PageloadFGPlantMasterDAL()
        {
            ResponseFGPlantMaster response = new ResponseFGPlantMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[FGPLANTMASTER_PAGELOAD]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {

                            response.JS_Recordstatus = ds.Tables[0];
                            response.JS_UOM = ds.Tables[1];
                            response.JS_Plant = ds.Tables[2];
                            response.JS_group = ds.Tables[3];
                            response.JS_Itemsubgroup = ds.Tables[4];
                            response.JS_Category = ds.Tables[5];
                            response.JS_FgplantmasterDetails = ds.Tables[6];
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

        public ResponseFGPlantMaster FetchpartcodeMasterDAL(RequestFGPlantMaster request)
        {

            ResponseFGPlantMaster response = new ResponseFGPlantMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[FGLOCATIONMASTER_FETCHBYPARTCODE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@PLANTCODE", request.requestfgplantmaster.PLANTCODE.Trim().ToUpper()));
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
        public ResponseFGPlantMaster InsertFGPlantMasterDAL(RequestFGPlantMaster request)
        {

            ResponseFGPlantMaster response = new ResponseFGPlantMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[FGPLANTMASTER_INSERT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@PLANTCODE", request.requestfgplantmaster.PLANTCODE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@LOCATION", request.requestfgplantmaster.LOCATION.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@FGITEMCODE", request.requestfgplantmaster.FGITEMCODE));
                        cmd.Parameters.Add(new SqlParameter("@VARIANTCODE", request.requestfgplantmaster.VARIANTCODE));
                        cmd.Parameters.Add(new SqlParameter("@DESCRIPTION", request.requestfgplantmaster.DESCRIPTION));
                        cmd.Parameters.Add(new SqlParameter("@UOM", request.requestfgplantmaster.UOM));
                        cmd.Parameters.Add(new SqlParameter("@WEIGHT", request.requestfgplantmaster.Weight));
                        cmd.Parameters.Add(new SqlParameter("@GROUP", request.requestfgplantmaster.GROUP));
                        cmd.Parameters.Add(new SqlParameter("@ITEMSUBGROUP", request.requestfgplantmaster.ITEMSUBGROUP));
                        cmd.Parameters.Add(new SqlParameter("@CATEGORY", request.requestfgplantmaster.CATEGORY));
                        cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", request.requestfgplantmaster.RECORDSTATUS));
                        cmd.Parameters.Add(new SqlParameter("@REMARKS", request.requestfgplantmaster.REMARKS));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestfgplantmaster.USERCODE));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        {
                            if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                                response.result = true;
                            else
                                response.result = false;
                            response.JS_FgplantmasterDetails = ds.Tables[0];// get the Shift Details
                        }
                        response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestfgplantmaster.FGITEMCODE.ToUpper() });
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
        public ResponseFGPlantMaster EditFGPlantMasterDAL(RequestFGPlantMaster request)
        {
            ResponseFGPlantMaster response = new ResponseFGPlantMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[FGPLANTMASTER_EDIT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestfgplantmaster.AUTOID.Trim().ToUpper()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_FgplantmasterDetails = ds.Tables[0];
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
        public ResponseFGPlantMaster UpdateFGPlantMasterDAL(RequestFGPlantMaster request)
        {
            ResponseFGPlantMaster response = new ResponseFGPlantMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[FGPLANTMASTER_UPDATE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestfgplantmaster.AUTOID.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@PLANTCODE", request.requestfgplantmaster.PLANTCODE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@LOCATION", request.requestfgplantmaster.LOCATION.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@FGITEMCODE", request.requestfgplantmaster.FGITEMCODE));
                        cmd.Parameters.Add(new SqlParameter("@VARIANTCODE", request.requestfgplantmaster.VARIANTCODE));
                        cmd.Parameters.Add(new SqlParameter("@DESCRIPTION", request.requestfgplantmaster.DESCRIPTION));
                        cmd.Parameters.Add(new SqlParameter("@UOM", request.requestfgplantmaster.UOM));
                        cmd.Parameters.Add(new SqlParameter("@WEIGHT", request.requestfgplantmaster.Weight));
                        cmd.Parameters.Add(new SqlParameter("@GROUP", request.requestfgplantmaster.GROUP));
                        cmd.Parameters.Add(new SqlParameter("@ITEMSUBGROUP", request.requestfgplantmaster.ITEMSUBGROUP));
                        cmd.Parameters.Add(new SqlParameter("@CATEGORY", request.requestfgplantmaster.CATEGORY));
                        cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", request.requestfgplantmaster.RECORDSTATUS));
                        cmd.Parameters.Add(new SqlParameter("@REMARKS", request.requestfgplantmaster.REMARKS));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestfgplantmaster.USERCODE));
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
                                response.JS_FgplantmasterDetails = ds.Tables[0];// get the Shift Details
                        }
                        response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestfgplantmaster.FGITEMCODE.ToUpper() });
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
