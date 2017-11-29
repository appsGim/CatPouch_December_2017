using DogAndCatAPI.Models;
using DogAndCatAPI.Utils;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace DogAndCatAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.CanWriteType(typeof(Result));
            AppManager.Update();
        }


        protected void Application_PreSendRequestHeaders()
        {
            // Response.Headers.Remove("Server");
            Response.Headers.Set("Server", "My httpd server");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-AspNetMvc-Version");
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

            if (Request.UrlReferrer != null && Domains.Get_AllowedDomains().Select(x => Request.UrlReferrer.Host.ToLower().Contains(x)).ToList().Count > 0)
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", Request.UrlReferrer.Scheme + "://" + Request.UrlReferrer.Host);


        }
    }



    }



//AreaRegistration.RegisterAllAreas();
//			GlobalConfiguration.Configuration.Formatters.JsonFormatter.CanWriteType(typeof(Result));
//			WebApiConfig.Register(GlobalConfiguration.Configuration);
//			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
//			RouteConfig.RegisterRoutes(RouteTable.Routes);
//			BundleConfig.RegisterBundles(BundleTable.Bundles);