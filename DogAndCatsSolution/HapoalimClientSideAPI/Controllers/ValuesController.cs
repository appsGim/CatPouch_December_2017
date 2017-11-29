using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using HapoalimClientSideAPI.Models;
using HapoalimAPI.Utils;
using System.Net.NetworkInformation;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace HapoalimClientSideAPI.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        System.Web.HttpContext _context = System.Web.HttpContext.Current;
        const string CookieStr = "UT_{0}";
        /// <summary>
        /// Insert or get unique user on first load
        /// </summary>
        /// <returns></returns>
        public UserResult IsAlive()
        {
            UserResult ur = new UserResult();

            BaseClass b = new BaseClass() {
                ClientToServerGuid = ConfigurationManager.AppSettings["ServerToken"],
                IP = GetIPAddress(),
                MAC = GetMACAddress(),
                OS = Environment.OSVersion.ToString(),
                UserAgent = GetUserAgent()
            };

            HttpCookie uniqueUserToken = _context.Request.Cookies[String.Format(CookieStr, b.MAC)];

            if (uniqueUserToken == null)
            {
                var json = JsonConvert.SerializeObject(b);
                uniqueUserToken = new HttpCookie(String.Format(CookieStr, b.MAC));
                uniqueUserToken.Expires = DateTime.Now.AddDays(3);
                var retJson = CreateHttpPost("IsAlive", json);
                uniqueUserToken.Value = retJson;
            }

            var userResultJson = JsonConvert.SerializeObject(Newtonsoft.Json.Linq.JObject.Parse(uniqueUserToken.Value)["Response"]);

            ur = JsonConvert.DeserializeObject<UserResult>(userResultJson);

            return ur;
        }
        #region Base Helper // UserAgent, IP, MAC, Platform

        private string GetUserAgent()
        {
            string result = string.Empty;
            if (_context != null)
            {
                if (_context.Request.Headers.ToString().Contains("User-Agent"))
                {
                    var headers = _context.Request.Headers.GetValues("User-Agent");

                    StringBuilder sb = new StringBuilder();

                    foreach (var header in headers)
                    {
                        sb.Append(header);
                        sb.Append(" ");
                    }

                    result = sb.ToString().Trim();
                }
            }
            return result;
        }

        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"] == "::1" ? "127.0.0.1" : context.Request.ServerVariables["REMOTE_ADDR"];
        }

        public static string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    //IPInterfaceProperties properties = adapter.GetIPProperties(); Line is not required
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }

        #endregion

        #region HttpRequest
        public string CreateHttpPost(string apiUrl, string json)
        {
            string retVal = string.Empty;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(String.Format(ConfigurationManager.AppSettings["APIBaseUrl"], apiUrl));
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                retVal = streamReader.ReadToEnd();
            }

            return retVal;
        }


        #endregion
    }
}
