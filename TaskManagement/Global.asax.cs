using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TaskManagement
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exceptionObject = Server.GetLastError();
            if (exceptionObject != null)
            {
                if (exceptionObject.InnerException != null)
                {
                    exceptionObject = exceptionObject.InnerException;
                }
                switch (exceptionObject.GetType().ToString())
                {
                    case "System.Threading.ThreadAbortException":
                        HttpContext.Current.Server.ClearError();
                        break;
                    default:
                        ErrorLogService.LogError(exceptionObject); 
                        break;
                }
            }
        }
    }
}
