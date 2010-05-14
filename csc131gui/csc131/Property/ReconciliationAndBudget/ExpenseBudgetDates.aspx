<%@ Page Language="C#" MasterPageFile="~/Property/Property.master" AutoEventWireup="true" Inherits="Property_BudgetDates" Title="Untitled Page" Codebehind="ExpenseBudgetDates.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ChildMainContent" Runat="Server">
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
    <% if (Session["Error"] != null)
       { %>
       <%= Session["Error"].ToString() %>
       <br />
    <% Session["Error"] = null;
       } %>
    <% if (Session["BudgetHtml"] == null)
       { %>
       <br />
    <asp:Panel ID="Panel1" GroupingText="Beginning Period" runat="server">
        Month &nbsp;<asp:DropDownList ID="DropDownListBeginMonth" runat="server">
        <asp:ListItem>January</asp:ListItem>
        <asp:ListItem>February</asp:ListItem>
        <asp:ListItem>March</asp:ListItem>
        <asp:ListItem>April</asp:ListItem>
        <asp:ListItem>May</asp:ListItem>
        <asp:ListItem>June</asp:ListItem>
        <asp:ListItem>July</asp:ListItem>
        <asp:ListItem>August</asp:ListItem>
        <asp:ListItem>September</asp:ListItem>
        <asp:ListItem>October</asp:ListItem>
        <asp:ListItem>November</asp:ListItem>
        <asp:ListItem>December</asp:ListItem>
        </asp:DropDownList><br /><br />
        Year &nbsp; 
        <asp:DropDownList ID="DropDownListBeginYear" runat="server">
        </asp:DropDownList>
        
    </asp:Panel>
<br />
    <asp:Panel ID="Panel2" GroupingText="Ending Period" runat="server">
        Month &nbsp;<asp:DropDownList ID="DropDownListEndMonth" runat="server">
        <asp:ListItem>January</asp:ListItem>
        <asp:ListItem>February</asp:ListItem>
        <asp:ListItem>March</asp:ListItem>
        <asp:ListItem>April</asp:ListItem>
        <asp:ListItem>May</asp:ListItem>
        <asp:ListItem>June</asp:ListItem>
        <asp:ListItem>July</asp:ListItem>
        <asp:ListItem>August</asp:ListItem>
        <asp:ListItem>September</asp:ListItem>
        <asp:ListItem>October</asp:ListItem>
        <asp:ListItem>November</asp:ListItem>
        <asp:ListItem>December</asp:ListItem>
        </asp:DropDownList><br /><br />
        Year &nbsp; 
        <asp:DropDownList ID="DropDownListEndYear" runat="server">
        </asp:DropDownList>
    </asp:Panel>
    
    <!--<br />
    Estimated Monthly Expenses &nbsp; 
    <asp:TextBox ID="TextBoxMonthlyExpenses" runat="server" Text="0.0">  </asp:TextBox> -->
    <br />
    <br />
    <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" OnClick="ButtonSubmit_Click" />
    <% }
       else
       { %>
       <br />
    <asp:Panel ID="Panel3" GroupingText="Income" runat="server">
    
        <%= (string)Session["BudgetHtml"] %>
        </asp:Panel>
        <br />
    <asp:Panel ID="Panel4" GroupingText="Expenses" runat="server">
        <%= (string)Session["BudgetExpenses"] %>
    </asp:Panel>
        <br />    
    <asp:Panel ID="Panel5" GroupingText="Net Income" runat="server">
        <%= (string)Session["BudgetNet"] %>
        
    </asp:Panel>
    
    <% Session["BudgetHtml"] = null;
       Session["BudgetExpenses"] = null;
       Session["BudgetNet"] = null;       
       } %>
</asp:Content>

