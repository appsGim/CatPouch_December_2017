using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DateTime_EditField : System.Web.DynamicData.FieldTemplateUserControl {
    private static DataTypeAttribute DefaultDateAttribute = new DataTypeAttribute(DataType.DateTime);
    protected void Page_Load(object sender, EventArgs e) {
        TextBox1.ToolTip = Column.Description;
        
        SetUpValidator(RequiredFieldValidator1);
        SetUpValidator(RegularExpressionValidator1);
        SetUpValidator(DynamicValidator1);
        SetUpCustomValidator(DateValidator);
    }

    private void SetUpCustomValidator(CustomValidator validator) {
        if (Column.DataTypeAttribute != null) {
            switch (Column.DataTypeAttribute.DataType) {
                case DataType.Date:
                case DataType.DateTime:
                case DataType.Time:
                    validator.Enabled = true;
                    DateValidator.ErrorMessage = HttpUtility.HtmlEncode(Column.DataTypeAttribute.FormatErrorMessage(Column.DisplayName));
                    break;
            }
        }
        else if (Column.ColumnType.Equals(typeof(DateTime))) {
            validator.Enabled = true;
            DateValidator.ErrorMessage = HttpUtility.HtmlEncode(DefaultDateAttribute.FormatErrorMessage(Column.DisplayName));
        }
    }

    protected void DateValidator_ServerValidate(object source, ServerValidateEventArgs args) {
        DateTime dummyResult;
        args.IsValid = DateTime.TryParse(args.Value, out dummyResult);
    }

    protected override void ExtractValues(IOrderedDictionary dictionary) {
        dictionary[Column.Name] = ConvertEditedValue(TextBox1.Text);
    }

    public override Control DataControl {
        get {
            return TextBox1;
        }
    }
    
    protected void Calendar1_DataBinding(object sender, EventArgs e)
    {
       // TextBox1.Text = Calendar1.SelectedDate.ToString("yyyy-MM-dd HH:mm:00");
    }

    protected void TextBox1_DataBinding(object sender, EventArgs e)
    {
        DateTime current = TextBox1.Text == "" ? UtilsDateTime.UTC_To_Israel_Time().Date : DateTime.Parse(TextBox1.Text);
        cbInclude.Checked = TextBox1.Text != "";
        if (cbInclude.Checked)
        {
            Calendar1.SelectedDate = current.Date;
            Calendar1.VisibleDate= current.Date; 
            ddlHour.SelectedIndex = current.Hour;
            tbMinute.Text = current.Hour.ToString();
            tbSecond.Text = current.Second.ToString();
        }

        if (Column.DisplayName.ToLower().Contains("insert") || Column.DisplayName.ToLower().Contains("created")
            || Column.DisplayName.ToLower().Contains("update"))
        {
            if (TextBox1.Text == "")
            {
                TextBox1.Text = DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss");
            }

            Calendar1.Visible = false;
            ddlHour.Visible = false;
            tbMinute.Visible = false;
            tbSecond.Visible = false;
            cbInclude.Visible = false;

            Calendar1.Enabled = false;
            ddlHour.Enabled = false;
            tbMinute.Enabled = false;
            tbSecond.Enabled = false;
            cbInclude.Enabled = false;

            lblHour.Visible = false;
            lblMinute.Visible = false;
            lblSecond.Visible = false;
            lblDate.Visible = false;
            lblIncludeDate.Visible = false;
        }
        

    }

    protected void ddlHour_SelectedIndexChanged(object sender, EventArgs e)
    {
        int g = 0;

        if (TextBox1.Text != "")
        {
            DateTime current = DateTime.Parse(TextBox1.Text);
            current = new DateTime(current.Year, current.Month, current.Day, int.Parse(((DropDownList)sender).SelectedValue), current.Minute, current.Second).AddSeconds(current.Second);
            TextBox1.Text = current.ToString("MM/dd/yyyy HH:mm:ss");
        }
    }

    protected void tbMinute_TextChanged(object sender, EventArgs e)
    {
        int g = 0;
        DateTime current = DateTime.Parse(TextBox1.Text);
        current = new DateTime(current.Year, current.Month, current.Day, current.Hour, int.Parse(((TextBox)sender).Text), current.Second);
        TextBox1.Text = current.ToString("MM/dd/yyyy HH:mm:ss");
    }

    protected void tbSecond_TextChanged(object sender, EventArgs e)
    {
        int g = 0;
        if (TextBox1.Text != "")
        {
            DateTime current = DateTime.Parse(TextBox1.Text);
            current = new DateTime(current.Year, current.Month, current.Day, current.Hour, current.Minute, int.Parse(((TextBox)sender).Text));
            TextBox1.Text = current.ToString("MM/dd/yyyy HH:mm:ss");
        }
    }

    protected void cbInclude_CheckedChanged(object sender, EventArgs e)
    {

        if (!cbInclude.Checked)
        {
            TextBox1.Text = "";
            ddlHour.SelectedIndex = 0;
            tbMinute.Text = "0";
            tbSecond.Text = "0";
        }
    }

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        TextBox1.Text = Calendar1.SelectedDate.ToString("MM/dd/yyyy 00:00:00");
        ddlHour.SelectedIndex = 0;
        tbMinute.Text = "0";
        tbSecond.Text = "0";
    }
}
