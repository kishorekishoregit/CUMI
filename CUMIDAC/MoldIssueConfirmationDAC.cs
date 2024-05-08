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
        public ResponseMoldIssueConfirmation MoldIssueConfirmationPageloadDAL()
        {
            ResponseMoldIssueConfirmation response = new ResponseMoldIssueConfirmation();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[MOLDISSUECONFIRMATION_PAGELOAD]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_Productionuser = ds.Tables[0];
                            response.JS_Moldissuedetails = ds.Tables[1];
                            response.result = true;
                        }
                        scope.Complete();

                    }
                }
            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("MoldIssueConfirmationPageloadDAL: " + "Method Name MoldIssueConfirmationPageloadDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }
            return response;
        }
        public ResponseMoldIssueConfirmation MoldIssueConfirmationAssignDAL(RequestMoldIssueConfirmation request)
        {
            ResponseMoldIssueConfirmation response = new ResponseMoldIssueConfirmation();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                DataSet ds = new DataSet();
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        con.Open();
                        foreach (MoldIssueConfirmationEntity det in request.requestmoldissueconfirmations)
                        {
                            SqlCommand cmd = new SqlCommand("[TRANSACTIONS].[MOLDISSUECONFIRMATION_PRODUCTIONUSERASSIGN]", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@PRODUCTIONUSER", request.requestmoldissueconfirmation.PRODUCTIONUSER.Trim().ToString()));
                            cmd.Parameters.Add(new SqlParameter("@MOLDITEMCODE", det.MOLDITEMCODE));
                            cmd.Parameters.Add(new SqlParameter("@PRODUCTIONORDERNO", det.PRODUCTIONORDERNO));
                           
                            SqlDataAdapter oda = new SqlDataAdapter(cmd);
                           
                            oda.Fill(ds);
                            
                        }
                        if (ds != null)
                        {
                            response.result = true;
                            response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = ds.Tables[0].Rows[0][2].ToString() });
                        }
                        else
                        {
                            response.result = false;
                            response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = ds.Tables[0].Rows[0][2].ToString() });
                        }
                        scope.Complete();

                    }
                }
            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("MoldIssueConfirmationAssignDAL: " + "Method Name MoldIssueConfirmationAssignDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }
            return response;
        }
    }
}
