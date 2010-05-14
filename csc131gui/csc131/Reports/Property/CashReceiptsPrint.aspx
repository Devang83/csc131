<%@ Page Language="C#" AutoEventWireup="true" Inherits="Reports_CashReceiptsPrint" Codebehind="CashReceiptsPrint.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cash Receipts Report</title>
</head>
<body>
      <div id="content">
<a onclick="window.print();" href="javascript:void(0)">Print</a>
<br />
 <% if (Session["CashReceiptsReportHtmlPrint"] != null)
    {
        %>
        <%= Session["CashReceiptsReportHtmlPrint"]%>
 <% Session["CashReceiptsReportHtmlPrint"] = null;
    } %>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
    </div>

</body>
</html>
