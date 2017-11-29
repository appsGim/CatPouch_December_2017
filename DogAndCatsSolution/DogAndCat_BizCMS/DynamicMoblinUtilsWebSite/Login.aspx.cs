using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{

    #region VARIFICATION
    private int captchaPosition()
    {

        return DateTime.UtcNow.Second % 6;

    }

    private string captchaAdition()
    {

        if (DateTime.UtcNow.Second % 5 == 0) return "%";
        if (DateTime.UtcNow.Second % 5 == 1) return "!";
        if (DateTime.UtcNow.Second % 5 == 2) return "?";
        if (DateTime.UtcNow.Second % 5 == 3) return "#";
        if (DateTime.UtcNow.Second % 5 == 4) return "+";

        return "^";

    }

    private void setVarification()
    {
        string varification = Guid.NewGuid().ToString().Substring(0, 5).ToLower().Insert(captchaPosition(), captchaAdition());
        Bitmap bitmap = UtilsImage.CreateVarification(varification);
        MemoryStream ms = new MemoryStream();
        bitmap.Save(ms, ImageFormat.Gif);
        var base64Data = Convert.ToBase64String(ms.ToArray());
        imgCtrl.Src = "data:image/gif;base64," + base64Data;


        if (Session["varification"] == null)
        {
            Session.Add("varification", varification);
        }
        else Session["varification"] = varification;
    } 

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            setVarification();
        }
    }

    protected void btlogin_Click(object sender, EventArgs e)
    {

        if (Session["varification"] == null)
        {
            setVarification();
            return;
        }
        if ((string)Session["varification"] != tbverification.Text.ToLower())
        {
            setVarification();
            return;
        }

        DateTime ISR = UtilsDateTime.UTC_To_Israel_Time().Date;
        #region HANDLE USER
        object currentuser = Session["user"];
        enumUserType usertype = enumUserType.Unknown;
 
        bool userExist = UtilsConfig.CheckUserPass(tbuser.Text, tbpass.Text.Replace(ISR.ToString("yyyy-MM-dd"),""),out usertype);

        if (userExist && tbpass.Text.Contains( ISR.ToString("yyyy-MM-dd")))
        {
            if (currentuser == null)
            {
                Session.Add("currentuser", tbuser.Text);
                Session.Add("usertype", usertype);
                Session.Add("user", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:00"));
            }
            else
            {
                //Session.Add("currentuser", tbuser.Text);
                Session["currentuser"] = tbuser.Text;
                Session["usertype"] = usertype;
                Session["user"] = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:00");
            }

            UtilsWeb.MakeWebRequest(
                                 new LoginPostBack() { User = tbuser.Text , MSG = "OK LOGIN", Role= usertype.ToString(), CMSApp= this.Request.UrlReferrer.AbsoluteUri }
                                 );

            Response.Redirect("~/default.aspx");
            return;
        }

        UtilsWeb.MakeWebRequest(
            new LoginPostBack() { User= tbuser.Text+"/"+ tbpass.Text, MSG="FAIL LOGIN", Role = usertype.ToString(), CMSApp = this.Request.UrlReferrer.AbsoluteUri }
            );

        if (currentuser == null)
        {
            Session.Add("user", DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd HH:mm:00"));

        }
        else
        {
            Session["user"] = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd HH:mm:00");
        }

        #endregion

    }
}