<%@ Page Language="C#" AutoEventWireup="true" Inherits="Notices_Notice" Codebehind="Notice.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Notice</title>
    <link rel="stylesheet" href="../style.css"/>    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <% if (Session["NoticeText"] != null)
       { %>
    <%= Session["NoticeText"].ToString()%>
    <% } %>
    <% Session["NoticeText"] = null; %>
    </div>
    </form>
</body>
</html>
