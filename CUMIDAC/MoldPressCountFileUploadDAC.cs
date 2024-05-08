using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CUMIDAC
{
    public partial class WMSDAL
    {
        public ResponseMoldPressCountFileUpload MoldPressCountFileUploadInsertDAL(RequestMoldPressCountFileUpload request)
        {
            ResponseMoldPressCountFileUpload response = new ResponseMoldPressCountFileUpload();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                DataSet ds = new DataSet();
                string erromessage = "";
                string ALREADYEXIST = "";
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 10, 0)))
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        con.Open();
                        string Invoicenumbers = "";
                        int rowcount = 0;

                        foreach (MoldPressCountFileUploadDetailsEntity det in request.requestmoldpresscountdts)
                        {
                            SqlCommand cmd1 = new SqlCommand("[TRANSACTIONS].[MOLDPRESSCOUNT_INSERT]", con);
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Parameters.Add(new SqlParameter("@MOLDITEMCODE", det.MOLDITEMCODE.ToUpper()));
                            cmd1.Parameters.Add(new SqlParameter("@MOLDITEMNAME", det.MOLDITEMNAME.ToUpper()));
                            cmd1.Parameters.Add(new SqlParameter("@FGITEMCODE", det.FGITEMCODE.ToUpper()));
                            cmd1.Parameters.Add(new SqlParameter("@SHOTCOUNT", det.SHOTCOUNT.ToUpper()));
                            //cmd1.Parameters.Add(new SqlParameter("@DATEOFSHOT", det.DATEOFSHOT));
                            cmd1.Parameters.Add(new SqlParameter("@DATEOFSHOT", det.DATEOFSHOT == "" ? null : Convert.ToDateTime(det.DATEOFSHOT).ToString("dd/MM/yyyy", new CultureInfo("en-US"))));
                            cmd1.Parameters.Add(new SqlParameter("@TOTALSHOTCOUNT", det.TOTALSHOTCOUNT));
                            cmd1.Parameters.Add(new SqlParameter("@RFIDNO", det.RFIDNO));
                            SqlDataAdapter oda1 = new SqlDataAdapter(cmd1);
                            oda1.SelectCommand.CommandTimeout = 300;
                            oda1.Fill(ds);
                            if (ds != null)
                            {
                                if (ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][0].ToString() == "SUCCESS")
                                {
                                    rowcount++;
                                }

                                else if (ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][0].ToString() == "FAILURE")
                                {
                                    ALREADYEXIST += ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][2].ToString() + ",";
                                }
                                else
                                {
                                    ALREADYEXIST += ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][2].ToString() + ",";
                                }
                            }
                        }

                        if (ALREADYEXIST.Length > 0)
                            ALREADYEXIST = ALREADYEXIST.Substring(0, ALREADYEXIST.Length - 1) + " Already Exist.";
                        if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS" && request.requestmoldpresscountdts.Count == rowcount)
                        {
                            scope.Complete();
                            response.result = true;
                            response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = ds.Tables[0].Rows[0][2].ToString() });
                        }
                        else
                        {
                            //erromessage = erromessage.Substring(0, erromessage.Length - 1);
                            response.message ="Mold Item Code"+"-"+ ALREADYEXIST;
                            response.result = false;
                            //response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = ds.Tables[0].Rows[0][2].ToString() });
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("MoldPressCountFileUploadInsertDAL: " + "Method Name MoldPressCountFileUploadInsertDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }

            return response;

        }
    }
}
