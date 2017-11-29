using System.Collections.Generic;
using System;
using System.Linq;

public enum enumConfigKeys
{
    Storage_AccountName,
    Storage_AccountKey,
    Storage_AccountKeySecondary,
    Storage_QueueUri,
    Storage_TableUri,
    Storage_BlobUri,
    Storage_Container,

    ImageKeepFullURL,
    VERSION,
    SessionTimeAsMinutes,
    Users,
    UserTables,
    Admins,
    AdminTables,
    Developers,
    SystemViews,
    RejectDeleteTables,

    DynamicCMSMoblinUtilsKey,
    LoginPostBackEnabled,
    LoginPostBackURL,
    RemoveRejectedNewsletter
}
 

public enum enumUserType {Unknown, Users, Admins, Developers }

public static class UtilsConfig
{
    public static bool ImageKeepFullURL = Get(enumConfigKeys.ImageKeepFullURL).Equals("true");
    public static string VERSION =  Get(enumConfigKeys.VERSION);
    public static int SessionTimeAsMinutes=int.Parse( Get(enumConfigKeys.SessionTimeAsMinutes));

    public static volatile Dictionary<string, string> Users = new Dictionary<string, string>();
    public static volatile Dictionary<string, string> Admins = new Dictionary<string, string>();
    public static volatile Dictionary<string, string> Developers = new Dictionary<string, string>();

    public static volatile List<string> UserTables = Get(enumConfigKeys.UserTables).Split(',').ToList();
    public static volatile List<string> AdminTables = Get(enumConfigKeys.AdminTables).Split(',').ToList();

    //private static volatile List<string> RejectDeleteTables = Get(enumConfigKeys.RejectDeleteTables).Split(',').ToList();

    private static DateTime LastUpdate = new DateTime(DateTime.UtcNow.AddDays(-1).Ticks);

    private static volatile List<string> SystemViews = Get(enumConfigKeys.SystemViews).Split(',').ToList();

    public static bool isViewTable(string tableName)
    {
        if (tableName == null) return true;
        Update();
        return SystemViews.Contains(tableName);
    }

    //public static bool RejectDelete(string tableName)
    //{
    //    Update();

    //    return RejectDeleteTables.Contains(tableName);
    //}

    public static bool CheckUserPass(string user, string pass, out enumUserType type)
    {
        Update();
        type = enumUserType.Unknown;
        Dictionary<string, string> temp;

        if (Users.ContainsKey(user))
        {
            temp = Users;
            type = enumUserType.Users;
        }
        else if (Admins.ContainsKey(user))
        {
            temp = Admins;
            type = enumUserType.Admins;
        }
        else if (Developers.ContainsKey(user))
        {
            temp = Developers;
            type = enumUserType.Developers;
        }
        else return false;

        if (temp[user] != pass) { type = enumUserType.Unknown; return false; }

        return true;
    }

    public static void Update()
    {
        if (LastUpdate.AddMinutes(10) < DateTime.UtcNow || Users.Count == 0 || Admins.Count==0 || Developers.Count==0)
        {
            SessionTimeAsMinutes = int.Parse(Get(enumConfigKeys.SessionTimeAsMinutes));
            VERSION = Get(enumConfigKeys.VERSION);
            ImageKeepFullURL = Get(enumConfigKeys.ImageKeepFullURL).Equals("true");
            UserTables = Get(enumConfigKeys.UserTables).Split(',').ToList();
            SystemViews = Get(enumConfigKeys.SystemViews).Split(',').ToList();
            //RejectDeleteTables = Get(enumConfigKeys.RejectDeleteTables).Split(',').ToList();

            for (int i = 0; i <= UserTables.Count - 1; i++)
            {
                UserTables[i] = UserTables[i].ToLower();
            }

            #region MyRegion USERS
            string[] users = Get(enumConfigKeys.Users).Split(',');
            Dictionary<string, string> _users = new Dictionary<string, string>();
            foreach (string s in users.ToList())
            {
                string[] userpass = s.Split(':');
                if (userpass.Count() == 2 && !_users.ContainsKey(userpass[0]))
                    _users.Add(userpass[0], userpass[1]);

            }

            Users = _users;
            #endregion

            #region MyRegion ADMINS

            string[] admins = Get(enumConfigKeys.Admins).Split(',');
            Dictionary<string, string> _admins = new Dictionary<string, string>();
            foreach (string s in admins.ToList())
            {
                string[] userpass = s.Split(':');
                if (userpass.Count() == 2 && !_admins.ContainsKey(userpass[0]))
                    _admins.Add(userpass[0], userpass[1]);

            }

            Admins = _admins;
            #endregion

            #region MyRegion Developers

            string[] developers = Get(enumConfigKeys.Developers).Split(',');
            Dictionary<string, string> _developers = new Dictionary<string, string>();
            foreach (string s in developers.ToList())
            {
                string[] userpass = s.Split(':');
                if (userpass.Count() == 2 && !_developers.ContainsKey(userpass[0]))
                    _developers.Add(userpass[0], userpass[1]);

            }

            Developers = _developers;
            #endregion

            LastUpdate = new DateTime(DateTime.UtcNow.Ticks);
        }
    }

    public static string Get(enumConfigKeys key)
    {

        return System.Web.Configuration.WebConfigurationManager.AppSettings[key.ToString()];

    }

    public static string AzureStorage_BaseURL()
    {
        return Get(enumConfigKeys.Storage_BlobUri) + Get(enumConfigKeys.Storage_Container);
    }
}