using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CUMIENTITY;
using CUMIBC;
using CUMI.Common;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Data;
using System.Transactions;
using System.Data.SqlClient;

namespace CUMI.Controllers
{

    public class HomeController : Controller
    {
        string connectionstring = ConfigurationManager.ConnectionStrings["CUMI"].ConnectionString;
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }
        public ActionResult GetDashboardDetails()
        {
            DataTable red = new DataTable();
            DataTable alert = new DataTable();
            DataTable rdc = new DataTable();
            DataTable pd = new DataTable();
            DataTable redlist = new DataTable();
            DataTable alertlist = new DataTable();
            DataTable RDCpicklist = new DataTable();
            DataTable MoldPicklist = new DataTable();

            using (TransactionScope scope = new TransactionScope())
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[DASHBOARD_FETCH]", con);
                    //cmd.Parameters.AddWithValue("@USERCODE", Session["LoginEmployeeCode"].ToString());
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter oda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    oda.Fill(ds);

                    if (ds != null)
                    {
                        red = ds.Tables[0];
                        alert = ds.Tables[1];
                        rdc = ds.Tables[2];
                        pd = ds.Tables[3];
                        redlist = ds.Tables[4];
                        alertlist = ds.Tables[5];
                        RDCpicklist = ds.Tables[6];
                        MoldPicklist = ds.Tables[7];
                    }
                    scope.Complete();
                }
            }
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(red)
                               + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(alert)
                               + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(rdc)
                                + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(pd)
            + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(redlist)
            + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(alertlist)
            +"|"+Utility.DataTableToJSONWithJavaScriptSerializer(RDCpicklist)
            +"|"+Utility.DataTableToJSONWithJavaScriptSerializer(MoldPicklist);
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);

        }

    }
}

