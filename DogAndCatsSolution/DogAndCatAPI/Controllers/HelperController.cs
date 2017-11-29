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

 
namespace DogAndCatAPI.Controllers
{

    public class HelperController : BaseController
    {

        [HttpGet]
        public Result input(string key, bool nd, int? p = null)//(string key)
        {

            if (string.IsNullOrEmpty(key) || key != "moblinMNG" + UtilsDateTime.UTC_To_Israel_Time().ToString("yyyy-MM-dd")) return null;

            if (p == null) p = 1;

            API_Project_GetResult project = AppManager.GetProject(enumProject.Cat);

            //PetOut pet = AppManager.PetProjectGet(ref base.APICreateDate_ISR, (enumProject)p).FirstOrDefault();

            string tokens =  "cats: " + AppManager.Cat_ServerToken.ToString();
            Dictionary<string, dynamic> data = AppManager.get_data_helper(base.APICreateDate_ISR);
            in_isAlive isalive = new in_isAlive() { IP = "12.12.12.12", ND = true, UA = "some user aget", T =  AppManager.Cat_ServerToken };

            SubmitLead lead = new SubmitLead()
            {
                AcceptContent = true,
                Regulation = true,
                CAP = null,
                City = "רמת השרון",
                Email = "sharonb@moblin.com",
                FlatNumber = "12",
                FName = "moblin",
                IP = "12.12.12.12",
                LName = "test",
                Phone = "0000000000",
                POBox = "PO BOXS",
                STNumber = "1",
                Street = "STREET",
                UA = "SOME USER AGENT",
                P = project.Token,
                T =  AppManager.Cat_ServerToken//,
                //PetType = pet.T

            };





            Registered_SubmitLead rl = new Registered_SubmitLead()
            {
                IP = "12.12.12.12",
                UA = "some user aget",
                Email = "email",
                T = AppManager.Cat_ServerToken,
                P = project.Token
            };

            return AppResponse.OK(new Dictionary<string, dynamic>()
            {
                {"static_tokens",str( tokens)},
                {"a_as_is_alive",str( isalive) },
                {"l_as_submit_lead", str( lead)},
                {"rl_as_registered_submit_lead",str( rl) },
                { "data",str( data)}
            });
        }

        private string str(object str)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(str);
        }

        [HttpGet]
        public Result ido(string key)//(string key)
        {

            if (string.IsNullOrEmpty(key) || key != "moblinMNG" + UtilsDateTime.UTC_To_Israel_Time().ToString("yyyy-MM-dd")) return null;



            AppManager.cleanhelper();




            return AppResponse.OK(new Dictionary<string, dynamic>()
            {
                {"cleand",AppManager.cleanhelper()}
            });
        }
         

        [HttpGet]
        public Result MSG(string key, string email)
        {

            if (string.IsNullOrEmpty(key) || key != "moblinxMNG" + UtilsDateTime.UTC_To_Israel_Time().ToString("yyyy-MM-dd")) return null;

            API_Project_GetResult project = AppManager.GetProject(enumProject.Dog);
            DataProResult _result = UtilsDataPro.SendCouponEmail(email, ref project);

            //return AppResponse.OK(_result);




       //     SendMsgResults RESULTMSG = null;
            //SendMsgServiceClient client = new SendMsgServiceClient();

            //SendMsgService service = new il.co.sendmsg.api3.SendMsgService();

             

            // service.se



            //SendMsgUser USER = new SendMsgUser() { EmailAddress = "sharonb@moblin.com" };//, UserIDSpecified=false
            //SendMsgMessage MSG = new SendMsgMessage()
            //{
            //    SenderEmailAddress = "info@moblin.com",
            //    MessageType = SendMsgMessageType.MailMessage,
            //    MessageID = int.Parse(project.Coupon_EmailID)
            //    //UseTemplateID = int.Parse(project.Coupon_EmailID)
            //};
            //SendMsgUser[] _u = new SendMsgUser[1] { USER };

            //SendMsgList msglist = new SendMsgList();
            //msglist.ExistingListID = project.Coupon_GroupID.GetValueOrDefault();

            //SendMsgList[] _msglist = new SendMsgList[1] { msglist };

            ////MSG.MessageContent = "";
            ////MSG.MessageSubject = "";
            ////MSG.MessageInnerName = "";

             
            //SendMsgResults result;

            //SendMsgServiceClient service = new SendMsgServiceClient();
            
            //service.AddUsersAndSend(project.Coupon_SiteID.GetValueOrDefault(),
            //                          project.Coupon_APIPassword,
            //                        _u, MSG,
            //                         out result);

           
             
            return AppResponse.OK(_result);
        }

    }
}
