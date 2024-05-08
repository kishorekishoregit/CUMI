using ClosedXML.Excel;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using CUMI.ErrorManager;
using CUMIENTITY;
using CUMIBC;
using CUMI.Common;
using System.Data.SqlClient;
using System.Transactions;
using System.Web.Util;
using DocumentFormat.OpenXml.Math;

namespace CUMI.Controllers
{
 
     public class ReportController : Controller
     {

        #region Mold Inward/Interlinking Report............................................
        public ActionResult MoldInwardOrInterlinkingReport()
        {
            return View();
        }
        public ActionResult MoldInwardOrInterlinkingPageLoad()
        {
            RequestMoldInwardOrInterlinking request = new RequestMoldInwardOrInterlinking();
            ResponseMoldInwardOrInterlinking response = new ResponseMoldInwardOrInterlinking();
            MoldInwardOrInterlinkingReportBC bc = new MoldInwardOrInterlinkingReportBC();
            response = bc.MoldInwardOrInterlinkingPageLoadBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_MoldInwardPageload);
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MoldInwardOrInterlinkingGenerate(string Fromdate,string Todate)
        {
            RequestMoldInwardOrInterlinking request = new RequestMoldInwardOrInterlinking();
            ResponseMoldInwardOrInterlinking response = new ResponseMoldInwardOrInterlinking();
            request.requestinwardorinterlinking = new MoldInwardOrInterlinkingReportEntity();
            request.requestinwardorinterlinking.FROMDATE = Fromdate;
            request.requestinwardorinterlinking.TODATE = Todate;
            MoldInwardOrInterlinkingReportBC bc = new MoldInwardOrInterlinkingReportBC();
            response = bc.MoldInwardOrInterlinkingGenerateDAL(request);
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_MoldInwardGenerate);
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Mold PutAway Report............................................
        public ActionResult MoldPutAwayReport()
        {
            return View();
        }

        public ActionResult MoldPutawayreportPageLoad()
        {
            RequestMoldPutawayReport request = new RequestMoldPutawayReport();
            ResponseMoldPutawayReport response = new ResponseMoldPutawayReport();
            MoldPutawayReportBC bc = new MoldPutawayReportBC();
            response = bc.MoldPutawayPageLoadBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_MoldPutawayPageload);
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MoldGeneratereport(string Fromdate, string Todate)
        {
            RequestMoldPutawayReport request = new RequestMoldPutawayReport();
            ResponseMoldPutawayReport response = new ResponseMoldPutawayReport();
            request.requestmoldputaway = new MoldPutawayReportEntity();
            request.requestmoldputaway.FROMDATE = Fromdate;
            request.requestmoldputaway.TODATE = Todate;
            MoldPutawayReportBC bc = new MoldPutawayReportBC();
            response = bc.MoldPutawayGenerateBC(request);
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_MoldPutawayGenerate);
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Location Wise Mold Stock Report............................................
        public ActionResult LocationWiseMoldStockReport()
        {
            return View();
        }
        public ActionResult LocationWiseMoldStockReportPageLoad()
        {
            RequestLocationWiseMoldStockReport request = new RequestLocationWiseMoldStockReport();
            ResponseLocationWiseMoldStockReport response = new ResponseLocationWiseMoldStockReport();
            LocationWiseMoldStockReportBC bc = new LocationWiseMoldStockReportBC();
            response = bc.LocationWiseMoldReportPageLoadBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Location)
                  + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_LocationwisemoldreportPageload);
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LocationWiseMoldStockReportGeneratere(string Location, string Fromdate, string Todate)
        {
            RequestLocationWiseMoldStockReport request = new RequestLocationWiseMoldStockReport();
            ResponseLocationWiseMoldStockReport response = new ResponseLocationWiseMoldStockReport();
            request.requestlocationwisemoldreport = new LocationWiseMoldStockReportEntity();
            request.requestlocationwisemoldreport.FROMDATE = Fromdate;
            request.requestlocationwisemoldreport.TODATE = Todate;
            request.requestlocationwisemoldreport.LOCATION = Location;
            //   request.requestrmmoldputaway.TODATE = Todate;
            LocationWiseMoldStockReportBC bc = new LocationWiseMoldStockReportBC();
            response = bc.LocationWiseMoldReportGenerateBC(request);
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_LocationwisemoldreportGenerate);
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region RM Inward Report............................................
        public ActionResult RMInwardReport()
        {
            return View();
        }
        public ActionResult RMInwardReportPageload()
        {
            RequestRMIwardReport request = new RequestRMIwardReport();
            ResponseRMInwardReport response = new ResponseRMInwardReport();
            RMIwardReportBC bc = new RMIwardReportBC();
            response = bc.RMInwardReportPageloadBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_RMInwardPageload);
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RMInwardReportGenerate(string Fromdate,string Todate)
        {
            RequestRMIwardReport request = new RequestRMIwardReport();
            ResponseRMInwardReport response = new ResponseRMInwardReport();
            request.requestrminward = new RMIwardReportEntity();
            request.requestrminward.FROMDATE = Fromdate;
            request.requestrminward.TODATE = Todate;
            RMIwardReportBC bc = new RMIwardReportBC();
            response = bc.RMInwardReportGenerateBC(request);
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_RMInwardGenerate);
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region RM PutAway Report............................................
        public ActionResult RMPutAwayReport()
        {
            return View();
        }

