<%@ Control Language="C#" CodeFile="TextArea_Edit.ascx.cs" Inherits="TextArea_EditField" %>

<asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Text='<%# FieldValueEditString %>'></asp:TextBox>

<asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" ControlToValidate="TextBox1" Display="Dynamic" Enabled="false" />
<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ControlToValidate="TextBox1" Display="Dynamic" Enabled="false" />
<asp:DynamicValidator runat="server" id="DynamicValidator1" ControlToValidate="TextBox1" Display="Dynamic" />