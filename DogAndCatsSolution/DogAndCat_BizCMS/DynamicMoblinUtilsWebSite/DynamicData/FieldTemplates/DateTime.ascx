<%@ Control Language="C#" CodeFile="DateTime.ascx.cs" Inherits="DateTimeField" %>

<script runat="server">

    string GetDateString()
    {
      if (FieldValue != null)
      {
        DateTime dt = (DateTime)FieldValue;
        return dt.ToString("yyyy-MM-dd HH:mm:ss");
      }
      else
      {
        return string.Empty;
      }
    }

</script>

<asp:Literal runat="server" ID="Literal1" Text="<%# GetDateString() %>"  />

<%--
    
    <%# GetDateString() %>
<%# FieldValueString %>
    --%>
 

