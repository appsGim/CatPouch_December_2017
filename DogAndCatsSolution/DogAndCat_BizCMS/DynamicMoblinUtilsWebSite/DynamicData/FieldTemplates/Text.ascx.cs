using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TextField : System.Web.DynamicData.FieldTemplateUserControl {
    private const int MAX_DISPLAYLENGTH_IN_LIST = 250;

    public override string FieldValueString {
        get {
            string value = base.FieldValueString;
            //if (ContainerType == ContainerType.List)
            //{
            //    if (value != null && value.Length > MAX_DISPLAYLENGTH_IN_LIST && !Column.Name.ToLower().Contains("image"))
            //    {
            //        value = value.Substring(0, MAX_DISPLAYLENGTH_IN_LIST - 3) + "...";
            //    }
            //}
            return value;
        }
    }

    public override Control DataControl {
        get {
            return Literal1;
        }
    }

    //protected override void ExtractValues(IOrderedDictionary dictionary)
    //{
    //    dictionary[Column.Name] = ConvertEditedValue(Literal1.Text);
    //}

}
