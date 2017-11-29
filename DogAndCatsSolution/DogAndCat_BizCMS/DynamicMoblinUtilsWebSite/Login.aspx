<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Table runat="server">

                <asp:TableRow>

                    <asp:TableCell>User:</asp:TableCell>

                    <asp:TableCell>
                        <asp:TextBox ID="tbuser" runat="server"></asp:TextBox>
                    </asp:TableCell>

                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell></asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                   

                     <asp:TableCell>Pass:</asp:TableCell>

                    <asp:TableCell>
                        <asp:TextBox ID="tbpass" runat="server"></asp:TextBox>
                    </asp:TableCell>

                    <asp:TableCell></asp:TableCell>
                     <asp:TableCell></asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>

                    <asp:TableCell>Code: </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="tbverification" runat="server"></asp:TextBox>
                    </asp:TableCell>
                     <asp:TableCell>

                        <img runat="server" id="imgCtrl" src="~/Images/login.jpg" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:ImageButton ID="btlogin" runat="server" Height="40" ImageUrl="~/Images/login.jpg" OnClick="btlogin_Click" BorderStyle="Outset" />
                    </asp:TableCell>

                   

                </asp:TableRow>



            </asp:Table>
        </div>
    </form>
</body>
</html>
