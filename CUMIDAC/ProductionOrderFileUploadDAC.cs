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
        public ResponseProductionOrderFileUpload ProductionOrderFileUploadInsertDAL(RequestProductionOrderFileUpload request)
        {
            ResponseProductionOrderFileUpload response = new ResponseProductionOrderFileUpload();
            response.ErrorConatiner = new List<ErrorItem>();
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

                        foreach (ProductionOrderFileUploadDetailsEntity det in request.requestproductionfileuploaddetails)
                        {
                            SqlCommand cmd1 = new SqlCommand("[TRANSACTIONS].[PRODUCTIONORDERFILEUPLOAD_INSERT]", con);
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Parameters.Add(new SqlParameter("@PRODUCTIONORDERNO", det.PRODUCTIONORDERNO.ToUpper()));
                            //cmd1.Parameters.Add(new SqlParameter("@ORDERDATE", det.ORDERDATE.ToUpper()));
                            cmd1.Parameters.Add(new SqlParameter("@ORDERDATE", det.ORDERDATE == "" ? null : Convert.ToDateTime(det.ORDERDATE).ToString("dd/MM/yyyy", new CultureInfo("en-US"))));
                            cmd1.Parameters.Add(new SqlParameter("@CUMIORDERREFNO", det.CUMIREFORDERNO.ToUpper()));
                            cmd1.Parameters.Add(new SqlParameter("@CUSTITEMCODE", det.CUSTITEMCODE.ToUpper()));
                            cmd1.Parameters.Add(new SqlParameter("@CUSTITEMNAME", det.CUSTITEMNAME));
                            cmd1.Parameters.Add(new SqlParameter("@UOM", det.UOM));
                            cmd1.Parameters.Add(new SqlParameter("@ORDERQTY", det.ORDERQTY));
                            cmd1.Parameters.Add(new SqlParameter("@VARIANT", det.VARIANT));
                            SqlDataAdapter oda1 = new SqlDataAdapter(cmd1);
                            oda1.SelectCommand.CommandTimeout = 300;
                            oda1.Fill(ds);
                            if (ds != null)
                            {
                                if (ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][0].ToString() == "SUCCESS")
                                {
                                    rowcount++;
                                }

                                //else if (ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][0].ToString() == "FAILURE")
                                //{
                                //    ALREADYEXIST += ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][2].ToString() + ",";
                                //}
                                //else
                                //{
                                //    ALREADYEXIST += ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][2].ToString() + ",";
                                //}
                            }
                        }

                        //if (ALREADYEXIST.Length > 0)
                        //    ALREADYEXIST = ALREADYEXIST.Substring(0, ALREADYEXIST.Length - 1) + " Already Exist.";
                        if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS" && request.requestproductionfileuploaddetails.Count == rowcount)
                        {
                            scope.Complete();
                            response.result = true;
                            response.ErrorConatiner.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = ds.Tables[0].Rows[0][2].ToString() });
                        }
                        else
                        {
                            //erromessage = erromessage.Substring(0, erromessage.Length - 1);
                            //response.message = ALREADYEXIST;
                            response.result = false;
                            //response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = ds.Tables[0].Rows[0][2].ToString() });
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("ProductionOrderFileUploadInsertDAL: " + "Method Name ProductionOrderFileUploadInsertDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorConatiner.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }

            return response;

        }
    }
}
