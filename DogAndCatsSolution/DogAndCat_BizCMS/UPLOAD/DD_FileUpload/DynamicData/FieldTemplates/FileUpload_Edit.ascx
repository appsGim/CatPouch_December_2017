<%@ Control 
	Language="C#" 
	AutoEventWireup="true" 
	CodeFile="FileUpload_Edit.ascx.cs"
	Inherits="FileImage_Edit" %>
	
<asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible="false">
	<asp:Image ID="Image1" runat="server" />&nbsp;
	<asp:Label ID="Label1" runat="server" Text="<%# FieldValueString %>"></asp:Label>
	<asp:HyperLink ID="HyperLink1" runat="server"></asp:HyperLink>&nbsp;
</asp:PlaceHolder>
<asp:FileUpload ID="FileUpload1" runat="server" />&nbsp;
<asp:CustomValidator 
	ID="CustomValidator1" 
	runat="server" 
	ErrorMessage="">
</asp:CustomValidator>