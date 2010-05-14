<%@ Control Language="C#" AutoEventWireup="true" Inherits="Reports_Property_LeaseSummaryReport" Codebehind="LeaseSummaryReport.ascx.cs" %>
<% if(Request["PropertyId"] == null) { %>
Property &nbsp;&nbsp; 
<asp:DropDownList ID="DropDownListProperty" runat="server">
</asp:DropDownList> &nbsp; &nbsp; 
<asp:LinkButton ID="LinkButtonGenerate" runat="server" OnClick="Submit">Generate Report</asp:LinkButton>
<% } %>
<% if(report != null) {       
       %>    
       <br />
       <br />
<a href="<%= ResolveUrl("~/Reports/Print.aspx?" + GenerateParams()) %>">Print Report</a>
<br />
<%= report %>
<% 
    report = null;
   } %>