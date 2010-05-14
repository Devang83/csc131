
using System;
using System.Web;
using System.Web.UI;

namespace QuickPMWebsite
{


	public partial class DownloadTenantContactInformation : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
    	{
        	QuickPMWebsite.DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, Request);
        	long propertyId;
        	if (!long.TryParse(Request["PropertyId"], out propertyId))
        	{
            	return;
        	}
        	QuickPM.Property property = new QuickPM.Property(propertyId);        
        
        	/*string fileName = Request.PhysicalApplicationPath + "/App_Data/Tmp/" + "RentRoll.pdf";*/
        	//System.IO.FileStream file = new System.IO.FileStream(fileName, System.IO.FileMode.Create);



			string tenantContactInformation = QuickPMWebsite.ContactInformationCsv.GenerateContactInformationCsv(property.Id, false, new QuickPM.Period(DateTime.Today.Year, DateTime.Today.Month));
        	
        	Response.Clear();
        	Response.AddHeader("Content-Disposition", "attachment; filename=" + "TenantContactInformation.csv");        	
        	Response.ContentType = "application/octet-stream";			
        	Response.Write(tenantContactInformation);
        	Response.End();   	
        }
	}
}
