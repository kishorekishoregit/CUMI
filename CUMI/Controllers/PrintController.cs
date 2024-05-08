using CUMI.Common;
using CUMIBC;
using CUMIENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CUMI.Controllers
{
    public class PrintController : Controller
    {
        //
        #region GRN Label Print .............................................................................
        public ActionResult GRNLabelPrint()
        {
            return View();
        }
        #endregion


        #region Mold Picklist Print ............................................
        public ActionResult MoldPicklistPrint()
        {
            return View();
        }
        public ActionResult MoldPicklistReportpageload()
        {
            RequestMoldPicklist request = new RequestMoldPicklist();
            ResponseMoldPicklist response = new ResponseMoldPicklist();
            MoldPicklistReportBC bc = new MoldPicklistReportBC();
            response = bc.MoldPicklistReportpageloadBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Moldpicklistpageload);

            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MoldPicklistReportGenerate(string Picklistno)
        {
            RequestMoldPicklist request = new RequestMoldPicklist();
            ResponseMoldPicklist response = new ResponseMoldPicklist();
            request.requestmoldpicklist = new MoldPicklistReportEntity();
            request.requestmoldpicklist.PICKLISTNO = Picklistno;
            MoldPicklistReportBC bc = new MoldPicklistReportBC();
            response = bc.MoldPicklistReportGenerateBC(request);
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_MoldpicklistHeader) + "|" +
                Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_MoldpicklistDetails);

            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region RM Picklist Print ............................................
        public ActionResult RMPicklistPrint()
        {
            return View();
        }
        public ActionResult RMPicklistReportpageload()
        {
            RequestRMPicklistReport request = new RequestRMPicklistReport();
            ResponseRMPicklistReport response = new ResponseRMPicklistReport();
            RMPicklistReportBC bc = new RMPicklistReportBC();
            response = bc.RMPicklistReportpageloadBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_RMpicklistpageload);

            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RMPicklistReportGenerate(string Picklistno)
        {
            RequestRMPicklistReport request = new RequestRMPicklistReport();
            ResponseRMPicklistReport response = new ResponseRMPicklistReport();
            request.requestrmpicklistreport = new RMPicklistReportEntity();
            request.requestrmpicklistreport.PICKLISTNO = Picklistno;
            RMPicklistReportBC bc = new RMPicklistReportBC();
            response = bc.RMPicklistReportGenerateBC(request);
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_RMPicklistheader) + "|" +
                Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_RMPicklistdetails);

            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
