using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using CUMI.ErrorManager;
using CUMIENTITY;
using CUMIBC;
using CUMI.Common;
using System.Text;
using System.Configuration;
using System.Transactions;
using ExcelDataReader;
using System.Data.SqlClient;
using static CUMI.Controllers.TransactionController;
using DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;


namespace CUMI.Controllers
{

    public class MastersController : Controller
    {
        private string LOCATIONLABEL = ConfigurationManager.AppSettings["LOCATIONLABEL"].ToString();

        //#region Customer Master ............................................................................
        //public ActionResult CustomerMaster()
        //{
        //    return View();
        //}
        //public ActionResult GetCustomerMasterPageLoad()
        //{
        //    RequestCustomerMaster request = new RequestCustomerMaster();
        //    ResponseCustomerMaster response = new ResponseCustomerMaster();
        //    CustomerMasterBC bc = new CustomerMasterBC();
        //    response = bc.FetchCustomerMasterPageLoadBC();
        //    string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_RecordStatus) + "|" +
        //        Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_CustomerMasterDetails);

        //    var data = resulttable;
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    return Json(data, JsonRequestBehavior.AllowGet);

        //}

        //public ActionResult InsertCustomerMaster(string autoId, string CustCode, string CustName, string EmailID, string MobileNo, string ShipToAddress, string Status, string actiontype)
        //{
        //    RequestCustomerMaster request = new RequestCustomerMaster();
        //    ResponseCustomerMaster response = new ResponseCustomerMaster();
        //    request.requestCustomerMaster = new CUMIENTITY.CustomerMasterEntity();

        //    request.requestCustomerMaster.CustomerId = CustCode;
        //    request.requestCustomerMaster.CustomerName = CustName;
        //    request.requestCustomerMaster.EmailID = EmailID;
        //    request.requestCustomerMaster.MobileNo = MobileNo;
        //    request.requestCustomerMaster.ShipToAddress = ShipToAddress;
        //    request.requestCustomerMaster.RecordStatus = Status;
        //    request.requestCustomerMaster.CreatedBy = Session["LoginEmployeeCode"].ToString();


        //    CustomerMasterBC bc = new CustomerMasterBC();
        //    string value = "Update";
        //    if (value != actiontype)
        //    {
        //        response = bc.InsertCustomerMasterBC(request);
        //    }
        //    else
        //    {
        //        request.requestCustomerMaster.AutoID = autoId;
        //        response = bc.UpdateCustomerMasterBC(request);
        //    }
        //    ManageError Err = new ManageError();

        //    string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
        //    resultjson = response.result + "|" + resultjson;

        //    return Json(resultjson);
        //}

        //public ActionResult GetCustomerMasterByID(string usercode)
        //{

        //    RequestCustomerMaster request = new RequestCustomerMaster();
        //    ResponseCustomerMaster response = new ResponseCustomerMaster();
        //    request.requestCustomerMaster = new CUMIENTITY.CustomerMasterEntity();
        //    request.requestCustomerMaster.AutoID = usercode;
        //    CustomerMasterBC bc = new CustomerMasterBC();
        //    response = bc.FetchCustomerMasterByIDBC(request);

        //    var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_CustomerMasterDetails);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}



        //#endregion
        //#region Employee Master .............................................................................
        //public ActionResult EmployeeMaster()
        //{
        //    return View();
        //}
        //public ActionResult GetEmployeeMasterPageLoad()
        //{
        //    RequestEmployeeMaster request = new RequestEmployeeMaster();
        //    ResponseEmployeeMaster response = new ResponseEmployeeMaster();
        //    EmployeeMasterBC bc = new EmployeeMasterBC();
        //    response = bc.EmployeeMasterPageLoadBC();
        //    string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_RecordStatus)
        //                 + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Department)
        //                 + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Plant)
        //                 + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Employeedetails);
        //    var data = resulttable;
        //    JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
        //    json.MaxJsonLength = int.MaxValue;
        //    return json;
        //}

        //public ActionResult InsertEmployeeMaster(string actiontype, string AUTOID, string EMPLOYEECODE, string EMPLOYEENAME, string DEPARTMENT, string EMAILID, string CONTACTNO, string PLANT, string EMPLOYEESTATUS)
        //{
        //    RequestEmployeeMaster request = new RequestEmployeeMaster();
        //    ResponseEmployeeMaster response = new ResponseEmployeeMaster();
        //    request.requestemployeemaster = new EmployeeMasterEntity();
        //    request.requestemployeemaster.EMPLOYEECODE = EMPLOYEECODE;
        //    request.requestemployeemaster.EMPLOYEENAME = EMPLOYEENAME;
        //    request.requestemployeemaster.DEPARTMENT = DEPARTMENT;
        //    request.requestemployeemaster.EMAILID = EMAILID;
        //    request.requestemployeemaster.CONTACTNO = CONTACTNO;
        //    request.requestemployeemaster.PLANT = PLANT;
        //    request.requestemployeemaster.EMPLOYEESTATUS = EMPLOYEESTATUS;
        //    request.requestemployeemaster.USERCODE = Session["LoginEmployeeCode"].ToString();
        //    EmployeeMasterBC bc = new EmployeeMasterBC();
        //    string value = "Update";
        //    if (value != actiontype)
        //    {
        //        response = bc.InsertEmployeeMasteMasterBC(request);
        //    }
        //    else
        //    {
        //        request.requestemployeemaster.AUTOID= AUTOID;
        //        response = bc.UpdateEmployeeMasterBC(request);
        //    }

        //    ManageError Err = new ManageError();

        //    string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
        //    resultjson = response.result + "|" + resultjson;

        //    JsonResult json = Json(resultjson, JsonRequestBehavior.AllowGet);
        //    json.MaxJsonLength = int.MaxValue;
        //    return json;

        //}
        //public ActionResult GeteMPLOYEEMasterID(string ID)
        //{

        //    RequestEmployeeMaster request = new RequestEmployeeMaster();
        //    ResponseEmployeeMaster response = new ResponseEmployeeMaster();
        //    request.requestemployeemaster = new EmployeeMasterEntity();
        //    request.requestemployeemaster.AUTOID = ID;
        //    EmployeeMasterBC bc = new EmployeeMasterBC();
        //    response = bc.EditEmployeeMasterBC(request);

        //    var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Employeedetails);
        //    ;
        //    JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
        //    json.MaxJsonLength = int.MaxValue;
        //    return json;
        //}
        //#endregion

        #region Plant Master.........................................................................

        public ActionResult PlantMaster()
        {
            return View();
        }

        public ActionResult PlantMasterPageLoad()
        {
            RequestPlantMaster request = new RequestPlantMaster();
            ResponsePlantMaster response = new ResponsePlantMaster();
            PlantMastersBC bc = new PlantMastersBC();
            response = bc.PlantMasterPageLoadBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_RecordStatus) + "|" +
                Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_PlantDetails);

            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsertPlantMaster(string autoId, string PlantCode, string PlantName, string Status,string Remarks, string actiontype)
        {
            RequestPlantMaster request = new RequestPlantMaster();
            ResponsePlantMaster response = new ResponsePlantMaster();
            request.requestPlantMaster = new CUMIENTITY.PlantMasterEntity();
            request.requestPlantMaster.PLANTCODE = PlantCode;
            request.requestPlantMaster.PLANTNAME = PlantName;
            request.requestPlantMaster.RECORDSTATUS = Status;
            request.requestPlantMaster.REMARKS = Remarks;
            request.requestPlantMaster.USERCODE = Session["LoginEmployeeCode"].ToString();

            PlantMastersBC bc = new PlantMastersBC();
            string value = "Update";
            if (value != actiontype)
            {
                response = bc.InsertPlantMasterBC(request);
            }
            else
            {
                request.requestPlantMaster.AUTOID = autoId;
                response = bc.UpdateCustomerMasterBC(request);
            }
            ManageError Err = new ManageError();

            string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
            string result = resultjson.Remove(0, 2);
            resultjson = result;
            resultjson = response.result + "|" + resultjson;

            return Json(resultjson);
        }

