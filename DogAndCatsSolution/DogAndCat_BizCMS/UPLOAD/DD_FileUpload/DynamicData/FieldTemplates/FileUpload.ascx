<%@ Control 
	Language="C#" 
	AutoEventWireup="true" 
	CodeFile="FileUpload.ascx.cs" 
	Inherits="FileImage" %>

<asp:Image ID="Image1" runat="server" />&nbsp;
<asp:Label ID="Label1" runat="server" Text="<%# FieldValueString %>"></asp:Label>
<asp:HyperLink ID="HyperLink1" runat="server"></asp:HyperLink>&nbsp;
<asp:CustomValidator 
	ID="CustomValidator1" 
	runat="server" 
	ErrorMessage="">
</asp:CustomValidator>