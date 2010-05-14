<%@ Page Language="C#" AutoEventWireup="true" Inherits="Tenant_Insurance" MasterPageFile="~/Tenant/Tenant.master" Codebehind="Insurance.aspx.cs" %>

<%@ Register TagPrefix="uc" TagName="InsurancePolicy" Src="~/Insurance/InsurancePolicy.ascx" %>
<%@ Register TagPrefix="uc" TagName="Documents" Src="~/Documents/DocumentsControl.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ChildMainContent" runat="server">
<h3>Insurance</h3>
<script type="text/javascript">
    function onbodyload(){

    }
    function onbodyunload(){

    }
    
</script> 
<style type="text/css">
    div.heading
    {
         color: Blue;
         font-size:large;
    }
    
</style>
    
      <% if (Session["Message"] != null)
         { %>
      <br />
         <%= Session["Message"] %>
      <br />
      <br />
      <% } %>
<asp:Panel ID="Panel1" runat="server" style="cursor: pointer;" GroupingText="Certificate Information">
     
   <table>
   <tr>
   <td>Policy Effective Date</td>
   <td>
       <asp:TextBox ID="TextBoxICBeginDate" runat="server"></asp:TextBox>
       <script type="text/ecmascript">		      
		      $(document).ready(function() {
		        $("#<%= TextBoxICBeginDate.UniqueID.Replace("$", "_") %>").datepicker();
		      });

		  </script>
   </td>
   </tr>
   <tr>
   <td>Policy Expiration Date</td>
   <td>
       <asp:TextBox ID="TextBoxICEndDate" runat="server"></asp:TextBox>
       <script type="text/ecmascript">		      
		      $(document).ready(function() {
		        $("#<%= TextBoxICEndDate.UniqueID.Replace("$", "_") %>").datepicker();
		      });

		  </script>
   </td>
   </tr>
   <tr>
   <td>Insured</td>
   <td><asp:TextBox ID="TextBoxInsured" runat="server" Height="92px" 
           TextMode="MultiLine" Width="262px"></asp:TextBox></td>
   
   <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
   <td>Additional Insured</td>
   <td><asp:TextBox ID="TextBoxAdditionalInsured" runat="server" Height="90px" 
           TextMode="MultiLine" Width="244px"></asp:TextBox></td>
   </tr>
   
   </table>
    <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" OnClick="ButtonSubmit_Click" />
    </asp:Panel>
    
    <br />
   <br />
     
     <br />

     <uc:Documents runat="server" Id="DocumentsIC"></uc:Documents>   
</asp:Content>            