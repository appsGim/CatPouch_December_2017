using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Linq;
using System.Collections;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         
        object usertime = Session["user"];
        object currentuser= Session["currentuser"];
        
        object usertype = Session["usertype"];

        #region HANDLE USER

        if (usertime == null || usertype==null || currentuser==null)
        {
            Session.Add("user", DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd HH:mm:00"));
            Response.Redirect("~/login.aspx");
            return;
        }
        
        if ((string)currentuser == "Unknown") { Session["usertype"] = null; Session["currentuser"]=null; Response.Redirect("~/login.aspx"); }

        DateTime now = DateTime.UtcNow.AddDays(-1);

        if (!DateTime.TryParseExact((string)usertime, "yyyy-MM-dd HH:mm:00", null, System.Globalization.DateTimeStyles.AssumeUniversal, out now))
        {
            Session["user"] = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd HH:mm:00");
            Response.Redirect("~/login.aspx");
            return;
        }

        if (now.AddMinutes(UtilsConfig.SessionTimeAsMinutes) < DateTime.UtcNow)
        {
            Session["user"] = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd HH:mm:00");
            Response.Redirect("~/login.aspx");
            return;
        }

        #endregion

        enumUserType currentusertype = (enumUserType)Session["usertype"];

        //System.Collections.IList visibleTables = ASP.global_asax.DefaultModel.VisibleTables;
        List<MetaTable> visibleTables = ASP.global_asax.DefaultModel.VisibleTables;
        if (visibleTables.Count == 0)
        {
            throw new InvalidOperationException("There are no accessible tables. Make sure that at least one data model is registered in Global.asax and scaffolding is enabled or implement custom pages.");
        }

        switch (currentusertype)
        {
            case enumUserType.Developers:
           
                break;

            case enumUserType.Admins:
                
                if(UtilsConfig.AdminTables.Count>0 && UtilsConfig.AdminTables[0]!=string.Empty)
                visibleTables = (from vt in visibleTables where UtilsConfig.AdminTables.Contains(((MetaTable)vt).Name.ToLower()) select vt).ToList();
         
                break;

            case enumUserType.Users:
                if(UtilsConfig.UserTables.Count>0 && UtilsConfig.UserTables[0] != string.Empty)
                visibleTables = (from vt in visibleTables where UtilsConfig.UserTables.Contains(((MetaTable)vt).Name.ToLower()) select vt).ToList();
         
                break;

            default:
                {
                    Session["user"] = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd HH:mm:00");
                    Response.Redirect("~/login.aspx");
                    return;
                }break;
        }


        Menu1.DataSource = visibleTables;
        Menu1.DataBind();
    }
 
}
