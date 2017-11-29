<%@ Control Language="C#" CodeFile="DateTime_Edit.ascx.cs" Inherits="DateTime_EditField" %>

<asp:TextBox ID="TextBox1" runat="server" CssClass="DDTextBox" Enabled="false" Text='<%# FieldValueEditString %>' Columns="20" OnDataBinding="TextBox1_DataBinding"></asp:TextBox>
<table>

    <tr>
        <td><asp:Label ID="lblDate" Text="Date: " runat="server" /></td>
         <td>
             <asp:Label ID="lblIncludeDate" Text="Include Date: " runat="server" />
             <asp:CheckBox ID="cbInclude" runat="server" OnCheckedChanged="cbInclude_CheckedChanged" AutoPostBack="True" />
             <asp:Calendar ID="Calendar1" runat="server" OnDataBinding="Calendar1_DataBinding" OnSelectionChanged="Calendar1_SelectionChanged" ></asp:Calendar></td>
    </tr>
     <tr>
        <td> 
         <asp:Label ID="lblHour" Text="Hour: " runat="server" />

        </td>
         <td> <asp:DropDownList ID="ddlHour" runat="server" Width="50" OnSelectedIndexChanged="ddlHour_SelectedIndexChanged" AutoPostBack="True">
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
              </asp:DropDownList></td>
    </tr>
    <tr>
        <td><asp:Label ID="lblMinute" Text="Minute: " runat="server" /> </td>
         <td> 
             <asp:TextBox ID="tbMinute" runat="server" OnTextChanged="tbMinute_TextChanged" Text="0" AutoPostBack="True">0</asp:TextBox>
         </td>
    </tr>
    <tr>
      <td><asp:Label ID="lblSecond" Text="Sec: " runat="server" /></td>
         <td> 
             <asp:TextBox ID="tbSecond" runat="server" OnTextChanged="tbSecond_TextChanged" Text="0" AutoPostBack="True">0</asp:TextBox>

         </td>
    </tr>
</table>

<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" Enabled="false" />
<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" Enabled="false" />
<asp:DynamicValidator runat="server" ID="DynamicValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" />
<asp:CustomValidator runat="server" ID="DateValidator" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" EnableClientScript="false" Enabled="false" OnServerValidate="DateValidator_ServerValidate" />

