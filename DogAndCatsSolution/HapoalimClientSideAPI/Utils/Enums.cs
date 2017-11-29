using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HapoalimAPI.Utils
{
    public enum enumPlatformType
    {
        Unknown = 0, iPhone = 1, Android = 2, IPad = 3, Tablet = 4, Web = 5
    }

    public enum enumProjectType { APIGeneral = -3, WorkerLogger = -2, Init = -1, Hapoalim = 2 }

    public enum enumAction
    {
        Init = -1,
        LocalCache = -2,
        APIGeneral = -3,
        GoogleCaptcha = -4,
        Hapoalim_Register = 10,
        Hapoalim_ViewVotes = 11,
        Hapoalim_ViewProposal = 15,
        SMSSend = 20,
        QHandler = 21,
        Hapoalim_AllLeads = 26
    }

    public enum enumLogType
    {
        _1_Google_Captcha_Fail = -21,
        _1_Need_ReCaptcha = -20,
        _1_Need_Captcha = -19,
        _1_NoMoreCodeToday = -18,
        _1_TooEarly = -17,
        _1_Q_Error = -16,
        _1_RejectPhoneRegistration = -15,
        _1_GameNotFound = -14,
        _1_CloseGame_IllegalAction = -13,
        _1_SMS_Fail = -12,
        _1_DB_Configuration_Error = -11,
        _1_RoleEnvironment_GetConfigurationSettingValue = -10,
        _1_SuspendPhone = -9,
        _1_CodeError = -8,
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
        //SMS_4 = 4,
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