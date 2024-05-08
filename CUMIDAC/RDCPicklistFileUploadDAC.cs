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
        public ResponseRDCPicklistFileupload RDCPicklistFileuploadInsertDAL(RequestRDCPicklistFileupload request)
        {
            ResponseRDCPicklistFileupload response = new ResponseRDCPicklistFileupload();
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

                        foreach (RDCPicklistFileuploadDetailsEntity det in request.requestRDCPicklistFileuploadDetails)
                        {
                            SqlCommand cmd1 = new SqlCommand("[TRANSACTIONS].[RDCPICKLISTFILEUPLOAD_INSERT]", con);
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Parameters.Add(new SqlParameter("@RDCNO", det.RDCNO.ToUpper()));
                            //cmd1.Parameters.Add(new SqlParameter("@ORDERDATE", det.ORDERDATE.ToUpper()));
                            cmd1.Parameters.Add(new SqlParameter("@DATE", det.DATE == "" ? null : Convert.ToDateTime(det.DATE).ToString("dd/MM/yyyy", new CultureInfo("en-US"))));
                            cmd1.Parameters.Add(new SqlParameter("@ITEMCODE", det.ITEMCODE.ToUpper()));
                            cmd1.Parameters.Add(new SqlParameter("@ITEMNAME", det.ITEMNAME.ToUpper()));
                            cmd1.Parameters.Add(new SqlParameter("@SUPPLIER", det.SUPPLIER));
                            cmd1.Parameters.Add(new SqlParameter("@QTY", det.QTY));
                     
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
                            ALREADYEXIST ="RDC No"+"-"+ ALREADYEXIST.Substring(0, ALREADYEXIST.Length - 1) + " Already Exist.";
                        if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS" && request.requestRDCPicklistFileuploadDetails.Count == rowcount)
                        {
                            scope.Complete();
                            response.result = true;
                            response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = ds.Tables[0].Rows[0][2].ToString() });
                        }
                        else
                        {
                            //erromessage = erromessage.Substring(0, erromessage.Length - 1);
                            response.message = ALREADYEXIST;
                            response.result = false;
                            //response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = ds.Tables[0].Rows[0][2].ToString() });
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("RDCPicklistFileuploadInsertDAL: " + "Method Name RDCPicklistFileuploadInsertDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }

            return response;

        }
    }
}
