<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeFile="List.aspx.cs" Inherits="List" EnableEventValidation="false" %>

<%@ Register Src="~/DynamicData/Content/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="asp" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<%----%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:DynamicDataManager ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
        <DataControls>
            <asp:DataControlReference ControlID="GridView1" />
        </DataControls>
    </asp:DynamicDataManager>

    <table style="background-color: aliceblue; border-radius: 10px; -moz-border-radius: 10px; -webkit-border-radius: 10px; border: thin; border-style: solid;">
        <tr>
            <td style="width:150px;">
                <h1 class="DDSubHeader"><%= table.DisplayName%></h1>
            </td>
            <td>&nbsp;&nbsp;&nbsp;
                <asp:ImageButton ID="btnExportToExcel" runat="server" ImageUrl="~/Images/excel_icon3.png" Width="40" ToolTip="Download to excel"
                    OnClick="btnExportToExcel_Click" /></td>

        </tr>
    </table>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="background-color: aliceblue; border-radius: 10px; -moz-border-radius: 10px; -webkit-border-radius: 10px; border: thin; border-style: solid;">
                <tr>
                    <td>
                        <div class="DD">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="true"
                                HeaderText="List of validation errors" CssClass="DDValidator" />
                            <asp:DynamicValidator runat="server" ID="GridViewValidator" ControlToValidate="GridView1" Display="None" CssClass="DDValidator" />

                            <asp:QueryableFilterRepeater runat="server" ID="FilterRepeater">
                                <ItemTemplate>
                                    <table style="border-bottom: thin; border-bottom-style: solid;">
                                        <tr>
                                            <td style="background-color:ivory; width:200px;" align="left">
                                                <asp:Label runat="server" Text='<%# Eval("DisplayName") %>' OnPreRender="Label_PreRender" /></td>
                                            <td style="background-color: aliceblue;">
                                                <asp:DynamicFilter runat="server" ID="DynamicFilter" OnFilterChanged="DynamicFilter_FilterChanged" />
                                            </td>
                                        </tr>
                                    </table>
                                    <%--<asp:Label runat="server" Text='<%# Eval("DisplayName") %>' OnPreRender="Label_PreRender" />
                                      <asp:DynamicFilter runat="server" ID="DynamicFilter" OnFilterChanged="DynamicFilter_FilterChanged"  />
                                        <br />--%>
                                </ItemTemplate>
                            </asp:QueryableFilterRepeater>
                            <br />
                        </div>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server" DataSourceID="GridDataSource" EnablePersistedSelection="true" EnableSortingAndPagingCallbacks="true"
                AllowPaging="True" AllowSorting="True" CssClass="DDGridView"
                RowStyle-CssClass="td" HeaderStyle-CssClass="th" CellPadding="6"
                OnRowDataBound="GridView1_RowDataBound" OnRowDeleted="GridView1_RowDeleted">
             
                <Columns>
                    
                    <asp:TemplateField ItemStyle-Width="50">
                        <ItemTemplate>
                            <asp:Label ID="lblKey" runat="server" /><br />
                            <asp:DynamicHyperLink ID="dynEdit" runat="server" Action="Edit" Text="Edit" />
                            | 
                            <%--<asp:LinkButton runat="server" ID="dynDelete" CommandName="Delete" Text="Delete" OnClientClick='return confirm("Are you sure you want to delete this item?");' />--%>
                            <%--|--%>
                            <asp:DynamicHyperLink runat="server" Text="Details" />
                            <%--OnDataBinding="AllowDelete_DataBinding"--%>
                        </ItemTemplate>

                    </asp:TemplateField>
                </Columns>
                 
                <PagerStyle CssClass="DDFooter" />
                <PagerTemplate>
                    <asp:GridViewPager runat="server" />
                </PagerTemplate>
                <EmptyDataTemplate>
                    There are currently no items in this table.
                </EmptyDataTemplate>
            </asp:GridView>

            <asp:LinqDataSource ID="GridDataSource" runat="server" EnableDelete="true" />

            <asp:QueryExtender TargetControlID="GridDataSource" ID="GridQueryExtender" runat="server">

                <asp:DynamicFilterExpression ControlID="FilterRepeater" />
            </asp:QueryExtender>

            <br />

            <div class="DDBottomHyperLink">
                <asp:DynamicHyperLink ID="InsertHyperLink" runat="server" Action="Insert">
                    <img runat="server" src="~/DynamicData/Content/Images/plus.gif" alt="Insert new item" />Insert new item</asp:DynamicHyperLink>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

