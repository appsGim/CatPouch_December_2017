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
//using DogAndCatAPI.il.co.sendmsg.api3;

namespace DogAndCatAPI.Controllers
{

    public class CampaignController : BaseController
    {

        [HttpPost]
        public Result A(in_isAlive req)
        {

            base.Init_Request_Data(enumAction.IsAlive, req, req.IP, req.UA, req.T);

            string exc = null;
            IPHolder cacheIP = null;

            #region MyRegion VALIDATION

            if (!ModelState.IsValid || !base.ValidRequestFromServerIP)
            {
                cacheIP = Cache_IP.IP_Get(req.IP, 1, out exc, ref base.APICreateDate_ISR, ref base.ProjectType);

                #region MyRegion LOG

                UtilsDB.API_Log_Insert(this.Action, enumLogType.Response, enumLogType._1_PayloadError,
                                                             base.SerializedRequest,
                                                             "base.ValidRequestFromServerIP IP:" + base.ServerIP + " IsValid:" + base.ValidRequestFromServerIP.ToString(),
                                                             Newtonsoft.Json.JsonConvert.SerializeObject(ModelState.Keys)+" -> cacheip:"+ Newtonsoft.Json.JsonConvert.SerializeObject(cacheIP),
                                                             true,
                                                             base.Refferer,
                                                             base.IP,
                                                             base.UA,
                                                             ref base.APICreateDate,
                                                             ref base.APICreateDate_ISR,
                                                             ref base.APITransaction, true, req.IP, base.ProjectType);

                #endregion

                return AppResponse.Any(enumReturnStatus.PayloadError, null);
            }

            #endregion

            Guid ProjectToken = base.APITransaction;
            if (!AppManager.CampaignAlive(base.APICreateDate_ISR, base.ProjectType, ref base.APITransaction, out ProjectToken))
            {
                cacheIP = Cache_IP.IP_Get(req.IP, 1, out exc, ref base.APICreateDate_ISR, ref base.ProjectType);

                #region MyRegion LOG

                UtilsDB.API_Log_Insert(this.Action, enumLogType.End, enumLogType.Response,
                                                             base.SerializedRequest,
                                                             Newtonsoft.Json.JsonConvert.SerializeObject(cacheIP),
                                                             null,
                                                             true,
                                                             base.Refferer,
                                                             base.IP,
                                                             base.UA,
                                                             ref base.APICreateDate,
                                                             ref base.APICreateDate_ISR,
                                                             ref base.APITransaction, false, null, base.ProjectType);

                #endregion

                return AppResponse.Any(enumReturnStatus.Ends, null);
            }

            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>()
            {
                {"P",ProjectToken},
                {"UI",null },
                {"Pets",null }
            };

            List<UIOut> uimsg = new List<Models.UIOut>();

            if (req.ND.GetValueOrDefault(false))
            {
                uimsg = AppManager.UIResponse_Get(ref base.APICreateDate_ISR, base.ProjectType);
                result["UI"] = uimsg;

                #region MyRegion PET REMOVED - PREDEFINED NOW
                //if (uimsg.Count > 0)
                //{
                //    result["Pets"] = AppManager.PetProjectGet(ref base.APICreateDate_ISR, base.ProjectType);
                //} 
                #endregion

            }

            //CHECK IF IP NEED CAPTCHA BUT DO NOT ADD, WE WILL ADD LATER IF NEEDED
          
            cacheIP = Cache_IP.IP_Get(req.IP, 0, out exc, ref base.APICreateDate_ISR, ref base.ProjectType);
            bool needCaptcha = cacheIP.need_Captcha();
            if (needCaptcha)
            {
                #region MyRegion LOG

                UtilsDB.API_Log_Insert(this.Action, enumLogType.Response, enumLogType.Response,
                                                             base.SerializedRequest,
                                                             "NEED CAPTCHA",
                                                             null,
                                                             true,
                                                             base.Refferer,
                                                             base.IP,
                                                             base.UA,
                                                             ref base.APICreateDate,
                                                             ref base.APICreateDate_ISR,
                                                             ref base.APITransaction, false, null, base.ProjectType);

                #endregion

                return AppResponse.Any(enumReturnStatus.Captcha, result);
            }

            #region MyRegion LOG

            UtilsDB.API_Log_Insert(this.Action, enumLogType.Response, enumLogType.Response,
                                                         base.SerializedRequest,
                                                         "OK",
                                                         null,
                                                         true,
                                                         base.Refferer,
                                                         base.IP,
                                                         base.UA,
                                                         ref base.APICreateDate,
                                                         ref base.APICreateDate_ISR,
                                                         ref base.APITransaction, false, null, base.ProjectType);

            #endregion

            return AppResponse.OK(result);
        }

