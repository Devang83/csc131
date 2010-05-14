using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Property_Units : System.Web.UI.Page
{
    protected List<QuickPM.PropertyUnit> units = null;
    protected QuickPM.Property property = null;
    protected void Page_Load(object sender, EventArgs e)
    {        
        string sProp = Request["PropertyId"];
        if (sProp == null)
        {
            return;
        }        
        int pNum;
        if (!Int32.TryParse(sProp, out pNum))
        {
            return;
        }
        property = new QuickPM.Property(pNum);
        GetUnits();

        if (Request.Form["__EVENTTARGET"] == "DeleteUnit")
        {
            DeleteUnit(Request.Form["__EVENTARGUMENT"]);
        }

        if (!IsPostBack)
        {            
            DropDownListTenant.Items.Add(new ListItem("None", ""));
            foreach (string tenantId in property.GetTenantIds())
            {
                QuickPM.Tenant tenant = new QuickPM.Tenant(tenantId);
                string text = "" + tenant.TenantId + " " + tenant.Name;
                //text = tenant.Name;
                text = text.Length > 20 ? text.Substring(0, 20) : text;
                DropDownListTenant.Items.Add(new ListItem(text, tenant.TenantId));
            }
        }
        bool canWrite = property.ACL.CanWrite(QuickPM.Database.GetUserId());
        ButtonAdd.Visible = canWrite;
        ButtonAdd.Enabled = canWrite;
		
		
	DropDownListTenant.Visible = canWrite;	
    TextBoxUnitNumber.Visible = canWrite;
    TextBoxSqFt.Visible = canWrite;
    TextBoxAreaSize.Visible = canWrite;
    RadioButtonListHasOutside.Visible = canWrite;
    TextBoxSqFtOutside.Visible = canWrite;
    TextBoxOutsideAreaSize.Visible = canWrite;
    TextBoxNotes.Visible = canWrite;
    
    	
		
		
        if (!canWrite)
        {
            QuickPMWebsite.AppCode.DisableControls.DisableTextBoxControls(Page);
        }
    }

    protected void GetUnits()
    {
        units = QuickPM.PropertyUnit.FindUnits(property.Id);        
    }

    
    protected void DeleteUnit(string unitId)
    {
        QuickPM.PropertyUnit unit = new QuickPM.PropertyUnit(long.Parse(unitId));
        unit.Delete();
        Response.Redirect("Units.aspx?PropertyId=" + property.Id);
    }

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        string tenantId = DropDownListTenant.SelectedValue;
        //string name = DropDownListTenant.SelectedItem.Text;
        int sqft = 0;
        Int32.TryParse(TextBoxSqFt.Text, out sqft);
        int outsidesqft;
        Int32.TryParse(TextBoxSqFtOutside.Text, out outsidesqft);
        string unitNumber = TextBoxUnitNumber.Text;        
        bool hasOutside = RadioButtonListHasOutside.SelectedItem.Text == "Yes";
        QuickPM.PropertyUnit unit = new QuickPM.PropertyUnit(unitNumber, property.Id, TextBoxNotes.Text.Trim());        
        unit.HasOutside = hasOutside;
        unit.AreaSize = TextBoxAreaSize.Text.Trim();
        unit.AreaSizeOutside = TextBoxOutsideAreaSize.Text.Trim();
        unit.SqFt = sqft;
        unit.SqFtOutside = outsidesqft;
        
        unit.PropertyId = property.Id;
        unit.Save();
        if (tenantId != "")
        {
            QuickPM.Tenant tenant = new QuickPM.Tenant(tenantId);
            tenant.AddUnit(unit.Id, DateTime.Today, DateTime.MaxValue);			
            tenant.Save();
        }
        GetUnits();
    }
}