        public ActionResult GetPlantMasterByID(string usercode)
        {

            RequestPlantMaster request = new RequestPlantMaster();
            ResponsePlantMaster response = new ResponsePlantMaster();
            request.requestPlantMaster = new CUMIENTITY.PlantMasterEntity();
            request.requestPlantMaster.AUTOID = usercode;
            PlantMastersBC bc = new PlantMastersBC();
            response = bc.EditPlantMasterByIDBC(request);

            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_PlantDetails);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Downloads(string file)
        {
            string filename = Path.GetFileName(file);
            string fullPath = Path.Combine(Server.MapPath("~/Downloads"), filename);
            return File(fullPath, "application/octet-stream", filename);
        }
        public ActionResult PlantMastersUploads()
        {
            string results = "";

            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    
                    var postedFile = System.Web.HttpContext.Current.Request.Files["CsvDoc"];

                    string path = Path.GetFileName(postedFile.FileName);

                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        if (file.FileName == postedFile.FileName)
                        {
                            if (file != null && file.ContentLength > 0)
                            {
                                Stream stream = file.InputStream;
                                IExcelDataReader reader = null;
                                if (file.FileName.EndsWith(".xls"))
                                {
                                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                                }
                                else if (file.FileName.EndsWith(".xlsx"))
                                {
                                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                                }
                                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                    {
                                        UseHeaderRow = true
                                    }
                                });
                                DataTable dataTable = result.Tables[0];

                                reader.Close();

                                results = Utility.DataTableToJSONWithJavaScriptSerializer(dataTable);

                                if (dataTable.Rows.Count > 0)
                                {
                                    DataTable dt = new DataTable();
                                    string check = string.Empty;

                                    int rowcount = 0;
                                    int rowcount1 = 0;
                                   
                                    string employeecodes = "";
                                    try
                                    {
                                        using (TransactionScope scope = new TransactionScope())
                                        {
                                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CUMI"].ConnectionString);
                                            {
                                                using (DataSet ds = new DataSet())
                                                {
                                                    foreach (DataRow req in dataTable.Rows)
                                                    {
                                                        
                                                            SqlCommand cmd = new SqlCommand("[MASTERS].[PLANTMASTER_INSERT]", con);
                                                            cmd.CommandType = CommandType.StoredProcedure;
                                                            cmd.Parameters.AddWithValue("@PLANTCODE", string.IsNullOrEmpty(req["PLANTCODE"].ToString()) ? string.Empty : req["PLANTCODE"].ToString());
                                                            cmd.Parameters.AddWithValue("@PLANTNAME", string.IsNullOrEmpty(req["PLANTNAME"].ToString()) ? string.Empty : req["PLANTNAME"].ToString());
                                                            cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", string.IsNullOrEmpty(req["RECORDSTATUS"].ToString()) ? string.Empty : req["RECORDSTATUS"].ToString()));
                                                            cmd.Parameters.AddWithValue("@REMARKS", string.IsNullOrEmpty(req["REMARKS"].ToString()) ? string.Empty : req["REMARKS"].ToString());
                                                            cmd.Parameters.Add(new SqlParameter("@USERCODE", Session["LoginEmployeeCode"].ToString()));
                                                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                                                            //DataSet ds2 = new DataSet();
                                                            da.Fill(ds);
                                                            rowcount++;
                                                       
                                                    }

                                                    if (dataTable.Rows.Count == rowcount && ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                                                    {
                                                        results = "Successfully uploaded!!!";
                                                        scope.Complete();
                                                        results = "true";

                                                    }
                                                    else
                                                    {
                                                        results = "Already uploaded";
                                                    }
                                                }

                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                                        string responselog = "";
                                        results = "false";

                                    }


                                }
                                else
                                {
                                    results = "false" + "|" + "Uploaded Excel File is Empty. ";
                                }

                            }
                        }
                        else
                        {
                            results = "False" + "|" + "Uploaded InValid Excel File. ";
                        }

                    }


                }

