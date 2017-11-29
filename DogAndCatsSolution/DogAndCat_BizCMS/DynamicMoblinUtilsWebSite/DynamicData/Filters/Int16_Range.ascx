<%@ Control Language="C#" CodeFile="Int16_Range.ascx.cs" Inherits="Int16_RangeFilter" %>

<%--AutoPostBack="true"--%>
<asp:TextBox runat="server" ID="TextBox1"  Width="50" />To
<asp:TextBox runat="server" ID="TextBox2"  Width="50" />
<asp:ImageButton runat="server" ID="Button1" ToolTip="Apply filter" Width="20" Height="20" ImageUrl="~/Images/search2.png" OnClick="Button1_Click" />
<asp:ImageButton runat="server" ID="Button2" ToolTip="Remove filter" CommandArgument="RemoveFilter"  Width="20" Height="20" ImageUrl="~/Images/filter_delete-128.png" OnClick="Button1_Click" />


