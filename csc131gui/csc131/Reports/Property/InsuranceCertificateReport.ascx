<%@ Control Language="C#" AutoEventWireup="true" Inherits="Reports_Property_InsuranceCertificateReport" Codebehind="InsuranceCertificateReport.ascx.cs" %>
<% if (Request["PropertyId"] == null)
   { %>
Property &nbsp; &nbsp; 
    <asp:DropDownList ID="DropDownListProperty" runat="server">
    </asp:DropDownList>&nbsp;&nbsp;
<asp:LinkButton ID="LinkButtonSubmit" runat="server" OnClick="Submit">Go</asp:LinkButton>
<br />
<br />

Tenants Missing Insurance Certificates &nbsp; &nbsp;    
<asp:LinkButton ID="LinkButtonMissing" runat="server" OnClick="SubmitMissing">Go</asp:LinkButton>
<% } %>

<% if(report != null) { %>    
       <br />
       <br />
<a href="<%= ResolveUrl("~/Reports/Print.aspx?" + GenerateParams()) %>">Print Report</a>
<br />
<%= report %>
<% 
    report = null;
   } %>