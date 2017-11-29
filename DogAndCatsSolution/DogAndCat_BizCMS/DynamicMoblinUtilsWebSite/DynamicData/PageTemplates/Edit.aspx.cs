using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web.DynamicData;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;

public partial class Edit : System.Web.UI.Page
{
    protected MetaTable table;

    protected void Page_Init(object sender, EventArgs e)
    {

        object currentuser = Session["currentuser"];
        if (currentuser == null || (string)currentuser == "Unknown") { Session["usertype"] = null; Session["currentuser"] = null; Response.Redirect("~/login.aspx"); }

        table = DynamicDataRouteHandler.GetRequestMetaTable(Context);
        FormView1.SetMetaTable(table);

        DetailsDataSource.EntityTypeName = table.EntityType.AssemblyQualifiedName;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Title = table.DisplayName;
    }

    protected void FormView1_ItemCommand(object sender, FormViewCommandEventArgs e)
    {
        if (e.CommandName == DataControlCommands.CancelCommandName)
        {
            Response.Redirect(table.ListActionPath);
        }
         
    }

    protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    { 
        if (e.Exception == null || e.ExceptionHandled)
        {
            int G = 0;
            //table = DynamicDataRouteHandler.GetRequestMetaTable(Context);
            //DataKey k = FormView1.DataKey;
            //DynamicDBDataContext db;
            //switch (table.ToString().ToLower())
            //{
            //    case "campaignhtmls":
            //        db = new DynamicDBDataContext();
            //        db.MBZ_CampaignHTMLUpdate((int)k.Value);

            //        break;

            //    case "campaigns":
            //        db = new DynamicDBDataContext();
            //        db.MBZ_CampaignUpdate((int)k.Value);

            //        break;

            //    case "pageimgs":
            //        db = new DynamicDBDataContext();
            //        db.MBZ_PageIMGUpdate((int)k.Value);

            //        break;

            //    case "pages":
            //        db = new DynamicDBDataContext();
            //        db.MBZ_PageUpdate((int)k.Value);

            //        break;
 

            //}

            Response.Redirect(table.ListActionPath);

        }


    }
 
 
    protected void DetailsDataSource_Updated(object sender, LinqDataSourceStatusEventArgs e)
    {
        //SHURON UPDATE HERE SPECIALIZED
        if (e.Exception == null)
        {

            //if (e.Result is FindWinner)
            //{
            //    object currentuser = (object)Session["currentuser"];

            //    string username = currentuser == null ? "Why Null" : (string)currentuser;

            //    UtilsDB.CMS_Winner_Insert(((FindWinner)e.Result).ID, username);
            //}

            //if (e.Result is Find_Big)
            //{

            //    UtilsDB.CMS_Winner_Insert(((Find_Big)e.Result).ID, true);
            //}
            //else
            //if (e.Result is Find_Small)
            //{
            //    UtilsDB.CMS_Winner_Insert(((Find_Small)e.Result).ID, false);
            //}
        }
    }
}