        /// <summary>
        /// LEAD - AND REGISTRATION
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public Result L(SubmitLead req)
        {

            base.Init_Request_Data(enumAction.Lead, req, req.IP, req.UA, req.T);

            string exception = string.Empty;
            IPHolder cacheIP = null;
            bool TokenError = false;
            if (!AppManager.CampaignAlive(base.APICreateDate_ISR, base.ProjectType, ref base.APITransaction, req.P, out TokenError))
            {
                cacheIP = Cache_IP.IP_Get(req.IP, 1, out exception, ref base.APICreateDate_ISR, ref base.ProjectType);

                #region  LOG  if(TokenError)
                if (TokenError)
                {
                    UtilsDB.API_Log_Insert(this.Action, enumLogType.Response, enumLogType._1_PayloadError,
                                                                 base.SerializedRequest,
                                                                 "enumReturnStatus.Ends -> TokenError(P): " + TokenError.ToString(),
                                                                 "cacheIP: " + Newtonsoft.Json.JsonConvert.SerializeObject(cacheIP),
                                                                 true,
                                                                 base.Refferer,
                                                                 base.IP,
                                                                 base.UA,
                                                                 ref base.APICreateDate,
                                                                 ref base.APICreateDate_ISR,
                                                                 ref base.APITransaction, true, req.Email, base.ProjectType);

                    return AppResponse.Any(enumReturnStatus.PayloadError, null);
                }
                #endregion

                return AppResponse.Any(enumReturnStatus.Ends,null);
            }

            #region MyRegion VALIDATION

            if (!ModelState.IsValid || !base.ValidRequestFromServerIP || TokenError || (req.FlatNumber!=null && req.FlatNumber.Length>4)  )
            {
                cacheIP = Cache_IP.IP_Get(req.IP, 1, out exception, ref base.APICreateDate_ISR, ref base.ProjectType);

                #region MyRegion LOG

                UtilsDB.API_Log_Insert(this.Action, enumLogType.Response, enumLogType._1_PayloadError,
                                                             base.SerializedRequest,
                                                             "base.ValidRequestFromServerIP IP:" + base.ServerIP + " IsValid:" + base.ValidRequestFromServerIP.ToString() + " -> TokenError(P): " + TokenError.ToString(),
                                                             Newtonsoft.Json.JsonConvert.SerializeObject(ModelState.Keys)+ " -> cacheIP: " + Newtonsoft.Json.JsonConvert.SerializeObject(cacheIP),
                                                             true,
                                                             base.Refferer,
                                                             base.IP,
                                                             base.UA,
                                                             ref base.APICreateDate,
                                                             ref base.APICreateDate_ISR,
                                                             ref base.APITransaction, true, req.Email, base.ProjectType);

                #endregion

                return AppResponse.Any(enumReturnStatus.PayloadError, null);
            }

            #endregion


            enumPet_Project PetType = enumPet_Project.Cat;


            #region UNIQUE_Exist

            if (Cache_Unique.MAC_UNIQUE_IS_EXIST(ref base.APICreateDate, req.Email, ref base.ProjectType))
            {
                #region MyRegion LOG

                UtilsDB.API_Log_Insert(this.Action, enumLogType.Response, enumLogType._1_Unique_Not_Allowed,
                                                             base.SerializedRequest,
                                                             "_1_MAC_OR_Unique_Address_Not_Allowed",
                                                             null,
                                                             true,
                                                             base.Refferer,
                                                             base.IP,
                                                             base.UA,
                                                             ref base.APICreateDate,
                                                             ref base.APICreateDate_ISR,
                                                             ref base.APITransaction, true, req.Email, base.ProjectType);

                #endregion

                return AppResponse.Any(enumReturnStatus.Forbidden, null);
            }

            #endregion

           
            bool NeedCaptcha = false;

            #region REGINO CACHE IP AND IF CAPTCH IS NEEDED
            //ADD TO IP CALL
            cacheIP = Cache_IP.IP_Get(req.IP, 0, out exception, ref base.APICreateDate_ISR, ref base.ProjectType);

            if (cacheIP != null && cacheIP.need_Captcha() && string.IsNullOrEmpty(req.CAP))
            {
                NeedCaptcha = true;
                UtilsDB.API_Log_Insert(base.Action, enumLogType.Request, enumLogType._1_Need_Captcha, base.SerializedRequest, "NEED CAPTCHA -> Captch: " + Newtonsoft.Json.JsonConvert.SerializeObject(cacheIP), null, false,
                                                         base.Refferer, base.IP, base.UA, ref base.APICreateDate, ref base.APICreateDate_ISR,
                                                         ref base.APITransaction, true, req.Email, base.ProjectType);



                return AppResponse.Any(enumReturnStatus.Captcha, null);
            }
            #endregion

            #region REGION GOOGLE VERIFICATION
            string googleRequest = string.Empty, googleResponse = string.Empty;
            bool GoogleApproved = false;
            if (cacheIP != null && cacheIP.need_Captcha() && !string.IsNullOrEmpty(req.CAP))
            {
                googleResponse = UtilsWeb.MakeRequest_Google_Captch(req.CAP, req.IP, ref base.APITransaction, ref base.APICreateDate, ref base.APICreateDate_ISR,
                                                                   out googleRequest, ref base.ProjectType);


                GoogleApproved = googleResponse.ToLower().Contains("true");

                if (!GoogleApproved)
                {
                    NeedCaptcha = true;
                    UtilsDB.API_Log_Insert(base.Action, enumLogType.Request, enumLogType._1_Need_ReCaptcha,
                                                             "FAIL ON GOOGLE CAPTCHA: " + base.SerializedRequest,
                                                             googleRequest,
                                                             googleResponse,
                                                             false,
                                                             base.Refferer,
                                                             base.IP,
                                                             base.UA,
                                                             ref base.APICreateDate,
                                                             ref base.APICreateDate_ISR,
                                                             ref base.APITransaction,
                                                             true, req.Email, base.ProjectType);



                    return AppResponse.Any(enumReturnStatus.ReCaptcha, null);
                }
            }
            #endregion

            API_Lead_InsertResult lead =
            UtilsDB.API_Lead_Insert(ref req, /*pet.ID*/(int)PetType, ref base.APITransaction, req.CAP,1, 
                                    ref base.APICreateDate, ref base.APICreateDate_ISR, ref base.Platform, ref base.ProjectType);

            if (lead == null)
            {
                #region  LOG  

                UtilsDB.API_Log_Insert(this.Action, enumLogType.Response, enumLogType._1_InternalError,
                                                             base.SerializedRequest,
                                                             "API_Lead_Insert ERROR",
                                                             null,
                                                             true,
                                                             base.Refferer,
                                                             base.IP,
                                                             base.UA,
                                                             ref base.APICreateDate,
                                                             ref base.APICreateDate_ISR,
                                                             ref base.APITransaction, true, req.Email, base.ProjectType);

                #endregion

                return AppResponse.Any(enumReturnStatus.InternalError, null);
            }

            switch (lead.DBResult.GetValueOrDefault(-1000))
            {

                case -1000://LOGIC ERROR OR INTERNAL DVB ERROR
                    #region  LOG  

                    UtilsDB.API_Log_Insert(this.Action, enumLogType.Response, enumLogType._1_InternalError,
                                                                 base.SerializedRequest,
                                                                 "API_Lead_Insert ERROR",
                                                                 "lead.DBResult is null",
                                                                 true,
                                                                 base.Refferer,
                                                                 base.IP,
                                                                 base.UA,
                                                                 ref base.APICreateDate,
                                                                 ref base.APICreateDate_ISR,
                                                                 ref base.APITransaction, true, req.Email, base.ProjectType);

                    #endregion

                    return AppResponse.Any(enumReturnStatus.InternalError, null);
                    break;
                case 200://OK

                    string ex = null;
                    Cache_IP.AddUpdate_IP(req.IP, new IPHolder(null, req.IP, lead.Count_IP.GetValueOrDefault(), lead.MaxAllowed_IP.GetValueOrDefault(), lead.ExpiredIPAt.GetValueOrDefault()), out ex, ref base.ProjectType);


                    //API_Project_GetResult project = AppManager.GetProject(base.ProjectType);
                    //DataProResult result = UtilsDataPro.SendCouponEmail(req.Email, ref project);
                      
                    //API_Lead_Update_CouponMSGResult coupon =
                    //UtilsDB.API_Lead_Update_CouponMSG(ref base.APITransaction, ref base.APICreateDate, 
                    //                                  ref base.APICreateDate_ISR, lead.LeadID.GetValueOrDefault(), 
                    //                                  Newtonsoft.Json.JsonConvert.SerializeObject(result), result.isSuccess());

                    #region  LOG  

                    UtilsDB.API_Log_Insert(this.Action, enumLogType.Response, enumLogType.Response,
                                                                 base.SerializedRequest,
                                                                null/* Newtonsoft.Json.JsonConvert.SerializeObject(result)*/,
                                                                "lead: " + Newtonsoft.Json.JsonConvert.SerializeObject(lead)/* + " -> coupon update: " + Newtonsoft.Json.JsonConvert.SerializeObject(coupon)*/,
                                                                 true,
                                                                 base.Refferer,
                                                                 base.IP,
                                                                 base.UA,
                                                                 ref base.APICreateDate,
                                                                 ref base.APICreateDate_ISR,
                                                                 ref base.APITransaction, true, req.Email, base.ProjectType);

                    #endregion

                    return AppResponse.OK(null);



                    break;
            }

            #region  LOG  

            UtilsDB.API_Log_Insert(this.Action, enumLogType.Response, enumLogType._1_BadRequest,
                                                         base.SerializedRequest,
                                                         "BadRequest -> lead: " + Newtonsoft.Json.JsonConvert.SerializeObject(lead),
                                                         "SHOULD NOT BE HERE",
                                                         true,
                                                         base.Refferer,
                                                         base.IP,
                                                         base.UA,
                                                         ref base.APICreateDate,
                                                         ref base.APICreateDate_ISR,
                                                         ref base.APITransaction, true, req.Email, base.ProjectType);

            #endregion

            return AppResponse.BadRequest(null);


        }

