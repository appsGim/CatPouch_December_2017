using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;

namespace DogAndCatAPI.Utils
{
    public static class UtilsWeb
    {

        //https://www.google.com/recaptcha/api/siteverify'
        //Sercret=6LcBZyQTAAAAAC-ZXYLd9UOzmdrC39Owx2BzTRV7&reponse=’token from client’&remoteip=’clientip’


        private static string GoogleURL = "https://www.google.com/recaptcha/api/siteverify";
        private static string Data = "secret=6LcBZyQTAAAAAC-ZXYLd9UOzmdrC39Owx2BzTRV7&response={0}&remoteip={1}";//
        private static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        /// <summary>
        /// 0 - MSG
        /// 1 - PHONE
        /// 2 - APITransaction
        /// return util response
        /// </summary> 
        public static string MakeRequest_Google_Captch(string token, string ip, ref Guid APITransaction, ref DateTime APICreateDate,
                                                                         ref DateTime APICreateDate_ISR,
                                                                         out string Request, ref enumProject Project)
        {
            Request = "Init";
            try
            {

                 

                Request = string.Format(Data, token, ip);//,ip
                Uri address = new Uri(GoogleURL);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded; ";


                if (!string.IsNullOrWhiteSpace(Request))
                {
                    byte[] byteData = UTF8Encoding.UTF8.GetBytes(Request);

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
            catch (Exception ex)
            {

                #region MyRegion LOG

                UtilsDB.API_Log_Insert(enumAction.GoogleCaptcha, enumLogType._1_WebException, enumLogType._1_WebException,
                                                             token,
                                                             ip,
                                                             UtilsException.GetMSG(ref ex),
                                                             true,
                                                             null,
                                                             null,
                                                             null,
                                                             ref APICreateDate,
                                                             ref APICreateDate_ISR,
                                                             ref APITransaction, true, ip,Project);

                #endregion

                return "EXCEPTION";
            }
        }

    }
}