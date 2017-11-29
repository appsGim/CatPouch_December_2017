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

public partial class SearchFilter : System.Web.DynamicData.QueryableFilterUserControl {
    private const string NullValueString = "";
    public override Control FilterControl {
            get {
                return TextBox1;
            }
        }

    protected void Page_Init(object sender, EventArgs e) {
        if (!Column.ColumnType.Equals(typeof(string)))
        {
            throw new InvalidOperationException(String.Format("A string filter was loaded for column '{0}' but the column has an incompatible type '{1}'.", Column.Name, Column.ColumnType));
        }

        if (!Page.IsPostBack)
        { 
            string initialValue = DefaultValue;
            if (!String.IsNullOrEmpty(initialValue))
            {
                TextBox1.Text = initialValue;
            }

        }
    }
  
    public override IQueryable GetQueryable(IQueryable source)
    {

        if (Column.TypeCode != TypeCode.String)
            return source;
         
        string searchString = TextBox1.Text;
        if (TextBox1.Text == "")
            searchString = String.Empty;
        Type type = typeof(String);

        if (DropDownList1.SelectedValue.Equals("Equal"))
        {
            object value = searchString;
            if (searchString == NullValueString)
            {
                value = null;
            }
            if (DefaultValues != null)
            {
                DefaultValues[Column.Name] = value;
            }
            return ApplyEqualityFilter(source, Column.Name, value);
        }

        if (searchString == "") return source;

        string searchProperty = Column.DisplayName;
        ConstantExpression searchFilter = Expression.Constant(searchString);


        ParameterExpression parameter = Expression.Parameter(source.ElementType);
        MemberExpression property = Expression.Property(parameter, this.Column.Name);
        if (Nullable.GetUnderlyingType(property.Type) != null)
        {
            property = Expression.Property(property, "Value");
        }     
        
        MethodInfo method = typeof(String).GetMethod(/*"Contains"*/DropDownList1.SelectedValue, new[] { typeof(String) });
         
        var containsMethodExp = Expression.Call(property, method, searchFilter);
        var containsLambda = Expression.Lambda(containsMethodExp, parameter);
         
        var resultExpression = Expression.Call(typeof(Queryable), "Where", new Type[] { source.ElementType }, source.Expression, Expression.Quote(containsLambda));
        return source.Provider.CreateQuery(resultExpression);

    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        if (((ImageButton)sender).CommandArgument.Equals("RemoveFilter"))
        {
            DropDownList1.SelectedIndex = 0;
            TextBox1.Text = string.Empty;
            
        }
        OnFilterChanged();
    }
}
