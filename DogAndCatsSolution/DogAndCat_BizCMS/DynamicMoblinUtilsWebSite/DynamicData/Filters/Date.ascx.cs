using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DateFilter : System.Web.DynamicData.QueryableFilterUserControl {
    private const string NullValueString = "";
    public override Control FilterControl {
            get {
                return Calendar1;
            }
        }

    protected void Page_Init(object sender, EventArgs e) {
        if (!Column.ColumnType.Equals(typeof(DateTime))) {
            throw new InvalidOperationException(String.Format("A DateTime filter was loaded for column '{0}' but the column has an incompatible type '{1}'.", Column.Name, Column.ColumnType));
        }

        if (!Page.IsPostBack) {
            //DropDownList1.Items.Add(new ListItem("All", String.Empty));
            //if (!Column.IsRequired) {
            //    DropDownList1.Items.Add(new ListItem("[Not Set]", NullValueString));
            //}
            //DropDownList1.Items.Add(new ListItem("True", Boolean.TrueString));
            //DropDownList1.Items.Add(new ListItem("False", Boolean.FalseString));
            //// Set the initial value if there is one
           string initialValue = DefaultValue;
            //if (!String.IsNullOrEmpty(initialValue)) {
            //    DropDownList1.SelectedValue = initialValue;
            //}
        }
    }

    public override IQueryable GetQueryable(IQueryable source) {        
        DateTime selectedValue = Calendar1.SelectedDate;
        if (Calendar1.SelectedDate.ToString().Contains("0001") && Calendar2.SelectedDate.ToString().Contains("0001")) {
            return source;
        }

        if (!Calendar1.SelectedDate.ToString().Contains("0001") &&   !Calendar2.SelectedDate.ToString().Contains("0001"))
        {
          
            return UtilsExpression.Set_IQueryableRange((typeof(DateTime)).Name, 
                                                            Calendar1.SelectedDate.AddHours(int.Parse(ddlFromHour.SelectedValue)).ToString("yyyy-MM-dd HH:00:ss"), 
                                                            Calendar2.SelectedDate.AddHours(int.Parse(ddlToHour.SelectedValue)).ToString("yyyy-MM-dd HH:00:ss")
                                                            , source.ElementType,
                                                            this.Column.Name,
                                                            ref source);

        }


        object value = selectedValue;
        //if(selectedValue == NullValueString) { 
        //    value = null;
        //}
        //if(DefaultValues != null) {
        //    DefaultValues[Column.Name] = value;
        //}


        switch (DropDownList1.SelectedValue)

        {
            default:
                return ApplyEqualityFilter(source, Column.Name, value);
                break;
            case "Greater":
            case "GreaterOrEqual":
            case "Equal":
            case "Less":
            case "LessOrEqual":
            case "==":
            case "=":
            case ">=":
            case "<=":
            case ">":
            case "<":
                return UtilsExpression.Set_IQueryable((typeof(DateTime)).Name, selectedValue.AddHours(int.Parse(ddlFromHour.SelectedValue)).ToString("yyyy-MM-dd HH:00:00"), source.ElementType,
                                                      this.Column.Name, DropDownList1.SelectedValue,
                                                      ref source);
        }


        return ApplyEqualityFilter(source, Column.Name, value);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //Filter/RemoveFilter
        if (((ImageButton)sender).CommandArgument.Equals("RemoveFilter"))
        { 
            Calendar1.SelectedDate = DateTime.MinValue;
            Calendar2.SelectedDate = DateTime.MinValue;
            ddlFromHour.SelectedValue = "0";
            ddlToHour.SelectedValue = "0";
            btnViewDates.ImageUrl = "~/Images/openpanel.png";
            Panel1.Visible = false;
        }
        OnFilterChanged();
    }


    protected void btnViewDates_Click(object sender, ImageClickEventArgs e)
    {
        if (Panel1.Visible)
        {
            Panel1.Visible = false;
            btnViewDates.ImageUrl = "~/Images/openpanel.png";
        }
        else
        {
            Panel1.Visible = true;
            btnViewDates.ImageUrl = "~/Images/closepanel.png";
          
        }

    }
}
