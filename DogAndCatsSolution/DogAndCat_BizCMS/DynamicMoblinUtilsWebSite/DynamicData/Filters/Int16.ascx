<%@ Control Language="C#" CodeFile="Int16.ascx.cs" Inherits="Int16Filter" %>

<%--AutoPostBack="true"--%>
<asp:TextBox runat="server" ID="TextBox1"  Width="50" />
<asp:DropDownList ID="DropDownList1" runat="server">
    <asp:ListItem Text="=" Value="=" Selected="True" />
    <asp:ListItem Text=">=" Value=">="  />
    <asp:ListItem Text="<=" Value="<=" />
    <asp:ListItem Text="<" Value="<"   />
    <asp:ListItem Text=">" Value=">"  />
</asp:DropDownList>
<asp:ImageButton runat="server" ID="Button1" ToolTip="Apply filter" Width="20" Height="20" ImageUrl="~/Images/search2.png" OnClick="Button1_Click" />
<asp:ImageButton runat="server" ID="Button2" ToolTip="Remove filter" CommandArgument="RemoveFilter"  Width="20" Height="20" ImageUrl="~/Images/filter_delete-128.png" OnClick="Button1_Click" />


