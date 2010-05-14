<%@ Page Language="C#" AutoEventWireup="true" Inherits="AddCheck" MasterPageFile="~/BaseMaster.master" Codebehind="AddCheck.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="MainMenu" Src="~/Topbar.ascx" %>
<script language="C#" runat="server"> 
    void Cancel(Object sender, EventArgs e)
    {
        Response.Redirect(Request.Params["backlinkurl"]);
            
    }
    void AddCheck(Object sender, EventArgs e)
    {
        QuickPM.MonetaryTransaction mt;
        if (Request["Type"].ToLower() == "check")
        {
            mt = new QuickPM.Check();
        }
        else if (Request["Type"].ToLower() == "nsf")
        {
            mt = new QuickPM.NSFCheck();
        }
        else
        {
            throw new Exception("Unknown monetary transaction type: " + Request["Type"]);
        }        
        NameValueCollection form = Request.Form;
        NameValueCollection param = Request.Params;
        mt.TenantId = Request["TenantId"];
        DateTime cDate;
        if (DateTime.TryParse(checkDate.Text, out cDate))
        {
            if (mt is QuickPM.Check)
            {
                ((QuickPM.Check)mt).CheckDate = cDate;
            }
            else if (mt is QuickPM.NSFCheck)
            {
                ((QuickPM.NSFCheck)mt).CheckDate = cDate;
            }
            else
            {
                throw new Exception("Unknown monetary transaction type: " + mt.GetType().ToString());
            }
        }
        else
        {
            Session["ChecksAddCheckError"] = "<h2>Please a date for the check.</h2>";
            return;
        }

        DateTime rDate;
        if (DateTime.TryParse(receivedDate.Text, out rDate))
        {
            if (mt is QuickPM.Check)
            {
                ((QuickPM.Check)mt).ReceivedDate = rDate;
            }
            else if (mt is QuickPM.NSFCheck)
            {
                ((QuickPM.NSFCheck)mt).NSFDate = rDate;
            }
            else
            {
                throw new Exception("Unknown monetary transaction type: " + mt.GetType().ToString());
            }            
        }
        else
        {
            Session["ChecksAddCheckError"] = "<h2>Please enter a date for the check.</h2>";
            return;
        }
        
        mt.ARRecordDate = new DateTime(int.Parse(Request["Year"]), int.Parse(Request["Month"]), 1);
        QuickPM.Period p = new QuickPM.Period(mt.ARRecordDate.Year, mt.ARRecordDate.Month);
        decimal amount = 0m;
        if (Decimal.TryParse(checkAmount.Text, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.CurrentInfo, out amount))
        {
            mt.Amount = amount;
        }
        else
        {
            Session["ChecksAddCheckError"] = "<h2>Please enter an amount for the check.</h2>";
            return;
        }
        
        mt.Number = checkNumber.Text.Trim();
        mt.AutoApply(p);
        mt.Save();
        
        Response.Redirect(ResolveClientUrl("~/Tenant/AR.aspx?tenantid=" + mt.TenantId + "&year=" + mt.ARRecordDate.Year + "&month=" + mt.ARRecordDate.Month)); 
    }
</script>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <link rel="stylesheet" href="../style.css"/>   
    
    <title>Add <%= mtName  %></title>
    <script type="text/javascript">
        function onbodyload() {

        }
        function onbodyunload() {

        }
    
</script> 
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContent">
    <uc:MainMenu ID="MainMenu" runat="server" />        
    <div id="errormessage"> 
    <% if (Session["ChecksAddCheckError"] != null)
       { %>
       <%= Session["ChecksAddCheckError"].ToString() %>
    <% Session["ChecksAddCheckError"] = null;
       } %>
     </div>

        
    <div>
        <table border="2">
        <tr>
        <td><%= mtName %>#<asp:TextBox ID="checkNumber" runat="server"></asp:TextBox></td>
        <td><%= mtName %> Amount<asp:TextBox ID="checkAmount" runat="server"></asp:TextBox></td>        
        </tr>
        <tr>        
        <td><%= mtName %> Date<asp:TextBox ID="checkDate" runat="server"></asp:TextBox>        
        <script type="text/ecmascript">		      
		      $(document).ready(function() {
		        $("#<%= checkDate.UniqueID.Replace("$", "_") %>").datepicker();
		      });

		  </script>
        </td>        
        <% string recString = "Received";
            if (mtName == "Check")
           {
               recString = "Received";
           }
           else
           {
               recString = "NSF";
           } %>
        <td><%= recString %> Date<asp:TextBox ID="receivedDate" runat="server"></asp:TextBox>        
        <script type="text/ecmascript">		      
		      $(document).ready(function() {
		        $("#<%= receivedDate.UniqueID.Replace("$", "_") %>").datepicker();
		      });

		  </script>
        </td>
        </tr>
        </table>
        
    </div>
    <asp:Button ID="buttonAddCheck" Text="Add" runat="server" OnClick="AddCheck" />
    
 
    
    <br />
    <br />
    <a href="javascript: window.location=document.referrer;">Cancel</a>
</asp:Content>
