<%@ Control Language="C#" CodeFile="Date.ascx.cs" Inherits="DateFilter" %>

<table>
    <tr>

        <td >
            <asp:ImageButton runat="server" ID="btnViewDates" Width="20" Height="20" ImageUrl="~/Images/openpanel.png" OnClick="btnViewDates_Click" />
            <asp:ImageButton runat="server" ID="ImageButton3" ToolTip="Remove filter" CommandArgument="RemoveFilter" Width="20" Height="20" ImageUrl="~/Images/filter_delete-128.png" OnClick="Button1_Click" />
        </td>
        <td>
            <asp:Panel ID="Panel1" runat="server" Visible="false" BackColor="#e8ffff">
                <table style="border: solid;">
                    <tr>
                        <td>From: </td>
                        <td>
                            <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFromHour" runat="server" Width="50"  AutoPostBack="True">
                                <asp:ListItem Text="0" Value="0" Selected="True" />
                                <asp:ListItem Text="1" Value="1" />
                                <asp:ListItem Text="2" Value="2" />
                                <asp:ListItem Text="3" Value="3" />
                                <asp:ListItem Text="4" Value="4" />
                                <asp:ListItem Text="5" Value="5" />
                                <asp:ListItem Text="6" Value="6" />
                                <asp:ListItem Text="7" Value="7" />
                                <asp:ListItem Text="8" Value="8" />
                                <asp:ListItem Text="9" Value="9" />
                                <asp:ListItem Text="10" Value="10" />
                                <asp:ListItem Text="11" Value="11" />
                                <asp:ListItem Text="12" Value="12" />
                                <asp:ListItem Text="13" Value="13" />
                                <asp:ListItem Text="14" Value="14" />
                                <asp:ListItem Text="15" Value="15" />
                                <asp:ListItem Text="16" Value="16" />
                                <asp:ListItem Text="17" Value="17" />
                                <asp:ListItem Text="18" Value="18" />
                                <asp:ListItem Text="19" Value="19" />
                                <asp:ListItem Text="20" Value="20" />
                                <asp:ListItem Text="21" Value="21" />
                                <asp:ListItem Text="22" Value="22" />
                                <asp:ListItem Text="23" Value="23" />
                            </asp:DropDownList><br />
                            <asp:DropDownList ID="DropDownList1" runat="server">
                                <%--  <asp:ListItem Text="=" Value="=" Selected="True" />--%>
                                <asp:ListItem Text=">=" Value=">=" />
                                <asp:ListItem Text="<=" Value="<=" />
                                <asp:ListItem Text="<" Value="<" />
                                <asp:ListItem Text=">" Value=">" />
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>To:</td>
                        <td>
                            <asp:Calendar ID="Calendar2" runat="server"></asp:Calendar>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlToHour" runat="server" Width="50" AutoPostBack="True">
                                <asp:ListItem Text="0" Value="0" Selected="True" />
                                <asp:ListItem Text="1" Value="1" />
                                <asp:ListItem Text="2" Value="2" />
                                <asp:ListItem Text="3" Value="3" />
                                <asp:ListItem Text="4" Value="4" />
                                <asp:ListItem Text="5" Value="5" />
                                <asp:ListItem Text="6" Value="6" />
                                <asp:ListItem Text="7" Value="7" />
                                <asp:ListItem Text="8" Value="8" />
                                <asp:ListItem Text="9" Value="9" />
                                <asp:ListItem Text="10" Value="10" />
                                <asp:ListItem Text="11" Value="11" />
                                <asp:ListItem Text="12" Value="12" />
                                <asp:ListItem Text="13" Value="13" />
                                <asp:ListItem Text="14" Value="14" />
                                <asp:ListItem Text="15" Value="15" />
                                <asp:ListItem Text="16" Value="16" />
                                <asp:ListItem Text="17" Value="17" />
                                <asp:ListItem Text="18" Value="18" />
                                <asp:ListItem Text="19" Value="19" />
                                <asp:ListItem Text="20" Value="20" />
                                <asp:ListItem Text="21" Value="21" />
                                <asp:ListItem Text="22" Value="22" />
                                <asp:ListItem Text="23" Value="23" />
                            </asp:DropDownList><br />
                            <asp:ImageButton runat="server" ID="Button1" ToolTip="Apply filter" CommandArgument="Filter" Width="20" Height="20" ImageUrl="~/Images/search2.png" OnClick="Button1_Click" />
                            <asp:ImageButton runat="server" ID="Button2" ToolTip="Remove filter" CommandArgument="RemoveFilter" Width="20" Height="20" ImageUrl="~/Images/filter_delete-128.png" OnClick="Button1_Click" />
                        </td>
                    </tr>

                </table>
            </asp:Panel>


        </td>
    </tr>

</table>









