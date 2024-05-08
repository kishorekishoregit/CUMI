using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CUMIDAC
{
    public partial class WMSDAL
    {
        public ResponseMoldPutawayReport MoldPutawayPageLoadDAL()
        {
            ResponseMoldPutawayReport response = new ResponseMoldPutawayReport();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[REPORTS].[MOLDPUTAWAYREPORT_PAGELOAD]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_MoldPutawayPageload = ds.Tables[0];
                            response.result = true;
                        }
                        scope.Complete();
                    }
                }

            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("MoldInwardOrInterlinkingPageLoadDAL: " + "Method Name MoldInwardOrInterlinkingPageLoadDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }

            return response;
        }
        public ResponseMoldPutawayReport MoldPutawayGenerateDAL(RequestMoldPutawayReport request)
        {
            ResponseMoldPutawayReport response = new ResponseMoldPutawayReport();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[REPORTS].[MOLDPUTAWAYREPORT_GENERATE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@FROMDATE", request.requestmoldputaway.FROMDATE));
                        cmd.Parameters.Add(new SqlParameter("@TODATE", request.requestmoldputaway.TODATE));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_MoldPutawayGenerate = ds.Tables[0];
                            response.result = true;
                        }
                        scope.Complete();
                    }
                }

            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("MoldInwardOrInterlinkingGenerateDAL: " + "Method Name MoldInwardOrInterlinkingGenerateDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }
            return response;
        }
    }
}
