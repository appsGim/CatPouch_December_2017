using System;

namespace HapoalimAPI.Models
{
    /// <summary>
	/// OK = 200, BadRequest = 400, Forbidden = 403, PayloadError = 414, ValidationError = 421, ValidationDataError = 424, InternalError = 500
	/// </summary>
	public enum enumReturnStatus
    {
        OK = 200,
        Ended = 201,
        Captcha = 202,
        ReCaptcha = 203,
        BadRequest = 400,
        TryAgain = 401,
        UnavailableNow = 402,
        Forbidden = 403,
        PayloadError = 414,
        ValidationError = 421,
        CodeError = 424, // קוד לא תקין
        GameError = 425,
        InternalError = 500,
        ServerTokenIsNotMatch = 911
    }

    /// <summary>
    /// enumReturnStatus Status; string Message
    /// </summary>
    public class Meta
    {
        public enumReturnStatus Status { get; set; }
        public string Message { get; set; }

        public Meta(enumReturnStatus _status, string _message/*, string timestamp*/)//, List<MSG> _Masseges
        {
            Status = _status;
            Message = _message;
        }
    }

    /// <summary>
    /// Meta Meta; object Response
    /// </summary>
    public class Result
    {
        public Meta Meta { get; set; }
        public object Response { get; set; }

        public Result(Meta _meta, object _response)
        {
            Meta = _meta;
            Response = _response;
        }
    }

    public static class AppResponse
    {
        public static Result OK(object ob)
        {
            return new Result(new Meta(enumReturnStatus.OK, "OK"), ob);
        }

        public static Result BadRequest(object ob)
        {
            return new Result(new Meta(enumReturnStatus.BadRequest, "BadRequest"), ob);
        }

        public static Result Any(enumReturnStatus ReturnStatus, object ob)
        {
            return new Result(new Meta(ReturnStatus, ReturnStatus.ToString()), ob);
        }
    }
}