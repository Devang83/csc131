<%@ Control Language="C#" AutoEventWireup="true" Inherits="Checks_CheckHeaderMenu" Codebehind="CheckHeaderMenu.ascx.cs" %>
<div id="top">
    <% int year = DateTime.Now.Year;
       int month = DateTime.Now.Month;
       if (Request["CheckId"] != null && Request["Type"] != null)
       {
           string checkId = Request["CheckId"];
           string transactiontType = Request["Type"];
           transactiontType = transactiontType.ToLower();
           QuickPM.MonetaryTransaction transaction;
           if (transactiontType == "check")
           {
               transaction = new QuickPM.Check(long.Parse(checkId));
           }
           else
           {
               transaction = new QuickPM.NSFCheck(long.Parse(checkId));
           }
           DateTime date = transaction.ARRecordDate;
           year = date.Year;
           month = date.Month;           
       
        %>
    <% if (Request.Url.Segments[Request.Url.Segments.Length - 1].Contains("AR.aspx"))
       { %>
       Ledger&nbsp;&nbsp;
       <% }
       else
       { %>

            <a href="<%= ResolveUrl("~/Tenant/AR.aspx?tenantid=") + transaction.TenantId + "&year=" + year + "&month=" + month %>">Ledger</a> &nbsp;&nbsp;
    
        <% } %>
    <% if (Request.Url.Segments[Request.Url.Segments.Length - 1].Contains("ViewCheck.aspx"))
       { %>
       View Check&nbsp;&nbsp;
       <% }
       else
       { %>
            <a href="<%= ResolveUrl("~/Checks/ViewCheck.aspx?checkid=") + Request["checkid"] + "&type=" + Request["Type"] %>">View Check</a>&nbsp;&nbsp;
    <% } %>
    <% } %>
</div>