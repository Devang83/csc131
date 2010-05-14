// DownloadRentRoll.aspx.cs created with MonoDevelop
// User: bbell at 1:02 PM 1/24/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Web;
using System.Web.UI;

	
	public partial class Property_DownloadRentRoll : System.Web.UI.Page
	{
		
		protected void Page_Load(object sender, EventArgs e)
    	{
        	QuickPMWebsite.DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, Request);
        	DateTime date = DateTime.Today;
        	int year = DateTime.Today.Year;        
        	int month = DateTime.Today.Month;

        	if (Int32.TryParse(Request["Year"], out year) && Int32.TryParse(Request["Month"], out month))
        	{
            	date = new DateTime(year, month, 1);
        	}
        	int propertyId;
        	if (!Int32.TryParse(Request["PropertyId"], out propertyId))
        	{
            	//return null;
        	}
        	QuickPM.Property property = new QuickPM.Property(propertyId);        
        
        	string fileName = Request.PhysicalApplicationPath + "/App_Data/Tmp/" + "RentRoll.pdf";
        	//System.IO.FileStream file = new System.IO.FileStream(fileName, System.IO.FileMode.Create);




        	QuickPMWebsite.RentRollCSV.GenerateRentRollPdf(property.Id, false, new QuickPM.Period(date.Year, date.Month), fileName);
        	//doc.Data = new UTF8Encoding(true).GetBytes("test");
        	//file.Write(doc.Data, 0, doc.Data.Length);
        	//long fileLength = file.Length;
        	//file.Close();
        	Response.Clear();
        	Response.AddHeader("Content-Disposition", "attachment; filename=" + "RentRoll.pdf");
        	//Response.AddHeader("Content-Length", fileLength.ToString());
        	Response.ContentType = "application/octet-stream";			
        	Response.WriteFile(fileName);
        	Response.End();   	
        }
	}
