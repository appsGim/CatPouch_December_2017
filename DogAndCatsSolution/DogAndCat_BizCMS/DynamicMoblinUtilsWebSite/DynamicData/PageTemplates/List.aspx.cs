//using DocumentFormat.OpenXml;
//using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml.Spreadsheet;
//using DocumentFormat.OpenXml.Spreadsheet;
//using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Web.DynamicData;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.WebControls.Expressions;
using System.Web;
using System.Linq.Expressions;
using System.Data.Linq;

 
public partial class List : System.Web.UI.Page
{
    protected MetaTable table;

    protected void Page_Init(object sender, EventArgs e)
    {
         
        table = DynamicDataRouteHandler.GetRequestMetaTable(Context);
 
        foreach (MetaColumn mt in table.Columns)
        { 

            if (mt.ColumnType == typeof(Guid) || mt.IsPrimaryKey)
            {
                mt.Scaffold = true;
            }
           
        }

        object currentuser = Session["currentuser"];
        if (currentuser==null || (string)currentuser == "Unknown") { Session["usertype"] = null; Session["currentuser"] = null; Response.Redirect("~/login.aspx"); }

        GridView1.SetMetaTable(table, table.GetColumnValuesFromRoute(Context));

        GridDataSource.EntityTypeName = table.EntityType.AssemblyQualifiedName;
        if (table.EntityType != table.RootEntityType)
        {
            GridQueryExtender.Expressions.Add(new OfTypeExpression(table.EntityType));
        }
 
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Title = table.DisplayName;

        // Disable various options if the table is readonly
        if (table.IsReadOnly || UtilsConfig.isViewTable(table.DisplayName))
        {
            GridView1.Columns[0].Visible = false;
            InsertHyperLink.Visible = false;
            GridView1.EnablePersistedSelection = false;
        }

 
    }


    private List<int> hideCols(bool visible)
    {
        System.Collections.ObjectModel.ReadOnlyCollection<MetaColumn>
        cols = GridView1.GetMetaTable().Columns;

        List<int> mylist = new List<int>();

        GridViewRow gr =
        GridView1.HeaderRow;
        if (gr == null) return null;

        MetaTable mt = GridView1.GetMetaTable();

        if(!table.IsReadOnly)mylist.Add(0);


        for (int i = 1; i < gr.Cells.Count; i++)
        {
 
             
            string coltype = "TemplateField";
            string colname = ((DataControlFieldHeaderCell)gr.Cells[i]).ContainingField.ToString();

            if (colname != coltype)
            {
                coltype = GridView1.GetMetaTable().GetColumn(colname).TypeCode.ToString();
         
                switch (coltype.ToString())
                {
                    case "Object":
                        mylist.Add(i);
                        break;


                }

            }
           
        }

        return mylist;
    }
 
    protected void Label_PreRender(object sender, EventArgs e)
    {
        Label label = (Label)sender;
        System.Web.DynamicData.DynamicFilter dynamicFilter = (System.Web.DynamicData.DynamicFilter)label.FindControl("DynamicFilter");
        QueryableFilterUserControl fuc = dynamicFilter.FilterTemplate as QueryableFilterUserControl;
        if (fuc != null && fuc.FilterControl != null)
        {
            label.AssociatedControlID = fuc.FilterControl.GetUniqueIDRelativeTo(label);
       
        }
    }

    protected override void OnPreRenderComplete(EventArgs e)
    {
        RouteValueDictionary routeValues = new RouteValueDictionary(GridView1.GetDefaultValues());
        InsertHyperLink.NavigateUrl = table.GetActionPath(PageAction.Insert, routeValues);
        base.OnPreRenderComplete(e);
    }