        public ActionResult RMMoldPutawayreportPageLoad()
        {
            RequestRMMoldPutaway request = new RequestRMMoldPutaway();
            ResponseRMMoldPutaway response = new ResponseRMMoldPutaway();
            RMMoldPutawayReportBC bc = new RMMoldPutawayReportBC();
            response = bc.RMMoldPutawayPageLoadBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_RMMoldPutawayPageload);
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RMMoldGeneratereport(string Fromdate, string Todate)
        {
            RequestRMMoldPutaway request = new RequestRMMoldPutaway();
            ResponseRMMoldPutaway response = new ResponseRMMoldPutaway();
            request.requestrmmoldputaway = new RMMoldPutawayReportEntity();
            request.requestrmmoldputaway.FROMDATE = Fromdate;
            request.requestrmmoldputaway.TODATE = Todate;
            RMMoldPutawayReportBC bc = new RMMoldPutawayReportBC();
            response = bc.RMMoldPutawayGenerateBC(request);
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_RMMoldPutawayGenerate);
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Mold Status Report............................................
        public ActionResult MoldStatusReport()
        {
            return View();
        }
        public ActionResult MoldStatusReportPageLoad()
        {
            RequestMoldStatusReport request = new RequestMoldStatusReport();
            ResponseMoldStatusReport response = new ResponseMoldStatusReport();
            MoldStatusReportBC bc = new MoldStatusReportBC();
            response = bc.MoldStatusReportPageLoadBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_type)
                  + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_MoldstatusPageload);
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MoldstatusGeneratereport(string type)
        {
            RequestMoldStatusReport request = new RequestMoldStatusReport();
            ResponseMoldStatusReport response = new ResponseMoldStatusReport();
            request.requestmoldstatusreport = new MoldStatusReportEntity();
            request.requestmoldstatusreport.MOLDHEALTHTYPE = type;
            //   request.requestrmmoldputaway.TODATE = Todate;
            MoldStatusReportBC bc = new MoldStatusReportBC();
            response = bc.MoldStatusReportGenerateBC(request);
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_MoldstatusGenerate);
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Warehouse Transactions Report ............................................
        public ActionResult WarehouseTransactionsReport()
        {
            return View();
        }
        #endregion

      
        #region Mold Scrapping Report ............................................
        public ActionResult MoldScrappingReport()
        {
            return View();
        }
        public ActionResult PageloadMoldScrapReport()
        {
            RequestMoldScrapReport request = new RequestMoldScrapReport();
            ResponseMoldScrapReport response = new ResponseMoldScrapReport();
            MoldScrapReportBC bc = new MoldScrapReportBC();
            response = bc.PageloadMoldScrapReportBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_MoldScrapPageload);
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Mold Recondition Report ............................................
        public ActionResult MoldReconditionReport()
        {
            return View();
        }
        public ActionResult PageloadMoldReconditionReport()
        {
            RequestMoldReconditionReport request = new RequestMoldReconditionReport();
            ResponseMoldReconditionReport response = new ResponseMoldReconditionReport();
            MoldReconditionReportBC bc = new MoldReconditionReportBC();
            response = bc.PageloadMoldReconditionReportBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_MoldReconditionDts);
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Mold Nearby Expiry/Expired Report  ............................................
        public ActionResult MoldNearbyExpiryOrExpiredReport()
        {
            return View();
        }
        public ActionResult MoldNearbyExpiryExpiredReportPageload()
        {
            RequestMoldNearbyExpiryExpiredReport request = new RequestMoldNearbyExpiryExpiredReport();
            ResponseMoldNearbyExpiryExpiredReport response = new ResponseMoldNearbyExpiryExpiredReport();
            MoldNearbyExpiryExpiredReportBC bc = new MoldNearbyExpiryExpiredReportBC();
            response = bc.MoldNearbyExpiryExpiredReportPageloadBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_MoldNearbyExpiryExpiredPageload);
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Mold Stock and Historical Data Report  ............................................
        public ActionResult MoldStockandHistoricalDataReport()
        {
            return View();
        }
        #endregion


        #region Mold Shot Count Report............................................
        public ActionResult ShotCountMoldReport()
        {
            return View();
        }
        public ActionResult ShotCountReportPageLoad()
        {
            RequestShotCountReport request = new RequestShotCountReport();
            ResponseShotCountReport response = new ResponseShotCountReport();
            ShotCountReportBC bc = new ShotCountReportBC();
            response = bc.MoldShotCountReportPageLoadBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_moldcode)
                  + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_MoldcountreportPageload);
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShotCountReportGenerate(string Fromdate, string Todate, string Mold)
        {
            RequestShotCountReport request = new RequestShotCountReport();
            ResponseShotCountReport response = new ResponseShotCountReport();
            request.requestshotcountmoldreport = new ShotCountReportEntity();
            request.requestshotcountmoldreport.MOLDITEMCODE = Mold;
            request.requestshotcountmoldreport.FROMDATE = Fromdate;
            request.requestshotcountmoldreport.TODATE = Todate;
            ShotCountReportBC bc = new ShotCountReportBC();
            response = bc.MoldShotCountReportGenerateBC(request);
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_MoldcountreportGenerate);
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Physical Stock Report............................................
        public ActionResult PhysicalStockReport()
        {
            return View();
        }
        public ActionResult PhysicalStockReportPageLoad()
        {
            RequestPhysicalStockReport request = new RequestPhysicalStockReport();
            ResponsePhysicalStockReport response = new ResponsePhysicalStockReport();
            PhysicalStockReportBC bc = new PhysicalStockReportBC();
            response = bc.PhysicalStockReportPageLoadBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_PhysicalStockPageload);
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult PhysicalStockReportView(string MoldItemCode)
        {
            RequestPhysicalStockReport request = new RequestPhysicalStockReport();
            ResponsePhysicalStockReport response = new ResponsePhysicalStockReport();
            request.requestphysicalstockreport = new PhysicalStockReportEntity();
            request.requestphysicalstockreport.MOLDITEMCODE = MoldItemCode;
            PhysicalStockReportBC bc = new PhysicalStockReportBC();
            response = bc.PhysicalStockReportViewBC(request);
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_PhysicalStockView);
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
