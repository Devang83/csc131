<%@ Page Language="C#" AutoEventWireup="true" Inherits="ImportChecks" MasterPageFile="~/BaseMaster.master" Codebehind="ImportChecks.aspx.cs" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>Import Checks</title>
<script type="text/javascript">
    function onbodyload() {

    }
    function onbodyunload() {

    }
    
</script> 
</asp:Content>    

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <h2>Please Paste The Checks To Import From Excel:</h2>    
    <div>
        
        <asp:TextBox ID="TextBoxChecks" runat="server" Height="373px" Width="801px" 
            TextMode="MultiLine"></asp:TextBox>
        <asp:Button ID="ButtonImport" runat="server" onclick="ButtonImport_Click" 
            Text="Import" />
        
    </div>
    <br />
    <% if (Session["ImportedChecks"] != null)
       { %>
        <h2>Imported Checks</h2>
        <table cellpadding="10">
        <tr>
            <th>Check#</th>
            <th>Duplicate</th>
        </tr>
        <% System.Collections.Generic.List<System.Collections.Generic.List<string>> importedChecks = (System.Collections.Generic.List<System.Collections.Generic.List<string>>)Session["ImportedChecks"]; %>
        <% foreach (System.Collections.Generic.List<string> checkInfo in importedChecks)
           { %>
           <tr>
            <td>#<%= checkInfo[0] %></td>
            <td><%= checkInfo[1] %></td>
            </tr>
            
        <% } %>
        </table>
    <% } %>
    <% if (Session["SkippedLines"] != null)
       {
            foreach(string line in (System.Collections.Generic.List<string>)(Session["SkippedLines"])){
        %>
                <h2>Did Not Import:</h2>
                <%= line %> <br />
        <% };
       }; %>
</asp:Content>