using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

public class LoginPostBack
{
    public string AppKey { get; set; }

    public DateTime ISRTime = UtilsDateTime.UTC_To_Israel_Time();

    public string CMSApp = AppDomain.CurrentDomain.BaseDirectory;

    public string AppVersion { get; set; }
    public string User { get; set; }

    public string MSG { get; set; }

    public string Role { get; set; }

    public string IP { get; set; }
    public LoginPostBack() { }
}
 
    public static class UtilsWeb
{

   private static  string payloadUtil_SMS = "<\"payload\":<\"msg\": \"{0}\",\"phone\": \"{1}\">,\"header\": <\"appkey\": \"{2}\",\"platform\":"
                                                            + " 1,\"version\": 1.1, \"os\": \"os\",\"langid\": 1,\"userapplicationidentifier\":"
                                                            + "\"{3}\",\"udid\": \"{4}\">>";

    private static string GetIPAddress()
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

        return context.Request.ServerVariables["REMOTE_ADDR"];
    }

    public static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
 
   public static bool MakeWebRequest(LoginPostBack data)
        {
         
            string DynamicCMSMoblinUtilsKey = string.Empty,
                   LoginPostBackEnabled = string.Empty,
                   LoginPostBackURL = string.Empty;

            try
            {
                DynamicCMSMoblinUtilsKey = UtilsConfig.Get(enumConfigKeys.DynamicCMSMoblinUtilsKey);
                LoginPostBackEnabled = UtilsConfig.Get(enumConfigKeys.LoginPostBackEnabled);
                LoginPostBackURL = UtilsConfig.Get(enumConfigKeys.LoginPostBackURL);

                if (string.IsNullOrEmpty(DynamicCMSMoblinUtilsKey) ||
                   string.IsNullOrEmpty(LoginPostBackEnabled) ||
                   string.IsNullOrEmpty(LoginPostBackURL) ||
                   !LoginPostBackEnabled.ToLower().Equals("true")) return false;


            }
            catch (Exception ex)
            {
                return false;
            }

            data.AppKey = DynamicCMSMoblinUtilsKey;
            data.AppVersion = UtilsConfig.Get(enumConfigKeys.VERSION);
            data.IP = GetIPAddress();

        try
            {
                HttpWebRequest oHttpRequest = (HttpWebRequest)WebRequest.Create(LoginPostBackURL);
                oHttpRequest.Headers.Clear();
                oHttpRequest.KeepAlive = false;
                oHttpRequest.Method = "GET";
                if (data != null)
                {
                    // Set values for the request back
                    oHttpRequest.Method = "POST";
                    oHttpRequest.ContentType = "application/json";
                    byte[] dataArr = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject( data));
                    oHttpRequest.ContentLength = dataArr.Length;
                    using (Stream stream = oHttpRequest.GetRequestStream())
                        for (int i = 0, lim = dataArr.Length; i < lim; i++)
                            stream.WriteByte(dataArr[i]);
                }
                HttpWebResponse oHttpResponse = (HttpWebResponse)oHttpRequest.GetResponse();
                //check status code
                //if (oHttpResponse.StatusCode == HttpStatusCode.OK)
                //    data = new StreamReader(oHttpResponse.GetResponseStream(), Encoding.UTF8).ReadToEnd();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

    public static string SendUtilsSMS(string msg,string phone,string user, string referer)
    {
        string tosend = string.Format(payloadUtil_SMS,msg,phone, ConfigurationManager.AppSettings["UtilsSMSKey"].ToString(), AppDomain.CurrentDomain.BaseDirectory.Replace("\\","-")+ ": "+referer,user).Replace("<","{").Replace(">","}");
        return MakeSMSRequest(tosend);
    }

    private static string MakeSMSRequest( string data)
    {
        Uri address = new Uri(ConfigurationManager.AppSettings["UtilsSMSLink"].ToString());

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);

        request.Method = "POST"; 
        request.ContentType =   "application/json; charset=utf-8";
         
        if (!string.IsNullOrWhiteSpace(data))
        {
            byte[] byteData = UTF8Encoding.UTF8.GetBytes(data);

            request.ContentLength = byteData.Length;

            using (Stream postStream = request.GetRequestStream())
                postStream.Write(byteData, 0, byteData.Length);
        }
        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        {
            if (response.StatusCode != HttpStatusCode.OK)
                return response.StatusCode.ToString();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            return reader.ReadToEnd().Replace("<", "&lt;").Replace(">", "&gt;");
        }

    }
}
