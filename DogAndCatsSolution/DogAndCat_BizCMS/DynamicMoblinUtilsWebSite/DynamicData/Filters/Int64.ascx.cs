using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class Int64Filter : System.Web.DynamicData.QueryableFilterUserControl
{
    private const string NullValueString = "";
    public override Control FilterControl
    {
        get
        {
            return TextBox1;
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!Column.ColumnType.Equals(typeof(Int64)))
        {
            throw new InvalidOperationException(String.Format("A Int16 filter was loaded for column '{0}' but the column has an incompatible type '{1}'.", Column.Name, Column.ColumnType));
        }

        if (!Page.IsPostBack)
        {

            // Set the initial value if there is one
            string initialValue = DefaultValue;
            if (!String.IsNullOrEmpty(initialValue))
            {
                TextBox1.Text = initialValue;
            }
        }
    }
 
    public override IQueryable GetQueryable(IQueryable source)
    {
        string selectedValue = TextBox1.Text;
        if (String.IsNullOrEmpty(selectedValue))
        {
            return source;
        }
        object value = selectedValue;
        if (selectedValue == NullValueString)
        {
            value = null;
        }
        if (DefaultValues != null)
        {
            DefaultValues[Column.Name] = value;
        }
         
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
            case "<": return UtilsExpression.Set_IQueryable((typeof(Int64)).Name, selectedValue, source.ElementType,
                                                            this.Column.Name, DropDownList1.SelectedValue,
                                                            ref source);
        }

        return ApplyEqualityFilter(source, Column.Name, value);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (((ImageButton)sender).CommandArgument.Equals("RemoveFilter"))
        {
            TextBox1.Text = string.Empty; 
        }
        OnFilterChanged();
    }
}
