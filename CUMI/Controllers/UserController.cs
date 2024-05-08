﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CUMIENTITY;
using CUMIBC;
using CUMI.Common;
using System.Web.Script.Serialization;

namespace CUMI.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        string UserCreationDALResult = string.Empty;
        public ActionResult Index()
        {
            return View();
        }

        #region User Creation......................................................................
        public ActionResult UserCreation()
        {
            Session["View"] = "UAC001";
            return View();
        }

        //Page Load
        public ActionResult GetUserCreationPageLoad()
        {


            UserCreationEntity usercreationentity = new UserCreationEntity();
            RequestUserCreation requestusercreation = new RequestUserCreation();
            ResponseUserCreation responseusercreation = new ResponseUserCreation();

            requestusercreation.requestUserCreation = usercreationentity;
            UserCreationBC bc = new UserCreationBC();
            responseusercreation = bc.FetchUserCreationPageLoadBC();



            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(responseusercreation.JS_RecordStatus)
                //+ "|" + Utility.DataTableToJSONWithJavaScriptSerializer(responseusercreation.JS_RecordStatus)
                + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(responseusercreation.JS_UserCreationDetails);

            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        //Fetch Grid
        public ActionResult GetUserCreationByEmployeename(string EmpCode)
        {


            RequestUserCreation request = new RequestUserCreation();
            ResponseUserCreation response = new ResponseUserCreation();
            request.requestUserCreation = new UserCreationEntity();
            request.requestUserCreation.EmployeeCode = EmpCode;
            UserCreationBC bc = new UserCreationBC();
            response = bc.FetchUserCreationbyEmpNameBC(request);

            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Employee);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //Insert and Update
        [HttpPost]
        public ActionResult InsertUserCreation(string actiontype, string Empcode, string EmpName, string UserName, string Password, string ConfirmPassword, string Status)
        {


            RequestUserCreation request = new RequestUserCreation();
            ResponseUserCreation response = new ResponseUserCreation();
            request.requestUserCreation = new UserCreationEntity();
            request.requestUserCreation.EmployeeCode = Empcode;
            request.requestUserCreation.EmployeeName = EmpName;
            request.requestUserCreation.RecordStatus = Status;
            request.requestUserCreation.UserName = UserName;
            request.requestUserCreation.UserPassword = Password;
            request.requestUserCreation.ConfirmPassword = ConfirmPassword;
            request.requestUserCreation.CreatedBy = Session["LoginEmployeeCode"].ToString();
            request.requestUserCreation.CreatedDate = DateTime.Now;
            request.requestUserCreation.ModifiedBy = Session["LoginEmployeeCode"].ToString();
            request.requestUserCreation.ModifiedDate = DateTime.Now;
            //   request.requestUserCreation.Recortimestamp = Request.Form["hrecordtimestamp"] == string.Empty ? 0 : Convert.ToInt64(Request.Form["hrecordtimestamp"]);

            UserCreationBC bc = new UserCreationBC();
            string value = "Update";
            if (value != actiontype)
                response = bc.InsertUserCreationBC(request);
            else
                response = bc.UpdateUserCreationBC(request);

            ManageError Err = new ManageError();

            string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
            resultjson = response.result + "|" + resultjson;

            return Json(resultjson);
        }


        //Fetch Grid
        public ActionResult GetUserCreationByID(string usercode)
        {

            RequestUserCreation request = new RequestUserCreation();
            ResponseUserCreation response = new ResponseUserCreation();
            request.requestUserCreation = new UserCreationEntity();
            request.requestUserCreation.EmployeeCode = usercode;
            UserCreationBC bc = new UserCreationBC();
            response = bc.FetchUserCreationbyUserDetailsBC(request);

            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_UserCreationDetails);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region User Authentication................................................................
        public ActionResult UserAuthentication()
        {
            Session["View"] = "UAC002";
            return View();
        }

        //Page Load
        public ActionResult GetUserAuthenticationPageLoad()
        {

            UserAuthenticationEntity userauthenticationentity = new UserAuthenticationEntity();
            RequestUserAuthentication requestuserauthentication = new RequestUserAuthentication();
            ResponseUserAuthentication responseuserauthentication = new ResponseUserAuthentication();
            requestuserauthentication.requestUserAuthentication = userauthenticationentity;
            UserAuthenticationBC bc = new UserAuthenticationBC();
            responseuserauthentication = bc.FetchUserAuthenticationPageLoadBC();



            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(responseuserauthentication.JS_UserName)
                + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(responseuserauthentication.JS_ScreenDetails);

            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);

        }


        //Fetch ScreenDetails by UserName
        public ActionResult FetchScreenDetailsByUserName(string username)
        {

            ResponseUserAuthentication responseuserauthentication = new ResponseUserAuthentication();
            RequestUserAuthentication requestuserauthentication = new RequestUserAuthentication();
            requestuserauthentication.requestUserAuthentication = new UserAuthenticationEntity();
            requestuserauthentication.requestUserAuthentication.UserName = username;
            UserAuthenticationBC bc = new UserAuthenticationBC();
            responseuserauthentication = bc.FetchScreenDetailsbyUserNameBC(requestuserauthentication);


            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(responseuserauthentication.JS_ScreenDetails);
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult InsertUserAuthenticationMaster(string actiontype)
        {

            String[] screenid = Request.Form["screenid"].Split('^');
            String[] view = Request.Form["view"].Split('^');
            String[] Cancel = Request.Form["Cancel"].Split('^');
              
            RequestUserAuthentication request = new RequestUserAuthentication();
            ResponseUserAuthentication responseuserauthentication = new ResponseUserAuthentication();
            request.requestUserAuthentication = new UserAuthenticationEntity();
            request.requestUserAuthentication.UserName = Request.Form["username"];
            request.requestUserRoleScreenDetails = new List<UserAuthenticationEntity>();
            for (int i = 0; i < screenid.Length; i++)
            {
                UserAuthenticationEntity ua = new UserAuthenticationEntity();
                ua.screenid = screenid[i];
                ua.UserName = Request.Form["username"];
                ua.view = view[i];
                ua.Cancel = Cancel[i];
                ua.HOD = Request.Form["HOD"];
                ua.CreatedBy = "ADMIN";
                ua.CreatedDate = DateTime.Now;
                ua.ModifiedBy = "ADMIN";
                ua.ModifiedDate = DateTime.Now;
                request.requestUserRoleScreenDetails.Add(ua);
            }

            UserAuthenticationBC bc = new UserAuthenticationBC();
            string value = "Update";

            if (value != Request.Form["actiontype"].ToString())
                responseuserauthentication = bc.InsertUserAuthenticationBC(request);



            ManageError Err = new ManageError();

            string resultjson = Err.GenerateErrorMessages(responseuserauthentication.ErrorContainer);
            resultjson = responseuserauthentication.result + "|" + resultjson;

            return Json(resultjson);
        }


        #endregion





    }
}
