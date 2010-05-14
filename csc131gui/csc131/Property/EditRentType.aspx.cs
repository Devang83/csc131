using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Property_EditRentType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            QuickPM.Property property = new QuickPM.Property(Int32.Parse(Request["PropertyId"]));
            int rentTypeIndex = Int32.Parse(Request["RentNum"]);
            if (property.ChartOfAccounts.ContainsKey(rentTypeIndex))
            {
                TextBoxChartOfAccount.Text = property.ChartOfAccounts[rentTypeIndex].ToString();
            }
            TextBoxName.Text = property.RentTypes[rentTypeIndex];
        }
    }

    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        string rentTypeName = TextBoxName.Text.Trim();
        int rentTypeIndex = Int32.Parse(Request["RentNum"]);
        int chartOfAccount = 0;
        if (TextBoxChartOfAccount.Text.Trim() != "")
        {
            if (!Int32.TryParse(TextBoxChartOfAccount.Text, out chartOfAccount))
            {
                Session["Error"] = "<font color=\"red\"> Please enter a number for the chart of account</font>";
                return;
            }
        }
        QuickPM.Property property = new QuickPM.Property(Int32.Parse(Request["PropertyId"]));
        if (property.RentTypes.IndexOf(rentTypeName) != -1 && rentTypeName != property.RentTypes[rentTypeIndex])
        {
            Session["Error"] = "<font color=\"red\"> That name is already taken</font>";
            return;
        }
        if (!QuickPM.Property.ValidRentType(rentTypeName))
        {
            Session["Error"] = "<font color=\"red\">Please only use letters, numbers, and % for the name</font>";
            return;
        }
        property.RentTypes[rentTypeIndex] = rentTypeName;
        property.ChartOfAccounts[rentTypeIndex] = chartOfAccount;
        property.Save();
        Response.Redirect("PropertyPage.aspx?PropertyId=" + property.Id);
    }
}
