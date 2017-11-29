using System;
using System.Drawing;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SMSApp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        object usertime = Session["user"];
        object currentuser = Session["currentuser"];

        object usertype = Session["usertype"];

        #region HANDLE USER

        if (usertime == null || usertype == null || currentuser == null)
        {
            Session.Add("user", DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd HH:mm:00"));
            Response.Redirect("~/login.aspx");
            return;
        }

        if ((string)currentuser == "Unknown") { Session["usertype"] = null; Session["currentuser"] = null; Response.Redirect("~/login.aspx"); }

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

        #region REGION SMS CHECK

        if (!ConfigurationManager.AppSettings["UtilsSMSEnabled"].ToString().Equals("true") || ConfigurationManager.AppSettings["UtilsSMSKey"].ToString() == string.Empty)
        {
            Session["user"] = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd HH:mm:00");
            Response.Redirect("~/login.aspx");
            return;
        }

        #endregion
    }

    protected void btnSend_Click(object sender, EventArgs e)
    { 
        object currentuser = Session["currentuser"];
        object usertime = Session["user"];
        object usertype = Session["usertype"];

        if ((string)currentuser == null || ((string)currentuser).Equals("Unknown") || usertime==null || usertype==null)
        {
            Session.Add("user", DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd HH:mm:00"));
            Response.Redirect("~/login.aspx");
            return;
        }

        DateTime now = DateTime.UtcNow.AddDays(-1);

        if (!DateTime.TryParseExact((string)usertime, "yyyy-MM-dd HH:mm:00", null, System.Globalization.DateTimeStyles.AssumeUniversal, out now))
        {
            Session["user"] = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd HH:mm:00");
            Response.Redirect("~/login.aspx");
            return;
        }

        tbPhone.BackColor = Color.White;
        tbText.BackColor = Color.White;
        if (!UtilsRegex.CheckPhone(tbPhone.Text.Trim()))
        {
            tbResult.Text = "Only numbers accepted!";
            tbPhone.BackColor = Color.Red;
            return;
        }

        if (tbText.Text.Trim().Equals(string.Empty))
        {
            tbResult.Text = "Text is missing";
            tbText.BackColor = Color.Red;
            return;
        }

        //object currentuser = (object)Session["currentuser"];

        string result = UtilsWeb.SendUtilsSMS(tbText.Text, tbPhone.Text.Trim(),(string)currentuser, this.Request.UrlReferrer.AbsoluteUri);

        UtilsWeb.MakeWebRequest(
            new LoginPostBack() { User = (string)currentuser, MSG = "SendSMS: "+ result, Role = ((enumUserType)usertype).ToString(), CMSApp = this.Request.UrlReferrer.AbsoluteUri }
            );

     

        tbResult.Text = "Send to: "+ tbPhone.Text + Environment.NewLine + "Result: "+ result;
        tbPhone.Text = string.Empty;
    }

    
}