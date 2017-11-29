<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SMSApp.aspx.cs" Inherits="SMSApp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <table style="background-color: aliceblue; border-radius: 10px; -moz-border-radius: 10px; -webkit-border-radius: 10px; border: thin; border-style: solid;">
                <tr>
                    <td>
                        Phone:
                    </td>

                     <td>
                              <asp:TextBox ID="tbPhone" runat="server" Width="250px"></asp:TextBox>
                    </td>
                     
                </tr>
          <tr>
                    <td>
                        Text:
                    </td>

                     <td>
                         <asp:TextBox ID="tbText" runat="server" TextMode="MultiLine"  Width="500px" ></asp:TextBox>
                    </td>
               
                </tr>

          <tr>
                   
                     <td colspan="2">
                         <asp:Button ID="btnSend" Width="100%" runat="server" Text="Send" OnClick="btnSend_Click" OnClientClick='return confirm("Are you sure you want to send sms?");'  />
                    </td>
               
                </tr>

          <tr>
                    <td >
                        Result send:
                    </td>
                   <td >
                           <asp:TextBox ID="tbResult" runat="server" ReadOnly="True" TextMode="MultiLine" Width="500px" ></asp:TextBox>
                    </td>
               
                </tr>

            </table>

</asp:Content>

