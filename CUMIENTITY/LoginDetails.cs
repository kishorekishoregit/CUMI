﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CUMIENTITY
{
    public class LoginDetails
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmployeeCode { get; set; }
        public string Modifiedby { get; set; }
    }
 
    public class RequestLoginDetails
    {
     
        public LoginDetails requestLoginDetails { get; set; }
    }

    public class ResponseLoginDetails
    {

        public bool result { get; set; }

  
        public List<ErrorItem> ErrorContainer { get; set; }

        public DataTable JS_LoginDetails { get; set; }

        public DataTable JS_ScreenDetails { get; set; }

        public DataTable JS_LoginEmployeeCode { get; set; }
    }
}
