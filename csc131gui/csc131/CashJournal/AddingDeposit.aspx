<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddingDeposit.aspx.cs" Inherits="QuickPMWebsite.CashJournal.AddingDeposit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <meta http-equiv="REFRESH" content="0;url=<%= Page.ResolveClientUrl("AddingDeposit.aspx?DepositId=" + deposit.Id.ToString()) + "&DepositEntryIndex=" + (index + 1).ToString() %>" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <script type="text/javascript">
    <% if (Session["ErrorMessage"] != null) { %>
    	alert("<%= Session["ErrorMessage"] %>");
    <% 
    	Session["ErrorMessage"] = null;
    } %>
    </script>
    <h1>Adding Deposit To Accounts Receivable Records, Please Wait...</h1>
    <% 
            //int index = GetDepositEntryIndex();
            //QuickPM.Deposit deposit = GetDeposit();
         %>
    <% if(deposit.Id != -1 && deposit.DepositEntries.Count > index && deposit.DepositEntries[index].HasTenantId) { %>
    <h2>Processing Check#<%= deposit.DepositEntries[index].TransactionId  %></h2>
    <% } %>
    </div>
    </form>
</body>
</html>
