<%@ Page Language="C#" AutoEventWireup="true" Inherits="Checks_ViewCheck" MasterPageFile="~/BaseMaster.master" Codebehind="ViewCheck.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="CheckMenuHeader" Src="~/Checks/CheckHeaderMenu.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
<script type="text/javascript">
    function onbodyload() {

    }
    function onbodyunload() {

    }
    
</script> 
    <% string mtName = "Check";
        
        if (mt is QuickPM.Check)
       {
           mtName = "Check";
       }
        else if (mt is QuickPM.NSFCheck)
        {
            mtName = "NSF Check";
        }
           
           %>
    <title><%= mtName %> Details</title>
    
<script language="javascript" type="text/javascript">
    function Confirm()
    {
        var answer = confirm("Delete <%= mtName %>?")
	    if (answer){
		    return true;
	    }
	    else
	    {
		    return false;
	    }
    }
</script>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <% string mtName = "Check";
       string transactionName = "Received Date";
        if (mt is QuickPM.Check)
       {
           mtName = "Check";
       }
        else if (mt is QuickPM.NSFCheck)
        {
            mtName = "NSF Check";
            transactionName = "NSF Date";
        }
           
           %>
    <uc:CheckMenuHeader ID="CheckMenu" runat="server" />        
    <% if(Session["Error"] != null){ %>
        <%= Session["Error"].ToString() %>
    <%  Session["Error"] = null;
        } %>
        <div style="margin-left:auto;margin-right:auto;text-align:center">
    <b>Tenant#</b> <%= tenant.TenantId %>, &nbsp; &nbsp; <%= tenant.Name %>
    <br />
    <br />
    
    <table style="margin-left:auto;margin-right:auto">
    
    <tr>
        <td align="right"><%= mtName %>#</td><td align="left"><asp:TextBox ID="CheckNumber" Text="" runat="server" /></td>
    </tr>
    <tr>
        <td align="right"><%= mtName %> Date</td><td align="left"><asp:TextBox ID="TextBoxCheckDate" Text="" runat="server" /></td>
    </tr>
    
    <tr>
        <td align="right"><%= transactionName %> </td><td align="left"><asp:TextBox ID="TextBoxReceivedDate" Text="" runat="server" /></td>
    </tr>
    
    <tr>
    
    </tr>
    <tr>
        <td align="right">Amount</td><td align="left"><asp:TextBox ID="CheckAmount" Text="" runat = "server" /></td>
    </tr>
    </table>
    <br />
    <b>Amount Applied:</b> <%= (mt.Amount - mt.RemainingMoney()).ToString("c") %> <asp:LinkButton ID="UnapplyCheck" Text="Unapply" runat="server" OnClick="Unapply_Click"></asp:LinkButton>, &nbsp;&nbsp;    
    <b>Amount Remaining:</b> <%= mt.RemainingMoney().ToString("c") %>&nbsp;&nbsp;    
    <asp:LinkButton ID="ApplyCheck" Text="Apply Remaining" runat="server" 
                onclick="ApplyCheck_Click" />&nbsp; &nbsp;
    
    <br />
    <br />
    <% if (mt.AppliedTo.Count > 0)
       { %>    
    <h3><%= mtName%> Applied To:</h3>
    
    <table style="margin-left:auto;margin-right:auto;" class="highlight" cellpadding="10px" cellspacing="0px" border="0px">
    <tr><th>Rent Type</th><th>Date</th><th>Amount</th></tr>
    <% foreach (QuickPM.MoneyApplied moneyApplied in mt.AppliedTo)
       { 
           %>
        <% if (moneyApplied.Amount != 0m)
           { %>
        
        <tr>
            <td>        
                <%= tenant.RentTypes[moneyApplied.RentTypeIndex] %>
            </td>        
            <td>
                <%= moneyApplied.Date.ToShortDateString()%>
            </td>
            <td>
                <%= moneyApplied.Amount.ToString("c")%> 
            </td>
        </tr>
        <% } %>
    <% } %>
    </table>
    
    <br />
    <br />
    <% } %>
    Memo:<asp:TextBox ID="Memo" Text = "" runat ="server" TextMode="MultiLine"/>
    <br />
    <br />
    <br />
    <asp:LinkButton ID="SubmitButton" Text="Submit" runat="server" 
                onclick="SubmitButton_Click" />&nbsp; &nbsp;
    <asp:LinkButton ID="ButtonDeleteCheck" runat="server" 
    onclick="ButtonDeleteCheck_Click" OnClientClick="return Confirm();" Text="Delete" /><br /><br />
    </div>
    
    </asp:Content>    