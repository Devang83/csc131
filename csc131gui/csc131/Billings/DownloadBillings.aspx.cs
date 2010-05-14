
using System;
using System.Web;
using System.Web.UI;

namespace QuickPMWebsite
{


	public partial class DownloadBillings : System.Web.UI.Page
	{
		
		protected void Page_Load(object sender, EventArgs e)
    	{
        	QuickPMWebsite.DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, Request);
        	long PropertyId = long.Parse(Request["PropertyId"]);
        	int year = int.Parse(Request["Year"]);	
        	int month = int.Parse(Request["Month"]);
        	PrintTenantBilling.url = "http://cmd.quickpm.net" + "/Public/Tenant.aspx";        
        	PDFjet.NET.PDF document = PrintBillings.GetPdf(PropertyId, year, month);        
			string filePathName = Request.PhysicalApplicationPath + "/Billings/" + "Billings.pdf";
			document.Save(filePathName); 
		
			byte[] fileBytes = System.IO.File.ReadAllBytes(filePathName);        	
        	QuickPM.Period period = new QuickPM.Period(year, month);
			Response.Clear();
        	Response.AddHeader("Content-Disposition", "attachment; filename=\"#" + PropertyId.ToString() + " " + new QuickPM.Property(PropertyId).Name + " " + period.ToString() + ".pdf\"");
        	Response.AddHeader("Content-Length", fileBytes.Length.ToString());
        	Response.ContentType = "application/octet-stream";        
        	Response.WriteFile(filePathName);
			Response.End();        	
			
    	}
	}
}
