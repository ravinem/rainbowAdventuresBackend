using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Http.Filters;
using log4net;
using logging_study;
using System.Web.Http.Controllers;
using System.Diagnostics;

namespace Rainbow_Adventure_web_service
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }

   
}
