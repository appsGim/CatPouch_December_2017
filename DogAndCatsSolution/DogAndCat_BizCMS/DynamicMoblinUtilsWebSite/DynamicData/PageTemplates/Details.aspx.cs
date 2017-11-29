using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;

public partial class Details : System.Web.UI.Page {
    protected MetaTable table;

    protected void Page_Init(object sender, EventArgs e)
    {

        object currentuser = Session["currentuser"];
        if (currentuser == null || (string)currentuser == "Unknown") { Session["usertype"] = null; Session["currentuser"] = null; Response.Redirect("~/login.aspx"); }

        table = DynamicDataRouteHandler.GetRequestMetaTable(Context);

        object current = Session["usertype"];
        if (current == null || (enumUserType)current == enumUserType.Unknown)
        {
            Session["user"] = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd HH:mm:00");
            Response.Redirect("~/login.aspx");
            return;
        }

        System.Web.HttpRequest h = this.Request;

        if ((enumUserType)current == enumUserType.Users)
        {
            if (!UtilsConfig.UserTables.Contains(table.Name.ToLower()))
            {
                Response.Redirect(this.Request.UrlReferrer.PathAndQuery);
                return;
            }
        }

        FormView1.SetMetaTable(table);
        DetailsDataSource.EntityTypeName = table.EntityType.AssemblyQualifiedName;
    }

    protected void Page_Load(object sender, EventArgs e) {
        Title = table.DisplayName;
        
    }

    protected void FormView1_ItemDeleted(object sender, FormViewDeletedEventArgs e) {
        if (e.Exception == null || e.ExceptionHandled) {
            Response.Redirect(table.ListActionPath);
        }
    }

 
    protected void FormView1_ItemCommand(object sender, FormViewCommandEventArgs e)
    {
        if (e.CommandName == DataControlCommands.CancelCommandName)
        {
            Response.Redirect(table.ListActionPath);
        }


    }

    //protected void RejectDelete_DataBinding(object sender, EventArgs e)
    //{
    //    if (UtilsConfig.RejectDelete(Title))
    //    {
    //        ((LinkButton)sender).Visible = false;
    //    }
    //}
}
