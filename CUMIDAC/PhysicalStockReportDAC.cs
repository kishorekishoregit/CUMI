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
        public ResponsePhysicalStockReport PhysicalStockReportPageLoadDAL()
        {
            ResponsePhysicalStockReport response = new ResponsePhysicalStockReport();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[REPORTS].[PHYSICALSTOCKREPORT_PAGELOAD]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_PhysicalStockPageload = ds.Tables[0];
                            response.result = true;
                        }
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("PhysicalStockReportPageLoadDAL: " + "Method Name PhysicalStockReportPageLoadDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }

            return response;
        }
       
        public ResponsePhysicalStockReport PhysicalStockReportViewDAL(RequestPhysicalStockReport request)
        {
            ResponsePhysicalStockReport response = new ResponsePhysicalStockReport();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[REPORTS].[PHYSICALSTOCKREPORT_VIEW]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MOLDITEMCODE", request.requestphysicalstockreport.MOLDITEMCODE.Trim().ToString()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_PhysicalStockView = ds.Tables[0];
                            response.result = true;
                        }
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("PhysicalStockReportViewDAL: " + "Method Name PhysicalStockReportViewDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }

            return response;
        }
    }
}
