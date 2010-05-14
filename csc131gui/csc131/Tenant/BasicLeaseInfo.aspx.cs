using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;

public partial class Tenant_BasicLeaseInfo : System.Web.UI.Page
{
    protected QuickPM.Lease lease = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["TenantId"] == null)
        {
            lease = new QuickPM.Lease(QuickPM.Util.FormatTenantId("0-0"));
            return;
        }
        lease = new QuickPM.Lease(Request["TenantId"]);
        
        if (!IsPostBack && !lease.NewLease)
        {
            
            TextBoxExlusiveRights.Text = lease.ExclusiveRights;
            TextBoxRestrictionOfUse.Text = lease.RestrictionOfUse;
            TextBoxUseOfPremises.Text = lease.UseOfPremises;            
        }

        if (!lease.ACL.CanWrite(QuickPM.Database.GetUserId()))
        {
            QuickPMWebsite.AppCode.DisableControls.DisableTextBoxControls(Page);
			LinkButtonSubmit.Visible = false;
			LinkButtonSubmit.Enabled = false;
        }    
    }
	
	protected List<QuickPM.PropertyUnit> GetUnits()
	{
		List<QuickPM.PropertyUnit> units = (new QuickPM.Tenant(lease.TenantId)).GetCurrentUnits();
		for(int i = 0; i < units.Count; i++)
		{
			QuickPM.PropertyUnit u = units[i];
			if(HasDup(u, units))
			{
				RemoveDups(u, units);
				i--;
			}
		}
		return units;		           
	}
		
	protected bool HasDup(QuickPM.PropertyUnit unit, List<QuickPM.PropertyUnit> units)
	{
		int count = 0;
		foreach(QuickPM.PropertyUnit u in units)
		{
			if(u.Id == unit.Id)
			{
				count++;
			}
		}
		return count > 1;			
	}
	
	protected void RemoveDups(QuickPM.PropertyUnit unit, List<QuickPM.PropertyUnit> units)
	{
		int count = 0;
		for(int i = 0; i < units.Count; i++)
		{
			QuickPM.PropertyUnit u = units[i];
			if(unit.Id == u.Id)
			{
				count++;
				if(count > 1)
				{
					units.RemoveAt(i);
					i--;
				}
			}
		}
	}

    protected void LinkButtonSubmit_Click(object sender, EventArgs e)
    {
        if (lease == null || lease.TenantId == QuickPM.Util.FormatTenantId("0-0"))
        {
            return;
        }

		foreach (QuickPM.PropertyUnit unit in GetUnits())
		{
			int premiseSize;
        	int outsidePremiseSize;
			
        	string pSize = Request[unit.Id + "SqFt"].Trim();
        	string oSize = Request[unit.Id + "SqFtOutside"].Trim();
        	if (pSize == "")
        	{
            	premiseSize = 0;
        	}
        	else if (!Int32.TryParse(pSize, out premiseSize))
        	{
            
                	Session["Message"] = "<font color=\"red\">" + "Please enter a number for premise size" + "</font>";
                	return;
            
        	}
        	if (oSize == "")
        	{
            	outsidePremiseSize = 0;
        	}
        	else if (!Int32.TryParse(oSize, out outsidePremiseSize))
        	{
            	Session["Message"] = "<font color=\"red\">" + "Please enter a number for outside premise size" + "</font>";
            	return;
        	}        				
			unit.SqFt = premiseSize;
        	unit.SqFtOutside = outsidePremiseSize;
        	unit.AreaSize = Request[unit.Id + "InsideArea"];
        	unit.AreaSizeOutside = Request[unit.Id + "OutsideArea"];
        	unit.HasOutside = true;
        	if(!unit.Save())
			{
				Session["Message"] = "<font color=\"red\">Error Saving, Error Code 111</font>";
				return;
			}
		 
        	
		}
		
        lease.UseOfPremises = TextBoxUseOfPremises.Text;        
        lease.RestrictionOfUse = TextBoxRestrictionOfUse.Text;
        lease.ExclusiveRights = TextBoxExlusiveRights.Text;
        if (lease.Save())
        {
            Session["Message"] = "<font color=\"red\">Information Saved</font>";
        }
        else
        {
            Session["Message"] = "<font color=\"red\">Error Saving Code 101</font>";
        }
    }
}
