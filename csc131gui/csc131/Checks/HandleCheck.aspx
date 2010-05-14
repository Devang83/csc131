<%@ Page Language="C#" AutoEventWireup="true" Inherits="HandleCheck" Codebehind="HandleCheck.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="MainMenu" Src="~/Topbar.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Handle Check</title>
    <link rel="stylesheet" href="../style.css"/>
<script type="text/javascript">
    function onbodyload() {

    }
    function onbodyunload() {

    }
    
</script> 
</head>
<body>
    <form id="form1" runat="server">        
    <uc:MainMenu ID="MainMenu" runat="server" />
    </form>
    <% 
        string t = Request.Params["Func"];
        if (Request.Params["Func"] == null)
       {
           //throw new Exception("Error invalid request params.");
       }
       else if(Request.Params["Func"].Trim() == "ApplyCheck")
       {
           Server.Transfer("ApplyCheck.aspx", true);
       }
       else if (Request.Params["Func"].Trim() == "AddCheck")
       {
           Server.Transfer("AddCheck.aspx", true);
       }
       else if (Request.Params["Func"].Trim() == "ViewCheck")
       {
           Server.Transfer("ViewCheck.aspx", true);
       }
        %>       
    
</body>
</html>
