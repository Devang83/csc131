<%@ Control Language="C#" AutoEventWireup="true" Inherits="Insurance_InsurancePolicy" Codebehind="InsurancePolicy.ascx.cs" %>
<script language="C#" runat="server"> 
    public DropDownList PolicyDescription()
    {
        return this.DropDownListPolicyDescription;
    }

    public DropDownList LimitDescription()
    {
        return DropDownListLimitDescription;
    }

    public TextBox Insurer()
    {
        return TextBoxInsurer;        
    }
    
    public TextBox PolicyNumber()
    {
        return TextBoxPolicyNumber;
    }

    public TextBox BeginPeriod()
    {
        return TextBoxBeginPeriod;
    }

    public TextBox EndPeriod()
    {
        return TextBoxEndPeriod;
    }

    public TextBox LimitAmount()
    {
        return TextBoxLimitAmount;
    }       
</script>

<table>
<tr>
<th>Insurer</th>
<th>Type of Insurance</th>
<th>Policy Number</th>
<th>Policy Period</th>
<th>Limits</th>
</tr>

<tr>


    <td>
        <asp:TextBox ID="TextBoxInsurer" runat="server"></asp:TextBox>        
    </td>
    <td>
        <asp:DropDownList ID="DropDownListPolicyDescription" runat="server">
            <asp:ListItem>General Liability</asp:ListItem>
            <asp:ListItem>Non-Owned & Hired Autos </asp:ListItem>
            <asp:ListItem>Commercial Umbrella</asp:ListItem>
        </asp:DropDownList>
    </td>
    <td>
        <asp:TextBox ID="TextBoxPolicyNumber" runat="server"></asp:TextBox>        
    </td>
    
    <td>
        <asp:TextBox ID="TextBoxBeginPeriod" runat="server"></asp:TextBox>
        &nbsp;-&nbsp;
        <asp:TextBox ID="TextBoxEndPeriod" runat="server"></asp:TextBox>                               
    </td>
    <td>
        <asp:TextBox ID="TextBoxLimitAmount" runat="server"></asp:TextBox>        
        <br />
        <asp:DropDownList ID="DropDownListLimitDescription" runat="server">
            <asp:ListItem>Each Occurrence</asp:ListItem>
            <asp:ListItem>Aggregate</asp:ListItem>
        </asp:DropDownList>
    </td>
    <td>
        <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" 
            onclick="ButtonSubmit_Click" />
    </td>
</tr>

</table>