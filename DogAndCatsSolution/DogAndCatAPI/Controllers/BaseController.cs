using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using DogAndCatAPI.Utils;
using DogAndCatAPI.Models;
using DogAndCatAPI.DAL;
using System.Configuration;
using System.Web;

namespace DogAndCatAPI.Controllers
{

    public class BaseController : ApiController
    {
        public enumProject ProjectType = enumProject.Init;

        public Guid APITransaction = Guid.NewGuid();
        public DateTime APICreateDate = new DateTime(DateTime.UtcNow.Ticks);
        public DateTime APICreateDate_ISR = UtilsDateTime.UTC_To_Israel_Time();
        public string SerializedRequest = string.Empty;
        public enumAction Action = enumAction.Init;
    
        public string UA = string.Empty;
        public string IP = string.Empty;
        public string Refferer = string.Empty;
        public enumPlatformType Platform = enumPlatformType.Web;

        public bool ValidRequestFromServerIP = true;
        public string ServerIP = "-1";

        /// <summary>
        /// ValidRequestFromServerIP IS CHECKING IF ONLY SERVER OF CLIENT MADE THE REQUEST
        /// </summary>
        /// <param name="action"></param>
        /// <param name="UserRequest"></param>
        /// <param name="ip"></param>
        /// <param name="ua"></param>
        public void Init_Request_Data(enumAction action, object UserRequest, 
                                      string ip, string ua, Guid ProjectToken)
        {
            AppManager.Update();
             
            Action = action;
            SerializedRequest = Newtonsoft.Json.JsonConvert.SerializeObject(UserRequest);
            Platform = enumPlatformType.Web;
            IP = ip;
            UA = string.IsNullOrEmpty(ua) ? "NULL" : ua;

            if (UA.ToLower().Contains("iphone"))
            {
                Platform = enumPlatformType.iPhone;
            }
            else if (UA.ToLower().Contains("android"))
            {
                Platform = enumPlatformType.Android;
            }
            else if (UA.ToLower().Contains("ipad"))
            {
                Platform = enumPlatformType.IPad;
            }


            HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];

            if (context.Request.ServerVariables["HTTP_VIA"] != null)
            {
                ServerIP = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else
            {
                ServerIP = context.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }

            if (AppManager.RestrictServerIP &&  !Domains.Get_AllowedDomains().Contains(ServerIP))
            {
                ValidRequestFromServerIP = false;
            }

            ProjectType = enumProject.Cat;

            //if (ProjectToken == AppManager.Dog_ServerToken)
            //{
            //    ProjectType = enumProject.Dog;
            //}
            //else if (ProjectToken == AppManager.Cat_ServerToken)
            //{
            //    ProjectType = enumProject.Cat;
            //}
            //else
            //{
            //    ValidRequestFromServerIP = false;
            //}

        }
    }
}
