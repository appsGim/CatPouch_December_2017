using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Net;

namespace HapoalimAPI.Utils
{
    public static class UtilsDB
    {
        private static string _Connection { get; set; }

        public static string Connection()
        {
            if (_Connection == null) _Connection = ConfigurationManager.ConnectionStrings["BETA_ConnectionString"].ConnectionString;
            return _Connection;
        }
    }
}