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
        public ResponsePlantMaster PlantMasterPageLoadDAL()
        {
            ResponsePlantMaster response = new ResponsePlantMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[PLANTMASTER_PAGELOAD]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_RecordStatus = ds.Tables[0];
                            response.JS_PlantDetails = ds.Tables[1];

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

        public ResponsePlantMaster InsertPlantMasterDAL(RequestPlantMaster request)
        {

            ResponsePlantMaster response = new ResponsePlantMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[PLANTMASTER_INSERT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@PLANTCODE", request.requestPlantMaster.PLANTCODE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@PLANTNAME", request.requestPlantMaster.PLANTNAME.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", request.requestPlantMaster.RECORDSTATUS));
                        cmd.Parameters.Add(new SqlParameter("@REMARKS", request.requestPlantMaster.REMARKS));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestPlantMaster.USERCODE));

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

                            response.JS_PlantDetails = ds.Tables[0];
                            response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = ds.Tables[0].Rows[0][2].ToString() });
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

        public ResponsePlantMaster EditPlantMasterDetailIdDAL(RequestPlantMaster request)
        {
            ResponsePlantMaster response = new ResponsePlantMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[PLANTMASTER_EDIT]", con);
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestPlantMaster.AUTOID.Trim().ToUpper()));
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {

                            response.JS_PlantDetails = ds.Tables[0];

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

        public ResponsePlantMaster UpdatePlantMasterDAL(RequestPlantMaster request)
        {

            ResponsePlantMaster response = new ResponsePlantMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[PLANTMASTER_UPDATE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestPlantMaster.AUTOID.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@PLANTCODE", request.requestPlantMaster.PLANTCODE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@PLANTNAME", request.requestPlantMaster.PLANTNAME));
                        cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", request.requestPlantMaster.RECORDSTATUS));
                        cmd.Parameters.Add(new SqlParameter("@REMARKS", request.requestPlantMaster.REMARKS));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestPlantMaster.USERCODE));

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

                            response.JS_PlantDetails = ds.Tables[0];
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
