using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogAndCatAPI.Utils
{
    public static class UtilsException
    {
        public static string GetMSG(ref Exception ex)
        {
            return ex.Message + (ex.InnerException == null ? " -> Inner null" : " -> inner: " + ex.InnerException.Message);
        }
    }
}