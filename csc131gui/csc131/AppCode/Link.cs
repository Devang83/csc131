using System;
using System.Collections.Generic;
using System.Web;
namespace QuickPMWebsite.AppCode
{
    /// <summary>
    /// Summary description for Link
    /// </summary>
    public class Link
    {
        public Link()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        
		
		/*public static string LinkTo(VolunteerTracker.Tenant tenant, System.Web.UI.TemplateControl page)
		{
			return LinkTo(tenant, page, false);
		}			
		
		public static string LinkTo(VolunteerTracker.Tenant tenant, System.Web.UI.TemplateControl page, bool isAnonymous)
        {
            if (isAnonymous)
            {
                return page.ResolveUrl("~/Public/Tenant/Tenant.aspx?tenantid=" + tenant.TenantId);
            }
            else
            {
                return page.ResolveUrl("~/Tenants/TenantPage/" + tenant.Id);
            }

        }
		
		public static string LinkTo(VolunteerTracker.Property property, System.Web.UI.TemplateControl page)
		{
			return LinkTo(property, page, false);
		}

        public static string LinkTo(VolunteerTracker.Property property, System.Web.UI.TemplateControl page, bool isAnonymous)
        {
            if (isAnonymous)
            {
                return page.ResolveUrl("~/Public/Property/PropertyPage.aspx?propertyid=" + property.Id);
            }
            else
            {
                return page.ResolveUrl("~/Property/PropertyPage/" + property.Id);
            }

        }
		
		public static string LinkTo(VolunteerTracker.Deposit deposit, System.Web.UI.TemplateControl page) 
		{			
			return page.ResolveUrl("~/CashJournal/CashJournal.aspx?propertyId=" + deposit.PropertyId + "&year=" + deposit.DepositDate.Year + 
			                       "&month=" + deposit.DepositDate.Month);
		}
		
				
		public static string LinkTo(VolunteerTracker.BillingRecord billingRecord, System.Web.UI.TemplateControl page) 
		{
			
			return page.ResolveUrl("~/Tenant/Billing.aspx?tenantid=" + billingRecord.TenantId + "&rentNum=" + billingRecord.RentTypeIndex);
		}							
		
		public static string LinkTo(VolunteerTracker.Check check, System.Web.UI.TemplateControl page) 
		{
			return "";
		}		*/							
		
		
		
    }
}
