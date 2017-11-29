<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Login Page</title>
	<link href="~/Site.css" rel="stylesheet" type="text/css" />
</head>
<body class="template">
	<form id="form1" runat="server">
	<div>
		<asp:Login ID="Login1" runat="server">
		</asp:Login>
		<strong>User Login Details:<br />
		</strong>
		<table class="gridview">
			<thead>
				<tr>
					<th>
						Username
					</th>
					<th>
						Password
					</th>
					<th>
						Roles
					</th>
				</tr>
			</thead>
			<tbody>
				<tr>
					<td>
						Admin
					</td>
					<td>
						password
					</td>
					<td>
						<strong>Admin</strong>
					</td>
				</tr>
				<tr>
					<td>
						Fred
					</td>
					<td>
						password
					</td>
					<td>
						Sales
					</td>
				</tr>
				<tr>
					<td>
						Sue
					</td>
					<td>
						password
					</td>
					<td>
						Finance, <strong>Admin</strong>
					</td>
				</tr>
				<tr>
					<td>
						Tom
					</td>
					<td>
						password
					</td>
					<td>
						Finance, Purchasing
					</td>
				</tr>
				<tr>
					<td>
						Mary
					</td>
					<td>
						password
					</td>
					<td>
						Purchasing
					</td>
				</tr>
			</tbody>
		</table>
	</div>
	</form>
</body>
</html>
