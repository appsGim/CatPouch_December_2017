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



public partial class Int16_RangeFilter : System.Web.DynamicData.QueryableFilterUserControl
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
        if (!Column.ColumnType.Equals(typeof(Int16)))
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
        //string selectedValue = TextBox1.Text;
        if (String.IsNullOrEmpty(TextBox1.Text) && String.IsNullOrEmpty(TextBox2.Text))
        {
            
           return source;
        }
 
       
        return UtilsExpression.Set_IQueryableRange((typeof(Int16)).Name, TextBox1.Text, TextBox2.Text, source.ElementType,
                                                            this.Column.Name, 
                                                            ref source);
        
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (((ImageButton)sender).CommandArgument.Equals("RemoveFilter"))
        {
            TextBox1.Text = string.Empty;
            TextBox2.Text = string.Empty;
        }
        OnFilterChanged();
    }
}


