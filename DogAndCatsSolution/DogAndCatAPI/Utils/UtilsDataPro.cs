using DogAndCatAPI.DAL;
using System;

using DogAndCatAPI.il.co.sendmsg.api3;

namespace DogAndCatAPI.Utils
{
    public class DataProResult
    {
        public SendMsgResults result { get; set; }
        public int CodeResult { get; set; }

        public string exMSG { get; set; }

        public bool add_and_send { get; set; }

        public bool add_and_send_specified { get; set; }

        public DataProResult() { }

        /// <summary>
        /// 200 IS OK
        /// </summary>
        /// <returns></returns>
        public bool isSuccess()
        {

            return result != null && CodeResult == 200 ;
        }
    }
    public static class UtilsDataPro
    {
        //private static SendMsgService service = null;

        public static DataProResult SendCouponEmail(string email, ref API_Project_GetResult project)
        {
            SendMsgService service = new il.co.sendmsg.api3.SendMsgService();
            SendMsgResults result=null;
             
            try
            {
                SendMsgUser USER = new SendMsgUser() { EmailAddress = email };
                SendMsgMessage MSG = new SendMsgMessage()
                { 
                    MessageType = SendMsgMessageType.MailMessage,
                    MessageID = int.Parse(project.Coupon_EmailID)
                };
                SendMsgUser[] _u = new SendMsgUser[1] { USER };

                SendMsgList msglist = new SendMsgList();
                msglist.ExistingListID = project.Coupon_GroupID.GetValueOrDefault();

                SendMsgList[] _msglist = new SendMsgList[1] { msglist };
                MSG.MessageIDSpecified = true;
               
                bool add_users_and_send_result=false;
                bool add_users_and_send_specified = false; ;
              
                service.AddUsersAndSend(project.Coupon_SiteID.GetValueOrDefault(),
                                        true, project.Coupon_APIPassword,
                                        _u, MSG,
                                        out add_users_and_send_result,
                                        out add_users_and_send_specified, out result);

                return new DataProResult() { CodeResult = result.ResultID,
                                             result = result,
                                             add_and_send = add_users_and_send_result,
                                             add_and_send_specified = add_users_and_send_specified };

            }
            catch (Exception ex)
            {

                return new DataProResult() { CodeResult = -1, exMSG = UtilsException.GetMSG(ref ex), result = result };
            }
 
        }

    }
}