    protected void DynamicFilter_FilterChanged(object sender, EventArgs e)
    {
        
        GridView1.PageIndex = 0;
    
    }

 
 
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {

        if (GridView1 == null || GridView1.Rows.Count == 0) return;


        List<int> mylist=   hideCols(false);
        mylist.Reverse();

        if (table.IsReadOnly)
        {
            GridView1.HeaderRow.Cells[0].Visible = false;
        }
        foreach (int i in mylist)
        {
            GridView1.HeaderRow.Cells[i].Visible = false;
        }


        //WITHOUT THIS WILL WRITE HTML TO EXCEL - MUST SET IT TO FALSE AND AT THE END TO TRUE
        //NOTE: BY THIS WE DECIDE IF TO SET IMAGES OT NOT -> NO WE DONT WANT IMAGES AT EXCEL
        GridView1.EnableSortingAndPagingCallbacks = false;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + table.Name + "_" + DateTime.UtcNow.ToString("yyyy_mm_dd_HH_mm_ss") + ".xls");
        Response.ContentType = "application/vnd.ms-excel;";
        //Response.ContentType = "text/csv";
        Response.ContentEncoding = System.Text.Encoding.Unicode;
        Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

        //SHURON REAL REMOVED    removeHeader();

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);             
            GridView1.RenderBeginTag(hw);
            GridView1.HeaderRow.RenderControl(hw);

            //if (table.IsReadOnly) GridView1.HeaderRow.Cells[0].Visible = false;

            int g = 0;
            for (int t = 0; t <= GridView1.PageCount - 1; t++)
            {
                List<int> invisible = new List<int>();
 
                GridView1.PageIndex = t;                                
                GridView1.DataBind();

                
                //if (t > 20 || g > 20000) continue;
                foreach (GridViewRow row in GridView1.Rows)
                {

                    //row.Cells[0].Visible = false;
                    //HIDE ROW DATA CELLS WE DONT WANT TO EXORRT
                    foreach (int i in mylist)
                    {
                        row.Cells[i].Visible = false;
                    }

                    if (table.IsReadOnly)
                    {
                        row.Cells[0].Visible = false;
                    }
                    //if (table.IsReadOnly) row.Cells[0].Visible = false;
                    //HIDE FROM PK CELL ALL DATA UNRELATED TO PRIMARY KEY
                    for (int i = 0; i <= row.Cells[0].Controls.Count-1; i++)
                    {
                        if(i!=1)
                        row.Cells[0].Controls[i].Visible = false;
                    }

                    row.RenderControl(hw);
                }
            }

            GridView1.FooterRow.RenderControl(hw);
            GridView1.RenderEndTag(hw);
             
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        GridView1.EnableSortingAndPagingCallbacks = true;
        hideCols(true);

    }

    /// <summary>
    /// THIS ENABLE TO RENDER GRIDVIEW1 DIRECTLY
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
    {
        return;
    }
     
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (table.IsReadOnly) { return; }
        Label ll = e.Row.Cells[0].FindControl("lblKey") as Label;
        if (ll != null) ll.Text = GridView1.DataKeys[e.Row.DataItemIndex].Value.ToString();

        if (table.DisplayName.ToLower().Contains("log"))
        {
            //< asp:DynamicHyperLink ID = "dynEdit" < asp:LinkButton runat = "server" ID = "dynDelete"

            DynamicHyperLink dynEdit = e.Row.Cells[0].FindControl("dynEdit") as DynamicHyperLink;
            if (dynEdit != null) dynEdit.Visible = false;

            LinkButton dynDelete = e.Row.Cells[0].FindControl("dynDelete") as LinkButton;
            if (dynDelete != null) dynDelete.Visible = false;
        }
    }




    protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
    { 
        if (e.Exception != null && e.Exception.Message.ToLower().Contains("reference"))
        {
            
            e.ExceptionHandled = true;
             

        }
    }


    //protected void AllowDelete_DataBinding(object sender, EventArgs e)
    //{
    //    if (UtilsConfig.RejectDelete(Title))
    //    {
    //        ((LinkButton)sender).Visible = false;
    //    }
    //}
}
 