        public Result RL(Registered_SubmitLead req)
        {

            base.Init_Request_Data(enumAction.Lead, req, req.IP, req.UA, req.T);

            string exception = string.Empty;
            IPHolder cacheIP = null;
            bool TokenError = false;
            if (!AppManager.CampaignAlive(base.APICreateDate_ISR, base.ProjectType, ref base.APITransaction, req.P, out TokenError))
            {

                cacheIP = Cache_IP.IP_Get(req.IP, 1, out exception, ref base.APICreateDate_ISR, ref base.ProjectType);

                #region  LOG  if(TokenError)
                if (TokenError)
                {
                    UtilsDB.API_Log_Insert(this.Action, enumLogType.Response, enumLogType._1_PayloadError,
                                                                 base.SerializedRequest,
                                                                 "enumReturnStatus.Ends -> is TokenError: " + TokenError.ToString(),
                                                                 "_cacheIP: "+Newtonsoft.Json.JsonConvert.SerializeObject(cacheIP),
                                                                 true,
                                                                 base.Refferer,
                                                                 base.IP,
                                                                 base.UA,
                                                                 ref base.APICreateDate,
                                                                 ref base.APICreateDate_ISR,
                                                                 ref base.APITransaction, true, req.Email, base.ProjectType);

                    return AppResponse.Any(enumReturnStatus.PayloadError, null);
                }
                #endregion

                return AppResponse.Any(enumReturnStatus.Ends, null);
            }

            #region MyRegion VALIDATION

            if (!ModelState.IsValid || !base.ValidRequestFromServerIP || TokenError)
            {
                cacheIP = Cache_IP.IP_Get(req.IP, 1, out exception, ref base.APICreateDate_ISR, ref base.ProjectType);

                #region MyRegion LOG

                UtilsDB.API_Log_Insert(this.Action, enumLogType.Response, enumLogType._1_PayloadError,
                                                             base.SerializedRequest,
                                                             "base.ValidRequestFromServerIP IP:" + base.ServerIP + " IsValid:" + base.ValidRequestFromServerIP.ToString() + " -> TokenError(P): " + TokenError.ToString(),
                                                             Newtonsoft.Json.JsonConvert.SerializeObject(ModelState.Keys)+ " -> cacheIP: " + Newtonsoft.Json.JsonConvert.SerializeObject(cacheIP),
                                                             true,
                                                             base.Refferer,
                                                             base.IP,
                                                             base.UA,
                                                             ref base.APICreateDate,
                                                             ref base.APICreateDate_ISR,
                                                             ref base.APITransaction, true, req.Email, base.ProjectType);

                #endregion

                return AppResponse.Any(enumReturnStatus.PayloadError, null);
            }

            #endregion

            #region UNIQUE_Exist

            if (Cache_Unique.MAC_UNIQUE_IS_EXIST(ref base.APICreateDate, req.Email, ref base.ProjectType))
            {

                #region MyRegion LOG

                UtilsDB.API_Log_Insert(this.Action, enumLogType.Response, enumLogType._1_Unique_Not_Allowed,
                                                             base.SerializedRequest,
                                                             "_1_MAC_OR_Unique_Address_Not_Allowed",
                                                             null,
                                                             true,
                                                             base.Refferer,
                                                             base.IP,
                                                             base.UA,
                                                             ref base.APICreateDate,
                                                             ref base.APICreateDate_ISR,
                                                             ref base.APITransaction, true, req.Email, base.ProjectType);

                #endregion

                return AppResponse.Any(enumReturnStatus.Forbidden, null);
            }

            #endregion

            
            bool NeedCaptcha = false;

            #region REGINO CACHE IP AND IF CAPTCH IS NEEDED
            //ADD TO IP CALL
            cacheIP = Cache_IP.IP_Get(req.IP, 0, out exception, ref base.APICreateDate_ISR, ref base.ProjectType);

            if (cacheIP != null && cacheIP.need_Captcha() && string.IsNullOrEmpty(req.CAP))
            {
                NeedCaptcha = true;
                UtilsDB.API_Log_Insert(base.Action, enumLogType.Request, enumLogType._1_Need_Captcha, base.SerializedRequest, "NEED CAPTCHA -> Captch: " + Newtonsoft.Json.JsonConvert.SerializeObject(cacheIP), null, false,
                                                         base.Refferer, base.IP, base.UA, ref base.APICreateDate, ref base.APICreateDate_ISR,
                                                         ref base.APITransaction, true, req.Email, base.ProjectType);



                return AppResponse.Any(enumReturnStatus.Captcha, null);
            }
            #endregion

            #region REGION GOOGLE VERIFICATION
            string googleRequest = string.Empty, googleResponse = string.Empty;
            bool GoogleApproved = false;
            if (cacheIP != null && cacheIP.need_Captcha() && !string.IsNullOrEmpty(req.CAP))
            {
                googleResponse = UtilsWeb.MakeRequest_Google_Captch(req.CAP, req.IP, ref base.APITransaction, ref base.APICreateDate, ref base.APICreateDate_ISR,
                                                                   out googleRequest, ref base.ProjectType);


                GoogleApproved = googleResponse.ToLower().Contains("true");

                if (!GoogleApproved)
                {
                    NeedCaptcha = true;
                    UtilsDB.API_Log_Insert(base.Action, enumLogType.Request, enumLogType._1_Need_ReCaptcha,
                                                             "FAIL ON GOOGLE CAPTCHA: " + base.SerializedRequest,
                                                             googleRequest,
                                                             googleResponse,
                                                             false,
                                                             base.Refferer,
                                                             base.IP,
                                                             base.UA,
                                                             ref base.APICreateDate,
                                                             ref base.APICreateDate_ISR,
                                                             ref base.APITransaction,
                                                             true, req.Email, base.ProjectType);



                    return AppResponse.Any(enumReturnStatus.ReCaptcha, null);
                }
            }
            #endregion

            API_Project_GetResult project = AppManager.GetProject(base.ProjectType);

            API_Validate_Registered_UserResult user =
            UtilsDB.API_Validate_Registered_User(base.APITransaction, base.APICreateDate_ISR, req.Email, base.ProjectType);

            if (user == null)
            {
                Dictionary<string, dynamic> result = new Dictionary<string, dynamic>()
            {
                {"P",project.Token}
            };

                UtilsDB.API_Log_Insert(this.Action, enumLogType.Response, enumLogType._1_NeedRegistration,
                                                                 base.SerializedRequest,
                                                                 "Register REQUIRED UForbidden RESPONSE",
                                                                 null,
                                                                 true,
                                                                 base.Refferer,
                                                                 base.IP,
                                                                 base.UA,
                                                                 ref base.APICreateDate,
                                                                 ref base.APICreateDate_ISR,
                                                                 ref base.APITransaction, true, req.Email, base.ProjectType);

                return AppResponse.Any(enumReturnStatus.UForbidden, result);
            }

            SubmitLead demi = new SubmitLead()
            {
                Email = req.Email,
                IP = req.IP,
                CAP = req.CAP,
                P = req.P,
                T = req.T,
                UA = req.UA,
                FName =  "NA",
                LName = "NA",
                City =req.City,// "NA",
                Street =req.Street,// "NA",
                Phone =  "NA",
                FlatNumber=  "NA",
                STNumber=req.STNumber,// "NA"
                AcceptContent=true,  //req.AcceptContent,
                POBox=req.POBox,
                Regulation=true// req.Regulation
            };

            API_Lead_InsertResult lead =
            UtilsDB.API_Lead_Insert(ref demi, -1, ref base.APITransaction, req.CAP, 1, 
                                    ref base.APICreateDate, 
                                    ref base.APICreateDate_ISR, ref base.Platform, ref base.ProjectType);

            if (lead == null)
            {
                #region  LOG  

                UtilsDB.API_Log_Insert(this.Action, enumLogType.Response, enumLogType._1_InternalError,
                                                             base.SerializedRequest,
                                                             "API_Lead_Insert ERROR",
                                                             null,
                                                             true,
                                                             base.Refferer,
                                                             base.IP,
                                                             base.UA,
                                                             ref base.APICreateDate,
                                                             ref base.APICreateDate_ISR,
                                                             ref base.APITransaction, true, req.Email, base.ProjectType);

                #endregion

                return AppResponse.Any(enumReturnStatus.InternalError, null);
            }

            switch (lead.DBResult.GetValueOrDefault(-1000))
            {

                case -1000://LOGIC ERROR OR INTERNAL DVB ERROR
                    #region  LOG  

                    UtilsDB.API_Log_Insert(this.Action, enumLogType.Response, enumLogType._1_InternalError,
                                                                 base.SerializedRequest,
                                                                 "API_Lead_Insert ERROR",
                                                                 "lead.DBResult is null",
                                                                 true,
                                                                 base.Refferer,
                                                                 base.IP,
                                                                 base.UA,
                                                                 ref base.APICreateDate,
                                                                 ref base.APICreateDate_ISR,
                                                                 ref base.APITransaction, true, req.Email, base.ProjectType);

                    #endregion

                    return AppResponse.Any(enumReturnStatus.InternalError, null);
                    break;
                case 200://OK

                    string ex = null;
                    Cache_IP.AddUpdate_IP(req.IP, new IPHolder(null, req.IP, lead.Count_IP.GetValueOrDefault(), lead.MaxAllowed_IP.GetValueOrDefault(), lead.ExpiredIPAt.GetValueOrDefault()), out ex, ref base.ProjectType);


                    //DataProResult result = UtilsDataPro.SendCouponEmail(req.Email, ref project);                    
                    //API_Lead_Update_CouponMSGResult coupon =
                    //UtilsDB.API_Lead_Update_CouponMSG(ref base.APITransaction, ref base.APICreateDate, ref base.APICreateDate_ISR, lead.LeadID.GetValueOrDefault(),
                    //                                  Newtonsoft.Json.JsonConvert.SerializeObject(result), result.isSuccess());

                    #region  LOG  

                    UtilsDB.API_Log_Insert(this.Action, enumLogType.Response, enumLogType.Response,
                                                                         base.SerializedRequest,
                                                                         null/*Newtonsoft.Json.JsonConvert.SerializeObject(result)*/,
                                                                        "lead: " + Newtonsoft.Json.JsonConvert.SerializeObject(lead) /*+ " -> coupon update: " + Newtonsoft.Json.JsonConvert.SerializeObject(coupon)*/,
                                                                         true,
                                                                         base.Refferer,
                                                                         base.IP,
                                                                         base.UA,
                                                                         ref base.APICreateDate,
                                                                         ref base.APICreateDate_ISR,
                                                                         ref base.APITransaction, true, req.Email, base.ProjectType);

                            #endregion

                            return AppResponse.OK(null);

                             
                   

                    break;
            }

            #region  LOG  

            UtilsDB.API_Log_Insert(this.Action, enumLogType.Response, enumLogType._1_BadRequest,
                                                         base.SerializedRequest,
                                                         "BadRequest -> lead: " + Newtonsoft.Json.JsonConvert.SerializeObject(lead),
                                                         "SHOULD NOT BE HERE",
                                                         true,
                                                         base.Refferer,
                                                         base.IP,
                                                         base.UA,
                                                         ref base.APICreateDate,
                                                         ref base.APICreateDate_ISR,
                                                         ref base.APITransaction, true, req.Email, base.ProjectType);

            #endregion

            return AppResponse.BadRequest(null);


        }

    }
}
