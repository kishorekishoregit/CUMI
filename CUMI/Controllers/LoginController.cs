using Newtonsoft.Json;
using CUMIENTITY;
using CUMI.Common;
using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using CUMIDAC;
using CUMIBC;
using System.Web.Helpers;
using DocumentFormat.OpenXml.Drawing.Charts;






namespace CUMI.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult Index()
        {
            try
            {
                base.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpCachePolicyBase varWK0 = base.Response.Cache;
                DateTime utcNow = DateTime.UtcNow;
                varWK0.SetExpires(utcNow.AddHours(-1.0));
                
                base.Response.Cache.SetNoStore();
                Session["Ucon"] = null;
                Session["LoginEmployeeCode"] = null;
           
            }
            catch (Exception ex)
            {}
            return View();
        }
        public ActionResult LoginCheck(string Username, string Upassword)
        {
            // Create request object
            RequestLoginDetails request = new RequestLoginDetails();
            request.requestLoginDetails = new LoginDetails();
            request.requestLoginDetails.UserName = Username;
            request.requestLoginDetails.Password = Upassword;

            // Create response object
            ResponseLoginDetails response = new ResponseLoginDetails();

            // Business logic for login
            LoginBC bc = new LoginBC();
            var data = "";
            try
            {
                // Call login business logic
                response = bc.LoginsBC(request);

                // Initialize data variable
                

                if (response.result)
                {
                    // If login successful

                    // Set session variable
                    Session["Ucon"] = response;

                    if (response.JS_LoginEmployeeCode != null && response.JS_LoginEmployeeCode.Rows.Count > 0)
                    {
                        // If employee code available, set session variable and prepare data
                        Session["LoginEmployeeCode"] = response.JS_LoginEmployeeCode.Rows[0]["EMPLOYEECODE"].ToString();
                        data = response.result + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_LoginDetails)
                            + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_ScreenDetails)
                            + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_LoginEmployeeCode);
                    }
                    else
                    {
                        // If employee code not available, set error message in data
                        data = false + "|" + "Employee code not found.";
                    }
                }
                else
                {
                    // If login unsuccessful, generate error message
                    ManageError Err = new ManageError();
                    string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
                    data = response.result + "|" + resultjson;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                // Log the exception for debugging or error tracking
                // Return appropriate error message to the user
                // For simplicity, you can leave this empty for now
            }

            // Return JSON result
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }


        public ActionResult GetSessionUserValue()
        {
            var data = "";
            if (Session["Ucon"] != null)
            {
                ResponseLoginDetails responseSession = new ResponseLoginDetails();
                responseSession = (ResponseLoginDetails)Session["Ucon"];
                data = Utility.DataTableToJSONWithJavaScriptSerializer(responseSession.JS_LoginDetails) 
                    + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(responseSession.JS_ScreenDetails)
                    + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(responseSession.JS_LoginEmployeeCode);
            }
            else
            {
                Session["Ucon"] = null;
                // return RedirectToAction("Index");
            }

            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }

        //[OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
        //[HttpGet]

        [HttpPost]
        public ActionResult logoutnew()
        {
            Session["Ucon"] = null;
            Session["LoginEmployeeCode"] = null;
            base.Session.Clear();
            base.Session.Abandon();
            base.Session.RemoveAll();
            FormsAuthentication.SignOut();
            return RedirectPermanent("~/");
          

        }

       
       
    }
}
