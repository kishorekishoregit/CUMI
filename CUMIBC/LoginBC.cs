using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CUMIENTITY;
using CUMIDAC;
using System.Data.SqlClient;
using System.Data;

namespace CUMIBC
{
    public class LoginBC
    {
        public ResponseLoginDetails LoginsBC(RequestLoginDetails request)
        {

            ResponseLoginDetails response = new ResponseLoginDetails();
            //try
            //{
                WMSDAL DAL = new WMSDAL();
                response.ErrorContainer = Validate(request);
                if (response.ErrorContainer.Count == 0)
                {
                    response = DAL.LoginDAL(request);
                }
                
            //}
            //catch(Exception e) { }
            return response;
        }

        public ResponseLoginDetails FetchUserDetailsBC(RequestLoginDetails request)
        {
            ResponseLoginDetails response = new ResponseLoginDetails();
            WMSDAL DAL = new WMSDAL();
            response = DAL.FetchUserDetails(request);

            return response;
        }

        public ResponseLoginDetails ChangePassWordBC(RequestLoginDetails request)
        {


            ResponseLoginDetails response = new ResponseLoginDetails();
            WMSDAL DAL = new WMSDAL();
            response = DAL.ChangePassWord(request);

            return response;
        }

        public List<ErrorItem> Validate(RequestLoginDetails request)
        {
            List<ErrorItem> err = new List<ErrorItem>();
            if (request.requestLoginDetails.UserName == "")
                err.Add(new ErrorItem { DataItem = "User Name", ErrorNo = "SSB0009" });
            if (request.requestLoginDetails.Password == "")
                err.Add(new ErrorItem { DataItem = "Password", ErrorNo = "SSB0009" });


            return err;
        }
    }
}
