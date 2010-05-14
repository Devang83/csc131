using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Property_Reports_DownloadCSVRentRoll : System.Web.UI.Page
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
        
        string fileName = Request.PhysicalApplicationPath + "/App_Data/Tmp/" + "RentRoll.csv";
        //System.IO.FileStream file = new System.IO.FileStream(fileName, System.IO.FileMode.Create);


				
        QuickPMWebsite.RentRollCSV.GenerateRentRollCsv(property.Id, false, new QuickPM.Period(date.Year, date.Month), fileName);
        //doc.Data = new UTF8Encoding(true).GetBytes("test");
        //file.Write(doc.Data, 0, doc.Data.Length);
        //long fileLength = file.Length;
        //file.Close();
        Response.Clear();
        Response.AddHeader("Content-Disposition", "attachment; filename=" + "RentRoll.csv");
        //Response.AddHeader("Content-Length", fileLength.ToString());
        Response.ContentType = "application/octet-stream";
        Response.WriteFile(fileName);
        Response.End();   	
    }
}
