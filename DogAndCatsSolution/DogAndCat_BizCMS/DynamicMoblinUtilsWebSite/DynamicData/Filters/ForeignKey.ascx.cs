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

public partial class ForeignKeyFilter : System.Web.DynamicData.QueryableFilterUserControl {
    private const string NullValueString = "[null]";
    private new MetaForeignKeyColumn Column {
        get {
            return (MetaForeignKeyColumn)base.Column;
        }
    }

    public override Control FilterControl {
        get {
            return DropDownList1;
        }
    }

    protected void Page_Init(object sender, EventArgs e) {
        if (!Page.IsPostBack) {
            if (!Column.IsRequired) {
                DropDownList1.Items.Add(new ListItem("[Not Set]", NullValueString));
            }
 
            PopulateListControl(DropDownList1);
            ReorderAlphabetized();
            // Set the initial value if there is one
            string initialValue = DefaultValue;
            if (!String.IsNullOrEmpty(initialValue)) {
                DropDownList1.SelectedValue = initialValue;
            }
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e) {
        OnFilterChanged();
    }


    public void ReorderAlphabetized()
    {
        List<ListItem> listCopy = new List<ListItem>();
        foreach (ListItem item in DropDownList1.Items)
            listCopy.Add(item);
 

        switch (this.Column.DisplayName)
        {
            case "Application_CapabilityType":

                int id = 0;

                #region SET CAPABILITY TYPE ID BY TABLE NAME
               
                switch (this.Column.Table.DisplayName)
                {
                    case "Capability_SMS":

                        id = 1;
                        goto case "InternalFinalize";

                    case "Capability_OTPs":

                        id = 4;
                        goto case "InternalFinalize";

                    case "Capability_Barcodes":

                        id = 6;
                        goto case "InternalFinalize";

                    case "Capability_EmailToUsers":

                        id = 3;
                        goto case "InternalFinalize";

                    case "Capability_EmailToVendors":

                        id = 2;
                        goto case "InternalFinalize";


                    case "Capability_ImageUploads":

                        id = 5;
                        goto case "InternalFinalize";


                    case "Capability_OTPViews":
                        id = 7;
                        goto case "InternalFinalize";

                    case "InternalFinalize":
                        DropDownList1.Items.FindByValue(id.ToString()).Selected = true;
                        DropDownList1.Enabled = false;
                        return;

                }
 

                #endregion
                 
                break;
        }


        DropDownList1.Items.Clear();
        DropDownList1.Items.Add(listCopy.FirstOrDefault(item => item.Text == "All"));
        listCopy.RemoveAll(item => item.Text == "All");

        foreach (ListItem item in listCopy.OrderBy(item => item.Text))
                 DropDownList1.Items.Add(item);
    }

    public override IQueryable GetQueryable(IQueryable source) {
        string selectedValue = DropDownList1.SelectedValue;
        if (String.IsNullOrEmpty(selectedValue)) {
            return source;
        }

        if (selectedValue == NullValueString) {
            return ApplyEqualityFilter(source, Column.Name, null);
        }

        IDictionary dict = new Hashtable();
        Column.ExtractForeignKey(dict, selectedValue);
        foreach (DictionaryEntry entry in dict) {
            string key = (string)entry.Key;
            if(DefaultValues != null) {
                DefaultValues[key] = entry.Value;
            }
            source = ApplyEqualityFilter(source, Column.GetFilterExpression(key), entry.Value);
        }
        return source;
    }

}
