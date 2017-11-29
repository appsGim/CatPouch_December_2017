using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
public partial class GridViewPager : System.Web.UI.UserControl {
    private GridView _gridView;

    protected void Page_Load(object sender, EventArgs e) {
        Control c = Parent;
        while (c != null) {
            if(c is GridView) {
                _gridView = (GridView)c;
                
                break;
            }
            c = c.Parent;
        }

        if (c is GridView)
        {
            setImages(_gridView);
        }
         
        //if (_gridView != null)
        //{
        //    lblTotalRows.Text = "Total " + _gridView.Rows.Count.ToString() + " rows";
        //}else lblTotalRows.Text = "Total 0 rows";
    }


    protected void setImages(GridView GridView1)
    {
        if (!GridView1.EnableSortingAndPagingCallbacks)
              return;

            System.Collections.ObjectModel.ReadOnlyCollection<MetaColumn>
        cols = GridView1.GetMetaTable().Columns;
        bool conainsimages = (from cc in cols.ToList() where cc.Name.ToLower().Contains("image") select cc).FirstOrDefault() != null;

        if (!conainsimages) return;

        string baseurl = UtilsConfig.AzureStorage_BaseURL();
        int row = -1;

        foreach (GridViewRow gv in GridView1.Rows)
        {
            row++;

            if (!gv.Visible) continue;

            TableCellCollection tbc = gv.Cells;

            int cellPosition = -1;
            foreach (TableCell ob in tbc)
            {
                cellPosition++;
                if (GridView1.HeaderRow.Cells[cellPosition].Controls.Count == 0) continue;
                object lb = (object)GridView1.HeaderRow.Cells[cellPosition].Controls[0];
                if (lb == null || lb.GetType().BaseType != typeof(LinkButton)) continue;


                if (((LinkButton)lb).Text.ToLower().Contains("image") && !((LinkButton)lb).Text.ToLower().Contains("type"))
                {
                    ControlCollection cl = ob.Controls;
                    // object l = (object)cl[0];
                    // DynamicControl dn = (DynamicControl)l;
                    DynamicControl dn = (DynamicControl)((object)cl[0]);
                    Literal lt = (Literal)dn.Controls[0].Controls[0];

                    if (!string.IsNullOrEmpty(lt.Text))
                    {
                        dn.Controls[0].Controls.Add(new LiteralControl("</br>"));
                        dn.Controls[0].Controls.Add(new System.Web.UI.WebControls.Image()
                        {
                            Width = 100,
                            Height = 100,
                            ID = "Image" + row.ToString() + cellPosition.ToString(),
                            ImageUrl = UtilsConfig.ImageKeepFullURL ? lt.Text :  lt.Text.Contains("http")? lt.Text: baseurl + lt.Text,
                            ToolTip = UtilsConfig.ImageKeepFullURL  ? lt.Text : lt.Text.Contains("http") ? lt.Text : baseurl + lt.Text
                        });
                    }
                }
            }

        }

    }

    protected void TextBoxPage_TextChanged(object sender, EventArgs e) {
        if (_gridView == null) {
            return;
        }
        int page;
        if (int.TryParse(TextBoxPage.Text.Trim(), out page)) {
            if (page <= 0) {
                page = 1;
            }
            if (page > _gridView.PageCount) {
                page = _gridView.PageCount;
            }
            _gridView.PageIndex = page - 1;
        }
        TextBoxPage.Text = (_gridView.PageIndex + 1).ToString(CultureInfo.CurrentCulture);
    }

    protected void DropDownListPageSize_SelectedIndexChanged(object sender, EventArgs e) {
        if (_gridView == null) {
            return;
        }
        DropDownList dropdownlistpagersize = (DropDownList)sender;
        _gridView.PageSize = Convert.ToInt32(dropdownlistpagersize.SelectedValue, CultureInfo.CurrentCulture);
        int pageindex = _gridView.PageIndex;
        _gridView.DataBind();
         
        if (_gridView.PageIndex != pageindex) {
            //if page index changed it means the previous page was not valid and was adjusted. Rebind to fill control with adjusted page
            _gridView.DataBind();
            //lblTotalRows.Text ="Total "+ _gridView.Rows.Count.ToString() + " rows";
        }
    }

    protected void Page_PreRender(object sender, EventArgs e) {
        if (_gridView != null) {
            LabelNumberOfPages.Text = _gridView.PageCount.ToString(CultureInfo.CurrentCulture);
            TextBoxPage.Text = (_gridView.PageIndex + 1).ToString(CultureInfo.CurrentCulture);
            DropDownListPageSize.SelectedValue = _gridView.PageSize.ToString(CultureInfo.CurrentCulture);
            //lblTotalRows.Text = "Total " + (_gridView.Rows.Count * _gridView.PageCount -1)+_gridView.pa + " rows";
        }
    }

}
