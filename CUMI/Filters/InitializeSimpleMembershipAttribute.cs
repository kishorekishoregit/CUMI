using System;
using System.Data.Entity;
using System.Threading;
using System.Web.Mvc;
using CUMI.Models;
//using WebMatrix.WebData;

namespace CUMI.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<UserContext>(null);
                try
                {
                    using (UserContext usersContext = new UserContext())
                    {
                        //if (!usersContext.get_Database().Exists())
                        //{
                        //    //usersContext.get_ObjectContext().CreateDatabase();
                        //}
                    }
                    //WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", true);
                }
                catch (Exception varLK0)
                {
                    Exception innerException = varLK0;
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", innerException);
                }
            }
        }
        private static InitializeSimpleMembershipAttribute.SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            LazyInitializer.EnsureInitialized<InitializeSimpleMembershipAttribute.SimpleMembershipInitializer>(ref InitializeSimpleMembershipAttribute._initializer, ref InitializeSimpleMembershipAttribute._isInitialized, ref InitializeSimpleMembershipAttribute._initializerLock);
        }
    }
}
