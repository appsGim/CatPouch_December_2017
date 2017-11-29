<%@ Control Language="C#" CodeFile="Search.ascx.cs" Inherits="SearchFilter" %>

<%--<asp:DropDownList runat="server" ID="DropDownList1" AutoPostBack="True" CssClass="DDFilter"
    OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
</asp:DropDownList>--%>

<asp:TextBox runat="server" ID="TextBox1" AutoPostBack="False">

</asp:TextBox>
  <asp:DropDownList runat="server" ID="DropDownList1" AutoPostBack="False" >
  <asp:ListItem Text="Contains" Value="Contains" />
 <asp:ListItem Text="Equal" Value="Equal" />
</asp:DropDownList>
<asp:ImageButton runat="server" ID="Button1" ToolTip="Apply filter" Width="20" Height="20" ImageUrl="~/Images/search2.png" OnClick="Button1_Click" />
<asp:ImageButton runat="server" ID="Button2" ToolTip="Remove filter" CommandArgument="RemoveFilter"  Width="20" Height="20" ImageUrl="~/Images/filter_delete-128.png" OnClick="Button1_Click" />


