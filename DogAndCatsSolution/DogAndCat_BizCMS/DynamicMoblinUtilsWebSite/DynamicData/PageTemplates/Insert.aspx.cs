using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web.DynamicData;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;
using System.Linq;
using System.Collections.Specialized;
using System.Collections;

public partial class Insert : System.Web.UI.Page {
    protected MetaTable table;

    protected void Page_Init(object sender, EventArgs e) {

        object currentuser = Session["currentuser"];
        if (currentuser == null || (string)currentuser == "Unknown") { Session["usertype"] = null; Session["currentuser"] = null; Response.Redirect("~/login.aspx"); }

        table = DynamicDataRouteHandler.GetRequestMetaTable(Context);
       
        FormView1.SetMetaTable(table, table.GetColumnValuesFromRoute(Context));
        DetailsDataSource.EntityTypeName = table.EntityType.AssemblyQualifiedName;
         
    }

    protected void Page_Load(object sender, EventArgs e) {
        Title = table.DisplayName;
         
    }

    protected void FormView1_ItemCommand(object sender, FormViewCommandEventArgs e) {
        if (e.CommandName == DataControlCommands.CancelCommandName) {
            Response.Redirect(table.ListActionPath);
        }
          
    }

    protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e) {
        if (e.Exception == null || e.ExceptionHandled)
        {
            
            #region MyRegion NOTES FOR OTHER PROJECTS POSSIBLITIES TO UPDATE IF NEEDED
            //table = DynamicDataRouteHandler.GetRequestMetaTable(Context);
            //DynamicDBDataContext db;
            //switch (table.ToString().ToLower())
            //{


            //    case "campaignhtmls":
            //        db = new DynamicDBDataContext();
            //        db.MBZ_CampaignHTMLUpdate(db.CampaignHTMLs.ToList().Last().ID);

            //        break;

            //    case "campaigns":
            //        db = new DynamicDBDataContext();
            //        db.MBZ_CampaignUpdate(db.Campaigns.ToList().Last().ID);

            //        break;

            //    case "pageimgs":
            //        db = new DynamicDBDataContext();
            //        db.MBZ_PageIMGUpdate(db.PageIMGs.ToList().Last().ID);

            //        break;

            //    case "pages":
            //        db = new DynamicDBDataContext();
            //        db.MBZ_PageUpdate(db.Pages.ToList().Last().ID);

            //        break;


            //}

            #endregion
           // Response.Redirect(table.ListActionPath);
        }

        //table = DynamicDataRouteHandler.GetRequestMetaTable(Context);



        //if (e.Exception != null && table.DisplayName.Equals("Applications"))
        //{
        //    e.ExceptionHandled = true;

        //    DynamicDBDataContext db = new DynamicDBDataContext() ;
        //    db.CMS_Applcation_Insert(e.Values["Name"].ToString(), Guid.NewGuid().ToString("N").ToUpper(), (bool)e.Values["IsActive"]);

        //    Response.Redirect(table.ListActionPath);
        //    return;
        //}
       // Response.Redirect(table.ListActionPath);
    }




    protected void DetailsDataSource_Inserted(object sender, LinqDataSourceStatusEventArgs e)
    {
        //SHURON UPDATE HERE SPECIALIZED
        if (e.Exception == null)
        {   
            //if (e.Result is Capability_EmailToUser)
            //{ 
            //    UtilsDB.CMS_RejectNewsletter_Seed_OnCreateOrUpdate(((Capability_EmailToUser)e.Result).ID, Guid.NewGuid().ToString("N").Substring(0, 6));
            //}
            //else
            //if (e.Result is Capability_SMS)
            //{ 
            //    UtilsDB.CMS_RejectNewsletter_Seed_OnCreateOrUpdate(((Capability_SMS)e.Result).ID, Guid.NewGuid().ToString("N").Substring(0, 6));
            //}
            //else
            //if (e.Result is Application)
            //{ 
            //    UtilsDB.CMS_Applcation_TokenCreation(((Application)e.Result).AppId);
            //    //Response.Redirect(table.ListActionPath+ "?lnk="+ ((Application)e.Result).AppId.ToString());//
            //}
            //else
            //if (e.Result is Capability_EmailToVendor)
            //{
            //    UtilsDB.CMS_Capability_EmailToVendor_SetHTML(((Capability_EmailToVendor)e.Result).ID);
            //    //Response.Redirect(table.ListActionPath+ "?lnk="+ ((Application)e.Result).AppId.ToString());//
            //}

            Response.Redirect(table.ListActionPath);
        }


    }


}
