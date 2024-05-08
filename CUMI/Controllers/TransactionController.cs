using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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
using System.Globalization;
using ExcelDataReader;
using System.Transactions;
using System.Data.SqlClient;
using System.Web.UI;
using DocumentFormat.OpenXml.VariantTypes;


namespace CUMI.Controllers
{

    public class TransactionController : Controller
    {
        string connectionstring = ConfigurationManager.ConnectionStrings["CUMI"].ConnectionString;



        #region Mold RFID Assignment .............................................................................
        public ActionResult MoldRFIDAssignment()
        {
            return View();
        }

        public ActionResult GetMoldRFIDAssignmentPageLoad()
        {
            // listInterlinkingDetailsdet = new List<InterlinkingMasterDetailsEntity>();
            RequestMoldRFIDAssignment request = new RequestMoldRFIDAssignment();
            ResponseMoldRFIDAssignment response = new ResponseMoldRFIDAssignment();
            MoldRFIDAssignmentBC bc = new MoldRFIDAssignmentBC();
            response = bc.MoldRFIDAssignmentPageLoadBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Recordstatus)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_UOM)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_plantdetails)
                         // + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_FGdetails)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Molddetails)
                         // + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Childpartdetails)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Moldrfidassignmentdetails);
            var data = resulttable;
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }

        public ActionResult GetMoldRFIDAssignmentpartcode(string partcode)
        {

            RequestMoldRFIDAssignment request = new RequestMoldRFIDAssignment();
            ResponseMoldRFIDAssignment response = new ResponseMoldRFIDAssignment();
            request.requestMoldRFIDAssignment = new MoldRFIDAssignmentEntity();
            request.requestMoldRFIDAssignment.PLANTCODE = partcode;
            MoldRFIDAssignmentBC bc = new MoldRFIDAssignmentBC();
            response = bc.FetchLocationPartcodeBC(request);
            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Plants);
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }

        public ActionResult GetVarientcodefetch(string Moldcode)
        {

            RequestMoldRFIDAssignment request = new RequestMoldRFIDAssignment();
            ResponseMoldRFIDAssignment response = new ResponseMoldRFIDAssignment();
            request.requestMoldRFIDAssignment = new MoldRFIDAssignmentEntity();
            request.requestMoldRFIDAssignment.MOLDITEMCODE = Moldcode;
            MoldRFIDAssignmentBC bc = new MoldRFIDAssignmentBC();
            response = bc.FetchVarientCodeBC(request);
            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Plants);
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult InsertMoldRFIDAssignment(string MOLDITEMNAME,string UOM,string SUPPLIERID, string actiontype, string AUTOID, string MRCOUNT, string PLANTCODE, string LOCATION, string MOLDITEMCODE, string RECORDSTATUS, string VARIENTCODE, string MOLDOPENCOUNT, string PREVIOUSMRCOUNT, string PREVIUOSMRDATE, string MRALERTCOUNT, string MOLDLIFECOUNT, string RFIDNUMBER, string REMARKS, string BATCHNUMBER, string PONUMBER)
        {
            RequestMoldRFIDAssignment request = new RequestMoldRFIDAssignment();
            ResponseMoldRFIDAssignment response = new ResponseMoldRFIDAssignment();
            request.requestMoldRFIDAssignment = new MoldRFIDAssignmentEntity();
            //request.requestMoldRFIDAssignment = listInterlinkingDetailsdet;
            request.requestMoldRFIDAssignment.PLANTCODE = PLANTCODE;
            request.requestMoldRFIDAssignment.LOCATION = LOCATION;
            request.requestMoldRFIDAssignment.MOLDITEMCODE = MOLDITEMCODE;
            request.requestMoldRFIDAssignment.VARIENTCODE = VARIENTCODE;
            request.requestMoldRFIDAssignment.PONUMBER = PONUMBER;
            request.requestMoldRFIDAssignment.BATCHNUMBER = BATCHNUMBER;
            request.requestMoldRFIDAssignment.MOLDOPENCOUNT = MOLDOPENCOUNT;
            request.requestMoldRFIDAssignment.PREVIOUSMRCOUNT = PREVIOUSMRCOUNT;
            request.requestMoldRFIDAssignment.PREVIUOSMRDATE = PREVIUOSMRDATE;
            request.requestMoldRFIDAssignment.MRCOUNT = MRCOUNT;
            request.requestMoldRFIDAssignment.MRALERTCOUNT = MRALERTCOUNT;
            request.requestMoldRFIDAssignment.MOLDLIFECOUNT = MOLDLIFECOUNT;
            request.requestMoldRFIDAssignment.RFIDNUMBER = RFIDNUMBER;
            request.requestMoldRFIDAssignment.RECORDSTATUS = RECORDSTATUS;
            request.requestMoldRFIDAssignment.REMARKS = REMARKS;
            request.requestMoldRFIDAssignment.SUPPLIERID = SUPPLIERID;
            request.requestMoldRFIDAssignment.MOLDITEMNAME = MOLDITEMNAME;
            request.requestMoldRFIDAssignment.UOM = UOM;
            request.requestMoldRFIDAssignment.USERCODE = Session["LoginEmployeeCode"].ToString();
            MoldRFIDAssignmentBC bc = new MoldRFIDAssignmentBC();
            string value = "Update";
            if (value != Request.Form["actiontype"].ToString())
            //if (value != actiontype)
            {
                response = bc.InsertMoldRFIDAssignmentBC(request);
            }
            else
            {
                request.requestMoldRFIDAssignment.AUTOID = AUTOID;
                response = bc.UpdateMoldRFIDAssignmentBC(request);
            }
            ManageError Err = new ManageError();
            string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
            string result = resultjson.Remove(0, 2);
            resultjson = result;
            resultjson = response.result + "|" + resultjson;
            return Json(resultjson);
        }
        public ActionResult EditMoldRFIDAssignment(string AUTOID)
        {
            RequestMoldRFIDAssignment request = new RequestMoldRFIDAssignment();
            ResponseMoldRFIDAssignment response = new ResponseMoldRFIDAssignment();
            request.requestMoldRFIDAssignment = new MoldRFIDAssignmentEntity();
            request.requestMoldRFIDAssignment.AUTOID = AUTOID;
            //request.requestInterlinkingMaster.MOLDITEMCODE = MOLDITEMCODE;
            MoldRFIDAssignmentBC bc = new MoldRFIDAssignmentBC();
            response = bc.EditMoldRFIDAssignmentBC(request);
            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Moldrfidassignmentdetails);
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }

        #endregion

        #region Production Order Assign ............................................................................
        public ActionResult ProductionOrderAssign()
        {
            return View();
        }
        public ActionResult ProductionOrderAssignPageLoad()
        {
           // listBOMMasterDetailsdet = new List<ProductionOrderAssignDetailsEntity>();
            RequestProductionOrderAssign request = new RequestProductionOrderAssign();
            ResponseProductionOrderAssign response = new ResponseProductionOrderAssign();
            request.requestproductionordernoheaderdetails = new ProductionOrderAssignEntity();
            request.requestproductionordernoheaderdetails.USERCODE = Session["LoginEmployeeCode"].ToString();
            ProductionOrderAssignBC bc = new ProductionOrderAssignBC();
            response = bc.ProductionOrderAssignPageLoadBC(request);
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_warehousepicker)
                + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_location)
                + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Productiondetails)
                +"|"+Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_ProductionERPDts)
                +"|"+Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_AssemblyID);

            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult ProductionOrderAssigndts(string AssemblyID, string VariantCode, string Fgitemcode, string Productionqty)
        {
            RequestProductionOrderAssign request = new RequestProductionOrderAssign();
            ResponseProductionOrderAssign response = new ResponseProductionOrderAssign();
            request.requestproductionordernoheaderdetails = new ProductionOrderAssignEntity();
            request.requestProductionOrderAssigndetailsss = new List<ProductionOrderAssignEntity>();

            string PRODUCTIONORDERNO = "";
            PRODUCTIONORDERNO = Request.Form["PRODUCTIONORDERNO"];

            var prodts = PRODUCTIONORDERNO.Split('^');
            var assemid = AssemblyID.Split('^');
            var fgcode = Fgitemcode.Split('^');
            var vrtcode = VariantCode.Split('^');
            var prdqty = Productionqty.Split('^');

            for (int i = 0; i < prodts.Length; i++)
            {
                ProductionOrderAssignEntity rowData = new ProductionOrderAssignEntity
                {
                    ASSEMBLYID = assemid[i],
                    FGITEMCODE = fgcode[i],
                    VARIANT = vrtcode[i],
                    PRODUCTIONQTY = prdqty[i],
                    USERCODE = Session["LoginEmployeeCode"].ToString()
                };
                request.requestProductionOrderAssigndetailsss.Add(rowData);
            }

            ProductionOrderAssignBC bc = new ProductionOrderAssignBC();
            response = bc.ProductionOrderAssigndtsBC(request);
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_ProductionDtsAssign);
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //public class Productionorderdts
        //{
        //    public string ProductionOrderNo { get; set; }
        //    public string ProductionDate { get; set; }
        //    public string FGItemcode { get; set; }
        //    public string ProductionQty { get; set; }
        //    public string AssemblyId { get; set; }
        //    public string AssemblyQty { get; set; }
        //}
        //static List<ProductionOrderAssignDetailsEntity> listBOMMasterDetailsdet = new List<ProductionOrderAssignDetailsEntity>();

        //public ActionResult AddAssemblydetails()
        //{
        //    string errormsg = "";
        //    string PRODUCTIONORDERNO = "", PRODUCTIONORDERDATE = "", FGITEMCODE = "", PRODUCTIONQTY = "", ASSEMBLYID = "", ASSEMBLYQTY = "";

        //    PRODUCTIONORDERNO = Request.Form["PRODUCTIONORDERNO"];
        //    PRODUCTIONORDERDATE = Request.Form["PRODUCTIONORDERDATE"];
        //    FGITEMCODE = Request.Form["FGITEMCODE"];
        //    PRODUCTIONQTY = Request.Form["PRODUCTIONQTY"];
        //    ASSEMBLYID = Request.Form["ASSEMBLYID"];
        //    ASSEMBLYQTY = Request.Form["ASSEMBLYQTY"];

        //    List<Productionorderdts> listitemdetInsert = new List<Productionorderdts>();
        //    int duplicatecount = listBOMMasterDetailsdet.Where(inventorylist => inventorylist.PRODUCTIONORDERNO == Request.Form["PRODUCTIONORDERNO"].ToString()).Count();
        //    string data = "";
        //    string value = "Update";

        //    if (duplicatecount == 0)
        //    {
        //        ProductionOrderAssignDetailsEntity listitemdet1 = new ProductionOrderAssignDetailsEntity();
        //        listitemdet1.PRODUCTIONORDERNO = Request.Form["PRODUCTIONORDERNO"];
        //        listitemdet1.PRODUCTIONORDERDATE = Request.Form["PRODUCTIONORDERDATE"];
        //        listitemdet1.FGITEMCODE = Request.Form["FGITEMCODE"];
        //        listitemdet1.PRODUCTIONQTY = Request.Form["PRODUCTIONQTY"];
        //        listitemdet1.ASSEMBLYID = Request.Form["ASSEMBLYID"];
        //        listitemdet1.ASSEMBLYQTY = Request.Form["ASSEMBLYQTY"];

        //        if (PRODUCTIONORDERNO != "" && PRODUCTIONORDERDATE != "" && FGITEMCODE != "" && PRODUCTIONQTY != "" && ASSEMBLYID != "" && ASSEMBLYQTY != "")
        //        {
        //            listBOMMasterDetailsdet.Add(listitemdet1);
        //        }

        //        // Splitting the values if they contain delimiters
        //        var productionOrderNos = PRODUCTIONORDERNO.Split('^');
        //        var productionDates = PRODUCTIONORDERDATE.Split('^');
        //        var fgItemCodes = FGITEMCODE.Split('^');
        //        var productionQtys = PRODUCTIONQTY.Split('^');
        //        var assemblyIds = ASSEMBLYID.Split('^');
        //        var assemblyQtys = ASSEMBLYQTY.Split('^');

        //        for (int i = 0; i < productionOrderNos.Length; i++)
        //        {
        //            Productionorderdts rowData = new Productionorderdts
        //            {
        //                ProductionOrderNo = productionOrderNos[i],
        //                ProductionDate = productionDates[i],
        //                FGItemcode = fgItemCodes[i],
        //                ProductionQty = productionQtys[i],
        //                AssemblyId = assemblyIds[i],
        //                AssemblyQty = assemblyQtys[i]
        //            };
        //            listitemdetInsert.Add(rowData);
        //        }

        //        data = "true" + "|" + JsonConvert.SerializeObject(listitemdetInsert);
        //    }
        //    else
        //    {
        //        data = "false" + "|" + JsonConvert.SerializeObject("PO Already Exist.");
        //    }

        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult ProductionOrderAssignInsert(string FGitemcodedts,string Assemblyid, string Moldvariant,string Totalassemblyqty,string FGVariant,string PODATE,string ProductionQty,string autoId, string PONO, string Warehousepicker, string FGItemCode, string Date, string MoldItemcode, string MoldItemname, string Quantity, string ChildItemCode,string Location, string actiontype)
        {
            RequestProductionOrderAssign request = new RequestProductionOrderAssign();
            ResponseProductionOrderAssign response = new ResponseProductionOrderAssign();
            ProductionOrderAssignBC bc = new ProductionOrderAssignBC();
            request.requestproductionordernoheaderdetails = new ProductionOrderAssignEntity();
            request.requestProductionOrderAssigndetails = new List<ProductionOrderAssignDetailsEntity>();
            request.requestProductionOrderAssigndetailsss = new List<ProductionOrderAssignEntity>();
            request.requestproductionordernoheaderdetails.WAREHOUSEPICKER = Warehousepicker;
            request.requestproductionordernoheaderdetails.USERCODE = Session["LoginEmployeeCode"].ToString();

            var Productionnoarray = PONO.Split('^');
            var ProductionnoDatearray = PODATE.Split('^');
            var FGItemcodearray = FGItemCode.Split('^');
            var Fgvariantarray = FGVariant.Split('^');
            var Productionqtyarry = ProductionQty.Split('^');
            var Assemblyidarray = Assemblyid.Split('^');

            var FGitemcodedtsarray = FGitemcodedts.Split('^');
            var Molditemcodearray = MoldItemcode.Split('^');
            var Molditemnamearray = MoldItemname.Split('^');
            var Moldvariantarray = Moldvariant.Split('^');
            var Totalassemblyqtyarray = Totalassemblyqty.Split('^');

            for (int i = 0; i < FGItemcodearray.Length; i++)
            {
                ProductionOrderAssignDetailsEntity det = new ProductionOrderAssignDetailsEntity();
                det.PRODUCTIONORDERNO = Productionnoarray[i];
                det.PRODUCTIONORDERDATE = ProductionnoDatearray[i];
                det.FGITEMCODE = FGItemcodearray[i];
                det.FGVARIANT = Fgvariantarray[i];
                det.PRODUCTIONQTY = Productionqtyarry[i];
                det.ASSEMBLYID = Assemblyidarray[i];

                request.requestProductionOrderAssigndetails.Add(det);
            }
            for (int i = 0; i < Molditemcodearray.Length; i++)
            {
                ProductionOrderAssignEntity det = new ProductionOrderAssignEntity();

                det.FGITEMCODE = FGitemcodedtsarray[i];
                det.MOLDITEMCODE = Molditemcodearray[i];
                det.MOLDITEMNAME = Molditemnamearray[i];
                det.MOLDVARIANT = Moldvariantarray[i];
                det.ASSEMBLYQTY = Totalassemblyqtyarray[i];
                request.requestProductionOrderAssigndetailsss.Add(det);
            }

           response = bc.InsertProductionorderassignBC(request);
           
            ManageError Err = new ManageError();

            string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
            string result = resultjson.Remove(0, 2);
            resultjson = result;
            resultjson = response.result + "|" + resultjson;
            if (response.result == true)
            {
                TempData["InventoryInsert"] = null;
                // listGeneralGRNNEWDetails = new List<GRNEntryDetailsEntity>();
                return Json(resultjson);
            }
            return Json(resultjson);
        }
        public ActionResult GetProductionordernumberdetails(string PRODUCTIONORDER)
        {

            RequestProductionOrderAssign request = new RequestProductionOrderAssign();
            ResponseProductionOrderAssign response = new ResponseProductionOrderAssign();
            request.requestproductionordernoheaderdetails = new ProductionOrderAssignEntity();
            request.requestproductionordernoheaderdetails.PRODUCTIONORDERNO = PRODUCTIONORDER;
            ProductionOrderAssignBC bc = new ProductionOrderAssignBC();
            response = bc.FetchProductionorderassignBC(request);
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Productiondetails);
            var data = resulttable;
            ManageError Err = new ManageError();
            string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
            resultjson = response.result + "|" + resultjson + "|" + data;

            JsonResult json = Json(resultjson, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }


        public ActionResult ProductionorderdetialsView(string PRODUCTIONORDER)
        {
            RequestProductionOrderAssign request = new RequestProductionOrderAssign();
            ResponseProductionOrderAssign response = new ResponseProductionOrderAssign();
            request.requestproductionordernoheaderdetails = new ProductionOrderAssignEntity();
            request.requestproductionordernoheaderdetails.PRODUCTIONORDERNO = PRODUCTIONORDER;
            ProductionOrderAssignBC bc = new ProductionOrderAssignBC();
            response = bc.ViewProductionorderBC(request);

            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Productiondetails);

            var data = resulttable;
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;

        }


        #endregion

        #region RDC PickList Assign ............................................................................
        public ActionResult RDCPicklistAssign()
        {
            return View();
        }
        public ActionResult RDCPicklistAssignPageLoad()
        {
            RequestRDCPicklistAssign request = new RequestRDCPicklistAssign();
            ResponseRDCPicklistAssign response = new ResponseRDCPicklistAssign();
            request.requestrdcheaderdetails = new RDCPicklistAssignEntity();
            request.requestrdcheaderdetails.USERCODE = Session["LoginEmployeeCode"].ToString();
            RDCPicklistAssignBC bc = new RDCPicklistAssignBC();
            response = bc.RDCPicklistAssignPageLoadBC(request);
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_warehousepicker)
                + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_location)
                + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Supplier)
                + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_RDCdetails)
                +"|"+Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_ERPdts);

            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult RDCPicklistAssignInsert(string RFIDNo,string Remarks,string autoId, string RDCNO, string Warehousepicker, string Location, string Date,string supplier,string Itemcode, string Itemname, string Quantity, string Uom, string actiontype)
        {
            RequestRDCPicklistAssign request = new RequestRDCPicklistAssign();
            ResponseRDCPicklistAssign response = new ResponseRDCPicklistAssign();
            RDCPicklistAssignBC bc = new RDCPicklistAssignBC();
            request.requestrdcheaderdetails = new RDCPicklistAssignEntity();
            request.requestrdcassigndetails = new List<RDCPicklistAssignDetailsEntity>();
            request.requestrdcheaderdetails.WAREHOUSEPICKER = Warehousepicker;
            request.requestrdcheaderdetails.USERCODE = Session["LoginEmployeeCode"].ToString();

            var RDCNoarray = RDCNO.Split('^');
            var DateArray = Date.Split('^');
            var ITEMCODEarray = Itemcode.Split('^');
            var ITEMNAMEarray = Itemname.Split('^');
            var supplierarray = supplier.Split('^');
            var remarksarray = Remarks.Split('^');
            var QUANTITYarray = Quantity.Split('^');
            var RFidNoarray = RFIDNo.Split('^');

            for (int i = 0; i < ITEMCODEarray.Length; i++)
            {
                RDCPicklistAssignDetailsEntity det = new RDCPicklistAssignDetailsEntity();
                det.RDCNO = RDCNoarray[i];
                det.DATE = DateArray[i];
                det.ITEMCODE = ITEMCODEarray[i];
                det.ITEMNAME = ITEMNAMEarray[i];
                det.SUPPLIER = supplierarray[i];
                det.REMARKS = remarksarray[i];
                det.QUANTITY = QUANTITYarray[i];
                det.RFIDNO = RFidNoarray[i];    
                request.requestrdcassigndetails.Add(det);
            }

            string value = "Update";

            if (value != actiontype)
            {
                response = bc.InsertRDCNOassignBC(request);
            }
            else
            {
                //response = bc.GRNEntryUpdateBC(request);
            }


            ManageError Err = new ManageError();

            string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
            string result = resultjson.Remove(0, 2);
            resultjson = result;
            resultjson = response.result + "|" + resultjson;
            if (response.result == true)
            {
                TempData["InventoryInsert"] = null;
                // listGeneralGRNNEWDetails = new List<GRNEntryDetailsEntity>();
                return Json(resultjson);
            }
            return Json(resultjson);
        }
        public ActionResult GetRDCNumberpicklistassigndetails(string RDCNO)
        {

            RequestRDCPicklistAssign request = new RequestRDCPicklistAssign();
            ResponseRDCPicklistAssign response = new ResponseRDCPicklistAssign();
            request.requestrdcheaderdetails = new RDCPicklistAssignEntity();
            request.requestrdcheaderdetails.RDCNO = RDCNO;
            RDCPicklistAssignBC bc = new RDCPicklistAssignBC();
            response = bc.FetchRDCNoassignBC(request);
            var resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_RDCdetails);
            var data = resulttable;
            ManageError Err = new ManageError();
            string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
            resultjson = response.result + "|" + resultjson + "|" + data;
            JsonResult json = Json(resultjson, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }


        public ActionResult RDCPicklistassigndetialsView(string RDCNO)
        {
            RequestRDCPicklistAssign request = new RequestRDCPicklistAssign();
            ResponseRDCPicklistAssign response = new ResponseRDCPicklistAssign();
            request.requestrdcheaderdetails = new RDCPicklistAssignEntity();
            request.requestrdcheaderdetails.RDCNO = RDCNO;
            RDCPicklistAssignBC bc = new RDCPicklistAssignBC();
            response = bc.ViewRDCNoBC(request);

            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_RDCdetails);

            var data = resulttable;
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;

        }


        #endregion

        #region GRN Entry .............................................................................
        public ActionResult GRNEntry()
        {
            return View();
        }
        public ActionResult GRNEntryPageload()
        {
            listPurchaseOrder = new List<GRNEntryDetailsEntity>();
            RequestGRNEntry request = new RequestGRNEntry();
            ResponseGRNEntry response = new ResponseGRNEntry();
            request.requestgrnentry = new GRNEntryEntity();
            request.requestgrnentry.USERCODE = Session["LoginEmployeeCode"].ToString();
            GRNEntryBC bc = new GRNEntryBC();
            response = bc.GRNEntryPageloadBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_RMItemcode)
                + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_GRNpageload);
             
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GRNEntryItemDetailsFetch(string ItemCode)
        {
            RequestGRNEntry request = new RequestGRNEntry();
            ResponseGRNEntry response = new ResponseGRNEntry();
            request.requestgrnentry = new GRNEntryEntity();
            request.requestgrnentry.ITEMCODE = ItemCode;
            GRNEntryBC bc = new GRNEntryBC();
            response = bc.GRNEntryItemDetailsFetchBC(request);
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_ItemDetails);
              
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public class GRNentrydetails
        {

            public string RMItemCode { get; set; }
            public string RMItemName { get; set; }
            public string Variant { get; set; }
            public string UOM { get; set; }
            public string Quantity { get; set; }
            public string Remove { get; set; }

        }

        static List<GRNEntryDetailsEntity> listPurchaseOrder = new List<GRNEntryDetailsEntity>();
        public ActionResult AddGRNEntryDetails()
        {
            List<GRNentrydetails> listPurchaseOrderdet = new List<GRNentrydetails>();

            if (Request.Form["action"].ToString() == "Update")
            {
                for (int i = listPurchaseOrder.Count - 1; i >= 0; i--)
                {
                    if (listPurchaseOrder[i].RMITEMCODE == Request.Form["RMITEMCODE"])
                    {
                        listPurchaseOrder.RemoveAt(i);
                    }
                }

            }
            //}

            int duplicatecount = listPurchaseOrder.Where(inventorylist => inventorylist.RMITEMCODE == Request.Form["RMITEMCODE"].ToString()).Count();


            string data = "";
            if (duplicatecount == 0)
            {
                GRNEntryDetailsEntity invdet = new GRNEntryDetailsEntity();

                invdet.RMITEMCODE = Request.Form["RMITEMCODE"];
                invdet.RMITEMNAME = Request.Form["RMITEMNAME"];
                invdet.VARIANT = Request.Form["VARIANT"];
                invdet.UOM = Request.Form["UOM"];
                invdet.QUANTITY = Request.Form["QUANTITY"];
                listPurchaseOrder.Add(invdet);


                listPurchaseOrderdet = (from inv in listPurchaseOrder.AsEnumerable()
                                        select new GRNentrydetails
                                        {

                                            RMItemCode = inv.RMITEMCODE,
                                            RMItemName = inv.RMITEMNAME,
                                            Variant = inv.VARIANT,
                                            UOM = inv.UOM,
                                            Quantity = inv.QUANTITY,
                                        }).ToList();
                data = "true" + "|" + JsonConvert.SerializeObject(listPurchaseOrderdet);

            }
            else
            {
                data = "false" + "|" + JsonConvert.SerializeObject("RM Item Code " + Request.Form["RMITEMCODE"].ToString() + " Already Exist.");
            }
            //TempData["InventoryInsert"] = listInventorydet;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
      
        public ActionResult DeleteGRNentryRow(string Code,string Qty,string Uom)
        {



            List<GRNentrydetails> listPurchaseOrderdet = new List<GRNentrydetails>();

            for (int i = listPurchaseOrder.Count - 1; i >= 0; i--)
            {
                if (listPurchaseOrder[i].RMITEMCODE == Code)
                {
                    listPurchaseOrder.RemoveAt(i);
                }
            }


            List<GRNentrydetails> listPOdet = new List<GRNentrydetails>();

            listPOdet = (from inv in listPurchaseOrder.AsEnumerable()
                         select new GRNentrydetails
                         {
                             RMItemCode = inv.RMITEMCODE,
                             RMItemName = inv.RMITEMNAME,
                             Variant = inv.VARIANT,
                             UOM = inv.UOM,
                             Quantity = inv.QUANTITY,
                         }).ToList();

            return Json(JsonConvert.SerializeObject(listPOdet), JsonRequestBehavior.AllowGet);

        }
        public ActionResult GRNEntryInsert()
        {

            RequestGRNEntry request = new RequestGRNEntry();
            ResponseGRNEntry response = new ResponseGRNEntry();
            GRNEntryBC bc = new GRNEntryBC();
            request.requestgrnentry = new GRNEntryEntity();
            request.requestgrnentrydetails = listPurchaseOrder;
            request.requestgrnentry.GRNNO = Request.Form["GRNNO"];
            request.requestgrnentry.GRNDATE = Request.Form["GRNDATE"];
            request.requestgrnentry.SUPPLIER = Request.Form["SUPPLIER"];
            request.requestgrnentry.PONO = Request.Form["PONO"];
            request.requestgrnentry.INVOICENO = Request.Form["INVOICENO"];
            request.requestgrnentry.USERCODE = Session["LoginEmployeeCode"].ToString();
            

            string value = "Update";

            if (value != Request.Form["actiontype"].ToString())
            {
                response = bc.GRNEntryInsertBC(request);
            }
            else
            {
                response = bc.GRNUpdateBC(request);
            }


            ManageError Err = new ManageError();

            string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
            string result = resultjson.Remove(0, 2);
            resultjson = result;
            resultjson = response.result + "|" + resultjson;
            if (response.result == true)
            {
                TempData["GRNEntryInsert"] = null;
                // listInventorydet = new List<InventoryDetailEntity>();
                return Json(resultjson);
            }
            return Json(resultjson);
        }
        public ActionResult GRNEntryView(string GRNNO)
        {
            RequestGRNEntry request = new RequestGRNEntry();
            ResponseGRNEntry response = new ResponseGRNEntry();
            request.requestgrnentry = new GRNEntryEntity();
            request.requestgrnentry.GRNNO = GRNNO;
            GRNEntryBC bc = new GRNEntryBC();
            response = bc.GRNEntryViewBC(request);
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_GRNEnrtyDetails);

            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GRNEntryEdit(string GRNNO)
        {
            RequestGRNEntry request = new RequestGRNEntry();
            ResponseGRNEntry response = new ResponseGRNEntry();
            request.requestgrnentry = new GRNEntryEntity();
            request.requestgrnentry.GRNNO = GRNNO;
            GRNEntryBC bc = new GRNEntryBC();
            response = bc.GRNEntryEditBC(request);

            for (int i = 0; i < response.JS_ItemDetails.Rows.Count; i++)
            {
                GRNEntryDetailsEntity GRNEdit = new GRNEntryDetailsEntity();

                GRNEdit.RMITEMCODE = response.JS_ItemDetails.Rows[i]["RMItemCode"].ToString();
                GRNEdit.RMITEMNAME = response.JS_ItemDetails.Rows[i]["RMItemName"].ToString();
                GRNEdit.VARIANT = response.JS_ItemDetails.Rows[i]["Variant"].ToString();
                GRNEdit.UOM = response.JS_ItemDetails.Rows[i]["UOM"].ToString();
                GRNEdit.QUANTITY = response.JS_ItemDetails.Rows[i]["Quantity"].ToString();
                listPurchaseOrder.Add(GRNEdit);
            }
           // TempData["GRNINSERT"] = listGRNItemdet;

            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_GRNEnrtyDetails) + "|" +
                       Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_ItemDetails);

            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;

        }

        #endregion

        #region RM Production Order ............................................................................
        public ActionResult RMProductionOrder()
        {
            return View();
        }
        public ActionResult RMProductionOrderPageload()
        {
            
            RequestRMProductionOrder request = new RequestRMProductionOrder();
            ResponseRMProductionOrder response = new ResponseRMProductionOrder();
            request.requestrmproductionorder = new RMProductionOrderEntity();
            request.requestrmproductionorder.USERCODE = Session["LoginEmployeeCode"].ToString();
            RMProductionOrderBC bc = new RMProductionOrderBC();
            response = bc.RMProductionOrderPageloadBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_ProductioNo)+"|"+
                Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_RMProductionPageload);

            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult RMProductionOrderFetchDtsByProductionno(string ProductionNo)
        {

            RequestRMProductionOrder request = new RequestRMProductionOrder();
            ResponseRMProductionOrder response = new ResponseRMProductionOrder();
            request.requestrmproductionorder = new RMProductionOrderEntity();
            request.requestrmproductionorder.PRODUCTIONORDERNO = ProductionNo;
            request.requestrmproductionorder.USERCODE = Session["LoginEmployeeCode"].ToString();
            RMProductionOrderBC bc = new RMProductionOrderBC();
            response = bc.RMProductionOrderFetchDtsByProductionnoBC(request);
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_ProductioNo);

            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult RMProductionOrderBView(string ProductionNo)
        {

            RequestRMProductionOrder request = new RequestRMProductionOrder();
            ResponseRMProductionOrder response = new ResponseRMProductionOrder();
            request.requestrmproductionorder = new RMProductionOrderEntity();
            request.requestrmproductionorder.PRODUCTIONORDERNO = ProductionNo;
            request.requestrmproductionorder.USERCODE = Session["LoginEmployeeCode"].ToString();
            RMProductionOrderBC bc = new RMProductionOrderBC();
            response = bc.RMProductionOrderBViewBC(request);
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_RMProductionOrderView);

            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult RMProductionOrderInsert(string Uom,string Quantity,string ProductionQty, string autoId, string PONO, string Warehousepicker, string FGItemCode, string Date, string Itemcode, string Itemname,  string ChildItemCode, string Location, string actiontype)
        {
            RequestRMProductionOrder request = new RequestRMProductionOrder();
            ResponseRMProductionOrder response = new ResponseRMProductionOrder();
          
            request.requestrmproductionorder = new RMProductionOrderEntity();
            request.requestrmproductionorderdts = new List<RMProductionOrderDetailsEntity>();
            request.requestrmproductionorder.PRODUCTIONORDERNO = PONO;
            request.requestrmproductionorder.RECEIVEDDATE = Date;
            request.requestrmproductionorder.WAREHOUSEPICKER = Warehousepicker;
            request.requestrmproductionorder.USERCODE = Session["LoginEmployeeCode"].ToString();

            var ITEMCODEarray = Itemcode.Split('^');
            var ITEMNAMEarray = Itemname.Split('^');
            var Uomarray = Uom.Split('^');
            var Quantityarray = Quantity.Split('^');

            for (int i = 0; i < ITEMCODEarray.Length; i++)
            {
                RMProductionOrderDetailsEntity det = new RMProductionOrderDetailsEntity();
                det.ITEMCODE = ITEMCODEarray[i];
                det.ITEMNAME = ITEMNAMEarray[i];
                det.UOM = Uomarray[i];
                det.QUANTITY = Quantityarray[i];
                request.requestrmproductionorderdts.Add(det);
            }

            string value = "Update";

            if (value != actiontype)
            {
                RMProductionOrderBC bc = new RMProductionOrderBC();
                response = bc.RMProductionOrderInsertBC(request);
            }
            else
            {
                //response = bc.GRNEntryUpdateBC(request);
            }


            ManageError Err = new ManageError();

            string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
            string result = resultjson.Remove(0, 2);
            resultjson = result;
            resultjson = response.result + "|" + resultjson;
            if (response.result == true)
            {
                TempData["InventoryInsert"] = null;
                // listGeneralGRNNEWDetails = new List<GRNEntryDetailsEntity>();
                return Json(resultjson);
            }
            return Json(resultjson);
        }
        #endregion

        #region Mold Receipt-RDC ............................................................................
        public ActionResult MoldReceiptRDC()
        {
            return View();
        }
        public ActionResult MoldReceipt_RDCPageload()
        {

            RequestMoldReceipt_RDC request = new RequestMoldReceipt_RDC();
            ResponseMoldReceipt_RDC response = new ResponseMoldReceipt_RDC();
            request.requestmoldrecieptrdc = new MoldReceipt_RDCEntity();
            request.requestmoldrecieptrdc.USERCODE = Session["LoginEmployeeCode"].ToString();
            MoldReceipt_RDCBC bc = new MoldReceipt_RDCBC();
            response = bc.MoldReceipt_RDCPageloadBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_RDCNo)+"|"+
                Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_MoldReceiptPageload);

            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult MoldReceipt_RDCFetchDetailsByRDCNO(string RDCNO)
        {

            RequestMoldReceipt_RDC request = new RequestMoldReceipt_RDC();
            ResponseMoldReceipt_RDC response = new ResponseMoldReceipt_RDC();
            request.requestmoldrecieptrdc = new MoldReceipt_RDCEntity();
            request.requestmoldrecieptrdc.RDCNO = RDCNO;
            request.requestmoldrecieptrdc.USERCODE = Session["LoginEmployeeCode"].ToString();
            MoldReceipt_RDCBC bc = new MoldReceipt_RDCBC();
            response = bc.MoldReceipt_RDCFetchDetailsByRDCNOBC(request);
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_MoldReceiptDetails);

            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult MoldReceiptRDCInsert(string RDCNO,string RFIDNO,string ITEMCODE,string ITEMNAME,string UOM,string QUANTITY,string SUPPLIER)
        {

            RequestMoldReceipt_RDC request = new RequestMoldReceipt_RDC();
            ResponseMoldReceipt_RDC response = new ResponseMoldReceipt_RDC();
            request.requestmoldrecieptrdc = new MoldReceipt_RDCEntity();
            request.requestmoldreceiptrdcdetails = new List<MoldReceipt_RDCDetailsEntity>();
            request.requestmoldrecieptrdc.RDCNO = RDCNO;
           
            request.requestmoldrecieptrdc.USERCODE = Session["LoginEmployeeCode"].ToString();



            string[] RDCNo = RDCNO.Split('^');
            string[] RFIDNo = RFIDNO.Split('^');
            string[] itemcode = ITEMCODE.Split('^');
            string[] itemname = ITEMNAME.Split('^');
           // string[] uomarr = UOM.Split('^');
            string[] quantityarr = QUANTITY.Split('^');
            string[] Supplierarr = SUPPLIER.Split('^');
            for (int i = 0; i < itemcode.Length; i++)
            {

                MoldReceipt_RDCDetailsEntity det = new MoldReceipt_RDCDetailsEntity();
                det.RDCNO = RDCNo.ToString();
                det.RFIDNO = RFIDNo[i];
                det.ITEMCODE = itemcode[i];
                det.ITEMNAME = itemname[i];
               // det.UOM = uomarr[i];
                det.QUANTITY = quantityarr[i];
                det.SUPPLIER = Supplierarr[i];
                request.requestmoldreceiptrdcdetails.Add(det);

            }

            MoldReceipt_RDCBC bc = new MoldReceipt_RDCBC();
            string value = "Update";

            response = bc.MoldReceiptRDCInsertBC(request);

            ManageError Err = new ManageError();

            string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
            string result = resultjson.Remove(0, 2);
            resultjson = result;
            resultjson = response.result + "|" + resultjson;
            if (response.result == true)
            {
                TempData["MoldReceiptRDCInsert"] = null;
               // listMaterialRequisitiondet = new List<MaterialRequisitionDetailsEntity>();
                return Json(resultjson);
            }
            return Json(resultjson);
        }
        #endregion

        #region Mold Shot Count .............................................................................
        public ActionResult MoldShotCount()
        {
            return View();
        }
        public ActionResult GetMoldShotCountPageLoad()
        {
            RequestMoldShotCount request = new RequestMoldShotCount();
            ResponseMoldShotCount response = new ResponseMoldShotCount();
            MoldShotCountBC bc = new MoldShotCountBC();
            response = bc.MoldshotcountPageLoadBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Molddetails)
                         + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_shotdetails);
            var data = resulttable;
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }

        public ActionResult GetMoldShotCountcodedetails(string moldcode)
        {

            RequestMoldShotCount request = new RequestMoldShotCount();
            ResponseMoldShotCount response = new ResponseMoldShotCount();
            request.requestmoldshotcount = new MoldShotCountEntity();
            request.requestmoldshotcount.MOLDITEMCODE = moldcode;
            MoldShotCountBC bc = new MoldShotCountBC();
            response = bc.FetchMoldshotcountDetailsBC(request);
            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Molddetails);
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }

        public ActionResult GetMoldShotCountcodeRFIDdetails(string moldcode)
        {

            RequestMoldShotCount request = new RequestMoldShotCount();
            ResponseMoldShotCount response = new ResponseMoldShotCount();
            request.requestmoldshotcount = new MoldShotCountEntity();
            request.requestmoldshotcount.MOLDITEMCODE = moldcode;
            MoldShotCountBC bc = new MoldShotCountBC();
            response = bc.FetchMoldshotcountRFIDDetailsBC(request);
            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Molddetails);
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult InsertMoldShotCount(string actiontype, string AUTOID, string RFIDNUMBER, string MOLDITEMCODE, string MOLDNAME, string FGITEMCODE, string SHOTCOUNT, string DATEOFSHOT)
        {
            RequestMoldShotCount request = new RequestMoldShotCount();
            ResponseMoldShotCount response = new ResponseMoldShotCount();
            request.requestmoldshotcount = new MoldShotCountEntity();
            request.requestmoldshotcount.MOLDITEMCODE = MOLDITEMCODE;
            request.requestmoldshotcount.RFIDNUMBER = RFIDNUMBER;
            request.requestmoldshotcount.MOLDNAME = MOLDNAME;
            request.requestmoldshotcount.FGITEMCODE = FGITEMCODE;
            request.requestmoldshotcount.SHOTCOUNT = SHOTCOUNT;
            request.requestmoldshotcount.DATEOFSHOT = DATEOFSHOT;
            request.requestmoldshotcount.USERCODE = Session["LoginEmployeeCode"].ToString();
            MoldShotCountBC bc = new MoldShotCountBC();
            string value = "Update";
            if (value != actiontype)
            {
                response = bc.InsertMoldshotcountBC(request);
            }
            else
            {
                request.requestmoldshotcount.AUTOID = AUTOID;
                response = bc.UpdateMoldshotcountBC(request);
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
        public ActionResult EditMoldShotCount(string ID)
        {
            RequestMoldShotCount request = new RequestMoldShotCount();
            ResponseMoldShotCount response = new ResponseMoldShotCount();
            request.requestmoldshotcount = new MoldShotCountEntity();
            request.requestmoldshotcount.AUTOID = ID;
            MoldShotCountBC bc = new MoldShotCountBC();
            response = bc.EditMoldshotcountBC(request);

            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_shotdetails);
            ;
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        #endregion

        #region Mold Issue Confirmation ............................................................................
        public ActionResult MoldIssueConfirmation()
        {
            return View();
        }
        public ActionResult MoldIssueConfirmationPageload()
        {
            RequestMoldIssueConfirmation request = new RequestMoldIssueConfirmation();
            ResponseMoldIssueConfirmation response = new ResponseMoldIssueConfirmation();
            request.requestmoldissueconfirmation = new MoldIssueConfirmationEntity();
            request.requestmoldissueconfirmation.USERCODE = Session["LoginEmployeeCode"].ToString();
            MoldIssueConfirmationBC bc = new MoldIssueConfirmationBC();
            response = bc.MoldIssueConfirmationPageloadBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Productionuser)+"|"+
                                 Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Moldissuedetails);
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult MoldIssueConfirmationAssign(string ProductionUser,string productionno,string productiondate,string FGitemcode,string Molditemcode,string Molditemname,string Productionqty)
        {
            string actiontype = "";
            RequestMoldIssueConfirmation request = new RequestMoldIssueConfirmation();
            ResponseMoldIssueConfirmation response = new ResponseMoldIssueConfirmation();
            request.requestmoldissueconfirmation = new MoldIssueConfirmationEntity();
            request.requestmoldissueconfirmations = new List<MoldIssueConfirmationEntity>();
            request.requestmoldissueconfirmation.PRODUCTIONUSER = ProductionUser;
           // request.requestmoldissueconfirmation.USERCODE = Session["LoginEmployeeCode"].ToString();
            MoldIssueConfirmationBC bc = new MoldIssueConfirmationBC();

            var Productionnoarray = productionno.Split('^');
            var ProductionnoDatearray = productiondate.Split('^');
            var FGItemcodearray = FGitemcode.Split('^');
            var Molditemcodearray = Molditemcode.Split('^');
            var Molditemnamearray = Molditemname.Split('^');
            var Productionqtyarry = Productionqty.Split('^');

            for (int i = 0; i < FGItemcodearray.Length; i++)
            {
                MoldIssueConfirmationEntity det = new MoldIssueConfirmationEntity();
                det.PRODUCTIONORDERNO = Productionnoarray[i];
                det.PRODUCTIONDATE = ProductionnoDatearray[i];
                det.FGITEMCODE = FGItemcodearray[i];
                det.MOLDITEMCODE = Molditemcodearray[i];
                det.MOLDITEMNAME = Molditemnamearray[i];
                det.QUANTITY = Productionqtyarry[i];
                request.requestmoldissueconfirmations.Add(det);
            }

            string value = "Update";
            if (value != actiontype)
            {
                response = bc.MoldIssueConfirmationAssignBC(request);
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
        #endregion

        #region Production Order FileUpload ............................................................................
        public ActionResult ProductionOrderFileUpload()
        {
            return View();
        }
        static List<ProductionOrderFileUploadDetailsEntity> listofProductionOrderFileUplDts = new List<ProductionOrderFileUploadDetailsEntity>();

        public ActionResult ProductionOrderFileUploadView()
        {
            string results = "";
            int productionorderno = 0;

            if (Request.Files.Count > 0)
            {
                try
                {

                    // string Effective = 
                    //  Get all files from Request object  
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
                                DataTable dts = new DataTable();
                                dts.Columns.Add("Production Order No");
                                dts.Columns.Add("Order Date");
                                dts.Columns.Add("Cumi Order Ref No");
                                dts.Columns.Add("Cust Item Code");
                                dts.Columns.Add("Cust Item Name");
                                dts.Columns.Add("UOM");
                                dts.Columns.Add("Order Qty");
                                dts.Columns.Add("Variant");

                                string colnames = string.Empty;
                                string ColumnData = string.Empty;

                                for (var j = 0; j < dataTable.Rows.Count; j++)
                                {
                                    DataRow dr = dts.NewRow();
                                    dr["Production Order No"] = dataTable.Rows[j][0].ToString();
                                    dr["Order Date"] = dataTable.Rows[j][1].ToString();
                                    dr["Cumi Order Ref No"] = dataTable.Rows[j][2].ToString();
                                    dr["Cust Item Code"] = dataTable.Rows[j][3].ToString();
                                    dr["Cust Item Name"] = dataTable.Rows[j][4].ToString();
                                    dr["UOM"] = dataTable.Rows[j][5].ToString();
                                    dr["Order Qty"] = dataTable.Rows[j][6].ToString();
                                    dr["Variant"] = dataTable.Rows[j][7].ToString();
                                    if (!dataTable.Rows[j][0].ToString().Equals(""))
                                    {
                                        ProductionOrderFileUploadDetailsEntity Itemdts = new ProductionOrderFileUploadDetailsEntity();
                                        Itemdts.PRODUCTIONORDERNO = dataTable.Rows[j][0].ToString();
                                        Itemdts.ORDERDATE = dataTable.Rows[j][1].ToString();
                                        Itemdts.CUMIREFORDERNO = dataTable.Rows[j][2].ToString();
                                        Itemdts.CUSTITEMCODE = dataTable.Rows[j][3].ToString();
                                        Itemdts.CUSTITEMNAME = dataTable.Rows[j][4].ToString();
                                        Itemdts.UOM = dataTable.Rows[j][5].ToString();
                                        Itemdts.ORDERQTY = dataTable.Rows[j][6].ToString();
                                        Itemdts.VARIANT = dataTable.Rows[j][7].ToString();
                                        listofProductionOrderFileUplDts.Add(Itemdts);
                                        dts.Rows.Add(dr);

                                    }

                                    else
                                    {
                                        productionorderno++;
                                    }

                                }

                                reader.Close();
                                if (productionorderno > 0)
                                {
                                    results = "False" + "^" + "Production Order No Is Missing.";
                                }

                                else
                                {
                                    results = "True" + "^" + Utility.DataTableToJSONWithJavaScriptSerializer(dts);
                                }




                            }
                        }
                        else
                        {
                            results = "False" + "^" + "Uploaded InValid Excel File. ";
                        }

                    }


                }

                catch (Exception ex)
                {

                    results = "False" + "^" + "Error occurred. Error details: ";

                }
            }

            JsonResult json = Json(results, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult ProductionOrderFileUploadInsert()
        {

            //string _stringOfA = char.ConvertFromUtf32(65);

            // int _asciiOfA = char.ConvertToUtf32("A", 0);


            string results = "";
            string data = "";
            IExcelDataReader reader = null;
            DataTable dataTable = null;
            if (Request.Files.Count > 0)
            {
                try
                {
                    if (listofProductionOrderFileUplDts.Count > 0)
                    {
                        RequestProductionOrderFileUpload request = new RequestProductionOrderFileUpload();
                        ResponseProductionOrderFileUpload response = new ResponseProductionOrderFileUpload();
                        request.requestproductionfileuploaddetails = listofProductionOrderFileUplDts;
                        request.requestproductionorderfileupload = new ProductionOrderFileUploadEntity();
                        // request.requestexistinginventory.USERCODE = Session["LoginEmployeeCode"].ToString();
                        ProductionOrderFileUploadBC bc = new ProductionOrderFileUploadBC();
                        // string value = "Update";
                        response = bc.ProductionOrderFileUploadInsertBC(request);
                        ManageError Err = new ManageError();
                        string resultjson = Err.GenerateErrorMessages(response.ErrorConatiner);
                        resultjson = response.result + "|" + resultjson;
                        if (response.result == true)
                        {
                            listofProductionOrderFileUplDts = new List<ProductionOrderFileUploadDetailsEntity>();
                            results = "True" + "|" + "Production Order Details Uploaded Successfully.!";
                        }
                        else
                        {
                            results = "False" + "|" + response.message;
                        }
                        return Json(results);
                    }
                    else
                    {
                        results = "False" + "|" + "Please View Uploaded Content before Save. ";
                        return Json(results);
                    }
                }
                catch (Exception ex)
                {
                    results = "False" + "|" + ex.Message.ToString();
                    return Json(results);
                }
            }
            else
            {
                results = "False" + "|" + "Please Choose File to Upload. ";
                return Json(results);
            }
        }
        public ActionResult ProductionOrderFileUploadDownload(string file)
        {
            string filename = Path.GetFileName(file);
            string fullPath = Path.Combine(Server.MapPath("~/Downloads"), filename);


            return File(fullPath, "application/octet-stream", filename);


        }
        #endregion

        #region Mold PressCount FileUpload ............................................................................
        public ActionResult MoldPressCountFileUpload()
        {
            return View();
        }
        static List<MoldPressCountFileUploadDetailsEntity> listofMoldPressCountFileUplDts = new List<MoldPressCountFileUploadDetailsEntity>();

        public ActionResult MoldPressCountFileUploadView()
        {
            string results = "";
            int MoldItemCode = 0;

            if (Request.Files.Count > 0)
            {
                try
                {

                    // string Effective = 
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    var postedFile = System.Web.HttpContext.Current.Request.Files["CsvDoc"];
                    listofMoldPressCountFileUplDts = new List<MoldPressCountFileUploadDetailsEntity>();
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
                                DataTable dts = new DataTable();
                                dts.Columns.Add("Mould Item Code");
                                dts.Columns.Add("Mould Item Name");
                                dts.Columns.Add("FG Item Code");
                                dts.Columns.Add("Shot Count");
                                dts.Columns.Add("Date Of Shot");
                                dts.Columns.Add("Total Shot Count");
                                dts.Columns.Add("RFID No");

                                string colnames = string.Empty;
                                string ColumnData = string.Empty;

                                for (var j = 0; j < dataTable.Rows.Count; j++)
                                {
                                    DataRow dr = dts.NewRow();
                                    dr["Mould Item Code"] = dataTable.Rows[j][0].ToString();
                                    dr["Mould Item Name"] = dataTable.Rows[j][1].ToString();
                                    dr["FG Item Code"] = dataTable.Rows[j][2].ToString();
                                    dr["Shot Count"] = dataTable.Rows[j][3].ToString();
                                    dr["Date Of Shot"] = dataTable.Rows[j][4].ToString();
                                    dr["Total Shot Count"] = dataTable.Rows[j][5].ToString();
                                    dr["RFID No"] = dataTable.Rows[j][6].ToString();
                                    if (!dataTable.Rows[j][0].ToString().Equals(""))
                                    {
                                        MoldPressCountFileUploadDetailsEntity Itemdts = new MoldPressCountFileUploadDetailsEntity();
                                        Itemdts.MOLDITEMCODE = dataTable.Rows[j][0].ToString();
                                        Itemdts.MOLDITEMNAME = dataTable.Rows[j][1].ToString();
                                        Itemdts.FGITEMCODE = dataTable.Rows[j][2].ToString();
                                        Itemdts.SHOTCOUNT = dataTable.Rows[j][3].ToString();
                                        Itemdts.DATEOFSHOT = dataTable.Rows[j][4].ToString();
                                        Itemdts.TOTALSHOTCOUNT = dataTable.Rows[j][5].ToString();
                                        Itemdts.RFIDNO = dataTable.Rows[j][6].ToString();
                                        listofMoldPressCountFileUplDts.Add(Itemdts);
                                        dts.Rows.Add(dr);

                                    }

                                    else
                                    {
                                        MoldItemCode++;
                                    }

                                }

                                reader.Close();
                                if (MoldItemCode > 0)
                                {
                                    results = "False" + "^" + "Mold Item Code Is Missing.";
                                }

                                else
                                {
                                    results = "True" + "^" + Utility.DataTableToJSONWithJavaScriptSerializer(dts);
                                }




                            }
                        }
                        else
                        {
                            results = "False" + "^" + "Uploaded InValid Excel File. ";
                        }

                    }


                }

                catch (Exception ex)
                {

                    results = "False" + "^" + "Error occurred. Error details: ";

                }
            }

            JsonResult json = Json(results, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult MoldPressCountFileUploadInsert()
        {

            //string _stringOfA = char.ConvertFromUtf32(65);

            // int _asciiOfA = char.ConvertToUtf32("A", 0);


            string results = "";
            string data = "";
            IExcelDataReader reader = null;
            DataTable dataTable = null;
            if (Request.Files.Count > 0)
            {
                try
                {
                    if (listofMoldPressCountFileUplDts.Count > 0)
                    {
                        RequestMoldPressCountFileUpload request = new RequestMoldPressCountFileUpload();
                        ResponseMoldPressCountFileUpload response = new ResponseMoldPressCountFileUpload();
                        request.requestmoldpresscountdts = listofMoldPressCountFileUplDts;
                        request.requestmoldpresscount = new MoldPressCountFileUploadEntitycs();
                        // request.requestexistinginventory.USERCODE = Session["LoginEmployeeCode"].ToString();
                        MoldPressCountFileUploadBC bc = new MoldPressCountFileUploadBC();
                        // string value = "Update";
                        response = bc.MoldPressCountFileUploadInsertBC(request);
                        ManageError Err = new ManageError();
                        string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
                        resultjson = response.result + "|" + resultjson;
                        if (response.result == true)
                        {
                            listofMoldPressCountFileUplDts = new List<MoldPressCountFileUploadDetailsEntity>();
                            results = "True" + "|" + "Mold Press Count Uploaded Successfully.!";
                        }
                        else
                        {
                            results = "False" + "|" + response.message;
                        }
                        return Json(results);
                    }
                    else
                    {
                        results = "False" + "|" + "Please View Uploaded Content before Save. ";
                        return Json(results);
                    }
                }
                catch (Exception ex)
                {
                    results = "False" + "|" + ex.Message.ToString();
                    return Json(results);
                }
            }
            else
            {
                results = "False" + "|" + "Please Choose File to Upload. ";
                return Json(results);
            }
        }
        public ActionResult MoldPressCountFileUploadDownload(string file)
        {
            string filename = Path.GetFileName(file);
            string fullPath = Path.Combine(Server.MapPath("~/Downloads"), filename);


            return File(fullPath, "application/octet-stream", filename);


        }
        #endregion

        #region RDC Picklist FileUpload ............................................................................
        public ActionResult RDCPicklistFileUpload()
        {
            return View();
        }
        static List<RDCPicklistFileuploadDetailsEntity> listofRDCPicklistFileUplDts = new List<RDCPicklistFileuploadDetailsEntity>();

        public ActionResult RDCPicklistFileuploadView()
        {
            string results = "";
            int RDCNo = 0;

            if (Request.Files.Count > 0)
            {
                try
                {

                    // string Effective = 
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    var postedFile = System.Web.HttpContext.Current.Request.Files["CsvDoc"];
                    listofRDCPicklistFileUplDts = new List<RDCPicklistFileuploadDetailsEntity>();
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
                                DataTable dts = new DataTable();
                                dts.Columns.Add("RDC No");
                                dts.Columns.Add("Date");
                                dts.Columns.Add("Item Code");
                                dts.Columns.Add("Item Name");
                                dts.Columns.Add("Supplier");
                                dts.Columns.Add("Qty");

                                string colnames = string.Empty;
                                string ColumnData = string.Empty;

                                for (var j = 0; j < dataTable.Rows.Count; j++)
                                {
                                    DataRow dr = dts.NewRow();
                                    dr["RDC No"] = dataTable.Rows[j][0].ToString();
                                    dr["Date"] = dataTable.Rows[j][1].ToString();
                                    dr["Item Code"] = dataTable.Rows[j][2].ToString();
                                    dr["Item Name"] = dataTable.Rows[j][3].ToString();
                                    dr["Supplier"] = dataTable.Rows[j][4].ToString();
                                    dr["Qty"] = dataTable.Rows[j][5].ToString();
                                    if (!dataTable.Rows[j][0].ToString().Equals(""))
                                    {
                                        RDCPicklistFileuploadDetailsEntity Itemdts = new RDCPicklistFileuploadDetailsEntity();
                                        Itemdts.RDCNO = dataTable.Rows[j][0].ToString();
                                        Itemdts.DATE = dataTable.Rows[j][1].ToString();
                                        Itemdts.ITEMCODE = dataTable.Rows[j][2].ToString();
                                        Itemdts.ITEMNAME = dataTable.Rows[j][3].ToString();
                                        Itemdts.SUPPLIER = dataTable.Rows[j][4].ToString();
                                        Itemdts.QTY = dataTable.Rows[j][5].ToString();
                                        listofRDCPicklistFileUplDts.Add(Itemdts);
                                        dts.Rows.Add(dr);

                                    }

                                    else
                                    {
                                        RDCNo++;
                                    }

                                }

                                reader.Close();
                                if (RDCNo > 0)
                                {
                                    results = "False" + "^" + "RDC No Is Missing.";
                                }

                                else
                                {
                                    results = "True" + "^" + Utility.DataTableToJSONWithJavaScriptSerializer(dts);
                                }




                            }
                        }
                        else
                        {
                            results = "False" + "^" + "Uploaded InValid Excel File. ";
                        }

                    }


                }

                catch (Exception ex)
                {

                    results = "False" + "^" + "Error occurred. Error details: ";

                }
            }

            JsonResult json = Json(results, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult RDCPicklistFileuploadInsert()
        {

            //string _stringOfA = char.ConvertFromUtf32(65);

            // int _asciiOfA = char.ConvertToUtf32("A", 0);


            string results = "";
            string data = "";
            IExcelDataReader reader = null;
            DataTable dataTable = null;
            if (Request.Files.Count > 0)
            {
                try
                {
                    if (listofRDCPicklistFileUplDts.Count > 0)
                    {
                        RequestRDCPicklistFileupload request = new RequestRDCPicklistFileupload();
                        ResponseRDCPicklistFileupload response = new ResponseRDCPicklistFileupload();
                        request.requestRDCPicklistFileuploadDetails = listofRDCPicklistFileUplDts;
                        request.requestRDCPicklistFileupload = new RDCPicklistFileuploadEntity();
                        // request.requestexistinginventory.USERCODE = Session["LoginEmployeeCode"].ToString();
                        RDCPicklistFileUploadBC bc = new RDCPicklistFileUploadBC();
                        // string value = "Update";
                        response = bc.RDCPicklistFileuploadInsertBC(request);
                        ManageError Err = new ManageError();
                        string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
                        resultjson = response.result + "|" + resultjson;
                        if (response.result == true)
                        {
                            listofRDCPicklistFileUplDts = new List<RDCPicklistFileuploadDetailsEntity>();
                            results = "True" + "|" + "RDC Picklist Uploaded Successfully.!";
                        }
                        else
                        {
                            results = "False" + "|" + response.message;
                        }
                        return Json(results);
                    }
                    else
                    {
                        results = "False" + "|" + "Please View Uploaded Content before Save. ";
                        return Json(results);
                    }
                }
                catch (Exception ex)
                {
                    results = "False" + "|" + ex.Message.ToString();
                    return Json(results);
                }
            }
            else
            {
                results = "False" + "|" + "Please Choose File to Upload. ";
                return Json(results);
            }
        }
        public ActionResult RDCPicklistFileUploadDownload(string file)
        {
            string filename = Path.GetFileName(file);
            string fullPath = Path.Combine(Server.MapPath("~/Downloads"), filename);


            return File(fullPath, "application/octet-stream", filename);


        }
        #endregion

        public string createlog(string content)
        {
            string filepath = ConfigurationSettings.AppSettings["logpath"].ToString();
            string Errordescription = ConfigurationSettings.AppSettings["logdescription"].ToString();

            try
            {
                string CurrentMachineName = Environment.MachineName;
                content += "  --  CurrentMachineName : " + CurrentMachineName;
                string UniqueID = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt");

                System.IO.StreamWriter logWriter;
                DateTime currentTime = DateTime.Now;
                string logdate = "";
                logdate += currentTime.Year.ToString();

                if (int.Parse(currentTime.Month.ToString()) < 10)
                {
                    logdate += "0" + currentTime.Month.ToString();
                }
                else
                {
                    logdate += currentTime.Month.ToString();
                }

                if (int.Parse(currentTime.Day.ToString()) < 10)
                {
                    logdate += "0" + currentTime.Day.ToString();
                }
                else
                {
                    logdate += currentTime.Day.ToString();
                }

                //filepath after defined
                string logFile = filepath + "accesslogs_" + logdate + ".txt";

                if (System.IO.File.Exists(logFile))
                {
                    logWriter = System.IO.File.AppendText(logFile);
                }
                else
                {
                    logWriter = System.IO.File.CreateText(logFile);
                    logWriter.WriteLine("UniqueId,Page,IP,Date,Time,Param");
                }
                if (content != "")
                {
                    logWriter.WriteLine(UniqueID + "," + content);
                }
                logWriter.Close();
            }
            catch (Exception exp)
            {
                string Message = "ex.message :" + exp.Message + " -- Ex.Source :" + exp.Source + " -- e.starktrace : " + exp.StackTrace;
                String source = "Logger - accesslog";
                String log = "Application";
            }
            return Errordescription;
        }

    }

}



