<%@ Control Language="C#" CodeFile="Text_Edit.ascx.cs" Inherits="Text_EditField" %>

<asp:TextBox ID="TextBox1" runat="server" Text='<%# FieldValueEditString %>' CssClass="DDTextBox" OnDataBinding="TextBox1_DataBinding" TextMode="MultiLine"></asp:TextBox>
<br />
<asp:Image ID ="Image1" runat="server" visible="false" /><br />
<asp:FileUpload ID ="FileUpload1" runat="server" visible="false" />

<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" Enabled="false" />
<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" Enabled="false" />
<asp:DynamicValidator runat="server" ID="DynamicValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" />

