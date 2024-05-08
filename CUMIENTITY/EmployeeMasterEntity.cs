using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CUMIENTITY
{
    public class EmployeeMasterEntity
    {
        public string AUTOID { get; set; }
        public string EMPLOYEECODE { get; set; }
        public string EMPLOYEENAME { get; set; }
        public string DEPARTMENT { get; set; }
        public string EMAILID { get; set; }
        public string CONTACTNO { get; set; }
        public string PLANT { get; set; }
        public string EMPLOYEESTATUS { get; set; }
        public string USERCODE { get; set; }
    }

    public class RequestEmployeeMaster
    {
        public EmployeeMasterEntity requestemployeemaster { get; set; }
    }

    public class ResponseEmployeeMaster
    {
        public bool result { get; set; }
        public List<ErrorItem> ErrorContainer { get; set; }
        public DataTable JS_RecordStatus { get; set; }
        public DataTable JS_Plant { get; set; }
        public DataTable JS_Department { get; set; }
        public DataTable JS_Employeedetails { get; set; }

    }
}
