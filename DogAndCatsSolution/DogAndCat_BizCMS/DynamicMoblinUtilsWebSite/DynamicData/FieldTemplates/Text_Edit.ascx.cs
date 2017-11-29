using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Text_EditField : System.Web.DynamicData.FieldTemplateUserControl {
    protected void Page_Load(object sender, EventArgs e) {
        if (Column.MaxLength < 50) {
            TextBox1.Columns = Column.MaxLength;
        }
        TextBox1.ToolTip = Column.Description;

        SetUpValidator(RequiredFieldValidator1);
        SetUpValidator(RegularExpressionValidator1);
        SetUpValidator(DynamicValidator1);
    }

    protected override void OnDataBinding(EventArgs e) {
        base.OnDataBinding(e);
        if(Column.MaxLength > 0) {
            TextBox1.MaxLength = Math.Max(FieldValueEditString.Length, Column.MaxLength);
        }
    }

    protected override void ExtractValues(IOrderedDictionary dictionary) {
        dictionary[Column.Name] = ConvertEditedValue(TextBox1.Text);

        if (FileUpload1.Visible && FileUpload1.HasFile && Column.Name.ToLower().Contains("image"))
        {
            
            MediaModel mm = new MediaModel();
            System.Drawing.Image newImage = (Bitmap)((new ImageConverter()).ConvertFrom(FileUpload1.FileBytes));

            if (TextBox1.Text != "")//WE TRY TO DELETE HERE
            {
                try {
                   bool deleted=   UtilsConfig.ImageKeepFullURL?
                    mm.DeleteImage(TextBox1.Text.Replace(UtilsConfig.AzureStorage_BaseURL(), "")): mm.DeleteImage(TextBox1.Text);
                } catch (Exception ex) { }
            }

            string newName = Guid.NewGuid().ToString("N").Substring(0,10) ;
            string[] result = mm.Save(newName, UtilsConfig.Get(enumConfigKeys.Storage_Container), newImage);
            newName = result[1];
            Image1.ImageUrl = result[1];
            TextBox1.Text = result[0];
            dictionary[Column.Name] =UtilsConfig.ImageKeepFullURL? result[1]: result[0];
 
        }
        else if (Column.Name.ToLower().Contains("image"))
        {
            FileUpload1.Visible = true;
            Image1.Visible = true;
            Image1.ImageUrl = UtilsConfig.ImageKeepFullURL ? TextBox1.Text : UtilsConfig.AzureStorage_BaseURL()+ TextBox1.Text;
        }
        else { Image1.Visible = false;  FileUpload1.Visible = false; }
    }

    public override Control DataControl {
        get {
            return TextBox1;
        }
    }


    protected void TextBox1_DataBinding(object sender, EventArgs e)
    {
      
        if (Column.Name.ToLower().Contains("image") )
        {
            FileUpload1.Visible = true;
            TextBox1.Visible = true;
            TextBox1.Enabled = false;
        }

    }
}
