using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web.UI.WebControls;
using System;
using System.Configuration;

public partial class Site : System.Web.UI.MasterPage {


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            object currentuser = (object)Session["currentuser"];


            #region REGION SMS

            if (!ConfigurationManager.AppSettings["UtilsSMSEnabled"].ToString().Trim().Equals("true") || ConfigurationManager.AppSettings["UtilsSMSKey"].ToString().Trim() == string.Empty)
            {
                sms.Visible = false;
            }

            #endregion


            if (currentuser != null)
            {

                if ((string)currentuser == "Unknown") { Session["usertype"] = null; Session["currentuser"] = null; Response.Redirect("~/login.aspx"); }
                 
                lbUser.Text ="Hello "+ (string)currentuser + " "+ UtilsDateTime.UTC_To_Israel_Time().ToString("dd/MM/yyyy HH:mm");
            }
        }

   
    }

    protected void btlogout_Click(object sender, System.EventArgs e)
    {
        object currentuser = (object)Session["currentuser"];
        object usertype =   (object)Session["usertype"];


        UtilsWeb.MakeWebRequest(
           new LoginPostBack() { User = currentuser==null? "NULL ? ": (string)currentuser, MSG = "LOGOUT", Role = usertype==null? enumUserType.Unknown.ToString(): ((enumUserType)usertype).ToString(),
                               CMSApp = this.Request.UrlReferrer.AbsoluteUri }
           );

        Session["varification"] = null;
        Session["user"] = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd HH:mm:00") ;
        Response.Redirect("~/login.aspx");
    }
}
