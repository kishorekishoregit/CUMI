using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUMIENTITY
{
    public class MoldIssueConfirmationEntity
    {
        public string AUTOID {  get; set; }
        public string PICKLISTNO {  get; set; }
        public string PRODUCTIONUSER {  get; set; }
        public string PRODUCTIONORDERNO {  get; set; }
        public string PRODUCTIONDATE {  get; set; }
        public string FGITEMCODE {  get; set; }
        public string FGITEMNAME {  get; set; }
        public string MOLDITEMCODE {  get; set; }
        public string MOLDITEMNAME {  get; set; }
        public string QUANTITY {  get; set; }
        public string USERCODE {  get; set; }
    }
    public class RequestMoldIssueConfirmation
    {
        public MoldIssueConfirmationEntity requestmoldissueconfirmation { get; set; }
        public List<MoldIssueConfirmationEntity> requestmoldissueconfirmations { get; set; }

    }
    public class ResponseMoldIssueConfirmation
    {
        public bool result { get; set; }
        public List<ErrorItem> ErrorContainer { get; set; }
        public DataTable JS_Productionuser { get; set; }
        public DataTable JS_Moldissuepageload { get; set; }
        public DataTable JS_Moldissuedetails {  get; set; }
    }
}
