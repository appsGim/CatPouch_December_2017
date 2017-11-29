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



public partial class IntFilter : System.Web.DynamicData.QueryableFilterUserControl
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
        if (!Column.ColumnType.Equals(typeof(int)))
        {
            throw new InvalidOperationException(String.Format("A boolean filter was loaded for column '{0}' but the column has an incompatible type '{1}'.", Column.Name, Column.ColumnType));
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
         
        #region MyRegion working all
        //string selectedValue = TextBox1.Text;
        //if (String.IsNullOrEmpty(selectedValue))
        //{
        //    return source;
        //}
        //object value = selectedValue;
        //if (selectedValue == NullValueString)
        //{
        //    value = null;
        //}
        //if (DefaultValues != null)
        //{
        //    DefaultValues[Column.Name] = value;
        //}

        //int intCondition = int.Parse(selectedValue);
        //ConstantExpression intCondition_expression = Expression.Constant(intCondition);

        //ParameterExpression parameter = Expression.Parameter(source.ElementType);
        //MemberExpression property = Expression.Property(parameter, this.Column.Name);
        //if (Nullable.GetUnderlyingType(property.Type) != null)
        //{
        //    property = Expression.Property(property, "Value");
        //}

        //BinaryExpression comparison;

        //switch (DropDownList1.SelectedValue)

        //{

        //    default:
        //        return ApplyEqualityFilter(source, Column.Name, value);

        //        break;

        //    case "=":
        //        comparison = Expression.Equal(property, intCondition_expression);
        //        break;
        //    case ">=":

        //        comparison = Expression.GreaterThanOrEqual(property, intCondition_expression);

        //        break;
        //    case "<=":

        //        comparison = Expression.LessThanOrEqual(property, intCondition_expression);

        //        break;
        //    case ">":

        //        comparison = Expression.GreaterThan(property, intCondition_expression);

        //        break;
        //    case "<":

        //        comparison = Expression.LessThan(property, intCondition_expression);

        //        break;
        //}

        //LambdaExpression lambda = Expression.Lambda(comparison, parameter);

        //MethodCallExpression where = Expression.Call(
        //  typeof(Queryable),
        //  "Where",
        //  new Type[] { source.ElementType },
        //  new Expression[] { source.Expression, Expression.Quote(lambda) });

        //return source.Provider.CreateQuery(where);

        #endregion

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
                return UtilsExpression.Set_IQueryable((typeof(int)).Name, selectedValue, source.ElementType,
                                                            this.Column.Name, DropDownList1.SelectedValue,
                                                            ref source);
                //return UtilsExpression.Set_IQueryableRange((typeof(int)).Name, "0","5", source.ElementType,
                //                                            this.Column.Name, DropDownList1.SelectedValue,
                //                                            ref source);
        }

   
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