                catch (Exception ex)
                {

                    results = "False" + "|" + "Error occurred. Error details: ";

                }
            }

            JsonResult json = Json(results, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        #endregion

        #region Warehouse Master.........................................................................

        public ActionResult WarehouseMaster()
        {
            return View();
        }

        public ActionResult WarehouseMasterPageLoad()
        {
            RequestWarehouseMaster request = new RequestWarehouseMaster();
            ResponseWarehouseMaster response = new ResponseWarehouseMaster();
            WarehouseMasterBC bc = new WarehouseMasterBC();
            response = bc.WarehouseMasterPageLoadBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Plant) + "|" +
                Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_STATUS) + "|" +
                Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Warehousedetails);

            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetWarehouseMasterpartcodebyID(string partcode)
        {

            RequestWarehouseMaster request = new RequestWarehouseMaster();
            ResponseWarehouseMaster response = new ResponseWarehouseMaster();
            request.requestWarehouseMaster = new WarehouseMasterEntity();
            request.requestWarehouseMaster.PLANTCODE = partcode;
            WarehouseMasterBC bc = new WarehouseMasterBC();
            response = bc.FetchWarehouseMasterPartBC(request);
            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Plant);
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }

        public ActionResult InsertWarehouseMaster(string AUTOID, string WAREHOUSECODE, string WAREHOUSENAME, string PLANTCODE,string RECORDSTATUS, string actiontype,string SUPERVISOR,string ADDRESS1,string ADDRESS2, string ADDRESS3, string CITY,string STATE,string PINCODE,string REMARKS)
        {
            RequestWarehouseMaster request = new RequestWarehouseMaster();
            ResponseWarehouseMaster response = new ResponseWarehouseMaster();
            request.requestWarehouseMaster = new CUMIENTITY.WarehouseMasterEntity();
            request.requestWarehouseMaster.WAREHOUSECODE = WAREHOUSECODE;
            request.requestWarehouseMaster.WAREHOUSENAME = WAREHOUSENAME;
            request.requestWarehouseMaster.PLANTCODE = PLANTCODE;
            request.requestWarehouseMaster.SUPERVISOR = SUPERVISOR;
            request.requestWarehouseMaster.ADDRESS1 = ADDRESS1;
            request.requestWarehouseMaster.ADDRESS2 = ADDRESS2;
            request.requestWarehouseMaster.ADDRESS3 = ADDRESS3;
            request.requestWarehouseMaster.CITY = CITY;
            request.requestWarehouseMaster.STATE = STATE;
            request.requestWarehouseMaster.PINCODE = PINCODE;
            request.requestWarehouseMaster.REMARKS = REMARKS;
            request.requestWarehouseMaster.RECORDSTATUS = RECORDSTATUS;
            request.requestWarehouseMaster.USERCODE = Session["LoginEmployeeCode"].ToString();
            WarehouseMasterBC bc = new WarehouseMasterBC();
            string value = "Update";
            if (value != actiontype)
            {
                response = bc.InsertWarehouseMasterBC(request);
            }
            else
            {
                request.requestWarehouseMaster.AUTOID = AUTOID;
                response = bc.UpdateWarehouseMasterBC(request);
            }
            ManageError Err = new ManageError();

            string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
            string result = resultjson.Remove(0, 2);
            resultjson = result;
            resultjson = response.result + "|" + resultjson;

            return Json(resultjson);
        }

        public ActionResult EditWarehouseMaster(string usercode)
        {

            RequestWarehouseMaster request = new RequestWarehouseMaster();
            ResponseWarehouseMaster response = new ResponseWarehouseMaster();
            request.requestWarehouseMaster = new CUMIENTITY.WarehouseMasterEntity();
            request.requestWarehouseMaster.AUTOID = usercode;
            WarehouseMasterBC bc = new WarehouseMasterBC();
            response = bc.EditWarehouseMasterBC(request);
            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Warehousedetails);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult WarehouseMastersUploads()
        {
            string results = "";

            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;

                    var postedFile = System.Web.HttpContext.Current.Request.Files["CsvDoc"];

                    string path = Path.GetFileName(postedFile.FileName);

                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        if (file.FileName == postedFile.FileName)
                        {
                            if (file != null && file.ContentLength > 0)
                            {
                                Stream stream = file.InputStream;
                                IExcelDataReader reader = null;
                                if (file.FileName.EndsWith(".xls"))
                                {
                                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                                }
                                else if (file.FileName.EndsWith(".xlsx"))
                                {
                                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                                }
                                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                    {
                                        UseHeaderRow = true
                                    }
                                });
                                DataTable dataTable = result.Tables[0];

                                reader.Close();

                                results = Utility.DataTableToJSONWithJavaScriptSerializer(dataTable);

                                if (dataTable.Rows.Count > 0)
                                {
                                    DataTable dt = new DataTable();
                                    string check = string.Empty;

                                    int rowcount = 0;
                                    int rowcount1 = 0;
                                    string employeecodes = "";
                                    try
                                    {
                                        using (TransactionScope scope = new TransactionScope())
                                        {
                                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CUMI"].ConnectionString);
                                            {
                                                using (DataSet ds = new DataSet())
                                                {
                                                    foreach (DataRow req in dataTable.Rows)
                                                    {
                                                        SqlCommand cmd = new SqlCommand("[MASTERS].[WAREHOUSEMASTER_INSERT]", con);
                                                        cmd.CommandType = CommandType.StoredProcedure;
                                                        cmd.Parameters.AddWithValue("@WHCODE", string.IsNullOrEmpty(req["WAREHOUSECODE"].ToString()) ? string.Empty : req["WAREHOUSECODE"].ToString());
                                                        cmd.Parameters.AddWithValue("@WHNAME", string.IsNullOrEmpty(req["WAREHOUSENAME"].ToString()) ? string.Empty : req["WAREHOUSENAME"].ToString());
                                                        cmd.Parameters.AddWithValue("@PLANT", string.IsNullOrEmpty(req["PLANT"].ToString()) ? string.Empty : req["PLANT"].ToString());
                                                        cmd.Parameters.AddWithValue("@SUPERVISOR", string.IsNullOrEmpty(req["SUPERVISOR"].ToString()) ? string.Empty : req["SUPERVISOR"].ToString());
                                                        cmd.Parameters.AddWithValue("@ADDRESS1", string.IsNullOrEmpty(req["ADDRESS1"].ToString()) ? string.Empty : req["ADDRESS1"].ToString());
                                                        cmd.Parameters.AddWithValue("@ADDRESS2", string.IsNullOrEmpty(req["ADDRESS2"].ToString()) ? string.Empty : req["ADDRESS2"].ToString());
                                                        cmd.Parameters.AddWithValue("@ADDRESS3", string.IsNullOrEmpty(req["ADDRESS3"].ToString()) ? string.Empty : req["ADDRESS3"].ToString());
                                                        cmd.Parameters.AddWithValue("@CITY", string.IsNullOrEmpty(req["CITY"].ToString()) ? string.Empty : req["CITY"].ToString());
                                                        cmd.Parameters.AddWithValue("@STATE", string.IsNullOrEmpty(req["STATE"].ToString()) ? string.Empty : req["STATE"].ToString());
                                                        cmd.Parameters.AddWithValue("@PINCODE", string.IsNullOrEmpty(req["PINCODE"].ToString()) ? string.Empty : req["PINCODE"].ToString());
                                                        cmd.Parameters.AddWithValue("@REMARKS", string.IsNullOrEmpty(req["REMARKS"].ToString()) ? string.Empty : req["REMARKS"].ToString());
                                                        cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", "MDSUB_001_0001"));
                                                        cmd.Parameters.Add(new SqlParameter("@USERCODE", Session["LoginEmployeeCode"].ToString()));
                                                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                                                        //DataSet ds2 = new DataSet();
                                                        da.Fill(ds);

                                                        rowcount++;
                                                    }

                                                    if (dataTable.Rows.Count == rowcount && ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                                                    {
                                                        results = "Successfully uploaded!!!";
                                                        scope.Complete();
                                                        results = "true";

                                                    }
                                                    else
                                                    {
                                                        results = "Already uploaded";
                                                    }
                                                }

                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                                        string responselog = "";
                                        results = "false";
                                    }
                                }
                                else
                                {
                                    results = "false" + "|" + "Uploaded Excel File is Empty. ";
                                }

                            }
                        }
                        else
                        {
                            results = "False" + "|" + "Uploaded InValid Excel File. ";
                        }

                    }

                }
                catch (Exception ex)
                {
                    results = "False" + "|" + "Error occurred. Error details: ";

                }
            }

            JsonResult json = Json(results, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        #endregion

        #region Warehouse Location Master.........................................................................

        public ActionResult WarehouseLocationMaster()
        {
            return View();
        }

        public ActionResult WarehouseMasterLocationPageLoad()
        {
            RequestWarehouseLocationMaster request = new RequestWarehouseLocationMaster();
            ResponseWarehouseLocationMaster response = new ResponseWarehouseLocationMaster();
            WarehouseLocationBC bc = new WarehouseLocationBC();
            response = bc.WarehouseLocationMasterPageLoadBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Plant) + "|" +
                Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Warehouse) + "|" +
                Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Locationdetails);

            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult WarehouseLocationMasterPrintView()
        {
            RequestWarehouseLocationMaster request = new RequestWarehouseLocationMaster();
            ResponseWarehouseLocationMaster response = new ResponseWarehouseLocationMaster();
            WarehouseLocationBC bc = new WarehouseLocationBC();
            response = bc.WarehouseLocationMasterPrintViewBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_PrintView);
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PrintBarcodeByLocation(string locationcode, string locationname, string locationdescription)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string[] locationcodearr = locationcode.Split('^');
            string[] locationnamearr = locationname.Split('^');
            string[] locationdescriptionarr = locationdescription.Split('^');

            for (int i = 0; i < locationcodearr.Length; i++)
            {
                String test = @"<div style='margin-top:2px;margin-left:30px;margin-right:0px;height:110px;width:300px;' id='$qrcode$'></div><div><span style='font-weight: 900;font-size: 12px;margin-left:10px;'>Location Code</span><span> : </span><span style='font-size: 12px;'>$LOCATIONCODE$</span></div><div><span style='font-weight: 900;font-size: 12px;margin-left:10px;'>Location Name</span><span> : </span><span style='font-size: 12px;'>$LOCATIONNAME$</span></div><div><span style='font-weight: 900;font-size: 12px;margin-left:10px;'>Capacity</span><span> : </span><span style='font-size: 12px;'>$CAPACITY$</span></div>";

                byte[] byteArray = Encoding.ASCII.GetBytes(test);
                MemoryStream stream = new MemoryStream(byteArray);
                //StreamReader streamReader = new StreamReader(stream);
                StreamReader streamReader = new StreamReader(LOCATIONLABEL);
                string str_ = streamReader.ReadToEnd();
                string str_2 = str_;
                str_2 = str_2.Replace("$LOCATIONCODE$", locationcodearr[i]);
                str_2 = str_2.Replace("$qrcode$", "locationid" + i);

                stringBuilder.Append(str_2);
            }
            var data = stringBuilder.ToString();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetWarehouseLocationMasterpartcodebyID(string partcode)
        {

            RequestWarehouseLocationMaster request = new RequestWarehouseLocationMaster();
            ResponseWarehouseLocationMaster response = new ResponseWarehouseLocationMaster();
            request.requestWarehouseLocationMaster = new WarehouseLocationMasterEntity();
            request.requestWarehouseLocationMaster.PLANTCODE = partcode;
            WarehouseLocationBC bc = new WarehouseLocationBC();
            response = bc.FetchWarehouseMasterLocationPartcodeBC(request);
            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Plant);
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
      
        public ActionResult InsertWarehouseLocationMaster(string AUTOID, string PLANT, string WAREHOUSE,string LOCATIONCODE, string LOCATION, string RECORDSTATUS,string REMARKS, string actiontype)
        {
            RequestWarehouseLocationMaster request = new RequestWarehouseLocationMaster();
            ResponseWarehouseLocationMaster response = new ResponseWarehouseLocationMaster();
            request.requestWarehouseLocationMaster = new CUMIENTITY.WarehouseLocationMasterEntity();
            request.requestWarehouseLocationMaster.PLANTCODE = PLANT;
            request.requestWarehouseLocationMaster.WAREHOUSECODE = WAREHOUSE;
            request.requestWarehouseLocationMaster.LOCATIONCODE = LOCATIONCODE;
            request.requestWarehouseLocationMaster.LOCATION = LOCATION;
            request.requestWarehouseLocationMaster.RECORDSTATUS = RECORDSTATUS;
            request.requestWarehouseLocationMaster.REMARKS = REMARKS;
            request.requestWarehouseLocationMaster.USERCODE = Session["LoginEmployeeCode"].ToString();
            WarehouseLocationBC bc = new WarehouseLocationBC();
            string value = "Update";
            if (value != actiontype)
            {
                response = bc.InsertWarehouseLocationMasterBC(request);
            }
            else
            {
                request.requestWarehouseLocationMaster.AUTOID = AUTOID;
                response = bc.UpdateWarehouseLocationMasterBC(request);
            }
            ManageError Err = new ManageError();

            string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
            string result = resultjson.Remove(0, 2);
            resultjson = result;
            resultjson = response.result + "|" + resultjson;

            return Json(resultjson);
        }

        public ActionResult EditWarehouseLocationMaster(string usercode)
        {

            RequestWarehouseLocationMaster request = new RequestWarehouseLocationMaster();
            ResponseWarehouseLocationMaster response = new ResponseWarehouseLocationMaster();
            request.requestWarehouseLocationMaster = new CUMIENTITY.WarehouseLocationMasterEntity();
            request.requestWarehouseLocationMaster.AUTOID = usercode;
          //  request.requestWarehouseLocationMaster.WAREHOUSE = warehouse;
            WarehouseLocationBC bc = new WarehouseLocationBC();
            response = bc.EditWarehouseLocationMasterBC(request);
            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Locationdetails);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult WarehouseLocationMastersUploads()
        {
            string results = "";

            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;

                    var postedFile = System.Web.HttpContext.Current.Request.Files["CsvDoc"];

                    string path = Path.GetFileName(postedFile.FileName);

                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        if (file.FileName == postedFile.FileName)
                        {
                            if (file != null && file.ContentLength > 0)
                            {
                                Stream stream = file.InputStream;
                                IExcelDataReader reader = null;
                                if (file.FileName.EndsWith(".xls"))
                                {
                                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                                }
                                else if (file.FileName.EndsWith(".xlsx"))
                                {
                                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                                }
                                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                    {
                                        UseHeaderRow = true
                                    }
                                });
                                DataTable dataTable = result.Tables[0];

                                reader.Close();

                                results = Utility.DataTableToJSONWithJavaScriptSerializer(dataTable);

                                if (dataTable.Rows.Count > 0)
                                {
                                    DataTable dt = new DataTable();
                                    string check = string.Empty;

                                    int rowcount = 0;
                                    int rowcount1 = 0;
                                    string employeecodes = "";
                                    try
                                    {
                                        using (TransactionScope scope = new TransactionScope())
                                        {
                                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CUMI"].ConnectionString);
                                            {
                                                using (DataSet ds = new DataSet())
                                                {
                                                    foreach (DataRow req in dataTable.Rows)
                                                    {
                                                        SqlCommand cmd = new SqlCommand("[MASTERS].[WAREHOUSELOCATIONMASTER_INSERT]", con);
                                                        cmd.CommandType = CommandType.StoredProcedure;
                                                        cmd.Parameters.AddWithValue("@PLANT", string.IsNullOrEmpty(req["PLANT"].ToString()) ? string.Empty : req["PLANT"].ToString());
                                                        cmd.Parameters.AddWithValue("@WAREHOUSE", string.IsNullOrEmpty(req["WAREHOUSENAME"].ToString()) ? string.Empty : req["WAREHOUSENAME"].ToString());
                                                        cmd.Parameters.AddWithValue("@LOCATIONCODE", string.IsNullOrEmpty(req["LOCATIONCODE"].ToString()) ? string.Empty : req["LOCATIONCODE"].ToString());
                                                        cmd.Parameters.AddWithValue("@LOCATIONNAME", string.IsNullOrEmpty(req["LOCATIONNAME"].ToString()) ? string.Empty : req["LOCATIONNAME"].ToString());
                                                        cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", "MDSUB_001_0001"));
                                                        cmd.Parameters.AddWithValue("@REMARKS", string.IsNullOrEmpty(req["REMARKS"].ToString()) ? string.Empty : req["REMARKS"].ToString());
                                                        cmd.Parameters.Add(new SqlParameter("@USERCODE", Session["LoginEmployeeCode"].ToString()));
                                                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                                                        //DataSet ds2 = new DataSet();
                                                        da.Fill(ds);

                                                        rowcount++;
                                                    }

                                                    if (dataTable.Rows.Count == rowcount && ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                                                    {
                                                        results = "Successfully uploaded!!!";
                                                        scope.Complete();
                                                        results = "true";

                                                    }
                                                    else
                                                    {
                                                        results = "Already uploaded";
                                                    }
                                                }

                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                                        string responselog = "";
                                        results = "false";
                                    }
                                }
                                else
                                {
                                    results = "false" + "|" + "Uploaded Excel File is Empty. ";
                                }

                            }
                        }
                        else
                        {
                            results = "False" + "|" + "Uploaded InValid Excel File. ";
                        }

                    }

                }
                catch (Exception ex)
                {
                    results = "False" + "|" + "Error occurred. Error details: ";

                }
            }

            JsonResult json = Json(results, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        #endregion

        #region FG plant Master .............................................................................
        public ActionResult FGPlantMaster()
        {
            return View();
        }
        public ActionResult GetFGPlantMasterPageLoad()
        {
            RequestFGPlantMaster request = new RequestFGPlantMaster();
            ResponseFGPlantMaster response = new ResponseFGPlantMaster();
            FGPlantMasterBC bc = new FGPlantMasterBC();
            response = bc.FGPlantMasterPageLoadBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Recordstatus)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_UOM)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Plant)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_group)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Itemsubgroup)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Category)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_FgplantmasterDetails);
            var data = resulttable;
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }

        public ActionResult GetLocationMasterpartcodebyID(string partcode)
        {

            RequestFGPlantMaster request = new RequestFGPlantMaster();
            ResponseFGPlantMaster response = new ResponseFGPlantMaster();
            request.requestfgplantmaster = new FGPlantMasterEntity();
            request.requestfgplantmaster.PLANTCODE = partcode;
            FGPlantMasterBC bc = new FGPlantMasterBC();
            response = bc.FetchLocationPartcodeBC(request);
            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Plant);
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult InsertFGPlantMaster(string actiontype,string ITEMSUBGROUP, string AUTOID, string PLANTCODE, string LOCATION, string FGITEMCODE, string VARIANTCODE, string DESCRIPTION, string UOM, string WEIGHT, string GROUP, string CATEGORY, string RECORDSTATUS,string REMARKS)
        {
            RequestFGPlantMaster request = new RequestFGPlantMaster();
            ResponseFGPlantMaster response = new ResponseFGPlantMaster();
            request.requestfgplantmaster = new FGPlantMasterEntity();
            request.requestfgplantmaster.PLANTCODE = PLANTCODE;
            request.requestfgplantmaster.LOCATION = LOCATION;
            request.requestfgplantmaster.FGITEMCODE = FGITEMCODE;
            request.requestfgplantmaster.VARIANTCODE = VARIANTCODE;
            request.requestfgplantmaster.DESCRIPTION = DESCRIPTION;
            request.requestfgplantmaster.UOM = UOM;
            request.requestfgplantmaster.Weight = WEIGHT;
            request.requestfgplantmaster.GROUP = GROUP;
            request.requestfgplantmaster.ITEMSUBGROUP = ITEMSUBGROUP;
            request.requestfgplantmaster.CATEGORY = CATEGORY;
            request.requestfgplantmaster.RECORDSTATUS = RECORDSTATUS;
            request.requestfgplantmaster.REMARKS = REMARKS;
            request.requestfgplantmaster.USERCODE = Session["LoginEmployeeCode"].ToString();
            FGPlantMasterBC bc = new FGPlantMasterBC();
            string value = "Update";
            if (value != actiontype)
            {
                response = bc.InsertFGPlantMasterBC(request);
            }
            else
            {
                request.requestfgplantmaster.AUTOID = AUTOID;
                response = bc.UpdateFGPlantMasterBC(request);
            }

            ManageError Err = new ManageError();

            string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
            string result = resultjson.Remove(0, 2);
            resultjson = result;
            resultjson = response.result + "|" + resultjson;

            JsonResult json = Json(resultjson, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;

        }
        public ActionResult EditFGPlantMaster(string ID)
        {

            RequestFGPlantMaster request = new RequestFGPlantMaster();
            ResponseFGPlantMaster response = new ResponseFGPlantMaster();
            request.requestfgplantmaster = new FGPlantMasterEntity();
            request.requestfgplantmaster.AUTOID = ID;
            FGPlantMasterBC bc = new FGPlantMasterBC();
            response = bc.EditFGPlantMasterBC(request);

            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_FgplantmasterDetails);
            ;
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult FGMastersUploads()
        {
            string results = "";

            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;

                    var postedFile = System.Web.HttpContext.Current.Request.Files["CsvDoc"];

                    string path = Path.GetFileName(postedFile.FileName);

                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        if (file.FileName == postedFile.FileName)
                        {
                            if (file != null && file.ContentLength > 0)
                            {
                                Stream stream = file.InputStream;
                                IExcelDataReader reader = null;
                                if (file.FileName.EndsWith(".xls"))
                                {
                                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                                }
                                else if (file.FileName.EndsWith(".xlsx"))
                                {
                                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                                }
                                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                    {
                                        UseHeaderRow = true
                                    }
                                });
                                DataTable dataTable = result.Tables[0];

                                reader.Close();

                                results = Utility.DataTableToJSONWithJavaScriptSerializer(dataTable);

                                if (dataTable.Rows.Count > 0)
                                {
                                    DataTable dt = new DataTable();
                                    string check = string.Empty;
                                    int rowcount = 0;
                                    string Grp = "";
                                    string Grp1 = "";
                                    string subcategory1 = "";
                                    string subcategory2 = "";
                                    int rowcount1 = 0;
                                    string employeecodes = "";
                                    try
                                    {
                                        using (TransactionScope scope = new TransactionScope())
                                        {
                                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CUMI"].ConnectionString);
                                            {
                                                using (DataSet ds = new DataSet())
                                                {
                                                    foreach (DataRow req in dataTable.Rows)
                                                    {
                                                        string[] itemsubgrp = req["ITEMSUBGROUP"].ToString().Split(',');

                                                        if (itemsubgrp.Length > 0)
                                                        {
                                                            for (int k = 0; k < itemsubgrp.Length; k++)
                                                            {
                                                                Grp = itemsubgrp[0];

                                                                if (Grp.ToUpper() == "REFRACTORIES")
                                                                {
                                                                    subcategory1 = "MDSUB_004_0001";
                                                                }
                                                                else if (Grp.ToUpper() == "FIRED")
                                                                {
                                                                    subcategory2 = "MDSUB_004_0002";
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            for (int k = 0; k < itemsubgrp.Length; k++)
                                                            {

                                                                Grp = itemsubgrp[0];
                                                                Grp1 = itemsubgrp[1];

                                                                if (Grp.ToUpper() == "REFRACTORIES")
                                                                {
                                                                    subcategory1 = "MDSUB_004_0001";
                                                                }
                                                                else if (Grp1.ToUpper() == "FIRED")
                                                                {
                                                                    subcategory2 = "MDSUB_004_0002";
                                                                }
                                                            }
                                                        }

                                                        SqlCommand cmd = new SqlCommand("[MASTERS].[FGPLANTMASTER_INSERT]", con);
                                                        cmd.CommandType = CommandType.StoredProcedure;

                                                        cmd.Parameters.AddWithValue("@PLANTCODE", string.IsNullOrEmpty(req["PLANTCODE"].ToString()) ? string.Empty : req["PLANTCODE"].ToString());
                                                        cmd.Parameters.AddWithValue("@LOCATION", string.IsNullOrEmpty(req["LOCATION"].ToString()) ? string.Empty : req["LOCATION"].ToString());
                                                        cmd.Parameters.AddWithValue("@FGITEMCODE", string.IsNullOrEmpty(req["FGITEMCODE"].ToString()) ? string.Empty : req["FGITEMCODE"].ToString());
                                                        cmd.Parameters.AddWithValue("@VARIANTCODE", string.IsNullOrEmpty(req["VARIANTCODE"].ToString()) ? string.Empty : req["VARIANTCODE"].ToString());
                                                        cmd.Parameters.AddWithValue("@DESCRIPTION", string.IsNullOrEmpty(req["DESCRIPTION"].ToString()) ? string.Empty : req["DESCRIPTION"].ToString());

                                                        if (req["UOM"].ToString().ToUpper() == "NOS")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@UOM", "MDSUB_002_0002"));
                                                        }
                                                        else if (req["UOM"].ToString().ToUpper() == "SET")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@UOM", "MDSUB_002_0003"));
                                                        }
                                                        else if (req["UOM"].ToString().ToUpper() == "LTR")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@UOM", "MDSUB_002_0004"));
                                                        }
                                                        else if (req["UOM"].ToString().ToUpper() == "KGS")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@UOM", "MDSUB_002_0005"));
                                                        }
                                                        cmd.Parameters.AddWithValue("@WEIGHT", string.IsNullOrEmpty(req["WEIGHT"].ToString()) ? string.Empty : req["WEIGHT"].ToString());

                                                        if (req["GROUP"].ToString().ToUpper() == "CUMILITE 65A SPL")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@GROUP", "MDSUB_005_0001"));
                                                        }

                                                        cmd.Parameters.AddWithValue("@ITEMSUBGROUP", subcategory1 + ',' + subcategory2);

                                                        if (req["CATEGORY"].ToString().ToUpper() == "N/A")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@CATEGORY", "MDSUB_003_0005"));
                                                        }
                                                        else if (req["CATEGORY"].ToString().ToUpper() == "SHAPE")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@CATEGORY", "MDSUB_003_0001"));
                                                        }
                                                        else if (req["CATEGORY"].ToString().ToUpper() == "TILE")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@CATEGORY", "MDSUB_003_0002"));
                                                        }
                                                        else if (req["CATEGORY"].ToString().ToUpper() == "BRICK")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@CATEGORY", "MDSUB_003_0003"));
                                                        }
                                                        else if (req["CATEGORY"].ToString().ToUpper() == "BLOCK")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@CATEGORY", "MDSUB_003_0004"));
                                                        }
                                                        cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", "MDSUB_001_0001"));
                                                        cmd.Parameters.AddWithValue("@REMARKS", string.IsNullOrEmpty(req["REMARKS"].ToString()) ? string.Empty : req["REMARKS"].ToString());
                                                        cmd.Parameters.Add(new SqlParameter("@USERCODE", Session["LoginEmployeeCode"].ToString()));
                                                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                                                        //DataSet ds2 = new DataSet();
                                                        da.Fill(ds);

                                                        rowcount++;
                                                    }

                                                    if (dataTable.Rows.Count == rowcount && ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                                                    {
                                                        results = "Successfully uploaded!!!";
                                                        scope.Complete();
                                                        results = "true";

                                                    }
                                                    else
                                                    {
                                                        results = "Already uploaded";
                                                    }
                                                }

                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                                        string responselog = "";
                                        results = "false";
                                    }
                                }
                                else
                                {
                                    results = "false" + "|" + "Uploaded Excel File is Empty. ";
                                }

                            }
                        }
                        else
                        {
                            results = "False" + "|" + "Uploaded InValid Excel File. ";
                        }

                    }

                }
                catch (Exception ex)
                {
                    results = "False" + "|" + "Error occurred. Error details: ";

                }
            }

            JsonResult json = Json(results, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        #endregion

        #region RM Master .............................................................................
        public ActionResult RMMaster()
        {
            return View();
        }
        public ActionResult GetRMMasterPageLoad()
        {
            RequestRMMaster request = new RequestRMMaster();
            ResponseRMMaster response = new ResponseRMMaster();
            RMMasterBC bc = new RMMasterBC();
            response = bc.RMMasterPageLoadBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Recordstatus)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_UOM)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Plant)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Group)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_ITEMSUBGROUP)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_CATEGORY)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_rmmasterDetails);
            var data = resulttable;
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }

        public ActionResult GetLocationpartcodebyID(string partcode)
        {

            RequestRMMaster request = new RequestRMMaster();
            ResponseRMMaster response = new ResponseRMMaster();
            request.requestrmmaster = new RMMasterEntity();
            request.requestrmmaster.PLANTCODE = partcode;
            RMMasterBC bc = new RMMasterBC();
            response = bc.FetchLocationPartcodeBC(request);
            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Plant);
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult InsertRMMaster(string actiontype, string AUTOID, string PLANTCODE, string LOCATION, string RMITEMCODE, string VARIANTCODE, string DESCRIPTION, string UOM, string PACKSIZE, string GROUP, string CATEGORY, string RECORDSTATUS,string REMARKS,string ITEMSUBGROUP)
        {
            RequestRMMaster request = new RequestRMMaster();
            ResponseRMMaster response = new ResponseRMMaster();
            request.requestrmmaster = new RMMasterEntity();
            request.requestrmmaster.PLANTCODE = PLANTCODE;
            request.requestrmmaster.LOCATION = LOCATION;
            request.requestrmmaster.RMITEMCODE = RMITEMCODE;
            request.requestrmmaster.VARIANTCODE = VARIANTCODE;
            request.requestrmmaster.RMDESCRIPTION = DESCRIPTION;
            request.requestrmmaster.UOM = UOM;
            request.requestrmmaster.PACKSIZE = PACKSIZE;
            request.requestrmmaster.GROUP = GROUP;
            request.requestrmmaster.ITEMSUBGROUP = ITEMSUBGROUP;
            request.requestrmmaster.CATEGORY = CATEGORY;
            request.requestrmmaster.RECORDSTATUS = RECORDSTATUS;
            request.requestrmmaster.REMARKS = REMARKS;
            request.requestrmmaster.USERCODE = Session["LoginEmployeeCode"].ToString();
            RMMasterBC bc = new RMMasterBC();
            string value = "Update";
            if (value != actiontype)
            {
                response = bc.InsertRMMasterBC(request);
            }
            else
            {
                request.requestrmmaster.AUTOID = AUTOID;
                response = bc.UpdateRMMasterBC(request);
            }

            ManageError Err = new ManageError();

            string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
            resultjson = response.result + "|" + resultjson;

            JsonResult json = Json(resultjson, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;

        }
        public ActionResult EditRMMaster(string ID)
        {

            RequestRMMaster request = new RequestRMMaster();
            ResponseRMMaster response = new ResponseRMMaster();
            request.requestrmmaster = new RMMasterEntity();
            request.requestrmmaster.AUTOID = ID;
            RMMasterBC bc = new RMMasterBC();
            response = bc.EditRMMasterBC(request);

            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_rmmasterDetails);
            ;
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        #endregion

        #region Mold Master .............................................................................
        public ActionResult MoldMaster()
        {
            return View();
        }
        public ActionResult GetMoldMasterPageLoad()
        {
            RequestMoldMaster request = new RequestMoldMaster();
            ResponseMoldMaster response = new ResponseMoldMaster();
            MoldMasterBC bc = new MoldMasterBC();
            response = bc.MoldMasterPageLoadBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Recordstatus)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_UOM)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Plant)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_group)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Itemsubgroup)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Category)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_moldmasterDetails)
                         +"|"+Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Moldtype);
            var data = resulttable;
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }

        public ActionResult GetpartcodebyID(string partcode)
        {

            RequestMoldMaster request = new RequestMoldMaster();
            ResponseMoldMaster response = new ResponseMoldMaster();
            request.requestrmoldmaster = new MoldMasterEntity();
            request.requestrmoldmaster.PLANTCODE = partcode;
            MoldMasterBC bc = new MoldMasterBC();
            response = bc.FetchLocationPartcodeBC(request);
            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Plant);
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult InsertMoldMaster(string SUPPLIERADDRESS, string actiontype,string ITEMSUBGROUP,string REMARKS, string AUTOID, string PLANTCODE, string LOCATION,string MOLDTYPE,string BATCHNUMBER, string SUPPLIERCODE,string SUPPLIERNAME,  string MOLDITEMCODE,string PONUMBER, string VARIANTCODE, string DESCRIPTION, string UOM, string PACKSIZE, string GROUP, string CATEGORY, string RECORDSTATUS)
        {
            RequestMoldMaster request = new RequestMoldMaster();
            ResponseMoldMaster response = new ResponseMoldMaster();
            request.requestrmoldmaster = new MoldMasterEntity();
            request.requestrmoldmaster.PLANTCODE = PLANTCODE;
            request.requestrmoldmaster.LOCATION = LOCATION;
            request.requestrmoldmaster.MOLDTYPE = MOLDTYPE;
            request.requestrmoldmaster.MOLDITEMCODE = MOLDITEMCODE;
            request.requestrmoldmaster.MOLDDESCRIPTION = DESCRIPTION;
            //request.requestrmoldmaster.PONUMBER = PONUMBER;
            //request.requestrmoldmaster.BATCHNUMBER = BATCHNUMBER;
            request.requestrmoldmaster.SUPPLIERCODE = SUPPLIERCODE;
            request.requestrmoldmaster.SUPPLIERNAME = SUPPLIERNAME;
            request.requestrmoldmaster.SUPPLIERADDRESS = SUPPLIERADDRESS;
            request.requestrmoldmaster.VARIANTCODE = VARIANTCODE;
            request.requestrmoldmaster.UOM = UOM;           
            request.requestrmoldmaster.GROUP = GROUP;
            request.requestrmoldmaster.ITEMSUBGROUP = ITEMSUBGROUP;
            request.requestrmoldmaster.CATEGORY = CATEGORY;
            request.requestrmoldmaster.RECORDSTATUS = RECORDSTATUS;
            request.requestrmoldmaster.REMARKS = REMARKS;
            request.requestrmoldmaster.USERCODE = Session["LoginEmployeeCode"].ToString();
            MoldMasterBC bc = new MoldMasterBC();
            string value = "Update";
            if (value != actiontype)
            {
                response = bc.InsertMoldMasterBC(request);
            }
            else
            {
                request.requestrmoldmaster.AUTOID = AUTOID;
                response = bc.UpdateMoldMasterBC(request);
            }

            ManageError Err = new ManageError();

            string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
            string result = resultjson.Remove(0, 2);
            resultjson = result;
            resultjson = response.result + "|" + resultjson;

            JsonResult json = Json(resultjson, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;

        }
        public ActionResult EditMoldMaster(string ID)
        {

            RequestMoldMaster request = new RequestMoldMaster();
            ResponseMoldMaster response = new ResponseMoldMaster();
            request.requestrmoldmaster = new MoldMasterEntity();
            request.requestrmoldmaster.AUTOID = ID;
            MoldMasterBC bc = new MoldMasterBC();
            response = bc.EditMoldMasterBC(request);

            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_moldmasterDetails);
           
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult MoldMastersUploads()
        {
            string results = "";

            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;

                    var postedFile = System.Web.HttpContext.Current.Request.Files["CsvDoc"];

                    string path = Path.GetFileName(postedFile.FileName);

                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        if (file.FileName == postedFile.FileName)
                        {
                            if (file != null && file.ContentLength > 0)
                            {
                                Stream stream = file.InputStream;
                                IExcelDataReader reader = null;
                                if (file.FileName.EndsWith(".xls"))
                                {
                                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                                }
                                else if (file.FileName.EndsWith(".xlsx"))
                                {
                                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                                }
                                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                    {
                                        UseHeaderRow = true
                                    }
                                });
                                DataTable dataTable = result.Tables[0];

                                reader.Close();

                                results = Utility.DataTableToJSONWithJavaScriptSerializer(dataTable);

                                if (dataTable.Rows.Count > 0)
                                {
                                    DataTable dt = new DataTable();
                                    string check = string.Empty;
                                    int rowcount = 0;
                                    string Grp = "";
                                    string Grp1 = "";
                                    string subcategory1 = "";
                                    string subcategory2 = "";
                                    try
                                    {
                                        using (TransactionScope scope = new TransactionScope())
                                        {
                                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CUMI"].ConnectionString);
                                            {
                                                using (DataSet ds = new DataSet())
                                                {
                                                    foreach (DataRow req in dataTable.Rows)
                                                    {
                                                        string[] itemsubgrp = req["ITEMSUBGROUP"].ToString().Split(',');

                                                        if (itemsubgrp.Length > 0)
                                                        {
                                                            for (int k = 0; k < itemsubgrp.Length; k++)
                                                            {

                                                                Grp = itemsubgrp[0];
                                                                // Grp1 = itemsubgrp[1];

                                                                if (Grp.ToUpper() == "REFRACTORIES")
                                                                {
                                                                    subcategory1 = "MDSUB_004_0001";
                                                                }
                                                                else if (Grp.ToUpper() == "FIRED")
                                                                {
                                                                    subcategory2 = "MDSUB_004_0002";
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            for (int k = 0; k < itemsubgrp.Length; k++)
                                                            {

                                                                Grp = itemsubgrp[0];
                                                                Grp1 = itemsubgrp[1];

                                                                if (Grp.ToUpper() == "REFRACTORIES")
                                                                {
                                                                    subcategory1 = "MDSUB_004_0001";
                                                                }
                                                                else if (Grp1.ToUpper() == "FIRED")
                                                                {
                                                                    subcategory2 = "MDSUB_004_0002";
                                                                }
                                                            }
                                                        }
                                                        SqlCommand cmd = new SqlCommand("[MASTERS].[MOLDMASTER_INSERT]", con);
                                                        cmd.CommandType = CommandType.StoredProcedure;

                                                        cmd.Parameters.AddWithValue("@PLANTCODE", string.IsNullOrEmpty(req["PLANTCODE"].ToString()) ? string.Empty : req["PLANTCODE"].ToString());
                                                        cmd.Parameters.AddWithValue("@LOCATION", string.IsNullOrEmpty(req["LOCATION"].ToString()) ? string.Empty : req["LOCATION"].ToString());
                                                        cmd.Parameters.AddWithValue("@DESCRIPTION", string.IsNullOrEmpty(req["DESCRIPTION"].ToString()) ? string.Empty : req["DESCRIPTION"].ToString());
                                                        cmd.Parameters.AddWithValue("@MOLDITEMCODE", string.IsNullOrEmpty(req["MOLDITEMCODE"].ToString()) ? string.Empty : req["MOLDITEMCODE"].ToString());
                                                        cmd.Parameters.AddWithValue("@PONUMBER", string.IsNullOrEmpty(req["PONUMBER"].ToString()) ? string.Empty : req["PONUMBER"].ToString());
                                                        cmd.Parameters.AddWithValue("@BATCHNUMBER", string.IsNullOrEmpty(req["BATCHNUMBER"].ToString()) ? string.Empty : req["BATCHNUMBER"].ToString());
                                                        cmd.Parameters.AddWithValue("@SUPPLIERCODE", string.IsNullOrEmpty(req["SUPPLIERCODE"].ToString()) ? string.Empty : req["SUPPLIERCODE"].ToString());
                                                        cmd.Parameters.AddWithValue("@SUPPLIERNAME", string.IsNullOrEmpty(req["SUPPLIERNAME"].ToString()) ? string.Empty : req["SUPPLIERNAME"].ToString());
                                                        cmd.Parameters.AddWithValue("@SUPPLIERADDRESS", string.IsNullOrEmpty(req["SUPPLIERADDRESS"].ToString()) ? string.Empty : req["SUPPLIERADDRESS"].ToString());
                                                        cmd.Parameters.AddWithValue("@VARIANTCODE", string.IsNullOrEmpty(req["VARIANTCODE"].ToString()) ? string.Empty : req["VARIANTCODE"].ToString());

                                                        if (req["MOLDTYPE"].ToString().ToUpper() == "WOODEN")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@MOLDTYPE", "MDSUB_015_0001"));
                                                        }
                                                        else if (req["MOLDTYPE"].ToString().ToUpper() == "STEEL")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@MOLDTYPE", "MDSUB_015_0002"));
                                                        }


                                                        if (req["UOM"].ToString().ToUpper() == "NOS")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@UOM", "MDSUB_002_0002"));
                                                        }
                                                        else if (req["UOM"].ToString().ToUpper() == "SET")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@UOM", "MDSUB_002_0003"));
                                                        }
                                                        else if (req["UOM"].ToString().ToUpper() == "LTR")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@UOM", "MDSUB_002_0004"));
                                                        }
                                                        else if (req["UOM"].ToString().ToUpper() == "KGS")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@UOM", "MDSUB_002_0005"));
                                                        }
                                                        // cmd.Parameters.AddWithValue("@WEIGHT", string.IsNullOrEmpty(req["WEIGHT"].ToString()) ? string.Empty : req["WEIGHT"].ToString());

                                                        if (req["GROUP"].ToString().ToUpper() == "TW")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@GROUP", "MDSUB_007_0001"));
                                                        }
                                                        else if (req["GROUP"].ToString().ToUpper() == "KVM")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@GROUP", "MDSUB_007_0002"));
                                                        }
                                                        else if (req["GROUP"].ToString().ToUpper() == "MS")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@GROUP", "MDSUB_007_0003"));
                                                        }
                                                        else if (req["GROUP"].ToString().ToUpper() == "AL")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@GROUP", "MDSUB_007_0005"));
                                                        }
                                                        else if (req["GROUP"].ToString().ToUpper() == "PLY")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@GROUP", "MDSUB_007_0004"));
                                                        }

                                                        cmd.Parameters.AddWithValue("@ITEMSUBGROUP", subcategory1 + ',' + subcategory2);

                                                        if (req["CATEGORY"].ToString().ToUpper() == "N/A")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@CATEGORY", "MDSUB_003_0005"));
                                                        }
                                                        else if (req["CATEGORY"].ToString().ToUpper() == "SHAPE")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@CATEGORY", "MDSUB_003_0001"));
                                                        }
                                                        else if (req["CATEGORY"].ToString().ToUpper() == "TILE")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@CATEGORY", "MDSUB_003_0002"));
                                                        }
                                                        else if (req["CATEGORY"].ToString().ToUpper() == "BRICK")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@CATEGORY", "MDSUB_003_0003"));
                                                        }
                                                        else if (req["CATEGORY"].ToString().ToUpper() == "BLOCK")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@CATEGORY", "MDSUB_003_0004"));
                                                        }
                                                        cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", "MDSUB_001_0001"));
                                                        cmd.Parameters.AddWithValue("@REMARKS", string.IsNullOrEmpty(req["REMARKS"].ToString()) ? string.Empty : req["REMARKS"].ToString());
                                                        cmd.Parameters.Add(new SqlParameter("@USERCODE", Session["LoginEmployeeCode"].ToString()));
                                                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                                                        //DataSet ds2 = new DataSet();
                                                        da.Fill(ds);

                                                        rowcount++;
                                                    }
                                                    if (dataTable.Rows.Count == rowcount && ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                                                    {
                                                        results = "Successfully uploaded!!!";
                                                        scope.Complete();
                                                        results = "true";

                                                    }
                                                    else
                                                    {
                                                        results = "Already uploaded";
                                                    }
                                                }

                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                                        string responselog = "";
                                        results = "false";
                                    }
                                }
                                else
                                {
                                    results = "false" + "|" + "Uploaded Excel File is Empty. ";
                                }

                            }
                        }
                        else
                        {
                            results = "False" + "|" + "Uploaded InValid Excel File. ";
                        }

                    }

                }
                catch (Exception ex)
                {
                    results = "False" + "|" + "Error occurred. Error details: ";

                }
            }

            JsonResult json = Json(results, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        #endregion

        #region Child Part Master .............................................................................
        public ActionResult ChildPartMaster()
        {
            return View();
        }
        public ActionResult GetChildPartMasterPageLoad()
        {
            RequestChildPartMaster request = new RequestChildPartMaster();
            ResponseChildPartMaster response = new ResponseChildPartMaster();
            ChildPartMasterBC bc = new ChildPartMasterBC();
            response = bc.ChildPartMasterPageLoadBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Recordstatus)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_UOM)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Plant)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_ChildpartmasterDetails);
            var data = resulttable;
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }

        public ActionResult Getpartcode(string partcode)
        {

            RequestChildPartMaster request = new RequestChildPartMaster();
            ResponseChildPartMaster response = new ResponseChildPartMaster();
            request.requestchildpartmaster = new ChildPartMasterEntity();
            request.requestchildpartmaster.PLANTCODE = partcode;
            ChildPartMasterBC bc = new ChildPartMasterBC();
            response = bc.FetchLocationPartcodeBC(request);
            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Plant);
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult InsertChildPartMaster(string actiontype, string AUTOID, string PLANTCODE, string LOCATION, string CHILDITEMCODE, string VARIANTCODE, string DESCRIPTION, string UOM, string QUANTITY,  string RECORDSTATUS)
        {
            RequestChildPartMaster request = new RequestChildPartMaster();
            ResponseChildPartMaster response = new ResponseChildPartMaster();
            request.requestchildpartmaster = new ChildPartMasterEntity();
            request.requestchildpartmaster.PLANTCODE = PLANTCODE;
            request.requestchildpartmaster.LOCATION = LOCATION;
            request.requestchildpartmaster.CHILDITEMCODE = CHILDITEMCODE;    
            request.requestchildpartmaster.DESCRIPTION = DESCRIPTION;
            request.requestchildpartmaster.UOM = UOM;
            request.requestchildpartmaster.QUANTITY = QUANTITY;
            request.requestchildpartmaster.RECORDSTATUS = RECORDSTATUS;
            request.requestchildpartmaster.USERCODE = Session["LoginEmployeeCode"].ToString();
            ChildPartMasterBC bc = new ChildPartMasterBC();
            string value = "Update";
            if (value != actiontype)
            {
                response = bc.InsertChildPartMasterBC(request);
            }
            else
            {
                request.requestchildpartmaster.AUTOID = AUTOID;
                response = bc.UpdateChildPartMasterBC(request);
            }

            ManageError Err = new ManageError();

            string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
            string result = resultjson.Remove(0, 2);
            resultjson = result;
            resultjson = response.result + "|" + resultjson;

            JsonResult json = Json(resultjson, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;

        }
        public ActionResult EditChildPartMaster(string ID)
        {

            RequestChildPartMaster request = new RequestChildPartMaster();
            ResponseChildPartMaster response = new ResponseChildPartMaster();
            request.requestchildpartmaster = new ChildPartMasterEntity();
            request.requestchildpartmaster.AUTOID = ID;
            ChildPartMasterBC bc = new ChildPartMasterBC();
            response = bc.EditChildPartMasterBC(request);
            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_ChildpartmasterDetails);        
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult ChildPartMastersUploads()
        {
            string results = "";

            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;

                    var postedFile = System.Web.HttpContext.Current.Request.Files["CsvDoc"];

                    string path = Path.GetFileName(postedFile.FileName);

                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        if (file.FileName == postedFile.FileName)
                        {
                            if (file != null && file.ContentLength > 0)
                            {
                                Stream stream = file.InputStream;
                                IExcelDataReader reader = null;
                                if (file.FileName.EndsWith(".xls"))
                                {
                                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                                }
                                else if (file.FileName.EndsWith(".xlsx"))
                                {
                                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                                }
                                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                    {
                                        UseHeaderRow = true
                                    }
                                });
                                DataTable dataTable = result.Tables[0];

                                reader.Close();

                                results = Utility.DataTableToJSONWithJavaScriptSerializer(dataTable);

                                if (dataTable.Rows.Count > 0)
                                {
                                    DataTable dt = new DataTable();
                                    string check = string.Empty;

                                    int rowcount = 0;
                                    int rowcount1 = 0;
                                    string employeecodes = "";
                                    try
                                    {
                                        using (TransactionScope scope = new TransactionScope())
                                        {
                                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CUMI"].ConnectionString);
                                            {
                                                using (DataSet ds = new DataSet())
                                                {
                                                    foreach (DataRow req in dataTable.Rows)
                                                    {
                                                        SqlCommand cmd = new SqlCommand("[MASTERS].[CHIDPARTMASTER_INSERT]", con);
                                                        cmd.CommandType = CommandType.StoredProcedure;
                                                        cmd.Parameters.AddWithValue("@PLANTCODE", string.IsNullOrEmpty(req["PLANTCODE"].ToString()) ? string.Empty : req["PLANTCODE"].ToString());
                                                        cmd.Parameters.AddWithValue("@LOCATION", string.IsNullOrEmpty(req["LOCATION"].ToString()) ? string.Empty : req["LOCATION"].ToString());
                                                        cmd.Parameters.AddWithValue("@CHILDITEMCODE", string.IsNullOrEmpty(req["CHILDITEMCODE"].ToString()) ? string.Empty : req["CHILDITEMCODE"].ToString());
                                                        cmd.Parameters.AddWithValue("@DESCRIPTION", string.IsNullOrEmpty(req["DESCRIPTION"].ToString()) ? string.Empty : req["DESCRIPTION"].ToString());
                                                        // cmd.Parameters.AddWithValue("@UOM", string.IsNullOrEmpty(req["UOM"].ToString()) ? string.Empty : req["UOM"].ToString());
                                                        if (req["UOM"].ToString().ToUpper() == "NOS")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@UOM", "MDSUB_002_0002"));
                                                        }
                                                        else if (req["UOM"].ToString().ToUpper() == "SET")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@UOM", "MDSUB_002_0003"));
                                                        }
                                                        else if (req["UOM"].ToString().ToUpper() == "LTR")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@UOM", "MDSUB_002_0004"));
                                                        }
                                                        else if (req["UOM"].ToString().ToUpper() == "KGS")
                                                        {
                                                            cmd.Parameters.Add(new SqlParameter("@UOM", "MDSUB_002_0005"));
                                                        }


                                                        cmd.Parameters.AddWithValue("@QUANTITY", string.IsNullOrEmpty(req["QUANTITY"].ToString()) ? string.Empty : req["QUANTITY"].ToString());
                                                        cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", "MDSUB_001_0001"));
                                                        //cmd.Parameters.AddWithValue("@REMARKS", string.IsNullOrEmpty(req["REMARKS"].ToString()) ? string.Empty : req["REMARKS"].ToString());
                                                        cmd.Parameters.Add(new SqlParameter("@USERCODE", Session["LoginEmployeeCode"].ToString()));
                                                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                                                        //DataSet ds2 = new DataSet();
                                                        da.Fill(ds);

                                                        rowcount++;
                                                    }

                                                    if (dataTable.Rows.Count == rowcount && ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                                                    {
                                                        results = "Successfully uploaded!!!";
                                                        scope.Complete();
                                                        results = "true";

                                                    }
                                                    else
                                                    {
                                                        results = "Already uploaded";
                                                    }
                                                }

                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                                        string responselog = "";
                                        results = "false";
                                    }
                                }
                                else
                                {
                                    results = "false" + "|" + "Uploaded Excel File is Empty. ";
                                }

                            }
                        }
                        else
                        {
                            results = "False" + "|" + "Uploaded InValid Excel File. ";
                        }

                    }

                }
                catch (Exception ex)
                {
                    results = "False" + "|" + "Error occurred. Error details: ";

                }
            }

            JsonResult json = Json(results, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        #endregion

        #region BOM Master.........................................................................

        public ActionResult BOMMaster()
        {
            return View();
        }
        public ActionResult PageloadBOMMaster()
        {
            listBOMMasterDetailsdet = new List<BOMMasterDetailsEntity>();
            RequestBOMMaster request = new RequestBOMMaster();
            ResponseBOMMaster response = new ResponseBOMMaster();
            BOMMasterBC bc = new BOMMasterBC();
            response = bc.PageloadBOMMasterBC();
            string resulttable = 
                                 Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_VARIANT)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_MOLDITEMCODE)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_BOMMASTERDETAILS)
                          +"|"+Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_DetailsVariant);
            var data = resulttable;
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult FetchItemCodeBOMMaster(string MOLDITEMNAME)
        {
            RequestBOMMaster request = new RequestBOMMaster();
            ResponseBOMMaster response = new ResponseBOMMaster();
            request.requestbommaster = new BOMMasterEntity();
            request.requestbommaster.MOLDITEMNAME = MOLDITEMNAME;
            BOMMasterBC bc = new BOMMasterBC();
            response = bc.FetchItemCodeBOMMasterBC(request);
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_MOLDITEMNAME);
                         
            var data = resulttable;
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult FetchItemNameBOMMaster(string MOLDITEMCODE)
        {
            RequestBOMMaster request = new RequestBOMMaster();
            ResponseBOMMaster response = new ResponseBOMMaster();
            request.requestbommaster = new BOMMasterEntity();
            request.requestbommaster.MOLDITEMCODE = MOLDITEMCODE;
            BOMMasterBC bc = new BOMMasterBC();
            response = bc.FetchItemNameBOMMasterBC(request);
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_MOLDITEMCODE);

            var data = resulttable;
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult FetchFGItemnameBOMMaster(string FGITEMCODE)
        {
            RequestBOMMaster request = new RequestBOMMaster();
            ResponseBOMMaster response = new ResponseBOMMaster();
            request.requestbommaster = new BOMMasterEntity();
            request.requestbommaster.FGITEMCODE = FGITEMCODE;
            BOMMasterBC bc = new BOMMasterBC();
            response = bc.FetchFGItemnameBOMMasterBC(request);
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_FGITEMNAME);

            var data = resulttable;
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public class BOMDetails
        {
            public string Mold_Item_Code { get; set; }
            public string Mold_Item_Name { get; set; }
            public string Variant { get; set; }
            public string Quantity { get; set; }
            public string Remove { get; set; }

        }
        static List<BOMMasterDetailsEntity> listBOMMasterDetailsdet = new List<BOMMasterDetailsEntity>();

        public ActionResult AddBOMmasterdetails()
        {

            string errormsg = "";
            string MOLDITEMCODE = "";
            string MOLDITEMNAME = "";
            string QUANTITY = "";
            string VARIANT = "";
            MOLDITEMCODE = Request.Form["MOLDITEMCODE"];
            MOLDITEMNAME = Request.Form["MOLDITEMNAME"];
            VARIANT = Request.Form["VARIANT"];
            QUANTITY  = Request.Form["QUANTITY"];
            List<BOMDetails> listitemdetInsert = new List<BOMDetails>();
            int duplicatecount = listBOMMasterDetailsdet.Where(inventorylist => inventorylist.MOLDITEMCODE == Request.Form["MOLDITEMCODE"].ToString()).Count();
            string data = "";
            string value = "Update";

            // if (value != Request.Form["action"].ToString())
            //   {
            if (duplicatecount == 0)
            {
                BOMMasterDetailsEntity listitemdet1 = new BOMMasterDetailsEntity();
                listitemdet1.MOLDITEMCODE = Request.Form["MOLDITEMCODE"];
                listitemdet1.MOLDITEMNAME = Request.Form["MOLDITEMNAME"];
                listitemdet1.VARIANT = Request.Form["VARIANT"];
                listitemdet1.QUANTITY = Request.Form["QUANTITY"];

                if (MOLDITEMCODE != "" && MOLDITEMNAME != "" && VARIANT !="" && QUANTITY != "")

                {
                    listBOMMasterDetailsdet.Add(listitemdet1);
                }

                listitemdetInsert = (from inv in listBOMMasterDetailsdet.AsEnumerable()
                                     select new BOMDetails
                                     {
                                         Mold_Item_Code = inv.MOLDITEMCODE,
                                         Mold_Item_Name = inv.MOLDITEMNAME,
                                         Variant = inv.VARIANT,
                                         Quantity = inv.QUANTITY,

                                     }).ToList();
                data = "true" + "|" + JsonConvert.SerializeObject(listitemdetInsert);

            }
            else
            {
                data = "false" + "|" + JsonConvert.SerializeObject("Mould Item Code Already Exist.");
            }
           
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteBOMmasterdts(string MOLDITEMCODE)
        {

            listBOMMasterDetailsdet.Remove(listBOMMasterDetailsdet.Single(r => r.MOLDITEMCODE == MOLDITEMCODE));

            List<BOMDetails> listitemdet = new List<BOMDetails>();

            listitemdet = (from inv in listBOMMasterDetailsdet.AsEnumerable()
                           select new BOMDetails
                           {
                               Mold_Item_Code = inv.MOLDITEMCODE,
                               Mold_Item_Name = inv.MOLDITEMNAME,
                               Quantity = inv.QUANTITY,
                           }).ToList();

            return Json(JsonConvert.SerializeObject(listitemdet), JsonRequestBehavior.AllowGet);

        }
        public ActionResult InsertBOMMMaster(string ASSEMBLYID,string actiontype, string AUTOID, string FGITEMCODE, string FGITEMNAME, string CHILDITEMCODE, string VARIANT, string MOLDITEMCODE, string UOM, string QUANTITY, string RECORDSTATUS, string RFIDNUMBER, string REMARKS)
        {
            RequestBOMMaster request = new RequestBOMMaster();
            ResponseBOMMaster response = new ResponseBOMMaster();
            request.requestbommaster = new BOMMasterEntity();
            request.requestbommasterdetails = listBOMMasterDetailsdet;
            request.requestbommaster.FGITEMCODE = FGITEMCODE;
            request.requestbommaster.FGITEMNAME = FGITEMNAME;
            request.requestbommaster.ASSEMBLYID = ASSEMBLYID;
            request.requestbommaster.VARIANT = VARIANT;
            request.requestbommaster.USERCODE = Session["LoginEmployeeCode"].ToString();
            BOMMasterBC bc = new BOMMasterBC();
            string value = "Update";
            if (value != Request.Form["actiontype"].ToString())
            //if (value != actiontype)
            {
                response = bc.InsertBOMMMasterBC(request);
            }
            else
            {
                request.requestbommaster.ASSEMBLYID = ASSEMBLYID;
                response = bc.UpdateBOMMasterBC(request);
            }

            ManageError Err = new ManageError();

            string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
            string result = resultjson.Remove(0, 2);
            resultjson = result;
            resultjson = response.result + "|" + resultjson;

            if (response.result == true)
            {
                TempData["InsertBOMMMaster"] = null;
                listBOMMasterDetailsdet = new List<BOMMasterDetailsEntity>();
                return Json(resultjson);
            }
            return Json(resultjson);

        }
        public ActionResult EditBOMMaster(string FGITEMCODE)
        {

            RequestBOMMaster request = new RequestBOMMaster();
            ResponseBOMMaster response = new ResponseBOMMaster();
            request.requestbommaster = new BOMMasterEntity();
            request.requestbommaster.FGITEMCODE = FGITEMCODE;
            request.requestbommaster.USERCODE = Session["LoginEmployeeCode"].ToString();
            BOMMasterBC bc = new BOMMasterBC();
            response = bc.EditBOMMasterBC(request);
            listBOMMasterDetailsdet = new List<BOMMasterDetailsEntity>();

            for (int i = 0; i < response.JS_BOMMASTERDETAILS.Rows.Count; i++)
            {
                BOMMasterDetailsEntity childdts = new BOMMasterDetailsEntity();
                childdts.MOLDITEMCODE = response.JS_BOMMASTERDETAILS.Rows[i]["Mold_Item_Code"].ToString();
                childdts.MOLDITEMNAME = response.JS_BOMMASTERDETAILS.Rows[i]["Mold_Item_Name"].ToString();
                childdts.VARIANT = response.JS_BOMMASTERDETAILS.Rows[i]["Variant"].ToString();
                childdts.QUANTITY = response.JS_BOMMASTERDETAILS.Rows[i]["Quantity"].ToString();
                listBOMMasterDetailsdet.Add(childdts);
            }

            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_BOMMASTERHEADER)
            + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_BOMMASTERDETAILS);
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult ViewBOMMaster(string FGITEMCODE)
        {
            RequestBOMMaster request = new RequestBOMMaster();
            ResponseBOMMaster response = new ResponseBOMMaster();
            request.requestbommaster = new BOMMasterEntity();
            request.requestbommaster.FGITEMCODE = FGITEMCODE;
            BOMMasterBC bc = new BOMMasterBC();
            response = bc.ViewBOMMasterBC(request);
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_BOMMASTERVIEW);

            var data = resulttable;
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        #endregion

        #region Supplier Master.........................................................................

        public ActionResult SupplierMaster()
        {
            return View();
        }
        public ActionResult PageloadSupplierMaster()
        {
            RequestSupplierMaster request = new RequestSupplierMaster();
            ResponseSupplierMaster response = new ResponseSupplierMaster();
            SupplierMasterBC bc = new SupplierMasterBC();
            response = bc.PageloadSupplierMasterBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.Js_Status)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.Js_SupplierDts);
            var data = resulttable;
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
         public ActionResult InsertSupplierMaster(string AUTOID,string SUPPLIERADDRESS, string actiontype,string SupplierCode,string SupplierName,string Address,string RecordStatus)
        {
            RequestSupplierMaster request = new RequestSupplierMaster();
            ResponseSupplierMaster response = new ResponseSupplierMaster();
            request.requestsuppliermaster = new SupplierMasterEntity();
            request.requestsuppliermaster.SUPPLIERCODE = SupplierCode;
            request.requestsuppliermaster.SUPPLIERNAME = SupplierName;
            request.requestsuppliermaster.ADDRESS= Address;
            request.requestsuppliermaster.RECORDSTATUS = RecordStatus;
            request.requestsuppliermaster.USERCODE = Session["LoginEmployeeCode"].ToString();
            SupplierMasterBC bc = new SupplierMasterBC();
            string value = "Update";
            if (value != actiontype)
            {
                response = bc.InsertSupplierMasterBC(request);
            }
            else
            {
                request.requestsuppliermaster.AUTOID = AUTOID;
                response = bc.UpdateSupplierMasterBC(request);
            }

            ManageError Err = new ManageError();

            string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
            //string result = resultjson.Replace("-","");
            string result = resultjson.Remove(0,2);
            resultjson = result;
            resultjson = response.result + "|" + resultjson;

            JsonResult json = Json(resultjson, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;

        }
        public ActionResult EditSupplierMaster(string ID)
        {

            RequestSupplierMaster request = new RequestSupplierMaster();
            ResponseSupplierMaster response = new ResponseSupplierMaster();
            request.requestsuppliermaster = new SupplierMasterEntity();
            request.requestsuppliermaster.AUTOID = ID;
            SupplierMasterBC bc = new SupplierMasterBC();
            response = bc.EditSupplierMasterBC(request);

            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.Js_SupplierDts);

            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult SupplierDownloads(string file)
        {
            string filename = Path.GetFileName(file);
            string fullPath = Path.Combine(Server.MapPath("~/Downloads"), filename);
            return File(fullPath, "application/octet-stream", filename);
        }
        public ActionResult SupplierMastersUploads()
        {
            string results = "";

            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;

                    var postedFile = System.Web.HttpContext.Current.Request.Files["CsvDoc"];

                    string path = Path.GetFileName(postedFile.FileName);

                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        if (file.FileName == postedFile.FileName)
                        {
                            if (file != null && file.ContentLength > 0)
                            {
                                Stream stream = file.InputStream;
                                IExcelDataReader reader = null;
                                if (file.FileName.EndsWith(".xls"))
                                {
                                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                                }
                                else if (file.FileName.EndsWith(".xlsx"))
                                {
                                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                                }
                                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                    {
                                        UseHeaderRow = true
                                    }
                                });
                                DataTable dataTable = result.Tables[0];

                                reader.Close();

                                results = Utility.DataTableToJSONWithJavaScriptSerializer(dataTable);

                                if (dataTable.Rows.Count > 0)
                                {
                                    DataTable dt = new DataTable();
                                    string check = string.Empty;

                                    int rowcount = 0;
                                    int rowcount1 = 0;

                                    string employeecodes = "";
                                    try
                                    {
                                        using (TransactionScope scope = new TransactionScope())
                                        {
                                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CUMI"].ConnectionString);
                                            {
                                                using (DataSet ds = new DataSet())
                                                {
                                                    foreach (DataRow req in dataTable.Rows)
                                                    {

                                                        SqlCommand cmd = new SqlCommand("[MASTERS].[SUPPLIERMASTER_INSERT]", con);
                                                        cmd.CommandType = CommandType.StoredProcedure;
                                                        cmd.Parameters.AddWithValue("@SUPPLIERCODE", string.IsNullOrEmpty(req["SUPPLIER CODE"].ToString()) ? string.Empty : req["SUPPLIER CODE"].ToString());
                                                        cmd.Parameters.AddWithValue("@SUPPLIERNAME", string.IsNullOrEmpty(req["SUPPLIER NAME"].ToString()) ? string.Empty : req["SUPPLIER NAME"].ToString());
                                                        cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", string.IsNullOrEmpty(req["RECORD STATUS"].ToString()) ? string.Empty : req["RECORD STATUS"].ToString()));
                                                        cmd.Parameters.AddWithValue("@ADDRESS", string.IsNullOrEmpty(req["ADDRESS"].ToString()) ? string.Empty : req["ADDRESS"].ToString());
                                                        cmd.Parameters.Add(new SqlParameter("@USERCODE", Session["LoginEmployeeCode"].ToString()));
                                                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                                                        //DataSet ds2 = new DataSet();
                                                        da.Fill(ds);
                                                        rowcount++;

                                                    }

                                                    if (dataTable.Rows.Count == rowcount && ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                                                    {
                                                        results = "Successfully uploaded!!!";
                                                        scope.Complete();
                                                        results = "true";

                                                    }
                                                    else
                                                    {
                                                        results = "Already uploaded";
                                                    }
                                                }

                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                                        string responselog = "";
                                        results = "false";

                                    }


                                }
                                else
                                {
                                    results = "false" + "|" + "Uploaded Excel File is Empty. ";
                                }

                            }
                        }
                        else
                        {
                            results = "False" + "|" + "Uploaded InValid Excel File. ";
                        }

                    }


                }

                catch (Exception ex)
                {

                    results = "False" + "|" + "Error occurred. Error details: ";

                }
            }

            JsonResult json = Json(results, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        #endregion


    }
}
