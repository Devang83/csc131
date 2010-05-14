<%@ Page Language="C#" MasterPageFile="~/Views/Property/Property.master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ContentPlaceHolderID="ChildMainContent" ID="Content1" runat="server">
<br/>
    <table>
    <tr><th>Property Tenant Ledger Summary</th></tr>
    </table>
    
     <fieldset>
     	<legend>
     		Beginning Period
     	</legend>
     
     
            <table>
            <tr>
            <td>Year</td>
            <td>
            	<select name="beginYear" id="beginYear">
            		<% for(int year = DateTime.Now.Year - 5; year <= DateTime.Now.Year + 5; year++) { %>
            			<% if (year == DateTime.Now.Year) { %>
            				<%= "<option selected=\"selected\" value=\"" + year + "\">" + year + "</option>" %>
            			<% } else { %>
            				<%= "<option value=\"" + year + "\">" + year + "</option>" %>
            			<% } %>
            		<% } %>
            	</select>
            </td>
            </tr>
            <tr>
            <td>Month</td>
            <td>
            <select name="beginMonth" id="beginMonth">
                <option value="1">January</option>
                <option value="2">February</option>
                <option value="3">March</option>
                <option value="4">April</option>
                <option value="5">May</option>
                <option value="6">June</option>
                <option value="7">July</option>
                <option value="8">August</option>
                <option value="9">September</option>
                <option value="10">October</option>
                <option value="11">November</option>
                <option value="12">December</option>
            </select>            
            </td>
            </tr>
            </table>
		</fieldset>        
            <br />
        <br />
                
        <fieldset>
        	<legend>Ending Period</legend>
        
                <table>
                <tr>
                <td>Year</td>
                <td><select name="endYear" id="endYear">
            		<% for(int year = DateTime.Now.Year - 5; year <= DateTime.Now.Year + 5; year++) { %>
            			<% if (year == DateTime.Now.Year) { %>
            				<%= "<option selected=\"selected\" value=\"" + year + "\">" + year + "</option>" %>
            			<% } else { %>
            				<%= "<option value=\"" + year + "\">" + year + "</option>" %>
            			<% } %>
            		<% } %>
            	</select></td>
                </tr>
                <tr>
                <td>Month</td>
                <td><select name="endMonth" id="endMonth">
                		<option value="1">January</option>
                		<option value="2">February</option>
                		<option value="3">March</option>
                		<option value="4">April</option>
                		<option value="5">May</option>
                		<option value="6">June</option>
                		<option value="7">July</option>
                		<option value="8">August</option>
                		<option value="9">September</option>
                		<option value="10">October</option>
                		<option value="11">November</option>
                		<option value="12">December</option>
            		</select>            
            	</td>
                </tr>
                </table>
        </fieldset>
            <br />
        
		<input type="submit" value="Submit"/>        
&nbsp;&nbsp;&nbsp; 

</asp:Content>     
