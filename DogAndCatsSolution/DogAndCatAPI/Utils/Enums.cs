using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 
namespace DogAndCatAPI.Utils
{

    /// <summary>
    /// Dog=1,Cat=3
    /// </summary>
    public enum enumPet_Project { Dog=1,Cat=3}

    public enum enumUserType {Unknown=-1, Client=0, Banker=1 }
    public enum enumVoteType { Offer = 1, Vote = 2 }
    public enum enumGenderType { Female=0, Male=1}


    public enum enumPlatformType
    {
        Unknown = 0, iPhone = 1, Android = 2, IPad = 3, Tablet = 4, Web = 5
    }

    public enum enumProject { APIGeneral = -3,   Init = -1, Dog=1, Cat = 2 }

    public enum enumAction
    {
        Init = -1,
        LocalCache = -2,
        APIGeneral = -3,
        GoogleCaptcha = -4,
        RefreshToken=-10,

         Lead=1,
         RegistersLead = 3,

         IsAlive =2,

         Third_Party_Web_Service=10,

        SMSSend = 20,
        QHandler = 21
    }

    public enum enumLogType
    {
        _1_BadRequest=-25,
    
        _1_Try_Again  =-23,
        _1_Unique_Not_Allowed =-22,
        _1_Google_Captcha_Fail = -21,
        _1_Need_ReCaptcha = -20,
        _1_Need_Captcha = -19,
        _1_NoMoreCodeToday = -18,
        _1_NeedRegistration=-17,


        _1_InvalidDataRequest =-13,
        _1_SMS_Fail = -12,
        _1_DB_Configuration_Error = -11,
        _1_RoleEnvironment_GetConfigurationSettingValue = -10,
        _1_Suspend = -9,
      
        _1_InternalError = -7,
        _1_DBLogicError = -6,
        _1_PayloadError = -5,
        _1_WebException = -4,
        _1_Fail = -3,
        _1_DBException = -2,
        _1_Exception = -1,
        Success_1 = 1,
        Register_2 = 2,
        Tracking_3 = 3,
       
        Request = 9,
        Response = 10,
        Start = 20,
        End = 21,
        OK_200 = 200
    }

    /// <summary>
    /// SPLogicError= -1000, SuspendMode = -10, ErrorCode = -20, OK = 200
    /// </summary>
    public enum enumHolidays_Register_DBResult { SPLogicError = -1000, SuspendMode = -10, ErrorCode = -20, NoMoreToday = -111, Force_Need_Captcha_AsError = -4, OK = 200 }

}