
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace QuickPMWebsite
{


	public partial class Occupancy : System.Web.UI.UserControl
	{
		protected string message = "";
	
		public bool CanEdit()
		{
			return GetTenant().ACL.CanWrite(QuickPM.Database.GetUserId());
		}
		
		protected void Page_Load(object sender, EventArgs e)
    	{			
			if (!IsPostBack)
			{
				PopulateDropDown();		
				if (!CanEdit()) 
				{
					System.Console.WriteLine("Setting controls to invisible");
					DropDownListSelectUnit.Visible = false;
					TextBoxStartDate.Visible = false;
					TextBoxEndDate.Visible = false;
					ButtonAddUnit.Visible = false;
				}
			}
			
			if (Request.Form["__EVENTTARGET"] == "EditUnit")
        	{
           	 	EditUnit(Request.Form["__EVENTARGUMENT"]);
        	}
			if (Request.Form["__EVENTTARGET"] == "DeleteUnit")
			{
				DeleteUnit(Request.Form["__EVENTARGUMENT"]);
			}
    	}
		
		protected void EditUnit(string unitId)
		{					
			string[] parts = unitId.Split(new char[]{'^'});
			string id = parts[0];
			string url = parts[1];
			Response.Redirect("~/Tenants/EditUnit/" + GetTenant().Id + "?unitid=" + id + "&" + "backlink=" + Server.UrlEncode(url));
			message = "<font color=\"red\">Sorry, editing not implemented</font>";
			return;
		}
		
		protected void DeleteUnit(string sunitId)
		{
			long unitId = 0;
			if (long.TryParse(sunitId, out unitId))
			{
				QuickPM.Tenant tenant = GetTenant();
				if (tenant.UnitIds.Contains(unitId)) 
				{
					tenant.UnitEndDates.Remove(unitId);
					tenant.UnitStartDates.Remove(unitId);
					tenant.UnitIds.Remove(unitId);
					tenant.Save();
					return;
				}
			}
			message = "<font color=\"red\">Sorry, unable to delete unit</font>";
		}
		
		protected DateTime GetStartDate(long unitId)
		{					
			if (GetTenant() == null || !GetTenant().UnitStartDates.ContainsKey(unitId)) 
			{
				return DateTime.MinValue;
			}
			return GetTenant().UnitStartDates[unitId];			
		}
		
		protected DateTime GetEndDate(long unitId)
		{
			if (GetTenant() == null || !GetTenant().UnitStartDates.ContainsKey(unitId)) 
			{
				return DateTime.MaxValue;
			}
			
			return GetTenant().UnitEndDates[unitId];			
		}
		
		protected List<QuickPM.PropertyUnit> GetPropertyUnits()
		{
			if (Request["TenantId"] != null)
			{
				string tenantId = "";
				if (QuickPM.Util.TryFormatTenantId(Request["TenantId"], out tenantId))
				{
					QuickPM.Property property = new QuickPM.Property(new QuickPM.Tenant(tenantId).GetPropertyId());
					List<QuickPM.PropertyUnit> units = QuickPM.PropertyUnit.FindUnits(property.Id);
					List<QuickPM.PropertyUnit> units2 = new List<QuickPM.PropertyUnit>();
					foreach (QuickPM.PropertyUnit unit in units)
					{
						if (unit.UnitNumber.Trim() != "") 
						{
							units2.Add(unit);
						}
					}
					return units2;
				}
			}				
			return new List<QuickPM.PropertyUnit>();
		}
		
		protected QuickPM.Tenant GetTenant() 
		{
			if (Request["TenantId"] != null)
			{
				string tenantId = "";
				if (QuickPM.Util.TryFormatTenantId(Request["TenantId"], out tenantId))
				{
					return new QuickPM.Tenant(tenantId);
					
				}
			}	
			return new QuickPM.Tenant();			
		}
		
		protected List<long> GetTenantUnitIds()
		{			
			return GetTenant().UnitIds;
		}
		
		protected void PopulateDropDown() 
		{		
			DropDownListSelectUnit.Items.Clear();
			DropDownListSelectUnit.Items.Add(new ListItem("", ""));
			DropDownListSelectUnit.Items.Add(new ListItem("Add New", "New"));
			foreach (QuickPM.PropertyUnit unit in GetPropertyUnits())
			{
				DropDownListSelectUnit.Items.Add(new ListItem(unit.UnitNumber, unit.Id.ToString()));
			}		
		}
		
		protected void DropDownListSelectUnit_SelectedIndexChanged(object obj, EventArgs e)
		{
			
		}
		
		protected void ButtonAddNewUnit_Click(object obj, EventArgs e)
		{
			string unitNumber = TextBoxUnitNumber.Text.Trim();
			int sqFt = 0;
			if (int.TryParse(TextBoxUnitSqFt.Text, out sqFt))
			{
				QuickPM.PropertyUnit unit = new QuickPM.PropertyUnit(unitNumber, GetTenant().GetPropertyId(), "");
				unit.SqFt = sqFt;
				unit.Save();
				PopulateDropDown();
				message = "<font color=\"red\">Unit#/Suite#" + unit.UnitNumber + " Added</font>";
			} else {			
				message = "<font color=\"red\">Please enter a number for the sq.ft.</font>";
			}
		}
		
		protected void ButtonAddUnit_Click(object obj, EventArgs e)
		{
			if (DropDownListSelectUnit.SelectedValue.Trim() == "")
			{
				return;
			}
			if (DropDownListSelectUnit.SelectedValue == "New")
			{
				return;
			}
			
			long unitId = long.Parse(DropDownListSelectUnit.SelectedValue);
		
			DateTime startDate;
			DateTime endDate;
			if (!DateTime.TryParse(TextBoxStartDate.Text, out startDate) || !DateTime.TryParse(TextBoxEndDate.Text, out endDate))
			{
				message = "<font color=\"red\">Error, please enter a valid date</font>";
				return;
			}
			
			QuickPM.Tenant tenant = GetTenant();			
			tenant.AddUnit(unitId, startDate, endDate);
			tenant.Save();
		}
	}
}
