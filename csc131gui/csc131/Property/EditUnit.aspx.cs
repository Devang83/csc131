using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Property_EditUnit : System.Web.UI.Page
{
    protected QuickPM.PropertyUnit unit = null;
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (Request["Id"] == null)
        {
            return;
        }
        unit = new QuickPM.PropertyUnit(long.Parse(Request["Id"]));
        if (!IsPostBack)
        {
            ListItem it = new ListItem("None", "");
            it.Selected = "" == unit.GetCurrentTenantId();
            DropDownListTenant.Items.Add(it);
            QuickPM.Property property = new QuickPM.Property(unit.PropertyId);
            foreach (string tenantId in property.GetTenantIds())
            {
                QuickPM.Tenant tenant = new QuickPM.Tenant(tenantId);
                ListItem item = new ListItem(tenant.GetShortName(), tenant.TenantId);
                item.Selected = tenantId == unit.GetCurrentTenantId();
                DropDownListTenant.Items.Add(item);
            }
            TextBoxAreaSize.Text = unit.AreaSize;
            TextBoxNotes.Text = unit.Notes;
            TextBoxOutsideAreaSize.Text = unit.AreaSizeOutside;
            TextBoxSqFt.Text = unit.SqFt.ToString();
            TextBoxSqFtOutside.Text = unit.SqFtOutside.ToString();
            TextBoxUnitNumber.Text = unit.UnitNumber;
            RadioButtonListHasOutside.Items[0].Selected = unit.HasOutside;
            RadioButtonListHasOutside.Items[1].Selected = !unit.HasOutside;
        }

    }

    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        string tenantId = DropDownListTenant.SelectedValue;        
        int sqft = 0;
        Int32.TryParse(TextBoxSqFt.Text, out sqft);
        int outsidesqft;
        Int32.TryParse(TextBoxSqFtOutside.Text, out outsidesqft);
        string unitNumber = TextBoxUnitNumber.Text;
        bool hasOutside = RadioButtonListHasOutside.SelectedItem.Text == "Yes";
        unit.HasOutside = hasOutside;
        unit.AreaSize = TextBoxAreaSize.Text.Trim();
        unit.AreaSizeOutside = TextBoxOutsideAreaSize.Text.Trim();
        unit.SqFt = sqft;
        unit.SqFtOutside = outsidesqft;        
        unit.UnitNumber = unitNumber;
        unit.Notes = TextBoxNotes.Text.Trim();
        
        if (tenantId != "")
        {
            QuickPM.Tenant tenant = new QuickPM.Tenant(tenantId);
            tenant.SetCurrentUnitId(unit.Id);            
            tenant.Save();
        }
        else
        {
            string id = unit.GetCurrentTenantId();
            if (id != "")
            {
                QuickPM.Tenant t = new QuickPM.Tenant(id);
                QuickPM.PropertyUnit pUnit = new QuickPM.PropertyUnit("", unit.PropertyId, "");
                pUnit.Save();                
                t.SetCurrentUnitId(pUnit.Id);
                t.Save();
            }
        }
        unit.Save();
        Response.Redirect("Units.aspx?PropertyId=" + unit.PropertyId);
    }
}
